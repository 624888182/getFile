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

public partial class Boundary_wfrmLVPIDLinkquery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanaguage();
        }
    }
    private void MultiLanaguage()
    {
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        lblLVID.Text = (String)GetGlobalResourceObject("SFCQuery", "LVID");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strSql;
        string strLVID = tbProductID.Text.Trim().ToUpper();
        strSql = "SELECT * FROM SFC.MES_ASSY_LV_LINK WHERE LV_ID ='" + strLVID + "' OR PRODUCT_ID ='" + strLVID + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            Label2.Visible = false;
            dgLVIDLink.Visible = true;
            dgLVIDLink.DataSource = dt1.DefaultView;
            dgLVIDLink.DataBind();
        }
        else
        {            
            dgLVIDLink.Visible = false;
            Label2.Text = "Input Error!";
            Label2.Visible = true;
        }  
    }
}
