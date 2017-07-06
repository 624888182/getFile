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
using System.IO;

public partial class Boundary_B2B_ASN_NEW : System.Web.UI.UserControl
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

    private void bind()
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
        sda.Fill(ds, "ASN");
        this.gvASN.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvASN.DataSource = ds;
            gvASN.DataBind();
            int columnCount = gvASN.Rows[0].Cells.Count;
            gvASN.Rows[0].Cells.Clear();
            gvASN.Rows[0].Cells.Add(new TableCell());
            gvASN.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvASN.Rows[0].Cells[0].Text = "No Records Found.";
            gvASN.Rows[0].Visible = true;
            btnExport.Visible = false;
            panel3.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
            this.gvASN.AllowPaging = true;
            string pagerows = ddlPageRows.SelectedValue.Trim().ToString();
            this.gvASN.PageSize = Convert.ToInt16(pagerows);//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈

            if (ds.Tables[0].Rows.Count <= gvASN.PageSize)
                panel3.Visible = false;
            else
            {
                panel3.Visible = true;
                if (this.gvASN.PageIndex == 0)
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                else
                {
                    this.btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (gvASN.PageIndex == gvASN.PageCount - 1)
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
                int now = gvASN.PageIndex + 1;
                lblNow.Text = "第" + now.ToString() + "頁 / ";
                lblTotal.Visible = true;
                txtNo.Text = now.ToString();
                int count = ds.Tables[0].Rows.Count / gvASN.PageSize;
                if (count * gvASN.PageSize < ds.Tables[0].Rows.Count)
                    count = count + 1;
                lblTotal.Text = "共" + count + "頁   轉到";
            }
            this.gvASN.DataBind();
        }
        conn.Close();
    }

    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {
        switch (((ImageButton)sender).CommandArgument.ToString())
        {
            case "First":
                gvASN.PageIndex = 0;
                break;
            case "Previous":
                if (gvASN.PageIndex >= 1)
                {
                    gvASN.PageIndex = gvASN.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                break;
            case "Next":
                if (gvASN.PageIndex < gvASN.PageCount - 1)
                {
                    gvASN.PageIndex = gvASN.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvASN.PageIndex < gvASN.PageCount - 1)
                    gvASN.PageIndex = gvASN.PageCount - 1;
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
                        if (index > 0 && index < gvASN.PageCount + 1)
                            gvASN.PageIndex = index - 1;
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

    private string GetData()
    {
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strSendFlag = ddlSendFlag.SelectedValue.Trim().ToUpper();
        string strAckStatus = ddlAckFlag.SelectedValue.Trim().ToUpper();
        string strPoNo = txtPoNo.Text.Trim();
        //string strIdocNo = txtIdocNo.Text.Trim();
        string strBatchNo = txtBatchNo.Text.Trim();
        string strMesgID = txtMesgid.Text.Trim();
        string strLoadID = txtLoadid.Text.Trim();
        string strPalletID = txtPalletid.Text.Trim();
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
        int PageRows = Convert.ToInt16(strPageRows);

        //" and t3.ordnum=? and t1.batchno=? and t1.mesgid=? and t1.loadid=? and t1.sendflag=? and t1.ackflag=? order by t1.lasteditdt desc"
        string strsql = "select distinct t1.*,t2.lasteditdt as asnackreceivedt   from dbo.asnmain t1(nolock) left join asnack t2(nolock) on t1.loadid=t2.loadid left join asnitem t3(nolock) on t1.pallet_id=t3.pallet_id where t1.sendflag<>'F' "
            + " and  t1.lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and t1.lasteditdt<=convert(varchar,'" + strEndDate + "',120)";

        //if (!strIdocNo.Equals(""))
        //{
        //    strIdocNo = Split(strIdocNo);
        //    if (strIdocNo.Equals("split error"))
        //        return "input error";
        //    else
        //        strsql += " and t1.idocno in (" + strIdocNo + ")";
        //}

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t3.ordnum in (" + strPoNo + ")";
        }

        if (!strBatchNo.Equals(""))
        {
            strBatchNo = Split(strBatchNo);
            if (strBatchNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.batchno in (" + strBatchNo + ")";
        }

        if (!strMesgID.Equals(""))
        {
            strMesgID = Split(strMesgID);
            if (strMesgID.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.mesgid in (" + strMesgID + ")";
        }

        if (!strLoadID.Equals(""))
        {
            strLoadID = Split(strLoadID);
            if (strLoadID.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.loadid in (" + strLoadID + ")";
        }


        if (!strSendFlag.Equals("ALL"))
        {
            strSendFlag = strSendFlag.Substring(0, 1);
            strsql += " and t1.sendflag ='" + strSendFlag + "'";
        }

        if (!strAckStatus.Equals("ALL"))
        {
            strAckStatus = strAckStatus.Substring(0, 1);
            strsql += " and t1.ackflag ='" + strAckStatus + "'";
        }

        strsql += " order by t1.lasteditdt " + strOrder;

        return strsql;
    }

    protected void gvASN_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvASN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvASN.PageIndex * gvASN.PageSize + e.Row.RowIndex + 1);

            e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[19].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[22].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

            LinkButton lbtloadid;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbtloadid = (LinkButton)e.Row.Cells[6].Controls[1];
                if (lbtloadid != null)
                {
                    if (lbtloadid.CommandName == "LoadID")
                        lbtloadid.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }
    }
    protected void gvASN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "LoadID")
        {
            string strBatchNo = ((Label)gvASN.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;
            string strSendID = ((Label)gvASN.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;
            string strReceID = ((Label)gvASN.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;
            string strLoadID = ((LinkButton)gvASN.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Controls[1]).Text;
            string strPalletID = ((Label)gvASN.Rows[Convert.ToInt32(e.CommandArgument)].Cells[14].Controls[1]).Text;

            string strScript = "<script language='jscript'>var res = window.open('./B2B_ASN_ItemReport.aspx?BatchNo=" + strBatchNo + "&SendID=" + strSendID
           + "&ReceID=" + strReceID + "&LoadID=" + strLoadID + "&PalletID=" + strPalletID + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvASN.PageIndex = 0;

        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = true;
            lblgeshi2.Visible = false;
            btnExport.Visible = false;
            gvASN.Visible = false;
            panel3.Visible = false;
            return;
        }

        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = false;
            lblgeshi2.Visible = true;
            btnExport.Visible = false;
            gvASN.Visible = false;
            panel3.Visible = false;
            return;
        }
        lblgeshi1.Visible = false;
        lblgeshi2.Visible = false;
        bind();  
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        try
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/ms-excel";
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_ASN_Report.xls");
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
            sda.Fill(ds, "ASN1");
            this.gvASN.DataSource = ds.Tables[0].DefaultView;

            this.gvASN.AllowPaging = false;
            this.gvASN.DataBind();

            DisableControls(gvASN);
            gvASN.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();

            // turn the paging on again 
            //gvPO.AllowPaging = true;
            //bind(); 
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
            return;
        }   
    }
}
