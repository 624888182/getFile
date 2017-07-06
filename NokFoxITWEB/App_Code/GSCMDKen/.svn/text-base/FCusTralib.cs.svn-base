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
using Economy.Publibrary;
using SCM.GSCMDKen;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;

namespace SCM.GSCMDKen
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class FCusTralib
{
    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd"); 
    public static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    // FSplitlib FSplitlibPointer = new FSplitlib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();

    public string CustomTraSE3(string iRunDate, int iDayCnt, string iDBFPath)
    {
        int v1 = 0;
        string s1 = "", DownDVDay="";
        if (iRunDate == "") iRunDate = CuurDate;
        // if (iDayCnt  <= 0 )   iDayCnt = 1;
        // if (iDBFPath != "")   RunDBPath = iDBFPath;

        DownDVDay = iRunDate;
        for (v1 = 0; v1 <= iDayCnt; v1++)
        {
            DownDVDay = (Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(DownDVDay))).AddDays(-v1).ToString("yyyyMMdd");
            s1 = CustomTraSE(DownDVDay, iDayCnt, iDBFPath); 
        }
          
        return ( s1);
    }
    public string CustomTraSE(string RunDate, int DayCnt, string DBFPath)
	{
        if ( RunDate == "" )    RunDate   = CuurDate;
        if ( DayCnt <= 0   )    DayCnt    = 1;
        if (DBFPath != "")      RunDBPath = DBFPath;
        
        string sqlsplit = "", sqlinsertsplit="";
        string subtmpstr1 = "";
        string subtmpstr2 = "";
        string subtmpstr3 = "";
        string subtmpstr4 = "";
        string s0 = "", s11 = "", s12 = "", s13 = "", s14 = "", s15 = "", s16 = "", s17 = "", s18 = "";
        string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", s9 = "", s10 = "";
        string s19 = "", s20 = "", s21 = "", s22 = "", s23 = "", s24 = "", s25 = "", s26 = "", s27 = "", s28="", stmp = "";
        int localvar1 = 0;
        int localvar2 = 0;
        int localvar3 = 0;
        int localvar4 = 0;
        int localVarX = 0;
        int Long4A3_Detail_PNOneSet = 0;
        Double d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0, d7 = 0, d8 = 0;

        string localstr1 = "";
        string localstr2 = "";
        string localstr3 = "";
        string localstr4 = "";
        string localstr5 = "";
        string localstr6 = "";
        string localstr7 = "";
        string localstr9 = "";
        string localstr12 = "";
        string localstr13 = "";
        string localstr14 = "";
        string localstr15 = "";
        string localstr16 = "";
        string localstr17 = "";
        string localstr18 = "";
        string localstr19 = "";
        string localstr20 = "";
        string localstr21 = "";
        string localstr22 = ""; 
        string localstr23 = "";
        string localstr24 = "";
        string localstr25 = "";

        string s111, s222, s333, s444, s666, strtmp="";
        
        // int Main4A3Long = 0;
        // int Parter4A3Long = 0;
        DateTime locdate1 = DateTime.Today;

        string tmpReleaseYear, tmpReleaseDate, tmpDocumentID, tmpSPWeek, PreDownDVDay, DownDVDay;

        tmpReleaseYear = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        tmpReleaseDate = tmptoday.ToString("yyyyMMdd");
        tmpDocumentID = tmptoday.ToString("yyyyMMdd");

        subtmpstr2 = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
        localvar1 = Convert.ToInt32(tmptoday.DayOfYear);
        localvar2 = Convert.ToInt32(tmptoday.DayOfWeek);

        subtmpstr1 = ShipPlanlibPointer.getWeekofthisYear(1, localvar1, localvar2, subtmpstr2, tmptoday);
    
        tmpSPWeek = subtmpstr1;  // 2010-WK51 --> 2010-W51
        localvar1 = tmpSPWeek.Length;
        tmpSPWeek = subtmpstr1.Substring(0, 6) + subtmpstr1.Substring(7, localvar1-7);  
        // DownDVDay = textBox1.Text.Substring(0, 4) + textBox1.Text.Substring(5, 2) + textBox1.Text.Substring(8, 2); // textBox1.Text = DateTime.Today.ToString("yyyy-MM-dd");
        DownDVDay = RunDate;
        PreDownDVDay = DownDVDay; 

        // PreDownDVDay = (Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(DownDVDay))).AddDays(-RunDays).ToString("yyyyMMdd");
              
        // 20110102 WriteCount = 0;
        // 20110102 if (textBox8.Text != "") WriETAarraytransDayflag = Convert.ToChar(textBox8.Text.Substring(0, 1));

        string ErrMsg="";
        
        int NCLong=0;
        s1 = " Select * from Nokia_Calendar ";    
        DataSet dsNC = LibSCM1Pointer.GetDataByDataPath(s1, RunDBPath);
        NCLong = dsNC.Tables[0].Rows.Count;  // Real how many record in this table
        if ( NCLong == 0)
           ErrMsg = "It can not find any data in  Syncro_Foxconn_Nokia_PartNoLong";
      
        string[,] arrayNokiaCalendar = new string[NCLong + 1, 10 + 1];
        GetDataInArrayNokiaCalendar(ref arrayNokiaCalendar, ref dsNC, ref NCLong);
        strtmp    = ShipPlanlibPointer.GetNokiaCalendar(RunDate, arrayNokiaCalendar, NCLong);
        if ( ( strtmp != "" ) && ( strtmp != null ) )
               tmpSPWeek = ShipPlanlibPointer.GetNokiaCalendar( RunDate, arrayNokiaCalendar, NCLong);
        localvar1 = tmpSPWeek.Length;
        tmpSPWeek = tmpSPWeek.Substring(0, 6) + tmpSPWeek.Substring(7, localvar1 - 7);  // Cut 8 Code

        if ( tmpSPWeek.Length > 8 )    tmpSPWeek = tmpSPWeek.Substring(0, 8);

        int Foxconn_Nokia_PartNoLong = 0;

        string sql51 = "select * from Syncro_Foxconn_Nokia_PartNo order by NokiaSite, NokiaPartNo, Project ";  // table error need update
        DataSet dsPart = LibSCM1Pointer.GetDataByDataPath(sql51, RunDBPath); // DataSet ds5 = DataConnlib.Get_InfoByPara(sql5);
        Foxconn_Nokia_PartNoLong = dsPart.Tables[0].Rows.Count;  // Real how many record in this table
        if ( Foxconn_Nokia_PartNoLong == 0)
           ErrMsg = "It can not find any data in  Syncro_Foxconn_Nokia_PartNoLong";

        string[,] arrayFoxconn_Nokia_PartNo = new string[Foxconn_Nokia_PartNoLong + 1, 15 + 1];
        GetDataInArrarFoxconn_Nokia_PartNo(ref arrayFoxconn_Nokia_PartNo, ref dsPart, ref Foxconn_Nokia_PartNoLong); // Put DBA data into arrarpart


        ErrMsg = "";
        int CustomerPlantLong = 0;
        string sql5 = "select * from Customer_Plant ";  // table error need update
        DataSet ds5 = LibSCM1Pointer.GetDataByDataPath(sql5, RunDBPath); // DataSet ds5 = DataConnlib.Get_InfoByPara(sql5);
        CustomerPlantLong = ds5.Tables[0].Rows.Count;  // Real how many record in this table
        if (CustomerPlantLong == 0)
        { //    Response.Write("<script>alert('select * from Customer_Plant失敗，請檢查後重試！')</script>");
            ErrMsg = "It can not find any data in  Customer_Plant, Call Operator";
            return("-1");
        }

        string[,] arrayCustomerPlant = new string[CustomerPlantLong + 1, 10 + 1];
        GetDataInArrarPartCustomerPlant(ref arrayCustomerPlant, ref ds5, ref CustomerPlantLong); // Put DBA data into arrarpart

        //////////////////////////////////
        // Start run 4A3_Detail EV

        string sql4A3_Detail_PNOneSet = " Select * from Syncro_4A3_Detail_PNOneSet where ( Substring(DocumentTime,1,8) <= '" + DownDVDay + "' "  // where ( documenttime like '" + DownDVDay + "'+'%' )"
        + " and Substring(DocumentTime,1,8) >= '" + PreDownDVDay + "' ) order by FoxconnRegion, CustomerSite, Forecast_CustomerPN, Document_ID ";
        DataSet ds4A3_Detail_PNOneSet = LibSCM1Pointer.GetDataByDataPath(sql4A3_Detail_PNOneSet, RunDBPath);  // All Data Not only EV
        if (ds4A3_Detail_PNOneSet.Tables.Count <= 0) Long4A3_Detail_PNOneSet = 0; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        else
            Long4A3_Detail_PNOneSet = ds4A3_Detail_PNOneSet.Tables[0].Rows.Count;
        
        int GetDataSqlLong=0, UploadETDrecordLong=0, EVLong=0;

          
    // sqlsplit = " Select top(2000) D.* , "
    sqlsplit = " Select  D.* , "
    + " P.Forecast_PartnerGBI CustomerSiteNew, R.to_GBI FoxconnRegionNew, M.Document_Time Document_TimeNew, "
    + " M.Forecast_Time Forecast_TimeNew, A.Forecast_ReferenceLine CItem,  A.Forecast_ReferenceNumber CAgreement"
    + " From Syncro_4A3_Detail D left join Syncro_4A3_Reference A on D.Document_ID = A.Document_ID and "
    + " D.Forecast_CustomerPN = A.Forecast_CustomerPN, Syncro_4A3_Partner P, Syncro_4A3_Main M, Syncro_4A3_role R "
    + " Where D.Document_ID = M.Document_ID and D.Document_ID = P.Document_ID and  D.Document_ID = R.Document_ID "
    + " and   M.Document_ID = P.Document_ID and M.Document_ID = R.Document_ID and  P.Document_ID = R.Document_ID "
    + " and ( Substring( M.document_time,1,8) <= '" + DownDVDay + "' ) "
    + " and ( Substring( M.document_time,1,8) >= '" + PreDownDVDay + "' ) "
    + " And Substring([Forecast_QtyTypeCode],1,8)='Discrete' "
    + " And ( D.ReplyNokia_ID is NULL or D.ReplyNokia_ID = '') "  // ReplyNokia_ID = Ticket
    + " order by FoxconnRegionNew, CustomerSiteNew, D.Forecast_CustomerPN, D.Document_ID ";
        DataSet dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // All Data Not only EV
        if (dssplit.Tables.Count <= 0) localvar1 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        else
        {
            localvar1 = dssplit.Tables[0].Rows.Count;
            GetDataSqlLong = dssplit.Tables[0].Rows.Count;
            UploadETDrecordLong = GetDataSqlLong;
            EVLong = UploadETDrecordLong;
        }


        ////////////////////////////////////////////
        // Set Arround for 120000 Summary to 5000
        // var3 = ( ( EVLong / 120000 ) + 1 ) * 5000;
        // if ( var3 > arrayCustomerFoxconnPNToOneSetLong ) arrayCustomerFoxconnPNToOneSetLong = var3; // 找到最大會總數

        int var1 = 0, var2 = 0, var3 = 0, var4 = 0, var5 = 0, var6 = 0, var7=0, var8=0, IndexArrayLoc=10;
        int arrayCustomerFoxconnPNToOneSetLong = 60000, arrayCustomerFoxconnPNToOneSetIndex = 80;
        string[,] arrayCustomerFoxconnPNToOneSet = new string[arrayCustomerFoxconnPNToOneSetLong, arrayCustomerFoxconnPNToOneSetIndex + 1];
        var3 = arrayCustomerFoxconnPNToOneSetLong;

        for (var4 = 0; var4 < var3; var4++)
        {
            for (var5 = 0; var5 < arrayCustomerFoxconnPNToOneSetIndex + 1; var5++) arrayCustomerFoxconnPNToOneSet[var4, var5] = "";

            arrayCustomerFoxconnPNToOneSet[var4, 5] = (10000 + var4).ToString();     // 為 Key當Index 到 array 13 Upload
            arrayCustomerFoxconnPNToOneSet[var4, 6] = tmptoday.ToString("yyyyMMdd"); // DV 最早一天
            arrayCustomerFoxconnPNToOneSet[var4, 7] = tmptoday.ToString("yyyyMMdd"); // DV 最晚一天
            arrayCustomerFoxconnPNToOneSet[var4, 8] = "0";                           // Summary GIT, Stock, Consigned
            arrayCustomerFoxconnPNToOneSet[var4, IndexArrayLoc] = "11";              // loc 10 記錄下一位, 11放實際 DTDUpload Loc 
            arrayCustomerFoxconnPNToOneSet[var4, 15] = "0";                          // EV Total             

        }   // End InitializeCulture space
        
              
        int DelpcateEVNum = 0, eachIDSetCount = 0;
        localvar1 = 0;
        localvar2 = 0;
        localvar3 = 0;  
        s1 = ""; s2 = ""; s3 = ""; s6 = "";
        
        for (var1 = 0; var1 < dssplit.Tables[0].Rows.Count; var1++)
        {
            localstr5 = dssplit.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString();

            if (localstr5 == "Discrete Gross Demand")  // Delete new function iin DataSet 20100703
            {
                localvar4++;
                // textBox4.Text = var1.ToString();

                localstr1 = dssplit.Tables[0].Rows[var1]["CustomerSiteNew"].ToString();  // Convert to CustomerSite
                localstr2 = dssplit.Tables[0].Rows[var1]["FoxconnRegionNew"].ToString();
                localstr3 = dssplit.Tables[0].Rows[var1]["Forecast_CustomerPN"].ToString();
                localstr4 = dssplit.Tables[0].Rows[var1]["Forecast_BeginDate"].ToString();
                localstr5 = dssplit.Tables[0].Rows[var1]["Forecast_Qty"].ToString();
                localstr6 = dssplit.Tables[0].Rows[var1]["Document_ID"].ToString();
                localstr7 = dssplit.Tables[0].Rows[var1]["Week"].ToString();
                localstr9 = dssplit.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString();
                localstr12= dssplit.Tables[0].Rows[var1]["Conversation_ID"].ToString();
                localstr13= dssplit.Tables[0].Rows[var1]["Document_TimeNew"].ToString();
                localstr15= dssplit.Tables[0].Rows[var1]["Forecast_Qty"].ToString();
                localstr15= ShipPlanlibPointer.TrsStrToInteger(localstr15);
                localstr16 = dssplit.Tables[0].Rows[var1]["Forecast_TimeNew"].ToString();
                localstr19 = dssplit.Tables[0].Rows[var1]["CustomerSiteNew"].ToString();  // Convert to CustomerSite
                localstr20 = dssplit.Tables[0].Rows[var1]["FoxconnRegionNew"].ToString();
                localstr21 = dssplit.Tables[0].Rows[var1]["CItem"].ToString();
                localstr22 = dssplit.Tables[0].Rows[var1]["CAgreement"].ToString();
               
                if ((localstr1 == s1) && (localstr2 == s2) && (localstr3 == s3) && (localstr6 == s6))
                {
                    localvar1++;
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 11] = localvar1.ToString(); // SP total number                 
                }                  
                else
                {
                    localvar1 = 1;
                    eachIDSetCount++;
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1]  = localstr1; // CustomerSite
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2]  = localstr2; // FoxconnRegion
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3]  = localstr3; // Forecast_CustomerPN
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4]  = localstr6; // Document_ID
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7]  = localstr7; // Week
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 9]  = localstr9; // Forecast_QtyTypeCode  
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 11] = localvar1.ToString(); // SP total number
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 12] = localstr12; // Conversation_ID
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 13] = localstr13; // DocumentTime
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 14] = "Y"; // write flag
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 16] = localstr16; // Forecast_Time
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 19] = localstr19; // CustomerCode
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 20] = localstr20; // FoxconnRegionCode
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 21] = localstr21; // dssplit.Tables[0].Rows[var1]["CItem"].ToString();
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 22] = localstr22; // dssplit.Tables[0].Rows[var1]["CAgreement"].ToString();

                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = tmpSPWeek; //  localstr7; 特殊讀 Nokia_Calandar
               
                    localvar3 = 0; // clear for new PN QTY
                }

                localvar3 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 15]);
                localvar3 = localvar3 + Convert.ToInt32(localstr15);
                arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 15] = localvar3.ToString(); 
               
                s1 = dssplit.Tables[0].Rows[var1]["CustomerSiteNew"].ToString();  // Convert to CustomerSiteNew
                s2 = dssplit.Tables[0].Rows[var1]["FoxconnRegionNew"].ToString();
                s3 = dssplit.Tables[0].Rows[var1]["Forecast_CustomerPN"].ToString();
                s4 = dssplit.Tables[0].Rows[var1]["Forecast_BeginDate"].ToString();
                s5 = dssplit.Tables[0].Rows[var1]["Forecast_Qty"].ToString();
                s6 = dssplit.Tables[0].Rows[var1]["Document_ID"].ToString();  // 取比大小
                s7 = dssplit.Tables[0].Rows[var1]["Week"].ToString();
                s8 = dssplit.Tables[0].Rows[var1]["Forecast_IntervalCode"].ToString();
                s9 = dssplit.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString();
                
            } // end if (ds3.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString() != "")  

        }  // end for loop 
        // end 1

        // arrayCustomerPlant[localvar + 1, 1] = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
        // arrayCustomerPlant[localvar + 1, 2] = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
        // arrayCustomerPlant[localvar + 1, 3] = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
        // arrayCustomerPlant[localvar, 4] = localvar.ToString();
        // Conert FoxconnRegion, CustomerSite Code to name
        
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSiteNew
            s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion

            for (var2 = 1; var2 < CustomerPlantLong+1; var2++)
            {
                s111=arrayCustomerPlant[var2, 1]; // = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
                s222=arrayCustomerPlant[var2, 2]; // = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
                s333=arrayCustomerPlant[var2, 3]; // = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
                s444=arrayCustomerPlant[var2, 4]; // = ds5.Tables[0].Rows[localvar]["CustomerNo"].ToString();
                if ((s1 == s222))
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 1] = s333; // Write CustomerSite Plant_Name
                    arrayCustomerFoxconnPNToOneSet[var1, 25] = s444; // = ds5.Tables[0].Rows[localvar]["CustomerNo"].ToString();
                    var2 = CustomerPlantLong + 1; // break
                }
            }

            for (var2 = 1; var2 < CustomerPlantLong + 1; var2++)
            {
                s111 = arrayCustomerPlant[var2, 1]; // = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
                s222 = arrayCustomerPlant[var2, 2]; // = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
                s333 = arrayCustomerPlant[var2, 3]; // = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
                if ((s2 == s222))
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 2] = s333;// Write FoxconnRegion Plant_Name
                    var2 = CustomerPlantLong + 1; // break
                }
            }
                       
        }  // end get Plant Name


        //////////////////// Get Ticket 20101225
        // for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        // {
        //    s10 = LibSCM1Pointer.CGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath);
        //    arrayCustomerFoxconnPNToOneSet[var1, 10] = s10.ToString(); // SP ticket            
        // }  // end get CGet_Ticket

        // CNGet_Ticket
        if ( eachIDSetCount > 0 )
            d1 = Convert.ToDouble(LibSCM1Pointer.CNGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath, eachIDSetCount));

        d1 = d1 - Convert.ToDouble(eachIDSetCount);
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s10 = (d1++).ToString(); 
            arrayCustomerFoxconnPNToOneSet[var1, 10] = s10.ToString(); // SP ticket            
        }  // end get ticket
        
        d1 = 0;  // claer

        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            // s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
            // s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
            // s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
            // s6 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID

            for (var2 = 0; var2 < Long4A3_Detail_PNOneSet; var2++)
            {
                // s111 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["CustomerSite"].ToString();  // Convert to CustomerSite
                // s222 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["FoxconnRegion"].ToString();
                // s333 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Forecast_CustomerPN"].ToString();
                // s666 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Document_ID"].ToString();
                // if ((s111 == s1) && (s222 == s2) && (s333 == s3) && (s666 == s6))
                if ((ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["CustomerSite"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 1]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["FoxconnRegion"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 2]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Forecast_CustomerPN"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 3]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Document_ID"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 4]))
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 10] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Ticket"].ToString(); // With Old ticket
                    arrayCustomerFoxconnPNToOneSet[var1, 14] = "N"; // Write flag
                    var2 = Long4A3_Detail_PNOneSet + 1; // break
                }

            }
        }

        /////////////////////////////////////////////////////////////////
        // Project Foxconn_Nokia_PartNo Project  Project, Description
        /////////////////////////////////////////////////////////////////
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
            s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
            s20= arrayCustomerFoxconnPNToOneSet[var1, 20];   // FoxconnRegionCode
            s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN

            for ( var2 = 1; var2 < Foxconn_Nokia_PartNoLong + 1; var2++)  // load in memory
            {
                stmp = arrayFoxconn_Nokia_PartNo[var2, 1];
                if ((s1.ToUpper() == arrayFoxconn_Nokia_PartNo[var2, 1].ToUpper()) && (s3 == arrayFoxconn_Nokia_PartNo[var2, 3]) && (s20 == arrayFoxconn_Nokia_PartNo[var2, 12]) )
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 23]  = arrayFoxconn_Nokia_PartNo[var2, 4]; // Project
                    arrayCustomerFoxconnPNToOneSet[var1, 24] = StrClrSpecialChar(arrayFoxconn_Nokia_PartNo[var2, 5]); // Description
                    arrayCustomerFoxconnPNToOneSet[var1, 26] = arrayFoxconn_Nokia_PartNo[var2, 6]; // FoxconnPartNo
                    arrayCustomerFoxconnPNToOneSet[var1, 28] = arrayFoxconn_Nokia_PartNo[var2, 13];// FoxconnBU
                    var2 = Foxconn_Nokia_PartNoLong + 1;
                }
                    // arrayFoxconn_Nokia_PartNo[localvar2, 1] = dsPart.Tables[0].Rows[localvar]["NokiaSite"].ToString();
                    // arrayFoxconn_Nokia_PartNo[localvar2, 2] = dsPart.Tables[0].Rows[localvar]["FoxconnSite"].ToString();
                    // arrayFoxconn_Nokia_PartNo[localvar2, 3] = dsPart.Tables[0].Rows[localvar]["NokiaPartNo"].ToString();
                    // arrayFoxconn_Nokia_PartNo[localvar2, 4] = dsPart.Tables[0].Rows[localvar]["Project"].ToString();
            }  // end load tabel in memor
        }

        d1=0.0;
        d2=0.0;
        /////////////////////////////////////////////////////////////////
        // Check Split Flag 20110221
        /////////////////////////////////////////////////////////////////
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
            s20= arrayCustomerFoxconnPNToOneSet[var1, 20];   // FoxconnRegionCode
            s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN

            for (var2 = 1; var2 < Foxconn_Nokia_PartNoLong + 1; var2++)  // load in memory
            {
                stmp = arrayFoxconn_Nokia_PartNo[var2, 1];
                if ((s1.ToUpper() == arrayFoxconn_Nokia_PartNo[var2, 1].ToUpper()) && (s3 == arrayFoxconn_Nokia_PartNo[var2, 3]) && (s20 == arrayFoxconn_Nokia_PartNo[var2, 12]))
                {

                    s11 = arrayFoxconn_Nokia_PartNo[var1, 7];  // = dsPart.Tables[0].Rows[localvar]["Split_Method"].ToString();
                    s12 = arrayFoxconn_Nokia_PartNo[var1, 8];  // = dsPart.Tables[0].Rows[localvar]["Split_Priority
                    s13 = arrayFoxconn_Nokia_PartNo[var1, 9];  // = dsPart.Tables[0].Rows[localvar]["Min_ratio"].ToString();
                    s14 = arrayFoxconn_Nokia_PartNo[var1, 10]; // = dsPart.Tables[0].Rows[localvar]["Max_ratiod"].ToString();
           
                    if ( ( s11 != "" ) && ( s13 != "")  && ( s14 != "") )
                    {
                        d1 = Convert.ToDouble(s13);
                        d2 = Convert.ToDouble(s14);
                        if ( ( d1 > 0 ) && ( d2 > 0 ) && ( d1 < 1 ) && ( d2 < 1 ) )
                        {    
                            arrayCustomerFoxconnPNToOneSet[var1, 27] = "Y"; // Split_Flag="Y"
                            var2 = Foxconn_Nokia_PartNoLong + 1;
                        }
                    }
                    else
                        arrayCustomerFoxconnPNToOneSet[var1, 27] = "N"; // Split_Flag="N"
                                   
                }
                // arrayFoxconn_Nokia_PartNo[localvar2, 1] = dsPart.Tables[0].Rows[localvar]["NokiaSite"].ToString();
                // arrayFoxconn_Nokia_PartNo[localvar2, 2] = dsPart.Tables[0].Rows[localvar]["FoxconnSite"].ToString();
                // arrayFoxconn_Nokia_PartNo[localvar2, 3] = dsPart.Tables[0].Rows[localvar]["NokiaPartNo"].ToString();
                // arrayFoxconn_Nokia_PartNo[localvar2, 4] = dsPart.Tables[0].Rows[localvar]["Project"].ToString();
            }  // end load tabel in memor
        }
        var2 = 0;
        // Insert Syncro_4A3_Detail_PNOneSet , Update 4A3_Detail with ReplyNokia_ID 
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
            s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
            s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
            s6 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID
            s7 = arrayCustomerFoxconnPNToOneSet[var1, 7];    // Week
            s9 = arrayCustomerFoxconnPNToOneSet[var1, 9];    // Forecast_QtyTypeCode
            s10 = arrayCustomerFoxconnPNToOneSet[var1, 10];  // SP ticket
            s11 = arrayCustomerFoxconnPNToOneSet[var1, 11];  // SP total number
            s12 = arrayCustomerFoxconnPNToOneSet[var1, 12];  // Conversation_ID
            s13 = arrayCustomerFoxconnPNToOneSet[var1, 13];  // DocumentTime
            s15 = arrayCustomerFoxconnPNToOneSet[var1, 15];  // ToTForecast_Qty
            s16 = arrayCustomerFoxconnPNToOneSet[var1, 16];  // Forecast_Time
            s19 = arrayCustomerFoxconnPNToOneSet[var1, 19];  // CustomerSiteCode
            s20 = arrayCustomerFoxconnPNToOneSet[var1, 20];  // FoxconnRegionCode
            s21 = arrayCustomerFoxconnPNToOneSet[var1, 21];  // CItem
            s22 = arrayCustomerFoxconnPNToOneSet[var1, 22];  // CAgreement
            s23 = arrayCustomerFoxconnPNToOneSet[var1, 23];  // Project
            s24 = arrayCustomerFoxconnPNToOneSet[var1, 24];  // Description
            s25 = arrayCustomerFoxconnPNToOneSet[var1, 25];  // CustomerNo
            s26 = arrayCustomerFoxconnPNToOneSet[var1, 26];  // FoxconnPartNo
            s27 = arrayCustomerFoxconnPNToOneSet[var1, 27];  // FoxconnPartNo
            s28 = arrayCustomerFoxconnPNToOneSet[var1, 28];  // FoxconnBU
   
            if (arrayCustomerFoxconnPNToOneSet[var1, 14].ToString() == "Y")  // write only new
            {
                subtmpstr3 = "Insert into Syncro_4A3_Detail_PNOneSet ( CustomerSite, FoxconnRegion, Forecast_CustomerPN, Document_ID,"
            + " Forecast_QtyTypeCode, Ticket, SPAccount, Conversation_ID, DocumentTime, ToTForecast_Qty, Forecast_Qty, Week, Forecast_BeginDate, "
            + " CustomerSiteCode, FoxconnRegionCode, Datafrom, Item, Agreement, Project, Description, CustomerNo, HHPARTS, SplitFlag, Customer, FoxconnBU ) "
            + " Values ( '" + s1 + "', '" + s2 + "', '" + s3 + "', '" + s6 + "', '" + s9 + "', '" + s10 + "', '" + s11 + "' , "
            + " '" + s12 + "', '" + s13 + "', '" + s15 + "', '" + s15 + "', '" + s7 + "', '" + s16 + "', '" + s19 + "', '" + s20 + "', "
            + " 'EV', '" + s21 + "', '" + s22 + "',  '" + s23 + "',  '" + s24 + "',  '" + s25 + "',  '" + s26 + "', '" + s27 + "', 'Nokia', '" + s28 + "' ) ";

                if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr3, RunDBPath))
                {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                    // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                    ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
                    // string t1 = FSplitlibPointer.Wri_MessLog("WRIERR", s1, s2, s3, s6, "4A3 Create Error", "", "", "", "");  // 10 Code
                }
            } // write flag
            else
            if (arrayCustomerFoxconnPNToOneSet[var1, 14].ToString() == "N")  // 重讀並 Update
            {
                s0 = Get4A3_DetailQTYinSecondTime(s3, s6);
                s15 = s0;
                subtmpstr3 = "Update Syncro_4A3_Detail_PNOneSet set ToTForecast_Qty = '" + s15 + "', Forecast_Qty = '" + s15 + "' "
                + " where Forecast_CustomerPN = '" + s3 + "' and Document_ID =  '" + s6 + "' ";

                if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr3, RunDBPath))
                {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                    // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                    ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
                }
                else  // 成功
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 15] = s15;  // ToTForecast_Qty
                }
            }    // 只做一半, 重算 4A3_Detail
            else
            {
                var2++;
            }

            ///////// update 4A3_Detail
            subtmpstr4 = " UPDATE Syncro_4A3_Detail SET ReplyNokia_ID = '" + s10 + "' WHERE ( ReplyNokia_ID is NULL "
            + " or ReplyNokia_ID = '') and  Document_ID = '" + s6 + "' and  Forecast_CustomerPN = '" + s3 + "' ";

            if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
            {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                ErrMsg = "Update 4A3_Detail 新增失敗，請稍后重試！";
            }

            subtmpstr4 = " UPDATE Syncro_4A5_Detail SET ReplyNokia_ID = '" + s10 + "' WHERE ( ReplyNokia_ID is NULL "
            + " or ReplyNokia_ID = '') and  Document_ID = '" + s6 + "' and  Forecast_CustomerPN = '" + s3 + "' ";

            if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
            {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                ErrMsg = "Update 4A3_Detail 新增失敗，請稍后重試！";
            }

            subtmpstr4 = " UPDATE Syncro_4A5_Detail_Plant SET ReplyNokia_ID = '" + s10 + "' WHERE ( ReplyNokia_ID is NULL "
            + " or ReplyNokia_ID = '') and  Document_ID = '" + s6 + "' and  Forecast_CustomerPN = '" + s3 + "' ";

            if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
            {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                ErrMsg = "Update 4A5_Detail_Plant 新增失敗，請稍后重試！";
            }

            Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
            ///////// update  Syncro_Foxconn_Nokia_PartNo
            // 20110406
            //subtmpstr4 = " UPDATE Syncro_Foxconn_Nokia_PartNo SET EOL = '',  UpdateTime = '" + s13 + "' "
            //+ "                             WHERE NokiaSite = '" + s1 + "' and  NokiaPartNo = '" + s3 + "' "
            //+ " and FoxconnRegion = '" + s20 + "' ";  // and FoxconnBU =   ";
            //
            //if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
            //{
            //    ErrMsg = "Update Syncro_Foxconn_Nokia_PartNo失敗，請稍后重試！";
            //} 
            //20110406 

        }  // end insert


        // ReSort 4A3_Detail_PNOneSet 每天最後一筆 
        sql4A3_Detail_PNOneSet = " Select * from Syncro_4A3_Detail_PNOneSet where  Substring( Documenttime,1,8) <= '" + DownDVDay + "' " // ( documenttime like '" + DownDVDay + "'+'%' )"
          + " and Substring( Documenttime,1,8) >= '" + PreDownDVDay + "' order by FoxconnRegion, CustomerSite, Forecast_CustomerPN, Document_ID ";
        ds4A3_Detail_PNOneSet = LibSCM1Pointer.GetDataByDataPath(sql4A3_Detail_PNOneSet, RunDBPath);  // All Data Not only EV
        if (ds4A3_Detail_PNOneSet.Tables.Count <= 0) Long4A3_Detail_PNOneSet = 0; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        else
            Long4A3_Detail_PNOneSet = ds4A3_Detail_PNOneSet.Tables[0].Rows.Count;

        for (var4 = 0; var4 < var3; var4++)  // clear array
             for (var5 = 0; var5 < arrayCustomerFoxconnPNToOneSetIndex + 1; var5++) 
                  arrayCustomerFoxconnPNToOneSet[var4, var5] = "";

   
        for (var1 = Long4A3_Detail_PNOneSet - 1; var1 >= 0; var1--)
        {
      
            arrayCustomerFoxconnPNToOneSet[var1 + 1, 1] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var1]["CustomerSite"].ToString();  // Convert to CustomerSite
            arrayCustomerFoxconnPNToOneSet[var1 + 1, 2] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var1]["FoxconnRegion"].ToString();
            arrayCustomerFoxconnPNToOneSet[var1 + 1, 3] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var1]["Forecast_CustomerPN"].ToString();
            arrayCustomerFoxconnPNToOneSet[var1 + 1, 6] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var1]["Document_ID"].ToString();
            arrayCustomerFoxconnPNToOneSet[var1 + 1, 15] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var1]["ToTForecast_Qty"].ToString(); // Old
            arrayCustomerFoxconnPNToOneSet[var1 + 1, 16] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var1]["DateLV"].ToString(); // Old
            arrayCustomerFoxconnPNToOneSet[var1 + 1, 17] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var1]["DateLV"].ToString(); // New
      
            if ((s1 != arrayCustomerFoxconnPNToOneSet[var1 + 1, 1]) || (s2 != arrayCustomerFoxconnPNToOneSet[var1 + 1, 2]) || (s3 != arrayCustomerFoxconnPNToOneSet[var1 + 1, 3])) // || (s6 == arrayCustomerFoxconnPNToOneSet[var1 + 1, 6]))
            {
                arrayCustomerFoxconnPNToOneSet[var1 + 1, 17] = "Y";
                s1 = arrayCustomerFoxconnPNToOneSet[var1 + 1, 1];
                s2 = arrayCustomerFoxconnPNToOneSet[var1 + 1, 2];
                s3 = arrayCustomerFoxconnPNToOneSet[var1 + 1, 3];
                s6 = arrayCustomerFoxconnPNToOneSet[var1 + 1, 6];
                s4 = arrayCustomerFoxconnPNToOneSet[var1 + 1, 6]; // document_id max
            }
            else
            {
                arrayCustomerFoxconnPNToOneSet[var1 + 1, 17] = "N";
            }
        }

        // Update 4A3_Detail_PNOnetSet DateLV 
        for (var1=1; var1 < Long4A3_Detail_PNOneSet+1; var1++)
        {
             if ( arrayCustomerFoxconnPNToOneSet[var1, 16] != arrayCustomerFoxconnPNToOneSet[var1, 17])  //update 
             {
                s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];
                s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];
                s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];
                s6 = arrayCustomerFoxconnPNToOneSet[var1, 6];
                s4 = arrayCustomerFoxconnPNToOneSet[var1, 17];
                subtmpstr4 = " UPDATE Syncro_4A3_Detail_PNOneSet SET DateLV = '" + s4 + "' WHERE CustomerSite = '" + s1 + "' and "
                + " FoxconnRegion = '" + s2 + "' and  Document_ID = '" + s6 + "' and  Forecast_CustomerPN = '" + s3 + "' ";

                if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
                { //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                  // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                  // ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
                  // Response.Write("<script>alert('Update 4A3_Detail 新增失敗，請稍后重試！ '" + (s1+s2+s3+s6) + "' ')</script>");
                  ErrMsg = "Update 4A3_Detail 新增失敗，請稍后重試！";
                } 
                 
             }   // end 16==17
        }

   
        // return (GetDataSqlLong.ToString());  // end 4A3_Detail
        // sql22 = " Select D.*,C.Customer Customer,C.Plant_Name Customer_Site  "
        //    + " From Syncro_4A1_Detail_plant D, Syncro_4A1_Partner P, Customer_Plant C, Syncro_4A1_Main M, Syncro_Foxconn_Nokia_PartNo S "
        //    + " Where D.Document_ID = M.Document_ID "
        //    + " And P.Document_ID =M.Document_ID "
        //    + " And P.Forecast_PartnerGBI=C.Plant_Code "
        //    + " And C.Plant_Name=S.NokiaSite  "  // -- 以下3條為關聯物料維護檔 "
        //    + " And D.Foxconn_Site=S.FoxconnRegion+':'+S.FoxconnSite+':'+S.FoxconnBU "
        //    + " And D.Forecast_CustomerPN=S.NokiaPartNO "
        //    + " And ( document_time like '" + DownPVfirstThuDate + "'+'%' or document_time like '" + DownPVfirstFriDate + "'+'%' )"
        //    + " And Substring([Forecast_QtyTypeCode],1,8)='Discrete' ";


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // End of 4A3 Generate
        // Svae 4A3 to tmpbuff  b1 and Pre4A3Long
        int Pre4A3Long = Long4A3_Detail_PNOneSet;  // store old 
        string[,] b1 = new string[arrayCustomerFoxconnPNToOneSetLong, 30 + 1];
        var3 = arrayCustomerFoxconnPNToOneSetLong;

        for (var4 = 0; var4 < var3; var4++)
            for (var5 = 0; var5 < 30 + 1; var5++) 
                  b1[var4, var5] = arrayCustomerFoxconnPNToOneSet[var4, var5];

        for (var4 = 0; var4 < var3; var4++)
            if (arrayCustomerFoxconnPNToOneSet[var4, 15] != "0")
                var4 = var4;

        for (var4 = 0; var4 < var3; var4++)
            if (b1[var4, 15] != "0")
                var4 = var4; 
        //////////////////////////////////
        // Start run 4A1_Detail

        var3 = arrayCustomerFoxconnPNToOneSetLong;

        for (var4 = 0; var4 < var3; var4++)
        {
            for (var5 = 0; var5 < arrayCustomerFoxconnPNToOneSetIndex + 1; var5++) arrayCustomerFoxconnPNToOneSet[var4, var5] = "";

            arrayCustomerFoxconnPNToOneSet[var4, 5] = (10000 + var4).ToString();     // 為 Key當Index 到 array 13 Upload
            arrayCustomerFoxconnPNToOneSet[var4, 6] = tmptoday.ToString("yyyyMMdd"); // DV 最早一天
            arrayCustomerFoxconnPNToOneSet[var4, 7] = tmptoday.ToString("yyyyMMdd"); // DV 最晚一天
            arrayCustomerFoxconnPNToOneSet[var4, 8] = "0";                           // Summary GIT, Stock, Consigned
            arrayCustomerFoxconnPNToOneSet[var4, IndexArrayLoc] = "11";        // loc 10 記錄下一位, 11放實際 DTDUpload Loc 
            arrayCustomerFoxconnPNToOneSet[var4, 15] = "0";

        }   // End InitializeCulture space



        sql4A3_Detail_PNOneSet = " Select * from Syncro_4A1_Detail_PNOneSet where Substring(DocumentTime,1,8) <= '" + DownDVDay + "' " // (documenttime like '" + DownDVDay + "'+'%' )"
          + " and Substring( Documenttime,1,8) >= '" + PreDownDVDay + "' order by FoxconnRegion, CustomerSite, Forecast_CustomerPN, Document_ID ";
        ds4A3_Detail_PNOneSet = LibSCM1Pointer.GetDataByDataPath(sql4A3_Detail_PNOneSet, RunDBPath);
        if (ds4A3_Detail_PNOneSet.Tables.Count <= 0) Long4A3_Detail_PNOneSet = 0;
        else
            Long4A3_Detail_PNOneSet = ds4A3_Detail_PNOneSet.Tables[0].Rows.Count;

        // sqlsplit = " Select top(2000) D.* , "
        sqlsplit = " Select D.* , "
         + " P.Forecast_PartnerGBI CustomerSiteNew, R.to_GBI FoxconnRegionNew, M.Document_Time Document_TimeNew, M.Forecast_Time Forecast_TimeNew "
         + " From Syncro_4A1_Detail D, Syncro_4A1_Partner P, Syncro_4A1_Main M, Syncro_4A1_role R "
         + " Where D.Document_ID = M.Document_ID and D.Document_ID = P.Document_ID and  D.Document_ID = R.Document_ID "
         + " and   M.Document_ID = P.Document_ID and M.Document_ID = R.Document_ID and  P.Document_ID = R.Document_ID "
        //  + " And ( M.document_time like '" + DownDVDay + "'+'%' )"
         + " and ( Substring( M.document_time,1,8) <= '" + DownDVDay + "' ) "
         + " and ( Substring( M.document_time,1,8) >= '" + PreDownDVDay + "' ) "
         + " And Substring([Forecast_QtyTypeCode],1,8)='Discrete' "
         + " And ( D.ReplyNokia_ID is NULL or D.ReplyNokia_ID = '') "  // ReplyNokia_ID = Ticket
         + " order by FoxconnRegionNew, CustomerSiteNew, D.Forecast_CustomerPN, D.Document_ID ";

        dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // All Data Not only EV
        if (dssplit.Tables.Count <= 0) localvar1 = -1; // return;
        else
        {
            localvar1 = dssplit.Tables[0].Rows.Count;
            GetDataSqlLong = dssplit.Tables[0].Rows.Count;
            UploadETDrecordLong = GetDataSqlLong;
            EVLong = UploadETDrecordLong;
        }

        if (localvar1 <= 0)
            return ("1");

        // 1
        DelpcateEVNum = 0;
        eachIDSetCount = 0;
        localvar1 = 0;
        localvar2 = 0;
        localvar3 = 0;
        s1 = ""; s2 = ""; s3 = ""; s6 = "";

        for (var1 = 0; var1 < dssplit.Tables[0].Rows.Count; var1++)
        {
            localstr5 = dssplit.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString();

            if (localstr5 == "Discrete Gross Demand")  // Delete new function iin DataSet 20100703
            {
                localvar4++;
        //        textBox4.Text = var1.ToString();

                localstr1  = dssplit.Tables[0].Rows[var1]["CustomerSiteNew"].ToString();  // Convert to CustomerSite
                localstr2  = dssplit.Tables[0].Rows[var1]["FoxconnRegionNew"].ToString();
                localstr3  = dssplit.Tables[0].Rows[var1]["Forecast_CustomerPN"].ToString();
                localstr4  = dssplit.Tables[0].Rows[var1]["Forecast_BeginDate"].ToString();
                localstr5  = dssplit.Tables[0].Rows[var1]["Forecast_Qty"].ToString();
                localstr6  = dssplit.Tables[0].Rows[var1]["Document_ID"].ToString();
                localstr7  = dssplit.Tables[0].Rows[var1]["Week"].ToString();
                localstr9  = dssplit.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString();
                localstr12 = dssplit.Tables[0].Rows[var1]["Conversation_ID"].ToString();
                localstr13 = dssplit.Tables[0].Rows[var1]["Document_TimeNew"].ToString();
                localstr15 = dssplit.Tables[0].Rows[var1]["Forecast_Qty"].ToString();
                localstr16 = dssplit.Tables[0].Rows[var1]["Forecast_TimeNew"].ToString();

                localstr23 = dssplit.Tables[0].Rows[var1]["PartnerLevel_ID"].ToString();
                localstr24 = dssplit.Tables[0].Rows[var1]["Forecast_ID"].ToString();
                localstr25 = dssplit.Tables[0].Rows[var1]["Forecast_LineID"].ToString();
                
                localstr15 = ShipPlanlibPointer.TrsStrToInteger(localstr15);


                if ((localstr1 == s1) && (localstr2 == s2) && (localstr3 == s3) && (localstr6 == s6))
                {
                    localvar1++;
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 11] = localvar1.ToString(); // SP total number
                }
                else
                {
                    localvar1 = 1;
                    eachIDSetCount++;
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = localstr1; // CustomerSite
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = localstr2; // FoxconnRegion
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = localstr3; // Forecast_CustomerPN
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = localstr6; // Document_ID
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5] = localstr5; // Forecast_Qty
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = localstr7; // Week
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 8] = localstr4; // Forecast_BeginDate  
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 9] = localstr9; // Forecast_QtyTypeCode  
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 11] = localvar1.ToString(); // SP total number
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 12] = localstr12; // Conversation_ID
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 13] = localstr13; // DocumentTime
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 14] = "Y"; // write flag  4A1_Detail_PNOneSet
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 16] = localstr16; // Forecast_Time
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 23] = localstr23; // = dssplit.Tables[0].Rows[var1]["PartnerLevel_ID"].ToString();
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 24] = localstr24; // = dssplit.Tables[0].Rows[var1]["Forecast_ID"].ToString();
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 25] = localstr25; // = dssplit.Tables[0].Rows[var1]["Forecast_LineID"].ToString();
                    localvar3 = 0; // clear for new PN QTY

                }

                localvar3 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 15]);
                localvar3 = localvar3 + Convert.ToInt32(localstr15);
                arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 15] = localvar3.ToString();  // Write 4A1 ToTQTy

                s1 = dssplit.Tables[0].Rows[var1]["CustomerSiteNew"].ToString();  // Convert to CustomerSiteNew
                s2 = dssplit.Tables[0].Rows[var1]["FoxconnRegionNew"].ToString();
                s3 = dssplit.Tables[0].Rows[var1]["Forecast_CustomerPN"].ToString();
                s4 = dssplit.Tables[0].Rows[var1]["Forecast_BeginDate"].ToString();
                s5 = dssplit.Tables[0].Rows[var1]["Forecast_Qty"].ToString();
                s6 = dssplit.Tables[0].Rows[var1]["Document_ID"].ToString();  // 取比大小
                s7 = dssplit.Tables[0].Rows[var1]["Week"].ToString();
                s8 = dssplit.Tables[0].Rows[var1]["Forecast_IntervalCode"].ToString();
                s9 = dssplit.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString();

            } // end if (ds3.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString() != "")  

        }  // end for loop 
        // end 1

        // arrayCustomerPlant[localvar + 1, 1] = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
        // arrayCustomerPlant[localvar + 1, 2] = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
        // arrayCustomerPlant[localvar + 1, 3] = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
        // arrayCustomerPlant[localvar, 4] = localvar.ToString();
        // Conert FoxconnRegion, CustomerSite Code to name

        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSiteNew
            s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion

            for (var2 = 1; var2 < CustomerPlantLong + 1; var2++)
            {
                s111 = arrayCustomerPlant[var2, 1]; // = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
                s222 = arrayCustomerPlant[var2, 2]; // = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
                s333 = arrayCustomerPlant[var2, 3]; // = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
                if ((s1 == s222))
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 1] = s333;// Write CustomerSite Plant_Name
                    var2 = CustomerPlantLong + 1; // break
                }
            }

            for (var2 = 1; var2 < CustomerPlantLong + 1; var2++)
            {
                s111 = arrayCustomerPlant[var2, 1]; // = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
                s222 = arrayCustomerPlant[var2, 2]; // = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
                s333 = arrayCustomerPlant[var2, 3]; // = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
                if ((s2 == s222))
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 2] = s333;// Write FoxconnRegion Plant_Name
                    var2 = CustomerPlantLong + 1; // break
                }
            }

        }  // end get Plant Name


        //////////////////// Get Ticket
        // for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        // {
        //    s10 = LibSCM1Pointer.CGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath);
        //    arrayCustomerFoxconnPNToOneSet[var1, 10] = s10.ToString(); // SP ticket            
        // }  // end get ticket

        // CNGet_Ticket
        if (eachIDSetCount > 0)
            d1 = Convert.ToDouble(LibSCM1Pointer.CNGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath, eachIDSetCount));

        d1 = d1 - Convert.ToDouble(eachIDSetCount);
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s10 = (d1++).ToString();
            arrayCustomerFoxconnPNToOneSet[var1, 10] = s10.ToString(); // SP ticket            
        }  // end get ticket

        d1 = 0;  // claer
               

        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
            s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
            s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
            s6 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID

            for (var2 = 0; var2 < Long4A3_Detail_PNOneSet; var2++)
            {
                s111 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["CustomerSite"].ToString();  // Convert to CustomerSite
                s222 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["FoxconnRegion"].ToString();
                s333 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Forecast_CustomerPN"].ToString();
                s666 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Document_ID"].ToString();
                if ((s111 == s1) && (s222 == s2) && (s333 == s3) && (s666 == s6))
                {
                    arrayCustomerFoxconnPNToOneSet[var1, 10] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Ticket"].ToString();
                    arrayCustomerFoxconnPNToOneSet[var1, 14] = "N"; // Write flag
                    var2 = Long4A3_Detail_PNOneSet + 1; // break
                }

            }
        }

        var2 = 0;
        // Insert Syncro_4A3_Detail_PNOneSet , Update 4A3_Detail with ReplyNokia_ID 
        string tPNGroupFlag = "N", tMCGCode;
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
            s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
            s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
            s6 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID
            s7 = arrayCustomerFoxconnPNToOneSet[var1, 7];    // Week
            s9 = arrayCustomerFoxconnPNToOneSet[var1, 9];    // Forecast_QtyTypeCode
            s10 = arrayCustomerFoxconnPNToOneSet[var1, 10];  // SP ticket
            s11 = arrayCustomerFoxconnPNToOneSet[var1, 11];  // SP total number
            s12 = arrayCustomerFoxconnPNToOneSet[var1, 12];  //  Conversation_ID
            s13 = arrayCustomerFoxconnPNToOneSet[var1, 13];  //  DocumentTime
            s15 = arrayCustomerFoxconnPNToOneSet[var1, 15];  //  ToT4A1Forecast_Qty
            s16 = arrayCustomerFoxconnPNToOneSet[var1, 16];  //  Forecast_Time
            tMCGCode = s3.Substring(0, 3);
            if (tMCGCode == "MCG")
                tPNGroupFlag = "Y";
            else tPNGroupFlag = "N";

            arrayCustomerFoxconnPNToOneSet[var1, 16] = tPNGroupFlag;  //  is MCG Group ??

            if (arrayCustomerFoxconnPNToOneSet[var1, 14].ToString() == "Y")  // write only new
            {
                subtmpstr3 = "Insert into Syncro_4A1_Detail_PNOneSet ( CustomerSite, FoxconnRegion, "
            + "Forecast_CustomerPN, Document_ID, Forecast_QtyTypeCode, Ticket, SPAccount, Conversation_ID, DocumentTime, "
            + " PNGroup, PNGroupFlag, ToT4A1Forecast_Qty, Week, Forecast_ID )  Values "
            + " ( '" + s1 + "', '" + s2 + "', '" + s3 + "', '" + s6 + "', '" + s9 + "', '" + s10 + "', '" + s11 + "' , "
            + " '" + s12 + "', '" + s13 + "',  '" + s3 + "',  '" + tPNGroupFlag + "',  '" + s15 + "', '" + s7 + "', '" + s16 + "' ) ";

                if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr3, RunDBPath))
                {
                    ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
                }
            } // write flag
            else
            {
                var2++;
            }

            ///////// update 4A3_Detail
            subtmpstr4 = " UPDATE Syncro_4A1_Detail SET ReplyNokia_ID = '" + s10 + "' WHERE ( ReplyNokia_ID is NULL "
            + " or ReplyNokia_ID = '') and  Document_ID = '" + s6 + "' and  Forecast_CustomerPN = '" + s3 + "' ";

            if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
            {
                // Response.Write("<script>alert('Update 4A3_Detail 新增失敗，請稍后重試！ '" + (s12 + s3) + "' ')</script>");
                ErrMsg = "Update 4A3_Detail 新增失敗，請稍后重試！";    
            }

        }  // end insert


        // Write PNGroup MCG

        int PN_GroupLong = 0;
        string SqlPN_Group = " Select A.*, B.documentTime DocumentTime, B.Week Week from Syncro_4A1_PN_Group A, Syncro_4A1_Detail_PNOneSet B   "
        + " where Substring(B.documenttime,1,8) <= '" + DownDVDay + "'  and  Substring(B.documenttime,1,8) >= '" + PreDownDVDay + "' "
        + " and A.Document_ID = B.Document_ID  and  A.Forecast_CustomerPN = B.Forecast_CustomerPN "
        + " order by A.document_id, A.Forecast_CustomerPN, A.Forecast_PNGroup  ";
        DataSet dsPN_Group = LibSCM1Pointer.GetDataByDataPath(SqlPN_Group, RunDBPath);  // All Data Not only EV
        if (dsPN_Group.Tables.Count <= 0) PN_GroupLong = 0;
        else
            PN_GroupLong = dsPN_Group.Tables[0].Rows.Count;

        string[,] arrayPNGroup = new string[PN_GroupLong+1, 25 + 1];
        //   arrayPNGroup[localvar + 1, 6] = dsPN_Group.Tables[0].Rows[localvar]["Document_ID"].ToString();
        //   arrayPNGroup[localvar + 1, 3] = dsPN_Group.Tables[0].Rows[localvar]["Forecast_CustomerPN"].ToString();
        //   arrayPNGroup[localvar + 1, 17] = dsPN_Group.Tables[0].Rows[localvar]["Forecast_PNGroup"].ToString();

        GetDataInArrayPNGroup(ref arrayPNGroup, ref dsPN_Group); // Put DBA data into arrarpart

        ///////////////////////////////////////////////////////////////////////////////////////
        // Find MCG in 4A1_Detail_PNOneSet, 21 PNGroupPN, PNGroupPNDVQty(?), 4A3McgPNDVQty   
        var2 = 20; var3 = 0; var5 = 5;

        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            if (arrayCustomerFoxconnPNToOneSet[var1, 16] == "Y")  // MCG Flag
            {
                s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
                s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
                s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
                s6 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID
                s7 = arrayCustomerFoxconnPNToOneSet[var1, 7];    // Week
                s9 = arrayCustomerFoxconnPNToOneSet[var1, 9];    // Forecast_QtyTypeCode
                s10 = arrayCustomerFoxconnPNToOneSet[var1, 10];  // SP ticket
                // s11 = arrayCustomerFoxconnPNToOneSet[var1, 11];  // SP total number
                // s12 = arrayCustomerFoxconnPNToOneSet[var1, 12];  //  Conversation_ID
                s13 = arrayCustomerFoxconnPNToOneSet[var1, 13];  //  DocumentTime
                if (( arrayCustomerFoxconnPNToOneSet[var1, 15] == "" ) || ( arrayCustomerFoxconnPNToOneSet[var1, 15] == null ) )
                      arrayCustomerFoxconnPNToOneSet[var1, 15] = "0";

                s15 = arrayCustomerFoxconnPNToOneSet[var1, 15];  //  ToT4A1Forecast_Qty

                var7 = 0; // flag
                for (var6 = 1; var6 < PN_GroupLong + 1; var6++)
                {
                    if ((arrayPNGroup[var6, 6] == s6) && (arrayPNGroup[var6, 3] == s3)) // Find MCG
                    {
                        localstr14 = arrayPNGroup[var6, 3];
                        var7++; // find start
                        arrayPNGroup[var6, 1] = s1; // = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
                        arrayPNGroup[var6, 2] = s2; // = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
                        // arrayPNGroup[var6, 3] = s3; // = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
                        // arrayPNGroup[var6, 6] = s6; // = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID
                        arrayPNGroup[var6, 10] = s10; // = arrayCustomerFoxconnPNToOneSet[var1, 10];  // SP ticket
                        // s11 = arrayCustomerFoxconnPNToOneSet[var1, 11];  // SP total number
                        // s12 = arrayCustomerFoxconnPNToOneSet[var1, 12];  //  Conversation_ID
                        arrayPNGroup[var6, 13] = s13; // = arrayCustomerFoxconnPNToOneSet[var1, 13];  //  DocumentTime
                        arrayPNGroup[var6, 15] = s15; // ToT4A1Forecast_Qty
                        arrayPNGroup[var6, 17] = arrayPNGroup[var6, 17]; // dsPN_Group.Tables[0].Rows[localvar]["Forecast_PNGroup"].ToString();
                        arrayPNGroup[var6, 16] = "Y"; // MCG Need Spilt Flag
                        arrayPNGroup[var6, 18] = "0"; // 4A3Forecast_Qty
                        arrayPNGroup[var6, 19] = "0"; // TOT4A3Forecast_Qty
                        arrayPNGroup[var6, 20] = "0"; // ( 18/19 ) Rate 
                        arrayPNGroup[var6, 21] = "0"; // 15 * ( 18/19 ) 
                    }
                    else
                    if (var7 >= 1)
                        var6 = PN_GroupLong + 1; // break

                }

            }
        }

        ////////////////////////////////////////////////////////////
        // Get 4A3 DVQTy in  arrayPNGroup[var1, 18] PNGroup=4A1_DetailMcgPN
        // Double d1=0, d2=0, d3=0, d4=0, d5=0, d6=0, d7=0, d8=0;
        subtmpstr1 = LibSCM1Pointer.SCMGet4A3QtyToPNGroup( 16, Pre4A3Long, PN_GroupLong, b1, arrayPNGroup); 
        
 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 在同一 PNGroup (MCG) in 4A1_DetailMCGPN arrayPNGroup[var6, 18] 加總放入arrayPNGroup[var6, 19] // TOT4A3Forecast_Qty

        var3 = 0; var4 = 0; s1 = ""; s2 = ""; var5 = 0; var6 = 0;
        for (var1 = 1; var1 < PN_GroupLong + 1; var1++)
        {
            if (arrayPNGroup[var1, 16] == "Y")  // 用 Document_ID + MCG(Forecast_CustomerPN)
            {
                s3 = arrayPNGroup[var1, 3]; // = arrayCustomerFoxconnPNToOneSet[var1, 3]; // Forecast_CustomerPN 因 PNGroup 戶換
                s6 = arrayPNGroup[var1, 6]; // = arrayCustomerFoxconnPNToOneSet[var1, 4]; // Document_ID
                if ((arrayPNGroup[var1, 18] == "") || (arrayPNGroup[var1, 18] == null)) arrayPNGroup[var1, 18] = "0";
                
                d6=Convert.ToDouble(arrayPNGroup[var1, 18]);
                arrayPNGroup[var1, 19] = d6.ToString(); // MCGPN Sumary

                if ((s1 == s3) && (s2 == s6))
                    d5 = d5 + d6;   // MCGPN 累加同 MCG 
                else
                    d5 = d6; 
           
                arrayPNGroup[var1, 19] = d5.ToString(); // MCGPN Sumary

            }     // if ( arrayPNGroup[var1, 16] ==  "Y") 

            s1 = arrayPNGroup[var1, 3]; // = arrayCustomerFoxconnPNToOneSet[var1, 3]; // Forecast_CustomerPN 因 PNGroup 戶換
            s2 = arrayPNGroup[var1, 6]; // = arrayCustomerFoxconnPNToOneSet[var1, 4]; // Document_ID

        }

        ////////////////////////
        // 倒回來從後面為最大數甜入 MCG

        var5 = 0;
        s1 = ""; s2 = ""; s3 = ""; s6 = "";  
        for (var1 = PN_GroupLong-1; var1 > 0; var1--)
        {
            if (arrayPNGroup[var1, 16] == "Y")  // 用 Document_ID + MCG(Forecast_CustomerPN)
            {
                s3 = arrayPNGroup[var1, 3]; // = arrayCustomerFoxconnPNToOneSet[var1, 3]; // Forecast_CustomerPN 因 PNGroup 戶換
                s6 = arrayPNGroup[var1, 6]; // = arrayCustomerFoxconnPNToOneSet[var1, 4]; // Document_ID
                if (arrayPNGroup[var1, 18] == "") arrayPNGroup[var1, 18] = "0";

                if ((s1 == s3) && (s2 == s6))
                    arrayPNGroup[var1, 19] = d1.ToString(); // MCGPN 
                else
                    d1 = Convert.ToDouble(arrayPNGroup[var1, 19]);

                ////////////////////
                d2 = Convert.ToDouble(arrayPNGroup[var1, 15]); // 4A1b Forcase_Qty
                d3 = Convert.ToDouble(arrayPNGroup[var1, 18]); // Sub   4A3 Foircase_Qty
                d4 = Convert.ToDouble(arrayPNGroup[var1, 19]); // Total 4A3 Foircase_Qty


                if ( ( d3 != 0 ) && ( d4 != 0 ))
                {
                     d5   = (d3 * 10000) / d4;
                     var5 = Convert.ToInt32(d5); 
                     arrayPNGroup[var1, 20] = var5.ToString(); // Rate 萬位
                     d6 = ( d2 * var5) / 10000;             // 4A3SubQty/4A3TotQty * 4A1QTy
                     var6 = Convert.ToInt32(d6); 
                     arrayPNGroup[var1, 21] = var6.ToString(); // 4A1 數量
                }
                else
                {
                    arrayPNGroup[var1, 20] = "0"; // Rate
                    arrayPNGroup[var1, 21] = "0"; // PNGroup PN QTyRate                    
                }

            }     // if ( arrayPNGroup[var1, 16] ==  "Y") 

            s1 = arrayPNGroup[var1, 3]; // = arrayCustomerFoxconnPNToOneSet[var1, 3]; // Forecast_CustomerPN 因 PNGroup 戶換
            s2 = arrayPNGroup[var1, 6]; // = arrayCustomerFoxconnPNToOneSet[var1, 4]; // Document_ID


        }

        /////////////////////////////////////////////////////////////
        // Write PNGroup_Detail_PNOneSet
        for (var1 = 1; var1 < PN_GroupLong + 1; var1++)
        {
            if (arrayPNGroup[var1, 16] == "Y")  // 用Document_ID + MCG(Forecast_CustomerPN)
            {
                s1 = arrayPNGroup[var1, 1];    // CustomerSite
                s2 = arrayPNGroup[var1, 2];    // FoxconnRegion
                s3 = arrayPNGroup[var1, 17];    // Forecast_CustomerPN
                s6 = arrayPNGroup[var1, 6];    // Document_ID
                s7 = arrayPNGroup[var1, 7];    // Week
                s9 = arrayPNGroup[var1, 9];    // Forecast_QtyTypeCode
                s10 = arrayPNGroup[var1, 10];  // SP ticket
                // s11 = arrayPNGroup[var1, 11];  // SP total number
                // s12 = arrayPNGroup[var1, 12];  //  Conversation_ID
                s13 = arrayPNGroup[var1, 13];  //  DocumentTime
                s15 = arrayPNGroup[var1, 15];  //  ToT4A1Forecast_Qty

                s17 = arrayPNGroup[var1, 3]; // dsPN_Group.Tables[0].Rows[localvar]["Forecast_PNGroup"].ToString();
                s16 = arrayPNGroup[var1, 16]; //  = "Y"; // MCG Need Spilt Flag
                s18 = arrayPNGroup[var1, 18]; // 4A3Forecast_Qty
                s19 = arrayPNGroup[var1, 19]; // TOT4A3Forecast_Qty
                s20 = arrayPNGroup[var1, 20]; // Rate 
                s21 = arrayPNGroup[var1, 21]; // 15 * ( 18/19 ) 

                if (arrayPNGroup[var1, 16].ToString() == "Y")  // write only new
                {
                    subtmpstr3 = "Insert into Syncro_PNGroup_Detail_PNOneSet ( CustomerSite, FoxconnRegion, "
                + "Forecast_CustomerPN, Document_ID, Forecast_QtyTypeCode, Ticket, SPAccount, Conversation_ID, DocumentTime, "
                + " PNGroup, PNGroupFlag, ToT4A1Forecast_Qty, QtyRate, Forecast_Qty, Sub4A3Forecast_Qty, ToT4A3Forecast_Qty, Week )  Values "
                + " ( '" + s1 + "', '" + s2 + "', '" + s3 + "', '" + s6 + "', '" + s9 + "', '" + s10 + "', '" + s11 + "' , "
                + " '" + s12 + "', '" + s13 + "',  '" + s17 + "',  '" + s16 + "',  '" + s15 + "', "
                + " '" + s20 + "',  '" + s21 + "',  '" + s18 + "',  '" + s19 + "', '" + s7 + "' ) ";

                    if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr3, RunDBPath))
                    {
                        ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
                    }
                } // write flag
                else
                {
                    var2++;
                }

            }
        }


        ///////////////////////////////////////////////////////////////////
        // 20101116 Read 4A1_Detail map PNGroup_Detail_PNOneSet to PNGroup_Detail 
        var2 = 20; var3 = 0; var5 = 5;
        for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
        {
            if (arrayCustomerFoxconnPNToOneSet[var1, 16] == "Y")  // MCG Flag
            {
                s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
                s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
                s17 = arrayCustomerFoxconnPNToOneSet[var1, 3];   // MCG Forecast_CustomerPN
                s6 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID
                s7 = arrayCustomerFoxconnPNToOneSet[var1, 7];    // Week
                s8 = arrayCustomerFoxconnPNToOneSet[var1, 8];    // Forecast_BeginDate
                s9 = arrayCustomerFoxconnPNToOneSet[var1, 9];    // Forecast_QtyTypeCode
                s10 = arrayCustomerFoxconnPNToOneSet[var1, 10];  // SP ticket
                // s11 = arrayCustomerFoxconnPNToOneSet[var1, 11];  // SP total number
                s12 = arrayCustomerFoxconnPNToOneSet[var1, 12];  //  Conversation_ID
                s13 = arrayCustomerFoxconnPNToOneSet[var1, 13];  //  DocumentTime
                s23 = arrayCustomerFoxconnPNToOneSet[var1, 23]; // = dssplit.Tables[0].Rows[var1]["PartnerLevel_ID"].ToString();
                s24 = arrayCustomerFoxconnPNToOneSet[var1, 24]; // = dssplit.Tables[0].Rows[var1]["Forecast_ID"].ToString();
                s25 = arrayCustomerFoxconnPNToOneSet[var1, 25]; // = dssplit.Tables[0].Rows[var1]["Forecast_LineID"].ToString();
             
   // Forecast_CustomerPN
   // Forecast_BeginDate
   // ReplyNokia_DateTime

                if ((arrayCustomerFoxconnPNToOneSet[var1, 15] == "") || (arrayCustomerFoxconnPNToOneSet[var1, 15] == null))
                    arrayCustomerFoxconnPNToOneSet[var1, 15] = "0";

                s15 = arrayCustomerFoxconnPNToOneSet[var1, 15];  //  ToT4A1Forecast_Qty

                var7 = 0; // flag
                for (var6 = 1; var6 < PN_GroupLong + 1; var6++)
                {
                    if ((arrayPNGroup[var6, 6] == s6) && (arrayPNGroup[var6, 3] == s17)) // Find MCG
                    {
                        s3 = arrayPNGroup[var6, 17]; // Forecast_CustomerPN
                        d1 = Convert.ToDouble(s15);  // Fore_Qty
                        d2 = Convert.ToDouble(arrayPNGroup[var6, 20]); // Rate
                        var5 = Convert.ToInt32(((d1 * d2) / 10000)) ;
                        localstr1 = var5.ToString(); 
                        var7++; // find start
                        subtmpstr3 = "Insert into Syncro_PNGroup_Detail ( Forecast_CustomerPN, Document_ID,  ReplyNokia_ID,  " 
                              + " Forecast_Qty, Conversation_ID,  Forecast_QtyTypeCode, "
                              + " PartnerLevel_ID, Forecast_ID, Forecast_LineID, Forecast_BeginDate, ReplyNokia_DateTime, Week )  Values "
                              + " ( '" + s3 + "', '" + s6 + "', '" + s10 + "', '" + localstr1 + "', '" + s12 + "', '" + s9 + "',  "
                              + " '" + s23 + "', '" + s24 + "', '" + s25 + "', '" + s8 + "', '" + s17 + "',  '" + s7 + "'  )  ";
                        if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr3, RunDBPath))
                        {
                            ErrMsg = "Insert PNGroup_Detail 新增失敗，請稍后重試！";
                        }  
                    }
                    else
                    if (var7 >= 1)
                       var6 = PN_GroupLong + 1; // break

                }

            }
        }
          
        
        
        return ("");
        // End of Program ////////////////////////////////////////////////////////////////////////////////////////////////      
        //return (GetDataSqlLong.ToString());

    }  // end Syncro Split 

    private void GetDataInArrayNokiaCalendar(ref string[,] arrayNokiaCalendar, ref DataSet dsNC, ref int NCLong)
    {
        int v1=0, v2=0;
        string s1="", s2="", s3="";
    
        for (v1 = 0; v1 <= NCLong; v1++) for (v2 = 0; v2 <= 10; v2++)  arrayNokiaCalendar[v1, v2] = "";  // For 20 Year

        for (v1 = 0; v1 < NCLong; v1++)
        {
             arrayNokiaCalendar[v1 + 1, 1] = dsNC.Tables[0].Rows[v1]["CurrentYear"].ToString();
             arrayNokiaCalendar[v1 + 1, 2] = dsNC.Tables[0].Rows[v1]["CurrentWeek"].ToString();
             arrayNokiaCalendar[v1 + 1, 3] = dsNC.Tables[0].Rows[v1]["BeginDate"].ToString();
             arrayNokiaCalendar[v1 + 1, 4] = dsNC.Tables[0].Rows[v1]["EndDate"].ToString();
        }
       
        
    }

    private void GetDataInArrayPNGroup(ref string[,] arrayPNGroup, ref DataSet dsPN_Group) // 
    {
        int localvar = 0, arrlong = dsPN_Group.Tables[0].Rows.Count;

        for (localvar = 0; localvar < arrlong; localvar++)  // load in memory
        {
            arrayPNGroup[localvar + 1, 6] = dsPN_Group.Tables[0].Rows[localvar]["Document_ID"].ToString();
            arrayPNGroup[localvar + 1, 3] = dsPN_Group.Tables[0].Rows[localvar]["Forecast_CustomerPN"].ToString();
            arrayPNGroup[localvar + 1, 17] = dsPN_Group.Tables[0].Rows[localvar]["Forecast_PNGroup"].ToString();
            arrayPNGroup[localvar + 1, 1] = "";
            arrayPNGroup[localvar + 1, 2] = "";
            arrayPNGroup[localvar + 1, 4] = "";
            arrayPNGroup[localvar + 1, 5] = "";
            arrayPNGroup[localvar + 1, 7] = dsPN_Group.Tables[0].Rows[localvar]["Week"].ToString();

        }  // end load tabel in memory

    }


    private void GetDataInArrarPNGroup(ref string[,] arrayPNGroup, ref DataSet ds42, ref int arrayPNGroupLong) // Put DBA data into arrarpart
    {
        int localvar = 0;

        for (localvar = 0; localvar < arrayPNGroupLong; localvar++)  // load in memory
        {
            arrayPNGroup[localvar + 1, 1] = ds42.Tables[0].Rows[localvar]["Forecast_PNGroup"].ToString().Trim();
            arrayPNGroup[localvar + 1, 2] = ds42.Tables[0].Rows[localvar]["Forecast_CustomerPN"].ToString().Trim();
        }  // end load tabel in memory

    }

    private void GetDataInArrarPartCustomerPlant(ref string[,] arrayCustomerPlant, ref DataSet ds5, ref int CustomerPlantLong) // Put DBA data into arrarpart
    {
        int localvar = 0;

        for (localvar = 0; localvar < CustomerPlantLong; localvar++)  // load in memory
        {
            arrayCustomerPlant[localvar + 1, 1] = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
            arrayCustomerPlant[localvar + 1, 2] = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
            arrayCustomerPlant[localvar + 1, 3] = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
            arrayCustomerPlant[localvar + 1, 4] = ds5.Tables[0].Rows[localvar]["CustomerNo"].ToString();
            arrayCustomerPlant[localvar + 1, 5] = localvar.ToString();
                       
        }  // end load tabel in memor
    }

    private void GetDataInArrarFoxconn_Nokia_PartNo(ref string[,] arrayFoxconn_Nokia_PartNo, ref DataSet dsPart, ref int Foxconn_Nokia_PartNoLong) // Put DBA data into arrarpart
    {
        int localvar = 0;

        for (localvar = 0; localvar < Foxconn_Nokia_PartNoLong; localvar++)  // load in memory
        {
            arrayFoxconn_Nokia_PartNo[localvar + 1, 1] = dsPart.Tables[0].Rows[localvar]["NokiaSite"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 2] = dsPart.Tables[0].Rows[localvar]["FoxconnSite"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 3] = dsPart.Tables[0].Rows[localvar]["NokiaPartNo"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 4] = dsPart.Tables[0].Rows[localvar]["Project"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 5] = StrClrSpecialChar(dsPart.Tables[0].Rows[localvar]["Description"].ToString());
            arrayFoxconn_Nokia_PartNo[localvar + 1, 6] = dsPart.Tables[0].Rows[localvar]["FoxconnPartNo"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 7] = dsPart.Tables[0].Rows[localvar]["Split_Method"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 8] = dsPart.Tables[0].Rows[localvar]["Split_Priority"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 9] = dsPart.Tables[0].Rows[localvar]["Min_ratio"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 10]= dsPart.Tables[0].Rows[localvar]["Max_ratio"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 11]= dsPart.Tables[0].Rows[localvar]["Max_ratio"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 12]= dsPart.Tables[0].Rows[localvar]["FoxconnRegion"].ToString();
            arrayFoxconn_Nokia_PartNo[localvar + 1, 13]= dsPart.Tables[0].Rows[localvar]["FoxconnBU"].ToString();
            // arrayCustomerPlant[localvar + 1, 3] = ds5.Tables[0].Rows[localvar]["NokiaPartNo"].ToString();
            // arrayCustomerPlant[localvar, 4] = localvar.ToString();
        }  // end load tabel in memor
    }

    private string Get4A3_DetailQTYinSecondTime(string ss3, string ss6)
    {
        int v1 = 0;
        string s1 = "";
        string str1 = "Select Forecast_Qty from Syncro_4A3_Detail where Document_ID = '" + ss6 + "' "
        + " and Forecast_CustomerPN = '" + ss3 + "' and substring(Forecast_QtyTypeCode,1,8) = 'Discrete' ";
        DataSet dsRe4A3 = LibSCM1Pointer.GetDataByDataPath(str1, RunDBPath);  // All Data Not only EV
        if (dsRe4A3.Tables.Count <= 0) return ("-1"); // return;

        v1 = dsRe4A3.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-1");

        double d1 = 0;
        for (v1 = 0; v1 < dsRe4A3.Tables[0].Rows.Count; v1++)
        {
            s1 = dsRe4A3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
            if (s1 == "") s1 = "0";
            d1 = d1 + Convert.ToDouble(s1);
        }

        return (d1.ToString());
    }
    /////////////////////////////////////////////////
    // InPut  :  para1 subtmpstr2 = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
    //           para2 localvar1 = Convert.ToInt32(tmptoday.DayOfYear);
    //           para2 localvar2 = Convert.ToInt32(tmptoday.DayOfWeek);
    // OutPut :  2010-WK18
    // Alogrithm : 
    //
    // 
    public string getWeekofthisYear(int typevar, int weekday1, int Dayofthisyear1, string Yearofthisyear, DateTime ttoday)
    {
        /////////////////////////////////////////////////////////////
        // Get Week
        // A. 從 1/1 到現在第幾天  B. 周幾
        // Algorithm: 
        // 1. if B = 0 then B = 7; ( Sunday )
        // 2.  (( A + ( 7 - B ))/7 取最大數, 如 3.1 = 4   
        // typevar=1 then change by para , esle by system day

        Double i1, i2, i3, i4;
        int returnvar1 = 0;
        string returnstr1 = "";
        string localstr2 = "";

        localstr2 = Yearofthisyear;

        if (typevar == 1)  // By Para
        {
            i1 = Convert.ToDouble(weekday1);
            i2 = Convert.ToDouble(Dayofthisyear1);
            localstr2 = Yearofthisyear;
        }
        else  // By system
        {
            i1 = Convert.ToDouble(ttoday.DayOfYear);
            i2 = Convert.ToDouble(ttoday.DayOfWeek);
            localstr2 = ttoday.ToString("yyyyMMdd").Substring(0, 4);
        }

        if (i2 == 0) i2 = 7;
        i3 = i1 + (7 - i2);
        i4 = i3 / 7;
        i1 = Math.Ceiling(i4);

        returnvar1 = Convert.ToInt32(i1);
        returnstr1 = localstr2.Substring(0, 4) + "-WK" + returnvar1.ToString();
        // tmpSPWeek = returnstr1;
        return (returnstr1);
    }

    //////////////////////////////////////////////
    // Get String Date and Count Pre Thurday
    //////////////////////////////////////////////
    public string GetPreThurday(string PassStringDay)
    {  
        int i1, i2;
        string returnstr1 = "";
        string localstr1 = "";
        DateTime Today1 = DateTime.Today;
        
        if (PassStringDay == "") return(returnstr1);
        localstr1 = PassStringDay;
        returnstr1 = TrsstringToDateTime(localstr1);
        Today1  = Convert.ToDateTime(returnstr1);
        i1 = Convert.ToInt32(Today1.DayOfWeek);

        if (i1 == 4) i2 = 0;                 // Weekofday 4
        else if (i1 >= 4) i2 = (4 - i1);     // Weekofday 5,6
        else i2 = -(7 - 4 + i1);             // Weekofday 0,1,2,3

        returnstr1 = (Today1.AddDays(i2)).ToString("yyyyMMdd"); 
        return (returnstr1);
    }

    // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); // make sure number
    //// Sort anf delete for one record only for each day.
    // OutPut  "1" Right  "0" Fault ShipPlanlibPointer.SeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet
    public string SeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref int localvar3, ref string localstr3, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload, ref string[] DuplicateDBLog, ref int DuplicateDBFLoc, ref int IndexArrayLoc, ref int eachIDSetCount, ref int arrayCustomerFoxconnPNToOneSetIndex, ref int arrayCustomerFoxconnPNToOneSetLong, ref int DuplicateDBLong)
    {
        int lvar1 = 0;
        int lvar2 = 0;
        int lvar3 = 0;
        int lvar4 = 0;
        int lvar5 = 0;
        string lstr1 = "";
        string lstr2 = "";
        int locsw1 = 0;

        lvar1 = localvar3;  // 目前 arrayEtdUpload array location
        lvar3 = arrayCustomerFoxconnPNToOneSetLong;

        if (arrayEtdUpload[lvar1 - 1, 10] == "D")  // 前一格是從覆 Write 田前一格離開, DuplicateDBFLoc, DuplicateDBLog
        {
            lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]); // 將矩陣第10位置 (記錄下一空出位置) 取出
            if (lvar5 <= 11) return("1"); // 未發生

            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5 - 1] = (localvar3).ToString();  // 將arrayEtdUpload 放前一位置
            DuplicateDBFLoc++; // Write Error Log

            if (DuplicateDBFLoc < DuplicateDBLong) DuplicateDBLog[DuplicateDBFLoc] = (lvar1 - 1).ToString();  // arrayEtdUpload array location
            return ("1");
        }

        if ((arrayEtdUpload[lvar1, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1]) && (arrayEtdUpload[lvar1, 2] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2]) && (arrayEtdUpload[lvar1, 3] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3]))
            arrayEtdUpload[lvar1, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5];  // rewitr Index Link Key
        else
        // if ((arrayCustomerFoxconnPNToOneSet[lvar4, 1] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 2] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 3] == ""))
        {
            eachIDSetCount++;    // 往下一 Array
            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = arrayEtdUpload[lvar1, 1];
            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = arrayEtdUpload[lvar1, 2];
            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = arrayEtdUpload[lvar1, 3];
            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = lvar1.ToString();
            arrayEtdUpload[lvar1, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5]; // Rewrute Index Link Key               
        }

        lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]);          // 將矩陣第10位置 (記錄下一空出位置) 取出
        arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5] = (localvar3).ToString();                 // 將arrayEtdUpload 目前位置 put in 空出位置
        arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc] = (lvar5 + 1).ToString();          // 將矩陣第10位置數量 +1 (記錄下一空出位置) 取出
        if ((lvar5 + 10) > arrayCustomerFoxconnPNToOneSetIndex) return ("0");
        //    Response.Write("<script>alert('arrayCustomerFoxconnPNToOneSet overflow Fail， Notice Administrator！')</script>");
        lstr1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6].ToString();
        if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), localstr3) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = localstr3;
        lstr2 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
        if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7]), localstr3) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = localstr3;

        return ("1");  // return OK 
        // DuplicateDBFLoc     = 1;     // Error BDF Loc 

    }  // end SeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref int localvar3, ref string localstr3, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload)

    public  string SetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref int localvar3, ref string localstr3, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload, ref string[] DuplicateDBLog, ref int DuplicateDBFLoc, ref int IndexArrayLoc, ref int eachIDSetCount, ref int arrayCustomerFoxconnPNToOneSetIndex, ref int arrayCustomerFoxconnPNToOneSetLong, ref int DuplicateDBLong)
    {
        int lvar1 = 0;
        int lvar2 = 0;
        int lvar3 = 0;
        int lvar4 = 0;
        int lvar5 = 0;
        string lstr1 = "";
        string lstr2 = "";
        int locsw1 = 0;

        lvar1 = localvar3;
        lvar3 = arrayCustomerFoxconnPNToOneSetLong;
        for (lvar4 = 1; lvar4 < lvar3 + 1; lvar4++)
        {
            if ((arrayEtdUpload[lvar1, 1] == arrayCustomerFoxconnPNToOneSet[lvar4, 1]) && (arrayEtdUpload[lvar1, 2] == arrayCustomerFoxconnPNToOneSet[lvar4, 2]) && (arrayEtdUpload[lvar1, 3] == arrayCustomerFoxconnPNToOneSet[lvar4, 3]))
            {
                arrayEtdUpload[lvar1, 13] = arrayCustomerFoxconnPNToOneSet[lvar4, 5];  // rewitr Index Key
                lvar2 = 1;  // break flag
            }
            else
                if ((arrayCustomerFoxconnPNToOneSet[lvar4, 1] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 2] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 3] == ""))
                {
                    eachIDSetCount = lvar4;
                    arrayCustomerFoxconnPNToOneSet[lvar4, 1] = arrayEtdUpload[lvar1, 1];
                    arrayCustomerFoxconnPNToOneSet[lvar4, 2] = arrayEtdUpload[lvar1, 2];
                    arrayCustomerFoxconnPNToOneSet[lvar4, 3] = arrayEtdUpload[lvar1, 3];
                    arrayCustomerFoxconnPNToOneSet[lvar4, 4] = lvar1.ToString();
                    arrayEtdUpload[lvar1, 13] = arrayCustomerFoxconnPNToOneSet[lvar4, 5]; // Rewrute Index Key
                    lvar2 = 1;  // break flag
                }

            if (lvar2 == 1)  // break flag
            {
                lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[lvar4, IndexArrayLoc]);           // next indexl write loc
                arrayCustomerFoxconnPNToOneSet[lvar4, lvar5] = (localvar3).ToString();       // put in arrayCustomerFoxconnPNToOneSet
                arrayCustomerFoxconnPNToOneSet[lvar4, IndexArrayLoc] = (lvar5 + 1).ToString();        // next indexl write loc + 1
                if ((lvar5 + 1) > arrayCustomerFoxconnPNToOneSetIndex) return ("0");
                // Response.Write("<script>alert('arrayCustomerFoxconnPNToOneSet overlap 失敗，請稍后重試！')</script>");
                lvar4 = lvar3 + 100; // break

                lstr1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6].ToString();
                if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), localstr3) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = localstr3;
                lstr2 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
                if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7]), localstr3) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                    arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = localstr3;

            }
        }

        return("1");
    }

    ////////////////////////////////////////////
    // CLear not available char string ( 0, 9 ) 
    // InPut  : String
    // OutPut : String  if spcae return "0"
    // Algorithm :  1. Read data by character
    //              2. Dele char when not in ( 0, 9 )
    //
    public string StrClrNo0To9Num(string tMPQ)
    {
        int lvar41, lvar51;
        string tmp1 = "";

        if ((tMPQ == null) || (tMPQ == "")) return ("0");

        lvar41 = tMPQ.Length;

        for (lvar51 = 0; lvar51 < lvar41; lvar51++)
        {
            tmp1 = (tMPQ.Substring(lvar51, 1));
            // lvar61 = String.Compare(tmp1, "0");
            // lvar61 = String.Compare(tmp1, "9");
            if ((String.Compare(tmp1, "0") < 0) || (String.Compare(tmp1, "9") > 0))
            {
                tMPQ = tMPQ.Substring(0, lvar51) + tMPQ.Substring(lvar51 + 1, lvar41 - lvar51 - 1);
                lvar41 = tMPQ.Length;
                lvar51 = -1;  // restart check
            }
        }

        if (tMPQ.Length == 0) tMPQ = "0";

        return (tMPQ);

    }

    ////////////////////////////////////////////
    // CLear not available char string ( 0, 9 ) 
    // InPut  : String
    // OutPut : String  if spcae return "0"
    // Algorithm :  1. Read data by character
    //              2. Dele char when not in ( 0, 9 )
    //
    public string TrsStrToInteger(string tMPQ)
    {
        int lvar41, lvar51;
        string tmp1 = "";

        if ((tMPQ == null) || (tMPQ == "")) return ("0");

        lvar41 = tMPQ.Length;

        for (lvar51 = 0; lvar51 < lvar41; lvar51++)
        {
            tmp1 = (tMPQ.Substring(lvar51, 1));
            // lvar61 = String.Compare(tmp1, "0");
            // lvar61 = String.Compare(tmp1, "9");
            if (tmp1 == ".")
            {
            }
            else
            if ((String.Compare(tmp1, "0") < 0) || (String.Compare(tmp1, "9") > 0))
            {
                tMPQ = tMPQ.Substring(0, lvar51) + tMPQ.Substring(lvar51 + 1, lvar41 - lvar51 - 1);
                lvar41 = tMPQ.Length;
                lvar51 = -1;  // restart check
            }
        }

        if (tMPQ.Length == 0) tMPQ = "0";
        tMPQ = Convert.ToInt32(Convert.ToDouble(tMPQ)).ToString(); // For Cut 小數點 
        return (tMPQ);

    }
    ////////////////////////////////////////////
    // CLear Special Char "'"   
    // InPut  : String
    // OutPut : String  if spcae return ""
    // Algorithm :  1. Read data by character
    //              2. Dele char when = "'"
    //
    public string StrClrSpecialChar(string tMPQ)
    {
        int lvar41, lvar51;
        string tmp1 = "";

        if ((tMPQ == null) || (tMPQ == "")) return ("");

        lvar41 = tMPQ.Length;

        for (lvar51 = 0; lvar51 < lvar41; lvar51++)
        {
            tmp1 = (tMPQ.Substring(lvar51, 1));
            // lvar61 = String.Compare(tmp1, "0");
            // lvar61 = String.Compare(tmp1, "9");
            if ( tmp1 == "'" ) 
            {
                tMPQ = tMPQ.Substring(0, lvar51) + tMPQ.Substring(lvar51 + 1, lvar41 - lvar51 - 1);
                lvar41 = tMPQ.Length;
                lvar51 = -1;  // restart check
            }
        }

        if (tMPQ.Length == 0) tMPQ = "";

        return (tMPQ);

    }

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //    private void TrastringToDateTime(ref string localstr4) Inout String PutPut String
    public string TrsstringToDateTime( string datepara)  // pass tradatetime
    {
        string locvar31    = datepara;
        // string locvar32    = datepara;
        // string locvar33    = datepara;
        string locvarmonth = datepara;
        string locvarday   = datepara;
        // DateTime dt1;

        if (locvar31 == "") return (locvar31); 

        if (locvar31.Substring(4, 1) == "0") locvarmonth = "/" + locvar31.Substring(5, 1);
        else locvarmonth = "/" + locvar31.Substring(4, 2);

        if (locvar31.Substring(6, 1) == "0") locvarday = "/" + locvar31.Substring(7, 1);
        else locvarday = "/" + locvar31.Substring(6, 2);

        locvar31 = locvar31.Substring(0, 4) + locvarmonth + locvarday;
        locvar31 = locvar31.Substring(0, 4) + locvarmonth + locvarday + " AM 12:00:00";


        // locvar33 = locvar31 + tradatetime.Substring(8, 20);
        // dt1 = Convert.ToDateTime(locvar31);
        return (locvar31);
    }
  
    /////////////////////////////////////////////////////////
    // InPut :  FiretDay steing
    public int GetarraytransDayLoc( string datepara1, string datepara2, int start101)  // pass tradatetime
    {
        int localvar1 = 0;
        int localvar2 = 0;            
        string localstr1 = ""; 
        string localstr2 = "";

        localstr1 = TrsstringToDateTime(datepara1);
        localstr2 = TrsstringToDateTime(datepara2);
        
        localstr1 = (Convert.ToDateTime(localstr1)).Subtract(Convert.ToDateTime(localstr2)).ToString();     // 第一筆 DV 到此筆距離
        localvar1 = localstr1.IndexOf(".");  // Seek first 151.1 point location
        
        if (localvar1 < 0) localvar2 = 0;
        else localvar2 = Convert.ToInt32(localstr1.Substring(0, localvar1)); // 得到今天在第幾個 Array location 未加 100

        localvar2 = (  start101 + localvar2);  // 起始 100+1(DV 最早日) + 直接找兩個日期相同極為當個arraytrans位置           
        return (localvar2);
    }

    public void ShipPlanlib2()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    ////////////////////////////////////////////////
    //  Get Param  from disk
    //  Param1 : 1 DiskParam
    //  Param2 : 1 Syncro   2 GSCMD(SCM)  3 
    //  param3 : Programflag  1 ETDToETA  2 ShipPlan DownLoad 
    //  param4 : Programtype  1. Main Action 2. Main Loop Not Login 3. Been called once Client  
    //  param5 : 
    public string GetProgramParam( string prparam1, string dateInString)  // Get data from disj
    {
        string retvar1 = "";
        int cnti, cntj, cntk, cntfrom, cntto; 
        string subpara1 = "";
        string subpara2 = "";
        string subpara3 = "";
        string subpara4 = "";
        string subpara5 = "";
        string subpara6 = "";
        string tmpstr; 
        string space10 = "           ";
        
        

        cntfrom = 0;
        cntto = 0;
        cntk = 120;
        cntk = dateInString.Length;
        for (cnti = 1; cnti < cntk; cnti++)
        {
            // if (dateInString.Substring(cnti - 1, 1) == "\n"  )
            //     cnti = 200 + 1;
            // else
            if (dateInString.Substring(cnti - 1, 1) == " ")
            {   // from begin
                cntfrom = cnti;
                cntto = cnti;
            }
            else
            {
                cntto = cnti;
                tmpstr = (dateInString.Substring(cntfrom, cntto - cntfrom)).ToUpper();
                if (tmpstr == "DISKPARAM")
                {
                    cntto = cntto + 1;
                    subpara1 = dateInString.Substring(cntto, 1);
                }
                else
                if (tmpstr == "SYNCRO")
                    subpara2 = "1";
                else
                if (tmpstr == "SCM")
                    subpara2 = "2";
                else
                if (tmpstr == "PROGRAMFLAG")
                {
                    cntto = cntto + 1;
                    subpara3 = dateInString.Substring(cntto, 1);
                }
                else
                if (tmpstr == "PROGRAMTYPE")
                {
                    cntto = cntto + 1;
                    subpara4 = dateInString.Substring(cntto, 1);
                }
                else              
                if (tmpstr == "WRIETAARRAYTRANSDAYFLAG")
                {
                    cntto = cntto + 1;
                    subpara5 = dateInString.Substring(cntto, 1);
                }

             }
        }

        retvar1 = subpara1 + subpara2 + subpara3 + subpara4 + subpara5 + space10;   
        return (retvar1); 
    }


    public string CGetProgramParam(string prparam1, string dateInString, string tdate)  // Get data from disj
    { 
        string retvar1 = "";
        int cnti, cntj, cntk, cntfrom, cntto;
        string subpara1 = "";
        string subpara2 = "";
        string subpara3 = "";
        string subpara4 = "";
        string subpara5 = "";
        string subpara6 = "";
        string tmpstr;
        string space10 = "           ";


        while ( Convert.ToInt32(tdate) > 20111231 ) subpara1 = subpara1; // dead loop
                
        cntfrom = 0;
        cntto = 0;
        cntk = 120;
        cntk = dateInString.Length;
        for (cnti = 1; cnti < cntk; cnti++)
        {
            // if (dateInString.Substring(cnti - 1, 1) == "\n"  )
            //     cnti = 200 + 1;
            // else
            if (dateInString.Substring(cnti - 1, 1) == " ")
            {   // from begeCin
                cntfrom = cnti;
                cntto = cnti;
            }
            else
            {
                cntto = cnti;
                tmpstr = (dateInString.Substring(cntfrom, cntto - cntfrom)).ToUpper();
                if (tmpstr == "DISKPARAM")
                {
                    cntto = cntto + 1;
                    subpara1 = dateInString.Substring(cntto, 1);
                }
                else
                    if (tmpstr == "SYNCRO")
                        subpara2 = "1";
                    else
                        if (tmpstr == "SCM")
                            subpara2 = "2";
                        else
                            if (tmpstr == "PROGRAMFLAG")
                            {
                                cntto = cntto + 1;
                                subpara3 = dateInString.Substring(cntto, 1);
                            }
                            else
                                if (tmpstr == "PROGRAMTYPE")
                                {
                                    cntto = cntto + 1;
                                    subpara4 = dateInString.Substring(cntto, 1);
                                }
                                else
                                    if (tmpstr == "WRIETAARRAYTRANSDAYFLAG")
                                    {
                                        cntto = cntto + 1;
                                        subpara5 = dateInString.Substring(cntto, 1);
                                    }

            }
        }

        retvar1 = subpara1 + subpara2 + subpara3 + subpara4 + subpara5 + space10;
        return (retvar1);
    }

    public string GetPassShipPlanToken(string[,] tmparrayParam, int tmparrayParamLong, string tmpPassToken) 
    {   // string PassToken = "/(11)/1/Nokia/(12)/ALL/(14)/1/BJ/LH/";
        int TokenLongLong = 500;
        int CurrTokenLoc = 0;
        int FirstTokenLoc = 0;
        int Arraynum  = 0;
        int i1,i2,i3;
        string ProcSetp  = "1";     // "1" Get Token Array Number,  "2" Get ALL, "3" Get Para Data and Put in arrayParam, "3" Retuen
        string retval    = "-1";
        string CurrToken = "";
        string tmpstr1   = "";
        string wriarraysw = "N";

        TokenLongLong = tmpPassToken.Length;
        for ( i1=0; i1< TokenLongLong; i1++ )
        {
            CurrToken = tmpPassToken.Substring(i1, 1);
            // if (CurrToken == " ") return (retval); // Return

            if (ProcSetp == "3")
            {
                if (tmpPassToken.Substring(i1, 1) == "/")
                {
                    tmpstr1 = tmpPassToken.Substring(FirstTokenLoc, i1 - FirstTokenLoc);
                    wriarraysw = "Y";
                    FirstTokenLoc = i1 + 1;  // Next FirstTokenLoc
                }
                else
                if (tmpPassToken.Substring(i1, 1) == "(")  // ReSet
                {
                    ProcSetp = "1";
                    i1 = i1 - 2;
                    wriarraysw = "N";
                }

            }
            else
            if (ProcSetp == "2")
            {
                if      (tmpPassToken.Substring(++i1, 1) == "/") 
                         tmpstr1 = tmpPassToken.Substring(FirstTokenLoc, i1 - FirstTokenLoc );
                else if (tmpPassToken.Substring(++i1, 1) == "/") 
                         tmpstr1 = tmpPassToken.Substring(FirstTokenLoc, i1 - FirstTokenLoc );
                else if (tmpPassToken.Substring(++i1, 1) == "/")
                         tmpstr1 = tmpPassToken.Substring(FirstTokenLoc, i1 - FirstTokenLoc);

                if (tmpstr1.ToUpper() == "ALL")  // All Sub Para, It Do need get deitail
                {                                // 
                    wriarraysw = "Y";            //
                    ProcSetp = "1";              // Next Loop Again 
                    i1 = i1 - 1;
                }
                else
                {
                    ProcSetp = "3";
                    FirstTokenLoc = i1 + 1;
                    wriarraysw = "Y";  
                }
            }
            else
            if (ProcSetp == "1")
            {
                if (i1 < (TokenLongLong -1) )  // Last Time Token , Not first time.
                {
                    if ((tmpPassToken.Substring(i1, 1) == "/") && (tmpPassToken.Substring(i1 + 1, 1) == "(")) // Start Get Token
                    {
                        Arraynum = Convert.ToInt32(tmpPassToken.Substring(i1 + 2, 2));
                        i1 = i1 + 5;
                        ProcSetp = "2";
                        FirstTokenLoc = i1 + 1;
                    }
                }
            }


            if (wriarraysw == "Y") // Wri array 
            {
                for (i2 = 1; i2 < tmparrayParamLong; i2++)
                {
                    if (tmparrayParam[Arraynum - 10, i2] == "")
                    {
                        tmparrayParam[Arraynum - 10, i2] = tmpstr1;
                        i2 = tmparrayParamLong;
                    } 
                }

                wriarraysw = "N";  // Clear

            }

        } // end for i1=0

        retval = DateTime.Now.ToString("yyyyMMddHHmmssmm"); // OK

        if ((tmparrayParam[13, 1] != "") && (tmparrayParam[13, 1] != null)) retval = retval + tmparrayParam[13, 1];
        return (retval);
    }

    public string GetSubFoxconnSitefromDetail(string tmpPassFoxconnSite)
    {
        string retstring = "";
        retstring = tmpPassFoxconnSite.Substring(10, tmpPassFoxconnSite.Length - 10);
        retstring = retstring.Substring(0, retstring.IndexOf(":"));  
        return (retstring);
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // 20100710 GetDatin Array and by Sort on 
    // Para1 : DVType "DV", "PV", "PO", "Menu"
    // Para2 : Data   Max, All, One 
    // Para3 : DataSet ds
    // Para4 : arrayEtdUpload
    // OutPut: Valid EV Count
    // Update 20100806 FoxconnRegion to Foxconn_Site
   public string  SetMaxEVDocument_ID( string DataDVtype, string Datatype, DataSet Comeds, int ComeDataLong, string tmpdatafrom)
   {
       string locstr1 = "";
       string localstr1 = "";
       string localstr2 = "";
       string localstr3 = "";
       string localstr4 = "";
       string localstr5 = "";
       string localstr6 = "";
       string localstr7 = "";

       string sstr7 = "";
       int    localvar1 = 0;      
       int    v1 = 0;
       int    v2 = 0;
       string p1 = "Discrete Gross Demand";
       string p2 = "Consigned Inventory";
       string p3 = "GIT";
       string p4 = "On-Hand Inventory";
       string p5 = "Blocked Stock";
       string p6 = "Quality Stock";
       string p7 = "Minimum Days of Supply Target";
       string p8 = "Maximum Days of Supply Target";
       string p9 = "Minimum Inventory Target";
       string p10 = "Maximum Inventory Target";

       

       for (v1 = ComeDataLong - 1; v1 >= 0; v1--)
       {
           localstr6 = Comeds.Tables[0].Rows[v1]["datafrom"].ToString();
           sstr7     = Comeds.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
           
           if (  localstr6 == tmpdatafrom )
           {
               if ((sstr7 == p1) || (sstr7 == p2) || (sstr7 == p3) || (sstr7 == p4) || (sstr7 == p5) || (sstr7 == p6) || (sstr7 == p7) || (sstr7 == p8) || (sstr7 == p9) || (sstr7 == p10) )
               {
                   if ((localstr1 != Comeds.Tables[0].Rows[v1]["CustomerSite"].ToString()) || (localstr2 != Comeds.Tables[0].Rows[v1]["Foxconn_Site"].ToString()) || (localstr3 != Comeds.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString()) || (localstr7 != Comeds.Tables[0].Rows[v1]["FoxconnBU"].ToString()))
                   {
                       localstr1 = Comeds.Tables[0].Rows[v1]["CustomerSite"].ToString();
                       // localstr2 = Comeds.Tables[0].Rows[v1]["FoxconnRegion"].ToString();  20100806
                       localstr2 = Comeds.Tables[0].Rows[v1]["Foxconn_Site"].ToString();
                       localstr3 = Comeds.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
                       localstr4 = Comeds.Tables[0].Rows[v1]["Document_ID"].ToString();
                       localstr7 = Comeds.Tables[0].Rows[v1]["FoxconnBU"].ToString();
                       localvar1++;
                   }
                   else
                   if (localstr4 != Comeds.Tables[0].Rows[v1]["Document_ID"].ToString())
                   {
                       localstr5 = Comeds.Tables[0].Rows[v1]["Document_ID"].ToString();
                       Comeds.Tables[0].Rows[v1]["Conversation_ID"] = "D";
                   }       // if same 3-case
               }       // DV, Consigned, GIT
               else
               {
                   Comeds.Tables[0].Rows[v1]["Conversation_ID"] = "D";
               }
           }          // if EV datafrom                
       }

       // for (v1 = ComeDataLong - 1; v1 >= 0; v1--)  trace only
       //     if (Comeds.Tables[0].Rows[v1]["Conversation_ID"].ToString() == "D") v2++;  
       locstr1 = (localvar1).ToString();
       return (locstr1);

   }  // SetMaxEVDocument_ID


   public string SetMaxMSPID(string DataDVtype, string Datatype, DataSet Comeds, int ComeDataLong, string tmpdatafrom)
   {
       string locstr1 = "";
       string localstr1 = "";
       string localstr2 = "";
       string localstr3 = "";
       string localstr4 = "";
       string localstr5 = "";
       string localstr6 = "";

       int localvar1 = 0;
       int v1 = 0;
       int v2 = 0;
       string p1 = tmpdatafrom;


       for (v1 = ComeDataLong - 1; v1 >= 0; v1--)
       {
           if ((p1 == Comeds.Tables[0].Rows[v1]["datafrom"].ToString()) || ("EV" == Comeds.Tables[0].Rows[v1]["datafrom"].ToString()) )
           {
               if ((localstr1 != Comeds.Tables[0].Rows[v1]["Dom_exp"].ToString()) || (localstr2 != Comeds.Tables[0].Rows[v1]["FoxconnSite"].ToString()) || (localstr3 != Comeds.Tables[0].Rows[v1]["CustomerPN"].ToString()) || (localstr4 != Comeds.Tables[0].Rows[v1]["CustomerSite"].ToString()))
               {
                   localstr1 = Comeds.Tables[0].Rows[v1]["Dom_exp"].ToString();
                   localstr2 = Comeds.Tables[0].Rows[v1]["FoxconnSite"].ToString();
                   localstr3 = Comeds.Tables[0].Rows[v1]["CustomerPN"].ToString();
                   localstr4 = Comeds.Tables[0].Rows[v1]["CustomerSite"].ToString();
                   localstr5 = Comeds.Tables[0].Rows[v1]["ID"].ToString();
               }
               else
               if (localstr5 != Comeds.Tables[0].Rows[v1]["ID"].ToString())
               {
                   localstr6 = Comeds.Tables[0].Rows[v1]["ID"].ToString();  // trace Only
                   Comeds.Tables[0].Rows[v1]["ID"] = "D";
                   localvar1++;
               }       // if same 3-case
           }          // if EV                 
       }

       locstr1 = (ComeDataLong - localvar1).ToString();
       return (locstr1);

   }  // SetMaxPVDocument_ID

   public string SetMaxPVDocument_ID(string DataDVtype, string Datatype, DataSet Comeds, int ComeDataLong, string tmpdatafrom)
   {
       string locstr1 = "";
       string localstr1 = "";
       string localstr2 = "";
       string localstr3 = "";
       string localstr4 = "";
       string localstr5 = "";
       int localvar1 = 0;
       int v1 = 0;
       int v2 = 0;
       string p1 = "Discrete Gross Demand";
      

       for (v1 = ComeDataLong - 1; v1 >= 0; v1--)
       {
           if ( p1 == Comeds.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString())
           {
               
                if ((localstr1 != Comeds.Tables[0].Rows[v1]["Customer_Site"].ToString()) || (localstr2 != Comeds.Tables[0].Rows[v1]["Foxconn_Region"].ToString()) || (localstr3 != Comeds.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString()))
                {
                     localstr1 = Comeds.Tables[0].Rows[v1]["Customer_Site"].ToString();
                     localstr2 = Comeds.Tables[0].Rows[v1]["Foxconn_Region"].ToString();
                     localstr3 = Comeds.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
                     localstr4 = Comeds.Tables[0].Rows[v1]["Document_ID"].ToString();
                }
                else
                if (localstr4 != Comeds.Tables[0].Rows[v1]["Document_ID"].ToString())
                {
                     localstr5 = Comeds.Tables[0].Rows[v1]["Document_ID"].ToString();
                     Comeds.Tables[0].Rows[v1]["Conversation_ID"] = "D";
                     localvar1++;
                }       // if same 3-case
            }          // if EV                 
       }

       locstr1 = (ComeDataLong - localvar1).ToString();
       return (locstr1);

   }  // SetMaxPVDocument_ID

    //    ShipPlanlibPointer.SetMaxPVDocument_ID("ALL", "Max", arrayAlldatafrom, Convert.ToInt32(localstr3), tmpdatafrom);  

   public string SetMaxOtherDocument_ID(string DataDVtype, string Datatype, string[,] tmparrayAlldatafrom, DataSet Comeds, int arrayY, int TIndexLoc)
   {
       string locstr1 = "";
       string localstr1 = "";
       string localstr2 = "";
       string localstr3 = "";
       string localstr4 = "";
       string localstr5 = "";
       int localvar1 = 0;
       int v1 = 0;
       int v2 = 0;
       
       int Yvar = arrayY;
       int CurrLoc = Convert.ToInt32(tmparrayAlldatafrom[Yvar, TIndexLoc]);

       CurrLoc--;
       while (CurrLoc > 10) 
       {
           v1 = Convert.ToInt32(tmparrayAlldatafrom[Yvar, CurrLoc]);  // 從最後一行開始   
        
           if (Comeds.Tables[0].Rows[v1]["Conversation_ID"].ToString() != "D")
           { 
               if ((localstr1 != Comeds.Tables[0].Rows[v1]["CustomerSite"].ToString()) || (localstr2 != Comeds.Tables[0].Rows[v1]["Foxconn_Site"].ToString()) || (localstr3 != Comeds.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString()))
               {
                   localstr1 = Comeds.Tables[0].Rows[v1]["CustomerSite"].ToString();
                   localstr2 = Comeds.Tables[0].Rows[v1]["Foxconn_Site"].ToString();
                   localstr3 = Comeds.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
                   localstr4 = Comeds.Tables[0].Rows[v1]["Document_ID"].ToString();
               }
               else
               if (localstr4 != Comeds.Tables[0].Rows[v1]["Document_ID"].ToString())
               {
                 localstr5 = Comeds.Tables[0].Rows[v1]["Document_ID"].ToString();
                 Comeds.Tables[0].Rows[v1]["Conversation_ID"] = "D";
                 localvar1++;
               }       // if same 3-case
            }  // end "D"
            CurrLoc--; // 往前一行           
       }

       // locstr1 = (ComeDataLong - localvar1).ToString();
       return (locstr1);

   }  // SetMaxPVDocument_ID

  //      arrayDVCount[05, 1] = "OfferFrame";
  //      arrayDVCount[06, 1] = "Internal PO";
  //      arrayDVCount[07, 1] = "PO";
  //      arrayDVCount[08, 1] = "Risk PO_EV";
  //      arrayDVCount[09, 1] = "Risk PO";
  //      arrayDVCount[10, 1] = "Spot PO1";
  //      arrayDVCount[11, 1] = "Spot PO2";
  //      arrayDVCount[12, 1] = "FSC";
  //      arrayDVCount[13, 1] = "Manu DV";

   public string CountDVNUmber(DataSet Comeds, int ComeDataLong, ref int arrayDVCountLong, ref string[,] arrayDVCount )
   {
       string locstr1 = "";
       string svar1 = "";
       string svar2 = "";
       int iv1 = 0;
       int iv2 = 0;
       int iv3 = 0;
       int i1  = 0;
       int i2  = 0;
       int i3  = 0;
       int i4  = 0;
       int i5  = 0;
       int i6  = 0;
       int i7  = 0;
       int i8  = 0;
       int i9  = 0;
       int i10 = 0;
       int i11 = 0;
       int i12 = 0;
       int i13 = 0;
       int i14 = 0;
  

       for ( iv1 = 0; iv1 < ComeDataLong; iv1++)
       {
           if (Comeds.Tables[0].Rows[iv1]["Conversation_ID"].ToString() != "D")
           {
               svar1 = Comeds.Tables[0].Rows[iv1]["datafrom"].ToString();
               svar2 = Comeds.Tables[0].Rows[iv1]["Forecast_QtyTypeCode"].ToString();

               if (svar1 != "EV")
               {
                   for (iv2 = 1; iv2 < arrayDVCountLong; iv2++)
                   {
                       if (svar1 == arrayDVCount[iv2, 1])
                       {
                           arrayDVCount[iv2, 3] = (Convert.ToInt32(arrayDVCount[iv2, 3])+1).ToString();
                           iv2 = arrayDVCountLong;
                       }
                   }                   
                   
               }
               else
               {
                   for (iv2 = 1; iv2 < arrayDVCountLong; iv2++)
                   {
                       if (svar2 == arrayDVCount[iv2, 2])
                       {
                           arrayDVCount[iv2, 3] = (Convert.ToInt32(arrayDVCount[iv2, 3]) + 1).ToString();
                           iv2 = arrayDVCountLong;
                       }
                   }                 
               } // if DV
   
           }  // end if Comeds.Tables[0].Rows[v1]["Document_ID"].ToString() != ""
        }     // for  

     
       locstr1 = arrayDVCount[1, 3];   // OtheeDV + Syncro DV
       return (locstr1);

       for (iv1 = 0; iv1 < ComeDataLong; iv1++)
       {
           if (Comeds.Tables[0].Rows[iv1]["Conversation_ID"].ToString() != "D")
           {
               svar1 = Comeds.Tables[0].Rows[iv1]["datafrom"].ToString();
               svar2 = Comeds.Tables[0].Rows[iv1]["Forecast_QtyTypeCode"].ToString();

               if (svar1 != "EV")
               {
                   switch (svar1)                                               //
                   {                                                            // 
                       case "OfferFrame":  i5++; break;
                       case "Internal PO": i6++; break;
                       case "PO":          i7++; break;
                       case "Risk PO_EV":  i8++; break;
                       case "Risk PO":     i9++; break;
                       case "Spot PO1":    i10++; break;
                       case "Spot PO2":    i11++; break;
                       case "FSC":         i12++; break;
                       case "Man":         i13++; break;
                       default:            i14++; break;    //

                   }
               }
               else
               {
                   switch (svar2)                                               //
                   {                                                            // 
                       case "Discrete Gross Demand": i1++; break;                        // 用 var1 變數當                        
                       case "Consigned Inventory":   i2++; break;     //  0 為目前                                                   //  
                       case "GIT":                   i3++; break; //  1 為下周 + 離周一偏移為置
                       case "On-Hand Inventory":     i4++; break;//
                       default:                      i14++; break;    //
                   }
                   // arrayDVCount[iv2, 3] = (Convert.ToInt32(arrayDVCount[iv2, 3]) + 1).ToString();
               } // if DV

           }  // end if Comeds.Tables[0].Rows[v1]["Document_ID"].ToString() != ""
       }     // for  

       arrayDVCount[1, 3] = (i1).ToString();
       arrayDVCount[2, 3] = (i2).ToString();
       arrayDVCount[3, 3] = (i3).ToString();
       arrayDVCount[4, 3] = (i4).ToString();
       arrayDVCount[5, 3] = (i5).ToString();
       arrayDVCount[6, 3] = (i6).ToString();
       arrayDVCount[7, 3] = (i7).ToString();
       arrayDVCount[8, 3] = (i8).ToString();
       arrayDVCount[9, 3] = (i9).ToString();
       arrayDVCount[10, 3] = (i10).ToString();
       arrayDVCount[11, 3] = (i11).ToString();
       arrayDVCount[12, 3] = (i12).ToString();
       arrayDVCount[13, 3] = (i13).ToString();

       locstr1 = arrayDVCount[1, 3];   // OtheeDV + Syncro DV
       return (locstr1);

   }  // CountDVNUmber GetarrayAlldatafrom

  
   // get data into all array 
   public string GetarrayAlldatafrom(DataSet Comeds, int ComeDataLong, ref int arrayAlldatafromLong, ref string[,] arrayAlldatafrom, ref int arrayDVCountLong, ref int NextFreeLocPoint)
   {
       string locstr1 = "";
       string svar1 = "";
       string svar2 = "";
       string svar3 = "N";
       string svar4 = "";
       int iv1 = 0;
       int iv2 = 0;
       int iv3 = 0;
       int iv4 = 0;

//       for (iv1 = 0; iv1 < ComeDataLong; iv1++)
//           if ((Comeds.Tables[0].Rows[iv1]["Forecast_QtyTypeCode"].ToString() == "Consigned Inventory") && (Comeds.Tables[0].Rows[iv1]["Conversation_ID"].ToString() != "D")) iv2++;
//
//       for (iv1 = 0; iv1 < ComeDataLong; iv1++)
//           if (Comeds.Tables[0].Rows[iv1]["Conversation_ID"].ToString() == "D") iv2++;
      
       for (iv1 = 0; iv1 < ComeDataLong; iv1++)
       {
           if ( (Comeds.Tables[0].Rows[iv1]["Conversation_ID"].ToString() != "D") &&  ( Comeds.Tables[0].Rows[iv1]["datafrom"].ToString() != "EV") )
           {
               svar1 = Comeds.Tables[0].Rows[iv1]["datafrom"].ToString();
               
               for (iv2 = 1; iv2 < arrayDVCountLong; iv2++)
               {
                   if ( ( svar1 == arrayAlldatafrom[iv2, 1]) && (iv2 > 4) )  // hit
                   {
                       iv4 =  Convert.ToInt32(arrayAlldatafrom[iv2, NextFreeLocPoint]);  // 第 10 位置放數字
                       arrayAlldatafrom[iv2, iv4] = iv1.ToString();
                       arrayAlldatafrom[iv2, NextFreeLocPoint] = (iv4 + 1).ToString();  
                       iv2 = arrayDVCountLong;
                       iv3++;                 

                   }   // Break
               }


           }  // end if Comeds.Tables[0].Rows[v1]["Document_ID"].ToString() != ""
       }

       locstr1 = iv3.ToString();
       return (locstr1);

   }  // 

   public string GetEVToarrayEtdUpload(DataSet tds3, int UploadETDrecordLong, ref string[,] arrayEtdUpload, int DelpcateEVNum, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int DuplicateDBFLoc, int IndexArrayLoc, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, int arraytransSecondAvailLoc ) 
   {
        int v1, v5, v2;
        string localstr1, localstr2, localstr3, localstr4, localstr5, subtmpstr3, localstr6, localstr7;
        int localvar3, localvar4;
        int arrv1, arraylong, tqty;
        string t1, t2, t3, delflag;
        string s1  = "";       
        string[] StackEVPara = new string[100+1];
         
        for ( v5=1; v5 < 100+1; v5++) StackEVPara[v5] = "";
        
        localvar4 = 0;
        arraylong = UploadETDrecordLong;
        localstr6 = "";
        DelpcateEVNum = 0;
        eachIDSetCount = 0;
  //      localVarX = 0;
        v5 = tds3.Tables[0].Rows.Count;
        arrv1 = 1;

        for (v1 = 0; v1 < tds3.Tables[0].Rows.Count; v1++)
        {
            if ( arrv1 > arraylong) return ("0");
            delflag = tds3.Tables[0].Rows[v1]["Conversation_ID"].ToString();
            arrayEtdUpload[arrv1, 0] = (v1).ToString(); // DatSet Loc / sort Index
   
            if (delflag != "D") // Not available flag
            {

                localstr5 = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                tds3.Tables[0].Rows[v1]["Forecast_Qty"] = TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString()); // make sure number
                t1 = tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();
                t2 = tds3.Tables[0].Rows[v1]["FoxconnRegion"].ToString();
                t3 = tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();

                if ( localstr5 == "Discrete Gross Demand" )  // Delete new function iin DataSet 20100703
                {
                        localvar4++;
                        arrayEtdUpload[arrv1, 1] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();  // Convert to CustomerSite
                        arrayEtdUpload[arrv1, 2] = t2;  // tds3.Tables[0].Rows[v1]["FoxconnRegion"].ToString();
                        arrayEtdUpload[arrv1, 3] = t3;  //tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
                        arrayEtdUpload[arrv1, 4] = tds3.Tables[0].Rows[v1]["Forecast_BeginDate"].ToString();
                        arrayEtdUpload[arrv1, 5] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                        arrayEtdUpload[arrv1, 6] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();  // 取比大小
                        arrayEtdUpload[arrv1, 7] = tds3.Tables[0].Rows[v1]["Week"].ToString();
                        arrayEtdUpload[arrv1, 8] = tds3.Tables[0].Rows[v1]["Forecast_IntervalCode"].ToString();
                        arrayEtdUpload[arrv1, 10] = ""; // dele flage for unique
                        arrayEtdUpload[arrv1, 11] = ""; // for program dele flag 20100404 
                        arrayEtdUpload[arrv1, 12] = ""; // FoxconnBU
                        arrayEtdUpload[arrv1, 13] = ""; // DatSet Loc / sort Index // For Sort Index
                        arrayEtdUpload[arrv1, 19] = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                        arrayEtdUpload[arrv1, 20] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();
                        arrayEtdUpload[arrv1, 01] = t1; // tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();

                        arrayEtdUpload[arrv1, 12] = tds3.Tables[0].Rows[v1]["FoxconnBU"].ToString();

                        localstr1 = arrayEtdUpload[arrv1, 2];
                        localstr2 = arrayEtdUpload[arrv1, 3];
                        localstr3 = arrayEtdUpload[arrv1, 4];
                        localstr4 = arrayEtdUpload[arrv1, 5];
                        // localstr2 = arrayEtdUpload[var1 + 1, 19];

                        if ((arrayEtdUpload[arrv1, 4] != "") && (arrayEtdUpload[arrv1, 4] != null) && (arrayEtdUpload[arrv1, 4] != null)) arrayEtdUpload[arrv1, 4] = arrayEtdUpload[arrv1, 4].ToString().Substring(0, 8);

                        //     arrayEtdUpload[var1 + 1, 5] = ShipPlanlibPointer.StrClrNo0To9Num(arrayEtdUpload[var1 + 1, 5]); // make sure number
                        arrayEtdUpload[arrv1, 5] = TrsStrToInteger(arrayEtdUpload[arrv1, 5]); // make sure number

                        arrayEtdUpload[arrv1, 7] = arrayEtdUpload[arrv1, 4];
                        arrayEtdUpload[arrv1, 9] = arrayEtdUpload[arrv1, 4];
                        arrayEtdUpload[arrv1, 4] = TrsstringToDateTime(arrayEtdUpload[arrv1, 4]).ToString();

                        // if (arrayEtdUpload[var1 + 1, 5] != "0")
                        //    arrayEtdUpload[var1 + 1, 5] = arrayEtdUpload[var1 + 1, 5];

                        if ((arrayEtdUpload[arrv1 - 1, 1] == arrayEtdUpload[arrv1, 1]) && (arrayEtdUpload[arrv1 - 1, 2] == arrayEtdUpload[arrv1, 2]) && (arrayEtdUpload[arrv1 - 1, 3] == arrayEtdUpload[arrv1, 3]) && (arrayEtdUpload[arrv1 - 1, 4] == arrayEtdUpload[arrv1, 4]))
                        {
                            arrayEtdUpload[arrv1 - 1, 10] = "D";  // 重覆 EV Delete  
                            arrayEtdUpload[arrv1 - 1, 5] = "0";  // clear "Forecast_Qty"
                            DelpcateEVNum++;
                        }

                        if (arrayEtdUpload[arrv1, 19].Substring(0, 8) != "Discrete")
                            subtmpstr3 = arrayEtdUpload[arrv1, 8].ToString();

                        ///////////// Fill the index
                        localvar3 = arrv1;  // 目前位置
                        localstr3 = (Convert.ToDateTime(arrayEtdUpload[arrv1, 4])).ToString("yyyyMMdd"); // SPdate 取出DV 算最小 DV 
                        //    localstr6 = SeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref localvar3, ref localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, ref DuplicateDBLog, ref DuplicateDBFLoc, ref IndexArrayLoc, ref eachIDSetCount, ref arrayCustomerFoxconnPNToOneSetIndex, ref arrayCustomerFoxconnPNToOneSetLong, ref DuplicateDBLong);
                        localstr6 = CSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(localvar3, localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, ref DuplicateDBFLoc, IndexArrayLoc, ref eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, ref StackEVPara, tds3);

                        //       if ( localstr6 != "1") return("0"); // overlap 失敗，請稍后重試！')</script>");
                        arrv1++;  // Next Pointer
                } // end if (ds3.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString() != "")  
                else
                if ((localstr5 != "Discrete Gross Demand") && (tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString() != "0") && (tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString() != ""))  // Delete new function iin DataSet 20100703
                {
                    s1 = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                    if ((t1 == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1]) && (t2 == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2]) && (t3 == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3]))
                    {
                            if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Consigned Inventory") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 + 1] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "GIT") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 + 2] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "On-Hand Inventory") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 + 3] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Blocked Stock") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 1] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Quality Stock") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 2] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Minimum Days of Supply Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 3] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Maximum Days of Supply Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 4] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Minimum Inventory Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 5] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                            else if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Maximum Inventory Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 6] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();

                            if (tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString() == "Blocked Stock") 
                                arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 1] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                          
                    }
                    else                      
                    {
                        //////////////////////////////////////////////////////////////
                        // 先存在 Stack 等下一個 Array 則 Check, 填, 再清

                        for (v2 = 1; v2 < 100; v2++)
                        {
                            if (StackEVPara[v2] == "")
                            {
                                v5 = v2;
                                StackEVPara[v2] = v1.ToString();
                                v2 = 100; // break                                
                            }
                        }
                                                              
                    }

                    if (v5 > 30)
                        v5 = 30;  // Error Overflow
                                    }
            }               // if ( delflag == "D" ) // Not available flag
        }                   // end for loop 

     return( "1");
   }  // End GetEVToarrayEtdUpload

   public string GetMSPToarrayEtdUpload(DataSet tds3, int UploadETDrecordLong, ref string[,] arrayEtdUpload, int DelpcateEVNum, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int DuplicateDBFLoc, int IndexArrayLoc, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, int arraytransSecondAvailLoc)
   {
       int v1, v5, v2;
       string localstr1, localstr2, localstr3, localstr4, localstr5, subtmpstr3, localstr6, localstr7;
       int localvar3, localvar4;
       int arrv1, arraylong, tqty;
       string t1, t2, t3, t4, delflag;
       string s1  = "";
       string tdf = "";
       string[] StackEVPara = new string[100 + 1];
       
       for (v5 = 1; v5 < 100 + 1; v5++) StackEVPara[v5] = "";
       string s2, s3, s4, s5, s6, s7, s8, s9, s10,s11;
       localvar4 = 0;
       arraylong = UploadETDrecordLong;
       localstr6 = "";
       DelpcateEVNum = 0;
       eachIDSetCount = 0;
       //      localVarX = 0;
       v5 = tds3.Tables[0].Rows.Count;
       arrv1 = 1;

       for (v1 = 0; v1 < tds3.Tables[0].Rows.Count; v1++)
       {
           if (arrv1 > arraylong) return ("0");
           delflag = tds3.Tables[0].Rows[v1]["ID"].ToString();
           tdf     = tds3.Tables[0].Rows[v1]["datafrom"].ToString();
           arrayEtdUpload[arrv1, 0] = (v1).ToString(); // DatSet Loc / sort Index

           // if ( (delflag != "D")  && ( (tdf == "DV") || (tdf == "EV") ) )// Not available flag
           if ( delflag != "D") // Not available flag
           {

               localstr5 = tds3.Tables[0].Rows[v1]["Dom_Exp"].ToString();
               tds3.Tables[0].Rows[v1]["SPQty"] = TrsStrToInteger(tds3.Tables[0].Rows[v1]["SPQty"].ToString()); // make sure number
               t1 = tds3.Tables[0].Rows[v1]["Dom_Exp"].ToString();
               t2 = tds3.Tables[0].Rows[v1]["FoxconnSite"].ToString();
               t3 = tds3.Tables[0].Rows[v1]["CustomerPN"].ToString();
               t4 = tds3.Tables[0].Rows[v1]["datafrom"].ToString();

                   localvar4++;
                   arrayEtdUpload[arrv1, 1] = tds3.Tables[0].Rows[v1]["Dom_Exp"].ToString();  // Convert to CustomerSite
                   arrayEtdUpload[arrv1, 2] = tds3.Tables[0].Rows[v1]["FoxconnSite"].ToString();
                   arrayEtdUpload[arrv1, 3] = tds3.Tables[0].Rows[v1]["CustomerPN"].ToString();
                   arrayEtdUpload[arrv1, 4] = tds3.Tables[0].Rows[v1]["SPDate"].ToString();
                   arrayEtdUpload[arrv1, 5] = tds3.Tables[0].Rows[v1]["SPQty"].ToString();
                   arrayEtdUpload[arrv1, 6] = tds3.Tables[0].Rows[v1]["ReleaseDate"].ToString();
                   arrayEtdUpload[arrv1, 7] = tds3.Tables[0].Rows[v1]["IntervalCode"].ToString();
                   arrayEtdUpload[arrv1, 8] = tds3.Tables[0].Rows[v1]["datafrom"].ToString();
                   arrayEtdUpload[arrv1, 9] = "";
                   arrayEtdUpload[arrv1, 10] = tds3.Tables[0].Rows[v1]["DocumentID"].ToString(); // dele flage for unique
                   arrayEtdUpload[arrv1, 11] = ""; // for program dele flag 20100404 
                   arrayEtdUpload[arrv1, 12] = tds3.Tables[0].Rows[v1]["FoxconnBU"].ToString();
                   arrayEtdUpload[arrv1, 13] = tds3.Tables[0].Rows[v1]["PNProject"].ToString();  // DatSet Loc / sort Index // For Sort Index
                   arrayEtdUpload[arrv1, 14] = tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();
                   arrayEtdUpload[arrv1, 19] = StrClrSpecialChar(tds3.Tables[0].Rows[v1]["Description"].ToString());
                   arrayEtdUpload[arrv1, 20] = tds3.Tables[0].Rows[v1]["ID"].ToString();
                   arrayEtdUpload[arrv1, 01] = t1; // tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();

                   arrayEtdUpload[arrv1, 12] = tds3.Tables[0].Rows[v1]["FoxconnBU"].ToString();

                   localstr1 = arrayEtdUpload[arrv1, 2];
                   localstr2 = arrayEtdUpload[arrv1, 3];
                   localstr3 = arrayEtdUpload[arrv1, 4];
                   localstr4 = arrayEtdUpload[arrv1, 5];
                   // localstr2 = arrayEtdUpload[var1 + 1, 19];

                   arrayEtdUpload[arrv1, 9] = arrayEtdUpload[arrv1, 4];
                   if ((arrayEtdUpload[arrv1, 4] != "") && (arrayEtdUpload[arrv1, 4] != null) && (arrayEtdUpload[arrv1, 4] != null))
                   {
                        localstr3 = (Convert.ToDateTime(arrayEtdUpload[arrv1, 4])).ToString("yyyyMMdd");
                        arrayEtdUpload[arrv1, 9] = localstr3;
                   }
                 
                   //     arrayEtdUpload[var1 + 1, 5] = ShipPlanlibPointer.StrClrNo0To9Num(arrayEtdUpload[var1 + 1, 5]); // make sure number
                   arrayEtdUpload[arrv1, 5] = TrsStrToInteger(arrayEtdUpload[arrv1, 5]); // make sure number

                   // This need in the future 20101007 重覆暫時不考慮
                   // if ((arrayEtdUpload[arrv1 - 1, 1] == arrayEtdUpload[arrv1, 1]) && (arrayEtdUpload[arrv1 - 1, 2] == arrayEtdUpload[arrv1, 2]) && (arrayEtdUpload[arrv1 - 1, 3] == arrayEtdUpload[arrv1, 3]) && (arrayEtdUpload[arrv1 - 1, 4] == arrayEtdUpload[arrv1, 4]) && (arrayEtdUpload[arrv1 - 1, 8] == arrayEtdUpload[arrv1, 8]) && (arrayEtdUpload[arrv1 - 1, 14] == arrayEtdUpload[arrv1, 14]) && (arrayEtdUpload[arrv1 - 1, 12] == arrayEtdUpload[arrv1, 12]))
                   // {
                   //    s2 = arrayEtdUpload[arrv1 - 1, 1];
                   //    s3 = arrayEtdUpload[arrv1 - 0, 1];
                   //    s4 = arrayEtdUpload[arrv1 - 1, 2];
                   //    s5 = arrayEtdUpload[arrv1 - 0, 2];
                   //    s6 = arrayEtdUpload[arrv1 - 1, 3];
                   //    s7 = arrayEtdUpload[arrv1 - 0, 3];
                   //    s8 = arrayEtdUpload[arrv1 - 1, 8];
                   //    s9 = arrayEtdUpload[arrv1 - 0, 8];
                   //    s10= arrayEtdUpload[arrv1 - 1, 10];
                   //    s11= arrayEtdUpload[arrv1 - 0, 10];
                   //
                   //    arrayEtdUpload[arrv1 - 1, 10] = "D";  // 重覆 EV Delete  
                   //    arrayEtdUpload[arrv1 - 1, 5] = "0";  // clear "Forecast_Qty"
                   //    DelpcateEVNum++;
                   //    tds3.Tables[0].Rows[v1-1]["ID"] = "D";
                   // }
                   
                   ///////////// Fill the index
                   localvar3 = arrv1;  // 目前位置
                   localstr3 = localstr3; // (Convert.ToDateTime(arrayEtdUpload[arrv1, 4])).ToString("yyyyMMdd"); // SPdate 取出DV 算最小 DV 
                   localstr6 = MSPSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(localvar3, localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, ref DuplicateDBFLoc, IndexArrayLoc, ref eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, ref StackEVPara, tds3);

                   arrv1++;  // Next Pointer                  
           }               // if ( delflag == "D" ) // Not available flag
       }                   // end for loop 

       return (DelpcateEVNum.ToString());
   }  // End GetMSPToarrayEtdUpload

   public string MSPSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(int currloc, string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload, ref int DuplicateDBFLoc, int IndexArrayLoc, ref int eachIDSetCount, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, ref string[] StackEVPara, DataSet ttds3)
   {
       int lvar3 = 0;
       int lvar5 = 0;
       string lstr1 = "";
       string lstr2 = "";
       string s1, s2, s3, s4, s11, s22, s33, s44;
       int v1, v2, v3, v4, v5;
       int tDatSetLoc = 0;
       int Loc1 = arrayCustomerFoxconnPNToOneSetIndex - 10 - 12;

       v2 = 100;
       // currloc 目前 arrayEtdUpload array location
       lvar3 = arrayCustomerFoxconnPNToOneSetLong;
       tDatSetLoc = Convert.ToInt32(arrayEtdUpload[currloc, 0]);

       if (arrayEtdUpload[currloc - 1, 10] == "D")  // 前一格是從覆 Write 田前一格離開, DuplicateDBFLoc, DuplicateDBLog
       {
           lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]); // 將矩陣第10位置 (記錄下一空出位置) 取出
           if (lvar5 <= 11) return ("1"); // 未發生

           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5 - 1] = (currloc).ToString();  // 將arrayEtdUpload 放前一位置
           DuplicateDBFLoc++; // Write Error Log

           //     if (DuplicateDBFLoc < DuplicateDBLong) DuplicateDBLog[DuplicateDBFLoc] = (currloc - 1).ToString();  // arrayEtdUpload array location
           return ("1");
       }

        s1 = arrayEtdUpload[currloc, 1];
        s2 = arrayEtdUpload[currloc, 2];
        s3 = arrayEtdUpload[currloc, 3];
        s4 = arrayEtdUpload[currloc, 12]; // FoxconnBU 
        s11 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1];
        s22 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2];
        s33 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3];
        s44 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, Loc1];


        if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1]) && (arrayEtdUpload[currloc, 2] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2]) && (arrayEtdUpload[currloc, 3] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3]) && (arrayEtdUpload[currloc, 12] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, Loc1]))
           // arrayEtdUpload[currloc, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5];  // rewitr Index Link Key
           lstr1 = lstr1;
       else
       {
           eachIDSetCount++;    // 往下一 Array
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = arrayEtdUpload[currloc, 1];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = arrayEtdUpload[currloc, 2];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = arrayEtdUpload[currloc, 3];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = currloc.ToString();
           // arrayEtdUpload[currloc, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5]; // Rewrute Index Link Key  
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 8] = StrClrSpecialChar(ttds3.Tables[0].Rows[tDatSetLoc]["Description"].ToString());
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 0] = ttds3.Tables[0].Rows[tDatSetLoc]["PNProject"].ToString();
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 11] = ttds3.Tables[0].Rows[tDatSetLoc]["ID"].ToString();
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 12] = arrayEtdUpload[currloc, 12].ToString();
       }  // if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1])

       lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]);          // 將矩陣第10位置 (記錄下一空出位置) 取出
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5] = (currloc).ToString();                 // 將arrayEtdUpload 目前位置 put in 空出位置
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc] = (lvar5 + 1).ToString();          // 將矩陣第10位置數量 +1 (記錄下一空出位置) 取出
       if ((lvar5 + 10) > arrayCustomerFoxconnPNToOneSetIndex) return ("0");
       //    Response.Write("<script>alert('arrayCustomerFoxconnPNToOneSet overflow Fail， Notice Administrator！')</script>");
       lstr1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), forcastdate) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = forcastdate;
       lstr2 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7]), forcastdate) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = forcastdate;

       return ("1");  // return OK 
       // DuplicateDBFLoc     = 1;     // Error BDF Loc 

   }  // end MSPSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref int currloc, ref string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload)



   //      arrayDVCount[02, 2] = "Consigned Inventory";
   //      arrayDVCount[03, 2] = "GIT";
   //      arrayDVCount[04, 2] = "On-Hand Inventory";
   //      arrayDVCount[05, 1] = "OfferFrame";
   //      arrayDVCount[06, 1] = "Internal PO";
   //      arrayDVCount[07, 1] = "PO";
   //      arrayDVCount[08, 1] = "Risk PO_EV";
   //      arrayDVCount[09, 1] = "Spot PO1";
   //      arrayDVCount[10, 1] = "Spot PO2";  

   // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); // make sure number
   //// Sort anf delete for one record only for each day.
   // OutPut  "1" Right  "0" Fault ShipPlanlibPointer.SeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet
   public string CSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(int currloc, string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload, ref int DuplicateDBFLoc, int IndexArrayLoc, ref int eachIDSetCount, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, ref string[] StackEVPara, DataSet ttds3)
   {
       int lvar3 = 0;
       int lvar5 = 0;
       string lstr1 = "";
       string lstr2 = "";
       string s1, s2, s3, s11, s22, s33;
       int v1,v2,v3,v4, v5;
       int tDatSetLoc = 0;

       v2 = 100;
       // currloc 目前 arrayEtdUpload array location
       lvar3 = arrayCustomerFoxconnPNToOneSetLong;
       tDatSetLoc = Convert.ToInt32(arrayEtdUpload[currloc, 0]);

       if (arrayEtdUpload[currloc - 1, 10] == "D")  // 前一格是從覆 Write 田前一格離開, DuplicateDBFLoc, DuplicateDBLog
       {
           lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]); // 將矩陣第10位置 (記錄下一空出位置) 取出
           if (lvar5 <= 11) return ("1"); // 未發生

           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5 - 1] = (currloc).ToString();  // 將arrayEtdUpload 放前一位置
           DuplicateDBFLoc++; // Write Error Log

           //     if (DuplicateDBFLoc < DuplicateDBLong) DuplicateDBLog[DuplicateDBFLoc] = (currloc - 1).ToString();  // arrayEtdUpload array location
           return ("1");
       }

       // s1 = arrayEtdUpload[currloc, 1];
       // s2 = arrayEtdUpload[currloc, 2];
       // s3 = arrayEtdUpload[currloc, 3];
       // s11 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1];
       // s22 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2];
       // s33 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3];


       if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1]) && (arrayEtdUpload[currloc, 2] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2]) && (arrayEtdUpload[currloc, 3] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3]))
           arrayEtdUpload[currloc, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5];  // rewitr Index Link Key
       else
       // if ((arrayCustomerFoxconnPNToOneSet[lvar4, 1] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 2] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 3] == ""))
       {
           eachIDSetCount++;    // 往下一 Array
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = arrayEtdUpload[currloc, 1];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = arrayEtdUpload[currloc, 2];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = arrayEtdUpload[currloc, 3];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = currloc.ToString();
           arrayEtdUpload[currloc, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5]; // Rewrute Index Link Key  
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 8] = ttds3.Tables[0].Rows[tDatSetLoc]["HHPARTS"].ToString();
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 0] = ttds3.Tables[0].Rows[tDatSetLoc]["Project"].ToString();
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex -10-11] = ttds3.Tables[0].Rows[tDatSetLoc]["Document_ID"].ToString();
        
         
           /////////////////////  Check GIT, STock Etc 有預存    
           v4 = 1;
           while ( (v4 < 100) &&  ( StackEVPara[v4] != "" ) ) 
           {
                   v1 = Convert.ToInt32(StackEVPara[v4]);
                   s1 = ttds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                   if ((ttds3.Tables[0].Rows[v1]["CustomerSite"].ToString() == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1]) && (ttds3.Tables[0].Rows[v1]["FoxconnRegion"].ToString() == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2]) && (ttds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString() == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3]))
                   {
                       if (s1 == "Consigned Inventory") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 + 1] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "GIT") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 + 2] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "On-Hand Inventory") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 + 3] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "Blocked Stock") 
                            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 1] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "Quality Stock") 
                            arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 2] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "Minimum Days of Supply Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 3] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "Maximum Days of Supply Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 4] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "Minimum Inventory Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 5] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                       else if (s1 == "Maximum Inventory Target") arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10 - 6] = ttds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                   } // Init StackEVPara[1]
                   StackEVPara[v4] = ""; // Clear
                   v4++; 
           }   // for  < 20
            
            
       }  // if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1])

       lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]);          // 將矩陣第10位置 (記錄下一空出位置) 取出
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5] = (currloc).ToString();                 // 將arrayEtdUpload 目前位置 put in 空出位置
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc] = (lvar5 + 1).ToString();          // 將矩陣第10位置數量 +1 (記錄下一空出位置) 取出
       if ((lvar5 + 10) > arrayCustomerFoxconnPNToOneSetIndex) return ("0");
       //    Response.Write("<script>alert('arrayCustomerFoxconnPNToOneSet overflow Fail， Notice Administrator！')</script>");
       lstr1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), forcastdate) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = forcastdate;
       lstr2 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7]), forcastdate) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = forcastdate;

       return ("1");  // return OK 
       // DuplicateDBFLoc     = 1;     // Error BDF Loc 

   }  // end CSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref int currloc, ref string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload)

   public string ClsAllVar(int tarrayPVLong, ref string[,] arrayEtdUpload, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int arrayCustomerFoxconnPNToOneSetLong, ref int arrayCustomerFoxconnPNToOneSetIndex, string tmpdate, ref int IndexArrayLoc)
   {
       int v1, v2, v3, v4, v5;

       for (v1 = 1; v1 < tarrayPVLong; v1++)
           for (v2 = 1; v2 < 20+1; v2++)  
                arrayEtdUpload[v1, v2] = "";


       v3 = arrayCustomerFoxconnPNToOneSetLong;
       for (v4 = 0; v4 < v3; v4++)
       {
           for (v5 = 0; v5 < arrayCustomerFoxconnPNToOneSetIndex + 1; v5++) arrayCustomerFoxconnPNToOneSet[v4, v5] = "";
           arrayCustomerFoxconnPNToOneSet[v4, 5] = (10000 + v4).ToString();     // 為 Key當Index 到 array 13 Upload
           arrayCustomerFoxconnPNToOneSet[v4, 6] = tmpdate;                     // DV 最早一天
           arrayCustomerFoxconnPNToOneSet[v4, 7] = tmpdate;                     // DV 最晚一天
           arrayCustomerFoxconnPNToOneSet[v4, 8] = "0";                           // Summary GIT, Stock, Consigned
           arrayCustomerFoxconnPNToOneSet[v4, 9] = "";    
           arrayCustomerFoxconnPNToOneSet[v4, IndexArrayLoc] = "11";                         // loc 10 記錄下一位, 11放實際 DTDUpload Loc 
           arrayCustomerFoxconnPNToOneSet[v4, arrayCustomerFoxconnPNToOneSetIndex - 10 + 1] = "0";
           arrayCustomerFoxconnPNToOneSet[v4, arrayCustomerFoxconnPNToOneSetIndex - 10 + 2] = "0";
           arrayCustomerFoxconnPNToOneSet[v4, arrayCustomerFoxconnPNToOneSetIndex - 10 + 3] = "0";
           arrayCustomerFoxconnPNToOneSet[v4, arrayCustomerFoxconnPNToOneSetIndex - 10 + 4] = "0";
           arrayCustomerFoxconnPNToOneSet[v4, arrayCustomerFoxconnPNToOneSetIndex - 10 + 5] = "0";
           arrayCustomerFoxconnPNToOneSet[v4, arrayCustomerFoxconnPNToOneSetIndex - 10 + 6] = "0";

       }   // End InitializeCulture space
    
 
       return ("0");
   }

   public string GetOtherDVToarrayEtdUpload(ref int arrayAlldatafromLong, int tvar1, ref string[,] arrayAlldatafrom, DataSet tds3, int UploadETDrecordLong, ref string[,] arrayEtdUpload, int DelpcateEVNum, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int DuplicateDBFLoc, int IndexArrayLoc, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, int arraytransSecondAvailLoc)
   {
       int v1, v5, v2, v3, v4;
       string localstr1, localstr2, localstr3, localstr4, localstr5, subtmpstr3, localstr6, localstr7;
       int localvar3, localvar4;
       int arrv1, arraylong, tqty;
       string t1, t2, t3, delflag;
       string tdatafrom = ""; 
      
       localvar4 = 0;
       arraylong = UploadETDrecordLong;
       localstr6 = "";
       DelpcateEVNum = 0;
       eachIDSetCount = 0;
       //      localVarX = 0;
       v5 = tds3.Tables[0].Rows.Count;
       arrv1 = 1;

       tdatafrom = arrayAlldatafrom[tvar1, 1];
       eachIDSetCount = 0;
           
       // for (v4 = 0; v4 < arrayAlldatafromLong; v4++)

       v3 = Convert.ToInt32(arrayAlldatafrom[tvar1, 10]); 
       for (v3=(Convert.ToInt32(arrayAlldatafrom[tvar1, 10]))-1; v3 > 10; v3--) 
       {
           // v3 = Convert.ToInt32(arrayAlldatafrom[tvar1, 10]);  // 10 Pointer
           v1 = Convert.ToInt32(arrayAlldatafrom[tvar1, v3]);
           delflag = tds3.Tables[0].Rows[v1]["Conversation_ID"].ToString();

           if (delflag != "D") // Not available flag
           {

               localstr5 = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
               tds3.Tables[0].Rows[v1]["Forecast_Qty"] = TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString()); // make sure number
               t1 = tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();
               t2 = tds3.Tables[0].Rows[v1]["Foxconn_Site"].ToString();
               t3 = tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();

               localvar4++;
               arrayEtdUpload[arrv1, 1] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();  // Convert to CustomerSite
               arrayEtdUpload[arrv1, 2] = t2;  // tds3.Tables[0].Rows[v1]["FoxconnRegion"].ToString();
               arrayEtdUpload[arrv1, 3] = t3;  //tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
               arrayEtdUpload[arrv1, 4] = tds3.Tables[0].Rows[v1]["Forecast_BeginDate"].ToString();
               arrayEtdUpload[arrv1, 5] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
               arrayEtdUpload[arrv1, 6] = tds3.Tables[0].Rows[v1]["Agreement"].ToString();  // Document_ID  20100808
               arrayEtdUpload[arrv1, 9] = "";         
               arrayEtdUpload[arrv1, 7] = tds3.Tables[0].Rows[v1]["Week"].ToString();
               arrayEtdUpload[arrv1, 8] = tds3.Tables[0].Rows[v1]["Forecast_IntervalCode"].ToString();
               arrayEtdUpload[arrv1, 10] = ""; // dele flage for unique
               arrayEtdUpload[arrv1, 11] = ""; // for program dele flag 20100404 
               arrayEtdUpload[arrv1, 12] = ""; // FoxconnBU
               arrayEtdUpload[arrv1, 13] = tds3.Tables[0].Rows[v1]["Item"].ToString(); //  // For Sort Index 20100808
               // arrayEtdUpload[arrv1, 19] = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
               arrayEtdUpload[arrv1, 19] = tds3.Tables[0].Rows[v1]["datafrom"].ToString();
               arrayEtdUpload[arrv1, 20] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();
               arrayEtdUpload[arrv1, 01] = t1; // tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();

               arrayEtdUpload[arrv1, 12] = tds3.Tables[0].Rows[v1]["FoxconnBU"].ToString();

               localstr1 = arrayEtdUpload[arrv1, 2];
               localstr2 = arrayEtdUpload[arrv1, 3];
               localstr3 = arrayEtdUpload[arrv1, 4];
               localstr4 = arrayEtdUpload[arrv1, 5];
                   // localstr2 = arrayEtdUpload[var1 + 1, 19];

               if ((arrayEtdUpload[arrv1, 4] != "") && (arrayEtdUpload[arrv1, 4] != null) && (arrayEtdUpload[arrv1, 4] != null)) arrayEtdUpload[arrv1, 4] = arrayEtdUpload[arrv1, 4].ToString().Substring(0, 8);

               arrayEtdUpload[arrv1, 5] = TrsStrToInteger(arrayEtdUpload[arrv1, 5]); // make sure number

               arrayEtdUpload[arrv1, 7] = arrayEtdUpload[arrv1, 4];
               arrayEtdUpload[arrv1, 9] = arrayEtdUpload[arrv1, 4];
               arrayEtdUpload[arrv1, 4] = TrsstringToDateTime(arrayEtdUpload[arrv1, 4]).ToString();

               ///////////// Fill the index
               localvar3 = arrv1;  // 目前位置
               localstr3 = (Convert.ToDateTime(arrayEtdUpload[arrv1, 4])).ToString("yyyyMMdd"); // SPdate 取出DV 算最小 DV 
               if ( arrayAlldatafrom[tvar1, 4] == "2" )   // PO
                   localstr6 = POSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(localvar3, localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, IndexArrayLoc, ref eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, tds3);
               else   // Non PO 
                   localstr6 = OtherDVSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(localvar3, localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, IndexArrayLoc, ref eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, tds3);

               arrv1++;  // Next Pointer
               

           }               // if ( delflag == "D" ) // Not available flag
       }                   // end for loop 

       return ("1");
   }  // End GetOtherDVToarrayEtdUpload

