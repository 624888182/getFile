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

public partial class Boundary_wfrmCartonWeightQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage(); 
        }
    }
    private void MultiLanguage()
    { 
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "SELECT * FROM SHP.CMCS_SFC_CARTON where CARTON_NO = '" + this.txtCartonno.Text.Trim().ToUpper() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgCarton.Visible = true;
            dgCarton.DataSource = dt1.DefaultView;
            dgCarton.DataBind();
        }
        else
        {
            dgCarton.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該箱號的稱重信息...');</script>");
            return;
        }
    }
}
