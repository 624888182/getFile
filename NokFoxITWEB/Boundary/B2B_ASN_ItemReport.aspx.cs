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

public partial class Boundary_B2B_ASN_ItemReport : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            bind(); //调用数据绑定方法
        }
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
        sda.Fill(ds, "ASNITEM");
        this.gvASNItem.DataSource = ds.Tables[0].DefaultView;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvASNItem.DataSource = ds;
            gvASNItem.DataBind();
            int columnCount = gvASNItem.Rows[0].Cells.Count;
            gvASNItem.Rows[0].Cells.Clear();
            gvASNItem.Rows[0].Cells.Add(new TableCell());
            gvASNItem.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvASNItem.Rows[0].Cells[0].Text = "No Records Found.";
            gvASNItem.Rows[0].Visible = true;

        }
        else
            this.gvASNItem.DataBind();

        conn.Close();
    }

    private string GetData()
    {
        string strBatchNo = Request.QueryString["BatchNo"].ToString();
        string strSendID = Request.QueryString["SendID"].ToString();
        string strReceID = Request.QueryString["ReceID"].ToString();
        string strLoadID = Request.QueryString["LoadID"].ToString();
        string strPalletID = Request.QueryString["PalletID"].ToString(); 

        string strsql = "select * from asnitem where batchno='" + strBatchNo + "' And sendid='" + strSendID
            + "' And receid='" + strReceID + "' And loadid='" + strLoadID + "' And pallet_id='" + strPalletID
            + "' order by len(itmnum),itmnum ";

        return strsql;

    }

    protected void gvASNItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[4].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[10].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        LinkButton lbtItemNo;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtItemNo = (LinkButton)e.Row.Cells[6].Controls[1];
            if (lbtItemNo != null)
            {
                if (lbtItemNo.CommandName == "ItemNo")
                    lbtItemNo.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void gvASNItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvASNItem.PageIndex * gvASNItem.PageSize + e.Row.RowIndex + 1);
        }
    }

    protected void gvASNItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ItemNo")
        {
            string strBatchNo = ((Label)gvASNItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;
            string strSendID = ((Label)gvASNItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Controls[1]).Text;
            string strReceID = ((Label)gvASNItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
            string strLoadID = ((Label)gvASNItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;
            string strPalletID = ((Label)gvASNItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;
            string strItemNo = ((LinkButton)gvASNItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Controls[1]).Text;

            string strScript = "<script language='jscript'>var res = window.open('./B2B_ASN_SubItemReport.aspx?BatchNo=" + strBatchNo + "&SendID=" + strSendID
                + "&ReceID=" + strReceID + "&LoadID=" + strLoadID + "&PalletID=" + strPalletID + "&ItemNo=" + strItemNo + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问 
    }
}
