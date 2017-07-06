namespace SFCQuery.Boundary
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using DBAccess.EAI;
	using System.Globalization;
	using System.Resources;
	using System.Reflection;
    using System.Data.OracleClient;

	/// <summary>
	///		Summary description for WFrmSFCQuery
	/// </summary>
	public partial class WFrmSFCAssyQuery : System.Web.UI.UserControl
	{ 
		private static string strWOFrom = "";
		//private static string strWOTO = "";
		private static string strModel = "";
        private static string strItem = "";
		private static string dtSDateFrom = "";
		private static string dtSDateTo = "";
		//private int Flag ; //用於判斷當前用戶點擊的是哪一個DataGrid
		private static string strStationID = "";
		private static string strdgWO_NO;
		private static string strdgLine;
		private static string strdgModel;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				MultiLanguage();
				BindModel();
				tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:00";
				tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
				if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text)>0)
					tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
                BindListSMT();
                BindListAssygsm();
                BindListAssynextest();
				BindListPack();
			}
			ddlRegion.SelectedIndex = 1;
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
            this.dgSMT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgSMT_ItemCommand);
            this.dgAssemblygsm.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAssemblygsm_ItemCommand);
            this.dgAssemblynextest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAssemblynextest_ItemCommand);
			this.dgPacking.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPacking_ItemCommand);
			this.dgPIDDetail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPIDDetail_ItemCommand);
		}
		#endregion

		private void MultiLanguage()
		{
            lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
            lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo"); 
            lblItem.Text = (String)GetGlobalResourceObject("SFCQuery", "Item");
            lblRegion.Text = (String)GetGlobalResourceObject("SFCQuery", "Region");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");

            ViewState["WONull"] = (String)GetGlobalResourceObject("SFCQuery", "WONull");
            ViewState["ModelNull"] = (String)GetGlobalResourceObject("SFCQuery", "ModelNull");
            ViewState["InvalidDigit"] = (String)GetGlobalResourceObject("SFCQuery", "InvalidDigit");
            ViewState["ErrorDate"] = (String)GetGlobalResourceObject("CommonRes", "ErrorDate");
            ViewState["QueryCondition"] = (String)GetGlobalResourceObject("SFCQuery", "QueryCondition");

            dgSMT.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgSMT.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");
            dgSMT.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");
            dgSMT.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");

            dgAssemblynextest.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgAssemblynextest.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");
            dgAssemblynextest.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");
            dgAssemblynextest.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");

            dgAssemblygsm.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgAssemblygsm.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model"); 
            dgAssemblygsm.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");
            dgAssemblygsm.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");

            dgPacking.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgPacking.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");
            dgPacking.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");
            dgPacking.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");

            dgPIDDetail.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");
            dgPIDDetail.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "StateID");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			//檢查至少要有一個查詢條件
//			if (tbWOFrom.Text.Trim().Equals("")&&tbWOTo.Text.Trim().Equals("")&&tbModel.Text.Trim().Equals("")&&tbItem.Text.Trim().Equals("")
//				&&tbLineFrom.Text.Trim().Equals("")&&tbLineEnd.Text.Trim().Equals("")&&tbStartDate.DateTextBox.Text.Trim().Equals("")&&tbEndDate.DateTextBox.Text.Trim().Equals(""))
//			{
//				Page.RegisterStartupScript("QueryCondition","<script language=javascript>alert('"+ViewState["QueryCondition"].ToString()+"');</script>");
//				return;
//			}
		    strWOFrom = "";
//			strWOTO = "";
            strModel = "";
            strItem = "";
			dtSDateFrom = "";
			dtSDateTo = "";		
		    strStationID = "";
			ClsGlobal.Flag = 0;

			//得到所有查詢條件的值
			//掃描日期的起迄
			if (!tbStartDate.DateTextBox.Text.Trim().Equals("")) 
			{
				if (ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
				{
					dtSDateFrom = tbStartDate.DateTextBox.Text.Trim();
					Label29.Visible = false;
				}
				else
				{
					Label29.Text = ViewState["ErrorDate"].ToString();
					Label29.Visible = true;
					return;
				}
			}
			if (!tbEndDate.DateTextBox.Text.Trim().Equals("")) 
			{
				if (ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
				{
					dtSDateTo = tbEndDate.DateTextBox.Text.Trim();
					Label28.Visible = false;
				}
				else
				{
					Label28.Text = ViewState["ErrorDate"].ToString();
					Label28.Visible = true;
					return;
				}
			}			

			if (!tbType.Text.Trim().Equals(""))   strWOFrom = tbType.Text.Trim().ToUpper();  //工單起
			//if (!tbWOTo.Text.Trim().Equals(""))     strWOTO = tbWOTo.Text.Trim().ToUpper();      //工單迄
			//if (!ddlType.SelectedValue.ToString().Equals(""))    strModel = ddlType.SelectedValue.ToString();    //機種
           
            switch (rblType.SelectedIndex)
            {
                case 0:
                    strModel = ddlType.SelectedValue.ToString();
                    if (!tbItem.Text.Trim().Equals(""))
                    {
                        strItem = tbItem.Text.Trim().ToUpper();      //料號
                        if (ddlType.SelectedValue.ToString().Equals(""))
                            strModel = strItem.Substring(2, 3);
                        else
                            if (ddlType.SelectedValue.ToString().Equals(strItem.Substring(2, 3)))
                                strModel = ddlType.SelectedValue.ToString();
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "ModelNotMatchPart", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "ModelNotMatchPart") + "');</script>");
                                return;
                            }
                    }
                    break;
                case 1:
                    strWOFrom = tbType.Text.Trim().ToUpper();
                    string strSql = "select substr(apart,3,3) model FROM CMCS_SFC_AORDER where aorder='" + tbType.Text.ToUpper() + "'";                  
                    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

                    if (dt.Rows.Count <= 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(),"WONotExist", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "WONotExist") + "');</script>");
                        return;
                    }
                    else
                        if (!tbItem.Text.Trim().Equals(""))
                        {
                            strItem = tbItem.Text.Trim().ToUpper();      //料號
                            if (dt.Rows[0]["model"].ToString().Equals(strItem.Substring(2, 3)))
                                strModel = strItem.Substring(2, 3).ToString();
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "WONotMatchPart", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "WONotMatchPart") + "');</script>");
                                return;
                            }
                        } 
                        else
                            strModel = dt.Rows[0]["model"].ToString();

                    break;
            } 
			
			Label28.Visible = false;
			Label29.Visible = false;

			switch(ddlRegion.SelectedIndex)
			{
				case 0:
					dgSMT.Visible = true;
                    dgAssemblygsm.Visible = false;
                    dgAssemblynextest.Visible = false;
					dgPacking.Visible = false;
					break;
				case 1:
                    if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "RUY")
                    {
                        dgSMT.Visible = false;
                        dgAssemblygsm.Visible = false;
                        dgAssemblynextest.Visible = true;
                        dgPacking.Visible = false;
                    }
                    else
                    {
                        dgSMT.Visible = false;
                        dgAssemblygsm.Visible = true;
                        dgAssemblynextest.Visible = false;
                        dgPacking.Visible = false;
                    }
					break;
				case 2:
                    dgSMT.Visible = false;
                    dgAssemblygsm.Visible = false;
                    dgAssemblynextest.Visible = false;
					dgPacking.Visible = true;
					break;
			}			

			QueryByModel(ddlType.SelectedValue,tbStartDate.DateTextBox.Text.Trim(),tbEndDate.DateTextBox.Text.Trim());
