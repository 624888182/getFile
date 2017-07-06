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



namespace SFC.TJWEB
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class SFCLinkTiptop
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

    public string ZRFC_WLBG_KIT_P0_22(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "", SN1 = "";
        string tmp0 = "", tmp1 = "", tmp2 = "", tmp3="", tmp4="", tmp5="", tmp6="", tmp7="", tmp8, tmp9;
        string tmp10 = "", tmp11 = "", tmp12 = "", tmp13="", tmp14="", tmp15="", tmp16="", tmp17="", tmp18, tmp19;
        string tmp20 = "", tmp21 = "", tmp22 = "", tmp23="", tmp24="", tmp25="", tmp26="", tmp27="", tmp28, tmp29;
        string tmp30 = "", tmp31 = "", tmp32 = "", tmp33="", tmp34="", tmp35="", tmp36="", tmp37="", tmp38, tmp39;
        string tmp40 = "", tmp41 = "", tmp42 = "", tmp43="", tmp44="", tmp45="", tmp46="", tmp47="", tmp48, tmp49;

        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "PUBLIB", Wridir = "TMP";

        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv3.ToUpper() != "") Wridir = pv3;
        else                     Wridir = "SAP";


        // Clear SET flag1 is space initial
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG1 = '' where ( FLAG1 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = '0' where ( FLAG2 is NULL  or  FLAG2 = '' ) ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG3 = '' where ( FLAG3 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);  
 


        // Clear Data , if some fileds = '' then update flag2= 'E' and flag3 = 'Y'
        tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = 'E', FLAG3 = 'Y'  "
                    + " where ( ( INVOICE_NUMBER = '" + sp + "' )   or ( ITEM_NUMBER = '" + sp + "' )  "
                    + " or ( CARTON_NUMBER = '" + sp + "' ) or ( NVOICE_NUMBER is NULL       )         "
                    + " or ( ITEM_NUMBER is NULL ) or ( CARTON_NUMBER is NULL )       ) ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);  
 
        // Check Unique INVOICE_NUMBER + CARTON_NUMBER in publib
        sqlr = "  SELECT  INVOICE_NUMBER, CARTON_NUMBER, ITEM_NUMBER from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL where ( (  FLAG3 is NULL ) or ( FLAG3 = '' ) ) "
            + " order by INVOICE_NUMBER, CARTON_NUMBER asc ";
        //+ " where ( ( ( FLAG3 is NULL ) or ( FLAG3 = '' ) ) and ( ( FLAG1 <> null ) and ( FLAG1 <>  '' ) ) "; // order by TO_CHAR( CREATE_DATE, 'yyyymmdd' ) desc  ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0");     // Not Data
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            tmp5 = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
            tmp6 = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString().Trim();
            tmp7 = DNdt.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();
            if ((tmp2 == tmp5) && (tmp3 == tmp6)) // 重覆錯務
            {
                tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = 'E', FLAG3 =  'Y'  "
                    + " where INVOICE_NUMBER = '" + tmp5 + "'  and CARTON_NUMBER = '" + tmp6 + "' and  ITEM_NUMBER = '" + tmp7 + "' ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);    
            }
            else
            {
                tmp2 = tmp5;
                tmp3 = tmp6;
                tmp4 = tmp7;
            }           

        }

        // Get Uniqueue DN from PACKl_ALL 
        sqlr = "  SELECT  distinct INVOICE_NUMBER, FLAG1, FLAG2  from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL where ( (  FLAG3 is NULL ) or ( FLAG3 = '' ) ) ";
        //+ " where ( ( ( FLAG3 is NULL ) or ( FLAG3 = '' ) ) and ( ( FLAG1 <> null ) and ( FLAG1 <>  '' ) ) "; // order by TO_CHAR( CREATE_DATE, 'yyyymmdd' ) desc  ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if ( DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if ( DNCnt <= 0 ) return ("0");  // No Data
        string[,] arrayDN = new string[ DNCnt + 1, 10 + 1];
        for (v1 = 0; v1 <= DNCnt ; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayDN[v1, v2] = "";

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayDN[v1 + 1, 1] = (v1 + 1).ToString().Trim();
            arrayDN[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
            arrayDN[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["FLAG1"].ToString().Trim();   // flag1
            arrayDN[v1 + 1, 4] = "";   // flag2 
            arrayDN[v1 + 1, 5] = "";   // flag3
            arrayDN[v1 + 1, 6] = "";   // 包裝已使用 Y 以用, "N" 為用, "N" 才能刪除
            //arrayDN[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString();  // CARTON_NUMBER   
            //arrayDN[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["INTERNAL_CARTON"].ToString();

        }
        // check in Real Data exist and put in this array
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            t02 = arrayDN[v1 + 1, 2].ToString(); //  = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString();
            sqlr = @"SELECT count(*) cnt  from  " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where INVOICE_NUMBER =  '" + t02 + "' ";
            dt3 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
            if (dt3 == null) v3 = 0;
            else v3 = Convert.ToInt32(dt3.Tables[0].Rows[0]["cnt"].ToString() );   //  Tables[0].Rows.Count;

            if (v3 > 0) arrayDN[v1 + 1, 4] = "Y";   // flag2 
            else        arrayDN[v1 + 1, 4] = "N";   // flag2   

            // 簡查如果 "C" 不能 INTERNAL_CARTON 有值
            sqlr = @"SELECT count(*) cnt  from  " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where ( ( INVOICE_NUMBER =  '" + t02 + "' ) and "
            + " ( ( INTERNAL_CARTON is not NULL ) or ( INVOICE_NUMBER <> '')  ) )";
            dt3 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
            if (dt3 == null) v3 = 0;
            else v3 = Convert.ToInt32(dt3.Tables[0].Rows[0]["cnt"].ToString());   //  Tables[0].Rows.Count;
            if (v3 > 0) arrayDN[v1 + 1, 6] = "Y";   // flag4 
            else arrayDN[v1 + 1, 6] = "N";   // flag4   

            if ((arrayDN[v1 + 1, 3] == "I") && (arrayDN[v1 + 1, 4] == "Y"))  // F;ag1 = "I", Flag2 = "Y", 表不須作業 
                arrayDN[v1 + 1, 5] = "Y";
            if ((arrayDN[v1 + 1, 3] == "C") && (arrayDN[v1 + 1, 6] == "Y"))  // F;ag1 = "I", Flag4 = "Y", 表不須作業, 包裝已使用, 不能刪除 
            {
                arrayDN[v1 + 1, 4] = "E";
                arrayDN[v1 + 1, 5] = "Y";
            }
        }

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            //t1 = arrayTrs[02, v1 + 1];  // INVOICE_NUMBER
            //t2 = arrayTrs[05, v1 + 1];  // ITEM_NUMBER
            //t3 = arrayTrs[07, v1 + 1];  // INTERNAL_CARTON

            t01 = arrayDN[v1 + 1, 1]; //= (v1 + 1).ToString();
            t02 = arrayDN[v1 + 1, 2]; //= DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString();
            t03 = arrayDN[v1 + 1, 3]; //= "";   // flag1
            t04 = arrayDN[v1 + 1, 4]; //= "";   // flag2 
            t05 = arrayDN[v1 + 1, 5]; //= "";   // flag3
            // Check in Read Data
            tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = '" + t04 + "', FLAG3 =  '" + t05 + "' "
                    + " where   ( (INVOICE_NUMBER = 'EM1-D60002' )  and  ( FLAG3 is NULL  or FLAG3 = '' ) and ( FLAG2 <> 'E' )  )  ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);               

        }

        // case flag1  flag2                                                            flag3
        // ===========================================================================================================
        // 1    I      Y        No Action                                               Y
        // 2    I      N        insert into sap from publib                             Y
        // 3    C      Y        delete Sap then insert sap from publib                  Y
        // 4    C      N                        insert sap from publib                  Y

        // Case 3 delete C in Write
        SN1 = "Y";
        sqlr = @"select * from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL  "
            + "  where ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG1 = 'C') and ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG2 = 'Y') "
            + "  and ( ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG3 is null  ) or  ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG3 = ''  ) ) ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null)  SN1 = "N";// Syn Error
        else  DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) SN1 = "N";     // Not Data

        if (SN1 == "Y") // 有資料
        {
            for (v1 = 0; v1 < DNCnt; v1++)
            {
                tmp5 = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
                tmp6 = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString().Trim();
                tmp7 = DNdt.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();
                tmpsqlW = @"delete   from " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where "
                          + " INVOICE_NUMBER = '" + tmp5 + "'  and CARTON_NUMBER = '" + tmp6 + "' and  ITEM_NUMBER = '" + tmp7 + "' ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            } // End For 
        } // End SN1      


        // Case 3
        //tmpsqlW = @"delete   from " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where  exists  (   "
        //    + " select * from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL  "
        //    + "  where ( ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG1 = 'C') and ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG2 = 'Y') "
        //    + "  and ( TMP.CMCS_SFC_PACKING_LINES_ALL.INVOICE_NUMBER = PUBLIB.CMCS_SFC_PACKING_LINES_ALL.INVOICE_NUMBER )    "
        //    + "  and ( ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG3 is null  ) or  ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG3 = ''  ) ) "
        //    + "  ) ) ";
        //v4 = PDataBaseOperation.PExecSQL("oracle", DBWriString, tmpsqlW);



        DateTime date1 = DateTime.Today;
        DateTime date2 = DateTime.Today;
        DateTime date3 = DateTime.Today;
        // Case 2,4 
        SN1 = "Y";
        sqlr = @"select *  from " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL where ( ( ( FLAG3 = '')  or ( FLAG3 is null ) ) and  ( FLAG2 <> 'E' ) ) ";         
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) SN1 = "N";// Syn Error
        else DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) SN1 = "N";     // Not Data

        if (SN1 != "Y")
            return (Ret1); // No Data有資料
        
            // Trim Data
            for (int i = 0; i < DNdt.Tables[0].Columns.Count; i++)
            {
                if (DNdt.Tables[0].Columns[i].DataType == typeof(string))
                {
                    for (int j = 0; j < DNdt.Tables[0].Rows.Count; j++)
                    {
                        DNdt.Tables[0].Rows[j][i] = DNdt.Tables[0].Rows[j][i].ToString().Trim();
                        tmp49 = DNdt.Tables[0].Rows[j][i].ToString().Trim();
                    }
                }
            }


            for (v1 = 0; v1 < DNCnt; v1++)
            {
                tmp4 = DNdt.Tables[0].Rows[v1]["PALLET_NUMBER"].ToString().Trim();
                tmp5 = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
                tmp6 = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString().Trim();
                tmp7 = DNdt.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();

                 sqlr = @"select *  from " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL "
                 + " where  INVOICE_NUMBER = '" + tmp5 + "'  and CARTON_NUMBER = '" + tmp6 + "' and  ITEM_NUMBER = '" + tmp7 + "'  ";  
                 dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
                 if ( ( dt1 != null) && ( dt1.Tables[0].Rows.Count == 0 ) ) // Not data in Wri Dir that can write
                 {
                      //tmp5 = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
                      //tmp6 = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString().Trim();
                      //tmp7 = DNdt.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();
                      tmp1 = DNdt.Tables[0].Rows[v1]["PALLET_NUMBER"].ToString().Trim();
                      tmp2 = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString().Trim();
                      tmp3 = DNdt.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();  
                   
                      tmp8  = DNdt.Tables[0].Rows[v1]["QUANTITY"].ToString().Trim();
                      tmp9  = DNdt.Tables[0].Rows[v1]["INTERNAL_CARTON"].ToString().Trim();
                      tmp10 = DNdt.Tables[0].Rows[v1]["CREATION_DATE"].ToString().Trim();
                      tmp11 = DNdt.Tables[0].Rows[v1]["LAST_UPDATE_DATE"].ToString().Trim();
                      tmp12 = DNdt.Tables[0].Rows[v1]["CUSTOMER_PO"].ToString().Trim();
                      tmp13 = DNdt.Tables[0].Rows[v1]["DESTINATION"].ToString().Trim();
                      tmp14 = DNdt.Tables[0].Rows[v1]["SHIP_DATE"].ToString().Trim();
                      tmp15 = DNdt.Tables[0].Rows[v1]["MARKET_NAME"].ToString().Trim();
                      tmp16 = DNdt.Tables[0].Rows[v1]["SHIP_TO_CUSTOMERNAME"].ToString().Trim();
                      tmp17 = DNdt.Tables[0].Rows[v1]["SHIP_TO_CITY"].ToString().Trim();
                      tmp18 = DNdt.Tables[0].Rows[v1]["SHIP_TO_COUNTRY"].ToString().Trim();
                      tmp19 = DNdt.Tables[0].Rows[v1]["SO_NO"].ToString().Trim();
                      tmp20 = DNdt.Tables[0].Rows[v1]["CUSTOMER_ITEM"].ToString().Trim();
                      tmp21 = DNdt.Tables[0].Rows[v1]["CORE_ID"].ToString().Trim();
                      tmp22 = DNdt.Tables[0].Rows[v1]["TANAPA"].ToString().Trim();
                      tmp23 = DNdt.Tables[0].Rows[v1]["SHIP_TYPE"].ToString().Trim();
                      tmp24 = DNdt.Tables[0].Rows[v1]["COMPANY"].ToString().Trim();
                      tmp25 = DNdt.Tables[0].Rows[v1]["PROJECT"].ToString().Trim();
                      tmp26 = DNdt.Tables[0].Rows[v1]["PALLET_NUMBER_NEW"].ToString().Trim();
                      tmp27 = DNdt.Tables[0].Rows[v1]["PLANT"].ToString().Trim();
                      tmp28 = DNdt.Tables[0].Rows[v1]["NET_WEIGHT"].ToString().Trim();
                      tmp29 = DNdt.Tables[0].Rows[v1]["GROSS_WEIGHT"].ToString().Trim();
                      tmp30 = DNdt.Tables[0].Rows[v1]["LENGTH"].ToString().Trim();
                      tmp31 = DNdt.Tables[0].Rows[v1]["WIDTH"].ToString().Trim();
                      tmp32 = DNdt.Tables[0].Rows[v1]["HIGTH"].ToString().Trim();
                      tmp33 = DNdt.Tables[0].Rows[v1]["DATE_CODE"].ToString().Trim();
                      tmp34 = DNdt.Tables[0].Rows[v1]["PALLET_WEIGTH"].ToString().Trim();
                      tmp35 = DNdt.Tables[0].Rows[v1]["CONTACT_NAME"].ToString().Trim();
                      tmp36 = DNdt.Tables[0].Rows[v1]["PHONE_NUMBER"].ToString().Trim();
                      tmp37 = DNdt.Tables[0].Rows[v1]["COMMENTS"].ToString().Trim();
                      tmp38 = DNdt.Tables[0].Rows[v1]["EMAIL_ADDRESS"].ToString().Trim();
                      tmp39 = DNdt.Tables[0].Rows[v1]["BUILD_PHASE"].ToString().Trim();
                      tmp40 = DNdt.Tables[0].Rows[v1]["SO_LINE_NO"].ToString().Trim();
                      tmp41 = DNdt.Tables[0].Rows[v1]["MANIFEST_ID"].ToString().Trim();
                      tmp42 = DNdt.Tables[0].Rows[v1]["WMS_TRUCK_NUMBER"].ToString().Trim();
                      tmp43 = DNdt.Tables[0].Rows[v1]["LOAD_NUMBER"].ToString().Trim();
                      tmp44 = DNdt.Tables[0].Rows[v1]["COUNTRY_CODE"].ToString().Trim();

                      //date1 = Convert.ToDateTime(DNdt.Tables[0].Rows[v1]["CREATION_DATE"].ToString().Trim()); // tmp10);
                      //date2 = Convert.ToDateTime(DNdt.Tables[0].Rows[v1]["LAST_UPDATE_DATE"].ToString().Trim()); // tmp11);
                      //date3 = Convert.ToDateTime(DNdt.Tables[0].Rows[v1]["SHIP_DATE"].ToString().Trim()); // tmp11);
                      // CREATION_DATE, LAST_UPDATE_DATE, is sysdate not need input

                      tmp45 = DNdt.Tables[0].Rows[v1][4].ToString().Trim();


                      tmpsqlW = @"insert into " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL ( INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, "
                     + " QUANTITY,   INTERNAL_CARTON, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, "
                     + " SHIP_TO_CUSTOMERNAME, SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY, "
                     + " PROJECT, PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE, PALLET_WEIGTH,  "
                     + " CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO, MANIFEST_ID, WMS_TRUCK_NUMBER,  "
                     + " LOAD_NUMBER, COUNTRY_CODE   )  " 
                     + " values (  '" + tmp5 + "', '" + tmp4 + "', '" + tmp6 + "', '" + tmp7 + "', "
                     //+ " '" + DNdt.Tables[0].Rows[v1][4] + "',   '" + DNdt.Tables[0].Rows[v1][5] + "', '" + date1 + "',  "
                     //+ " '" + date2 + "',   '" + DNdt.Tables[0].Rows[v1][8] + "', '" + DNdt.Tables[0].Rows[v1][9] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][4] + "',   '" + DNdt.Tables[0].Rows[v1][5] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][8] + "', '" + DNdt.Tables[0].Rows[v1][9] + "',  "
               //      + " '" + DNdt.Tables[0].Rows[v1][4] + "',   '" + DNdt.Tables[0].Rows[v1][5] + "', to_date('" + DNdt.Tables[0].Rows[v1][6].ToString() + "','yyyy/mm/dd HH24:mi:ss'),  "
               //      + " to_date('" + DNdt.Tables[0].Rows[v1][7].ToString() + "','yyyy/mm/dd HH24:mi:ss'),   '" + DNdt.Tables[0].Rows[v1][8] + "', '" + DNdt.Tables[0].Rows[v1][9] + "',  "
                     + " to_date('" + DNdt.Tables[0].Rows[v1][10] + "','yyyy/mm/dd HH24:mi:ss'),  '" + DNdt.Tables[0].Rows[v1][11] + "', '" + DNdt.Tables[0].Rows[v1][12] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][13] + "',  '" + DNdt.Tables[0].Rows[v1][14] + "', '" + DNdt.Tables[0].Rows[v1][15] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][16] + "',  '" + DNdt.Tables[0].Rows[v1][17] + "', '" + DNdt.Tables[0].Rows[v1][18] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][19] + "',  '" + DNdt.Tables[0].Rows[v1][20] + "', '" + DNdt.Tables[0].Rows[v1][21] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][22] + "',  '" + DNdt.Tables[0].Rows[v1][23] + "', '" + DNdt.Tables[0].Rows[v1][24] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][25] + "',  '" + DNdt.Tables[0].Rows[v1][26] + "', '" + DNdt.Tables[0].Rows[v1][27] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][28] + "',  '" + DNdt.Tables[0].Rows[v1][29] + "', '" + DNdt.Tables[0].Rows[v1][30] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][31] + "',  '" + DNdt.Tables[0].Rows[v1][32] + "', '" + DNdt.Tables[0].Rows[v1][33] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][34] + "',  '" + DNdt.Tables[0].Rows[v1][35] + "', '" + DNdt.Tables[0].Rows[v1][36] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][37] + "',  '" + DNdt.Tables[0].Rows[v1][38] + "', '" + DNdt.Tables[0].Rows[v1][39] + "',  "
                     + " '" + DNdt.Tables[0].Rows[v1][40] + "'    )  "; 
                     
                                   
                    

                     //+ " INVOICE_NUMBER = '" + tmp1 + "'  and CARTON_NUMBER = '" + tmp2 + "' and  ITEM_NUMBER = '" + tmp3 + "' "  
                     //+ " INVOICE_NUMBER = '" + tmp8 + "'  and CARTON_NUMBER = '" + tmp9 + "' and  ITEM_NUMBER = '" + tmp10 + "' "
                     //+ " INVOICE_NUMBER = '" + tmp11 + "'  and CARTON_NUMBER = '" + tmp12 + "' and  ITEM_NUMBER = '" + tmp13 + "' "     
                     //+ " INVOICE_NUMBER = '" + tmp14 + "'  and CARTON_NUMBER = '" + tmp15 + "' and  ITEM_NUMBER = '" + tmp16 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp17 + "'  and CARTON_NUMBER = '" + tmp18 + "' and  ITEM_NUMBER = '" + tmp19 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp20 + "'  and CARTON_NUMBER = '" + tmp21 + "' and  ITEM_NUMBER = '" + tmp22 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp23 + "'  and CARTON_NUMBER = '" + tmp24 + "' and  ITEM_NUMBER = '" + tmp25 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp26 + "'  and CARTON_NUMBER = '" + tmp27 + "' and  ITEM_NUMBER = '" + tmp18 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp29 + "'  and CARTON_NUMBER = '" + tmp30 + "' and  ITEM_NUMBER = '" + tmp31 + "' "   
                     //+ " INVOICE_NUMBER = '" + tmp32 + "'  and CARTON_NUMBER = '" + tmp33 + "' and  ITEM_NUMBER = '" + tmp34 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp35 + "'  and CARTON_NUMBER = '" + tmp36 + "' and  ITEM_NUMBER = '" + tmp37 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp38 + "'  and CARTON_NUMBER = '" + tmp39 + "' and  ITEM_NUMBER = '" + tmp40 + "' "      
                     //+ " INVOICE_NUMBER = '" + tmp41 + "'  and CARTON_NUMBER = '" + tmp42 + "' and  ITEM_NUMBER = '" + tmp43 + "' "      
                     //+ " COUNTRY_CODE = '" + tmp44 + "'  ";

                     v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                     if (v5 >= 0) // Successed, It could be not data
                     {
                         tmpsqlW = "Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG3 = 'Y' where "
                         + " INVOICE_NUMBER = '" + tmp5 + "'  and CARTON_NUMBER = '" + tmp6 + "' and  ITEM_NUMBER = '" + tmp7 + "'  ";
                         v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                         
                     }
                 }

                 if (v1 >= DNCnt) SN1 = "N"; 
            } // End For 
             
        
        // Case 2,4 
        //tmpsqlW = @"Insert into " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL A ( INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, "
        //+ " QUANTITY, INTERNAL_CARTON, CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
        //+ "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
        //+ "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
        //+ "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
        //+ "  LOAD_NUMBER, COUNTRY_CODE  )   ( select  INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, QUANTITY, INTERNAL_CARTON, "
        //+ "  CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
        //+ "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
        //+ "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
        //+ "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
        //+ "  LOAD_NUMBER, COUNTRY_CODE from " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL B  where  "
        //+ "  (  (   ( B.FLAG3 = '')  or ( B.FLAG3 is null ) ) and  ( B.FLAG2 <> 'E' )  )        ) ";   
        //v4 = PDataBaseOperation.PExecSQL("oracle", DBReadString, tmpsqlW);
        //if (v4 >= 0) // Successed, It could be not data
        //{
        //    tmpsqlW = "Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG3 = 'Y' where (( FLAG2 <> 'E' ) and (( FLAG3 = '')  or ( FLAG3 is null ))) ";
        //      v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        //    if (v4 != v5)  // Write Record Error
        //    {
        //        ErrFlag = "Insert Fail";
        //    }
        //}

        return (Ret1);


    }   //  end ZRFC_WLBG_KIT_P0_22

    public string ZRFC_WLBG_KIT_P0_2(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
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
        string Readdir = "PUBLIB", Wridir = "TMP";

        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv3.ToUpper() != "") Wridir = pv3;
        else Wridir = "SAP";


        // Clear SET flag1 is space initial
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG1 = '' where ( FLAG1 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = '0' where ( FLAG2 is NULL  or  FLAG2 = '' ) ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
        // tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG3 = '' where ( FLAG3 is NULL ) ";
        // v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);  



        // Clear Data , if some fileds = '' then update flag2= 'E' and flag3 = 'Y'
        tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = 'E', FLAG3 = 'Y'  "
                    + " where ( ( INVOICE_NUMBER = '" + sp + "' )   or ( ITEM_NUMBER = '" + sp + "' )  "
                    + " or ( CARTON_NUMBER = '" + sp + "' ) or ( NVOICE_NUMBER is NULL       )         "
                    + " or ( ITEM_NUMBER is NULL ) or ( CARTON_NUMBER is NULL )       ) ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);

        // Check Unique INVOICE_NUMBER + CARTON_NUMBER in publib
        sqlr = "  SELECT  INVOICE_NUMBER, CARTON_NUMBER, ITEM_NUMBER from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL where ( (  FLAG3 is NULL ) or ( FLAG3 = '' ) ) "
            + " order by INVOICE_NUMBER, CARTON_NUMBER asc ";
        //+ " where ( ( ( FLAG3 is NULL ) or ( FLAG3 = '' ) ) and ( ( FLAG1 <> null ) and ( FLAG1 <>  '' ) ) "; // order by TO_CHAR( CREATE_DATE, 'yyyymmdd' ) desc  ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0");     // Not Data
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            tmp5 = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
            tmp6 = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString().Trim();
            tmp7 = DNdt.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();
            if ((tmp2 == tmp5) && (tmp3 == tmp6)) // 重覆錯務
            {
                tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = 'E', FLAG3 =  'Y'  "
                    + " where INVOICE_NUMBER = '" + tmp5 + "'  and CARTON_NUMBER = '" + tmp6 + "' and  ITEM_NUMBER = '" + tmp7 + "' ";
                v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
            }
            else
            {
                tmp2 = tmp5;
                tmp3 = tmp6;
                tmp4 = tmp7;
            }

        }

        // Get Uniqueue DN from PACKl_ALL 
        sqlr = "  SELECT  distinct INVOICE_NUMBER, FLAG1, FLAG2  from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL where ( (  FLAG3 is NULL ) or ( FLAG3 = '' ) ) ";
        //+ " where ( ( ( FLAG3 is NULL ) or ( FLAG3 = '' ) ) and ( ( FLAG1 <> null ) and ( FLAG1 <>  '' ) ) "; // order by TO_CHAR( CREATE_DATE, 'yyyymmdd' ) desc  ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt <= 0) return ("0");  // No Data
        string[,] arrayDN = new string[DNCnt + 1, 10 + 1];
        for (v1 = 0; v1 <= DNCnt; v1++)
            for (v2 = 0; v2 <= 10; v2++)
                arrayDN[v1, v2] = "";

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            arrayDN[v1 + 1, 1] = (v1 + 1).ToString().Trim();
            arrayDN[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
            arrayDN[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["FLAG1"].ToString().Trim();   // flag1
            arrayDN[v1 + 1, 4] = "";   // flag2 
            arrayDN[v1 + 1, 5] = "";   // flag3
            arrayDN[v1 + 1, 6] = "";   // 包裝已使用 Y 以用, "N" 為用, "N" 才能刪除
            //arrayDN[v1 + 1, 6] = DNdt.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString();  // CARTON_NUMBER   
            //arrayDN[v1 + 1, 7] = DNdt.Tables[0].Rows[v1]["INTERNAL_CARTON"].ToString();

        }
        // check in Real Data exist and put in this array
        for (v1 = 0; v1 < DNCnt; v1++)
        {
            t02 = arrayDN[v1 + 1, 2].ToString(); //  = DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString();
            sqlr = @"SELECT count(*) cnt  from  " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where INVOICE_NUMBER =  '" + t02 + "' ";
            dt3 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
            if (dt3 == null) v3 = 0;
            else v3 = Convert.ToInt32(dt3.Tables[0].Rows[0]["cnt"].ToString());   //  Tables[0].Rows.Count;

            if (v3 > 0) arrayDN[v1 + 1, 4] = "Y";   // flag2 
            else arrayDN[v1 + 1, 4] = "N";   // flag2   

            // 簡查如果 "C" 不能 INTERNAL_CARTON 有值
            sqlr = @"SELECT count(*) cnt  from  " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where ( ( INVOICE_NUMBER =  '" + t02 + "' ) and "
            + " ( ( INTERNAL_CARTON is not NULL ) or ( INVOICE_NUMBER <> '')  ) )";
            dt3 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlr);
            if (dt3 == null) v3 = 0;
            else v3 = Convert.ToInt32(dt3.Tables[0].Rows[0]["cnt"].ToString());   //  Tables[0].Rows.Count;
            if (v3 > 0) arrayDN[v1 + 1, 6] = "Y";   // flag4 
            else arrayDN[v1 + 1, 6] = "N";   // flag4   

            if ((arrayDN[v1 + 1, 3] == "I") && (arrayDN[v1 + 1, 4] == "Y"))  // F;ag1 = "I", Flag2 = "Y", 表不須作業 
                arrayDN[v1 + 1, 5] = "Y";
            if ((arrayDN[v1 + 1, 3] == "C") && (arrayDN[v1 + 1, 6] == "Y"))  // F;ag1 = "I", Flag4 = "Y", 表不須作業, 包裝已使用, 不能刪除 
            {
                arrayDN[v1 + 1, 4] = "E";
                arrayDN[v1 + 1, 5] = "Y";
            }
        }

        for (v1 = 0; v1 < DNCnt; v1++)
        {
            //t1 = arrayTrs[02, v1 + 1];  // INVOICE_NUMBER
            //t2 = arrayTrs[05, v1 + 1];  // ITEM_NUMBER
            //t3 = arrayTrs[07, v1 + 1];  // INTERNAL_CARTON

            t01 = arrayDN[v1 + 1, 1]; //= (v1 + 1).ToString();
            t02 = arrayDN[v1 + 1, 2]; //= DNdt.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString();
            t03 = arrayDN[v1 + 1, 3]; //= "";   // flag1
            t04 = arrayDN[v1 + 1, 4]; //= "";   // flag2 
            t05 = arrayDN[v1 + 1, 5]; //= "";   // flag3
            // Check in Read Data
            tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = '" + t04 + "', FLAG3 =  '" + t05 + "' "
                    + " where   ( (INVOICE_NUMBER = 'EM1-D60002' )  and  ( FLAG3 is NULL  or FLAG3 = '' ) and ( FLAG2 <> 'E' )  )  ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);

        }

        // case flag1  flag2                                                            flag3
        // ===========================================================================================================
        // 1    I      Y        No Action                                               Y
        // 2    I      N        insert into sap from publib                             Y
        // 3    C      Y        delete Sap then insert sap from publib                  Y
        // 4    C      N                        insert sap from publib                  Y



        // Case 3
        tmpsqlW = @"delete   from " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where  exists  (   "
            + " select * from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL  "
            + "  where ( ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG1 = 'C') and ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG2 = 'Y') "
            + "  and ( TMP.CMCS_SFC_PACKING_LINES_ALL.INVOICE_NUMBER = PUBLIB.CMCS_SFC_PACKING_LINES_ALL.INVOICE_NUMBER )    "
            + "  and ( ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG3 is null  ) or  ( PUBLIB.CMCS_SFC_PACKING_LINES_ALL.FLAG3 = ''  ) ) "
            + "  ) ) ";
        v4 = PDataBaseOperation.PExecSQL("oracle", DBReadString, tmpsqlW);


        // Case 2,4 Insert 20130907
        tmpsqlW = @"Insert into " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL A ( INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, "
        + " QUANTITY, INTERNAL_CARTON, CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
        + "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
        + "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
        + "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
        + "  LOAD_NUMBER, COUNTRY_CODE  )   ( select  INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, QUANTITY, INTERNAL_CARTON, "
        + "  CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
        + "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
        + "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
        + "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
        + "  LOAD_NUMBER, COUNTRY_CODE from " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL B  where  "
        + "  (  (   ( B.FLAG3 = '')  or ( B.FLAG3 is null ) ) and  ( B.FLAG2 <> 'E' )  )        ) ";

        v4 = PDataBaseOperation.PExecSQL("oracle", DBReadString, tmpsqlW);
        if (v4 >= 0) // Successed, It could be not data
        {
            tmpsqlW = "Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG3 = 'Y' where (( FLAG2 <> 'E' ) and (( FLAG3 = '')  or ( FLAG3 is null ))) ";

            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            if (v4 != v5)  // Write Record Error
            {
                ErrFlag = "Insert Fail";
            }
        }

        return (Ret1);


    }   //  end ZRFC_WLBG_KIT_P0_2



    public string ZRFC_WLBG_KIT_P0_2BACK1( string BSite , string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlr = "",  sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo="", tdn="", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "";
        Decimal d1=0, d2=0, d3=0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "PUBLIB", Wridir = "TMP";

        string flag = "Y";
        // Get Data
      //  sqlr = "  SELECT  * from PUBLIB.CMCS_SFC_PACKING_LINES_ALL  where ( ( FLAG1 is null ) or ( FLAG1 <> 'Y' ) ) "; // order by TO_CHAR( CREATE_DATE, 'yyyymmdd' ) desc  ";
        sqlr = "  SELECT  * from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL  "; // order by TO_CHAR( CREATE_DATE, 'yyyymmdd' ) desc  ";
        dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (dt1 == null) return ("-1");
        v3 = dt1.Tables[0].Rows.Count;
        int arrTrslong = v3, arrTrswidth = 50;
        string[,] arrayTrs = new string[arrTrswidth + 1, arrTrslong + 1];

        for (v1 = 0; v1 < arrTrslong; v1++)
            for (v2 = 0; v2 < arrTrswidth; v2++)
                arrayTrs[v2, v1] = "";

        for (v1 = 0; v1 < arrTrslong; v1++)
        {
            arrayTrs[01, v1 + 1] = "";  // write flag
            arrayTrs[02, v1 + 1] = dt1.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString().Trim();
            arrayTrs[03, v1 + 1] = dt1.Tables[0].Rows[v1]["PALLET_NUMBER"].ToString().Trim();
            arrayTrs[04, v1 + 1] = dt1.Tables[0].Rows[v1]["CARTON_NUMBER"].ToString().Trim();
            arrayTrs[05, v1 + 1] = dt1.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();
            t05 = dt1.Tables[0].Rows[v1]["ITEM_NUMBER"].ToString().Trim();
            arrayTrs[06, v1 + 1] = dt1.Tables[0].Rows[v1]["QUANTITY"].ToString().Trim();
            arrayTrs[07, v1 + 1] = dt1.Tables[0].Rows[v1]["INTERNAL_CARTON"].ToString().Trim();
            arrayTrs[08, v1 + 1] = dt1.Tables[0].Rows[v1]["CREATION_DATE"].ToString().Trim();
            arrayTrs[09, v1 + 1] = dt1.Tables[0].Rows[v1]["LAST_UPDATE_DATE"].ToString().Trim();
            arrayTrs[10, v1 + 1] = dt1.Tables[0].Rows[v1]["CUSTOMER_PO"].ToString().Trim();
            arrayTrs[11, v1 + 1] = dt1.Tables[0].Rows[v1]["DESTINATION"].ToString().Trim();
            arrayTrs[12, v1 + 1] = dt1.Tables[0].Rows[v1]["SHIP_DATE"].ToString().Trim();
            arrayTrs[13, v1 + 1] = dt1.Tables[0].Rows[v1]["MARKET_NAME"].ToString().Trim();
            arrayTrs[14, v1 + 1] = dt1.Tables[0].Rows[v1]["SHIP_TO_CUSTOMERNAME"].ToString().Trim();
            arrayTrs[15, v1 + 1] = dt1.Tables[0].Rows[v1]["SHIP_TO_CITY"].ToString().Trim();
            arrayTrs[16, v1 + 1] = dt1.Tables[0].Rows[v1]["SHIP_TO_COUNTRY"].ToString().Trim();
            arrayTrs[17, v1 + 1] = dt1.Tables[0].Rows[v1]["SO_NO"].ToString().Trim();
            arrayTrs[18, v1 + 1] = dt1.Tables[0].Rows[v1]["CUSTOMER_ITEM"].ToString().Trim();
            arrayTrs[19, v1 + 1] = dt1.Tables[0].Rows[v1]["CORE_ID"].ToString().Trim();
            arrayTrs[20, v1 + 1] = dt1.Tables[0].Rows[v1]["TANAPA"].ToString().Trim();
            arrayTrs[21, v1 + 1] = dt1.Tables[0].Rows[v1]["SHIP_TYPE"].ToString().Trim();
            arrayTrs[22, v1 + 1] = dt1.Tables[0].Rows[v1]["COMPANY"].ToString().Trim();
            arrayTrs[23, v1 + 1] = dt1.Tables[0].Rows[v1]["PROJECT"].ToString().Trim();
            arrayTrs[24, v1 + 1] = dt1.Tables[0].Rows[v1]["PALLET_NUMBER_NEW"].ToString().Trim();
            arrayTrs[25, v1 + 1] = dt1.Tables[0].Rows[v1]["PLANT"].ToString().Trim();
            arrayTrs[26, v1 + 1] = dt1.Tables[0].Rows[v1]["NET_WEIGHT"].ToString().Trim();
            arrayTrs[27, v1 + 1] = dt1.Tables[0].Rows[v1]["GROSS_WEIGHT"].ToString().Trim();
            arrayTrs[28, v1 + 1] = dt1.Tables[0].Rows[v1]["LENGTH"].ToString().Trim();
            arrayTrs[29, v1 + 1] = dt1.Tables[0].Rows[v1]["WIDTH"].ToString().Trim();
            arrayTrs[30, v1 + 1] = dt1.Tables[0].Rows[v1]["HIGTH"].ToString().Trim();
            arrayTrs[31, v1 + 1] = dt1.Tables[0].Rows[v1]["DATE_CODE"].ToString().Trim();
            arrayTrs[32, v1 + 1] = dt1.Tables[0].Rows[v1]["PALLET_WEIGTH"].ToString().Trim();
            arrayTrs[33, v1 + 1] = dt1.Tables[0].Rows[v1]["CONTACT_NAME"].ToString().Trim();
            arrayTrs[34, v1 + 1] = dt1.Tables[0].Rows[v1]["PHONE_NUMBER"].ToString().Trim();
            arrayTrs[35, v1 + 1] = dt1.Tables[0].Rows[v1]["COMMENTS"].ToString().Trim();
            arrayTrs[36, v1 + 1] = dt1.Tables[0].Rows[v1]["EMAIL_ADDRESS"].ToString().Trim();
            arrayTrs[37, v1 + 1] = dt1.Tables[0].Rows[v1]["BUILD_PHASE"].ToString().Trim();
            arrayTrs[38, v1 + 1] = dt1.Tables[0].Rows[v1]["SO_LINE_NO"].ToString().Trim();
            arrayTrs[39, v1 + 1] = dt1.Tables[0].Rows[v1]["MANIFEST_ID"].ToString().Trim();
            arrayTrs[30, v1 + 1] = dt1.Tables[0].Rows[v1]["WMS_TRUCK_NUMBER"].ToString().Trim();
            arrayTrs[41, v1 + 1] = dt1.Tables[0].Rows[v1]["LOAD_NUMBER"].ToString().Trim();
            arrayTrs[42, v1 + 1] = dt1.Tables[0].Rows[v1]["COUNTRY_CODE"].ToString().Trim();
            arrayTrs[43, v1 + 1] = dt1.Tables[0].Rows[v1]["FLAG1"].ToString().Trim();
            arrayTrs[44, v1 + 1] = dt1.Tables[0].Rows[v1]["FLAG2"].ToString().Trim();
            arrayTrs[45, v1 + 1] = dt1.Tables[0].Rows[v1]["FLAG3"].ToString().Trim();
            arrayTrs[46, v1 + 1] = "";

            if ((arrayTrs[02, v1 + 1].ToString() == null) || (arrayTrs[02, v1 + 1].ToString() == ""))
                arrayTrs[46, v1 + 1] = "E";  // Error Data is space
            if ((arrayTrs[05, v1 + 1].ToString() == null) || (arrayTrs[05, v1 + 1].ToString() == ""))
                arrayTrs[46, v1 + 1] = "E";  // Error Data is space
            if ((arrayTrs[07, v1 + 1].ToString() == null) || (arrayTrs[07, v1 + 1].ToString() == ""))
                arrayTrs[46, v1 + 1] = "E";  // Error Data is space
            
            // Test Only
            if ((arrayTrs[05, v1 + 1].ToString() == null) || (arrayTrs[05, v1 + 1].ToString() == "11DMPWW0A01"))
                arrayTrs[46, v1 + 1] = "E";  // Error Data is space

          

        }

        string t1="", t2="", t3="", t4="";

        // Check Publin Data in Sap Sata or not
        for (v1 = 0; v1 < arrTrslong; v1++)
        {
            t1 = arrayTrs[02, v1 + 1];  // INVOICE_NUMBER
            t2 = arrayTrs[05, v1 + 1];  // ITEM_NUMBER
            t3 = arrayTrs[07, v1 + 1];  // INTERNAL_CARTON

            sqlw = "  SELECT  * from " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL  where INVOICE_NUMBER = '" + arrayTrs[02, v1 + 1] + "' and  "
                 + " ITEM_NUMBER = '" + arrayTrs[05, v1 + 1] + "' and  INTERNAL_CARTON = '" + arrayTrs[07, v1 + 1] + "'  ";
            dt2 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlw);
            if (dt2 == null) return ("-1");
            v4 = dt2.Tables[0].Rows.Count;
            if (v4 == 0) t4 = "N"; else t4 = "Y";
            if ( arrayTrs[46, v1 + 1] == "E" ) t4 = arrayTrs[46, v1 + 1]; // 錯誤最大, 或 Y:Exits, N:No Exist
            arrayTrs[44, v1 + 1] = t4; // dt1.Tables[0].Rows[v1]["FLAG2"].ToString();
            tmpsqlW = " Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG2 = '" + t4 + "'  where INVOICE_NUMBER = '" + arrayTrs[02, v1 + 1] + "' and "
                        + " ITEM_NUMBER = '" + arrayTrs[05, v1 + 1] + "' and  INTERNAL_CARTON = '" + arrayTrs[07, v1 + 1] + "'  ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBReadString, tmpsqlW);
                        
            // flag1 mean update ok flag  "Y"
            // flag2 mean data exist in Server "Y"      

        }

        // case flag1  flag2                                                            flag3
        // ===========================================================================================================
        // 1    I      Y        No Action                                               Y
        // 2    I      N        insert into sap from publib                             Y
        // 3    C      Y        delete Sap then insert sap from publib                  Y
        // 4    C      N                        insert sap from publib                  Y



        // Case 3
        tmpsqlW = @"delete   from " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL where  exists  (   "
            + " select * from  " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL  "
            + " where ( ( " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL.FLAG1 = 'C') and ( " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL.FLAG2 = 'Y')  "
            + " and ( " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL.INVOICE_NUMBER = " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL.INVOICE_NUMBER )  "
            + " and ( " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL.ITEM_NUMBER = " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL.ITEM_NUMBER )  "
            + " and ( " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL.INTERNAL_CARTON = " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL.INTERNAL_CARTON ) "
            + " and ( " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL.FLAG3 <> 'Y'   ) "
            + " ) ) ";  
        v4 = PDataBaseOperation.PExecSQL("oracle", DBReadString, tmpsqlW);
        //if (v4 > 0) // Successed
        //{
        //    tmpsqlW = "Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG3 = 'Y' where ( ( FLAG1 = 'C') and ( FLAG2 = 'Y')) ";
        //    v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
        //    if ( v5 <= 0 )  // Write Record Error
        //    {
        //        ErrFlag = "Update Fail";
        //    }
        //}


        // Case 2,4 
        tmpsqlW = @"Insert into " + Wridir + ".CMCS_SFC_PACKING_LINES_ALL A ( INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, "
        + " QUANTITY, INTERNAL_CARTON, CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
        + "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
        + "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
        + "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
        + "  LOAD_NUMBER, COUNTRY_CODE  )   ( select  INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, QUANTITY, INTERNAL_CARTON, "
        + "  CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
        + "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
        + "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
        + "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
        + "  LOAD_NUMBER, COUNTRY_CODE from " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL B  where  "
        + " ( ( ( B.FLAG1 = 'I' ) and ( B.FLAG2 = 'N' )  and ( B.FLAG3 <> 'Y' ) ) or "
        //+ "  ( ( B.FLAG1 = 'C' ) and ( B.FLAG2 = 'N' )  and ( B.FLAG3 <> 'Y' ) ) ) )";
        + "  ( ( B.FLAG1 = 'C' ) and ( B.FLAG3 <> 'Y' ) ) ) )";
        v4 = PDataBaseOperation.PExecSQL("oracle", DBReadString, tmpsqlW);
        if (v4 > 0) // Successed
        {
            tmpsqlW = "Update " + Readdir + ".CMCS_SFC_PACKING_LINES_ALL set FLAG3 = 'Y' where ( ( ( FLAG1 = 'I' ) and ( FLAG2 = 'N' ) and ( FLAG3 <> 'Y' )) "
                    + " or ( ( FLAG1 = 'C' ) and ( FLAG3 <> 'N' )  ) )   ";
            v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
            if (v4 != v5)  // Write Record Error
            {
                ErrFlag = "Insert Fail";
            }
        }


      //  tmpsqlW = @"Update  SAP.CMCS_SFC_PACKING_LINES_ALL A ( INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, "
      //  + " QUANTITY, INTERNAL_CARTON, CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
      //  + "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
      //  + "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
      //  + "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
      //  + "  LOAD_NUMBER, COUNTRY_CODE  )   ( select  INVOICE_NUMBER, PALLET_NUMBER, CARTON_NUMBER, ITEM_NUMBER, QUANTITY, INTERNAL_CARTON, "
      //  + "  CREATION_DATE, LAST_UPDATE_DATE, CUSTOMER_PO, DESTINATION, SHIP_DATE, MARKET_NAME, SHIP_TO_CUSTOMERNAME,  "
      //  + "  SHIP_TO_CITY, SHIP_TO_COUNTRY, SO_NO, CUSTOMER_ITEM, CORE_ID, TANAPA, SHIP_TYPE, COMPANY,  PROJECT, "
      //  + "  PALLET_NUMBER_NEW, PLANT, NET_WEIGHT, GROSS_WEIGHT, LENGTH, WIDTH, HIGTH, DATE_CODE,  PALLET_WEIGTH, "
      //  + "  CONTACT_NAME, PHONE_NUMBER, COMMENTS, EMAIL_ADDRESS, BUILD_PHASE, SO_LINE_NO,  MANIFEST_ID, WMS_TRUCK_NUMBER, "
      //  + "  LOAD_NUMBER, COUNTRY_CODE from PUBLIB.CMCS_SFC_PACKING_LINES_ALL B    where ( ( B.FLAG1 = 'Y') and ( B.FLAG2 = 'Y')  ) ) ";
      //  v4 = PDataBaseOperation.PExecSQL("oracle", DBReadString, tmpsqlW);
      //  if (v4 > 0) // Successed
      //  {
      //      tmpsqlW = "Update PUBLIB.CMCS_SFC_PACKING_LINES_ALL set FLAG3 = 'Y' where ( ( B.FLAG1 = 'Y') and ( B.FLAG2 = 'Y')  ) ";
      //      v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
      //      if (v4 != v5)  // Write Record Error
      //      {
      //          ErrFlag = "Update Fail";
      //      }
      //  }


        return (Ret1);       
        
    
    }   //  end ZRFC_WLBG_KIT_P0_2        

    //publib.SAP_WO_LIST
    //T_ZQM_HEADE_STRU 20130722
    public string T_ZQM_HEADE_STRU_2(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {

        string Ret1 = "", sqlpublib = "", sqlsap = "";
        string PLANT, WO_NO, STATUS_TYPE, ASSEMBLY_ITEM, CLASS_CODE, START_QTY, CREATION_DATE, BASE_START_DATE, BASE_FINISH_DATE, BOM_REVISION, SO_NO, SO_LINE_NO, PROJECT_CODE, SCH, FLAG1;
        DataSet dspublib = null;
        int v1 = 0, v2 = 0, daycnt = 1, idet = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "PUBLIB", Wridir = "TMP";

        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv3.ToUpper() != "") Wridir = pv3;  // " + Wridir + "
        else Wridir = "SAP";
        
        sqlpublib = "select * from publib.SAP_WO_LIST where FLAG3 is null or FLAG3 = '' "; //36030275
        //sqlpublib = "select * from publib.SAP_WO_LIST where wo_no='WM7-D60002' or wo_no='WMa-D60010'";

        dspublib = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlpublib);
        if (dspublib == null) return ("-1");
        v1 = dspublib.Tables[0].Rows.Count;
        if (v1 == 0) return ("0");
        int arrWOlong = v1, arrWOwidth = 17;
        string[,] arrWO = new string[arrWOlong + 1, arrWOwidth + 1];
        for (int i = 0; i < arrWOlong; i++)
            for (int j = 0; j < arrWOwidth; j++)
                arrWO[i, j] = "";
        for (int i = 0; i < arrWOlong; i++)
        {
            arrWO[i, 0] = dspublib.Tables[0].Rows[i]["PLANT"].ToString().Trim();
            arrWO[i, 1] = dspublib.Tables[0].Rows[i]["WO_NO"].ToString().Trim();
            arrWO[i, 2] = dspublib.Tables[0].Rows[i]["STATUS_TYPE"].ToString().Trim();
            arrWO[i, 3] = dspublib.Tables[0].Rows[i]["ASSEMBLY_ITEM"].ToString().Trim();
            arrWO[i, 4] = dspublib.Tables[0].Rows[i]["CLASS_CODE"].ToString().Trim();
            arrWO[i, 5] = dspublib.Tables[0].Rows[i]["START_QTY"].ToString().Trim();
            arrWO[i, 6] = dspublib.Tables[0].Rows[i]["CREATION_DATE"].ToString().Trim();
            arrWO[i, 7] = dspublib.Tables[0].Rows[i]["BASE_START_DATE"].ToString().Trim();
            arrWO[i, 8] = dspublib.Tables[0].Rows[i]["BASE_FINISH_DATE"].ToString().Trim();
            arrWO[i, 9] = dspublib.Tables[0].Rows[i]["BOM_REVISION"].ToString().Trim();
            arrWO[i, 10] = dspublib.Tables[0].Rows[i]["SO_NO"].ToString().Trim();
            arrWO[i, 11] = dspublib.Tables[0].Rows[i]["SO_LINE_NO"].ToString().Trim();
            arrWO[i, 12] = dspublib.Tables[0].Rows[i]["PROJECT_CODE"].ToString().Trim();
            arrWO[i, 13] = dspublib.Tables[0].Rows[i]["SCH"].ToString().Trim();
            arrWO[i, 14] = dspublib.Tables[0].Rows[i]["FLAG1"].ToString().Trim();
            arrWO[i, 15] = dspublib.Tables[0].Rows[i]["FLAG2"].ToString().Trim();
            arrWO[i, 16] = dspublib.Tables[0].Rows[i]["FLAG3"].ToString().Trim();
        }

        for (int j = 0; j < arrWOlong; j++)
        {
            PLANT = ""; WO_NO = ""; STATUS_TYPE = ""; ASSEMBLY_ITEM = ""; CLASS_CODE = ""; START_QTY = ""; CREATION_DATE = ""; BASE_START_DATE = ""; BASE_FINISH_DATE = ""; BOM_REVISION = ""; SO_NO = ""; SO_LINE_NO = ""; PROJECT_CODE = ""; SCH = ""; FLAG1 = "";
            sqlsap = ""; idet = 0;

            PLANT = arrWO[j, 0];
            WO_NO = arrWO[j, 1];
            STATUS_TYPE = arrWO[j, 2];
            ASSEMBLY_ITEM = arrWO[j, 3];
            CLASS_CODE = arrWO[j, 4];
            START_QTY = arrWO[j, 5];
            CREATION_DATE = (DateTime.Parse(arrWO[j, 6])).ToString("yyyy-MM-dd HH:mm:ss");
            BASE_START_DATE = (DateTime.Parse(arrWO[j, 7])).ToString("yyyy-MM-dd HH:mm:ss");
            BASE_FINISH_DATE = (DateTime.Parse(arrWO[j, 8])).ToString("yyyy-MM-dd HH:mm:ss");
            BOM_REVISION = arrWO[j, 9];
            SO_NO = arrWO[j, 10];
            SO_LINE_NO = arrWO[j, 11];
            PROJECT_CODE = arrWO[j, 12];
            SCH = arrWO[j, 13];
            FLAG1 = arrWO[j, 14].Trim();

            if (WO_NO.Trim() == "")
            {
                string upstr = "update PUBLIB.SAP_WO_LIST set FLAG2='E' where WO_NO='" + WO_NO + "'";

                int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);

                continue;
            }

            string sql = "select * from " + Wridir + ".SAP_WO_LIST where WO_NO='" + WO_NO + "'";
            //string sql = "select * from TMP.SAP_WO_LIST where WO_NO='" + WO_NO + "'";
            // " + Wridir + "

            DataSet dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sql);

            if (FLAG1 == "" || FLAG1 == "I")
            {
                //string sql = "select * from SAP.SAP_WO_LIST where WO_NO='" + WO_NO + "'";


                if (dt1.Tables[0].Rows.Count > 0)
                {
                    //string destr = "delete from SAP.SAP_WO_LIST where WO_NO='" + WO_NO + "'";

                    string upstr = "update PUBLIB.SAP_WO_LIST set FLAG2='Y' where WO_NO='" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);

                    idet = 1;
                }


                else
                {
                    //sqlsap = " insert into SAP.SAP_WO_LIST(PLANT,WO_NO,STATUS_TYPE,ASSEMBLY_ITEM,CLASS_CODE,START_QTY,CREATION_DATE,BASE_START_DATE,BASE_FINISH_DATE,"
                    //         + " BOM_REVISION,SO_NO,SO_LINE_NO,PROJECT_CODE,SCH)"
                    //         + " values('" + PLANT + "','" + WO_NO + "','" + STATUS_TYPE + "','" + ASSEMBLY_ITEM + "','" + CLASS_CODE + "','" + START_QTY + "',"
                    //         + "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_START_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_FINISH_DATE + "','yyyy/mm/dd HH24:mi:ss'),"
                    //         + "'" + BOM_REVISION + "','" + SO_NO + "','" + SO_LINE_NO + "','" + PROJECT_CODE + "','" + SCH + "') ";


                    sqlsap = " insert into " + Wridir + ".SAP_WO_LIST(PLANT,WO_NO,STATUS_TYPE,ASSEMBLY_ITEM,CLASS_CODE,START_QTY,CREATION_DATE,BASE_START_DATE,BASE_FINISH_DATE,"
                             + " BOM_REVISION,SO_NO,SO_LINE_NO,PROJECT_CODE,SCH)"
                             + " values('" + PLANT + "','" + WO_NO + "','" + STATUS_TYPE + "','" + ASSEMBLY_ITEM + "','" + CLASS_CODE + "','" + START_QTY + "',"
                             + "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_START_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_FINISH_DATE + "','yyyy/mm/dd HH24:mi:ss'),"
                             + "'" + BOM_REVISION + "','" + SO_NO + "','" + SO_LINE_NO + "','" + PROJECT_CODE + "','" + SCH + "') ";

                    idet = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlsap);

                    string upstr = "update PUBLIB.SAP_WO_LIST set FLAG2='N' where WO_NO='" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);
                }
            }
            else if (FLAG1 == "C")
            {


                if (dt1.Tables[0].Rows.Count > 0)
                {
                    //string destr = "delete from SAP.SAP_WO_LIST where WO_NO='" + WO_NO + "'";
                    string destr = "delete from " + Wridir + ".SAP_WO_LIST where WO_NO='" + WO_NO + "'";

                    int deidet = PDataBaseOperation.PExecSQL(DBType, DBWriString, destr);

                    string upstr = "update PUBLIB.SAP_WO_LIST set FLAG2='Y' where WO_NO='" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);
                }
                else
                {
                    string upstr = "update PUBLIB.SAP_WO_LIST set FLAG2='N' where WO_NO='" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);
                }

                //sqlsap = " insert into SAP.SAP_WO_LIST(PLANT,WO_NO,STATUS_TYPE,ASSEMBLY_ITEM,CLASS_CODE,START_QTY,CREATION_DATE,BASE_START_DATE,BASE_FINISH_DATE,"
                //         + " BOM_REVISION,SO_NO,SO_LINE_NO,PROJECT_CODE,SCH)"
                //         + " values('" + PLANT + "','" + WO_NO + "','" + STATUS_TYPE + "','" + ASSEMBLY_ITEM + "','" + CLASS_CODE + "','" + START_QTY + "',"
                //         + "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_START_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_FINISH_DATE + "','yyyy/mm/dd HH24:mi:ss'),"
                //         + "'" + BOM_REVISION + "','" + SO_NO + "','" + SO_LINE_NO + "','" + PROJECT_CODE + "','" + SCH + "') ";


                sqlsap = " insert into " + Wridir + ".SAP_WO_LIST(PLANT,WO_NO,STATUS_TYPE,ASSEMBLY_ITEM,CLASS_CODE,START_QTY,CREATION_DATE,BASE_START_DATE,BASE_FINISH_DATE,"
                         + " BOM_REVISION,SO_NO,SO_LINE_NO,PROJECT_CODE,SCH)"
                         + " values('" + PLANT + "','" + WO_NO + "','" + STATUS_TYPE + "','" + ASSEMBLY_ITEM + "','" + CLASS_CODE + "','" + START_QTY + "',"
                         + "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_START_DATE + "','yyyy/mm/dd HH24:mi:ss'),to_date('" + BASE_FINISH_DATE + "','yyyy/mm/dd HH24:mi:ss'),"
                         + "'" + BOM_REVISION + "','" + SO_NO + "','" + SO_LINE_NO + "','" + PROJECT_CODE + "','" + SCH + "') ";

                idet = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlsap);
            }


            if (idet > 0)
            {
                string sql2 = "update PUBLIB.SAP_WO_LIST set FLAG3='Y' where WO_NO='" + WO_NO + "'";
                int idet1 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sql2);
                v2 += idet1;
            }
        }
        Ret1 = v2.ToString();
        return (Ret1);

    }

    //publib.SAP_WO_COMP
    //T_ZQM_COMPONENT_STRU Test 20130722
    public string T_ZQM_COMPONENT_STRU_2(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlpublib = "", sqlsap = "";
        string WO_NO, ASSEMBLY_ITEM, ASSEMBLY_ITEM_DESC, COMPONEMT, COMPONEMT_DESC, REQUIRED_QTY, ISSUED_QTY, QTY_PER_ASSEMBLY, PLANT, CREATION_DATE, BULK_FLAG, PHANTOM_FLAG, FLAG1;
        DataSet dspublib = null;
        int v1 = 0, v2 = 0, daycnt = 1, idet = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string Readdir = "PUBLIB", Wridir = "TMP";
        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv3.ToUpper() != "") Wridir = pv3;  // " + Wridir + "
        else Wridir = "SAP";

        sqlpublib = "select * from publib.SAP_WO_COMP where FLAG3 is null or FLAG3 <> 'Y'";  //wo_no=67902279  0613952Y17A
        //sqlpublib = "select * from publib.SAP_WO_COMP where wo_no='WM4-D40001' and COMPONEMT='S0M0413L000         '";

        dspublib = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlpublib);
        if (dspublib == null) return ("-1");
        v1 = dspublib.Tables[0].Rows.Count;
        if (v1 == 0) return ("0");
        int arrCOMlong = v1, arrCOMwidth = 15;
        string[,] arrWO = new string[arrCOMlong + 1, arrCOMwidth + 1];
        for (int i = 0; i < arrCOMlong; i++)
            for (int j = 0; j < arrCOMwidth; j++)
                arrWO[i, j] = "";
        for (int i = 0; i < arrCOMlong; i++)
        {
            arrWO[i, 0] = dspublib.Tables[0].Rows[i]["WO_NO"].ToString().Trim();
            arrWO[i, 1] = dspublib.Tables[0].Rows[i]["ASSEMBLY_ITEM"].ToString().Trim();
            arrWO[i, 2] = dspublib.Tables[0].Rows[i]["ASSEMBLY_ITEM_DESC"].ToString().Trim();
            arrWO[i, 3] = dspublib.Tables[0].Rows[i]["COMPONEMT"].ToString().Trim();
            arrWO[i, 4] = dspublib.Tables[0].Rows[i]["COMPONEMT_DESC"].ToString().Trim();
            arrWO[i, 5] = dspublib.Tables[0].Rows[i]["REQUIRED_QTY"].ToString().Trim();
            arrWO[i, 6] = dspublib.Tables[0].Rows[i]["ISSUED_QTY"].ToString().Trim();
            arrWO[i, 7] = dspublib.Tables[0].Rows[i]["QTY_PER_ASSEMBLY"].ToString().Trim();
            arrWO[i, 8] = dspublib.Tables[0].Rows[i]["PLANT"].ToString().Trim();
            arrWO[i, 9] = dspublib.Tables[0].Rows[i]["CREATION_DATE"].ToString().Trim();
            arrWO[i, 10] = dspublib.Tables[0].Rows[i]["BULK_FLAG"].ToString().Trim();
            arrWO[i, 11] = dspublib.Tables[0].Rows[i]["PHANTOM_FLAG"].ToString().Trim();
            arrWO[i, 12] = dspublib.Tables[0].Rows[i]["FLAG1"].ToString().Trim();
            arrWO[i, 13] = dspublib.Tables[0].Rows[i]["FLAG2"].ToString().Trim();
            arrWO[i, 14] = dspublib.Tables[0].Rows[i]["FLAG3"].ToString().Trim();
        }

        for (int j = 0; j < arrCOMlong; j++)
        {
            WO_NO = ""; ASSEMBLY_ITEM = ""; ASSEMBLY_ITEM_DESC = ""; COMPONEMT = ""; COMPONEMT_DESC = ""; REQUIRED_QTY = ""; ISSUED_QTY = ""; QTY_PER_ASSEMBLY = ""; PLANT = ""; CREATION_DATE = ""; BULK_FLAG = ""; PHANTOM_FLAG = ""; FLAG1 = "";
            sqlsap = ""; idet = 0;

            WO_NO = arrWO[j, 0];
            ASSEMBLY_ITEM = arrWO[j, 1];
            ASSEMBLY_ITEM_DESC = arrWO[j, 2];
            COMPONEMT = arrWO[j, 3];
            COMPONEMT_DESC = arrWO[j, 4];
            REQUIRED_QTY = arrWO[j, 5];
            ISSUED_QTY = arrWO[j, 6];
            QTY_PER_ASSEMBLY = arrWO[j, 7];
            PLANT = arrWO[j, 8];
            CREATION_DATE = (DateTime.Parse(arrWO[j, 9])).ToString("yyyy-MM-dd HH:mm:ss");
            BULK_FLAG = arrWO[j, 10];
            PHANTOM_FLAG = arrWO[j, 11];
            FLAG1 = arrWO[j, 12].Trim();

            if (WO_NO.Trim() == "" || COMPONEMT.Trim() == "")
            {
                string upstr = "update PUBLIB.SAP_WO_COMP set FLAG2='E' where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "'";

                int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);

                continue;
            }

            string sql = "select * from " + Wridir + ".SAP_WO_COMP where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "' ";

            DataSet dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sql);

            if (FLAG1 == "" || FLAG1 == "I")
            {
                //string sql = "select * from SAP.SAP_WO_COMP where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "' ";


                if (dt1.Tables[0].Rows.Count > 0)
                {
                    //string destr = "delete from SAP.SAP_WO_COMP where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "' ";

                    string upstr = "update PUBLIB.SAP_WO_COMP set FLAG2='Y' where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);

                    idet = 1;
                }

                else
                {
                    //sqlsap = "insert into SAP.SAP_WO_COMP(WO_NO, ASSEMBLY_ITEM , ASSEMBLY_ITEM_DESC ,COMPONEMT , COMPONEMT_DESC,"
                    //+ "REQUIRED_QTY, ISSUED_QTY,  QTY_PER_ASSEMBLY, PLANT,CREATION_DATE,BULK_FLAG, PHANTOM_FLAG)"
                    //+"values('" + WO_NO + "','" + ASSEMBLY_ITEM + "','" + ASSEMBLY_ITEM_DESC + "','" + COMPONEMT + "','" + COMPONEMT_DESC + "', "
                    //+ "'" + REQUIRED_QTY + "','" + ISSUED_QTY + "','" + QTY_PER_ASSEMBLY + "','" + PLANT + "', "
                    //+ "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),'" + BULK_FLAG + "','" + PHANTOM_FLAG + "')";

                    sqlsap = "insert into " + Wridir + ".SAP_WO_COMP(WO_NO, ASSEMBLY_ITEM , ASSEMBLY_ITEM_DESC ,COMPONEMT , COMPONEMT_DESC,"
                    + "REQUIRED_QTY, ISSUED_QTY,  QTY_PER_ASSEMBLY, PLANT,CREATION_DATE,BULK_FLAG, PHANTOM_FLAG)"
                    + "values('" + WO_NO + "','" + ASSEMBLY_ITEM + "','" + ASSEMBLY_ITEM_DESC + "','" + COMPONEMT + "','" + COMPONEMT_DESC + "', "
                    + "'" + REQUIRED_QTY + "','" + ISSUED_QTY + "','" + QTY_PER_ASSEMBLY + "','" + PLANT + "', "
                    + "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),'" + BULK_FLAG + "','" + PHANTOM_FLAG + "')";

                    idet = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlsap);

                    string upstr = "update PUBLIB.SAP_WO_COMP set FLAG2='N' where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);
                }
            }
            else if (FLAG1 == "C")
            {
                if (dt1.Tables[0].Rows.Count > 0)
                {
                    //string destr = "delete from SAP.SAP_WO_COMP where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "' ";

                    string destr = "delete from " + Wridir + ".SAP_WO_COMP where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "' ";

                    int deidet = PDataBaseOperation.PExecSQL(DBType, DBWriString, destr);

                    string upstr = "update PUBLIB.SAP_WO_COMP set FLAG2='Y' where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);
                }
                else
                {
                    string upstr = "update PUBLIB.SAP_WO_COMP set FLAG2='N' where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "'";

                    int upidet = PDataBaseOperation.PExecSQL(DBType, DBReadString, upstr);
                }

                //sqlsap = "insert into SAP.SAP_WO_COMP(WO_NO, ASSEMBLY_ITEM , ASSEMBLY_ITEM_DESC ,COMPONEMT , COMPONEMT_DESC,"
                //+ "REQUIRED_QTY, ISSUED_QTY,  QTY_PER_ASSEMBLY, PLANT,CREATION_DATE,BULK_FLAG, PHANTOM_FLAG)"
                //+ "values('" + WO_NO + "','" + ASSEMBLY_ITEM + "','" + ASSEMBLY_ITEM_DESC + "','" + COMPONEMT + "','" + COMPONEMT_DESC + "', "
                //+ "'" + REQUIRED_QTY + "','" + ISSUED_QTY + "','" + QTY_PER_ASSEMBLY + "','" + PLANT + "', "
                //+ "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),'" + BULK_FLAG + "','" + PHANTOM_FLAG + "')";


                sqlsap = "insert into " + Wridir + ".SAP_WO_COMP(WO_NO, ASSEMBLY_ITEM , ASSEMBLY_ITEM_DESC ,COMPONEMT , COMPONEMT_DESC,"
                + "REQUIRED_QTY, ISSUED_QTY,  QTY_PER_ASSEMBLY, PLANT,CREATION_DATE,BULK_FLAG, PHANTOM_FLAG)"
                + "values('" + WO_NO + "','" + ASSEMBLY_ITEM + "','" + ASSEMBLY_ITEM_DESC + "','" + COMPONEMT + "','" + COMPONEMT_DESC + "', "
                + "'" + REQUIRED_QTY + "','" + ISSUED_QTY + "','" + QTY_PER_ASSEMBLY + "','" + PLANT + "', "
                + "to_date('" + CREATION_DATE + "','yyyy/mm/dd HH24:mi:ss'),'" + BULK_FLAG + "','" + PHANTOM_FLAG + "')";


                idet = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlsap);
            }

            if (idet > 0)
            {
                string sql2 = "update PUBLIB.SAP_WO_COMP set FLAG3='Y' where COMPONEMT='" + COMPONEMT + "' and  WO_NO = '" + WO_NO + "'";
                int idet1 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sql2);
                v2 += idet1;
            }
        }
        Ret1 = v2.ToString();
        return (Ret1);
    }


    // L6 施勇部份
    public string ZRFC_WLBG_KIT_P02_2(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string iRet = string.Empty;
        DataSet ds = new DataSet();
        string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        string[] colName = new string[0];

        string Readdir = "PUBLIB", Wridir = "TMP";
        string pv1 = "", pv2 = "", pv3 = "", pv4 = "", pv5 = "", pv6 = "", pv7 = "", pv8 = "", pv9 = "", pv10 = "";
        ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
        string R1 = LibPDBA1Pointer.Getinit_var10(ref pv1, ref pv2, ref pv3, ref pv4, ref pv5, ref pv6, ref pv7, ref pv8, ref pv9, ref pv10);
        if (pv4.ToUpper() != "") Wridir = pv4;  // " + Wridir + "
        else Wridir = "MRP02";

        #region
        string sql = @"SELECT * FROM PUBLIB.MOCTA WHERE FLAG2 IS NULL";
        ds = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql);
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            if (ds.Tables[0].Columns[i].DataType == typeof(string))
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ds.Tables[0].Rows[j][i] = ds.Tables[0].Rows[j][i].ToString().Trim();
                }
            }
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["TA001"].ToString() == "" || ds.Tables[0].Rows[i]["TA002"].ToString() == "")
            {
                string sqlUF2 = @"UPDATE PUBLIB.MOCTA SET FLAG2 = 'E' WHERE TA001 IS NULL AND TA002 = '" + ds.Tables[0].Rows[i]["TA002"].ToString() + "'";
                int ret = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlUF2);
            }
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string sqlS2 = @"SELECT * FROM " + Wridir + ".MOCTA WHERE TA001 = '" + ds.Tables[0].Rows[i]["TA001"].ToString() + "' AND TA002 = '" + ds.Tables[0].Rows[i]["TA002"].ToString() + "'";
            DataSet ds1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlS2);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string sqlUF2 = @"UPDATE PUBLIB.MOCTA SET FLAG2 = 'Y',FLAG3='Y' WHERE TA001 = '" + ds.Tables[0].Rows[i]["TA001"].ToString() + "' AND TA002 = '" + ds.Tables[0].Rows[i]["TA002"].ToString() + "'";
                int ret = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlUF2);
            }
            else
            {
                string sqlUF2 = @"UPDATE PUBLIB.MOCTA SET FLAG2 = 'N' WHERE TA001 = '" + ds.Tables[0].Rows[i]["TA001"].ToString() + "' AND TA002 = '" + ds.Tables[0].Rows[i]["TA002"].ToString() + "'";
                int ret = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlUF2);
            }
        }
        string sqlF3 = @"SELECT * FROM PUBLIB.MOCTA WHERE FLAG3 IS NULL or FLAG3<>'Y'";
        ds = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlF3);
        //Trim()
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            if (ds.Tables[0].Columns[i].DataType == typeof(string))
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ds.Tables[0].Rows[j][i] = ds.Tables[0].Rows[j][i].ToString().Trim();
                }
            }
        }
        for (int i = 0; i < ds.Tables[0].Columns.Count - 4; i++) // for (int i = 0; i < ds.Tables[0].Columns.Count - 3; i++)
        {
            Array.Resize(ref colName, colName.Length + 1);
            colName[colName.Length - 1] = ds.Tables[0].Columns[i].ColumnName.ToString();
        }
        InsertSql = @"INSERT INTO " + Wridir + ".MOCTA (";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["FLAG1"].ToString() == "C" && ds.Tables[0].Rows[i]["FLAG2"].ToString() == "Y")
            {
                string sqlD = @"DELETE FROM " + Wridir + ".MOCTA WHERE  TA001 = '" + ds.Tables[0].Rows[i]["TA001"].ToString() + "' AND TA002 = '" + ds.Tables[0].Rows[i]["TA002"].ToString() + "'";
                int iret = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlD);
            }
            if (ds.Tables[0].Rows[i]["FLAG1"].ToString() == "I" && ds.Tables[0].Rows[i]["FLAG2"].ToString() == "Y")
                continue;
            if (ds.Tables[0].Rows[i]["FLAG2"].ToString() == "N")
            {
                for (int j = 0; j < colName.Length; j++)
                {
                    if (j == colName.Length - 1) // j == colName.Length - 1
                    {
                        InsertSql += colName[j] + @") VALUES (";
                        InsertSqlBy += "'" + ds.Tables[0].Rows[i][j] + "')";
                    }
                    else
                    {
                        InsertSql += colName[j] + ",";
                        InsertSqlBy += "'" + ds.Tables[0].Rows[i][j] + "',";
                    }

                }
                string sql1 = InsertSql + InsertSqlBy;
                string sql2 = @"UPDATE PUBLIB.MOCTA SET FLAG3 = 'Y' WHERE TA002 = '" + ds.Tables[0].Rows[i]["TA002"].ToString() + "' AND TA001 = '" + ds.Tables[0].Rows[i]["TA001"].ToString() + "'";
                int retb = PDataBaseOperation.PExecSQL(DBType, DBWriString, sql1);
                if (retb == 1)
                {
                    iRet += retb;
                    iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql2);
                }
                InsertSql = @"INSERT INTO " + Wridir + ".MOCTA (";
                InsertSqlBy = string.Empty;
            }
        }
        #endregion

        string sqlA2 = @"SELECT * FROM PUBLIB.MOCTB WHERE FLAG2 IS NULL";
        ds = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlA2);
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            if (ds.Tables[0].Columns[i].DataType == typeof(string))
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ds.Tables[0].Rows[j][i] = ds.Tables[0].Rows[j][i].ToString().Trim();
                }
            }
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["TB001"].ToString() == "" || ds.Tables[0].Rows[i]["TB002"].ToString() == "" || ds.Tables[0].Rows[i]["TB003"].ToString() == "")
            {
                string sqlUF2 = @"UPDATE PUBLIB.MOCTB SET FLAG2 = 'E' WHERE TB001 IS NULL AND TB002 = '" + ds.Tables[0].Rows[i]["TB002"].ToString() + "'";
                int ret = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlUF2);
            }
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string sqlS2 = @"SELECT * FROM " + Wridir + ".MOCTB WHERE TB001 = '" + ds.Tables[0].Rows[i]["TB001"].ToString() + "' AND TB002 = '" + ds.Tables[0].Rows[i]["TB002"].ToString() + "' AND TB003 = '" + ds.Tables[0].Rows[i]["TB003"].ToString() + "'";
            DataSet ds1 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlS2);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string sqlUF2 = @"UPDATE PUBLIB.MOCTB SET FLAG2 = 'Y',FLAG3 = 'Y' WHERE TB001 = '" + ds.Tables[0].Rows[i]["TB001"].ToString() + "' AND TB002 = '" + ds.Tables[0].Rows[i]["TB002"].ToString() + "' AND TB003 = '" + ds.Tables[0].Rows[i]["TB003"].ToString() + "'";
                int ret = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlUF2);
            }
            else
            {
                string sqlUF2 = @"UPDATE PUBLIB.MOCTB SET FLAG2 = 'N' WHERE TB001 = '" + ds.Tables[0].Rows[i]["TB001"].ToString() + "' AND TB002 = '" + ds.Tables[0].Rows[i]["TB002"].ToString() + "'  AND TB003 = '" + ds.Tables[0].Rows[i]["TB003"].ToString() + "'";
                int ret = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlUF2);
            }
        }
        string sql1F3 = @"SELECT * FROM PUBLIB.MOCTB WHERE FLAG3 is null or FLAG3<>'Y'";
        ds = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql1F3);
        //Trim()
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            if (ds.Tables[0].Columns[i].DataType == typeof(string))
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ds.Tables[0].Rows[j][i] = ds.Tables[0].Rows[j][i].ToString().Trim();
                }
            }
        }

        colName = new string[0];
        for (int i = 0; i < ds.Tables[0].Columns.Count - 4; i++)  // for (int i = 0; i < ds.Tables[0].Columns.Count - 3; i++)
        {
            Array.Resize(ref colName, colName.Length + 1);
            colName[colName.Length - 1] = ds.Tables[0].Columns[i].ColumnName.ToString();
        }
        InsertSql = @"INSERT INTO " + Wridir + ".MOCTB (";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["FLAG1"].ToString() == "C" && ds.Tables[0].Rows[i]["FLAG2"].ToString() == "Y")
            {
                string sqlD = @"DELETE FROM " + Wridir + ".MOCTB WHERE  TB001 = '" + ds.Tables[0].Rows[i]["TB001"].ToString() + "' AND TB002 = '" + ds.Tables[0].Rows[i]["TB002"].ToString() + "'  AND TB003 = '" + ds.Tables[0].Rows[i]["TB003"].ToString() + "'";
                int iret = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlD);
            }
            if (ds.Tables[0].Rows[i]["FLAG1"].ToString() == "I" && ds.Tables[0].Rows[i]["FLAG2"].ToString() == "Y")
                continue;
            if (ds.Tables[0].Rows[i]["FLAG2"].ToString() == "N")
            {
                for (int j = 0; j < colName.Length; j++)
                {
                    if (j == colName.Length - 1)
                    {
                        InsertSql += colName[j] + @") VALUES (";
                        InsertSqlBy += "'" + ds.Tables[0].Rows[i][j] + "')";
                    }
                    else
                    {
                        InsertSql += colName[j] + ",";
                        InsertSqlBy += "'" + ds.Tables[0].Rows[i][j] + "',";
                    }

                }
                string sql1 = InsertSql + InsertSqlBy;
                string sql2 = @"UPDATE PUBLIB.MOCTB SET FLAG3 = 'Y' WHERE TB002 = '" + ds.Tables[0].Rows[i]["TB002"].ToString() + "' AND TB001 = '" + ds.Tables[0].Rows[i]["TB001"].ToString() + "'  AND TB003 = '" + ds.Tables[0].Rows[i]["TB003"].ToString() + "'";
                int retb = PDataBaseOperation.PExecSQL(DBType, DBWriString, sql1);
                if (retb == 1)
                {
                    iRet += retb;
                    iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql2);
                }
                InsertSql = @"INSERT INTO " + Wridir + ".MOCTB (";
                InsertSqlBy = string.Empty;
            }
        }
        return iRet;
    }
           
