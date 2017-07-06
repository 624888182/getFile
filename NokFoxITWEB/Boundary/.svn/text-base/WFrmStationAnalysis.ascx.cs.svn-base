/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/09/11
 *  Modifier : Shu Jian Bo             Date: 2007/09/11
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
	using DB.EAI;
	using ChartDirector;
    using Excel = Microsoft.Office.Interop.Excel;

	/// <summary>
	///		WFrmMachineRate 的摘要描述。
	/// </summary>
	public partial class WFrmStationAnalysis : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				tbStartDate.DateTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")+" 08:20";
				tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:20";
				InitialDDL();
				MultiLanaguage();				
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
			this.dgMachineRate.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgMachineRate_PageIndexChanged);
		}
		#endregion

		private void InitialDDL()
		{
			string strSql = "SELECT MODEL FROM CMCS_SFC_MODEL ORDER BY MODEL";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			ddlModel.DataTextField = "MODEL";
			ddlModel.DataValueField = "MODEL";
			ddlModel.DataSource = dt.DefaultView;
			ddlModel.DataBind();

			strSql = "SELECT T.STATION_ID,T.DESCRIPTION FROM MES_COMM_STATION_DESCRIPTION T WHERE TESTSTATION = 'Y' UNION SELECT 'FQC','FQC' FROM DUAL ORDER BY DESCRIPTION ";
			dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			ddlStation.DataTextField = "DESCRIPTION";
			ddlStation.DataValueField = "STATION_ID";
			ddlStation.DataSource = dt.DefaultView;
			ddlStation.DataBind();

			ddlStation.Items.Insert(0,"");
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
			lblStation.Text = (String)GetGlobalResourceObject("SFCQuery","StationID");

			dgMachineRate.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Employee");
            dgMachineRate.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "LineID");
			dgMachineRate.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery","FixtureID");
			dgMachineRate.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery","FailureItem");
			dgMachineRate.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery","Qty");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			dgMachineRate.CurrentPageIndex = 0;
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


			Label28.Visible = false;
			Label29.Visible = false;
			GetData();
		}

		private void GetData()
		{
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetSqlString()).Tables[0];
			dgMachineRate.DataSource = dt.DefaultView;
			dgMachineRate.DataBind();
		}

        private string GetSqlString()
        {
            string strStationID = ddlStation.SelectedValue.Trim().ToString().ToUpper();
            string strModel = ddlModel.SelectedValue.Trim().ToUpper();
            string strStartDate = tbStartDate.DateTextBox.Text.Trim();
            string strEndDate = tbEndDate.DateTextBox.Text.Trim();

            //string strTableName = "";
            //string strListColumnName = "";
            //string strGroupColumnName = "";
            //string strDateColumnName = "";

            //switch (strStationID)
            //{
            //    case "DL":
            //        strTableName = "DOWNLOAD";
            //        strListColumnName = "";
            //        strDateColumnName = "DLDATE";
            //        break;
            //    case "CA":
            //        strTableName = "CALIBRATION";
            //        strDateColumnName = "CALDATE";
            //        break;
            //    case "PT":
            //        strTableName = "PRETEST";
            //        strDateColumnName = "PTDATE";
            //        break;
            //    case "B1":
            //    case "B2":
            //    case "B3":
            //    case "B4":
            //        strTableName = "BASEBANDTEST";
            //        strDateColumnName = "BBDATE";
            //        break;
            //    case "BT":
            //        strTableName = "BLUETOOTH";
            //        strDateColumnName = "PDDATE";
            //        break;
            //    case "BTWL":
            //        strTableName = "BTWIRELESS";
            //        strDateColumnName = "PDDATE";
            //        break;
            //    case "WL":
            //        strTableName = "WIRELESS";
            //        strDateColumnName = "WLDATE";
            //        break;
            //    case "D2":
            //        strTableName = "REDOWNLOAD";
            //        strDateColumnName = "DLDATE";
            //        break;
            //    case "E2":
            //        strTableName = "E2PCONFIG";
            //        strDateColumnName = "E2PDATE";
            //        break;
            //    case "A1":
            //    case "A2":
            //    case "A3":
            //    case "A4":
            //    case "A5":
            //        strTableName = "BASEBANDTEST";
            //        strDateColumnName = "BBDATE";
            //        break;
            //    case "FQC":
            //        strTableName = "MES_ASSY_HISTORY";
            //        strDateColumnName = "CREATION_DATE";
            //        break;
            //    default:
            //        strTableName = "PRODUCT_HISTORY_V";
            //        strDateColumnName = "PDDATE";
            //        break;
            //}

            string strSql = "SELECT EMPLOYEE,LINE_CODE LineID,FIXTUREID,ERROR_MSG FAILUREITEM,COUNT(*) QTY ";
            strSql = strSql + " FROM " + strModel  + ".PRODUCT_HISTORY_V  ";
            strSql = strSql + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') ";
            strSql = strSql + " AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') ";
            strSql = strSql + " AND STATUS = 'F' ";
            strSql = strSql + " AND UPPER(EMPLOYEE) <> 'REPAIR'";


            if (ckbRepair.Checked == true)
            {
                strSql = strSql + " AND REPAIR = 0 ";
            }
            if (!strStationID.Equals(""))
            {
                strSql = strSql + " AND STATION_CODE = " + ClsCommon.GetSqlString(strStationID);
            }
            strSql = strSql + " GROUP BY EMPLOYEE,LINE_CODE,FIXTUREID,ERROR_MSG" ;

            return strSql;
        }

		private void dgMachineRate_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgMachineRate.CurrentPageIndex = e.NewPageIndex ;
			GetData();
		}

        protected void btnOutPut_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetSqlString()).Tables[0];
            ExortToExcel(dt);
        }

        private void ExortToExcel(DataTable dt)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //新建一Excel應用程式
            Missing miss = Missing.Value;

            Excel.ApplicationClass objExcel = null;
            Excel.Workbooks objBooks = null;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null;

            try
            {
                objExcel = new Excel.ApplicationClass();
                objExcel.Visible = false;
                objBooks = (Excel.Workbooks)objExcel.Workbooks;
                objBook = (Excel.Workbook)(objBooks.Add(miss));
                objSheet = (Excel.Worksheet)objBook.ActiveSheet;
                objSheet.Name = ddlModel.SelectedValue + "_" + ddlStation.SelectedValue;

                int intColumn = dt.Columns.Count;
                for (int i = 1; i <= intColumn; i++)
                {
                    //SetRangeValue(objSheet,GetCellName(i,1),GetCellName(i,1),dt.Columns[i].ColumnName,true,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                    objSheet.Cells[1, i] = dt.Columns[i-1].ColumnName;
                }

                int RowID = 2;

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i <= intColumn ; i++)
                    {
                        //SetRangeValue(objSheet,GetCellName(i,RowID),GetCellName(i,RowID),row[i].ToString(),false,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                        objSheet.Cells[RowID, i] = row[i-1].ToString();
                    }

                    RowID++;
                }
                //

                objSheet.Columns.AutoFit();
                objSheet.Rows.AutoFit();

                //關閉Excel
                objBook.SaveAs(ExportPath + strFileName, miss, miss, miss, miss, miss, Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
                objBook.Close(false, miss, miss);
                objBooks.Close();
                objExcel.Quit();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
            }
            catch
            {
                throw;
            }
            finally
            {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
                if (!objSheet.Equals(null))
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
                if (objBook != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
                if (objBooks != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
                if (objExcel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
                GC.Collect();
            }

            //保存或打開報表
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
            Response.Charset = "";
            this.EnableViewState = false;
            Response.WriteFile(ExportPath + strFileName);
            Response.End();
        }
}
}