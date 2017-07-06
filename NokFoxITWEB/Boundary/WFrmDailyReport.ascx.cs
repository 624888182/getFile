/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/07/08
 *  Modifier : Shu Jian Bo             Date: 2007/07/08
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
	using System.Resources;
	//using System.Globalization;
	using System.Reflection;
	using Excel = Microsoft.Office.Interop.Excel;
	using DB.EAI;
    using System.Data.OracleClient;

	/// <summary>
	///		WFrmDailyReport 的摘要描述。
	/// </summary>
	public partial class WFrmDailyReport : System.Web.UI.UserControl
	{
		private int CurrentRow = 6;
		//private int PreWeekRow = 6;
		//private string strTotal = "=0";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			//ddlMonth.SelectedIndex = DateTime.Now.Month-1;
			if (!IsPostBack)
			{
                //btnDateFrom.Attributes["onclick"] = "return showCalendar('"+tbStartDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
                //btnDateTo.Attributes["onclick"] = "return showCalendar('"+tbEndDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
				tbStartDate.DateTextBox.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")+" 08:20";
				tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd")+" 08:20";
                BindModel();
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
			//lblMonth.Text = (String)GetGlobalResourceObject("SFCQuery","Month");
			lblModel.Text = (String)GetGlobalResourceObject("SFCQuery","Model");
			btnReport.Text = (String)GetGlobalResourceObject("SFCQuery","Report");
			Label28.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			Label29.Text = (String)GetGlobalResourceObject("SFCQuery","ErrorDateTime");
			lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateFrom");
			lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery","DateTo");
		}

		private void WriteToExcel(Excel.Worksheet objSheet,DataTable dt,DateTime dttm,int intDay)
		{
			//string strDate = DateTime.Now.Year.ToString()+"/"+ strMonth +"/"+intDay.ToString();
			objSheet.Cells[CurrentRow+intDay,1] = dttm.ToString("yyyy/MM/dd");
			foreach(DataRow rw in dt.Rows)
			{				
				switch(rw["Station_ID"].ToString().ToUpper())
				{
					case "DL":
						objSheet.Cells[CurrentRow+intDay,5] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,6] = rw["FinalPass"].ToString();
						break;
					case "PT":
						objSheet.Cells[CurrentRow+intDay,8] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,9] = rw["FinalPass"].ToString();
						break;
					case "B1":
						objSheet.Cells[CurrentRow+intDay,11] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,12] = rw["FinalPass"].ToString();
						break;
					case "B2":
						objSheet.Cells[CurrentRow+intDay,14] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,15] = rw["FinalPass"].ToString();
						break;
					case "A1":
						objSheet.Cells[CurrentRow+intDay,17] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,18] = rw["FinalPass"].ToString();
						break;
					case "A2":
						objSheet.Cells[CurrentRow+intDay,20] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,21] = rw["FinalPass"].ToString();
						break;
					case "A3":
