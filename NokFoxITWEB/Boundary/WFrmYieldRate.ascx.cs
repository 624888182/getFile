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
    using DB.EAI;
    using System.Threading;
    using ChartDirector;
    using System.Data.OracleClient;

    /// <summary>
    ///		WFrmYieldRate 的摘要描述。
    /// </summary>
    public partial class WFrmYieldRate : System.Web.UI.UserControl
    {
        //private static ResourceManager rm = new ResourceManager("SFCQuery.MultiLanguage.SFCQuery",Assembly.GetExecutingAssembly());

        protected void Page_Load(object sender, System.EventArgs e)
        {
            tbStartDate.Language = Calendar1.Languages.TraditionalChinese;
            string temp = tbStartDate.ThemePath;
            temp = tbStartDate.ThemeTitle;
            temp = tbStartDate.LayoutPath;
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(WFrmYieldRate));
            // 在這裡放置使用者程式碼以初始化網頁
            if (!IsPostBack)
            {
                //btnDateFrom.Attributes["onclick"] = "return showCalendar('"+tbStartDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
                //btnDateTo.Attributes["onclick"] = "return showCalendar('"+tbEndDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
                MultiLanguage();
                BindModel();
                //tbStartDate.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:00";
                //tbEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
                tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                    tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
            }
            btnQuery.Attributes["onclick"] = "go();";
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
            //modified by shujianbo at 2007/11/28  upgrade to vs 2005
            lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");//rm.GetString("DateFrom");
            lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");//rm.GetString("DateTo");
            lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");//rm.GetString("Line");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");
            lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
            ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
            ViewState["WONotExist"] = (String)GetGlobalResourceObject("SFCQuery", "WONotExist");//rm.GetString("WONotExist");
        }

        private void BindModel()
        {
            //string StrSql = "SELECT DISTINCT MODEL FROM CVT.MES_DUMP_MODEL";
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            //ddlModel.DataTextField = "Model";
            //ddlModel.DataValueField = "Model";
            //ddlModel.DataSource = dt.DefaultView;
            //ddlModel.DataBind();

            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlModel.DataTextField = "MODEL";
            ddlModel.DataValueField = "MODEL";
            ddlModel.DataSource = dt.DefaultView;
            ddlModel.DataBind();
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
            WebChartViewer1.Visible = true;
            WebChartViewer2.Visible = true;

            System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
            //if (intday.TotalDays > 1)
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於24小時！');</script>");
            //    return;
            //}

            string strModel = ddlModel.SelectedValue.ToUpper();

            DataTable dt = null;
                       
            lock (o)
            {
                //System.Threading.Thread.Sleep(30000);
                dt = this.GetData();
            }
            string[] lblStation = new string[dt.Rows.Count];
            double[] FirstYield = new double[dt.Rows.Count];
            double[] FinalYield = new double[dt.Rows.Count];
            double[] RetestRate = new double[dt.Rows.Count];
            int rowCount = 0;

            DataRow[] dr = dt.Select("", "SortCol");
            foreach (DataRow rw in dr)
            {
                lblStation[rowCount] = rw[0].ToString();
                FirstYield[rowCount] = Convert.ToDouble(rw[3].ToString().Substring(0, rw[3].ToString().IndexOf("%")));
                FinalYield[rowCount] = Convert.ToDouble(rw[5].ToString().Substring(0, rw[5].ToString().IndexOf("%")));
                RetestRate[rowCount] = Convert.ToDouble(rw[10].ToString().Substring(0, rw[10].ToString().IndexOf("%")));
                rowCount++;
            }
            switch (rblChartType.SelectedIndex)
            {
                case 0:
                    createLineChart(WebChartViewer1, lblStation, FirstYield, FinalYield, "First Yield");
                    createLineChart(WebChartViewer2, lblStation, RetestRate, null, "Retest Rate");
                    break;
                case 1:
                    createBarChart(WebChartViewer1, lblStation, FirstYield, FinalYield, "First Yield");
                    createBarChart(WebChartViewer2, lblStation, RetestRate, null, "Retest Rate");
                    break;
            }

            //			double[] software = new double[]{500,800,700,455,655,455,879,632,156,489,100,599};
            //			double[] hardware = new double[]{600,700,600,655,555,355,679,732,256,589,200,699};
            //			double[] services = new double[]{700,900,800,555,755,555,979,832,356,689,300,799};

            //createChart1(WebChartViewer1, "2007", software, hardware,services);

            //DrawChart();
            //			ThreadStart threadPopup = new ThreadStart(PopupWinShow);
            //			Thread fThread_1 = new Thread(threadPopup);
            //			fThread_1.Name = "PopupWinShow";
            //			fThread_1.Start();
            //
            //			ThreadStart threadGetData = new ThreadStart(GetData);
            //			Thread fThread_2 = new Thread(threadGetData);
            //			fThread_2.Name = "GetData";
            //			fThread_2.Start();


            //			PopupWin1.AutoShow = true ;
            //			PopupWin1.Visible = true;

            //ADD BY SHUJIANBO AT 2007/08/24
            //			DateTime dttmStartDate = Convert.ToDateTime(tbStartDate.Text);
            //			DateTime dttmEndDate = Convert.ToDateTime(tbEndDate.Text);
            //			string strModel = ddlModel.SelectedValue.ToUpper();
            //			string strLine = ddlLine.SelectedValue.ToUpper();
            //			bool blRepair = ckbRepair.Checked;
            //			clsDBYieldData objYieldData = new clsDBYieldData();
            //			DataTable dt = objYieldData.ShowYield(strModel,dttmStartDate,dttmEndDate,strLine,blRepair);
            //			DataGrid1.DataSource = dt.DefaultView;
            //			DataGrid1.DataBind();

            //ShowYield();
            //ShowYield();
            //一次直通率
            lbSMTFirstPass.Text = GetSMTFirstPass(3, strModel);
            lbASSYFirstPass.Text = GetAssyFirstPass(3, strModel);
            lbTotalFirstPass.Text = (Convert.ToString(Math.Round((Convert.ToDouble(lbSMTFirstPass.Text.Substring(0, lbSMTFirstPass.Text.Length - 1)) / 100) * (Convert.ToDouble(lbASSYFirstPass.Text.Substring(0, lbASSYFirstPass.Text.Length - 1)) / 100) * 100, 2)) + "%");
            //最終直通率
            lbSMTFinalPass.Text = GetSMTFirstPass(5, strModel);
            lbASSYFinalPass.Text = GetAssyFirstPass(5, strModel);
            lbTotalFinalPass.Text = (Convert.ToString(Math.Round((Convert.ToDouble(lbSMTFinalPass.Text.Substring(0, lbSMTFinalPass.Text.Length - 1)) / 100) * (Convert.ToDouble(lbASSYFinalPass.Text.Substring(0, lbASSYFinalPass.Text.Length - 1)) / 100) * 100, 2)) + "%");
            //一次平均良率
            lbFirstSMTAverageYied.Text = GetSMTAverageYied(3, strModel);
            lbFirstASSYAverageYied.Text = GetASSAverageYied(3, strModel);
            lbFirstTotalAverageYied.Text = Convert.ToString(Math.Round((Convert.ToDouble(lbFirstSMTAverageYied.Text.Substring(0, lbFirstSMTAverageYied.Text.Length - 1)) + Convert.ToDouble(lbFirstASSYAverageYied.Text.Substring(0, lbFirstASSYAverageYied.Text.Length - 1))) / 2, 2)) + "%";
            //最終平均良率
            lbFinalSMTAverageYied.Text = GetSMTAverageYied(5, strModel);
            lbFinalASSYAverageYied.Text = GetASSAverageYied(5, strModel);
            lbFinalTotalAverageYied.Text = Convert.ToString(Math.Round((Convert.ToDouble(lbFinalSMTAverageYied.Text.Substring(0, lbFinalSMTAverageYied.Text.Length - 1)) + Convert.ToDouble(lbFinalASSYAverageYied.Text.Substring(0, lbFinalASSYAverageYied.Text.Length - 1))) / 2, 2)) + "%";
            //重測率
            lbSMTRetest.Text = GetSMTRetestYied(strModel).ToString() + "%";
            lbASSYRetest.Text = GetASSYRetestYied(strModel).ToString() + "%";
            lbTotalRetest.Text = Convert.ToString(Math.Round((GetSMTRetestYied(strModel) + GetASSYRetestYied(strModel)) / 2, 2)) + "%";
        }

        //		private void PopupWinShow()
        //		{
        //			PopupWin1.AutoShow = true ;
        //			PopupWin1.Visible = true;
        //		}
        //[AjaxPro.AjaxMethod]

        //*****************add by jinwu at 2008/3/13**************************
        //獲得SMT重測率
        private double GetSMTRetestYied(string strModel)
        {
            double smtRetestYied;
            double sum = 0;
            double average;
            int count = 0;
            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "MRO" || strModel == "MRE")
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PW" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BD")
                    {
                        sum = sum + Convert.ToDouble(((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Substring(0, ((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Length - 1));
                        count++;
                    }

                }
                else
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "DL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PT" 
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B1" 
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B3" 
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BT")
                    {
                        sum = sum + Convert.ToDouble(((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Substring(0, ((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Length - 1));
                        count++;
                    }

                }
            }
            if (count == 0)
                count = 1;
            average = Math.Round(sum / count, 2);
            smtRetestYied = average;
            return smtRetestYied;
        }
        //獲得ASSY重測率
        private double GetASSYRetestYied(string strModel)
        {
            double assyRetestYied;
            double sum = 0;
            double average;
            int count = 0;
            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "MRO" || strModel == "MRE")
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PH" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PRT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CI" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "HT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CT"
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CM" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FC" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "AD" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PK" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "RA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "QA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CF"
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WLA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "GP")
                    {
                        sum = sum + Convert.ToDouble(((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Substring(0, ((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Length - 1));
                        count++;
                    }
                }
                else
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A1" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A3" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BTWL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "D2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FQC" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "E2")
                    {
                        sum = sum + Convert.ToDouble(((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Substring(0, ((Label)DataGrid1.Items[i].Cells[10].FindControl("Label2")).Text.Length - 1));
                        count++;
                    }
                }

            }
            if (count == 0)
                count = 1;
            average = Math.Round(sum / count, 2);
            assyRetestYied = average;
            return assyRetestYied;
        }

        //獲得ASSY平均良率
        private string GetASSAverageYied(int a, string strModel)
        {
            string assyAverageYied;
            double sum = 0;
            double average;
            int count = 0;
            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "MRO" || strModel == "MRE")
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PH" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PRT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CI" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "HT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CT"
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CM" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FC" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "AD" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PK" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "RA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "QA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CF"
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WLA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "GP")
                    {
                        sum = sum + Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1)));
                        count++;
                    }
                }
                else
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A1" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A3" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BTWL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "D2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FQC" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "E2")
                    {
                        sum = sum + Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1)));
                        count++;
                    }
                }

            }
            if (count == 0)
                count = 1;
            average = Math.Round(sum / count, 2);
            assyAverageYied = Convert.ToString(average) + "%";
            return assyAverageYied;
        }
        //獲得SMT平均良率
        private string GetSMTAverageYied(int a, string strModel)
        {
            string smtAverageYied;
            double sum = 0;
            double average;
            int count = 0;
            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "MRO" || strModel == "MRE")
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PW" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BD")
                    {
                        sum = sum + Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1)));
                        count++;
                    }
                }
                else
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "DL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B1" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B3" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BD" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "SN")
                    {
                        sum = sum + Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1)));
                        count++;
                    }
                }

            }
            if (count == 0)
                count = 1;
            average = Math.Round(sum / count, 2);
            smtAverageYied = Convert.ToString(average) + "%";
            return smtAverageYied;
        }
        //獲得SMT直通率
        private string GetSMTFirstPass(int a, string strModel)
        {
            string smtFirstPassnum;
            double multi = 1;

            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "MRO" || strModel == "MRE")
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PW" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BD")
                        multi = multi * Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1))) / 100;

                }
                else
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "DL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CA" 
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B1" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "B3" 
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BD" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "SN"
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "SN")
                        multi = multi * Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1))) / 100;

                }

            }
            multi = Math.Round(multi * 100, 2);
            smtFirstPassnum = Convert.ToString(multi) + "%";
            return smtFirstPassnum;
        }
        //獲得ASSY直通率
        private string GetAssyFirstPass(int a, string strModel)
        {
            string assyFirstPass;
            double multi = 1;

            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "MRO" || strModel == "MRE")
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PH" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PRT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CI" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "HT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CT"
                          || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CM" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FC" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "AD" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "PK" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "RA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "QA" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WT" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "CF"
                        || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "GP" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WLA")
                        multi = multi * Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1))) / 100;
                }
                else
                {
                    if (((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A1" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "A3" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "WL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "BTWL" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "D2" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "FQC" || ((Label)DataGrid1.Items[i].Cells[0].FindControl("label1")).Text == "E2")
                        multi = multi * Convert.ToDouble((DataGrid1.Items[i].Cells[a].Text.Substring(0, DataGrid1.Items[i].Cells[a].Text.Length - 1))) / 100;
                }
            }
            multi = Math.Round(multi * 100, 2);
            assyFirstPass = Convert.ToString(multi) + "%";
            return assyFirstPass;
        }
        //*********************************************************************
        private object o = new object();
        private DataTable GetData()
        {
            //ADD BY SHUJIANBO AT 2007/08/24
            DateTime dttmStartDate = Convert.ToDateTime(tbStartDate.DateTextBox.Text);
            DateTime dttmEndDate = Convert.ToDateTime(tbEndDate.DateTextBox.Text);
            string strModel = ddlModel.SelectedValue.ToUpper();
            string strLine = ddlLine.SelectedValue.ToUpper();
            bool blRepair = ckbRepair.Checked;
            int intWOType = rblWOType.SelectedIndex;
            clsDBYieldData objYieldData = new clsDBYieldData();
            DataTable dt = objYieldData.ShowYield(strModel, dttmStartDate, dttmEndDate, strLine, blRepair, intWOType, "");
            dt.DefaultView.Sort = "SortCol";
            DataGrid1.DataSource = dt.DefaultView;
            DataGrid1.DataBind();
            return dt;
        }

        private void createLineChart(WebChartViewer viewer, string[] lblStation, double[] FirstYieldData, double[] FinalYieldData, string strTitle)
        //double[] software, double[] hardware, double[] services)
        {
            // Create a XYChart object of size 600 x 300 pixels, with a light grey (eeeeee)
            // background, black border, 1 pixel 3D border effect and rounded corners.
            XYChart c = new XYChart(600, 300, 0xeeeeee, 0x000000, 1);
            c.setRoundedFrame();

            // Set the plotarea at (60, 60) and of size 520 x 200 pixels. Set background
            // color to white (ffffff) and border and grid colors to grey (cccccc)
            c.setPlotArea(60, 60, 520, 200, 0xffffff, -1, 0xcccccc, 0xccccccc);

            // Add a title to the chart using 15pts Times Bold Italic font, with a light blue
            // (ccccff) background and with glass lighting effects.
            c.addTitle(strTitle,
                "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
                Chart.glassEffect());

            // Add a legend box at (70, 32) (top of the plotarea) with 9pts Arial Bold font
            c.addLegend(70, 32, false, "Arial Bold", 9).setBackground(Chart.Transparent);

            // Add a line chart layer using the supplied data
            LineLayer layer = c.addLineLayer2();

            ChartDirector.TextBox textbox = layer.addDataSet(FirstYieldData, 0xff0000, strTitle).setDataLabelStyle("Arial Bold Italic");

            // Set the data label font color for the  data set to yellow (0x0000FF)
            textbox.setFontColor(0x0000ff);
            //Set the Node's Style
            layer.getDataSet(0).setDataSymbol(Chart.DiamondShape, 11);

            if (FinalYieldData != null)
            {
                c.addTitle("First Yield And Final Yield",
                    "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
                    Chart.glassEffect());
                textbox = layer.addDataSet(FinalYieldData, 0x00ff00, "Final Yield").setDataLabelStyle("Arial Bold Italic");

                // Set the data label font color for the  data set to yellow (0x0000FF)
                textbox.setFontColor(0x00ff00);
                layer.getDataSet(1).setDataSymbol(Chart.CircleShape, 9);
            }
            //			layer.addDataSet(hardware, 0x00ff00, "Hardware").setDataSymbol(
            //				Chart.DiamondShape, 11);
            //			layer.addDataSet(services, 0xffaa00, "Services").setDataSymbol(Chart.Cross2Shape(
            //				), 11);

            // Set the line width to 3 pixels
            layer.setLineWidth(3);

            c.xAxis().setLabels(lblStation);

            // Set the y axis title
            c.yAxis().setTitle("良率百分比(%)");

            // Set axes width to 2 pixels
            c.xAxis().setWidth(2);
            c.yAxis().setWidth(2);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "",
                "title='{dataSetName}  for {xLabel} =  {value}%'");
        }

        // private void createChart(WebChartViewer viewer, string[] XLabel, double[] Values, string strTitle)
        private void createBarChart(WebChartViewer viewer, string[] lblStation, double[] FirstYieldData, double[] FinalYieldData, string strTitle)
        //double[] software, double[] hardware, double[] services)
        {
            // Create a XYChart object of size 600 x 300 pixels, with a light grey (eeeeee)
            // background, black border, 1 pixel 3D border effect and rounded corners.
            XYChart c = new XYChart(600, 300, 0xeeeeee, 0x000000, 1);
            c.setRoundedFrame();

            // Set the plotarea at (60, 60) and of size 520 x 200 pixels. Set background
            // color to white (ffffff) and border and grid colors to grey (cccccc)
            //c.setPlotArea(60, 60, 520, 200, c.linearGradientColor(60, 40, 60, 280, 0xeeeeff, 0x0000cc), -1, 0xffffff, 0xffffff);// 0xcccccc, 0xccccccc);
            c.setPlotArea(60, 60, 520, 200, 0xffffff, -1, 0xcccccc, 0xccccccc);

            // Add a title to the chart using 15pts Times Bold Italic font, with a light blue
            // (ccccff) background and with glass lighting effects.
            c.addTitle(strTitle,
                "Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
                Chart.glassEffect());

            // Add a legend box at (70, 32) (top of the plotarea) with 9pts Arial Bold font
            c.addLegend(70, 32, false, "Arial Bold", 9).setBackground(Chart.Transparent);

            // Add a line chart layer using the supplied data
            //LineLayer layer = c.addLineLayer2();
            //BarLayer layer = c.addBarLayer3(Values);

            // Add a multi-color bar chart layer using the supplied data
            BarLayer layer = c.addBarLayer2(Chart.Side, 3);


            //ChartDirector.TextBox textbox = 
            layer.addDataSet(FirstYieldData, 0x80ff80, strTitle).setDataLabelStyle("Arial Bold Italic");
            if (FinalYieldData != null)
                layer.addDataSet(FinalYieldData, 0x8080ff, "Final Yield").setDataLabelStyle("Arial Bold Italic");


            //Set to 3D
            //layer.set3D();

            // Set the data label font color for the  data set to yellow (0x0000FF)
            //textbox.setFontColor(0x0000ff);
            //Set the Node's Style
            layer.getDataSet(0).setDataSymbol(Chart.DiamondShape, 11);

            // Use glass lighting effect with light direction from the left
            layer.setBorderColor(Chart.Transparent, Chart.glassEffect(Chart.NormalGlare,
                Chart.Left));


            // Set the line width to 3 pixels
            layer.setLineWidth(3);

            //c.xAxis().
            c.xAxis().setLabels(lblStation);

            // Set the y axis title
            c.yAxis().setTitle(strTitle);

            // Set the labels on the x axis. Rotate the labels by 60 degrees.
            c.xAxis().setLabels(lblStation).setFontAngle(345);

            // Set axes width to 2 pixels
            c.xAxis().setWidth(2);
            c.yAxis().setWidth(2);

            // Set all axes to transparent
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);
            c.yAxis2().setColors(Chart.Transparent);


            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "",
                "title='{dataSetName}  for {xLabel} =  {value}%'");
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName.ToUpper().Trim().Equals("RETESTRATE"))
            {
                string strStartDate = tbStartDate.DateTextBox.Text.Trim();
                string strEndDate = tbEndDate.DateTextBox.Text.Trim();
                string strLine = ddlLine.SelectedValue;
                //string strTopN = "3";
                string strModel = ddlModel.SelectedValue;
                bool blRepair = ckbRepair.Checked;
                int intWOType = rblWOType.SelectedIndex;
                string strStation = ((Label)(e.Item.Cells[0].Controls[1])).Text;

                string strScript = "<script language='jscript'>var res = window.open('./WFrmModalDialog.aspx?Url=./WFrmTopNDefectSMT.ascx&StartDate="
                    + strStartDate + "&EndDate=" + strEndDate + "&Line=" + strLine + "&TopN=3&Model=" + strModel + "&Repair=" + blRepair.ToString() + "&WOType="
                    + intWOType.ToString() + "&Station=" + strStation + "','_blank', '');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Top3", strScript);

                //string strScript = "<script language='jscript'>var res = window.open('./WFrmModalDialog.aspx?Url=./WFrmRetestDetail.ascx&StartDate="
                //    + strStartDate + "&EndDate=" + strEndDate + "&Line=" + strLine + "&TopN=3&Model=" + strModel + "&Repair=" + blRepair.ToString() + "&WOType="
                //    + intWOType.ToString() + "&Station=" + strStation + "','_blank', '');</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Top3", strScript);	
            }
        }
    }
}
