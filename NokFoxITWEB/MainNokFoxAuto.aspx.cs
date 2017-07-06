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
//using SCM.GSCMDKen;
// using Prilibrary.FsplitNew;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using SFC.TJWEB;

public partial class MainNokFoxAuto : System.Web.UI.Page
{
    static string NetSqlDB4 = ConfigurationManager.AppSettings["SqlZZWebString"];
    static string OraL10DBAMain = "";       // 76  Oracle Unix
    static string OraL10DBAStandBy = "";    // 211 Oracle Unix
    static string OraL6DBAMain = "";        // 214 Oracle Unix 
    static string OraL6DBAStandBy = "";     // 
    static string OraWebCTLDBA = "";        // 221 Oracle Window
    static string SqlWebCTLDBA = "";        // 221 Sql Windoq 
    string SiteName = "ZZSFC";
    static string Username, FPassword, LoginFlag, PSFlag = "N", Acttype = "C", CallTiptoptype = "I";
    public static string PPSite = "ZZSFC";

    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();

    GSFClib1 GSFClib1Pointer = new GSFClib1();

    static int autorunflag = 0, Aerrcnt = 0;
    static int Arunstatus = 0, ASysCount = 1;
    static int firstcnt = 0;
    static int Aautoctlflag = 1; // Auto Control
    static int AautoCount = 1;   // Auto Control
    protected string oraDba = "oracle";
    protected string TimeSlice = "20"; // TIme each 10 minute;
    protected string TimeBaseCNT = "10000"; // cnt + 10000 
    protected string SqlWebDBA = "";
    protected string SqlWebDBA1 = "";

    

    protected string Oradbtype = "oracle";
    protected string Sqldbtype = "sql";

    //static string MotStandDB = "L8StandByConnectionString";   
    string tmpstr = "";
    //static string ITSys = "R16";
    // protected string ProgramParam = "N";
    static int ArrMax = 300;
    static string[] ReadTxtArray = new string[ArrMax];
    protected void Page_Load(object sender, EventArgs e)
    {
        string p2 = ReadParaTxt("WebReadParam.txt", "23101");
        string p1 = SiteName.ToLower();
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string t22 = LibPDBA1Pointer.Gooinit_var(p1, p2);
        string r1 = "", t11 = "";
        int in1 = 0;
        SqlWebDBA  = ReadParaTxt("WebReadParam.txt", "23712");  // Test Data IM--205
        SqlWebDBA1 = ReadParaTxt("WebReadParam.txt", "23716");  // Test Data IM--93

        if (!Page.IsPostBack)
        {
            string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", t7 = "", t8 = "", t9 = "";
            TextBox1.Focus();
            TextBox4.Text = " TJ IDM Web Server http://10.186.19.108/GSFCWeb/MainGSFCITAuto.aspx?Autoflag=1 ";
            Timer1.Enabled = false;

            r1 = ReadParaTxt("WebReadParam.txt", "10001");
            //// Auto Only
            string Loc1 = "";
            GetTextFile(ref ReadTxtArray, ArrMax, "2");  // "1" Org, "2" Decode by Pcode "23101"                 
            
            NetSqlDB4 = SqlWebCTLDBA;

            if (Aautoctlflag == 1)
            {
                Button17.Text = "系統自動啟動 LF Site";
                TextBox4.Text = "系統自動啟動 LF Site" + ASysCount.ToString();
                firstcnt++;
                Aautoctlflag = Aautoctlflag;
                // t11 = FPubLibPointer.PubWri_MessLog("IDMAuto", ASysCount.ToString(), Aautorunflag.ToString(), Aautoctlflag.ToString(), Arunstatus.ToString(), "FirstTimeMainloop", "", "", "", NetSqlDB4, "sql");         // 10 Code
                // 20131107 tmpstr = MainLoop();
                //tmpstr = GetRawData();
                t1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                //tmpstr = GetRawData(t1);
                //t1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                //t1 = "201504010101010101";
                //t1 = "201506060830010101";
                //t1 = "201507110001010101";
                tmpstr = MainLoop(t1);
                //Timer1.Enabled = true;
            }

        }


        if (Aautoctlflag == 1)
        {
            //Timer1.Enabled = true;
            AautoCount++;
        }

        ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>window.opener=null;window.open('','_self');window.close();</script>"); 
    }
    protected string MainLoop(string tdate)
    {
        if (tdate == "") return (" no data");
        int i = 3;
        Timer1.Enabled = false;
        string tmptime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        TextBox4.Text = "系統自動啟動" + "ZZAutoMainLoop : " + ASysCount.ToString() + "次, " + tmptime;
        string CurrDay = DateTime.Today.ToString("yyyyMMdd");  // 


        DateTime Today1 = DateTime.Today;
        string LastDay3 = (Today1.AddDays(-2)).ToString("yyyyMMdd");
        string LastDay1 = (Today1.AddDays(-0)).ToString("yyyyMMdd");
        string readdb = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";

        string mes = GSFClib1.GetSapDN("", readdb, SqlWebDBA, LastDay3, LastDay1);//Get 3B2 from SAP --205
        string mes1 = GSFClib1.GetSap940("", readdb, SqlWebDBA, LastDay3, LastDay1);//Get 940 from SAP --205
        string mes2 = GSFClib1.GetSapDN("", readdb, SqlWebDBA1, LastDay3, LastDay1);//Get 3B2 from SAP --93
        string mes3 = GSFClib1.GetSap940("", readdb, SqlWebDBA1, LastDay3, LastDay1);//Get 940 from SAP --93

        return ("");

        DataSet DN1 = null;
        DN1 = PDataBaseOperation.PSelectSQLDS(Sqldbtype, SqlWebDBA, " select * from FGISM1001 where F1 = 'NokFoxM2002'  ");
        if (DN1.Tables.Count <= 0) return (" Table not found in PO_CREATE_DT ");
        int DN1Cnt1 = DN1.Tables[0].Rows.Count;
        if (DN1Cnt1 == 0) return ("No Data 0");

        string t1 = "", t2 = "", t3 = "";
        int v1 = 0, v2 = 0;
        for (v1 = 0; v1 < DN1.Tables[0].Rows.Count; v1++)
        {
            t1 = DN1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // Sales Org
            t2 = DN1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // Sap Factory

            // string mes = GSFClib1.GetSapDN("", readdb, SqlWebDBA, LastDay3, LastDay1);//Get 3B2 from SAP --205
            // string mes1 = GSFClib1.GetSap940("", readdb, SqlWebDBA, LastDay3, LastDay1);//Get 940 from SAP --205
            // string mes2 = GSFClib1.GetSapDN("", readdb, SqlWebDBA1, LastDay3, LastDay1);//Get 3B2 from SAP --93
            // string mes3 = GSFClib1.GetSap940("", readdb, SqlWebDBA1, LastDay3, LastDay1);//Get 940 from SAP --93

        }
       
        
        return ("");
    }
    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
        int ArrMax = 200;
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
            //if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
            //    ReadTxtArray[i] = InString;

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
    protected void Button1_Click(object sender, EventArgs e)
    {

    }


