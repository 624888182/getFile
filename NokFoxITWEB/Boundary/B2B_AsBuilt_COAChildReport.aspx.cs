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

public partial class Boundary_B2B_AsBuilt_COAChildReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
            bindchild();//调用数据绑定方法
        }
    }

    private void bindchild()
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetDataChild();
        if (strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //SqlDataAdapter sda1 = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "Child");
        this.gvTioLabel.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvTioLabel.DataSource = ds;
            gvTioLabel.DataBind();
            int columnCount = gvTioLabel.Rows[0].Cells.Count;
            gvTioLabel.Rows[0].Cells.Clear();
            gvTioLabel.Rows[0].Cells.Add(new TableCell());
            gvTioLabel.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvTioLabel.Rows[0].Cells[0].Text = "No Records Found.";
            gvTioLabel.Rows[0].Visible = true;
        }
        else
        {
            this.gvTioLabel.DataBind();
        }

        conn.Close();
    }

    private string GetDataChild()
    {
        string strBatchno = Request.QueryString["Batchno"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strDocumentid = Request.QueryString["Documentid"].ToString();
        string strParentppid = Request.QueryString["Parentppid"].ToString();

        string strsql = "select * from asbuiltchild where batchno='" + strBatchno + "' And senderid='" + strSendid + "' And receiverid='" + strReceid
            + "' And documentid='" + strDocumentid + "' And parentppid='" + strParentppid + "'";

        return strsql;
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
        sda.Fill(ds, "COA");
        this.gvAsbuiltCOA.DataSource = ds.Tables[0].DefaultView;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvAsbuiltCOA.DataSource = ds;
            gvAsbuiltCOA.DataBind();
            int columnCount = gvAsbuiltCOA.Rows[0].Cells.Count;
            gvAsbuiltCOA.Rows[0].Cells.Clear();
            gvAsbuiltCOA.Rows[0].Cells.Add(new TableCell());
            gvAsbuiltCOA.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvAsbuiltCOA.Rows[0].Cells[0].Text = "No Records Found.";
            gvAsbuiltCOA.Rows[0].Visible = true;
        }
        else
        {
            this.gvAsbuiltCOA.DataBind();
        }
        conn.Close();
    }

    private string GetData()
    {
        string strBatchno = Request.QueryString["Batchno"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strDocumentid = Request.QueryString["Documentid"].ToString();
        string strParentppid = Request.QueryString["Parentppid"].ToString();

        string strsql = "select * from asbuiltcoa where batchno='" + strBatchno + "' And senderid='" + strSendid + "' And receiverid='" + strReceid
            + "' And documentid='" + strDocumentid + "' And parentppid='" + strParentppid + "'";

        return strsql;
    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }
    protected void gvAsbuiltCOA_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvAsbuiltCOA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvAsbuiltCOA.PageIndex * gvAsbuiltCOA.PageSize + e.Row.RowIndex + 1);

        }
    }
    protected void gvTioLabel_RowCreated(object sender, GridViewRowEventArgs e)
    {

        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvTioLabel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex1");
            olabel.Text = Convert.ToString(gvTioLabel.PageIndex * gvTioLabel.PageSize + e.Row.RowIndex + 1);

        }
    }
}