//			switch(rblType.SelectedIndex)
//			{
//				case 0:
//					QueryByModel(ddlType.SelectedValue,tbStartDate.DateTextBox.Text.Trim(),tbEndDate.DateTextBox.Text.Trim());						
//					break;
//				case 1:
//					//QueryByWO(tbType.Text.Trim().ToUpper(),tbStartDate.DateTextBox.Text.Trim(),tbEndDate.DateTextBox.Text.Trim());
////					if (StrSql.Equals(""))
////					{
////						Page.RegisterStartupScript("WONotExist","<script language=javascript>alert('"+ViewState["WONotExist"].ToString()+"');</script>");
////						return;
////					}
//					break;
//			}
			//DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];

//			binddata(true);
			BindListPid();
//			}
		}

		private void QueryByModel(string Model, string DateStart, string DateEnd) 
		{
			switch(ddlRegion.SelectedIndex)
			{
				case 0:
					GetData(dgSMT,"MES_PCBA_HISTORY","CMCS_SFC_SORDER");
					break;
				case 1:
                    if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "RUY")
                        GetData(dgAssemblynextest, "MES_ASSY_WIP", "CMCS_SFC_AORDER");
                    else
                        GetData(dgAssemblygsm, "MES_ASSY_WIP", "CMCS_SFC_AORDER");
					break;
				case 2:
					GetData(dgPacking,"MES_PACK_HISTORY","CMCS_SFC_PORDER");
					break;
			}	
		}

