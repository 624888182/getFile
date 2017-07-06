using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;

namespace SFCQuery.Boundary
{
	/// <summary>
	/// WFrmDateCodeToImei 的摘要描述。
	/// </summary>
	public partial class WFrmDateCodeToImei : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            string strRcvNo = Request.QueryString["RcvNo"].ToString();
            string strWONO = Request.QueryString["WONO"].ToString();
            string strModel = Request.QueryString["MODEL"].ToString();
            string strLine = Request.QueryString["Line"].ToString();
            string strDiskNo = Request.QueryString["DISKNO"].ToString();
            string strStation = Request.QueryString["Station"].ToString();
            string strInDate = Request.QueryString["INDATE"].ToString();
            string strOutDate = Request.QueryString["OUTDATE"].ToString();

            string strsql = "SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE  TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND T.PRODUCT_ID NOT IN  V.PRODUCTID";
            strsql = strsql + "UNION ALL";
            strsql = strsql + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            dgRcvNoDetail.DataSource = dt1.DefaultView;
            dgRcvNoDetail.DataBind(); 

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
		/// 此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
