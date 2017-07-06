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
    using System.Data.OracleClient;
    using System.Configuration;

    /// <summary>
    ///		WFrmEfficiencyQuery 的摘要描述。
    /// </summary>
    public partial class WFrmEfficiencyAssyQuery : System.Web.UI.UserControl
    {
        protected System.Web.UI.HtmlControls.HtmlTable TABLE1;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在這裡放置使用者程式碼以初始化網頁
            if (!IsPostBack)
            {
                //btnDateFrom.Attributes["onclick"] = "return showCalendar('"+tbStartDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
                //btnDateTo.Attributes["onclick"] = "return showCalendar('"+tbEndDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
                MultiLanguage();
                BindModel();
                tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
                tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                    tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
                ddlType.Visible = true;
                tbType.Visible = false;
            }
            ddlRegion.SelectedIndex = 1;
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

        }
        #endregion

        private void MultiLanguage()
        {
            lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
            lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");
            lblRegion.Text = (String)GetGlobalResourceObject("SFCQuery", "Region");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
            lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
            ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
            ViewState["WONotExist"] = (String)GetGlobalResourceObject("SFCQuery", "WONotExist");
        }

        private void BindModel()
        {
            //string StrSql = "SELECT DISTINCT MODEL FROM CVT.MES_DUMP_MODEL";
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            //ddlType.DataTextField = "Model";
            //ddlType.DataValueField = "Model";
            //ddlType.DataSource = dt.DefaultView;
            //ddlType.DataBind();

            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlType.DataTextField = "MODEL";
            ddlType.DataValueField = "MODEL";
            ddlType.DataSource = dt.DefaultView;
            ddlType.DataBind();
        }

        //protected void rblType_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    switch (rblType.SelectedIndex)
        //    {
        //        case 0:
        //            ddlType.Visible = true;
        //            tbType.Visible = false;
        //            break;
        //        case 1:
        //            ddlType.Visible = false;
        //            tbType.Visible = true;
        //            break;
        //    }
        //}

        private string GetPN(string WO_NO)
        {
            string StrSql = "";
            switch (ddlRegion.SelectedIndex)
            {
                case 0:
                    StrSql = "SELECT SPART FROM CMCS_SFC_SORDER WHERE SORDER =" + ClsCommon.GetSqlString(WO_NO);
                    break;
                case 1:
                    StrSql = "SELECT APART FROM CMCS_SFC_AORDER WHERE AORDER =" + ClsCommon.GetSqlString(WO_NO);
                    break;
                case 2:
                    StrSql = "SELECT PPART FROM CMCS_SFC_PORDER WHERE PORDER =" + ClsCommon.GetSqlString(WO_NO);
                    break;
            }
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }


        private string QueryByWO(string WONO, string DateStart, string DateEnd)
        {
            string StrSql;
            string rawdata;
            string PN = "";
            //PN = GetPN(WONO);

            string strRegion = "";
            string strHistoryTable = "";
            string strTestData = "";

            PN = GetPN(WONO);
            if (PN.Equals(""))
                return "";

            switch (ddlRegion.SelectedIndex)
            {
                case 0:
                    strRegion = "'T','S'";
                    strHistoryTable = "MES_PCBA_HISTORY";
                    strTestData = "UNION (SELECT DISTINCT LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY FROM (SELECT DISTINCT PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                        + " DECODE(STATION_CODE,'DL','T_BT','CA','T_ET','BT','T_DT','BTWL','T_MT','PT','T_JT','B1','T_CT','B2','T_NT') STATION_ID,"
                        + " STATUS STATE_ID FROM " + PN.Substring(2, 3) + ".PRODUCT_HISTORY_V T WHERE   STATION_CODE IN ('DL', 'CA', 'BT', 'BTWL', 'B1', 'B2', 'PT') ";
                    break;
                case 1:
                    strRegion = "'A'";
                    strHistoryTable = "MES_ASSY_HISTORY";
                    strTestData = "UNION (SELECT DISTINCT LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY FROM (SELECT DISTINCT PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                        + " DECODE(STATION_CODE,'WL','A_DT','D2','T_LT','A1','A_KT','A2','A_CT','A3','A_HT','A4','A_IT','A5','A_JT') STATION_ID,"
                        + " STATUS STATE_ID FROM " + PN.Substring(2, 3) + ".PRODUCT_HISTORY_V T WHERE   STATION_CODE IN ('WL', 'D2', 'A1', 'A2', 'A3', 'A4', 'A5') ";
                    break;
                case 2:
                    strRegion = "'P'";
                    strHistoryTable = "MES_PACK_HISTORY";
                    strTestData = "UNION (SELECT DISTINCT LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY FROM (SELECT DISTINCT PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                        + " DECODE(STATION_CODE,'E2','P_BT') STATION_ID,"
                        + " STATUS STATE_ID FROM " + PN.Substring(2, 3) + ".PRODUCT_HISTORY_V T WHERE   STATION_CODE IN ('E2') ";
                    break;
            }

            rawdata = " SELECT 'LINE' ||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1)) - 64) LINE_ID, SUBSTR(STATION_ID,1,1)||'_'||SUBSTR(STATION_ID,3,2) STATION_ID, STATE_ID "
                + " FROM " + strHistoryTable + " a "
                + " WHERE CREATION_DATE BETWEEN TO_DATE('" + DateStart + "','yyyy/mm/dd hh24:mi') AND TO_DATE('" + DateEnd + "','yyyy/mm/dd hh24:mi') "
                + " AND WO_NO=" + ClsCommon.GetSqlString(WONO)
                + " AND SEQUENCE_ID=(SELECT MAX(SEQUENCE_ID) FROM " + strHistoryTable + " WHERE PRODUCT_ID=a.PRODUCT_ID AND STATION_ID=a.STATION_ID) "
                + " AND SUBSTR(STATION_ID,2,1)<>'_'";

            strTestData = strTestData + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi') "
                + " AND WORK_ORDER = " + ClsCommon.GetSqlString(WONO) + " AND PDDATE =(SELECT MAX(PDDATE) FROM " + PN.Substring(2, 3) + ".PRODUCT_HISTORY_V S WHERE T.PRODUCT_ID = S.PRODUCT_ID "
                + " AND T.STATION_CODE = S.STATION_CODE)) GROUP BY LINE_ID, STATION_ID, STATE_ID))";

            StrSql = " SELECT /*+ RULE*/LINE_ID" + GetStationList(PN.Substring(2, 3), strRegion) + " "
                + " FROM ((SELECT /*+ RULE*/LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY "
                + " FROM (" + rawdata + ") "
                + " GROUP BY LINE_ID, STATION_ID, STATE_ID) " + strTestData
                + " GROUP BY LINE_ID ORDER BY LINE_ID ";
            return StrSql;
        }

        private string QueryByModel(string Model, string DateStart, string DateEnd)
        {
            string StrSql = "";
            string rawdata = "";
            string strRegion = "";
            string strHistoryTable = "";
            string strTestData = "";
            switch (ddlRegion.SelectedIndex)
            {
                case 0:
                    strRegion = "'T','S'";
                    strHistoryTable = "MES_PCBA_HISTORY";
                    strTestData = "UNION (SELECT DISTINCT LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY FROM (SELECT DISTINCT PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                        + " DECODE(STATION_CODE,'DL','T_BT','CA','T_ET','BT','T_DT','BTWL','T_MT','PT','T_JT','B1','T_CT','B2','T_NT') STATION_ID,"
                        + " STATUS STATE_ID FROM " + Model + ".PRODUCT_HISTORY_V T WHERE STATION_CODE IN ('DL', 'CA', 'BT', 'BTWL', 'B1', 'B2', 'PT') ";
                    break;
                case 1:
                    //strRegion = "'A'";
                    //strHistoryTable = "MES_ASSY_HISTORY";
                    //strTestData = "UNION (SELECT DISTINCT LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY FROM (SELECT DISTINCT PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                    //    +" DECODE(STATION_CODE,'WL','A_DT','D2','T_LT','A1','A_KT','A2','A_CT','A3','A_HT','A4','A_IT','A5','A_JT') STATION_ID,"
                    //    +" STATUS STATE_ID FROM "+Model+".PRODUCT_HISTORY_V T WHERE   STATION_CODE IN ('WL', 'D2', 'A1', 'A2', 'A3', 'A4', 'A5') ";
                    //break;

                    strRegion = "'A'";
                    strHistoryTable = "MES_ASSY_HISTORY";
                    if (Model == "RCX" || Model == "DVR" || Model == "GNG" || Model == "SLG" || Model == "TWN" || Model == "DVL")
                        strTestData = "UNION (SELECT   LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY  FROM (SELECT * FROM (SELECT DISTINCT  PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                            + "STATION_CODE STATION_ID,"
                            + " STATUS STATE_ID,ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID,STATION_CODE ORDER BY PDDATE DESC) RN FROM " + Model + ".PRODUCT_HISTORY_V T WHERE  ";
                    else
                        strTestData = "UNION (SELECT  LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY FROM (SELECT * FROM (SELECT DISTINCT PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                            + " DECODE(STATION_CODE,'WL','A_DT','D2','T_LT','A1','A_KT','A2','A_CT','A3','A_HT','A4','A_IT','A5','A_JT') STATION_ID,"
                            + " STATUS STATE_ID,ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID,STATION_CODE ORDER BY PDDATE DESC) RN FROM " + Model + ".PRODUCT_HISTORY_V T WHERE ";
                    break;

                case 2:
                    strRegion = "'P'";
                    strHistoryTable = "MES_PACK_HISTORY";
                    strTestData = "UNION (SELECT   LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY FROM (SELECT   PRODUCT_ID,UPPER(LINE_CODE) LINE_ID,"
                        + " DECODE(STATION_CODE,'E2','P_BT') STATION_ID,"
                        + " STATUS STATE_ID FROM " + Model + ".PRODUCT_HISTORY_V T WHERE   STATION_CODE IN ('E2') ";
                    break;
            }

            //rawdata = " SELECT 'LINE' ||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1)) - 64) LINE_ID, SUBSTR(STATION_ID,1,1)||'_'||SUBSTR(STATION_ID,3,2) STATION_ID, STATE_ID "
            //    + " FROM " + strHistoryTable + " a "
            //    + " WHERE CREATION_DATE BETWEEN TO_DATE(" +ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" +ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi') ";

            //strTestData = strTestData + " AND PDDATE BETWEEN TO_DATE(" +ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" +ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi') ";
            //if (ckbRepair.Checked)  //Without Repair
            //{
            //    strTestData = strTestData + " AND REPAIR = 0 ";
            //}
            //strTestData = strTestData + " AND PDDATE =(SELECT MAX(PDDATE) FROM "+Model+".PRODUCT_HISTORY_V S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_CODE = S.STATION_CODE) ";

            //if (Model.CompareTo("")!=0)
            //{
            //    rawdata = rawdata + " AND PRODUCT_ID LIKE " +ClsCommon.GetSqlString(Model+ "%");
            //    strTestData = strTestData + " AND PRODUCT_ID LIKE " +ClsCommon.GetSqlString(Model+ "%");
            //}

            //rawdata = rawdata + " AND SEQUENCE_ID=(SELECT MAX(SEQUENCE_ID) FROM " + strHistoryTable + " WHERE PRODUCT_ID=a.PRODUCT_ID AND STATION_ID=a.STATION_ID) "
            //    + " AND SUBSTR(STATION_ID,2,1)<>'_' ";
            //strTestData = strTestData + ") GROUP BY LINE_ID, STATION_ID, STATE_ID)";

            //StrSql = " SELECT DISTINCT LINE_ID " + GetStationList(Model, strRegion)
            //    + " FROM ((SELECT /*+ RULE*/DISTINCT LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY"
            //    + " FROM (" + rawdata + ") "
            //    + " GROUP BY LINE_ID, STATION_ID, STATE_ID) "+strTestData
            //    + " ) GROUP BY LINE_ID ORDER BY LINE_ID";	
            //return StrSql;

            rawdata = " SELECT 'LINE' ||TO_CHAR(ASCII(SUBSTR(A.STATION_ID, 2, 1)) - 64) LINE_ID,SUBSTR(A.STATION_ID,1,1)||'_'||SUBSTR(A.STATION_ID,3,2) STATION_ID,A.STATE_ID FROM SFC.MES_ASSY_HISTORY  A ,SHP.CMCS_SFC_AORDER C WHERE "
                + " A.CREATION_DATE  >= TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND A.CREATION_DATE  <=TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi') AND A.WO_NO=C.AORDER AND SUBSTR(C.APART,3,3)='"+Model+"'";

            strTestData = strTestData + " PDDATE >= TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND PDDATE <= TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi') ";
            if (ckbRepair.Checked)  //Without Repair
            {
                strTestData = strTestData + " AND REPAIR = 0 ";
            }
            //strTestData = strTestData + " AND PDDATE =(SELECT MAX(PDDATE) FROM " + Model + ".PRODUCT_HISTORY_V S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_CODE = S.STATION_CODE) ";

            //rcx dvr gng ?

            //if (Model.CompareTo("") != 0)
            //{
            //    if (Model == "RCX" || Model == "DVR" || Model == "GNG" || Model == "SLG" || Model == "TWN" || Model == "DVL")
            //    {
            //        rawdata = rawdata + " AND PRODUCT_ID IN (SELECT  S.PRODUCT_ID FROM Sfc.MES_PCBA_PANEL_DETAIL R,Sfc.MES_PCBA_PANEL_LINK S,SHP.CMCS_SFC_SORDER T WHERE S.PANEL_ID=R.PANEL_ID AND R.WO_NO=T.SORDER AND T.MODEL =" + ClsCommon.GetSqlString(Model) + ")";
            //        strTestData = strTestData + " AND PRODUCT_ID IN (SELECT  S.PRODUCT_ID FROM Sfc.MES_PCBA_PANEL_DETAIL R,Sfc.MES_PCBA_PANEL_LINK S,SHP.CMCS_SFC_SORDER T WHERE S.PANEL_ID=R.PANEL_ID AND R.WO_NO=T.SORDER AND T.MODEL = " + ClsCommon.GetSqlString(Model) + ")";
            //    }
            //    else
            //    {
            //        rawdata = rawdata + " AND  SUBSTR(PRODUCT_ID,1,3) = " + ClsCommon.GetSqlString(Model);
            //        strTestData = strTestData + " AND SUBSTR(PRODUCT_ID,1,3) = " + ClsCommon.GetSqlString(Model);
            //    }

            //}

            //rawdata = rawdata + " AND SUBSTR(STATION_ID,2,1)<>'_' ";
            strTestData = strTestData + " ) WHERE RN=1 ) GROUP BY LINE_ID, STATION_ID, STATE_ID)";

            StrSql = " SELECT LINE_ID " + GetStationList(Model, strRegion)
                + " FROM ((SELECT /*+RULE*/  LINE_ID, STATION_ID, STATE_ID, COUNT(*) QTY"
                + " FROM (" + rawdata + ") "
                + " GROUP BY LINE_ID, STATION_ID, STATE_ID) " + strTestData
                + " ) GROUP BY LINE_ID";
            return StrSql;
        }

        private String GetStationList(string Model, string Region)
        {
            string station_list = "";
            string sequence_list = "";
            string sqlstr = "";
            string SelectStr = "";

            //station_list = "SELECT STATE_ID, DECODE(STATE_ID,'P',STATION_ID,STATION_ID_O) STATION_ID "
            //    + " FROM MES_COMM_ROUTE "
            //    + " WHERE AREA_ID IN (" + Region + ") ";
            //if (Model != "")
            //{
            //    station_list = station_list + " AND MODEL_ID=" + ClsCommon.GetSqlString(Model);
            //}
            //sequence_list = "SELECT   SEQUENCE_ID, STATION_ID,'P'STATE_ID "
            //    + "FROM MES_COMM_ROUTE AA "
            //    + "WHERE AREA_ID1 IN (" + Region + ") ";
            //if (Model != "")
            //{
            //    sequence_list = sequence_list + "AND MODEL_ID=" + ClsCommon.GetSqlString(Model);
            //}
            //sequence_list = sequence_list + " AND SEQUENCE_ID=(SELECT MAX(SEQUENCE_ID) FROM MES_COMM_ROUTE "
            //    + " WHERE AREA_ID1=AA.AREA_ID1 ";
            //if (Model != "")
            //{
            //    sequence_list = sequence_list + " AND MODEL_ID=AA.MODEL_ID "
            //        + "AND MODEL_ALIGN=AA.MODEL_ALIGN ";
            //}
            //sequence_list = sequence_list + "AND STATION_ID=AA.STATION_ID)";


            //sqlstr = "SELECT B.SEQUENCE_ID, A.STATION_ID, A.STATE_ID "
            //    + "FROM (" + station_list + ") A "
            //    + ",(" + sequence_list + ") B "
            //    + "WHERE a.STATION_ID=b.STATION_ID "
            //    + "ORDER BY B.SEQUENCE_ID ";

            sequence_list = "SELECT SEQUENCE_ID,STATION_ID,STATE_ID FROM MES_COMM_ROUTE WHERE AREA_ID1 IN (" + Region + ")  AND MODEL_ID=" + ClsCommon.GetSqlString(Model)+" AND MODEL_ALIGN='20' ORDER BY SEQUENCE_ID ";
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(sqlstr).Tables[0];
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(sequence_list).Tables[0];
            SelectStr = "";

            foreach (DataRow rw in dt.Rows)
            {
                SelectStr = SelectStr + ","
                    + "SUM("
                    + "DECODE(STATION_ID"
                    + "," + ClsCommon.GetSqlString(rw["STATION_ID"].ToString())
                    + ",DECODE(STATE_ID, " + ClsCommon.GetSqlString(rw["STATE_ID"].ToString()) + ",QTY,0)"
                    + ",0)"
                    + ") " + rw["STATION_ID"].ToString() + "_" + rw["STATE_ID"].ToString();

                if (rw["STATION_ID"].ToString().Substring(0, 1) != "S" && rw["STATION_ID"].ToString() != "A_BI")
                {
                    SelectStr = SelectStr + ","
                        + "SUM("
                        + "DECODE(STATION_ID"
                        + "," + ClsCommon.GetSqlString(rw["STATION_ID"].ToString())
                        + ",DECODE(STATE_ID, 'F',QTY,0)"
                        + ",0)"
                        + ") " + rw["STATION_ID"].ToString() + "_F";
                }

                //if ((rw["STATION_ID"].ToString().Substring(3,1).Equals("T"))
                //    ||(rw["STATION_ID"].ToString().Substring(3,1).Equals("O"))) 
                //{
                //    SelectStr = SelectStr + ","
                //        + "SUM("
                //        + "DECODE(STATION_ID"
                //        + ","+ClsCommon.GetSqlString(rw["STATION_ID"].ToString())
                //        + ",DECODE(STATE_ID, 'F',QTY,0)"
                //        + ",0)"
                //        + ") " + rw["STATION_ID"].ToString()+"_F";
                //}
            }
            return SelectStr;
        }

        protected void btnQuery_Click(object sender, System.EventArgs e)
        {
            if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
            {
                Label28.Text = ViewState["ErrorDate"].ToString();
                Label28.Visible = true;
                Label29.Visible = false;
                return;
            }
            if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
            {
                Label29.Text = ViewState["ErrorDate"].ToString();
                Label29.Visible = true;
                Label28.Visible = false;
                return;
            }

            Label28.Visible = false;
            Label29.Visible = false;
            string StrSql = "";
            //switch (rblType.SelectedIndex)
            //{
            //    case 0:
                    StrSql = QueryByModel(ddlType.SelectedValue, tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
            //        break;
            //    case 1:
            //        StrSql = QueryByWO(tbType.Text.Trim().ToUpper(), tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
            //        if (StrSql.Equals(""))
            //        {
            //            Page.ClientScript.RegisterStartupScript(this.GetType(), "WONotExist", "<script language=javascript>alert('" + ViewState["WONotExist"].ToString() + "');</script>");
            //            return;
            //        }
            //        break;
            //}

            //OracleConnection conn = new OracleConnection("Data Source=TYSFC;Persist Security Info=True;User ID=SFC;Password=SFC;Unicode=True");
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
            OracleConnection conn = new OracleConnection(strcon);
            conn.Open();
            OracleCommand cmd = new OracleCommand(StrSql);
            cmd.Connection = conn;
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand =cmd;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            BuilderHtmlTableTitleClicking(ds.Tables[0]);
            FullTableData(ds.Tables[0]);
            conn.Close();

            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            //BuilderHtmlTableTitleClicking(dt);
            //FullTableData(dt);
            //			StrSql = ShowHeader(dt);
            //			Page.RegisterClientScriptBlock("test",StrSql);
            //dgPCBA.DataSource = dt.DefaultView;
            //dgPCBA.DataBind();
            //dgPCBA.Columns.Add()
            //如果以工單查，檢查工單是否存在，調用函數：GetPN(string WO_NO)
        }

        private void BuilderHtmlTableTitleClicking(DataTable dtSet)
        {
            HtmlTableRow htr = new HtmlTableRow();
            HtmlTableRow htr1 = new HtmlTableRow();

            HtmlTableCell htc = new HtmlTableCell();
            htc.InnerHtml = "<font color='White'><STRONG>Line ID</STRONG></font>";
            htc.RowSpan = 2;
            htc.BgColor = "#006699";
            htc.Align = "Center";
            htr.Cells.Add(htc);

            for (int i = 1; i <= dtSet.Columns.Count - 1; i++)
            {
                string strColumnName = dtSet.Columns[i].ColumnName;
                //if (strColumnName.Substring(5,1).ToUpper()=="P")

                AddTableCellClicking(htr, htr1, i, strColumnName, strColumnName.Substring(strColumnName.Length - 1, 1).ToUpper());

            }
            tblEffiency.Rows.Add(htr);
            tblEffiency.Rows.Add(htr1);
            htr.Dispose();
            htr1.Dispose();
        }

        private void AddStationCell(HtmlTableRow htr, HtmlTableRow htr1, string StationName, string strStatus)
        {
            HtmlTableCell htc = new HtmlTableCell();
            htc.InnerHtml = "<font color='White'><STRONG>" + StationName + "</STRONG></font>";
            //htc.ColSpan = 2;
            htc.BgColor = "#006699";
            htc.Align = "Center";
            htr.Cells.Add(htc);
            htc.Dispose();

            if (strStatus.Equals("P"))
            {
                htc = new HtmlTableCell();
                htc.InnerHtml = "<font color='White'><STRONG>Pass</STRONG></font>";
                htc.BgColor = "#006699";
                htc.Align = "Center";
                htr1.Cells.Add(htc);
                htc.Dispose();
            }
            else
            {
                htc = new HtmlTableCell();
                htc.InnerHtml = "<font color='White'><STRONG>Fail</STRONG></font>";
                htc.BgColor = "#006699";
                htc.Align = "Center";
                htr1.Cells.Add(htc);
                htc.Dispose();
            }
        }
        //傳入一個HtmlTableRow，在這一行中增加Cell
        private void AddTableCellClicking(HtmlTableRow htr, HtmlTableRow htr1, int intColumns, string strColumnName, string strStatus)
        {
            HtmlTableCell htc = new HtmlTableCell();
            //HtmlTableCell htc1 = new HtmlTableCell();
            if ((!(strColumnName.Substring(3, 1).Equals("T")) && !(strColumnName.Substring(2, 2).Equals("FO")) && (strColumnName.Substring(3, 1).Equals("I"))) || strColumnName.Substring(0, strColumnName.Length - 2).Equals("A_BI"))
            {
                htc.InnerHtml = "<font color='White'><STRONG>Input</STRONG></font>";
                htc.RowSpan = 2;
                htc.BgColor = "#006699";
                htc.Align = "Center";
                htr.Cells.Add(htc);
                htc.Dispose();
            }
            else
            {
                switch (strColumnName.Substring(0, strColumnName.Length - 2).ToUpper())
                {
                    //PCBA
                    case "S_BO":
                        AddStationCell(htr, htr1, "XRAY", strStatus);
                        break;
                    case "S_CO":
                        AddStationCell(htr, htr1, "Touch/Up", strStatus);
                        break;
                    case "S_DO":
                        AddStationCell(htr, htr1, "IPQC", strStatus);
                        break;
                    case "T_AI":
                        AddStationCell(htr, htr1, "Router", strStatus);
                        break;
                    case "T_BT":
                        AddStationCell(htr, htr1, "DownLoad", strStatus);
                        break;
                    case "T_CT":
                        AddStationCell(htr, htr1, "BaseBand1", strStatus);
                        break;
                    case "T_NT":
                        AddStationCell(htr, htr1, "BaseBand2", strStatus);
                        break;
                    case "T_OT":
                        AddStationCell(htr, htr1, "BaseBand3", strStatus);
                        break;
                    case "T_PT":
                        AddStationCell(htr, htr1, "BaseBand4", strStatus);
                        break;
                    case "T_LT":
                        AddStationCell(htr, htr1, "BaseBand5", strStatus);
                        break;
                    case "T_DT":
                        AddStationCell(htr, htr1, "BlueTooth", strStatus);
                        break;
                    case "T_MT":
                        AddStationCell(htr, htr1, "BT Wireless", strStatus);
                        break;
                    case "T_ET":
                        AddStationCell(htr, htr1, "Calibration", strStatus);
                        break;
                    case "T_JT":
                        AddStationCell(htr, htr1, "Pretest", strStatus);
                        break;
                    //--------------NexTest 
                    case "PW":
                        AddStationCell(htr, htr1, "PowerOn", strStatus);
                        break;
                    case "BlnkFlsh":
                        AddStationCell(htr, htr1, "BlnkFlsh", strStatus);
                        break;
                    case "T_FO":
                        AddStationCell(htr, htr1, "Glue", strStatus);
                        break;

                    //Assembly

                    case "A_KT":
                        AddStationCell(htr, htr1, "ABaseBand1", strStatus);
                        break;
                    case "A_CT":
                        AddStationCell(htr, htr1, "ABaseBand2", strStatus);
                        break;
                    case "A_HT":
                        AddStationCell(htr, htr1, "ABaseBand3", strStatus);
                        break;
                    case "A_IT":
                        AddStationCell(htr, htr1, "ABaseBand4", strStatus);
                        break;
                    case "A_DT":
                        AddStationCell(htr, htr1, "Wireless", strStatus);
                        break;
                    case "A_ET":
                        AddStationCell(htr, htr1, "BTWireless", strStatus);
                        break;
                    case "A_LT":
                        AddStationCell(htr, htr1, "ReDownload", strStatus);
                        break;
                    //-------------------NexTest
                    case "FL":
                        AddStationCell(htr, htr1, "Flashing", strStatus);
                        break;
                    case "BD":
                        AddStationCell(htr, htr1, "Board Test", strStatus);
                        break;
                    case "PH":
                        AddStationCell(htr, htr1, "Phasing", strStatus);
                        break;
                    case "PT":
                        AddStationCell(htr, htr1, "Proto", strStatus);
                        break;
                    case "CI":
                        AddStationCell(htr, htr1, "CIT", strStatus);
                        break;
                    case "BL":
                        AddStationCell(htr, htr1, "Bluetooth", strStatus);
                        break;
                    case "HT":
                        AddStationCell(htr, htr1, "HotThermal", strStatus);
                        break;
                    case "CT":
                        AddStationCell(htr, htr1, "ColdThermal", strStatus);
                        break;
                    case "CM":
                        AddStationCell(htr, htr1, "Camera", strStatus);
                        break;
                    case "FC":
                        AddStationCell(htr, htr1, "Focus", strStatus);
                        break;
                    case "AD":
                        AddStationCell(htr, htr1, "Audio", strStatus);
                        break;
                    case "RA":
                        AddStationCell(htr, htr1, "Radiated", strStatus);
                        break;
                    case "QA":
                        AddStationCell(htr, htr1, "FQA", strStatus);
                        break;
                    case "WLA":
                        AddStationCell(htr, htr1, "WLAN", strStatus);
                        break;
                    case "GP":
                        AddStationCell(htr, htr1, "GPS", strStatus);
                        break;
                    case "CF":
                        AddStationCell(htr, htr1, "CFC", strStatus);
                        break;
                    case "PK":
                        AddStationCell(htr, htr1, "Pack", strStatus);
                        break;
                    case "A_FO":
                        AddStationCell(htr, htr1, "FQC", strStatus);
                        break;

                    //Packing
                    case "P_AT":
                        AddStationCell(htr, htr1, "ReDownload", strStatus);
                        break;
                    case "P_BT":
                        AddStationCell(htr, htr1, "E2PConfig", strStatus);
                        break;
                    case "P_GO":
                        AddStationCell(htr, htr1, "OQC", strStatus);
                        break;
                    case "P_DT":
                        AddStationCell(htr, htr1, "Packing", strStatus);
                        break;
                    case "P_EO":
                        AddStationCell(htr, htr1, "OOB", strStatus);
                        break;
                    case "P_FI":
                        AddStationCell(htr, htr1, "PACK W/H", strStatus);
                        break;

                    default:
                        AddStationCell(htr, htr1, strColumnName.Substring(0, 4), strStatus);
                        break;
                }
            }
        }

        private void FullTableData(DataTable dt)
        {
            HtmlTableRow htr = null;
            HtmlTableCell htc = null;
            string strTemp = "SD=" + tbStartDate.DateTextBox.Text.Trim() + "&ED=" + tbEndDate.DateTextBox.Text.Trim() + "&Region=" + ddlRegion.SelectedValue;
            //if (rblType.SelectedIndex == 0)
            //{
                strTemp = strTemp + "&Model=" + ddlType.SelectedValue + "&WO=";
            //}
            //else
            //{
            //    strTemp = strTemp + "&Model=&WO=" + tbType.Text.Trim();
            //}
            foreach (DataRow rw in dt.Rows)
            {
                //htr = new HtmlTableRow();
                //for(int i=0;i<dt.Columns.Count;i++)
                //{
                //    htc = new HtmlTableCell();
                //    if (i!=0&&int.Parse(rw[i].ToString())>0)
                //        //htc.InnerHtml = "<a href=\"/SFCQuery/Boundary/WFrmPIDDetail.aspx?"+strTemp+"&StationID="+dt.Columns[i].ColumnName.Substring(0,1)+rw["LINE_ID"].ToString()+dt.Columns[i].ColumnName.Substring(2)+"\">"+rw[i].ToString()+"</a>";
                //        htc.InnerHtml = "<a href=\"/SFCQuery/Boundary/WFrmPIDDetail.aspx?"+strTemp+"&StationID="+dt.Columns[i].ColumnName.Substring(0,1)+Convert.ToChar(Convert.ToInt32(rw["LINE_ID"].ToString().Substring(4))+64)+dt.Columns[i].ColumnName.Substring(2)+"\" target=\"_blank\">"+rw[i].ToString()+"</a>";
                //    else
                //        htc.InnerHtml = rw[i].ToString();
                //    htc.Align = "Center";
                //    htr.Cells.Add(htc);
                //    htc.Dispose();
                //}
                //tblEffiency.Rows.Add(htr);
                //htr.Dispose();

                htr = new HtmlTableRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    htc = new HtmlTableCell();
                    if (i != 0 && int.Parse(rw[i].ToString()) > 0)
                    {
                        if (rw["LINE_ID"].ToString() == "")
                            htc.InnerHtml = "<a href=\"/SFCQuery/Boundary/WFrmPIDDetail.aspx?" + strTemp + "&StationID=" + dt.Columns[i].ColumnName.ToString() + "\" target=\"_blank\">" + rw[i].ToString() + "</a>";
                        //htc.InnerHtml = "<a href=\"/SFCQuery/Boundary/WFrmPIDDetail.aspx?"+strTemp+"&StationID="+dt.Columns[i].ColumnName.Substring(0,1)+rw["LINE_ID"].ToString()+dt.Columns[i].ColumnName.Substring(2)+"\">"+rw[i].ToString()+"</a>";
                        else
                            htc.InnerHtml = "<a href=\"/SFCQuery/Boundary/WFrmPIDDetail.aspx?" + strTemp + "&StationID=" + dt.Columns[i].ColumnName.Substring(0, 1) + Convert.ToChar(Convert.ToInt32(rw["LINE_ID"].ToString().Substring(4)) + 64) + dt.Columns[i].ColumnName.Substring(2) + "\" target=\"_blank\">" + rw[i].ToString() + "</a>";
                    }
                    else
                        htc.InnerHtml = rw[i].ToString();
                    htc.Align = "Center";
                    htr.Cells.Add(htc);
                    htc.Dispose();
                }
                tblEffiency.Rows.Add(htr);
                htr.Dispose();
            }
        }
        /*
        private string ShowHeader(DataTable rset) 
        {		
            for (int i=1 ; i<= cnt ; i++) 
            {
                Header += ("<td align='center'><font color='White' face='Arial' size='2'><STRONG>&nbsp;Pass&nbsp;</STRONG></font></td>");
                Header += ("<td align='center'><font color='White' face='Arial' size='2'><STRONG>&nbsp;Fail&nbsp;</STRONG></font></td>");
            }
	
            return Header;
        }
*/

    }

}
