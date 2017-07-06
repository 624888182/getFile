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
	/// WFrmDateCodeToImei ���K�n�y�z�C
	/// </summary>
	public partial class WFrmDateCodeToImei : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
            //string strDateCode = Request.QueryString["DateCode"].ToString(); 
            //string strLine = Request.QueryString["Line"].ToString();
            //string strTrack = Request.QueryString["Track"].ToString();
            //string strEmployee = Request.QueryString["Employee"].ToString();
            //string strWONO = Request.QueryString["WONO"].ToString(); 
            string strDiskNo = Request.QueryString["DISKNO"].ToString();
            string strStation= Request.QueryString["Station"].ToString(); 

            string strsql;
            strsql = "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') INDATE,TRACK_NO FROM Sfc.TRACK_COMP_DATECODE WHERE DISKNO=" + "'" + strDiskNo + "'";
            DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            string strInDate = dt0.Rows[0]["INDATE"].ToString();

            //string strSql = "SELECT MIN(A.CREATION_DATE) OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = '" + strLine + "' GROUP BY A.MACHINE_NAME,A.TRACK_NO";
            string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = 'ES07'  GROUP BY A.MACHINE_NAME,A.TRACK_NO";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            string strOutDate = dt.Rows[0]["OUTDATE"].ToString();

            string strsql1 = "SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE  TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND T.PRODUCT_ID NOT IN  V.PRODUCTID";
            strsql1 = strsql1 + " UNION ALL ";
            strsql1 = strsql1 + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            dgDateCodeDetail.DataSource = dt1.DefaultView;
            dgDateCodeDetail.DataBind(); 
		}

		#region Web Form �]�p�u�㲣�ͪ��{���X
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: ���� ASP.NET Web Form �]�p�u��һݪ��I�s�C
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����]�p�u��䴩�ҥ�������k - �ФŨϥε{���X�s�边�ק�
		/// �o�Ӥ�k�����e�C
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
