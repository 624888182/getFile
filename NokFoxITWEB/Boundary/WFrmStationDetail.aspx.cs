/*************************************************************************
 * 
 *  Unit description: Show Station Detail
 *  Developer: Shu Jian Bo             Date: 2007/03/20
 *  Modifier : Shu Jian Bo             Date: 2007/11/28
 * 
 * ***********************************************************************/

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
using DB.EAI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Data.OracleClient;

namespace SFCQuery.Boundary
{
	/// <summary>
	/// Summary description for WFrmStationDetail
	/// </summary>
	public partial class WFrmStationDetail : Localized
	{
		private string strProductID = "";
        private string strPanelID = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			dgmainsec.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","WO");
			dgmainsec.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","MainID");
			dgmainsec.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","SecondaryID");

			dgStation.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","CDate");
			dgStation.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","StationID");
			dgStation.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","StateID");
			dgStation.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Line");
			dgStation.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","EmpID");

			strProductID = Request.QueryString["PID"].ToString().ToUpper(); 
			bindmainsec(strProductID);
			binddata(strProductID);
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
			this.dgStation.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgStation_ItemCommand);

		}
		#endregion

		private void binddata(string strProductID)
		{
			ClsDBScanHistory objScanHistory = new ClsDBScanHistory();
			objScanHistory.setProductID(strProductID,"");

            string strProcedureName = "SFCQUERY.GETPROCESSINFO";
            OracleParameter[] oraPara = new OracleParameter[] { new OracleParameter("PRODUCTID",OracleType.VarChar,20),
											new OracleParameter("PANELID",OracleType.VarChar,9),
											new OracleParameter("DATA",OracleType.Cursor)};
            oraPara[0].Value = objScanHistory.FProductID;
            oraPara[1].Value = objScanHistory.FPanelID;
            oraPara[2].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, oraPara).Tables[0];
			//DataTable dt = objScanHistory.GetProcessInfo(objScanHistory.FProductID,objScanHistory.FPanelID);
			dgStation.DataSource = dt.DefaultView;
			dgStation.DataBind();

//			string strTemp = "";
//			if (Session["Lines"].ToString().Equals("") ||Session["Lines"].ToString().Equals("''"))
//			{
//				strTemp = "";
//			}
//			else
//			{
//				strTemp = " and substr(station_id,2,1) in ("+Session["Lines"].ToString()+")";
//			}
//			string StrSql = "SELECT creation_date fcdate,case substr(station_id, 1, 1) || '_' || substr(station_id, 3, 2) "
//				+" when 'A_BI' then 'Assembly Input' when 'A_KT' THEN 'ABaseBand1' when 'A_CT' then 'ABaseBand2' when 'A_HT' then 'ABaseBand3' "
//				+" when 'A_IT' then 'ABaseBand4' when 'A_DT' then 'WireLess' when 'A_LT' then 'ReDownLoad' when 'A_FO' then 'FQC' when 'A_MO' then 'ReWork' "
//				+" else station_id end FStationID,state_id FStateID,substr(station_id, 2, 1) fline,emp_id,'2'seq FROM MES_ASSY_HISTORY where product_id="+ClsCommon.GetSqlString(strProductID)
//				+strTemp
//				+" union  "
//				+" SELECT creation_date fcdate,case substr(station_id, 1, 1) || '_' || substr(station_id, 3, 2) when 'S_AI' then 'SMT Input' when 'S_BO' THEN 'X-Ray' "
//				+" when 'S_CO' then 'Touth Up' when 'T_AI' then 'Router' when 'T_BT' then 'DownLoad' when 'T_ET' then 'Calibration' when 'T_JT' then 'PreTest' "
//				+" when 'T_CT' then 'TBaseBand1' when 'T_NT' then 'TBaseBand2' when 'T_MT' then 'BTWireLess' when 'T_DT' then 'BlueTooch' when 'T_FO' then 'Glue' "
//				+" else station_id end fstationid,state_id fstateid,substr(station_id, 2, 1) fline,emp_id,'1'seq FROM MES_PCBA_HISTORY where product_id="+ClsCommon.GetSqlString(strProductID)
//				+strTemp
//				+" union "
//				+" SELECT creation_date fcdate,case substr(station_id, 1, 1) || '_' || substr(station_id, 3, 2) when 'P_AT' then 'ReDownLoad' when 'P_BT' THEN 'E2PConfig' "
//				+" when 'P_GO' then 'OQC' when 'P_EO' then 'OOB' else station_id end fstationid,state_id fstateID,substr(station_id, 2, 1) fline,emp_id,'3'seq "
//				+" FROM MES_PACK_HISTORY where product_id="+ClsCommon.GetSqlString(strProductID)
//				+strTemp;
//
//			DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
//			dgStation.DataSource = dt.DefaultView;
//			dgStation.DataBind();
		}

		private void bindmainsec(string strProductID)
		{
			string StrSql = "select wo_no,main_id,Secondary_ID,lcm_ID from mes_assy_pid_join where main_id = "+ClsCommon.GetSqlString(strProductID);
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
			dgmainsec.DataSource = dt.DefaultView;
			dgmainsec.DataBind();
		}

		private void bindList()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("fcdate");
			dt.Columns.Add("fstationid");
			dt.Columns.Add("fstateid");
			dt.Columns.Add("fline");
			dt.Columns.Add("emp_id");
			dgStation.DataSource = dt.DefaultView;
			dgStation.DataBind();
		}

		private void dgStation_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.ToUpper()=="SET")
			{
				if(dgStation.CurrentStatus=="SET")
				{
					bindList();
				}
				else if(dgStation.CurrentStatus=="")
				{
					binddata(strProductID);
				}
			}
			else 
			{
				binddata(strProductID);
			}	
		}
	}
}
