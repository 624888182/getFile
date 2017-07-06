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

public partial class Boundary_B2B_AsBuilt_ParentReport : System.Web.UI.Page
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
        sda.Fill(ds, "POITEM");
        this.gvAsbuiltParent.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvAsbuiltParent.DataSource = ds;
            gvAsbuiltParent.DataBind();
            int columnCount = gvAsbuiltParent.Rows[0].Cells.Count;
            gvAsbuiltParent.Rows[0].Cells.Clear();
            gvAsbuiltParent.Rows[0].Cells.Add(new TableCell());
            gvAsbuiltParent.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvAsbuiltParent.Rows[0].Cells[0].Text = "No Records Found.";
            gvAsbuiltParent.Rows[0].Visible = true;
        }
        else
        {
            this.gvAsbuiltParent.DataBind();
        }
        conn.Close();
    }

    private string GetData()
    {
        string strBatchno = Request.QueryString["Batchno"].ToString();
        string strSendid = Request.QueryString["Sendid"].ToString();
        string strReceid = Request.QueryString["Receid"].ToString();
        string strDocumentid = Request.QueryString["Documentid"].ToString();

        string strsql = "select * from asbuiltparent where batchno='" + strBatchno + "' And senderid='" + strSendid + "' And receiverid='" + strReceid + "' And documentid='" + strDocumentid + "'";

        return strsql;
    }

    protected void gvAsbuiltParent_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        LinkButton lbtParentPPID;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtParentPPID = (LinkButton)e.Row.Cells[5].Controls[1];
            if (lbtParentPPID != null)
            {
                if (lbtParentPPID.CommandName == "ParentPPID")
                    lbtParentPPID.CommandArgument = e.Row.RowIndex.ToString();

            }
        }
    }
    protected void gvAsbuiltParent_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvAsbuiltParent.PageIndex * gvAsbuiltParent.PageSize + e.Row.RowIndex + 1);

        }

       
    }
    protected void gvAsbuiltParent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ParentPPID")
        {
            string strBatchno = ((Label)gvAsbuiltParent.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;
            string strSendid = ((Label)gvAsbuiltParent.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Controls[1]).Text;
            string strReceid = ((Label)gvAsbuiltParent.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
            string strDocumentid = ((Label)gvAsbuiltParent.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;
            string strParentppid = ((LinkButton)gvAsbuiltParent.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;

            string strScript = "<script language='jscript'>var res = window.open('./B2B_AsBuilt_COAChildReport.aspx?Batchno=" + strBatchno + "&Sendid=" + strSendid
                + "&Receid=" + strReceid + "&Documentid=" + strDocumentid + "&Parentppid=" + strParentppid + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }
}
