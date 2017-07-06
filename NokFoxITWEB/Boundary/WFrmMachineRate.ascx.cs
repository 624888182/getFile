/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/09/11
 *  Modifier : Shu Jian Bo             Date: 2007/09/11
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
    using System.Reflection;
    using System.Resources;
    using DBAccess.EAI;
    using DB.EAI;
    using ChartDirector;
    using System.Data.OracleClient;

    /// <summary>
    ///		WFrmMachineRate 的摘要描述。
    /// </summary>
    public partial class WFrmMachineRate : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在這裡放置使用者程式碼以初始化網頁
            if (!IsPostBack)
            {
                tbStartDate.DateTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 08:20";
                tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:20";
                InitialDDL();
                MultiLanaguage();
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
            this.dgMachineRate.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgMachineRate_PageIndexChanged);
            this.dgSMTLineRate.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgSMTLineRate_PageIndexChanged);
            this.dgAssyLineRate.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgAssyLineRate_PageIndexChanged);

        }
        #endregion

        private void InitialDDL()
        {
            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlModel.DataTextField = "MODEL";
            ddlModel.DataValueField = "MODEL";
            ddlModel.DataSource = dt.DefaultView;
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, "");
 
        }

        private void MultiLanaguage()
        {
            //lblMonth.Text = (String)GetGlobalResourceObject("SFCQuery","Month");
            lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
            Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
            Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
            lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
            lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");
            lblStation.Text = (String)GetGlobalResourceObject("SFCQuery", "StationID");
            lblMachineRate.Text = (String)GetGlobalResourceObject("SFCQuery", "MachineYield1");
            lblSMTLineRate.Text = (String)GetGlobalResourceObject("SFCQuery", "SMTLineYield");
            lblAssyLineRate.Text = (String)GetGlobalResourceObject("SFCQuery", "ASSYLineYield");

            dgMachineRate.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Line");
            dgMachineRate.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "TestStation");
            dgMachineRate.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "PassQty");
            dgMachineRate.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "FailQty");
            dgMachineRate.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "TotalQty");
            dgMachineRate.Columns[5].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "MachineYield");

            dgSMTLineRate.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Line");
            dgSMTLineRate.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "PassQty");
            dgSMTLineRate.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "FailQty");
            dgSMTLineRate.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "TotalQty");
            dgSMTLineRate.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "LinesYield");

            dgAssyLineRate.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Line");
            dgAssyLineRate.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "PassQty");
            dgAssyLineRate.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "FailQty");
            dgAssyLineRate.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "TotalQty");
            dgAssyLineRate.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "LinesYield");
        }

        protected void btnQuery_Click(object sender, System.EventArgs e)
        {
            dgMachineRate.CurrentPageIndex = 0;
            dgSMTLineRate.CurrentPageIndex = 0;
            dgAssyLineRate.CurrentPageIndex = 0;
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
            wcvMachineRate.Visible = false;
            lblMachineRate.Visible = true;
            //lblSMTLineRate.Visible = true;
            //lblAssyLineRate.Visible = true;
            //by  litao  add start 2011/4/28
            if (ddlStation.SelectedValue == "" || ddlStation.SelectedItem == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇站名！！');</script>");
                   
                return;
            }
            if (ddlModel.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種！！');</script>");
                   
                return;
            }
            //by litao add end 2011/4/28
            GetData();
            //GetLineRate(1, dgSMTLineRate);
            //GetLineRate(2, dgAssyLineRate);
        }

        private void GetData()
        {
            string strStartDate = tbStartDate.DateTextBox.Text.Trim();
            string strEndDate = tbEndDate.DateTextBox.Text.Trim();
            string strStation = ddlStation.SelectedValue.Trim().ToString().ToUpper();
            string strStationDesc = ddlStation.SelectedItem.Text;
            string strModel = ddlModel.SelectedValue.Trim().ToUpper();
            string strTemp = "";
            string strSql;
            if (!strStation.Equals(""))
                strTemp = " AND STATION_CODE = " + ClsCommon.GetSqlString(strStation);
            if (strModel == "HAIER" || strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "TWN" || strModel == "TWL")
                strSql = "SELECT S.LINEID,S.TESTSTATION,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY,(S.PASSQTY+NVL(T.FAILQTY,0))TOTALQTY,ROUND(S.PASSQTY/(S.PASSQTY+NVL(T.FAILQTY,0))*100,2)YIELD,"
                    + " ROUND(NVL(T.FAILQTY, 0) / (S.PASSQTY + NVL(T.FAILQTY, 0)) * 100, 2) DEFECTRATE FROM "
                    + " (SELECT LINE_CODE LINEID,COMPUTERNAME TESTSTATION,COUNT(*)PASSQTY FROM " + ddlModel.SelectedValue + ".PRODUCT_HISTORY_V "
                    + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'P' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE,COMPUTERNAME) S, "
                    + " (SELECT LINE_CODE LINEID,COMPUTERNAME TESTSTATION,COUNT(*)FAILQTY FROM " + ddlModel.SelectedValue + ".PRODUCT_HISTORY_V "
                    + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'F' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE,COMPUTERNAME ) T "
                    + " WHERE  S.TESTSTATION = T.TESTSTATION(+) order by S.LINEID,S.TESTSTATION";

                    //+ " WHERE S.LINEID = T.LINEID(+)  ";  有線別時，再使用該語句
            else
                //strSql = "SELECT S.LINEID,S.TESTSTATION,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY,(S.PASSQTY+NVL(T.FAILQTY,0))TOTALQTY,ROUND(S.PASSQTY/(S.PASSQTY+NVL(T.FAILQTY,0))*100,2)YIELD,"
                //    + " ROUND(NVL(T.FAILQTY, 0) / (S.PASSQTY + NVL(T.FAILQTY, 0)) * 100, 2) DEFECTRATE FROM "
                //    + " (SELECT LINE_CODE LINEID,COMPUTERNAME TESTSTATION,COUNT(*)PASSQTY FROM " + ddlModel.SelectedValue + ".PRODUCT_HISTORY_V "
                //    + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                //    + " AND STATUS = 'P' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE,COMPUTERNAME) S, "
                //    + " (SELECT LINE_CODE LINEID,COMPUTERNAME TESTSTATION,COUNT(*)FAILQTY FROM " + ddlModel.SelectedValue + ".PRODUCT_HISTORY_V "
                //    + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                //    + " AND STATUS = 'F' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE,COMPUTERNAME ) T "
                //    + " WHERE S.LINEID = T.LINEID(+) AND S.TESTSTATION = T.TESTSTATION(+)  order by S.LINEID,S.TESTSTATION";
                //by litao add 2011/4/28
                strSql = "SELECT S.LINEID,S.TESTSTATION,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY,(S.PASSQTY+NVL(T.FAILQTY,0))TOTALQTY,ROUND(S.PASSQTY/(S.PASSQTY+NVL(T.FAILQTY,0))*100,2)YIELD,"
                    + " ROUND(NVL(T.FAILQTY, 0) / (S.PASSQTY + NVL(T.FAILQTY, 0)) * 100, 2) DEFECTRATE FROM "
                    + " (SELECT LINE_CODE LINEID,COMPUTERNAME TESTSTATION,COUNT(*)PASSQTY FROM " + ddlModel.SelectedValue + ".PRODUCT_HISTORY_V "
                    + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'P' AND NVL(UPPER(EMPLOYEE),'NULL') <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE,COMPUTERNAME) S, "
                    + " (SELECT LINE_CODE LINEID,COMPUTERNAME TESTSTATION,COUNT(*)FAILQTY FROM " + ddlModel.SelectedValue + ".PRODUCT_HISTORY_V "
                    + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'F' AND NVL(UPPER(EMPLOYEE),'NULL') <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE,COMPUTERNAME ) T "
                    + " WHERE S.LINEID = T.LINEID(+) AND NVL(S.TESTSTATION,'NULL') = NVL(T.TESTSTATION(+),'NULL')  order by S.LINEID,S.TESTSTATION";

            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            dgMachineRate.DataSource = dt.DefaultView;
            dgMachineRate.DataBind();

            //if the station is not empty,draw the chart
            if (dt.Rows.Count > 0)
            {
                if (!strStation.Equals(""))
                {
                    DBTable dbdt = new DBTable(dt);
                    string[] XLabel = dbdt.getColAsString(1);
                    double[] XValues = dbdt.getCol(5);
                    clsDBTopNDefect.createChart(wcvMachineRate, XLabel, XValues, strStationDesc + " Machines Rate(%)");
                    wcvMachineRate.Visible = true;
                }
            }
        }

        private void dgMachineRate_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgMachineRate.CurrentPageIndex = e.NewPageIndex;
            GetData();
        }

        private void dgSMTLineRate_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgSMTLineRate.CurrentPageIndex = e.NewPageIndex;
            GetLineRate(1, dgSMTLineRate);
        }

        private void dgAssyLineRate_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgAssyLineRate.CurrentPageIndex = e.NewPageIndex;
            GetLineRate(2, dgAssyLineRate);
        }

        /// <summary>
        /// Get Line Rate
        /// </summary>
        /// <param name="PhaseType">1:SMT;2:Assembly</param>
        private void GetLineRate(int PhaseType, DataGrid dg)
        {
            string strStartDate = tbStartDate.DateTextBox.Text.Trim();
            string strEndDate = tbEndDate.DateTextBox.Text.Trim();
            string strModel = ddlModel.SelectedValue.Trim().ToUpper();

            string strTemp = "";
            WebChartViewer wcv = null;
            switch (PhaseType)
            {
                case 1:
                    strTemp = " AND STATION_CODE IN ('DL', 'CA', 'PT', 'B1', 'B2', 'B3', 'BT', 'BTWL') ";
                    wcv = wcvSMTLineRate;
                    break;
                case 2:
                    strTemp = " AND STATION_CODE IN ('WL', 'D2', 'A1', 'A2', 'A3', 'A4', 'A5','E2') ";
                    wcv = wcvAssyLineRate;
                    break;
            }
            string strSql = "SELECT LINE_CODE LINEID,PASSQTY,FAILQTY,(PASSQTY+FAILQTY)TOTALQTY,ROUND(PASSQTY/(PASSQTY+FAILQTY)*100,2) YIELD FROM ( "
                + " SELECT S.LINE_CODE,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY FROM ( "
                + " SELECT LINE_CODE,COUNT(*)PASSQTY FROM " + strModel + ".PRODUCT_HISTORY_V  WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                + " AND STATUS = 'P' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE) S, "
                + " (SELECT LINE_CODE,COUNT(*)FAILQTY FROM " + strModel + ".PRODUCT_HISTORY_V WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND  TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                + " AND STATUS = 'F' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE) T "
                + " WHERE S.LINE_CODE = T.LINE_CODE(+)) ";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            dg.DataSource = dt.DefaultView;
            dg.DataBind();

            //if the station is not empty,draw the chart
            if (dt.Rows.Count > 0)
            {
                DBTable dbdt = new DBTable(dt);
                string[] XLabel = dbdt.getColAsString(0);
                double[] XValues = dbdt.getCol(4);
                clsDBTopNDefect.createChart(wcv, XLabel, XValues, "Lines Rate(%)");
                wcv.Visible = true;
            }
        }
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
                    //string strSql = "SELECT distinct STATION_CODE FROM " + strModel + ".PRODUCT_HISTORY_V order by station_code";   delete by wh in20100811  because 根據幾種獲取站點速度太慢，對數據庫壓力過大
                    string strSql = "SELECT distinct STATION_CODE FROM " + strModel + ".SFC_STATION_NAME order by station_code";
                    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                    ddlStation.DataTextField = "STATION_CODE";
                    ddlStation.DataValueField = "STATION_CODE";
                    ddlStation.DataSource = dt.DefaultView;
                    ddlStation.DataBind();
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請確認是否存在此幾種！！');</script>");
                    return;
                }
            }
        }
}
}

