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
using SCM.GSCMDKen;
// using Prilibrary.FsplitNew;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using SFC.TJWEB;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Linq;
using Pub.Publibary;

public partial class Main_MainNokFox20170630 : System.Web.UI.Page
{
    static string NetSqlDB4 = ConfigurationManager.AppSettings["LocalHostSqlString"]; // Local104SqlString
    static string RunORACLEDB = ""; // ConfigurationManager.AppSettings["NormalDbConnectionString"]; //  NormalDbConnectionString
    static string OraL10DBAMain = "";      // 76  Oracle Unix
    static string OraL10DBAStandBy = "";    // 211 Oracle Unix
    static string OraL6DBAMain = "";        // 214 Oracle Unix 
    static string OraL6DBAStandBy = "";     // 
    static string OraWebCTLDBA = "";        // 221 Oracle Window
    static string SqlWebCTLDBA = "";        //      Control DBA 
    static string SqlWebDBAB2B = "";           // 108 Main DBA 
    static string CallTiptoptype = "";
    string SiteName = "BBLF";
    public static string PPSite = "BBLF";

    static string tparam = "";

    static string OraBBFUSEL10ReadDBA = "";       //   
    static string OraBBFUSEL10WriDBA = "";


    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
    ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();

    UsrCtllib UsrCtllibPointer = new UsrCtllib();
    //FPubLib     FPubLibPointer     = new FPubLib();
    AutoUpload AutoUploadPointer = new AutoUpload(); // CreateFile.cs
    TJLibZhu TJLibZhuPointer = new TJLibZhu();
    TJSY TJSYPointer = new TJSY();
    Convertlib ConvertlibPointer = new Convertlib();
    SFCLinkTiptop SFCLinkTiptopPointer = new SFCLinkTiptop();
    BBSCMlib BBSCMlibPointer = new BBSCMlib();

    NokSCMlib  NokSCMlibPointer   = new NokSCMlib();
    N_CreateSO N_CreateSOPointer = new N_CreateSO();
    GSFClib1   GSFClib1Pointer   = new GSFClib1();

    NokFoxlib  NokFoxlibPointer  = new NokFoxlib();
    NokFoxlib5 NokFoxlib5Pointer = new NokFoxlib5();
    NokFoxlib1 NokFoxlib1Pointer = new NokFoxlib1();
    NokFoxlib2 NokFoxlib2Pointer = new NokFoxlib2();
    NokFoxlib6 NokFoxlib6Pointer = new NokFoxlib6();
    PubLib1 PubLib1Pointer = new PubLib1();
    FGISlib FGISlibPointer = new FGISlib();
    

    static string Username = "", FPassword = "", SPassword = "", PSDB = "", LoginEMPTYPE = "", LoginFlag = "";
    static string Acttype = "", FoxconnSite = "";
    DbAccessing myAccessing = new DbAccessing();
    protected string CurrDay = DateTime.Today.ToString("yyyyMMdd");  // 20100320
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static int swflag1 = 0, swflag2 = 0, swflag3 = 0;
    static int arrayPicLong = 5000, DataPicLong = 0, DataNewLong, arrayPicPriLong = 5000, DataPicPriLong = 0;
    // static string[,] arrayPic = new string[arrayPicLong + 1, 20 + 1];
    // static string[,] arrayPicPri = new string[arrayPicPriLong + 1, 20 + 1];
    static int arrMsgX = 20, arrMsgY = 100;
    static string[,] arrMsg = new string[ arrMsgY + 1, arrMsgX + 1];
    string VUrl;
    static string sw1 = "1", sw2 = "1", sw3 = "1", ParaAutoflag = "", tmpstr = "";
    static int autorunflag = 0, runstep = 0, errcnt = 0;
    static int runstatus = 0, SysCount = 1;
    static int cnt1 = 1, cnt2 = 1, firstcnt = 0;
    static int s1flag = 0;
    static string msgdbstr = "sql";
    protected string Dboracle = "oracle", DbSql = "sql";
    protected string Autoprg = "auto", Menuprg = "menu", PITSystem = "Mot";
    static int ArrMax = 400;
    static string[] ReadTxtArray = new string[ArrMax];
    static string GPROLINE = "7A";
    static string connstr;
    string MSSCMDIR = "IMSCMWS";
    //string SFCFuncIP = "http://10.83.216.137/bbryreport/webservice/bbryservice.asmx";
    string SFCFuncIP = "http://10.74.14.48/report/webservice/bbryservice.asmx";

    static int arrB2BPSX = 300, arrB2BPSY = 30;
    static string[,] arrB2BPS = new string[arrB2BPSX + 1, arrB2BPSY + 1];

    public static string SN5A5A = "", PSB2BDBA = "", userCode = "test"; 
  


    // public static int autoctlflag = 1; // Auto Control
    // public static int autoCount = 1;   // Auto Control
    // 20170511 http://localhost:2580/NokFoxITWEB/MainNokFox.aspx?RunMode=5A5A12
    protected void Page_Load(object sender, EventArgs e) // 23524
    {      
        if (!Page.IsPostBack)
        {
            // http://localhost:2422/ITWeb/MainBBRY.aspx?RunMode=5A5A12
            // 5. digit 1.REAL 2.TEST
            // 6. digit 1.Mroal 2.Auto

            string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", t7 = "", t8 = "", t9 = "";
         

            tparam = "REAL";
            SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23716");  //  Data IM--93
            TextBox12.Text = "REAL"; 

            if (Request.QueryString["RunMode"] != null)
            {
                t2 = Request.QueryString["RunMode"].ToString();
                if ( t2.Substring(4,1) == "2" )
                {
                       tparam = "TEST";
                       SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23712");  //  Data IM--205
                       TextBox12.Text = "TEST"; 
                }
 
            }
          
            
            //    connstr = ConfigurationManager.AppSettings["BBSCM"];

            //int ArrMax = 200;
            //string[] ReadTxtArray = new string[ArrMax];
            ParaAutoflag = Request["Autoflag"];  // http://localhost:2422/ITWeb/MainBBRY.aspx?Autoflag=1  1000=1Sec
            string t22 = Request["Smode"];       // http://localhost:1328/IDMIntelMot/MainSCM.aspx?Smode=supervisor   Request["Smode=Supervisor"]; 
            OraBBFUSEL10ReadDBA = ReadParaTxt("WebReadParam.txt", "23535"); // SS FUSE Read  L10 44
            OraBBFUSEL10WriDBA = ReadParaTxt("WebReadParam.txt", "23535"); // SS FUSE Write L10 TJ Test 215
            // SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23524"); // Sql DBA 
            FoxconnSite = ReadParaTxt("WebReadParam.txt", "40030"); // BBRY FoxconnSite
            t1 = ReadParaTxt("WebReadParam.txt", "40031");          // BBRY Server location number 主服務器位置編號
            TextBox9.Text = FoxconnSite;
            //if (t1 != "") SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", t1);  // Get Data 
            // tparam = "PROD";
            //if (tparam == "TEST")
            //{
            //    SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23712");  // Test Data IM--205
            //}
            //else if (tparam == "REAL")  // 20170314
            //{
            //    SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23714");  //  Data IM--93
            //}
            //SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23703");  // Test Data IM-93
            // TextBox12.Text = "TEST";    // TEST, REAL
            TextBox11.Text = MSSCMDIR; // default "IMSCMMS"; // DBA Nane
            InitVar();
            //DataPicLong = Convert.ToInt32(PictlibPointer.GetPictByUserName(Username, arrayPic, arrayPicLong, NetSqlDB4, ref DataPicLong, ref DataNewLong));
            TextBox1.Focus();
            if (SiteName == "BBLF")
                TextBox6.Text = " LF SFC Web Server http://10.186.19.20/BBITWEB/MainBBRY.aspx";


            //Timer1.Enabled = false;
            // s1flag = autoctlflag; //Auto Only
            // string t11 = FPubLibPointer.PubWri_MessLog("MotTrace", SysCount.ToString(), autorunflag.ToString(), s1flag.ToString(), runstatus.ToString(), "FirstTime", "", "", "", NetSqlDB4, msgdbstr);         // 10 Code

            string Loc1 = "";
            Loc1 = ReadParaTxt("WebReadParam.txt", "12701");
            if (Loc1 != "") NetSqlDB4 = ConfigurationManager.AppSettings[Loc1]; //  NetSqlDB4 = ConfigurationManager.AppSettings["Sql221String"];  
            ReaddDRAFromwebconfig(SiteName);
            // ReaddDRAFromText( SiteName );
            GetTextFile(ref ReadTxtArray, ArrMax, "2");  // "1" Org, "2" Decode by Pcode "23101" 
            Loc1 = UsrCtllibPointer.GetTextByCode(ref ReadTxtArray, ArrMax, "10001", "1");  // "1" 不傳換
            Loc1 = UsrCtllibPointer.GetTextBySite(ref ReadTxtArray, ArrMax, SiteName, "1", ref OraL10DBAMain, ref OraL10DBAStandBy, ref OraL6DBAMain, ref OraL6DBAStandBy, ref OraWebCTLDBA, ref SqlWebCTLDBA, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8);  // "1" 不傳換
            RunORACLEDB = OraL10DBAMain; //  UsrCtllibPointer.GetTextByCode(ref ReadTxtArray, ArrMax, "23201", "1");  // TJ DB10 Server
            // NetSqlDB4 = SqlWebCTLDBA;
            TextBox5.Text = "";
            //TextBox8.Text = "";
            TextBox10.Text = "";
            TextBox13.Text = "";

            //if (Session["SysModeType"] == "TestMenu")  // Change Test Only
            //{
            //    FoxconnSite = "BJ"; // BBRY Test FoxconnSite
            //    TextBox9.Text = FoxconnSite;
            //    SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23524");  // Get Test DBA Data 
            //}

        }

        tmpstr = firstcnt.ToString();
        // Test ParaAutoflag = "1";
        // if (ParaAutoflag == "1")
        // 12

        string chk1 = "";
        if (Request.QueryString["RunMode"] != null)
        {
            chk1 = Request.QueryString["RunMode"].ToString();

            if ((chk1.Length > 5) && (chk1.Substring(5, 1) == "2"))
            {
                string t3 = auto1();
                ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>window.opener=null;window.open('','_self');window.close();</script>");

            }
        }

        //{
        //    string t3 = SysAutoExec();
        //    ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>window.opener=null;window.open('','_self');window.close();</script>");
        //}


    }   // end Page_load;


    protected string MainLoop()
    {
        int i = 3;
        string s1 = "";
        //Timer1.Enabled = false;
        TextBox6.Text = "系統自動啟動" + "IDMZZMainLoop" + SysCount.ToString();
        //string t1 = FPubLibPointer.PubWri_MessLog("IDMIntelAuto", SysCount.ToString(), autorunflag.ToString(), s1flag.ToString(), runstatus.ToString(), "MainLoop", "", "", "", NetSqlDB4, msgdbstr);         // 10 Code
        AutoMotProc();
        //Timer1.Enabled = true;
        return ("");
    }



