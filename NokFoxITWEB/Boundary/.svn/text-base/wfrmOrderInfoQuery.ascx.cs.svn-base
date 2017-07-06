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
using Excelb = Microsoft.Office.Interop.Excel;
using DB.EAI;
using System.Reflection;


public partial class Boundary_wfrmOrderInfoQuery : System.Web.UI.UserControl
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
                dgOrder.DataSource = dt.DefaultView;
                dgOrder.DataBind();
                Label1.Text = "Current Page:" + (dgOrder.CurrentPageIndex + 1).ToString() + "/" + dgOrder.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            }
            else
            {
                btnExportExcel.Visible = false;
                dgOrder.Visible = false;
                Label1.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('沒有你要查詢的數據...');</script>");
                return;
            }
        }

    }

    private string getdata()
    {
        string strsql = "";
        string strPpart = txtPpart.Text.Trim().ToUpper();
        string strPorder = txtPorder.Text.Trim().ToUpper();
        if (strPorder.Equals("") && strPpart.Equals(""))
            strsql = "no data";
        else
          strsql = "select * from shp.cmcs_sfc_porder where 1=1 ";
      if (strPpart.Length > 0)
          strsql = strsql + " and ppart='" + strPpart + "'";
      if (strPorder.Length > 0)
          strsql = strsql + " and porder='" + strPorder + "'"; 
        return strsql;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string strsql = getdata();
        ExortToExcel1(strsql);
    }

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";

        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";

        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excelb.ApplicationClass objExcel = null;
        Excelb.Workbooks objBooks = null;
        Excelb.Workbook objBook = null;
        Excelb.Worksheet objSheet = null;
        try
        {
            objExcel = new Excelb.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excelb.Workbooks)objExcel.Workbooks;
            objBook = (Excelb.Workbook)(objBooks.Add(missing));
            objSheet = (Excelb.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excelb.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
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
    protected void dgOrder_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgOrder.CurrentPageIndex = e.NewPageIndex;
        string strsql = getdata();
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgOrder.DataSource = dt.DefaultView;
            dgOrder.DataBind();
            Label1.Text = "Current Page:" + (dgOrder.CurrentPageIndex + 1).ToString() + "/" + dgOrder.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
        } 
    }
}
