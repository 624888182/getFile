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

public partial class Boundary_wfrmErrorCodeMaintenance : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            string strsql = "select * from SFC.MES_REPAIR_DEFECTCODE ";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgError.DataSource = this.GetIDTable(dt1).DefaultView;
                dgError.DataBind();
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

    private void BindData(string errorcode)
    {
        dgError.Visible = true;
        string strsql = "select * from SFC.MES_REPAIR_DEFECTCODE where defect_code LIKE '%" + txterrorcode.Text.Trim() + "'%";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgError.DataSource = this.GetIDTable(dt1).DefaultView;
            dgError.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在該ErrorCode信息...');</script>");
            return;
        }
    }

    protected void dgError_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1)).ToString();

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[5].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?');"); 
    }

    protected void dgError_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgError.ShowFooter = true;
            string strsql = "select * from SFC.MES_REPAIR_DEFECTCODE where defect_code= '" + txterrorcode.Text.Trim() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgError.DataSource = this.GetIDTable(dt1).DefaultView;
                dgError.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgError.ShowFooter = false;
            string strsql = "select * from SFC.MES_REPAIR_DEFECTCODE where defect_code= '" + txterrorcode.Text.Trim() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgError.DataSource = this.GetIDTable(dt1).DefaultView;
                dgError.DataBind();
            }
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgError.ShowFooter = false;

            try
            {
                OracleConnection orcn = null;
                OracleDataAdapter da;
                DataSet myds;
                myds = new DataSet();
                orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");

                orcn.Open();

                da = new OracleDataAdapter("select * from SFC.MES_REPAIR_DEFECTCODE where 0=1", orcn);
                da.Fill(myds, "MES_REPAIR_DEFECTCODE");

                DataRow mydr = myds.Tables["MES_REPAIR_DEFECTCODE"].NewRow();

                mydr["DEFECT_CODE"] = ((TextBox)e.Item.FindControl("txtFdefectcode")).Text.Trim();
                mydr["DESCRIPTION"] = ((TextBox)e.Item.FindControl("txtFdescription")).Text.Trim();
                mydr["USED"] = ((DropDownList)e.Item.FindControl("ddlFused")).SelectedValue.ToUpper();
                mydr["FACTORY_AREA"] = ((TextBox)e.Item.FindControl("txtFfactoryarea")).Text.Trim().ToUpper();

                myds.Tables["MES_REPAIR_DEFECTCODE"].Rows.Add(mydr);

                OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
                da.Update(myds, "MES_REPAIR_DEFECTCODE");
                orcn.Close();

                string errorcode = txterrorcode.Text.Trim();
                BindData(errorcode);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('維護資料錯誤,請檢查你的輸入...');</script>");
                return;
            }
        }

        if (e.CommandName == "ItemDelete")
        {
            string strdefectcode = ((Label)e.Item.Cells[1].Controls[1]).Text;
            string strdescription = ((Label)e.Item.Cells[2].Controls[1]).Text;
            string strused = ((Label)e.Item.Cells[3].Controls[1]).Text;
            string strfactoryarea = ((Label)e.Item.Cells[4].Controls[1]).Text;
            string strsql = "delete from  SFC.MES_REPAIR_DEFECTCODE where defect_code='" + strdefectcode + "' and description='" + strdescription + "' and used='" + strused + "' and factory_area='" + strfactoryarea + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgError.EditItemIndex = -1;
            string strsql1 = "SELECT * FROM SFC.MES_REPAIR_DEFECTCODE where defect_code= '" + strdefectcode + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Visible = true;
                dgError.Visible = true;
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgError.DataSource = this.GetIDTable(dt1).DefaultView;
                dgError.DataBind();
            }
            else
            {
                lbcount.Visible = false;
                dgError.Visible = false;
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "select * from SFC.MES_REPAIR_DEFECTCODE where defect_code = '" + txterrorcode.Text.Trim() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgError.Visible = true;
            lbcount.Visible = true;
            lbcount.Text = "Total:" + dt1.Rows.Count;

            dgError.DataSource = this.GetIDTable(dt1).DefaultView;
            dgError.DataBind();
        }
        else
        {
            dgError.Visible = false;
            lbcount.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在該ErrorCode信息...');</script>");
            return;
        }
    }
}
