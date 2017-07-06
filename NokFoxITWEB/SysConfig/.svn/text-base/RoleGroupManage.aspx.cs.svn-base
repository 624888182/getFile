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

public partial class SysConfig_RoleGroupManage2 : System.Web.UI.Page
{
    DbAccessing db = new DbAccessing();
    private string SessionName = "Oper";

    private void BindGridView(string sqlBind)
    {
        DataTable tb = db.ExecuteSqlTable(sqlBind);
        GridViewList.DataSource = tb;
        GridViewList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
        GridViewList.DataBind();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            Session[SessionName] = "";
            btnAdd.Attributes.Add("onclick", "window.open('RoleGroupAdd.aspx?action=add','one','width=630,height=150,status=no,resizeable=no,scrollbars=yes,top=200,left=200')");

            //權限管控開始------------
            GridViewList.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
            this.btnFind.Visible = PubFunction.HasOperPermission("Z0403", "F0");
            this.btnReset.Visible = PubFunction.HasOperPermission("Z0403", "F0");
            this.btnAdd.Visible = PubFunction.HasOperPermission("Z0403", "A0");
            //權限管控結束------------
        }
        else
        {
            if ((Session[SessionName] != null) && (Session[SessionName].ToString() != ""))
            {
                BindGridView(Session[SessionName].ToString());
            }
        }
    }
    protected void btnReset_ServerClick(object sender, EventArgs e)
    {
        tbRoleCode.Text = tbRoleName.Text = "";
    }


    protected void btnFind_Click(object sender, EventArgs e)
    {
        string findSql = " select * from R_OperRole where 1=1";
        if (this.tbRoleCode.Text != "")
        {
            findSql += " and RoleCode like '%" + tbRoleCode.Text.Trim().ToString() + "%'";
        }
        if (this.tbRoleName.Text != "")
        {
            findSql += " and RoleName like '%" + tbRoleName.Text.Trim().ToString() + "%'";
        }
        Session[SessionName] = findSql;
        BindGridView(Session[SessionName].ToString());
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label MyIndex = (Label)e.Row.FindControl("lbRoleCode");
            string StrID = Server.UrlEncode(MyIndex.Text.Trim().ToString());
            e.Row.Attributes.Add("ondblclick", "window.open('RoleGroupAdd.aspx?action=edit&kerid=" + StrID + "','one','width=630,height=150,status=no,resizable=no,scrollbars=yes,top=200,left=200');");
            Label myLabel = (Label)e.Row.FindControl("lbItem");
            myLabel.Text = System.Convert.ToString((e.Row.RowIndex + 1 + GridViewList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));
        }
    }

    protected void GridViewList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreated(sender, e);
    }

    protected void GridViewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewList.PageIndex = e.NewPageIndex;
        BindGridView(Session[SessionName].ToString());
    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}
