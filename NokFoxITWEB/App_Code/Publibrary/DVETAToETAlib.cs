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
using System.Linq;
using Economy.Publibrary;



/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
/// 
public class DVETAToETAlib
{
    public void DVETAToETAlib1()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    // protected string FileName;
    // protected string FileType;
    // protected string ServerPath;
    // protected string Upload_File;
    // protected string StrTicks;
    // protected int Excel_int;
    // protected string ExcelFile;
    // protected string siteName = "NLV";
    // protected string currency = "RMB";   //幣種
    // protected string tableDate = "";      //報表日期
    // protected string userName = "EricNLV";
    // protected string BUType = "NLV";
    // protected bool flag = false;
    // protected string mark = "";
    protected DataSet ds;
    protected SqlConnection conn;
    protected SqlCommand cmd, command;
    protected string Programstatus = "";
    protected string Programtype = "3";  // 1. Main Action 2. Main Loop Not Login 3. Been called once Client 

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //   if (!IsPostBack)
    //    {
    //        lblMsg = "";
    //        text1 = DateTime.Today.ToString("yyyy-MM-dd");
    //    }
    //
    //   if (Programtype == "2")
    //       this.PreSelfloopProcETDToETA();  // Response.Redirect("http://10.186.19.104/SyncroDVConsole");
    // 
    //   if (Programtype == "3")
    //   {
    //       this.ProcETDToETA();
    //       Response.Write("Finish!");
    //       Thread.Sleep(10000);	 // 10 Min 6000 000---休眠2秒鐘 2000
    //       Response.Write("<SCRIPT   LANGUAGE='JavaScript'>window.opener=null;window.close();</SCRIPT> ");
    //   }
    // 
    //}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  Test Process 20100115 Ken Beijing
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    // Initial Global Variable

    protected DateTime tmptoday = DateTime.Today;
    protected DateTime datetimebuf = DateTime.Today;
    protected DateTime MinStr = DateTime.Today;
    protected DateTime MaxStr = DateTime.Today;
    protected DateTime tmpMaxMin = DateTime.Today;
    protected DateTime firstThuDate = DateTime.Today;

    protected string ErrMsg = "";
    protected string tmpUploadType = "EVPV";
    protected string tmpDocumentID = "";
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
    protected int arraytransDayY35 = 35;
    protected int tmpReqProcETDToETAVar = 0;
    protected int arrayCustomerFoxconnPNToOneSetLong = 2800;
    protected int arrayCustomerFoxconnPNToOneSetIndex = 130;
    protected int arrayShipPlanXLong = 2800;
    protected int arrayShipPlanYLong = 60;
    protected int DuplicateDBLong = 10000;
    protected int arrayNokiaEVDocmLong = 0;
    protected int arrayNokiaPVDocmLong = 0;


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
    protected char Programflag = '2';                                // 1. Upload  2.Download 3. Search
    protected char WriDBFShipPlanFlag = 'Y';
    protected char WriETAarraytransDayflag = 'N';

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
    protected int arrayParamLong = 20;
    protected int DelpcateEVNum = 0;
    protected int MainloopReadHead = 0;
    protected int MainloopReadTail = 0;
    protected int IndexArrayLoc = 10;  // save in next 1
    protected int MaxMimDVLong = 1;
    protected int WriteCount = 0;
    protected int WriteDBFBaseLong = 7 * 19;     // 回寫資料基本長度
    protected int WriteDBFBaseLongLoc = 7 * 19;   // 回寫資料長度位置
    protected int DuplicateDBFLoc = 0;     // Error BDF Loc 
    protected int firstThuDateloc = 0;
    protected int FirstDVLoc = 101;
    protected int FirstPVLoc = 104;
    protected int arrayGITWriLong = 4000;


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
    string sql23 = "";

    protected string DownDVDay = DateTime.Today.ToString("yyyyMMdd");  // 20100320
    protected string ds3 = "";

    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();

    static string text1 = "", text2 = "", text3 = "", text4 = "", text5 = "", text6 = "", text7 = "", text8 = "", text9 = "", lblMsg = "";

    // text8 為傳參數 1. Setup Write MemoryCurrNextDos 明細 text8 = "Y" + text8.Substring(1, (text8.Length)-1);
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Data From                               Accept Paramater    Foxconn Part             4A5_detail_plant                                               
    // Para 0  : free                    1     ==================================================================
    // Para 1  : Customer      那各客戶 ALL    (Y) accept 
    // para 2  : CustomerSite  客戶Site ALL    (Y) accept map --> Fox CustomerDum to para3
    // para 3  : CustomerDukNo 客戶Site ALL             get from para2                               
    // para 4  : FoxconnSite            ALL    (Y) accept                                       Foxconn_Site ( 11, 2)
    // para 5  : FoxconnDukNo  Fox Site ALL
    // para 6  : NokiaPartnoNo          ALL    (Y) accept                                       Forecast_CustomerPN
    // para 7  : Foxconn Partno         ALL              
    // para 8  : FoxconnBU              ALL    (Y) accept 
    // para 9  : project                ALL    (Y) accept 
    // para 10 : subproject             ALL    (Y) accept 
    // para 11 : FoxconnRegion          ALL    (Y) accept 
    // para 12 : Type                   ALL    (Y) accept 
    // 1. X 軸 代表多少單位, 第一欄位 ALL 代表全部
    // 2. Y 軸 代表  CustomerSite, FoxconnSite,,
    //
    // Algorithm 1: Accepted Paramater from calling or other
    //           2. Convert  para2 to CustomerDum to param3  
    //           3. para4 accepted OK
    //           4. para6 accept OK
    ///////////////////////////////////////////////////////////////////////////////////////////
    private void GetArrarParaForCalcCurrNextDosMPQ(ref string subtmpstr1, ref string[,] arrayParam, ref string[,] arrayPart) //
    {
        // Substring(10, 2)  544790470:LF:MPELF ( 11,2 ) 
        string subs2 = "";
        string subs4 = "";
        string subs6 = "";

        // arrayParam[2, 1] = "ALL";                 // All CustomerSite
        arrayParam[2, 2] = "Beijing";                // Customersite 
        arrayParam[2, 3] = "LH";
        arrayParam[2, 4] = "LF";
        arrayParam[2, 5] = "Dongguan";
        arrayParam[2, 6] = "Manuas";
        // arrayParam[4, 2] = "LF";                  // FoxconnSite
        // arrayParam[4, 3] = "BJ";
        arrayParam[4, 2] = "LH";
        // arrayParam[6, 2] = "0254273";  
        // arrayParam[4, 3] = "Beijing";
        // arrayParam[4, 4] = "LF";


        subtmpstr1 = "select * from Syncro_4A5_Detail_plant where ";   //
        subs4 = "";   // 

        if (arrayParam[2, 1] == "ALL") subs2 = "";         // all CustomerSite
        else
        {
            for (var2 = 2; var2 < arrayParamLong + 1; var2++)
            {
                if (arrayParam[2, var2] == "") var2 = arrayParamLong + 1;
                else
                {
                    if (subs2 != "") subs2 = subs2 + " or ";
                    //                subs4 = subs4 + " Substring(Foxconn_Site, 11, 2) =  '" + arrayParam[4, var2] + "' ";
                    subs2 = subs2 + " C.Plant_Name = '" + arrayParam[2, var2] + "' "; // C.Customer_Site
                }
            }
            subs2 = " and ( " + subs2 + " ) ";
        }

        if (arrayParam[4, 1] == "ALL") subs4 = "";         // all FoxconnSite
        else
        {
            for (var2 = 2; var2 < arrayParamLong + 1; var2++)
            {
                if (arrayParam[4, var2] == "") var2 = arrayParamLong + 1;
                else
                {
                    if (subs4 != "") subs4 = subs4 + " or ";
                    //                subs4 = subs4 + " Substring(Foxconn_Site, 11, 2) =  '" + arrayParam[4, var2] + "' ";
                    subs4 = subs4 + " substring (substring(Foxconn_Site,11,len(Foxconn_Site)),0,CHARINDEX(':',substring(Foxconn_Site,11,len(Foxconn_Site)))) = '" + arrayParam[4, var2] + "' ";
                }
            }
            subs4 = " and ( " + subs4 + " ) ";
        }

        DownDVDay = text1.Substring(0, 4) + text1.Substring(5, 2) + text1.Substring(8, 2); // text1 = DateTime.Today.ToString("yyyy-MM-dd");

        // sql21 = " Select * From Syncro_4A5_Detail_plant Where Document_id in  (Select distinct Document_id From Syncro_4A3_Main Where document_time like '" + DownDVDay + "'+'%') and Substring([Foxconn_Site],11,2) = '" + tmpFoxconnSite + "' and Substring([Forecast_QtyTypeCode],1,8) = 'Discrete' ";

        subtmpstr1 = " Select D.*,C.Customer Customer,C.Plant_Name Customer_Site From Syncro_4A5_Detail_plant D, Syncro_4A3_Partner P, Customer_Plant C Where D.Document_ID = P.Document_ID And P.Forecast_PartnerGBI=C.Plant_Code And D.Document_id in (Select distinct Document_id From Syncro_4A3_Main Where document_time like '" + DownDVDay + "'+'%') and Substring([Forecast_QtyTypeCode],1,8) = 'Discrete' ";
        subtmpstr1 = subtmpstr1 + subs2 + subs4;

        if (arrayParam[6, 2] != "") subtmpstr1 = subtmpstr1 + " and Forecast_CustomerPN = '" + arrayParam[6, 2] + "' "; // 料號一次
        subtmpstr1 = subtmpstr1 + " order by Customer_Site,Foxconn_Site,Forecast_CustomerPN,Forecast_BeginDate, D.Document_ID ";

    }
    

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    // arrayPart 物料主檔  Function 1 Upload File 
    // arrayCustomerPlant 客戶成品對照表
    // arrayGIT  在途量
    // arrayEtdUpload 從 Upload 進來 ETD 資料
    // arrayCustomerFoxconnPNToOneSet   依客戶+Foxconn+料號PN 為一組  
    // arrayDVByDocmID_Cust_Fox_PN    依客戶+Foxconn+料號PN 為一組 DV 讀入此表 
    // arraytransDay            建立一個一日期排列, 將 arrayDVByDocmID_Cust_Fox_PN DV 讀入並轉換
    //////////////////////////////////////////////////////////////////////////////////////////////////////
    //protected void btnUpload3_Click(object sender, EventArgs e)
    //{
    //    ProcETDToETA();
    //}

    //public void PreSelfloopProcETDToETA( string p11, string p22, string p33, string p44, string p55, string p66, string p77 )
    //{
    //    int loccnt = 0;
    //
    //    try
    //    {
    //        ProcETDToETA();
    //    }
    //    catch (Exception ex)
    //    {
    //        // 20121031 Response.Write(ex);
    //    }
    //    finally
    //    {
    //        Programstatus = Programstatus; //Close Connection
    //    }
    //
    //    lblMsg = "Since no data coming, System Waiting 10 Minute" + loccnt.ToString() + " times";
    //    loccnt++;
    //    // Thread.Sleep(600000);	 // 10 Min 6000 000---休眠2秒鐘 2000
    // }

