using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using Excel6 = Microsoft.Office.Interop.Excel;
using DB.EAI;

public partial class Boundary_wfrmMtracInfoQuery : System.Web.UI.UserControl
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
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel"); 
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = getdata();
        if (strsql.Equals("no data"))
        {
            btnExportExcel.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請至少輸入一個查詢條件...');</script>");
            return;
        }
        else
        {
            btnExportExcel.Visible = true;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dgMtrak.DataSource = dt.DefaultView;
                dgMtrak.DataBind();
                Label1.Text = "Current Page:" + (dgMtrak.CurrentPageIndex + 1).ToString() + "/" + dgMtrak.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            }
            else
            {
                btnExportExcel.Visible = false;
                dgMtrak.Visible = false;
                Label1.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('尚無M_Track資料...');</script>");
                return;
            }
        }
        
    }

    private string getdata()
    {
        string strInvoiceno = txtInvoiceNO.Text.Trim();
        string strCartonno = txtCartonID.Text.Trim().ToUpper();
        string strPid = txtpid.Text.Trim().ToUpper();

        string strsql = "select a.productid serial_number,a.model ship_model,a.imei,b.market_name model, "
            + "b.build_phase,a.carton_no carton_id,b.last_update_date creation_date,b.core_id,b.tanapa,b.ship_type, "
            + "b.company,b.contact_name first_name,b.phone_number,b.email_address "
            + "from shp.cmcs_sfc_shipping_data a,sap.cmcs_sfc_packing_lines_all b "
            + "where a.carton_no=b.internal_carton ";
        if (strInvoiceno.Length > 0) 
            strsql = strsql + " and b.invoice_number='" + strInvoiceno + "'"; 
        if(strCartonno.Length>0)
            strsql = strsql + " and a.carton_no='" + strCartonno + "'";
        if (strPid.Length > 0)
            strsql = strsql + " and a.productid='" + strPid + "'";
        if (strInvoiceno.Length == 0 && strCartonno.Length == 0 && strPid.Length == 0) 
            strsql="no data";
        return strsql;
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string strsql = getdata();
        ExortToExcel1(strsql);
    }
    private void ExortToExcel(DataTable dt)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing miss = Missing.Value;

        Microsoft.Office.Interop.Excel.ApplicationClass objExcel = null;
        Microsoft.Office.Interop.Excel.Workbooks objBooks = null;
        Excel6.Workbook objBook = null;
        Microsoft.Office.Interop.Excel.Worksheet objSheet = null;

        try
        {
            objExcel = new Excel6.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excel6.Workbooks)objExcel.Workbooks;
            objBook = (Excel6.Workbook)(objBooks.Add(miss));
            objSheet = (Excel6.Worksheet)objBook.ActiveSheet;
            objSheet.Name = Session.SessionID;

            int intColumn = dt.Columns.Count;
            for (int i = 1; i <= intColumn - 1; i++)
            {
                //SetRangeValue(objSheet,GetCellName(i,1),GetCellName(i,1),dt.Columns[i].ColumnName,true,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                objSheet.Cells[1, i] = dt.Columns[i].ColumnName;
            }

            int RowID = 2;

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 1; i <= intColumn - 1; i++)
                {
                    //SetRangeValue(objSheet,GetCellName(i,RowID),GetCellName(i,RowID),row[i].ToString(),false,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                    objSheet.Cells[RowID, i] = row[i].ToString();
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
                objSheet.PageSetup.Orientation = Excel6.XlPageOrientation.xlPortrait;
                objSheet.PageSetup.PaperSize = Excel6.XlPaperSize.xlPaperA4;
                objSheet.PageSetup.Zoom = false;
                objSheet.PageSetup.FitToPagesWide = 1;
                objSheet.PageSetup.FitToPagesTall = false;
            }
            catch
            {
                throw;
            }
            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, miss, miss, miss, miss, miss, Excel6.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
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

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";

        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";

        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excel6.ApplicationClass objExcel = null;
        Excel6.Workbooks objBooks = null;
        Excel6.Workbook objBook = null;
        Excel6.Worksheet objSheet = null;
        try
        {
            objExcel = new Excel6.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excel6.Workbooks)objExcel.Workbooks;
            objBook = (Excel6.Workbook)(objBooks.Add(missing));
            objSheet = (Excel6.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excel6.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
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
        Response.Charset = "GB2312";
        Response.Buffer = true;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
        Response.ContentType = "application/ms-excel";
        //Response.ContentType = "application/vnd.ms-excel";
        this.EnableViewState = false;
        Response.WriteFile(ExportPath + strFileName);
        //Response.Flush();
        //File.Delete(ExportPath + strFileName);

        Response.End();
    }
    protected void dgMtrak_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgMtrak.CurrentPageIndex = e.NewPageIndex;
        string strsql = getdata();
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgMtrak.DataSource = dt.DefaultView;
            dgMtrak.DataBind();
            Label1.Text = "Current Page:" + (dgMtrak.CurrentPageIndex + 1).ToString() + "/" + dgMtrak.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
        } 
    }
}
