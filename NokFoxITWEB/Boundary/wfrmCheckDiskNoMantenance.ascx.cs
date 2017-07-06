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
using System.Data.OracleClient;

public partial class Boundary_wfrmCheckDiskNoMantenance : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            string strpart = txtPart.Text.ToUpper().Trim().ToString();
            if(strpart.Length>0)
                BindData(strpart);
        }
    }

    private void MultiLanguage()
    {
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
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

    private void BindData(string apart)
    {
        dgpart.Visible = true;
        string strsql = "SELECT * FROM SHP.APART_KEYPART_LINK where APART = '" + apart.ToUpper() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgpart.DataSource = this.GetIDTable(dt1).DefaultView;
            dgpart.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該料號的key part維護信息...');</script>");
            return;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "SELECT * FROM SHP.APART_KEYPART_LINK where APART ='" + this.txtPart.Text.Trim().ToUpper() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgpart.DataSource = this.GetIDTable(dt1).DefaultView;
            dgpart.DataBind();
        }
        else
        {
            dgpart.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該料號的key part維護信息...');</script>");
            return;
        }
    }

    protected void dgpart_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strpart = txtPart.Text.ToUpper().Trim().ToString();
        dgpart.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strpart);
    }

    protected void dgpart_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string strsql0 = "SELECT LOGINNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
        DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
        ViewState["UserName"] = dt0.Rows[0]["LOGINNAME"].ToString();

        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgpart.ShowFooter = true;
            string strsql = "SELECT * FROM SHP.APART_KEYPART_LINK where APART= '" + this.txtPart.Text.Trim().ToUpper() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgpart.DataSource = this.GetIDTable(dt1).DefaultView;
                dgpart.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgpart.ShowFooter = false;
            string strsql = "SELECT * FROM SHP.APART_KEYPART_LINK where APART= '" + this.txtPart.Text.Trim().ToUpper() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgpart.DataSource = this.GetIDTable(dt1).DefaultView;
                dgpart.DataBind();
            }
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgpart.ShowFooter = false; 
            string strFapart;
            string strFkeypart; 

            if (((TextBox)e.Item.FindControl("txtFapart")).Text.Trim().ToUpper() == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('A Part不能為空...');</script>");
                return;
            }
            else
                strFapart = ((TextBox)e.Item.FindControl("txtFapart")).Text.Trim().ToUpper();

            if (((TextBox)e.Item.FindControl("txtFkeypart")).Text.Trim().ToUpper() == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Key Part不能為空...');</script>");
                return;
            }
            else
                strFkeypart = ((TextBox)e.Item.FindControl("txtFkeypart")).Text.Trim().ToUpper();

            string csql = "SELECT * FROM SHP.APART_KEYPART_LINK where apart='" + strFapart + "' and keypart='" + strFkeypart + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(csql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('此料號對應設置已存在,請先確認...');</script>");
                return;
            }
            else
            {
                string strsql = "Insert into SHP.APART_KEYPART_LINK(APART,KEYPART,EMPNO,CREATEDATE) values('" +
                    strFapart + "','" + strFkeypart + "','" + ViewState["UserName"] + "',sysdate)";
                ClsGlobal.objDataConnect.DataExecute(strsql);
                dgpart.EditItemIndex = -1;
                string strsql2 = "SELECT * FROM SHP.APART_KEYPART_LINK where APART= '" + this.txtPart.Text.Trim().ToUpper() + "'";
                DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    lbcount.Text = "Total:" + dt2.Rows.Count;

                    dgpart.DataSource = this.GetIDTable(dt2).DefaultView;
                    dgpart.DataBind();
                }
            }
        }

        if (e.CommandName == "ItemDelete")
        {
            string strapart = ((Label)e.Item.Cells[1].Controls[1]).Text; 
            string strkeypart = ((Label)e.Item.Cells[2].Controls[1]).Text;

            string strsql = "delete from SHP.APART_KEYPART_LINK where apart='" + strapart + "' and keypart='" + strkeypart + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgpart.EditItemIndex = -1;
            string strsql1 = "SELECT * FROM SHP.APART_KEYPART_LINK where APART= '" + this.txtPart.Text.Trim().ToUpper() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgpart.DataSource = this.GetIDTable(dt1).DefaultView;
                dgpart.DataBind();
            }
            else
            {
                txtPart.Text = "";
                string strsql2 = "SELECT * FROM SHP.APART_KEYPART_LINK ";
                DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                lbcount.Text = "Total:" + dt2.Rows.Count;

                dgpart.DataSource = this.GetIDTable(dt2).DefaultView;
                dgpart.DataBind();
            }
        }

    }
    protected void dgpart_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgpart.PageSize) * (dgpart.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[6].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?');"); 
    }
    protected void dgpart_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgpart.CurrentPageIndex = e.NewPageIndex;
        string strsql = "SELECT * FROM SHP.APART_KEYPART_LINK where APART = '" + this.txtPart.Text.Trim().ToUpper() + "'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        dgpart.DataSource = this.GetIDTable(dt).DefaultView;
        dgpart.DataBind();
        lbcount.Text = "Current Page:" + (dgpart.CurrentPageIndex + 1).ToString() + "/" + dgpart.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
    protected void dgpart_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strHapart = ((Label)e.Item.Cells[1].Controls[1]).Text;
        string strQkeypart = ((Label)e.Item.Cells[2].Controls[1]).Text;
        string strHkeypart = ((TextBox)e.Item.FindControl("txtkeypart")).Text.Trim().ToUpper();
        string strHempno = ((TextBox)e.Item.FindControl("txtempno")).Text.Trim().ToUpper();

        try
        {
            string strsql = "update SHP.APART_KEYPART_LINK set KEYPART='" + strHkeypart + "',EMPNO='" + strHempno + "'  where  APART='" + strHapart + "' AND KEYPART='" + strQkeypart + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgpart.EditItemIndex = -1;
            string strSql = "SELECT * FROM SHP.APART_KEYPART_LINK where APART ='" + strHapart + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            dgpart.DataSource = this.GetIDTable(dt).DefaultView;
            dgpart.DataBind();
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('更新數據有誤...');</script>");
            return;
        }
        dgpart.EditItemIndex = -1;
        BindData(strHapart); 
    }

    protected void dgpart_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strpart = txtPart.Text.ToUpper().Trim().ToString();
        dgpart.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strpart); 
    }
}