    static string DBKind = "", DBType = "", DBRead = "", DBWri = ""; 
    public void ProcETDToETA( string p11, string p22, string p33, string p44, string p55, string p66, string p77)
    {
        DBKind = p11;
        DBType = p22.ToLower();
        DBRead = p33;
        DBWri  = p44;
        Programflag = Convert.ToChar( p66.Substring(0,1) );  // Programflag = '1'; ETDToETA
        string subtmpstr1 = "";
        string subtmpstr2 = "";
        string subtmpstr3 = "";
        string subtmpstr4 = "";
        string s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12;
        int localvar1 = 0;
        int localvar2 = 0;
        int localvar3 = 0;
        DateTime BeginGITDate = DateTime.Today;
        text1 = DateTime.Today.ToString("yyyy-MM-dd");
        int v1 = 0;

        // Programflag = '1';
        tmpReleaseYear = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        tmpReleaseDate = tmptoday.ToString("yyyyMMdd");

        subtmpstr2 = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        subtmpstr3 = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        localvar1 = Convert.ToInt32(tmptoday.DayOfYear);
        localvar2 = Convert.ToInt32(tmptoday.DayOfWeek);
        // ShipPlanlib ff = new ShipPlanlib();
        subtmpstr1 = ShipPlanlibPointer.getWeekofthisYear(1, localvar1, localvar2, subtmpstr2, tmptoday); // Para 1 , 2 for system 
        tmpSPWeek = subtmpstr1;
        int Reti = 0;

        // string[,] arrayCustomerFoxconnPNToOneSet = new string[arrayCustomerFoxconnPNToOneSetLong, 100 + 1];
        // var3 = arrayCustomerFoxconnPNToOneSetLong;
        //
        // for (var4 = 0; var4 < var3; var4++)
        // {
        //    arrayCustomerFoxconnPNToOneSet[var4, 0] = "";
        //    arrayCustomerFoxconnPNToOneSet[var4, 1] = "";
        //    arrayCustomerFoxconnPNToOneSet[var4, 2] = "";
        //    arrayCustomerFoxconnPNToOneSet[var4, 3] = "";
        //    arrayCustomerFoxconnPNToOneSet[var4, 4] = "";
        //    arrayCustomerFoxconnPNToOneSet[var4, 5] = (1000 + var4).ToString(); // 為 Key當Index 到 array 13 Upload
        // }   // End InitializeCulture space

        string[,] arrayCustomerFoxconnPNToOneSet = new string[arrayCustomerFoxconnPNToOneSetLong, arrayCustomerFoxconnPNToOneSetIndex + 1];
        var3 = arrayCustomerFoxconnPNToOneSetLong;

        for (var4 = 0; var4 < var3; var4++)
        {
            for (var5 = 0; var5 < arrayCustomerFoxconnPNToOneSetIndex + 1; var5++) arrayCustomerFoxconnPNToOneSet[var4, var5] = "";
            arrayCustomerFoxconnPNToOneSet[var4, 5] = (10000 + var4).ToString();     // 為 Key當Index 到 array 13 Upload
            arrayCustomerFoxconnPNToOneSet[var4, 6] = tmptoday.ToString("yyyyMMdd"); // DV 最早一天
            arrayCustomerFoxconnPNToOneSet[var4, 7] = tmptoday.ToString("yyyyMMdd"); // DV 最晚一天
            arrayCustomerFoxconnPNToOneSet[var4, IndexArrayLoc] = "11";                         // loc 10 記錄下一位, 11放實際 DTDUpload Loc 
        }   // End InitializeCulture space

        sql1 = "select * from Syncro_Foxconn_Nokia_partNo";  // table error need update
        // 20121031 DataSet ds1 = DataConnlib.Get_InfoByPara(sql1);
        DataSet ds1 = DataBaseOperation.SelectSQLDS( DBType, DBRead, sql1);
        if ( ds1.Tables.Count > 0) PartUploadETDrecordLong = ds1.Tables[0].Rows.Count;
        if (PartUploadETDrecordLong == 0)
        {  //Response.Write("<script>alert('select from Syncro_Foxconn_Nokia_partNo失敗，請檢查後重試！')</script>");
            ErrMsg = " It is can find any data in Syncro_Foxconn_Nokia_partNo, Call operator";
            return;
        }

        string[,] arrayPart = new string[PartUploadETDrecordLong + 1, 20 + 1];
        GetDataInArrarPart(ref arrayPart, ref ds1); // Put DBA data into arrarpart
        lblMsg = "Test GSCMD & Syncro Proc ";

        ////////////////////////////////////////////////////////////////////////////////
        // Get_InfoByPara  start  F5 Trace Key  20100120 Ken Beijing
        // According to Paramater Sql to Get data base 
        // Get Data from Users and show all infomation
        // ds1 : Syncro_Foxconn_Nokia_partNo into arrayPart, PartUploadETDrecordLong
        // ds2 : 
        // DateTime dt = new DateTime(DateTime.Now.Year, 1, 20);
        // int firstday  = Convert.ToInt32(dt.DayOfWeek);
        // DateTime dt   = DateTime.Today;
        // DateTime dt1  = DateTime.Today;
        // int firstday  = Convert.ToInt32(dt.DayOfWeek);
        // int firstday1 = Convert.ToInt32(dt.Year);
        // dt = dt1.AddDays(330);
        ////////////////////////////////////////////////////////////////////////////////
        // 找 Customer_Plant 
        ////////////////////////////////////////////////////////////////////////////////
        // Customer, Plant_Code, Plant_Name
        tmpCustomerSite = "Nokia";
        sql5 = "select * from Customer_Plant where Customer = '" + tmpCustomerSite + "'  ";  // table error need update
        //DataSet ds5 = DataConnlib.Get_InfoByPara(sql5);
        //CustomerPlantLong = ds5.Tables[0].Rows.Count;  // Real how many record in this table
        DataSet ds5 = DataBaseOperation.SelectSQLDS(DBType, DBRead, sql5);
        if (ds5.Tables.Count > 0) CustomerPlantLong = ds5.Tables[0].Rows.Count;        
        if (CustomerPlantLong == 0)
        {  // Response.Write("<script>alert('select from Customer_Plant失敗，請檢查後重試！')</script>");
            ErrMsg = " It can not find any data in  Customer_Plant, Call operator";
        }

        var1 = CustomerPlantLong;
        var2 = 1;

        string[,] arrayCustomerPlant = new string[var1 + 1, 4 + 1];
        GetDataInArrarPartCustomerPlant(ref arrayCustomerPlant, ref ds5); // Put DBA data into arrarpart
        ////////////////////////////////////////////////////////////////////////////////
        // 找 GIT_Info to  arrayGIT 
        ////////////////////////////////////////////////////////////////////////////////
        // NokiaSite+FoxconnSite+CustomerPN+Delivery_Number+Document_ID
        // 依每筆出貨明細對帳, 意思當出貨時一天可能好幾次, 一次可能好幾個或同料號,
        // 1. 而我們依出貨單(應為訂單) 為一單位取最 Nokia+FoxconnSite+Delivery_Number
        // 2. 依出貨單為同一批次, 但可能出好幾次, 故取最後一次記錄為準 DocumentID

        subtmpstr1 = (tmptoday.AddDays(-5)).ToString("yyyyMMdd");
        localvar1 = Convert.ToInt32(subtmpstr1);
        // 20100510    sql4 = "select * from GIT_Info  where [Devivery_Date] <= '" + subtmpstr1 + "' order by  [Nokia_DUNS],[Foxconn_Site],[CUS_Materilano],[Document_ID],[Delivery_Date] ";  // table error need update
        // 20100512 sql4 = "select * from GIT_Info where Document_ID in ( SELECT max( Document_ID ) FROM GIT_Info group by Nokia_DUNS, Foxconn_Site, CUS_Materilano ) order by Nokia_DUNS, Foxconn_Site, CUS_Materilano, Delivery_Date ";  // 20100511

        subtmpstr4 = ShipPlanlibPointer.TrsstringToDateTime(DownDVDay);
        BeginGITDate = Convert.ToDateTime(subtmpstr4);
        BeginGITDate = BeginGITDate.AddDays(-45);
        subtmpstr4 = BeginGITDate.ToString("yyyyMMdd");
        sql4 = "select * from GIT_Info where Delivery_Date > '" + subtmpstr4 + "'order by Nokia_DUNS, Foxconn_Site, CUS_Materilano, Document_ID desc";  // 20100511
        // sql4 = "select * from GIT_Info order by Nokia_DUNS, Foxconn_Site, CUS_Materilano, Document_ID desc";  // 20100511
        // 20121031 DataSet ds4 = DataConnlib.Get_InfoByPara(sql4);
        // arrayGITLong = ds4.Tables[0].Rows.Count;
        DataSet ds4 = DataBaseOperation.SelectSQLDS(DBType, DBRead, sql4);
        if (ds4.Tables.Count > 0) arrayGITLong = ds4.Tables[0].Rows.Count;
        if (arrayGITLong == 0)
        {  //Response.Write("<script>alert('GIT_Info 無資料，請檢查後重試！')</script>");
            ErrMsg = "It can not find any data in GIT_Info, Call Operator";
        }
        // if (arrayGITLong > 5000) Response.Write("<script>alert('select from GIT_Info 資料須加大，請檢查後重試！')</script>");
        arrayGITWriLong = 4000;  // 
        string[,] arrayGIT = new string[arrayGITWriLong + 1, 15 + 1];  // 暫時給 5000 應夠的
        // string[,] arrayGIT = new string[arrayGITLong + 1, 15 + 1];  // 暫時給 5000 應夠的

        GetDataInArrarPartGIT(ref arrayCustomerPlant, ref arrayGIT, ref ds4); // Put DBA data into arrarpart
        // mergeGIT(ref arrayGITLong, ref arrayGIT);  // 20100312 20100511
        //        sql9 = " Select  T3.Forecast_PartnerGBI , Plant_name, rol.to_GBI ,t4.foxconnsite, T2.Forecast_CustomerPN,max(T1.Document_ID) as Document_ID "
        //        + " From Syncro_4A3_Main T1, Syncro_4A5_Detail_Plant T2, Syncro_4A3_Partner T3, Customer_Plant Duns, "
        //        + " Syncro_Foxconn_Nokia_PartNo T4, Syncro_4A3_Role rol where T1.Document_ID = T2.Document_ID "
        //        + " and T2.Document_ID = T3.Document_ID and Duns.Plant_Code = T3.Forecast_PartnerGBI "
        //        + " and T4.NokiaPartNo = T2.Forecast_CustomerPN and T4.NokiaSite =  Duns.Plant_Name "
        //        + " and T1.Document_ID = rol.Document_ID and  rol.to_GBI = T4.FoxconnRegion "
        //        + " group by  T3.Forecast_PartnerGBI,Duns.Plant_name , rol.to_GBI , T2.Forecast_CustomerPN ,t4.foxconnsite "
        //        + " order by plant_name, foxconnsite, Forecast_CustomerPN, Document_ID ";

        sql9 = "Select  T3.Forecast_PartnerGBI ,plant_name, rol.to_GBI ,t4.foxconnsite, T2.Forecast_CustomerPN,max(T1.Document_ID) as Document_ID "
             + "From						"
             + "Syncro_4A3_Main T1,				"
             + "Syncro_4A3_Status_Flag_Plant  T2,		"
             + "Syncro_4A3_Partner T3,			"
             + "Customer_Plant Duns,				"
             + "Syncro_Foxconn_Nokia_PartNo T4,		"
             + "Syncro_4A3_Role rol				"
             + "where					"
             + "T1.Document_ID = T2.Document_ID		"
             + "and T2.Document_ID = T3.Document_ID		"
             + "and Duns.Plant_Code = T3.Forecast_PartnerGBI "
             + "and T4.NokiaPartNo = T2.Forecast_CustomerPN	"
             + "and T4.NokiaSite =  Duns.Plant_Name 		"
             + "and T1.Document_ID = rol.Document_ID		"
             + "and  rol.to_GBI = T4.FoxconnRegion		"
             + "group by  T3.Forecast_PartnerGBI,Duns.plant_name , rol.to_GBI , T2.Forecast_CustomerPN ,t4.foxconnsite "
             + "order by plant_name, foxconnsite, Forecast_CustomerPN, Document_ID ";

        //DataSet ds6 = DataConnlib.Get_InfoByPara(sql9);
        //arrayNokiaEVDocmLong = ds6.Tables[0].Rows.Count;
        DataSet ds6 = DataBaseOperation.SelectSQLDS(DBType, DBRead, sql9);
        if (ds6.Tables.Count > 0) arrayNokiaEVDocmLong = ds6.Tables[0].Rows.Count;        
        if (arrayNokiaEVDocmLong == 0)
        { //Response.Write("<script>alert('GIT_Info 無資料，請檢查後重試！')</script>");
            ErrMsg = " It can not find Nokia Max EV Document_ID";
        }
        string[,] arrayNokiaEVDocm = new string[arrayNokiaEVDocmLong + 1, 10 + 1];
        GetDataInArrarNokiaEVDocm(ref arrayNokiaEVDocm, ref arrayNokiaEVDocmLong, ref ds6);

        if (ErrMsg != "") ErrMsg = " You need to check what issue in program which get data before running";

        sq20 = "Select  T3.Forecast_PartnerGBI ,plant_name, rol.to_GBI ,t4.foxconnsite, T2.Forecast_CustomerPN,max(T1.Document_ID) as Document_ID "
             + "From Syncro_4A1_Main T1, "
             + "Syncro_4A1_Status_Flag_Plant  T2, "
             + "Syncro_4A1_Partner T3, "
             + "Customer_Plant Duns,  "
             + "Syncro_Foxconn_Nokia_PartNo T4, "
             + "Syncro_4A1_Role rol "
             + "where T1.Document_ID = T2.Document_ID "
             + "and   T2.Document_ID = T3.Document_ID "
             + "and Duns.Plant_Code  = T3.Forecast_PartnerGBI "
             + "and T4.NokiaPartNo   = T2.Forecast_CustomerPN "
             + "and T4.NokiaSite     =  Duns.Plant_Name "
             + "and T1.Document_ID   = rol.Document_ID "
             + "and  rol.to_GBI = T4.FoxconnRegion "
             + "group by  T3.Forecast_PartnerGBI,Duns.plant_name , rol.to_GBI , T2.Forecast_CustomerPN ,t4.foxconnsite "
             + "order by plant_name, foxconnsite, Forecast_CustomerPN, Document_ID ";

        //DataSet ds7 = DataConnlib.Get_InfoByPara(sq20);
        //arrayNokiaPVDocmLong = ds7.Tables[0].Rows.Count;
        DataSet ds7 = DataBaseOperation.SelectSQLDS(DBType, DBRead, sq20);
        if (ds7.Tables.Count > 0) arrayNokiaPVDocmLong = ds7.Tables[0].Rows.Count;
        if (arrayNokiaPVDocmLong == 0)
        { //Response.Write("<script>alert('GIT_Info 無資料，請檢查後重試！')</script>");
            ErrMsg = " It can not find Nokia Max PV Document_ID";
        }
        string[,] arrayNokiaPVDocm = new string[arrayNokiaPVDocmLong + 1, 10 + 1];
        GetDataInArrarNokiaPVDocm(ref arrayNokiaPVDocm, ref arrayNokiaPVDocmLong, ref ds7);

        if (ErrMsg != "") ErrMsg = " You need to check what issue in program which get data before running";



        lblMsg = "Start Test ";
        GITUpdateNumber = 0;
        tmpDocumentID = tmptoday.ToString("yyyyMMdd");

        sql2 = "select * from ReqProcETDToETA where FinishedDate = '' or FinishedDate = null order by DocumentID";
        //DataSet ds2 = DataConnlib.Get_InfoByPara(sql2);
        //tmpReqProcETDToETAVar = ds2.Tables[0].Rows.Count;
        DataSet ds2 = DataBaseOperation.SelectSQLDS(DBType, DBRead, sql2);
        if (ds2.Tables.Count > 0) tmpReqProcETDToETAVar = ds2.Tables[0].Rows.Count;
        if (tmpReqProcETDToETAVar == 0)  // No data requirement 
        {
            // Response.Write("<script>alert('ReqProcETDToETA 無上傳資料，按任意鍵, 正常返回！')</script>");
            return;
        }

        for (tmpReqProcETDToETAVar = 0; tmpReqProcETDToETAVar < ds2.Tables[0].Rows.Count; tmpReqProcETDToETAVar++)
        {
            tmpDocumentID = ds2.Tables[0].Rows[tmpReqProcETDToETAVar]["DocumentID"].ToString();
            if (tmpDocumentID == "") tmpDocumentID = tmptoday.ToString("yyyyMMdd");
            //  tmpUploadType = ds2.Tables[0].Rows[tmpReqProcETDToETAVar]["UploadType"].ToString(); tmpUploadType = "EVPV";

            PreDownLoadShippingPlanLoopReqProcETDToETA(ref arrayPart, ref arrayGIT, ref arrayCustomerFoxconnPNToOneSet, ref arrayNokiaEVDocm, ref arrayNokiaPVDocm);
            subtmpstr3 = "End UploadETDToETA Prog1 筆數:" + UploadETDrecordLong.ToString() + " 重覆筆數:" + DelpcateEVNum.ToString() + " 機種:" + eachIDSetCount.ToString();

            if (UpdateReqprocflag == 'Y') // Update Syncro_ReqprocETDToETA FinishedDate=Today 
            {
                Reti = 0;
                subtmpstr1 = tmptoday.ToString("yyyyMMdd");
                sql7 = "UPDATE ReqProcETDToETA SET FinishedDate = '" + subtmpstr1 + "', RunStatus = '" + subtmpstr3 + "' WHERE DocumentID = '" + tmpDocumentID + "' ";  // table error need update
                Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, sql7); // 76
                if (Reti == 0)
                {
                    ErrMsg = "UPDATE ReqProcETDToETA Fail";
                }      
                
                //if (!DataConnlib.Excute(sql7))
                //{
                //    // Response.Write("<script>alert('Update ReqProc 失敗，請稍后重試！')</script>");
                //    ErrMsg = "Update ReqProc fail, Call Programmer";
                //}

            }

        }

        // DelpcateEVNum, UploadETDrecordLong, eachIDSetCount
        lblMsg = "End UploadETDToETA Prog1 筆數:" + UploadETDrecordLong.ToString() + " 重覆筆數:" + DelpcateEVNum.ToString() + " 機種:" + eachIDSetCount.ToString();

        if (ErrMsg != "") lblMsg = ErrMsg;

        if (Programtype == "2")
            Thread.Sleep(10000);	 // 10 Min 6000 000---休眠2秒鐘 2000

