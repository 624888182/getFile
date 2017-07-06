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
    FIH.Security.db.OperateInfo tmpOperateInfo = new OperateInfo();
    FIH.Security.db.Operate tmpOperate = new Operate();
    OperateInfo oper = new OperateInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            gvList.Visible = false;
            gvList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
            btnAdd.Attributes.Add("onclick", GetWinPageStr("OperateOperAdd.aspx", "add", ""));   
            gvList.DataBind();

            //操作權限管控
            PubFunction.BindOperPermission(this, "Z02","");
        }
       
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        gvList.Visible = true;
        gvList.DataBind(); 
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
            Label MyIndex = (Label)e.Row.FindControl("lblOperationCodeID");
            string StrID = Server.UrlEncode(MyIndex.Text.Trim().ToString());
            MyIndex.Attributes.Add("onclick", GetWinPageStr("OperateOperAdd.aspx", "view", StrID));
            Label myLabel = (Label)e.Row.FindControl("lblItem");
            myLabel.Text = System.Convert.ToString((e.Row.RowIndex + 1 + gvList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));

            
            ImageButton imgbtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
            imgbtnEdit.Attributes.Add("onclick", GetWinPageStr("OperateOperAdd.aspx", "edit", StrID));
            
            //Label lbOperationName = (Label)e.Row.FindControl("lbOperationName");
            //lbOperationName.Font.Underline = true;
            //lbOperationName.Attributes.Add("onclick", GetWinPageStr("OperateOperAdd.aspx", "view", StrID));

            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
           // lbtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";
            lbtnDelete.Attributes.Add("onclick", "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "');"); //把提示信息加入资源文件调用
        }
    }
    protected void dsOper_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        oper.OperCode = txtOperCode.Text.Trim();
        oper.OperName = txtOperName.Text.Trim();
        e.InputParameters["operate"] = oper;
    }
    protected void gvList_RowDeleting( object sender, GridViewDeleteEventArgs e ) 
    {
        Label MyIndex = (Label)gvList.Rows[e.RowIndex].FindControl("lblOperationCodeID");
        tmpOperateInfo.OperCode = MyIndex.Text;
        tmpOperate.Delete(tmpOperateInfo);
        gvList.DataBind();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtOperCode.Text = txtOperName.Text = "";
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
