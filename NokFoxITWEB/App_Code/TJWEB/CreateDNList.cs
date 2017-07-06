using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Adapter.SAP;
using System.Globalization;
using System.IO;

/// <summary>
/// Summary description for CreateDNList
/// </summary>
public class CreateDNList
{
	public CreateDNList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Create_DnList1(string io, string dbtype, string DBReadString, string DBWriString, string DBWriteTString, string Autoprg)
    {
        int iRet=0;
        int li=0;
        try
        {
            if (io != "1")
            {
                iRet = 0;
                return iRet;
            }
            DataBaseOperation dboRead = new DataBaseOperation(dbtype, DBReadString);//211
            DataBaseOperation dboWrite = new DataBaseOperation(dbtype, DBWriString);//76
            DataBaseOperation dboWriteT = new DataBaseOperation(dbtype, DBWriteTString);//221

            string FilePath = @"D:\UPDFile";
            string Filename = "AS_FIHMLXTJ_UPD_AUDIT_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".dat";
            string FilePathName = FilePath + @"\" + Filename;

            string strsql1 = "select distinct DN from SAP.SHIPPING_DN_LIST";
            DataTable dtdn = dboWriteT.SelectSQLDT(strsql1);
            string dn = "";
            string sql = "";
            if (dtdn.Rows.Count > 0)
            {

                for (int i = 0; i < dtdn.Rows.Count - 1; i++)
                {
                    dn = dn + "'" + dtdn.Rows[i]["DN"].ToString().Trim() + "',";
                }
                dn = dn + "'" + dtdn.Rows[dtdn.Rows.Count - 1]["DN"].ToString().Trim() + "'";
                 sql = @"select INVOICE,to_char(CREATE_DATE,'dd-MM-yy') CREATE_DATE from PUBLIB.UPD_PODN_LIST_T 
                  where sapflag='Y' and CREATE_DATE>sysdate-7 and INVOICE not in(" + dn + ")  order by CREATE_DATE desc";
            }
            else {
                 sql = @"select INVOICE,to_char(CREATE_DATE,'dd-MM-yy') CREATE_DATE from PUBLIB.UPD_PODN_LIST_T 
                  where sapflag='Y' and CREATE_DATE>sysdate-7 order by CREATE_DATE desc";
            }
            
//            string sql = @"select INVOICE,to_char(CREATE_DATE,'dd-MM-yy') CREATE_DATE from PUBLIB.UPD_PODN_LIST_T 
//                where sapflag='Y' and CREATE_DATE>sysdate-7 and INVOICE not in(select DN from SAP.SHIPPING_DN_LIST)  order by CREATE_DATE desc";
            DataTable dt = dboWriteT.SelectSQLDT(sql);
            li=dt.Rows.Count;
            if (li > 0)
            {
                for (int i = 0; i < li; i++)
                {
                    string Strsql = @"SELECT DISTINCT 'AS' region_id,'FIHMLXTJ' factory_id, 'ZFOXTJ-ODM' system_id,
                        b.so_number sales_order, a.customer_po customer_po_number,
                        b.ship_tocustomer_name ship_to_customer_name,
                        a.ship_to_country ship_to_customer_country,
                        c.market_name market_model,
                        DECODE (g.cust_pno,
                                NULL, 'unknown',
                                g.cust_pno
                               ) transceiver_model,
                        DECODE (g.sa_no, NULL, g.cust_pno, g.sa_no) customer_model,
                        DECODE (d.prgcode,
                                'UPD05', 'GSM',
                                'UPD06', 'CDMA',
                                ''
                               ) handset_technology_type,
                        SUBSTR (e.customer_num, 1, 3) item_apc_code,
                        f.shipped_qty unit_quantity,'"+dt.Rows[i]["CREATE_DATE"].ToString().Trim()+"' ship_date,";
                    Strsql = Strsql + @" A.INVOICE_NUMBER
                        FROM sap.cmcs_sfc_packing_lines_all a,
                        shp.upd_order_information b,
                        shp.ros_tch_pn c,
                        sfc.cmcs_sfc_model d,
                        shp.cmcs_sfc_imeinum e,
                        sap.sap_invoice_info f,
                        shp.cmcs_sfc_shipping_data g
                        WHERE a.invoice_number='"+dt.Rows[i]["INVOICE"].ToString().Trim()+"'";
                   Strsql=Strsql+@" and a.invoice_number = f.invoice
                        AND a.customer_po = b.po_number
                        AND a.internal_carton = g.carton_no
                        AND g.imei = e.imeinum
                        AND g.MODEL = d.MODEL
                        and c.PPART=E.PPART";
                   DataTable dts = dboRead.SelectSQLDT(Strsql);
                   if (dts.Rows.Count > 0)
                   {
                       int mm = CreateOne_DnList(dts, dboWrite, dboWriteT, dboRead, FilePath, FilePathName);
                   }
                        

                }
            }
        }
        catch(Exception ex)
        {
             string sxstr = ex.Message;
             iRet = 0;
             return iRet;
        }
        finally
        {
            
        }
        return iRet;
    }
    public int Create_DnList2(string io, string dbtype, string DBReadString, string DBWriString, string DBWriteTString, string Autoprg)
    {
        int iRet = 0;
        int li = 0;
        try
        {
            if (io != "1")
            {
                iRet = 0;
                return iRet;
            }
            DataBaseOperation dboRead = new DataBaseOperation(dbtype, DBReadString);//211
            DataBaseOperation dboWriteT = new DataBaseOperation(dbtype, DBWriString);//76--221
            DataBaseOperation dboWrite = new DataBaseOperation(dbtype, DBWriteTString);//221--76

            string FilePath = @"D:\ReadWeb\Motor\DNLIST";
            string Filename = "AS_FIHMLXTJ_UPD_AUDIT_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".dat";
            string FilePathName = FilePath + @"\" + Filename;
            string sql="";
          // if (DBWriString.Contains("76"))
            sql = @"select INVOICE_NUMBER, MAX(LAST_UPDATE_DATE) dates, substr(ITEM_NUMBER,3,3) model from SAP.CMCS_SFC_PACKING_LINES_ALL 
                            where LAST_UPDATE_DATE>sysdate-7
                            and substr(ITEM_NUMBER,3,3) not in(select distinct substr(model,1,3)  from PUBLIB.CUSTOMERMODEL)
                            and INVOICE_NUMBER not in (select distinct DN from SAP.SHIPPING_DN_LIST)
                             group by INVOICE_NUMBER, ITEM_NUMBER order by dates desc";
//           else
//           {
//               string strsql1 = "select distinct DN from SAP.SHIPPING_DN_LIST";
//               DataTable dtdn = dboWriteT.SelectSQLDT(strsql1);
//               string dn = "";
//               if (dtdn.Rows.Count > 0)
//               {

//                   for (int i = 0; i < dtdn.Rows.Count - 1; i++)
//                   {
//                       dn = dn + "'" + dtdn.Rows[i]["DN"].ToString().Trim() + "',";
//                   }
//                   dn = dn + "'" + dtdn.Rows[dtdn.Rows.Count - 1]["DN"].ToString().Trim() + "'";
//                   sql = @"select INVOICE_NUMBER, MAX(LAST_UPDATE_DATE) dates from SAP.CMCS_SFC_PACKING_LINES_ALL 
//                            where LAST_UPDATE_DATE>sysdate-7 
//                            and INVOICE_NUMBER not in (" + dn + ") group by INVOICE_NUMBER order by INVOICE_NUMBER desc";
//               }
//               else
//               {
//                   sql = @"select INVOICE_NUMBER, MAX(LAST_UPDATE_DATE) dates from SAP.CMCS_SFC_PACKING_LINES_ALL 
//                            where LAST_UPDATE_DATE>sysdate-7 
//                            group by INVOICE_NUMBER order by INVOICE_NUMBER desc";
//               }
               
//           }
           DataTable dt = dboRead.SelectSQLDT(sql);//正式改為dboRead
            li = dt.Rows.Count;
            if (li > 0)
            {
                int DNStatus = 2;
                SAPOperate sapo = new SAPOperate();
                string Shap_Date = DateTime.Now.ToString("dd-MMM-yy", CultureInfo.CreateSpecificCulture("en-US"));
                foreach (DataRow dr in dt.Rows)
                {
                    string checkDN = dr["INVOICE_NUMBER"].ToString().Trim();
                    if (CheckDN(dboWriteT, checkDN))//判斷DN是否存在write數據庫
                    {
                        DNStatus = sapo.CheckDNMovementStatus(checkDN);//獲取銷單信息
                        if (DNStatus == 0)
                        {
                            string Strsql = @"SELECT DISTINCT 'AS' region_id,'FIHMLXTJ' factory_id, 'ZFOXTJ-ODM' system_id,
                        b.so_number sales_order, a.customer_po customer_po_number,
                        b.ship_tocustomer_name ship_to_customer_name,
                        a.ship_to_country ship_to_customer_country,
                        c.market_name market_model,
                        DECODE (g.cust_pno,
                                NULL, 'unknown',
                                g.cust_pno
                               ) transceiver_model,
                        DECODE (g.sa_no, NULL, g.cust_pno, g.sa_no) customer_model,
                        DECODE (d.prgcode,
                                'UPD05', 'GSM',
                                'UPD06', 'CDMA',
                                ''
                               ) handset_technology_type,
                        SUBSTR (e.customer_num, 1, 3) item_apc_code,
                        f.shipped_qty unit_quantity,'" + Shap_Date + "' ship_date,";
                            Strsql = Strsql + @" A.INVOICE_NUMBER
                        FROM sap.cmcs_sfc_packing_lines_all a,
                        shp.upd_order_information b,
                        shp.ros_tch_pn c,
                        sfc.cmcs_sfc_model d,
                        shp.cmcs_sfc_imeinum e,
                        sap.sap_invoice_info f,
                        shp.cmcs_sfc_shipping_data g
                        WHERE a.invoice_number='" + checkDN + "'";
                            Strsql = Strsql + @" and a.invoice_number = f.invoice
                        AND a.customer_po = b.po_number
                        AND a.internal_carton = g.carton_no
                        AND g.imei = e.imeinum
                        AND g.MODEL = d.MODEL
                        and c.PPART=E.PPART";
                            DataTable dts = dboRead.SelectSQLDT(Strsql);
                            if (dts.Rows.Count > 0)
                            {

                                int mm = CreateOne_DnList(dts, dboWrite, dboWriteT, dboRead, FilePath, FilePathName);

                            }
                        }
                    }
                }
              
            }
        }
        catch (Exception ex)
        {
            string sxstr = ex.Message;
            iRet = 0;
            return iRet;
        }
        finally
        {

        }
        return iRet;
    }
    //判斷write數據庫中DN是否存在：由於76和211同步時間可能插入多條
    private bool CheckDN(DataBaseOperation dboWriteT,string DN)
    {
        bool flagDN = false;
        string sql = "select distinct DN from SAP.SHIPPING_DN_LIST where DN='" + DN + "'";
        DataTable dt = dboWriteT.SelectSQLDT(sql);
        int li = dt.Rows.Count;
        if (li.Equals(0))
        {
            flagDN = true;
        }
        return flagDN;
    }
    //插入1條數據到數據庫
    private int CreateOne_DnList(DataTable dt, DataBaseOperation dboWrite, DataBaseOperation dboWriteT, DataBaseOperation dboRead,string FilePath, string FilePathName)
    {
        int iRet = 0;
        try
        {
            int count = dt.Rows.Count;
            string F0 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string F1 = dt.Rows[0]["factory_id"].ToString().Trim();
            string F2 = dt.Rows[0]["system_id"].ToString().Trim();
            string F3 = dt.Rows[0]["sales_order"].ToString().Trim();
            string F4 = dt.Rows[0]["customer_po_number"].ToString().Trim();
            string F5 = dt.Rows[0]["ship_to_customer_name"].ToString().Trim();
            string F6 = dt.Rows[0]["ship_to_customer_country"].ToString().Trim();
            string F7 = dt.Rows[0]["market_model"].ToString().Trim();
            string F8 = dt.Rows[0]["transceiver_model"].ToString().Trim();
            string F9 = dt.Rows[0]["customer_model"].ToString().Trim();
            string F10 = dt.Rows[0]["handset_technology_type"].ToString().Trim();
            string F11 = dt.Rows[0]["item_apc_code"].ToString().Trim();
            string F12 = dt.Rows[0]["unit_quantity"].ToString().Trim();
            string F13 = dt.Rows[0]["ship_date"].ToString().Trim();
            string F14 = dt.Rows[0]["INVOICE_NUMBER"].ToString().Trim();
            string F15 = F0;
            string strSql = @"insert into SAP.SHIPPING_DN_LIST (CREATE_TIME,FACTORY_ID,SYSTEM_ID,SALES_ORDER, CUSTOMER_PO_NUMBER,SHIP_TO_CUSTOMER_NAME,SHIP_TO_CUSTOMER_COUNTRY,MARKET_MODEL,TRANSCEIVER_MODEL,CUSTOMER_MODEL,
                HANDSET_TECHNOLOGY_TYPE,ITEM_APC_CODE,UNIT_QUANTITY,SHIP_DATE,DN,DOCUMENTID) values(:V_F0,:V_F1,:V_F2,:V_F3,:V_F4,:V_F5,:V_F6,:V_F7,:V_F8,:V_F9,:V_F10,:V_F11,:V_F12,:V_F13,:V_F14,:V_F15)";
            iRet = dboWriteT.ExecSQL(strSql, new string[] { "V_F0", "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6", "V_F7", "V_F8", "V_F9", "V_F10", "V_F11", "V_F12", "V_F13", "V_F14", "V_F15" },
                new object[] { F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F0 });
            if (iRet > 0)
            {
                bool fileFlag = CreateSHIPPINGFile(dt, dboWriteT,FilePath, FilePathName);
            }
        }
        catch (Exception ex)
        {
            iRet = 0;
            return iRet;
        }
        
