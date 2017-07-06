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
using System.Data.SqlClient;

public partial class Boundary_wfrmB2B_PO : System.Web.UI.UserControl
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
            bindplantcode();
            bindcotmod();
        }
    }

    private void bindcotmod()
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = "select distinct ctomod from purchaseordersmain ";
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "ctomod");
        ddlCtoMod.DataTextField = "ctomod";
        ddlCtoMod.DataValueField = "ctomod";
        ddlCtoMod.DataSource = ds.Tables[0].DefaultView;
        ddlCtoMod.DataBind();
        ddlCtoMod.Items.Insert(0, "All");
    }

    private void bindplantcode()
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = "select distinct plantcode from purchaseordersmain where upper(plantcode) not in('NULL') ";
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn); 
        DataSet ds = new DataSet();
        sda.Fill(ds, "plantcode");
        ddlPlantCode.DataTextField = "plantcode";
        ddlPlantCode.DataValueField = "plantcode";
        ddlPlantCode.DataSource = ds.Tables[0].DefaultView;
        ddlPlantCode.DataBind();
        ddlPlantCode.Items.Insert(0, "All");
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
        string strPlantCode = ddlPlantCode.SelectedValue.Trim();
        string strCsoNum = txtCsoNum.Text.Trim();
        string strMesgID = txtMesgID.Text.Trim();
        string strPoNo = txtPoNo.Text.Trim();
        string strUploadFlag = ddlUploadFlag.SelectedValue.Trim();
        string strCtoMod = ddlCtoMod.SelectedValue.Trim();
        string strSoNo = txtSoNo.Text.Trim();
        string strIdocNo = txtIdocNo.Text.Trim();
        string strSfcFlag = ddlSfcFlag.SelectedValue.Trim();
        string strAckFlag = ddlAckFlag.SelectedValue.Trim();
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

        string strScript = "<script language='jscript'>var res = window.open('./B2B_PO_MainReport.aspx?StartDate=" + strStartDate + "&EndDate=" + strEndDate
            + "&PlantCode=" + strPlantCode + "&CsoNum=" + strCsoNum + "&MesgID=" + strMesgID + "&PoNo=" + strPoNo + "&UploadFlag=" + strUploadFlag
            + "&CtoMod=" + strCtoMod + "&SoNo=" + strSoNo + "&IdocNo=" + strIdocNo + "&SFCFlag=" + strSfcFlag + "&ACKFlag=" + strAckFlag
            + "&Order=" + strOrder + "&PageRows=" + strPageRows + "','_blank', '');</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
    }
}