    private void InitVar()
    {
        Username = "";
        FPassword = "";
        SPassword = "";
        PSDB = "";
        TextBox14.Text = "N";
        TextBox14.Text = "";
        Acttype = "C";
        LoginFlag = "N";
        Session["SUsername"] = "web";
        Session["SReadFlag"] = "";
        string p2 = ReadParaTxt("WebReadParam.txt", "23101");
        string p1 = SiteName.ToLower();
        // string t1 = PDataBaseOperation.init_var(p1, p2);
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string t2 = LibPDBA1Pointer.Gooinit_var(p1, p2);
        if ((ParaAutoflag != null) && (ParaAutoflag != ""))
        {
            if (ParaAutoflag.Substring(0, 1) == "1")
            {
                autorunflag = 1;
                // autoctlflag = 1; // Auto only
                // Timer1.Enabled = true; 
            }
        }


        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;


        for (v1 = 0; v1 <= arrMsgY; v1++)
            for (v2 = 0; v2 <= arrMsgX; v2++)
                 arrMsg[v1, v2] = "";


        int Alen = 50;

        //for ( v1=0; v1 < Alen; v1++ )
        //      for ( v2=0; v2 < Alen; v2++ )
        //            arrVar1[v1, v2] = "";

        string t1 = "", t3 = "", t4 = "";


        PSB2BDBA = TrsSecondIP("WebReadParam.txt", "11200");    // Real DBA
        // string DBAStatus = ReadParaTxt("WebReadParam.txt", "11903"); // This file define 1: Test 2:Real
        TextBox15.Text = TrsSecondIP("WebReadParam.txt", "11201");
        TextBox16.Text = TrsSecondIP("WebReadParam.txt", "11201");
        TextBox17.Text = "";

        for (v1 = 0; v1 < arrB2BPSX; v1++)
            for (v2 = 0; v2 < arrB2BPSY; v2++)
                arrB2BPS[v1, v2] = "";


        string st3 = FGISlibPointer.SetupNokPSarray("setup", "Username", ref arrB2BPS, "sql", PSB2BDBA, ref t1, ref t2, ref t3, ref t4);

        string st4 = FGISlibPointer.SetupNokPSarray("GETSNBYUSER", "Hanoi", ref arrB2BPS, "sql", PSB2BDBA, ref t1, ref t2, ref t3, ref t4);

    
        return;

      


        string Wridir = "PUBLIB";
        DataSet DNdt = null;

        // Get LINE
        string sqlr = " select *  from " + Wridir + ".TCWM01  where DATES = '" + CurrDay + "' ORDER by LINE, CLASS1 asc ";
        //DNdt = PDataBaseOperation.PSelectSQLDS(Dboracle, OraBBFUSEL10WriDBA, sqlr);
        if (DNdt == null) return;
        if (DNdt.Tables.Count <= 0) return;  // 20140813 Add
        if (DNdt.Tables[0].Rows.Count <= 0) return;
        int DNCnt = DNdt.Tables[0].Rows.Count;
        GPROLINE = DNdt.Tables[0].Rows[0]["LINE"].ToString().Trim();

        // Response.Redirect("main.aspx");   
    }  // initvar

    protected void Button1_Click(object sender, EventArgs e)
    {
        string pst1 = NetSqlDB4;
        //  ChkUsrPS(pst1);
        //ChkUsrPS();
        //TextBox14.Text = "";
        TextBox14.Text = "";

        string t1 = "", t2 = "", t5="", t3="", t4="", t6="";
        string sqlr = " select * from [IMSCMWS].[dbo].[Tbl_user] where USERID ='" + TextBox1.Text + "' and ps ='" + TextBox2.Text + "' ";
        DataTable DT = PDataBaseOperation.PSelectSQLDT("SQL", SqlWebDBAB2B, sqlr);

        if (!DT.Rows.Count.Equals(0))
        {
            //TextBox14.Text = "Y";
            TextBox14.Text = "Y";
            LinkButton1.Visible = true;
            LinkButton2.Visible = true;
            Button26.Enabled = true;
            Button27.Enabled = true;
            Button49.Enabled = true;
            Button89.Enabled = true;
            Button5.Enabled = true;
            Button91.Enabled = true;
            Button85.Enabled = true;
            Session["C_3A4"] = DT.Rows[0]["C_3A4"].ToString();
            Session["C_3A4C"] = DT.Rows[0]["C_3A4C"].ToString();
            Session["C_940"] = DT.Rows[0]["C_940"].ToString();
            Session["C_204"] = DT.Rows[0]["C_204"].ToString();
            Session["C_3B2"] = DT.Rows[0]["C_3B2"].ToString();
            Session["userid"] = DT.Rows[0]["userid"].ToString();
            Session["username"] = DT.Rows[0]["username"].ToString();
            Session["Nokusersite"] = DT.Rows[0]["UserSite"].ToString();
            Session["Nokfactoryno"] = DT.Rows[0]["F1"].ToString();
            Session["Nokcstatus"] = "";
            Session["NokRunEnv"] = "";
           
            Session["NokVendID"] = ""; // DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
            Session["Nokcstatus"] = ""; // = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // 1,4
            Session["NokRunEnv"] = ""; // DN1.Tables[0].Rows[0]["F12"].ToString().Trim();  // TEST REAL
            Session["NokSalesOrg"] = ""; // DN1.Tables[0].Rows[0]["F14"].ToString().Trim();  // factory status
            Session["NokSapPlant"] = ""; // DN1.Tables[0].Rows[0]["F16"].ToString().Trim();  // factory status
            Session["NokB2BIP"] = ""; // DN1.Tables[0].Rows[0]["F18"].ToString().Trim();  // factory status
            Session["NokSFCIP"] = ""; // DN1.Tables[0].Rows[0]["F20"].ToString().Trim();  // factory status

            lblUserName.Text = DT.Rows[0]["username"].ToString();

            t2 = DT.Rows[0]["F2"].ToString();  // Foxconn factory code by Foxconn Site SiteNo1003 
            t1 = DT.Rows[0]["F1"].ToString().Trim();
                     

            if ( ( t2 != "" ) && ( t2 != null ))
            {
                string sqlr1 = " select * from FGISM1001 where F1 = 'NokFoxM2002'  and F2 ='" + t2 + "' ";
                DataSet DN1 = PDataBaseOperation.PSelectSQLDS("SQL", SqlWebDBAB2B, sqlr1);
                if (DN1.Tables.Count > 0)
                {
                    int DN1Cnt1 = DN1.Tables[0].Rows.Count;
                    if (DN1Cnt1 > 0)
                    {
                        t5 = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // factory status
                        Session["Nokcstatus"] = t5.ToString().Trim();

                        string t31 = DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
                        string t32 = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // 1,4
                        string t33 = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();  // TEST REAL
                        string t34 = DN1.Tables[0].Rows[0]["F14"].ToString().Trim();  // factory status
                        string t35 = DN1.Tables[0].Rows[0]["F16"].ToString().Trim();  // factory status
                        string t36 = DN1.Tables[0].Rows[0]["F18"].ToString().Trim();  // B2B IP
                        string t37 = DN1.Tables[0].Rows[0]["F20"].ToString().Trim();  // SFC
                        string t38 = DN1.Tables[0].Rows[0]["F26"].ToString().Trim();  // B2B Second IP
                        string t39 = DN1.Tables[0].Rows[0]["F28"].ToString().Trim();  // SFC
                        Session["NokVendID"]    = DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
                        Session["Nokcstatus"]   = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // 1,4
                        Session["NokRunEnv"]    = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();  // TEST REAL
                        Session["NokSalesOrg"]  = DN1.Tables[0].Rows[0]["F14"].ToString().Trim();  // factory status
                        Session["NokSapPlant"]  = DN1.Tables[0].Rows[0]["F16"].ToString().Trim();  // factory status
                        Session["NokB2BIP"]     = DN1.Tables[0].Rows[0]["F18"].ToString().Trim();  // factory status
                        Session["NokSFCIP"]     = DN1.Tables[0].Rows[0]["F20"].ToString().Trim();  // factory status
                        Session["NokB2BIPSec"]  = DN1.Tables[0].Rows[0]["F26"].ToString().Trim();  // factory status
                        Session["NokSFCIPSec"]  = DN1.Tables[0].Rows[0]["F28"].ToString().Trim();  // factory status
                        Session["NokUrlEnv"]    = DN1.Tables[0].Rows[0]["F29"].ToString().Trim();  // url Envirment
                        Session["NokDataBomID"] = DN1.Tables[0].Rows[0]["F30"].ToString().Trim();  // 156 DataBomID

                        tparam          = Session["NokRunEnv"].ToString().Trim();//  "TEST";
                        SqlWebDBAB2B       = ReadParaTxt("WebReadParam.txt",  Session["NokB2BIP"].ToString().Trim() );
                        RunORACLEDB = ReadParaTxt("WebReadParam.txt", Session["NokSFCIP"].ToString().Trim());
                        TextBox12.Text  = Session["NokRunEnv"].ToString().Trim();
                        Session["CURRNokB2BIP"]     = SqlWebDBAB2B.ToString().Trim();  // factory status
                        Session["CURRNokB2BDBType"] = DbSql.ToString().Trim();
                        Session["CURRNokSFCIP"]     = RunORACLEDB.ToString().Trim();  // factory status
                        Session["CURRNokSFCDBType"] = Dboracle.ToString().Trim();

                        TextBox15.Text = TrsSecondIP("WebReadParam.txt", DN1.Tables[0].Rows[0]["F26"].ToString().Trim());  // Sec IP
                        TextBox16.Text = TrsSecondIP("WebReadParam.txt", DN1.Tables[0].Rows[0]["F26"].ToString().Trim());  // Sec IP
                        TextBox17.Text = DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
                    }
                }

            }
            else
            Session["Nokcstatus"]   = DT.Rows[0]["F3"].ToString();

            // 20170603 confirm which way by data or param

            if (Request.QueryString["RunMode"] != null)
            {
                t2 = Request.QueryString["RunMode"].ToString();
                if (t2.Substring(4, 1) == "2")
                {
                    tparam = "TEST";
                    SqlWebDBAB2B = TrsSecondIP("WebReadParam.txt", "11202"); // ReadParaTxt("WebReadParam.txt", "23712");  //  Data IM--93                
                    TextBox15.Text = TrsSecondIP("WebReadParam.txt", "11202");
                    TextBox16.Text = TrsSecondIP("WebReadParam.txt", "11202");
                    TextBox12.Text = "TEST";
                }
                else
                {
                    tparam = "REAL";
                    // SqlWebDBAB2B   = TrsSecondIP("WebReadParam.txt", "11001"); // ReadParaTxt("WebReadParam.txt", "23716");  //  Data IM--93
                    SqlWebDBAB2B   = TrsSecondIP("WebReadParam.txt", "11201"); // ReadParaTxt("WebReadParam.txt", "23712");  //  Data IM--93                
                    TextBox15.Text = TrsSecondIP("WebReadParam.txt", "11201");
                    TextBox16.Text = TrsSecondIP("WebReadParam.txt", "11201");             
                    TextBox12.Text = "REAL";

                }
            }
            // end switch 
            
        }
        else 
        {
            TextBox14.Text = "";
            TextBox14.Text = "";
            Response.Write("<script>alert('Your username or password is not correct,Please try again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
        
        }

    }

    protected void Button2_Click(object sender, EventArgs e)  // Modify 
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {


    }


    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        Username = TextBox1.Text;
        FPassword = TextBox2.Text;
        // ChkUsrPS(); 
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
    }

    protected void tvManuTree_SelectedNodeChanged(object sender, EventArgs e)
    {

    }


    //////////////////////////////////////////////////////////////////////////////////////
    //  1000  for 1 second,  //  60000 for 1 minute, 1800000 for 30 minute
    //  
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        string s1 = "";
        TextBox6.Text = "Timer Interrupt";
        SysCount = SysCount + 2;
        // s1flag = autoctlflag; //Auto only
        // string t1 = FPubLibPointer.PubWri_MessLog("IDMIntelAuto", SysCount.ToString(), autorunflag.ToString(), s1flag.ToString(), runstatus.ToString(), "TimerINT", "", "", "", NetSqlDB4, "sql");         // 10 Code

        return; // not auto only

        //s1flag = autoctlflag;
        //AutoCount++;
        //if (autoctlflag == 1)  // boot automatic runnning
        //{
        //    if (runstatus == 0) // go to running
        //    {
        //        t1 = FPubLibPointer.PubWri_MessLog("IDMIntelAuto", autoCount.ToString(), autorunflag.ToString(), s1flag.ToString(), runstatus.ToString(), "TimeINTGOToMainloop", "", "", "", NetSqlDB4);  // 10 Code
        //        s1 = MainLoop();
        //    }
        //    else
        //        errcnt++;
        //}

