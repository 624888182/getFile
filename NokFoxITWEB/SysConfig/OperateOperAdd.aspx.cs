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

    FIH.Security.db.Operate myOperate = new FIH.Security.db.Operate();
    FIH.Security.db.OperateInfo myOperateInfo = new FIH.Security.db.OperateInfo();
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
                txtOperCode.Enabled = false;
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
        txtOperCode.Enabled = txtOperName.Enabled = isAble;
        System.Drawing.Color myColor = new Color();
        myColor = (isAble == true) ? Color.Red : Color.Black;
        lblOperCode.ForeColor = lblOperName.ForeColor = myColor;
    }


    private void BindData(string sCondition)
    {
        myOperateInfo = myOperate.getOperate(sCondition);
        txtOperCode.Text = myOperateInfo.OperCode;
        txtOperName.Text = myOperateInfo.OperName;
    }

    private void clearText()
    {
        this.txtOperCode.Text = this.txtOperName.Text = "";
    }

    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        //判斷是否為空值
        if (this.txtOperCode.Text.Trim() == "")
        {
            return_value += lblOperCode.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        if (this.txtOperName.Text.Trim() == "")
        {
            return_value += lblOperName.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
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
        OperateInfo myOperateInfo = new OperateInfo();
        myOperateInfo.OperCode = txtOperCode.Text.Trim();
        myOperateInfo.OperName = txtOperName.Text.Trim();

        //判斷資料是否存在
        if (myOperate.isExist(myOperateInfo) == true)
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
                int rowNum = myOperate.Update(myOperateInfo);
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
                int rowNum = myOperate.Insert(myOperateInfo);
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