        return iRet;
    }
   
    //產生dat文檔
    public bool CreateSHIPPINGFile(DataTable dt, DataBaseOperation dboWriteT,string FilePath, string FilePathName)
    {
        bool bRet = false;
        string strHead = string.Empty;
        string strSql = string.Empty;
        string strDN = dt.Rows[0][14].ToString().Trim();
        if (!Directory.Exists(FilePath))
        {
            Directory.CreateDirectory(FilePath);
        }

        //if (!File.Exists(FilePathName))
        //{
        //    File.Create(FilePathName);
        //}
        
            try
            {
                System.IO.StreamWriter sw = null;
                if (File.Exists(FilePathName))
                    sw = System.IO.File.AppendText(FilePathName);
                else
                    sw = File.CreateText(FilePathName);
                sw.Write("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}\r\n", dt.Rows[0][0].ToString().Trim(), dt.Rows[0][1].ToString().Trim(), dt.Rows[0][2].ToString().Trim(), dt.Rows[0][3].ToString().Trim(), dt.Rows[0][4].ToString().Trim(), dt.Rows[0][5].ToString().Trim(), dt.Rows[0][6].ToString().Trim(), dt.Rows[0][7].ToString().Trim(), dt.Rows[0][8].ToString().Trim(), dt.Rows[0][9].ToString().Trim(), dt.Rows[0][10].ToString().Trim(), dt.Rows[0][11].ToString().Trim(), dt.Rows[0][12].ToString().Trim(), dt.Rows[0][13].ToString().Trim());
                //strHead = dt.Rows[0][0].ToString() + "|" + dt.Rows[0][1].ToString() + "|" + dt.Rows[0][2].ToString() + "|" + dt.Rows[0][3].ToString() + "|" + dt.Rows[0][4].ToString() + "|" + dt.Rows[0][5].ToString() + "|" + dt.Rows[0][6].ToString();
                //strHead = strHead + "|" + dt.Rows[0][7].ToString() + "|" + dt.Rows[0][8].ToString() + "|" + dt.Rows[0][9].ToString() + "|" + dt.Rows[0][10].ToString() + "|" + dt.Rows[0][11].ToString() + "|" + dt.Rows[0][12].ToString() + "|" + dt.Rows[0][13].ToString() + "\r";
                //sw.WriteLine(strHead);
                //sw.Write(strHead);
                sw.Close();
                
                string fileName = FilePathName.Substring(FilePathName.LastIndexOf("\\") + 1);
                strSql = @"UPDATE SAP.SHIPPING_DN_LIST SET DATNAME='" + fileName + "' WHERE DN ='" + strDN + "'";
                int mm = dboWriteT.ExecSQL(strSql);
                if (mm == 0)
                {
                    WriterLog(FilePath, "ErrorLog.txt", strDN, "InsertDBError", "", FilePathName);
                    bRet = false;
                }
                
                string NewFilePath = FilePath + @"\backup";
                if (!Directory.Exists(NewFilePath))
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(NewFilePath);
                    dirinfo.Create();
                }
                string path2 = NewFilePath +@"\"+ fileName;
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                }
                File.Copy(FilePathName, path2);
                bRet = true;
            }
            catch (Exception ex)
            {
                //Log.Write("[ERROR] CreateUPDFile " + ex.Message);
                WriterLog(FilePath, "ErrorLog.txt", strDN, "InsertDATError",ex.Message, FilePathName);
            }
       
        return bRet;
    }

    #region 寫錯誤日誌當
    public void WriterLog(string path, string logName, string DN,string message, string log, string FilePathName)
    {
        System.DateTime tm = DateTime.Now;
        string p = path + logName;
        try
        {
            if (!Directory.Exists(path))//判断路径下目录是否存在
            {
                Directory.CreateDirectory(path);
                if (!File.Exists(p))
                {
                    using (StreamWriter sw = File.CreateText(p))
                    {
                        sw.WriteLine(message + "(" + DN + "):" + log + "----------" + tm + "[" + FilePathName + "]");
                    }

                }
                else
                {
                    using (StreamWriter sw = File.AppendText(p))
                    {
                        sw.WriteLine(message + "(" + DN + ":" + log + "----------" + tm + "[" + FilePathName + "]");
                    }
                }
            }
            else
            {
                if (!File.Exists(p))
                {
                    using (StreamWriter sw = File.CreateText(p))
                    {
                        sw.WriteLine(message + "(" + DN + ":" + log + "----------" + tm + "[" + FilePathName + "]");
                    }

                }
                else
                {
                    using (StreamWriter sw = File.AppendText(p))
                    {
                        sw.WriteLine(message + "(" + DN + ":" + log + "----------" + tm + "[" + FilePathName + "]");
                    }
                }

            }
        }
        catch (Exception e)
        {

        }

    }
    #endregion

    #region 手動生成
    public string Create_DnListM(string dn, DataBaseOperation dboRead, DataBaseOperation dboWriteT, DataBaseOperation dboWrite)
    {
        string iRet="1";
        if (!CheckDN(dboWriteT, dn))
        {
            return iRet = "此數據已存在，請等待同步！";
        }
         int DNStatus = 2;
                SAPOperate sapo = new SAPOperate();
                string Shap_Date = DateTime.Now.ToString("dd-MMM-yy", CultureInfo.CreateSpecificCulture("en-US"));
                
                string checkDN = dn.Trim();
                if (CheckDN(dboWriteT, checkDN))//判斷DN是否存在write數據庫
                {
                    DNStatus = sapo.CheckDNMovementStatus(checkDN);//獲取銷單信息
                    if (DNStatus == 0)
                    {
                        string Strsql = @"SELECT DISTINCT 'AS' region_id,'FIHMLXTJ' factory_id, 'ZFOXTJ-ODM' system_id,
                    b.so_number sales_order, a.customer_po customer_po_number,
                    b.ship_tocustomer_name ship_to_customer_name,
                    a.ship_to_country ship_to_customer_country,
                    c.market_name market_model,
                    DECODE (g.cust_pno,
                            NULL, 'unknown',
                            g.cust_pno
                           ) transceiver_model,
                    DECODE (g.sa_no, NULL, g.cust_pno, g.sa_no) customer_model,
                    DECODE (d.prgcode,
                            'UPD05', 'GSM',
                            'UPD06', 'CDMA',
                            ''
                           ) handset_technology_type,
                    SUBSTR (e.customer_num, 1, 3) item_apc_code,
                    f.shipped_qty unit_quantity,'" + Shap_Date + "' ship_date,";
                        Strsql = Strsql + @" A.INVOICE_NUMBER
                    FROM sap.cmcs_sfc_packing_lines_all a,
                    shp.upd_order_information b,
                    shp.ros_tch_pn c,
                    sfc.cmcs_sfc_model d,
                    shp.cmcs_sfc_imeinum e,
                    sap.sap_invoice_info f,
                    shp.cmcs_sfc_shipping_data g
                    WHERE a.invoice_number='" + checkDN + "'";
                        Strsql = Strsql + @" and a.invoice_number = f.invoice
                    AND a.customer_po = b.po_number
                    AND a.internal_carton = g.carton_no
                    AND g.imei = e.imeinum
                    AND g.MODEL = d.MODEL
                    and c.PPART=E.PPART";
                        DataTable dts = dboRead.SelectSQLDT(Strsql);
                        if (dts.Rows.Count > 0)
                        {
                            string FilePath = @"D:\ReadWeb\Motor\DNLIST";
                            string Filename = "AS_FIHMLXTJ_UPD_AUDIT_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".dat";
                            string FilePathName = FilePath + @"\" + Filename;
                            int mm = CreateOne_DnList(dts, dboWrite, dboWriteT, dboRead, FilePath, FilePathName);
                            if (mm == 0)
                            {
                                iRet = "數據產生失敗，請重試！";
                            }
                        }
                    }
                    else
                    {
                        return iRet = "此DN未銷單，請先銷單后產生！";
                    }
                }
        return iRet;
    }
    #endregion
}
