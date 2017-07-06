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
	using System.Globalization;
	using DBAccess.EAI;

	/// <summary>
	///		WFrmPanelInfoQuery 的摘要描述。
	/// </summary>
	public partial class WFrmPanelInfoQuery : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
                MultiLanguage();
            }
            string strPanelID = "";
            try
            {
                strPanelID = Request.QueryString["PanelID"].ToString();
            }
            catch
            {
                //不做任何操作
            }
            if (!strPanelID.Equals(""))
            {
                lblPanelID.Visible = false;
                tbPanelID.Visible = false;
                btnQuery.Visible = false;
                hr1.Visible = false;
                btnQuery_Click(null, null);
            }
            else
            {
                lblPanelID.Visible = true;
                tbPanelID.Visible = true;
                btnQuery.Visible = true;
                hr1.Visible = true;
            }
			
			// 在這裡放置使用者程式碼以初始化網頁
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
            lblPanelID.Text = (String)GetGlobalResourceObject("SFCQuery", "PanelID");
            lblWO.Text = (String)GetGlobalResourceObject("SFCQuery", "WO");
            lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
            lblItem.Text = (String)GetGlobalResourceObject("SFCQuery", "Item");
            lblWODate.Text = (String)GetGlobalResourceObject("SFCQuery", "WODate");
            lblBOMVer.Text = (String)GetGlobalResourceObject("SFCQuery", "BOMVer");
            lblSerialQty.Text = (String)GetGlobalResourceObject("SFCQuery", "SerialQty");
            lblPrintDate.Text = (String)GetGlobalResourceObject("SFCQuery", "PrintDate");
            lblProductID.Text = (String)GetGlobalResourceObject("SFCQuery", "ProductID");
            lblBaseInfo.Text = (String)GetGlobalResourceObject("SFCQuery", "BaseInfo");
            lblRepairInfo.Text = (String)GetGlobalResourceObject("SFCQuery", "RepairInfo");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
            ViewState["ErrorMsg"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorMsg");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{

            string strPanelID = "";
            try
            {
                strPanelID = "A"+Request.QueryString["PanelID"].ToString();
            }
            catch
            {
                //不做任何操作
            }

            if (strPanelID.Equals(""))
            {
                strPanelID = tbPanelID.Text.Trim().ToUpper();
            }

			if (strPanelID.Equals(""))
			{
				lblErrorMsg.Text = "Panel ID is Empty!";
				lblErrorMsg.Visible = true;
				Panel1.Visible = false;
				return;
			}
			lblErrorMsg.Visible = false;
			Panel1.Visible = true;
			GetBaseInfo(strPanelID.Substring(1));
			GetDefectInfo(strPanelID);
            GetHistoryInfo(strPanelID.Substring(1));
		}

		private void GetBaseInfo(string strPanelID)
		{
			string strSql = "SELECT A.SORDER,A.MODEL,A.SPART,TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') WODATE,A.BOM_VER,B.SEQUENCE_ID FROM CMCS_SFC_SORDER A,MES_PCBA_PANEL_DETAIL B "
				+" WHERE A.SORDER=B.WO_NO AND B.PANEL_ID="+ClsCommon.GetSqlString(strPanelID);
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			if (dt.Rows.Count>0)
			{
				lblWOValue.Text = dt.Rows[0]["SORDER"].ToString();
				lblModelValue.Text = dt.Rows[0]["MODEL"].ToString();
				lblItemValue.Text = dt.Rows[0]["SPART"].ToString();
				lblWODateValue.Text = dt.Rows[0]["WODATE"].ToString();
				lblBOMVerValue.Text = dt.Rows[0]["BOM_VER"].ToString();
				lblSerialQtyValue.Text = dt.Rows[0]["SEQUENCE_ID"].ToString();
				lblPrintDateValue.Text = "";//dt.Rows[0]["PRINTDATE"].ToString();
				strSql = "SELECT PRODUCT_ID FROM MES_PCBA_PANEL_LINK WHERE PANEL_ID = "+ClsCommon.GetSqlString(strPanelID)+" ORDER BY PRODUCT_ID";
				dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
				string strTemp = "";
				string strProductid="";
				//int i = 1;
				foreach(DataRow dr in dt.Rows)
				{
					strProductid = dr["PRODUCT_ID"].ToString();
					string tt = "<script language='jscript'>function "+strProductid+"(){var res = window.showModalDialog('./WFrmStationDetail.aspx?PID="+strProductid+"&MyDate=' + Date(), '','dialogWidth:450px; dialogHeight:400px; center:yes; scroll:1;"
					+"status:no;help:no');}</script>";
					Page.ClientScript.RegisterStartupScript(this.GetType(),strProductid,tt);
					strTemp = strTemp +"<a onclick=\""+strProductid+"()\" href='#'>"+ dr["PRODUCT_ID"].ToString()+"</a> <br>";
//					if (i%2==0)
//						strTemp = "<a onclick=\"javasrcrip:alert('OK');\" href='#'>"+strTemp+"</a> <br>";
//					i++;
				}
				lblProductIDValue.Text = strTemp;
			}
			else
			{
				lblErrorMsg.Visible = true;
				Panel1.Visible = false;
				lblErrorMsg.Text = ViewState["ErrorMsg"].ToString();
			}
		}

		private void GetDefectInfo(string strPanelID)
		{
			string strSql = "SELECT * FROM MES_PCBA_PANEL_ERROR WHERE PANEL_ID = "+ClsCommon.GetSqlString(strPanelID);
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			dgDefectDtail.DataSource = dt.DefaultView;
			dgDefectDtail.DataBind();
		}

		private void GetHistoryInfo(string strPanelID)
		{
            string strSql = "SELECT * FROM MES_PCBA_PANEL_HISTORY WHERE (PANEL_ID = " + ClsCommon.GetSqlString("A" + strPanelID) + " OR PANEL_ID = " + ClsCommon.GetSqlString("B" + strPanelID) + ") ORDER BY CREATION_DATE";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			dgHistory.DataSource = dt.DefaultView;
			dgHistory.DataBind();
		}
	}
}
