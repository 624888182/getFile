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

public partial class Boundary_WFrmUserPrivilege : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SetSqlDataSource();
        if (!IsPostBack)
        {
            MultiLanguage();
        }
        LoadGridPrivilegesData("");
    }

    private void MultiLanguage()
    {
        GetGridPrivilegesTitle();
        LblUserID.Text = (String)GetGlobalResourceObject("SFCQuery", "UserID");
        BtnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    private void GetGridPrivilegesTitle()
    {
        GridPrivileges.Columns[0].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "MenuID");
        GridPrivileges.Columns[1].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Function");
        GridPrivileges.Columns[2].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Description");
        GridPrivileges.Columns[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Privileges");
    }

    private void SetSqlDataSource()
    {
        SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
        SqlDataSource1.ProviderName = ConfigurationManager.ConnectionStrings[1].ProviderName.ToString();
    }

    private void LoadGridPrivilegesData(string StrUserID)
    {
        string[] ArrUserID ={ "UserID" };
        GridPrivileges.DataKeyNames = ArrUserID;

        SqlDataSource1.SelectParameters.Clear();

        if (StrUserID.Length > 0)
        {
            SqlDataSource1.SelectParameters.Add("UserID",TypeCode.String, StrUserID);
        }
        else
        {
            SqlDataSource1.SelectParameters.Add("UserID", TypeCode.String,Context.User.Identity.Name);
        }

        SqlDataSource1.SelectCommand = "select a.ftabid,a.fdesktopsrc,a.description,b.userid  from   TBLTABSDEFINITIONS A left join tblmenuprivileges B ON A.FTABID=b.menuid and b.userid=:UserID order by a.ftabid";
        GridPrivileges.DataSourceID = "SqlDataSource1";
    }

    protected void GridPrivileges_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridPrivileges.Rows.Count; i++)
        {
            if (((Label)GridPrivileges.Rows[i].Cells[3].FindControl("Label1")).Text.Length > 0)
            {
                ((CheckBox)GridPrivileges.Rows[i].Cells[3].FindControl("CheckBox1")).Checked = true;
            }
            else 
            {
                ((CheckBox)GridPrivileges.Rows[i].Cells[3].FindControl("CheckBox1")).Checked = false;
            }   
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string StrUserID = GetUserID();

        for (int i = 0; i < GridPrivileges.Rows.Count; i++)
        {
            if (((CheckBox)GridPrivileges.Rows[i].Cells[3].FindControl("CheckBox1")).Checked == true)
            {
                string StrSql = "select * from tblmenuprivileges where userid='" + StrUserID + "' And menuid='" + GridPrivileges.Rows[i].Cells[0].Text + "'";

                if (ClsGlobal.objDataConnect.DataIsExist(StrSql) == false)
                {
                    SqlDataSource1.InsertParameters.Clear();
                    SqlDataSource1.InsertParameters.Add("UserID", TypeCode.String, StrUserID);
                    SqlDataSource1.InsertParameters.Add("MenuID", TypeCode.String, GridPrivileges.Rows[i].Cells[0].Text);

                    SqlDataSource1.InsertCommand = "INSERT INTO TBLMENUPRIVILEGES(USERID,MENUID) VALUES(:UserID,:MenuID)";
                    SqlDataSource1.Insert();
                }
            }
            else
            {
                SqlDataSource1.DeleteParameters.Clear();
                SqlDataSource1.DeleteParameters.Add("UserID", TypeCode.String, StrUserID);
                SqlDataSource1.DeleteParameters.Add("MenuID", TypeCode.String, GridPrivileges.Rows[i].Cells[0].Text);

                SqlDataSource1.DeleteCommand = "DELETE FROM TBLMENUPRIVILEGES WHERE UserID=:UserID AND MenuID=:MenuID";
                SqlDataSource1.Delete();
            }
        }
    }
    
    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)GridPrivileges.HeaderRow.Cells[3].FindControl("CheckAll")).Checked == true)
        {
            for (int i = 0; i < GridPrivileges.Rows.Count; i++)
            {
                ((CheckBox)GridPrivileges.Rows[i].Cells[3].FindControl("CheckBox1")).Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < GridPrivileges.Rows.Count; i++)
            {
                ((CheckBox)GridPrivileges.Rows[i].Cells[3].FindControl("CheckBox1")).Checked = false;
            }
        }
    }

    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        string StrSql = "Select t.loginname from new_users t where t.loginname ='" + TbUserID.Text.Trim() + "' ";
        
        if (ClsGlobal.objDataConnect.DataIsExist(StrSql) == true)
        {
            LoadGridPrivilegesData(TbUserID.Text.Trim());
        }
        else
        {
            ClsCommon.ShowMessage(this.Page, (string)GetGlobalResourceObject("SFCQuery", "UserNotExists"));
        }
    }

    private string GetUserID()
    {
        if (TbUserID.Text.Trim().Length > 0)
        {
            return TbUserID.Text.Trim();
        }
        else
        {
            return Context.User.Identity.Name;
        }
    }
}
