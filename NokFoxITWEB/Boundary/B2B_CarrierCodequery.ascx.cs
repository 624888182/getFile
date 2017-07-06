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
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OracleClient;
using DBAccess.EAI;
using System.IO;

public partial class Boundary_B2B_CarrierCodequery : System.Web.UI.UserControl
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
            bindmcid(); 
        }
    }

    private void bindmcid()
    {
        string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString; 
        OracleConnection conn = new OracleConnection(strConn);
        string strsql = "select distinct MCID from DELL_B2B_CARRIER_CODE where ORIGIN ='FTY'"; 
        OracleDataAdapter sda = new OracleDataAdapter(strsql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "MCID");
        ddlMCID.DataTextField = "MCID";
        ddlMCID.DataValueField = "MCID";
        ddlMCID.DataSource = ds.Tables[0].DefaultView;
        ddlMCID.DataBind();
        ddlMCID.Items.Insert(0, "ALL");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvCarrierCode.PageIndex = 0;        
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
        string strRegion = txtRegion.Text.Trim().ToUpper();
        string strCarrierCode = txtCarrierCode.Text.Trim().ToUpper();
        string strMCID = ddlMCID.SelectedValue.Trim().ToUpper();
        string strShipMode = ddlShipMode.SelectedValue.Trim().ToUpper(); 
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
        int PageRows = Convert.ToInt16(strPageRows);

        string strsql = "select * from dell_b2b_carrier_code where 1=1 ";
        if (!strRegion.Equals(""))
        {
            strRegion = Split(strRegion);
            if (strRegion.Equals("split error"))
                return "input error";
            else
                strsql += " and region in (" + strRegion + ")";
        }

        if (!strCarrierCode.Equals(""))
        {
            strCarrierCode = Split(strCarrierCode);
            if (strCarrierCode.Equals("split error"))
                return "input error";
            else
                strsql += " and scacid in (" + strCarrierCode + ")";
        }

        if (!strShipMode.Equals("ALL"))
        {
            strShipMode = strShipMode.Substring(0, 1);
            strsql += " and ship_model ='" + strShipMode + "'";
        }

        if (!strMCID.Equals("ALL"))
        {
            strsql += " and MCID= '" + strMCID + "'";
        }

        strsql += " order by CREATED_BY " + strOrder;

        return strsql;
    }

    private void bind()//数据绑定方法
    {
        string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString;
        OracleConnection conn = new OracleConnection(strConn);
        string strsql = GetData();
        if (strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        OracleDataAdapter sda = new OracleDataAdapter(strsql, conn);

        DataSet ds = new DataSet();
        sda.Fill(ds, "carriercode");
        this.gvCarrierCode.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvCarrierCode.DataSource = ds;
            gvCarrierCode.DataBind();
            int columnCount = gvCarrierCode.Rows[0].Cells.Count;
            gvCarrierCode.Rows[0].Cells.Clear();
            gvCarrierCode.Rows[0].Cells.Add(new TableCell());
            gvCarrierCode.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvCarrierCode.Rows[0].Cells[0].Text = "No Records Found.";
            gvCarrierCode.Rows[0].Visible = true;
            btnExport.Visible = false;
            panel3.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
            this.gvCarrierCode.AllowPaging = true;
            string pagerows = ddlPageRows.SelectedValue.Trim().ToString();
            this.gvCarrierCode.PageSize = Convert.ToInt16(pagerows);//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈

            if (ds.Tables[0].Rows.Count <= gvCarrierCode.PageSize)
                panel3.Visible = false;
            else
            {
                panel3.Visible = true;
                if (this.gvCarrierCode.PageIndex == 0)
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                else
                {
                    this.btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (gvCarrierCode.PageIndex == gvCarrierCode.PageCount - 1)
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
                int now = gvCarrierCode.PageIndex + 1;
                lblNow.Text = "第" + now.ToString() + "頁 / ";
                lblTotal.Visible = true;
                txtNo.Text = now.ToString();
                int count = ds.Tables[0].Rows.Count / gvCarrierCode.PageSize;
                if (count * gvCarrierCode.PageSize < ds.Tables[0].Rows.Count)
                    count = count + 1;
                lblTotal.Text = "共" + count + "頁   轉到";
            }
            this.gvCarrierCode.DataBind();
        }
        conn.Close();
    }

    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {
        //gvPO.PageIndex = Convert.ToInt32(((ImageButton)sender).CommandName) - 1; 
        switch (((ImageButton)sender).CommandArgument.ToString())
        {
            case "First":
                gvCarrierCode.PageIndex = 0;
                break;
            case "Previous":
                if (gvCarrierCode.PageIndex >= 1)
                {
                    gvCarrierCode.PageIndex = gvCarrierCode.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                break;
            case "Next":
                if (gvCarrierCode.PageIndex < gvCarrierCode.PageCount - 1)
                {
                    gvCarrierCode.PageIndex = gvCarrierCode.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvCarrierCode.PageIndex < gvCarrierCode.PageCount - 1)
                    gvCarrierCode.PageIndex = gvCarrierCode.PageCount - 1;
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
                        if (index > 0 && index < gvCarrierCode.PageCount + 1)
                            gvCarrierCode.PageIndex = index - 1;
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

    protected void gvCarrierCode_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    
    protected void gvCarrierCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        Label uplabel;
        Label rlabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvCarrierCode.PageIndex * gvCarrierCode.PageSize + e.Row.RowIndex + 1);
        }
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
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_CarrierCode.xls");
            this.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString;
            OracleConnection conn = new OracleConnection(strConn);
            string strsql = GetData();
            if (strsql.Equals("input error"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
                return;
            }
            OracleDataAdapter sda = new OracleDataAdapter(strsql, conn);

            DataSet ds = new DataSet();
            sda.Fill(ds, "carriercode");
            this.gvCarrierCode.DataSource = ds.Tables[0].DefaultView;


            this.gvCarrierCode.AllowPaging = false;
            this.gvCarrierCode.DataBind();

            DisableControls(gvCarrierCode);
            gvCarrierCode.RenderControl(hw);
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
