using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using SCM.GSCMDKen;
using EconomyUser;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Economy.BLL;
using Economy.Publibrary;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using SFC.TJWEB;

/// <summary>
/// Summary description for DataBaseOperation
/// </summary>
public class PDataBaseOperation
{
    public static string PCode = "";
    public static string PSite = "";
    Convertlib ConvertlibPointer = new Convertlib();
    ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();

    #region privateVariant

    private static string FERROR = "";
    private static int ERRORCode = 0;
    private string sDBType = "";
    private OracleConnection oraConn= null;
    private OracleTransaction oraTran = null;
    private SqlConnection sqlConn = null;
    private SqlTransaction sqlTran = null;
    



    #endregion

    #region ConstitutionFunction

    public PDataBaseOperation(string dbtype,string connstr)
	{
        string tmp1 = tmpCProConvert(connstr);  // string tmp1 = ProConvert(connstr);
        if (tmp1 != "") connstr = tmp1;

        if (dbtype.ToLower().Trim() == "oracle")
        {
            sDBType = "oracle";
            oraConn = new OracleConnection(connstr);
        
        }
        else if (dbtype.ToLower().Trim() == "sql")
        {
            sDBType = "sql";
            sqlConn = new SqlConnection(connstr);
        }
        else
        {
            SetErrorInfo("First param error", 1);
        }
    }

    public void Dispose()
    {
        if (oraConn != null)
        {
            oraConn.Close();
            oraConn.Dispose();
        }
        if (sqlConn != null)
        {
            sqlConn.Close();
            sqlConn.Dispose();
        }
    
    }

    #endregion

    #region privateFunctions

    /* Function SetErrorInfo
     * set itself information
     * Input 
     * 1: Error Message
     * 2: Error Code
     */
    private static void SetErrorInfo(string Msg, int Code)
    {
        FERROR = Msg;
        ERRORCode = Code;
    }

    #region Create&Connect

    /* Connecte Oracle/sql 
    * check conn status , if it is not open then try reconnect 
    * output 
    * zero success 
    * non-zero fail
    */

    private int ConnecteOracle()
    {
        try
        {
            if (sDBType == "oracle")
            {
                if (oraConn.State != ConnectionState.Open)
                    oraConn.Open();
            }
            else
            {
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
            }
            SetErrorInfo("", 0);
        }
        catch (Exception e)
        {
            SetErrorInfo(e.Message, -1);
        }
        return ERRORCode;
    }

    /* CreateOracleConnection
     * Create an oracle connection and try open it
     * input connect string
     * oracle connction object
     */
    private static OracleConnection CreateOracleConnection(string connstr)
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

