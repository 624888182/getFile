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
using System.Drawing;
using System.Data.SqlClient;


using FIH.Security.db;

public partial class SysConfig_OperateOper : System.Web.UI.Page
{
    FIH.Security.db.OperRoleGpInfo myDeptRoleGpInfo = new OperRoleGpInfo();
    FIH.Security.db.OperRoleGp myOperRoleGp = new OperRoleGp();

    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            gvList.Visible = false;
            gvList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
            btnAdd.Attributes.Add("onclick", GetWinPageStr("OperRoleGroupAdd.aspx", "add", ""));

            //操作權限管控
            PubFunction.BindOperPermission(this, "Z03","");
        }
        gvList.DataBind();
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        gvList.Visible = true;
    }

    #region   得到链接文件
    private string GetWinPageStr(string winPage, string action, string StrID)
    {
        return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=360,height=150,status=no,resizable=no,scrollbars=yes,top=220,left=350')";
    }
    #endregion
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label MyIndex = (Label)e.Row.FindControl("lblOpeRoleGroupCode");  
            string StrID = Server.UrlEncode(MyIndex.Text.Trim().ToString());
            MyIndex.Attributes.Add("onclick", GetWinPageStr("OperRoleGroupAdd.aspx", "view", StrID));
            Label myLabel = (Label)e.Row.FindControl("lblItem");
            myLabel.Text = System.Convert.ToString((e.Row.RowIndex + 1 + gvList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));

            ImageButton imgbtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
            imgbtnEdit.Attributes.Add("onclick", GetWinPageStr("OperRoleGroupAdd.aspx", "edit", StrID));

            //Label lbOperRoleGpName = (Label)e.Row.FindControl("lbOperRoleGpName");
            //lbOperRoleGpName.Font.Underline = true;
            //lbOperRoleGpName.Attributes.Add("onclick", GetWinPageStr("OperRoleGroupAdd.aspx", "view", StrID));

            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
            lbtnDelete.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "');"); //把提示信息加入资源文件调用
        }
    }
   

   
    protected void dsOper_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        myDeptRoleGpInfo.OperRoleGpCode = txtOperRoleGpCode.Text.Trim();
        myDeptRoleGpInfo.OperRoleGpName = txtOperRoleGpName.Text.Trim();
        e.InputParameters["operRoleGp"] = myDeptRoleGpInfo;
    }

    protected void gvList_RowDeleting( object sender, GridViewDeleteEventArgs e ) 
    {
        Label MyIndex = (Label)gvList.Rows[e.RowIndex].FindControl("lblOpeRoleGroupCode");
        myDeptRoleGpInfo.OperRoleGpCode = MyIndex.Text;
        myOperRoleGp.Delete(myDeptRoleGpInfo);
        gvList.DataBind();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtOperRoleGpCode.Text = txtOperRoleGpName.Text = "";
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreatedNoMouseHand(sender, e);
    }
   
}
