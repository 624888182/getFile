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


public partial class MainMSPrg_DeletePage : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbRead;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string param;
    private static string tablename;
    private static string pocnt;
    private static string tparam;
    private static string tprivate;
    private static int count;
    int editRow = -1;
    bool isEdit = false;
    private static DataTable dt = new DataTable();
    BBSCMlib BBSCMlibPointer = new BBSCMlib();
    static string type;
    static string dnid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                conntype = Session["Param2"].ToString();
                Conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                //Autoprg = Session["Param5"].ToString();
                //tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
            }
            else if (tmp1 == "")
            {
                conntype = "sql";
                Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            dbo = new PDataBaseOperation(conntype, Conn);
            type = Request.QueryString["type"];
            dnid = Request.QueryString["DNID"];
            Label1.Text = type + " Delete";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string mes = TextBox1.Text.ToString().Trim();
        if (mes == "")
        {
            //Response.Write("<script>alert('Please enter the reason!');</script>");
            Label2.Text = "Please enter the reason!";
            return;
        }
        string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //獲取客戶端IP
        HttpRequest request = HttpContext.Current.Request;
        string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(result))
        {
            result = request.ServerVariables["REMOTE_ADDR"];
        }
        if (string.IsNullOrEmpty(result))
        {
            result = request.UserHostAddress;
        }
        if (string.IsNullOrEmpty(result))
        {
            result = "0.0.0.0";
        }
        if (type == "940")
        {
            string check3B2 = "select * from  " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT]  where DNID='" + dnid + "'";
            DataTable dtcheck3B2 = dbo.PSelectSQLDT(check3B2);
            if (dtcheck3B2.Rows.Count > 0)
            {
                string del3B2 = "update " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] set CONFIRMFLAG='D' where DNID='" + dnid + "'";
                int idet = dbo.PExecSQLTran(del3B2);
                if (idet == 1)
                {
                    string delete = "update " + BBSCMDIR + ".[dbo].[DN_Header] set Create_flag='D',uf3='" + date + "/" + result + "',uf4='" + mes + "' where w0502='" + dnid + "'";
                    int idet1 = dbo.PExecSQLTran(delete);
                    if (idet1 >= 1)
                    {
                        Response.Write("<script>alert('DNID " + dnid + " Delete OK!!')</script>");
                        Response.Write("<script language='javascript'>window.returnVal='1';window.close();</script>");
                    }
                    else
                    {
                        Label2.Text = "Delete Exception,Please try again!";
                    }
                }
                else
                {
                    Label2.Text = "Delete Exception,Please try again!";
                }
            }
            else
            {
                string delete = "update " + BBSCMDIR + ".[dbo].[DN_Header] set Create_flag='D',uf3='" + date + "/" + result + "',uf4='" + mes + "' where w0502='" + dnid + "'";
                int idet1 = dbo.PExecSQLTran(delete);
                if (idet1 >= 1)
                {
                    Response.Write("<script>alert('DNID " + dnid + " Delete OK!!')</script>");
                    Response.Write("<script language='javascript'>window.returnVal='1';window.close();</script>");
                }
                else
                {
                    Label2.Text = "Delete Exception,Please try again!";
                }
            }
        }
        else if (type == "204")
        {
            string del204 = "update " + BBSCMDIR + ".[dbo].[EDI204HEADER] set CONFIRMFLAG='D',uf1='" + date + "/" + result + "',uf2='" + mes + "' where B204='" + dnid + "'";
            int idet2 = dbo.PExecSQLTran(del204);
            if (idet2 >= 1)
            {
                Response.Write("<script>alert('LoadID " + dnid + " Delete OK!!')</script>");
                Response.Write("<script language='javascript'>window.returnVal='1';window.close();</script>");
                Label2.Text = "";
            }
            else
            {
                Label2.Text = "Delete Exception,Please try again!";
            }
        }
        else if (type == "3A4")
        {
            string del3A4MT = "update " + BBSCMDIR + ".[dbo].[PO_CREATE_MT] set CONFIRMFLAG='D',PO_CREATE_MT_UF9='" + date + "/" + result + "',PO_CREATE_MT_UF10='" + mes + "' where POID='" + dnid + "'";
            int idet3 = dbo.PExecSQLTran(del3A4MT);
            if (idet3 >= 1)
            {
                string del3A4DT = "update " + BBSCMDIR + ".[dbo].[PO_CREATE_DT] set CONFIRMFLAG='D' where POID='" + dnid + "'";
                int idet4 = dbo.PExecSQLTran(del3A4DT);
                if (idet4 >= 1)
                {
                    Response.Write("<script>alert('Cancel OK!!')</script>");
                    Response.Write("<script language='javascript'>window.returnVal='1';window.close();</script>");
                    Label2.Text = "";
                }
                else
                {
                    Response.Write("<script>alert('Update PO_CREATE_DT Error!')</script>");
                    Label2.Text = "Delete Exception,Please try again!";
                }
            }
            else
            {
                Response.Write("<script>alert('Update PO_CREATE_MT Error!')</script>");
                Label2.Text = "Delete Exception,Please try again!";
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>window.returnVal='0';window.close();</script>");
    }
}