    protected void Button2_Click(object sender, EventArgs e)  // Modify 
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Encl String Algorithm : 
    // 1. Input string  default for (v1 = 0; v1 < 20; v1++) trschar[v1] = Convert.ToChar((v1*v1+47) % 255 );
    //    Put Password in this array from start
    // 2. encchar string default  encchar[v1] = Convert.ToChar((v1*v1+4703) % 255 );
    // 3. OutPut string  rchar = trschar[v1] ^= encchar[v1]; 
    //
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    protected void tvManuTree_SelectedNodeChanged(object sender, EventArgs e)
    {

    }


    //////////////////////////////////////////////////////////////////////////////////////
    //  1000  for 1 second,  //  60000 for 1 minute, 1800000 for 30 minute
    //  
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        string s1 = "";
        TextBox4.Text = "Timer Interrupt";
        ASysCount++;
        //Aautoctlflag = Aautoctlflag; //Auto only
        //string t1 = FPubLibPointer.PubWri_MessLog("IDMAuto", ASysCount.ToString(), Aautorunflag.ToString(), Aautoctlflag.ToString(), Arunstatus.ToString(), "TimerINT", "", "", "", NetSqlDB4, "sql");         // 10 Code

        // return; // not auto only

        // Aautoctlflag = Aautoctlflag;
        AautoCount++;
        if (Aautoctlflag == 1)  // boot automatic runnning
        {
            if (Arunstatus == 0) // go to running
            {
                //t1 = FPubLibPointer.PubWri_MessLog("IDMAuto", AautoCount.ToString(), Aautorunflag.ToString(), Aautoctlflag.ToString(), Arunstatus.ToString(), "TimeINTGOToMainloop", "", "", "", NetSqlDB4, "sql");  // 10 Code
                s1 = MainLoop("");
            }
            else
                Aerrcnt++;
        }

        if (Aerrcnt > 2)
        {
            Aerrcnt = 0;
            Arunstatus = 0;
        }

        return;
        // if ( ASysCount >= 1 )  // 一次 20 分, 3*20=60 次 60*20=1200/60=20 Hr 
        //      Response.Write("<SCRIPT   LANGUAGE='JavaScript'>window.opener=null;window.close();</SCRIPT> "); // close system



    }

    ///////////////////////////////////////////////////////////////////////
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Write("<SCRIPT   LANGUAGE='JavaScript'>window.opener=null;window.close();</SCRIPT> ");
    }

    protected void Button35_Click(object sender, EventArgs e)
    {
        autorunflag = 0;   // Automatic running
        Arunstatus = 0;     // currstatus
        //  runstep = 0;       // run step

        Timer1.Enabled = false;
        TextBox4.Text = "Clear Loop flag" + ASysCount.ToString();
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        if (FPassword.ToLower() != "kenken") return;
        //Timer1.Enabled = true;
        // Aautoctlflag = 1; // auto only
        Button17.Text = "系統自動啟動中";
        TextBox4.Text = "系統自動啟動";
        string tmpstr = MainLoop("");
    }

}
