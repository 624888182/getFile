/*************************************************************************
 * 
 *  Unit description: Top N Defect
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
	using DBAccess.EAI;
	using DB.EAI;
	using System.Reflection;
	using System.Resources;
	using System.Globalization;
	using ChartDirector;
    using System.Data.OracleClient;

	/// <summary>
	///		WFrmTopNDefect 的摘要描述。
	/// </summary>
	public partial class WFrmTopNDefectPack : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				btnDateFrom.Attributes["onclick"] = "return showCalendar('"+tbStartDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
				btnDateTo.Attributes["onclick"] = "return showCalendar('"+tbEndDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
				MultiLanguage();
				BindModel();
				tbStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")+" 08:00";
				tbEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:00";
				if (tbStartDate.Text.CompareTo(tbEndDate.Text)>0)
					tbEndDate.Text = tbStartDate.Text;
			}
			ddlRegion.SelectedIndex = 2;
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
			this.dgDefectTopN.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDefectTopN_ItemCommand);

		}
		#endregion

		private void MultiLanguage()
		{
			lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateFrom");
			lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateTo");
			lblLine.Text = (String)GetGlobalResourceObject("SFCQuery","Line");
			lblRegion.Text = (String)GetGlobalResourceObject("SFCQuery","Region");
			lblTopN.Text = (String)GetGlobalResourceObject("SFCQuery","TopN");
			lblStation.Text = (String)GetGlobalResourceObject("SFCQuery","StationID");
			lblStatisticsType.Text = (String)GetGlobalResourceObject("SFCQuery","StatisticsType");
			btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery","Query");

			ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			ViewState["ErrorInt"] = (String)GetGlobalResourceObject("SFCQuery","ErrorInt");
			ViewState["WONotExist"] = (String)GetGlobalResourceObject("SFCQuery","WONotExist");

			dgDefectTopN.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","DefectCode");
			dgDefectTopN.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","DefectDesc");
			dgDefectTopN.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Qty");
			dgDefectTopN.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","InspectionQty");
			dgDefectTopN.Columns[5].HeaderText = (String)GetGlobalResourceObject("SFCQuery","DefectRate");

			dgDefectDtail.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","WO");
			dgDefectDtail.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","StationID");
			dgDefectDtail.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","CDate");
			dgDefectDtail.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","EmpID");
			dgDefectDtail.Columns[5].HeaderText = (String)GetGlobalResourceObject("SFCQuery","DefectCode");
			dgDefectDtail.Columns[6].HeaderText = (String)GetGlobalResourceObject("SFCQuery","DefectDesc");
		}

		private void BindModel()
		{
            //string StrSql = "SELECT DISTINCT MODEL FROM CVT.MES_DUMP_MODEL";
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
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			if (tbStartDate.Text.Trim()!="" && tbEndDate.Text.Trim()!="") 
			{
				if (!ClsCommon.CheckIsDateTime(tbStartDate.Text.Trim()))
				{
					Label28.Text = ViewState["ErrorDate"].ToString();
					Label28.Visible = true;
					Label29.Visible = false;
					return;
				}
				if (!ClsCommon.CheckIsDateTime(tbEndDate.Text.Trim()))
				{
					Label29.Text = ViewState["ErrorDate"].ToString();
					Label29.Visible = true;
					Label28.Visible = false;
					return;
				}	
			}
			if (tbTopN.Text.Trim()!="" && !ClsCommon.CheckIsDigit(tbTopN.Text.Trim()))
			{
				Label1.Text = ViewState["ErrorInt"].ToString();
				Label1.Visible = true;
				return;
			}
			Label1.Visible = false;
			Label28.Visible = false;
			Label29.Visible = false;
			dgDefectDtail.Visible = false;
			Label2.Visible = true;
			Label3.Visible = false;
            WebChartViewer1.Visible = true;

			System.TimeSpan intday = Convert.ToDateTime(tbEndDate.Text.Trim()) - Convert.ToDateTime(tbStartDate.Text);
			if(intday.TotalDays > 1)
			{
				Page.ClientScript.RegisterStartupScript(this.GetType(),"DateTime","<script language=javascript>alert('選取的時間不能大於24小時！');</script>");
				return;
			}
			//GetDefectSQLString();
			//查詢條件
			string strStartDate = tbStartDate.Text.Trim();
			string strEndDate = tbEndDate.Text.Trim();
			string strLine = ddlLine.SelectedValue;
			string strTopN = tbTopN.Text.Trim();
			int intRegion = ddlRegion.SelectedIndex;
			int intType = rblType.SelectedIndex;
			int intStatisticsType = DropDownList1.SelectedIndex;
			string strModel = ddlType.SelectedValue;
			string strWO = tbType.Text.ToUpper().Trim();
			bool blRepair = CheckBox1.Checked;
			int intStationID = ddlStationID.SelectedIndex;
            int intWOType = rblWOType.SelectedIndex;
			//GetDefectSQLString();
			string ReturnMsg = "";

            OracleParameter[] orapara = new OracleParameter[] {new OracleParameter("STARTDATE",OracleType.VarChar,20),
                                                                new OracleParameter("ENDDATE",OracleType.VarChar,20),
                                                                new OracleParameter("LINE",OracleType.VarChar,7),
                                                                new OracleParameter("TOPN",OracleType.VarChar,3),
                                                                new OracleParameter("REGION",OracleType.Number),
                                                                new OracleParameter("SELECTTYPE",OracleType.Number),
                                                                new OracleParameter("STATISTICSTYPE",OracleType.Number),
                                                                new OracleParameter("STATIONINDEX",OracleType.Number),
                                                                new OracleParameter("MODEL",OracleType.VarChar,3),
                                                                new OracleParameter("WO",OracleType.VarChar,10),
                                                                new OracleParameter("WOTYPE",OracleType.Number),
                                                                new OracleParameter("REPAIR",OracleType.Number),
                                                                new OracleParameter("STATION",OracleType.VarChar,10),
                                                                new OracleParameter("DATA",OracleType.Cursor)};
            orapara[0].Value = strStartDate;
            orapara[1].Value = strEndDate;
            orapara[2].Value = strLine;
            orapara[3].Value = strTopN;
            orapara[4].Value = intRegion;
            orapara[5].Value = intType;
            orapara[6].Value = intStatisticsType;
            orapara[7].Value = intStationID;
            orapara[8].Value = strModel;
            orapara[9].Value = strWO;
            orapara[10].Value = intWOType;
            orapara[11].Value = blRepair ? 1 : 0;
            orapara[12].Value = "";
            orapara[13].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery("SFCQUERY.GETTOPNDATA", orapara).Tables[0];
            int item = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = item;
                item++;
            }

			//DataTable dt = clsDBTopNDefect.GetPACKData(strStartDate,strEndDate,strLine,strTopN,intRegion,intType,intStatisticsType,intStationID,strModel,strWO,blRepair,out ReturnMsg);
			dgDefectTopN.DataSource = dt.DefaultView;
			dgDefectTopN.DataBind();

			//Draw the Chart
			DBTable dt1 = new DBTable(dt);
			string[] DefectDesc = dt1.getColAsString(0);
			double[] DefectQty = dt1.getCol(3);
			clsDBTopNDefect.createChart(WebChartViewer1,DefectDesc,DefectQty,"ASSY TOP "+strTopN+" Defect(PCS)");
			

			if (!ReturnMsg.Equals(""))
			{
				BindList();
				Page.ClientScript.RegisterStartupScript(this.GetType(),"Error","<script language=javascript>alert("+ClsCommon.GetSqlString(ReturnMsg)+");</script>");
				return;
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

		private void BindList()
		{
			DataTable dt = new DataTable();

			dt.Columns.Add("DEFECT_CODE");
			dt.Columns.Add("DEFECT_DESC_CHT");
			dt.Columns.Add("QTY");
			dt.Columns.Add("CC");			
			dt.Columns.Add("Rate");
            			
			dgDefectTopN.DataSource = dt.DefaultView;
			dgDefectTopN.DataBind();
		}

		private void dgDefectTopN_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.ToUpper()=="SET")
			{
				if(dgDefectTopN.CurrentStatus=="SET")
				{
					BindList();
				}
				else if(dgDefectTopN.CurrentStatus=="")
				{
					btnQuery_Click(null,null);
					
				}
			}
			else 
			{
				if (e.CommandName.ToUpper()=="SORT" || e.CommandName.ToLower()== "page" || e.CommandName.ToLower()== "changepage")
					btnQuery_Click(null,null);
//				if (e.CommandName.ToUpper()!="SORT" &&e.CommandName.ToLower()!= "page" && e.CommandName.ToLower()!= "changepage")
//				{
//					string strdgWO_NO = ((Label)(e.Item.Cells[0].Controls[1])).Text;
//					//strdgModel = ((Label)(e.Item.Cells[1].Controls[1])).Text;
//					//strdgLine = ((Label)(e.Item.Cells[2].Controls[1])).Text;
//				}
			}
	
			if (e.CommandName.ToUpper()=="QTY")
			{
				dgDefectDtail.Visible = true;
				Label3.Visible = true;

				string strStartDate = tbStartDate.Text.Trim();
				string strEndDate = tbEndDate.Text.Trim();
				string strLine = ddlLine.SelectedValue;
				string strTopN = tbTopN.Text.Trim();
				int intRegion = ddlRegion.SelectedIndex;
				int intType = rblType.SelectedIndex;
				int intStatisticsType = DropDownList1.SelectedIndex;
				string strModel = ddlType.SelectedValue;
				string strWO = tbType.Text.ToUpper().Trim();
				bool blRepair = CheckBox1.Checked;
                int intWOType = rblWOType.SelectedIndex;
                int intStationID = ddlStationID.SelectedIndex;

                string strDefectCode = ((Label)(e.Item.Cells[1].Controls[1])).Text;
                string strDefectMsg = ((Label)(e.Item.Cells[2].Controls[1])).Text;

                //string strSql = clsDBTopNDefect.GetDetailData(strStartDate,strEndDate,strLine,strTopN,intRegion,intType,intStatisticsType,0,strModel,strWO,blRepair,strDefectCode,strDefectMsg);
                //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                OracleParameter[] orapara = new OracleParameter[] {new OracleParameter("STARTDATE",OracleType.VarChar,20),
                                                                new OracleParameter("ENDDATE",OracleType.VarChar,20),
                                                                new OracleParameter("LINE",OracleType.VarChar,7),
                                                                new OracleParameter("TOPN",OracleType.VarChar,3),
                                                                new OracleParameter("REGION",OracleType.Number),
                                                                new OracleParameter("SELECTTYPE",OracleType.Number),
                                                                new OracleParameter("STATISTICSTYPE",OracleType.Number),
                                                                new OracleParameter("STATIONINDEX",OracleType.Number),
                                                                new OracleParameter("MODEL",OracleType.VarChar,3),
                                                                new OracleParameter("WO",OracleType.VarChar,10),
                                                                new OracleParameter("WOTYPE",OracleType.Number),
                                                                new OracleParameter("REPAIR",OracleType.Number),
                                                                new OracleParameter("DEFECTCODE",OracleType.VarChar,20),
                                                                new OracleParameter("DEFECTMSG",OracleType.VarChar,100),
                                                                new OracleParameter("STATION",OracleType.VarChar,10),
                                                                new OracleParameter("DATA",OracleType.Cursor)};
                orapara[0].Value = strStartDate;
                orapara[1].Value = strEndDate;
                orapara[2].Value = strLine;
                orapara[3].Value = strTopN;
                orapara[4].Value = intRegion;
                orapara[5].Value = intType;
                orapara[6].Value = intStatisticsType;
                orapara[7].Value = intStationID;
                orapara[8].Value = strModel;
                orapara[9].Value = strWO;
                orapara[10].Value = intWOType;
                orapara[11].Value = blRepair ? 1 : 0;
                orapara[12].Value = strDefectCode;
                orapara[13].Value = strDefectMsg;
                orapara[14].Value = "";
                orapara[15].Direction = ParameterDirection.Output;
                DataTable dt = ClsGlobal.objDataConnect.DataQuery("SFCQUERY.GETTOPNDETAILDATA", orapara).Tables[0];
				dgDefectDtail.DataSource = dt.DefaultView;
				dgDefectDtail.DataBind();
			}
		}
	}
}
