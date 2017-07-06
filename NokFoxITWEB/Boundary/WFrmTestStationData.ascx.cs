/*************************************************************************
 * 
 *  Unit description: Search the test station data for Pass/Fail
 *  Developer: Shu Jian Bo             Date: 2007/05/23
 *  Modifier : Shu Jian Bo             Date: 2007/05/24
 * 
 * ***********************************************************************/

namespace SFCQuery.Boundary
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
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
    using System.Text;
    using System.Web.UI;

    //using NPOI.HSSF.UserModel;
    //using NPOI.HPSF;
    //using NPOI.POIFS.FileSystem;
    //using NPOI.HSSF.Util;
    //using NPOI.SS.UserModel;
    /// <summary>
    ///		WFrmTestStationData 的摘要描述。
    /// </summary>
    public partial class WFrmTestStationData : System.Web.UI.UserControl
    {
        protected System.Web.UI.WebControls.LinkButton LinkButton1;
        protected System.Web.UI.WebControls.LinkButton LinkButton2;
        protected System.Web.UI.WebControls.LinkButton LinkButton3;
        protected System.Web.UI.WebControls.TextBox Textbox6;
        protected System.Web.UI.WebControls.Label Label10;
        protected System.Web.UI.WebControls.TextBox Textbox5;
        protected System.Web.UI.WebControls.Label Label9;
        protected System.Web.UI.WebControls.TextBox TextBox1;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.TextBox TextBox2;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.TextBox TextBox3;
        protected System.Web.UI.WebControls.Label Label3;
        //private ResourceManager rm = new ResourceManager("SFCQuery.MultiLanguage.SFCQuery",Assembly.GetExecutingAssembly());
        public string strSqlFailQuery = string.Empty;
        public static DataTable dtFailToExcel = null;
        //static HSSFWorkbook hssfworkbook;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在這裡放置使用者程式碼以初始化網頁
            if (!IsPostBack)
            {
                //btnDateFrom.Attributes["onclick"] = "return showCalendar('"+tbStartDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
                //btnDateTo.Attributes["onclick"] = "return showCalendar('"+tbEndDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
                tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
                tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                    tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
                InitiaPage();
                MultiLanguage();
            }
        }

        #region Web Form 設計工具產生的程式碼
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
        ///		這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgTestStationData.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgTestStationData_PageIndexChanged);

        }
        #endregion

        /// <summary>
        /// Load data from database to DropDownList contain Model and Station
        /// </summary>
        private void InitiaPage()
        {
            //string StrSql = "SELECT MODEL FROM CMCS_SFC_MODEL ORDER BY MODEL";
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            //ddlModel.DataTextField = "MODEL";
            //ddlModel.DataValueField = "MODEL";
            //ddlModel.DataSource = dt.DefaultView;
            //ddlModel.DataBind();

            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlModel.DataTextField = "MODEL";
            ddlModel.DataValueField = "MODEL";
            ddlModel.DataSource = dt.DefaultView;
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, "");

            //string StrSql = "SELECT T.STATION_ID,T.DESCRIPTION FROM MES_COMM_STATION_DESCRIPTION T WHERE TESTSTATION = 'Y' UNION SELECT 'FQC','FQC' FROM DUAL ORDER BY DESCRIPTION ";
            //dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            //ddlStation.DataTextField = "DESCRIPTION";
            //ddlStation.DataValueField = "STATION_ID";
            //ddlStation.DataSource = dt.DefaultView;
            //ddlStation.DataBind();

            //strProcedureName = "SFCQUERY.GETSTATION";
            //dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            //ddlStation.DataTextField = "DESCRIPTION";
            //ddlStation.DataValueField = "STATION_ID";
            //ddlStation.DataSource = dt.DefaultView;
            //ddlStation.DataBind();
        }

        /// <summary>
        /// Change the english and chinese
        /// </summary>
        private void MultiLanguage()
        {
            lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");//rm.GetString("Model");
            lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");//rm.GetString("Line");
            lblStation.Text = (String)GetGlobalResourceObject("SFCQuery", "StationID");//rm.GetString("StationID");
            //lblRepair.Text = rm.GetString("Repair");
            lblWO.Text = (String)GetGlobalResourceObject("SFCQuery", "WO");//rm.GetString("WO");
            //lblProductID.Text = rm.GetString("ProductID");
            lblPCName.Text = (String)GetGlobalResourceObject("SFCQuery", "PCName");//rm.GetString("PCName");
            lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");//rm.GetString("DateFrom");
            lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");//rm.GetString("DateTo");
            btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
            Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
            Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");

        }

        private string GetTestStationSql(string strModel, string strTableName, string strColumnName, string strStationID)
        {
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
            string strStartDate = tbStartDate.DateTextBox.Text.Trim();
            string strEndDate = tbEndDate.DateTextBox.Text.Trim();
            //Product ID
            if (!tbProductID.Text.Trim().Equals(""))
                strWhere = strWhere + " AND A." + strProductID + " = " + ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper());
            else if (!tbIMEI.Text.Trim().Equals(""))
            {
                if (tbIMEI.Text.Trim().Length == 15)
                    strWhere = strWhere + " AND A.PRODUCTID =(SELECT PRODUCTID FROM " + strModel + ".E2PCONFIG WHERE IMEI LIKE " + ClsCommon.GetSqlString(tbIMEI.Text.Trim().ToUpper() + "%")
                        + " AND E2PDATE =(SELECT MAX(E2PDATE) FROM " + strModel + ".E2PCONFIG WHERE IMEI LIKE " + ClsCommon.GetSqlString(tbIMEI.Text.Trim().ToUpper() + "%") + ") )";
                else
                    strWhere = strWhere + " AND A.PRODUCTID = (SELECT PRODUCTID FROM " + strModel + ".E2PCONFIG WHERE PICASSO LIKE " + ClsCommon.GetSqlString(tbIMEI.Text.Trim().ToUpper() + "%")
                        + " AND E2PDATE= (SELECT MAX(E2PDATE) FROM " + strModel + ".E2PCONFIG WHERE PICASSO LIKE " + ClsCommon.GetSqlString(tbIMEI.Text.Trim().ToUpper() + "%") + ") )";
            }

            //DataTime 范圍
            strWhere = strWhere + " AND " + strColumnName + " BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
                + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

            //LINE
            if (!ddlLine.SelectedValue.ToString().Equals(""))
                strWhere = strWhere + " AND UPPER(ONLINENAME) = " + ClsCommon.GetSqlString(ddlLine.SelectedValue.ToString().ToUpper());

            //PC NAME
            if (!tbPCName.Text.Trim().Equals(""))
                strWhere = strWhere + " AND UPPER(COMPUTERNAME) = " + ClsCommon.GetSqlString(tbPCName.Text.Trim().ToUpper());

            //WORK ORDER
            if (!tbWO.Text.Trim().Equals(""))
                strWhere = strWhere + " AND UPPER(WORKORDER) = " + ClsCommon.GetSqlString(tbWO.Text.Trim().ToUpper());

            //whether Repair
            switch (ddlRepair.SelectedIndex)
            {
                case 0:
                    //if ((!strStationID.Equals("FQC") && !strStationID.Equals("CFC") && !strStationID.Equals("CIT") && !strStationID.Equals("Pack") && !strStationID.Equals("CFC")
                    //    && !strStationID.Equals("Phasing") && !strStationID.Equals("PowerOn") && !strStationID.Equals("Proto") && !strStationID.Equals("CFC")
                    //    && !strStationID.Equals("Bluetooth") && !strStationID.Equals("Bluetest")) && (strModel.Equals("GNG") && !strStationID.Equals("E2")) &&
                    //    (strModel.Equals("SLG") && !strStationID.Equals("E2")) && (strModel.Equals("TWN") && !strStationID.Equals("E2")) &&
                    //    (strModel.Equals("MRO") && !strStationID.Equals("E2")) && (strModel.Equals("MRE") && !strStationID.Equals("E2")))
                    if ((!strStationID.Equals("FQC") && !strStationID.Equals("CFC") && !strStationID.Equals("CI") && !strStationID.Equals("Pack") && !strStationID.Equals("CFC")
                        && !strStationID.Equals("Phasing") && !strStationID.Equals("PowerOn") && !strStationID.Equals("Proto") && !strStationID.Equals("CFC")
                        && !strStationID.Equals("Bluetooth") && !strStationID.Equals("Bluetest")))
                        strWhere = strWhere + " AND REPAIR=0  and '" + strWorkOrder + "' not like 'R%'";
                    break;
                case 1:
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
                //case "GPS":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("FA6") && !strModel.Equals("FA7") && !strModel.Equals("AU3") && !strModel.Equals("AU4"))
                //    {
                //        strWhere = strWhere + " AND STATION='GPS'  ";
                //        strStaioncode = " AND STATION='GPS'";
                //        strStationName = "STATION";
                //    }
                //    else
                //    {
                //        strWhere = strWhere + " AND UPPER(STATION_CODE)='GPS'  ";
                //        strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                //        strStationName = "STATION_CODE";
                //    }
                //    break;
                case "GPS": 
                    strWhere = strWhere + " AND UPPER(STATION_CODE)='GPS'  ";
                    strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                    strStationName = "STATION_CODE"; 
                    break;

                //case "GPSWL":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("FA6") && !strModel.Equals("FA7") && !strModel.Equals("AU3") && !strModel.Equals("AU4") && !strModel.Equals("HER") && !strModel.Equals("CAS"))
                //    {
                //        strWhere = strWhere + " AND STATION='GPSWL'  ";
                //        strStaioncode = " AND STATION='GPSWL'";
                //        strStationName = "STATION";
                //    }
                //    else
                //    {
                //        strWhere = strWhere + " AND UPPER(STATION_CODE)='GPSWL'  ";
                //        strStaioncode = " AND STATION_CODE = '" + strStationID.Trim() + "'";
                //        strStationName = "STATION_CODE";
                //    }
                //    break;
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
                //case "CFC":
                //case "CIT":
                //case "Pack":
                //case "Phasing":
                //case "PowerOn":
                //case "Proto":
                //case "Bluetooth":
                //case "Bluetest":
                //    strWhere = strWhere + " AND PROCESS_CODE = "+ClsCommon.GetSqlString(strStationID);
                //    break;

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
                case "ALL":
                    strStaioncode = "";
                    strStationName = "";
                    break;
                default:
                    //if ((strModel.Equals("BZ3") && strStationID.Equals("GSMPT")) || (strModel.Equals("BZ3") && strStationID.Equals("GSMWL")) 
                    //    || (strModel.Equals("BLA") && strStationID.Equals("GSMPT")) || (strModel.Equals("BLA") && strStationID.Equals("GSMWL")) || (strModel.Equals("BZA") && strStationID.Equals("GSMPT")) || (strModel.Equals("BZA") && strStationID.Equals("GSMWL")) || strModel.Equals("ZUS") || strModel.Equals("RCX") || (strModel.Equals("GNG") && !strStationID.Equals("E2")) 
                    //    || strModel.Equals("DVR") || strModel.Equals("RUY") || (strModel.Equals("SLG") && !strStationID.Equals("E2")) || (strModel.Equals("TWN") && !strStationID.Equals("E2")) || strModel.Equals("HAIER") || (strModel.Equals("MRO") && !strStationID.Equals("E2")) || (strModel.Equals("MRE") && !strStationID.Equals("E2")) 
                    //    || (strModel.Equals("AU3") && !strStationID.Equals("E2P"))|| (strModel.Equals("AU4") && !strStationID.Equals("E2P"))|| (strModel.Equals("AU2") && !strStationID.Equals("E2P"))|| (strModel.Equals("FA0") && !strStationID.Equals("E2P"))|| (strModel.Equals("FA1") && !strStationID.Equals("E2P"))|| (strModel.Equals("FA3") && !strStationID.Equals("E2P"))
                    //    || (strModel.Equals("F02") && !strStationID.Equals("E2P"))|| (strModel.Equals("F03") && !strStationID.Equals("E2P"))|| (strModel.Equals("F05") && !strStationID.Equals("E2P")))
                        // strWhere = strWhere + " AND STATION_CODE = " + ClsCommon.GetSqlString(strStationID); +ClsCommon.GetSqlString(tbWO.Text.Trim().ToUpper());
                        strWhere = strWhere + " AND UPPER(STATION_CODE) = " + ClsCommon.GetSqlString(strStationID);
                        strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                        strStationName = "STATION_CODE";
                    break;


            }

            if ((strModel.Equals("GLM") && strTableName.Equals("PRETEST")) || (strModel.Equals("GLM") && strTableName.Equals("FQC_PRETEST")))
                strWhere = strWhere + " AND (UPPER(TESTNAME) NOT LIKE 'PRTINITUUT%' or testname is null )"; 
            string strOrder = " ASC";
            string strRn = string.Empty;
            switch (rblQueryType.SelectedIndex)
            {
                case 0:  //Total Data
                    if (strModel.Equals("GLM") && strTableName.Equals("PRETEST"))
                        strSql = "SELECT distinct  * FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + " AND UPPER(TESTNAME) NOT LIKE 'PRTINITUUT%' ORDER BY " + strColumnName;
                    else
                    {
                        if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && ddlRepair.SelectedIndex == 0)
                            strSql = "SELECT A.* FROM " + strModel + "." + strTableName + " A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD  WHERE " + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID ORDER BY A." + strColumnName;
                        else
                            //strSql = "SELECT distinct  * FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "  ORDER BY " + strColumnName;
                            strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,PROCESS_TIME,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "  ORDER BY " + strColumnName;

                    }
                    break;
                case 1:  //Final Data 

                    //if (strTableName.Equals("BASEBANDTEST"))
                    //    strWhere = strWhere + "AND " + strColumnName + "=(SELECT MAX(" + strColumnName + ") FROM " + strModel + "." + strTableName + " WHERE PRODUCTID=A.PRODUCTID AND   STATION=A.STATION " 
                    //        + strWhere + ")";
                    //else
                    //    strWhere = strWhere + "AND " + strColumnName + "=(SELECT MAX(" + strColumnName + ") FROM " + strModel + "." + strTableName + " WHERE " + strProductID + "=A." + strProductID 
                    //        + strWhere + ")";
                    //break;

                    //----------------2008/09/11 add by zhangjijing  
                    if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && ddlRepair.SelectedIndex == 0)
                        strSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                               + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1";
                    else
                        //strSql = "SELECT distinct * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE  "
                        //         + strWhere.Substring(4) + ") WHERE RN=1 ";
                        strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,PROCESS_TIME,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE  "
                                 + strWhere.Substring(4) + ") WHERE RN=1 ";
                    break;
                //----------------end 
                case 2:  //First Data
                    //if (strTableName.Equals("BASEBANDTEST")) 
                    //    strWhere = strWhere + " AND " + strColumnName + "=(SELECT MIN(" + strColumnName + ") FROM " + strModel + "." + strTableName + " WHERE PRODUCTID=A.PRODUCTID AND STATION=A.STATION  "
                    //        + strWhere +")";
                    //else 
                    //    strWhere = strWhere + "AND " + strColumnName + "=(SELECT MIN(" + strColumnName + ") FROM " + strModel + "." + strTableName + " WHERE " + strProductID + "=A." + strProductID
                    //        + strWhere + ")";
                    //break;

                    //-----------------2008/09/11 add by zhangjijing 

                    if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && ddlRepair.SelectedIndex == 0)
                        strSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE ASC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                               + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1";
                    else
                        //strSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                             //  + strWhere.Substring(4) + ") WHERE RN=1 ";
                        strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,PROCESS_TIME,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                               + strWhere.Substring(4) + ") WHERE RN=1 ";
                    break;
                //-----------------end 
                case 3:  //Retest Fail Data
                    if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && ddlRepair.SelectedIndex == 0)
                        strSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                               + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN>1 AND STATUS='F'";
                    else
                        //strSql = "SELECT * FROM ( SELECT * FROM (SELECT B.*,row_number() over(partition by " + strProductID + "," + strStationName + " order by " + strColumnName + " asc) rn from " + strModel + "." + strTableName
                        //       + " B WHERE " + strColumnName + " BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI:SS') AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI:SS') " + strStaioncode  + strWhere.ToString() + ") "
                        //       + " where rn>1 and UPPER(SUBSTR(STATUS, 1, 1)) = 'F')";
                        strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,PROCESS_TIME,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM ( SELECT * FROM (SELECT B.*,row_number() over(partition by " + strProductID + "," + strStationName + " order by " + strColumnName + " asc) rn from " + strModel + "." + strTableName
                               + " B WHERE " + strColumnName + " BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI:SS') AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI:SS') " + strStaioncode + strWhere.ToString() + ") "
                               + " where rn>1 and UPPER(SUBSTR(STATUS, 1, 1)) = 'F')";
                    break;
                case 4:  //All Fail Data
                    if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && ddlRepair.SelectedIndex == 0)
                        strSql = "SELECT A.* FROM " + strModel + "." + strTableName + " A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD  WHERE " + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID AND A.STATUS='F' ORDER BY A." + strColumnName;
                    else
                    {
                        strWhere = strWhere + " AND UPPER(SUBSTR(STATUS,1,1))='F'";
                        //strSql = "SELECT distinct  * FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "  ORDER BY " + strColumnName;
                        strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,PROCESS_TIME,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + "  ORDER BY " + strColumnName;

                    }
                    break;
                case 5:  //First Fail Data 
                case 6:  //Final Fail Information 
                    // Modfied by shujianbo at 2008/01/30
                    if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && ddlRepair.SelectedIndex == 0)
                    {
                        switch (rblQueryType.SelectedIndex)
                        {
                            case 5:
                                //strSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE ASC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                                //       + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1 AND STATUS='F' ";
                                strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE ASC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                                                   + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=1 AND STATUS='F' ";

                                break;
                            case 6:
                                //strSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                                //       + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=3 AND STATUS='F' ";
                                strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,PROCESS_TIME,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                                       + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN=3 AND STATUS='F' ";

                                break;
                        } 
                    }
                    else
                    {
                        switch (rblQueryType.SelectedIndex)
                        {
                            case 5:
                                strOrder = " ASC";
                                strRn = "RN=1";
                                break;
                            case 6:
                                strOrder = " ASC";
                                strRn = "RN=3";
                                break;
                        }

                        //strSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                        //    + strWhere.Substring(4) + ") WHERE "+strRn+"  AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F' ";
                        strSql = "SELECT distinct(PRODUCT_ID),STATION_CODE,PROCESS_TIME,STATUS,REPAIR,ERROR_MSG,FIXTUREID LINE_CODE,PDDATE,LINE_CODE FIXTUREID FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                            + strWhere.Substring(4) + ") WHERE " + strRn + "  AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F' ";

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

        public string GetData()
        {
            string strModel = ddlModel.SelectedValue.ToString();
            string strStationID = ddlStation.SelectedValue.ToString().ToUpper();
            string strTableName = "";
            string strColumnName = "";
            switch (strStationID)
            {
                //case "DL":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("AU3") && !strModel.Equals("AU4"))
                //    {
                //        strTableName = "DOWNLOAD";
                //        strColumnName = "DLDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break; 

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
                //case "WCAWPT":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("AU3") && !strModel.Equals("AU4"))
                //    {
                //        strTableName = "WCDMA_PRETEST";
                //        strColumnName = "PTDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
                case "B1":
                case "B2":
                case "B3":
                case "B4":
                    strTableName = "BASEBANDTEST";
                    strColumnName = "BBDATE";
                    break;
                //case "GPSWL":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("AU3") && !strModel.Equals("FA7") && !strModel.Equals("FA6") && !strModel.Equals("AU4") && !strModel.Equals("HER") && !strModel.Equals("CAS"))
                //    {
                //        strTableName = "BASEBANDTEST";
                //        strColumnName = "BBDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
                case "GPSWL": 
                    strTableName = "PRODUCT_HISTORY_V";
                    strColumnName = "PDDATE"; 
                    break;

                //case "BT":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("AU3") && !strModel.Equals("AU4"))
                //    {
                //        strTableName = "BLUETOOTH";
                //        strColumnName = "PDDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
                //case "BL":
                //    if (strModel.Equals("SPK"))
                //    {
                //        strTableName = "BLUETOOTH";
                //        strColumnName = "PDDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
                //case "BTWL":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("AU3") && !strModel.Equals("AU4"))
                //    {
                //        strTableName = "BTWIRELESS";
                //        strColumnName = "PDDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
                //case "WL":
                //    strTableName = "WIRELESS";
                //    strColumnName = "WLDATE";
                //    break;
                //case "RWL":
                //    strTableName = "RWIRELESS";
                //    strColumnName = "WLDATE";
                //    break;
                //case "D2":
                //    strTableName = "REDOWNLOAD";
                //    strColumnName = "DLDATE";
                //    break;
                //case "E2":
                //case "E2P":
                //    strTableName = "E2PCONFIG";
                //    strColumnName = "E2PDATE";
                //    break;
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
                //case "GPS":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("FA6") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("AU3") && !strModel.Equals("FA7") && !strModel.Equals("AU4"))
                //    {
                //        strTableName = "BASEBANDTEST";
                //        strColumnName = "BBDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
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
                //case "WCAWPT":
                //    if (strModel.Equals("SPK"))
                //    {
                //        strTableName = "WCDMA_PRETEST";
                //        strColumnName = "PTDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
                //case "WIFI":
                //    if (!strModel.Equals("ZUS") && !strModel.Equals("F02") && !strModel.Equals("F03") && !strModel.Equals("F05") && !strModel.Equals("FA0") && !strModel.Equals("FA1")
                //        && !strModel.Equals("FA3") && !strModel.Equals("AU2") && !strModel.Equals("AU3") && !strModel.Equals("AU4"))
                //    {
                //        strTableName = "WIFITEST";
                //        strColumnName = "PDDATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "PDDATE";
                //    }
                //    break;
                //case "LVCIT":
                //    if (strModel.Equals("SLG"))
                //    {
                //        strTableName = "SLG_LV_PATS_TH";
                //        strColumnName = "TEST_DATE";
                //    }
                //    else
                //    {
                //        strTableName = "PRODUCT_HISTORY_V";
                //        strColumnName = "TEST_DATE";

                //    }
                //    strStationID = "LV";
                //    break;

                //case "CFC":
                //case "CIT":
                //case "Pack":
                //case "Phasing":
                //case "PowerOn":
                //case "Proto":
                //case "Bluetooth":
                //case "Bluetest":
                //    strTableName = "PATS_TH";
                //    strColumnName = "TEST_DATE";
                //    break;

                case "SIMLOCK":
                    strTableName = "E2PCONFIG";
                    strColumnName = "E2PDATE";
                    break;
                default:
                    strTableName = "PRODUCT_HISTORY_V";
                    strColumnName = "PDDATE";
                    break;
            }
            string strSql = GetTestStationSql(strModel, strTableName, strColumnName, strStationID);
            if (strStationID.Equals("FQC") && !strModel.Equals("GLM"))
            {
                strSql = strSql.Replace(strModel + ".", " ");
                strSql = strSql.Replace("PRODUCTID", "PRODUCT_ID");
                strSql = strSql.Replace("STATUS", "STATE_ID");
            }

            return strSql;
        }

        protected void btnQuery_Click(object sender, System.EventArgs e)
        {
            dgTestStationData.CurrentPageIndex = 0;
            if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = true;
                Label29.Visible = false;
                return;
            }

            if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = false;
                Label29.Visible = true;
                return;
            }


            Label28.Visible = false;
            Label29.Visible = false;

            if (ddlModel.SelectedValue.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種！！');</script>");
                return;
            }
            try
            {
                dgTestStationData.Visible = true;
                gvList.Visible = false;

                Label4.Visible = true;
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];
 
                dgTestStationData.DataSource = dt.DefaultView;
                dgTestStationData.DataBind();
                
                Label4.Text = "Current Page:" + (dgTestStationData.CurrentPageIndex + 1).ToString() + "/" + dgTestStationData.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

                dt.Dispose();
            }
            catch
            {
                dgTestStationData.Visible = false;
                Label4.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在該工站的查詢數據！！');</script>");
                return;
            }
        }

        void dgTestStationData_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgTestStationData.CurrentPageIndex = e.NewPageIndex;

            DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];
            dgTestStationData.DataSource = dt.DefaultView;
            dgTestStationData.DataBind();
            Label4.Text = "Current Page:" + (dgTestStationData.CurrentPageIndex + 1).ToString() + "/" + dgTestStationData.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

            dt.Dispose();
        }

        protected void btnExportExcel_Click(object sender, System.EventArgs e)
        {
            //by litao add 2011/3/30 start
            if (!dgTestStationData.Visible)
            {
                return;
            }
            //by litao add 2011/3/30 end
            if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = true;
                Label29.Visible = false;
                return;
            }

            if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = false;
                Label29.Visible = true;
                return;
            }

            Label28.Visible = false;
            Label29.Visible = false;

            string strSql = string.Empty;
            //DataTable dt = GetData();
            //ExortToExcel(dt);
            //by litao add 2011/7/1 增加全部導出功能
            //string strStationID = ddlStation.SelectedValue.ToString().ToUpper();
            //if (strStationID == "ALL")
            //{
            //    string strModel = ddlModel.SelectedValue.ToString();
            //    try
            //    {
            //        //string strSql = "SELECT distinct case(STATION_CODE) when 'D2' then 'Re/DL' else station_code end station_code FROM " + strModel + ".PRODUCT_HISTORY_V order by station_code"; delete by wh in20100811  because 根據幾種獲取站點速度太慢，對數據庫壓力過大
            //        strSql = "SELECT distinct case(STATION_CODE) when 'D2' then 'Re/DL' else station_code end STATION_CODE FROM " + strModel + ".SFC_STATION_NAME order by station_code";
            //        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            //        if (dt.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                //strStationID = dt.Rows[i][0].ToString();
            //                ddlStation.SelectedIndex =  i + 1;
            //                strSql = GetData();
            //                ExortToExcel1(strSql);
            //            }
            //            return;
            //        }
            //    }
            //    catch (Exception en)
            //    {
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！原因為"+en+",請截圖給資訊!');</script>");
            //        return;
            //    }
            //}


            strSql = GetData();
            //try
            //{
                ExortToExcel1(strSql);
            //}
            //catch
            //{
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
                //return;
            //}
        }

        private void ExortToExcel1(string strSql)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
           
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";

            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            ExortToExcel(dt);
             
            
            
            //新建一Excel應用程式
            //Missing missing = Missing.Value;
            //Excel.ApplicationClass objExcel = null;
            //Excel.Workbooks objBooks = null;
            //Excel.Workbook objBook = null;
            //Excel.Worksheet objSheet = null;
            //try
            //{
            //    objExcel = new Excel.ApplicationClass();
            //    objExcel.Visible = false;
            //    objBooks = (Excel.Workbooks)objExcel.Workbooks;
            //    objBook = (Excel.Workbook)(objBooks.Add(missing));
            //    objSheet = (Excel.Worksheet)objBook.ActiveSheet;

            //   clsDBToExcel.ExportToExcel(objSheet, strSql);

            //    //關閉Excel
            //    objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
            //    objBook.Close(false, missing, missing);
            //    objBooks.Close();
            //    objExcel.Quit();
            //}
            //finally
            //{
            //    //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
            //    if (!objSheet.Equals(null))
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
            //    if (objBook != null)
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
            //    if (objBooks != null)
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
            //    if (objExcel != null)
            //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
            //    GC.Collect();
            //}
            ////保存或打開報表
            //Response.Clear();
            //Response.Charset = "GB2312"; 
            //Response.Buffer = true; 
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName); 
            //Response.ContentType = "application/ms-excel";
            ////Response.ContentType = "application/vnd.ms-excel";
            //this.EnableViewState = false; 
            //Response.WriteFile(ExportPath + strFileName);
            ////Response.Flush();
            ////File.Delete(ExportPath + strFileName);

            //Response.End();
        }

        private void ExortToExcel(DataTable dt)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //新建一Excel應用程式
            Missing miss = Missing.Value;

            Excel.ApplicationClass objExcel = null;
            Excel.Workbooks objBooks = null;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null;

            try
            {
                objExcel = new Excel.ApplicationClass();
                objExcel.Visible = false;
                objBooks = (Excel.Workbooks)objExcel.Workbooks;
                objBook = (Excel.Workbook)(objBooks.Add(miss));
                objSheet = (Excel.Worksheet)objBook.ActiveSheet;
                objSheet.Name = ddlModel.SelectedValue + "_" + ddlStation.SelectedValue + "_" + rblQueryType.SelectedValue;

                int intColumn = dt.Columns.Count;
                for (int i = 1; i <= intColumn - 1; i++)
                {
                    //SetRangeValue(objSheet,GetCellName(i,1),GetCellName(i,1),dt.Columns[i].ColumnName,true,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                    objSheet.Cells[1, i] = dt.Columns[i].ColumnName;
                }

                int RowID = 2;

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i <= intColumn - 1; i++)
                    {
                        //SetRangeValue(objSheet,GetCellName(i,RowID),GetCellName(i,RowID),row[i].ToString(),false,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                        objSheet.Cells[RowID, i] = row[i].ToString();
                    }

                    RowID++;
                }
                //

                objSheet.Columns.AutoFit();
                objSheet.Rows.AutoFit();

                //頁面設置
                try
                {
                    objSheet.PageSetup.LeftMargin = 20;
                    objSheet.PageSetup.RightMargin = 20;
                    objSheet.PageSetup.TopMargin = 35;
                    objSheet.PageSetup.BottomMargin = 15;
                    objSheet.PageSetup.HeaderMargin = 7;
                    objSheet.PageSetup.FooterMargin = 10;
                    objSheet.PageSetup.CenterHorizontally = true;
                    objSheet.PageSetup.CenterVertically = false;
                    objSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait;
                    objSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                    objSheet.PageSetup.Zoom = false;
                    objSheet.PageSetup.FitToPagesWide = 1;
                    objSheet.PageSetup.FitToPagesTall = false;
                }
                catch
                {
                    throw;
                }
                //關閉Excel
                objBook.SaveAs(ExportPath + strFileName, miss, miss, miss, miss, miss, Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
                objBook.Close(false, miss, miss);
                objBooks.Close();
                objExcel.Quit();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
            }
            catch
            {
                throw;
            }
            finally
            {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
                if (!objSheet.Equals(null))
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
                if (objBook != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
                if (objBooks != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
                if (objExcel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
                GC.Collect();
            }

            //保存或打開報表
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
            Response.Charset = "";
            this.EnableViewState = false;
            Response.WriteFile(ExportPath + strFileName);
            Response.End();
        }
        //protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string strModel = ddlModel.SelectedValue.Trim().ToUpper();

        //    string strSql = "SELECT DISTINCT STATION_CODE FROM  " + strModel + ".PRODUCT_HISTORY_V ";
        //    dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        //    ddlStation.DataTextField = "STATION_CODE";
        //    ddlStation.DataValueField = "STATION_CODE";
        //    ddlStation.DataSource = dt.DefaultView;
        //    ddlStation.DataBind();
        //}         
        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModel.SelectedValue.Equals(""))
            {
                ddlStation.Items.Clear();
                ddlStation.Items.Insert(0, "");
            }
            else
            {
                string strModel = ddlModel.SelectedValue.ToString();
                try
                {
                    //string strSql = "SELECT distinct case(STATION_CODE) when 'D2' then 'Re/DL' else station_code end station_code FROM " + strModel + ".PRODUCT_HISTORY_V order by station_code"; delete by wh in20100811  because 根據幾種獲取站點速度太慢，對數據庫壓力過大
                    string strSql = "SELECT distinct case(STATION_CODE) when 'D2' then 'Re/DL' else station_code end STATION_CODE FROM " + strModel + ".SFC_STATION_NAME order by station_code";
                    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                    ddlStation.DataTextField = "STATION_CODE";
                    ddlStation.DataValueField = "STATION_CODE";
                    ddlStation.DataSource = dt.DefaultView;
                    ddlStation.DataBind();
                    ddlStation.Items.Insert(0, "All");
                    
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請確認是否存在此幾種！！');</script>");
                    return;
                }
            }
        }

        //by litao add 2011/03/28 失败统计查询
        protected void btnFailQuery_Click(object sender, EventArgs e)
        {
            switch (rblQueryType.SelectedIndex)
            {
                case 0:  //Total Data                     
                case 1:  //Final Data                     
                case 2:  //First Data
                    return;
                    break;
                     
                case 3:  //Retest Fail Data                     
                case 4:  //All Fail Data                     
                case 5:  //First Fail Data 
                case 6:  //Final Fail Information                      
                    break;
            }

             
            //dgTestStationData.CurrentPageIndex = 0;
            if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = true;
                Label29.Visible = false;
                return;
            }

            if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = false;
                Label29.Visible = true;
                return;
            }


            Label28.Visible = false;
            Label29.Visible = false;

            if (ddlModel.SelectedValue.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種！！');</script>");
                return;
            }
            try
            {
                Label4.Visible = true;
                string strSql = GetData();
                switch (rblQueryType.SelectedIndex)
                {
                    case 3:  //Retest Fail Data
                        break;
                    case 4:  //All Fail Data
                        int index = strSql.IndexOf("ORDER BY");
                        strSql = strSql.Substring(0, index) + "ORDER BY ERROR_MSG";
                        break;
                    case 5:  //First Fail Data 
                        break;
                    case 6:  //Final Fail Information 
                        //this.dgTestStationData.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgTestStationData_ItemCommand);
                        break;
                }
 
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                txtSqlFail.Text = strSql.ToString();

                dgTestStationData.Visible = false;
                gvList.Visible = true;
                //////////////////////////by litao add 2011/3/25 start

                 
                DataTable dtFail = new DataTable();
                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "ERROR_MSG";
                dtFail.Columns.Add(dc1);
                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "FailQty";
                dtFail.Columns.Add(dc2);
                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "FailLoss%";
                dtFail.Columns.Add(dc3);

                int itotal = dt.Rows.Count;
                string strError = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strError.IndexOf(dt.Rows[i][5].ToString()) == -1)
                    {
                        if (strError!="")
                        {
                            strError += ",";
                        }
                        strError += dt.Rows[i][5].ToString();
                    }
                }

                string[] strError_msg = strError.Split(',');
                for (int j = 0; j < strError_msg.Length; j++)
                {
                    DataRow dr = dtFail.NewRow();
                    dr[0] = strError_msg[j].ToString();
                    int iTemp = 0;
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (dt.Rows[k][5].ToString() == dr[0].ToString())
                        {
                            iTemp++;
                        }
                    }
                    dr[1] = iTemp.ToString();
                    dr[2] = Convert.ToString(Convert.ToInt32(((double)iTemp / itotal) * 100))+"%";
                    dtFail.Rows.Add(dr);
                }
                /////////////////////////by litao add 2011/3/25 end
                //為導出到excel做準備 start
                if (dtFailToExcel != null)
                {
                    dtFailToExcel.Dispose();
                    dtFailToExcel = null;
                }
                dtFailToExcel = dtFail;
                //為導出到excel做準備 end

                gvList.DataSource = dtFail.DefaultView;
                gvList.DataBind();
                
                Label4.Text = "Current Page:" + (dgTestStationData.CurrentPageIndex + 1).ToString() + "/" + dgTestStationData.PageCount.ToString() + " Total:" + dtFail.Rows.Count.ToString();

                dt.Dispose();
                //dtFail.Dispose();
            }
            catch(Exception en)
            {
                gvList.Visible = false;
                Label4.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在該工站的查詢數據！！');</script>");
                return;
            }
        }
        //by litao add 2011/3/30  导出excel
        protected void btnFailToExcel_Click(object sender, EventArgs e)
        {
            if (!gvList.Visible)
            {
                return;
            }
            switch (rblQueryType.SelectedIndex)
            {
                case 0:  //Total Data

                case 1:  //Final Data 


                case 2:  //First Data
                    return;
                default:
                    break;

            }
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            gvList.RenderControl(hw);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment; filename=FailQuery.xls");
            response.Charset = "gb2312";
            response.Write(tw.ToString());
            response.End();
            //FailToExcel();
        }

        //by litao add 2011/3/30
        protected void FailData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#bec5e7'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f5f5'");
            }            
        }
         
        //by litao add 2011/3/30
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#bec5e7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f5f5'");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string NoticeCode = gvList.DataKeys[e.Row.RowIndex].Value.ToString();
               
                LinkButton lbtnTitle = (LinkButton)e.Row.FindControl("lbtnTitle");
                lbtnTitle.OnClientClick = "window.open('wfrmtesttationdatafailrecord.aspx?NoticeCode=" 
                    + NoticeCode//ERROR_MSG
                    + ";" + tbStartDate.DateTextBox.Text.Trim()//开始时间
                    + ";" + tbEndDate.DateTextBox.Text.Trim()//结束时间
                    + ";" + rblQueryType.SelectedIndex.ToString()//类别
                    + ";" + ddlStation.SelectedValue.ToString().ToUpper()//选择站别
                    + ";" + ddlModel.SelectedValue.ToString()//幾種
                    + ";" + ddlLine.SelectedValue.ToString()//線別
                    + ";" + ddlRepair.SelectedIndex.ToString()//Repair
                    //+ ";" + txtSqlFail.Text.ToString()
                    + "&op=view','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');";
            } 
        }
        //by litao add 2011/3/30
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            //ShowGrid();
        }

        //protected void FailToExcel()
        //{
        //    //ImportDetailDataToExcel();
        //    InitializeWorkbook();

        //    ImportDataToExcel();
            
        //    ExcelFileDownLoad(hssfworkbook, "FailQuery");

        //}
        //static void InitializeWorkbook()
        //{
        //    hssfworkbook = new HSSFWorkbook();

        //    //Create a entry of DocumentSummaryInformation
        //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        //    dsi.Company = "NPOI Team";
        //    hssfworkbook.DocumentSummaryInformation = dsi;

        //    //Create a entry of SummaryInformation
        //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        //    si.Subject = "NPOI SDK Apply";
        //    hssfworkbook.SummaryInformation = si;
        //}
        //protected void ImportDataToExcel()
        //{
        //    CellStyle hlink_style = hssfworkbook.CreateCellStyle();
        //    NPOI.SS.UserModel.Font hlink_font = hssfworkbook.CreateFont();
        //    //hlink_font.Underline = (byte)FontUnderlineType.SINGLE;
        //    hlink_font.Color = HSSFColor.BLUE.index;
        //    hlink_style.SetFont(hlink_font);
             
        //   // Cell cell;
        //    Sheet sheet = hssfworkbook.CreateSheet("TotalFail");

        //    if (dtFailToExcel!=null&&dtFailToExcel.Rows.Count > 0)
        //    {
        //        string strColumnsName = string.Empty;
        //        Row row = sheet.CreateRow(0);
        //        for (int i = 0; i < dtFailToExcel.Columns.Count; i++)
        //        {
        //            //cell = sheet.CreateRow(0).CreateCell(i);
        //            strColumnsName = dtFailToExcel.Columns[i].ColumnName;
        //            row.CreateCell(i).SetCellValue(strColumnsName.ToString());
        //            //cell.SetCellValue(strColumnsName);
        //        }

        //        for (int j = 0; j < dtFailToExcel.Rows.Count; j++)
        //        {
        //            row = sheet.CreateRow(j+1);
        //            for (int k = 0; k < dtFailToExcel.Columns.Count; k++)
        //            {
        //                //cell = sheet.CreateRow(j+1).CreateCell(k);
        //                //cell.SetCellValue(dtFailToExcel.Rows[j][k].ToString());
        //                row.CreateCell(k).SetCellValue(dtFailToExcel.Rows[j][k].ToString());
        //            }
        //        }
        //    }
             
            

            ////Create a target sheet and cell
            //Sheet sheet2 = hssfworkbook.CreateSheet("Target Sheet");
            //sheet2.CreateRow(0).CreateCell(0).SetCellValue("Target Cell");

            //cell = sheet.CreateRow(3).CreateCell(0);
            //cell.SetCellValue("Worksheet Link");
            //link = new HSSFHyperlink(HyperlinkType.DOCUMENT);
            //link.Address = ("'Target Sheet'!A1");
            //cell.Hyperlink = (link);
            //cell.CellStyle = (hlink_style);

        //}

        //protected void ImportDetailDataToExcel()
        //{
        //    string NoticeCode = string.Empty;
        //    string strsql = string.Empty;

        //    for (int i=0; i<gvList.Rows.Count;i++)
        //    {                 
        //        NoticeCode = gvList.Rows[i].Cells[0].Text.ToString();
                 
        //    }
                
                //LinkButton lbtnTitle = (LinkButton)e.Row.FindControl("lbtnTitle");
                //lbtnTitle.OnClientClick = "window.open('wfrmtesttationdatafailrecord.aspx?NoticeCode=" 
                //    + NoticeCode//ERROR_MSG
                //    + ";" + tbStartDate.DateTextBox.Text.Trim()//开始时间
                //    + ";" + tbEndDate.DateTextBox.Text.Trim()//结束时间
                //    + ";" + rblQueryType.SelectedIndex.ToString()//类别
                //    + ";" + ddlStation.SelectedValue.ToString().ToUpper()//选择站别
                //    + ";" + ddlModel.SelectedValue.ToString()//幾種
                //    + ";" + ddlLine.SelectedValue.ToString()//線別
                //    + ";" + ddlRepair.SelectedIndex.ToString()//Repair

        //}
        //public static void ExcelFileDownLoad(HSSFWorkbook hssfworkbook, string DownLoadName)
        //{
        //    if (hssfworkbook != null)
        //    {
        //        using (MemoryStream file = new MemoryStream())
        //        {
        //            hssfworkbook.Write(file);
        //            HttpContext.Current.Response.Clear();
        //            HttpContext.Current.Response.ClearHeaders();
        //            HttpContext.Current.Response.Buffer = false;
        //            HttpContext.Current.Response.AppendHeader("Content-Disposition",
        //                string.Format("attachment;filename={0}",
        //                HttpUtility.UrlEncode(DownLoadName + ".xls", System.Text.Encoding.UTF8)));
        //            HttpContext.Current.Response.AppendHeader("Content-Length", file.Length.ToString());
        //            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        //            HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
        //            HttpContext.Current.Response.Flush();
        //            HttpContext.Current.Response.End();
        //        }
        //    }
        //}
}
}
