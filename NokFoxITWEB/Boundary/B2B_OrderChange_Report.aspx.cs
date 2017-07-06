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

public partial class Boundary_B2B_OrderChange_Report : System.Web.UI.Page
{
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
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strReqChange = Request.QueryString["ReqChange"].ToString();
        string strUploadFlag = Request.QueryString["UploadFlag"].ToString();
        string strChangeFlag = Request.QueryString["ChangeFlag"].ToString();
        string strAckSendFlag = Request.QueryString["AckSendFlag"].ToString();
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
        sda.Fill(ds, "OrderChange");
        this.gvOrderChange.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvOrderChange.DataSource = ds;
            gvOrderChange.DataBind();
            int columnCount = gvOrderChange.Rows[0].Cells.Count;
            gvOrderChange.Rows[0].Cells.Clear();
            gvOrderChange.Rows[0].Cells.Add(new TableCell());
            gvOrderChange.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvOrderChange.Rows[0].Cells[0].Text = "No Records Found.";
            gvOrderChange.Rows[0].Visible = true;
            btnAll.Visible = false;
            lblAll.Visible = false;
            lblSingle.Visible = false;
            btnSingle.Visible = false;
        }
        else
        {
            panel2.Visible = true;
            this.gvOrderChange.AllowPaging = true;
            this.gvOrderChange.PageSize = Convert.ToInt16(Request.QueryString["PageRows"].ToString());//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈
            this.gvOrderChange.DataBind();

            if (gvOrderChange.Rows.Count > gvOrderChange.PageSize)
            {
                if (this.gvOrderChange.PageIndex == 0)
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                else
                {
                    this.btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (gvOrderChange.PageIndex == gvOrderChange.PageCount - 1)
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
                int now = gvOrderChange.PageIndex + 1;
                lblNow.Text = "第" + now.ToString() + "頁 / ";
                lblTotal.Visible = true;
                lblTotal.Text = "共" + gvOrderChange.PageCount.ToString() + "頁   轉到";
            }
            else
            {
                panel2.Visible = false;
            }
        }
        conn.Close();
    }

    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {

        //gvPO.PageIndex = Convert.ToInt32(((ImageButton)sender).CommandName) - 1; 
        switch (((ImageButton)sender).CommandArgument.ToString())
        {
            case "First":
                gvOrderChange.PageIndex = 0;
                break;
            case "Previous":
                if (gvOrderChange.PageIndex >= 1)
                {
                    gvOrderChange.PageIndex = gvOrderChange.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                break;
            case "Next":
                if (gvOrderChange.PageIndex < gvOrderChange.PageCount - 1)
                {
                    gvOrderChange.PageIndex = gvOrderChange.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvOrderChange.PageIndex < gvOrderChange.PageCount - 1)
                    gvOrderChange.PageIndex = gvOrderChange.PageCount - 1;
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
                        if (index > 0 && index < gvOrderChange.PageCount + 1)
                            gvOrderChange.PageIndex = index - 1;
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
        DisableControls(gvOrderChange);
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=B2B_OrderChange_Report.xls");
        Response.ContentType = "application/ms-excel";
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvOrderChange.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();  
    }
    protected void btnAll_Click(object sender, ImageClickEventArgs e)
    {
        DisableControls(gvOrderChange);
        try
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF7;
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_OrderChange_Report.xls");
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
            sda.Fill(ds, "OrderChange");
            this.gvOrderChange.DataSource = ds.Tables[0].DefaultView;

            this.gvOrderChange.AllowPaging = false;
            this.gvOrderChange.DataBind();

            gvOrderChange.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();

            // turn the paging on again 
            gvOrderChange.AllowPaging = true;
            bind();

        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
            return;
        }       
    }
    private string GetData()
    {
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strReqChange = Request.QueryString["ReqChange"].ToString().ToUpper();
        string strUploadFlag = Request.QueryString["UploadFlag"].ToString().ToUpper();
        string strChangeFlag = Request.QueryString["ChangeFlag"].ToString().ToUpper();
        string strAckSendFlag = Request.QueryString["AckSendFlag"].ToString().ToUpper();
        string strOrder = Request.QueryString["Order"].ToString(); 
        int PageRows = Convert.ToInt16(Request.QueryString["PageRows"].ToString());

        string strsql = "select t1.*, t2.sendflag as acksendflag,t2.senddate as acksenddate,t2.ack_action,t2.reason as ackreason,t2.reason_desc as ackreason_desc from orderchange  t1 (nolock) join orderchangeack t2 (nolock) on t2.ordchangemesgid=t1.mesgid and t2.ordnum=t1.ordnum where t1.uploadflag<>'F'"
            + " and  t1.lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and t1.lasteditdt<=convert(varchar,'" + strEndDate + "',120)";

        if (!strMesgID.Equals(""))
        {
            strMesgID = Split(strMesgID);
            if (strMesgID.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.mesgid in (" + strMesgID + ")";
        }

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.ordnum in (" + strPoNo + ")";
        }

        if (!strReqChange.Equals("ALL"))
        {
            strsql += " and t1.req_change ='" + strReqChange+"'";
        }

        if (!strUploadFlag.Equals("ALL"))
        {
            strsql += " and t1.uploadflag ='" + strUploadFlag+"'";
        }

        if (!strChangeFlag.Equals("ALL"))
        {
            strsql += " and t1.changeflag ='" + strChangeFlag+"'";
        }

        if (!strAckSendFlag.Equals("ALL"))
        {
            strsql += " and t2.sendflag ='" + strAckSendFlag+"'";
        }
        
        strsql+=" order by t1.lasteditdt " +strOrder; 

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

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问 
    }
    protected void gvOrderChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[15].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvOrderChange.PageIndex * gvOrderChange.PageSize + e.Row.RowIndex + 1);
        }

    }
    protected void gvOrderChange_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
}