    /* CreateSQLConnection
     * Create a SqlServer Connection and try open it
     * input Connect string
     * sqlserver connection object
     */
    private static SqlConnection CreateSqlConnection(string connstr)
    {
        SqlConnection conn = new SqlConnection(connstr);
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

    #endregion

    #region CreateCommand

    /* CreateCommand
     * Create an oracle command object and set sql & parameters
     * input 
     * 0:OracleConnection
     * 1: sql
     * 2: parameters name
     * 3: parameters value
     * output OracleCommand
     */
    private static OracleCommand CreateCommand(ref OracleConnection conn, string sqlstr, string[] paramName, object[] paramValue)
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

    /* CreateSQLCommand
        * Create an sql command object and set sql & parameters
        * input 
        * 0:SqlConnection
        * 1: sql
        * 2: parameters name
        * 3: parameters value
        * output SqlCommand
        */
    private static SqlCommand CreateCommand(ref SqlConnection conn, string sqlstr, string[] paramName, object[] paramValue)
    {
        SqlCommand cmd = null;
        if (ERRORCode == 0)
        {
            try
            {
                cmd = new SqlCommand(sqlstr, conn);
                int i;
                for (i = 0; i < paramName.GetLength(0); i++)
                    cmd.Parameters.AddWithValue(paramName[i], paramValue[i]);
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return cmd;
    }

    #endregion

    #region CreateDataAdapter

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

    /* CreateSqlDataAdapter
     * Create an SqlDataAdapter and set select command 
     * input SqlCommand
     * output SqlDataAdapter
     */
    private static SqlDataAdapter CreateDataAdapter(ref SqlCommand cmd)
    {
        SqlDataAdapter oda = null;
        if (ERRORCode == 0)
        {
            try
            {
                oda = new SqlDataAdapter();
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

    #endregion

    #region FillDate

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

    /* FillDataDT
     * let SqlDataAdapter fill with DataTable
     * input SqlDataAdapter
     * output DataTable
     */
    private static DataTable FillDataDT(ref SqlDataAdapter sda)
    {
        DataTable dt = new DataTable();
        if (ERRORCode == 0)
        {
            try
            {
                sda.Fill(dt);
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
    private static DataSet FillDataDS(ref OracleDataAdapter oda)
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

    /* FillDataDS
    * let SqlDataAdapter fill with DataSet
    * input SqlDataAdapter
    * output DataSet
    */
    private static DataSet FillDataDS(ref SqlDataAdapter sda)
    {
        DataSet ds = new DataSet();
        if (ERRORCode == 0)
        {
            try
            {
                sda.Fill(ds);
                SetErrorInfo("", 0);
            }
            catch (Exception e)
            {
                SetErrorInfo(e.Message, -1);
            }
        }
        return ds;
    }

    #endregion

    #region ExecCommand

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

    /* ExecCommand
     * execut a sql without return data set
     * input SqlCommand
     * output effect rows count
     */
    private static int ExecCommand(ref SqlCommand cmd)
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

    #endregion

    #region Release

    /* Release
     * Release oracle db object
     */
    private static void Release(ref OracleConnection conn, ref OracleCommand cmd, ref OracleDataAdapter oda)
    {
        if (oda != null) oda.Dispose();
        if (cmd != null) cmd.Dispose();
        if (conn != null)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
        }
    }

    /* ReleaseSql
     * Release sql server db object
     */
    private static void Release(ref SqlConnection conn, ref SqlCommand cmd, ref SqlDataAdapter sda)
    {
        if (sda != null) sda.Dispose();
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

    /* Release
         * Release oracle db object
         */
    private static void Release(ref SqlConnection conn, ref SqlCommand cmd)
    {
        if (cmd != null) cmd.Dispose();
        if (conn != null)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
        }
    }

    private void Release(ref OracleCommand cmd, ref OracleDataAdapter oda)
    {
        if (cmd != null) cmd.Dispose();
        if (oda != null) oda.Dispose();
    }

    private void Release(ref SqlCommand cmd, ref SqlDataAdapter oda)
    {
        if (cmd != null) cmd.Dispose();
        if (oda != null) oda.Dispose();
    }

    private void Release(ref SqlCommand cmd)
    {
        if (cmd != null) cmd.Dispose();
    }

    private void Release(ref OracleCommand cmd)
    {
        if (cmd != null) cmd.Dispose();
    }

    #endregion

    #endregion

    #region publicFunction

    public static string GetError()
    {
        return FERROR;
    }

    #region SelectSQLDT

    /* SelectSQLDT
     * Exec a sql query and return a datatable
     * input 
     * 1: connection string
     * 2: select sql
     * output datatable
     */
    public static DataTable PSelectSQLDT(string dbtype,string connstr, string sqlstr)
    {
        DataTable dt = null;
        string tmp1 = tmpCProConvert(connstr);  // string tmp1 = ProConvert(connstr);
        if (tmp1 != "") connstr = tmp1;

        if (dbtype.ToLower().Trim() == "oracle")
        {
            
            OracleConnection conn = CreateOracleConnection(connstr);
            OracleCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref conn, ref cmd, ref oda);
            
        }
        else if (dbtype.ToLower().Trim() == "sql")
        {
            SqlConnection conn = CreateSqlConnection(connstr);
            SqlCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
            SqlDataAdapter sda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref sda);
            Release(ref conn, ref cmd, ref sda);
        }
        else
        {
            SetErrorInfo("First param error", 1);
        }
        return dt;
    }

    /* SelectSQLDT
    * Exec a sql query and return a datatable
    * input 
    * 1: connection string
    * 2: select sql
    * 3: parameters name array
    * 4: parameters value array
    * output datatable
    */
    public static DataTable PSelectSQLDT(string dbtype,string connstr, string sqlstr, string[] paramName, object[] paramValue)
    {
        string tmp1 = tmpCProConvert(connstr);  // string tmp1 = ProConvert(connstr);
        if (tmp1 != "") connstr = tmp1;
        DataTable dt = null;
        if (dbtype.ToLower().Trim() == "oracle")
        {
            OracleConnection conn = CreateOracleConnection(connstr);
            OracleCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref conn, ref cmd, ref oda);
        }
        else if (dbtype.ToLower().Trim() == "sql")
        {
            SqlConnection conn = CreateSqlConnection(connstr);
            SqlCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
            SqlDataAdapter sda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref sda);
            Release(ref conn, ref cmd, ref sda);
        }
        else
        {
            SetErrorInfo("First param error", 1);
        }

        return dt;
    }

    /*  SelectSQLDT
     *  execute a sql and return datatable
     *  input sql
     *  out put dataTable
     */
    public DataTable PSelectSQLDT(string sqlstr)
    {
        DataTable dt = null;
        ConnecteOracle();
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, new string[] { }, new object[] { });
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, new string[] { }, new object[] { });
            SqlDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        return dt;
    }

    public DataTable PSelectSQLDTWithTran(string sqlstr)
    {
        DataTable dt = null;
        ConnecteOracle();
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, new string[] { }, new object[] { });
            cmd.Transaction = oraTran;
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, new string[] { }, new object[] { });
            cmd.Transaction = sqlTran;
            SqlDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        return dt;
    }

