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

public partial class Boundary_B2B_InvoiceItem_Report : System.Web.UI.Page
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
        sda.Fill(ds, "InvoiceItem");
        this.gvInvoiceItem.DataSource = ds.Tables[0].DefaultView;

        this.gvInvoiceItem.DataBind();

        conn.Close();
    }

    private string GetData()
    {
        string strInvoiceno = Request.QueryString["InvoiceNo"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();

        string strsql = "select * from invoiceitem where invcnm='" + strInvoiceno + "' And Sendid='" + strSendid + "' And Receid='" + strReceid + "'";

        return strsql;
    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }
    protected void gvInvoiceItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvInvoiceItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvInvoiceItem.PageIndex * gvInvoiceItem.PageSize + e.Row.RowIndex + 1);

        }
    }
}
