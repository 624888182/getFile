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
using System.Data.OracleClient;
using System.Reflection;
using Excel5 = Microsoft.Office.Interop.Excel;
using DB.EAI;

public partial class Boundary_wfrmFQCFailQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnDateFrom.Attributes["onclick"] = "return showCalendar('"+tbStartDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
            //btnDateTo.Attributes["onclick"] = "return showCalendar('"+tbEndDate.ClientID+"', '%Y/%m/%d %H:%M', '24', true);";
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
            InitiaPage();
            MultiLanguage();
        }
    }

    private void InitiaPage()
    { 
        string strProcedureName = "SFCQUERY.GETMODELNAME";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
        orapara[0].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();
        ddlModel.Items.Insert(0, ""); 
    }
 
    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");//rm.GetString("Model");
        lblWO.Text = (String)GetGlobalResourceObject("SFCQuery", "WO");//rm.GetString("WO");
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");//rm.GetString("DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");//rm.GetString("DateTo");
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
        Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgfqc.CurrentPageIndex = 0;
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

        if (ddlModel.SelectedValue.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種！！');</script>");
            return;
        }
        try
        {
            dgfqc.Visible = true;
            Label4.Visible = true;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];
            dgfqc.DataSource = dt.DefaultView;
            dgfqc.DataBind();
            Label4.Text = "Current Page:" + (dgfqc.CurrentPageIndex + 1).ToString() + "/" + dgfqc.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

            dt.Dispose();
        }
        catch
        {
            dgfqc.Visible = false;
            Label4.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在該工站的查詢數據！！');</script>");
            return;
        }
    }

    private string GetData()
    {
        string strModel = ddlModel.SelectedValue.ToString();
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();

        string strsql = "select distinct s.wo_no,substr(u.apart,3,3) model,s.product_id,s.creation_date,s.station_id,"
            +" s.state_id,s.line_name,s.emp_id,t.defect_code,description error_msg"
            +" from sfc.mes_assy_history s,shp.cmcs_sfc_aorder u,sfc.mes_product_fail_history t,sfc.mes_repair_defectcode r"
            +" where s.station_id='FQC' and s.state_id='F' and s.wo_no=u.aorder and s.product_id=t.product_id"
            +" and s.station_id=t.station_id and t.defect_code=r.defect_code"; 

        if (!tbProductID.Text.Trim().Equals(""))
            strsql = strsql + " and s.product_id=" + ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper());

        if (!tbWO.Text.Trim().Equals(""))
            strsql = strsql + " and s.wo_no= " + ClsCommon.GetSqlString(tbWO.Text.Trim().ToUpper());

        strsql = strsql + " and s.creation_date BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
               + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";
        if (!strModel.Equals(""))
            strsql = strsql + " and substr(u.apart,3,3)='" + strModel + "'";

        return strsql;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
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
 
        string strSql = GetData(); 
        ExortToExcel1(strSql);
    }

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";

        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";

        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excel5.ApplicationClass objExcel = null;
        Excel5.Workbooks objBooks = null;
        Excel5.Workbook objBook = null;
        Excel5.Worksheet objSheet = null;
        try
        {
            objExcel = new Excel5.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excel5.Workbooks)objExcel.Workbooks;
            objBook = (Excel5.Workbook)(objBooks.Add(missing));
            objSheet = (Excel5.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excel5.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
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
        this.EnableViewState = false;
        Response.WriteFile(ExportPath + strFileName); 

        Response.End();
    }

    protected void dgfqc_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgfqc.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];
        dgfqc.DataSource = dt.DefaultView;
        dgfqc.DataBind();
        Label4.Text = "Current Page:" + (dgfqc.CurrentPageIndex + 1).ToString() + "/" + dgfqc.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
}
