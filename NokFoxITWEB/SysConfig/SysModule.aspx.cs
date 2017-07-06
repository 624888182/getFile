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


public partial class SysConfig_SysModule2 : System.Web.UI.Page
{
    FIH.Security.db.SysModule myEntity = new SysModule() ;
    FIH.Security.db.SysModuleInfo myEntityInfo = new SysModuleInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;
        if (!IsPostBack)
        { 
            gvList.Visible = false;
            gvList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
            btnAdd.Attributes.Add("onclick", GetWinPageStr("SysModuleAdd.aspx", "add", ""));
            //操作權限管控
            PubFunction.BindOperPermission(this, "Z07","");
        }
      gvList.DataBind();
    }

    protected void btnReset_ServerClick(object sender, EventArgs e)
    {
        txtModuleCode.Text = txtModuleNameEn.Text = "";
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        gvList.Visible = true;
    }
   protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label MyIndex = (Label)e.Row.FindControl("lblModuleCode");
            string StrID = Server.UrlEncode(MyIndex.Text.Trim().ToString());

           // Label lbBuName = (Label)e.Row.FindControl("lbBuName");
            MyIndex.Font.Underline = true;
            MyIndex.Attributes.Add("onclick", GetWinPageStr("SysModuleAdd.aspx", "view", StrID));
            
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

            lbtnDelete.OnClientClick = "return confirm('" +   GetGlobalResourceObject("Message", "Delete_Sure").ToString()   + "')";

            ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
            ibtnEdit.Attributes.Add("onclick", GetWinPageStr("SysModuleAdd.aspx", "edit", StrID));

            Label myLabel = (Label)e.Row.FindControl("lblItem");
            myLabel.Text = System.Convert.ToString((e.Row.RowIndex + 1 + gvList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));
      
        }
    }
    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreatedNoMouseHand(sender, e);
    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label MyIndex = (Label)gvList.Rows[e.RowIndex].FindControl("lblModuleCode");
        myEntityInfo.ModuleCode = MyIndex.Text;
        myEntity.Delete(myEntityInfo);
        gvList.DataBind();
    }

   
    protected void dsOper_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        myEntityInfo.ModuleCode = txtModuleCode.Text.Trim();
        myEntityInfo.ModuleNameEn = txtModuleNameEn.Text.Trim();

        e.InputParameters["sysModule"] = myEntityInfo;
    }

#region   得到链接文件
    private string GetWinPageStr(string winPage, string action, string StrID)
    {
        return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=630,height=340,status=no,resizable=no,scrollbars=yes,top=200,left=200')";
    }
#endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}
