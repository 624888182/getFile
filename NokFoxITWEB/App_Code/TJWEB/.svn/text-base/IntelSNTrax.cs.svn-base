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
using System.IO;
using System.Text;
using SCM.GSCMDKen;
/// <summary>
///IntelSNTrax 的摘要说明
/// </summary>
public class IntelSNTrax
{
    private string ErrorMsg;
    public IntelSNTrax()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public string GetError()
    {
        return ErrorMsg;
    }
    /*這個方法被用來產生SNtrax數據。
     * 方法正確執行，並且有SNtrax數據產生時，返回iRet = 2；
     * 方法正確執行，但沒有SNtrax數據產生時，返回iRet = 1；
     * 方法執行時出現異常，返回iRet =-1；
     */
    #region 產生SNtrax數據

    public int GenerateSNTraxDataA(string dataBaseIO, string dbType, string readDataBase, string writeDataBase, string excuteType, DateTime Date,string DN,bool EmptyFile,ref string SNTraxFileName,ref int ItemCount)
    {
        string FilePath = "D:\\SNTraxFile\\";
        int iRet = 0;
        DateTime CreateDate = Date;

        #region WriteMessageBegin
        try
        {
            string NetSqlDB4 = ConfigurationManager.AppSettings["Sql221String"];
            FPubLib FPubLibPointer = new FPubLib();
            string s1 = readDataBase;
            if (s1.Length > 100) s1 = s1.Substring(0, 100);
            string s2 = writeDataBase;
            if (s2.Length > 100) s2 = s2.Substring(0, 100);
            FPubLibPointer.PubWri_MessLog("SNTraxA", dataBaseIO + "/" + dbType + "/" + excuteType, s1, s2, SNTraxFileName + "/" + ItemCount.ToString() + "/" + iRet.ToString(), "SNTraxA", "SNTraxA begin", Date.ToString("yyyy-MM-dd HH:mm:ss"), DN + "/" + EmptyFile.ToString(), NetSqlDB4, "sql");
        }
        catch
        { }
        #endregion

        try
        {
            string generateDocumetID = CreateDate.ToString("yyyyMMddHHmm") + "-";
            string generateFileName = "b" + CreateDate.ToString("yyyyMMddHHmm") + ".";
            string findDocumentID = "SELECT MAX(substr(documentid,14)) FROM PUBLIB.SNTrax_Head" +
                                      " WHERE  SUBSTR (DOCUMENTID,1,8)='" + generateDocumetID.Substring(0, 8) + "'";
            DataTable dtDocumentID = DataBaseOperation.SelectSQLDT(dbType, writeDataBase, findDocumentID);
            if (string.IsNullOrEmpty(dtDocumentID.Rows[0][0].ToString()))
            {
                generateDocumetID += "001";
                generateFileName += "001";
            }
            else
            {
                string addValue = Convert.ToString(1000 + Convert.ToInt32(dtDocumentID.Rows[0][0].ToString().Trim()) + 1);
                addValue = addValue.Substring(1);
                generateDocumetID = CreateDate.ToString("yyyyMMddHHmm") + "-" + addValue;
                generateFileName = "b" + CreateDate.ToString("yyyyMMddHHmm") + '.' + addValue;
            }
            string selectModuel = "Select Model From PUBLIB.INTELMODEL ";
            DataTable dt = DataBaseOperation.SelectSQLDT("oracle", writeDataBase, selectModuel);
            string ModuleCon = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ModuleCon += "'"+dt.Rows[i][0].ToString()+"'";
                if (i != dt.Rows.Count - 1) ModuleCon += ",";
            }
            ModuleCon = "(" + ModuleCon + ")";
            string selectSnTraxDetailSql = "";
            if (DN.Trim() == "")
            {
                selectSnTraxDetailSql = "SELECT DISTINCT K.* FROM (SELECT 'NOCNF' Rec_Type,'A' TYP,'FIHTJ' Site,b.serial_num Sn," +
                                              " d.market_name  Fcst_prd_nm, a.porder   Work_Order," +
                                              " C.serial_numBER Cust_sn, TO_CHAR(C.TEST_TIME ,'MM/DD/YYYY' ) Bldate,'CN' Coo_flag," +
                                              " a.Batch_no,d.sa_no  Mat_id, d.phone_model  Version,b.imeinum IMEI," +
                                              " b.imeinum2 IMEI_2,s_uc Unlock_Code_1,'' Unlock_Code_2,'' Unlock_Code_3,'' Unlock_Code_4" +
                                               " from shp.cmcs_sfc_porder a," +
                                              "shp.cmcs_sfc_imeinum b," +
                                              "wcdma_tse.r_function_head_t c," +
                                             "shp.ros_tch_pn d" +
                                              " where a.porder =b.porder  and a.ppart=d.ppart  and b.product_id=c.serial_number" +
                    //" AND  TO_CHAR（b.CREATION_DATE,'yyyyMMdd') ='" + CreateDate.AddDays(-1).ToString("yyyyMMdd") + "' "+
                                             "  ) K," +
                                             " SHP.CMCS_SFC_SHIPPING_DATA  M," +
                                             " SAP.CMCS_SFC_PACKING_LINES_ALL  N" +
                                             " WHERE K.Work_Order=M.Work_ORDER AND K.IMEI= M.IMEI AND M .CARTON_NO IS NOT NULL" +
                                             " And TO_CHAR（N.CREATION_DATE,'yyyyMMdd') = '" + CreateDate.AddDays(-1).ToString("yyyyMMdd") + "' " +
                                             " And M.Model in " + ModuleCon +
                                             " AND  M.CARTON_NO=N.INTERNAL_CARTON AND N.INTERNAL_CARTON IS NOT NULL";
            }
            else
            {
                selectSnTraxDetailSql = "SELECT DISTINCT K.* FROM (SELECT 'NOCNF' Rec_Type,'A' TYP,'FIHTJ' Site,b.serial_num Sn," +
                                              " d.market_name  Fcst_prd_nm, a.porder   Work_Order," +
                                              " C.serial_numBER Cust_sn, TO_CHAR(C.TEST_TIME ,'MM/DD/YYYY' ) Bldate,'CN' Coo_flag," +
                                              " a.Batch_no,d.sa_no  Mat_id, d.phone_model  Version,b.imeinum IMEI," +
                                              " b.imeinum2 IMEI_2,s_uc Unlock_Code_1,'' Unlock_Code_2,'' Unlock_Code_3,'' Unlock_Code_4" +
                                               " from shp.cmcs_sfc_porder a," +
                                              "shp.cmcs_sfc_imeinum b," +
                                              "wcdma_tse.r_function_head_t c," +
                                             "shp.ros_tch_pn d" +
                                              " where a.porder =b.porder  and a.ppart=d.ppart  and b.product_id=c.serial_number" +
                                             "  ) K," +
                                             " SHP.CMCS_SFC_SHIPPING_DATA  M," +
                                             " SAP.CMCS_SFC_PACKING_LINES_ALL  N" +
                                             " WHERE K.Work_Order=M.Work_ORDER AND K.IMEI= M.IMEI AND M .CARTON_NO IS NOT NULL" +
                    //" And TO_CHAR（N.CREATION_DATE,'yyyyMMdd') = '" + CreateDate.AddDays(-1).ToString("yyyyMMdd") + "' " +
                                             " And M.Model in " + ModuleCon +
                                             " AND  M.CARTON_NO=N.INTERNAL_CARTON AND N.INTERNAL_CARTON IS NOT NULL" +
                                             " And N.INVOICE_NUMBER = (SELECT INVOICE_NUMBER From SAP.CMCS_SFC_PACKING_LINES_ALL Where TO_CHAR（N.CREATION_DATE,'yyyyMMdd') = '" + CreateDate.ToString("yyyyMMdd") + "' " +
                                             "                          And INVOICE_NUMBER = '" + DN.Trim() + "' And RowNum = 1) ";
                                            
            }
            DataTable dtSelectTraxDNInformation = DataBaseOperation.SelectSQLDT(dbType, readDataBase, selectSnTraxDetailSql);
            /*
            if (dtSelectTraxDNInformation.Rows.Count == 0)
            {
                iRet = 1;
                return iRet;
            }
             */
            int insertSuccessCount = 0;
            int insertFailCount = 0;
            for (int i = 0; i < dtSelectTraxDNInformation.Rows.Count; i++)
            {
                string rec_Type = dtSelectTraxDNInformation.Rows[i][0].ToString();
                string type = dtSelectTraxDNInformation.Rows[i][1].ToString();
                string site = dtSelectTraxDNInformation.Rows[i][2].ToString();
                string sn = dtSelectTraxDNInformation.Rows[i][3].ToString();
                string fcst_Prd__Nm = dtSelectTraxDNInformation.Rows[i][4].ToString();
                string work_Order = dtSelectTraxDNInformation.Rows[i][5].ToString();
                string cust_SN = dtSelectTraxDNInformation.Rows[i][6].ToString();
                //string bldate = dtSelectTraxDNInformation.Rows[i][7].ToString();
                string bldate = CreateDate.AddDays(-1).ToString("MM/dd/yyyy");
                string rcoo_flag = dtSelectTraxDNInformation.Rows[i][8].ToString();
                string batch_no = dtSelectTraxDNInformation.Rows[i][9].ToString();
                string mat_ID = dtSelectTraxDNInformation.Rows[i][10].ToString();
                string version = dtSelectTraxDNInformation.Rows[i][11].ToString();
                string imei = dtSelectTraxDNInformation.Rows[i][12].ToString();
                string imei2 = dtSelectTraxDNInformation.Rows[i][13].ToString();
                string unlock_code_1 = dtSelectTraxDNInformation.Rows[i][14].ToString();
                string unlock_code_2 = dtSelectTraxDNInformation.Rows[i][15].ToString();
                string unlock_code_3 = dtSelectTraxDNInformation.Rows[i][16].ToString();
                string unlock_code_4 = dtSelectTraxDNInformation.Rows[i][17].ToString();
                string insertSnTraxDetailSql = "INSERT INTO Publib.SNTrax_Detail VALUES('" + generateDocumetID + "','" + rec_Type + "','" + type + "','" + site + "','" + sn + "'," +
                                                                      "'" + fcst_Prd__Nm + "','" + work_Order + "','" + cust_SN + "','" + bldate + "','" + rcoo_flag + "'," +
                                                                      "'" + batch_no + "', '" + mat_ID + "','" + version + "', '" + imei + "','" + imei2 + "','" + unlock_code_1 + "'," +
                                                                      "'" + unlock_code_2 + "','" + unlock_code_3 + "','" + unlock_code_4 + "')";
                DataBaseOperation.ExecSQL(dbType, writeDataBase, insertSnTraxDetailSql);
                insertSuccessCount++;
            }
            if ((dtSelectTraxDNInformation.Rows.Count > 0) || ((EmptyFile == true) && (DN.Trim() == "")))
            {
                insertFailCount = dtSelectTraxDNInformation.Rows.Count - insertSuccessCount;
                string sFileName = FilePath + generateFileName;
                string insertSnTraxHeadSql = "INSERT INTO Publib.SNTrax_Head (DOCUMENTID,FILENAME,CREATEDATE," +
                                "CHECKFLAG,CHECKTIME,CONFFLAG,CONFTIME,SENDFLAG,SENDTIME,ITEM_COUNT,SUCCESS_COUNT,FAIL_COUNT,FILE_LOCATION,INVOICE_NUM)" +
                    " VALUES('" + generateDocumetID + "','" + generateFileName + "',SYSDATE,'N','','N','','N','','" + dtSelectTraxDNInformation.Rows.Count + "','" + insertSuccessCount + "','" + insertFailCount + "','" + sFileName + "','"+DN+"')";
                DataBaseOperation.ExecSQL(dbType, writeDataBase, insertSnTraxHeadSql);
                iRet = GenerateSNTraxFile(dataBaseIO, dbType, readDataBase, writeDataBase, excuteType, FilePath, generateDocumetID, sFileName);
                iRet = 2;
                ItemCount = insertSuccessCount;
                SNTraxFileName = generateFileName;
            }
            else
            {
                iRet = 1;
            }
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.ToString();
            iRet = -1;
        }

