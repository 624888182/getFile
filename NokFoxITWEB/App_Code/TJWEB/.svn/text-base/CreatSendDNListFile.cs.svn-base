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
using System.IO;

/// <summary>
/// Summary description for CreatSendDNListFile
/// </summary>
public class CreatSendDNListFile
{
	public CreatSendDNListFile()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int GetDNlistData(string io, string dbtype, string DBReadString, string DBWriString, string DBWriteTString, string Autoprg, string yeDate)
    {
        int iRet = 1;
        int rowCount = 0;
        int columCount = 0;
        DataSet ds = new DataSet();
        string readLine = string.Empty;
        string fileName = string.Empty;
        string tFileName = string.Empty;
        string path = @"D:\ReadWeb\Motor\DNLIST";
        string dataNow = System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

        if (io != "1")
        {
                iRet = 0;
                return iRet;
        }

        string sql = @"SELECT FACTORY_ID,SYSTEM_ID,SALES_ORDER,CUSTOMER_PO_NUMBER,SHIP_TO_CUSTOMER_NAME,SHIP_TO_CUSTOMER_COUNTRY,
                        MARKET_MODEL,TRANSCEIVER_MODEL,CUSTOMER_MODEL,HANDSET_TECHNOLOGY_TYPE,ITEM_APC_CODE,UNIT_QUANTITY,SHIP_DATE,DN 
                        FROM SAP.SHIPPING_DN_LIST WHERE SUBSTR(CREATE_TIME,1,8) = '" + yeDate + "' AND SENDNAME IS NULL";
        ds = DataBaseOperation.SelectSQLDS(dbtype, DBReadString, sql);

        rowCount = ds.Tables[0].Rows.Count;
        columCount = ds.Tables[0].Columns.Count - 1;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columCount; j++)
            {
                readLine = readLine + ds.Tables[0].Rows[i][j].ToString().Trim() + "|";
                
            }
            readLine += "\r\n";
        }

        if (readLine != "")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            fileName = "AS_FIHMLXTJ_UPD_AUDIT_" + dataNow + ".dat";
            tFileName = path + "\\" + fileName;


            StreamWriter sw = null;
            if (File.Exists(tFileName))
                sw = File.AppendText(tFileName);
            else
                sw = File.CreateText(tFileName);
            try
            {
                sw.Write(readLine);
            }
            catch (Exception e)
            {

            }
            finally
            {
                sw.Close();
                iRet = 0;
            }


            string backPath = path + "\\" + "backup";
            if (!Directory.Exists(backPath))
                Directory.CreateDirectory(backPath);
            string newFile = backPath + "\\" +fileName;

            
            File.Copy(tFileName, newFile);


            for (int i = 0; i < rowCount; i++)
            {
                string dn = ds.Tables[0].Rows[i]["DN"].ToString().Trim();
                string sql1 = @"UPDATE SAP.SHIPPING_DN_LIST SET SENDNAME = '" + fileName + "' WHERE  DN = '" + dn + "'";
                iRet = DataBaseOperation.ExecSQL(dbtype, DBWriString, sql1);
            }
        }

            return iRet;        
    }

    // 20120928
    // Same SO merge to one ( Qty Add ) 

    public string GetDNlistData20120928(string io, string dbtype, string DBReadString, string DBWriString, string DBWriteTString, string Autoprg, string yeDate)
    {
        string iRet = "0";
        int rowCount = 0;
        int columCount = 0;
        DataSet ds = new DataSet();
        string readLine = string.Empty;
        string fileName = string.Empty;
        string tFileName = string.Empty;
        string[] dn = new string[0];
        string path = @"D:\ReadWeb\Motor\DNLIST";
        string dataNow = System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

        if (io != "1")
        {
            iRet = "0";
            return iRet;
        }

        string sql = @"SELECT FACTORY_ID,SYSTEM_ID,SALES_ORDER,CUSTOMER_PO_NUMBER,SHIP_TO_CUSTOMER_NAME,SHIP_TO_CUSTOMER_COUNTRY,
                        MARKET_MODEL,TRANSCEIVER_MODEL,CUSTOMER_MODEL,HANDSET_TECHNOLOGY_TYPE,ITEM_APC_CODE,UNIT_QUANTITY,SHIP_DATE,DN 
                        FROM SAP.SHIPPING_DN_LIST WHERE SUBSTR(CREATE_TIME,1,8) = '" + yeDate + "' AND SENDNAME IS NULL";//AND SENDNAME IS NULL
        ds = DataBaseOperation.SelectSQLDS(dbtype, DBReadString, sql);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Array.Resize(ref dn, dn.GetLength(0) + 1);
            dn[i] = ds.Tables[0].Rows[i]["DN"].ToString();
        }

        int nowRowC = ds.Tables[0].Rows.Count;

        for (int i = 0; i < (nowRowC - 1); i++)
        {
            for (int j = i + 1; j < nowRowC; )
            {
                //判斷SO 幾種和ITEM_APC_CODE是否一致
                if ((ds.Tables[0].Rows[i]["SALES_ORDER"].ToString() == ds.Tables[0].Rows[j]["SALES_ORDER"].ToString()) && ((ds.Tables[0].Rows[i]["MARKET_MODEL"].ToString() == ds.Tables[0].Rows[j]["MARKET_MODEL"].ToString())) && (ds.Tables[0].Rows[i]["ITEM_APC_CODE"].ToString() == ds.Tables[0].Rows[j]["ITEM_APC_CODE"].ToString()))
                {
                    ds.Tables[0].Rows[i]["UNIT_QUANTITY"] = Convert.ToString(Convert.ToDecimal(ds.Tables[0].Rows[i]["UNIT_QUANTITY"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[j]["UNIT_QUANTITY"].ToString()));
                    ds.Tables[0].Rows[j].Delete();
                    ds.Tables[0].Rows[j].AcceptChanges();
                    nowRowC = nowRowC - 1;
                    continue;
                }
                j++;
            }
        }

        // 判斷region id, system id, factory id, transceiver, apc, shipdate是否一致，一致則數量相加，PO SO置空
        //int iRow = ds.Tables[0].Rows.Count;
        //for (int i = 0; i < (iRow - 1); i++)
        //{
        //    for (int j = i + 1; j < iRow; )
        //    {
        //        if (ds.Tables[0].Rows[i]["FACTORY_ID"].ToString() == ds.Tables[0].Rows[j]["FACTORY_ID"].ToString() && ds.Tables[0].Rows[i]["SYSTEM_ID"].ToString() == ds.Tables[0].Rows[j]["SYSTEM_ID"].ToString() && ds.Tables[0].Rows[i]["TRANSCEIVER_MODEL"].ToString() == ds.Tables[0].Rows[j]["TRANSCEIVER_MODEL"].ToString() && ds.Tables[0].Rows[i]["ITEM_APC_CODE"].ToString() == ds.Tables[0].Rows[j]["ITEM_APC_CODE"].ToString() && ds.Tables[0].Rows[i]["SHIP_DATE"].ToString() == ds.Tables[0].Rows[j]["SHIP_DATE"].ToString())
        //        {
        //            ds.Tables[0].Rows[i]["UNIT_QUANTITY"] = Convert.ToString(Convert.ToDecimal(ds.Tables[0].Rows[i]["UNIT_QUANTITY"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[j]["UNIT_QUANTITY"].ToString()));
        //            if (ds.Tables[0].Rows[i]["SALES_ORDER"].ToString() != ds.Tables[0].Rows[j]["SALES_ORDER"].ToString() && ds.Tables[0].Rows[i]["SALES_ORDER"].ToString() != "")
        //            {
        //                ds.Tables[0].Rows[i]["SALES_ORDER"] = "";
        //            }
        //            if (ds.Tables[0].Rows[i]["CUSTOMER_PO_NUMBER"].ToString() != ds.Tables[0].Rows[j]["CUSTOMER_PO_NUMBER"].ToString() && ds.Tables[0].Rows[i]["CUSTOMER_PO_NUMBER"].ToString() != "")
        //            {
        //                ds.Tables[0].Rows[i]["CUSTOMER_PO_NUMBER"] = "";
        //            }
        //            ds.Tables[0].Rows[j].Delete();
        //            ds.Tables[0].Rows[j].AcceptChanges();
        //            iRow--;
        //            continue;
        //        }
        //        j++;
        //    }
        //}
        rowCount = ds.Tables[0].Rows.Count;
        columCount = ds.Tables[0].Columns.Count - 1;

        for (int i = 0; i < rowCount; i++)
        {
            readLine = readLine + "AS" + "|";
            for (int j = 0; j < columCount; j++)
            {
                readLine = readLine + ds.Tables[0].Rows[i][j].ToString().Trim() + "|";

            }
            readLine += "\r\n";
        }

        if (readLine != "")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            fileName = "AS_FIHMLXTJ_UPD_AUDIT_" + dataNow + ".dat";
            tFileName = path + "\\" + fileName;


            StreamWriter sw = null;
            if (File.Exists(tFileName))
                sw = File.AppendText(tFileName);
            else
                sw = File.CreateText(tFileName);
            try
            {
                sw.Write(readLine);
            }
            catch (Exception e)
            {

            }
            finally
            {
                sw.Close();
                iRet = "0";
            }


            string backPath = path + "\\" + "backup";
            if (!Directory.Exists(backPath))
                Directory.CreateDirectory(backPath);
            string newFile = backPath + "\\" + fileName;


            File.Copy(tFileName, newFile);


            for (int i = 0; i < dn.Length; i++)
            {
                string dnNum = dn[i].ToString();
                string sql1 = @"UPDATE SAP.SHIPPING_DN_LIST SET SENDNAME = '" + fileName + "' WHERE  DN = '" + dnNum + "'";
                iRet = (DataBaseOperation.ExecSQL(dbtype, DBWriString, sql1)).ToString();
            }
        }

        return fileName;

    }

    //ADD BY 2012-11-6
    // 判斷region id, system id, factory id, transceiver, apc, shipdate是否一致，一致則數量相加，PO SO置空
    public string GetDNlistData1(string io, string dbtype, string DBReadString, string DBWriString, string DBWriteTString, string Autoprg, string yeDate)
    {
        string iRet = "0";
        int rowCount = 0;
        int columCount = 0;
        DataSet ds = new DataSet();
        string readLine = string.Empty;
        string fileName = string.Empty;
        string tFileName = string.Empty;
        string[] dn = new string[0];
        string path = @"D:\ReadWeb\Motor\DNLIST";
        string dataNow = System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

        if (io != "1")
        {
            iRet = "0";
            return iRet;
        }

        string sql = @"SELECT FACTORY_ID,SYSTEM_ID,SALES_ORDER,CUSTOMER_PO_NUMBER,SHIP_TO_CUSTOMER_NAME,SHIP_TO_CUSTOMER_COUNTRY,
                        MARKET_MODEL,TRANSCEIVER_MODEL,CUSTOMER_MODEL,HANDSET_TECHNOLOGY_TYPE,ITEM_APC_CODE,UNIT_QUANTITY,SHIP_DATE,DN 
                        FROM SAP.SHIPPING_DN_LIST WHERE SUBSTR(CREATE_TIME,1,8) = '" + yeDate + "' AND SENDNAME IS NULL";//AND SENDNAME IS NULL
        ds = DataBaseOperation.SelectSQLDS(dbtype, DBReadString, sql);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Array.Resize(ref dn, dn.GetLength(0) + 1);
            dn[i] = ds.Tables[0].Rows[i]["DN"].ToString();
        }

        int nowRowC = ds.Tables[0].Rows.Count;

        for (int i = 0; i < (nowRowC - 1); i++)
        {
            for (int j = i + 1; j < nowRowC; )
            {
                //判斷SO 幾種和ITEM_APC_CODE是否一致
                if ((ds.Tables[0].Rows[i]["SALES_ORDER"].ToString() == ds.Tables[0].Rows[j]["SALES_ORDER"].ToString()) && ((ds.Tables[0].Rows[i]["MARKET_MODEL"].ToString() == ds.Tables[0].Rows[j]["MARKET_MODEL"].ToString())) && (ds.Tables[0].Rows[i]["ITEM_APC_CODE"].ToString() == ds.Tables[0].Rows[j]["ITEM_APC_CODE"].ToString()))
                {
                    ds.Tables[0].Rows[i]["UNIT_QUANTITY"] = Convert.ToString(Convert.ToDecimal(ds.Tables[0].Rows[i]["UNIT_QUANTITY"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[j]["UNIT_QUANTITY"].ToString()));
                    ds.Tables[0].Rows[j].Delete();
                    ds.Tables[0].Rows[j].AcceptChanges();
                    nowRowC = nowRowC - 1;
                    continue;
                }
                j++;
            }
        }

        // 判斷region id, system id, factory id, transceiver, apc, shipdate是否一致，一致則數量相加，PO SO置空
        int iRow = ds.Tables[0].Rows.Count;
        for (int i = 0; i < (iRow - 1); i++)
        {
            for (int j = i + 1; j < iRow; )
            {
                if (ds.Tables[0].Rows[i]["FACTORY_ID"].ToString() == ds.Tables[0].Rows[j]["FACTORY_ID"].ToString() && ds.Tables[0].Rows[i]["SYSTEM_ID"].ToString() == ds.Tables[0].Rows[j]["SYSTEM_ID"].ToString() && ds.Tables[0].Rows[i]["TRANSCEIVER_MODEL"].ToString() == ds.Tables[0].Rows[j]["TRANSCEIVER_MODEL"].ToString() && ds.Tables[0].Rows[i]["ITEM_APC_CODE"].ToString() == ds.Tables[0].Rows[j]["ITEM_APC_CODE"].ToString() && ds.Tables[0].Rows[i]["SHIP_DATE"].ToString() == ds.Tables[0].Rows[j]["SHIP_DATE"].ToString())
                {
                    ds.Tables[0].Rows[i]["UNIT_QUANTITY"] = Convert.ToString(Convert.ToDecimal(ds.Tables[0].Rows[i]["UNIT_QUANTITY"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[j]["UNIT_QUANTITY"].ToString()));
                    if (ds.Tables[0].Rows[i]["SALES_ORDER"].ToString() != ds.Tables[0].Rows[j]["SALES_ORDER"].ToString() && ds.Tables[0].Rows[i]["SALES_ORDER"].ToString() != "")
                    {
                        ds.Tables[0].Rows[i]["SALES_ORDER"] = "";
                    }
                    if (ds.Tables[0].Rows[i]["CUSTOMER_PO_NUMBER"].ToString() != ds.Tables[0].Rows[j]["CUSTOMER_PO_NUMBER"].ToString() && ds.Tables[0].Rows[i]["CUSTOMER_PO_NUMBER"].ToString() != "")
                    {
                        ds.Tables[0].Rows[i]["CUSTOMER_PO_NUMBER"] = "";
                    }
                    ds.Tables[0].Rows[j].Delete();
                    ds.Tables[0].Rows[j].AcceptChanges();
                    iRow--;
                    continue;
                }
                j++;
            }
        }
        rowCount = ds.Tables[0].Rows.Count;
        columCount = ds.Tables[0].Columns.Count - 1;

        for (int i = 0; i < rowCount; i++)
        {
            readLine = readLine + "AS" + "|";
            for (int j = 0; j < columCount; j++)
            {
                readLine = readLine + ds.Tables[0].Rows[i][j].ToString().Trim() + "|";

            }
            readLine += "\r\n";
        }

        if (readLine != "")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            fileName = "AS_FIHMLXTJ_UPD_AUDIT_" + dataNow + ".dat";
            tFileName = path + "\\" + fileName;


            StreamWriter sw = null;
            if (File.Exists(tFileName))
                sw = File.AppendText(tFileName);
            else
                sw = File.CreateText(tFileName);
            try
            {
                sw.Write(readLine);
            }
            catch (Exception e)
            {

            }
            finally
            {
                sw.Close();
                iRet = "0";
            }


            string backPath = path + "\\" + "backup";
            if (!Directory.Exists(backPath))
                Directory.CreateDirectory(backPath);
            string newFile = backPath + "\\" + fileName;


            File.Copy(tFileName, newFile);


            for (int i = 0; i < dn.Length; i++)
            {
                string dnNum = dn[i].ToString();
                string sql1 = @"UPDATE SAP.SHIPPING_DN_LIST SET SENDNAME = '" + fileName + "' WHERE  DN = '" + dnNum + "'";
                iRet = (DataBaseOperation.ExecSQL(dbtype, DBWriString, sql1)).ToString();
            }
        }

        return fileName;

    }
}
