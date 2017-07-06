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

public partial class Boundary_wfrmSJUGMantenance : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            string strsql = " select * from sfc.cdma_millau_sjug ORDER BY SEQUENCES_ID ";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            dgsjug.Visible = true;
            dgsjug.DataSource = this.GetIDTable(dt).DefaultView;
            dgsjug.DataBind();
        }
    }

    private void MultiLanguage()
    {
        lblPart.Text = (String)GetGlobalResourceObject("SFCQuery", "PartNo");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    private void BindData(string part)
    {
        dgsjug.Visible = true; 
        string strPart = txtPart.Text.ToUpper();
        string strsql = "SELECT * FROM SFC.CDMA_MILLAU_SJUG Where Spart='" + strPart + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgsjug.DataSource = this.GetIDTable(dt1).DefaultView;
            dgsjug.DataBind();  
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('找不到資料,請先新增..！！');</script>");
            return;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strPart = txtPart.Text.ToUpper().Trim();
        string strsql = "SELECT * FROM SFC.CDMA_MILLAU_SJUG Where Spart='" + strPart + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgsjug.Visible = true;
            dgsjug.ShowFooter = false;
            dgsjug.DataSource = this.GetIDTable(dt1).DefaultView;
            dgsjug.DataBind(); 
        }
        else
        { 
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

    protected void dgsjug_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strPart = txtPart.Text.ToUpper();
        dgsjug.EditItemIndex = e.Item.ItemIndex;
        if (strPart.Equals(""))
        {
            string strsql = " select * from sfc.cdma_millau_sjug ORDER BY SEQUENCES_ID ";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            dgsjug.Visible = true;
            dgsjug.DataSource = this.GetIDTable(dt).DefaultView;
            dgsjug.DataBind();
        }
        else
            BindData(strPart);
        e.Item.Cells[11].Enabled = true;
        e.Item.Cells[12].Enabled = true;
        dgsjug.Enabled = true;
        btnQuery.Enabled = true; 
    } 
    protected void dgsjug_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgsjug.ShowFooter = true;
            string strPart = txtPart.Text.ToUpper();
            if (strPart.Equals(""))
            {
                string strsql = " select * from sfc.cdma_millau_sjug ORDER BY SEQUENCES_ID ";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                dgsjug.Visible = true;
                dgsjug.DataSource = this.GetIDTable(dt).DefaultView;
                dgsjug.DataBind();
            }
            else
                BindData(strPart);
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgsjug.ShowFooter = false;
            string strPart = txtPart.Text.ToUpper();
            string strsql3 = "select * from sfc.cdma_millau_sjug ORDER BY SEQUENCES_ID where spart='" + strPart + "'";
            DataTable dt3 = ClsGlobal.objDataConnect.DataQuery(strsql3).Tables[0];
            if (dt3.Rows.Count > 0)
            {
                dgsjug.DataSource = this.GetIDTable(dt3).DefaultView;
                dgsjug.DataBind();
            }
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgsjug.ShowFooter = true;
            string txtFSPartNo="";
            string txtFAPartNo="";
            string txtFPPartNo="";
            string txtFSJUG="";
            string txtFColor="";
            string txtFCustomer="";
            string txtFPidsuffix="";
            string txtFSw="";
            string txtFFlex="";

            if ((!((TextBox)e.Item.FindControl("txtFSPartNo")).Text.Trim().ToString().Equals("")))
                txtFSPartNo = ((TextBox)e.Item.FindControl("txtFSPartNo")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFAPartNo")).Text.Trim().ToString().Equals("")))
                txtFAPartNo = ((TextBox)e.Item.FindControl("txtFAPartNo")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFPPartNo")).Text.Trim().ToString().Equals("")))
                txtFPPartNo = ((TextBox)e.Item.FindControl("txtFPPartNo")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFSJUG")).Text.Trim().ToString().Equals("")))
                txtFSJUG = ((TextBox)e.Item.FindControl("txtFSJUG")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFColor")).Text.Trim().ToString().Equals("")))
                txtFColor = ((TextBox)e.Item.FindControl("txtFColor")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFCustomer")).Text.Trim().ToString().Equals("")))
                txtFCustomer = ((TextBox)e.Item.FindControl("txtFCustomer")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFPidsuffix")).Text.Trim().ToString().Equals("")))
                txtFPidsuffix = ((TextBox)e.Item.FindControl("txtFPidsuffix")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFSw")).Text.Trim().ToString().Equals("")))
                txtFSw = ((TextBox)e.Item.FindControl("txtFSw")).Text.Trim().ToString();
            if ((!((TextBox)e.Item.FindControl("txtFFlex")).Text.Trim().ToString().Equals("")))
                txtFFlex = ((TextBox)e.Item.FindControl("txtFFlex")).Text.Trim().ToString();
           
            string strsql = "INSERT INTO SFC.CDMA_MILLAU_SJUG (SJUG,COLOR,PPART,APART,SPART,SW_VERSION,FLEX_VERSION,CUSTOMER,ASSGIN,SEQUENCES_ID,CREATE_DATE)"
                + "values('" + txtFSJUG + "','" + txtFColor + "','" + txtFPPartNo + "','" + txtFAPartNo + "','"
                + txtFSPartNo + "','" + txtFSw + "','" + txtFFlex + "','" + txtFCustomer + "','" + txtFPidsuffix 
                + "',SFC.CDMA_MILLAU_SJUG_S.NEXTVAL,SYSDATE)";
            try
            {
                ClsGlobal.objDataConnect.DataExecute(strsql);
                dgsjug.EditItemIndex = -1;
                string strsql4 = "select * from sfc.cdma_millau_sjug ORDER BY SEQUENCES_ID where spart='" + txtFSPartNo + "'";
                DataTable dt4 = ClsGlobal.objDataConnect.DataQuery(strsql4).Tables[0];
                if (dt4.Rows.Count > 0)
                {
                    dgsjug.DataSource = this.GetIDTable(dt4).DefaultView;
                    dgsjug.DataBind();
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('新增至數據庫異常..');</script>");
                return;
            }
        }
    }

    protected void dgsjug_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string strPart = txtPart.Text.ToUpper().Trim();
        dgsjug.CurrentPageIndex = e.NewPageIndex;
        if (strPart.Equals(""))
        {
            string strsql = " select * from sfc.cdma_millau_sjug ORDER BY SEQUENCES_ID ";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            dgsjug.Visible = true;
            dgsjug.DataSource = this.GetIDTable(dt).DefaultView;
            dgsjug.DataBind();
        }
        else
            BindData(strPart); 
    }

    protected void dgsjug_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgsjug.PageSize) * (dgsjug.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[12].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?操作不可返回,按確認繼續..');");

        if (e.Item.ItemType == ListItemType.EditItem)
        { 
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            btnDelete.Enabled = true;
            e.Item.Cells[11].Enabled = true;
            e.Item.Cells[12].Enabled = false;

            txtPart.Enabled = false; 
            btnQuery.Enabled = false;
        } 
    }
   
    protected void dgsjug_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        txtPart.Enabled = false;
        string strSeqid = ((Label)e.Item.FindControl("lbSequencesID")).Text.ToString();
        int seqid=int.Parse(strSeqid);
        string txtESPartNo="";
        string txtEAPartNo="";
        string txtEPPartNo="";
        string txtESJUG="";
        string txtEColor="";
        string txtECustomer="";
        string txtEPidsuffix="";
        string txtESw="";
        string txtEFlex="";

        if ((!((TextBox)e.Item.FindControl("txtESPartNo")).Text.Trim().ToString().Equals("")))
            txtESPartNo = ((TextBox)e.Item.FindControl("txtESPartNo")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEAPartNo")).Text.Trim().ToString().Equals("")))
            txtEAPartNo = ((TextBox)e.Item.FindControl("txtEAPartNo")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEPPartNo")).Text.Trim().ToString().Equals("")))
            txtEPPartNo = ((TextBox)e.Item.FindControl("txtEPPartNo")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtESJUG")).Text.Trim().ToString().Equals("")))
            txtESJUG = ((TextBox)e.Item.FindControl("txtESJUG")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEColor")).Text.Trim().ToString().Equals("")))
            txtEColor = ((TextBox)e.Item.FindControl("txtEColor")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtECustomer")).Text.Trim().ToString().Equals("")))
            txtECustomer = ((TextBox)e.Item.FindControl("txtECustomer")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEPidsuffix")).Text.Trim().ToString().Equals("")))
            txtEPidsuffix = ((TextBox)e.Item.FindControl("txtEPidsuffix")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtESw")).Text.Trim().ToString().Equals("")))
            txtESw = ((TextBox)e.Item.FindControl("txtESw")).Text.Trim().ToString();
        if ((!((TextBox)e.Item.FindControl("txtEFlex")).Text.Trim().ToString().Equals("")))
            txtEFlex = ((TextBox)e.Item.FindControl("txtEFlex")).Text.Trim().ToString();

        string strsql = "UPDATE sfc.cdma_millau_sjug SET spart='" + txtESPartNo + "',apart='" + txtEAPartNo + "',ppart='" + txtEPPartNo
            + "',sjug='" + txtESJUG + "',color='" + txtEColor + "',customer='" + txtECustomer + "',assgin='" + txtEPidsuffix
            + "',sw_version='" + txtESw + "',flex_version='" + txtEFlex + "' WHERE sequences_id=" + seqid;
        try
        {
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgsjug.EditItemIndex = -1;
            string strsql4 = "SELECT * FROM SFC.CDMA_MILLAU_SJUG Where Spart='" + txtESPartNo + "'";
            DataTable dt4 = ClsGlobal.objDataConnect.DataQuery(strsql4).Tables[0];
            if (dt4.Rows.Count > 0)
            {
                dgsjug.DataSource = this.GetIDTable(dt4).DefaultView;
                dgsjug.DataBind(); 
            }
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('更新至數據庫異常..');</script>");
            return;
        }
    }
    
    protected void dgsjug_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strPart = txtPart.Text.ToUpper();
        dgsjug.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strPart);
        e.Item.Cells[11].Enabled = true;
        e.Item.Cells[12].Enabled = true; 
        txtPart.Enabled = true;
        btnQuery.Enabled = true; 
    }
}
