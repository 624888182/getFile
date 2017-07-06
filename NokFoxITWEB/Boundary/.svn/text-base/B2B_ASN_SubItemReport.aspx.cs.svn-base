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

public partial class Boundary_B2B_ASN_SubItemReport : System.Web.UI.Page
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
        sda.Fill(ds, "ASNSubItem");
        this.gvASNSubItem.DataSource = ds.Tables[0].DefaultView;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvASNSubItem.DataSource = ds;
            gvASNSubItem.DataBind();
            int columnCount = gvASNSubItem.Rows[0].Cells.Count;
            gvASNSubItem.Rows[0].Cells.Clear();
            gvASNSubItem.Rows[0].Cells.Add(new TableCell());
            gvASNSubItem.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvASNSubItem.Rows[0].Cells[0].Text = "No Records Found.";
            gvASNSubItem.Rows[0].Visible = true;
        }
        else
            this.gvASNSubItem.DataBind();

        conn.Close();
    }

    private string GetData()
    {
        string strBatchNo = Request.QueryString["BatchNo"].ToString();
        string strSendID = Request.QueryString["SendID"].ToString();
        string strReceID = Request.QueryString["ReceID"].ToString();
        string strLoadID = Request.QueryString["LoadID"].ToString();
        string strPalletID = Request.QueryString["PalletID"].ToString();
        string strItemNo = Request.QueryString["ItemNo"].ToString();

        string strsql = "select * from asnsubitem where batchno='" + strBatchNo + "' And sendid='" + strSendID
            + "' And receid='" + strReceID + "' And loadid='" + strLoadID + "' And pallet_id='" + strPalletID
            + "' And itmnum='" + strItemNo + "' order by len(subnum),subnum";

        return strsql;

    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问
    }
    protected void gvASNSubItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvASNSubItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvASNSubItem.PageIndex * gvASNSubItem.PageSize + e.Row.RowIndex + 1);

        }
    }
}
