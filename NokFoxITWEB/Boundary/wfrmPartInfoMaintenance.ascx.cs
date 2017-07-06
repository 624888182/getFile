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

public partial class Boundary_wfrmPartInfoMaintenance : System.Web.UI.UserControl
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

    private void BindData(string spart)
    {
        dgSpart.Visible = true;
        
        string strsql = "select * from SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart like'" + spart.ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgSpart.DataSource = this.GetIDTable(dt1).DefaultView;
            dgSpart.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該料號的維護信息...');</script>");
            return;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "select * from SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart like '" + this.txtSpart.Text.Trim().ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Visible = true;
            dgSpart.Visible = true;
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgSpart.DataSource = this.GetIDTable(dt1).DefaultView;
            dgSpart.DataBind();
        }
        else
        {
            lbcount.Visible = false;
            dgSpart.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該料號的維護信息...');</script>");
            return;
        }
    }

    protected void dgSpart_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgSpart.PageSize) * (dgSpart.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[12].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?');");
        if (e.Item.ItemType == ListItemType.EditItem)
        {
            string strorderprefix = ((DataRowView)e.Item.DataItem).Row["ORDER_PREFIX"].ToString();
            DropDownList ddlorderprefix = (DropDownList)e.Item.FindControl("ddlEorderprefix");
            ListItem itemorderprefix = ddlorderprefix.Items.FindByText(strorderprefix);
            if (itemorderprefix != null)
                itemorderprefix.Selected = true;
        }
    }

    protected void dgSpart_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strSpart = txtSpart.Text.Trim().ToUpper();
        dgSpart.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strSpart);
    }
    protected void dgSpart_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strSpart = txtSpart.Text.Trim().ToUpper();
        dgSpart.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strSpart);
    }
    protected void dgSpart_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgSpart.ShowFooter = true;
            string strsql = "select * from SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart like '" + this.txtSpart.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgSpart.DataSource = this.GetIDTable(dt1).DefaultView;
                dgSpart.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgSpart.ShowFooter = false;
            string strsql = "select * from SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart like '" + this.txtSpart.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgSpart.DataSource = this.GetIDTable(dt1).DefaultView;
                dgSpart.DataBind();
            }
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgSpart.ShowFooter = false;

            OracleConnection orcn = null;
            OracleDataAdapter da;
            DataSet myds;
            myds = new DataSet();

            try
            {
                orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");

                orcn.Open();

                da = new OracleDataAdapter("select * from SFC.CMCS_SFC_PCBA_BARCODE_CTL where 0=1 ", orcn);
                da.Fill(myds, "CMCS_SFC_PCBA_BARCODE_CTL");

                //----------- Insert new row----------

                DataRow mydr = myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].NewRow();

                if (((TextBox)e.Item.Cells[1].Controls[1]).Text.Trim().Length > 0)
                    mydr["SPART"] = ((TextBox)e.Item.Cells[1].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SPART欄位不能為空...');</script>");
                    return;
                }
                if (((TextBox)e.Item.Cells[2].Controls[1]).Text.Trim().Length > 0)
                    mydr["SEQUENCE_ID"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text.Trim().ToUpper();
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SEQUENCE_ID欄位不能為空...');</script>");
                    return;
                }
                if (((TextBox)e.Item.Cells[4].Controls[1]).Text.Trim().Length > 0)
                    mydr["DESCRIPTION"] = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('DESCRIPTION欄位不能為空...');</script>");
                    return;
                }
                if (((TextBox)e.Item.Cells[5].Controls[1]).Text.Trim().Length > 0)
                    mydr["MODEL"] = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('MODEL欄位不能為空...');</script>");
                    return;
                }
                mydr["BARCODE_PREFIX"] = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
                mydr["ORDER_PREFIX"] = ((DropDownList)e.Item.Cells[7].Controls[1]).SelectedValue;
                mydr["BOARD_QTY"] = ((TextBox)e.Item.Cells[8].Controls[1]).Text;
                if (((TextBox)e.Item.Cells[9].Controls[1]).Text.Trim().Length > 0)
                    mydr["ROUTE_CODE"] = ((TextBox)e.Item.Cells[9].Controls[1]).Text;
                else
                    mydr["ROUTE_CODE"] = "10";                
                mydr["NAME"] = ((TextBox)e.Item.Cells[10].Controls[1]).Text;
                myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows.Add(mydr);

                // ---------------end -------------------

                OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
                da.Update(myds, "CMCS_SFC_PCBA_BARCODE_CTL");
                orcn.Close();

                dgSpart.EditItemIndex = -1;
                string strSpart = txtSpart.Text.Trim().ToUpper();
                BindData(strSpart);
            }
            catch (Exception ex)
            {
                orcn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該料號息已存在,請檢查...');</script>");
                return;
            }
        }

        if (e.CommandName == "ItemDelete")
        {
            string strSpart = ((Label)e.Item.Cells[1].Controls[1]).Text;
            string strSequenceid = ((Label)e.Item.Cells[2].Controls[1]).Text;
            string strsql = "delete from SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart='" + strSpart + "' and sequence_id='" + strSequenceid + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgSpart.EditItemIndex = -1;
            string strsql1 = "SELECT * FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart like '" + this.txtSpart.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgSpart.DataSource = this.GetIDTable(dt1).DefaultView;
                dgSpart.DataBind();
            }
            else
            {
                lbcount.Visible = false;
                dgSpart.Visible = false;
            }
        }
    }

    protected void dgSpart_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string strSpart = txtSpart.Text.Trim().ToUpper();
        dgSpart.CurrentPageIndex = e.NewPageIndex;
        string strsql = "select * from SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart like '" + strSpart + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgSpart.DataSource = this.GetIDTable(dt).DefaultView;
            dgSpart.DataBind();
            lbcount.Text = "Current Page:" + (dgSpart.CurrentPageIndex + 1).ToString() + "/" + dgSpart.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

            dt.Dispose();
        }
        else
        {
            string strsql1 = "SELECT * FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            dgSpart.DataSource = this.GetIDTable(dt1).DefaultView;
            dgSpart.DataBind();
            lbcount.Text = "Current Page:" + (dgSpart.CurrentPageIndex + 1).ToString() + "/" + dgSpart.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();

            dt1.Dispose();
        }
    }
    protected void dgSpart_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strSpart = ((Label)e.Item.Cells[1].Controls[1]).Text.Trim(); 

        OracleConnection orcn = null;
        OracleDataAdapter da;
        DataSet myds;
        myds = new DataSet();

        try
        {
            orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");

            orcn.Open();

            da = new OracleDataAdapter("select * from SFC.CMCS_SFC_PCBA_BARCODE_CTL where spart='" + strSpart + "'", orcn);
            da.Fill(myds, "CMCS_SFC_PCBA_BARCODE_CTL");

            //----------- update part-----------
            //DataTable myDt = myds.Tables["CDMA_MOTO_ORDERNO"];
            //myDt.PrimaryKey = new DataColumn[] { myDt.Columns["ORDER_NUMBER"] };
            //出错就是因为少了上面这一句。这条语句指定了DataTable的主键。或者用下一条语句也可以，下一条语句是让适配器自动加上表的架构（Key约束）
            //da.MissingSchemaAction = MissingSchemaAction.AddWithKey; 

            myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["SEQUENCE_ID"] = ((TextBox)e.Item.Cells[2].Controls[3]).Text;
            myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["DESCRIPTION"] = ((TextBox)e.Item.Cells[4].Controls[3]).Text;
            myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["MODEL"] = ((TextBox)e.Item.Cells[5].Controls[3]).Text;
            myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["BARCODE_PREFIX"] = ((TextBox)e.Item.Cells[6].Controls[3]).Text;
            myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["ORDER_PREFIX"] = ((DropDownList)e.Item.Cells[7].Controls[3]).SelectedValue;
            if (((TextBox)e.Item.Cells[8].Controls[3]).Text.Trim().Length > 0)
                myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["BOARD_QTY"] = ((TextBox)e.Item.Cells[8].Controls[3]).Text;
            myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["ROUTE_CODE"] = ((TextBox)e.Item.Cells[9].Controls[3]).Text;
            myds.Tables["CMCS_SFC_PCBA_BARCODE_CTL"].Rows[0]["NAME"] = ((TextBox)e.Item.Cells[10].Controls[3]).Text;
            
            OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
            da.Update(myds, "CMCS_SFC_PCBA_BARCODE_CTL"); 
            orcn.Close();
        }
        catch 
        {
            orcn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該料號信息已存在,請檢查...');</script>");
            return;
        }
        dgSpart.EditItemIndex = -1;
        BindData(strSpart);
    }
}
