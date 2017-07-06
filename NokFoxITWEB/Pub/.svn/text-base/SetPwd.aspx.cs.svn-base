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
using System.Data.OracleClient;
using SCM.GSCMDKen;
using System.Collections.Generic;
using System.Linq;


public partial class SetPwd : System.Web.UI.Page
{
    FPubLib FPubLibPointer = new FPubLib();
    string login_no = "";
    string login_name = "";
    static string web = "";
    public static string DBRaed = "", DBWri = "", DBtype = "", OraDBWri = "", OraDBtype = "";
    public string Supertype = "1"; // 1 Standard, "S" SupervisorUsets

    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack){

          web = Session["Param1"].ToString();

          string p1 = Session["Param1"].ToString();
          string p2 = Session["Param2"].ToString();
          string p3 = Session["Param3"].ToString();
          string p4 = Session["Param4"].ToString();
          string p5 = Session["Param5"].ToString();
          string p6 = Session["Param6"].ToString(); // Supervisor Login 
          string p7 = Session["Param7"].ToString(); // ITSystem 
          string p8 = Session["Param8"].ToString(); // oracle 
          string p9 = Session["Param9"].ToString(); // Oracle Server  L10 Server 


          DBtype = p2; 
          DBRaed = p3;
          DBWri  = p4;
          OraDBtype = p8;
          OraDBWri  = p9;
          TextBox2.Text = p7;          
          this.user_id.Text = "";
          this.user_pwd.Text = "";
          TextBox1.Text = "";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user_id_v = this.user_id.Text;
        string aa = user_id.Text;
        string user_pwd_v = this.user_pwd.Text;
        if (user_id_v.Equals(""))
        {
            RegisterClientScriptBlock("0", "<script>alert('請輸入工號！')</script>");
            this.user_id.Focus();
            return;
        }
        if(user_pwd_v.Equals("")){
            RegisterClientScriptBlock("0", "<script>alert('請輸入密碼！')</script>");
            this.user_pwd.Focus();
            return;
        }
        if (!checkLoginUser(user_id_v, user_pwd_v, "1"))
        {
            RegisterClientScriptBlock("0", "<script>alert('工號或密碼不正確，請重新輸入！')</script>");
            this.user_id.Focus();
            return;
        }
        else
        {
          //  Response.Redirect(web+"?delflag=true");
           //Response.Write("<script>window.close();</script>");
    
        }
    }

    private bool checkLoginUser(string user_id, string password, string functype)
    {
        bool isOk = false;
        Button5.Text = "須輸入Admin及密碼";
      //  TextBox4.Text.S
        string t1 = FPubLibPointer.ChkUsrPSExist(TextBox4.Text, TextBox5.Text, DBRaed);
        if (t1 == "-1") return isOk;
        else Supertype = "S";

        string tfunc = functype;
        isOk = true;
        string sp = FPubLibPointer.UsrPSFunc(this.user_id.Text, this.user_pwd.Text, TextBox2.Text, TextBox1.Text, DBRaed, tfunc, "sql", Supertype, TextBox6.Text, OraDBtype, OraDBWri );
        
        if ( sp == "-1" ) isOk = false;

        return isOk;
                
       
    }

    private DataSet queryByDefinedSql(string sqlstr)
    {
        OracleDataAdapter ada = new OracleDataAdapter();
        DataSet dsPos = new DataSet();
        OracleConnection con = new OracleConnection(ConfigurationManager.AppSettings["dbconnstr"]);
        OracleCommand com = new OracleCommand(sqlstr, con);
        com.CommandType = CommandType.Text;
        ada.SelectCommand = com;
        con.Open();
        ada.Fill(dsPos, "allUser");
        con.Close();
        return dsPos;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<SCRIPT   LANGUAGE='JavaScript'>window.opener=null;window.close();</SCRIPT> "); // close system
        //string web = Request.QueryString["web"].ToString();
        //Response.Redirect(web);
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)  // Modify
    {
        string t1 = ChkSuperPW(TextBox4.Text, TextBox5.Text, DBRaed);
        if (TextBox1.Text != TextBox3.Text) return;
        if (TextBox1.Text == "") return;
        string sp = FPubLibPointer.UsrPSFunc(this.user_id.Text, this.user_pwd.Text, TextBox2.Text, TextBox1.Text, DBRaed, "2", "sql", t1, TextBox6.Text,  OraDBtype, OraDBWri );
        if (sp == "-1")
        {
            Response.Write("<script>alert('Open Users Fail ! Pls Entry Again ! ')</script>");
            return;
        }
        else
        {
            Response.Write("<script>alert('Operation Succesd ! ')</script>");
            return;            
        }
      
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string t1 = ChkSuperPW(TextBox4.Text, TextBox5.Text, DBRaed);
        string sp = FPubLibPointer.UsrPSFunc(this.user_id.Text, this.user_pwd.Text, TextBox2.Text, TextBox1.Text, DBRaed, "3", "sql", t1, TextBox6.Text, "", ""); // Delete

    }
    protected void ok_Click(object sender, EventArgs e)
    {
        string t1 = ChkSuperPW(this.user_id.Text, this.user_pwd.Text, DBRaed);
        if (t1 == "-1") Button5.Text = "密碼錯誤";
        else Button5.Text = "密碼正確";
    }


    private string ChkSuperPW( string pUser, string pPst2, string pDRStr )
    {
        string t1 = FPubLibPointer.ChkUsrPSExist( pUser, pPst2, pDRStr);
        if (t1 == "-1") return("-1"); // False Users 

        if ( ( pUser.ToLower() != "admin" ) && ( pUser.ToLower() != "supervosor" )) return("1"); // Normal Users 

        string p6 = Session["Param6"].ToString();
        if (p6.ToLower() != "supervisor") return ("1");  // Not Get Para from Web

        Supertype = "s";
        return("s");
    }

}
