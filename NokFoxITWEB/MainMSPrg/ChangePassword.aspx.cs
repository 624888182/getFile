using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SFC.TJWEB;
using System.Drawing;
public partial class MainMSPrg_ChangePassword : System.Web.UI.Page
{
    private static string connType;
    private static string conn;
    private static string dbRead;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string userID;
    private static string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 数据库连接
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                connType = Session["Param2"].ToString();
                conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
                userID = Session["userid"].ToString();
                userName = Session["username"].ToString();   

            }
            else if (tmp1 == "")
            {
                connType = "sql";
                conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                //conn = "Server=10.83.18.93 ;User id=IMSCM;Pwd=Foxconn88;Database=IMSCM";//93
                //dbWrite = "Server=10.83.18.93 ;User id=IMSCM;Pwd=Foxconn88;Database=IMSCM";//93
                BBSCMDIR = "IMSCM";
                userID = "User";
                userName = "User";
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            #endregion

            lblUser.Text = userID;
            lblUserName.Text = userName;
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string strUseID = lblUser.Text.ToString().Trim();    
        string strOldPassword = txtOldPassword.Text.ToString().Trim();
        string strNewPassword = txtNewPassword.Text.ToString().Trim();
        string strConNewPassword = txtConNewPassword.Text.ToString().Trim();

        string strSel = "select * from " + BBSCMDIR + ".[dbo].[Tbl_user] where userid='" + strUseID + "'";
        DataTable dt = PDataBaseOperation.PSelectSQLDT(connType, conn, strSel);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string oldPassword = dt.Rows[i]["ps"].ToString().Trim();
            if (oldPassword == strOldPassword)
            {
                if (strNewPassword == strConNewPassword)
                {
                    string strUpdate = "update " + BBSCMDIR + ".[dbo].[Tbl_user] set ps='" + strNewPassword + "' where userid='" + strUseID + "'";
                    int idetCount = PDataBaseOperation.PExecSQL(connType, conn, strUpdate);
                    if (idetCount == 1)
                    {
                        Response.Write("<script>alert('Update OK!')</script>");
                        break;
                    }
                }
                else
                {
                    Response.Write("<script>alert('The new password and confirm password do not match !')</script>");
                    break;
                }
            }
            else
            {
                Response.Write("<script>alert('Old Password Error!')</script>");
                break;
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtConNewPassword.Text = "";
        txtNewPassword.Text = "";
        txtOldPassword.Text = "";
    }
}