        #region WriteMessageEnd
        try
        {
            string NetSqlDB4 = ConfigurationManager.AppSettings["Sql221String"];
            FPubLib FPubLibPointer = new FPubLib();
            string s1 = readDataBase;
            if (s1.Length > 100) s1 = s1.Substring(0, 100);
            string s2 = writeDataBase;
            if (s2.Length > 100) s2 = s2.Substring(0, 100);
            FPubLibPointer.PubWri_MessLog("SNTraxA", dataBaseIO + "/" + dbType + "/" + excuteType, s1, s2, SNTraxFileName + "/" + ItemCount.ToString()+"/"+iRet.ToString(), "SNTraxA", "SNTraxA end", Date.ToString("yyyy-MM-dd HH:mm:ss"), DN + "/" + EmptyFile.ToString(), NetSqlDB4, "sql");
        }
        catch
        { }
        #endregion

        return iRet;
    
    }

    public int GenerateSNTraxData(string dataBaseIO, string dbType, string readDataBase, string writeDataBase, string excuteType)
    {
        DateTime dt = DateTime.Now;

        #region WriteMessageBegin
        try
        {
            string NetSqlDB4 = ConfigurationManager.AppSettings["Sql221String"];
            FPubLib FPubLibPointer = new FPubLib();
            string s1 = readDataBase;
            if (s1.Length > 100) s1 = s1.Substring(0, 100);
            string s2 = writeDataBase;
            if (s2.Length > 100) s2 = s2.Substring(0, 100);
            FPubLibPointer.PubWri_MessLog("IDMAuto", dataBaseIO + "/" + dbType + "/" + excuteType, s1, s2, "//", "SNTrax", "SNTrax begin", dt.ToString("yyyy-MM-dd HH:mm:ss"), "", NetSqlDB4, "sql");
        }
        catch
        {

        }
        #endregion

        int iRet = 3;
        if (excuteType.ToLower() == "auto")
        {
            if (DateTime.Now.Hour <= 2) return iRet;
        }

        string FileName = "";
        int ItemCount = 0;
        string sSql = "Select Count(*) From  PUBLIB.SNTrax_Head Where DocumentID like '" + dt.ToString("yyyyMMdd") + "%'";
        DataTable dtID = DataBaseOperation.SelectSQLDT(dbType, writeDataBase, sSql);
        if (dtID.Rows[0][0].ToString() == "0")
        {
            iRet = GenerateSNTraxDataA(dataBaseIO, dbType, readDataBase, writeDataBase, excuteType, dt,"",true,ref FileName,ref ItemCount);
        }
        #region WriteMessageEnd
        try
        {
            string NetSqlDB4 = ConfigurationManager.AppSettings["Sql221String"];
            FPubLib FPubLibPointer = new FPubLib();
            string s1 = readDataBase;
            if (s1.Length > 100) s1 = s1.Substring(0, 100);
            string s2 = writeDataBase;
            if (s2.Length > 100) s2 = s2.Substring(0, 100);
            FPubLibPointer.PubWri_MessLog("IDMAuto", dataBaseIO + "/" + dbType + "/" + excuteType, s1, s2, FileName + "/" + ItemCount.ToString() + "/" + iRet.ToString(), "SNTrax", "SNTrax End", dt.ToString("yyyy-MM-dd HH:mm:ss"), "", NetSqlDB4, "sql");
        }
        catch
        {

        }
        #endregion

        return iRet;
    }
    #endregion
    /*這個方法被用來產生SNtrax文檔。
    * 方法正確執行，並且抓取到SNtrax數據時，返回iRet = 2；
    * 方法正確執行，但沒有抓取到SNtrax數據時，返回iRet = 1；
    * 方法執行時出現異常，返回iRet =-1；
    */
    #region 產生SNtrax文檔
    private int GenerateSNTraxFile(string dataBaseIO, string dbType, string readDataBase, string writeDataBase, string excuteType, string FilePath, string FileDocumentID,string FileName)
    {
        int iRet = 0;
        try
        {
            string getTraxDNInformation = "SELECT '' FILENAME,REC_TYPE,TYPE,SITE,SN,FCST_PRD_NM,WORK_ORDER,CUST_SN,BLDATE,COO_FLAG,BATCH_NO,MAT_ID," +
                                                   "VERSION,IMEI,IMEI_2,UNLOCK_CODE_1,UNLOCK_CODE_2,UNLOCK_CODE_3,UNLOCK_CODE_4" +
                                                    " FROM   PUBLIB.SNTrax_Detail A " +
                                                    " WHERE DOCUMENTID='" + FileDocumentID + "' ";
            DataTable dtTraxDNInformation = DataBaseOperation.SelectSQLDT(dbType, writeDataBase, getTraxDNInformation);
            /*
            if (dtTraxDNInformation.Rows.Count == 0)
            {
                iRet = 1;
                return iRet;
            }
             */
            StringBuilder sb = new StringBuilder();
            sb.Append("HDERV8");
            for (int i = 0; i < dtTraxDNInformation.Rows.Count; i++)
            {
                sb.AppendLine();
                sb.Append("~");
                for (int j = 1; j < dtTraxDNInformation.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dtTraxDNInformation.Rows[i][j].ToString()))
                    {
                        sb.Append(dtTraxDNInformation.Rows[i][j].ToString() + "~");
                    }
                    else
                    {
                        sb.Append("~");
                    }
                }
                //sb.Append("~");
            }
            sb.AppendLine();
            sb.Append("TRLR");

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string FilePathName = FileName;
            using (StreamWriter sw = new StreamWriter(FilePathName))
            {
                sw.Write(sb.ToString());
                sw.Close();
                iRet = 2;
            }
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.ToString();
            iRet = -1;
        }
        return iRet;
    }
    #endregion

    #region DELETESNtraxFILE

    public int DeleteSNTraxData(string dataBaseIO, string dbType, string readDataBase, string writeDataBase, string excuteType,string DocID)
    {
        int iRet = 0;
        string sFileName = "";
        DataBaseOperation dbo = new DataBaseOperation("oracle",writeDataBase );
        string sSql = "";
        try
        {
            sSql = "Select FILE_LOCATION FrOM PUBLIB.SNTRAX_HEAD Where DOCUMENTID = :V_ID";
            DataTable dt = dbo.SelectSQLDT(sSql, new string[] { "V_ID" }, new object[] { DocID });
            if (dt.Rows.Count > 0)
            {
                sFileName = dt.Rows[0][0].ToString().Trim();
            }

            sSql = "Delete From PUBLIB.SNTRAX_DETAIL WHERE DOCUMENTID = :V_ID";
            dbo.ExecSQL(sSql, new string[] { "V_ID" }, new object[] { DocID });

            sSql = "Delete From PUBLIB.SNTRAX_HEAD WHERE DOCUMENTID = :V_ID";
            dbo.ExecSQL(sSql, new string[] { "V_ID" }, new object[] { DocID });

            if (sFileName != "") File.Delete(sFileName);
            iRet = 0;
        }
        catch (Exception e)
        {
            ErrorMsg = e.Message;
            iRet = -1;
        }

        return iRet;
    }

    #endregion

}
