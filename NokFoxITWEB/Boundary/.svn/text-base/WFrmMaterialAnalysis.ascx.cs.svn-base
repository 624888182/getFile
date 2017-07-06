/*************************************************************************
 * 
 *  Unit description: Material Analysis
 *  Developer: Shu Jian Bo             Date: 
 *  Modifier : Shu Jian Bo             Date: 
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

	/// <summary>
	///		WFrmMaterialAnalysis 的摘要描述。
	/// </summary>
	public partial class WFrmMaterialAnalysis : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				MultiLanguage();
				BindLine();
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

		}
		#endregion

		private void MultiLanguage()
		{
            lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
            lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");
            lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");
            lblMaterialNO.Text = (String)GetGlobalResourceObject("SFCQuery", "MaterialNO");
            lblFactory.Text = (String)GetGlobalResourceObject("SFCQuery", "Factory");
            lblMONumber.Text = (String)GetGlobalResourceObject("SFCQuery", "WO");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
            ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
			//ViewState["WONotExist"] = rm.GetString("WONotExist");
		}

		private void BindLine()
		{
			string StrSql = "SELECT DISTINCT LINE_NAME FROM K_SEND_LINE WHERE FACTORY_AREA = "+ClsCommon.GetSqlString(ddlFactory.SelectedValue)+" ORDER BY LINE_NAME";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
			ddlLine.DataTextField = "LINE_NAME";
			ddlLine.DataValueField = "LINE_NAME";
			ddlLine.DataSource = dt.DefaultView;
			ddlLine.DataBind();
			ddlLine.Items.Insert(0,"");

			StrSql = "SELECT DISTINCT MO_NUMBER FROM K_KITTING_WO WHERE FACTORY_AREA = "+ClsCommon.GetSqlString(ddlFactory.SelectedValue)+" ORDER BY MO_NUMBER";
			dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
			ddlMONumber.DataTextField = "MO_NUMBER";
			ddlMONumber.DataValueField = "MO_NUMBER";
			ddlMONumber.DataSource = dt.DefaultView;
			ddlMONumber.DataBind();
			ddlMONumber.Items.Insert(0,"");
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

			string strSql = "SELECT * FROM K_CHECK_HISTORY WHERE FACTORY_AREA ="+ClsCommon.GetSqlString(ddlFactory.SelectedValue)+" AND CREATE_TIME BETWEEN TO_DATE("
				+ClsCommon.GetSqlString(tbStartDate.DateTextBox.Text.Trim()) +",'YYYY/MM/DD HH24:MI') AND TO_DATE("+ClsCommon.GetSqlString(tbEndDate.DateTextBox.Text.Trim())+",'YYYY/MM/DD HH24:MI')";
			if (!ddlLine.SelectedValue.Equals(""))
				strSql = strSql + " AND LINE_NAME = "+ClsCommon.GetSqlString(ddlLine.SelectedValue);
			if (!ddlMONumber.SelectedValue.Equals(""))
			    strSql =strSql +" AND MO_NUMBER ="+ClsCommon.GetSqlString(ddlMONumber.SelectedValue);
			if (!tbMaterialNO.Text.Trim().Equals(""))
				strSql = strSql +" AND UPPER(KEY_PART_NO) LIKE "+ClsCommon.GetSqlString("%"+tbMaterialNO.Text.Trim().ToUpper()+"%");
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			dgMaterial.DataSource = dt.DefaultView;
			dgMaterial.DataBind();
		}

		protected void ddlFactory_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindLine();
		}
	}
}
