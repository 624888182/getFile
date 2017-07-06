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
using Excel = Microsoft.Office.Interop.Excel;
using DB.EAI;
using System.IO;

public partial class Boundary_B2B_Invoice_Report : System.Web.UI.Page
{
    #region Web Form Designer generated code

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    #endregion

    private void DisableControls(Control gv)
    {

        LinkButton lb = new LinkButton();
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {
                l.Text = (gv.Controls[i] as LinkButton).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {
                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }

            if (gv.Controls[i].HasControls())
            {
                DisableControls(gv.Controls[i]);
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strIdocNo = Request.QueryString["IdocNo"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strLoadID = Request.QueryString["LoadID"].ToString();
        string strSendFlag = Request.QueryString["SendFlag"].ToString();
        string strInvoiceNo = Request.QueryString["InvoiceNo"].ToString();
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
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetData();
        if (strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //SqlDataAdapter sda1 = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "InvoiceMain");
        this.gvInvoice.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvInvoice.DataSource = ds;
            gvInvoice.DataBind();
            int columnCount = gvInvoice.Rows[0].Cells.Count;
            gvInvoice.Rows[0].Cells.Clear();
            gvInvoice.Rows[0].Cells.Add(new TableCell());
            gvInvoice.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvInvoice.Rows[0].Cells[0].Text = "No Records Found.";
            gvInvoice.Rows[0].Visible = true;
            btnAll.Visible = false;
            lblAll.Visible = false;
            lblSingle.Visible = false;
            btnSingle.Visible = false;
            panel3.Visible = false;
        }
        else
        {
            this.gvInvoice.AllowPaging = true;
            this.gvInvoice.PageSize = Convert.ToInt16(Request.QueryString["PageRows"].ToString());//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈

            if (ds.Tables[0].Rows.Count <= gvInvoice.PageSize)
                panel3.Visible = false;
            else
            {
                if (this.gvInvoice.PageIndex == 0)
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                else
                {
                    this.btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (gvInvoice.PageIndex == gvInvoice.PageCount - 1)
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
                int now = gvInvoice.PageIndex + 1;
                lblNow.Text = "第" + now.ToString() + "頁 / ";
                lblTotal.Visible = true;
                int count = ds.Tables[0].Rows.Count / gvInvoice.PageSize;
                lblTotal.Text = "共" + count + "頁   轉到";
            }
            this.gvInvoice.DataBind();
        }
        conn.Close();
    }

    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {
        //gvPO.PageIndex = Convert.ToInt32(((ImageButton)sender).CommandName) - 1; 
        switch (((ImageButton)sender).CommandArgument.ToString())
        {
            case "First":
                gvInvoice.PageIndex = 0;
                break;
            case "Previous":
                if (gvInvoice.PageIndex >= 1)
                {
                    gvInvoice.PageIndex = gvInvoice.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                break;
            case "Next":
                if (gvInvoice.PageIndex < gvInvoice.PageCount - 1)
                {
                    gvInvoice.PageIndex = gvInvoice.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvInvoice.PageIndex < gvInvoice.PageCount - 1)
                    gvInvoice.PageIndex = gvInvoice.PageCount - 1;
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
                        if (index > 0 && index < gvInvoice.PageCount + 1)
                            gvInvoice.PageIndex = index - 1;
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

    protected void gvInvoice_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        LinkButton lbtInvoiceno;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtInvoiceno = (LinkButton)e.Row.Cells[8].Controls[1];
            if (lbtInvoiceno != null)
            {
                if (lbtInvoiceno.CommandName == "InvoiceNo")
                    lbtInvoiceno.CommandArgument = e.Row.RowIndex.ToString();

            }
        }
    }
    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvInvoice.PageIndex * gvInvoice.PageSize + e.Row.RowIndex + 1);
        }
    }

    protected void btnSingle_Click(object sender, ImageClickEventArgs e)
    {
        DisableControls(gvInvoice);
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=B2B_Invoice_Report.xls");
        Response.ContentType = "application/ms-excel";
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvInvoice.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();  
    }
    protected void btnAll_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF7;
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_Invoice_Report.xls");
            Response.ContentType = "application/ms-excel";
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            this.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
            //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
            string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            string strsql = GetData();
            if (strsql == "error" || strsql == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('查詢條件錯誤,請檢查！！');</script>");
                return;
            }
            SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

            DataSet ds = new DataSet();
            sda.Fill(ds, "InvoiceMain1");
            this.gvInvoice.DataSource = ds.Tables[0].DefaultView;

            this.gvInvoice.AllowPaging = false;
            this.gvInvoice.DataBind();
            DisableControls(gvInvoice);
            gvInvoice.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();

            // turn the paging on again 
            gvInvoice.AllowPaging = true;
            bind();

        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
            return;
        }       

    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问 
    }

    private string GetData()
    {
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strIdocNo = Request.QueryString["IdocNo"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strLoadID = Request.QueryString["LoadID"].ToString();
        string strSendFlag = Request.QueryString["SendFlag"].ToString().ToUpper();
        string strInvoiceNo = Request.QueryString["InvoiceNo"].ToString();
        string strOrder = Request.QueryString["Order"].ToString();
        int PageRows = Convert.ToInt16(Request.QueryString["PageRows"].ToString());

        string strsql = "select * from dbo.invoicemain(nolock) where "
           + " lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and lasteditdt<=convert(varchar,'" + strEndDate + "',120)";

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and ordnum in (" + strPoNo + ")";
        }

        if (!strIdocNo.Equals(""))
        {
            strIdocNo = Split(strIdocNo);
            if (strIdocNo.Equals("split error"))
                return "input error";
            else
                strsql += " and idocno in (" + strIdocNo + ")";
        }

        if (!strMesgID.Equals(""))
        {
            strMesgID = Split(strMesgID);
            if (strMesgID.Equals("split error"))
                return "input error";
            else
                strsql += " and mesgid in (" + strMesgID + ")";
        }

        if (!strLoadID.Equals(""))
        {
            strLoadID = Split(strLoadID);
            if (strLoadID.Equals("split error"))
                return "input error";
            else
                strsql += " and loadid in (" + strLoadID + ")";
        }

        if (!strInvoiceNo.Equals(""))
        {
            strInvoiceNo = Split(strInvoiceNo);
            if (strInvoiceNo.Equals("split error"))
                return "input error";
            else
                strsql += " and invcnm in (" + strInvoiceNo + ")";
        }

        if (!strSendFlag.Equals("ALL"))
        {
            strsql += " and sendflag ='" + strSendFlag + "'"; 
        }

        strsql += " order by lasteditdt " + strOrder;

        return strsql;
    }

    private string Split(string strPoNo)
    {
 	    string strInput=strPoNo;
        string strOutput="";
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
            return "error";
        }
    }
    protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "InvoiceNo")
        {
            string strInvoiceno = ((LinkButton)gvInvoice.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Controls[1]).Text;
            string strSendid = ((Label)gvInvoice.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;
            string strReceid = ((Label)gvInvoice.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;
           
            string strScript = "<script language='jscript'>var res = window.open('./B2B_InvoiceItem_Report.aspx?Sendid=" + strSendid + "&Receid=" + strReceid
                + "&Invoiceno=" + strInvoiceno + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
}
