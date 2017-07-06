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

public partial class Boundary_wfrmLackPartAlert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MultiLanguage();
        show();
    }

    private void show()
    {
        string strSql = GetSql();
        DataTable dt = null;
        dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        dgLack.DataSource = dt.DefaultView;
        dgLack.DataBind(); 
    }

    private string GetSql()
    {
        string strsql = "SELECT T.MODEL_NAME,T.MACHINE_BOM_NAME,S.MACHINE_CODE,S.TRACK_NO,S.KEY_PART_NO,S.STD_QTY FROM SFC.K_SMT_BOM_DETAIL s,SFC.K_SMT_BOM t " +
            " where S.MACHINE_BOM_ID=T.MACHINE_BOM_ID ";

        return strsql;

    }

   
    private void MultiLanguage()
    {
        lb1Lack.Text = (String)GetGlobalResourceObject("SFCQuery", "SMTLack"); 
    }
}
