using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using SCM.GSCMDKen;
using System.Configuration;
 
public class CreateDN_3B2
{

    public int CreateDN3B2(string io, string type, string DBReadString, string DBWriString,string DBReadDetail, string Autoprg)
          
    {
         int iRet =1;
         int i1 = 2;
        //string dbtype="oracle";
         if (io != "1")
         {
             iRet = 0;
             return iRet;
         }
         string dbtype = type;
        DataBaseOperation dboRead = new DataBaseOperation(dbtype, DBReadString);
        DataBaseOperation dboWrite = new DataBaseOperation(dbtype, DBWriString); 
        DataBaseOperation dboReadDtl = new DataBaseOperation(dbtype, DBReadDetail); 
         try
         {
             string sql = "select INVOICE,PO, UPDFILENAME, SAPFLAG  from PUBLIB.UPD_PODN_LIST_T where  nvl(updflag,'N') <> 'Y' and sapflag='Y' ";
            // DataTable DT1 = DataBaseOperation.SelectSQLDT("oracle", DBReadString, sql);
             DataTable DT1 = dboWrite.SelectSQLDT(sql);
             int m = DT1.Rows.Count;
             if (m > 0)
             {
                 //-----正式时使用---------
                 i1= DT1.Rows.Count;
                 // if (i1 >= 2) i1 = 2;
                 for (int i = 0; i < i1; i++)
                 //-----测试时使用---------
                 //for (int i = 0; i < 1; i++)
                 {
                     string strsql = "select * from PUBLIB.DN_3B2HEADER where DN='" + DT1.Rows[i]["INVOICE"].ToString() + "'";
                     DataTable dtst = dboWrite.SelectSQLDT(strsql);
                     if (dtst.Rows.Count == 0)
                         CreateONE_DN(DT1.Rows[i]["INVOICE"].ToString(), dboRead, dboWrite, dboReadDtl);
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


    
    #region ConstitutionFunction

    public string CreateONE_DN(string DN,DataBaseOperation dboRead,DataBaseOperation dboWrite,DataBaseOperation dboReadDtl)
	{  
        
         FPubLib FPubLibPointer = new FPubLib();
         string NetSqlDB4  = ConfigurationManager.AppSettings["Sql221String"]; 
         string   V_DocID  ;
         string ERR    ;
         string rec_Policy ;
         Int16 i  ;
         Int16 row_count  ;
         string str1 = "";
         string str2 = "";
         string str3 = "";
         string str4 = "";
         string str5 = "";
         DataTable dthead = new DataTable();
        str1= " select replace( substr (to_char(systimestamp,'yyyymmddHH24MISSXff'),0,17)  ,'.','') V_DocID from dual";
        DataTable dt1 = new DataTable();
        //dt1 = DataBaseOperation.SelectSQLDT("oracle", System.Configuration.ConfigurationManager.AppSettings["L10IntelConnectingString"], str1);
        dt1 = dboWrite.SelectSQLDT(str1);
        V_DocID = dt1.Rows[0]["V_DocID"].ToString();
        //str2="select *    from  DN_3B2HEADER  where dn='"+DN +"'";
        //if (DataBaseOperation.SelectSQLDT("oracle", System.Configuration.ConfigurationManager.AppSettings["L10IntelConnectingString"], str2).Rows.Count >= 1)
        //{ 
        //    return "";
        //}
        try 
        {
            //-------------------查詢Header數據------------------------------------------//
            str2 = "select  '" + V_DocID + "' DOCUMENTID,c.invoice DN,To_Char(c.creation_date,'YYYY/MM/DD HH24:Mi:SS') DATESTAMP,c.invoice INVOICENO,c.invoice PACKING_SLIP, a.cust_po_number PO_NUMBER,c.so_no SALESORDERNO,b.plant SHIPFROMPLANT ";
            str2 = str2 + "from ";
            str2 = str2 + "sap.SAP_ORDER_INFO a,";
            str2 = str2 + "sap.SAP_ORDER_LINE_INFO b,";
            str2 = str2 + "sap.SAP_INVOICE_INFO c ";
            str2 = str2 + "where a.order_number=b.order_number ";
            str2 = str2 + "and  a.order_number =c.so_no ";
            str2 = str2 + "and  c.invoice ='" + DN + "' and rownum = 1 ";
            str2 = str2 + "order by DATESTAMP desc";
            dthead = dboRead.SelectSQLDT(str2);
            //-------------------查詢detail數據------------------------------------------//
            str4 = "select a.invoice_number DN,'" + V_DocID + "' DOCUMENTID,'' UIDNUM,d.BATH_NUMBER BATCH_NUMBER,e.CASEID  CARTONID,b.imei IMEI_NO,"
                           + "to_char(a.creation_date,'YYYY/MM/DD HH24:Mi:SS') MANUFACTURE_DATE,pallet_number_new PALLETID,C.SERIAL_NUM SERIAL_ID,to_char(a.creation_date,'YYYY/MM/DD HH24:Mi:SS') DATESTAMP ";
            str4 = str4 + ",f.sa_no productid,a.so_line_no linenumber ";
            str4 = str4 + "from  ";
            str4 = str4 + "SAP.CMCS_SFC_PACKING_LINES_ALL a, ";
            str4 = str4 + "shp.cmcs_sfc_shipping_data b,  ";
            str4 = str4 + "shp.cmcs_sfc_imeinum c, ";
            str4 = str4 + "SFC.WORK_ORDER_AND_BATHNUMBER d, ";
            str4 = str4 + "SHP.CMCS_SFC_CARTON e, ";
            str4 = str4 + "SHP.ros_tch_pn f ";
            str4 = str4 + "where a.internal_carton = b.carton_no ";
            str4 = str4 + "and a.invoice_number ='" + DN + "'   ";
            str4 = str4 + "and(b.imei =c.imeinum or c.serial_num =b.IMEI )  ";
            str4 = str4 + "and  a.internal_carton=e.CARTON_NO ";
            str4 = str4 + "and  c.PPART=f.PPART ";
            str4 = str4 + "and b.work_order= d.WO_NUMBER order by imei ";
            DataTable dtDetail = new DataTable();
            dtDetail = dboReadDtl.SelectSQLDT(str4);
            int iii=0;
            //----------------插入Header表------
            if (dthead.Rows.Count > 0 && dtDetail.Rows.Count > 0)
            {
                //-------------------判斷detail數量與SAP.SAP_INVOICE_INFO數量是否相等-add by ysq 20120608-----------------------------------------//
                string sqlqty = "select SHIPPED_QTY from SAP.SAP_INVOICE_INFO where INVOICE ='" + DN + "'";
                DataTable dtQty = dboRead.SelectSQLDT(sqlqty);
                int sapqty = Convert.ToInt32(dtQty.Rows[0][0].ToString());
                if (dtDetail.Rows.Count != sapqty)
                {
                    ERR = FPubLibPointer.PubWri_MessLog("Intel3B2_Check", "Qty Not Equal", "DN = " + dthead.Rows[0]["DN"].ToString(), "SAPQty=" + sapqty, "DetailQty=" + dtDetail.Rows.Count, "", "", "", "", NetSqlDB4, "sql");
                    return "false";
                }
                string F1 = dthead.Rows[0]["DOCUMENTID"].ToString();
                string F2 = dthead.Rows[0]["DN"].ToString();
                string F3 = dthead.Rows[0]["DATESTAMP"].ToString();
                string F4 = dthead.Rows[0]["INVOICENO"].ToString();
                string F5 = dthead.Rows[0]["PACKING_SLIP"].ToString();
                string F6 = dthead.Rows[0]["PO_NUMBER"].ToString();
                string F7 = dthead.Rows[0]["SALESORDERNO"].ToString();
                string F8 = dthead.Rows[0]["SHIPFROMPLANT"].ToString();
                string F9 = dthead.Rows[0]["DN"].ToString();
                str3 = "insert into  PUBLIB.DN_3B2HEADER ";
                str3 = str3 + "( DOCUMENTID,DN,DATESTAMP,INVOICENO,PACKING_SLIP,PO_NUMBER, SALESORDERNO, SHIPFROMPLANT,OrgDN )"
                            + " values(:V_F1,:V_F2,To_Date(:V_F3,'YYYY/MM/DD HH24:Mi:SS'),:V_F4,:V_F5,:V_F6,:V_F7,:V_F8,:V_F9)";   
                iii = dboWrite.ExecSQL(str3, new string[] { "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6", "V_F7", "V_F8","V_F9" },
                    new object[] { F1, F2, F3, F4, F5, F6, F7, F8,F9 });
            }
            //-------------------插入detail表------------------------------------------//

            if (iii == 1)
            {
               
                int sumSuccess = 0;
                int sumFail = 0;
                if (dtDetail.Rows.Count > 0)
                {
                    for (int j = 0; j < dtDetail.Rows.Count; j++)
                    {
                        string H1 = dtDetail.Rows[j]["DN"].ToString();
                        string H2 = dtDetail.Rows[j]["DOCUMENTID"].ToString();
                        string H3 = dtDetail.Rows[j]["UIDNUM"].ToString();
                        string H4 = dtDetail.Rows[j]["BATCH_NUMBER"].ToString();
                        string H5 = dtDetail.Rows[j]["CARTONID"].ToString();
                        string H6 = dtDetail.Rows[j]["IMEI_NO"].ToString();
                        string H7 = dtDetail.Rows[j]["MANUFACTURE_DATE"].ToString();
                        string H8 = dtDetail.Rows[j]["PALLETID"].ToString();
                        string H9 = dtDetail.Rows[j]["SERIAL_ID"].ToString();
                        string H10 = dtDetail.Rows[j]["DATESTAMP"].ToString();
                        string H11 = dtDetail.Rows[j]["productid"].ToString();
                        string H12 = dtDetail.Rows[j]["linenumber"].ToString();
                        str5 = " insert into PUBLIB.dn_3b2detail   ";
                        str5 = str5 + " (  DN,DOCUMENTID,UIDNUM,BATCH_NUMBER,CARTONID,IMEI_NO,MANUFACTURE_DATE,PALLETID,SERIAL_ID, DATESTAMP ,productid,linenumber ) "
                            + "values(:V_H1,:V_H2,:V_H3,:V_H4,:V_H5,:V_H6,To_Date(:V_H7,'YYYY/MM/DD HH24:Mi:SS'),:V_H8,:V_H9,To_Date(:V_H10,'YYYY/MM/DD HH24:Mi:SS'),:V_H11,:V_H12)";
                        int jjj;
                        jjj = dboWrite.ExecSQL(str5, new string[] { "V_H1", "V_H2", "V_H3", "V_H4", "V_H5", "V_H6", "V_H7", "V_H8", "V_H9", "V_H10", "V_H11", "V_H12" },
                                new object[] { H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, H11, H12 });
                        //------當明細為插入該如何處理？----------------
                        if (jjj == 0)
                        {
                            sumFail = sumFail + 1;
                            ERR = FPubLibPointer.PubWri_MessLog("Intel3B2_Detail", "InsertError", "DOCUMENTID = " + dtDetail.Rows[j]["DOCUMENTID"].ToString(), "DN = " + dtDetail.Rows[j]["DN"].ToString(), "IMEI = " + dtDetail.Rows[j]["IMEI_NO"].ToString(), "", "", "", "", NetSqlDB4, "sql");
                        }
                        else
                        {
                            sumSuccess = sumSuccess + 1;
                        }
                    }
                    //add by ysq 20120416 更新數量欄位
                    
                    string sqlUpdateCount = @"update  publib.dn_3b2detail a
                                            set 
                                            Pallet_count=(select count( distinct palletID ) from  publib.dn_3b2detail where Dn =:V_M1),
                                            Count_carton_pallet=(select count( distinct cartonid ) from  publib.dn_3b2detail b  where b.Dn =:V_M1 and b.palletID=a.palletID), 
                                            count_Box_carton=(select count( distinct imei_no ) from  publib.dn_3b2detail b  where b.Dn =:V_M1 and b.cartonid=a.cartonid),
                                            count_carton=(select count( distinct imei_no ) from  publib.dn_3b2detail b  where b.Dn =:V_M1 and b.cartonid=a.cartonid),
                                            batch_qty_pallet=(select count( distinct palletID ) from  publib.dn_3b2detail where Dn =:V_M1),
                                            batch_qty_carton=(select count( distinct cartonid ) from  publib.dn_3b2detail b  where b.Dn =:V_M1 and b.palletID=a.palletID),
                                            Pallet_QTY=(select count( distinct imei_no ) from  publib.dn_3b2detail b  where b.Dn =:V_M1 and b.palletID=a.palletID),
                                            Carton_QTY=(select count( distinct imei_no ) from  publib.dn_3b2detail b  where b.Dn =:V_M1 and b.cartonid=a.cartonid)
                                            where DN =:V_M1";
                    int zzz = dboWrite.ExecSQL(sqlUpdateCount, new string[] { "V_M1" }, new object[] { DN });
                    if (zzz == 0)
                    {
                        sumFail = sumFail + 1;
                        ERR = FPubLibPointer.PubWri_MessLog("Intel3B2_Detail", "InsertError", "DOCUMENTID = " + V_DocID, "DN = " + DN, "CountUpdateFail", "", "", "", "", NetSqlDB4, "sql");
                    }
                }
                
                // Modify by ZH 20120405
                //string sqlupdate = "update PUBLIB.UPD_PODN_LIST_T set updflag='Y' where INVOICE='" + DN + "'";
                // int kkk = dboWrite.ExecSQL(sqlupdate);
                //---------------當為更新成功該如何處理？-------------
                //if (kkk != 1)
                //{
                //    ERR = FPubLibPointer.PubWri_MessLog("Update3B2_UPD_PODN_LIST_T", "UpdateError", "DN = "+dthead.Rows[0]["DN"].ToString(), "", "", "", "", "", "", NetSqlDB4, "sql");
                //}
                //-----------更新3B2Header表数量------------------
                string sqlupdate = "update PUBLIB.DN_3B2HEADER set SUCCESS_COUNT = " + sumSuccess + ",FAIL_COUNT = " + sumFail + ",Item_count = " + dtDetail.Rows.Count + " where DOCUMENTID = '" + V_DocID + "'";
                int mmm = dboWrite.ExecSQL(sqlupdate);
                if (mmm == 0)
                {
                    ERR = FPubLibPointer.PubWri_MessLog("Update3B2_Hear", "Upload_Item_count_Error", "DOCUMENTID = " + dthead.Rows[0]["DOCUMENTID"].ToString(), "DN = " + dthead.Rows[0]["DN"].ToString(), "", "", "", "", "", NetSqlDB4, "sql"); 
                }
            }
            else
            {

                ERR = FPubLibPointer.PubWri_MessLog("Intel3B2_Hear", "insertError", "DN = " + dthead.Rows[0]["DN"].ToString(), "", "", "", "", "", "", NetSqlDB4, "sql");
            }
      
        }
        catch (Exception ex)
        { 
          string tmp = ex.ToString();

        }
         
        return "";
  }

    #endregion
 

    


}
