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

public partial class SysConfig_UserUpdatePwd2 : System.Web.UI.Page
{
    DbAccessing myDbAccessing = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            try
            {
                this.lblUserID.Text = Session["loginUser"].ToString();
            }
            catch
            { }
        }
    }

    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        return_value += PubFunction.JustifyTextNotNull(this.txtOldPW, "舊密碼");
        return_value += PubFunction.JustifyTextNotNull(this.txtNewPW, " 新密碼");
        return_value += PubFunction.JustifyTextNotNull(this.txtNewPWagain, "新密碼(again)");
        if ((txtNewPW.Text.Trim()) != (txtNewPWagain.Text.Trim()))
        {
            return_value += "<b><FONT color= red>" + MisSystem.PasswordDifMessage + "</FONT></b>";
        }
        if (return_value.Trim() != "")
        {
            return_value = "系統提示：" + return_value;
        }
        return return_value;
    }


    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lblMessage.Text = CommitError().Trim();
            return;
        }

        string sql = "select * from tbrole_Users where UserID = '" + Session["loginUser"].ToString() + "' and  PassWD = '" + txtOldPW.Text.Trim() + "'";
        if (PubFunction.DataExist(sql) == false)
        {
            lblMessage.Text = "系統提示：" + "<b><FONT color= red>" + MisSystem.PasswordOldFailureMessage + "</FONT></b>";
            return;
        }

        string sqlUpdate = "update tbrole_Users set PassWD='" + this.txtNewPW.Text.Trim().ToString() + "' where UserID ='" + Session["loginUser"].ToString() + "'";
        if (myDbAccessing.ExecuteSql(sqlUpdate, "修改", "密碼修改"))
        {
            lblMessage.Text = "<b><FONT color= Teal>" + MisSystem.PasswordUpdateSuccessMessage + "</FONT></b>";
        }
        else { lblMessage.Text = "<b><FONT color= Red>" + MisSystem.PasswordUpdateFailureMessage + "</FONT></b>"; }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }
}
