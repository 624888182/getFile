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

public partial class Boundary_B2B_PO_DetailReport : System.Web.UI.Page
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
        sda.Fill(ds, "POITEMDetail");
        this.gvPODetail.DataSource = ds.Tables[0].DefaultView;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvPODetail.DataSource = ds;
            gvPODetail.DataBind();
            int columnCount = gvPODetail.Rows[0].Cells.Count;
            gvPODetail.Rows[0].Cells.Clear();
            gvPODetail.Rows[0].Cells.Add(new TableCell());
            gvPODetail.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvPODetail.Rows[0].Cells[0].Text = "No Records Found.";
            gvPODetail.Rows[0].Visible = true;
        }
        else
            this.gvPODetail.DataBind();

        conn.Close();
    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }

    private string GetData()
    {
        string strItemnum = Request.QueryString["Itemnum"].ToString();
        string strOrdnum = Request.QueryString["Ordnum"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strMsegid = Request.QueryString["Msegid"].ToString();

        string strsql = "select * from purchaseordersitemdetail where sendid='" + strSendid + "' And receid='" + strReceid + "' And mesgid='" + strMsegid + "' And ordnum='" + strOrdnum + "' And itmnum='" + strItemnum + "' order by len(subitm),subitm ";

        return strsql;

    }
    protected void gvPODetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvPODetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvPODetail.PageIndex * gvPODetail.PageSize + e.Row.RowIndex + 1);

        }
    }
}
