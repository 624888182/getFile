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
using FIH.Security.db;


public partial class SysConfig_OperateOperAdd : System.Web.UI.Page
{

    FIH.Security.db.OperRoleGpInfo myDeptRoleGpInfo = new OperRoleGpInfo();
    FIH.Security.db.OperRoleGp myOperRoleGp = new OperRoleGp();
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
                StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
                BindData(StrKeyId);                
                textUnable(true);
                txtOperRoleGpCode.Enabled = false;
            }
            if (StrAction == "view")
            {
                StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
                BindData(StrKeyId);
                textUnable(false);
                btnCommit.Visible = false;
            }
        } 
    }


    private void textUnable(bool isAble)
    {
        this.txtOperRoleGpCode.Enabled = this.txtOperRoleGpName.Enabled = isAble;
        System.Drawing.Color myColor = new Color();
        myColor = (isAble == true) ? Color.Red : Color.Black;
        this.lblOperRoleGpCode.ForeColor = this.lblOperRoleGpName.ForeColor = myColor;
    }


    private void BindData(string sCondition)
    {
        myDeptRoleGpInfo = myOperRoleGp.getOperRoleGp(sCondition);
        txtOperRoleGpCode.Text = myDeptRoleGpInfo.OperRoleGpCode;
        txtOperRoleGpName.Text = myDeptRoleGpInfo.OperRoleGpName;
    }

    private void clearText()
    {
        this.txtOperRoleGpCode.Text = this.txtOperRoleGpName.Text = "";
    }

    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        //判斷是否為空值
        if (this.txtOperRoleGpCode.Text.Trim() == "")
        {
            return_value += lblOperRoleGpCode.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        if (this.txtOperRoleGpName.Text.Trim() == "")
        {
            return_value += lblOperRoleGpName.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        return return_value;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lblMessage.Text = GetGlobalResourceObject("Message", "Msg_Error").ToString() + CommitError().Trim();
            return;
        }
        OperRoleGpInfo myDeptRoleGpInfo = new OperRoleGpInfo();
        myDeptRoleGpInfo.OperRoleGpCode = txtOperRoleGpCode.Text.Trim();
        myDeptRoleGpInfo.OperRoleGpName = txtOperRoleGpName.Text.Trim();

        //判斷資料是否存在
        if (myOperRoleGp.isExist(myDeptRoleGpInfo) == true)
        {
            lblMessage.Text = GetGlobalResourceObject("Message", "Save_DataExist").ToString();
            return; 
        }
        
        string StrAction = Request.QueryString["action"];
        //修改
        if (StrAction == "edit")
        {
            try
            {
                int rowNum = myOperRoleGp.Update(myDeptRoleGpInfo);
                lblMessage.Text = GetGlobalResourceObject("Message", "Edit_Success").ToString();
            }
            catch
            {
                lblMessage.Text = GetGlobalResourceObject("Message", "Edit_Fail").ToString();
                return;
            }
        }
        //新增
        else if (StrAction == "add")
        {
            try
            {
                int rowNum = myOperRoleGp.Insert(myDeptRoleGpInfo);
                lblMessage.Text = GetGlobalResourceObject("Message", "Add_Success").ToString();
            }
            catch
            {
                lblMessage.Text = GetGlobalResourceObject("Message", "Add_Fail").ToString();
                return; 
            }            
            clearText();           
        }
        Response.Write("<script>window.opener.document.formOperate.submit();</script>");

    }
   

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");		
    }
   
}
