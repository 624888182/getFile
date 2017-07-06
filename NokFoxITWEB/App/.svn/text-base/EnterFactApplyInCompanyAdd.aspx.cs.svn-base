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

public partial class App_EnterFactApplyInCompanyAdd : System.Web.UI.Page
{
    FIH.ForeignStaff.db.EnterFactApplyDetail myEntityDetail = new FIH.ForeignStaff.db.EnterFactApplyDetail();
    FIH.ForeignStaff.db.EnterFactApplyDetailInfo myEntityInfoDetail = new FIH.ForeignStaff.db.EnterFactApplyDetailInfo();
    DbAccessing ds = new DbAccessing();

    protected string ApplyCode = "";
    protected string ItemNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ApplyCode = Request.QueryString["ApplyCode"];
        ItemNo = Request.QueryString["ItemNo"];
        if (!IsPostBack)
        {
            ShowData(ApplyCode, ItemNo);
            txtIDCardNo.Focus();
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            ds.ExecuteSql("Update appEnterFactApplyDetail set IDCardNo='" + txtIDCardNo.Text + "',TakeItems = '" + txtTakeItems.Text + "' , CardStatus='2',ActualEnterDate=getdate() "
                            + " where ApplyCode='" + ApplyCode + "' and ItemNo='" + ItemNo + "'");
            lblMessage.Text = "登記成功!";
        }
        catch
        {
            lblMessage.Text = "登記失敗!";
        }
        //this.Page.RegisterClientScriptBlock("", "<script>window.opener.document.formInCompany.location.href='EnterFactApplyInCompany.aspx'");

        Response.Write("<script language=javascript>window.opener.document.formInCompany.submit();window.close();</script>"); 
        
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close()</script>");
    }

    private void ShowData(string ApplyCode,string ItemID)
    {

        myEntityInfoDetail = myEntityDetail.getEnterFactApplyDetail(ApplyCode, Convert.ToInt32(ItemID));
        txtApplyCode.Text = ApplyCode;
        txtItemNo.Text = ItemID;
        txtStaffName.Text = myEntityInfoDetail.StaffName;
        txtCompany.Text = myEntityInfoDetail.Company;
        txtTel.Text = myEntityInfoDetail.Tel;
        txtEnterFactReason.Text = PubFunction.GetEnterReasonNameByID(myEntityInfoDetail.EnterFactReason);
        txtGateHouse.Text = PubFunction.GetGateHouseNameByID(myEntityInfoDetail.GateHouse);
        txtReceptionDept.Text = myEntityInfoDetail.ReceptionDept;
        txtReceptionStaff.Text = myEntityInfoDetail.ReceptionStaff;
        txtReceptionTel.Text = myEntityInfoDetail.ReceptionTel;
        txtExpectedEnterDate.Text = Convert.ToDateTime(myEntityInfoDetail.ExpectedEnterDate).ToString("yyyy/M/dd")+" "+myEntityInfoDetail.ExpectedEnterTime;
        txtExpectedLeaveDate.Text = Convert.ToDateTime(myEntityInfoDetail.ExpectedLeaveDate).ToString("yyyy/M/dd") + " " + myEntityInfoDetail.ExpectedLeaveTime;
        txtCardNo.Text = myEntityInfoDetail.CardNo;
        txtMemo.Text = myEntityInfoDetail.Memo;
        txtIDCardNo.Text = myEntityInfoDetail.IDCardNo;
        txtTakeItems.Text = myEntityInfoDetail.TakeItems;
    }

    private void dd(string dd)
    {   
     
    
    
    }
}
