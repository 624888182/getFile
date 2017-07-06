/*************************************************************************
 * 
 *  Unit description: Repair Wip
 *  Developer: Shu Jian Bo             Date: 2007/05/29
 *  Modifier : Shu Jian Bo             Date: 2007/05/30
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

	/// <summary>
	///		WFrmRepairWip 的摘要描述。
	/// </summary>
	public partial class WFrmRepairWip : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				InitialDDL();
				MultiLanaguage();
				tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:00";
				tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
				if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text)>0)
					tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
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
			this.dgRepairWip.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgRepairWip_PageIndexChanged);

		}
		#endregion

		private void InitialDDL()
		{
			string strSql = "SELECT STATION_ID,DESCRIPTION FROM MES_COMM_STATION_DESCRIPTION T WHERE TESTSTATION = 'Y' ORDER BY STATION_ID";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			ddlStationID.DataTextField = "DESCRIPTION";
			ddlStationID.DataValueField = "STATION_ID";
			ddlStationID.DataSource = dt.DefaultView;
			ddlStationID.DataBind();

			ddlStationID.Items.Insert(0,"");

			strSql = "SELECT DB_USER MODEL FROM CMCS_SFC_MODEL ORDER BY MODEL";
			dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			ddlModel.DataTextField = "MODEL";
			ddlModel.DataValueField = "MODEL";
			ddlModel.DataSource = dt.DefaultView;
			ddlModel.DataBind();

			ddlModel.Items.Insert(0,"");
		}

		private void MultiLanaguage()
		{
			lblProductID.Text = (String)GetGlobalResourceObject("SFCQuery","ProductID");
			lblStationID.Text = (String)GetGlobalResourceObject("SFCQuery","StationID");
			lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateFrom");
			lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateTo");
			lblLine.Text = (String)GetGlobalResourceObject("SFCQuery","Line");
			lblModel.Text = (String)GetGlobalResourceObject("SFCQuery","Model");
			btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery","Query");
			Label28.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			Label29.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			dgRepairWip.CurrentPageIndex = 0;
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
			GetData();
		}

		private void GetData()
		{
			string strSql = "SELECT * FROM MES_REPAIR_WIP T ";
			string strWhere = "";
			if (!tbProductID.Text.Trim().Equals(""))
			{
				strWhere = strWhere + " AND PRODUCT_ID LIKE "+ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper()+"%");
			}

			if (!ddlStationID.SelectedIndex.Equals(0))
				strWhere = strWhere + " AND STATION_CODE = "+ ClsCommon.GetSqlString(ddlStationID.SelectedValue);

			if (!ddlLine.SelectedIndex.Equals(0))
				strWhere = strWhere + " AND FACTORY_AREA = "+ClsCommon.GetSqlString(ddlLine.SelectedValue);

			if (!ddlModel.SelectedIndex.Equals(0))
				strWhere = strWhere + " AND MODEL_ID = "+ClsCommon.GetSqlString(ddlModel.SelectedValue);

			if (!tbStartDate.DateTextBox.Text.Trim().Equals(""))
			{
				strWhere = strWhere + " AND CREATION_DATE BETWEEN TO_DATE("+ClsCommon.GetSqlString(tbStartDate.DateTextBox.Text.Trim())+",'YYYY/MM/DD HH24:MI') AND TO_DATE("
					+ClsCommon.GetSqlString(tbEndDate.DateTextBox.Text.Trim())+",'YYYY/MM/DD HH24:MI') ";
			}

			strSql = strSql + " WHERE " + strWhere.Substring(4) + " ORDER BY MODEL_ID";

			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			dgRepairWip.DataSource = dt.DefaultView;
			dgRepairWip.DataBind();
			Label4.Text = "Current Page:"+(dgRepairWip.CurrentPageIndex+1).ToString()+"/"+dgRepairWip.PageCount.ToString()+" Total:"+dt.Rows.Count.ToString();
		}
		private void dgRepairWip_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgRepairWip.CurrentPageIndex = e.NewPageIndex;
			GetData();
		}
	}
}
