/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/09/11
 *  Modifier : Shu Jian Bo             Date: 2007/09/12
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
	using System.Resources;
	using System.Reflection;
	using DBAccess.EAI;
	using DB.EAI;
	using ChartDirector;

	/// <summary>
	///		WFrmLineRate 的摘要描述。
	/// </summary>
	public partial class WFrmLineRate : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				tbStartDate.DateTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")+" 08:20";
				tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:20";
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
			this.dgLineRate.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgLineRate_PageIndexChanged);

		}
		#endregion

		private void InitialDDL()
		{
			string strSql = "SELECT MODEL FROM CMCS_SFC_MODEL ORDER BY MODEL";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			ddlModel.DataTextField = "MODEL";
			ddlModel.DataValueField = "MODEL";
			ddlModel.DataSource = dt.DefaultView;
			ddlModel.DataBind();
		}

		private void MultiLanaguage()
		{			
			//lblMonth.Text = (String)GetGlobalResourceObject("SFCQuery","Month");
			lblModel.Text = (String)GetGlobalResourceObject("SFCQuery","Model");
			btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery","Query");
			Label28.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			Label29.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateFrom");
			lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateTo");

			dgLineRate.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Line");
			dgLineRate.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","PassQty");
			dgLineRate.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","FailQty");
			dgLineRate.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","TotalQty");
			dgLineRate.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","LinesYield");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			dgLineRate.CurrentPageIndex = 0 ;
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
			wcvLineRate.Visible = false;
			GetLineRate();
		}

		private void GetLineRate()
		{
			string strStartDate = tbStartDate.DateTextBox.Text.Trim();
			string strEndDate = tbEndDate.DateTextBox.Text.Trim();
			string strModel = ddlModel.SelectedValue.Trim().ToUpper();

			string strSql = "SELECT LINE_CODE LINEID,PASSQTY,FAILQTY,(PASSQTY+FAILQTY)TOTALQTY,ROUND(PASSQTY/(PASSQTY+FAILQTY)*100,2) YIELD FROM ( "
				+" SELECT S.LINE_CODE,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY FROM ( "
				+" SELECT LINE_CODE,COUNT(*)PASSQTY FROM "+strModel+".PRODUCT_HISTORY_V  WHERE PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
				+" AND STATUS = 'P' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 GROUP BY LINE_CODE) S, "
				+" (SELECT LINE_CODE,COUNT(*)FAILQTY FROM "+strModel+".PRODUCT_HISTORY_V WHERE PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND  TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
				+" AND STATUS = 'F' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 GROUP BY LINE_CODE) T "
				+" WHERE S.LINE_CODE = T.LINE_CODE(+)) ";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			dgLineRate.DataSource = dt.DefaultView;
			dgLineRate.DataBind();

			//if the station is not empty,draw the chart
			if(dt.Rows.Count>0)
			{
				DBTable dbdt = new DBTable(dt);
				string[] XLabel = dbdt.getColAsString(0);
				double[] XValues = dbdt.getCol(4);			
				clsDBTopNDefect.createChart(wcvLineRate,XLabel,XValues,"Lines Rate(%)");
				wcvLineRate.Visible = true;
			}
		}

		private void dgLineRate_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgLineRate.CurrentPageIndex = e.NewPageIndex;
			GetLineRate();
		}
	}
}
