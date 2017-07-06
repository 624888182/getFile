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

public partial class Boundary_B2B_Label_DetailReport : System.Web.UI.Page
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
        sda.Fill(ds, "LabelDetail");
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.gvLabelDetail.DataSource = ds.Tables[0].DefaultView;
            this.gvLabelDetail.DataBind();
        }
        else
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvLabelDetail.DataSource = ds;
            gvLabelDetail.DataBind();
            int columnCount = gvLabelDetail.Rows[0].Cells.Count;
            gvLabelDetail.Rows[0].Cells.Clear();
            gvLabelDetail.Rows[0].Cells.Add(new TableCell());
            gvLabelDetail.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvLabelDetail.Rows[0].Cells[0].Text = "No Records Found.";
            gvLabelDetail.Rows[0].Visible = true;
        }
        conn.Close();
    }

    private string GetData()
    {
        string strMsegid = Request.QueryString["Msegid"].ToString();
        string strMsegtype = Request.QueryString["Msegtype"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strOrdno = Request.QueryString["Ordno"].ToString();
        string strBuid = Request.QueryString["Buid"].ToString();

        string strsql = "select * from shipsyslabel where mesgid='" + strMsegid + "' And mesgtyp='" + strMsegtype + "' And sendid='" + strSendid
            + "' And receid='" + strReceid + "' And ordnum='" + strOrdno + "' And buid='" + strBuid + "'";

        return strsql;
    }

    protected void gvLabelDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvLabelDetail.PageIndex * gvLabelDetail.PageSize + e.Row.RowIndex + 1);

        }
    }

    protected void gvLabelDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }
}
