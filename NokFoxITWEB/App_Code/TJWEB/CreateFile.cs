using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text;
using System.Xml;
using System.Net.Sockets;
using System.Net;
using System.Drawing;
using System.Data.OracleClient;

/// <summary>
/// Summary description for CreateFile
/// </summary>
public class CreateFile
{
    /*UPD,IMEI產生過程需要的特定方法*/
    #region 常量定義
    private OracleConnection connNormal;
    private OracleConnection connUpdConnect;
    public string FERROR;
    public const Int32 SINGLE_UPD_FILE_MAX_LENGTH = 20 * 1024 * 1024; // 20 MB 
    const int ERROR_FILE_NOT_FOUND = 2;
    const int ERROR_ACCESS_DENIED = 5;
    public bool bInTimer = false;
    public const string FACTORY_CODE = "FIHMLXTJ";
    public const string CDMA_FACTORY_CODE = "FIHMLXTJ";
    public static string FactoryCode;
    public const Int32 UPLOAD_FAIL_WAIT_DURATION = 1 * 20 * 1000; // 2 minutes
    public static readonly string UPD_UPLOAD_PROGRAM_PATH = @"C:\MCMS\PROGRAM\";
    public static readonly string ASN_FILE_NAME = @"C:\mcms\DS_ASN_FIH_{0}_{1}.dat";
    public static readonly string IMEI_FILE_BAK = @"D:\MCMS\DataBack\IMEI";
    //public static readonly string GFS_FILE_NAME = @"D:\MCMS\DataBack\GFS";
    public static readonly string ASN_FILE_BAK = @"D:\MCMS\DataBack\ASN";
    public static readonly string UPD_FILE_BAK = @"D:\MCMS\DataBack\UPD";
    public static readonly string CMD_UPD_FILE_BAK = @"D:\MCMS\DataBack\CDMA_UPD";
    public static readonly string GFS_FILE_BAK = @"D:\MCMS\DataBack\GFS";
    public static readonly string FTP_UPD_PATH = @"upd_service\to_be_processed";
    public static readonly string FTP_IMEI_PATH = "IMEI";
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

    public static readonly string[] DIRECTORY_ARRY_NEW = new string[] { @"D:\MCMS\DataBack\UPD",
                                                                        @"D:\MCMS\DataBack\CDMA_UPD",
                                                                        @"D:\MCMS\DataBack\IMEI",
                                                                        @"D:\MCMS\DataBack\GFS",
                                                                        @"D:\MCMS\DataBack\ASN"
                                                                      };
    #region NewFileName DingYi
    public static readonly string UPD_FILE_NAME = DIRECTORY_ARRY_NEW[DIRECTORY_ARRY_NEW.Length - 5] + @"\AS_" + FACTORY_CODE + "_UPD_IMEI_{0}_{1}.dat";
    public static readonly string IMEI_FILE_NAME = DIRECTORY_ARRY_NEW[DIRECTORY_ARRY_NEW.Length - 3] + @"\IMEI_{0}.xls";
    public static readonly string CDMA_UPD_FILE_NAME = DIRECTORY_ARRY_NEW[DIRECTORY_ARRY_NEW.Length - 4] + @"\AS_" + CDMA_FACTORY_CODE + "_UPD_MEID_" + "{0}_{1}.dat";
    public static readonly string GFS_FILE_NAME = DIRECTORY_ARRY_NEW[DIRECTORY_ARRY_NEW.Length - 2] + @"\PROG_V01_" + CDMA_FACTORY_CODE + "_{0}_{1}-{2}.dat";
    #endregion

    #region Old FileName DingYi
    //public static readonly string UPD_FILE_NAME = DIRECTORY_ARRY[DIRECTORY_ARRY.Length - 5] + @"\AS_" +FACTORY_CODE + "_UPD_IMEI_{0}_{1}.dat";
    //public static readonly string IMEI_FILE_NAME = @"d:\mcms\IMEI_{0}.xls";
    //public static readonly string CDMA_UPD_FILE_NAME = DIRECTORY_ARRY[DIRECTORY_ARRY.Length - 5]
    //                                                + @"\AS_" + CDMA_FACTORY_CODE + "_UPD_MEID_" + "{0}_{1}.dat";
    //public static readonly string GFS_FILE_NAME = DIRECTORY_ARRY[DIRECTORY_ARRY.Length - 1] + @"\PROG_V01_" + CDMA_FACTORY_CODE + "_{0}_{1}-{2}.dat";
    #endregion 
    public static readonly string ZIP_CMD = @"e:\Program Files\7-Zip\7z.exe ";// a -tzip ";  

    #endregion
    public CreateFile()
	{
		//
		// TODO: Add constructor logic here
		//
        string strConnNormal = ConfigurationManager.AppSettings["NormalDbConnectionString"];
        connNormal = new OracleConnection(strConnNormal);
        FERROR = "";
	}

    public string GetFerror()
    {
        return FERROR;
    }

    public int CheckConnect(OracleConnection conn)
    {
        int iRet;
        try
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            FERROR = "";
            iRet = 0;
        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        return iRet;
    }

