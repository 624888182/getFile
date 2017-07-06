namespace SFCQuery.Boundary
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using DBAccess.EAI;
    using System.Globalization;
    using System.Resources;
    using System.Reflection;
    using System.Data.OracleClient;

    /// <summary>
    ///		Summary description for WFrmSFCQuery
    /// </summary>
    public partial class WFrmSFCPackQuery : System.Web.UI.UserControl
    {

        private static string strLine = "";
        private static string strWOFrom = "";
        //private static string strWOTO = "";
        private static string strModel = "";
        private static string strItem = "";
        private static string dtSDateFrom = "";
        private static string dtSDateTo = "";
        //private int Flag ; //用於判斷當前用戶點擊的是哪一個DataGrid
        private static string strStationID = "";
        private static string strdgWO_NO;
        //private static string strdgLine;
        private static string strdgModel;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            if (!IsPostBack)
            {
                MultiLanguage();
                BindModel();
                tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
                tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                    tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
                BindListSMT();
                BindListAssy();
                BindListPack();
            }
            ddlRegion.SelectedIndex = 2;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgSMT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgSMT_ItemCommand);
            this.dgAssembly.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAssembly_ItemCommand);
            this.dgPacking.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPacking_ItemCommand);
            this.dgPIDDetail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPIDDetail_ItemCommand);

        }
        #endregion

        private void MultiLanguage()
        {
            lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
            lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");
            //lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");
            lblItem.Text = (String)GetGlobalResourceObject("SFCQuery", "Item");
            lblRegion.Text = (String)GetGlobalResourceObject("SFCQuery", "Region");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");

            ViewState["WONull"] = (String)GetGlobalResourceObject("SFCQuery", "WONull");
            ViewState["ModelNull"] = (String)GetGlobalResourceObject("SFCQuery", "ModelNull");
            ViewState["InvalidDigit"] = (String)GetGlobalResourceObject("SFCQuery", "InvalidDigit");
            ViewState["ErrorDate"] = (String)GetGlobalResourceObject("CommonRes", "ErrorDate");
            ViewState["QueryCondition"] = (String)GetGlobalResourceObject("SFCQuery", "QueryCondition");

            dgSMT.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgSMT.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");
            dgSMT.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Line");
            dgSMT.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");
            dgSMT.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");

            dgAssembly.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgAssembly.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");
            dgAssembly.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Line");
            dgAssembly.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");
            dgAssembly.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");

            dgPacking.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgPacking.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");
            dgPacking.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");
            dgPacking.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");

            dgPIDDetail.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            //dgPIDDetail.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Line");
            dgPIDDetail.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "StateID");
        }

        protected void btnQuery_Click(object sender, System.EventArgs e)
        {
            //檢查至少要有一個查詢條件
            //			if (tbWOFrom.Text.Trim().Equals("")&&tbWOTo.Text.Trim().Equals("")&&tbModel.Text.Trim().Equals("")&&tbItem.Text.Trim().Equals("")
            //				&&tbLineFrom.Text.Trim().Equals("")&&tbLineEnd.Text.Trim().Equals("")&&tbStartDate.DateTextBox.Text.Trim().Equals("")&&tbEndDate.DateTextBox.Text.Trim().Equals(""))
            //			{
            //				Page.RegisterStartupScript("QueryCondition","<script language=javascript>alert('"+ViewState["QueryCondition"].ToString()+"');</script>");
            //				return;
            //			}
            strLine = "";
            strWOFrom = "";
            //			strWOTO = "";
            strModel = "";
            strItem = "";
            dtSDateFrom = "";
            dtSDateTo = "";
            strStationID = "";
            ClsGlobal.Flag = 0;

            //得到所有查詢條件的值
            //掃描日期的起迄
            if (!tbStartDate.DateTextBox.Text.Trim().Equals(""))
            {
                if (ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
                {
                    dtSDateFrom = tbStartDate.DateTextBox.Text.Trim();
                    Label29.Visible = false;
                }
                else
                {
                    Label29.Text = ViewState["ErrorDate"].ToString();
                    Label29.Visible = true;
                    return;
                }
            }
            if (!tbEndDate.DateTextBox.Text.Trim().Equals(""))
            {
                if (ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
                {
                    dtSDateTo = tbEndDate.DateTextBox.Text.Trim();
                    Label28.Visible = false;
                }
                else
                {
                    Label28.Text = ViewState["ErrorDate"].ToString();
                    Label28.Visible = true;
                    return;
                }
            }

            //if (!tbType.Text.Trim().Equals(""))   strWOFrom = tbType.Text.Trim().ToUpper();  //工單起
            //if (!tbWOTo.Text.Trim().Equals(""))     strWOTO = tbWOTo.Text.Trim().ToUpper();      //工單迄
            switch (rblType.SelectedIndex)
            {
                case 0:
                    strModel = ddlType.SelectedValue.ToString();
                    break;
                case 1:
                    strWOFrom = tbType.Text.Trim().ToUpper();
                    break;
            }


            //料號
            //if (!ddlLine.SelectedValue.ToString().Equals("")) strLine = ddlLine.SelectedValue.ToString(); //線別

            Label28.Visible = false;
            Label29.Visible = false;

            switch (ddlRegion.SelectedIndex)
            {
                case 0:
                    dgSMT.Visible = true;
                    dgAssembly.Visible = false;
                    dgPacking.Visible = false;
                    break;
                case 1:
                    dgSMT.Visible = false;
                    dgAssembly.Visible = true;
                    dgPacking.Visible = false;
                    break;
                case 2:
                    dgSMT.Visible = false;
                    dgAssembly.Visible = false;
                    dgPacking.Visible = true;
                    break;
            }

            //QueryByModel(ddlType.SelectedValue,tbStartDate.DateTextBox.Text.Trim(),tbEndDate.DateTextBox.Text.Trim());
            switch (rblType.SelectedIndex)
            {
                case 0:
                    if (!ddlType.SelectedValue.Equals(""))
                        QueryByModel(ddlType.SelectedValue, tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
                    break;
                case 1:
                    QueryByWO(tbType.Text.Trim().ToUpper(), tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
                    //if (StrSql.Equals(""))
                    //{
                    //    Page.RegisterStartupScript("WONotExist", "<script language=javascript>alert('" + ViewState["WONotExist"].ToString() + "');</script>");
                    //    return;
                    //}
                    break;

            }

            if (!ddlType.SelectedValue.ToString().Equals("")) strModel = ddlType.SelectedValue.ToString();    //機種
            if (!tbItem.Text.Trim().Equals(""))
            {
                strItem = tbItem.Text.Trim().ToUpper();
                ddlType.Text = "";
                tbType.Text = "";
                QueryByItem(tbItem.Text.Trim().ToUpper(), tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
            }
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];

            //			binddata(true);
            BindListPid();
            //			}
        }

        private void QueryByWO(string WO, string DateStart, string DateEnd)
        {
            switch (ddlRegion.SelectedIndex)
            {
                case 0:
                    GetDataByWO(dgSMT, "MES_PCBA_HISTORY", "CMCS_SFC_SORDER", WO);
                    break;
                case 1:
                    GetDataByWO(dgAssembly, "MES_ASSY_HISTORY", "CMCS_SFC_AORDER", WO);
                    break;
                case 2:
                    GetDataByWO(dgPacking, "CMCS_SFC_WIP_T", "CMCS_SFC_PORDER", WO);
                    break;
            }
        }
        private void QueryByModel(string Model, string DateStart, string DateEnd)
        {
            switch (ddlRegion.SelectedIndex)
            {
                case 0:
                    GetData(dgSMT, "MES_PCBA_HISTORY", "CMCS_SFC_SORDER", Model);
                    break;
                case 1:
                    GetData(dgAssembly, "MES_ASSY_HISTORY", "CMCS_SFC_AORDER", Model);
                    break;
                case 2:
                    GetData(dgPacking, "CMCS_SFC_WIP_T", "CMCS_SFC_PORDER", Model);
                    break;
            }
        }
        private void QueryByItem(string Item, string DateStart, string DateEnd)
        {
            switch (ddlRegion.SelectedIndex)
            {
                case 0:
                    GetDataByItem(dgSMT, "MES_PCBA_HISTORY", "CMCS_SFC_SORDER", Item);
                    break;
                case 1:
                    GetDataByItem(dgAssembly, "MES_ASSY_HISTORY", "CMCS_SFC_AORDER", Item);
                    break;
                case 2:
                    GetDataByItem(dgPacking, "CMCS_SFC_WIP_T", "CMCS_SFC_PORDER", Item);
                    break;
            }
        }

        //		private void binddata(bool refresh)
        //		{
        //			switch(ddlRegion.SelectedIndex)
        //			{
        //				case 0:
        //					GetData(dgSMT,"MES_PCBA_WIP","CMCS_SFC_SORDER");
        //					break;
        //				case 1:				
        //					GetData(dgAssembly,"MES_ASSY_WIP","CMCS_SFC_AORDER");
        //					break;
        //				case 2:
        //					GetData(dgPacking,"MES_PACK_WIP","CMCS_SFC_PORDER");
        //					break;
        //			}			
        //		}

        private void GetData(WebDataGrid dgResource, string strTable, string strWoTable, string strModel)
        {
            string strTemp = "";
            string strTestData = "";
            string strItem = "";
            string StrSql = "";
            string strOrder = "";
            switch (strTable.ToLower())
            {
                case "mes_pcba_history":
                    break;
                case "mes_assy_history":
                    break;
                case "cmcs_sfc_wip_t":
                    StrSql = "SELECT PPART PN,PPID_QTY WO_QTY,B.* FROM " + strWoTable + " A JOIN( ";
                    strTemp = " SUM(DECODE(STATION_ID,'E2',DECODE(STATE_ID, 'P',QTY,0),''))FE2P, "
                    + "SUM(DECODE(STATION_ID,'PACK',DECODE(STATE_ID, 'P',QTY,0),''))FPACK, "
                    + "SUM(DECODE(STATION_ID,'OQC',DECODE(STATE_ID, 'P',QTY,0),''))FOQC, "
                    + "SUM(DECODE(STATION_ID,'OOB',DECODE(STATE_ID, 'P',QTY,0),''))FOOB, "
                    + "SUM(DECODE(STATION_ID,'WH',DECODE(STATE_ID, '已出庫',QTY,0),''))FWH ";
                    strTestData += " ((SELECT WORK_ORDER WO_NO,'E2' STATION_ID, STATUS STATE_ID,PRODUCT_ID ,PDDATE CREATE_DATE "
                        + "  FROM {0}.PRODUCT_HISTORY_V T WHERE STATION_CODE ='E2' AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') )";
                    strTestData += " UNION ALL (SELECT WO_NO,STATION_NAME STATION_ID, STATE_FLAG STATE_ID,PRODUCT_ID ,UPDATE_DATE CREATE_DATE "
                        + "  FROM SFC.CMCS_SFC_WIP_T S,SHP.CMCS_SFC_PORDER T WHERE  S.WO_NO=T.PORDER AND PPART LIKE " + ClsCommon.GetSqlString("%" + strModel + "%") + " AND  UPDATE_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') AND STATION_NAME IN ('PACK','OQC','OOB'))";
                    strTestData += " UNION  ALL (SELECT WORK_ORDER WO_NO,  'WH' STATION_ID, STATUS STATE_ID,PRODUCTID PRODUCT_ID ,OUT_DATE CREATE_DATE "
                        + "  FROM SHP.CMCS_SFC_SHIPPING_DATA WHERE STATUS ='已出庫' AND MODEL = " + ClsCommon.GetSqlString(strModel) + " AND OUT_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi')";
                    if (!strItem.Equals(""))
                        strItem = strItem + " AND UPPER(A.PPART) LIKE " + ClsCommon.GetSqlString("%" + strItem + "%");
                    strOrder = "PORDER";
                    break;
            }

            StrSql = StrSql
                        + " SELECT WO_NO FWO_NO,FMODEL, "
                        + strTemp
                        + " FROM (SELECT WO_NO,'" + strModel + "' FMODEL, STATION_ID, STATE_ID, COUNT(*) QTY"
                        + " FROM (SELECT WO_NO,STATION_ID ,STATE_ID,PRODUCT_ID FROM ( "
                        + " SELECT WO_NO,STATION_ID ,STATE_ID,PRODUCT_ID,ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID ORDER BY CREATE_DATE DESC ) RN FROM ";
            switch (rblType.SelectedIndex)
            {
                case 0:
                    if (!strModel.Equals(""))
                    {
                        strModel = ddlType.SelectedValue.ToString().ToUpper();
                    }
                    break;
                case 1:
                    if (!strWOFrom.Trim().Equals(""))
                    {
                        StrSql = StrSql + " AND WO_NO = " + ClsCommon.GetSqlString(strWOFrom);
                        strModel = GetPN(strWOFrom).Substring(2, 3);
                    }
                    break;
            }

            if (!strLine.Trim().Equals(""))
            {
                StrSql = StrSql + " AND SUBSTR(STATION_ID,2,1) = CHR(" + strLine.Substring(4) + "+64)";//+ClsCommon.GetSqlString(strLine);
                strTestData = strTestData + " AND UPPER(LINE_CODE) = " + ClsCommon.GetSqlString(strLine);
            }

            if (ckbRepair.Checked)
                strTestData = strTestData + " AND REPAIR = 0 ";

            strTestData = string.Format(strTestData, strModel);
            StrSql = StrSql + strTestData + " ) ";

            StrSql = StrSql + " ))WHERE RN=1) GROUP BY WO_NO,substr(product_id,1,3), STATION_ID, STATE_ID) GROUP BY WO_NO,FMODEL)b  ON A." + strOrder + "=B.FWO_NO AND FMODEL = " + ClsCommon.GetSqlString(strModel);

            StrSql = StrSql + strItem;

            DataTable dtRes = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            dgResource.DataSource = dtRes.DefaultView;
            dgResource.DataBind();
        }

        private void GetDataByWO(WebDataGrid dgResource, string strTable, string strWoTable, string strWO)
        {
            string strTemp = "";
            string strTestData = "";
            string strItem = "";
            string StrSql = "";
            string strOrder = "";
            string strModel;
            string strSql = "select substr(ppart,3,3) model FROM CMCS_SFC_PORDER where porder='" + strWO.Trim().ToUpper() + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "WONotExist", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "WONotExist") + "');</script>");
                return;
            }
            else
                strModel = dt.Rows[0]["model"].ToString();
            switch (strTable.ToLower())
            {
                case "mes_pcba_history":
                    break;
                case "mes_assy_history":
                    break;
                case "cmcs_sfc_wip_t":
                    StrSql = "SELECT PPART PN,PPID_QTY WO_QTY,B.* FROM " + strWoTable + " A JOIN( ";
                    strTemp = " SUM(DECODE(STATION_ID,'E2',DECODE(STATE_ID, 'P',QTY,0),''))FE2P, "
                    + "SUM(DECODE(STATION_ID,'PACK',DECODE(STATE_ID, 'P',QTY,0),''))FPACK, "
                    + "SUM(DECODE(STATION_ID,'OQC',DECODE(STATE_ID, 'P',QTY,0),''))FOQC, "
                    + "SUM(DECODE(STATION_ID,'OOB',DECODE(STATE_ID, 'P',QTY,0),''))FOOB, "
                    + "SUM(DECODE(STATION_ID,'WH',DECODE(STATE_ID, '已出庫',QTY,0),''))FWH ";
                    strTestData += " ((SELECT WORK_ORDER WO_NO,'E2' STATION_ID, STATUS STATE_ID,PRODUCT_ID ,PDDATE CREATE_DATE "
                        + "  FROM {0}.PRODUCT_HISTORY_V T WHERE STATION_CODE ='E2' AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') )";
                    strTestData += " UNION ALL (SELECT WO_NO,STATION_NAME STATION_ID, STATE_FLAG STATE_ID,PRODUCT_ID ,UPDATE_DATE CREATE_DATE "
                        + "  FROM SFC.CMCS_SFC_WIP_T S,SHP.CMCS_SFC_PORDER T WHERE  S.WO_NO=T.PORDER AND PPART LIKE " + ClsCommon.GetSqlString("%" + strModel + "%") + " AND  UPDATE_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') AND STATION_NAME IN ('PACK','OQC','OOB'))";
                    strTestData += " UNION  ALL (SELECT WORK_ORDER WO_NO,  'WH' STATION_ID, STATUS STATE_ID,PRODUCTID PRODUCT_ID ,OUT_DATE CREATE_DATE "
                        + "  FROM SHP.CMCS_SFC_SHIPPING_DATA WHERE STATUS ='已出庫' AND MODEL = " + ClsCommon.GetSqlString(strModel) + " AND OUT_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi')";
                    if (!strItem.Equals(""))
                        strItem = strItem + " AND UPPER(A.PPART) LIKE " + ClsCommon.GetSqlString("%" + strItem + "%");
                    strOrder = "PORDER";
                    break;
            }

            StrSql = StrSql
                        + " SELECT WO_NO FWO_NO,FMODEL, "
                        + strTemp
                        + " FROM (SELECT WO_NO,'" + strModel + "' FMODEL, STATION_ID, STATE_ID, COUNT(*) QTY"
                        + " FROM (SELECT WO_NO,STATION_ID ,STATE_ID,PRODUCT_ID FROM ( "
                        + " SELECT WO_NO,STATION_ID ,STATE_ID,PRODUCT_ID,ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID ORDER BY CREATE_DATE DESC ) RN FROM ";

            if (!strLine.Trim().Equals(""))
            {
                StrSql = StrSql + " AND SUBSTR(STATION_ID,2,1) = CHR(" + strLine.Substring(4) + "+64)";//+ClsCommon.GetSqlString(strLine);
                strTestData = strTestData + " AND UPPER(LINE_CODE) = " + ClsCommon.GetSqlString(strLine);
            }

            if (ckbRepair.Checked)
                strTestData = strTestData + " AND REPAIR = 0 ";

            strTestData = string.Format(strTestData, strModel);
            StrSql = StrSql + strTestData + " ) ";

            StrSql = StrSql + " ))WHERE RN=1) GROUP BY WO_NO,substr(product_id,1,3), STATION_ID, STATE_ID) GROUP BY WO_NO,FMODEL)b  ON A." + strOrder + "=B.FWO_NO AND B.FWO_NO=" + ClsCommon.GetSqlString(strWO) + " AND FMODEL = " + ClsCommon.GetSqlString(strModel);

            StrSql = StrSql + strItem;

            DataTable dtRes = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            dgResource.DataSource = dtRes.DefaultView;
            dgResource.DataBind();
        }

        private void GetDataByItem(WebDataGrid dgResource, string strTable, string strWoTable, string strItem)
        {
            string strTemp = "";
            string strTestData = "";
            string StrSql = "";
            string strOrder = "";
            string strModel;
            string strSql = "select substr(ppart,3,3) model FROM CMCS_SFC_PORDER where ppart='" + strItem.Trim().ToUpper() + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PartNotExist", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "PartNotExist") + "');</script>");
                return;
            }
            else
                strModel = dt.Rows[0]["model"].ToString();
            switch (strTable.ToLower())
            {
                case "mes_pcba_history":
                    break;
                case "mes_assy_history":
                    break;
                case "cmcs_sfc_wip_t":
                    StrSql = "SELECT PPART PN,PPID_QTY WO_QTY,B.* FROM " + strWoTable + " A JOIN( ";
                    strTemp = " SUM(DECODE(STATION_ID,'E2',DECODE(STATE_ID, 'P',QTY,0),''))FE2P, "
                    + "SUM(DECODE(STATION_ID,'PACK',DECODE(STATE_ID, 'P',QTY,0),''))FPACK, "
                    + "SUM(DECODE(STATION_ID,'OQC',DECODE(STATE_ID, 'P',QTY,0),''))FOQC, "
                    + "SUM(DECODE(STATION_ID,'OOB',DECODE(STATE_ID, 'P',QTY,0),''))FOOB, "
                    + "SUM(DECODE(STATION_ID,'WH',DECODE(STATE_ID, '已出庫',QTY,0),''))FWH ";
                    strTestData += " ((SELECT WORK_ORDER WO_NO,'E2' STATION_ID, STATUS STATE_ID,PRODUCT_ID ,PDDATE CREATE_DATE "
                        + "  FROM {0}.PRODUCT_HISTORY_V T WHERE STATION_CODE ='E2' AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') )";
                    strTestData += " UNION ALL (SELECT WO_NO,STATION_NAME STATION_ID, STATE_FLAG STATE_ID,PRODUCT_ID ,UPDATE_DATE CREATE_DATE "
                        + "  FROM SFC.CMCS_SFC_WIP_T S,SHP.CMCS_SFC_PORDER T WHERE  S.WO_NO=T.PORDER AND PPART LIKE " + ClsCommon.GetSqlString("%" + strModel + "%") + " AND  UPDATE_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') AND STATION_NAME IN ('PACK','OQC','OOB'))";
                    strTestData += " UNION  ALL (SELECT WORK_ORDER WO_NO,  'WH' STATION_ID, STATUS STATE_ID,PRODUCTID PRODUCT_ID ,OUT_DATE CREATE_DATE "
                        + "  FROM SHP.CMCS_SFC_SHIPPING_DATA WHERE STATUS ='已出庫' AND MODEL = " + ClsCommon.GetSqlString(strModel) + " AND OUT_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi')";
                    strOrder = "PORDER";
                    break;
            }

            StrSql = StrSql
                        + " SELECT WO_NO FWO_NO,FMODEL, "
                        + strTemp
                        + " FROM (SELECT WO_NO,'" + strModel + "' FMODEL, STATION_ID, STATE_ID, COUNT(*) QTY"
                        + " FROM (SELECT WO_NO,STATION_ID ,STATE_ID,PRODUCT_ID FROM ( "
                        + " SELECT WO_NO,STATION_ID ,STATE_ID,PRODUCT_ID,ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID ORDER BY CREATE_DATE DESC ) RN FROM ";
            //switch (rblType.SelectedIndex)
            //{
            //    case 0:
            //        if (!strModel.Equals(""))
            //        {
            //            strModel = ddlType.SelectedValue.ToString().ToUpper();
            //        }
            //        break;
            //    case 1:
            //        if (!strWOFrom.Trim().Equals(""))
            //        {
            //            StrSql = StrSql + " AND WO_NO = " + ClsCommon.GetSqlString(strWOFrom);
            //            strModel = GetPN(strWOFrom).Substring(2, 3);
            //        }
            //        break;
            //}

            if (!strLine.Trim().Equals(""))
            {
                StrSql = StrSql + " AND SUBSTR(STATION_ID,2,1) = CHR(" + strLine.Substring(4) + "+64)";//+ClsCommon.GetSqlString(strLine);
                strTestData = strTestData + " AND UPPER(LINE_CODE) = " + ClsCommon.GetSqlString(strLine);
            }

            if (ckbRepair.Checked)
                strTestData = strTestData + " AND REPAIR = 0 ";

            strTestData = string.Format(strTestData, strModel);
            StrSql = StrSql + strTestData + " ) ";

            StrSql = StrSql + " ))WHERE RN=1) GROUP BY WO_NO,substr(product_id,1,3), STATION_ID, STATE_ID) GROUP BY WO_NO,FMODEL)b  ON A." + strOrder + "=B.FWO_NO AND A.PPART=" + ClsCommon.GetSqlString(strItem) + " AND FMODEL = " + ClsCommon.GetSqlString(strModel);

            DataTable dtRes = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            dgResource.DataSource = dtRes.DefaultView;
            dgResource.DataBind();
        }

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

        private DataTable GetSMTDate(DataTable dtRes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FWo_no", typeof(String));
            dt.Columns.Add("FModel", typeof(String));
            dt.Columns.Add("FLine", typeof(String));
            dt.Columns.Add("PN", typeof(String));
            dt.Columns.Add("WO_Qty", typeof(String));
            dt.Columns.Add("FInput", typeof(String));
            dt.Columns.Add("FXRay", typeof(String));
            dt.Columns.Add("FTouchUP", typeof(String));
            dt.Columns.Add("FRouter", typeof(String));
            dt.Columns.Add("FDownLoad", typeof(String));
            dt.Columns.Add("FCalibration", typeof(String));
            dt.Columns.Add("FPreTest", typeof(String));
            dt.Columns.Add("FBaseBand1", typeof(String));
            dt.Columns.Add("FBaseBand2", typeof(String));
            dt.Columns.Add("FGlue", typeof(String));

            string strWO_NO = "";
            string strModel = "";
            string strLine = "";
            DataRow dr = null;
            foreach (DataRow rw in dtRes.Rows)
            {
                if (strWO_NO != rw["wo_no"].ToString() || strModel != rw["model"].ToString() || strLine != rw["line"].ToString())
                {
                    if (dr != null)
                        dt.Rows.InsertAt(dr, 0);
                    dr = dt.NewRow();
                    dr["FWo_no"] = rw["wo_no"].ToString();
                    dr["FModel"] = rw["Model"].ToString();
                    dr["FLine"] = rw["Line"].ToString();
                    dr["PN"] = rw["PN"].ToString();
                    dr["wo_qty"] = rw["wo_qty"].ToString();
                    switch (rw["station_id"].ToString().ToUpper())
                    {
                        case "S_AI":
                            dr["FInput"] = rw["Qty"].ToString();
                            break;
                        case "S_BO":
                            dr["FXRay"] = rw["Qty"].ToString();
                            break;
                        case "S_CO":
                            dr["FTouchUP"] = rw["Qty"].ToString();
                            break;
                        case "T_AI":
                            dr["FRouter"] = rw["Qty"].ToString();
                            break;
                        case "T_BT":
                            dr["FDownLoad"] = rw["Qty"].ToString();
                            break;
                        case "T_ET":
                            dr["FCalibration"] = rw["Qty"].ToString();
                            break;
                        case "T_CT":
                            dr["FBaseBand1"] = rw["Qty"].ToString();
                            break;
                        case "T_NT":
                            dr["FBaseBand2"] = rw["Qty"].ToString();
                            break;
                        case "T_JT":
                            dr["FPreTest"] = rw["Qty"].ToString();
                            break;
                        case "T_FO":
                            dr["FGlue"] = rw["Qty"].ToString();
                            break;
                    }
                    strWO_NO = rw["wo_no"].ToString();
                    strModel = rw["Model"].ToString();
                    strLine = rw["Line"].ToString();
                }
                else
                {
                    switch (rw["station_id"].ToString().ToUpper())
                    {
                        case "S_AI":
                            dr["FInput"] = rw["Qty"].ToString();
                            break;
                        case "S_BO":
                            dr["FXRay"] = rw["Qty"].ToString();
                            break;
                        case "S_CO":
                            dr["FTouchUP"] = rw["Qty"].ToString();
                            break;
                        case "T_BT":
                            dr["FDownLoad"] = rw["Qty"].ToString();
                            break;
                        case "T_ET":
                            dr["FCalibration"] = rw["Qty"].ToString();
                            break;
                        case "T_CT":
                            dr["FBaseBand1"] = rw["Qty"].ToString();
                            break;
                        case "T_NT":
                            dr["FBaseBand2"] = rw["Qty"].ToString();
                            break;
                        case "T_JT":
                            dr["FPreTest"] = rw["Qty"].ToString();
                            break;
                        case "T_FO":
                            dr["FGlue"] = rw["Qty"].ToString();
                            break;
                    }
                    strWO_NO = rw["wo_no"].ToString();
                    strModel = rw["Model"].ToString();
                    strLine = rw["Line"].ToString();
                }
                //				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
                //				{
                //					dt.Rows.InsertAt(dr,0);
                //				}
            }
            if (dr != null)
                dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        private DataTable GetAssyDate(DataTable dtRes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FWo_no", typeof(String));
            dt.Columns.Add("FModel", typeof(String));
            dt.Columns.Add("FLine", typeof(String));
            dt.Columns.Add("PN", typeof(String));
            dt.Columns.Add("WO_Qty", typeof(String));
            dt.Columns.Add("FInput", typeof(String));
            dt.Columns.Add("FABaseBand1", typeof(String));
            dt.Columns.Add("FABaseBand2", typeof(String));
            dt.Columns.Add("FWireLess", typeof(String));
            dt.Columns.Add("FReDownload", typeof(String));
            dt.Columns.Add("FFQC", typeof(String));
            dt.Columns.Add("FReWork", typeof(String));

            string strWO_NO = "";
            string strModel = "";
            string strLine = "";
            DataRow dr = null;
            foreach (DataRow rw in dtRes.Rows)
            {
                if (strWO_NO != rw["wo_no"].ToString() || strModel != rw["model"].ToString() || strLine != rw["line"].ToString())
                {
                    if (dr != null)
                        dt.Rows.InsertAt(dr, 0);
                    dr = dt.NewRow();
                    dr["FWo_no"] = rw["wo_no"].ToString();
                    dr["FModel"] = rw["Model"].ToString();
                    dr["FLine"] = rw["Line"].ToString();
                    dr["PN"] = rw["PN"].ToString();
                    dr["wo_qty"] = rw["wo_qty"].ToString();
                    switch (rw["station_id"].ToString().ToUpper())
                    {
                        case "A_BI":
                            dr["FInput"] = rw["Qty"].ToString();
                            break;
                        case "A_KT":
                            dr["FABaseBand1"] = rw["Qty"].ToString();
                            break;
                        case "A_CT":
                            dr["FABaseBand2"] = rw["Qty"].ToString();
                            break;
                        case "A_DT":
                            dr["FWireLess"] = rw["Qty"].ToString();
                            break;
                        case "A_LT":
                            dr["FReDownload"] = rw["Qty"].ToString();
                            break;
                        case "A_FO":
                            dr["FFQC"] = rw["Qty"].ToString();
                            break;
                        case "A_MO":
                            dr["FReWork"] = rw["Qty"].ToString();
                            break;
                    }
                    strWO_NO = rw["wo_no"].ToString();
                    strModel = rw["Model"].ToString();
                    strLine = rw["Line"].ToString();

                }
                else
                {
                    switch (rw["station_id"].ToString().ToUpper())
                    {
                        case "A_BI":
                            dr["FInput"] = rw["Qty"].ToString();
                            break;
                        case "A_KT":
                            dr["FABaseBand1"] = rw["Qty"].ToString();
                            break;
                        case "A_CT":
                            dr["FABaseBand2"] = rw["Qty"].ToString();
                            break;
                        case "A_DT":
                            dr["FWireLess"] = rw["Qty"].ToString();
                            break;
                        case "A_LT":
                            dr["FReDownload"] = rw["Qty"].ToString();
                            break;
                        case "A_FO":
                            dr["FFQC"] = rw["Qty"].ToString();
                            break;
                        case "A_MO":
                            dr["FReWork"] = rw["Qty"].ToString();
                            break;
                    }
                    strWO_NO = rw["wo_no"].ToString();
                    strModel = rw["Model"].ToString();
                    strLine = rw["Line"].ToString();
                }
                //				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
                //				{
                //					dt.Rows.InsertAt(dr,0);
                //				}
            }
            if (dr != null)
                dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        private DataTable GetPackDate(DataTable dtRes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FWo_no", typeof(String));
            dt.Columns.Add("FModel", typeof(String));
            dt.Columns.Add("PN", typeof(String));
            dt.Columns.Add("WO_Qty", typeof(String));
            dt.Columns.Add("FE2P", typeof(String));
            dt.Columns.Add("FPACK", typeof(String));
            dt.Columns.Add("FOQC", typeof(String));
            dt.Columns.Add("FOOB", typeof(String));
            dt.Columns.Add("FWH", typeof(String));

            string strWO_NO = "";
            string strModel = "";
            string strLine = "";
            DataRow dr = null;
            foreach (DataRow rw in dtRes.Rows)
            {
                if (strWO_NO != rw["wo_no"].ToString() || strModel != rw["model"].ToString() || strLine != rw["line"].ToString())
                {
                    if (dr != null)
                        dt.Rows.InsertAt(dr, 0);
                    dr = dt.NewRow();
                    dr["FWo_no"] = rw["wo_no"].ToString();
                    dr["FModel"] = rw["Model"].ToString();
                    dr["PN"] = rw["PN"].ToString();
                    dr["wo_qty"] = rw["wo_qty"].ToString();
                    switch (rw["station_id"].ToString().ToUpper())
                    {
                        case "E2":
                            dr["FE2P"] = rw["Qty"].ToString();
                            break;
                        case "PACK":
                            dr["FPACK"] = rw["Qty"].ToString();
                            break;
                        case "P_GO":
                            dr["FOQC"] = rw["Qty"].ToString();
                            break;
                        case "P_EO":
                            dr["FOOB"] = rw["Qty"].ToString();
                            break;
                        case "WH":
                            dr["FWH"] = rw["Qty"].ToString();
                            break;
                    }
                    strWO_NO = rw["wo_no"].ToString();
                    strModel = rw["Model"].ToString();
                }
                else
                {
                    switch (rw["station_id"].ToString().ToUpper())
                    {
                        case "E2":
                            dr["FE2P"] = rw["Qty"].ToString();
                            break;
                        case "PACK":
                            dr["FPACK"] = rw["Qty"].ToString();
                            break;
                        case "P_GO":
                            dr["FOQC"] = rw["Qty"].ToString();
                            break;
                        case "P_EO":
                            dr["FOOB"] = rw["Qty"].ToString();
                            break;
                        case "WH":
                            dr["FWH"] = rw["Qty"].ToString();
                            break;
                    }
                    strWO_NO = rw["wo_no"].ToString();
                    strModel = rw["Model"].ToString();
                }
                //				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
                //				{
                //					dt.Rows.InsertAt(dr,0);
                //				}
            }
            if (dr != null)
                dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        private void GetOtherData(DataTable dtRes)
        {
            dgSMT.DataSource = dtRes.DefaultView;
            dgSMT.DataBind();
        }

        private void BindListSMT()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("FWo_no");
            dt.Columns.Add("FModel");
            dt.Columns.Add("FLine");
            dt.Columns.Add("FInput");
            dt.Columns.Add("FXRay");
            dt.Columns.Add("FTouchUP");
            dt.Columns.Add("FRouter");
            dt.Columns.Add("FDownLoad");
            dt.Columns.Add("FCalibration");
            dt.Columns.Add("FPreTest");
            dt.Columns.Add("FBaseBand1");
            dt.Columns.Add("FBaseBand2");
            dt.Columns.Add("FGlue");

            dgSMT.DataSource = dt.DefaultView;
            dgSMT.DataBind();
        }

        private void BindListAssy()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("FWo_no");
            dt.Columns.Add("FModel");
            dt.Columns.Add("FLine");
            dt.Columns.Add("FInput");
            dt.Columns.Add("FABaseBand1");
            dt.Columns.Add("FABaseBand2");
            dt.Columns.Add("FWireLess");
            dt.Columns.Add("FReDownload");
            dt.Columns.Add("FFQC");
            dt.Columns.Add("FReWork");

            dgAssembly.DataSource = dt.DefaultView;
            dgAssembly.DataBind();
        }

        private void BindListPack()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("FWo_no");
            dt.Columns.Add("FModel");
            dt.Columns.Add("FLine");
            dt.Columns.Add("FE2P");
            dt.Columns.Add("FPACK");
            dt.Columns.Add("FOQC");
            dt.Columns.Add("FOOB");
            dt.Columns.Add("FWH");

            dgPacking.DataSource = dt.DefaultView;
            dgPacking.DataBind();
        }

        private void dgSMT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ClsGlobal.Flag = 1;
            if (e.CommandName.ToUpper() == "SET")
            {
                if (dgSMT.CurrentStatus == "SET")
                {
                    BindListSMT();
                }
                else if (dgSMT.CurrentStatus == "")
                {
                    //binddata(false);
                    btnQuery_Click(null, null);
                }
            }
            else
            {
                if (e.CommandName.ToUpper() == "SORT" || e.CommandName.ToLower() == "page" || e.CommandName.ToLower() == "changepage")
                    btnQuery_Click(null, null);
                if (e.CommandName.ToUpper() != "SORT" && e.CommandName.ToLower() != "page" && e.CommandName.ToLower() != "changepage")
                {
                    strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
                    strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
                    //strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
                }
            }

            ClsGlobal.Flag = 1;
            //			strLine = strLine.Replace("'","\"");
            if (e.CommandName == "Input") { strStationID = "S_AI"; binddataPID(); }
            if (e.CommandName == "TouchUP") { strStationID = "S_CO"; binddataPID(); }
            if (e.CommandName == "XRay") { strStationID = "S_BO"; binddataPID(); }
            if (e.CommandName == "Router") { strStationID = "T_AI"; binddataPID(); }
            if (e.CommandName == "download") { strStationID = "T_BT"; binddataPID(); }
            if (e.CommandName == "Calibration") { strStationID = "T_ET"; binddataPID(); }
            if (e.CommandName == "PreTest") { strStationID = "T_JT"; binddataPID(); }
            if (e.CommandName == "BaseBand1") { strStationID = "T_CT"; binddataPID(); }
            if (e.CommandName == "BaseBand2") { strStationID = "T_NT"; binddataPID(); }
            if (e.CommandName == "Glue") { strStationID = "T_FO"; binddataPID(); }
        }

        private void dgAssembly_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ClsGlobal.Flag = 2;
            if (e.CommandName.ToUpper() == "SET")
            {
                if (dgAssembly.CurrentStatus == "SET")
                {
                    BindListAssy();
                }
                else if (dgAssembly.CurrentStatus == "")
                {
                    //binddata(false);
                    btnQuery_Click(null, null);
                }
            }
            else
            {
                if (e.CommandName.ToUpper() != "SORT" && e.CommandName.ToLower() == "page" || e.CommandName.ToLower() == "changepage")
                    btnQuery_Click(null, null);
                if (e.CommandName.ToUpper() != "SORT" && e.CommandName.ToLower() != "page" && e.CommandName.ToLower() != "changepage")
                {
                    strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
                    strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
                    //strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
                }
            }

            //			strLine = strLine.Replace("'","\"");
            if (e.CommandName == "Input") { strStationID = "A_BI"; binddataPID(); }
            if (e.CommandName == "ABaseBand1") { strStationID = "A_KT"; binddataPID(); }
            if (e.CommandName == "ABaseBand2") { strStationID = "A_CT"; binddataPID(); }
            if (e.CommandName == "ABaseBand3") { strStationID = "A_HT"; binddataPID(); }
            if (e.CommandName == "ABaseBand4") { strStationID = "A_IT"; binddataPID(); }
            if (e.CommandName == "WireLess") { strStationID = "A_DT"; binddataPID(); }
            if (e.CommandName == "ReDownload") { strStationID = "A_LT"; binddataPID(); }
            if (e.CommandName == "FQC") { strStationID = "A_FO"; binddataPID(); }
            if (e.CommandName == "ReWork") { strStationID = "A_MO"; binddataPID(); }
        }

        private void dgPacking_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ClsGlobal.Flag = 3;
            if (e.CommandName.ToUpper() == "SET")
            {
                if (dgPacking.CurrentStatus == "SET")
                {
                    BindListPack();
                }
                else if (dgPacking.CurrentStatus == "")
                {
                    //binddata(false);
                    btnQuery_Click(null, null);
                }
            }
            else
            {
                if (e.CommandName.ToUpper() == "SORT" && e.CommandName.ToLower() == "page" || e.CommandName.ToLower() == "changepage" || e.CommandName.ToLower() == "next" || e.CommandName.ToLower() == "prior")
                    btnQuery_Click(null, null);
                if (e.CommandName.ToUpper() != "SORT" && e.CommandName.ToLower() != "page" && e.CommandName.ToLower() != "changepage" && e.CommandName.ToLower() != "next" && e.CommandName.ToLower() != "prior")
                {
                    strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
                    strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
                    //strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
                }
            }

            ClsGlobal.Flag = 3;
            //			strLine = strLine.Replace("'","\"");
            if (e.CommandName == "E2P") { strStationID = "E2"; binddataPID(); }
            if (e.CommandName == "PACK") { strStationID = "PACK"; binddataPID(); }
            if (e.CommandName == "OQC") { strStationID = "OQC"; binddataPID(); }
            if (e.CommandName == "OOB") { strStationID = "OOB"; binddataPID(); }
            if (e.CommandName == "WH") { strStationID = "WH"; binddataPID(); }
        }

        private void binddataPID()
        {
            string strStation = strStationID;
            string StrSql;
            StrSql = " SELECT PRODUCT_ID FPID,WO_NO FWO_NO,STATE_ID FStatus FROM (  SELECT WO_NO,STATION_ID ,STATE_ID,PRODUCT_ID, ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID ORDER BY CREATE_DATE DESC ) RN FROM  ( "
                + "(SELECT WORK_ORDER WO_NO,'E2' STATION_ID, STATUS STATE_ID,PRODUCT_ID ,PDDATE CREATE_DATE   FROM DUO.PRODUCT_HISTORY_V T  WHERE STATION_CODE ='E2' AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') ) "
                + "UNION ALL (SELECT WO_NO,STATION_NAME STATION_ID, STATE_FLAG STATE_ID,PRODUCT_ID ,UPDATE_DATE CREATE_DATE  FROM SFC.CMCS_SFC_WIP_T S,SHP.CMCS_SFC_PORDER T WHERE  S.WO_NO=T.PORDER AND PPART LIKE '%" + strdgModel + "%' AND  UPDATE_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi') AND STATION_NAME IN ('PACK','OQC','OOB')) "
                + "UNION  ALL (SELECT WORK_ORDER WO_NO,'WH' STATION_ID,'P'  STATE_ID,PRODUCTID PRODUCT_ID,OUT_DATE CREATE_DATE FROM SHP.CMCS_SFC_SHIPPING_DATA WHERE STATUS ='已出庫' AND MODEL = '" + strdgModel + "' AND OUT_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi'))))WHERE RN=1"
                + " AND STATION_ID='" + strStation + "' AND STATE_ID='P' AND WO_NO=" + ClsCommon.GetSqlString(strdgWO_NO);
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            dgPIDDetail.DataSource = dt.DefaultView;
            dgPIDDetail.DataBind();
            dt.Dispose();
        }

        private void BindListPid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FPID");
            dt.Columns.Add("FWO_NO");
            dt.Columns.Add("FStatus");

            dgPIDDetail.DataSource = dt.DefaultView;
            dgPIDDetail.DataBind();
        }


        private void dgPIDDetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName.ToUpper() == "SET")
            {
                if (dgPIDDetail.CurrentStatus == "SET")
                {
                    BindListPid();
                }
                else if (dgPIDDetail.CurrentStatus == "")
                {
                    //binddataPID();
                    BindListPid();
                }
            }
            else
                if (e.CommandName.ToLower() == "sort" || e.CommandName.ToLower() == "page" || e.CommandName.ToLower() == "changepage")
                    binddataPID();
                else
                    if (e.CommandName == "PID")
                    {
                        string strProdurct = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text;
                        //Session["Lines"] = ((Label)(e.Item.Cells[0].Controls[1])).Text;
                        string strScript = "<script language='jscript'>var res = window.showModalDialog('./WFrmStationDetail.aspx?PID=" + strProdurct + "&MyDate=' + Date(), '','dialogWidth:450px; dialogHeight:400px; center:yes; scroll:1;"
                            + "status:no;help:no');</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowStationElem", strScript);
                    }
        }

        protected void rblType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (rblType.SelectedIndex)
            {
                case 0:
                    ddlType.Visible = true;
                    tbType.Visible = false;
                    break;
                case 1:
                    ddlType.Visible = false;
                    tbType.Visible = true;
                    break;
            }
        }

        private void BindModel()
        {
            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlType.DataTextField = "Model";
            ddlType.DataValueField = "Model";
            ddlType.DataSource = dt.DefaultView;
            ddlType.DataBind();
            ddlType.Items.Insert(0, "");
        }
    }
}
