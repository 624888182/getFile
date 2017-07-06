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

public partial class Boundary_wfrmIMEINUMinformation : System.Web.UI.UserControl
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
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strSql;
        string strImei = tbProductID.Text.Trim().ToUpper();
        if(strImei.Substring(0,1).Equals("M"))
            strSql = "SELECT S.porder,S.ppart,imeinum,serial_num,product_id,shift_porder FROM CMCS_SFC_IMEINUM S,SFC.CDMA_EMS_MEID_INFO T WHERE T.TRACK_ID=S.SERIAL_NUM AND S.IMEINUM=T.SSN AND SERIAL_NUM ='" + strImei + "'";
        else
            strSql = "SELECT S.porder,S.ppart,imeinum,serial_num,product_id,shift_porder FROM CMCS_SFC_IMEINUM S,SFC.CDMA_EMS_MEID_INFO T WHERE T.TRACK_ID=S.SERIAL_NUM AND S.IMEINUM=T.SSN AND IMEINUM like'" + strImei + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            Label2.Visible = false;
            dgIMEINUM.Visible = true;
            dgIMEINUM.DataSource = dt1.DefaultView;
            dgIMEINUM.DataBind();
        }
        else
        {
            string strSql1;
            strSql1 = "SELECT porder,ppart,imeinum,serial_num,product_id,shift_porder FROM CMCS_SFC_IMEINUM WHERE IMEINUM like'" + strImei + "%' or serial_num = '" + strImei + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql1).Tables[0];
            if (dt.Rows.Count == 0)
            {               
                dgIMEINUM.Visible = false;
                Label2.Text = "此板尚未綁定!";
                Label2.Visible = true;
            }
            else
            {
                Label2.Visible = false;
                dgIMEINUM.Visible = true;
                dgIMEINUM.DataSource = dt.DefaultView;
                dgIMEINUM.DataBind();
            }
        }            
    }  
}