    private int SelectSql(string SQL, OracleConnection conn, ref System.Data.DataTable dt)
    {
        int iRet;
        iRet = CheckConnect(conn);
        OracleDataAdapter oda = null;
        OracleCommand cmd = null;
        try
        {
            cmd = new OracleCommand(SQL, conn);
            oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(dt);
            iRet = 0;
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (oda != null) oda.Dispose();
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    private OracleDataReader GetDataReader(string strBySql,OracleConnection conn)
    {
        int iRet;
        iRet = CheckConnect(conn);
        OracleDataReader odr = null;
        OracleCommand oCmd = null;
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            odr = oCmd.ExecuteReader();
        }
        catch (OracleException ex)
        {
            FERROR = ex.Message;
        }
        finally
        {
            if (oCmd != null) oCmd.Dispose();
        }
        return odr;
    }

    private OracleDataReader GetDataReader(string strBySql,  OracleParameter[] cmdParms,OracleConnection conn)
    {
        int iRet;
        iRet = CheckConnect(conn);
        OracleDataReader odr = null;
        OracleCommand oCmd = null;
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Clear();
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    oCmd.Parameters.Add(parm);
            }
            odr = oCmd.ExecuteReader();
            oCmd.Parameters.Clear();
        }
        catch (OracleException ex)
        {
           Write("[ERROR]" + ex.Message + ":GetDataReader()");
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }
        return odr;
    }

    private bool ExecuteStoredProcedure(string strStoredProcedureName, string[] strParams, string[] strValue, out string[] strOutput)
    {
        OracleCommand ocmd = null;
        bool bStatus = false;
        strOutput = new string[strParams.Length - strValue.Length];
        try
        {
            ocmd = new OracleCommand();
            ocmd.Connection = connNormal;
            ocmd.CommandType = CommandType.StoredProcedure;
            ocmd.CommandText = strStoredProcedureName;
            for (int i = 0; i < strParams.Length; i++)
            {
                if (i > strValue.Length - 1)
                {
                    ocmd.Parameters.Add(strParams[i], OracleType.NVarChar, 200);
                    ocmd.Parameters[strParams[i]].Direction = ParameterDirection.Output;
                }
                else
                {
                    ocmd.Parameters.AddWithValue(strParams[i], strValue[i]);
                }
            }
            ocmd.ExecuteNonQuery();
            int iOutputIndx = 0;
            for (int i = strValue.Length; i < strParams.Length; i++)
            {
                strOutput[iOutputIndx] = ocmd.Parameters[strParams[i]].Value.ToString();
                iOutputIndx++;
            }
            bStatus = true;
        }
        catch (OracleException ex)
        {
            bStatus = false;
            Write("[ERROR] " + ex.Message + " :ExecuteStoredProcedure() :" + strStoredProcedureName);
        }
        finally
        {
            if (ocmd != null)
            {
                ocmd.Dispose();
            }

        }
        return bStatus;
    }

    private  bool ExecuteNonQuery(string strBySql,OracleConnection conn)
    {
        bool bRet = false;
        int iRet = 0;
        OracleCommand oCmd = null;
        try
        {
            iRet = CheckConnect(conn);
            oCmd = new OracleCommand(strBySql, conn);
            int i = Convert.ToInt32(oCmd.ExecuteNonQuery());
            if (i > 0)
            {
                bRet = true;
            }
        }
        catch (OracleException ex)
        {
           Write("[ERROR] " + ex.Message + ":ExecuteNonQuery - " + strBySql);
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }

        return bRet;
    }

    private int ExecSqlNoQuery(string strSql, OracleConnection conn)
    {
        int iRet;
        OracleCommand cmd = null;
        int intRet = CheckConnect(conn);
        try
        {
            cmd = new OracleCommand(strSql, conn);
            iRet = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    public DataSet GetDataSet(string strBySql, OracleParameter[] cmdParms,OracleConnection conn)
    {
        DataSet odr = new DataSet();
        OracleCommand oCmd = null;
        int iRet = 0;
        try
        {
            iRet = CheckConnect(conn);
            oCmd = new OracleCommand(strBySql, conn);
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Clear();
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    oCmd.Parameters.Add(parm);
            }
            OracleDataAdapter dd = new OracleDataAdapter();
            dd.SelectCommand = oCmd;
            dd.Fill(odr);
        }
        catch (Exception ex)
        {
            Write(" [ERROR] " + ex.Message + ":GetDataAdapter()");
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }
        return odr;
    }

    public DataSet GetDataSet(string strBySql,OracleConnection conn)
    {
        DataSet odr = new DataSet();
        OracleCommand oCmd = null;
        int iRet = 0;
        iRet = CheckConnect(conn);
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            OracleDataAdapter dd = new OracleDataAdapter();
            dd.SelectCommand = oCmd;
            dd.Fill(odr);
        }
        catch (OracleException ex)
        {
           Write("[ERROR] " + ex.Message + ":GetDataReader()");
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }

        return odr;
    }

    private int  ExecSqlNoQueryWithParam(string strSql, OracleParameter[] cmdParms,OracleConnection conn)
    {
        int iRet ;
        OracleCommand cmd = null;
        int intRet = CheckConnect(conn);
        try
        {
            cmd = new OracleCommand(strSql, conn);

            for (int i = 0; i < cmdParms.GetLength(0); i++)
                cmd.Parameters.Add(cmdParms[i]);
            intRet = cmd.ExecuteNonQuery();
            iRet = 0;
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    public  string  GetAllInvoiceNumberHanle(string DN,OracleConnection conn)
    {
        string strRet = string.Empty;
        string strInvoice = string.Empty;
        string strSql = string.Empty;
        int iRet = 0;
        strSql = @"SELECT DISTINCT A.PLANT,
                                A.INVOICE,
                                A.CUSTOMER_NAME,
                                A.ITEM_MODULE,
                                UPPER (b.SHIP_TO_COUNTRY) AS SHIP_TO_COUNTRY,
                                (SELECT SUM (quantity)
                                   FROM CMCS_SFC_PACKING_LINES_ALL b
                                  WHERE b.INVOICE_NUMBER = a.invoice)
                                   AS SHIPPED_QTY,
                                LAST_SHIPMENT_DATE,
                                C.CUSTOMER_TYPE
                  FROM SFC.MES_MIT_INVOICE A,
                       SAP.CMCS_SFC_PACKING_LINES_ALL B,
                       SFC.CMCS_SFC_MODEL C
                 WHERE A.Invoice = B.INVOICE_NUMBER
                       AND A.ITEM_MODULE = C.MODEL
                       AND INVOICE ='" + DN + "'";
        try
        {
            iRet=CheckConnect(conn);
            using (OracleDataReader odr = GetDataReader(strSql,conn))
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
            FERROR = ex.Message;
        }
        return strRet;
    }

    public int GetCdmaPo(string DN,ref string CdmaPo,OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        strSql = "SELECT CUSTOMER_PO FROM SAP. CMCS_SFC_PACKING_LINES_ALL WHERE INVOICE_NUMBER='"+DN+"' AND ROWNUM=1";

        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                using (OracleDataReader odr = GetDataReader(strSql, conn))
                {
                    while (odr.Read())
                    {
                        CdmaPo = odr[0].ToString();
                    }
                    if (CdmaPo.Length > 0)
                    {
                        iRet = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;

        }
        return iRet;
    }

    public int GetPo(string strSql, ref System.Data.DataTable dt,OracleConnection conn)
    {
        int iRet;
        iRet=SelectSql(strSql,conn,ref dt);
        return iRet;
    }

    public int GetSfcModelType(string DN, ref string strWitchCode, ref string strSfcModel, OracleConnection conn)
    {

        int iRet = 0;
        string strSql = "SELECT C.PRGCODE AS SWITCHCODE,C.MODEL AS MODEL FROM " +
                                            "SAP.SAP_INVOICE_INFO A," +
                                            "SHP.ROS_TCH_PN B," +
                                            "SFC.CMCS_SFC_MODEL C " +
                                            "WHERE A.ORDER_ITEM=B.PPART " +
                                            "AND B.TYPE_CODE=C.MODEL " +
                                            "AND A.INVOICE=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            OracleDataReader odr = GetDataReader(strSql, Parms, conn);
            while (odr.Read())
            {
                strWitchCode = odr[0].ToString();
                strSfcModel = odr[1].ToString();
                break;
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message.ToString();
        }
        return iRet;
    }

    public int InserterrorMessage(string strDN, string Message, string strimei,OracleConnection conn)
    {
        int RRet ;
        string strSql = string.Empty;
        int iRet = 0;
        strSql = "INSERT INTO SHP.UPD_INVOICE_HAND_TEMP(INVOICE,MESSAGE,IMEI) " +
                           "VALUES(:V_DN,:V_MES,:V_IMEI)";
        OracleParameter[] Parms = new OracleParameter[3];
        Parms[0] = new OracleParameter("V_DN", strDN);
        Parms[1] = new OracleParameter("V_MES", Message);
        Parms[2] = new OracleParameter("V_IMEI", strimei);
         iRet = CheckConnect(conn);
        try
        {
            if (iRet == 0)
            {
                iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);     
            }

        }
        catch (Exception ex)
        {
            iRet = -1;
            Write("[ERROR] Record" + ex.Message);
        }
        return iRet;
    }

    public int IsoPresence(string DN,OracleConnection conn)
    {
        int iRet = 0;
        string strSql = @"SELECT COUNT(B.COUNTRY_CODE) AS COUNTRYCOUNT FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SAP.COUNTRY_ISO_LINK B" +
                                                                       " WHERE (A.SHIP_TO_COUNTRY=B.DESCRIPTION or A.SHIP_TO_COUNTRY=B.CMCSDESCRIPTION)" +
                                                                       " AND A.INVOICE_NUMBER=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        iRet = CheckConnect(conn);
        try
        {
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                iRet = Convert.ToInt32(odr[0].ToString());
                break;
            }
        }
        catch (Exception ex)
        {
            Write("[ERROR] ISO Find" + ex.Message);
            FERROR = ex.Message;
            iRet = -1;
        }
        return iRet;
    }

    public int GetImeiCode(string Region, string SapModel, ref string ImeiCode,OracleConnection conn)
    {
        int iRet = 0;
        string strSql = "SELECT IMEICODE FROM  SHP.UPD_COUNTRY_LINK_IMEICODE WHERE SAPMODEL=:V_SMODEL AND SHIPTOCOUNTRY=:V_REN";
        OracleParameter[] Parms = new OracleParameter[2];
        Parms[0] = new OracleParameter("V_SMODEL", SapModel);
        Parms[1] = new OracleParameter("V_REN", Region);
        iRet = CheckConnect(conn);
        try
        {
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                ImeiCode = odr[0].ToString();
                iRet = 0;
                break;
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public string CheckPo(string DN,OracleConnection conn)
    {
        string strRet = string.Empty;
        int iRet;
        string strSql = @"SELECT DISTINCT B.PO_NUMBER FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SHP.UPD_ORDER_INFORMATION B" +
                                                            " WHERE A.CUSTOMER_PO=B.PO_NUMBER AND A.INVOICE_NUMBER=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                strRet = odr[0].ToString();
                break;
            }
        }
        catch (Exception ex)
        {
            Write("[ERROR]CheckPO " + ex.Message);
            FERROR = ex.Message.ToString();
        }
        return strRet;
    }

    public string CheckRepeat(string DN,OracleConnection conn)
    {
        string strRet = string.Empty;
        int iRet;
        string strSql = string.Empty;
        strSql = @"SELECT INVOICE,DECODE(COUNTRE,NULL,1,COUNTRE) AS CS FROM SHP.UPD_DATAlOAD_DETAIL_T WHERE ROWNUM<=1 AND INVOICE=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(connNormal);
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
              while (odr.Read())
                {
                    strRet = "" + odr[0].ToString() + "#" + odr[1].ToString() + "";
                    break;
                }
            
        }
        catch (Exception ex)
        {
            Write("[ERROR] CheckRepeat " + ex.Message);
            FERROR = ex.Message.ToString();
        }
        return strRet;
    }

    public int regionPresence(string DN,OracleConnection conn)
    {
        int i = 0;
        string strSql = @"SELECT COUNT(B.COUNTRY_CODE) AS COUNTRYCOUNT FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SAP.COUNTRY_ISO_LINK B" +
                                                                    " WHERE (A.SHIP_TO_COUNTRY=B.DESCRIPTION or A.SHIP_TO_COUNTRY=B.CMCSDESCRIPTION)" +
                                                                    " AND A.INVOICE_NUMBER=:V_DN and B.REGION='LA'";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
            try
            {
                i = CheckConnect(conn);
                using (OracleDataReader odr =GetDataReader(strSql, Parms,conn))
                {
                    while (odr.Read())
                    {
                        i = Convert.ToInt32(odr[0].ToString());
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Write("[ERROR] ISO Find" + ex.Message);
                i = -1;
            }

        return i;
    }

    public List<StringBuilder> GetUpdData(string DN, string SAPModel, string ProCode, string Qty, ref string SfcQty, ref string ErrorMessage, OracleConnection conn, OracleConnection conntw)
    {
        string strSql = string.Empty;
        string FactoryCode = string.Empty;
        List<StringBuilder> lsb = new List<StringBuilder>();
        FactoryCode = "ZFOXTJ-ODM";

        if (ProCode == "UPD01")
        {
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2,'" + FactoryCode + "' f3,"
                                              + "TO_CHAR(e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                              + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                              + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                              + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                              + "DECODE (c.out_date,"
                                                     + " NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                                      + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                              + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                              + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                              + "h.sold_tocustomer_name f18,"
                                              + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                              + "d.serial_num f20, d.ta_number f21,"
                                              + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                              + "c.work_order f25, e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                              + "e.privilegepwd f30, '' f31, '' f32, '' f33, '' f34,"
                                              + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                              + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                              + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                              + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                              + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                              + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                              + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                              + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                              + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79,e.nkey f80,"
                                              + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90 "
                                         + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                              + "shp.cmcs_sfc_packing_lines_all b,"
                                              + "shp.cmcs_sfc_shipping_data c,"
                                              + "shp.cmcs_sfc_imeinum d,"
                                              + "" + SAPModel + ".e2pconfig e,"
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
                                               + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + SAPModel + ".e2pconfig h "
                                               + "WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                          + "AND j.carton_no = b.internal_carton "
                                          + "AND (UPPER (b.ship_to_country) =UPPER (k.DESCRIPTION )  or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) )"
                                          + "AND b.invoice_number = a.invoice_no "
                                          + "AND a.invoice_no =:V_DN";
        }
        else if (ProCode == "UPD04")
        {

            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, 'ZFOXTJ-ODM' f3,"
                                   + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                   + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                   + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                   + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                   + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                           + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                  + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                   + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ "
                                   + "h.sold_tocustomer_name f18,"
                                   + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                   + "d.serial_num f20, d.ta_number f21,"
                                   + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                   + "c.work_order f25,  e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                   + "'' f30, '' f31, '' f32, '' f33, '' f34,"
                                   + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                   + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                   + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                   + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                   + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                   + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59, '' f60, '' f61,"
                                   + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                   + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                   + "DECODE(d.imeinum2,NULL,'',d.imeinum2) f71, DECODE(d.imeinum2,NULL,'','IMEI') f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79,DECODE(e.nkey,NULL,'',e.nkey) f80,"
                                   + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90 "
                              + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                   + "shp.cmcs_sfc_packing_lines_all b,"
                                   + "shp.cmcs_sfc_shipping_data c,"
                                   + "shp.cmcs_sfc_imeinum d,"
                                   + "" + SAPModel + ".e2pconfigtw e,"
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
                                                + "(SELECT MAX (h.e2pdate) FROM " + SAPModel + ".e2pconfigtw h WHERE h.imei = c.imei "
                                    + "AND h.status = 'PASS')) "
                                 + "AND j.carton_no = b.internal_carton "
                                 + "AND (UPPER (b.ship_to_country) = UPPER (k.DESCRIPTION )  or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) ) "
                                 + "AND b.invoice_number = a.invoice_no "
                                 + "AND a.invoice_no =:V_DN";


        }
        else if (ProCode == "UPD02")
        {
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                + "DECODE(h.customer_id,NULL,'','N/A','',h.customer_id) f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                + "b.ship_to_city f15, k.iso_code f16, DECODE(h.customer_id,NULL,'','N/A','',h.customer_id) f17," /*H.CUSTOMER_NAME*/
                                + "h.sold_tocustomer_name f18,"
                                + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                + "d.serial_num f20, d.ta_number f21,"
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
                                + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                + "d.imeinum2 f71, 'IMEI' f72, '' f73, '' f74, '' f75, '' f76,'' f77,'' f78,'' f79,e.nkey f80,"
                                + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90 "
                        + " FROM shp.cmcs_sfc_ship_carton_map a,"
                                + "shp.cmcs_sfc_packing_lines_all b,"
                                + "shp.cmcs_sfc_shipping_data c,"
                                + "shp.cmcs_sfc_imeinum d,"
                                + "" + SAPModel + ".e2pconfig e,"
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
                                                + "  FROM " + SAPModel + ".e2pconfig h"
                                                + "  WHERE h.imei = c.imei AND h.status = 'PASS')"
                                + " ) "
                           + " AND j.carton_no = b.internal_carton"
                           + " AND (UPPER (b.ship_to_country) = UPPER (k.DESCRIPTION ) or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) ) "
                           + " AND b.invoice_number = a.invoice_no"
                           + " AND a.invoice_no =:V_DN";

        }
        else if (ProCode == "UPD03")
        {
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                    + "TO_CHAR (e.create_date, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                    + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                    + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                    + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                    + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                    + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                    + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                    + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ h.sold_tocustomer_name f18,"
                                    + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                    + "d.serial_num f20, d.ta_number f21,"
                                    + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                    + "c.work_order f25, SUBSTR(e.th7, 16, 8) f26, '' f27, '' f28, SUBSTR (e.th7, 48, 8) f29,"
                                    + "'' f30,'' f31, '' f32, '' f33, '' f34,"
                                    + "DECODE (d.customer_num, NULL, 'M14' || SUBSTR (d.imeinum, 8, 7), d.customer_num) f35,"
                                    + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                    + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,'' f43, '' f44, '' f45, '' f46,"
                                    + "'' f47, '' f48, '' f49,"
                                    + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                    + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                    + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                    + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                    + "DECODE(d.imeinum2,NULL,'',d.imeinum2) f71, DECODE(d.imeinum2,NULL,'','IMEI') f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79,'' f80,"
                                    + "'' f81,'' f82,'' f83,'' f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90 "
                               + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                    + "shp.cmcs_sfc_packing_lines_all b,"
                                    + "shp.cmcs_sfc_shipping_data c,"
                                    + "shp.cmcs_sfc_imeinum d,"
                                    + "(SELECT   MAX (ID || th7) th7, TRACK_ID,MAX (create_date) create_date FROM testinfo.testinfo_head"
                                    + " WHERE status = 'PASS' AND LENGTH (th7) = 80 AND station_name = 'E2P' GROUP BY TRACK_ID) e, "
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
                                + "AND c.productid = e.TRACK_ID "
                                + "AND d.porder = g.porder "
                                + "AND (UPPER (b.ship_to_country) =UPPER ( k.description )  or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) )"
                                + "AND a.invoice_no =:V_DN "
                                + "AND b.invoice_number =:V_DN";
        }
        else if (ProCode == "UPD06")
        {
            strSql = @"SELECT DISTINCT 'MEID' f1, d.imeinum f2, 'FIHTJ' f3,
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
                               '' f80, '' f81, '' f82,'' f83,'' f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90,'' f91,'' f92,'' f93,'' f94,'' f95
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
                           AND  (UPPER (b.ship_to_country) = UPPER (k.description)
            OR UPPER (b.ship_to_country) = UPPER (k.CMCSDESCRIPTION))
                           AND b.invoice_number=:V_DN";
        }
        else
        {
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                                  + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                                  + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                                  + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                                  + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                                  + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                                  + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                                  + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                                  + "h.sold_tocustomer_name f18,"
                                                  + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                                  + "d.serial_num f20, d.ta_number f21,"
                                                  + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                                                  + "c.work_order f25, DECODE(e.nkey,NULL,'',e.nkey) f26, '' f27, '' f28, DECODE(e.nskey,NULL,'',e.nskey) f29,"
                                                  + "DECODE(e.privilegepwd,NULL,'',e.privilegepwd) f30, '' f31, '' f32, '' f33, '' f34,"
                                                  + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                                  + "d.btaddress f36, '' f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                                  + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                                  + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                                  + " '' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                                  + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                                  + "DECODE (h.bill_to_id,NULL,'','N/A','',h.bill_to_id) f62, '' f63,"
                                                  + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                                  + "DECODE(d.imeinum2,NULL,'',d.imeinum2) f71, DECODE(d.imeinum2,NULL,'','IMEI') f72,"
                                                  + "'' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79,DECODE(e.nkey,NULL,'',e.nkey) f80,"
                                                  + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,"
                                                  + "'' f85,'' f86,'' f87,'' f88,'' f89 ,'' f90 "
                                             + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                                 + "shp.cmcs_sfc_packing_lines_all b,"
                                                 + "shp.cmcs_sfc_shipping_data c,"
                                                 + "shp.cmcs_sfc_imeinum d,"
                                                 + "" + SAPModel + ".e2pconfigtw e,"
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
                                                   + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + SAPModel + ".e2pconfig h WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                             + "AND j.carton_no = b.internal_carton "
                                             + "AND (UPPER (b.ship_to_country) = UPPER (k.DESCRIPTION )   or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) )"
                                             + "AND a.invoice_no =:V_DN";

        }
        if (ProCode != "UPD06")
            strSql = strSql + " ORDER BY C.IMEI";
        else
            strSql = strSql + " ORDER BY D.IMEINUM";


        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        int iRet;
        try
        {
            iRet = CheckConnect(conn);
            using (DataSet odr = GetDataSet(strSql, Parms, conn))
            {
                StringBuilder sbupd = new StringBuilder();
                StringBuilder sbs = new StringBuilder();
                int iColNum = odr.Tables[0].Columns.Count;
                int iRowNum = odr.Tables[0].Rows.Count;
                SfcQty = Convert.ToString(iRowNum);
                if (SfcQty == Qty)
                {
                    for (int n = 0; n < iRowNum; n++)
                    {
                        if (ProCode != "UPD06")
                        {
                            for (int rownum = 0; rownum < iColNum; rownum++)
                            {
                                string FiledValue = odr.Tables[0].Rows[n][rownum].ToString();
                                string FiledImei = odr.Tables[0].Rows[n][1].ToString();
                                bool FunChk = CheckFieldValue(DN, SAPModel, FiledValue, FiledImei, n + 1, rownum + 1, ref ErrorMessage, conntw);
                                if (FunChk == false)
                                {
                                    return lsb;
                                }
                            }
                        }

                        for (int i = 0; i < iColNum; i++)
                        {

                            if (ProCode != "UPD06")
                            {
                                if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15 || i == 16 || i == 17 || i == 19 || i == 22 || i == 23 || i == 25 || i == 28 || i == 29 || i == 34 || i == 55 || i == 79)
                                {
                                    if (odr.Tables[0].Rows[n][i].ToString() == "")
                                    {
                                        odr.Tables[0].Rows[n][i] = "NA";
                                    }
                                }

                            }
                            sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                            sbs.Append("|");
                        }
                        sbs.AppendLine();

                        if ((sbupd.Length + sbs.Length) <= CreateFile.SINGLE_UPD_FILE_MAX_LENGTH)
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
                else
                {
                    return lsb;
                }
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return lsb;
    }

    public List<StringBuilder> GetGFS(string strInvoice, OracleConnection conn)
    {
        List<StringBuilder> lsb = new List<StringBuilder>();
        string strSql = string.Empty;
        int iRet;
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
                             AND A.INVOICE_NUMBER=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", strInvoice);

        try
        {
            iRet = CheckConnect(conn);
            using (DataSet odr = GetDataSet(strSql, Parms, conn))
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

                    if ((sbupd.Length + sbs.Length) <= CreateFile.SINGLE_UPD_FILE_MAX_LENGTH)
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
            iRet = -1;
            FERROR = ex.Message;
        }
        return lsb;
    }

    public string CreateIMEIFile(string strInvoice, string strModel,string ImeiCode,OracleConnection conn)
    {
        string strIMEIFile = "-";
        string strCustomerName = string.Empty;
        string strSql = string.Empty;
        string strSelectSql = string.Empty;
        string strWhereSql = string.Empty;
        string strTableSql = string.Empty;
        int ShuangKa = 0;
        strSelectSql = @" SELECT e.SO_NO SALES_PO,g.PPART FIH_PN,g.HW_VER HW,e.SO_NO SO,g.SW_VER SW,
                                            LPAD(e.INVOICE,10,'0') INVOICE_NO,g.PHONE_MODEL XCVR_NO,
                                            (SELECT COUNT(b.IMEI) FROM SAP.CMCS_SFC_PACKING_LINES_ALL a,SHP.CMCS_SFC_SHIPPING_DATA b 
                                            WHERE a.INTERNAL_CARTON=B.CARTON_NO and a.INVOICE_NUMBER='" + strInvoice + @"'  ) QTY ,
                                            e.ADDRESS SHIP_TO,g.SA_NO moto_no, 'TIANJIN' FACILITY,
                                            TO_CHAR(SYSDATE,'YYYY/MM/DD') CREATEDATE,a.CUSTOMER_PO MOTO_PO, 
                                            c.IMEINUM IMEI,c.CUSTOMER_NUM MSN,'12345678' NP_PW,b.SA_NO MODEL,TO_CHAR(a.SHIP_DATE,'yyyy/mm/ dd') SHIP_DATE,
                                            d.CASEID  CARTON_ID,lpad(a.INVOICE_NUMBER,10,'0')||'+1095759' PALLET_ID";

        strWhereSql = @" WHERE a.INVOICE_Number='" + strInvoice + @"'
                                    AND a.INVOICE_Number=e.INVOICE
                                    AND a.INTERNAL_CARTON=b.CARTON_NO
                                    AND b.IMEI=c.IMEINUM 
                                    AND a.INTERNAL_CARTON=d.CARTON_NO 
                                    AND e.SO_NO=f.ORDER_NUMBER
                                    AND d.PPART=g.PPART";

        strTableSql = @" FROM SAP.CMCS_SFC_PACKING_LINES_ALL a,
                                 SHP.CMCS_SFC_SHIPPING_DATA b,
                                 SHP.CMCS_SFC_IMEINUM c,
                                 SHP.CMCS_SFC_CARTON d,
                                 SAP.SAP_INVOICE_INFO e ,
                                 SAP.SAP_ORDER_INFO F,
                                 SHP.ROS_TCH_PN g";
        ShuangKa = DouBSwitch(strInvoice,conn);//判斷單雙卡
        if (ShuangKa > 0)
        {
            strSelectSql += ",C.IMEINUM2 IMEI2";
        }
        else
        {
            if (ImeiCode=="IMEI06")
            {
                strSelectSql += ",DECODE(c.imeinum2,'','N/A',c.imeinum2) imei2, SUBSTR (h.th7, 16, 8) p_uc,SUBSTR (h.th7, 48, 8) s_uc, 'N/A' s_pc";
                strTableSql += ",(SELECT   MAX (ID || th7) th7, th26, MAX (create_date) create_date FROM testinfo.testinfo_head" +
                                            " WHERE status = 'PASS' AND LENGTH (th7) = 80" +
                                            "AND station_name = 'E2P' GROUP BY th26) h";
                strWhereSql += " AND b.imei = h.th26";
            }
            else if (ImeiCode=="IMEI05")
            {
                strSelectSql += ",DECODE (c.imeinum2, '', 'N/A', c.imeinum2) imei2, DECODE(h.nkey,'','N/A',h.nkey) p_uc,DECODE(h.nskey,'','N/A',h.nskey) s_uc,'N/A' s_pc";
                strTableSql += ","+strModel+".E2PCONFIGTW h";
                strWhereSql += " AND (c.imeinum = h.imei AND h.status = 'PASS' AND h.e2pdate = (SELECT MAX (h.e2pdate) FROM " + strModel + ".e2pconfigtw h" +
                                               " WHERE h.imei = c.imeinum AND h.status = 'PASS'))";
            }
            else
            {
                strSelectSql += ",C.BTADDRESS,C.WIFI_ADDRESS";
            }
        }

        strSql = strSelectSql + strTableSql + strWhereSql + " ORDER BY CARTON_ID";

        DateTime Process_BeforeTime = DateTime.Now;
        Excel.Application app = new Excel.Application();

        int ConnStatus = CheckConnect(conn);

            try
            {
                Excel.Workbook wb = app.Workbooks.Add(true);
                Excel.Worksheet st = (Excel.Worksheet)app.Sheets[1];
                app.DisplayAlerts = false;
                app.AlertBeforeOverwriting = false;
                
                DataSet odr = GetDataSet(strSql,conn);
                System.Data.DataTable dt = odr.Tables[0];
                //throw new Exception("test");
                bool bInit = false;
                int n = 8;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!bInit)
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
                        if (ShuangKa > 0 &&ImeiCode!="IMEI05"&&ImeiCode!="IMEI06")
                        {
                            st.Cells[7, 1] = "Pri-IMEI NO.";
                            st.Cells[7, 2] = "Sec-IMEI NO.";
                            st.Cells[7, 3] = "MSN";
                            st.Cells[7, 4] = "NP P/W";
                            st.Cells[7, 5] = "Model";
                            st.Cells[7, 6] = "Shipment Date";
                            st.Cells[7, 7] = "CARTON ID";
                            st.Cells[7, 8] = "PALLET ID";
                        }
                        else
                        {
                        
                            if (ImeiCode=="IMEI06")
                            {
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
                            else if (ImeiCode=="IMEI05")
                            {
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
                            else if (ImeiCode=="IMEI01")
                            {
                                st.Cells[7, 1] = "IMEI NO.";
                                st.Cells[7, 2] = "MSN";
                                st.Cells[7, 3] = "NP P/W";
                                st.Cells[7, 4] = "Model";
                                st.Cells[7, 5] = "Shipment Date";
                                st.Cells[7, 6] = "CARTON ID";
                                st.Cells[7, 7] = "PALLET ID";
                            }
                            else if (ImeiCode=="IMEI02")
                            {
                                st.Cells[7, 1] = "IMEI NO.";
                                st.Cells[7, 2] = "MSN";
                                st.Cells[7, 3] = "NP P/W";
                                st.Cells[7, 4] = "Model";
                                st.Cells[7, 5] = "Shipment Date";
                                st.Cells[7, 6] = "CARTON ID";
                                st.Cells[7, 7] = "PALLET ID";
                                st.Cells[7, 8] = "BT MAC Adress";
                                st.Cells[7, 9] = "Wifi MAC Adress";
                            }
                            else
                            {
                                st.Cells[7, 2] = "MSN";
                                st.Cells[7, 3] = "NP P/W";
                                st.Cells[7, 4] = "Model";
                                st.Cells[7, 5] = "Shipment Date";
                                st.Cells[7, 6] = "CARTON ID";
                                st.Cells[7, 7] = "PALLET ID";
                            }
                        }

                        bInit = true;
                    }
                    break;
                }
                int x = 0;
                object[,] objData = null;

                if (ShuangKa > 0 && ImeiCode!="IMEI05"&&ImeiCode!="IMEI06")
                {
                    objData = new Object[dt.Rows.Count, 8];
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
                    Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                    range.NumberFormatLocal = "@";
                    range.Value2 = objData;
                }
                else
                {
                  
                    if (ImeiCode=="IMEI06")
                    {
                        objData = new Object[dt.Rows.Count, 10];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else if (ImeiCode=="IMEI05")
                    {
                        objData = new Object[dt.Rows.Count, 10];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else if (ImeiCode=="IMEI01")
                    {
                        objData = new Object[dt.Rows.Count, 8];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else if (ImeiCode=="IMEI02")
                    {
                        objData = new Object[dt.Rows.Count, 10];
                        foreach (DataRow dr in dt.Rows)
                        {
                            objData[x, 0] = dr[13].ToString();
                            objData[x, 1] = dr[14].ToString();
                            objData[x, 2] = dr[15].ToString();
                            objData[x, 3] = dr[16].ToString();
                            objData[x, 4] = dr[17].ToString();
                            objData[x, 5] = dr[18].ToString();
                            objData[x, 6] = dr[19].ToString();
                            objData[x, 7] = dr[20].ToString();
                            objData[x, 8] = dr[21].ToString();
                            x++;
                        }
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else
                    {
                        objData = new Object[dt.Rows.Count, 8];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                }

                string strTempImeiPath = AppDomain.CurrentDomain.BaseDirectory;
                strTempImeiPath = strTempImeiPath + "Upload\\IMEI\\" + "IMEI_{0}.xls";
                strIMEIFile = string.Format(strTempImeiPath, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                wb.SaveAs(strIMEIFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //Excel.XlFileFormat.xlExcel8
                app.ActiveWorkbook.Close(true, strIMEIFile, Type.Missing);
                app.Workbooks.Close();
                app.Quit();
                DateTime Process_AfterTime = DateTime.Now;
                KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                GC.Collect();
                 
            }
            catch (Exception ex)
            {
                Write("[ERROR] GetInvoiceInfo " + ex.Message);
                app.Quit();
                DateTime Process_AfterTime = DateTime.Now;
                KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                GC.Collect();
            }
  

        return strIMEIFile;
    }

    public int DouBSwitch(string DN,OracleConnection conn)
    {
        int i = 0;
        string strSql = @"SELECT COUNT(C.IMEINUM2) FROM SAP.CMCS_SFC_PACKING_LINES_ALL A, SHP.CMCS_SFC_SHIPPING_DATA B,SHP.CMCS_SFC_IMEINUM C 
                                 WHERE A.INTERNAL_CARTON=B.CARTON_NO AND B.WORK_ORDER = C.PORDER AND  INVOICE_NUMBER=:V_DN  AND ROWNUM=1";

        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
     
            try
            {
                i = CheckConnect(connNormal);
                using (OracleDataReader odr =GetDataReader(strSql, Parms,conn))
                {
                    while (odr.Read())
                    {
                        i = Convert.ToInt32(odr[0].ToString());
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Write("[ERROR] IMEI2 Find" + ex.Message);
                i = -1;
            }
        

        return i;
    }

    public bool CreateUpdFile(StringBuilder sb, string FilePathName)
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
                Write("[SUCCEED] create file " + FilePathName);
            }
            catch (Exception ex)
            {
                Write("[ERROR] Create upd file " + ex.Message);
                FERROR = ex.Message;
            }
        }
        return bRet;
    }

    public int CreateGFSFile(StringBuilder sb, string FilePathName, string CT, string strBatchID, string siteName, string strVersion, string strDN, OracleConnection conn)
    {
        int iRet = 0;
        string strHead = string.Empty;
        string strSql = string.Empty;
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
                    iRet = 0;
                }

                if (iRet == 0)
                {

                    strSql = @"UPDATE SHP.UPD_DATALOAD_DETAIL_T SET GFS_FILE_TIME=SYSDATE WHERE INVOICE IN ('" + strDN + "')";
                    try
                    {
                        iRet = CheckConnect(conn);
                        if (iRet == 0)
                        {
                            iRet = ExecSqlNoQuery(strSql, conn);
                        }
                    }
                    catch (Exception ex)
                    {
                        iRet = -1;
                        FERROR = ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {
                iRet = -1;
                FERROR = ex.Message;
            }
        }
        return iRet;
    }

    public int UpdateMasterStatus(string DN, string UpdateValue,string PO,string Qty, int intStatus, OracleConnection conn)
    {
        int iRet = 0;
        string strSql = string.Empty;
        System.Data.DataTable SelDt = new System.Data.DataTable();
        strSql = "SELECT * FROM PUBLIB.UPD_PODN_LIST_T WHERE INVOICE='"+DN+"'";
        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = SelectSql(strSql, conn, ref SelDt);
                if (SelDt.Rows.Count > 0)
                {
                    switch (intStatus)
                    {
                        case 1:
                            strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET UPDFILENAME=:V_TRA WHERE INVOICE=:V_DN";
                            break;
                        case 2:
                            strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET IMEIFILENAME=:V_TRA WHERE INVOICE=:V_DN";
                            break;
                        case 3:
                            strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET GFSFILENAME=:V_TRA WHERE INVOICE=:V_DN";
                            break;
                        case 4:
                            strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET UPDFLAG=:V_TRA,TRY_TIMES=TO_CHAR(cast(DECODE(TRY_TIMES,'',0,TRY_TIMES) as VARCHAR(5))+1),CREATE_DATE=SYSDATE WHERE INVOICE=:V_DN";
                            break;
                        default:
                            strSql = "";
                            break;
                    }
                    if (strSql.Length > 0)
                    {
                        OracleParameter[] Parms = new OracleParameter[2];
                        Parms[0] = new OracleParameter("V_TRA", UpdateValue);
                        Parms[1] = new OracleParameter("V_DN", DN);
                        iRet = CheckConnect(conn);
                        if (iRet == 0)
                        {
                            iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);
                        }
                    }
                }
                else
                {

                    strSql = "INSERT INTO PUBLIB.UPD_PODN_LIST_T(INVOICE,PO,SHIP_QTY,UPDFILENAME) VALUES(:V_DN,:V_PO,:V_QTY,:V_FILE)";
                    OracleParameter[] Parms = new OracleParameter[4];
                    Parms[0] = new OracleParameter("V_DN", DN);
                    Parms[1] = new OracleParameter("V_PO", PO);
                    Parms[2] = new OracleParameter("V_QTY", Qty);
                    Parms[3] = new OracleParameter("V_FILE", UpdateValue);
                    iRet = CheckConnect(conn);
                    if (iRet == 0)
                    {
                        iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);
                    }                    
                }
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public int UpdateMasterDnModel(string DN, string strModel, string strModelType, OracleConnection conn)
    {
        int iRet = 0;
        string strSql = string.Empty;
        strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET SFCMODEL=:V_SMODEL,MODELTYPE=:V_MTYPE WHERE INVOICE=:V_DN";
        OracleParameter[] Parms = new OracleParameter[3];
        Parms[0] = new OracleParameter("V_SMODEL", strModel);
        Parms[1] = new OracleParameter("V_MTYPE", strModelType);
        Parms[2] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private bool CheckFieldValue(string Invoice, string ModelName, string FieldValue, string Fieldimei, int rownum, int FieldIndex, ref string ErrorMessage, OracleConnection conn)
    {
        string errorMessagex = string.Empty;
        switch (FieldIndex)
        {

            case 4: if (FieldValue == "N/A")
                {
                    errorMessagex = "Generation Date  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 5: if (FieldValue == "")
                {
                    errorMessagex = "APC  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                } break;
            case 6: if (FieldValue == "")
                {
                    errorMessagex = "TransceiverModel  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 7: if (FieldValue == "")
                {
                    errorMessagex = "CustomerModel  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 8: if (FieldValue == "")
                {
                    errorMessagex = "MarketName  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 9: if (FieldValue == "")
                {
                    errorMessagex = "ItemCode  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 11: if (FieldValue == "" || FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "ShipDate  is  Null or is 'N/A'";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 12: if (FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "Ship-to Customer Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 13: if (FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "ship-to Customer Address ID  is  Null or is 'N/A' ";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 14: if (FieldValue == "" || FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "ShipToCustomerName  is  Null or is 'N/A'";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 15: if (FieldValue == "")
                {
                    errorMessagex = "ShipToCity  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 16: if (FieldValue == "")
                {
                    errorMessagex = "ShipToCountry  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            case 18: if (FieldValue == "N/A")
                {
                    errorMessagex = "Sold-to Customer Name  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 20: if (FieldValue == "")
                {
                    errorMessagex = "Track ID  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 23: if (FieldValue == "")
                {
                    errorMessagex = "Po Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 24: if (FieldValue == "")
                {
                    errorMessagex = "So Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 26: //NKEY
                if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
                {
                    errorMessagex = "NetworkUnlockCode  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            case 29: //NSKEY
                if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
                {
                    errorMessagex = "SIMUnlockCode  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            //case 30: //PRIVILEGEPWD
            //    if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
            //    {
            //        errorMessagex = "ServicePasscode  is  Null";
            //        InserterrorMessage(Invoice, errorMessagex, Fieldimei);
            //        ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
            //        return false;
            //    }
            //    break;

            case 35: if (FieldValue == "")
                {
                    errorMessagex = "MSN  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                } break;
            case 56: if (FieldValue == "")
                {
                    errorMessagex = "Shipment Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei, conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            default:
                return true;

        }
        return true;

    }

    public int InsertResult(string strUPDfile, string DN, string CustomerName, string strSapModel, string Country, string Qty, string strShipDate, int Countrep, OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        string strRepSql = string.Empty;
        DateTime ShipDate = Convert.ToDateTime(strShipDate);
        OracleParameter[] Parms = null;
        OracleParameter[] ParmsRep = null;
        if (Countrep == 1)
        {
            strSql = @"INSERT INTO SHP.UPD_DATAlOAD_DETAIL_T(INVOICE,SHIP_QTY,MODEL,SHIP_TO,CUSTOMER,SHIP_DATE,FILENAME,ACTION) " +
                                                 "VALUES(:V_DN,:V_QTY,:V_MODEL,:V_SHIP,:V_CUS,:V_SHIPDATE,:V_FILENAME,:V_ACT)";
            Parms = new OracleParameter[8];
            Parms[0] = new OracleParameter("V_DN", DN);
            Parms[1] = new OracleParameter("V_QTY", Qty);
            Parms[2] = new OracleParameter("V_MODEL", strSapModel);
            Parms[3] = new OracleParameter("V_SHIP", Country);
            Parms[4] = new OracleParameter("V_CUS", CustomerName);
            Parms[5] = new OracleParameter("V_SHIPDATE", ShipDate);
            Parms[6] = new OracleParameter("V_FILENAME", strUPDfile);
            Parms[7] = new OracleParameter("V_ACT", "NORMAL");
        }
        else
        {
            strSql = @"UPDATE SHP.UPD_DATALOAD_DETAIL_T SET COUNTRE=:V_CONT WHERE INVOICE=:V_DN";
            strRepSql = @"INSERT INTO SHP.UPD_REPSEND_DETAIL_T(INVOICE,SHIP_QTY,MODELX,SHIP_TO,CUSTOMER,SHIP_DATE,FILENAME,ACTION) " +
                                                 "VALUES(:V_DN,:V_QTY,:V_MODEL,:V_SHIP,:V_CUS,:V_SHIPDATE,:V_FILENAME,:V_ACT)";
            Parms = new OracleParameter[2];
            Parms[0] = new OracleParameter("V_CONT", Countrep);
            Parms[1] = new OracleParameter("V_DN", DN);

            ParmsRep = new OracleParameter[8];
            ParmsRep[0] = new OracleParameter("V_DN", DN);
            ParmsRep[1] = new OracleParameter("V_QTY", Qty);
            ParmsRep[2] = new OracleParameter("V_MODEL", strSapModel);
            ParmsRep[3] = new OracleParameter("V_SHIP", Country);
            ParmsRep[4] = new OracleParameter("V_CUS", CustomerName);
            ParmsRep[5] = new OracleParameter("V_SHIPDATE", ShipDate);
            ParmsRep[6] = new OracleParameter("V_FILENAME", strUPDfile);
            ParmsRep[7] = new OracleParameter("V_ACT", "NORMAL");

        }
        try
        {
            iRet = CheckConnect(conn);
            iRet = ExecSqlNoQueryWithParam(strSql, Parms,conn);
            if (Countrep > 1)
            {
                iRet = CheckConnect(conn);
                iRet = ExecSqlNoQueryWithParam(strRepSql, ParmsRep, conn);
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public int MailIMEI(string strKeyWord, string strFilePath, string strFileName, string strErrMsg, string strShipCode, string strstrmodule, string strstrshipcountry, OracleConnection conn)
    {
        int iRet;
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
        strTemp += strKeyWord + "文件每天手動產生";
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
        strSql = @"SELECT distinct  MAIL_ADDR FROM SHP.R_IMEI_COUNTRY_LIST A
                                        WHERE  A.SHIP_TO_CODE='" + strshipcode + "'";// CREATE_TIME>SYSDATE-1 
        try
        {
            iRet = CheckConnect(conn);
            using (OracleDataReader odr = GetDataReader(strSql, conn))
            {
                if (odr.Read())
                {
                    strSendto = odr[0].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        //mail.SendTo = strSendto;
        mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
        mail.SendCC = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
        mail.SendFrom = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
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
            iRet = CheckConnect(conn);
            iRet = ExecSqlNoQuery(strSql, conn);

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }

    public int MailGFS(string invoice, string qty, string customertype, string gfsfilename, string filename, OracleConnection conn)
    {
        int iRet = 0;
        string strSql = string.Empty;
        string strSendto = string.Empty;
        string strKeyWord = customertype + " GFS FILE";

        Mail mail = new Mail();
        mail.SendProgram = "QAZ GFS File";
        mail.CreateTime = DateTime.Now;
        mail.FinishedMark = "0";


        string strTemp = string.Empty;

        mail.NoteSubject = "(FIH-TJ) " + strKeyWord + "  Send Auto Notice:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        strTemp += "File Create OK\r\n";
        strTemp += "FileName : " + filename;

        strTemp += "\r\n";
        strTemp += "\r\n";
        strTemp += strKeyWord + "文件每天由系統自動發送";
        mail.NoteContent = strTemp;

        //   mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
        mail.SendTo = Xml.XmlGetValue("CONFIG", "GFSMAILINFO", "TO");
        mail.SendCC = Xml.XmlGetValue("CONFIG", "GFSMAILINFO", "CC");
        mail.SendFrom = Xml.XmlGetValue("CONFIG", "GFSMAILINFO", "FROM");

        strSql = @"INSERT INTO SFC.C_NOTES_SEND (
                                   NOTE_SUBJECT, NOTE_CONTENT, SEND_TO,SEND_CC, SEND_FROM, SEND_PROG, 
                                   CREATE_DATE, FINISHED_MARK,ATTACHED_PATH) 
                                   VALUES ( '" + mail.NoteSubject + "', '" + mail.NoteContent + "', '" + mail.SendTo + @"',
                                    '" + mail.SendCC + "', '" + mail.SendFrom + "', '" + mail.SendProgram + @"',
                                    TO_DATE('" + mail.CreateTime.ToString("yyyy/MM/dd HH:mm:ss") +
                     "','YYYY/MM/DD HH24:MI:SS'),  '" + mail.FinishedMark + "','" + gfsfilename + "')";
        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = ExecSqlNoQuery(strSql, conn);
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public int AddMail(string strKeyWord, string strFilePath, string strFileName, string strErrMsg, string strShipCode, OracleConnection conn)
    {
        int iRet;
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
        strTemp += strKeyWord + "文件每天手動產生！";
        mail.NoteContent = strTemp;

        if (strErrMsg.ToUpper().Equals("OK"))
        {
            mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
            mail.SendCC = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
            mail.SendFrom = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
        }
        else
        {
            mail.SendTo = Xml.XmlGetValue("CONFIG", "ERRORMAILINFO", "TO");
            mail.SendCC = Xml.XmlGetValue("CONFIG", "ERRORMAILINFO", "CC");
            mail.SendFrom = Xml.XmlGetValue("CONFIG", "ERRORMAILINFO", "FROM");
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
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = ExecSqlNoQuery(strSql, conn);
            }

        }

        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public int UpdateImeiTime(string DN, int sendCount, OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        if (sendCount == 1)
        {
            strSql = "UPDATE SHP.UPD_DATALOAD_DETAIL_T SET IMEI_FILE_TIME=SYSDATE WHERE INVOICE=:V_DN AND IMEI_FILE_TIME IS NULL";
        }
        else
        {
            strSql = "UPDATE SHP.UPD_REPSEND_DETAIL_T SET IMEI_FILE_TIME=SYSDATE WHERE INVOICE=:V_DN AND IMEI_FILE_TIME IS NULL";
        }
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public bool SaveUpdFileName(string strPo, string DN, string strQty, string strFileName)
    {
        bool bRet = false;
        int iRet;
            try
            {
                iRet = CheckConnect(connNormal);
                string[] strParams = new string[] { "TRANTYPE", "DN", "UPDFILE", "PO_NUMBER", "QTY", "RES" };
                string[] strValue = new string[] { "New", DN.Replace("'", ""), strFileName, strPo.Replace("'", ""), strQty };
                string[] strOut;
                bRet = ExecuteStoredProcedure("SHP.W_RASNFILEINFO_EDIT", strParams, strValue, out strOut);
            }
            catch (Exception ex)
            {
                Write("[ERROR] SaveUPDFileName" + ex.Message);
            }
        
        return bRet;
    }

    public int SetInvoiceStatus(string DN, OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        strSql = "UPDATE SHP.CMCS_SFC_SHIP_CARTON_MAP SET UPLOAD=:V_UP WHERE INVOICE_NO=:V_DN";
        OracleParameter[] Parms = new OracleParameter[2];
        Parms[0] = new OracleParameter("V_UP", "1");
        Parms[1] = new OracleParameter("V_DN", DN);

        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);
            }

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }

    public bool GrivwDisay(string DN, ref System.Data.DataTable dtTop, ref System.Data.DataTable dtList,OracleConnection conn)
    {

        bool bRet = false;
        string strsql = string.Empty;
        string strTwsql = string.Empty;
        strsql = @"SELECT INVOICE,SHIP_QTY,MODEL,SHIP_TO,CUSTOMER,SHIP_DATE,FILENAME,CREATE_DATE,ASN_FTP_TIME,IMEI_FILE_TIME,COUNTRE FROM SHP.UPD_DATALOAD_DETAIL_T WHERE INVOICE=:V_DN";
        strTwsql = @"SELECT INVOICE,PO,SHIP_QTY,UPDFILENAME,IMEIFILENAME,GFSFILENAME,UPDFLAG,SAPFLAG,CREATE_DATE,TRY_TIMES,SENDFLAG FROM PUBLIB.UPD_PODN_LIST_T WHERE INVOICE=:V_IN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);

        OracleParameter[] Parmstw = new OracleParameter[1];
        Parmstw[0] = new OracleParameter("V_IN", DN);
            try
            {
                using (System.Data.DataSet odr = GetDataSet(strsql, Parms,conn))
                {
                    if (odr.Tables[0].Rows.Count > 0)
                    {
                        dtTop = odr.Tables[0];
                        bRet = true;
                    }
                }
                using (DataSet odr = GetDataSet(strTwsql, Parmstw,conn))
                {
                    if (odr.Tables[0].Rows.Count > 0)
                    {
                        dtList = odr.Tables[0];
                    }
                }

            }
            catch (Exception ex)
            {
                Write("[ERROR] Query  " + ex.Message);
            }      
        return bRet;
    }

    public bool FtpFile(string strFilePath, string strFileName, string strFolder)
    {
        bool bRet = false;
        string strIPAddress = Xml.XmlGetValue("CONFIG", "FTP", "IPADDRESS");
        string strUserName = Xml.XmlGetValue("CONFIG", "FTP", "USERNAME");
        string strUserPWD = Xml.XmlGetValue("CONFIG", "FTP", "USERPWD");
        FTPClient ftp = new FTPClient(strIPAddress, "", strUserName, strUserPWD, 21);
        try
        {
            ftp.Connect();
            ftp.ChDir("" + strFolder);
            ftp.Put(strFilePath);
            bRet = true;
        }
        catch (Exception ex)
        {
            FERROR = ex.Message;

        }
        return bRet;
    }

    private static void KillExcelProcess(DateTime Process_BeforeTime, DateTime Process_AfterTime)
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

    public static void Write(string strlog)
    {
        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Log\\"))
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
        }
        string filename = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\GSMUPD" + DateTime.Now.ToString("yyyyMMdd") + ".log";
        System.IO.FileStream oFileStream;
        if (!System.IO.File.Exists(filename))
        {
            oFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
        }
        else
        {
            oFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
        }
        System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(oFileStream, System.Text.Encoding.GetEncoding("big5"));
        oStreamWriter.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " || " + strlog);
        oStreamWriter.Close();
        oFileStream.Close();
    }

    public static void Write(string strfilename, int iRow, string strErrorType, string strErrors)
    {
        Write("Error [" + strfilename + "] [" + iRow.ToString() + "] [" + strErrorType + "] [" + strErrors + "]");
    }

    public int GetFileName(string SQL, ref string[] UpdList, ref string[] ImeiList, ref string[] GfsList,OracleConnection conn)
    {
        int iRet = 0;
        System.Data.DataTable dtFile=new System.Data.DataTable();
        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = SelectSql(SQL, conn, ref dtFile);
                if (dtFile.Rows.Count > 0)
                {
                    UpdList = new string[dtFile.Rows.Count];
                    ImeiList = new string[dtFile.Rows.Count];
                    GfsList = new string[dtFile.Rows.Count];

                    for (int i = 0; i < dtFile.Rows.Count; i++)
                    {
                        UpdList[i] = dtFile.Rows[i]["UPDFILENAME"].ToString();
                        ImeiList[i] = dtFile.Rows[i]["IMEIFILENAME"].ToString();
                        GfsList[i] = dtFile.Rows[i]["GFSFILENAME"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }

    private int  DeleteTempFile()
    {
        int iRet;
        string[] tempFilePath = new string[2];
        tempFilePath[0] = AppDomain.CurrentDomain.BaseDirectory + "Upload\\UPD\\";
        tempFilePath[1] = AppDomain.CurrentDomain.BaseDirectory + "Upload\\IMEI\\";
        try
        {
            foreach (string tempPath in tempFilePath)
            {
                if (Directory.Exists(tempPath))
                {
                    string[] FileNameCount = Directory.GetFiles(tempPath);
                    if (FileNameCount.Length > 0)
                    {
                        foreach (string FN in FileNameCount)
                        {
                            File.Delete(FN);
                        }
                    }
                }
            }
            iRet = 0;
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }


   
     

}

/// <summary>
/// UPDAuotoUpLoad-Class
/// </summary>
public class AutoUpload
{

   
    private OracleConnection ConnReadConnect;
    private OracleConnection ConnWriteConnect;
    CreateFile cf = new CreateFile();
    public string FERROR;

    public AutoUpload()
    {
        FERROR = "";
    }

    private void GetConnect(string strDbReadConnectString,string strDbWriteConnectString)
    {
        ConnReadConnect = new OracleConnection(strDbReadConnectString);
        ConnWriteConnect = new OracleConnection(strDbWriteConnectString);

    }
    /* 取正式庫中的DN,PO,QTY插入 SFIS1.UPD_PODN_LIST_T中
                * 參數 
                * 0: 成功
                * -1::異常
                */
    public int GetDnWithInsert(string DbReadConnectString,string DbWriteConnectString)
    {
        int iRet=0;
        string strSql = string.Empty;
        string strTempDN = string.Empty;
        string strTempPO = string.Empty;
        string strTempQty = string.Empty;
        GetConnect(DbReadConnectString,DbWriteConnectString);
        try
        {
            #region/*GSM/WCDMA-幾種DN,PO,QTY,預計出貨日期*/

            strSql = "SELECT DISTINCT A.INVOICE_NUMBER, B.PO_NUMBER,C.SHIPPED_QTY,C.LAST_SHIPMENT_DATE " +
                                "FROM SAP.CMCS_SFC_PACKING_LINES_ALL A," +
                                     "SHP.UPD_ORDER_INFORMATION B," +
                                     "SFC.MES_MIT_INVOICE C " +
                               "WHERE A.CUSTOMER_PO = B.PO_NUMBER " +
                                     "AND C.INVOICE = A.INVOICE_NUMBER AND A.INTERNAL_CARTON IS NOT NULL AND LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-30 " +
                            "ORDER BY C.LAST_SHIPMENT_DATE DESC";

            using (OracleDataReader odr = GetDataReader(strSql, ConnReadConnect))
            {
                while (odr.Read())
                {
                    strTempDN = odr[0].ToString();
                    strTempPO = odr[1].ToString();
                    strTempQty = odr[2].ToString();
                    iRet = CheckDnList(strTempDN, strTempPO, strTempQty, ConnWriteConnect);
                }
            }
            #endregion

            #region/*CDMA幾種 DN,PO,QTY,預計出貨日期*/
            strSql = " SELECT DISTINCT A.INVOICE_NUMBER, A.CUSTOMER_PO,C.SHIPPED_QTY,C.LAST_SHIPMENT_DATE " + 
                                "FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,"+
                                     "SFC.MES_MIT_INVOICE C " +
                               "WHERE  A.INVOICE_NUMBER=C.INVOICE "+ 
                                     "AND A.INTERNAL_CARTON IS NOT NULL AND  C.ITEM_MODULE='FD1' AND  C.LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-30 "+
                            "ORDER BY C.LAST_SHIPMENT_DATE DESC";
            using (OracleDataReader odr = GetDataReader(strSql, ConnReadConnect))
            {
                while (odr.Read())
                {
                    strTempDN = odr[0].ToString();
                    strTempPO = odr[1].ToString();
                    strTempQty = odr[2].ToString();
                    iRet = CheckDnList(strTempDN, strTempPO, strTempQty, ConnWriteConnect);
                }
            }
            #endregion

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }


    public int GetDnWithInsertIntel(string DbReadConnectString, string DbWriteConnectString)
    {
        int iRet = 0;
        string strSql = string.Empty;
        string strTempDN = string.Empty;
        string strTempPO = string.Empty;
        string strTempQty = string.Empty;
        string strTempModel = string.Empty;
        GetConnect(DbReadConnectString, DbWriteConnectString);
        try
        {  
            #region/*GSM/WCDMA-幾種DN,PO,QTY,預計出貨日期*/
            string DNType = "";
            string strType = "Select customer,model from publib.intelmodel";
            System.Data.DataTable dtType = DataBaseOperation.SelectSQLDT("oracle", DbWriteConnectString, strType);


            for (int i = 0; i < dtType.Rows.Count; i++)
            {
                DNType += "'" + dtType.Rows[i][1].ToString() + "'";
                if (i != dtType.Rows.Count - 1) DNType += ",";

            }
            DNType = "(" + DNType + ")";
            strSql = "SELECT DISTINCT A.INVOICE_NUMBER, B.PO_NUMBER,C.SHIPPED_QTY,C.LAST_SHIPMENT_DATE ,C.ITEM_MODULE " +
                                "FROM SAP.CMCS_SFC_PACKING_LINES_ALL A," +
                                     "SHP.UPD_ORDER_INFORMATION B," +
                                     "SFC.MES_MIT_INVOICE C " +
                               "WHERE A.CUSTOMER_PO = B.PO_NUMBER " +
                                     "AND C.INVOICE = A.INVOICE_NUMBER " +
                                     //" and A.INVOICE_NUMBER='82181264'" +
                                      "AND C.ITEM_MODULE in " + DNType + 
                                     " AND LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-60 " +
                //"AND A.INTERNAL_CARTON IS NOT NULL AND C.ITEM_MODULE='BB2' AND LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-30 " +
                            "ORDER BY C.LAST_SHIPMENT_DATE DESC";

          
            using (OracleDataReader odr = GetDataReader(strSql, ConnReadConnect))
            {
                while (odr.Read())
                {
                    strTempDN = odr[0].ToString();
                    strTempPO = odr[1].ToString();
                    strTempQty = odr[2].ToString();
                    strTempModel = odr[4].ToString();
                    iRet = CheckDnListIntel(strTempDN, strTempPO, strTempQty, ConnWriteConnect, strTempModel);
                }
            }
            #endregion

            #region/*CDMA幾種 DN,PO,QTY,預計出貨日期*/
            strSql = " SELECT DISTINCT A.INVOICE_NUMBER, A.CUSTOMER_PO,C.SHIPPED_QTY,C.LAST_SHIPMENT_DATE,C.ITEM_MODULE " +
                                "FROM SAP.CMCS_SFC_PACKING_LINES_ALL A," +
                                     "SFC.MES_MIT_INVOICE C " +
                               "WHERE  A.INVOICE_NUMBER=C.INVOICE " +
                                     "AND A.INTERNAL_CARTON IS NOT NULL " +
               // " AND  C.ITEM_MODULE='FD1' +
                 "AND C.ITEM_MODULE in " + DNType + 
                " AND  C.LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-60 " +
                            "ORDER BY C.LAST_SHIPMENT_DATE DESC";
            using (OracleDataReader odr = GetDataReader(strSql, ConnReadConnect))
            {
                while (odr.Read())
                {
                    strTempDN = odr[0].ToString();
                    strTempPO = odr[1].ToString();
                    strTempQty = odr[2].ToString();
                    strTempModel = odr[4].ToString();
                    iRet = CheckDnListIntel(strTempDN, strTempPO, strTempQty, ConnWriteConnect, strTempModel);
                }
            }
            #endregion

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public int GetDnWithInsertCTL(string DbReadConnectString, string DbWriteConnectString,string Usertype)
    {
        int iRet = 0;
        string strSql = string.Empty;
        string strTempDN = string.Empty;
        string strTempPO = string.Empty;
        string strTempQty = string.Empty;
        string strTempModel = string.Empty;
        string datetimenow = DateTime.Now.ToString("yyyyMMdd");
        GetConnect(DbReadConnectString, DbWriteConnectString);
        try
        {
            #region/*GSM/WCDMA-幾種DN,PO,QTY,預計出貨日期*/
            string DNType = "";
            string strType = "Select customer,model,duedate from " + Usertype + ".CUSTOMERMODEL WHERE duedate IS NOT NULL AND TO_DATE(DUEDATE,'yyyyMMdd') >=  TO_DATE("+ datetimenow +",'yyyyMMdd') ";
            System.Data.DataTable dtType = DataBaseOperation.SelectSQLDT("oracle", DbWriteConnectString, strType);


            for (int i = 0; i < dtType.Rows.Count; i++)
            {
                DNType += "'" + dtType.Rows[i][1].ToString() + "'";
                if (i != dtType.Rows.Count - 1) DNType += ",";

            }
            DNType = "(" + DNType + ")";
            strSql = "SELECT DISTINCT A.INVOICE_NUMBER, B.PO_NUMBER,C.SHIPPED_QTY,C.LAST_SHIPMENT_DATE ,C.ITEM_MODULE " +
                                "FROM SAP.CMCS_SFC_PACKING_LINES_ALL A," +
                                     "SHP.UPD_ORDER_INFORMATION B," +
                                     "SFC.MES_MIT_INVOICE C " +
                               "WHERE A.CUSTOMER_PO = B.PO_NUMBER " +
                                     "AND C.INVOICE = A.INVOICE_NUMBER " +
                //" and A.INVOICE_NUMBER='82181264'" +
                                      "AND C.ITEM_MODULE in " + DNType +
                                     " AND LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-60 " +
                //"AND A.INTERNAL_CARTON IS NOT NULL AND C.ITEM_MODULE='BB2' AND LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-30 " +
                            "ORDER BY C.LAST_SHIPMENT_DATE DESC";


            using (OracleDataReader odr = GetDataReader(strSql, ConnReadConnect))
            {
                while (odr.Read())
                {
                    strTempDN = odr[0].ToString();
                    strTempPO = odr[1].ToString();
                    strTempQty = odr[2].ToString();
                    strTempModel = odr[4].ToString();
                    iRet = CheckDnListIntelByCustomer(strTempDN, strTempPO, strTempQty, ConnWriteConnect, strTempModel, Usertype);
                }
            }
            #endregion

            #region/*CDMA幾種 DN,PO,QTY,預計出貨日期*/
            strSql = " SELECT DISTINCT A.INVOICE_NUMBER, A.CUSTOMER_PO,C.SHIPPED_QTY,C.LAST_SHIPMENT_DATE,C.ITEM_MODULE " +
                                "FROM SAP.CMCS_SFC_PACKING_LINES_ALL A," +
                                     "SFC.MES_MIT_INVOICE C " +
                               "WHERE  A.INVOICE_NUMBER=C.INVOICE " +
                                     "AND A.INTERNAL_CARTON IS NOT NULL " +
                // " AND  C.ITEM_MODULE='FD1' +
                 "AND C.ITEM_MODULE in " + DNType +
                " AND  C.LAST_SHIPMENT_DATE >TO_DATE( TO_CHAR(SYSDATE,'yyyy/MM/dd'),'YYYY/MM/DD')-60 " +
                            "ORDER BY C.LAST_SHIPMENT_DATE DESC";
            using (OracleDataReader odr = GetDataReader(strSql, ConnReadConnect))
            {
                while (odr.Read())
                {
                    strTempDN = odr[0].ToString();
                    strTempPO = odr[1].ToString();
                    strTempQty = odr[2].ToString();
                    strTempModel = odr[4].ToString();
                    iRet = CheckDnListIntelByCustomer(strTempDN, strTempPO, strTempQty, ConnWriteConnect, strTempModel, Usertype);
                }
            }
            #endregion

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    /* 定時查詢UPD_PODN_LIST_T表中美有產生的DN,產生客戶需要的文件
                * 參數 
                * 0: 成功
                * -1::異常
                */
    public int GetCreateUpdFile(string DbreadConnectString, string DbWriteConnectString)
    {
        int iRet = 0;
        string strSql = string.Empty;
        string strTempDN = string.Empty;
        string strTempPo = string.Empty;
        #region /*檢查生成文件夾及實例化讀/寫數據庫連接*/
        CheckDirectory();
        GetConnect(DbreadConnectString, DbWriteConnectString);
        #endregion
        try
        {
            strSql = "SELECT INVOICE,PO FROM PUBLIB.UPD_PODN_LIST_T WHERE SAPFLAG IS NOT NULL AND UPDFLAG IS NULL AND DELIVERY_TIME IS NOT NULL";
            System.Data.DataTable dtDnPo = new System.Data.DataTable();
            iRet = SelectSql(strSql, ConnWriteConnect, ref dtDnPo);
            if (dtDnPo.Rows.Count > 0)
            {
                    for (int i = 0; i < dtDnPo.Rows.Count; i++)
                    {
                        strTempDN = dtDnPo.Rows[i][0].ToString();
                        strTempPo = dtDnPo.Rows[i][1].ToString();

                        //Test GSM_UPD
                        //strTempDN = "82128271";
                        //strTempPo = "PA227685";
                        //TestEnd

                        //Test CDMA_UPD
                        //strTempDN = "86006127";
                        //strTempPo = "PA224593";
                        //TestEnd
                        #region /*取得PO相關信息*/
                        string strInvoice = GetAllInvoiceNumberHanle(strTempDN,ConnReadConnect);
                        if (strInvoice.Length == 0)
                        {
                            continue;
                        }
                        CreateFile.Write("[Create Begining]" + strInvoice);
                        string[] GetInvoiceWith = strInvoice.Split('#');
                        string strPlant = GetInvoiceWith[0].ToString();
                        string strInvoiceNumber = GetInvoiceWith[1].ToString();
                        string strCustomerName = GetInvoiceWith[2].ToString();
                        string strProductModel = GetInvoiceWith[3].ToString();
                        string strShipCountry = GetInvoiceWith[4].ToString();
                        string strProductQty = GetInvoiceWith[5].ToString();
                        string strShipDate = GetInvoiceWith[6].ToString();
                        string strCustomerType = GetInvoiceWith[7].ToString();
                        #endregion

                        #region /*取得程式對應的ProCode*/
                        string strWitchCode = string.Empty;
                        string strSfcModel = string.Empty;
                        iRet= GetSfcModelType(strInvoiceNumber,ref strWitchCode,ref strSfcModel,ConnReadConnect);
                        if (iRet<0)
                        {
                            continue;
                        }
                        #endregion

                        #region /*判斷SHIP_TO_COUNTRY對應的ISO碼*/
                        iRet = IsoPresence(strInvoiceNumber,ConnReadConnect);
                        if (iRet <= 0)
                        {
                            FERROR = "Find Not Iso,Please Make Sure SHIP_TO_COUNTRY Correspond whith ISO";
                            InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                            continue;
                        }
                        #endregion

                        #region /*判斷是否是出拉美*/
                        string region = string.Empty;
                         iRet = regionPresence(strInvoiceNumber,ref region,ConnReadConnect);
                        #endregion

                         #region /*判斷是否出台灣*/
                         iRet = regionTW(strInvoiceNumber, ref region, ConnReadConnect);//修改于2012-05-18
                         #endregion

                         #region /*判斷出貨前是否已經維護PO*/
                         if (strWitchCode != "UPD06")
                         {
                             string strCheckPo = CheckPo(strInvoiceNumber, ConnReadConnect);
                             if (strCheckPo.Length == 0)
                             {
                                 FERROR = "No have maintain PO";
                                 InserterrorMessage(strInvoiceNumber, FERROR, "", ConnWriteConnect);
                                 continue;
                             }
                         }
                         #endregion

                        #region/*判斷程式是否已經產生過文件*/
                         int sendCount = 1;
                        string strRepeat = CheckRepeat(strInvoiceNumber,ConnWriteConnect);
                        string[] repeat = strRepeat.Split('#');
                        if (strRepeat.Length != 0)
                        {
                            sendCount = Convert.ToInt32(repeat[1]) + 1;
                            FERROR = "DN have already Create this is repeat";
                            InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                        }
                         #endregion

                        #region/*從數據庫中抓取相關DN信息*/
                        List<StringBuilder> lsb = null;
                        string SfcQty = string.Empty;
                        lsb = GetUpdData(strInvoiceNumber, strProductModel, strWitchCode, strProductQty, ref SfcQty, ref FERROR,ConnReadConnect,ConnWriteConnect);
                        if (strProductQty != SfcQty)
                        {
                            FERROR = "sap ship Qty is not equal Db Qty";
                            InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                            continue;
                        }
                        if (lsb.Count == 0)
                        {
                            if (FERROR.Length > 0)
                            {
                                InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                                continue;
                            }
                            FERROR = "No Query Data,Please Information IT Dep";
                            InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                            continue;
                        }
                        #endregion


                        #region CREATE GFS FILE
                        List<StringBuilder> msb = null;
                        //List<StringBuilder> lsb = GetInvoiceInfo(strInvoiceNumber);
                        //msb.Clear();

                        msb = GetGFSNEW(strInvoiceNumber, ConnReadConnect);

                        if (strWitchCode != "UPD03" && strWitchCode!="UPD06")
                        {
                            for (int k = 0; k < lsb.Count; k++)
                            {
                                DateTime dt = DateTime.Now;
                                string strGFSFileName = string.Empty;
                                System.Random a = new Random(System.DateTime.Now.Millisecond);
                                string strQty = msb.Count.ToString();
                                int RandKey = a.Next(100);
                                string strRand = RandKey.ToString();
                                strRand = strRand.PadLeft(3, '0');
                                strGFSFileName = string.Format(CreateFile.GFS_FILE_NAME, dt.ToString("yyyyMMddHHmmss"), strInvoiceNumber.Replace("'", ""), strRand);
                                string[] strTmp = strGFSFileName.Split('\\');
                                string strFileName = strTmp[strTmp.Length - 1];
                                //string strBKGFSFileName = Application.StartupPath + @"\GFS\" + GetFileName(strGFSFileName);82199174
                                if (!CreateGFSFile1(msb[k], strGFSFileName, dt.ToString("yyyyMMddHHmmss"), strInvoiceNumber.Replace("'", "") + "-" + strRand, "FIHTJ", "V01", strInvoiceNumber, ConnReadConnect))
                                {
                                    FERROR = "GFS FILE Create Error";
                                    InserterrorMessage(strInvoiceNumber, FERROR, "", ConnWriteConnect);
                                    continue;
                                }


                            }
                        }

                        #endregion




                        /*UPD文件產生*/
                        for (int j = 0; j < lsb.Count; j++)
                        {

                            if (strWitchCode == "UPD06")
                            {
                                #region /*生成CDMA-UPD文件*/
                                string strUPDFileName = string.Empty;
                                strUPDFileName = string.Format(CreateFile.CDMA_UPD_FILE_NAME, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"), j + 1);
                                string[] strTmp = strUPDFileName.Split('\\');
                                string strFileName = strTmp[strTmp.Length - 1];
                                if (!cf.CreateUpdFile(lsb[j], strUPDFileName))
                                {
                                    FERROR = "UPD Create Error";
                                    InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                                    continue;
                                }
                                #endregion

                                //File.Move(strUPDFileName, CreateFile.CMD_UPD_FILE_BAK + "\\" + strFileName);
                                InsertResult(strFileName, strInvoiceNumber, strCustomerName, strProductModel, strShipCountry, strProductQty, strShipDate, sendCount,ConnWriteConnect);
                                iRet = UpdateMasterStatus(strInvoiceNumber, strFileName, 1, ConnWriteConnect);

                                #region /*取數據庫GFS文件需要的數據*/
                                lsb.Clear();
                                lsb = GetGFS(strInvoiceNumber,ConnReadConnect);
                                if (lsb.Count == 0)
                                {
                                    File.Delete(strUPDFileName);
                                    FERROR = strInvoiceNumber + ":GFS No Have Data";
                                    InserterrorMessage(strInvoiceNumber, FERROR, "", ConnWriteConnect);
                                    continue;
                                }
                                #endregion

                                #region /*生成GFS文件及更新數據庫*/
                                for (int k = 0; k < lsb.Count; k++)
                                {
                                    DateTime dt = DateTime.Now;
                                    string strGFSFileName = string.Empty;
                                    System.Random a = new Random(System.DateTime.Now.Millisecond);
                                    string strQty = lsb.Count.ToString();
                                    int RandKey = a.Next(100);
                                    string strRand = RandKey.ToString();
                                    strRand = strRand.PadLeft(3, '0');
                                    strGFSFileName = string.Format(CreateFile.GFS_FILE_NAME, dt.ToString("yyyyMMddHHmmss"), strInvoiceNumber.Replace("'", ""), strRand);
                                    strTmp = strGFSFileName.Split('\\');
                                    strFileName = strTmp[strTmp.Length - 1];
                                   
                                    iRet = CreateGFSFile(lsb[k], strGFSFileName, dt.ToString("yyyyMMddHHmmss"), strInvoiceNumber.Replace("'", "") + "-" + strRand, "FIHTJ", "V01", strInvoiceNumber, ConnWriteConnect);
                                    if (iRet < 0)
                                    {
                                        AddMail(" GFS FILE", strGFSFileName, strFileName, "Create GFS File Error", "",ConnWriteConnect);
                                        File.Delete(strUPDFileName);
                                        FERROR = GetError();
                                        InserterrorMessage(strInvoiceNumber, FERROR, "", ConnWriteConnect);
                                        continue;                              
                                    }
                                    //File.Move(strGFSFileName, CreateFile.GFS_FILE_BAK + "\\" + strFileName);
                                    iRet = UpdateMasterStatus(strInvoiceNumber, strFileName, 3, ConnWriteConnect);

                                    iRet = MailGFS(strInvoiceNumber, strQty, "", CreateFile.GFS_FILE_BAK + "\\" + strFileName, strFileName, ConnWriteConnect);
                                    if (iRet > 0)
                                    {
                                        iRet = UpdateMasterStatus(strInvoiceNumber, "Y", 4, ConnWriteConnect);
                                        iRet = UpdateMasterDnModel(strInvoiceNumber, strSfcModel, strWitchCode, ConnWriteConnect);
                                    }
                                }
                                #endregion
                            }
                            else
                            {



                                #region /*生成GSM-UPD文件*/
                                string strUPDFileName = string.Empty;
                                string strIMEIFileName = string.Empty;
                                string strASNFileName = string.Empty;
                                strUPDFileName = string.Format(CreateFile.UPD_FILE_NAME, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"), j + 1);
                                string[] strTmp = strUPDFileName.Split('\\');
                                string strFileName = strTmp[strTmp.Length - 1];
                                string strtempFileName = strFileName;
                                if (!cf.CreateUpdFile(lsb[j], strUPDFileName))
                                {
                                    FERROR = "UPD Create Error";
                                    InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                                    continue;
                                }
                                #endregion

                                //File.Move(strUPDFileName, CreateFile.UPD_FILE_BAK + "\\" + strFileName);
                                InsertResult(strFileName, strInvoiceNumber, strCustomerName, strProductModel, strShipCountry, strProductQty, strShipDate, sendCount,ConnWriteConnect);
                                iRet = UpdateMasterStatus(strInvoiceNumber, strFileName, 1, ConnWriteConnect);

                                #region/*根據DN,SAPMODEL在SHP.UPD_COUNTRY_LINK_IMEICODE 取得相應的IMEICODE*/
                                string ImeiCode = string.Empty;
                                iRet = GetImeiCode(region, strProductModel, ref ImeiCode,ConnReadConnect);
                                if (iRet<0) 
                                {
                                    FERROR = cf.GetFerror();
                                    InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                                    continue;
                                }
                                #endregion

                                #region/*生成IMEI文件*/
                                //strIMEIFileName = cf.CreateIMEIFile(strInvoiceNumber, strProductModel, strShipCountry, region);
                                strIMEIFileName = CreateIMEIFile(strInvoiceNumber, strProductModel, region,ImeiCode, strShipCountry, ConnReadConnect,ConnWriteConnect);
                                strTmp=strIMEIFileName.Split('\\');
                                strFileName = strTmp[strTmp.Length - 1];
                                if (strIMEIFileName.Equals("-"))
                                {
                                    File.Delete(strUPDFileName);
                                    FERROR = strInvoiceNumber + ":IMEI Create Error";
                                    InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                                    continue;
                                }
                                else
                                {
                                    UpdateImeiTime(strInvoiceNumber, sendCount,ConnWriteConnect);
                                    iRet = UpdateMasterStatus(strInvoiceNumber, strFileName, 2, ConnWriteConnect);

                                }
                                #endregion

                                strTmp = strIMEIFileName.Split('\\');
                                strFileName = strTmp[strTmp.Length - 1];
                                string tempImeiFile = strFileName;
                                //File.Move(strIMEIFileName, CreateFile.IMEI_FILE_BAK + "\\" + strFileName);

                                #region/*發送成功或失敗Mail*/
                                iRet = MailIMEI("IMEI FILE Invoice : " + strInvoiceNumber, CreateFile.IMEI_FILE_BAK + "\\" + strFileName, strFileName, "", strInvoiceNumber, strProductModel, strShipCountry, ConnWriteConnect);
                                if (iRet<0)
                                {
                                    File.Delete(strUPDFileName); // delete bad file
                                    File.Delete(strIMEIFileName);
                                    FERROR = strInvoiceNumber + " :Mail IMEI file error";
                                    InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                                    continue;
                                }
                                #endregion

                                #region/*更新已經生成文件的數據庫狀態*/
                                iRet = SetInvoiceStatus(strInvoiceNumber, ConnWriteConnect);
                                if (iRet < 0)
                                {
                                    FERROR = strInvoiceNumber + " :Update Invoice Failed";
                                    InserterrorMessage(strInvoiceNumber, FERROR, "",ConnWriteConnect);
                                    continue;
                                }
                                #endregion

                                #region/*發送成功Mail提醒*/
                                iRet =AddMail("UPD FILE Invoice : " + strInvoiceNumber, CreateFile.UPD_FILE_BAK + "\\" + strFileName, strFileName, "OK", "",ConnWriteConnect);
                                if (iRet>0)
                                {
                                    iRet = UpdateMasterStatus(strInvoiceNumber, "Y", 4, ConnWriteConnect);
                                    iRet = UpdateMasterDnModel(strInvoiceNumber, strSfcModel, strWitchCode, ConnWriteConnect);
                                }
                                #endregion
                            }

                        }
                    }


            }

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    /* 查詢表中是否有相同的記錄,沒有則插入
                * 參數 
                * 1: 成功插入
                * 2: 語句重複
                * 3: 程序異常  
                */
    private int CheckDnList(string DN, string PO,string Qty,OracleConnection conn)
    {
        int iRet=0;
        string strSql = string.Empty;
        string strInsert = string.Empty;
        try
        {

            System.Data.DataTable dtDnPoList = new System.Data.DataTable();
            strSql = "SELECT * FROM  PUBLIB.UPD_PODN_LIST_T WHERE INVOICE='" + DN + "' AND ROWNUM=1";
            iRet = SelectSql(strSql, conn, ref dtDnPoList);
            if (dtDnPoList.Rows.Count == 0)
            {
                strInsert = "INSERT INTO PUBLIB.UPD_PODN_LIST_T(INVOICE,PO,SHIP_QTY,UPDFILENAME,IMEIFILENAME,GFSFILENAME) VALUES(:V_DN,:V_PO,:V_QTY,:V_UPDFILE,:V_IMEIFILE,:V_GFSFILE)";
                OracleParameter[] Parms = new OracleParameter[6];
                Parms[0] = new OracleParameter("V_DN", DN);
                Parms[1] = new OracleParameter("V_PO", PO);
                Parms[2] = new OracleParameter("V_QTY", Qty);
                Parms[3] = new OracleParameter("V_UPDFILE", DN);
                Parms[4] = new OracleParameter("V_IMEIFILE", DN);
                Parms[5] = new OracleParameter("V_GFSFILE", DN);
                iRet = ExecSqlNoQueryWithParam(strInsert, Parms,conn);

            }
            else
            {
                iRet = 2;
            }

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }
    /* 查詢表中是否有相同的記錄,沒有則插入 Intel
                * 參數 
                * 1: 成功插入
                * 2: 語句重複
                * 3: 程序異常  
                */
    private int CheckDnListIntelByCustomer(string DN, string PO, string Qty, OracleConnection conn, string model, string Usertype)
    {
        int iRet = 0;
        string strSql = string.Empty;
        string strInsert = string.Empty;
        try
        {

            System.Data.DataTable dtDnPoList = new System.Data.DataTable();
            strSql = "SELECT * FROM  "+ Usertype +".UPD_PODN_LIST_T WHERE INVOICE='" + DN + "' AND ROWNUM=1";
            iRet = SelectSql(strSql, conn, ref dtDnPoList);
            if (dtDnPoList.Rows.Count == 0)
            {
                strInsert = "INSERT INTO " + Usertype + ".UPD_PODN_LIST_T(INVOICE,PO,SHIP_QTY,UPDFILENAME,IMEIFILENAME,GFSFILENAME,SFCMODEL,MODELTYPE) VALUES(:V_DN,:V_PO,:V_QTY,:V_UPDFILE,:V_IMEIFILE,:V_GFSFILE,:V_MODEL,:V_MODELTYPE)";
                OracleParameter[] Parms = new OracleParameter[8];
                Parms[0] = new OracleParameter("V_DN", DN);
                Parms[1] = new OracleParameter("V_PO", PO);
                Parms[2] = new OracleParameter("V_QTY", Qty);
                Parms[3] = new OracleParameter("V_UPDFILE", DN);
                Parms[4] = new OracleParameter("V_IMEIFILE", DN);
                Parms[5] = new OracleParameter("V_GFSFILE", DN);
                Parms[6] = new OracleParameter("V_MODEL", model);
                Parms[7] = new OracleParameter("V_MODELTYPE", model);
                iRet = ExecSqlNoQueryWithParam(strInsert, Parms, conn);

            }
            else
            {
                iRet = 2;
            }

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }

    private int CheckDnListIntel(string DN, string PO, string Qty, OracleConnection conn, string model)
    {
        int iRet = 0;
        string strSql = string.Empty;
        string strInsert = string.Empty;
        try
        {

            System.Data.DataTable dtDnPoList = new System.Data.DataTable();
            strSql = "SELECT * FROM  PUBLIB.UPD_PODN_LIST_T WHERE INVOICE='" + DN + "' AND ROWNUM=1";
            iRet = SelectSql(strSql, conn, ref dtDnPoList);
            if (dtDnPoList.Rows.Count == 0)
            {
                strInsert = "INSERT INTO PUBLIB.UPD_PODN_LIST_T(INVOICE,PO,SHIP_QTY,UPDFILENAME,IMEIFILENAME,GFSFILENAME,SFCMODEL,MODELTYPE) VALUES(:V_DN,:V_PO,:V_QTY,:V_UPDFILE,:V_IMEIFILE,:V_GFSFILE,:V_MODEL,:V_MODELTYPE)";
                OracleParameter[] Parms = new OracleParameter[8];
                Parms[0] = new OracleParameter("V_DN", DN);
                Parms[1] = new OracleParameter("V_PO", PO);
                Parms[2] = new OracleParameter("V_QTY", Qty);
                Parms[3] = new OracleParameter("V_UPDFILE", DN);
                Parms[4] = new OracleParameter("V_IMEIFILE", DN);
                Parms[5] = new OracleParameter("V_GFSFILE", DN);
                Parms[6] = new OracleParameter("V_MODEL", model);
                Parms[7] = new OracleParameter("V_MODELTYPE", model);
                iRet = ExecSqlNoQueryWithParam(strInsert, Parms, conn);

            }
            else
            {
                iRet = 2;
            }

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }

    private void CheckDirectory()
    {
        //if (!Directory.Exists(CreateFile.IMEI_FILE_BAK))
        //{
        //    Directory.CreateDirectory(CreateFile.IMEI_FILE_BAK);
        //}
        //if (!Directory.Exists(CreateFile.ASN_FILE_BAK))
        //{
        //    Directory.CreateDirectory(CreateFile.ASN_FILE_BAK);
        //}
        //if (!Directory.Exists(CreateFile.UPD_FILE_BAK))
        //{
        //    Directory.CreateDirectory(CreateFile.UPD_FILE_BAK);
        //}
        //if (!Directory.Exists(CreateFile.CMD_UPD_FILE_BAK))
        //{
        //    Directory.CreateDirectory(CreateFile.CMD_UPD_FILE_BAK);
        //}
        //if (!Directory.Exists(CreateFile.GFS_FILE_BAK))
        //{
        //    Directory.CreateDirectory(CreateFile.GFS_FILE_BAK);
        //}
        for (int i = 0; i < CreateFile.DIRECTORY_ARRY.Length; i++)
        {
            if (!Directory.Exists(CreateFile.DIRECTORY_ARRY[i]))
            {
                Directory.CreateDirectory(CreateFile.DIRECTORY_ARRY[i]);
            }

        }
        for (int j = 0; j < CreateFile.DIRECTORY_ARRY_NEW.Length; j++)
        {
            if (!Directory.Exists(CreateFile.DIRECTORY_ARRY_NEW[j]))
            {
                Directory.CreateDirectory(CreateFile.DIRECTORY_ARRY_NEW[j]);
            }
        }
    }

    private  string GetAllInvoiceNumberHanle(string DN,OracleConnection conn)
    {
        string strRet = string.Empty;
        string strInvoice = string.Empty;
        string strSql = string.Empty;
        strSql = @"SELECT DISTINCT A.PLANT,
                                A.INVOICE,
                                A.CUSTOMER_NAME,
                                A.ITEM_MODULE,
                                UPPER (b.SHIP_TO_COUNTRY) AS SHIP_TO_COUNTRY,
                                (SELECT SUM (quantity)
                                   FROM CMCS_SFC_PACKING_LINES_ALL b
                                  WHERE b.INVOICE_NUMBER = a.invoice)
                                   AS SHIPPED_QTY,
                                LAST_SHIPMENT_DATE,
                                C.CUSTOMER_TYPE
                  FROM SFC.MES_MIT_INVOICE A,
                       SAP.CMCS_SFC_PACKING_LINES_ALL B,
                       SFC.CMCS_SFC_MODEL C
                 WHERE A.Invoice = B.INVOICE_NUMBER
                       AND A.ITEM_MODULE = C.MODEL
                       AND INVOICE ='" + DN + "'";
        try
        {
            CheckConnect(conn);
            using (OracleDataReader odr = GetDataReader(strSql,conn))
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
            FERROR = ex.Message;
        }
        return strRet;
    }

    private int  GetSfcModelType(string DN,ref string strWitchCode, ref string strSfcModel,OracleConnection conn)
    {

        int iRet=0;
        string strSql = "SELECT C.PRGCODE AS SWITCHCODE,C.MODEL AS MODEL FROM " +
                                            "SAP.SAP_INVOICE_INFO A," +
                                            "SHP.ROS_TCH_PN B," +
                                            "SFC.CMCS_SFC_MODEL C " +
                                            "WHERE A.ORDER_ITEM=B.PPART " +
                                            "AND B.TYPE_CODE=C.MODEL " +
                                            "AND A.INVOICE=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                strWitchCode = odr[0].ToString();
                strSfcModel = odr[1].ToString();
                break;
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message.ToString();
        }
        return iRet;
    }

    private int IsoPresence(string DN,OracleConnection conn)
    {
        int iRet = 0;
        string strSql = @"SELECT COUNT(B.COUNTRY_CODE) AS COUNTRYCOUNT FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SAP.COUNTRY_ISO_LINK B" +
                                                                       " WHERE (A.SHIP_TO_COUNTRY=B.DESCRIPTION or A.SHIP_TO_COUNTRY=B.CMCSDESCRIPTION)" +
                                                                       " AND A.INVOICE_NUMBER=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        iRet = CheckConnect(conn);
        try
        {
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                iRet = Convert.ToInt32(odr[0].ToString());
                break;
            }
        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        return iRet;
    }

    private int regionPresence(string DN,ref string region,OracleConnection conn)
    {
        int iRet = 0;
        string strSql = @"SELECT COUNT(B.COUNTRY_CODE) AS COUNTRYCOUNT FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SAP.COUNTRY_ISO_LINK B" +
                                                                    " WHERE (A.SHIP_TO_COUNTRY=B.DESCRIPTION or A.SHIP_TO_COUNTRY=B.CMCSDESCRIPTION)" +
                                                                    " AND A.INVOICE_NUMBER=:V_DN and B.REGION='LA'";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            using (OracleDataReader odr = GetDataReader(strSql, Parms,conn))
            {
                while (odr.Read())
                {
                    iRet = Convert.ToInt32(odr[0].ToString());
                    break;
                }
                if (iRet > 0)
                {
                    region = "LA";
                }
            }
        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }

        return iRet;
    }

    public int regionTW(string DN, ref string region, OracleConnection conn)
    {
        int iRet = 0;
        string strSql = @"SELECT COUNT(B.COUNTRY_CODE) AS COUNTRYCOUNT FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SAP.COUNTRY_ISO_LINK B" +
                                                                    " WHERE (A.SHIP_TO_COUNTRY=B.DESCRIPTION or A.SHIP_TO_COUNTRY=B.CMCSDESCRIPTION)" +
                                                                    " AND A.INVOICE_NUMBER=:V_DN and B.REGION='TW'";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            using (OracleDataReader odr = GetDataReader(strSql, Parms, conn))
            {
                while (odr.Read())
                {
                    iRet = Convert.ToInt32(odr[0].ToString());
                    break;
                }
                if (iRet > 0)
                {
                    region = "TW";
                }
            }
        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }

        return iRet;
    }

    private string CheckPo(string DN,OracleConnection conn)
    {
        string strRet = string.Empty;
        int iRet;
        string strSql = @"SELECT DISTINCT B.PO_NUMBER FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SHP.UPD_ORDER_INFORMATION B" +
                                                            " WHERE A.CUSTOMER_PO=B.PO_NUMBER AND A.INVOICE_NUMBER=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                strRet = odr[0].ToString();
                break;
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message.ToString();
        }
        return strRet;
    }

    private string CheckRepeat(string DN,OracleConnection conn)
    {
        string strRet = string.Empty;
        int iRet;
        string strSql = string.Empty;
        strSql = @"SELECT INVOICE,DECODE(COUNTRE,NULL,1,COUNTRE) AS CS FROM SHP.UPD_DATAlOAD_DETAIL_T WHERE ROWNUM<=1 AND INVOICE=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                strRet = "" + odr[0].ToString() + "#" + odr[1].ToString() + "";
                break;
            }

        }
        catch (Exception ex)
        {
            iRet=-1;
            FERROR = ex.Message.ToString();
        }
        return strRet;
    }

    private int InserterrorMessage(string strDN, string Message, string strimei,OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        iRet=CheckConnect(conn);
        strSql = "INSERT INTO SHP.UPD_INVOICE_HAND_TEMP(INVOICE,MESSAGE,IMEI) " +
                           "VALUES(:V_DN,:V_MES,:V_IMEI)";
        OracleParameter[] Parms = new OracleParameter[3];
        Parms[0] = new OracleParameter("V_DN", strDN);
        Parms[1] = new OracleParameter("V_MES", Message);
        Parms[2] = new OracleParameter("V_IMEI", strimei);

        try
        {
            iRet = ExecSqlNoQueryWithParam(strSql, Parms,conn);
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    public List<StringBuilder> GetGFSNEW(string strInvoice, OracleConnection conn)
    {
        List<StringBuilder> lsb = new List<StringBuilder>();
        string strSql = string.Empty;
        int iRet;
        strSql = @"SELECT 'UN' F1,'GS' F2,rownum F3,C.CUSTOMER_NUM F4,C.SERIAL_NUM F5,
                C.BTADDRESS F6,'' F7,'' F8,'' F9,
                '' F10,'' F11,'' F12,'' F13,
                '' F14,
                '00000000' F15,c.IMEINUM F16,b.P_UC F17, S_UC F18,c.WIFI_ADDRESS F19,
                '00000000' F20,F.software_ver F21,'' F22,D.MARKET_NAME F23,D.PHONE_MODEL F24,
                A.INTERNAL_CARTON F25,LPAD('FOUR_S',20,'0') F26,TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') F27,
                TO_CHAR(A.LAST_UPDATE_DATE,'YYYYMMDDHH24MISS') F28
                FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,WCDMA_TSE.R_FUNCTION_HEAD_T B,SHP.CMCS_SFC_IMEINUM C,SHP.ROS_TCH_PN D,
                SHP.CMCS_SFC_SHIPPING_DATA E,shp.cmcs_sfc_porder F WHERE A.INTERNAL_CARTON=E.CARTON_NO AND E.IMEI=C.IMEINUM AND
                b.SERIAL_NUMBER=e.PRODUCTID and C.PPART=D.PPART AND C.porder = F.porder
                AND A.INVOICE_NUMBER=:V_DN ";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", strInvoice);

        try
        {
            iRet = CheckConnect(conn);
            using (DataSet odr = GetDataSet(strSql, Parms, conn))
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

                    if ((sbupd.Length + sbs.Length) <= CreateFile.SINGLE_UPD_FILE_MAX_LENGTH)
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
            iRet = -1;
            FERROR = ex.Message;
        }
        return lsb;
    }

    private string GetFilePath(string strFileFullName)
    {
        return strFileFullName.Substring(0, strFileFullName.LastIndexOf(@"\"));
    }

    public string GetFileName(string strFileFullName)
    {
        return strFileFullName.Substring(strFileFullName.LastIndexOf(@"\") + 1, strFileFullName.Length - strFileFullName.LastIndexOf(@"\") - 1);
    }

    public bool CreateGFSFile1(StringBuilder sb, string FilePathName, string CT, string strBatchID, string siteName, string strVersion, string strDN,OracleConnection conn)
    {
        int iRet;
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
                        iRet = CheckConnect(conn);
                        if (iRet == 0)
                        {
                            iRet = ExecSqlNoQuery(strSql, conn);
                        }
                    }
                    catch (Exception ex)
                    {
                        iRet = -1;
                        FERROR = ex.Message;
                    }
                }

                iRet = 1;
            }
            catch (Exception ex)
            {
                iRet = -1;
                FERROR = ex.Message;
            }
        }
        return bRet;
    }




    private List<StringBuilder> GetUpdData(string DN, string SAPModel, string ProCode, string Qty, ref string SfcQty, ref string ErrorMessage, OracleConnection conn, OracleConnection conntw)
    {
        string strSql = string.Empty;
        string FactoryCode = string.Empty;
        List<StringBuilder> lsb = new List<StringBuilder>();
        FactoryCode = "ZFOXTJ-ODM";

        if (ProCode == "UPD01")
        {
            //修改CDMA幾種的第26個欄位  其餘幾種修改第25個欄位 修改為b.PALLET_NUMBER_NEW
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2,'" + FactoryCode + "' f3,"
                                              + "TO_CHAR(e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                              + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                              + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                              + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                              + "DECODE (c.out_date,"
                                                     + " NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                                      + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                              + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                              + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                              + "h.sold_tocustomer_name f18,"
                                              + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                              + "d.serial_num f20, d.ta_number f21,"
                                               + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                //+ "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ '" + strSO + "' f24,"
                                              + "b.PALLET_NUMBER_NEW f25, e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                              + "e.privilegepwd f30, '' f31, '' f32, '' f33, '' f34,"
                                              + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                              + "d.btaddress f36, d.WIFI_ADDRESS f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                              + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                              + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                              + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                              + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                              + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                              + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                              + "'' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79,DECODE(e.nkey,NULL,'',e.nkey) f80,"
                                              + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90, "
                                              + "'' f91,'' f92,'' f93,'' f94,'' f95, DECODE (e.nkey, NULL, '', e.nkey) f96,'' f97,'' f98, "
                                              + " '' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106 "
                /*注意：
                 * 1.目前MOTO還沒有IMEI3、WIFI2、WIFI3/WIFI4,所以，SHP.CMCS_SFC_IMEINUM_NEW表為空表，沒有數據，
                 * 只能將91~106欄位直接置為空，如果以后有IMEI3、WIFI2、WIFI3/WIFI4，則將下面注釋掉的SQL語句替換掉91~106欄位，
                 * 并且添加
                 * decode(M.WIFI2,null,'',M.WIFI2) f91, decode(M.WIFI3,null,'',M.WIFI3)  f92, decode(M.WIFI4,null,'',M.WIFI4) f93,
                 * '' f94, decode(M.IMEI3,null,'NA',M.IMEI3)  f95,'' f96,'' f97,'' f98,'' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106
                 * FORM SHP.CMCS_SFC_IMEINUM_NEW表，
                 * WHERE SHP.CMCS_SFC_IMEINUM_NEW.IMEINUM=SHP.CMCS_SFC_IMEINUM.IMEINUM
                 *   AND SHP.CMCS_SFC_IMEINUM_NEW.PRODUCT_ID=SHP.CMCS_SFC_IMEINUM.PRODUCT_ID
                 */
                                         + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                              + "shp.cmcs_sfc_packing_lines_all b,"
                                              + "shp.cmcs_sfc_shipping_data c,"
                                              + "shp.cmcs_sfc_imeinum d,"
                                              + "" + SAPModel + ".e2pconfig e,"
                                              + "shp.ros_tch_pn f,"
                                              + "shp.cmcs_sfc_porder g,"
                                              + "shp.upd_order_information h,"
                                              + "shp.cmcs_sfc_carton j,"
                                              + "sap.country_iso_link k "
                //+", SHP.CMCS_SFC_IMEINUM_NEW M"
                                        + "WHERE a.order_carton_no = b.internal_carton "
                                          + "AND b.internal_carton = c.carton_no "
                                          + "AND b.customer_po = h.po_number "
                                          + "AND b.item_number = f.ppart "
                                          + "AND d.porder = g.porder "
                                          + "AND c.imei = d.imeinum "
                //+" AND D.IMEINUM=M.IMEINUM "
                //+" AND D.PRODUCT_ID=M.PRODUCT_ID "
                                          + "AND ( c.imei = e.imei  "
                                               + "AND e.status = 'PASS' "
                                               + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + SAPModel + ".e2pconfig h "
                                               + "WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                          + "AND j.carton_no = b.internal_carton "
                                          + "AND (UPPER (b.ship_to_country) =UPPER (k.DESCRIPTION )  or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) )"
                                          + "AND b.invoice_number = a.invoice_no "
                                          + "AND a.invoice_no =:V_DN";
        }
        else if (ProCode == "UPD04")
        {

            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, 'ZFOXTJ-ODM' f3,"
                                   + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                   + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                   + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                   + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                   + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                           + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                  + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                   + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ "
                                   + "h.sold_tocustomer_name f18,"
                                   + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                   + "d.serial_num f20, d.ta_number f21,"
                                    + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                //+ "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ '" + strSO + "' f24,"
                                   + "b.PALLET_NUMBER_NEW f25,  e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                   + "'' f30, '' f31, '' f32, '' f33, '' f34,"
                                   + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                   + "d.btaddress f36, d.WIFI_ADDRESS f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                   + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                   + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                   + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                   + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59, '' f60, '' f61,"
                                   + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                   + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                   + "DECODE(d.imeinum2,NULL,'',d.imeinum2) f71, DECODE(d.imeinum2,NULL,'','IMEI') f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79,DECODE(e.nkey,NULL,'',e.nkey) f80,"
                                   + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90, "
                                   + "'' f91,'' f92,'' f93,'' f94,'' f95, DECODE (e.nkey, NULL, '', e.nkey) f96,'' f97,'' f98, "
                                   + " '' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106 "
                /*注意：
                 * 1.目前MOTO還沒有IMEI3、WIFI2、WIFI3/WIFI4,所以，SHP.CMCS_SFC_IMEINUM_NEW表為空表，沒有數據，
                 * 只能將91~106欄位直接置為空，如果以后有IMEI3、WIFI2、WIFI3/WIFI4，則將下面注釋掉的SQL語句替換掉91~106欄位，
                 * 并且添加
                 * decode(M.WIFI2,null,'',M.WIFI2) f91, decode(M.WIFI3,null,'',M.WIFI3)  f92, decode(M.WIFI4,null,'',M.WIFI4) f93,
                 * '' f94, decode(M.IMEI3,null,'NA',M.IMEI3)  f95,'' f96,'' f97,'' f98,'' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106
                 * FORM SHP.CMCS_SFC_IMEINUM_NEW表，
                 * WHERE SHP.CMCS_SFC_IMEINUM_NEW.IMEINUM=SHP.CMCS_SFC_IMEINUM.IMEINUM
                 *   AND SHP.CMCS_SFC_IMEINUM_NEW.PRODUCT_ID=SHP.CMCS_SFC_IMEINUM.PRODUCT_ID
                 */
                              + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                   + "shp.cmcs_sfc_packing_lines_all b,"
                                   + "shp.cmcs_sfc_shipping_data c,"
                                   + "shp.cmcs_sfc_imeinum d,"
                                   + "" + SAPModel + ".e2pconfigtw e,"
                                   + "shp.ros_tch_pn f,"
                                   + "shp.cmcs_sfc_porder g,"
                                   + "shp.upd_order_information h,"
                                   + "shp.cmcs_sfc_carton j,"
                                   + "sap.country_iso_link k "
                //+", SHP.CMCS_SFC_IMEINUM_NEW M"
                             + "WHERE a.order_carton_no = b.internal_carton "
                               + "AND b.internal_carton = c.carton_no "
                               + "AND b.customer_po = h.po_number "
                               + "AND b.item_number = f.ppart "
                               + "AND d.porder = g.porder "
                               + "AND c.imei = d.imeinum "
                //+" AND D.IMEINUM=M.IMEINUM "
                //+" AND D.PRODUCT_ID=M.PRODUCT_ID "
                               + "AND (c.imei = e.imei "
                                    + "AND e.status = 'PASS' "
                                    + "AND e.e2pdate = "
                                                + "(SELECT MAX (h.e2pdate) FROM " + SAPModel + ".e2pconfigtw h WHERE h.imei = c.imei "
                                    + "AND h.status = 'PASS')) "
                                 + "AND j.carton_no = b.internal_carton "
                                 + "AND (UPPER (b.ship_to_country) = UPPER (k.DESCRIPTION )  or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) ) "
                                 + "AND b.invoice_number = a.invoice_no "
                                 + "AND a.invoice_no =:V_DN";


        }
        else if (ProCode == "UPD02")
        {
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                + "DECODE(h.customer_id,NULL,'','N/A','',h.customer_id) f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                + "b.ship_to_city f15, k.iso_code f16, DECODE(h.customer_id,NULL,'','N/A','',h.customer_id) f17," /*H.CUSTOMER_NAME*/
                                + "h.sold_tocustomer_name f18,"
                                + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                + "d.serial_num f20, d.ta_number f21,"
                                 + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                //+ "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ '" + strSO + "' f24,"
                                + "b.PALLET_NUMBER_NEW f25, e.nkey f26, '' f27, '' f28, e.nskey f29,"
                                + "e.privilegepwd f30, '' f31, '' f32, '' f33, '' f34,"
                                + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                + "d.btaddress f36, d.WIFI_ADDRESS f37, '' f38, '' f39, '' f40,"
                                + "a.invoice_no f41,"
                                + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,"
                                + "'' f60, '' f61,"
                                + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                + "d.imeinum2 f71, 'IMEI' f72, '' f73, '' f74, '' f75, '' f76,'' f77,'' f78,'' f79,DECODE(e.nkey,NULL,'',e.nkey) f80,"
                                + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90, "
                                + "'' f91,'' f92,'' f93,'' f94,'' f95, DECODE (e.nkey, NULL, '', e.nkey) f96,'' f97,'' f98, "
                                + " '' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106 "
                /*注意：
                * 1.目前MOTO還沒有IMEI3、WIFI2、WIFI3/WIFI4,所以，SHP.CMCS_SFC_IMEINUM_NEW表為空表，沒有數據，
                * 只能將91~106欄位直接置為空，如果以后有IMEI3、WIFI2、WIFI3/WIFI4，則將下面注釋掉的SQL語句替換掉91~106欄位，
                * 并且添加
                * decode(M.WIFI2,null,'',M.WIFI2) f91, decode(M.WIFI3,null,'',M.WIFI3)  f92, decode(M.WIFI4,null,'',M.WIFI4) f93,
                * '' f94, decode(M.IMEI3,null,'NA',M.IMEI3)  f95,'' f96,'' f97,'' f98,'' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106
                * FORM SHP.CMCS_SFC_IMEINUM_NEW表，
                * WHERE SHP.CMCS_SFC_IMEINUM_NEW.IMEINUM=SHP.CMCS_SFC_IMEINUM.IMEINUM
                *   AND SHP.CMCS_SFC_IMEINUM_NEW.PRODUCT_ID=SHP.CMCS_SFC_IMEINUM.PRODUCT_ID
                */
                        + " FROM shp.cmcs_sfc_ship_carton_map a,"
                                + "shp.cmcs_sfc_packing_lines_all b,"
                                + "shp.cmcs_sfc_shipping_data c,"
                                + "shp.cmcs_sfc_imeinum d,"
                                + "" + SAPModel + ".e2pconfig e,"
                                + "shp.ros_tch_pn f,"
                                + "shp.cmcs_sfc_porder g,"
                                + "shp.upd_order_information h,"
                                + "shp.cmcs_sfc_carton j,"
                                + "sap.country_iso_link k"
                //+", SHP.CMCS_SFC_IMEINUM_NEW M"
                       + " WHERE a.order_carton_no = b.internal_carton"
                            + " AND b.internal_carton = c.carton_no"
                            + " AND b.customer_po = h.po_number"
                            + " AND b.item_number = f.ppart"
                            + " AND d.porder = g.porder"
                            + " AND c.imei = d.imeinum"
                //+" AND D.IMEINUM=M.IMEINUM "
                //+" AND D.PRODUCT_ID=M.PRODUCT_ID "
                            + " AND (    c.imei = e.imei"
                                 + " AND e.status = 'PASS'"
                                 + " AND e.e2pdate = (SELECT MAX (h.e2pdate)"
                                                + "  FROM " + SAPModel + ".e2pconfig h"
                                                + "  WHERE h.imei = c.imei AND h.status = 'PASS')"
                                + " ) "
                           + " AND j.carton_no = b.internal_carton"
                           + " AND (UPPER (b.ship_to_country) = UPPER (k.DESCRIPTION ) or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) ) "
                           + " AND b.invoice_number = a.invoice_no"
                           + " AND a.invoice_no =:V_DN";

        }
        else if (ProCode == "UPD03")//AVON機種
        {
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                    + "TO_CHAR (e.create_date, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                    + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                    + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                    + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                    + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),"
                                    + "TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                    + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                    + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17,/*H.CUSTOMER_NAME*/ h.sold_tocustomer_name f18,"
                                    + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                    + "d.serial_num f20, d.ta_number f21,"
                                     + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                //+ "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ '" + strSO + "' f24,"
                                    + "b.PALLET_NUMBER_NEW f25, SUBSTR(e.th7, 16, 8) f26, '' f27, '' f28, SUBSTR (e.th7, 48, 8) f29,"
                                    + "'' f30,'' f31, '' f32, '' f33, '' f34,"
                                    + "DECODE (d.customer_num, NULL, 'M14' || SUBSTR (d.imeinum, 8, 7), d.customer_num) f35,"
                                    + "d.btaddress f36, d.WIFI_ADDRESS f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                    + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,'' f43, '' f44, '' f45, '' f46,"
                                    + "'' f47, '' f48, '' f49,"
                                    + "'' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                    + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                    + "DECODE (h.bill_to_id, NULL, '','N/A','', h.bill_to_id) f62, '' f63,"
                                    + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                    + "DECODE(d.imeinum2,NULL,'',d.imeinum2) f71, DECODE(d.imeinum2,NULL,'','IMEI') f72, '' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79, SUBSTR(e.th7, 16, 8) f80,"
                                    + "'' f81,'' f82,'' f83,'' f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90 ,"
                                    + "'' f91,'' f92,'' f93,'' f94,'' f95,  SUBSTR(e.th7, 16, 8) f96,'' f97,'' f98, "
                                    + " '' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106 "
                /*注意：
                * 1.目前MOTO還沒有IMEI3、WIFI2、WIFI3/WIFI4,所以，SHP.CMCS_SFC_IMEINUM_NEW表為空表，沒有數據，
                * 只能將91~106欄位直接置為空，如果以后有IMEI3、WIFI2、WIFI3/WIFI4，則將下面注釋掉的SQL語句替換掉91~106欄位，
                * 并且添加
                * decode(M.WIFI2,null,'',M.WIFI2) f91, decode(M.WIFI3,null,'',M.WIFI3)  f92, decode(M.WIFI4,null,'',M.WIFI4) f93,
                * '' f94, decode(M.IMEI3,null,'NA',M.IMEI3)  f95,'' f96,'' f97,'' f98,'' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106
                * FORM SHP.CMCS_SFC_IMEINUM_NEW表，
                * WHERE SHP.CMCS_SFC_IMEINUM_NEW.IMEINUM=SHP.CMCS_SFC_IMEINUM.IMEINUM
                *   AND SHP.CMCS_SFC_IMEINUM_NEW.PRODUCT_ID=SHP.CMCS_SFC_IMEINUM.PRODUCT_ID
                */
                               + "FROM shp.cmcs_sfc_ship_carton_map a,"
                                    + "shp.cmcs_sfc_packing_lines_all b,"
                                    + "shp.cmcs_sfc_shipping_data c,"
                                    + "shp.cmcs_sfc_imeinum d,"
                                    + "(SELECT   MAX (ID || th7) th7, TRACK_ID,MAX (create_date) create_date FROM testinfo.testinfo_head"
                                    + " WHERE status = 'PASS' AND LENGTH (th7) = 80 AND station_name = 'E2P' GROUP BY TRACK_ID) e, "
                                    + "shp.ros_tch_pn f,"
                                    + "shp.cmcs_sfc_porder g,"
                                    + "shp.upd_order_information h,"
                                    + "shp.cmcs_sfc_carton j,"
                                    + "sap.country_iso_link k "
                //+", SHP.CMCS_SFC_IMEINUM_NEW M"   
                              + "WHERE a.order_carton_no = b.internal_carton "
                                + "AND a.invoice_no = b.invoice_number "
                                + "AND b.customer_po = h.po_number "
                                + "AND b.internal_carton = c.carton_no "
                                + "AND b.internal_carton = j.carton_no "
                                + "AND b.item_number = f.ppart "
                                + "AND c.imei = d.imeinum "
                                + "AND c.productid = e.TRACK_ID "
                                + "AND d.porder = g.porder "
                //+" AND D.IMEINUM=M.IMEINUM "
                //+" AND D.PRODUCT_ID=M.PRODUCT_ID "
                                + "AND (UPPER (b.ship_to_country) =UPPER ( k.description )  or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) )"
                                + "AND a.invoice_no =:V_DN "
                                + "AND b.invoice_number =:V_DN";
        }
        else if (ProCode == "UPD06")//CDMA機種
        {
            strSql = @"SELECT DISTINCT 'MEID' f1, d.imeinum f2, 'FIHTJ' f3,
                               TO_CHAR (d.creation_date, 'yyyy-mm-dd HH24:MI:SS') f4, '' f5,
                               SUBSTR (DECODE (d.customer_num, NULL, d.serial_num, d.customer_num),1,3) f6,c.cust_pno f7,
                               DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f8, f.market_name f9,
                               b.customer_item f10, '' f11,
                               TO_CHAR(DECODE (c.out_date, NULL, c.in_date, c.out_date),'yyyy-mm-dd HH24:MI:SS') f12, '123456' f13,
                               '123456' f14, b.ship_to_customername f15, b.ship_to_city f16,
                               k.iso_code f17, '123456' f18, 'MOTOROLA' f19,
                               TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f20,
                               c.serial_no f21, '' f22, b.internal_carton f23, b.customer_po f24,
                               b.so_number f25, b.PALLET_NUMBER_NEW f26, e.sub_lock f27, '' f28, '' f29,
                               e.onetime_lock f30, '' f31,e.pesn f32, e.dmeid f33, e.akey1 f34,
                               '' f35, d.customer_num f36, '' f37, d.btaddress f38,d.WIFI_ADDRESS f39, '' f40,
                               i.akey2_type f41, e.akey2 f42, '' f43, '' f44, '' f45, '' f46, '' f47,
                               g.software_ver f48, '' f49, '' f50, '' f51, '' f52, '' f53, '' f54,
                               b.invoice_number f55, b.so_number f56, '' f57, k.iso_code f58,
                               '' f59, '' f60, b.invoice_number f61, g.porder f62, '' f63,
                               k.iso_code f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,
                               '' f71, '' f72, '' f73, '' f74, '' f75, '' f76, '' f77, '' f78, '' f79,
                               '' f80, '' f81, '' f82,'' f83,'' f84,'' f85,'' f86,'' f87,'' f88,'' f89,'' f90,'' f91,'' f92,'' f93,'' f94,'' f95
                              ,'' f96, '' f97,'' f98,'' f99,'' f100,DECODE(e.sub_lock,null,'',e.sub_lock) f101,'' f102,'' f103,'' f104,'' f105,'' f106,'' f107,'' f108,'' f109,'' f110,'' f111 
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
                           AND  (UPPER (b.ship_to_country) = UPPER (k.description)
            OR UPPER (b.ship_to_country) = UPPER (k.CMCSDESCRIPTION))
                           AND b.invoice_number=:V_DN";
                                          //,'' f96, '' f97,'' f98,'' f99,'' f100,DECODE(e.sub_lock,null,'',e.sub_lock) f101,'' f102,'' f103,'' f104,'' f105,'' f106,'' f107,'' f108,'' f109,'' f110,'' f111 
        }
        else
        {
            strSql = @"SELECT DISTINCT 'IMEI' f1, c.imei f2, '" + FactoryCode + "' f3,"
                                                  + "TO_CHAR (e.e2pdate, 'yyyy-mm-dd HH24:MI:SS') f4,"
                                                  + "SUBSTR (d.customer_num, 1, 3) f5, DECODE(c.cust_pno,NULL,'unknown',c.cust_pno) f6,"
                                                  + "DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f7,"
                                                  + "f.market_name f8, DECODE (c.sa_no, NULL, c.cust_pno, c.sa_no) f9, '' f10,"
                                                  + "DECODE (c.out_date,NULL, TO_CHAR (c.in_date, 'yyyy-mm-dd HH24:MI:SS'),TO_CHAR (c.out_date, 'yyyy-mm-dd HH24:MI:SS')) f11,"
                                                  + "h.customer_id f12, DECODE(h.ship_id , NULL, '','N/A','',h.ship_id) f13, /*b.SHIP_TO_CUSTOMERNAME */ h.ship_tocustomer_name f14,"
                                                  + "b.ship_to_city f15, k.iso_code f16, h.customer_id f17, /*H.CUSTOMER_NAME*/"
                                                  + "h.sold_tocustomer_name f18,"
                                                  + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f19,"
                                                  + "d.serial_num f20, d.ta_number f21,"
                                                   + "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ h.so_number f24,"
                //+ "j.caseid f22, b.customer_po f23, /*b.SO_NUMBER*/ '" + strSO + "' f24,"
                                                  + "b.PALLET_NUMBER_NEW f25, DECODE(e.nkey,NULL,'',e.nkey) f26, '' f27, '' f28, DECODE(e.nskey,NULL,'',e.nskey) f29,"
                                                  + "DECODE(e.privilegepwd,NULL,'',e.privilegepwd) f30, '' f31, '' f32, '' f33, '' f34,"
                                                  + "DECODE (d.customer_num,NULL, 'M14' || SUBSTR (d.imeinum, 8, 7),d.customer_num) f35,"
                                                  + "d.btaddress f36, d.WIFI_ADDRESS f37, '' f38, '' f39, '' f40,a.invoice_no f41,"
                                                  + "TO_CHAR (b.creation_date, 'yyyy-mm-dd HH24:MI:SS') f42,"
                                                  + "'' f43, '' f44, '' f45, '' f46, '' f47, '' f48, '' f49,"
                                                  + " '' f50, '' f51, '' f52, '' f53, '' f54, '' f55,"
                                                  + "a.invoice_no f56, b.so_number f57, '' f58, k.iso_code f59,'' f60, '' f61,"
                                                  + "DECODE (h.bill_to_id,NULL,'','N/A','',h.bill_to_id) f62, '' f63,"
                                                  + "'' f64, '' f65, '' f66, '' f67, '' f68, '' f69, '' f70,"
                                                  + "DECODE(d.imeinum2,NULL,'',d.imeinum2) f71, DECODE(d.imeinum2,NULL,'','IMEI') f72,"
                                                  + "'' f73, '' f74, '' f75, '' f76, '' f77,'' f78,'' f79,DECODE(e.nkey,NULL,'',e.nkey) f80,"
                                                  + "'' f81,'' f82,e.nskey f83,e.privilegepwd f84,"
                                                  + "'' f85,'' f86,'' f87,'' f88,'' f89 ,'' f90,"
                                                  + "'' f91,'' f92,'' f93,'' f94,'' f95, DECODE (e.nkey, NULL, '', e.nkey) f96,'' f97,'' f98, "
                                                  + " '' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106 "
                /*注意：
                 * 1.目前MOTO還沒有IMEI3、WIFI2、WIFI3/WIFI4,所以，SHP.CMCS_SFC_IMEINUM_NEW表為空表，沒有數據，
                 * 只能將91~106欄位直接置為空，如果以后有IMEI3、WIFI2、WIFI3/WIFI4，則將下面注釋掉的SQL語句替換掉91~106欄位，
                 * 并且添加
                 * decode(M.WIFI2,null,'',M.WIFI2) f91, decode(M.WIFI3,null,'',M.WIFI3)  f92, decode(M.WIFI4,null,'',M.WIFI4) f93,
                 * '' f94, decode(M.IMEI3,null,'NA',M.IMEI3)  f95,'' f96,'' f97,'' f98,'' f99,'' f100,'' f101,'' f102,'' f103,'' f104,'' f105,'' f106
                 * FORM SHP.CMCS_SFC_IMEINUM_NEW表，
                 * WHERE SHP.CMCS_SFC_IMEINUM_NEW.IMEINUM=SHP.CMCS_SFC_IMEINUM.IMEINUM
                 *   AND SHP.CMCS_SFC_IMEINUM_NEW.PRODUCT_ID=SHP.CMCS_SFC_IMEINUM.PRODUCT_ID
                 */
                                                 + " FROM shp.cmcs_sfc_ship_carton_map a,"
                                                 + "shp.cmcs_sfc_packing_lines_all b,"
                                                 + "shp.cmcs_sfc_shipping_data c,"
                                                 + "shp.cmcs_sfc_imeinum d,"
                                                 + "" + SAPModel + ".e2pconfigtw e,"
                                                 + "shp.ros_tch_pn f,"
                                                 + "shp.cmcs_sfc_porder g,"
                                                 + "shp.upd_order_information h,"
                                                 + "shp.cmcs_sfc_carton j,"
                                                 + "sap.country_iso_link k "
                //+", SHP.CMCS_SFC_IMEINUM_NEW M"
                                             + "WHERE a.order_carton_no = b.internal_carton "
                                             + "AND b.customer_po = h.po_number "
                                             + "AND b.invoice_number = a.invoice_no "
                                             + "AND b.internal_carton = c.carton_no "
                                             + "AND c.imei = d.imeinum "
                                             + "AND b.item_number = f.ppart "
                                             + "AND d.porder = g.porder "
                //+" AND D.IMEINUM=M.IMEINUM "
                //+" AND D.PRODUCT_ID=M.PRODUCT_ID "
                                             + "AND (c.imei = e.imei "
                                                   + "AND e.status = 'PASS'"
                                                   + "AND e.e2pdate =(SELECT MAX (h.e2pdate) FROM " + SAPModel + ".e2pconfig h WHERE h.imei = c.imei AND h.status = 'PASS')) "
                                             + "AND j.carton_no = b.internal_carton "
                                             + "AND (UPPER (b.ship_to_country) = UPPER (k.DESCRIPTION )   or UPPER (b.ship_to_country) =UPPER (k.CMCSDESCRIPTION ) )"
                                             + "AND a.invoice_no =:V_DN";

        }
        if (ProCode != "UPD06")
            strSql = strSql + " ORDER BY C.IMEI";
        else
            strSql = strSql + " ORDER BY D.IMEINUM";


        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        int iRet;
        try
        {
            iRet = CheckConnect(conn);
            using (DataSet odr = GetDataSet(strSql, Parms, conn))
            {
                StringBuilder sbupd = new StringBuilder();
                StringBuilder sbs = new StringBuilder();
                int iColNum = odr.Tables[0].Columns.Count;
                int iRowNum = odr.Tables[0].Rows.Count;
                SfcQty = Convert.ToString(iRowNum);
                if (SfcQty == Qty)
                {


                    //修改于5月22日，由施勇修改
                    //如果221服務器上的PUBLIB.DNMULTSO_MAP表中存在數據則修改已有的SO
                    //
                    #region 修改SO

                    DataSet soDataset = new DataSet();
                    int iRow = 0;
                    int count = 0;

                    string[] soNum = new string[0];
                    string dnNum = string.Empty;
                    string poNum = string.Empty;
                    string level = string.Empty;
                    decimal totailQty = 0;
                    dnNum = odr.Tables[0].Rows[0][40].ToString();
                    poNum = odr.Tables[0].Rows[0][22].ToString();
                    soDataset = RetDs(dnNum, poNum);
                    //level = soDataset.Tables[0].Rows[0]["LEVEL_FLAG"].ToString();
                    iRow = soDataset.Tables[0].Rows.Count;
                    if (iRow != 0)
                    {
                        level = soDataset.Tables[0].Rows[0]["LEVEL_FLAG"].ToString();
                        for (int i = 0; i < iRow; i++)
                        {
                            Array.Resize(ref soNum, (soNum.Length + 1));
                            soNum[i] = soDataset.Tables[0].Rows[i]["SO"].ToString();
                        }
                        if (soDataset.Tables[0].Rows.Count == 1)
                        {
                            for (int i = 0; i < iRowNum; i++)
                            {
                                odr.Tables[0].Rows[i][23] = soNum[0];
                            }
                        }
                        else if (soDataset.Tables[0].Rows.Count > 1)
                        {
                            for (int z = 0; z < iRow; z++)
                            {
                                decimal totailnum = Convert.ToDecimal(soDataset.Tables[0].Rows[z]["UQTY"].ToString()) + Convert.ToDecimal(soDataset.Tables[0].Rows[z]["UNQTY"].ToString());
                                totailQty = totailQty + totailnum;
                            }
                            if (totailQty == Convert.ToDecimal(Qty))
                            {
                                for (int i = 0; i < iRow; i++)
                                {
                                    int qty = 0;
                                    if (Convert.ToDecimal(soDataset.Tables[0].Rows[i]["UNQTY"].ToString()) != 0)
                                    {
                                        for (int j = 0; j < Convert.ToDecimal(soDataset.Tables[0].Rows[i]["UNQTY"].ToString()); j++)
                                        {
                                            DataSet ds = new DataSet();
                                            ds = RetDs(dnNum, poNum);
                                            if (!(ds.Tables[0].Rows[i]["FLAG1"].Equals("C")))
                                            {

                                                odr.Tables[0].Rows[count][23] = soNum[i];
                                                count++;
                                                //qty++;
                                                string sql = @"UPDATE PUBLIB.DNMULTSO_MAP SET UQTY = UQTY + 1,UNQTY = UNQTY - 1 WHERE SO = '" + soNum[i] + "' AND DN = '" + dnNum + "' ";
                                                try
                                                {

                                                    if (ConnWriteConnect.State != ConnectionState.Open) ConnWriteConnect.Open();
                                                    OracleCommand com = new OracleCommand(sql, ConnWriteConnect);
                                                    com.ExecuteNonQuery();
                                                }
                                                catch (Exception ex)
                                                {
                                                    ex.ToString();
                                                }
                                                finally
                                                {
                                                    //ConnWriteConnect.Close();
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    //string sql = @"UPDATE PUBLIB.DNMULTSO_MAP SET UQTY = UQTY + '" + qty + "',UNQTY = UNQTY - '" + qty + "' WHERE SO = '" + soNum[i] + "' AND DN = '" + dnNum + "' ";

                                    //try
                                    //{
                                    //    ConnWriteConnect.Open();
                                    //    //con.Open();
                                    //    OracleCommand com = new OracleCommand(sql, ConnWriteConnect);
                                    //    com.ExecuteNonQuery();
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    ex.ToString();
                                    //}
                                    //finally
                                    //{
                                    //    ConnWriteConnect.Close();
                                    //}
                                }
                            }
                        }
                    }
                    #endregion

                    for (int n = 0; n < iRowNum; n++)
                    {



                        if (ProCode != "UPD06")
                        {
                            for (int rownum = 0; rownum < iColNum; rownum++)
                            {
                                string FiledValue = odr.Tables[0].Rows[n][rownum].ToString();
                                string FiledImei = odr.Tables[0].Rows[n][1].ToString();
                                bool FunChk = CheckFieldValue(DN, SAPModel, FiledValue, FiledImei, n + 1, rownum + 1, ref ErrorMessage, conntw);
                                if (FunChk == false)
                                {
                                    return lsb;
                                }
                            }


                            /*修改于2012-04-17，更新SO，
                             * 如果221服務器PUBLIB.UPD_PODN_LIST_T表中SO_NUMBER欄位有值，則更新到生成文件中，如無值，則不更新
                             */
                            //string SO = string.Empty;
                            //int imer = CallSoNo(odr.Tables[0].Rows[n][40].ToString(), odr.Tables[0].Rows[n][22].ToString(), ref SO);//參數為DN,PO
                            //if (SO != "" && imer == 0)
                            //{
                            //    odr.Tables[0].Rows[n][23] = SO;
                            //}

                        }

                        for (int i = 0; i < iColNum; i++)
                        {

                            if (ProCode != "UPD06")
                            {
                                if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15 || i == 16 || i == 17 || i == 19 || i == 22 || i == 23 || i == 34 || i == 55)
                                {
                                    if (odr.Tables[0].Rows[n][i].ToString() == "")
                                    {
                                        odr.Tables[0].Rows[n][i] = "NA";
                                    }

                                }


                                /*---2012-04-11---陳宏博修改
                                 * 由MOTO提出新需求，如下：
                                 * 1.判斷單卡（70欄位），如果解鎖碼（25，28，29）為空或為“00000000”，則26~30，80~90為""
                                 * 2.判斷雙卡（70欄位），如果解鎖碼（25，28，29）為空或為“00000000”，則26~30為"",80~90為"NA"
                                 * 
                                    Model / Loc	71	    94	     26,29,30		26-30	    80       81-90 	     96        97-106
                                    ================================================================================================
				                                                無,"00000000"	空	        空         空        空          空
                                    單卡		無值   無值	    有			Donot-care	
                                    ================================================================================================
				                                                無,"00000000"	空          NA         空        空          空
                                    雙卡		有值   無值	    有			Donot-care	
                                    ================================================================================================
				                                                無,"00000000"	空          NA         空        NA          空
                                    三卡		有值   有值	    有			Donot-care	
                                    ================================================================================================

                                    Remark :   Loc 71 單卡/雙卡,   Loc 94 雙卡/三卡       Loc26/Loc80/Loc96  Lockcode,  X mean Donot-care

                                    第95欄位邏輯為DECODE(imeinum3,NULL,'','IMEI')

                                    91～93欄位意義為WIFI2～WIFI4

                                 */
                                if (i == 70)//判斷是否是雙卡
                                {
                                    if (odr.Tables[0].Rows[n][i].ToString() != "")//判斷是雙卡
                                    {

                                        if (odr.Tables[0].Rows[n][94].ToString() != "")//判斷是三卡
                                        {
                                            #region 三卡
                                            //下面判斷26，80，96欄位是否有解鎖碼
                                            if (odr.Tables[0].Rows[n][25].ToString() == "" || odr.Tables[0].Rows[n][25].ToString() == "00000000" || odr.Tables[0].Rows[n][79].ToString() == "" || odr.Tables[0].Rows[n][79].ToString() == "00000000" || odr.Tables[0].Rows[n][95].ToString() == "" || odr.Tables[0].Rows[n][95].ToString() == "00000000")
                                            {
                                                //下面判斷29，30欄位是否有解鎖碼
                                                if (odr.Tables[0].Rows[n][28].ToString() == "" || odr.Tables[0].Rows[n][28].ToString() == "00000000" || odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                                {
                                                    odr.Tables[0].Rows[n][25] = "";
                                                    odr.Tables[0].Rows[n][26] = "";
                                                    odr.Tables[0].Rows[n][27] = "";
                                                    odr.Tables[0].Rows[n][28] = "";
                                                    odr.Tables[0].Rows[n][29] = "";
                                                }
                                                odr.Tables[0].Rows[n][79] = "NA";
                                                odr.Tables[0].Rows[n][80] = "";
                                                odr.Tables[0].Rows[n][81] = "";
                                                odr.Tables[0].Rows[n][82] = "";
                                                odr.Tables[0].Rows[n][83] = "";
                                                odr.Tables[0].Rows[n][84] = "";
                                                odr.Tables[0].Rows[n][85] = "";
                                                odr.Tables[0].Rows[n][86] = "";
                                                odr.Tables[0].Rows[n][87] = "";
                                                odr.Tables[0].Rows[n][88] = "";
                                                odr.Tables[0].Rows[n][89] = "";
                                                odr.Tables[0].Rows[n][90] = "";
                                                odr.Tables[0].Rows[n][91] = "";
                                                odr.Tables[0].Rows[n][92] = "";
                                                odr.Tables[0].Rows[n][93] = "";
                                                odr.Tables[0].Rows[n][94] = "";
                                                odr.Tables[0].Rows[n][95] = "NA";
                                                odr.Tables[0].Rows[n][96] = "";
                                                odr.Tables[0].Rows[n][97] = "";
                                                odr.Tables[0].Rows[n][98] = "";
                                                odr.Tables[0].Rows[n][99] = "";
                                                odr.Tables[0].Rows[n][100] = "";
                                                odr.Tables[0].Rows[n][101] = "";
                                                odr.Tables[0].Rows[n][102] = "";
                                                odr.Tables[0].Rows[n][103] = "";
                                                odr.Tables[0].Rows[n][104] = "";
                                                odr.Tables[0].Rows[n][105] = "";
                                            }
                                            #endregion
                                        }
                                        else//判斷不是三卡
                                        {
                                            #region 双卡
                                            odr.Tables[0].Rows[n][95] = "";
                                            odr.Tables[0].Rows[n][96] = "";
                                            odr.Tables[0].Rows[n][97] = "";
                                            odr.Tables[0].Rows[n][98] = "";
                                            odr.Tables[0].Rows[n][99] = "";
                                            odr.Tables[0].Rows[n][100] = "";
                                            odr.Tables[0].Rows[n][101] = "";
                                            odr.Tables[0].Rows[n][102] = "";
                                            odr.Tables[0].Rows[n][103] = "";
                                            odr.Tables[0].Rows[n][104] = "";
                                            odr.Tables[0].Rows[n][105] = "";
                                            //下面判斷26，80欄位是否有解鎖碼
                                            if (odr.Tables[0].Rows[n][25].ToString() == "" || odr.Tables[0].Rows[n][25].ToString() == "00000000" || odr.Tables[0].Rows[n][79].ToString() == "" || odr.Tables[0].Rows[n][79].ToString() == "00000000")
                                            {
                                                //下面判斷29，30欄位是否有解鎖碼
                                                if (odr.Tables[0].Rows[n][28].ToString() == "" || odr.Tables[0].Rows[n][28].ToString() == "00000000" || odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                                {
                                                    odr.Tables[0].Rows[n][25] = "";
                                                    odr.Tables[0].Rows[n][26] = "";
                                                    odr.Tables[0].Rows[n][27] = "";
                                                    odr.Tables[0].Rows[n][28] = "";
                                                    odr.Tables[0].Rows[n][29] = "";
                                                }
                                                odr.Tables[0].Rows[n][79] = "NA";
                                                odr.Tables[0].Rows[n][80] = "";
                                                odr.Tables[0].Rows[n][81] = "";
                                                odr.Tables[0].Rows[n][82] = "";
                                                odr.Tables[0].Rows[n][83] = "";
                                                odr.Tables[0].Rows[n][84] = "";
                                                odr.Tables[0].Rows[n][85] = "";
                                                odr.Tables[0].Rows[n][86] = "";
                                                odr.Tables[0].Rows[n][87] = "";
                                                odr.Tables[0].Rows[n][88] = "";
                                                odr.Tables[0].Rows[n][89] = "";
                                                odr.Tables[0].Rows[n][90] = "";
                                                odr.Tables[0].Rows[n][91] = "";
                                                odr.Tables[0].Rows[n][92] = "";
                                                odr.Tables[0].Rows[n][93] = "";
                                                odr.Tables[0].Rows[n][94] = "";

                                            }
                                            else
                                            {
                                                if (odr.Tables[0].Rows[n][28].ToString() == "" || odr.Tables[0].Rows[n][28].ToString() == "00000000" || odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                                {
                                                    odr.Tables[0].Rows[n][26] = "";
                                                    odr.Tables[0].Rows[n][27] = "";
                                                    odr.Tables[0].Rows[n][28] = "";
                                                    odr.Tables[0].Rows[n][29] = "";
                                                }
                                                if (odr.Tables[0].Rows[n][82].ToString() == "" || odr.Tables[0].Rows[n][82].ToString() == "00000000")
                                                {
                                                    odr.Tables[0].Rows[n][82] = "";
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    else //判斷是單卡
                                    {
                                        #region 单卡
                                        odr.Tables[0].Rows[n][79] = "";
                                        odr.Tables[0].Rows[n][80] = "";
                                        odr.Tables[0].Rows[n][81] = "";
                                        odr.Tables[0].Rows[n][82] = "";
                                        odr.Tables[0].Rows[n][83] = "";
                                        odr.Tables[0].Rows[n][84] = "";
                                        odr.Tables[0].Rows[n][85] = "";
                                        odr.Tables[0].Rows[n][86] = "";
                                        odr.Tables[0].Rows[n][87] = "";
                                        odr.Tables[0].Rows[n][88] = "";
                                        odr.Tables[0].Rows[n][89] = "";
                                        odr.Tables[0].Rows[n][90] = "";
                                        odr.Tables[0].Rows[n][91] = "";
                                        odr.Tables[0].Rows[n][92] = "";
                                        odr.Tables[0].Rows[n][93] = "";
                                        odr.Tables[0].Rows[n][94] = "";
                                        odr.Tables[0].Rows[n][95] = "";
                                        odr.Tables[0].Rows[n][96] = "";
                                        odr.Tables[0].Rows[n][97] = "";
                                        odr.Tables[0].Rows[n][98] = "";
                                        odr.Tables[0].Rows[n][99] = "";
                                        odr.Tables[0].Rows[n][100] = "";
                                        odr.Tables[0].Rows[n][101] = "";
                                        odr.Tables[0].Rows[n][102] = "";
                                        odr.Tables[0].Rows[n][103] = "";
                                        odr.Tables[0].Rows[n][104] = "";
                                        odr.Tables[0].Rows[n][105] = "";
                                        //下面判斷26欄位是否有解鎖碼
                                        if (odr.Tables[0].Rows[n][25].ToString() == "" || odr.Tables[0].Rows[n][25].ToString() == "00000000")
                                        {
                                            //下面判斷29，30欄位是否有解鎖碼
                                            if (odr.Tables[0].Rows[n][28].ToString() == "" || odr.Tables[0].Rows[n][28].ToString() == "00000000" || odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                            {
                                                odr.Tables[0].Rows[n][25] = "";
                                                odr.Tables[0].Rows[n][26] = "";
                                                odr.Tables[0].Rows[n][27] = "";
                                                odr.Tables[0].Rows[n][28] = "";
                                                odr.Tables[0].Rows[n][29] = "";
                                            }
                                        }
                                        else
                                        {
                                            if (odr.Tables[0].Rows[n][28].ToString() == "" || odr.Tables[0].Rows[n][28].ToString() == "00000000" || odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                            {
                                                odr.Tables[0].Rows[n][26] = "";
                                                odr.Tables[0].Rows[n][27] = "";
                                                odr.Tables[0].Rows[n][28] = "";
                                                odr.Tables[0].Rows[n][29] = "";
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region CDMA機種
                                /*  CDMA V6.2 
                                  Model / Loc	76	    99	     27,85,100		27	      30             85             101     
                                    ================================================================================================
                                                                無,"00000000"	空	      空             空              空        
                                    單卡		無值   無值	    有			Donot-care	  
                                    ================================================================================================
                                                                無,"00000000"	空        空             NA              空       
                                    雙卡		有值   無值	    有			Donot-care	 
                                    ================================================================================================
                                                                無,"00000000"	空        空             NA              NA        
                                    三卡		有值   有值	    有			Donot-care	  
                                    ================================================================================================

                                    Remark :   Loc 76 單卡/雙卡,   Loc 99 雙卡/三卡       Loc27/Loc85/Loc100  Lockcode,  X mean Donot-care
                                * */
                                if (i == 75)
                                {
                                    if (odr.Tables[0].Rows[n][75].ToString() != "")//判斷是否雙卡
                                    {
                                        if (odr.Tables[0].Rows[n][98].ToString() != "")//判斷是否三卡
                                        {
                                            if (odr.Tables[0].Rows[n][26].ToString() == "" || odr.Tables[0].Rows[n][26].ToString() == "00000000" || odr.Tables[0].Rows[n][84].ToString() == "" || odr.Tables[0].Rows[n][84].ToString() == "00000000" || odr.Tables[0].Rows[n][100].ToString() == "" || odr.Tables[0].Rows[n][100].ToString() == "00000000")
                                            {
                                                odr.Tables[0].Rows[n][26] = "";
                                                odr.Tables[0].Rows[n][84] = "NA";
                                                odr.Tables[0].Rows[n][100] = "NA";
                                            }
                                            if (odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                            {
                                                odr.Tables[0].Rows[n][29] = "";
                                            }
                                        }
                                        else//判斷是雙卡
                                        {
                                            odr.Tables[0].Rows[n][100] = "";
                                            if (odr.Tables[0].Rows[n][26].ToString() == "" || odr.Tables[0].Rows[n][26].ToString() == "00000000" || odr.Tables[0].Rows[n][84].ToString() == "" || odr.Tables[0].Rows[n][84].ToString() == "00000000")
                                            {
                                                odr.Tables[0].Rows[n][26] = "";
                                                odr.Tables[0].Rows[n][84] = "NA";
                                            }
                                            if (odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                            {
                                                odr.Tables[0].Rows[n][29] = "";
                                            }
                                        }
                                    }
                                    else//判斷是單卡
                                    {
                                        odr.Tables[0].Rows[n][84] = "";
                                        odr.Tables[0].Rows[n][100] = "";
                                        if (odr.Tables[0].Rows[n][26].ToString() == "" || odr.Tables[0].Rows[n][26].ToString() == "00000000")
                                        {
                                            odr.Tables[0].Rows[n][26] = "";
                                        }
                                        if (odr.Tables[0].Rows[n][29].ToString() == "" || odr.Tables[0].Rows[n][29].ToString() == "00000000")
                                        {
                                            odr.Tables[0].Rows[n][29] = "";
                                        }
                                    }
                                }
                                #endregion
                            }
                            //sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                            //sbs.Append("|");
                        }
                        /*2012-04-11 因做出如上修改，所以將odr.Tables[0]表轉換成 String 類型的步驟，在下面的程式中，將重新轉換成STRING類型
                         * sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                         * sbs.Append("|");
                         */
                      
                        for (int i = 0; i < iColNum; i++)
                        {
                            if (ProCode != "UPD06")
                            {
                                sbs.Append(odr.Tables[0].Rows[n][i].ToString().Replace("\r\n", ""));
                                sbs.Append("|");
                            }

                        }
                        sbs.AppendLine();

                        if ((sbupd.Length + sbs.Length) <= CreateFile.SINGLE_UPD_FILE_MAX_LENGTH)
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
                else
                {
                    return lsb;
                }

            }


        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return lsb;
    }

    private DataSet RetDs(string dn, string po)
    {
        DataSet ds = new DataSet();
        string sql = @"SELECT SO,SOQTY,UQTY,UNQTY,FLAG1,FLAG2,LEVEL_FLAG  FROM PUBLIB.DNMULTSO_MAP WHERE PO = '" + po + "' AND DN = '" + dn + "' ORDER BY SEQNO";
        try
        {
            //if (ConnWriteConnect.State.Equals("Close"))
            //{
            //    ConnWriteConnect.Open();
            //}
            if (ConnWriteConnect.State != ConnectionState.Open) ConnWriteConnect.Open();
            OracleDataAdapter da = new OracleDataAdapter(sql, ConnWriteConnect);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
        }

        return ds;
    }

    private int CallSoNo(string DN, string PO, ref string SO)
    {
        int iRet = 0;
        int k = 0;
        string strSO = "select SO_NUMBER FROM PUBLIB.UPD_PODN_LIST_T WHERE INVOICE='" + DN + "' AND PO='" + PO + "' ";
        //string SO = string.Empty;
        try
        {
            using (OracleDataReader odr = GetDataReader(strSO, ConnWriteConnect))
            {

                while (odr.Read())
                {
                    SO = odr[0].ToString();

                    //if (SO != "" && SO != null && k == 0)
                    //{
                    //    string getSO = "update shp.upd_order_information  set so_number='" + SO + "' where po_number='" + PO + "'";
                    //    iRet = DataBaseOperation.ExecSQL("Oracle", strReadConn76, getSO);
                    //    k = 1;
                    //    break;
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            SO = "";
            iRet = -1;
            FERROR = ex.Message;
        }


        return iRet;

    }

    private List<StringBuilder> GetGFS(string strInvoice,OracleConnection conn)
    {
        List<StringBuilder> lsb = new List<StringBuilder>();
        string strSql = string.Empty;
        int iRet;
        strSql = @"SELECT 'UN' F1,'CM' F2,rownum F3,C.CUSTOMER_NUM F4,C.SERIAL_NUM F5,
                             C.BTADDRESS F6,C.imeinum F7,B.DMEID F8,B.AKEY1 F9,
                             B.AKEY2 F10,B.SUB_LOCK F11,B.ONETIME_LOCK F12,B.PESN F13,
                             LPAD(TO_NUMBER(SUBSTR(B.PESN,1,2),'XXX'),3,'0')||LPAD(TO_NUMBER(SUBSTR(B.PESN,3,LENGTH(B.PESN)),'XXXXXXXX'),8,'0') F14,
                             '' F15, ''  F16,'' F17, '' F18,C.WIFI_ADDRESS F19,
                             '' F20,F.software_ver F21,'' F22,D.MARKET_NAME F23,D.PHONE_MODEL F24,
                             A.INTERNAL_CARTON F25,LPAD('FOUR_S',20,'0') F26,TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') F27,
                             TO_CHAR(A.LAST_UPDATE_DATE,'YYYYMMDDHH24MISS') F28
                             FROM SAP.CMCS_SFC_PACKING_LINES_ALL A,SFC.CDMA_UPD_PARSER_MEID_V B,SHP.CMCS_SFC_IMEINUM C,SHP.ROS_TCH_PN D,
                             SHP.CMCS_SFC_SHIPPING_DATA E,shp.cmcs_sfc_porder F WHERE A.INTERNAL_CARTON=E.CARTON_NO AND E.IMEI=C.IMEINUM AND
                             C.imeinum=B.MEID_ID AND C.PPART=D.PPART AND C.porder = F.porder
                             AND A.INVOICE_NUMBER=:V_DN";
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", strInvoice);

        try
        {
            iRet = CheckConnect(conn);
            using (DataSet odr = GetDataSet(strSql, Parms,conn))
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

                    if ((sbupd.Length + sbs.Length) <= CreateFile.SINGLE_UPD_FILE_MAX_LENGTH)
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
            iRet = -1;
            FERROR = ex.Message;
        }
        return lsb;
    }

    private string CreateIMEIFile(string strInvoice, string strModel,string region, string ImeiCode,string RevCity,OracleConnection conn,OracleConnection connTw)
    {
        string strIMEIFile = "-";
        string strCustomerName = string.Empty;
        string strSql = string.Empty;
        string strSelectSql = string.Empty;
        string strWhereSql = string.Empty;
        string strTableSql = string.Empty;
        int ShuangKa = 0;
        int iRet = 0;
        strSelectSql = @" SELECT e.SO_NO SALES_PO,g.PPART FIH_PN,g.HW_VER HW,e.SO_NO SO,g.SW_VER SW,
                                            LPAD(e.INVOICE,10,'0') INVOICE_NO,g.PHONE_MODEL XCVR_NO,
                                            (SELECT COUNT(b.IMEI) FROM SAP.CMCS_SFC_PACKING_LINES_ALL a,SHP.CMCS_SFC_SHIPPING_DATA b 
                                            WHERE a.INTERNAL_CARTON=B.CARTON_NO and a.INVOICE_NUMBER='" + strInvoice + @"'  ) QTY ,
                                            e.ADDRESS SHIP_TO,g.SA_NO moto_no, 'TIANJIN' FACILITY,
                                            TO_CHAR(SYSDATE,'YYYY/MM/DD') CREATEDATE,a.CUSTOMER_PO MOTO_PO, 
                                            c.IMEINUM IMEI,c.CUSTOMER_NUM MSN,'12345678' NP_PW,b.SA_NO MODEL,TO_CHAR(a.SHIP_DATE,'yyyy/mm/ dd') SHIP_DATE,
                                            d.CASEID  CARTON_ID,a.PALLET_NUMBER_NEW PALLET_ID";

        strWhereSql = @" WHERE a.INVOICE_Number='" + strInvoice + @"'
                                    AND a.INVOICE_Number=e.INVOICE
                                    AND a.INTERNAL_CARTON=b.CARTON_NO
                                    AND b.IMEI=c.IMEINUM 
                                    AND a.INTERNAL_CARTON=d.CARTON_NO 
                                    AND e.SO_NO=f.ORDER_NUMBER
                                    AND d.PPART=g.PPART";

        strTableSql = @" FROM SAP.CMCS_SFC_PACKING_LINES_ALL a,
                                 SHP.CMCS_SFC_SHIPPING_DATA b,
                                 SHP.CMCS_SFC_IMEINUM c,
                                 SHP.CMCS_SFC_CARTON d,
                                 SAP.SAP_INVOICE_INFO e ,
                                 SAP.SAP_ORDER_INFO F,
                                 SHP.ROS_TCH_PN g";
        ShuangKa = DouBSwitch(strInvoice,conn);//判斷單雙卡
            if (ImeiCode == "IMEI06")
            {
                strSelectSql += ",DECODE(c.imeinum2,'','N/A',c.imeinum2) imei2, SUBSTR (h.th7, 16, 8) p_uc,SUBSTR (h.th7, 48, 8) s_uc, 'N/A' s_pc";
                strTableSql += ",(SELECT   MAX (ID || th7) th7, th26, MAX (create_date) create_date FROM testinfo.testinfo_head" +
                                            " WHERE status = 'PASS' AND LENGTH (th7) = 80" +
                                            "AND station_name = 'E2P' GROUP BY th26) h";
                strWhereSql += " AND b.imei = h.th26";
            }
            else if (ImeiCode == "IMEI05")
            {
                strSelectSql += ",DECODE (c.imeinum2, '', 'N/A', c.imeinum2) imei2, DECODE(h.nkey,'','N/A',h.nkey) p_uc,DECODE(h.nskey,'','N/A',h.nskey) s_uc,'N/A' s_pc";
                strTableSql += "," + strModel + ".E2PCONFIGTW h";
                strWhereSql += " AND (c.imeinum = h.imei AND h.status = 'PASS' AND h.e2pdate = (SELECT MAX (h.e2pdate) FROM " + strModel + ".e2pconfigtw h" +
                                               " WHERE h.imei = c.imeinum AND h.status = 'PASS'))";
            }
            else if (ShuangKa > 0 && region != "TW")
            {
                strSelectSql += ",C.IMEINUM2 IMEI2";
            }
            else if (ShuangKa > 0 && region == "TW")
            {
                strSelectSql += ",C.IMEINUM2 IMEI2,C.WIFI_ADDRESS";
            }
            else if (region == "TW")
            {
                strSelectSql += ",C.WIFI_ADDRESS";
            }
            else
            {
                strSelectSql += ",C.BTADDRESS,C.WIFI_ADDRESS";
            }
        

        strSql = strSelectSql + strTableSql + strWhereSql + " ORDER BY CARTON_ID";

        DateTime Process_BeforeTime = DateTime.Now;
        Excel.Application app = new Excel.Application();

        int ConnStatus = CheckConnect(conn);

        try
        {
            Excel.Workbook wb = app.Workbooks.Add(true);
            Excel.Worksheet st = (Excel.Worksheet)app.Sheets[1];
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;

            DataSet odr = GetDataSet(strSql,conn);
            System.Data.DataTable dt = odr.Tables[0];
            if (odr.Tables[0].Rows.Count > 0)
            {
                //throw new Exception("test");
                bool bInit = false;
                int n = 8;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!bInit)
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
                        if (ShuangKa > 0 && ImeiCode != "IMEI05" && ImeiCode != "IMEI06" && region != "TW")
                        {
                            st.Cells[7, 1] = "Pri-IMEI NO.";
                            st.Cells[7, 2] = "Sec-IMEI NO.";
                            st.Cells[7, 3] = "MSN";
                            st.Cells[7, 4] = "NP P/W";
                            st.Cells[7, 5] = "Model";
                            st.Cells[7, 6] = "Shipment Date";
                            st.Cells[7, 7] = "CARTON ID";
                            st.Cells[7, 8] = "PALLET ID";
                        }
                        else if (ShuangKa > 0 && region == "TW")//原代碼為if (ShuangKa > 0 && strcountry != "LA")  2011/12/30  尹傑更正 
                        {
                            st.Cells[7, 1] = "Pri-IMEI NO.";
                            st.Cells[7, 2] = "Sec-IMEI NO.";
                            st.Cells[7, 3] = "MSN";
                            st.Cells[7, 4] = "NP P/W";
                            st.Cells[7, 5] = "Model";
                            st.Cells[7, 6] = "Shipment Date";
                            st.Cells[7, 7] = "CARTON ID";
                            st.Cells[7, 8] = "PALLET ID";
                            st.Cells[7, 9] = "Wifi MAC Adress";
                        }
                        else if (region == "TW")
                        {
                            st.Cells[7, 1] = "IMEI NO.";
                            st.Cells[7, 2] = "MSN";
                            st.Cells[7, 3] = "NP P/W";
                            st.Cells[7, 4] = "Model";
                            st.Cells[7, 5] = "Shipment Date";
                            st.Cells[7, 6] = "CARTON ID";
                            st.Cells[7, 7] = "PALLET ID";
                            st.Cells[7, 8] = "Wifi MAC Adress";
                        }
                        else
                        {

                            if (ImeiCode == "IMEI06")
                            {
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
                            else if (ImeiCode == "IMEI05")
                            {
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
                            else if (ImeiCode == "IMEI01")
                            {
                                st.Cells[7, 1] = "IMEI NO.";
                                st.Cells[7, 2] = "MSN";
                                st.Cells[7, 3] = "NP P/W";
                                st.Cells[7, 4] = "Model";
                                st.Cells[7, 5] = "Shipment Date";
                                st.Cells[7, 6] = "CARTON ID";
                                st.Cells[7, 7] = "PALLET ID";
                            }
                            else if (ImeiCode == "IMEI02")
                            {
                                st.Cells[7, 1] = "IMEI NO.";
                                st.Cells[7, 2] = "MSN";
                                st.Cells[7, 3] = "NP P/W";
                                st.Cells[7, 4] = "Model";
                                st.Cells[7, 5] = "Shipment Date";
                                st.Cells[7, 6] = "CARTON ID";
                                st.Cells[7, 7] = "PALLET ID";
                                st.Cells[7, 8] = "BT MAC Adress";
                                st.Cells[7, 9] = "Wifi MAC Adress";
                            }
                            else
                            {
                                st.Cells[7, 2] = "MSN";
                                st.Cells[7, 3] = "NP P/W";
                                st.Cells[7, 4] = "Model";
                                st.Cells[7, 5] = "Shipment Date";
                                st.Cells[7, 6] = "CARTON ID";
                                st.Cells[7, 7] = "PALLET ID";
                            }
                        }

                        bInit = true;
                    }
                    break;
                }
                int x = 0;
                object[,] objData = null;

                if (ShuangKa > 0 && ImeiCode != "IMEI05" && ImeiCode != "IMEI06" && region != "TW")
                {
                    objData = new Object[dt.Rows.Count, 8];
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
                    Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                    range.NumberFormatLocal = "@";
                    range.Value2 = objData;
                }
                else if (ShuangKa > 0 && region == "TW")
                {
                    objData = new Object[dt.Rows.Count, 9];
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
                        objData[x, 8] = dr[21].ToString();
                        x++;
                    }
                    Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                    range.NumberFormatLocal = "@";
                    range.Value2 = objData;
                }
                else if (region == "TW")
                {
                    objData = new Object[dt.Rows.Count, 8];
                    foreach (DataRow dr in dt.Rows)
                    {
                        objData[x, 0] = dr[13].ToString();
                        objData[x, 1] = dr[14].ToString();
                        objData[x, 2] = dr[15].ToString();
                        objData[x, 3] = dr[16].ToString();
                        objData[x, 4] = dr[17].ToString();
                        objData[x, 5] = dr[18].ToString();
                        objData[x, 6] = dr[19].ToString();
                        objData[x, 7] = dr[20].ToString();
                        x++;
                    }
                    Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                    range.NumberFormatLocal = "@";
                    range.Value2 = objData;
                }
                else
                {

                    if (ImeiCode == "IMEI06")
                    {
                        objData = new Object[dt.Rows.Count, 10];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else if (ImeiCode == "IMEI05")
                    {
                        objData = new Object[dt.Rows.Count, 10];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else if (ImeiCode == "IMEI01")
                    {
                        objData = new Object[dt.Rows.Count, 8];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else if (ImeiCode == "IMEI02")
                    {
                        objData = new Object[dt.Rows.Count, 10];
                        foreach (DataRow dr in dt.Rows)
                        {
                            objData[x, 0] = dr[13].ToString();
                            objData[x, 1] = dr[14].ToString();
                            objData[x, 2] = dr[15].ToString();
                            objData[x, 3] = dr[16].ToString();
                            objData[x, 4] = dr[17].ToString();
                            objData[x, 5] = dr[18].ToString();
                            objData[x, 6] = dr[19].ToString();
                            objData[x, 7] = dr[20].ToString();
                            objData[x, 8] = dr[21].ToString();
                            x++;
                        }
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 10]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                    else
                    {
                        objData = new Object[dt.Rows.Count, 8];
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
                        Excel.Range range = st.get_Range(st.Cells[n, 1], st.Cells[n + dt.Rows.Count - 1, 8]);
                        range.NumberFormatLocal = "@";
                        range.Value2 = objData;
                    }
                }
                strIMEIFile = string.Format(CreateFile.IMEI_FILE_NAME, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                string ImeiPsd = GetImeiPwd(RevCity);//出LA的設置密碼：出貨City-前三位(首位大寫)+"123"
                if (ImeiCode == "IMEI05" || ImeiCode == "IMEI06")
                {

                    iRet = InsertImeiPwd(strInvoice, ImeiPsd, connTw);
                    wb.SaveAs(strIMEIFile, Type.Missing, ImeiPsd, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //Excel.XlFileFormat.xlExcel8
                    app.ActiveWorkbook.Close(true, strIMEIFile, Type.Missing);
                    app.Workbooks.Close();
                    app.Quit();
                    DateTime Process_AfterTime = DateTime.Now;
                    KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                    GC.Collect();
                }
                else
                {
                    wb.SaveAs(strIMEIFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //Excel.XlFileFormat.xlExcel8
                    app.ActiveWorkbook.Close(true, strIMEIFile, Type.Missing);
                    app.Workbooks.Close();
                    app.Quit();
                    DateTime Process_AfterTime = DateTime.Now;
                    KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                    GC.Collect();
                }
 
            }
     

        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            app.Quit();
            DateTime Process_AfterTime = DateTime.Now;
            KillExcelProcess(Process_BeforeTime, Process_AfterTime);
            GC.Collect();
        }


        return strIMEIFile;
    }

    private string GetImeiPwd(string cityCode)
    {
        return cityCode.Substring(0, 1).ToUpper() + cityCode.Substring(1, 2).ToLower() + "123";
    }

    private int InsertImeiPwd(string DN,string ImeiPwd,OracleConnection conn)
    {
        int iRet = 0;
        string strSql = string.Empty;
        strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET IMEIPSD=:V_PWD WHERE INVOICE=:V_DN";
        OracleParameter[] Parms = new OracleParameter[2];
        Parms[0] = new OracleParameter("V_PWD", ImeiPwd);
        Parms[1] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            if(iRet==0)
             iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);
            
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int  CreateGFSFile(StringBuilder sb, string FilePathName, string CT, string strBatchID, string siteName, string strVersion, string strDN,OracleConnection conn)
    {
        int iRet=0;
        string strHead = string.Empty;
        string strSql = string.Empty;
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
                    iRet =0;
                }

                if (iRet == 0)
                {
                    
                    strSql = @"UPDATE SHP.UPD_DATALOAD_DETAIL_T SET GFS_FILE_TIME=SYSDATE WHERE INVOICE IN ('" + strDN + "')";
                    try
                    {
                        iRet = CheckConnect(conn);
                        if (iRet == 0)
                        {
                            iRet=ExecSqlNoQuery(strSql, conn);
                        }         
                    }
                    catch (Exception ex)
                    {
                        iRet = -1;
                        FERROR = ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {
                iRet = -1;
                FERROR = ex.Message;
            }
        }
        return iRet;
    }

    private int  InsertResult(string strUPDfile, string DN, string CustomerName, string strSapModel, string Country, string Qty, string strShipDate, int Countrep,OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        string strRepSql = string.Empty;
        DateTime ShipDate = Convert.ToDateTime(strShipDate);
        OracleParameter[] Parms = null;
        OracleParameter[] ParmsRep = null;
        if (Countrep == 1)
        {
            strSql = @"INSERT INTO SHP.UPD_DATAlOAD_DETAIL_T(INVOICE,SHIP_QTY,MODEL,SHIP_TO,CUSTOMER,SHIP_DATE,FILENAME,ACTION) " +
                                                 "VALUES(:V_DN,:V_QTY,:V_MODEL,:V_SHIP,:V_CUS,:V_SHIPDATE,:V_FILENAME,:V_ACT)";
            Parms = new OracleParameter[8];
            Parms[0] = new OracleParameter("V_DN", DN);
            Parms[1] = new OracleParameter("V_QTY", Qty);
            Parms[2] = new OracleParameter("V_MODEL", strSapModel);
            Parms[3] = new OracleParameter("V_SHIP", Country);
            Parms[4] = new OracleParameter("V_CUS", CustomerName);
            Parms[5] = new OracleParameter("V_SHIPDATE", ShipDate);
            Parms[6] = new OracleParameter("V_FILENAME", strUPDfile);
            Parms[7] = new OracleParameter("V_ACT", "NORMAL");
        }
        else
        {
            strSql = @"UPDATE SHP.UPD_DATALOAD_DETAIL_T SET COUNTRE=:V_CONT WHERE INVOICE=:V_DN";
            strRepSql = @"INSERT INTO SHP.UPD_REPSEND_DETAIL_T(INVOICE,SHIP_QTY,MODELX,SHIP_TO,CUSTOMER,SHIP_DATE,FILENAME,ACTION) " +
                                                 "VALUES(:V_DN,:V_QTY,:V_MODEL,:V_SHIP,:V_CUS,:V_SHIPDATE,:V_FILENAME,:V_ACT)";
            Parms = new OracleParameter[2];
            Parms[0] = new OracleParameter("V_CONT", Countrep);
            Parms[1] = new OracleParameter("V_DN", DN);

            ParmsRep = new OracleParameter[8];
            ParmsRep[0] = new OracleParameter("V_DN", DN);
            ParmsRep[1] = new OracleParameter("V_QTY", Qty);
            ParmsRep[2] = new OracleParameter("V_MODEL", strSapModel);
            ParmsRep[3] = new OracleParameter("V_SHIP", Country);
            ParmsRep[4] = new OracleParameter("V_CUS", CustomerName);
            ParmsRep[5] = new OracleParameter("V_SHIPDATE", ShipDate);
            ParmsRep[6] = new OracleParameter("V_FILENAME", strUPDfile);
            ParmsRep[7] = new OracleParameter("V_ACT", "NORMAL");

        }
        try
        {
             iRet = CheckConnect(conn);
            iRet = ExecSqlNoQueryWithParam(strSql, Parms,conn);
            if (Countrep > 1)
            {
                iRet = CheckConnect(conn);
                iRet = ExecSqlNoQueryWithParam(strRepSql, ParmsRep,conn);
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int  UpdateImeiTime(string DN, int sendCount,OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        if (sendCount == 1)
        {
            strSql = "UPDATE SHP.UPD_DATALOAD_DETAIL_T SET IMEI_FILE_TIME=SYSDATE WHERE INVOICE=:V_DN AND IMEI_FILE_TIME IS NULL";
        }
        else
        {
            strSql = "UPDATE SHP.UPD_REPSEND_DETAIL_T SET IMEI_FILE_TIME=SYSDATE WHERE INVOICE=:V_DN AND IMEI_FILE_TIME IS NULL";
        }
        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            iRet = ExecSqlNoQueryWithParam(strSql, Parms,conn);

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    /* 產生文件后更新主表UPD_PODN_LIST 
                * 傳入參數 intStatus 
                * 1: 更新UPDFILENAME
                * 2: 更新IMEIFILENAME
                * 3: 更新GFSFILENAME
                * 4: 更新UPDFLAG
                */
    private int UpdateMasterStatus(string DN, string UpdateValue ,int intStatus,OracleConnection conn)
    {
        int iRet=0;
        string strSql = string.Empty;
        switch (intStatus)
        { 
            case 1:
                strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET UPDFILENAME=:V_TRA WHERE INVOICE=:V_DN";
                break;
            case 2:
                strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET IMEIFILENAME=:V_TRA WHERE INVOICE=:V_DN";
                break;
            case 3:
                strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET GFSFILENAME=:V_TRA WHERE INVOICE=:V_DN";
                break;
            case 4:
                strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET UPDFLAG=:V_TRA,CREATE_DATE=SYSDATE WHERE INVOICE=:V_DN";
                break;
            default:
                strSql = "";
                break;
        }
        try
        {
            if (strSql.Length > 0)
            {
                OracleParameter[] Parms = new OracleParameter[2];
                Parms[0] = new OracleParameter("V_TRA", UpdateValue);
                Parms[1] = new OracleParameter("V_DN", DN);

                iRet = CheckConnect(conn);
                if (iRet == 0)
                {
                    iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);
                }
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int UpdateMasterDnModel(string DN, string strModel, string strModelType, OracleConnection conn)
    {
        int iRet = 0;
        string strSql = string.Empty;
        strSql = "UPDATE PUBLIB.UPD_PODN_LIST_T SET SFCMODEL=:V_SMODEL,MODELTYPE=:V_MTYPE WHERE INVOICE=:V_DN";
        OracleParameter[] Parms = new OracleParameter[3];
        Parms[0] = new OracleParameter("V_SMODEL", strModel);
        Parms[1] = new OracleParameter("V_MTYPE", strModelType);
        Parms[2] = new OracleParameter("V_DN", DN);
        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = ExecSqlNoQueryWithParam(strSql, Parms, conn);
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int GetImeiCode(string Region, string SapModel, ref string ImeiCode,OracleConnection conn)
    {
        int iRet = 0;
        string strSql = "SELECT IMEICODE FROM  SHP.UPD_COUNTRY_LINK_IMEICODE WHERE SAPMODEL=:V_SMODEL AND SHIPTOCOUNTRY=:V_REN";
        OracleParameter[] Parms = new OracleParameter[2];
        Parms[0] = new OracleParameter("V_SMODEL", SapModel);
        Parms[1] = new OracleParameter("V_REN", Region);
        try
        {
            OracleDataReader odr = GetDataReader(strSql, Parms,conn);
            while (odr.Read())
            {
                ImeiCode = odr[0].ToString();
                iRet = 0;
                break;
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int  SetInvoiceStatus(string DN,OracleConnection conn)
    {
        int iRet;
        string strSql = string.Empty;
        strSql = "UPDATE SHP.CMCS_SFC_SHIP_CARTON_MAP SET UPLOAD=:V_UP WHERE INVOICE_NO=:V_DN";
        OracleParameter[] Parms = new OracleParameter[2];
        Parms[0] = new OracleParameter("V_UP", "1");
        Parms[1] = new OracleParameter("V_DN", DN);

        try
        {
            iRet = CheckConnect(conn);
            if (iRet == 0)
            {
                iRet = ExecSqlNoQueryWithParam(strSql, Parms,conn);         
            }
  
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }

    private int DouBSwitch(string DN,OracleConnection conn)
    {
        int iRet = 0;
        string strSql = @"SELECT COUNT(C.IMEINUM2) FROM SAP.CMCS_SFC_PACKING_LINES_ALL A, SHP.CMCS_SFC_SHIPPING_DATA B,SHP.CMCS_SFC_IMEINUM C 
                                 WHERE A.INTERNAL_CARTON=B.CARTON_NO AND B.WORK_ORDER = C.PORDER AND  INVOICE_NUMBER=:V_DN  AND ROWNUM=1";

        OracleParameter[] Parms = new OracleParameter[1];
        Parms[0] = new OracleParameter("V_DN", DN);

        try
        {
            iRet = CheckConnect(conn);
            using (OracleDataReader odr = GetDataReader(strSql, Parms,conn))
            {
                while (odr.Read())
                {
                    iRet = Convert.ToInt32(odr[0].ToString());
                    break;
                }
            }
        }
        catch (Exception ex)
        {
      
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int SelectSql(string SQL, OracleConnection conn, ref System.Data.DataTable dt)
    {
        int iRet;
        iRet = CheckConnect(conn);
        OracleDataAdapter oda = null;
        OracleCommand cmd = null;
        try
        {
            cmd = new OracleCommand(SQL, conn);
            oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(dt);
            iRet = 0;
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (oda != null) oda.Dispose();
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    private string GetError()
    {
        return FERROR;
    }

    private int CheckConnect(OracleConnection conn)
    {
        int iRet;
        try
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            FERROR = "";
            iRet = 0;
        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        return iRet;
    }

    private bool CheckFieldValue(string Invoice, string ModelName, string FieldValue, string Fieldimei, int rownum, int FieldIndex, ref string ErrorMessage,OracleConnection conn)
    {
        string errorMessagex = string.Empty;
        switch (FieldIndex)
        {

            case 4: if (FieldValue == "N/A")
                {
                    errorMessagex = "Generation Date  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 5: if (FieldValue == "")
                {
                    errorMessagex = "APC  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                } break;
            case 6: if (FieldValue == "")
                {
                    errorMessagex = "TransceiverModel  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 7: if (FieldValue == "")
                {
                    errorMessagex = "CustomerModel  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 8: if (FieldValue == "")
                {
                    errorMessagex = "MarketName  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 9: if (FieldValue == "")
                {
                    errorMessagex = "ItemCode  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 11: if (FieldValue == "" || FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "ShipDate  is  Null or is 'N/A'";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 12: if (FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "Ship-to Customer Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 13: if (FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "ship-to Customer Address ID  is  Null or is 'N/A' ";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 14: if (FieldValue == "" || FieldValue.ToUpper() == "N/A")
                {
                    errorMessagex = "ShipToCustomerName  is  Null or is 'N/A'";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 15: if (FieldValue == "")
                {
                    errorMessagex = "ShipToCity  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 16: if (FieldValue == "")
                {
                    errorMessagex = "ShipToCountry  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            case 18: if (FieldValue == "N/A")
                {
                    errorMessagex = "Sold-to Customer Name  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 20: if (FieldValue == "")
                {
                    errorMessagex = "Track ID  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 23: if (FieldValue == "")
                {
                    errorMessagex = "Po Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 24: if (FieldValue == "")
                {
                    errorMessagex = "So Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;
            case 26: //NKEY
                if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
                {
                    errorMessagex = "NetworkUnlockCode  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            case 29: //NSKEY
                if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
                {
                    errorMessagex = "SIMUnlockCode  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            //case 30: //PRIVILEGEPWD
            //    if (FieldValue == "" && (ModelName == "A3G" || ModelName == "A2G"))
            //    {
            //        errorMessagex = "ServicePasscode  is  Null";
            //        InserterrorMessage(Invoice, errorMessagex, Fieldimei);
            //        ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
            //        return false;
            //    }
            //    break;

            case 35: if (FieldValue == "")
                {
                    errorMessagex = "MSN  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                } break;
            case 56: if (FieldValue == "")
                {
                    errorMessagex = "Shipment Number  is  Null";
                    InserterrorMessage(Invoice, errorMessagex, Fieldimei,conn);
                    ErrorMessage = "UPD FILE Invoice : " + Invoice + "->" + rownum + " 行" + FieldIndex + " 列," + errorMessagex;
                    return false;
                }
                break;

            default:
                return true;

        }
        return true;

    }

    private int  MailIMEI(string strKeyWord, string strFilePath, string strFileName, string strErrMsg, string strShipCode, string strstrmodule, string strstrshipcountry,OracleConnection conn)
    {
        int iRet;
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
        strTemp += strKeyWord + "文件每天手動產生";
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
        strSql = @"SELECT distinct  MAIL_ADDR FROM SHP.R_IMEI_COUNTRY_LIST A
                                        WHERE  A.SHIP_TO_CODE='" + strshipcode + "'";// CREATE_TIME>SYSDATE-1 
        try
        {
            iRet = CheckConnect(conn);
            using (OracleDataReader odr = GetDataReader(strSql,conn))
            {
                if (odr.Read())
                {
                    strSendto = odr[0].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        //mail.SendTo = strSendto;
        mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
        mail.SendCC = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
        mail.SendFrom = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
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
            iRet = CheckConnect(conn);
            iRet = ExecSqlNoQuery(strSql,conn);

        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }

        return iRet;
    }

    private int AddMail(string strKeyWord, string strFilePath, string strFileName, string strErrMsg, string strShipCode,OracleConnection conn)
    {
        int iRet;
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
        strTemp += strKeyWord + "文件每天手動產生！";
        mail.NoteContent = strTemp;

        if (strErrMsg.ToUpper().Equals("OK"))
        {
            mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
            mail.SendCC = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "CC");
            mail.SendFrom = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "FROM");
        }
        else
        {
            mail.SendTo = Xml.XmlGetValue("CONFIG", "ERRORMAILINFO", "TO");
            mail.SendCC = Xml.XmlGetValue("CONFIG", "ERRORMAILINFO", "CC");
            mail.SendFrom = Xml.XmlGetValue("CONFIG", "ERRORMAILINFO", "FROM");
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
           iRet= CheckConnect(conn);
           if (iRet == 0)
           {
               iRet = ExecSqlNoQuery(strSql,conn);
           }          
        
        }

        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int MailGFS(string invoice, string qty, string customertype, string gfsfilename, string filename,OracleConnection conn)
    {
        int  iRet=0;
        string strSql = string.Empty;
        string strSendto = string.Empty;
        string strKeyWord = customertype + " GFS FILE";

        Mail mail = new Mail();
        mail.SendProgram = "QAZ GFS File";
        mail.CreateTime = DateTime.Now;
        mail.FinishedMark = "0";


        string strTemp = string.Empty;

        mail.NoteSubject = "(FIH-TJ) " + strKeyWord + "  Send Auto Notice:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        strTemp += "File Create OK\r\n";
        strTemp += "FileName : " + filename;

        strTemp += "\r\n";
        strTemp += "\r\n";
        strTemp += strKeyWord + "文件每天由系統自動發送";
        mail.NoteContent = strTemp;

        //   mail.SendTo = Xml.XmlGetValue("CONFIG", "SUCCEEDMAILINFO", "TO");
        mail.SendTo = Xml.XmlGetValue("CONFIG", "GFSMAILINFO", "TO");
        mail.SendCC = Xml.XmlGetValue("CONFIG", "GFSMAILINFO", "CC");
        mail.SendFrom = Xml.XmlGetValue("CONFIG", "GFSMAILINFO", "FROM");

        strSql = @"INSERT INTO SFC.C_NOTES_SEND (
                                   NOTE_SUBJECT, NOTE_CONTENT, SEND_TO,SEND_CC, SEND_FROM, SEND_PROG, 
                                   CREATE_DATE, FINISHED_MARK,ATTACHED_PATH) 
                                   VALUES ( '" + mail.NoteSubject + "', '" + mail.NoteContent + "', '" + mail.SendTo + @"',
                                    '" + mail.SendCC + "', '" + mail.SendFrom + "', '" + mail.SendProgram + @"',
                                    TO_DATE('" + mail.CreateTime.ToString("yyyy/MM/dd HH:mm:ss") +
                     "','YYYY/MM/DD HH24:MI:SS'),  '" + mail.FinishedMark + "','" + gfsfilename + "')";
        try
        {
            iRet = CheckConnect(conn);
            if(iRet==0)
            {
                iRet = ExecSqlNoQuery(strSql, conn);
            }      
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        return iRet;
    }

    private int  ExecSqlNoQueryWithParam(string strSql, OracleParameter[] cmdParms,OracleConnection conn)
    {
        int iRet;
        OracleCommand cmd = null;
        int intRet = CheckConnect(conn);
        try
        {
            cmd = new OracleCommand(strSql, conn);

            for (int i = 0; i < cmdParms.GetLength(0); i++)
                cmd.Parameters.Add(cmdParms[i]);
            iRet = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    private int ExecSqlNoQuery(string strSql,OracleConnection conn)
    {
        int iRet;
        OracleCommand cmd = null;
        int intRet = CheckConnect(conn);
        try
        {
            cmd = new OracleCommand(strSql, conn);
            iRet = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    private DataSet GetDataSet(string strBySql,  OracleParameter[] cmdParms,OracleConnection conn)
    {
        int iRet=0;
        DataSet odr = new DataSet();
        OracleCommand oCmd = null;
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Clear();
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    oCmd.Parameters.Add(parm);
            }
            OracleDataAdapter dd = new OracleDataAdapter();
            dd.SelectCommand = oCmd;
            dd.Fill(odr);
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }
        return odr;
    }

    private DataSet GetDataSet(string strBySql,OracleConnection conn)
    {
        int iRet = CheckConnect(conn);
        DataSet odr = new DataSet();
        OracleCommand oCmd = null;
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            OracleDataAdapter dd = new OracleDataAdapter();
            dd.SelectCommand = oCmd;
            dd.Fill(odr);
        }
        catch (OracleException ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }
        return odr;
    }

    private OracleDataReader GetDataReader(string strBySql,OracleConnection conn)
    {
        int iRet;
        iRet = CheckConnect(conn);
        OracleDataReader odr = null;
        OracleCommand oCmd = null;
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            odr = oCmd.ExecuteReader();
        }
        catch (OracleException ex)
        {
            FERROR = ex.Message;
        }
        finally
        {
            if (oCmd != null) oCmd.Dispose();
        }
        return odr;
    }

    private OracleDataReader GetDataReader(string strBySql,OracleParameter[] cmdParms,OracleConnection conn)
    {
        int iRet;
        iRet = CheckConnect(conn);
        OracleDataReader odr = null;
        OracleCommand oCmd = null;
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Clear();
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    oCmd.Parameters.Add(parm);
            }
            odr = oCmd.ExecuteReader();
            oCmd.Parameters.Clear();
        }
        catch (OracleException ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }
        return odr;
    }

    private OracleDataReader GetDataReaderWithParm(string strBySql,OracleParameter[] cmdParms,OracleConnection conn)
    {
        int iRet;
        iRet = CheckConnect(conn);
        OracleDataReader odr = null;
        OracleCommand oCmd = null;
        try
        {
            oCmd = new OracleCommand(strBySql, conn);
            oCmd.CommandType = CommandType.Text;
            oCmd.Parameters.Clear();
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    oCmd.Parameters.Add(parm);
            }
            odr = oCmd.ExecuteReader();
            oCmd.Parameters.Clear();
        }
        catch (OracleException ex)
        {
            FERROR = ex.Message;
        }
        finally
        {
            if (oCmd != null)
            {
                oCmd.Dispose();
            }
        }
        return odr;
    }

    private static void KillExcelProcess(DateTime Process_BeforeTime, DateTime Process_AfterTime)
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

}

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

public class Xml
{
    public static string GetConfigPath()
    {
        return AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
    }

    public static string XmlGetValue(string psMain, string psSub, string psThree)
    {
        string sReturn = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GetConfigPath());
            XmlNode node1 = doc.SelectSingleNode("/" + psMain + "/" + psSub + "/" + psThree);
            sReturn = node1.InnerText;
        }
        catch { }
        return sReturn;
    }

    public static bool XmlSetValuestring(string psMain, string psSub, string psThree, string psKeyValue)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GetConfigPath());
            XmlNode node1 = doc.SelectSingleNode("/" + psMain + "/" + psSub + "/" + psThree);
            node1.InnerText = psKeyValue;
            doc.Save(GetConfigPath());
            return true;
        }
        catch { return false; }
    }

    public static bool NodeIsExist(string psMain, string psSub)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GetConfigPath());
            XmlNodeList xnList = doc.SelectNodes("/" + psMain + "/" + psSub);
            if (xnList.Count == 0)
            {
                return false;
            }
            return true;
        }
        catch { return false; }
    }
}

public class FTPClient
{

    #region 篶硑ㄧ计
    /// <summary>
    /// 篶硑ㄧ计
    /// </summary>
    public FTPClient()
    {
        strRemoteHost = "";
        strRemotePath = "";
        strRemoteUser = "";
        strRemotePass = "";
        strRemotePort = 21;
        bConnected = false;
    }

    /// <summary>
    /// 篶硑ㄧ计
    /// </summary>
    /// <param name="remoteHost"></param>
    /// <param name="remotePath"></param>
    /// <param name="remoteUser"></param>
    /// <param name="remotePass"></param>
    /// <param name="remotePort"></param>
    public FTPClient(string remoteHost, string remotePath, string remoteUser, string remotePass, int remotePort)
    {
        strRemoteHost = remoteHost;
        strRemotePath = remotePath;
        strRemoteUser = remoteUser;
        strRemotePass = remotePass;
        strRemotePort = remotePort;
        Connect();
    }
    #endregion

    #region 祅嘲
    /// <summary>
    /// FTP狝竟IP
    /// </summary>
    private string strRemoteHost;
    public string RemoteHost
    {
        get
        {
            return strRemoteHost;
        }
        set
        {
            strRemoteHost = value;
        }
    }
    /// <summary>
    /// FTP狝竟梆
    /// </summary>
    private int strRemotePort;
    public int RemotePort
    {
        get
        {
            return strRemotePort;
        }
        set
        {
            strRemotePort = value;
        }
    }
    /// <summary>
    /// 讽玡狝竟ヘ魁
    /// </summary>
    private string strRemotePath;
    public string RemotePath
    {
        get
        {
            return strRemotePath;
        }
        set
        {
            strRemotePath = value;
        }
    }
    /// <summary>
    /// 祅魁ㄏノ眀腹
    /// </summary>
    private string strRemoteUser;
    public string RemoteUser
    {
        set
        {
            strRemoteUser = value;
        }
    }
    /// <summary>
    /// ㄏノ祅魁盞絏
    /// </summary>
    private string strRemotePass;
    public string RemotePass
    {
        set
        {
            strRemotePass = value;
        }
    }

    /// <summary>
    /// 琌祅魁
    /// </summary>
    private Boolean bConnected;
    public bool Connected
    {
        get
        {
            return bConnected;
        }
    }
    #endregion

    #region 硈挡
    /// <summary>
    /// ミ硈钡 
    /// </summary>
    public void Connect()
    {
        IPEndPoint main_ipEndPoint;
        socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
#if NET1
                main_ipEndPoint = new IPEndPoint(Dns.GetHostByName(RemoteHost).AddressList[0], strRemotePort);
#else
        main_ipEndPoint = new IPEndPoint(System.Net.Dns.GetHostEntry(RemoteHost).AddressList[0], strRemotePort);
#endif
        // 硈挡
        try
        {
            socketControl.Connect(main_ipEndPoint);
        }
        catch (Exception)
        {
            CreateFile.Write("Couldn't connect to remote server");
            throw new IOException("Couldn't connect to remote server");
        }

        // 莉莱氮絏
        ReadReply();
        if (iReplyCode != 220)
        {
            DisConnect();
            throw new IOException(strReply.Substring(4));
        }

        // 祅嘲
        SendCommand("USER " + strRemoteUser);
        if (!(iReplyCode == 331 || iReplyCode == 230))
        {
            CloseSocketConnect();//闽超硈钡
            throw new IOException(strReply.Substring(4));
        }
        if (iReplyCode != 230)
        {
            SendCommand("PASS " + strRemotePass);
            if (!(iReplyCode == 230 || iReplyCode == 202))
            {
                CloseSocketConnect();//闽超硈钡
                throw new IOException(strReply.Substring(4));
            }
        }
        bConnected = true;

        // ち传ヘ魁
        ChDir(strRemotePath);
    }


    /// <summary>
    /// 闽超硈钡
    /// </summary>
    public void DisConnect()
    {
        if (socketControl != null)
        {
            SendCommand("QUIT");
        }
        CloseSocketConnect();
    }

    #endregion

    #region 肚块家Α

    /// <summary>
    /// 肚块家Α:秈摸ASCII摸
    /// </summary>
    public enum TransferType { Binary, ASCII };

    /// <summary>
    /// 砞竚肚块家Α
    /// </summary>
    /// <param name="ttType">肚块家Α</param>
    public void SetTransferType(TransferType ttType)
    {
        if (ttType == TransferType.Binary)
        {
            SendCommand("TYPE I");//binary摸肚块
        }
        else
        {
            SendCommand("TYPE A");//ASCII摸肚块
        }
        if (iReplyCode != 200)
        {
            throw new IOException(strReply.Substring(4));
        }
        else
        {
            trType = ttType;
        }
    }


    /// <summary>
    /// 莉眔肚块家Α
    /// </summary>
    /// <returns>肚块家Α</returns>
    public TransferType GetTransferType()
    {
        return trType;
    }

    #endregion

    #region 郎巨
    /// <summary>
    /// 莉眔郎睲虫
    /// </summary>
    /// <param name="strMask">郎で皌﹃</param>
    /// <returns></returns>
    public string[] Dir(string strMask)
    {
        // ミ硈挡
        if (!bConnected)
        {
            Connect();
        }

        //ミ秈︽戈硈钡socket
        Socket socketData = CreateDataSocket();

        //肚癳㏑
        SendCommand("NLST " + strMask);

        if (iReplyCode == 550)
        {
            return new string[] { };
        }

        //だ猂莱氮絏
        if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226))
        {
            CreateFile.Write(strReply.Substring(4));
            throw new IOException(strReply.Substring(4));
        }

        //莉眔挡狦
        strMsg = "";
        while (true)
        {
            int iBytes = socketData.Receive(buffer, buffer.Length, 0);
            strMsg += ASCII.GetString(buffer, 0, iBytes);
            if (iBytes < buffer.Length)
            {
                break;
            }
        }
        char[] seperator = { '\n' };
        string[] strsFileList = strMsg.Replace("\r", "").Split(seperator);
        socketData.Close();//戈socket闽超穦Τ絏
        if (iReplyCode != 226)
        {
            ReadReply();
            if (iReplyCode != 226)
            {
                CreateFile.Write(strReply.Substring(4));
                throw new IOException(strReply.Substring(4));
            }
        }
        return strsFileList;
    }


    /// <summary>
    /// 莉郎
    /// </summary>
    /// <param name="strFileName">郎</param>
    /// <returns>郎</returns>
    public long GetFileSize(string strFileName)
    {
        if (!bConnected)
        {
            Connect();
        }
        SendCommand("SIZE " + Path.GetFileName(strFileName));
        long lSize = 0;
        string strtmp = strReply.Replace("213 ", "").Replace("\r", "");
        if (iReplyCode == 213)
        {
            lSize = Int64.Parse(strtmp);
        }
        else
        {
            ;//throw new IOException(strtmp);
        }
        return lSize;
    }


    /// <summary>
    /// 埃
    /// </summary>
    /// <param name="strFileName">埃郎</param>
    public void Delete(string strFileName)
    {
        if (!bConnected)
        {
            Connect();
        }
        SendCommand("DELE " + strFileName);
        if (iReplyCode != 250)
        {
            throw new IOException(strReply.Substring(4));
        }
    }

    /// <summary>
    /// ㏑(狦穝郎籔Τ郎,盢滦籠Τ郎)
    /// </summary>
    /// <param name="strOldFileName">侣郎</param>
    /// <param name="strNewFileName">穝郎</param>
    public void Rename(string strOldFileName, string strNewFileName)
    {
        if (!bConnected)
        {
            Connect();
        }
        SendCommand("RNFR " + strOldFileName);
        if (iReplyCode != 350)
        {
            throw new IOException(strReply.Substring(4));
        }
        // 狦穝郎籔Τ郎,盢滦籠Τ郎
        SendCommand("RNTO " + strNewFileName);
        if (iReplyCode != 250)
        {
            throw new IOException(strReply.Substring(4));
        }
    }
    #endregion

    #region 肚㎝更
    /// <summary>
    /// 更у郎
    /// </summary>
    /// <param name="strFileNameMask">郎で皌﹃</param>
    /// <param name="strFolder">セヘ魁(ぃ眔\挡)</param>
    public void Get(string strFileNameMask, string strFolder)
    {
        if (!bConnected)
        {
            Connect();
        }
        string[] strFiles = Dir(strFileNameMask);
        foreach (string strFile in strFiles)
        {
            if (!strFile.Equals(""))//ㄓ弧strFiles程じ琌﹃
            {
                Get(strFile, strFolder, strFile);
            }
        }
    }


    /// <summary>
    /// 更郎
    /// </summary>
    /// <param name="strRemoteFileName">璶更郎</param>
    /// <param name="strFolder">セヘ魁(ぃ眔\挡)</param>
    /// <param name="strLocalFileName">玂セ郎</param>
    public void Get(string strRemoteFileName, string strFolder, string strLocalFileName)
    {
        if (!bConnected)
        {
            Connect();
        }
        SetTransferType(TransferType.Binary);
        if (strLocalFileName.Equals(""))
        {
            strLocalFileName = strRemoteFileName;
        }
        if (!File.Exists(strFolder + "\\" + strLocalFileName))
        {
            File.Delete(strFolder + "\\" + strLocalFileName);
        }
        FileStream output = new
            FileStream(strFolder + "\\" + strLocalFileName, FileMode.Create);
        Socket socketData = CreateDataSocket();
        SendCommand("RETR " + strRemoteFileName);
        if (!(iReplyCode == 150 || iReplyCode == 125
            || iReplyCode == 226 || iReplyCode == 250))
        {
            CreateFile.Write(strReply.Substring(4));
            throw new IOException(strReply.Substring(4));
        }
        while (true)
        {
            int iBytes = socketData.Receive(buffer, buffer.Length, 0);
            output.Write(buffer, 0, iBytes);
            if (iBytes <= 0)
            {
                break;
            }
        }
        output.Close();
        if (socketData.Connected)
        {
            socketData.Close();
        }
        if (!(iReplyCode == 226 || iReplyCode == 250))
        {
            ReadReply();
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                CreateFile.Write(strReply.Substring(4));
                throw new IOException(strReply.Substring(4));
            }
        }
    }


    /// <summary>
    /// 肚у郎
    /// </summary>
    /// <param name="strFolder">セヘ魁(ぃ眔\挡)</param>
    /// <param name="strFileNameMask">郎で皌じ(*㎝?)</param>
    public void Put(string strFolder, string strFileNameMask)
    {
        string[] strFiles = Directory.GetFiles(strFolder, strFileNameMask);
        foreach (string strFile in strFiles)
        {
            //strFile琌Ч俱郎(隔畖)
            Put(strFile);
        }
    }


    /// <summary>
    /// 肚郎
    /// </summary>
    /// <param name="strFileName">セ郎</param>
    public void Put(string strFileName)
    {
        if (!bConnected)
        {
            Connect();
        }
        Socket socketData = CreateDataSocket();
        SendCommand("STOR " + Path.GetFileName(strFileName));
        if (!(iReplyCode == 125 || iReplyCode == 150))
        {
            throw new IOException(strReply.Substring(4));
        }
        FileStream input = new
            FileStream(strFileName, FileMode.Open);
        int iBytes = 0;
        while ((iBytes = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            socketData.Send(buffer, iBytes, 0);
        }
        input.Close();
        if (socketData.Connected)
        {
            socketData.Close();
        }
        if (!(iReplyCode == 226 || iReplyCode == 250))
        {
            ReadReply();
            if (!(iReplyCode == 226 || iReplyCode == 250))
            {
                throw new IOException(strReply.Substring(4));
            }
        }
    }

    #endregion

    #region ヘ魁巨
    /// <summary>
    /// 承ヘ魁
    /// </summary>
    /// <param name="strDirName">ヘ魁</param>
    public void MkDir(string strDirName)
    {
        if (!bConnected)
        {
            Connect();
        }
        SendCommand("MKD " + strDirName);
        if (iReplyCode != 257)
        {
            throw new IOException(strReply.Substring(4));
        }
    }


    /// <summary>
    /// 埃ヘ魁
    /// </summary>
    /// <param name="strDirName">ヘ魁</param>
    public void RmDir(string strDirName)
    {
        if (!bConnected)
        {
            Connect();
        }
        SendCommand("RMD " + strDirName);
        if (iReplyCode != 250)
        {
            throw new IOException(strReply.Substring(4));
        }
    }


    /// <summary>
    /// э跑ヘ魁
    /// </summary>
    /// <param name="strDirName">穝ヘ魁</param>
    public void ChDir(string strDirName)
    {
        if (strDirName.Equals(".") || strDirName.Equals(""))
        {
            return;
        }
        if (!bConnected)
        {
            Connect();
        }
        SendCommand("CWD " + strDirName);
        if (iReplyCode != 250)
        {
            throw new IOException(strReply.Substring(4));
        }
        this.strRemotePath = strDirName;
    }

    #endregion

    #region ?场?秖
    /// <summary>
    /// 狝?竟?氮獺(?氮?)
    /// </summary>
    private string strMsg;
    /// <summary>
    /// 狝?竟?氮獺(?氮?)
    /// </summary>
    private string strReply;
    /// <summary>
    /// 狝?竟?氮?
    /// </summary>
    private int iReplyCode;
    /// <summary>
    /// ?︽北?钡socket
    /// </summary>
    private Socket socketControl;
    /// <summary>
    /// ??家Α
    /// </summary>
    private TransferType trType;
    /// <summary>
    /// 钡Μ㎝?癳?誹???
    /// </summary>
    private static int BLOCK_SIZE = 512;
    Byte[] buffer = new Byte[BLOCK_SIZE];
    /// <summary>
    /// ??よΑ
    /// </summary>
    Encoding ASCII = Encoding.ASCII;
    #endregion

    #region ?场ㄧ?
    /// <summary>
    /// ?︽?氮才﹃??strReply㎝strMsg
    /// ?氮???iReplyCode
    /// </summary>
    private void ReadReply()
    {
        strMsg = "";
        strReply = ReadLine();
        iReplyCode = Int32.Parse(strReply.Substring(0, 3));
    }
    /// <summary>
    /// ミ秈︽戈硈钡socket
    /// </summary>
    /// <returns>戈硈钡socket</returns>
    private Socket CreateDataSocket()
    {
        SendCommand("PASV");
        if (iReplyCode != 227)
        {
            throw new IOException(strReply.Substring(4));
        }
        int index1 = strReply.IndexOf('(');
        int index2 = strReply.IndexOf(')');
        string ipData =
            strReply.Substring(index1 + 1, index2 - index1 - 1);
        int[] parts = new int[6];
        int len = ipData.Length;
        int partCount = 0;
        string buf = "";
        for (int i = 0; i < len && partCount <= 6; i++)
        {
            char ch = Char.Parse(ipData.Substring(i, 1));
            if (Char.IsDigit(ch))
                buf += ch;
            else if (ch != ',')
            {
                throw new IOException("Malformed PASV strReply: " +
                    strReply);
            }
            if (ch == ',' || i + 1 == len)
            {
                try
                {
                    parts[partCount++] = Int32.Parse(buf);
                    buf = "";
                }
                catch (Exception)
                {
                    throw new IOException("Malformed PASV strReply: " +
                        strReply);
                }
            }
        }
        string ipAddress = parts[0] + "." + parts[1] + "." +
            parts[2] + "." + parts[3];
        int port = (parts[4] << 8) + parts[5];
        Socket s = new
            Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ep = new
            IPEndPoint(IPAddress.Parse(ipAddress), port);
        try
        {
            s.Connect(ep);
        }
        catch (Exception)
        {
            throw new IOException("Can't connect to remote server");
        }
        return s;
    }


    /// <summary>
    /// 闽超socket硈钡(ノ祅魁玡)
    /// </summary>
    private void CloseSocketConnect()
    {
        if (socketControl != null)
        {
            socketControl.Close();
            socketControl = null;
        }
        bConnected = false;
    }

    /// <summary>
    /// 弄Socket┮Τ﹃
    /// </summary>
    /// <returns>莱氮絏﹃︽</returns>
    private string ReadLine()
    {
        while (true)
        {
            int iBytes = socketControl.Receive(buffer, buffer.Length, 0);
            strMsg += ASCII.GetString(buffer, 0, iBytes);
            if (iBytes < buffer.Length)
            {
                break;
            }
        }
        char[] seperator = { '\n' };
        string[] mess = strMsg.Split(seperator);
        if (strMsg.Length > 2)
        {
            strMsg = mess[mess.Length - 2];
            //seperator[0]琌10,だ︽才腹琌パ13㎝0舱Θ,だ筳10瘤⊿Τ﹃,
            //穦だ皌﹃倒(琌程)﹃皚,
            //┮程mess琌⊿ノ﹃
            //ぐ或ぃ钡mess[0],Τ程︽﹃莱氮絏籔戈癟ぇ丁Τ
        }
        else
        {
            strMsg = mess[0];
        }
        if (!strMsg.Substring(3, 1).Equals(" "))//﹃タ絋琌莱氮絏(220秨繷,钡,钡拜﹃)
        {
            return ReadLine();
        }
        return strMsg;
    }


    /// <summary>
    /// 祇癳㏑莉莱氮絏㎝程︽莱氮﹃
    /// </summary>
    /// <param name="strCommand">㏑</param>
    private void SendCommand(String strCommand)
    {
        Byte[] cmdBytes =
            Encoding.ASCII.GetBytes((strCommand + "\r\n").ToCharArray());
        socketControl.Send(cmdBytes, cmdBytes.Length, 0);
        ReadReply();
    }

    #endregion

}