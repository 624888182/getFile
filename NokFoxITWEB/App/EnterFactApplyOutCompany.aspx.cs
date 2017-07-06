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
    protected string UpdateErr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PubFunction.BindOperPermission(this, "B05", "");
        if (!IsPostBack)
        {

        }

    }

    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }


    protected void btnOutCompany_Click(object sender, EventArgs e)
    {
        if (txtCardNo.Text.Trim() == "")
        {
            lblMessage.Text = "�d��������!";
            return;
        }
        if (GetOutCount(txtCardNo.Text)==1)
        {
            string sqlUpdate = "Update appEnterFactApplyDetail set CardStatus='3',ActualLeaveDate = getDate()"
                                +" where CardStatus = '2' and CardNo = '" + txtCardNo.Text.Trim() + "'";
            DAL.ExecuteSql(sqlUpdate);
            lblMessage.Text = "�X�t���\!";
        }
        else
        {
            lblMessage.Text = "�X�t����\r\n" + UpdateErr;
        }
    }

    private int GetOutCount(string CardNo)
    {
        int rtn = 0;
        DataTable dt = DAL.ExecuteSqlTable("select * from appEnterFactApplyDetail where CardStatus = '2' and CardNo = '" + CardNo + "'");
        if (dt.Rows.Count == 1)
        {
            rtn = 1;
            txtStaffName.Text = dt.Rows[0]["StaffName"].ToString();
            txtCompany.Text = dt.Rows[0]["Company"].ToString();
        }
        else if (dt.Rows.Count == 0)
        {
            rtn = 0;
            UpdateErr = "�����d���H�S�����!";
        }
        else if (dt.Rows.Count > 1)
        {
            rtn = dt.Rows.Count;
            UpdateErr = "�����d���H" + dt.Rows.Count.ToString() + "��";
        }
        return rtn;
    }

    
}