//		private void binddata(bool refresh)
//		{
//			switch(ddlRegion.SelectedIndex)
//			{
//				case 0:
//					GetData(dgSMT,"MES_PCBA_WIP","CMCS_SFC_SORDER");
//					break;
//				case 1:				
//					GetData(dgAssembly,"MES_ASSY_WIP","CMCS_SFC_AORDER");
//					break;
//				case 2:
//					GetData(dgPacking,"MES_PACK_WIP","CMCS_SFC_PORDER");
//					break;
//			}			
//		}

		private void GetData(WebDataGrid dgResource,string strTable,string strWoTable)
		{

			string strTemp = "";
			string strTestData = "";
			string strItem = "";
			string StrSql = "";
			string strOrder = "";
			switch(strTable.ToLower())
			{
				case "mes_pcba_history":
					StrSql = "SELECT SPART PN,PID_QTY WO_QTY,B.* FROM "+strWoTable+" A JOIN( ";
					strTemp = " SUM(DECODE(STATION_ID,'S_AI',DECODE(STATE_ID, 'P',QTY,0),''))FINPUT, "
						+" SUM(DECODE(STATION_ID,'S_BO',DECODE(STATE_ID, 'P',QTY,0),''))FXRAY, "
						+" SUM(DECODE(STATION_ID,'S_CO',DECODE(STATE_ID, 'P',QTY,0),''))FTOUCHUP, "
						+" SUM(DECODE(STATION_ID,'T_AI',DECODE(STATE_ID, 'P',QTY,0),''))FROUTER, "
						+" SUM(DECODE(STATION_ID,'T_BT',DECODE(STATE_ID, 'P',QTY,0),''))FDOWNLOAD, "
						+" SUM(DECODE(STATION_ID,'T_ET',DECODE(STATE_ID, 'P',QTY,0),''))FCALIBRATION, "
						+" SUM(DECODE(STATION_ID,'T_JT',DECODE(STATE_ID, 'P',QTY,0),''))FPRETEST, "
						+" SUM(DECODE(STATION_ID,'T_CT',DECODE(STATE_ID, 'P',QTY,0),''))FBASEBAND1, "
						+" SUM(DECODE(STATION_ID,'T_NT',DECODE(STATE_ID, 'P',QTY,0),''))FBASEBAND2, "
						+" SUM(DECODE(STATION_ID,'T_FO',DECODE(STATE_ID, 'P',QTY,0),''))FGLUE ";
					strTestData = " UNION (SELECT DISTINCT UPPER(LINE_CODE) LINE_ID,WORK_ORDER WO_NO, "
                       +" DECODE(STATION_CODE,'DL','T_BT','CA','T_ET','BT','T_DT','BTWL','T_MT','PT','T_JT','B1','T_CT','B2','T_NT') STATION_ID,"
                       +" STATUS STATE_ID,PRODUCT_ID FROM {0}.PRODUCT_HISTORY_V T WHERE STATION_CODE IN ('DL', 'CA', 'BT', 'BTWL', 'B1', 'B2', 'PT') AND UPPER(PRODUCT_ID)<>'SCA_INVALID_SN' ";
					if (!strItem.Equals(""))
						strItem = strItem + " AND UPPER(A.SPART) LIKE "+ClsCommon.GetSqlString("%"+strItem+"%");
					strOrder = "SORDER" ;
					break;
                case "mes_assy_wip":
                    StrSql = "SELECT APART PN,APID_QTY WO_QTY,B.* FROM " + strWoTable + " A JOIN( ";
                    if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "RUY")
                        strTemp = " SUM(DECODE(STATION_ID,'A_BI',DECODE(STATE_ID, 'P',QTY,''),''))FINPUT, "
                             + " SUM(DECODE(STATION_ID,'BD',QTY,''))FBoardTest, "
                             + "SUM(DECODE(STATION_ID,'PH',QTY,''))FPhasing, "
                             + " SUM(DECODE(STATION_ID,'PRT',QTY,''))FProto, "
                             + " SUM(DECODE(STATION_ID,'CI',QTY,''))FCIT, "
                             + " SUM(DECODE(STATION_ID,'BL',QTY,''))FBluetooth, "
                             + " SUM(DECODE(STATION_ID,'FC',QTY,''))FFocus, "
                             + "SUM(DECODE(STATION_ID,'PK',QTY,''))FPack, "
                             + " SUM(DECODE(STATION_ID,'RA',QTY,''))FRadiated, "
                             + " SUM(DECODE(STATION_ID,'QA',QTY,''))FFQA, "
                             + " SUM(DECODE(STATION_ID,'CF',QTY,''))FCFC ";
                    else
                    {
                        strTemp = " SUM(DECODE(STATION_ID,'A_BI',DECODE(STATE_ID, 'P',QTY,''),''))FINPUT, "
                          + " SUM(DECODE(STATION_ID,'A_KT',QTY,''))FABASEBAND1, "
                          //+ "SUM(DECODE(STATION_ID,'T_NT',QTY,''))FTBASEBAND2, "
                          + " SUM(DECODE(STATION_ID,'A_CT',QTY,''))FABASEBAND2, "
                          + " SUM(DECODE(STATION_ID,'A_HT',QTY,''))FABASEBAND3, "
                            //+ " SUM(DECODE(STATION_ID,'A_IT',QTY,''))FABASEBAND4, "
                          + " SUM(DECODE(STATION_ID,'A_DT',QTY,''))FWIRELESS, "
                          + "SUM(DECODE(STATION_ID,'A_ET',QTY,''))FBTWIRELESS, "
                          + " SUM(DECODE(STATION_ID,'A_LT',QTY,''))FREDOWNLOAD, "
                          + " SUM(DECODE(STATION_ID,'A_MO',QTY,''))FREWORK, "
                          + " SUM(DECODE(STATION_ID,'A_FO',QTY,''))FFQC ";
                    }

                        strTestData = " UNION ALL (SELECT /*+RULE+*/ S.WO_NO,  "
                             + " DECODE(STATION_CODE,'WL','A_DT','D2','T_LT','A1','A_KT','A2','A_CT','A3','A_HT','A4','A_IT','A5','A_JT','B2','T_NT','BTWL','A_ET',STATION_CODE) STATION_ID,"
                             + " STATUS STATE_ID,T.PRODUCT_ID,PDDATE CREATE_DATE FROM Sfc.MES_ASSY_PID_JOIN S,"+strModel+".PRODUCT_HISTORY_V T WHERE STATION_CODE IN ('WL', 'D2', 'A1', 'A2', 'A3', 'A4', 'A5','B2','E2','BTWL','BD','PH','PRT','CI','BL','FC','PK','RA','QA','CF') AND S.MAIN_ID=T.PRODUCT_ID ";
                        //switch (rblType.SelectedIndex)
                        //{
                        //    case 0:
                        //        break;
                        //    case 1:
                        //        if (!strWOFrom.Equals(""))
                        //        strTestData += " AND S.WO_NO =" + ClsCommon.GetSqlString(strWOFrom);
                        //        break;
                        //} 
                     //if (!tbItem.Text.ToUpper().Trim().Equals(""))
                     //    strItem = strItem + " AND UPPER(A.APART) ='"+ tbItem.Text.ToUpper().Trim()+"'";
					strOrder = "AORDER" ;
					break;
				case "mes_pack_history":
					StrSql = "SELECT PPART PN,PPID_QTY WO_QTY,B.* FROM "+strWoTable+" A JOIN( ";
					strTemp = " SUM(DECODE(STATION_ID,'P_BT',DECODE(STATE_ID, 'P',QTY,0),''))FE2P, "
						+" SUM(DECODE(STATION_ID,'P_GO',DECODE(STATE_ID, 'P',QTY,0),''))FOQC, "
						+" SUM(DECODE(STATION_ID,'P_EO',DECODE(STATE_ID, 'P',QTY,0),''))FOOB ";
					strTestData = " UNION (SELECT DISTINCT UPPER(LINE_CODE) LINE_ID,WORK_ORDER WO_NO, "
						+" DECODE(STATION_CODE,'E2','P_BT') STATION_ID,"
						+" STATUS STATE_ID,PRODUCT_ID FROM "+strModel+".PRODUCT_HISTORY_V T WHERE STATION_CODE IN ('E2') AND UPPER(PRODUCT_ID)<>'SCA_INVALID_SN' ";
					if (!strItem.Equals(""))
						strItem = strItem + " AND UPPER(A.PPART) LIKE "+ClsCommon.GetSqlString("%"+strItem+"%");
					strOrder = "PORDER" ;
					break;
			}

			StrSql = StrSql
						+" SELECT WO_NO FWO_NO,FMODEL, "
						+strTemp
                        + " FROM (SELECT WO_NO," + ClsCommon.GetSqlString(strModel) + " FMODEL, STATION_ID, STATE_ID, COUNT(*) QTY"
                        + " FROM (SELECT WO_NO,STATION_ID,STATE_ID,PRODUCT_ID FROM (SELECT WO_NO,STATION_ID,STATE_ID,PRODUCT_ID,ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID ORDER BY CREATE_DATE DESC ) RN FROM (( "
						+" SELECT WO_NO"
                        + " ,SUBSTR(STATION_ID,1,1)||'_'||SUBSTR(STATION_ID,3,2) STATION_ID,STATE_ID, PRODUCT_ID,CREATION_DATE CREATE_DATE"
						+" FROM "+strTable+" WHERE 1=1 ";
			switch (rblType.SelectedIndex)
			{
				case 0:
					if (!strModel.Equals(""))
					{
                        if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "RUY")
                            StrSql = StrSql + " AND UPPER(PRODUCT_ID) IN (SELECT  S.PRODUCT_ID FROM Sfc.MES_PCBA_PANEL_DETAIL R,Sfc.MES_PCBA_PANEL_LINK S,SHP.CMCS_SFC_SORDER T WHERE S.PANEL_ID=R.PANEL_ID AND R.WO_NO=T.SORDER AND T.MODEL=" + ClsCommon.GetSqlString(strModel) + ")";
                        else
                            StrSql = StrSql + " AND UPPER(SUBSTR(PRODUCT_ID,1,3)) LIKE " + ClsCommon.GetSqlString(strModel + "%");
						strModel = ddlType.SelectedValue.ToString().ToUpper();
					}
					break;
				case 1:
					if (!strWOFrom.Trim().Equals(""))
					{
						StrSql = StrSql + " AND WO_NO = "+ClsCommon.GetSqlString(strWOFrom);
						strModel = GetPN(strWOFrom).Substring(2,3);
					}
					break;
			}
				

			if(ckbRepair.Checked)
				strTestData = strTestData + " AND REPAIR = 0 ";

			if (!dtSDateFrom.Equals("") && !dtSDateTo.Equals(""))
			{
				StrSql = StrSql + "AND CREATION_DATE BETWEEN TO_DATE("+ClsCommon.GetSqlString(dtSDateFrom)+",'yyyy/mm/dd hh24:mi') AND TO_DATE("+ClsCommon.GetSqlString(dtSDateTo)+",'yyyy/mm/dd hh24:mi') ";
				strTestData = strTestData +" AND PDDATE BETWEEN TO_DATE("+ClsCommon.GetSqlString(dtSDateFrom)+", 'yyyy/mm/dd hh24:mi') AND TO_DATE("+ClsCommon.GetSqlString(dtSDateTo)+", 'yyyy/mm/dd hh24:mi')";
			}
			

			strTestData = string.Format(strTestData,strModel);
            if (strModel == "GNG" || strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "RUY")
                StrSql = StrSql + "  AND SUBSTR(STATION_ID,1,1)||'_'||SUBSTR(STATION_ID,3,2) ='A_BI' ) " + strTestData + " ) ";
            else
                StrSql = StrSql + " ) " + strTestData + " ) ";

			
			//StrSql = StrSql +" group by wo_no,substr(product_id,1,3),substr(station_id,2,1),substr(station_id,1,1)||'_'||substr(station_id,3,2)) b  on a.wo_no=b.wo_no";
            StrSql = StrSql + " )) where RN=1) WHERE STATION_ID IN('A_BI','A_KT','A_CT','A_HT','A_DT','A_ET','A_LT','A_MO','A_FO') GROUP BY WO_NO,substr(product_id,1,3), STATION_ID, STATE_ID) GROUP BY WO_NO,FMODEL)b  ON A." + strOrder + "=B.FWO_NO ";

			StrSql = StrSql + strItem;

            switch (rblType.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    if (!strWOFrom.Equals(""))
                        StrSql += " AND B.FWO_NO =" + ClsCommon.GetSqlString(strWOFrom);
                    break;
            }
            if (!tbItem.Text.ToUpper().Trim().Equals(""))
                StrSql = StrSql + " AND UPPER(A.APART) ='" + tbItem.Text.ToUpper().Trim() + "'";
			

			DataTable  dtRes = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
			dgResource.DataSource = dtRes.DefaultView;
			dgResource.DataBind();
            //if (strModel.Equals("SLI"))
            //    dgAssembly.Columns[7].Visible = true;
            //else
            //    dgAssembly.Columns[7].Visible = false ;

