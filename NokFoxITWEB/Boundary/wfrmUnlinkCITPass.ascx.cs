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

public partial class Boundary_wfrmUnlinkCITPass : System.Web.UI.UserControl
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
        btnUnlink.Text = (String)GetGlobalResourceObject("SFCQuery", "unlink");
    }
    protected void btnUnlink_Click(object sender, EventArgs e)
    {
        string strIMEI = txtIMEI.Text.Trim().ToUpper();
        string strsql;
        string strppart;
        string strSN;
        string strModel;
        string strIMEI1;
        if (strIMEI.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入IMEI/SN！！');</script>");
            return;
        }
        else
        {
            strsql = "select ppart,SERIAL_NUM,IMEINUM from  SHP.CMCS_SFC_IMEINUM Where IMEINUM LIKE '" + strIMEI + "%' or SERIAL_NUM ='" + strIMEI + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strppart = dt.Rows[0]["ppart"].ToString();
                strSN=dt.Rows[0]["SERIAL_NUM"].ToString();
                strModel = strppart.Substring(2, 3);
                strIMEI1=dt.Rows[0]["IMEINUM"].ToString();
                string strsql1 = "SELECT * FROM SHP.CMCS_SFC_SHIPPING_DATA Where SERIAL_NO ='" + strSN + "'";
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該手機已出貨,不能解除綁定！！');</script>");
                    return;
                }
                else
                {
                    string strsql2 = "select * from " + strModel + "." + strModel + "_CIT_PATS_TH where track_id='" + strSN + "' and test_result='P'";
                    DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        try
                        {
                            string strsql3 = "update SHP.CMCS_SFC_IMEINUM set serial_num='' where imeinum='" + strIMEI1 + "'";
                            ClsGlobal.objDataConnect.DataExecute(strsql3);
                            string strsql4 = "update SFC.CDMA_EMS_MEID_INFO set track_id='' Where SSN ='" + strIMEI1 + "'";
                            ClsGlobal.objDataConnect.DataExecute(strsql4);
                        }
                        catch
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI錯誤！！');</script>");
                            return;
                        }

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該手機未做CIT Pass,請用CMC503解綁！！');</script>");
                        return;
                    }
                }
                
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI/SN輸入錯誤！！');</script>");
                return;
            }

        }
    }
}
