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
//using SCM.GSCMDKen;
using SFC.TJWEB;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using System.Linq;
using System.Web.UI;
using System.Data.Odbc;
using Microsoft.Adapter.SAP; 



namespace SFC.TJWEB
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class BBSCMlib
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
    ShipPlanlib              ShipPlanlibPointer = new ShipPlanlib();

    string DBType = "oracle";
    static int PredayQty = 10;
    static int Gdaycnt = 2;
    static string tmpType = "";
    
    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");


    //include two ways:
    // 1.typec='00'--verify
    // 2.typec='01'--create
    //
    //--verify--
    //select po from create_mt where confirmflag='Y' and (userconfirmflag='' or userconfirmflag='E')--find po which is not sent to SAP
    //select * from create_dt where po--select basic data about po
    //select * from POShipToAddress where po--select basic data about po
    //select * from POSoldToAddress where po--select basic data about po
    //call sap to verify,meanwhile write the reback data in create_mt.SAP_LOG
    //
    //--create--
    //select po from create_mt where confirmflag='Y' and userconfirmflag='V'--find po which has been verify but no create
    //select * from create_dt where po--select basic data about po
    //select * from POShipToAddress where po--select basic data about po
    //select * from POSoldToAddress where po--select basic data about po
    //call sap to create,meanwhile write the reback data in create_mt.SAP_LOG
    public string CallSapAndCreateSO(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string typec, string poid, string spocnt,string MSSCMDIR)
    {
        string mess = "", strmt = "", strmt1 = "", strmt2 = "";

        //select PO_Create_mt沒有createSO資料
        if (typec == "00")
        {
            strmt1 = "SELECT * FROM " + MSSCMDIR + ".[dbo].[PO_CREATE_MT] " +
                    " where CONFIRMFLAG ='Y' and (USERCONFIRMFLAG is null or USERCONFIRMFLAG ='' or USERCONFIRMFLAG ='E') ";//and CREATIONDT>'2014-10-07T10:00:47.055Z'";
            strmt2 = " union " +
                    " SELECT * FROM " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] " +
                    " where CONFIRMFLAG ='Y' and (USERCONFIRMFLAG is null or USERCONFIRMFLAG ='' or USERCONFIRMFLAG ='E') ";// and CREATIONDT>'2014-10-07T10:00:47.055Z'";
            if (poid != "")
            {
                strmt1 = strmt1 + " and POID='" + poid + "'";
                strmt2 = strmt2 + " and POID='" + poid + "'";
            }
            if (spocnt != "")
            {
                strmt1 = strmt1 + " and POCNT='" + spocnt + "'";
                strmt2 = strmt2 + " and POCNT='" + spocnt + "'";
            }
            strmt = strmt1 + strmt2 + " order by CREATIONDT ";
            //strmt = "SELECT * FROM [BBSCM].[dbo].[PO_CREATE_MT] where POID='20000250' ";
        }

        else if (typec == "01")
        {
            if (spocnt != "")
            {
                int sspocnt = Int32.Parse(spocnt);
                if (sspocnt == 1)
                {
                    strmt = "SELECT * FROM " + MSSCMDIR + ".[dbo].[PO_CREATE_MT] where CONFIRMFLAG ='Y' and USERCONFIRMFLAG ='V' and POID='" + poid + "' and POCNT='" + spocnt + "' order by CREATIONDT ";
                    //strmt = "SELECT * FROM [BBSCM].[dbo].[PO_CREATE_MT] where POID='20000000' ";
                }
                else if (sspocnt > 1)
                {
                    strmt = "SELECT * FROM " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] where CONFIRMFLAG ='Y' and USERCONFIRMFLAG ='V' and POID='" + poid + "' and POCNT='" + spocnt + "' order by CREATIONDT ";

                    ////if po change,檢查之前的po creat和po change 有沒有做過Verify和Create
                    //string checkstr =  "SELECT * FROM [BBSCM].[dbo].[PO_CREATE_MT] " +
                    //                   " where POID='" + poid + "' and (CONFIRMFLAG is null or CONFIRMFLAG ='' or CONFIRMFLAG ='Y') " +
                    //                   " union " +
                    //                   " SELECT * FROM [BBSCM].[dbo].[PO_CHANGE_MT] " +
                    //                   " where POID='" + poid + "' and (CONFIRMFLAG is null or CONFIRMFLAG ='' or CONFIRMFLAG ='Y') ";
                    //DataTable checkdt = new DataTable();
                    //checkdt = PDataBaseOperation.PSelectSQLDT(DbSql, DBReadString, checkstr);
                    //if (checkdt.Rows.Count>0)
                    //{
                    //    for (int rcount = 0; rcount < checkdt.Rows.Count; rcount++)
                    //    {
                    //        string sapflag = checkdt.Rows[rcount]["USERCONFIRMFLAG"].ToString().Trim();
                    //        if (sapflag == "Y")
                    //        {

                    //        }
                    //    }
                    //}
                }
            }
        }
        DataTable dtmt = new DataTable();
        dtmt = PDataBaseOperation.PSelectSQLDT(DbSql, DBReadString, strmt);
        if (dtmt.Rows.Count > 0)
        {
            for (int mtcount = 0; mtcount < dtmt.Rows.Count; mtcount++)
            {
                string id = dtmt.Rows[mtcount]["ID"].ToString().Trim();
                poid = dtmt.Rows[mtcount]["POID"].ToString().Trim();
                string pocnt = dtmt.Rows[mtcount]["POCNT"].ToString().Trim();

                //select PO_Create_dt相關資料
                string strdt = "";
                if (pocnt == "1")
                {
                    strdt = "select * from " + MSSCMDIR + ".[dbo].[PO_CREATE_DT] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                }
                else
                {
                    strdt = "select * from " + MSSCMDIR + ".[dbo].[PO_CHANGE_DT] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                }
                DataTable dtdt = new DataTable();
                dtdt = PDataBaseOperation.PSelectSQLDT(DbSql, DBReadString, strdt);
                if (dtdt.Rows.Count > 0)
                {
                    //call sap interface
                    //SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh");
                    SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=Foxconn8;LANG=zh");
                    con.Open();
                    SAPCommand cmd = new SAPCommand(con);
                    cmd.CommandText = "EXEC ZRFC_SD_BBRY_INTERFACE   @INPUT_HEADER=@INPUT_HEADERV OUTPUT, @INPUT_ITEM=@INPUT_ITEMV OUTPUT, @INPUT_PATNER = @INPUT_PATNERV OUTPUT, @VBELN=@VBELNV OUTPUT, @OUTPUT_MSG=@OUT_MSGV OUTPUT ";

                    //input table1:INPUT_HEATER
                    SAPParameter INPUT_HEADERV = new SAPParameter("@INPUT_HEADERV", ParameterDirection.InputOutput);
                    DataTable dtheader = new DataTable();
                    dtheader.Columns.Add("VBELN");
                    dtheader.Columns.Add("BSTKD");
                    dtheader.Columns.Add("CSHIP");
                    dtheader.Columns.Add("CSOLD");
                    dtheader.Columns.Add("BSTDK");
                    dtheader.Columns.Add("WERKS");
                    dtheader.Columns.Add("AUART");
                    dtheader.Columns.Add("VKORG");
                    dtheader.Columns.Add("VTWEG");
                    dtheader.Columns.Add("TYPEC");
                    dtheader.Columns.Add("KUNNR");
                    dtheader.Columns.Add("SHIP_TYPE");
                    DataRow rowheader = dtheader.NewRow();
                    //rowheader["VBELN"] = dtmt.Rows[mtcount]["SAP_LOG"].ToString().Trim();
                    rowheader["VBELN"] = "";
                    rowheader["BSTKD"] = poid;
                    if (Int32.Parse(pocnt) == 1)
                    {
                        rowheader["CSHIP"] = dtdt.Rows[0]["PO_CREATE_DT_UF3"].ToString().Trim();
                    }
                    else
                    {
                        rowheader["CSHIP"] = dtdt.Rows[0]["PO_CHANGE_DT_UF3"].ToString().Trim();
                    }
                    rowheader["CSOLD"] = dtdt.Rows[0]["PRODUCTRECIPIENTPARTYID"].ToString().Trim();
                    rowheader["BSTDK"] = dtmt.Rows[mtcount]["ReceiveTime"].ToString().Trim().Substring(0, 8);
                    rowheader["WERKS"] = "";
                    rowheader["AUART"] = "";
                    rowheader["VKORG"] = "";
                    rowheader["VTWEG"] = "";
                    rowheader["TYPEC"] = typec;
                    rowheader["KUNNR"] = "";
                    rowheader["SHIP_TYPE"] = "01";
                    dtheader.Rows.Add(rowheader);
                    INPUT_HEADERV.Value = dtheader;
                    cmd.Parameters.Add(INPUT_HEADERV);

                    //input table2:INPUT_ITEM
                    SAPParameter INPUT_ITEMV = new SAPParameter("@INPUT_ITEMV", ParameterDirection.InputOutput);
                    DataTable dtitem = new DataTable();
                    dtitem.Columns.Add("BSTKD");
                    dtitem.Columns.Add("POSEX");
                    dtitem.Columns.Add("MATNR");
                    dtitem.Columns.Add("MAKTX");
                    dtitem.Columns.Add("KDMAT");
                    dtitem.Columns.Add("VBELN");
                    dtitem.Columns.Add("POSNR");
                    dtitem.Columns.Add("KWMENG");
                    dtitem.Columns.Add("WERKS");
                    dtitem.Columns.Add("WAERK");
                    dtitem.Columns.Add("NETPR");
                    dtitem.Columns.Add("KPEIN");
                    dtitem.Columns.Add("LGORT");
                    dtitem.Columns.Add("VRKME");
                    dtitem.Columns.Add("VSTEL");
                    dtitem.Columns.Add("EDATU");

                    for (int dtcount = 0; dtcount < dtdt.Rows.Count; dtcount++)
                    {
                        string itemid = dtdt.Rows[dtcount]["ITEMID"].ToString().Trim();
                        string dtuf3 = "";
                        if (Int32.Parse(pocnt) == 1)
                        {
                            dtuf3 = dtdt.Rows[dtcount]["PO_CREATE_DT_UF3"].ToString().Trim();
                        }
                        else
                        {
                            dtuf3 = dtdt.Rows[dtcount]["PO_CHANGE_DT_UF3"].ToString().Trim();
                        }
                        string dtpcpid = dtdt.Rows[dtcount]["PRODUCTRECIPIENTPARTYID"].ToString().Trim();
                        DataRow rowitem = dtitem.NewRow();
                        rowitem["BSTKD"] = poid;
                        rowitem["POSEX"] = "0000" + itemid;
                        rowitem["MATNR"] = "";
                        rowitem["MAKTX"] = "";
                        rowitem["KDMAT"] = dtdt.Rows[dtcount]["INTERNALID"].ToString().Trim();
                        rowitem["VBELN"] = "";
                        rowitem["POSNR"] = "000010";
                        rowitem["KWMENG"] = dtdt.Rows[dtcount]["SCHEDULEQUANTITY"].ToString().Trim();
                        rowitem["WERKS"] = "";
                        rowitem["WAERK"] = dtdt.Rows[dtcount]["Currency"].ToString().Trim();
                        rowitem["NETPR"] = dtdt.Rows[dtcount]["AMOUNT"].ToString().Trim();
                        rowitem["KPEIN"] = dtdt.Rows[dtcount]["BASEQTY"].ToString().Trim();
                        rowitem["LGORT"] = "";
                        rowitem["VRKME"] = dtdt.Rows[dtcount]["Unit"].ToString().Trim();
                        rowitem["VSTEL"] = "";
                        rowitem["EDATU"] = "";
                        dtitem.Rows.Add(rowitem);
                    }
                    INPUT_ITEMV.Value = dtitem;
                    cmd.Parameters.Add(INPUT_ITEMV);

                    //若為Po change,要找到其對應的Po create的ID
                    string pochangeid = "";
                    if (pocnt != "1")
                    {
                        string findid = "select ID from " + MSSCMDIR + ".[dbo].[PO_CREATE_MT] where POID='" + poid + "'";
                        DataTable dtid = new DataTable();
                        dtid = PDataBaseOperation.PSelectSQLDT(DbSql, DBReadString, findid);
                        if (dtid.Rows.Count > 0)
                        {
                            pochangeid = id;
                            id = dtid.Rows[0]["ID"].ToString().Trim();
                        }
                    }
                    string strship = "select * from " + MSSCMDIR + ".[dbo].[POShipToAddress] where ID='" + id + "'";
                    DataTable dtship = new DataTable();
                    dtship = PDataBaseOperation.PSelectSQLDT(DbSql, DBReadString, strship);
                    //select POSoldToAddress相關資料
                    string strsold = "select * from " + MSSCMDIR + ".[dbo].[POSoldToAddress] where ID='" + id + "'";
                    DataTable dtsold = new DataTable();
                    dtsold = PDataBaseOperation.PSelectSQLDT(DbSql, DBReadString, strsold);

                    if (dtship.Rows.Count == 0 || dtsold.Rows.Count == 0)
                    {
                        mess = "PO地址資料不齊全，創建SO失敗！";

                        string failme = "update " + MSSCMDIR + ".[dbo].[PO_CREATE_MT] set UPLOAD_SAP='E',SAP_LOG='" + mess + "',USERCONFIRMFLAG ='E'"
                                         + " where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                        int idetfail = PDataBaseOperation.PExecSQL(DbSql, DBReadString, failme);
                        continue;
                    }

                    //input table3:INPUT_PATNER
                    SAPParameter INPUT_PATNERV = new SAPParameter("@INPUT_PATNERV", ParameterDirection.InputOutput);
                    DataTable dtpatner = new DataTable();
                    dtpatner.Columns.Add("BSTKD");
                    dtpatner.Columns.Add("PARVW");
                    dtpatner.Columns.Add("KUNNR");
                    dtpatner.Columns.Add("CSHIP");
                    dtpatner.Columns.Add("NAME1");
                    dtpatner.Columns.Add("STREET");
                    dtpatner.Columns.Add("STR_SUPPL3");
                    dtpatner.Columns.Add("LOCATION");
                    dtpatner.Columns.Add("CITY1");
                    dtpatner.Columns.Add("LAND1");
                    dtpatner.Columns.Add("POST_CODE1");
                    DataRow rowpatner1 = dtpatner.NewRow();
                    rowpatner1["BSTKD"] = dtmt.Rows[mtcount]["POID"].ToString().Trim();
                    rowpatner1["PARVW"] = "WE";
                    rowpatner1["KUNNR"] = "";
                    if (pocnt == "1")
                    {
                        rowpatner1["CSHIP"] = dtdt.Rows[0]["PO_CREATE_DT_UF3"].ToString().Trim();
                    }
                    else
                    {
                        rowpatner1["CSHIP"] = dtdt.Rows[0]["PO_CHANGE_DT_UF3"].ToString().Trim();
                    }
                    rowpatner1["NAME1"] = dtship.Rows[0]["GivenName"].ToString().Trim();
                    rowpatner1["STREET"] = dtship.Rows[0]["StreetName"].ToString().Trim();
                    rowpatner1["STR_SUPPL3"] = "";
                    rowpatner1["LOCATION"] = "";
                    rowpatner1["CITY1"] = dtship.Rows[0]["CityName"].ToString().Trim();
                    rowpatner1["LAND1"] = dtship.Rows[0]["CountryCode"].ToString().Trim();
                    rowpatner1["POST_CODE1"] = dtship.Rows[0]["PostalCode"].ToString().Trim();
                    dtpatner.Rows.Add(rowpatner1);

                    DataRow rowpatner2 = dtpatner.NewRow();
                    rowpatner2["BSTKD"] = dtmt.Rows[mtcount]["POID"].ToString().Trim();
                    rowpatner2["PARVW"] = "AG";
                    rowpatner2["KUNNR"] = "";
                    rowpatner2["CSHIP"] = dtdt.Rows[0]["PRODUCTRECIPIENTPARTYID"].ToString().Trim();
                    rowpatner2["NAME1"] = dtsold.Rows[0]["GivenName"].ToString().Trim();
                    rowpatner2["STREET"] = dtsold.Rows[0]["StreetName"].ToString().Trim();
                    rowpatner2["STR_SUPPL3"] = "";
                    rowpatner2["LOCATION"] = "";
                    rowpatner2["CITY1"] = dtsold.Rows[0]["CityName"].ToString().Trim();
                    rowpatner2["LAND1"] = dtsold.Rows[0]["CountryCode"].ToString().Trim();
                    rowpatner2["POST_CODE1"] = dtsold.Rows[0]["PostalCode"].ToString().Trim();
                    dtpatner.Rows.Add(rowpatner2);
                    INPUT_PATNERV.Value = dtpatner;
                    cmd.Parameters.Add(INPUT_PATNERV);

                    //output param:VBELN--SO
                    SAPParameter VBELNV = new SAPParameter("@VBELNV", ParameterDirection.Output);
                    cmd.Parameters.Add(VBELNV);

                    //output table1:OUT_MSG--fail information
                    SAPParameter OUT_MSGV = new SAPParameter("@OUT_MSGV", ParameterDirection.InputOutput);
                    cmd.Parameters.Add(OUT_MSGV);

                    SAPDataReader dr = cmd.ExecuteReader();
                    string SAPparam1 = (string)cmd.Parameters["@VBELNV"].Value;
                    DataTable SAPdt1 = (DataTable)cmd.Parameters["@OUT_MSGV"].Value;

                    string updso = "", updme = "";
                    int idetso = 0, idetme = 0;

                    string nowtime = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    string tablename = "";
                    if (pocnt == "1")
                    {
                        tablename = "" + MSSCMDIR + ".[dbo].[PO_CREATE_MT]";
                    }
                    else
                    {
                        tablename = "" + MSSCMDIR + ".[dbo].[PO_CHANGE_MT]";
                        id = pochangeid;
                    }
                    //00--verify
                    if (typec == "00")
                    {
                        string flag = "V", mes = "";
                        if (SAPparam1.Trim() == "" && SAPdt1.Rows.Count > 0)
                        {
                            for (int i = 0; i < SAPdt1.Rows.Count; i++)
                            {
                                string type = SAPdt1.Rows[i]["TYPE"].ToString().Trim();

                                if (type == "E")
                                {
                                    flag = "E";
                                    mes = SAPdt1.Rows[i]["MESSAGE"].ToString().Trim();
                                    break;
                                }
                            }
                        }
                        mes = nowtime + mes;
                        updme = "update " + tablename + " set UPLOAD_SAP='" + flag + "',SAP_LOG='" + mes + "',USERCONFIRMFLAG ='" + flag + "'"
                                         + " where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                        idetme = PDataBaseOperation.PExecSQL(DbSql, DBReadString, updme);
                        if (flag == "V")
                        {
                            mess = "Verify is successful!";
                        }
                        else
                        {
                            mess = "Verify is failed";
                        }
                    }

                    //01--create SO
                    else if (typec == "01")
                    {
                        string flag = "Y", mes = "";
                        if (SAPparam1.Trim() != "")
                        {
                            mes = "_" + SAPparam1.Trim();
                        }
                        else
                        {
                            for (int i = 0; i < SAPdt1.Rows.Count; i++)
                            {
                                string type = SAPdt1.Rows[i]["TYPE"].ToString().Trim();

                                if (type == "E")
                                {
                                    flag = "E";
                                    mes = SAPdt1.Rows[i]["MESSAGE"].ToString().Trim();
                                    break;
                                }
                            }
                        }
                        mes = nowtime + mes;
                        updme = "update " + tablename + " set UPLOAD_SAP='" + flag + "',SAP_LOG='" + mes + "',USERCONFIRMFLAG ='" + flag + "'"
                                         + " where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                        idetme = PDataBaseOperation.PExecSQL(DbSql, DBReadString, updme);
                        if (flag == "Y")
                        {
                            mess = "CreateSO is successful!";
                        }
                        else
                        {
                            mess = "CreateSO is failed";
                        }
                    }
                }
            }
        }
        else
        {
            mess = "沒有confirm,請confirm后再Create SO!";
        }
        return mess;
    }


    // string t1 = BBSCMlibPointer.Pass_Upfile("1", DbSql, SqlWebDBA, SqlWebDBA, "menu", tmpdate, cmd, "SCM-LABEL-M1", "");
    public string Pass_Upfile(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string Cmd1, string DATATYPE, string tmpDocumentID)
    {

        if (DATATYPE != "SCM-LABEL-M1") return ("");

        string tmpDN = "", tmpIP = "";

        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, v8 = 0, v9 = 0;
        int DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);


        sqlr = "  SELECT  * from  [BBSCM].[dbo].jbomm1 where (  Osversion = '" + DATATYPE + "' ) order by ITEM, PART asc ";
        DataSet BOMdt = PDataBaseOperation.PSelectSQLDS(DbSql, DBReadString, sqlr);
        if (BOMdt == null) return ("Table not found in Jbomm1"); // Syn Error
        if (BOMdt.Tables.Count <= 0) return (" Table not found in Jbomm1");
        int BOMCnt1 = BOMdt.Tables[0].Rows.Count;
        if (BOMCnt1 == 0) return ("No Data 0");     // Not Data

        v3 = 30;
        string[,] arrayBOM = new string[BOMCnt1 + 1, v3 + 1];
        for (v1 = 0; v1 <= BOMCnt1; v1++)
            for (v2 = 0; v2 <= v3; v2++)
                arrayBOM[v1, v2] = "";

        // Put im tmp array
        for (v1 = 0; v1 < BOMCnt1; v1++)
        {
            arrayBOM[v1 + 1, 0] = (v1 + 1).ToString();
            arrayBOM[v1 + 1, 1] = BOMdt.Tables[0].Rows[v1]["OSVersion"].ToString().Trim();
            arrayBOM[v1 + 1, 2] = BOMdt.Tables[0].Rows[v1]["ITEM"].ToString().Trim();
            arrayBOM[v1 + 1, 3] = BOMdt.Tables[0].Rows[v1]["PART"].ToString().Trim();
            arrayBOM[v1 + 1, 4] = BOMdt.Tables[0].Rows[v1]["MARK"].ToString().Trim(); // N: Can not space, Y:OK
            arrayBOM[v1 + 1, 5] = BOMdt.Tables[0].Rows[v1]["NOTE"].ToString().Trim(); // string, Char
            arrayBOM[v1 + 1, 10] = "N";  // Switch

        }


        ///////////////////////////////////////////////////////////////////////////////
        // 從第一位置次抓取 BOM 進/出, 
        // 第一欄位取後, 抓 BOM (進)位置後, (取)出出位置
        // 放在BOM 後面依出位置, 放入如第 4 位置, 放在 V3 + 4 
        // 如此就是寫入得順序 , BOM 加長 2倍, 原為進, 後為出    
        string DBType = DbSql;

        if (Cmd1 == "2") // documentid
            sqlr = "  SELECT  * from  [BBSCM].[dbo].FUPLD1 where (  FLAG1 = '' or  FLAG1 = 'N' or FLAG1 is null ) and DOCUMENTID  = '" + tmpDocumentID + "' order by DocumentID desc ";
        else
            sqlr = "  SELECT  * from  [BBSCM].[dbo].FUPLD1 where (  FLAG1 = '' or  FLAG1 = 'N' or FLAG1 is null ) order by DocumentID desc ";

        DataSet UPDdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (UPDdt == null) return ("Table not found in DBA"); // Syn Error
        int UPDCnt1 = UPDdt.Tables[0].Rows.Count;
        if (UPDCnt1 == 0) return ("No Data 0");     // Not Data

        int UPDCnt2 = UPDdt.Tables[0].Columns.Count;

        ///////////////////////////////////////////////////////////////////
        // Algorithm:
        // 1. 將 UPD File 依照每列讀取, 並依照每個欄位順序放矩陣 arrayUPD X 軸, arrayULD 為加強 2倍加 10.
        // 2. 每放一次 X 軸, 依  X 軸第幾位置, 如第 1為置, 讀取 jbomm1 中 ITEM = "1"
        // 3. 讀到後 PART 即為放置為置, 如 2 就放在 arrauUPD 中, 第 2 個長度為置
        //    


        int UPDXLong = 0;
        v3 = UPDCnt2 + 10;
        UPDXLong = UPDCnt2 + 10;

        string[,] arrayUPD = new string[UPDCnt1 + 1, UPDXLong + UPDXLong + 1]; // 設計 2 倍, 後面放真鄭回寫 File 的值 
        for (v1 = 0; v1 <= UPDCnt1; v1++)
            for (v2 = 0; v2 <= v3; v2++)
                arrayUPD[v1, v2] = "";

        // Put im tmp array
        string RefSpace = "", RefNewLoc = "", RefFieldType = "";

        for (v1 = 0; v1 < UPDCnt1; v1++)  // y Ray
        {
            for (v2 = 0; v2 < UPDCnt2; v2++)  // X Ray 
            {
                t10 = UPDdt.Tables[0].Rows[v1][v2].ToString().Trim(); // Check Only Data-Value
                arrayUPD[v1 + 1, v2 + 1] = UPDdt.Tables[0].Rows[v1][v2].ToString().Trim(); //  UPDdt.Rows[v1][v2].ToString();

                arrayUPD[v1 + 1, UPDXLong - 1] = "Y";  // Switch Pass
                RefSpace = ""; RefNewLoc = ""; RefFieldType = "";
                t01 = getfileloc(v2 + 1, BOMCnt1, arrayBOM, ref RefSpace, ref RefNewLoc, ref RefFieldType); // X-Ray Loc, BOM Cnt, BOM array

                if (t01 != "") arrayUPD[v1 + 1, UPDXLong + Convert.ToInt32(t01)] = t10;

                if ((RefSpace.ToUpper() == "N") && (t10 == "")) // 定義 RefSpace == "N" ==> 資料不為空 
                    arrayUPD[v1 + 1, UPDXLong - 1] = "N";      // Switch Returen 

            }

            if ((arrayUPD[v1 + 1, 1].ToString() == "") || (arrayUPD[v1 + 1, 2].ToString() == ""))
                arrayUPD[v1 + 1, UPDXLong - 1] = "N"; // Switch Returen


            //arrayUPD[v1 + 1, 0] = (v1 + 1).ToString();
            //arrayUPD[v1 + 1, 1] = UPDdt.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            //arrayUPD[v1 + 1, 2] = UPDdt.Tables[0].Rows[v1]["Seqno"].ToString().Trim();
            //arrayUPD[v1 + 1, 3] = UPDdt.Tables[0].Rows[v1]["TIME"].ToString().Trim();
            //arrayUPD[v1 + 1, 4] = UPDdt.Tables[0].Rows[v1]["Users"].ToString().Trim();
            //arrayUPD[v1 + 1, 5] = UPDdt.Tables[0].Rows[v1]["Seqno"].ToString().Trim();
            //arrayUPD[v1 + 1, 6] = UPDdt.Tables[0].Rows[v1]["DATATYPE"].ToString().Trim();
            //arrayUPD[v1 + 1, 7] = UPDdt.Tables[0].Rows[v1]["FLAG1"].ToString().Trim();
            //arrayUPD[v1 + 1, 8] = UPDdt.Tables[0].Rows[v1]["FLAG2"].ToString().Trim();
            //arrayUPD[v1 + 1, 9] = UPDdt.Tables[0].Rows[v1]["FLAG3"].ToString().Trim();
            //arrayUPD[v1 + 1, 10] = ""; // Switch Returen
            //
            //arrayUPD[v1 + 1, 11] = UPDdt.Tables[0].Rows[v1]["D01"].ToString().Trim();
            //arrayUPD[v1 + 1, 12] = UPDdt.Tables[0].Rows[v1]["D02"].ToString().Trim();
            //arrayUPD[v1 + 1, 13] = UPDdt.Tables[0].Rows[v1]["D03"].ToString().Trim();
            //arrayUPD[v1 + 1, 14] = UPDdt.Tables[0].Rows[v1]["D04"].ToString().Trim();
            //arrayUPD[v1 + 1, 15] = UPDdt.Tables[0].Rows[v1]["D05"].ToString().Trim();
            //arrayUPD[v1 + 1, 16] = UPDdt.Tables[0].Rows[v1]["D06"].ToString().Trim();
            //arrayUPD[v1 + 1, 17] = UPDdt.Tables[0].Rows[v1]["D07"].ToString().Trim();
            //arrayUPD[v1 + 1, 18] = UPDdt.Tables[0].Rows[v1]["D08"].ToString().Trim();
            //arrayUPD[v1 + 1, 19] = UPDdt.Tables[0].Rows[v1]["D09"].ToString().Trim();

        }




        DataSet Newdt = null, UPDatedt = null;
        int Newv1 = 0, Newv2 = 0;
        string Wridir = "[BBSCM].[dbo]";
        string UPDFLAG1 = "F";
        v5 = 0; v6 = 0;
        for (v1 = 0; v1 < UPDCnt1; v1++)
        {
            t01 = arrayUPD[v1 + 1, 1].ToString().Trim();  // Documentid
            t02 = arrayUPD[v1 + 1, 2].ToString().Trim();  // Seqno
            UPDFLAG1 = "F";
            if (arrayUPD[v1 + 1, UPDXLong - 1] == "Y")
            {

                sqlr = "  SELECT  * from  [BBSCM].[dbo].FDLABM1 where (  PRODUCTBUYERID = '" + arrayUPD[v1 + 1, 15] + "' ) and  (  RODUCTRECIPIENTPARTYID = '" + arrayUPD[v1 + 1, 16] + "' )  "; // Prd+SOLD-TO
                Newdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
                if (Newdt == null) return ("Table not found in Jbomm1"); // Syn Error
                if (Newdt.Tables.Count <= 0) return ("Table not found in Jbomm1");
                Newv1 = Newdt.Tables[0].Rows.Count;
                if (Newv1 == 0)  // Can Insert
                {
                    // PRD + SOLD-TO, 
                    sqlr = " insert into " + Wridir + ".FDLABM1( PRODUCTBUYERID, RODUCTRECIPIENTPARTYID, INTERNALID, POSLABEL, CartonLabel, CountryCode, GivenName, DELIVERYSTARTDT, "
                    + " CDATE     ) "
                    + " values('" + arrayUPD[v1 + 1, UPDXLong + 1] + "','" + arrayUPD[v1 + 1, UPDXLong + 2] + "',  '" + arrayUPD[v1 + 1, UPDXLong + 3] + "', "
                    + " '" + arrayUPD[v1 + 1, UPDXLong + 4] + "', '" + arrayUPD[v1 + 1, UPDXLong + 5] + "', '" + arrayUPD[v1 + 1, UPDXLong + 6] + "',  "
                    + " '" + arrayUPD[v1 + 1, UPDXLong + 7] + "', '" + CuurDate + "', '" + CuurDate + "' ) ";
                    v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);
                    if (v4 > 0) // Insert Succedd  Update FUPDLM1 FLAG1 = "Y"
                    {
                        UPDFLAG1 = "Y";
                        v5++;
                    }
                    else
                    {
                        UPDFLAG1 = "E";
                        v6++;
                    }


                    //t01 = arrayUPD[v1 + 1, 1].ToString().Trim();  // Documentid
                    //t02 = arrayUPD[v1 + 1, 2].ToString().Trim();  // Seqno
                    //
                    //sqlr = "update " + Wridir + ".FUPLD1 set FLAG1 = '" + UPDFLAG1 + "' where DocumentID = '" + t01 + "' and Seqno = '" + t02 + "' ";
                    //v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

                } // if (Newv1 == 0)
                else
                {
                    UPDFLAG1 = "E";
                    v6++;
                }

            }   // if (arrayUPD[v1 + 1, UPDXLong - 1] == "Y")
            else
            {
                UPDFLAG1 = "E";
                v6++;
            }   // if (arrayUPD[v1 + 1, UPDXLong - 1] == "Y")



            //arrayUPD[v1 + 1, 1] = UPDdt.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            //arrayUPD[v1 + 1, 2] = UPDdt.Tables[0].Rows[v1]["Seqno"].ToString().Trim();

            sqlr = "update " + Wridir + ".FUPLD1 set FLAG1 = '" + UPDFLAG1 + "' where DocumentID = '" + t01 + "' and Seqno = '" + t02 + "' ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

        }  //  for (v1 = 0; v1 < UPDCnt1; v1++)

        t01 = "Success : " + v5.ToString() + " , " + "Fail : " + v6.ToString() + " ,";

        return (t01);


    }


    ///////////////////////////////////////////////////////////////////////////////
    // 從第一位置次抓取 BOM 進/出, 
    // 第一欄位取後, 抓 BOM (進)位置後, (取)出出位置
    // 放在BOM 後面依出位置, 放入如第 4 位置, 放在 V3 + 4 
    // 如此就是寫入得順序 , BOM 加長 2倍, 原為進, 後為出   
    // OutPut 為後面矩陣位置 
    private string getfileloc(int OrgXloc, int BOMCnt1, string[,] arrayBOM, ref string RefSpace, ref string RefNewLoc, ref string RefFieldType)
    {
        int v1 = 0, v2 = 0;
        string t1 = "", t2 = "";

        for (v1 = 0; v1 < BOMCnt1; v1++)
        {

            t2 = arrayBOM[v1 + 1, 2]; // Array[i, 2] = BIM.ITEM, Array[i,3] = BOM.PART
            if ((t2 != "") && (OrgXloc == Convert.ToInt32(arrayBOM[v1 + 1, 2])))  // 第幾個位置  f1/f2/f3/f4 , [x,2]  if f1 = x get [x,3] 
            {
                t1 = arrayBOM[v1 + 1, 3]; // UPD Out Location from Second Long
                RefSpace = arrayBOM[v1 + 1, 4].ToString(); //  = BOMdt.Tables[0].Rows[v1]["MARK"].ToString().Trim(); // N: Can not space, Y:OK
                RefFieldType = arrayBOM[v1 + 1, 5].ToString(); //  = BOMdt.Tables[0].Rows[v1]["NOTE"].ToString().Trim(); // string, Char
                v1 = BOMCnt1; // Break
            }

        }

        RefNewLoc = t1;

        return (t1);

    }


    public string AutoGetPackingFromSFC(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string MSSCMDIR)
    {
        string tmpDN = "";
       
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, v8 = 0, v9 = 0;
        int DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        ///////////////////////////////////////////////////////////////////////////////
        // if ( PO_CREATE_MT CONFIRMATIONFLAG <> "Y" )  CHeck By this ID
        // {
        //      if ( ID in PO_CREATE_DT == "Y" ) and PO_CONFIRMATION == "Y" )
        //      if ( ID in PO_CREATE_DT QTY == SUM PO_CONFIRMATION ) 
        //      PO_CONFIRMATIONFLAG == "Y"
        // }
        string DBType = DbSql;

        // 20140424 sqlr = "  SELECT  * from Delivery_Notification_MT where (  SFCFlag <> 'Y' or SFCFlag is null ) order by ID desc ";
        if ( Systype == "DN" )
            sqlr = "  SELECT  * from  " + MSSCMDIR + ".[dbo].Delivery_Notification_MT where (  SFCFlag = '' or  SFCFlag = 'N' or SFCFlag is null ) and DNID  = '" + Actiontype + "' ";
        else
            sqlr = "  SELECT  * from  " + MSSCMDIR + ".[dbo].Delivery_Notification_MT where (  SFCFlag = '' or  SFCFlag = 'N' or SFCFlag is null ) order by ID desc ";
        
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        int DNCnt1 = DNdt.Tables[0].Rows.Count;
        if (DNCnt1 == 0) return ("0");     // Not Data

        string[,] arrayPO_CREATE_MT = new string[DNCnt1 + 1, 15 + 1];
        for (v1 = 0; v1 <= DNCnt1; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayPO_CREATE_MT[v1, v2] = "";

        // Put im tmp array
        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            arrayPO_CREATE_MT[v1 + 1, 0] = (v1 + 1).ToString();
            arrayPO_CREATE_MT[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["CreationDT"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["DNID"].ToString().Trim();
            // arrayPO_CREATE_MT[v1 + 1, 3] = "80168556"; //  "80168537";  // Test Only
            arrayPO_CREATE_MT[v1 + 1, 4] = "0";
            arrayPO_CREATE_MT[v1 + 1, 5] = "0";
            arrayPO_CREATE_MT[v1 + 1, 6] = "0";
            arrayPO_CREATE_MT[v1 + 1, 7] = "0";
            arrayPO_CREATE_MT[v1 + 1, 8] = "N";
            tmpDN = arrayPO_CREATE_MT[v1 + 1, 3];
        }



        // Check in  FROM [BBSCM].[dbo].[Delivery_Notification_HU], where DNID = '80168556' if not exist then call 
        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            tmpDN = arrayPO_CREATE_MT[v1 + 1, 3];
            tmp1 = "N";
            v2 = 1;
            v3 = 1;
            sqlr = "  SELECT  COUNT(*) as HUQTY from  " + MSSCMDIR + ".[dbo].Delivery_Notification_HU where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("Delivery_Notification_HU ERROR");
            if (DNdt.Tables.Count > 0) v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["HUQTY"].ToString()); // tmpds.Tables[0].Rows.Count;

            sqlr = "  SELECT  COUNT(*) as DTQTY from  " + MSSCMDIR + ".[dbo].Delivery_Notification_DT where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("Delivery_Notification_DT ERROR");
            if (DNdt.Tables.Count > 0) v3 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["DTQTY"].ToString()); // tmpds.Tables[0].Rows.Count;

            if ( ( v2 == 0 ) && ( v3 == 0) )  // No SFC Coming Data you can running
            {
                // Clear Data in temp
                sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Notification_HU_TEMP where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
                v4  = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
                sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Notification_DT_TEMP where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
                sqlr = "  Delete   " + MSSCMDIR + ".[dbo].Delivery_Map_DN          where DNID = '" + arrayPO_CREATE_MT[v1 + 1, 3] + "' ";
                v6 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
                sqlr = "  truncate table  " + MSSCMDIR + ".[dbo].Delivery_Map_DN   ";
                v7 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr); 

                // tmpDN = arrayPO_CREATE_MT[v1 + 1, 3];
                // 20140504 
                // tmp2 = GetPackingFromSFCTOBUFByDN(Systype, DbSql, DBReadString, DBWriString, Actiontype, SysDate, tmpIP, tmpDN);
                tmp2 = GetPackingFromSFCTOBUFByDN_V01(Systype, DbSql, DBReadString, DBWriString, Actiontype, SysDate, tmpIP, tmpDN, MSSCMDIR);
                if (tmp2 == "")
                    tmp3 = CheckDNQTYAndInsert(Systype, DbSql, DBReadString, DBWriString, Actiontype, SysDate, tmpIP, tmpDN,MSSCMDIR); // 20140422

                

                //  tmp2 = GetPackingFromBUFTODBAByDN(Systype, DbSql, DBReadString, DBWriString, Actiontype, SysDate, tmpIP, tmpDN);
            }  // endif
                  // endif
        }

       

        return ("");

        // ww.GetPackingFromSFCTOBUFByDN("", "", Session["Param3"].ToString(), Session["Param3"].ToString(), "", "", "", DN);
  

    }
    public string ReturnSendFlagToSFCByDN(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string tmpDN, string MSSCMDIR)
    {
        string strsendDN = "select DNID from " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] where SendFlag='Y' " +
                           "and (Delivery_Notification_MT_UF1='' or Delivery_Notification_MT_UF1 is null) " +
                           "and SendTime>'2015-03-25 00:00:00'";
        DataTable dtsendDN = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, strsendDN);
        if (!dtsendDN.Rows.Count.Equals(0))
        {
            ws.BBRYWebService wsbblbnew = new ws.BBRYWebService();

            if (tmpIP == "")
                //bbsc.Url = "http://10.83.216.137/bbryreport/webservice/bbryservice.asmx";
                wsbblbnew.Url = "http://10.74.14.48/report/webservice/bbryservice.asmx";
            else
                wsbblbnew.Url = tmpIP;
            for (int i = 0; i < dtsendDN.Rows.Count; i++)
            {
                string DNID = dtsendDN.Rows[i]["DNID"].ToString().Trim();
                string str = wsbblbnew.ExecuteConfirmSendDataByDNN("BBRYDNN", "BBRYDNN", DNID);
                string updateflag = "update " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] set Delivery_Notification_MT_UF1='Y' where DNID='" + DNID + "'";
                int idetupt = PDataBaseOperation.PExecSQL("SQL", DBReadString, updateflag);
            }
        }
        else
        {
            return ("Not found DN in Delivery_Notification_MT");
        }
        return ("");
    }

    // 邢秀明
    public string CheckDNQTYAndInsert(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string tmpDN, string MSSCMDIR)
    {
        string emess = "";
        string sqlDNDT = "SELECT SerialID FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";//20140904 distinct SerialID-->SerialID for SerialID may be null
        DataTable dtDNDT = new DataTable();
        dtDNDT = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, sqlDNDT);
        string sqlDNHU = "SELECT COUNT(*) as BoxQTY FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU_TEMP] where DNID='" + tmpDN + "'";
        DataTable dtDNHU = new DataTable();
        dtDNHU = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, sqlDNHU);
        string sqlDNMT = "SELECT BoxQTY,SerialIDQTY FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] where DNID='" + tmpDN + "'";
        DataTable dtDNMT = new DataTable();
        dtDNMT = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, sqlDNMT);

        //20141031 serialid all null or all have value
        string sqlserialid = "SELECT distinct SerialID FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";//20140904 distinct SerialID-->SerialID for SerialID may be null
        DataTable dtserialid = new DataTable();
        dtserialid = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, sqlserialid);

        if ((dtDNDT.Rows.Count > 0) && (dtDNMT.Rows.Count > 0) && (dtDNHU.Rows.Count > 0) && (dtDNHU.Rows.Count > 0))
        {
            int sfcidqty = Int32.Parse(dtDNDT.Rows.Count.ToString());
            int sapidqty = Int32.Parse(dtDNMT.Rows[0]["SerialIDQTY"].ToString());
            int sfcboxqty = Int32.Parse(dtDNHU.Rows[0]["BoxQTY"].ToString());
            int sapboxqty = Int32.Parse(dtDNMT.Rows[0]["BoxQTY"].ToString());

            if (sfcboxqty != sapboxqty)
            {
                emess = "The sfcboxqty is" + sfcboxqty;
                string checkflag2 = "update " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] set Delivery_Notification_MT_UF10='" + emess + "' where DNID='" + tmpDN + "'";
                int idetc2 = PDataBaseOperation.PExecSQL("SQL", DBReadString, checkflag2);
                return (emess);
            }

            if (sapidqty != dtserialid.Rows.Count && dtserialid.Rows.Count != 1)
            {
                string checkflag1 = "update " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] set Delivery_Notification_MT_UF10='" + dtserialid.Rows.Count + "' where DNID='" + tmpDN + "'";
                int idetc1 = PDataBaseOperation.PExecSQL("SQL", DBReadString, checkflag1);
                return (emess);
            }

            ////20140816--Add module to check the repeat SerialID in DN_DT
            //for (int i = 0; i < dtDNDT.Rows.Count; i++)
            //{
            //    string serialid = dtDNDT.Rows[i]["SerialID"].ToString();

            //    if (serialid.ToString().Trim() != "")//20140904 add for serialid may be null
            //    {
            //        string chectdt = "select * from [BBSCM].[dbo].[Delivery_Notification_DT] where SerialID='" + serialid + "' and (Delivery_Notification_DT_UF1='' or Delivery_Notification_DT_UF1 is null)";
            //        DataTable dtchectdt = new DataTable();
            //        dtchectdt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, chectdt);
            //        if (dtchectdt.Rows.Count > 0)
            //        {
            //            string mes = dtDNDT.Rows[0]["DNID"].ToString() + "_" + dtDNDT.Rows[0]["SERIALID"].ToString();
            //            string checkflag = "update [BBSCM].[dbo].[Delivery_Notification_MT] set Delivery_Notification_MT_UF10='" + mes + "' where DNID='" + tmpDN + "'";
            //            int idetc = PDataBaseOperation.PExecSQL("SQL", DBReadString, checkflag);
            //            return (emess);
            //        }
            //    }
            //}
            if ((sfcidqty == sapidqty) && (sfcboxqty == sapboxqty))
            {
                string updateDNDT = "insert into " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT]([ID],[DNID],[ItemID],[BoxID],[LoadInternalID]"
                         + ",[LoadProductRecipientID],[TypeCode],[CompletedIndicator],[QAUnit],[QA],[SerialID],[POID],[POTypeCode]"
                         + ",[POItemID],[POItemTypeCode],[InternalID],[ProductRecipientID],[UserStart],[CONFIRMFLAG],[HUID])"
                         + "select [ID],[DNID],[ItemID],[BoxID],[LoadInternalID],[LoadProductRecipientID],[TypeCode]"
                         + ",[CompletedIndicator],[QAUnit],[QA],[SerialID],[POID],[POTypeCode],[POItemID],[POItemTypeCode],"
                         + "[InternalID],[ProductRecipientID],[UserStart],[CONFIRMFLAG],[HUID]"
                         + "from " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";
                int idet = PDataBaseOperation.PExecSQL("SQL", DBReadString, updateDNDT);
                string updateDNHU = "insert into " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU]([ID],[DNID],[HUID],[BoxID],[ItemID],"
                                  + "[LoadInternalID],[LoadProductRecipientID],[LoadQAUnit],[LoadQA],[UserStart],[CONFIRMFLAG],[Delivery_Notification_HU_UF1])"
                                  + "select [ID],[DNID],[HUID],[BoxID],[ItemID],[LoadInternalID],[LoadProductRecipientID],"
                                  + "[LoadQAUnit],[LoadQA],[UserStart],[CONFIRMFLAG],[Delivery_Notification_HU_UF1]"
                                  + " FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU_TEMP] where DNID='" + tmpDN + "'";
                int idet1 = PDataBaseOperation.PExecSQL("SQL", DBReadString, updateDNHU);
                if ((idet == sapidqty) && (idet1 == sapboxqty))
                {

                    //20140912 --抓SFC資料成功，set DN_MT issueDT=Now.DateTime
                    string issuedt = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    string sfcflag = "update " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] set SFCFlag='Y',IssueDT='" + issuedt + "',Delivery_Notification_MT_UF10=''  where DNID='" + tmpDN + "'";
                    int idet5 = PDataBaseOperation.PExecSQL("SQL", DBReadString, sfcflag);

                    return (emess);
                }
            }
        }
        
        //////////////////////////////////////////////////////////
        // tmp no delete for trace 20140502
        // string delDTtemp = "delete from [BBSCM].[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";
        // int idet2 = PDataBaseOperation.PExecSQL("SQL", DBReadString, delDTtemp);
        // string delHUtemp = "delete from [BBSCM].[dbo].[Delivery_Notification_HU_TEMP] where DNID='" + tmpDN + "'";
        // int idet3 = PDataBaseOperation.PExecSQL("SQL", DBReadString, delHUtemp);

        return (emess);
    }
    //1.SELECT POID ,count(*),MAX(ReceiveTime) FROM [BBSCM].[dbo].[PO_CHANGE_MT] where CONFIRMFLAG <>'Y' or CONFIRMFLAG is null group by POID
    //2.start loop
    //        if(po_create confirmflag='Y')
    //     {
    //         po_create保留，update pochange confirmflag='C'，且保留最後一筆PO_change
    //     }
    //        else
    //     {
    //         update pocreate confirmflag='C'
    //         if(pochange中有confirmflag='Y'的資料，保留此筆)
    //       {
    //           pochange中保留最後一筆，其餘Confirmflag='C'
    //       }
    //         else   
    //       {
    //           loop pochange set confirmflag='C',保留最後一筆.
    //       }
    //     }
    // end loop
    //3.move 正式表中confirmflag='C'的資料到Backup(因發貨地址原因，只移pochange)
    //4.start loop(check data before delete)
    //        if(backup中有此筆資料) delete正式表中資料
    //  end loop

    public string AutoChk_POValue(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string MSSCMDIR)
    {
        string mess = "";
        string strpo = "SELECT POID ,count(*),MAX(ReceiveTime) FROM " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] where CONFIRMFLAG <>'Y' or CONFIRMFLAG is null group by POID";
        DataTable dtpo = new DataTable();
        dtpo = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, strpo);
        if (dtpo.Rows.Count > 0)
        {

            for (int i = 0; i < dtpo.Rows.Count; i++)
            {
                string poid = "";
                poid = dtpo.Rows[i]["POID"].ToString().Trim();
                //poid = "20000010";

                //update PO_CREATE_MT,PO_CREATE_DT CONFIRMFLAG='C'
                string strmtflag = "select * from " + MSSCMDIR + ".[dbo].[PO_CREATE_MT] where POID='" + poid + "' and CONFIRMFLAG='Y'";
                DataTable dtpomtflag = new DataTable();
                dtpomtflag = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, strmtflag);

                //如果ConfirmFlag='Y',跳到下一筆。
                //if (dtpomtflag.Rows.Count > 0)
                //{
                //    string uppocmt = "update [BBSCM].[dbo].[PO_CHANGE_MT] set CONFIRMFLAG='C' where POID='" + poid + "' ";
                //    int idetc1 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppocmt);
                //    string uppocdt = "update [BBSCM].[dbo].[PO_CHANGE_DT] set CONFIRMFLAG='C' where POID='" + poid + "' ";
                //    int idetc2 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppocdt);
                //}

                if (dtpomtflag.Rows.Count == 0)
                {
                    string uppomt = "update " + MSSCMDIR + ".[dbo].[PO_CREATE_MT] set CONFIRMFLAG='C' where POID='" + poid + "'";
                    int idet1 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppomt);
                    string uppodt = "update " + MSSCMDIR + ".[dbo].[PO_CREATE_DT] set CONFIRMFLAG='C' where POID='" + poid + "'";
                    int idet2 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppodt);
                }

                //update PO_CHANGE_MT,PO_CHANGE_DT CONFIRMFLAG='C'
                string strpochange = "SELECT * FROM " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] where POID='" + poid + "' order by ReceiveTime+POCNT asc";
                DataTable dtpochange = new DataTable();
                dtpochange = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, strpochange);
                if (dtpochange.Rows.Count > 1)
                {
                    int count = dtpochange.Rows.Count;
                    for (int j = 0; j < dtpochange.Rows.Count - 1; j++)
                    {
                        string id = dtpochange.Rows[j]["ID"].ToString().Trim();
                        string pocnt = dtpochange.Rows[j]["POCNT"].ToString().Trim();
                        string confirmflag = dtpochange.Rows[j]["ConfirmFlag"].ToString().Trim();
                        if (confirmflag == "Y")
                        {
                            //string uppcmt1 = "update [BBSCM].[dbo].[PO_CHANGE_MT] set CONFIRMFLAG='C' where POID='" + poid + "' and (CONFIRMFLAG<>'Y' or CONFIRMFLAG is null)";
                            //int idet5 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppcmt1);
                            //string uppcdt1 = "update [BBSCM].[dbo].[PO_CHANGE_DT] set CONFIRMFLAG='C' where POID='" + poid + "' and (CONFIRMFLAG<>'Y' or CONFIRMFLAG is null)";
                            //int idet6 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppcdt1);
                            //continue;
                        }
                        else
                        {
                            string uppcmt = "update " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] set CONFIRMFLAG='C' where POID='" + poid + "' and ID='" + id + "' and POCNT='" + pocnt + "'";
                            int idet3 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppcmt);
                            string uppcdt = "update " + MSSCMDIR + ".[dbo].[PO_CHANGE_DT] set CONFIRMFLAG='C' where POID='" + poid + "' and ID='" + id + "' and POCNT='" + pocnt + "'";
                            int idet4 = PDataBaseOperation.PExecSQL("SQL", DBReadString, uppcdt);
                        }
                    }
                }
            }
        }

        //move ConfirmFlag='C'的資料到BackUp表中
        //string movepomt = "insert into [BBSCM].[dbo].[PO_CREATE_MT_BK] select * from [BBSCM].[dbo].[PO_CREATE_MT] where ConfirmFlag ='C'";
        //int idetpmt = PDataBaseOperation.PExecSQL("SQL", DBReadString, movepomt);
        //string movepodt = "insert into [BBSCM].[dbo].[PO_CREATE_DT_BK] select * from [BBSCM].[dbo].[PO_CREATE_DT] where ConfirmFlag ='C'";
        //int idetpdt = PDataBaseOperation.PExecSQL("SQL", DBReadString, movepodt);
        string movepocmt = "insert into " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT_BK] select * from " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] where ConfirmFlag ='C'";
        int idetcmt = PDataBaseOperation.PExecSQL("SQL", DBReadString, movepocmt);
        string movepocdt = "insert into " + MSSCMDIR + ".[dbo].[PO_CHANGE_DT_BK] select * from " + MSSCMDIR + ".[dbo].[PO_CHANGE_DT] where ConfirmFlag ='C'";
        int idetcdt = PDataBaseOperation.PExecSQL("SQL", DBReadString, movepocdt);

        //check 后delete正式表
        //delete [BBSCM].[dbo].[PO_CREATE_MT]
        //string checkpomt = "select * from [BBSCM].[dbo].[PO_CREATE_MT] where CONFIRMFLAG='C'";
        //DataTable dtckpomt = new DataTable();
        //dtckpomt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, checkpomt);
        //if (dtckpomt.Rows.Count > 0)
        //{
        //    for(int pm=0;pm<dtckpomt.Rows.Count;pm++)
        //    {
        //        string id = dtckpomt.Rows[pm]["ID"].ToString().Trim();
        //        string poid = dtckpomt.Rows[pm]["POID"].ToString().Trim();
        //        string pocnt = dtckpomt.Rows[pm]["POCNT"].ToString().Trim();
        //        string ckpomtbk = "select * from [BBSCM].[dbo].[PO_CREATE_MT_BK] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
        //        DataTable ckpomt = new DataTable();
        //        ckpomt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, ckpomtbk);
        //        if (ckpomt.Rows.Count == 1)
        //        {
        //            string delpomt = "delete from [BBSCM].[dbo].[PO_CREATE_MT] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
        //            int idelmt = PDataBaseOperation.PExecSQL("SQL", DBReadString, delpomt);
        //        }
        //    }
        //}

        //delete [BBSCM].[dbo].[PO_CREATE_DT]
        //string checkpodt = "select * from [BBSCM].[dbo].[PO_CREATE_DT] where CONFIRMFLAG='C'";
        //DataTable dtckpodt = new DataTable();
        //dtckpodt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, checkpodt);
        //if (dtckpodt.Rows.Count > 0)
        //{
        //    for (int pm = 0; pm < dtckpodt.Rows.Count; pm++)
        //    {
        //        string id = dtckpodt.Rows[pm]["ID"].ToString().Trim();
        //        string poid = dtckpodt.Rows[pm]["POID"].ToString().Trim();
        //        string pocnt = dtckpodt.Rows[pm]["POCNT"].ToString().Trim();
        //        string ckpodtbk = "select * from [BBSCM].[dbo].[PO_CREATE_DT_BK] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
        //        DataTable ckpodt = new DataTable();
        //        ckpodt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, ckpodtbk);
        //        if (ckpodt.Rows.Count == 1)
        //        {
        //            string delpomt = "delete from [BBSCM].[dbo].[PO_CREATE_DT] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
        //            int idelmt = PDataBaseOperation.PExecSQL("SQL", DBReadString, delpomt);
        //        }
        //    }
        //}

        //delete [BBSCM].[dbo].[PO_CHANGE_MT]
        string checkpocmt = "select * from " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] where CONFIRMFLAG='C'";
        DataTable dtckpocmt = new DataTable();
        dtckpocmt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, checkpocmt);
        if (dtckpocmt.Rows.Count > 0)
        {
            for (int pm = 0; pm < dtckpocmt.Rows.Count; pm++)
            {
                string id = dtckpocmt.Rows[pm]["ID"].ToString().Trim();
                string poid = dtckpocmt.Rows[pm]["POID"].ToString().Trim();
                string pocnt = dtckpocmt.Rows[pm]["POCNT"].ToString().Trim();
                string ckpodtbk = "select * from " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT_BK] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                DataTable ckpodt = new DataTable();
                ckpodt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, ckpodtbk);
                if (ckpodt.Rows.Count == 1)
                {
                    string delpomt = "delete from " + MSSCMDIR + ".[dbo].[PO_CHANGE_MT] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                    int idelmt = PDataBaseOperation.PExecSQL("SQL", DBReadString, delpomt);
                }
            }
        }

        //delete [BBSCM].[dbo].[PO_CHANGE_DT]
        string checkpocdt = "select * from " + MSSCMDIR + ".[dbo].[PO_CHANGE_DT] where CONFIRMFLAG='C'";
        DataTable dtckpocdt = new DataTable();
        dtckpocdt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, checkpocdt);
        if (dtckpocdt.Rows.Count > 0)
        {
            for (int pm = 0; pm < dtckpocdt.Rows.Count; pm++)
            {
                string id = dtckpocdt.Rows[pm]["ID"].ToString().Trim();
                string poid = dtckpocdt.Rows[pm]["POID"].ToString().Trim();
                string pocnt = dtckpocdt.Rows[pm]["POCNT"].ToString().Trim();
                string ckpodtbk = "select * from " + MSSCMDIR + ".[dbo].[PO_CHANGE_DT_BK] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                DataTable ckpodt = new DataTable();
                ckpodt = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, ckpodtbk);
                if (ckpodt.Rows.Count == 1)
                {
                    string delpomt = "delete from " + MSSCMDIR + ".[dbo].[PO_CHANGE_DT] where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
                    int idelmt = PDataBaseOperation.PExecSQL("SQL", DBReadString, delpomt);
                    mess = idelmt.ToString();
                }
            }
        }

        return mess;
    }

    public string tmpCheckDNQTYAndInsert(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string tmpDN,string MSSCMDIR)
    {
        string emess = "";
        string sqlDNDT = "SELECT COUNT(*) as SerialIDQTY FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";
        DataTable dtDNDT = new DataTable();
        dtDNDT = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, sqlDNDT);
        string sqlDNHU = "SELECT COUNT(*) as BoxQTY FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU_TEMP] where DNID='" + tmpDN + "'";
        DataTable dtDNHU = new DataTable();
        dtDNHU = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, sqlDNHU);
        string sqlDNMT = "SELECT BoxQTY,SerialIDQTY FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] where DNID='" + tmpDN + "'";
        DataTable dtDNMT = new DataTable();
        dtDNMT = PDataBaseOperation.PSelectSQLDT("SQL", DBReadString, sqlDNMT);
        if ((dtDNDT.Rows.Count > 0) && (dtDNMT.Rows.Count > 0) && (dtDNHU.Rows.Count > 0))
        {
            int sfcidqty = Int32.Parse(dtDNDT.Rows[0]["SerialIDQTY"].ToString());
            int sapidqty = Int32.Parse(dtDNMT.Rows[0]["SerialIDQTY"].ToString());
            int sfcboxqty = Int32.Parse(dtDNHU.Rows[0]["BoxQTY"].ToString());
            int sapboxqty = Int32.Parse(dtDNMT.Rows[0]["BoxQTY"].ToString());
            if ((sfcidqty != sapidqty) || (sfcboxqty != sapboxqty))
            {
                string delDTtemp1 = "delete from " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";
                int idet21 = PDataBaseOperation.PExecSQL("SQL", DBReadString, delDTtemp1);
                string delHUtemp1 = "delete from " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU_TEMP] where DNID='" + tmpDN + "'";
                int idet31 = PDataBaseOperation.PExecSQL("SQL", DBReadString, delHUtemp1);
                emess = "SAP和SFC數量不等!";
                return (emess);
            }
        }
        string updateDNDT = "insert into " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT]([ID],[DNID],[ItemID],[BoxID],[LoadInternalID]"
                         + ",[LoadProductRecipientID],[TypeCode],[CompletedIndicator],[QAUnit],[QA],[SerialID],[POID],[POTypeCode]"
                         + ",[POItemID],[POItemTypeCode],[InternalID],[ProductRecipientID],[UserStart],[CONFIRMFLAG],[HUID])"
                         + "select [ID],[DNID],[ItemID],[BoxID],[LoadInternalID],[LoadProductRecipientID],[TypeCode]"
                         + ",[CompletedIndicator],[QAUnit],[QA],[SerialID],[POID],[POTypeCode],[POItemID],[POItemTypeCode],"
                         + "[InternalID],[ProductRecipientID],[UserStart],[CONFIRMFLAG],[HUID]"
                         + "from " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";
        int idet = PDataBaseOperation.PExecSQL("SQL", DBReadString, updateDNDT);
        string updateDNHU = "insert into " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU]([ID],[DNID],[HUID],[BoxID],[ItemID],"
                          + "[LoadInternalID],[LoadProductRecipientID],[LoadQAUnit],[LoadQA],[UserStart],[CONFIRMFLAG])"
                          + "select [ID],[DNID],[HUID],[BoxID],[ItemID],[LoadInternalID],[LoadProductRecipientID],"
                          + "[LoadQAUnit],[LoadQA],[UserStart],[CONFIRMFLAG]"
                          + "FROM " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU_TEMP] where DNID='" + tmpDN + "'";
        int idet1 = PDataBaseOperation.PExecSQL("SQL", DBReadString, updateDNHU);
        if (idet > 0 && idet1 > 0)
        {
            string delDTtemp = "delete from " + MSSCMDIR + ".[dbo].[Delivery_Notification_DT_TEMP] where DNID='" + tmpDN + "'";
            int idet2 = PDataBaseOperation.PExecSQL("SQL", DBReadString, delDTtemp);
            string delHUtemp = "delete from " + MSSCMDIR + ".[dbo].[Delivery_Notification_HU_TEMP] where DNID='" + tmpDN + "'";
            int idet3 = PDataBaseOperation.PExecSQL("SQL", DBReadString, delHUtemp);

            string tmp = "update " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT]  set SFCFLAG = 'Y' where DNID='" + tmpDN + "'";
            int tmpvar = PDataBaseOperation.PExecSQL("SQL", DBReadString, tmp);
        }
        return (emess);
    }

  
    // 施勇         
    public string GetPackingFromBUFTODBAByDN(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string tmpDN)
    {
      
        return ("");
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0, v9 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        ///////////////////////////////////////////////////////////////////////////////
        // if ( PO_CREATE_MT CONFIRMATIONFLAG <> "Y" )  CHeck By this ID
        // {
        //      if ( ID in PO_CREATE_DT == "Y" ) and PO_CONFIRMATION == "Y" )
        //      if ( ID in PO_CREATE_DT QTY == SUM PO_CONFIRMATION ) 
        //      PO_CONFIRMATIONFLAG == "Y"
        // }
        string DBType = DbSql;

        sqlr = "  SELECT  * from Delivery_Notification_MT where (  SFCFlag <> 'Y' or SFCFlag is null ) order by DNID desc ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0");     // Not Data

        string[,] arrayPO_CREATE_MT = new string[DNCnt + 1, 15 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayPO_CREATE_MT[v1, v2] = "";

        return ("");
        // ww.GetPackingFromSFCTOBUFByDN("", "", Session["Param3"].ToString(), Session["Param3"].ToString(), "", "", "", DN);
    }
                 
    public string AutoGetDNfromSap(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string BBSCMDIR)
    {
        if (  SysDate == "" ) SysDate =  DateTime.Today.ToString("yyyyMMdd");
        
        string edate = SysDate, bdate = SysDate ;
        DateTime D1 = DateTime.Today;
        string datetype = ShipPlanlibPointer.TrsstringToDateTime(SysDate);
        bdate = (Convert.ToDateTime(datetype)).AddDays(-10).ToString("yyyyMMdd");
        string return_var = GetSapDN("", DBWriString, DBWriString, bdate, edate, BBSCMDIR);
        return ("");
        //  string w111 = ww.GetSapDN("", Session["Param3"].ToString(), Session["Param3"].ToString(), "20140321", "20140321");
    }

    ///////////////////////////////////
    //  20140502 By Ken
    //  1. Data Put In array
    //  2. Check data in array
    //  3. Write 
    public string GetPackingFromSFCTOBUFByDN_V01(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string tmpDN, string MSSCMDIR)
    {
        string sqlstr;
        string strconn = DBWriString;
        string t1 = "", t2 = "", t3 = "", t4="", t5="", t6="";
        int v1 = 0, v2 = 0, v3 = 0;


        // WebService地址：http://10.83.216.137/bbryreport/webservice/bbryservice.asmx?wsdl
        // WebService地址：http://10.74.14.48/report/webservice/bbryservice.asmx?wsdl--20141210
        // BBRYService.BBRYService bbsc = new BBRYService();

       string sysdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss_fffZ");

       sqlstr = "select * from  Delivery_Notification_MT where DNID='" + tmpDN + "'";  // 取得MT主表id 號 同步 DN 數據.
       DataTable dt_DN = PDataBaseOperation.PSelectSQLDT( DbSql, strconn, sqlstr);
       if (!dt_DN.Rows.Count.Equals(0))
       {
           sysdatetime = dt_DN.Rows[0]["ID"].ToString();
           t1 = dt_DN.Rows[0]["HUQTY"].ToString();
           t2 = dt_DN.Rows[0]["BoxQTY"].ToString();
           t3 = dt_DN.Rows[0]["SerialIDQTY"].ToString();
       }
       else
           return ("Not found DN in Delivery_Notification_MT");
       
       if ((t3 == "") || (t3 == "0")) return("DN = space");

       wsdlLib.BBRYService bbsc = new wsdlLib.BBRYService();

        if (tmpIP == "")
            //bbsc.Url = "http://10.83.216.137/bbryreport/webservice/bbryservice.asmx";
            bbsc.Url = "http://10.74.14.48/report/webservice/bbryservice.asmx";
        else
            bbsc.Url = tmpIP;
        //      bbsc.Url = tmpIP;
        string DNID;
        DNID = tmpDN;
        string str = bbsc.ExecuteGetDataByDNN("BBRYDNN", "BBRYDNN", DNID).Replace("TRUE^", "");
        if (str.Substring(0, 5) == "FALSE") 
        {
            if (str.Trim() != "FALSE^DNN is not exist!".Trim())
            {
                string strinfo = "update " + MSSCMDIR + ".[dbo].Delivery_Notification_MT set Delivery_Notification_MT_UF10='" + str.Replace("'", ",") + "' where DNID='" + tmpDN + "' ";
                int idetinfo = PDataBaseOperation.PExecSQL("SQL", strconn, strinfo);
            }
            return "FALSE"; 
        }


        str = str.Replace("[", "");
        str = str.Replace("]", "");
        str = str.Replace(",{", "{");
        str = str.Replace("{", "");
        str = str.Replace(@"""", "");
        str = str.Replace(@"\", "");
        str = str.Replace("DNID:", "");
        str = str.Replace("ITEMID:", "");
        str = str.Replace("BOXID:", "");
        str = str.Replace("QAUNIT:", "");
        str = str.Replace("LOADQA", "");
        str = str.Replace("QA:", "");
        str = str.Replace("LOADQAUNIT:", "");
        str = str.Replace("SERIALID:", "");
        str = str.Replace("HUID:", "");
        str = str.Replace("LOADINTERNALID:", "");
        str = str.Replace("LOADPRODUCTRECIPIENTID:", "");
        str = str.Replace("TAGID:", "");
        str = str.Replace("LOAD", "");
        str = str.Replace("null", "");
        str = str.Replace(@"""", "");
        str = str.Replace(@":", "");


        //DataTable dt1 = new DataTable();


        //dt1.Columns.Add("DNN");
        //dt1.Columns.Add("ITEM_NO");
        //dt1.Columns.Add("PALLET");
        //dt1.Columns.Add("MASTER");
        //dt1.Columns.Add("SN");
              
        string[] sArray = str.Split('}');
        int Arrcnt = sArray.Count() - 1;

        if (Arrcnt != Convert.ToInt32(t3))
        {
            //20141226--if sfcqty is not equal to sapqty,delete dn_mt  
            string strdelete = "delete from " + MSSCMDIR + ".[dbo].[Delivery_Notification_MT] where DNID='" + DNID + "'";
            int idetdelete = PDataBaseOperation.PExecSQL("SQL", strconn, strdelete);
            string strdeleteitem = "delete from " + MSSCMDIR + ".[dbo].[Delivery_DNITEM] where DNID='" + DNID + "'";
            int idetdeleteitem = PDataBaseOperation.PExecSQL("SQL", strconn, strdeleteitem);
            return (" Data array not equal " + Arrcnt.ToString()); // Not engough
        }


        ////////////////////////////////////
        // Check data

        DataSet DNT1 = null;

        //20150130--foreach change to for_loop 
        for (int i = 0; i < sArray.Length; i++)
        {
            string ii = sArray[i];
            if (ii.Length > 8)
            {
                string[] CK = ii.Split(',');
                t4 = CK[0];  // DN
                t5 = CK[7];  // SerialTD
                //20140903 if SerialID=''-- pass
                if (t5.ToString().Trim() != "")
                {
                    t6 = " select DNID, SERIALID from " + MSSCMDIR + ".[dbo].Delivery_Notification_DT where SERIALID ='" + t5 + "' and (Delivery_Notification_DT_UF1='' or Delivery_Notification_DT_UF1 is null)";
                    DNT1 = PDataBaseOperation.PSelectSQLDS(DbSql, strconn, t6);
                    v1 = DNT1.Tables[0].Rows.Count;
                    if (v1 > 0)
                    {
                        //string mes = DNT1.Tables[0].Rows[0]["DNID"].ToString() + "_" + DNT1.Tables[0].Rows[0]["SERIALID"].ToString();
                        string mes = "The BSN:" + DNT1.Tables[0].Rows[0]["SERIALID"].ToString() + "has been used in DN:" + DNT1.Tables[0].Rows[0]["DNID"].ToString();
                        string updstr = "update " + MSSCMDIR + ".[dbo].Delivery_Notification_MT set Delivery_Notification_MT_UF10='" + mes + "' where DNID='" + tmpDN + "'";
                        int idetp = PDataBaseOperation.PExecSQL("SQL", strconn, updstr);
                        return (" Delivery_Notification_DT dupli ! Error ");
                        //continue;
                    }
                }
            }
        }
        //foreach (string ii in sArray)
        //{
        //    if (ii.Length > 8)
        //    {
        //        string[] CK = ii.Split(',');
        //        t4 = CK[0];  // DN
        //        t5 = CK[7];  // SerialTD

        //        //20140903 if SerialID=''-- pass
        //        if (t5.ToString().Trim() != "")
        //        {
        //            t6 = " select DNID, SERIALID from [BBSCM].[dbo].Delivery_Notification_DT where SERIALID ='" + t5 + "' and (Delivery_Notification_DT_UF1='' or Delivery_Notification_DT_UF1 is null)";
        //            DNT1 = PDataBaseOperation.PSelectSQLDS(DbSql, strconn, t6);
        //            v1 = DNT1.Tables[0].Rows.Count;
        //            if (v1 > 0)
        //            {
        //                string mes = DNT1.Tables[0].Rows[0]["DNID"].ToString() + "_" + DNT1.Tables[0].Rows[0]["SERIALID"].ToString();
        //                string updstr = "update [BBSCM].[dbo].Delivery_Notification_MT set Delivery_Notification_MT_UF10='" + mes + "' where DNID='" + tmpDN + "'";
        //                int idetp = PDataBaseOperation.PExecSQL("SQL", strconn, updstr);
        //                return (" Delivery_Notification_DT dupli ! Error ");
        //                //continue;
        //            }
        //        }
        //    }
        //}
        

        // checj data

          

        try
        {

            foreach (string i in sArray)
            {
                if (i.Length > 8)
                {
                    string[] SA = i.Split(',');
                    sqlstr = " insert into " + MSSCMDIR + ".[dbo].Delivery_Map_DN ( DNID,ITEMID,BOXID,QAUNIT,QA,LOADQAUNIT,LOADQA,SERIALID,HUID,LOADINTERNALID,LOADPRODUCTRECIPIENTID,TagID,DocumentID ) values ('";
                    sqlstr = sqlstr + SA[0] + "','";
                    sqlstr = sqlstr + SA[1] + "','";
                    sqlstr = sqlstr + SA[2] + "','";
                    sqlstr = sqlstr + SA[3] + "','";
                    sqlstr = sqlstr + SA[4] + "','";
                    sqlstr = sqlstr + SA[5] + "','";
                    sqlstr = sqlstr + SA[6] + "','";
                    sqlstr = sqlstr + SA[7] + "','";
                    sqlstr = sqlstr + SA[8] + "','";
                    sqlstr = sqlstr + SA[9] + "','";
                    sqlstr = sqlstr + SA[10] + "','";
                    sqlstr = sqlstr + SA[11] + "','" + sysdatetime + "' )";
                    int I = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);
                }
            }

            ////////////////////////////////////////////
            // Check Data in Delivery_Map_DN



            t1 = "select SerialID from " + MSSCMDIR + ".[dbo].Delivery_Map_DN where DNID='" + tmpDN + "' ";//20140904 distinct(SerialID)-->count(*) for SerialID may be null
            DataSet DNdt = PDataBaseOperation.PSelectSQLDS(DbSql, strconn, t1);
            if (DNdt == null) return( " Delivery_Map_DN Read Errora ");     // Syn Error
            int DNCnt1 = DNdt.Tables[0].Rows.Count;
            if (DNCnt1 == 0)
               return( "Delivery_Map_DN no data" ); 
                       
            if ( t3 != DNCnt1.ToString().Trim() ) // Serialqty not euqal
                 return( " Delivery_Map_DN no data" );

            //20141031 serialid all null or all have value
            t1 = "select distinct(SerialID) from " + MSSCMDIR + ".[dbo].Delivery_Map_DN where DNID='" + tmpDN + "' ";
            DataSet DNdt1 = PDataBaseOperation.PSelectSQLDS(DbSql, strconn, t1);
            if (DNdt1 == null) return (" Delivery_Map_DN Read Errora ");     // Syn Error
            int DNCnt11 = DNdt.Tables[0].Rows.Count;
            if (t3 != DNCnt1.ToString().Trim() && DNCnt11 != 1) // Serialqty not euqal
            {
                string error = "The Value of SerialID have Exception!Serialid are either all null or all value";
                string strinfo = "update " + MSSCMDIR + ".[dbo].Delivery_Notification_MT set Delivery_Notification_MT_UF10='" + error + "' where DNID='" + tmpDN + "' ";
                int idetinfo = PDataBaseOperation.PExecSQL("SQL", strconn, strinfo);
                return (" Delivery_Map_DN no data");
            }

            //20150311--add Item Qty
            string itemstr = "SELECT distinct ITEMID FROM " + MSSCMDIR + ".[dbo].[Delivery_Map_DN] where DNID='" + tmpDN + "'";
            DataTable itemdt = PDataBaseOperation.PSelectSQLDT(DbSql, strconn,itemstr);
            if (itemdt.Rows.Count > 1)
            {
                for (int itemcnt = 0; itemcnt < itemdt.Rows.Count; itemcnt++)
                {
                    string itemid = itemdt.Rows[itemcnt]["ITEMID"].ToString().Trim();
                    string qastr = "select COUNT(*) QA FROM " + MSSCMDIR + ".[dbo].[Delivery_Map_DN] where DNID='" + tmpDN + "' and ItemID='" + itemid + "'";
                    DataTable qadt = PDataBaseOperation.PSelectSQLDT(DbSql, strconn,qastr);
                    if (qadt.Rows.Count > 0)
                    {
                        string QA = qadt.Rows[0]["QA"].ToString().Trim() + ".0";
                        string qaupd = "update " + MSSCMDIR + ".[dbo].[Delivery_Map_DN] set QA='" + QA + "' where DNID='" + tmpDN + "' and ItemID='" + itemid + "'";
                        int idetupd = PDataBaseOperation.PExecSQL("SQL", strconn, qaupd);
                    }
                }
            }

            sqlstr = "DNID,ItemID,BoxID,LoadInternalID,LoadProductRecipientID,QAUnit,QA,SerialID,HUID,ID";   //,POID,POItemID,InternalID,ProductRecipientID
            sqlstr = "insert into  " + MSSCMDIR + ".[dbo].Delivery_Notification_DT_TEMP  (" + sqlstr + ") select distinct  DNID,ITEMID,BOXID,'BOXTS','BOXTS',QAUNIT,QA,SERIALID,HUID, '" + sysdatetime + "' FROM " + MSSCMDIR + ".[dbo].Delivery_Map_DN  ";
            sqlstr = sqlstr + " WHERE DocumentID='" + sysdatetime + "' and DNID = '" + tmpDN + "' ";
            int II = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr) ;

            sqlstr = "INSERT INTO " + MSSCMDIR + ".[dbo].Delivery_Notification_HU_TEMP ( ID,DNID,HUID,ItemID,BoxID,LoadQAUnit,LoadQA,LoadInternalID,LoadProductRecipientID,Delivery_Notification_HU_UF1 )";
            sqlstr = sqlstr + "SELECT  DISTINCT           DocumentID,DNID,HUID,ItemID,BoxID,LoadQAUnit,LoadQA,LoadInternalID,LoadProductRecipientID,TagID FROM  " + MSSCMDIR + ".[dbo].Delivery_Map_DN";
            sqlstr = sqlstr + " WHERE DocumentID='" + sysdatetime + "' and DNID = '" + tmpDN + "' ";
            int III = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);

            sqlstr = " update a";
            sqlstr = sqlstr + " set ";
            sqlstr = sqlstr + " a.POID=b.POID";
            sqlstr = sqlstr + " ,a.POItemID=b.POItemID";
            sqlstr = sqlstr + " ,a.InternalID=b.InternalID";
            sqlstr = sqlstr + " ,a.ProductRecipientID=b.ProductRecipientID ";
            sqlstr = sqlstr + " from Delivery_Notification_DT_TEMP a, Delivery_DNITEM b where a.DNID=b.DNID and a.ItemID=b.ItemID and a.ID='" + sysdatetime + "'";

            int I3 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);
            sqlstr = "   update Delivery_Notification_DT_TEMP set ";
            sqlstr = sqlstr + "TypeCode =(SELECT   ValueStr FROM  DN_Master  where NameStr ='TypeCode'),";
            sqlstr = sqlstr + "CompletedIndicator= (SELECT   ValueStr FROM  DN_Master  where NameStr ='CompletedIndicator'),";
            sqlstr = sqlstr + "POTypeCode =(sELECT   ValueStr FROM  DN_Master  where NameStr ='POTypeCode'),";
            sqlstr = sqlstr + "POItemTypeCode=(SELECT   ValueStr FROM  DN_Master  where NameStr ='POItemTypeCode') where  ID= '" + sysdatetime + "'";

            int I4 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);

            //************************************************

            //sqlstr = " ID,DNID,HUID,BoxID,ItemID,LoadInternalID,LoadProductRecipientID";
            //sqlstr = sqlstr + ",LoadQAUnit,LoadQA,UserStart,CONFIRMFLAG  ";
            //sqlstr = "  insert into Delivery_Notification_HU(  " + sqlstr + ")  select " + sqlstr + "  from Delivery_Notification_HU_TEMP where     ID= '" + sysdatetime + "'";

            //int I6 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);




            //sqlstr = " ID,DNID,ItemID,BoxID,LoadInternalID,LoadProductRecipientID,TypeCode,CompletedIndicator,QAUnit";
            //sqlstr = sqlstr + ",QA,SerialID,POID,POTypeCode,POItemID,POItemTypeCode,InternalID,ProductRecipientID,UserStart,CONFIRMFLAG,HUID ";
            //sqlstr = " insert into Delivery_Notification_DT(   " + sqlstr + " )  select  " + sqlstr + "  from Delivery_Notification_DT_TEMP where ID= '" + sysdatetime + "'";
            //int I7 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);





            //sqlstr = "truncate table  Delivery_Map_DN ;truncate table Delivery_Notification_DT_TEMP ;truncate table  Delivery_Notification_HU_TEMP";

            //int I100 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);

        }

        catch (Exception ex)
        {

            string err_str = ex.Message;
            return (err_str);

        }
        return ("");
    }
    // ww.GetPackingFromSFCTOBUFByDN("", "", Session["Param3"].ToString(), Session["Param3"].ToString(), "", "", "", DN);
    public string GetPackingFromSFCTOBUFByDN(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string tmpIP, string tmpDN)
    {
        string sqlstr;
        string strconn = DBWriString;

        // WebService地址：http://10.83.216.137/bbryreport/webservice/bbryservice.asmx?wsdl
        // BBRYService.BBRYService bbsc = new BBRYService();

        wsdlLib.BBRYService bbsc = new wsdlLib.BBRYService();

        //bbsc.Url = "http://10.83.216.137/bbryreport/webservice/bbryservice.asmx";
        bbsc.Url = "http://10.74.14.48/report/webservice/bbryservice.asmx";
        //      bbsc.Url = tmpIP;
        string DNID;
        DNID = tmpDN;
        string str = bbsc.ExecuteGetDataByDNN("BBRYDNN", "BBRYDNN", DNID).Replace("TRUE^", "");
        if (str.Substring(0, 5) == "FALSE") { return "FALSE"; }





        str = str.Replace("[", "");
        str = str.Replace("]", "");
        str = str.Replace(",{", "{");
        str = str.Replace("{", "");
        str = str.Replace(@"""", "");
        str = str.Replace(@"\", "");
        str = str.Replace("DNID:", "");
        str = str.Replace("ITEMID:", "");
        str = str.Replace("BOXID:", "");
        str = str.Replace("QAUNIT:", "");
        str = str.Replace("LOADQA", "");
        str = str.Replace("QA:", "");
        str = str.Replace("LOADQAUNIT:", "");
        str = str.Replace("SERIALID:", "");
        str = str.Replace("HUID:", "");
        str = str.Replace("LOADINTERNALID:", "");
        str = str.Replace("LOADPRODUCTRECIPIENTID:", "");
        str = str.Replace("LOAD", "");
        str = str.Replace(@"""", "");
        str = str.Replace(@":", "");


        //DataTable dt1 = new DataTable();


        //dt1.Columns.Add("DNN");
        //dt1.Columns.Add("ITEM_NO");
        //dt1.Columns.Add("PALLET");
        //dt1.Columns.Add("MASTER");
        //dt1.Columns.Add("SN");

        string sysdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss_fffZ");
        string[] sArray = str.Split('}');


        try
        {

            foreach (string i in sArray)
            {
                if (i.Length > 8)
                {

                    string[] SA = i.Split(',');
                    sqlstr = " select count(*) as ct from Delivery_Notification_DT where DNID='" + SA[0] + "'";
                    DataTable dt1 = PDataBaseOperation.PSelectSQLDT("SQL", strconn, sqlstr);
                    if (dt1.Rows[0]["ct"].Equals(0))
                    {


                        sqlstr = "select  ID from  Delivery_Notification_MT where DNID='" + SA[0] + "'";  // 取得MT主表id 號 同步 DN 數據.
                        DataTable dt_DN = PDataBaseOperation.PSelectSQLDT("SQL", strconn, sqlstr);
                        if (!dt_DN.Rows.Count.Equals(0))
                        {
                            sysdatetime = dt_DN.Rows[0]["ID"].ToString();
                            sqlstr = " insert into Delivery_Map_DN ( DNID,ITEMID,BOXID,QAUNIT,QA,LOADQAUNIT,LOADQA,SERIALID,HUID,LOADINTERNALID,LOADPRODUCTRECIPIENTID ,DocumentID ) values ('";
                            sqlstr = sqlstr + SA[0] + "','";
                            sqlstr = sqlstr + SA[1] + "','";
                            sqlstr = sqlstr + SA[2] + "','";
                            sqlstr = sqlstr + SA[3] + "','";
                            sqlstr = sqlstr + SA[4] + "','";
                            sqlstr = sqlstr + SA[5] + "','";
                            sqlstr = sqlstr + SA[6] + "','";
                            sqlstr = sqlstr + SA[7] + "','";
                            sqlstr = sqlstr + SA[8] + "','";
                            sqlstr = sqlstr + SA[9] + "','";
                            sqlstr = sqlstr + SA[10] + "','" + sysdatetime + "' )";
                            int I = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);
                        }
                    }
                }
            }

            sqlstr = "DNID,ItemID,BoxID,LoadInternalID,LoadProductRecipientID,QAUnit,QA,SerialID,HUID,ID";   //,POID,POItemID,InternalID,ProductRecipientID
            sqlstr = "insert into  Delivery_Notification_DT_TEMP  (" + sqlstr + ") select distinct  DNID,ITEMID,BOXID,'BOXTS','BOXTS',QAUNIT,QA,SERIALID,HUID, '" + sysdatetime + "' FROM Delivery_Map_DN  ";
            sqlstr = sqlstr + " WHERE DocumentID='" + sysdatetime + "'";
            int II = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);

            sqlstr = "INSERT INTO Delivery_Notification_HU_TEMP ( ID,DNID,HUID,ItemID,BoxID,LoadQAUnit,LoadQA,LoadInternalID,LoadProductRecipientID )";
            sqlstr = sqlstr + "SELECT  DISTINCT           DocumentID,DNID,HUID,ItemID,BoxID,LoadQAUnit,LoadQA,LoadInternalID,LoadProductRecipientID FROM  Delivery_Map_DN";
            sqlstr = sqlstr + " WHERE DocumentID='" + sysdatetime + "'";
            int III = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);

            sqlstr = " update a";
            sqlstr = sqlstr + " set ";
            sqlstr = sqlstr + " a.POID=b.POID";
            sqlstr = sqlstr + " ,a.POItemID=b.POItemID";
            sqlstr = sqlstr + " ,a.InternalID=b.InternalID";
            sqlstr = sqlstr + " ,a.ProductRecipientID=b.ProductRecipientID ";
            sqlstr = sqlstr + " from Delivery_Notification_DT_TEMP a, Delivery_DNITEM b where a.DNID=b.DNID and a.ItemID=b.ItemID and a.ID='" + sysdatetime + "'";

            int I3 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);
            sqlstr = "   update Delivery_Notification_DT_TEMP set ";
            sqlstr = sqlstr + "TypeCode =(SELECT   ValueStr FROM  DN_Master  where NameStr ='TypeCode'),";
            sqlstr = sqlstr + "CompletedIndicator= (SELECT   ValueStr FROM  DN_Master  where NameStr ='CompletedIndicator'),";
            sqlstr = sqlstr + "POTypeCode =(sELECT   ValueStr FROM  DN_Master  where NameStr ='POTypeCode'),";
            sqlstr = sqlstr + "POItemTypeCode=(SELECT   ValueStr FROM  DN_Master  where NameStr ='POItemTypeCode') where  ID= '" + sysdatetime + "'";

            int I4 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);

            //************************************************

            //sqlstr = " ID,DNID,HUID,BoxID,ItemID,LoadInternalID,LoadProductRecipientID";
            //sqlstr = sqlstr + ",LoadQAUnit,LoadQA,UserStart,CONFIRMFLAG  ";
            //sqlstr = "  insert into Delivery_Notification_HU(  " + sqlstr + ")  select " + sqlstr + "  from Delivery_Notification_HU_TEMP where     ID= '" + sysdatetime + "'";

            //int I6 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);




            //sqlstr = " ID,DNID,ItemID,BoxID,LoadInternalID,LoadProductRecipientID,TypeCode,CompletedIndicator,QAUnit";
            //sqlstr = sqlstr + ",QA,SerialID,POID,POTypeCode,POItemID,POItemTypeCode,InternalID,ProductRecipientID,UserStart,CONFIRMFLAG,HUID ";
            //sqlstr = " insert into Delivery_Notification_DT(   " + sqlstr + " )  select  " + sqlstr + "  from Delivery_Notification_DT_TEMP where ID= '" + sysdatetime + "'";
            //int I7 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);





            //sqlstr = "truncate table  Delivery_Map_DN ;truncate table Delivery_Notification_DT_TEMP ;truncate table  Delivery_Notification_HU_TEMP";

            //int I100 = PDataBaseOperation.PExecSQL("SQL", strconn, sqlstr);

        }

        catch (Exception ex)
        {

            string err_str = ex.Message;

        }
        return ("");
    }

    public string GetSapDN(string DN, string dboReadS, string dboWriteS, string bdate, string edate, string MSSCMDIR)
    {
        try
        {
            //DataBaseOperation dboRead = new DataBaseOperation("ORACLE", dboReadS);
            //DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", dboWriteS);
            //string sql = "select * from publib.dn_3b2header where dn='" + DN + "' ";
            ////int mm = dboWrite.ExecSQL(sql);
            //DataTable dtdn = dboRead.SelectSQLDT(sql);
            //int mm = dtdn.Rows.Count;

            //SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh");

            // SAPConnection con = new SAPConnection("ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=RFCLFSAP01;PASSWD=12345678;LANG=zh");
            SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=Foxconn8;LANG=zh");
            // SAPConnection con = new SAPConnection(dboReadS);    //"ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh"
            //   SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh");
            // SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802test;SYSNR=00;USER=lfsupitsap;PASSWD=88foxcon;LANG=zh");
            // ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=SIDCFI;PASSWD=FI61625;LANG=zh");
            con.Open();

            SAPCommand cmd = new SAPCommand(con);
            //cmd.CommandText = "EXEC ZRFC_SD_BBRY_0001   @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV , @SOLD_TO= @SOLD_TOV, @OUTPUT_HEADER=@OUTPUT_HEADERV output, @OUTPUT_ITEM=@OUTPUT_ITEMV output ";
            cmd.CommandText = "EXEC ZRFC_SD_BBRY_0001   @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV ,  @OUTPUT_HEADER=@OUTPUT_HEADERV output, @OUTPUT_ITEM=@OUTPUT_ITEMV output ";
            //  cmd.CommandText = "EXEC ZRFC_INTEL_B2B_3B2  @T_INPUT=@TINPUT output, @T_OUTPUT=@TOUTPUT output,@T_MSG=@TMSG output";
            //@VBELN=@VBELNV ,
            SAPParameter BEGIN_DATEV = new SAPParameter("@BEGIN_DATEV", ParameterDirection.Input);
            BEGIN_DATEV.Value = bdate;
            cmd.Parameters.Add(BEGIN_DATEV);

            SAPParameter END_DATEV = new SAPParameter("@END_DATEV", ParameterDirection.Input);
            END_DATEV.Value = edate;
            cmd.Parameters.Add(END_DATEV);


            SAPParameter OUTPUT_HEADERV = new SAPParameter("@OUTPUT_HEADERV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUT_HEADERV);
            SAPParameter OUTPUT_ITEMV = new SAPParameter("@OUTPUT_ITEMV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUT_ITEMV);
            SAPDataReader dr = cmd.ExecuteReader();
            DataTable SAPdt1 = (DataTable)cmd.Parameters["@OUTPUT_HEADERV"].Value;
            DataTable SAPdt2 = (DataTable)cmd.Parameters["@OUTPUT_ITEMV"].Value;

            if (SAPdt1.Rows.Count.Equals(0) || SAPdt2.Rows.Count.Equals(0))
            {
                return "False";
            }
            string Error = "";
            string sqlstr = "DNID,CreationDT,IssueDT,GrossWeight,NetWeight,Country";
            //  sqlstr = sqlstr + ",BuyerPartyInternalID,VendorPartyVendorID ";
            Int64 v1 = 0, v2 = 0, v3 = 0;
            int v4 = 0, v5 = 0;
            string tmp6 = "", tmp7 = "";
            DataSet DNdt = null;

            int sapv1 = SAPdt1.Rows.Count;
            int sapv2 = SAPdt2.Rows.Count;

            if ((sapv1 <= 0) && (sapv2 <= 0)) return (""); // No data 

            string[,] arrDN_MT = new string[sapv1 + 1, 5 + 1];
            for (v2 = 0; v2 <= sapv1; v2++)
                for (v3 = 0; v3 <= 5; v3++)
                    arrDN_MT[v2, v3] = "";



            int I5 = 0, I6 = 0;
            string sysdatetime = System.DateTime.Now.ToString("yyMMddHHmmssmm"); // 
            string ID = "F" + SAPdt1.Rows[0]["VBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;
            string tmpDN = "";
            for (int i = 0; i < SAPdt1.Rows.Count; i++)
            {

                sysdatetime = System.DateTime.Now.ToString("yyMMddHHmmssmm");
                v1 = Convert.ToInt64(sysdatetime) + i;
                sysdatetime = v1.ToString();   //  time+ count var 

                ID = "F" + SAPdt1.Rows[0]["VBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;

                if (SAPdt1.Rows[i]["VBELN"].ToString().Trim() == "") continue;

                tmpDN = SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8);

                
                sqlstr = "select count(*) ct from Delivery_Notification_MT where DNID='" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows[0]["ct"].Equals(0))
                {

                    arrDN_MT[i + 1, 1] = tmpDN;
                    arrDN_MT[i + 1, 2] = ID;
                    //     ,HUQTY-----pallet_QTY
                    //     ,BoxQTY-----Carton_QTY
                    //     ,SerialIDQTY----QTY
                    //,ItemQTY -----""
                    // netunitcode----unit(SAP)

                    //sqlstr = "GrossUnitCode,ID,DNID,CreationDT,IssueDT,GrossWeight,NetWeight,Country,BuyerPartyInternalID, VendorPartyVendorID ,HUQTY ,BoxQTY ,SerialIDQTY  ,ItemQTY ,netunitcode ,VolumeUnitCode,Volume";

                    //20140912 去掉IssueDT
                    sqlstr = "GrossUnitCode,ID,DNID,CreationDT,GrossWeight,NetWeight,Country,BuyerPartyInternalID, VendorPartyVendorID ,HUQTY ,BoxQTY ,SerialIDQTY  ,ItemQTY ,netunitcode ,VolumeUnitCode,Volume";
                    sqlstr = "insert into  Delivery_Notification_MT (" + sqlstr + " ) values ('";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["UNIT"].ToString().Replace("KG", "KGM") + "','" + ID + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    //2013-12-02T12:12:12.1234567Z
                    sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["BLDAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";
                    //sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["WADAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["BRGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["NTGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["COUNTRY_KEY"] + "',";
                    sqlstr = sqlstr + "'" + "" + "',";
                    sqlstr = sqlstr + "'" + "" + "'  , ";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["pallet_QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["Carton_QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["ITEM_QTY"] + ",";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["UNIT"].ToString().Replace("KG", "KGM") + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["VOLEH"].ToString().Replace("M3", "LTR") + "',";


                    //  Thread.Sleep(100);  // 1000 = 1sec  sleep
                    // 20140508
                    //sqlstr = sqlstr + "'" + (System.Convert.ToInt32(SAPdt1.Rows[i]["TAVOL"]) * 1000).ToString() + "')";
                    //int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    //sqlstr = "  update Delivery_Notification_MT set ";
                    //sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
                    //I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    //sqlstr = " update  a ";
                    //sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
                    //sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
                    //sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.ID= '" + ID + "'";
                    //I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

                    sqlstr = sqlstr + "'" + (System.Convert.ToInt32(SAPdt1.Rows[i]["TAVOL"]) * 1000).ToString() + "')";
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    sqlstr = "  update Delivery_Notification_MT set ";
                    sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where DNID= '" + tmpDN + "'";
                    I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    sqlstr = " update  a ";
                    sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
                    sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
                    sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.DNID= '" + tmpDN + "'";
                    I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                }
            }


            for (int i = 0; i < SAPdt2.Rows.Count; i++)
            {
                sqlstr = "select ID from Delivery_Notification_MT where DNID='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' ";
                DNdt = PDataBaseOperation.PSelectSQLDS("SQL", dboWriteS, sqlstr);
                v4 = DNdt.Tables[0].Rows.Count;
                if (v4 > 0)
                    tmp6 = DNdt.Tables[0].Rows[0]["ID"].ToString().Trim();

                sqlstr = "select count(*) ct from Delivery_DNITEM where DNID='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' and ItemID ='" + SAPdt2.Rows[i]["POSNR"] + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows[0]["ct"].Equals(0))
                {
                    sqlstr = "insert into  Delivery_DNITEM ( ID,DNID,ItemID,POID,POItemID,InternalID,ProductRecipientID ) values ( ";
                    // 20140507 sqlstr = sqlstr + "'" + ID + "',";
                    sqlstr = sqlstr + "'" + tmp6 + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["POSNR"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VGBEL"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VGPOS"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["MATNR"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["KDMAT"] + "')";
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    if (v > 0) // Succ update POID into DN
                    {
                        tmpDN = SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8); // DNID
                        tmp7 = SAPdt2.Rows[i]["VGBEL"].ToString().Trim(); // POID
                        sqlstr = " update Delivery_Notification_MT set POID = '" + SAPdt2.Rows[i]["VGBEL"] + "'  where DNID= '" + tmpDN + "'";
                        I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    }
                }
            }


            //  20140504
            //  sqlstr = "  update Delivery_Notification_MT set ";
            //  sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
            //  int I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
            //  sqlstr = " update  a ";
            //  sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
            //  sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
            //  sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.ID= '" + ID + "'";
            //  int I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

            // 20140508
            for (int i = 0; i < SAPdt1.Rows.Count; i++)
            {
                tmpDN = arrDN_MT[i + 1, 1];
                ID = arrDN_MT[i + 1, 2];
                if ((ID != "") && (tmpDN != ""))
                {
                    sqlstr = "  update Delivery_Notification_MT set ";
                    sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
                    I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    sqlstr = " update  a ";
                    sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
                    sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c ";
                    sqlstr = sqlstr + " where  a.POID =c.POID and a.POID =b.POID and a.DNID = '" + tmpDN + "'";
                    I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                }
            }


            return "True";
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            return "False";
        }

    }  //

    public string GetSapDN20140425(string DN, string dboReadS, string dboWriteS, string bdate, string edate)
    {
        try
        {
            //DataBaseOperation dboRead = new DataBaseOperation("ORACLE", dboReadS);
            //DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", dboWriteS);
            //string sql = "select * from publib.dn_3b2header where dn='" + DN + "' ";
            ////int mm = dboWrite.ExecSQL(sql);
            //DataTable dtdn = dboRead.SelectSQLDT(sql);
            //int mm = dtdn.Rows.Count;

            //test 802 ip 10.134.92.27， SYSNR=00;   USER=RFCTJIDM

            //SAPConnection con = new SAPConnection("ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=SIDCFI;PASSWD=FI61625;LANG=zh");
            SAPConnection con = new SAPConnection("ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=RFCLFSAP01;PASSWD=12345678;LANG=zh");
            //SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh");
            // SAPConnection con = new SAPConnection(dboReadS);    //"ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh"
            //   SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh");
            // SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802test;SYSNR=00;USER=lfsupitsap;PASSWD=88foxcon;LANG=zh");
            // ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=SIDCFI;PASSWD=FI61625;LANG=zh");
            con.Open();


            SAPCommand cmd = new SAPCommand(con);

            //cmd.CommandText = "EXEC ZRFC_SD_BBRY_0001   @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV , @SOLD_TO= @SOLD_TOV, @OUTPUT_HEADER=@OUTPUT_HEADERV output, @OUTPUT_ITEM=@OUTPUT_ITEMV output ";
            cmd.CommandText = "EXEC ZRFC_SD_BBRY_0001   @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV , @SOLD_TO= @SOLD_TOV, @OUTPUT_HEADER=@OUTPUT_HEADERV output, @OUTPUT_ITEM=@OUTPUT_ITEMV output ";
            //  cmd.CommandText = "EXEC ZRFC_INTEL_B2B_3B2  @T_INPUT=@TINPUT output, @T_OUTPUT=@TOUTPUT output,@T_MSG=@TMSG output";

            //@VBELN=@VBELNV ,

            SAPParameter BEGIN_DATEV = new SAPParameter("@BEGIN_DATEV", ParameterDirection.Input);

            BEGIN_DATEV.Value = bdate;
            cmd.Parameters.Add(BEGIN_DATEV);



            SAPParameter END_DATEV = new SAPParameter("@END_DATEV", ParameterDirection.Input);

            END_DATEV.Value = edate;
            cmd.Parameters.Add(END_DATEV);



            SAPParameter SOLD_TOV = new SAPParameter("@SOLD_TOV", ParameterDirection.Input);

            SOLD_TOV.Value = "0000358190";
            //  SOLD_TOV.Value = "0000347732";
            //     SOLD_TOV.Value = "CNM006";
            cmd.Parameters.Add(SOLD_TOV);





            SAPParameter OUTPUT_HEADERV = new SAPParameter("@OUTPUT_HEADERV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUT_HEADERV);
            SAPParameter OUTPUT_ITEMV = new SAPParameter("@OUTPUT_ITEMV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUT_ITEMV);




            SAPDataReader dr = cmd.ExecuteReader();
            DataTable SAPdt1 = (DataTable)cmd.Parameters["@OUTPUT_HEADERV"].Value;
            DataTable SAPdt2 = (DataTable)cmd.Parameters["@OUTPUT_ITEMV"].Value;

            string Error = "";

            string sqlstr = "DNID,CreationDT,IssueDT,GrossWeight,NetWeight,Country";
            //  sqlstr = sqlstr + ",BuyerPartyInternalID,VendorPartyVendorID	";

            string sysdatetime = System.DateTime.Now.ToString("yyMMddHHmmss");
            string ID = "FIH" + SAPdt1.Rows[0]["VBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;
            for (int i = 0; i < SAPdt1.Rows.Count; i++)
            {
                sqlstr = "select count(*) ct from Delivery_Notification_MT where DNID='" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "'";

                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows[0]["ct"].Equals(0))
                {



                    //     ,HUQTY-----pallet_QTY
                    //     ,BoxQTY-----Carton_QTY
                    //     ,SerialIDQTY----QTY
                    //,ItemQTY -----""
                    // netunitcode----unit(SAP)

                    sqlstr = "GrossUnitCode,ID,DNID,CreationDT,IssueDT,GrossWeight,NetWeight,Country,BuyerPartyInternalID,	VendorPartyVendorID ,HUQTY ,BoxQTY ,SerialIDQTY  ,ItemQTY ,netunitcode ,VolumeUnitCode,Volume";
                    sqlstr = "insert into  Delivery_Notification_MT (" + sqlstr + " ) values ('";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["UNIT"].ToString().Replace ("KG","KGM")+ "','" + ID + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    //2013-12-02T12:12:12.1234567Z
                    sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["BLDAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";
                    sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["WADAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";

                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["BRGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["NTGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["COUNTRY_KEY"] + "',";
                    sqlstr = sqlstr + "'" + "" + "',";

                    sqlstr = sqlstr + "'" + "" + "'  , ";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["pallet_QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["Carton_QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["ITEM_QTY"] + ",";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["UNIT"].ToString().Replace("KG", "KGM") + "',";

                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["VOLEH"].ToString().Replace("M3", "LTR") + "',";
                 
                    sqlstr = sqlstr + "'" + (System.Convert.ToInt32(    SAPdt1.Rows[i]["TAVOL"] )*1000).ToString()  + "')";
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                }
            }




            for (int i = 0; i < SAPdt2.Rows.Count; i++)
            {



                sqlstr = "select count(*) ct from Delivery_DNITEM where DNID='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' and ItemID ='" + SAPdt2.Rows[i]["POSNR"] + "'";

                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows[0]["ct"].Equals(0))
                {

                    sqlstr = "insert into  Delivery_DNITEM ( ID,DNID,ItemID,POID,POItemID,InternalID,ProductRecipientID ) values ( ";
                    sqlstr = sqlstr + "'" + ID + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["POSNR"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VGBEL"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VGPOS"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["MATNR"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["KDMAT"] + "')";
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);



                }
            }


            sqlstr = "  update Delivery_Notification_MT set ";
            sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
            int I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

            sqlstr = " update  a ";
            sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
            sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
            sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.ID= '" + ID + "'";

            int I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);



            return "True";

        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            return "False";
        }


    }
    /////////////////////////////////////////////////////
    // 20140426 Get DLABEMINFO from Delivery_DNITEM 
    //20140920 修正
    public string AutoCreDLABELINFO(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string BBSCMDIR)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0, v9 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);
        string DBType = DbSql;

        // sqlr = " SELECT * from Delivery_DNITEM order by DN, POID desc ";
        sqlr = " select a.DNID,a.ID,a.POID,a.ITEMID,a.INTERNALID,b.ProductRecipientID, "
        + " b.PRODUCTBUYERID, b.PRODUCTRECIPIENTPARTYID, b.DELIVERYSTARTDT,c.GivenName, "
        + " c.CountryCode,c.RegionCode,c.PostalCode,c.CityName,c.StreetName,c.CareOfName "
        + " ,'LABELCODE','CDATE','FLAG1','FLAG2' "
        + " from " + BBSCMDIR + ".dbo.Delivery_DNITEM a," + BBSCMDIR + ".dbo.PO_CREATE_DT b," + BBSCMDIR + ".dbo.POSoldToAddress c "
        + " where a.POID=b.POID and b.ID=c.ID and a.DNID not in (select DNID from " + BBSCMDIR + ".dbo.DLABELINFO) ";


        // sqlr = " select a.DNID,a.ID,a.POID,a.ITEMID,a.INTERNALID,b.ProductRecipientID, "
        // + " b.PRODUCTBUYERID,b.PRODUCTRECIPIENTPARTYID, b.DELIVERYSTARTDT,c.GivenName, "
        // + " c.CountryCode,c.RegionCode,c.PostalCode,c.CityName,c.StreetName,c.CareOfName "
        // + " ,'LABELCODE','CDATE','FLAG1','FLAG2' "
        // + " from BBSCM.dbo.Delivery_DNITEM a,BBSCM.dbo.PO_CREATE_DT b,BBSCM.dbo.POSoldToAddress c "
        // + " where a.POID=b.POID "
        // + " and b.ID=c.ID "
        // + " union "
        // + " select DNID,ID,POID,ITEMID,INTERNALID,ProductRecipientID, "
        // + " 'b.PRODUCTBUYERID_NO','RODUCTRECIPIENTPARTYID_NO','b.DELIVERYSTARTDT_NO','c.GivenName_NO', "
        // + " 'c.CountryCode_NO','c.RegionCode_NO','c.PostalCode_NO','c.CityName_NO','c.StreetName_NO','c.CareOfName_NO', "
        // + " 'LABELCODE_NO','CDATE_NO','FLAG1_NO','FLAG2_NO' from BBSCM.dbo.Delivery_DNITEM "
        // + " where POID not in( "
        // + " select distinct a.POID from BBSCM.dbo.Delivery_DNITEM a,BBSCM.dbo.PO_CREATE_DT b "
        // + " where a.POID=b.POID "
        // + " ) ";



        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if ((DNdt == null) || (DNdt.Tables.Count <= 0)) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0"); // Not Data
        v8 = DNCnt;

        int ycnt1 = 30;
        string[,] arrayDLABELINFO = new string[DNCnt + 1, ycnt1 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= ycnt1; v2++)
                arrayDLABELINFO[v1, v2] = "";

        for (v1 = 0; v1 < v8; v1++)
        {
            arrayDLABELINFO[v1 + 1, 0] = (v1 + 1).ToString();
            arrayDLABELINFO[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["DNID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["POID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 4] = DNdt.Tables[0].Rows[v1]["ITEMID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["INTERNALID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["ProductRecipientID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["PRODUCTBUYERID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["PRODUCTRECIPIENTPARTYID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["DELIVERYSTARTDT"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 10] = DNdt.Tables[0].Rows[v1]["GivenName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 11] = DNdt.Tables[0].Rows[v1]["CountryCode"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 12] = DNdt.Tables[0].Rows[v1]["RegionCode"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 13] = DNdt.Tables[0].Rows[v1]["PostalCode"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 14] = DNdt.Tables[0].Rows[v1]["CityName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 15] = DNdt.Tables[0].Rows[v1]["StreetName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 16] = DNdt.Tables[0].Rows[v1]["CareOfName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 17] = ""; // DNdt.Tables[0].Rows[v1]["LABELCODE"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 18] = DateTime.Now.ToString("yyyyMMddHHmmssmm"); // DNdt.Tables[0].Rows[v1]["CDATE"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 19] = ""; // DNdt.Tables[0].Rows[v1]["FLAG1"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 20] = ""; // DNdt.Tables[0].Rows[v1]["FLAG2"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, ycnt1] = "N";

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Check not in DLABELINFO
        for (v1 = 0; v1 < v8; v1++)
        {
            v2 = 0;
            tmp3 = "N";
            tmp1 = arrayDLABELINFO[v1 + 1, 1]; // DNID
            tmp2 = arrayDLABELINFO[v1 + 1, 3];
            sqlr = " SELECT count(*) NoDupCNT from DLABELINFO where DNID = '" + tmp1 + "' and POID = '" + tmp2 + "' ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("Read Error file DLABELINFO"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) tmp3 = "N";
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoDupCNT"].ToString().Trim());
            if (v2 == 0) tmp3 = "Y";
            arrayDLABELINFO[v1 + 1, ycnt1] = tmp3; // Record Not-finsh count 
        }

        v5 = 0;
        for (v1 = 0; v1 < v8; v1++)
        {
            tmp1 = arrayDLABELINFO[v1 + 1, 1].ToString().Trim(); // DNID 
            tmp2 = arrayDLABELINFO[v1 + 1, 3].ToString().Trim(); // POID
            tmp3 = arrayDLABELINFO[v1 + 1, ycnt1].ToString().Trim();
            tmp4 = "";


            if ((tmp3 == "Y") && (tmp1 != "") && (tmp2 != ""))
            {
                tmpsqlW = " Insert into DLABELINFO ( DNID, ID, POID, ITEMID, INTERNALID, ProductRecipientID, PRODUCTBUYERID, PRODUCTRECIPIENTPARTYID, "
                + " DELIVERYSTARTDT, GivenName, CountryCode, RegionCode, PostalCode, CityName, StreetName, CareOfName, "
                + " LABELCODE, CDATE, FLAG1, FLAG2 ) values "
                + "('" + arrayDLABELINFO[v1 + 1, 01].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 02].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 03].ToString().Trim() + "', "
                + " '" + arrayDLABELINFO[v1 + 1, 04].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 05].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 06].ToString().Trim() + "', "
                + " '" + arrayDLABELINFO[v1 + 1, 07].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 08].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 09].ToString().Trim() + "', "
                + " '" + arrayDLABELINFO[v1 + 1, 10].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 11].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 12].ToString().Trim() + "', "
                + " '" + arrayDLABELINFO[v1 + 1, 13].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 14].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 15].ToString().Trim() + "', "
                + " '" + arrayDLABELINFO[v1 + 1, 16].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 17].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 18].ToString().Trim() + "', "
                + " '" + arrayDLABELINFO[v1 + 1, 19].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 20].ToString().Trim() + "' ) ";
                v2 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                if (v2 < 0) // Successed, It could be not data
                    v3++;
            } // End Write 
            else
            {
                v5++;
                tmp4 = tmp1 + " / " + tmp4;
            }


        }

        return ("");
    } // End AutoCreDLABELINFO


    /////////////////////////////////////////////////////
    //  20140426 Get DLABEMCODE from DLABEMINFO 
    public string AutoCreFDLABM1(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string BBSCMDIR)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0, v9 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);
        string DBType = DbSql;

        //   sqlr = " SELECT * from Delivery_DNITEM order by DN, POID desc ";
        sqlr = " select * from " + BBSCMDIR + ".dbo.DLABELINFO where ( FLAG1 = ''  or FLAG1 = null ) and ( INTERNALID not in (select INTERNALID from " + BBSCMDIR + ".dbo.FDLABM1) ) order by INTERNALID asc";


        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if ((DNdt == null) || (DNdt.Tables.Count <= 0)) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0"); // Not Data
        v8 = DNCnt;

        int ycnt1 = 30;
        string[,] arrayDLABELINFO = new string[DNCnt + 1, ycnt1 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= ycnt1; v2++)
                arrayDLABELINFO[v1, v2] = "";

        for (v1 = 0; v1 < v8; v1++)
        {
            arrayDLABELINFO[v1 + 1, 0] = (v1 + 1).ToString();
            arrayDLABELINFO[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["DNID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["POID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 4] = DNdt.Tables[0].Rows[v1]["ITEMID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["INTERNALID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["ProductRecipientID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["PRODUCTBUYERID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["PRODUCTRECIPIENTPARTYID"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["DELIVERYSTARTDT"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 10] = DNdt.Tables[0].Rows[v1]["GivenName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 11] = DNdt.Tables[0].Rows[v1]["CountryCode"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 12] = DNdt.Tables[0].Rows[v1]["RegionCode"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 13] = DNdt.Tables[0].Rows[v1]["PostalCode"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 14] = DNdt.Tables[0].Rows[v1]["CityName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 15] = DNdt.Tables[0].Rows[v1]["StreetName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 16] = DNdt.Tables[0].Rows[v1]["CareOfName"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 17] = "";  // DNdt.Tables[0].Rows[v1]["LABELCODE"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 18] = DateTime.Now.ToString("yyyyMMddHHmmssmm"); //  DNdt.Tables[0].Rows[v1]["CDATE"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 19] = ""; //  DNdt.Tables[0].Rows[v1]["FLAG1"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, 20] = ""; //  DNdt.Tables[0].Rows[v1]["FLAG2"].ToString().Trim();
            arrayDLABELINFO[v1 + 1, ycnt1] = "N";

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        v5 = 0;
        // Check not in DLABELINFO
        for (v1 = 0; v1 < v8; v1++)
        {
            v2 = 0;
            tmp3 = "N";
            tmp1 = arrayDLABELINFO[v1 + 1, 5];  // INTERNALID, H.H.PART
            tmp2 = arrayDLABELINFO[v1 + 1, 3];
            sqlr = " SELECT count(*) NoDupCNT from " + BBSCMDIR + ".dbo.FDLABM1 where INTERNALID = '" + tmp1 + "' "; // and POID = '" + tmp2 + "' ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if ((DNdt == null) || (DNdt.Tables.Count <= 0)) return ("Read Error file DLABELM1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) tmp3 = "N";
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoDupCNT"].ToString().Trim());

            if (v2 == 0)  // Insert  
            {
                tmp3 = "Y";
                arrayDLABELINFO[v1 + 1, ycnt1] = tmp3; // Record Not-finsh count 
                tmp1 = arrayDLABELINFO[v1 + 1, 5].ToString().Trim();  // INTERNALID, H.H.PART
                tmp2 = arrayDLABELINFO[v1 + 1, 3].ToString().Trim();  // 
                tmp3 = arrayDLABELINFO[v1 + 1, ycnt1].ToString().Trim();
                tmp4 = "";
                tmp5 = "N"; // Update flag
                if ((tmp3 == "Y") && (tmp1 != "") && (tmp2 != ""))
                {
                    tmpsqlW = " Insert into FDLABM1  ( DNID, ID, POID, ITEMID, INTERNALID, ProductRecipientID, PRODUCTBUYERID, PRODUCTRECIPIENTPARTYID, "
                    + " DELIVERYSTARTDT, GivenName, CountryCode, RegionCode, PostalCode, CityName, StreetName, CareOfName, "
                    + " POSLABEL, CDATE, FLAG1, FLAG2 ) values "
+ "('" + arrayDLABELINFO[v1 + 1, 01].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 02].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 03].ToString().Trim() + "', "
+ " '" + arrayDLABELINFO[v1 + 1, 04].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 05].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 06].ToString().Trim() + "', "
+ " '" + arrayDLABELINFO[v1 + 1, 07].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 08].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 09].ToString().Trim() + "', "
+ " '" + arrayDLABELINFO[v1 + 1, 10].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 11].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 12].ToString().Trim() + "', "
+ " '" + arrayDLABELINFO[v1 + 1, 13].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 14].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 15].ToString().Trim() + "', "
+ " '" + arrayDLABELINFO[v1 + 1, 16].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 17].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 18].ToString().Trim() + "', "
+ " '" + arrayDLABELINFO[v1 + 1, 19].ToString().Trim() + "', '" + arrayDLABELINFO[v1 + 1, 20].ToString().Trim() + "'  ) ";
                    v2 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                    if (v2 < 0) // Successed, It could be not data
                        v3++;
                    else
                        tmp5 = "Y";


                } // End Write 
                else
                {
                    v5++;
                    tmp4 = tmp1 + " / " + tmp4;
                    tmp5 = "Y";
                }


                if (tmp5 == "Y")
                {
                    tmpselwri = "  update DLABELINFO set FLAG1 = 'Y'  where INTERNALID = '" + tmp1 + "'";
                    v9 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpselwri);
                }

            } // end if (v2 == 0)  // Insert  
        } // End Loop

        return ("");
    }   // End AutoCreDLABELCODE

    public string AutoCheckPOCRAETE(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string BBSCMDIR)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0, v9 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        ///////////////////////////////////////////////////////////////////////////////
        // if ( PO_CREATE_MT CONFIRMATIONFLAG <> "Y" ) CHeck By this ID
        // {
        // if ( ID in PO_CREATE_DT == "Y" ) and PO_CONFIRMATION == "Y" )
        // if ( ID in PO_CREATE_DT QTY == SUM PO_CONFIRMATION ) 
        // PO_CONFIRMATIONFLAG == "Y"
        // }
        string DBType = DbSql;

        sqlr = " SELECT * from PO_CREATE_MT where ( CONFIRMFLAG ='' or CONFIRMFLAG is null ) order by ID desc ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0"); // Not Data

        string[,] arrayPO_CREATE_MT = new string[DNCnt + 1, 15 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayPO_CREATE_MT[v1, v2] = "";

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayPO_CREATE_MT[v1 + 1, 0] = (v1 + 1).ToString();
            arrayPO_CREATE_MT[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["POID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["ReceiveTime"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 4] = ""; // Detail finsih count
            arrayPO_CREATE_MT[v1 + 1, 5] = ""; // Detail not finsih count
            arrayPO_CREATE_MT[v1 + 1, 6] = ""; // PO_CONFIRMATION_MT finsih count 
            arrayPO_CREATE_MT[v1 + 1, 7] = ""; // PO_CONFIRMATION_MT not finsih count
            arrayPO_CREATE_MT[v1 + 1, 8] = ""; // Detail = PO_CONFIRMATION_MT
            arrayPO_CREATE_MT[v1 + 1, 9] = ""; // Last Confirm status update 
        }

        v8 = DNCnt;

        for (v1 = 0; v1 < v8; v1++)
        {
            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CREATE_DT where ( CONFIRMFLAG = 'Y' ) and ID = '" + tmp1 + "' ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 4] = v2.ToString(); // Record Not-finsh count 

            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CREATE_DT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) and ID = '" + tmp1 + "'";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 5] = v2.ToString(); // Record Not-finsh count 

            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CONFIRMATION_MT where ( CONFIRMFLAG = 'Y' ) and ID = '" + tmp1 + "'";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 6] = v2.ToString(); // Record Not-finsh count 

            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CONFIRMATION_MT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) and ID = '" + tmp1 + "'";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 7] = v2.ToString(); // Record Not-finsh count 


        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Check by each ID if Detail finish > 0 and fail = 0 and Confirmation finish > 0 and fail = 0 
        // Rewrite Master CONFIRMFLAG = "Y"
        for (v1 = 0; v1 < v8; v1++)
        {
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1].ToString().Trim();
            v4 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 4]); // Detail finsih count
            v5 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 5]); // Detail not finsih count
            v6 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 6]); // PO_CONFIRMATION_MT finsih count 
            v7 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 7]); // PO_CONFIRMATION_MT not finsih count
            // v8 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 8]); // Detail = PO_CONFIRMATION_MT
            // v9 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 9]); // Last Confirm status update 

            if ((v4 > 0) && (v5 == 0) && (v6 > 0) && (v7 == 0))
            {
                tmp2 = "Y";
                tmpsqlW = "Update PO_CREATE_MT set CONFIRMFLAG = '" + tmp2 + "' where ID = '" + tmp1 + "' ";
                v2 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                if (v2 < 0) // Successed, It could be not data
                    v3++;
            } // End Write 
        }


        //po_change
        string Ret1a = "", sqlra = "", sqlwa = "", tmpsqlWa = "", spa = "", sp0a = "0", tpoa = "", tdna = "", tmpselwria = "", ErrFlaga = "", sp1a = "";
        DataSet dt1a = null, dt2a = null, DNdta = null, dt3a = null, dt4a = null;
        int v1a = 0, v2a = 0, v3a = 0, v4a = 0, v5a = 0, v6a = 0, v7a = 0, daycnta = 1, DNCnta = 0, v8a = 0, v9a = 0;
        string t01a = "", t02a = "", t03a = "", t04a = "", t05a = "", t06a = "", t07a = "", t08a = "", t09a = "", t10a = "", t11a = "", t12a = "", t13a = "", t14a = "", t15a = "";
        string t21a = "", t22a = "", t23a = "", t121a = "";
        string tmp1a = "", tmp2a = "", tmp3a = "", tmp4a = "", tmp5a = "", tmp6a = "", tmp7a = "";
        Decimal d1a = 0, d2a = 0, d3a = 0;
        sqlra = " SELECT * from PO_CHANGE_MT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) order by ID desc ";
        DNdta = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlra);
        if (DNdta == null) return ("-1"); // Syn Error
        DNCnta = DNdta.Tables[0].Rows.Count;
        if (DNCnta == 0) return ("0"); // Not Data

        string[,] arrayPO_CREATE_MTa = new string[DNCnta + 1, 15 + 1];
        for (v1a = 0; v1a <= DNCnta; v1a++)
            for (v2a = 0; v2a <= 10; v2a++)
                arrayPO_CREATE_MTa[v1a, v2a] = "";

        for (v1a = 0; v1a < DNCnta; v1a++)
        {
            arrayPO_CREATE_MTa[v1a + 1, 0] = (v1a + 1).ToString();
            arrayPO_CREATE_MTa[v1a + 1, 1] = DNdta.Tables[0].Rows[v1a]["ID"].ToString().Trim();
            arrayPO_CREATE_MTa[v1a + 1, 2] = DNdta.Tables[0].Rows[v1a]["POID"].ToString().Trim();
            arrayPO_CREATE_MTa[v1a + 1, 3] = DNdta.Tables[0].Rows[v1a]["ReceiveTime"].ToString().Trim();
            arrayPO_CREATE_MTa[v1a + 1, 4] = ""; // Detail finsih count
            arrayPO_CREATE_MTa[v1a + 1, 5] = ""; // Detail not finsih count
            arrayPO_CREATE_MTa[v1a + 1, 6] = ""; // PO_CONFIRMATION_MT finsih count 
            arrayPO_CREATE_MTa[v1a + 1, 7] = ""; // PO_CONFIRMATION_MT not finsih count
            arrayPO_CREATE_MTa[v1a + 1, 8] = ""; // Detail = PO_CONFIRMATION_MT
            arrayPO_CREATE_MTa[v1a + 1, 9] = ""; // Last Confirm status update 
        }

        v8a = DNCnta;

        for (v1a = 0; v1a < v8a; v1a++)
        {
            v2a = 0;
            tmp1a = arrayPO_CREATE_MTa[v1a + 1, 1]; // ID
            sqlra = " SELECT count(*) NoFinishCNT from PO_CHANGE_DT where ( CONFIRMFLAG = 'Y' ) and ID = '" + tmp1a + "' ";
            DNdta = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlra);
            if (DNdta == null) return ("-1"); // Syn Error
            if (DNdta.Tables.Count<=0) return ("-1");
            DNCnta = DNdta.Tables[0].Rows.Count;
            if (DNCnta == 0) v2a = 0;
            else v2a = Convert.ToInt32(DNdta.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MTa[v1a + 1, 4] = v2a.ToString(); // Record Not-finsh count 

            v2a = 0;
            tmp1a = arrayPO_CREATE_MTa[v1a + 1, 1]; // ID
            sqlra = " SELECT count(*) NoFinishCNT from PO_CHANGE_DT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) and ID = '" + tmp1a + "'";
            DNdta = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlra);
            if (DNdta == null) return ("-1"); // Syn Error
            DNCnta = DNdta.Tables[0].Rows.Count;
            if (DNCnta == 0) v2a = 0;
            else v2a = Convert.ToInt32(DNdta.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MTa[v1a + 1, 5] = v2a.ToString(); // Record Not-finsh count 

            v2a = 0;
            tmp1a = arrayPO_CREATE_MTa[v1a + 1, 1]; // ID
            sqlra = " SELECT count(*) NoFinishCNT from PO_CONFIRMATION_MT where ( CONFIRMFLAG = 'Y' ) and ID = '" + tmp1a + "'";
            DNdta = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlra);
            if (DNdta == null) return ("-1"); // Syn Error
            DNCnta = DNdta.Tables[0].Rows.Count;
            if (DNCnta == 0) v2a = 0;
            else v2a = Convert.ToInt32(DNdta.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MTa[v1a + 1, 6] = v2a.ToString(); // Record Not-finsh count 

            v2a = 0;
            tmp1a = arrayPO_CREATE_MTa[v1a + 1, 1]; // ID
            sqlra = " SELECT count(*) NoFinishCNT from PO_CONFIRMATION_MT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) and ID = '" + tmp1a + "'";
            DNdta = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlra);
            if (DNdta == null) return ("-1"); // Syn Error
            DNCnta = DNdta.Tables[0].Rows.Count;
            if (DNCnta == 0) v2a = 0;
            else v2a = Convert.ToInt32(DNdta.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MTa[v1a + 1, 7] = v2a.ToString(); // Record Not-finsh count 


        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Check by each ID if Detail finish > 0 and fail = 0 and Confirmation finish > 0 and fail = 0 
        // Rewrite Master CONFIRMFLAG = "Y"
        for (v1a = 0; v1a < v8a; v1a++)
        {
            tmp1a = arrayPO_CREATE_MTa[v1a + 1, 1].ToString().Trim();
            v4a = Convert.ToInt32(arrayPO_CREATE_MTa[v1a + 1, 4]); // Detail finsih count
            v5a = Convert.ToInt32(arrayPO_CREATE_MTa[v1a + 1, 5]); // Detail not finsih count
            v6a = Convert.ToInt32(arrayPO_CREATE_MTa[v1a + 1, 6]); // PO_CONFIRMATION_MT finsih count 
            v7a = Convert.ToInt32(arrayPO_CREATE_MTa[v1a + 1, 7]); // PO_CONFIRMATION_MT not finsih count
            // v8 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 8]); // Detail = PO_CONFIRMATION_MT
            // v9 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 9]); // Last Confirm status update 

            if ((v4a > 0) && (v5a == 0) && (v6a > 0) && (v7a == 0))
            {
                tmp2a = "Y";
                tmpsqlWa = "Update PO_CHANGE_MT set CONFIRMFLAG = '" + tmp2a + "' where ID = '" + tmp1a + "' ";
                v2a = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlWa);
                if (v2a < 0) // Successed, It could be not data
                    v3a++;
            } // End Write 
        }


        return ("");
    }

    //20141129--update
    //5 field--CostAmount,INTERNALID,IncoTermsCode,IncoTermsName,OriginID,DELIVERYSTARTDT,SCHEDULEQUANTITY
    // 20140920
    // 因須判斷  PO_CREATE_DT, PO_CHANGE_DT 做 2 次
    // Check POCNT 聯續性就刪除
    // 20141122 modify
    // Sort PO_CREATE_DT + PO_CHANGE_DT order by POCNT asc
    // Check some POCNT if 5 fields is same then 
    //                     if CONFIRMFLAG <> 'Y'  delete big POCNT = "D"
    // 相同, 第2筆若 CONFIRM="Y", 不動, 其他更新為 "N"
    public string AutoCheckPOCHANGE(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string BBSCMDIR)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0, v9 = 0, v10 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");
        string DBType = DbSql;
        string BBSCMD = "[" + BBSCMDIR + "]" + "." + "[dbo]" + ".";  // [BBSCM].[dbo].PO_CHANGE_DT
        string YFlag = "Y";


        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        //////////////////////////////////////////////////////////////////
        // Setup PO_CHANGE_DT_UF6 = "Y" if CONFIRMFLAG = 'D', 'C'. 'Y' 
        t21 = "Y";

        //tmpsqlW = "Update  " + BBSCMD + "PO_CHANGE_DT set PO_CHANGE_DT_UF6 = '" + t21 + "' where  "
        //        + " ( CONFIRMFLAG != '' or   CONFIRMFLAG != null ) and ( PO_CHANGE_DT_UF6 = ''  or  PO_CHANGE_DT_UF6 is null )   ";
        //v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        ///////////////////////////////////////////////////////////////////////////////
        // SELECT      [CostAmount] (單價)
        //             ,[INTERNALID]  (料號)
        //            ,[IncoTermsCode] (交易條款)
        //            ,[IncoTermsName]
        //             ,[OriginID] 
        //           ,[DELIVERYSTARTDT] (交貨時間）
        //          ,[SCHEDULEQUANTITY]（數量）
        //          FROM [BBSCM].[dbo].[PO_CHANGE_DT] == "Y"
        // 

        sqlr = " SELECT * FROM " + BBSCMD + "PO_CREATE_DT where ( CONFIRMFLAG is null or CONFIRMFLAG = '' or CONFIRMFLAG = 'Y') and POID='20000914' "
               + " union            SELECT * FROM " + BBSCMD + "PO_CHANGE_DT where ( CONFIRMFLAG is null or CONFIRMFLAG = '' or CONFIRMFLAG = 'Y' ) and POID='20000914'"
            // + " and ( PO_CHANGE_DT_UF6 = '' or PO_CHANGE_DT_UF6 is null ) "
               + " order by POID, ITEMID, POCNT asc  ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if ((DNdt == null) || (DNdt.Tables.Count <= 0)) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt <= 1) return ("0"); // Not Data

        string[,] arrayPO_CREATE_MT = new string[DNCnt + 1, 15 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayPO_CREATE_MT[v1, v2] = "";


        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayPO_CREATE_MT[v1 + 1, 0] = (v1 + 1).ToString();
            arrayPO_CREATE_MT[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["POID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["ITEMID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 4] = DNdt.Tables[0].Rows[v1]["CostAmount"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["INTERNALID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["IncoTermsCode"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["IncoTermsName"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["OriginID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["DELIVERYSTARTDT"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 10] = DNdt.Tables[0].Rows[v1]["SCHEDULEQUANTITY"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 11] = DNdt.Tables[0].Rows[v1]["POCNT"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 12] = DNdt.Tables[0].Rows[v1]["CONFIRMFLAG"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 13] = DNdt.Tables[0].Rows[v1]["AMOUNT"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 14] = "";
        }

        v8 = DNCnt;

        int arrcnt1 = 1, arrcnt2 = 2;
        string ERRCode = "E2";

        while (arrcnt2 <= v8)
        {
            v1 = arrcnt1;
            v2 = arrcnt2;
            t01 = arrayPO_CREATE_MT[v1, 1].ToString().Trim();
            t02 = arrayPO_CREATE_MT[v1, 2].ToString().Trim();
            t03 = arrayPO_CREATE_MT[v1, 3].ToString().Trim();
            t04 = arrayPO_CREATE_MT[v1, 4].ToString().Trim();
            t05 = arrayPO_CREATE_MT[v1, 5].ToString().Trim();
            t06 = arrayPO_CREATE_MT[v1, 6].ToString().Trim();
            t07 = arrayPO_CREATE_MT[v1, 7].ToString().Trim();
            t08 = arrayPO_CREATE_MT[v1, 8].ToString().Trim();
            t09 = arrayPO_CREATE_MT[v1, 9].ToString().Trim();
            t10 = arrayPO_CREATE_MT[v1, 10].ToString().Trim();
            t11 = arrayPO_CREATE_MT[v1, 11].ToString().Trim();
            t12 = arrayPO_CREATE_MT[v1, 12].ToString().Trim();
            t13 = arrayPO_CREATE_MT[v1, 13].ToString().Trim();

            string tt01 = "", tt02 = "", tt03 = "", tt04 = "", tt05 = "", tt06 = "", tt07 = "", tt08 = "", tt09 = "", tt10 = "", tt11 = "", tt12 = "", tt13 = "";
            tt01 = arrayPO_CREATE_MT[v2, 1].ToString().Trim();
            tt02 = arrayPO_CREATE_MT[v2, 2].ToString().Trim();
            tt03 = arrayPO_CREATE_MT[v2, 3].ToString().Trim();
            tt04 = arrayPO_CREATE_MT[v2, 4].ToString().Trim();
            tt05 = arrayPO_CREATE_MT[v2, 5].ToString().Trim();
            tt06 = arrayPO_CREATE_MT[v2, 6].ToString().Trim();
            tt07 = arrayPO_CREATE_MT[v2, 7].ToString().Trim();
            tt08 = arrayPO_CREATE_MT[v2, 8].ToString().Trim();
            tt09 = arrayPO_CREATE_MT[v2, 9].ToString().Trim();
            tt10 = arrayPO_CREATE_MT[v2, 10].ToString().Trim();
            tt11 = arrayPO_CREATE_MT[v2, 11].ToString().Trim();
            tt12 = arrayPO_CREATE_MT[v2, 12].ToString().Trim();
            tt13 = arrayPO_CREATE_MT[v2, 13].ToString().Trim();

            /////////////////////////////////////////////////////////////
            // Algorithm"
            // 1. arrcnt1 = arrcn2  PO, ITEM Part 表示同訂單, 後面更新
            // else
            // 2. 表示資料重覆
            //
            //if ((arrayPO_CREATE_MT[v1, 2].ToString().Trim() == arrayPO_CREATE_MT[v2, 2].ToString().Trim()) &&
            //   (arrayPO_CREATE_MT[v1, 3].ToString().Trim() == arrayPO_CREATE_MT[v2, 3].ToString().Trim()) &&
            //   (arrayPO_CREATE_MT[v1, 5].ToString().Trim() == arrayPO_CREATE_MT[v2, 5].ToString().Trim()))
            //{  // 同一料號, 訂單, 數量
            //    if ((arrayPO_CREATE_MT[v1, 2].ToString().Trim() == arrayPO_CREATE_MT[v2, 2].ToString().Trim()) &&
            //    (arrayPO_CREATE_MT[v1, 3].ToString().Trim() == arrayPO_CREATE_MT[v2, 3].ToString().Trim()) &&
            //    (arrayPO_CREATE_MT[v1, 5].ToString().Trim() == arrayPO_CREATE_MT[v2, 5].ToString().Trim()) &&
            //    (arrayPO_CREATE_MT[v1, 6].ToString().Trim() == arrayPO_CREATE_MT[v2, 6].ToString().Trim()) &&
            //    (arrayPO_CREATE_MT[v1, 7].ToString().Trim() == arrayPO_CREATE_MT[v2, 7].ToString().Trim()) &&
            //    (arrayPO_CREATE_MT[v1, 8].ToString().Trim() == arrayPO_CREATE_MT[v2, 8].ToString().Trim()) &&
            //    (arrayPO_CREATE_MT[v1, 9].ToString().Trim() == arrayPO_CREATE_MT[v2, 9].ToString().Trim()) &&
            //    (arrayPO_CREATE_MT[v1, 10].ToString().Trim() == arrayPO_CREATE_MT[v1 + 2, 10].ToString().Trim()))
            if ((t02.ToString().Trim() == tt02.ToString().Trim()) &&
               (t03.ToString().Trim() == tt03.ToString().Trim()) &&
               (t05.ToString().Trim() == tt05.ToString().Trim()))
            {  // 同一料號, 訂單, 數量
                if ((t02.ToString().Trim() == tt02.ToString().Trim()) &&
                (t03.ToString().Trim() == tt03.ToString().Trim()) &&
                (t05.ToString().Trim() == tt05.ToString().Trim()) &&
                (t06.ToString().Trim() == tt06.ToString().Trim()) &&
                (t07.ToString().Trim() == tt07.ToString().Trim()) &&
                (t08.ToString().Trim() == tt08.ToString().Trim()) &&
                (t09.ToString().Trim() == tt09.ToString().Trim()) &&
                (t10.ToString().Trim() == tt10.ToString().Trim()) &&
                (t13.ToString().Trim() == tt13.ToString().Trim()))
                {   // 同訂單數量, 刪後面, 也可刪後面
                    tmp1 = arrayPO_CREATE_MT[v1, 12].ToString().Trim();  // arrcnt1
                    tmp2 = arrayPO_CREATE_MT[v2, 12].ToString().Trim();  // arrcnt2

                    if ((tmp1 == "") && (tmp2 == ""))
                    {
                        tmp3 = tmp2;  // acrrcnt1 = "E"
                        tmp3 = UpdateConfirmflag(DbSql, DBReadString, DBWriString, arrayPO_CREATE_MT[v2, 11], ERRCode, arrayPO_CREATE_MT[v2, 1], arrayPO_CREATE_MT[v2, 2], arrayPO_CREATE_MT[v2, 3], arrayPO_CREATE_MT[v2, 5], BBSCMDIR);
                        // arrcnt1 = arrcnt2;
                        arrcnt2++;
                    }
                    else
                        if ((tmp1 != "") && (tmp2 != ""))
                        {
                            tmp3 = "";
                            arrcnt1 = arrcnt2;
                            arrcnt2++;
                        }
                        else
                            if ((tmp1 == "Y") && (tmp2 == ""))
                            {
                                tmp3 = tmp2; // skin
                                tmp3 = UpdateConfirmflag(DbSql, DBReadString, DBWriString, arrayPO_CREATE_MT[v2, 11], ERRCode, arrayPO_CREATE_MT[v2, 1], arrayPO_CREATE_MT[v2, 2], arrayPO_CREATE_MT[v2, 3], arrayPO_CREATE_MT[v2, 5], BBSCMDIR);
                                // arrcnt1 = arrcnt2;
                                arrcnt2++;

                            }
                            else
                                if ((tmp1 == "") && (tmp2 == "Y"))
                                {
                                    tmp3 = tmp1; // Update
                                    tmp3 = UpdateConfirmflag(DbSql, DBReadString, DBWriString, arrayPO_CREATE_MT[v1, 11], ERRCode, arrayPO_CREATE_MT[v1, 1], arrayPO_CREATE_MT[v1, 2], arrayPO_CREATE_MT[v1, 3], arrayPO_CREATE_MT[v1, 5], BBSCMDIR);
                                    arrcnt1 = arrcnt2;
                                    arrcnt2++;
                                }
                }


                else
                // PO 相同, 數量不同, 表 PO 更新資料, 刪前面
                {
                    tmp1 = arrayPO_CREATE_MT[v1, 12].ToString().Trim();  // arrcnt1
                    tmp2 = arrayPO_CREATE_MT[v2, 12].ToString().Trim();  // arrcnt2

                    if ((tmp1 == "") && (tmp2 == ""))
                    {
                        tmp3 = tmp1;  // acrrcnt1 = "E"
                        tmp3 = UpdateConfirmflag(DbSql, DBReadString, DBWriString, arrayPO_CREATE_MT[v1, 11], ERRCode, arrayPO_CREATE_MT[v1, 1], arrayPO_CREATE_MT[v1, 2], arrayPO_CREATE_MT[v1, 3], arrayPO_CREATE_MT[v1, 5], BBSCMDIR);
                        arrcnt1 = arrcnt2;
                        arrcnt2++;
                    }
                    else
                        if ((tmp1 != "") && (tmp2 != ""))
                        {
                            tmp3 = "";
                            arrcnt1 = arrcnt2;
                            arrcnt2++;
                        }
                        else
                            if ((tmp1 == "Y") && (tmp2 == ""))
                            {
                                //tmp3 = tmp2; // skin
                                //tmp3 = UpdateConfirmflag(DbSql, DBReadString, DBWriString, arrayPO_CREATE_MT[v2, 11], ERRCode, arrayPO_CREATE_MT[v2, 1], arrayPO_CREATE_MT[v2, 2], arrayPO_CREATE_MT[v2, 3], arrayPO_CREATE_MT[v2, 5], BBSCMDIR);
                                //// arrcnt1 = arrcnt2;
                                tmp3 = "";
                                arrcnt1 = arrcnt2;
                                arrcnt2++;

                            }
                            else
                                if ((tmp1 == "") && (tmp2 == "Y"))
                                {
                                    tmp3 = tmp1; // Update
                                    tmp3 = UpdateConfirmflag(DbSql, DBReadString, DBWriString, arrayPO_CREATE_MT[v1, 11], ERRCode, arrayPO_CREATE_MT[v1, 1], arrayPO_CREATE_MT[v1, 2], arrayPO_CREATE_MT[v1, 3], arrayPO_CREATE_MT[v1, 5], BBSCMDIR);
                                    arrcnt1 = arrcnt2;
                                    arrcnt2++;
                                }
                }

            } // 同訂單完畢, 不同不須比較
            else
            {
                arrcnt1 = arrcnt2;
                arrcnt2++;
            }
        }  // End DO while

        return ("");


    } // end AutoCheckPOCHANGE

    // tmp3 = UpdateConfirmflag(DbSql, DBReadString, DBWriString, arrayPO_CREATE_MT[v2, 11], "E1", arrayPO_CREATE_MT[v2, 1], arrayPO_CREATE_MT[v2, 2], arrayPO_CREATE_MT[v2, 3], arrayPO_CREATE_MT[v2, 5]);
    private string UpdateConfirmflag(string DBType, string DBReadString, string DBWriString, string tmpPOCNT, string Updateflag, string tmpID, string tmpPOID, string tmpITEMID, string tmpINTERNALID, string BBSCMDIR)
    {
        int v1 = 0, v2 = 0, v3 = 0;
        string tmpsqlW = "";

        string t01 = tmpID;
        string t02 = tmpPOID;
        string t03 = tmpITEMID;
        string t05 = tmpINTERNALID;
        string BBSCMD = "[" + BBSCMDIR + "]" + "." + "[dbo]" + ".";


        if (tmpPOCNT == "1")
        {
            tmpsqlW = "Update " + BBSCMD + "PO_CREATE_DT set CONFIRMFLAG = '" + Updateflag + "'  where ID = '" + t01 + "' and "
               + " POID = '" + t02 + "' and ITEMID = '" + t03 + "' and INTERNALID = '" + t05 + "' and  "
               + " ( CONFIRMFLAG = '' or CONFIRMFLAG is null ) ";
        }
        else
        {
            tmpsqlW = "Update " + BBSCMD + "PO_CHANGE_DT set CONFIRMFLAG = '" + Updateflag + "'  where ID = '" + t01 + "' and "
               + " POID = '" + t02 + "' and ITEMID = '" + t03 + "' and INTERNALID = '" + t05 + "' and  "
               + " ( CONFIRMFLAG = '' or CONFIRMFLAG is null ) ";
        }
        v3 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        return ("");
    }
    // 20140920
    // 因須判斷  PO_CREATE_DT, PO_CHANGE_DT 做 2 次
    // Check POCNT 聯續性就刪除
    public string OLDAutoCheckPOCHANGE(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string BBSCMDIR)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0, v9 = 0, v10 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");
        string DBType = DbSql;
        string BBSCMD = "[" + BBSCMDIR + "]" + "." + "[dbo]" + ".";  // [BBSCM].[dbo].PO_CHANGE_DT
        string YFlag = "Y";


        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        //////////////////////////////////////////////////////////////////
        // Setup PO_CHANGE_DT_UF6 = "Y" if CONFIRMFLAG = 'D', 'C'. 'Y' 
        t21 = "Y";

        //tmpsqlW = "Update  " + BBSCMD + "PO_CHANGE_DT set PO_CHANGE_DT_UF6 = '" + t21 + "' where  "
        //        + " ( CONFIRMFLAG != '' or   CONFIRMFLAG != null ) and ( PO_CHANGE_DT_UF6 = ''  or  PO_CHANGE_DT_UF6 is null )   ";
        //v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

        ///////////////////////////////////////////////////////////////////////////////
        // SELECT      [CostAmount] (單價)
        //             ,[INTERNALID]  (料號)
        //            ,[IncoTermsCode] (交易條款)
        //            ,[IncoTermsName]
        //             ,[OriginID] 
        //           ,[DELIVERYSTARTDT] (交貨時間）
        //          ,[SCHEDULEQUANTITY]（數量）
        //          FROM [BBSCM].[dbo].[PO_CHANGE_DT] == "Y"
        // 

        sqlr = " SELECT * from " + BBSCMD + "PO_CHANGE_DT where ( PO_CHANGE_DT_UF6 = '' or PO_CHANGE_DT_UF6 is null ) order by OriginID + POCNT asc ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0"); // Not Data

        string[,] arrayPO_CREATE_MT = new string[DNCnt + 1, 15 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayPO_CREATE_MT[v1, v2] = "";

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayPO_CREATE_MT[v1 + 1, 0] = (v1 + 1).ToString();
            arrayPO_CREATE_MT[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["POID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["ITEMID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 4] = DNdt.Tables[0].Rows[v1]["CostAmount"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["INTERNALID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["IncoTermsCode"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["IncoTermsName"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["OriginID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["DELIVERYSTARTDT"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 10] = DNdt.Tables[0].Rows[v1]["SCHEDULEQUANTITY"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 11] = DNdt.Tables[0].Rows[v1]["POCNT"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 12] = DNdt.Tables[0].Rows[v1]["CONFIRMFLAG"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 13] = "";
        }

        v8 = DNCnt;

        for (v1 = 0; v1 < v8; v1++)
        {
            v2 = 0;

            t01 = arrayPO_CREATE_MT[v1 + 1, 1].ToString().Trim();
            t02 = arrayPO_CREATE_MT[v1 + 1, 2].ToString().Trim();
            t03 = arrayPO_CREATE_MT[v1 + 1, 3].ToString().Trim();
            t04 = arrayPO_CREATE_MT[v1 + 1, 4].ToString().Trim();
            t05 = arrayPO_CREATE_MT[v1 + 1, 5].ToString().Trim();
            t06 = arrayPO_CREATE_MT[v1 + 1, 6].ToString().Trim();
            t07 = arrayPO_CREATE_MT[v1 + 1, 7].ToString().Trim();
            t08 = arrayPO_CREATE_MT[v1 + 1, 8].ToString().Trim();
            t09 = arrayPO_CREATE_MT[v1 + 1, 9].ToString().Trim();
            t10 = arrayPO_CREATE_MT[v1 + 1, 10].ToString().Trim();
            t11 = arrayPO_CREATE_MT[v1 + 1, 11].ToString().Trim();
            t12 = arrayPO_CREATE_MT[v1 + 1, 12].ToString().Trim();


            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            //  sqlr = " SELECT count(*) NoFinishCNT from [BBSCM].[dbo].PO_CREATE_DT where CostAmount = '" + t04 + "' and "
            //       + " INTERNALID =  '" + t05 + "'  and  IncoTermsCode = '" + t06 + "'                and "
            //       + " IncoTermsName =  '" + t07 + "'  and  OriginID = '" + t08 + "'                  and "
            //       + " DELIVERYSTARTDT =  '" + t09 + "'  and  SCHEDULEQUANTITY = '" + t10 + "'    ";
            //  DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            //  if (DNdt == null) return ("-1"); // Syn Error
            //  DNCnt = DNdt.Tables[0].Rows.Count;
            //  if (DNCnt == 0) v2 = 0;
            //  else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            t22 = ""; t21 = ""; v3 = 0;

            if ((t11 == "") || (t11 is Nullable)) v9 = 0;
            else v9 = Convert.ToInt32(t11);

            v9 = v9 - 1;  // 減為前一編號表重覆
            tmp6 = v9.ToString();

            tmp7 = ""; // buffer create.CONFIRMFLAG
            sqlr = " SELECT * from " + BBSCMD + "PO_CREATE_DT where CostAmount = '" + t04 + "' and "
                 + " INTERNALID =  '" + t05 + "'  and  IncoTermsCode = '" + t06 + "'                and "
                 + " IncoTermsName =  '" + t07 + "'  and  OriginID = '" + t08 + "'                  and "
                 + " DELIVERYSTARTDT =  '" + t09 + "'  and  SCHEDULEQUANTITY = '" + t10 + "'        and "
                 + " ( POCNT =  '" + tmp6 + "' )                                                                                    ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else
            {
                v2 = DNdt.Tables[0].Rows.Count;
                tmp6 = DNdt.Tables[0].Rows[0]["POCNT"].ToString().Trim();
                tmp7 = DNdt.Tables[0].Rows[0]["CONFIRMFLAG"].ToString().Trim();
                t22 = "D";
                arrayPO_CREATE_MT[v1 + 1, 13] = tmp7 + tmp6; // Record Not-finsh count    
            }

            if (t22 != "D")
            {
                tmp6 = v9.ToString();
                sqlr = " SELECT * from " + BBSCMD + "PO_CHANGE_DT where CostAmount = '" + t04 + "' and "
                + " INTERNALID =  '" + t05 + "'  and  IncoTermsCode = '" + t06 + "'                and "
                + " IncoTermsName =  '" + t07 + "'  and  OriginID = '" + t08 + "'                  and "
                + " DELIVERYSTARTDT =  '" + t09 + "'  and  SCHEDULEQUANTITY = '" + t10 + "'        and "
                + " ( POCNT =  '" + tmp6 + "' )                                                                                    ";
                DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
                if (DNdt == null) return ("-1"); // Syn Error
                DNCnt = DNdt.Tables[0].Rows.Count;
                if (DNCnt == 0) v2 = 0;
                else
                {
                    v2 = DNdt.Tables[0].Rows.Count;
                    tmp6 = DNdt.Tables[0].Rows[0]["POCNT"].ToString().Trim();
                    tmp7 = DNdt.Tables[0].Rows[0]["CONFIRMFLAG"].ToString().Trim();
                    t22 = "D";
                    arrayPO_CREATE_MT[v1 + 1, 13] = tmp7 + tmp6; // Record Not-finsh count    
                }

            }

            if (t22 == "D")  // 重覆更新 將 CREATE-DT CONFIRMFLAG + POCNT 寫到 CHANGE UF7
            {
                if ((t12 == "") || (t12 == null)) // CONFIRMFLAG 已有值
                {
                    tmpsqlW = "Update " + BBSCMD + "PO_CHANGE_DT set CONFIRMFLAG = '" + t22 + "',  PO_CHANGE_DT_UF6 = '" + YFlag + "', "
                       + " PO_CHANGE_DT_UF7 = '" + (tmp7 + tmp6) + "'  where ID = '" + t01 + "' and "
                       + " POID = '" + t02 + "' and ITEMID = '" + t03 + "' and INTERNALID = '" + t05 + "' and  "
                       + " ( CONFIRMFLAG = '' or CONFIRMFLAG is null ) ";
                }
                else
                {
                    tmpsqlW = "Update " + BBSCMD + "PO_CHANGE_DT set PO_CHANGE_DT_UF6 = '" + YFlag + "', "
                              + " PO_CHANGE_DT_UF7 = '" + (tmp7 + tmp6) + "'  where ID = '" + t01 + "' and "
                              + " POID = '" + t02 + "' and ITEMID = '" + t03 + "' and INTERNALID = '" + t05 + "'  ";
                }
                v3 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                v4++;
            }
            else
            {
                tmpsqlW = "Update " + BBSCMD + ".[dbo].PO_CHANGE_DT set PO_CHANGE_DT_UF6 = '" + YFlag + "' where ID = '" + t01 + "' and "
                  + " POID = '" + t02 + "' and ITEMID = '" + t03 + "' and INTERNALID = '" + t05 + "'  ";
                v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                v5++;


            }




        }  // end for

        return ("");

    } // end AutoCheckPOCHANGE

    public string AutoCheckPOCRAETE1(string Systype, string DbSql, string DBReadString, string DBWriString, string Actiontype, string SysDate, string BBSCMDIR)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0, v9 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        ///////////////////////////////////////////////////////////////////////////////
        // if ( PO_CREATE_MT CONFIRMATIONFLAG <> "Y" ) CHeck By this ID
        // {
        // if ( ID in PO_CREATE_DT == "Y" ) and PO_CONFIRMATION == "Y" )
        // if ( ID in PO_CREATE_DT QTY == SUM PO_CONFIRMATION ) 
        // PO_CONFIRMATIONFLAG == "Y"
        // }
        string DBType = DbSql;

        sqlr = " SELECT * from PO_CREATE_MT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) order by ID desc ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0"); // Not Data

        string[,] arrayPO_CREATE_MT = new string[DNCnt + 1, 15 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayPO_CREATE_MT[v1, v2] = "";

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayPO_CREATE_MT[v1 + 1, 0] = (v1 + 1).ToString();
            arrayPO_CREATE_MT[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["POID"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["ReceiveTime"].ToString().Trim();
            arrayPO_CREATE_MT[v1 + 1, 4] = ""; // Detail finsih count
            arrayPO_CREATE_MT[v1 + 1, 5] = ""; // Detail not finsih count
            arrayPO_CREATE_MT[v1 + 1, 6] = ""; // PO_CONFIRMATION_MT finsih count 
            arrayPO_CREATE_MT[v1 + 1, 7] = ""; // PO_CONFIRMATION_MT not finsih count
            arrayPO_CREATE_MT[v1 + 1, 8] = ""; // Detail = PO_CONFIRMATION_MT
            arrayPO_CREATE_MT[v1 + 1, 9] = ""; // Last Confirm status update 
        }

        v8 = DNCnt;

        for (v1 = 0; v1 < v8; v1++)
        {
            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CREATE_DT where ( CONFIRMFLAG = 'Y' ) and ID = '" + tmp1 + "' ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 4] = v2.ToString(); // Record Not-finsh count 

            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CREATE_DT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) and ID = '" + tmp1 + "'";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 5] = v2.ToString(); // Record Not-finsh count 

            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CONFIRMATION_MT where ( CONFIRMFLAG = 'Y' ) and ID = '" + tmp1 + "'";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 6] = v2.ToString(); // Record Not-finsh count 

            v2 = 0;
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1]; // ID
            sqlr = " SELECT count(*) NoFinishCNT from PO_CONFIRMATION_MT where ( CONFIRMFLAG <> 'Y' or CONFIRMFLAG is null ) and ID = '" + tmp1 + "'";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) v2 = 0;
            else v2 = Convert.ToInt32(DNdt.Tables[0].Rows[0]["NoFinishCNT"].ToString().Trim());
            arrayPO_CREATE_MT[v1 + 1, 7] = v2.ToString(); // Record Not-finsh count 


        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Check by each ID if Detail finish > 0 and fail = 0 and Confirmation finish > 0 and fail = 0 
        // Rewrite Master CONFIRMFLAG = "Y"
        for (v1 = 0; v1 < v8; v1++)
        {
            tmp1 = arrayPO_CREATE_MT[v1 + 1, 1].ToString().Trim();
            v4 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 4]); // Detail finsih count
            v5 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 5]); // Detail not finsih count
            v6 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 6]); // PO_CONFIRMATION_MT finsih count 
            v7 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 7]); // PO_CONFIRMATION_MT not finsih count
            // v8 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 8]); // Detail = PO_CONFIRMATION_MT
            // v9 = Convert.ToInt32(arrayPO_CREATE_MT[v1 + 1, 9]); // Last Confirm status update 

            if ((v4 > 0) && (v5 == 0) && (v6 > 0) && (v7 == 0))
            {
                tmp2 = "Y";
                tmpsqlW = "Update PO_CREATE_MT set CONFIRMFLAG = '" + tmp2 + "' where ID = '" + tmp1 + "' ";
                v2 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                if (v2 < 0) // Successed, It could be not data
                    v3++;
            } // End Write 
        }

        return ("");
    }

    public string ConvertBOM2(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null, dt4 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0, v8 = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);
        //tmp1 = "TOP";
        //sqlr = " update BOMTXT set DataLevel = '00' where (  ( DataLevel = 'TOP' )  and ( flag is null )) ";
        //v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //if (v4 > 0) // Successed
        //    v4 = v4;

    //    sqlr = "  SELECT  * from BOMTXT  where ( ( substring(CDATE,1,6) = '" + ChkDate + "' ) and  ( flag is null ) ) order by DataLevel asc ";
        sqlr = "  SELECT  * from BOMTXT  where ( ( substring(CDATE,1,6) = '" + ChkDate + "' ) and ( flag is null  ) and ( DataLevel != '' ) ) order by DataLevel asc ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);       
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0");     // Not Data

        string[,] arrayBOM1 = new string[DNCnt + 1, 50 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayBOM1[v1, v2] = "";

        int MaxItem = 50, ItemLen = 0;
        string[,] arrayBOM1Item = new string[MaxItem + 1, 50 + 1];
        for (v1 = 0; v1 <= MaxItem; v1++)
            for (v2 = 0; v2 <= 50; v2++)
                arrayBOM1[v1, v2] = "";

        ///////////////////////////////////////////////////////
        // 放上階臨時表, 每次須更新, from array 0
        string[,] arrayLevel = new string[50 + 1, 3 + 1];
        for (v1 = 0; v1 <= 50; v1++)
            for (v2 = 0; v2 <= 3; v2++)
                arrayLevel[v1, v2] = "";

        string PPart = "", CPart = "";

        v3 = 0; // Length
        v4 = 0;
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayBOM1[v1 + 1, 0] = (v1 + 1).ToString();
            arrayBOM1[v1 + 1, 1] = ChkDate; // Chech Date yyyymm
            arrayBOM1[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["CDATE"].ToString().Trim();
            arrayBOM1[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            arrayBOM1[v1 + 1, 4] = DNdt.Tables[0].Rows[v1]["DataLevel"].ToString().Trim();
            arrayBOM1[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["LineID"].ToString().Trim();
            arrayBOM1[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["Pri"].ToString().Trim();
            arrayBOM1[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["WT"].ToString().Trim();
            arrayBOM1[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["Part_Source"].ToString().Trim();
            arrayBOM1[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["Phan_Part"].ToString().Trim();
            arrayBOM1[v1 + 1, 10] = DNdt.Tables[0].Rows[v1]["Outsourcing"].ToString().Trim();
            arrayBOM1[v1 + 1, 11] = DNdt.Tables[0].Rows[v1]["H_H_PN"].ToString().Trim();
            arrayBOM1[v1 + 1, 12] = DNdt.Tables[0].Rows[v1]["H_H_PN_Ver"].ToString().Trim();
            arrayBOM1[v1 + 1, 13] = DNdt.Tables[0].Rows[v1]["Cus_PN"].ToString().Trim();
            arrayBOM1[v1 + 1, 14] = DNdt.Tables[0].Rows[v1]["Cus_Ver"].ToString().Trim();
            arrayBOM1[v1 + 1, 15] = DNdt.Tables[0].Rows[v1]["QTY"].ToString().Trim();
            arrayBOM1[v1 + 1, 16] = DNdt.Tables[0].Rows[v1]["Unit"].ToString().Trim();
            arrayBOM1[v1 + 1, 17] = DNdt.Tables[0].Rows[v1]["Part_Type"].ToString().Trim();
            arrayBOM1[v1 + 1, 18] = DNdt.Tables[0].Rows[v1]["Dely_Part"].ToString().Trim();
            arrayBOM1[v1 + 1, 19] = DNdt.Tables[0].Rows[v1]["English_Name"].ToString().Trim();
            arrayBOM1[v1 + 1, 20] = DNdt.Tables[0].Rows[v1]["Chinese_Name"].ToString().Trim();
            arrayBOM1[v1 + 1, 21] = DNdt.Tables[0].Rows[v1]["Process"].ToString().Trim();
            arrayBOM1[v1 + 1, 22] = ""; // Parent ITEM
            arrayBOM1[v1 + 1, 41] = ""; // Parent ITEM Ver
            arrayBOM1[v1 + 1, 42] = ""; // Parent ITEM + Ver
            arrayBOM1[v1 + 1, 23] = DNdt.Tables[0].Rows[v1]["Op_Scrap"].ToString().Trim();
            arrayBOM1[v1 + 1, 24] = DNdt.Tables[0].Rows[v1]["Comp_Scrap"].ToString().Trim();
            arrayBOM1[v1 + 1, 25] = DNdt.Tables[0].Rows[v1]["Target_Op_Scrap"].ToString().Trim();
            arrayBOM1[v1 + 1, 26] = DNdt.Tables[0].Rows[v1]["Change_Description"].ToString().Trim();
            arrayBOM1[v1 + 1, 27] = DNdt.Tables[0].Rows[v1]["ABC_Indicator"].ToString().Trim();
            arrayBOM1[v1 + 1, 28] = DNdt.Tables[0].Rows[v1]["RunningDay"].ToString().Trim();
            arrayBOM1[v1 + 1, 29] = ""; //  DNdt.Tables[0].Rows[v1]["Flag"].ToString().Trim();   phantom
            arrayBOM1[v1 + 1, 30] = "11"; //  Phantom, 00, 01, 10, 11
            arrayBOM1[v1 + 1, 31] = DNdt.Tables[0].Rows[v1]["DataLevel"].ToString().Trim();
            arrayBOM1[v1 + 1, 37] = DNdt.Tables[0].Rows[v1]["H_H_PN"].ToString().Trim() + "V" + DNdt.Tables[0].Rows[v1]["H_H_PN_Ver"].ToString().Trim(); // Bom+Ver
            //  Parent   Item     = 22      Child   Item = 11 
            //           Ver      = 41              Ver  = 12
            //           Item+ver = 42              Item+Ver = 37 
            if (arrayBOM1[v1 + 1, 31] != "")
            {
                tmp2 = arrayBOM1[v1 + 1, 31].ToString().Trim();
                v3 = (tmp2.Length) / 2;
                arrayBOM1[v1 + 1, 32] = v3.ToString(); // Length  LevelCount

                if ( v3 == 1 ) // BOM ITEM
                {
                    // arrayBOM1[v1 + 1, 32] = "0"; // Length  LevelCount  Main ITEM 母料號
                    arrayBOM1Item[v4 + 1, 33] = (v1 + 1).ToString();   // Bom array pointer
                    arrayBOM1[v1 + 1, 33] = (v4 + 1).ToString();     // Bom ITEM Head 
                    arrayBOM1Item[v4 + 1, 11] = arrayBOM1[v1 + 1, 11]; // Parent
                    arrayBOM1Item[v4 + 1, 12] = arrayBOM1[v1 + 1, 12]; // ParentVer
                    arrayBOM1Item[v4 + 1, 37] = arrayBOM1[v1 + 1, 37]; // Parent + Ver
                    arrayBOM1Item[v4 + 1, 31] = arrayBOM1[v1 + 1, 31]; // Parent DataLevel
                    v4++;
                    ItemLen++;
                } // arrayBOM1[v1 + 1, 32] = "PARENT";

            }


            if ("1A322UP00" == arrayBOM1[v1 + 1, 11].ToString().Trim())
                v3 = v3;

        } // end for

        // 1 個 BOM Check
        // 檢查資料, 找出錯誤 BOM
        // arrayBOM1Item[v4 + 1, 11], 34: Level 1  
        v2 = 0;
        v3 = 0;
        v4 = 0; // pre  Level
        v5 = 0; // Curr Level
        v6 = 0; // Pre  Loc

        string PreDataLevel = "", CurrDataLevel = "";
        int PreLevelLong = 0, CurrLevelLong = 0;

        tmp1 = ""; // pre Item
        tmp2 = ""; // currency Item
        string LevelFlag = "", ChkDataLevel = "";
        string ChkCurrDataLevel = "";

        int v32 = 0; // DatLeveL Legth 
        sp1 = "";
        ///////////////////////////////////////////////////////////
        //  1. 先分組, 每個 BOM 為一組 
        //  2. 將每組起始 Array 記錄並開始作業
        ///////////////////////////////////////////////////////////
        for (v1 = 0; v1 < ItemLen; v1++)
        {
            LevelFlag = "";
            tmp1 = arrayBOM1Item[v1 + 1, 31].ToString(); // BOM ITEM DataLevel ( 須一樣 ) 
            ChkDataLevel = arrayBOM1Item[v1 + 1, 31].ToString();
            // tmp2 = arrayBOM1Item[v1 + 1, 33].Substring(0.1); // BOM Detail Pointer            
            tmp2 = arrayBOM1Item[v1 + 1, 33]; // BOM Detail Pointer 矩陣位置
          
            if (tmp2 != "")
            {
                v2 = Convert.ToInt32(tmp2);                   // start BOM Array Loc
                arrayLevel[0, 0] = arrayBOM1Item[v1 + 1, 11]; // Parent 2 個應相同
                arrayLevel[0, 0] = arrayBOM1[v2 + 1, 11];     // Parent
            }
            else
                arrayBOM1Item[v1 + 1, 29] = "E";  // flag = error

            ChkCurrDataLevel = arrayBOM1[v2, 31].ToString().Trim();
            ChkCurrDataLevel = ChkCurrDataLevel.Substring(0, 2);
            // ChkCurrDataLevel = ( arrayBOM1[v2, 31].ToString()).Substring(0, 6); 
            // while ((ChkDataLevel == arrayBOM1[v2, 31].Substring(0, 2)) && ( sp1 == arrayBOM1Item[v1 + 1, 29].ToString()) ) 
            // while ( ChkDataLevel == arrayBOM1[v2, 31].Substring(0, 2).ToString() ) 
            // while ( ChkDataLevel == ChkCurrDataLevel  )
            while ((   ChkDataLevel == ChkCurrDataLevel   )) 
            {
                LevelFlag = "";
                tmp1 = arrayBOM1[v2, 31].ToString();
                CurrDataLevel = arrayBOM1[v2, 31].ToString();
                CurrLevelLong = 0;
                if (arrayBOM1[v2, 32] != "")
                    CurrLevelLong = Convert.ToInt32(arrayBOM1[v2, 32]);

                if (tmp1 == "00")  // BOM Master
                {
                    CurrDataLevel = "";
                    CurrLevelLong = 0;
                    arrayLevel[0, 0] = arrayBOM1[v2, 11]; // ITEM
                    arrayBOM1[v2, 22] = arrayBOM1[v2, 11]; // Parent ITEM in 22
                    arrayBOM1[v2, 41] = arrayBOM1[v2, 12];  // 前一階為現階母料號Ver
                    arrayBOM1[v2, 42] = arrayBOM1[v2, 37];  // 前一階為現階母料號 + Ver
                    arrayBOM1[v2, 30] = "00";              // Phantom  00, 01, 11, 10 上下階
                    arrayBOM1[v2, 15] = "0";  // DNdt.Tables[0].Rows[v1]["QTY"].ToString().Trim();
                }
                else
                    if (CurrLevelLong == 1)  // tmp1 == "01")  // 第一階
                    {
                        arrayBOM1[v2, 22] = arrayBOM1[v2, 11];  // 前一階為現階母料號
                        arrayBOM1[v2, 41] = arrayBOM1[v2, 12];  // 前一階為現階母料號Ver
                        arrayBOM1[v2, 42] = arrayBOM1[v2, 37];  // 前一階為現階母料號 + Ver
                        arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號   
                        arrayLevel[CurrLevelLong, 1] = arrayBOM1[v2, 12];   // 目前為下一個母料號 Ver
                        arrayLevel[CurrLevelLong, 2] = arrayBOM1[v2, 37];   // 目前為下一個母料號 + Ver  
                        arrayBOM1[v2, 30] = "01";              // Phantom  00, 01, 11, 10 上下階 
                        arrayBOM1[v2, 15] = "0";  // DNdt.Tables[0].Rows[v1]["QTY"].ToString().Trim();
                        // Pre Level must cloase
                        if (v2 > 1)
                        {
                            tmp3 = arrayBOM1[v2 - 1 , 30].Substring(0, 1) + "0";
                            arrayBOM1[v2 - 1, 30] = tmp3;  // 前一階 Close
                        }                       


                    }
                    else
                        if (PreLevelLong + 1 < CurrLevelLong)
                        {
                            LevelFlag = "F";
                            v2 = DNCnt;
                            arrayBOM1Item[v1 + 1, 29] = LevelFlag;  // flag = Level Error
                        }
                        else
                            if ((PreLevelLong + 1) == CurrLevelLong)  // 下一階是連續
                            {
                                arrayBOM1[v2, 22] = arrayLevel[PreLevelLong, 0];  // 前一階為現階母料號
                                arrayBOM1[v2, 41] = arrayLevel[PreLevelLong, 1];  // 目前為下一個母料號 Ver
                                arrayBOM1[v2, 42] = arrayLevel[PreLevelLong, 2];  // 目前為下一個母料號 + Ver  ;                                  
                                arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號   
                                arrayLevel[CurrLevelLong, 1] = arrayBOM1[v2, 12];   // 目前為下一個母料號 Ver
                                arrayLevel[CurrLevelLong, 2] = arrayBOM1[v2, 37];   // 目前為下一個母料號 + Ver  
                                arrayBOM1[v2, 30] = "11";              // Phantom  00, 01, 11, 10 上下階 
                            }
                            else // 下一階不是連續, 而是往前縮
                            {
                                PreLevelLong = CurrLevelLong - 1;
                                arrayBOM1[v2, 22] = arrayLevel[PreLevelLong, 0];  // 前一階為現階母料號
                                arrayBOM1[v2, 41] = arrayLevel[PreLevelLong, 1];  // 目前為下一個母料號 Ver
                                arrayBOM1[v2, 42] = arrayLevel[PreLevelLong, 2];  // 目前為下一個母料號 + Ver  ;  

                                arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號 
                                arrayLevel[CurrLevelLong, 1] = arrayBOM1[v2, 12];   // 目前為下一個母料號 Ver
                                arrayLevel[CurrLevelLong, 2] = arrayBOM1[v2, 37];   // 目前為下一個母料號 + Ver 
                                tmp3 = arrayBOM1[v2, 30].Substring(0, 1) + "0";
                                arrayBOM1[v2 - 1, 30] = tmp3;  // 前一階 Close
                                arrayBOM1[v2, 30] = "11";
                            }

                PreDataLevel = CurrDataLevel;
                PreLevelLong = CurrLevelLong;
                v2++;
                if (v2 <= DNCnt)  // Get Next DataLevel
                {
                    ChkCurrDataLevel = arrayBOM1[v2, 31].ToString().Trim();
                    ChkCurrDataLevel = ChkCurrDataLevel.Substring(0, 2);
                }
                else // end
                    ChkCurrDataLevel = "";
                    
            }
        } // end for loop check BOMTXT     (v1 = 0; v1 < ItemLen; v1++)

        Decimal dqty = 0;
        v6 = 0;
        // Insert jbom2
        ChkCurrDataLevel = "";
        for (v1 = 0; v1 < ItemLen; v1++)
        {
            v5 = 100;
            if ((arrayBOM1Item[v1 + 1, 29] == "") || (arrayBOM1Item[v1 + 1, 29] == null))
            {
                Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                tmp2 = arrayBOM1Item[v1 + 1, 33]; // BOM Detail Pointer 
                ChkDataLevel = arrayBOM1Item[v1 + 1, 31].ToString();
                v2 = Convert.ToInt32(tmp2);       // start BOM Array Loc 
  

                ChkCurrDataLevel = arrayBOM1[v2, 31].ToString().Trim();
                ChkCurrDataLevel = ChkCurrDataLevel.Substring(0, 2);              
                while ((ChkDataLevel == ChkCurrDataLevel)) 
                // for (v2 = v3; v2 < DNCnt; v2++)
                {
                    v5++;
                    dqty = 0;
                    if (arrayBOM1[v2, 15].ToString().Trim() != "")
                    {
                        tmp1 = (arrayBOM1[v2, 15].ToString().Trim());  // 數量
                        dqty = Convert.ToDecimal(tmp1);
                    }

                    if ("01" == arrayBOM1[v2, 31].ToString().Trim())
                        tmp2 = tmp2;

                    // '1511' = DataLevel and '7CA17-001' = H_H_PN
                    tmpsqlW = "  SELECT  * from jbomm1  where  Osversion =  '" + arrayBOM1[v2, 1].ToString().Trim() + "'  "
                    + "  and   Item1 =    '" + arrayBOM1[v2, 42].ToString().Trim() + "'   "
                    + "  and   Part1 =    '" + arrayBOM1[v2, 37].ToString().Trim() + "'   ";
                    dt4 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, tmpsqlW );
                    if ( ( dt4 != null) && (  ( v8 = dt4.Tables[0].Rows.Count )  <= 0 ) )  // check insert need
                    {       

                    tmpsqlW = "Insert into jbomm1  ( Osversion, Item, Part, UnitQty, UnitQtyStr, LevelCount, BeginDate, EndDate, DueDate, Phantom, CG, "
                            + " Rseq, DocumentID, LineID, Mark, Note, Remark, ItemVer, PartVer,  Item1, Part1 ) values "
       + " ( '" + arrayBOM1[v2, 1].ToString().Trim() + "' , '" + arrayBOM1[v2, 22].ToString().Trim() + "' , '" + arrayBOM1[v2, 11].ToString().Trim() + "' ,  "
                        //+ " '" + arrayBOM1[v2, 15].ToString().Trim() + "' ,  '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
                        //+ "   cast (  ' " + dqty + " ' as float )  ,  '" + arrayBOM1[v2, 15].ToString().Trim() + "' , '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
       + "   " + dqty + " ,  '" + arrayBOM1[v2, 15].ToString().Trim() + "' , '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
       + "   '" + tmpDate + "' , '" + sp1 + "' , '" + sp1 + "' , '" + arrayBOM1[v2, 30].ToString().Trim() + "' , '" + sp1 + "' ,"
       + "   '" + arrayBOM1[v2, 6].ToString().Trim() + "' ,  '" + Currtime + "' , '" + v5.ToString() + "' ,  "
       + "   '" + sp1 + "' , '" + arrayBOM1[v2, 4].ToString().Trim() + "' , '" + sp1 + "' , '" + arrayBOM1[v2, 41].ToString().Trim() + "',  "
       + "   '" + arrayBOM1[v2, 12].ToString().Trim() + "', '" + arrayBOM1[v2, 42].ToString().Trim() + "', '" + arrayBOM1[v2, 37].ToString().Trim() + "' ) ";
                    v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                    if (v4 >= 0) // Successed, It could be not data
                        v6++;

                    }   // insert end 

                    v2++;
                    if (v2 <= DNCnt)  // Get Next DataLevel
                    {
                        ChkCurrDataLevel = arrayBOM1[v2, 31].ToString().Trim();
                        ChkCurrDataLevel = ChkCurrDataLevel.Substring(0, 2);
                    }
                    else // end
                        ChkCurrDataLevel = "";
                }
            }

        } // end for loop check BOMTXT



        return ("");

    } // EV expand BOM

    public string AddEV(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        //tmp1 = "TOP";
        //sqlr = " update BOMTXT set DataLevel = '00' where (  ( DataLevel = 'TOP' )  and ( flag is null )) ";
        //v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //if (v4 > 0) // Successed
        //    v4 = v4;

        sqlr = "  SELECT * FROM jevm1 where Osversion = '" + ChkDate + "' ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0");     // Not Data

        int MaxItem = 60, ItemLen = 0;
        string[,] arrayBOM1Item = new string[DNCnt + 1, MaxItem + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= MaxItem; v2++)
                arrayBOM1Item[v1, v2] = "";

        v3 = 0; // 
        v4 = 0;
  
        // Read jevm1
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayBOM1Item[v1 + 1, 0] = (v1 + 1).ToString();
            arrayBOM1Item[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["Osversion"].ToString().Trim(); 
            arrayBOM1Item[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["CDATE"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 4] = ""; //  DNdt.Tables[0].Rows[v1]["DataLevel"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["LineID"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["DATE"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["DV_ID"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["CustomerPN"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["Descr"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 10] = DNdt.Tables[0].Rows[v1]["CustomerSite"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 11] = DNdt.Tables[0].Rows[v1]["FoxconnSite"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 12] = DNdt.Tables[0].Rows[v1]["Agreement"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 13] = DNdt.Tables[0].Rows[v1]["Item"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 14] = DNdt.Tables[0].Rows[v1]["Project"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 15] = DNdt.Tables[0].Rows[v1]["FoxconnPartNo"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 16] = DNdt.Tables[0].Rows[v1]["Consigned"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 17] = DNdt.Tables[0].Rows[v1]["On_Hand"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 18] = DNdt.Tables[0].Rows[v1]["GIT"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 19] = DNdt.Tables[0].Rows[v1]["Block"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 20] = DNdt.Tables[0].Rows[v1]["Quality"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 21] = DNdt.Tables[0].Rows[v1]["Min_Days"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 22] = ""; // Parent ITEM
            arrayBOM1Item[v1 + 1, 23] = DNdt.Tables[0].Rows[v1]["Max_Days"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 24] = DNdt.Tables[0].Rows[v1]["Min_Inventory"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 25] = DNdt.Tables[0].Rows[v1]["Max_Inventory"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 26] = DNdt.Tables[0].Rows[v1]["W1"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 27] = DNdt.Tables[0].Rows[v1]["W2"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 28] = DNdt.Tables[0].Rows[v1]["W3"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 29] = DNdt.Tables[0].Rows[v1]["W4"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 30] = DNdt.Tables[0].Rows[v1]["W5"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 31] = DNdt.Tables[0].Rows[v1]["W6"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 32] = DNdt.Tables[0].Rows[v1]["W7"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 33] = DNdt.Tables[0].Rows[v1]["W8"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 34] = DNdt.Tables[0].Rows[v1]["W9"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 35] = DNdt.Tables[0].Rows[v1]["W10"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 36] = DNdt.Tables[0].Rows[v1]["W11"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 37] = DNdt.Tables[0].Rows[v1]["W12"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 38] = DNdt.Tables[0].Rows[v1]["W13"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 39] = DNdt.Tables[0].Rows[v1]["W14"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 40] = DNdt.Tables[0].Rows[v1]["W15"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 41] = DNdt.Tables[0].Rows[v1]["W16"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 42] = DNdt.Tables[0].Rows[v1]["W17"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 43] = DNdt.Tables[0].Rows[v1]["W18"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 44] = DNdt.Tables[0].Rows[v1]["W19"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 45] = DNdt.Tables[0].Rows[v1]["W20"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 46] = DNdt.Tables[0].Rows[v1]["TotWDV"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 47] = DNdt.Tables[0].Rows[v1]["CDV"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 48] = DNdt.Tables[0].Rows[v1]["NetDV"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 49] = DNdt.Tables[0].Rows[v1]["RunningDay"].ToString().Trim();
            arrayBOM1Item[v1 + 1, 50] = DNdt.Tables[0].Rows[v1]["Flag"].ToString().Trim(); 
            arrayBOM1Item[v1 + 1, 51] = ""; //  Phantom, 00, 01, 10, 11
            arrayBOM1Item[v1 + 1, 52] = "";     
     
        } // end for

        sqlr = "  delete jbomm1tmp  ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

        sqlr = "  delete jbomm1tmp1 ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

        sqlr = "  delete jbomm1tmp2 ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

        sqlr = "  delete jbomm1tmp3 ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            tmp1 = arrayBOM1Item[v1 + 1, 1].Trim();   // Osversion
            tmp2 = arrayBOM1Item[v1 + 1, 15].Trim();  // FoxconnPartNo
            tmp3 = arrayBOM1Item[v1 + 1, 46].Trim();  // Qty

            if (tmp2 == "1A322UP00")
                v4 = v4;
            if ( ( tmp3 != "" ) && ( tmp3 != "0" ) && ( tmp3 != null ) )   
                   t21 = ExpBOM( BSite,  Dtype, SysDate, DBType,  DBReadString, DBWriString, PCode, "", tmp1, tmp2, tmp3, "" ); 

        }

        // 完成從 EV 抓取到 tmp file 
        // run jbomm1tmp1 not data 
        // jbomm1tmp 為展開處
        // jbomm1tmp1 為 01, 11
        // jbomm1tmp2 為 10, 00

        Ret1 = "Y";

        while (Ret1 == "Y")
        {
            sqlr = "  select * from jbomm1tmp1 ";
            dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (dt1 == null) return ("-1"); // Syn Error
            DNCnt = dt1.Tables[0].Rows.Count;
            if (DNCnt == 0) return ("0");     // Not Data

            sqlr = "  insert into [ERPDBF].[dbo].[jbomm1tmp3]  select * from jbomm1tmp1 ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

            sqlr = "  delete jbomm1tmp  ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

            sqlr = "  insert into [ERPDBF].[dbo].[jbomm1tmp]  select * from jbomm1tmp1 ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

            sqlr = "  delete jbomm1tmp1 ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);           

            sqlr = "  SELECT * FROM jbomm1tmp ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if (DNCnt == 0) return ("0");     // Not Data

            // int MaxItem = 60, ItemLen = 0;
            // string[,] arrayBOM1Item = new string[DNCnt + 1, MaxItem + 1];
            for (v1 = 0; v1 <= DNCnt; v1++)
                for (v2 = 0; v2 <= MaxItem; v2++)
                    arrayBOM1Item[v1, v2] = "";


            for (v1 = 0; v1 < DNCnt; v1++)
            {
                arrayBOM1Item[v1 + 1, 0] = (v1 + 1).ToString();
                arrayBOM1Item[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["Osversion"].ToString().Trim();
                arrayBOM1Item[v1 + 1, 15] = DNdt.Tables[0].Rows[v1]["Part1"].ToString().Trim();
                arrayBOM1Item[v1 + 1, 46] = DNdt.Tables[0].Rows[v1]["TmpQty"].ToString().Trim();
               

            } // end for

         
            for (v1 = 0; v1 < DNCnt; v1++)
            {
                tmp1 = arrayBOM1Item[v1 + 1, 1].Trim();   // Osversion
                tmp2 = arrayBOM1Item[v1 + 1, 15].Trim();  // FoxconnPartNo
                tmp3 = arrayBOM1Item[v1 + 1, 46].Trim();  // Qty
                if ((tmp3 != "") && (tmp3 != "0") && (tmp3 != null))
                    t21 = ExpBOM(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, "", tmp1, tmp2, tmp3, "");

            }

        } // while (Ret1 == "Y")





        return ("");
      

    } // end  ADDEV

    public string ExpBOM(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string funct1, string Osver, string PPart, string QQty, string TType)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t16 = "", t17 = "", t18 = "", t19 = "", t20 = "", t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0, d4 = 0;
        Decimal d5 = 0, d6 = 0, d7 = 0, d8 = 0;
        Decimal dqty = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");
        
       
        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);


        //tmp1 = "TOP";
        //sqlr = " update BOMTXT set DataLevel = '00' where (  ( DataLevel = 'TOP' )  and ( flag is null )) ";
        //v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        //if (v4 > 0) // Successed
        //    v4 = v4;


        tmp1 = Osver;
        tmp2 = PPart;
        tmp3 = QQty;

        d1 = Convert.ToDecimal(tmp3);  // 數量

        sqlr = "  select * from [ERPDBF].[dbo].[jbomm1] where Osversion = '" + tmp1 + "' and Item1 = '" + tmp2 + "' and UnitQtyStr != '0' order by Part1 desc ";
        dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (dt1 == null) return ("-1"); // Syn Error
        DNCnt = dt1.Tables[0].Rows.Count;
        if ( DNCnt == 0) return ("0");     // Not Data

        int MaxItem = 60, ItemLen = 0;
        string[,] arrayBOM1 = new string[DNCnt + 1, MaxItem + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= MaxItem; v2++)
                arrayBOM1[v1, v2] = "";

        v3 = 0; // 
        v4 = 0;


        string BuffBom = " [ERPDBF].[dbo].[jbomm1tmp1] "; // 還要做
        string LastBom = " [ERPDBF].[dbo].[jbomm1tmp2] "; // 不須再做
        string CurrBom = "";
        // Read jevm1

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            t01 = (v1 + 1).ToString();
            t02 = dt1.Tables[0].Rows[v1]["Osversion"].ToString().Trim();
            t03 = dt1.Tables[0].Rows[v1]["Item"].ToString().Trim();
            t04 = dt1.Tables[0].Rows[v1]["Part"].ToString().Trim();
            t05 = dt1.Tables[0].Rows[v1]["UnitQty"].ToString().Trim();
            t06 = dt1.Tables[0].Rows[v1]["UnitQtyStr"].ToString().Trim();
            t07 = dt1.Tables[0].Rows[v1]["LevelCount"].ToString().Trim();
            t08 = dt1.Tables[0].Rows[v1]["BeginDate"].ToString().Trim();
            t09 = dt1.Tables[0].Rows[v1]["Phantom"].ToString().Trim();
            t10 = dt1.Tables[0].Rows[v1]["TmpQty"].ToString().Trim();

            t11 = dt1.Tables[0].Rows[v1]["Rseq"].ToString().Trim();
            t12 = dt1.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            t13 = dt1.Tables[0].Rows[v1]["LineID"].ToString().Trim();
            t14 = dt1.Tables[0].Rows[v1]["Mark"].ToString().Trim();
            t15 = dt1.Tables[0].Rows[v1]["Note"].ToString().Trim();
            t16 = dt1.Tables[0].Rows[v1]["Remark"].ToString().Trim();
            t17 = dt1.Tables[0].Rows[v1]["CG"].ToString().Trim();
            t18 = dt1.Tables[0].Rows[v1]["ItemVer"].ToString().Trim();
            t19 = dt1.Tables[0].Rows[v1]["PartVer"].ToString().Trim();
            t20 = dt1.Tables[0].Rows[v1]["Item1"].ToString().Trim();
            t21 = dt1.Tables[0].Rows[v1]["Part1"].ToString().Trim();

            if (t21 == "5T020Y400VA")  // trace
                t13 = t13; 

            d2 = Convert.ToDecimal(t05); // 單為用量

            dqty = d1 * d2;  // 數量 * 單為用量

            // 10, 00, // 無下階     
            if (t09.Substring(1, 1) == "0") CurrBom = LastBom; // 10, 00, // 無下階  
            else  CurrBom = BuffBom;

            tmpsqlW = "select * from " + CurrBom + " where Osversion = '" + t02 + "' and Item1 = '" + t03 + "' and Part1 = '" + t21 + "'  ";
            dt2 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, tmpsqlW);
            if (dt2 == null) v4 = 0;
            else
                v4 = dt2.Tables[0].Rows.Count;
            if (v4 > 0) // Update 
            {
                tmp5 = dt2.Tables[0].Rows[0]["TmpQty"].ToString().Trim();  // 原有
                d3 = Convert.ToDecimal(tmp5);       
                dqty = dqty + d3;   // 原有 +  新的

                tmpsqlW = "Update " + CurrBom +  " set TmpQty = " + dqty + "  where Osversion = '" + t02 + "' and Item1 = '" + t03 + "' and Part1 = '" + t21 + "'  ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                if (v5 >= 0) // Successed, It could be not data
                    v6++;
            }
            else  // Insert Data
            {
                tmpsqlW = "INSERT INTO " + CurrBom + " ( Osversion, Item, Part, UnitQty, UnitQtyStr, LevelCount, BeginDate, EndDate, DueDate, Phantom, CG, "
                            + " Rseq, DocumentID, TmpQty, LineID, Mark, Note, Remark, ItemVer, PartVer, Item1, Part1 ) values "
                    + " ( '" + t02 + "' , '" + t03 + "' , '" + t04 + "' , " + d2 + " , '" + t06 + "' , "
                    + "   '" + t07 + "',  '" + t08 + "' , '" + sp1 + "', '" + sp1 + "', '" + t09 + "', '" + t17 + "', '" + t11 + "', '" + t12 + "', "
                    + "   " + dqty + " , '" + t13 + "', '" + t14 + "', '" + t15 + "' ,  '" + t16 + "',  "
                    + "   '" + t18 + "', '" + t19 + "', '" + t20 + "' ,  '" + t21 + "'                                                        ) ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                if (v5 >= 0) // Successed, It could be not data
                    v7++;

            }



            if ( funct1 == "A" ) // write unitqty in jevm2 
            {

                tmpsqlW = "select * from jevm2 where Osversion = '" + t02 + "' and Supplier_Code = '" + t21 + "'  ";
                dt2 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, tmpsqlW);
                if (dt2 == null) v4 = 0;
                else    v4 = dt2.Tables[0].Rows.Count;                    
                
                if (v4 > 0)
                {
                    tmp1 = dt2.Tables[0].Rows[0]["Tmp1Qty"].ToString().Trim();
                    tmp2 = dt2.Tables[0].Rows[0]["Tmp2Qty"].ToString().Trim();
                    tmp3 = dt2.Tables[0].Rows[0]["Tmp3Qty"].ToString().Trim();

                    if (tmp1 == "") tmp1 = "0";
                    if (tmp2 == "") tmp1 = "0";
                    if (tmp3 == "") tmp1 = "0";
                    if ( t05 == "") t05 = "0";

                    
                    //d1 = Convert.ToDecimal(tmp1);
                    //d2 = Convert.ToDecimal(tmp2);
                    //d3 = Convert.ToDecimal(tmp3);
                    //d4 = Convert.ToDecimal(t05); // UnitQty


                    d5 = Convert.ToDecimal(tmp1);
                    d6 = Convert.ToDecimal(tmp2);
                    d7 = Convert.ToDecimal(tmp3);
                    d8 = Convert.ToDecimal(t05); // UnitQty

                    if ( d8 > 0 )  // 單位用量須大於 0  
                    {
                         d5 = d5 + d8;  // 全部單位育量相加
                         d2++;          // 發生次數
                         d3 = d1 / d2;  // 總平均單用育量

                         tmpsqlW = "Update jevm2 set Tmp1Qty = " + d5 + ",  Tmp2Qty = " + d2 + ",  Tmp3Qty = " + d3 + "     "
                         + " where Osversion = '" + t02 + "' and  Supplier_Code  = '" + t21 + "'  ";  
                         v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                         if (v5 >= 0) // Successed, It could be not data
                             v6++;
                    }
                }

            }

        } // end for     
       

        return("");
    } // end Exp_BOM   
  

    public string SumExpEV(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t16 = "", t17 = "", t18 = "", t19 = "", t20 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0, dqty = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");
        string LastUpdate = DateTime.Now.ToString("yyyyMMddHHmmssmm");

        sqlr = " DELETE FROM [ERPDBF].[dbo].[jbomm1tmp4] ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);
        sqlr = "  select * from [ERPDBF].[dbo].[jbomm1tmp2] where Osversion = '" + ChkDate + "' and TmpQty != '0' order by Part1 ";
        dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (dt1 == null) DNCnt = 0; // Syn Error
        else
            DNCnt = dt1.Tables[0].Rows.Count;
            // if (DNCnt == 0) return ("0");     // Not Data
         
        //int MaxItem = 60, ItemLen = 0;
        //string[,] arrayBOM1 = new string[DNCnt + 1, MaxItem + 1];
        //for (v1 = 0; v1 <= DNCnt; v1++)
        //    for (v2 = 0; v2 <= MaxItem; v2++)
        //        arrayBOM1[v1, v2] = "";

        v3 = 0; // 
        v4 = 0;

        dqty = 0;
        tmpsqlW = "Update jevm2  set BomQty = " + dqty + ", TmpQty = " + dqty + ", Flag2 = ''  where Osversion = '" + SysDate + "'  ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        

        string BuffBom = " [ERPDBF].[dbo].[jbomm1tmp1] "; // 還要做
        string LastBom = " [ERPDBF].[dbo].[jbomm1tmp2] "; // 不須再做
        string CurrBom = "";
        
        // Update Raw
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            t01 = (v1 + 1).ToString();
            t02 = dt1.Tables[0].Rows[v1]["Osversion"].ToString().Trim();
            t03 = dt1.Tables[0].Rows[v1]["Item"].ToString().Trim();
            t04 = dt1.Tables[0].Rows[v1]["Part"].ToString().Trim();
            t05 = dt1.Tables[0].Rows[v1]["UnitQty"].ToString().Trim();
            t06 = dt1.Tables[0].Rows[v1]["UnitQtyStr"].ToString().Trim();
            t07 = dt1.Tables[0].Rows[v1]["LevelCount"].ToString().Trim();
            t08 = dt1.Tables[0].Rows[v1]["BeginDate"].ToString().Trim();
            t09 = dt1.Tables[0].Rows[v1]["Phantom"].ToString().Trim();
            t10 = dt1.Tables[0].Rows[v1]["TmpQty"].ToString().Trim();

            t11 = dt1.Tables[0].Rows[v1]["Rseq"].ToString().Trim();
            t12 = dt1.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            t13 = dt1.Tables[0].Rows[v1]["LineID"].ToString().Trim();
            t14 = dt1.Tables[0].Rows[v1]["Mark"].ToString().Trim();
            t15 = dt1.Tables[0].Rows[v1]["Note"].ToString().Trim();
            t16 = dt1.Tables[0].Rows[v1]["Remark"].ToString().Trim();
            t17 = dt1.Tables[0].Rows[v1]["CG"].ToString().Trim();
            t18 = dt1.Tables[0].Rows[v1]["ItemVer"].ToString().Trim();
            t19 = dt1.Tables[0].Rows[v1]["PartVer"].ToString().Trim();
            t20 = dt1.Tables[0].Rows[v1]["Item1"].ToString().Trim();
            t21 = dt1.Tables[0].Rows[v1]["Part1"].ToString().Trim();


            // d2 = Convert.ToDecimal(t05); // 單為用量
            // dqty = d1 * d2;  // 數量 * 單為用量

            d3 = Convert.ToDecimal(t10);
            dqty = d3;   // 新的

            if (t21 == "5T020Y400VA")
                v5 = v5;

            tmpsqlW = "Update jevm2  set BomQty = BomQty + " + dqty + ", Flag2 = 'Y'  where Osversion = '" + t02 + "' and Supplier_Code = '" + t21 + "'   ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            if (v5 > 0) // Successed, It could be not data
                v6++;
            else
            {   // Insert fail table
                tmpsqlW = "Insert into jbomm1tmp4   ( Osversion, Item, Part, UnitQty, UnitQtyStr, LevelCount, BeginDate, Phantom, tmpQty, "
                            + " Rseq, DocumentID, LineID, Mark, Note, ItemVer, PartVer,  Item1, Part1 ) values "
       + " ( '" + t02 + "', '" + t03 + "', '" + t04 + "' , '" + t05 + "' , '" + t06 + "' , '" + t07 + "' ,  '" + t08 + "' , '" + t09 + "' , '" + t10 + "', "
       + "  '" + t11 + "', '" + t12 + "', '" + t13 + "' , '" + t14 + "' , '" + t15 + "' , '" + t18 + "' ,  '" + t19 + "' , '" + t20 + "' , '" + t21 + "' ) "; 
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);              
       
            }
            

        } // end for  
   

        // Update WIP
        sqlr = "  select * from [ERPDBF].[dbo].[jbomm1tmp3] where Osversion = '" + ChkDate + "' and TmpQty != '0' order by Part1 ";
        dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (dt1 == null) DNCnt = 0; // Syn Error
        else
            DNCnt = dt1.Tables[0].Rows.Count;
        

        v3 = 0; // 
        v4 = 0;
        dqty = 0;
       

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            t01 = (v1 + 1).ToString();
            t02 = dt1.Tables[0].Rows[v1]["Osversion"].ToString().Trim();
            t03 = dt1.Tables[0].Rows[v1]["Item"].ToString().Trim();
            t04 = dt1.Tables[0].Rows[v1]["Part"].ToString().Trim();
            t05 = dt1.Tables[0].Rows[v1]["UnitQty"].ToString().Trim();
            t06 = dt1.Tables[0].Rows[v1]["UnitQtyStr"].ToString().Trim();
            t07 = dt1.Tables[0].Rows[v1]["LevelCount"].ToString().Trim();
            t08 = dt1.Tables[0].Rows[v1]["BeginDate"].ToString().Trim();
            t09 = dt1.Tables[0].Rows[v1]["Phantom"].ToString().Trim();
            t10 = dt1.Tables[0].Rows[v1]["TmpQty"].ToString().Trim();

            t11 = dt1.Tables[0].Rows[v1]["Rseq"].ToString().Trim();
            t12 = dt1.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            t13 = dt1.Tables[0].Rows[v1]["LineID"].ToString().Trim();
            t14 = dt1.Tables[0].Rows[v1]["Mark"].ToString().Trim();
            t15 = dt1.Tables[0].Rows[v1]["Note"].ToString().Trim();
            t16 = dt1.Tables[0].Rows[v1]["Remark"].ToString().Trim();
            t17 = dt1.Tables[0].Rows[v1]["CG"].ToString().Trim();
            t18 = dt1.Tables[0].Rows[v1]["ItemVer"].ToString().Trim();
            t19 = dt1.Tables[0].Rows[v1]["PartVer"].ToString().Trim();
            t20 = dt1.Tables[0].Rows[v1]["Item1"].ToString().Trim();
            t21 = dt1.Tables[0].Rows[v1]["Part1"].ToString().Trim();

            if (t21 == "5T020Y400VA")
                v5 = v5;

            // d2 = Convert.ToDecimal(t05); // 單為用量
            // dqty = d1 * d2;  // 數量 * 單為用量

            d3 = Convert.ToDecimal(t10);
            dqty = d3;   // 新的

            tmpsqlW = "Update jevm2  set BomQty = BomQty + " + dqty + ", Flag2 = 'Y'  where Osversion = '" + t02 + "' and Supplier_Code = '" + t21 + "'   ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            if (v5 > 0) // Successed, It could be not data
                v6++;
            else
            {   // Insert fail table
                tmpsqlW = "Insert into jbomm1tmp4   ( Osversion, Item, Part, UnitQty, UnitQtyStr, LevelCount, BeginDate, Phantom, tmpQty, "
                            + " Rseq, DocumentID, LineID, Mark, Note, ItemVer, PartVer,  Item1, Part1 ) values "
       + " ( '" + t02 + "', '" + t03 + "', '" + t04 + "' , '" + t05 + "' , '" + t06 + "' , '" + t07 + "' ,  '" + t08 + "' , '" + t09 + "' , '" + t10 + "', "
       + "  '" + t11 + "', '" + t12 + "', '" + t13 + "' , '" + t14 + "' , '" + t15 + "' , '" + t18 + "' ,  '" + t19 + "' , '" + t20 + "' , '" + t21 + "' ) ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);

            }

        } // end for  


        tmpsqlW = "Update jevm2  set TmpQty = BomQty + Inventory,  TmpExcess = Inventory, UpdateTime = '" + LastUpdate + "'   "
        +  "  where Osversion = '" + t02 + "' and BomQty > 0 ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        if (v5 > 0) // Successed, It could be not data
            v6++;
        tmpsqlW = "Update jevm2  set TmpQty = BomQty + Inventory,  TmpExcess = BomQty + Inventory, UpdateTime = '" + LastUpdate + "'   "
        + "  where Osversion = '" + t02 + "' and BomQty <= 0 ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        if (v5 > 0) // Successed, It could be not data
            v6++;


        return ("");

    } // end of SumExpEV


    public string ConvertBOM1(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        tmp1 = "TOP";
        sqlr = " update BOMTXT set DataLevel = '00' where (  ( DataLevel = 'TOP' )  and ( flag is null )) ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        if (v4 > 0) // Successed
            v4 = v4;

        sqlr = "  SELECT  * from BOMTXT  where ( ( substring(CDATE,1,6) = '" + ChkDate + "' ) and  ( flag is null ) ) order by DataLevel asc ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0");     // Not Data

        string[,] arrayBOM1 = new string[DNCnt + 1, 50 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayBOM1[v1, v2] = "";

        int MaxItem = 50, ItemLen = 0;
        string[,] arrayBOM1Item = new string[MaxItem + 1, 50 + 1];
        for (v1 = 0; v1 <= MaxItem; v1++)
            for (v2 = 0; v2 <= 50; v2++)
                arrayBOM1[v1, v2] = "";

        ///////////////////////////////////////////////////////
        // 放妻階臨時表, 每次須更新, from array 0
        string[,] arrayLevel = new string[50 + 1, 3 + 1];
        for (v1 = 0; v1 <= 50; v1++)
            for (v2 = 0; v2 <= 3; v2++)
                arrayLevel[v1, v2] = "";

        string PPart = "", CPart = "";

        v3 = 0; // Length
        v4 = 0;
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayBOM1[v1 + 1, 0] = (v1 + 1).ToString();
            arrayBOM1[v1 + 1, 1] = ChkDate; // Chech Date yyyymm
            arrayBOM1[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["CDATE"].ToString().Trim();
            arrayBOM1[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["DocumentID"].ToString().Trim();
            arrayBOM1[v1 + 1, 4] = DNdt.Tables[0].Rows[v1]["DataLevel"].ToString().Trim();
            arrayBOM1[v1 + 1, 5] = DNdt.Tables[0].Rows[v1]["LineID"].ToString().Trim();
            arrayBOM1[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["Pri"].ToString().Trim();
            arrayBOM1[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["WT"].ToString().Trim();
            arrayBOM1[v1 + 1, 8] = DNdt.Tables[0].Rows[v1]["Part_Source"].ToString().Trim();
            arrayBOM1[v1 + 1, 9] = DNdt.Tables[0].Rows[v1]["Phan_Part"].ToString().Trim();
            arrayBOM1[v1 + 1, 10] = DNdt.Tables[0].Rows[v1]["Outsourcing"].ToString().Trim();
            arrayBOM1[v1 + 1, 11] = DNdt.Tables[0].Rows[v1]["H_H_PN"].ToString().Trim();
            arrayBOM1[v1 + 1, 12] = DNdt.Tables[0].Rows[v1]["H_H_PN_Ver"].ToString().Trim();
            arrayBOM1[v1 + 1, 13] = DNdt.Tables[0].Rows[v1]["Cus_PN"].ToString().Trim();
            arrayBOM1[v1 + 1, 14] = DNdt.Tables[0].Rows[v1]["Cus_Ver"].ToString().Trim();
            arrayBOM1[v1 + 1, 15] = DNdt.Tables[0].Rows[v1]["QTY"].ToString().Trim();
            arrayBOM1[v1 + 1, 16] = DNdt.Tables[0].Rows[v1]["Unit"].ToString().Trim();
            arrayBOM1[v1 + 1, 17] = DNdt.Tables[0].Rows[v1]["Part_Type"].ToString().Trim();
            arrayBOM1[v1 + 1, 18] = DNdt.Tables[0].Rows[v1]["Dely_Part"].ToString().Trim();
            arrayBOM1[v1 + 1, 19] = DNdt.Tables[0].Rows[v1]["English_Name"].ToString().Trim();
            arrayBOM1[v1 + 1, 20] = DNdt.Tables[0].Rows[v1]["Chinese_Name"].ToString().Trim();
            arrayBOM1[v1 + 1, 21] = DNdt.Tables[0].Rows[v1]["Process"].ToString().Trim();
            arrayBOM1[v1 + 1, 22] = ""; // Parent ITEM
            arrayBOM1[v1 + 1, 23] = DNdt.Tables[0].Rows[v1]["Op_Scrap"].ToString().Trim();
            arrayBOM1[v1 + 1, 24] = DNdt.Tables[0].Rows[v1]["Comp_Scrap"].ToString().Trim();
            arrayBOM1[v1 + 1, 25] = DNdt.Tables[0].Rows[v1]["Target_Op_Scrap"].ToString().Trim();
            arrayBOM1[v1 + 1, 26] = DNdt.Tables[0].Rows[v1]["Change_Description"].ToString().Trim();
            arrayBOM1[v1 + 1, 27] = DNdt.Tables[0].Rows[v1]["ABC_Indicator"].ToString().Trim();
            arrayBOM1[v1 + 1, 28] = DNdt.Tables[0].Rows[v1]["RunningDay"].ToString().Trim();
            arrayBOM1[v1 + 1, 29] = ""; //  DNdt.Tables[0].Rows[v1]["Flag"].ToString().Trim();   phantom
            arrayBOM1[v1 + 1, 30] = "11"; //  Phantom, 00, 01, 10, 11
            arrayBOM1[v1 + 1, 31] = DNdt.Tables[0].Rows[v1]["DataLevel"].ToString().Trim();
            if (arrayBOM1[v1 + 1, 31] != "")
            {
                tmp2 = arrayBOM1[v1 + 1, 31].ToString().Trim();
                v3 = (tmp2.Length) / 2;
                arrayBOM1[v1 + 1, 32] = v3.ToString(); // Length  LevelCount

                if ((v3 == 1) && (arrayBOM1[v1 + 1, 31] == "00")) // BOM ITEM
                {
                    arrayBOM1[v1 + 1, 32] = "0"; // Length  LevelCount  Main ITEM 母料號
                    arrayBOM1Item[v4 + 1, 33] = (v1 + 1).ToString();   // Bom array pointer
                    arrayBOM1[v1 + 1, 33] = (v4 + 1).ToString();     // Bom ITEM Head 
                    arrayBOM1Item[v4 + 1, 11] = arrayBOM1[v1 + 1, 11]; // Parent
                    arrayBOM1Item[v4 + 1, 31] = arrayBOM1[v1 + 1, 31]; // Parent number
                    v4++;
                    ItemLen++;
                } // arrayBOM1[v1 + 1, 32] = "PARENT";

            }


        } // end for

        // 1 個 BOM Check
        // 檢查資料, 找出錯誤 BOM
        // arrayBOM1Item[v4 + 1, 11], 34: Level 1  
        v2 = 0;
        v3 = 0;
        v4 = 0; // pre  Level
        v5 = 0; // Curr Level
        v6 = 0; // Pre  Loc

        string PreDataLevel = "", CurrDataLevel = "";
        int PreLevelLong = 0, CurrLevelLong = 0;

        tmp1 = ""; // pre Item
        tmp2 = ""; // currency Item
        string LevelFlag = "";

        int v32 = 0; // DatLeveL Legth 
        for (v1 = 0; v1 < ItemLen; v1++)
        {
            LevelFlag = "";
            tmp1 = arrayBOM1Item[v1 + 1, 31]; // BOM ITEM

            // tmp2 = arrayBOM1Item[v1 + 1, 33].Substring(0.1); // BOM Detail Pointer            
            tmp2 = arrayBOM1Item[v1 + 1, 33]; // BOM Detail Pointer 

            if (tmp2 != "")
            {
                v2 = Convert.ToInt32(tmp2);                   // start BOM Array Loc
                arrayLevel[0, 0] = arrayBOM1Item[v1 + 1, 11]; // Parent 2 個應相同
                arrayLevel[0, 0] = arrayBOM1[v2 + 1, 11];     // Parent
            }
            else
                arrayBOM1Item[v1 + 1, 29] = "E";  // flag = error

            for (v2 = v2; v2 < DNCnt; v2++)
            {
                LevelFlag = "";
                tmp1 = arrayBOM1[v2, 31];
                CurrDataLevel = arrayBOM1[v2, 31];
                CurrLevelLong = 0;
                if (arrayBOM1[v2, 32] != "")
                    CurrLevelLong = Convert.ToInt32(arrayBOM1[v2, 32]);

                if (tmp1 == "00")  // BOM Master
                {
                    CurrDataLevel = "";
                    CurrLevelLong = 0;
                    arrayLevel[0, 0] = arrayBOM1[v2, 11]; // ITEM
                    arrayBOM1[v2, 22] = arrayBOM1[v2, 11]; // Parent ITEM in 22
                    arrayBOM1[v2, 30] = "00";              // Phantom  00, 01, 11, 10 上下階
                }
                else
                    if (CurrLevelLong == 1)  // tmp1 == "01")  // 第一階
                    {
                        arrayBOM1[v2, 22] = arrayLevel[PreLevelLong, 0];  // 前一階為現階母料號
                        arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號   
                        arrayBOM1[v2, 30] = "01";              // Phantom  00, 01, 11, 10 上下階 
                    }
                    else
                        if (PreLevelLong + 1 < CurrLevelLong)
                        {
                            LevelFlag = "F";
                            v2 = DNCnt;
                            arrayBOM1Item[v1 + 1, 29] = LevelFlag;  // flag = Level Error
                        }
                        else
                            if ((PreLevelLong + 1) == CurrLevelLong)  // 下一階是連續
                            {
                                arrayBOM1[v2, 22] = arrayLevel[PreLevelLong, 0];  // 前一階為現階母料號
                                arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號   
                                arrayBOM1[v2, 30] = "11";              // Phantom  00, 01, 11, 10 上下階 
                            }
                            else // 下一階不是連續, 而是往前縮
                            {
                                PreLevelLong = CurrLevelLong - 1;
                                arrayBOM1[v2, 22] = arrayLevel[PreLevelLong, 0];  // 前一階為現階母料號
                                arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號   
                                tmp3 = arrayBOM1[v2, 30].Substring(0, 1) + "0";
                                arrayBOM1[v2 - 1, 30] = tmp3;  // 前一階 Close
                                arrayBOM1[v2, 30] = "11";
                            }

                PreDataLevel = CurrDataLevel;
                PreLevelLong = CurrLevelLong;
            }
        } // end for loop check BOMTXT     

        Decimal dqty = 0;
        // Insert jbom2
        for (v1 = 0; v1 < ItemLen; v1++)
        {
            v5 = 100;
            if ((arrayBOM1Item[v1 + 1, 29] == "") || (arrayBOM1Item[v1 + 1, 29] == null))
            {
                Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                tmp2 = arrayBOM1Item[v1 + 1, 33]; // BOM Detail Pointer 
                v3 = Convert.ToInt32(tmp2);       // start BOM Array Loc              
                for (v2 = v3; v2 < DNCnt; v2++)
                {
                    v5++;
                    dqty = 0;
                    if (arrayBOM1[v2, 15].ToString().Trim() != "")
                    {
                        tmp1 = (arrayBOM1[v2, 15].ToString().Trim());  // 數量
                        dqty = Convert.ToDecimal(tmp1);
                    }

                    if ("1511" == arrayBOM1[v2, 31].ToString().Trim())
                        tmp2 = tmp2;

                    // '1511' = DataLevel and '7CA17-001' = H_H_PN

                    tmpsqlW = "Insert into jbomm1  ( Osversion, Item, Part, UnitQty, UnitQtyStr, LevelCount, BeginDate, EndDate, DueDate, Phantom, CG, "
                            + " Rseq, DocumentID, LineID, TmpQty, Mark, Note, Remark ) values "
       + " ( '" + arrayBOM1[v2, 1].ToString().Trim() + "' , '" + arrayBOM1[v2, 22].ToString().Trim() + "' , '" + arrayBOM1[v2, 11].ToString().Trim() + "' ,  "
                        //+ " '" + arrayBOM1[v2, 15].ToString().Trim() + "' ,  '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
                        //+ "   cast (  ' " + dqty + " ' as float )  ,  '" + arrayBOM1[v2, 15].ToString().Trim() + "' , '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
       + "   " + dqty + " ,  '" + arrayBOM1[v2, 15].ToString().Trim() + "' , '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
       + "   '" + tmpDate + "' , '" + sp1 + "' , '" + sp1 + "' , '" + arrayBOM1[v2, 30].ToString().Trim() + "' , '" + sp1 + "' ,"
       + "   '" + arrayBOM1[v2, 6].ToString().Trim() + "' ,  '" + Currtime + "' , '" + v5.ToString() + "' ,  "
       + "   " + dqty + " , '" + sp1 + "' , '" + arrayBOM1[v2, 4].ToString().Trim() + "' , '" + sp1 + "'  ) ";
                    v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                    if (v4 >= 0) // Successed, It could be not data
                        v4 = v4;
                }
            }

        } // end for loop check BOMTXT



        return ("");

    } // end ConvertBOM1

}  // end public class BJNLVMRPlib

}  // end namespace SFC.TJWEB


