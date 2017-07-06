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

public partial class Boundary_WFrmModuleManage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetSqlDataSource();

        if (!IsPostBack)
        {
            MultiLanguage();
            
        }
        GetGridFunctionData("");
    }
    private void MultiLanguage()
    {
        GetGridFunctionTitle();
        GetDetailFunctionTitle();
        LblFTabID.Text = (String)GetGlobalResourceObject("SFCQuery", "TabID");
        BtnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    private void GetGridFunctionTitle()
    {
        GridFunction.Columns[0].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "TabID");
        GridFunction.Columns[1].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "TabsDefinitions");
        GridFunction.Columns[2].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Description");
        GridFunction.Columns[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FParentID");
        GridFunction.Columns[4].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FTabOrder");
    }
    private void GetDetailFunctionTitle()
    {
        DetailFunction.Fields[0].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "TabID");
        DetailFunction.Fields[1].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "TabsDefinitions");
        DetailFunction.Fields[2].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Description");
        DetailFunction.Fields[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FParentID");
        DetailFunction.Fields[4].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FTabOrder");

    }

    private void SetSqlDataSource()
    {
        SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
        SqlDataSource1.ProviderName = ConfigurationManager.ConnectionStrings[1].ProviderName.ToString();

        SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
        SqlDataSource2.ProviderName = ConfigurationManager.ConnectionStrings[1].ProviderName.ToString();
    }

    private void GetGridFunctionData(string StrFTabID)
    {
        SqlDataSource1.SelectParameters.Clear();

        string[] ArrFTabid = {"FTABID"};
        GridFunction.DataKeyNames = ArrFTabid;

        if (StrFTabID.Length > 0)
        {
            SqlDataSource1.SelectParameters.Add("FTabID", TypeCode.String, StrFTabID);
            SqlDataSource1.SelectCommand = "SELECT FTABID,FDESKTOPSRC,DESCRIPTION,FPARENTID,FTABORDER FROM TBLTABSDEFINITIONS WHERE FTabID=:FTabID ORDER BY FTABID";
        }
        else 
        {
            SqlDataSource1.SelectCommand = "SELECT FTABID,FDESKTOPSRC,DESCRIPTION,FPARENTID,FTABORDER  FROM TBLTABSDEFINITIONS ORDER BY FTABID";
        }

        GridFunction.DataSourceID ="SqlDataSource1";
    }

    private void GetDetailFunctionData(string StrFTabID)
    {
        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("FTabID", TypeCode.String, StrFTabID);
        SqlDataSource2.SelectCommand = "SELECT FTABID,FDESKTOPSRC,DESCRIPTION,FPARENTID,FTABORDER  FROM TBLTABSDEFINITIONS WHERE FTabID=:FTabID";

        DetailFunction.DataSourceID = "SqlDataSource2";
    }

    protected void GridFunction_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string StrFTabID = GridFunction.DataKeys[e.NewSelectedIndex].Value.ToString();

        GetDetailFunctionData(StrFTabID);

    }

    protected void DetailFunction_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Edit")
        {
            string StrFTabID = DetailFunction.Rows[0].Cells[1].Text;
            GetDetailFunctionData(StrFTabID);
        }
    }

    protected void DetailFunction_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        string StrFTabID      = e.Values[0].ToString();
        string StrFDeskTopSrc = e.Values[1].ToString();
        string StrDescription = e.Values[2].ToString();
        string StrFParentID   = e.Values[3].ToString();
        string StrFTabOrder   = e.Values[4].ToString();

        InsertFunctionData(StrFTabID, StrFDeskTopSrc, StrDescription, StrFParentID, StrFTabOrder);
    }
    protected void DetailFunction_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
    {
        string StrFTabID = e.Values[0].ToString();
        

        DeleteFunctionData(StrFTabID);
    }
    protected void DetailFunction_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        string StrFTabID      = e.OldValues[0].ToString();
        string StrFDeskTopSrc = e.NewValues[1].ToString();
        string StrDescription = e.NewValues[2].ToString();
        string StrFParentID   = e.NewValues[3].ToString();
        string StrFTabOrder   = e.NewValues[4].ToString();

        UpdateFunctionData(StrFTabID, StrFDeskTopSrc, StrDescription, StrFParentID, StrFTabOrder);
    }

    private void InsertFunctionData(string StrFTabID, string StrFDeskTopSrc, string StrDescription, string StrFParentID, string StrFTabOrder)
    {
        string StrSql = "SELECT FTABID FROM TBLTABSDEFINITIONS WHERE FTABID='" + StrFTabID + "'";

        if (ClsGlobal.objDataConnect.DataIsExist(StrSql) == false)
        {
            SqlDataSource2.InsertParameters.Clear();
            SqlDataSource2.InsertParameters.Add("FTabID", TypeCode.String, StrFTabID);
            SqlDataSource2.InsertParameters.Add("FDESKTOPSRC", TypeCode.String, StrFDeskTopSrc);
            SqlDataSource2.InsertParameters.Add("DESCRIPTION", TypeCode.String, StrDescription);
            SqlDataSource2.InsertParameters.Add("FPARENTID", TypeCode.String, StrFParentID);
            SqlDataSource2.InsertParameters.Add("FTABORDER", TypeCode.String, StrFTabOrder);

            SqlDataSource2.InsertCommand = "INSERT INTO TBLTABSDEFINITIONS(FTABID,FDESKTOPSRC,DESCRIPTION,FPARENTID,FTABORDER) VALUES(:FTabID,:FDESKTOPSRC,:DESCRIPTION,:FPARENTID,:FTABORDER)";

            GetDetailFunctionData(StrFTabID);
        }
        else
        {
            ClsCommon.ShowMessage(this.Page, (string)GetGlobalResourceObject("SFCQuery", "DataExists"));
        }
    }

    private void DeleteFunctionData(string StrFTabID)
    {
        SqlDataSource2.DeleteParameters.Clear();
        SqlDataSource2.DeleteParameters.Add("FTabID", TypeCode.String, StrFTabID);

        SqlDataSource2.DeleteCommand = "DELETE FROM TBLTABSDEFINITIONS WHERE FTabID=:FTabID";
    }

    private void UpdateFunctionData(string StrFTabID, string StrFDeskTopSrc, string StrDescription, string StrFParentID, string StrFTabOrder)
    {
        SqlDataSource2.UpdateParameters.Clear();
        SqlDataSource2.UpdateParameters.Add("FTabID", TypeCode.String, StrFTabID);
        SqlDataSource2.UpdateParameters.Add("FDESKTOPSRC", TypeCode.String, StrFDeskTopSrc);
        SqlDataSource2.UpdateParameters.Add("Description", TypeCode.String, StrDescription);
        SqlDataSource2.UpdateParameters.Add("FPARENTID", TypeCode.String, StrFParentID);
        SqlDataSource2.UpdateParameters.Add("FTABORDER", TypeCode.String, StrFTabOrder);

        SqlDataSource2.UpdateCommand = "UPDATE TBLTABSDEFINITIONS Set FTabID =:FTabID,FDESKTOPSRC=:FDESKTOPSRC,DESCRIPTION=:DESCRIPTION,FPARENTID=:FPARENTID,FTABORDER=:FTABORDER WHERE FTabID=:FTabID";
        GetDetailFunctionData(StrFTabID);
    }
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        GetGridFunctionData(this.TbFTabID.Text.Trim());
    }
}