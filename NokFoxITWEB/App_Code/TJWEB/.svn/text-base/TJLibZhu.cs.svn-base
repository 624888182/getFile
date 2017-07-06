using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;
using SCM.GSCMDKen;

/// <summary>
///TJLibZhu 的摘要说明
/// </summary>
public class TJLibZhu
{
    FPubLib FPubLibPointer = new FPubLib();
    OracleConnection connNormal;
    public TJLibZhu()
    {

    }
    public DataSet GetDataSet(string strBySql, string ReadDataBase)
    {
        DataSet odr = new DataSet();
        OracleCommand oCmd = null;
        try
        {
            string strConnNormal = ReadDataBase;
            connNormal = new OracleConnection(strConnNormal);
            if (connNormal.State == ConnectionState.Closed)
            {
                connNormal.Open();
            }
            oCmd = new OracleCommand(strBySql, connNormal);
            OracleDataAdapter dd = new OracleDataAdapter();
            dd.SelectCommand = oCmd;
            dd.Fill(odr);
        }
        catch (OracleException ex)
        {

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
    public bool ExecuteNonQuery(string strBySql, string WriteDataBase)
    {
        bool bRet = false;
        OracleCommand oCmd = null;
        try
        {
            string strConnNormal = WriteDataBase;
            connNormal = new OracleConnection(strConnNormal);
            if (connNormal.State == ConnectionState.Closed)
            {
                connNormal.Open();
            }
            oCmd = new OracleCommand(strBySql, connNormal);
            int i = Convert.ToInt32(oCmd.ExecuteNonQuery());
            if (i > 0)
            {
                bRet = true;
            }
        }
        catch (OracleException ex)
        {

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
    public void CheckSAPClose(string ReadDB, string WriteDB)
    {
        string getUPDFlagSql = string.Empty;
        getUPDFlagSql = "SELECT INVOICE FROM PUBLIB.UPD_PODN_LIST_T where SAPFLAG IS NULL";
        try
        {
            DataSet ds = GetDataSet(getUPDFlagSql, ReadDB);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            int DNStatus = 2;
            SAPOperate sapo = new SAPOperate();
            foreach (DataRow dr in dt.Rows)
            {
                string checkDN = dr[0].ToString();
                DNStatus = sapo.CheckDNMovementStatus(checkDN);
                if (DNStatus == 0)
                {
                    string updateSAPFlagSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET SAPFLAG='Y'" +
                                              " WHERE INVOICE='" + checkDN + "'";
                    ExecuteNonQuery(updateSAPFlagSql, WriteDB);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void CheckSAPCloseByCustomer(string ReadDB, string WriteDB, string Usertype)
    {
        string getUPDFlagSql = string.Empty;
        getUPDFlagSql = "SELECT INVOICE FROM " + Usertype + ".UPD_PODN_LIST_T where SAPFLAG IS NULL OR SAPFLAG = ''";
        try
        {
            DataSet ds = GetDataSet(getUPDFlagSql, ReadDB);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            int DNStatus = 2;
            SAPOperate sapo = new SAPOperate();
            foreach (DataRow dr in dt.Rows)
            {
                string checkDN = dr[0].ToString();
                DNStatus = sapo.CheckDNMovementStatus(checkDN);
                if (DNStatus == 0)
                {
                    string updateSAPFlagSql = "UPDATE " + Usertype + ".UPD_PODN_LIST_T SET SAPFLAG='Y'" +
                                              " WHERE INVOICE='" + checkDN + "'";
                    ExecuteNonQuery(updateSAPFlagSql, WriteDB);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    public void CheckSAPCloseMess(string ReadDB, string WriteDB, string MessDB, string SCNT)
    {
        string t1 = FPubLibPointer.PubWri_MessLog("IDMAuto", SCNT, "", "", "", "SapClose", "Start", "", "", MessDB, "sql");         // 10 Code
        string getUPDFlagSql = string.Empty;
        getUPDFlagSql = "SELECT INVOICE FROM PUBLIB.UPD_PODN_LIST_T where SAPFLAG IS NULL";
        try
        {
            DataSet ds = GetDataSet(getUPDFlagSql, ReadDB);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            int DNStatus = 2;
            SAPOperate sapo = new SAPOperate();
            foreach (DataRow dr in dt.Rows)
            {
                string checkDN = dr[0].ToString();
                DNStatus = sapo.CheckDNMovementStatus(checkDN);
                t1 = FPubLibPointer.PubWri_MessLog("IDMAuto", SCNT, "", "", "", "SapClose", "DNStatus", checkDN, DNStatus.ToString(), MessDB, "sql"); 
                if (DNStatus == 0)
                {
                    string updateSAPFlagSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET SAPFLAG='Y'" +
                                              " WHERE INVOICE='" + checkDN + "'";
                    ExecuteNonQuery(updateSAPFlagSql, WriteDB);
                    t1 = FPubLibPointer.PubWri_MessLog("IDMAuto", SCNT, "", "", "", "SapClose", "UPOK", "", "", MessDB, "sql");        
      
                }
            }
        }
        catch (Exception ex)
        {
            t1 = FPubLibPointer.PubWri_MessLog("IDMAuto", SCNT, "", "", "", "SapClose", "Error", "", "", MessDB, "sql");               
        }

        t1 = FPubLibPointer.PubWri_MessLog("IDMAuto", SCNT, "", "", "", "SapClose", "Close", "", "", MessDB, "sql");         // 10 Code
      
    }  // Sap Close
}
