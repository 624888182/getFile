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

public partial class Boundary_wfrmE2PMantenance : System.Web.UI.UserControl
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
        dgE2p.Visible = true;
        string strsql = "SELECT * FROM SHP.CMCS_E2P_CHECK where MODEL like '" + model.ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgE2p.DataSource = this.GetIDTable(dt1).DefaultView;
            dgE2p.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該幾種的E2P Check項信息...');</script>");
            return;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "SELECT * FROM SHP.CMCS_E2P_CHECK where MODEL like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgE2p.DataSource = this.GetIDTable(dt1).DefaultView;
            dgE2p.DataBind();
        }
        else
        {
            dgE2p.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無該幾種的E2P Check項信息...');</script>");
            return;
        }
    }

    protected void dgE2p_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgE2p.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strModel); 
    }

    protected void dgE2p_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strModel = txtModel.Text.Trim();
        dgE2p.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strModel);
    }

    protected void dgE2p_ItemCommand(object source, DataGridCommandEventArgs e)
    {

        string strsql0 = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
        DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
        ViewState["UserName"] = dt0.Rows[0]["USERNAME"].ToString();

        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgE2p.ShowFooter = true;
            string strsql = "SELECT * FROM SHP.CMCS_E2P_CHECK where MODEL like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgE2p.DataSource = this.GetIDTable(dt1).DefaultView;
                dgE2p.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgE2p.ShowFooter = false;
            string strsql = "SELECT * FROM SHP.CMCS_E2P_CHECK where MODEL like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgE2p.DataSource = this.GetIDTable(dt1).DefaultView;
                dgE2p.DataBind();
            }
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgE2p.ShowFooter = false;
            string strFid;
            int intid;
            string strFmodel;
            string strFpackingline;
            string strFpid;
            string strFimei;
            string strFpicasso;
            string strFerrormsg;
            string strFprocesstime;
            string strFrepair;
            string strFprivilegepwd;
            string strFnkey;
            string strFnskey;
            string strFspkey;
            string strFckey;
            string strFpkey;
            string strFfskey;
            string strFrandomseed;
            string strFe2preffile;
            string strFe2pprofile;
            string strFbtaddress;
            string strFsugnumber;
            string strFmoduleproductid;
            string strFphonever;
            string strFouterfid;
            string strFinnerfid;
            string strFpderrorcode;
            string strFpderrormsg;
            string strFmiscinfo;
            string strFdisable;

            if (((TextBox)e.Item.FindControl("txtFID")).Text.Trim().ToUpper() == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('ID不能為空...');</script>");
                return;
            }
            else
            {
                strFid = ((TextBox)e.Item.FindControl("txtFID")).Text.Trim().ToUpper(); 
                if (((TextBox)e.Item.FindControl("txtFID")).Text.Length > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                    return;
                }
                else
                {  
                    char ichar = Convert.ToChar(((TextBox)e.Item.FindControl("txtFID")).Text);
                    if (!(ichar >= '0' && ichar <= '9'))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('U_DEL僅接受0-9的自然數...');</script>");
                        return;
                    }
                    else
                        intid = Convert.ToInt32(((TextBox)e.Item.FindControl("txtFID")).Text.Trim());
                }
            }
            if (((TextBox)e.Item.FindControl("txtFModel")).Text.Trim().ToUpper() == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Model不能為空...');</script>");
                return;
            }
            else
                strFmodel = ((TextBox)e.Item.FindControl("txtFModel")).Text.Trim().ToUpper();
            
            string csql="SELECT * FROM SHP.CMCS_E2P_CHECK where id="+intid+" and model='"+strFmodel+"'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(csql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('此幾種對應設置已存在,請先確認...');</script>");
                return;
            }
            else
            {
                if (((TextBox)e.Item.FindControl("txtFPackingline")) == null)
                    strFpackingline = "";
                else
                    strFpackingline = ((TextBox)e.Item.FindControl("txtFPackingline")).Text.Trim().ToUpper();

                if (((TextBox)e.Item.FindControl("txtFPidflag")) == null)
                    strFpid = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFPidflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PID_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFpid = ((TextBox)e.Item.FindControl("txtFPidflag")).Text.Trim().ToUpper();                  
                }

                if (((TextBox)e.Item.FindControl("txtFImeiflag")) == null)
                    strFimei = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFImeiflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFimei = ((TextBox)e.Item.FindControl("txtFImeiflag")).Text.Trim().ToUpper();
                }

                if (((TextBox)e.Item.FindControl("txtFPicassoflag")) == null)
                    strFpicasso = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFPicassoflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PICASSO_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFpicasso = ((TextBox)e.Item.FindControl("txtFPicassoflag")).Text.Trim().ToUpper();
                }

                if (((TextBox)e.Item.FindControl("txtFErrormsgflag")) == null)
                    strFerrormsg = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFErrormsgflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('ERRORMSG_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else 
                        strFerrormsg = ((TextBox)e.Item.FindControl("txtFErrormsgflag")).Text.Trim().ToUpper(); 
                }

                if (((TextBox)e.Item.FindControl("txtFProcesstimeflag")) == null)
                    strFprocesstime = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFProcesstimeflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PROCESSTIME_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else 
                        strFprocesstime = ((TextBox)e.Item.FindControl("txtFProcesstimeflag")).Text.Trim().ToUpper(); 
                }
                if (((TextBox)e.Item.FindControl("txtFRepairflag")) == null)
                    strFrepair = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFRepairflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('REPAIR_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFrepair = ((TextBox)e.Item.FindControl("txtFRepairflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFPrivilegepwdflag")) == null)
                    strFprivilegepwd = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFPrivilegepwdflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PRIVILEGEPWD_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFprivilegepwd = ((TextBox)e.Item.FindControl("txtFPrivilegepwdflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFNkeyflag")) == null)
                    strFnkey = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFNkeyflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('NKEY_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFnkey = ((TextBox)e.Item.FindControl("txtFNkeyflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFNskeyflag")) == null)
                    strFnskey = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFNskeyflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('NSKEY_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFnskey = ((TextBox)e.Item.FindControl("txtFNskeyflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFSpkeyflag")) == null)
                    strFspkey = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFSpkeyflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SPKEY_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFspkey = ((TextBox)e.Item.FindControl("txtFSpkeyflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFCkeyflag")) == null)
                    strFckey = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFCkeyflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('CKEY_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFckey = ((TextBox)e.Item.FindControl("txtFCkeyflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFPkeyflag")) == null)
                    strFpkey = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFPkeyflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PKEY_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFpkey = ((TextBox)e.Item.FindControl("txtFPkeyflag")).Text.Trim().ToUpper();
                }

                if (((TextBox)e.Item.FindControl("txtFFskeyflag")) == null)
                    strFfskey = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFFskeyflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('FSKEY_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFfskey = ((TextBox)e.Item.FindControl("txtFFskeyflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFRandomseedflag")) == null)
                    strFrandomseed = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFRandomseedflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('RANDOMSEED_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFrandomseed = ((TextBox)e.Item.FindControl("txtFRandomseedflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFE2preffileflag")) == null)
                    strFe2preffile = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFE2preffileflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('E2PREFFILE_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFe2preffile = ((TextBox)e.Item.FindControl("txtFE2preffileflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFE2pprofileflag")) == null)
                    strFe2pprofile = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFE2pprofileflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('E2PPROFILE_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFe2pprofile = ((TextBox)e.Item.FindControl("txtFE2pprofileflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFBtaddressflag")) == null)
                    strFbtaddress = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFBtaddressflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('BTADDRESS_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFbtaddress = ((TextBox)e.Item.FindControl("txtFBtaddressflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFSugflag")) == null)
                    strFsugnumber = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFSugflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SUGNUMBER_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFsugnumber = ((TextBox)e.Item.FindControl("txtFSugflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFModuleflag")) == null)
                    strFmoduleproductid = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFModuleflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('MODULEPRODUCTID_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFmoduleproductid = ((TextBox)e.Item.FindControl("txtFModuleflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFPhoneverflag")) == null)
                    strFphonever = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFPhoneverflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PHONEVER_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFphonever = ((TextBox)e.Item.FindControl("txtFPhoneverflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFOuterflag")) == null)
                    strFouterfid = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFOuterflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('OUTERFID_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFouterfid = ((TextBox)e.Item.FindControl("txtFOuterflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFInnerflag")) == null)
                    strFinnerfid = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFInnerflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('INNERFID_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFinnerfid = ((TextBox)e.Item.FindControl("txtFInnerflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFPderrorcodeflag")) == null)
                    strFpderrorcode = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFPderrorcodeflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PDERRORCODE_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFpderrorcode = ((TextBox)e.Item.FindControl("txtFPderrorcodeflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFPderrormsgflag")) == null)
                    strFpderrormsg = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFPderrormsgflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PDERRORMSG_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFpderrormsg = ((TextBox)e.Item.FindControl("txtFPderrormsgflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFMiscinfoflag")) == null)
                    strFmiscinfo = "";
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFMiscinfoflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('MISCINFO_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFmiscinfo = ((TextBox)e.Item.FindControl("txtFMiscinfoflag")).Text.Trim().ToUpper();
                }
                if (((TextBox)e.Item.FindControl("txtFDisableflag")) == null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('DISABLE_FLAG欄位不能為空...');</script>");
                    return;
                }
                else
                {
                    if (((TextBox)e.Item.FindControl("txtFDisableflag")).Text.Length > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('DISABLE_FLAG長度不能大於2...');</script>");
                        return;
                    }
                    else
                        strFdisable = ((TextBox)e.Item.FindControl("txtFDisableflag")).Text.Trim().ToUpper();
                }

                string strsql = "Insert into SHP.CMCS_E2P_CHECK(ID,MODEL,PACKING_LINE,PID_FLAG,IMEI_FLAG,PICASSO_FLAG,ERRORMSG_FLAG,PROCESSTIME_FLAG,REPAIR_FLAG,PRIVILEGEPWD_FLAG,NKEY_FLAG,NSKEY_FLAG,SPKEY_FLAG,CKEY_FLAG,PKEY_FLAG,FSKEY_FLAG,RANDOMSEED_FLAG,E2PREFFILE_FLAG,E2PPROFILE_FLAG,BTADDRESS_FLAG,SUGNUMBER_FLAG,MODULEPRODUCTID_FLAG,PHONEVER_FLAG,OUTERFID_FLAG,INNERFID_FLAG,PDERRORCODE_FLAG,PDERRORMSG_FLAG,MISCINFO_FLAG,DISABLE_FLAG,CREATE_DATE,EMP_NAME) values(" +
                    intid + ",'" + strFmodel + "','" + strFpackingline + "','" + strFpid + "','" + strFimei + "','" + strFpicasso
                    + "','" + strFerrormsg + "','" + strFprocesstime + "','" + strFrepair + "','" + strFprivilegepwd + "','" + 
                    strFnkey + "','" + strFnskey + "','" + strFspkey + "','" + strFckey + "','" + strFpkey + "','" +
                    strFfskey + "','" + strFrandomseed + "','" + strFe2preffile + "','" + strFe2pprofile + "','" + 
                    strFbtaddress + "','" + strFsugnumber + "','" + strFmoduleproductid + "','" + strFphonever + "','" + 
                    strFouterfid + "','" + strFinnerfid + "','" + strFpderrorcode + "','" + strFpderrormsg + "','" +
                    strFmiscinfo + "','" + strFdisable + "',sysdate,'" + ViewState["UserName"] + "')";
                ClsGlobal.objDataConnect.DataExecute(strsql);
                dgE2p.EditItemIndex = -1;
                string strsql2 = "SELECT * FROM SHP.CMCS_E2P_CHECK where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
                DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    lbcount.Text = "Total:" + dt2.Rows.Count;

                    dgE2p.DataSource = this.GetIDTable(dt2).DefaultView;
                    dgE2p.DataBind();
                }
            }
        }

        if (e.CommandName == "ItemDelete")
        {
            string strID = ((Label)e.Item.Cells[1].Controls[1]).Text;
            int intid = Convert.ToInt32(strID);
            string strModel = ((Label)e.Item.Cells[2].Controls[1]).Text;

            string strsql = "delete from SHP.CMCS_E2P_CHECK where ID=" + intid + " and MODEL='" + strModel + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgE2p.EditItemIndex = -1;
            string strsql1 = "SELECT * FROM SHP.CMCS_E2P_CHECK where Model like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                lbcount.Text = "Total:" + dt1.Rows.Count;
                dgE2p.DataSource = this.GetIDTable(dt1).DefaultView;
                dgE2p.DataBind();
            }
            else
            {
                txtModel.Text = "";
                string strsql2 = "SELECT * FROM SHP.CMCS_E2P_CHECK ";
                DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                lbcount.Text = "Total:" + dt2.Rows.Count;

                dgE2p.DataSource = this.GetIDTable(dt2).DefaultView;
                dgE2p.DataBind();
            }
        }
    }

    protected void dgE2p_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgE2p.CurrentPageIndex = e.NewPageIndex;
        string strsql = "SELECT * FROM SHP.CMCS_E2P_CHECK where MODEL like '" + this.txtModel.Text.Trim().ToUpper() + "%'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        dgE2p.DataSource = this.GetIDTable(dt).DefaultView;
        dgE2p.DataBind();
        lbcount.Text = "Current Page:" + (dgE2p.CurrentPageIndex + 1).ToString() + "/" + dgE2p.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }

    protected void dgE2p_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strHID = ((Label)e.Item.Cells[1].Controls[1]).Text;
        string strHModel = ((Label)e.Item.Cells[2].Controls[1]).Text;
        string strHPackingline = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
        string strHPID = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
        string strHImei = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
        string strHPicasso = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
        string strHErrormsg = ((TextBox)e.Item.Cells[7].Controls[1]).Text;
        string strHProcesstime = ((TextBox)e.Item.Cells[8].Controls[1]).Text;
        string strHRepair = ((TextBox)e.Item.Cells[9].Controls[1]).Text;
        string strHPrivilegepwd = ((TextBox)e.Item.Cells[10].Controls[1]).Text;
        string strHNkey = ((TextBox)e.Item.Cells[11].Controls[1]).Text;
        string strHNskey = ((TextBox)e.Item.Cells[12].Controls[1]).Text;
        string strHSpkey = ((TextBox)e.Item.Cells[13].Controls[1]).Text;
        string strHCkey = ((TextBox)e.Item.Cells[14].Controls[1]).Text;
        string strHPkey = ((TextBox)e.Item.Cells[15].Controls[1]).Text;
        string strHFskey = ((TextBox)e.Item.Cells[16].Controls[1]).Text;
        string strHRandomseed = ((TextBox)e.Item.Cells[17].Controls[1]).Text;
        string strHE2preffile = ((TextBox)e.Item.Cells[18].Controls[1]).Text;
        string strHE2pprofile = ((TextBox)e.Item.Cells[19].Controls[1]).Text;
        string strHBtaddress = ((TextBox)e.Item.Cells[20].Controls[1]).Text;
        string strHSugnumber = ((TextBox)e.Item.Cells[21].Controls[1]).Text;
        string strHModuleproductid = ((TextBox)e.Item.Cells[22].Controls[1]).Text;
        string strHPhonever = ((TextBox)e.Item.Cells[23].Controls[1]).Text;
        string strHOuterfid = ((TextBox)e.Item.Cells[24].Controls[1]).Text;
        string strHInnerfid = ((TextBox)e.Item.Cells[25].Controls[1]).Text;
        string strHPderrorcode = ((TextBox)e.Item.Cells[26].Controls[1]).Text;
        string strHPderrormsg = ((TextBox)e.Item.Cells[27].Controls[1]).Text;
        string strHMiscinfo = ((TextBox)e.Item.Cells[28].Controls[1]).Text;
        string strHDisable = ((TextBox)e.Item.Cells[29].Controls[1]).Text;

        int intid = Convert.ToInt32(strHID);

        if (strHPID.Length> 2)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PID_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHImei.Length > 2)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHPicasso.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PICASSO_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHErrormsg.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('ERRORMSG_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHProcesstime.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PROCESSTIME_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHRepair.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('REPAIR_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHPrivilegepwd.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PRIVILEGEPWD_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHNkey.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('NKEY_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHNskey.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('NSKEY_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHSpkey.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SPKEY_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHCkey.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('CKEY_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHPkey.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PKEY_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHFskey.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('FSKEY_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHRandomseed.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('RANDOMSEED_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHE2preffile.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('E2PREFFILE_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHE2pprofile.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('E2PPROFILE_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHBtaddress.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('BTADDRESS_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHSugnumber.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SUGNUMBER_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHModuleproductid.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('MODULEPRODUCTID_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHPhonever.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PHONEVER_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHOuterfid.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('OUTERFID_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHInnerfid.Length > 2)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('INNERFID_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHPderrorcode.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PDERRORCODE_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHPderrormsg.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PDERRORMSG_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHMiscinfo.Length > 2) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('MISCINFO_FLAG長度不能大於2...');</script>");
            return;
        }
        if (strHDisable.Length > 2)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('DISABLE_FLAG長度不能大於2...');</script>");
            return;
        } 

        try
        {
            string strsql = "update SHP.CMCS_E2P_CHECK set PACKING_LINE='" + strHPackingline + "',PID_FLAG='" + strHPID + "',IMEI_FLAG='" + strHImei + "',PICASSO_FLAG='" + strHPicasso
                + "',ERRORMSG_FLAG='" + strHErrormsg + "',PROCESSTIME_FLAG='" + strHProcesstime + "',REPAIR_FLAG='" + strHRepair + "',PRIVILEGEPWD_FLAG='" + strHPrivilegepwd
                + "',NKEY_FLAG='" + strHNkey + "',NSKEY_FLAG='" + strHNskey + "',SPKEY_FLAG='" + strHSpkey + "',CKEY_FLAG='" + strHCkey + "',PKEY_FLAG='" + strHPkey
                + "',FSKEY_FLAG='" + strHFskey + "',RANDOMSEED_FLAG='" + strHRandomseed + "',E2PREFFILE_FLAG='" + strHE2preffile + "',E2PPROFILE_FLAG='" + strHE2pprofile
                + "',BTADDRESS_FLAG='" + strHBtaddress + "',SUGNUMBER_FLAG='" + strHSugnumber + "',MODULEPRODUCTID_FLAG='" + strHModuleproductid 
                + "',PHONEVER_FLAG='" + strHPhonever + "',OUTERFID_FLAG='" + strHOuterfid + "',INNERFID_FLAG='" + strHInnerfid + "',PDERRORCODE_FLAG='" + strHPderrorcode 
                + "',PDERRORMSG_FLAG='" + strHPderrormsg + "',MISCINFO_FLAG='" + strHMiscinfo + "',DISABLE_FLAG='" + strHDisable + "' where ID=" + intid + " AND MODEL='" + strHModel + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgE2p.EditItemIndex = -1; 
            string strSql = "SELECT * FROM SHP.CMCS_E2P_CHECK where MODEL like '" + strHModel + "%'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            dgE2p.DataSource = this.GetIDTable(dt).DefaultView;
            dgE2p.DataBind(); 
        }
        catch
        { 
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('更新數據有誤...');</script>");
            return;
        } 
    }

    protected void dgE2p_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgE2p.PageSize) * (dgE2p.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            ((LinkButton)(e.Item.Cells[33].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?');"); 
    }
}
