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

using SQLHelperA;
using System.Globalization;
using System.Drawing;
using FIH.Security.db;

public partial class DepRoleDistribute : System.Web.UI.Page
{

    FIH.Security.db.DeptRoleGp myEntity = new DeptRoleGp();
    FIH.Security.db.DeptRoleGpInfo myEntityInfo = new DeptRoleGpInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            
            GridViewList.Visible = false;
            GridViewList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
            btnAdd.Attributes.Add("onclick", GetWinPageStr("DepRoleDistributeAdd.aspx", "add", ""));

            //操作權限管控
            PubFunction.BindOperPermission(this, "Z06","");
        }
      GridViewList.DataBind();
    }

    protected void btnReset_ServerClick(object sender, EventArgs e)
    {
        tbDeptRoleGpCode.Text = tbDeptRoleGpName.Text = "";
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        GridViewList.Visible = true;
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label MyIndex = (Label)e.Row.FindControl("lbDeptRoleGpCode");
            string StrID = Server.UrlEncode(MyIndex.Text.Trim().ToString());

           // Label lbBuName = (Label)e.Row.FindControl("lbBuName");
            MyIndex.Font.Underline = true;
            MyIndex.Attributes.Add("onclick", GetWinPageStr("DepRoleDistributeAdd.aspx", "view", StrID));
            
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

            lbtnDelete.OnClientClick = "return confirm('" +   GetGlobalResourceObject("Message", "Delete_Sure").ToString()   + "')";

            ImageButton imgbtnEdit = (ImageButton)e.Row.FindControl("imgbtnEdit");
            imgbtnEdit.Attributes.Add("onclick", GetWinPageStr("DepRoleDistributeAdd.aspx", "edit", StrID));

            Label myLabel = (Label)e.Row.FindControl("lbItem");
            myLabel.Text = System.Convert.ToString((e.Row.RowIndex + 1 + GridViewList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));
      
        }
    }
    protected void GridViewList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreatedNoMouseHand(sender, e);
    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    protected void GridViewList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label MyIndex = (Label)GridViewList.Rows[e.RowIndex].FindControl("lbDeptRoleGpCode");
        myEntityInfo.DeptRoleGpCode = MyIndex.Text;
        myEntity.Delete(myEntityInfo);
        GridViewList.DataBind();
    }

   
    protected void ODSourceOper_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        myEntityInfo.DeptRoleGpCode = tbDeptRoleGpCode.Text.Trim();
        myEntityInfo.DeptRoleGpName = tbDeptRoleGpName.Text.Trim();

        e.InputParameters["deptRoleGp"] = myEntityInfo;
    }

#region   得到链接文件
    private string GetWinPageStr(string winPage, string action, string StrID)
    {
        return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=350,height=380,status=no,resizable=no,scrollbars=yes,top=220,left=350')";
    }
#endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}
