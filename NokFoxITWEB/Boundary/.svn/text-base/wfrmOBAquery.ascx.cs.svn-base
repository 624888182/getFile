using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBAccess.EAI;
using DB.EAI;
using System.Reflection;
using Exceld = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Text;
using System.IO;
using System.Xml;
using UsingClass;
using System.Web.UI.WebControls; 

public partial class Boundary_wfrmOBAquery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanaguage();
        }
    }
    private void MultiLanaguage()
    {
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
        lblCartonNo.Text = (String)GetGlobalResourceObject("SFCQuery", "CartonID");

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgOBA.CurrentPageIndex = 0;
        string strSql="";
        if (tbCartonNo.Text.Trim().Equals("") && tbPID.Text.Trim().Equals(""))
        {
            Label3.Text = "請輸入查詢條件";
            Label3.Visible = true;
        }
        else
           strSql = GetData();
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            Label3.Visible = false;
            dgOBA.Visible = true;
            //Label4.Text = "Current Page:" + (dgOBA.CurrentPageIndex + 1).ToString() + "/" + dgOBA.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();
            Label4.Text = " Total:" + dt1.Rows.Count.ToString();
            Label4.Visible = true;
            dgOBA.DataSource = dt1.DefaultView;
            dgOBA.DataBind();
        }
        else
        {
            dgOBA.Visible = false;
            Label3.Text = "Input Error!";
            Label3.Visible = true;
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string strSql = GetData();
        ExortToExcel1(strSql);
    }

    private string GetData()
    {
        string strSql = "";
        string strCartonNo = tbCartonNo.Text.Trim().ToUpper();
        string strPID = tbPID.Text.Trim().ToUpper();
        
        if (!tbCartonNo.Text.Trim().Equals(""))
        {
            if (!tbPID.Text.Trim().Equals(""))
                strSql = "SELECT * FROM SHP.CMCS_SFC_SHIPPING_DATA WHERE CARTON_NO ='" + strCartonNo + "' and (PRODUCTID ='" + strPID + "' or IMEI='" + strPID + "')";
            else
                strSql = "SELECT * FROM SHP.CMCS_SFC_SHIPPING_DATA WHERE CARTON_NO ='" + strCartonNo + "'";
        }
        else
        {
            if (!tbPID.Text.Trim().Equals(""))
                strSql = "SELECT * FROM SHP.CMCS_SFC_SHIPPING_DATA WHERE PRODUCTID ='" + strPID + "' or IMEI='" + strPID + "'";
        }
        return strSql;
    }

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Exceld.ApplicationClass objExcel = null;
        Exceld.Workbooks objBooks = null;
        Exceld.Workbook objBook = null;
        Exceld.Worksheet objSheet = null;
        try
        {
            objExcel = new Exceld.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Exceld.Workbooks)objExcel.Workbooks;
            objBook = (Exceld.Workbook)(objBooks.Add(missing));
            objSheet = (Exceld.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Exceld.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
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
    protected void dgOBA_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgOBA.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];
        dgOBA.DataSource = dt.DefaultView;
        dgOBA.DataBind();
        Label4.Text = "Current Page:" + (dgOBA.CurrentPageIndex + 1).ToString() + "/" + dgOBA.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
}
