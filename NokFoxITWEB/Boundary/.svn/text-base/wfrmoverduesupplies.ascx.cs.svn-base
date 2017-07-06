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
using DBAccess.EAI;

public partial class Boundary_wfrmoverduesupplies : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tbStartDate.Language = Calendar1.Languages.TraditionalChinese;
        string temp = tbStartDate.ThemePath;
        temp = tbStartDate.ThemeTitle;
        temp = tbStartDate.LayoutPath;
        if (!IsPostBack)
        {
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            Label28.Text = ViewState["ErrorDate"].ToString();
            Label28.Visible = true;
            Label29.Visible = false;
            return;
        }
        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            Label29.Text = ViewState["ErrorDate"].ToString();
            Label29.Visible = true;
            Label28.Visible = false;
            return;
        }

        Label28.Visible = false;
        Label29.Visible = false;

        System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
        if (intday.TotalDays > 7)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於一周！');</script>");
            return;
        }
        DataTable dt = GetData();
        if (dt.Rows.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "<script language=javascript>alert('此段時間尚無過期物料!');</script>");
            return;
        }
        else
        {
            dgData.DataSource = dt.DefaultView;
            dgData.DataBind();
            lbltotal.Text = "Current Page:" + (dgData.CurrentPageIndex + 1).ToString() + "/" + dgData.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

            dt.Dispose();
        }
    }

    private DataTable GetData()
    {
        DateTime dttmStartDate = Convert.ToDateTime(tbStartDate.DateTextBox.Text);
        DateTime dttmEndDate = Convert.ToDateTime(tbEndDate.DateTextBox.Text);
        string strStartDate = dttmStartDate.ToString("yyyy/MM/dd HH:mm");
        string strEndDate = dttmEndDate.ToString("yyyy/MM/dd HH:mm");
        string strsql = " select * from (select a.gr_no,a.seq_no,a.cdt,b.key_part_no,b.diskno from sap.sap_gr_seq a,sfc.k_receive_diskno b where b.key_part_no like 'SA%' and length(b.diskno)>13 " +
            " and a.gr_no=substr(b.diskno,1,10)  and lpad(seq_no,4,'0')=substr(b.diskno,11,4) and cdt between to_date('" + strStartDate + "','YYYY/MM/DD HH24:MI')-360 and to_date('" + strEndDate + "','YYYY/MM/DD HH24:MI')-360 ";
        strsql = strsql + " union all ";
        strsql = strsql + "select a.gr_no,a.seq_no,a.cdt,b.key_part_no,b.diskno from sap.sap_gr_seq a,sfc.k_receive_diskno b where b.key_part_no like 'SB%' and length(b.diskno)>13 " +
            " and a.gr_no=substr(b.diskno,1,10)  and lpad(seq_no,4,'0')=substr(b.diskno,11,4) and cdt between to_date('" + strStartDate + "','YYYY/MM/DD HH24:MI')-180 and to_date('" + strEndDate + "','YYYY/MM/DD HH24:MI')-180 ) order by  cdt";

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        return dt;
    }

    protected void dgData_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgData.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = GetData();
        dgData.DataSource = dt.DefaultView;
        dgData.DataBind();
        lbltotal.Text = "Current Page:" + (dgData.CurrentPageIndex + 1).ToString() + "/" + dgData.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
}
