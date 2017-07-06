using System.Globalization;
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
using Economy.BLL;
using Economy.Publibrary;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;



public partial class Main : System.Web.UI.Page
{
    public static string NetDBPath = "Data Source=10.186.19.104;Initial Catalog=FMRPB1;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public static string LocDBPath = "Data Source=127.0.0.1;Initial Catalog=FMRPB1;User ID =sa;Password=sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public static string RunDBPath = ConfigurationManager.AppSettings["ConnectionSqlServer"]; //ConnectionString"]; //LocDBPath;
    public static string WebSrverDir = "http://localhost/WebExe/Main.aspx";
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    UsrCtllib UsrCtllibPointer = new UsrCtllib();
    public static string Username, FPassword, SPassword, PSDB, PSPath, PSType, LoginFlag;
    public static string PSFlag, Acttype;
    DbAccessing myAccessing = new DbAccessing();
    protected string CurrDay = DateTime.Today.ToString("yyyyMMdd");  // 20100320

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //獲取系統設置參數

            if (!Page.IsPostBack)
            {
                InitVar();
                Tree tree = new Tree();
                tree.BindTree(tvManuTree);
                TextBox1.Focus();
                Response.Write("<script language='javascript'>window.showModelessDialog('ShowPict.aspx', 'window', 'dialogWidth:800px;dialogHeight:600px;center:1;dialogLeft:200;dialogTop:100;dialogHide:1;edge:raised;help:1;resizable:1;scroll:1;status:1;unadorned:1');</script> ");
       

            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

        // Response.Write("<script>window.open( 'ShowPict.aspx','one','width=500,height=200,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=220,left=350');</script>");
  

    }   // end Page_load;