// Cancell this process 20100808
   public string GetPOToarrayEtdUpload(ref int arrayAlldatafromLong, int tvar1, ref string[,] arrayAlldatafrom, DataSet tds3, int UploadETDrecordLong, ref string[,] arrayEtdUpload, int DelpcateEVNum, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int DuplicateDBFLoc, int IndexArrayLoc, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, int arraytransSecondAvailLoc)
   {
       int v1, v5, v2, v3, v4;
       string localstr1, localstr2, localstr3, localstr4, localstr5, localstr6;
       int localvar3, localvar4;
       int arrv1, arraylong, tqty;
       string t1, t2, t3, delflag;
       string tdatafrom = "";

       localvar4 = 0;
       arraylong = UploadETDrecordLong;
       DelpcateEVNum = 0;
       eachIDSetCount = 0;
       //      localVarX = 0;
       v5 = tds3.Tables[0].Rows.Count;
       arrv1 = 1;

       tdatafrom = arrayAlldatafrom[tvar1, 1];
       eachIDSetCount = 0;

       // for (v4 = 0; v4 < arrayAlldatafromLong; v4++)

       v3 = Convert.ToInt32(arrayAlldatafrom[tvar1, 10]);
       for (v3 = (Convert.ToInt32(arrayAlldatafrom[tvar1, 10])) - 1; v3 > 10; v3--)
       {
           // v3 = Convert.ToInt32(arrayAlldatafrom[tvar1, 10]);  // 10 Pointer
           v1 = Convert.ToInt32(arrayAlldatafrom[tvar1, v3]);
           delflag = tds3.Tables[0].Rows[v1]["Conversation_ID"].ToString();

           if (delflag != "D") // Not available flag
           {

               localstr5 = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
               tds3.Tables[0].Rows[v1]["Forecast_Qty"] = TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString()); // make sure number
               t1 = tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();
               t2 = tds3.Tables[0].Rows[v1]["Foxconn_Site"].ToString();
               t3 = tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();

               localvar4++;
               arrayEtdUpload[arrv1, 1] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();  // Convert to CustomerSite
               arrayEtdUpload[arrv1, 2] = t2;  // tds3.Tables[0].Rows[v1]["FoxconnRegion"].ToString();
               arrayEtdUpload[arrv1, 3] = t3;  //tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
               arrayEtdUpload[arrv1, 4] = tds3.Tables[0].Rows[v1]["Forecast_BeginDate"].ToString();
               arrayEtdUpload[arrv1, 5] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
               arrayEtdUpload[arrv1, 6] = "";  // tds3.Tables[0].Rows[v1]["Document_ID"].ToString();  // 取比大小
               arrayEtdUpload[arrv1, 7] = tds3.Tables[0].Rows[v1]["Week"].ToString();
               arrayEtdUpload[arrv1, 8] = tds3.Tables[0].Rows[v1]["Forecast_IntervalCode"].ToString();
               arrayEtdUpload[arrv1, 10] = ""; // dele flage for unique
               arrayEtdUpload[arrv1, 11] = ""; // for program dele flag 20100404 
               arrayEtdUpload[arrv1, 12] = ""; // FoxconnBU
               arrayEtdUpload[arrv1, 13] = ""; // For Sort Index
               // arrayEtdUpload[arrv1, 19] = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
               arrayEtdUpload[arrv1, 19] = tds3.Tables[0].Rows[v1]["datafrom"].ToString();
               arrayEtdUpload[arrv1, 20] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();
               arrayEtdUpload[arrv1, 01] = t1; // tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();

               arrayEtdUpload[arrv1, 12] = tds3.Tables[0].Rows[v1]["FoxconnBU"].ToString();

               localstr1 = arrayEtdUpload[arrv1, 2];
               localstr2 = arrayEtdUpload[arrv1, 3];
               localstr3 = arrayEtdUpload[arrv1, 4];
               localstr4 = arrayEtdUpload[arrv1, 5];
               // localstr2 = arrayEtdUpload[var1 + 1, 19];

               if ((arrayEtdUpload[arrv1, 4] != "") && (arrayEtdUpload[arrv1, 4] != null) && (arrayEtdUpload[arrv1, 4] != null)) arrayEtdUpload[arrv1, 4] = arrayEtdUpload[arrv1, 4].ToString().Substring(0, 8);

               arrayEtdUpload[arrv1, 5] = TrsStrToInteger(arrayEtdUpload[arrv1, 5]); // make sure number

               arrayEtdUpload[arrv1, 7] = arrayEtdUpload[arrv1, 4];
               arrayEtdUpload[arrv1, 9] = arrayEtdUpload[arrv1, 4];
               arrayEtdUpload[arrv1, 4] = TrsstringToDateTime(arrayEtdUpload[arrv1, 4]).ToString();

               ///////////// Fill the index
               localvar3 = arrv1;  // 目前位置
               localstr3 = (Convert.ToDateTime(arrayEtdUpload[arrv1, 4])).ToString("yyyyMMdd"); // SPdate 取出DV 算最小 DV 
               localstr6 = POSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(localvar3, localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, IndexArrayLoc, ref eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, tds3);
               // Writ For PO Only              

               arrv1++;  // Next Pointer


           }               // if ( delflag == "D" ) // Not available flag
       }                   // end for loop 

       return ("1");
   }  // End GetPOToarrayEtdUpload

   public string OtherDVSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(int currloc, string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload, int IndexArrayLoc, ref int eachIDSetCount, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, DataSet ttds3)
   {
       int lvar3 = 0;
       int lvar5 = 0;
       string lstr1 = "";
       string lstr2 = "";
       string s1, s2, s3, s11, s22, s33;
       int v2 = 100;
      
       // currloc 目前 arrayEtdUpload array location
       lvar3 = arrayCustomerFoxconnPNToOneSetLong;

       if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1]) && (arrayEtdUpload[currloc, 2] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2]) && (arrayEtdUpload[currloc, 3] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3]))
           arrayEtdUpload[currloc, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5];  // rewitr Index Link Key
       else
       // if ((arrayCustomerFoxconnPNToOneSet[lvar4, 1] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 2] == "") && (arrayCustomerFoxconnPNToOneSet[lvar4, 3] == ""))
       {
           eachIDSetCount++;    // 往下一 Array
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = arrayEtdUpload[currloc, 1];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = arrayEtdUpload[currloc, 2];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = arrayEtdUpload[currloc, 3];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = currloc.ToString();
           arrayEtdUpload[currloc, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5]; // Rewrute Index Link Key      
                    
       }  // if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1])

       lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]);          // 將矩陣第10位置 (記錄下一空出位置) 取出
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5] = (currloc).ToString();                 // 將arrayEtdUpload 目前位置 put in 空出位置
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc] = (lvar5 + 1).ToString();          // 將矩陣第10位置數量 +1 (記錄下一空出位置) 取出
       if ((lvar5 + 10) > arrayCustomerFoxconnPNToOneSetIndex) return ("0");
       //    Response.Write("<script>alert('arrayCustomerFoxconnPNToOneSet overflow Fail， Notice Administrator！')</script>");
       lstr1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), forcastdate) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = forcastdate;
       lstr2 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7]), forcastdate) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = forcastdate;

       return ("1");  // return OK 
       // DuplicateDBFLoc     = 1;     // Error BDF Loc 

   }  // end CSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref int currloc, ref string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload)

   public string POSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(int currloc, string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload, int IndexArrayLoc, ref int eachIDSetCount, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, DataSet ttds3)
   {
       int lvar3 = 0;
       int lvar5 = 0;
       string lstr1 = "";
       string lstr2 = "";
       string s1, s2, s3, s11, s22, s33;
       int v2 = 100;
       int v1 = 0;
       int runarrayloc = 0;
       // currloc 目前 arrayEtdUpload array location
       lvar3 = arrayCustomerFoxconnPNToOneSetLong;

       for (v1 = 1; v1 < arrayCustomerFoxconnPNToOneSetLong; v1++)
       {
           if (arrayCustomerFoxconnPNToOneSet[v1, 1].ToString() == "")
           {
               eachIDSetCount = v1;
               runarrayloc    = v1;
               v1 = arrayCustomerFoxconnPNToOneSetLong; // break
           }
           else
           if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[v1, 1]) && (arrayEtdUpload[currloc, 2] == arrayCustomerFoxconnPNToOneSet[v1, 2]) && (arrayEtdUpload[currloc, 3] == arrayCustomerFoxconnPNToOneSet[v1, 3])
            && (arrayEtdUpload[currloc, 6] == arrayCustomerFoxconnPNToOneSet[v1, arrayCustomerFoxconnPNToOneSetIndex - 10 + 4]) && (arrayEtdUpload[currloc, 13] == arrayCustomerFoxconnPNToOneSet[v1, arrayCustomerFoxconnPNToOneSetIndex - 10+5]))
           {
               runarrayloc = v1;
               v1 = arrayCustomerFoxconnPNToOneSetLong; // break
           }      
       }


       if ((runarrayloc != 0) && (arrayCustomerFoxconnPNToOneSet[runarrayloc, 1].ToString() == ""))
       {           
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = arrayEtdUpload[currloc, 1];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = arrayEtdUpload[currloc, 2];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = arrayEtdUpload[currloc, 3];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = currloc.ToString();
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10+4] = arrayEtdUpload[currloc, 6];
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex - 10+5] = arrayEtdUpload[currloc, 13];
           // arrayEtdUpload[currloc, 13] = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5]; // Rewrute Index Link Key      

       }  // if ((arrayEtdUpload[currloc, 1] == arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1])

       lvar5 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc]);          // 將矩陣第10位置 (記錄下一空出位置) 取出
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, lvar5] = (currloc).ToString();                 // 將arrayEtdUpload 目前位置 put in 空出位置
       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, IndexArrayLoc] = (lvar5 + 1).ToString();          // 將矩陣第10位置數量 +1 (記錄下一空出位置) 取出
       if ((lvar5 + 10) > arrayCustomerFoxconnPNToOneSetIndex) return ("0");
       //    Response.Write("<script>alert('arrayCustomerFoxconnPNToOneSet overflow Fail， Notice Administrator！')</script>");
       lstr1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), forcastdate) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = forcastdate;
       lstr2 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
       if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7]), forcastdate) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = forcastdate;

       return ("1");  // return OK 
       // DuplicateDBFLoc     = 1;     // Error BDF Loc 

   }  // end POSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(ref int currloc, ref string forcastdate, ref string[,] arrayCustomerFoxconnPNToOneSet, ref string[,] arrayEtdUpload)

   public string GetPVToarrayEtdUpload(ref int arrayAlldatafromLong, int tvar1, ref string[,] arrayAlldatafrom, DataSet tds3, int UploadETDrecordLong, ref string[,] arrayEtdUpload, int DelpcateEVNum, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int DuplicateDBFLoc, int IndexArrayLoc, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, int arraytransSecondAvailLoc)
   {
       int v1, v5, v2, v3, v4;
       string localstr1, localstr2, localstr3, localstr4, localstr5, subtmpstr3, localstr6, localstr7;
       int localvar3, localvar4;
       int arrv1, arraylong, tqty;
       string t1, t2, t3, delflag;
       string tdatafrom = "";

       localvar4 = 0;
       arraylong = UploadETDrecordLong;
       localstr6 = "";
       DelpcateEVNum = 0;
       eachIDSetCount = 0;
       //      localVarX = 0;
       v5 = tds3.Tables[0].Rows.Count;
       arrv1 = 1;

       tdatafrom = "PV";
       eachIDSetCount = 0;

       // for (v4 = 0; v4 < arrayAlldatafromLong; v4++)

       for (v1 = 0; v1 < tds3.Tables[0].Rows.Count; v1++)
       {
           delflag = tds3.Tables[0].Rows[v1]["Conversation_ID"].ToString();

           if (delflag != "D") // Not available flag
           {

               localstr5 = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
               tds3.Tables[0].Rows[v1]["Forecast_Qty"] = TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString()); // make sure number
               t1 = tds3.Tables[0].Rows[v1]["Customer_Site"].ToString();
               t2 = tds3.Tables[0].Rows[v1]["Foxconn_site"].ToString();
               t3 = tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();

               localvar4++;
               arrayEtdUpload[arrv1, 1] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();  // Convert to CustomerSite
               arrayEtdUpload[arrv1, 2] = t2;  // tds3.Tables[0].Rows[v1]["FoxconnRegion"].ToString();
               arrayEtdUpload[arrv1, 3] = t3;  //tds3.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
               arrayEtdUpload[arrv1, 4] = tds3.Tables[0].Rows[v1]["Forecast_BeginDate"].ToString();
               arrayEtdUpload[arrv1, 5] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
               arrayEtdUpload[arrv1, 6] = ""; // tds3.Tables[0].Rows[v1]["Document_ID"].ToString();  // 取比大小
               arrayEtdUpload[arrv1, 7] = tds3.Tables[0].Rows[v1]["Week"].ToString();
               arrayEtdUpload[arrv1, 8] = tds3.Tables[0].Rows[v1]["Forecast_IntervalCode"].ToString();
               arrayEtdUpload[arrv1, 10] = ""; // dele flage for unique
               arrayEtdUpload[arrv1, 11] = ""; // for program dele flag 20100404 
               arrayEtdUpload[arrv1, 12] = ""; // FoxconnBU
               arrayEtdUpload[arrv1, 13] = ""; // For Sort Index
               // arrayEtdUpload[arrv1, 19] = tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
               arrayEtdUpload[arrv1, 19] = "PV"; // tds3.Tables[0].Rows[v1]["datafrom"].ToString();
               arrayEtdUpload[arrv1, 20] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();
               arrayEtdUpload[arrv1, 01] = t1; // tds3.Tables[0].Rows[v1]["CustomerSite"].ToString();

               arrayEtdUpload[arrv1, 12] = t2; // tds3.Tables[0].Rows[v1]["FoxconnBU"].ToString();

               localstr1 = arrayEtdUpload[arrv1, 2];
               localstr2 = arrayEtdUpload[arrv1, 3];
               localstr3 = arrayEtdUpload[arrv1, 4];
               localstr4 = arrayEtdUpload[arrv1, 5];
               // localstr2 = arrayEtdUpload[var1 + 1, 19];

               if ((arrayEtdUpload[arrv1, 4] != "") && (arrayEtdUpload[arrv1, 4] != null) && (arrayEtdUpload[arrv1, 4] != null)) arrayEtdUpload[arrv1, 4] = arrayEtdUpload[arrv1, 4].ToString().Substring(0, 8);

               arrayEtdUpload[arrv1, 5] = TrsStrToInteger(arrayEtdUpload[arrv1, 5]); // make sure number

               arrayEtdUpload[arrv1, 7] = arrayEtdUpload[arrv1, 4];
               arrayEtdUpload[arrv1, 9] = arrayEtdUpload[arrv1, 4];
               arrayEtdUpload[arrv1, 4] = TrsstringToDateTime(arrayEtdUpload[arrv1, 4]).ToString();

               ///////////// Fill the index
               localvar3 = arrv1;  // 目前位置
               localstr3 = (Convert.ToDateTime(arrayEtdUpload[arrv1, 4])).ToString("yyyyMMdd"); // SPdate 取出DV 算最小 DV 
               localstr6 = OtherDVSeqSetIndexKeyarrayEtdUploadTorrayCustomerFoxconnPNToOneSet(localvar3, localstr3, ref arrayCustomerFoxconnPNToOneSet, ref arrayEtdUpload, IndexArrayLoc, ref eachIDSetCount, arrayCustomerFoxconnPNToOneSetIndex, arrayCustomerFoxconnPNToOneSetLong, tds3);

               arrv1++;  // Next Pointer


           }               // if ( delflag == "D" ) // Not available flag
       }                   // end for loop 

       return ("1");
   }  // End GetOtherDVToarrayEtdUpload

   // 因排在一起, 所以會連續抓到後, 一斷就須結束
  
   public void GetPlantCodeInArrarPlantCode(int tFoxconnBULoc, int teachIDSetCount, string[,] tarrayCustomerFoxconnPNToOneSet, int tarrayPlantCodeLong, string[,] tarrayPlantCode, int tarrayCustomerFoxconnPNToOneSetIndex, int tPlantCodeinarrayCustomerFoxconnPNToOneSetloc)
   {
       int lv1 = 0;
       int lv2 = 0;
       int lv3 = 0;

       string s0 = "";
       string s1 = "";
       string s2 = "";
       string s3 = "";
       string s4 = "";
       string s5 = "";
       string s6 = "";
       string s7 = "";
       string s8 = "";
       int tLocOffSet = tarrayCustomerFoxconnPNToOneSetIndex - tPlantCodeinarrayCustomerFoxconnPNToOneSetloc;


       for (lv1 = 1; lv1 < teachIDSetCount + 1; lv1++)  // first loop start 開始用 Customer+Foxconn+PN+Dom_Exp+FoconnBU
       {
           s1 = tarrayCustomerFoxconnPNToOneSet[lv1, 1].ToString().Trim();  // Dom_exp
           s2 = tarrayCustomerFoxconnPNToOneSet[lv1, 2].ToString().Trim();  // FoxconnSite
           s0 = tarrayCustomerFoxconnPNToOneSet[lv1, tFoxconnBULoc].ToString().Trim();  // FoxconnBU

           for (lv2 = 1; lv2 < tarrayPlantCodeLong+1; lv2++)
           {
               s3 = tarrayPlantCode[lv2, 3].ToString().Trim(); // FoxconnSite
               s4 = tarrayPlantCode[lv2, 4].ToString().Trim(); // Dom_Exp
               s5 = tarrayPlantCode[lv2, 5].ToString().Trim(); // factory   Mold
               s6 = tarrayPlantCode[lv2, 2].ToString().Trim(); // PlantCode
               s7 = tarrayPlantCode[lv2, 7].ToString().Trim(); // Para WeeklyDays
               s8 = tarrayPlantCode[lv2, 8].ToString().Trim(); // FoxconnBU

               if ((s1 == s4) && (s2 == s3) && (s0 == s8)  && (s5 == "Mold"))
               {
                   tarrayCustomerFoxconnPNToOneSet[lv1, tLocOffSet] = s6;  // Put in Array 201 -4
                   tarrayCustomerFoxconnPNToOneSet[lv1, tarrayCustomerFoxconnPNToOneSetIndex - 10] = s7;  // Put in Array 201 - 10
                   lv2 = tarrayPlantCodeLong + 1;
                   lv3++;
               }


           }
       }

   }    // end GetPlantCodeInArrarPlantCode

   
   public static DataSet GetDataByPara(string sql)
   {
          string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
          SqlConnection scnn = new SqlConnection(ConnString);
        
          DataSet ds = new DataSet();
          try
          {
                scnn.Open();
                SqlCommand scmm = new SqlCommand(sql, scnn);

                SqlDataAdapter sdapter = new SqlDataAdapter(scmm);
                sdapter.SelectCommand.CommandTimeout = 300;
                sdapter.Fill(ds);
                return ds;
          }
            // catch (Exception ex)
            // {
            //    throw ex;
            // }
          catch
          {
              return ds;
          }
          finally
          {
              scnn.Close();
          }
  }  // Get_InfoByPara end

      
   public static DataSet PGetDataByPara(string sql, string DataPath )
   {
          string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
          if (  DataPath != "" ) ConnString = DataPath.ToString();
            
          SqlConnection scnn = new SqlConnection(ConnString);
          DataSet ds = new DataSet();
          try
          {
                scnn.Open();
                SqlCommand scmm = new SqlCommand(sql, scnn);

                SqlDataAdapter sdapter = new SqlDataAdapter(scmm);
                sdapter.SelectCommand.CommandTimeout = 300;
                sdapter.Fill(ds);
                return ds;
           }
            // catch (Exception ex)
            // {
            //    throw ex;
            // }
           catch
           {
               return ds;
           }
           finally
           {
               scnn.Close();
           }
    }  // PGetDataByPara end


   // public static bool CDBExcute(string sql)
   public static bool PDBExcute(string sql, string DataPath)
        {
            // string ConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID sa;Password=Sa123456;Timeout=120;";
            //       ConnString = DefaultConnString;
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            if (  DataPath != "" ) ConnString = DataPath.ToString();
         
            SqlConnection scnn = new SqlConnection(ConnString);
           
            try
            {
                scnn.Open();
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }


   /////////////////////////////////////////////////////////////////
   // Loop 3 time for summary GSCMD_X_SP 20110212
   public string Gscmd_splitc(string iRunDate, int iDayCnt, string iDBFPath)
   {
       int v1 = 0;
       string s1 = "", DownDVDay = "";
       if (iRunDate == "") iRunDate = CuurDate;
       // if (iDayCnt  <= 0 )   iDayCnt = 1;
       // if (iDBFPath != "")   RunDBPath = iDBFPath;

       DownDVDay = iRunDate;
       for (v1 = 0; v1 <= iDayCnt; v1++)
       {
           DownDVDay = (Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(DownDVDay))).AddDays(-v1).ToString("yyyyMMdd");
           s1 = Gscmd_splitcloop(DownDVDay, iDayCnt, iDBFPath);
       }

       return (s1);
   }
  ////////////////////////////////////////////////////////////////////
  // Summary Total Gscmd_X_SP to Gscmd_4A5_Detail_PnOnetSet
  // Index Document_ID, FoxconnSite, CustomerSite, CustomerPN, Datafrom
   public string Gscmd_splitcloop(string RunDate, int DayCnt, string DBFPath)
   {
       if (RunDate == "") RunDate = CuurDate;
       if (DayCnt <= 0) DayCnt = 1;
       if (DBFPath != "") RunDBPath = DBFPath;

       string sqlsplit = "", sqlinsertsplit = "";
       string subtmpstr1 = "";
       string subtmpstr2 = "";
       string subtmpstr3 = "";
       string subtmpstr4 = "";
       string s0 = "", s11 = "", s12 = "", s13 = "", s14 = "", s15 = "", s16 = "", s17 = "", s18 = "";
       string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", s9 = "", s10 = "";
       string s19 = "", s20 = "", s21 = "", s22 = "", s23 = "", s24 = "", s25 = "";
       int localvar1 = 0;
       int localvar2 = 0;
       int localvar3 = 0;
       int localvar4 = 0;
       int localVarX = 0;
       int Long4A3_Detail_PNOneSet = 0;
       Double d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0, d7 = 0, d8 = 0;

       string localstr1 = "";
       string localstr2 = "";
       string localstr3 = "";
       string localstr4 = "";
       string localstr5 = "";
       string localstr6 = "";
       string localstr7 = "";
       string localstr8 = "";
       string localstr9 = "";
       string localstr12 = "";
       string localstr13 = "";
       string localstr14 = "";
       string localstr15 = "";
       string localstr16 = "";
       string localstr17 = "";
       string localstr18 = "";
       string localstr19 = "";
       string localstr20 = "";
       string localstr21 = "";
       string localstr22 = "";
       string localstr23 = "";
       string localstr24 = "";
       string localstr25 = "";

       string s111, s222, s333, s666;

       // int Main4A3Long = 0;
       // int Parter4A3Long = 0;
       DateTime locdate1 = DateTime.Today;

       string tmpReleaseYear, tmpReleaseDate, tmpDocumentID, tmpSPWeek, PreDownDVDay, DownDVDay, tmpdatafrom;

       tmpReleaseYear = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
       tmpReleaseDate = tmptoday.ToString("yyyyMMdd");
       tmpDocumentID = tmptoday.ToString("yyyyMMdd");

       subtmpstr2 = tmptoday.ToString("yyyyMMdd").Substring(0, 4);
       localvar1 = Convert.ToInt32(tmptoday.DayOfYear);
       localvar2 = Convert.ToInt32(tmptoday.DayOfWeek);

       subtmpstr1 = ShipPlanlibPointer.getWeekofthisYear(1, localvar1, localvar2, subtmpstr2, tmptoday);
       tmpSPWeek = subtmpstr1;  // 2010-WK51 --> 2010-W51
       localvar1 = tmpSPWeek.Length;
       tmpSPWeek = subtmpstr1.Substring(0, 6) + subtmpstr1.Substring(7, localvar1 - 7);
       // DownDVDay = textBox1.Text.Substring(0, 4) + textBox1.Text.Substring(5, 2) + textBox1.Text.Substring(8, 2); // textBox1.Text = DateTime.Today.ToString("yyyy-MM-dd");
       DownDVDay = RunDate;
       PreDownDVDay = DownDVDay;

       // PreDownDVDay = (Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(DownDVDay))).AddDays(-RunDays).ToString("yyyyMMdd");

       // 20110102 WriteCount = 0;
       // 20110102 if (textBox8.Text != "") WriETAarraytransDayflag = Convert.ToChar(textBox8.Text.Substring(0, 1));

       string ErrMsg = "";
       int CustomerPlantLong = 0;

       string sql5 = "select * from Customer_Plant ";  // table error need update
       DataSet ds5 = LibSCM1Pointer.GetDataByDataPath(sql5, RunDBPath); // DataSet ds5 = DataConnlib.Get_InfoByPara(sql5);
       CustomerPlantLong = ds5.Tables[0].Rows.Count;  // Real how many record in this table
       if (CustomerPlantLong == 0)
       { //    Response.Write("<script>alert('select * from Customer_Plant失敗，請檢查後重試！')</script>");
           ErrMsg = "It can not find any data in  Customer_Plant, Call Operator";
           return ("-1");
       }

       string[,] arrayCustomerPlant = new string[CustomerPlantLong + 1, 15 + 1];
       GetDataInArrarPartCustomerPlant(ref arrayCustomerPlant, ref ds5, ref CustomerPlantLong); // Put DBA data into arrarpart
       int GetDataSqlLong = 0, UploadETDrecordLong = 0, EVLong = 0;
       int var1 = 0, var2 = 0, var3 = 0, var4 = 0, var5 = 0, var6 = 0, var7 = 0, var8 = 0, IndexArrayLoc = 10;
       int arrayCustomerFoxconnPNToOneSetLong = 60000, arrayCustomerFoxconnPNToOneSetIndex = 80;
       string[,] arrayCustomerFoxconnPNToOneSet = new string[arrayCustomerFoxconnPNToOneSetLong, arrayCustomerFoxconnPNToOneSetIndex + 1];
       var3 = arrayCustomerFoxconnPNToOneSetLong;

       // SetUp Ticket in MLN_T_NEW 20110308
       sqlsplit = " SELECT  count(*) tmp1 FROM NLV_T_NEWDV where  ( Ticket = '' or Ticket is Null ) "
       + "  and  substring( Document_ID,1,8) =  '" + DownDVDay + "' "
       + "  and (    datafrom <> 'Man'  and   datafrom <> 'FSC'  )  ";
       DataSet dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // All Data Not only EV
       if (dssplit.Tables.Count <= 0) localvar2 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else localvar2 = Convert.ToInt32(dssplit.Tables[0].Rows[0]["tmp1"].ToString());  // dssplit.Tables[0].Rows.Count;

       sqlsplit = " SELECT  count(*) tmp1 FROM NLV_T_NEWDV_4A5_Site where  ( Ticket = '' or Ticket is Null ) "
       + "  and  substring( Document_ID,1,8) =  '" + DownDVDay + "' "
       + "  and (    datafrom <> 'Man'  and   datafrom <> 'FSC'  )  ";
       dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // All Data Not only EV
       if (dssplit.Tables.Count <= 0) localvar3 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else localvar3 = Convert.ToInt32(dssplit.Tables[0].Rows[0]["tmp1"].ToString());  // dssplit.Tables[0].Rows.Count;
      
       if ( (localvar2 > 0) || (localvar3 > 0) ) // Valid Data 
       {
           sqlsplit = " SELECT  distinct datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item  FROM NLV_T_NEWDV "
           + "  where substring( Document_ID,1,8) =  '" + DownDVDay + "'  "
           + "  and (    datafrom <> 'Man'  and   datafrom <> 'FSC'  )  "
           + "  group by datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item   "
           + "  order by  datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item ";
           dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // 
           if (dssplit.Tables.Count <= 0) localvar1 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
           else
           {
               localvar1 = dssplit.Tables[0].Rows.Count;
               GetDataSqlLong = dssplit.Tables[0].Rows.Count;
               UploadETDrecordLong = GetDataSqlLong;
               EVLong = UploadETDrecordLong;
           }

           // string[,] NLV_T_NEWDVArr = new string[localvar1 + 1, 10 + 1];

           for (var1 = 0; var1 < dssplit.Tables[0].Rows.Count; var1++)
           {

               s1 = dssplit.Tables[0].Rows[var1]["datafrom"].ToString();
               s2 = dssplit.Tables[0].Rows[var1]["Customer_PN"].ToString();
               s3 = dssplit.Tables[0].Rows[var1]["Receiving_Site"].ToString(); // CustomerSite
               s4 = dssplit.Tables[0].Rows[var1]["Supplying_Site"].ToString(); // FoxconnSite
               s5 = dssplit.Tables[0].Rows[var1]["Agreement"].ToString();
               s6 = dssplit.Tables[0].Rows[var1]["Item"].ToString();
               //s7 = Convert.ToDouble(LibSCM1Pointer.CNGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath, 1));
               s7 = LibSCM1Pointer.CGet_Ticket(DownDVDay.Substring(0, 8), "SSP", "1", RunDBPath);

               s8 = "Update NLV_T_NEWDV set Ticket = '" + s7 + "' "
               + " where datafrom = '" + s1 + "' and Customer_PN =  '" + s2 + "'  and Receiving_Site =  '" + s3 + "' "
               + " and   Supplying_Site = '" + s4 + "' and Agreement = '" + s5 + "'"
               + " and  Item = '" + s6 + "' and substring(Document_ID,1,8) =  '" + DownDVDay + "' ";

               if (!LibSCM1Pointer.DBExcuteByDataPath(s8, RunDBPath))
               {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                   // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                   ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
               }

               s8 = "Update NLV_T_NEWDV_4A5_Site set Ticket = '" + s7 + "' "
               + " where datafrom = '" + s1 + "' and Customer_PN =  '" + s2 + "'  and Receiving_Site =  '" + s3 + "' "
               + " and   Supplying_Site = '" + s4 + "' and Agreement = '" + s5 + "'"
               + " and  Item = '" + s6 + "' and substring(Document_ID,1,8) =  '" + DownDVDay + "' ";

               if (!LibSCM1Pointer.DBExcuteByDataPath(s8, RunDBPath))
               {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                   // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                   ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
               }


           }  // end for loop  

       } // End localvar2 = 0 Valid Data
       //////////////////////////////////
       // Start run 4A3_Detail EV

       string sql4A3_Detail_PNOneSet = " Select * from GSCMD_X_SP_PNOneSet where ( Substring(DocumentTime,1,8) <= '" + DownDVDay + "' "
       + " and Substring(DocumentTime,1,8) >= '" + PreDownDVDay + "' ) order by FoxconnSite, CustomerSite, Forecast_CustomerPN, Document_ID ";
       DataSet ds4A3_Detail_PNOneSet = LibSCM1Pointer.GetDataByDataPath(sql4A3_Detail_PNOneSet, RunDBPath);  // All Data Not only EV
       if (ds4A3_Detail_PNOneSet.Tables.Count <= 0) Long4A3_Detail_PNOneSet = 0; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else
           Long4A3_Detail_PNOneSet = ds4A3_Detail_PNOneSet.Tables[0].Rows.Count;

       
       sqlsplit = " Select * from GSCMD_X_SP Where ( Substring( ReleaseDate,1,8) <= '" + DownDVDay + "' ) "
       + " and ( Substring( ReleaseDate,1,8) >= '" + PreDownDVDay + "' ) "
       + " and ( Ticket is NULL or Ticket = '') "  // ReplyNokia_ID = Ticket 
       + " order by ReleaseDate, FoxconnSite, CustomerSite, FoxconnBU, CustomerPN, ID , datafrom, Dom_Exp";
       dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // All Data Not only EV
       if (dssplit.Tables.Count <= 0) localvar1 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else
       {
           localvar1 = dssplit.Tables[0].Rows.Count;
           GetDataSqlLong = dssplit.Tables[0].Rows.Count;
           UploadETDrecordLong = GetDataSqlLong;
           EVLong = UploadETDrecordLong;
       }

       if (localvar1 <= 0) return (""); // Not Data
       ////////////////////////////////////////////
       // Set Arround for 120000 Summary to 5000
       // var3 = ( ( EVLong / 120000 ) + 1 ) * 5000;
       // if ( var3 > arrayCustomerFoxconnPNToOneSetLong ) arrayCustomerFoxconnPNToOneSetLong = var3; // 找到最大會總數

       var3 = arrayCustomerFoxconnPNToOneSetLong;

       for (var4 = 0; var4 < var3; var4++)
       {
           for (var5 = 0; var5 < arrayCustomerFoxconnPNToOneSetIndex + 1; var5++) arrayCustomerFoxconnPNToOneSet[var4, var5] = "";

           arrayCustomerFoxconnPNToOneSet[var4, 5] = (10000 + var4).ToString();     // 為 Key當Index 到 array 13 Upload
           arrayCustomerFoxconnPNToOneSet[var4, 6] = tmptoday.ToString("yyyyMMdd"); // DV 最早一天
           arrayCustomerFoxconnPNToOneSet[var4, 7] = tmptoday.ToString("yyyyMMdd"); // DV 最晚一天
           arrayCustomerFoxconnPNToOneSet[var4, 8] = "0";                           // Summary GIT, Stock, Consigned
           arrayCustomerFoxconnPNToOneSet[var4, IndexArrayLoc] = "11";              // loc 10 記錄下一位, 11放實際 DTDUpload Loc 
           arrayCustomerFoxconnPNToOneSet[var4, 15] = "0";                          // EV Total             

       }   // End InitializeCulture space


       int DelpcateEVNum = 0, eachIDSetCount = 0;
       localvar1 = 0;
       localvar2 = 0;
       localvar3 = 0;
       s1 = ""; s2 = ""; s3 = ""; s6 = "";

       for (var1 = 0; var1 < dssplit.Tables[0].Rows.Count; var1++)
       {
           localstr5 = dssplit.Tables[0].Rows[var1]["ReleaseDate"].ToString();

           if (localstr5 == DownDVDay )  // Delete new function iin DataSet 20100703
           {
               localvar4++;
               // textBox4.Text = var1.ToString();

               localstr1 = dssplit.Tables[0].Rows[var1]["CustomerSite"].ToString();  
               localstr2 = dssplit.Tables[0].Rows[var1]["FoxconnSite"].ToString();
               localstr3 = dssplit.Tables[0].Rows[var1]["CustomerPN"].ToString();
               localstr4 = dssplit.Tables[0].Rows[var1]["ID"].ToString();
               localstr5 = dssplit.Tables[0].Rows[var1]["Dom_Exp"].ToString();
               localstr6 = dssplit.Tables[0].Rows[var1]["datafrom"].ToString();
               localstr7 = StrClrSpecialChar(dssplit.Tables[0].Rows[var1]["Description"].ToString());
               localstr8 = dssplit.Tables[0].Rows[var1]["SPDate"].ToString();
               localstr9 = dssplit.Tables[0].Rows[var1]["FoxconnBU"].ToString();
               localstr12 = dssplit.Tables[0].Rows[var1]["FoxconnPN"].ToString();
               localstr13 = dssplit.Tables[0].Rows[var1]["ReleaseDate"].ToString();
               localstr15 = dssplit.Tables[0].Rows[var1]["SPQty"].ToString();
               localstr15 = ShipPlanlibPointer.TrsStrToInteger(localstr15);
               localstr16 = dssplit.Tables[0].Rows[var1]["Agreement"].ToString();
               localstr17 = dssplit.Tables[0].Rows[var1]["SPWeek"].ToString();
               localstr19 = dssplit.Tables[0].Rows[var1]["CustomerSite"].ToString();  // Convert to CustomerSite
               localstr20 = dssplit.Tables[0].Rows[var1]["FoxconnSite"].ToString();
               localstr21 = dssplit.Tables[0].Rows[var1]["Item"].ToString();
               localstr22 = dssplit.Tables[0].Rows[var1]["PNProject"].ToString();  // Convert to CustomerSite
              

               if ((localstr1 == s1) && (localstr2 == s2) && (localstr3 == s3) && (localstr4 == s4) && (localstr5 == s5) && (localstr6 == s6) && (localstr9 == s9) )
               {
                   localvar1++;
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 11] = localvar1.ToString(); // SP total number                 
               }
               else
               {
                   localvar1 = 1;
                   eachIDSetCount++;
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = localstr1; // CustomerSite
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = localstr2; // FoxconnSite
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = localstr3; // CustomerPN
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = localstr4; // Document_ID
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5] = localstr5; // Dom_Exp
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = localstr6; // datafrom
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = localstr7;  // = dssplit.Tables[0].Rows[var1]["Description"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 8] = localstr8;  // = dssplit.Tables[0].Rows[var1]["SPDate"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 9] = localstr9;  // = dssplit.Tables[0].Rows[var1]["FoxconnBU"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 12]= localstr12; // = dssplit.Tables[0].Rows[var1]["FoxconnPN"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 13]= localstr13; // = dssplit.Tables[0].Rows[var1]["ReleaseDate"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 14]= "Y"; // write flag
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 15]= localstr15; // = dssplit.Tables[0].Rows[var1]["SPQty"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 16]= localstr16; // = dssplit.Tables[0].Rows[var1]["Agreement"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 17]= localstr17; // = dssplit.Tables[0].Rows[var1]["SPWeek"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 19]= localstr19; // = dssplit.Tables[0].Rows[var1]["CustomerSite"].ToString();  // Convert to CustomerSite
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 20]= localstr20; // = dssplit.Tables[0].Rows[var1]["FoxconnSite"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 21]= localstr21; // = dssplit.Tables[0].Rows[var1]["Item"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 22]= localstr22; // = dssplit.Tables[0].Rows[var1]["PNProject"].ToString();  // Convert to CustomerSite
              
                   localvar3 = 0; // clear for new PN QTY
               }

               localvar3 = Convert.ToInt32(arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 15]);
               localvar3 = localvar3 + Convert.ToInt32(localstr15);
               arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 15] = localvar3.ToString();

               s1 = dssplit.Tables[0].Rows[var1]["CustomerSite"].ToString();
               s2 = dssplit.Tables[0].Rows[var1]["FoxconnSite"].ToString();
               s3 = dssplit.Tables[0].Rows[var1]["CustomerPN"].ToString();
               s4 = dssplit.Tables[0].Rows[var1]["ID"].ToString();
               s5 = dssplit.Tables[0].Rows[var1]["Dom_Exp"].ToString();
               s6 = dssplit.Tables[0].Rows[var1]["datafrom"].ToString();
               s9 = dssplit.Tables[0].Rows[var1]["FoxconnBU"].ToString();  
               

           } // end if (ds3.Tables[0].Rows[var1]["Forecast_QtyTypeCode"].ToString() != "")  

       }  // end for loop 
       // end 1

     
   //    for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
   //    {
   //        s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSiteNew
   //        s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
   //
   //        for (var2 = 1; var2 < CustomerPlantLong + 1; var2++)
   //        {
   //            s111 = arrayCustomerPlant[var2, 1]; // = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
   //            s222 = arrayCustomerPlant[var2, 2]; // = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
   //            s333 = arrayCustomerPlant[var2, 3]; // = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
   //            if ((s1 == s222))
   //            {
   //                arrayCustomerFoxconnPNToOneSet[var1, 1] = s333;// Write CustomerSite Plant_Name
   //                var2 = CustomerPlantLong + 1; // break
   //            }
   //        }
   //
   //        for (var2 = 1; var2 < CustomerPlantLong + 1; var2++)
   //        {
   //            s111 = arrayCustomerPlant[var2, 1]; // = ds5.Tables[0].Rows[localvar]["Customer"].ToString();
   //            s222 = arrayCustomerPlant[var2, 2]; // = ds5.Tables[0].Rows[localvar]["Plant_Code"].ToString();
   //            s333 = arrayCustomerPlant[var2, 3]; // = ds5.Tables[0].Rows[localvar]["Plant_Name"].ToString();
   //            if ((s2 == s222))
   //            {
   //                arrayCustomerFoxconnPNToOneSet[var1, 2] = s333;// Write FoxconnRegion Plant_Name
   //                var2 = CustomerPlantLong + 1; // break
   //            }
   //        }
   //
   //    }  // end get Plant Name


       //////////////////// Get Ticket 20101225
       // for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
       // {
       //    s10 = LibSCM1Pointer.CGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath);
       //    arrayCustomerFoxconnPNToOneSet[var1, 10] = s10.ToString(); // SP ticket            
       // }  // end get CGet_Ticket

       // CNGet_Ticket
       if (eachIDSetCount > 0)
           d1 = Convert.ToDouble(LibSCM1Pointer.CNGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath, eachIDSetCount));

       d1 = d1 - Convert.ToDouble(eachIDSetCount);
       for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
       {
           s10 = (d1++).ToString();
           arrayCustomerFoxconnPNToOneSet[var1, 10] = s10.ToString(); // SP ticket            
       }  // end get ticket

       d1 = 0;  // claer

       for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
       {
           // s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
           // s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
           // s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
           // s6 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID

           for (var2 = 0; var2 < Long4A3_Detail_PNOneSet; var2++)
           {
               // s111 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["CustomerSite"].ToString();  // Convert to CustomerSite
               // s222 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["FoxconnRegion"].ToString();
               // s333 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Forecast_CustomerPN"].ToString();
               // s666 = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Document_ID"].ToString();
               // if ((s111 == s1) && (s222 == s2) && (s333 == s3) && (s666 == s6))
               if ((ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["CustomerSite"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 1]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["FoxconnSite"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 2]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Forecast_CustomerPN"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 3]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Document_ID"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 4]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Dom_Exp"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 5]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["datafrom"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 6]) && (ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["FoxconnBU"].ToString() == arrayCustomerFoxconnPNToOneSet[var1, 9]))
               {
                   arrayCustomerFoxconnPNToOneSet[var1, 10] = ds4A3_Detail_PNOneSet.Tables[0].Rows[var2]["Ticket"].ToString(); // With Old ticket
                   arrayCustomerFoxconnPNToOneSet[var1, 14] = "N"; // Write flag
                   var2 = Long4A3_Detail_PNOneSet + 1; // break
               }

           }
       }

       var2 = 0;
       // Insert Syncro_4A3_Detail_PNOneSet , Update 4A3_Detail with ReplyNokia_ID 
       for (var1 = 1; var1 < eachIDSetCount + 1; var1++)
       {
           s1 = arrayCustomerFoxconnPNToOneSet[var1, 1];    // CustomerSite
           s2 = arrayCustomerFoxconnPNToOneSet[var1, 2];    // FoxconnRegion
           s3 = arrayCustomerFoxconnPNToOneSet[var1, 3];    // Forecast_CustomerPN
           s4 = arrayCustomerFoxconnPNToOneSet[var1, 4];    // Document_ID
           s5 = arrayCustomerFoxconnPNToOneSet[var1, 5];    // Dom_Exp
           s6 = arrayCustomerFoxconnPNToOneSet[var1, 6];    // datafrom
           s7 = arrayCustomerFoxconnPNToOneSet[var1, 7];    // Description
           s8 = arrayCustomerFoxconnPNToOneSet[var1, 8];    // SPDate
           s9 = arrayCustomerFoxconnPNToOneSet[var1, 9];    // FoxconnBU
           s10 = arrayCustomerFoxconnPNToOneSet[var1, 10];  // SP ticket
           s11 = arrayCustomerFoxconnPNToOneSet[var1, 11];  // SP total number
           s12 = arrayCustomerFoxconnPNToOneSet[var1, 12];  // FoxconnPN
           s13 = arrayCustomerFoxconnPNToOneSet[var1, 13];  // ReleaseDate DocumentTime
           s15 = arrayCustomerFoxconnPNToOneSet[var1, 15];  // ToT SPQty Forecast_Qty
           s16 = arrayCustomerFoxconnPNToOneSet[var1, 16];  // Agreement
           s17 = arrayCustomerFoxconnPNToOneSet[var1, 17];  // = dssplit.Tables[0].Rows[var1]["SPWeek"].ToString();
           s19 = arrayCustomerFoxconnPNToOneSet[var1, 19];  // CustomerSiteCode
           s20 = arrayCustomerFoxconnPNToOneSet[var1, 20];  // FoxconnSiteCode
           s21 = arrayCustomerFoxconnPNToOneSet[var1, 21];  // Item
           s22 = arrayCustomerFoxconnPNToOneSet[var1, 22];  // PNProject

           if (arrayCustomerFoxconnPNToOneSet[var1, 14].ToString() == "Y")  // write only new
           {
               subtmpstr3 = "Insert into GSCMD_X_SP_PNOneSet ( CustomerSite, FoxconnSite, Forecast_CustomerPN, Document_ID,"
           + " FoxconnBU, Ticket, SPAccount, HHPARTS, DocumentTime, ToTForecast_Qty, Forecast_Qty, Week, Agreement, "
           + " CustomerSiteCode, FoxconnRegionCode, Forecast_QtyTypeCode, FoxconnRegion, Dom_Exp, Datafrom, Conversation_ID, "
           + " Description, Item, Project, Customer ) "
           + " Values ( '" + s1 + "', '" + s2 + "', '" + s3 + "', '" + s4 + "', '" + s9 + "', '" + s10 + "', '" + s11 + "' , "
           + " '" + s12 + "', '" + s13 + "', '" + s15 + "', '" + s15 + "', '" + s17 + "', '" + s16 + "', '" + s19 + "', '" + s20 + "', "
           + " 'EV', '" + s2 + "', '" + s5 + "', '" + s6 + "', '" + s4 + "', "
           + " '" + s7 + "',  '" + s21 + "', '" + s22 + "', 'Nokia' ) ";

               if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr3, RunDBPath))
               {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                   // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                   ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
               }
           } // write flag
           else
           if (arrayCustomerFoxconnPNToOneSet[var1, 14].ToString() == "N")  // 重讀並 Update
           {
                   // s0 = Get4A3_DetailQTYinSecondTime(s3, s6);
                   // s15 = s0;
                   subtmpstr3 = "Update GSCMD_X_SP_PNOneSet set ToTForecast_Qty = '" + s15 + "', Forecast_Qty = '" + s15 + "' "
                   + " where Forecast_CustomerPN = '" + s3 + "' and Document_ID =  '" + s4 + "'  and FoxconnBU =  '" + s9 + "' "
                   + " and   Dom_Exp = '" + s5 + "' and datafrom = '" + s6 + "'";

                   if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr3, RunDBPath))
                   {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                       // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                       ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
                   }
                   else  // 成功
                   {
                       arrayCustomerFoxconnPNToOneSet[var1, 15] = s15;  // ToTForecast_Qty
                   }
               }    // 只做一半, 重算 4A3_Detail
               else
               {
                   var2++;
           }

           ///////// update GSCMD_X_SP
           subtmpstr4 = " UPDATE GSCMD_X_SP SET Ticket = '" + s10 + "' WHERE ( ticket is NULL "
           + " or ticket = '') and  ID = '" + s4 + "' and CustomerPN = '" + s3 + "' "
           + " and   Dom_Exp = '" + s5 + "' and datafrom = '" + s6 + "' and FoxconnBU = '" + s9 + "' ";
           if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
           {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
               // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
               // Response.Write("<script>alert('Update 4A3_Detail 新增失敗，請稍后重試！ '" + (s12+s3) + "' ')</script>");
               ErrMsg = "Update 4A3_Detail 新增失敗，請稍后重試！";
           }


           Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm"); 
          ///////// update  Syncro_Foxconn_Nokia_PartNo
          // 20110406
          // subtmpstr4 = " UPDATE Syncro_Foxconn_Nokia_PartNo SET EOL = '', UpdateTime = '" + Currtime + "' "
          //+ "   WHERE NokiaSite = '" + s1 + "' and  NokiaPartNo = '" + s3 + "' "
          //+ " and FoxconnSite = '" + s2 + "' ";  
          // if (!LibSCM1Pointer.DBExcuteByDataPath(subtmpstr4, RunDBPath))
          // {
          //     ErrMsg = "Update Syncro_Foxconn_Nokia_PartNo失敗，請稍后重試！";
          // }
          // 20110406

       }  // end insert


       return ("");   // End of Generate Gscmd_X_SP_PNOneSet

       // SetUp Ticket in MLN_T_NEW

       sqlsplit = " Select * from GSCMD_X_SP Where ( Substring( ReleaseDate,1,8) <= '" + DownDVDay + "' ) "
      + " and ( Substring( ReleaseDate,1,8) >= '" + PreDownDVDay + "' ) "
      + " and ( Ticket is NULL or Ticket = '') "  // ReplyNokia_ID = Ticket 
      + " order by ReleaseDate, FoxconnSite, CustomerSite, FoxconnBU, CustomerPN, ID , datafrom, Dom_Exp";
       dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // All Data Not only EV
       if (dssplit.Tables.Count <= 0) localvar1 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else
       {
           localvar1 = dssplit.Tables[0].Rows.Count;
           GetDataSqlLong = dssplit.Tables[0].Rows.Count;
           UploadETDrecordLong = GetDataSqlLong;
           EVLong = UploadETDrecordLong;
       }

       sqlsplit = " SELECT  Document_ID FROM NLV_T_NEWDV where Ticket = '' or Ticket is Null "
          + "  where substring( a.Document_ID,1,8) =  '" + DownDVDay + "'+'%'  "
          + "  and (    datafrom <> 'Man'  and   datafrom <> 'FSC'  )  ";
       dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // All Data Not only EV
       if (dssplit.Tables.Count <= 0) localvar1 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else
       {
           localvar1 = dssplit.Tables[0].Rows.Count;
           GetDataSqlLong = dssplit.Tables[0].Rows.Count;
           UploadETDrecordLong = GetDataSqlLong;
           EVLong = UploadETDrecordLong;
       }

       if (localvar1 <= 0) return ("");

       sqlsplit = " SELECT  distinct datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item  FROM NLV_T_NEWDV "
          + "  where substring( a.Document_ID,1,8) =  '" + DownDVDay + "'+'%'  "
          + "  and (    datafrom <> 'Man'  and   datafrom <> 'FSC'  )  "
          + "  group by datafrom, Customer_PN, Receiving_Site, Supplying_Site, UPLOADTIME, agreement, item   "
          + "  order by  datafrom, Customer_PN, Receiving_Site, Supplying_Site, agreement, item ";
       dssplit = LibSCM1Pointer.GetDataByDataPath(sqlsplit, RunDBPath);  // 
       if (dssplit.Tables.Count <= 0) localvar1 = -1; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else
       {
           localvar1 = dssplit.Tables[0].Rows.Count;
           GetDataSqlLong = dssplit.Tables[0].Rows.Count;
           UploadETDrecordLong = GetDataSqlLong;
           EVLong = UploadETDrecordLong;
       }




       // string[,] NLV_T_NEWDVArr = new string[localvar1 + 1, 10 + 1];

       for (var1 = 0; var1 < dssplit.Tables[0].Rows.Count; var1++)
       {

           s1 = dssplit.Tables[0].Rows[var1]["datafrom"].ToString();
           s2 = dssplit.Tables[0].Rows[var1]["Customer_PN"].ToString();
           s3 = dssplit.Tables[0].Rows[var1]["Receiving_Site"].ToString(); // CustomerSite
           s4 = dssplit.Tables[0].Rows[var1]["Supplying_Site"].ToString(); // FoxconnSite
           s5 = dssplit.Tables[0].Rows[var1]["Agreement"].ToString();
           s6 = dssplit.Tables[0].Rows[var1]["Item"].ToString();
           //s7 = Convert.ToDouble(LibSCM1Pointer.CNGet_Ticket(tmpDocumentID.Substring(0, 8), "SSP", "1", RunDBPath, 1));
           s7 = LibSCM1Pointer.CGet_Ticket(DownDVDay.Substring(0, 8), "SSP", "1", RunDBPath);

           s8 = "Update NLV_T_NEWDV set Ticket = '" + s7 + "' "
           + " where datafrom = '" + s1 + "' and Customer_PN =  '" + s2 + "'  and Receiving_Site =  '" + s3 + "' "
           + " and   Supplying_Site = '" + s4 + "' and Agreement = '" + s5 + "'"
           + " and  Item = '" + s6 + "' and substring(Document_ID,1,8) =  '" + DownDVDay + "' ";

           if (!LibSCM1Pointer.DBExcuteByDataPath(s8, RunDBPath))
           {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
               // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
               ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";
           }


       }  // end for loop  

       return ("");   // End of Generate Gscmd_X_SP_PNOneSet
   }

   public void PullSumArray(ref DataSet dsMain, ref int eachIDSetCount, ref int UploadETDrecordLong, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int arrayCustomerFoxconnPNToOneSetIndex, ref string Programflag)
   {
       int v1 = 0, v2 = 0;
       string s1="";

       for (v1 = 0; v1 < eachIDSetCount; v1++)
       {
           arrayCustomerFoxconnPNToOneSet[v1+1, 0]  = dsMain.Tables[0].Rows[v1]["Project"].ToString(); 
           arrayCustomerFoxconnPNToOneSet[v1+1, 1]  = dsMain.Tables[0].Rows[v1]["CustomerSite"].ToString();
           if (Programflag == "5") arrayCustomerFoxconnPNToOneSet[v1 + 1, 2] = dsMain.Tables[0].Rows[v1]["FoxconnRegion"].ToString();
           else arrayCustomerFoxconnPNToOneSet[v1 + 1, 2] = dsMain.Tables[0].Rows[v1]["FoxSite"].ToString();
               // s1 = arrayCustomerFoxconnPNToOneSet[v1 + 1, 2].Substring(10, arrayCustomerFoxconnPNToOneSet[v1 + 1, 2].Length - 10);
               // arrayCustomerFoxconnPNToOneSet[v1 + 1, 80] = s1.Substring(0, s1.IndexOf(":"));  // Reserve in 80 Get Site 544790470:LH:NLVLH
              
           arrayCustomerFoxconnPNToOneSet[v1+1, 3]  = dsMain.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString();
           arrayCustomerFoxconnPNToOneSet[v1+1, 4]  = dsMain.Tables[0].Rows[v1]["Document_ID"].ToString();
           arrayCustomerFoxconnPNToOneSet[v1+1, 5]  = dsMain.Tables[0].Rows[v1]["Documenttime"].ToString().Substring(0,8);
           arrayCustomerFoxconnPNToOneSet[v1+1, 6]  = dsMain.Tables[0].Rows[v1]["Documenttime"].ToString().Substring(0, 8); // Min
           arrayCustomerFoxconnPNToOneSet[v1+1, 7]  = dsMain.Tables[0].Rows[v1]["Documenttime"].ToString().Substring(0,8); // Max
           arrayCustomerFoxconnPNToOneSet[v1+1, 8]  = dsMain.Tables[0].Rows[v1]["HHPARTS"].ToString();
           arrayCustomerFoxconnPNToOneSet[v1+1, 9]  = dsMain.Tables[0].Rows[v1]["Dom_Exp"].ToString();
           arrayCustomerFoxconnPNToOneSet[v1+1, 29] = StrClrSpecialChar(dsMain.Tables[0].Rows[v1]["Description"].ToString());
           arrayCustomerFoxconnPNToOneSet[v1+1, 56] = dsMain.Tables[0].Rows[v1]["datafrom"].ToString();
           arrayCustomerFoxconnPNToOneSet[v1+1, 60] = dsMain.Tables[0].Rows[v1]["Agreement"].ToString();
           arrayCustomerFoxconnPNToOneSet[v1+1, 61] = dsMain.Tables[0].Rows[v1]["Item"].ToString();

           //if ( ( arrayCustomerFoxconnPNToOneSet[v1 + 1, 2] == "Beijing" ) && ( arrayCustomerFoxconnPNToOneSet[v1+1, 3] == "9904470" ) )
           //if ( arrayCustomerFoxconnPNToOneSet[v1 + 1, 3] == "9904470" ) 
           //    s1 = dsMain.Tables[0].Rows[v1]["Agreement"].ToString();
           

       }
   }

   ////////////////////////////////////////////////////////////////////
   // Summary VLV_TV_NEW                                                        
   public string GetNLV_T_NEWDVEtdUpload(int Sumtype, DataSet tds3, int UploadETDrecordLong, ref string[,] arrayEtdUpload, int DelpcateEVNum, ref int eachIDSetCount, ref string[,] arrayCustomerFoxconnPNToOneSet, ref int DuplicateDBFLoc, int IndexArrayLoc, int arrayCustomerFoxconnPNToOneSetIndex, int arrayCustomerFoxconnPNToOneSetLong, int arraytransSecondAvailLoc)
   {
       int v1 = 0, v2 = 0, v3 = 0, v4=0, v5=0, currloc = 0;
       string localstr1, localstr2, localstr3, localstr4, localstr5, subtmpstr3, localstr6, localstr7;
       int localvar3, localvar4;
       int arrv1, arraylong, tqty;
       string t1, t2, t3, delflag, forcastdate = "";
       string s1 = "";

       localvar4 = 0;
       arraylong = UploadETDrecordLong;
       localstr6 = "";
       DelpcateEVNum = 0;

       int xloop = 40;
       string[,] tParam = new string[xloop + 1, 3];

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

       tParam[15, 1] = "On-Hand Inv.";
       tParam[15, 2] = "12";
       tParam[16, 1] = "GIT";
       tParam[16, 2] = "61";
       tParam[17, 1] = "Mix.DOS";
       tParam[17, 2] = "71";
       tParam[18, 1] = "Max.DOS";
       tParam[18, 2] = "72";
       tParam[19, 1] = "Refrence_No.";
       tParam[19, 2] = "60";
       tParam[20, 1] = "Item";
       tParam[20, 2] = "61";

       
       //////////////////////////////////////////////////////////////////////////
       // Make a new table 1-- 291 Data 291+1=292
       // 291+1 -- 1000 Data (1) Forecast_QtyTypeCode (2) Forecast_BeginDate 
       //                    (3) Forecast_IntervalCode (4) Forecast_Qty

       arrv1 = 1;
       int MaxLoc = arrayCustomerFoxconnPNToOneSetLong / 10;
       string tmpDocument_ID = "", tmpCustomer_PN = "", tmpFoxconnSite="";
       string tmpCustomerSite = "", tmpFoxconnRegion = "", tmpUPLOADTIME="";
       string ttype = "";
       int currseq = 0, currfield = arrayCustomerFoxconnPNToOneSetIndex + 1;
       v5 = tds3.Tables[0].Rows.Count;

       if ( v5 <= 0 ) return("");
       
       if (Sumtype == 51)
       {
           for (v1 = 0; v1 < v5; v1++)
           {
               ttype = tds3.Tables[0].Rows[v1]["DataType"].ToString();
               if ((tmpDocument_ID != tds3.Tables[0].Rows[v1]["Document_ID"].ToString()) || (tmpCustomer_PN != tds3.Tables[0].Rows[v1]["Customer_PN"].ToString()) || (tmpCustomerSite != tds3.Tables[0].Rows[v1]["Receiving_Site"].ToString()) || (tmpFoxconnSite != tds3.Tables[0].Rows[v1]["Supplying_Site"].ToString()))
               {
                   eachIDSetCount++; // 
                   currfield = arrayCustomerFoxconnPNToOneSetIndex + 1;
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 0] = tds3.Tables[0].Rows[v1]["Project"].ToString(); 
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = tds3.Tables[0].Rows[v1]["Receiving_Site"].ToString();  // CustomerSite
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = tds3.Tables[0].Rows[v1]["Supplying_Site"].ToString();  // FoxconnSite
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = tds3.Tables[0].Rows[v1]["Customer_PN"].ToString();     // 
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();     // Document_ID
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString().Substring(0, 8); //
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString().Substring(0, 8); //""; // Min
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString().Substring(0, 8); //""; // Max
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 8] = tds3.Tables[0].Rows[v1]["HHPARTS"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 9] = tds3.Tables[0].Rows[v1]["Dom_Exp"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 29] = StrClrSpecialChar(tds3.Tables[0].Rows[v1]["Description"].ToString());
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 56] = tds3.Tables[0].Rows[v1]["datafrom"].ToString();        // datafrom
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 60] = tds3.Tables[0].Rows[v1]["Agreement"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 61] = tds3.Tables[0].Rows[v1]["Item"].ToString();

                   s1 = tds3.Tables[0].Rows[v1]["datafrom"].ToString();        // datafrom               
                     

                   tmpDocument_ID   = tds3.Tables[0].Rows[v1]["Document_ID"].ToString();
                   tmpCustomer_PN   = tds3.Tables[0].Rows[v1]["Customer_PN"].ToString();
                   tmpCustomerSite  = tds3.Tables[0].Rows[v1]["Receiving_Site"].ToString();
                   tmpFoxconnSite = tds3.Tables[0].Rows[v1]["Supplying_Site"].ToString();
                   
               }

               if (ttype == "Discrete Gross Demand") 
               {
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = tds3.Tables[0].Rows[v1]["datatype"].ToString();// tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = tds3.Tables[0].Rows[v1]["Forecast_Date"].ToString().Substring(0, 8);
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = tds3.Tables[0].Rows[v1]["Forecast_IntervalCode"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString());
                   forcastdate = tds3.Tables[0].Rows[v1]["Forecast_Date"].ToString();
                   forcastdate = forcastdate.Substring(0, 8);

                   if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), forcastdate) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = forcastdate;
                   s1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
                   if (String.Compare((arrayCustomerFoxconnPNToOneSet[v2, 7]), forcastdate) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = forcastdate;

                   if (currfield >= MaxLoc)
                   {
                       currfield = currfield - 4;
                       // return ("-1");
                   }
               }
               else
               {
                   for (v4 = 0; v4 < xloop; v4++)
                       if (ttype == tParam[v4, 1])
                       {
                           // v6 = Convert.ToInt32(tParam[v4, 2]); // trace
                           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, Convert.ToInt32(tParam[v4, 2])] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                           v4 = xloop + 1; // break
                       }

               } // end   if (ttype == "Discrete Gross Demand")
           }  // eachIDSetCount loop
        
       }  // end of if Sumtype == '51'

       string tmpTicket = "", tmpdatafrom="";

       if (Sumtype == 52)
       {
           for (v1 = 0; v1 < v5; v1++)
           {
               ttype = tds3.Tables[0].Rows[v1]["DataType"].ToString();
               if ((tmpTicket != tds3.Tables[0].Rows[v1]["Ticket"].ToString()) || (tmpCustomer_PN != tds3.Tables[0].Rows[v1]["Customer_PN"].ToString()) || (tmpCustomerSite != tds3.Tables[0].Rows[v1]["Receiving_Site"].ToString()) || (tmpFoxconnSite != tds3.Tables[0].Rows[v1]["Supplying_Site"].ToString()) || (tmpdatafrom != tds3.Tables[0].Rows[v1]["datafrom"].ToString()))
               {
                   eachIDSetCount++; // 
                   currfield = arrayCustomerFoxconnPNToOneSetIndex + 1;
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 0] = tds3.Tables[0].Rows[v1]["Project"].ToString(); 
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 1] = tds3.Tables[0].Rows[v1]["Receiving_Site"].ToString();  // CustomerSite
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 2] = tds3.Tables[0].Rows[v1]["Supplying_Site"].ToString();  // FoxconnSite
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 3] = tds3.Tables[0].Rows[v1]["Customer_PN"].ToString();     // 
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 4] = tds3.Tables[0].Rows[v1]["Ticket"].ToString();          // Document_ID
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 5] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString().Substring(0, 8); // DocumentTime
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString().Substring(0, 8); //""; // Min
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = tds3.Tables[0].Rows[v1]["Document_ID"].ToString().Substring(0, 8); //""; // Max
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 8] = tds3.Tables[0].Rows[v1]["HHPARTS"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 9] = tds3.Tables[0].Rows[v1]["Dom_Exp"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 29] = StrClrSpecialChar(tds3.Tables[0].Rows[v1]["Description"].ToString());
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 30] = tds3.Tables[0].Rows[v1]["agreement"].ToString();       // datafrom
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 56] = tds3.Tables[0].Rows[v1]["datafrom"].ToString();       // datafrom
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 60] = tds3.Tables[0].Rows[v1]["Agreement"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 61] = tds3.Tables[0].Rows[v1]["Item"].ToString();
                  


                   tmpTicket        = tds3.Tables[0].Rows[v1]["Ticket"].ToString();
                   tmpCustomer_PN   = tds3.Tables[0].Rows[v1]["Customer_PN"].ToString();
                   tmpCustomerSite  = tds3.Tables[0].Rows[v1]["Receiving_Site"].ToString();
                   tmpFoxconnRegion = tds3.Tables[0].Rows[v1]["Supplying_Site"].ToString();
                   tmpdatafrom      = tds3.Tables[0].Rows[v1]["datafrom"].ToString();

               }


               if (ttype == "Discrete Gross Demand")
               {
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = tds3.Tables[0].Rows[v1]["datatype"].ToString();// tds3.Tables[0].Rows[v1]["Forecast_QtyTypeCode"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = tds3.Tables[0].Rows[v1]["Forecast_Date"].ToString().Substring(0, 8);
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = tds3.Tables[0].Rows[v1]["Forecast_IntervalCode"].ToString();
                   arrayCustomerFoxconnPNToOneSet[eachIDSetCount, currfield++] = TrsStrToInteger(tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString());
                   forcastdate = tds3.Tables[0].Rows[v1]["Forecast_Date"].ToString();
                   forcastdate = forcastdate.Substring(0, 8);

                   if (String.Compare((arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6]), forcastdate) > 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 6] = forcastdate;
                   s1 = arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7].ToString();
                   if (String.Compare((arrayCustomerFoxconnPNToOneSet[v2, 7]), forcastdate) < 0)   // str1>str2, n=1 20100228 for ReleaseDate 
                       arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 7] = forcastdate;
                   
                   if (currfield >= MaxLoc)
                   {
                       currfield = currfield - 4;
                       // return ("-1");
                   }
                 
               }
               else
               {
                   for (v4 = 0; v4 < xloop; v4++)
                       if (ttype == tParam[v4, 1])
                       {
                           // v6 = Convert.ToInt32(tParam[v4, 2]); // trace
                           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, Convert.ToInt32(tParam[v4, 2])] = tds3.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                           v4 = xloop + 1; // break
                       }

               }

           }  // eachIDSetCount loop

       }  // end of if Sumtype == '52'

       return ("1");
        
           
       
   }  // End GetEV4ToarrayEtdUpload    
   
}  // end public class FMers1lib
}  // end namespace Economy.GCMDKen


