using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;

/// <summary>
/// Summary description for DataBaseOracle
/// </summary>
public class DataBaseOracle
{
    #region privateVariant

    private static string FERROR = "";
    private static int ERRORCode = 0;

    #endregion

    #region ConstitutionFunction

    public DataBaseOracle()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #endregion

    #region privateFunctions

    /* Function SetErrorInfo
     * set itself information
     */
    private static void SetErrorInfo(string Msg, int Code)
    {
        FERROR = Msg;
        ERRORCode = Code;
    }

    /* CreateConnection
     * Create an oracle connection and try open it
     * input connect string
     * oracle connction object
     */ 
    private static OracleConnection CreateConnection(string connstr)
    {
        OracleConnection conn = new OracleConnection(connstr);
        try
        {
            conn.Open();
            SetErrorInfo("", 0);
        }
        catch (Exception e)
        {
            SetErrorInfo(e.Message, -1);
        }
        return conn;
    }

    /* ConnectOracle
     * if OracleConnection status is not open try open it
     * input OracleConnection object
     */
    private static int ConnectOracle(ref OracleConnection conn)
    {
        if (ERRORCode != 0) return ERRORCode;
        try
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SetErrorInfo("",0);
        }
        catch (Exception e)
        {
            SetErrorInfo(e.Message, -1);
        }
        return ERRORCode;
    
    }

    /* CreateCommand
     * Create an oracle command object and set sql & parameters
     * input 
     * 0:OracleConnection
     * 1: sql
     * 2: parameters name
     * 3: parameters value
     * output OracleCommand
     */
    private static OracleCommand CreateCommand(ref OracleConnection conn ,string sqlstr,string[] paramName,object[] paramValue)
    {
        OracleCommand cmd = null;
        if (ERRORCode == 0)
        {
            try
            {
                cmd = new OracleCommand(sqlstr, conn);
                int i;
                for (i = 0; i < paramName.GetLength(0); i++)
                    cmd.Parameters.Add(paramName[i], paramValue[i]);
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return cmd;
    }

    /* OracleDataAdapter
     * Create an OracleDataAdapter and set select command 
     * input OracleCommand
     * output OracleDataAdapter
     */
    private static OracleDataAdapter CreateDataAdapter(ref OracleCommand cmd)
    {
        OracleDataAdapter oda = null;
        if (ERRORCode == 0)
        {
            try
            {
                oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return oda;
    }

    /* FillDataDT
     * let OracleDataAdapter fill with DataTable
     * input OracleDataAdapter
     * output DataTable
     */
    private static DataTable FillDataDT(ref OracleDataAdapter oda)
    {
        DataTable dt = new DataTable();
        if (ERRORCode == 0)
        {
            try
            {
                oda.Fill(dt);
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return dt;
    }

    /* FillDataDS
    * let OracleDataAdapter fill with DataSet
    * input OracleDataAdapter
    * output DataSet
    */
    private static  DataSet FillDataDS(ref OracleDataAdapter oda)
    {
        DataSet ds = new DataSet();
        if (ERRORCode == 0)
        {
            try
            {
                oda.Fill(ds);
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return ds;    
    }

    /* ExecCommand
     * execut a sql without return data set
     * input OracleCommand
     * output effect rows count
     */
    private static int ExecCommand(ref OracleCommand cmd)
    {
        int iRet = ERRORCode;
        if (ERRORCode == 0)
        {
            try
            {
                iRet = cmd.ExecuteNonQuery();
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return iRet;
    
    }

    /* Release
     * Release oracle db object
     */
    private static void Release(ref OracleConnection conn,ref OracleCommand cmd ,ref OracleDataAdapter oda)
    {
        if (oda != null) oda.Dispose();
        if (cmd != null) cmd.Dispose();
        if (conn != null)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
        }
    }

    /* Release
     * Release oracle db object
     */
    private static void Release(ref OracleConnection conn, ref OracleCommand cmd)
    {
        if (cmd != null) cmd.Dispose();
        if (conn != null)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
        }
    }

    #endregion

    #region publicFunction

    /* Function Return Error Information
         *  Inpub None
         *  Out Error 
         */
    public static string GetError()
    {
        return FERROR;
    }

    /* SelectSQLDT
     * Exec a sql query and return a datatable
     * input 
     * 1: connection string
     * 2: select sql
     * output datatable
     */
    public static DataTable SelectSQLDT(string connstr, string sqlstr)
    {
        OracleConnection conn = CreateConnection(connstr);
        OracleCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
        OracleDataAdapter oda = CreateDataAdapter(ref cmd);
        DataTable dt = FillDataDT(ref oda);
        Release(ref conn, ref cmd, ref oda);
        return dt;
    }

    /* SelectSQLDT
    * Exec a sql query and return a datatable
    * input 
    * 1: connection string
    * 2: select sql
    * 3: oracle parameters name array
    * 4: oracle parameters value array
    * output datatable
    */
    public static DataTable SelectSQLDT(string connstr, string sqlstr,string[] paramName,object[] paramValue)
    {
        OracleConnection conn = CreateConnection(connstr);
        OracleCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
        OracleDataAdapter oda = CreateDataAdapter(ref cmd);
        DataTable dt = FillDataDT(ref oda);
        Release(ref conn, ref cmd, ref oda);
        return dt;
    }


    /* SelectSQLDS
    * Exec a sql query and return a DataSet
    * input 
    * 1: connection string
    * 2: select sql
    * output DataSet
    */
    public static DataSet  SelectSQLDS(string connstr, string sqlstr)
    {
        OracleConnection conn = CreateConnection(connstr);
        OracleCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
        OracleDataAdapter oda = CreateDataAdapter(ref cmd);
        DataSet ds = FillDataDS(ref oda);
        Release(ref conn, ref cmd, ref oda);
        return ds;
    }

    /* SelectSQLDS
     * Exec a sql query and return a DataSet
     * input 
     * 1: connection string
     * 2: select sql
     * 3: oracle parameters name array
     * 4: oracle parameters values array
     * output DataSet
     */
    public static DataSet SelectSQLDS(string connstr, string sqlstr, string[] paramName, object[] paramValue)
    {
        OracleConnection conn = CreateConnection(connstr);
        OracleCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
        OracleDataAdapter oda = CreateDataAdapter(ref cmd);
        DataSet ds = FillDataDS(ref oda);
        Release(ref conn, ref cmd, ref oda);
        return ds;
    }

    /* ExecSQL
     * Exec a sql noquery 
     * input 
     * 1: connection string
     * 2: select sql
     * output effect rows count less zero is an error.
     */
    public static int ExecSQL(string connstr, string sqlstr)
    {
        OracleConnection conn = CreateConnection(connstr);
        OracleCommand cmd = CreateCommand(ref conn, sqlstr,new string[] {}, new object[] {});
        int iRet = ExecCommand(ref cmd);
        Release(ref conn, ref cmd);
        return iRet;
    }

    /* ExecSQL
     * Exec a sql noquery with oracle parameters
     * input 
     * 1: connection string
     * 2: select sql
     * 3: oracle parameters name array
     * 4: oracle parameters value array
     * output effect rows count less zero is an error.
     */
    public static int ExecSQL(string connstr, string sqlstr, string[] paramName, object[] paramValue)
    {
        OracleConnection conn = CreateConnection(connstr);
        OracleCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
        int iRet = ExecCommand(ref cmd);
        Release(ref conn, ref cmd);
        return iRet;
    }


    #endregion
}


public class DataBaseOracleConnection
{
    #region PrivateVariant

    OracleConnection conn;
    string FERROR;
    int ErrorCode;

    #endregion
    
    #region ConstitutionFunction

    /* ConstitutionFunction: DataBaseOracleConnection
     * create OracleConnection
     * Input connection string
     */
    public DataBaseOracleConnection(string connstr)
	{
        conn = new OracleConnection(connstr);
    }

    #endregion

    #region privateFunctions

    /* SetErrorInfo
     * Set private variant FERROR & ErrorCode
     * Input 
     * 1: Error Message
     * 2: Error Code
     */ 
    private void SetErrorInfo(string Msg, int Code)
    {
        FERROR = Msg;
        ErrorCode = Code;
    }

    /* ConnecteOracle
     * check conn status , if it is not open then try reconnect 
     * output 
     * zero success 
     * non-zero fail
     */
    private int ConnecteOracle()
    {
        try
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            SetErrorInfo("", 0);
        }
        catch (Exception e)
        {
            SetErrorInfo(e.Message, -1);
        }
        return ErrorCode;
    }

    /* OracleCommand
     * Create an oracle command and set sql & oracle parameters
     * input 
     * 1: sql 
     * 2: oralce parameters name array
     * 3: oracle parameters value array
     */
    private OracleCommand CreateCommand(string sqlstr, string[] paramName, object[] paramValue)
    { 
        OracleCommand cmd = new OracleCommand(sqlstr,conn);
        if (ErrorCode == 0)
        {
            try
            {
                int i;
                for (i = 0; i < paramName.GetLength(0); i++)
                    cmd.Parameters.Add(paramName[i], paramValue[i]);
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return cmd;
    
    }

    /* OracleDataAdapter
     * create an OracleDataAdapter and set selectcommand is cmd
     * input OracleCommand
     * output OracleDataAdapter
     */
    private OracleDataAdapter CreateOracleDataAdapter(ref OracleCommand cmd)
    {
        OracleDataAdapter oda = new OracleDataAdapter();
        if (ErrorCode == 0)
        {
            try 
            {
                oda.SelectCommand = cmd;
                SetErrorInfo("",0);
            }
            catch(Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return oda;
    
    }

    /* FillDataTable
     * Create a DataTable and fill it with oda
     * input OracleDataAdapter
     * outPut DataTable
     */
    private DataTable FillDataTable(ref OracleDataAdapter oda)
    {
        DataTable dt = new DataTable();
        if (ErrorCode == 0)
        {
            try
            {
                oda.Fill(dt);
                SetErrorInfo("", 0);
            }
            catch(Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return dt;
    }

    /* FillDataTable
   * Create a DataTable and fill it with oda
   * input OracleDataAdapter
   * outPut DataTable
   */
    private DataSet FillDataSet(ref OracleDataAdapter oda)
    {
        DataSet ds = new DataSet();
        if (ErrorCode == 0)
        {
            try
            {
                oda.Fill(ds);
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return ds;
    }

    /* ExecCommand
     * execute a sql without query
     * input OracleCommand
     * output effect rows count
     */
    private int ExecCommand(ref OracleCommand cmd)
    {
        int iRet = ErrorCode;
        if (ErrorCode == 0)
        {
            try
            {
                iRet = cmd.ExecuteNonQuery();
                SetErrorInfo("", 0);

            }
            catch(Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return ErrorCode;
    }

    /* release
     * release oracle database objects
     * input
     * 1: OracleCommand
     * 2: OracleDataAdapter
     */
    private void Release(ref OracleCommand cmd, ref OracleDataAdapter oda)
    {
        if (cmd != null) cmd.Dispose();
        if (oda != null) oda.Dispose();
    }

    /* release
     * release oracle database objects
     * input
     * 1: OracleCommand
     * 2: OracleDataAdapter
     */
    private void Release(ref OracleCommand cmd)
    {
        if (cmd != null) cmd.Dispose();
    }


    #endregion

    #region publicFunctions

    /*  SelectSQLDT
     *  execute a sql and return datatable
     *  input sql
     *  out put dataTable
     */ 
    public DataTable SelectSQLDT(string sqlstr)
    {
        ConnecteOracle();
        OracleCommand cmd = CreateCommand(sqlstr, new string[] { }, new object[] { });
        OracleDataAdapter oda = CreateOracleDataAdapter(ref cmd);
        DataTable dt = FillDataTable(ref oda);
        Release(ref cmd, ref oda);
        return dt;
    }

    /*  SelectSQLDT
     *  execute a sql and return datatable
     *  input 
     *  1: sql
     *  2: oracle parameters name array
     *  3: oracle parameters value array
     *  out put dataTable
     */
    public DataTable SelectSQLDT(string sqlstr, string[] paramName, object[] paramValue)
    {
        ConnecteOracle();
        OracleCommand cmd = CreateCommand(sqlstr, paramName, paramValue);
        OracleDataAdapter oda = CreateOracleDataAdapter(ref cmd);
        DataTable dt = FillDataTable(ref oda);
        Release(ref cmd, ref oda);
        return dt;
    }

    /*  SelectSQLDS
     *  execute a sql and return dataset
     *  input sql
     *  output dataset
     */
    public DataSet SelectSQLDS(string sqlstr)
    {
        ConnecteOracle();
        OracleCommand cmd = CreateCommand(sqlstr, new string[] { }, new object[] { });
        OracleDataAdapter oda = CreateOracleDataAdapter(ref cmd);
        DataSet  ds = FillDataSet(ref oda);
        Release(ref cmd, ref oda);
        return ds;
    }

    /*  SelectSQLDS
     *  execute a sql and return dataset
     *  
     *  input 
     *  1: sql
     *  2: oracle parameters name array
     *  3: oracle parameters value array
     *  output dataset
     */
    public DataSet SelectSQLDS(string sqlstr, string[] paramName, object[] paramValue)
    {
        ConnecteOracle();
        OracleCommand cmd = CreateCommand(sqlstr, paramName, paramValue);
        OracleDataAdapter oda = CreateOracleDataAdapter(ref cmd);
        DataSet ds = FillDataSet(ref oda);
        Release(ref cmd, ref oda);
        return ds;
    }

    /*  ExecSQL
     *  Executes a sql with noquery
     *  input sql
     *  output effect rows count
     */ 
    public int ExecSQL(string sqlstr)
    {
        ConnecteOracle();
        OracleCommand cmd = CreateCommand(sqlstr, new string[] { }, new object[] { });
        int iRet = ExecCommand(ref cmd);
        Release(ref cmd);
        return iRet;
    }

    /*  ExecSQL
     *  Executes a sql with noquery
     *  input 
     *  1: sql
     *  2: oracle parameters name array
     *  3: oracle parameters value array
     *  output effect rows count
     */
    public int ExecSQL(string sqlstr,string[] paramName,object[] paramValue)
    {
        ConnecteOracle();
        OracleCommand cmd = CreateCommand(sqlstr, paramName, paramValue);
        int iRet = ExecCommand(ref cmd);
        Release(ref cmd);
        return iRet;
    }


    #endregion

}
