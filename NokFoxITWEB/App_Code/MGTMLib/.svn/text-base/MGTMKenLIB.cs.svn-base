using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Data.OracleClient;
using System.IO;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Text;


/// <summary>
/// Summary description for MGTMKenLIB
/// </summary>
/// 
// namespace MGTMLibrary.Ken
namespace DB.EAI
{ 

public class MGTMKenLIB
{
    #region 定義
    const int ERROR_FILE_NOT_FOUND = 2;
    const int ERROR_ACCESS_DENIED = 5;
    public bool bInTimer = false;
    public const string FACTORY_CODE = "FIHTJ";
    public static string FactoryCode;
    public const Int32 SINGLE_UPD_FILE_MAX_LENGTH = 20 * 1024 * 1024; // 20 MB 
    public const Int32 UPLOAD_FAIL_WAIT_DURATION = 1 * 20 * 1000; // 2 minutes
    public static readonly string UPD_UPLOAD_PROGRAM_PATH = @"C:\MCMS\PROGRAM\";
    public static readonly string UPD_FILE_NAME = @"C:\mcms\upd_service\to_be_processed\AS_"
                                                + FACTORY_CODE + "_UPD_IMEI_{0}_{1}.dat";
    public static readonly string ASN_FILE_NAME = @"C:\mcms\DS_ASN_FIH_{0}_{1}.dat";
    public static readonly string IMEI_FILE_NAME = @"d:\mcms\IMEI_{0}.xls";
    public static readonly string IMEI_FILE_BAK = @"D:\MCMS\DataBack\IMEI";
    public static readonly string ASN_FILE_BAK = @"D:\MCMS\DataBack\ASN";
    public static readonly string UPD_FILE_BAK = @"D:\MCMS\DataBack\UPD";
    public static readonly string GFS_FILE_BAK = @"D:\MCMS\DataBack\GFS";
    public static readonly string[] CDMA_DIRECTORY_ARRAY = new string[] {@"c:\mcms\program",
                                                                        @"c:\mcms\gfs\to_be_processed",          
                                                                        @"c:\mcms\upd_service\to_be_processed"};
    public static readonly string[] DIRECTORY_ARRY = new string[] {    @"C:\MCMS\mototrak",
                                                                         @"C:\MCMS\upd_service",
                                                                         @"C:\MCMS\gfs",
                                                                         @"C:\MCMS\program",
                                                                         @"C:\MCMS\workspace",
                                                                         @"C:\MCMS\mototrak\done",
                                                                         @"C:\MCMS\mototrak\failed",
                                                                         @"C:\MCMS\mototrak\log",
                                                                         @"c:\MCMS\mototrak\to_be_processed",
                                                                         @"c:\MCMS\upd_service\done",
                                                                         @"C:\MCMS\upd_service\failed",
                                                                         @"C:\MCMS\upd_service\log",
                                                                         @"C:\MCMS\upd_service\to_be_processed",
                                                                         @"C:\MCMS\gfs\done",
                                                                         @"C:\MCMS\gfs\failed",
                                                                         @"C:\MCMS\gfs\log",
                                                                         @"C:\MCMS\gfs\to_be_processed"
                                                                    };
    public static readonly string ZIP_CMD = @"e:\Program Files\7-Zip\7z.exe ";// a -tzip ";  
    public static readonly string CDMA_UPD_FILE_NAME = CDMA_DIRECTORY_ARRAY[CDMA_DIRECTORY_ARRAY.Length - 1]
                                                 + @"\AS_" + FACTORY_CODE + "_UPD_MEID_" + "{0}_{1}.dat";
    public static readonly string CDMA_GFS_FILE_NAME = CDMA_DIRECTORY_ARRAY[CDMA_DIRECTORY_ARRAY.Length - 2] + @"\PROG_V01_" + FACTORY_CODE + "_{0}_{1}-{2}.dat";
    public static readonly string[] Model_type = new string[] { "QAZ", "FD1", "DMQ" };

    //------------------------------------------- 
    public static readonly string[] DIRECTORY_ARRAY_FIX = new string[] {
                                                                        @"C:\mototrak",
                                                                        @"C:\mcms\gfs\done",
                                                                        @"C:\mcms\gfs\failed",
                                                                        @"C:\mcms\gfs\log",
                                                                        @"C:\gfs\temp",
                                                                        @"c:\mcms\program",
                                                                        @"c:\mcms\workspace",
                                                                        @"c:\mcms\upd_service\done",
                                                                        @"c:\mcms\upd_service\failed",
                                                                        @"c:\mcms\upd_service\log",
                                                                        @"c:\mcms\upd_service\temp",
                                                                        @"c:\mcms\upd_service\to_be_processed"};
    //public static readonly string UPD_UPLOAD_PROGRAM_PATH = DIRECTORY_ARRAY[0];
    public static readonly string[] DIRECTORY_ARRAY = new string[] {@"c:\mcms\program",
                                                                        @"c:\mcms\gfs\to_be_processed",          
                                                                        @"c:\mcms\upd_service\to_be_processed"};
    public static readonly string GFS_FILE_NAME = DIRECTORY_ARRAY[DIRECTORY_ARRAY.Length - 2] + @"\PROG_V01_" + FACTORY_CODE + "_{0}_{1}-{2}.dat";                                           


