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

public partial class Boundary_wfrmITRouteSet : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(WFrmYieldRate));
        // 在這裡放置使用者程式碼以初始化網頁
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModel();
            this.TextBoxStation.Visible = false;
            this.TextBoxStationDesc.Visible = false;

            this.Button1.Visible = false;
        }
    }
    private void BindModel()
    {

        string strProcedureName = "ROUTE_CODE_CONFIGURE_PACKAGE.ROUTE_CODE_GET_MODEL";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("MODEL", OracleType.Cursor) };
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
        string strProcedureName = "ROUTE_CODE_CONFIGURE_PACKAGE.ROUTE_CODE_GET_TABLENAME";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("MODEL",OracleType.VarChar),
                                                            new OracleParameter("TABLE_NAME", OracleType.Cursor) };

        orapara[0].Value = ddlModel.SelectedItem.Value;

        orapara[1].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];

        ListBoxTableName.DataTextField = "TABLE_NAME";
        ListBoxTableName.DataValueField = "TABLE_NAME";
        ListBoxTableName.DataSource = dt.DefaultView;
        ListBoxTableName.DataBind();
    }
    protected void ButtonSelect_Click(object sender, EventArgs e)
    {
        this.ListBoxRouter.Items.Add(ddlModel.SelectedItem.Text + "," + ListBoxTableName.SelectedItem.Text + "," + DropDownListStation.SelectedItem.Text);
    }
    protected void ButtonUnselect_Click(object sender, EventArgs e)
    {
        this.ListBoxRouter.Items.Remove(ListBoxRouter.SelectedItem);
    }
    protected void ButtonUP_Click(object sender, EventArgs e)
    {
        int index = this.ListBoxRouter.SelectedIndex;
        if (index > 0)
        {
            ListItem item = this.ListBoxRouter.Items[index - 1];
            this.ListBoxRouter.Items.RemoveAt(index - 1);
            this.ListBoxRouter.Items.Insert(index, item);
            this.ListBoxRouter.SelectedIndex = index - 1;
        }
    }
    protected void ButtonDOWN_Click(object sender, EventArgs e)
    {
        int index = this.ListBoxRouter.SelectedIndex;
        if (index < this.ListBoxRouter.Items.Count - 1)
        {
            ListItem item = this.ListBoxRouter.Items[index + 1];
            this.ListBoxRouter.Items.RemoveAt(index + 1);
            this.ListBoxRouter.Items.Insert(index, item);
            this.ListBoxRouter.SelectedIndex = index + 1;
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {

    }
    protected void ListBoxTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownListStation.Items.Clear();
        string strProcedureName = "ROUTE_CODE_CONFIGURE_PACKAGE.ROUTE_CODE_GET_STATION_NAME";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("TABLE_NAME",OracleType.VarChar),
                                                            new OracleParameter("STATION_NAME", OracleType.Cursor) };

        orapara[0].Value = ListBoxTableName.SelectedItem.Text.Trim();

        orapara[1].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];

            DropDownListStation.DataTextField = "STATION";
            DropDownListStation.DataValueField = "STATION";
            DropDownListStation.DataSource = dt.DefaultView;
            DropDownListStation.DataBind();
            DropDownListStation.Items.Add(" ");
            DropDownListStation.Items.Add("新增...");
    }
    protected void DropDownListStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListStation.SelectedItem.Text == "新增...")
        {
            this.TextBoxStation.Visible = true;
            this.TextBoxStationDesc.Visible = true;

            this.Button1.Visible = true;
        }
        else 
        {
            this.TextBoxStation.Visible = false;
            this.TextBoxStationDesc.Visible = false;
            this.Button1.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBoxStation.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入站點名稱！！');</script>");
            this.TextBoxStation.Focus();
            return;
        }
        else
        {
            string sql = "insert into SFC.ROUTE_CODE_STATION_DESCRIPTION( STATION_ID, DESCRIPTION,STATION_TABLE, TESTSTATION, USER_NAME, CREATE_DATE) values("
                        + "'" + this.TextBoxStation.Text + "','"+this.TextBoxStationDesc.Text+"','" + ListBoxTableName.SelectedItem.Text + "','Y','" + Context.User.Identity.Name + "',sysdate)";

            ClsGlobal.objDataConnect.DataExecute(sql);

            this.TextBoxStation.Text = "";
            this.TextBoxStationDesc.Text = ""; 

            this.TextBoxStation.Visible = false;
            this.TextBoxStationDesc.Visible = false; 
            this.Button1.Visible = false;
        }
    }
}
