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
using System.Data.OracleClient;
using DBAccess.EAI;

public partial class Boundary_wfrmRouteSet : System.Web.UI.UserControl
{
    string Model_Align = "20";
    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(WFrmYieldRate));
        // 在這裡放置使用者程式碼以初始化網頁
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModel();
        }
    }
    private void BindModel()
    {

        string strProcedureName = "SFCQUERY.GETMODELNAME";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
        orapara[0].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();
    }
    private void MultiLanguage()
    {
        ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        ViewState["WONotExist"] = (String)GetGlobalResourceObject("SFCQuery", "WONotExist");//rm.GetString("WONotExist");
    }
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "SELECT distinct SPART FROM SHP.CMCS_SFC_SORDER WHERE MODEL LIKE '" + this.ddlModel.SelectedItem.Text.Trim() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DropDownListSPart.DataTextField = "SPART";
            DropDownListSPart.DataValueField = "SPART";
            DropDownListSPart.DataSource = dt.DefaultView;
            DropDownListSPart.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('選擇有誤！！');</script>");
            return;
        }
    }
    protected void DropDownListRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        string sql = "SELECT SEQUENCE_ID,STATION_ID FROM SFC.MES_COMM_ROUTE WHERE MODEL_ID LIKE '" + this.ddlModel.SelectedItem.Text.Trim() + "%' AND AREA_ID1 LIKE '" + this.DropDownListRoute.SelectedItem.Value.Trim() + "%' AND MODEL_ALIGN='20' ORDER BY SEQUENCE_ID";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label1.Text += dt.Rows[i][0].ToString() + "." + dt.Rows[i][1].ToString() + "-->";
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前沒有這個INVOICE的發送數據！！');</script>");
            return;
        }
    }
    protected void DropDownListSPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";

        string sql = "select SFC.GET_SEQUENCE_FROM_PARTNO('" + this.DropDownListSPart.SelectedItem.Text.ToString().Trim() + "') from dual";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() != "")
            {
                Model_Align = dt.Rows[0][0].ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('選擇有誤！！');</script>");
                return;
            }

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前沒有這個INVOICE的發送數據！！');</script>");
            return;
        }
    }
}