        return; // Not trace temp 20100406
        // all data rewrite and for trace rewrite GIT 
        for (localvar1 = 1; localvar1 < arrayGITLong + 1; localvar1++) // trace Only
        {
            s1 = arrayGIT[localvar1, 1];  // ID
            s2 = arrayGIT[localvar1, 2];  // Nokia DSun
            s3 = arrayGIT[localvar1, 3];  // FoxconnSite
            s4 = arrayGIT[localvar1, 4];  // PN
            s5 = arrayGIT[localvar1, 5];  // Delivery_Qty
            s6 = arrayGIT[localvar1, 6];  // Delivery_Date
            s7 = arrayGIT[localvar1, 7];  // Customer
            s11 = arrayGIT[localvar1, 11]; // delele falg
            s0 = fillCharToSameLocZero(localvar1, 5); // seqno 2,  2,3,4,6,1  
            s12 = arrayGIT[localvar1, 12];  // Delivery_Qty backup   

            // [Nokia_DUNS],[Foxconn_Site],[CUS_Materilano],[Delivery_Date],[Document_ID] ";  // table error need update
            Reti = 0;
            subtmpstr1 = "Insert into  Test_Record ( Proc1, D1, R1, R2, R3, R4, R5, R6, R7) Values ( '" + s7 + "', '" + s3 + "', '" + s4 + "', '" + s1 + "', '" + s6 + "', '" + s5 + "', '" + s12 + "', '" + s11 + "', '" + s0 + "' ) ";
            v1 = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, subtmpstr1); // 76
            if (v1 == 0)
            {
                ErrMsg = "Insert EV PreETA Fail";
            }          
            //if (!DataConnlib.Excute(subtmpstr1))
            //{
            //    // Response.Write("<script>alert('Test_record 新增失敗，請稍后重試！')</script>"); //
            //    ErrMsg = "Test_record 新增失敗，請稍后重試";
            //}
        }  // Rewrite GIT to trace table 

        lblMsg = "End Test UploadETDToETA Prog1 ";
        return;


    }  // ebd proc

    //////////////////////////////// DownLoad ShippingPlan Only
    private void PreDownLoadShippingPlanLoopReqProcETDToETA(ref string[,] arrayPart, ref string[,] arrayGIT, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayNokiaEVDocm, ref string[,] arrayNokiaPVDocm) // 
    {
        int localvar3 = 0;
        string localstr2 = "";
        string localstr3 = "";
        string localstr4 = "";

        DownDVDay = text1.Substring(0, 4) + text1.Substring(5, 2) + text1.Substring(8, 2); // text1 = DateTime.Today.ToString("yyyy-MM-dd");

        sql3 = "select * from ETD_SP_Upload where  DocumentID = '" + tmpDocumentID + "' order by CustomerSite, FoxconnSite, CustomerPN, SPDate";
        //DataSet ds3 = DataConnlib.Get_InfoByPara(sql3);
        //UploadETDrecordLong = ds3.Tables[0].Rows.Count;
        DataSet ds3 = DataBaseOperation.SelectSQLDS(DBType, DBRead, sql3);
        if (ds3.Tables.Count > 0) UploadETDrecordLong = ds3.Tables[0].Rows.Count;
        if (UploadETDrecordLong == 0)
        {
            // Response.Write("<script>alert('select from ETD_SP_Upload失敗，請檢查後重試！')</script>"); //
            ErrMsg = "There are No Data in ETD_SP_Upload File, You can check later";
            return;
        }

        var3 = UploadETDrecordLong;
        var4 = 20 + 1;
        string[,] arrayEtdUpload = new string[UploadETDrecordLong + 1, var4];


        for (var1 = 0; var1 < UploadETDrecordLong + 1; var1++)
            arrayEtdUpload[var1, 1] = "";

        eachIDDVno = 0;               // 每個 Docm 中 DV 數量和
        eachIDSetCount = 0;           // 每個 Docm 中 Cus+Fox+CustPN 組別數 
        eachIDSetDVno = 0;           // 每個 Docm 中 Cus+Fox+CustPN 組別數中 DV 數量和
        eachIDDVduplicateno = 0;      // 每個 Docm 中 DV duplicate 數量和
        loopforeachSetcount = 0;

        var3 = arrayCustomerFoxconnPNToOneSetLong;
        for (var4 = 0; var4 < var3; var4++)
        {
            for (var5 = 0; var5 < arrayCustomerFoxconnPNToOneSetIndex + 1; var5++) arrayCustomerFoxconnPNToOneSet[var4, var5] = "";
            arrayCustomerFoxconnPNToOneSet[var4, 5] = (10000 + var4).ToString();     // 為 Key當Index 到 array 13 Upload
            arrayCustomerFoxconnPNToOneSet[var4, 6] = tmptoday.ToString("yyyyMMdd"); // DV 最早一天
            arrayCustomerFoxconnPNToOneSet[var4, 7] = tmptoday.ToString("yyyyMMdd"); // DV 最晚一天
            arrayCustomerFoxconnPNToOneSet[var4, IndexArrayLoc] = "11";                         // loc 10 記錄下一位, 11放實際 DTDUpload Loc 
        }   // End InitializeCulture space

        string[] DuplicateDBLog = new string[DuplicateDBLong + 1];

        for (var1 = 0; var1 < ds3.Tables[0].Rows.Count; var1++)
        {
            text4 = var1.ToString();
            arrayEtdUpload[var1 + 1, 1] = ds3.Tables[0].Rows[var1]["CustomerSite"].ToString();
            arrayEtdUpload[var1 + 1, 2] = ds3.Tables[0].Rows[var1]["FoxconnSite"].ToString();
            arrayEtdUpload[var1 + 1, 3] = ds3.Tables[0].Rows[var1]["CustomerPN"].ToString();
            arrayEtdUpload[var1 + 1, 4] = ds3.Tables[0].Rows[var1]["SPDate"].ToString();
            arrayEtdUpload[var1 + 1, 5] = ds3.Tables[0].Rows[var1]["SPQty"].ToString();
            arrayEtdUpload[var1 + 1, 6] = ds3.Tables[0].Rows[var1]["ReleaseDate"].ToString();
            arrayEtdUpload[var1 + 1, 7] = ds3.Tables[0].Rows[var1]["SPDate"].ToString();
            arrayEtdUpload[var1 + 1, 8] = ds3.Tables[0].Rows[var1]["Forecast_IntervalCode"].ToString();
            arrayEtdUpload[var1 + 1, 9] = Convert.ToDateTime(ds3.Tables[0].Rows[var1]["SPDate"].ToString()).ToString("yyyyMMdd");
            arrayEtdUpload[var1 + 1, 10] = "";
            arrayEtdUpload[var1 + 1, 11] = ""; // for program dele flag 20100404 
            arrayEtdUpload[var1 + 1, 12] = "";
            arrayEtdUpload[var1 + 1, 13] = ""; // For Sort Index

            arrayEtdUpload[var1 + 1, 5] = ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); // make sure number
            if (arrayEtdUpload[var1 + 1, 6].ToString() == "") arrayEtdUpload[var1 + 1, 6] = tmptoday.ToString("yyyyMMdd"); // ReleaseDate

            if ((arrayEtdUpload[var1, 1] == arrayEtdUpload[var1 + 1, 1]) && (arrayEtdUpload[var1, 2] == arrayEtdUpload[var1 + 1, 2]) && (arrayEtdUpload[var1, 3] == arrayEtdUpload[var1 + 1, 3]) && (arrayEtdUpload[var1, 4] == arrayEtdUpload[var1 + 1, 4]))
            {
                arrayEtdUpload[var1, 10] = "D";  // 重覆 EV Delete  
                arrayEtdUpload[var1, 5] = "0";   // clear "Forecast_Qty"
                DelpcateEVNum++;
            }

            localstr2 = arrayEtdUpload[var1 + 1, 9].ToString();
            // DV 過到今天為 0 20100525
            if (Convert.ToInt32(arrayEtdUpload[var1 + 1, 9].ToString()) < Convert.ToInt32(DownDVDay))
                arrayEtdUpload[var1 + 1, 5] = "0";   // clear "Forecast_Qty"




            localvar3 = var1 + 1;  // 目前位置
            localstr3 = (Convert.ToDateTime(arrayEtdUpload[var1 + 1, 4])).ToString("yyyyMMdd"); // SPdate 取出DV 算最小 DV 
            // SetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref localvar3, ref localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload); 
            // 20121103 localstr4 = ShipPlanlibPointer.SeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref localvar3, ref localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, ref DuplicateDBLog, ref DuplicateDBFLoc, ref IndexArrayLoc, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSetIndex, ref arrayCustomerFoxconnPNToOneSetLong, ref DuplicateDBLong);
            localstr4 = LibSCM1Pointer.SeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSetClass1(ref localvar3, ref localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, ref DuplicateDBLog, ref DuplicateDBFLoc, ref IndexArrayLoc, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSetIndex, ref arrayCustomerFoxconnPNToOneSetLong, ref DuplicateDBLong);
            if (localstr4 != "1")
            {
                // Response.Write("<script>alert('arrayCustomerFoxconnPNToOneSet overlap 失敗，請稍后重試！')</script>");
                ErrMsg = "arrayCustomerFoxconnPNToOneSet overlap, Call porgrammer";
                return;
            }
        }

        LoopReqProcETDToETA(ref arrayPart, ref arrayGIT, ref arrayEtdUpload, ref arrayCustomerFoxconnPNToOneSet, ref arrayNokiaEVDocm, ref arrayNokiaPVDocm);

    }   // end PreDownLoadShippingPlanLoopReqProcETDToETA



    ///////////////////////////////////////////////////////////////////////////////////
    /// 共用開始 Loop for each Reqire Process  ////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////ref tmpprebuff, ref tmpaftbuff

    private void LoopReqProcETDToETA(ref string[,] arrayPart, ref string[,] arrayGIT, ref string[,] arrayEtdUpload, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayNokiaEVDocm, ref string[,] arrayNokiaPVDocm) // 
    {
        string tmpstring1 = "";
        DateTime tmpdate1, tmpdate2, tmpdate3;
        tmpdate1 = DateTime.Today;
        tmpdate2 = DateTime.Now;
        tmpdate3 = DateTime.Now;

        int localvar1 = 0;
        int localvar2 = 0;
        int localvar3 = 0;
        int localvar4 = 0;
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
            for (var2 = 0; var2 < arrayShipPlanYLong; var2++)
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

        /////////////////////////////////////////////////////////////////////////////////////////////
        // 依 CustomerSide+FoxconnSide+CustomerPN 為 Key 讀入此Table 
        // Algorithm : if A==b and C==D then break
        //             if A<>B or C<>D then next
        //             if A=spce then Write and break 
        // 1. CustomerSide  ................... (600) 
        // 2. FoxconnSide   |...................
        // 3. CustomerPN    |
        // 4. Recorslong    |
        // 5. IndexKey      | 
        //                  (3)
        // 依 CustomerSide + FoxconnSide 先分出
        /////////////////////////////////////////////////
        /////////////////////////////////////////////////
        // Do this loop1 to end write , 
        //
        // string[,] arraytransDay = new string[arraytransDayfixLong400, 20+1]; // Initial transfer array
        string[,] arraytransDay = new string[arraytransDayfixLong400, arraytransDayY35 + 1]; // Initial transfer array 20100319

        int loop1var1 = 0;
        int loop1var2 = 0;
        int loop1var3 = 0;
        int loop1var4 = 0;

        MainloopReadHead = IndexArrayLoc + 1;
        MainloopReadTail = IndexArrayLoc + 1;


        for (loopforeachSetcount = 1; loopforeachSetcount < eachIDSetCount + 1; loopforeachSetcount++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA
        {

            tmpCustomerSite = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 1];
            tmpFoxconnSite = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 2];
            tmpCustomerPN = arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 3];
            MainloopReadHead = 11;
            MainloopReadTail = 11;

            if ((tmpCustomerSite == "") || (tmpFoxconnSite == "") || (tmpCustomerPN == ""))
                lblMsg = "ErrortmpCustomerSite = Space ";

            //////////////////////////////////////////////////////////////////////////////////
            //  1. Seek for  Max and Min Account
            ///////////////////////////////////////////////////////////////////////////////////
            // 1. 從本次 DocumentID 中找出最大日期及最小日期, 目前日期, 用日期比較大小
            // 2. 最小日期放在第 101 矩陣位置
            // 3. 前推 100 位置為須算 GIT 數量
            //             MaxStr    = Convert.ToDateTime(arrayEtdUpload[1, 4]); 20100218 Update


            // MinStr = Convert.ToDateTime(arrayDVByDocmID_Cust_Fox_PN[1, 4]);
            // MaxStr = Convert.ToDateTime(arrayDVByDocmID_Cust_Fox_PN[1, 4]);
            // tmpMaxMin = Convert.ToDateTime(arrayDVByDocmID_Cust_Fox_PN[1, 4]);
            // 
            // int MaxMimDVLong = 0;             // DV Max - Min = Read Distance day      
            //
            // for (var1 = 1; var1 < arrayDVByDocmID_Cust_Fox_PNLong + 1; var1++)
            // {
            //    if (arrayDVByDocmID_Cust_Fox_PN[var1, 4] != "")
            //    {
            //        tmpMaxMin = Convert.ToDateTime(arrayDVByDocmID_Cust_Fox_PN[var1, 4]);
            //        if (MinStr > tmpMaxMin) MinStr = tmpMaxMin;
            //        if (MaxStr < tmpMaxMin) MaxStr = tmpMaxMin;
            //    }
            // }

            MinStr = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 6]));
            MaxStr = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 7]));

            text2 = MaxStr.Subtract(MinStr).ToString();
            var1 = text2.IndexOf(".");  // Seek first 151.1 point location

            if (var1 <= 0) MaxMimDVLong = 1;
            else MaxMimDVLong = Convert.ToInt32(text2.Substring(0, var1)); // DV (最大 - 最小) 天數    

            text2 = tmptoday.Subtract(MinStr).ToString();
            var1 = text2.IndexOf(".");  // Seek first 151.1 point location

            if (var1 <= 0) var2 = 0;
            else var2 = Convert.ToInt32(text2.Substring(0, var1)); // 得到今天在第幾個 Array location 未加 100
            var1 = BefreDVLoc100 + 1 + var2;  // 起始 100+1(DV 最早日) + 今天實際位置 114
            tmptodaylocation = var1; // 從最早 DV 為100 , Offset 偏移到今天 100+1+OffSet

            WriteDBFBaseLongLoc = WriteDBFBaseLong + tmptodaylocation; // 今天起 18+1 周 最後位置
            if (WriteDBFBaseLongLoc >= 400)
            {
                // Response.Write("<script>alert(' 超過矩陣 400 資料失敗，請通資訊人員！')</script>");
                ErrMsg = "array over 400 , Programm eerror or data over 300 days";
                return;
            }

            // text2 = DateTimePicker.ToStr
            // MaxMimDVLong = Convert.ToInt32(text2.Substring(0, var1)); // DV (最大 - 最小) 天數       
            // text2 = DateTimePicker.ToString();
            // tmpdate1 = Convert.ToDateTime(arrayEtdUpload[1, 4]);
            // tmpdate2 = Convert.ToDateTime(arrayEtdUpload[1000, 4]);         
            // string aa = DateTime.Now.AddDays(10).ToString();
            // tmpdate1 = tmpdate2.AddDays(10);
            // var1 = 0;
            //
            // if (tmpdate1 >= tmpdate2)
            // {
            //    text2 = tmpdate1.Subtract(tmpdate2).ToString();
            // }
            // else
            //    text2 = tmpdate2.Subtract(tmpdate1).ToString();              


            /////////////////////////////////////////////////////////////////////////////////
            // 找到此料號 Master Data 中 LeadTime 
            /////////////////////////////////////////////////////////////////////////////////
            tmpExeceptionLog = "01 The UpLoad Part not in dbo.Syncro_Foxconn_Nokia_PartNo";
            for (var1 = 1; var1 < PartUploadETDrecordLong + 1; var1++)  // load in memory
            {
                if ((arrayPart[var1, 1].ToString() == tmpCustomerSite) && (arrayPart[var1, 2].ToString() == tmpFoxconnSite) && (arrayPart[var1, 3].ToString() == tmpCustomerPN))
                {
                    // arrayPart[var1, 1] = ds1.Tables[0].Rows[var1 - 1]["NokiaSite"].ToString();
                    // arrayPart[var1, 2] = ds1.Tables[0].Rows[var1 - 1]["FoxconnSite"].ToString();
                    // arrayPart[var1, 3] = ds1.Tables[0].Rows[var1 - 1]["NokiaPartNo"].ToString();
                    tmpCurrent_Dos = arrayPart[var1, 4].ToString();   // ds1.Tables[0].Rows[var1]["Current_Dos"].ToString();
                    tmpNext_Dos = arrayPart[var1, 5].ToString();      // ds1.Tables[0].Rows[var1]["Next_Dos"].ToString();
                    tmpGIT_Dos = arrayPart[var1, 6].ToString();       // ds1.Tables[0].Rows[var1]["GIT_Dos"].ToString();
                    tmpMPQ = arrayPart[var1, 7].ToString();           // ds1.Tables[0].Rows[var1]["MPQ"].ToString();
                    tmpLeadTime = arrayPart[var1, 8].ToString();      // ds1.Tables[0].Rows[var1]["LeadTime"].ToString();
                    tmpFoxconnBU = arrayPart[var1, 11].ToString();    // = ds1.Tables[0].Rows[var1]["FoxconnBU"].ToString(); 
                    tmpNokiaPartNo = arrayPart[var1, 3].ToString();   // = ds1.Tables[0].Rows[var1]["NokiaPartNo"].ToString();
                    tmpFoxconnPN = arrayPart[var1, 13].ToString();    // = ds1.Tables[0].Rows[var1]["FoxconnPartNo"].ToString();  
                    tmpPNProject = arrayPart[var1, 10].ToString();    // = ds1.Tables[0].Rows[var1]["Project"].ToString();   
                    tmpDescription = arrayPart[var1, 12].ToString();  // = ds1.Tables[0].Rows[var1]["Description"].ToString(); 
                    tmpDom_Exp = arrayPart[var1, 14].ToString();      // = ds1.Tables[0].Rows[var1]["Dom_Exp"].ToString(); 
                    tmpFoxconnPlant = arrayPart[var1, 15].ToString(); // = ds1.Tables[0].Rows[var1]["FoxconnPlant"].ToString();
                    tmpHHPN = arrayPart[var1, 16].ToString(); // = ds1.Tables[0].Rows[var1]["FoxconnPartNo"].ToString();

                    if ((tmpDescription == "") || (tmpDescription == null)) tmpDescription = "No Data";
                    if ((tmpDom_Exp == "") || (tmpDom_Exp == null)) tmpDom_Exp = "No Data";

                    tmpCurrent_Dos = ShipPlanlibPointer.TrsStrToInteger(tmpCurrent_Dos); // Para 1 , 2 for system 
                    tmpNext_Dos = ShipPlanlibPointer.TrsStrToInteger(tmpNext_Dos);
                    tmpGIT_Dos = ShipPlanlibPointer.TrsStrToInteger(tmpGIT_Dos);
                    tmpCurrent_Dos = ShipPlanlibPointer.TrsStrToInteger(tmpCurrent_Dos);
                    tmpMPQ = ShipPlanlibPointer.TrsStrToInteger(tmpMPQ); // Para 1 , 2 for system 
                    tmpLeadTime = ShipPlanlibPointer.TrsStrToInteger(tmpLeadTime); // Para 1 , 2 for system      

                    if (Convert.ToInt32(tmpCurrent_Dos) > 100) tmpCurrent_Dos = "11";
                    if (Convert.ToInt32(tmpNext_Dos) > 100) tmpNext_Dos = "7";
                    if (Convert.ToInt32(tmpLeadTime) > 100) tmpLeadTime = "7";

                    localvar1 = tmpDescription.Length;  // delete '  長度 19 從 1 開始, Array 確從 0 
                    for (localvar2 = 0; localvar2 < localvar1 - 1; localvar2++)
                        if (tmpDescription.Substring(localvar2, 1) == "'")
                            tmpDescription = tmpDescription.Substring(0, localvar2) + " " + tmpDescription.Substring(localvar2 + 1, localvar1 - 1 - localvar2);

                    arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 0] = tmpPNProject;   // Project  
                    arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 7] = tmpDescription; // Description  
                    arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 8] = tmpHHPN;        // H.H P/N  
                    arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 9] = tmpDom_Exp;     // Dom_Exp  

                    var1 = PartUploadETDrecordLong + 10; // break
                    tmpExeceptionLog = "";
                }

            }


            if (tmpExeceptionLog != "")
                WritmpExeceptionLog("01", "", "ETD_SP_Upload", ref tmpDocumentID, "", ref tmpCustomerSite, ref tmpFoxconnSite, ref tmpCustomerPN, "", ref tmptoday, "");




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
            // arraytransDay[1 + BefreDVLoc100, 1] = (1 + BefreDVLoc100).ToString();
            // arraytransDay[1 + BefreDVLoc100, 2] = Convert.ToString(tmpMaxMin);
            // arraytransDay[1, 3] = Convert.ToDateTime(MinStr).ToString("yyyyMMdd");

            // change to 400 array for (var1 = 1; var1 < MaxMimDVLong + 200; var1++)  
            // ShipPlanlib mm = new ShipPlanlib();
            tmpSPWeek = ShipPlanlibPointer.getWeekofthisYear(2, localvar1, localvar2, localstr1, tmptoday); // Para 1 , 2 for system 

            arraytransDay[0, 0] = "";
            for (var1 = 1; var1 < arraytransDayfixLong400; var1++)  // 建立出始 Table
            {

                arraytransDay[var1, 1] = var1.ToString();

                if (var1 < 10)
                    arraytransDay[var1, 1] = "00" + arraytransDay[var1, 1]; // 補3位
                else
                    if (var1 < 100)
                        arraytransDay[var1, 1] = "0" + arraytransDay[var1, 1]; // 補3位

                localstr4 = arraytransDay[var1, 1];                          // trace only
                arraytransDay[var1, 2] = Convert.ToString(tmpMaxMin);
                arraytransDay[var1, 3] = tmpMaxMin.ToString("yyyyMMdd");
                arraytransDay[var1, 4] = "0";
                arraytransDay[var1, 5] = "0";
                arraytransDay[var1, 6] = "0";
                arraytransDay[var1, 7] = "0";
                arraytransDay[var1, 8] = "0";
                arraytransDay[var1, 9] = "0";
                arraytransDay[var1, 10] = (Convert.ToInt32(tmpMaxMin.DayOfWeek)).ToString();
                arraytransDay[var1, 11] = "";
                arraytransDay[var1, 12] = tmpReleaseDate;  // ReleaseDate
                arraytransDay[var1, 13] = tmpSPWeek;
                arraytransDay[var1, 14] = tmpDocumentID;
                arraytransDay[var1, 15] = "0"; // Org ETA 
                arraytransDay[var1, 16] = tmpCustomerSite;
                arraytransDay[var1, 17] = tmpFoxconnSite;
                arraytransDay[var1, 18] = tmpCustomerPN;
                arraytransDay[var1, 19] = "";
                arraytransDay[var1, 20] = "";
                arraytransDay[var1, 21] = tmpCurrent_Dos;
                arraytransDay[var1, 22] = tmpNext_Dos;
                arraytransDay[var1, 23] = tmpGIT_Dos;
                arraytransDay[var1, 24] = tmpMPQ;
                arraytransDay[var1, 25] = tmpLeadTime;
                arraytransDay[var1, 26] = "0";
                arraytransDay[var1, 27] = "0";
                arraytransDay[var1, 28] = "";
                arraytransDay[var1, 29] = "";
                arraytransDay[var1, 30] = "";
                arraytransDay[var1, 31] = "0";  // GIT_QTy  20100522
                arraytransDay[var1, 32] = "";  // GIT Document_ID
                arraytransDay[var1, 33] = "";
                arraytransDay[var1, 34] = "";
                arraytransDay[var1, 35] = "";

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

            // var1 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[var4, 10]) - 10 - 1;  // 資料量
            // 20100219 for (var1 = 1; var1 < MaxMimDVLong + 1; var1++)  // 開始從資料庫第一筆SPdate  只要所須 Array 長度即可
            // for (var1 = 11; var1 < arrayDVByDocmID_Cust_Fox_PNLong + 1; var1++)   // 開始從資料庫第一筆SPdate  只要所須 Array 長度即可

            MainloopReadHead = IndexArrayLoc;
            MainloopReadTail = IndexArrayLoc + 1;
            var1 = 11;
            while (arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail] != "")   // 從 10 開使, 地一筆 11
            {                                                                    //
                var1 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, MainloopReadTail]);
                text2 = arrayEtdUpload[var1, 4].ToString(); // ds3.Tables[0].Rows[var1]["SPDate"].ToString();
                text3 = arrayEtdUpload[var1, 5].ToString(); // ds3.Tables[0].Rows[var1]["SPQty"].ToString();
                if (text3 == null) text3 = "0";

                swdayWeek = arrayEtdUpload[var1, 8].ToString();     // ds3.Tables[0].Rows[var1]["Forecast_IntervalCode"].ToString();
                tmpSPDate = arrayEtdUpload[var1, 9].ToString();     // Convert.ToDateTime(ds3.Tables[0].Rows[var1]["SPDate"].ToString()).ToString("yyyyMMdd");    
                tmpReleaseDate = arrayEtdUpload[var1, 6].ToString();// ReleaseDate
                if ((tmpReleaseDate == "") || (tmpReleaseDate == null)) tmpReleaseDate = tmptoday.ToString("yyyyMMdd");

                loop1var2 = 1;
                if ((text3 != "0") && (arrayEtdUpload[var1, 10].ToUpper() != "D"))
                {
                    localstr1 = Convert.ToDateTime(text2).Subtract(MinStr).ToString();     // 第一筆 DV 到此筆距離
                    localvar1 = localstr1.IndexOf(".");  // Seek first 151.1 point location
                    if (localvar1 < 0) localvar2 = 0;
                    else localvar2 = Convert.ToInt32(localstr1.Substring(0, localvar1)); // 得到今天在第幾個 Array location 未加 100
                    var3 = BefreDVLoc100 + 1 + localvar2;  // 起始 100+1(DV 最早日) + 直接找兩個日期相同極為當個arraytrans位置                                                                                               // 

                    arraytransDay[var3, 12] = tmpReleaseDate;                      //
                    arraytransDay[var3, 20] = "DV"; // Flag for Real DV Coming 

                    tmp1SPDate = arraytransDay[var3, 3];                           // if Forecast_IntervalCode = "Day" 
                    if (tmp1SPDate == tmpSPDate)                                  // ("yyyyMMdd"); Put in arraytransDay
                    {                                                              // else
                        arraytransDay[var3, 12] = tmpReleaseDate; // ReleaseDate   // 本日期起 SPQty  周1 1/7, 周2 1/7, 周3 1/7,周4 1/7,周5 1/7,周6 1/7, 周7 SPQty - 6/7     

                        if (text3 != "0") text3 = text3;

                        // arraytransDay[var3, 4]  = text3; //  SPQty 數量 20100507 
                        arraytransDay[var3, 15] = text3;  // Trace Org SPQty 20100303

                        tmpConvertDoublebuf = Convert.ToDouble(text3);                 // For Cut 小數點 
                        text3 = Convert.ToInt32(tmpConvertDoublebuf).ToString();       //
                        arraytransDay[var3, 15] = text3;  // Trace Org SPQty 20100303  //
                        var5 = Convert.ToInt32(text3);    // SPQty convert number                       

                        if (swdayWeek == "Week")
                        {
                            if (var5 == 0)
                            {
                                arraytransDay[var3 + 0, 4] = "0"; // 1
                                arraytransDay[var3 + 1, 4] = "0"; // 2
                                arraytransDay[var3 + 2, 4] = "0"; // 3
                                arraytransDay[var3 + 3, 4] = "0"; // 4 
                                arraytransDay[var3 + 4, 4] = "0"; // 5 
                                arraytransDay[var3 + 5, 4] = "0"; // 6
                                arraytransDay[var3 + 6, 4] = "0"; // 7
                            }
                            else
                            {
                                var6 = var5 / 7;
                                var7 = var5 - var6 * 6;

                                // 20100304 改直接填上, 不是相加 arraytransDay[var3 + 0, 4] = (Convert.ToInt32(arraytransDay[var3 + 0, 4]) + var6).ToString(); // 1
                                // arraytransDay[var3 + 1, 4] = (Convert.ToInt32(arraytransDay[var3 + 1, 4]) + var6).ToString(); // 2
                                // arraytransDay[var3 + 2, 4] = (Convert.ToInt32(arraytransDay[var3 + 2, 4]) + var6).ToString(); // 3
                                // arraytransDay[var3 + 3, 4] = (Convert.ToInt32(arraytransDay[var3 + 3, 4]) + var6).ToString(); // 4 
                                // arraytransDay[var3 + 4, 4] = (Convert.ToInt32(arraytransDay[var3 + 4, 4]) + var6).ToString(); // 5 
                                // arraytransDay[var3 + 5, 4] = (Convert.ToInt32(arraytransDay[var3 + 5, 4]) + var6).ToString(); // 6
                                // arraytransDay[var3 + 6, 4] = (Convert.ToInt32(arraytransDay[var3 + 6, 4]) + var7).ToString(); // 7

                                arraytransDay[var3 + 0, 4] = (var6).ToString(); // 1
                                arraytransDay[var3 + 1, 4] = (var6).ToString(); // 2
                                arraytransDay[var3 + 2, 4] = (var6).ToString(); // 3
                                arraytransDay[var3 + 3, 4] = (var6).ToString(); // 4 
                                arraytransDay[var3 + 4, 4] = (var6).ToString(); // 5 
                                arraytransDay[var3 + 5, 4] = (var6).ToString(); // 6
                                arraytransDay[var3 + 6, 4] = (var7).ToString(); // 7
                            }

                            arraytransDay[var3 + 0, 12] = tmpReleaseDate;
                            arraytransDay[var3 + 1, 12] = tmpReleaseDate;
                            arraytransDay[var3 + 2, 12] = tmpReleaseDate;
                            arraytransDay[var3 + 3, 12] = tmpReleaseDate;
                            arraytransDay[var3 + 4, 12] = tmpReleaseDate;
                            arraytransDay[var3 + 5, 12] = tmpReleaseDate;
                            arraytransDay[var3 + 6, 12] = tmpReleaseDate;

                        }
                        else    // swdayWeek == "Day" or other
                        {                                                          //
                            // 20100304 改直接填上, 不是相加 arraytransDay[var3, 4] = (Convert.ToInt32(arraytransDay[var3 + 0, 4]) + var5).ToString(); // 1text3; SPQty
                            arraytransDay[var3, 4] = text3; // 1text3 SPQty 數量   
                        }
                    }   //  if (tmp1SPDate == tmpSPDate )     
                }       // if (text3 != "0") 

                MainloopReadTail++;  // next array 
            }   // end while read loop 

            /////////////////////////////////////////////////////////////////////////////////
            //  1. 將數量現日期+Leadtime 移到新日期, 找 Leadtime 往右移 SPDate+Leadtime,  並存下一格 Y 軸, y4 to y5  ? 從那理開始
            //  2. 今天前 GIT 數量加到今天, 今天後明天起, 加 LeadTime 往後移  
            ////////////////////////////////////////////////////////////////////////////////
            var3 = Convert.ToInt32(tmpLeadTime); // Offset LeadTime

            if (Programflag == '1')
            {
                for (var1 = 101; var1 < arraytransDayfixLong400 - 100; var1++) // Arr[4] offset var1 to arr[5] 因 EV第一筆在 101 開始
                    arraytransDay[var1 + Convert.ToInt32(tmpLeadTime), 5] = arraytransDay[var1, 4]; // DV+LeadTime New Location

                // 找 arrayGIT 中使用 IN_DATE 開始, 後為 Delivery_Day + LeadTime to arraytransDay
                Cal_arrayGITLongAndDeliveryQty(ref tmpLeadTime, ref arrayGITLong, ref arrayGITWriLong, ref tmpCustomerSite, ref tmpFoxconnSite, ref tmpCustomerPN, ref MaxMimDVLong, ref arrayGIT, ref arraytransDay);
                locrefstr1 = "1";
                if (WriarraytransDay == 'Y')
                {
                    GetNokiaEVDocmID(ref arrayNokiaEVDocm, ref arrayNokiaEVDocmLong, ref tmpNokiaEVDocm, ref tmpCustomerDum, ref tmpCustomerSite, ref tmpFoxconnSite, ref tmpCustomerPN);
                    //mark 20120827 modified by chen
                    GetNokiaPVDocmID(ref arrayNokiaPVDocm, ref arrayNokiaPVDocmLong, ref tmpNokiaPVDocm, ref tmpCustomerDum, ref tmpCustomerSite, ref tmpFoxconnSite, ref tmpCustomerPN);
                    // Mark 20110916 TmpWriHardarraytransDay(ref locrefstr1, ref arraytransDay); // trace only
                    TmpWriPreETA(ref locrefstr1, ref arraytransDay);
                }
            }

            // if (tmpReleaseDate == "") tmpReleaseDate = tmptoday.ToString("yyyyMMdd");
            // var2 = tmptodaylocation + 7*18 + 1;                    

            if (Programflag == '2')
            {
                Cal_CurrNextDosMPQ(ref locrefstr1, ref arraytransDay);
                locrefstr1 = loopforeachSetcount.ToString();
                if (WriarraytransDay == 'Y') TmpWriShipPlanarraytransDay(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref arrayCustomerFoxconnPNToOneSet); // trace only

                locrefstr1 = "1";
                if (WriETAarraytransDayflag == 'Y')
                    TmpWriHardarraytransDay(ref locrefstr1, ref arraytransDay); // trace only

            }

        }  // end  for (loopforeachSetcount = 1; loopforeachSetcount < eachIDSetCount + 1; loopforeachSetcount++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA

        tmpaccount++;
        loopforeachSetcount++;
        if ((Programflag == '2') && (WriDBFShipPlanFlag == 'Y')) TmpWriDBFShipPlanarraytransDay(ref locrefstr1, ref arraytransDay, ref arrayShipPlan, ref tmptodaylocation, ref WriteDBFBaseLongLoc, ref firstThuDate, ref tmpSPWeek); // trace only

        lblMsg = "End Test ";

    } // end LoopReqProcETDToETA      

    //////////////////////////////
    // 計算 Curr_Dos 20100323
    // Curr Dos = 11, Next Dos = 7
    // 第一筆 DV  3/10  
    // Curr Dos = 3/10 + 11 = 3/10 到 3/20 因含 3/10 起 11 天, 加到今天
    // Next Dos = 3/21 + 7  = 3/21 到 3/27 加總到 下周一
    // Next Dos = 3/28 + 7  = 3/28 到 4/03 加總到 下周一
    //
    // MPQ = 300          MPQ             MPQQty         欠庫存
    // Curr   C1  1000   1000-0           1200           -200
    // 下周一 D1  1500   1500-200=1300    1500           -200
    // 下周一 D2  700    700-200=500       600           -100
    // 下周一 D3  200    200-100=100       300           -200
    // 下周一 WK  1000   1000-200=800
    // 如果 WK 有減不足應往前面扣
    private void Cal_CurrNextDosMPQ(ref string locrefstr1, ref string[,] arraytransDay)  // 
    {

        string NextDosFlag = "Y";
        int tmpMPQPacket = 0;
        int tmplose = 0;
        int nextMondayoffset = 0;
        int MPQarrayloc = 0;
        //       int FirstDVLoc  = 101;
        int NextDosReadLoc = 0;
        int NextDosWriteLoc = 0;

        // if ((tmpFoxconnSite == "LH") && (tmpCustomerPN == "0254600") && (tmpCustomerSite == "Beijing"))
        //     tmpCustomerSite = tmpCustomerSite;


        NextDosFlag = "Y";
        var2 = Convert.ToInt32(arraytransDay[tmptodaylocation, 10]);     // 今天星期幾 ?
        if (var2 == 0) var3 = 1;                                        // 計算離下周一還有幾天 ?
        else var3 = 7 - var2 + 1;                                        //
        nextMondayoffset = var3; // finial offset day tMPQQty            // 離第一個下周一天數

        var2 = Convert.ToInt32(tmpCurrent_Dos);    //  = tmpCurrent_Dos; 依數目加總後面次數 arr[21]
        var3 = Convert.ToInt32(tmpNext_Dos);       //  = tmpNext_Dos; 依數目加總後面次數 arr[22]

        if ((tmpCurrent_Dos == "0") || (tmpNext_Dos == "0"))        // 如果 tmpCurrent_Dos; tmpNext_Dos 其一為 0
        {                                                           // 不做動作, 直接將數量移到 CuusNextQty[26] 
            for (var4 = 1; var4 < (arraytransDayfixLong400); var4++) arraytransDay[var4, 26] = arraytransDay[var4, 4];
            NextDosFlag = "N";
        }
        else
        {
            var6 = 0;
            var5 = 0;
            for (var4 = 0; var4 < var2; var4++)                                     // 用 DV 第一天開始(含) 
                var5 = var5 + Convert.ToInt32(arraytransDay[FirstDVLoc + var4, 4]); // 加 Curr_Dos 幾天的數量到今天
            arraytransDay[tmptodaylocation, 26] = var5.ToString();                  // Currendy Dos 到今天
            var6 = FirstDVLoc + Convert.ToInt32(tmpCurrent_Dos);                    // for next Dos 起點加 Next Dos 數次到下周一 var6 為 NextDos 起點
            NextDosReadLoc = FirstDVLoc + Convert.ToInt32(tmpCurrent_Dos);
            NextDosWriteLoc = tmptodaylocation + nextMondayoffset;
        }


        ////////////////// Next Dos start var6
        var3 = Convert.ToInt32(tmpNext_Dos);       //  = tmpNext_Dos; 依數目加總後面次數 arr[22]
        var1 = 0;

        if (NextDosFlag == "Y")
        {
            while (var1 < (arraytransDayfixLong400 - var2 - var3))            //  NextDos 從頭到尾 
            {                                                                     //  var6 為 NextDos 起點
                var7 = 0;                                                         //  var3 為 NextDos 天數
                for (var4 = 0; var4 < var3; var4++)                               //  從 Var6 起點 加 var3 次數 
                    var7 = var7 + Convert.ToInt32(arraytransDay[NextDosReadLoc + var4, 4]); //  Next Dos qty
                //  找下周一位置在 7 天內
                arraytransDay[NextDosWriteLoc, 26] = (Convert.ToInt32(arraytransDay[NextDosWriteLoc, 26]) + var7).ToString();
                NextDosReadLoc = NextDosReadLoc + Convert.ToInt32(tmpNext_Dos); // Next Loop 起點為 var6 + var4 離動距離
                NextDosWriteLoc = NextDosWriteLoc + 7;                            // Loop 改為  NextDocReadLoc 直到超過
                if (NextDosReadLoc > NextDosWriteLoc) var1 = NextDosReadLoc;
                else var1 = NextDosWriteLoc;
            } // ned next Dos loop                
        } // end NextDosFlag = "Y";

        //////////////////////////////////////////////////////////////// arr[26] To arr[27]
        // MPQ = 300          MPQ             MPQQty         欠庫存
        // Curr   C1  1000   1000-0           1200           -200
        // 下周一 D1  1500   1500-200=1300    1500           -200
        // 下周一 D2  700    700-200  =500     600           -100
        // 下周一 D3  200    200-100  =100     300           -200
        // 下周一 WK  1000   1000-200 =800
        // 如果 WK 有減不足應往前面扣
        ////////////////////// MPQ ///////////////////////////// arraytransDay[tmptodaylocation, 26]

        if (tmpMPQ == "0")
        {
            for (var4 = 1; var4 < (arraytransDayfixLong400); var4++)     // if MPQ = 0 將 array26 移到 array27    
                arraytransDay[var4, 27] = arraytransDay[var4, 26];       // CurrNextDos -> MPQ
            return;                                                      // 結束  
        }

        tmpMPQPacket = Convert.ToInt32(tmpMPQ);
        var1 = 0;
        tmplose = 0;
        for (var1 = 0; var1 < 5; var1++)                                              // 計算 目前, 下 3 周 (C+3) , 及 C+4 
        {                                                                             //
            switch (var1)                                                           //
            {                                                                         // 
                case 0: MPQarrayloc = tmptodaylocation; break;                        // 用 var1 變數當                        
                case 1: MPQarrayloc = tmptodaylocation + nextMondayoffset; break;     //  0 為目前                                                   //  
                case 2: MPQarrayloc = tmptodaylocation + nextMondayoffset + 7; break; //  1 為下周 + 離周一偏移為置
                case 3: MPQarrayloc = tmptodaylocation + nextMondayoffset + 14; break;//
                case 4: MPQarrayloc = tmptodaylocation + nextMondayoffset + 21; break;//                       
                default: MPQarrayloc = tmptodaylocation + nextMondayoffset; break;    //
            }                                                                         //
            //  tmplose 表前欠數量 
            var3 = Convert.ToInt32(arraytransDay[MPQarrayloc, 26]);                   //
            var3 = var3 - tmplose;                                                    // Curr DosQty - 前欠數量   
            if ((var1 == 4) && (var3 >= 0))                                     //  最後只處理. 不做 MPQ
            {                                                                         //  > 0 回寫 WK 餘數 
                arraytransDay[MPQarrayloc, 27] = var3.ToString();                     //  前欠數量 = 0 
                tmplose = 0;                                                          //
            }                                                                         // 
            else
                if ((var1 == 4) && (var3 < 0))
                {
                    arraytransDay[MPQarrayloc, 27] = "0";                               //  前欠數量 = 0 
                    tmplose = -var3;
                }
                else
                    if (var3 == 0)                                                         //  if 目前 DV 數量為 0 array[27] MPQQty = 0 
                    {                                                                      //
                        arraytransDay[MPQarrayloc, 27] = "0";                              //
                        tmplose = 0;                                                       //  
                    }                                                                      //
                    else                                                                   //     
                        if (var3 > 0)                                                          //             
                        {                                                                      //               if 目前 DV 數量為 > 0
                            if ((var3 % tmpMPQPacket) == 0) var4 = (var3 / tmpMPQPacket) * tmpMPQPacket; // if DV數量 取餘數 MPQ包裝量 = 0
                            else var4 = ((var3 / tmpMPQPacket) + 1) * tmpMPQPacket;                  //         MPQ數量 = (DV數量/MPQ包裝量) x MPQ包裝量
                            //         else  MPQ數量 = ((DV數量/MPQ包裝量)+1) x MPQ包裝量
                            tmplose = var4 - var3;                                               //         前欠數量 = MPQ數量 - DV數量
                            arraytransDay[MPQarrayloc, 27] = var4.ToString();                    //         MPQ數量 Write Array[27]
                        }                                                                        //
                        else // var3 < 0                                                         //       
                        {                                                                        //         if 目前 DV 數量為 < 0
                            arraytransDay[MPQarrayloc, 27] = "0";                                //            MPQ數量 = 0 Write Array[27]
                            tmplose = -var3;                                                     //            前欠數量 = ( MPQ數量 + 前欠數量 )
                        }
        } // end loop 4 times       


        //////////////////////////////////////////////////////////////// 倒推 arr[27]
        // MPQ = 300          MPQQty          ChangeMPQQty         欠庫存
        //                                                        -1000
        // 前4周一 WK          360             0                   360-1000=-740
        // 前3周一 D3          360             0                   360-740 = -280      
        // 前2周一 D2          720             720-280=440         0      
        ////////////////////// MPQ /////////////////////////////

        if (tmplose > 0)  // 須倒推回去                                                  //  
        {                                                                                //
            for (var1 = 0; var1 < 5; var1++)                                             //        if 前欠數量 仍有 > 0
            {                                                                            //        同上位置, 但 var1 位置相反 
                if (var1 == 4) MPQarrayloc = tmptodaylocation;                           // 
                if (var1 == 3) MPQarrayloc = tmptodaylocation + nextMondayoffset;        //
                if (var1 == 2) MPQarrayloc = tmptodaylocation + nextMondayoffset + 7;    //
                if (var1 == 1) MPQarrayloc = tmptodaylocation + nextMondayoffset + 14;   //
                if (var1 == 0) MPQarrayloc = tmptodaylocation + nextMondayoffset + 21; ; //
                //  
                var3 = Convert.ToInt32(arraytransDay[MPQarrayloc, 27]);                  //        開始從C+3 後周一 旦 ( C+4 )
                var3 = var3 - tmplose; // Curr DosQty - 前欠數量                         //      
                if (var3 >= 0)                                                           //        if (MPQ 數量 - 前欠數量 > 0)
                {                                                                        //            MPQ 數量 = MPQ 數量 - 前欠數量;
                    arraytransDay[MPQarrayloc, 27] = var3.ToString();                    //            結束計算 var5 = 5
                    tmplose = 0;                                                         //            Claer tmplose
                    var1 = 5;         // break;                                          //  
                }                                                                        //        else 
                else                                                                     //
                {                                                                        //
                    arraytransDay[MPQarrayloc, 27] = "0";                                //            MPQ 數量 = 0;
                    tmplose = -var3;                                                     //            前欠數量 = MPQ 數量 - 前欠數量;
                }                                                                        //            再計算

            }
        }

        if (tmplose > 0)
        {
            // 20121031Response.Write("<script>alert('MPQ計算 前欠數量 > 0 失敗，請稍后檢查重試！')</script>");
        }
        for (var4 = tmptodaylocation + nextMondayoffset + 21 + 1; var4 < arraytransDayfixLong400 - 14; var4++) arraytransDay[var4, 27] = arraytransDay[var4, 26]; // 將以後 Week 補齊

    } // end function Cal_CurrNextDosMPQ  


    ///////////////////////////////////////////
    // Error Condition Wrute to table
    //
    private void WritmpExeceptionLog(string tExeceptionType, string tSeqno, string tSourcefile, ref string tmpDocumentID, string tReleaseDate, ref string tmpCustomerSite, ref string tmpFoxconnSite, ref string tmpCustomerPN, string tFoxconnPN, ref DateTime tmptoday, string tMailFlag)
    {
        string tmpsapce = "";
        string ttoday = tmptoday.ToString("yyyyMMdd");
        string tmpsql01 = "";
        int Reti = 0;

        tmpsql01 = "Insert into ExeceptionLog ( ExeceptionType, Seqno, Sourcefile, DocumentID, ReleaseDate, CustomerSite, "
        + "FoxconnSite, CustomerPN, FoxconnPN, CurrentDate, MailFlag ) Values ( '" + tExeceptionType + "', "
        + " '" + tSeqno + "', + '" + tSourcefile + "', '" + tmpDocumentID + "',    "
        + " '" + tReleaseDate + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', "
        + " '" + tmpCustomerPN + "', '" + tFoxconnPN + "','" + ttoday + "', '" + tmpsapce + "'  ) ";
        Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, tmpsql01 ); // 76
        if ( Reti == 0)
        {
            ErrMsg = "Insert EV PreETA Fail";
        }        
        //if (!DataConnlib.Excute(tmpsql01))
        //{           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
        //    // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
        //    ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
        //}
    }

    ///////////////////////////////////////////////////////////////////////////////////////////
    // 20100314
    // Merge GIT to currency 依最後一天 DocumentID 為準取其相同 DocumentID 有效 
    // CustomerSite FoxconnSite CustomerPN  Delivery_Date   DocumentID 
    // Beijing      BJ          A           20100309        8886
    // Beijing      BJ          A           20100309        8887
    // Beijing      BJ          A           20100310        8887
    // Beijing      BJ          A           20100310        8888  -- Get Info
    // Beijing      BJ          A           20100310        8888  -- Get Info
    // Beijing      BJ          A           20100311        8888  -- Get Info
    //
    // arrayGIT[var2 + 1, 1] = ds4.Tables[0].Rows[var2]["Document_ID"].ToString();
    //        arrayGIT[var2 + 1, 2] = ds4.Tables[0].Rows[var2]["Nokia_DUNS"].ToString();
    //        arrayGIT[var2 + 1, 3] = ds4.Tables[0].Rows[var2]["Foxconn_Site"].ToString();
    //        arrayGIT[var2 + 1, 4] = ds4.Tables[0].Rows[var2]["CUS_Materilano"].ToString();
    //        arrayGIT[var2 + 1, 5] = ds4.Tables[0].Rows[var2]["Delivery_Qty"].ToString();
    //        arrayGIT[var2 + 1, 6] = ds4.Tables[0].Rows[var2]["Delivery_Date"].ToString();
    //        arrayGIT[var2 + 1, 7] = ""; // CustomerSide
    //        arrayGIT[var2 + 1, 8] = ds4.Tables[0].Rows[var2]["IN_DATE"].ToString();  // -1
    //        arrayGIT[var2 + 1, 9] = var1.ToString();
    //        arrayGIT[var2 + 1,10] = ds4.Tables[0].Rows[var2]["Delivery_Number"].ToString();
    //////////////////////////////////////////////////////////
    private void mergeGIT(ref int arrayGITLong, ref string[,] arrayGIT)  //    
    {
        int loctmpvar1, loctmpvar2, loctmpvar3;
        string loctmpDocumentID = "";
        string loctmpNokia_DUNS = "";
        string loctmpCustomerSite = "";
        string loctmpFoxconn_Site = "";
        string loctmpDelivery_Date = "";
        string loctmpCUS_Materilano = "";
        string loctmpDelivery_Qty = "";
        string s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, locsql2;
        s1 = "";
        loctmpvar2 = 0;
        s5 = "";

        if (arrayGITLong == 0) return; // No Data

        loctmpDocumentID = arrayGIT[arrayGITLong, 1];  // 最後一筆倒推
        loctmpNokia_DUNS = arrayGIT[arrayGITLong, 2];
        loctmpFoxconn_Site = arrayGIT[arrayGITLong, 3];
        loctmpCUS_Materilano = arrayGIT[arrayGITLong, 4];

        for (loctmpvar1 = arrayGITLong; loctmpvar1 > 0; loctmpvar1--)
        {
            s1 = arrayGIT[loctmpvar1, 1];
            s2 = arrayGIT[loctmpvar1, 2];
            s3 = arrayGIT[loctmpvar1, 3];
            s4 = arrayGIT[loctmpvar1, 4];


            if ((loctmpNokia_DUNS == arrayGIT[loctmpvar1, 2]) && (loctmpFoxconn_Site == arrayGIT[loctmpvar1, 3]) && (loctmpCUS_Materilano == arrayGIT[loctmpvar1, 4]))
            {

                if (loctmpDocumentID != arrayGIT[loctmpvar1, 1])
                {
                    arrayGIT[loctmpvar1, 11] = "D"; // Clear
                    loctmpvar2++;
                }

            }   // ( loctmpvar1 > 0 )
            else
            {
                loctmpDocumentID = arrayGIT[loctmpvar1, 1];  // 最後一筆倒推
                loctmpNokia_DUNS = arrayGIT[loctmpvar1, 2];
                loctmpFoxconn_Site = arrayGIT[loctmpvar1, 3];
                loctmpCUS_Materilano = arrayGIT[loctmpvar1, 4];
            }
        }

        // Clear duplicate array  20100313
        loctmpvar2 = 0;
        loctmpvar3 = 0; // 移動次數
        for (loctmpvar1 = 1; loctmpvar1 < arrayGITLong + 1; loctmpvar1++)
        {
            if (arrayGIT[loctmpvar1, 11] == "D")
            {
                loctmpvar2 = loctmpvar1; // Move Start point 往前移一格

                loctmpvar3++; // 移動次數
                for (loctmpvar2 = loctmpvar1; loctmpvar2 < arrayGITLong; loctmpvar2++)
                {

                    arrayGIT[loctmpvar2, 1] = arrayGIT[loctmpvar2 + 1, 1]; // DocumentID
                    arrayGIT[loctmpvar2, 2] = arrayGIT[loctmpvar2 + 1, 2]; // Nokia_DUNS
                    arrayGIT[loctmpvar2, 3] = arrayGIT[loctmpvar2 + 1, 3]; // ds4.Tables[0].Rows[var2]["Foxconn_Site"].ToString();
                    arrayGIT[loctmpvar2, 4] = arrayGIT[loctmpvar2 + 1, 4]; // ds4.Tables[0].Rows[var2]["CUS_Materilano"].ToString();
                    arrayGIT[loctmpvar2, 5] = arrayGIT[loctmpvar2 + 1, 5]; // ds4.Tables[0].Rows[var2]["Delivery_Qty"].ToString();
                    arrayGIT[loctmpvar2, 6] = arrayGIT[loctmpvar2 + 1, 6]; // ds4.Tables[0].Rows[var2]["Delivery_Date"].ToString();
                    arrayGIT[loctmpvar2, 7] = arrayGIT[loctmpvar2 + 1, 7]; //  // CustomerSide
                    arrayGIT[loctmpvar2, 8] = arrayGIT[loctmpvar2 + 1, 8]; //  ds4.Tables[0].Rows[var2]["IN_DATE"].ToString();  // -1
                    arrayGIT[loctmpvar2, 9] = arrayGIT[loctmpvar2 + 1, 9]; // var1.ToString();
                    arrayGIT[loctmpvar2, 10] = arrayGIT[loctmpvar2 + 1, 10]; // ds4.Tables[0].Rows[var2]["Delivery_Number"].ToString();
                    arrayGIT[loctmpvar2, 11] = arrayGIT[loctmpvar2 + 1, 11]; // delete falg
                    arrayGIT[loctmpvar2, 12] = arrayGIT[loctmpvar2 + 1, 12]; // Delivery_Qty"].ToString(); // Bbackup Org Qty
                }
                loctmpvar1--; // 移到原位在執行   
            }
        }

        if (loctmpvar3 > 0) arrayGITLong = arrayGITLong - loctmpvar3;  // Cut the No Use // Cancell 20100315

        s1 = s1; // EndEventHandler trans

    }


    // write to ShipPlan memory
    private void TmpWriShipPlanarraytransDay(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref string[,] arrayCustomerFoxconnPNToOneSet)  //    
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
        //                     4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  0     1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 
        tmptransDay = "001002003004C00005006007008009010011C01012013014015016017018C02019020021022023024025C03W01W02W03W04W05W06W07W08W09W10W11W12W13W14W15E01EEEEEE"; // first4loc 加地第一個周4
        tmpShipPlan = "007008009010011012013014015016017018019020021022023024025026027028029030031032033034035036037038039040041042043044045046047048049050051052053054055"; // first4loc


        if ((arraytransDay[lvar31, 17].ToString() == "LH") && (arraytransDay[lvar31, 18].ToString() == "0254600") && (arraytransDay[lvar31, 16].ToString() == "Beijing"))
            locsql1 = arraytransDay[lvar31, 18].ToString();


        CurrShipPlanLine = Convert.ToInt32(locrefstr1);                            // 第幾個 Array

        //      Project CustomerSite Description CustomerPN H.H P/N FoxconnSite Dom_Exp  20100415  20100416 20100417  20100418
        arrayShipPlan[CurrShipPlanLine, 0] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 0].ToString(); // Project
        arrayShipPlan[CurrShipPlanLine, 1] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 1].ToString(); // Customer
        arrayShipPlan[CurrShipPlanLine, 2] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 7].ToString(); // Description  
        arrayShipPlan[CurrShipPlanLine, 3] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 3].ToString(); // CustomerPN  
        arrayShipPlan[CurrShipPlanLine, 4] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 8].ToString(); // HHPN  
        arrayShipPlan[CurrShipPlanLine, 5] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 2].ToString(); // FoxconnSite  
        arrayShipPlan[CurrShipPlanLine, 6] = arrayCustomerFoxconnPNToOneSet[CurrShipPlanLine, 9].ToString(); // Dom_Exp  

        //
        locvar1 = Convert.ToInt32(arraytransDay[tmptodaylocation, 10].ToString()); // 星期幾 ? 找周4

        if (locvar1 == 0) first4loc = tmptodaylocation - 7 + 4;                    // 第一個周4位置
        else if (locvar1 >= 4) first4loc = tmptodaylocation - locvar1 + 4;       //
        else first4loc = tmptodaylocation - locvar1 + 4 - 7;   //

        prefirst4loc = first4loc - 1; // count from 0
        readheadloc = prefirst4loc;
        readtailloc = 3 * 1;
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
                            subtot = subtot + Convert.ToInt32(arraytransDay[locvar2 + locvar1, 27]); // 加一周

                        arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = subtot.ToString();
                        tottot = subtot + tottot;
                        subtot = 0;
                    }
                    else
                    {
                        tmpMPQ = Convert.ToInt32(arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 27].ToString());
                        arrayShipPlan[CurrShipPlanLine, Convert.ToInt32(s2)] = arraytransDay[Convert.ToInt32(s1) + prefirst4loc, 27].ToString();
                        subtot = tmpMPQ + subtot;
                        tottot = tmpMPQ + tottot;
                    }  // end if 

            readtailloc = readtailloc + 3;

        }  // end while      


    }  // end of TmpWriShipPlanarraytransDay


    private void TmpWriDBFShipPlanarraytransDay(ref string locrefstr1, ref string[,] arraytransDay, ref string[,] arrayShipPlan, ref int tmptodaylocation, ref int WriteDBFBaseLongLoc, ref DateTime firstThuDate, ref string tmpSPWeek)  //    
    {
        int lvar31 = 1;
        int localvar1 = 7;
        int localvar2 = 7;
        int localvar3 = 7;
        int localvar4 = 7;
        string locsql1 = "";
        string locstr1 = "";
        string locstr2 = "";
        int Reti = 0;
        

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
        //    (Convert.ToInt32(tmpMaxMin.DayOfWeek)).ToString();
        lvar31 = tmpSPWeek.Length;
        localvar4 = Convert.ToInt32(tmpSPWeek.Substring(7, lvar31 - 7));  // 第幾週  20110101
        // localvar4 = Convert.ToInt32(tmpSPWeek.Substring(7, 2));  // 第幾週  20100728
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

            if (localvar1 == 0) HeadarrayShipPlan[2, ++localvar2] = "ToT";  // Week total


            localvar1++;

        }

        for (localvar2 = 36; localvar2 <= 53; localvar2++)
        {
            HeadarrayShipPlan[1, localvar2] = "WK" + (localvar4).ToString();
            HeadarrayShipPlan[2, localvar2] = (firstThuDate.AddDays(localvar3)).ToString("yyyyMMdd");
            localvar4 = localvar4 + 1;
            localvar3 = localvar3 + 7;

        }

        lvar31 = 0;
        while (lvar31 <= 2)  // write head 
        {

            locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53) Values ( '" + HeadarrayShipPlan[lvar31, 0].ToString() + "', '" + HeadarrayShipPlan[lvar31, 1].ToString() + "', '" + HeadarrayShipPlan[lvar31, 2].ToString() + "', '" + HeadarrayShipPlan[lvar31, 3].ToString() + "', '" + HeadarrayShipPlan[lvar31, 4].ToString() + "', '" + HeadarrayShipPlan[lvar31, 5].ToString() + "', '" + HeadarrayShipPlan[lvar31, 6].ToString() + "', '" + HeadarrayShipPlan[lvar31, 7].ToString() + "', '" + HeadarrayShipPlan[lvar31, 8].ToString() + "', '" + HeadarrayShipPlan[lvar31, 9].ToString() + "', '" + HeadarrayShipPlan[lvar31, 10].ToString() + "', '" + HeadarrayShipPlan[lvar31, 11].ToString() + "', '" + HeadarrayShipPlan[lvar31, 12].ToString() + "',  '" + HeadarrayShipPlan[lvar31, 13].ToString() + "', '" + HeadarrayShipPlan[lvar31, 14].ToString() + "', '" + HeadarrayShipPlan[lvar31, 15].ToString() + "', '" + HeadarrayShipPlan[lvar31, 16].ToString() + "', '" + HeadarrayShipPlan[lvar31, 17].ToString() + "', '" + HeadarrayShipPlan[lvar31, 18].ToString() + "', '" + HeadarrayShipPlan[lvar31, 19].ToString() + "', '" + HeadarrayShipPlan[lvar31, 20].ToString() + "', '" + HeadarrayShipPlan[lvar31, 21].ToString() + "', '" + HeadarrayShipPlan[lvar31, 22].ToString() + "', '" + HeadarrayShipPlan[lvar31, 23].ToString() + "', '" + HeadarrayShipPlan[lvar31, 24].ToString() + "', '" + HeadarrayShipPlan[lvar31, 25].ToString() + "', '" + HeadarrayShipPlan[lvar31, 26].ToString() + "', '" + HeadarrayShipPlan[lvar31, 27].ToString() + "', '" + HeadarrayShipPlan[lvar31, 28].ToString() + "', '" + HeadarrayShipPlan[lvar31, 29].ToString() + "', '" + HeadarrayShipPlan[lvar31, 30].ToString() + "', '" + HeadarrayShipPlan[lvar31, 31].ToString() + "', '" + HeadarrayShipPlan[lvar31, 32].ToString() + "', '" + HeadarrayShipPlan[lvar31, 33].ToString() + "', '" + HeadarrayShipPlan[lvar31, 34].ToString() + "', '" + HeadarrayShipPlan[lvar31, 35].ToString() + "', '" + HeadarrayShipPlan[lvar31, 36].ToString() + "', '" + HeadarrayShipPlan[lvar31, 37].ToString() + "', '" + HeadarrayShipPlan[lvar31, 38].ToString() + "', '" + HeadarrayShipPlan[lvar31, 39].ToString() + "', '" + HeadarrayShipPlan[lvar31, 40].ToString() + "', '" + HeadarrayShipPlan[lvar31, 41].ToString() + "', '" + HeadarrayShipPlan[lvar31, 42].ToString() + "', '" + HeadarrayShipPlan[lvar31, 43].ToString() + "', '" + HeadarrayShipPlan[lvar31, 44].ToString() + "', '" + HeadarrayShipPlan[lvar31, 45].ToString() + "', '" + HeadarrayShipPlan[lvar31, 46].ToString() + "', '" + HeadarrayShipPlan[lvar31, 47].ToString() + "', '" + HeadarrayShipPlan[lvar31, 48].ToString() + "', '" + HeadarrayShipPlan[lvar31, 49].ToString() + "', '" + HeadarrayShipPlan[lvar31, 50].ToString() + "', '" + HeadarrayShipPlan[lvar31, 51].ToString() + "', '" + HeadarrayShipPlan[lvar31, 52].ToString() + "', '" + HeadarrayShipPlan[lvar31, 53].ToString() + "'  ) ";
            Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
            if (Reti == 0)
            {
                ErrMsg = "TestDBFShipPlan Head 新增失敗";
            }            
           
            //if (DataConnlib.Excute(locsql1))
            //{
            //    // Response.Write("<script>alert('Test_Syncro_ShippingPlan_ETA 新增成功！')</script>");
            //}
            //else
            //{
            //    //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
            //    // Response.Write("<script>alert('TestDBFShipPlan Head 新增失敗，請稍后重試！ ')</script>"); //
            //
            //}

            lvar31++;
        }


        lvar31 = 1;
        while (arrayShipPlan[lvar31, 1] != "")   // write data
        {

            locsql1 = "Insert into  TestDBFShipPlan ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18,F19, F20, F21,F22, F23,F24, F25, F26, F27,F28, F29, F30,F31,F32, F33, F34,F35, F36,F37,F38, F39, F40,F41, F42,F43,F44, F45,F46,F47, F48,F49,F50, F51,F52, F53) Values ( '" + arrayShipPlan[lvar31, 0].ToString() + "', '" + arrayShipPlan[lvar31, 1].ToString() + "', '" + arrayShipPlan[lvar31, 2].ToString() + "', '" + arrayShipPlan[lvar31, 3].ToString() + "', '" + arrayShipPlan[lvar31, 4].ToString() + "', '" + arrayShipPlan[lvar31, 5].ToString() + "', '" + arrayShipPlan[lvar31, 6].ToString() + "', '" + arrayShipPlan[lvar31, 7].ToString() + "', '" + arrayShipPlan[lvar31, 8].ToString() + "', '" + arrayShipPlan[lvar31, 9].ToString() + "', '" + arrayShipPlan[lvar31, 10].ToString() + "', '" + arrayShipPlan[lvar31, 11].ToString() + "', '" + arrayShipPlan[lvar31, 12].ToString() + "',  '" + arrayShipPlan[lvar31, 13].ToString() + "', '" + arrayShipPlan[lvar31, 14].ToString() + "', '" + arrayShipPlan[lvar31, 15].ToString() + "', '" + arrayShipPlan[lvar31, 16].ToString() + "', '" + arrayShipPlan[lvar31, 17].ToString() + "', '" + arrayShipPlan[lvar31, 18].ToString() + "', '" + arrayShipPlan[lvar31, 19].ToString() + "', '" + arrayShipPlan[lvar31, 20].ToString() + "', '" + arrayShipPlan[lvar31, 21].ToString() + "', '" + arrayShipPlan[lvar31, 22].ToString() + "', '" + arrayShipPlan[lvar31, 23].ToString() + "', '" + arrayShipPlan[lvar31, 24].ToString() + "', '" + arrayShipPlan[lvar31, 25].ToString() + "', '" + arrayShipPlan[lvar31, 26].ToString() + "', '" + arrayShipPlan[lvar31, 27].ToString() + "', '" + arrayShipPlan[lvar31, 28].ToString() + "', '" + arrayShipPlan[lvar31, 29].ToString() + "', '" + arrayShipPlan[lvar31, 30].ToString() + "', '" + arrayShipPlan[lvar31, 31].ToString() + "', '" + arrayShipPlan[lvar31, 32].ToString() + "', '" + arrayShipPlan[lvar31, 33].ToString() + "', '" + arrayShipPlan[lvar31, 34].ToString() + "', '" + arrayShipPlan[lvar31, 35].ToString() + "', '" + arrayShipPlan[lvar31, 36].ToString() + "', '" + arrayShipPlan[lvar31, 37].ToString() + "', '" + arrayShipPlan[lvar31, 38].ToString() + "', '" + arrayShipPlan[lvar31, 39].ToString() + "', '" + arrayShipPlan[lvar31, 40].ToString() + "', '" + arrayShipPlan[lvar31, 41].ToString() + "', '" + arrayShipPlan[lvar31, 42].ToString() + "', '" + arrayShipPlan[lvar31, 43].ToString() + "', '" + arrayShipPlan[lvar31, 44].ToString() + "', '" + arrayShipPlan[lvar31, 45].ToString() + "', '" + arrayShipPlan[lvar31, 46].ToString() + "', '" + arrayShipPlan[lvar31, 47].ToString() + "', '" + arrayShipPlan[lvar31, 48].ToString() + "', '" + arrayShipPlan[lvar31, 49].ToString() + "', '" + arrayShipPlan[lvar31, 50].ToString() + "', '" + arrayShipPlan[lvar31, 51].ToString() + "', '" + arrayShipPlan[lvar31, 52].ToString() + "', '" + arrayShipPlan[lvar31, 53].ToString() + "'  ) ";
            Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1 ); // 76
            if ( Reti == 0)
            {
                ErrMsg = "'TestDBFShipPlan 新增失敗";
            } 
            //if (DataConnlib.Excute(locsql1))
            //{
            //    // Response.Write("<script>alert('Test_Syncro_ShippingPlan_ETA 新增成功！')</script>");
            //}
            //else
            //{
            //    //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
            //    //  Response.Write("<script>alert('TestDBFShipPlan 新增失敗，請稍后重試！ ')</script>"); //              
            //
            //}

            lvar31++;
        }


    }  // end of TmpWriDBFShipPlanarraytransDay
    /////////////////////////////////////////////////////////////////////////
    //  Write Memory to file for Tracer
    //  AccountNum char(5)
    //  SPDate char(50)
    //  SPDAte_8bytes C(10)
    //  Org_SPQty C(10)
    //  Leadtime_SPQty C(10)
    //  Leadtime_GIT_SPQty C(10)
    //  TodayGIT_SPQty C(10)
    //  SumSPQTY C(10)
    //  SumC3 C(10)
    //  EVWeekOfDay C(10)
    //  SWWeekDay C(10)
    //  ReleaseDateC(10)
    //  SPweek
    //  DocumentID C(50)
    //  Array15
    //  CustomerSite  C(16) 16
    //  FoxconnSite   C(16) 17
    //  CustomerPN    C(30) 18
    //       DVCode (19)      |
    //       first time Come from DV(20) | FirstFlag  
    //       arraytransDay[var1, 21] = tmpCurrent_Dos;
    //       arraytransDay[var1, 22] = tmpNext_Dos;
    //       arraytransDay[var1, 23] = tmpGIT_Dos;
    //       arraytransDay[var1, 24] = tmpMPQ;
    //       arraytransDay[var1, 25] = tmpLeadTime;
    //       arraytransDay[var1, 26] = Curr_Dos_Qty + Next_Dos_Qty CurrNextDosQty
    //       arraytransDay[var1, 27] = MPQQty
    //       arraytransDay[var1, 28] = DownToTQty       
    /////////////////////////////////////////////////////////////////////


    private void TmpWriHardarraytransDay(ref string locrefstr1, ref string[,] arraytransDay)  //    
    {
        int lvar31 = 0;
        int lvar33 = 0;
        DateTime ltmpdate1 = DateTime.Today;
        string locsql1 = "";
        string locsql2 = "";
        string tmpp1, tmpd1, tmpr1, tmpr2, tmpr3;
        string s1, s2, s3, s4, s5, s6, s7, s8, d9;
        string tmpspace = "";
        int Reti = 0;

        swdayWeek = "Day";
        // for (lvar31 = tmptodaylocation; lvar31 < tmpC3location; lvar31++)   // 
        // for (lvar31 = 99; lvar31 < tmpC3location ; lvar31++)



        for (lvar31 = 99; lvar31 < tmpC3location; lvar31++) // Real program
        {
            ltmpdate1 = Convert.ToDateTime(arraytransDay[lvar31, 2]);
            s1 = tmpCustomerSite;
            s2 = tmpFoxconnSite;
            s3 = tmpCustomerPN;
            s4 = arraytransDay[lvar31, 16].ToString();
            s5 = arraytransDay[lvar31, 17].ToString();
            s6 = arraytransDay[lvar31, 18].ToString();
            s7 = arraytransDay[lvar31, 03].ToString();
            s8 = arraytransDay[lvar31, 04].ToString();
            arraytransDay[lvar31, 11] = "Day";


            if ((tmpCustomerSite == "") || (tmpFoxconnSite == "") || (tmpCustomerPN == ""))
                lblMsg = "ErrortmpCustomerSite = Space ";

            if (Programflag == '1')
            {

                locsql1 = "Insert into  MemoryETDToETA ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, "
                        + " Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty, Nokia1Document_ID, Nokia2Document_ID, CustomerDum, OrgGITQty, GITDocument_ID ) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', "
                        + " '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "', '" + tmpNokiaEVDocm + "', '" + tmpspace + "', '" + tmpCustomerDum + "', '" + arraytransDay[lvar31, 31].ToString() + "', '" + arraytransDay[lvar31, 32].ToString() + "'  ) ";
                Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 
                if (Reti == 0)
                {
                    ErrMsg = "MemoryETDToETA Insert Day Proc Fail";
                } 

                //if (!DataConnlib.Excute(locsql1))
                //{
                //    // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！')</script>");  twt
                //    ErrMsg = "MemoryETDToETA Insert Day Proc Fail  Call programmer";
                //}

            }
            if (Programflag == '2')
                if ((arraytransDay[lvar31, 4] != "0") || (arraytransDay[lvar31, 26] != "0") || (arraytransDay[lvar31, 27] != "0") || (lvar31 == 101))
                {
                    locsql1 = "Insert into  MemoryCurrNextDosMPQ ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "'  ) ";
                    Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
                    if (Reti == 0)
                    {
                        ErrMsg = "Insert EV PreETA Fail";
                    } 

                    //if (DataConnlib.Excute(locsql1))
                    //{
                    //    // Response.Write("<script>alert('Test_Syncro_ShippingPlan_ETA 新增成功！')</script>");
                    //}
                    //else
                    //{
                    //    //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                    //    // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ + '" + tmpCustomerSite + "' ')</script>"); //
                    //}
                }
            tmpaccount++;
        }  // end Day


        swdayWeek = "Week";
        for (lvar33 = 0; lvar33 < 19 * 7; lvar33++)  // For 19 Week Begin Monday
        {
            lvar31 = tmpC3location + lvar33;

            ltmpdate1 = Convert.ToDateTime(arraytransDay[lvar31, 2]);
            s1 = tmpCustomerSite;
            s2 = tmpFoxconnSite;
            s3 = tmpCustomerPN;
            s4 = arraytransDay[lvar31, 16].ToString();
            s5 = arraytransDay[lvar31, 17].ToString();
            s6 = arraytransDay[lvar31, 18].ToString();
            s7 = arraytransDay[lvar31, 03].ToString();
            s8 = arraytransDay[lvar31, 04].ToString();
            // arraytransDay[lvar31, 11] = "Week";

            if (Programflag == '1')
            {
                locsql1 = "Insert into  MemoryETDToETA ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, "
                        + " Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty, Nokia1Document_ID, Nokia2Document_ID, CustomerDum, OrgGITQty, GITDocument_ID ) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', "
                        + " '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "', '" + tmpNokiaPVDocm + "', '" + tmpspace + "', '" + tmpCustomerDum + "', '" + arraytransDay[lvar31, 31].ToString() + "', '" + arraytransDay[lvar31, 32].ToString() + "'  ) ";
                Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
                if (Reti == 0)
                {
                    ErrMsg = "MemoryETDToETA Insert Week Proc Fail";
                } 
                //if (!DataConnlib.Excute(locsql1))
                //{
                //    // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！')</script>");
                //    ErrMsg = "MemoryETDToETA Insert Week Proc Fail, Call programmer";
                //}
            }
            if (Programflag == '2')
                if ((arraytransDay[lvar31, 4] != "0") || (arraytransDay[lvar31, 26] != "0") || (arraytransDay[lvar31, 27] != "0") || (lvar31 == 101))
                {
                    locsql1 = "Insert into  MemoryCurrNextDosMPQ ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "'  ) ";
                    Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
                    if (Reti == 0)
                    {
                        ErrMsg = "Insert MemoryETDToETA Fail";
                    } 
                    //if (!DataConnlib.Excute(locsql1))
                    //{
                    //    // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ + '" + tmpCustomerSite + "' ')</script>"); //
                    //}
                }
            tmpaccount++;
        }


    }  // end of TmpWriHardarraytransDay


    private void TmpWriPreETA(ref string locrefstr1, ref string[,] arraytransDay)  //    
    {
        int lvar31 = 0;
        int lvar33 = 0;
        DateTime ltmpdate1 = DateTime.Today;
        string locsql1 = "";
        string locsql2 = "";
        string tmpp1, tmpd1, tmpr1, tmpr2, tmpr3;
        string s1, s2, s3, s4, s5, s6, s7, s8, tmpDVType;
        string tmpspace = "";
        int v1 = 0;


        swdayWeek = "Day";
        // for (lvar31 = tmptodaylocation; lvar31 < tmpC3location; lvar31++)   // 
        // for (lvar31 = 99; lvar31 < tmpC3location; lvar31++)


        swdayWeek = "Day";
        tmpDVType = "EV";
        for (lvar31 = tmptodaylocation; lvar31 < tmpC3location; lvar31++) // Real program 
        {                                                                 // Write from today.
            ltmpdate1 = Convert.ToDateTime(arraytransDay[lvar31, 2]);
            s1 = tmpCustomerSite;
            s2 = tmpFoxconnSite;
            s3 = tmpCustomerPN;
            s4 = arraytransDay[lvar31, 16].ToString();
            s5 = arraytransDay[lvar31, 17].ToString();
            s6 = arraytransDay[lvar31, 18].ToString();
            s7 = arraytransDay[lvar31, 03].ToString();
            s8 = arraytransDay[lvar31, 04].ToString();
            arraytransDay[lvar31, 11] = "Day";

            if (Programflag == '1')
            {
                tmpDVType = "EV";
                locsql1 = "Insert into  PreETA ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, "
                        + " CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty, DVType, Nokia1Document_ID, Nokia2Document_ID, CustomerDum, OrgGITQty, GITDocument_ID ) "
                        + " Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', '" + arraytransDay[lvar31, 4].ToString() + "',    "
                        + " '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "',          "
                        + " '" + arraytransDay[lvar31, 9].ToString() + "', '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',       "
                        + " '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', "
                        + " '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 23].ToString() + "', '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', "
                        + " '" + arraytransDay[lvar31, 28].ToString() + "' , '" + tmpDVType + "', '" + tmpNokiaEVDocm + "', '" + tmpspace + "', '" + tmpCustomerDum + "', '" + arraytransDay[lvar31, 31].ToString() + "', '" + arraytransDay[lvar31, 32].ToString() + "' ) ";
                // 20121031 if (!DataConnlib.Excute(locsql1))
                //{
                //    // Response.Write("<script>alert('PreETA 新增失敗，請稍后重試！')</script>");  //
                //}
                v1 = DataBaseOperation.ExecSQL(DBType.ToLower(), DBWri, locsql1); // 76
                if (v1 == 0)
                {
                    lblMsg = "Insert EV PreETA Fail";
                }
           
            }

            tmpaccount++;
        }  // end Day

        tmpDVType = "PV";
        swdayWeek = "Week";
        for (lvar33 = 0; lvar33 < 23; lvar33++)  // For 19 Week Begin Monday
        {
            lvar31 = FirstPVLoc + lvar33 * 7;

            ltmpdate1 = Convert.ToDateTime(arraytransDay[lvar31, 2]);
            s1 = tmpCustomerSite;
            s2 = tmpFoxconnSite;
            s3 = tmpCustomerPN;
            s4 = arraytransDay[lvar31, 16].ToString();
            s5 = arraytransDay[lvar31, 17].ToString();
            s6 = arraytransDay[lvar31, 18].ToString();
            s7 = arraytransDay[lvar31, 03].ToString();
            s8 = arraytransDay[lvar31, 04].ToString();
            arraytransDay[lvar31, 11] = "Week";

            if (Programflag == '1')
            {
                tmpDVType = "PV";  // write PV
                locsql1 = "Insert into  PreETA ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty, DVType, Nokia1Document_ID, Nokia2Document_ID, CustomerDum, OrgGITQty, GITDocument_ID) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "' , '" + tmpDVType + "', '" + tmpNokiaPVDocm + "', '" + tmpspace + "', '" + tmpCustomerDum + "', '" + arraytransDay[lvar31, 31].ToString() + "', '" + arraytransDay[lvar31, 32].ToString() + "' ) ";
                //if (!DataConnlib.Excute(locsql1))
                //{
                //    // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！')</script>"); //
                //}
                v1 = DataBaseOperation.ExecSQL(DBType.ToLower(), DBWri, locsql1); // 76
                if (v1 == 0)
                {
                    lblMsg = "Insert PV PreETA Fail";
                }


                if (lvar33 >= 2)  // write EV before C+3
                {
                    tmpDVType = "EV";
                    locsql1 = "Insert into  PreETA ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty, DVType, Nokia1Document_ID, Nokia2Document_ID, CustomerDum, OrgGITQty, GITDocument_ID) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "' , '" + tmpDVType + "', '" + tmpNokiaEVDocm + "', '" + tmpspace + "', '" + tmpCustomerDum + "', '" + arraytransDay[lvar31, 31].ToString() + "', '" + arraytransDay[lvar31, 32].ToString() + "' ) ";
                    //if (!DataConnlib.Excute(locsql1))
                    //{
                    //    // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！')</script>"); //
                    //}
                    v1 = DataBaseOperation.ExecSQL(DBType.ToLower(), DBWri, locsql1); // 76
                    if (v1 == 0)
                    {
                        lblMsg = "Insert PV PreETA Fail";
                    }

                }

            }
            tmpaccount++;

        }


        return;

        swdayWeek = "Week";
        for (lvar33 = 0; lvar33 < 20; lvar33++)  // For 19 Week Begin Monday
        {
            lvar31 = tmpC3location + lvar33 * 7;

            ltmpdate1 = Convert.ToDateTime(arraytransDay[lvar31, 2]);
            s1 = tmpCustomerSite;
            s2 = tmpFoxconnSite;
            s3 = tmpCustomerPN;
            s4 = arraytransDay[lvar31, 16].ToString();
            s5 = arraytransDay[lvar31, 17].ToString();
            s6 = arraytransDay[lvar31, 18].ToString();
            s7 = arraytransDay[lvar31, 03].ToString();
            s8 = arraytransDay[lvar31, 04].ToString();
            arraytransDay[lvar31, 11] = "Week";

            if (Programflag == '1')
            {
                locsql1 = "Insert into  MemoryETDToETA ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "'  ) ";
                //if (DataConnlib.Excute(locsql1))
                //{
                //    // Response.Write("<script>alert('Test_Syncro_ShippingPlan_ETA 新增成功！')</script>");
                //}
                v1 = DataBaseOperation.ExecSQL(DBType.ToLower(), DBWri, locsql1); // 76
                if (v1 != 0)
                {
                    lblMsg = "Insert week PreETA OK";                    
                }
                else
                {
                    // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！')</script>"); //
                }
            }
            if (Programflag == '2')
                if ((arraytransDay[lvar31, 4] != "0") || (arraytransDay[lvar31, 26] != "0") || (arraytransDay[lvar31, 27] != "0") || (lvar31 == 101))
                {
                    locsql1 = "Insert into  MemoryCurrNextDosMPQ ( AccountNum, SPDate, SPDate_8Bytes, Org_Spilt_SPQty, Leadtime_SPQty, Leadtime_GIT_SPQty, TodayGIT_SPQty, SumSPQty, SumC3, EVWeekOfDay, SWWeekDay, ReleaseDate, SPweek, DocumentID, ETD_SPQty, CustomerSite, FoxconnSite, CustomerPN, DVCode, FirstFlag, Current_Dos, Next_Dos, GIT_Dos, MPQ, LeadTime, CurrNextDosQty, MPQQty, DownToTQty) Values ( '" + arraytransDay[lvar31, 1].ToString() + "', '" + arraytransDay[lvar31, 2].ToString() + "', '" + arraytransDay[lvar31, 3].ToString() + "', '" + arraytransDay[lvar31, 4].ToString() + "', '" + arraytransDay[lvar31, 5].ToString() + "', '" + arraytransDay[lvar31, 6].ToString() + "', '" + arraytransDay[lvar31, 7].ToString() + "', '" + arraytransDay[lvar31, 8].ToString() + "', '" + arraytransDay[lvar31, 9].ToString() + "', '" + arraytransDay[lvar31, 10].ToString() + "', '" + arraytransDay[lvar31, 11].ToString() + "', '" + arraytransDay[lvar31, 12].ToString() + "',  '" + arraytransDay[lvar31, 13].ToString() + "', '" + arraytransDay[lvar31, 14].ToString() + "', '" + arraytransDay[lvar31, 15].ToString() + "', '" + tmpCustomerSite + "', '" + tmpFoxconnSite + "', '" + tmpCustomerPN + "', '" + arraytransDay[lvar31, 19].ToString() + "', '" + arraytransDay[lvar31, 20].ToString() + "', '" + arraytransDay[lvar31, 21].ToString() + "', '" + arraytransDay[lvar31, 22].ToString() + "', '" + arraytransDay[lvar31, 23].ToString() + "', '" + arraytransDay[lvar31, 24].ToString() + "', '" + arraytransDay[lvar31, 25].ToString() + "', '" + arraytransDay[lvar31, 26].ToString() + "', '" + arraytransDay[lvar31, 27].ToString() + "', '" + arraytransDay[lvar31, 28].ToString() + "'  ) ";
                    // if (DataConnlib.Excute(locsql1))
                    v1 = DataBaseOperation.ExecSQL(DBType.ToLower(), DBWri, locsql1); // 76
                    if (v1 != 0)
                    {
                        // Response.Write("<script>alert('Test_Syncro_ShippingPlan_ETA 新增成功！')</script>");
                    }
                    else
                    {
                        //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                        // Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ + '" + tmpCustomerSite + "' ')</script>"); //
                    }
                }
            tmpaccount++;

        }

    }  // end of TmpWriPreTA



    /////////////////////////////////////////////////////////////////////////////////////////////
    // fill full int to char "000"  送入數字及補滿 "0" 長度, 回覆 String
    //////////////////////////////////////////////////////////////////////////////////////////////
    private string fillCharToSameLocZero(int InputNum, int ConvertZeroNo)
    {
        int returnvar1 = 0;
        string returnstr1 = "";
        string localstr2 = "";
        int baselong10 = 10;
        returnvar1 = InputNum + 100000000;  // 10
        localstr2 = returnvar1.ToString();
        returnstr1 = localstr2.Substring(baselong10 - 1 - ConvertZeroNo, ConvertZeroNo);
        return (returnstr1);
    }

    // 20121103
    //protected void btnUpload5_Click(object sender, EventArgs e)
    //{
    //    text6 = "D";
    //    text7 = "N";
    //}
    //protected void btnUpload6_Click(object sender, EventArgs e)
    //{
    //    string locsql1 = "";
    //
    //    // 20121031 Response.Write("<script>alert('正式系統不予修正參數！ 請回並繼續')</script>");
    //    return; // Not Open for Real Data 
    //
    //    int Reti = 0;
    //
    //    if ((text6 == "") || (text7 == "")) return;
    //
    //    text6 = text6.Substring(0, 1).ToUpper();
    //    text7 = text7.Substring(0, 1).ToUpper();
    //
    //    if ((text6 == "") || (text7 == "") || (text7 != "Y")) return;
    //
    //    if ((text6 == "F") && (text7 == "Y")) // Delete 3 file 
    //    {
    //        locsql1 = "DELETE * FROM [MemoryCurrNextDosMPQ]";
    //        Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
    //        if (Reti == 0)
    //        {
    //            ErrMsg = "UPDATE [ReqProcETDToETA] Fail";
    //            // 20121031 Response.Write("<script>alert('CLear ReqProc Finisheddate 失敗，請稍后重試！')</script>");
    //        } 
    //    }
    //
    //    if ((text6 == "D") && (text7 == "Y")) // Delete 3 file 
    //    {
    //        locsql1 = "DELETE * FROM [MemoryETDToETA]";
    //        Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
    //        if (Reti == 0)
    //        {
    //            ErrMsg = "UPDATE [ReqProcETDToETA] Fail";
    //            // 20121031 Response.Write("<script>alert('CLear ReqProc Finisheddate 失敗，請稍后重試！')</script>");
    //        } 
    //        // locsql1 = "DELETE * FROM [Syncro_ShippingPlan_ETA]";
    //        locsql1 = "DELETE * FROM [Syncro_ShippingPlan_ETA]";
    //        Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
    //        if (Reti == 0)
    //        {
    //            ErrMsg = "UPDATE [ReqProcETDToETA] Fail";
    //            // 20121031 Response.Write("<script>alert('CLear ReqProc Finisheddate 失敗，請稍后重試！')</script>");
    //        } 
    //            locsql1 = "DELETE * FROM [Test_Record]";
    //        Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
    //        if (Reti == 0)
    //        {
    //            ErrMsg = "UPDATE [ReqProcETDToETA] Fail";
    //            // 20121031 Response.Write("<script>alert('CLear ReqProc Finisheddate 失敗，請稍后重試！')</script>");
    //        } 
    //    }
    //    if ((text6 == "E") && (text7 == "Y"))
    //    {
    //        locsql1 = "UPDATE [ReqProcETDToETA] SET [FinishedDate] = '' ";
    //        Reti = DataBaseOperation.ExecSQL(DBType.ToLower(), DBRead, locsql1); // 76
    //        if (Reti == 0)
    //        {
    //            ErrMsg = "UPDATE [ReqProcETDToETA] Fail";
    //            // 20121031 Response.Write("<script>alert('CLear ReqProc Finisheddate 失敗，請稍后重試！')</script>");
    //        }            
    //             
    //    }
    //
    //    if ((text6 == "G") && (text7 == "Y")) //  
    //    {
    //        locsql1 = "DELETE * FROM [TestDBFShipPlan]";
    //        if (DataConnlib.Excute(locsql1))
    //        {
    //            // Response.Write("<script>alert('TestDBFShipPlan Delete ')</script>");
    //        }
    //        else
    //        {
    //            // 20121031 Response.Write("<script>alert('TestDBFShipPlan Delete 失敗，請稍后重試！')</script>");
    //        }
    //    }
    //    if ((text6 == "H") && (text7 == "Y"))  // text8 為傳參數 1. Setup Write MemoryCurrNextDos 明細
    //    {
    //        if (text8 == "") text8 = "Y";
    //        else text8 = "Y" + text8.Substring(1, (text8.Length) - 1);
    //    }
    //    text6 = "";
    //    text7 = "";
    // }  // btnUpload6_Click

    private void GetDataInArrarPart(ref string[,] arrayPart, ref DataSet ds1) // Put DBA data into arrarpart
    {
        int localvar = 0;

        for (localvar = 0; localvar < PartUploadETDrecordLong; localvar++)  // load in memory
        {
            arrayPart[localvar + 1, 1] = ds1.Tables[0].Rows[localvar]["NokiaSite"].ToString();
            arrayPart[localvar + 1, 2] = ds1.Tables[0].Rows[localvar]["FoxconnSite"].ToString();
            arrayPart[localvar + 1, 3] = ds1.Tables[0].Rows[localvar]["NokiaPartNo"].ToString();
            arrayPart[localvar + 1, 4] = ds1.Tables[0].Rows[localvar]["Current_Dos"].ToString();
            arrayPart[localvar + 1, 5] = ds1.Tables[0].Rows[localvar]["Next_Dos"].ToString();
            arrayPart[localvar + 1, 6] = ds1.Tables[0].Rows[localvar]["GIT_Dos"].ToString();
            arrayPart[localvar + 1, 7] = ds1.Tables[0].Rows[localvar]["MPQ"].ToString();
            arrayPart[localvar + 1, 8] = ds1.Tables[0].Rows[localvar]["LeadTime"].ToString();
            arrayPart[localvar + 1, 9] = localvar.ToString();
            arrayPart[localvar + 1, 10] = ds1.Tables[0].Rows[localvar]["Project"].ToString();
            arrayPart[localvar + 1, 11] = ds1.Tables[0].Rows[localvar]["FoxconnBU"].ToString();
            arrayPart[localvar + 1, 12] = ds1.Tables[0].Rows[localvar]["Description"].ToString();
            arrayPart[localvar + 1, 13] = ds1.Tables[0].Rows[localvar]["FoxconnPartNo"].ToString();
            arrayPart[localvar + 1, 14] = ds1.Tables[0].Rows[localvar]["Dom_Exp"].ToString();
            arrayPart[localvar + 1, 15] = ds1.Tables[0].Rows[localvar]["FoxconnPlant"].ToString();
            arrayPart[localvar + 1, 16] = ds1.Tables[0].Rows[localvar]["FoxconnPartNo"].ToString();

            if ((arrayPart[localvar + 1, 8] == "") || (arrayPart[localvar + 1, 8] == null)) arrayPart[localvar + 1, 8] = "0";
            if ((arrayPart[localvar + 1, 4] == "") || (arrayPart[localvar + 1, 4] == null)) arrayPart[localvar + 1, 4] = "0";
            if ((arrayPart[localvar + 1, 5] == "") || (arrayPart[localvar + 1, 5] == null)) arrayPart[localvar + 1, 5] = "0";
            if ((arrayPart[localvar + 1, 7] == "") || (arrayPart[localvar + 1, 7] == null)) arrayPart[localvar + 1, 7] = "0";


        }  // end load tabel in memory

    }

    // plant_name, foxconnsite, Forecast_CustomerPN, Document_ID ";
    private void GetDataInArrarNokiaEVDocm(ref string[,] arrayNokiaEVDocm, ref int arrayNokiaEVDocmLong, ref DataSet ds6) // Put DBA data into arrarpart
    {
        int localvar = 0;

        for (localvar = 0; localvar < arrayNokiaEVDocmLong; localvar++)  // load in memory
        {
            arrayNokiaEVDocm[localvar + 1, 1] = "";
            arrayNokiaEVDocm[localvar + 1, 2] = "";
            arrayNokiaEVDocm[localvar + 1, 3] = "";
            arrayNokiaEVDocm[localvar + 1, 4] = "";
            arrayNokiaEVDocm[localvar + 1, 5] = "";
            arrayNokiaEVDocm[localvar + 1, 6] = "";
        }

        for (localvar = 0; localvar < arrayNokiaEVDocmLong; localvar++)  // load in memory
        {
            arrayNokiaEVDocm[localvar + 1, 1] = ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
            arrayNokiaEVDocm[localvar + 1, 2] = ds6.Tables[0].Rows[localvar]["plant_name"].ToString();  // CustomerSite
            arrayNokiaEVDocm[localvar + 1, 3] = ds6.Tables[0].Rows[localvar]["foxconnsite"].ToString(); // FoxconnSite
            arrayNokiaEVDocm[localvar + 1, 4] = ds6.Tables[0].Rows[localvar]["Forecast_CustomerPN"].ToString();  // CustomerPN
            arrayNokiaEVDocm[localvar + 1, 5] = ds6.Tables[0].Rows[localvar]["Forecast_PartnerGBI"].ToString();  // CustomerDun       

        }  // end load tabel in memory

    }

    // plant_name, foxconnsite, Forecast_CustomerPN, Document_ID ";
    private void GetDataInArrarNokiaPVDocm(ref string[,] arrayNokiaPVDocm, ref int arrayNokiaPVDocmLong, ref DataSet ds7) // Put DBA data into arrarpart
    {
        int localvar = 0;

        for (localvar = 0; localvar < arrayNokiaPVDocmLong; localvar++)  // load in memory
        {
            arrayNokiaPVDocm[localvar + 1, 1] = "";
            arrayNokiaPVDocm[localvar + 1, 2] = "";
            arrayNokiaPVDocm[localvar + 1, 3] = "";
            arrayNokiaPVDocm[localvar + 1, 4] = "";
            arrayNokiaPVDocm[localvar + 1, 5] = "";
            arrayNokiaPVDocm[localvar + 1, 6] = "";
        }

        for (localvar = 0; localvar < arrayNokiaPVDocmLong; localvar++)  // load in memory
        {
            arrayNokiaPVDocm[localvar + 1, 1] = ds7.Tables[0].Rows[localvar]["Document_ID"].ToString();
            arrayNokiaPVDocm[localvar + 1, 2] = ds7.Tables[0].Rows[localvar]["plant_name"].ToString();  // CustomerSite
            arrayNokiaPVDocm[localvar + 1, 3] = ds7.Tables[0].Rows[localvar]["foxconnsite"].ToString(); // FoxconnSite
            arrayNokiaPVDocm[localvar + 1, 4] = ds7.Tables[0].Rows[localvar]["Forecast_CustomerPN"].ToString();  // CustomerPN
            arrayNokiaPVDocm[localvar + 1, 5] = ds7.Tables[0].Rows[localvar]["Forecast_PartnerGBI"].ToString();  // CustomerDun       

        }  // end load tabel in memory

    }

    private void GetNokiaEVDocmID(ref string[,] arrayNokiaEVDocm, ref int arrayNokiaEVDocmLong, ref string tmpNokiaEVDocm, ref string tmpCustomerDum, ref string tmpCustomerSite, ref string tmpFoxconnSite, ref string tmpCustomerPN) // Put DBA data into arrarpart
    {
        int localvar = 0;
        string s1, s2, s3;
        tmpCustomerDum = "";
        tmpNokiaEVDocm = "";

        for (localvar = 0; localvar < arrayNokiaEVDocmLong; localvar++)  // load in memory
        {
            s1 = arrayNokiaEVDocm[localvar, 2];
            s2 = arrayNokiaEVDocm[localvar, 3];
            s3 = arrayNokiaEVDocm[localvar, 4];
            if ((tmpCustomerSite == arrayNokiaEVDocm[localvar, 2]) && (tmpFoxconnSite == arrayNokiaEVDocm[localvar, 3]) && (tmpCustomerPN == arrayNokiaEVDocm[localvar, 4]))
            {
                tmpCustomerDum = arrayNokiaEVDocm[localvar, 5]; //  ds6.Tables[0].Rows[localvar]["Forecast_PartnerGBI"].ToString();  // CustomerDun       
                tmpNokiaEVDocm = arrayNokiaEVDocm[localvar, 1]; //  ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
                localvar = arrayNokiaEVDocmLong + 1; // break
            }
        }  // end load tabel in memory

    }

    private void GetNokiaPVDocmID(ref string[,] arrayNokiaPVDocm, ref int arrayNokiaPVDocmLong, ref string tmpNokiaPVDocm, ref string tmpCustomerDum, ref string tmpCustomerSite, ref string tmpFoxconnSite, ref string tmpCustomerPN) // Put DBA data into arrarpart
    {
        int localvar = 0;
        string s1, s2, s3;
        tmpCustomerDum = "";
        tmpNokiaPVDocm = "";

        for (localvar = 0; localvar < arrayNokiaPVDocmLong; localvar++)  // load in memory
        {
            s1 = arrayNokiaPVDocm[localvar, 2];
            s2 = arrayNokiaPVDocm[localvar, 3];
            s3 = arrayNokiaPVDocm[localvar, 4];
            if ((tmpCustomerSite == arrayNokiaPVDocm[localvar, 2]) && (tmpFoxconnSite == arrayNokiaPVDocm[localvar, 3]) && (tmpCustomerPN == arrayNokiaPVDocm[localvar, 4]))
            {
                tmpCustomerDum = arrayNokiaPVDocm[localvar, 5]; //  ds6.Tables[0].Rows[localvar]["Forecast_PartnerGBI"].ToString();  // CustomerDun       
                tmpNokiaPVDocm = arrayNokiaPVDocm[localvar, 1]; //  ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
                localvar = arrayNokiaPVDocmLong + 1; // break
            }
        }  // end load tabel in memory

    }

    private void GetDataInArrarPartCustomerPlant(ref string[,] arrayCustomerPlant, ref DataSet ds5) // Put DBA data into arrarpart
    {
        int localvar = 0;

        for (localvar = 0; localvar < CustomerPlantLong; localvar++)  // load in memory
        {
            arrayCustomerPlant[localvar + 1, 1] = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
            arrayCustomerPlant[localvar + 1, 2] = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
            arrayCustomerPlant[localvar + 1, 3] = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
            arrayCustomerPlant[localvar, 4] = localvar.ToString();
        }  // end load tabel in memor
    }

    private void GetDataInArrarPartGIT(ref string[,] arrayCustomerPlant, ref string[,] arrayGIT, ref DataSet ds4) // Put DBA data into arrarpart
    {
        int localvar2 = 0;
        int localvar3 = 0;
        int wriptrarrayGIT = 0;
        String tCus, tFox, tCusPN, tDocm, currDocm;
        string s1, s2, s3, s4, s5, s6, s7, s8;
        for (localvar2 = 0; localvar2 < arrayGITWriLong + 1; localvar2++)  // Initial array
        {
            arrayGIT[localvar2, 0] = "";
            arrayGIT[localvar2, 1] = "";
            arrayGIT[localvar2, 2] = "";
            arrayGIT[localvar2, 3] = "";
            arrayGIT[localvar2, 4] = "";
            arrayGIT[localvar2, 5] = "0";
            arrayGIT[localvar2, 6] = "";
            arrayGIT[localvar2, 7] = "";
            arrayGIT[localvar2, 8] = "";
            arrayGIT[localvar2, 9] = "";
            arrayGIT[localvar2, 10] = "";
            arrayGIT[localvar2, 11] = "";     // Delete Flag 20100312
            arrayGIT[localvar2, 12] = "0";    // Org DElivery_QTy

        }  // end load tabel in memory

        localvar2 = 0;                        // Init First Record
        tDocm = ds4.Tables[0].Rows[localvar2]["Document_ID"].ToString();
        tCus = ds4.Tables[0].Rows[localvar2]["Nokia_DUNS"].ToString();
        tFox = ds4.Tables[0].Rows[localvar2]["Foxconn_Site"].ToString();
        tCusPN = ds4.Tables[0].Rows[localvar2]["CUS_Materilano"].ToString();

        wriptrarrayGIT = 1;
        for (localvar2 = 0; localvar2 < arrayGITLong; localvar2++)  // load in memory
        {
            // arrayGIT[localvar2 + 1, 2] replace by arrayGIT[wriptrarrayGIT, 2] 20100513
            text4 = var1.ToString();
            arrayGIT[wriptrarrayGIT, 1] = ds4.Tables[0].Rows[localvar2]["Document_ID"].ToString();
            arrayGIT[wriptrarrayGIT, 2] = ds4.Tables[0].Rows[localvar2]["Nokia_DUNS"].ToString();
            arrayGIT[wriptrarrayGIT, 3] = ds4.Tables[0].Rows[localvar2]["Foxconn_Site"].ToString();
            arrayGIT[wriptrarrayGIT, 4] = ds4.Tables[0].Rows[localvar2]["CUS_Materilano"].ToString();
            arrayGIT[wriptrarrayGIT, 5] = ds4.Tables[0].Rows[localvar2]["Delivery_Qty"].ToString();
            arrayGIT[wriptrarrayGIT, 6] = ds4.Tables[0].Rows[localvar2]["Delivery_Date"].ToString();
            arrayGIT[wriptrarrayGIT, 7] = ""; // CustomerSide
            arrayGIT[wriptrarrayGIT, 8] = ds4.Tables[0].Rows[localvar2]["IN_DATE"].ToString();  // -1
            arrayGIT[wriptrarrayGIT, 9] = var1.ToString();
            arrayGIT[wriptrarrayGIT, 10] = ds4.Tables[0].Rows[localvar2]["Delivery_Number"].ToString();
            arrayGIT[wriptrarrayGIT, 11] = "";
            arrayGIT[wriptrarrayGIT, 12] = ds4.Tables[0].Rows[localvar2]["Delivery_Qty"].ToString(); // Bbackup Org Qty

            if (arrayGIT[wriptrarrayGIT, 2] == arrayGIT[wriptrarrayGIT - 1, 2])
                arrayGIT[wriptrarrayGIT, 7] = arrayGIT[wriptrarrayGIT - 1, 7];
            else
            {
                for (localvar3 = 1; localvar3 < CustomerPlantLong + 1; localvar3++)
                {
                    if (arrayGIT[wriptrarrayGIT, 2].ToString() == arrayCustomerPlant[localvar3, 2].ToString())  // fine Plant_code if Nokia_DUNS = Plant_Code
                        arrayGIT[wriptrarrayGIT, 7] = arrayCustomerPlant[localvar3, 3];                         // CustomerSide                

                }
            }

            if (arrayGIT[wriptrarrayGIT, 7] == "")
                text5 = "Error CotomerPlant";

            if ((arrayGIT[wriptrarrayGIT, 3].ToString() == "BJ") && (arrayGIT[wriptrarrayGIT, 4].ToString() == "0251644") && (arrayGIT[wriptrarrayGIT, 2].ToString() == "401307301"))
            {
                text4 = text4;
            }


            s1 = arrayGIT[wriptrarrayGIT, 1];
            s2 = arrayGIT[wriptrarrayGIT, 2];
            s3 = arrayGIT[wriptrarrayGIT, 3];
            s4 = arrayGIT[wriptrarrayGIT, 4];
            s5 = arrayGIT[wriptrarrayGIT, 5];
            s6 = arrayGIT[wriptrarrayGIT, 6];

            if ((arrayGIT[wriptrarrayGIT, 2].ToString() == tCus) && (arrayGIT[wriptrarrayGIT, 3].ToString() == tFox) && (arrayGIT[wriptrarrayGIT, 4].ToString() == tCusPN))
            {
                currDocm = arrayGIT[wriptrarrayGIT, 1].ToString();
                // if (arrayGIT[wriptrarrayGIT, 1].ToString() != tDocm)
                //    arrayGIT[localvar2 + 1, 11] = "D"; // 此筆無效
                if (arrayGIT[wriptrarrayGIT, 1].ToString() == tDocm)  // 此資料有效
                    wriptrarrayGIT = wriptrarrayGIT + 1;
            }
            else
            {
                tDocm = arrayGIT[wriptrarrayGIT, 1].ToString();
                tCus = arrayGIT[wriptrarrayGIT, 2].ToString();
                tFox = arrayGIT[wriptrarrayGIT, 3].ToString();
                tCusPN = arrayGIT[wriptrarrayGIT, 4].ToString();
                wriptrarrayGIT = wriptrarrayGIT + 1; //  // 此資料有效
            }

        }  // end load tabel in memory

        arrayGITWriLong = wriptrarrayGIT;

    }


    //// Sort anf delete for one record only for each day.
    //   arrayCustomerFoxconnPNToOneSet[var4, 1] =  CustomerSite  
    //   arrayCustomerFoxconnPNToOneSet[var4, 2] =  FoxconnSite
    //   arrayCustomerFoxconnPNToOneSet[var4, 3] =  CustomerPN
    //   arrayCustomerFoxconnPNToOneSet[var4, 4] =  ??  //  Array Loc
    //   arrayCustomerFoxconnPNToOneSet[var4, 5] = (10000 + var4)             // 為 Key當Index值, 用此對照所有資料 到 array 13 Upload
    //   arrayCustomerFoxconnPNToOneSet[var4, 6] = "";                        // DV 最早一天
    //   arrayCustomerFoxconnPNToOneSet[var4, 10] = "11";                     // next write pointer
    //   arrayCustomerFoxconnPNToOneSet[var4, 11] = "1", "2", "3",            // 實際 arrayEtdUpload pointer 
    //   20100422
    //   arrayCustomerFoxconnPNToOneSet[var4, 0] =  Project  
    //   arrayCustomerFoxconnPNToOneSet[var4, 7] =  Description  
    //   arrayCustomerFoxconnPNToOneSet[var4, 8] =  H.H P/N  
    //   arrayCustomerFoxconnPNToOneSet[var4, 9] =  Dom_Exp  

    /////////////////////////////////////////////////////////////////////////////////
    // 找 arrayGIT 中使用 IN_DATE 開始, 後為 Delivery_Day + LeadTime to arraytransDay
    // 1. GIT_Info process 1.1 依目前 Currency Date 為主 
    // 2. 目前為明細對帳表, 每筆明細為出貨單號 Delivery_number
    // 3. Customersite+FoxconnSite+PN+Delivery_Number+最後一筆 ( Document_ID 為 Key )
    // 4. 以上面為 unquire, 前面須寫入時只有一筆, 所以不必管以前重覆問題
    //
    ////////////////////////////////////////////////////////////////////////////////
    // Nokia_DUNS, Foxconn_DUNS, Part_Number, Delivery_Qty, Delivery_Date
    private void Cal_arrayGITLongAndDeliveryQty(ref string tmpLeadTime, ref int arrayGITLong, ref int arrayGITWriLong, ref string tmpCustomerSite, ref string tmpFoxconnSite, ref string tmpCustomerPN, ref int MaxMimDVLong, ref string[,] arrayGIT, ref string[,] arraytransDay)
    {
        int locv1 = 0;
        int locv2 = 0;
        int tmpGITloc = 0;

        string locs1 = "";
        string tcuspn = "";

        var4 = Convert.ToInt32(tmpLeadTime);
        tmpstr1 = "";

        if ((tmpFoxconnSite == "BJ") && (tmpCustomerPN == "0251644") && (tmpCustomerSite == "Komarom"))
        {
            tmpstr1 = tmpstr1;
        }

        for (var1 = 1; var1 < arrayGITWriLong + 1; var1++)  // 開始從資料庫 GIT_Info Loop 第一筆 SPdate  
            if ((tmpCustomerSite == arrayGIT[var1, 7].ToString()) && (tmpFoxconnSite == arrayGIT[var1, 3].ToString()) && (tmpCustomerPN == arrayGIT[var1, 4].ToString()) && ("D" != arrayGIT[var1, 11].ToString())) // 找此 Customer+Foxconn+PN
            {
                tmpGITloc = var1;                // find 
                var1 = var1 + arrayGITWriLong + 1;  // find loc and leave
            }

        if (tmpGITloc > 0)  // find 
        {
            for (var1 = tmpGITloc; var1 < arrayGITWriLong + 1; var1++)  // 開始從資料庫 GIT_Info Loop 第一筆 SPdate  
            {
                if ((tmpCustomerSite == arrayGIT[var1, 7].ToString()) && (tmpFoxconnSite == arrayGIT[var1, 3].ToString()) && (tmpCustomerPN == arrayGIT[var1, 4].ToString()) && ("D" != arrayGIT[var1, 11].ToString())) // 找此 Customer+Foxconn+PN
                {   // 符合 3 Condition NokiaSite, FoxconnSite, PN

                    tcuspn = arrayGIT[var1, 1];  // trace only
                    text3 = arrayGIT[var1, 5].ToString();                       // Delivery_Qty
                    text4 = arrayGIT[var1, 6].ToString();                       // Delivery_date 新位置為 delivery_date+leadtime, tmpLeadTime
                    text3 = ShipPlanlibPointer.TrsStrToInteger(text3);
                    locv1 = ShipPlanlibPointer.GetarraytransDayLoc(arrayGIT[var1, 6].ToString(), arraytransDay[FirstDVLoc, 3], FirstDVLoc);
                    locv2 = locv1 + Convert.ToInt32(tmpLeadTime);  // New GIT Loc + LeadTime
                    if ((locv2 > 400) || (locv2 < 1)) return;  // error 

                    arraytransDay[locv1, 31] = (Convert.ToInt32(text3) + Convert.ToInt32(arraytransDay[locv1, 31])).ToString();  // GIT 原來數量放在 Arr[31] 位置
                    if (arraytransDay[locv1, 32].ToString() != "") arraytransDay[locv1, 32] = arrayGIT[var1, 1].ToString() + "(2)"; // it mean two times. 
                    else arraytransDay[locv1, 32] = arrayGIT[var1, 1].ToString(); // Docuemtn_ID 20100522


                    // tmpstr1 = arraytransDay[locv1, 31];  // GIT 原來數量放在 Arr[31] 位置
                    // tmpstr2 = arraytransDay[locv1, 32];       // Docuemtn_ID 20100522

                    tmpstr1 = arraytransDay[locv2, 3].ToString();         // Trace Only
                    tmpstr2 = arraytransDay[locv2, 6].ToString();         // 目前var3 +LeadTimevar4 = 新位置Qty Save
                    arraytransDay[locv2, 6] = (Convert.ToInt32(text3) + Convert.ToInt32(tmpstr2)).ToString();          // 將 GIT 數量 + LeadTime + 原來數量放在 Arr[6] 位置
                    arrayGIT[var1, 11] = arraytransDay[locv2, 6];         // delele falg 為寫入新數據 For Trace 20100317
                    text5 = arraytransDay[locv2, 6].ToString();
                    GITUpdateNumber++;                                         // check count how many GIT Update

                } // if end 
                else var1 = var1 + arrayGITWriLong + 1; // break                   

            }  // for end
        }

        tmpstr1 = "";
        //////////////////////////////////////////////////////////////////////////////////////////
        // 將 GIT array 6 前抓未超過今天 qty 加到今天, 超過今天, 加 LeadTime 回寫   20100414
        //////////////////////////////////////////////////////////////////////////////////////////
        var3 = tmptodaylocation;
        for (var1 = 1; var1 < tmptodaylocation; var1++)  // 將 GIT array 6 前抓未超過今天 qty 加到今天 array 7,
        {
            text2 = arraytransDay[var3, 7];  // trace only
            text2 = arraytransDay[var1, 6];  // trace only
            if (Convert.ToInt32(arraytransDay[var1, 6]) > 0) // 未測
            {
                arraytransDay[tmptodaylocation, 7] = (Convert.ToInt32(arraytransDay[tmptodaylocation, 7]) + Convert.ToInt32(arraytransDay[var1, 6])).ToString();  // 超過今天的 GIT 數量 放在今天 Arry 7 
                arraytransDay[var1, 6] = "0"; // 將 GIT 今後移到今天, 並清除今後
            }
        }

        // for (var1 = BefreDVLoc100 + 1; var1 < arraytransDayfixLong400; var1++) // 最後合併 5+6+7 = 8 20100317
        for (var1 = 1; var1 < arraytransDayfixLong400; var1++) // 最後合併 5+6+7 = 8 
            arraytransDay[var1, 8] = (Convert.ToInt32(arraytransDay[var1, 5]) + Convert.ToInt32(arraytransDay[var1, 6]) + Convert.ToInt32(arraytransDay[var1, 7])).ToString();

        ///////////////////////////////////////////////////////////////
        // C+2 以後將每周加到周一, 從 tmptodaylocation 為今天位置算起 
        // if WeekofDay = "0" then 21-7 else 21 - WeekofDay 
        // <-----------|---------|---------|----------------------------
        //     |Today  |  C+1    |   C+2   | C +3  
        ///////////////////////////////////////////////////////////////
        // var3 = (arraytransDayfixLong400 - tmptodaylocation) / 7 - 4; // 預計幾周,減前3周 
        // var1 = 0; // 為 C+2 到那個 Array Location
        // var2 = 0;
        // var4 = 0;

        for (var1 = 1; var1 < tmpC3location; var1++) arraytransDay[var1, 11] = "Day";

        for (var1 = 0; var1 < 4; var1++)         // 前面 C+3 前也要 PV
        {
            // var2 = tmpC3location + var1 * 7;   // 20100513
            var2 = FirstPVLoc + var1 * 7;         // 20100513
            // arraytransDay[var2, 11] = "Week";     // 從 C+3 (var2 ) 開始每周1-7 加到星期1, var2 自動增加 
            arraytransDay[var2, 9] = Convert.ToString(Convert.ToInt32(arraytransDay[var2 + 0, 8]) + Convert.ToInt32(arraytransDay[var2 + 1, 8]) + Convert.ToInt32(arraytransDay[var2 + 2, 8]) + Convert.ToInt32(arraytransDay[var2 + 3, 8]) + Convert.ToInt32(arraytransDay[var2 + 4, 8]) + Convert.ToInt32(arraytransDay[var2 + 5, 8]) + Convert.ToInt32(arraytransDay[var2 + 6, 8])); // 周一加到日放進周一
            text2 = arraytransDay[var2, 9];  // trace only
        }

        for (var1 = 0; var1 < 19; var1++)         // run var3 time add all to C+3 = 星期一
        {
            var2 = tmpC3location + var1 * 7;   // 20100513                     
            arraytransDay[var2, 11] = "Week";      // 從 C+3 (var2 ) 開始每周1-7 加到星期1, var2 自動增加 
            arraytransDay[var2, 9] = Convert.ToString(Convert.ToInt32(arraytransDay[var2 + 0, 8]) + Convert.ToInt32(arraytransDay[var2 + 1, 8]) + Convert.ToInt32(arraytransDay[var2 + 2, 8]) + Convert.ToInt32(arraytransDay[var2 + 3, 8]) + Convert.ToInt32(arraytransDay[var2 + 4, 8]) + Convert.ToInt32(arraytransDay[var2 + 5, 8]) + Convert.ToInt32(arraytransDay[var2 + 6, 8])); // 周一加到日放進周一
            text2 = arraytransDay[var2, 9];  // trace only
        }


    }  // Cal_arrayGITLongAndDeliveryQty

} // end this class

   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   // end my programmer 20200310
   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Programming Spec Ken Lin 20100219
