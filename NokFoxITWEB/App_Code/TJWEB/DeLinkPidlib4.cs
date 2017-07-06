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
using SFC.TJWEB;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using System.Linq;
using System.Web.UI;


namespace SFC.TJWEB
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class DeLinkPidlib4
{
    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd"); 
    static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    DeLinkPidlib DeLinkPidlibPointer = new DeLinkPidlib();
    DeLinkPidlib3 DeLinkPidlib3Pointer = new DeLinkPidlib3();
    // FSplitlib FSplitlibPointer = new FSplitlib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    string DBType = "oracle";
    protected string Backdbstring = ConfigurationManager.AppSettings["NormalBakupConnectionString"];


    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");

  
    public string GetPIDIMEI(string Type1, string dbtype, string dbstring, string dbWristring, string InTYpe, string IData)  // return IMEI
    {
        string Ret1 = "", sql1 = "";
        if ( InTYpe  == "1" ) // Input PID Ret IMEINUM
             sql1 = " select * from SHP.CMCS_SFC_IMEINUM where PRODUCT_ID = '" + IData + "' ";
        else
             sql1 = " select * from SHP.CMCS_SFC_IMEINUM where IMEINUM = '" + IData + "' ";
        
        DataSet dt1 = DataBaseOperation.SelectSQLDS(dbtype, dbstring, sql1);
        int v3 = dt1.Tables[0].Rows.Count;
        if (v3 > 0)
        {
            if (InTYpe == "1") // Input PID Ret IMEINUM
                Ret1 = dt1.Tables[0].Rows[0]["IMEINUM"].ToString();
            else
                Ret1 = dt1.Tables[0].Rows[0]["PRODUCT_ID"].ToString();
        }

        return (Ret1);
    }

    public string CopyL6PID(string P1, String DBType, string DBReadString, string DBWriString1, string DBWriString2, ref string[,] arrayOutDB)
    {
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, arrv1 = 10;
        string s1 = "", s2 = "", s3 = "";
        arrv1 = arrayOutDB.GetLength(0);
        if (arrv1 <= 1) return( s1 );
        string DBRead = arrayOutDB[1, 2]; 
        for (v1 = 0; v1 <= arrv1; v1++)
        {

        }

        DBRead = DBReadString; // 20130501 Add for Server L6 Read 
        string tmpsqlW = "", sp = "", t1 = "";
        int RetInt = 0, limno = 2000;
        // Read Data
        //string sqls1 = @"Select * from PUBLIB.L6TOL8T2 order by DocumentID asc"; // ordrer by DocumentID asc";
        string sqls1 = @"Select  t.*, ROWNUM  from PUBLIB.L6TOL8T2 t  where  ROWNUM <= " + limno + "   order by DocumentID asc ";
        DataSet dt1 = DataBaseOperation.SelectSQLDS("oracle", DBRead, sqls1);
        if (dt1.Tables.Count <= 0) return (s1);
        v3 = dt1.Tables[0].Rows.Count;
        if (v3 <= 0) return (s1);
        //   string[,] arrDeLinkRou = new string[v3 + 2, 10 + 1];
        string st1 = "", st2 = "";
        string[,] arrL6Data = new string[v3 + 1, 10 + 1];
        string tmpWDB1 = DBWriString1; // 221 ConfigurationManager.AppSettings["L8TestandWebConnectionString"];
        string tmpWDB2 = DBWriString2; // 76

        string st11 = "";
        DataSet dsst1 = null;

        // Test
       // string st11 = "select count(*) from PUBLIB.MAINPIDTRACE";
       // DataSet dsst1 = DataBaseOperation.SelectSQLDS( "oracle", tmpWDB1, st11);
       // if (dsst1.Tables.Count > 0)
       // {
       //     v4 = dsst1.Tables[0].Rows.Count;
       //     st1 = dsst1.Tables[0].Rows[0][0].ToString();
       // }
        //for (v1 = 0; v1 < v4; v1++)
        //{
        //    st1 = dsst1.Tables[0].Rows[v1]["PID"].ToString();
        //    st2 = dsst1.Tables[0].Rows[v1]["PRODLINE"].ToString(); // dt1.Tables[0].Rows[v1]["Desp"].ToString();  // Convert to CustomerSite
        //    
        //}

        if ( limno > v3 ) limno = v3;
        for (v1 = 0; v1 < v3; v1++)
        {
            arrL6Data[v1 + 1, 0] = v1.ToString();
            arrL6Data[v1 + 1, 1] = dt1.Tables[0].Rows[v1][0].ToString();
            arrL6Data[v1 + 1, 2] = dt1.Tables[0].Rows[v1][1].ToString(); // dt1.Tables[0].Rows[v1]["Desp"].ToString();  // Convert to CustomerSite
            arrL6Data[v1 + 1, 3] = dt1.Tables[0].Rows[v1][2].ToString();  // Convert to CustomerSite
            t1 = dt1.Tables[0].Rows[v1][2].ToString();
            t1 = t1.Substring(0, 16);
            arrL6Data[v1 + 1, 3] = t1;
            arrL6Data[v1 + 1, 4] = dt1.Tables[0].Rows[v1][3].ToString();
           

            if ((arrL6Data[v1 + 1, 1] != "") && (arrL6Data[v1 + 1, 3] != ""))
            {
                tmpsqlW = "Insert into PUBLIB.MAINPIDTRACE ( PID, CDATE , CSTATUS, PRODLINE, INDATE ) "
                   + " Values ( '" + arrL6Data[v1 + 1, 1] + "', '" + arrL6Data[v1 + 1, 3] + "', '" + sp + "', '" + arrL6Data[v1 + 1, 2] + "', '" + arrL6Data[v1 + 1, 3] + "' ) ";
                RetInt = DataBaseOperation.ExecSQL("oracle", tmpWDB2, tmpsqlW);
                RetInt = DataBaseOperation.ExecSQL("oracle", tmpWDB1, tmpsqlW);
                if (RetInt > 0) arrL6Data[v1 + 1, 0] = "Y";
                tmpsqlW = "Insert into PUBLIB.MAINPIDBAK ( PID, CDATE , CSTATUS, PRODLINE, INDATE ) "
                   + " Values ( '" + arrL6Data[v1 + 1, 1] + "', '" + arrL6Data[v1 + 1, 3] + "', '" + sp + "', '" + arrL6Data[v1 + 1, 2] + "', '" + arrL6Data[v1 + 1, 3] + "') ";
                RetInt = DataBaseOperation.ExecSQL("oracle", tmpWDB2, tmpsqlW);
                RetInt = DataBaseOperation.ExecSQL("oracle", tmpWDB1, tmpsqlW);
            }

            // if (v1 >= limno) v1 = v3; // break
        }

        string dstr = ""; 
        for (v1 = 0; v1 < v3; v1++)
        {
            st11 = "select * from PUBLIB.MAINPIDTRACE where PID = '" + arrL6Data[v1 + 1, 1] + "' ";
            dsst1 = DataBaseOperation.SelectSQLDS("oracle", tmpWDB1, st11);
            if (dsst1.Tables.Count > 0) v4 = dsst1.Tables[0].Rows.Count;
            // if ( (v4 >= 1) && ( arrL6Data[v1 + 1, 0] == "Y" ) )// Del
            if ( v4 >= 1 ) 
            {
                // delete From PUBLIB.L6TOL8T1 Where DO
                dstr = "delete from PUBLIB.L6TOL8T2 where PSN =  '" + arrL6Data[v1 + 1, 1] + "' ";
                RetInt = DataBaseOperation.ExecSQL("oracle", DBRead, dstr);
            }


        }
        //string tmpsqlW = "select * from PUBLIB.L6ToL8T2 "; // order by documentid asc ";
        //int    RetInt = DataBaseOperation.ExecSQL( "oracle", DBRead, tmpsqlW);
        //if (RetInt <= 0) return (s1);  
        return( v3.ToString());//
    }  // Copy l6 to L8  

  
    public string CopyL6PIDCheck(string P1, String DBType, string DBReadString, string DBWriString)
    {
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, arrv1 = 10;
        string s1 = "", s2 = "", s3 = "";
        string tCurrtime = DateTime.Now.ToString("yyyyMMdd"); // DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tCurrtime1 = DateTime.Now.AddDays(-2).ToString("yyyyMMdd"); // AddDays(-160)
        string tmpsqlW = "", sp = "", t1 = "";
        int RetInt = 0, limno = 3000;
        // Read Data
        //string sqls1 = @"Select * from PUBLIB.L6TOL8T2 order by DocumentID asc"; // ordrer by DocumentID asc";
        string sqls1 = @"Select  * from PUBLIB.MAINPIDTRACE  where ( ( (CSTATUS is null) or (CSTATUS = '') or (CSTATUS = '0') )  "
            + " and ( ( SUBSTR(CDATE, 0, 8) <= '" + tCurrtime + "')  and ( SUBSTR(CDATE, 0, 8) >= '" + tCurrtime1 + "') )  ) ";
        DataSet dt1 = DataBaseOperation.SelectSQLDS( DBType, DBReadString, sqls1);
        if (dt1.Tables.Count <= 0) return (s1);
        v3 = dt1.Tables[0].Rows.Count;
        if (v3 <= 0) return (s1);
        //   string[,] arrDeLinkRou = new string[v3 + 2, 10 + 1];
        string st1 = "", st2 = "";
        string[,] arrL6Data = new string[v3 + 1, 20 + 1];
        
        string Rtype="1", dbtype=DBType, DBString=DBReadString;
        string tPID="", WO_NO="", LINE_NAME="", tSTATION_ID="", tIMEI="", tSTATUS="", SFC_Level="", STATION_IDTable="";
        string LTIME="", tModel="", tRoutingNo="", tPART="", Customer="", tCSTATUS = "0";

        v2 = 0;
        if (limno > v3) limno = v3;
        for (v1 = 0; v1 < v3; v1++)
        {
            WO_NO=""; LINE_NAME=""; tSTATION_ID=""; tIMEI=""; tSTATUS=""; SFC_Level=""; STATION_IDTable="";
            LTIME=""; tModel=""; tRoutingNo=""; tPART=""; Customer=""; tCSTATUS = "0";
            arrL6Data[v1 + 1, 0] = v1.ToString();
            arrL6Data[v1 + 1, 1] = dt1.Tables[0].Rows[v1]["PID"].ToString();
            arrL6Data[v1 + 1, 2] = ""; // dt1.Tables[0].Rows[v1]["Desp"].ToString();  
            arrL6Data[v1 + 1, 3] = ""; // dt1.Tables[0].Rows[v1][2].ToString(); 
            arrL6Data[v1 + 1, 5] = ""; // dt1.Tables[0].Rows[v1][3].ToString();
            arrL6Data[v1 + 1, 6] = ""; // dt1.Tables[0].Rows[v1][4].ToString();
            arrL6Data[v1 + 1, 7] = ""; // dt1.Tables[0].Rows[v1][5].ToString();
            arrL6Data[v1 + 1, 8] = ""; // dt1.Tables[0].Rows[v1][6].ToString();
            arrL6Data[v1 + 1, 9] = ""; // dt1.Tables[0].Rows[v1][6].ToString();
            arrL6Data[v1 + 1, 10] = ""; // dt1.Tables[0].Rows[v1][6].ToString();

            tPID = arrL6Data[v1 + 1, 1];
            if ( tPID != "")
            {
                v2++;
                s3 = GetPIDDataL6(Rtype, dbtype, DBString, DBString, DBString, tPID, ref  WO_NO, ref LINE_NAME, ref  tSTATION_ID, ref tIMEI, ref tSTATUS, ref  SFC_Level, ref STATION_IDTable, ref  LTIME, ref tModel, ref tRoutingNo, ref  tPART, ref Customer);
                arrL6Data[v1 + 1, 2] = tIMEI;
                arrL6Data[v1 + 1, 3] = "";  // LTIME <> CDATE
                arrL6Data[v1 + 1, 4] = WO_NO;
                arrL6Data[v1 + 1, 5] = tPART;
                arrL6Data[v1 + 1, 6] = SFC_Level;   // LEVELLINE
                arrL6Data[v1 + 1, 7] = tSTATION_ID; // LINEWS
                arrL6Data[v1 + 1, 8] = LINE_NAME; // PRODLINE
                arrL6Data[v1 + 1, 9] = tSTATUS; //  STATUS <> CSTATUS;  "1" Succ "0" Error 
                arrL6Data[v1 + 1, 10] = tRoutingNo;
                arrL6Data[v1 + 1, 11] = tModel;

                arrL6Data[v1 + 1, 12] = STATION_IDTable;
                arrL6Data[v1 + 1, 13] = Customer;
                arrL6Data[v1 + 1, 15] = "Y";           
                
            } 

            // if (v1 >= limno) v1 = v3; // break
        }

        dt1 = null;
        string sw1="";
        v2 = 0;
        for (v1 = 0; v1 < v3; v1++)
        {
            tPID = arrL6Data[v1 + 1, 1].ToString();
            sw1    = arrL6Data[v1 + 1, 15].ToString();
            WO_NO = ""; LINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = ""; SFC_Level = ""; STATION_IDTable = "";
            LTIME = ""; tModel = ""; tRoutingNo = ""; tPART = ""; Customer = ""; tCSTATUS = "";
            if ((tPID != "") && (sw1 == "Y")) // write
            {
                tIMEI = arrL6Data[v1 + 1, 2];
                // arrL6Data[v1 + 1, 3] = "";  // LTIME <> CDATE
                WO_NO = arrL6Data[v1 + 1, 4];
                tPART = arrL6Data[v1 + 1, 5];
                SFC_Level = arrL6Data[v1 + 1, 6];   // LEVELLINE
                tSTATION_ID = arrL6Data[v1 + 1, 7]; // LINEWS
                // LINE_NAME = arrL6Data[v1 + 1, 8]  LINE_NAME; // PRODLINE
                tCSTATUS = arrL6Data[v1 + 1, 9];     //  STATUS <> CSTATUS;
                tRoutingNo = arrL6Data[v1 + 1, 10];
                tModel = arrL6Data[v1 + 1, 11];
                tmpsqlW = "UPDATE PUBLIB.MAINPIDTRACE SET  IMEI =  '" + tIMEI + "', WO =  '" + WO_NO + "', PART = '" + tPART + "', "
                + "  LEVELLINE =  '" + SFC_Level + "', LINEWS =  '" + tSTATION_ID + "', PRODLINE =  '" + LINE_NAME + "', "
                + "  CSTATUS =  '" + tCSTATUS + "', ROUTINGNO =  '" + tRoutingNo + "', MODEL =  '" + tModel + "' "
                + " where PID =  '" + tPID + "' ";    
                v4 = DataBaseOperation.ExecSQL(DBType, DBWriString, tmpsqlW);
                v2++;
            }
        }

        return (v2.ToString());//
    }  // end CopyL6PIDCheck

    public string CopyL6PIDIMEI(string P1, String DBType, string DBReadString, string DBWriString)
    {
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, arrv1 = 10;
        string s1 = "", s2 = "", s3 = "";
        string tCurrtime = DateTime.Now.ToString("yyyyMMdd"); // DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tCurrtime1 = DateTime.Now.AddDays(-7).ToString("yyyyMMdd"); // AddDays(-160)
        string tmpsqlW = "", sp = "", t1 = "";
        int RetInt = 0, limno = 3000;
        // Read Data
        //string sqls1 = @"Select * from PUBLIB.L6TOL8T2 order by DocumentID asc"; // ordrer by DocumentID asc";

        string sqls1 = @" select  a.CDATE tcdate,  a.PID tpid, b.IMEINUM timeinum from PUBLIB.MAINPIDTRACE a, SHP.CMCS_SFC_IMEINUM b where "
            + " ( b.IMEINUM is not null and a.PID = b.PRODUCT_ID  "
            + " and ( ( SUBSTR(CDATE, 0, 8) <= '" + tCurrtime + "')  and ( SUBSTR(CDATE, 0, 8) >= '" + tCurrtime1 + "') ) "
            + "  and ( (a.IMEI is null ) or (a.IMEI =  ' ')  ) ) ";
        //string sqls1 = @"Select  * from PUBLIB.MAINPIDTRACE  where ( ( ( IMEI is null) or ( IMEI = '')  )  "
        //    + " and ( ( SUBSTR(CDATE, 0, 8) <= '" + tCurrtime + "')  and ( SUBSTR(CDATE, 0, 8) >= '" + tCurrtime1 + "') )  ) ";
        DataSet dt1 = DataBaseOperation.SelectSQLDS(DBType, DBReadString, sqls1);
        if (dt1.Tables.Count <= 0) return (s1);
        v3 = dt1.Tables[0].Rows.Count;
        if (v3 <= 0) return (s1);
        //   string[,] arrDeLinkRou = new string[v3 + 2, 5 + 1];
        string st1 = "", st2 = "";
        string[,] arrL6Data = new string[v3 + 1, 20 + 1];

        string Rtype = "1", dbtype = DBType, DBString = DBReadString;
        string tPID = "", WO_NO = "", LINE_NAME = "", tSTATION_ID = "", tIMEI = "", tSTATUS = "", SFC_Level = "", STATION_IDTable = "";
      

        v2 = 0;
        if (limno > v3) limno = v3;
        for (v1 = 0; v1 < v3; v1++)
        {
            arrL6Data[v1 + 1, 0] = v1.ToString();
            arrL6Data[v1 + 1, 1] = dt1.Tables[0].Rows[v1]["tpid"].ToString();
            arrL6Data[v1 + 1, 2] = dt1.Tables[0].Rows[v1]["timeinum"].ToString();   
            arrL6Data[v1 + 1, 3] = ""; // dt1.Tables[0].Rows[v1][2].ToString(); 
           // arrL6Data[v1 + 1, 5] = ""; // dt1.Tables[0].Rows[v1][3].ToString();
           // arrL6Data[v1 + 1, 6] = ""; // dt1.Tables[0].Rows[v1][4].ToString();
           // arrL6Data[v1 + 1, 7] = ""; // dt1.Tables[0].Rows[v1][5].ToString();
           // arrL6Data[v1 + 1, 8] = ""; // dt1.Tables[0].Rows[v1][6].ToString();
           // arrL6Data[v1 + 1, 9] = ""; // dt1.Tables[0].Rows[v1][6].ToString();
           // arrL6Data[v1 + 1, 10] = ""; // dt1.Tables[0].Rows[v1][6].ToString();          
            
        }

        dt1 = null;
        string sw1 = "";
        v2 = 0;
        for (v1 = 0; v1 < v3; v1++)
        {
            tPID = arrL6Data[v1 + 1, 1].ToString();
            tIMEI = arrL6Data[v1 + 1, 2].ToString();
            if ((tPID != "") && ( tIMEI != "")) // write
            {
                tmpsqlW = "UPDATE PUBLIB.MAINPIDTRACE SET  IMEI =  '" + tIMEI + "' where PID =  '" + tPID + "' ";
                v4 = DataBaseOperation.ExecSQL(DBType, DBWriString, tmpsqlW);
                v2++;
            }
        }

        return (v2.ToString());//
    }  // end CopyL6PIDIMEI
    
    public string GetPIDDataL6(string Rtype, string dbtype, string DBString, string DBReadString1, string DBWriString, string PID, ref string WO_NO, ref string LINE_NAME, ref string tSTATION_ID, ref string IMEI, ref string STATUS, ref string SFC_Level, ref string STATION_IDTable, ref string LTIME, ref string Model, ref string RoutingNo, ref string PART, ref string Customer)
    {
        DateTime d1 = DateTime.Today;
        int v1 = 0, v2 = 20, v3 = 0, ArrPtr = 1, Datecnt = 0;
        string[,] tmpBUFF = new string[v2 + 1, v2 + 1];
        for (v1 = 0; v1 < v2 + 1; v1++)
            for (v3 = 0; v3 < v2 + 1; v3++)
                tmpBUFF[v1, v3] = "";

        v1 = 0; v2 = 0; v3 = 0;
        string tPID = PID, Rets1 = "", tmp1 = "", tmp2 = "", tWO = "";
        string tSERIAL_NUMBER = "", tpart = "", tMODEL = "", tSTATION_NAME = "";
        DataSet tdt = null;
        if (Rtype != "1") return ("");

        string str1 = " SELECT A.SERIAL_NUMBER tmpSERIAL_NUMBER, A.MODEL_NAME tmppart, A.STATION_NAME tSTATION_NAME, B.MODEL tmpMODEL, "
             + " A.MO_NUMBER tmpWO FROM sfc.r_wip_tracking_t_pid A,SFC.CMCS_SFC_PCBA_BARCODE_CTL B "
             + " WHERE A.MODEL_NAME=B.SPART AND A.SERIAL_NUMBER = '" + tPID + "' "; // 52EE11105146347'
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "sfc.r_wip_tracking_t_pid";
        Rets1 = DeLinkPidlibPointer.GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "5");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            tSERIAL_NUMBER = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // PID
            tpart = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    // Part
            tSTATION_NAME = tmpBUFF[Convert.ToInt32(ArrPtr), 3];  // tdt.Tables[0].Rows[0]["tmpMODEL"].ToString();         // MODEL 
            tMODEL = tmpBUFF[Convert.ToInt32(ArrPtr), 4];  // MODEL
            tWO = tmpBUFF[Convert.ToInt32(ArrPtr), 5];  // WO 
        }

        ArrPtr++;
        string ttMODEL = "", tCUSTOMER_NAME = "";
        str1 = " SELECT DISTINCT SUBSTR(MODEL,1,3) AA, CUSTOMER_NAME BB FROM SFC.CMCS_SFC_MODEL WHERE SUBSTR(MODEL,1,3) = '" + tMODEL + "' ";
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.CMCS_SFC_MODEL";
        Rets1 = DeLinkPidlibPointer.GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            ttMODEL = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
            tCUSTOMER_NAME = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //             
        }
        Customer = tCUSTOMER_NAME;

        ArrPtr++;
        v2 = 0;
        string tLevel = "", tLineWS = "";
        str1 = " SELECT SFC_LINE, SFC_WS  FROM  SFC.SFC_LINE_WS   WHERE CUSTOMER_NO = '" + tCUSTOMER_NAME + "' ";  // and SFC_WS = '" + tCUSTOMER_NAME + "'
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.SFC_LINE_WS";
        Rets1 = DeLinkPidlibPointer.GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            tLevel = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
            tLineWS = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //             
        }

        string sqls2 = "select ROUTING_SEQUENCE_ID, PART_NUMBER from  SFC.SFC_ROUTING_HEADERS " +
                            " where model_name='" + tMODEL + "'  order by PART_NUMBER desc ";
                      //      " and (PART_NUMBER='' or PART_NUMBER is null)";

        int v7=0, v8=0, v9=0;
        string sp1 = "";
        DataSet dt2 = DataBaseOperation.SelectSQLDS( DBType, DBString, sqls2);
        if (dt2.Tables.Count <= 0) RoutingNo = "";
        else
        {
            v3 = dt2.Tables[0].Rows.Count;
            if (v3 <= 0) RoutingNo = "";
            else if (v3 == 1) RoutingNo = dt2.Tables[0].Rows[v7]["ROUTING_SEQUENCE_ID"].ToString();
            else
            {
                for (v7 = 0; v7 < v3; v7++)
                {
                    if (tpart == dt2.Tables[0].Rows[v7]["PART_NUMBER"].ToString())  v8 = v7 + 1;
                    else if (sp1 == dt2.Tables[0].Rows[v7]["PART_NUMBER"].ToString()) v9 = v7 + 1;                     
                }

                if (v8 > 0) RoutingNo = dt2.Tables[0].Rows[v8-1]["ROUTING_SEQUENCE_ID"].ToString();
                else RoutingNo = dt2.Tables[0].Rows[v9 - 1]["ROUTING_SEQUENCE_ID"].ToString();
            }
                            
            // RoutingNo = GetRoutingNO("P1", DBType, DBString, DBString, DBString, tSERIAL_NUMBER, ref tpart, ref tMODEL);
        }
        tSTATION_ID = tSTATION_NAME;
        IMEI = IMEI = GetPIDIMEI("1", dbtype, DBString, DBString, "1", PID);
        STATUS = "1";
        SFC_Level = tLevel;
        STATION_IDTable = "sfc.r_wip_tracking_t_pid";
        LTIME = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        Model = tMODEL;
        RoutingNo = RoutingNo;
        PART = tpart;
        Customer = tCUSTOMER_NAME;
        WO_NO = tWO;

        if ((Model == "") || (PART == "") || (WO_NO == "") || (RoutingNo == "")) STATUS = "0";

        return (PID);
        // Close this Proc

        ArrPtr++;
        v2 = 0;
        string MES_PCBA_WIP_PAUSEtime = "";
        str1 = " SELECT CREATION_DATE, WO_NO, STATION_ID, LINE_NAME from  SFC.MES_PCBA_WIP where PRODUCT_ID = '" + tPID + "' ";
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.MES_PCBA_WIP";
        Rets1 = DeLinkPidlibPointer.GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "4");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            // tmp1 = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
            // d1 = Convert.ToDateTime(tmp1);
            // tmp1 = ((DateTime)( d1 )).ToString("yyyyMMddHHmmssmm");  
            tmp1 = ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");
            tmpBUFF[Convert.ToInt32(ArrPtr), 1] = tmp1;
            MES_PCBA_WIP_PAUSEtime = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
            // MES_PCBA_WIP_PAUSEtime = ((DateTime)(tdt.Tables[0].Rows[0]["CREATION_DATE"])).ToString("yyyyMMddHHmmssmm");          
        }

        // Get MaxTime
        tmp1 = "0"; v2 = 4;
        for (v1 = Datecnt; v1 <= ArrPtr; v1++)  // 從有記錄日期開始
        {
            tmp2 = tmpBUFF[v1, 1].ToString();
            if ((tmp2 != "") && (Convert.ToInt64(tmp1) < Convert.ToInt64(tmp2)))
            {
                v2 = v1;
                tmp1 = tmpBUFF[v1, 1].ToString();
                STATION_IDTable = tmpBUFF[v1, 0].ToString();
                LTIME = tmpBUFF[v1, 1].ToString();
                WO_NO = tmpBUFF[v1, 2].ToString();
                tSTATION_ID = tmpBUFF[v1, 3].ToString();
                LINE_NAME = tmpBUFF[v1, 4].ToString();
            }

        }

        ArrPtr++;
        v2 = 0;
        // string tLevel = "", tLineWS = "";
        str1 = " SELECT SFC_LINE, SFC_WS  FROM  SFC.SFC_LINE_WS   WHERE CUSTOMER_NO = '" + tCUSTOMER_NAME + "' and SFC_WS = '" + tSTATION_ID + "' ";
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.SFC_LINE_WS";
        Rets1 = DeLinkPidlibPointer.GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            tLevel = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
            tLineWS = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //      
            SFC_Level = tLevel;  // Get Data
        }

        //         IMEI = GetPIDIMEI("1", dbtype, DBString, DBString, "1", PID);  // return IMEI
        //         WO_NO = GetWO_NO("P1", DBType, DBString, DBReadString1, PID, ref PART);
        //         Model = GetModelData("P1", DBType, DBString, PID);
        //         RoutingNo = GetRoutingNO("P1", DBType, DBString, DBWriString, DBReadString1, PID, ref PART, ref Model);

        if (tmp1 == "0") tmp1 = "";

        return (tmp1);

    } // end GetPIDDataL6 

    public string DeLinkOldDataUpdate(string P1, string DBType, string ReadDB, string WriDB1, string WriDB2, string begpid)
    {
        int v1 = 0, v2 = 0, v3 = 0, v4=0, v5=0;
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "";
        int limno = 20000;
        string tCurrtime = DateTime.Now.ToString("yyyyMMdd"); // DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tCurrtime1 = DateTime.Now.AddDays(-2).ToString("yyyyMMdd"); // AddDays(-160)
        tCurrtime  = "20110130";
        tCurrtime1 = "20000130";
        string tmpsql = "select PID  from PUBLIB.MAINPIDTRACE where "
        +" SUBSTR(CDATE, 0, 8) <= '" + tCurrtime + "'  and  SUBSTR(CDATE, 0, 8) >= '" + tCurrtime1 + "' order by CDATE desc";
        // string tmpsql = "select PID  from PUBLIB.MAINPIDTRACE  where PID = '" + begpid + "'  order by CDATE desc ";
        
        // string tCurrtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        // string tmpsql = "select SERIAL_NUMBER, IN_LINE_TIME from SFC. R_WIP_TRACKING_T_PID ";
        // string tmpsql = @"Select  t.SERIAL_NUMBER, t.IN_LINE_TIME, ROWNUM  from SFC. R_WIP_TRACKING_T_PID t  where     "
        //        + "   t.SERIAL_NUMBER <=  '" + begpid + "'   order by t.SERIAL_NUMBER asc ";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(DBType, ReadDB, tmpsql);
        if (ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 == 0) return ("");

        string Ret1 = "", tmpPID="", tWO_NO="", tLINE_NAME="", tSTATION_ID="", tIMEI="", tSTATUS="", SFC_LEVEL="", STATION_IDTable="";
        string LTIME="",  tpart="", tmodel="",  tcustomer="",  routno="";
             
        string[,] arrtmp = new string[v1 + 2, 14];
        for (v2 = 0; v2 < v1; v2++)
        {
            Ret1 = ""; tmpPID=""; tWO_NO=""; tLINE_NAME=""; tSTATION_ID=""; tIMEI=""; tSTATUS=""; SFC_LEVEL=""; STATION_IDTable="";
            LTIME="";  tpart=""; tmodel="";  tcustomer="";  routno="";
            arrtmp[v2 + 1, 0] = (v2 + 1).ToString();;
            arrtmp[v2 + 1, 1] = ds1.Tables[0].Rows[v2]["PID"].ToString();
            tmpPID = ds1.Tables[0].Rows[v2]["PID"].ToString();
            v4 = tmpPID.Length;
            arrtmp[v2 + 1, 13] = "";

            if ( (tmpPID != "") && ( v4 > 5  ) ) // Get Data
            {
                Ret1 = DeLinkPidlib3Pointer.GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tmodel, ref routno, ref tpart, ref tcustomer);
                
                arrtmp[v2 + 1, 2] = tWO_NO;
                arrtmp[v2 + 1, 3] = tSTATION_ID;
                arrtmp[v2 + 1, 4] = tIMEI;
                arrtmp[v2 + 1, 5] = tSTATUS;
                arrtmp[v2 + 1, 6] = SFC_LEVEL;
                arrtmp[v2 + 1, 7] = tpart;
                arrtmp[v2 + 1, 8] = tmodel;
                arrtmp[v2 + 1, 9] = routno;
                arrtmp[v2 + 1, 13] = "Y";

                if ( tSTATUS == "未進倉") arrtmp[v2 + 1, 5] = "4";
                else if ( tSTATUS == "已出庫") arrtmp[v2 + 1, 5] = "S";
                else arrtmp[v2 + 1, 5] = "3";

                //arrtmp[v2 + 1, 2] = tWO_NO; 
                //arrtmp[v2 + 1, 2] = tWO_NO; 
            }
        }

        string tmpsqlW1 = "", tmpsqlW2 = "", SP = "";
        for (v2 = 0; v2 < v1; v2++)
        {
            tmpPID = arrtmp[v2 + 1, 1]; //= ds1.Tables[0].Rows[v2]["SERIAL_NUMBER"].ToString();
          
            if  ( (tmpPID != "") && (   arrtmp[v2 + 1, 13] == "Y" ) ) 
            {
                tWO_NO= arrtmp[v2 + 1, 2];
                tSTATION_ID= arrtmp[v2 + 1, 3];
                tIMEI= arrtmp[v2 + 1, 4];
                tSTATUS= arrtmp[v2 + 1, 5];
                SFC_LEVEL= arrtmp[v2 + 1, 6];
                tpart= arrtmp[v2 + 1, 7];
                tmodel= arrtmp[v2 + 1, 8];
                routno= arrtmp[v2 + 1, 9];
                tmpsqlW1 = "UPDATE PUBLIB.MAINPIDTRACE SET WO = '" + tWO_NO + "', IMEI = '" + tIMEI + "', LEVELLINE = '" + SFC_LEVEL + "', PART = '" + tpart + "', "
                + " MODEL = '" + tmodel + "', ROUTINGNO = '" + routno + "', WSSTSTUS = '" + tSTATUS + "'     where  PID = '" + tmpPID + "'  ";
                // tmpsqlW2 = "UPDATE PUBLIB.MAINPIDBAK   SET CDATE = '" + tmp2 + "'  where  PID = '" + tmp1 + "'  ";
                v3 = DataBaseOperation.ExecSQL(DBType, WriDB1, tmpsqlW1);
                //v3 = DataBaseOperation.ExecSQL(DBType, WriDB1, tmpsqlW2);
                tmp3 = tmp1;  // Last PID
            }
            else          
            {    
                tmp4 = tmp1;
            }

            //if (WriDB2 != "")
            //{
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW1);
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW2);
            //}
        }

        return (tmp3);

    }

    // Move L6 SFIS1,C_MODEL_DESC_T to L10 
    public string L6ToL8SFCSFIS1C_MODEL_DESC_T(string ptype, string DBtype, string DBRead, string DBWri)
    {
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, RetInt = 0;
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", Ret1 = "", INSStr = "";
        string tCurrtime = DateTime.Now.ToString("yyyyMMdd"); // DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tCurrtime1 = DateTime.Now.AddDays(-2).ToString("yyyyMMdd"); // AddDays(-160)
        string tmpsql = "select * from SFIS1.C_MODEL_DESC_T order by MODEL_NAME desc ";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(DBType, DBRead, tmpsql);
        if (ds1 == null) return ( "-1");
        if (ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 == 0) return ("0");

        string[,] arrL6 = new string[v1 + 2, 14];
        for (v2 = 0; v2 < v1; v2++)
        {
            arrL6[v2 + 1, 0] = (v2 + 1).ToString(); ;
            arrL6[v2 + 1, 1] = ds1.Tables[0].Rows[v2]["MODEL_NAME"].ToString();
            arrL6[v2 + 1, 2] = ds1.Tables[0].Rows[v2]["MODEL_SERIAL"].ToString();
            arrL6[v2 + 1, 3] = ds1.Tables[0].Rows[v2]["MODEL_TYPE"].ToString();
            arrL6[v2 + 1, 4] = ds1.Tables[0].Rows[v2]["CUSTOMER"].ToString();
            arrL6[v2 + 1, 5] = ds1.Tables[0].Rows[v2]["STD_PKG_QTY"].ToString();
            arrL6[v2 + 1, 6] = ds1.Tables[0].Rows[v2]["TYPE"].ToString();
            arrL6[v2 + 1, 7] = ds1.Tables[0].Rows[v2]["CUST_NAME"].ToString();
            arrL6[v2 + 1, 13] = "";  // Check Flag
        }


        // L10
        string tmpsq2 = "select * from SFIS1.C_MODEL_DESC_T order by MODEL_NAME desc ";
        DataSet ds2 = DataBaseOperation.SelectSQLDS(DBType, DBWri, tmpsq2);
        if (ds2 == null) return ("-1");
        if (ds2.Tables.Count > 0) v3 = ds2.Tables[0].Rows.Count;
       
        string[,] arrL10 = new string[v3 + 2, 14];
        for (v4 = 0; v4 < v3; v4++)
        {
            arrL10[v4 + 1, 0] = (v4 + 1).ToString(); ;
            arrL10[v4 + 1, 1] = ds2.Tables[0].Rows[v4]["MODEL_NAME"].ToString();
            //arrL10[v4 + 1, 2] = ds1.Tables[0].Rows[v4]["MODEL_SERIAL"].ToString();
            //arrL10[v4 + 1, 3] = ds1.Tables[0].Rows[v4]["MODEL_TYPE"].ToString();
            //arrL10[v4 + 1, 4] = ds1.Tables[0].Rows[v4]["CUSTOMER"].ToString();
            //arrL10[v4 + 1, 5] = ds1.Tables[0].Rows[v4]["STD_PKG_QTY"].ToString();
            //arrL10[v4 + 1, 6] = ds1.Tables[0].Rows[v4]["TYPE"].ToString();
            //arrL10[v4 + 1, 7] = ds1.Tables[0].Rows[v4]["CUST_NAME"].ToString();
            //arrL10[v4 + 1, 13] = "";  // Check Flag
        }

        // Check and Insert
        string tmpmodel_name = "", Iflag = "N";
        for (v2 = 0; v2 < v1; v2++)
        {
            Iflag = "Y";
            tmpmodel_name = arrL6[v2 + 1, 1];
            // Check loop
            for (v4 = 0; v4 < v3; v4++)
            {
                if (arrL10[v4 + 1, 1] == tmpmodel_name)
                {
                    Iflag = "N";
                    v4 = v3; // break
                }
            }

            if (( Iflag.ToUpper() == "Y") && (tmpmodel_name != "")) // Insert 
            {
                if (arrL6[v2 + 1, 5] == "") v5 = 0; 
                else v5 = Convert.ToInt32(arrL6[v2 + 1, 5]);
                INSStr = "Insert into SFIS1.C_MODEL_DESC_T  ( MODEL_NAME, MODEL_SERIAL, MODEL_TYPE, CUSTOMER, STD_PKG_QTY, TYPE, CUST_NAME ) "
             + " Values ( '" + arrL6[v2 + 1, 1] + "', '" + arrL6[v2 + 2, 2] + "', '" + arrL6[v2 + 1, 3] + "' ,  "
             + " '" + arrL6[v2 + 1, 4] + "', " + v5 + " , '" + arrL6[v2 + 1, 6] + "', '" + arrL6[v2 + 1, 7] + "') ";
                RetInt = DataBaseOperation.ExecSQL("oracle", DBWri, INSStr);                           
            }

        }


        return ("1");
    } // end L6ToL8SFCSFIS1C_MODEL_DESC_T


}  // end public class DeLinkPId 
}  // end namespace SFC.TJWEB


