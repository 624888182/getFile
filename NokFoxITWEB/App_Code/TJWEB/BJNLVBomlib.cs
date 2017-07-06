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
public class BJNLVBomlib
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

    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");

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
        string ChkDate = "201307";

        tmp1 = "TOP"; 
        sqlr = " update BOMTXT set DataLevel = '00' where (  ( DataLevel = 'TOP' )  and ( flag is null )) ";                
        v4 = PDataBaseOperation.PExecSQL( DBType, DBReadString, sqlr);
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
        string[,] arrayLevel = new string[ 50 + 1, 3 + 1];
        for (v1 = 0; v1 <= 50; v1++)
            for (v2 = 0; v2 <= 3; v2++)
                arrayLevel[v1, v2] = "";

        string PPart = "", CPart="";
     
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
                v3 = ( tmp2.Length ) / 2;
                arrayBOM1[v1 + 1, 32] = v3.ToString(); // Length

                if ( ( v3 == 1 ) && (  arrayBOM1[v1 + 1, 31] == "00" ) ) // BOM ITEM
                {
                    arrayBOM1Item[v4 + 1, 33] = ( v1 + 1).ToString();   // Bom array pointer
                    arrayBOM1[ v1 + 1, 33] = ( v4 + 1 ).ToString();     // Bom ITEM Head 
                    arrayBOM1Item[v4 + 1, 11] = arrayBOM1[ v1 + 1, 11]; // Parent
                    arrayBOM1Item[v4 + 1, 31] = arrayBOM1[ v1 + 1, 31]; // Parent number
                    v4++;
                    ItemLen ++;
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

            for ( v2 = v2; v2 < DNCnt; v2++ )
            {
                LevelFlag = "";
                tmp1 = arrayBOM1[v2, 31];
                CurrDataLevel = arrayBOM1[v2, 31];
                CurrLevelLong = 0;
                if ( arrayBOM1[v2, 32] != "" ) 
                     CurrLevelLong = Convert.ToInt32( arrayBOM1[v2, 32]) ;
                
                if (tmp1 == "00")  // BOM Master
                {
                    CurrDataLevel = "";
                    CurrLevelLong = 0;
                    arrayLevel[0, 0] = arrayBOM1[v2, 11]; // ITEM
                    arrayBOM1[v2, 22] = arrayBOM1[v2, 11]; // Parent ITEM in 22
                    arrayBOM1[v2, 30] = "00";              // Phantom  00, 01, 11, 10 上下階
                }
                else
                if (CurrLevelLong == 1 )  // tmp1 == "01")  // 第一階
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
                if ( ( PreLevelLong + 1 ) == CurrLevelLong)  // 下一階是連續
                {
                        arrayBOM1[v2, 22]    = arrayLevel[PreLevelLong, 0];  // 前一階為現階母料號
                        arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號   
                        arrayBOM1[v2, 30] = "11";              // Phantom  00, 01, 11, 10 上下階 
                }
                else // 下一階不是連續, 而是往前縮
                {
                        PreLevelLong = CurrLevelLong - 1;
                        arrayBOM1[v2, 22] = arrayLevel[PreLevelLong, 0];  // 前一階為現階母料號
                        arrayLevel[CurrLevelLong, 0] = arrayBOM1[v2, 11];   // 目前為下一個母料號   
                        tmp3 = arrayBOM1[v2, 30].Substring(0,1) + "0";
                        arrayBOM1[v2-1, 30] = tmp3;  // 前一階 Close
                        arrayBOM1[v2,   30] = "11";                                               
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
            if ( ( arrayBOM1Item[v1 + 1, 29] == "") || ( arrayBOM1Item[v1 + 1, 29] == null ) )
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
                            + " Rseq, DocumentID, LineID, Mark, Note, Remark ) values "
       + " ( '" + arrayBOM1[v2, 1].ToString().Trim() + "' , '" + arrayBOM1[v2, 22].ToString().Trim() + "' , '" + arrayBOM1[v2, 11].ToString().Trim() + "' ,  "
     //+ " '" + arrayBOM1[v2, 15].ToString().Trim() + "' ,  '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
     //+ "   cast (  ' " + dqty + " ' as float )  ,  '" + arrayBOM1[v2, 15].ToString().Trim() + "' , '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
       + "   " + dqty + " ,  '" + arrayBOM1[v2, 15].ToString().Trim() + "' , '" + arrayBOM1[v2, 32].ToString().Trim() + "' , "
       + "   '" + tmpDate + "' , '" + sp1 + "' , '" + sp1 + "' , '" + arrayBOM1[v2, 30].ToString().Trim() + "' , '" + sp1 + "' ,"
       + "   '" + arrayBOM1[v2, 6].ToString().Trim() + "' ,  '" + Currtime + "' , '" + v5.ToString() + "' ,  "
       + "   '" + sp1 + "' , '" + arrayBOM1[v2, 4].ToString().Trim() + "' , '" + sp1 + "'  ) ";
                    v4 = PDataBaseOperation.PExecSQL(DBType, DBWriString, tmpsqlW);
                    if (v4 >= 0) // Successed, It could be not data
                        v4 = v4;
                }
            }     
            
        } // end for loop check BOMTXT
              


     return("");    

   } // ConvertBOM1

  

}  // end public class BJNLVBomlib

}  // end namespace SFC.TJWEB


