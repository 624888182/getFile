using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
 
 
using System.Collections;
using System.Data;
using System.Data.OracleClient;

//Create by james 2006/08/09


/// <summary>
/// DbAccessORA 的摘要说明

/// </summary>
public class DbAccessORA: System.Web.UI.Page
{
    public static string ClassName = "COracleDB";

    private string m_strPath;
    private bool m_bIsXML;
    private string strDBName;
    private string strUserName;
    private bool m_bTrans;   //Transaction
    private OracleTransaction myTrans;
   // private ConnectionStatuts m_ConnectState;
    private OracleConnection m_oConn = null;

    #region Property method
   // public ConnectionStatuts ConnectionStatuts
   // {
   //     get { return m_ConnectState; }
   // }
    public string User
    {
        get { return strUserName; }
    }
    public string DBName
    {
        get { return strDBName; }
    }
    #endregion
 
    
        protected OracleConnection oraConn;

        public void Dispose()
        {
            if (oraConn != null)
            {
                if (oraConn.State != ConnectionState.Closed)
                {
                    oraConn.Close();
                }
                oraConn.Dispose();
            }
        }
     
        protected virtual void OracleDB(string DB)
        {
            try
            {
 

                oraConn = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings[ DB ]);
                oraConn.Open();
            }
            catch (OracleException ex)
            {
                //Log.Write("[ERROR] " + ex.Message + ":COracleDB()");
            }
        }
 
        
        public OracleDataReader GetDataReader(string strBySql)
        {
            OracleDataReader odr = null;
            OracleCommand oCmd = null;
            try
            {
                oCmd = new OracleCommand(strBySql, oraConn);
                odr = oCmd.ExecuteReader();
            }
            catch (OracleException ex)
            {
                //Log.Write("[ERROR] " + ex.Message + ":GetDataReader()");
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Dispose();
                }
            }
            return odr;
        }
                
        public bool ExecuteNonQuery(string strBySql)
        {
            bool bRet = false;
            OracleCommand oCmd = null;
            try
            {
                oCmd = new OracleCommand(strBySql, oraConn);
                int i = Convert.ToInt32(oCmd.ExecuteNonQuery());
                if (i > 0)
                { 
                    bRet = true;
                }
            }
            catch (OracleException ex)
            {
               // Log.Write("[ERROR] " + ex.Message + ":ExecuteNonQuery - " + strBySql);
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Dispose();
                }
            }

            return bRet;
        }

        public DataSet GetDataSet(string strBySql)
        {
            DataSet odr = new DataSet();
            OracleCommand oCmd = null;
            try
            {
                oCmd = new OracleCommand(strBySql, oraConn);
                OracleDataAdapter dd = new OracleDataAdapter();
                dd.SelectCommand = oCmd;
                dd.Fill(odr);
            }
            catch (OracleException ex)
            {
                //Log.Write("[ERROR] " + ex.Message + ":GetDataReader()");
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Dispose();
                }
            }

            return odr;
        }
        public DataTable  GetDataTable(string strBySql,string DBStr)
        {   string DBconn;
            DBconn = System.Configuration.ConfigurationManager.AppSettings[DBStr];
            oraConn = new OracleConnection();
            oraConn.ConnectionString = DBconn;
            oraConn.Open();
            DataSet odr = new DataSet();
            OracleCommand oCmd = null;
          try
            {
                oCmd = new OracleCommand(strBySql, oraConn);
                OracleDataAdapter dd = new OracleDataAdapter();
                dd.SelectCommand = oCmd;
                dd.Fill(odr);
            }
            catch (OracleException ex)
            {
                //Log.Write("[ERROR] " + ex.Message + ":GetDataReader()");
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Dispose();
                }
            }

            return odr.Tables [0];
        }
                
    
        public bool ExecuteStoredProcedure(string strStoredProcedureName, string[] strParams, string[] strValue, out string[] strOutput)
        {
            OracleCommand ocmd = null;
            bool bStatus = false;
            strOutput = new string[strParams.Length - strValue.Length];
            try
            {
                ocmd = new OracleCommand();
                ocmd.Connection = oraConn;
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.CommandText = strStoredProcedureName;
                for (int i = 0; i < strParams.Length; i++)
                {
                    if (i > strValue.Length - 1)
                    {
                        ocmd.Parameters.Add(strParams[i], OracleType.NVarChar, 200);
                        ocmd.Parameters[strParams[i]].Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        ocmd.Parameters.AddWithValue(strParams[i], strValue[i]);
                    }
                }
                ocmd.ExecuteNonQuery();
                int iOutputIndx = 0;
                for (int i = strValue.Length; i < strParams.Length; i++)
                {
                    strOutput[iOutputIndx] = ocmd.Parameters[strParams[i]].Value.ToString();
                    iOutputIndx++;
                }
                bStatus = true;
            }
            catch (OracleException ex)
            {
                bStatus = false;
                //Log.Write("[ERROR] " + ex.Message + " :ExecuteStoredProcedure() :" + strStoredProcedureName);
            }
            finally
            {
                if (ocmd != null)
                {
                    ocmd.Dispose();
                }

            }
            return bStatus;
        }



        public bool CheckTextbox(string strTB)
        {


            bool flag = false;
            if (strTB.Contains(" "))
                flag = true;
            else if (strTB.Contains("'"))
                flag = true;
            else if (strTB.Contains("\\"))
                flag = true;
            else if (strTB.Contains("\""))
                flag = true;
            else if (strTB.Contains("$"))
                flag = true;
            else if (strTB.Contains(","))
                flag = true;
            else if (strTB.Contains(":"))
                flag = true;
            else if (strTB.Contains("."))
                flag = true;
            else if (strTB.Contains("!"))
                flag = true;
            else if (strTB.Contains("^"))
                flag = true;
            //new string str[10] = {"'",";",","," ","@","/","\"","\\","(",")","!"};
            //strTB.Contains(
            flag = !flag;
            return flag;
        }   
     
    

}
