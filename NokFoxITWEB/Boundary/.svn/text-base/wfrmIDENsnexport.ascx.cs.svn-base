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
using Excel7 = Microsoft.Office.Interop.Excel;
using DB.EAI;
using System.Data.OracleClient;

public partial class Boundary_wfrmIDENsnexport : System.Web.UI.UserControl
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
            MultiLanguage();
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
        this.dgSN.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgSN_PageIndexChanged);

    }
    #endregion

    private void MultiLanguage()
    { 
        lblWO.Text = (String)GetGlobalResourceObject("SFCQuery", "WO");//rm.GetString("WO");       
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
        Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql;
        string strWO = txtWO.Text.Trim().ToUpper();
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        if (strWO.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入你要查詢的S工單！！');</script>");
            return;
        }
        if(strStartDate.Equals("")&strEndDate.Equals(""))
            strsql = "SELECT a.product_id, b.item_no, c.sorder, c.model, c.spart,a.create_date FROM mes_pcba_panel_link a,mes_pcba_panel_detail b,"
                               + "shp.cmcs_sfc_sorder c WHERE a.panel_id = b.panel_id AND b.wo_no = c.sorder and c.sorder='" + strWO + "' order by a.create_date asc";
        else
            strsql="SELECT a.product_id,  b.item_no, c.sorder, c.model, c.spart,a.create_date FROM mes_pcba_panel_link a,mes_pcba_panel_detail b,"
                       + "shp.cmcs_sfc_sorder c WHERE a.panel_id = b.panel_id AND b.wo_no = c.sorder and c.sorder='" + strWO + "' and  a.create_date BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
                       + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  order by a.create_date asc";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgSN.DataSource = dt.DefaultView;
            dgSN.DataBind();
            Label4.Text = "Current Page:" + (dgSN.CurrentPageIndex + 1).ToString() + "/" + dgSN.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

            dt.Dispose();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('沒有你要查詢的資料！！');</script>");
            return ;
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()) & !ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('導出Excel時,必須輸入時間範圍!');</script>");
            return;
        }

        Label28.Visible = false;
        Label29.Visible = false;

        System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
        if (intday.TotalDays > 7)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於7天！');</script>");
            return;
        }

        //DataTable dt = GetData();
        //ExortToExcel(dt);
        string strSql = GetData();
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        if (dt.Rows.Count > 0)
            ExortToExcel(dt);
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('此段時間沒有數據！');</script>");
            return;
        }

    }

    private void ExortToExcel(DataTable dt)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing miss = Missing.Value;

        Excel7.ApplicationClass objExcel = null;
        Excel7.Workbooks objBooks = null;
        Excel7.Workbook objBook = null;
        Excel7.Worksheet objSheet = null;

        try
        {
            objExcel = new Excel7.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excel7.Workbooks)objExcel.Workbooks;
            objBook = (Excel7.Workbook)(objBooks.Add(miss));
            objSheet = (Excel7.Worksheet)objBook.ActiveSheet; 
            //objSheet.Name = s.SelectedValue + "_" + ddlStation.SelectedValue + "_" + rblQueryType.SelectedValue;

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
                    objSheet.Cells[RowID, i] = row[i - 1].ToString();
                }

                RowID++;
            }
            //

            objSheet.Columns.AutoFit();
            objSheet.Rows.AutoFit();

            //頁面設置
            try
            {
                //objSheet.PageSetup.LeftMargin = 20;
                //objSheet.PageSetup.RightMargin = 20;
                //objSheet.PageSetup.TopMargin = 35;
                //objSheet.PageSetup.BottomMargin = 15;
                //objSheet.PageSetup.HeaderMargin = 7;
                //objSheet.PageSetup.FooterMargin = 10;
                //objSheet.PageSetup.CenterHorizontally = true;
                //objSheet.PageSetup.CenterVertically = false;
                //objSheet.PageSetup.Orientation = Excel7.XlPageOrientation.xlPortrait;
                //objSheet.PageSetup.PaperSize = Excel7.XlPaperSize.xlPaperA4;
                //objSheet.PageSetup.Zoom = false;
                //objSheet.PageSetup.FitToPagesWide = 1;
                //objSheet.PageSetup.FitToPagesTall = false;
            }
            catch
            {
                throw;
            }
            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, miss, miss, miss, miss, miss, Excel7.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
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

    protected void dgSN_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgSN.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetData()).Tables[0];
        dgSN.DataSource = dt.DefaultView;
        dgSN.DataBind();
        Label4.Text = "Current Page:" + (dgSN.CurrentPageIndex + 1).ToString() + "/" + dgSN.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }

    private string GetData()
    {
        string strsql = "";
        string strWO = txtWO.Text.Trim().ToUpper();
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        if (strWO.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入你要查詢的S工單！！');</script>");
            return "";
        }
        if(strStartDate.Equals("")&strEndDate.Equals(""))
            strsql = "SELECT a.product_id,b.item_no, c.sorder, c.model, c.spart,a.create_date FROM mes_pcba_panel_link a,mes_pcba_panel_detail b,"
                               + "shp.cmcs_sfc_sorder c WHERE a.panel_id = b.panel_id AND b.wo_no = c.sorder and c.sorder='" + strWO + "' order by a.create_date asc";
        else
            strsql="SELECT a.product_id,b.item_no, c.sorder, c.model, c.spart,a.create_date FROM mes_pcba_panel_link a,mes_pcba_panel_detail b,"
                       + "shp.cmcs_sfc_sorder c WHERE a.panel_id = b.panel_id AND b.wo_no = c.sorder and c.sorder='" + strWO + "' and  a.create_date BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
                       + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  order by a.create_date asc";
        return strsql;
    }
}
