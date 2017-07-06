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

public partial class SysConfig_UserManage2 : System.Web.UI.Page
{

    FIH.Security.db.Users myEntity = new FIH.Security.db.Users();
    FIH.Security.db.UsersInfo myEntityInfo = new FIH.Security.db.UsersInfo();


    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            string sqlBind = "select OperRoleGpCode ,OperRoleGpName from usyOperRoleGp ";
            PubFunction.dlstBound(ddlRole, sqlBind);
            gvList.Visible = true;
            gvList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
            //btnAdd.Attributes.Add("onclick", GetWinPageStr("UserAdd.aspx", "add", ""));

            //操作權限管控
            PubFunction.BindOperPermission(this, "Z01","");
            gvList.DataBind();
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        gvList.DataBind();
        gvList.Visible = true;
    }

    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreated(sender, e);
    }


    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label MyIndex = (Label)e.Row.FindControl("lblUserID");
            string StrID = Server.UrlEncode(MyIndex.Text.Trim().ToString());
            MyIndex.Attributes.Add("onclick", GetWinPageStr("UserAdd.aspx", "view", StrID));

            //lbtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";

            Label lblItem = (Label)e.Row.FindControl("lblItem");
            lblItem.Text = System.Convert.ToString((e.Row.RowIndex + 1 + gvList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));
        }
    }
    protected void btnReset_ServerClick(object sender, EventArgs e)
    {
        txtUserID.Text = txtUserName.Text =  ddlRole.SelectedValue = "";
    }

    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label MyIndex = (Label)gvList.Rows[e.RowIndex].FindControl("lblUserID");
        myEntityInfo.UserID = MyIndex.Text;
        myEntity.Delete(myEntityInfo);
        gvList.DataBind();

    }
    protected void dsOper_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        myEntityInfo.UserID = txtUserID.Text.Trim();
        myEntityInfo.UserName = txtUserName.Text.Trim();
        myEntityInfo.OperRoleGpCode = ddlRole.SelectedValue.ToString();
        e.InputParameters["users"] = myEntityInfo;
    }

    #region   得到链接文件
    private string GetWinPageStr(string winPage, string action, string StrID)
    {
        //return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=480,height=420,status=no,resizable=no,scrollbars=yes,top=220,left=350')";
        return "window.location.href='" + winPage + "?action=" + action + "&kerid=" + StrID + "'";
    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Response.Redirect("UserAdd.aspx?action=add");
    }
}
