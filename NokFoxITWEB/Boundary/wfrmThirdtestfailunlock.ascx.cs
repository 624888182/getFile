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

public partial class Boundary_wfrmThirdtestfailunlock : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitiaPage();
            txtProductID.Attributes.Add("onkeydown", "if(event.keyCode==13){document.all.ctl01_btnQuery.click(); }");   

        }
    }

    private void InitiaPage()
    {
        string strsql = "select group_name FROM MCMSMO.C_GROUP_CONFIG_T where section_name like '%TEST' order by group_name ";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        ddlStation.DataTextField = "group_name";
        ddlStation.DataValueField = "group_name";
        ddlStation.DataSource = dt.DefaultView;
        ddlStation.DataBind();
        ddlStation.Items.Insert(0, "");       
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        
        string strpid = txtProductID.Text.Trim().ToUpper();
        string strstation = ddlStation.SelectedValue.ToString();
        if (strstation.Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('請選擇測試工站');</script>");
            return;
        }
        else
        {
            string strSql = "select finish_times from SFC.CMCS_SFC_WIP_T where STATE_FLAG='F' AND product_id= '" + strpid + "' and station_name= '" + strstation + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strtimes = dt.Rows[0][0].ToString();
                int times = Convert.ToInt32(strtimes);
                if (times >= 3)
                {
                    try
                    {
                        string strsql = "update sfc.cmcs_sfc_wip_t set finish_times=0 ,state_flag='P'  where STATE_FLAG='F' AND product_id= '" + strpid + "' and station_name= '" + strstation + "'";
                        ClsGlobal.objDataConnect.DataExecute(strsql);

                        Label4.Text = "解鎖OK!";
                        Label4.Visible = true;
                        this.txtProductID.Focus();
                        this.txtProductID.Attributes["onFocus"] = "javascript:this.select();";
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('數據庫操作異常...');</script>");
                        return;
                    }
                }
                else
                {
                    txtProductID.Focus();
                    txtProductID.Attributes["onFocus"] = "javascript:this.select();";
                    Label4.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,該手機不需要解鎖!');</script>");
                    return;
                }

            }
            else
            {
                string strsql0 = "select station_name from sfc.cmcs_sfc_wip_t where state_flag='F' and product_id='" + strpid + "' and finish_times=3 ";
                DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
                if (dt0.Rows.Count > 0)
                {
                    string station = dt0.Rows[0][0].ToString();
                    txtProductID.Focus();
                    txtProductID.Attributes["onFocus"] = "javascript:this.select();";
                    Label4.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('工站錯誤,請選擇" + station + "!');</script>");
                    return;
                }
                else
                {
                    txtProductID.Focus();
                    txtProductID.Attributes["onFocus"] = "javascript:this.select();";
                    Label4.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,此工站不存在該手機FAIL的測試記錄!');</script>");
                    return;
                }
            }
        }
    }
 
    protected void txtProductID_TextChanged(object sender, EventArgs e)
    {
        string strpid = txtProductID.Text.Trim().ToUpper();
        string strstation = ddlStation.SelectedValue.ToString();
        string strSql = "select finish_times from SFC.CMCS_SFC_WIP_T where STATE_FLAG='F' AND product_id= '" + strpid + "' and station_name= '" + strstation + "'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string strtimes=dt.Rows[0][0].ToString();
            int times=Convert.ToInt32(strtimes);
            if(times>=3)
                this.btnQuery_Click(sender, e);
            else
            {
                txtProductID.Focus();
                txtProductID.Attributes["onFocus"] = "javascript:this.select();";
                Label4.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,此手機沒有鎖板!');</script>");
                return;
            }
        }
        else
        {
            txtProductID.Focus();
            txtProductID.Attributes["onFocus"] = "javascript:this.select();";
            Label4.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('輸入錯誤,此工站不存在該手機FAIL的測試記錄!');</script>");
            return;
        }
    } 
}
