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

public partial class Boundary_wfrmModelMaintenance : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage(); 
        }

    }

    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
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

    private void BindData(string model)
    {
        dgModel.Visible = true;
        string strsql = "select * from SFC.CMCS_SFC_MODEL where model='" + model.ToUpper() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgModel.DataSource = this.GetIDTable(dt1).DefaultView;
            dgModel.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該幾種的維護信息...');</script>");
            return;
        }
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "select * from SFC.CMCS_SFC_MODEL where model='" + this.txtModel.Text.Trim().ToUpper() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Visible = true;
            dgModel.Visible = true;
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgModel.DataSource = this.GetIDTable(dt1).DefaultView;
            dgModel.DataBind();
        }
        else
        {
            lbcount.Visible = false;
            dgModel.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該幾種的維護信息...');</script>");
            return;
        }
    }

    protected void dgModel_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgModel.PageSize) * (dgModel.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[34].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?');");

        if (e.Item.ItemType == ListItemType.EditItem)
        {
            string strnewflag = ((DataRowView)e.Item.DataItem).Row["NEW_FLAG"].ToString();
            string strgiftflag = ((DataRowView)e.Item.DataItem).Row["GIFT_FLAG"].ToString();
            string strskuflag = ((DataRowView)e.Item.DataItem).Row["SKU_FLAG"].ToString();
            string strcartonflag = ((DataRowView)e.Item.DataItem).Row["CARTON_FLAG"].ToString();
            string strabled = ((DataRowView)e.Item.DataItem).Row["ABLED"].ToString();
            string stre2pflag = ((DataRowView)e.Item.DataItem).Row["E2P_FLAG"].ToString();
            string strfqcflag = ((DataRowView)e.Item.DataItem).Row["FQC_FLAG"].ToString();
            string strmsnflag = ((DataRowView)e.Item.DataItem).Row["MSN_FLAG"].ToString();
            string strpidflag = ((DataRowView)e.Item.DataItem).Row["PID_FLAG"].ToString();
            string strpcbaflag = ((DataRowView)e.Item.DataItem).Row["PCBA_FLAG"].ToString();
            string stroqcflag = ((DataRowView)e.Item.DataItem).Row["OQC_FLAG"].ToString();
            string stroobflag = ((DataRowView)e.Item.DataItem).Row["OOB_FLAG"].ToString();
            string strcdmaflag = ((DataRowView)e.Item.DataItem).Row["CDMA_FLAG"].ToString();
            string strimeilinkflag = ((DataRowView)e.Item.DataItem).Row["IMEI_LINK_FLAG"].ToString();
            string strthirdidlinkflag = ((DataRowView)e.Item.DataItem).Row["THIRDID_LINK_FLAG"].ToString();
            string strporderflag = ((DataRowView)e.Item.DataItem).Row["PORDER_FLAG"].ToString();
            string strglmlinkflag = ((DataRowView)e.Item.DataItem).Row["GLM_LINK_FLAG"].ToString();
            string strshipcountryflag = ((DataRowView)e.Item.DataItem).Row["SHIPCOUNTRY_FLAG"].ToString();
            string strreworkflag = ((DataRowView)e.Item.DataItem).Row["REWORK_FLAG"].ToString();
            string strdatatypeflag = ((DataRowView)e.Item.DataItem).Row["DATA_TYPE_FLAG"].ToString();
            string strimeiflag = ((DataRowView)e.Item.DataItem).Row["IMEI_FLAG"].ToString();
            string strpicassoflag = ((DataRowView)e.Item.DataItem).Row["PICASSO_FLAG"].ToString();
            string strshiptypeflag = ((DataRowView)e.Item.DataItem).Row["SHIP_TYPE_FLAG"].ToString();
            string strweightflag = ((DataRowView)e.Item.DataItem).Row["WEIGHT_FLAG"].ToString();

            DropDownList ddlnewflag = (DropDownList)e.Item.FindControl("ddlEnewflag");
            DropDownList ddlgiftflag = (DropDownList)e.Item.FindControl("ddlEgiftflag");
            DropDownList ddlskuflag = (DropDownList)e.Item.FindControl("ddlEskuflag");
            DropDownList ddlcartonflag = (DropDownList)e.Item.FindControl("ddlEcartonflag");
            DropDownList ddlabled = (DropDownList)e.Item.FindControl("ddlEabled");
            DropDownList ddle2pflag = (DropDownList)e.Item.FindControl("ddlEe2pflag");
            DropDownList ddlfqcflag = (DropDownList)e.Item.FindControl("ddlEfqcflag");
            DropDownList ddlmsnflag = (DropDownList)e.Item.FindControl("ddlEmsnflag");
            DropDownList ddlpidflag = (DropDownList)e.Item.FindControl("ddlEpidflag");
            DropDownList ddlpcbaflag = (DropDownList)e.Item.FindControl("ddlEpcbaflag");
            DropDownList ddloqcflag = (DropDownList)e.Item.FindControl("ddlEoqcflag");
            DropDownList ddloobflag = (DropDownList)e.Item.FindControl("ddlEoobflag");
            DropDownList ddlcdmaflag = (DropDownList)e.Item.FindControl("ddlEcdmaflag");
            DropDownList ddlimeilinkflag = (DropDownList)e.Item.FindControl("ddlEimeilinkflag");
            DropDownList ddlthirdidlinkflag = (DropDownList)e.Item.FindControl("ddlEthirdidlinkflag");
            DropDownList ddlporderflag = (DropDownList)e.Item.FindControl("ddlEporderflag");
            DropDownList ddlglmlinkflag = (DropDownList)e.Item.FindControl("ddlEglmlinkflag");
            DropDownList ddlshipcountryflag = (DropDownList)e.Item.FindControl("ddlEshipcountryflag");
            DropDownList ddlreworkflag = (DropDownList)e.Item.FindControl("ddlEreworkflag");
            DropDownList ddldatatypeflag = (DropDownList)e.Item.FindControl("ddlEdatatypeflag");
            DropDownList ddlimeiflag = (DropDownList)e.Item.FindControl("ddlEimeiflag");
            DropDownList ddlpicassoflag = (DropDownList)e.Item.FindControl("ddlEpicassoflag");
            DropDownList ddlshiptypeflag = (DropDownList)e.Item.FindControl("ddlEshiptypeflag");
            DropDownList ddlweightflag = (DropDownList)e.Item.FindControl("ddlEweightflag");

            ListItem itemnewflag = ddlnewflag.Items.FindByText(strnewflag);
            ListItem itemgiftflag = ddlgiftflag.Items.FindByText(strgiftflag);
            ListItem itemskuflag = ddlskuflag.Items.FindByText(strskuflag);
            ListItem itemcartonflag = ddlcartonflag.Items.FindByText(strcartonflag);
            ListItem itemabled = ddlabled.Items.FindByText(strabled);
            ListItem iteme2pflag = ddle2pflag.Items.FindByText(stre2pflag);
            ListItem itemfqcflag = ddlfqcflag.Items.FindByText(strfqcflag);
            ListItem itemmsnflag = ddlmsnflag.Items.FindByText(strmsnflag);
            ListItem itempidflag = ddlpidflag.Items.FindByText(strpidflag);
            ListItem itempcbaflag = ddlpcbaflag.Items.FindByText(strpcbaflag);
            ListItem itemoqcflag = ddloqcflag.Items.FindByText(stroqcflag);
            ListItem itemoobflag = ddloobflag.Items.FindByText(stroobflag);
            ListItem itemcdmaflag = ddlcdmaflag.Items.FindByText(strcdmaflag);
            ListItem itemimeilinkflag = ddlimeilinkflag.Items.FindByText(strimeilinkflag);
            ListItem itemthirdidlinkflag = ddlthirdidlinkflag.Items.FindByText(strthirdidlinkflag);
            ListItem itemporderflag = ddlporderflag.Items.FindByText(strporderflag);
            ListItem itemglmlinkflag = ddlglmlinkflag.Items.FindByText(strglmlinkflag);
            ListItem itemshipcountryflag = ddlshipcountryflag.Items.FindByText(strshipcountryflag);
            ListItem itemreworkflag = ddlreworkflag.Items.FindByText(strreworkflag);
            ListItem itemdatatypeflag = ddldatatypeflag.Items.FindByText(strdatatypeflag);
            ListItem itemimeiflag = ddlimeiflag.Items.FindByText(strimeiflag);
            ListItem itempicassoflag = ddlpicassoflag.Items.FindByText(strpicassoflag);
            ListItem itemshiptypeflag = ddlshiptypeflag.Items.FindByText(strshiptypeflag);
            ListItem itemweightflag = ddlweightflag.Items.FindByText(strweightflag);
            if (itemnewflag != null)
                itemnewflag.Selected = true;
            if (itemgiftflag != null)
                itemgiftflag.Selected = true;
            if (itemskuflag != null)
                itemskuflag.Selected = true;
            if (itemcartonflag != null)
                itemcartonflag.Selected = true;
            if (itemabled != null)
                itemabled.Selected = true;
            if (iteme2pflag != null)
                iteme2pflag.Selected = true;
            if (itemfqcflag != null)
                itemfqcflag.Selected = true;
            if (itemmsnflag != null)
                itemmsnflag.Selected = true;
            if (itempidflag != null)
                itempidflag.Selected = true;
            if (itempcbaflag != null)
                itempcbaflag.Selected = true;
            if (itemoqcflag != null)
                itemoqcflag.Selected = true;
            if (itemoobflag != null)
                itemoobflag.Selected = true;
            if (itemcdmaflag != null)
                itemcdmaflag.Selected = true;
            if (itemimeilinkflag != null)
                itemimeilinkflag.Selected = true;
            if (itemthirdidlinkflag != null)
                itemthirdidlinkflag.Selected = true;
            if (itemporderflag != null)
                itemporderflag.Selected = true;
            if (itemglmlinkflag != null)
                itemglmlinkflag.Selected = true;
            if (itemshipcountryflag != null)
                itemshipcountryflag.Selected = true;
            if (itemreworkflag != null)
                itemreworkflag.Selected = true;
            if (itemdatatypeflag != null)
                itemdatatypeflag.Selected = true;
            if (itemimeiflag != null)
                itemimeiflag.Selected = true;
            if (itempicassoflag != null)
                itempicassoflag.Selected = true;
            if (itemshiptypeflag != null)
                itemshiptypeflag.Selected = true;
            if (itemweightflag != null)
                itemweightflag.Selected = true;
        }        
    }
    protected void dgModel_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strid = ((Label)e.Item.Cells[1].Controls[1]).Text.Trim();
        string strModel = ((Label)e.Item.Cells[2].Controls[1]).Text.Trim();
        int intid = Convert.ToInt32(strid);
 
        OracleConnection orcn = null;
        OracleDataAdapter da;
        DataSet myds;
        myds = new DataSet();

        try
        {
            orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");

            orcn.Open();

            da = new OracleDataAdapter("select * from SFC.CMCS_SFC_MODEL where model='" + strModel + "' and id="+intid, orcn);
            da.Fill(myds, "CMCS_SFC_MODEL");

            //----------- update part-----------
            //DataTable myDt = myds.Tables["CDMA_MOTO_ORDERNO"];
            //myDt.PrimaryKey = new DataColumn[] { myDt.Columns["ORDER_NUMBER"] };
            //出错就是因为少了上面这一句。这条语句指定了DataTable的主键。或者用下一条语句也可以，下一条语句是让适配器自动加上表的架构（Key约束）
            //da.MissingSchemaAction = MissingSchemaAction.AddWithKey; 

            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["DESCRIPTION"] = ((TextBox)e.Item.Cells[3].Controls[3]).Text;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["CUSTOMER_TYPE"] = ((TextBox)e.Item.Cells[4].Controls[3]).Text;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["NEW_FLAG"] = ((DropDownList)e.Item.Cells[5].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["GIFT_FLAG"] = ((DropDownList)e.Item.Cells[6].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["SKU_FLAG"] = ((DropDownList)e.Item.Cells[7].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["CARTON_FLAG"] = ((DropDownList)e.Item.Cells[8].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["DB_USER"] = ((TextBox)e.Item.Cells[9].Controls[3]).Text;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["ABLED"] = ((DropDownList)e.Item.Cells[10].Controls[3]).SelectedValue;
            if (((TextBox)e.Item.Cells[11].Controls[3]).Text.Trim().Length > 0)
                myds.Tables["CMCS_SFC_MODEL"].Rows[0]["MASS"] = ((TextBox)e.Item.Cells[11].Controls[3]).Text;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["CUSTOMER_NAME"] = ((TextBox)e.Item.Cells[12].Controls[3]).Text;
            if (((TextBox)e.Item.Cells[13].Controls[3]).Text.Trim().Length > 0)
                myds.Tables["CMCS_SFC_MODEL"].Rows[0]["ALERT_VALUE"] = ((TextBox)e.Item.Cells[13].Controls[3]).Text;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["E2P_FLAG"] = ((DropDownList)e.Item.Cells[14].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["FQC_FLAG"] = ((DropDownList)e.Item.Cells[15].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["MSN_FLAG"] = ((DropDownList)e.Item.Cells[16].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["PID_FLAG"] = ((DropDownList)e.Item.Cells[17].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["PCBA_FLAG"] = ((DropDownList)e.Item.Cells[18].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["OQC_FLAG"] = ((DropDownList)e.Item.Cells[19].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["OOB_FLAG"] = ((DropDownList)e.Item.Cells[20].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["CDMA_FLAG"] = ((DropDownList)e.Item.Cells[21].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["IMEI_LINK_FLAG"] = ((DropDownList)e.Item.Cells[22].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["THIRDID_LINK_FLAG"] = ((DropDownList)e.Item.Cells[23].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["PORDER_FLAG"] = ((DropDownList)e.Item.Cells[24].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["GLM_LINK_FLAG"] = ((DropDownList)e.Item.Cells[25].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["SHIPCOUNTRY_FLAG"] = ((DropDownList)e.Item.Cells[26].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["REWORK_FLAG"] = ((DropDownList)e.Item.Cells[27].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["DATA_TYPE_FLAG"] = ((DropDownList)e.Item.Cells[28].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["IMEI_FLAG"] = ((DropDownList)e.Item.Cells[29].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["PICASSO_FLAG"] = ((DropDownList)e.Item.Cells[30].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["SHIP_TYPE_FLAG"] = ((DropDownList)e.Item.Cells[31].Controls[3]).SelectedValue;
            myds.Tables["CMCS_SFC_MODEL"].Rows[0]["WEIGHT_FLAG"] = ((DropDownList)e.Item.Cells[32].Controls[3]).SelectedValue; 

            OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
            da.Update(myds, "CMCS_SFC_MODEL");
            //da.Update(myds, "CDMA_MOTO_ORDERNO");
            orcn.Close();
        }

        catch (Exception ex)
        {
            orcn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種信息已存在,請檢查...');</script>");
            return; 
        }
        dgModel.EditItemIndex = -1;
        BindData(strModel);
    }
    protected void dgModel_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim().ToUpper();
        dgModel.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strModel);
    }
    protected void dgModel_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim().ToUpper();
        dgModel.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strModel);
    }
    protected void dgModel_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgModel.ShowFooter = true;
            string strsql = "select * from SFC.CMCS_SFC_MODEL where model='" + this.txtModel.Text.Trim().ToUpper() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgModel.DataSource = this.GetIDTable(dt1).DefaultView;
                dgModel.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgModel.ShowFooter = false;
            string strsql = "select * from SFC.CMCS_SFC_MODEL where model= '" + this.txtModel.Text.Trim().ToUpper() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgModel.DataSource = this.GetIDTable(dt1).DefaultView;
                dgModel.DataBind();
            }
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgModel.ShowFooter = false;

            OracleConnection orcn = null;
            OracleDataAdapter da;
            DataSet myds;
            myds = new DataSet();

            try
            {
                orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");

                orcn.Open();

                da = new OracleDataAdapter("select * from SFC.CMCS_SFC_MODEL where 0=1 ", orcn);
                da.Fill(myds, "CMCS_SFC_MODEL");

                //----------- Insert new row----------

                DataRow mydr = myds.Tables["CMCS_SFC_MODEL"].NewRow();

                if (((TextBox)e.Item.Cells[1].Controls[1]).Text.Trim().Length > 0)
                    mydr["ID"] = ((TextBox)e.Item.Cells[1].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('ID欄位不能為空...');</script>");
                    return;
                }
                if (((TextBox)e.Item.Cells[2].Controls[1]).Text.Trim().Length > 0)
                    mydr["MODEL"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text.Trim().ToUpper();
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Model欄位不能為空...');</script>");
                    return;
                }
                if (((TextBox)e.Item.Cells[3].Controls[1]).Text.Trim().Length > 0)
                    mydr["DESCRIPTION"] = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Description欄位不能為空...');</script>");
                    return;
                }
                mydr["CUSTOMER_TYPE"] = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
                mydr["NEW_FLAG"] = ((DropDownList)e.Item.Cells[5].Controls[1]).SelectedValue;
                mydr["GIFT_FLAG"] = ((DropDownList)e.Item.Cells[6].Controls[1]).SelectedValue;
                mydr["SKU_FLAG"] = ((DropDownList)e.Item.Cells[7].Controls[1]).SelectedValue;
                mydr["CARTON_FLAG"] = ((DropDownList)e.Item.Cells[8].Controls[1]).SelectedValue;
                mydr["DB_USER"] = ((TextBox)e.Item.Cells[9].Controls[1]).Text;
                mydr["ABLED"] = ((DropDownList)e.Item.Cells[10].Controls[1]).SelectedValue;
                if (((TextBox)e.Item.Cells[11].Controls[1]).Text.Trim().Length>0)
                    mydr["MASS"] = Convert.ToInt32(((TextBox)e.Item.Cells[11].Controls[1]).Text);
                mydr["CUSTOMER_NAME"] = ((TextBox)e.Item.Cells[12].Controls[1]).Text;
                if (((TextBox)e.Item.Cells[13].Controls[1]).Text.Trim().Length > 0)
                    mydr["ALERT_VALUE"] = ((TextBox)e.Item.Cells[13].Controls[1]).Text;
                mydr["E2P_FLAG"] = ((DropDownList)e.Item.Cells[14].Controls[1]).SelectedValue;
                mydr["FQC_FLAG"] = ((DropDownList)e.Item.Cells[15].Controls[1]).SelectedValue;
                mydr["MSN_FLAG"] = ((DropDownList)e.Item.Cells[16].Controls[1]).SelectedValue;
                mydr["PID_FLAG"] = ((DropDownList)e.Item.Cells[17].Controls[1]).SelectedValue;
                mydr["PCBA_FLAG"] = ((DropDownList)e.Item.Cells[18].Controls[1]).SelectedValue;
                mydr["OQC_FLAG"] = ((DropDownList)e.Item.Cells[19].Controls[1]).SelectedValue;
                mydr["OOB_FLAG"] = ((DropDownList)e.Item.Cells[20].Controls[1]).SelectedValue;
                mydr["CDMA_FLAG"] = ((DropDownList)e.Item.Cells[21].Controls[1]).SelectedValue;
                mydr["IMEI_LINK_FLAG"] = ((DropDownList)e.Item.Cells[22].Controls[1]).SelectedValue;
                mydr["THIRDID_LINK_FLAG"] = ((DropDownList)e.Item.Cells[23].Controls[1]).SelectedValue;
                mydr["PORDER_FLAG"] = ((DropDownList)e.Item.Cells[24].Controls[1]).SelectedValue;
                mydr["GLM_LINK_FLAG"] = ((DropDownList)e.Item.Cells[25].Controls[1]).SelectedValue;
                mydr["SHIPCOUNTRY_FLAG"] = ((DropDownList)e.Item.Cells[26].Controls[1]).SelectedValue;
                mydr["REWORK_FLAG"] = ((DropDownList)e.Item.Cells[27].Controls[1]).SelectedValue;
                mydr["DATA_TYPE_FLAG"] = ((DropDownList)e.Item.Cells[28].Controls[1]).SelectedValue;
                mydr["IMEI_FLAG"] = ((DropDownList)e.Item.Cells[29].Controls[1]).SelectedValue;
                mydr["PICASSO_FLAG"] = ((DropDownList)e.Item.Cells[30].Controls[1]).SelectedValue;
                mydr["SHIP_TYPE_FLAG"] = ((DropDownList)e.Item.Cells[31].Controls[1]).SelectedValue;
                mydr["WEIGHT_FLAG"] = ((DropDownList)e.Item.Cells[32].Controls[1]).SelectedValue;

                myds.Tables["CMCS_SFC_MODEL"].Rows.Add(mydr);

                // ---------------end -------------------

                OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
                da.Update(myds, "CMCS_SFC_MODEL");
                orcn.Close();
            }
            catch (Exception ex)
            {
                orcn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種信息已存在,請檢查...');</script>");
                return;
            }
            dgModel.EditItemIndex = -1;
            string strModel = txtModel.Text.Trim().ToUpper();
            BindData(strModel);
        }

        if (e.CommandName == "ItemDelete")
        {
            string strid = ((Label)e.Item.Cells[1].Controls[1]).Text;
            string strModel = ((Label)e.Item.Cells[2].Controls[1]).Text;
            int intid = Convert.ToInt32(strid);
            string strsql = "delete from SFC.CMCS_SFC_MODEL where Model='" + strModel + "' and id=" + intid;
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgModel.EditItemIndex = -1;
            string strsql1 = "SELECT * FROM SFC.CMCS_SFC_MODEL where Model='" + this.txtModel.Text.Trim().ToUpper() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgModel.DataSource = this.GetIDTable(dt1).DefaultView;
                dgModel.DataBind();
            }
            else
            {
                lbcount.Visible = false;
                dgModel.Visible = false;
            }
        }
    }
    protected void dgModel_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string strModel = txtModel.Text.Trim().ToUpper();
        dgModel.CurrentPageIndex = e.NewPageIndex;
        string strsql = "select * from SFC.CMCS_SFC_MODEL where model= '" + strModel + "'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgModel.DataSource = this.GetIDTable(dt).DefaultView;
            dgModel.DataBind();
            lbcount.Text = "Current Page:" + (dgModel.CurrentPageIndex + 1).ToString() + "/" + dgModel.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

            dt.Dispose();
        }
        else
        {
            string strsql1 = "SELECT * FROM SFC.CDMA_MOTO_ORDERNO";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            dgModel.DataSource = this.GetIDTable(dt1).DefaultView;
            dgModel.DataBind();
            lbcount.Text = "Current Page:" + (dgModel.CurrentPageIndex + 1).ToString() + "/" + dgModel.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();

            dt1.Dispose();
        }
    }
}
