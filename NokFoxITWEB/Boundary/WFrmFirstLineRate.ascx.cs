namespace SFCQuery.Boundary
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using ChartDirector;
	using DBAccess.EAI;
	using DB.EAI;
	using System.Reflection;
	using System.Resources;
    using System.Data.OracleClient;

	/// <summary>
	///		WFrmFirstLineRate ���K�n�y�z�C
	/// </summary>
	public partial class WFrmFirstLineRate : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
			if (!IsPostBack)
			{
				tbStartDate.DateTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")+" 08:20";
				tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:20";
				InitialDDL();
				MultiLanaguage();
                Panel1.Visible = false;
                Panel2.Visible = false;
			}
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
		///		�����]�p�u��䴩�ҥ�������k - �ФŨϥε{���X�s�边�ק�
		///		�o�Ӥ�k�����e�C
		/// </summary>
		private void InitializeComponent()
		{
			this.dgFirstLineRate.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgFirstLineRate_PageIndexChanged);

		}
		#endregion

		private void InitialDDL()
		{
            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlModel.DataTextField = "MODEL";
            ddlModel.DataValueField = "MODEL";
            ddlModel.DataSource = dt.DefaultView;
            ddlModel.DataBind();

            //string strSql = "SELECT MODEL FROM CMCS_SFC_MODEL ORDER BY MODEL";
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            //ddlModel.DataTextField = "MODEL";
            //ddlModel.DataValueField = "MODEL";
            //ddlModel.DataSource = dt.DefaultView;
            //ddlModel.DataBind();
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
            RadioButtonList2.Items[0].Text = (String)GetGlobalResourceObject("SFCQuery", "LineRate");
            RadioButtonList2.Items[1].Text = (String)GetGlobalResourceObject("SFCQuery", "LineFirstRate");

			dgFirstLineRate.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Line");
			dgFirstLineRate.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","PassQty");
			dgFirstLineRate.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","FailQty");
			dgFirstLineRate.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","TotalQty");
			dgFirstLineRate.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","LinesYield");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			dgFirstLineRate.CurrentPageIndex = 0 ;
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

            System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
            if (intday.TotalDays > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('������ɶ�����j��24�p�ɡI');</script>");
                return;
            }

			Label28.Visible = false;
			Label29.Visible = false;
			
            switch (RadioButtonList2.SelectedIndex)
            {
                case 0 :
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    GetLineRate(1, dgSMTLineRate);
                    GetLineRate(2, dgAssyLineRate);
                    break;
                case 1 :
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    GetFirstLineRate(RadioButtonList1.SelectedIndex);
                    break;
            }
			
		}

		private void dgFirstLineRate_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgFirstLineRate.CurrentPageIndex = e.NewPageIndex;
			GetFirstLineRate(RadioButtonList1.SelectedIndex);
		}

		private void GetFirstLineRate(int type)
		{
			string strStartDate = tbStartDate.DateTextBox.Text.Trim();
			string strEndDate = tbEndDate.DateTextBox.Text.Trim();
			string strModel = ddlModel.SelectedValue.Trim().ToUpper();

			string strSql = "SELECT LINEID,FAILQTY,PASSQTY,FAILQTY+PASSQTY TOTALQTY,ROUND(PASSQTY/(FAILQTY+PASSQTY)*100,2)YIELD FROM( "
				+" SELECT LINEID,SUM(DECODE(STATUS,'F',QTY,0))FAILQTY,SUM(DECODE(STATUS,'P',QTY,0))PASSQTY FROM "
                + " (SELECT /*+RULE*/LINEID,STATUS,COUNT(*)QTY FROM(SELECT LINEID,STATUS,PRODUCT_ID FROM( SELECT LINE_CODE LINEID, STATUS," 
                +" PRODUCT_ID,ROW_NUMBER() OVER(PARTITION BY PRODUCT_ID ORDER BY PDDATE ASC) RN "
				+" FROM "+strModel+".PRODUCT_HISTORY_V S WHERE STATION_CODE IN ";
			if(type.Equals(0)) strSql = strSql +" ('DL', 'CA', 'PT', 'B1', 'B2', 'B3', 'BT','ECAL','EPT','CAL') ";
            else strSql = strSql + "('A1','A2','A3','A4','A5','WL','D2','E2', 'BTWL') ";
			strSql =strSql +" AND PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI:SS') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI:SS') "
                //+" AND PDDATE =(SELECT MIN(PDDATE) FROM "+strModel+".PRODUCT_HISTORY_V T WHERE S.PRODUCT_ID = T.PRODUCT_ID "
                //+" AND PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI:SS') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI:SS') "
                + " AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0) WHERE RN =1 )GROUP BY LINEID,STATUS   ) GROUP BY LINEID)";
//			string strSql = "SELECT LINE_CODE LINEID,PASSQTY,FAILQTY,(PASSQTY+FAILQTY)TOTALQTY,ROUND(PASSQTY/(PASSQTY+FAILQTY)*100,2) YIELD FROM ( "
//				+" SELECT S.LINE_CODE,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY FROM ( "
//				+" SELECT LINE_CODE,COUNT(*)PASSQTY FROM "+strModel+".PRODUCT_HISTORY_V  WHERE PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
//				+" AND STATUS = 'P' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 GROUP BY LINE_CODE) S, "
//				+" (SELECT LINE_CODE,COUNT(*)FAILQTY FROM "+strModel+".PRODUCT_HISTORY_V WHERE PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND  TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
//				+" AND STATUS = 'F' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 GROUP BY LINE_CODE) T "
//				+" WHERE S.LINE_CODE = T.LINE_CODE(+)) ";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			dgFirstLineRate.DataSource = dt.DefaultView;
			dgFirstLineRate.DataBind();

			//if the station is not empty,draw the chart
			if(dt.Rows.Count>0)
			{
				DBTable dbdt = new DBTable(dt);
				string[] XLabel = dbdt.getColAsString(0);
				double[] XValues = dbdt.getCol(4);			
				clsDBTopNDefect.createChart(wcvFirstLineRate,XLabel,XValues,"Lines Rate(%)");
			}
		}

        private void GetLineRate(int PhaseType, DataGrid dg)
        {
            string strStartDate = tbStartDate.DateTextBox.Text.Trim();
            string strEndDate = tbEndDate.DateTextBox.Text.Trim();
            string strModel = ddlModel.SelectedValue.Trim().ToUpper();

            string strSql;
            string strTemp = "";
            WebChartViewer wcv = null;
            switch (PhaseType)
            {
                case 1:
                    strTemp = " AND STATION_CODE IN ('DL', 'CA', 'PT', 'B1', 'B2', 'B3', 'BT','ECAL','EPT','CAL','BD','PW','FL','BD','SN') ";
                    wcv = wcvSMTLineRate;
                    break;
                case 2:
                    strTemp = " AND STATION_CODE IN ('WL','D2','A1','A2','A3','A4','A5','E2','BTWL','PH','PRT','CI','BL','HT','CT','CM','FC','AD','PK','RA','QA','WT','CF','WLA','GP') ";
                    wcv = wcvAssyLineRate;
                    break;
            }
            if(strModel=="HAIER"||strModel=="RCX"||strModel=="DVR"||strModel=="GNG"||strModel=="DVL"||strModel=="SLG"||strModel=="TWN")
                strSql = "SELECT LINE_CODE LINEID,PASSQTY,FAILQTY,(PASSQTY+FAILQTY)TOTALQTY,ROUND(PASSQTY/(PASSQTY+FAILQTY)*100,2) YIELD FROM ( "
                    + " SELECT S.LINE_CODE,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY FROM ( "
                    + " SELECT LINE_CODE,COUNT(*)PASSQTY FROM " + strModel + ".PRODUCT_HISTORY_V  WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'P'  AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE) S, "
                    + " (SELECT LINE_CODE,COUNT(*)FAILQTY FROM " + strModel + ".PRODUCT_HISTORY_V WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND  TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'F' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE) T "

                    //+ " WHERE S.LINE_CODE = T.LINE_CODE(+)) ";   ���u�O�ɡA�A�ϥ�
                    + " ) "; 
            else
                strSql = "SELECT LINE_CODE LINEID,PASSQTY,FAILQTY,(PASSQTY+FAILQTY)TOTALQTY,ROUND(PASSQTY/(PASSQTY+FAILQTY)*100,2) YIELD FROM ( "
                    + " SELECT S.LINE_CODE,S.PASSQTY,NVL(T.FAILQTY,0)FAILQTY FROM ( "
                    + " SELECT LINE_CODE,COUNT(*)PASSQTY FROM " + strModel + ".PRODUCT_HISTORY_V  WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'P' AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE) S, "
                    + " (SELECT LINE_CODE,COUNT(*)FAILQTY FROM " + strModel + ".PRODUCT_HISTORY_V WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND  TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                    + " AND STATUS = 'F'  AND UPPER(EMPLOYEE) <> 'REPAIR' AND REPAIR = 0 " + strTemp + " GROUP BY LINE_CODE) T " 
                    + " WHERE S.LINE_CODE = T.LINE_CODE(+)) ";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            dg.DataSource = dt.DefaultView;
            dg.DataBind();

            //if the station is not empty,draw the chart
            if (dt.Rows.Count > 0)
            {
                DBTable dbdt = new DBTable(dt);
                string[] XLabel = dbdt.getColAsString(0);
                double[] XValues = dbdt.getCol(4);
                clsDBTopNDefect.createChart(wcv, XLabel, XValues, "Lines Rate(%)");
                wcv.Visible = true;
            }
        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RadioButtonList2.SelectedIndex)
            {
                case 0:
                    RadioButtonList1.Visible = false;
                    break;
                case 1 :
                    RadioButtonList1.Visible = true;
                    break;
            }
        }
        protected void dgSMTLineRate_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgSMTLineRate.CurrentPageIndex = e.NewPageIndex;
            GetLineRate(1, dgSMTLineRate);
        }
        protected void dgAssyLineRate_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgAssyLineRate.CurrentPageIndex = e.NewPageIndex;
            GetLineRate(2, dgAssyLineRate);
        }
}
}
