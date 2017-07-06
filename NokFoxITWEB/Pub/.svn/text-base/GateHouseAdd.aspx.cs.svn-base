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
using FIH.ForeignStaff.db;

public partial class Pub_GateHouseAdd : System.Web.UI.Page
{

    protected string StrKeyId = "";
    protected string StrAction = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
        StrAction = Server.UrlDecode(Request.QueryString["Action"]);
        if (!IsPostBack)
        {
            if (StrAction == "edit")
            {
                textUnable(true, "edit");
                this.btnCommit.Visible = true;
                this.btnEdit.Visible = false;

                GatehouseInfo myEntryInfo = new GatehouseInfo();
                Gatehouse myEntry = new Gatehouse();

                myEntryInfo = myEntry.getGatehouse(StrKeyId);

                txtGateHouseCode.Text = myEntryInfo.GatehouseCode;
                txtDescription.Text = myEntryInfo.Description;
                txtMemo.Text = myEntryInfo.Memo;
                txtInitiateId.Text = myEntryInfo.InitiateId;
                txtInitiateDate.Text = myEntryInfo.InitiateDate.ToString();

            }
            else if (StrAction == "view")
            {
                textUnable(false, "view");
                this.btnCommit.Visible = false;
                this.btnEdit.Visible = true;

                GatehouseInfo myEntryInfo = new GatehouseInfo();
                Gatehouse myEntry = new Gatehouse();

                myEntryInfo = myEntry.getGatehouse(StrKeyId);

                txtGateHouseCode.Text = myEntryInfo.GatehouseCode;
                txtDescription.Text = myEntryInfo.Description;
                txtMemo.Text = myEntryInfo.Memo;
                txtInitiateId.Text = myEntryInfo.InitiateId;
                txtInitiateDate.Text = myEntryInfo.InitiateDate.ToString();

            }
            else if (StrAction == "add")
            {
                textUnable(true, "add");
                this.btnCommit.Visible = true;
                this.btnEdit.Visible = false;
            }
        }
    }

    #region   數據驗證
    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        if (txtGateHouseCode.Text == "")
        {
            return_value += lblGateHouseCode.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        else if (txtDescription.Text == "")
        {
            return_value += lblDescription.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        return return_value;
    }
    #endregion
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lblMessage.Text = CommitError().Trim();
            return;
        }
        this.btnCommit.Visible = false;
        this.btnEdit.Visible = true;

        GatehouseInfo myEntryInfo = new GatehouseInfo();
        Gatehouse myEntry = new Gatehouse();

        try
        {
            myEntryInfo.GatehouseCode = txtGateHouseCode.Text;
            myEntryInfo.Description = txtDescription.Text;
            myEntryInfo.Memo = txtMemo.Text;
            myEntryInfo.InitiateId = txtInitiateId.Text.ToString();
            myEntryInfo.InitiateDate = txtInitiateDate.Text == "" ? System.DateTime.Now : Convert.ToDateTime(txtInitiateDate.Text.ToString());
            myEntryInfo.ModiId = Session["loginUser"].ToString();
            myEntryInfo.ModiDate = System.DateTime.Now;
            if (StrAction == "add")
            {
                myEntry.Insert(myEntryInfo);
            }
            else
            {
                myEntry.Update(myEntryInfo);
            }
            this.btnCommit.Visible = false;
            this.btnEdit.Visible = true;
            lblMessage.Text = GetGlobalResourceObject("Message", "Edit_Success").ToString();
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            lblMessage.Text = tmp;
            this.btnCommit.Visible = true;
            this.btnEdit.Visible = false;
        }
        Response.Write("<script>window.opener.document.form1.submit();</script>");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        StrAction = "edit";
        textUnable(true, "edit");
        this.btnCommit.Visible = true;
        this.btnEdit.Visible = false;
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }

    #region  able
    private void textUnable(bool isAble, string op)
    {
        if (op == "add")
        {
            txtGateHouseCode.Enabled = isAble;
        }
        else
        {
            txtGateHouseCode.Enabled = false;
        }
        txtDescription.Enabled = isAble;
        txtMemo.Enabled = isAble;
    }
    #endregion
}
