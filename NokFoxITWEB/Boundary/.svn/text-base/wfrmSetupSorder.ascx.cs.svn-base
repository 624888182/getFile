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
//using mshtml;
//using AxSHDocVw; 

public partial class Boundary_wfrmSetupSorder : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strWO = txtWO.Text.Trim().ToUpper();
        string strPart = txtPart.Text.Trim().ToUpper();
        string strVersion = txtVer.Text.Trim().ToUpper();
        string strQty = txtQty.Text.Trim().ToUpper();
        if (!IsPostBack)
        {
            MultiLanaguage();
            //Binddata();
        }

        Binddata();

        this.ButtonDelete.Attributes.Add("onclick", "return confirm('你确定要删除所选择的工單吗？');");

    }

    #region Web Form 設計工具產生的程式碼
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
    ///		這個方法的內容。
    /// </summary>
    private void InitializeComponent()
    {


    }
    #endregion

    private void Binddata()
    {
        string strsql = "select * from SHP.CMCS_SFC_SORDER order by creation_date desc ";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgSorder.DataSource = dt.DefaultView;
            dgSorder.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SHP.CMCS_SFC_SORDER表沒有數據！！');</script>");
            return;
        }
    }

    private void MultiLanaguage()
    {
        //btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    protected void btnWOInfo_Click(object sender, EventArgs e)
    {
        string strWO = txtWO.Text.Trim().ToUpper();
        if (strWO.Equals(""))
        {
            this.txtWO.Focus();
            //this.txtWO.Attributes["onFocus"] = "javascript:this.select();";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('工單不能為空！！');</script>");
            return;
        }
        else
        {
            string strSql = "select WO_NO,ASSEMBLY_ITEM,NVL(BOM_REVISION,'01') BOM_REVISION,CLASS_CODE,DECODE(STATUS_TYPE, 'CRTD','1','REL','3','TECO','4','DLV','4','LKD','6',STATUS_TYPE) STATUS_TYPE from SAP.SAP_WO_LIST where WO_NO ='" + strWO + "' and STATUS_TYPE <> '1'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtPart.Text = dt.Rows[0]["ASSEMBLY_ITEM"].ToString();
                txtVer.Text = dt.Rows[0]["BOM_REVISION"].ToString();
                this.txtQty.Focus();
                //this.txtQty.Attributes["onFocus"] = "javascript:this.select();";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('找不到此工單,請確認是否已Release！！');</script>");
                return;
            }
        }
        ckbDUO.Checked = false;
        ckbElse.Checked = false;
        string strsql = "SELECT Get_Sequence_From_PartNO2('" + txtPart.Text + "')   SequenceID from dual";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            string strSqID = dt1.Rows[0]["SequenceID"].ToString();
            rbl1.SelectedIndex = Convert.ToInt16(strSqID) - 1;
        }
    }
    protected void btnCreateWO_Click(object sender, EventArgs e)
    {
        string strWO = txtWO.Text.Trim().ToUpper();
        string strPart = txtPart.Text.Trim().ToUpper();
        string strVersion = txtVer.Text.Trim().ToUpper();
        string strQty = txtQty.Text.Trim().ToUpper();
        string strModelPre = "";
        string strymd;
        string temp = "";
        string strPre = "";

        if (!strWO.Equals(""))
        {
            string strsql1 = "SELECT * FROM SHP.CMCS_SFC_SORDER Where SORDER='" + strWO + "' order by creation_date desc ";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該工單已經創建！！');</script>");
                return;
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('工單不能為空！！');</script>");
            return;
        }
        if (strPart.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('料號不能為空！！');</script>");
            return;
        }
        else
        {
            string strsql = "SELECT Get_Sequence_From_PartNO2('" + strPart + "')   SequenceID from dual";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strSqID = dt.Rows[0]["SequenceID"].ToString();
                rbl1.SelectedIndex = Convert.ToInt16(strSqID) - 1;
            }
        }

        switch (rbl1.SelectedIndex)
        {
            case 0:
                strPre = "A";
                break;
            case 1:
                strPre = "B";
                break;
            case 2:
                strPre = "C";
                break;
        }

        if (strVersion.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('BOM版本不能為空！！');</script>");
            return;
        }

        if ((ckbDUO.Checked) || (ckbElse.Checked))
        {
            if (ckbElse.Checked)
            {
                strModelPre = "L";

                strModelPre += strPre;
            }
            else
            {
                if (ckb2.Checked)
                {
                    string strTime = txtTime.Text.Trim().ToString();
                    if (strTime.Equals(""))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('次數不能為空！！');</script>");
                        return;
                    }
                    else
                    {
                        string strSql = "select to_char(sysdate,'y')||to_char(sysdate,'ww')||to_char(sysdate,'d') ymd   from dual";
                        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            strymd = dt1.Rows[0]["ymd"].ToString();
                            temp = strPre + strymd;
                        }
                    }
                }
                else
                {
                    temp = strPre;
                }
                if (ckb1.Checked)
                {
                    switch (rbl2.SelectedIndex)
                    {
                        case 0:
                            strModelPre = "L";
                            break;
                        case 1:
                            strModelPre = "F";
                            break;
                        case 2:
                            strModelPre = "E";
                            break;
                        case 3:
                            strModelPre = "X";
                            break;
                    }
                }

                if (strPart.Substring(2, 3).Equals("DUT") || strPart.Substring(2, 3).Equals("DUA") || strPart.Substring(2, 3).Equals("DUO") || strPart.Substring(2, 3).Equals("BLZ") || strPart.Substring(2, 3).Equals("BEZ"))
                    strModelPre += temp;
                else
                    strModelPre += strPre;
            }
        }
        else
        {
            strModelPre = "M";
            strModelPre += strPre;
        }
        try
        {
            string strSql = "INSERT INTO shp.CMCS_SFC_SORDER(sorder,model,spart,prd_type,sequence_id,pid_qty,bom_ver,creation_date) values('" +
                             strWO + "','" + strPart.Substring(2, 3) + "','" + strPart + "','" + strPart.Substring(2, 3) + strModelPre + "','" + Convert.ToChar(rbl1.SelectedIndex + 1) + "','" + strQty + "','" + strVersion + "',sysdate)";
            ClsGlobal.objDataConnect.DataExecute(strSql);
            txtWO.Text = "";
            txtPart.Text = "";
            txtVer.Text = "";
            txtQty.Text = "";
            this.txtWO.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('工單創建成功！！');</script>");
            return;
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('插入語句執行有bug！！');</script>");
            return;
        }
    }

    protected void dgSorder_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='blue'");

        if (e.Item.ItemType == ListItemType.Item)
        {
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");
        }

        if (e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='seashell'");
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        //HTMLDocument document = (HTMLDocument)axWebBrowser1.Document;
        //document.focus();
        //SendKeys.Send("^f");
    }

    protected void ButtonFind_Click(object sender, EventArgs e)
    {
        string strsql = "select * from SHP.CMCS_SFC_SORDER where SORDER LIKE '" + this.TextBox1.Text.Trim() + "%' order by creation_date desc ";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgSorder.DataSource = dt.DefaultView;
            dgSorder.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SHP.CMCS_SFC_SORDER表沒有數據！！');</script>");
            return;
        }
    }
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < this.dgSorder.Items.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)dgSorder.Items[i].FindControl("CheckBox1");
            if (chkSelect.Checked)
            {
                string sname = dgSorder.Items[i].Cells[1].Text;
                string sms_sqlstr = "delete from shp.cmcs_sfc_sorder where sorder='" + sname + "'";

                try
                {
                    //ClsGlobal.objDataConnect.DataExecute(strsql);
                    Response.Write("<script>alert('刪除成功!')</script>;");

                }
                catch
                {
                    Response.Write("<script>alert('刪除失敗!')</script>;");
                }

            }
        }
        Binddata();

    }
    protected void dgSorder_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            //鼠标经过时，行背景色变 
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
            //鼠标移出时，行背景色变 
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            //如果是绑定数据行 
        }

    }
}

