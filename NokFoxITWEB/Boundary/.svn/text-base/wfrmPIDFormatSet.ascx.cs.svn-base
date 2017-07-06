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

public partial class Boundary_wfrmPIDFormatSet : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            string model = txtModel.Text.Trim();
            BindData(model);
        }
    }

    private void BindData(string model)
    {
        dgPidFormat.Visible = true;
        string strsql = "SELECT * FROM SFC.MES_COMM_MODEL where model_name like '" + model.ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgPidFormat.DataSource = this.GetIDTable(dt1).DefaultView;
            dgPidFormat.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種的PID格式尚未維護！！');</script>");
            return;
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

    protected void dgPidFormat_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgPidFormat.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strModel); 
    }
    protected void dgPidFormat_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgPidFormat.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strModel);
    }
    protected void dgPidFormat_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgPidFormat.PageSize) * (dgPidFormat.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[6].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?');"); 
    }
    protected void dgPidFormat_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgPidFormat.ShowFooter = true;
            string strsql = "SELECT * FROM SFC.MES_COMM_MODEL where model_name like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgPidFormat.DataSource = this.GetIDTable(dt1).DefaultView;
                dgPidFormat.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgPidFormat.ShowFooter = false;
            string strsql = "SELECT * FROM SFC.MES_COMM_MODEL where model_name like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgPidFormat.DataSource = this.GetIDTable(dt1).DefaultView;
                dgPidFormat.DataBind();
            }

        }
        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgPidFormat.ShowFooter = false;
            string strFModel;
            string strFHead;
            string strFSerialNo;
            string strFDesc;
            if (((TextBox)e.Item.FindControl("txtFModelID")).Text.Trim().ToUpper() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('ModelName不能為空！！');</script>");
                return;
            }
            else
                strFModel = ((TextBox)e.Item.FindControl("txtFModelID")).Text.Trim().ToUpper();
            if (((TextBox)e.Item.FindControl("txtFModelHead")).Text.Trim().ToUpper() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('ModelHead不能為空！！');</script>");
                return;
            }
            else
                strFHead = ((TextBox)e.Item.FindControl("txtFModelHead")).Text.Trim().ToUpper();
            if (((TextBox)e.Item.FindControl("txtFSerialNo")).Text.Trim().ToUpper() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SerialNo不能為空！！');</script>");
                return;
            }
            else
                strFSerialNo = ((TextBox)e.Item.FindControl("txtFSerialNo")).Text.Trim().ToUpper();
            if (((TextBox)e.Item.FindControl("txtFDescription")).Text.Trim().ToUpper() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Description不能為空！！');</script>");
                return;
            }
            else
                strFDesc = ((TextBox)e.Item.FindControl("txtFDescription")).Text.Trim().ToUpper();

            if (!strFModel.Equals(" ") && !strFHead.Equals(" ") && !strFSerialNo.Equals(" ") && !strFDesc.Equals(" "))
            {
                string strSql = "select * from sfc.mes_comm_model where  model_head='" + strFHead + "'";
                DataTable dt= ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbcount.Text = "Total:" + dt.Rows.Count;
                    dgPidFormat.DataSource = this.GetIDTable(dt).DefaultView;
                    dgPidFormat.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該格式已維護！！');</script>");
                    return;
                }
                else
                {
                    string strsql = "insert into SFC.MES_COMM_MODEL(MODEL_NAME,MODEL_HEAD,SERIAL_NO,DESCRIPTION) values('" +
                        strFModel + "','" + strFHead + "','" + strFSerialNo + "','" + strFDesc + "')";
                    ClsGlobal.objDataConnect.DataExecute(strsql);
                    dgPidFormat.EditItemIndex = -1;
                    string strsql1 = "SELECT * FROM sfc.MES_COMM_MODEL where Model_NAME like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
                    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        lbcount.Text = "Total:" + dt1.Rows.Count;

                        dgPidFormat.DataSource = this.GetIDTable(dt1).DefaultView;
                        dgPidFormat.DataBind();
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('信息填寫有誤！！');</script>");
                return;
            }
        }
        if (e.CommandName == "ItemDelete")
        {
            string strModel = ((Label)e.Item.Cells[1].Controls[1]).Text;
            string strHead = ((Label)e.Item.Cells[2].Controls[1]).Text;            
            string strSerialNo = ((Label)e.Item.Cells[3].Controls[1]).Text;
            string strDesc = ((Label)e.Item.Cells[4].Controls[1]).Text;
            string strsql = "delete from  sfc.MES_COMM_MODEL where  model_name='" + strModel + "' and model_head='" + strHead + "' and serial_no='" + strSerialNo + "' and description='" + strDesc + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgPidFormat.EditItemIndex = -1;
            string strsql1 = "SELECT * FROM sfc.MES_COMM_MODEL where Model_NAME like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgPidFormat.DataSource = this.GetIDTable(dt1).DefaultView;
                dgPidFormat.DataBind();
            }
            else
            {
                txtModel.Text = "";
                string strsql2 = "SELECT * FROM sfc.MES_COMM_MODEL ";
                DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgPidFormat.DataSource = this.GetIDTable(dt2).DefaultView;
                dgPidFormat.DataBind();

            }
        }
    }
    protected void dgPidFormat_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgPidFormat.CurrentPageIndex = e.NewPageIndex;
        string strsql = "SELECT * FROM SFC.MES_COMM_MODEL where model_name like  '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        dgPidFormat.DataSource = this.GetIDTable(dt).DefaultView;
        dgPidFormat.DataBind();
        lbcount.Text = "Current Page:" + (dgPidFormat.CurrentPageIndex + 1).ToString() + "/" + dgPidFormat.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "SELECT * FROM SFC.MES_COMM_MODEL where model_name like  '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgPidFormat.Visible = true;
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgPidFormat.DataSource = this.GetIDTable(dt1).DefaultView;
            dgPidFormat.DataBind();
        }
        else
        {
            dgPidFormat.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種的PID格式尚未維護！！');</script>");
            return;
        }
    }
    protected void dgPidFormat_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strQHead = ((Label)e.Item.Cells[2].Controls[1]).Text.ToUpper();
        string strQModel = ((TextBox)e.Item.Cells[1].Controls[1]).Text.ToUpper();
        string strQSerialNo = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
        if (!strQModel.Equals(" ") && !strQSerialNo.Equals(" "))
        {
            //string strSql = "delete from SHP.MES_PACK_MEASURES_STANDARD where MODEL_ID='" + strHModel + "',COUNTRY_NAME='" + strHcountry + "'";
            //ClsGlobal.objDataConnect.DataExecute(strSql);

            string strsql = "update SFC.MES_COMM_MODEL set  model_name='" + strQModel + "', serial_no='" + strQSerialNo
                + "' WHERE  model_head='" + strQHead + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgPidFormat.EditItemIndex = -1;
            txtModel.Text = "";
            string strSql = "select * from SFC.MES_COMM_MODEL ";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            dgPidFormat.DataSource = this.GetIDTable(dt).DefaultView;
            dgPidFormat.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('信息維護有誤！！');</script>");
            return;
        } 
    }
}
