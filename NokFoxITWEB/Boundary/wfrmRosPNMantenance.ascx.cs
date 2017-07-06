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

public partial class Boundary_wfrmRosPNMantenance : System.Web.UI.UserControl
{
    string current;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage(); 
        }
    }

    private void MultiLanguage()
    {
        lblPart.Text = (String)GetGlobalResourceObject("SFCQuery", "PartNo"); 
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    private void BindData(string part)
    {
        dgPart.Visible = true;
        string strPart = txtPart.Text.ToUpper();
        string strsql = "select PPART,NVL(PHONE_MODEL,'') PHONE_MODEL,NVL(T_OPTI,'') T_OPTI,NVL(SA_NO,'') SA_NO,NVL(COLOR,'') COLOR,NVL(MFG_COUNTRY,'') MFG_COUNTRY,NVL(EAN,'') EAN,hw_ver,sw_ver,NVL(picasso_def,'') picasso_def,NVL(BACK_NUM,'') BACK_NUM,NVL(last_updated,'') last_updated,qty,NVL(updated_by,'') updated_by, " +
            "NVL(COMMODITY,'') COMMODITY,NVL(CONTENT,'') CONTENT,NVL(MRP,'') MRP,NVL(MARKET_NAME,'') MARKET_NAME,TYPE_CODE,CHECKBOX,NVL(LABEL_TYPE,'') LABEL_TYPE,NVL(E2PREFFILE,'') E2PREFFILE,NVL(RDL_PATH,'') RDL_PATH FROM shp.ROS_TCH_PN where ppart =  '" + strPart + "'";
         DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        { 
            dgPart.DataSource = this.GetIDTable(dt1).DefaultView;
            dgPart.DataBind();
            panel2.Visible = true;
            checkbox();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('找不到資料,請先新增..！！');</script>");
            return;
        }
    }

    protected void dgPart_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strPart = txtPart.Text.ToUpper();
        dgPart.EditItemIndex = e.Item.ItemIndex;
        BindData(strPart);
        e.Item.Cells[24].Enabled = true;
        e.Item.Cells[25].Enabled = true;
        txtPart.Enabled = true;
        btnQuery.Enabled = true;
        btnSA.Enabled = true;
        panel2.Enabled = true;
    }
    protected void dgPart_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strPart = txtPart.Text.ToUpper();
        dgPart.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strPart);
        e.Item.Cells[24].Enabled = true;
        e.Item.Cells[25].Enabled = true;
        panel2.Enabled = false;
        txtPart.Enabled = true;
        btnQuery.Enabled = true;
        btnSA.Enabled = true;
        
    }
    protected void dgPart_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        txtPart.Enabled = false;
        Label lbPartNo = (Label)e.Item.FindControl("lbPartNo");
        panel2.Visible = true;
        string ckbvalue = checkbox();
        string ddlEModel_Type = "";
        string txtECust_Item = "";
        string txtESaNo = "";
        string txtEBackNum = "";
        string txtEMfgCountry = "";
        string txtETOpti = "";
        string txtEHw = "";
        string txtESw = "";
        string txtEPicassoDef = "";
        string txtEQty = "";
        string txtEMrp = "";
        string txtEEan = "";
        string txtEMarketName = "";
        string txtEColor = "";
        string txtECommodity = "";
        string ddlELabelType = "";
        string txtERdlPath = "";
        string txtEContent = "";
        string txtEE2pReffile = "";
        int qty;

        if ((((DropDownList)e.Item.FindControl("ddlEModel_Type")).SelectedValue.ToString().Equals("")))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種類型..');</script>");
            return;
            //e.Item.FindControl("ddlFModel_Type").Focus();
        }
        else
            ddlEModel_Type = ((DropDownList)e.Item.FindControl("ddlEModel_Type")).SelectedValue.ToString();
        if ((!((TextBox)e.Item.FindControl("txtECust_Item")).Text.Trim().ToString().Equals("")))
            txtECust_Item = ((TextBox)e.Item.FindControl("txtECust_Item")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtESaNo")).Text.Trim().ToString().Equals("")))
            txtESaNo = ((TextBox)e.Item.FindControl("txtESaNo")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEBackNum")).Text.Trim().ToString().Equals("")))
            txtEBackNum = ((TextBox)e.Item.FindControl("txtEBackNum")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEMfgCountry")).Text.Trim().ToString().Equals("")))
            txtEMfgCountry = ((TextBox)e.Item.FindControl("txtEMfgCountry")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtETOpti")).Text.Trim().ToString().Equals("")))
            txtETOpti = ((TextBox)e.Item.FindControl("txtETOpti")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEPicassoDef")).Text.Trim().ToString().Equals("")))
            txtEPicassoDef = ((TextBox)e.Item.FindControl("txtEPicassoDef")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEMrp")).Text.Trim().ToString().Equals("")))
            txtEMrp = ((TextBox)e.Item.FindControl("txtEMrp")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEEan")).Text.Trim().ToString().Equals("")))
            txtEEan = ((TextBox)e.Item.FindControl("txtEEan")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEMarketName")).Text.Trim().ToString().Equals("")))
            txtEMarketName = ((TextBox)e.Item.FindControl("txtEMarketName")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEColor")).Text.Trim().ToString().Equals("")))
            txtEColor = ((TextBox)e.Item.FindControl("txtEColor")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtECommodity")).Text.Trim().ToString().Equals("")))
            txtECommodity = ((TextBox)e.Item.FindControl("txtECommodity")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtERdlPath")).Text.Trim().ToString().Equals("")))
            txtERdlPath = ((TextBox)e.Item.FindControl("txtERdlPath")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEContent")).Text.Trim().ToString().Equals("")))
            txtEContent = ((TextBox)e.Item.FindControl("txtEContent")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEE2pReffile")).Text.Trim().ToString().Equals("")))
            txtEE2pReffile = ((TextBox)e.Item.FindControl("txtEE2pReffile")).Text.Trim().ToString();

        if ((((DropDownList)e.Item.FindControl("ddlELabelType")).SelectedValue.ToString().Equals("")))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇Label類型..');</script>");
            return;
            //e.Item.FindControl("ddlFLabelType").Focus();
        }
        else
            ddlELabelType = ((DropDownList)e.Item.FindControl("ddlELabelType")).SelectedValue.ToString();

        if (((TextBox)e.Item.FindControl("txtEHw")).Text.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入HW_Ver..');</script>");
            return;
            //e.Item.FindControl("txtFHw").Focus();
        }
        else
            txtEHw = ((TextBox)e.Item.FindControl("txtEHw")).Text.ToUpper();
        if (((TextBox)e.Item.FindControl("txtESw")).Text.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入SW_Ver..');</script>");
            return;
            //e.Item.FindControl("txtFSw").Focus();
        }
        else
            txtESw = ((TextBox)e.Item.FindControl("txtESw")).Text.ToUpper();
        if (((TextBox)e.Item.FindControl("txtEQty")).Text.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入每箱裝箱數量..');</script>");
            return;
            //e.Item.FindControl("txtFQty").Focus();
        }
        else
        {
            txtEQty = ((TextBox)e.Item.FindControl("txtEQty")).Text.ToUpper();
            try
            {
                string str = txtEQty;
                qty = int.Parse(str);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('裝箱數量必須為整數..');</script>");
                return;
            }
        }
        string sql = "Select * from shp.ros_tch_pn where PPART='" + lbPartNo + "'";
        DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt0.Rows.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該料號不存在,請先新增..');</script>");
            return;
        }
        else
        {
            string strsql = "UPDATE shp.ros_tch_pn SET SA_NO='" + txtESaNo + "',PHONE_MODEL='" + ddlEModel_Type + "',T_OPTI='" + txtETOpti
                + "',HW_VER='" + txtEHw + "',SW_VER='" + txtESw + "',PICASSO_DEF='" + txtEPicassoDef + "',LAST_UPDATED=sysdate,MFG_COUNTRY='" + txtEMfgCountry
                + "',EAN='" + txtEEan + "',color='" + txtEColor + "',qty=" + qty + ",Updated_by='" + ViewState["UserName"] + "',COMMODITY='" + txtECommodity
                + "',CONTENT='" + txtEContent + "',MRP='" + txtEMrp + "',MARKET_NAME='" + txtEMarketName + "',TYPE_CODE='" + ddlEModel_Type + "',BACK_NUM='" + txtEBackNum
                + "',CHECKBOX='" + ckbvalue + "',LABEL_TYPE='" + ddlELabelType + "',E2PREFFILE='" + txtEE2pReffile + "',RDL_PATH='" + txtERdlPath + "' WHERE PPART='" + lbPartNo + "'";
            try
            {
                ClsGlobal.objDataConnect.DataExecute(strsql);
                dgPart.EditItemIndex = -1;
                string strsql4 = "select PPART,NVL(PHONE_MODEL,'') PHONE_MODEL,NVL(T_OPTI,'') T_OPTI,NVL(SA_NO,'') SA_NO,NVL(COLOR,'') COLOR,NVL(MFG_COUNTRY,'') MFG_COUNTRY,NVL(EAN,'') EAN,hw_ver,sw_ver,NVL(picasso_def,'') picasso_def,NVL(BACK_NUM,'') BACK_NUM,NVL(last_updated,'') last_updated,qty,NVL(updated_by,'') updated_by, " +
                "NVL(COMMODITY,'') COMMODITY,NVL(CONTENT,'') CONTENT,NVL(MRP,'') MRP,NVL(MARKET_NAME,'') MARKET_NAME,TYPE_CODE,CHECKBOX,NVL(LABEL_TYPE,'') LABEL_TYPE,NVL(E2PREFFILE,'') E2PREFFILE,NVL(RDL_PATH,'') RDL_PATH FROM shp.ROS_TCH_PN where ppart ='" + lbPartNo + "'";
                DataTable dt4 = ClsGlobal.objDataConnect.DataQuery(strsql4).Tables[0];
                if (dt4.Rows.Count > 0)
                { 
                    dgPart.DataSource = this.GetIDTable(dt4).DefaultView;
                    dgPart.DataBind();
                    string strsql5 = "insert into cmcs_sfc_mit_sa(ppart,phone_model,hw_ver,sw_ver,maket_name,sa_date,sa_flag,updated_by,last_updated,qty) values('"
                        + lbPartNo + "','" + ddlEModel_Type + "','" + txtEHw + "','" + txtESw + "','" + txtEMarketName + "',to_date(date,'yyyy/mm/dd'),'Y','"
                        + ViewState["UserName"] + "',sysdate," + qty + ")";
                    ClsGlobal.objDataConnect.DataExecute(strsql5);
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('新增至數據庫異常..');</script>");
                return;
            }
        }
    }

    protected void dgPart_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string strPart = txtPart.Text.ToUpper();
        dgPart.CurrentPageIndex = e.NewPageIndex;
        BindData(strPart); 
    }

    protected void dgPart_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string strsql1 = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
        ViewState["UserName"] = dt1.Rows[0]["USERNAME"].ToString();

        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgPart.ShowFooter = true;
            string strsql2 = "select PPART,NVL(PHONE_MODEL,'') PHONE_MODEL,NVL(T_OPTI,'') T_OPTI,NVL(SA_NO,'') SA_NO,NVL(COLOR,'') COLOR,NVL(MFG_COUNTRY,'') MFG_COUNTRY,NVL(EAN,'') EAN,hw_ver,sw_ver,NVL(picasso_def,'') picasso_def,NVL(BACK_NUM,'') BACK_NUM,NVL(last_updated,'') last_updated,qty,NVL(updated_by,'') updated_by, " +
                "NVL(COMMODITY,'') COMMODITY,NVL(CONTENT,'') CONTENT,NVL(MRP,'') MRP,NVL(MARKET_NAME,'') MARKET_NAME,TYPE_CODE,CHECKBOX,NVL(LABEL_TYPE,'') LABEL_TYPE,NVL(E2PREFFILE,'') E2PREFFILE,NVL(RDL_PATH,'') RDL_PATH FROM shp.ROS_TCH_PN where ppart = '" + this.txtPart.Text.Trim().ToUpper() + "'";
            DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
            if (dt2.Rows.Count > 0)
            { 
                dgPart.DataSource = this.GetIDTable(dt2).DefaultView;
                dgPart.DataBind();
                
            }
            this.dgPart.Columns[24].Visible = false;
            this.dgPart.Columns[25].Visible = false;
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgPart.ShowFooter = false;
            panel2.Enabled = false; ;
            string strsql3 = "select PPART,NVL(PHONE_MODEL,'') PHONE_MODEL,NVL(T_OPTI,'') T_OPTI,NVL(SA_NO,'') SA_NO,NVL(COLOR,'') COLOR,NVL(MFG_COUNTRY,'') MFG_COUNTRY,NVL(EAN,'') EAN,hw_ver,sw_ver,NVL(picasso_def,'') picasso_def,NVL(BACK_NUM,'') BACK_NUM,NVL(last_updated,'') last_updated,qty,NVL(updated_by,'') updated_by, " +
                "NVL(COMMODITY,'') COMMODITY,NVL(CONTENT,'') CONTENT,NVL(MRP,'') MRP,NVL(MARKET_NAME,'') MARKET_NAME,TYPE_CODE,CHECKBOX,NVL(LABEL_TYPE,'') LABEL_TYPE,NVL(E2PREFFILE,'') E2PREFFILE,NVL(RDL_PATH,'') RDL_PATH FROM shp.ROS_TCH_PN where ppart ='" + this.txtPart.Text.Trim().ToUpper() + "'";
            DataTable dt3 = ClsGlobal.objDataConnect.DataQuery(strsql3).Tables[0];
            if (dt3.Rows.Count > 0)
            { 
                dgPart.DataSource = this.GetIDTable(dt3).DefaultView;
                dgPart.DataBind();
            }

            this.dgPart.Columns[24].Visible = true;
            this.dgPart.Columns[25].Visible = true;
        }
        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgPart.ShowFooter = true;
            panel2.Visible = true;
            string ckbvalue = checkbox();
            string txtFPartNo="";
            string ddlFModel_Type="";
            string txtFCust_Item="";
            string txtFSaNo="";
            string txtFBackNum="";
            string txtFMfgCountry="";
            string txtFTOpti="";
            string txtFHw="";
            string txtFSw="";
            string txtFPicassoDef="";
            string txtFQty="";
            string txtFMrp="";
            string txtFEan="";
            string txtFMarketName="";
            string txtFColor="";
            string txtFCommodity="";
            string ddlFLabelType="";
            string txtFRdlPath="";
            string txtFContent="";
            string txtFE2pReffile="";
            int qty ;

            if ((((TextBox)e.Item.FindControl("txtFPartNo")).Text.ToUpper().Equals("")))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入料號..');</script>");
                return; 
                //e.Item.FindControl("txtFPartNo").Focus();
            }
            else
                txtFPartNo = ((TextBox)e.Item.FindControl("txtFPartNo")).Text.ToUpper();
            if ((((DropDownList)e.Item.FindControl("ddlFModel_Type")).SelectedValue.ToString().Equals("")))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種類型..');</script>");
                return;
                //e.Item.FindControl("ddlFModel_Type").Focus();
            }
            else
                ddlFModel_Type = ((DropDownList)e.Item.FindControl("ddlFModel_Type")).SelectedValue.ToString();
            if ((!((TextBox)e.Item.FindControl("txtFCust_Item")).Text.Trim().ToString().Equals("")))
                txtFCust_Item = ((TextBox)e.Item.FindControl("txtFCust_Item")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFSaNo")).Text.Trim().ToString().Equals("")))
                txtFSaNo = ((TextBox)e.Item.FindControl("txtFSaNo")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFBackNum")).Text.Trim().ToString().Equals("")))
                txtFBackNum = ((TextBox)e.Item.FindControl("txtFBackNum")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFMfgCountry")).Text.Trim().ToString().Equals("")))
                txtFMfgCountry = ((TextBox)e.Item.FindControl("txtFMfgCountry")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFTOpti")).Text.Trim().ToString().Equals("")))
                txtFTOpti = ((TextBox)e.Item.FindControl("txtFTOpti")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFPicassoDef")).Text.Trim().ToString().Equals("")))
                txtFPicassoDef = ((TextBox)e.Item.FindControl("txtFPicassoDef")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFMrp")).Text.Trim().ToString().Equals("")))
                txtFMrp = ((TextBox)e.Item.FindControl("txtFMrp")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFEan")).Text.Trim().ToString().Equals("")))
                txtFEan = ((TextBox)e.Item.FindControl("txtFEan")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFMarketName")).Text.Trim().ToString().Equals("")))
                txtFMarketName = ((TextBox)e.Item.FindControl("txtFMarketName")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFColor")).Text.Trim().ToString().Equals("")))
                txtFColor = ((TextBox)e.Item.FindControl("txtFColor")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFCommodity")).Text.Trim().ToString().Equals("")))
                txtFCommodity = ((TextBox)e.Item.FindControl("txtFCommodity")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFRdlPath")).Text.Trim().ToString().Equals("")))
                txtFRdlPath = ((TextBox)e.Item.FindControl("txtFRdlPath")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFContent")).Text.Trim().ToString().Equals("")))
                txtFContent = ((TextBox)e.Item.FindControl("txtFContent")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFE2pReffile")).Text.Trim().ToString().Equals("")))
                txtFE2pReffile = ((TextBox)e.Item.FindControl("txtFE2pReffile")).Text.Trim().ToString();

            if ((((DropDownList)e.Item.FindControl("ddlFLabelType")).SelectedValue.ToString().Equals("")))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇Label類型..');</script>");
                return;
                //e.Item.FindControl("ddlFLabelType").Focus();
            }
            else
                ddlFLabelType = ((DropDownList)e.Item.FindControl("ddlFLabelType")).SelectedValue.ToString();

            if (((TextBox)e.Item.FindControl("txtFHw")).Text.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入HW_Ver..');</script>");
                return;
                //e.Item.FindControl("txtFHw").Focus();
            }
            else
                txtFHw = ((TextBox)e.Item.FindControl("txtFHw")).Text.ToUpper();
            if (((TextBox)e.Item.FindControl("txtFSw")).Text.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入SW_Ver..');</script>");
                return;
                //e.Item.FindControl("txtFSw").Focus();
            }
            else
                txtFSw = ((TextBox)e.Item.FindControl("txtFSw")).Text.ToUpper();
            if (((TextBox)e.Item.FindControl("txtFQty")).Text.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入每箱裝箱數量..');</script>");
                return;
                //e.Item.FindControl("txtFQty").Focus();
            }
            else
            {
                txtFQty = ((TextBox)e.Item.FindControl("txtFQty")).Text.ToUpper();
                try
                {
                    string str = txtFQty;
                    qty = int.Parse(str);
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('裝箱數量必須為整數..');</script>");
                    return;
                }
            }
            string sql = "Select * from shp.ros_tch_pn where PPART='" + txtFPartNo + "'";
            DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
            if (dt0.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該料號已經存在,請選擇更新..');</script>");
                return;
            }
            else
            {
                string strsql = "INSERT INTO shp.ros_tch_pn (PPART,SA_NO,PHONE_MODEL,T_OPTI,HW_VER,SW_VER,PICASSO_DEF,LAST_UPDATED,MFG_COUNTRY,EAN,color,qty,Updated_by,"
                    + "COMMODITY,CONTENT,MRP,MARKET_NAME,TYPE_CODE,BACK_NUM,CHECKBOX,LABEL_TYPE,E2PREFFILE,RDL_PATH) values('" +
                    txtFPartNo + "','" + txtFSaNo + "','" + ddlFModel_Type + "','" + txtFTOpti + "','" + txtFHw + "','"
                    + txtFSw + "','" + txtFPicassoDef + "',sysdate,'" + txtFMfgCountry + "','" + txtFEan + "','" + txtFColor + "'," + qty + ",'" + ViewState["UserName"]
                    + "','" + txtFCommodity + "','" + txtFContent + "','" + txtFMrp + "','" + txtFMarketName + "','" + ddlFModel_Type + "','" + txtFBackNum + "','" + ckbvalue + "','"
                    + ddlFLabelType + "','" + txtFE2pReffile + "','" + txtFRdlPath + "')";
                try
                {
                    ClsGlobal.objDataConnect.DataExecute(strsql);
                    dgPart.EditItemIndex = -1;
                    string strsql4 = "select PPART,NVL(PHONE_MODEL,'') PHONE_MODEL,NVL(T_OPTI,'') T_OPTI,NVL(SA_NO,'') SA_NO,NVL(COLOR,'') COLOR,NVL(MFG_COUNTRY,'') MFG_COUNTRY,NVL(EAN,'') EAN,hw_ver,sw_ver,NVL(picasso_def,'') picasso_def,NVL(BACK_NUM,'') BACK_NUM,NVL(last_updated,'') last_updated,qty,NVL(updated_by,'') updated_by, " +
                    "NVL(COMMODITY,'') COMMODITY,NVL(CONTENT,'') CONTENT,NVL(MRP,'') MRP,NVL(MARKET_NAME,'') MARKET_NAME,TYPE_CODE,CHECKBOX,NVL(LABEL_TYPE,'') LABEL_TYPE,NVL(E2PREFFILE,'') E2PREFFILE,NVL(RDL_PATH,'') RDL_PATH FROM shp.ROS_TCH_PN where ppart ='" + txtFPartNo + "'";
                    DataTable dt4 = ClsGlobal.objDataConnect.DataQuery(strsql4).Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        dgPart.DataSource = this.GetIDTable(dt4).DefaultView;
                        dgPart.DataBind();
                        string strsql5 = "insert into cmcs_sfc_mit_sa(ppart,phone_model,hw_ver,sw_ver,maket_name,sa_date,sa_flag,updated_by,last_updated,qty) values('"
                            + txtFPartNo + "','" + ddlFModel_Type + "','" + txtFHw + "','" + txtFSw + "','" + txtFMarketName + "',to_date(date,'yyyy/mm/dd'),'Y','"
                            + ViewState["UserName"] + "',sysdate,'" + txtFQty + "')";
                        ClsGlobal.objDataConnect.DataExecute(strsql5);
                    }
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('新增至數據庫異常..');</script>");
                    return;
                }
            }

            this.dgPart.Columns[24].Visible = true;
            this.dgPart.Columns[25].Visible = true;
        }
        //if (e.CommandName == "ItemDelete")
        //{
        //    string strModel = ((Label)e.Item.Cells[1].Controls[1]).Text;
        //    string strcountry = ((Label)e.Item.Cells[2].Controls[1]).Text;
        //    int max = Convert.ToInt16(((Label)e.Item.Cells[3].Controls[1]).Text);
        //    int min = Convert.ToInt16(((Label)e.Item.Cells[4].Controls[1]).Text);
        //    string strqty = ((Label)e.Item.Cells[5].Controls[1]).Text;
        //    string strarea = ((Label)e.Item.Cells[6].Controls[1]).Text;
        //    string strsql = "delete from  SHP.MES_PACK_MEASURES_STANDARD where MODEL_ID='" + strModel + "' and COUNTRY_NAME='" + strcountry + "'";
        //    ClsGlobal.objDataConnect.DataExecute(strsql);
        //    dgMeasure.EditItemIndex = -1;
        //    string strsql1 = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        //    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
        //    if (dt1.Rows.Count > 0)
        //    {
        //        lbcount.Text = "Total:" + dt1.Rows.Count;

        //        dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
        //        dgMeasure.DataBind();
        //    }
        //    else
        //    {
        //        txtModel.Text = "";
        //        string strsql2 = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD ";
        //        DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
        //        lbcount.Text = "Total:" + dt1.Rows.Count;

        //        dgMeasure.DataSource = this.GetIDTable(dt2).DefaultView;
        //        dgMeasure.DataBind();

        //    }
        //}
    }

    private string checkbox()
    {
        string checkboxvalue = "";
        if (ckb1.Checked)
            checkboxvalue += "1";
        else
            checkboxvalue += "0";
        if (ckb2.Checked)
            checkboxvalue += "1";
        else
            checkboxvalue += "0";
        if (ckb3.Checked)
            checkboxvalue += "1";
        else
            checkboxvalue += "0";
        if (ckb4.Checked)
            checkboxvalue += "1";
        else
            checkboxvalue += "0";
        if (ckb5.Checked)
            checkboxvalue += "1";
        else
            checkboxvalue += "0";
        if (ckb6.Checked)
            checkboxvalue += "1";
        else
            checkboxvalue += "0";
        return checkboxvalue;
    }

    private void setcheckbox(string checkboxvalue)
    {
        int i;
        int strLen;
        strLen = checkboxvalue.Trim().Length;
        string strckb1 = checkboxvalue.Substring(0, 1).ToString();
        if (strckb1.Equals("1"))
            ckb1.Checked = true;
        else
            ckb1.Checked = false;
        string strckb2 = checkboxvalue.Substring(1, 1).ToString();
        if (strckb2.Equals("1"))
            ckb2.Checked = true;
        else
            ckb2.Checked = false;
        string strckb3 = checkboxvalue.Substring(2, 1).ToString();
        if (strckb3.Equals("1"))
            ckb3.Checked = true;
        else
            ckb3.Checked = false;
        string strckb4 = checkboxvalue.Substring(3, 1).ToString();
        if (strckb4.Equals("1"))
            ckb4.Checked = true;
        else
            ckb4.Checked = false;
        string strckb5 = checkboxvalue.Substring(4, 1).ToString();
        if (strckb5.Equals("1"))
            ckb5.Checked = true;
        else
            ckb5.Checked = false;
        string strckb6 = checkboxvalue.Substring(5, 1).ToString();
        if (strckb6.Equals("1"))
            ckb6.Checked = true;
        else
            ckb6.Checked = false;    
    }

    protected void dgPart_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgPart.PageSize) * (dgPart.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[25].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?操作不可返回,按確認繼續..');");

        if (e.Item.ItemType == ListItemType.EditItem)
        {           
            panel2.Enabled = true; 
            DropDownList ddlEModel = (DropDownList)e.Item.FindControl("ddlEModel_Type");
            string StrSql1 = "select distinct model from SFC.CMCS_SFC_MODEL order by model";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(StrSql1).Tables[0];
            ddlEModel.DataTextField = "model";
            ddlEModel.DataValueField = "model";
            ddlEModel.DataSource = dt1.DefaultView;
            ddlEModel.DataBind();
            ddlEModel.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "TYPE_CODE"))).Selected = true;


            DropDownList ddlELabelType = (DropDownList)e.Item.FindControl("ddlELabelType");
            string StrSql2 = "select barcode_des from shp.cmcs_sfc_imeiprint_function order by barcode_des";
            DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(StrSql2).Tables[0];
            ddlELabelType.DataTextField = "barcode_des";
            ddlELabelType.DataValueField = "barcode_des";
            ddlELabelType.DataSource = dt2.DefaultView;
            ddlELabelType.DataBind();
            try
            {
                ddlELabelType.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LABEL_TYPE"))).Selected = true;
            }
            catch
            {
                ddlELabelType.Items.Insert(0, "");
            }
            string lbcheckbox = ((Label)e.Item.FindControl("lbCheckbox")).Text.ToString();
            panel2.Visible = true;
            setcheckbox(lbcheckbox);

            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            btnDelete.Enabled = true;
            e.Item.Cells[24].Enabled = true;
            e.Item.Cells[25].Enabled = false;

            txtPart.Enabled = false;
            btnSA.Enabled = false;
            btnQuery.Enabled = false;
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            panel2.Enabled = true;
            ckb1.Checked = false;
            ckb2.Checked = false;
            ckb3.Checked = false;
            ckb4.Checked = false;
            ckb5.Checked = false;  
            ckb6.Checked = false;  
            DropDownList ddlFModel_Type = (DropDownList)e.Item.FindControl("ddlFModel_Type");
            string StrSql3 = "select distinct model from SFC.CMCS_SFC_MODEL order by model";
            DataTable dt3 = ClsGlobal.objDataConnect.DataQuery(StrSql3).Tables[0];
            ddlFModel_Type.DataTextField = "model";
            ddlFModel_Type.DataValueField = "model";
            ddlFModel_Type.DataSource = dt3.DefaultView;
            ddlFModel_Type.DataBind();
            ddlFModel_Type.Items.Insert(0, "");

            DropDownList ddlFLabelType = (DropDownList)e.Item.FindControl("ddlFLabelType");
            string StrSql4 = "select barcode_des from shp.cmcs_sfc_imeiprint_function order by barcode_des";
            DataTable dt4 = ClsGlobal.objDataConnect.DataQuery(StrSql4).Tables[0];
            ddlFLabelType.DataTextField = "barcode_des";
            ddlFLabelType.DataValueField = "barcode_des";
            ddlFLabelType.DataSource = dt4.DefaultView;
            ddlFLabelType.DataBind();
            ddlFLabelType.Items.Insert(0, "");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strPart = txtPart.Text.ToUpper();
        string strsql = "select PPART,NVL(PHONE_MODEL,'') PHONE_MODEL,NVL(T_OPTI,'') T_OPTI,NVL(SA_NO,'') SA_NO,NVL(COLOR,'') COLOR,NVL(MFG_COUNTRY,'') MFG_COUNTRY,NVL(EAN,'') EAN,hw_ver,sw_ver,NVL(picasso_def,'') picasso_def,NVL(BACK_NUM,'') BACK_NUM,NVL(last_updated,'') last_updated,qty,NVL(updated_by,'') updated_by, " +
            "NVL(COMMODITY,'') COMMODITY,NVL(CONTENT,'') CONTENT,NVL(MRP,'') MRP,NVL(MARKET_NAME,'') MARKET_NAME,TYPE_CODE,CHECKBOX,NVL(LABEL_TYPE,' ') LABEL_TYPE,NVL(E2PREFFILE,'') E2PREFFILE,NVL(RDL_PATH,'') RDL_PATH FROM shp.ROS_TCH_PN where ppart =  '" + strPart + "'";
        
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgPart.Visible = true;
            dgPart.ShowFooter = false;
            btnSA.Visible = true; 
            dgPart.DataSource = this.GetIDTable(dt1).DefaultView;
            dgPart.DataBind();
            panel2.Enabled = false;
            string lbcheckbox = dt1.Rows[0]["CHECKBOX"].ToString();
            setcheckbox(lbcheckbox);
        }
        else
        {
            panel2.Visible = false;
            btnSA.Visible = false;
            dgPart.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('找不到資料,請先新增..');</script>");
            return;
        }
    }

    private DataTable GetIDTable(DataTable dt)
    {
        DataColumn col = new DataColumn("新增", Type.GetType("System.Int32"));
        dt.Columns.Add(col);
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (0 == i)
                dt.Rows[0][col] = 1;
            else
                dt.Rows[i][col] = Convert.ToInt32(dt.Rows[i - 1][col]) + 1;
        }
        return dt;
    }
    protected void btnSA_Click(object sender, EventArgs e)
    {
        string strPart = txtPart.Text.ToUpper();
        string strsql = "select PPART,PHONE_MODEL,hw_ver,sw_ver,maket_name,sa_date,sa_flag,QTY,LAST_UPDATED From CMCS_SFC_MIT_SA " +
            " where ppart= '" + strPart + "' order by LAST_UPDATED ";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        { 
            dgsa.Visible = true;
            dgsa.ShowFooter = false; 
            dgsa.DataSource = this.GetIDTable(dt1).DefaultView;
            dgsa.DataBind();
        }
        else
        {
            dgsa.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('找不到認證記錄,請先新增..');</script>");
            return;
        }
    }
    protected void dgsa_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string strsql1 = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
        ViewState["UserName"] = dt1.Rows[0]["USERNAME"].ToString();

        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgsa.ShowFooter = true;
            string strPart = txtPart.Text.ToUpper();
            string strsql2 = "select PPART,PHONE_MODEL,hw_ver,sw_ver,maket_name,sa_date,sa_flag,QTY,LAST_UPDATED From CMCS_SFC_MIT_SA " +
                " where ppart= '" + strPart + "' order by LAST_UPDATED "; 
            DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
            if (dt2.Rows.Count > 0)
            {
                dgsa.DataSource = this.GetIDTable(dt2).DefaultView;
                dgsa.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgsa.ShowFooter = false;
            string strPart = txtPart.Text.ToUpper();
            string strsql3 = "select PPART,PHONE_MODEL,hw_ver,sw_ver,maket_name,sa_date,sa_flag,QTY,LAST_UPDATED From CMCS_SFC_MIT_SA " +
                " where ppart= '" + strPart + "' order by LAST_UPDATED ";
            DataTable dt3 = ClsGlobal.objDataConnect.DataQuery(strsql3).Tables[0];
            if (dt3.Rows.Count > 0)
            {
                dgsa.DataSource = this.GetIDTable(dt3).DefaultView;
                dgsa.DataBind();
            }
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgsa.ShowFooter = true;
            string lbppart = ((Label)e.Item.FindControl("lbFPartNo")).Text.ToString();
            string lbsadate = ((Label)e.Item.FindControl("lbFSadate")).Text.ToString();
            string lbsapass = ((Label)e.Item.FindControl("lbFSapass")).Text.ToString(); 
            string txtFCustomerItem = "";
            string txtFHw = "";
            string txtFSw = "";
            string txtFMarketName = "";
            string txtFQty = "";
            int qty;

            if ((((TextBox)e.Item.FindControl("txtFCustomerItem")).Text.ToUpper().Equals("")))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入客戶料號..');</script>");
                return;
            }
            else
                txtFCustomerItem = ((TextBox)e.Item.FindControl("txtFCustomerItem")).Text.ToUpper();

            if (((TextBox)e.Item.FindControl("txtFHw")).Text.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入HW_Ver..');</script>");
                return;
            }
            else
                txtFHw = ((TextBox)e.Item.FindControl("txtFHw")).Text.ToUpper();
            if (((TextBox)e.Item.FindControl("txtFSw")).Text.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入SW_Ver..');</script>");
                return;
            }
            else
                txtFSw = ((TextBox)e.Item.FindControl("txtFSw")).Text.ToUpper();
            if ((!((TextBox)e.Item.FindControl("txtFMarketName")).Text.Trim().ToString().Equals("")))
                txtFMarketName = ((TextBox)e.Item.FindControl("txtFMarketName")).Text.Trim().ToString();
            if (((TextBox)e.Item.FindControl("txtFQty")).Text.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入每箱裝箱數量..');</script>");
                return;
            }
            else
            {
                txtFQty = ((TextBox)e.Item.FindControl("txtFQty")).Text.ToUpper();
                try
                {
                    string str = txtFQty;
                    qty = int.Parse(str);
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('裝箱數量必須為整數..');</script>");
                    return;
                }
            } 
            string strsql = "INSERT INTO CMCS_SFC_MIT_SA (PPART,PHONE_MODEL,HW_VER,SW_VER,MAKET_NAME,SA_DATE,SA_FLAG,UPDATED_BY,LAST_UPDATED,QTY)"
                + "values('" + lbppart + "','" + txtFCustomerItem + "','" + txtFHw + "','" + txtFSw + "','"
                + txtFMarketName + "',to_date('" + lbsadate + "','yyyy/mm/dd'),'Y','" + ViewState["UserName"]
                + "',sysdate," + qty + ")";
            try
            {
                ClsGlobal.objDataConnect.DataExecute(strsql);
                dgsa.EditItemIndex = -1;
                string strsql4 = "select PPART,PHONE_MODEL,hw_ver,sw_ver,maket_name,sa_date,sa_flag,QTY,LAST_UPDATED From CMCS_SFC_MIT_SA " +
                    " where ppart= '" + lbppart + "' order by LAST_UPDATED ";
                DataTable dt4 = ClsGlobal.objDataConnect.DataQuery(strsql4).Tables[0];
                if (dt4.Rows.Count > 0)
                {
                    dgsa.DataSource = this.GetIDTable(dt4).DefaultView;
                    dgsa.DataBind();
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('新增至數據庫異常..');</script>");
                return;
            } 
        }
    }

    protected void dgsa_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            string strPart = txtPart.Text.ToUpper();
            string strsql = "select PPART,PHONE_MODEL,hw_ver,sw_ver,maket_name,sa_date,sa_flag,QTY,LAST_UPDATED From CMCS_SFC_MIT_SA " +
                " where ppart= '" + strPart + "' order by LAST_UPDATED ";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                dgsa.Visible = true;
                Label lbFPartNo = (Label)e.Item.FindControl("lbFPartNo");
                lbFPartNo.Text = strPart; 
                Label lbFSadate = (Label)e.Item.FindControl("lbFSadate");
                lbFSadate.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mi:ss"); 
                Label lbFSapass = (Label)e.Item.FindControl("lbFSapass");
                lbFSapass.Text = "Y";  
            }
            else
            {
                dgsa.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('找不到認證記錄,請先新增..');</script>");
                return;
            }
        }
    }
}