//			string StrSql = "select  wo_no,substr(product_id,1,3)model,substr(station_id,2,1)line,substr(station_id,1,1)||'_'||substr(station_id,3,2)station_id,count(*)Qty "
//				+" from "+strTable+" where Upper(wo_no) like "+ClsCommon.GetSqlString(tbWOFrom.Text.ToUpper())+" and substr(product_id,1,3)="+ClsCommon.GetSqlString(tbModel.Text.ToUpper());
//			if (!strLine.Trim().Equals("''"))
//				StrSql = StrSql +" and substr(station_id,2,1) in("+strLine+")";
//			StrSql = StrSql +" group by wo_no,substr(product_id,1,3),substr(station_id,2,1),substr(station_id,1,1)||'_'||substr(station_id,3,2)  order by wo_no,model,line";
//			dtRes = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
//			string StrWo = tbWOFrom.Text.Substring(0,1).ToUpper();
//			switch(StrWo)
//			{
//				case "S" :
//					dtRes = GetSMTDate(dtRes);
//					break;
//				case "A" :
//					dtRes = GetAssyDate(dtRes);
//					break;
//				case "P" :
//					dtRes = GetPackDate(dtRes);
//					break;
//			}
//			
//			dgResource.DataSource = dtRes.DefaultView;
//			dgResource.DataBind();

			/*
			switch(strTable.ToLower())
			{
				case "mes_pcba_wip":
					dtRes = GetSMTDate(dtRes);					
					dgResource.DataSource = dtRes.DefaultView;
					dgResource.DataBind();
					break;
				case "mes_assy_wip":
					dtRes = GetAssyDate(dtRes);
					dgResource.DataSource = dtRes.DefaultView;
					dgResource.DataBind();
					break;
				case "mes_pack_wip":
					dtRes = GetPackDate(dtRes);
					dgResource.DataSource = dtRes.DefaultView;
					dgResource.DataBind();
					break;
			}	
			
			*/
		}

		private string GetPN(string WO_NO) 
		{
			string StrSql = "";
			switch(ddlRegion.SelectedIndex)
			{
				case 0:	
					StrSql = "SELECT SPART FROM CMCS_SFC_SORDER WHERE SORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
				case 1:
					StrSql = "SELECT APART FROM CMCS_SFC_AORDER WHERE AORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
				case 2:
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

		private DataTable GetSMTDate(DataTable dtRes)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("FWo_no",typeof(String));
			dt.Columns.Add("FModel",typeof(String));
			dt.Columns.Add("FLine",typeof(String));			
			dt.Columns.Add("PN",typeof(String));
			dt.Columns.Add("WO_Qty",typeof(String));
			dt.Columns.Add("FInput",typeof(String));
			dt.Columns.Add("FXRay",typeof(String));
			dt.Columns.Add("FTouchUP",typeof(String));
			dt.Columns.Add("FRouter",typeof(String));
			dt.Columns.Add("FDownLoad",typeof(String));
			dt.Columns.Add("FCalibration",typeof(String));
			dt.Columns.Add("FPreTest",typeof(String));
			dt.Columns.Add("FBaseBand1",typeof(String));
			dt.Columns.Add("FBaseBand2",typeof(String));
			dt.Columns.Add("FGlue",typeof(String));
			
			string strWO_NO = "";
			string strModel = "";
			string strLine = "";
			DataRow dr = null;
			foreach(DataRow rw in dtRes.Rows)
			{
				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
				{
					if (dr!=null)
						dt.Rows.InsertAt(dr,0);
					dr = dt.NewRow();
					dr["FWo_no"] = rw["wo_no"].ToString();
					dr["FModel"] = rw["Model"].ToString();
					dr["FLine"] = rw["Line"].ToString();
					dr["PN"] = rw["PN"].ToString();
					dr["wo_qty"] = rw["wo_qty"].ToString();
					switch (rw["station_id"].ToString().ToUpper())
					{
						case "S_AI" :
							dr["FInput"] = rw["Qty"].ToString();
							break;						
						case "S_BO" :
							dr["FXRay"] = rw["Qty"].ToString();
							break;
						case "S_CO" :
							dr["FTouchUP"] = rw["Qty"].ToString();
							break;
						case "T_AI" :
							dr["FRouter"] = rw["Qty"].ToString();
							break;
						case "T_BT" :
							dr["FDownLoad"] = rw["Qty"].ToString();
							break;
						case "T_ET" :
							dr["FCalibration"] = rw["Qty"].ToString();
							break;
						case "T_CT" :
							dr["FBaseBand1"] = rw["Qty"].ToString();
							break;
						case "T_NT" :
							dr["FBaseBand2"] = rw["Qty"].ToString();
							break;
						case "T_JT" :
							dr["FPreTest"] = rw["Qty"].ToString();
							break;
						case "T_FO" :
							dr["FGlue"] = rw["Qty"].ToString();
							break;
					}
					strWO_NO = rw["wo_no"].ToString();
					strModel = rw["Model"].ToString();
					strLine = rw["Line"].ToString();
					
				}
				else
				{
					switch (rw["station_id"].ToString().ToUpper())
					{
						case "S_AI" :
							dr["FInput"] = rw["Qty"].ToString();
							break;						
						case "S_BO" :
							dr["FXRay"] = rw["Qty"].ToString();
							break;
						case "S_CO" :
							dr["FTouchUP"] = rw["Qty"].ToString();
							break;
						case "T_BT" :
							dr["FDownLoad"] = rw["Qty"].ToString();
							break;
						case "T_ET" :
							dr["FCalibration"] = rw["Qty"].ToString();
							break;
						case "T_CT" :
							dr["FBaseBand1"] = rw["Qty"].ToString();
							break;
						case "T_NT" :
							dr["FBaseBand2"] = rw["Qty"].ToString();
							break;
						case "T_JT" :
							dr["FPreTest"] = rw["Qty"].ToString();
							break;
						case "T_FO" :
							dr["FGlue"] = rw["Qty"].ToString();
							break;
					}
					strWO_NO = rw["wo_no"].ToString();
					strModel = rw["Model"].ToString();
					strLine = rw["Line"].ToString();
				}
//				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
//				{
//					dt.Rows.InsertAt(dr,0);
//				}

			}
			if (dr!=null)
			  dt.Rows.InsertAt(dr,0);
			return dt;
		}

		private DataTable GetAssyDate(DataTable dtRes)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("FWo_no",typeof(String));
			dt.Columns.Add("FModel",typeof(String));
			dt.Columns.Add("FLine",typeof(String));
			dt.Columns.Add("PN",typeof(String));
			dt.Columns.Add("WO_Qty",typeof(String));
			dt.Columns.Add("FInput",typeof(String));
			dt.Columns.Add("FABaseBand1",typeof(String));
			dt.Columns.Add("FABaseBand2",typeof(String));
			dt.Columns.Add("FWireLess",typeof(String));
			dt.Columns.Add("FReDownload",typeof(String));
			dt.Columns.Add("FFQC",typeof(String));
			dt.Columns.Add("FReWork",typeof(String));
			
			string strWO_NO = "";
			string strModel = "";
			string strLine = "";
			DataRow dr = null;
			foreach(DataRow rw in dtRes.Rows)
			{
				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
				{
					if (dr!=null)
						dt.Rows.InsertAt(dr,0);
					dr = dt.NewRow();
					dr["FWo_no"] = rw["wo_no"].ToString();
					dr["FModel"] = rw["Model"].ToString();
					dr["FLine"] = rw["Line"].ToString();
					dr["PN"] = rw["PN"].ToString();
					dr["wo_qty"] = rw["wo_qty"].ToString();
					switch (rw["station_id"].ToString().ToUpper())
					{
						case "A_BI" :
							dr["FInput"] = rw["Qty"].ToString();
							break;						
						case "A_KT" :
							dr["FABaseBand1"] = rw["Qty"].ToString();
							break;
						case "A_CT" :
							dr["FABaseBand2"] = rw["Qty"].ToString();
							break;
						case "A_DT" :
							dr["FWireLess"] = rw["Qty"].ToString();
							break;
						case "A_LT" :
							dr["FReDownload"] = rw["Qty"].ToString();
							break;
						case "A_FO" :
							dr["FFQC"] = rw["Qty"].ToString();
							break;
						case "A_MO" :
							dr["FReWork"] = rw["Qty"].ToString();
							break;
					}
					strWO_NO = rw["wo_no"].ToString();
					strModel = rw["Model"].ToString();
					strLine = rw["Line"].ToString();
					
				}
				else
				{
					switch (rw["station_id"].ToString().ToUpper())
					{
						case "A_BI" :
							dr["FInput"] = rw["Qty"].ToString();
							break;						
						case "A_KT" :
							dr["FABaseBand1"] = rw["Qty"].ToString();
							break;
						case "A_CT" :
							dr["FABaseBand2"] = rw["Qty"].ToString();
							break;
						case "A_DT" :
							dr["FWireLess"] = rw["Qty"].ToString();
							break;
						case "A_LT" :
							dr["FReDownload"] = rw["Qty"].ToString();
							break;
						case "A_FO" :
							dr["FFQC"] = rw["Qty"].ToString();
							break;
						case "A_MO" :
							dr["FReWork"] = rw["Qty"].ToString();
							break;
					}
					strWO_NO = rw["wo_no"].ToString();
					strModel = rw["Model"].ToString();
					strLine = rw["Line"].ToString();
				}
//				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
//				{
//					dt.Rows.InsertAt(dr,0);
//				}
			}
			if (dr!=null)
				dt.Rows.InsertAt(dr,0);
			return dt;
		}

		private DataTable GetPackDate(DataTable dtRes)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("FWo_no",typeof(String));
			dt.Columns.Add("FModel",typeof(String));
			dt.Columns.Add("FLine",typeof(String));
			dt.Columns.Add("PN",typeof(String));
			dt.Columns.Add("WO_Qty",typeof(String));
			dt.Columns.Add("FE2P",typeof(String));
			dt.Columns.Add("FOQC",typeof(String));
			dt.Columns.Add("FOOB",typeof(String));
			
			string strWO_NO = "";
			string strModel = "";
			string strLine = "";
			DataRow dr = null;
			foreach(DataRow rw in dtRes.Rows)
			{
				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
				{
					if (dr!=null)
						dt.Rows.InsertAt(dr,0);
					dr = dt.NewRow();
					dr["FWo_no"] = rw["wo_no"].ToString();
					dr["FModel"] = rw["Model"].ToString();
					dr["FLine"] = rw["Line"].ToString();
					dr["PN"] = rw["PN"].ToString();
					dr["wo_qty"] = rw["wo_qty"].ToString();
					switch (rw["station_id"].ToString().ToUpper())
					{
						case "P_BT" :
							dr["FE2P"] = rw["Qty"].ToString();
							break;						
						case "P_GO" :
							dr["FOQC"] = rw["Qty"].ToString();
							break;
						case "P_EO" :
							dr["FOOB"] = rw["Qty"].ToString();
							break;
					}
					strWO_NO = rw["wo_no"].ToString();
					strModel = rw["Model"].ToString();
					strLine = rw["Line"].ToString();
					
				}
				else
				{
					switch (rw["station_id"].ToString().ToUpper())
					{
						case "P_BT" :
							dr["FE2P"] = rw["Qty"].ToString();
							break;						
						case "P_GO" :
							dr["FOQC"] = rw["Qty"].ToString();
							break;
						case "P_EO" :
							dr["FOOB"] = rw["Qty"].ToString();
							break;
					}
					strWO_NO = rw["wo_no"].ToString();
					strModel = rw["Model"].ToString();
					strLine = rw["Line"].ToString();
				}
//				if (strWO_NO!=rw["wo_no"].ToString() || strModel!=rw["model"].ToString() || strLine!=rw["line"].ToString())
//				{
//					dt.Rows.InsertAt(dr,0);
//				}
			}
			if (dr!=null)
				dt.Rows.InsertAt(dr,0);
			return dt;
		}

		private void GetOtherData(DataTable dtRes)
		{
			dgSMT.DataSource = dtRes.DefaultView;
			dgSMT.DataBind();
		}

		private void BindListSMT()
		{
			DataTable dt = new DataTable();

			dt.Columns.Add("FWo_no");
			dt.Columns.Add("FModel");
			dt.Columns.Add("FLine");
			dt.Columns.Add("FInput");
			dt.Columns.Add("FXRay");
			dt.Columns.Add("FTouchUP");
			dt.Columns.Add("FRouter");
			dt.Columns.Add("FDownLoad");
			dt.Columns.Add("FCalibration");
			dt.Columns.Add("FPreTest");
			dt.Columns.Add("FBaseBand1");
			dt.Columns.Add("FBaseBand2");
			dt.Columns.Add("FGlue");
            			
			dgSMT.DataSource = dt.DefaultView;
			dgSMT.DataBind();
		}

        private void BindListAssygsm()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("FWo_no");
            dt.Columns.Add("FModel");
            dt.Columns.Add("FLine");
            dt.Columns.Add("FInput");
            dt.Columns.Add("FABaseBand1");
            dt.Columns.Add("FABaseBand2");
            dt.Columns.Add("FWireLess");
            dt.Columns.Add("FReDownload");
            dt.Columns.Add("FFQC");
            dt.Columns.Add("FReWork");

            dgAssemblygsm.DataSource = dt.DefaultView;
            dgAssemblygsm.DataBind();
        }

        private void BindListAssynextest()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("FWo_no");
            dt.Columns.Add("FModel");
            dt.Columns.Add("FLine");
            dt.Columns.Add("FInput");
            dt.Columns.Add("FBoardTest");
            dt.Columns.Add("FPhsing");
            dt.Columns.Add("FProto");
            dt.Columns.Add("FCIT");
            dt.Columns.Add("FBluetooth");
            dt.Columns.Add("FFocus");
            dt.Columns.Add("FPack");
            dt.Columns.Add("FRadiated");
            dt.Columns.Add("FFQA");
            dt.Columns.Add("FCFC");

            dgAssemblynextest.DataSource = dt.DefaultView;
            dgAssemblynextest.DataBind();
        }

		private void BindListPack()
		{
			DataTable dt = new DataTable();

			dt.Columns.Add("FWo_no");
			dt.Columns.Add("FModel");
			dt.Columns.Add("FLine");
			dt.Columns.Add("FE2P");
			dt.Columns.Add("FOQC");
			dt.Columns.Add("FOOB");
            			
			dgPacking.DataSource = dt.DefaultView;
			dgPacking.DataBind();
		}

		private void dgSMT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClsGlobal.Flag = 1;
			if(e.CommandName.ToUpper()=="SET")
			{
				if(dgSMT.CurrentStatus=="SET")
				{
					BindListSMT();
				}
				else if(dgSMT.CurrentStatus=="")
				{
					//binddata(false);
					btnQuery_Click(null,null);
				}
			}
			else 
			{
				if (e.CommandName.ToUpper()=="SORT" || e.CommandName.ToLower()== "page" || e.CommandName.ToLower()== "changepage")
				    btnQuery_Click(null,null);
				if (e.CommandName.ToUpper()!="SORT" &&e.CommandName.ToLower()!= "page" && e.CommandName.ToLower()!= "changepage")
				{
					strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
					strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
					strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
				}
			}	

			ClsGlobal.Flag = 1;
