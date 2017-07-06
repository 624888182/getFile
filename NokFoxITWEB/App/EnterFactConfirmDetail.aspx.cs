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

public partial class App_EnterFactConfirmDetail : System.Web.UI.Page
{

    FIH.ForeignStaff.db.EnterFactApplyMaster myEntryMaster = new FIH.ForeignStaff.db.EnterFactApplyMaster();
    FIH.ForeignStaff.db.EnterFactApplyMasterInfo myEntryMasterInfo = new FIH.ForeignStaff.db.EnterFactApplyMasterInfo();

    DbAccessing DAL = new DbAccessing();
    protected string ApplyCode = "";
    protected string ConfirmType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ApplyCode = Request.QueryString[0];
        ConfirmType = Request.QueryString[1];
        if (!IsPostBack)
        {
            txtRejectReason.Focus();
            SetButtonPerssio(GetStatusByApplyCode(ApplyCode), ConfirmType);
            DataBindMaster(ApplyCode);
        }
    }

    private void DataBindMaster(string applyCode)
    {
        myEntryMasterInfo = myEntryMaster.getEnterFactApplyMaster(applyCode);

        txtApplyCode.Text = myEntryMasterInfo.ApplyCode;
        txtApplyDate.Text = myEntryMasterInfo.ApplyDate.ToString();
        txtApplyName.Text = PubFunction.GetEmployeeNameByEmployeeID(myEntryMasterInfo.ApplyId);
        txtDeptName.Text = PubFunction.GetDeptNameByDeptID(myEntryMasterInfo.ApplyDepartment);
        txtTel.Text = myEntryMasterInfo.Tel;
        txtIsBUMgrConfirm.Text = myEntryMasterInfo.IsBUMgrConfirm==true? "Y" : "N";
        txtStatus.Text = PubFunction.GetStatusNameByStatus(myEntryMasterInfo.Status.ToString(), myEntryMasterInfo.IsBUMgrConfirm == true ? "Y" : "N");
        txtRejectReason.Text = myEntryMasterInfo.RejectReason;
        txtDivisionEmployee.Text = PubFunction.GetEmployeeNameByEmployeeID(myEntryMasterInfo.DivisionMgrId);
        txtBUEmployee.Text = PubFunction.GetEmployeeNameByEmployeeID(myEntryMasterInfo.BUMgrId);
        txtMemo.Text = myEntryMasterInfo.Memo;
    }



    private string GetStatusByApplyCode(string applyCode)
    {
        string rtn = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select Status from appEnterFactApplyMaster where ApplyCode = '" + applyCode + "'");
            if (dt.Rows.Count > 0)
            {
                rtn = dt.Rows[0]["Status"].ToString();
            }
        }
        catch { }
        return rtn;
    }

    private void SetButtonPerssio(string status, string ConfirmType)
    {
        switch (ConfirmType)
        {
            case "1":
                switch (status)
                {
                    case "0":
                        btnConfirmY.Enabled = true;
                        btnConfirmN.Enabled = true;
                        break;
                    case "1":
                    case "2":
                    case "8":
                    case "9":
                        btnConfirmY.Enabled = false;
                        btnConfirmN.Enabled = false;
                        break;                    

                }
                break;
            case "2":
                switch (status)
                {
                    case "0":
                    case "1":
                        btnConfirmY.Enabled = true;
                        btnConfirmN.Enabled = true;
                        break;
                    case "2":
                    case "8":
                    case "9":
                        btnConfirmY.Enabled = false;
                        btnConfirmN.Enabled = false;
                        break;

                }
                break;
        }

    }

    private void SetApplyMasterStatus(string ApplyCode, string status, string ConfirmType, string reason)
    {
        try
        {
            string sqlUpdate = "update appEnterFactApplyMaster set Status = '" + status + "' , RejectReason='"+reason+"'";
            if (ConfirmType == "1")
            {
                sqlUpdate += " ,DivisionMgrID = '" + Session["loginUser"].ToString() + "' ,DivisionConfirmDate=getdate() ";
            }
            else
            {
                sqlUpdate += " ,BUMgrID = '" + Session["loginUser"].ToString() + "' ,BUConfirmDate=getdate() ";
            }

            sqlUpdate += " where ApplyCode = '" + ApplyCode + "'";
            DAL.ExecuteSql(sqlUpdate);
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
        }
    }

    protected void btnConfirmY_Click(object sender, EventArgs e)
    {
        switch (ConfirmType)
        {
            case "1":
                SetApplyMasterStatus(ApplyCode, "1", ConfirmType, "");
                break;
            case "2":
                SetApplyMasterStatus(ApplyCode, "2", ConfirmType, "");
                break;
        }
        Response.Redirect("EnterFactConfirm.aspx");
    }
    protected void btnConfirmN_Click(object sender, EventArgs e)
    {
        if (txtRejectReason.Text.Trim() == "")
        {
            lblMessage.Text = "拒絕原因不能是空!";
            return;
        }
        switch (ConfirmType)
        {
            case "1":
                SetApplyMasterStatus(ApplyCode, "8", ConfirmType,txtRejectReason.Text);
                break;
            case "2":
                SetApplyMasterStatus(ApplyCode, "9",ConfirmType,txtRejectReason.Text);
                break;
        }
        Response.Redirect("EnterFactConfirm.aspx");
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("EnterFactConfirm.aspx");
    }
}
