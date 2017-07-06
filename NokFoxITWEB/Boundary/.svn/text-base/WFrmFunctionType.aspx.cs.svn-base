/*************************************************************************
 * 
 *  Unit description: Edit Function Type
 *  Developer: Shu Jian Bo             Date: 2008/01/04
 *  Modifier : Shu Jian Bo             Date: 2008/01/04
 * 
 * ***********************************************************************/
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

public partial class Boundary_WFrmFunctionType : Localized
{
    #region property
    private bool IsEdit
    {
        get
        {
            return (bool)ViewState["IsEdit"];
        }
        set
        {
            ViewState["IsEdit"] = value;
        }

    }
    private bool IsAdd
    {
        get
        {
            return (bool)ViewState["IsAdd"];
        }
        set
        {
            ViewState["IsAdd"] = value;
        }

    }

    private int iCurrent
    {
        get
        {
            return (int)ViewState["iCurrent"];
        }
        set
        {
            ViewState["iCurrent"] = value;
        }

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsAdd = false;
            IsEdit = false;
            MultiLanguage();

            iCurrent = 0;
        }
        dgFunctionName.DataSource = GetQueryData().DefaultView ;
        dgFunctionName.DataBind();
    }

    private void MultiLanguage()
    {
        ViewState["delsucc"] = (String)GetGlobalResourceObject("commonres", "DeleteSuccess");
        ViewState["delfail"] = (String)GetGlobalResourceObject("commonres", "DeleteFail");
        ViewState["SaveSuccess"] = (String)GetGlobalResourceObject("commonres", "SaveSuccess");
        ViewState["SaveFail"] = (String)GetGlobalResourceObject("commonres", "SaveFail");

        dgFunctionName.Columns[0].HeaderText = (string)GetGlobalResourceObject("commonres", "Edit");
        dgFunctionName.Columns[1].HeaderText = (string)GetGlobalResourceObject("commonres", "Delete");
        dgFunctionName.Columns[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FunctionName");
        dgFunctionName.Columns[4].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FunctionDesc");
    }

    private void SetEnabled(bool Astate)
    {
        btnAdd.Enabled = Astate;

        dgFunctionName.DisablePaging = !Astate;
        dgFunctionName.DisableSetButton = !Astate;
        dgFunctionName.AllowSorting = Astate;
    }

    private DataTable GetQueryData()
    {
        string strSql = "SELECT * FROM TBLFUNCTION_NAME T ORDER BY FUNCTION_ID ";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        return dt;
    }

    private void BindList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FUNCTION_ID");
        dt.Columns.Add("FUNCTION_NAME");
        dt.Columns.Add("FUNCTION_DESC");
        dgFunctionName.DataSource = dt.DefaultView;
        dgFunctionName.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        iCurrent = 0;
        IsAdd = true;
        IsEdit = false;
        SetEnabled(false);
        DataTable dt = new DataTable();
        if (dgFunctionName.Items.Count > 0)
        {
            dt = GetQueryData();
        }
        else
        {
            dt.Columns.Add("FUNCTION_ID");
            dt.Columns.Add("FUNCTION_NAME");
            dt.Columns.Add("FUNCTION_DESC");
        }

        DataRow dr = dt.NewRow();
        dt.Rows.InsertAt(dr, 0);
        dgFunctionName.EditItemIndex = 0;
        dgFunctionName.CurrentPageIndex = 0;
        dgFunctionName.DataSource = dt.DefaultView;
        dgFunctionName.DataBind();
    }
    protected void dgFunctionName_EditCommand(object source, DataGridCommandEventArgs e)
    {
        ViewState["Functionid"] = ((Label)e.Item.Cells[2].FindControl("Label1")).Text;
        IsAdd = false;
        IsEdit = true;
        SetEnabled(false);

        iCurrent = e.Item.ItemIndex;
        dgFunctionName.EditItemIndex = iCurrent;

        dgFunctionName.DataSource = GetQueryData().DefaultView;
        dgFunctionName.DataBind();
    }
    protected void dgFunctionName_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        IsAdd = false;
        IsEdit = false;
        SetEnabled(true);
        dgFunctionName.EditItemIndex = -1;
        dgFunctionName.DataSource = GetQueryData().DefaultView;
        dgFunctionName.DataBind();
    }
    protected void dgFunctionName_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            if ((!IsAdd) && (!IsEdit))
            {
                LinkButton lnkbtn = (LinkButton)e.Item.Cells[1].FindControl("LinkButton4");
                lnkbtn.Attributes.Add("onclick", "return confirm(\"" + (String)GetGlobalResourceObject("commonres", "DeleteConfirm") + "\");");
            }
        }
    }
    protected void dgFunctionName_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if ((!e.CommandName.ToUpper().Equals("EDIT")) && (!e.CommandName.ToUpper().Equals("UPDATE")))
        {
            if (e.CommandName.ToUpper().Equals("SET"))
            {
                if (dgFunctionName.CurrentStatus.ToUpper().Equals("SET"))
                {
                    btnAdd.Enabled = false;
                    BindList();
                }
                else
                {
                    btnAdd.Enabled = true;
                    dgFunctionName.DataSource = GetQueryData().DefaultView;
                    dgFunctionName.DataBind();
                }
            }
        }    
    }
    protected void dgFunctionName_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.EditItem))
        {
            if (IsEdit)
            {
                if (e.Item.ItemIndex == (int)ViewState["iCurrent"])
                {
                    ((LinkButton)(e.Item.Cells[0].Controls[1])).Enabled = true;
                    ((LinkButton)(e.Item.Cells[1].Controls[1])).Enabled = false;
                }
                else
                {
                    ((LinkButton)(e.Item.Cells[0].Controls[1])).Enabled = false;
                    ((LinkButton)(e.Item.Cells[1].Controls[1])).Enabled = false;
                }
            }
            else if (IsAdd)
            {

                if (e.Item.ItemIndex == (int)ViewState["iCurrent"])
                {
                    ((LinkButton)(e.Item.Cells[0].Controls[1])).Enabled = true;
                    ((LinkButton)(e.Item.Cells[1].Controls[1])).Enabled = false;
                }
                else
                {
                    ((LinkButton)(e.Item.Cells[0].Controls[1])).Enabled = false;
                    ((LinkButton)(e.Item.Cells[1].Controls[1])).Enabled = false;
                }
            }
        }
    }
    protected void dgFunctionName_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        string strFunctionID = ((Label)e.Item.Cells[2].FindControl("Label1")).Text;
        string strSql = "DELETE TBLFUNCTION_NAME WHERE FUNCTION_ID="+ClsCommon.GetSqlString(strFunctionID);
        try
        {
            ClsGlobal.objDataConnect.DataExecute(strSql);
            ClsCommon.ShowMessage(this.Page, ViewState["delsucc"].ToString());
        }
        catch
        {
            ClsCommon.ShowMessage(this.Page, ViewState["delfail"].ToString());
        }
        finally
        {
            dgFunctionName.DataSource = GetQueryData().DefaultView;
            dgFunctionName.DataBind();
        }
    }
    protected void dgFunctionName_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        int ss = e.Item.ItemIndex;
        string strFunctionName = ((TextBox)e.Item.Cells[3].FindControl("tbFunctionName")).Text;
        string strFunctionDesc = ((TextBox)e.Item.Cells[4].FindControl("tbFunctionDesc")).Text;
        strFunctionName = ((TextBox)dgFunctionName.Items[iCurrent].Cells[3].Controls[1]).Text.Trim();
        try
        {
            string strSql = "";
            if (IsAdd)
                strSql = "INSERT INTO TBLFUNCTION_NAME (FUNCTION_ID, FUNCTION_NAME, FUNCTION_DESC) VALUES (TBLFUNCTION_NAME_S.NEXTVAL, "+ClsCommon.GetSqlString(strFunctionName)+", "+ClsCommon.GetSqlString(strFunctionDesc)+")";
            else if (IsEdit)
                strSql = "UPDATE TBLFUNCTION_NAME SET FUNCTION_NAME = " + ClsCommon.GetSqlString(strFunctionName) + ",FUNCTION_DESC = " + ClsCommon.GetSqlString(strFunctionDesc) + " WHERE FUNCTION_ID =" + ClsCommon.GetSqlString(ViewState["Functionid"].ToString());
            ClsGlobal.objDataConnect.DataExecute(strSql);

            SetEnabled(true);
            IsAdd = false;
            IsEdit = false;
            dgFunctionName.EditItemIndex = -1;
            //dgConfiguration.DataSource = GetQueryData().DefaultView;
            //dgConfiguration.DataBind();

            ClsCommon.ShowMessage(this.Page, ViewState["SaveSuccess"].ToString());
            dgFunctionName.DataSource = GetQueryData().DefaultView;
            dgFunctionName.DataBind();
        }
        catch
        {
            ClsCommon.ShowMessage(this.Page, ViewState["SaveFail"].ToString());
        }
       
    }
}