//			strLine = strLine.Replace("'","\"");
			if (e.CommandName=="Input")		    {strStationID = "S_AI";	binddataPID();}
			if (e.CommandName=="TouchUP")       {strStationID = "S_CO"; binddataPID();}
			if (e.CommandName=="XRay")          {strStationID = "S_BO"; binddataPID();}
			if (e.CommandName=="Router")        {strStationID = "T_AI"; binddataPID();}
			if (e.CommandName=="download")      {strStationID = "T_BT"; binddataPID();}
			if (e.CommandName=="Calibration")   {strStationID = "T_ET"; binddataPID();}
			if (e.CommandName=="PreTest")       {strStationID = "T_JT"; binddataPID();}
			if (e.CommandName=="BaseBand1")     {strStationID = "T_CT"; binddataPID();}
			if (e.CommandName=="BaseBand2")     {strStationID = "T_NT"; binddataPID();}
			if (e.CommandName=="Glue")          {strStationID = "T_FO"; binddataPID();}
		}

        private void dgAssemblynextest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ClsGlobal.Flag = 2;
            if (e.CommandName.ToUpper() == "SET")
            {
                if (dgAssemblynextest.CurrentStatus == "SET")
                {
                    BindListAssynextest();
                }
                else if (dgAssemblynextest.CurrentStatus == "")
                {
                    //binddata(false);
                    btnQuery_Click(null, null);
                }
            }
            else
            {
                if (e.CommandName.ToUpper() == "SORT" && e.CommandName.ToLower() == "page" || e.CommandName.ToLower() == "changepage" || e.CommandName.ToLower() == "next" || e.CommandName.ToLower() == "prior")
                    btnQuery_Click(null, null);
                if (e.CommandName.ToUpper() != "SORT" && e.CommandName.ToLower() != "page" && e.CommandName.ToLower() != "changepage" && e.CommandName.ToLower() != "next" && e.CommandName.ToLower() != "prior")
                {
                    strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
                    strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
                    strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
                }
            }

            //			strLine = strLine.Replace("'","\"");
            if (e.CommandName == "Input") { strStationID = "A_BI"; binddataPID(); }
            if (e.CommandName == "BoardTest") { strStationID = "BD"; binddataPID(); }
            if (e.CommandName == "Phasing") { strStationID = "PH"; binddataPID(); }
            if (e.CommandName == "Proto") { strStationID = "PRT"; binddataPID(); }
            if (e.CommandName == "CIT") { strStationID = "CI"; binddataPID(); }
            if (e.CommandName == "Bluetooth") { strStationID = "BL"; binddataPID(); }
            if (e.CommandName == "Focus") { strStationID = "FC"; binddataPID(); }
            if (e.CommandName == "Pack") { strStationID = "PK"; binddataPID(); }
            if (e.CommandName == "Radiated") { strStationID = "RA"; binddataPID(); }
            if (e.CommandName == "FQA") { strStationID = "QA"; binddataPID(); }
            if (e.CommandName == "CFC") { strStationID = "CF"; binddataPID(); }
        }

        private void dgAssemblygsm_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ClsGlobal.Flag = 2;
            if (e.CommandName.ToUpper() == "SET")
            {
                if (dgAssemblygsm.CurrentStatus == "SET")
                {
                    BindListAssygsm();
                }
                else if (dgAssemblygsm.CurrentStatus == "")
                {
                    //binddata(false);
                    btnQuery_Click(null, null);

                }
            }
            else
            {
                if (e.CommandName.ToUpper() != "SORT" && e.CommandName.ToLower() == "page" || e.CommandName.ToLower() == "changepage")
                    btnQuery_Click(null, null);
                if (e.CommandName.ToUpper() != "SORT" && e.CommandName.ToLower() != "page" && e.CommandName.ToLower() != "changepage")
                {
                    strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
                    strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
                    strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
                }
            }

            //			strLine = strLine.Replace("'","\"");
            if (e.CommandName == "Input") { strStationID = "A_BI"; binddataPID(); }
            if (e.CommandName == "ABaseBand1") { strStationID = "A_KT"; binddataPID(); }
            if (e.CommandName == "ABaseBand2") { strStationID = "A_CT"; binddataPID(); }
            if (e.CommandName == "ABaseBand3") { strStationID = "A_HT"; binddataPID(); }
            if (e.CommandName == "ABaseBand4") { strStationID = "A_IT"; binddataPID(); }
            if (e.CommandName == "WireLess") { strStationID = "A_DT"; binddataPID(); }
            if (e.CommandName == "ReDownload") { strStationID = "A_LT"; binddataPID(); }
            if (e.CommandName == "FQC") { strStationID = "A_FO"; binddataPID(); }
            if (e.CommandName == "ReWork") { strStationID = "A_MO"; binddataPID(); }
        }

		private void dgPacking_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClsGlobal.Flag  = 3;
			if(e.CommandName.ToUpper()=="SET")
			{
				if(dgPacking.CurrentStatus=="SET")
				{
					BindListPack();
				}
				else if(dgPacking.CurrentStatus=="")
				{
					//binddata(false);
					btnQuery_Click(null,null);
					
				}
			}
			else 
			{
				if (e.CommandName.ToUpper()!="SORT" && e.CommandName.ToLower()== "page" || e.CommandName.ToLower()== "changepage")
				    btnQuery_Click(null,null);
				if (e.CommandName.ToUpper()!="SORT"&&e.CommandName.ToLower()!= "page" && e.CommandName.ToLower()!= "changepage")
				{
					strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
					strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
					strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
				}
			}	

			ClsGlobal.Flag  = 3;
