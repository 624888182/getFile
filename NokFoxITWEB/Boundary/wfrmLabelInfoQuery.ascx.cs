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
using Excele = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using DB.EAI;
using System.Data.OracleClient;
using System.IO;


public partial class Boundary_wfrmLabelInfoQuery : System.Web.UI.UserControl
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
        Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgShipLabel.CurrentPageIndex = 0;

        if (!tbShipDate.Text.Trim().Equals(""))
        {
            if (!ClsCommon.CheckIsDateTime(tbShipDate.Text.Trim()))
            {
                Label28.Visible = true;
                return;
            }
        }
        Label28.Visible = false;

        if (tbPO.Text.Trim().Equals("")&&tbPN.Text.Trim().Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入PO或PN！！');</script>");
            return;
        }
        try
        {
            dgShipLabel.Visible = true;
            Label4.Visible = true;
            try
            {
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    dgShipLabel.DataSource = dt.DefaultView;
                    dgShipLabel.DataBind();
                    Label4.Text = "Current Page:" + (dgShipLabel.CurrentPageIndex + 1).ToString() + "/" + dgShipLabel.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

                    dt.Dispose();
                    btnExportExcel.Visible = true;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無相關資料！！');</script>");
                    return;
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無相關資料！！');</script>");
                return;
            }
        }
        catch
        {
            btnExportExcel.Visible = false;
            dgShipLabel.Visible = false;
            Label4.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無相關資料！！');</script>");
            return;
        }
    }
    protected void dgShipLabel_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgShipLabel.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];
        dgShipLabel.DataSource = dt.DefaultView;
        dgShipLabel.DataBind();
        Label4.Text = "Current Page:" + (dgShipLabel.CurrentPageIndex + 1).ToString() + "/" + dgShipLabel.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string strSql = GetData();
        //try
        //{
        ExortToExcel1(strSql);
    }

    private string GetData()
    {
        string strpo = tbPO.Text.Trim().ToUpper();
        string strpn = tbPN.Text.Trim().ToUpper(); 
        string strshipdate = tbShipDate.Text.Trim();
        string strsql = "select po_no,pn,color,ship_carton_no,order_carton_no,work_order,carton_no,big_carton_no,to_char(ship_date,'YYYY/MM/DD') ship_date,invoice_no,print_date from shp.cmcs_sfc_ship_carton_map" +
               " where po_no like '%" + strpo + "%' and pn like '%" + strpn + "%'";
        if (!tbShipDate.Text.Trim().Equals(""))
        {
            if (!ClsCommon.CheckIsDateTime(tbShipDate.Text.Trim()))
            {
                Label28.Visible = true;
                return "error";
            }
            else
            {
                strsql += " and ship_date=to_date(" + ClsCommon.GetSqlString(strshipdate) + ",'YYYY/MM/DD') ";
            }
        }
        return strsql;
    }
    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";

        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";

        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excele.ApplicationClass objExcel = null;
        Excele.Workbooks objBooks = null;
        Excele.Workbook objBook = null;
        Excele.Worksheet objSheet = null;
        try
        {
            objExcel = new Excele.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excele.Workbooks)objExcel.Workbooks;
            objBook = (Excele.Workbook)(objBooks.Add(missing));
            objSheet = (Excele.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excele.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
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
}
