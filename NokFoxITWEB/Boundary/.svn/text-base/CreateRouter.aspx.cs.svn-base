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
using System.Data.OracleClient;

public partial class Boundary_CreateRouter : System.Web.UI.Page
{
    public static string Kind = "";
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

    protected void DropDownListRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        Kind = this.DropDownListRoute.SelectedValue.Trim();
    }

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (Kind != "")
        {
            string value = "";
            string PriorStation = "";
            string LatterStation = "";
            string strModel = this.ddlModel.SelectedItem.Text.Trim();
            for (int i = 0; i < this.ListBoxRouterEdit.Items.Count; i++)
            {
                value = "";
                if (i == 0)
                {
                    if (Kind == "S")
                    {
                        PriorStation = "";
                    }
                    else if (Kind == "T")
                    {
                        PriorStation = "S_AI";
                    }
                    else if (Kind == "A")
                    {
                        PriorStation = "T_WH";
                    }
                    else
                    {
                        PriorStation = "";
                    }
                }
                else
                {
                    PriorStation = this.ListBoxRouterEdit.Items[i - 1].Text.Trim();
                }
                if (i == this.ListBoxRouterEdit.Items.Count - 1)
                {
                    if (Kind == "S")
                    {
                        if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "MRO" || strModel == "MRE")
                        {
                            LatterStation = "PW";
                        }
                        else
                        {
                            LatterStation = "";
                        }
                    }
                    else if (Kind == "T")
                    {
                        LatterStation = "A_BI";
                    }
                    else if (Kind == "A")
                    {
                        LatterStation = "LINK";
                    }
                    else
                    {
                        LatterStation = "";
                    }
                }
                else
                {
                    LatterStation = this.ListBoxRouterEdit.Items[i + 1].Text.ToString().Trim();
                }
                value += "'" + Kind + "','" + strModel + "','" + Model_Align + "','" + this.ListBoxRouterEdit.Items[i].Text.Trim() + "','" + LatterStation + "','P','" + (i + 1) + "','" + PriorStation + "'";

                if (value.Trim() != null)
                {
                    string sql = "Insert into SFC.MES_COMM_ROUTE"
                                + "(AREA_ID1, MODEL_ID, MODEL_ALIGN, STATION_ID, STATION_ID_O1, STATE_ID, SEQUENCE_ID, TEST_STATION)"
                                + " Values"
                                + " (" + value + ")";
                    ClsGlobal.objDataConnect.DataExecute(sql);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請創建合理的路由！！');</script>");
                    return;
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由成功！！');</script>");
            return;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇保存類型！！');</script>");
            return;
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
