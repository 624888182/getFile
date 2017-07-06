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

public partial class Boundary_B2B_Label : System.Web.UI.UserControl
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
        string strPoNo = txtPoNo.Text.Trim();       
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
        //string strPanelID = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text; 

        //string strScript = "<script language='jscript'>var res = window.open('./WFrmModalDialog.aspx?Url=./wfrmDellPOMainReport.ascx&StartDate=" + strStartDate + "&EndDate=" + strEndDate
        //    + "&PlantCode=" + strPlantCode + "&CsoNum=" + strCsoNum + "&MesgID=" + strMesgID + "&PoNo=" + strPoNo + "&UploadFlag=" + strUploadFlag
        //    + "&CtoMod=" + strCtoMod + "&SoNo=" + strSoNo + "&IdocNo=" + strIdocNo + "&SfcFlag=" + strSfcFlag + "&AckFlag=" + strAckFlag
        //    + "&Order=" + strOrder + "&PageRows=" + strPageRows + "','_blank', '');</script>";

        //string tt = "<script language='jscript'>function " + strStartDate + "(){var res = window.showModalDialog('./B2B_PO_MainReport.aspx?PID=" + strProductid + "&MyDate=' + Date(), '','dialogWidth:450px; dialogHeight:400px; center:yes; scroll:1;"
        //+ "status:no;help:no');}</script>";
        //Page.ClientScript.RegisterStartupScript(this.GetType(), strProductid, tt);
        //strTemp = strTemp + "<a onclick=\"" + strProductid + "()\" href='#'>" + dr["PRODUCT_ID"].ToString() + "</a> <br>";

        string strScript = "<script language='jscript'>var res = window.open('./B2B_Label_MainReport.aspx?StartDate=" + strStartDate + "&EndDate=" + strEndDate
            + "&PoNo=" +strPoNo+ "&Order=" + strOrder + "&PageRows=" + strPageRows + "','_blank', '');</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
    }
}
