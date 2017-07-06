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

public partial class SysConfig_SysSetting : System.Web.UI.Page
{
    DbAccessing myDbAccessing = new DbAccessing();

    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            BindUserData();

            //操作權限管控開始------------
            BindOperPermission();
        }
    }

    public void BindOperPermission()
    {
        #region //操作權限管控開始------------
        string isPurview = "";
        string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
        isPurview = SecFunction.HasOperPermissionString(RoleCode, "Z04");
        string isAllPurview = SecFunction.HasAllOperPermissionString("Z04");

        //修改
        if (SecFunction.IsPurview(isAllPurview, "modify"))
        {
            try
            {
                this.btnCommit.Visible = SecFunction.IsPurview(isPurview, "modify");

            }
            catch { }
        }

        #endregion　//操作權限管控結束------------
    }

    private void BindUserData()
    {
        string sql_edit = "select * from usySysConfig where 1=1 ";
        DataTable dt = myDbAccessing.ExecuteSqlTable(sql_edit);
        foreach (DataRow dr in dt.Rows)
        {
            txtPageRow.Text = dr["PageRow"].ToString();
            txtDefaultPasswd.Text = dr["DefaultPasswd"].ToString();
        }
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        BindUserData();
        lblMessage.Text = "";
    }

    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        return_value += PubFunction.JustifyTextNotNull(txtPageRow, "每頁顯示行數");
        return_value += PubFunction.JustifyTextNotNull(txtDefaultPasswd, "系統默認密碼");
        if (return_value.Trim() == "")
        {
            return_value += PubFunction.JustifyNumTextNotNull(txtPageRow, 10, 1000, "每頁顯示行數", false);
        }
        if (return_value.Trim() != "")
        {
            return_value = "系統訊息：" + return_value;
        }
        return return_value;
    }


    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lblMessage.Text = CommitError().Trim();
            return;
        }
        string sqlUpdate = "update usySysConfig set DefaultPasswd='" + txtDefaultPasswd.Text.Trim().ToString()
            + "',PageRow='" + txtPageRow.Text.Trim().ToString()+ "'";

        if (myDbAccessing.ExecuteSql(sqlUpdate, "修改", "系統設置"))
        {
            lblMessage.Text = "系統訊息：<b><FONT color= Teal>" + MisSystem.OperationUpdateSuccessMessage +"　需要重新登錄後生效！" +"</FONT></b>";
        }
        else
        {
            lblMessage.Text = "系統訊息：<b><FONT color= Red>" + MisSystem.OperationUpdateFailureMessage + "</FONT></b>";
            //Response.Write(e.ToString());
        }	        
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }
}
