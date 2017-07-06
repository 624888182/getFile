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
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace SFCQuery.Boundary
{
	/// <summary>
	/// Summary description for WFrmPIDDetail
	/// </summary>
	public partial class WFrmPIDDetail1 : Localized
	{	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here			
			if (!IsPostBack)
			{
				dgPIDDetail.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","WO");
				dgPIDDetail.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","StationID");
				dgPIDDetail.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","StateID");
				dgPIDDetail.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","CDate");

				//BindList();
				binddata();
			}
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
			this.dgPIDDetail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPIDDetail_ItemCommand);

		}
		#endregion


		private void binddata()
		{
			string strRegion = Request.QueryString["Region"].ToString();
			string strModel = Request.QueryString["Model"].ToString();
			string strWO = Request.QueryString["WO"].ToString();
			string strSD = Request.QueryString["SD"].ToString();
			string strED = Request.QueryString["ED"].ToString();
			string strStationID = Request.QueryString["StationID"].ToString();
            string strLineID = Request.QueryString["LineID"].ToString();
            string strStatus = Request.QueryString["Status"].ToString();
			
		    string strTemp = "";
			string strTemp1 = "";
			switch(strRegion.ToUpper())
			{
				case "PCBA" :
					strTemp = "MES_PCBA_HISTORY";
					strTemp1 = "STATION_CODE STATION_ID";
					break;
				case "ASSEMBLY" :
					strTemp = "MES_ASSY_HISTORY";
					strTemp1 = "STATION_CODE STATION_ID";
					break;
				case "PACKING" :
					strTemp = "MES_PACK_HISTORY";
					strTemp1 = "DECODE(STATION_CODE,'E2','P_BT',STATION_CODE)STATION_ID";
					break;
			}
            string StrSql = "SELECT WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,MAX(CREATION_DATE) CREATION_DATE,EMP_ID FROM " + strTemp + " a WHERE ";
			if (!strWO.Equals(""))
				StrSql = StrSql + " WO_NO="+ClsCommon.GetSqlString(strWO);
            else if (!strModel.Equals(""))
            {
                if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "DVL" || strModel == "TWN")
                    StrSql = StrSql + " PRODUCT_ID IN (SELECT  S.PRODUCT_ID FROM Sfc.MES_PCBA_PANEL_DETAIL R,Sfc.MES_PCBA_PANEL_LINK S,SHP.CMCS_SFC_SORDER T WHERE S.PANEL_ID=R.PANEL_ID AND R.WO_NO=T.SORDER AND T.MODEL =" + ClsCommon.GetSqlString(strModel) + ")";
                else
                    StrSql = StrSql + " PRODUCT_ID LIKE " + ClsCommon.GetSqlString(strModel + "%");
            }

            if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "DVL" || strModel == "TWN")
            {
                StrSql = StrSql + " AND SEQUENCE_ID=(SELECT MAX(SEQUENCE_ID) FROM " + strTemp + " WHERE PRODUCT_ID=a.PRODUCT_ID AND STATION_ID=a.STATION_ID) "
                       + " AND STATION_ID='" + strStationID
                       + "' AND STATE_ID='" + strStatus
                       + "' AND CREATION_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strSD) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(strED) + ",'yyyy/mm/dd hh24:mi') "
                       + " GROUP BY  WO_NO,PRODUCT_ID,STATION_ID,STATE_ID, EMP_ID ";
                StrSql = StrSql + "UNION SELECT WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATEION_DATE,EMP_ID  from ( "
                    + " SELECT B.SORDER WO_NO,A.PRODUCT_ID," + strTemp1 + ","
                    + " STATUS STATE_ID,PDDATE CREATEION_DATE,EMPLOYEE EMP_ID ,ROW_NUMBER() OVER(PARTITION BY A.PRODUCT_ID,A.STATION_CODE ORDER BY PDDATE DESC) RN  FROM ";
            }
            else
            {
                StrSql = StrSql + " AND SEQUENCE_ID=(SELECT MAX(SEQUENCE_ID) FROM " + strTemp + " WHERE PRODUCT_ID=a.PRODUCT_ID AND STATION_ID=a.STATION_ID) "
                      + " AND STATION_ID='" + strStationID + "'  AND STATE_ID='" + strStatus
                      + "' AND CREATION_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strSD) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(strED) + ",'yyyy/mm/dd hh24:mi') "
                      + " GROUP BY  WO_NO,PRODUCT_ID,STATION_ID,STATE_ID, EMP_ID ";

                StrSql = StrSql + "UNION  SELECT WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATEION_DATE,EMP_ID  from (  "
                    + " SELECT WORK_ORDER WO_NO,PRODUCT_ID," + strTemp1 + ","
                    + " STATUS STATE_ID,PDDATE CREATEION_DATE,EMPLOYEE EMP_ID,ROW_NUMBER() OVER(PARTITION BY A.PRODUCT_ID,A.STATION_CODE ORDER BY PDDATE DESC) RN  FROM ";
            }
			if (!strWO.Equals(""))
				StrSql = StrSql + GetPN(strWO).Substring(2,3)+".PRODUCT_HISTORY_V A  WHERE PRODUCT_ID LIKE  "+ClsCommon.GetSqlString(GetPN(strWO).Substring(2,3)+"%") ;
            else if (!strModel.Equals(""))
            {
                if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "DVL" || strModel == "TWN")
                {
                    StrSql = StrSql + strModel + ".PRODUCT_HISTORY_V A, Sfc.PRODUCT_PANEL_SORDER B  WHERE A.PRODUCT_ID=B.PRODUCT_ID AND B.MODEL =" + ClsCommon.GetSqlString(strModel);
                    StrSql = StrSql + " AND A.STATION_CODE ='" + strStationID
                        + "' AND A.STATUS =  '" + strStatus
                        + "' AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strSD) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(strED) + ",'yyyy/mm/dd hh24:mi') ) where RN=1  ";
                }
                else
                {
                    StrSql = StrSql + strModel + ".PRODUCT_HISTORY_V A  WHERE PRODUCT_ID LIKE  " + ClsCommon.GetSqlString(strModel + "%");
                    StrSql = StrSql + " AND STATION_CODE = '" + strStationID + "' AND STATUS =  '" + strStatus
                        + "' AND UPPER(LINE_CODE) = '" + strLineID+ "' AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strSD) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(strED) + ",'yyyy/mm/dd hh24:mi') ) where 1=1 ";
       
                }
            } 
			 
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
			dgPIDDetail.DataSource = dt.DefaultView;
			dgPIDDetail.DataBind();
			dt.Dispose();
		}

		private string GetStation(string strModel,string strStationID)
		{
			string strStation = "";
            if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "DVL" || strModel == "TWN")
                //if(strStationID.Substring(0,1).ToUpper().Equals("S"))
                //    strStation=strStationID.Substring(0, 1) + "_" + strStationID.Substring(2, 2);
                //else
                    strStation = ClsCommon.GetSqlString(strStationID.Substring(0, strStationID.Length - 2).ToUpper());
            else
                if(strStationID.Substring(0,1).ToUpper().Equals("S"))
                    strStation = strStationID.Substring(0, 1) + "_" + strStationID.Substring(2, 2);

            else
                switch (strStationID.Substring(0, 1) + "_" + strStationID.Substring(2, 2))
                {
                    case "T_BT":
                        strStation = "DL";
                        break;
                    case "T_CT":
                        strStation = "B1";
                        break;
                    case "T_DT":
                        strStation = "BT";
                        break;
                    case "T_ET":
                        strStation = "CA";
                        break;
                    case "T_JT":
                        strStation = "PT";
                        break;
                    case "T_LT":
                        strStation = "B5";
                        break;
                    case "T_MT":
                        strStation = "BTWL";
                        break;
                    case "T_NT":
                        strStation = "B2";
                        break;
                    case "T_OT":
                        strStation = "B3";
                        break;
                    case "T_PT":
                        strStation = "B4";
                        break;
                    case "A_CT":
                        strStation = "A2";
                        break;
                    case "A_DT":
                        strStation = "WL";
                        break;
                    case "A_ET":
                        strStation = "BTWL";
                        break;
                    case "A_HT":
                        strStation = "A3";
                        break;
                    case "A_IT":
                        strStation = "A4";
                        break;
                    case "A_JT":
                        strStation = "A5";
                        break;
                    case "A_KT":
                        strStation = "A1";
                        break;
                    case "A_LT":
                        strStation = "D2";
                        break;
                    case "P_BT":
                        strStation = "E2";
                        break;
                    case "P_GO":
                        strStation ="OQC";
                        break;
                    case "P_DT":
                        strStation ="PACK";
                        break;
                    case "P_EO":
                        strStation ="OOB";
                        break;
                    case "P_FI":
                        strStation = "WH";
                        break;
                }
			return strStation;
		}

		private string GetPN(string WO_NO) 
		{
			string StrSql = "";
			string strRegion = Request.QueryString["Region"].ToString();
			switch(strRegion)
			{
				case "PCBA":	
					StrSql = "SELECT SPART FROM CMCS_SFC_SORDER WHERE SORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
				case "ASSEMBLY":
					StrSql = "SELECT APART FROM CMCS_SFC_AORDER WHERE AORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
				case "PACKING":
					StrSql = "SELECT PPART FROM CMCS_SFC_PORDER WHERE PORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
			}
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
			if (dt.Rows.Count>0)
			{
				return dt.Rows[0][0].ToString();			
			}
			else
			{
				return "";
			}

		}

		private void BindList()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("WO_NO");
			dt.Columns.Add("Product_ID");
			dt.Columns.Add("Station_ID");
			dt.Columns.Add("State_ID");
			dt.Columns.Add("Creation_Date");

			dgPIDDetail.DataSource = dt.DefaultView;
			dgPIDDetail.DataBind();
		}

		private void dgPIDDetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.ToUpper()=="SET")
			{
				if(dgPIDDetail.CurrentStatus=="SET")
				{
					BindList();
				}
				else if(dgPIDDetail.CurrentStatus=="")
				{
					binddata();
					
				}
			}
			else 
			{
				binddata();
			}	

			if (e.CommandName == "PID")
			{
				string strProdurct = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text;
				string strScript = "<script language='jscript'>var res = window.showModalDialog('./WFrmStationDetail.aspx?PID="+strProdurct+"&MyDate=' + Date(), '','dialogWidth:450px; dialogHeight:400px; center:yes; scroll:1;"
					+"status:no;help:no');</script>" ; 
				Page.ClientScript.RegisterStartupScript(this.GetType(),"ShowStationElem", strScript);	
			}
		}
	}
}
