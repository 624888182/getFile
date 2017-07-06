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

public partial class Boundary_AlterRouter : System.Web.UI.Page
{
    public static string Model_Align = "20";
    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(WFrmYieldRate));
        // 在這裡放置使用者程式碼以初始化網頁
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModel();
            BindStation();
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
    private void BindStation()
    {
        string[] s ={ "A_BI", "A_CT", "A_DT", "A_ET", "A_FO", "A_HT", "A_IT", "A_KT", "A_LT", "A_OT", "CF", "CI", "FC", "FL", "GP", "PH", "PK", "PW", "P_BT", "P_CO", "P_DI", "P_EO", "P_GO", "QA", "RA", "S_AI", "S_BO", "S_CO", "S_IA", "S_IB", "S_LK", "S_OA", "S_OB", "S_VA", "S_VB", "T_BT", "T_CT", "T_DT", "T_ET", "T_FO", "T_JT", "T_LT", "T_NT", "T_QT", "T_WH" };
        foreach (string temp in s)
        {
            ListItem item = new ListItem(temp);
            this.ListBoxRouter.Items.Add(item);
        }
    }
    protected void ButtonSelect_Click(object sender, EventArgs e)
    {
        this.ListBoxRouterEdit.Items.Add(this.ListBoxRouter.SelectedItem);
    }
    protected void ButtonUnselect_Click(object sender, EventArgs e)
    {
        this.ListBoxRouterEdit.Items.Remove(this.ListBoxRouterEdit.SelectedItem);
    }
    protected void ButtonUP_Click(object sender, EventArgs e)
    {
        int index = this.ListBoxRouterEdit.SelectedIndex;
        if (index > 0)
        {
            ListItem item = this.ListBoxRouterEdit.Items[index - 1];
            this.ListBoxRouterEdit.Items.RemoveAt(index - 1);
            this.ListBoxRouterEdit.Items.Insert(index, item);
            this.ListBoxRouterEdit.SelectedIndex = index - 1;
        }
    }

    protected void ButtonDOWN_Click(object sender, EventArgs e)
    {
        int index = this.ListBoxRouterEdit.SelectedIndex;
        if (index < this.ListBoxRouterEdit.Items.Count - 1)
        {
            ListItem item = this.ListBoxRouterEdit.Items[index + 1];
            this.ListBoxRouterEdit.Items.RemoveAt(index + 1);
            this.ListBoxRouterEdit.Items.Insert(index, item);
            this.ListBoxRouterEdit.SelectedIndex = index + 1;
        }
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
        this.ListBoxRouterEdit.Items.Clear();
        string sql = "SELECT STATION_ID FROM SFC.MES_COMM_ROUTE WHERE MODEL_ID LIKE '" + this.ddlModel.SelectedItem.Text.Trim() + "%' AND AREA_ID1 LIKE '" + this.DropDownListRoute.SelectedItem.Value.Trim() + "%' AND MODEL_ALIGN='20' ORDER BY SEQUENCE_ID";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem item = new ListItem(dt.Rows[i][0].ToString());
                this.ListBoxRouterEdit.Items.Add(item);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('選擇有誤！！');</script>");
            return;
        }
    }
    protected void DropDownListSPart_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('選擇有誤！！');</script>");
            return;
        }
    }
}
