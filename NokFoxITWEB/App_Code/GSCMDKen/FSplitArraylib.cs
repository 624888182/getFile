using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;
using Economy.BLL;
using EconomyUser;
using Economy.Publibrary;
using SCM.GSCMDKen;

namespace SCM.GSCMDKen
{

 
public class FSplitArraylib
{
    protected string CuurDate1 = DateTime.Today.ToString("yyyyMMdd");
    protected string Currtime1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    public static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    FSplitlib FSplitlibPointer = new FSplitlib();
    FPubLib   FPubLibPointer = new FPubLib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();

    protected string FileName;
    protected string FileType;
    protected string ServerPath;
    protected string Upload_File;
    protected string StrTicks;
    protected int Excel_int;
    protected string ExcelFile;
    protected string siteName = "NLV";
    protected string currency = "RMB";   //幣種
    protected string tableDate = "";      //報表日期
    protected string userName = "EricNLV";
    protected string BUType = "NLV";
    protected bool flag = false;
    protected string mark = "";
    protected DataSet ds;
    protected SqlConnection conn;
    protected SqlCommand cmd, command;
    protected string PassToken = "/(11)/1/Nokia/(12)/ALL/(14)/1/LF/(19)/5/Wall-E/Lynx/Justin/Saga/";
    protected string PassPrgflag = "";
    protected string PassDatadate = "";
    protected string ProgramParam = "N";
    protected string Programnum = "1";
    protected string Programstatus = "1";
    protected string Programtype = "1";  // 1. Main Action 2. Main Loop Not Login 3. Been called once Client 
    protected string Programflag = "5";  // 1.Syn-ETDUpload  2.Syn-ShipPlan 3. GSCMD-4A3_Detail
    protected string DiskParam = "N";
    protected char WriETAarraytransDayflag = 'N';
    protected string WriDBFFlag = "N";

    protected DateTime tmptoday = DateTime.Today;
    protected DateTime datetimebuf = DateTime.Today;
    protected DateTime MinStr = DateTime.Today;
    protected DateTime MaxStr = DateTime.Today;
    protected DateTime tmpMaxMin = DateTime.Today;
    protected DateTime firstThuDate = DateTime.Today;

    protected string ErrMsg = "";
    protected string tmpUploadType = "EVPV";
    protected string tmpDocumentID = "";
    protected int tmpOrgSPQty = 0;
    protected string tmpFoxconnSite = "";
    protected string tmpCustomerSite = "";
    protected string tmpCustomerPN = "";
    protected string tmpFoxconnPN = "";
    protected string tmpFoxconnBU = "";
    protected string tmpHHPN = "";
    protected string tmpDescription = "";
    protected string tmpPNProject = "";
    protected string tmpFoxconnPlant = "";
    protected string tmpsapce = "";
    protected string tmpDom_Exp = "";
    protected string tmpSPDate = "";
    protected string tmp1SPDate = "";
    protected string tmpSPWeek = "";
    protected string tmpNokiaPartNo = "";
    protected string tmpReleaseYear = "";
    protected string tmpReleaseDate = "";
    protected string tmpCurrent_Dos = "0";
    protected string tmpNext_Dos = "0";
    protected string tmpGIT_Dos = "0";
    protected string tmpMPQ = "0";
    protected string tmpLeadTime = "0";
    protected string swdayWeek = "";
    protected string tmpstr1 = "";
    protected string tmpstr2 = "";
    protected string tmpNokiaEVDocm = "";
    protected string tmpNokiaPVDocm = "";
    protected string tmpCustomerDum = "";
    protected string tmpExeceptionLog = "";
    protected string tmpTicket = "";

    protected int GetDataSqlLong = 0;
    protected int UploadETDrecordLong = 0;
    protected int PartUploadETDrecordLong = 0;
    protected int CustomerPlantLong = 0;
    protected int arrayGITLong = 3000;
    protected int sortnumber = 3;
    protected int BefreDVLoc100 = 101 - 1;
    protected int sw1 = 0;
    protected int tmptodaylocation = 0; // 從最早 DV 為100 , Offset 偏移到今天 
    protected int tmpC3location = 0;   // C+3 起始位置
    protected int weekloc = 1; // Week 會總到星期 ? 20100209 為星期一 Nokia 每周為星期一開始
    protected int arraytransDayfixLong400 = 400;
    protected int arraytransDayY35 = 27; // 35
    protected int tmpReqProcETDToETAVar = 0;
    protected int IndexArrayLoc = 91;  // save in next 1
    protected static int arrayCustomerFoxconnPNToOneSetLong = 10000;
    protected static int arrayCustomerFoxconnPNToOneSetIndex = 91;
    protected static int arrayCustomerFoxconnPNToOneSetFieldLong = arrayCustomerFoxconnPNToOneSetIndex + arrayCustomerFoxconnPNToOneSetLong/10;
    protected int arrayShipPlanXLong = 6000;
    protected int arrayShipPlanYLong = 80;
    protected int DuplicateDBLong = 10000;
    protected int arrayNokiaEVDocmLong = 0;
    protected int arrayNokiaPVDocmLong = 0;
    protected int GIT4A5Long = 0, UNLV_T_NEWrecordLong = 0;
    protected string GIT4A5Str = "";

    protected int var1 = 0;
    protected int var2 = 0;
    protected int var3 = 0;
    protected int var4 = 0;
    protected int var5 = 0;
    protected int var6 = 0;
    protected int var7 = 0;
    protected int var8 = 0;
    protected int tmpaccount = 0;
    protected char UpdateReqprocflag = 'Y';
    protected char WriarraytransDay = 'Y';
    //    protected char Programflag = '2';                                // 1. Upload  2.Download 3. Search
    protected char WriDBFShipPlanFlag = 'Y';

    protected int totloopcount = 0;
    protected int totloopcountno = 0;
    protected int eachIDDVno = 0;               // 每個 Docm 中 DV 數量和
    protected int eachIDSetCount = 0;           // 每個 Docm 中 Cus+Fox+CustPN 組別數 
    protected int eachIDSetDVno = 0;           // 每個 Docm 中 Cus+Fox+CustPN 組別數中 DV 數量和
    protected int eachIDDVduplicateno = 0;      // 每個 Docm 中 DV duplicate 數量和
    protected int GITUpdateNumber = 0;
    protected int loopforeachSetcount = 0;
    protected string tradatetime = "";
    protected double tmpConvertDoublebuf = 0;
    protected int arrayParamLong = 500;
    protected int DelpcateEVNum = 0;
    protected int MainloopReadHead = 0;
    protected int MainloopReadTail = 0;
    protected int MaxMimDVLong = 1;
    protected int WriteCount = 0;
    protected int WriteDBFBaseLong = 7 * 19;     // 回寫資料基本長度
    protected int WriteDBFBaseLongLoc = 7 * 19;   // 回寫資料長度位置
    protected int DuplicateDBFLoc = 0;     // Error BDF Loc 
    protected int firstThuDateloc = 0;
    protected int FirstDVLoc = 101;
    protected int FirstPVLoc = 104;
    protected int arrayGITWriLong = 4000;
    protected int arrayPlantCodeLong = 0;
    protected int arrayPNGroupLong = 0;
    protected int arraySapLong = 0;


    string sql1 = "";
    string sql2 = "";
    string sql3 = "";
    string sql4 = "";
    string sql5 = "";
    string sql6 = "";
    string sql7 = "";
    string sql8 = "";
    string sql9 = "";
    string sq20 = "";
    string sql21 = "";
    string sql22 = "";
    string sql23 = "";   // Programflag = "3" and GSCMD Org DV Query
    string sql41 = "";   // PlantCode
    string sql42 = "";   // 
    string sql43 = "";   // InHouse_FromSap 

    protected string DownDVDay      = DateTime.Today.ToString("yyyyMMdd");  // 20100320
    protected string DownDVBeginDay = "";
    protected string ds3 = "", ds3NLV_T_NEWDV = "";
    protected string tmpdatafrom = "EV";
    protected string RunEVflag = "N";
    protected string RunPVflag = "N";
    protected string RunPOflag = "N";
    string[,] DataPara = new string[10, 3];
    protected int arrayVarX = 2 - 1 - 1;
    protected int arrayVarY = 0;
    protected int NextFreeLocPoint = 10;
    // string[,] arrayVar = new string[2, 10000];

    protected int EVLong = 0;
    protected int PVLong = 0;
    protected int arrayPVLong = 0;

    protected string DownPVfirstThuDate, DownPVfirstFriDate;
    protected int arraytransSecondAvailLoc = 0;   // 20100709 New Assign Loc from arraytransSecondAvailLoc

    protected int arrayDVCountLong = 20;
    string[,] arrayDVCount = new string[20 + 1, 5 + 1];
    string[,] ResData = new string[10000, 100];
    protected int arrayAlldatafromLong = 500;
    protected int OrgDVinarrayCustomerFoxconnPNToOneSetloc = 4;      //  20100823 
    protected int PlantCodeinarrayCustomerFoxconnPNToOneSetloc = 3;  //  20100823                  
    protected int PNGroupinarrayCustomerFoxconnPNToOneSetloc = 2;    //  20100823
    protected int SapinarrayCustomerFoxconnPNToOneSetloc = 1;        //  20100823
    protected int SapParaCapLoc = 25;                                //  20100917
    protected string GSCMDRecQty = "0";
    protected string Sap_InvRecQty = "0";
    protected string GWCappRecQty = "0";
    // protected string LocPath1 = "Data Source=127.0.0.1;Initial Catalog=MHReckon;User ID =sa;Password=sa123456;Timeout=120;";
    // protected string WebPath = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    protected int FoxconnBULoc = 0;
    protected int CapDayQtyLoc = 0;
    protected int DataSetGWCappLoc = 0;
    protected int GWCappFindFlagLoc = 0; // GWCappFindFlagLoc
    protected int SapInvFindFlagLoc = 0; // SapInv
    protected string WriGWCappFindFlag = "Y";
    string[,] arrayNokiaCalendar = new string[8000 + 1, 5 + 1];  // 20 year
    protected int arrayNokiaCalendarLong = 0;
    protected string Sum4A3sql = "";
    protected string SumNLV_T_NEWDVsql1 = "", SumNLV_T_NEWDVsql2="", SumNLV_T_NEWDVsql3="";
    protected int QueryTotLong = 0;
    protected string EVNeed = "";
    /////////////////////////////////////////////
    // New Function for Split flag 20100709

    string splitWeektoDay = "Y";
    int PrintHeadSW = 0;   // "Y" Print Head, "N"
    public string tmp1 = "";
    
    // 20101025 SCMMain
    public static string LogUsername = ""; 
    public string text1 = DateTime.Today.ToString("yyyy-MM-dd");
    public string text2 = DateTime.Today.ToString("yyyyMMdd");
    public string text3 = DateTime.Today.ToString("yyyyMMdd");
    public string text8 = DateTime.Today.ToString("yyyyMMdd");
    public static int PDSLoc = 11;

    string[,] P5 = new string[50 + 1, 3 + 1];  //
    string[,] P6 = new string[50 + 1, 3 + 1];
    string[,] P7 = new string[50 + 1, 3 + 1];
    string[,] PL = new string[50 + 1, 3 + 1];

    private void SetInitvar()
    {
        //string[,] P5 = new string[30 + 1, 3 + 1];  //
        //string[,] P6 = new string[30 + 1, 3 + 1];

        int v1 = 0, v2 = 0;
        string s1 = "", s2 = "";

        s1 = "select * from ParaMap where F0 = '1' and F1 = 'Nokia' and F3 = '5' ";
        DataSet Pds1 = LibSCM1Pointer.GetDataByDataPath(s1, RunDBPath);  // All Data Not only EV
        if ( Pds1.Tables.Count > 0) 
        {
            v1 = Pds1.Tables[0].Rows.Count;
            if ( v1 >= 50 ) v1 = 50;
            for (v2 = 0; v2 < v1; v2++)
            {
                P5[v2 + 1, 1] = Pds1.Tables[0].Rows[v2]["F4"].ToString();
                P5[v2 + 1, 2] = Pds1.Tables[0].Rows[v2]["F5"].ToString();
            }
        }

        s1 = "select * from ParaMap where F0 = '1' and F1 = 'Nokia' and F3 = '6' ";
        Pds1 = LibSCM1Pointer.GetDataByDataPath(s1, RunDBPath);  // All Data Not only EV
        if (Pds1.Tables.Count > 0)
        {
            v1 = Pds1.Tables[0].Rows.Count;
            if (v1 >= 50) v1 = 50;
            for (v2 = 0; v2 < v1; v2++)
            {
                P6[v2 + 1, 1] = Pds1.Tables[0].Rows[v2]["F4"].ToString();
                P6[v2 + 1, 2] = Pds1.Tables[0].Rows[v2]["F5"].ToString();
            }
        }

        s1 = "select * from ParaMap where F0 = '1' and F1 = 'Nokia' and F3 = '7' ";
        Pds1 = LibSCM1Pointer.GetDataByDataPath(s1, RunDBPath);  // All Data Not only EV
        if (Pds1.Tables.Count > 0)
        {
            v1 = Pds1.Tables[0].Rows.Count;
            if (v1 >= 50) v1 = 50;
            for (v2 = 0; v2 < v1; v2++)
            {
                P7[v2 + 1, 1] = Pds1.Tables[0].Rows[v2]["F4"].ToString();
                P7[v2 + 1, 2] = Pds1.Tables[0].Rows[v2]["F5"].ToString();
            }
        }

        v1 = 50;
        for (v2 = 0; v2 < v1; v2++)
        {
            PL[v2 + 1, 1] = "";
            PL[v2 + 1, 2] = "";
        }
       
    }

    public string[,] QueryGSCMDOrgDV(string prgflag, string tdate, string Ppar, string tWriDBFFlag) //    Test = FSplitArraylibPointer.QueryGSCMDOrgDV(t1);
    {
        string subtmpstr1 = "";
        string subtmpstr2 = "";
        string subtmpstr3 = "";
        string subtmpstr4 = "";
        string SCRbox = "";
        string s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12;
        int localvar1 = 0;
        int localvar2 = 0;
        int localvar3 = 0;
        int localvar4 = 0;
        int localVarX = 0;
        string localstr1 = "";
        string localstr2 = "";
        string localstr3 = "";
        string localstr4 = "";
        string localstr5 = "";
        string localstr6 = "";

        PrintHeadSW = 0; //
        DateTime locdate1 = DateTime.Today;
        splitWeektoDay = "N"; // 不用拆開
        WriDBFFlag = tWriDBFFlag;
        QueryTotLong = 0;

        // DownDVDay = text1.Substring(0, 4) + text1.Substring(5, 2) + text1.Substring(8, 2); // text1= DateTime.Today.ToString("yyyy-MM-dd");

        if (tdate != "" ) DownDVDay = tdate;
        if (prgflag != "" ) Programflag = prgflag;

        SetInitvar();

        tmptoday = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(DownDVDay)); // Test 20110423 
        
        tmpReleaseYear = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        tmpReleaseDate = tmptoday.ToString("yyyyMMdd");
        tmpDocumentID = tmptoday.ToString("yyyyMMdd");

        subtmpstr2 = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        localvar1 = Convert.ToInt32(tmptoday.DayOfYear);
        localvar2 = Convert.ToInt32(tmptoday.DayOfWeek);

        subtmpstr1 = FPubLibPointer.getNatWeekofthisYear(2, localvar1, localvar2, subtmpstr2, tmptoday);
        // 20110423 subtmpstr1 = ShipPlanlibPointer.getWeekofthisYear(1, localvar1, localvar2, subtmpstr2, tmptoday);
        tmpSPWeek = subtmpstr1;
        // string[,] ResData = new string[10, 3];

        WriteCount = 0;

        if (text8 != "") WriETAarraytransDayflag = Convert.ToChar(text8.Substring(0, 1));

        string[,] arrayPV  = new string[1+1, 1];
        string[,] arrayVar = new string[1+1, 1];

        SetDVParamArray();


        // protected static int arrayCustomerFoxconnPNToOneSetLong = 10000;
        // protected static int arrayCustomerFoxconnPNToOneSetFieldLong = 1000;
        // arrayCustomerFoxconnPNToOneSetIndex 291
        string[,] arrayCustomerFoxconnPNToOneSet = new string[arrayCustomerFoxconnPNToOneSetLong, arrayCustomerFoxconnPNToOneSetFieldLong + 1];
        var3 = arrayCustomerFoxconnPNToOneSetLong;

        for (var4 = 0; var4 < var3; var4++)
        {
            for (var5 = 0; var5 < arrayCustomerFoxconnPNToOneSetFieldLong + 1; var5++) arrayCustomerFoxconnPNToOneSet[var4, var5] = "";
            for (var5 =62; var5 < 74 + 1; var5++) arrayCustomerFoxconnPNToOneSet[var4, var5] = "0";
            arrayCustomerFoxconnPNToOneSet[var4, 5] = (10000 + var4).ToString();     // 為 Key當Index 到 array 13 Upload
            arrayCustomerFoxconnPNToOneSet[var4, 6] = tmptoday.ToString("yyyyMMdd"); // DV 最早一天
            arrayCustomerFoxconnPNToOneSet[var4, 7] = tmptoday.ToString("yyyyMMdd"); // DV 最晚一天
            arrayCustomerFoxconnPNToOneSet[var4, 8] = "0";                           // Summary GIT, Stock, Consigned
            // arrayCustomerFoxconnPNToOneSet[var4, IndexArrayLoc] = "91"; // "11";  // loc 10 記錄下一位, 11放實際 DTDUpload Loc 
           
          
        }   // End InitializeCulture space

        string[,] arrayPart = new string[PartUploadETDrecordLong + 1, 20 + 1];
        SCRbox = "Test GSCMD & Syncro Proc ";

        if (tdate == "") subtmpstr1 = tmptoday.ToString("yyyyMMdd");
        else subtmpstr1 = tdate;
        // subtmpstr1 = (tmptoday.AddDays(-5)).ToString("yyyyMMdd");
        // localvar1 = Convert.ToInt32(subtmpstr1);
        arrayGITLong = 0;
        arrayGITWriLong = 0;
        string[,] arrayGIT = new string[arrayGITWriLong + 1, 15 + 1];
        arrayNokiaEVDocmLong = 0;
        string[,] arrayNokiaEVDocm = new string[arrayNokiaEVDocmLong + 1, 6 + 1];

        string[] DuplicateDBLog = new string[DuplicateDBLong + 1];
        DuplicateDBFLoc = 0;
        /////////////////////////////////////// Get detail data
        // Para    :                1       2      3     4 
        // para 1  : CustomerSite   ALL
        // para 2  : FoxconnSite    ALL 
        // para 3  : CustoermPN     ALL 
        // 1. X 軸 代表多少單位, 第一欄位 ALL 代表全部
        // 2. Y 軸 代表CustomerSite, FoxconnSite,,
        // 
        string[,] arrayParam = new string[arrayParamLong + 1, arrayParamLong + 1];
        for (var1 = 0; var1 < arrayParamLong + 1; var1++)
            for (var2 = 0; var2 < arrayParamLong + 1; var2++) arrayParam[var1, var2] = "";

        subtmpstr4 = TransWebParamToTable(ref subtmpstr1, ref arrayParam, ref arrayPart, Programflag, Ppar);  // Get Param return DocumentID 


        if (arrayParam[19, 2] != "") Programflag = arrayParam[19, 2]; // 20110312 Program Flag

        tmptoday = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(DownDVDay)); // 再做一次 20110428 

        tmpReleaseYear = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        tmpReleaseDate = tmptoday.ToString("yyyyMMdd");
        // subtmpstr2 = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        // localvar1 = Convert.ToInt32(tmptoday.DayOfYear);
        // localvar2 = Convert.ToInt32(tmptoday.DayOfWeek);

        tmpSPWeek = FPubLibPointer.getNatWeekofthisYear(2, localvar1, localvar2, subtmpstr2, tmptoday);
        // protected static string Sum4A3sql = "";  4A3_Detail_PNOnetSet
        DataSet dsMain = LibSCM1Pointer.GetDataByDataPath(Sum4A3sql, RunDBPath);  // All Data Not only EV
        if (dsMain.Tables.Count <= 0)
        {
            // tmpDocumentID = "-1"; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
            // return (ResData);
        }
        else
        {
            eachIDSetCount = dsMain.Tables[0].Rows.Count;
            FSplitlibPointer.PullSumArray(ref dsMain, ref eachIDSetCount, ref UploadETDrecordLong, ref arrayCustomerFoxconnPNToOneSet, ref arrayCustomerFoxconnPNToOneSetIndex, ref Programflag);
        }

        sql21 = subtmpstr1;
        DataSet ds3 = LibSCM1Pointer.GetDataByDataPath(sql21, RunDBPath);  // All Data Not only EV
        if (ds3.Tables.Count > 0) 
        {
            localvar1 = ds3.Tables[0].Rows.Count;
            GetDataSqlLong = ds3.Tables[0].Rows.Count;
            UploadETDrecordLong = ds3.Tables[0].Rows.Count;
            QueryTotLong        = ds3.Tables[0].Rows.Count;
        }

        string sql4A5GIT = "";
        EVLong = UploadETDrecordLong;
       
        var4 = 20 + 1;
        string[,] arrayEtdUpload = new string[UploadETDrecordLong + 100, var4];
        for (var1 = 0; var1 < UploadETDrecordLong + 100; var1++)
            arrayEtdUpload[var1, 1] = "";
        
       
        // if ((EVLong > 0) && (RunEVflag != "N"))
        if ( EVLong > 0 )
        {
            localstr1 = GetEV10ToarrayEtdUpload(51, ds3, UploadETDrecordLong, ref arrayEtdUpload, DelpcateEVNum, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSet, ref DuplicateDBFLoc, IndexArrayLoc, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, arraytransSecondAvailLoc, ref Programflag); // 不同 DV write into array
            // localstr1 = LibSCM1Pointer.GetEV4ToarrayEtdUpload(51, ds3, UploadETDrecordLong, ref arrayEtdUpload, DelpcateEVNum, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSet, ref DuplicateDBFLoc, IndexArrayLoc, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, arraytransSecondAvailLoc); // 不同 DV write into array
            //    localstr1 = FSplitlibPointer.GetEV4ToarrayEtdUpload( 51, ds3, UploadETDrecordLong, ref arrayEtdUpload, DelpcateEVNum, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSet, ref DuplicateDBFLoc, IndexArrayLoc, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, arraytransSecondAvailLoc); // 不同 DV write into array
            // for (var4=1; var4 < eachIDSetCount + 1; var4++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA
            //    arrayCustomerFoxconnPNToOneSet[var4, 56] = "DV";
        }