//			strLine = strLine.Replace("'","\"");
			if (e.CommandName=="E2P")       {strStationID = "P_BT"; binddataPID();}
			if (e.CommandName=="OQC")       {strStationID = "P_GO"; binddataPID();}
			if (e.CommandName=="OOB")       {strStationID = "P_EO"; binddataPID();}
		}

        // old
//        private void binddataPID()
//        {			
//            string StrSql = "SELECT DISTINCT PRODUCT_ID FPID,WO_NO FWO_NO,'LINE'||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1)) - 64)FLINE,STATE_ID FSTATUS FROM ";
//            string strTestData = "SELECT DISTINCT PRODUCT_ID FPID,WORK_ORDER FWO_NO,UPPER(LINE_CODE) FLINE,STATUS FSTATUS FROM "+strdgModel+".PRODUCT_HISTORY_V T WHERE "
//                +" PRODUCT_ID LIKE "+ClsCommon.GetSqlString(strdgModel+"%")
//                +" AND UPPER(LINE_CODE)= "+ClsCommon.GetSqlString(strdgLine)
//                +" AND WORK_ORDER= "+ClsCommon.GetSqlString(strdgWO_NO)
//                +" AND STATION_CODE IN ("+ClsCommon.GetSqlString(GetStation(strStationID))+") ";
//            switch(ClsGlobal.Flag)
//            {
//                case 1 :
//                    StrSql = StrSql + " MES_PCBA_HISTORY ";
//                    break;
//                case 2 :
//                    StrSql = StrSql+" MES_ASSY_HISTORY ";
//                    break;
//                case 3 :
//                    StrSql = StrSql+" MES_PACK_HISTORY ";
//                    break;
//            }	
//            StrSql = StrSql + " WHERE 1=1 ";
//            //if (!strModel.Equals(""))
//                StrSql = StrSql+" AND UPPER(SUBSTR(PRODUCT_ID,1,3)) LIKE "+ClsCommon.GetSqlString("%"+strdgModel+"%");
//            //if (!strLine.Trim().Equals("''"))
//                StrSql = StrSql +" AND SUBSTR(STATION_ID,2,1) IN ("+ClsCommon.GetSqlString(Convert.ToChar(Convert.ToInt32(strdgLine.ToString().Substring(4))+64).ToString())+")";
////			if (!dtSDateFrom.Equals(""))
////				StrSql = StrSql + " and to_char(creation_date,'yyyy/MM/dd')>="+ClsCommon.GetSqlString(dtSDateFrom);
////			if (!dtSDateTo.Equals(""))
////				StrSql = StrSql + " and to_char(creation_date,'yyyy/MM/dd') <="+ClsCommon.GetSqlString(dtSDateTo);