// FlowChart :
// 1.1 矩陣資料庫讀取會從 0 位置開始,  ds1.Tables[0].Rows.Count 回應為實際長度數字, 不含 0 , 我設計矩陣計算從1 所以資料矩陣須加 1
// 1.2 抓取資基本資料,料號主檔 Syncro_Foxconn_Nokia_partNo 指標 ds1,sql1資料長度為 PartUploadETDrecordLong 矩陣 arrayPart[PartUploadETDrecordLong+1, 20 + 1]
// 1.3 抓取資基本資料,客戶編號 Customer_Plant ds5,sql5 資料長度為 CustomerPlantLong, 矩陣 arrayCustomerPlant[CustomerPlantLong, 4 + 1]  
// 1.4 抓取資在途量 GIT_Infont ds4,sql4 資料長度為 CustomerPlantLong, 矩陣 arrayGIT[arrayGITLong+1, 13 + 1]  
//     1.4.1 在途量為已出貨, 但客戶未收到, 所以用將 Sap 出貨資料取得, Nokia DV 回覆已收料, 相減為在途量, 
//     1.4.2 目前為明細對帳表,每筆明細為出貨單號 Delivery_number,Customersite+FoxconnSite+PN+Delivery_Number+最後一筆(Document_ID 為 Key),
//           可能分批出貨, 所以可能會出現同料號好幾筆, 只是Document_ID不同, 須找最新一筆.
//     1.4.3 前面程式須Update 資料庫同一客戶+公司+料號 為最新 GIT 數量, 可能 2筆已上, 表不同出貨單號, 改成只有一筆罪新極可.
// 1.5 開始 迴圈 Loop, 每個 DocumentID 為單一處理, ReqPorcETDToETA
//     CHekc 資料庫, 入 FinishedDate = Space , 就處理完並回寫 FinishedDate 今天日期表示完成
//     1.5.1 將 Users Upload file ETD_SP_Upload, sql3, ds3 讀入此 DocumentID 資料處理 arrayEtdUpload=[UploadETDrecordLong+1,21];  
//           將此 DocumentID 為處理資料, 依 Customersite+Foxconnsite+CustomerPN 為一組, 逐一計算, 先產生在
//           arrayCustomerFoxconnPNToOneSet[arrayCustomerFoxconnPNToOneSetLong, var4];[600,4]
//           Get Customersite+Foxconnsite+CustomerPN 建力一個 SubSet 為 Key 
//           // 每個為單一 Module to Running
//           //
//           // X  CustomerSide  --------------------------------- (600)
//           // Y  FoxconnSide   |
//           //    CustomerPN    |
//           //                 (5)
//           ////////////////////////////////////////////////////////////////////////
//           // Algorithm : 建一 arrayCustomerFoxconnPNToOneSet Array, 從頭開始將 arrayEtdUpload 讀入與 Array 資料比較 Check , 
//           //             如果相同換 arrayEtdUpload  下一筆, 不同 Check 下一 Array 位置, 如果Array為空即填入 
//           //             if A==b and C==D then break
//           //             if A<>B or C<>D then next
//           //             if A=spce then Write and break (最後 eachIDSetCount 為此 Array arrayCustomerFoxconnPNToOneSet Array數量)
//     1.5.2 建 Array arrayDVByDocmID_Cust_Fox_PN[arrayDVByDocmID_Cust_Fox_PNLong+1, var4][500,16] 為 DV 最後DV處理單位
//     1.5.3 建立計算結果 arraytransDay[arraytransDayfixLong400, 16] 預計在 loop1 中 從 arrayDVByDocmID_Cust_Fox_PN 讀入後計算.   
//     1.5.4 loop for arrayCustomerFoxconnPNToOneSet 逐筆完成 ( eachIDSetCount 筆數)      
//           1.5.4.1 將原始 arrayEtdUpload 依 CustomerSite+FoxconnSite+CustomerPN 讀入最後處理單位 arrayDVByDocmID_Cust_Fox_PN
//           1.5.4.2 取的 arrayDVByDocmID_Cust_Fox_PN 長度, 最大, 最小, 日期, 及 最大最小日期間距    
//           1.5.4.3 BubbleSort( Para1, Para2 ) ( Number, Table Pointer ( 依 SPdate Sort ) This is ( Not Must )
//           1.5.4.4 依 CustomerSite+FoxconnSite+CustomerPN 從料號中 arrayPart 並取的 LeadTime 等資料    
//                   做一新矩陣  arraytransDay
//             100            MaxMimDVLong                   Extend
//       1-------------- 101 -------------------------------------> <---------------------------->400
// < LeadTime>      <最小DV日期>           < DV >             <最大DV日期>  <Toatl 400 Array>
//
//                        < ------------ Currency Day ------------->
//
// ( X, Y ) X: 為 arraytransDay 開始計算並進入天數 
//
//  1. 建立一個矩陣  400  1-100 為前100 天 (留給前置時間 LeadTime), 101 為 DV 最小天日, 
//  2. DV 前後相格為 MaxMimDVLong 
// 
//   X                    --------------------------------------------------------- (400)
//   Y   Account Number(1)| 1 2 3 4 5 6 7 8 9 .... 101 ( DV 最小天, 不一定是今天....
//       SPDate (2)       |
//       SPDate "YYYYMMDD |(3)
//       Org SPQty (4)    |
//  Add Leadtime SPQty(5) |
//  Add Leadtime GIT (6)  |        
//  > Today GIT move today| (7)   
//       New SPQty(8)     | 5+6+7 = 8
//       C+2 (9)          | 合併 C+2  C+3 後到周一
//       WeekofDay(10)    | 記錄第幾天, 周日為 0 
//       Day/Week (11)    | 計錄回寫 Day or Week 
//       ReleaseDate (12) |
//       SPWeek (13)      | 記錄第幾周  
//       DocumentID (14)  |   
//       ETDSPQty   (15)  |  
//       CustomerSite(16) |    
//       FoxconnSite(17)  |
//       CustomerPN(18)   |
//                       (20)
//      tmpCurrent_Dos(21) 
//      tmpNext_Dos(22)
//      tmpGIT_Dos(23)
//      tmpMPQ(24)
//      tmpLeadTime(25)  
//



