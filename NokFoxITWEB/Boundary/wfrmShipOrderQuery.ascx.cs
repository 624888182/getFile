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
using System.Data.OleDb;
using DBAccess.EAI;

public partial class Boundary_wfrmShipOrderQuery : System.Web.UI.UserControl
{
    string sql = "";
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
        string sql = "";
        sql = "SELECT * FROM SFC.CDMA_MOTO_ORDERNO where ORDER_NUMBER LIKE '" + this.txtShoporder.Text.Trim() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgShiporder.Visible = true;
            this.dgShiporder.DataSource = dt.DefaultView;
            this.dgShiporder.DataBind(); 
        }
        else
        {
            dgShiporder.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該ShopOrder不存在...');</script>");
            return;
        }
    }
}
