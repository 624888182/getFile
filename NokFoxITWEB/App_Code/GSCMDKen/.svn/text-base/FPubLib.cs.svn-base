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
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data.OracleClient;
using System.Data;
using System.Data.Sql;


namespace SCM.GSCMDKen
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class FPubLib
{
    protected DateTime Pubtmptoday = DateTime.Today;
    protected string PubCurrtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string PubCuurDate = DateTime.Today.ToString("yyyyMMdd");
    protected string CurrDate = DateTime.Today.ToString("yyyyMMdd");
    protected string CtlDate = "99999999"; // DateTime.Today.ToString("yyyyMMdd");
    protected string PStrSpilt = "";
    protected string DBsql = "sql"; 
    public static string PubDefaultConnString = "Data Source=10.83.16.74;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public string PubRunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    FSplitlib FSplitlibPointer = new FSplitlib();
    Convertlib ConvertlibPointer = new Convertlib();
    // FPubLib FPubLibPointer = new FPubLib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    // FPriLib FPriLibPointer = new FPriLib(); 
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    
    #region privateVariant

    private static string FERROR = "";
    private static int ERRORCode = 0;
    private string sDBType = "";
    private OracleConnection oraConn = null;
    private OracleTransaction oraTran = null;
    private SqlConnection sqlConn = null;
    private SqlTransaction sqlTran = null;

    #endregion
    ////////////////////////////////////////////////////////////////////////////
    ///// Modify 20120205
    ///// <param name="tdate"></param> Date
    ///// <param name="jcode"></param> "01","02, "51"
    ///// <param name="datano"></param> How many days need ?
    ///// <param name="DBFConnStr"></param> BDase IP
    ///// <param name="ticketqty"></param>  要多少數量
    ///// <param name="TmpDBType"></param>　sql", oracle", 
    ///// reurn: ticket number
    ///////////////////////////////////////
    public string PubGet_Ticket(string tdate, string jcode, string datano, string DBFConnStr, int ticketqty, string TmpDBType)
    {
        string swflag = "Y";
        string ssql1 = "";
        string ssql2 = "";
        string ssqlWri = "";
        string tticket = "20100823";
        string tmpno = "N";
        string retval = "-1";
        string tSemaphone11 = "N";
        string tSemaphone12 = "N";
        string tSemaphone21 = "N";
        string tSemaphone22 = "N";
        string readflag = "1";
        int Long1 = 0;
        double ticketseq = 1;
        int waittime = 0;
        string RunPath = ConfigurationManager.AppSettings["Sql221String"];
        string DBType = "sql";
        int RetInt = 0;
        DataSet sds1 = null;

        if (Convert.ToInt32(CurrDate) > Convert.ToInt32(CtlDate)) return ("-1");  // Control Date

        if ((tdate.ToString() == "") || (jcode.ToString() == "")) return ("-1");

        if (DBFConnStr != "") RunPath = DBFConnStr;  // 送進 DataConnecting Para

        if (TmpDBType != "") DBType = TmpDBType.ToLower();   // Sql or Oracle DBF

        if (ticketqty <= 0) ticketqty = 1;


        if (datano == "11")
        {
            ssql1 = "select * from jcod01 where codedate= '" + tdate + "' and  code= '" + jcode + "' ";  // table error need update
            // DataSet sds1 = GetDataByDataPath(ssql1, RunPath); // CGetDataByPara(ssql1); 20120205
            sds1 = DataBaseOperation.SelectSQLDS(DBType, RunPath, ssql1); // CGetDataByPara(ssql1);
            Long1 = sds1.Tables[0].Rows.Count;
            if (Long1 == 0) retval = "-1";
            else
            {
                tSemaphone11 = sds1.Tables[0].Rows[0]["Semaphone1"].ToString().Trim();
                tSemaphone12 = sds1.Tables[0].Rows[0]["Semaphone2"].ToString().Trim();
                tticket = sds1.Tables[0].Rows[0]["ticket"].ToString().Trim();
                retval = sds1.Tables[0].Rows[0]["Para1"].ToString().Trim();
            }
            return (retval);  // Close 
        }



        while (swflag == "Y")
        {

            if (readflag != "")
            {
                ssql1 = "select * from jcod01 where codedate= '" + tdate + "' and  code= '" + jcode + "' ";  // table error need update
                // DataSet sds1 = GetDataByDataPath(ssql1, RunPath);  // DataSet sds1 = CGetDataByPara(ssql1); 20120105
                sds1 = DataBaseOperation.SelectSQLDS(DBType, RunPath, ssql1); // CGetDataByPara(ssql1);
                Long1 = sds1.Tables[0].Rows.Count;
                if (Long1 == 0)  // Insert and Break
                {
                    // ticketseq = (Convert.ToDouble(tdate)) * 10000000 + 1;  // Initial Value
                    ticketseq = (Convert.ToDouble(tdate)) * 10000000 + ticketqty;  // Initial Value
                    tticket = ticketseq.ToString();
                    ssqlWri = "Insert into jcod01 ( codedate, code, ticket, Semaphone1, Semaphone2  ) Values ( '" + tdate + "', '" + jcode + "', '" + tticket + "','" + tmpno + "', '" + tmpno + "'  ) ";

                    //if (!DBExcuteByDataPath(ssqlWri, RunPath)) retval = "-1";
                    RetInt = DataBaseOperation.ExecSQL(DBType, RunPath, ssqlWri);
                    if (RetInt < 0) retval = "-1";
                    else
                    {
                        readflag = "";
                        swflag = "N"; // closed
                        retval = tticket;
                        return (retval);
                    }
                }
                else
                {
                    if (readflag == "1")
                    {
                        tSemaphone11 = sds1.Tables[0].Rows[0]["Semaphone1"].ToString().Trim();
                        tSemaphone12 = sds1.Tables[0].Rows[0]["Semaphone2"].ToString().Trim();
                        tticket = sds1.Tables[0].Rows[0]["ticket"].ToString().Trim();
                    }
                    else
                        if (readflag == "2")
                        {
                            tSemaphone21 = sds1.Tables[0].Rows[0]["Semaphone1"].ToString().Trim();
                            tSemaphone22 = sds1.Tables[0].Rows[0]["Semaphone2"].ToString().Trim();
                            tticket = sds1.Tables[0].Rows[0]["ticket"].ToString().Trim();
                        }
                }   // end Long1 == 0
            }  // end readflag !=  ""


            if (readflag == "1")
            {
                if (tSemaphone11 == "N")
                {
                    ssqlWri = "UPDATE jcod01 SET Semaphone1 = 'Y' WHERE codedate = '" + tdate + "' and code = '" + jcode + "' ";
                    // if (!DBExcuteByDataPath(ssqlWri, RunPath)) retval = "-1";
                    RetInt = DataBaseOperation.ExecSQL(DBType, RunPath, ssqlWri);
                    if (RetInt < 0) retval = "-1";
                    else
                    {
                        swflag = "Y"; // closed
                        readflag = "2";
                    }
                }
                else // tSemaphone11 != "")
                {
                    Thread.Sleep(1000);	 // 10 Min 6000 000---休眠2秒鐘 2000               
                    waittime++;
                    if (waittime > 2)
                    {
                        waittime = 0;
                        ssqlWri = "UPDATE jcod01 SET Semaphone1 = 'N' WHERE codedate = '" + tdate + "' and code = '" + jcode + "' ";
                        // if (!DBExcuteByDataPath(ssqlWri, RunPath)) retval = "-1";
                        RetInt = DataBaseOperation.ExecSQL(DBType, RunPath, ssqlWri);
                        if (RetInt < 0) retval = "-1";
                        else
                        {
                            swflag = "Y"; // closed
                            readflag = "1";
                        }
                    }
                }
            }
            else
                if (readflag == "2")
                {
                    if (tSemaphone22 == "N")
                    {
                        // tticket = (Convert.ToDouble(tticket) + 1).ToString();
                        tticket = (Convert.ToDouble(tticket) + ticketqty).ToString();
                        ssqlWri = "UPDATE jcod01 SET ticket = '" + tticket + "' WHERE codedate = '" + tdate + "' and code = '" + jcode + "' ";
                        //if (!DBExcuteByDataPath(ssqlWri, RunPath)) retval = "-1";
                        RetInt = DataBaseOperation.ExecSQL(DBType, RunPath, ssqlWri);
                        if (RetInt < 0) retval = "-1";
                        else
                        {
                            swflag = "N"; // closed
                            retval = tticket;
                            readflag = "";
                        }
                    }
                    else // tSemaphone11 != "")  give up
                    {
                        swflag = "Y";  // Repeat
                        readflag = "1";
                    }

                    ssqlWri = "UPDATE jcod01 SET Semaphone1='N',Semaphone2='N'  WHERE codedate = '" + tdate + "' and code = '" + jcode + "' ";
                    // if (!DBExcuteByDataPath(ssqlWri, RunPath)) retval = "-1";
                    RetInt = DataBaseOperation.ExecSQL(DBType, RunPath, ssqlWri);
                    if (RetInt < 0) retval = "-1";

                }


        } // end while

        return (retval);
    }   // end PubGet_Ticket 
    public string getNatWeekofthisYear(int typevar, int weekday1, int Dayofthisyear1, string Yearofthisyear, DateTime ttoday)
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
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0;

        localstr2 = Yearofthisyear;

        if (typevar == 1)  // By Para
        {
            i1 = Convert.ToDouble(weekday1);
            i2 = Convert.ToDouble(Dayofthisyear1);
            v1 = Convert.ToInt32(weekday1);
            v2 = Convert.ToInt32(Dayofthisyear1);
            localstr2 = Yearofthisyear;
        }
        else  // By para ttoday
        {
            i1 = Convert.ToDouble(ttoday.DayOfYear);
            i2 = Convert.ToDouble(ttoday.DayOfWeek);
            v1 = Convert.ToInt32(ttoday.DayOfYear);
            v2 = Convert.ToInt32(ttoday.DayOfWeek);
            localstr2 = ttoday.ToString("yyyyMMdd").Substring(0, 4);
        }

        if (v1 <= v2) returnvar1 = 1;
        else
        {
            if (v2 == 0) v2 = 7;

            v3 = (v1 - v2) % 7;     // Get first week days

            i3 = v1 - v3;
            i4 = i3 / 7;
            i1 = Math.Ceiling(i4);
            returnvar1 = Convert.ToInt32(i1);
        }

        returnstr1 = localstr2.Substring(0, 4) + "-WK" + returnvar1.ToString();
        // tmpSPWeek = returnstr1;
        return (returnstr1);
    }

   //////////////////////////////////////////////
   // MessLog  for all Record 
   //  t1 = FSplitlibPointer.Wri_MessLog("OrgDVQueryOK", v1.ToString(), QueryArr[3, 77], "", "", "", "", "", "", "");  // 10 Code
   public string Wri_MessLog( string F2, string F3, string F4, string F5, string F6, string F7, string F8, string F9, string F10, string DPath)
   {
       string ErrMsg="", LF0="", LF1="", LF2="", LF3="", LF4="", LF5="", LF6="", LF7="", LF8="", LF9="", LF10="";
 
       string RPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
       if ((DPath != "") && (DPath != null)) RPath = DPath;
    
       // protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
           
       if ((F2 == "") || (F2 == null)) LF2 = "Normal";
       else LF2 = F2;

       if ((F3 == "") || (F3 == null)) LF3 = "";
       else LF3 = F3;

       if ((F4 == "") || (F4 == null)) LF4 = "";
       else LF4 = F4;

       if ((F5 == "") || (F5 == null)) LF5 = "";
       else LF5 = F5;

       if ((F6 == "") || (F6 == null)) LF6 = "";
       else LF6 = F6;

       if ((F7 == "") || (F7 == null)) LF7 = "";
       else LF7 = F7;

       if ((F8 == "") || (F8 == null)) LF8 = "";
       else LF8 = F8;

       if ((F9 == "") || (F9 == null)) LF9 = "";
       else LF9 = F9;

       if ((F10 == "") || (F10 == null)) LF10 = "";
       else LF10 = F10;

       LF0 = LibSCM1Pointer.CNGet_TicketPath(DateTime.Today.ToString("yyyyMMdd"), LF2, "1", RPath, 1); // get Ticket
       LF1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");

       string sql1 = "Insert into MessLog ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10 ) "
       + " Values ( '" + LF0 + "', '" + LF1 + "', '" + LF2 + "', '" + LF3 + "', '" + LF4 + "', '" + LF5 + "', '" + LF6 + "' , "
       + " '" + LF7 + "', '" + LF8 + "', '" + LF9 + "', '" + LF10 + "' ) ";

       if (!LibSCM1Pointer.DBExcuteByDataPath( sql1, RPath))
            ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";

       return ("1");
   }  // end MessLog

   //////////////////////////////////////////////
   // PubMessLog  for all Record  
   public string PubWri_MessLog(string F2, string F3, string F4, string F5, string F6, string F7, string F8, string F9, string F10, string DPath, String DBType)
   {
       string ErrMsg = "", LF0 = "", LF1 = "", LF2 = "", LF3 = "", LF4 = "", LF5 = "", LF6 = "", LF7 = "", LF8 = "", LF9 = "", LF10 = "", TmpDBType;

       if (DBType == "") TmpDBType = "sql";
       else TmpDBType = DBType.ToLower();

       string RPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
       if ((DPath != "") && (DPath != null)) RPath = DPath;

       // protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");

       if ((F2 == "") || (F2 == null)) LF2 = "Normal";
       else LF2 = F2;

       if ((F3 == "") || (F3 == null)) LF3 = "";
       else LF3 = F3;

       if ((F4 == "") || (F4 == null)) LF4 = "";
       else LF4 = F4;

       if ((F5 == "") || (F5 == null)) LF5 = "";
       else LF5 = F5;

       if ((F6 == "") || (F6 == null)) LF6 = "";
       else LF6 = F6;

       if ((F7 == "") || (F7 == null)) LF7 = "";
       else LF7 = F7;

       if ((F8 == "") || (F8 == null)) LF8 = "";
       else LF8 = F8;

       if ((F9 == "") || (F9 == null)) LF9 = "";
       else LF9 = F9;

       if ((F10 == "") || (F10 == null)) LF10 = "";
       else LF10 = F10;

       //LF0 = LibSCM1Pointer.CNGet_TicketPath(DateTime.Today.ToString("yyyyMMdd"), LF2, "1", RPath, 1); // get Ticket
       LF0 = PubGet_Ticket(DateTime.Today.ToString("yyyyMMdd"), LF2, "1", RPath, 1, TmpDBType.ToLower());
       LF1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");

       string sql1 = "Insert into MessLog ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10 ) "
       + " Values ( '" + LF0 + "', '" + LF1 + "', '" + LF2 + "', '" + LF3 + "', '" + LF4 + "', '" + LF5 + "', '" + LF6 + "' , "
       + " '" + LF7 + "', '" + LF8 + "', '" + LF9 + "', '" + LF10 + "' ) ";
       //if (!LibSCM1Pointer.DBExcuteByDataPath(sql1, RPath))
       //     ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";

       int RetInt = DataBaseOperation.ExecSQL(TmpDBType.ToLower(), RPath, sql1);
       if (RetInt < 0) ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";

       return ("1");
   }  // end PubMessLog


   /////////////////////////////////////////////////////////////////
   // TYpe = "1" : Normal just check User/Password
   //
   //
   public string ChkUsrPSType(string PUser, string PPassword, string DBstring, string ptype)
   {
      
      //String RPSStr = "";
      string s1="",s2="",s3="",s4="", s5="";
      char[] Rtrschar = new char[20];
      string tmpUsername = PUser, FPassword = PPassword;

      if ((tmpUsername == "") || (FPassword == "")) s1 = "-1";
      string tmpsql = "select * from Fuser1 where UserID = '" + tmpUsername + "' ";
      // DataSet tmpds = LibSCM1Pointer.GetDataByDataPath(tmpsql, DBstring);
      DataSet tmpds = DataBaseOperation.SelectSQLDS(DBsql, DBstring, tmpsql); 
      if ((tmpds.Tables.Count <= 0) || (s1 == "-1") || (FPassword != tmpds.Tables[0].Rows[0]["PassWD"].ToString()))
      {
          return("-1");  
      }
      else
      {
          s2 = (tmpds.Tables[0].Rows.Count).ToString();
          s3 = tmpds.Tables[0].Rows[0]["PassWD"].ToString();
          s4 = tmpds.Tables[0].Rows[0]["CheckSumNum"].ToString();          
      }

       return("0");
    }   // end ChkUsrPSType

   /////////////////////////////////////////////////////////////////
   // TYpe = "1" : Normal just check User/Password
   // string ptype ="1" 新增, ptype="2" 修改, ptype = "3" 刪除 
   // string DBtype  : sql
   // 轉換密碼並從時間取的加密 Code 形成新密碼
   public string UsrPSFunc(string PUser, string PPassword, string PITSystem, string NewPS, string DBstring, string ptype, string DBsql, string Supermode, string Passwd2, string Oratype, string OraWriDB )
   {
       int FLong = 0, v1 = 0, v2 = 0, RetInt=0;
       String RPSStr = "";
       string f1=ptype, f2="", s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", t1="", t2="", t3="", t4="", t5="";
       char[] Rtrschar = new char[16];
       string tmpUsername = PUser, FPassword = PPassword;
       string InsSql = "", retval = "", tmpsql="";
       DataSet tmpds = null;
       t4 = DateTime.Now.ToString("yyyyMMddHHmm");
       string ITrspass = "", UTrspass = "", DeITrspass = "";

       if ((tmpUsername == "") || (FPassword == "")) return ("-1");

       // DBstring = "Data Source=10.186.33.41;Initial Catalog=ERPDBF;User ID =sa;Password=Sa123456;Timeout=120;"; // Test Only
     
       tmpsql = "select * from Fuser1 where UserID = '" + tmpUsername + "' ";
       //tmpds = LibSCM1Pointer.GetDataByDataPath(tmpsql, DBstring);       
       tmpds = DataBaseOperation.SelectSQLDS(DBsql, DBstring, tmpsql); 
       if (tmpds.Tables.Count <= 0) return ("-1");
       if (tmpds.Tables[0].Rows.Count <= 0) f2 = "1";  // f2 = "1" Not-found
       else                                            // f2 = "2" found password error
       {                                               // f2 = "3" found password right
           f2 = "3";
           s2 = (tmpds.Tables[0].Rows.Count).ToString();
           s3 = tmpds.Tables[0].Rows[0]["PassWD"].ToString();
           s4 = tmpds.Tables[0].Rows[0]["CheckSumNum"].ToString();
           s5 = tmpds.Tables[0].Rows[0]["OperRoleGpCode"].ToString();  // Random Code
           s6 = tmpds.Tables[0].Rows[0]["ITsystem"].ToString();
           s7 = tmpds.Tables[0].Rows[0]["PositionSeries"].ToString();  // 第 2 密碼

            // t3 = EclPS(FPassword, s5, PStrSpilt );
            // t3 = FPriLibPointer.EclPS(FPassword, s5, PStrSpilt);
           t3 = LibUsrPointer.PSaddEclCode(FPassword, s5, PStrSpilt);
           DeITrspass = LibUsrPointer.ITrsPassVal("2", PUser, s3); // Decode Test

           if ( t3 != s4) f2 = "2";  // password error 
           if ( Supermode.ToLower() == "s") f2 = "3";  // SuperUsers Can do all 
       }

       if (( ptype == "1" ) && ( f2 != "1" )) return("-1");   // 新增又有資料
       if (( ptype == "2")  && ( f2 == "1" )) return("-1");   // 修改無資料
       if (( ptype == "3")  && ( f2 == "1" )) return("-1");   // 刪除無資料

       if ( ((ptype == "2") || ( ptype == "3"))  && ( f2 == "2" )  )  return ("-1");  // 修改, 刪除密碼錯誤

       //t1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
       //v1 = t1.Length;
       //for (v2 = 0; v2 < v1; v2++)
       //    t2 = t2 + t1.Substring(v1 - v2 -1, 1);
       //t3 = EclPS(PPassword, t2, PStrSpilt);  // pass ^= t1  
      

       // ITrspass   = ITrsPassVal("1", PUser, PPassword);
       // DeITrspass = ITrsPassVal("2", PUser, ITrspass);

       //UTrspass = UTrsPassVal(PUser, PPassword);
       if ( ( f1 == "1" ) && (f2 == "1") ) // f1:Insert, f2='1' Not Record
       {
           t1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
           v1 = t1.Length;
           for (v2 = 0; v2 < v1; v2++)
               t2 = t2 + t1.Substring(v1 - v2 - 1, 1);

           t3 = LibUsrPointer.PSaddEclCode(PPassword, t2, PStrSpilt);  // pass ^= t1   

           // InsSql = "Insert into FUser1( UserID, UserName, PassWD, OperRoleGpCode, CheckSumNum, ITSystem, PositionSeries  ) Values "
           //         + " ( '" + PUser + "', '" + PUser + "',  '" + PPassword + "', '" + t2 + "',  '" + t3 + "',  '" + PITSystem + "',  '" + Passwd2 + "') ";
           ITrspass = LibUsrPointer.ITrsPassVal("1", PUser, PPassword);  // 變更儲存轉換密碼
           InsSql = "Insert into FUser1( UserID, UserName, PassWD, OperRoleGpCode, CheckSumNum, ITSystem, PositionSeries  ) Values "
                    + " ( '" + PUser + "', '" + PUser + "',  '" + ITrspass + "', '" + t2 + "',  '" + t3 + "',  '" + PITSystem + "',  '" + Passwd2 + "') ";
           RetInt = DataBaseOperation.ExecSQL("sql", DBstring, InsSql);
           if (RetInt <= 0) retval = "-1";           
           return (retval);        
       }

       if ((f1 == "2") && (f2 == "3"))   // 休改
       {
           t1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
           v1 = t1.Length;
           for (v2 = 0; v2 < v1; v2++)
               t2 = t2 + t1.Substring(v1 - v2 - 1, 1);

           t3 = LibUsrPointer.PSaddEclCode(NewPS, t2, PStrSpilt);  // pass ^= t1   Passwd2
           ITrspass = LibUsrPointer.ITrsPassVal("1", PUser, NewPS);  // 變更儲存轉換密碼
           // for (v2 = 0; v2 < 16; v2++ ) Rtrschar[v2] = Convert.ToChar(t3.Substring(v2, 1));
           InsSql = "UPDATE PUBLIB.FUser1 SET PassWD = '" + ITrspass + "', OperRoleGpCode = '" + t2 + "', CheckSumNum = '" + t3 + "', "
                 + "  ITSystem = '" + PITSystem + "',   PositionSeries = '" + Passwd2 + "'    WHERE UserID = '" + PUser + "' ";
           RetInt = DataBaseOperation.ExecSQL(Oratype, OraWriDB, InsSql); // Write Oracle

           InsSql = "UPDATE FUser1 SET PassWD = '" + ITrspass + "', OperRoleGpCode = '" + t2 + "', CheckSumNum = '" + t3 + "', "
                  + "  ITSystem = '" + PITSystem + "',   PositionSeries = '" + Passwd2 + "'    WHERE UserID = '" + PUser + "' ";         
           RetInt = DataBaseOperation.ExecSQL("sql", DBstring, InsSql);  // Write Sql 
           if (RetInt < 0) retval = "-1";
           return (retval);    
       }

       if ((f1 == "3") && (f2 == "3"))   // Delete
       {
           InsSql = "DELETE FUser1 WHERE UserID = '" + PUser + "' ";
           RetInt = DataBaseOperation.ExecSQL("sql", DBstring, InsSql);
           if (RetInt < 0) retval = "-1";
           return (retval);
       }


       return (retval);
   }   // end ChkUsrPSType

   

   public string ChkUsrPSExist(string PUser, string PPassword, string DBstring)
   {
       string  s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", t3 = "";
       char[] Rtrschar = new char[16];
       string tmpUsername = PUser, FPassword = PPassword;
       string tmpsql = "";
       DataSet tmpds = null;

       // DBstring = "Data Source=10.186.33.41;Initial Catalog=ERPDBF;User ID =sa;Password=Sa123456;Timeout=120;"; // Test Only

       if ((tmpUsername == "") || (FPassword == "")) return ("-1");

       tmpsql = "select * from FUser1 where UserID = '" + tmpUsername + "' ";
       tmpds = DataBaseOperation.SelectSQLDS(DBsql, DBstring, tmpsql); // CGetDataByPara(ssql1);
       //tmpds = LibSCM1Pointer.GetDataByDataPath(tmpsql, DBstring);
       if (tmpds.Tables.Count <= 0) return ("-1");
       if (tmpds.Tables[0].Rows.Count <= 0) return("-1");  
       else                                            
       {                                               
           s2 = (tmpds.Tables[0].Rows.Count).ToString();
           s3 = tmpds.Tables[0].Rows[0]["PassWD"].ToString();
           s4 = tmpds.Tables[0].Rows[0]["CheckSumNum"].ToString();
           s5 = tmpds.Tables[0].Rows[0]["OperRoleGpCode"].ToString();  // Random Code
           s6 = tmpds.Tables[0].Rows[0]["ITsystem"].ToString();
           s7 = tmpds.Tables[0].Rows[0]["PositionSeries"].ToString();  // 第 2 密碼

           t3 = LibUsrPointer.PSaddEclCode(FPassword, s5, PStrSpilt);
           if (t3 != s4) return("-1");  // password error 
       }

       return ("0");
   }   // end ChkUsrPSExist

   public string ChkUsrPSExistAllDB(string p1, string DBType, string DBstring, string PUser, string PPassword, string SecondPass, ref string tEMPTYPE )
   {
       string s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", t3 = "";
       char[] Rtrschar = new char[16];
       string tmpUsername = PUser, FPassword = PPassword;
       string tmpsql = "";
       DataSet tmpds = null;

       // DBstring = "Data Source=10.186.33.41;Initial Catalog=ERPDBF;User ID =sa;Password=Sa123456;Timeout=120;"; // Test Only

       if ((tmpUsername == "") || (FPassword == "")) return ("-1");

       if (DBType == "oracle") tmpsql = "select * from FUser1 where UserID = '" + tmpUsername + "' ";
       else                    tmpsql = "select * from FUser1 where UserID = '" + tmpUsername + "' ";
       tmpds = DataBaseOperation.SelectSQLDS(DBType, DBstring, tmpsql); // CGetDataByPara(ssql1);
       //tmpds = LibSCM1Pointer.GetDataByDataPath(tmpsql, DBstring);
       if (tmpds.Tables.Count <= 0) return ("-1");
       if (tmpds.Tables[0].Rows.Count <= 0) return ("-1");
       else
       {
           s2 = (tmpds.Tables[0].Rows.Count).ToString();
           s3 = tmpds.Tables[0].Rows[0]["PassWD"].ToString();
           s4 = tmpds.Tables[0].Rows[0]["CheckSumNum"].ToString();
           s5 = tmpds.Tables[0].Rows[0]["OperRoleGpCode"].ToString();  // Random Code
           s6 = tmpds.Tables[0].Rows[0]["ITsystem"].ToString();
           s7 = tmpds.Tables[0].Rows[0]["PositionSeries"].ToString();  // 第 2 密碼
           s8 = tmpds.Tables[0].Rows[0]["EMPTYPE"].ToString();  // 
           tEMPTYPE = s8; // 使用級別

           t3 = LibUsrPointer.PSaddEclCode(FPassword, s5, PStrSpilt);
           if (t3 != s4) return ("-1");  // password error 
       }

       return ( "0" );
   }   // end ChkUsrPSExist


}  // end public class FPubLib
}  // end namespace Economy.GCMDKen


// string ddlDNSql = @"SELECT DISTINCT INVOICE_NUMBER FROM CMCS_SFC_PACKING_LINES_ALL where INVOICE_NUMBER IS NOT null and "
//            + " to_char((SHIP_DATE),'yyyy/MM/dd') >= '" + txtStartDate.Text + "' and  to_char((SHIP_DATE),'yyyy/MM/dd') <= '" + txtEndDate.Text + "'"
//            + "   ORDER BY INVOICE_NUMBER DESC ";