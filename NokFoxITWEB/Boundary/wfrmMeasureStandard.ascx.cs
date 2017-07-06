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
using System.Data.SqlClient;

public partial class Boundary_wfrmMeasureStandard : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            string model = txtModel.Text.Trim();
            BindData(model);
        }

        
        //string strSql = "select * from SHP.MES_PACK_MEASURES_STANDARD";
        //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        //dgMeasure.DataSource = dt.DefaultView;
        //dgMeasure.DataBind();
    }

    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");       
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query"); 

        //dgTestStationData.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Items");
        //dgTestStationData.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Lo_Limit");
        //dgTestStationData.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Up_Limit");
        //dgTestStationData.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Max");
        //dgTestStationData.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Min");
        //dgTestStationData.Columns[5].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Average");
        //dgTestStationData.Columns[6].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Tetval_Sample");
        //dgTestStationData.Columns[7].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Tetval_CP");
        //dgTestStationData.Columns[8].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Tetval_CPK");
    }

    private void BindData(string model)
    {
        dgMeasure.Visible = true;
        string strsql = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '"+model.ToUpper()+"%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
            dgMeasure.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種的稱重標準尚未維護！！');</script>");
            return;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '"+this.txtModel.Text.Trim().ToUpper()+"%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count; 

            dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
            dgMeasure.DataBind();
        }
        else
        {
            dgMeasure.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種的稱重標準尚未維護！！');</script>");
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

    //protected void gvMeasure_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvMeasure.PageIndex = e.NewPageIndex;
    //    binddata();
    //}
    //protected void gvMeasure_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gvMeasure.EditIndex = -1;
    //    binddata();
    //}
    //protected void gvMeasure_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvMeasure.EditIndex = e.NewEditIndex;
    //    binddata();
    //}
    //protected void gvMeasure_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string strQModel = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbModelID")).Text.Trim().ToUpper();
    //    string strQcountry = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbCountry")).Text.Trim();
    //    string strQmax = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbMaxWeight")).Text.Trim();
    //    string strQmin = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbMinWeight")).Text.Trim();
    //    string strQqty = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbCartonQty")).Text.Trim();
    //    string strQarea = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbArea")).Text.Trim();

    //    string strHModel = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtModelID")).Text.Trim().ToUpper();
    //    string strHcountry = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtCountry")).Text.Trim();
    //    string strHmax = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtaxWeight")).Text.Trim();
    //    string strHmin = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtMinWeight")).Text.Trim();
    //    string strHqty = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtCartonQty")).Text.Trim();
    //    string strHarea = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtFArea")).Text.Trim();
    //    if (!strHModel.Equals("") && !strHmax.Equals("") && !strHmin.Equals(""))
    //    {
    //        string strsql = "update SHP.MES_PACK_MEASURES_STANDARD set MODEL_ID='" + strHModel + "',COUNTRY_NAME='" + strHcountry
    //            + "',MAX_WEIGHT='" + strHmax + "',MIN_WEIGHT='" + strHmin + "',CARTON_QTY='" + strHqty + "',AREA='" + strHarea
    //            + "' where MODEL_ID='" + strQModel + "',COUNTRY_NAME='" + strQcountry + "',MAX_WEIGHT='" + strQmax + "',MIN_WEIGHT='" +
    //            strQmin + "',CARTON_QTY='" + strQqty + "',AREA='" + strQarea + "'";
    //        ClsGlobal.objDataConnect.DataExecute(strsql);
    //        gvMeasure.EditIndex = -1;
    //        binddata();
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種與重量範圍有誤！！');</script>");
    //        return;
    //    }
    //}   

    //private string ascOrDesc
    //{
    //    get
    //    {
    //        if (ViewState["ascOrDesc"] == null) ViewState["ascOrDesc"] = "ASC";
    //        return ViewState["ascOrDesc"].ToString();
    //    }
    //    set { ViewState["ascOrDesc"] = value; }
    //} 


    //protected void gvMeasure_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    //string oldExpression = gvMeasure.SortExpression;
    //    //string newExpression = e.SortExpression;
    //    //if (oldExpression.IndexOf(newExpression) < 0)
    //    //{
    //    //    if (oldExpression.Length > 0)
    //    //        e.SortExpression = newExpression + "," + oldExpression;
    //    //    else
    //    //        e.SortExpression = newExpression;
    //    //}
    //    //else
    //    //{
    //    //    e.SortExpression = oldExpression;
    //    //}  
    //    if (this.ascOrDesc == "ASC")
    //    {
    //        this.ascOrDesc = "DESC";
    //    }
    //    else
    //    {
    //        this.ascOrDesc = "ASC";
    //    }
    //    //BArticle barticle = new BArticle();
    //    //this.gvMeasure.DataSource = barticle.ListByArticleTypeID(0, "", 0, e.SortExpression, this.ascOrDesc);
    //    //this.gvMeasure.DataBind(); 


    //}
    //protected void gvMeasure_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "AddItem")//点“新增”按钮
    //    {
    //        gvMeasure.ShowFooter = true;

    //    }
    //    if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
    //    {
    //        gvMeasure.ShowFooter = false;

    //    }
    //    if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
    //    {
    //        gvMeasure.ShowFooter = false;
    //        string strFModel;
    //        string strFcountry;
    //        string strFmax;
    //        string strFmin;
    //        string strFqty;
    //        string strFarea;
    //        if (((TextBox)gvMeasure.FooterRow.FindControl("txtFModelID")).Text.Trim().ToUpper() == null)
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種不能為空！！');</script>");
    //            return;
    //        }
    //        else
    //            strFModel = ((TextBox)gvMeasure.FooterRow.FindControl("txtFModelID")).Text.Trim().ToUpper();
    //        if (((TextBox)gvMeasure.FooterRow.FindControl("txtFCountry")).Text.Trim().ToUpper() == null)
    //            strFcountry = " ";
    //        else
    //            strFcountry = ((TextBox)gvMeasure.FooterRow.FindControl("txtFCountry")).Text.Trim();
    //        if (((TextBox)gvMeasure.FooterRow.FindControl("txtFMaxWeight")) == null)
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入最大重量！！');</script>");
    //            return;
    //        }
    //        else
    //            strFmax = ((TextBox)gvMeasure.FooterRow.FindControl("txtFMaxWeight")).Text.Trim().ToUpper();
    //        if (((TextBox)gvMeasure.FooterRow.FindControl("txtFMinWeight")) == null)
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入最小重量！！');</script>");
    //            return;
    //        }
    //        else
    //            strFmin = ((TextBox)gvMeasure.FooterRow.FindControl("txtFMinWeight")).Text.Trim().ToUpper();
    //        if (((TextBox)gvMeasure.FooterRow.FindControl("txtFCartonQty")) == null)
    //            strFqty=" ";
    //        else
    //            strFqty = ((TextBox)gvMeasure.FooterRow.FindControl("txtFCartonQty")).Text.Trim().ToUpper();
    //        if (((TextBox)gvMeasure.FooterRow.FindControl("txtFArea")) == null)
    //            strFarea=" ";
    //        else
    //            strFarea = ((TextBox)gvMeasure.FooterRow.FindControl("txtFArea")).Text.Trim().ToUpper();
    //        if (strFcountry.Equals(""))
    //        {
    //            string strsql = "insert into SHP.MES_PACK_MEASURES_STANDARD(MODEL_ID,COUNTRY_NAME,MAX_WEIGHT,MIN_WEIGHT,CARTON_QTY,AREA) values('" +
    //                   strFModel + "',' ','" + strFmax + "','" + strFmin + "','" + strFqty + "','" + strFarea + "')";
    //            ClsGlobal.objDataConnect.DataExecute(strsql);
    //            gvMeasure.EditIndex = -1;
    //            binddata();
    //        }
    //        else
    //        {
    //            if (!strFModel.Equals(" ") && !strFmax.Equals(" ") && !strFmin.Equals(" "))
    //            {
    //                string strsql = "insert into SHP.MES_PACK_MEASURES_STANDARD(MODEL_ID,COUNTRY_NAME,MAX_WEIGHT,MIN_WEIGHT,CARTON_QTY,AREA) values('" +
    //                    strFModel + "','" + strFcountry + "','" + strFmax + "','" + strFmin + "','" + strFqty + "','" + strFarea + "')";
    //                ClsGlobal.objDataConnect.DataExecute(strsql);
    //                gvMeasure.EditIndex = -1;
    //                binddata();
    //            }
    //            else
    //            {
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種與重量範圍有誤！！');</script>");
    //                return;
    //            }
    //        }
    //    }
    //    if (e.CommandName == "ItemUpdate")
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument);


    //        //string id = this.gvMeasure.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();

    //        string strQModel = ((Label)this.gvMeasure.Rows[index].FindControl("lbModelID")).Text;
    //        string strQcountry = ((Label)gvMeasure.Rows[index].FindControl("lbCountry")).Text;
    //        string strQmax = ((Label)gvMeasure.Rows[index].FindControl("lbMaxWeight")).Text;
    //        string strQmin = ((Label)gvMeasure.Rows[index].FindControl("lbMinWeight")).Text;
    //        string strQqty = ((Label)gvMeasure.Rows[index].FindControl("lbCartonQty")).Text;
    //        string strQarea = ((Label)gvMeasure.Rows[index].FindControl("lbArea")).Text;

    //        string strHModel = ((TextBox)gvMeasure.Rows[index].FindControl("txtModelID")).Text.Trim().ToUpper();
    //        string strHcountry = ((TextBox)gvMeasure.Rows[index].FindControl("txtCountry")).Text.Trim();
    //        string strHmax = ((TextBox)gvMeasure.Rows[index].FindControl("txtaxWeight")).Text.Trim();
    //        string strHmin = ((TextBox)gvMeasure.Rows[index].FindControl("txtMinWeight")).Text.Trim();
    //        string strHqty = ((TextBox)gvMeasure.Rows[index].FindControl("txtCartonQty")).Text.Trim();
    //        string strHarea = ((TextBox)gvMeasure.Rows[index].FindControl("txtArea")).Text.Trim();
    //        if (!strHModel.Equals("") && !strHmax.Equals("") && !strHmin.Equals(""))
    //        {
    //            string strsql = "update SHP.MES_PACK_MEASURES_STANDARD set MODEL_ID='" + strHModel + "',COUNTRY_NAME='" + strHcountry
    //                + "',MAX_WEIGHT='" + strHmax + "',MIN_WEIGHT='" + strHmin + "',CARTON_QTY='" + strHqty + "',AREA='" + strHarea
    //                + "' where MODEL_ID='" + strQModel + "',COUNTRY_NAME='" + strQcountry + "',MAX_WEIGHT='" + strQmax + "',MIN_WEIGHT='" +
    //                strQmin + "',CARTON_QTY='" + strQqty + "',AREA='" + strQarea + "'";
    //            ClsGlobal.objDataConnect.DataExecute(strsql);
    //            gvMeasure.EditIndex = -1;
    //            binddata();
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種與重量範圍有誤！！');</script>");
    //            return;
    //        }
    //    }
       
       
    //}
    //protected void gvMeasure_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    ListItemType itemtype = (ListItemType)e.Row.RowType;
    //    gvMeasure.ShowFooter = false;
    //    if (itemtype == ListItemType.Item || itemtype == ListItemType.AlternatingItem)
    //    {
    //        if (gvMeasure.EditIndex < 0)//说明是正常显示状态，为“删除”按钮添加属性
    //            ((LinkButton)e.Row.Cells[7].FindControl("LinkButton2")).Attributes.Add("onclick", "return confirm(\"确定要删除此记录吗?\");");
    //        else//说明是编辑状态，为“取消”按钮添加属性
    //            ((LinkButton)e.Row.Cells[7].FindControl("LinkButton2")).Attributes.Add("onclick", "return confirm(\"确定要取消吗?\");");

    //    }
    //}
    //protected void gvMeasure_RowDeleted(object sender, GridViewDeletedEventArgs e)
    //{
    //    if (e.Exception == null)
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據刪除成功！！');</script>");
    //        return;
    //    }
            
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('刪除數據有誤！！');</script>");
    //        return;
    //    }
    //    binddata();
    //}
    //protected void gvMeasure_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string strQModel = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbModelID")).Text.Trim().ToUpper();
    //    string strQcountry = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbCountry")).Text.Trim();
    //    string strQmax = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbMaxWeight")).Text.Trim();
    //    string strQmin = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbMinWeight")).Text.Trim();
    //    string strQqty = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbCartonQty")).Text.Trim();
    //    string strQarea = ((Label)gvMeasure.Rows[e.RowIndex].FindControl("lbArea")).Text.Trim();

    //    string strHModel = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtModelID")).Text.Trim().ToUpper();
    //    string strHcountry = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtCountry")).Text.Trim();
    //    string strHmax = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtaxWeight")).Text.Trim();
    //    string strHmin = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtMinWeight")).Text.Trim();
    //    string strHqty = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtCartonQty")).Text.Trim();
    //    string strHarea = ((TextBox)gvMeasure.Rows[e.RowIndex].FindControl("txtFArea")).Text.Trim();
    //    if (!strHModel.Equals("") && !strHmax.Equals("") && !strHmin.Equals(""))
    //    {
    //        string strsql = "update SHP.MES_PACK_MEASURES_STANDARD set MODEL_ID='" + strHModel + "',COUNTRY_NAME='" + strHcountry
    //            + "',MAX_WEIGHT='" + strHmax + "',MIN_WEIGHT='" + strHmin + "',CARTON_QTY='" + strHqty + "',AREA='" + strHarea
    //            + "' where MODEL_ID='" + strQModel + "',COUNTRY_NAME='" + strQcountry + "',MAX_WEIGHT='" + strQmax + "',MIN_WEIGHT='" +
    //            strQmin + "',CARTON_QTY='" + strQqty + "',AREA='" + strQarea + "'";
    //        ClsGlobal.objDataConnect.DataExecute(strsql);
    //        gvMeasure.EditIndex = -1;
    //        binddata();
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種與重量範圍有誤！！');</script>");
    //        return;
    //    }
    //}
    protected void dgMeasure_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgMeasure.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strModel);
    }
    protected void dgMeasure_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgMeasure.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strModel); 
    }
    protected void dgMeasure_UpdateCommand(object source, DataGridCommandEventArgs e)
    { 
        string strHModel = ((Label)e.Item.Cells[1].Controls[1]).Text;
        string strHcountry = ((Label)e.Item.Cells[2].Controls[1]).Text;
        string strHmax = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
        string strHmin = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
        string strHqty = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
        string strHarea = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
        if (!strHModel.Equals("") && !strHmax.Equals("") && !strHmin.Equals(""))
        {
            //string strSql = "delete from SHP.MES_PACK_MEASURES_STANDARD where MODEL_ID='" + strHModel + "',COUNTRY_NAME='" + strHcountry + "'";
            //ClsGlobal.objDataConnect.DataExecute(strSql);

            string strsql = "update SHP.MES_PACK_MEASURES_STANDARD set  MAX_WEIGHT='" + strHmax + "',MIN_WEIGHT='" + strHmin + "',CARTON_QTY='" + strHqty + "',AREA='" + strHarea
                + "' where MODEL_ID='" + strHModel + "' AND COUNTRY_NAME='" + strHcountry + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgMeasure.EditItemIndex = -1;
            txtModel.Text = "";
            string strSql = "select * from SHP.MES_PACK_MEASURES_STANDARD";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            dgMeasure.DataSource = this.GetIDTable(dt).DefaultView;
            dgMeasure.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種與重量範圍有誤！！');</script>");
            return;
        } 
        
    }

    protected void dgMeasure_ItemDataBound(object sender, DataGridItemEventArgs e)
    { 
        if(e.Item.ItemIndex>=0) 
            e.Item.Cells[0].Text=((e.Item.ItemIndex+1)+(dgMeasure.PageSize)*(dgMeasure.CurrentPageIndex)).ToString(); 
        if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[8].Controls[1])).Attributes.Add("onclick","return confirm('你确认删除吗?');"); 

    }

    protected void dgMeasure_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgMeasure.CurrentPageIndex = e.NewPageIndex;
        string strsql = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        dgMeasure.DataSource = this.GetIDTable(dt).DefaultView;
        dgMeasure.DataBind();
        lbcount.Text = "Current Page:" + (dgMeasure.CurrentPageIndex + 1).ToString() + "/" + dgMeasure.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }

    protected void dgMeasure_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        { 
            dgMeasure.ShowFooter = true;
            string strsql = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
                dgMeasure.DataBind();
            }        
        }

         if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
         {
             dgMeasure.ShowFooter = false;
             string strsql = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
             DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
             if (dt1.Rows.Count > 0)
             {
                 lbcount.Text = "Total:" + dt1.Rows.Count;

                 dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
                 dgMeasure.DataBind();
             }

         }
         if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
         {
             dgMeasure.ShowFooter = false;
             string strFModel;
             string strFcountry;
             string strFmax;
             string strFmin;
             string strFqty;
             string strFarea;
             if (((TextBox)e.Item.FindControl("txtFModelID")).Text.Trim().ToUpper() == null)
             {
                 Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種不能為空！！');</script>");
                 return;
             }
             else
                 strFModel = ((TextBox)e.Item.FindControl("txtFModelID")).Text.Trim().ToUpper();
             if (((TextBox)e.Item.FindControl("txtFCountry")).Text.Trim().ToUpper() == null)
                 strFcountry = " ";
             else
                 strFcountry = ((TextBox)e.Item.FindControl("txtFCountry")).Text.Trim();
             if (((TextBox)e.Item.FindControl("txtFMaxWeight")) == null || (((TextBox)e.Item.FindControl("txtFMaxWeight")).Text.Equals("")))
             {
                 Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入最大重量！！');</script>");
                 return;
                 dgMeasure.ShowFooter = true;

             }
             else
                 strFmax = ((TextBox)e.Item.FindControl("txtFMaxWeight")).Text.Trim().ToUpper();
             if (((TextBox)e.Item.FindControl("txtFMinWeight")) == null || (((TextBox)e.Item.FindControl("txtFMinWeight")).Text.Equals("")))
             {
                 Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入最小重量！！');</script>");
                 return;
             }
             else
                 strFmin = ((TextBox)e.Item.FindControl("txtFMinWeight")).Text.Trim().ToUpper();
             if (((TextBox)e.Item.FindControl("txtFCartonQty")) == null)
                 strFqty = " ";
             else
                 strFqty = ((TextBox)e.Item.FindControl("txtFCartonQty")).Text.Trim().ToUpper();
             if (((TextBox)e.Item.FindControl("txtFArea")) == null)
                 strFarea = " ";
             else
                 strFarea = ((TextBox)e.Item.FindControl("txtFArea")).Text.Trim().ToUpper();
             if (strFcountry.Equals(""))
             {
                 string strsql = "insert into SHP.MES_PACK_MEASURES_STANDARD(MODEL_ID,COUNTRY_NAME,MAX_WEIGHT,MIN_WEIGHT,CARTON_QTY,AREA) values('" +
                        strFModel + "',' ','" + strFmax + "','" + strFmin + "','" + strFqty + "','" + strFarea + "')";
                 ClsGlobal.objDataConnect.DataExecute(strsql);
                 dgMeasure.EditItemIndex = -1;
                 string strsql1 = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
                 DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                 if (dt1.Rows.Count > 0)
                 {
                     lbcount.Text = "Total:" + dt1.Rows.Count;

                     dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
                     dgMeasure.DataBind();
                 }
             }
             else
             {
                 if (!strFModel.Equals(" ") && !strFmax.Equals(" ") && !strFmin.Equals(" "))
                 {
                     string strsql = "insert into SHP.MES_PACK_MEASURES_STANDARD(MODEL_ID,COUNTRY_NAME,MAX_WEIGHT,MIN_WEIGHT,CARTON_QTY,AREA) values('" +
                         strFModel + "','" + strFcountry + "','" + strFmax + "','" + strFmin + "','" + strFqty + "','" + strFarea + "')";
                     ClsGlobal.objDataConnect.DataExecute(strsql);
                     dgMeasure.EditItemIndex = -1;
                     string strsql1 = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
                     DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                     if (dt1.Rows.Count > 0)
                     {
                         lbcount.Text = "Total:" + dt1.Rows.Count;

                         dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
                         dgMeasure.DataBind();
                     }
                 }
                 else
                 {
                     Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種與重量範圍有誤！！');</script>");
                     return;
                 }
             }
         }
         if (e.CommandName == "ItemDelete")
         {
             string strModel = ((Label)e.Item.Cells[1].Controls[1]).Text;
             string strcountry = ((Label)e.Item.Cells[2].Controls[1]).Text;
             int  max = Convert.ToInt16(((Label)e.Item.Cells[3].Controls[1]).Text);
             int min = Convert.ToInt16(((Label)e.Item.Cells[4].Controls[1]).Text);
             string strqty = ((Label)e.Item.Cells[5].Controls[1]).Text;
             string strarea = ((Label)e.Item.Cells[6].Controls[1]).Text;
             string strsql = "delete from  SHP.MES_PACK_MEASURES_STANDARD where MODEL_ID='" + strModel + "' and COUNTRY_NAME='" + strcountry+"'";
             ClsGlobal.objDataConnect.DataExecute(strsql);
             dgMeasure.EditItemIndex = -1;
             string strsql1 = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD where Model_ID like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
             DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
             if (dt1.Rows.Count > 0)
             {
                 lbcount.Text = "Total:" + dt1.Rows.Count;

                 dgMeasure.DataSource = this.GetIDTable(dt1).DefaultView;
                 dgMeasure.DataBind();
             }
             else
             {
                 txtModel.Text = "";
                 string strsql2 = "SELECT * FROM SHP.MES_PACK_MEASURES_STANDARD ";
                 DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                 lbcount.Text = "Total:" + dt1.Rows.Count;

                 dgMeasure.DataSource = this.GetIDTable(dt2).DefaultView;
                 dgMeasure.DataBind();
            
             }
         }
    }
}
