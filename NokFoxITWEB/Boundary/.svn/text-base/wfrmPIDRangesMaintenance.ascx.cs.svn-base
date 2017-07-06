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
using System.Text;

public partial class Boundary_wfrmPIDRangesMaintenance : System.Web.UI.UserControl
{
    private string format = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage(); 
            BindModel();
        }
    }

    private void BindModel()
    {
        string StrSql = "select distinct model from SFC.CMCS_SFC_MODEL order by model";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        ddlModel.DataTextField = "model";
        ddlModel.DataValueField = "model";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();
    }

    private void BindData(string model,string type)
    {
        dgLabelRange.Visible = true; 
        string strsql = "SELECT * FROM SFC.CMCS_SFC_NUMBER_RANGES where PROJECT_CODE like  '" + model + "%'";
        if (!type.Equals(""))
            strsql += " and NUMBER_CATEGORY ='" + type + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgLabelRange.DataSource = this.GetIDTable(dt1).DefaultView;
            dgLabelRange.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該條碼範圍尚未維護！！');</script>");
            return;
        }
    }

    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        lblType.Text = (String)GetGlobalResourceObject("SFCQuery", "Type");
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

    private long To_Number(string innum,string rangenumber)
    {
        int i;
        int Findex;
        int N;
        long Sum=0;
        N = rangenumber.Length;
        for (i = 0; i < innum.Length; i++)
        {
            Findex = rangenumber.IndexOf(innum.Substring(i, 1));
            if (Findex == -1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('異常..參數傳遞錯誤!請確認數據是否合法..');</script>");
                return -1;
            }
            Sum = Sum * N + Findex;
        }
        return Sum;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strModel = ddlModel.SelectedValue.Trim();
        string strsql = "SELECT * FROM SFC.CMCS_SFC_NUMBER_RANGES where PROJECT_CODE like  '" + strModel + "%'";
        string strType = ddlType.SelectedValue.Trim();
        if(!strType.Equals(""))
            strsql += " and NUMBER_CATEGORY ='" + strType + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgLabelRange.Visible = true;
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgLabelRange.DataSource = this.GetIDTable(dt1).DefaultView;
            dgLabelRange.DataBind();
        }
        else
        {
            dgLabelRange.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該幾種信息尚未維護！！');</script>");
            return;
        }
    }
    
    protected void dgLabelRange_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = ddlModel.SelectedValue.Trim();
        string strType = ddlType.SelectedValue.Trim();
        dgLabelRange.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strModel, strType); 
    }

    protected void dgLabelRange_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = ddlModel.SelectedValue.Trim();
        string strType = ddlType.SelectedValue.Trim();
        dgLabelRange.EditItemIndex = e.Item.ItemIndex;     
        BindData(strModel, strType); 
    }

    protected void dgLabelRange_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strPrefix;
        string strFirst;
        string strLast;
        int FirstLen;
        int LastLen;
        long First;
        long Last; 
        string strModel = ((Label)e.Item.Cells[1].Controls[1]).Text.ToString();
        string strType = ((Label)e.Item.Cells[2].Controls[1]).Text.ToString();
        string strRegion = ((Label)e.Item.Cells[3].Controls[1]).Text.ToString();
        if (((TextBox)e.Item.Cells[4].Controls[1]).Text.ToString().Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入條碼前綴..');</script>");
            return;
        }
        else 
            strPrefix = ((TextBox)e.Item.Cells[4].Controls[1]).Text.ToString(); 
        if (((TextBox)e.Item.Cells[5].Controls[1]).Text.ToString().Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入起始範圍..');</script>");
            return;
        }
        else
            strFirst = ((TextBox)e.Item.Cells[5].Controls[1]).Text.ToString();
        if (((TextBox)e.Item.Cells[6].Controls[1]).Text.ToString().Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入結束值..');</script>");
            return;
        }
        else
            strLast = ((TextBox)e.Item.Cells[6].Controls[1]).Text.ToString();
        FirstLen = strFirst.Length;
        LastLen = strLast.Length;
        if (FirstLen != LastLen)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('流水碼上限與下限長度應該一致..');</script>");
            return;
        }
        First = To_Number(strFirst,format);
        Last = To_Number(strLast, format);
        if (First >= Last)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('流水碼上限不能大於下限..');</script>");
            return;
        }

        for (int i = 0; i <strPrefix.Length; i++)
        {
            //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1)); 
            string c = strPrefix.Substring(i, 1);
            char ichar =Convert.ToChar(c);
            if (!(ichar >= 'A' && ichar <= 'Z') && !(ichar >= '0' && ichar <= '9'))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('前綴規格錯誤..');</script>");
                return;
            }
        }

        for (int i = 0; i < FirstLen; i++)
        {
            //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1));
            string c = strFirst.Substring(i, 1);
            char ichar = Convert.ToChar(c);
            if (!(ichar >= 'A' && ichar <= 'Z') && !(ichar >= '0' && ichar <= '9'))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('上限規格錯誤..');</script>");
                return;
            }
        }

        for (int i = 0; i < LastLen; i++)
        {
            //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1));
            string c = strLast.Substring(i, 1);
            char ichar = Convert.ToChar(c);
            if (!(ichar >= 'A' && ichar <= 'Z') && !(ichar >= '0' && ichar <= '9'))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('下限規格錯誤..');</script>");
                return;
            }
        }

        switch (ddlType.SelectedValue)
        {
            case "Imei":
                {
                    string strF = strPrefix + strFirst;
                    string strL = strPrefix + strLast;
                    if (strF.Length != 14 || strL.Length != 14)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Imei長度應該為14位,設置錯誤..');</script>");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < strF.Length; i++)
                        {
                            //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1)); 
                            string c = strF.Substring(i, 1);
                            char ichar = Convert.ToChar(c);
                            if (!(ichar >= '0' && ichar <= '9'))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Imei範圍必須為數字..');</script>");
                                return;
                            }
                        }
                        for (int i = 0; i < strL.Length; i++)
                        {
                            //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1));
                            string c = strL.Substring(i, 1);
                            char ichar = Convert.ToChar(c);
                            if (!(ichar >= '0' && ichar <= '9'))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Imei範圍必須為數字..');</script>");
                                return;
                            }
                        }
                    }
                }
                break;
            case "BlueTooth":
                {
                    string strF = strPrefix + strFirst;
                    string strL = strPrefix + strLast;
                    if (strF.Length != 12 || strL.Length != 12)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('BtAddress長度應該為12位,設置錯誤..');</script>");
                        return;
                    }
                }
                break;
            case "Picasso":
                {
                    string strP1="";
                    string strF1="";
                    string strL1="";
                    for (int i = 0; i < strPrefix.Length; i++)
                    {
                        //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1)); 
                        string c = strPrefix.Substring(i, 1);
                        char ichar = Convert.ToChar(c);
                        if (!(ichar >= '0' && ichar <= '9'))
                        {
                            strP1 = "string";
                            return;
                        }
                    }
                    for (int i = 0; i < strFirst.Length; i++)
                    {
                        //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1));
                        string c = strFirst.Substring(i, 1);
                        char ichar = Convert.ToChar(c);
                        if (!(ichar >= '0' && ichar <= '9'))
                        {
                            strF1 = "string";
                            return;
                        }
                    }

                    for (int i = 0; i < strLast.Length; i++)
                    {
                        //int ichar = System.Text.Encoding.ASCII.GetBytes(strPrefix.Substring(i, 1)); 
                        string c = strLast.Substring(i, 1);
                        char ichar = Convert.ToChar(c);
                        if (!(ichar >= '0' && ichar <= '9'))
                        {
                            strL1 = "string";
                            return;
                        }
                    }
                    if (strP1.Equals("string"))
                    {
                        if (!strF1.Equals("string") || !strL1.Equals("string"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Picasso前綴為數字,整個編碼都必須為數字..');</script>");
                            return;
                        }
                        string strFF = strPrefix + strPrefix;
                        string strFL = strPrefix + strLast;
                        if (strFF.Length != 7 || strFL.Length != 7)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Picasso長度設置錯誤,如果為數字的話必須為7位..');</script>");
                            return;
                        }
                    }
                }
                break;
        }
        if (!strPrefix.Equals(" ") && !strFirst.Equals(" ") && !strLast.Equals(" "))
        {
            string strSql = "select * from SFC.CMCS_SFC_NUMBER_RANGES WHERE PROJECT_CODE='" + strModel + "' AND NUMBER_CATEGORY='" + strType + "'  AND ORGANIZATION_ID='" + strRegion + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該機種對應設置在數據庫不存在,無法修改...');</script>");
                return;
            }
            else
            {
                string strSql1 = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql1).Tables[0];
                ViewState["UserName"] = dt1.Rows[0]["USERNAME"].ToString();


                //備份插入記錄
                string strsql1 = "insert into SFC.CMCS_SFC_RANGES_HISTORY values('37','" +
                    strType + "','" + strModel + "','" + strPrefix + "','Update','" + strFirst + "','"
                    + strLast + "',sysdate,'" + ViewState["UserName"] + "','')";
                ClsGlobal.objDataConnect.DataExecute(strsql1);

                //插入操作
                int id;
                string strSql2 = "select ID from SFC.CMCS_SFC_NUMBER_RANGES WHERE PROJECT_CODE='" + strModel + "'  AND NUMBER_CATEGORY='" + strType + "' AND ORGANIZATION_ID='" + strRegion + "'";
                DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strSql2).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt2.Rows[0]["ID"].ToString());

                    string strsql3 = "update SFC.CMCS_SFC_NUMBER_RANGES set prefix_code='" +
                        strPrefix + "',first_number='" + strFirst + "', last_number='" + strLast
                        + "',last_update_date=sysdate,last_updated_by='" + ViewState["UserName"]
                        + "'  where id= " + id;
                    ClsGlobal.objDataConnect.DataExecute(strsql3);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該機種對應設置在數據庫不存在,無法修改...');</script>");
                    return;
                }
                dgLabelRange.EditItemIndex = -1;
                BindData(strModel, strType);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('信息填寫有誤！！');</script>");
            return;
        }
    }

    protected void dgLabelRange_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgLabelRange.PageSize) * (dgLabelRange.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((LinkButton)(e.Item.Cells[12].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?操作不可返回,按確認繼續..');");
           
        } 
    }

    protected void dgLabelRange_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgLabelRange.ShowFooter = true;
            
            string strModel = ddlModel.SelectedValue.Trim();
            string strsql = "SELECT * FROM SFC.CMCS_SFC_NUMBER_RANGES where PROJECT_CODE like  '" + strModel + "%'";
            string strType = ddlType.SelectedValue.Trim();
            if (!strType.Equals(""))
                strsql += " and NUMBER_CATEGORY ='" + strType + "'";            
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgLabelRange.DataSource = this.GetIDTable(dt1).DefaultView;
                dgLabelRange.DataBind(); 
                dgLabelRange.Columns[1].FooterText = ddlModel.SelectedValue.Trim();
            }            
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgLabelRange.ShowFooter = false;
            string strModel = ddlModel.SelectedValue.Trim(); 
            string strType = "";            
            BindData(strModel, strType);
        }
        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgLabelRange.ShowFooter = false;
            string strFPrefix;
            string strFFirst;
            string strFLast;
            int FirstLen;
            int LastLen;
            long First;
            long Last; 
            //string strModel = ((Label)e.Item.FindControl("lbFModel")).Text.Trim();
            //string strType = ((Label)e.Item.FindControl("lbEType")).Text.Trim();
            //string strRegion = ((Label)e.Item.FindControl("lbERegion")).Text.Trim();
            string strModel = ddlModel.SelectedValue.ToString();
            string strType = ddlType.SelectedValue.ToString();
            if (strModel.Equals("") || strType.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇條碼類型..');</script>");
                return;
            }
            int region = 37;
            if (((TextBox)e.Item.FindControl("txtFPrefix")).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入條碼前綴..');</script>");
                return;
            }
            else
            {
                strFPrefix = ((TextBox)e.Item.FindControl("txtFPrefix")).Text.Trim();
            }
            if (((TextBox)e.Item.FindControl("txtFFirst")).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入起始範圍..');</script>");
                return;
            }
            else
                strFFirst = ((TextBox)e.Item.FindControl("txtFFirst")).Text.Trim();
            if (((TextBox)e.Item.FindControl("txtFLast")).Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入結束值..');</script>");
                return;
            }
            else
                strFLast = ((TextBox)e.Item.FindControl("txtFLast")).Text.Trim();

            FirstLen = strFFirst.Length;
            LastLen = strFLast.Length;
            if(FirstLen!=LastLen)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('流水碼上限與下限長度應該一致..');</script>");
                return;
            }
            First = To_Number(strFFirst, format);
            Last = To_Number(strFLast, format);
            if (First >= Last)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('流水碼上限不能大於下限..');</script>");
                return;
            }

            for (int i = 0; i < strFPrefix.Length; i++)
            {
                //int ichar = System.Text.Encoding.ASCII.GetBytes(strFPrefix.Substring(i, 1));
                string c = strFPrefix.Substring(i, 1);
                char ichar = Convert.ToChar(c);
                if (!(ichar >= 'A' && ichar <= 'Z') && !(ichar >= '0' && ichar <= '9'))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('前綴規格錯誤..');</script>");
                    return;
                }
            }

            for (int i = 0; i < FirstLen; i++)
            {
                //int ichar = System.Text.Encoding.ASCII.GetBytes(strFFirst.Substring(i, 1)); 
                string c = strFFirst.Substring(i, 1);
                char ichar = Convert.ToChar(c);
                if (!(ichar >= 'A' && ichar <= 'Z') && !(ichar >= '0' && ichar <= '9')) 
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('上限規格錯誤..');</script>");
                    return;
                }
            }

            for (int i = 0; i < LastLen; i++)
            {
                //int ichar = System.Text.Encoding.ASCII.GetBytes(strFLast.Substring(i, 1)); 
                string c = strFLast.Substring(i, 1);
                char ichar = Convert.ToChar(c);
                if (!(ichar >= 'A' && ichar <= 'Z') && !(ichar >= '0' && ichar <= '9'))  
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('下限規格錯誤..');</script>");
                    return;
                }
            }

            switch (ddlType.SelectedValue)
            {
                case "Imei":
                    {
                        string strF = strFPrefix + strFFirst;
                        string strL = strFPrefix + strFLast;
                        if (strF.Length != 14 || strL.Length != 14)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Imei長度應該為14位,設置錯誤..');</script>");
                            return;
                        }
                        else
                        {
                            for (int i = 0; i < strF.Length; i++)
                            {
                                //int ichar = System.Text.Encoding.ASCII.GetBytes(strF.Substring(i, 1)); 
                                string c = strF.Substring(i, 1);
                                char ichar = Convert.ToChar(c);
                                if (!(ichar >= '0' && ichar <= '9'))  
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Imei範圍必須為數字..');</script>");
                                    return;
                                }
                            }
                            for (int i = 0; i < strL.Length; i++)
                            {
                                //int ichar = System.Text.Encoding.ASCII.GetBytes(strL.Substring(i, 1)); 
                                string c = strL.Substring(i, 1);
                                char ichar = Convert.ToChar(c);
                                if (!(ichar >= '0' && ichar <= '9'))  
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Imei範圍必須為數字..');</script>");
                                    return;
                                }
                            }
                        }
                    }
                    break;
                case "BlueTooth":
                    {
                        string strF = strFPrefix + strFFirst;
                        string strL = strFPrefix + strFLast;
                        if (strF.Length != 12 || strL.Length != 12)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('BtAddress長度應該為12位,設置錯誤..');</script>");
                            return;
                        }
                    }
                    break;
                case "Picasso":
                    {
                        string strP1="";
                        string strF1="";
                        string strL1="";
                        for (int i = 0; i < strFPrefix.Length; i++)
                        {
                            //int ichar = System.Text.Encoding.ASCII.GetBytes(strFPrefix.Substring(i, 1)); 
                            string c = strFPrefix.Substring(i, 1);
                            char ichar = Convert.ToChar(c);
                            if (!(ichar >= '0' && ichar <= '9')) 
                            {
                                strP1="string";
                                return;
                            }
                        }  
                        for (int i = 0; i < strFFirst.Length; i++)
                        {
                            //int ichar = System.Text.Encoding.ASCII.GetBytes(strFFirst.Substring(i, 1)); 
                            string c = strFFirst.Substring(i, 1);
                            char ichar = Convert.ToChar(c);
                            if (!(ichar >= '0' && ichar <= '9')) 

                            {
                                strF1="string";
                                return;
                            }
                        }
                        
                        for (int i = 0; i <strFLast.Length; i++)
                        {
                            //int ichar = System.Text.Encoding.ASCII.GetBytes(strFLast.Substring(i, 1)); 
                            string c = strFLast.Substring(i, 1);
                            char ichar = Convert.ToChar(c);
                            if (!(ichar >= '0' && ichar <= '9')) 
                            {
                                strL1="string";
                                return;
                            }
                        }
                        if (strP1.Equals("string"))
                        {
                            if (!strF1.Equals("string") || !strL1.Equals("string"))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Picasso前綴為數字,整個編碼都必須為數字..');</script>");
                                return;
                            }
                            string strFF = strFPrefix + strFPrefix;
                            string strFL = strFPrefix + strFLast;
                            if (strFF.Length != 7 || strFL.Length != 7)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Picasso長度設置錯誤,如果為數字的話必須為7位..');</script>");
                                return;
                            }
                        }
                    }
                    break;
            }
            if (!strFPrefix.Equals(" ") && !strFFirst.Equals(" ") && !strFLast.Equals(" "))
            {
                string strSql = "select * from SFC.CMCS_SFC_NUMBER_RANGES WHERE PROJECT_CODE='" + strModel + "' AND NUMBER_CATEGORY='" + strType + "' AND ORGANIZATION_ID=" + region;
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbcount.Text = "Total:" + dt.Rows.Count;
                    dgLabelRange.DataSource = this.GetIDTable(dt).DefaultView;
                    dgLabelRange.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該機種對應設置在數據庫已經存在不可新增,您可以修改設置...！！');</script>");
                    return;
                }
                else
                {
                    string strSql0 = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
                    DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strSql0).Tables[0];
                    ViewState["UserName"] = dt0.Rows[0]["USERNAME"].ToString();

                    //備份插入記錄
                    string strsql1 = "insert into SFC.CMCS_SFC_RANGES_HISTORY values('37','" +
                        strType + "','" + strModel + "','" + strFPrefix + "','Create','" + strFFirst + "','"
                        + strFLast + "',sysdate,'" + ViewState["UserName"] + "','')";
                    ClsGlobal.objDataConnect.DataExecute(strsql1);

                    //插入操作
                    string strsql2 = "insert into SFC.CMCS_SFC_NUMBER_RANGES values('37','" +
                        strType + "','" + strModel + "','" + strFPrefix + "','ALL','" + strFFirst + "','"
                        + strFLast + "','" + strFFirst + "',sysdate,'" + ViewState["UserName"] + "',sysdate,'"
                        + ViewState["UserName"] + "','',sfc.cmcs_sfc_number_ranges_s.nextval)";
                    ClsGlobal.objDataConnect.DataExecute(strsql2);

                    dgLabelRange.EditItemIndex = -1;
                    strType = "";
                    BindData(strModel, strType);
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
            int id;
            string strModel = ((Label)e.Item.FindControl("lbModel")).Text.Trim();
            string strType = ((Label)e.Item.FindControl("lbType")).Text.Trim();
            string strRegion = ((Label)e.Item.FindControl("lbRegion")).Text.Trim();
            string strPrefix = ((Label)e.Item.FindControl("lbPrefix")).Text.Trim();
            string strFirst = ((Label)e.Item.FindControl("lbFirst")).Text.Trim();
            string strLast = ((Label)e.Item.FindControl("lbLast")).Text.Trim();
            string strSql = "select ID from SFC.CMCS_SFC_NUMBER_RANGES WHERE PROJECT_CODE='" + strModel + "' AND NUMBER_CATEGORY='" + strType + "' AND ORGANIZATION_ID='" + strRegion+"'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0]["ID"].ToString());

                //備份要刪除的數據
                string strsql1 = "insert into SFC.CMCS_SFC_RANGES_HISTORY values('" + strRegion + "','" +
                            strType + "','" + strModel + "','" + strPrefix + "','Delete','" + strFirst + "','"
                            + strLast + "',sysdate,'" + ViewState["UserName"] + "','')";
                ClsGlobal.objDataConnect.DataExecute(strsql1);

                string strsql2 = "delete from  sfc.CMCS_SFC_NUMBER_RANGES where  id=" + id;
                ClsGlobal.objDataConnect.DataExecute(strsql2);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該機種對應設置數據異常,無法刪除...！！');</script>");
                return;
            }
            dgLabelRange.Visible = false;
            lbcount.Visible = false;
        }
    }

    protected void dgLabelRange_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string strModel = ddlModel.SelectedValue.Trim();
        string strType = ddlType.SelectedValue.Trim();
        dgLabelRange.CurrentPageIndex = e.NewPageIndex;
        BindData(strModel, strType);
    }
}
