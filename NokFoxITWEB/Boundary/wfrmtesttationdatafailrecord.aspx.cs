using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using DB.EAI;
using System.Data.OracleClient;
using System.IO;
//using SFCQuery.Boundary;
public partial class Boundary_wfrmtesttationdatafailrecord : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["NoticeCode"] = Request.QueryString["NoticeCode"].ToString().Trim();
         
        string strNoticeCode = Session["NoticeCode"].ToString();
        Show_Data(strNoticeCode);
    }

    protected void Show_Data(string sql)
    {

        if (sql == "")
        {
            return;
        }
        string[] strDateAndFail = sql.Split(';');///0:ERROR_MSG;1:?始??;2:?束??;3:??;4：??站?;5:幾種;6:線別;7:Repair
        string strSql = GetData(sql);
        
        
        dgTestStationData.Visible = true;
        Label4.Visible = true;
        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql.ToString());
        DataTable dt = null;
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }
         
        dgTestStationData.DataSource = dt.DefaultView;
        dgTestStationData.DataBind();
         
        Label4.Text = "Current Page:" + (dgTestStationData.CurrentPageIndex + 1).ToString() + "/" + dgTestStationData.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }


    public string GetData(string sql)
    {
        if (sql == "")
        {
            return "";
        }
        string[] strDateAndFail = sql.Split(';');//0:ERROR_MSG;1:?始??;2:?束??;3:??;4：??站?;5:幾種
        string strModel = strDateAndFail[5];
        string strStationID = strDateAndFail[4];
        string strTableName = "";
        string strColumnName = "";
        switch (strStationID)
        {
            

            case "RE/DL":
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;
            case "CA_EDGE":
            case "CA_SET 1":
                strTableName = "CALIBRATION";
                strColumnName = "CALDATE";
                break;
            case "PT_EDGE":
            case "PT_SET 1":
                strTableName = "PRETEST";
                strColumnName = "PTDATE";
                break;
            case "EDGEPT":
                strTableName = "EDGE_PRETEST";
                strColumnName = "PTDATE";
                break;
             
            case "B1":
            case "B2":
            case "B3":
            case "B4":
                strTableName = "BASEBANDTEST";
                strColumnName = "BBDATE";
                break;
            
            case "GPSWL":
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;
 
            case "A1":
            case "A2":
            case "A3":
            case "A4":
            case "A5":
                strTableName = "BASEBANDTEST";
                strColumnName = "BBDATE";
                break;
            case "OOB":
                if (strModel.Equals("GLM"))
                {
                    strTableName = "BASEBANDTEST";
                    strColumnName = "BBDATE";
                }
                else
                {
                    strTableName = "MES_ASSY_HISTORY";
                    strColumnName = "CREATION_DATE";
                }
                break;
            case "FQC":
                if (strModel.Equals("GLM"))
                {
                    strTableName = "BASEBANDTEST";
                    strColumnName = "BBDATE";
                }
                else
                {
                    strTableName = "MES_PACK_OOB";
                    strColumnName = "CREATION_DATE";
                }
                break;
            case "GP":
             
            case "GPS":
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;

            case "BlnkFlsh":
                if (strModel.Equals("GNG"))
                {
                    strTableName = "GNG_POWERON_PATS_TH";
                    strColumnName = "TEST_DATE";
                }

                break;
             
            case "SIMLOCK":
                strTableName = "E2PCONFIG";
                strColumnName = "E2PDATE";
                break;
            default:
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;
        }
        string strSql = GetTestStationSql(strTableName,strColumnName, sql);
        if (strStationID.Equals("FQC") && !strModel.Equals("GLM"))
        {
            strSql = strSql.Replace(strModel + ".", " ");
            strSql = strSql.Replace("PRODUCTID", "PRODUCT_ID");
            strSql = strSql.Replace("STATUS", "STATE_ID");
        }

        return strSql;
    }

    private string GetTestStationSql(string strTableName,string strColumnName,string sql)
    {
         if (sql == "")
        {
            return "";
        }
        string[] strDateAndFail = sql.Split(';');//0:ERROR_MSG;1:?始??;2:?束??;3:??;4：??站?;5:幾種;6:線別;7:Repair
        string strModel = strDateAndFail[5];
         
        string strStationID = strDateAndFail[4];
        string strStartDate = strDateAndFail[1];
        string strEndDate = strDateAndFail[2];
        string strLine = strDateAndFail[6];
        string strRepair = strDateAndFail[7];
        string strRadioType = strDateAndFail[3];
        string strProductID = "PRODUCTID";
        string strWorkOrder = "WORKORDER";
        if (strTableName.Equals("PRODUCT_HISTORY_V"))
        {
            strProductID = "PRODUCT_ID";
            strWorkOrder = "WORK_ORDER";
        }
        string strSql = "";
        string strWhere = "";
        string strStaioncode = "";
        string strStationName = "";
        
        //DataTime 范圍
        strWhere = strWhere + " AND " + strColumnName + " BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

        //LINE
        if (!strLine.ToString().Equals(""))
            strWhere = strWhere + " AND UPPER(ONLINENAME) = " + ClsCommon.GetSqlString(strLine.ToString().ToUpper());

        
        //whether Repair
        switch (strRepair)
        {
            case "0":
                   if ((!strStationID.Equals("FQC") && !strStationID.Equals("CFC") && !strStationID.Equals("CI") && !strStationID.Equals("Pack") && !strStationID.Equals("CFC")
                    && !strStationID.Equals("Phasing") && !strStationID.Equals("PowerOn") && !strStationID.Equals("Proto") && !strStationID.Equals("CFC")
                    && !strStationID.Equals("Bluetooth") && !strStationID.Equals("Bluetest")))
                    strWhere = strWhere + " AND REPAIR=0  and '" + strWorkOrder + "' not like 'R%'";
                break;
            case "1":
                //if (strModel.Equals("DVR") || strModel.Equals("GNG") || strModel.Equals("MRE") || strModel.Equals("MRO") || strModel.Equals("RCD") || strModel.Equals("RCX") || strModel.Equals("RUY") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("WLO"))
                //strWhere = strWhere + " AND REPAIR=3 ";
                break;
        }

        switch (strStationID)
        {

            case "B1":
                strWhere = strWhere + " AND STATION='B1'  ";
                strStaioncode = " AND STATION='B1'";
                strStationName = "STATION";
                break;
            case "B2":
                strWhere = strWhere + " AND STATION='B2'  ";
                strStaioncode = " AND STATION='B2'";
                strStationName = "STATION";
                break;
            case "B3":
                strWhere = strWhere + " AND STATION='B3'  ";
                strStaioncode = " AND STATION='B3'";
                strStationName = "STATION";
                break;
            case "B4":
                strWhere = strWhere + " AND STATION='B4'  ";
                strStaioncode = " AND STATION='B4'";
                strStationName = "STATION";
                break;
            case "A1":
                strWhere = strWhere + " AND STATION='A1'  ";
                strStaioncode = " AND STATION='A1'";
                strStationName = "STATION";
                break;
            case "A2":
                strWhere = strWhere + " AND STATION='A2'  ";
                strStaioncode = " AND STATION='A2'";
                strStationName = "STATION";
                break;
            case "A3":
                strWhere = strWhere + " AND STATION='A3'  ";
                strStaioncode = " AND STATION='A3'";
                strStationName = "STATION";
                break;
            case "A4":
                strWhere = strWhere + " AND STATION='A4'  ";
                strStaioncode = " AND STATION='A4'";
                strStationName = "STATION";
                break;
            case "A5":
                strWhere = strWhere + " AND STATION='A5'  ";
                strStaioncode = " AND STATION='A5'";
                strStationName = "STATION";
                break;
            case "GP":
                strWhere = strWhere + " AND STATION='GP'  ";
                strStaioncode = " AND STATION='GP'";
                strStationName = "STATION";
                break;
             
            case "GPS":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPS'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;

            
            case "GPSWL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPSWL'  ";
                strStaioncode = " AND STATION_CODE = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "OOB":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION='OOB'  ";
                    strStaioncode = " AND STATION='OOB'";
                    strStationName = "STATION";
                }
                break;
            case "CA_EDGE":
            case "PT_EDGE":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'EDGE' ";
                strStationName = "STATION";
                break;
            case "CA_SET 1":
            case "PT_SET 1":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'SET 1' ";
                strStationName = "STATION";

                break;
            case "FQC":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION = 'FQC' ";
                    strStaioncode = " AND STATION='FQC'";
                    strStationName = "STATION";
                }
                else
                    strWhere = strSql + " AND STATION_ID LIKE 'A_FO' ";
                break;
            case "LV":

                strWhere = strWhere + " AND UPPER(STATION_CODE) LIKE '" + strStationID.Trim() + "%'";

                strStaioncode = " AND STATION_CODE='LV'";
                strStationName = "STATION_CODE";
                break;
            case "GSMWL":
            case "FQA":
            case "QA":
                strWhere = strWhere + " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "SIMLOCK":
                strStationName = "STATION_CODE";
                break;
            case "RE/DL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='D2'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            default:
                 strWhere = strWhere + " AND UPPER(STATION_CODE) = " + ClsCommon.GetSqlString(strStationID);
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;


        }

        if ((strModel.Equals("GLM") && strTableName.Equals("PRETEST")) || (strModel.Equals("GLM") && strTableName.Equals("FQC_PRETEST")))
            strWhere = strWhere + " AND (UPPER(TESTNAME) NOT LIKE 'PRTINITUUT%' or testname is null )";
        string strOrder = " DESC";
        switch (strRadioType)
        {
            case "0":  //Total Data
                if (strModel.Equals("GLM") && strTableName.Equals("PRETEST"))
                    strSql = "SELECT distinct  * FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + " AND UPPER(TESTNAME) NOT LIKE 'PRTINITUUT%' ORDER BY " + strColumnName;
                else
                {
                    if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                        strSql = "SELECT A.* FROM " + strModel + "." + strTableName + " A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD  WHERE " + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID ORDER BY A." + strColumnName;
                    else
                        strSql = "SELECT distinct  * FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "  ORDER BY " + strColumnName;
                        //strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID  FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "  ORDER BY " + strColumnName;

                }
                break;
            case "1":  //Final Data 
 
                //----------------2008/09/11 add by zhangjijing  
                if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                    strSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                           + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1";
                else
                    strSql = "SELECT distinct * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE  "
                             + strWhere.Substring(4) + ") WHERE RN=1 ";
                    //strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE  "
                       //      + strWhere.Substring(4) + ") WHERE RN=1 ";
                break;
            //----------------end distinct(PRODUCT_ID),STATION_CODE,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID 
            case "2":  //First Data
                 
                //-----------------2008/09/11 add by zhangjijing 

                if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                    strSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE ASC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                           + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1";
                else
                    strSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                           + strWhere.Substring(4) + ") WHERE RN=1 ";
                    //strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                          // + strWhere.Substring(4) + ") WHERE RN=1 ";
                break;
            //-----------------end 
            case "3":  //Retest Fail Data
                if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                    strSql = "SELECT PRODUCT_ID,STATION_CODE,ERROR_MSG,PDDATE,FIXTUREID FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                           + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN>1 AND STATUS='F'";
                else

                    strSql = "SELECT PRODUCT_ID,STATION_CODE,ERROR_MSG,PDDATE,LINE_CODE FIXTUREID FROM ( SELECT * FROM (SELECT B.*,row_number() over(partition by " + strProductID + "," + strStationName + " order by " + strColumnName + " asc) rn from " + strModel + "." + strTableName
                           + " B WHERE " + strColumnName + " BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI:SS') AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI:SS') " + strStaioncode + strWhere.ToString() + ") "
                           + " where rn>1 and UPPER(SUBSTR(STATUS, 1, 1)) = 'F' AND ERROR_MSG='" 
                    + strDateAndFail[0] + "')";

                break;
            case "4":  //All Fail Data
                if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                    strSql = "SELECT A.* FROM " + strModel + "." + strTableName + " A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD  WHERE " + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID AND A.STATUS='F' ORDER BY A." + strColumnName;
                else
                {
                    strWhere = strWhere + " AND UPPER(SUBSTR(STATUS,1,1))='F'";
                    strSql = "SELECT distinct  PRODUCT_ID,STATION_CODE,ERROR_MSG,PDDATE,LINE_CODE FIXTUREID FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "AND ERROR_MSG='" 
                    + strDateAndFail[0] + "'  ORDER BY " + strColumnName;
                }
                break;
            case "5":  //First Fail Data 
            case "6":  //Final Fail Information 
                // Modfied by shujianbo at 2008/01/30
                if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                {
                    switch (strRadioType)
                    {
                        case "5":
                            strSql = "SELECT PRODUCT_ID,STATION_CODE,ERROR_MSG,PDDATE,LINE_CODE FIXTUREID FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE ASC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                                   + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1 AND STATUS='F' AND ERROR_MSG='"
                    + strDateAndFail[0] + "')";

                            break;
                        case "6":
                            strSql = "SELECT PRODUCT_ID,STATION_CODE,ERROR_MSG,PDDATE,LINE_CODE FIXTUREID FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                                   + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1 AND STATUS='F' AND ERROR_MSG='"
                    + strDateAndFail[0] + "')";
                            break;
                    }
                }
                else
                {
                    switch (strRadioType)
                    {
                        case "5":
                            strOrder = " ASC";
                            break;
                        case "6":
                            strOrder = " DESC";
                            break;
                    }

                    strSql = "SELECT distinct  PRODUCT_ID,STATION_CODE,ERROR_MSG,PDDATE,LINE_CODE FIXTUREID FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                        + strWhere.Substring(4) + ") WHERE RN=1  AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F' AND ERROR_MSG='" 
                    + strDateAndFail[0] + "'";
                    if (!strModel.Equals("RCX") && !strModel.Equals("GNG") && !strModel.Equals("DVR") && !strModel.Equals("SLG") && !strModel.Equals("TWN") && !strModel.Equals("DVL") && !strModel.Equals("HAIER") && !strModel.Equals("MRO") && !strModel.Equals("RUY") && !strModel.Equals("HER") && !strModel.Equals("CAS"))
                    {
                        strSql = strSql + " AND  '" + strWorkOrder + "'  NOT LIKE 'R%' ";
                    }
                }
                break;
        }

        //if ((!rblQueryType.SelectedIndex.Equals(3)) && (!rblQueryType.SelectedIndex.Equals(6)) && (!rblQueryType.SelectedIndex.Equals(5)))
        //    strSql = "SELECT  * FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "  ORDER BY " + strColumnName;

        return strSql;
    }
    //by litao add 2011/3/30
    protected void Data_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#bec5e7'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f5f5'");
        }


    }
         

}
