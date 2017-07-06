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
public class BJNLVMRPlib
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
    static int Gdaycnt = 2;
    static string tmpType = "";
    
    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");

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


    //10.	program 3: Get Netdv, from jevm1
    //         3.1: TOTWDV + CDV ( 上傳 )  = TotDV
    //         3.2: TotDV – NOI ( On-Hands )  – SOI  ( Consigned  + GIT )  = NetDV
    //         3.2. NetDV – Inventory  =  TmpQty   (  Last Value ) 
    //         3.2  TmpQty >   計算 
    public string CalNetDV(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
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
        string LastUpdate = DateTime.Now.ToString("yyyyMMddHHmmssmm");

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        sqlr = " UPDATE [ERPDBF].[dbo].[jevm1] SET TotDV = [TotWDV] + [CDV],  NetDV =  TotDV - On_Hand - Consigned - GIT, UpDateTime = '" + LastUpdate + "', "
        + " TmpQty =  On_Hand - [TotWDV] - [CDV] where  Osversion = '" + ChkDate + "' and  ( ( flag is null ) or ( flag = '') ) ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        if (v4 > 0) // Successed
            v4 = v4;

        sqlr = " UPDATE [ERPDBF].[dbo].[jevm1] SET TmpExcess = Consigned + GIT + Inventory, UpDateTime = '" + LastUpdate + "', Flag1 = '+' "
        + " where  Osversion = '" + ChkDate + "' and  ( ( flag is null ) or ( flag = '')  ) and TmpQty >= 0   ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        if (v4 > 0) // Successed
            v4 = v4;

        sqlr = " UPDATE [ERPDBF].[dbo].[jevm1] SET TmpExcess = On_Hand + Consigned + GIT - TotDV + Inventory, UpDateTime = '" + LastUpdate + "', Flag1 = '-' "
       + " where  Osversion = '" + ChkDate + "' and  ( ( flag is null ) or ( flag = '')  ) and TmpQty < 0   ";
        v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
        if (v4 > 0) // Successed
            v4 = v4;

    //    sqlr = " UPDATE [ERPDBF].[dbo].[jevm2] A,   [ERPDBF].[dbo].[jevm2] B SET  A.TmpExCess =  B.TmpExCess, UpDateTime = '" + LastUpdate + "' "
    //  + " where  Osversion = '" + ChkDate + "' and  ( ( flag is null ) or ( flag = '')  ) ";
    //    v4 = PDataBaseOperation.PExecSQL(DBType, DBReadString, sqlr);
    //    if (v4 > 0) // Successed
    //        v4 = v4;
         
        return ("");

    } // end CalNetDV

    public string ExpEVToRaw(string BSite, string Dtype, string SysDate, string DBType, string DBReadString, string DBWriString, string PCode)
    {
        string Ret1 = "", sqlr = "", sqlw = "", tmpsqlW = "", sp = "", sp0 = "0", tpo = "", tdn = "", tmpselwri = "", ErrFlag = "", sp1 = "";
        DataSet dt1 = null, dt2 = null, DNdt = null, dt3 = null;
        int v0 = 0, v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, daycnt = 1, DNCnt = 0;
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t16 = "", t17 = "", t18 = "", t19 = "", t20 = "", t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "", tmp5 = "", tmp6 = "", tmp7 = "";
        Decimal d1 = 0, d2 = 0, d3 = 0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(Gdaycnt).ToString("yyyyMMdd");
        string ChkDate = DateTime.Today.ToString("yyyyMM");
        string LastUpdate = DateTime.Now.ToString("yyyyMMddHHmmssmm");

        tmpType = "RM"; // 計算原物料

        if (SysDate != "") ChkDate = SysDate.Substring(0, 6);

        tmpsqlW = "Update jevm2  set BomQty = " + v0 + ", TmpQty = " + v0 + ", TmpExcess = " + v0 + "  where Osversion = '" + SysDate + "'  ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);


        // 20130912 sqlr = "  SELECT * FROM jevm1 where Osversion = '" + ChkDate + "' and TmpExcess < 0 ";
        sqlr = "  SELECT * FROM jevm1 where Osversion = '" + ChkDate + "' ";
        DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if (DNdt == null) return ("-1"); // Syn Error
        DNCnt = DNdt.Tables[0].Rows.Count;
        if (DNCnt == 0) return ("0");     // Not Data
        int arrayBOM1ItemLong = DNCnt;

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
            arrayBOM1Item[v1 + 1, 53] = DNdt.Tables[0].Rows[v1]["TmpExcess"].ToString().Trim(); // TmpExcess
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
            tmp3 = arrayBOM1Item[v1 + 1, 53].Trim();  // 53  TmpExpress, 46 Qty

            if (tmp2 == "7A400QA00VA")
                v4 = v4;
            if ((tmp3 != "") && (tmp3 != "0") && (tmp3 != null))
                t21 = ExpBOM(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, "A", tmp1, tmp2, tmp3, tmpType);

        }

        // 完成從 EV 抓取到 tmp file 
        // run jbomm1tmp1 not data 
        // jbomm1tmp  為展開處
        // jbomm1tmp1 為 01, 11
        // jbomm1tmp2 為 10, 00

        Ret1 = "Y";
        v7 = 0;
        while (Ret1 == "Y")
        {
            sqlr = "  select * from jbomm1tmp1 ";
            dt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (dt1 == null) return ("-1"); // Syn Error
            v4 = dt1.Tables[0].Rows.Count;
            if ( v4 == 0) return ("0");     // Not Data
            v7++;
            sqlr = "  insert into [ERPDBF].[dbo].[jbomm1tmp3]  select * from jbomm1tmp1 ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

            sqlr = "  delete jbomm1tmp  ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

            sqlr = "  insert into [ERPDBF].[dbo].[jbomm1tmp]  select * from jbomm1tmp1 ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

            sqlr = "  delete jbomm1tmp1 ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, sqlr);

            ///////////////////////////////////////////////////////////////////
            // 新一輪展開
            sqlr = "  SELECT * FROM jbomm1tmp ";
            DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
            if (DNdt == null) return ("-1"); // Syn Error
            DNCnt = DNdt.Tables[0].Rows.Count;
            if ( DNCnt == 0) return ("0");     // Not Data

            // MaxItem = 60; // ItemLen = 0;
            arrayBOM1Item = new string[DNCnt + 1, MaxItem + 1];
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
                    t21 = ExpBOM(BSite, Dtype, SysDate, DBType, DBReadString, DBWriString, PCode, "A", tmp1, tmp2, tmp3, "");

            }

        } // while (Ret1 == "Y")

        return ("");        


    } // end  ExpEVToRaw 

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