        if (errcnt > 2)
        {
            errcnt = 0;
            runstatus = 0;
        }

    }

    ///////////////////////////////////////////////////////////////////////
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");
        Session["tparam"] = "TEST";
        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/POCancel.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }
    protected void Button91_Click(object sender, EventArgs e)
    {
        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");
        
        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/FPODL3.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        LinkButton1.Visible =  false;
        LinkButton2.Visible = false;
        Button26.Enabled = false;
        Button27.Enabled = false;
        Button49.Enabled = false;
        Button89.Enabled = false;
        Button5.Enabled = false;
        Button91.Enabled = false;
        Session["C_3A4"] ="";
        Session["C_3A4C"] = "";
        Session["C_940"] = "";
        Session["C_204"] = "";
        Session["C_3B2"] = "";
        Session["userid"] = "";
        Session["username"] = "";
        lblUserName.Text = "User";
    }

    protected void Button35_Click(object sender, EventArgs e)
    {
        autorunflag = 0;   // Automatic running
        runstatus = 0;     // currstatus
        runstep = 0;       // run step
        //Timer1.Enabled = false;
        TextBox6.Text = "Clear Loop flag" + SysCount.ToString();
    }

    protected void Button21_Click(object sender, EventArgs e)
    {

    }

    // Query SFC link Tiptop
    protected void Button44_Click(object sender, EventArgs e)
    {
        return;
        ////Timer1.Enabled = false;
        //string Dtype = "C", SysDate = DateTime.Today.ToString("yyyyMMdd"), DBType = "oracle";
        //string DBReadString = ReadParaTxt("WebReadParam.txt", "23301");  // L10
        //string DBWriString = ReadParaTxt("WebReadParam.txt", "23303");  // L10
        //string PCode = ReadParaTxt("WebReadParam.txt", "23101");
        //Session["ZZPCode"] = ReadParaTxt("WebReadParam.txt", "23101");
        //Session["ZZPSite"] = "tjsfc";
        ////DBReadString = ConfigurationManager.AppSettings["OraZZL10DBConnectionString"]; 
        ////DBWriString  = ConfigurationManager.AppSettings["OraZZL10DBConnectionString"];
        ////string Retstr1 = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P0_3("1", "oracle", DBReadString, DBWriString, "1");  

        //string tmp1 = OraWebCTLDBA;   // ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraWebConnectionStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
        //string tmp2 = OraL10DBAMain;  // ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraL10NormalConnStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
        //Session["Param1"] = 1;
        //Session["Param2"] = "oracle";     // oracle
        //Session["Param3"] = DBReadString; // L10
        //Session["Param4"] = DBWriString;  // L6
        //Response.Write("<script>window.open( 'MainGooPrg/SelectSFCTiptop.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
    }


    protected void Button24_Click(object sender, EventArgs e)
    {
        return;
        //Timer1.Enabled = false;
        if (FPassword.ToLower() == "web")
            Response.Write("<script>window.open( 'Traceability/TJTraceMenu.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    protected void Button26_Click(object sender, EventArgs e)
    {
        //Timer1.Enabled = false;
        // if (FPassword.ToLower() == "web")
        string tmp1 = OraBBFUSEL10WriDBA; // 215
        string tmp2 = OraBBFUSEL10WriDBA; // 215
        string tmpdate = "";
        string tmpLINE = "A1";

        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        if (GPROLINE != "") tmpLINE = GPROLINE;

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Session["tparam"] = tparam;  // PROLINE 

        Response.Write("<script>window.open( 'MainMSPrg/F940Q1.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }


    /////////////////////////////////////////////////////////////////////
    protected void Button27_Click(object sender, EventArgs e)
    {
        //Timer1.Enabled = false;
        // if (FPassword.ToLower() == "web")
        string tmp1 = OraBBFUSEL10WriDBA; // 215
        string tmp2 = OraBBFUSEL10WriDBA; // 215
        string tmpdate = "";
        string tmpLINE = "A1";

        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        if (GPROLINE != "") tmpLINE = GPROLINE;

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Session["tparam"] = tparam;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/FMDN01.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    protected void Button47_Click(object sender, EventArgs e)
    {
        //Timer1.Enabled = false;

        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/FDISP1.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }


    protected void Button49_Click(object sender, EventArgs e)
    {
        //Timer1.Enabled = false;

        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/F204Q1.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }
    protected void Button89_Click(object sender, EventArgs e)
    {
        //Timer1.Enabled = false;

        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Session["tparam"] = tparam;
        Response.Write("<script>window.open( 'MainMSPrg/FPODL2.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
    }
    protected void Button90_Click(object sender, EventArgs e)
    {
        //Timer1.Enabled = false;

        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/FPOI01.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        if (FPassword.ToLower() != "kenken") return;
        //Timer1.Enabled = true;
        // autoctlflag = 1; // auto only
        Button17.Text = "系統自動啟動中";
        TextBox6.Text = "系統自動啟動";
        string tmpstr = MainLoop();
    }


    public string Auto_CreDN()
    {
        return ("");
    }

    ////////////////////////////////////////////////////////////
    // Auto Generate UPD
    // 1. Use Table UPD_PODN_List_T table
    // 2. Index Invoice(DN), 
    // 3. DN, PO, UPDflag, Sapflag, 
    // 4. Auto_CreDN() -- Generate DN, PO 
    // 5. Auto_CloseSap() -- Check and Write Sap
    // 6. Auto_GenUPDEMEI() -- Check Sapclag="C" , Close , write
    // Appendix :
    // 1. Need check double or already generate UPD/EMEI
    // 2. Sap.Cmcs_pack_line_all singe DN + Multi Carton
    // 3. Sap.Sap_DN_Info singe DN + Multi Item
    // 4. Sap.Cmcs_ship_date Singe Carton + Multi PSN,EMEI
    // 5. DN Count = Sap.Sap_Invoice_Info
    ///////////////////////////////////////////////////////////
    public void AutoMotProc()
    {
        return;
        //string t1 = FPubLibPointer.PubWri_MessLog("IDMIntelAuto", SysCount.ToString(), autorunflag.ToString(), s1flag.ToString(), runstatus.ToString(), "AutoUPD", "", "", "", NetSqlDB4, msgdbstr);         // 10 Code
        //TextBox6.Text = "系統自動 Execute" + firstcnt.ToString();
        //string DBReadString = ConfigurationManager.AppSettings["L8StandByConnectionString"]; // bjl6testConnectionString
        //string DBWriString = ConfigurationManager.AppSettings["L8TestandWebConnectionString"]; // bjl6testConnectionString
        //int int1 = AutoUploadPointer.GetDnWithInsert(DBReadString, DBWriString);
        //TJLibZhuPointer.CheckSAPClose(DBWriString, DBWriString);
        //TJSYPointer.check3s4s(DBReadString, DBWriString);
        //int int2 = AutoUploadPointer.GetCreateUpdFile(DBReadString, DBWriString);     
    }

    protected void Button51_Click(object sender, EventArgs e) // 查尋 UPD 
    {
        //return;
        ////Timer1.Enabled = false;
        //string tmp1 = OraWebCTLDBA;   // ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraWebConnectionStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
        //string tmp2 = OraL10DBAMain;  // ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraL10NormalConnStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
        //Session["DBReadString"] = tmp1;
        //Session["Param1"] = 1;
        //Session["Param2"] = tmp1; // DBReadString
        //Session["Param3"] = tmp2; // DBReadString
        //Response.Write("<script>window.open( 'MainMotPrg/Query_UPDStates.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        //// Response.Write("<script>window.open( 'Finvm1.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    // Check and Get data automatic
    protected void Button52_Click(object sender, EventArgs e)
    {
        if (TextBox5.Text != "Foxconn88")
        {
            Response.Write("<script>alert('You password is not correct Reentry password again !!')</script>");
            return;

        }

        string t3 = SysAutoExec();

    }


    protected string SysAutoExec()
    {

        ////Timer1.Enabled = false;

        //string t2 = "", t3 = "";

        //string tmpdate = "", tmpdate1 = "", tmpdate2="";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 

        //string t11 = BBSCMlibPointer.AutoCheckPOCHANGE("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, MSSCMDIR);

        //string t1 = BBSCMlibPointer.AutoCheckPOCRAETE("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, MSSCMDIR);
        //t1 = BBSCMlibPointer.AutoCreDLABELINFO("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, MSSCMDIR);
        //t1 = BBSCMlibPointer.AutoCreFDLABM1("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, MSSCMDIR);

        ////t3 = BBSCMlibPointer.CallSapAndCreateSO("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, "00", "", "");
        //// Check PO create and write CONFIRMFLAG = "Y" in Master if OK
        ////Response.Write("<script>window.open( 'MainMSPrg/FDNQ01.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        //BBSCMlib ww = new BBSCMlib();

        //// string dboReadS = "ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh";
        //string dboReadS = "ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=RFCLFSAP01;PASSWD=12345678;LANG=zh";

        //// string return_var = ww.GetSapDN("", dboReadS, SqlWebDBAB2B, "20140321", "20140321");
        //// string return_var = ww.GetSapDN("82420682", dboReadS, SqlWebDBAB2B, tmpdate, tmpdate );

        //tmpdate2 = DateTime.Today.ToString("yyyyMMdd");
        //tmpdate1 = DateTime.Today.AddDays(-15).ToString("yyyyMMdd");
        ////tmpdate1 = DateTime.Today.AddMonths(-2).ToString("yyyyMMdd");

        //string return_var = ww.GetSapDN("", dboReadS, SqlWebDBAB2BB2B, tmpdate1, tmpdate2, MSSCMDIR);
        ////string return_var = ww.GetSapDN("", dboReadS, SqlWebDBAB2B, "20150130", "20150201");


        ////  string tmpDN = BBSCMlibPointer.AutoGetDNfromSap("1", DbSql, dboReadS, SqlWebDBAB2B, "menu", tmpdate, MSSCMDIR);
        //string tmpIP = SFCFuncIP; //  ReadParaTxt("WebReadParam.txt", "40035"); // SS FUSE Write L10 TJ Test 215
        //t3 = BBSCMlibPointer.ReturnSendFlagToSFCByDN("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, tmpIP, "", MSSCMDIR);
        //t2 = BBSCMlibPointer.AutoGetPackingFromSFC("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, tmpIP, MSSCMDIR);



        ////pochange
        ////t3 = BBSCMlibPointer.AutoChk_POValue("1", DbSql, SqlWebDBAB2B, SqlWebDBAB2B, "menu", tmpdate, MSSCMDIR);

        return ("");
    }



    protected void Button54_Click(object sender, EventArgs e)
    {
        ////Timer1.Enabled = false;
        //string t2 = "", t3 = "";

        //string tmpdate = "", tmpdate1 = "", tmpdate2 = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 

        //BBSCMlib ww = new BBSCMlib();

        //tmpdate2 = DateTime.Today.ToString("yyyyMMdd");
        //tmpdate1 = DateTime.Today.AddDays(-7).ToString("yyyyMMdd");

        //string dboReadS = "ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=RFCLFSAP01;PASSWD=12345678;LANG=zh";
        //// string return_var = ww.GetSapDN("", dboReadS, SqlWebDBAB2B, tmpdate1, tmpdate2);
        ////  string tmpDN = BBSCMlibPointer.AutoGetDNfromSap("1", DbSql, dboReadS, SqlWebDBAB2B, "menu", tmpdate, MSSCMDIR);
        //string tmpIP = " http://10.74.14.48/report/webservice/bbryservice.asmx"; //  ReadParaTxt("WebReadParam.txt", "40035"); // SS FUSE Write L10 TJ Test 215
        ////string tmpIP = "http://10.83.216.137/bbryreport/webservice/bbryservice.asmx";
        //string tmpDN = TextBox10.Text.ToString().Trim();

        //string sqlr = "  SELECT  * from  " + MSSCMDIR + ".[dbo].Delivery_Notification_MT where (  SFCFlag = '' or  SFCFlag = 'N' or SFCFlag is null ) and DNID  = '" + tmpDN + "' ";
        //DataSet DNdt = PDataBaseOperation.PSelectSQLDS(DbSql, SqlWebDBAB2B, sqlr);
        //if (DNdt == null) return;     // Syn Error
        //int DNCnt1 = DNdt.Tables[0].Rows.Count;
        //if (DNCnt1 == 0)
        //{
        //    Response.Write("<script>alert('This DN already get data from SFC !!')</script>");
        //    TextBox6.Text = "No this DN ";
        //    return;     // Not Data

        //}

        //int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6= 0, v7= 0;
        //string[,] arrayPO_CREATE_MT = new string[DNCnt1 + 1, 15 + 1];
        //for (v1 = 0; v1 <= DNCnt1; v1++)
        //    for (v2 = 0; v2 <= 10; v2++)
        //        arrayPO_CREATE_MT[v1, v2] = "";

        //// Put im tmp array
        //for (v1 = 0; v1 < DNCnt1; v1++)
        //{
        //    arrayPO_CREATE_MT[v1 + 1, 0] = (v1 + 1).ToString();
        //    arrayPO_CREATE_MT[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
        //    arrayPO_CREATE_MT[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["CreationDT"].ToString().Trim();
        //    arrayPO_CREATE_MT[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["DNID"].ToString().Trim();
        //    // arrayPO_CREATE_MT[v1 + 1, 3] = "80168556"; //  "80168537";  // Test Only
        //    arrayPO_CREATE_MT[v1 + 1, 4] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 5] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 6] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 7] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 8] = "N";
        //    tmpDN = arrayPO_CREATE_MT[v1 + 1, 3];
        //}


        //string DBType = DbSql;
        //string DBReadString = SqlWebDBAB2B;
        //string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = ""; ;
        //// Check in  FROM [BBSCM].[dbo].[Delivery_Notification_HU], where DNID = '80168556' if not exist then call 
        //for (v1 = 0; v1 < DNCnt1; v1++)
        //{
        //    tmpDN = arrayPO_CREATE_MT[v1 + 1, 3];
        //    tmp1 = "N";
        //    v2 = 1;
        //    v3 = 1;
        //    sqlr = "  SELECT  COUNT(*) as HUQTY from  " + MSSCMDIR + ".[dbo].Delivery_Notification_HU where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
        //    DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        //    if (DNdt == null) return;
        //    if (DNdt.Tables.Count > 0) v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["HUQTY"].ToString()); // tmpds.Tables[0].Rows.Count;

        //    sqlr = "  SELECT  COUNT(*) as DTQTY from  " + MSSCMDIR + ".[dbo].Delivery_Notification_DT where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
        //    DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        //    if (DNdt == null) return;
        //    if (DNdt.Tables.Count > 0) v3 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["DTQTY"].ToString()); // tmpds.Tables[0].Rows.Count;

        //    if ((v2 == 0) && (v3 == 0))  // No SFC Coming Data you can running
        //    {
        //        // Clear Data in temp
        //        sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Notification_HU_TEMP where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
        //        v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //        sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Notification_DT_TEMP where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
        //        v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //        sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Map_DN          where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
        //        v6 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //        sqlr = "  truncate table  " + MSSCMDIR + ".[dbo].Delivery_Map_DN   ";
        //        v7 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr); 

        //        // tmpDN = arrayPO_CREATE_MT[v1 + 1, 3];
        //        tmp2 = BBSCMlibPointer.GetPackingFromSFCTOBUFByDN_V01("DN", DbSql, DBReadString, SqlWebDBAB2B, tmpDN, tmpdate, tmpIP, tmpDN, MSSCMDIR);
        //        TextBox6.Text = tmp2;

        //        if ( tmp2 == "" )
        //            tmp3 = BBSCMlibPointer.CheckDNQTYAndInsert("1", DbSql, DBReadString, DBReadString, tmpDN, tmpdate, tmpIP, tmpDN,MSSCMDIR); // 20140422

        //    }  // endif
        //    // endif
        //}





    }

    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
        //int ArrMax = 300;
        string[] ReadTxtArray = new string[ArrMax];
        string FileName = "SetReadParam.txt";
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string InString = "";
        int i = 0, strlen = 0;

        while (((InString = sr.ReadLine()) != null) && (i < ArrMax))
        {
            ReadTxtArray[i] = InString;
            //             Response.Write(ReadTxtArray[i]);
            //             Response.Write("<br>");
            if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
            {
                strlen = InString.Length - 1;
                if ((InString.Substring(0, 5) == ParaNum) && (strlen >= 6))
                {
                    retPara = InString.Substring(6, strlen - 5);
                    i = ArrMax;  // Break
                }

            }
            i++;

        }

        sr.Close();
        return (retPara);
    }


    // select distinct INVOICE from CMCS_SFC_PACKING_LINES_ALL order by INVOICE desc
    // DeliMotchk.aspx  
    // 1.1 Check E2P      DelchkUPD.cs
    // 1.2 Check Simlock  DelchkSimLock.cs
    // 1.3 Check Qty      DelchkProductQty.cs
    // 1.4 Check PO       DelchkPO.cs
    // 1.5 Check SAP      DelchkSap.cs
    // 1.6 Write Del-time to DELITIME
    protected void Button57_Click(object sender, EventArgs e)
    {
        ////Timer1.Enabled = false;

        //string tmpdate = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 
        //Response.Write("<script>window.open( 'MainMSPrg/FDNQ02.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

        //return;
        //// DeliMotchk.aspx
        ////Timer1.Enabled = false;

        //// if (FPassword.ToLower() != "web") return;

        //string tmp1 = OraL10DBAStandBy; // ConfigurationManager.AppSettings["L8StandByConnectionString"];  // 211 string tSUsername = Session["DBReadString"].ToString();
        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle;  // oracle
        //Session["Param3"] = tmp1;      // DBReadString
        //tmp1 = OraL10DBAMain; // ConfigurationManager.AppSettings["NormalDbConnectionString"];  // 76 string tSUsername = Session["DBReadString"].ToString();
        //Session["Param4"] = tmp1;      // DBWriteString
        //Session["Param5"] = "menu";    // Menu input
        //tmp1 = OraWebCTLDBA; // ConfigurationManager.AppSettings["L8TestandWebConnectionString"];  // 221 string tSUsername = Session["DBReadString"].ToString();
        //Session["Param6"] = tmp1;   
        //Response.Write("<script>window.open( 'MainMotPrg/DeliMotChk.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        ////Response.Write("<script>window.open( 'Carton.Check.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    protected void Button62_Click(object sender, EventArgs e)
    {
        ////Timer1.Enabled = false;

        //string tmpdate = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 
        //Response.Write("<script>window.open( 'MainMSPrg/SCMPOShipToAddress.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");


    }

    protected void Button36_Click(object sender, EventArgs e)
    {
        if (TextBox14.Text != "Y")
        {
            Response.Write("<script>alert('Your username or password is not correct,Please try again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
            return;
        }

        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");
        string Runtype = TextBox12.Text;  // TEST, REAL
        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime = tmpdate + "235959";  // Session["Param9"].ToString();

        string t5 = "";
        t5 = Session["NokVendID"].ToString().Trim(); // = DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
        t5 = Session["Nokcstatus"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // 1,4
        t5 = Session["NokRunEnv"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();  // TEST REAL
        t5 = Session["NokSalesOrg"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F14"].ToString().Trim();  // factory status
        t5 = Session["NokSapPlant"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F16"].ToString().Trim();  // factory status
        t5 = Session["NokB2BIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F18"].ToString().Trim();  // factory status
        t5 = Session["NokSFCIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F20"].ToString().Trim();  // factory status
        t5 = Session["CURRNokB2BIP"].ToString().Trim();  // Curr B2B IP
        t5 = Session["CURRNokB2BDBType"].ToString().Trim(); //  DBA Type sql

        t5 = SqlWebDBAB2B.ToString().Trim();  
        t5 = DbSql.ToString().Trim();
        t5 = RunORACLEDB.ToString().Trim();  
        t5 = Dboracle.ToString().Trim();

        t5 = Session["CURRNokB2BIP"].ToString().Trim();  // factory status
        t5 = Session["CURRNokB2BDBType"].ToString().Trim();
        t5 = Session["CURRNokSFCIP"].ToString().Trim();  // factory status
        t5 = Session["CURRNokSFCDBType"].ToString().Trim();


        string tmpReadDBA = "", tmpWriDBA = "", tVendid = "";

        string t1 = "", t2 = "", t3 = "", t4 = "";
        string st4 = FGISlibPointer.SetupPSarray("GETSNBYUSER", TextBox1.Text, ref arrB2BPS, PSB2BDBA, ref t1, ref t2, ref t3, ref t4);
        if ((st4 != "") && (st4 != "0"))
        {
            int arrcnt = Convert.ToInt32(st4);
            if (arrcnt > 1)
            {
                tmpReadDBA = TrsSecondIP("WebReadParam.txt", arrB2BPS[arrcnt, 19].ToString().Trim());
                tmpWriDBA  = TrsSecondIP("WebReadParam.txt", arrB2BPS[arrcnt, 19].ToString().Trim());
                tVendid    = arrB2BPS[arrcnt, 0].ToString().Trim();
                Session["NokVendID"] = tVendid.ToString().Trim();
                // Runtype    = 
                t4 = arrB2BPS[arrcnt, 2].ToString().Trim(); // ReadParaTxt("WebReadParam.txt", arrPS[arrcnt, 20].ToString().Trim());
            }
        }


        if ((tmpReadDBA == "") || (tmpWriDBA == "") || (Session["NokVendID"] == null) )
        {
            Response.Write("<script>alert('Your User + Password fault , Pls ! Re-Entry User + Password  !!')</script>");
            return;
        }


        if (tmpReadDBA.ToString().Trim() != TextBox15.Text.ToString().Trim())
        {
            Response.Write("<script>alert('Your User + Password Text != Static Variable , Pls ! Re-Entry User + Password  !!')</script>");            
        }
        else
            TextBox6.Text = "tmpReadDBA = TextBox15 :" + TextBox15.Text.ToString(); 

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = tmpReadDBA; // DBReadString   
        Session["Param4"] = tmpWriDBA; // DBWriteString  
        Session["Param5"] = Runtype;   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Session["Param8"] = tmpdate + "000001";
        Session["Param9"] = tmpdate + "235959";
        Response.Write("<script>window.open( 'MainNokFoxPrg/PoQuerySo.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

        
        //string tmpdate = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");
        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        //Session["Param5"] = "TEST";   // TEST, REAL
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 
        //Session["Param8"] = tmpdate + "000001";
        //Session["Param9"] = tmpdate + "235959";
        //
        //Response.Write("<script>window.open( 'MainNokFoxPrg/PoQuerySo.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    
    }


    protected void Button63_Click(object sender, EventArgs e)
    {

        //return;
        ////Timer1.Enabled = false;
        //TextBox6.Text = "MainPIDModifyProc";
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraL10DBAMain; //ConfigurationManager.AppSettings["NormalDbConnectionString"];  // 76
        //string tmp2 = OraWebCTLDBA; //ConfigurationManager.AppSettings["L8TestandWebConnectionString"];  // 221

        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = tmp1; // DBReadString  76
        //Session["Param4"] = tmp2; // DBReadString  76
        //Session["Param5"] = "menu";   // Menu input
        //Response.Write("<script>window.open( 'MainMotPrg/MainPIDModify.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    protected void Button64_Click(object sender, EventArgs e)
    {
        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        if ((TextBox14.Text == "N") || (TextBox14.Text == ""))
        {
            Response.Write("<script>alert('Your User + Password fault , Pls ! Re-Entry User + Password  !!')</script>");
            return;
        }

        string tmpReadDBA = "", tmpWriDBA = "", tSite = "";

        string t1 = "", t2 = "", t3 = "", t4 = "";
        string st4 = FGISlibPointer.SetupPSarray("GETSNBYUSER", TextBox1.Text, ref arrB2BPS, PSB2BDBA, ref t1, ref t2, ref t3, ref t4);
        if ((st4 != "") && (st4 != "0"))
        {
            int arrcnt = Convert.ToInt32(st4);
            if (arrcnt > 1)
            {
                tmpReadDBA = TrsSecondIP("WebReadParam.txt", arrB2BPS[arrcnt, 19].ToString().Trim());
                tmpWriDBA = TrsSecondIP("WebReadParam.txt", arrB2BPS[arrcnt, 19].ToString().Trim());               
                tSite = arrB2BPS[arrcnt, 8].ToString().Trim();
                t4 = arrB2BPS[arrcnt, 2].ToString().Trim(); // ReadParaTxt("WebReadParam.txt", arrPS[arrcnt, 20].ToString().Trim());
            }
        }


        if (( tmpReadDBA == "" ) || ( tmpWriDBA == "" ))
        {
            Response.Write("<script>alert('Your User + Password fault , Pls ! Re-Entry User + Password  !!')</script>");
            return;
        }

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Session["Param8"] = tmpdate + "000001";
        Session["Param9"] = tmpdate + "235959";

        Response.Write("<script>window.open( 'MainNokFoxPrg/PoQuery.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    // UPdate PO Block
    protected void Button65_Click(object sender, EventArgs e)
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");

        string tmpPO = "";
        if (TextBox13.Text != "") tmpPO = TextBox13.Text;
        string Ret1 = POCheck(tdate, TextBox12.Text, MSSCMDIR, tmpPO);
    }

    private string POCheck(string tdate, string Rtype, string tmpMSSCMDIR, string tmpPO)
    {

        string tmpdate = "";
        if (tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype = "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime = tmpdate + "235959";  // Session["Param9"].ToString();


        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";

        string Ret1 = ""; // NokScnRevPo.GetPoHeader(DbSql, SqlWebDBAB2B, tmpMSSCMDIR, Starttime, Endtime);
        // string Starttime = Session["Param8"].ToString();
        // string Endtime = Session["Param9"].ToString();
        // NokScnRevPo.GetPoHeader(DbSql, SqlWebDBAB2B, MSSCMDIR, Starttime, Endtime);
        return (Ret1);

    }

    protected void Button66_Click(object sender, EventArgs e)
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");
        string Ret1 = POCreSO(tdate, TextBox12.Text, MSSCMDIR);
       
    }

    private string POCreSO(string tdate, string Rtype, string tmpMSSCMDIR)
    {


        string tmpdate = "";
        if (tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype = "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime   = tmpdate + "235959";  // Session["Param9"].ToString();

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";

        //GSFCliib2 NokScmAuto = new GSFCliib2();
        string envtype = Runtype; //  Session["Param5"].ToString();
        // NokScmAuto.AutoRunSo(DbSql, SqlWebDBAB2B, envtype, tmpMSSCMDIR);
        
        // cs.WebCreateSO("sql", SqlWebDBAB2B, "TEST", testPO, "IMSCMWS");

        return( "" );
    }

    protected void Button67_Click(object sender, EventArgs e)
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");
        string tmpDN = "";
        if (TextBox10.Text != "") tmpDN = TextBox10.Text; ;

        string Ret1 = GetDNFromSap(tdate, TextBox12.Text, MSSCMDIR, tmpDN);
    }

    private string GetDNFromSap(string tdate, string Rtype, string tmpMSSCMDIR, string tmpDN)
    {

            
        string tmpdate = "";
        if ( tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype   =  "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime   = tmpdate + "235959";  // Session["Param9"].ToString();

     
        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";
  
        string bdate = tmpdate,  edate = tmpdate;
        string LastDay3 = (DateTime.Today.AddDays(-2)).ToString("yyyyMMdd");
        string LastDay1 = (DateTime.Today.AddDays(-0)).ToString("yyyyMMdd");
        string readdb = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";

     // string mes = NokFoxlib1.GetSapDN("83138437", "TEST", SqlWebDBAB2B, LastDay3, LastDay1, "IMSCMWS");
     // 20170603   string mes = NokFoxlib1.GetSapDN(tmpDN, Runtype, SqlWebDBAB2B, LastDay3, LastDay1, tmpMSSCMDIR);//Get 3B2 from SAP --205  

        string Ret1 = GetSapDNByGroup(tmpDN, Runtype, SqlWebDBAB2B, LastDay3, LastDay1, tmpMSSCMDIR);
        return( Ret1 ); 

}

    protected string GetSapDNByGroup( string tmpDN, string Runtype, string SqlWebDBAB2B, string LastDay3, string LastDay1, string tmpMSSCMDIR )
    {
        DataSet DN1 = null;
        // DN1 = PubLib1Pointer.GetDataTable(DbSql, SqlWebDBAB2B, " select * from FGISM1001 where F1 = 'NokFoxM2002'  ");
        DN1 = PDataBaseOperation.PSelectSQLDS(DbSql, SqlWebDBAB2B, " select * from FGISM1001 where F1 = 'NokFoxM2002'  ");
        if (DN1.Tables.Count <= 0) return (" Table not found in PO_CREATE_DT ");
        int DN1Cnt1 = DN1.Tables[0].Rows.Count;
        if (DN1Cnt1 == 0) return ("No Data 0");

        string t1 = "", t2 = "", t3 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "";
        int v1=0, v2=0;
        for ( v1 = 0; v1 < DN1.Tables[0].Rows.Count; v1++)
        {
            t1 = DN1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // Sales Org
            t2 = DN1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // Sap Factory

            string t6 = DN1.Tables[0].Rows[v1]["F6"].ToString().Trim();   // 1000253
            string t10 = DN1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 1,4
            string t12 = DN1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // TEST REAL
            string t14 = DN1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // factory status
            string t16 = DN1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // factory status
            string t18 = DN1.Tables[0].Rows[v1]["F18"].ToString().Trim();  // B2B IP
            string t20 = DN1.Tables[0].Rows[v1]["F20"].ToString().Trim();  // SFC IP 
            string t26 = DN1.Tables[0].Rows[v1]["F26"].ToString().Trim();  // B2B Second IP
            string t28 = DN1.Tables[0].Rows[v1]["F28"].ToString().Trim();  // SFC Second IP

            //tparam = Session["NokRunEnv"].ToString().Trim();//  "TEST";
            //SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", Session["NokB2BIP"].ToString().Trim());
            //RunORACLEDB = ReadParaTxt("WebReadParam.txt", Session["NokSFCIP"].ToString().Trim());

            tmp1 = ReadParaTxt("WebReadParam.txt", t18.ToString().Trim());
            tmp2 = ReadParaTxt("WebReadParam.txt", t20.ToString().Trim());

              NokFoxlib1.GetSapDN(tmpDN, t12,     tmp1,      LastDay3, LastDay1, tmpMSSCMDIR, t1, t2);//Get 3B2 from SAP -
   // 1 times NokFoxlib1.GetSapDN(tmpDN, Runtype, SqlWebDBAB2B, LastDay3, LastDay1, tmpMSSCMDIR, t1, t2);//Get 3B2 from SAP --205
        }

        return( v1.ToString().Trim() );

    }

    protected string GetSFCdnConfirmdnByGroup(string DbSql, string SqlWebDBAB2B, string sfcread, string Runtype, string tmpMSSCMDIR, string tmpdate, string tmpDN)
    {
        DataSet DN1 = null;
        // DN1 = PubLib1Pointer.GetDataTable(DbSql, SqlWebDBAB2B, " select * from FGISM1001 where F1 = 'NokFoxM2002'  ");
        DN1 = PDataBaseOperation.PSelectSQLDS(DbSql, SqlWebDBAB2B, " select * from FGISM1001 where F1 = 'NokFoxM2002'  ");
        if (DN1.Tables.Count <= 0) return (" Table not found in PO_CREATE_DT ");
        int DN1Cnt1 = DN1.Tables[0].Rows.Count;
        if (DN1Cnt1 == 0) return ("No Data 0");

        string t1 = "", t2 = "", t3 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "";
        int v1 = 0, v2 = 0;
        for (v1 = 0; v1 < DN1.Tables[0].Rows.Count; v1++)
        {
            t1 = DN1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // Sales Org
            t2 = DN1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // Sap Factory

            string t6  = DN1.Tables[0].Rows[v1]["F6"].ToString().Trim();   // 1000253
            string t10 = DN1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 1,4
            string t12 = DN1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // TEST REAL
            string t14 = DN1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // factory status
            string t16 = DN1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // factory status
            string t18 = DN1.Tables[0].Rows[v1]["F18"].ToString().Trim();  // B2B IP
            string t20 = DN1.Tables[0].Rows[v1]["F20"].ToString().Trim();  // SFC IP 
            string t26 = DN1.Tables[0].Rows[v1]["F26"].ToString().Trim();  // B2B Second IP
            string t28 = DN1.Tables[0].Rows[v1]["F28"].ToString().Trim();  // SFC Second IP
           
            //tparam = Session["NokRunEnv"].ToString().Trim();//  "TEST";
            //SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", Session["NokB2BIP"].ToString().Trim());
            //RunORACLEDB = ReadParaTxt("WebReadParam.txt", Session["NokSFCIP"].ToString().Trim());
       
            tmp1 = ReadParaTxt("WebReadParam.txt", t18.ToString().Trim());
            tmp2 = ReadParaTxt("WebReadParam.txt", t20.ToString().Trim());


            NokFoxlib2Pointer.GetSFCdnConfirmdn(DbSql, tmp1,      tmp2,    t12,     tmpMSSCMDIR, tmpdate, tmpDN); //抓取SFC 出貨資訊
         // NokFoxlib2Pointer.GetSFCdnConfirmdn(DbSql, SqlWebDBAB2B, sfcread, Runtype, tmpMSSCMDIR, tmpdate, tmpDN); //抓取SFC 出貨資訊
        }

        return (v1.ToString().Trim());

    }
    protected void Button71_Click(object sender, EventArgs e)
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");

        string t1="",t2="", t3="", t4="", t5;

        if (Session["Nokusersite"] != null)
            t1 = Session["Nokusersite"].ToString().Trim();

        if (Session["Nokfactoryno"] != null)
            t2 = Session["Nokfactoryno"].ToString().Trim();

        if (Session["Nokcstatus"] != null)
            t3 = Session["Nokcstatus"].ToString().Trim();

        if (Session["NokRunEnv"] != null)
            t4 = Session["NokRunEnv"].ToString().Trim();

        t5 = Session["NokVendID"].ToString().Trim(); // = DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
        t5 = Session["Nokcstatus"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // 1,4
        t5 = Session["NokRunEnv"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();  // TEST REAL
        t5 = Session["NokSalesOrg"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F14"].ToString().Trim();  // factory status
        t5 = Session["NokSapPlant"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F16"].ToString().Trim();  // factory status
        t5 = Session["NokB2BIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F18"].ToString().Trim();  // factory status
        t5 = Session["NokSFCIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F20"].ToString().Trim();  // factory status
        t5 = Session["CURRNokB2BIP"].ToString().Trim();  // Curr B2B IP
        t5 = Session["CURRNokB2BDBType"].ToString().Trim(); //  DBA Type sql
  
        string Ret1 = DNUpdatePO(tdate, TextBox12.Text, MSSCMDIR);
    }

    private string DNUpdatePO(string tdate, string Rtype, string tmpMSSCMDIR)
    {
      
        string tmpdate = "";
        if ( tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype   =  "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime   = tmpdate + "235959";  // Session["Param9"].ToString();

     
        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";

        arrMsg[1, 1] = DbSql;
        arrMsg[1, 2] = SqlWebDBAB2B;
        arrMsg[1, 3] = SqlWebDBAB2B;
        arrMsg[1, 4] = DbSql;
        arrMsg[1, 5] = DbSql;

        NokFoxlibPointer.DNUpdPO(DbSql, SqlWebDBAB2B, SqlWebDBAB2B, tmpMSSCMDIR, arrMsg);

        // DN  DNUpdPO(DbSql, SqlWebDBAB2B, tmpMSSCMDIR, arrMsg );
        //FGISlib5 NokScnRevPo = new FGISlib5();

        //string Ret1 = NokScnRevPo.GetPoHeader(DbSql, SqlWebDBAB2B, tmpMSSCMDIR, Starttime, Endtime);
        // string Starttime = Session["Param8"].ToString();
        // string Endtime = Session["Param9"].ToString();
        // NokScnRevPo.GetPoHeader(DbSql, SqlWebDBAB2B, MSSCMDIR, Starttime, Endtime);
        return ("");
      
    }
    protected void Button72_Click(object sender, EventArgs e)
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");

        string tmpPO = "", tmpDN = "";
        if (TextBox10.Text != "") tmpDN = TextBox10.Text;
        if (TextBox13.Text != "") tmpPO = TextBox13.Text;
        string Ret1 = Chk3B2SFC(tdate, TextBox12.Text, MSSCMDIR, tmpPO, tmpDN);
    }

    private string Chk3B2SFC(string tdate, string Rtype, string tmpMSSCMDIR, string tmpPO, string tmpDN)
    {
      
        string tmpdate = "";
        if ( tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype   =  "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime   = tmpdate + "235959";  // Session["Param9"].ToString();


        string tmpReadDBA = "", tmpWriDBA = "", tVendid = "", tRuntype="";

        string t1 = "", t2 = "", t3 = "", t4 = "";
        string st4 = FGISlibPointer.SetupPSarray("GETSNBYUSER", TextBox1.Text, ref arrB2BPS, PSB2BDBA, ref t1, ref t2, ref t3, ref t4);
        if ((st4 != "") && (st4 != "0"))
        {
            int arrcnt = Convert.ToInt32(st4);
            if (arrcnt > 1)
            {
                tmpReadDBA = TrsSecondIP("WebReadParam.txt", arrB2BPS[arrcnt, 19].ToString().Trim());
                tmpWriDBA  = TrsSecondIP("WebReadParam.txt", arrB2BPS[arrcnt, 19].ToString().Trim());
                tVendid    = arrB2BPS[arrcnt, 0].ToString().Trim();
                tRuntype   = arrB2BPS[arrcnt, 5].ToString().Trim();                
                Session["NokVendID"] = tVendid.ToString().Trim();
                // Runtype    = 
                t4 = arrB2BPS[arrcnt, 2].ToString().Trim(); // ReadParaTxt("WebReadParam.txt", arrPS[arrcnt, 20].ToString().Trim());
            }
        }


        if ((tmpReadDBA == "") || (tmpWriDBA == "") || (Session["NokVendID"] == null))
        {
            Response.Write("<script>alert('Your User + Password fault , Pls ! Re-Entry User + Password  !!')</script>");
            return("");
        }


        if (tmpReadDBA.ToString().Trim() != TextBox15.Text.ToString().Trim())
        {
            Response.Write("<script>alert('Your User + Password Text != Static Variable , Pls ! Re-Entry User + Password  !!')</script>");
        }
        else
            TextBox6.Text = "tmpReadDBA = TextBox15 :" + TextBox15.Text.ToString(); 

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = tmpReadDBA; //  SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = tmpWriDBA;  // SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = tRuntype;   // Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";


        //Chk3B2SFC()
        // string sfcread = ReadParaTxt("WebReadParam.txt", "23705");  // Test 
        // if ( Runtype.ToUpper() == "REAL" ) sfcread = ReadParaTxt("WebReadParam.txt", "23704");  // REAL  
        //NokFoxlib2 NokScmAuto = new NokFoxlib2();

        string sfccode = Session["NokSFCIP"].ToString(); //sfc ip code 
        string sfcread = ReadParaTxt("WebReadParam.txt", sfccode);  // sfc ipsytring

        NokFoxlib2Pointer.GetSFCdnConfirmdn(DbSql, SqlWebDBAB2B, sfcread, Runtype, tmpMSSCMDIR, tmpdate, tmpDN); //抓取SFC 出貨資訊
        NokFoxlib2Pointer.checkqty(DbSql, SqlWebDBAB2B, Runtype, tmpMSSCMDIR, tmpdate, tmpDN);  //檢查出貨數量是否與接近來的PO數量相符
        
     
        return ( "");
    }

    public void ReaddDRAFromText(string SiteName)
    {
        //static string OraL10DBAMain  = "";
        // static string OraL10DBAStandBy = "";
        //static string OraL6DBAMain = "";
        //static string OraL6DBAStandBy = "";
        //static string OraWebCTLDBA = "";
        //static string SqlWebCTLDBA = "";

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", t7 = "";


        if (SiteName == "TJSFC")
        {
            t1 = ReadParaTxt("WebReadParam.txt", "23101");  // procode
            t2 = ReadParaTxt("WebReadParam.txt", "23201");
            t3 = ReadParaTxt("WebReadParam.txt", "23202");
            t4 = ReadParaTxt("WebReadParam.txt", "23203");
            t5 = ReadParaTxt("WebReadParam.txt", "23204");
            t6 = ReadParaTxt("WebReadParam.txt", "23205");
            t7 = ReadParaTxt("WebReadParam.txt", "23206");

            if (t1 == "") return;

            if (t2 != "") OraL10DBAMain = ConvertlibPointer.DeEncCodeWithoutEclcode(t2, t1, ",", "2DBA");
            if (t3 != "") OraL10DBAStandBy = ConvertlibPointer.DeEncCodeWithoutEclcode(t3, t1, ",", "2DBA");
            if (t4 != "") OraL6DBAMain = ConvertlibPointer.DeEncCodeWithoutEclcode(t4, t1, ",", "2DBA");
            if (t5 != "") OraL6DBAStandBy = ConvertlibPointer.DeEncCodeWithoutEclcode(t5, t1, ",", "2DBA");
            if (t6 != "") OraWebCTLDBA = ConvertlibPointer.DeEncCodeWithoutEclcode(t6, t1, ",", "2DBA");
            if (t7 != "") SqlWebCTLDBA = ConvertlibPointer.DeEncCodeWithoutEclcode(t7, t1, ",", "2DBA");

        }
    }

    public void ReaddDRAFromwebconfig(string SiteName)
    {

        if (SiteName == "TJSFC")
        {
            OraL10DBAMain = ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraL10NormalConnStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
            OraL10DBAStandBy = ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraL8StandByConnStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
            // OraL6DBAMain = ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraL10NormalConnStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
            OraWebCTLDBA = ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["OraWebConnectionStringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
            SqlWebCTLDBA = ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["Sql221StringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");

        }

    }  // end ReaddDRAFromwebconfig(string SiteName)


    public void GetTextFile(ref string[] ReadTxtArray, int ArrMax, string dtype)
    {
        string retPara = "";
        //  int ArrMax = 200;
        //  string[] ReadTxtArray = new string[ArrMax];
        //  string[] ProtTxtArray = new string[ArrMax];
        //  string[] CProtTxtArray = new string[ArrMax];

        //  string DBString = "", ProDBSTring = "", PCode = "", ConString = "", PStrSpilt = "",

        string InString = "", PCode = "", PStrSpilt = ",";
        string t1 = "";
        int v1 = 0;
        string FileName = "WebReadParam.txt";
        string FilePara = "", ParaNum = "License Document By IT Team 20120131 Donot Copy and Delete this file";
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string EnCodeStr = "", DnCodeStr = "";
        int i = 0, strlen = 0, Chead = 5;
        PCode = ReadParaTxt("WebReadParam.txt", "23101"); // "License Document By IT Team 20120131 Donot Copy and Delete this file";


        while (((InString = sr.ReadLine()) != null) && (i < ArrMax))
        {
            if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
            {
                strlen = InString.Length - 1;
                t1 = InString.Substring(0, 5); // Pre 5 code 
                v1 = Convert.ToInt32(t1);
                if ((v1 >= 23201) && (v1 <= 23999) && (dtype == "2"))
                {
                    retPara = InString.Substring(6, strlen - 5);
                    EnCodeStr = InString.Substring(Chead + 1, strlen - Chead);
                    // DnCodeStr = ConvertlibPointer.DeEncCode(EnCodeStr, PCode, PStrSpilt, "");
                    DnCodeStr = LibUSR1Pointer.DBDeEncCode(EnCodeStr, PCode, ",", "2DBA");
                    ReadTxtArray[i] = InString.Substring(0, 6) + DnCodeStr;

                    //  i = ArrMax;  // Break
                }
                else
                    ReadTxtArray[i] = InString;

            }
            i++;

        }

        sr.Close();



    }  // end GetTextFile

    protected void Button73_Click(object sender, EventArgs e)
    {
        //return;
        //string rps = TextBox5.Text;
        //string st1 = "";
        //if (((TextBox1.Text.ToLower() == "web") && (TextBox2.Text.ToLower() == "web")) ||
        //     ((TextBox1.Text.ToLower() == "zzsfc") && (TextBox2.Text.ToLower() == "zzsfc"))) st1 = "Y"; 
        //if (st1 == "")
        //{
        //    Response.Write("<script>alert('You need entry Uersname and Password ! ')</script>");
        //    return;
        //}

        ////Timer1.Enabled = false;
        //TextBox6.Text = "OrgDeLink Proc";
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraL10DBAStandBy; //  RunORACLEDB; // ConfigurationManager.AppSettings["L8StandByConnectionString"];  // string tSUsername = Session["DBReadString"].ToString();
        //string tmp2 = OraL10DBAStandBy; //  RunORACLEDB; // ConfigurationManager.AppSettings["L8TestandWebConnectionString"]; // NormalDbConnectionString"];  // 76 string tSUsername = Session["DBReadString"].ToString();

        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = tmp2; // DBReadString  197
        //Session["Param4"] = tmp2; // DBReadString  197
        //Session["Param5"] = "menu";   // Menu input
        //Response.Write("<script>window.open( 'MainMotPrg/R15R16Rework.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");


    }

    // SFC call Tiptop
    // ReqType : "1": Day 20130626, "2" DN :/EM1-D60002 "3" WO_NO WM1-D60002, L6 "4" WO_NO L10, "5"; ReqData : 20130626/WM1-D60002/EM1-D60002 
    // Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_ALL("tjsfc", Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData); 
    protected void Button74_Click(object sender, EventArgs e)
    {
        //return;
        //string st1 = "";
        //if (((TextBox1.Text.ToLower() == "web") && (TextBox2.Text.ToLower() == "web")) ||
        //     ((TextBox1.Text.ToLower() == "zzsfc") && (TextBox2.Text.ToLower() == "zzsfc"))) st1 = "Y";
        //if (st1 == "")
        //{
        //    Response.Write("<script>alert('You need entry Uersname and Password ! ')</script>");
        //    return;
        //}

        //if ( ( TextBox7.Text == "" ) || ( TextBox8.Text == "" ) )
        //{
        //    Response.Write("<script>alert('You need entry Number ITEM 2 ! ')</script>");
        //    return;
        //}

        //// "1": Day 20130626, "2" DN :  "3" WO_NO WM1-D60002  "4" , "5";
        //// if ((TextBox7.Text != "1") && (TextBox7.Text != "2") && (TextBox7.Text != "3") && (TextBox7.Text != "4") && (TextBox7.Text != "5"))
        //if ( (TextBox7.Text != "2") && (TextBox7.Text != "3") && (TextBox7.Text != "4") ) 
        //{
        //    Response.Write("<script>alert('You need entry Number ITEM 4 ! ')</script>");
        //    return;
        //}

        //// 20130726 改不刪除 Sap 原有 data CallTiptoptype = "C"  改  CallTiptoptype = "I";
        //CallTiptoptype = "I"; // one way toacket
        //string Dtype = "I", SysDate = DateTime.Today.ToString("yyyyMMdd"), DBType = "oracle";
        //string DBReadString = ReadParaTxt("WebReadParam.txt", "23301");    // L10
        //string DBWriString = ReadParaTxt("WebReadParam.txt", "23301");     // L10
        //string DBWriStringL6 = ReadParaTxt("WebReadParam.txt", "23303");   // L6
        //string PCode = ReadParaTxt("WebReadParam.txt", "23101");
        //Session["ZZPCode"] = ReadParaTxt("WebReadParam.txt", "23101");
        //Session["ZZPSite"] = "tjsfc";
        ////DBReadString = ConfigurationManager.AppSettings["OraZZL10DBConnectionString"]; 
        ////DBWriString  = ConfigurationManager.AppSettings["OraZZL10DBConnectionString"];
        ////string Retstr1 = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P0_3("1", "oracle", DBReadString, DBWriString, "1");  
        //if (CallTiptoptype != "") Dtype = CallTiptoptype;
        //string Retstr = "";
        //string BSite = PPSite;

        //// Test C 
        ////Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P0_2("tjsfc", Dtype, SysDate, DBType, DBReadString, DBWriString, PCode);
        ////Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P02_2(BSite, Dtype, SysDate, DBType, DBReadString, DBWriStringL6, PCode);  // Move MOCTAfrom Buffer to L6 SFC  
        ////return (""); 
        //// end Test

        //// ReqType  : "1": Day 20130626, "2" DN :/EM1-D60002   "3" WO_NO WM1-D60002, L6  "4" WO_NO  L10, "5";  ReqData  : 20130626/WM1-D60002/EM1-D60002 
        //string  ReqType = TextBox7.Text;
        //string  ReqData = TextBox8.Text;
        //Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_ALL("tjsfc", Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);

        //switch (ReqType)
        //{
        //    //    case "1":    // Day
        //    //       Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P02_2(BSite, Dtype, SysDate, DBType, DBReadString, DBWriStringL6, PCode);  // Move MOCTAfrom Buffer to L6 SFC  
        //    //       Retstr = SFCLinkTiptopPointer.T_ZQM_HEADE_STRU_2(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode);     // WO_LIST Move from Buffer to SFC  
        //    //       Retstr = SFCLinkTiptopPointer.T_ZQM_COMPONENT_STRU_2(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode); // WO_COMP Move from Buffer to SFC
        //    //       Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P0_2("tjsfc", Dtype, SysDate, DBType, DBReadString, DBWriString, PCode);   // PACK Move from Buffer to SFC
        //    //       break;
        //        case "2":    // DN
        //           Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P0_2("tjsfc", Dtype, SysDate, DBType, DBReadString, DBWriString, PCode);   // PACK Move from Buffer to SFC             
        //           break;

        //        case "3":    // L6 WO
        //           Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P02_2(BSite, Dtype, SysDate, DBType, DBReadString, DBWriStringL6, PCode);  // Move MOCTAfrom Buffer to L6 SFC  
        //           break;

        //        case "4":    // L10 WO
        //           Retstr = SFCLinkTiptopPointer.T_ZQM_HEADE_STRU_2(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode);     // WO_LIST Move from Buffer to SFC  
        //           Retstr = SFCLinkTiptopPointer.T_ZQM_COMPONENT_STRU_2(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode); // WO_COMP Move from Buffer to SFC
        //           Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_P0_2( BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode);   // PACK Move from Buffer to SFC
        //           break;

        //        default:
        //             break;
        //}

        //CallTiptoptype = "";     

    }

    // Test Only
    protected void Button75_Click(object sender, EventArgs e)
    {
        //if (TextBox5.Text != "Foxconn88")
        //{
        //    Response.Write("<script>alert('You password is not correct Reentry password again !!')</script>");
        //    return;

        //}


        ////Timer1.Enabled = false;
        //string t2 = "", t3 = "";

        //int v1=0, v2=0, v3=0, v4=0, v5=0;
        //string tmpdate = "", tmpdate1 = "", tmpdate2 = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 

        //string tmpDN = TextBox10.Text.ToString().Trim();

        //if (tmpDN == "") return;

        //string sqlr = "  SELECT  * from  " + MSSCMDIR + ".[dbo].Delivery_Notification_MT where  DNID  = '" + tmpDN + "' ";
        //DataSet DNdt = PDataBaseOperation.PSelectSQLDS(DbSql, SqlWebDBAB2B, sqlr);
        //if (DNdt == null) return;     // Syn Error
        //int DNCnt1 = DNdt.Tables[0].Rows.Count;
        //if (DNCnt1 == 0)
        //{
        //    TextBox6.Text = "No this DN ";
        //    Response.Write("<script>alert('DN can not been CONFIRM or SEND to Customer !!')</script>");
        //    return;     // Not Data
        //}
        //else
        //{
        //    t2 = DNdt.Tables[0].Rows[v1]["CONFIRMFLAG"].ToString().Trim();
        //    t3 = DNdt.Tables[0].Rows[v1]["SENDFLAG"].ToString().Trim();
        //}

        //if (t2 == "Y")
        //{
        //    Response.Write("<script>alert('DN can not been clear because already CONFIRM !!')</script>");
        //    return;     // Not Data
        //}
        //else
        //if (t3 == "Y")
        //{
        //    Response.Write("<script>alert('DN can not been clear because already SEND !!')</script>");
        //    return;     // Not Data
        //}


        //string DBType = DbSql;
        //string DBReadString = SqlWebDBAB2B;
        //string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = ""; ;

        //sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Notification_HU where DNID = '" + tmpDN + "' ";
        //v1 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Notification_DT where DNID = '" + tmpDN + "' ";
        //v2 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Map_DN          where DNID = '" + tmpDN + "' ";
        //v3 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //sqlr = "  Update   " + MSSCMDIR + ".[dbo].Delivery_Notification_MT set SFCFLAG = ''  where DNID = '" + tmpDN + "' ";
        //v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);

        //Response.Write("<script>alert('DN been clear Data !!')</script>");

    }

    protected void Button76_Click(object sender, EventArgs e)
    {
        //return;
        ////Timer1.Enabled = false;
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraBBFUSEL10ReadDBA;     //  44
        //string tmp2 = OraBBFUSEL10WriDBA;      //  215
        //string tmpdate = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //string tmpLINE = "A1";
        //if (GPROLINE != "") tmpLINE = GPROLINE;

        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = tmp1; // DBReadString   
        //Session["Param4"] = tmp2; // DBWriteString  
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = tmpLINE;  // PROLINE 
        //Response.Write("<script>window.open( 'MainGooPrg/JGECC1.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    protected void Button77_Click(object sender, EventArgs e)
    {
        //return;
        ////Timer1.Enabled = false;
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraBBFUSEL10WriDBA; // 215
        //string tmp2 = OraBBFUSEL10WriDBA; // 215
        //string tmpdate = "";
        //string tmpLINE = "A1";

        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //if (GPROLINE != "") tmpLINE = GPROLINE;

        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = tmpLINE;  // PROLINE 
        //Response.Write("<script>window.open( 'MainGooPrg/JGECQ1.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }


    protected void Button78_Click(object sender, EventArgs e)
    {
        //return;
        ////Timer1.Enabled = false;
        //string tmpLINE = "A1";
        //if (GPROLINE != "") tmpLINE = GPROLINE; 
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraBBFUSEL10WriDBA;  // 215
        //string tmp2 = OraBBFUSEL10WriDBA;  // 215
        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = tmp1; // DBReadString   44
        //Session["Param4"] = tmp2; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = DateTime.Today.ToString("yyyyMMdd");
        //Session["Param7"] = tmpLINE;  // PROLINE 
        //Response.Write("<script>window.open( 'MainGooPrg/JGECQ2.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }
    protected void Button79_Click(object sender, EventArgs e)
    {
        //return;
        ////Timer1.Enabled = false;
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraBBFUSEL10WriDBA; // 215
        //string tmp2 = OraBBFUSEL10WriDBA; // 215
        //string tmpdate = "";
        //string tmpLINE = "A1";

        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //if (GPROLINE != "") tmpLINE = GPROLINE;

        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = tmp1; // DBReadString   44
        //Session["Param4"] = tmp2; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = tmpLINE;  // PROLINE 
        //Response.Write("<script>window.open( 'MainGooPrg/TCWM01Query.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");


    }
    protected void Button80_Click(object sender, EventArgs e)
    {
        //return;
        ////Timer1.Enabled = false;
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraBBFUSEL10WriDBA; // 215
        //string tmp2 = OraBBFUSEL10WriDBA; // 215
        //string tmpdate = "";
        //string tmpLINE = "A1";

        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //if (GPROLINE != "") tmpLINE = GPROLINE;

        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = tmp1; // DBReadString   44
        //Session["Param4"] = tmp2; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = tmpLINE;  // PROLINE 
        //Response.Write("<script>window.open( 'MainGooPrg/TCWD01Query.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }
    protected void Button81_Click(object sender, EventArgs e)
    {
        //return;
        ////Timer1.Enabled = false;
        //// if (FPassword.ToLower() == "web")
        //string tmp1 = OraBBFUSEL10WriDBA; // 215
        //string tmp2 = OraBBFUSEL10WriDBA; // 215
        //string tmpdate = "";
        //string tmpLINE = "A1";

        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //if (GPROLINE != "") tmpLINE = GPROLINE;

        //Session["Param1"] = 1;
        //Session["Param2"] = Dboracle; // DBReadString
        //Session["Param3"] = tmp1; // DBReadString   44
        //Session["Param4"] = tmp2; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = tmpLINE;  // PROLINE 
        //Response.Write("<script>window.open( 'MainGooPrg/TCWD02Query.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }
    protected void TextBox8_TextChanged(object sender, EventArgs e)
    {
        return;
        string tdate = TextBox8.Text;
        string Wridir = "PUBLIB";
        string sqlr = " select *  from " + Wridir + ".TCWM01  where DATES = '" + tdate + "' ORDER by LINE, CLASS1 asc ";
        DataSet DNdt = PDataBaseOperation.PSelectSQLDS(Dboracle, OraBBFUSEL10WriDBA, sqlr);
        if (DNdt == null) return;
        if (DNdt.Tables.Count <= 0) return;
        if (DNdt.Tables[0].Rows.Count <= 0) return;
        GPROLINE = DNdt.Tables[0].Rows[0]["LINE"].ToString().Trim();

    }

    //  Clear one day data
    protected void Button82_Click(object sender, EventArgs e)
    {
        //return;
        //if (TextBox2.Text.ToLower() != "foxconn88")
        //{
        //    Response.Write("<script>alert('You need entry password ! ')</script>");
        //    return;
        //}

        //string tmp1 = CurrDay, tWridir = "PUBLIB", tmpsqlW = "";
        //int v5 = 0, v6 = 0;

        //if (TextBox8.Text != "") tmp1 = TextBox8.Text;

        //tmpsqlW = " delete " + tWridir + ".TCWM01  where DATES = '" + tmp1 + "' ";
        //v5 = PDataBaseOperation.PExecSQL(Dboracle, OraBBFUSEL10WriDBA, tmpsqlW);
        //tmpsqlW = " delete " + tWridir + ".TCWD01  where DATES = '" + tmp1 + "' ";
        //v6 = PDataBaseOperation.PExecSQL(Dboracle, OraBBFUSEL10WriDBA, tmpsqlW);

        //if ( ( v5 > 0 ) && (  v6 > 0 ) )
        //{
        //    Response.Write("<script>alert('You clear k-ban data finish ! ')</script>");
        //    return;
        //}

    }
    protected void Button83_Click(object sender, EventArgs e)
    {
        //if (TextBox9.Text == "") return;


        //string tmpSite = TextBox9.Text.ToUpper().ToString().Trim();

        //if (tmpSite == "BJ")
        //{
        //    FoxconnSite = "BJ"; // BBRY FoxconnSite
        //    SqlWebDBAB2B   = ReadParaTxt("WebReadParam.txt", "23524");  // Get Data 
        //    Response.Write("<script>alert('You currency directory is BJ !!')</script>");
        //}

        //if ( tmpSite == "LF" )
        //{
        //    FoxconnSite = "LF"; // BBRY FoxconnSite
        //    SqlWebDBAB2B = ReadParaTxt("WebReadParam.txt", "23702");  // Get Data 
        //    Response.Write("<script>alert('You currency directory is LF !!')</script>");
        //}


    }

    protected void Button85_Click(object sender, EventArgs e)
    {
        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");
        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/DnnReport.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
    }


    protected void Button86_Click(object sender, EventArgs e)
    {
        ////Timer1.Enabled = false;

        //string tmpdate = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 
        //Response.Write("<script>window.open( 'MainMSPrg/FDNQ01.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");


    }

    protected void Button88_Click(object sender, EventArgs e)
    {
        ////Timer1.Enabled = false;

        //string tmpdate = "";
        //if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        //else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        //Session["Param1"] = 1;
        //Session["Param2"] = DbSql; // DBReadString
        //Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        //Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        //Session["Param5"] = "menu";   // Menu input
        //Session["Param6"] = tmpdate;
        //Session["Param7"] = MSSCMDIR;  // PROLINE 
        //Response.Write("<script>window.open( 'MainMSPrg/FDLABI1.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   44
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Response.Write("<script>window.open( 'MainMSPrg/ChangePassword.aspx','','width=500,height=500,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
    }

    // Get PO from Web Service and write to MSSCMDIR
    protected void Button62_Click1(object sender, EventArgs e)
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");

        string tmpPO = "";
        if ( TextBox13.Text != "") tmpPO = TextBox13.Text;
        string Ret1 = PORecv(tdate, TextBox12.Text, MSSCMDIR, tmpPO);
    }

    private string PORecv(string tdate, string Rtype, string tmpMSSCMDIR, string tmpPO)
    {
      

        string tmpdate = "";
        if ( tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype   =  "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime   = tmpdate + "235959";  // Session["Param9"].ToString();

     
        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";

        // FGISlib5 NokScnRevPo = new FGISlib5();
        string db156 = ReadParaTxt("WebReadParam.txt", "24301");

        //string Ret1 = NokFoxlib5Pointer.GetPoHeader(Dboracle, db156, DbSql, SqlWebDBAB2B, tmpMSSCMDIR, Starttime, Endtime, tmpPO, TextBox12.Text);  // 
        //string Ret2 = NokFoxlib5Pointer.FeedBackACK(Dboracle, db156, DbSql, SqlWebDBAB2B, MSSCMDIR, Starttime, Endtime, tmpPO, TextBox12.Text);

        

        // 20170612
        string t5 = "", t6 = "", t7 = "", t8 = "";
        if (Session["NokVendID"] != null)
        {
            t5 = Session["NokVendID"].ToString().Trim();   // 1000253
        }
        if (Session["NokRunEnv"] != null)
        {
            t6 = Session["NokRunEnv"].ToString().Trim();   // DB Test or Real
        }
        if (Session["NokUrlEnv"] != null)
        {
            t7 = Session["NokUrlEnv"].ToString().Trim();   // Test、 Real or UAT
        }
        if (Session["NokDataBomID"] != null)
        {
            t8 = Session["NokDataBomID"].ToString().Trim();   // SB2B1002
        }

        if ((t5 == "") || (t7 == ""))
            return ("");
        // Rec PO
        string Ret1 = NokFoxlib5Pointer.GetPoHeader(Dboracle, db156, DbSql, SqlWebDBAB2B, tmpMSSCMDIR, Starttime, Endtime, tmpPO, t7, t5);
        // PO Ack 
        string Ret2 = NokFoxlib5Pointer.FeedBackACK(Dboracle, db156, DbSql, SqlWebDBAB2B, MSSCMDIR, Starttime, Endtime, tmpPO, t7, t5);       

        return (Ret1);
      
    }

    // Get 3B2 
    protected void Button92_Click(object sender, EventArgs e)
    {
        string tmpdate = "";
        if (TextBox8.Text != "") tmpdate = TextBox8.Text;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype = TextBox12.Text;  // TEST, REAL
        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime = tmpdate + "235959";  // Session["Param9"].ToString();


        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = MSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";

        string bdate = tmpdate, edate = tmpdate;

        // string LastDay3 = (DateTime.Today.AddDays(-2)).ToString("yyyyMMdd");
        // string LastDay1 = (DateTime.Today.AddDays(-0)).ToString("yyyyMMdd");
        // string readdb = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";
        // string mes = GSFClib1.GetSapDN("", readdb, SqlWebDBAB2B, LastDay3, LastDay1);//Get 3B2 from SAP --205     

        Response.Write("<script>window.open( 'MainNokFoxPrg/ASNQuery.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
 

        //Response.Write("<script>window.open( 'MainNokFoxPrg/FMDN01.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");

    }

    // Automatic
    protected void Button93_Click(object sender, EventArgs e)
    {

        string t1 = auto1();
    }

    private string auto1()
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");

        string tmpPO = "";
        if ( TextBox13.Text != "") tmpPO = TextBox13.Text;
        string tmpDN = "";
        if (TextBox10.Text != "") tmpDN = TextBox10.Text;
      

        string Ret1 = Auto2(tdate, TextBox12.Text, MSSCMDIR, tmpPO, tmpDN);

        return("");
    }


    private string Auto2(string tdate, string Rtype, string tmpMSSCMDIR, string tmpPO, string tmpDN)
    {
        string tmpdate = "";
        if (tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype = "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime = tmpdate + "235959";  // Session["Param9"].ToString();


        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";

        string db156 = ReadParaTxt("WebReadParam.txt", "24301");

        // 20170613 Get PO
        //string Ret1 = NokFoxlib5Pointer.GetPoHeader( Dboracle, db156, DbSql, SqlWebDBAB2B, tmpMSSCMDIR, Starttime, Endtime, tmpPO, TextBox12.Text);  // 
        // PO Ack
        //string Ret2 = NokFoxlib5Pointer.FeedBackACK(Dboracle, db156, DbSql, SqlWebDBAB2B, MSSCMDIR, Starttime, Endtime, tmpPO, TextBox12.Text);
        // End PO

        string ret = GetPOByGroup(Dboracle, db156, DbSql, SqlWebDBAB2B, tmpMSSCMDIR, Starttime, Endtime, tmpPO, TextBox12.Text);

        // Get DN from Sap
      
        string bdate = tmpdate, edate = tmpdate;
        string LastDay3 = (DateTime.Today.AddDays(-2)).ToString("yyyyMMdd");
        string LastDay1 = (DateTime.Today.AddDays(-0)).ToString("yyyyMMdd");
        string readdb = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";

        // string mes = NokFoxlib1.GetSapDN("83138437", "TEST", SqlWebDBAB2B, LastDay3, LastDay1, "IMSCMWS");
        // string mes = NokFoxlib1.GetSapDN(tmpDN, Runtype, SqlWebDBAB2B, LastDay3, LastDay1, tmpMSSCMDIR,"","");//Get 3B2 from SAP --205 
        string mes = GetSapDNByGroup(tmpDN, Runtype, SqlWebDBAB2B, LastDay3, LastDay1, tmpMSSCMDIR);

        // Check 3B2
       
        //Chk3B2SFC()
        string sfcread = ReadParaTxt("WebReadParam.txt", "23705");  // Test 

        if (Runtype.ToUpper() == "REAL") sfcread = ReadParaTxt("WebReadParam.txt", "23704");  // REAL  
        //NokFoxlib2 NokScmAuto = new NokFoxlib2();

        string Ret11 = GetSFCdnConfirmdnByGroup(DbSql, SqlWebDBAB2B, sfcread, Runtype, tmpMSSCMDIR, tmpdate, tmpDN); //抓取SFC 出貨資訊
        // one time NokFoxlib2Pointer.GetSFCdnConfirmdn(DbSql, SqlWebDBAB2B, sfcread, Runtype, tmpMSSCMDIR, tmpdate, tmpDN); //抓取SFC 出貨資訊


        NokFoxlib2Pointer.checkqty(DbSql, SqlWebDBAB2B, Runtype, tmpMSSCMDIR, tmpdate, tmpDN);  //檢查出貨數量是否與接近來的PO數量相符

        // Send 3B2 XML
       
        NokFoxlib2Pointer.send3B2( Dboracle, db156, DbSql, SqlWebDBAB2B, Runtype, tmpMSSCMDIR, tmpdate, tmpDN, Runtype); 
       
        return("");
       
    }

    protected string GetPOByGroup(string Dboracle, string db156, string DbSql, string SqlWebDBAB2B, string DBName, string starttime, string endtime, string PO, string Envirment)
    {
        DataSet DN1 = null;
        DN1 = PDataBaseOperation.PSelectSQLDS(DbSql, SqlWebDBAB2B, " select * from FGISM1001 where F1 = 'NokFoxM2002'  ");
        if (DN1.Tables.Count <= 0) return (" Table not found in FGISM1001 ");
        int DN1Cnt1 = DN1.Tables[0].Rows.Count;
        if (DN1Cnt1 == 0) return ("No Data 0");
        string t1 = "", t2 = "", t3 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "";
        int v1 = 0, v2 = 0;
        for (v1 = 0; v1 < DN1.Tables[0].Rows.Count; v1++)
        {
            t1 = DN1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // Sales Org
            t2 = DN1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // Sap Factory
            string t6 = DN1.Tables[0].Rows[v1]["F6"].ToString().Trim();   // 1000253
            string t10 = DN1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 1,4
            string t12 = DN1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // TEST REAL
            string t14 = DN1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // factory status
            string t16 = DN1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // factory status
            string t18 = DN1.Tables[0].Rows[v1]["F18"].ToString().Trim();  // B2B IP
            string t20 = DN1.Tables[0].Rows[v1]["F20"].ToString().Trim();  // SFC IP 
            string t26 = DN1.Tables[0].Rows[v1]["F26"].ToString().Trim();  // B2B Second IP
            string t28 = DN1.Tables[0].Rows[v1]["F28"].ToString().Trim();  // SFC Second IP
            string t29 = DN1.Tables[0].Rows[v1]["F29"].ToString().Trim();  // Test Real UAT（Nokia Environment）
            string t30 = DN1.Tables[0].Rows[v1]["F30"].ToString().Trim();  // DataBomID
            tmp1 = ReadParaTxt("WebReadParam.txt", t18.ToString().Trim());
            tmp2 = ReadParaTxt("WebReadParam.txt", t20.ToString().Trim());

            if (t29.ToUpper() == "REAL")
            {
                // Rec PO
                string Ret1 = NokFoxlib5Pointer.GetPoHeader(Dboracle, db156, DbSql, tmp1, DBName, starttime, endtime, PO, t29, t6);  // 
                // PO Ack
                string Ret2 = NokFoxlib5Pointer.FeedBackACK(Dboracle, db156, DbSql, tmp1, DBName, starttime, endtime, PO, t29, t6);
            }            
        }
        return (v1.ToString().Trim());
    }

    protected void Button87_Click(object sender, EventArgs e)
    {
        string tdate = "";
        if (TextBox8.Text != "") tdate = TextBox8.Text;
        else tdate = DateTime.Today.ToString("yyyyMMdd");
        string tmpDN = "";
        if (TextBox10.Text != "") tmpDN = TextBox10.Text; ;

        string Ret1 = Send3B2(tdate, TextBox12.Text, MSSCMDIR, tmpDN);
    }

    private string Send3B2(string tdate, string Rtype, string tmpMSSCMDIR, string tmpDN)
    {
      
        string tmpdate = "";
        if ( tdate != "") tmpdate = tdate;
        else tmpdate = DateTime.Today.ToString("yyyyMMdd");

        string Runtype   =  "TEST"; // Test, REAL
        if (Rtype != "") Runtype = Rtype;

        string Starttime = tmpdate + "000001"; // Session["Param8"].ToString();
        string Endtime   = tmpdate + "235959";  // Session["Param9"].ToString();

     
        Session["Param1"] = 1;
        Session["Param2"] = DbSql; // DBReadString
        Session["Param3"] = SqlWebDBAB2B; // DBReadString   
        Session["Param4"] = SqlWebDBAB2B; // DBWriteString  
        Session["Param5"] = Runtype;  //  "TEST";   // TEST, REAL
        Session["Param6"] = tmpdate;
        Session["Param7"] = tmpMSSCMDIR;  // PROLINE 
        Session["Param8"] = Starttime; //  tmpdate + "000001";
        Session["Param9"] = Endtime;   //  tmpdate + "235959";

        string db156 = ReadParaTxt("WebReadParam.txt", "24301");

        NokFoxlib2Pointer.send3B2(Dboracle, db156, DbSql, SqlWebDBAB2B, Runtype, tmpMSSCMDIR, tmpdate, tmpDN, Runtype); 
       
        return( "" ); 

    }

    private string TrsSecondIP(string infile, string inpno)
    {
        string R1 = ReadParaTxt(infile, inpno); // ReadParaTxt("WebReadParam.txt", "40030"); 
        string R2 = ReadParaTxt(infile, R1);
        return (R2);
    }


}  // end public partial class Main ////// End all SubRoutine ////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
// Document : Session 傳參數string Session["Param1"] = 1; tSUsername = Session["Param1"].ToString(); 
//  Session["Param1"] = 1;         // Data Process I/O
//  Session["Param2"] = "oracle";  // 數據庫
//  Session["Param3"] = str1;      // DBReadString
//  Session["Param4"] = str2;      // DBWriString Dboracle, Dbsql 
//  Session["Param5"] = "auto";    // 自動或手動執行 Autoprg, Menuprg
// Session["Param1"]  = 1;  代表第 1 類表傳  Session["Param2"] 為讀資料庫位置
// Session["Param1"] = 1;  代表第 1 類表傳  Session["Param2"] 為讀資料庫位置   Session["Param3"] 為寫資料庫位置
// Session["Param1"] = 2;  代表第 2 類表傳  Session["Param2"] ? 
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Remaek
// There are two way to proect dba password and you can get by 2 way,
// 1.1 Put pass in web.config 
// passDBA = passDBA + ConfigurationManager.AppSettings["LicenseString"] =  ConfigurationManager.AppSettings["Sql221StringEC"]
// Convert program is ConvertlibPointer.DeEncCodeWithoutEclcode
// sample :string t3 = SqlWebCTLDBA; // ConvertlibPointer.DeEncCodeWithoutEclcode(ConfigurationManager.AppSettings["Sql221StringEC"], ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
// 1.2 Put passDBA in WebReadParam.txt  with protparamater =  "23101"
// protect way like item 1 and you can get by function call  ReaddDRAFromwebconfig( SiteName ) return all DBA param 
// GetTextFile( ref string[] ReadTxtArray, int ArrMax, string dtype  ) Read WebReadParam.txt and put in memory ReadTxtArray
// and convter code from  ( code >= 23201 )  && ( code <= 23999 ) by DnCodeStr = LibUSR1Pointer.DBDeEncCode(EnCodeStr, PCode, ",", "2DBA");
//
//
//