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

public partial class SysConfig_RoleGroupAdd : System.Web.UI.Page
{
    DbAccessing myDbAccessing = new DbAccessing();
    string StrAction = "";
    string StrKeyId = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;


        StrAction = Request.QueryString["action"];
        if (!IsPostBack)
        {
            if (StrAction == "edit")
            {
                lbHead.Text = " 權限組瀏覽";
                StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
                BindData(StrKeyId);
                btnDelete.Attributes.Add("onclick", "return confirm('" + MisSystem.OperationDeleteMessage + "');");
                this.btnCommit.Visible = btnReset.Visible = this.btnAdd.Visible = false;
                textUnable(false);


                //修改權限管控開始------------
                this.btnDelete.Visible = PubFunction.HasOperPermission("Z0403", "D0");
                this.btnModify.Visible = PubFunction.HasOperPermission("Z0403", "M0");
                //權限管控結束-------------
            }
            else
            {
                lbHead.Text = " 權限組新增";
                this.btnDelete.Visible = this.btnModify.Visible = this.btnAdd.Visible = false;
                textUnable(true);

                //新增權限管控開始------------
                this.btnCommit.Visible = PubFunction.HasOperPermission("Z0403", "A0");
                //權限管控結束-------------
            }
        }
    }

    private void BindData(string sCondition)
    {
        string sql_edit = "select * from R_OperRole where 1=1 and RoleCode='" + sCondition + "'";
        DataTable dt = myDbAccessing.ExecuteSqlTable(sql_edit);
        foreach (DataRow dr in dt.Rows)
        {
            tbRoleCode.Text = dr["RoleCode"].ToString();
            tbRoleCode.ReadOnly = true;
            tbRoleName.Text = dr["RoleName"].ToString();
        }
    }


    private void textUnable(bool isAble)
    {
        tbRoleCode.Enabled = tbRoleName.Enabled = isAble;
        System.Drawing.Color myColor = new Color();
        myColor = (isAble == true) ? Color.Red : Color.Black;
        lbRoleCode.ForeColor = lbRoleName.ForeColor = myColor;
        if (StrAction == "edit")
        {
            tbRoleCode.Enabled = false;
        }
    }


    private void clearText(object sender, System.EventArgs e)
    {
        this.tbRoleCode.Text = this.tbRoleName.Text = "";
    }

    private string CommitError()
    {
        string return_value = "";
        lbMessage.Text = "";
        return_value += PubFunction.JustifyTextNotNull(tbRoleCode, "權限組代碼");
        return_value += PubFunction.JustifyTextNotNull(tbRoleName, "權限組名稱");

        if (return_value.Trim() != "")
        {
            return_value = "系統訊息：" + return_value;
        }
        return return_value;
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        string StrAction = Request.QueryString["action"];
        string StrKeyId = StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
        if (StrAction != "edit")
        {
            this.clearText(sender, e);
            lbMessage.Text = "";
        }
        else
        {
            BindData(StrKeyId);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        textUnable(true);
        clearText(sender, e);
        this.lbMessage.Text = "";
        this.btnDelete.Visible = this.btnModify.Visible = this.btnAdd.Visible = false;
        this.btnCommit.Visible = this.btnReset.Visible = true;
        lbHead.Text = "權限組查看";
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        textUnable(true);
        this.lbMessage.Text = "";
        this.btnCommit.Visible = this.btnReset.Visible = true;
        this.btnDelete.Visible = this.btnModify.Visible = false;
        lbHead.Text = " 權限組修改";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string str_del = "delete from R_OperRole where RoleCode='" + this.tbRoleCode.Text.Trim() + "'";
        if (myDbAccessing.ExecuteSql(str_del, "刪除", "權限組操作"))
        {
            this.btnDelete.Visible = this.btnModify.Visible = false;
            lbMessage.Text = "系統訊息：<b><FONT color= Teal>" + MisSystem.OperationDeleteSuccessMessage + "</FONT></b>";
            Response.Write("<script>window.opener.document.formRoleGroup.submit();</script>");
        }
        else
        {
            lbMessage.Text = "系統訊息：<b><FONT color= red>" + MisSystem.OperationDeleteFailureMessage + "</FONT></b>";
        }		
    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lbMessage.Text = CommitError().Trim();
            return;
        }

        string StrAction = Request.QueryString["action"];
        if (StrAction == "edit")
        {
            string sqlUpdate = "update R_OperRole set RoleName='" + this.tbRoleName.Text.Trim().ToString() + "' where RoleCode ='" + tbRoleCode.Text.Trim().ToString() + "'";
            if (myDbAccessing.ExecuteSql(sqlUpdate, "修改", "權限組操作"))
            {
                textUnable(false);
                btnCommit.Visible = btnReset.Visible = false;
                btnDelete.Visible = btnModify.Visible = true;
                lbHead.Text = "權限組瀏覽";
                lbMessage.Text = "系統訊息：<b><FONT color= Teal>" + MisSystem.OperationUpdateSuccessMessage + "</FONT></b>";
                Response.Write("<script>window.opener.document.formRoleGroup.submit();</script>");
            }
        }
        else
        {
            string sqlExist = "select * from R_OperRole  where RoleCode='" + tbRoleCode.Text.Trim().ToString() + "'";
            if (PubFunction.DataExist(sqlExist))
            {
                Response.Write("<script language='javascript'>alert('" + MisSystem.OperationAddDataExistMessage.ToString() + "');</script>");
                return;
            }
            string sql = "Insert into R_OperRole(RoleCode, RoleName) Values('"
                + tbRoleCode.Text.Trim() + "','" + tbRoleName.Text.Trim() + "')";
            if (myDbAccessing.ExecuteSql(sql, "Add", "Role Group Operate"))
            {
                textUnable(false);
                this.btnCommit.Visible = this.btnReset.Visible = false;
                this.btnAdd.Visible = true;
                lbMessage.Text = "系統訊息：<b><FONT color= Teal>" + MisSystem.OperationAddSuccessMessage.ToString() + "</FONT></b>";
                Response.Write("<script>window.opener.document.formRoleGroup.submit();</script>");
            }
        }	
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");		
    }
}