//            if (!dtSDateFrom.Equals("") && !dtSDateTo.Equals(""))
//            {
//                StrSql = StrSql + "AND CREATION_DATE BETWEEN TO_DATE("+ClsCommon.GetSqlString(dtSDateFrom)+",'yyyy/mm/dd hh24:mi') AND TO_DATE("+ClsCommon.GetSqlString(dtSDateTo)+",'yyyy/mm/dd hh24:mi') ";
//                strTestData = strTestData +" AND PDDATE BETWEEN TO_DATE("+ClsCommon.GetSqlString(dtSDateFrom)+", 'yyyy/mm/dd hh24:mi') AND TO_DATE("+ClsCommon.GetSqlString(dtSDateTo)+", 'yyyy/mm/dd hh24:mi')";
//            }

//            //string aa = Convert.ToChar(Convert.ToInt32(strdgLine.ToString().Substring(4))+64).ToString();
////			if (!strWOFrom.Trim().Equals(""))
////				StrSql = StrSql + " and wo_no >= "+ClsCommon.GetSqlString(strWOFrom);
////			if (!strWOTO.Trim().Equals(""))
////				StrSql = StrSql +" and wo_no <="+ClsCommon.GetSqlString(strWOTO);

//            StrSql = StrSql	+" AND WO_NO ="+ClsCommon.GetSqlString(strdgWO_NO)+" AND UPPER(SUBSTR(STATION_ID,1,1)||'_'||SUBSTR(STATION_ID,3,2)) = "
//                +ClsCommon.GetSqlString(strStationID) + " UNION "  + strTestData + " order by fline,fpid";
//            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
//            dgPIDDetail.DataSource = dt.DefaultView;
//            dgPIDDetail.DataBind();
//            dt.Dispose();
//        }

        private void binddataPID()
        {
            string StrSql = "SELECT FPID,FWO_NO,FSTATUS FROM (SELECT /*+RULE+*/ FPID,FWO_NO,FSTATUS,FSTATION,ROW_NUMBER() OVER(PARTITION BY FPID ORDER BY CREATION_DATE DESC ) RN FROM (SELECT  PRODUCT_ID FPID,WO_NO FWO_NO,STATE_ID FSTATUS,SUBSTR(STATION_ID,1,1)||'_'||SUBSTR(STATION_ID,3,2) FSTATION,CREATION_DATE FROM ";
            string strTestData;
            string s = strdgWO_NO.Substring(0, 1).ToUpper();
            if (s.Trim().Equals("A"))
                strTestData = "SELECT  A.PRODUCT_ID FPID,B.WO_NO FWO_NO,STATUS FSTATUS,STATION_CODE FSTATION,PDDATE CREATION_DATE FROM " + strdgModel + ".PRODUCT_HISTORY_V A,Sfc.MES_ASSY_PID_JOIN B  WHERE "
                    + " A.PRODUCT_ID=B.MAIN_ID  AND B.WO_NO=" + ClsCommon.GetSqlString(strdgWO_NO);
             else
                //strTestData = "SELECT DISTINCT PRODUCT_ID FPID,WORK_ORDER FWO_NO,UPPER(LINE_CODE) FLINE,STATUS FSTATUS FROM " + strdgModel + ".PRODUCT_HISTORY_V T WHERE "
                //    + " PRODUCT_ID LIKE " + ClsCommon.GetSqlString(strdgModel + "%")
                //    + " AND UPPER(LINE_CODE)= " + ClsCommon.GetSqlString(strdgLine)
                //    + " AND WORK_ORDER= " + ClsCommon.GetSqlString(strdgWO_NO)
                //    + " AND STATION_CODE IN (" + ClsCommon.GetSqlString(GetStation(strStationID)) + ") ";
                strTestData = "SELECT A.PRODUCT_ID FPID,B.WO_NO FWO_NO,STATUS FSTATUS,STATION_CODE FSTATION,PDDATE CREATION_DATE FROM " + strdgModel + ".PRODUCT_HISTORY_V A,Sfc.MES_PCBA_PANEL_DETAIL R, Sfc.MES_PCBA_PANEL_LINK S,SHP.CMCS_SFC_SORDER T  WHERE "
                    + " A.PRODUCT_ID=B.MAIN_ID  AND B.WO_NO= " + ClsCommon.GetSqlString(strdgWO_NO);
            switch (ClsGlobal.Flag)
            {
                case 1:
                    StrSql = StrSql + " MES_PCBA_HISTORY ";
                    break;
                case 2:
                    StrSql = StrSql + " MES_ASSY_WIP ";
                    break;
                case 3:
                    StrSql = StrSql + " MES_PACK_HISTORY ";
                    break;
            }
            StrSql = StrSql + " WHERE 1=1 ";
            //if (!strModel.Equals(""))
            if (strModel == "GNG" || strModel == "RCX" || strModel == "DVL" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN")
                StrSql = StrSql + " AND UPPER(PRODUCT_ID) IN (SELECT  S.PRODUCT_ID FROM Sfc.MES_PCBA_PANEL_DETAIL R,Sfc.MES_PCBA_PANEL_LINK S,SHP.CMCS_SFC_SORDER T WHERE S.PANEL_ID=R.PANEL_ID AND R.WO_NO=T.SORDER AND T.MODEL=" + ClsCommon.GetSqlString(strModel) + ")";
            else
                StrSql = StrSql + " AND UPPER(SUBSTR(PRODUCT_ID,1,3)) LIKE " + ClsCommon.GetSqlString("%" + strdgModel + "%");
            //if (!strLine.Trim().Equals("''")) 
            //if (strModel == "GNG" || strModel == "RCX" || strModel == "DVL" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN")
            //    StrSql = StrSql + " AND  STATION_ID =" + ClsCommon.GetSqlString(GetStation(strStationID));
            //else
            //    StrSql = StrSql + " AND SUBSTR(STATION_ID,2,1) IN (" + ClsCommon.GetSqlString(Convert.ToChar(Convert.ToInt32(strdgLine.ToString().Substring(4)) + 64).ToString()) + ")";
            //			if (!dtSDateFrom.Equals(""))
            //				StrSql = StrSql + " and to_char(creation_date,'yyyy/MM/dd')>="+ClsCommon.GetSqlString(dtSDateFrom);
            //			if (!dtSDateTo.Equals(""))
            //				StrSql = StrSql + " and to_char(creation_date,'yyyy/MM/dd') <="+ClsCommon.GetSqlString(dtSDateTo);

            if (!dtSDateFrom.Equals("") && !dtSDateTo.Equals(""))
            {
                StrSql = StrSql + "AND CREATION_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ",'yyyy/mm/dd hh24:mi') ";
                strTestData = strTestData + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(dtSDateFrom) + ", 'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(dtSDateTo) + ", 'yyyy/mm/dd hh24:mi')";
            }

            //string aa = Convert.ToChar(Convert.ToInt32(strdgLine.ToString().Substring(4))+64).ToString();
            //			if (!strWOFrom.Trim().Equals(""))
            //				StrSql = StrSql + " and wo_no >= "+ClsCommon.GetSqlString(strWOFrom);
            //			if (!strWOTO.Trim().Equals(""))
            //				StrSql = StrSql +" and wo_no <="+ClsCommon.GetSqlString(strWOTO);

            StrSql = StrSql + " AND WO_NO =" + ClsCommon.GetSqlString(strdgWO_NO) + " UNION ALL " + strTestData + " ))WHERE RN=1 AND FSTATION LIKE " + ClsCommon.GetSqlString(GetStation(strStationID));
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            dgPIDDetail.DataSource = dt.DefaultView;
            dgPIDDetail.DataBind();
            dt.Dispose();
        }

		private string GetStation(string StationID)
		{
			string strTemp = "";
            if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "RUY")
                strTemp = StationID;
            else
			    switch(StationID)
			    {	
				    case "T_BT":
					    strTemp = "DL";
					    break;
				    case "T_ET":
					    strTemp = "CA";
					    break;
				    case "T_JT":
					    strTemp = "PT";
					    break;
				    case "T_CT":
					    strTemp = "B1";
					    break;
				    case "T_NT":
					    strTemp = "B2";
					    break;
				    case "A_KT":
					    strTemp = "A1";
					    break;
				    case "A_CT":
					    strTemp = "A2";
					    break;
				    case "A_HT":
					    strTemp = "A3";
					    break;
				    case "A_IT":
					    strTemp = "A4";
					    break;
				    case "A_DT":
					    strTemp = "WL";
					    break;
				    case "A_LT":
					    strTemp = "D2";
					    break;
				    case "P_BT":
					    strTemp = "E2";
					    break;
				    default :
                        strTemp = StationID;
					    break;
			    }
			return strTemp;
		}

		private void BindListPid()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("FPID");
			dt.Columns.Add("FWO_NO");
			dt.Columns.Add("FLine");

			dgPIDDetail.DataSource = dt.DefaultView;
			dgPIDDetail.DataBind();
		}

		private void dgPIDDetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.ToUpper()=="SET")
			{
				if(dgPIDDetail.CurrentStatus=="SET")
				{
					BindListPid();
				}
				else if(dgPIDDetail.CurrentStatus=="")
				{
					//binddataPID();
					BindListPid();
				}
			}
			else 
			{
				//binddataPID();
				if (e.CommandName.ToLower() == "sort" || e.CommandName.ToLower()== "page" || e.CommandName.ToLower()== "changepage" || e.CommandName.ToLower()=="pid")
					binddataPID();
				else
					BindListPid();
			}	

			if (e.CommandName == "PID")
			{
				string strProdurct = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text;
				//Session["Lines"] = ((Label)(e.Item.Cells[0].Controls[1])).Text;
				string strScript = "<script language='jscript'>var res = window.showModalDialog('./WFrmStationDetail.aspx?PID="+strProdurct+"&MyDate=' + Date(), '','dialogWidth:450px; dialogHeight:400px; center:yes; scroll:1;"
					+"status:no;help:no');</script>" ; 
				Page.ClientScript.RegisterStartupScript(this.GetType(),"ShowStationElem", strScript);	
			}
		}

		protected void rblType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(rblType.SelectedIndex)
			{
				case 0:
					ddlType.Visible = true;
					tbType.Visible = false;
					break;
				case 1:
					ddlType.Visible = false;
					tbType.Visible = true;
					break;
			}
		}

		private void BindModel()
		{
            //string StrSql = "SELECT TRIM(MODEL)MODEL FROM CMCS_SFC_MODEL WHERE ID <>-1 ORDER BY MODEL ";
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            //ddlType.DataTextField = "Model";
            //ddlType.DataValueField = "Model";
            //ddlType.DataSource = dt.DefaultView;
            //ddlType.DataBind();

            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlType.DataTextField = "MODEL";
            ddlType.DataValueField = "MODEL";
            ddlType.DataSource = dt.DefaultView;
            ddlType.DataBind();
            ddlType.Items.Insert(0,"");
		}
	}
}
