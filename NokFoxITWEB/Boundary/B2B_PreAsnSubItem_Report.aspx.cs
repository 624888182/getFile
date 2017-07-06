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

public partial class Boundary_B2B_PreAsnSubItem_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        sda.Fill(ds, "PreAsnItem");
        this.gvPreAsnSubItem.DataSource = ds.Tables[0].DefaultView;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvPreAsnSubItem.DataSource = ds;
            gvPreAsnSubItem.DataBind();
            int columnCount = gvPreAsnSubItem.Rows[0].Cells.Count;
            gvPreAsnSubItem.Rows[0].Cells.Clear();
            gvPreAsnSubItem.Rows[0].Cells.Add(new TableCell());
            gvPreAsnSubItem.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvPreAsnSubItem.Rows[0].Cells[0].Text = "No Records Found.";
            gvPreAsnSubItem.Rows[0].Visible = true;
        }
        else
        {
            this.gvPreAsnSubItem.DataBind();
        }

        conn.Close();
    }

    private string GetData()
    {
        string strBatchno = Request.QueryString["Batchno"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strLoadid = Request.QueryString["Loadid"].ToString();
        string strPalletid = Request.QueryString["Palletid"].ToString();
        string strItemno = Request.QueryString["Itemno"].ToString();

        string strsql = "select * from preasnsubitem where Batchno='" + strBatchno + "' And Sendid='" + strSendid + "' And Receid='" + strReceid + "' And Loadid='" + strLoadid + "' And pallet_id='" + strPalletid + "' And itmnum='" + strItemno + "' order by len(subnum),subnum";

        return strsql;
    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }
    protected void gvPreAsnSubItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvPreAsnSubItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvPreAsnSubItem.PageIndex * gvPreAsnSubItem.PageSize + e.Row.RowIndex + 1);

        }
    }
}
