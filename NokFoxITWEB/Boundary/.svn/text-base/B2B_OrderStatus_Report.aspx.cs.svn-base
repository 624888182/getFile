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

public partial class Boundary_B2B_OrderStatus_Report : System.Web.UI.Page
{
    #region Web Form Designer generated code

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strIdocNo = Request.QueryString["IdocNo"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strStatus = Request.QueryString["Status"].ToString();
        string strSendFlag = Request.QueryString["SendFlag"].ToString();
        string strOrder = Request.QueryString["Order"].ToString();
        int PageRows = Convert.ToInt16(Request.QueryString["PageRows"].ToString());

        lblFromDate1.Text = strStartDate;
        lblToDate1.Text = strEndDate;

        if (!IsPostBack)
        {
            bind(); //调用数据绑定方法
        }
    }

    private string GetData()
    {
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strIdocNo = Request.QueryString["IdocNo"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strStatus = Request.QueryString["Status"].ToString().ToUpper();
        string strSendFlag = Request.QueryString["SendFlag"].ToString().ToUpper();
        string strOrder = Request.QueryString["Order"].ToString();
        int PageRows = Convert.ToInt16(Request.QueryString["PageRows"].ToString());

        string strsql = "select * from purchaseorderstatus(nolock) where sendflag<>'F'"
            + " and lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and lasteditdt<=convert(varchar,'" + strEndDate + "',120)";
       
        if (!strMesgID.Equals(""))
        {
            strMesgID = Split(strMesgID);
            if (strMesgID.Equals("split error"))
                return "input error";
            else
                strsql += " and mesgid in (" + strMesgID + ")";
        }

        if (!strIdocNo.Equals(""))
        {
            strIdocNo = Split(strIdocNo);
            if (strIdocNo.Equals("split error"))
                return "input error";
            else
                strsql += " and idocno in (" + strIdocNo + ")";
        }

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and ordnum in (" + strPoNo + ")";
        }

        if (!strStatus.Equals("ALL"))
        {
            strsql += " and status ='" + strStatus + "'";
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
        sda.Fill(ds, "PURCHASEORDERSMAIN");
        this.gvOrderStatus.DataSource = ds.Tables[0].DefaultView;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvOrderStatus.DataSource = ds;
            gvOrderStatus.DataBind();
            int columnCount = gvOrderStatus.Rows[0].Cells.Count;
            gvOrderStatus.Rows[0].Cells.Clear();
            gvOrderStatus.Rows[0].Cells.Add(new TableCell());
            gvOrderStatus.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvOrderStatus.Rows[0].Cells[0].Text = "No Records Found.";
            gvOrderStatus.Rows[0].Visible = true;
            btnAll.Visible = false;
            lblAll.Visible = false;
            lblSingle.Visible = false;
            btnSingle.Visible = false;
            panel3.Visible = false;
        }
        else
        {
            this.gvOrderStatus.AllowPaging = true;

            this.gvOrderStatus.PageSize = Convert.ToInt16(Request.QueryString["PageRows"].ToString());//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈
            if (gvOrderStatus.PageCount <= 1)
                panel3.Visible = false;
            else
            {
                if (this.gvOrderStatus.PageIndex == 0)
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                else
                {
                    this.btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (gvOrderStatus.PageIndex == gvOrderStatus.PageCount - 1)
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
                int now = gvOrderStatus.PageIndex + 1;
                lblNow.Text = "第" + now.ToString() + "頁 / ";
                lblTotal.Visible = true;
                txtNo.Text = now.ToString();
                lblTotal.Text = "共" + gvOrderStatus.PageCount.ToString() + "頁   轉到";
            }
            this.gvOrderStatus.DataBind();
        }
        conn.Close();
    }

    protected void gvOrderStatus_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvOrderStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[2].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[7].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[14].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvOrderStatus.PageIndex * gvOrderStatus.PageSize + e.Row.RowIndex + 1);
        }
    }
    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {
        //gvPO.PageIndex = Convert.ToInt32(((ImageButton)sender).CommandName) - 1; 
        switch (((ImageButton)sender).CommandArgument.ToString())
        {
            case "First":
                gvOrderStatus.PageIndex = 0;
                break;
            case "Previous":
                if (gvOrderStatus.PageIndex >= 1)
                {
                    gvOrderStatus.PageIndex = gvOrderStatus.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                break;
            case "Next":
                if (gvOrderStatus.PageIndex < gvOrderStatus.PageCount - 1)
                {
                    gvOrderStatus.PageIndex = gvOrderStatus.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvOrderStatus.PageIndex < gvOrderStatus.PageCount - 1)
                    gvOrderStatus.PageIndex = gvOrderStatus.PageCount - 1;
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
                        if (index > 0 && index < gvOrderStatus.PageCount + 1)
                            gvOrderStatus.PageIndex = index - 1;
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
    protected void btnSingle_Click(object sender, ImageClickEventArgs e)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=B2B_OrderStatus_Report.xls");
        Response.ContentType = "application/ms-excel";
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvOrderStatus.RenderControl(htw);
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
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_OrderStatus_Report.xls");
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
            if (strsql.Equals("input error"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
                return;
            }

            SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

            DataSet ds = new DataSet();
            sda.Fill(ds, "PURCHASEORDERSMAIN");
            this.gvOrderStatus.DataSource = ds.Tables[0].DefaultView;

            this.gvOrderStatus.AllowPaging = false;
            this.gvOrderStatus.DataBind();

            gvOrderStatus.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();

            // turn the paging on again 
            gvOrderStatus.AllowPaging = true;
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
}
