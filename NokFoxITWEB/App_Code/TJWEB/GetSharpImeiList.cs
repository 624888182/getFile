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
using System.Configuration;
using System.Globalization;
using System.IO;

/// <summary>
/// Summary description for GetSharpImeiList
/// </summary>
public class GetSharpImeiList
{

    private string partMath = "";
    
    public int GetSharpIMEIData(string io, string dbtype, string DBReadString, string DBWriString, string DBWriteTString, string Autoprg) //throw System.Web.HttpException 
    {
        int iRet = 0;
        int li = 0;
        if (io != "1")
        {
            iRet = 0;
            return iRet;
        }
        DataSet ds = new DataSet();
        //DataSet dsdetail = new DataSet();
        DataTable dsdetail = new DataTable();
        string shipdate = DateTime.Now.ToString("dd-MMM-yy",CultureInfo.CreateSpecificCulture("en-US"));
        shipdate = DateTime.Now.ToString("yyyyMMdd");
        string readCon = DBReadString;
        string writeCon = DBWriString;//接收76数据库连接字符串
        string writetCon = DBWriteTString;//接收221数据库连接字符串
        string conType = dbtype;
        partMath = @"D:\ReadWeb\Sharp";
        if (!Directory.Exists(partMath))
        {
            Directory.CreateDirectory(partMath);
        }

        string sql = @"SELECT INVOICE_NUMBER,SUM(QUANTITY),
                     SUBSTR (ITEM_NUMBER, 3, 3) model
                     FROM SAP.CMCS_SFC_PACKING_LINES_ALL WHERE
                     LAST_UPDATE_DATE>sysdate-7 AND
                     SUBSTR (ITEM_NUMBER, 3, 3) IN
                     (SELECT DISTINCT SUBSTR (model, 1, 3) FROM PUBLIB.CUSTOMERMODEL  WHERE CUSTOMER = 'Sharp')
                     group by INVOICE_NUMBER, ITEM_NUMBER";
//        string sql = @"select INVOICE_NUMBER,SUM(QUANTITY), substr(ITEM_NUMBER,3,3) model from SAP.CMCS_SFC_PACKING_LINES_ALL 
//                            where LAST_UPDATE_DATE>sysdate-7
//                            and substr(ITEM_NUMBER,3,3) not in(select distinct substr(model,1,3)  from PUBLIB.CUSTOMERMODEL)
//                            and INVOICE_NUMBER not in (select distinct DN from SAP.SHIPPING_DN_LIST)
//                             group by INVOICE_NUMBER, ITEM_NUMBER";
        ds = DataBaseOperation.SelectSQLDS(dbtype, readCon, sql);
        SAPOperate sapo = new SAPOperate();
        int DNStatus = 2;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string dn = ds.Tables[0].Rows[i]["INVOICE_NUMBER"].ToString().Trim();
            string qty = ds.Tables[0].Rows[i]["SUM(QUANTITY)"].ToString().Trim();
            int Qty;
            try {
                Qty = Convert.ToInt32(qty);
            }catch(Exception e)
            {
               Qty=0;
            }
           
            string modelSharp = ds.Tables[0].Rows[i]["model"].ToString();
            if (CheckDN(conType,writeCon, dn))
            {
                DNStatus = sapo.CheckDNMovementStatus(dn);
                if (DNStatus == 0)
                {
                    string sql1 = @"SELECT  A.IMEINUM,A.IMEINUM2,A.MEID2,A.BTADDRESS,A.WIFI_ADDRESS,A.TA_NUMBER,A.SERIAL_NUM,D.COLOR,E.CASEID CARTON_NO,C.PALLET_NUMBER_NEW,B.PRODUCTID,C.SHIP_TO_COUNTRY SHIP_TO_CUSTOMERNAME FROM 
                        SHP.CMCS_SFC_IMEINUM A, SHP.CMCS_SFC_SHIPPING_DATA B,SAP.CMCS_SFC_PACKING_LINES_ALL C,SHP.ROS_TCH_PN D,SHP.CMCS_SFC_CARTON E WHERE
                        C.INVOICE_NUMBER='" + ds.Tables[0].Rows[i]["INVOICE_NUMBER"].ToString().Trim() + "' AND D.PPART = A.PPART AND A.IMEINUM=B.IMEI AND B.CARTON_NO=C.INTERNAL_CARTON and e.CARTON_NO=C.INTERNAL_CARTON";
                    dsdetail = DataBaseOperation.SelectSQLDT(dbtype, readCon, sql1);
                    //shipdate = DateTime.Now.ToString("dd-MMM-yy", CultureInfo.CreateSpecificCulture("en-US"));
                    string dateNow = DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (dsdetail.Rows.Count == Qty)//判斷數量相等才能產生 --原(dsdetail.Rows.Count > 0)
                    {

                        string fileName = modelSharp + "_" + dn + "_" + qty + "_" + dateNow;
                        
                        DateTime Process_BeforeTime = DateTime.Now;
                        dsdetail.Columns.Add("SHIPPING_DATE");
                        Excel.Application app = new Excel.Application();
                        try
                        {
                            Excel.Workbook wb = app.Workbooks.Add(true);
                            Excel.Worksheet st = (Excel.Worksheet)app.Sheets[1];
                            
                            app.DisplayAlerts = false;
                            app.AlertBeforeOverwriting = false;

                            st.Cells[1, 1] = "IMEI";
                            st.Cells[1, 2] = "IMEI2";
                            st.Cells[1, 3] = "MEID";
                            st.Cells[1, 4] = "BI_ID";
                            st.Cells[1, 5] = "WIFI";
                            st.Cells[1, 6] = "TA";
                            st.Cells[1, 7] = "SERIAL_NO";
                            st.Cells[1, 8] = "COLOR";
                            st.Cells[1, 9] = "MASTERPACK_NUMBER";
                            st.Cells[1, 10] = "PALLET_NUMBER";
                            st.Cells[1, 11] = "PSN";
                            st.Cells[1, 12] = "SHIPPING_DESTINATION";
                            st.Cells[1, 13] = "SHIPPING_DATE";
                            for (int j = 0; j < dsdetail.Rows.Count; j++)
                            {
                                //st.Cells.Style = "vnd.ms-excel.numberformat:@";
                                st.Cells[j + 2, 1] = "'" + dsdetail.Rows[j][0].ToString();
                                st.Cells[j + 2, 2] = "'" + dsdetail.Rows[j][1].ToString();
                                st.Cells[j + 2, 3] = "'" + dsdetail.Rows[j][2].ToString();
                                st.Cells[j + 2, 4] = dsdetail.Rows[j][3].ToString();
                                st.Cells[j + 2, 5] = dsdetail.Rows[j][4].ToString();
                                st.Cells[j + 2, 6] = dsdetail.Rows[j][5].ToString();
                                st.Cells[j + 2, 7] = dsdetail.Rows[j][6].ToString();
                                st.Cells[j + 2, 8] = dsdetail.Rows[j][7].ToString();
                                st.Cells[j + 2, 9] = dsdetail.Rows[j][8].ToString();
                                st.Cells[j + 2, 10] = dsdetail.Rows[j][9].ToString();
                                st.Cells[j + 2, 11] = dsdetail.Rows[j][10].ToString();
                                st.Cells[j + 2, 12] = dsdetail.Rows[j][11].ToString();
                                st.Cells[j + 2, 13] = shipdate;
                            }
                            string tFileName = partMath + "\\" + fileName + ".xls";
                            wb.SaveAs(tFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //ExcelS.XlFileFormat.xlExcel8
                            app.ActiveWorkbook.Close(true, tFileName, Type.Missing);
                            app.Workbooks.Close();
                            app.Quit();
                            DateTime Process_AfterTime = DateTime.Now;
                            KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                            GC.Collect();


                            string NewFilePath = partMath + @"\backup";
                            if (!Directory.Exists(NewFilePath))
                            {
                                DirectoryInfo dirinfo = new DirectoryInfo(NewFilePath);
                                dirinfo.Create();
                            }
                            string path2 = NewFilePath + @"\" + fileName + ".xls";
                            if (File.Exists(path2))
                            {
                                File.Delete(path2);
                            }
                            File.Copy(tFileName, path2);

                        }

                        catch (Exception e)
                        {
                            
                            //app.Quit();
                            //DateTime Process_AfterTime = DateTime.Now;
                            //KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                            //GC.Collect();
                        }
                        iRet = insertDate(fileName, dsdetail, dn, conType, writeCon, shipdate, qty,writetCon);
                    }
                }
            }
        }
        return iRet;
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

    private int insertDate(string name, DataTable ds, string dn, string type,string dbcon,string shipDate,string qty,string dbconT)
    {
        int Ret = 0;
        int insert2 = 0;
        string dateNow = DateTime.Now.ToString("yyyyMMddhhmmss");
        DataBaseOperation dbo = new DataBaseOperation(type, dbcon);
        DataBaseOperation dboBackup = new DataBaseOperation(type, dbconT);
        try
        {
            dbo.BeginTran();

            string sql1 = @"INSERT INTO SHARP.DELEMEILISTM (CREATE_TIME,DN,DATNAME,DOCUMENTID,QTY_DN) VALUES ('" + dateNow + "','" + dn + "','" + name + "','" + dateNow + "','" + qty + "')";
            int insert1 = dbo.ExecSQLTran(sql1);
            //int insertany = dboBackup.ExecSQLTran(sql1);
            if (insert1 <= 0)
            {
                dbo.Rollback();
                return insert1;
            }
            //int insert1 = DataBaseOperation.ExecSQL(type, dbcon, sql1);//向76数据库内插入数据
           // int insertany = DataBaseOperation.ExecSQL(type, dbconT, sql1);//向221数据库插入数据
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                string F0 = ds.Rows[i]["IMEINUM"].ToString();
                string F1 = ds.Rows[i]["IMEINUM2"].ToString();
                string F2 = ds.Rows[i]["MEID2"].ToString();
                string F3 = ds.Rows[i]["BTADDRESS"].ToString();
                string F4 = ds.Rows[i]["WIFI_ADDRESS"].ToString();
                string F5 = ds.Rows[i]["TA_NUMBER"].ToString();
                string F6 = ds.Rows[i]["SERIAL_NUM"].ToString();
                string F7 = ds.Rows[i]["COLOR"].ToString();
                string F8 = ds.Rows[i]["CARTON_NO"].ToString();
                string F9 = ds.Rows[i]["PALLET_NUMBER_NEW"].ToString();
                string F10 = ds.Rows[i]["PRODUCTID"].ToString();
                string F11 = ds.Rows[i]["SHIP_TO_CUSTOMERNAME"].ToString();
                string F12 = shipDate;
                string F13 = dn;
                string F14 = dateNow;
                string sql = @"INSERT INTO SHARP.DELEMEILISTD (IMEI,IMEI2,MEID,BT_ID,WIFI_WLAN,TA_NUMBER,SERIAL_NO,MCOLOR,MASTERPACK_NUMBER,PALLET_NUMBER,PSN,SHIPPING_DESTINATION,SHIPPING_DATE,DN,DOCUMENTID)
                            values(:V_F0,:V_F1,:V_F2,:V_F3,:V_F4,:V_F5,:V_F6,:V_F7,:V_F8,:V_F9,:V_F10,:V_F11,:V_F12,:V_F13,:V_F14)";
                //insert2 = DataBaseOperation.ExecSQL(type, dbcon, sql, new string[] { "V_F0", "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6", "V_F7", "V_F8", "V_F9", "V_F10", "V_F11", "V_F12", "V_F13", "V_F14" },
                //    new object[] { F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14 });//向76数据库内插入数据
                insert2 = dbo.ExecSQLTran(sql, new string[] { "V_F0", "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6", "V_F7", "V_F8", "V_F9", "V_F10", "V_F11", "V_F12", "V_F13", "V_F14" },
                    new object[] { F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14 });//向76数据库内插入数据

                //insert2 = dboBackup.ExecSQLTran(sql, new string[] { "V_F0", "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6", "V_F7", "V_F8", "V_F9", "V_F10", "V_F11", "V_F12", "V_F13", "V_F14" },
                //    new object[] { F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14 });//向221数据库内插入数据
                if (insert2 <= 0)
                {
                    dbo.Rollback();
                    return insert2;
                }

            }
            
            if (insert1 == 1 && insert2 > 0)
            {
                Ret = 1;
                dbo.CommitTran();
            }
            if (dbconT != "" && dbconT != dbcon)
            {
                insertDate(name, ds, dn, type, dbconT, shipDate, qty, "");//插入備用數據庫
            }
        }
        catch (Exception ex)
        {
            dbo.Rollback();
            return 0;
        }
       
        return Ret;
    }
    private bool CheckDN(string type,string dbWrite, string DN)
    {
        bool flagDN = false;
        string sql = "select distinct DN from SHARP.DELEMEILISTM where DN='" + DN + "'";
        DataTable dt = DataBaseOperation.SelectSQLDT(type, dbWrite, sql);
        int li = dt.Rows.Count;
        if (li.Equals(0))
        {
            flagDN = true;
        }
        return flagDN;
    }

