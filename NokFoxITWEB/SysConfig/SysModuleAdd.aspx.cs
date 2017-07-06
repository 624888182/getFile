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

using SQLHelperA;
using System.Globalization;
using FIH.Security.db;



public partial class SysConfig_SysModuleAdd2 : System.Web.UI.Page
{

    FIH.Security.db.SysModule myEntity = new SysModule();
    FIH.Security.db.SysModuleInfo myEntityInfo = new SysModuleInfo();
    string StrAction = "";     string StrKeyId = "";

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
                BindData(StrKeyId);   textUnable(true);
            }
            if (StrAction == "view")
            {
                StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
                BindData(StrKeyId); textUnable(false);
                btnCommit.Visible = false;
            }
            if (StrAction == "add")
            {
               textUnable(true);
            }
        }
    }



    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lblMessage.Text = CommitError().Trim();
            return;
        }
        
        string StrAction = Request.QueryString["action"];

        myEntityInfo.ModuleCode = txtModuleCode.Text.Trim().ToString();
        myEntityInfo.ModuleNameCn = txtModuleNameCn.Text.Trim().ToString();
        myEntityInfo.ModuleNameEn = txtModuleNameEn.Text.Trim().ToString();
        myEntityInfo.ParentModuleCode = txtParentModuleCode.Text.Trim().ToString();
        myEntityInfo.OperCodeGroup = txtOperCodeGroup.Text.Trim().ToString();
        myEntityInfo.URL = txtURL.Text.Trim().ToString();
        myEntityInfo.SysName = txtSysName.Text.Trim().ToString();
        if (chkIsOperModule.Checked == true)
        {
            myEntityInfo.IsOperModule = "Y";

        }
        else { myEntityInfo.IsOperModule = "N"; }
        if (chkIsRole.Checked == true)
        {
            myEntityInfo.IsRole = "Y";

        }
        else { myEntityInfo.IsRole = "N"; }

        if (StrAction == "edit")
        {
            //myEntityInfo.ModiID = Session["loginUser"].ToString();
            //myEntityInfo.ModiDate = DateTime.Now;

            myEntity.Update(myEntityInfo);
            Response.Write("<script>window.opener.document.formBu.submit();</script>");
            lblMessage.Text = GetGlobalResourceObject("Message", "Edit_Success").ToString();
            
        }
        else if (StrAction == "add")
        {
            try
            {
                //myEntityInfo.CreaterID = Session["loginUser"].ToString();
                //myEntityInfo.CreaterDate = DateTime.Now;

                myEntity.Insert(myEntityInfo);
                clearText(sender,e);
                Response.Write("<script>window.opener.document.formBu.submit();</script>");
                lblMessage.Text = GetGlobalResourceObject("Message", "Add_Success").ToString();
                //btnCommit.Visible = false; textUnable(false);
  
            }
            catch 
            {
                lblMessage.Text = GetGlobalResourceObject("Message", "Add_Fail").ToString();
            }
     }


    }
    protected void btnExist_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }

    #region  失效设置
    private void textUnable(bool isAble)
    {
        txtModuleCode.Enabled = txtModuleNameCn.Enabled = txtModuleNameEn.Enabled = txtParentModuleCode.Enabled = txtOperCodeGroup.Enabled = isAble;
        txtURL.Enabled = txtSysName.Enabled = chkIsOperModule.Enabled = chkIsRole.Enabled = isAble;

        System.Drawing.Color myColor = new Color();
        myColor = (isAble == true) ? Color.Red : Color.Black;
        lblModuleCode.ForeColor = lblModuleNameCn.ForeColor = lblModuleNameEn.ForeColor = lblParentModuleCode.ForeColor = lblOperCodeGroup.ForeColor = myColor;
        string StrAction = Request.QueryString["action"];
        if (StrAction == "edit")
        {
            txtModuleCode.Enabled = false;
        }
    }
    #endregion

    #region   清除 text
    private void clearText(object sender, System.EventArgs e)
    {
        txtModuleCode.Text = txtModuleNameCn.Text = txtModuleNameEn.Text = txtParentModuleCode.Text = txtOperCodeGroup.Text = txtURL.Text = txtSysName.Text =  "";
       
        chkIsRole.Checked = chkIsOperModule.Checked = false;
    }

    #endregion

    #region   数据绑定
    private void BindData(string sCondition)
    {
        myEntityInfo = myEntity.getSysModule(sCondition);
        txtModuleCode.Text = myEntityInfo.ModuleCode;
        txtModuleNameCn.Text = myEntityInfo.ModuleNameCn;
        txtModuleNameEn.Text = myEntityInfo.ModuleNameEn;
        txtParentModuleCode.Text = myEntityInfo.ParentModuleCode;
        txtOperCodeGroup.Text = myEntityInfo.OperCodeGroup;
        txtURL.Text = myEntityInfo.URL;
        txtSysName.Text = myEntityInfo.SysName;
        if (myEntityInfo.IsOperModule == "Y")
        {
            chkIsOperModule.Checked = true;
        }
        else
        {
            chkIsOperModule.Checked = false;
        }
        if (myEntityInfo.IsRole == "Y")
        {
            chkIsRole.Checked = true;
        }
        else
        {
            chkIsRole.Checked = false;
        }
    }
    #endregion

    #region   数据验证
    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        if (txtModuleCode.Text.Trim().ToString() == "") { return_value += lblModuleCode.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }
        if (txtModuleNameCn.Text.Trim().ToString() == "") { return_value += lblModuleNameCn.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }
        if (txtModuleNameEn.Text.Trim().ToString() == "") { return_value += lblModuleNameEn.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }
        if (txtParentModuleCode.Text.Trim().ToString() == "") { return_value += lblParentModuleCode.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }
        if (txtOperCodeGroup.Text.Trim().ToString() == "") { return_value += lblOperCodeGroup.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }
       
        return return_value;
    }
    #endregion


}

