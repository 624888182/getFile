using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Data.OracleClient;
using System.IO;
using System.Diagnostics;
using Excel3 = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Text;


namespace DB.EAI
{
    /// <summary>
    /// Summary description for MGTM_lib
    /// </summary>
    public class MGTMLib
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
        public static readonly string[] DIRECTORY_ARRY = new string[] {  @"C:\MCMS\mototrak",
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
        #endregion

        public MGTMLib()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Create Steps

        public string GetAllInvoiceNumber()
        {
            string strRet = string.Empty;
            using (OracleDB oraDB = new OracleDB())
            {
                string strInvoice = string.Empty;
                string strSql = string.Empty;
                strSql = @"select DISTINCT A.PLANT, A.invoice, A.customer_name, A.ITEM_MODULE,b.Ship_To_Country,
                                      (select sum(quantity) from CMCS_SFC_PACKING_LINES_ALL b where b.INVOICE_NUMBER=a.invoice) shipped_qty,
                                      LAST_SHIPMENT_DATE ,C.CUSTOMER_TYPE from SFC.MES_MIT_INVOICE A,CMCS_SFC_PACKING_LINES_ALL B ,SFC.CMCS_SFC_MODEL C
                                      Where A.Invoice = B.INVOICE_NUMBER
                                      AND A.ITEM_MODULE=C.MODEL AND C.MODEL IN ('QAZ','DMQ','A3G')
                                      and A.LAST_SHIPMENT_DATE>sysdate-15
                                      and (UPPER(A.CUSTOMER_NAME) LIKE '%MOTO%' or UPPER(A.CUSTOMER_NAME) LIKE '%摩托羅拉%'
                                      or UPPER(A.CUSTOMER_NAME) LIKE '%SUTECH%' or UPPER(A.CUSTOMER_NAME) LIKE '%TX4-SOI%' or 
                                      A.CUSTOMER_NAME LIKE 'FIH (Hong Kong) Limited'
                                      or UPPER(A.CUSTOMER_NAME) LIKE '%MI-AMK%' or UPPER(A.CUSTOMER_NAME) LIKE '%S&B INDUSTRY INC.%')
                                      order by A.LAST_SHIPMENT_DATE desc";
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strInvoice += "" + odr[0].ToString() + "#" + odr[1].ToString() + "#" + odr[2].ToString() + "#" + odr[3].ToString() + "#" + odr[4].ToString() + "#" + odr[5].ToString() + "#" + odr[6].ToString() + "#" + odr[7].ToString() + "*";
                        }

                        if (strInvoice.Length > 0)
                        {
                            strRet = strInvoice.Remove(strInvoice.Length - 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetAllInvoiceNumber " + ex.Message);
                }
            }
            return strRet;
        }
        public string AuotoCheckDN()
        {
            string strRet = string.Empty;
            string strSql = @"SELECT * FROM (SELECT DISTINCT b.po_number, a.invoice_number, b.creation_date,
                                                    (SELECT SUM (quantity)
                                                       FROM cmcs_sfc_packing_lines_all a
                                                      WHERE a.invoice_number = c.invoice) shipped_qty
                                               FROM sap.cmcs_sfc_packing_lines_all a,
                                                    shp.upd_order_information b,
                                                    sfc.mes_mit_invoice c
                                              WHERE a.customer_po = b.po_number
                                                AND c.invoice = a.invoice_number
                                                AND b.creation_date > SYSDATE - 5
                                                AND a.invoice_number NOT IN (
                                                                            SELECT invoice
                                                                              FROM shp.upd_dataload_detail_t) )";
            using (OracleDB odb = new OracleDB())
            {
                try
                {
                    using (OracleDataReader odr = odb.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strRet += "" + odr[0].ToString() + "#" + odr[1].ToString() + "#" + odr[2].ToString() + "#" + odr[3].ToString() + "#" + odr[4].ToString() + "*";

                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] UPDAlarm_Load->GetDataReader:" + ex.Message);
                }
            }
            return strRet;
        }
        public string checkPo(string strInvoice)
        {
            string strRet = string.Empty;
            using (OracleDB oraDB = new OracleDB())
            {
                string strSql = string.Empty;
                strSql = @"   select distinct  b.po_number from sap.cmcs_sfc_packing_lines_all a,shp.upd_order_information b          
                                         WHERE  a.customer_po=b.po_number and a.invoice_number ='" + strInvoice + "'";

                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strRet = odr[0].ToString();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] Checkrepeat " + ex.Message);
                }
            }
            return strRet;
        }
        public bool CheckFieldValue(string Invoice, string ModelName, string FieldValue, string Fieldimei, int FieldIndex)
        {
            string errorMessagex = string.Empty;
            switch (FieldIndex)
            {

                case 4: if (FieldValue == "")
                    {
                        errorMessagex = "Generation Date  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "Generation Date  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 5: if (FieldValue == "")
                    {
                        errorMessagex = "APC  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "APC  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    } break;
                case 6: if (FieldValue == "")
                    {
                        errorMessagex = "TransceiverModel  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "TransceiverModel  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 7: if (FieldValue == "")
                    {
                        errorMessagex = "CustomerModel  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "CustomerModel  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 8: if (FieldValue == "")
                    {
                        errorMessagex = "MarketName  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "MarketName  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 9: if (FieldValue == "")
                    {
                        errorMessagex = "ItemCode  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "ItemCode  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 11: if (FieldValue == "")
                    {
                        errorMessagex = "ShipDate  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "ShipDate  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 14: if (FieldValue == "")
                    {
                        errorMessagex = "ShipToCustomerName  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "ShipToCustomerName  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 15: if (FieldValue == "")
                    {
                        errorMessagex = "ShipToCity  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "ShipToCity  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 16: if (FieldValue == "")
                    {
                        errorMessagex = "ShipToCountry  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "ShipToCountry  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;

                case 18: if (FieldValue == "")
                    {
                        errorMessagex = "Sold-to Customer Name  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "Sold-to Customer Name  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 20: if (FieldValue == "")
                    {
                        errorMessagex = "Track ID  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "Track ID  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 23: if (FieldValue == "")
                    {
                        errorMessagex = "Po Number  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "Po Number  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 24: if (FieldValue == "")
                    {
                        errorMessagex = "So Number  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "So Number  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;
                case 26: //NKEY
                    if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
                    {
                        errorMessagex = "NetworkUnlockCode  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "NetworkUnlockCode  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    else if (FieldValue == "")
                    {
                        errorMessagex = "NetworkUnlockCode  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "NetworkUnlockCode  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    else if ((FieldValue.Length != 16 && FieldValue.Length != 8) && FieldDefine.UPDField.NetworkUnlockCodeModel.IndexOf(ModelName) > 0)
                    {
                        errorMessagex = "NetworkUnlockCode's  length  nust  be  16  or  8";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "NetworkUnlockCode's  length  nust  be  16  or  8, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;

                case 29: //NSKEY
                    if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
                    {
                        errorMessagex = "SIMUnlockCode  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "SIMUnlockCode  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    else if ((FieldValue.Length != 16 && FieldValue.Length != 8) && FieldDefine.UPDField.SIMUnlockCodeModel.IndexOf(ModelName) > 0)
                    {
                        errorMessagex = "SIMUnlockCode's  length  nust  be  16  or  8";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "SIMUnlockCode's  length  nust  be  16  or  8, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    else if (FieldValue == "")
                    {
                        errorMessagex = "SIMUnlockCode  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "SIMUnlockCode's  length  nust  be  16  or  8, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;

                case 30: //PRIVILEGEPWD
                    if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
                    {
                        errorMessagex = "ServicePasscode  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "ServicePasscode  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    if (FieldValue == "" && FieldDefine.UPDField.ServicePasscodeModel.IndexOf(ModelName) > 0)
                    {
                        errorMessagex = "ServicePasscode  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "ServicePasscode  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;

                case 35: if (FieldValue == "")
                    {
                        errorMessagex = "MSN  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "MSN  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    } break;
                case 56: if (FieldValue == "")
                    {
                        errorMessagex = "Shipment Number  is  Null";
                        Inserterror(Invoice, errorMessagex, Fieldimei);
                        AddMail("UPD FILE Invoice : " + Invoice, "", "", "Shipment Number  is  Null, " + ModelName + ",IMEI:" + Fieldimei, "");
                        return false;
                    }
                    break;

                default:
                    return true;

            }
            return true;

        }
        public string Checkrepeat(string strInvoice)
        {
            string strRet = string.Empty;
            using (OracleDB oraDB = new OracleDB())
            {
                string strSql = string.Empty;
                strSql = @"SELECT DISTINCT INVOICE,DECODE(COUNTRE,NULL,1,COUNTRE) FROM SHP.UPD_DATALOAD_DETAIL_T WHERE  SHIP_DATE IS NOT NULL AND INVOICE='" + strInvoice + "'";
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strRet = "" + odr[0].ToString() + "#" + odr[1].ToString() + "";
                            break;
                        }

                    }


                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] Checkrepeat " + ex.Message);
                }
            }
            return strRet;
        }
        public string GetCustModelType(string strInvoice)
        {
            string strRet = string.Empty;
            using (OracleDB oraDB = new OracleDB())
            {
                string strSql = string.Empty;
                //strSql = @"SELECT DECODE(CUST_MODEL,'CAS','Castle','HER','Heartland',CUST_MODEL) FROM CDMA_DC.R_INVOICE_T WHERE INVOICE_NUMBER=" + strInvoice;// CREATE_TIME>SYSDATE-1 AND
                strSql = @"select DISTINCT  B.MODEL AS Mode_Type  FROM SAP.CMCS_SFC_PACKING_LINES_ALL a,
                                    SHP.CMCS_SFC_SHIPPING_DATA b where internal_carton is not null and A.INTERNAL_CARTON=B.CARTON_NO 
                                    AND A.INVOICE_NUMBER='" + strInvoice + "'";
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strRet = odr[0].ToString();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetCustModelType " + ex.Message);
                }
            }
            return strRet;
        }
        public bool InsertDetail(string strUPDfile, string strDNnumber, string Customer, string Module, string country, string qty, string strshipdate, int countrep)
        {
            bool bRet = false;
            string strSql = string.Empty;
            string strSqlrep = string.Empty;
            if (countrep == 1)
            {
                strSql = "INSERT INTO SHP.UPD_DATALOAD_DETAIL_T(INVOICE,SHIP_QTY,Model,Ship_to,Customer,SHIP_DATE,Filename,CREATE_DATE,ACTION) " +
                                     "VALUES('" + strDNnumber + "','" + qty + "','" + Module + "','" + country + "','" + Customer + "'," +
                                     "TO_DATE('" + Convert.ToDateTime(strshipdate).ToString("yyyy/MM/dd") + "','YYYY/MM/DD')" + ",'" + strUPDfile + "',sysdate,'NORMAL')";
            }
            else
            {
                strSql = "update SHP.UPD_DATALOAD_DETAIL_T set COUNTRE='" + countrep + "'  where INVOICE='" + strDNnumber + "'";
                strSqlrep = "INSERT INTO SHP.UPD_REPSEND_DETAIL_T(INVOICE,SHIP_QTY,MODELX,Ship_to,Customer,SHIP_DATE,Filename,CREATE_DATE,ACTION) " +
                                     "VALUES('" + strDNnumber + "','" + qty + "','" + Module + "','" + country + "','" + Customer + "'," +
                                     "TO_DATE('" + Convert.ToDateTime(strshipdate).ToString("yyyy/MM/dd") + "','YYYY/MM/DD')" + ",'" + strUPDfile + "',sysdate,'NORMAL')";


                try
                {
                    using (OracleDB oraDB = new OracleDB())
                    {
                        bRet = oraDB.ExecuteNonQuery(strSqlrep);
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] record" + ex.Message);
                }

            }
            try
            {
                using (OracleDB oraDB = new OracleDB())
                {
                    bRet = oraDB.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] record" + ex.Message);
            }

            return bRet;
        }
        public bool Inserterror(string strDNnumber, string strmessage, string strimei)
        {
            bool bRet = false;
            string strSql = string.Empty;

            strSql = "INSERT INTO SHP.UPD_INVOICE_HAND_TEMP (INVOICE,MESSAGE,IMEI) " +
                                 "VALUES('" + strDNnumber + "','" + strmessage + "','" + strimei + "')";

            try
            {
                using (OracleDB oraDB = new OracleDB())
                {
                    bRet = oraDB.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] record" + ex.Message);
            }

            return bRet;
        }
        public bool UpdateImeiTime(string strInvoice, int sendcountx)
        {
            bool bRet = false;
            string strSqlUpdate = string.Empty;
            if (sendcountx == 1)
            {
                strSqlUpdate = @"update SHP.UPD_DATALOAD_DETAIL_T set IMEI_FILE_TIME=sysdate  where INVOICE='" + strInvoice + "'";
            }
            else
            {
                strSqlUpdate = @"update SHP.UPD_REPSEND_DETAIL_T set IMEI_FILE_TIME=sysdate  where INVOICE='" + strInvoice + "' AND IMEI_FILE_TIME IS NULL";
            }

            try
            {
                using (OracleDB oraDB = new OracleDB())
                {
                    bRet = oraDB.ExecuteNonQuery(strSqlUpdate);
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR]UpdateImeiTime " + ex.Message);
            }

            return bRet;
        }
        public string UploadASNFile()
        {
            string strRes = "OK";
            return strRes;
        }
        public bool UploadUPDFile(string strInvoice)
        {
            bool bRet = false;
            for (int i = 0; i < 3; i++)
            {
                if (!UploadFile())
                {
                    if (i == 2)
                    {
                        OracleDB.Write("[ERROR] upload " + strInvoice + " failed");
                        OracleDB.Write("[SUCCEED] end");
                        return bRet;
                    }
                    System.Threading.Thread.Sleep(UPLOAD_FAIL_WAIT_DURATION);
                }
                else
                {
                    break;
                }
            }
            bRet = true;
            return bRet;
        }
        public bool UploadFile()
        {
            bool bRet = true;
            string strErrorInfo = string.Empty;
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = UPD_UPLOAD_PROGRAM_PATH + @"\mcms_client.bat";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.WorkingDirectory = UPD_UPLOAD_PROGRAM_PATH;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                OracleDB.Write("StandardOutput Message:");
                OracleDB.Write(p.StandardOutput.ReadToEnd());
                strErrorInfo = p.StandardError.ReadToEnd();
                OracleDB.Write("StandardError Message:");
                OracleDB.Write(strErrorInfo);
                if (strErrorInfo == null || strErrorInfo.Length == 0)
                {
                    OracleDB.Write("[SUCCEED] upload file");
                }
                else
                {
                    OracleDB.Write("[ERROR] upload file failed");
                    bRet = false;
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] upload file " + ex.Message);
                bRet = false;
            }
            return bRet;

        }
        public static void KillExcelProcess(DateTime Process_BeforeTime, DateTime Process_AfterTime)
        {
            foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                DateTime ProcessBeginTime = pro.StartTime;
                if (((ProcessBeginTime >= Process_BeforeTime) && (ProcessBeginTime <= Process_AfterTime)) || pro.StartTime.AddMinutes(10) < DateTime.Now)
                {
                    try
                    {
                        pro.Kill();
                    }
                    catch { ;}
                }
            }
        }
        public bool FtpASNFile(string strFilePath, string strFileName, string strFolder)
        {

            strFolder = "ASN_TEST";
            bool bRet = true;
            FTPClient ftp = new FTPClient("ftp6.foxconn.com.cn", "/", "motouser", "d3s!aguGa7", 21);
            try
            {
                ftp.Connect();
                ftp.ChDir("/" + strFolder);
                ftp.Put(strFilePath);
                return bRet;
            }
            catch
            {
                return false;
            }
        }
        public bool MailIMEI(string strKeyWord, string strFilePath, string strFileName, string strErrMsg, string strShipCode, string strstrmodule, string strstrshipcountry)
        {

            bool bRet = false;
            string strSql = string.Empty;
            string strSendto = string.Empty;
            string strshipcode = string.Empty;
            strKeyWord = strKeyWord.Replace("'", "");

            Mail mail = new Mail();
            mail.SendProgram = "IMEI";
            mail.CreateTime = DateTime.Now;
            mail.FinishedMark = "0";

            string strTemp = string.Empty;


            if (strErrMsg.Equals(""))
            {
                strTemp += "File Send OK\r\n";
                mail.NoteSubject = "OK , (MSE-TJ) " + strKeyWord + "  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                strTemp += strErrMsg;
                mail.NoteSubject = "FAIL , (MSE-TJ) " + strKeyWord + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            strTemp += "FileName : " + strFileName;

            strTemp += "\r\n";
            strTemp += "\r\n";
            strTemp += strKeyWord + "文件每天由系統自動發送";
            mail.NoteContent = strTemp;

            //  mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
            switch (strstrmodule)
            {
                case "QAZ":
                    if (strstrshipcountry.ToUpper() == "CHINA")
                    {
                        strshipcode = "100411";
                    }
                    else
                    {
                        strshipcode = "100412";
                    }
                    break;
                case "DMQ":
                    strshipcode = "100413";
                    break;
                case "AVON":
                    strshipcode = "100411";
                    break;
                default:
                    strshipcode = "100412";
                    break;
            }

            using (OracleDB oraDB = new OracleDB())
            {
                strSql = @"SELECT distinct  MAIL_ADDR FROM SHP.R_IMEI_COUNTRY_LIST A
                                        WHERE  A.SHIP_TO_CODE='" + strshipcode + "'";// CREATE_TIME>SYSDATE-1 
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        if (odr.Read())
                        {
                            strSendto = odr[0].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] Get Mail Address Error  " + strShipCode + " " + ex.Message);
                }
            }
            //mail.SendTo = strSendto;
            mail.SendTo = OracleDB.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
            mail.SendCC = OracleDB.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
            mail.SendFrom = OracleDB.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
            if (strSendto.Equals(string.Empty))
            {
                mail.NoteSubject += " Warning : Not found mail Addr List";
            }

            strSql = @"INSERT INTO SFC.C_NOTES_SEND (
                       NOTE_SUBJECT, NOTE_CONTENT, SEND_TO,SEND_CC, SEND_FROM, SEND_PROG, 
                       CREATE_DATE, FINISHED_MARK,ATTACHED_PATH) 
                       VALUES ( '" + mail.NoteSubject + "', '" + mail.NoteContent + "', '" + mail.SendTo + @"',
                        '" + mail.SendCC + "', '" + mail.SendFrom + "', '" + mail.SendProgram + @"',
                        TO_DATE('" + mail.CreateTime.ToString("yyyy/MM/dd HH:mm:ss") +
                         "','YYYY/MM/DD HH24:MI:SS'),  '" + mail.FinishedMark + "','" + strFilePath + "')";
            try
            {
                using (OracleDB oraDB = new OracleDB("Mail"))
                {
                    bRet = oraDB.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] AddMail " + ex.Message);
            }

            return bRet;
        }
        public bool MailGFS(string invoice, string qty, string customertype, string gfsfilename, string filename)
        {
            bool bRet = false;
            string strSql = string.Empty;
            string strSendto = string.Empty;
            string strKeyWord = customertype + " GFS FILE";

            Mail mail = new Mail();
            mail.SendProgram = "QAZ GFS File";
            mail.CreateTime = DateTime.Now;
            mail.FinishedMark = "0";


            string strTemp = string.Empty;

            mail.NoteSubject = "(FIH-TJ) " + strKeyWord + "  Send Auto Notice:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strTemp += "File Send OK\r\n";
            strTemp += "FileName : " + filename;

            strTemp += "\r\n";
            strTemp += "\r\n";
            strTemp += strKeyWord + "文件每天由系統自動發送";
            mail.NoteContent = strTemp;

            //   mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
            mail.SendTo = OracleDB.XmlGetValue("CONFIG", "GFSMAILINFO", "TO");
            mail.SendCC = OracleDB.XmlGetValue("CONFIG", "GFSMAILINFO", "CC");
            mail.SendFrom = OracleDB.XmlGetValue("CONFIG", "GFSMAILINFO", "FROM");

            /*using (OracleDB oraDB = new OracleDB())
            {
                strSql = @"SELECT MAIL_ADDR FROM CDMA_DC.R_IMEI_COUNTRY_LIST A
                                        WHERE  A.SHIP_TO_CODE='" + strShipCode + "'";// CREATE_TIME>SYSDATE-1 AND QUASHED='N'  
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        if (odr.Read())
                        {
                            strSendto = odr[0].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write("[ERROR] GetAllInvoiceNumber " + ex.Message);
                }
            }
            mail.SendTo = strSendto;
            mail.SendCC = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
            mail.SendFrom = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
            */
            try
            {
                using (OracleDB oraDB = new OracleDB("Mail"))
                {
                    bRet = oraDB.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] AddMail " + ex.Message);
            }

            strSql = @"INSERT INTO SFC.C_NOTES_SEND (
                                   NOTE_SUBJECT, NOTE_CONTENT, SEND_TO,SEND_CC, SEND_FROM, SEND_PROG, 
                                   CREATE_DATE, FINISHED_MARK,ATTACHED_PATH) 
                                   VALUES ( '" + mail.NoteSubject + "', '" + mail.NoteContent + "', '" + mail.SendTo + @"',
                                    '" + mail.SendCC + "', '" + mail.SendFrom + "', '" + mail.SendProgram + @"',
                                    TO_DATE('" + mail.CreateTime.ToString("yyyy/MM/dd HH:mm:ss") +
                         "','YYYY/MM/DD HH24:MI:SS'),  '" + mail.FinishedMark + "','" + gfsfilename + "')";
            try
            {
                using (OracleDB oraDB = new OracleDB("Mail"))
                {
                    bRet = oraDB.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] AddMail " + ex.Message);
            }

            return bRet;
        }
        public bool AddMail(string strKeyWord, string strFilePath, string strFileName, string strErrMsg, string strShipCode)
        {
            bool bRet = false;
            string strSql = string.Empty;
            string strSendto = string.Empty;
            strKeyWord = strKeyWord.Replace("'", "");

            Mail mail = new Mail();
            mail.SendProgram = strKeyWord;
            mail.CreateTime = DateTime.Now;
            mail.FinishedMark = "0";

            string strTemp = string.Empty;


            if (strErrMsg.ToUpper().Equals("OK"))
            {
                strTemp += " File Send OK\r\n";
                mail.NoteSubject = " Success , MSE(TJ) " + strKeyWord + "  Send Auto Notice:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                strTemp += " FileName : " + strFileName;
            }
            else
            {
                strTemp += strErrMsg;
                mail.NoteSubject = " Error , MSE(TJ) " + strKeyWord + "  Send Auto Notice:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }


            strTemp += "\r\n";
            strTemp += "\r\n";
            strTemp += strKeyWord + "文件每天由系統自動發送";
            mail.NoteContent = strTemp;

            if (strErrMsg.ToUpper().Equals("OK"))
            {
                mail.SendTo = OracleDB.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
                mail.SendCC = OracleDB.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
                mail.SendFrom = OracleDB.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
            }
            else
            {
                mail.SendTo = OracleDB.XmlGetValue("CONFIG", "ERRORMAILINFO", "TO");
                mail.SendCC = OracleDB.XmlGetValue("CONFIG", "ERRORMAILINFO", "CC");
                mail.SendFrom = OracleDB.XmlGetValue("CONFIG", "ERRORMAILINFO", "FROM");
            }

            /*using (OracleDB oraDB = new OracleDB())
            {
                strSql = @"SELECT MAIL_ADDR FROM CDMA_DC.R_IMEI_COUNTRY_LIST A
                                        WHERE  A.SHIP_TO_CODE='" + strShipCode + "'";// CREATE_TIME>SYSDATE-1 AND QUASHED='N'  
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        if (odr.Read())
                        {
                            strSendto = odr[0].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write("[ERROR] GetAllInvoiceNumber " + ex.Message);
                }
            }
            mail.SendTo = strSendto;
            mail.SendCC = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
            mail.SendFrom = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
            */

            strSql = @"INSERT INTO SFC.C_NOTES_SEND (
                       NOTE_SUBJECT, NOTE_CONTENT, SEND_TO,SEND_CC, SEND_FROM, SEND_PROG, 
                       CREATE_DATE, FINISHED_MARK,ATTACHED_PATH) 
                       VALUES ( '" + mail.NoteSubject + "', '" + mail.NoteContent + "', '" + mail.SendTo + @"',
                        '" + mail.SendCC + "', '" + mail.SendFrom + "', '" + mail.SendProgram + @"',
                        TO_DATE('" + mail.CreateTime.ToString("yyyy/MM/dd HH:mm:ss") +
                         "','YYYY/MM/DD HH24:MI:SS'),  '" + mail.FinishedMark + "','" + strFilePath + "')";
            try
            {
                using (OracleDB oraDB = new OracleDB("Mail"))
                {
                    bRet = oraDB.ExecuteNonQuery(strSql);
                }
            }
            catch (Exception ex)
            {
                OracleDB.Write("[ERROR] AddMail " + ex.Message);
            }

            return bRet;
        }
        public bool GetImei(string strInvoice, string strtype)
        {
            bool strRet = false;
            string strSql = string.Empty;
            string strimeitime = string.Empty;
            strSql = @"SELECT IMEI_FILE_TIME FROM SHP.UPD_DATALOAD_DETAIL_T WHERE INVOICE='" + strInvoice + "'";
            using (OracleDB oraDB = new OracleDB())
            {
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strimeitime = odr[0].ToString();
                        }

                        if (strimeitime.Length == 0)
                        {
                            strRet = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] Get IMEI SQL- " + strimeitime + " " + ex.Message);

                }
            }
            return strRet;

        }
        public bool CreateFile(StringBuilder sb, string FilePathName)
        {
            bool bRet = false;
            if (sb != null && sb.Length > 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(FilePathName))
                    {
                        sw.Write(sb.ToString());
                        sw.Close();
                        bRet = true;
                    }

                    OracleDB.Write("[SUCCEED] create file " + FilePathName);
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] CreateUPDFile " + ex.Message);
                }
            }
            return bRet;
        }
        public string GetAllInvoiceNumberHanle()
        {
            string strRet = string.Empty;
            using (OracleDB oraDB = new OracleDB())
            {
                string strInvoice = string.Empty;
                string strSql = string.Empty;
                strSql = @"select DISTINCT A.PLANT, A.invoice, A.customer_name,A.ITEM_MODULE,b.Ship_To_Country,
                                      (select sum(quantity) from CMCS_SFC_PACKING_LINES_ALL b where b.INVOICE_NUMBER=a.invoice) shipped_qty,
                                      LAST_SHIPMENT_DATE ,C.CUSTOMER_TYPE from SFC.MES_MIT_INVOICE A,CMCS_SFC_PACKING_LINES_ALL B ,SFC.CMCS_SFC_MODEL C
                                      Where A.Invoice = B.INVOICE_NUMBER
                                      AND A.ITEM_MODULE=C.MODEL AND C.MODEL IN ('QAZ','DMQ','A3G','A2G')
                                      and (UPPER(A.CUSTOMER_NAME) LIKE '%MOTO%' or UPPER(A.CUSTOMER_NAME) LIKE '%摩托羅拉%'
                                      or UPPER(A.CUSTOMER_NAME) LIKE '%SUTECH%' or UPPER(A.CUSTOMER_NAME) LIKE '%TX4-SOI%' or 
                                      A.CUSTOMER_NAME LIKE 'FIH (Hong Kong) Limited'
                                      or UPPER(A.CUSTOMER_NAME) LIKE '%MI-AMK%' or UPPER(A.CUSTOMER_NAME) LIKE '%S&B INDUSTRY INC.%') ";
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strInvoice = "" + odr[0].ToString() + "#" + odr[1].ToString() + "#" + odr[2].ToString() + "#" + odr[3].ToString() + "#" + odr[4].ToString() + "#" + odr[5].ToString() + "#" + odr[6].ToString() + "#" + odr[7].ToString() + "*";
                        }

                        if (strInvoice.Length > 0)
                        {
                            strRet = strInvoice.Remove(strInvoice.Length - 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetAllInvoiceNumber " + ex.Message);
                }
            }
            return strRet;
        }
        public string GetFileName(string strFileFullName)
        {
            return strFileFullName.Substring(strFileFullName.LastIndexOf(@"\") + 1, strFileFullName.Length - strFileFullName.LastIndexOf(@"\") - 1);
        }
        public bool CreateGFSFile(StringBuilder sb, string FilePathName, string CT, string strBatchID, string siteName, string strVersion, string strDN)
        {
            bool bRet = false;
            string strHead = string.Empty;
            string strSql = string.Empty;
            if (!Directory.Exists(GetFilePath(FilePathName)))
            {
                Directory.CreateDirectory(GetFilePath(FilePathName));
            }
            if (sb != null && sb.Length > 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(FilePathName))
                    {
                        strHead = CT + "|" + strBatchID + "|" + (sb.ToString().Split('\r').Length - 1).ToString() + "|" + siteName + "|" + strVersion + "\r\n";
                        sw.Write(strHead);
                        sw.Write(sb.ToString());
                        sw.Write("EOF");
                        sw.Close();
                        bRet = true;
                    }

                    if (bRet == true)
                    {
                        bool eRet = false;
                        strSql = @"UPDATE SHP.UPD_DATALOAD_DETAIL_T SET GFS_FILE_TIME=SYSDATE WHERE INVOICE IN ('" + strDN + "')";
                        try
                        {
                            using (OracleDB oraDB = new OracleDB())
                            {
                                eRet = oraDB.ExecuteNonQuery(strSql);
                            }
                        }
                        catch (Exception ex)
                        {
                            OracleDB.Write("[ERROR] record" + ex.Message);
                        }
                    }

                    OracleDB.Write("[SUCCEED] create file " + FilePathName);
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] CreateUPDFile " + ex.Message);
                }
            }
            return bRet;
        }

        private string GetFilePath(string strFileFullName)
        {
            return strFileFullName.Substring(0, strFileFullName.LastIndexOf(@"\"));
        }
        #endregion

        #region Get UPD,IMEI,ASN,GFS
        public List<StringBuilder> Get_tra_UPD(string strPlantx, string strInvoice, string strmachinetypex, string strmodulex, string strshipcountryx, string strCustModelTypex, string qtyx, string strcustomertype)
        {
            string Sqlstr = string.Empty;
            List<StringBuilder> lsb = new List<StringBuilder>();
            #region 工廠代碼獲取
            switch (strPlantx)
            {
                case "WCLA":
                    FactoryCode = "CMCSMCMG2";
                    break;
                case "WCLB":
                    FactoryCode = "CMCSMCME10";
                    break;
                case "WCTA":
                    FactoryCode = "CMCSMCMTY";
                    break;
                case "WSJ2":
                    FactoryCode = "ZFOXTJ-ODM";
                    break;
                default:
                    FactoryCode = "ZFOXTJ-ODM";
                    break;
            }
            #endregion
            #region Moto-GetSql
            if (strcustomertype == "CDMA")
            {

                Sqlstr = @"SELECT 'MEID' f1, d.imeinum f2, 'FIHTJ' f3,
                                       TO_CHAR (d.creation_date, 'yyyy-mm-dd HH24:MI:SS') f4, '' f5,
                                       SUBSTR (DECODE (d.customer_num, NULL, d.serial_num, d.customer_num),1,3) f6,c.cust_pno f7,
                                       DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f8, f.market_name f9,
                                       b.customer_item f10, '' f11,
                                       TO_CHAR(DECODE (c.out_date, NULL, c.in_date, c.out_date),'yyyy-mm-dd HH24:MI:SS') f12, '123456' f13,
                                       '123456' f14, b.ship_to_customername f15, b.ship_to_city f16,
                                       k.iso_code f17, '123456' f18, 'MOTOROLA' f19,
                                       TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f20,
                                       c.serial_no f21, '' f22, b.internal_carton f23, b.customer_po f24,
                                       b.so_number f25, '' f26, e.sub_lock f27, '' f28, '' f29,
                                       e.onetime_lock f30, '' f31,e.pesn f32, e.dmeid f33, e.akey1 f34,
                                       '' f35, d.customer_num f36, '' f37, d.btaddress f38, '' f39, '' f40,
                                       i.akey2_type f41, e.akey2 f42, '' f43, '' f44, '' f45, '' f46, '' f47,
                                       g.software_ver f48, '' f49, '' f50, '' f51, '' f52, '' f53, '' f54,
                                       b.invoice_number f55, b.so_number f56, '' f57, k.iso_code f58,
                                       '' f59, '' f60, b.invoice_number f61, g.porder f62, '' f63,
                                       k.iso_code f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,
                                       '' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77, '' f78, '' f79,
                                       '' f80, '' f81, '' f82
                                  FROM sfc.cmcs_sfc_packing_lines_all b,
                                       shp.cmcs_sfc_shipping_data c,
                                       shp.cmcs_sfc_imeinum d,
                                       sfc.CDMA_UPD_PARSER_MEID_V e,
                                       shp.ros_tch_pn f,
                                       shp.cmcs_sfc_porder g,
                                       sfc.maky_information h,
                                       sfc.maif_information i,
                                       sap.country_iso_link k
                                 WHERE b.internal_carton = c.carton_no
                                   AND (c.imei = d.imeinum)
                                   AND (b.item_number = f.ppart)
                                        AND d.porder = g.porder
                                   AND d.imeinum = e.MEID_ID
                                   AND d.imeinum = h.meid_id
                                   AND h.master_id = i.master_id
                                   AND upper(b.ship_to_country)=K.country_code
                                   AND b.invoice_number IN ('" + strInvoice + "')";
            }
            else if (strcustomertype == "GSM")
            {

                if (strCustModelTypex == "QAZ")
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2,'" + FactoryCode + "' f3,"
                                               + "TO_CHAR(e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                               + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                               + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                               + "f.market_name f8, b.customer_item f9, '' f10,"
                                               + "DECODE (c.out_date,"
                                                      + " NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                                       + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                               + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                               + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                               + "h.sold_tocustomer_name f18,"
                                               + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                               + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                               + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                               + "c.work_order f25, e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                               + "e.privilegepwd f30, '' f31, '' f32, '' f33, '' f34,"
                                               + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                               + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                               + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                               + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                               + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                               + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                               + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                               + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                               + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                          + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                               + "shp.cmcs_sfc_packing_lines_all b,"
                                               + "shp.cmcs_sfc_shipping_data c,"
                                               + "shp.cmcs_sfc_imeinum d,"
                                               + "" + strmodulex + ".e2pconfig e,"
                                               + "shp.ros_tch_pn f,"
                                               + "shp.cmcs_sfc_porder g,"
                                               + "shp.upd_order_information h,"
                                               + "shp.cmcs_sfc_carton j,"
                                               + "sap.country_iso_link k "
                                         + "WHERE a.order_carton_no = b.internal_carton "
                                           + "AND b.internal_carton = c.carton_no "
                                           + "AND b.customer_po = h.po_number "
                                           + "AND b.item_number = f.ppart "
                                           + "AND d.porder = g.porder "
                                           + "AND c.imei = d.imeinum "
                                           + "AND ( c.imei = e.imei  "
                                                + "AND e.status = 'PASS' "
                                                + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + strmodulex + ".e2pconfig h "
                                                + "WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                           + "AND j.carton_no = b.internal_carton "
                                           + "AND UPPER (b.ship_to_country) = k.country_code "
                                           + "AND b.invoice_number = a.invoice_no "
                                           + "AND a.invoice_no ='" + strInvoice + "'";
                }
                else if (strCustModelTypex.Substring(0, 3) == "DMQ")
                {

                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, 'ZFOXTJ-ODM' f3,"
                                            + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                            + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                            + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                            + "f.market_name f8, b.customer_item f9, '' f10,"
                                            + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                                    + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                           + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                            + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ "
                                            + "h.sold_tocustomer_name f18,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                            + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                            + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                            + "c.work_order f25, '12345678' f26, '' f27, '' f28, '12345678' f29,"
                                            + "'12345678' f30, '' f31, '' f32, '' f33, '' f34,"
                                            + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                            + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                            + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                            + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                            + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59, '' f60, '' f61,"
                                            + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                            + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                            + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                       + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                            + "shp.cmcs_sfc_packing_lines_all b,"
                                            + "shp.cmcs_sfc_shipping_data c,"
                                            + "shp.cmcs_sfc_imeinum d,"
                                            + "" + strmodulex + ".e2pconfig e,"
                                            + "shp.ros_tch_pn f,"
                                            + "shp.cmcs_sfc_porder g,"
                                            + "shp.upd_order_information h,"
                                            + "shp.cmcs_sfc_carton j,"
                                            + "sap.country_iso_link k "
                                      + "WHERE a.order_carton_no = b.internal_carton "
                                        + "AND b.internal_carton = c.carton_no "
                                        + "AND b.customer_po = h.po_number "
                                        + "AND b.item_number = f.ppart "
                                        + "AND d.porder = g.porder "
                                        + "AND c.imei = d.imeinum "
                                        + "AND (c.imei = e.imei "
                                             + "AND e.status = 'PASS' "
                                             + "AND e.e2pdate = "
                                                         + "(SELECT MAX (h.e2pdate) FROM " + strmodulex + ".e2pconfig h WHERE h.imei = c.imei "
                                             + "AND h.status = 'PASS')) "
                                          + "AND j.carton_no = b.internal_carton "
                                          + "AND UPPER (b.ship_to_country) = k.country_code "
                                          + "AND b.invoice_number = a.invoice_no "
                                          + "AND a.invoice_no ='" + strInvoice + "'";

                }
                else if (strCustModelTypex == "QAZ-DS")
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                            + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                            + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                            + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                            + "f.market_name f8, b.customer_item f9, '' f10,"
                                            + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                            + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                            + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17," /*H.CUSTOMER_NAME*/
                                            + "h.sold_tocustomer_name f18,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                            + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                            + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                            + "c.work_order f25, e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                            + "e.privilegepwd f30, '' f31, '' f32, '' f33, '' f34,"
                                            + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                            + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,"
                                            + "a.invoice_no f41,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                            + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                            + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                            + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,"
                                            + "'' f60, '' f61,"
                                            + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                            + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                            + "d.imeinum2 f71, 'IMEI' f72, '' f73, '' f74, '' f75, '' f76,"
                                            + "'' f77, '' f78"
                                    + " FROM shp.cmcs_sfc_ship_carton_map a,"
                                            + "shp.cmcs_sfc_packing_lines_all b,"
                                            + "shp.cmcs_sfc_shipping_data c,"
                                            + "shp.cmcs_sfc_imeinum d,"
                                            + "" + strmodulex + ".e2pconfig e,"
                                            + "shp.ros_tch_pn f,"
                                            + "shp.cmcs_sfc_porder g,"
                                            + "shp.upd_order_information h,"
                                            + "shp.cmcs_sfc_carton j,"
                                            + "sap.country_iso_link k"
                                   + " WHERE a.order_carton_no = b.internal_carton"
                                        + " AND b.internal_carton = c.carton_no"
                                        + " AND b.customer_po = h.po_number"
                                        + " AND b.item_number = f.ppart"
                                        + " AND d.porder = g.porder"
                                        + " AND c.imei = d.imeinum"
                                        + " AND (    c.imei = e.imei"
                                             + " AND e.status = 'PASS'"
                                             + " AND e.e2pdate = (SELECT MAX (h.e2pdate)"
                                                            + "  FROM " + strmodulex + ".e2pconfig h"
                                                            + "  WHERE h.imei = c.imei AND h.status = 'PASS')"
                                            + " ) "
                                       + " AND j.carton_no = b.internal_carton"
                                       + " AND UPPER (b.ship_to_country) = k.country_code"
                                       + " AND b.invoice_number = a.invoice_no"
                                       + " AND a.invoice_no ='" + strInvoice + "'";

                }
                else if (strCustModelTypex.Substring(0, 3) == "A3G" || strCustModelTypex.Substring(0, 3) == "A2G")
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                            + "TO_CHAR (e.create_date, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                            + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                            + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                            + "f.market_name f8, b.customer_item f9, '' f10,"
                                            + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                            + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                            + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                            + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ h.sold_tocustomer_name f18,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                            + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                            + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                            + "c.work_order f25, SUBSTR(e.th7, 16, 8) f26, '' f27, '' f28, SUBSTR (e.th7, 48, 8) f29,"
                                            + "'12345678' f30,'' f31, '' f32, '' f33, '' f34,"
                                            + "DECODE (d.customer_num, NULL, 'M14' || SUBSTR (d.imeinum, 8, 7), d.customer_num) f35,"
                                            + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,'' f43, '' f44, '' f45, '' f46,"
                                            + "'' f47, '' f48, '' f49,"
                                            + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                            + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                            + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                            + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                            + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                       + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                            + "shp.cmcs_sfc_packing_lines_all b,"
                                            + "shp.cmcs_sfc_shipping_data c,"
                                            + "shp.cmcs_sfc_imeinum d,"
                                            + "(SELECT   MAX (ID || th7) th7, th26,MAX (create_date) create_date FROM testinfo.testinfo_head"
                                            + " WHERE status = 'PASS' AND LENGTH (th7) = 80 AND station_name = 'E2P' GROUP BY th26) e, "
                                            + "shp.ros_tch_pn f,"
                                            + "shp.cmcs_sfc_porder g,"
                                            + "shp.upd_order_information h,"
                                            + "shp.cmcs_sfc_carton j,"
                                            + "sap.country_iso_link k "
                                      + "WHERE a.order_carton_no = b.internal_carton "
                                        + "AND a.invoice_no = b.invoice_number "
                                        + "AND b.customer_po = h.po_number "
                                        + "AND b.internal_carton = c.carton_no "
                                        + "AND b.internal_carton = j.carton_no "
                                        + "AND b.item_number = f.ppart "
                                        + "AND c.imei = d.imeinum "
                                        + "AND c.imei = e.th26 "
                                        + "AND d.porder = g.porder "
                                        + "AND UPPER (b.ship_to_country) = k.country_code "
                                        + "AND a.invoice_no ='" + strInvoice + "' "
                                        + "AND b.invoice_number ='" + strInvoice + "'";
                }
                else
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                        + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                        + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                        + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                        + "f.market_name f8, b.customer_item f9, '' f10,"
                                        + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                        + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                        + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                        + "h.sold_tocustomer_name f18,"
                                        + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                        + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                        + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                        + "c.work_order f25, '12345678' f26, '' f27, '' f28, '12345678' f29,"
                                        + "'12345678' f30, '' f31, '' f32, '' f33, '' f34,"
                                        + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                        + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                        + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                        + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                        + " '' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                        + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                        + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                        + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                        + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                   + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                       + "shp.cmcs_sfc_packing_lines_all b,"
                                       + "shp.cmcs_sfc_shipping_data c,"
                                       + "shp.cmcs_sfc_imeinum d,"
                                       + "" + strmodulex + ".e2pconfig e,"
                                       + "shp.ros_tch_pn f,"
                                       + "shp.cmcs_sfc_porder g,"
                                       + "shp.upd_order_information h,"
                                       + "shp.cmcs_sfc_carton j,"
                                       + "sap.country_iso_link k "
                                   + "WHERE a.order_carton_no = b.internal_carton "
                                   + "AND b.customer_po = h.po_number "
                                   + "AND b.invoice_number = a.invoice_no "
                                   + "AND b.internal_carton = c.carton_no "
                                   + "AND c.imei = d.imeinum "
                                   + "AND b.item_number = f.ppart "
                                   + "AND d.porder = g.porder "
                                   + "AND (c.imei = e.imei "
                                         + "AND e.status = 'PASS'"
                                         + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + strmodulex + ".e2pconfig h WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                   + "AND j.carton_no = b.internal_carton "
                                   + "AND UPPER (b.ship_to_country) = k.country_code "
                                   + "AND a.invoice_no ='" + strInvoice + "'";

                }
            }



            #endregion
            using (OracleDB oraDB = new OracleDB())
            {
                try
                {

                    using (DataSet odr = oraDB.GetDataSet(Sqlstr))
                    {

                        StringBuilder sbupd = new StringBuilder();
                        int iColNum = odr.Tables[0].Columns.Count;
                        string CountQty = odr.Tables[0].Rows.Count.ToString();
                        StringBuilder sbs = new StringBuilder();
                        if (CountQty == qtyx)
                        {



                            for (int n = 0; n < odr.Tables[0].Rows.Count; n++)
                            {
                                //#region 特定欄位判定

                                //for (int rownum = 0; rownum < 40; rownum++)
                                //{
                                //    Boolean FunChk = CheckFieldValue(strInvoice, strmodulex, odr.Tables[0].Rows[n][rownum].ToString(), odr.Tables[0].Rows[n][1].ToString(), rownum + 1);
                                //    if (FunChk == false)
                                //    {
                                //        return lsb;
                                //    }
                                //}
                                //#endregion
                                #region 循環|放入sbs
                                for (int i = 0; i < iColNum; i++)
                                {
                                    sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                                    sbs.Append("|");
                                }
                                sbs.AppendLine();

                                if ((sbupd.Length + sbs.Length) <= SINGLE_UPD_FILE_MAX_LENGTH)
                                {
                                    sbupd.Append(sbs);
                                    sbs = new StringBuilder();
                                }
                                else
                                {
                                    lsb.Add(sbupd);
                                    sbupd = new StringBuilder();
                                    sbupd.Append(sbs);
                                    sbs = new StringBuilder();
                                }
                                #endregion
                            }

                            if (sbupd.Length > 0)
                            {
                                lsb.Add(sbupd);
                            }

                        }
                        else
                        {

                            AddMail("UPD FILE Invoice : " + strInvoice, "", "", "Number does not match,please make sure download DN or Print 3s,4s!", "");


                            return lsb;
                        }

                    }


                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetInvoiceInfo " + ex.Message);
                }
            }
            return lsb;

        }
        public string Get_tra_IMEI(string strInvoice, string strModel, string strcountry)
        {
            string strIMEIFile = "-";
            string strCustomerName = string.Empty;

            using (OracleDB oraDB = new OracleDB())
            {
                string strPO = "";
                string strsqlcn = string.Empty;

                strsqlcn = "select '' customer_name from dual";
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strsqlcn))
                    {
                        while (odr.Read())
                        {
                            strCustomerName = odr[0].ToString().ToUpper();
                        }

                    }
                }
                catch (Exception ex)
                {
                    strCustomerName = "";
                }
            }

            string strSql = string.Empty;
            #region GetSql-Normal
            strSql = @" SELECT e.SO_NO SALES_PO,g.ppart FIH_PN,G.HW_VER HW,e.SO_NO SO,G.SW_VER SW,
                         LPAD(E.INVOICE,10,'0') INVOICE_NO,g.phone_model XCVR_NO,
                         (SELECT COUNT(b.imei) FROM CMCS_SFC_PACKING_LINES_ALL a,shp.CMCS_SFC_SHIPPING_DATA b 
                         WHERE a.INTERNAL_CARTON=B.CARTON_NO and  a.INVOICE_Number=" + strInvoice + @"  ) QTY ,
                         E.ADDRESS SHIP_TO,G.SA_NO moto_no, 'TIANJIN' FACILITY,
                         TO_CHAR(SYSDATE,'YYYY/MM/DD') CREATEDATE,CUST_PO_NUMBER MOTO_PO, 
                         C.IMEInum IMEI,c.customer_num MSN,'12345678' NP_PW,b.sa_no MODEL,to_char(a.ship_date,'yyyy/mm/ dd') SHIP_DATE,
                         d.caseid  CARTON_ID,lpad(a.INVOICE_Number,10,'0')||'+1095759' PALLET_ID,C.IMEINUM2 IMEI2
                        FROM CMCS_SFC_PACKING_LINES_ALL a,
                             shp.CMCS_SFC_SHIPPING_DATA b,
                             shp.CMCS_SFC_IMEINUM c,
                             shp.cmcs_sfc_carton d,
                             SAP.SAP_INVOICE_INFO e ,
                             SAP.SAP_ORDER_INFO F,
                             ROS_TCH_PN g
                        WHERE  a.INVOICE_Number=" + strInvoice + @"
                               and a.INVOICE_Number=E.INVOICE
                               and a.INTERNAL_CARTON=B.CARTON_NO
                               and B.IMEI=C.IMEINUM 
                               and a.INTERNAL_CARTON=D.CARTON_NO 
                               AND E.SO_NO=F.ORDER_NUMBER
                               and D.PPART=G.PPART
                        ORDER BY CARTON_ID";
            #endregion
            #region GetSql-A3G/A2G-shipto LA
            if ((strModel == "A3G" || strModel == "A2G") && strcountry == "LA")
            {
                strSql = @"SELECT   e.so_no sales_po, g.ppart fih_pn, g.hw_ver hw, e.so_no so,
                                 g.sw_ver sw, LPAD (e.invoice, 10, '0') invoice_no,
                                 g.phone_model xcvr_no,
                                 (SELECT COUNT (b.imei)
                                    FROM cmcs_sfc_packing_lines_all a,
                                         shp.cmcs_sfc_shipping_data b
                                   WHERE a.internal_carton = b.carton_no
                                     AND a.invoice_number =" + strInvoice + @") qty,
                                 e.address ship_to, g.sa_no moto_no, 'TIANJIN' facility,
                                 TO_CHAR (SYSDATE, 'YYYY/MM/DD') createdate, cust_po_number moto_po,
                                 c.imeinum imei, c.customer_num msn, '12345678' np_pw, b.sa_no model,
                                 TO_CHAR (a.ship_date, 'yyyy/mm/ dd') ship_date, d.caseid carton_id,
                                 LPAD (a.invoice_number, 10, '0') || '+1095759' pallet_id,
                                 DECODE(c.imeinum2,'','NA',c.imeinum2) imei2, SUBSTR (h.th7, 16, 8) p_uc,
                                 SUBSTR (h.th7, 48, 8) s_uc, 'NA' s_pc
                            FROM cmcs_sfc_packing_lines_all a,
                                 shp.cmcs_sfc_shipping_data b,
                                 shp.cmcs_sfc_imeinum c,
                                 shp.cmcs_sfc_carton d,
                                 sap.sap_invoice_info e,
                                 sap.sap_order_info f,
                                 ros_tch_pn g,
                                 (SELECT   MAX (ID || th7) th7, th26, MAX (create_date) create_date
                                      FROM testinfo.testinfo_head
                                     WHERE status = 'PASS' AND LENGTH (th7) = 80
                                           AND station_name = 'E2P'
                                  GROUP BY th26) h
                           WHERE a.invoice_number = " + strInvoice + @"
                             AND a.invoice_number = e.invoice
                             AND a.internal_carton = b.carton_no
                             AND b.imei = c.imeinum
                             AND b.imei = h.th26
                             AND a.internal_carton = d.carton_no
                             AND e.so_no = f.order_number
                             AND d.ppart = g.ppart
                        ORDER BY carton_id";
            }
            #endregion
            #region GetSql-DMP-shipto LA
            if (strModel == "DMQ" && strcountry == "LA")
            {
                strSql = @"SELECT   e.so_no sales_po, g.ppart fih_pn, g.hw_ver hw, e.so_no so,
                             g.sw_ver sw, LPAD (e.invoice, 10, '0') invoice_no,
                             g.phone_model xcvr_no,
                             (SELECT COUNT (b.imei)
                                FROM cmcs_sfc_packing_lines_all a,
                                     shp.cmcs_sfc_shipping_data b
                               WHERE a.internal_carton = b.carton_no
                                 AND a.invoice_number =" + strInvoice + @") qty,
                             e.address ship_to, g.sa_no moto_no, 'TIANJIN' facility,
                             TO_CHAR (SYSDATE, 'YYYY/MM/DD') createdate, cust_po_number moto_po,
                             c.imeinum imei, c.customer_num msn, '12345678' np_pw, b.sa_no model,
                             TO_CHAR (a.ship_date, 'yyyy/mm/ dd') ship_date, d.caseid carton_id,
                             LPAD (a.invoice_number, 10, '0') || '+1095759' pallet_id,
                             DECODE (c.imeinum2, '', 'NA', c.imeinum2) imei2, 'NA' p_uc,
                             'NA' s_uc, 'NA' s_pc
                        FROM cmcs_sfc_packing_lines_all a,
                             shp.cmcs_sfc_shipping_data b,
                             shp.cmcs_sfc_imeinum c,
                             shp.cmcs_sfc_carton d,
                             sap.sap_invoice_info e,
                             sap.sap_order_info f,
                             ros_tch_pn g,
                             " + strModel + @".e2pconfig h
                       WHERE a.invoice_number =" + strInvoice + @"
                         AND a.invoice_number = e.invoice
                         AND a.internal_carton = b.carton_no
                         AND b.imei = c.imeinum
                         AND (    c.imeinum = h.imei
                              AND h.status = 'PASS'
                              AND h.e2pdate = (SELECT MAX (h.e2pdate)
                                                 FROM " + strModel + @".e2pconfig h
                                                WHERE h.imei = c.imeinum AND h.status = 'PASS')
                             )
                         AND a.internal_carton = d.carton_no
                         AND e.so_no = f.order_number
                         AND d.ppart = g.ppart
                    ORDER BY carton_id";
            }
            #endregion
            DateTime Process_BeforeTime = DateTime.Now;
            Excel3.Application app = new Excel3.Application();

            using (OracleDB oraDB = new OracleDB())
            {

                try
                {
                    Excel3.Workbook wb = app.Workbooks.Add(true);
                    Excel3.Worksheet st = (Excel3.Worksheet)app.Sheets[1];
                    app.DisplayAlerts = false;
                    app.AlertBeforeOverwriting = false;

                    DataSet odr = oraDB.GetDataSet(strSql);
                    DataTable dt = odr.Tables[0];
                    //throw new Exception("test");
                    bool bInit = false;
                    int n = 8;
                    #region QAZ
                    if (strModel == "QAZ")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!bInit)
                            {

                                if (strCustomerName.Equals("CONCORDE S.P.A."))
                                {
                                    st.Cells[1, 1] = "Model";
                                    st.Cells[1, 2] = "IMEI";
                                    st.Cells[1, 3] = "CARTON ID";
                                    st.Cells[1, 4] = "PALLET ID";
                                }
                                else
                                {
                                    st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                    st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                    st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                    st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                    st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                    st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                    st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                    st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                    st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                    st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                    st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                    st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                    st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                    st.Cells[7, 1] = "IMEI NO.";

                                    //st.Cells[7, 2] = "MEID NO.";
                                    st.Cells[7, 2] = "MSN";
                                    st.Cells[7, 3] = "NP P/W";
                                    st.Cells[7, 4] = "Model";
                                    st.Cells[7, 5] = "Shipment Date";
                                    st.Cells[7, 6] = "CARTON ID";
                                    st.Cells[7, 7] = "PALLET ID";
                                }
                                bInit = true;
                            }
                            break;
                        }

                        int x = 0;
                        if (strCustomerName.Equals("CONCORDE S.P.A."))
                        {

                            object[,] objData = new Object[dt.Rows.Count, 7];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[16].ToString();
                                objData[x, 1] = dr[13].ToString();
                                objData[x, 2] = dr[18].ToString();
                                objData[x, 3] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;
                        }
                        else
                        {
                            object[,] objData = new Object[dt.Rows.Count, 8];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[13].ToString();
                                objData[x, 1] = dr[14].ToString();
                                objData[x, 2] = dr[15].ToString();
                                objData[x, 3] = dr[16].ToString();
                                objData[x, 4] = dr[17].ToString();
                                objData[x, 5] = dr[18].ToString();
                                objData[x, 6] = dr[19].ToString();

                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;

                        }

                    }
                    #endregion
                    #region QAZ-DS
                    else if (strModel == "QAZ-DS")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!bInit)
                            {

                                if (strCustomerName.Equals("CONCORDE S.P.A."))
                                {
                                    st.Cells[1, 1] = "Model";
                                    st.Cells[1, 2] = "IMEI";
                                    st.Cells[1, 3] = "CARTON ID";
                                    st.Cells[1, 4] = "PALLET ID";
                                }
                                else
                                {
                                    st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                    st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                    st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                    st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                    st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                    st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                    st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                    st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                    st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                    st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                    st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                    st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                    st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                    st.Cells[7, 1] = "Pri-IMEI NO.";

                                    //st.Cells[7, 2] = "MEID NO.";
                                    st.Cells[7, 2] = "Sec-IMEI NO.";
                                    st.Cells[7, 3] = "MSN";
                                    st.Cells[7, 4] = "NP P/W";
                                    st.Cells[7, 5] = "Model";
                                    st.Cells[7, 6] = "Shipment Date";
                                    st.Cells[7, 7] = "CARTON ID";
                                    st.Cells[7, 8] = "PALLET ID";
                                }
                                bInit = true;
                            }
                            break;
                        }

                        int x = 0;
                        if (strCustomerName.Equals("CONCORDE S.P.A."))
                        {

                            object[,] objData = new Object[dt.Rows.Count, 7];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[16].ToString();
                                objData[x, 1] = dr[13].ToString();
                                objData[x, 2] = dr[18].ToString();
                                objData[x, 3] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;
                        }
                        else
                        {
                            object[,] objData = new Object[dt.Rows.Count, 8];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[13].ToString();
                                objData[x, 1] = dr[20].ToString();
                                objData[x, 2] = dr[14].ToString();
                                objData[x, 3] = dr[15].ToString();
                                objData[x, 4] = dr[16].ToString();
                                objData[x, 5] = dr[17].ToString();
                                objData[x, 6] = dr[18].ToString();
                                objData[x, 7] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;

                        }
                    }
                    #endregion
                    #region A3G/A2G ship to LA
                    else if ((strModel == "A3G" || strModel == "A2G") && strcountry == "LA")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!bInit)
                            {

                                if (strCustomerName.Equals("CONCORDE S.P.A."))
                                {
                                    st.Cells[1, 1] = "Model";
                                    st.Cells[1, 2] = "IMEI";
                                    st.Cells[1, 3] = "CARTON ID";
                                    st.Cells[1, 4] = "PALLET ID";
                                }
                                else
                                {
                                    st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                    st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                    st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                    st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                    st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                    st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                    st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                    st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                    st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                    st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                    st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                    st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                    st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                    st.Cells[7, 1] = "IMEI NO.";
                                    st.Cells[7, 2] = "IMEI_NO_2";
                                    st.Cells[7, 3] = "MSN";
                                    st.Cells[7, 4] = "P_UC";
                                    st.Cells[7, 5] = "S_UC";
                                    st.Cells[7, 6] = "S_PC";
                                    st.Cells[7, 7] = "Model";
                                    st.Cells[7, 8] = "Shipment Date";
                                    st.Cells[7, 9] = "CARTON ID";
                                    st.Cells[7, 10] = "PALLET ID";
                                }
                                bInit = true;
                            }
                            break;
                        }

                        int x = 0;
                        if (strCustomerName.Equals("CONCORDE S.P.A."))
                        {

                            object[,] objData = new Object[dt.Rows.Count, 7];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[16].ToString();
                                objData[x, 1] = dr[13].ToString();
                                objData[x, 2] = dr[18].ToString();
                                objData[x, 3] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;
                        }
                        else
                        {
                            object[,] objData = new Object[dt.Rows.Count, 10];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[13].ToString();
                                objData[x, 1] = dr[20].ToString();
                                objData[x, 2] = dr[14].ToString();
                                objData[x, 3] = dr[21].ToString();
                                objData[x, 4] = dr[22].ToString();
                                objData[x, 5] = dr[23].ToString();
                                objData[x, 6] = dr[16].ToString();
                                objData[x, 7] = dr[17].ToString();
                                objData[x, 8] = dr[18].ToString();
                                objData[x, 9] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;

                        }

                    }
                    #endregion
                    #region DMQ ship to LA
                    else if (strModel == "DMQ" && strcountry == "LA")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!bInit)
                            {

                                if (strCustomerName.Equals("CONCORDE S.P.A."))
                                {
                                    st.Cells[1, 1] = "Model";
                                    st.Cells[1, 2] = "IMEI";
                                    st.Cells[1, 3] = "CARTON ID";
                                    st.Cells[1, 4] = "PALLET ID";
                                }
                                else
                                {
                                    st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                    st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                    st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                    st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                    st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                    st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                    st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                    st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                    st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                    st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                    st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                    st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                    st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                    st.Cells[7, 1] = "IMEI NO.";
                                    st.Cells[7, 2] = "IMEI_NO_2";
                                    st.Cells[7, 3] = "MSN";
                                    st.Cells[7, 4] = "P_UC";
                                    st.Cells[7, 5] = "S_UC";
                                    st.Cells[7, 6] = "S_PC";
                                    st.Cells[7, 7] = "Model";
                                    st.Cells[7, 8] = "Shipment Date";
                                    st.Cells[7, 9] = "CARTON ID";
                                    st.Cells[7, 10] = "PALLET ID";
                                }
                                bInit = true;
                            }
                            break;
                        }

                        int x = 0;
                        if (strCustomerName.Equals("CONCORDE S.P.A."))
                        {

                            object[,] objData = new Object[dt.Rows.Count, 7];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[16].ToString();
                                objData[x, 1] = dr[13].ToString();
                                objData[x, 2] = dr[18].ToString();
                                objData[x, 3] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;
                        }
                        else
                        {
                            object[,] objData = new Object[dt.Rows.Count, 10];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[13].ToString();
                                objData[x, 1] = dr[20].ToString();
                                objData[x, 2] = dr[14].ToString();
                                objData[x, 3] = dr[21].ToString();
                                objData[x, 4] = dr[22].ToString();
                                objData[x, 5] = dr[23].ToString();
                                objData[x, 6] = dr[16].ToString();
                                objData[x, 7] = dr[17].ToString();
                                objData[x, 8] = dr[18].ToString();
                                objData[x, 9] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;

                        }
                    }
                    #endregion
                    #region else
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!bInit)
                            {

                                if (strCustomerName.Equals("CONCORDE S.P.A."))
                                {
                                    st.Cells[1, 1] = "Model";
                                    st.Cells[1, 2] = "IMEI";
                                    st.Cells[1, 3] = "CARTON ID";
                                    st.Cells[1, 4] = "PALLET ID";
                                }
                                else
                                {
                                    st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                    st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                    st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                    st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                    st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                    st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                    st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                    st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                    st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                    st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                    st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                    st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                    st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                    st.Cells[7, 1] = "IMEI NO.";

                                    //st.Cells[7, 2] = "MEID NO.";
                                    st.Cells[7, 2] = "MSN";
                                    st.Cells[7, 3] = "NP P/W";
                                    st.Cells[7, 4] = "Model";
                                    st.Cells[7, 5] = "Shipment Date";
                                    st.Cells[7, 6] = "CARTON ID";
                                    st.Cells[7, 7] = "PALLET ID";
                                }
                                bInit = true;
                            }
                            break;
                        }

                        int x = 0;
                        if (strCustomerName.Equals("CONCORDE S.P.A."))
                        {

                            object[,] objData = new Object[dt.Rows.Count, 7];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[16].ToString();
                                objData[x, 1] = dr[13].ToString();
                                objData[x, 2] = dr[18].ToString();
                                objData[x, 3] = dr[19].ToString();
                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;
                        }
                        else
                        {
                            object[,] objData = new Object[dt.Rows.Count, 8];
                            foreach (DataRow dr in dt.Rows)
                            {
                                objData[x, 0] = dr[13].ToString();
                                objData[x, 1] = dr[14].ToString();
                                objData[x, 2] = dr[15].ToString();
                                objData[x, 3] = dr[16].ToString();
                                objData[x, 4] = dr[17].ToString();
                                objData[x, 5] = dr[18].ToString();
                                objData[x, 6] = dr[19].ToString();

                                x++;
                            }
                            Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                            range.NumberFormatLocal = "@";
                            range.Value2 = objData;

                        }


                    }
                    #endregion
                    strIMEIFile = string.Format(IMEI_FILE_NAME, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    wb.SaveAs(strIMEIFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel3.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //Excel3.XlFileFormat.xlExcel8

                    app.ActiveWorkbook.Close(true, strIMEIFile, Type.Missing);
                    app.Workbooks.Close();
                    app.Quit();

                    DateTime Process_AfterTime = DateTime.Now;
                    KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                    GC.Collect();

                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetInvoiceInfo " + ex.Message);
                    app.Quit();
                    DateTime Process_AfterTime = DateTime.Now;
                    KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                    GC.Collect();
                }
            }

            return strIMEIFile;
        }
        public List<StringBuilder> Get_tra_GFS(string strInvoice)
        {
            List<StringBuilder> lsb = new List<StringBuilder>();
            string strSql = string.Empty;
            strSql = @"SELECT 'UN' F1,'CM' F2,rownum F3,C.CUSTOMER_NUM F4,C.SERIAL_NUM F5,
                             C.BTADDRESS F6,C.imeinum F7,B.DMEID F8,B.AKEY1 F9,
                             B.AKEY2 F10,B.SUB_LOCK F11,B.ONETIME_LOCK F12,B.PESN F13,
                             LPAD(TO_NUMBER(SUBSTR(B.PESN,1,2),'XXX'),3,'0')||LPAD(TO_NUMBER(SUBSTR(B.PESN,3,LENGTH(B.PESN)),'XXXXXXXX'),8,'0') F14,
                             '' F15, ''  F16,'' F17, '' F18,'' F19,
                             '' F20,F.software_ver F21,'' F22,D.MARKET_NAME F23,D.PHONE_MODEL F24,
                             A.INTERNAL_CARTON F25,LPAD('FOUR_S',20,'0') F26,TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') F27,
                             TO_CHAR(A.LAST_UPDATE_DATE,'YYYYMMDDHH24MISS') F28
                             FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SFC.CDMA_UPD_PARSER_MEID_V B,SHP.CMCS_SFC_IMEINUM C,SHP.ROS_TCH_PN D,
                             SHP.CMCS_SFC_SHIPPING_DATA E,shp.cmcs_sfc_porder F WHERE A.INTERNAL_CARTON=E.CARTON_NO AND E.IMEI=C.IMEINUM AND
                             C.imeinum=B.MEID_ID AND C.PPART=D.PPART AND C.porder = F.porder
                             AND A.INVOICE_NUMBER IN ('" + strInvoice + "')";
            using (OracleDB oraDB = new OracleDB())
            {
                try
                {
                    using (DataSet odr = oraDB.GetDataSet(strSql))
                    {
                        if (odr.Tables[0].Rows.Count < 1)
                            return lsb;
                        StringBuilder sbupd = new StringBuilder();
                        int iColNum = odr.Tables[0].Columns.Count;
                        StringBuilder sbs = new StringBuilder();
                        for (int n = 0; n < odr.Tables[0].Rows.Count; n++)
                        {
                            for (int i = 0; i < iColNum; i++)
                            {
                                sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                                sbs.Append("|");
                            }
                            sbs.AppendLine();

                            if ((sbupd.Length + sbs.Length) <= SINGLE_UPD_FILE_MAX_LENGTH)
                            {
                                sbupd.Append(sbs);
                                sbs = new StringBuilder();
                            }
                            else
                            {
                                lsb.Add(sbupd);
                                sbupd = new StringBuilder();
                                sbupd.Append(sbs);
                                sbs = new StringBuilder();
                            }
                        }

                        if (sbupd.Length > 0)
                        {
                            lsb.Add(sbupd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetInvoiceInfo " + ex.Message);
                }
            }

            return lsb;
        }
        public void Get_tra_ASN(string strInvoice, string strDate, string supdfile)
        {
            //GetInvoiceFromSAPTJ(strInvoice);

            SAPRFC.SAPFunction sapf;
            sapf = new SAPRFC.SAPFunction();
            sapf = new SAPRFC.SAPFunction("10.134.28.98", "802", "rfctymcm", "mvurn8x7", "00");  //zmf  正式802 環境


            string strSqlqty = string.Empty;
            string strshipqty = string.Empty;
            string strSql1 = string.Empty;
            strSqlqty = @"SELECT ship_qty  FROM SFC.UPD_DATALOAD_DETAIL_T   WHERE  invoice= '" + strInvoice + "'";// CREATE_TIME>SYSDATE-1 AND
            using (OracleDB oraDB = new OracleDB())
            {
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSqlqty))
                    {
                        while (odr.Read())
                        {
                            strshipqty = odr[0].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] SHP.UPD_DATALOAD_DETAIL_T shipqty is error " + ex.Message);

                }
            }

            if (strshipqty.Length > 0)
            {
                try
                {
                    DataSet ds = sapf.GetInvoice(strInvoice);
                    if (ds != null)
                    {
                        DataTable dtHead = ds.Tables[0];
                        DataTable dtDetail = ds.Tables[1];
                        StringBuilder sbs = new StringBuilder();
                        sbs.Append(dtHead.Rows[0][0].ToString() + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + dtDetail.Rows[0]["BSTKD"].ToString() + "|");
                        sbs.Append("1" + "|" + dtDetail.Rows[0]["KDMAT"].ToString() + "|" + strshipqty + "|");
                        sbs.Append(DateTime.Now.ToString("dd/MM/yyyy") + "||" + dtHead.Rows[0][0].ToString() + "|");
                        sbs.Append("1|Tianjin|" + dtHead.Rows[0]["LAND1"].ToString() + "|CN|");
                        sbs.Append(dtHead.Rows[0][0].ToString() + "|" + dtHead.Rows[0]["WAYBILL"].ToString() + "|" + supdfile + "|");
                        sbs.AppendLine();
                        string strASNFileName = string.Format(ASN_FILE_NAME, dtDetail.Rows[0]["BSTKD"].ToString(), DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                        string[] strTmp = strASNFileName.Split('\\');
                        string strFileName = strTmp[strTmp.Length - 1];

                        string strSql = string.Empty;
                        string strFolder = string.Empty;
                        string aaa = dtHead.Rows[0]["LAND1"].ToString();
                        strSql = @"SELECT FTP_FOLDER  FROM SHP.R_ASN_FTP_REGION    WHERE  COUNTRY='" + dtHead.Rows[0]["LAND1"].ToString() + "'";
                        using (OracleDB oraDB = new OracleDB())
                        {
                            try
                            {
                                using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                                {
                                    if (odr.Read())
                                    {
                                        // strFolder = odr[0].ToString();
                                        strFolder = "SN_ASIA";

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                OracleDB.Write("[ERROR] Get FTP_FOLDER Error " + ex.Message);
                                return;
                            }
                        }

                        strSql1 = @"update SFC.UPD_DATALOAD_DETAIL_T   set  asn_ftp_time=sysdate where invoice='" + strInvoice + "'";
                        bool strok;
                        try
                        {
                            OracleDB oraDB = new OracleDB();
                            strok = oraDB.ExecuteNonQuery(strSql1);
                            if (strok == false)
                            {
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            OracleDB.Write("[ERROR] Get FTP_FOLDER Error " + ex.Message);
                            return;
                        }

                        if (CreateFile(sbs, strASNFileName))
                        {

                            if (FtpASNFile(strASNFileName, strFileName, strFolder))
                            {
                                File.Move(strASNFileName, ASN_FILE_BAK + "\\" + strFileName);

                            }
                        }

                    }
                    else
                    {
                        OracleDB.Write("DownLoad Invoice Info Error , Invoice : " + strInvoice);
                    }
                }

                catch (Exception ex)
                {
                    OracleDB.Write("DownLoad Invoice Info Error , Invoice : " + strInvoice);
                }
            }
            else
            {

            }





        }
        private string GetInvoiceFromSAPTJ(string strInvoice)
        {

            SAPRFC.SAPFunction sapf;
            sapf = new SAPRFC.SAPFunction();
            sapf = new SAPRFC.SAPFunction("10.134.28.98", "802", "rfctymcm", "mvurn8x7", "00");  //zmf  正式802 環境
            try
            {
                DataSet ds = sapf.GetInvoice(strInvoice);
                if (ds != null)
                {
                    DataTable dtHead = ds.Tables[0];
                    DataTable dtDetail = ds.Tables[1];
                    return "OK";
                }
                else
                {
                    return "No Data";
                }
            }
            catch (Exception ex)
            {
                return "DownLoad Invoice Info Error , Invoice : " + strInvoice;
            }
        }
        #endregion

        #region Get New Method UPD,IMEI,ASN,GFS
        public List<StringBuilder> Get_tra_UPD_NEW(string strPlantx, string strInvoice, string strUpdCode, string strmodulex, string strshipcountryx, string qtyx)
        {
            /////////////////////////////////////////////////
            // Function : Get UPD  Luo Shi Jia 20110804
            // Algorithm :
            //  1. Get factoryCode and Model Type.
            //  2. According to UpdCode(UPD01,UPD02) and shipcountry Choose Correct SQL,Create to Data.
            //  3.Return Data.
            // PS:Plantx(工廠代碼),strInvoice(DN),strmodulex(機種類型),strshipcountryx（出貨國家)，qtyx（出貨數量）
            /////////////////////////////////////////////////


            #region Get FactoryCode
            switch (strPlantx)
            {
                case "WCLA":
                    FactoryCode = "CMCSMCMG2";
                    break;
                case "WCLB":
                    FactoryCode = "CMCSMCME10";
                    break;
                case "WCTA":
                    FactoryCode = "CMCSMCMTY";
                    break;
                case "WSJ2":
                    FactoryCode = "ZFOXTJ-ODM";
                    break;
                default:
                    FactoryCode = "ZFOXTJ-ODM";
                    break;
            }
            #endregion

            #region Get ModelType
            string strCustModelType = string.Empty;

            using (OracleDB oraDB = new OracleDB())
            {
                string strSql = string.Empty;
                strSql = @"select DISTINCT  B.MODEL AS Mode_Type  FROM SAP.CMCS_SFC_PACKING_LINES_ALL a,
                                    SHP.CMCS_SFC_SHIPPING_DATA b where internal_carton is not null and A.INTERNAL_CARTON=B.CARTON_NO 
                                    AND A.INVOICE_NUMBER='" + strInvoice + "'";
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                    {
                        while (odr.Read())
                        {
                            strCustModelType = odr[0].ToString();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetCustModelType " + ex.Message);
                }
            }

            #endregion

            #region Moto-GetSql
            string Sqlstr = string.Empty;
            List<StringBuilder> lsb = new List<StringBuilder>();
            if (strUpdCode.ToUpper() == "UPD01")
            {

                Sqlstr = @"SELECT 'MEID' f1, d.imeinum f2, 'FIHTJ' f3,
                                       TO_CHAR (d.creation_date, 'yyyy-mm-dd HH24:MI:SS') f4, '' f5,
                                       SUBSTR (DECODE (d.customer_num, NULL, d.serial_num, d.customer_num),1,3) f6,c.cust_pno f7,
                                       DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f8, f.market_name f9,
                                       b.customer_item f10, '' f11,
                                       TO_CHAR(DECODE (c.out_date, NULL, c.in_date, c.out_date),'yyyy-mm-dd HH24:MI:SS') f12, '123456' f13,
                                       '123456' f14, b.ship_to_customername f15, b.ship_to_city f16,
                                       k.iso_code f17, '123456' f18, 'MOTOROLA' f19,
                                       TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f20,
                                       c.serial_no f21, '' f22, b.internal_carton f23, b.customer_po f24,
                                       b.so_number f25, '' f26, e.sub_lock f27, '' f28, '' f29,
                                       e.onetime_lock f30, '' f31,e.pesn f32, e.dmeid f33, e.akey1 f34,
                                       '' f35, d.customer_num f36, '' f37, d.btaddress f38, '' f39, '' f40,
                                       i.akey2_type f41, e.akey2 f42, '' f43, '' f44, '' f45, '' f46, '' f47,
                                       g.software_ver f48, '' f49, '' f50, '' f51, '' f52, '' f53, '' f54,
                                       b.invoice_number f55, b.so_number f56, '' f57, k.iso_code f58,
                                       '' f59, '' f60, b.invoice_number f61, g.porder f62, '' f63,
                                       k.iso_code f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,
                                       '' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77, '' f78, '' f79,
                                       '' f80, '' f81, '' f82
                                  FROM sfc.cmcs_sfc_packing_lines_all b,
                                       shp.cmcs_sfc_shipping_data c,
                                       shp.cmcs_sfc_imeinum d,
                                       sfc.CDMA_UPD_PARSER_MEID_V e,
                                       shp.ros_tch_pn f,
                                       shp.cmcs_sfc_porder g,
                                       sfc.maky_information h,
                                       sfc.maif_information i,
                                       sap.country_iso_link k
                                 WHERE b.internal_carton = c.carton_no
                                   AND (c.imei = d.imeinum)
                                   AND (b.item_number = f.ppart)
                                        AND d.porder = g.porder
                                   AND d.imeinum = e.MEID_ID
                                   AND d.imeinum = h.meid_id
                                   AND h.master_id = i.master_id
                                   AND upper(b.ship_to_country)=K.country_code
                                   AND b.invoice_number IN ('" + strInvoice + "')";
            }
            else if (strUpdCode.ToUpper() == "UPD02")
            {

                if (strCustModelType == "QAZ")
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2,'" + FactoryCode + "' f3,"
                                               + "TO_CHAR(e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                               + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                               + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                               + "f.market_name f8, b.customer_item f9, '' f10,"
                                               + "DECODE (c.out_date,"
                                                      + " NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                                       + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                               + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                               + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                               + "h.sold_tocustomer_name f18,"
                                               + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                               + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                               + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                               + "c.work_order f25, e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                               + "e.privilegepwd f30, '' f31, '' f32, '' f33, '' f34,"
                                               + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                               + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                               + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                               + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                               + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                               + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                               + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                               + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                               + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                          + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                               + "shp.cmcs_sfc_packing_lines_all b,"
                                               + "shp.cmcs_sfc_shipping_data c,"
                                               + "shp.cmcs_sfc_imeinum d,"
                                               + "" + strmodulex + ".e2pconfig e,"
                                               + "shp.ros_tch_pn f,"
                                               + "shp.cmcs_sfc_porder g,"
                                               + "shp.upd_order_information h,"
                                               + "shp.cmcs_sfc_carton j,"
                                               + "sap.country_iso_link k "
                                         + "WHERE a.order_carton_no = b.internal_carton "
                                           + "AND b.internal_carton = c.carton_no "
                                           + "AND b.customer_po = h.po_number "
                                           + "AND b.item_number = f.ppart "
                                           + "AND d.porder = g.porder "
                                           + "AND c.imei = d.imeinum "
                                           + "AND ( c.imei = e.imei  "
                                                + "AND e.status = 'PASS' "
                                                + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + strmodulex + ".e2pconfig h "
                                                + "WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                           + "AND j.carton_no = b.internal_carton "
                                           + "AND UPPER (b.ship_to_country) = k.country_code "
                                           + "AND b.invoice_number = a.invoice_no "
                                           + "AND a.invoice_no ='" + strInvoice + "'";
                }
                else if (strCustModelType.Substring(0, 3) == "DMQ")
                {

                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, 'ZFOXTJ-ODM' f3,"
                                            + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                            + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                            + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                            + "f.market_name f8, b.customer_item f9, '' f10,"
                                            + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                                    + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                           + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                            + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ "
                                            + "h.sold_tocustomer_name f18,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                            + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                            + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                            + "c.work_order f25, '12345678' f26, '' f27, '' f28, '12345678' f29,"
                                            + "'12345678' f30, '' f31, '' f32, '' f33, '' f34,"
                                            + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                            + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                            + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                            + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                            + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59, '' f60, '' f61,"
                                            + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                            + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                            + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                       + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                            + "shp.cmcs_sfc_packing_lines_all b,"
                                            + "shp.cmcs_sfc_shipping_data c,"
                                            + "shp.cmcs_sfc_imeinum d,"
                                            + "" + strmodulex + ".e2pconfig e,"
                                            + "shp.ros_tch_pn f,"
                                            + "shp.cmcs_sfc_porder g,"
                                            + "shp.upd_order_information h,"
                                            + "shp.cmcs_sfc_carton j,"
                                            + "sap.country_iso_link k "
                                      + "WHERE a.order_carton_no = b.internal_carton "
                                        + "AND b.internal_carton = c.carton_no "
                                        + "AND b.customer_po = h.po_number "
                                        + "AND b.item_number = f.ppart "
                                        + "AND d.porder = g.porder "
                                        + "AND c.imei = d.imeinum "
                                        + "AND (c.imei = e.imei "
                                             + "AND e.status = 'PASS' "
                                             + "AND e.e2pdate = "
                                                         + "(SELECT MAX (h.e2pdate) FROM " + strmodulex + ".e2pconfig h WHERE h.imei = c.imei "
                                             + "AND h.status = 'PASS')) "
                                          + "AND j.carton_no = b.internal_carton "
                                          + "AND UPPER (b.ship_to_country) = k.country_code "
                                          + "AND b.invoice_number = a.invoice_no "
                                          + "AND a.invoice_no ='" + strInvoice + "'";

                }
                else if (strCustModelType == "QAZ-DS")
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                            + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                            + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                            + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                            + "f.market_name f8, b.customer_item f9, '' f10,"
                                            + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                            + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                            + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17," /*H.CUSTOMER_NAME*/
                                            + "h.sold_tocustomer_name f18,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                            + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                            + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                            + "c.work_order f25, e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                            + "e.privilegepwd f30, '' f31, '' f32, '' f33, '' f34,"
                                            + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                            + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,"
                                            + "a.invoice_no f41,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                            + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                            + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                            + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,"
                                            + "'' f60, '' f61,"
                                            + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                            + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                            + "d.imeinum2 f71, 'IMEI' f72, '' f73, '' f74, '' f75, '' f76,"
                                            + "'' f77, '' f78"
                                    + " FROM shp.cmcs_sfc_ship_carton_map a,"
                                            + "shp.cmcs_sfc_packing_lines_all b,"
                                            + "shp.cmcs_sfc_shipping_data c,"
                                            + "shp.cmcs_sfc_imeinum d,"
                                            + "" + strmodulex + ".e2pconfig e,"
                                            + "shp.ros_tch_pn f,"
                                            + "shp.cmcs_sfc_porder g,"
                                            + "shp.upd_order_information h,"
                                            + "shp.cmcs_sfc_carton j,"
                                            + "sap.country_iso_link k"
                                   + " WHERE a.order_carton_no = b.internal_carton"
                                        + " AND b.internal_carton = c.carton_no"
                                        + " AND b.customer_po = h.po_number"
                                        + " AND b.item_number = f.ppart"
                                        + " AND d.porder = g.porder"
                                        + " AND c.imei = d.imeinum"
                                        + " AND (    c.imei = e.imei"
                                             + " AND e.status = 'PASS'"
                                             + " AND e.e2pdate = (SELECT MAX (h.e2pdate)"
                                                            + "  FROM " + strmodulex + ".e2pconfig h"
                                                            + "  WHERE h.imei = c.imei AND h.status = 'PASS')"
                                            + " ) "
                                       + " AND j.carton_no = b.internal_carton"
                                       + " AND UPPER (b.ship_to_country) = k.country_code"
                                       + " AND b.invoice_number = a.invoice_no"
                                       + " AND a.invoice_no ='" + strInvoice + "'";

                }
                else if (strCustModelType.Substring(0, 3) == "A3G" || strCustModelType.Substring(0, 3) == "A2G")
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                            + "TO_CHAR (e.create_date, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                            + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                            + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                            + "f.market_name f8, b.customer_item f9, '' f10,"
                                            + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                            + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                            + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                            + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ h.sold_tocustomer_name f18,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                            + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                            + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                            + "c.work_order f25, SUBSTR(e.th7, 16, 8) f26, '' f27, '' f28, SUBSTR (e.th7, 48, 8) f29,"
                                            + "'12345678' f30,'' f31, '' f32, '' f33, '' f34,"
                                            + "DECODE (d.customer_num, NULL, 'M14' || SUBSTR (d.imeinum, 8, 7), d.customer_num) f35,"
                                            + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                            + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,'' f43, '' f44, '' f45, '' f46,"
                                            + "'' f47, '' f48, '' f49,"
                                            + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                            + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                            + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                            + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                            + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                       + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                            + "shp.cmcs_sfc_packing_lines_all b,"
                                            + "shp.cmcs_sfc_shipping_data c,"
                                            + "shp.cmcs_sfc_imeinum d,"
                                            + "(SELECT   MAX (ID || th7) th7, th26,MAX (create_date) create_date FROM testinfo.testinfo_head"
                                            + " WHERE status = 'PASS' AND LENGTH (th7) = 80 AND station_name = 'E2P' GROUP BY th26) e, "
                                            + "shp.ros_tch_pn f,"
                                            + "shp.cmcs_sfc_porder g,"
                                            + "shp.upd_order_information h,"
                                            + "shp.cmcs_sfc_carton j,"
                                            + "sap.country_iso_link k "
                                      + "WHERE a.order_carton_no = b.internal_carton "
                                        + "AND a.invoice_no = b.invoice_number "
                                        + "AND b.customer_po = h.po_number "
                                        + "AND b.internal_carton = c.carton_no "
                                        + "AND b.internal_carton = j.carton_no "
                                        + "AND b.item_number = f.ppart "
                                        + "AND c.imei = d.imeinum "
                                        + "AND c.imei = e.th26 "
                                        + "AND d.porder = g.porder "
                                        + "AND UPPER (b.ship_to_country) = k.country_code "
                                        + "AND a.invoice_no ='" + strInvoice + "' "
                                        + "AND b.invoice_number ='" + strInvoice + "'";
                }
                else
                {
                    Sqlstr = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                        + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                        + "SUBSTR (d.customer_num, 1, 3) f5, c.cust_pno f6,"
                                        + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                        + "f.market_name f8, b.customer_item f9, '' f10,"
                                        + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                        + "h.customer_id f12, h.ship_id f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                        + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                        + "h.sold_tocustomer_name f18,"
                                        + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                        + "'M8' || SUBSTR (c.serial_no, 7, 8) f20, d.ta_number f21,"
                                        + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                        + "c.work_order f25, '12345678' f26, '' f27, '' f28, '12345678' f29,"
                                        + "'12345678' f30, '' f31, '' f32, '' f33, '' f34,"
                                        + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                        + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                        + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                        + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                        + " '' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                        + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                        + "DECODE (h.bill_to_id, NULL, '', h.bill_to_id) f62, '' f63,"
                                        + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                        + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78 "
                                   + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                       + "shp.cmcs_sfc_packing_lines_all b,"
                                       + "shp.cmcs_sfc_shipping_data c,"
                                       + "shp.cmcs_sfc_imeinum d,"
                                       + "" + strmodulex + ".e2pconfig e,"
                                       + "shp.ros_tch_pn f,"
                                       + "shp.cmcs_sfc_porder g,"
                                       + "shp.upd_order_information h,"
                                       + "shp.cmcs_sfc_carton j,"
                                       + "sap.country_iso_link k "
                                   + "WHERE a.order_carton_no = b.internal_carton "
                                   + "AND b.customer_po = h.po_number "
                                   + "AND b.invoice_number = a.invoice_no "
                                   + "AND b.internal_carton = c.carton_no "
                                   + "AND c.imei = d.imeinum "
                                   + "AND b.item_number = f.ppart "
                                   + "AND d.porder = g.porder "
                                   + "AND (c.imei = e.imei "
                                         + "AND e.status = 'PASS'"
                                         + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + strmodulex + ".e2pconfig h WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                   + "AND j.carton_no = b.internal_carton "
                                   + "AND UPPER (b.ship_to_country) = k.country_code "
                                   + "AND a.invoice_no ='" + strInvoice + "'";

                }
            }



            #endregion

            #region Create Data
            using (OracleDB oraDB = new OracleDB())
            {
                try
                {

                    using (DataSet odr = oraDB.GetDataSet(Sqlstr))
                    {

                        StringBuilder sbupd = new StringBuilder();
                        int iColNum = odr.Tables[0].Columns.Count;
                        string CountQty = odr.Tables[0].Rows.Count.ToString();
                        StringBuilder sbs = new StringBuilder();
                        if (CountQty == qtyx)
                        {



                            for (int n = 0; n < odr.Tables[0].Rows.Count; n++)
                            {
                                //#region 特定欄位判定

                                //for (int rownum = 0; rownum < 40; rownum++)
                                //{
                                //    Boolean FunChk = CheckFieldValue(strInvoice, strmodulex, odr.Tables[0].Rows[n][rownum].ToString(), odr.Tables[0].Rows[n][1].ToString(), rownum + 1);
                                //    if (FunChk == false)
                                //    {
                                //        return lsb;
                                //    }
                                //}
                                //#endregion
                                #region 循環|放入sbs
                                for (int i = 0; i < iColNum; i++)
                                {
                                    sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                                    sbs.Append("|");
                                }
                                sbs.AppendLine();

                                if ((sbupd.Length + sbs.Length) <= SINGLE_UPD_FILE_MAX_LENGTH)
                                {
                                    sbupd.Append(sbs);
                                    sbs = new StringBuilder();
                                }
                                else
                                {
                                    lsb.Add(sbupd);
                                    sbupd = new StringBuilder();
                                    sbupd.Append(sbs);
                                    sbs = new StringBuilder();
                                }
                                #endregion
                            }

                            if (sbupd.Length > 0)
                            {
                                lsb.Add(sbupd);
                            }

                        }
                        else
                        {

                            AddMail("UPD FILE Invoice : " + strInvoice, "", "", "Number does not match,please make sure download DN or Print 3s,4s!", "");


                            return lsb;
                        }

                    }


                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] GetInvoiceInfo " + ex.Message);
                }
            }
            #endregion

            return lsb;

        }
        public string Get_tra_IMEI_NEW(string strInvoice, string strIMEICode, string strModel, string strcountry)
        {

            /////////////////////////////////////////////////
            // Function : Get IMEI  Luo Shi Jia 20110804
            // Algorithm :
            //  1. According to IMEICode(IMEI03) and shipcountry Choose Correct SQL,Create to Data.
            //  2. IF Have Data,so Create Data to Excel,Saved to the specified directory
            //  3. Return a Excel File Name.
            /////////////////////////////////////////////////
            string strIMEIFile = "-";
            if (strIMEICode.ToUpper() == "IMEI03")
            {

                string strCustomerName = string.Empty;
                string strSql = string.Empty;
                #region GetSql-Normal
                strSql = @" SELECT e.SO_NO SALES_PO,g.ppart FIH_PN,G.HW_VER HW,e.SO_NO SO,G.SW_VER SW,
                         LPAD(E.INVOICE,10,'0') INVOICE_NO,g.phone_model XCVR_NO,
                         (SELECT COUNT(b.imei) FROM CMCS_SFC_PACKING_LINES_ALL a,shp.CMCS_SFC_SHIPPING_DATA b 
                         WHERE a.INTERNAL_CARTON=B.CARTON_NO and  a.INVOICE_Number=" + strInvoice + @"  ) QTY ,
                         E.ADDRESS SHIP_TO,G.SA_NO moto_no, 'TIANJIN' FACILITY,
                         TO_CHAR(SYSDATE,'YYYY/MM/DD') CREATEDATE,CUST_PO_NUMBER MOTO_PO, 
                         C.IMEInum IMEI,c.customer_num MSN,'12345678' NP_PW,b.sa_no MODEL,to_char(a.ship_date,'yyyy/mm/ dd') SHIP_DATE,
                         d.caseid  CARTON_ID,lpad(a.INVOICE_Number,10,'0')||'+1095759' PALLET_ID,C.IMEINUM2 IMEI2
                        FROM CMCS_SFC_PACKING_LINES_ALL a,
                             shp.CMCS_SFC_SHIPPING_DATA b,
                             shp.CMCS_SFC_IMEINUM c,
                             shp.cmcs_sfc_carton d,
                             SAP.SAP_INVOICE_INFO e ,
                             SAP.SAP_ORDER_INFO F,
                             ROS_TCH_PN g
                        WHERE  a.INVOICE_Number=" + strInvoice + @"
                               and a.INVOICE_Number=E.INVOICE
                               and a.INTERNAL_CARTON=B.CARTON_NO
                               and B.IMEI=C.IMEINUM 
                               and a.INTERNAL_CARTON=D.CARTON_NO 
                               AND E.SO_NO=F.ORDER_NUMBER
                               and D.PPART=G.PPART
                        ORDER BY CARTON_ID";
                #endregion
                #region GetSql-A3G/A2G-shipto LA
                if ((strModel == "A3G" || strModel == "A2G") && strcountry == "LA")
                {
                    strSql = @"SELECT   e.so_no sales_po, g.ppart fih_pn, g.hw_ver hw, e.so_no so,
                                 g.sw_ver sw, LPAD (e.invoice, 10, '0') invoice_no,
                                 g.phone_model xcvr_no,
                                 (SELECT COUNT (b.imei)
                                    FROM cmcs_sfc_packing_lines_all a,
                                         shp.cmcs_sfc_shipping_data b
                                   WHERE a.internal_carton = b.carton_no
                                     AND a.invoice_number =" + strInvoice + @") qty,
                                 e.address ship_to, g.sa_no moto_no, 'TIANJIN' facility,
                                 TO_CHAR (SYSDATE, 'YYYY/MM/DD') createdate, cust_po_number moto_po,
                                 c.imeinum imei, c.customer_num msn, '12345678' np_pw, b.sa_no model,
                                 TO_CHAR (a.ship_date, 'yyyy/mm/ dd') ship_date, d.caseid carton_id,
                                 LPAD (a.invoice_number, 10, '0') || '+1095759' pallet_id,
                                 DECODE(c.imeinum2,'','NA',c.imeinum2) imei2, SUBSTR (h.th7, 16, 8) p_uc,
                                 SUBSTR (h.th7, 48, 8) s_uc, 'NA' s_pc
                            FROM cmcs_sfc_packing_lines_all a,
                                 shp.cmcs_sfc_shipping_data b,
                                 shp.cmcs_sfc_imeinum c,
                                 shp.cmcs_sfc_carton d,
                                 sap.sap_invoice_info e,
                                 sap.sap_order_info f,
                                 ros_tch_pn g,
                                 (SELECT   MAX (ID || th7) th7, th26, MAX (create_date) create_date
                                      FROM testinfo.testinfo_head
                                     WHERE status = 'PASS' AND LENGTH (th7) = 80
                                           AND station_name = 'E2P'
                                  GROUP BY th26) h
                           WHERE a.invoice_number = " + strInvoice + @"
                             AND a.invoice_number = e.invoice
                             AND a.internal_carton = b.carton_no
                             AND b.imei = c.imeinum
                             AND b.imei = h.th26
                             AND a.internal_carton = d.carton_no
                             AND e.so_no = f.order_number
                             AND d.ppart = g.ppart
                        ORDER BY carton_id";
                }
                #endregion
                #region GetSql-DMP-shipto LA
                if (strModel == "DMQ" && strcountry == "LA")
                {
                    strSql = @"SELECT   e.so_no sales_po, g.ppart fih_pn, g.hw_ver hw, e.so_no so,
                             g.sw_ver sw, LPAD (e.invoice, 10, '0') invoice_no,
                             g.phone_model xcvr_no,
                             (SELECT COUNT (b.imei)
                                FROM cmcs_sfc_packing_lines_all a,
                                     shp.cmcs_sfc_shipping_data b
                               WHERE a.internal_carton = b.carton_no
                                 AND a.invoice_number =" + strInvoice + @") qty,
                             e.address ship_to, g.sa_no moto_no, 'TIANJIN' facility,
                             TO_CHAR (SYSDATE, 'YYYY/MM/DD') createdate, cust_po_number moto_po,
                             c.imeinum imei, c.customer_num msn, '12345678' np_pw, b.sa_no model,
                             TO_CHAR (a.ship_date, 'yyyy/mm/ dd') ship_date, d.caseid carton_id,
                             LPAD (a.invoice_number, 10, '0') || '+1095759' pallet_id,
                             DECODE (c.imeinum2, '', 'NA', c.imeinum2) imei2, 'NA' p_uc,
                             'NA' s_uc, 'NA' s_pc
                        FROM cmcs_sfc_packing_lines_all a,
                             shp.cmcs_sfc_shipping_data b,
                             shp.cmcs_sfc_imeinum c,
                             shp.cmcs_sfc_carton d,
                             sap.sap_invoice_info e,
                             sap.sap_order_info f,
                             ros_tch_pn g,
                             " + strModel + @".e2pconfig h
                       WHERE a.invoice_number =" + strInvoice + @"
                         AND a.invoice_number = e.invoice
                         AND a.internal_carton = b.carton_no
                         AND b.imei = c.imeinum
                         AND (    c.imeinum = h.imei
                              AND h.status = 'PASS'
                              AND h.e2pdate = (SELECT MAX (h.e2pdate)
                                                 FROM " + strModel + @".e2pconfig h
                                                WHERE h.imei = c.imeinum AND h.status = 'PASS')
                             )
                         AND a.internal_carton = d.carton_no
                         AND e.so_no = f.order_number
                         AND d.ppart = g.ppart
                    ORDER BY carton_id";
                }
                #endregion
                DateTime Process_BeforeTime = DateTime.Now;
                Excel3.Application app = new Excel3.Application();

                using (OracleDB oraDB = new OracleDB())
                {

                    try
                    {
                        Excel3.Workbook wb = app.Workbooks.Add(true);
                        Excel3.Worksheet st = (Excel3.Worksheet)app.Sheets[1];
                        app.DisplayAlerts = false;
                        app.AlertBeforeOverwriting = false;

                        DataSet odr = oraDB.GetDataSet(strSql);
                        DataTable dt = odr.Tables[0];
                        //throw new Exception("test");
                        bool bInit = false;
                        int n = 8;
                        #region QAZ
                        if (strModel == "QAZ")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!bInit)
                                {

                                    if (strCustomerName.Equals("CONCORDE S.P.A."))
                                    {
                                        st.Cells[1, 1] = "Model";
                                        st.Cells[1, 2] = "IMEI";
                                        st.Cells[1, 3] = "CARTON ID";
                                        st.Cells[1, 4] = "PALLET ID";
                                    }
                                    else
                                    {
                                        st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                        st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                        st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                        st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                        st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                        st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                        st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                        st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                        st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                        st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                        st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                        st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                        st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                        st.Cells[7, 1] = "IMEI NO.";

                                        //st.Cells[7, 2] = "MEID NO.";
                                        st.Cells[7, 2] = "MSN";
                                        st.Cells[7, 3] = "NP P/W";
                                        st.Cells[7, 4] = "Model";
                                        st.Cells[7, 5] = "Shipment Date";
                                        st.Cells[7, 6] = "CARTON ID";
                                        st.Cells[7, 7] = "PALLET ID";
                                    }
                                    bInit = true;
                                }
                                break;
                            }

                            int x = 0;
                            if (strCustomerName.Equals("CONCORDE S.P.A."))
                            {

                                object[,] objData = new Object[dt.Rows.Count, 7];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[16].ToString();
                                    objData[x, 1] = dr[13].ToString();
                                    objData[x, 2] = dr[18].ToString();
                                    objData[x, 3] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;
                            }
                            else
                            {
                                object[,] objData = new Object[dt.Rows.Count, 8];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[13].ToString();
                                    objData[x, 1] = dr[14].ToString();
                                    objData[x, 2] = dr[15].ToString();
                                    objData[x, 3] = dr[16].ToString();
                                    objData[x, 4] = dr[17].ToString();
                                    objData[x, 5] = dr[18].ToString();
                                    objData[x, 6] = dr[19].ToString();

                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;

                            }

                        }
                        #endregion
                        #region QAZ-DS
                        else if (strModel == "QAZ-DS")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!bInit)
                                {

                                    if (strCustomerName.Equals("CONCORDE S.P.A."))
                                    {
                                        st.Cells[1, 1] = "Model";
                                        st.Cells[1, 2] = "IMEI";
                                        st.Cells[1, 3] = "CARTON ID";
                                        st.Cells[1, 4] = "PALLET ID";
                                    }
                                    else
                                    {
                                        st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                        st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                        st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                        st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                        st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                        st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                        st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                        st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                        st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                        st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                        st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                        st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                        st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                        st.Cells[7, 1] = "Pri-IMEI NO.";

                                        //st.Cells[7, 2] = "MEID NO.";
                                        st.Cells[7, 2] = "Sec-IMEI NO.";
                                        st.Cells[7, 3] = "MSN";
                                        st.Cells[7, 4] = "NP P/W";
                                        st.Cells[7, 5] = "Model";
                                        st.Cells[7, 6] = "Shipment Date";
                                        st.Cells[7, 7] = "CARTON ID";
                                        st.Cells[7, 8] = "PALLET ID";
                                    }
                                    bInit = true;
                                }
                                break;
                            }

                            int x = 0;
                            if (strCustomerName.Equals("CONCORDE S.P.A."))
                            {

                                object[,] objData = new Object[dt.Rows.Count, 7];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[16].ToString();
                                    objData[x, 1] = dr[13].ToString();
                                    objData[x, 2] = dr[18].ToString();
                                    objData[x, 3] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;
                            }
                            else
                            {
                                object[,] objData = new Object[dt.Rows.Count, 8];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[13].ToString();
                                    objData[x, 1] = dr[20].ToString();
                                    objData[x, 2] = dr[14].ToString();
                                    objData[x, 3] = dr[15].ToString();
                                    objData[x, 4] = dr[16].ToString();
                                    objData[x, 5] = dr[17].ToString();
                                    objData[x, 6] = dr[18].ToString();
                                    objData[x, 7] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;

                            }
                        }
                        #endregion
                        #region A3G/A2G ship to LA
                        else if ((strModel == "A3G" || strModel == "A2G") && strcountry == "LA")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!bInit)
                                {

                                    if (strCustomerName.Equals("CONCORDE S.P.A."))
                                    {
                                        st.Cells[1, 1] = "Model";
                                        st.Cells[1, 2] = "IMEI";
                                        st.Cells[1, 3] = "CARTON ID";
                                        st.Cells[1, 4] = "PALLET ID";
                                    }
                                    else
                                    {
                                        st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                        st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                        st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                        st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                        st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                        st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                        st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                        st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                        st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                        st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                        st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                        st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                        st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                        st.Cells[7, 1] = "IMEI NO.";
                                        st.Cells[7, 2] = "IMEI_NO_2";
                                        st.Cells[7, 3] = "MSN";
                                        st.Cells[7, 4] = "P_UC";
                                        st.Cells[7, 5] = "S_UC";
                                        st.Cells[7, 6] = "S_PC";
                                        st.Cells[7, 7] = "Model";
                                        st.Cells[7, 8] = "Shipment Date";
                                        st.Cells[7, 9] = "CARTON ID";
                                        st.Cells[7, 10] = "PALLET ID";
                                    }
                                    bInit = true;
                                }
                                break;
                            }

                            int x = 0;
                            if (strCustomerName.Equals("CONCORDE S.P.A."))
                            {

                                object[,] objData = new Object[dt.Rows.Count, 7];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[16].ToString();
                                    objData[x, 1] = dr[13].ToString();
                                    objData[x, 2] = dr[18].ToString();
                                    objData[x, 3] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;
                            }
                            else
                            {
                                object[,] objData = new Object[dt.Rows.Count, 10];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[13].ToString();
                                    objData[x, 1] = dr[20].ToString();
                                    objData[x, 2] = dr[14].ToString();
                                    objData[x, 3] = dr[21].ToString();
                                    objData[x, 4] = dr[22].ToString();
                                    objData[x, 5] = dr[23].ToString();
                                    objData[x, 6] = dr[16].ToString();
                                    objData[x, 7] = dr[17].ToString();
                                    objData[x, 8] = dr[18].ToString();
                                    objData[x, 9] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;

                            }

                        }
                        #endregion
                        #region DMQ ship to LA
                        else if (strModel == "DMQ" && strcountry == "LA")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!bInit)
                                {

                                    if (strCustomerName.Equals("CONCORDE S.P.A."))
                                    {
                                        st.Cells[1, 1] = "Model";
                                        st.Cells[1, 2] = "IMEI";
                                        st.Cells[1, 3] = "CARTON ID";
                                        st.Cells[1, 4] = "PALLET ID";
                                    }
                                    else
                                    {
                                        st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                        st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                        st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                        st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                        st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                        st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                        st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                        st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                        st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                        st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                        st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                        st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                        st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                        st.Cells[7, 1] = "IMEI NO.";
                                        st.Cells[7, 2] = "IMEI_NO_2";
                                        st.Cells[7, 3] = "MSN";
                                        st.Cells[7, 4] = "P_UC";
                                        st.Cells[7, 5] = "S_UC";
                                        st.Cells[7, 6] = "S_PC";
                                        st.Cells[7, 7] = "Model";
                                        st.Cells[7, 8] = "Shipment Date";
                                        st.Cells[7, 9] = "CARTON ID";
                                        st.Cells[7, 10] = "PALLET ID";
                                    }
                                    bInit = true;
                                }
                                break;
                            }

                            int x = 0;
                            if (strCustomerName.Equals("CONCORDE S.P.A."))
                            {

                                object[,] objData = new Object[dt.Rows.Count, 7];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[16].ToString();
                                    objData[x, 1] = dr[13].ToString();
                                    objData[x, 2] = dr[18].ToString();
                                    objData[x, 3] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;
                            }
                            else
                            {
                                object[,] objData = new Object[dt.Rows.Count, 10];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[13].ToString();
                                    objData[x, 1] = dr[20].ToString();
                                    objData[x, 2] = dr[14].ToString();
                                    objData[x, 3] = dr[21].ToString();
                                    objData[x, 4] = dr[22].ToString();
                                    objData[x, 5] = dr[23].ToString();
                                    objData[x, 6] = dr[16].ToString();
                                    objData[x, 7] = dr[17].ToString();
                                    objData[x, 8] = dr[18].ToString();
                                    objData[x, 9] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;

                            }
                        }
                        #endregion
                        #region else
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!bInit)
                                {

                                    if (strCustomerName.Equals("CONCORDE S.P.A."))
                                    {
                                        st.Cells[1, 1] = "Model";
                                        st.Cells[1, 2] = "IMEI";
                                        st.Cells[1, 3] = "CARTON ID";
                                        st.Cells[1, 4] = "PALLET ID";
                                    }
                                    else
                                    {
                                        st.Cells[3, 1] = "SALES PO NO:" + dr[0].ToString();
                                        st.Cells[3, 4] = "Invoice NO:" + dr[5].ToString();
                                        st.Cells[3, 8] = "MODEL NO:" + dr[9].ToString();
                                        st.Cells[4, 1] = "FIH P/N:" + dr[1].ToString();
                                        st.Cells[4, 4] = "XCVR NO:" + dr[6].ToString();
                                        st.Cells[4, 8] = "Facility : " + dr[10].ToString();
                                        st.Cells[5, 1] = "H/W:" + dr[2].ToString();
                                        st.Cells[5, 3] = "S/W:" + dr[4].ToString();
                                        st.Cells[5, 4] = "Q'ty:" + dr[7].ToString();
                                        st.Cells[5, 8] = "Date:" + dr[11].ToString();
                                        st.Cells[6, 8] = "MOTO_PO_NO:" + dr[12].ToString();
                                        st.Cells[6, 1] = "SO:" + dr[3].ToString();
                                        st.Cells[6, 3] = "Ship to:" + dr[8].ToString();
                                        st.Cells[7, 1] = "IMEI NO.";

                                        //st.Cells[7, 2] = "MEID NO.";
                                        st.Cells[7, 2] = "MSN";
                                        st.Cells[7, 3] = "NP P/W";
                                        st.Cells[7, 4] = "Model";
                                        st.Cells[7, 5] = "Shipment Date";
                                        st.Cells[7, 6] = "CARTON ID";
                                        st.Cells[7, 7] = "PALLET ID";
                                    }
                                    bInit = true;
                                }
                                break;
                            }

                            int x = 0;
                            if (strCustomerName.Equals("CONCORDE S.P.A."))
                            {

                                object[,] objData = new Object[dt.Rows.Count, 7];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[16].ToString();
                                    objData[x, 1] = dr[13].ToString();
                                    objData[x, 2] = dr[18].ToString();
                                    objData[x, 3] = dr[19].ToString();
                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 7]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;
                            }
                            else
                            {
                                object[,] objData = new Object[dt.Rows.Count, 8];
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objData[x, 0] = dr[13].ToString();
                                    objData[x, 1] = dr[14].ToString();
                                    objData[x, 2] = dr[15].ToString();
                                    objData[x, 3] = dr[16].ToString();
                                    objData[x, 4] = dr[17].ToString();
                                    objData[x, 5] = dr[18].ToString();
                                    objData[x, 6] = dr[19].ToString();

                                    x++;
                                }
                                Excel3.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                                range.NumberFormatLocal = "@";
                                range.Value2 = objData;

                            }


                        }
                        #endregion
                        strIMEIFile = string.Format(IMEI_FILE_NAME, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                        wb.SaveAs(strIMEIFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel3.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //Excel3.XlFileFormat.xlExcel8

                        app.ActiveWorkbook.Close(true, strIMEIFile, Type.Missing);
                        app.Workbooks.Close();
                        app.Quit();

                        DateTime Process_AfterTime = DateTime.Now;
                        KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                        GC.Collect();

                    }
                    catch (Exception ex)
                    {
                        OracleDB.Write("[ERROR] GetInvoiceInfo " + ex.Message);
                        app.Quit();
                        DateTime Process_AfterTime = DateTime.Now;
                        KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                        GC.Collect();
                    }
                }


            }
            else
            {
                strIMEIFile = "-";
            }
            return strIMEIFile;
        }
        public List<StringBuilder> Get_tra_GFS_NEW(string strInvoice, string strGFScode)
        {
            /////////////////////////////////////////////////
            // Function : Get GFS  Luo Shi Jia 20110804
            // Algorithm :
            //  1. According to GFSCode(GFS06) Choose Correct SQL,Create to Data.
            //  3. Return Data.
            /////////////////////////////////////////////////
            List<StringBuilder> lsb = new List<StringBuilder>();
            string strSql = string.Empty;
            if (strGFScode.ToUpper() == "GFS06")
            {
                strSql = @"SELECT 'UN' F1,'CM' F2,rownum F3,C.CUSTOMER_NUM F4,C.SERIAL_NUM F5,
                             C.BTADDRESS F6,C.imeinum F7,B.DMEID F8,B.AKEY1 F9,
                             B.AKEY2 F10,B.SUB_LOCK F11,B.ONETIME_LOCK F12,B.PESN F13,
                             LPAD(TO_NUMBER(SUBSTR(B.PESN,1,2),'XXX'),3,'0')||LPAD(TO_NUMBER(SUBSTR(B.PESN,3,LENGTH(B.PESN)),'XXXXXXXX'),8,'0') F14,
                             '' F15, ''  F16,'' F17, '' F18,'' F19,
                             '' F20,F.software_ver F21,'' F22,D.MARKET_NAME F23,D.PHONE_MODEL F24,
                             A.INTERNAL_CARTON F25,LPAD('FOUR_S',20,'0') F26,TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') F27,
                             TO_CHAR(A.LAST_UPDATE_DATE,'YYYYMMDDHH24MISS') F28
                             FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SFC.CDMA_UPD_PARSER_MEID_V B,SHP.CMCS_SFC_IMEINUM C,SHP.ROS_TCH_PN D,
                             SHP.CMCS_SFC_SHIPPING_DATA E,shp.cmcs_sfc_porder F WHERE A.INTERNAL_CARTON=E.CARTON_NO AND E.IMEI=C.IMEINUM AND
                             C.imeinum=B.MEID_ID AND C.PPART=D.PPART AND C.porder = F.porder
                             AND A.INVOICE_NUMBER IN ('" + strInvoice + "')";

                using (OracleDB oraDB = new OracleDB())
                {
                    try
                    {
                        using (DataSet odr = oraDB.GetDataSet(strSql))
                        {
                            if (odr.Tables[0].Rows.Count < 1)
                                return lsb;
                            StringBuilder sbupd = new StringBuilder();
                            int iColNum = odr.Tables[0].Columns.Count;
                            StringBuilder sbs = new StringBuilder();
                            for (int n = 0; n < odr.Tables[0].Rows.Count; n++)
                            {
                                for (int i = 0; i < iColNum; i++)
                                {
                                    sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                                    sbs.Append("|");
                                }
                                sbs.AppendLine();

                                if ((sbupd.Length + sbs.Length) <= SINGLE_UPD_FILE_MAX_LENGTH)
                                {
                                    sbupd.Append(sbs);
                                    sbs = new StringBuilder();
                                }
                                else
                                {
                                    lsb.Add(sbupd);
                                    sbupd = new StringBuilder();
                                    sbupd.Append(sbs);
                                    sbs = new StringBuilder();
                                }
                            }

                            if (sbupd.Length > 0)
                            {
                                lsb.Add(sbupd);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        OracleDB.Write("[ERROR] GetInvoiceInfo " + ex.Message);
                    }
                }
            }
            else
            {
                lsb = null;
            }


            return lsb;
        }
        public void Get_tra_ASN_NEW(string strInvoice, string strDate, string supdfile)
        {
            //GetInvoiceFromSAPTJ(strInvoice);

            SAPRFC.SAPFunction sapf;
            sapf = new SAPRFC.SAPFunction();
            sapf = new SAPRFC.SAPFunction("10.134.28.98", "802", "rfctymcm", "mvurn8x7", "00");  //zmf  正式802 環境


            string strSqlqty = string.Empty;
            string strshipqty = string.Empty;
            string strSql1 = string.Empty;
            strSqlqty = @"SELECT ship_qty  FROM SFC.UPD_DATALOAD_DETAIL_T   WHERE  invoice= '" + strInvoice + "'";// CREATE_TIME>SYSDATE-1 AND
            using (OracleDB oraDB = new OracleDB())
            {
                try
                {
                    using (OracleDataReader odr = oraDB.GetDataReader(strSqlqty))
                    {
                        while (odr.Read())
                        {
                            strshipqty = odr[0].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] SHP.UPD_DATALOAD_DETAIL_T shipqty is error " + ex.Message);

                }
            }

            if (strshipqty.Length > 0)
            {
                try
                {
                    DataSet ds = sapf.GetInvoice(strInvoice);
                    if (ds != null)
                    {
                        DataTable dtHead = ds.Tables[0];
                        DataTable dtDetail = ds.Tables[1];
                        StringBuilder sbs = new StringBuilder();
                        sbs.Append(dtHead.Rows[0][0].ToString() + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + dtDetail.Rows[0]["BSTKD"].ToString() + "|");
                        sbs.Append("1" + "|" + dtDetail.Rows[0]["KDMAT"].ToString() + "|" + strshipqty + "|");
                        sbs.Append(DateTime.Now.ToString("dd/MM/yyyy") + "||" + dtHead.Rows[0][0].ToString() + "|");
                        sbs.Append("1|Tianjin|" + dtHead.Rows[0]["LAND1"].ToString() + "|CN|");
                        sbs.Append(dtHead.Rows[0][0].ToString() + "|" + dtHead.Rows[0]["WAYBILL"].ToString() + "|" + supdfile + "|");
                        sbs.AppendLine();
                        string strASNFileName = string.Format(ASN_FILE_NAME, dtDetail.Rows[0]["BSTKD"].ToString(), DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                        string[] strTmp = strASNFileName.Split('\\');
                        string strFileName = strTmp[strTmp.Length - 1];

                        string strSql = string.Empty;
                        string strFolder = string.Empty;
                        string aaa = dtHead.Rows[0]["LAND1"].ToString();
                        strSql = @"SELECT FTP_FOLDER  FROM SHP.R_ASN_FTP_REGION    WHERE  COUNTRY='" + dtHead.Rows[0]["LAND1"].ToString() + "'";
                        using (OracleDB oraDB = new OracleDB())
                        {
                            try
                            {
                                using (OracleDataReader odr = oraDB.GetDataReader(strSql))
                                {
                                    if (odr.Read())
                                    {
                                        // strFolder = odr[0].ToString();
                                        strFolder = "SN_ASIA";

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                OracleDB.Write("[ERROR] Get FTP_FOLDER Error " + ex.Message);
                                return;
                            }
                        }

                        strSql1 = @"update SFC.UPD_DATALOAD_DETAIL_T   set  asn_ftp_time=sysdate where invoice='" + strInvoice + "'";
                        bool strok;
                        try
                        {
                            OracleDB oraDB = new OracleDB();
                            strok = oraDB.ExecuteNonQuery(strSql1);
                            if (strok == false)
                            {
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            OracleDB.Write("[ERROR] Get FTP_FOLDER Error " + ex.Message);
                            return;
                        }

                        if (CreateFile(sbs, strASNFileName))
                        {

                            if (FtpASNFile(strASNFileName, strFileName, strFolder))
                            {
                                File.Move(strASNFileName, ASN_FILE_BAK + "\\" + strFileName);

                            }
                        }

                    }
                    else
                    {
                        OracleDB.Write("DownLoad Invoice Info Error , Invoice : " + strInvoice);
                    }
                }

                catch (Exception ex)
                {
                    OracleDB.Write("DownLoad Invoice Info Error , Invoice : " + strInvoice);
                }
            }
            else
            {

            }





        }

        #endregion

        #region Public Method
        public string[,] Checkback(string strInvoice, string strtype)
        {
            string strSql = string.Empty;
            int iColNum, iRowNum;
            string[,] Getquery = new string[0, 0];
            switch (strtype)
            {
                case "UPD":
                    strSql = @"SELECT INVOICE,CREATE_DATE FROM SHP.UPD_DATALOAD_DETAIL_T WHERE INVOICE='" + strInvoice + "'";
                    break;
                case "IMEI":
                    strSql = @"SELECT INVOICE,IMEI_FILE_TIME FROM SHP.UPD_DATALOAD_DETAIL_T WHERE INVOICE='" + strInvoice + "'";
                    break;
                case "ASN":
                    strSql = @"SELECT INVOICE,ASN_FTP_TIME FROM SHP.UPD_DATALOAD_DETAIL_T WHERE INVOICE='" + strInvoice + "'";
                    break;
                case "GFS":
                    strSql = @"SELECT INVOICE,GFS_FILE_TIME FROM SHP.UPD_DATALOAD_DATAIL_T WHERE INVOICE='" + strInvoice + "'";
                    break;
                case "EDI":
                    strSql = @"SELECT INVOICE,EDI FROM SHP.UPD_DATALOAD_DATAIL_T WHERE INVOICE='" + strInvoice + "'";
                    break;
                default:
                    break;
            }
            using (OracleDB odb = new OracleDB())
            {
                try
                {
                    using (DataSet odr = odb.GetDataSet(strSql))
                    {
                        iColNum = odr.Tables[0].Columns.Count;
                        iRowNum = odr.Tables[0].Rows.Count;
                        Getquery = new string[iRowNum, iColNum];
                        for (int i = 0; i < iRowNum; i++)
                        {
                            for (int j = 0; j < iColNum; j++)
                            {
                                Getquery[i, j] = odr.Tables[0].Rows[i][j].ToString();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] Checkback " + ex.Message);
                }

            }
            return Getquery;

        }
        public string[,] AutoCheck(string p1, string p2, string type,string p4)
        {
            /// If p1=DN_NO :Means manual generate UPD file
            /// If p1=ALL   :Means auto chk data and generate UPD File
            string strRet = string.Empty;
            string strNo = string.Empty;
            int iColNum, iRowNum;
            string[,] arraydata = new string[0, 0];
            string strSql = string.Empty;

            ///check whether print 3s/4s finished?
            string sqlCondition1 = " AND a.SHIPPED_QTY=(SELECT SUM (quantity) FROM cmcs_sfc_packing_lines_all b WHERE b.invoice_number = a.invoice) ";
            string sqlCondition2 = " AND a.SHIPPED_QTY != (SELECT SUM (quantity) FROM cmcs_sfc_packing_lines_all b WHERE b.invoice_number = a.invoice) ";

            string sqlCondition3 = " AND a.invoice NOT IN (SELECT invoice FROM shp.upd_dataload_detail_t) ";
            string sqlCondition4 = " AND a.invoice  IN (SELECT invoice FROM shp.upd_dataload_detail_t) ";
            string SqlSort = " ORDER BY a.last_shipment_date DESC ";



            strSql = @"SELECT DISTINCT a.plant, a.invoice, a.customer_name, a.item_module,"
                                +"b.ship_to_country, b.ship_to_city,"
                                +"a.SHIPPED_QTY shipped_qty,"
                                +"last_shipment_date, c.customer_type,d.model "
                           +"FROM sfc.mes_mit_invoice a,"
                                +"cmcs_sfc_packing_lines_all b,"
                                +"sfc.cmcs_sfc_model c,"
                                +"shp.cmcs_sfc_shipping_data d "
                          +"WHERE a.invoice = b.invoice_number "
                            +"AND a.item_module = c.model "
                            + "AND b.internal_carton=d.carton_no "
                            +"AND a.last_shipment_date > SYSDATE - "+p4+" "
                            +"AND (   UPPER (a.customer_name) LIKE '%MOTO%' "
                                 +"OR UPPER (a.customer_name) LIKE '%藻迖縫嶺%' "
                                 +"OR UPPER (a.customer_name) LIKE '%SUTECH%' "
                                 +"OR UPPER (a.customer_name) LIKE '%TX4-SOI%' "
                                 +"OR a.customer_name LIKE 'FIH (Hong Kong) Limited' "
                                 +"OR UPPER (a.customer_name) LIKE '%MI-AMK%' "
                                 +"OR UPPER (a.customer_name) LIKE '%S&B INDUSTRY INC.%') ";

            if (p1.ToUpper() != "ALL")
            {
                if (p2 == "0")
                {
                    // need but donot execute yet
                    strSql = strSql + sqlCondition1 + sqlCondition3 + " AND a.invoice='" + p1 + "' ";
                    strSql = strSql + SqlSort;
                }
                else if (p2 == "1")
                {
                    strSql = strSql + sqlCondition1 + sqlCondition4 + " AND a.invoice='" + p1 + "' ";
                    strSql = strSql + SqlSort;
                }
                else if (p2 == "2")
                {
                    strSql = strSql + " AND a.invoice='" + p1 + "' ";
                    strSql = strSql + SqlSort;
                }
                else
                {
                    strSql = strSql + sqlCondition2 + sqlCondition3 + " AND a.invoice='" + p1 + "' ";
                    strSql = strSql + SqlSort;
                }
            }
            else
            {
                if (p2 == "0")
                {
                    // need but donot execute yet
                    strSql = strSql + sqlCondition1 + sqlCondition3 + SqlSort;
                }
                else if (p2 == "1")
                {
                    strSql = strSql + sqlCondition1 + sqlCondition4 + SqlSort;
                }
                else if (p2 == "2")
                {
                    strSql = strSql + SqlSort;
                }
                else
                {
                    strSql = strSql + sqlCondition2 + sqlCondition3 + SqlSort;
                }
            }

            using (OracleDB odb = new OracleDB())
            {
                try
                {

                    using (DataSet odr = odb.GetDataSet(strSql))
                    {
                        DataTable dt = odr.Tables[0];

                        iColNum = odr.Tables[0].Columns.Count;
                        iRowNum = odr.Tables[0].Rows.Count;
                        arraydata = new string[iRowNum, iColNum];
                        if (iRowNum > 0)
                        {
                            for (int i = 0; i < iRowNum; i++)
                            {
                                for (int j = 0; j < iColNum; j++)
                                {
                                    arraydata[i, j] = odr.Tables[0].Rows[i][j].ToString();
                                }
                            }


                        }
                    }

                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] UPDAlarm_Load->GetDataReader:" + ex.Message);
                }
            }





            return arraydata;

        }
        public string[,] Checkmaster(string motocustomer, string model, string shipto)
        {
            int iColNum, iRowNum;
            string[,] arraydata = new string[0, 0];
            string strSql = string.Empty;
            strSql = @"select F_TYPE,AUTOFLAG,F_TYPE2,F_TYPENO from ftram1 t WHERE MOTO_CUSTOMER='" + motocustomer + "' AND MODEL='" + model + "' AND SHIP_TO='" + shipto + "' order by F_TYPE DESC";

            using (OracleDB odb = new OracleDB())
            {
                try
                {

                    using (DataSet odr = odb.GetDataSet(strSql))
                    {
                        DataTable dt = odr.Tables[0];

                        iColNum = odr.Tables[0].Columns.Count;
                        iRowNum = odr.Tables[0].Rows.Count;
                        arraydata = new string[iRowNum, iColNum];
                        if (iRowNum > 0)
                        {
                            for (int i = 0; i < iRowNum; i++)
                            {
                                for (int j = 0; j < iColNum; j++)
                                {
                                    arraydata[i, j] = odr.Tables[0].Rows[i][j].ToString();
                                }
                            }


                        }
                    }

                }
                catch (Exception ex)
                {
                    OracleDB.Write("[ERROR] UPDAlarm_Load->GetDataReader:" + ex.Message);
                }
            }

            return arraydata;

        }
        #endregion

        public class Mail
        {
            public string NoteSubject { get; set; }
            public string NoteContent { get; set; }
            public string SendTo { get; set; }
            public string SendCC { get; set; }
            public string SendFrom { get; set; }
            public string SendProgram { get; set; }
            public DateTime CreateTime { get; set; }
            public string FinishedMark { get; set; }
        }
    }
}