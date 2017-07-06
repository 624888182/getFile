using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using System.Resources;
using System.Globalization;
using DB.EAI;
using System.Threading;
using ChartDirector;
using System.Data.OracleClient;

public partial class Boundary_WfrmLineStatusMonitor0 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 在這裡放置使用者程式碼以初始化網頁
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModel();
        } 
    }

    #region Web Form 設計工具產生的程式碼
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
    ///		這個方法的內容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    private void MultiLanguage()
    {
        lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");//rm.GetString("Line");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strModel = ddlModel.SelectedValue.ToUpper();
        string strLine = ddlLine.SelectedValue.ToUpper();

        string strScript = "<script language='jscript'>var res = window.open('./WFrmModalDialog.aspx?Url=./WfrmLineStatusMonitor.ascx&Line=" + strLine + "&Model="+strModel+"','_blank', '');</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);	
    }
}
