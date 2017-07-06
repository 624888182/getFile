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
using System.Data.Odbc;
using System.Globalization;
using Economy.BLL;
using System.Data.OracleClient;
using System.Collections.Generic;




namespace SFC.TJWEB
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class EKanbanlib
{
    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd");   
    //ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    //DeLinkPidlib DeLinkPidlibPointer = new DeLinkPidlib();
    //DeLinkPidlib3 DeLinkPidlib3Pointer = new DeLinkPidlib3();
    // FSplitlib FSplitlibPointer = new FSplitlib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();

    string DBType = "oracle";
    static int PredayQty = 10;
    static int Gdaycnt = 3;

    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");

 //   public string CreWS(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpClass, string tmpDATATYPE)
 //   {
 //       return ("0");
 //   }
    public string CreWS(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpClass, string tmpDATATYPE)
    {
        //if (DateTime.Now.Hour < 8)
        //    SysDate = DateTime.Today.AddDays(-1).ToString("yyyMMdd");
        string Ret = string.Empty;
        DateTime bgTime = DateTime.Today;
        string sqlStation = "SELECT STATION FROM PUBLIB.TCWD02 WHERE DATATYPE = 'YIELDRATE'";
        DataTable dtStation = PDataBaseOperation.PSelectSQLDT(DBType, DBWriString, sqlStation);
        if (dtStation.Rows.Count == 0)
            return "Error";
        //string[] station = { "FAT", "ANT", "RDL", "FQC", "FI", "OQC" };
        decimal tatolYelid = 1;
        decimal allInput = 0;
        decimal allField = 0;
        decimal trueYelid = 1;
        //if (tmpClass == "1")
        //    SysDate += "083000";
        //else
        //    SysDate += "203000";
        for (int i = 0; i < dtStation.Rows.Count; i++)
        {
            Ret = Ret + "." + upDateYieldrate(PROLINE, dtStation.Rows[i][0].ToString(), DBReadString, DBWriString, DBType, SysDate, tmpClass, tmpDATATYPE);
        }
        //string[] sRet = Ret.Split('.');
        //for (int i = 0; i < sRet.Length; i++)
        //{
        //    if (sRet[i] == "Error")
        //        return "Error";
        //}
        string sql = "SELECT STATION,ALL_QTY,PASS_QTY,TMP2,TMP3 FROM PUBLIB.TCWD01 WHERE DATES = '" + SysDate + "' AND DATATYPE = '" + tmpDATATYPE + "' AND LINE = '" + PROLINE + "' AND CLASS1 = '" + tmpClass + "'";
        DataTable dt = PDataBaseOperation.PSelectSQLDT(DBType, DBWriString, sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][1].ToString() != "0")
                tatolYelid = tatolYelid * Convert.ToDecimal(dt.Rows[i][1].ToString()) / 100;
            if (dt.Rows[i][2].ToString().Trim() != "0")
                trueYelid = trueYelid * Convert.ToDecimal(dt.Rows[i][2].ToString()) / 100;
            allField += Convert.ToDecimal(dt.Rows[i][3].ToString());
            allInput += Convert.ToDecimal(dt.Rows[i][4].ToString());
        }
        string[] yelidArrary;
        yelidArrary = (tatolYelid * 100).ToString().Split('.');
        string sTYelid = yelidArrary[0].ToString();
        yelidArrary = (trueYelid * 100).ToString().Split('.');
        string tYelid = yelidArrary[0].ToString();
        // 20131116
        // string upSql = "UPDATE PUBLIB.TCWM01 SET YRTARGET = '" + sTYelid + "' ,YRINPUT = '" + allInput + "',YRFAILQTY = '" + allField + "' , YRTOT = '" + tYelid + "' WHERE DATES = '" + SysDate + "' AND LINE = '" + PROLINE + "' AND CLASS1 = '" + tmpClass + "'";
        string upSql = "UPDATE PUBLIB.TCWM01 SET YRTARGET = '" + sTYelid + "' WHERE DATES = '" + SysDate + "' AND LINE = '" + PROLINE + "' AND CLASS1 = '" + tmpClass + "'";
        int iRet = PDataBaseOperation.PExecSQL(DBType, DBWriString, upSql);
        return Ret;
    }

    // 20131118
    //public string upDateYieldrate(string line, string station, string DBReadString, string DBWriString, string DBType, string SysData, string workClass, string tmpDATATYPE)
    //{
    //    string dt = string.Empty;
    //    //if (workClass == "1")
    //    //    dt = (Convert.ToDecimal(bgTime) + 90000).ToString();
    //    //else
    //    //    dt = (Convert.ToDecimal(bgTime) + 870000).ToString();
    //    string sqlUpTmp = "UPDATE PUBLIB.TCWD01 SET TMP2 = '0' , TMP3 = '0' WHERE STATION = '" + station + "' AND DATES = '" + SysData + "' AND DATATYPE ='" + tmpDATATYPE + "'  ";
    //    int iRet = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlUpTmp);
    //    string sqldate = "SELECT BEGIN_TIME,END_TIME FROM PUBLIB.TCWD01 WHERE DATES = '" + SysData + "' AND LINE = '" + line + "' AND DATATYPE ='" + tmpDATATYPE + "' AND STATION = '" + station + "' AND CLASS1 = '" + workClass + "'";
    //    DataTable dtTime = PDataBaseOperation.PSelectSQLDT(DBType, DBWriString, sqldate);
    //    if (dtTime.Rows.Count == 0)
    //        return "Error";
    //    string bgTime = dtTime.Rows[0][0].ToString().Trim() + "00";
    //    dt = dtTime.Rows[0][1].ToString().Trim() + "59";
    //    string sql = "SELECT COUNT(*) FROM MSA.ROUTING_HISTORY WHERE STATION_GROUP = '" + station + "' AND LINE_ID = '" + line + "' AND TIME_END >= TO_DATE('" + bgTime + "','yyyy/mm/dd hh24:mi:ss') AND TIME_END <= TO_DATE('" + dt + "','yyyy/mm/dd hh24:mi:ss')";
    //    decimal qtyDt = Convert.ToInt32(PDataBaseOperation.PSelectSQLDT(DBType, DBReadString, sql).Rows[0][0].ToString());
    //    if (qtyDt == 0)
    //        return "Error";
    //    string sqlP = "SELECT COUNT(*) FROM MSA.ROUTING_HISTORY WHERE STATION_GROUP = '" + station + "' AND LINE_ID = '" + line + "' AND TIME_END >= TO_DATE('" + bgTime + "','yyyy/mm/dd hh24:mi:ss') AND TIME_END <= TO_DATE('" + dt + "','yyyy/mm/dd hh24:mi:ss') AND STATUS = 'P'";
    //    decimal qtyPass = Convert.ToInt32(PDataBaseOperation.PSelectSQLDT(DBType, DBReadString, sqlP).Rows[0][0].ToString());
    //    decimal baiFen = qtyPass / qtyDt * 100;
    //    string[] rates = baiFen.ToString().Split('.');
    //    string rate = rates[0];
    //    string insertSql = "UPDATE PUBLIB.TCWD01 SET PASS_QTY = '" + rate + "',TMP2='" + (qtyDt - qtyPass) + "',TMP3='" + qtyDt + "' WHERE STATION = '" + station + "' AND LINE = '" + line + "' AND CLASS1 = '" + workClass + "' AND DATES = '" + bgTime.Substring(0, 8) + "'";
    //    int Ret = PDataBaseOperation.PExecSQL(DBType, DBWriString, insertSql);
    //    return Ret.ToString();
    //}

    public string upDateYieldrate(string line, string station, string DBReadString, string DBWriString, string DBType, string SysData, string workClass, string tmpDATATYPE)
    {
        string dt = string.Empty;
        string lineTrue = string.Empty;
        //if (workClass == "1")
        //    dt = (Convert.ToDecimal(bgTime) + 90000).ToString();
        //else
        //    dt = (Convert.ToDecimal(bgTime) + 870000).ToString();

        lineTrue = line;
        string sqlUpTmp = "UPDATE PUBLIB.TCWD01 SET TMP2 = '0' , TMP3 = '0' WHERE STATION = '" + station + "' AND DATES = '" + SysData + "' AND DATATYPE ='" + tmpDATATYPE + "'  ";
        int iRet = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlUpTmp);
        string sqldate = "SELECT BEGIN_TIME,END_TIME FROM PUBLIB.TCWD01 WHERE DATES = '" + SysData + "' AND LINE = '" + line + "' AND DATATYPE ='" + tmpDATATYPE + "' AND STATION = '" + station + "' AND CLASS1 = '" + workClass + "'";
        DataTable dtTime = PDataBaseOperation.PSelectSQLDT(DBType, DBWriString, sqldate);
        if (dtTime.Rows.Count == 0)
            return "Error";
        string bgTime = dtTime.Rows[0][0].ToString().Trim() + "00";
        dt = dtTime.Rows[0][1].ToString().Trim() + "59";
        string sqlLine = @"SELECT TMP1,TMP2,TMP3,TMP4 FROM PUBLIB.TCWD02 WHERE STATION = '" + station + "' AND DATATYPE = '" + tmpDATATYPE + "'";
        DataTable dtLine = PDataBaseOperation.PSelectSQLDT(DBType, DBWriString, sqlLine);
        for (int i = 0; i < dtLine.Columns.Count; i++)
        {
            if (dtLine.Rows[0][i].ToString().Trim() != "")
                lineTrue = dtLine.Rows[0][i].ToString();
        }
        // 20140109 string sql = "SELECT COUNT(*) FROM MSA.ROUTING_HISTORY WHERE STATION_GROUP = '" + station + "' AND LINE_ID = '" + lineTrue + "' AND TIME_END >= TO_DATE('" + bgTime + "','yyyy/mm/dd hh24:mi:ss') AND TIME_END <= TO_DATE('" + dt + "','yyyy/mm/dd hh24:mi:ss')";
        string sql = @"SELECT COUNT(*) FROM ( SELECT DISTINCT PSN FROM MSA.ROUTING_HISTORY WHERE STATION_GROUP = '" + station + "' AND LINE_ID = '" + lineTrue + "' AND TIME_END >= TO_DATE('" + bgTime + "','yyyy/mm/dd hh24:mi:ss') AND TIME_END <= TO_DATE('" + dt + "','yyyy/mm/dd hh24:mi:ss'))";
        decimal qtyDt = Convert.ToInt32(PDataBaseOperation.PSelectSQLDT(DBType, DBReadString, sql).Rows[0][0].ToString());
        if (qtyDt == 0)
            return "Error";
        // 20140109 string sqlP = "SELECT COUNT(*) FROM MSA.ROUTING_HISTORY WHERE STATION_GROUP = '" + station + "' AND LINE_ID = '" + lineTrue + "' AND TIME_END >= TO_DATE('" + bgTime + "','yyyy/mm/dd hh24:mi:ss') AND TIME_END <= TO_DATE('" + dt + "','yyyy/mm/dd hh24:mi:ss') AND STATUS = 'P'";
        string sqlP = @"SELECT COUNT(*) FROM ( SELECT DISTINCT PSN FROM MSA.ROUTING_HISTORY WHERE STATION_GROUP = '" + station + "' AND LINE_ID = '" + lineTrue + "' AND TIME_END >= TO_DATE('" + bgTime + "','yyyy/mm/dd hh24:mi:ss') AND TIME_END <= TO_DATE('" + dt + "','yyyy/mm/dd hh24:mi:ss') AND STATUS = 'P')";
        decimal qtyPass = Convert.ToInt32(PDataBaseOperation.PSelectSQLDT(DBType, DBReadString, sqlP).Rows[0][0].ToString());
        decimal baiFen = qtyPass / qtyDt * 100;
        string[] rates = baiFen.ToString().Split('.');
        string rate = rates[0];
        //line = "7A";
        string insertSql = "UPDATE PUBLIB.TCWD01 SET PASS_QTY = '" + rate + "',TMP2='" + (qtyDt - qtyPass) + "',TMP3='" + qtyDt + "' WHERE STATION = '" + station + "' AND LINE = '" + line + "' AND CLASS1 = '" + workClass + "' AND DATES = '" + bgTime.Substring(0, 8) + "'";
        int Ret = PDataBaseOperation.PExecSQL(DBType, DBWriString, insertSql);
        return Ret.ToString();
    }

    ////////////////////////////////////////
    // Update ENO
    public string GetENOFromECCM1(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpSEQ_NUMBER, string tmpDATATYPE, string tmpClass1, string SubLINE )
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, DNdt1 = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 10, DNCnt = 0, dt1Cnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "", SN1 = "";
        string tmps = "", tmp0 = "", tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "", tmp8, tmp9;
        string tmp10 = "", tmp11 = "", tmp12 = "", tmp13 = "", tmp14 = "", tmp15 = "", tmp16 = "", tmp17 = "", tmp18, tmp19;
        string tmp20 = "", tmp21 = "", tmp22 = "", tmp23 = "", tmp24 = "", tmp25 = "", tmp26 = "", tmp27 = "", tmp28, tmp29;
        string tmp30 = "", tmp31 = "", tmp32 = "", tmp33 = "", tmp34 = "", tmp35 = "", tmp36 = "", tmp37 = "", tmp38, tmp39;
        string tmp40 = "", tmp41 = "", tmp42 = "", tmp43 = "", tmp44 = "", tmp45 = "", tmp46 = "", tmp47 = "", tmp48, tmp49;

        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "MSA", Wridir = "MSA";
        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv5.ToUpper() != "") Wridir = pv5;
        else Wridir = "PUBLIB";


        // Clear SET flag1 is space initial
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG1 = '' where ( FLAG1 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        sqlr = " select *  from " + Wridir + ".TCWD01  where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' "
             + " and SEQ_NUMBER = '" + tmpSEQ_NUMBER + "' and DATATYPE = '" + tmpDATATYPE + "'   and  ( (  FLAG1 is null )  or ( FLAG1 = '' ) )  ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0)  return("-1");  // if not data then return

        tmp1 = DNdt.Tables[0].Rows[0]["BEGIN_TIME"].ToString().Trim();
        tmp2 = DNdt.Tables[0].Rows[0]["END_TIME"].ToString().Trim();

        // 20131019
        //sqlr = " select *  from " + Readdir + ".WORK_ORDER_INPUT  where TO_CHAR(MANUFACTURE_DATE, 'yyyyMMddhh24mi' ) >= '" + tmp1 + "'  "
        //   + " and TO_CHAR(MANUFACTURE_DATE, 'yyyyMMddhh24mi' ) <= '" + tmp2 + "' ";
        //dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        //if ( dt1 == null) return ("-1"); // Syn Error
        //dt1Cnt = dt1.Tables[0].Rows.Count;
        //tmp1 = dt1Cnt.ToString();

        // 2. 取得ENO 組裝開始工站 ----> StartENO
        sqlr = "  select station from publib.tcwd02 where datatype='BEGINENO' ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt1 == null) return ("-1");// Syn Error
        string STARTENO = DNdt1.Tables[0].Rows[0]["station"].ToString();

        // sqlr = " select   count(*)  ATOCT from msa.routing_history ";
        // sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + SubLINE + "'     ";
        // sqlr = sqlr + "and  station_group ='" + STARTENO + "' AND STATUS ='P'";  // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
        // update 20131116 not need TSTAUS = 'P'
        sqlr = " select   count(*)  ATOCT from msa.routing_history ";
        sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + SubLINE + "'     ";
        sqlr = sqlr + "and  station_group ='" + STARTENO + "' ";  // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
        dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if ( dt1 == null) return ("-1");// Syn Error
        if (dt1.Tables[0].Rows.Count < 0) return ("0");
        tmp1 = dt1.Tables[0].Rows[0]["ATOCT"].ToString();
        v1 = Convert.ToInt32(tmp1);  // add qty 

        if ( v1 > 0)
            tmp1 = tmp1;
        string tmptime1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        tmpsqlW = " Update " + Wridir + ".TCWD01 set PASS_QTY = '" + tmp1 + "', UPDATE1  = '" + tmptime1 + "'  "
                + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' "
                + " and SEQ_NUMBER = '" + tmpSEQ_NUMBER + "' and DATATYPE = '" + tmpDATATYPE + "' ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        // 20131116
        tmpsqlW = " Update " + Wridir + ".TCWM01 set ASSY_TOT_QTY =  to_number( ASSY_TOT_QTY ) + " + v1 + " , UPDATE1  = '" + tmptime1 + "',  "
                + " ASSYDIFF =  to_number( ASSY_TOT_QTY ) + " + v1 + " - to_number( ACCUMRUN ),    "
                + " PACKINGDIFF =  to_number( PACKING_TOT_QTY )        - to_number( ACCUMRUN )    "
                + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "'  and   (  FLAG1 is null  or FLAG1 = '' )    ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);  
        if ( v5 > 0 ) // Update OK
        {
            tmpsqlW = " Update " + Wridir + ".TCWD01 set FLAG1 = 'Y'  "
                + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' "
                + " and SEQ_NUMBER = '" + tmpSEQ_NUMBER + "' and DATATYPE = '" + tmpDATATYPE + "' ";
            v6 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        }

        return ("0");

    }  // end GetENOFromECCM1


    /////////////////////////////////////////////////////////////////////////////////
    // Update ENO 不檢查 FLAG1 每次執行, 並須減去 TCWD01 中已有並回寫 TCWD01, TCWM01
    // 不須計次, 隨時可行 20131228
    public string GetENOFromECCM1_1(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpSEQ_NUMBER, string tmpDATATYPE, string tmpClass1, string SubLINE)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, DNdt1 = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 10, DNCnt = 0, dt1Cnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "", SN1 = "";
        string tmps = "", tmp0 = "", tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "", tmp8, tmp9;
        string tmp10 = "", tmp11 = "", tmp12 = "", tmp13 = "", tmp14 = "", tmp15 = "", tmp16 = "", tmp17 = "", tmp18, tmp19;
        string tmp20 = "", tmp21 = "", tmp22 = "", tmp23 = "", tmp24 = "", tmp25 = "", tmp26 = "", tmp27 = "", tmp28, tmp29;
        string tmp30 = "", tmp31 = "", tmp32 = "", tmp33 = "", tmp34 = "", tmp35 = "", tmp36 = "", tmp37 = "", tmp38, tmp39;
        string tmp40 = "", tmp41 = "", tmp42 = "", tmp43 = "", tmp44 = "", tmp45 = "", tmp46 = "", tmp47 = "", tmp48, tmp49;

        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "MSA", Wridir = "MSA";
        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv5.ToUpper() != "") Wridir = pv5;
        else Wridir = "PUBLIB";


        // Clear SET flag1 is space initial
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG1 = '' where ( FLAG1 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        sqlr = " select *  from " + Wridir + ".TCWD01  where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' "
             + " and SEQ_NUMBER = '" + tmpSEQ_NUMBER + "' and DATATYPE = '" + tmpDATATYPE + "'  ";   // 20131228 and  ( (  FLAG1 is null )  or ( FLAG1 = '' ) )  "; 
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("-1");  // if not data then return

        tmp1 = DNdt.Tables[0].Rows[0]["BEGIN_TIME"].ToString().Trim();
        tmp2 = DNdt.Tables[0].Rows[0]["END_TIME"].ToString().Trim();
        tmp3 = DNdt.Tables[0].Rows[0]["PASS_QTY"].ToString().Trim(); // 20131228  原已有
        if (tmp3 == "") tmp3 = "0";                                  // 20131228

        // 20131019
        //sqlr = " select *  from " + Readdir + ".WORK_ORDER_INPUT  where TO_CHAR(MANUFACTURE_DATE, 'yyyyMMddhh24mi' ) >= '" + tmp1 + "'  "
        //   + " and TO_CHAR(MANUFACTURE_DATE, 'yyyyMMddhh24mi' ) <= '" + tmp2 + "' ";
        //dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        //if ( dt1 == null) return ("-1"); // Syn Error
        //dt1Cnt = dt1.Tables[0].Rows.Count;
        //tmp1 = dt1Cnt.ToString();

        // 2. 取得ENO 組裝開始工站 ----> StartENO
        sqlr = "  select station from publib.tcwd02 where datatype='BEGINENO' ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt1 == null) return ("-1");// Syn Error
        string STARTENO = DNdt1.Tables[0].Rows[0]["station"].ToString();

        // sqlr = " select   count(*)  ATOCT from msa.routing_history ";
        // sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + SubLINE + "'     ";
        // sqlr = sqlr + "and  station_group ='" + STARTENO + "' AND STATUS ='P'";  // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
        // update 20131116 not need TSTAUS = 'P'
        sqlr = " select   count(*)  ATOCT from msa.routing_history ";
        sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + SubLINE + "'     ";
        sqlr = sqlr + "and  station_group ='" + STARTENO + "' ";  // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
        dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (dt1 == null) return ("-1");// Syn Error
        if (dt1.Tables[0].Rows.Count < 0) return ("0");
        tmp4 = dt1.Tables[0].Rows[0]["ATOCT"].ToString();
        v1 = Convert.ToInt32(tmp4);  // add qty 
        v1 = Convert.ToInt32(tmp4) - Convert.ToInt32(tmp3); // 新減舊 20131228

        if (v1 > 0)
            tmp4 = tmp4;
        string tmptime1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        tmpsqlW = " Update " + Wridir + ".TCWD01 set PASS_QTY = '" + tmp4 + "', UPDATE1  = '" + tmptime1 + "'  "
                + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' "
                + " and SEQ_NUMBER = '" + tmpSEQ_NUMBER + "' and DATATYPE = '" + tmpDATATYPE + "' ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        // 20131116
        tmpsqlW = " Update " + Wridir + ".TCWM01 set ASSY_TOT_QTY =  to_number( ASSY_TOT_QTY ) + " + v1 + " , UPDATE1  = '" + tmptime1 + "',  "
                + " ASSYDIFF =  to_number( ASSY_TOT_QTY ) + " + v1 + " - to_number( ACCUMRUN ),    "
                + " PACKINGDIFF =  to_number( PACKING_TOT_QTY )        - to_number( ACCUMRUN )    "
                + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "'  and   (  FLAG1 is null  or FLAG1 = '' )    ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        if (v5 > 0) // Update OK
        {
            tmpsqlW = " Update " + Wridir + ".TCWD01 set FLAG1 = 'Y'  "
                + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' "
                + " and SEQ_NUMBER = '" + tmpSEQ_NUMBER + "' and DATATYPE = '" + tmpDATATYPE + "' ";
            v6 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        }

        return ("0");

    }  // end GetENOFromECCM1_1


    ////////////////////////////////////////
    // Update ASSy
    public string GetENOFromECCM2_1(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpSEQ_NUMBER, string tmpDATATYPE, string tmpClass1, string SubLINE)
    {

        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, DNdt1 = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 10, DNCnt = 0, dt1Cnt = 0;
        string tmps = "", tmp0 = "", tmp1 = "", tmp2 = "", tmp3 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "MSA", Wridir = "MSA";

        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv5.ToUpper() != "")
            Wridir = pv5;
        else Wridir = "PUBLIB";

        // 1. 取得時間段到 BEGIN_TIME--> tmp1 / END_TIME--> tmp2
        sqlr = "  select * from " + Wridir + ".TCWD01 WHERE  DATES = '" + SysDate + "' AND LINE = '" + PROLINE + "' ";
        sqlr = sqlr + "AND SEQ_NUMBER ='" + tmpSEQ_NUMBER + "' AND DATATYPE ='" + tmpDATATYPE + "' and CLASS1='" + tmpClass1 + "' "; // 20130113 and (  FLAG1 is null  or FLAG1 = '' ) ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1");// Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("-1"); // if not data then return    
        tmp1 = DNdt.Tables[0].Rows[0]["BEGIN_TIME"].ToString().Trim();
        tmp2 = DNdt.Tables[0].Rows[0]["END_TIME"].ToString().Trim();
        tmp3 = DNdt.Tables[0].Rows[0]["PASS_QTY"].ToString().Trim(); // 20131228  原已有
        if (tmp3 == "") tmp3 = "0";

        // 2. 取得ATO 包裝最後工站 ----> EndATO
        sqlr = "  select station from publib.tcwd02 where datatype='ENDATO' ";

        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt1 == null) return ("-1");// Syn Error
        string EndATO = DNdt1.Tables[0].Rows[0]["station"].ToString();


        // 3. 取得ATO 類工站所有投入數量
        //   sqlr = " select   count(*)  ATOCT from msa.routing_history ";
        //   sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + PROLINE + "'     ";
        //   sqlr = sqlr + "and  station_group ='" + EndATO + "' AND STATUS ='P'";  // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
        // 20131019 add subline 
        sqlr = " select   count(*)  ATOCT from msa.routing_history ";
        sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + SubLINE + "'     ";
        sqlr = sqlr + "and  station_group ='" + EndATO + "'  ";
        // 20131118 sqlr = sqlr + "and  station_group ='" + EndATO + "' AND STATUS ='P'"; 
        // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
        dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);

        // 4. 更新相關 flag 欄位
        if (dt1 == null) return ("-1"); //Syn Error
        if (dt1.Tables[0].Rows.Count != 0)
        {
            tmpsqlW = " update  " + Wridir + ".TCWD01      set pass_qty ='" + dt1.Tables[0].Rows[0]["ATOCT"] + "'   ,FLAG1='Y' ,update1='" + DateTime.Now.ToString("yyyyMMddHHmmssmm") + "'";
            tmpsqlW = tmpsqlW + " WHERE  DATES ='" + SysDate + "' AND  LINE ='" + PROLINE + "' AND SEQ_NUMBER ='" + tmpSEQ_NUMBER + "' AND DATATYPE ='ATO'  and   (  FLAG1 is null  or FLAG1 = '' )  ";

            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            if (v5 > 0) // Update OK
            {
                v1 = Convert.ToInt32(tmp3);
                tmpsqlW = " Update " + Wridir + ".TCWM01 set PACKING_TOT_QTY =  to_number( PACKING_TOT_QTY ) + "  + dt1.Tables[0].Rows[0]["ATOCT"] + "  "
                        + " - " + v1 + "  , UPDATE1  = '" + DateTime.Now.ToString("yyyyMMddHHmmssmm") + "'  "
                        + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "'  and   (  FLAG1 is null  or FLAG1 = '' )     ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            }

        }

        return "0";
    }  // // end GetENOFromECCM2_1 
    ////////////////////////////////////////
    // Update ASSy
    public string GetENOFromECCM2(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpSEQ_NUMBER, string tmpDATATYPE, string tmpClass1, string SubLINE )
    {

        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, DNdt1 = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 10, DNCnt = 0, dt1Cnt = 0;
        string tmps = "", tmp0 = "", tmp1 = "", tmp2 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "MSA", Wridir = "MSA";

        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv5.ToUpper() != "")
            Wridir = pv5;
        else Wridir = "PUBLIB";

        // 1. 取得時間段到 BEGIN_TIME--> tmp1 / END_TIME--> tmp2
        sqlr = "  select * from " + Wridir + ".TCWD01 WHERE  DATES = '" + SysDate + "' AND LINE = '" + PROLINE + "' ";
        sqlr = sqlr + "AND SEQ_NUMBER ='" + tmpSEQ_NUMBER + "' AND DATATYPE ='" + tmpDATATYPE + "' and CLASS1='" + tmpClass1 + "'and (  FLAG1 is null  or FLAG1 = '' ) ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1");// Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("-1"); // if not data then return    
        tmp1 = DNdt.Tables[0].Rows[0]["BEGIN_TIME"].ToString().Trim();
        tmp2 = DNdt.Tables[0].Rows[0]["END_TIME"].ToString().Trim();

        // 2. 取得ATO 包裝最後工站 ----> EndATO
        sqlr = "  select station from publib.tcwd02 where datatype='ENDATO' ";

        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt1 == null) return ("-1");// Syn Error
        string EndATO = DNdt1.Tables[0].Rows[0]["station"].ToString();


        // 3. 取得ATO 類工站所有投入數量
     //   sqlr = " select   count(*)  ATOCT from msa.routing_history ";
     //   sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + PROLINE + "'     ";
     //   sqlr = sqlr + "and  station_group ='" + EndATO + "' AND STATUS ='P'";  // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
          // 20131019 add subline 
          sqlr = " select   count(*)  ATOCT from msa.routing_history ";
          sqlr = sqlr + "where time_end   >=to_date('" + tmp1 + "','yyyy/mm/dd hh24:mi:ss') and  time_end   <=to_date('" + tmp2 + "','yyyy/mm/dd hh24:mi:ss')  and  line_id ='" + SubLINE + "'     ";
          sqlr = sqlr + "and  station_group ='" + EndATO + "'  ";
          // 20131118 sqlr = sqlr + "and  station_group ='" + EndATO + "' AND STATUS ='P'"; 
          // in ( select station_group  from MSA.ROUTING_STATION_GROUP_B where group_level   ='ATO')";
          dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);

        // 4. 更新相關 flag 欄位
        if (dt1 == null) return ("-1"); //Syn Error
        if (dt1.Tables[0].Rows.Count != 0)
        {
            tmpsqlW = " update  " + Wridir + ".TCWD01      set pass_qty ='" + dt1.Tables[0].Rows[0]["ATOCT"] + "'   ,FLAG1='Y' ,update1='" + DateTime.Now.ToString("yyyyMMddHHmmssmm") + "'";
            tmpsqlW = tmpsqlW + " WHERE  DATES ='" + SysDate + "' AND  LINE ='" + PROLINE + "' AND SEQ_NUMBER ='" + tmpSEQ_NUMBER + "' AND DATATYPE ='ATO'  and   (  FLAG1 is null  or FLAG1 = '' )  ";

            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            if (v5 > 0) // Update OK
            {
                tmpsqlW = " Update " + Wridir + ".TCWM01 set PACKING_TOT_QTY =  to_number( PACKING_TOT_QTY ) + " + dt1.Tables[0].Rows[0]["ATOCT"] + " , UPDATE1  = '" + DateTime.Now.ToString("yyyyMMddHHmmssmm") + "'  "
                        + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "'  and   (  FLAG1 is null  or FLAG1 = '' )     ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            }

        }

        return "0";   
    }  // // end GetENOFromECCM2 


    // 同時執行 ECN, ATO 
    public string GetENOFromECCM3(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpSEQ_NUMBER, string tmpDATATYPE, string tmpClass1, string SubLINE)
    {

        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "", subline = ""; ;
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 10, DNCnt = 0, dt1Cnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "", SN1 = "";
        string tmps = "", tmp0 = "", tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "", tmp8, tmp9;
        string tmp10 = "", tmp11 = "", tmp12 = "", tmp13 = "", tmp14 = "", tmp15 = "", tmp16 = "", tmp17 = "", tmp18, tmp19;
        string tmp20 = "", tmp21 = "", tmp22 = "", tmp23 = "", tmp24 = "", tmp25 = "", tmp26 = "", tmp27 = "", tmp28, tmp29;
        string tmp30 = "", tmp31 = "", tmp32 = "", tmp33 = "", tmp34 = "", tmp35 = "", tmp36 = "", tmp37 = "", tmp38, tmp39;
        string tmp40 = "", tmp41 = "", tmp42 = "", tmp43 = "", tmp44 = "", tmp45 = "", tmp46 = "", tmp47 = "", tmp48, tmp49;

        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "MSA", Wridir = "MSA";
        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv5.ToUpper() != "") Wridir = pv5;
        else Wridir = "PUBLIB";


        // Clear SET flag1 is space initial
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG1 = '' where ( FLAG1 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        sqlr = " select *  from " + Wridir + ".TCWD01  where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "'  and TMP1 = '" + SubLINE + "'  "
             + " and DATATYPE = '" + tmpDATATYPE + "'   and  ( (  FLAG1 is null )  or ( FLAG1 = '' ) )   order by SEQ_NUMBER asc";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("-1");  // if not data then return

        string[,] arrayEB = new string[DNCnt + 1, 20 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayEB[v1, v2] = "";

      
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayEB[v1 + 1, 1] = (v1 + 1).ToString().Trim();
            arrayEB[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["SEQ_NUMBER"].ToString().Trim();
            arrayEB[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["BEGIN_TIME"].ToString().Trim();
            arrayEB[v1 + 1, 4] = DNdt.Tables[0].Rows[v1]["SEQ_NUMBER"].ToString().Trim();
            arrayEB[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["LINE"].ToString().Trim();
            arrayEB[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["DATES"].ToString().Trim();
            arrayEB[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["CLASS1"].ToString().Trim();
            arrayEB[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["DATATYPE"].ToString().Trim();
            arrayEB[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["TMP1"].ToString().Trim();  // Subline ENO or ATO
            
        }

        string tmpCurrtime = DateTime.Now.ToString("yyyyMMddHHmm");
        string tSEQ_NUMBER = "", ttime = "";
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            ttime = arrayEB[v1 + 1, 3].ToString().Trim();
            tmpDATATYPE = arrayEB[v1 + 1, 8].ToString().Trim();
            tSEQ_NUMBER = arrayEB[v1 + 1, 4].ToString().Trim();
            subline     = arrayEB[v1 + 1, 9].ToString().Trim();
            if (Convert.ToDecimal(ttime) >= Convert.ToDecimal(tmpCurrtime))  // Over Currency time can not execute
                v2 = v2; // Test 
            else
                if (tmpDATATYPE.ToUpper() == "ENO")
                {
                    Ret1 = tSEQ_NUMBER;
                    tmp0 = GetENOFromECCM1(BSite, Dtype, SysDate, PROLINE, DBType, DBReadString, DBWriString, "", tSEQ_NUMBER, tmpDATATYPE, tmpClass1, subline);
                }
                else
                    if (tmpDATATYPE.ToUpper() == "ATO")
                    {
                        Ret1 = tSEQ_NUMBER;
                        tmp0 = GetENOFromECCM2_1(BSite, Dtype, SysDate, PROLINE, DBType, DBReadString, DBWriString, "", tSEQ_NUMBER, tmpDATATYPE, tmpClass1, subline);
                    }
                    else
                    if (tmpDATATYPE.ToUpper() == "YIELDRATE")
                    {
                        Ret1 = tSEQ_NUMBER;
                        tmp0 = CreWS("ZZ", "I", SysDate, PROLINE, DBType, DBReadString, DBWriString, tSEQ_NUMBER, tmpClass1, tmpDATATYPE);
                        //tmp0 = GetENOFromECCM2(BSite, Dtype, SysDate, PROLINE, DBType, DBReadString, DBWriString, "", tSEQ_NUMBER, tmpDATATYPE, tmpClass1);
                        //tmp0 = EKanbanlibPointer.CreWS("ZZ", "I", SDate, TmpLine, dbtype, DBReadString, DBWriString, "", tClass, tmpDataType);
                    }
                    else 
                        v2 = v2;
                 
                   
        }


        return (Ret1);

    }  // end GetENOFromECCM3


    /// <summary>
    ///////////////////////////////////////////
    // Daily Initial Data 
    // 1. CHeck TCWM01, Do generate today data if not process , TCWM01 依 日期 + 線別 + 班別 為 Index Key 產生 TCWM01
    // 2. Check TCMD02, How many Class then generate TCWM01 data depend on CLass number 依多少班別產生多少筆資料
    // 3. Get TCMD02 seq_number( 2001 - 2009 ) Paramater then update TCWM01, 從 TCWD02 取得所有參數如 數量目標值, 班別時間,更新 TCWM01
    // 4. 依 WORKTIME, TMP1 = 10 , 將時間分割為每 10 分 1單元, 由 1001 開始產生 24 小時, 例 10/單元 - 24*60/10 = 144 插入 TCWD01
    // 5. 依 TCWD02 DATATYPE = 'YIELDRATE' 取出, 插入 TCWD01
   
    public string CreECCM1(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string timeslice, string tENOLINE, string tATOLINE)
    {

        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 10, DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "", SN1 = "";
        string tmps = "", tmp0 = "", tmp1 = "", tmp2 = "", tmp3="", tmp4="", tmp5="", tmp6="", tmp7="", tmp8, tmp9;
        string tmp10 = "", tmp11 = "", tmp12 = "", tmp13="", tmp14="", tmp15="", tmp16="", tmp17="", tmp18, tmp19;
        string tmp20 = "", tmp21 = "", tmp22 = "", tmp23="", tmp24="", tmp25="", tmp26="", tmp27="", tmp28, tmp29;
        string tmp30 = "", tmp31 = "", tmp32 = "", tmp33="", tmp34="", tmp35="", tmp36="", tmp37="", tmp38, tmp39;
        string tmp40 = "", tmp41 = "", tmp42 = "", tmp43="", tmp44="", tmp45="", tmp46="", tmp47="", tmp48, tmp49;
        string tmpClass1 = "1";

        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "PUBLIB", Wridir = "PUBLIB";

        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv5.ToUpper() != "") Wridir = pv5;
        else Wridir = "PUBLIB";


        // 判斷有無資料
        sqlr = " select *  from " + Wridir + ".TCWM01 where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "'  ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt > 0) return ("1");   // if not data then insert


        tmp0 = "0";
        tmp1 = "1";
        tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");

        /////////////////////////////////
        // Get TCMD02 班別分組取出有幾班
        tmp2 = "2";
        sqlr = " select distinct(Class1)  from " + Wridir + ".TCWD02 where SEQ_NUMBER >= 2000 and SEQ_NUMBER <= 2999 ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if ((DNdt != null) && (DNdt.Tables[0].Rows.Count > 0))
            DNCnt = DNdt.Tables[0].Rows.Count;
        else  // Insert 1 record when no paraamter get
        {
            DNCnt = 0;
            tmpsqlW = " insert into " + Wridir + ".TCWM01( DATES, LINE, WORKNO, ASSY_TARGET_QTY, ASSY_PART, PACKING_TARGET_QTY, "
                + " PACKING_PART, DOCUMENTID, UPDATE1, UPDATE2, UPDATE3, FLAG1, FLAG2, Class1 ) "
                + " values('" + SysDate + "','" + PROLINE + "',  '" + tmp1 + "','" + tmp0 + "','" + tmp0 + "', '" + tmp0 + "', '" + tmp0 + "', "
                + " '" + tDocumentID + "', '" + tDocumentID + "', '" + tmps + "', '" + tmps + "', '" + tmps + "', '" + tmps + "', '1' ) ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        }

        ////////////////////////////////////
        // 先依幾個班別產生主檔幾個 TCWM01
        for (v2 = 0; v2 < DNCnt; v2++)
        {
            tmp3 = (v2 + 1001).ToString();
            //tmp2 = DNdt.Tables[0].Rows[v2]["DATATYPE"].ToString().Trim();
            //tmp1 = DNdt.Tables[0].Rows[v2]["STATION"].ToString().Trim();
            //tmp4 = DNdt.Tables[0].Rows[v2]["ALL_QTY"].ToString().Trim();
            tmp5 = DNdt.Tables[0].Rows[v2]["Class1"].ToString().Trim();  // Class
            //tmp8 = DNdt.Tables[0].Rows[v2]["BEGIN_TIME"].ToString().Trim();
            //tmp9 = DNdt.Tables[0].Rows[v2]["END_TIME"].ToString().Trim();
            tmpsqlW = " insert into " + Wridir + ".TCWM01( DATES, LINE, WORKNO, ASSY_TARGET_QTY, ASSY_PART, PACKING_TARGET_QTY, "
                + " PACKING_PART, DOCUMENTID, UPDATE1, UPDATE2, UPDATE3, FLAG1, FLAG2, Class1 ) "
                + " values('" + SysDate + "','" + PROLINE + "',  '" + tmp1 + "','" + tmp0 + "','" + tmp0 + "', '" + tmp0 + "', '" + tmp0 + "', "
                + " '" + tDocumentID + "', '" + tDocumentID + "', '" + tmps + "', '" + tmps + "', '" + tmps + "', '" + tmps + "', '" + tmp5 + "') ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        }   // end for loop

        ///////////////////////////////////////////////////////////////////////
        // Update 參數 TCWM01 ASSY_TARGET_QTY, PACKING_TARGET_QTY from TCWD02
        sqlr = " select * from " + Wridir + ".TCWD02 where SEQ_NUMBER >= 2000 and SEQ_NUMBER <= 2999 ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if ((DNdt != null) && (DNdt.Tables[0].Rows.Count > 0))
        {
            DNCnt = DNdt.Tables[0].Rows.Count;
            for (v2 = 0; v2 < DNCnt; v2++)
            {
                tmp2 = "";
                tmpsqlW = "";
                tmp2 = DNdt.Tables[0].Rows[v2]["DATATYPE"].ToString().Trim();
                tmp4 = DNdt.Tables[0].Rows[v2]["ALL_QTY"].ToString().Trim();
                tmp5 = DNdt.Tables[0].Rows[v2]["Class1"].ToString().Trim();  // Class
                tmp6 = DNdt.Tables[0].Rows[v2]["BEGIN_TIME"].ToString().Trim();
                tmp7 = DNdt.Tables[0].Rows[v2]["END_TIME"].ToString().Trim();
                tmp8 = DNdt.Tables[0].Rows[v2]["PASS_QTY"].ToString().Trim();
                tmp15 = DNdt.Tables[0].Rows[v2]["TMP1"].ToString().Trim();  
                tmp16 = DNdt.Tables[0].Rows[v2]["TMP2"].ToString().Trim();  
                tmp17 = DNdt.Tables[0].Rows[v2]["TMP3"].ToString().Trim();  
                tmp18 = DNdt.Tables[0].Rows[v2]["TMP4"].ToString().Trim();  
                // if ( tmp2.ToUpper() == "ASSY" )
                //      tmpsqlW = " update " + Wridir + ".TCWM01 set ASSY_TARGET_QTY = '" + tmp4 + "' where DATES = '" + SysDate + "' "
                //               + " and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                // if ( tmp2.ToUpper() == "PACKING")
                //      tmpsqlW = " update " + Wridir + ".TCWM01 set PACKING_TARGET_QTY = '" + tmp4 + "' where DATES = '" + SysDate + "' "
                //               + " and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";

                switch ( tmp2 )
                {
                    case "ASSYTARGET":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set ASSY_TARGET_QTY = '" + tmp4 + "'  "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "PACKINGTARGET":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set PACKING_TARGET_QTY = '" + tmp4 + "'  "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "SHIFTTARGET":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set SHIFTTARGET = '" + tmp4 + "'  "   // 20131116, ACCUMTARGET = '" + tmp4 + "'  "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "ACCUMTARGET":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set ACCUMTARGET = '" + tmp4 + "' " // SHIFTTARGET = '" + tmp4 + "', ACCUMTARGET = '" + tmp4 + "'  "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "YRTARGET":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set YRTARGET = '" + tmp4 + "' "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "WORKTIME":   // tmp8 為時間為 10 分為切割單位
                        tmp9 = SysDate + tmp6.Substring(8, 4);
                        tmp10 = SysDate + tmp7.Substring(8, 4);
                        tmpsqlW = " update " + Wridir + ".TCWM01 set TIMESLICE = '" + tmp8 + "', WORKTOTMIN = '" + tmp4 + "', BEGIN_TIME =  '" + tmp9 + "', "
                        + " END_TIME = '" + tmp10 + "' where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "UPH":  // UPH 每1小時數量 / 6 = 每 10 分數量  UPHH
                        tmpsqlW = " update " + Wridir + ".TCWM01 set UPH = '" + tmp4 + "', UPHH = '" + tmp8 + "' "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  "; // tmp8 = UPH/6 
                        break;

                    case "LINE":  // UPH 每1小時數量 / 6 = 每 10 分數量  UPHH
                        tmpsqlW = " update " + Wridir + ".TCWM01 set ENOLINE = '" + tmp15 + "', ATOLINE = '" + tmp16 + "' "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  "; // ENO, ATO, LINE 
                        break;

                    case "WORKITEM":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set ITEM = '" + tmp4 + "' "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "REMARK":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set REMARK = '" + tmp4 + "' "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;

                    case "WORKNO":
                        tmpsqlW = " update " + Wridir + ".TCWM01 set WORKNO = '" + tmp4 + "' "
                        + " where DATES = '" + SysDate + "' and  LINE = '" + PROLINE + "' and Class1 = '" + tmp5 + "'  ";
                        break;


                    default:

                        break;
                }
              
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            }   // end for loop
        }  // End TCWM01 Write file 



        //////////////////////////////////////////
        // 取的時間總數, 分隔時, UPH 等值
        //DataSet DNdt = null;
        //string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", Wridir = "PUBLIB", sqlr = "";
        tmp1 = "WORKTIME";
        tmp2 = "UPH";
        string tmpWORKTOTMIN = "", tmpTIMESLICE = "", tmpUPH = "", tmpUPHh = "";
        string tmpENOLINE="", tmpATOLINE="";
        sqlr = " select *  from " + Wridir + ".TCWM01  where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' and Class1 = '" + tmpClass1 + "' ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if ((DNdt != null) && ( DNdt.Tables[0].Rows.Count > 0 ) )
        {  
             DNCnt = DNdt.Tables[0].Rows.Count;
             tmpWORKTOTMIN = DNdt.Tables[0].Rows[0]["WORKTOTMIN"].ToString().Trim();   // 全部時間
             tmpTIMESLICE  = DNdt.Tables[0].Rows[0]["TIMESLICE"].ToString().Trim();  // 分隔時間
             tmpUPH = DNdt.Tables[0].Rows[0]["UPH"].ToString().Trim();   // UPH 數量時間 
             tmpUPHh = DNdt.Tables[0].Rows[0]["UPHH"].ToString().Trim();      // UPH 數量時間 / 10   
             tmpENOLINE = DNdt.Tables[0].Rows[0]["ENOLINE"].ToString().Trim();   
             tmpATOLINE = DNdt.Tables[0].Rows[0]["ATOLINE"].ToString().Trim();      
        }
        
    //int DNCnt = 0, v1 = 0, v2 = 0, v3 = 0;
    //    sqlr = " select *  from " + Wridir + ".TCWD02  where DATATYPE = '" + tmp1 + "' and LINE = '" + PROLINE + "' and Class1 = '" + tmpClass1 + "' ";
    //    DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
    //    if ((DNdt != null) && ( DNdt.Tables[0].Rows.Count > 0 ) )
    //    {  
    //         DNCnt = DNdt.Tables[0].Rows.Count;
    //         tmpWORKTOTMIN = DNdt.Tables[0].Rows[0]["ALL_QTY"].ToString().Trim();   // 全部時間
    //         tmpTIMESLICE  = DNdt.Tables[0].Rows[0]["PASS_QTY"].ToString().Trim();  // 分隔時間
    //    }
    //
    //    sqlr = " select *  from " + Wridir + ".TCWD02  where DATATYPE = '" + tmp2 + "' and LINE = '" + PROLINE + "' and Class1 = '" + tmpClass1 + "' ";
    //    DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
    //    if ((DNdt != null) && (DNdt.Tables[0].Rows.Count > 0))
    //    {
    //        DNCnt  = DNdt.Tables[0].Rows.Count;
    //        tmpUPH = DNdt.Tables[0].Rows[0]["ALL_QTY"].ToString().Trim();   // UPH 數量時間 
    //        tmpUPHh = DNdt.Tables[0].Rows[0]["PASS_QTY"].ToString().Trim();      // UPH 數量時間 / 10     
    //    }
        ////////////////////////////////////////
        // Start Write TCWD01 time slice by 10
        if (tmpTIMESLICE != "") timeslice = tmpTIMESLICE;
            if ( timeslice != "") daycnt = Convert.ToInt32(timeslice); // 時間差 10 分 1 次
            v1 = 24 * 60 / daycnt;
            tmp2 = "ENO";
            for ( v2 = 0; v2 < v1; v2 ++ ) // 一次全產生完畢 Detail
            {
                tmp3 = (v2 + 1001).ToString();
                v4 = v2 * 10;
                v5 = v4 / 60;
                v6 = v4 % 60;
                tmp4 = v5.ToString(); 
                tmp5 = v6.ToString();
              
                v4 = ( v2 + 1 ) * 10;
                v5 = v4 / 60;
                v6 = v4 % 60;
                tmp6 = v5.ToString();
                tmp7 = v6.ToString();
               
                if ((tmp4 == "0") || (tmp4 == "") || (tmp4 == " 0") || (tmp4 == "0 ")) tmp4 = "00";
                if ((tmp5 == "0") || (tmp5 == "") || (tmp5 == " 0") || (tmp5 == "0 ")) tmp5 = "00";
                if ((tmp6 == "0") || (tmp6 == "") || (tmp6 == " 0") || (tmp6 == "0 ")) tmp6 = "00";
                if ((tmp7 == "0") || (tmp7 == "") || (tmp7 == " 0") || (tmp7 == "0 ")) tmp7 = "00";

                if (tmp4.Length == 1) tmp4 = "0" + tmp4; // 補 "0" 
                if (tmp5.Length == 1) tmp5 = "0" + tmp5; // 補 "0" 
                if (tmp6.Length == 1) tmp6 = "0" + tmp6; // 補 "0" 
                if (tmp7.Length == 1) tmp7 = "0" + tmp7; // 補 "0" 

                tmp8 = SysDate + tmp4 + tmp5;
                tmp9 = SysDate + tmp6 + tmp7;
                tmp10 = tmp9.Substring(8, 4);

                // 壁開 2400 -- 2359
                if (tmp8.Substring(8, 4) == "2400") // tmp6.Substring(8, 4);
                    tmp8 = tmp8.Substring(0, 8) + "2359";
                if (tmp9.Substring(8, 4) == "2400") 
                    tmp9 = tmp9.Substring(0, 8) + "2359";

                tmp2 = "ENO";
                tmpsqlW = " insert into " + Wridir + ".TCWD01( DATES, LINE, DATATYPE, STATION, BEGIN_TIME, END_TIME, PASS_QTY,  "
                + " ALL_QTY, OPCODE, SEQ_NUMBER,DOCUMENTID, FLAG1, FLAG2, FLAG3, TMP1 ) "
                + " values('" + SysDate + "','" + PROLINE + "',  '" + tmp2 + "','" + tmps + "','" + tmp8 + "', '" + tmp9 + "', '" + tmp0 + "', "
                + "  '" + tmpUPHh + "', 'I', '" + tmp3 + "', '" + tDocumentID + "', '" + tmps + "', '" + tmps + "', '" + tmps + "', '" + tmpENOLINE + "' ) ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                tmp2 = "ATO";
                tmpsqlW = " insert into " + Wridir + ".TCWD01( DATES, LINE, DATATYPE, STATION, BEGIN_TIME, END_TIME, PASS_QTY,  "
                + " ALL_QTY, OPCODE, SEQ_NUMBER,DOCUMENTID, FLAG1, FLAG2, FLAG3, TMP1 ) "
                + " values('" + SysDate + "','" + PROLINE + "',  '" + tmp2 + "','" + tmps + "','" + tmp8 + "', '" + tmp9 + "', '" + tmp0 + "', "
                + "  '" + tmpUPHh + "', 'I', '" + tmp3 + "', '" + tDocumentID + "', '" + tmps + "', '" + tmps + "', '" + tmps + "', '" + tmpATOLINE + "' ) ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            } 


            ////////////////////////////////
            // Insert TCWD01  YIELDRATE
            tmp2 = "YIELDRATE";
            sqlr = " select *  from " + Wridir + ".TCWD02 where DATATYPE = '" + tmp2 + "' ";
            // sqlr = " select *  from " + Wridir + ".TCWD02 where DATATYPE = '" + tmp2 + "' and SEQ_NUMBER >= 1000 and SEQ_NUMBER <= 1999 ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) return ("0");  // if not data then insert

            for (v2 = 0; v2 < DNCnt; v2++)
            {
                tmp3 = (v2 + 2001).ToString();
                tmp2 = DNdt.Tables[0].Rows[v2]["DATATYPE"].ToString().Trim();
                tmp1 = DNdt.Tables[0].Rows[v2]["STATION"].ToString().Trim();
                tmp4 = DNdt.Tables[0].Rows[v2]["ALL_QTY"].ToString().Trim();
                tmp5 = DNdt.Tables[0].Rows[v2]["Class1"].ToString().Trim();  // Class
                tmp8 = DNdt.Tables[0].Rows[v2]["BEGIN_TIME"].ToString().Trim();
                tmp9 = DNdt.Tables[0].Rows[v2]["END_TIME"].ToString().Trim();
                tmp8 = SysDate + tmp8.Substring(8, 4);
                tmp9 = SysDate + tmp9.Substring(8, 4);
                tmpsqlW = " insert into " + Wridir + ".TCWD01( DATES, LINE, DATATYPE, STATION, BEGIN_TIME, END_TIME, PASS_QTY,  "
                + " ALL_QTY, OPCODE, SEQ_NUMBER,DOCUMENTID, FLAG1, FLAG2, FLAG3, Class1 ) "
                + " values('" + SysDate + "','" + PROLINE + "',  '" + tmp2 + "','" + tmp1 + "','" + tmp8 + "', '" + tmp9 + "', '" + tmp0 + "', "
                + "  '" + tmp4 + "', 'I', '" + tmp3 + "', '" + tDocumentID + "', '" + tmps + "', '" + tmps + "', '" + tmps + "', '" + tmp5 + "' ) ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            }   // end for loop
            

        return ("0");

    }   //  end  CreECCM1   

    ////////////////////////////////////////
    // Update ENO, ASSy
    public string UpdateTCWM01(string BSite, string Dtype, string SysDate, string PROLINE, string DBType, string DBReadString, string DBWriString, string PCode, string tmpSEQ_NUMBER, string tmpDATATYPE, string tmpClass1)
    {

        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 10, DNCnt = 0, dt1Cnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "", SN1 = "";
        string tmps = "", tmp0 = "", tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "", tmp8, tmp9;
        string tmp10 = "", tmp11 = "", tmp12 = "", tmp13 = "", tmp14 = "", tmp15 = "", tmp16 = "", tmp17 = "", tmp18, tmp19;
        string tmp20 = "", tmp21 = "", tmp22 = "", tmp23 = "", tmp24 = "", tmp25 = "", tmp26 = "", tmp27 = "", tmp28, tmp29;
        string tmp30 = "", tmp31 = "", tmp32 = "", tmp33 = "", tmp34 = "", tmp35 = "", tmp36 = "", tmp37 = "", tmp38, tmp39;
        string tmp40 = "", tmp41 = "", tmp42 = "", tmp43 = "", tmp44 = "", tmp45 = "", tmp46 = "", tmp47 = "", tmp48, tmp49;

        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "MSA", Wridir = "MSA";
        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv5.ToUpper() != "") Wridir = pv5;
        else Wridir = "PUBLIB";

        tmpDATATYPE = "ENO";
        // Clear SET flag1 is space initial
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG1 = '' where ( FLAG1 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        sqlr = " select *  from " + Wridir + ".TCWD01  where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' "
             + " and SEQ_NUMBER = '" + tmpSEQ_NUMBER + "' and DATATYPE = '" + tmpDATATYPE + "'   ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("-1");  // if not data then return

        tmp1 = DNdt.Tables[0].Rows[0]["BEGIN_TIME"].ToString().Trim();
        tmp2 = DNdt.Tables[0].Rows[0]["END_TIME"].ToString().Trim();   // 目前比較時間

        sqlr = " select *  from " + Wridir + ".TCWM01 where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' and Class1 = '" + tmpClass1 + "' ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("-1");  // if not data then return

        tmp3 = DNdt.Tables[0].Rows[0]["BEGIN_TIME"].ToString().Trim();
        tmp4 = DNdt.Tables[0].Rows[0]["END_TIME"].ToString().Trim();
        tmp5 = DNdt.Tables[0].Rows[0]["WORKTOTMIN"].ToString().Trim(); 
        // 20131116tmp6 = DNdt.Tables[0].Rows[0]["SHIFTTARGET"].ToString().Trim();
        tmp6 = DNdt.Tables[0].Rows[0]["ACCUMTARGET"].ToString().Trim();

        if ( ( Convert.ToDecimal(tmp2) < Convert.ToDecimal(tmp3) ) || ( Convert.ToDecimal(tmp2) > Convert.ToDecimal(tmp4) )  )
               return("0");

        tmp8 = tmp2.Substring(10, 2);
        v1 = Convert.ToInt32(tmp2.Substring(8, 2)) - Convert.ToInt32(tmp3.Substring(8, 2));  // Hr
        v2 = Convert.ToInt32(tmp2.Substring(10, 2)) - Convert.ToInt32(tmp3.Substring(10, 2));  // Minute
        v3 = v1 * 60 + v2; // currency  total minute
        v4 = Convert.ToInt32(tmp6) * v3 / Convert.ToInt32(tmp5);
        tmp7 = v4.ToString();

        tmpsqlW = " Update " + Wridir + ".TCWM01 set ACCUMRUN  = '" + tmp7 + "'   "
        + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' and Class1 = '" + tmpClass1 + "' ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        tmpsqlW = " Update " + Wridir + ".TCWM01 set "
           + " ASSYDIFF    =  to_number( ASSY_TOT_QTY )    - to_number( ACCUMRUN ), "
           + " PACKINGDIFF =  to_number( PACKING_TOT_QTY ) - to_number( ACCUMRUN )  "
           + " where DATES = '" + SysDate + "' and LINE = '" + PROLINE + "' and Class1 = '" + tmpClass1 + "' ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        return ("0");

    }  // end UpdateTCWM01
   
    
// end New Program 20131010

}  // end public class EKanbanlib

}  // end namespace SFC.TJWEB


