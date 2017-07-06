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

/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
/// 
public class DVShipPlanlib
{
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
        if ((lvar5 + 1) > arrayCustomerFoxconnPNToOneSetIndex) return ("0");
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

    public string GetPassShipPlanToken(ref string[,] arrayParam, int tmparrayParamLong, string tmpPassToken) 
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
                    if (arrayParam[Arraynum - 10, i2] == "")
                    {
                        arrayParam[Arraynum - 10, i2] = tmpstr1;
                        i2 = tmparrayParamLong;
                    } 
                }

                wriarraysw = "N";  // Clear

            }

        } // end for i1=0

        retval = DateTime.Now.ToString("yyyyMMddHHmmssmm"); // OK

        if ( ( arrayParam[13, 1] != "" ) && ( arrayParam[13, 1] != null ) ) retval = retval + arrayParam[13, 1];
        return (retval);
    }


    public string GetSubFoxconnSitefromDetail(string tmpPassFoxconnSite)
    {
        string retstring = "";
        retstring = tmpPassFoxconnSite.Substring(10, tmpPassFoxconnSite.Length - 10);
        retstring = retstring.Substring(0, retstring.IndexOf(":"));  
        return (retstring);
    }
}


