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

public partial class Boundary_B2B_AsBuilt_NEW : System.Web.UI.UserControl
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
        sda.Fill(ds, "AsBuiltMain");
        this.gvAsBuilt.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvAsBuilt.DataSource = ds;
            gvAsBuilt.DataBind();
            int columnCount = gvAsBuilt.Rows[0].Cells.Count;
            gvAsBuilt.Rows[0].Cells.Clear();
            gvAsBuilt.Rows[0].Cells.Add(new TableCell());
            gvAsBuilt.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvAsBuilt.Rows[0].Cells[0].Text = "No Records Found.";
            gvAsBuilt.Rows[0].Visible = true;
            //btnAll.Visible = false;
            //lblAll.Visible = false;
            //lblSingle.Visible = false;
            //btnSingle.Visible = false;
            panel3.Visible = false;
            btnExport.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
            this.gvAsBuilt.AllowPaging = true;
            string pagerows = ddlPageRows.SelectedValue.Trim().ToString();
            this.gvAsBuilt.PageSize = Convert.ToInt16(pagerows);

            if (ds.Tables[0].Rows.Count <= gvAsBuilt.PageSize)
                panel3.Visible = false;
            else
            {
                panel3.Visible = true;
                if (this.gvAsBuilt.PageIndex == 0)
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                else
                {
                    this.btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (gvAsBuilt.PageIndex == gvAsBuilt.PageCount - 1)
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
                int now = gvAsBuilt.PageIndex + 1;
                lblNow.Text = "第" + now.ToString() + "頁 / ";
                lblTotal.Visible = true;
                txtNo.Text = now.ToString(); 
                int count = ds.Tables[0].Rows.Count / gvAsBuilt.PageSize;
                if (count * gvAsBuilt.PageSize < ds.Tables[0].Rows.Count)
                    count = count + 1;
                lblTotal.Text = "共" + count + "頁   轉到";
            }
            this.gvAsBuilt.DataBind();
        }
        conn.Close();
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
        string strPoNo = txtPoNo.Text.Trim();
        string strBatchNo = txtBatcNo.Text.Trim();
        string strMesgID = txtMesgID.Text.Trim();  
        string strSendFlag =ddlSendFlag.SelectedValue.Trim().ToUpper();
        string strValidateFlag = ddlValidateFlag.SelectedValue.Trim().ToUpper();
        string strOrder = ddlOrder.SelectedValue.Trim().ToUpper();
        string strShipDest = txtShipDest.Text.Trim();
        string strAckStatus = ddlACKStatus.SelectedValue.Trim().ToUpper();

        //"select t1.* from asbuiltmain t1(nolock) left join asbuiltack t2 (nolock) on t1.documentid=t2.documentid left join asbuiltparent t3(nolock) on t1.documentid=t3.documentid where t1.sendflag<>'F' and  t1.lasteditdt>=? and t1.lasteditdt<=? and t3.ordernumber=? and t1.batchno=? and t1.documentid=? and t1.sendflag=? and t2.ack_action=? order by t1.lasteditdt desc"
        string strsql = "select distinct t1.* from asbuiltmain t1(nolock) left join asbuiltparent t2(nolock) on t1.documentid=t2.documentid where t1.sendflag<>'F'" +
            " and  t1.lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and t1.lasteditdt<=convert(varchar,'" + strEndDate + "',120)";

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t2.ordernumber in (" + strPoNo + ")";
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
                strsql += " and t1.documentid in (" + strMesgID + ")";
        }

        if (!strShipDest.Equals(""))
        {
            strShipDest = Split(strShipDest);
            if (strShipDest.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.shipdest in (" + strShipDest + ")";
        }


        if (!strSendFlag.Equals("ALL"))
        {
            strsql += " and t1.sendflag ='" + strSendFlag + "'";
        }

        if (!strValidateFlag.Equals("ALL"))
        {
            strsql += " and t1.validateflag ='" + strValidateFlag + "'";
        }

        if (!strAckStatus.Equals("ALL"))
        {
            strsql += " and t1.ackstatus ='" + strAckStatus + "'";
        }


        strsql += " order by t1.lasteditdt " + strOrder;

        return strsql;
    } 


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvAsBuilt.PageIndex = 0;
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
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_AsBuilt_Report.xls");
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
            this.gvAsBuilt.DataSource = ds.Tables[0].DefaultView;

            this.gvAsBuilt.AllowPaging = false;
            this.gvAsBuilt.DataBind();
            DisableControls(gvAsBuilt);
            gvAsBuilt.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();

            // turn the paging on again 
            gvAsBuilt.AllowPaging = true;
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
                gvAsBuilt.PageIndex = 0;
                break;
            case "Previous":
                if (gvAsBuilt.PageIndex >= 1)
                {
                    gvAsBuilt.PageIndex = gvAsBuilt.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                break;
            case "Next":
                if (gvAsBuilt.PageIndex < gvAsBuilt.PageCount - 1)
                {
                    gvAsBuilt.PageIndex = gvAsBuilt.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvAsBuilt.PageIndex < gvAsBuilt.PageCount - 1)
                    gvAsBuilt.PageIndex = gvAsBuilt.PageCount - 1;
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
                        if (index > 0 && index < gvAsBuilt.PageCount + 1)
                            gvAsBuilt.PageIndex = index - 1;
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
    protected void gvAsBuilt_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[4].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[10].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        LinkButton lbtnDocumentid;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtnDocumentid = (LinkButton)e.Row.Cells[4].Controls[1];
            if (lbtnDocumentid != null)
            {
                if (lbtnDocumentid.CommandName == "Documentid")
                    lbtnDocumentid.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
    protected void gvAsBuilt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvAsBuilt.PageIndex * gvAsBuilt.PageSize + e.Row.RowIndex + 1);
        }
    }
    protected void gvAsBuilt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Documentid")
        {
            string strBatchno = ((Label)gvAsBuilt.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;
            string strSendid = ((Label)gvAsBuilt.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Controls[1]).Text;
            string strReceid = ((Label)gvAsBuilt.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
            string strDocumentid = ((LinkButton)gvAsBuilt.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text; 

            string strScript = "<script language='jscript'>var res = window.open('./B2B_AsBuilt_ParentReport.aspx?Batchno=" + strBatchno + "&Sendid=" + strSendid
                + "&Receid=" + strReceid + "&Documentid=" + strDocumentid + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
}
