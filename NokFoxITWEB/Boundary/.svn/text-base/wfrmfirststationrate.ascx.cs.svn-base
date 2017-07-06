namespace SFCQuery.Boundary
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using ChartDirector;
	using DBAccess.EAI;
	using DB.EAI;
	using System.Reflection;
	using System.Resources;

	/// <summary>
	///		WFrmFirstLineRate 的摘要描述。
	/// </summary>
	public partial class WFrmFirststationRate : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				tbStartDate.DateTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")+" 08:20";
				tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:20";
				InitialModel();
                InitialLine();
                InitialStation();
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
			this.dgFirstLineRate.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgFirstLineRate_PageIndexChanged);

		}
		#endregion
       
		private void InitialModel()
		{
			string strSql = "SELECT MODEL FROM CMCS_SFC_MODEL ORDER BY MODEL";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			ddlModel.DataTextField = "MODEL";
			ddlModel.DataValueField = "MODEL";
			ddlModel.DataSource = dt.DefaultView;
			ddlModel.DataBind();
		}
        private void InitialLine()
        {
            string strSql = "SELECT LINE_NAME,LINE_DESC FROM MCMSMO.C_LINE_DESC_T";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            ddlLine.DataTextField = "LINE_DESC";
            ddlLine.DataValueField = "LINE_NAME";
            ddlLine.DataSource = dt.DefaultView;
            ddlLine.DataBind();
        }
        private void InitialStation()
		{
			string strSql = "SELECT STATION_ID,DESCRIPTION FROM MES_COMM_STATION_DESCRIPTION";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            ddlStation.DataTextField = "DESCRIPTION";
            ddlStation.DataValueField = "STATION_ID";
            ddlStation.DataSource = dt.DefaultView;
            ddlStation.DataBind();
		}
		private void MultiLanaguage()
		{
            lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");
            lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
			lblStation.Text = (String)GetGlobalResourceObject("SFCQuery","StationID");
			btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery","Query");
			Label28.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			Label29.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateFrom");
			lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateTo");

			dgFirstLineRate.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Line");
			dgFirstLineRate.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","PassQty");
			dgFirstLineRate.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","FailQty");
			dgFirstLineRate.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","TotalQty");
			dgFirstLineRate.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","LinesYield");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			dgFirstLineRate.CurrentPageIndex = 0 ;
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

            System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
            if (intday.TotalDays > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於24小時！');</script>");
                return;
            }

			Label28.Visible = false;
			Label29.Visible = false;
			wcvFirstLineRate.Visible = false;
			GetFirstLineRate(RadioButtonList1.SelectedIndex);
		}

		private void dgFirstLineRate_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgFirstLineRate.CurrentPageIndex = e.NewPageIndex;
            GetFirstLineRate(RadioButtonList1.SelectedIndex);
		}

		private void GetFirstLineRate(int type)
		{
			string strStartDate = tbStartDate.DateTextBox.Text.Trim();
			string strEndDate = tbEndDate.DateTextBox.Text.Trim();
			string strModel = ddlStation.SelectedValue.Trim().ToUpper();

			string strSql = "SELECT LINEID,FAILQTY,PASSQTY,FAILQTY+PASSQTY TOTALQTY,ROUND(PASSQTY/(FAILQTY+PASSQTY)*100,2)YIELD FROM( "
				+" SELECT LINEID,SUM(DECODE(STATUS,'F',QTY,0))FAILQTY,SUM(DECODE(STATUS,'P',QTY,0))PASSQTY FROM "
				+" (SELECT /*+RULE*/LINEID,STATUS,COUNT(*)QTY FROM(SELECT LINE_CODE LINEID,STATUS,PRODUCT_ID "
				+" FROM "+strModel+".PRODUCT_HISTORY_V S WHERE STATION_CODE IN ";
			if(type.Equals(0)) strSql = strSql +" ('DL', 'CA', 'PT', 'B1', 'B2', 'B3', 'BT', 'BTWL') ";
			else strSql = strSql + "('A1','A2','A3','A4','A5','WL','D2','E2') ";
			strSql =strSql +" AND PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI:SS') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI:SS') "
				+" AND PDDATE =(SELECT MIN(PDDATE) FROM "+strModel+".PRODUCT_HISTORY_V T WHERE S.PRODUCT_ID = T.PRODUCT_ID "
				+" AND PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI:SS') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI:SS') "
				+" AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0) )GROUP BY LINEID,STATUS   ) GROUP BY LINEID)";
//			string strSql = "SELECT LINE_CODE LINEID,PASSQTY,FAILQTY,(PASSQTY+FAILQTY)TOTALQTY,ROUND(PASSQTY/(PASSQTY+FAILQTY)*100,2) YIELD FROM ( "
//				+" SELECT S.LINE_CODE,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY FROM ( "
//				+" SELECT LINE_CODE,COUNT(*)PASSQTY FROM "+strModel+".PRODUCT_HISTORY_V  WHERE PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
//				+" AND STATUS = 'P' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 GROUP BY LINE_CODE) S, "
//				+" (SELECT LINE_CODE,COUNT(*)FAILQTY FROM "+strModel+".PRODUCT_HISTORY_V WHERE PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND  TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
//				+" AND STATUS = 'F' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 GROUP BY LINE_CODE) T "
//				+" WHERE S.LINE_CODE = T.LINE_CODE(+)) ";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			dgFirstLineRate.DataSource = dt.DefaultView;
			dgFirstLineRate.DataBind();

			//if the station is not empty,draw the chart
			if(dt.Rows.Count>0)
			{
				DBTable dbdt = new DBTable(dt);
				string[] XLabel = dbdt.getColAsString(0);
				double[] XValues = dbdt.getCol(4);			
				clsDBTopNDefect.createChart(wcvFirstLineRate,XLabel,XValues,"Lines Rate(%)");
				wcvFirstLineRate.Visible = true;
			}
		}
}
}
