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
using EconomyUser;

namespace Economy.Publibrary
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class ShipPlanlib
{
    public static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
	public void ShipPlanlib1()
	{
		//
		// TODO: Add constructor logic here
		//
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
        if ( tmpPassFoxconnSite == "" )  return("");
        
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
                   arrayEtdUpload[arrv1, 19] = tds3.Tables[0].Rows[v1]["Description"].ToString();
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
           arrayCustomerFoxconnPNToOneSet[eachIDSetCount, 8] = ttds3.Tables[0].Rows[tDatSetLoc]["Description"].ToString();
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
   public string GetSiteGITin4A5( string[,] tPNToOneSet, int teachIDSetCount, int tGIT4A5Long, DataSet tdsGIT4A5, int OnSetIndexLong ) // 不同 DV write into array
   {
       int v1 = 0;
       int v2 = 0;
       int v3 = 0;
       int v4 = 0;
       int v5 = 0;
       int v6 = 0;
       string tFoxconnSite = "";
       int LoopSW = 0;


       for (v2 = 1; v2 < teachIDSetCount + 1; v2++)  // Loop for Set  
       {
           LoopSW = 0;
           for (v1 = 0; v1 < tGIT4A5Long; v1++) // Loop for DataSet      
           {   // CustomerSite + Forecast_CustomerPN 
               if ((tPNToOneSet[v2, 1] == tdsGIT4A5.Tables[0].Rows[v1]["CustomerSite"].ToString()) && (tPNToOneSet[v2, 3] == tdsGIT4A5.Tables[0].Rows[v1]["Forecast_CustomerPN"].ToString()))
               {
                   if (tPNToOneSet[v2, OnSetIndexLong - 10 - 11] == tdsGIT4A5.Tables[0].Rows[v1]["Document_ID"].ToString())
                   {
                       v6++;
                       tFoxconnSite = tdsGIT4A5.Tables[0].Rows[v1]["FoxconnSite"].ToString();
                       switch (tFoxconnSite)                                               //
                       {                                                            // 
                           case "BJ": v3 = 7; break;  // F64
                           case "LF": v3 = 8; break;  // F65
                           case "LH": v3 = 9; break;  // F66
                           case "Chennai": v3 = 10; break;  // F67
                           default: v4 = v3; break;  // F68
                       }
                   }   // GIT be Max Document_ID because could be two GIT in one day 
                   tPNToOneSet[v2, OnSetIndexLong - 10 - v3] = tdsGIT4A5.Tables[0].Rows[v1]["Forecast_Qty"].ToString();
                   LoopSW++;
                   v5++;                
               }
               else  
               {
                   if ( LoopSW > 0)  // 連續抓到後, 一斷就須結束
                   {
                       LoopSW = 0;
                       v1 = tGIT4A5Long + 1;
                   }
               }   // end CustomerSite+PN
           }       // end tGIT4A5Long
       }           // end teachIDSetCount

       // for (v2 = 1; v2 < teachIDSetCount + 1; v2++)  // Loop for Set    Trcae
       // {
       //    if ((tPNToOneSet[v2, OnSetIndexLong - 10 - 7].ToString() != "") || (tPNToOneSet[v2, OnSetIndexLong - 10 - 8].ToString() != "") || (tPNToOneSet[v2, OnSetIndexLong - 10 - 9].ToString() != ""))
       //    {
       //        if ( v2 == 1345 )
       //            tFoxconnSite = tPNToOneSet[v2, OnSetIndexLong - 10 - 7].ToString();
       //        tFoxconnSite = tPNToOneSet[v2, OnSetIndexLong - 10 - 8].ToString();
       //        tFoxconnSite = tPNToOneSet[v2, OnSetIndexLong - 10 - 9].ToString();
       //        tFoxconnSite = tPNToOneSet[v2, OnSetIndexLong - 10 - 10].ToString();
       //    }
       // } 
       tFoxconnSite = v6.ToString();
       return(tFoxconnSite);
   }


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

   public void GetPNGroupInArrarPNGroup(int teachIDSetCount, string[,] tarrayCustomerFoxconnPNToOneSet, int tarrayPNGroupLong, string[,] tarrayPNGroup, int tarrayCustomerFoxconnPNToOneSetIndex, int tPNGrouparrayCustomerFoxconnPNToOneSetloc)
   {
       int lv1 = 0;
       int lv2 = 0;
       int lv3 = 0;
       string s1 = "";
       string s3 = "";
       string s4 = "";
       string s5 = "";
       string s6 = "";
       int tLocOffSet = tPNGrouparrayCustomerFoxconnPNToOneSetloc;

            
       for (lv1 = 1; lv1 < teachIDSetCount + 1; lv1++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA
       {
           s1 = tarrayCustomerFoxconnPNToOneSet[lv1, 3].ToString().Trim();  // CustomerPN          
           for (lv2 = 1; lv2 < tarrayPNGroupLong + 1; lv2++)
           {
                s6 = tarrayPNGroup[lv2, 1].ToString().Trim(); // Forecast_PNGroupSN    
                s3 = tarrayPNGroup[lv2, 2].ToString().Trim(); // CustomerPN   Forecast_CustomerPN           
                if (s1 == s6)  // Error by pre program 
                {
                    tarrayCustomerFoxconnPNToOneSet[lv1, tLocOffSet] = s3;  // Put in Array 201 -4
                    lv2 = tarrayPNGroupLong + 1;
                    lv3++;
                }
           }       // end for (lv2 = 1; lv2 < tarrayPNGroupLong + 1; lv2++)              
       }

   }  // end


   public string GetSapInvQtyInArrarOnSet(int teachIDSetCount, string[,] tarrayCustomerFoxconnPNToOneSet, int tarraySapLong, DataSet tds, int tarrayCustomerFoxconnPNToOneSetIndex, int tSapinarrayCustomerFoxconnPNToOneSetloc, int tPlantCodeinarrayCustomerFoxconnPNToOneSetloc, int tSapInvFindFlagLoc)
   {
       int lv1 = 0;
       int lv2 = 0;
       int lv3 = 0;
       int lv4 = 0;
       string s1 = "";
       string s3 = "";
       string tPlantCode = "";
       string tCustomerPN = "";
       string s6 = "";
       string s7 = "";
       decimal v5 = 0;

       int tPLocOffSet = tarrayCustomerFoxconnPNToOneSetIndex - tPlantCodeinarrayCustomerFoxconnPNToOneSetloc;
       int tSLocOffSet = tarrayCustomerFoxconnPNToOneSetIndex - tSapinarrayCustomerFoxconnPNToOneSetloc;
       // arrayNokiaEVDocm[localvar + 1, 1] = ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
       // arrayNokiaEVDocm[localvar + 1, 2] = ds6.Tables[0].Rows[localvar]["plant_name"].ToString();  // CustomerSite

       for (lv1 = 1; lv1 < teachIDSetCount + 1; lv1++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA
       {
           tPlantCode = tarrayCustomerFoxconnPNToOneSet[lv1, tPLocOffSet].ToString().Trim();  // tPlantCoded    
           tCustomerPN = tarrayCustomerFoxconnPNToOneSet[lv1, 3].ToString().Trim();  // CustomerPN          
           for (lv2 = tarraySapLong - 1; lv2 >= 0; lv2--)
           {
               s6 = tds.Tables[0].Rows[lv2]["PlantCode"].ToString().Trim(); // Forecast_PNGroupSN    
               s3 = tds.Tables[0].Rows[lv2]["CustomerPN"].ToString().Trim();  // CustomerPN   Forecast_CustomerPN           

               if ((tPlantCode == s6) && (tCustomerPN == s3))  // InHouse_Qty
               {
                   // tarrayCustomerFoxconnPNToOneSet[lv1, tSLocOffSet] = tds.Tables[0].Rows[lv2]["InHouse_Qty"].ToString();
                   // s7 = tarrayCustomerFoxconnPNToOneSet[lv1, tSLocOffSet];
                   v5 = Convert.ToDecimal(tds.Tables[0].Rows[lv2]["InHouse_Qty"].ToString());
                   lv4 = Convert.ToInt32(v5);
                   tarrayCustomerFoxconnPNToOneSet[lv1, tSLocOffSet] = lv4.ToString();
                   lv2 = -12;
                   lv3++;
               }

           }

           if (lv2 > -11) s7 = "N";
           else s7 = "Y";

           tarrayCustomerFoxconnPNToOneSet[lv1, tSapInvFindFlagLoc] = s7.ToString();

       }  // end teachIDSetCount 

       return (lv3.ToString());
   }   // end GetSapInvQtyInArrarOnSet
   
   // 20110223
   // public string GetSapInvQtyInArrarOnSet(int teachIDSetCount, string[,] tarrayCustomerFoxconnPNToOneSet, int tarraySapLong, DataSet tds, int tarrayCustomerFoxconnPNToOneSetIndex, int tSapinarrayCustomerFoxconnPNToOneSetloc, int tPlantCodeinarrayCustomerFoxconnPNToOneSetloc )
   //{
   //    int lv1 = 0;
   //    int lv2 = 0;
   //    int lv3 = 0;
   //    int lv4 = 0;
   //    string s1 = "";
   //    string s3 = "";
   //    string tPlantCode = "";
   //    string tCustomerPN = "";
   //    string s6 = "";
   //    string s7 = "";
   //    decimal v5 = 0;
   //
   //    int tPLocOffSet = tarrayCustomerFoxconnPNToOneSetIndex - tPlantCodeinarrayCustomerFoxconnPNToOneSetloc;
   //    int tSLocOffSet = tarrayCustomerFoxconnPNToOneSetIndex - tSapinarrayCustomerFoxconnPNToOneSetloc;
   //    // arrayNokiaEVDocm[localvar + 1, 1] = ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
   //    // arrayNokiaEVDocm[localvar + 1, 2] = ds6.Tables[0].Rows[localvar]["plant_name"].ToString();  // CustomerSite
   //
   //    for (lv1 = 1; lv1 < teachIDSetCount + 1; lv1++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA
   //    {
   //        tPlantCode = tarrayCustomerFoxconnPNToOneSet[lv1, tPLocOffSet].ToString().Trim();  // tPlantCoded    
   //        tCustomerPN = tarrayCustomerFoxconnPNToOneSet[lv1, 3].ToString().Trim();  // CustomerPN          
   //        for (lv2 = tarraySapLong-1; lv2 >= 0; lv2--)
   //        {
   //            s6 = tds.Tables[0].Rows[lv2]["PlantCode"].ToString().Trim(); // Forecast_PNGroupSN    
   //            s3 = tds.Tables[0].Rows[lv2]["CustomerPN"].ToString().Trim();  // CustomerPN   Forecast_CustomerPN           
   //
   //            if ((tPlantCode == s6) && (tCustomerPN == s3))  // InHouse_Qty
   //            {
   //                    // tarrayCustomerFoxconnPNToOneSet[lv1, tSLocOffSet] = tds.Tables[0].Rows[lv2]["InHouse_Qty"].ToString();
   //                    // s7 = tarrayCustomerFoxconnPNToOneSet[lv1, tSLocOffSet];
   //                   v5 = Convert.ToDecimal(tds.Tables[0].Rows[lv2]["InHouse_Qty"].ToString());
   //                   lv4 = Convert.ToInt32(v5);
   //                   tarrayCustomerFoxconnPNToOneSet[lv1, tSLocOffSet]= lv4.ToString();
   //                   lv2 = 0;
   //                   lv3++;
   //            }
   //         }
   //    }
   //
   //    return (lv3.ToString());
   //}   // end GetSapInvQtyInArrarOnSet
     

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

   public string GetParainGWCapp(int teachIDSetCount, string[,] tarrayCustomerFoxconnPNToOneSet, int tarrayPlantCodeLong, string[,] tarrayPlantCode, int tarrayCustomerFoxconnPNToOneSetIndex, int tPlantCodeinarrayCustomerFoxconnPNToOneSetloc, string[,] tDataPara, int tCapDayQtyLoc, int tFoxconnBULoc, int tDataSetGWCappLoc, int tPNGroupLoc, int tGWCappFindFlagLoc)
   {
       int tmpv1 = 0;    // AssyPN_MoldPN Count
       int Vtmpv2 = 0;
       int Vtmpv1 = 0;
       int v1, v2, v3, v4=0, v5=0;
       string s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, tmps1;
       int tLocOffSet = tarrayCustomerFoxconnPNToOneSetIndex - tPlantCodeinarrayCustomerFoxconnPNToOneSetloc; // PlantCode

       // int tSapParaCapLoc = tarrayCustomerFoxconnPNToOneSetIndex - SapParaCapLoc;  // Max - 21   
       // tarrayCustomerFoxconnPNToOneSet[v2, tSapParaCapLoc - 7];  // CapDayQty

       tmps1 = " Select * from GWCapp order by FoxconnSite, FoxconnBU, Dom_Exp, CustomerPN ";
       // DataSet dsGWCapp = DataConnlib.Get_InfoByPara(tmps1);  // All Data Not only EV
       DataSet dsGWCapp = PGetDataByPara(tmps1, "" );
       if (dsGWCapp.Tables.Count <= 0) tmpv1 = 0; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else
       {
           tmpv1 = dsGWCapp.Tables[0].Rows.Count;
       }

       for (v2 = 1; v2 < teachIDSetCount + 1; v2++)
       {
           s6 = tarrayCustomerFoxconnPNToOneSet[v2, 2].ToString();              // FoxconnSite 
           s7 = tarrayCustomerFoxconnPNToOneSet[v2, tFoxconnBULoc].ToString();  // FoxconnBU
           s8 = tarrayCustomerFoxconnPNToOneSet[v2, 1].ToString();              // Dom_Exp
           s9 = tarrayCustomerFoxconnPNToOneSet[v2, 3].ToString();              // CustomerPN

           for (v1 = dsGWCapp.Tables[0].Rows.Count - 1; v1 >= 0; v1--)
           {
               s1 = dsGWCapp.Tables[0].Rows[v1]["FoxconnSite"].ToString();
               s2 = dsGWCapp.Tables[0].Rows[v1]["FoxconnBU"].ToString();
               s3 = dsGWCapp.Tables[0].Rows[v1]["Dom_Exp"].ToString();
               s4 = dsGWCapp.Tables[0].Rows[v1]["CustomerPN"].ToString();
               s5 = dsGWCapp.Tables[0].Rows[v1]["CapDayQty"].ToString();
                          
               // if ((s1 == s6) && (s2 == s7) && (s3 == s8) && (s4 == s9)) 
               if ((s1 == s6) && (s2 == s7) && (s4 == s9)) 
               {
                   if ((s5 == "0") || (s5 == null)) s5 = "0";
                   tarrayCustomerFoxconnPNToOneSet[v2, tCapDayQtyLoc] = s5;  //  CapDayQty   
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 0] = dsGWCapp.Tables[0].Rows[v1]["Mold_Rate"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 1] = dsGWCapp.Tables[0].Rows[v1]["M_Base_Qty"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 2] = dsGWCapp.Tables[0].Rows[v1]["M_Splite_Qty"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 3] = dsGWCapp.Tables[0].Rows[v1]["M_MachineTime"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 4] = dsGWCapp.Tables[0].Rows[v1]["M_LaborTime"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 5] = dsGWCapp.Tables[0].Rows[v1]["M_WKCT_Desc"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 6] = dsGWCapp.Tables[0].Rows[v1]["M_Use_Rate"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 7] = dsGWCapp.Tables[0].Rows[v1]["M_T_Open"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 8] = dsGWCapp.Tables[0].Rows[v1]["M_T_Close"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 9] = dsGWCapp.Tables[0].Rows[v1]["M_T_Cut"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 10] = dsGWCapp.Tables[0].Rows[v1]["M_WKCT_Type"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 11] = dsGWCapp.Tables[0].Rows[v1]["A_Base_Qty"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 12] = dsGWCapp.Tables[0].Rows[v1]["A_Splite_Qty"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 13] = dsGWCapp.Tables[0].Rows[v1]["A_MachineTime"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 14] = dsGWCapp.Tables[0].Rows[v1]["A_LaborTime"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 15] = dsGWCapp.Tables[0].Rows[v1]["A_WKCT_Desc"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 16] = dsGWCapp.Tables[0].Rows[v1]["A_Use_Rate"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 17] = dsGWCapp.Tables[0].Rows[v1]["A_T_Open"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 18] = dsGWCapp.Tables[0].Rows[v1]["A_T_Close"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 19] = dsGWCapp.Tables[0].Rows[v1]["A_T_Cut"].ToString();
                   tarrayCustomerFoxconnPNToOneSet[v2, tDataSetGWCappLoc - 20] = dsGWCapp.Tables[0].Rows[v1]["A_WKCT_Type"].ToString();
                   if (tarrayCustomerFoxconnPNToOneSet[v2, tarrayCustomerFoxconnPNToOneSetIndex-2] == "")  // PNGroup, MCGCode
                       tarrayCustomerFoxconnPNToOneSet[v2, tPNGroupLoc] = dsGWCapp.Tables[0].Rows[v1]["PNGroup"].ToString();
                 
                   v1 = -10;     // break
                   Vtmpv1++;     // Valid 
                   tarrayCustomerFoxconnPNToOneSet[v2, tGWCappFindFlagLoc] = "Y";   
               } // end if ((s1 == s6) && (s2 == s7) && (s4 == s9)) 
           }   // end for (v1 = dsGWCapp.Tables[0].Rows.Count - 1; v1 >= 0; v1--)

           if (v1 != -11)  // Not-Found in GWCapp then Insert
           {
               Vtmpv2++;
               // Gmspm1有兩筆Dom_Exp 只須一筆, 判並與上一筆重覆不處理
               // if ( (s6 == tarrayCustomerFoxconnPNToOneSet[v2+1, 2].ToString()) && (s7 == tarrayCustomerFoxconnPNToOneSet[v2+1, tFoxconnBULoc].ToString()) && ( s9 == tarrayCustomerFoxconnPNToOneSet[v2+1, 3].ToString() ) )  
               // {
                   tarrayCustomerFoxconnPNToOneSet[v2, tGWCappFindFlagLoc] = "N";                  
               // }
               
           }
       }

       // Gmspm1有兩筆Dom_Exp 只須一筆, 判並與上一筆重覆不處理 
       for (v2 = 1; v2 < teachIDSetCount + 1; v2++)
       {   
           
           if (tarrayCustomerFoxconnPNToOneSet[v2, tGWCappFindFlagLoc] == "N")  // check double
           {
               s6 = tarrayCustomerFoxconnPNToOneSet[v2, 2].ToString();              // FoxconnSite 
               s7 = tarrayCustomerFoxconnPNToOneSet[v2, tFoxconnBULoc].ToString();  // FoxconnBU
               s8 = tarrayCustomerFoxconnPNToOneSet[v2, 1].ToString();              // Dom_Exp
               s9 = tarrayCustomerFoxconnPNToOneSet[v2, 3].ToString();              // CustomerPN
               for (v3 = 1; v3 < teachIDSetCount + 1; v3++)
               {
                   if (v2 == v3)
                   {
                   }
                   else
                   if ((s6 == tarrayCustomerFoxconnPNToOneSet[v3, 2].ToString()) && (s7 == tarrayCustomerFoxconnPNToOneSet[v3, tFoxconnBULoc].ToString()) && (s9 == tarrayCustomerFoxconnPNToOneSet[v3, 3].ToString()))
                   {
                       if (tarrayCustomerFoxconnPNToOneSet[v3, tGWCappFindFlagLoc] != "Y")
                       {
                           tarrayCustomerFoxconnPNToOneSet[v3, tGWCappFindFlagLoc] = "Y";
                           v4++;
                       }
                       v3 = teachIDSetCount + 1;                       
                   }             

               }

           }
       }
       return ( Vtmpv1.ToString() + "/" + teachIDSetCount.ToString() + "/" + tmpv1.ToString() );
       
   } // end of   GetParainGWCapp
       
   public static bool DBExcute(string sql)
   {
       string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
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

   // public static DataSet PGetDataByPara(string sql)
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

   public string GetNokiaCalendar(string ttSPDate, string[,] tarrayNokiaCalendar, int tarrayNokiaCalendarLong)
   {
       int lv1 = 0;
       int lv2 = 0;
       string s1="", s2="", s3="";
             
       // arrayNokiaEVDocm[localvar + 1, 1] = ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
       // arrayNokiaEVDocm[localvar + 1, 2] = ds6.Tables[0].Rows[localvar]["plant_name"].ToString();  // CustomerSite

       lv2 = Convert.ToInt32(ttSPDate.ToString());
       for (lv1 = 1; lv1 < tarrayNokiaCalendarLong + 1; lv1++)  // first loop start 開始用 Customer+Foxconn+PN 為一組做 ETD to ETA
       {

           if ((lv2 >= Convert.ToInt32(tarrayNokiaCalendar[lv1, 3].ToString())) && (lv2 <= Convert.ToInt32(tarrayNokiaCalendar[lv1, 4].ToString())) )  // InHouse_Qty
           {
                   s1  = tarrayNokiaCalendar[lv1,1].ToString(); 
                   s2  = tarrayNokiaCalendar[lv1,2].ToString();
                   lv1 = tarrayNokiaCalendarLong + 1;
           }
           
       }

       if ( (s1 != "") && ( s2 != "" ) ) s3 = s1 + "-" + s2; 
       
       return (s3);
   }   // GetNokiaCalendar

   //  20100805 Syncro_4A3_Detail 找最大  Document_ID, Get EV  Region, 
   //         {
   //             subtmpstr1 = "select a.Conversation_ID, a.Week, a.Document_ID, a.Forecast_CustomerPN, a.Forecast_QtyTypeCode, "
   //             + " a.Forecast_BeginDate, a.Forecast_IntervalCode, a.Forecast_Qty, a.Foxconn_Site, a.CustomerSite, a.Agreement, "
   //             + " a.Item, a.Datafrom, a.Project, a.DocumentTime, a.Customer, a.HHPARTS, a.FoxconnBU, a.FoxconnRegion "
   //             + " from   Syncro_4A3_Detail a, "
   //             + " ( select Forecast_CustomerPN, CustomerSite, Datafrom, FoxconnRegion, Forecast_BeginDate, max(Document_ID) as Document_ID "
   //                 //  + " from Syncro_4A3_Detail Where documenttime like '" + DownDVDay + "'+'%' " + PA[2] + PA[6] + PA[7] + PA[9] + PA[11] + PA[15] "
   //             + " from Syncro_4A3_Detail Where documenttime like '" + DownDVDay + "'+'%'   "
   //             + PA[2] + PA[6] + PA[7] + PA[9] + PA[11] + PA[15]
   //             + " group by Datafrom, CustomerSite, FoxconnRegion, Forecast_CustomerPN, Forecast_BeginDate   ) as b "
   //             + " where a.Forecast_CustomerPN = b.Forecast_CustomerPN  "
   //             + " and   a.CustomerSite = b.CustomerSite  "
   //             + " and   a.Datafrom     = b.Datafrom      "
   //             + " and   a.FoxconnRegion= b.FoxconnRegion "
   //             + " and   a.Document_ID  = b.Document_ID   "
   //             + " and   a.Forecast_BeginDate = b.Forecast_BeginDate "
   //             + " order by a.Datafrom, a.CustomerSite, a.FoxconnRegion, a.Forecast_CustomerPN, a.Document_ID, a.Forecast_BeginDate  ";
   //         }

}  // end public class ShipPlanlib
}  // end namespace Economy.Publibrary


