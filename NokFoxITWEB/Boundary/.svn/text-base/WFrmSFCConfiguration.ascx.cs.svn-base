/*************************************************************************
 * 
 *  Unit description: SFC parameters configuration
 *  Developer: Shu Jian Bo             Date: 2007/12/06
 *  Modifier : Shu Jian Bo             Date: 2007/12/06
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
using System.Data.OracleClient;
using DBAccess.EAI;

public partial class Boundary_WFrmSFCConfiguration : System.Web.UI.UserControl
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

    string strProcedureName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ClsCommon.RegisterClientScript(this.Page);
        if (!IsPostBack)
        {
            IsAdd = false;
            IsEdit = false;

            BindModel(ddlModel);
            BindStationID(ddlStationID);
            MultiLanguage();
            iCurrent = 0;
            ViewState["rowid"] = "";

            dgConfiguration.DataSource = GetQueryData().DefaultView;
            dgConfiguration.DataBind();            
        }
    }

    private void BindModel(DropDownList ddlModel)
    {
        strProcedureName = "SFCQUERY.GETMODELNAME";
        OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("DATA",OracleType.Cursor)};
        orapara[0].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName,orapara).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();
    }

    private void BindStationID(DropDownList ddlStationID)
    {
        strProcedureName = "SFCQUERY.GETSTATION";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
        orapara[0].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlStationID.DataTextField = "DESCRIPTION";
        ddlStationID.DataValueField = "STATION_ID";
        ddlStationID.DataSource = dt.DefaultView;
        ddlStationID.DataBind();
    }

    private void MultiLanguage()
    {
        lblModel.Text = (string)GetGlobalResourceObject("SFCQuery", "Model");
        lblStationID.Text = (string)GetGlobalResourceObject("SFCQuery", "StationID");
        btnQuery.Text = (string)GetGlobalResourceObject("SFCQuery", "Query");

        ViewState["delsucc"] = (String)GetGlobalResourceObject("commonres", "DeleteSuccess");
        ViewState["delfail"] = (String)GetGlobalResourceObject("commonres", "DeleteFail");
        ViewState["SaveSuccess"] = (String)GetGlobalResourceObject("commonres", "SaveSuccess");
        ViewState["SaveFail"] = (String)GetGlobalResourceObject("commonres", "SaveFail");

        dgConfiguration.Columns[0].HeaderText = (string)GetGlobalResourceObject("commonres", "Edit");
        dgConfiguration.Columns[1].HeaderText = (string)GetGlobalResourceObject("commonres", "Delete");
        dgConfiguration.Columns[2].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "Model");
        dgConfiguration.Columns[3].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "StationID");
        dgConfiguration.Columns[4].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "ColumnName");
        dgConfiguration.Columns[5].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "ColumnDesc");
        dgConfiguration.Columns[6].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FunctionType");
        dgConfiguration.Columns[7].HeaderText = (string)GetGlobalResourceObject("SFCQuery", "FunctionValue");
        dgConfiguration.Columns[8].HeaderText = (string)GetGlobalResourceObject("commonres", "BDate");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgConfiguration.DataSource = GetQueryData().DefaultView;
        dgConfiguration.DataBind();
    }

    private DataTable GetQueryData()
    {
        string strProcedureName = "SFCQUERY.GETSFCCONFIGURATION";
        OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("MODELID",OracleType.VarChar,3),
                                                            new OracleParameter("STATIONID",OracleType.VarChar,20),
                                                            new OracleParameter("DATA",OracleType.Cursor)};
        orapara[0].Value = ddlModel.SelectedValue;
        orapara[1].Value = ddlStationID.SelectedValue;
        orapara[2].Direction = ParameterDirection.Output;
        return ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        
    }

    private void BindList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MODEL");
        dt.Columns.Add("STATION_ID");
        dt.Columns.Add("COLUMN_NAME");
        dt.Columns.Add("COLUMN_DESC");
        dt.Columns.Add("FUNCTION_ID");
        dt.Columns.Add("FUNCTION_VALUE");
        dt.Columns.Add("CREATION_DATE");
        dt.Columns.Add("ROWID");
        dgConfiguration.DataSource = dt.DefaultView;
        dgConfiguration.DataBind();
    }

    private void BindE2PColumn(DropDownList ddlColumnName)
    {
        string strSql = "SELECT COLUMN_NAME FROM ALL_COL_COMMENTS WHERE OWNER = "+ClsCommon.GetSqlString(ddlModel.SelectedValue)+" AND TABLE_NAME='E2PCONFIG' ORDER BY COLUMN_NAME";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        ddlColumnName.DataTextField = "COLUMN_NAME";
        ddlColumnName.DataValueField = "COLUMN_NAME";
        ddlColumnName.DataSource = dt.DefaultView;
        ddlColumnName.DataBind();
    }

    private void BindFunctionType(DropDownList ddlFunctionType)
    {
        string strSql = "SELECT FUNCTION_ID,FUNCTION_NAME||' ('||FUNCTION_DESC||')' FUNCTION_NAME FROM TBLFUNCTION_NAME T ORDER BY FUNCTION_ID ";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        ddlFunctionType.DataTextField = "FUNCTION_NAME";
        ddlFunctionType.DataValueField = "FUNCTION_ID";
        ddlFunctionType.DataSource = dt.DefaultView;
        ddlFunctionType.DataBind();
    }

    private void BindFunctionValue(DropDownList ddlFunctionValue)
    {
        string strSql = "SELECT FUNCTION_VALUE,FUNCTION_VALUE||' ('||FUNCTION_VALUE_DESC||')'FUNCTION_VALUE_DESC FROM TBLFUNCTION_VALUES T ORDER BY FUNCTION_VALUE_ID";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        ddlFunctionValue.DataTextField = "FUNCTION_VALUE_DESC";
        ddlFunctionValue.DataValueField = "FUNCTION_VALUE";
        ddlFunctionValue.DataSource = dt.DefaultView;
        ddlFunctionValue.DataBind();
    }

    private void SetEnabled(bool Astate)
    {
        btnQuery.Enabled = Astate;
        btnAdd.Enabled = Astate;
        ddlModel.Enabled = Astate;
        ddlStationID.Enabled = Astate;

        dgConfiguration.DisablePaging = !Astate;
        dgConfiguration.DisableSetButton = !Astate;
        dgConfiguration.AllowSorting = Astate;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //dgConfiguration.EditItemIndex = 0;
        //dgConfiguration.CurrentPageIndex = 0;


        IsAdd = true;
        IsEdit = false;
        SetEnabled(false);
        DataTable dt = new DataTable();
        if (dgConfiguration.Items.Count > 0)
        {
            dt = GetQueryData();
        }
        else
        {
            dt.Columns.Add("MODEL");
            dt.Columns.Add("STATION_ID");
            dt.Columns.Add("COLUMN_NAME");
            dt.Columns.Add("COLUMN_DESC");
            dt.Columns.Add("FUNCTION_ID");
            dt.Columns.Add("FUNCTION_VALUE");
            dt.Columns.Add("CREATION_DATE");
            dt.Columns.Add("ROWID");
        }

        DataRow dr = dt.NewRow();
        dt.Rows.InsertAt(dr, 0);
        dgConfiguration.EditItemIndex = 0;
        dgConfiguration.CurrentPageIndex = 0;
        dgConfiguration.DataSource = dt.DefaultView;
        dgConfiguration.DataBind();

       // Label aa = (Label)dgConfiguration.Items[0].Cells[2].FindControl("Label1");
        DropDownList ddlTemp = (DropDownList)dgConfiguration.Items[0].Cells[2].FindControl("ddlModel1");
        BindModel(ddlTemp);

        ddlTemp = (DropDownList)dgConfiguration.Items[0].Cells[3].FindControl("ddlStationID");
        BindStationID(ddlTemp);        

        ddlTemp = (DropDownList)dgConfiguration.Items[0].Cells[4].FindControl("ddlColumnName");
        BindE2PColumn(ddlTemp);
        ddlTemp.Items.Insert(0, "");

        ddlTemp = (DropDownList)dgConfiguration.Items[0].Cells[6].FindControl("ddlFunctionType");
        BindFunctionType(ddlTemp);

        ddlTemp = (DropDownList)dgConfiguration.Items[0].Cells[7].FindControl("ddlFunction_Value");
        BindFunctionValue(ddlTemp);

        ((ImageButton)dgConfiguration.Items[0].Cells[6].FindControl("ibtnFunctionType")).Attributes["onclick"] = "OpenForm('./WFrmFunctionType.aspx');return true;";

    }
    protected void ddlFunctionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTemp = (DropDownList)dgConfiguration.Items[iCurrent].Cells[7].FindControl("ddlFunction_Value");
        DropDownList ddlTemp1 = (DropDownList)dgConfiguration.Items[iCurrent].Cells[6].FindControl("ddlFunctionType");
        TextBox tbTemp = (TextBox)dgConfiguration.Items[iCurrent].Cells[7].FindControl("tbFunction_Value");
        //string dd = ddlTemp.SelectedItem.Text;
        switch (ddlTemp1.SelectedIndex) 
        {
            case 0:
                ddlTemp.Visible = true;
                tbTemp.Visible = false;
                break;
            default :
                ddlTemp.Visible = false;
                tbTemp.Visible = true;
                break;
        }
    }

    protected void dgConfiguration_EditCommand(object source, DataGridCommandEventArgs e)
    {
        ViewState["rowid"] = ((Label)e.Item.Cells[9].FindControl("Label6")).Text;
        string strFunctionValue = ((Label)e.Item.Cells[7].FindControl("Label5")).Text;
        IsAdd = false;
        IsEdit = true;
        SetEnabled(false);

        iCurrent = e.Item.ItemIndex;
        dgConfiguration.EditItemIndex = iCurrent;

        dgConfiguration.DataSource = GetQueryData().DefaultView;
        dgConfiguration.DataBind();

        string strTemp = ((Label)e.Item.Cells[2].FindControl("Label1")).Text;
        DropDownList ddlTemp = (DropDownList)dgConfiguration.Items[iCurrent].Cells[2].FindControl("ddlModel1");
        ddlTemp.Items.Clear();
        BindModel(ddlTemp);
        ddlTemp.SelectedIndex = ddlTemp.Items.IndexOf(ddlTemp.Items.FindByValue(strTemp));

        strTemp = ((Label)e.Item.Cells[3].FindControl("Label2")).Text;
        ddlTemp = (DropDownList)dgConfiguration.Items[iCurrent].Cells[3].FindControl("ddlStationID");
        ddlTemp.Items.Clear();
        BindStationID(ddlTemp);
        ddlTemp.SelectedIndex = ddlTemp.Items.IndexOf(ddlTemp.Items.FindByValue(strTemp));

        strTemp = ((Label)e.Item.Cells[4].FindControl("Label3")).Text;
        ddlTemp = (DropDownList)dgConfiguration.Items[iCurrent].Cells[4].FindControl("ddlColumnName");
        ddlTemp.Items.Clear();
        BindE2PColumn(ddlTemp);
        ddlTemp.Items.Insert(0, "");
        ddlTemp.SelectedIndex = ddlTemp.Items.IndexOf(ddlTemp.Items.FindByValue(strTemp));

        strTemp = ((Label)e.Item.Cells[6].FindControl("Label4")).Text;
        ddlTemp = (DropDownList)dgConfiguration.Items[iCurrent].Cells[6].FindControl("ddlFunctionType");
        ddlTemp.Items.Clear();
        BindFunctionType(ddlTemp);
        ddlTemp.SelectedIndex = ddlTemp.Items.IndexOf(ddlTemp.Items.FindByText(strTemp));

        int SelectedIndex = ddlTemp.SelectedIndex;
        strTemp = ((Label)e.Item.Cells[7].FindControl("Label5")).Text;
        ddlTemp = (DropDownList)dgConfiguration.Items[iCurrent].Cells[7].FindControl("ddlFunction_Value");
        ddlTemp.Items.Clear();
        BindFunctionValue(ddlTemp);
        ddlTemp.SelectedIndex = ddlTemp.Items.IndexOf(ddlTemp.Items.FindByValue(strTemp));
        if (!SelectedIndex.Equals(0))
        {
            ddlTemp.Visible = false;
            TextBox tbvalue = (TextBox)dgConfiguration.Items[iCurrent].Cells[7].FindControl("tbFunction_Value");
            tbvalue.Visible = true;
            tbvalue.Text = strFunctionValue;
        }

        ((ImageButton)dgConfiguration.Items[e.Item.ItemIndex].Cells[6].FindControl("ibtnFunctionType")).Attributes["onclick"] = "OpenForm('./WFrmFunctionType.aspx');return true;";


    }

    protected void dgConfiguration_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        IsAdd = false;
        IsEdit = false;
        SetEnabled(true);
        dgConfiguration.EditItemIndex = -1;
        dgConfiguration.DataSource = GetQueryData().DefaultView;
        dgConfiguration.DataBind();
    }

    protected void dgConfiguration_ItemCreated(object sender, DataGridItemEventArgs e)
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

    protected void dgConfiguration_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if ((!e.CommandName.ToUpper().Equals("EDIT")) && (!e.CommandName.ToUpper().Equals("UPDATE")))
        {
            if (e.CommandName.ToUpper().Equals("SET"))
            {
                if (dgConfiguration.CurrentStatus.ToUpper().Equals("SET"))
                {
                    btnAdd.Enabled = false;
                    btnQuery.Enabled = false;
                    BindList();
                }
                else
                {
                    btnAdd.Enabled = true;
                    btnQuery.Enabled = true;
                    dgConfiguration.DataSource = GetQueryData().DefaultView;
                    dgConfiguration.DataBind();
                }
            }
        }        
    }
    protected void dgConfiguration_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.EditItem))
        {
            if (IsEdit)
            {
                if (e.Item.ItemIndex == (int)ViewState["iCurrent"])
                {
                    ((LinkButton)(e.Item.Cells[0].Controls[1])).Enabled = true;
                    ((LinkButton)(e.Item.Cells[1].Controls[1])).Enabled = false;
                    ((DropDownList)e.Item.Cells[2].Controls[1]).Enabled = false;
                    ((DropDownList)e.Item.Cells[3].Controls[1]).Enabled = false;
                    ((DropDownList)e.Item.Cells[4].Controls[1]).Enabled = false;
                    ((TextBox)e.Item.Cells[8].Controls[0]).Enabled = false;
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
                    ((TextBox)e.Item.Cells[8].Controls[0]).Enabled = false;
                }
                else
                {
                    ((LinkButton)(e.Item.Cells[0].Controls[1])).Enabled = false;
                    ((LinkButton)(e.Item.Cells[1].Controls[1])).Enabled = false;
                }
            }
        }
    }
    protected void dgConfiguration_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string rowid = ((Label)e.Item.Cells[9].FindControl("Label6")).Text;

            OracleParameter[] orapara = new OracleParameter[] {new OracleParameter("OPERATION",OracleType.VarChar,10),
                                                            new OracleParameter("STRDATA",OracleType.VarChar,1000),
                                                            new OracleParameter("CONDITION",OracleType.VarChar,20)};
            orapara[0].Value = "DELETE";
            orapara[1].Value = "";
            orapara[2].Value = rowid;
            string strProcedureName = "SFCQUERY.UPDATE_TBLCONTROLCONFIGURATION";
            ClsGlobal.objDataConnect.DataExecute(strProcedureName, orapara);
            ClsCommon.ShowMessage(this.Page, ViewState["delsucc"].ToString());
        }
        catch
        {
            ClsCommon.ShowMessage(this.Page, ViewState["delfail"].ToString());
        }
        finally
        {
            dgConfiguration.DataSource = GetQueryData().DefaultView;
            dgConfiguration.DataBind();
        }
        
    }
    protected void dgConfiguration_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        clsSFCConfiguration objConfig = new clsSFCConfiguration();
        try
        {
            string rowid = ViewState["rowid"].ToString();
            objConfig.FModel = ((DropDownList)e.Item.Cells[2].FindControl("ddlModel1")).SelectedValue;
            objConfig.FStationID = ((DropDownList)e.Item.Cells[3].FindControl("ddlStationID")).SelectedValue;
            objConfig.FColumnName = ((DropDownList)e.Item.Cells[4].FindControl("ddlColumnName")).SelectedValue;
            objConfig.FColumnDesc = ((TextBox)e.Item.Cells[5].Controls[0]).Text;
            objConfig.FFunctionID = ((DropDownList)e.Item.Cells[6].FindControl("ddlFunctionType")).SelectedValue;
            if (((DropDownList)e.Item.Cells[6].FindControl("ddlFunctionType")).SelectedIndex.Equals(0))
                objConfig.FFunctionValue = ((DropDownList)e.Item.Cells[7].FindControl("ddlFunction_Value")).SelectedItem.Text;
            else
                objConfig.FFunctionValue = ((TextBox)e.Item.Cells[7].FindControl("tbFunction_Value")).Text.Trim();
            string strData = objConfig.FModel + "," + objConfig.FStationID + "," + objConfig.FColumnName + "," + objConfig.FColumnDesc + "," + objConfig.FFunctionID + "," + objConfig.FFunctionValue + ",";

            OracleParameter[] orapara = new OracleParameter[] {new OracleParameter("OPERATION",OracleType.VarChar,10),
                                                            new OracleParameter("STRDATA",OracleType.VarChar,1000),
                                                            new OracleParameter("CONDITION",OracleType.VarChar,20)};
            if(IsAdd)
                orapara[0].Value = "INSERT";
            else if(IsEdit)
                orapara[0].Value = "UPDATE";
            orapara[1].Value = strData;
            orapara[2].Value = rowid ;
            string strProcedureName = "SFCQUERY.UPDATE_TBLCONTROLCONFIGURATION";
            ClsGlobal.objDataConnect.DataExecute(strProcedureName, orapara);

            SetEnabled(true);
            IsAdd = false;
            IsEdit = false;
            dgConfiguration.EditItemIndex = -1;
            dgConfiguration.DataSource = GetQueryData().DefaultView;
            dgConfiguration.DataBind();

            ClsCommon.ShowMessage(this.Page, ViewState["SaveSuccess"].ToString());
        }
        catch
        {
            ClsCommon.ShowMessage(this.Page, ViewState["SaveFail"].ToString());
        }
        finally
        {
            objConfig = null;
            dgConfiguration.DataSource = GetQueryData().DefaultView;
            dgConfiguration.DataBind();
        }
        
    }
    protected void ibtnFunctionType_Click(object sender, ImageClickEventArgs e)
    {
        DropDownList ddlTemp = (DropDownList)dgConfiguration.Items[0].Cells[6].FindControl("ddlFunctionType");
        BindFunctionType(ddlTemp);
    }
}
