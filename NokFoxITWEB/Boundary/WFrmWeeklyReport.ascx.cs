namespace SFCQuery.Boundary
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using DBAccess.EAI;
	using System.Resources;
	using System.Globalization;
	using System.Reflection;
	using SFCQuery.Boundary;
	using Excel = Microsoft.Office.Interop.Excel;
	using DB.EAI;
    using System.Data.OracleClient;

	/// <summary>
	///		WFrmWeeklyReport 的摘要描述。
	/// </summary>
	public partial class WFrmWeeklyReport : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
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

		}
		#endregion

		private void InitialDDL()
		{
			//機種
            BindModel();
			//周
			DateTime dttm = DateTime.Now.AddDays(-70);  //前十周的日期
			for(int i=1;i<12;i++)
			{
				int WeekOfYear = Convert.ToInt16(Math.Ceiling(dttm.DayOfYear/7.0)); //一年中的第幾周
				string strTemp = dttm.Year.ToString()+"年第"+WeekOfYear.ToString()+"周";
				//ddlMonth.Items.Add(strTemp);
				ddlMonth.Items.Add(new ListItem(strTemp,WeekOfYear.ToString()));
				dttm = dttm.AddDays(7);  //日期加七天（即一周）
			}
			ddlMonth.SelectedIndex = 10;
		}
        private void BindModel()
        {
            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlModel.DataTextField = "MODEL";
            ddlModel.DataValueField = "MODEL";
            ddlModel.DataSource = dt.DefaultView;
            ddlModel.DataBind();
        }

		private void MultiLanaguage()
		{			
			lblWeek.Text = (String)GetGlobalResourceObject("SFCQuery","Week");
			lblModel.Text = (String)GetGlobalResourceObject("SFCQuery","Model");
			btnReport.Text = (String)GetGlobalResourceObject("SFCQuery","Report");
		}

		private void GetWeeklyInputData(int[,] intStationQty,string strModel,DateTime dttm)
		{
			//WFrmDailyReport objDailyReport = new WFrmDailyReport();		
			clsDBYieldData objYieldData = new clsDBYieldData();
			for(int i=0;i<7;i++)
			{				
				//DataTable dt = objDailyReport.ShowYield(strModel,dttm.Month.ToString(),dttm.Day);
				DataTable dt = objYieldData.ShowYield(strModel,dttm,dttm.AddDays(1),"",true,2,""); //2表示所有工單 lwb modify in 01/14

                //DataTable dt = objYieldData.ShowYield(strModel, dttm, dttm.AddDays(1), "", true); //2表示所有工單

				dttm = dttm.AddDays(1);
				if (dt.Rows.Count>0)
				{
					foreach(DataRow rw in dt.Rows)
					{
						switch(rw["Station_ID"].ToString().ToUpper())
						{
							case "DL":
								intStationQty[0,1] = intStationQty[0,1] + int.Parse(rw["Input"].ToString());
								intStationQty[1,1] = intStationQty[1,1] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "PT":
								intStationQty[0,2] = intStationQty[0,2] + int.Parse(rw["Input"].ToString());
								intStationQty[1,2] = intStationQty[1,2] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "B1":
								intStationQty[0,3] = intStationQty[0,3] + int.Parse(rw["Input"].ToString());
								intStationQty[1,3] = intStationQty[1,3] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "B2":
								intStationQty[0,4] = intStationQty[0,4] + int.Parse(rw["Input"].ToString());
								intStationQty[1,4] = intStationQty[1,4] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "A1":
								intStationQty[0,5] = intStationQty[0,5] + int.Parse(rw["Input"].ToString());
								intStationQty[1,5] = intStationQty[1,5] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "A2":
								intStationQty[0,6] = intStationQty[0,6] + int.Parse(rw["Input"].ToString());
								intStationQty[1,6] = intStationQty[1,6] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "A3":
								intStationQty[0,7] = intStationQty[0,7] + int.Parse(rw["Input"].ToString());
								intStationQty[1,7] = intStationQty[1,7] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "WL":
								intStationQty[0,8] = intStationQty[0,8] + int.Parse(rw["Input"].ToString());
								intStationQty[1,8] = intStationQty[1,8] + int.Parse(rw["FinalFail"].ToString());
								break;
							case "FQC":
								intStationQty[0,9] = intStationQty[0,9] + int.Parse(rw["Input"].ToString());
								intStationQty[1,9] = intStationQty[1,9] + int.Parse(rw["FinalFail"].ToString());
								break;
						}
					}
				}
			}
			//return intStationQty;
		}

		private DataTable GetWeeklyTopIssue(string strModel,DateTime dttm)
		{
			string strSql = "SELECT * FROM (SELECT DEFECT_CODE, DEFECT_DESC_CHT, QTY, STATION_CODE FROM (SELECT ERROR_CODE DEFECT_CODE,ERROR_MSG DEFECT_DESC_CHT,STATION_CODE,COUNT(*) QTY "
				+" FROM (SELECT /*+RULE*/ DISTINCT PRODUCT_ID,STATION_CODE,ERROR_CODE,ERROR_MSG FROM "+strModel+".PRODUCT_HISTORY_V A "
				+" WHERE PDDATE BETWEEN TO_DATE('"+dttm.ToString("yyy/MM/dd")+" 08:20','yyyy/mm/dd hh24:mi') AND TO_DATE('"+dttm.AddDays(7).ToString("yyyy/MM/dd")+" 08:20','yyyy/mm/dd hh24:mi') "
				+" AND STATUS = 'F' AND UPPER(PRODUCT_ID) <> 'SCY_INVALID_SN' AND PDDATE =(SELECT MAX(PDDATE) "
				+" FROM "+strModel+".PRODUCT_HISTORY_V WHERE PRODUCT_ID = A.PRODUCT_ID AND STATION_CODE = A.STATION_CODE)) "
				+" GROUP BY ERROR_CODE, ERROR_MSG, STATION_CODE ORDER BY COUNT(*) DESC, DEFECT_DESC_CHT)) WHERE ROWNUM <= 10";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			return dt;
		}

		protected void btnReport_Click(object sender, System.EventArgs e)
		{
			string strWeek = ddlMonth.SelectedValue;
			int ss = DateTime.Now.DayOfYear-(7*(int.Parse(strWeek)-1));
			DateTime dttm = Convert.ToDateTime(DateTime.Now.AddDays(-ss).ToString("yyyy/MM/dd")+ " 08:20");
			string strModel = ddlModel.SelectedValue;
//			int []intStationInput = new int[10];
//			int []intStationNG = new int[10];
			int [,]intStationQty = new int[2,10];
			GetWeeklyInputData(intStationQty,strModel,dttm);
			DataTable dt = GetWeeklyTopIssue(strModel,dttm);
			WriteToExcel(intStationQty,dt,strWeek);
			
		}

		private void WriteToExcel(int [,]intStationQty,DataTable dt,string strWeek)
		{
			string strFileName = Request.PhysicalApplicationPath + @"Templet\Weekly Report.xlt";
			string ExportPath = Request.PhysicalApplicationPath + @"Temp\";
			

			Missing MissingValue = Missing.Value;
			Excel.ApplicationClass objExcel = null;
			Excel.Workbook objBook = null;
			Excel.Worksheet objSheet = null;
			try
			{

				objExcel=new Excel.ApplicationClass(); 
                objExcel.Application.Workbooks.Add ( true );//
				objExcel.Visible = true;

				objBook = objExcel.Workbooks.Open(strFileName.ToString(),MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue); 

				//YR Trend
				objSheet = (Excel.Worksheet)objBook.Worksheets[1]; 

				objSheet.Cells[2,2] = "WK"+strWeek;
				//X-Ray
				//			objSheet.Cells[3,2] = 0;
				//			objSheet.Cells[4,2] = 0;
				//DL
				objSheet.Cells[6,2] = intStationQty[0,1];
				objSheet.Cells[7,2] = intStationQty[1,1];
				//Calibration- Pretest
				objSheet.Cells[10,2] = intStationQty[0,2];
				objSheet.Cells[11,2] = intStationQty[1,2];
				//TB1
				objSheet.Cells[13,2] = intStationQty[0,3];
				objSheet.Cells[14,2] = intStationQty[1,3];
				//TB2
				objSheet.Cells[17,2] = intStationQty[0,4];
				objSheet.Cells[18,2] = intStationQty[1,4];
				//AB1
				objSheet.Cells[21,2] = intStationQty[0,5];
				objSheet.Cells[22,2] = intStationQty[1,5];
				//AB2
				objSheet.Cells[24,2] = intStationQty[0,6];
				objSheet.Cells[25,2] = intStationQty[1,6];
				//AB3
				objSheet.Cells[27,2] = intStationQty[0,7];
				objSheet.Cells[28,2] = intStationQty[1,7];
				//WL
				objSheet.Cells[30,2] = intStationQty[0,8];
				objSheet.Cells[31,2] = intStationQty[1,8];
				//FQC
				objSheet.Cells[33,2] = intStationQty[0,9];
				objSheet.Cells[34,2] = intStationQty[1,9];
				//Packing
				//			objSheet.Cells[37,2] = intStationQty[0,1];
				//			objSheet.Cells[38,2] = intStationQty[1,1];


				//Top Issue
				objSheet = (Excel.Worksheet)objBook.Worksheets[2];
				objSheet.Activate();  //lwb modify in 01/14
 
				int intRow = 2 ;
				foreach(DataRow rw in dt.Rows)
				{
					objSheet.Cells[intRow,1] = rw["DEFECT_DESC_CHT"].ToString();
					objSheet.Cells[intRow,2] = rw["Qty"].ToString();
					objSheet.Cells[intRow,6] = rw["STATION_CODE"].ToString();
					intRow++;
					if (intRow.Equals(12))
						break;
				}

				strFileName = Session.SessionID+DateTime.Now.ToString("yyyyMMdd hhmmss")+".xls";
				objBook.SaveAs(ExportPath+strFileName, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue,Excel.XlSaveAsAccessMode.xlExclusive, MissingValue, MissingValue, MissingValue, MissingValue,MissingValue);
				objBook.Close(false,MissingValue,MissingValue);
				objExcel.Quit();
			}
			finally
			{
				if (!objSheet.Equals(null))
					System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
				if (objBook!=null)
					System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
				if (objExcel!=null)
					System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
				GC.Collect();
			}
			//Label1.Text = Label1.Text + " 結束時間："+DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

			Response.Clear();
			Response.Buffer= true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.ContentEncoding = System.Text.Encoding.UTF8;			
			Response.AppendHeader("content-disposition","attachment;filename="+strFileName);
			Response.Charset = "";
			this.EnableViewState = false;
			Response.WriteFile(ExportPath+strFileName);
			Response.End();	

		}
	}
}