//						objSheet.Cells[CurrentRow+intDay,5] = rw["Input"].ToString();
//						objSheet.Cells[CurrentRow+intDay,6] = rw["FinalPass"].ToString();
						break;
					case "WL":
						objSheet.Cells[CurrentRow+intDay,23] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,24] = rw["FinalPass"].ToString();
						break;
					case "FQC":
						objSheet.Cells[CurrentRow+intDay,26] = rw["Input"].ToString();
						objSheet.Cells[CurrentRow+intDay,27] = rw["FinalPass"].ToString();
						break;
				}
			}

			//objSheet.Cells[8,1] = "Test Test";
			/*DateTime dttm = new DateTime(int.Parse(strDate.Substring(0,4)),int.Parse(strDate.Substring(5,2)),int.Parse(strDate.Substring(8)));
			int DayOfWeek = Convert.ToInt16(dttm.DayOfWeek);  //一個星期中的第幾天
			int WeekOfYear = Convert.ToInt16(Math.Ceiling(dttm.DayOfYear/7.0)); //一年中的第幾周
			int DayPerMonth = DateTime.DaysInMonth(DateTime.Now.Year,int.Parse(strMonth));
			if ((DayOfWeek.Equals(5)) || (!DayOfWeek.Equals(5) && DayPerMonth.Equals(intDay))) //星期五 或者是一個月的最後一天但不是星期五
			{
				CurrentRow ++;
				objSheet.Cells[CurrentRow+intDay,1] = "W"+WeekOfYear.ToString();
				objSheet.Cells[CurrentRow+intDay,2] = "=SUM(B"+Convert.ToString(PreWeekRow+1)+":B"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,3] = "=SUM(C"+Convert.ToString(PreWeekRow+1)+":C"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,5] = "=SUM(E"+Convert.ToString(PreWeekRow+1)+":E"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,6] = "=SUM(F"+Convert.ToString(PreWeekRow+1)+":F"+Convert.ToString(CurrentRow+intDay-1)+")";;
				objSheet.Cells[CurrentRow+intDay,8] = "=SUM(H"+Convert.ToString(PreWeekRow+1)+":H"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,9] = "=SUM(I"+Convert.ToString(PreWeekRow+1)+":I"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,11] = "=SUM(K"+Convert.ToString(PreWeekRow+1)+":K"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,12] = "=SUM(L"+Convert.ToString(PreWeekRow+1)+":L"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,14] = "=SUM(N"+Convert.ToString(PreWeekRow+1)+":N"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,15] = "=SUM(O"+Convert.ToString(PreWeekRow+1)+":O"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,17] = "=SUM(Q"+Convert.ToString(PreWeekRow+1)+":Q"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,18] = "=SUM(R"+Convert.ToString(PreWeekRow+1)+":R"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,20] = "=SUM(T"+Convert.ToString(PreWeekRow+1)+":T"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,21] = "=SUM(U"+Convert.ToString(PreWeekRow+1)+":U"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,23] = "=SUM(W"+Convert.ToString(PreWeekRow+1)+":W"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,24] = "=SUM(X"+Convert.ToString(PreWeekRow+1)+":X"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,26] = "=SUM(Z"+Convert.ToString(PreWeekRow+1)+":Z"+Convert.ToString(CurrentRow+intDay-1)+")";
				objSheet.Cells[CurrentRow+intDay,27] = "=SUM(AA"+Convert.ToString(PreWeekRow+1)+":AA"+Convert.ToString(CurrentRow+intDay-1)+")";
				//Excel.Range objRange = objSheet.get_Range("A"+Convert.ToString(CurrentRow+intDay),"AQ"+Convert.ToString(CurrentRow+intDay.ToString()));
				string cell1 = "A"+Convert.ToString(CurrentRow+intDay);
				string cell2 = "AQ"+Convert.ToString(CurrentRow+intDay);
				Excel.Range objRange = objSheet.get_Range(cell1,cell2);
				objRange.Select();
				objRange.Interior.Color= Color.YellowGreen.ToArgb();
				objRange.Borders.LineStyle = 1;
				PreWeekRow = CurrentRow+intDay+1;
				strTotal = strTotal + "+B"+Convert.ToString(CurrentRow+intDay);

				System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
				objRange = null;
			}

			if (DayPerMonth.Equals(intDay))
			{
				CurrentRow ++;
				objSheet.Cells[CurrentRow+intDay,1] = "Total";
				objSheet.Cells[CurrentRow+intDay,2] = strTotal;
				objSheet.Cells[CurrentRow+intDay,3] = strTotal.Replace("B","C");
				objSheet.Cells[CurrentRow+intDay,5] = strTotal.Replace("B","E");
				objSheet.Cells[CurrentRow+intDay,6] = strTotal.Replace("B","F");
				objSheet.Cells[CurrentRow+intDay,8] = strTotal.Replace("B","H");
				objSheet.Cells[CurrentRow+intDay,9] = strTotal.Replace("B","I");
				objSheet.Cells[CurrentRow+intDay,11] = strTotal.Replace("B","K");
				objSheet.Cells[CurrentRow+intDay,12] = strTotal.Replace("B","L");
				objSheet.Cells[CurrentRow+intDay,14] = strTotal.Replace("B","N");
				objSheet.Cells[CurrentRow+intDay,15] = strTotal.Replace("B","O");
				objSheet.Cells[CurrentRow+intDay,17] = strTotal.Replace("B","Q");
				objSheet.Cells[CurrentRow+intDay,18] = strTotal.Replace("B","R");
				objSheet.Cells[CurrentRow+intDay,20] = strTotal.Replace("B","T");
				objSheet.Cells[CurrentRow+intDay,21] = strTotal.Replace("B","U");
				objSheet.Cells[CurrentRow+intDay,23] = strTotal.Replace("B","W");
				objSheet.Cells[CurrentRow+intDay,24] = strTotal.Replace("B","X");
				objSheet.Cells[CurrentRow+intDay,26] = strTotal.Replace("B","Z");
				objSheet.Cells[CurrentRow+intDay,27] = strTotal.Replace("B","AA");
				//Excel.Range objRange = objSheet.get_Range("A"+Convert.ToString(CurrentRow+intDay),"AQ"+Convert.ToString(CurrentRow+intDay.ToString()));
				string cell1 = "A"+Convert.ToString(CurrentRow+intDay);
				string cell2 = "AQ"+Convert.ToString(CurrentRow+intDay);
				Excel.Range objRange = objSheet.get_Range(cell1,cell2);
				objRange.Select();
				objRange.Interior.Color= Color.Gray.ToArgb();
				objRange.Borders.LineStyle = 1;
				PreWeekRow = CurrentRow+intDay+1;
				strTotal = strTotal + "+A"+Convert.ToString(CurrentRow+intDay);

				System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
				objRange = null;
			}*/
			
		}

		protected void btnReport_Click(object sender, System.EventArgs e)
		{
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

			GetReportData();			
		}

		private void GetReportData()
		{
			//string test = DateTime.Now.Year.ToString()+"/"+ddlMonth.SelectedValue+"/"+DateTime.Now.Day.ToString();
			string strFileName = Request.PhysicalApplicationPath + @"Templet\Daily Report.xlt";
			string ExportPath = Request.PhysicalApplicationPath + @"Temp\";
			bool blRepair = ckbRepair.Checked;
			

			Missing MissingValue = Missing.Value;

			Excel.ApplicationClass objExcel = null;
			Excel.Workbook objBook = null;
			Excel.Worksheet objSheet = null;
			try
			{
				objExcel=new Excel.ApplicationClass(); 
				objExcel.Visible = true;

				objBook = objExcel.Workbooks.Open(strFileName.ToString(),MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue,MissingValue,MissingValue, 

					MissingValue); 
				objSheet = (Excel.Worksheet)objBook.Worksheets[1]; 

				//string strMonth = ddlMonth.SelectedValue;
				string strMonth = tbStartDate.DateTextBox.Text.Substring(5,2);
				string strModel = ddlModel.SelectedValue.ToUpper();
				//int intDay = DateTime.DaysInMonth(DateTime.Now.Year,int.Parse(strMonth));
				int intDay = 1;
				int intCurrentDay = int.Parse(tbStartDate.DateTextBox.Text.Substring(8,2));
				DateTime dttmStart = Convert.ToDateTime(tbStartDate.DateTextBox.Text);
				DateTime dttmEnd = Convert.ToDateTime(tbEndDate.DateTextBox.Text);

				DataTable dt = null;

				//Label1.Text = "開始時間："+DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
				//CurrentRow = 6;
				//PreWeekRow = 6;
				clsDBYieldData objYieldData = new clsDBYieldData();
				for(int i=1;i<=intDay;i++)
				{
                    dt = objYieldData.ShowYield(strModel, dttmStart, dttmEnd, "", blRepair, 2,""); //2表示所有工單
					WriteToExcel(objSheet,dt,dttmStart,i);
				
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

//		private DataTable CreateRateTable()
//		{
//			DataTable dt = new DataTable();
//			dt.Columns.Add("Station_ID",typeof(String));
//			dt.Columns.Add("Input",typeof(String));
//			dt.Columns.Add("FirstPass",typeof(String));			
//			dt.Columns.Add("FirstYield",typeof(String));
//			dt.Columns.Add("FinalPass",typeof(String));
//			dt.Columns.Add("FinalYield",typeof(String));
//			dt.Columns.Add("FirstFail",typeof(String));
//			dt.Columns.Add("SecondFail",typeof(String));
//			dt.Columns.Add("ThirdFail",typeof(String));
//			dt.Columns.Add("ForthFail",typeof(String));
//			dt.Columns.Add("FifthFail",typeof(String));
//			dt.Columns.Add("FinalFail",typeof(String));
//			dt.Columns.Add("RetestRate",typeof(String));
//			dt.Columns.Add("FinalFail1",typeof(String));
//			      
//
//			return dt;
//		}
//
//		/// <summary>
//		/// 日報表及周報表共用此方法
//		/// </summary>
//		/// <param name="strModel">機種</param>
//		/// <param name="dttmStart">開始時間</param>
//		/// <param name="dttmEnd">結束時間</param>
//		/// <returns></returns>
//		public DataTable ShowYield(string strModel,DateTime dttmStart,DateTime dttmEnd,bool blRepair) 
//		{
//			int RetestRow = 0;
//			int i = 0;
//			long FirstPass;			
//			long FinalPass;			
//			long FirstFail;			
//			long FinalFail;
//
//			long TotalFirstPass = 0;
//			long TotalFinalPass = 0;
//			long TotalFirstFail = 0;
//			long TotalFinalFail = 0;
//			long TotalInput  = 0;
//
//			long[] Fails = new long[4];
//			long[] FailCount = new long[6];
//			long[] TotalFailCount = new long[6];
//			bool TestPass=false;
//			int TestCount;
//			string strStation_code , strPID;
//			string strStartDate = dttmStart.ToString("yyyy/MM/dd HH:mm");
//			string strEndDate = dttmEnd.ToString("yyyy/MM/dd HH:mm");
//			//string strModel = strModel;//ddlModel.SelectedValue.ToUpper();
//			string strLine = "";//ddlLine.SelectedValue.ToUpper();
//			//bool blRepair = blRepair;// ckbRepair.Checked;
//			for(int j=0;j<TotalFailCount.Length;j++)
//				TotalFailCount[j] = 0;
//
//			DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetSQLString(strModel,dttmStart,dttmEnd,"",blRepair)).Tables[0];
//			DataTable dtRate = CreateRateTable();
//			DataRow dtRW = null;
//
//			while (i<dt.Rows.Count)
//			{
//				FirstPass = 0;
//				FirstFail = 0;
//				FinalPass = 0;
//				FinalFail = 0;
//
//				for (int j=0 ; j<FailCount.Length; j++)
//					FailCount[j] = 0;
//
//				for (int j=0 ; j<Fails.Length; j++)
//					Fails[j] = 0;
//				strStation_code = dt.Rows[i]["STATION_CODE"].ToString();
//				do
//				{
//					strPID = dt.Rows[i]["PRODUCT_ID"].ToString();
//					TestCount = 0;
//				while ((i<dt.Rows.Count) && (dt.Rows[i]["PRODUCT_ID"].ToString().CompareTo(strPID)==0) && (dt.Rows[i]["STATION_CODE"].ToString().CompareTo(strStation_code)==0))
//				{
//					TestCount ++;
//					if (dt.Rows[i]["STATUS"].ToString().CompareTo("P")==0)
//					{
//						if (TestCount==1)
//							FirstPass ++;
//						TestPass = true;
//					}
//					else
//					{
//						if (TestCount==1)
//							FirstFail ++;
//						TestPass = false;
//					}
//
//					if ((!TestPass)&&(TestCount<FailCount.Length)) 
//					{
//						FailCount[TestCount] ++;
//					}
//				        
//					if (i>dt.Rows.Count)
//						break;
//					else
//						i++;
//				}
//
//					if (TestPass) 
//					{
//						FinalPass = FinalPass + 1;
//					} 
//					else 
//					{
//						if (TestCount<Fails.Length)
//							Fails[TestCount]  ++;
//						FinalFail  ++;
//					}
//					if (i>dt.Rows.Count)
//						break;
//					//					else
//					//						i++;
//				}while(i<dt.Rows.Count && dt.Rows[i]["STATION_CODE"].ToString().CompareTo(strStation_code)==0);
//				
//				dtRW = dtRate.NewRow();
//				long longFinalFail = long.Parse(GetFinalFail(strStartDate,strEndDate,strModel,strLine,strStation_code,blRepair));
//				long longInput = FirstPass + FirstFail;
//				long longFinalPass = longInput - longFinalFail;
//				dtRW["Station_ID"] = strStation_code;
//				dtRW["Input"] = longInput;//Convert.ToString(FirstPass + FirstFail);
//				dtRW["FirstPass"] = FirstPass;
//				dtRW["FirstYield"] = Math.Round(Convert.ToDouble(FirstPass*100)/(FirstPass + FirstFail),2)+"%";// Math.Round(Convert.ToDouble(FirstPass*100/(FirstPass + FirstFail)),2);
//				dtRW["FinalPass"] = longFinalPass; //FinalPass;
//				dtRW["FinalYield"] = Math.Round(Convert.ToDouble(longFinalPass*100)/(FirstPass + FirstFail),2)+"%";
//				dtRW["FirstFail"] = FirstFail;
//				dtRW["SecondFail"] = FailCount[2];
//				dtRW["ThirdFail"] = FailCount[3];
//				dtRW["ForthFail"] = FailCount[4];
//				dtRW["FifthFail"] = FailCount[5];
//				dtRW["FinalFail"] = FinalFail;
//				dtRW["RetestRate"] = Math.Round(Convert.ToDouble((longFinalPass - FirstPass) * 100) / (FirstPass + FirstFail),2)+"%";
//				dtRW["FinalFail1"] = longFinalFail;
//				dtRate.Rows.Add(dtRW);
//
//				TotalInput = TotalInput + longInput;
//				TotalFirstPass = TotalFirstPass + FirstPass;
//				TotalFirstFail = TotalFirstFail + FirstFail;
//				TotalFinalPass = TotalFinalPass + longFinalPass;
//				TotalFinalFail = TotalFinalFail + longFinalFail;
//				for(int j=0;j<TotalFailCount.Length;j++)
//					TotalFailCount[j] = TotalFailCount[j] + FailCount[j];
//				RetestRow ++;
//
//
//				//i++;
//			}
//			//總計
//			dtRW = dtRate.NewRow();
//			dtRW["Station_ID"] = "Total";
//			dtRW["Input"] = TotalInput;
//			dtRW["FirstPass"] = TotalFirstPass;
//			dtRW["FirstYield"] = Math.Round(Convert.ToDouble(TotalFirstPass*100)/(TotalFirstPass + TotalFirstFail),2)+"%";
//			dtRW["FinalPass"] = TotalFinalPass; //FinalPass;
//			dtRW["FinalYield"] = Math.Round(Convert.ToDouble(TotalFinalPass*100)/(TotalFirstPass + TotalFirstFail),2)+"%";
//			dtRW["FirstFail"] = TotalFirstFail;
//			dtRW["SecondFail"] = TotalFailCount[2];
//			dtRW["ThirdFail"] = TotalFailCount[3];
//			dtRW["ForthFail"] = TotalFailCount[4];
//			dtRW["FifthFail"] = TotalFailCount[5];
//			dtRW["FinalFail"] = TotalFinalFail;
//			dtRW["RetestRate"] = Math.Round(Convert.ToDouble((TotalFinalPass - TotalFirstPass) * 100) / (TotalFirstPass + TotalFirstFail),2)+"%";
//			dtRW["FinalFail1"] = TotalFinalFail;
//			dtRate.Rows.Add(dtRW);
////			DataGrid1.DataSource = dtRate.DefaultView;
////			DataGrid1.DataBind();	
//			return dtRate;
//			
//		}
//
//		private string GetFQCString(string strModel,DateTime dttmStart,DateTime dttmEnd,string strLine)
//		{
//			string StrSql = "";
//			string StrWhere = "";
//		
//			StrWhere = "WHERE (CREATION_DATE BETWEEN TO_DATE('" + dttmStart.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') AND TO_DATE('" +  dttmEnd.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss'))";
//
//
//			//StrWhere = StrWhere + "AND PRODUCT_ID LIKE '" + strModel + "%' ";
//
//			if (strLine.CompareTo("")!=0) 
//			{
//				StrWhere = StrWhere + "AND STATION_ID = 'A" + strLine + "FO' ";
//			}
//			else
//			{
//				StrWhere = StrWhere + "AND STATION_ID LIKE 'A_FO' ";
//			}
//		
//			StrSql = "SELECT 'FQC',PRODUCT_ID,CREATION_DATE, STATE_ID FROM MES_ASSY_HISTORY " + StrWhere;
//		
//			return StrSql;
//		}
//	
//		private string GetSQLString(string strModel,DateTime dttmStart,DateTime dttmEnd,string strLine,bool blRepair) 
//		{
//			string StrSql = "";
//			string StrWhere = "";
//
//			StrSql = "SELECT STATION_CODE,PRODUCT_ID,PDDATE, STATUS FROM " + strModel + ".PRODUCT_HISTORY_V ";
//
//			StrWhere = "AND (PDDATE BETWEEN TO_DATE('" +  dttmStart.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') AND TO_DATE('" +  dttmEnd.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')) ";
//			if(blRepair)
//			{
//				StrWhere = StrWhere +" AND REPAIR = '0' ";
//			}
//		 
//			if (strLine.CompareTo("")!=0) 
//			{
//				StrWhere = StrWhere + "AND UPPER(LINE_CODE)='LINE" +Convert.ToSingle(strLine.ToCharArray()[0]- 64) + "' ";
//			}
//
//			//StrWhere = StrWhere + "AND PRODUCT_ID NOT LIKE '%NVALID_SN' ";
//			StrWhere = " WHERE " + StrWhere.Substring(4);
//		
//			StrSql = StrSql + StrWhere + "UNION " + GetFQCString(strModel,dttmStart,dttmEnd,strLine) + "ORDER BY 1,2,3";
//		  
//			return StrSql;
//		}
//
//		private string GetFinalFail(string strStartDate,string strEndDate,string strModel,string strLine,string strStationID,bool blRepair)
//		{
//			string strSql = " SELECT COUNT(*) FROM MES_PACK_HISTORY T WHERE CREATION_DATE BETWEEN "
//				+" TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
//				+" AND CREATION_DATE = (SELECT MAX(CREATION_DATE) FROM MES_PACK_HISTORY S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_ID = S.STATION_ID) "
//				+" AND STATE_ID = 'F' ";//AND PRODUCT_ID LIKE "+ClsCommon.GetSqlString(strModel+"%");
//			switch(strStationID)
//			{
//				case "FQC":
//					strSql = " SELECT COUNT(*) FROM MES_ASSY_HISTORY T WHERE CREATION_DATE BETWEEN "
//						+" TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
//						+" AND CREATION_DATE = (SELECT MAX(CREATION_DATE) FROM MES_ASSY_HISTORY S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_ID = S.STATION_ID) "
//						+" AND STATE_ID = 'F' AND STATION_ID LIKE 'A_FO' " ;//+ " AND PRODUCT_ID LIKE  "+ClsCommon.GetSqlString(strModel+"%") ;
//					break;
//				case "OQC":
//					strSql = strSql + " AND STATION_ID LIKE 'P_GO' ";
//					break;
//				case "OOB":
//					strSql = strSql + " AND STATION_ID LIKE 'P_EO' ";
//					break;
//				default:
//					strSql = "SELECT COUNT(*) FROM "+strModel+".PRODUCT_HISTORY_V T "
//						+" WHERE PDDATE BETWEEN TO_DATE('"+strStartDate+"', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"+strEndDate+"', 'YYYY/MM/DD HH24:MI') "
//						+" AND PDDATE = (SELECT MAX(PDDATE) FROM "+strModel+".PRODUCT_HISTORY_V S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_CODE = S.STATION_CODE) AND STATUS = 'F' ";
//					//+" AND PRODUCT_ID NOT LIKE '%NVALID_SN' AND PRODUCT_ID LIKE  "+ClsCommon.GetSqlString(strModel+"%");
//					if(!strLine.Equals("")) strSql = strSql + " AND UPPER(LINE_CODE) = "+ClsCommon.GetSqlString(strLine) ;
//					if(blRepair)  strSql = strSql + " AND REPAIR = 0 ";
//					if(!strStationID.Equals(""))  strSql = strSql + " AND STATION_CODE = "+ ClsCommon.GetSqlString(strStationID);
//					break;
//			}
//			return ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0].Rows[0][0].ToString(); //返回最終不良數
//		}
	}
}