    #endregion

   
   
   
    // Input String Name and OutPut Data and Rec Length 
    public string[,] GetFileData(string strName )
    {
        string strsql01 = strName;
        // string strsql01 = @"select * from FTRAM1 where AUTOFLAG = 'y' or AUTOFLAG = 'Y' order by MODEL, SHIP_TO, F_TYPE"; //
            
        int iColNum = 0, iRowNum = 0, AutoCnt = 0, AutoCnt1 = 0, FColNum = 0;
        string[,] TmpArr = new string[0, 0];
        string sw1 = "", AFlag = "";
        int i = 0, j = 0, k = 0;

        //////////////////////////////////////////////////////
        // 1. Get Master  FTRAM1
        // 2. Array 1. Model, 2 F_TYPE  3. SHIP_To 
        //
        //
        using (OracleDB odb = new OracleDB())
        {
            try
            {
                using (DataSet odr = odb.GetDataSet(strsql01))
                {
                    FColNum = odr.Tables[0].Columns.Count;
                    iColNum = FColNum + 2;
                    iRowNum = odr.Tables[0].Rows.Count;
                    TmpArr = new string[iRowNum, iColNum];

                    for (i = 0; i < iRowNum; i++)
                        for (j = 0; j < iColNum; j++)
                            TmpArr[i, j] = "";

                    for (i = 0; i < iRowNum; i++)
                    {
                        for (j = 0; j < FColNum; j++)
                        {
                            TmpArr[i, j] = odr.Tables[0].Rows[i][j].ToString();

                            // if (FTRAM1[i, 9] == null) FTRAM1[i, 9] = "";
                            // AFlag = FTRAM1[i, 9].ToString();                            
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] Checkback " + ex.Message);
            }

        }

        return ( TmpArr ); // Not Array
    } // end  public string[,] GetFileData(string strName )


    public string CntDays( string sdate,  string edate, string modesw)
    {
        string r1 = "", r2 = "";
        int var1 = 0, var2 = 0;

        if (modesw == "1") // yyyy-mm-dd 
        {
            DateTime MaxStr = DateTime.Today;
            DateTime MinStr = DateTime.Today;
            DateTime tmptoday = DateTime.Today;

            // MinStr = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 6]));
            // MaxStr = Convert.ToDateTime(ShipPlanlibPointer.TrsstringToDateTime(arrayCustomerFoxconnPNToOneSet[loopforeachSetcount, 7]));            
            MinStr = Convert.ToDateTime(TrsstringToDateTime(sdate));
            MaxStr = Convert.ToDateTime(TrsstringToDateTime(edate));

            r2   = MaxStr.Subtract(MinStr).ToString();
            var1 = r2.IndexOf(".");  // Seek first 151.1 point location

            if (var1 <= 0) var2 = 0;
            else var2 = Convert.ToInt32( r2.Substring(0, var1)); // DV (最大 - 最小) 天數    

       //     textBox2.Text = MaxStr.Subtract(MinStr).ToString();
       //     var1 = textBox2.Text.IndexOf(".");  // Seek first 151.1 point location
       //
       //     if (var1 <= 0) MaxMimDVLong = 1;
       //     else MaxMimDVLong = Convert.ToInt32(textBox2.Text.Substring(0, var1)); // DV (最大 - 最小) 天數    
       //
       //     textBox2.Text = tmptoday.Subtract(MinStr).ToString();
       //     var1 = textBox2.Text.IndexOf(".");  // Seek first 151.1 point location
       //
       //    if (var1 <= 0) var2 = 0;
       //     else var2 = Convert.ToInt32(textBox2.Text.Substring(0, var1)); // 得到今天在第幾個 Array location 未加 100
       //     var1 = BefreDVLoc100 + 1 + var2;  // 起始 100+1(DV 最早日) + 今天實際位置 114
       //     tmptodaylocation = var1;          // 從最早 DV 為100 , Offset 偏移到今天 100+1+OffSet          

        }


        return ( var2.ToString() );

    }  // end CntDays(this.txtStartDate.Value, this.txtStartDate.Value, "1");


    /////////////////////////////////////////////////////////////////////////////////////////////////
    //    private void TrastringToDateTime(ref string localstr4) Inout String PutPut String
    public string TrsstringToDateTime(string datepara)  // pass tradatetime
    {
        string locvar31 = datepara;
        // string locvar32    = datepara;
        // string locvar33    = datepara;
        string locvarmonth = datepara;
        string locvarday = datepara;
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

} // End of public class MGTMKenLIB

}  // End namespace MGTMLib.Ken