    public int creatImeiFileByWeb(string DN,string dbRead,string dbWrite,string dbWriteT, string dbtype, ref string error)
    {

        int iRet = 0;
        int iRow = 0;
        string invoiceNum = DN;
        DataSet ds = new DataSet();
        DataTable dsdetail = new DataTable();
        //string shipdate = DateTime.Now.ToString("dd-MMM-yy", CultureInfo.CreateSpecificCulture("en-US"));
        string shipdate = DateTime.Now.ToString("yyyyMMdd");
        string conType = dbtype;
        partMath = @"D:\ReadWeb\Sharp";
        if (!Directory.Exists(partMath))
        {
            Directory.CreateDirectory(partMath);
        }

        string sql = @"SELECT INVOICE_NUMBER,SUM(QUANTITY),
                     SUBSTR (ITEM_NUMBER, 3, 3) model
                     FROM SAP.CMCS_SFC_PACKING_LINES_ALL
                     WHERE INVOICE_NUMBER = '" + invoiceNum + "'group by INVOICE_NUMBER, ITEM_NUMBER";
        ds = DataBaseOperation.SelectSQLDS(dbtype, dbRead, sql);
        iRow = ds.Tables[0].Rows.Count;
        if (iRow != 0)
        {
            SAPOperate sapo = new SAPOperate();
            int DNStatus = 2;
            for (int i = 0; i < iRow; i++)
            {
                string dn = ds.Tables[0].Rows[i]["INVOICE_NUMBER"].ToString().Trim();
                string qty = ds.Tables[0].Rows[i]["SUM(QUANTITY)"].ToString();
                int Qty;
                try
                {
                    Qty = Convert.ToInt32(qty);
                }
                catch (Exception e)
                {
                    Qty = 0;
                }
                string modelSharp = ds.Tables[0].Rows[i]["model"].ToString();
                if (CheckDN(conType, dbWrite, dn))
                {
                    DNStatus = sapo.CheckDNMovementStatus(dn);
                    if (DNStatus == 0)
                    {
                        string sql1 = @"SELECT  A.IMEINUM,A.IMEINUM2,A.MEID2,A.BTADDRESS,A.WIFI_ADDRESS,A.TA_NUMBER,A.SERIAL_NUM,D.COLOR,E.CASEID CARTON_NO,C.PALLET_NUMBER_NEW,B.PRODUCTID,C.SHIP_TO_COUNTRY SHIP_TO_CUSTOMERNAME FROM 
                        SHP.CMCS_SFC_IMEINUM A, SHP.CMCS_SFC_SHIPPING_DATA B,SAP.CMCS_SFC_PACKING_LINES_ALL C,SHP.ROS_TCH_PN D,SHP.CMCS_SFC_CARTON E WHERE
                        C.INVOICE_NUMBER='" + ds.Tables[0].Rows[i]["INVOICE_NUMBER"].ToString().Trim() + "' AND D.PPART = A.PPART AND A.IMEINUM=B.IMEI AND B.CARTON_NO=C.INTERNAL_CARTON and e.CARTON_NO=C.INTERNAL_CARTON";
                        dsdetail = DataBaseOperation.SelectSQLDT(dbtype, dbRead, sql1);

                        string dateNow = DateTime.Now.ToString("yyyyMMddhhmmss");
                        if (dsdetail.Rows.Count == Qty)
                        {

                            string fileName = modelSharp + "_" + dn + "_" + qty + "_" + dateNow;

                            DateTime Process_BeforeTime = DateTime.Now;
                            dsdetail.Columns.Add("SHIPPING_DATE");
                            Excel.Application app = new Excel.Application();
                            try
                            {
                                Excel.Workbook wb = app.Workbooks.Add(true);
                                Excel.Worksheet st = (Excel.Worksheet)app.Sheets[1];

                                app.DisplayAlerts = false;
                                app.AlertBeforeOverwriting = false;

                                st.Cells[1, 1] = "IMEI";
                                st.Cells[1, 2] = "IMEI2";
                                st.Cells[1, 3] = "MEID";
                                st.Cells[1, 4] = "BI_ID";
                                st.Cells[1, 5] = "WIFI";
                                st.Cells[1, 6] = "TA";
                                st.Cells[1, 7] = "SERIAL_NO";
                                st.Cells[1, 8] = "COLOR";
                                st.Cells[1, 9] = "MASTERPACK_NUMBER";
                                st.Cells[1, 10] = "PALLET_NUMBER";
                                st.Cells[1, 11] = "PSN";
                                st.Cells[1, 12] = "SHIPPING_DESTINATION";
                                st.Cells[1, 13] = "SHIPPING_DATE";
                                for (int j = 0; j < dsdetail.Rows.Count; j++)
                                {
                                    //st.Cells.Style = "vnd.ms-excel.numberformat:@";
                                    st.Cells[j + 2, 1] = "'" + dsdetail.Rows[j][0].ToString();
                                    st.Cells[j + 2, 2] = "'" + dsdetail.Rows[j][1].ToString();
                                    st.Cells[j + 2, 3] = "'" + dsdetail.Rows[j][2].ToString();
                                    st.Cells[j + 2, 4] = dsdetail.Rows[j][3].ToString();
                                    st.Cells[j + 2, 5] = dsdetail.Rows[j][4].ToString();
                                    st.Cells[j + 2, 6] = dsdetail.Rows[j][5].ToString();
                                    st.Cells[j + 2, 7] = dsdetail.Rows[j][6].ToString();
                                    st.Cells[j + 2, 8] = dsdetail.Rows[j][7].ToString();
                                    st.Cells[j + 2, 9] = dsdetail.Rows[j][8].ToString();
                                    st.Cells[j + 2, 10] = dsdetail.Rows[j][9].ToString();
                                    st.Cells[j + 2, 11] = dsdetail.Rows[j][10].ToString();
                                    st.Cells[j + 2, 12] = dsdetail.Rows[j][11].ToString();
                                    st.Cells[j + 2, 13] = shipdate;
                                }
                                string tFileName = partMath + "\\" + fileName + ".xls";
                                wb.SaveAs(tFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //ExcelS.XlFileFormat.xlExcel8
                                app.ActiveWorkbook.Close(true, tFileName, Type.Missing);
                                app.Workbooks.Close();
                                app.Quit();
                                DateTime Process_AfterTime = DateTime.Now;
                                KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                                GC.Collect();


                                string NewFilePath = partMath + @"\backup";
                                if (!Directory.Exists(NewFilePath))
                                {
                                    DirectoryInfo dirinfo = new DirectoryInfo(NewFilePath);
                                    dirinfo.Create();
                                }
                                string path2 = NewFilePath + @"\" + fileName + ".xls";
                                if (File.Exists(path2))
                                {
                                    File.Delete(path2);
                                }
                                File.Copy(tFileName, path2);

                            }

                            catch (Exception e)
                            {
                                //app.Quit();
                                //DateTime Process_AfterTime = DateTime.Now;
                                //KillExcelProcess(Process_BeforeTime, Process_AfterTime);
                                //GC.Collect();
                            }
                            iRet = insertDate(fileName, dsdetail, dn, conType, dbWrite, shipdate, qty, dbWriteT);
                        }
                        else
                        {
                            error = "明細（" + dsdetail.Rows.Count + "）與主表（" + Qty + "）數量不相等！";
                            iRet = 0; 
                        }
                    }
                    else
                    {
                        error = "未銷單！";
                        iRet = 0;
                    }
                }
                else
                {
                    error = "數據庫已經存在該DN對應數據！";
                    iRet = 0;
                }
            }
        }
        else
        {
            error = "沒有對應的DN數據！";
            iRet = 0;
        }
        return iRet;
    }

        
            
}
