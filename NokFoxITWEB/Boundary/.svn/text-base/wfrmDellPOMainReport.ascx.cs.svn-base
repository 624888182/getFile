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
using System.Data.SqlClient;
using DBAccess.EAI;
using System.Reflection;
using Excel0 = Microsoft.Office.Interop.Excel;
using DB.EAI;
using System.IO;

public partial class Boundary_wfrmDellPOMainReport : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

        string strStartDate = Request.QueryString["StartDate"].ToString();       
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strPlantCode = Request.QueryString["PlantCode"].ToString();
        string strCsoNum = Request.QueryString["CsoNum"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strUploadFlag = Request.QueryString["UploadFlag"].ToString();
        string strCtoMod = Request.QueryString["CtoMod"].ToString();
        string strSoNo = Request.QueryString["SoNo"].ToString();
        string strIdocNo = Request.QueryString["IdocNo"].ToString();
        string strSfcFlag = Request.QueryString["SfcFlag"].ToString();
        string strAckFlag = Request.QueryString["AckFlag"].ToString();
        string strOrder = Request.QueryString["Order"].ToString(); 
        int PageRows = Convert.ToInt16(Request.QueryString["PageRows"].ToString());


        lblFromDate1.Text = strStartDate;
        lblToDate1.Text = strEndDate;

        if (!IsPostBack)
        {
            bind(); //调用数据绑定方法
        }
    }

    private void bind()//数据绑定方法
    {
        //string strSql = "SELECT * FROM [PURCHASEORDERSMAIN]";
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetData();
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //SqlDataAdapter sda1 = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "PURCHASEORDERSMAIN");
        this.gvPO.DataSource = ds.Tables[0].DefaultView;
 
             


        this.gvPO.AllowPaging = true;
        this.gvPO.PageSize = Convert.ToInt16(Request.QueryString["PageRows"].ToString());//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈
        this.gvPO.DataBind();

        if (this.gvPO.PageIndex == 0)
        {
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
        }
        else
        {
            this.btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
        }

        if (gvPO.PageIndex == gvPO.PageCount - 1)
        {
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
        }
        else
        {
            this.btnNext.Enabled = true;
            btnLast.Enabled = true;
        }

        lblNow.Visible = true;
        int now = gvPO.PageIndex + 1;
        lblNow.Text = "第" + now.ToString() + "頁 / ";
        lblTotal.Visible = true;
        lblTotal.Text = "共" + gvPO.PageCount.ToString() + "頁   轉到";

       // conn.Close();
    }

    protected void PagebtnClick(object sender, EventArgs e)
    {
        //gvPO.PageIndex = Convert.ToInt32(((ImageButton)sender).CommandName) - 1; 
        switch (((ImageButton)sender).CommandArgument.ToString())
        {
            case "First":
                gvPO.PageIndex = 0;
                break;
            case "Previous":
                if (gvPO.PageIndex >= 1)
                {
                    gvPO.PageIndex = gvPO.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false; 
                }
                break;
            case "Next":
                if (gvPO.PageIndex < gvPO.PageCount-1)
                {
                    gvPO.PageIndex = gvPO.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvPO.PageIndex < gvPO.PageCount-1)
                    gvPO.PageIndex = gvPO.PageCount - 1;
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "-1":
                {
                    try
                    {
                        int index = int.Parse(txtNo.Text.Trim());
                        if (index > 0 && index < gvPO.PageCount + 1)
                            gvPO.PageIndex = index - 1;
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('輸入錯誤！！');</script>");
                            return;
                        }
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('輸入錯誤！！');</script>");
                        return;                    
                    }
                }
                break;
        }
        bind();
    }


    //private DataTable GetIDTable(DataTable dt)
    //{
    //    DataColumn col = new DataColumn("Index", Type.GetType("System.Int32"));
    //    dt.Columns.Add(col);
    //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
    //    {
    //        if (0 == i)
    //            dt.Rows[0][col] = 1;
    //        else
    //            dt.Rows[i][col] = Convert.ToInt32(dt.Rows[i - 1][col]) + 1;
    //    }
    //    return dt;
    //}

    //protected void gvPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView gvw = (GridView)sender;

    //    if (e.NewPageIndex < 0)
    //    {
    //        TextBox pageNum = (TextBox)gvw.BottomPagerRow.FindControl("txtNo");

    //        int Pa = int.Parse(pageNum.Text);
    //        if (Pa <= 0)
    //            gvw.PageIndex = 0;
    //        else
    //            gvw.PageIndex = Pa - 1;
    //    }
    //    else
    //    {
    //        gvw.PageIndex = e.NewPageIndex;
    //    }
    //    bind();
    //}

    //protected void gvPO_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Pageindex")
    //    {
    //        TextBox txtNo = GridView1.BottomPagerRow.FindControl("txtNo") as TextBox;
    //        int pageindex = Convert.ToInt32(txt.Text);
    //        gvPO.PageIndex = pageindex - 1;
    //        gvPO.DataSource = (DataView)Session["temp"];
    //        gvPO.DataBind();
    //    } 
    //}
    protected void gvPO_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvPO.PageIndex * gvPO.PageSize + e.Row.RowIndex + 1);
        }

    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问 
    }
 

    protected void btnSingle_Click(object sender, ImageClickEventArgs e)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //string style = @" <style> .text { mso-number-format:\@; } </script> ";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", "attachment; filename='" + strFileName + "'");
        //Response.ContentType = "application/ms-excel";
        //Response.Charset = "GB2312";
        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //gvPO.RenderControl(htw);
        //Response.Write(style);
        //Response.Write(sw.ToString());
        //Response.End(); 

        //gvPO.AllowPaging = false; //清除分页 
        //gvPO.AllowSorting = false; //清除排序      
        //bind();  //你绑定gridview1数据源的那个函数。  

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

        Response.Charset = "gb2312";

        Response.ContentType = "application/vnd.ms-excel";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gvPO.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.Write(" </form>\r\n");
        Response.Flush();
        Response.Close();
        Response.End(); 



    }
    protected void btnAll_Click(object sender, ImageClickEventArgs e)
    { 
        //try
        //{
        //    string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        //    string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //    Response.Clear();
        //    Response.AddHeader("content-disposition", "attachment;filename="+strFileName);
        //    Response.Charset = "gb2312";
        //    Response.ContentType = "application/vnd.xls";
        //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //    gvPO.RenderControl(htmlWrite);

        //    Response.Write(stringWrite.ToString());
        //    Response.End();
        //}
        //catch
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
        //    return;
        //}

        string strSql = GetData();
        try
        {
            ExortToExcel1(strSql);
        }
        catch
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
            //return;
        }

    }

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excel0.ApplicationClass objExcel = null;
        Excel0.Workbooks objBooks = null;
        Excel0.Workbook objBook = null;
        Excel0.Worksheet objSheet = null;
        try
        {
            objExcel = new Excel0.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excel0.Workbooks)objExcel.Workbooks;
            objBook = (Excel0.Workbook)(objBooks.Add(missing));
            objSheet = (Excel0.Worksheet)objBook.ActiveSheet;

            ClsDBToExcelSQL.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excel0.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
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
        // Response.Flush();
        this.EnableViewState = false;
        Response.WriteFile(ExportPath + strFileName);
        //System.Diagnostics.Process.Start(ExportPath+strFileName);   

        Response.End();
    }

    private string GetData()
    {
        string strsql = "SELECT * FROM [PURCHASEORDERSMAIN] ";
       // string strsql = "SELECT * FROM SHP.ZJJ_CMCS_SFC_IMEINUM ";
        
        return strsql;
    }
}
