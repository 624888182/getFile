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
using System.IO;
using DBAccess.EAI;

public partial class Boundary_B2B_OrderStatus_NEW : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;

            MultiLanguage();
        }
    }

    private void MultiLanguage()
    {
        lblgeshi1.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        lblgeshi2.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
    }

    private string GetData()
    {
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strMesgID = txtMesgid.Text.Trim();
        string strIdocNo = txtIdocNo.Text.Trim();
        string strPoNo = txtPoNo.Text.Trim();
        string strStatus = ddlStatus.SelectedValue.Trim().ToUpper();
        string strSendFlag = ddlSendFlag.SelectedValue.Trim().ToUpper();
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
        int PageRows = Convert.ToInt16(strPageRows);

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
            btnExport.Visible = false;
            panel3.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
            this.gvOrderStatus.AllowPaging = true;
            string pagerows = ddlPageRows.SelectedValue.Trim().ToString();
            this.gvOrderStatus.PageSize = Convert.ToInt16(pagerows);//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈
            if (ds.Tables[0].Rows.Count <= gvOrderStatus.PageSize)
                panel3.Visible = false;
            else
            {
                panel3.Visible = true;
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
                int count = ds.Tables[0].Rows.Count / gvOrderStatus.PageSize;
                if (count * gvOrderStatus.PageSize < ds.Tables[0].Rows.Count)
                    count = count + 1;
                lblTotal.Text = "共" + count + "頁   轉到";
            }
            this.gvOrderStatus.DataBind();
        }
        conn.Close();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvOrderStatus.PageIndex = 0;
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = true;
            lblgeshi2.Visible = false;
            btnExport.Visible = false;
            gvOrderStatus.Visible = false;
            panel3.Visible = false;
            return;
        }

        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = false;
            lblgeshi2.Visible = true;
            btnExport.Visible = false;
            gvOrderStatus.Visible = false;
            panel3.Visible = false;
            return;
        }
        lblgeshi1.Visible = false;
        lblgeshi2.Visible = false;
        bind();  
    }
    protected void btnExport_Click(object sender, EventArgs e)
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
    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {
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
}