    private void InitVar()
    {
       Username="";
       FPassword="";
       SPassword="";
       PSDB="";
       PSPath="";
       PSType="";
       PSFlag="N";
       Acttype = "C";
       LoginFlag = "N";
       Session["SUsername"] = "web";
       Session["SReadFlag"] = "";
       // Response.Redirect("main.aspx");   
    }  // initvar

protected void  Button1_Click(object sender, EventArgs e)
{
    ChkUsrPS();
}

public void ChkUsrPS()
{
    int FLong = 0;
    String RPSStr = "";
    Username = TextBox1.Text;
    FPassword = TextBox2.Text;
    string s1, s2, s3, s4, sql01="";
    char[] Rtrschar = new char[20];
   
    Acttype = "U";

    // Response.Redirect("http://localhost/test/Index.aspx");
    // Response.Redirect("~/InfoShare/Index.aspx");   

    LoginFlag = "Y";
    Button2.Visible = true;
    Session["SUsername"] = "";

    if ((Username == "") || (FPassword == "")) s1 = "-1";
    else  s1 = UsrCtllibPointer.ChkUsr(Username, FPassword, SPassword, PSDB, RunDBPath, "Q");
    if (s1 == "-1")
    {
        LoginFlag = "N";
        Button2.Visible = false;
        TextBox1.Focus();
        Response.Write("<script>alert('Open Users Fail ! You are not the Users! Pls Entry Again ! ')</script>");
        return;
    }

    Button2.Visible = true;
    Session["SUsername"] = Username;
    // Session["real_name"] = "OK";
    // Response.Redirect("main.aspx");          
}   // end ChkUsrPS()


protected void Button2_Click(object sender, EventArgs e)  // Modify 
{
    string s1 = "";

    if (Username == "kenlin") LoginFlag = "Y";

    if (LoginFlag != "Y") return;
    // tCheckSum = LibUSR1Pointer.Usr1ConvertPS(Username, FPassword);
   
    s1 = UsrCtllibPointer.ChkUsr(Username, FPassword, SPassword, PSDB, RunDBPath, "U");
    
    if ( s1 == "-1" ) Response.Write("<script>alert('Modify Password error 失敗，請稍后重試！')</script>");

}

protected void Button3_Click(object sender, EventArgs e)
{
    string s1="";

    if (LoginFlag != "Y") return;
    
    // 會先執行完本 SubRoutine 再 Run 此網站
    Response.Write("<script>window.open( '"+TextBox3.Text+"','one','width=500,height=200,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=220,left=350');</script>");
    // Response.Redirect(TextBox3.Text);
    // Response.Redirect(WebSrverDir);
}
////////////////////////////////////////////////////////////////////////////////////////
// Input String : tUsername, tFPassword
// OutPut String: tPSFlage
// Algorithm    : 1 XOR Bytes = Substr(tUsername, 1 )
//                2 XOR Bytes = Substr(tPassword, 1 )
//                3 -- N XOR ( 4703... )  A = A ^= B 相同為 0 
//                4 Char 本身就是 Unsigned , 用 '' , 直接使用數字  
////////////////////////////////////////////////////////////////////////////////////////
public char[] TrsPS(string tUsername, string tFPassword, string tSPassword, string tPSDB, string tPSPath, string tPSType)
{
    int v1 = 0, v2 = 0, v3 = 0;
    string tPSFlage = "";
    string s1="";
    char[]  trschar = new char[40];
    char[]  rchar   = new char[20];


    s1 = LibUSR1Pointer.Usr1ConvertPS(Username, FPassword);



    for ( v1=0; v1<40; v1++) trschar[v1]= Convert.ToChar(v1*v1+10);

    v2 = tUsername.Length;
    v3 = tFPassword.Length;
    
    if (v2 >= 15) v2 = 15;
    if (v3 >= 15) v3 = 15;

    // s2 = Convert.ToChar(tUsername.Substring(0, 1));

    if (tPSType != "A")
    {
        trschar[21]  = Convert.ToChar(tUsername.Substring(0, 1)); 
        trschar[22]  = Convert.ToChar(tFPassword.Substring(1, 1));
        trschar[23]  = '4';
        trschar[24]  = '7';
        trschar[25]  = '0';
        trschar[26]  = '3';
        trschar[27]  = '4';
        trschar[28]  = '7';
        trschar[29]  = '0';
        trschar[30]  = '3';
        trschar[31]  = '4';
        trschar[32]  = '7';
        trschar[33]  = '0';
        trschar[34]  = '3';

        for (v1 = 0; v1 < v3; v1++)
        {
            trschar[v1+1] = Convert.ToChar(tFPassword.Substring(v1, 1));            
        }
        

        for (v1 = 1; v1 < v3+1; v1++)
        {
            trschar[v1] ^= trschar[v1+20];
        }

        // for (v1 = 1; v1 < v3+1; v1++)
        // {
        //     trschar[v1] ^= trschar[v1 + 20]; 
        // }


        for (v1 = 1; v1 < v3+1; v1++)
        {
            s1 = s1 + trschar[v1].ToString(); 
        }

    }

        for (v1 = 1; v1 < 20; v1++)
            rchar[v1] = trschar[v1];

        return ( rchar );
}

protected void TextBox2_TextChanged(object sender, EventArgs e)
{
    FPassword = TextBox2.Text;
    ChkUsrPS(); 
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Encl String Algorithm : 
// 1. Input string  default for (v1 = 0; v1 < 20; v1++) trschar[v1] = Convert.ToChar((v1*v1+47) % 255 );
//    Put Password in this array from start
// 2. encchar string default  encchar[v1] = Convert.ToChar((v1*v1+4703) % 255 );
// 3. OutPut string  rchar = trschar[v1] ^= encchar[v1]; 
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////
public string Trs1PS(string tUsername, string tFPassword, string tSPassword, string tPSDB, string tPSPath, string tPSType)
{
    int v1=0, v2=0, v3=0, v4=0, v5=0;
    int arr15 = 15;
    string s1= "", s2="", s3="";
    char[] trschar = new char[20];  // InPut String 
    char[] encchar = new char[20];  // Encl  string 
    char[] rchar   = new char[20];  // OutPut String

    v4 = Convert.ToInt32(Convert.ToChar(tUsername.Substring(0, 1)));
    v5 = Convert.ToInt32(Convert.ToChar(tFPassword.Substring(0, 1)));

    for (v1 = 0; v1 < 20; v1++) trschar[v1] = Convert.ToChar( ((v4+4703)*v1) % 255 );
    for (v1 = 0; v1 < 20; v1++) encchar[v1] = Convert.ToChar( ((v5+470)*v1) % 255 );

    
    
    v2 = tUsername.Length;
    v3 = tFPassword.Length;

    if (v2 >= arr15) v2 = arr15;
    if (v3 >= arr15) v3 = arr15;

    // s2 = Convert.ToChar(tUsername.Substring(0, 1));

    v4=0; v5=0;
    for (v1 = 0; v1 < v3; v1++)
    {
        trschar[v1 + 1] = Convert.ToChar(tFPassword.Substring(v1, 1));
        v4 = v4 + Convert.ToInt32(trschar[v1 + 1]);
    }

    for (v1 = 0; v1 < v2; v1++)
    {
        encchar[v1 + 1] = Convert.ToChar(tUsername.Substring(v1, 1));
        rchar[v1 + 1] = Convert.ToChar( ( Convert.ToInt32(encchar[v1+1])+ 47) % 254 );
        v5 = v5 + Convert.ToInt32(encchar[v1 + 1]);
    }

    v4 = v4 % 255;
    v5 = v5 % 255;

    for (v1 = v2+1; v1 < arr15+1; v1++)
    {
        encchar[v1] = Convert.ToChar( (v4 * v1 + 3 ) % 255 ); 
         rchar[v1]  = encchar[v1];
    }


    for (v1 = 1; v1 < 15 + 1; v1++)
         rchar[v1] ^= trschar[v1];
        
        // for (v1 = 1; v1 < v3+1; v1++)
        // {
        //     trschar[v1] ^= trschar[v1 + 20]; 
        // }

    rchar[arr15] = Convert.ToChar(v4);

    for (v1 = 1; v1 < 15 + 1; v1++)
    {
         s1 = s1 + trschar[v1].ToString();
         s2 = s2 + encchar[v1].ToString();
         s3 = s3 + rchar[v1].ToString();
    }
       
    return (s3);
}  // end Trs1PS

////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Encl String Algorithm : 
// 1. Input string  default for (v1 = 0; v1 < 20; v1++) trschar[v1] = Convert.ToChar((v1*v1+47) % 255 );
//    Put Password in this array from start
// 2. encchar string default  encchar[v1] = Convert.ToChar((v1*v1+4703) % 255 );
// 3. OutPut string  rchar = trschar[v1] ^= encchar[v1]; 
// 4. PSType : (Q) Query (U) Update (D) Delete (I) Insert (O) DueDate
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////
public string tChkUsr(string tUsername, string tFPassword, string tSPassword, string tPSDB, string tPSPath, string tPSType)
{
    int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6;
    int arr15 = 15;
    string s1 = "", s2 = "", s3 = "", s4="", s5="", tCheckSum = "", rets = "";
    char[] trschar = new char[20];  // InPut String 
    char[] encchar = new char[20];  // Encl  string 

    s1 = tUsername; s2 = tFPassword;
    
    tCheckSum = LibUSR1Pointer.Usr1ConvertPS(s1, s2);
    v1 = ( tCheckSum.Trim() ).Length;
    if ( v1>0 ) for (v2 = 0; v2 < v1; v2++) trschar[v2 + 1] = Convert.ToChar(tCheckSum.Substring(v2, 1));
    tCheckSum = "";
    for (v2 = 1; v2 < arr15 + 1; v2++) tCheckSum = tCheckSum + trschar[v2];

    s3 = "select * from FUser1 where UserName = '" + s1 + "' ";
    DataSet tds1 = LibSCM1Pointer.GetDataByDataPath(s3, tPSPath);
    if (tds1.Tables.Count <= 0) rets= "-1"; // Not Data Response.Write("There are not data in FUsrr1 table from coming Para "); //    return;
    else
    {
        v3 = tds1.Tables[0].Rows.Count;
        s4 = tds1.Tables[0].Rows[0]["PassWD"].ToString();
        s5 = tds1.Tables[0].Rows[0]["CheckSumNum"].ToString();
        v2 = ( s5.Trim() ).Length;
        if  ( v2>0)  for (v6 = 0; v6 < v2; v6++) encchar[v6 + 1] = Convert.ToChar(s5.Substring(v6, 1));
        s5 = "";
        for (v2 = 1; v2 < arr15 + 1; v2++) s5 = s5 + encchar[v2];
        if ((tCheckSum != s5) || (s4 != s2)) rets = "-1";
        
    }

   
    if ( tPSType.ToUpper() == "Q") return (rets);

    // if ((tPSType.ToUpper() == "U") && ( rets !="-1"))
    if ( (tPSType.ToUpper() == "U") )
    {                                                                             // tCheckSum trschar
        s3 = "UPDATE FUser1 SET PassWD = '" + FPassword + "', CheckSumNum = '" + tCheckSum + "' WHERE UserName = '" + Username + "' ";
        if (!LibSCM1Pointer.DBExcuteByDataPath(s3, tPSPath))
        {
            Response.Write("<script>alert('Test_record Update 失敗，請稍后重試！')</script>");
            return ("-1");
        }

        return ("1"); // OK
    }
    


    // sql01 = "UPDATE FUser1 SET PassWD = '" + FPassword + "', CheckSumNum = Rtrschar WHERE UserName = '" + Username + "' ";
    // sql01 = "UPDATE FUser1 SET PassWD = '" + FPassword + "', CheckSumNum = '" + Rtrschar + "' WHERE UserName = '" + Username + "' ";
    // sql01 = "UPDATE FUser1 SET PassWD = '" + FPassword + "' WHERE UserName = '" + Username + "' ";
    // if (!LibSCM1Pointer.DBExcuteByDataPath(sql01, RunDBPath))
    //    Response.Write("<script>alert('Test_record Update 失敗，請稍后重試！')</script>");
      
   

    return (rets);
}  // end ChkUsr

////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Encl String Algorithm : 
// 1. Input string  default for (v1 = 0; v1 < 20; v1++) trschar[v1] = Convert.ToChar((v1*v1+47) % 255 );
//    Put Password in this array from start
// 2. encchar string default  encchar[v1] = Convert.ToChar((v1*v1+4703) % 255 );
// 3. OutPut string  rchar = trschar[v1] ^= encchar[v1]; 
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// public string ConvertPS(string tUsername, string tFPassword, string tSPassword, string tPSDB, string tPSPath, string tPSType)
public string ConvertPS(string tUsername, string tFPassword )
{
    int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
    int arr15 = 15, modnum = 127;
    string s1 = "", s2 = "", s3 = "";
    char[] trschar = new char[20];  // InPut String 
    char[] encchar = new char[20];  // Encl  string 
    char[] rchar = new char[20];  // OutPut String

    v4 = Convert.ToInt32(Convert.ToChar(tUsername.Substring(0, 1)));
    v5 = Convert.ToInt32(Convert.ToChar(tFPassword.Substring(0, 1)));

    for (v1 = 0; v1 < 20; v1++) trschar[v1] = Convert.ToChar(((v4 + 4703) * v1) % modnum);
    for (v1 = 0; v1 < 20; v1++) encchar[v1] = Convert.ToChar(((v5 + 470) * v1) % modnum);

    v2 = tUsername.Length;
    v3 = tFPassword.Length;

    if (v2 >= arr15) v2 = arr15;
    if (v3 >= arr15) v3 = arr15;

    // s2 = Convert.ToChar(tUsername.Substring(0, 1));

    v4 = 0; v5 = 0;
    for (v1 = 0; v1 < v3; v1++)
    {
        trschar[v1 + 1] = Convert.ToChar(tFPassword.Substring(v1, 1));
        v4 = v4 + Convert.ToInt32(trschar[v1 + 1]);
    }

    for (v1 = 0; v1 < v2; v1++)
    {
        encchar[v1 + 1] = Convert.ToChar(tUsername.Substring(v1, 1));
        rchar[v1 + 1] = Convert.ToChar((Convert.ToInt32(encchar[v1 + 1]) + 47) % modnum);
        v5 = v5 + Convert.ToInt32(encchar[v1 + 1]);
    }

    v4 = v4 % 255;
    v5 = v5 % 255;

    for (v1 = v2 + 1; v1 < arr15 + 1; v1++)
    {
        encchar[v1] = Convert.ToChar((v4 * v1 + 3) % modnum);
        rchar[v1] = encchar[v1];
    }


    for (v1 = 1; v1 < 15 + 1; v1++)
        rchar[v1] ^= trschar[v1];

    // for (v1 = 1; v1 < v3+1; v1++)
    // {
    //     trschar[v1] ^= trschar[v1 + 20]; 
    // }

    rchar[arr15] = Convert.ToChar(v4);

    for (v1 = 1; v1 < 15 + 1; v1++)
    {
        if ( rchar[v1] == 39 )   
             rchar[v1] = Convert.ToChar((v1 + 50) % modnum); 
    }

    for (v1 = 1; v1 < 15 + 1; v1++)
    {
        s1 = s1 + trschar[v1].ToString();
        s2 = s2 + encchar[v1].ToString();
        s3 = s3 + rchar[v1].ToString();
    }

    return (s3);    
    
}  // end ConvertPS

protected void Button4_Click(object sender, EventArgs e)
{
    Response.Write("<script>window.open( 'ShowPict.aspx','one','width=800,height=500,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=30,left=30');</script>");
  
}

}  // end public partial class Main
