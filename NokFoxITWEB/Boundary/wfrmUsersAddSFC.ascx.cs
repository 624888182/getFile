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
using System.Data.OracleClient;
using DBAccess.EAI;

public partial class Boundary_wfrmUsersAddSFC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            string struser = Session["User"].ToString();
            if (struser != "F4004096" && struser != "F2860265" && struser != "F2833767")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('您沒有權限瀏覽此頁面!');</script>");
                return;
            }
            else
            {
                string user = txtUser.Text.Trim();
                BindData(user);
            }
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
    private void BindData(string user)
    {
        dgUser.Visible = true;
        string strsql = "SELECT * FROM sfc.NEW_USERS where loginname like '" + user + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgUser.DataSource = this.GetIDTable(dt1).DefaultView;
            dgUser.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('尚無該User信息,請先維護..');</script>");
            return;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strusers = Session["User"].ToString();
        if (strusers != "F4004096" && strusers != "F2860265" && strusers != "F2833767")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('您沒有權限查詢!');</script>");
            return;
        }

        string strUser = txtUser.Text.Trim();
        string strsql = "SELECT * FROM sfc.NEW_USERS where loginname  like '" + strUser + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgUser.Visible = true;
            dgUser.DataSource = this.GetIDTable(dt1).DefaultView;
            dgUser.DataBind();
        }
        else
        {
            dgUser.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('尚無該User信息,請先維護..');</script>");
            return;
        }
    }
    protected void dgUser_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgUser.PageSize) * (dgUser.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((LinkButton)(e.Item.Cells[12].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?操作不可返回,按確認繼續..');");

        }
    }
    protected void dgUser_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strUser = txtUser.Text.Trim();
        dgUser.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strUser); 
    }
    protected void dgUser_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strloginname = ((Label)e.Item.Cells[1].Controls[1]).Text.Trim();
        string strfactoryarea = ((Label)e.Item.Cells[8].Controls[1]).Text.Trim(); 

        OracleConnection orcn = null;
        OracleDataAdapter da;
        DataSet myds;
        myds = new DataSet();
        try
        {
            orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tjsfc");
            //orcn = new OracleConnection("Provider=MSDAORA.Oracle;Data Source=tjsfc;user id=sfc;password=sfc");
            orcn.Open();
            if(strfactoryarea.Length>0) 
                da = new OracleDataAdapter("select * from SFC.NEW_USERS where LOGINNAME='" + strloginname + "' and FACTORY_AREA='" + strfactoryarea + "'", orcn);
            else
                da = new OracleDataAdapter("select * from SFC.NEW_USERS where LOGINNAME='" + strloginname + "'", orcn);
            
            da.Fill(myds, "NEW_USERS");

            //----------- update part-----------
            //DataTable myDt = myds.Tables["CDMA_MOTO_ORDERNO"];
            //myDt.PrimaryKey = new DataColumn[] { myDt.Columns["ORDER_NUMBER"] };
            //出错就是因为少了上面这一句。这条语句指定了DataTable的主键。或者用下一条语句也可以，下一条语句是让适配器自动加上表的架构（Key约束）
            //da.MissingSchemaAction = MissingSchemaAction.AddWithKey; 

            myds.Tables["NEW_USERS"].Rows[0]["USERNAME"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
            myds.Tables["NEW_USERS"].Rows[0]["LOGINPSW"] = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
            myds.Tables["NEW_USERS"].Rows[0]["MENURIGHT"] = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
            if (((TextBox)e.Item.Cells[5].Controls[1]).Text.Trim().Length > 0)
                myds.Tables["NEW_USERS"].Rows[0]["DATARIGHT"] = Convert.ToInt32(((TextBox)e.Item.Cells[5].Controls[1]).Text);
            myds.Tables["NEW_USERS"].Rows[0]["DEPT"] = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
            myds.Tables["NEW_USERS"].Rows[0]["COMPUTERNAME"] = ((TextBox)e.Item.Cells[7].Controls[1]).Text;
            myds.Tables["NEW_USERS"].Rows[0]["EFFECTIVE_TO"] = ((TextBox)e.Item.Cells[10].Controls[1]).Text;
             
            OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
            da.Update(myds, "NEW_USERS");
            //da.Update(myds, "CDMA_MOTO_ORDERNO");
            orcn.Close();
        }

        catch (Exception ex)
        {
            orcn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種信息已存在,請檢查...');</script>");
            return;
        } 
        dgUser.EditItemIndex = -1;
        BindData(strloginname); 
    }
    protected void dgUser_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strUser = txtUser.Text.Trim();
        dgUser.EditItemIndex = e.Item.ItemIndex;
        BindData(strUser); 
    }
    protected void dgUser_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string strUser = txtUser.Text.Trim();
        dgUser.CurrentPageIndex = e.NewPageIndex;
        BindData(strUser);
    }
    protected void dgUser_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgUser.ShowFooter = true;

            string strUser = txtUser.Text.Trim();
            string strsql = "SELECT * FROM sfc.NEW_USERS where loginname like '" + strUser + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                dgUser.DataSource = this.GetIDTable(dt1).DefaultView;
                dgUser.DataBind(); 
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgUser.ShowFooter = false;
            string strUser = txtUser.Text.Trim();
            BindData(strUser);
        }
        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgUser.ShowFooter = false; 

            OracleConnection orcn = null;
            OracleDataAdapter da;
            DataSet myds;
            myds = new DataSet();

            try
            {
                orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");

                orcn.Open();

                da = new OracleDataAdapter("select * from SFC.NEW_USERS where 0=1 ", orcn);
                da.Fill(myds, "NEW_USERS");

                //----------- Insert new row----------

                DataRow mydr = myds.Tables["NEW_USERS"].NewRow();

                if (((TextBox)e.Item.Cells[1].Controls[1]).Text.Trim().Length > 0)
                    mydr["LOGINNAME"] = ((TextBox)e.Item.Cells[1].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('LoginName欄位不能為空...');</script>");
                    return;
                } 
                mydr["USERNAME"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
                mydr["LOGINPSW"] = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
                if (((TextBox)e.Item.Cells[4].Controls[1]).Text.Trim().Length > 0)
                    mydr["MENURIGHT"] = ((TextBox)e.Item.Cells[4].Controls[1]).Text; 
                if (((TextBox)e.Item.Cells[5].Controls[1]).Text.Trim().Length > 0)
                    mydr["DATARIGHT"] = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
                if (((TextBox)e.Item.Cells[6].Controls[1]).Text.Trim().Length > 0)
                    mydr["DEPT"] = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
                if (((TextBox)e.Item.Cells[7].Controls[1]).Text.Trim().Length > 0)
                    mydr["COMPUTERNAME"] = ((TextBox)e.Item.Cells[7].Controls[1]).Text;
                if (((TextBox)e.Item.Cells[8].Controls[1]).Text.Trim().Length > 0)
                    mydr["FACTORY_AREA"] = ((TextBox)e.Item.Cells[8].Controls[1]).Text;

                mydr["EFFECTIVE_FROM"] = DateTime.Now.ToString();
                mydr["EFFECTIVE_TO"] = DateTime.Now.AddYears(1).ToString();

                myds.Tables["NEW_USERS"].Rows.Add(mydr);

                // ---------------end -------------------

                OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
                da.Update(myds, "NEW_USERS");
                orcn.Close();
            }
            catch (Exception ex)
            {
                orcn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種信息已存在,請檢查...');</script>");
                return;
            }
            dgUser.EditItemIndex = -1;
            string strUser = txtUser.Text.Trim();
            BindData(strUser);

        }
        if (e.CommandName == "ItemDelete")
        {
            string strsql="";
            string strloginname = ((Label)e.Item.Cells[1].Controls[1]).Text;
            string strfactoryarea = ((Label)e.Item.Cells[8].Controls[1]).Text;
            if(strfactoryarea.Length>0)
                strsql = "delete from SFC.NEW_USERS where loginname='" + strloginname + "' and factory_area='" + strfactoryarea + "'";
            else
                strsql = "delete from SFC.NEW_USERS where loginname='" + strloginname + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgUser.EditItemIndex = -1;
            txtUser.Text = "";
            string strsql1 = "SELECT * FROM SFC.NEW_USERS ";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                dgUser.DataSource = this.GetIDTable(dt1).DefaultView;
                dgUser.DataBind();
            }
            else
            {
                dgUser.Visible = false;
            } 
        }
    }
}
