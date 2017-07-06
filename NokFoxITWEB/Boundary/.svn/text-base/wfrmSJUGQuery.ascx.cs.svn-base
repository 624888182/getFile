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
using DBAccess.EAI;
public partial class Boundary_wfrmSJUGQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            binddata();
            Label2.Visible = false;
        }
    }
    private void MultiLanguage()
    {
        Button1.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label2.Visible = true;
        string sql = "SELECT * FROM SFC.CDMA_MILLAU_SJUG WHERE PPART LIKE '" + this.TextBox1.Text.Trim() + "%' OR APART LIKE '" + this.TextBox1.Text.Trim() + "%' OR SPART LIKE '" + this.TextBox1.Text.Trim() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt.DefaultView;
            this.GridView1.DataBind();
            this.Label2.Text = "總計：" + dt.Rows.Count + "條";
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('此料號SJUG信息尚未維護！！');</script>");
            return;
        }
    }
    private void binddata()
    {

        string strsql = "SELECT * FROM SFC.CDMA_MILLAU_SJUG ";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt1.DefaultView;
            this.GridView1.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('此料號SJUG信息尚未維護！！');</script>");
            return;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Label2.Visible = true;
        this.GridView1.PageIndex = e.NewPageIndex;
        binddata();
    }

}
