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

public partial class Boundary_wfrmHWVersionMantenance : System.Web.UI.UserControl
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

    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    private void BindData(string model)
    {
        dgHWver.Visible = true;
        string strsql = "SELECT * FROM sfc.mes_assy_hw_control where Model like '" + model.ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgHWver.DataSource = this.GetIDTable(dt1).DefaultView;
            dgHWver.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種的HW信息尚未維護！！');</script>");
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "SELECT * FROM sfc.mes_assy_hw_control where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;

            dgHWver.DataSource = this.GetIDTable(dt1).DefaultView;
            dgHWver.DataBind();
        }
        else
        {
            dgHWver.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種的HW信息尚未維護！！');</script>");
            return;
        }
    }

    protected void dgHWver_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgHWver.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strModel);
    }
    protected void dgHWver_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgHWver.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strModel);
    }
    protected void dgHWver_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgHWver.ShowFooter = true;
            string strsql = "SELECT * FROM sfc.mes_assy_hw_control where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgHWver.DataSource = this.GetIDTable(dt1).DefaultView;
                dgHWver.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgHWver.ShowFooter = false;
            string strsql = "SELECT * FROM sfc.mes_assy_hw_control where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgHWver.DataSource = this.GetIDTable(dt1).DefaultView;
                dgHWver.DataBind();
            }

        }
        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            string strsqlu = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
            DataTable dtu = ClsGlobal.objDataConnect.DataQuery(strsqlu).Tables[0];
            ViewState["UserName"] = dtu.Rows[0]["USERNAME"].ToString();

            dgHWver.ShowFooter = false;
            string strFModel;
            string strFHWver;
            string strFUp;
            string strFLower;
            string strFDaughter;
            string strFUpdateby;

            if (((TextBox)e.Item.FindControl("txtFModel")) == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種不能為空！！');</script>");
                return;
            }
            else
            { 
                if (((TextBox)e.Item.FindControl("txtFModel")).Text.Trim().Length > 3)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種名長度超出範圍！！');</script>");
                    return;
                }
                else
                    strFModel = ((TextBox)e.Item.FindControl("txtFModel")).Text.Trim().ToUpper();
            }
            if (((TextBox)e.Item.FindControl("txtFHW")) == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('HW Ver不能為空！！');</script>");
                return;
            }
            else
                strFHWver = ((TextBox)e.Item.FindControl("txtFHW")).Text.Trim().ToUpper();
            if (((TextBox)e.Item.FindControl("txtFLow")) == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Lower PN不能為空！！');</script>");
                return;
            }
            else
                strFLower = ((TextBox)e.Item.FindControl("txtFLow")).Text.Trim().ToUpper();
            if (((TextBox)e.Item.FindControl("txtFUp")) == null)
                strFUp = "";
            else
                strFUp = ((TextBox)e.Item.FindControl("txtFUp")).Text.Trim().ToUpper();
            if (((TextBox)e.Item.FindControl("txtFDaughter")) == null)
                strFDaughter = "";
            else
                strFDaughter = ((TextBox)e.Item.FindControl("txtFDaughter")).Text.Trim().ToUpper();

            if (((TextBox)e.Item.FindControl("txtFLastBY")) == null)
                strFUpdateby = " ";
            else
                strFUpdateby = ((TextBox)e.Item.FindControl("txtFLastBY")).Text.Trim().ToUpper();

            if (!strFModel.Equals(" ") && !strFHWver.Equals(" ") && !strFLower.Equals(" "))
            {
                string strsql0 = "select * from sfc.mes_assy_hw_control where model='" + strFModel + "' and Lower_pn='" + strFLower + "' and Up_pn='" + strFUp + "' and Daughter_pn='" + strFDaughter + "'";
                DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
                if (dt0.Rows.Count > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該信息已經存在,不能新增...');</script>");
                    return;
                }
                else
                {
                    string strsql = "insert into sfc.mes_assy_hw_control(MODEL,HW_REV,UP_PN,LOWER_PN,DAUGHTER_PN,LAST_UPDATE_DATE,LAST_UPDATED_BY) values('" +
                        strFModel + "','" + strFHWver + "','" + strFUp + "','" + strFLower + "','" + strFDaughter + "',sysdate,'" + ViewState["UserName"] + "')";
                    ClsGlobal.objDataConnect.DataExecute(strsql);
                    dgHWver.EditItemIndex = -1;
                    string strsql1 = "SELECT * FROM sfc.mes_assy_hw_control where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
                    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        lbcount.Text = "Total:" + dt1.Rows.Count;

                        dgHWver.DataSource = this.GetIDTable(dt1).DefaultView;
                        dgHWver.DataBind();
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請檢查你的輸入,Model,HW_Ver,Lower_Pn都不能為空...');</script>");
                return;
            } 
        }
        if (e.CommandName == "ItemDelete")
        {
            string strModel = ((Label)e.Item.Cells[1].Controls[1]).Text;
            string strUp = ((Label)e.Item.Cells[3].Controls[1]).Text; 
            string strLower = ((Label)e.Item.Cells[4].Controls[1]).Text;
            string strDaughter = ((Label)e.Item.Cells[5].Controls[1]).Text;
            string strsql = "delete from  sfc.mes_assy_hw_control where Model='" + strModel + "' and UP_PN='" + strUp + "' and LOWER_PN='" + strLower + "' and DAUGHTER_PN='" + strDaughter + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgHWver.EditItemIndex = -1;
            string strsql1 = "SELECT * FROM sfc.mes_assy_hw_control where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgHWver.DataSource = this.GetIDTable(dt1).DefaultView;
                dgHWver.DataBind();
            }
            else
            {
                txtModel.Text = "";
                string strsql2 = "SELECT * FROM sfc.mes_assy_hw_control ";
                DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                lbcount.Text = "Total:" + dt1.Rows.Count;

                dgHWver.DataSource = this.GetIDTable(dt2).DefaultView;
                dgHWver.DataBind();
            }
        }
    }
    protected void dgHWver_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgHWver.CurrentPageIndex = e.NewPageIndex;
        string strsql = "SELECT * FROM sfc.mes_assy_hw_control where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        dgHWver.DataSource = this.GetIDTable(dt).DefaultView;
        dgHWver.DataBind();
        lbcount.Text = "Current Page:" + (dgHWver.CurrentPageIndex + 1).ToString() + "/" + dgHWver.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
    protected void dgHWver_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgHWver.PageSize) * (dgHWver.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[9].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?');"); 

    }
    protected void dgHWver_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strQModel = ((Label)e.Item.Cells[1].Controls[1]).Text.Trim();
        string strQUp = ((Label)e.Item.Cells[3].Controls[1]).Text.Trim();
        string strQLower = ((Label)e.Item.Cells[4].Controls[1]).Text.Trim();
        string strQDaughter = ((Label)e.Item.Cells[5].Controls[1]).Text.Trim();

        string strHModel = ((TextBox)e.Item.Cells[1].Controls[3]).Text;
        string strHHW = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
        string strHUp = ((TextBox)e.Item.Cells[3].Controls[3]).Text;
        string strHLow = ((TextBox)e.Item.Cells[4].Controls[3]).Text;
        string strHDaughter = ((TextBox)e.Item.Cells[5].Controls[3]).Text;
        string strHLastby = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
        if (!strHModel.Equals(" ") && !strHHW.Equals(" ") && !strHLow.Equals(" "))
        {
            string strsql0 = "select * from sfc.mes_assy_hw_control where model='" + strHModel + "' and Lower_pn='" + strHLow + "' and Up_pn='" + strHUp + "' and Daughter_pn='" + strHDaughter + "'";
            DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
            if (dt0.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該信息已經存在,不能更新...');</script>");
                return;
            }
            else
            {
                string strsql;
                if (strQUp.Equals(""))
                    strsql = "update sfc.mes_assy_hw_control set  MODEL='" + strHModel + "',HW_REV='" + strHHW + "',UP_PN='" + strHUp + "',LOWER_PN='" + strHLow
                        + "',DAUGHTER_PN='" + strHDaughter + "',LAST_UPDATE_DATE=SYSDATE,LAST_UPDATED_BY='" + strHLastby + "' where MODEL='" + strQModel
                        + "' AND LOWER_PN='" + strQLower + "'";
                else
                    if (strQDaughter.Equals(""))
                        strsql = "update sfc.mes_assy_hw_control set  MODEL='" + strHModel + "',HW_REV='" + strHHW + "',UP_PN='" + strHUp + "',LOWER_PN='" + strHLow
                            + "',DAUGHTER_PN='" + strHDaughter + "',LAST_UPDATE_DATE=SYSDATE,LAST_UPDATED_BY='" + strHLastby + "' where MODEL='" + strQModel
                            + "' AND UP_PN='" + strQUp + "' AND LOWER_PN='" + strQLower + "'";
                    else
                        strsql = "update sfc.mes_assy_hw_control set  MODEL='" + strHModel + "',HW_REV='" + strHHW + "',UP_PN='" + strHUp + "',LOWER_PN='" + strHLow
                            + "',DAUGHTER_PN='" + strHDaughter + "',LAST_UPDATE_DATE=SYSDATE,LAST_UPDATED_BY='" + strHLastby + "' where MODEL='" + strQModel 
                            + "' AND UP_PN='" + strQUp + "' AND LOWER_PN='" + strQLower + "' AND DAUGHTER_PN='" + strQDaughter + "'";
                ClsGlobal.objDataConnect.DataExecute(strsql);
                dgHWver.EditItemIndex = -1;
                txtModel.Text = "";
                string strSql = "select * from sfc.mes_assy_hw_control ";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                dgHWver.DataSource = this.GetIDTable(dt).DefaultView;
                dgHWver.DataBind();
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('更新數據有誤,Model,HW_Rev,Lower_Pn均不能為空...');</script>");
            return;
        } 
    }

}