    /*  SelectSQLDT
      *  execute a sql and return datatable
      *  input 
      *  1: sql
      *  2: parameters name array
      *  3:  parameters value array
      *  out put dataTable
      */
    public DataTable PSelectSQLDT(string sqlstr, string[] paramName, object[] paramValue)
    {
        ConnecteOracle();
        DataTable dt = null;
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, paramName, paramValue);
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, paramName, paramValue);
            SqlDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        return dt;
    }

    public DataTable PSelectSQLDTWithTran(string sqlstr, string[] paramName, object[] paramValue)
    {
        ConnecteOracle();
        DataTable dt = null;
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, paramName, paramValue);
            cmd.Transaction = oraTran;
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, paramName, paramValue);
            cmd.Transaction = sqlTran;
            SqlDataAdapter oda = CreateDataAdapter(ref cmd);
            dt = FillDataDT(ref oda);
            Release(ref cmd, ref oda);
        }
        return dt;
    }

    #endregion

    #region PSelectSQLDS

    /* SelectSQLDS
    * Exec a sql query and return a DataSet
    * input 
    * 1: connection string
    * 2: select sql
    * output DataSet
    */
    public static DataSet PSelectSQLDS(string dbtype,string connstr, string sqlstr)
    {
        string tmp1 = tmpCProConvert(connstr);  // string tmp1 = ProConvert(connstr);
        if (tmp1 != "") connstr = tmp1;
        DataSet ds = null;

        //ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        //string tmp1 = LibPDBA1Pointer.CProConvert(connstr);
      
        if (dbtype.ToLower().Trim() == "oracle")
        {
            OracleConnection conn = CreateOracleConnection(connstr);
            OracleCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref oda);
            Release(ref conn, ref cmd, ref oda);
        }
        else if (dbtype.ToLower().Trim() == "sql")
        {
            SqlConnection conn = CreateSqlConnection(connstr);
            SqlCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
            SqlDataAdapter sda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref sda);
            Release(ref conn, ref cmd, ref sda);
        }
        else
        {
            SetErrorInfo("First param error", 1);
        }
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
    public static DataSet PSelectSQLDS(string dbtype,string connstr, string sqlstr, string[] paramName, object[] paramValue)
    {
        string tmp1 = tmpCProConvert(connstr);  // string tmp1 = ProConvert(connstr);
        if (tmp1 != "") connstr = tmp1;

        DataSet ds = null;
        if (dbtype.ToLower().Trim() == "oracle")
        {
            OracleConnection conn = CreateOracleConnection(connstr);
            OracleCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref oda);
            Release(ref conn, ref cmd, ref oda);
        }
        else if (dbtype.ToLower().Trim() == "sql")
        {
            SqlConnection conn = CreateSqlConnection(connstr);
            SqlCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
            SqlDataAdapter oda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref oda);
            Release(ref conn, ref cmd, ref oda);
        }
        else
        {
            SetErrorInfo("First param error", 1);
        }
        return ds;
    }

    /*  SelectSQLDS
     *  execute a sql and return dataset
     *  input sql
     *  output dataset
     */
    public DataSet PSelectSQLDS(string sqlstr)
    {
        
        ConnecteOracle();
        DataSet ds = null;
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, new string[] { }, new object[] { });
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref oda);
            Release(ref cmd, ref oda);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, new string[] { }, new object[] { });
            SqlDataAdapter oda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref oda);
            Release(ref cmd, ref oda);
        }
        return ds;
    }

    /*  SelectSQLDS
     *  execute a sql and return dataset
     *  
     *  input 
     *  1: sql
     *  2: parameters name array
     *  3: parameters value array
     *  output dataset
     */
    public DataSet PSelectSQLDS(string sqlstr, string[] paramName, object[] paramValue)
    {
        ConnecteOracle();
        DataSet ds = null;
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, paramName, paramValue);
            OracleDataAdapter oda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref oda);
            Release(ref cmd, ref oda);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, paramName, paramValue);
            SqlDataAdapter oda = CreateDataAdapter(ref cmd);
            ds = FillDataDS(ref oda);
            Release(ref cmd, ref oda);
        }
        return ds;
    }

    #endregion


    #region Trans

    public int BeginTran()
    {
        int iRet = 0;
	ConnecteOracle();
        try
        {
            if (sDBType.ToLower() == "sql")
            {
                sqlTran = sqlConn.BeginTransaction();
            }
            else
            {
                oraTran = oraConn.BeginTransaction();
            }
            iRet = 0;
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        return iRet;

    }

    public int CommitTran()
    {
        int iRet = 0;
        try
        {
            if (sDBType.ToLower() == "sql")
            {
                sqlTran.Commit();
            }
            else
            {
                oraTran.Commit();
            }
            iRet = 0;
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        return iRet;        
    
    }

    public int Rollback()
    {
        int iRet = 0;
        try
        {
            if (sDBType.ToLower() == "sql")
            {
                sqlTran.Rollback();
            }
            else
            {
                oraTran.Rollback();
            }
            iRet = 0;
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        return iRet;

    }


    #endregion

    #region PExecSQL

    /* ExecSQL
     * Exec a sql noquery 
     * input 
     * 1: connection string
     * 2: select sql
     * output effect rows count less zero is an error.
     */
    public static int PExecSQL(string dbtype,string connstr, string sqlstr)
    {
        string tmp1 = tmpCProConvert(connstr); // string tmp1 = ProConvert(connstr);
        if (tmp1 != "") connstr = tmp1;

        int iRet = 0;
        if (dbtype.ToLower().Trim() == "oracle")
        {
            OracleConnection conn = CreateOracleConnection(connstr);
            OracleCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
            iRet = ExecCommand(ref cmd);
            Release(ref conn, ref cmd);
        }
        else if (dbtype.ToLower().Trim() == "sql")
        {
            SqlConnection conn = CreateSqlConnection(connstr);
            SqlCommand cmd = CreateCommand(ref conn, sqlstr, new string[] { }, new object[] { });
            iRet = ExecCommand(ref cmd);
            Release(ref conn, ref cmd);
        }
        else
        {
            SetErrorInfo("First param error", 1);
        }
        return iRet;
    }

    /* ExecSQL
     * Exec a sql noquery with oracle parameters
     * input 
     * 1: connection string
     * 2: select sql
     * 3: parameters name array
     * 4: parameters value array
     * output effect rows count less zero is an error.
     */
    public static int PExecSQL(string dbtype,string connstr, string sqlstr, string[] paramName, object[] paramValue)
    {
        string tmp1 = tmpCProConvert(connstr); //string tmp1 = ProConvert(connstr);
        if (tmp1 != "") connstr = tmp1;

        int iRet = 0;
        if (dbtype.ToLower().Trim() == "oracle")
        {
            OracleConnection conn = CreateOracleConnection(connstr);
            OracleCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
            iRet = ExecCommand(ref cmd);
            Release(ref conn, ref cmd);
        }
        else if (dbtype.ToLower().Trim() == "sql")
        {
            SqlConnection conn = CreateSqlConnection(connstr);
            SqlCommand cmd = CreateCommand(ref conn, sqlstr, paramName, paramValue);
            iRet = ExecCommand(ref cmd);
            Release(ref conn, ref cmd);
        }
        return iRet;
    }

    /*  ExecSQL
      *  Executes a sql with noquery
      *  input sql
      *  output effect rows count
      */
    public int PExecSQL(string sqlstr)
    {
        
        int iRet = ConnecteOracle();
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, new string[] { }, new object[] { });
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, new string[] { }, new object[] { });
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
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
    public int PExecSQL(string sqlstr, string[] paramName, object[] paramValue)
    {
        
        int iRet = ConnecteOracle();
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, paramName, paramValue);
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, paramName, paramValue);
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
        return iRet;
    }

       

    public int PExecSQLTran(string sqlstr)
    {
        
        int iRet = ConnecteOracle();
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, new string[] { }, new object[] { });
            cmd.Transaction = oraTran;
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, new string[] { }, new object[] { });
            cmd.Transaction = sqlTran;
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
        return iRet;
    }

    public int PExecSQLTran(string sqlstr, string[] paramName, object[] paramValue)
    {
        
        int iRet = ConnecteOracle();
        if (sDBType == "oracle")
        {
            OracleCommand cmd = CreateCommand(ref oraConn, sqlstr, paramName, paramValue);
            cmd.Transaction = oraTran;
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
        else
        {
            SqlCommand cmd = CreateCommand(ref sqlConn, sqlstr, paramName, paramValue);
            cmd.Transaction = sqlTran;
            iRet = ExecCommand(ref cmd);
            Release(ref cmd);
        }
        return iRet;
    }

    public static string init_var(string p1, string p2)
    {
        int v1 = 0;
        PSite = p1;
        PCode = p2;
        return ( "" );
    }

    public static string ProConvert(string InSource)
    {
        string Retcode = "";
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        if  ( (PSite.ToLower() == "tjsfc") || (PSite.ToLower() == "zzsfc") )
               Retcode = LibUSR1Pointer.DBDeEncCode(InSource, PCode, ",", "2DBA");
        
        return (Retcode);
    }
       

    #endregion

    private static string tmpCProConvert(string connstr)
    {
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string tmp1 = LibPDBA1Pointer.CProConvert(connstr);
        return (tmp1);
    }

    #endregion


}
