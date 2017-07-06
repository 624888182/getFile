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
using DB.EAI;
using DBAccess.EAI;
using System.Data.OracleClient;

public partial class WFrmUserManage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetSqlDataSource();

        if (!IsPostBack)
        {
            MultiLanguage();
        }
        LoadGridUserData("");
    }

    private void MultiLanguage()
    {
        GetGridUserTitle();
        GetDetailsUserTitle();
        LblLoginName.Text = (String)GetGlobalResourceObject("SFCQuery", "LoginName");
        BtnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    private void GetGridUserTitle()
    {
        GridUser.Columns[0].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "LoginName");
        GridUser.Columns[1].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "UserName");
        GridUser.Columns[2].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Dept");
        GridUser.Columns[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "EffectiveFrom");
        GridUser.Columns[4].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "EffectiveTo");
    }

    private void GetDetailsUserTitle()
    {
        DetailsUser.Fields[0].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "LoginName");
        DetailsUser.Fields[1].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "UserName");
        DetailsUser.Fields[2].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Dept");
        DetailsUser.Fields[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "EffectiveFrom");
        DetailsUser.Fields[4].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "EffectiveFrom");
    }

    private void SetSqlDataSource()
    {
        SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
        SqlDataSource1.ProviderName = ConfigurationManager.ConnectionStrings[1].ProviderName.ToString();

        SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
        SqlDataSource2.ProviderName = ConfigurationManager.ConnectionStrings[1].ProviderName.ToString();
    }

    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        LoadGridUserData(TbLoginName.Text);
        TbLoginName.Text = "";
    }

    protected void DetailsUser_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
    {
        DeleteDetailsUserData(e.Values[0].ToString());
    }

    protected void DetailsUser_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        string StrLoginName     = e.Values[0].ToString();
        string StrUserName      = e.Values[1].ToString();
        string StrDept          = e.Values[2].ToString();
        string StrEffecticeFrom = e.Values[3].ToString();
        string StrEffecticeTo   = e.Values[4].ToString();

        InsertDetailsUserData(StrLoginName, StrUserName, StrDept, StrEffecticeFrom, StrEffecticeTo);
    }

    protected void DetailsUser_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        string StrLoginName     = e.OldValues[0].ToString();
        string StrUserName      = e.NewValues[1].ToString();
        string StrDept          = e.NewValues[2].ToString();
        string StrEffecticeFrom = e.NewValues[3].ToString();
        string StrEffecticeTo   = e.NewValues[4].ToString();

        UpdteDetailsUserData(StrLoginName, StrUserName, StrDept, StrEffecticeFrom, StrEffecticeTo);
    }

    private void LoadGridUserData(string StrLoginName)
    {
        string[] ArrDataKeyNames ={ "LOGINNAME" };
        GridUser.DataKeyNames = ArrDataKeyNames;

        SqlDataSource1.SelectParameters.Clear();
        
        if (StrLoginName.Length > 0)
        {
            SqlDataSource1.SelectParameters.Add("LOGINNAME", TypeCode.String, StrLoginName);
            SqlDataSource1.SelectCommand = "SELECT LOGINNAME,USERNAME,DEPT,EFFECTIVE_FROM, EFFECTIVE_TO FROM NEW_USERS WHERE LOGINNAME =:LOGINNAME";
        }
        else
        {
            SqlDataSource1.SelectCommand = "SELECT LOGINNAME,USERNAME,DEPT,EFFECTIVE_FROM,EFFECTIVE_TO FROM NEW_USERS ";
        }
        GridUser.DataSourceID = "SqlDataSource1";
    }

    protected void DetailsUser_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Edit")
        {
            SelectDetailsUserData(DetailsUser.Rows[0].Cells[1].Text);
        }
    }

    protected void GridUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        SelectDetailsUserData(GridUser.DataKeys[e.NewSelectedIndex].Value.ToString());
    }

    private void SelectDetailsUserData(string StrLoginName)
    {
        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("LOGINNAME", TypeCode.String, StrLoginName);
        SqlDataSource2.SelectCommand = "SELECT LOGINNAME,USERNAME,DEPT,EFFECTIVE_FROM,EFFECTIVE_TO FROM NEW_USERS WHERE LOGINNAME =:LOGINNAME";

        DetailsUser.DataSourceID = "SqlDataSource2";
    }

    private void InsertDetailsUserData(string StrLoginName, string StrUserName, string StrDept, string StrEffecticeFrom, string StrEffecticeTo)
    {
        string StrSql = "SELECT LOGINNAME FROM NEW_USERS WHERE LOGINNAME='" + StrLoginName + "'";

        if (ClsGlobal.objDataConnect.DataIsExist(StrSql) == false)
        {
            SqlDataSource2.InsertParameters.Clear();
            SqlDataSource2.InsertParameters.Add("LOGINNAME", TypeCode.String, StrLoginName);
            SqlDataSource2.InsertParameters.Add("USERNAME", TypeCode.String, StrUserName);
            SqlDataSource2.InsertParameters.Add("DEPT", TypeCode.String, StrDept);
            SqlDataSource2.InsertParameters.Add("EFFECTIVE_FROM", TypeCode.String, StrEffecticeFrom);
            SqlDataSource2.InsertParameters.Add("EFFECTIVE_TO", TypeCode.String, StrEffecticeTo);
            SqlDataSource2.InsertCommand = "INSERT INTO NEW_USERS(LOGINNAME,USERNAME,DEPT,EFFECTIVE_FROM,EFFECTIVE_TO) VALUES(:LOGINNAME,:USERNAME,:DEPT,TO_DATE(:EFFECTIVE_FROM,'YYYY-MM-DD HH24:MI:SS'),TO_DATE(:EFFECTIVE_TO,'YYYY-MM-DD HH24:MI:SS'))";

            SelectDetailsUserData(StrLoginName);
        }
        else
        {
            ClsCommon.ShowMessage(this.Page, (string)GetGlobalResourceObject("SFCQuery", "DataExists"));
        }
    }

    private void DeleteDetailsUserData(string StrLoginName)
    {
        SqlDataSource2.DeleteParameters.Clear();
        SqlDataSource2.DeleteParameters.Add("D_LOGINNAME", TypeCode.String, StrLoginName);
        SqlDataSource2.DeleteCommand = "DELETE FROM NEW_USERS WHERE LOGINNAME =:D_LOGINNAME";
    }

    private void UpdteDetailsUserData(string StrLoginName, string StrUserName, string StrDept, string StrEffecticeFrom, string StrEffecticeTo)
    {
        SqlDataSource2.UpdateParameters.Clear();
        SqlDataSource2.UpdateParameters.Add("LOGINNAME", TypeCode.String, StrLoginName);
        SqlDataSource2.UpdateParameters.Add("USERNAME", TypeCode.String, StrUserName);
        SqlDataSource2.UpdateParameters.Add("DEPT", TypeCode.String, StrDept);
        SqlDataSource2.UpdateParameters.Add("EFFECTIVE_FROM", TypeCode.String, StrEffecticeFrom);
        SqlDataSource2.UpdateParameters.Add("EFFECTIVE_TO", TypeCode.String, StrEffecticeTo);
        SqlDataSource2.UpdateCommand = "UPDATE NEW_USERS SET LOGINNAME =:LOGINNAME,USERNAME =:USERNAME,DEPT =:DEPT,EFFECTIVE_FROM=TO_DATE(:EFFECTIVE_FROM,'YYYY-MM-DD HH24:MI:SS'),EFFECTIVE_TO =TO_DATE(:EFFECTIVE_TO,'YYYY-MM-DD HH24:MI:SS') WHERE LOGINNAME =:LOGINNAME";

        SelectDetailsUserData(StrLoginName);
    }
}
