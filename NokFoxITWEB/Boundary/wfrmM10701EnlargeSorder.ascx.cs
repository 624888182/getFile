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

public partial class Boundary_wfrmM10701EnlargeSorder : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            MultiLanguage();
            
        }
    }

    private void BindData(string sorder)
    {
        dgSorder.Visible = true;
        string sql = "select SORDER,MODEL,SPART,PRD_TYPE,SEQUENCE_ID,PID_QTY from SHP.CMCS_SFC_SORDER where SORDER = '" + sorder + "'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgSorder.DataSource = dt;
            dgSorder.DataBind();
        }
    }


    private void MultiLanguage()
    {
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        lbSorder.Text = (String)GetGlobalResourceObject("SFCQuery", "Sorder");
        dgSorder.Columns[0].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Sorder");
        dgSorder.Columns[1].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Model");
        dgSorder.Columns[2].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Spart");
        dgSorder.Columns[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Prd_type");
        dgSorder.Columns[4].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Squence_id");
        dgSorder.Columns[5].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "PID_qty");
        dgSorder.Columns[6].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "edit");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        
        string Sorder = tbSorder.Text.Trim().ToUpper();
        if (Sorder.Substring(0,1).Equals("S"))
        {
            string sql = "select * from SHP.CMCS_SFC_SORDER where SORDER = '" + Sorder + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
            if (dt.Rows.Count > 0)
                BindData(Sorder);
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在此工單,請檢查你的輸入!');</script>");
                dgSorder.Visible = false;
                return;
            }
            
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入S工單!');</script>");
            dgSorder.Visible = false;
            return;
        }
    }
    protected void dgSorder_EditCommand(object source, DataGridCommandEventArgs e)
    { 
        string sorder = tbSorder.Text.Trim().ToUpper();
        dgSorder.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(sorder);
    }
    protected void dgSorder_CancelCommand(object source, DataGridCommandEventArgs e)
    { 
        string sorder = tbSorder.Text.Trim().ToUpper();
        dgSorder.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(sorder);   　　//之前创建的DataGrid1绑定数据的方法   

    }
    protected void dgSorder_UpdateCommand(object source, DataGridCommandEventArgs e)
    {          
        try
        {
            int qty = Convert.ToInt32(((TextBox)e.Item.Cells[5].Controls[1]).Text);
            string sorder = tbSorder.Text.Trim().ToUpper();
            string strsql = "Update SHP.CMCS_SFC_SORDER  SET PID_QTY='" + qty + "' Where  SORDER='" + sorder + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgSorder.EditItemIndex = -1;
            BindData(sorder);
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('工單數量輸入錯誤!');</script>");
            
            return;
        }
    }
}