        // SumNLV_T_NEWDVsql
        DataSet ds3NLV_T_NEW = LibSCM1Pointer.GetDataByDataPath(SumNLV_T_NEWDVsql1, RunDBPath);  // All Data Not only EV Man
        if ( ds3NLV_T_NEW.Tables.Count > 0) // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        {
            localvar1 = ds3NLV_T_NEW.Tables[0].Rows.Count;
            QueryTotLong = QueryTotLong + localvar1;
            UNLV_T_NEWrecordLong = ds3NLV_T_NEW.Tables[0].Rows.Count;
            localstr1 = FSplitlibPointer.GetNLV_T_NEWDVEtdUpload(51, ds3NLV_T_NEW, UNLV_T_NEWrecordLong, ref arrayEtdUpload, DelpcateEVNum, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSet, ref DuplicateDBFLoc, IndexArrayLoc, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, arraytransSecondAvailLoc); // 不同 DV write into array
        }

        ds3NLV_T_NEW = LibSCM1Pointer.GetDataByDataPath(SumNLV_T_NEWDVsql2, RunDBPath);  // All Data Not only EV  FSC
        if (ds3NLV_T_NEW.Tables.Count > 0)  // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        {
            localvar1 = ds3NLV_T_NEW.Tables[0].Rows.Count;
            QueryTotLong = QueryTotLong + localvar1;
            UNLV_T_NEWrecordLong = ds3NLV_T_NEW.Tables[0].Rows.Count;
            localstr1 = FSplitlibPointer.GetNLV_T_NEWDVEtdUpload(51, ds3NLV_T_NEW, UNLV_T_NEWrecordLong, ref arrayEtdUpload, DelpcateEVNum, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSet, ref DuplicateDBFLoc, IndexArrayLoc, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, arraytransSecondAvailLoc); // 不同 DV write into array
        }

        ds3NLV_T_NEW = LibSCM1Pointer.GetDataByDataPath(SumNLV_T_NEWDVsql3, RunDBPath);  // All Data Not only EV Other
        if (ds3NLV_T_NEW.Tables.Count > 0)  // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        {
            localvar1 = ds3NLV_T_NEW.Tables[0].Rows.Count;
            QueryTotLong = QueryTotLong + localvar1; 
            UNLV_T_NEWrecordLong = ds3NLV_T_NEW.Tables[0].Rows.Count;
            localstr1 = FSplitlibPointer.GetNLV_T_NEWDVEtdUpload(52, ds3NLV_T_NEW, UNLV_T_NEWrecordLong, ref arrayEtdUpload, DelpcateEVNum, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSet, ref DuplicateDBFLoc, IndexArrayLoc, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, arraytransSecondAvailLoc); // 不同 DV write into array
        }

        if (QueryTotLong > 0) tmpDocumentID = tmpDocumentID + "-" + QueryTotLong.ToString(); //QueryTotLong.ToString();
        else tmpDocumentID = "-1";

        tmpdatafrom = "EV";
        LoopReqQueryDV(ref arrayPart, ref arrayGIT, ref arrayEtdUpload, ref arrayCustomerFoxconnPNToOneSet, ref arrayNokiaEVDocm, ref arrayPV, ref arrayVar);       
        PrintHeadSW++;

        SCRbox = "End Org DV Detail Query Record:" + QueryTotLong.ToString() + " DocumentID:" + tmpDocumentID;

        if ( tmpDocumentID == "-1" )
        {
            ResData[1, 77] = "-1";
            ResData[2, 77] = "-1";
            ResData[3, 77] = "-1";
        }
        
        return (ResData);
    }
      

    private void LoopReqQueryDV(ref string[,] arrayPart, ref string[,] arrayGIT, ref string[,] arrayEtdUpload, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayNokiaEVDocm, ref string[,] arrayPV, ref string[,] arrayVar) // 
    {
        string tmpstring1 = "";
        DateTime tmpdate1, tmpdate2, tmpdate3;
        tmpdate1 = DateTime.Today;
        tmpdate2 = DateTime.Now;
        tmpdate3 = DateTime.Now;

        int localvar1 = 0;
        int localvar2 = 0;
        int localvar3 = 0;  // Summary SPQty 20100819
        int localvar4 = 0;
        int localvar5 = 0;
        string localstr1 = "";
        string localstr2 = "";
        string localstr3 = "";
        string localstr4 = "";

        string locrefstr1 = "";

        var4 = 20;
        string[] tmpprebuff = new string[var4 + 1];
        string[] tmpaftbuff = new string[var4 + 1];

        string[,] arrayShipPlan = new string[arrayShipPlanXLong, arrayShipPlanYLong + 1];

        for (var1 = 0; var1 < arrayShipPlanXLong; var1++)
            for (var2 = 0; var2 < arrayShipPlanYLong + 1; var2++)
                arrayShipPlan[var1, var2] = "";


        // DateTime MinStr, MaxStr, tmpMaxMin;


        if (tmpDocumentID == "") return;

        totloopcount = 0;           // Initial Value
        totloopcountno = 0;           // 
        eachIDDVno = 0;           // 每個 Docm DV 數量
        // 一開使及使用 eachIDSetCount = 0;           // 每個 Docm 中 Cus+Fox+CustPN 組別數 
        eachIDSetDVno = 0;           // 每個 Docm 中 Cus+Fox+CustPN 組別數中 DV 數量和
        eachIDDVduplicateno = 0;      // 每個 Docm 中 DV duplicate 數量和

        tradatetime = "20100503";
        tradatetime = ShipPlanlibPointer.TrsstringToDateTime(tradatetime);


        ////////////////////////////////////////////////////////////////////////
        // Get Customer+Foxconnside+CustomerPN 建力一個 SubSet 為 Key 
        // 每個為單一 Module to Running
        //
        // X  CustomerSide  --------------------------------- (600)
        // Y  FoxconnSide   |
        //    CustomerPN    |
        //                 (5)
        ////////////////////////////////////////////////////////////////////////

        string[,] arraytransDay = new string[arraytransDayfixLong400, arraytransDayY35 + 1]; // Initial transfer array 20100319

        int loop1var1 = 0;
        int loop1var2 = 0;
        int loop1var3 = 0;
        int loop1var4 = 0;

        MainloopReadHead = IndexArrayLoc + 1;
        MainloopReadTail = IndexArrayLoc + 1;

        for (var1 = 1; var1 < arraytransDayfixLong400; var1++)  // 建立出始 Table
        {
            arraytransDay[var1, 1] = var1.ToString();
            if (var1 < 10)
                arraytransDay[var1, 1] = "00" + arraytransDay[var1, 1]; // 補3位
            else
            if (var1 < 100)
                arraytransDay[var1, 1] = "0" + arraytransDay[var1, 1]; // 補3位

            arraytransDay[var1, 20] = "DV";
        }
        
        // for (var1 = 0; var1 < arraytransDayfixLong400; var1++)  // 建立出始 Table
        //    for (var2 = 0; var2 < arraytransDayY35; var2++)
        //        arraytransDay[var1, var2] = "0";

        if (eachIDSetCount <= 0)
        {
            tmpDocumentID = "-1";
            return;  // Return 
        }
            
        for (loopforeachSetcount = 1; loopforeachSetcount < eachIDSetCount + 1; loopforeachSetcount++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA
        {
            tmpCustomerSite = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 56];  // Test
            tmpCustomerSite = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 1];
            tmpFoxconnSite = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 2];
            tmpCustomerPN = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 3];
            MinStr = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 6]));
            MaxStr = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 7]));
            // tmpAgreement = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, arrayCustomerFoxconnPNToOneSetIndex - 10+4];
            // tmpItem      = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, arrayCustomerFoxconnPNToOneSetIndex - 10+5];
            // if ((tmpCustomerPN == "9904202") && (tmpCustomerPN == "9904202")) 
            //    localstr4 = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 60]; 

            

            MainloopReadHead = 11;
            MainloopReadTail = 11;
            localvar3 = 0;  // Summary SPQty
            tmpOrgSPQty = 0;

            if ((tmpCustomerSite == "") || (tmpFoxconnSite == "") || (tmpCustomerPN == "")) tmp1 = "ErrortmpCustomerSite = Space ";

            //////////////////////////////////////////////////////////////////////////////////
            //  1. Seek for  Max and Min Account
            ///////////////////////////////////////////////////////////////////////////////////
            // 1. 從本次 DocumentID 中找出最大日期及最小日期, 目前日期, 用日期比較大小
            // 2. 最小日期放在第 101 矩陣位置
            // 3. 前推 100 位置為須算 GIT 數量
            //             MaxStr    = Convert.ToDateTime(arrayEtdUpload[1, 4]); 20100218 Update

            text2 = MaxStr.Subtract(MinStr).ToString();
            var1 = text2.IndexOf(".");  // Seek first 151.1 point location

            if (var1 <= 0) MaxMimDVLong = 1;
            else MaxMimDVLong = Convert.ToInt32(text2.Substring(0, var1)); // DV (最大 - 最小) 天數    

            text2 = tmptoday.Subtract(MinStr).ToString();
            var1 = text2.IndexOf(".");  // Seek first 151.1 point location

            if (var1 <= 0) var2 = 0;
            else var2 = Convert.ToInt32(text2.Substring(0, var1)); // 得到今天在第幾個 Array location 未加 100
            var1 = BefreDVLoc100 + 1 + var2;  // 起始 100+1(DV 最早日) + 今天實際位置 114
            tmptodaylocation = var1;          // 從最早 DV 為100 , Offset 偏移到今天 100+1+OffSet

            WriteDBFBaseLongLoc = WriteDBFBaseLong + tmptodaylocation; // 今天起 18+1 周 最後位置
            if (WriteDBFBaseLongLoc >= 400)
            {
                // Response.Write("<script>alert(' 超過矩陣 400 資料失敗，請通資訊人員！')</script>");
                ErrMsg = "array over 400 , Programm eerror or data over 300 days";
                return;
            }


            sw1 = 0;
            // sw1 = String.Compare( (arrayEtdUpload[var1, 0]) , (arrayEtdUpload[var1+1,0]) );  // str1>str2, n=1
            //
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 做一新矩陣  arraytransDay
            //             100               MaxMimDVLong                   Extend
            //       1-------------- 101 -------------------------------------> <---------------------------->
            // < LeadTime>      <最小DV日期>           < DV >             <最大DV日期>  <Toatl 400 Array>
            //
            //                        < ------------ Currency Day ------------->
            //
            // ( X, Y ) X: 為 arraytransDay 開始計算並進入天數 
            //
            //  1. 建立一個矩陣  400  1-100 為前100 天 (留給前置時間 LeadTime), 101 為 DV 最小天日, 
            //  2. DV 前後相格為 MaxMimDVLong 
            //   X                    --------------------------------------------------------- (500)
            //   Y   Account Number(1)| 1 2 3 4 5 6 7 8 9 .... 101 ( DV 最小天, 不一定是今天....
            //       SPDate (2)       |
            //       SPDate "YYYYMMDD |(3)
            //       Org SPQty (4)    |
            //  Add Leadtime SPQty(5) |
            //  Add Leadtime GIT (6)  |        |
            //  > Today GIT move today| (7)   
            //       New SPQty(8)     | 5+6+7 = 8
            //       C+2 (9)          | 合併 C+2  C+3 後到周一
            //       WeekofDay(10)    | 記錄周幾 ? 周日為 0 
            //       Day/Week (11)    | 計錄回寫 Day or Week 
            //       ReleaseDate (12) |
            //       SPWeek (13)      |    
            //       DocumentID (14)  |    
            //       ETDSPQty   (15)  |
            //       CustomerSite(16) |    
            //       FoxconnSite(17)  |
            //       CustomerPN(18)   |
            //       DVCode (19)      |
            //       Come from DV(20) | Flag 
            //       arraytransDay[var1, 21] = tmpCurrent_Dos;
            //       arraytransDay[var1, 22] = tmpNext_Dos;
            //       arraytransDay[var1, 23] = tmpGIT_Dos;
            //       arraytransDay[var1, 24] = tmpMPQ;
            //       arraytransDay[var1, 25] = tmpLeadTime;
            //       arraytransDay[var1, 26] = Curr_Dos_Qty + Next_Dos_Qty
            //       arraytransDay[var1, 27] = MPQQty
            //       arraytransDay[var1, 31] = GIT_QTy  [Delivery_Qty]
            //       arraytransDay[var1, 32] = GIT Document_ID [Document_ID]
            //      
            //  1. 建立依天為主 Array MaxMimDVLong+100 是為流水日期
            //  2. 起始日為 MinStr, 已後每格加一日
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // var3 = MaxMimDVLong + 200;
            // var4 = 15 + 1;
            // string[,] arraytransDay = new string[MaxMimDVLong + 200, var4];  Move to proc start

            // tmpMaxMin = MinStr;
            tmpMaxMin = MinStr.AddDays(-100); // Mix DV Date - 100 is the start point of this array date
            // 20110425 tmpSPWeek = ShipPlanlibPointer.getWeekofthisYear(2, localvar1, localvar2, localstr1, tmptoday); // Para 1 , 2 for system 
            tmpSPWeek = FPubLibPointer.getNatWeekofthisYear(2, localvar1, localvar2, localstr1, tmptoday); // Para 1 , 2 for system 

            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 12] = tmpReleaseDate;  // ReleaseDate
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 13] = tmpSPWeek;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 28] = tmpDocumentID;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 15] = "0"; // Org ETA 
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 16] = tmpCustomerSite;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 17] = tmpFoxconnSite;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 18] = tmpCustomerPN;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 19] = "";
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 20] = "";
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 21] = tmpCurrent_Dos;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 22] = tmpNext_Dos;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 23] = tmpGIT_Dos;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 24] = tmpMPQ;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 25] = tmpLeadTime;
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 26] = "0";
            arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 27] = "0";

            arraytransDay[0, 0] = "";
            for (var1 = 1; var1 < arraytransDayfixLong400; var1++)  // 建立出始 Table
            {

                arraytransDay[var1, 2] = Convert.ToString(tmpMaxMin);
                arraytransDay[var1, 3] = tmpMaxMin.ToString("yyyyMMdd");
                arraytransDay[var1, 4] = "0";
           //     arraytransDay[var1, 5] = "0";
           //     arraytransDay[var1, 6] = "0";
           //     arraytransDay[var1, 7] = "0";
           //     arraytransDay[var1, 8] = "0";
           //     arraytransDay[var1, 9] = "0";
                arraytransDay[var1, 10] = (Convert.ToInt32(tmpMaxMin.DayOfWeek)).ToString();
           //     arraytransDay[var1, 11] = "";
           //     arraytransDay[var1, 26] = "0";
                  arraytransDay[var1, 27] = "0";
           //     arraytransDay[var1, 28] = "";
           //     arraytransDay[var1, 29] = "";
           //     arraytransDay[var1, 30] = "";
           //     arraytransDay[var1, 31] = "0";  // GIT_QTy  20100522
           //     arraytransDay[var1, 32] = "";  // GIT Document_ID
           //     arraytransDay[var1, 33] = "";  // Agreement 20100808
           //     arraytransDay[var1, 34] = "";  // Item
           //     arraytransDay[var1, 35] = "";

                tmpMaxMin = tmpMaxMin.AddDays(1);
            }


            // Find first Thurday
            localvar1 = Convert.ToInt32(arraytransDay[tmptodaylocation, 10].ToString()); // 星期幾 ? 找周4
            if (localvar1 == 0) firstThuDateloc = tmptodaylocation - 7 + 4;                    // 第一個周4位置
            else if (localvar1 >= 4) firstThuDateloc = tmptodaylocation - localvar1 + 4;       //
            else firstThuDateloc = tmptodaylocation - localvar1 + 4 - 7;   //

            firstThuDate = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(arraytransDay[firstThuDateloc, 3]));

            if (Convert.ToInt32(arraytransDay[tmptodaylocation, 10]) == 0)                // if today = Sunday weekday=0
                localvar4 = 21 - 7;                                                       //    need 21 days
            else                                                                          // else
                localvar4 = 21 - (Convert.ToInt32(arraytransDay[tmptodaylocation, 10]));  //    need 21- 今天星期幾 = 天數
            //
            tmpC3location = tmptodaylocation + localvar4 + 1;                             // 完 C+2 , 第一個 C+3 位置, 應為星期一   // 今天+到 c+2 完天數+1 = 第一個 C+3 位置

            FirstPVLoc = firstThuDateloc + 4;
            /////////////////////////////////////////////////////////////////////////
            // 加入 SPQty
            var1 = 1;
            var3 = 1;
            var5 = 0;
            var6 = 0;
            var7 = 0;

            //////////////////////////////////////////////////////////////////////////
            // Make a new table 1-- 291 Data 291+1=292
            // 291+1 -- 1000 Data (1) Forecast_QtyTypeCode (2) Forecast_BeginDate 
            //                    (3) Forecast_IntervalCode (4) Forecast_Qty

            MainloopReadHead = arrayCustomerFoxconnPNToOneSetIndex + 1; // 20110226 IndexArrayLoc;
            MainloopReadTail = arrayCustomerFoxconnPNToOneSetIndex + 1;
            var1 = 11;
            while (arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail] != "")   // 從 10 開使, 地一筆 11
            {                                                                    //
                // var1 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail]);
                text2 = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail + 1].ToString(); // ds3.Tables[0].Rows[var1]["SPDate"].ToString();
                text3 = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail + 3].ToString(); // ds3.Tables[0].Rows[var1]["SPQty"].ToString();
                if (text3 == null) text3 = "0";

                swdayWeek = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail + 2].ToString(); // ds3.Tables[0].Rows[var1]["Forecast_IntervalCode"].ToString();
                tmpSPDate = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail + 1].ToString(); // .ToString("yyyyMMdd");    
                tmpReleaseDate = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 6].ToString(); // ReleaseDate
                // if ((tmpReleaseDate == "") || (tmpReleaseDate == null)) tmpReleaseDate = tmptoday.ToString("yyyyMMdd");

                loop1var2 = 1;
                text2 = ShipPlanlibPointer.TrsstringToDateTime(text2);
                localstr1 = Convert.ToDateTime(text2).Subtract(MinStr).ToString();     // 第一筆 DV 到此筆距離
                localvar1 = localstr1.IndexOf(".");  // Seek first 151.1 point location
                if (localvar1 < 0) localvar2 = 0;
                else localvar2 = Convert.ToInt32(localstr1.Substring(0, localvar1)); // 得到今天在第幾個 Array location 未加 100
                var3 = BefreDVLoc100 + 1 + localvar2;  // 起始 100+1(DV 最早日) + 直接找兩個日期相同極為當個arraytrans位置   
                if ((text3 != "0") && (var3 < 390))
                {
                    // localstr1 = Convert.ToDateTime(text2).Subtract(MinStr).ToString();     // 第一筆 DV 到此筆距離
                    // localvar1 = localstr1.IndexOf(".");  // Seek first 151.1 point location
                    // if (localvar1 < 0) localvar2 = 0;
                    // else localvar2 = Convert.ToInt32(localstr1.Substring(0, localvar1)); // 得到今天在第幾個 Array location 未加 100
                    // var3 = BefreDVLoc100 + 1 + localvar2;  // 起始 100+1(DV 最早日) + 直接找兩個日期相同極為當個arraytrans位置   

                    // arraytransDay[var3, 12] = tmpReleaseDate;                      //
                    // arraytransDay[var3, 20] = "DV"; // Flag for Real DV Coming 

                    tmp1SPDate = arraytransDay[var3, 3];                           // if Forecast_IntervalCode = "Day" 
                    if (tmp1SPDate == tmpSPDate)                                  // ("yyyyMMdd"); Put in arraytransDay
                    {                                                              // else
                        // arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 12] = tmpReleaseDate; // ReleaseDate   // 本日期起 SPQty  周1 1/7, 周2 1/7, 周3 1/7,周4 1/7,周5 1/7,周6 1/7, 周7 SPQty - 6/7     

                        if (text3 != "0") text3 = text3;

                        // arraytransDay[var3, 4]  = text3; //  SPQty 數量 20100507 
                        arraytransDay[var3, 15] = text3;  // Trace Org SPQty 20100303

                        tmpConvertDoublebuf = Convert.ToDouble(text3);                 // For Cut 小數點 
                        text3 = Convert.ToInt32(tmpConvertDoublebuf).ToString();       //
                        arraytransDay[var3, 15] = text3;  // Trace Org SPQty 20100303  //
                        var5 = Convert.ToInt32(text3);    // SPQty convert number                       
                        arraytransDay[var3, 4] = text3; // 1text3SPQty 數量   
                        // localvar3 = localvar3 + Convert.ToInt32(text3);
                        
                    }   //  if (tmp1SPDate == tmpSPDate )     
                }       // if (text3!= "0") 

                MainloopReadTail = MainloopReadTail + 4;  // next fields 
            }   // end while read loop 


            //////////////  Rewrite back array localvar3  20100819 OrgDVinarrayCustomerFoxconnPNToOneSetloc = 4
            // arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, arrayCustomerFoxconnPNToOneSetIndex - OrgDVinarrayCustomerFoxconnPNToOneSetloc] = localvar3.ToString();
            tmpOrgSPQty = localvar3;
            /////////////////////////////////////////////////////////////////////////////////
            //  1. 將數量現日期+Leadtime 移到新日期, 找 Leadtime 往右移 SPDate+Leadtime,  並存下一格 Y 軸, y4 to y5  ? 從那理開始
            //  2. 今天前 GIT 數量加到今天, 今天後明天起, 加 LeadTime 往後移  
            ////////////////////////////////////////////////////////////////////////////////
            var3 = Convert.ToInt32(tmpLeadTime); // Offset LeadTime


            if ( Programflag == "5" ) // if (Programflag == '5') for char
            {
                // CurrNextDos to MPQQty 20100714
                // for (localvar5 = 0; localvar5 < arraytransDayfixLong400 - 10; localvar5++) arraytransDay[localvar5, 27] = arraytransDay[localvar5, 4]; // 將 DV Move to Lasr loc 27
                locrefstr1 = loopforeachSetcount.ToString();
                GSCMDOrg4A3TransDayToShipPlan(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref arrayCustomerFoxconnPNToOneSet, ref arrayVar); // trace only

                // locrefstr1 = "1";
                // if (WriETAarraytransDayflag == 'Y')
                //     TmpWriHardarraytransDay(ref locrefstr1, ref arraytransDay); // trace only      
            }
            else
            if ( Programflag == "6" ) // if (Programflag == '5') for char
            {
                locrefstr1 = loopforeachSetcount.ToString();
                GSCMDOrg4A5TransDayToShipPlan(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref arrayCustomerFoxconnPNToOneSet, ref arrayVar); // trace only
            }
            else
            if (Programflag == "7") // if (Programflag == '5') for char
            {
                locrefstr1 = loopforeachSetcount.ToString();
                GSCMDOrg4A7TransDayToShipPlan(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref arrayCustomerFoxconnPNToOneSet, ref arrayVar); // trace only
            }

        }  // end  for (loopforeachSetcount = 1; loopforeachSetcount < eachIDSetCount + 1; loopforeachSetcount++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA

        tmpaccount++;
        loopforeachSetcount++;
        if ((Programflag == "5") && (WriDBFShipPlanFlag == 'Y')) TmpOrgDV4A3QueryWriDBFShipPlanarraytransDay(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref firstThuDate, ref tmpSPWeek, ref tmpDocumentID); // trace only
        else if ((Programflag == "6") && (WriDBFShipPlanFlag == 'Y')) TmpOrgDV4A5QueryWriDBFShipPlanarraytransDay(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref firstThuDate, ref tmpSPWeek, ref tmpDocumentID); // trace only
        else if ((Programflag == "7") && (WriDBFShipPlanFlag == 'Y')) TmpOrgDV4A7QueryWriDBFShipPlanarraytransDay(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref firstThuDate, ref tmpSPWeek, ref tmpDocumentID, 19 ); // trace only

        string tlblMsg = "End Test ";

    } // end LoopReqProcETDToETA      
    private void SetDVParamArray()
    {
        int v1 = 0;

        for (v1 = 0; v1 < arrayDVCountLong + 1; v1++)
        {
            arrayDVCount[v1, 0] = "";
            arrayDVCount[v1, 1] = "";
            arrayDVCount[v1, 2] = "";
            arrayDVCount[v1, 3] = "0";
            arrayDVCount[v1, 4] = "1";  // 2:PO, 1:EV
        }

        arrayDVCount[01, 1] = "EV";
        arrayDVCount[02, 1] = "EV";
        arrayDVCount[03, 1] = "EV";
        arrayDVCount[04, 1] = "EV";
        arrayDVCount[01, 2] = "Discrete Gross Demand";
        arrayDVCount[02, 2] = "Consigned Inventory";
        arrayDVCount[03, 2] = "GIT";
        arrayDVCount[04, 2] = "On-Hand Inventory";
        arrayDVCount[05, 1] = "OfferFrame";
        arrayDVCount[06, 1] = "Internal PO";
        arrayDVCount[07, 1] = "PO";
        arrayDVCount[08, 1] = "Risk PO_EV";
        arrayDVCount[09, 1] = "Risk PO";
        arrayDVCount[10, 1] = "Spot PO1";
        arrayDVCount[11, 1] = "Spot PO2";
        arrayDVCount[12, 1] = "FSC";
        arrayDVCount[13, 1] = "Man";
        arrayDVCount[06, 4] = "2";
        arrayDVCount[07, 4] = "2";
        arrayDVCount[08, 4] = "2";
        arrayDVCount[09, 4] = "2";
        arrayDVCount[10, 4] = "2";
        arrayDVCount[11, 4] = "2";
        arrayDVCount[12, 4] = "2";
        arrayDVCount[13, 4] = "1";
    }

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Program Start     
    // text8為傳參數 1. Setup Write MemoryCurrNextDos 明細 text8= "Y" + textBox8.Text.Substring(1, (textBox8.Text.Length)-1);
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Data From                               Accept Paramater    Foxconn Part             4A5_detail_plant                                               
    // Para 0  : (10) free                    1     ==================================================================
    // Para 1  : (11) Customer      那各客戶 ALL    (Y) accept 
    // para 2  : (12) CustomerSite  客戶Site ALL    (Y) accept map --> Fox CustomerDum to para3
    // para 3  : (13) CustomerDukNo 客戶Site ALL             get from para2                               
    // para 4  : (14) FoxconnSite            ALL    (Y) accept  CUU,BJ,LH,LF,KOM,LAHTI,Chennai       FoxconnSite ( 11, 2)
    // para 5  : (15) FoxconnDukNo  Fox Site ALL
    // para 6  : (16) NokiaPartnoNo          ALL    (Y) accept                                       Forecast_CustomerPN
    // para 7  : (17) Foxconn Partno         ALL              
    // para 8  : (18) FoxconnBU              ALL    (Y) accept 
    // para 9  : (19) project                ALL    (Y) accept 
    // para 10 : (20) subproject             ALL    (Y) accept 
    // para 11 : (21) FoxconnRegion          ALL    (Y) accept 
    // para 12 : (22) Type                   ALL    (Y) accept 
    // para 13 : (23) UsersID                1      (Y) accept // define match UsersID 
    // para 14 : (24) Require Date 8 Bytes   1      (Y) accept // define 20100619 
    // para 15 : (25) datafrom               1      /EV/PV/PO/Menu/ 
    // para 16 : (26) MCG                    1      (Y) ALL, MCG 
    // para 17 : (27) All or Max Document    1      (Y) Document_ID ALL , Max 
    // para 18 : (28) Return ID              1
    // 1. X 軸 代表多少單位, 第一欄位 ALL 代表全部
    // 2. Y 軸 代表  CustomerSite, FoxconnSite,,
    //
    // Algorithm 1: Accepted Paramater from calling or other
    //           2. Convert  para2 to CustomerDum to param3  
    //           3. para4 accepted OK
    //           4. para6 accept OK
    ///////////////////////////////////////////////////////////////////////////////////////////
    private string TransWebParamToTable(ref string subtmpstr1, ref string[,] arrayParam, ref string[,] arrayPart, string Reqprogflag, string Pparm) //
    {
        // Substring(10, 2)  544790470:LF:MPELF ( 11,2 ) 
        string subs2 = "";  // all CustomerSite
        string subs4 = "";
        string subs6 = "";
        string subs9 = "";
        int cnti = 0;
        int cntj = 0;
        int cntk = 0;
        string DocumentIDMaxSw = "N";

        string[] PA = new string[arrayParamLong + 1];
        for (cnti = 0; cnti < arrayParamLong + 1; cnti++) PA[cnti] = "";

        string[] PB = new string[arrayParamLong + 1];
        for (cnti = 0; cnti < arrayParamLong + 1; cnti++) PB[cnti] = "";

        string[] NEWDV = new string[arrayParamLong + 1];
        for (cnti = 0; cnti < arrayParamLong + 1; cnti++) NEWDV[cnti] = "";

        PassToken = Pparm;
        // arrayParam[2, 1] = "ALL"; // All CustomerSite arrayParam[2, 2] = "Beijing"; arrayParam[2, 3] = "LH"; arrayParam[2, 5] = "Dongguan"; arrayParam[2, 6] = "Manuas";

        // 20100705    if (PassToken == null) PassToken = "/(11)/1/Nokia/(12)/ALL/(14)/1/LF/(19)/5/Wall-E/Lynx/Justin/Saga/(23)/1/Ken/";
        // PassToken = "/(11)/1/Nokia/(12)/ALL/(14)/ALL/(19)/ALL/(16)/1/0255375/";
        // PassToken = "/(11)/1/Nokia/(12)/ALL/(14)/1/544790470/(19)/3/Lynx/Justin/Saga/(23)/1/Ken/(25)/3/EV/PV/PO/(17)/1/1A02DYQ00VDX/";  GSCMD Querg

        if (PassToken == "") PassToken = "/(11)/1/Nokia/(12)/ALL/(21)/1/544790470/(25)/7/EV/PO//Risk PO_EV/Spot PO1/Spot PO2/(14)/ALL/";  // (19)/3/Lynx/Justin/Saga/";

        // tmpDocumentID = ShipPlanlibPointer.GetPassShipPlanToken(arrayParam, arrayParamLong, PassToken);
        tmpDocumentID = LibSCM1Pointer.C1GetPassShipPlanToken(arrayParam, arrayParamLong, PassToken);
        // Response.Redirect("http://10.186.33.13/SyncroDV_EV_Query/" + "?DocumentID = " + tmpDocumentID);
        if (tmpDocumentID == "-1") return(tmpDocumentID);  // subs9 = DateTime.Now.ToString("yyyyMMddHHmmssmm");   

        if (arrayParam[19, 2] != "") Reqprogflag = arrayParam[19, 2];
        if (arrayParam[14, 2] != "") DownDVDay   = arrayParam[14, 2];    // enddate
        if (arrayParam[18, 2] != "") DownDVBeginDay = arrayParam[18, 2]; // startdate

        EVNeed = "";
        // Special Merga 11 --> 4  將 FoxconnRegion 加入 FoxconnSite
        // Cancell 20110317
        //  for (cnti = 2; cnti <= arrayParamLong; cnti++)
        // {
        //    if (arrayParam[11, cnti] != "")
        //    {
        //        for (cntj = 2; cntj <= arrayParamLong; cntj++)
        //        {
        //            if (arrayParam[4, cntj] == "")
        //            {
        //                arrayParam[4, cntj] = arrayParam[11, cnti];
        //                cntj = arrayParamLong; // break
        //            }
        //        }
        //
        //    }
        // }   // Merge End

        
       if ((arrayParam[2, 1] == "ALL") || (arrayParam[2, 2] == ""))
       {
                PA[2] = "";         // all CustomerSite
                PB[2] = "";   
                NEWDV[2] = "";
       }
       else
       {
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[2, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[2] != "") PA[2] = PA[2] + " or ";
                        if (PB[2] != "") PB[2] = PB[2] + " or ";
                        if (NEWDV[2] != "") NEWDV[2] = NEWDV[2] + " or ";

                        PA[2] = PA[2] + "    CustomerSite = '" + arrayParam[2, var2] + "'  ";
                        PB[2] = PB[2] + "  a.CustomerSite = '" + arrayParam[2, var2] + "'  ";
                        NEWDV[2] = NEWDV[2] + "  Receiving_Site = '" + arrayParam[2, var2] + "'  ";
                    }
                }
                PA[2] = " and ( " + PA[2] + " ) ";
                PB[2] = " and ( " + PB[2] + " ) ";
                NEWDV[2] = " and ( " + NEWDV[2] + " ) ";
       }

            if ((arrayParam[4, 1] == "ALL") || (arrayParam[4, 2] == ""))
            {
                PA[4] = "";         // Foxconn_Site
                PB[4] = "";  
                NEWDV[4] = "";
            }
            else
            {
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[4, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[4] != "") PA[4] = PA[4] + " or ";
                        if (PB[4] != "") PB[4] = PB[4] + " or ";
                        if (NEWDV[4] != "") NEWDV[4] = NEWDV[4] + " or ";
                        //                subs4 = subs4 + " Substring(FoxconnSite, 11, 2) =  '" + arrayParam[4, var2] + "' ";
                        PA[4] = PA[4] + "   FoxconnSite =  '" + arrayParam[4, var2] + "' ";
                        PB[4] = PB[4] + " substring (substring(c.Foxconn_Site,11,len(c.Foxconn_Site)),0,CHARINDEX(':',substring(c.Foxconn_Site,11,len(c.Foxconn_Site))))  =  '" + arrayParam[4, var2] + "' ";
                        NEWDV[4] = NEWDV[4] + " Supplying_Site =  '" + arrayParam[4, var2] + "' ";
                    }
                }
                PA[4] = " and ( a." + PA[4] + " ) ";
                PB[4] = " and ( " + PB[4] + " ) ";
                NEWDV[4] = " and ( " + NEWDV[4] + " ) ";
            }

            if ((arrayParam[6, 1] == "ALL") || (arrayParam[6, 2] == ""))
            {
                PA[6] = "";         // all CustomerPN
                PB[6] = "";  
                NEWDV[6] = "";
            }
            else
            {
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[6, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[6] != "") PA[6] = PA[6] + " or ";
                        if (PB[6] != "") PB[6] = PB[6] + " or ";
                        if (NEWDV[6] != "") NEWDV[6] = NEWDV[6] + " or ";

                        PA[6] = PA[6] + "   Forecast_CustomerPN = '" + arrayParam[6, var2] + "' "; // C.Customer_SitePN
                        PB[6] = PB[6] + " a.Forecast_CustomerPN = '" + arrayParam[6, var2] + "' "; // C.Customer_SitePN
                        NEWDV[6] = NEWDV[6] + "    Customer_PN = '" + arrayParam[6, var2] + "' "; // C.Customer_SitePN

                    }
                }
                PA[6] = " and ( " + PA[6] + " ) ";
                PB[6] = " and ( " + PB[6] + " ) ";
                NEWDV[6] = " and ( " + NEWDV[6] + " ) ";
            }

            if ((arrayParam[7, 1] == "ALL") || (arrayParam[7, 2] == "")) PA[7] = "";         // all FoxconnPN [HHPARTS]
            else
            {
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[7, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[7] != "") PA[7] = PA[7] + " or ";
                        PA[7] = PA[7] + "   HHPARTS = '" + arrayParam[7, var2] + "' "; // C.Customer_SitePN
                        PB[7] = PB[7] + " a.HHPARTS = '" + arrayParam[7, var2] + "' "; // C.Customer_SitePN
                    }
                }
                PA[7] = " and ( " + PA[7] + " ) ";
            }

            if ((arrayParam[9, 1] == "ALL") || (arrayParam[9, 1] == ""))
            {
                PA[9] = "";         // Project 20100609
                PB[9] = "";
                NEWDV[9] = "";
            }
            else
            {
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[9, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[9] != "") PA[9] = PA[9] + " or ";
                        if (PB[9] != "") PB[9] = PB[9] + " or ";
                        if (NEWDV[9] != "") NEWDV[9] = NEWDV[9] + " or ";
                        PA[9] = PA[9] + "   Project = '" + arrayParam[9, var2] + "' "; // S.Project
                        PB[9] = PB[9] + " a.Project = '" + arrayParam[9, var2] + "' "; // S.Project
                        NEWDV[9] = NEWDV[9] + "   Project = '" + arrayParam[9, var2] + "' "; // S.Project
                    }
                }
                PA[9] = " and ( " + PA[9] + " ) ";
                PB[9] = " and ( " + PB[9] + " ) ";
                NEWDV[9] = " and ( " + NEWDV[9] + " ) ";
            }

         //   if ((arrayParam[15, 1] == "ALL") || (arrayParam[15, 1] == ""))
         //   {
         //       PA[15] = "";         // Datafrom
         //       PB[15] = ""; // datafrom  
         //       PA[31] = ""; // datafrom
         //   }
         //   else                                                                                //
         //   {                                                                                   // 
         //       RunPVflag = "N";
         //       for (var2 = 2; var2 < arrayParamLong + 1; var2++)
         //       {
         //           if (arrayParam[15, var2] == "") var2 = arrayParamLong + 1;
         //           else
         //           {
         //               // if (PA[15] != "") PA[15] = PA[15] + " or ";
         //               // if (PA[31] != "") PA[31] = PA[31] + " or ";
         //
         //               if (arrayParam[15, var2] == "EV")  // 
         //               {
         //                   if (PA[15] != "") PA[15] = PA[15] + " or ";
         //                   if (PB[15] != "") PB[15] = PB[15] + " or ";
         //
         //                   PA[15] = PA[15] + " (   datafrom = 'EV' and ( a.Forecast_QtyTypeCode = 'Discrete Gross Demand' or a.Forecast_QtyTypeCode = 'Consigned Inventory' or Forecast_QtyTypeCode = 'GIT' or Forecast_QtyTypeCode = 'On-Hand Inventory' or Forecast_QtyTypeCode = 'Blocked Stock' or Forecast_QtyTypeCode = 'Quality Stock' or Forecast_QtyTypeCode = 'Minimum Days of Supply Target' or Forecast_QtyTypeCode = 'Maximum Days of Supply Target' or Forecast_QtyTypeCode = 'Minimum Inventory Target' or Forecast_QtyTypeCode = 'Maximum Inventory Target' )) ";
         //                   PB[15] = PB[15] + " ( a.datafrom = 'EV' and ( a.Forecast_QtyTypeCode = 'Discrete Gross Demand' or a.Forecast_QtyTypeCode = 'Consigned Inventory' or Forecast_QtyTypeCode = 'GIT' or Forecast_QtyTypeCode = 'On-Hand Inventory' or Forecast_QtyTypeCode = 'Blocked Stock' or Forecast_QtyTypeCode = 'Quality Stock' or Forecast_QtyTypeCode = 'Minimum Days of Supply Target' or Forecast_QtyTypeCode = 'Maximum Days of Supply Target' or Forecast_QtyTypeCode = 'Minimum Inventory Target' or Forecast_QtyTypeCode = 'Maximum Inventory Target' )) ";
         //                   // PA[30] = "  and ( datafrom = 'EV' )";
         //                   EVNeed = "  and ( datafrom = 'EV' )"; 
         //               }
         //               else
         //               {
         //                   if (PA[15] != "") PA[15] = PA[15] + " or ";
         //                   if (PB[15] != "") PB[15] = PB[15] + " or ";
         //                   if (PA[31] != "") PA[31] = PA[31] + " or ";
         //
         //                   PA[15] = PA[15] + "   datafrom = '" + arrayParam[15, var2] + "' "; // datafrom
         //                   PB[15] = PB[15] + " a.datafrom = '" + arrayParam[15, var2] + "' "; // datafrom  
         //                   PA[31] = PA[31] + "   datafrom = '" + arrayParam[15, var2] + "' "; // datafrom
         // 
         //               }
         //
         //               if (arrayParam[15, var2] == "PV") RunPVflag = "Y";  // Get PV run flag
         //           }
         //       }
         //       PA[15] = " and ( " + PA[15] + " ) ";
         //       PB[15] = " and ( " + PB[15] + " ) ";
         //       PA[31] = " and ( " + PA[31] + " ) ";
         //   }

            if ((arrayParam[15, 1] == "ALL") || (arrayParam[15, 1] == ""))
            {
                PA[15] = "";         // Datafrom
                PB[15] = ""; // datafrom  
                PA[31] = ""; // datafrom
            }
            else                                                                                //
            {                                                                                   // 
                RunPVflag = "N";
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[15, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[15] != "") PA[15] = PA[15] + " or ";
                        if (PB[15] != "") PB[15] = PB[15] + " or ";
                        if (PA[31] != "") PA[31] = PA[31] + " or ";

                        PA[15] = PA[15] + "   datafrom = '" + arrayParam[15, var2] + "' "; // datafrom
                        PB[15] = PB[15] + " a.datafrom = '" + arrayParam[15, var2] + "' "; // datafrom  
                        PA[31] = PA[31] + "   datafrom = '" + arrayParam[15, var2] + "' "; // datafrom


                        if (arrayParam[15, var2] == "EV") EVNeed = "  and ( datafrom = 'EV' )";
                        if (arrayParam[15, var2] == "PV") RunPVflag = "Y";  // Get PV run flag
                    }
                }
                PA[15] = " and ( " + PA[15] + " ) ";
                PB[15] = " and ( " + PB[15] + " ) ";
                PA[31] = " and ( " + PA[31] + " ) ";
            }
       // 20110320    if ((arrayParam[14, 2] != "") && (arrayParam[14, 2] != null)) DownDVDay = arrayParam[14, 2];  // Get Para 14 RequireDay
       //             else DownDVDay = text1.Substring(0, 4) + text1.Substring(5, 2) + text1.Substring(8, 2); // text1= DateTime.Today.ToString("yyyy-MM-dd");

            if ((arrayParam[11, 1] == "ALL") || (arrayParam[11, 1] == "")) PA[11] = "";          // For 4A3_Detail Region 
            else
            {
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[11, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[11] != "") PA[11] = PA[11] + " or ";
                        PA[11] = PA[11] + "   FoxconnRegionCode = '" + arrayParam[11, var2] + "' ";
                        PB[11] = PB[11] + " a.FoxconnRegionCode = '" + arrayParam[11, var2] + "' ";
                    }
                }
                PA[11] = " and ( " + PA[11] + " ) ";
                PB[11] = " and ( " + PB[11] + " ) ";
            }


        // 20110522  FoxconnBU --> FoxconnBUGroup
            if ((arrayParam[08, 1] == "ALL") || (arrayParam[08, 1] == "")) PA[08] = "";          // For 4A3_Detail Region 
            else
            {
                for (var2 = 2; var2 < arrayParamLong + 1; var2++)
                {
                    if (arrayParam[08, var2] == "") var2 = arrayParamLong + 1;
                    else
                    {
                        if (PA[08] != "") PA[08] = PA[08] + " or ";
                        PA[08] = PA[08] + "   FoxconnBUGroup = '" + arrayParam[08, var2] + "' ";
                        PB[08] = PB[08] + " a.FoxconnBUGroup = '" + arrayParam[08, var2] + "' ";
                    }
                }
                PA[08] = " and ( " + PA[08] + " ) ";
                PB[08] = " and ( " + PB[08] + " ) ";
            }


            RunEVflag = "N";
            RunPVflag = "N";
            RunPOflag = "N";

            for (cntj = 1; cntj < 30; cntj++)  // Confirm DV flag
            {
                if (arrayParam[15, 1] == "ALL")
                {
                    RunEVflag = "Y";
                    RunPVflag = "Y";
                    RunPOflag = "Y";
                    cntj = 30;        // Break
                }
                else
                {
                    subs9 = arrayParam[15, cntj].ToString();
                    switch (subs9)
                    {
                        case "EV": RunEVflag = "Y"; break;
                        case "PV": RunPVflag = "Y"; break;
                        case "": break;
                        default: RunPOflag = "Y"; break;    //
                    }
                }

                if ((RunEVflag == "Y") && (RunPVflag == "Y") && (RunPOflag == "Y")) cntj = 30; // Break 
            }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // (5) NLV_NEWDV,          4A3_Detail       ( Forecast_Qty != 0 )
        // (6) NLV_NEWDV_4A5,      4A3_Detail_Site  ( Commit_Qty != 0 )
        // (7) NLV_NEWDV,          4A3_Detail_Site  ( Forecast_Qty != 0 )
        if (Reqprogflag == "5")
        {
            // Get Side GIT from 4A5_Detail_plant
            GIT4A5Str = " ";
       //     // 找最大  Document_ID 再由程式依依清除
       //     subtmpstr1 = "select a.Conversation_ID, a.Week, a.Document_ID, a.Forecast_CustomerPN, a.Forecast_QtyTypeCode, "
       //     + " a.Forecast_BeginDate, a.Forecast_IntervalCode, a.Forecast_Qty, a.Foxconn_Site, a.CustomerSite, a.Agreement, "
       //     + " a.Item, a.Datafrom, a.Project, a.DocumentTime, a.Customer, a.HHPARTS, a.FoxconnBU, a.FoxconnRegion "
       //     + " from   Syncro_4A3_Detail a, "
       //     + " ( select Forecast_CustomerPN, CustomerSite, Datafrom, Foxconn_Site, Forecast_BeginDate, max(Document_ID) as Document_ID "
       //     + " from Syncro_4A3_Detail Where documenttime like '" + DownDVDay + "'+'%'   "
       //     + PA[2] + PA[6] + PA[7] + PA[9] + PA[4] + PA[15]
       //     + " group by Datafrom, CustomerSite, Foxconn_Site, Forecast_CustomerPN, Forecast_BeginDate   ) as b "
       //     + " where a.Forecast_CustomerPN = b.Forecast_CustomerPN  "
       //     + " and   a.CustomerSite = b.CustomerSite  "
       //     + " and   a.Datafrom     = b.Datafrom      "
       //     + " and   a.Foxconn_Site = b.Foxconn_Site "
       //     + " and   a.Document_ID  = b.Document_ID   "
       //     + " and   a.Forecast_BeginDate = b.Forecast_BeginDate "
       //     + " order by a.Datafrom, a.CustomerSite, a.Foxconn_Site, a.Forecast_CustomerPN, a.Document_ID, a.Forecast_BeginDate  ";

            // Srart 5.1. Get 4A3_Detail
            subtmpstr1 = " SELECT c.Forecast_CustomerPN, c.Document_ID, c.Forecast_QtyTypeCode, c.Forecast_BeginDate, "
            + " c.Forecast_IntervalCode, c.Forecast_Qty, a.DocumentTime, a.CustomerSite, a.FoxconnRegion, a.FoxconnSite FROM Syncro_4A3_Detail_PNOneSet a, "
            + " ( select Forecast_CustomerPN, CustomerSite, FoxconnRegion, Max(Document_ID) as Document_ID from Syncro_4A3_Detail_PNOneSet "
            + " where documenttime like '" + DownDVDay + "'+'%' " + PA[15]  //+ EVNeed
            + PA[2] + PA[11] + PA[6] + PA[9] + PA[8]
            + " group by Forecast_CustomerPN, CustomerSite, FoxconnRegion ) as b, "
            + " Syncro_4a3_Detail c "
            + " where documenttime like '" + DownDVDay + "'+'%' "
                //+ " where substring(documenttime,1,8) = '20110225' group by Forecast_CustomerPN ) as b, "
                //+ " Syncro_4a3_Detail c "
                //+ " where substring(a.documenttime,1,8) = '20110225' "
            + " and a.Document_ID = b.Document_ID "
            + " and a.Forecast_CustomerPN = b.Forecast_CustomerPN "
            + " and a.Document_ID = c.Document_ID "
            + " and a.Forecast_CustomerPN = c.Forecast_CustomerPN "
            + " and c.Forecast_Qty <> '0' and c.Forecast_Qty <> '' " 
            // + " and  c.Forecast_QtyTypeCode = 'Discrete Gross Demand' "
            + " order by  a.Forecast_CustomerPN desc, a.Document_ID desc, a.CustomerSite desc, a.FoxconnRegion desc ";

            // Start 5.2 ( Max 4A3_Detail_PNOneSet )
            Sum4A3sql = " SELECT a.*  FROM Syncro_4A3_Detail_PNOneSet a,   "
            + "( select Forecast_CustomerPN, CustomerSite, FoxconnRegion, Max(Document_ID) as Document_ID from Syncro_4A3_Detail_PNOneSet "
            + "  where documenttime like '" + DownDVDay + "'+'%' " +  PA[15]  // + EVNeed
            + PA[2] + PA[11] + PA[6] + PA[9] + PA[8]
            + " group by Forecast_CustomerPN, CustomerSite, FoxconnRegion ) as b  "
            + "  where a.documenttime like '" + DownDVDay + "'+'%'  "
            + "  and a.Document_ID = b.Document_ID and a.Forecast_CustomerPN = b.Forecast_CustomerPN "
            // + PB[2] + PB[11] + PB[6] + PB[9]
            + "  order by  a.Forecast_CustomerPN desc, a.Document_ID desc ";

            // Start 5.3 Get Man
            SumNLV_T_NEWDVsql1 = " SELECT a.*  FROM NLV_T_NEWDV a,   "
            + "( select Customer_PN, Receiving_Site, Supplying_Site, Max(Document_ID) as Document_ID from NLV_T_NEWDV "
            + "  where substring(Document_ID,1,8) <= '" + DownDVDay + "'+'%' "
            + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "   
            + "   and (    datafrom = 'Man'   )  "
            // + PA[2] + PA[6] + PA[9]
            + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]   
            + " group by Customer_PN, Receiving_Site, Supplying_Site ) as b  "
            + "  where substring( a.Document_ID,1,8) <=  '" + DownDVDay + "'+'%'  "
            + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  " // + PA[31] 
            + "  and a.Document_ID = b.Document_ID and a.Customer_PN = b.Customer_PN "
            + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
            // 20110321 + "  and a.Forecast_Qty <> '0' "
            + "  and (  a.datafrom = 'Man'  )  "
            + "  order by  a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Document_ID  ";

           
            // Start 5.4 Get FSC
            SumNLV_T_NEWDVsql2 = " SELECT a.*  FROM NLV_T_NEWDV a,   "
           + "( select Customer_PN, Receiving_Site, Supplying_Site, Max(Document_ID) as Document_ID from NLV_T_NEWDV "
           + "  where substring(Document_ID,1,8)  <= '" + DownDVDay + "'+'%' "
           + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  " 
           + "   and (    datafrom = 'FSC'  )  "
           + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]   
           + " group by Customer_PN, Receiving_Site, Supplying_Site ) as b  "
           + "  where substring( a.Document_ID,1,8)  <=  '" + DownDVDay + "'+'%'  "
           + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
           + "  and a.Document_ID = b.Document_ID and a.Customer_PN = b.Customer_PN "
           + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
           // 20110321 + "  and a.Forecast_Qty <> '0' "
           + "  and (    a.datafrom = 'FSC'  )  "
           + "  order by  a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Document_ID  ";

            //  Start 5.5 Get <>Man <>FSC
            SumNLV_T_NEWDVsql3 = " SELECT a.*  FROM NLV_T_NEWDV a,   "
            // + "( select Customer_PN, Receiving_Site, Supplying_Site, Convert( varchar, UPLOADTIME, 112),Max(Document_ID) as Document_ID from NLV_T_NEWDV "
            + "( select datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item, Max(Ticket) as Ticket from NLV_T_NEWDV "
            + "  where substring(Document_ID,1,8) <= '" + DownDVDay + "'+'%' "
            + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + " and (    datafrom <> 'Man'  and    datafrom <> 'FSC'  )  "
            + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15] 
            + " group by datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item  ) as b  "
            + "  where substring( a.Document_ID,1,8) <=  '" + DownDVDay + "'+'%'  "
            + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + "  and a.Customer_PN = b.Customer_PN and a.Ticket = b.Ticket "
            + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
            // 20110321 + "  and a.Forecast_Qty <> '0' "
            + "  and (    a.datafrom <> 'Man'  and   a.datafrom <> 'FSC'  )  "
            + "  order by  a.datafrom, a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Ticket ";

        }  // end programflag = "5"

        ////////////////////////////////////////////////////////////////////////////////////
        if (Reqprogflag == "6")    // SIte 4a5_Detail_Plant Query
        {
            
                    
            GIT4A5Str = "";

            // Srart 6.1  Get 4A5_Detail_Plant
            subtmpstr1 = " SELECT c.Forecast_CustomerPN, c.Document_ID, c.Forecast_QtyTypeCode, c.Forecast_BeginDate, "
            + " substring (substring(c.Foxconn_Site,11,len(c.Foxconn_Site)),0,CHARINDEX(':',substring(c.Foxconn_Site,11,len(c.Foxconn_Site))))  FoxSite, "
       //   + " c.Forecast_IntervalCode, c.Forecast_Qty, a.DocumentTime, a.CustomerSite, a.FoxconnRegion FROM Syncro_4A3_Detail_PNOneSet a, "
            + " c.Forecast_IntervalCode, c.Site_Net_Demand , a.DocumentTime, a.CustomerSite, a.FoxconnRegion FROM Syncro_4A3_Detail_PNOneSet a, "
            + " ( select Forecast_CustomerPN, CustomerSite, FoxconnRegion, Max(Document_ID) as Document_ID from Syncro_4A3_Detail_PNOneSet "
            + " where documenttime like '" + DownDVDay + "'+'%'  "
            + PA[2] + PA[11] + PA[6] + PA[9] + PA[15] + PA[08]    
            + " group by Forecast_CustomerPN, CustomerSite, FoxconnRegion ) as b, "
            + " Syncro_4A5_Detail_Site c "
            + " where documenttime like '" + DownDVDay + "'+'%' "
            + PB[4]
            + " and a.Document_ID = b.Document_ID "
            + " and a.Forecast_CustomerPN = b.Forecast_CustomerPN "
            + " and a.Document_ID = c.Document_ID "
            + " and a.Forecast_CustomerPN = c.Forecast_CustomerPN "
            + " and c.Forecast_Qty <> '0' and c.Forecast_Qty <> '' "                // + " and  c.Forecast_QtyTypeCode = 'Discrete Gross Demand' "
            + " order by  a.Forecast_CustomerPN desc, a.Document_ID desc, a.CustomerSite desc, c.Foxconn_Site desc"; // a.FoxconnRegion desc

            // Start 6.2 ( Max 4A3_Detail_PNOneSet ) Site
            Sum4A3sql = " SELECT distinct a.*, substring(substring(c.Foxconn_Site,11,len(c.Foxconn_Site)),0,CHARINDEX(':',substring(c.Foxconn_Site,11,len(c.Foxconn_Site))))  FoxSite " 
            +" FROM Syncro_4A3_Detail_PNOneSet a, Syncro_4A5_Detail_Site c,  "
            + "( select Forecast_CustomerPN, CustomerSite, FoxconnRegion, Max(Document_ID) as Document_ID from Syncro_4A3_Detail_PNOneSet "
            + "  where documenttime like '" + DownDVDay + "'+'%' "
            + PA[2] + PA[11] + PA[6] + PA[9] + PA[15] + PA[08]  
            + " group by Forecast_CustomerPN, CustomerSite, FoxconnRegion ) as b  "
            + "  where a.documenttime like '" + DownDVDay + "'+'%'  "
            + PB[4]
            + "  and a.Document_ID = b.Document_ID and a.Forecast_CustomerPN = b.Forecast_CustomerPN "
            + "  and b.Document_ID = c.Document_ID and b.Forecast_CustomerPN = c.Forecast_CustomerPN "
            + "  order by  a.Forecast_CustomerPN desc, a.Document_ID desc, a.CustomerSite desc, FoxSite desc ";

            // Start 6.3 
            SumNLV_T_NEWDVsql1 = " SELECT a.*  FROM NLV_T_NEWDV_4A5_Site a,   "
            + "( select Customer_PN, Receiving_Site, Supplying_Site, Max(Document_ID) as Document_ID from NLV_T_NEWDV_4A5_Site "
            + "  where substring(Document_ID,1,8)  <= '" + DownDVDay + "'+'%' "
            + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + "   and (    datafrom = 'Man'   )  "
            + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]
            + " group by Customer_PN, Receiving_Site, Supplying_Site ) as b  "
            + "  where substring( a.Document_ID,1,8) <=  '" + DownDVDay + "'+'%'  "
            + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + "  and a.Document_ID = b.Document_ID and a.Customer_PN = b.Customer_PN "
            + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
            // 20110321 + "  and a.Forecast_Qty <> '0' "
            + "  and (  a.datafrom = 'Man'  )  "
            + "  order by  a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Document_ID  ";

            // Start 6.4 
            SumNLV_T_NEWDVsql2 = " SELECT a.*  FROM NLV_T_NEWDV_4A5_Site a,   "
           + "( select Customer_PN, Receiving_Site, Supplying_Site, Max(Document_ID) as Document_ID from NLV_T_NEWDV_4A5_Site "
           + "  where substring(Document_ID,1,8)  <= '" + DownDVDay + "'+'%' "
           + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
           + "   and (    datafrom = 'FSC'  )  " 
           //+ PA[2] + PA[6] + PA[9] + PA[31]
           + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]
           + " group by Customer_PN, Receiving_Site, Supplying_Site ) as b  "
           + "  where substring( a.Document_ID,1,8)  <=  '" + DownDVDay + "'+'%'  "
           + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
           + "  and a.Document_ID = b.Document_ID and a.Customer_PN = b.Customer_PN "
           + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
           // 20110321 + "  and a.Forecast_Qty <> '0' "
           + "  and (    a.datafrom = 'FSC'  )  "
           + "  order by  a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Document_ID  ";

            // Start 6.5
            SumNLV_T_NEWDVsql3 = " SELECT a.*  FROM NLV_T_NEWDV_4A5_Site a,   "
                // + "( select Customer_PN, Receiving_Site, Supplying_Site, Convert( varchar, UPLOADTIME, 112),Max(Document_ID) as Document_ID from NLV_T_NEWDV "
            + "( select datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item, Max(Ticket) as Ticket from NLV_T_NEWDV_4A5_Site "
            + "  where substring(Document_ID,1,8) <= '" + DownDVDay + "'+'%' "
            + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + " and (    datafrom <> 'Man'  and    datafrom <> 'FSC'  )  "
            + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]
            + " group by datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item  ) as b  "
            + "  where substring( a.Document_ID,1,8) <=  '" + DownDVDay + "'+'%'  "
            + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
                //  + "  and a.Document_ID = b.Document_ID  "
            + "  and a.Customer_PN = b.Customer_PN and a.Ticket = b.Ticket "
            + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
            // 20110321 + "  and a.Forecast_Qty <> '0' "
            + "  and (    a.datafrom <> 'Man'  and   a.datafrom <> 'FSC'  )  "
            + "  order by  a.datafrom, a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Ticket ";

        }  // end programflag = "6"

        if (Reqprogflag == "7")    // SIte 4a5_Detail_Plant Query
        {
            GIT4A5Str = "";

            // Srart 7.1  Get 4A5_Detail_Plant
            subtmpstr1 = " SELECT c.Forecast_CustomerPN, c.Document_ID, c.Forecast_QtyTypeCode, c.Forecast_BeginDate, "
            + " substring (substring(c.Foxconn_Site,11,len(c.Foxconn_Site)),0,CHARINDEX(':',substring(c.Foxconn_Site,11,len(c.Foxconn_Site))))  FoxSite, "
            + " c.Forecast_IntervalCode, c.Forecast_Qty, a.DocumentTime, a.CustomerSite, a.FoxconnRegion FROM Syncro_4A3_Detail_PNOneSet a, "
         // + " c.Forecast_IntervalCode, c.Site_Net_Demand , a.DocumentTime, a.CustomerSite, a.FoxconnRegion FROM Syncro_4A3_Detail_PNOneSet a, "
            + " ( select Forecast_CustomerPN, CustomerSite, FoxconnRegion, Max(Document_ID) as Document_ID from Syncro_4A3_Detail_PNOneSet "
            + " where documenttime like '" + DownDVDay + "'+'%'  "
            + PA[2] + PA[11] + PA[6] + PA[9] + PA[15] + PA[08]  
            + " group by Forecast_CustomerPN, CustomerSite, FoxconnRegion ) as b, "
            + " Syncro_4A5_Detail_Site c "
            + " where documenttime like '" + DownDVDay + "'+'%' "
            + PB[4]
            + " and a.Document_ID = b.Document_ID "
            + " and a.Forecast_CustomerPN = b.Forecast_CustomerPN "
            + " and a.Document_ID = c.Document_ID "
            + " and a.Forecast_CustomerPN = c.Forecast_CustomerPN "
            + " and c.Forecast_Qty <> '0' and c.Forecast_Qty <> '' "                // + " and  c.Forecast_QtyTypeCode = 'Discrete Gross Demand' "
            + " order by  a.Forecast_CustomerPN desc, a.Document_ID desc, a.CustomerSite desc, c.Foxconn_Site desc"; // a.FoxconnRegion desc

            // Start 7.2 ( Max 4A3_Detail_PNOneSet ) Site
            Sum4A3sql = " SELECT distinct a.*, substring(substring(c.Foxconn_Site,11,len(c.Foxconn_Site)),0,CHARINDEX(':',substring(c.Foxconn_Site,11,len(c.Foxconn_Site))))  FoxSite "
            + " FROM Syncro_4A3_Detail_PNOneSet a, Syncro_4A5_Detail_Site c,  "
            + "( select Forecast_CustomerPN, CustomerSite, FoxconnRegion, Max(Document_ID) as Document_ID from Syncro_4A3_Detail_PNOneSet "
            + "  where documenttime like '" + DownDVDay + "'+'%' "
            + PA[2] + PA[11] + PA[6] + PA[9] + PA[15] + PA[08]  
            + " group by Forecast_CustomerPN, CustomerSite, FoxconnRegion ) as b  "
            + "  where a.documenttime like '" + DownDVDay + "'+'%'  "
            + PB[4]
            + "  and a.Document_ID = b.Document_ID and a.Forecast_CustomerPN = b.Forecast_CustomerPN "
            + "  and b.Document_ID = c.Document_ID and b.Forecast_CustomerPN = c.Forecast_CustomerPN "
            + "  order by  a.Forecast_CustomerPN desc, a.Document_ID desc, a.CustomerSite desc, FoxSite desc ";

            // Start 7.3 
            SumNLV_T_NEWDVsql1 = " SELECT a.*  FROM NLV_T_NEWDV a,   "
            + "( select Customer_PN, Receiving_Site, Supplying_Site, Max(Document_ID) as Document_ID from NLV_T_NEWDV "
            + "  where substring(Document_ID,1,8)  <= '" + DownDVDay + "'+'%' "
            + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + "   and (    datafrom = 'Man'   )  "
            + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]
            + " group by Customer_PN, Receiving_Site, Supplying_Site ) as b  "
            + "  where substring( a.Document_ID,1,8) <=  '" + DownDVDay + "'+'%'  "
            + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + "  and a.Document_ID = b.Document_ID and a.Customer_PN = b.Customer_PN "
            + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
                // 20110321 + "  and a.Forecast_Qty <> '0' "
            + "  and (  a.datafrom = 'Man'  )  "
            + "  order by  a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Document_ID  ";

            // Start 7.4 
            SumNLV_T_NEWDVsql2 = " SELECT a.*  FROM NLV_T_NEWDV a,   "
           + "( select Customer_PN, Receiving_Site, Supplying_Site, Max(Document_ID) as Document_ID from NLV_T_NEWDV "
           + "  where substring(Document_ID,1,8)  <= '" + DownDVDay + "'+'%' "
           + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
           + "   and (    datafrom = 'FSC'  )  "
                //+ PA[2] + PA[6] + PA[9] + PA[31]
           + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]
           + " group by Customer_PN, Receiving_Site, Supplying_Site ) as b  "
           + "  where substring( a.Document_ID,1,8)  <=  '" + DownDVDay + "'+'%'  "
           + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
           + "  and a.Document_ID = b.Document_ID and a.Customer_PN = b.Customer_PN "
           + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
                // 20110321 + "  and a.Forecast_Qty <> '0' "
           + "  and (    a.datafrom = 'FSC'  )  "
           + "  order by  a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Document_ID  ";

            // Start 7.5
            SumNLV_T_NEWDVsql3 = " SELECT a.*  FROM NLV_T_NEWDV a,   "
                // + "( select Customer_PN, Receiving_Site, Supplying_Site, Convert( varchar, UPLOADTIME, 112),Max(Document_ID) as Document_ID from NLV_T_NEWDV "
            + "( select datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item, Max(Ticket) as Ticket from NLV_T_NEWDV "
            + "  where substring(Document_ID,1,8) <= '" + DownDVDay + "'+'%' "
            + "  and   substring(Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
            + " and (    datafrom <> 'Man'  and    datafrom <> 'FSC'  )  "
            + NEWDV[2] + NEWDV[4] + NEWDV[6] + PA[9] + PA[15]
            + " group by datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item  ) as b  "
            + "  where substring( a.Document_ID,1,8) <=  '" + DownDVDay + "'+'%'  "
            + "  and   substring( a.Document_ID,1,8) >=  '" + DownDVBeginDay + "'+'%'  "
                //  + "  and a.Document_ID = b.Document_ID  "
            + "  and a.Customer_PN = b.Customer_PN and a.Ticket = b.Ticket "
            + "  and a.Customer_PN <> '' and  a.Document_ID <> '' "
                // 20110321 + "  and a.Forecast_Qty <> '0' "
            + "  and (    a.datafrom <> 'Man'  and   a.datafrom <> 'FSC'  )  "
            + "  order by  a.datafrom, a.Customer_PN, a.Receiving_Site, a.Supplying_Site, a.Ticket ";



        }  // end programflag = "7"


        return (tmpDocumentID);
    }      // end TransWebParamToTable
       

    // write to GSCMDOrgShipPlan 4A3 memory 20110308
    private void GSCMDOrg4A3TransDayToShipPlan(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayVar)  //    
    {
        int lvar31 = 1;
        int CurrShipPlanLine = 0;
        int first4loc = 0;
        int prefirst4loc = 0;
        int first4W1 = 0;
        int locvar1 = 0;
        int locvar2 = 0;
        int locvar3 = 0;
        int loopcnt = 7;
        int readheadloc = 0;
        int readtailloc = 0;
        int subtot = 0;
        int tottot = 0;
        int tmpMPQ = 0;

        DateTime ltmpdate1 = DateTime.Today;
        string locsql1 = "";
        string locsql2 = "";
        string tmpp1, tmpd1, tmpr1, tmpr2, tmpr3;
        string s1, s2, s3, s4, s5, s6, s7, s8, d9;

        string tmptransDay = "";
        string tmpShipPlan = "";

        //                    周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
        // tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEE"; // first4loc 加地第一個周4
        // tmpShipPlan = "007008009010011012013014015016017018019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057";    // first4loc


        //                    周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
        tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEEEEEEEE"; // first4loc 加地第一個周4
        tmpShipPlan = "025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057058059060061062063064065066067068069070071072073074075076077";    // first4loc
        


        CurrShipPlanLine = Convert.ToInt32(locrefstr1);                            // 第幾個 Array

        if (CurrShipPlanLine == 31)
            CurrShipPlanLine = CurrShipPlanLine;

        arrayShipPlan[CurrShipPlanLine, 00] = locrefstr1; // NO
        arrayShipPlan[CurrShipPlanLine, 01] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 04].ToString(); // Dpcument_ID Nokia
        arrayShipPlan[CurrShipPlanLine, 02] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom  
        arrayShipPlan[CurrShipPlanLine, 03] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 03].ToString(); // CustomerPN  
        arrayShipPlan[CurrShipPlanLine, 04] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 01].ToString(); // CustomerSite  
        arrayShipPlan[CurrShipPlanLine, 05] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 00].ToString(); // Project
        arrayShipPlan[CurrShipPlanLine, 06] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 29].ToString(); // Description  
        arrayShipPlan[CurrShipPlanLine, 07] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 60].ToString(); // Agreement
        arrayShipPlan[CurrShipPlanLine, 08] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 61].ToString(); // Item
        arrayShipPlan[CurrShipPlanLine, 09] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 02].ToString(); // FoxconnSite  
        arrayShipPlan[CurrShipPlanLine, 10] = "N"; // CommonParts
        arrayShipPlan[CurrShipPlanLine, 11] = "Demand"; // QtyDataType
        arrayShipPlan[CurrShipPlanLine, 12] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 62].ToString(); // OnHand  
        arrayShipPlan[CurrShipPlanLine, 13] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 63].ToString(); // Consigned  
        arrayShipPlan[CurrShipPlanLine, 14] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 64].ToString(); // GITToT  
        arrayShipPlan[CurrShipPlanLine, 15] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 65].ToString(); // GIT BJ
        arrayShipPlan[CurrShipPlanLine, 16] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 66].ToString(); // GIT LF  
        arrayShipPlan[CurrShipPlanLine, 17] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 67].ToString(); // GIT LH
        arrayShipPlan[CurrShipPlanLine, 18] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 68].ToString(); // GIT Chennai
        arrayShipPlan[CurrShipPlanLine, 19] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 69].ToString(); // Block  
        arrayShipPlan[CurrShipPlanLine, 20] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 70].ToString(); // NO
        arrayShipPlan[CurrShipPlanLine, 21] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 71].ToString(); // Dpcument_ID Nokia
        arrayShipPlan[CurrShipPlanLine, 22] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 72].ToString(); // datafrom  
        arrayShipPlan[CurrShipPlanLine, 23] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 73].ToString(); // CustomerPN  
        arrayShipPlan[CurrShipPlanLine, 24] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 74].ToString(); // CustomerSite  
                                                                                                               // EV Array
        arrayShipPlan[CurrShipPlanLine, 76] = (Convert.ToInt32(locrefstr1) + 1001).ToString();                 // Seqno 
        arrayShipPlan[CurrShipPlanLine, 77] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 28].ToString(); // DocumentID ret 
        arrayShipPlan[CurrShipPlanLine, 78] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 05].ToString(); // DocumentTime
        arrayShipPlan[CurrShipPlanLine, 79] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 08].ToString(); // HHPN 
        
        
        //arrayShipPlan[CurrShipPlanLine, 0] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 0].ToString(); // Project
        //arrayShipPlan[CurrShipPlanLine, 1] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 1].ToString(); // Customer
        //s1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 29].ToString(); // Description
        //arrayShipPlan[CurrShipPlanLine, 2] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 29].ToString(); // Description  
        //arrayShipPlan[CurrShipPlanLine, 3] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 3].ToString(); // CustomerPN  
        //arrayShipPlan[CurrShipPlanLine, 4] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 8].ToString(); // HHPN  
        //arrayShipPlan[CurrShipPlanLine, 5] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 2].ToString(); // FoxconnSite  
        //arrayShipPlan[CurrShipPlanLine, 6] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 9].ToString(); // Dom_Exp  
        //arrayShipPlan[CurrShipPlanLine, 57]= arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 28].ToString(); // DocumentID  
        if ( CurrShipPlanLine >= 420 )
             locsql1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom
      
         
        // F00:    Project
        // F01:    CustomerSite
        // F02:    FoxconnRegion           
        // F03:    Forecast_CustomerPN
        // F04:    Document_ID
        // F05:    DocumentTime
        // F06:    Min
        // F07:    Max
        // F08:    HHPN
        // F09:    Dom_Exp
        // F10:    Reserved
        // F11:    Reserved
        // F28:    Return DoucmentID
        // F29:    Desp
        // F56:    datafrom
        // F57:    DocumentID ( Private )
        // F60:    Consigned         / Consigned Inventory  ( 130-10+1 )
        // F61:    GIT               / GIT                  ( 130-10+2 )
        // F62:    On-Hand Inventory / On-Hand Inventory    ( 130-10+3 )
        // F63:    Agreement                                ( 130-10+4 )
        // F64:    Item                                     ( 130-10+5 )
        // F65:    GIT BJ                                   ( 130-10-7 )
        // F66:    GIT LF                                   ( 130-10-8 ) 
        // F67:    GIT LH                                   ( 130-10-9 )
        // F68:    GIT Chennai                              ( 130-10-10 )
        // F69:    Block    / Blocked Stock                   ( 130-10-1)
        // F70:    Quality  / Quality Stock                   ( 130-10-2)
        // F71:    Min Days / Minimum Days of Supply Target   ( 130-10-3)
        // F72:    Max Days / Maximum Days of Supply Target   ( 130-10-4) 
        // F73:    Min Inventory  / Minimum Inventory Target  ( 130-10-5)
        // F74:    Max Inventory  / Maximum Inventory Target  ( 130-10-6)
        // F75:    DocumentID ( Private )

      //  for ( locvar1=60; locvar1 <= 80; locvar1++ )
      //      arrayShipPlan[CurrShipPlanLine, locvar1] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, locvar1].ToString(); //   
        // arrayShipPlan[CurrShipPlanLine, 61] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 + 2].ToString(); // GIT  
        // arrayShipPlan[CurrShipPlanLine, 62] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 + 3].ToString(); // On_hand  
        // arrayShipPlan[CurrShipPlanLine, 63] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 + 4].ToString(); // Agreement  
        // arrayShipPlan[CurrShipPlanLine, 64] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 + 5].ToString(); // Item  
        // arrayShipPlan[CurrShipPlanLine, 69] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 1].ToString(); // Blocked Stock  
        // arrayShipPlan[CurrShipPlanLine, 70] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 2].ToString(); // Quality Stock   
        // arrayShipPlan[CurrShipPlanLine, 71] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 3].ToString(); // Minimum Days of Supply Target   
        // arrayShipPlan[CurrShipPlanLine, 72] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 4].ToString(); // Maximum Days of Supply Target  
        // arrayShipPlan[CurrShipPlanLine, 73] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 5].ToString(); // Minimum Inventory Target  
        // arrayShipPlan[CurrShipPlanLine, 74] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 6].ToString(); // Maximum Inventory Target  
        // arrayShipPlan[CurrShipPlanLine, 65] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 7].ToString(); // Blocked Stock  
        // arrayShipPlan[CurrShipPlanLine, 66] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 8].ToString(); // Quality Stock   
        // arrayShipPlan[CurrShipPlanLine, 67] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 9].ToString(); // Minimum Days of Supply Target   
        // arrayShipPlan[CurrShipPlanLine, 68] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arrayCustomerFoxconnPNToOneSetIndex - 10 - 10].ToString(); // Maximum Days of Supply Target  


        // arrayShipPlan[CurrShipPlanLine, 58] = arrayVar[arrayVarX + 1, CurrShipPlanLine].ToString(); // NetQty 20100704  
        arrayShipPlan[CurrShipPlanLine, 58] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arraytransSecondAvailLoc].ToString(); // NetQty 20100704  

        //
        locvar1 = Convert.ToInt32(arraytransDay[tmptodaylocation, 10].ToString()); // 星期幾 ? 找周4

        if (locvar1 == 0) first4loc = tmptodaylocation - 7 + 4;                    // 第一個周4位置
        else if (locvar1 >= 4) first4loc = tmptodaylocation - locvar1 + 4;       //
        else first4loc = tmptodaylocation - locvar1 + 4 - 7;   //

        prefirst4loc = first4loc - 1; // count from 0
        readheadloc = prefirst4loc;
        readtailloc = 3 * 0;  //  3 * 1;
        int firstW1 = prefirst4loc + 4 + 21;


        while (tmptransDay.Substring(readtailloc, 3) != "EEE")
        {

            s1 = tmptransDay.Substring(readtailloc, 3);    // 此為第一個周4, 所以須加  first4loc
            s2 = tmpShipPlan.Substring(readtailloc, 3);    // 此為 OutPut array 位置

            if (s1.Substring(0, 1).ToUpper() == "E")
                arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = tottot.ToString();
            else
            if (s1.Substring(0, 1).ToUpper() == "C")
            {
                arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
                subtot = 0;
            }
            else
            if (s1.Substring(0, 1).ToUpper() == "W")
            {
                subtot = 0;
                s3 = s1.Substring(1, 2);
                locvar2 = firstW1 + Convert.ToInt32(s3) * 7 - 7;  // Week 起始日往後加 7 天
                for (locvar1 = 0; locvar1 < 7; locvar1++)
                    subtot = subtot + Convert.ToInt32(arraytransDay[locvar2 + locvar1, 4]); // 加一周  Org 27 

                    // if (  subtot > 0 )
                    //       subtot =  subtot ;
                arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
                tottot = subtot + tottot;
                subtot = 0;
            }
            else
            {
                tmpMPQ = Convert.ToInt32(arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString());
                arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString();
                subtot = tmpMPQ + subtot;
                tottot = tmpMPQ + tottot;
            }  // end if 
            readtailloc = readtailloc + 3;

        }  // end while    

    }  // end of GSCMDOrg4A3TransDayToShipPlan

    // write to GSCMDOrgShipPlan 4A5 memory 20110308
    private void GSCMDOrg4A5TransDayToShipPlan(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayVar)  //    
    {
        int lvar31 = 1;
        int CurrShipPlanLine = 0;
        int first4loc = 0;
        int prefirst4loc = 0;
        int first4W1 = 0;
        int locvar1 = 0;
        int locvar2 = 0;
        int locvar3 = 0;
        int loopcnt = 7;
        int readheadloc = 0;
        int readtailloc = 0;
        int subtot = 0;
        int tottot = 0;
        int tmpMPQ = 0;

        DateTime ltmpdate1 = DateTime.Today;
        string locsql1 = "";
        string locsql2 = "";
        string tmpp1, tmpd1, tmpr1, tmpr2, tmpr3;
        string s1, s2, s3, s4, s5, s6, s7, s8, d9;

        string tmptransDay = "";
        string tmpShipPlan = "";

        //                    周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
        // tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEE"; // first4loc 加地第一個周4
        // tmpShipPlan = "007008009010011012013014015016017018019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057";    // first4loc


        //                    周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
        tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEEEEEEEE"; // first4loc 加地第一個周4
        tmpShipPlan = "011012013014015016017018019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057058059060061062063";    // first4loc

        CurrShipPlanLine = Convert.ToInt32(locrefstr1);                            // 第幾個 Array

        if (CurrShipPlanLine == 31)
            CurrShipPlanLine = CurrShipPlanLine;

        if (arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 03].ToString() == "9904470")
            s1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 60].ToString(); 

        arrayShipPlan[CurrShipPlanLine, 00] = locrefstr1; // NO
        arrayShipPlan[CurrShipPlanLine, 03] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 04].ToString(); // Dpcument_ID Nokia
        arrayShipPlan[CurrShipPlanLine, 02] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 05].ToString(); // DocumentTime
        arrayShipPlan[CurrShipPlanLine, 01] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom  
        arrayShipPlan[CurrShipPlanLine, 04] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 03].ToString(); // CustomerPN  
        arrayShipPlan[CurrShipPlanLine, 05] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 01].ToString(); // CustomerSite  
        arrayShipPlan[CurrShipPlanLine, 09] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 00].ToString(); // Project
       // arrayShipPlan[CurrShipPlanLine, 06] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 29].ToString(); // Description  
        arrayShipPlan[CurrShipPlanLine, 07] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 60].ToString(); // Agreement
        arrayShipPlan[CurrShipPlanLine, 08] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 61].ToString(); // Item
        arrayShipPlan[CurrShipPlanLine, 06] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 02].ToString(); // FoxconnSite  
        arrayShipPlan[CurrShipPlanLine, 10] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 08].ToString(); // HHPN 
        arrayShipPlan[CurrShipPlanLine, 76] = (Convert.ToInt32(locrefstr1) + 1001).ToString();                 // Seqno 
        arrayShipPlan[CurrShipPlanLine, 77] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 28].ToString(); // DocumentID ret 
        

     //   arrayShipPlan[CurrShipPlanLine, 10] = "N"; // CommonParts
     //   arrayShipPlan[CurrShipPlanLine, 11] = "Demand"; // QtyDataType
     //   arrayShipPlan[CurrShipPlanLine, 12] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 62].ToString(); // OnHand  
     //   arrayShipPlan[CurrShipPlanLine, 13] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 63].ToString(); // Consigned  
     //   arrayShipPlan[CurrShipPlanLine, 14] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 64].ToString(); // GITToT  
     //   arrayShipPlan[CurrShipPlanLine, 15] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 65].ToString(); // GIT BJ
     //   arrayShipPlan[CurrShipPlanLine, 16] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 66].ToString(); // GIT LF  
     //   arrayShipPlan[CurrShipPlanLine, 17] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 67].ToString(); // GIT LH
     //   arrayShipPlan[CurrShipPlanLine, 18] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 68].ToString(); // GIT Chennai
     //   arrayShipPlan[CurrShipPlanLine, 19] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 69].ToString(); // Block  
     //   arrayShipPlan[CurrShipPlanLine, 20] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 70].ToString(); // NO
     //   arrayShipPlan[CurrShipPlanLine, 21] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 71].ToString(); // Dpcument_ID Nokia
     //   arrayShipPlan[CurrShipPlanLine, 22] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 72].ToString(); // datafrom  
     //   arrayShipPlan[CurrShipPlanLine, 23] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 73].ToString(); // CustomerPN  
     //   arrayShipPlan[CurrShipPlanLine, 24] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 74].ToString(); // CustomerSite  
        // EV Array
     //   arrayShipPlan[CurrShipPlanLine, 76] = (Convert.ToInt32(locrefstr1) + 1001).ToString();                 // Seqno 
     //   arrayShipPlan[CurrShipPlanLine, 77] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 28].ToString(); // DocumentID ret 
        

        if (CurrShipPlanLine >= 420)
            locsql1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom


        // F00:    NO
        // F01:    datafrom Project
        // F02     DocumentTime:    CustomerSite
        // F03:    Document_ID FoxconnRegion           
        // F04:    Forecast_CustomerPN
        // F05:    CustomerSite
        // F06:    FoxconnSite
        // F07:    Agreement        // F07:    Max
        // F08:    Item
        // F09:    Project
        // F10:    HHPN
        // F11:    Reserved
        // F28:    Return DoucmentID
        // F29:    Desp
        // F56:    datafrom
        // F57:    DocumentID ( Private )
        // F60:    Agreement                                ( 130-10+4 )
        // F61:    Item                                     ( 130-10+5 )
        // F62:    Consigned         / Consigned Inventory  ( 130-10+1 )
        // F63:    GIT               / GIT                  ( 130-10+2 )
        // F64:    On-Hand Inventory / On-Hand Inventory    ( 130-10+3 )
        // F65:    GIT BJ                                   ( 130-10-7 )
        // F66:    GIT LF                                   ( 130-10-8 ) 
        // F67:    GIT LH                                   ( 130-10-9 )
        // F68:    GIT Chennai                              ( 130-10-10 )
        // F69:    Block    / Blocked Stock                   ( 130-10-1)
        // F70:    Quality  / Quality Stock                   ( 130-10-2)
        // F71:    Min Days / Minimum Days of Supply Target   ( 130-10-3)
        // F72:    Max Days / Maximum Days of Supply Target   ( 130-10-4) 
        // F73:    Min Inventory  / Minimum Inventory Target  ( 130-10-5)
        // F74:    Max Inventory  / Maximum Inventory Target  ( 130-10-6)
        // F75:    DocumentID ( Private )

       
        // arrayShipPlan[CurrShipPlanLine, 58] = arrayVar[arrayVarX + 1, CurrShipPlanLine].ToString(); // NetQty 20100704  
        arrayShipPlan[CurrShipPlanLine, 58] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arraytransSecondAvailLoc].ToString(); // NetQty 20100704  

        //
        locvar1 = Convert.ToInt32(arraytransDay[tmptodaylocation, 10].ToString()); // 星期幾 ? 找周4

        if (locvar1 == 0) first4loc = tmptodaylocation - 7 + 4;                    // 第一個周4位置
        else if (locvar1 >= 4) first4loc = tmptodaylocation - locvar1 + 4;       //
        else first4loc = tmptodaylocation - locvar1 + 4 - 7;   //

        prefirst4loc = first4loc - 1; // count from 0
        readheadloc = prefirst4loc;
        readtailloc = 3 * 0;  //  3 * 1;
        int firstW1 = prefirst4loc + 4 + 21;


        while (tmptransDay.Substring(readtailloc, 3) != "EEE")
        {

            s1 = tmptransDay.Substring(readtailloc, 3);    // 此為第一個周4, 所以須加  first4loc
            s2 = tmpShipPlan.Substring(readtailloc, 3);    // 此為 OutPut array 位置

            if (s1.Substring(0, 1).ToUpper() == "E")
                arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = tottot.ToString();
            else
                if (s1.Substring(0, 1).ToUpper() == "C")
                {
                    arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
                    subtot = 0;
                }
                else
                    if (s1.Substring(0, 1).ToUpper() == "W")
                    {
                        subtot = 0;
                        s3 = s1.Substring(1, 2);
                        locvar2 = firstW1 + Convert.ToInt32(s3) * 7 - 7;  // Week 起始日往後加 7 天
                        for (locvar1 = 0; locvar1 < 7; locvar1++)
                            subtot = subtot + Convert.ToInt32(arraytransDay[locvar2 + locvar1, 4]); // 加一周  Org 27 

                        // if (  subtot > 0 )
                        //       subtot =  subtot ;
                        arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
                        tottot = subtot + tottot;
                        subtot = 0;
                    }
                    else
                    {
                        tmpMPQ = Convert.ToInt32(arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString());
                        arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString();
                        subtot = tmpMPQ + subtot;
                        tottot = tmpMPQ + tottot;
                    }  // end if 
            readtailloc = readtailloc + 3;

        }  // end while    

    }  // end of GSCMDOrg4A5TransDayToShipPlan

    // write to GSCMDOrgShipPlan 4A5 memory 20110308
    private void GSCMDOrg4A7TransDayToShipPlan(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayVar)  //    
    {
        int lvar31 = 1;
        int CurrShipPlanLine = 0;
        int first4loc = 0;
        int prefirst4loc = 0;
        int first4W1 = 0;
        int locvar1 = 0;
        int locvar2 = 0;
        int locvar3 = 0;
        int loopcnt = 7;
        int readheadloc = 0;
        int readtailloc = 0;
        int subtot = 0;
        int tottot = 0;
        int tmpMPQ = 0;

        DateTime ltmpdate1 = DateTime.Today;
        string locsql1 = "";
        string locsql2 = "";
        string tmpp1, tmpd1, tmpr1, tmpr2, tmpr3;
        string s1, s2, s3, s4, s5, s6, s7, s8, d9;

        string tmptransDay = "";
        string tmpShipPlan = "";

        //                    周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
        // tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEE"; // first4loc 加地第一個周4
        // tmpShipPlan = "007008009010011012013014015016017018019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057";    // first4loc


        //   20110413         周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
        // tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEEEEEEEE"; // first4loc 加地第一個周4
        // tmpShipPlan = "011012013014015016017018019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057058059060061062063";    // first4loc

        //                    周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
        tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEEEEEEEE"; // first4loc 加地第一個周4
        tmpShipPlan = "019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057058059060061062063064065066067068069070071";    // first4loc

        CurrShipPlanLine = Convert.ToInt32(locrefstr1);                            // 第幾個 Array

        if (CurrShipPlanLine == 31)
            CurrShipPlanLine = CurrShipPlanLine;

        if (arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 03].ToString() == "9904470")
            s1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 60].ToString();

        arrayShipPlan[CurrShipPlanLine, 00] = locrefstr1; // NO
        arrayShipPlan[CurrShipPlanLine, 01] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 04].ToString(); // Dpcument_ID Nokia
        arrayShipPlan[CurrShipPlanLine, 02] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom  
        arrayShipPlan[CurrShipPlanLine, 03] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 03].ToString(); // CustomerPN 
        arrayShipPlan[CurrShipPlanLine, 04] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 01].ToString(); // CustomerSite  
        arrayShipPlan[CurrShipPlanLine, 05] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 00].ToString(); // Project
        arrayShipPlan[CurrShipPlanLine, 06] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 29].ToString(); // Description  
        arrayShipPlan[CurrShipPlanLine, 07] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 60].ToString(); // Agreement
        arrayShipPlan[CurrShipPlanLine, 08] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 61].ToString(); // Item        
        arrayShipPlan[CurrShipPlanLine, 09] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 02].ToString(); // FoxconnSite  
        
        // arrayShipPlan[CurrShipPlanLine, 02] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 05].ToString(); // DocumentTime
        arrayShipPlan[CurrShipPlanLine, 10] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 08].ToString(); // HHPN 
        arrayShipPlan[CurrShipPlanLine, 76] = (Convert.ToInt32(locrefstr1) + 1001).ToString();                 // Seqno 
        arrayShipPlan[CurrShipPlanLine, 77] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 28].ToString(); // DocumentID ret 


        //   arrayShipPlan[CurrShipPlanLine, 10] = "N"; // CommonParts
        //   arrayShipPlan[CurrShipPlanLine, 11] = "Demand"; // QtyDataType
        arrayShipPlan[CurrShipPlanLine, 10] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 62].ToString(); // OnHand  
        arrayShipPlan[CurrShipPlanLine, 11] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 63].ToString(); // Consigned  
        arrayShipPlan[CurrShipPlanLine, 12] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 64].ToString(); // GIT  
        arrayShipPlan[CurrShipPlanLine, 13] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 69].ToString(); // Block   
        arrayShipPlan[CurrShipPlanLine, 14] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 70].ToString(); // Quality  
        arrayShipPlan[CurrShipPlanLine, 15] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 71].ToString(); // Min Day  
        arrayShipPlan[CurrShipPlanLine, 16] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 72].ToString(); // Max Day  
        arrayShipPlan[CurrShipPlanLine, 17] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 73].ToString(); // Min Inventory  
        arrayShipPlan[CurrShipPlanLine, 18] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 74].ToString(); // Max Inventory 

        arrayShipPlan[CurrShipPlanLine, 76] = (Convert.ToInt32(locrefstr1) + 1001).ToString();                 // Seqno 
        arrayShipPlan[CurrShipPlanLine, 77] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 28].ToString(); // DocumentID ret 
        arrayShipPlan[CurrShipPlanLine, 78] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 05].ToString(); // DocumentTime
        arrayShipPlan[CurrShipPlanLine, 79] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 08].ToString(); // HHPN 
        
        if (CurrShipPlanLine >= 420)
            locsql1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom


        // F00:    NO
        // F01:    datafrom Project
        // F02     DocumentTime:    CustomerSite
        // F03:    Document_ID FoxconnRegion           
        // F04:    Forecast_CustomerPN
        // F05:    CustomerSite
        // F06:    FoxconnSite
        // F07:    Agreement        // F07:    Max
        // F08:    Item
        // F09:    Project
        // F10:    HHPN
        // F11:    Reserved
        // F28:    Return DoucmentID
        // F29:    Desp
        // F56:    datafrom
        // F57:    DocumentID ( Private )
        // F60:    Agreement                                ( 130-10+4 )
        // F61:    Item                                     ( 130-10+5 )
        // F62:    Consigned         / Consigned Inventory  ( 130-10+1 )
        // F63:    GIT               / GIT                  ( 130-10+2 )
        // F64:    On-Hand Inventory / On-Hand Inventory    ( 130-10+3 )
        // F65:    GIT BJ                                   ( 130-10-7 )
        // F66:    GIT LF                                   ( 130-10-8 ) 
        // F67:    GIT LH                                   ( 130-10-9 )
        // F68:    GIT Chennai                              ( 130-10-10 )
        // F69:    Block    / Blocked Stock                   ( 130-10-1)
        // F70:    Quality  / Quality Stock                   ( 130-10-2)
        // F71:    Min Days / Minimum Days of Supply Target   ( 130-10-3)
        // F72:    Max Days / Maximum Days of Supply Target   ( 130-10-4) 
        // F73:    Min Inventory  / Minimum Inventory Target  ( 130-10-5)
        // F74:    Max Inventory  / Maximum Inventory Target  ( 130-10-6)
        // F75:    DocumentID ( Private )


        // arrayShipPlan[CurrShipPlanLine, 58] = arrayVar[arrayVarX + 1, CurrShipPlanLine].ToString(); // NetQty 20100704  
        arrayShipPlan[CurrShipPlanLine, 58] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arraytransSecondAvailLoc].ToString(); // NetQty 20100704  

        //
        locvar1 = Convert.ToInt32(arraytransDay[tmptodaylocation, 10].ToString()); // 星期幾 ? 找周4

        if (locvar1 == 0) first4loc = tmptodaylocation - 7 + 4;                    // 第一個周4位置
        else if (locvar1 >= 4) first4loc = tmptodaylocation - locvar1 + 4;       //
        else first4loc = tmptodaylocation - locvar1 + 4 - 7;   //

        prefirst4loc = first4loc - 1; // count from 0
        readheadloc = prefirst4loc;
        readtailloc = 3 * 0;  //  3 * 1;
        int firstW1 = prefirst4loc + 4 + 21;


        while (tmptransDay.Substring(readtailloc, 3) != "EEE")
        {

            s1 = tmptransDay.Substring(readtailloc, 3);    // 此為第一個周4, 所以須加  first4loc
            s2 = tmpShipPlan.Substring(readtailloc, 3);    // 此為 OutPut array 位置

            if (s1.Substring(0, 1).ToUpper() == "E")
                arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = tottot.ToString();
            else
                if (s1.Substring(0, 1).ToUpper() == "C")
                {
                    arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
                    subtot = 0;
                }
                else
                    if (s1.Substring(0, 1).ToUpper() == "W")
                    {
                        subtot = 0;
                        s3 = s1.Substring(1, 2);
                        locvar2 = firstW1 + Convert.ToInt32(s3) * 7 - 7;  // Week 起始日往後加 7 天
                        for (locvar1 = 0; locvar1 < 7; locvar1++)
                            subtot = subtot + Convert.ToInt32(arraytransDay[locvar2 + locvar1, 4]); // 加一周  Org 27 

                        // if (  subtot > 0 )
                        //       subtot =  subtot ;
                        arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
                        tottot = subtot + tottot;
                        subtot = 0;
                    }
                    else
                    {
                        tmpMPQ = Convert.ToInt32(arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString());
                        arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString();
                        subtot = tmpMPQ + subtot;
                        tottot = tmpMPQ + tottot;
                    }  // end if 
            readtailloc = readtailloc + 3;

        }  // end while    

    }  // end of GSCMDOrg4A7TransDayToShipPlan

    private void TmpOrgDV4A3QueryWriDBFShipPlanarraytransDay(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref DateTime firstThuDate, ref string tmpSPWeek, ref string tmpDocumentID)  //    
    {
        int lvar31 = 1, seqno = 1001;
        int localvar1 = 7;
        int localvar2 = 7;
        int localvar3 = 7;
        int localvar4 = 7, v1 = 0;
        string locsql1 = "";
        string locstr1 = "";
        string locstr2 = "";
        string tspace = "";
        string Messg = "";
       
        string[,] HeadarrayShipPlan = new string[4, arrayShipPlanYLong + 1];

        for (lvar31 = 0; lvar31 < 3 + 1; lvar31++) for (localvar1 = 0; localvar1 < arrayShipPlanYLong + 1; localvar1++) HeadarrayShipPlan[lvar31, localvar1] = "";

        // Set Up Print Head   // array7 開使始為星期四                               Thu       Fri      Sat       Sun        ToT
        // Project CustomerSite Description CustomerPN H.H P/N FoxconnSite Dom_Exp  20100415  20100416 20100417  20100418

        HeadarrayShipPlan[2, 00] = " NO";
        HeadarrayShipPlan[2, 01] = " DocumentID";
        HeadarrayShipPlan[2, 02] = " Datafrom";
        HeadarrayShipPlan[2, 03] = " CustomerPN";
        HeadarrayShipPlan[2, 04] = " ReceivingSite";
        HeadarrayShipPlan[2, 05] = " Project";
        HeadarrayShipPlan[2, 06] = " Description";
        HeadarrayShipPlan[2, 07] = " Agreement";
        HeadarrayShipPlan[2, 08] = " Item";
        HeadarrayShipPlan[2, 09] = " SupplyingSite";
        HeadarrayShipPlan[2, 10] = " CommonParts";
        HeadarrayShipPlan[2, 11] = " QtyDataType";
        HeadarrayShipPlan[2, 12] = " OnHands";
        HeadarrayShipPlan[2, 13] = " Consigned";
        HeadarrayShipPlan[2, 14] = " GITToT";
        HeadarrayShipPlan[2, 15] = " GIT BJ";
        HeadarrayShipPlan[2, 16] = " GIT LF";
        HeadarrayShipPlan[2, 17] = " GIT LH";
        HeadarrayShipPlan[2, 18] = " GIT Chennai";
        HeadarrayShipPlan[2, 19] = " Blocked Stock";
        HeadarrayShipPlan[2, 20] = " Quality Stock";
        HeadarrayShipPlan[2, 21] = " Min Days";
        HeadarrayShipPlan[2, 22] = " Max Days";
        HeadarrayShipPlan[2, 23] = " Min Inventory";
        HeadarrayShipPlan[2, 24] = " Max Inventory";
        //    (Convert.ToInt32(tmpMaxMin.DayOfWeek)).ToString();

        v1 = tmpSPWeek.Length;
        localvar4 = Convert.ToInt32(tmpSPWeek.Substring(7, v1 - 7));  // 第幾週
        if ((Convert.ToInt32(tmptoday.DayOfWeek) >= 1) && (Convert.ToInt32(tmptoday.DayOfWeek) <= 3))
            localvar4 = localvar4 - 1; // Pre Week

        HeadarrayShipPlan[0, 25] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 25 + 8] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 25 + 16] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 25 + 24] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();

        // HeadarrayShipPlan[2, 7] = (firstThuDate.AddDays(localvar1++)).ToString("yyyyMMdd");
        localvar3 = 0;
        localvar1 = 4;
        for (localvar2 = 25; localvar2 <= 52; localvar2++)
        {
            localvar1 = localvar1 % 7;

            switch (localvar1)                   //
            {                                    // 
                case 0: locstr1 = " Sun"; break; // 用 var1 變數當                        
                case 1: locstr1 = " Mon"; break; //  0 為目前                                                    
                case 2: locstr1 = " Tue"; break; //  1 為下周 + 離周一偏移為置
                case 3: locstr1 = " Wed"; break; //
                case 4: locstr1 = " Thu"; break; //   
                case 5: locstr1 = " Fri"; break; //   
                case 6: locstr1 = " Sat"; break; //   
                default: locstr1 = ""; break;     //
            }

            HeadarrayShipPlan[1, localvar2] = locstr1;
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3++)).ToString("yyyyMMdd");

            if (localvar1 == 0) HeadarrayShipPlan[2, ++localvar2] = "Total";  // Week total


            localvar1++;

        }

        for (localvar2 = 54; localvar2 <= 72; localvar2++)
        {
            HeadarrayShipPlan[1, localvar2] = "WK" + (localvar4).ToString();
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3)).ToString("yyyyMMdd");
            localvar4 = localvar4 + 1;
            localvar3 = localvar3 + 7;
        }

        HeadarrayShipPlan[2, localvar2] = "Total"; // The Last End

        if (WriDBFFlag == "Y")
        {
            lvar31 = 0;
            while ((lvar31 <= 2) && (PrintHeadSW == 0))  // write head 
            {
                locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F59, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74,F75,F76, F77 ) Values ( '" + HeadarrayShipPlan[lvar31, 0].ToString() + "', '" + HeadarrayShipPlan[lvar31, 1].ToString() + "', '" + HeadarrayShipPlan[lvar31, 2].ToString() + "', '" + HeadarrayShipPlan[lvar31, 3].ToString() + "', '" + HeadarrayShipPlan[lvar31, 4].ToString() + "', '" + HeadarrayShipPlan[lvar31, 5].ToString() + "', '" + HeadarrayShipPlan[lvar31, 6].ToString() + "', '" + HeadarrayShipPlan[lvar31, 7].ToString() + "', '" + HeadarrayShipPlan[lvar31, 8].ToString() + "', '" + HeadarrayShipPlan[lvar31, 9].ToString() + "', '" + HeadarrayShipPlan[lvar31, 10].ToString() + "', '" + HeadarrayShipPlan[lvar31, 11].ToString() + "', '" + HeadarrayShipPlan[lvar31, 12].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 13].ToString() + "', '" + HeadarrayShipPlan[lvar31, 14].ToString() + "', '" + HeadarrayShipPlan[lvar31, 15].ToString() + "', '" + HeadarrayShipPlan[lvar31, 16].ToString() + "', '" + HeadarrayShipPlan[lvar31, 17].ToString() + "', '" + HeadarrayShipPlan[lvar31, 18].ToString() + "', '" + HeadarrayShipPlan[lvar31, 19].ToString() + "', '" + HeadarrayShipPlan[lvar31, 20].ToString() + "', '" + HeadarrayShipPlan[lvar31, 21].ToString() + "', '" + HeadarrayShipPlan[lvar31, 22].ToString() + "', '" + HeadarrayShipPlan[lvar31, 23].ToString() + "', '" + HeadarrayShipPlan[lvar31, 24].ToString() + "', '" + HeadarrayShipPlan[lvar31, 25].ToString() + "', '" + HeadarrayShipPlan[lvar31, 26].ToString() + "', '" + HeadarrayShipPlan[lvar31, 27].ToString() + "', '" + HeadarrayShipPlan[lvar31, 28].ToString() + "', '" + HeadarrayShipPlan[lvar31, 29].ToString() + "', '" + HeadarrayShipPlan[lvar31, 30].ToString() + "', '" + HeadarrayShipPlan[lvar31, 31].ToString() + "', '" + HeadarrayShipPlan[lvar31, 32].ToString() + "', '" + HeadarrayShipPlan[lvar31, 33].ToString() + "', '" + HeadarrayShipPlan[lvar31, 34].ToString() + "', '" + HeadarrayShipPlan[lvar31, 35].ToString() + "', '" + HeadarrayShipPlan[lvar31, 36].ToString() + "', '" + HeadarrayShipPlan[lvar31, 37].ToString() + "', '" + HeadarrayShipPlan[lvar31, 38].ToString() + "', '" + HeadarrayShipPlan[lvar31, 39].ToString() + "', '" + HeadarrayShipPlan[lvar31, 40].ToString() + "', '" + HeadarrayShipPlan[lvar31, 41].ToString() + "', '" + HeadarrayShipPlan[lvar31, 42].ToString() + "', '" + HeadarrayShipPlan[lvar31, 43].ToString() + "', '" + HeadarrayShipPlan[lvar31, 44].ToString() + "', '" + HeadarrayShipPlan[lvar31, 45].ToString() + "', '" + HeadarrayShipPlan[lvar31, 46].ToString() + "', '" + HeadarrayShipPlan[lvar31, 47].ToString() + "', '" + HeadarrayShipPlan[lvar31, 48].ToString() + "', '" + HeadarrayShipPlan[lvar31, 49].ToString() + "', '" + HeadarrayShipPlan[lvar31, 50].ToString() + "', '" + HeadarrayShipPlan[lvar31, 51].ToString() + "', '" + HeadarrayShipPlan[lvar31, 52].ToString() + "', '" + HeadarrayShipPlan[lvar31, 53].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 54].ToString() + "', '" + HeadarrayShipPlan[lvar31, 55].ToString() + "', '" + HeadarrayShipPlan[lvar31, 56].ToString() + "' , '" + HeadarrayShipPlan[lvar31, 57].ToString() + "', '" + HeadarrayShipPlan[lvar31, 58].ToString() + "', '" + HeadarrayShipPlan[lvar31, 59].ToString() + "', '" + HeadarrayShipPlan[lvar31, 60].ToString() + "', '" + HeadarrayShipPlan[lvar31, 61].ToString() + "', '" + HeadarrayShipPlan[lvar31, 62].ToString() + "', '" + HeadarrayShipPlan[lvar31, 63].ToString() + "', '" + HeadarrayShipPlan[lvar31, 64].ToString() + "', '" + HeadarrayShipPlan[lvar31, 65].ToString() + "', '" + HeadarrayShipPlan[lvar31, 66].ToString() + "', '" + HeadarrayShipPlan[lvar31, 67].ToString() + "', '" + HeadarrayShipPlan[lvar31, 68].ToString() + "', '" + HeadarrayShipPlan[lvar31, 69].ToString() + "', '" + HeadarrayShipPlan[lvar31, 70].ToString() + "', '" + HeadarrayShipPlan[lvar31, 71].ToString() + "', '" + HeadarrayShipPlan[lvar31, 72].ToString() + "', '" + HeadarrayShipPlan[lvar31, 73].ToString() + "', '" + HeadarrayShipPlan[lvar31, 74].ToString() + "', '', '" + (seqno++).ToString() + "', '" + tmpDocumentID + "'   ) ";
                if (!DataConnlib.Excute(locsql1))
                    Messg = "Failss";

                lvar31++;
            }


            lvar31 = 1;
            while (arrayShipPlan[lvar31, 1] != "")   // write data
            {

                locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F59, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74, F75, F76, F77, F78, F79 ) Values ( '" + arrayShipPlan[lvar31, 0].ToString() + "', '" + arrayShipPlan[lvar31, 1].ToString() + "', '" + arrayShipPlan[lvar31, 2].ToString() + "', '" + arrayShipPlan[lvar31, 3].ToString() + "', '" + arrayShipPlan[lvar31, 4].ToString() + "', '" + arrayShipPlan[lvar31, 5].ToString() + "', '" + arrayShipPlan[lvar31, 6].ToString() + "', '" + arrayShipPlan[lvar31, 7].ToString() + "', '" + arrayShipPlan[lvar31, 8].ToString() + "', '" + arrayShipPlan[lvar31, 9].ToString() + "', '" + arrayShipPlan[lvar31, 10].ToString() + "', '" + arrayShipPlan[lvar31, 11].ToString() + "', '" + arrayShipPlan[lvar31, 12].ToString() + "',  '" + arrayShipPlan[lvar31, 13].ToString() + "', '" + arrayShipPlan[lvar31, 14].ToString() + "', '" + arrayShipPlan[lvar31, 15].ToString() + "', '" + arrayShipPlan[lvar31, 16].ToString() + "', '" + arrayShipPlan[lvar31, 17].ToString() + "', '" + arrayShipPlan[lvar31, 18].ToString() + "', '" + arrayShipPlan[lvar31, 19].ToString() + "', '" + arrayShipPlan[lvar31, 20].ToString() + "', '" + arrayShipPlan[lvar31, 21].ToString() + "', '" + arrayShipPlan[lvar31, 22].ToString() + "', '" + arrayShipPlan[lvar31, 23].ToString() + "', '" + arrayShipPlan[lvar31, 24].ToString() + "', '" + arrayShipPlan[lvar31, 25].ToString() + "', '" + arrayShipPlan[lvar31, 26].ToString() + "', '" + arrayShipPlan[lvar31, 27].ToString() + "', '" + arrayShipPlan[lvar31, 28].ToString() + "', '" + arrayShipPlan[lvar31, 29].ToString() + "', '" + arrayShipPlan[lvar31, 30].ToString() + "', '" + arrayShipPlan[lvar31, 31].ToString() + "', '" + arrayShipPlan[lvar31, 32].ToString() + "', '" + arrayShipPlan[lvar31, 33].ToString() + "', '" + arrayShipPlan[lvar31, 34].ToString() + "', '" + arrayShipPlan[lvar31, 35].ToString() + "', '" + arrayShipPlan[lvar31, 36].ToString() + "', '" + arrayShipPlan[lvar31, 37].ToString() + "', '" + arrayShipPlan[lvar31, 38].ToString() + "', '" + arrayShipPlan[lvar31, 39].ToString() + "', '" + arrayShipPlan[lvar31, 40].ToString() + "', '" + arrayShipPlan[lvar31, 41].ToString() + "', '" + arrayShipPlan[lvar31, 42].ToString() + "', '" + arrayShipPlan[lvar31, 43].ToString() + "', '" + arrayShipPlan[lvar31, 44].ToString() + "', '" + arrayShipPlan[lvar31, 45].ToString() + "', '" + arrayShipPlan[lvar31, 46].ToString() + "', '" + arrayShipPlan[lvar31, 47].ToString() + "', '" + arrayShipPlan[lvar31, 48].ToString() + "', '" + arrayShipPlan[lvar31, 49].ToString() + "', '" + arrayShipPlan[lvar31, 50].ToString() + "', '" + arrayShipPlan[lvar31, 51].ToString() + "', '" + arrayShipPlan[lvar31, 52].ToString() + "', '" + arrayShipPlan[lvar31, 53].ToString() + "', '" + arrayShipPlan[lvar31, 54].ToString() + "', '" + arrayShipPlan[lvar31, 55].ToString() + "', '" + arrayShipPlan[lvar31, 56].ToString() + "', '" + arrayShipPlan[lvar31, 57].ToString() + "', '" + arrayShipPlan[lvar31, 58].ToString() + "', '" + arrayShipPlan[lvar31, 59].ToString() + "', '" + arrayShipPlan[lvar31, 60].ToString() + "' , '" + arrayShipPlan[lvar31, 61].ToString() + "', '" + arrayShipPlan[lvar31, 62].ToString() + "', '" + arrayShipPlan[lvar31, 63].ToString() + "', '" + arrayShipPlan[lvar31, 64].ToString() + "', '" + arrayShipPlan[lvar31, 65].ToString() + "', '" + arrayShipPlan[lvar31, 66].ToString() + "', '" + arrayShipPlan[lvar31, 67].ToString() + "', '" + arrayShipPlan[lvar31, 68].ToString() + "', '" + arrayShipPlan[lvar31, 69].ToString() + "', '" + arrayShipPlan[lvar31, 70].ToString() + "', '" + arrayShipPlan[lvar31, 71].ToString() + "', '" + arrayShipPlan[lvar31, 72].ToString() + "', '" + arrayShipPlan[lvar31, 73].ToString() + "', '" + arrayShipPlan[lvar31, 74].ToString() + "', '" + arrayShipPlan[lvar31, 75].ToString() + "', '" + (seqno++).ToString() + "'  , '" + tmpDocumentID + "', '" + arrayShipPlan[lvar31, 78].ToString() + "', '" + arrayShipPlan[lvar31, 79].ToString() + "' ) ";
                if (!DataConnlib.Excute(locsql1))
                    Messg = "Failss";

                lvar31++;

            }  // End Witch SW2 

        }  // end  if (WriDBFFlag == "Y")

        int i1 = 0, i2 = 0, i3 = Convert.ToInt32(locrefstr1);
       
        for (i1 = 0; i1 < 2+1; i1++)
            for (i2 = 0; i2 < arrayShipPlanYLong; i2++)
                ResData[i1, i2] = HeadarrayShipPlan[i1, i2]; 
       
        for (i1 = 1; i1 < i3 + 1; i1++)
            for (i2 = 0; i2 < arrayShipPlanYLong; i2++)
                ResData[i1+2, i2] = arrayShipPlan[i1, i2]; 


    }  // end of TmpDV4A3OrgQueryWriDBFShipPlanarraytransDay

    private void TmpOrgDV4A5QueryWriDBFShipPlanarraytransDay(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref DateTime firstThuDate, ref string tmpSPWeek, ref string tmpDocumentID)  //    
    {
        int lvar31 = 1, seqno = 1001;
        int localvar1 = 7;
        int localvar2 = 7;
        int localvar3 = 7;
        int localvar4 = 7, v1 = 0;
        string locsql1 = "";
        string locstr1 = "";
        string locstr2 = "";
        string tspace = "";
        string Messg = "";

        string[,] HeadarrayShipPlan = new string[4, arrayShipPlanYLong + 1];

        for (lvar31 = 0; lvar31 < 3 + 1; lvar31++) for (localvar1 = 0; localvar1 < arrayShipPlanYLong + 1; localvar1++) HeadarrayShipPlan[lvar31, localvar1] = "";

        // Set Up Print Head   // array7 開使始為星期四                               Thu       Fri      Sat       Sun        ToT
        // Project CustomerSite Description CustomerPN H.H P/N FoxconnSite Dom_Exp  20100415  20100416 20100417  20100418

        HeadarrayShipPlan[2, 00] = " NO";
        HeadarrayShipPlan[2, 01] = " Datafrom";
        HeadarrayShipPlan[2, 02] = " DocumentTime";
        HeadarrayShipPlan[2, 03] = " DocumentID";
        HeadarrayShipPlan[2, 04] = " CustomerPN";
        HeadarrayShipPlan[2, 05] = " ReceivingSite";
        HeadarrayShipPlan[2, 06] = " SupplyingSite";
        HeadarrayShipPlan[2, 07] = " SA";
        HeadarrayShipPlan[2, 08] = " Item";
        HeadarrayShipPlan[2, 09] = " Project";
        HeadarrayShipPlan[2, 10] = " FoxconnPartno";
        
        //    (Convert.ToInt32(tmpMaxMin.DayOfWeek)).ToString();

        v1 = tmpSPWeek.Length;
        localvar4 = Convert.ToInt32(tmpSPWeek.Substring(7, v1 - 7));  // 第幾週
        if ((Convert.ToInt32(tmptoday.DayOfWeek) >= 1) && (Convert.ToInt32(tmptoday.DayOfWeek) <= 3))
            localvar4 = localvar4 - 1; // Pre Week

        HeadarrayShipPlan[0, 11] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 11 + 8] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 11 + 16] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 11 + 24] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();

        // HeadarrayShipPlan[2, 7] = (firstThuDate.AddDays(localvar1++)).ToString("yyyyMMdd");
        localvar3 = 0;
        localvar1 = 4;
        for (localvar2 = 11; localvar2 <= 39; localvar2++)
        {
            localvar1 = localvar1 % 7;

            switch (localvar1)                   //
            {                                    // 
                case 0: locstr1 = " Sun"; break; // 用 var1 變數當                        
                case 1: locstr1 = " Mon"; break; //  0 為目前                                                    
                case 2: locstr1 = " Tue"; break; //  1 為下周 + 離周一偏移為置
                case 3: locstr1 = " Wed"; break; //
                case 4: locstr1 = " Thu"; break; //   
                case 5: locstr1 = " Fri"; break; //   
                case 6: locstr1 = " Sat"; break; //   
                default: locstr1 = ""; break;     //
            }

            HeadarrayShipPlan[1, localvar2] = locstr1;
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3++)).ToString("yyyyMMdd");

            if (localvar1 == 0) HeadarrayShipPlan[2, ++localvar2] = "Total";  // Week total


            localvar1++;

        }

        for (localvar2 = 40; localvar2 <= 58; localvar2++)
        {
            HeadarrayShipPlan[1, localvar2] = "WK" + (localvar4).ToString();
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3)).ToString("yyyyMMdd");
            localvar4 = localvar4 + 1;
            localvar3 = localvar3 + 7;
        }

        HeadarrayShipPlan[2, localvar2] = "Total"; // The Last End

        if (WriDBFFlag == "Y")
        {
            lvar31 = 0;
            while ((lvar31 <= 2) && (PrintHeadSW == 0))  // write head 
            {
                locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F59, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74,F75,F76, F77 ) Values ( '" + HeadarrayShipPlan[lvar31, 0].ToString() + "', '" + HeadarrayShipPlan[lvar31, 1].ToString() + "', '" + HeadarrayShipPlan[lvar31, 2].ToString() + "', '" + HeadarrayShipPlan[lvar31, 3].ToString() + "', '" + HeadarrayShipPlan[lvar31, 4].ToString() + "', '" + HeadarrayShipPlan[lvar31, 5].ToString() + "', '" + HeadarrayShipPlan[lvar31, 6].ToString() + "', '" + HeadarrayShipPlan[lvar31, 7].ToString() + "', '" + HeadarrayShipPlan[lvar31, 8].ToString() + "', '" + HeadarrayShipPlan[lvar31, 9].ToString() + "', '" + HeadarrayShipPlan[lvar31, 10].ToString() + "', '" + HeadarrayShipPlan[lvar31, 11].ToString() + "', '" + HeadarrayShipPlan[lvar31, 12].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 13].ToString() + "', '" + HeadarrayShipPlan[lvar31, 14].ToString() + "', '" + HeadarrayShipPlan[lvar31, 15].ToString() + "', '" + HeadarrayShipPlan[lvar31, 16].ToString() + "', '" + HeadarrayShipPlan[lvar31, 17].ToString() + "', '" + HeadarrayShipPlan[lvar31, 18].ToString() + "', '" + HeadarrayShipPlan[lvar31, 19].ToString() + "', '" + HeadarrayShipPlan[lvar31, 20].ToString() + "', '" + HeadarrayShipPlan[lvar31, 21].ToString() + "', '" + HeadarrayShipPlan[lvar31, 22].ToString() + "', '" + HeadarrayShipPlan[lvar31, 23].ToString() + "', '" + HeadarrayShipPlan[lvar31, 24].ToString() + "', '" + HeadarrayShipPlan[lvar31, 25].ToString() + "', '" + HeadarrayShipPlan[lvar31, 26].ToString() + "', '" + HeadarrayShipPlan[lvar31, 27].ToString() + "', '" + HeadarrayShipPlan[lvar31, 28].ToString() + "', '" + HeadarrayShipPlan[lvar31, 29].ToString() + "', '" + HeadarrayShipPlan[lvar31, 30].ToString() + "', '" + HeadarrayShipPlan[lvar31, 31].ToString() + "', '" + HeadarrayShipPlan[lvar31, 32].ToString() + "', '" + HeadarrayShipPlan[lvar31, 33].ToString() + "', '" + HeadarrayShipPlan[lvar31, 34].ToString() + "', '" + HeadarrayShipPlan[lvar31, 35].ToString() + "', '" + HeadarrayShipPlan[lvar31, 36].ToString() + "', '" + HeadarrayShipPlan[lvar31, 37].ToString() + "', '" + HeadarrayShipPlan[lvar31, 38].ToString() + "', '" + HeadarrayShipPlan[lvar31, 39].ToString() + "', '" + HeadarrayShipPlan[lvar31, 40].ToString() + "', '" + HeadarrayShipPlan[lvar31, 41].ToString() + "', '" + HeadarrayShipPlan[lvar31, 42].ToString() + "', '" + HeadarrayShipPlan[lvar31, 43].ToString() + "', '" + HeadarrayShipPlan[lvar31, 44].ToString() + "', '" + HeadarrayShipPlan[lvar31, 45].ToString() + "', '" + HeadarrayShipPlan[lvar31, 46].ToString() + "', '" + HeadarrayShipPlan[lvar31, 47].ToString() + "', '" + HeadarrayShipPlan[lvar31, 48].ToString() + "', '" + HeadarrayShipPlan[lvar31, 49].ToString() + "', '" + HeadarrayShipPlan[lvar31, 50].ToString() + "', '" + HeadarrayShipPlan[lvar31, 51].ToString() + "', '" + HeadarrayShipPlan[lvar31, 52].ToString() + "', '" + HeadarrayShipPlan[lvar31, 53].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 54].ToString() + "', '" + HeadarrayShipPlan[lvar31, 55].ToString() + "', '" + HeadarrayShipPlan[lvar31, 56].ToString() + "' , '" + HeadarrayShipPlan[lvar31, 57].ToString() + "', '" + HeadarrayShipPlan[lvar31, 58].ToString() + "', '" + HeadarrayShipPlan[lvar31, 59].ToString() + "', '" + HeadarrayShipPlan[lvar31, 60].ToString() + "', '" + HeadarrayShipPlan[lvar31, 61].ToString() + "', '" + HeadarrayShipPlan[lvar31, 62].ToString() + "', '" + HeadarrayShipPlan[lvar31, 63].ToString() + "', '" + HeadarrayShipPlan[lvar31, 64].ToString() + "', '" + HeadarrayShipPlan[lvar31, 65].ToString() + "', '" + HeadarrayShipPlan[lvar31, 66].ToString() + "', '" + HeadarrayShipPlan[lvar31, 67].ToString() + "', '" + HeadarrayShipPlan[lvar31, 68].ToString() + "', '" + HeadarrayShipPlan[lvar31, 69].ToString() + "', '" + HeadarrayShipPlan[lvar31, 70].ToString() + "', '" + HeadarrayShipPlan[lvar31, 71].ToString() + "', '" + HeadarrayShipPlan[lvar31, 72].ToString() + "', '" + HeadarrayShipPlan[lvar31, 73].ToString() + "', '" + HeadarrayShipPlan[lvar31, 74].ToString() + "', '', '" + (seqno++).ToString() + "', '" + tmpDocumentID + "'   ) ";
                if (!DataConnlib.Excute(locsql1))
                    Messg = "Failss";

                lvar31++;
            }


            lvar31 = 1;
            while (arrayShipPlan[lvar31, 1] != "")   // write data
            {

                locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F59, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74, F75, F76, F77, F78, F79 ) Values ( '" + arrayShipPlan[lvar31, 0].ToString() + "', '" + arrayShipPlan[lvar31, 1].ToString() + "', '" + arrayShipPlan[lvar31, 2].ToString() + "', '" + arrayShipPlan[lvar31, 3].ToString() + "', '" + arrayShipPlan[lvar31, 4].ToString() + "', '" + arrayShipPlan[lvar31, 5].ToString() + "', '" + arrayShipPlan[lvar31, 6].ToString() + "', '" + arrayShipPlan[lvar31, 7].ToString() + "', '" + arrayShipPlan[lvar31, 8].ToString() + "', '" + arrayShipPlan[lvar31, 9].ToString() + "', '" + arrayShipPlan[lvar31, 10].ToString() + "', '" + arrayShipPlan[lvar31, 11].ToString() + "', '" + arrayShipPlan[lvar31, 12].ToString() + "',  '" + arrayShipPlan[lvar31, 13].ToString() + "', '" + arrayShipPlan[lvar31, 14].ToString() + "', '" + arrayShipPlan[lvar31, 15].ToString() + "', '" + arrayShipPlan[lvar31, 16].ToString() + "', '" + arrayShipPlan[lvar31, 17].ToString() + "', '" + arrayShipPlan[lvar31, 18].ToString() + "', '" + arrayShipPlan[lvar31, 19].ToString() + "', '" + arrayShipPlan[lvar31, 20].ToString() + "', '" + arrayShipPlan[lvar31, 21].ToString() + "', '" + arrayShipPlan[lvar31, 22].ToString() + "', '" + arrayShipPlan[lvar31, 23].ToString() + "', '" + arrayShipPlan[lvar31, 24].ToString() + "', '" + arrayShipPlan[lvar31, 25].ToString() + "', '" + arrayShipPlan[lvar31, 26].ToString() + "', '" + arrayShipPlan[lvar31, 27].ToString() + "', '" + arrayShipPlan[lvar31, 28].ToString() + "', '" + arrayShipPlan[lvar31, 29].ToString() + "', '" + arrayShipPlan[lvar31, 30].ToString() + "', '" + arrayShipPlan[lvar31, 31].ToString() + "', '" + arrayShipPlan[lvar31, 32].ToString() + "', '" + arrayShipPlan[lvar31, 33].ToString() + "', '" + arrayShipPlan[lvar31, 34].ToString() + "', '" + arrayShipPlan[lvar31, 35].ToString() + "', '" + arrayShipPlan[lvar31, 36].ToString() + "', '" + arrayShipPlan[lvar31, 37].ToString() + "', '" + arrayShipPlan[lvar31, 38].ToString() + "', '" + arrayShipPlan[lvar31, 39].ToString() + "', '" + arrayShipPlan[lvar31, 40].ToString() + "', '" + arrayShipPlan[lvar31, 41].ToString() + "', '" + arrayShipPlan[lvar31, 42].ToString() + "', '" + arrayShipPlan[lvar31, 43].ToString() + "', '" + arrayShipPlan[lvar31, 44].ToString() + "', '" + arrayShipPlan[lvar31, 45].ToString() + "', '" + arrayShipPlan[lvar31, 46].ToString() + "', '" + arrayShipPlan[lvar31, 47].ToString() + "', '" + arrayShipPlan[lvar31, 48].ToString() + "', '" + arrayShipPlan[lvar31, 49].ToString() + "', '" + arrayShipPlan[lvar31, 50].ToString() + "', '" + arrayShipPlan[lvar31, 51].ToString() + "', '" + arrayShipPlan[lvar31, 52].ToString() + "', '" + arrayShipPlan[lvar31, 53].ToString() + "', '" + arrayShipPlan[lvar31, 54].ToString() + "', '" + arrayShipPlan[lvar31, 55].ToString() + "', '" + arrayShipPlan[lvar31, 56].ToString() + "', '" + arrayShipPlan[lvar31, 57].ToString() + "', '" + arrayShipPlan[lvar31, 58].ToString() + "', '" + arrayShipPlan[lvar31, 59].ToString() + "', '" + arrayShipPlan[lvar31, 60].ToString() + "' , '" + arrayShipPlan[lvar31, 61].ToString() + "', '" + arrayShipPlan[lvar31, 62].ToString() + "', '" + arrayShipPlan[lvar31, 63].ToString() + "', '" + arrayShipPlan[lvar31, 64].ToString() + "', '" + arrayShipPlan[lvar31, 65].ToString() + "', '" + arrayShipPlan[lvar31, 66].ToString() + "', '" + arrayShipPlan[lvar31, 67].ToString() + "', '" + arrayShipPlan[lvar31, 68].ToString() + "', '" + arrayShipPlan[lvar31, 69].ToString() + "', '" + arrayShipPlan[lvar31, 70].ToString() + "', '" + arrayShipPlan[lvar31, 71].ToString() + "', '" + arrayShipPlan[lvar31, 72].ToString() + "', '" + arrayShipPlan[lvar31, 73].ToString() + "', '" + arrayShipPlan[lvar31, 74].ToString() + "', '" + arrayShipPlan[lvar31, 75].ToString() + "', '" + (seqno++).ToString() + "'  , '" + tmpDocumentID + "', '" + arrayShipPlan[lvar31, 78].ToString() + "', '" + arrayShipPlan[lvar31, 79].ToString() + "' ) ";
                if (!DataConnlib.Excute(locsql1))
                    Messg = "Failss";

                lvar31++;

            }  // End Witch SW2 

        }  // end  if (WriDBFFlag == "Y")

        int i1 = 0, i2 = 0, i3 = Convert.ToInt32(locrefstr1);

        for (i1 = 0; i1 < 2 + 1; i1++)
            for (i2 = 0; i2 < arrayShipPlanYLong; i2++)
                ResData[i1, i2] = HeadarrayShipPlan[i1, i2];

        for (i1 = 1; i1 < i3 + 1; i1++)
            for (i2 = 0; i2 < arrayShipPlanYLong; i2++)
                ResData[i1 + 2, i2] = arrayShipPlan[i1, i2];


    }  // end of TmpDV4A5OrgQueryWriDBFShipPlanarraytransDay

    private void TmpOrgDV4A7QueryWriDBFShipPlanarraytransDay(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref DateTime firstThuDate, ref string tmpSPWeek, ref string tmpDocumentID, int DSLoc)  //    
    {
        int lvar31 = 1, seqno = 1001;
        int localvar1 = 7;
        int localvar2 = 7;
        int localvar3 = 7;
        int localvar4 = 7, v1 = 0;
        string locsql1 = "";
        string locstr1 = "";
        string locstr2 = "";
        string tspace = "";
        string Messg = "";

        if (DSLoc == 0) DSLoc = 19;  // 0,19, 19+29=48, 48+18=66

        string[,] HeadarrayShipPlan = new string[4, arrayShipPlanYLong + 1];

        for (lvar31 = 0; lvar31 < 3 + 1; lvar31++) for (localvar1 = 0; localvar1 < arrayShipPlanYLong + 1; localvar1++) HeadarrayShipPlan[lvar31, localvar1] = "";

        // Set Up Print Head   // array7 開使始為星期四                               Thu       Fri      Sat       Sun        ToT
        // Project CustomerSite Description CustomerPN H.H P/N FoxconnSite Dom_Exp  20100415  20100416 20100417  20100418

        HeadarrayShipPlan[2, 00] = " NO";
        HeadarrayShipPlan[2, 01] = " DocumentID";
        HeadarrayShipPlan[2, 02] = " Datafrom";
        HeadarrayShipPlan[2, 03] = " CustomerPN";
        HeadarrayShipPlan[2, 04] = " ReceivingSite";
        HeadarrayShipPlan[2, 05] = " Project";
        HeadarrayShipPlan[2, 06] = " Description";
        HeadarrayShipPlan[2, 07] = " Agreement";
        HeadarrayShipPlan[2, 08] = " Item";
        HeadarrayShipPlan[2, 09] = " SupplyingSite";
        HeadarrayShipPlan[2, 10] = " OnHand";
        HeadarrayShipPlan[2, 11] = " Consigned";
        HeadarrayShipPlan[2, 12] = " GIT";
        HeadarrayShipPlan[2, 13] = " Block";
        HeadarrayShipPlan[2, 14] = " Quality";
        HeadarrayShipPlan[2, 15] = " Min_Days";
        HeadarrayShipPlan[2, 16] = " Max_Days";
        HeadarrayShipPlan[2, 17] = " Min Inventory";
        HeadarrayShipPlan[2, 18] = " Max Inventory";
               

        //    (Convert.ToInt32(tmpMaxMin.DayOfWeek)).ToString();

        v1 = tmpSPWeek.Length;
        localvar4 = Convert.ToInt32(tmpSPWeek.Substring(7, v1 - 7));  // 第幾週
        if ((Convert.ToInt32(tmptoday.DayOfWeek) >= 1) && (Convert.ToInt32(tmptoday.DayOfWeek) <= 3))
            localvar4 = localvar4 - 1; // Pre Week

        HeadarrayShipPlan[0, DSLoc]      = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();  // HeadarrayShipPlan[0, 11] 
        HeadarrayShipPlan[0, DSLoc + 08] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, DSLoc + 16] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, DSLoc + 24] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();

        // HeadarrayShipPlan[2, 7] = (firstThuDate.AddDays(localvar1++)).ToString("yyyyMMdd");
        localvar3 = 0;
        localvar1 = 4;
        // for (localvar2 = 11; localvar2 <= 39; localvar2++)
        for (localvar2 = DSLoc; localvar2 <= 48; localvar2++)
        {
            localvar1 = localvar1 % 7;

            switch (localvar1)                   //
            {                                    // 
                case 0: locstr1 = " Sun"; break; // 用 var1 變數當                        
                case 1: locstr1 = " Mon"; break; //  0 為目前                                                    
                case 2: locstr1 = " Tue"; break; //  1 為下周 + 離周一偏移為置
                case 3: locstr1 = " Wed"; break; //
                case 4: locstr1 = " Thu"; break; //   
                case 5: locstr1 = " Fri"; break; //   
                case 6: locstr1 = " Sat"; break; //   
                default: locstr1 = ""; break;     //
            }

            HeadarrayShipPlan[1, localvar2] = locstr1;
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3++)).ToString("yyyyMMdd");

            if (localvar1 == 0) HeadarrayShipPlan[2, ++localvar2] = "Total";  // Week total


            localvar1++;

        }

        for (localvar2 = 48; localvar2 <= 66; localvar2++)
        {
            HeadarrayShipPlan[1, localvar2] = "WK" + (localvar4).ToString();
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3)).ToString("yyyyMMdd");
            localvar4 = localvar4 + 1;
            localvar3 = localvar3 + 7;
        }

        HeadarrayShipPlan[2, localvar2] = "Total"; // The Last End

        if (WriDBFFlag == "Y")
        {
            lvar31 = 0;
            while ((lvar31 <= 2) && (PrintHeadSW == 0))  // write head 
            {
                locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F59, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74,F75,F76, F77 ) Values ( '" + HeadarrayShipPlan[lvar31, 0].ToString() + "', '" + HeadarrayShipPlan[lvar31, 1].ToString() + "', '" + HeadarrayShipPlan[lvar31, 2].ToString() + "', '" + HeadarrayShipPlan[lvar31, 3].ToString() + "', '" + HeadarrayShipPlan[lvar31, 4].ToString() + "', '" + HeadarrayShipPlan[lvar31, 5].ToString() + "', '" + HeadarrayShipPlan[lvar31, 6].ToString() + "', '" + HeadarrayShipPlan[lvar31, 7].ToString() + "', '" + HeadarrayShipPlan[lvar31, 8].ToString() + "', '" + HeadarrayShipPlan[lvar31, 9].ToString() + "', '" + HeadarrayShipPlan[lvar31, 10].ToString() + "', '" + HeadarrayShipPlan[lvar31, 11].ToString() + "', '" + HeadarrayShipPlan[lvar31, 12].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 13].ToString() + "', '" + HeadarrayShipPlan[lvar31, 14].ToString() + "', '" + HeadarrayShipPlan[lvar31, 15].ToString() + "', '" + HeadarrayShipPlan[lvar31, 16].ToString() + "', '" + HeadarrayShipPlan[lvar31, 17].ToString() + "', '" + HeadarrayShipPlan[lvar31, 18].ToString() + "', '" + HeadarrayShipPlan[lvar31, 19].ToString() + "', '" + HeadarrayShipPlan[lvar31, 20].ToString() + "', '" + HeadarrayShipPlan[lvar31, 21].ToString() + "', '" + HeadarrayShipPlan[lvar31, 22].ToString() + "', '" + HeadarrayShipPlan[lvar31, 23].ToString() + "', '" + HeadarrayShipPlan[lvar31, 24].ToString() + "', '" + HeadarrayShipPlan[lvar31, 25].ToString() + "', '" + HeadarrayShipPlan[lvar31, 26].ToString() + "', '" + HeadarrayShipPlan[lvar31, 27].ToString() + "', '" + HeadarrayShipPlan[lvar31, 28].ToString() + "', '" + HeadarrayShipPlan[lvar31, 29].ToString() + "', '" + HeadarrayShipPlan[lvar31, 30].ToString() + "', '" + HeadarrayShipPlan[lvar31, 31].ToString() + "', '" + HeadarrayShipPlan[lvar31, 32].ToString() + "', '" + HeadarrayShipPlan[lvar31, 33].ToString() + "', '" + HeadarrayShipPlan[lvar31, 34].ToString() + "', '" + HeadarrayShipPlan[lvar31, 35].ToString() + "', '" + HeadarrayShipPlan[lvar31, 36].ToString() + "', '" + HeadarrayShipPlan[lvar31, 37].ToString() + "', '" + HeadarrayShipPlan[lvar31, 38].ToString() + "', '" + HeadarrayShipPlan[lvar31, 39].ToString() + "', '" + HeadarrayShipPlan[lvar31, 40].ToString() + "', '" + HeadarrayShipPlan[lvar31, 41].ToString() + "', '" + HeadarrayShipPlan[lvar31, 42].ToString() + "', '" + HeadarrayShipPlan[lvar31, 43].ToString() + "', '" + HeadarrayShipPlan[lvar31, 44].ToString() + "', '" + HeadarrayShipPlan[lvar31, 45].ToString() + "', '" + HeadarrayShipPlan[lvar31, 46].ToString() + "', '" + HeadarrayShipPlan[lvar31, 47].ToString() + "', '" + HeadarrayShipPlan[lvar31, 48].ToString() + "', '" + HeadarrayShipPlan[lvar31, 49].ToString() + "', '" + HeadarrayShipPlan[lvar31, 50].ToString() + "', '" + HeadarrayShipPlan[lvar31, 51].ToString() + "', '" + HeadarrayShipPlan[lvar31, 52].ToString() + "', '" + HeadarrayShipPlan[lvar31, 53].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 54].ToString() + "', '" + HeadarrayShipPlan[lvar31, 55].ToString() + "', '" + HeadarrayShipPlan[lvar31, 56].ToString() + "' , '" + HeadarrayShipPlan[lvar31, 57].ToString() + "', '" + HeadarrayShipPlan[lvar31, 58].ToString() + "', '" + HeadarrayShipPlan[lvar31, 59].ToString() + "', '" + HeadarrayShipPlan[lvar31, 60].ToString() + "', '" + HeadarrayShipPlan[lvar31, 61].ToString() + "', '" + HeadarrayShipPlan[lvar31, 62].ToString() + "', '" + HeadarrayShipPlan[lvar31, 63].ToString() + "', '" + HeadarrayShipPlan[lvar31, 64].ToString() + "', '" + HeadarrayShipPlan[lvar31, 65].ToString() + "', '" + HeadarrayShipPlan[lvar31, 66].ToString() + "', '" + HeadarrayShipPlan[lvar31, 67].ToString() + "', '" + HeadarrayShipPlan[lvar31, 68].ToString() + "', '" + HeadarrayShipPlan[lvar31, 69].ToString() + "', '" + HeadarrayShipPlan[lvar31, 70].ToString() + "', '" + HeadarrayShipPlan[lvar31, 71].ToString() + "', '" + HeadarrayShipPlan[lvar31, 72].ToString() + "', '" + HeadarrayShipPlan[lvar31, 73].ToString() + "', '" + HeadarrayShipPlan[lvar31, 74].ToString() + "', '', '" + (seqno++).ToString() + "', '" + tmpDocumentID + "'   ) ";
                if (!DataConnlib.Excute(locsql1))
                    Messg = "Failss";

                lvar31++;
            }


            lvar31 = 1;
            while (arrayShipPlan[lvar31, 1] != "")   // write data
            {

                locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F59, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74, F75, F76, F77, F78, F79 ) Values ( '" + arrayShipPlan[lvar31, 0].ToString() + "', '" + arrayShipPlan[lvar31, 1].ToString() + "', '" + arrayShipPlan[lvar31, 2].ToString() + "', '" + arrayShipPlan[lvar31, 3].ToString() + "', '" + arrayShipPlan[lvar31, 4].ToString() + "', '" + arrayShipPlan[lvar31, 5].ToString() + "', '" + arrayShipPlan[lvar31, 6].ToString() + "', '" + arrayShipPlan[lvar31, 7].ToString() + "', '" + arrayShipPlan[lvar31, 8].ToString() + "', '" + arrayShipPlan[lvar31, 9].ToString() + "', '" + arrayShipPlan[lvar31, 10].ToString() + "', '" + arrayShipPlan[lvar31, 11].ToString() + "', '" + arrayShipPlan[lvar31, 12].ToString() + "',  '" + arrayShipPlan[lvar31, 13].ToString() + "', '" + arrayShipPlan[lvar31, 14].ToString() + "', '" + arrayShipPlan[lvar31, 15].ToString() + "', '" + arrayShipPlan[lvar31, 16].ToString() + "', '" + arrayShipPlan[lvar31, 17].ToString() + "', '" + arrayShipPlan[lvar31, 18].ToString() + "', '" + arrayShipPlan[lvar31, 19].ToString() + "', '" + arrayShipPlan[lvar31, 20].ToString() + "', '" + arrayShipPlan[lvar31, 21].ToString() + "', '" + arrayShipPlan[lvar31, 22].ToString() + "', '" + arrayShipPlan[lvar31, 23].ToString() + "', '" + arrayShipPlan[lvar31, 24].ToString() + "', '" + arrayShipPlan[lvar31, 25].ToString() + "', '" + arrayShipPlan[lvar31, 26].ToString() + "', '" + arrayShipPlan[lvar31, 27].ToString() + "', '" + arrayShipPlan[lvar31, 28].ToString() + "', '" + arrayShipPlan[lvar31, 29].ToString() + "', '" + arrayShipPlan[lvar31, 30].ToString() + "', '" + arrayShipPlan[lvar31, 31].ToString() + "', '" + arrayShipPlan[lvar31, 32].ToString() + "', '" + arrayShipPlan[lvar31, 33].ToString() + "', '" + arrayShipPlan[lvar31, 34].ToString() + "', '" + arrayShipPlan[lvar31, 35].ToString() + "', '" + arrayShipPlan[lvar31, 36].ToString() + "', '" + arrayShipPlan[lvar31, 37].ToString() + "', '" + arrayShipPlan[lvar31, 38].ToString() + "', '" + arrayShipPlan[lvar31, 39].ToString() + "', '" + arrayShipPlan[lvar31, 40].ToString() + "', '" + arrayShipPlan[lvar31, 41].ToString() + "', '" + arrayShipPlan[lvar31, 42].ToString() + "', '" + arrayShipPlan[lvar31, 43].ToString() + "', '" + arrayShipPlan[lvar31, 44].ToString() + "', '" + arrayShipPlan[lvar31, 45].ToString() + "', '" + arrayShipPlan[lvar31, 46].ToString() + "', '" + arrayShipPlan[lvar31, 47].ToString() + "', '" + arrayShipPlan[lvar31, 48].ToString() + "', '" + arrayShipPlan[lvar31, 49].ToString() + "', '" + arrayShipPlan[lvar31, 50].ToString() + "', '" + arrayShipPlan[lvar31, 51].ToString() + "', '" + arrayShipPlan[lvar31, 52].ToString() + "', '" + arrayShipPlan[lvar31, 53].ToString() + "', '" + arrayShipPlan[lvar31, 54].ToString() + "', '" + arrayShipPlan[lvar31, 55].ToString() + "', '" + arrayShipPlan[lvar31, 56].ToString() + "', '" + arrayShipPlan[lvar31, 57].ToString() + "', '" + arrayShipPlan[lvar31, 58].ToString() + "', '" + arrayShipPlan[lvar31, 59].ToString() + "', '" + arrayShipPlan[lvar31, 60].ToString() + "' , '" + arrayShipPlan[lvar31, 61].ToString() + "', '" + arrayShipPlan[lvar31, 62].ToString() + "', '" + arrayShipPlan[lvar31, 63].ToString() + "', '" + arrayShipPlan[lvar31, 64].ToString() + "', '" + arrayShipPlan[lvar31, 65].ToString() + "', '" + arrayShipPlan[lvar31, 66].ToString() + "', '" + arrayShipPlan[lvar31, 67].ToString() + "', '" + arrayShipPlan[lvar31, 68].ToString() + "', '" + arrayShipPlan[lvar31, 69].ToString() + "', '" + arrayShipPlan[lvar31, 70].ToString() + "', '" + arrayShipPlan[lvar31, 71].ToString() + "', '" + arrayShipPlan[lvar31, 72].ToString() + "', '" + arrayShipPlan[lvar31, 73].ToString() + "', '" + arrayShipPlan[lvar31, 74].ToString() + "', '" + arrayShipPlan[lvar31, 75].ToString() + "', '" + (seqno++).ToString() + "'  , '" + tmpDocumentID + "', '" + arrayShipPlan[lvar31, 78].ToString() + "', '" + arrayShipPlan[lvar31, 79].ToString() + "' ) ";
                if (!DataConnlib.Excute(locsql1))
                    Messg = "Failss";

                lvar31++;

            }  // End Witch SW2 

        }  // end  if (WriDBFFlag == "Y")

        int i1 = 0, i2 = 0, i3 = Convert.ToInt32(locrefstr1);

        for (i1 = 0; i1 < 2 + 1; i1++)
            for (i2 = 0; i2 < arrayShipPlanYLong; i2++)
                ResData[i1, i2] = HeadarrayShipPlan[i1, i2];

        for (i1 = 1; i1 < i3 + 1; i1++)
            for (i2 = 0; i2 < arrayShipPlanYLong; i2++)
                ResData[i1 + 2, i2] = arrayShipPlan[i1, i2];


    }  // end of TmpDV4A7OrgQueryWriDBFShipPlanarraytransDay

    public string GetEV10ToarrayEtdUpload(int Sumtype, DataSet tds3, int UploadETDrecordLong, ref string[,] arrayEtdUpload, int DelpcateEVNum, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int DuplicateDBFLoc, int IndexArrayLoc, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, int arraytransSecondAvailLoc,  ref string Programflag)
    {
        int v1 = 0, v4=0, v5 = 0, v2 = 0, v3 = 0, currloc = 0, xloop=50, arraylong=0, v6=0, v7=0, v8=0, v9=0;
        string forcastdate = "", s1 = "", ttype = "";

        string[,] tParam = new string[xloop+1, 3];

        tParam[0, 1] = "Consigned Inventory"; //Consigned Inventory
        tParam[0, 2] = "60";
        tParam[1, 1] = "GIT";
        tParam[1, 2] = "61";
        tParam[2, 1] = "On-Hand Inventory";
        tParam[2, 2] = "62";
        tParam[3, 1] = "Agreement";
        tParam[3, 2] = "63";
        tParam[4, 1] = "Item";
        tParam[4, 2] = "64";
        tParam[5, 1] = "GIT:BJ";
        tParam[5, 2] = "65";
        tParam[6, 1] = "GIT:LF";
        tParam[6, 2] = "66";
        tParam[7, 1] = "GIT:LH";
        tParam[7, 2] = "67";
        tParam[8, 1] = "GIT:Chennai";
        tParam[8, 2] = "68";
        tParam[9, 1] = "Blocked Stock";
        tParam[9, 2] = "69";
        tParam[10, 1] = "Quality Stock";
        tParam[10, 2] = "70";
        tParam[11, 1] = "Minimum Days of Supply Target";
        tParam[11, 2] = "71";
        tParam[12, 1] = "Maximum Days of Supply Target";
        tParam[12, 2] = "72";
        tParam[13, 1] = "Minimum Inventory Target";
        tParam[13, 2] = "73";
        tParam[14, 1] = "Maximum Inventory Target";
        tParam[14, 2] = "74";

        // F60:    Consigned         / Consigned Inventory  ( 130-10+1 )
        // F61:    GIT               / GIT                  ( 130-10+2 )
        // F62:    On-Hand Inventory / On-Hand Inventory    ( 130-10+3 )
        // F63:    Agreement                                ( 130-10+4 )
        // F64:    Item                                     ( 130-10+5 )
        // F65:    GIT BJ                                   ( 130-10-7 )
        // F66:    GIT LF                                   ( 130-10-8 ) 
        // F67:    GIT LH                                   ( 130-10-9 )
        // F68:    GIT Chennai                              ( 130-10-10 )
        // F69:    Block    / Blocked Stock                   ( 130-10-1)
        // F70:    Quality  / Quality Stock                   ( 130-10-2)
        // F71:    Min Days / Minimum Days of Supply Target   ( 130-10-3)
        // F72:    Max Days / Maximum Days of Supply Target   ( 130-10-4) 
        // F73:    Min Inventory  / Minimum Inventory Target  ( 130-10-5)
        // F74:    Max Inventory  / Maximum Inventory Target  ( 130-10-6)
        //////////////////////////////////////////////////////////////////////////
        // Make a new table 1-- 291 Data 291+1=292
        // 291+1 -- 1000 Data (1) Forecast_QtyTypeCode (2) Forecast_BeginDate 
        //                    (3) Forecast_IntervalCode (4) Forecast_Qty

        string tmpDocument_ID = "", tmpCustomer_PN = "", tmpFoxconnSite="", s2="", s3="";
        int currseq = 0, currfield = arrayCustomerFoxconnPNToOneSetIndex + 1;
        v5 = tds3.Tables[0].Rows.Count;

        v1 = 50;
        for (v2 = 0; v2 < v1; v2++)
        {
             PL[v2, 1] = P5[v2, 1];
             PL[v2, 2] = P5[v2, 2];
        }
                         
        
        if (Sumtype == 51)
        {
            for (v2 = 1; v2 < eachIDSetCount + 1; v2++)  // 4A3_Detail_PNOneSet ++
            {
                tmpDocument_ID = arrayCustomerFoxconnPNToOneSet[v2, 4];  // Document_ID
                tmpCustomer_PN = arrayCustomerFoxconnPNToOneSet[v2, 3];  // CustomerPN
                if (Programflag == "6") tmpFoxconnSite = arrayCustomerFoxconnPNToOneSet[v2, 2];  // FoxconnSite
                if (Programflag == "7") tmpFoxconnSite = arrayCustomerFoxconnPNToOneSet[v2, 2];  // FoxconnSite
                currfield = arrayCustomerFoxconnPNToOneSetIndex + 1;

                v1 = currseq;
                for (v1 = currseq; v1 < v5; v1++)
                {
                    ttype = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                    // if (Programflag == "6") s3 = tmpFoxconnSite = tds3.Tables[0].Rows[v1]["FoxSite"].ToString(); // '" + tds3.Tables[0].Rows[v1]["FoxSite"].ToString() + "' ) ";
                    if ((Programflag == "6") || (Programflag == "7") ) s3 = tds3.Tables[0].Rows[v1]["FoxSite"].ToString(); // '" + tds3.Tables[0].Rows[v1]["FoxSite"].ToString() + "' ) ";
                    else s3 = "";


                    if ((tmpDocument_ID == tds3.Tables[0].Rows[v1]["Document_ID"].ToString()) && (tmpCustomer_PN == tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString()) && ( tmpFoxconnSite ==  s3 ) )
                    {
                        v3++;
                        if (ttype == "Discrete Gross Demand")
                        {
                            arrayCustomerFoxconnPNToOneSet[v2, currfield++] = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                            arrayCustomerFoxconnPNToOneSet[v2, currfield++] = tds3.Tables[0].Rows[v1]["Forecast_BeginDate"].ToString().Substring(0, 8);
                            arrayCustomerFoxconnPNToOneSet[v2, currfield++] = tds3.Tables[0].Rows[v1]["Forecast_IntervalCode"].ToString();

                            if  (Programflag == "6")
                                arrayCustomerFoxconnPNToOneSet[v2, currfield++] = FSplitlibPointer.TrsStrToInteger(tds3.Tables[0].Rows[v1]["Site_Net_Demand"].ToString());
                            else if (Programflag == "7")
                                arrayCustomerFoxconnPNToOneSet[v2, currfield++] = FSplitlibPointer.TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString());
                            else
                                arrayCustomerFoxconnPNToOneSet[v2, currfield++] = FSplitlibPointer.TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString());
        
                            forcastdate = tds3.Tables[0].Rows[v1]["Forecast_BeginDate"].ToString();
                            forcastdate = forcastdate.Substring(0, 8);

                            if (String.Compare((arrayCustomerFoxconnPNToOneSet[v2, 6]), forcastdate) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                                arrayCustomerFoxconnPNToOneSet[v2, 6] = forcastdate;
                            s1 = arrayCustomerFoxconnPNToOneSet[v2, 7].ToString(); // trace
                            if (String.Compare((arrayCustomerFoxconnPNToOneSet[v2, 7]), forcastdate) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                                arrayCustomerFoxconnPNToOneSet[v2, 7] = forcastdate;
                            if (currfield >= 1000) 
                                return ("-1");
                        }
                        else // not (ttype == "Discrete Gross Demand")
                        {
                            // if (ttype == "Consigned Inventory")
                            //     v4 = 0;

                            for ( v4=0; v4 < xloop; v4++ )
                                if (ttype == PL[v4, 1])  // if (ttype == tParam[v4, 1])
                                {
                                    // v6 = Convert.ToInt32(tParam[v4, 2]); // trace
                                    if (Programflag == "6") arrayCustomerFoxconnPNToOneSet[v2, Convert.ToInt32( PL[v4, 2])] = tds3.Tables[0].Rows[v1]["Site_Net_Demand"].ToString();
                                    else if (Programflag == "7") arrayCustomerFoxconnPNToOneSet[v2, Convert.ToInt32(PL[v4, 2])] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                                    else arrayCustomerFoxconnPNToOneSet[v2, Convert.ToInt32( PL[v4, 2])] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                                    v4 = xloop + 1; // break
                                }

                        }

                    }
                    else
                    {
                        currseq = v1;  // Save Curr point
                        v1 = v5 + 1;          // break
                    }
                } // end  for (v1 = currseq; v1 < v5; v1++)

            }  // end for (v2 = 1; v2 < eachIDSetCount + 1; v2++) 
        }  // end if Sumtype == 1
   
        return (v3.ToString());
    }  // End GetEV10ToarrayEtdUpload


    // Not Used 
    private void TmpOrgDVQueryWriDBFShipPlanarraytransDay(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref DateTime firstThuDate, ref string tmpSPWeek, ref string tmpDocumentID)  //    
    {
        int lvar31 = 1, seqno = 1000;
        int localvar1 = 7;
        int localvar2 = 7;
        int localvar3 = 7;
        int localvar4 = 7, v1 = 0;
        string locsql1 = "";
        string locstr1 = "";
        string locstr2 = "";
        string tspace = "";
        string Messg = "";

        string[,] HeadarrayShipPlan = new string[4, arrayShipPlanYLong + 1];

        for (lvar31 = 0; lvar31 < 3 + 1; lvar31++) for (localvar1 = 0; localvar1 < arrayShipPlanYLong + 1; localvar1++) HeadarrayShipPlan[lvar31, localvar1] = "";

        // Set Up Print Head   // array7 開使始為星期四                               Thu       Fri      Sat       Sun        ToT
        // Project CustomerSite Description CustomerPN H.H P/N FoxconnSite Dom_Exp  20100415  20100416 20100417  20100418

        HeadarrayShipPlan[2, 0] = " Project";
        HeadarrayShipPlan[2, 1] = " CustomerSite";
        HeadarrayShipPlan[2, 2] = " Description";
        HeadarrayShipPlan[2, 3] = " CustomerPN";
        HeadarrayShipPlan[2, 4] = " H.H P/N";
        HeadarrayShipPlan[2, 5] = " FoxconnSite";
        HeadarrayShipPlan[2, 6] = " Dom_Exp";

        HeadarrayShipPlan[2, 56] = " datafrom";
        HeadarrayShipPlan[2, 57] = " DocumentID";
        HeadarrayShipPlan[2, 60] = " Consigned";
        HeadarrayShipPlan[2, 61] = " GIT";
        HeadarrayShipPlan[2, 62] = " On-Hand";
        HeadarrayShipPlan[2, 63] = " Agreement";
        HeadarrayShipPlan[2, 64] = " Item";
        HeadarrayShipPlan[2, 65] = " GIT BJ";
        HeadarrayShipPlan[2, 66] = " GIT LF";
        HeadarrayShipPlan[2, 67] = " GIT LH";
        HeadarrayShipPlan[2, 68] = " GIT Chennai";
        HeadarrayShipPlan[2, 69] = " Blocked Stock";
        HeadarrayShipPlan[2, 70] = " Quality Stock";
        HeadarrayShipPlan[2, 71] = " Minimum";
        HeadarrayShipPlan[2, 72] = " Maximum";
        HeadarrayShipPlan[2, 73] = " Minimum";
        HeadarrayShipPlan[2, 74] = " Maximum";
        //    (Convert.ToInt32(tmpMaxMin.DayOfWeek)).ToString();

        v1 = tmpSPWeek.Length;
        localvar4 = Convert.ToInt32(tmpSPWeek.Substring(7, v1 - 7));  // 第幾週
        if ((Convert.ToInt32(tmptoday.DayOfWeek) >= 1) && (Convert.ToInt32(tmptoday.DayOfWeek) <= 3))
            localvar4 = localvar4 - 1; // Pre Week

        HeadarrayShipPlan[0, 7] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 7 + 8] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 7 + 16] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();
        HeadarrayShipPlan[0, 7 + 24] = " " + tmpSPWeek.Substring(0, 6) + (localvar4++).ToString();

        // HeadarrayShipPlan[2, 7] = (firstThuDate.AddDays(localvar1++)).ToString("yyyyMMdd");
        localvar3 = 0;
        localvar1 = 4;
        for (localvar2 = 7; localvar2 <= 34; localvar2++)
        {
            localvar1 = localvar1 % 7;

            switch (localvar1)                   //
            {                                    // 
                case 0: locstr1 = " Sun"; break; // 用 var1 變數當                        
                case 1: locstr1 = " Mon"; break; //  0 為目前                                                    
                case 2: locstr1 = " Tue"; break; //  1 為下周 + 離周一偏移為置
                case 3: locstr1 = " Wed"; break; //
                case 4: locstr1 = " Thu"; break; //   
                case 5: locstr1 = " Fri"; break; //   
                case 6: locstr1 = " Sat"; break; //   
                default: locstr1 = ""; break;     //
            }

            HeadarrayShipPlan[1, localvar2] = locstr1;
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3++)).ToString("yyyyMMdd");

            if (localvar1 == 0) HeadarrayShipPlan[2, ++localvar2] = "Total";  // Week total


            localvar1++;

        }

        for (localvar2 = 36; localvar2 <= 54; localvar2++)
        {
            HeadarrayShipPlan[1, localvar2] = "WK" + (localvar4).ToString();
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3)).ToString("yyyyMMdd");
            localvar4 = localvar4 + 1;
            localvar3 = localvar3 + 7;
        }

        HeadarrayShipPlan[2, localvar2] = "Total"; // The Last End

        lvar31 = 0;
        while ((lvar31 <= 2) && (PrintHeadSW == 0))  // write head 
        {

            locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F59, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74) Values ( '" + HeadarrayShipPlan[lvar31, 0].ToString() + "', '" + HeadarrayShipPlan[lvar31, 1].ToString() + "', '" + HeadarrayShipPlan[lvar31, 2].ToString() + "', '" + HeadarrayShipPlan[lvar31, 3].ToString() + "', '" + HeadarrayShipPlan[lvar31, 4].ToString() + "', '" + HeadarrayShipPlan[lvar31, 5].ToString() + "', '" + HeadarrayShipPlan[lvar31, 6].ToString() + "', '" + HeadarrayShipPlan[lvar31, 7].ToString() + "', '" + HeadarrayShipPlan[lvar31, 8].ToString() + "', '" + HeadarrayShipPlan[lvar31, 9].ToString() + "', '" + HeadarrayShipPlan[lvar31, 10].ToString() + "', '" + HeadarrayShipPlan[lvar31, 11].ToString() + "', '" + HeadarrayShipPlan[lvar31, 12].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 13].ToString() + "', '" + HeadarrayShipPlan[lvar31, 14].ToString() + "', '" + HeadarrayShipPlan[lvar31, 15].ToString() + "', '" + HeadarrayShipPlan[lvar31, 16].ToString() + "', '" + HeadarrayShipPlan[lvar31, 17].ToString() + "', '" + HeadarrayShipPlan[lvar31, 18].ToString() + "', '" + HeadarrayShipPlan[lvar31, 19].ToString() + "', '" + HeadarrayShipPlan[lvar31, 20].ToString() + "', '" + HeadarrayShipPlan[lvar31, 21].ToString() + "', '" + HeadarrayShipPlan[lvar31, 22].ToString() + "', '" + HeadarrayShipPlan[lvar31, 23].ToString() + "', '" + HeadarrayShipPlan[lvar31, 24].ToString() + "', '" + HeadarrayShipPlan[lvar31, 25].ToString() + "', '" + HeadarrayShipPlan[lvar31, 26].ToString() + "', '" + HeadarrayShipPlan[lvar31, 27].ToString() + "', '" + HeadarrayShipPlan[lvar31, 28].ToString() + "', '" + HeadarrayShipPlan[lvar31, 29].ToString() + "', '" + HeadarrayShipPlan[lvar31, 30].ToString() + "', '" + HeadarrayShipPlan[lvar31, 31].ToString() + "', '" + HeadarrayShipPlan[lvar31, 32].ToString() + "', '" + HeadarrayShipPlan[lvar31, 33].ToString() + "', '" + HeadarrayShipPlan[lvar31, 34].ToString() + "', '" + HeadarrayShipPlan[lvar31, 35].ToString() + "', '" + HeadarrayShipPlan[lvar31, 36].ToString() + "', '" + HeadarrayShipPlan[lvar31, 37].ToString() + "', '" + HeadarrayShipPlan[lvar31, 38].ToString() + "', '" + HeadarrayShipPlan[lvar31, 39].ToString() + "', '" + HeadarrayShipPlan[lvar31, 40].ToString() + "', '" + HeadarrayShipPlan[lvar31, 41].ToString() + "', '" + HeadarrayShipPlan[lvar31, 42].ToString() + "', '" + HeadarrayShipPlan[lvar31, 43].ToString() + "', '" + HeadarrayShipPlan[lvar31, 44].ToString() + "', '" + HeadarrayShipPlan[lvar31, 45].ToString() + "', '" + HeadarrayShipPlan[lvar31, 46].ToString() + "', '" + HeadarrayShipPlan[lvar31, 47].ToString() + "', '" + HeadarrayShipPlan[lvar31, 48].ToString() + "', '" + HeadarrayShipPlan[lvar31, 49].ToString() + "', '" + HeadarrayShipPlan[lvar31, 50].ToString() + "', '" + HeadarrayShipPlan[lvar31, 51].ToString() + "', '" + HeadarrayShipPlan[lvar31, 52].ToString() + "', '" + HeadarrayShipPlan[lvar31, 53].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 54].ToString() + "', '" + HeadarrayShipPlan[lvar31, 55].ToString() + "', '" + tspace + "' , '" + tmpDocumentID + "', '" + HeadarrayShipPlan[lvar31, 58].ToString() + "', '" + HeadarrayShipPlan[lvar31, 59].ToString() + "', '" + HeadarrayShipPlan[lvar31, 60].ToString() + "', '" + HeadarrayShipPlan[lvar31, 61].ToString() + "', '" + HeadarrayShipPlan[lvar31, 62].ToString() + "', '" + HeadarrayShipPlan[lvar31, 63].ToString() + "', '" + HeadarrayShipPlan[lvar31, 64].ToString() + "', '" + HeadarrayShipPlan[lvar31, 65].ToString() + "', '" + HeadarrayShipPlan[lvar31, 66].ToString() + "', '" + HeadarrayShipPlan[lvar31, 67].ToString() + "', '" + HeadarrayShipPlan[lvar31, 68].ToString() + "', '" + HeadarrayShipPlan[lvar31, 69].ToString() + "', '" + HeadarrayShipPlan[lvar31, 70].ToString() + "', '" + HeadarrayShipPlan[lvar31, 71].ToString() + "', '" + HeadarrayShipPlan[lvar31, 72].ToString() + "', '" + HeadarrayShipPlan[lvar31, 73].ToString() + "', '" + HeadarrayShipPlan[lvar31, 74].ToString() + "' ) ";
            if (DataConnlib.Excute(locsql1))
            {
                // Response.Write("<script>alert('Test_Syncro_ShippingPlan_ETA 新增成功！')</script>");
                Messg = "Sucess";
            }
            else
            {
                //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                //Response.Write("<script>alert('TestDBFShipPlan Head 新增失敗，請稍后重試！ ')</script>");
                Messg = "Error";
            }

            lvar31++;
        }


        lvar31 = 1;
        while (arrayShipPlan[lvar31, 1] != "")   // write data
        {

            locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53, F54, F55, F56, F57, F58, F60, F61, F62, F63, F64, F65, F66, F67, F68, F69, F70, F71, F72, F73, F74, F75, F76 ) Values ( '" + arrayShipPlan[lvar31, 0].ToString() + "', '" + arrayShipPlan[lvar31, 1].ToString() + "', '" + arrayShipPlan[lvar31, 2].ToString() + "', '" + arrayShipPlan[lvar31, 3].ToString() + "', '" + arrayShipPlan[lvar31, 4].ToString() + "', '" + arrayShipPlan[lvar31, 5].ToString() + "', '" + arrayShipPlan[lvar31, 6].ToString() + "', '" + arrayShipPlan[lvar31, 7].ToString() + "', '" + arrayShipPlan[lvar31, 8].ToString() + "', '" + arrayShipPlan[lvar31, 9].ToString() + "', '" + arrayShipPlan[lvar31, 10].ToString() + "', '" + arrayShipPlan[lvar31, 11].ToString() + "', '" + arrayShipPlan[lvar31, 12].ToString() + "',  '" + arrayShipPlan[lvar31, 13].ToString() + "', '" + arrayShipPlan[lvar31, 14].ToString() + "', '" + arrayShipPlan[lvar31, 15].ToString() + "', '" + arrayShipPlan[lvar31, 16].ToString() + "', '" + arrayShipPlan[lvar31, 17].ToString() + "', '" + arrayShipPlan[lvar31, 18].ToString() + "', '" + arrayShipPlan[lvar31, 19].ToString() + "', '" + arrayShipPlan[lvar31, 20].ToString() + "', '" + arrayShipPlan[lvar31, 21].ToString() + "', '" + arrayShipPlan[lvar31, 22].ToString() + "', '" + arrayShipPlan[lvar31, 23].ToString() + "', '" + arrayShipPlan[lvar31, 24].ToString() + "', '" + arrayShipPlan[lvar31, 25].ToString() + "', '" + arrayShipPlan[lvar31, 26].ToString() + "', '" + arrayShipPlan[lvar31, 27].ToString() + "', '" + arrayShipPlan[lvar31, 28].ToString() + "', '" + arrayShipPlan[lvar31, 29].ToString() + "', '" + arrayShipPlan[lvar31, 30].ToString() + "', '" + arrayShipPlan[lvar31, 31].ToString() + "', '" + arrayShipPlan[lvar31, 32].ToString() + "', '" + arrayShipPlan[lvar31, 33].ToString() + "', '" + arrayShipPlan[lvar31, 34].ToString() + "', '" + arrayShipPlan[lvar31, 35].ToString() + "', '" + arrayShipPlan[lvar31, 36].ToString() + "', '" + arrayShipPlan[lvar31, 37].ToString() + "', '" + arrayShipPlan[lvar31, 38].ToString() + "', '" + arrayShipPlan[lvar31, 39].ToString() + "', '" + arrayShipPlan[lvar31, 40].ToString() + "', '" + arrayShipPlan[lvar31, 41].ToString() + "', '" + arrayShipPlan[lvar31, 42].ToString() + "', '" + arrayShipPlan[lvar31, 43].ToString() + "', '" + arrayShipPlan[lvar31, 44].ToString() + "', '" + arrayShipPlan[lvar31, 45].ToString() + "', '" + arrayShipPlan[lvar31, 46].ToString() + "', '" + arrayShipPlan[lvar31, 47].ToString() + "', '" + arrayShipPlan[lvar31, 48].ToString() + "', '" + arrayShipPlan[lvar31, 49].ToString() + "', '" + arrayShipPlan[lvar31, 50].ToString() + "', '" + arrayShipPlan[lvar31, 51].ToString() + "', '" + arrayShipPlan[lvar31, 52].ToString() + "', '" + arrayShipPlan[lvar31, 53].ToString() + "', '" + arrayShipPlan[lvar31, 54].ToString() + "', '" + arrayShipPlan[lvar31, 55].ToString() + "', '" + tmpdatafrom + "', '" + tmpDocumentID + "', '" + arrayShipPlan[lvar31, 58].ToString() + "', '" + arrayShipPlan[lvar31, 60].ToString() + "' , '" + arrayShipPlan[lvar31, 61].ToString() + "', '" + arrayShipPlan[lvar31, 62].ToString() + "', '" + arrayShipPlan[lvar31, 63].ToString() + "', '" + arrayShipPlan[lvar31, 64].ToString() + "', '" + arrayShipPlan[lvar31, 65].ToString() + "', '" + arrayShipPlan[lvar31, 66].ToString() + "', '" + arrayShipPlan[lvar31, 67].ToString() + "', '" + arrayShipPlan[lvar31, 68].ToString() + "', '" + arrayShipPlan[lvar31, 69].ToString() + "', '" + arrayShipPlan[lvar31, 70].ToString() + "', '" + arrayShipPlan[lvar31, 71].ToString() + "', '" + arrayShipPlan[lvar31, 72].ToString() + "', '" + arrayShipPlan[lvar31, 73].ToString() + "', '" + arrayShipPlan[lvar31, 74].ToString() + "', '" + arrayShipPlan[lvar31, 75].ToString() + "' ) ";
            if (DataConnlib.Excute(locsql1))
            {
                // Response.Write("<script>alert('Test_Syncro_ShippingPlan_ETA 新增成功！')</script>");
            }
            else
            {
                //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                // Response.Write("<script>alert('TestDBFShipPlan 新增失敗，請稍后重試！ ')</script>");
                Messg = "Error";
            }

            lvar31++;
        }


    }  // end of TmpDVOrgQueryWriDBFShipPlanarraytransDay

}  // end public class FSplitArraylib

}  // end namespace Economy.Publibrary


   // Not Used 20110310 
   // write to GSCMDOrgShipPlan memory 20110308
   // private void GSCMDOrg4A3TransDayToShipPlan(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayVar)  //    
   // {
   //     int lvar31 = 1;
   //     int CurrShipPlanLine = 0;
   //     int first4loc = 0;
   //     int prefirst4loc = 0;
   //     int first4W1 = 0;
   //     int locvar1 = 0;
   //     int locvar2 = 0;
   //     int locvar3 = 0;
   //     int loopcnt = 7;
   //     int readheadloc = 0;
   //     int readtailloc = 0;
   //     int subtot = 0;
   //     int tottot = 0;
   //     int tmpMPQ = 0;
   //
   //     DateTime ltmpdate1 = DateTime.Today;
   //     string locsql1 = "";
   //     string locsql2 = "";
   //     string tmpp1, tmpd1, tmpr1, tmpr2, tmpr3;
   //     string s1, s2, s3, s4, s5, s6, s7, s8, d9;
   //
   //     string tmptransDay = "";
   //     string tmpShipPlan = "";
   //
        //                    周四       C00                     C01                     C02                     C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01000000
        //   1  2  3  4  5  6  7  8  910  11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
   //     tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15W16W17W18W19E01EEEEEE"; // first4loc 加地第一個周4
   //     tmpShipPlan = "007008009010011012013014015016017018019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055056057";    // first4loc

   //     CurrShipPlanLine = Convert.ToInt32(locrefstr1);                            // 第幾個 Array
   //
   //     if (CurrShipPlanLine == 31)
   //         CurrShipPlanLine = CurrShipPlanLine;

        //      Project CustomerSite Description CustomerPN H.H P/N FoxconnSite Dom_Exp  20100415  20100416 20100417  20100418
    //    arrayShipPlan[CurrShipPlanLine, 0] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 0].ToString(); // Project
    //    arrayShipPlan[CurrShipPlanLine, 1] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 1].ToString(); // Customer
    //    s1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 29].ToString(); // Description
    //    arrayShipPlan[CurrShipPlanLine, 2] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 29].ToString(); // Description  
    //    arrayShipPlan[CurrShipPlanLine, 3] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 3].ToString(); // CustomerPN  
    //    arrayShipPlan[CurrShipPlanLine, 4] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 8].ToString(); // HHPN  
    //    arrayShipPlan[CurrShipPlanLine, 5] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 2].ToString(); // FoxconnSite  
    //    arrayShipPlan[CurrShipPlanLine, 6] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 9].ToString(); // Dom_Exp  
    //    arrayShipPlan[CurrShipPlanLine, 57]= arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 28].ToString(); // DocumentID  
    //    if ( CurrShipPlanLine >= 420 )
    //         locsql1 = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom
    //    arrayShipPlan[CurrShipPlanLine, 56]= arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 56].ToString(); // datafrom  
         
        // F00:    Project
        // F01:    CustomerSite
        // F02:    FoxconnRegion           
        // F03:    Forecast_CustomerPN
        // F04:    Document_ID
        // F05:    DocumentTime
        // F06:    Min
        // F07:    Max
        // F08:    HHPN
        // F09:    Dom_Exp
        // F10:    Reserved
        // F11:    Reserved
        // F28:    Return DoucmentID
        // F29:    Desp
        // F56:    datafrom
        // F57:    DocumentID ( Private )
        // F60:    Agreement                                ( 130-10+4 )
        // F61:    Item                                     ( 130-10+5 )
        // F62:    Consigned         / Consigned Inventory  ( 130-10+1 )
        // F63:    GIT               / GIT                  ( 130-10+2 )
        // F64:    On-Hand Inventory / On-Hand Inventory    ( 130-10+3 )
        // F65:    GIT BJ                                   ( 130-10-7 )
        // F66:    GIT LF                                   ( 130-10-8 ) 
        // F67:    GIT LH                                   ( 130-10-9 )
        // F68:    GIT Chennai                              ( 130-10-10 )
        // F69:    Block    / Blocked Stock                   ( 130-10-1)
        // F70:    Quality  / Quality Stock                   ( 130-10-2)
        // F71:    Min Days / Minimum Days of Supply Target   ( 130-10-3)
        // F72:    Max Days / Maximum Days of Supply Target   ( 130-10-4) 
        // F73:    Min Inventory  / Minimum Inventory Target  ( 130-10-5)
        // F74:    Max Inventory  / Maximum Inventory Target  ( 130-10-6)

     //   for ( locvar1=60; locvar1 <= 80; locvar1++ )
     //       arrayShipPlan[CurrShipPlanLine, locvar1] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, locvar1].ToString(); //   
     //  
     //   arrayShipPlan[CurrShipPlanLine, 58] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, arraytransSecondAvailLoc].ToString(); // NetQty 20100704  

     //    locvar1 = Convert.ToInt32(arraytransDay[tmptodaylocation, 10].ToString()); // 星期幾 ? 找周4
     //   if (locvar1 == 0) first4loc = tmptodaylocation - 7 + 4;                    // 第一個周4位置
     //   else if (locvar1 >= 4) first4loc = tmptodaylocation - locvar1 + 4;       //
     //   else first4loc = tmptodaylocation - locvar1 + 4 - 7;   //
     //
     //   prefirst4loc = first4loc - 1; // count from 0
     //   readheadloc = prefirst4loc;
     //   readtailloc = 3 * 0;  //  3 * 1;
     //   int firstW1 = prefirst4loc + 4 + 21;


     //   while (tmptransDay.Substring(readtailloc, 3) != "EEE")
     //   {
     //       s1 = tmptransDay.Substring(readtailloc, 3);    // 此為第一個周4, 所以須加  first4loc
     //       s2 = tmpShipPlan.Substring(readtailloc, 3);    // 此為 OutPut array 位置
     //       if (s1.Substring(0, 1).ToUpper() == "E")
     //           arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = tottot.ToString();
     //       else
     //           if (s1.Substring(0, 1).ToUpper() == "C")
     //           {
     //               arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
     //               subtot = 0;
     //           }
     //           else
     //               if (s1.Substring(0, 1).ToUpper() == "W")
     //               {
     //                   subtot = 0;
     //                   s3 = s1.Substring(1, 2);
     //                   locvar2 = firstW1 + Convert.ToInt32(s3) * 7 - 7;  // Week 起始日往後加 7 天
     //                   for (locvar1 = 0; locvar1 < 7; locvar1++)
     //                       subtot = subtot + Convert.ToInt32(arraytransDay[locvar2 + locvar1, 4]); // 加一周  Org 27 
     //                   arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
     //                   tottot = subtot + tottot;
     //                   subtot = 0;
     //               }
     //               else
     //               {
     //                   tmpMPQ = Convert.ToInt32(arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString());
     //                   arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 4].ToString();
     //                   subtot = tmpMPQ + subtot;
     //                   tottot = tmpMPQ + tottot;
     //               }  // end if 
     //       readtailloc = readtailloc + 3;
     //        }  // end while    
     // }  // end of GSCMDOrg4A3TransDayToShipPlan
///////////////////////////////////////////////////////////////////////////////////////////////////////////