// New Program 20130627
// ReqType : "1": Day 20130626, "2" DN :/EM1-D60002 "3" WO_NO WM1-D60002, L6 "4" WO_NO L10, "5"; ReqData : 20130626/WM1-D60002/EM1-D60002 
// Retstr = SFCLinkTiptopPointer.ZRFC_WLBG_KIT_ALL("tjsfc", Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData); 

public string ZRFC_WLBG_KIT_ALL(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
{
        string iRet = string.Empty;
        switch (ReqType)
        {
            case "1"://Day   

                // 暫時不接受, 重新作業
                // iRet = ZRFC_WLBG_KIT_PO2A  (BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L6
                // iRet = ZRFC_WLBG_KIT_PO2B  (BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L6
                // iRet = T_ZQM_HEADE_STRU    (BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L10 WO LIST
                // iRet = T_ZQM_COMPONENT_STRU(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L10 COMP
                // iRet = ZSD_SFC_GET_HU_DATA (BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L10 Package 
             break;

            case "2"://DN   

                break;

            case "3"://WO_NO  L6
                // 暫時不接受, 重新作業
                iRet = ZRFC_WLBG_KIT_PO2A(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L6
                iRet = ZRFC_WLBG_KIT_PO2B(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L6
                break;

            case "4"://WO_NO L10

                // 暫時不接受, 重新作業
                iRet = T_ZQM_HEADE_STRU(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L10 WO LIST
                iRet = T_ZQM_COMPONENT_STRU(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L10 COMP
                iRet = ZSD_SFC_GET_HU_DATA(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, ReqType, ReqData);  // L10 Package 
                break;             

            default:
                 
                break;
        }
          
        return iRet;

    }

    public string ZRFC_WLBG_KIT_PO2A(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {

        // ReqType : "1": Day 20130626, "2" DN :/EM1-D60002 "3" WO_NO WM1-D60002, L6 "4" WO_NO L10, "5"; ReqData : 20130626/WM1-D60002/EM1-D60002 

        string str1;
        if ( !Dtype.Equals("C"))
        {
            Dtype = "I";        
        }
         
 
        str1 = " SELECT sfbuser  CREATOR,  TO_CHAR(  sfb81 ,'%Y%m%d')  CREATE_DATE,sfbmodu MODIFIER,  TO_CHAR(  sfbdate ,'%Y%m%d')   MODI_DATE,0 FLAG,";
        str1 = str1 + " '5101' TA001,sfb01 TA002,sfb05 TA003,'0000' TA004,sfb08 TA005,0 TA006,0 TA007,'' TA008,";
        str1 = str1 + " '' TA009,'' TA010,sfb01 TA011,  TO_CHAR(  sfb25 ,'%Y%m%d')   TA012,'' TA013,  TO_CHAR(  sfb15 ,'%Y%m%d')     TA014,'' TA015,'Y' TA016,";
        str1 = str1 + " '0' TA017,sfb05 TA018,'' TA019,'' TA020,ima25 TA021,";
        str1 = str1 + " '' TA022,'' TA023,'' TA024,'' TA025,'hfjz0bf6' TA026,'0' TA027,'4' TA028,'' TA029";//hfjzz0bf6
        str1 = str1 + " FROM  SFB_FILE, ima_file  where sfb05=ima01  ";
         


        switch (ReqType)
        {
            case "1"://Day

            ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
            ReqData = Convert.ToDateTime(ReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");

            str1 = str1 + "and sfb81 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
            break;
            case "3"://WO_NO  L6
            str1 = str1 + "and  sfb01  = '" + ReqData + "'";                 
            break; 
            default:
            break;
        }         
 
        Odbc ww = new Odbc();       
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";        
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0) 
        {
          return "0";
        } 
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];     
        string InsertSqlBy = string.Empty;
       // conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
       // conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA"); 
       // string sqlMocta = @"SELECT * FROM PUBLIB.MOCTA WHERE FLAG1 IS NULL";       
       // dsMocta = DataBaseOperation.SelectSQLDS(DBType, conStrW, sqlMocta);
        string sql1 = "DELETE from publib.MOCTA ";
        iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1); 
        for (int i = 0; i < dt.Rows.Count; i++)
        {
 
            if (  !Dtype.Equals("C"))
            {
                Dtype = "I";
            }             

             string InsertSql = string.Empty;
             InsertSql = @"INSERT INTO publib.MOCTA(CREATOR,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,TA001,TA002,TA003,TA004,TA005,TA006,TA007,TA008,TA009,TA010,TA011,TA012,TA013,TA014,TA015,TA016,TA017,TA018,TA019,TA020,TA021,TA022,TA023,TA024,TA025,TA026,TA027,TA028,TA029,FLAG1) values ('";
             InsertSql = InsertSql + dt.Rows[i]["CREATOR"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["CREATE_DATE"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["MODIFIER"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["MODI_DATE"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["FLAG"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA001"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA002"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA003"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA004"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA005"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA006"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA007"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA008"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA009"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA010"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA011"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA012"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA013"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA014"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA015"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA016"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA017"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA018"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA019"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA020"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA021"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA022"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA023"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA024"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA025"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA026"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA027"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA028"].ToString().Trim() + "','";
             InsertSql = InsertSql + dt.Rows[i]["TA029"].ToString().Trim() + "','";
                InsertSql = InsertSql + Dtype + "')";
            

          
            string sql = InsertSql + InsertSqlBy;
            iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);            
            InsertSqlBy = string.Empty;
        }
 
        return iRet;

  
    }

    public string ZRFC_WLBG_KIT_PO2B(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        string str1;
        string str2;
        string tmpReqData = ReqData;
        if (!Dtype.Equals("C"))
        {
            Dtype = "I";
        }
        str1 = " SELECT sfbuser CREATOR, TO_CHAR(sfb81 ,'%Y%m%d') CREATE_DATE,sfbmodu MODIFIER, TO_CHAR( sfbdate ,'%Y%m%d') MODI_DATE,0 FLAG, ";
        str1 = str1 + " '5101' TB001,sfb01 TB002,sfa03 TB003,sfa161 TB004,0 TB005,'' TB006,'1' TB007,'' TB008, ";
        str1 = str1 + " '' TB009,ima02 TB010,'' TB011,ima25 TB012,'' TB013,'' TB014,'' TB015,'' TB016 ";
        str1 = str1 + " FROM SFA_FILE,SFB_FILE,ima_file ";
        str1 = str1 + " where sfa01=sfb01 and sfa03=ima01 ";
        switch (ReqType)
        {
            case "1"://Day
                tmpReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                tmpReqData = Convert.ToDateTime(tmpReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");
                str1 = str1 + "and sfb81 >= TO_date( '" + tmpReqData + "' ,'%Y/%m/%d') ";
                break;
            case "3"://WO_NO L6
                str1 = str1 + "and sfb01 = '" + ReqData + "'";
                break;
            default:
                break;
        }


        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        //string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        //conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        // conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        string sql1 = "DELETE from publib.MOCTB ";
        iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string InsertSql = string.Empty;
            InsertSql = @"INSERT INTO publib.MOCTB ( CREATOR,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,TB001,TB002,TB003,TB004,TB005,TB006,TB007,TB008,TB009,TB010,TB011,TB012,TB013,TB014,TB015,TB016,FLAG1) values ('";
            InsertSql = InsertSql + dt.Rows[i]["CREATOR"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["CREATE_DATE"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["MODIFIER"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["MODI_DATE"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["FLAG"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB001"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB002"].ToString().Trim().ToUpper() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB003"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB004"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB005"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB006"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB007"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB008"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB009"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB010"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB011"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB012"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB013"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB014"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB015"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB016"].ToString().Trim() + "','";
            InsertSql = InsertSql + Dtype + "')";
            string sql = InsertSql + InsertSqlBy;
            iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
            InsertSqlBy = string.Empty;
        }
        //新增替代料部份^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        str1 = " SELECT distinct sfbuser CREATOR, TO_CHAR(sfb81 ,'%Y%m%d') CREATE_DATE,sfbmodu MODIFIER, TO_CHAR( sfbdate ,'%Y%m%d') MODI_DATE,0 FLAG, ";
        str1 = str1 + " '5101' TB001,sfb01 TB002,bmd04 TB003,sfa161 TB004,0 TB005,'' TB006,'1' TB007,'' TB008, ";
        str1 = str1 + " '' TB009,ima02 TB010,'' TB011,ima25 TB012,'' TB013,'' TB014,'' TB015,'' TB016 ";
        str1 = str1 + " FROM SFA_FILE,SFB_FILE,ima_file ,bmd_file";
        str1 = str1 + " where sfa01=sfb01 and sfa03=ima01 and sfa03 =bmd01 ";
        switch (ReqType)
        {
            case "1": //Day
                tmpReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                tmpReqData = Convert.ToDateTime(tmpReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");   
                str1 = str1 + "and sfb81 >= TO_date( '" + tmpReqData + "' ,'%Y/%m/%d') ";
                break;
            case "3": //WO_NO L6
                str1 = str1 + "and sfb01 = '" + ReqData + "'";
                break;
            default:
                break;
        }
        // Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        //DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string InsertSql = string.Empty;
            InsertSql = @"INSERT INTO publib.MOCTB ( CREATOR,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,TB001,TB002,TB003,TB004,TB005,TB006,TB007,TB008,TB009,TB010,TB011,TB012,TB013,TB014,TB015,TB016,FLAG1) values ('";
            InsertSql = InsertSql + dt.Rows[i]["CREATOR"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["CREATE_DATE"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["MODIFIER"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["MODI_DATE"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["FLAG"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB001"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB002"].ToString().Trim().ToUpper() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB003"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB004"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB005"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB006"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB007"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB008"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB009"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB010"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB011"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB012"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB013"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB014"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB015"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["TB016"].ToString().Trim() + "','";
            InsertSql = InsertSql + Dtype + "')";
            string sql = InsertSql + InsertSqlBy;
            iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
            InsertSqlBy = string.Empty;
        }
        return iRet;
    }
    

    public string T_ZQM_HEADE_STRU(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        string str1;
        if ( !Dtype.Equals("C"))
        {
            Dtype = "I";
        }
         
 
        str1 = " select  'hfjz0bf6'  PLANT, '0' SCH ,sfb01 WO_NO,sfb04 STATUS_TYPE,sfb05 ASSEMBLY_ITEM,'' CLASS_CODE, sfb08 START_QTY,";
        str1 = str1 + "   TO_CHAR(  sfb81 ,'%Y/%m/%d')    CREATION_DATE , TO_CHAR(  sfb13 ,'%Y/%m/%d')   BASE_START_DATE,   TO_CHAR(  sfb15 ,'%Y/%m/%d')   BASE_FINISH_DATE,'' BOM_REVISION,";
        str1 = str1 + "   '' SO_NO,'000000' SO_LINE_NO,tlk03 PROJECT_CODE,'0' SCH";
        str1 = str1 + "   from sfb_file, outer  tlk_file  where sfb01=tlk01 ";
          
        switch (ReqType)
        {
            case "1"://Day
                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-7).ToString("yyyy/MM/dd");
                str1 = str1 + " and sfb81 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;
            case "4"://WO_NO  L6
                str1 = str1 + "and  sfb01  = '" + ReqData + "'";
                break;
            default:
                break;
        }
          
        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        //string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        //conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        //conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        string sql1 = "DELETE from  publib.SAP_WO_LIST ";
        iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            
            string InsertSql = string.Empty;
            InsertSql = @"INSERT INTO publib.SAP_WO_LIST (plant,sch,wo_no,status_type,assembly_item,class_code,start_qty,creation_date,base_start_date,base_finish_date,bom_revision,so_no,FLAG1) VALUES ('";
            InsertSql = InsertSql + dt.Rows[i]["PLANT"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["SCH"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["WO_NO"].ToString().ToUpper().Trim() + "','";
           // InsertSql = InsertSql + dt.Rows[i]["STATUS_TYPE"].ToString()+"','";
            InsertSql = InsertSql + "REL','";
            InsertSql = InsertSql + dt.Rows[i]["ASSEMBLY_ITEM"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["CLASS_CODE"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["START_QTY"].ToString().Trim() + "', to_date('";
            InsertSql = InsertSql + dt.Rows[i]["CREATION_DATE"].ToString().Trim() + "','yyyy/mm/dd'), to_date('";
            InsertSql = InsertSql + dt.Rows[i]["BASE_START_DATE"].ToString().Trim() + "','yyyy/mm/dd')  ,to_date('";
            InsertSql = InsertSql + dt.Rows[i]["BASE_FINISH_DATE"].ToString().Trim() + "','yyyy/mm/dd'),'";
            InsertSql = InsertSql + dt.Rows[i]["BOM_REVISION"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["SO_NO"].ToString().Trim() + "','";
            InsertSql = InsertSql + Dtype + "' )";
         // InsertSql =InsertSql +dt.Rows[i]["PROJECT_CODE"].ToString()+"'";
            string sql = InsertSql + InsertSqlBy;
            iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
            InsertSqlBy = string.Empty;

        }
 
        return iRet;
    }

    
    public string T_ZQM_HEADE_STRUtmp(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        string str1;
        if ( !Dtype.Equals("C"))
        {
            Dtype = "I";
        }
         

 
        str1 = " select  'hfjz0bf6'  PLANT, '0' SCH ,sfb01 WO_NO,sfb04 STATUS_TYPE,sfb05 ASSEMBLY_ITEM,'' CLASS_CODE, sfb08 START_QTY,";
        str1 = str1 + "   TO_CHAR(  sfb81 ,'%Y/%m/%d')    CREATION_DATE , TO_CHAR(  sfb13 ,'%Y/%m/%d')   BASE_START_DATE,  TO_CHAR(  sfb15 ,'%Y/%m/%d')   BASE_FINISH_DATE,'' BOM_REVISION,";
        str1 = str1 + "   '' SO_NO,'000000' SO_LINE_NO,tlk03 PROJECT_CODE,'0' SCH";
        str1 = str1 + "   from sfb_file, outer  tlk_file  where sfb01=tlk01 ";
          
        switch (ReqType)
        {
            case "1"://Day

                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");
                str1 = str1 + " and sfb81 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;

            case "4"://WO_NO  L6
                str1 = str1 + "and  sfb01  = '" + ReqData + "'";
                break;
            default:

                break;
        }
          

        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }

        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        //string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        //conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        //conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");

        string sql1 = "DELETE from  publib.SAP_WO_LIST ";
        iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
 

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            
            string InsertSql = string.Empty;
            InsertSql = @"INSERT INTO publib.SAP_WO_LIST (plant,sch,wo_no,status_type,assembly_item,class_code,start_qty,creation_date,base_start_date,base_finish_date,bom_revision,so_no,FLAG1) VALUES ('";
            InsertSql = InsertSql + dt.Rows[i]["PLANT"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["SCH"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["WO_NO"].ToString().ToUpper().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["STATUS_TYPE"].ToString().Trim() + "','";
          //InsertSql = InsertSql +dt.Rows[i]["ASSEMBLY_ITEM"].ToString()+"','";
            InsertSql = InsertSql +   "REL','";
            InsertSql = InsertSql + dt.Rows[i]["CLASS_CODE"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["START_QTY"].ToString().Trim() + "', to_date('";
            InsertSql = InsertSql + dt.Rows[i]["CREATION_DATE"].ToString().Trim() + "','yyyy/mm/dd'), to_date('";
            InsertSql = InsertSql + dt.Rows[i]["BASE_START_DATE"].ToString().Trim() + "','yyyy/mm/dd')  ,to_date('";
            InsertSql = InsertSql + dt.Rows[i]["BASE_FINISH_DATE"].ToString().Trim() + "','yyyy/mm/dd'),'";
            InsertSql = InsertSql + dt.Rows[i]["BOM_REVISION"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["SO_NO"].ToString().Trim() + "','";
            InsertSql = InsertSql + Dtype + "' )";
         // InsertSql =InsertSql +dt.Rows[i]["PROJECT_CODE"].ToString()+"'";
            string sql = InsertSql + InsertSqlBy;
            iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
            InsertSqlBy = string.Empty;
        }
        
        return iRet;        
    }

    public string T_ZQM_COMPONENT_STRU(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        string str1;
        if ( !Dtype.Equals("C"))
        {
            Dtype = "I";
        }
          
        str1 = " select sfb81, sfb01 WO_NO,sfb05 ASSEMBLY_ITEM,a.ima02 ASSEMBLY_ITEM_DESC,sfa03 COMPONEMT,";
        str1 = str1 + "  b.ima02 COMPONEMT_DESC,sfa05 REQUIRED_QTY,0 ISSUED_QTY,sfa161 QTY_PER_ASSEMBLY,";
        str1 = str1 + "  'hfjz0bf6' PLANT, TO_CHAR(  sfb81 ,'%Y/%m/%d')    CREATION_DATE,'' BULK_FLAG,'' PHANTOM_FLAG";
        str1 = str1 + "  from  sfb_file,sfa_file,ima_file a,ima_file b";
        str1 = str1 + "  where sfb01=sfa01 and sfb05=a.ima01 and sfa03=b.ima01  ";
        switch (ReqType)
        {
            case "1"://Day
                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");
                str1 = str1 + "and sfb81 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;
            case "4"://WO_NO  L6
                str1 = str1 + "and  sfb01  = '" + ReqData + "'";
                break;
            default:
                break;
        }

        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count  <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
      //conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
      //  conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");

        string sql1 = " DELETE from  publib.SAP_WO_COMP ";

        iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
       
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            InsertSql = @"INSERT INTO publib.SAP_WO_COMP (WO_NO,ASSEMBLY_ITEM,ASSEMBLY_ITEM_DESC,COMPONEMT,COMPONEMT_DESC,REQUIRED_QTY,ISSUED_QTY,QTY_PER_ASSEMBLY,PLANT,CREATION_DATE,BULK_FLAG,PHANTOM_FLAG,FLAG1) VALUES ('";
            InsertSql = InsertSql + dt.Rows[i]["WO_NO"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["ASSEMBLY_ITEM"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["ASSEMBLY_ITEM_DESC"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["COMPONEMT"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["COMPONEMT_DESC"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["REQUIRED_QTY"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["ISSUED_QTY"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["QTY_PER_ASSEMBLY"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["PLANT"].ToString().Trim() + "',to_date('";
            InsertSql = InsertSql + dt.Rows[i]["CREATION_DATE"].ToString().Trim() + "','yyyy/mm/dd'),'";
            InsertSql = InsertSql + dt.Rows[i]["BULK_FLAG"].ToString().Trim() + "','";
            InsertSql = InsertSql + dt.Rows[i]["PHANTOM_FLAG"].ToString().Trim() + "','";
            InsertSql = InsertSql + Dtype  + "')";

            string sql = InsertSql + InsertSqlBy;

               iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
       
            InsertSqlBy = string.Empty;
         }
 
        return iRet;

    }

    public string ZSD_SFC_GET_HU_DATA(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        Int16 CartonQty;
        Int16 PalletQty;
        string str1;
        if (!Dtype.Equals("C"))
        {
            Dtype = "I";
        }

        string sdate;
        sdate = System.DateTime.Now.AddDays(-1).ToShortDateString();
        SysDate = SysDate.Substring(0, 4) + "/" + SysDate.Substring(4, 2) + "/" + SysDate.Substring(6, 2);
        SysDate = Convert.ToDateTime(SysDate).AddDays(-7).ToShortDateString();
        str1 = " select DISTINCT oga011 INVOICE_NUMBER,";
        str1 = str1 + " max(ogd12e)-min(ogd12b)+1 carton_qty,";
        str1 = str1 + " ogd19 pallet_qty,";
        str1 = str1 + " ogb11 ITEM_NUMBER,";
        str1 = str1 + " ogb908   CUSTOMER_PO,";
        str1 = str1 + " ocd227 DESTINATION,";
        str1 = str1 + " ocd221  SHIP_TO_CUSTOMERNAME,";
        str1 = str1 + " ocd223 SHIP_TO_CITY,";
        str1 = str1 + " ocd03 SHIP_TO_COUNTRY,";
        str1 = str1 + " oga01  SO_NO,";
        str1 = str1 + " ogb11 CUSTOMER_ITEM,";
        str1 = str1 + " tlk03 PROJECT,";
        str1 = str1 + " ogb08 PLANT,";
        str1 = str1 + " obe24 SO_LINE_NO";
        str1 = str1 + " from oga_file ,ogb_file,ogd_file,obe_file,outer ocd_file ,outer tlk_file";
        str1 = str1 + " where oga011=ogb01 and  ogb01=ogd01 and ogb03=ogd03 and ogd08=obe01 and  oga04=ocd01 and oga044=ocd02  and ogb04=tlk01 and oga09='2' and ogaconf='Y' and ogb11 <>'' ";
        switch (ReqType)
        {
            case "1"://Day
                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-7).ToString("yyyy/MM/dd");
                str1 = str1 + " and oga02 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;
            case "2"://WO_NO  L6
                str1 = str1 + "and  oga011  = '" + ReqData + "'";
                break;
            default:
                break;
        }
        str1 = str1 + " group by  oga01 ,ogd19,ogb11,ogb908,ocd227,ocd221,ocd223, ocd03 ,oga011 ,ogb11,tlk02,tlk03,ogb08,obe24";
        str1 = "select INVOICE_NUMBER,  sum (carton_qty)  carton_qty,  pallet_qty,  ITEM_NUMBER,  CUSTOMER_PO,  DESTINATION,  SHIP_TO_CUSTOMERNAME,  SHIP_TO_CITY,  SHIP_TO_COUNTRY,  SO_NO,  CUSTOMER_ITEM,  PROJECT,  PLANT ,  SO_LINE_NO from (" + str1 + ")";
        str1 = str1 + " group by  INVOICE_NUMBER,  pallet_qty,  ITEM_NUMBER,  CUSTOMER_PO,  DESTINATION,  SHIP_TO_CUSTOMERNAME,  SHIP_TO_CITY,  SHIP_TO_COUNTRY,  SO_NO,  CUSTOMER_ITEM,  PROJECT,  PLANT ,  SO_LINE_NO";

        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        string sql1 = " DELETE from  publib.CMCS_SFC_PACKING_LINES_ALL ";
        iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string ss = dt.Rows[i]["CARTON_QTY"].ToString();
            if (ss.IndexOf(".") > 0)
            {
                ss = ss.Substring(0, ss.IndexOf("."));
            }

            CartonQty = Convert.ToInt16(ss);
            PalletQty = Convert.ToInt16(dt.Rows[i]["pallet_qty"].ToString());
            for (int iii = 1; iii <= CartonQty; iii++)
            {
                InsertSql = @"INSERT INTO publib.CMCS_SFC_PACKING_LINES_ALL(CARTON_NUMBER,CUSTOMER_ITEM,CUSTOMER_PO,DESTINATION,INVOICE_NUMBER,ITEM_NUMBER,PALLET_NUMBER,PLANT,PROJECT,SHIP_TO_CITY,SHIP_TO_COUNTRY,SHIP_TO_CUSTOMERNAME,SO_LINE_NO,SO_NO,QUANTITY,FLAG1 ) VALUES ('";
                InsertSql = InsertSql + iii.ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["CUSTOMER_ITEM"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["CUSTOMER_PO"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["DESTINATION"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["INVOICE_NUMBER"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["ITEM_NUMBER"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["PALLET_QTY"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["PLANT"].ToString().Substring(0, 6).ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["PROJECT"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["SHIP_TO_CITY"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["SHIP_TO_COUNTRY"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["SHIP_TO_CUSTOMERNAME"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["SO_LINE_NO"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["SO_NO"].ToString().Trim() + "','";
                InsertSql = InsertSql + "0','";
                InsertSql = InsertSql + Dtype + "')";
                string sql = InsertSql + InsertSqlBy;
                iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
                InsertSqlBy = string.Empty;
            }
        }
        return iRet;
    }


    // 20130904 New1 
    public string ZRFC_WLBG_KIT_PO2A1(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {

        // ReqType : "1": Day 20130626, "2" DN :/EM1-D60002 "3" WO_NO WM1-D60002, L6 "4" WO_NO L10, "5"; ReqData : 20130626/WM1-D60002/EM1-D60002 

        string str1;
        if (!Dtype.Equals("C"))
        {
            Dtype = "I";
        }


        str1 = " SELECT sfbuser  CREATOR,  TO_CHAR(  sfb81 ,'%Y%m%d')  CREATE_DATE,sfbmodu MODIFIER,  TO_CHAR(  sfbdate ,'%Y%m%d')   MODI_DATE,0 FLAG,";
        str1 = str1 + " '5101' TA001,sfb01 TA002,sfb05 TA003,'0000' TA004,sfb08 TA005,0 TA006,0 TA007,'' TA008,";
        str1 = str1 + " '' TA009,'' TA010,sfb01 TA011,  TO_CHAR(  sfb25 ,'%Y%m%d')   TA012,'' TA013,  TO_CHAR(  sfb15 ,'%Y%m%d')     TA014,'' TA015,'Y' TA016,";
        str1 = str1 + " '0' TA017,sfb05 TA018,'' TA019,'' TA020,ima25 TA021,";
        str1 = str1 + " '' TA022,'' TA023,'' TA024,'' TA025,'hfjz0bf6' TA026,'0' TA027,'4' TA028,'' TA029";//hfjzz0bf6
        str1 = str1 + " FROM  SFB_FILE, ima_file  where sfb05=ima01  ";



        switch (ReqType)
        {
            case "1"://Day

                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");

                str1 = str1 + "and sfb81 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;
            case "3"://WO_NO  L6
                str1 = str1 + "and  sfb01  = '" + ReqData + "'";
                break;
            default:
                break;
        }

        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        string InsertSqlBy = string.Empty;
        // conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        // conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA"); 
        // string sqlMocta = @"SELECT * FROM PUBLIB.MOCTA WHERE FLAG1 IS NULL";       
        // dsMocta = DataBaseOperation.SelectSQLDS(DBType, conStrW, sqlMocta);
        //string sql1 = "DELETE from publib.MOCTA ";
        //iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1); 
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (!Dtype.Equals("C"))
            {
                Dtype = "I";
            }

            string sql;

            sql = "select * from publib.MOCTA where  TA002 ='" + dt.Rows[i]["TA002"].ToString().Trim().ToUpper() + "'".ToString();

            DataTable dt_temp1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql).Tables[0];
            if (dt_temp1.Rows.Count == 0)
            {
                string InsertSql = string.Empty;
                InsertSql = @"INSERT INTO publib.MOCTA(CREATOR,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,TA001,TA002,TA003,TA004,TA005,TA006,TA007,TA008,TA009,TA010,TA011,TA012,TA013,TA014,TA015,TA016,TA017,TA018,TA019,TA020,TA021,TA022,TA023,TA024,TA025,TA026,TA027,TA028,TA029,FLAG1) values ('";
                InsertSql = InsertSql + dt.Rows[i]["CREATOR"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["CREATE_DATE"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["MODIFIER"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["MODI_DATE"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["FLAG"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA001"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA002"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA003"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA004"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA005"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA006"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA007"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA008"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA009"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA010"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA011"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA012"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA013"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA014"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA015"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA016"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA017"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA018"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA019"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA020"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA021"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA022"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA023"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA024"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA025"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA026"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA027"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA028"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TA029"].ToString().Trim() + "','";
                InsertSql = InsertSql + Dtype + "')";



                sql = InsertSql + InsertSqlBy;
                iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
                InsertSqlBy = string.Empty;
            }
        }

        return iRet;


    }

    // New1
    public string ZRFC_WLBG_KIT_PO2B1(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        string str1;
        string str2;
        string tmpReqData = ReqData;
        if (!Dtype.Equals("C"))
        {
            Dtype = "I";
        }
        str1 = " SELECT sfbuser CREATOR, TO_CHAR(sfb81 ,'%Y%m%d') CREATE_DATE,sfbmodu MODIFIER, TO_CHAR( sfbdate ,'%Y%m%d') MODI_DATE,0 FLAG, ";
        str1 = str1 + " '5101' TB001,sfb01 TB002,sfa03 TB003,sfa161 TB004,0 TB005,'' TB006,'1' TB007,'' TB008, ";
        str1 = str1 + " '' TB009,ima02 TB010,'' TB011,ima25 TB012,'' TB013,'' TB014,'' TB015,'' TB016 ";
        str1 = str1 + " FROM SFA_FILE,SFB_FILE,ima_file ";
        str1 = str1 + " where sfa01=sfb01 and sfa03=ima01 ";
        switch (ReqType)
        {
            case "1"://Day
                tmpReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                tmpReqData = Convert.ToDateTime(tmpReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");
                str1 = str1 + "and sfb81 >= TO_date( '" + tmpReqData + "' ,'%Y/%m/%d') ";
                break;
            case "3"://WO_NO L6
                str1 = str1 + "and sfb01 = '" + ReqData + "'";
                break;
            default:
                break;
        }


        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        //string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        //conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        // conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        //string sql1 = "DELETE from publib.MOCTB ";
        //iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sql = " select * from  publib.MOCTB where  TB002  ='" + dt.Rows[i]["TB002"].ToString().Trim().ToUpper() + "' AND  TB003 ='" + dt.Rows[i]["TB003"].ToString().Trim().ToUpper() + "'".ToString();
            DataTable dt_temp1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql).Tables[0];
            if (dt_temp1.Rows.Count == 0)
            {
                string InsertSql = string.Empty;
                InsertSql = @"INSERT INTO publib.MOCTB ( CREATOR,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,TB001,TB002,TB003,TB004,TB005,TB006,TB007,TB008,TB009,TB010,TB011,TB012,TB013,TB014,TB015,TB016,FLAG1) values ('";
                InsertSql = InsertSql + dt.Rows[i]["CREATOR"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["CREATE_DATE"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["MODIFIER"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["MODI_DATE"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["FLAG"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB001"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB002"].ToString().Trim().ToUpper() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB003"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB004"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB005"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB006"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB007"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB008"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB009"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB010"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB011"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB012"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB013"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB014"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB015"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB016"].ToString().Trim() + "','";
                InsertSql = InsertSql + Dtype + "')";
                sql = InsertSql + InsertSqlBy;
                iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
                InsertSqlBy = string.Empty;
            }
        }
        //新增替代料部份^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        str1 = " SELECT distinct sfbuser CREATOR, TO_CHAR(sfb81 ,'%Y%m%d') CREATE_DATE,sfbmodu MODIFIER, TO_CHAR( sfbdate ,'%Y%m%d') MODI_DATE,0 FLAG, ";
        str1 = str1 + " '5101' TB001,sfb01 TB002,bmd04 TB003,sfa161 TB004,0 TB005,'' TB006,'1' TB007,'' TB008, ";
        str1 = str1 + " '' TB009,ima02 TB010,'' TB011,ima25 TB012,'' TB013,'' TB014,'' TB015,'' TB016 ";
        str1 = str1 + " FROM SFA_FILE,SFB_FILE,ima_file ,bmd_file";
        str1 = str1 + " where sfa01=sfb01 and sfa03=ima01 and sfa03 =bmd01 ";
        switch (ReqType)
        {
            case "1": //Day
                tmpReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                tmpReqData = Convert.ToDateTime(tmpReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");
                str1 = str1 + "and sfb81 >= TO_date( '" + tmpReqData + "' ,'%Y/%m/%d') ";
                break;
            case "3": //WO_NO L6
                str1 = str1 + "and sfb01 = '" + ReqData + "'";
                break;
            default:
                break;
        }
        // Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        //DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sql = "   select * from  publib.MOCTB where  TB002  ='" + dt.Rows[i]["TB002"].ToString().Trim().ToUpper() + "' AND  TB003 ='" + dt.Rows[i]["TB003"].ToString().Trim().ToUpper() + "'".ToString();
            DataTable dt_temp1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql).Tables[0];
            if (dt_temp1.Rows.Count == 0)
            {
                string InsertSql = string.Empty;
                InsertSql = @"INSERT INTO publib.MOCTB ( CREATOR,CREATE_DATE,MODIFIER,MODI_DATE,FLAG,TB001,TB002,TB003,TB004,TB005,TB006,TB007,TB008,TB009,TB010,TB011,TB012,TB013,TB014,TB015,TB016,FLAG1) values ('";
                InsertSql = InsertSql + dt.Rows[i]["CREATOR"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["CREATE_DATE"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["MODIFIER"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["MODI_DATE"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["FLAG"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB001"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB002"].ToString().Trim().ToUpper() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB003"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB004"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB005"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB006"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB007"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB008"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB009"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB010"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB011"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB012"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB013"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB014"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB015"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["TB016"].ToString().Trim() + "','";
                InsertSql = InsertSql + Dtype + "')";
                sql = InsertSql + InsertSqlBy;
                iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
                InsertSqlBy = string.Empty;
            }
        }
        return iRet;
    }
    // New 1
    public string T_ZQM_HEADE_STRU1(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        string str1;
        if (!Dtype.Equals("C"))
        {
            Dtype = "I";
        }


        str1 = " select  'hfjz0bf6'  PLANT, '0' SCH ,sfb01 WO_NO,sfb04 STATUS_TYPE,sfb05 ASSEMBLY_ITEM,'' CLASS_CODE, sfb08 START_QTY,";
        str1 = str1 + "   TO_CHAR(  sfb81 ,'%Y/%m/%d')    CREATION_DATE , TO_CHAR(  sfb13 ,'%Y/%m/%d')   BASE_START_DATE,   TO_CHAR(  sfb15 ,'%Y/%m/%d')   BASE_FINISH_DATE,'' BOM_REVISION,";
        str1 = str1 + "   '' SO_NO,'000000' SO_LINE_NO,tlk03 PROJECT_CODE,'0' SCH";
        str1 = str1 + "   from sfb_file, outer  tlk_file  where sfb01=tlk01 ";

        switch (ReqType)
        {
            case "1"://Day
                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-7).ToString("yyyy/MM/dd");
                str1 = str1 + " and sfb81 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;
            case "4"://WO_NO  L6
                str1 = str1 + "and  sfb01  = '" + ReqData + "'";
                break;
            default:
                break;
        }

        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        //string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        //conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        //conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        //string sql1 = "DELETE from  publib.SAP_WO_LIST ";
        //iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            string sql = "   select * from publib.SAP_WO_LIST where WO_NO  ='" + dt.Rows[i]["WO_NO"].ToString().Trim().ToUpper() + "'".ToString();

            DataTable dt_temp1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql).Tables[0];
            if (dt_temp1.Rows.Count == 0)
            {

                string InsertSql = string.Empty;
                InsertSql = @"INSERT INTO publib.SAP_WO_LIST (plant,sch,wo_no,status_type,assembly_item,class_code,start_qty,creation_date,base_start_date,base_finish_date,bom_revision,so_no,FLAG1) VALUES ('";
                InsertSql = InsertSql + dt.Rows[i]["PLANT"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["SCH"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["WO_NO"].ToString().ToUpper().Trim() + "','";
                // InsertSql = InsertSql + dt.Rows[i]["STATUS_TYPE"].ToString()+"','";
                InsertSql = InsertSql + "REL','";
                InsertSql = InsertSql + dt.Rows[i]["ASSEMBLY_ITEM"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["CLASS_CODE"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["START_QTY"].ToString().Trim() + "', to_date('";
                InsertSql = InsertSql + dt.Rows[i]["CREATION_DATE"].ToString().Trim() + "','yyyy/mm/dd'), to_date('";
                InsertSql = InsertSql + dt.Rows[i]["BASE_START_DATE"].ToString().Trim() + "','yyyy/mm/dd')  ,to_date('";
                InsertSql = InsertSql + dt.Rows[i]["BASE_FINISH_DATE"].ToString().Trim() + "','yyyy/mm/dd'),'";
                InsertSql = InsertSql + dt.Rows[i]["BOM_REVISION"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["SO_NO"].ToString().Trim() + "','";
                InsertSql = InsertSql + Dtype + "' )";
                // InsertSql =InsertSql +dt.Rows[i]["PROJECT_CODE"].ToString()+"'";
                sql = InsertSql + InsertSqlBy;
                iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
                InsertSqlBy = string.Empty;
            }
        }

        return iRet;
    }

    // New 
    public string T_ZQM_COMPONENT_STRU1(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        string str1;
        if (!Dtype.Equals("C"))
        {
            Dtype = "I";
        }

        str1 = " select sfb81, sfb01 WO_NO,sfb05 ASSEMBLY_ITEM,a.ima02 ASSEMBLY_ITEM_DESC,sfa03 COMPONEMT,";
        str1 = str1 + "  b.ima02 COMPONEMT_DESC,sfa05 REQUIRED_QTY,0 ISSUED_QTY,sfa161 QTY_PER_ASSEMBLY,";
        str1 = str1 + "  'hfjz0bf6' PLANT, TO_CHAR(  sfb81 ,'%Y/%m/%d')    CREATION_DATE,'' BULK_FLAG,'' PHANTOM_FLAG";
        str1 = str1 + "  from  sfb_file,sfa_file,ima_file a,ima_file b";
        str1 = str1 + "  where sfb01=sfa01 and sfb05=a.ima01 and sfa03=b.ima01  ";
        switch (ReqType)
        {
            case "1"://Day
                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-Gdaycnt).ToString("yyyy/MM/dd");
                str1 = str1 + "and sfb81 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;
            case "4"://WO_NO  L6
                str1 = str1 + "and  sfb01  = '" + ReqData + "'";
                break;
            default:
                break;
        }

        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        //conStrR = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        //  conStrW = LibUSR1Pointer.DBDeEncCode(DBReadString, PCode, ",", "2DBA");
        //string sql1 = " DELETE from  publib.SAP_WO_COMP ";
        //iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sql = "   select * from publib.SAP_WO_COMP  where WO_NO = '" + dt.Rows[i]["WO_NO"].ToString().Trim().ToUpper() + "' AND  COMPONEMT  = '" + dt.Rows[i]["COMPONEMT"].ToString().Trim().ToUpper() + "'".ToString();
            DataTable dt_temp1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql).Tables[0];
            if (dt_temp1.Rows.Count == 0)
            {
                InsertSql = @"INSERT INTO publib.SAP_WO_COMP (WO_NO,ASSEMBLY_ITEM,ASSEMBLY_ITEM_DESC,COMPONEMT,COMPONEMT_DESC,REQUIRED_QTY,ISSUED_QTY,QTY_PER_ASSEMBLY,PLANT,CREATION_DATE,BULK_FLAG,PHANTOM_FLAG,FLAG1) VALUES ('";
                InsertSql = InsertSql + dt.Rows[i]["WO_NO"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["ASSEMBLY_ITEM"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["ASSEMBLY_ITEM_DESC"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["COMPONEMT"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["COMPONEMT_DESC"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["REQUIRED_QTY"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["ISSUED_QTY"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["QTY_PER_ASSEMBLY"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["PLANT"].ToString().Trim() + "',to_date('";
                InsertSql = InsertSql + dt.Rows[i]["CREATION_DATE"].ToString().Trim() + "','yyyy/mm/dd'),'";
                InsertSql = InsertSql + dt.Rows[i]["BULK_FLAG"].ToString().Trim() + "','";
                InsertSql = InsertSql + dt.Rows[i]["PHANTOM_FLAG"].ToString().Trim() + "','";
                InsertSql = InsertSql + Dtype + "')";

                sql = InsertSql + InsertSqlBy;

                iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);

                InsertSqlBy = string.Empty;
            }
        }
        return iRet;

    }

    // new 1 
    public string ZSD_SFC_GET_HU_DATA1(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode, string ReqType, string ReqData)
    {
        Int16 CartonQty;
        Int16 PalletQty;
        string str1;
        if (!Dtype.Equals("C"))
        {
            Dtype = "I";
        }

        string sdate;
        sdate = System.DateTime.Now.AddDays(-1).ToShortDateString();
        SysDate = SysDate.Substring(0, 4) + "/" + SysDate.Substring(4, 2) + "/" + SysDate.Substring(6, 2);
        SysDate = Convert.ToDateTime(SysDate).AddDays(-7).ToShortDateString();
        str1 = " select DISTINCT oga011 INVOICE_NUMBER,";
        str1 = str1 + " max(ogd12e)-min(ogd12b)+1 carton_qty,";
        str1 = str1 + " ogd19 pallet_qty,";
        str1 = str1 + " ogb11 ITEM_NUMBER,";
        str1 = str1 + " ogb908   CUSTOMER_PO,";
        str1 = str1 + " ocd227 DESTINATION,";
        str1 = str1 + " ocd221  SHIP_TO_CUSTOMERNAME,";
        str1 = str1 + " ocd223 SHIP_TO_CITY,";
        str1 = str1 + " ocd03 SHIP_TO_COUNTRY,";
        str1 = str1 + " oga01  SO_NO,";
        str1 = str1 + " ogb11 CUSTOMER_ITEM,";
        str1 = str1 + " tlk03 PROJECT,";
        str1 = str1 + " ogb08 PLANT,";
        str1 = str1 + " obe24 SO_LINE_NO";
        str1 = str1 + " from oga_file ,ogb_file,ogd_file,obe_file,outer ocd_file ,outer tlk_file";
        str1 = str1 + " where oga011=ogb01 and  ogb01=ogd01 and ogb03=ogd03 and ogd08=obe01 and  oga04=ocd01 and oga044=ocd02  and ogb04=tlk01 and oga09='2' and ogaconf='Y' and ogb11 <>'' ";
        switch (ReqType)
        {
            case "1"://Day
                ReqData = ReqData.Substring(0, 4) + "/" + ReqData.Substring(4, 2) + "/" + ReqData.Substring(6, 2);
                ReqData = Convert.ToDateTime(ReqData).AddDays(-7).ToString("yyyy/MM/dd");
                str1 = str1 + " and oga02 >= TO_date(  '" + ReqData + "' ,'%Y/%m/%d')   ";
                break;
            case "2"://WO_NO  L6
                str1 = str1 + "and  oga011  = '" + ReqData + "'";
                break;
            default:
                break;
        }
        str1 = str1 + " group by  oga01 ,ogd19,ogb11,ogb908,ocd227,ocd221,ocd223, ocd03 ,oga011 ,ogb11,tlk02,tlk03,ogb08,obe24";
        str1 = "select INVOICE_NUMBER,  sum (carton_qty)  carton_qty,  pallet_qty,  ITEM_NUMBER,  CUSTOMER_PO,  DESTINATION,  SHIP_TO_CUSTOMERNAME,  SHIP_TO_CITY,  SHIP_TO_COUNTRY,  SO_NO,  CUSTOMER_ITEM,  PROJECT,  PLANT ,  SO_LINE_NO from (" + str1 + ")";
        str1 = str1 + " group by  INVOICE_NUMBER,  pallet_qty,  ITEM_NUMBER,  CUSTOMER_PO,  DESTINATION,  SHIP_TO_CUSTOMERNAME,  SHIP_TO_CITY,  SHIP_TO_COUNTRY,  SO_NO,  CUSTOMER_ITEM,  PROJECT,  PLANT ,  SO_LINE_NO";

        Odbc ww = new Odbc();
        ww.CommandText = str1;
        ww.ConnectionString = "DSN=www";
        DataTable dt;
        dt = ww.execsql();
        if (dt.Rows.Count <= 0)
        {
            return "0";
        }
        string iRet = string.Empty;
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        DataSet dsMocta = new DataSet();
        DataSet dsMoctb = new DataSet();
        string conStrR = string.Empty;
        string conStrW = string.Empty;
        string[] colName = new string[0];
        string InsertSql = string.Empty;
        string InsertSqlBy = string.Empty;
        //string sql1 = " DELETE from  publib.CMCS_SFC_PACKING_LINES_ALL ";
        //iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql1);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string ss = dt.Rows[i]["CARTON_QTY"].ToString();
            if (ss.IndexOf(".") > 0)
            {
                ss = ss.Substring(0, ss.IndexOf("."));
            }

            CartonQty = Convert.ToInt16(ss);
            PalletQty = Convert.ToInt16(dt.Rows[i]["pallet_qty"].ToString());
            for (int iii = 1; iii <= CartonQty; iii++)
            {
                string sql = "   select * from publib.CMCS_SFC_PACKING_LINES_ALL  where INVOICE_NUMBER = '" + dt.Rows[i]["INVOICE_NUMBER"].ToString().Trim().ToUpper() + "' AND  CARTON_NUMBER  = '" + iii.ToString() + "'".ToString();
                DataTable dt_temp1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sql).Tables[0];
                if (dt_temp1.Rows.Count == 0)
                {

                    InsertSql = @"INSERT INTO publib.CMCS_SFC_PACKING_LINES_ALL(CARTON_NUMBER,CUSTOMER_ITEM,CUSTOMER_PO,DESTINATION,INVOICE_NUMBER,ITEM_NUMBER,PALLET_NUMBER,PLANT,PROJECT,SHIP_TO_CITY,SHIP_TO_COUNTRY,SHIP_TO_CUSTOMERNAME,SO_LINE_NO,SO_NO,QUANTITY,FLAG1 ) VALUES ('";
                    InsertSql = InsertSql + iii.ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["CUSTOMER_ITEM"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["CUSTOMER_PO"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["DESTINATION"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["INVOICE_NUMBER"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["ITEM_NUMBER"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["PALLET_QTY"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["PLANT"].ToString().Substring(0, 6).ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["PROJECT"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["SHIP_TO_CITY"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["SHIP_TO_COUNTRY"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["SHIP_TO_CUSTOMERNAME"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["SO_LINE_NO"].ToString().Trim() + "','";
                    InsertSql = InsertSql + dt.Rows[i]["SO_NO"].ToString().Trim() + "','";
                    InsertSql = InsertSql + "0','";
                    InsertSql = InsertSql + Dtype + "')";
                    sql = InsertSql + InsertSqlBy;
                    iRet += PDataBaseOperation.PExecSQL(DBType, DBReadString, sql);
                    InsertSqlBy = string.Empty;
                }
            }
        }
        return iRet;
    }  
    





// end New Program 20130627

}  // end public class SFCLinkTopToplib

}  // end namespace SFC.TJWEB


