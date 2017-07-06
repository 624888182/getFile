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

public partial class Boundary_B2B_OrderChange : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
            //InitiaPage();
            MultiLanguage();
        }
    }

    private void MultiLanguage()
    {
        lblgeshi1.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        lblgeshi2.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = true;
            lblgeshi2.Visible = false;
            return;
        }

        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = false;
            lblgeshi2.Visible = true;
            return;
        }
        lblgeshi1.Visible = false;
        lblgeshi2.Visible = false;

        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strMesgID = txtMesgID.Text.Trim();
        string strPoNo = txtPoNo.Text.Trim();
        string strReqChange = ddlREQ_CHANGE.SelectedValue.Trim();
        string strUploadFlag = ddlUploadFlag.SelectedValue.Trim();
        string strChangeFlag = ddlChangeFlag.SelectedValue.Trim();
        string strAckSendFlag = ddlAckSendFlag.SelectedValue.Trim();
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
       
        string strScript = "<script language='jscript'>var res = window.open('./B2B_OrderChange_Report.aspx?StartDate=" + strStartDate + "&EndDate=" + strEndDate
            + "&MesgID=" + strMesgID + "&PoNo=" + strPoNo + "&ReqChange=" + strReqChange
            + "&UploadFlag=" + strUploadFlag + "&ChangeFlag=" + strChangeFlag + "&AckSendFlag=" + strAckSendFlag 
            + "&Order=" + strOrder + "&PageRows=" + strPageRows + "','_blank', '');</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
    }
}
