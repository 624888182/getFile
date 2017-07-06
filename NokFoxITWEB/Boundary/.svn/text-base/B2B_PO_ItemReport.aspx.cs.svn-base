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

public partial class Boundary_B2B_PO_ItemReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strPono = Request.QueryString["Pono"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strMsegid = Request.QueryString["Msegid"].ToString();

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
        sda.Fill(ds, "POITEM");
        this.gvPOItem.DataSource = ds.Tables[0].DefaultView;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvPOItem.DataSource = ds;
            gvPOItem.DataBind();
            int columnCount = gvPOItem.Rows[0].Cells.Count;
            gvPOItem.Rows[0].Cells.Clear();
            gvPOItem.Rows[0].Cells.Add(new TableCell());
            gvPOItem.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvPOItem.Rows[0].Cells[0].Text = "No Records Found.";
            gvPOItem.Rows[0].Visible = true;

        }
        else
            this.gvPOItem.DataBind();

        conn.Close();
    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }

    private string GetData()
    {
        string strPono = Request.QueryString["Pono"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strMsegid = Request.QueryString["Msegid"].ToString();

        string strsql = "select * from purchaseordersitem where sendid='" + strSendid + "' And receid='" + strReceid + "' And mesgid='" + strMsegid + "' And ordnum='" + strPono + "' order by len(itmnum),itmnum ";
        
        return strsql;
    }

    protected void gvPOItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        LinkButton lbtItemnum;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtItemnum = (LinkButton)e.Row.Cells[5].Controls[1];
            if (lbtItemnum != null)
            {
                if (lbtItemnum.CommandName == "ItemNum")
                    lbtItemnum.CommandArgument = e.Row.RowIndex.ToString();

            }
        }
    }
    protected void gvPOItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvPOItem.PageIndex * gvPOItem.PageSize + e.Row.RowIndex + 1);

        }
    }
    protected void gvPOItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ItemNum")
        {
            string strItemnum = ((LinkButton)gvPOItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;
            string strSendid = ((Label)gvPOItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;
            string strReceid = ((Label)gvPOItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Controls[1]).Text;
            string strMsegid = ((Label)gvPOItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
            string strOrdnum = ((Label)gvPOItem.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;

            string strScript = "<script language='jscript'>var res = window.open('./B2B_PO_DetailReport.aspx?Sendid=" + strSendid + "&Receid=" + strReceid
                + "&Msegid=" + strMsegid + "&Ordnum=" + strOrdnum + "&Itemnum=" + strItemnum + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
}