//  1. 起始日為 MinStr, 放在 Array 101,前100 格給 LeadTime用 101開始 DV後每格加一日,長度 MaxMimDVLong 
//           1.5.4.5 將 arrayDVByDocmID_Cust_Fox_PN ( MaxMinLong )  讀入 arraytransDay 開始計算
//                   讀 arrayDVByDocmID_Cust_Fox_PN, 逐一寫入 arraytransDay, 因 arraytransDay 是依照日期時間產生,
//                   所以只要 arrayDVByDocmID_Cust_Fox_PN SPDate 相同,即可加入 SPQty 
//                   if ( swdayWeek == Week ) then SPQty/7, 前6天為 1/7 最後一天為 SPQty- 6/7 esle add SPQty 
//           1.5.4.6 將所有資料 Array 101 Start 加上 SPQty SPDate+Leadtime 日期(在途時間) 往後移 從 Array[4] to Array[5], 並加入原有得 Array SPQty
//           1.5.4.7 將 GIT SPQty 加上 SPDate+Leadtime 日期(在途時間) 往後移 從 Array[5] to Array[6], 並加入原有得 Array SPQty
//                   從 arrayGIT逐一讀出此 Cust+Fox+PN,依日期找到 arraytransDay位置後加上 SP_Date+LeadTime 將SPQty 加入 [6] ]
//           1.5.4.8 GIT SPDate+LeadTime 比今天日期大的移動今天 > Today GIT move today array[6] 到 array[7], 並清除 array[6]
//                   今天減 DV 第一天 (MinStr array[100]), 得到數字即為今天第幾個array 位置, tmptodaylocation,
//                   從這個位置為今天日期, 往後算 GIT 有數字移到今天 array[6] to array[7]   
//           1.5.4.9 最後合併 array[5]+[6]+[7] = array[8]
//           1.5.4.A C+3 開始 SPQty 加到星期一, 先找到 C+3 (tmpC3location) 開始星期一位置, 21 - 今天 =  C+2 天數,  C+3 以後, 
//                   每周會總到一天 array[9]
//           1.5.4.B 寫到資料庫 C+2 以前是每天, C+3 以後是每周一天
// SELECT [DocumentID]                1. Upload ID 號, 應是日期
//       ,[CustomerSite]              2. 客戶   Site
//       ,[FoxconnSite]               3. 本公司 Site
//       ,[CustomerPN]                4. 客戶料號
//       ,[AccountNum]                5. 矩陣流水號
//       ,[SPDate_8Bytes]             6. DV 日期為 YYYYMMDD
//       ,[ETD_SPQty]                 7. DV 原始數據
//       ,[Org_Spilt_SPQty]           8. DV 分為日及周分為日
//       ,[Leadtime_SPQty]            9. DV 加上Leadtime 後新數據位置  
//       ,[Leadtime_GIT_SPQty]       10. GIT加上Leadtime 後新數據位置  
//       ,[TodayGIT_SPQty]           11. 超過今日 GIT 轉為今日
//       ,[SumSPQty]                 12. 合計 12 = 9+10+11 
//       ,[SumC3]                    13. 合計 C+3 周後到每周一
//       ,[EVWeekOfDay]              14. 周幾 ? 
//       ,[SWWeekDay]                15. Day or Week 
//       ,[ReleaseDate]              16. 轉上日期, 依此分先後
//       ,[SPWeek]                   17. 年度第幾周 ?
//       ,[SPDate]                   18. SPDate 為 DeadTime
//
//
//
//
//
//
//
//
    
