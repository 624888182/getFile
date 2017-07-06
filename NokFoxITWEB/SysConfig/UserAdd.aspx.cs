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

public partial class SysConfig_UserAdd2 : System.Web.UI.Page
{

    FIH.Security.db.Users myEntity = new FIH.Security.db.Users();
    FIH.Security.db.UsersInfo myEntityInfo = new FIH.Security.db.UsersInfo();
    string StrAction = "";
    string StrKeyId = "";
    DbAccessing CDAL = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {

        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
        StrAction = Server.UrlDecode(Request.QueryString["Action"]);
        if (!IsPostBack)
        {
            string sqlBind = "select OperRoleGpCode ,OperRoleGpName from usyOperRoleGp ";

            PubFunction.dlstBound(ddlRoleCode, sqlBind);

            if (StrAction == "edit")
            {
                textUnable(true);
                this.btnCommit.Visible = true;
            }
            if (StrAction == "view")
            {
                textUnable(false);
                this.btnCommit.Visible = false;
            }
            BindData(StrKeyId);
        }
        btnAddDept.Attributes.Add("onclick", "window.open('UserOperateDept.aspx?UserID=" + txtUserID.Text + "','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');");
    }

    #region   数据绑定
    public void BindData(string sCondition)
    {
        myEntityInfo = myEntity.getUsers(sCondition);
        txtUserID.Text = myEntityInfo.UserID;
        txtUserName.Text = myEntityInfo.UserName;
        txtMail.Text = myEntityInfo.Mail;
        txtTel.Text = myEntityInfo.Tel;
        txtRemark.Text = myEntityInfo.Remark;
        ddlRoleCode.SelectedValue = myEntityInfo.OperRoleGpCode;

        txtDeptNo.Text = myEntityInfo.DeptNo;
        txtPassWD.Text = myEntityInfo.PassWD;
        txtIsOnline.Text = myEntityInfo.IsOnLine.ToString();
        txtGradeCode.Text = myEntityInfo.GradeCode;
        txtGradeName.Text = myEntityInfo.GradeName;
        txtPositionCode.Text = myEntityInfo.PositionCode;
        txtPositionName.Text = myEntityInfo.PositionName;
        txtPositionSeries.Text = myEntityInfo.PositionSeries;
        txtPositionSeriesName.Text = myEntityInfo.PositionSeriesName;
        txtInCompanyDate.Text = myEntityInfo.InCompanyDate.ToString();
        txtStatus.Text = myEntityInfo.Status.ToString();

        //綁定人員負責部門的數據
        DataTable dtDept = CDAL.ExecuteSqlTable("select A.ID,B.DepartmentCode,B.DepartmentName from usyUserOperateDept A left join usyDepartment B on A.DeptID = B.DepartmentID where A.UserID = '" + myEntityInfo.UserID + "'");
        gvUserOperateDept.DataSource = dtDept;
        gvUserOperateDept.DataBind();
    }
    #endregion


    #region  able
    private void textUnable(bool isAble)
    {
        txtMail.Enabled = isAble;
        txtTel.Enabled = isAble;
        txtRemark.Enabled = isAble;
        ddlRoleCode.Enabled = isAble;


        System.Drawing.Color myColor = new Color();
        myColor = (isAble == true) ? Color.Red : Color.Black;

        txtMail.ForeColor = myColor;
        txtTel.ForeColor = myColor;
        txtRemark.ForeColor = myColor;
        ddlRoleCode.ForeColor = myColor;
    }
    #endregion


    #region   数据验证
    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        if (ddlRoleCode.SelectedValue.ToString() == "") { return_value += lblRoleCode.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }

        return return_value;
    }
    #endregion


    #region   清空
    private void clearText(object sender, System.EventArgs e)
    {
        this.txtTel.Text = "";
        this.txtMail.Text = "";
        this.txtRemark.Text = "";
        this.ddlRoleCode.SelectedValue = "";
    }
    #endregion

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            textUnable(false);
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = CommitError().Trim();
            return;
        }

        myEntityInfo.UserID = txtUserID.Text.Trim().ToString();
        myEntityInfo.UserName = txtUserName.Text.Trim().ToString();
        myEntityInfo.OperRoleGpCode = ddlRoleCode.SelectedValue.Trim();
        myEntityInfo.Mail = txtMail.Text.Trim().ToString();
        myEntityInfo.Tel = txtTel.Text.Trim().ToString();
        myEntityInfo.Remark = txtRemark.Text.Trim().ToString();

        myEntityInfo.DeptNo = txtDeptNo.Text;
        myEntityInfo.PassWD = txtPassWD.Text;
        myEntityInfo.IsOnLine = txtIsOnline.Text==""?false:true;
        myEntityInfo.GradeCode = txtGradeCode.Text;
        myEntityInfo.GradeName = txtGradeName.Text;
        myEntityInfo.PositionCode = txtPositionCode.Text;
        myEntityInfo.PositionName = txtPositionName.Text;
        myEntityInfo.PositionSeries = txtPositionSeries.Text;
        myEntityInfo.PositionSeriesName = txtPositionSeriesName.Text;

        if (txtInCompanyDate.Text != null && txtInCompanyDate.Text.Length>0)
        {
            myEntityInfo.InCompanyDate = Convert.ToDateTime(txtInCompanyDate.Text);
        }
        
        myEntityInfo.Status = Convert.ToInt32(txtStatus.Text==""?"0":txtStatus.Text);

        try
        {
            myEntity.Update(myEntityInfo);
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = GetGlobalResourceObject("Message", "Edit_Success").ToString();

            textUnable(false);
            btnEdit.Visible = true;
            btnCommit.Visible = false;
        }
        catch
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = GetGlobalResourceObject("Message", "Edit_Fail").ToString();
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserManage.aspx");
    }


    protected void ddlOrgNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string sqlBind = "select DepartmentID,DepartmentName from tbbase_Department where BuID='" + ddlOrgNo.SelectedValue.ToString() + "'";
        //PubFunction.dlstBound(ddlDept, sqlBind);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        textUnable(true);
        btnEdit.Visible = false;
        btnCommit.Visible = true;
        lblMessage.Text = "";
    }
    protected void gvUserOperateDept_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = gvUserOperateDept.DataKeys[e.RowIndex].Value.ToString();
        DbAccessing CDAL = new DbAccessing();
        try
        {
            string sql = "delete from usyUserOperateDept where ID = '" + ID + "'";
            CDAL.ExecuteSql(sql);
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
        }
        BindData(StrKeyId);

    }
    protected void gvUserOperateDept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
            ibtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";
        }
    }
    protected void btnSX_Click(object sender, EventArgs e)
    {
        BindData(StrKeyId);
    }
}
