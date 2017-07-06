using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
public partial class I_HubLogin : System.Web.UI.Page
{
     static string strconn = "";
    //string strconn = "Server=10.148.8.168 ;User id=FIH_IHUB_WEB;Pwd=FIH_IHUB_WEB!;Database=NOKIA_IOF";
    static string DBType = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            strconn = ConfigurationManager.AppSettings["BBSCM"]; 

              
            Session["Param1"] = 1;
            Session["Param2"] = "SQL";    //SQL Server
            Session["Param3"] = strconn;  //SQL Server
            Session["Param4"] = strconn;  //SQL Server
           // Response.Write("<script>window.open( 'MainNokPrg/I_HubLogin.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");



            if (Session["Param1"].ToString() == "1")
            {
                DBType = Session["Param2"].ToString();
                strconn = Session["Param3"].ToString();
            }
            else
            {
                Response.Write("<script>alert('您所傳遞的字符串地址不正確，請重新傳遞!')</script>");
                return;
            }
            DropDownList1.Items.Add(new ListItem("Beijing", "BJ"));
            DropDownList1.Items.Add(new ListItem("Langfang", "LF"));
            DropDownList1.Items.Add(new ListItem("Longhua", "LH"));
            DropDownList1.Items.Add(new ListItem("Chihuahua", "CU"));
            DropDownList1.Items.Add(new ListItem("Hungary", "HU"));
            DropDownList1.Items.Add(new ListItem("Reynosa", "RN"));
            DropDownList1.Items.Add(new ListItem("Hongkong", "HK"));
            DropDownList1.SelectedValue = "BJ";
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string UserName = TextBox1.Text;
        string PasswordHash = TextBox2.Text;
        string Region = DropDownList1.SelectedValue;
        string IsAdmin = string.Empty;
        string strDuns = string.Empty;
        string strBU = string.Empty;
        string strSql = string.Empty;
        strSql = "Select IsAdmin,IhubDuns,BU From siUser Where UserName = '" + UserName + "' and PasswordHash = '" + PasswordHash + "' and Region = '" + Region + "'";
        DataTable dt = DataBaseOperation.SelectSQLDT(DBType, strconn, strSql);
        if (dt.Rows.Count == 1)
        {
            if (dt.Rows[0][0].ToString() != "" && dt.Rows[0][1].ToString() != "" && dt.Rows[0][2].ToString() != "")
            {
                IsAdmin = dt.Rows[0][0].ToString();
                strDuns = dt.Rows[0][1].ToString();
                strBU = dt.Rows[0][2].ToString();
                Session["UserName"] = UserName;
                Session["Region"] = Region;
                Session["IsAdmin"] = IsAdmin;
                Session["strDuns"] = strDuns;
                Session["strBU"] = strBU;
                Session["strconn"]=strconn;
                Session["dbtype"]=DBType;
                //Response.Write("<script>window.open( 'Nokiaihub3c7.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
                Server.Transfer("Nokiaihub3c7.aspx");

            }
        }
        else
        {
            Response.Write("<script>alert('Sorry!Incorrect UserName or PassWord,Please Enter Again!')</script>");
            return;
        }
        

    }

}
