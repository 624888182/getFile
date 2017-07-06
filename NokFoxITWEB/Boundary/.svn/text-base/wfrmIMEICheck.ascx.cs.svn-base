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

public partial class Boundary_wfrmIMEICheck : System.Web.UI.UserControl
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
        string strIMEI1 = tbIMEI1.Text.Trim().ToUpper();
        string strIMEI2 = tbIMEI2.Text.Trim().ToUpper();
        if (tbIMEI1.Text.Trim().ToUpper() == tbIMEI2.Text.Trim().ToUpper())
        {
            Label4.Text="Check ok!";
            Label4.Visible = true; 
            this.tbIMEI1.Focus();
            this.tbIMEI1.Attributes["onFocus"] = "javascript:this.select();";
        }
        else
        {
            tbIMEI1.Focus();
            tbIMEI1.Attributes["onFocus"] = "javascript:this.select();";
            Label4.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,IMEI1與IMEI2必須一致!');</script>");
            return;
        }
    }

    protected void tbIMEI1_TextChanged(object sender, EventArgs e)
    {
        if (tbIMEI1.Text.Trim().Length >= 14)
        {
            string strSql = "select * from SHP.CMCS_SFC_IMEINUM where imeinum like '" + tbIMEI1.Text.Trim().ToUpper() + "%'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                tbIMEI2.Text = "";
                tbIMEI2.Focus();
                tbIMEI2.Attributes["onFocus"] = "javascript:this.select();";
                Label4.Visible = false; 
            }
            else 
            {
                tbIMEI1.Focus();
                tbIMEI1.Attributes["onFocus"] = "javascript:this.select();";
                Label4.Visible = false; 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,請輸入14位或15位的IMEI碼!');</script>");                
                return;               
            }
        }
        else
        {
            tbIMEI1.Focus();
            tbIMEI1.Attributes["onFocus"] = "javascript:this.select();";
            Label4.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,請輸入14位或15位的IMEI碼!');</script>");
            return;            
        }
    }
    protected void tbIMEI2_TextChanged(object sender, EventArgs e)
    {
        string strSql = "select * from SHP.CMCS_SFC_IMEINUM where imeinum like '" + tbIMEI2.Text.Trim().ToUpper() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (tbIMEI1.Text.Trim().ToUpper() == tbIMEI2.Text.Trim().ToUpper())
                this.btnQuery_Click(sender, e);
            else
            {
                tbIMEI1.Focus();
                tbIMEI1.Attributes["onFocus"] = "javascript:this.select();";
                Label4.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,IMEI1與IMEI2必須一致!');</script>");
                return;    
            }
        }
        else
        {
            tbIMEI1.Focus();
            tbIMEI1.Attributes["onFocus"] = "javascript:this.select();";
            Label4.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,請輸入14位或15位的IMEI碼!');</script>");
            return;    
        }
    }
}
