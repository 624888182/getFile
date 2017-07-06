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

public partial class App_EnterFactApplyInCompany : System.Web.UI.Page
{
    DbAccessing DAL = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {
        PubFunction.BindOperPermission(this, "B04", "InCompany");
        if (!IsPostBack)
        {
            
        }
        DataBindGV();
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        DataBindGV();
    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }
    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnIn = (LinkButton)e.Row.FindControl("lbtnIn");
            lbtnIn.CommandArgument = e.Row.RowIndex.ToString();
            string ApplyCode = gvList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string ItemNo = gvList.DataKeys[e.Row.RowIndex].Values[1].ToString();

            lbtnIn.OnClientClick = "window.open('EnterFactApplyInCompanyAdd.aspx?ApplyCode="+ApplyCode+"&ItemNo="+ItemNo+"','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');";
        }
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    private void DataBindGV()
    {
        string sql = "select A.ApplyCode,A.ItemNo,A.StaffName,A.IDCardNo,A.Company,A.Tel,A.CardStatus"
                    + ",Convert(varchar(10),A.ExpectedEnterDate,111)+' '+ExpectedEnterTime as ExpectedEnterDate,A.TakeItems,A.Memo "
                    +"  from appEnterFactApplyDetail  A"
                    +"  inner join appEnterFactApplyMaster B On A.ApplyCode = B.ApplyCode"
                    +"  where (B.IsBUMgrConfirm = '0' and B.Status='1' or B.IsBUMgrConfirm = '1' and B.Status='2') and A.CardStatus='1'";

        if (txtStaffName.Text != "")
        {
            sql += "    and A.StaffName like '%" + txtStaffName.Text.Trim() + "%'";
        }
        if (txtCompany.Text != "")
        {
            sql += " and A.Company like '%" + txtCompany.Text.Trim() + "%'";
        }
        if (txtCardNo.Text != "")
        {
            sql += "    and A.CardNo like '"+txtCardNo.Text.Trim()+"'";
        }

        DataTable dt = DAL.ExecuteSqlTable(sql);
        gvList.DataSource = dt.DefaultView;
        gvList.DataBind();

    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        DataBindGV();
    }
}
