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

public partial class Boundary_wfrmIMEIChangePorder : System.Web.UI.UserControl
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
        lbPorder.Text = (String)GetGlobalResourceObject("SFCQuery", "Porder");
        lbIMEI.Text = (String)GetGlobalResourceObject("SFCQuery", "IMEI");
        btnchange.Text = (String)GetGlobalResourceObject("SFCQuery", "ChangePorder"); 
    }

    protected void btnchange_Click(object sender, EventArgs e)
    {
        bool blSJUG = ckbSJUG.Checked;
        bool blPart = ckbPPart.Checked;
        string strPorder = txtPorder.Text.Trim().ToUpper();
        string strImei = txtIMEI.Text.Trim().ToUpper();
        string strorder;
        string strorder1;
        string strpart;
        string strpart1;
        string strsjug;
        string strsjug1;
        if (strPorder.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入你要轉入的P工單！！');</script>");
            return;

        }
        else
        {
            string strsql = "SELECT PORDER,PPART,PHONE_MODEL SJUG FROM SHP.CMCS_SFC_PORDER Where PORDER ='" + strPorder + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            string strsql1;
            if (dt.Rows.Count > 0)
            {
                strorder = dt.Rows[0]["PORDER"].ToString();
                strpart = dt.Rows[0]["PPART"].ToString();
                strsjug = dt.Rows[0]["SJUG"].ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('工單錯誤,請重新輸入！！');</script>");
                return;
            }

            if (strImei.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI不能為空！！');</script>");
                return;
            }
            else
            {
               strsql1="SELECT PORDER FROM SHP.ZJJ_CMCS_SFC_IMEINUM Where IMEINUM LIKE '"+strImei+"%'";
               DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
               if (dt1.Rows.Count > 0)
               {
                   strorder1 = dt1.Rows[0]["PORDER"].ToString();
                   string strsql2 = "SELECT PORDER,PPART,PHONE_MODEL SJUG FROM SHP.CMCS_SFC_PORDER Where PORDER ='" + strorder1 + "'";
                   DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
                   if (dt2.Rows.Count > 0)
                   {
                       strorder1 = dt2.Rows[0]["PORDER"].ToString();
                       strpart1 = dt2.Rows[0]["PPART"].ToString();
                       strsjug1 = dt2.Rows[0]["SJUG"].ToString();

                       if (blPart)
                       {
                           if (blSJUG)
                           {
                               if (strorder1 == strorder)
                               {
                                   Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('同一個工單,無需再轉！');</script>");
                                   return;
                               }
                               else
                               {
                                   if (strsjug.Substring(0, 8).ToUpper() == strsjug1.Substring(0, 8).ToUpper()&&strpart1==strpart)
                                   {
                                       string strSql = "update SHP.ZJJ_CMCS_SFC_IMEINUM set SHIFT_PORDER=PORDER,PORDER='" + strPorder + "' Where IMEINUM LIKE '" + strImei + "%'";
                                       ClsGlobal.objDataConnect.DataExecute(strSql);
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('轉入成功！');</script>");
                                       return;
                                   }
                                   else
                                   {
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SJUG或料號不一致,轉工單失敗!！');</script>");
                                       return;
                                   }
                               }
                           }
                           else
                           {
                               if (strorder1 == strorder)
                               {
                                   Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('同一個工單,無需再轉！');</script>");
                                   return;
                               }
                               else
                               {
                                   if (strpart1 == strpart)
                                   {
                                       string strSql = "update SHP.ZJJ_CMCS_SFC_IMEINUM set SHIFT_PORDER=PORDER,PORDER='" + strPorder + "' Where IMEINUM LIKE '" + strImei + "%'";
                                       ClsGlobal.objDataConnect.DataExecute(strSql);
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('轉入成功！');</script>");
                                       return;
                                   }
                                   else
                                   {
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('料號不一致,轉工單失敗!！');</script>");
                                       return;
                                   }
                               }
                           }
                       }
                       else
                       {
                           if (blSJUG)
                           {
                               if (strorder1 == strorder)
                               {
                                   Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('同一個工單,無需再轉！');</script>");
                                   return;
                               }
                               else
                               {
                                   if (strsjug.Substring(0, 8).ToUpper() == strsjug1.Substring(0, 8).ToUpper())
                                   {
                                       string strSql = "update SHP.ZJJ_CMCS_SFC_IMEINUM set SHIFT_PORDER=PORDER,PORDER='" + strPorder + "',PPART='" + strpart + "'  Where IMEINUM LIKE '" + strImei + "%'";
                                       ClsGlobal.objDataConnect.DataExecute(strSql);
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('轉入成功！');</script>");
                                       return;
                                   }
                                   else
                                   {
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SJUG不一致,轉工單失敗!！');</script>");
                                       return;
                                   }
                               }
                           }
                           else
                           {
                               if (strorder1 == strorder)
                               {
                                   Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('同一個工單,無需再轉！');</script>");
                                   return;
                               }
                               else
                               {
                                   if (strpart.Substring(2, 3) == strpart1.Substring(2, 3))
                                   {
                                       string strSql = "update SHP.ZJJ_CMCS_SFC_IMEINUM set SHIFT_PORDER=PORDER,PORDER='" + strPorder + "',PPART='" + strpart + "'  Where IMEINUM LIKE '" + strImei + "%'";
                                       ClsGlobal.objDataConnect.DataExecute(strSql);
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('轉入成功！');</script>");
                                       return;
                                   }
                                   else
                                   {
                                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('幾種不一致,轉入失敗！');</script>");
                                       return;
                                   }
                               }
                           }
                       }
                   }
                   else
                   {
                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert(' ???！！');</script>");
                       return;
                   }
               }
               else
               {
                   Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該IMEI不存在！！');</script>");
                   return;
               }
            }
        }
    }
}
