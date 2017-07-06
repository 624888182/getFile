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

public partial class Boundary_wfrmUsersPrivilegesAdd : System.Web.UI.UserControl
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strUser = txtUser.Text.Trim();
        string strsql = "SELECT * FROM SHP.CMCS_SFC_USERS where username like '" + strUser + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgUser.Visible = true;
            dgUser.DataSource = this.GetIDTable(dt1).DefaultView;
            dgUser.DataBind();
        }
        else
        {
            dgUser.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('尚無該User信息,請先維護..');</script>");
            return;
        }
    }
    protected void dgUser_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strUser = txtUser.Text.Trim(); 
        dgUser.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strUser); 
    }

    private void BindData(string user)
    {
        dgUser.Visible = true;
        string strsql = "SELECT * FROM SHP.CMCS_SFC_USERS where username like '" + user + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgUser.DataSource = this.GetIDTable(dt1).DefaultView;
            dgUser.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('尚無該User信息,請先維護..');</script>");
            return;
        }
    }

    protected void dgUser_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strUser = txtUser.Text.Trim();
        dgUser.EditItemIndex = e.Item.ItemIndex;
        BindData(strUser); 
    }

    protected void dgUser_ItemCommand(object source, DataGridCommandEventArgs e)
    {
         if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgUser.ShowFooter = true;

            string strUser = txtUser.Text.Trim();
            string strsql = "SELECT * FROM SHP.CMCS_SFC_USERS where username like  '" + strUser + "%'";         
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            { 
                dgUser.DataSource = this.GetIDTable(dt1).DefaultView;
                dgUser.DataBind();
                dgUser.Columns[2].FooterText = DateTime.Now.ToString("yyyy/MM/dd");
            }            
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgUser.ShowFooter = false;
            string strUser = txtUser.Text.Trim();
            BindData(strUser);
        }
        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgUser.ShowFooter = false;
            string strFUsername;
            string strFPwd;
            string strFDdate = ((Label)e.Item.FindControl("lbFDdate")).Text.Trim();
            //DateTime dtDdate = Convert.ToDateTime(strFDdate);
            string strFRemark;
            string strFDept;
            string strFGroupid;
            string strFModule;
            string strFName;
            int iFU_del;
            int iFU_update;
            int iFU_all_p;
            string strFDepartment;
            string strFMenu_part;
            string strFCnname;
            if (((TextBox)e.Item.FindControl("txtFUserName")).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入要添加的用戶名...');</script>");
                return;
            }
            else
            {
                strFUsername = ((TextBox)e.Item.FindControl("txtFUserName")).Text.Trim();
            }

            if (((TextBox)e.Item.FindControl("txtFPWD")).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入要新添加用戶的密碼...');</script>");
                return;
            }
            else
            {
                strFPwd = ((TextBox)e.Item.FindControl("txtFPWD")).Text.Trim();
            }
            if (((TextBox)e.Item.FindControl("txtFRemark")).Text.Trim() == "") 
                strFRemark = ""; 
            else 
                strFRemark = ((TextBox)e.Item.FindControl("txtFRemark")).Text.Trim();
            if (((TextBox)e.Item.FindControl("txtFDept")).Text.Trim() == "")
                strFDept ="";
            else
                strFDept = ((TextBox)e.Item.FindControl("txtFDept")).Text.Trim();
            if (((TextBox)e.Item.FindControl("txtFGroupid")).Text.Trim() == "")
                strFGroupid = "";
            else
                strFGroupid = ((TextBox)e.Item.FindControl("txtFGroupid")).Text.Trim();
            if (((TextBox)e.Item.FindControl("txtFModule")).Text.Trim() == "")
                strFModule = "";
            else
                strFModule = ((TextBox)e.Item.FindControl("txtFModule")).Text.Trim();

            if (((TextBox)e.Item.FindControl("txtFName")).Text.Trim() == "")
                strFName = "";
            else
                strFName = ((TextBox)e.Item.FindControl("txtFName")).Text.Trim();

            if (((TextBox)e.Item.FindControl("txtFUdel")).Text.Trim() == "")
                iFU_del = 0;
            else
            {
                if (((TextBox)e.Item.FindControl("txtFUdel")).Text.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                    return;
                }
                else
                {  
                    char ichar = Convert.ToChar(((TextBox)e.Item.FindControl("txtFUdel")).Text);
                    if (!(ichar >= '0' && ichar <= '9'))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                        return;
                    }
                    else
                        iFU_del = Convert.ToInt32(((TextBox)e.Item.FindControl("txtFUdel")).Text.Trim());
                }
            }

            if (((TextBox)e.Item.FindControl("txtFUupdate")).Text.Trim() == "")
                iFU_update = 0;
            else
            {
                if (((TextBox)e.Item.FindControl("txtFUupdate")).Text.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                    return;
                }
                else
                {
                    char ichar = Convert.ToChar(((TextBox)e.Item.FindControl("txtFUupdate")).Text);
                    if (!(ichar >= '0' && ichar <= '9'))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                        return;
                    }
                    else
                        iFU_update = Convert.ToInt32(((TextBox)e.Item.FindControl("txtFUupdate")).Text.Trim());
                }
            }

            if (((TextBox)e.Item.FindControl("txtFUallp")).Text.Trim() == "")
                iFU_all_p = 0;
            else
            {
                if (((TextBox)e.Item.FindControl("txtFUallp")).Text.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                    return;
                }
                else
                {
                    char ichar = Convert.ToChar(((TextBox)e.Item.FindControl("txtFUallp")).Text);
                    if (!(ichar >= '0' && ichar <= '9'))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                        return;
                    }
                    else
                        iFU_all_p = Convert.ToInt32(((TextBox)e.Item.FindControl("txtFUallp")).Text.Trim());
                }
            }

            if (((TextBox)e.Item.FindControl("txtFDepartment")).Text.Trim() == "")
                strFDepartment = "";
            else
                strFDepartment = ((TextBox)e.Item.FindControl("txtFDepartment")).Text.Trim();
            if (((TextBox)e.Item.FindControl("txtFMenupart")).Text.Trim() == "")
                strFMenu_part = "";
            else
                strFMenu_part = ((TextBox)e.Item.FindControl("txtFMenupart")).Text.Trim();
            if (((TextBox)e.Item.FindControl("txtFCnName")).Text.Trim() == "")
                strFCnname = "";
            else
                strFCnname = ((TextBox)e.Item.FindControl("txtFCnName")).Text.Trim();

            if (!strFUsername.Equals(" ") && !strFPwd.Equals(" "))
            {
                string strSql = "SELECT * FROM SHP.CMCS_SFC_USERS where username ='" + strFUsername + "'";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dgUser.DataSource = this.GetIDTable(dt).DefaultView;
                    dgUser.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該用戶已經存在,不能新增...');</script>");
                    return;
                }
                else
                {
                    string strsql1 = "insert into SHP.CMCS_SFC_USERS values('" +
                        strFUsername + "','" + strFPwd + "',TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd") + "','yyyy/MM/dd'),'" + strFRemark + "','"
                        + strFDept + "','" + strFGroupid + "','" + strFModule + "','" + strFName + "'," + iFU_del + ","
                        + iFU_update + "," + iFU_all_p + ",'" + strFDepartment + "','" + strFMenu_part + "','" + strFCnname + "')";
                    try
                    {
                        ClsGlobal.objDataConnect.DataExecute(strsql1);
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('插入操作異常...');</script>");
                        return;
                    }

                    dgUser.EditItemIndex = -1;
                    BindData(strFUsername);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入用戶名和密碼...');</script>");
                return;
            }
        }
        if (e.CommandName == "ItemDelete")
        {
            string strUsername = ((Label)e.Item.FindControl("lbUserName")).Text.Trim();
            string strSql = "SELECT * FROM SHP.CMCS_SFC_USERS where username ='" + strUsername + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strsql2 = "delete from SHP.CMCS_SFC_USERS where username ='" + strUsername + "'";
                ClsGlobal.objDataConnect.DataExecute(strsql2);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該用戶數據異常,無法刪除...');</script>");
                return;
            }
            
            dgUser.Visible = false; 
        }
    }

    protected void dgUser_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string strUser = txtUser.Text.Trim();
        dgUser.CurrentPageIndex = e.NewPageIndex;
        BindData(strUser);
    }

    protected void dgUser_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strPwd;
        string strRemark;
        string strDept;
        string strGroupid;
        string strModule;
        string strName;
        string strUdel;
        string strUupdate;
        string strUallp;
        string strDepartment;
        string strMenupart;
        string strCnname; 
        string strUser = ((Label)e.Item.Cells[1].Controls[1]).Text.ToString();
        string strDdate = ((Label)e.Item.Cells[3].Controls[1]).Text.ToString(); 
        if (((TextBox)e.Item.Cells[2].Controls[1]).Text.ToString().Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入密碼..');</script>");
            return;
        }
        else
            strPwd = ((TextBox)e.Item.Cells[4].Controls[1]).Text.ToString();
        
        if (((TextBox)e.Item.Cells[4].Controls[1]).Text.ToString().Equals(""))
        {
            strRemark =" ";
        }
        else
            strRemark = ((TextBox)e.Item.Cells[4].Controls[1]).Text.ToString();

        if (((TextBox)e.Item.Cells[5].Controls[1]).Text.ToString().Equals(""))
        {
            strDept = " ";
        }
        else
            strDept = ((TextBox)e.Item.Cells[5].Controls[1]).Text.ToString();

        if (((TextBox)e.Item.Cells[6].Controls[1]).Text.ToString().Equals(""))
        {
            strGroupid = " ";
        }
        else
            strGroupid = ((TextBox)e.Item.Cells[6].Controls[1]).Text.ToString();

        if (((TextBox)e.Item.Cells[7].Controls[1]).Text.ToString().Equals(""))
        {
            strModule = " ";
        }
        else
            strModule = ((TextBox)e.Item.Cells[7].Controls[1]).Text.ToString();
        
        if (((TextBox)e.Item.Cells[8].Controls[1]).Text.ToString().Equals(""))
        {
            strName =" ";
        }
        else
            strName = ((TextBox)e.Item.Cells[8].Controls[1]).Text.ToString();

        if (((TextBox)e.Item.Cells[9].Controls[1]).Text.ToString().Equals(""))
        {
            strUdel = "0";
        }
        else
        {
            if (((TextBox)e.Item.Cells[9].Controls[1]).Text.Length > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                return;
            }
            else
            {
                char ichar = Convert.ToChar(((TextBox)e.Item.Cells[9].Controls[1]).Text);
                if (!(ichar >= '0' && ichar <= '9'))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                    return;
                }
                else
                    strUdel = ((TextBox)e.Item.Cells[9].Controls[1]).Text.ToString();
            }
        }

        if (((TextBox)e.Item.Cells[10].Controls[1]).Text.ToString().Equals(""))
        {
            strUupdate = "0";
        }
        else
        {
            if (((TextBox)e.Item.Cells[10].Controls[1]).Text.Length > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_UPDATE僅接受0-9的自然數...');</script>");
                return;
            }
            else
            {
                char ichar = Convert.ToChar(((TextBox)e.Item.Cells[10].Controls[1]).Text);
                if (!(ichar >= '0' && ichar <= '9'))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                    return;
                }
                else
                    strUupdate = ((TextBox)e.Item.Cells[10].Controls[1]).Text.ToString();
            }
        }

        if (((TextBox)e.Item.Cells[11].Controls[1]).Text.ToString().Equals(""))
        {
            strUallp = "0";
        }
        else
        {
            if (((TextBox)e.Item.Cells[11].Controls[1]).Text.Length > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_ALL_P僅接受0-9的自然數...');</script>");
                return;
            }
            else
            {
                char ichar = Convert.ToChar(((TextBox)e.Item.Cells[11].Controls[1]).Text);
                if (!(ichar >= '0' && ichar <= '9'))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_ALL_P僅接受0-9的自然數...');</script>");
                    return;
                }
                else
                    strUallp = ((TextBox)e.Item.Cells[11].Controls[1]).Text.ToString();
            }
        }

        if (((TextBox)e.Item.Cells[12].Controls[1]).Text.ToString().Equals(""))
        {
            strDepartment = " ";
        }
        else
            strDepartment = ((TextBox)e.Item.Cells[12].Controls[1]).Text.ToString();

        if (((TextBox)e.Item.Cells[13].Controls[1]).Text.ToString().Equals(""))
        {
            strMenupart = " ";
        }
        else
            strMenupart = ((TextBox)e.Item.Cells[13].Controls[1]).Text.ToString();

        if (((TextBox)e.Item.Cells[14].Controls[1]).Text.ToString().Equals(""))
        {
            strCnname = " ";
        }
        else
            strCnname = ((TextBox)e.Item.Cells[14].Controls[1]).Text.ToString();
 
        //更新操作
        int udel,uupdate,uallp;
        udel = Convert.ToInt32(strUdel);
        uupdate = Convert.ToInt32(strUupdate);
        uallp = Convert.ToInt32(strUallp);
        string strSql2 = "select * from SHP.CMCS_SFC_USERS WHERE USERNAME LIKE '" + strUser + "%'";
        DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strSql2).Tables[0];
        if (dt2.Rows.Count > 0)
        {
            string strsql3 = "update SHP.CMCS_SFC_USERS set password='" +
                strPwd + "',remark='" + strRemark + "', dept='" + strDept
                + "',groupid='" + strGroupid + "',module='" + strModule + "',name='" + strName
                + "',u_del=" + udel + ",u_update=" + uupdate + ",u_all_p=" + uallp + ",department='" + strDepartment
                + "',menu_part='" + strMenupart + "',cnname='" + strCnname + "'  where username= '" + strUser + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql3);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在該用戶,無法修改...');</script>");
            return;
        }
        dgUser.EditItemIndex = -1;
        BindData(strUser); 
    }

    protected void dgUser_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgUser.PageSize) * (dgUser.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((LinkButton)(e.Item.Cells[16].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?操作不可返回,按確認繼續..');");

        }
    }
}
