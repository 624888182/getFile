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
    using System.Configuration;
    using Excel = Microsoft.Office.Interop.Excel;
    using DB.EAI;
    using System.Data.OracleClient;

    

    //using IMS.ServerControls;

    public partial class wfrmDownLoadShippingData : System.Web.UI.UserControl
    { 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiLanguage(); 
            }
        }

        private void MultiLanguage()
        {
            lblCartonNo.Text = (String)GetGlobalResourceObject("SFCQuery", "CartonNo"); 
            btnDownLoad.Text = (String)GetGlobalResourceObject("SFCQuery", "DownLoad");
        }

        private string Split(string strPoNo)
        {
            string strInput = strPoNo;
            string strOutput = "";
            try
            {
                string[] strInput1 = strInput.Split(new Char[] { ',' });
                for (int i = 0; i < strInput1.Length; i++)
                    strOutput = strOutput + "'" + strInput1[i].Trim().ToString() + "',";
                strOutput = strOutput.Substring(0, strOutput.Length - 1);

                return strOutput;
            }
            catch
            {
                return "split error";
            }
        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (tbCartonNo.Text.Trim().Equals(""))
            {
                ClsCommon.ShowMessage(this.Page, (String)GetGlobalResourceObject("SFCQuery", "NoInput"));
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "NoInput", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoInput") + "');</script>");
                return;
            } 
            string strCartonNo = tbCartonNo.Text.Trim().ToUpper();
            string strsql = "SELECT s.imei IMEI,S.SERIAL_NO,s.model,T.PPART,s.productid,s.cust_pno,s.carton_no,S.DDATE FROM SHP.CMCS_SFC_SHIPPING_DATA s,shp.cmcs_sfc_porder t "
            +" Where S.WORK_ORDER=T.PORDER ";

            if (!strCartonNo.Equals(""))
            {
                strCartonNo = Split(strCartonNo);
                if (strCartonNo.Equals("split error"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,請檢查!');</script>");
                    return; 
                }
                else
                    strsql += " and carton_no in (" + strCartonNo + ") order by carton_no,imei";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('請輸入箱號!');</script>");
                return; 
            }
   
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0]; 

            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoData") + "');</script>");
                return;
            }
            else
                ExortToExcel(dt);          

        }

        private void ExortToExcel1(string strSql)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            //string strFileName;
            //strFileName = tbCartonNo.Text.Trim().ToUpper(); 
            string strFileName =   DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //新建一Excel應用程式
            Missing missing = Missing.Value;
            Excel.ApplicationClass objExcel = null;
            Excel.Workbooks objBooks = null;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null;
            try
            {
                objExcel = new Excel.ApplicationClass();
                objExcel.Visible = false;
                objBooks = (Excel.Workbooks)objExcel.Workbooks;
                objBook = (Excel.Workbook)(objBooks.Add(missing));
                objSheet = (Excel.Worksheet)objBook.ActiveSheet;

                clsDBToExcel.ExportToExcel(objSheet, strSql);

                //關閉Excel
                objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                objBook.Close(false, missing, missing);
                objBooks.Close();
                objExcel.Quit();
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
            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
            Response.Charset = "";
            this.EnableViewState = false;
            Response.WriteFile(ExportPath + strFileName);
            Response.End();
        }

        private void ExortToExcel(DataTable dt)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            //string ExportPath = Request.PhysicalApplicationPath;
            //string strFileName;
            //strFileName = dt.Rows[0]["CARTON_NO"].ToString() + ".xls";

            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";

            //string strFileName = DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //新建一Excel應用程式
            Missing miss = Missing.Value;

            Excel.ApplicationClass objExcel = null;
            Excel.Workbooks objBooks = null;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null;
            Excel.Range range; 

            try
            {
                objExcel = new Excel.ApplicationClass();
                objExcel.Visible = false;
                objBooks = (Excel.Workbooks)objExcel.Workbooks;
                objBook = (Excel.Workbook)(objBooks.Add(miss));
                objSheet = (Excel.Worksheet)objBook.ActiveSheet; 

                int intColumn = dt.Columns.Count;
                for (int i = 1; i <= intColumn; i++)
                {
                    //SetRangeValue(objSheet,GetCellName(i,1),GetCellName(i,1),dt.Columns[i].ColumnName,true,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                    objSheet.Cells[1, i] = dt.Columns[i-1].ColumnName;
                }

                int RowID = 2;

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i <= intColumn; i++)
                    {
                        string type = objSheet.Cells[RowID, i].GetType().ToString();
                        range = (Excel.Range)objSheet.Cells[RowID, i];
                        switch (type)
                        {
                            case "System.String":
                                range.NumberFormatLocal = "@";
                                break;
                            //case "System.DateTime":
                                //range.NumberFormatLocal = "yyyy-mm-dd";
                                //range.ColumnWidth = 10;
                                //break;
                        } 

                        //SetRangeValue(objSheet,GetCellName(i,RowID),GetCellName(i,RowID),row[i].ToString(),false,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                        objSheet.Cells[RowID, i] = row[i-1].ToString();
                        
                    }

                    RowID++;
                }


                //
                objSheet.Columns.AutoFit();
                objSheet.Rows.AutoFit();

                //頁面設置
                try
                {
                    objSheet.PageSetup.LeftMargin = 20;
                    objSheet.PageSetup.RightMargin = 20;
                    objSheet.PageSetup.TopMargin = 35;
                    objSheet.PageSetup.BottomMargin = 15;
                    objSheet.PageSetup.HeaderMargin = 7;
                    objSheet.PageSetup.FooterMargin = 10;
                    objSheet.PageSetup.CenterHorizontally = true;
                    objSheet.PageSetup.CenterVertically = false;
                    objSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait;
                    objSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                    objSheet.PageSetup.Zoom = false;
                    objSheet.PageSetup.FitToPagesWide = 1;
                    objSheet.PageSetup.FitToPagesTall = false;
                }
                catch
                {
                    throw;
                }
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
            //Response.ContentType = "application/vnd.ms-excel";
            Response.ContentType = "application/vnd.ms-excel.numberformat:@";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
            //Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
            Response.Charset = "";
            this.EnableViewState = false;
            Response.WriteFile(ExportPath + strFileName); 
            Response.End();            
        }
    } 
}
