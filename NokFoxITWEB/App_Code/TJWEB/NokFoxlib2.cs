using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SFC.TJWEB;
using System.Xml;
using System.Text;
using System.Net;

/// <summary>
/// Summary description for AutoDn
/// </summary>
/// 
namespace SFC.TJWEB
{


    public class NokFoxlib2
    {
        N_CreateSO createso = new N_CreateSO();
        NokFoxlib6 lib6 = new NokFoxlib6();
        


        //PO insert into Sap to create SO if F6 = "2" and F7 = "1" then Call Sap F7 = "2"
        public void AutoRunSo(string dbtype, string dbconn, string evntype, string dbname) 
        {

                string selectpo = "select distinct poid  from " + dbname + ".[dbo].[PO_CREATE_DT] where  F6 = '2' and F7 = '1'  ";
                DataTable HeaderSqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, selectpo);

                if (HeaderSqldt.Rows.Count > 0) 
                {
                    for(int i = 0 ; i <HeaderSqldt.Rows.Count  ; i++ )
                    {
                             string poid = HeaderSqldt.Rows[i]["poid"].ToString();
                             string status = createso.WebCreateSO(dbtype, dbconn, evntype, poid,dbname);

                             if (status == "Y") 
                             {
                                 string updatestatusuf7 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F7 = '2' where poid = '" + poid + "'";
                                 int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf7);
                             }
                            //createso.WebCreateSO(dbtype, dbconn, evntype, poid);
                    }
                }
            }

        //get sfc dn confirm data. (po)
        public void GetSFCdnConfirm(string dbtype, string dbconn, string sfcread, string evntype, string dbname, string tmpdate)
        {
            //check sidetable to find the fusetype is fuse or newfuse?(not yet)
            string checkgetdnsql = "select * from " + dbname + ".[dbo].[PO_CREATE_DT] where  F9 = '2'  and F10 ='1' ";
            DataTable checkgetdnsqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkgetdnsql);
            if (checkgetdnsqldt.Rows.Count > 0)
            {
                for (int i = 0; i < checkgetdnsqldt.Rows.Count; i++)
                {
                    string poid = checkgetdnsqldt.Rows[i]["poid"].ToString();
                    string poitem = checkgetdnsqldt.Rows[i]["itemid"].ToString();

                    //1.use poid and poitem to sfc_edi_dn_item find DN , qty if(table.count >0 go through step2 , 3).
                    string selectdnitemsql = "select * from FLX.EDI_DN_ITEM WHERE  PO = '" + poid + "' AND PO_ITEM = '" + poitem + "'";
                    DataTable selectdnitemdt = PDataBaseOperation.PSelectSQLDT("Oracle", sfcread, selectdnitemsql);
                    if (selectdnitemdt.Rows.Count > 0)
                    {
                        //2.use dn to sfc_edi_dn_info to get status 
                        for (int j = 0; j < selectdnitemdt.Rows.Count; j++)
                        {
                            string dn = selectdnitemdt.Rows[j]["DN_NO"].ToString();
                            int sfcpoqty = Convert.ToInt32(selectdnitemdt.Rows[j]["total_qty"].ToString());

                            //3.if(status = '4'(dn confirm) or status = '6'(sfc create xml))
                            string dnstatussql = "select *  from FLX.EDI_DN_INFO where dn_no= '" + dn + "' and (status = '4' or status = '6') ";
                            DataTable dnstatussqldt = PDataBaseOperation.PSelectSQLDT("Oracle", sfcread, dnstatussql);
                            if (dnstatussqldt.Rows.Count > 0)
                            {
                                // update qty to [PO_CREATE_DT] "F16" AND F10 = '2'
                                string updatestatusuf9 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F10 = '2',F16 = '" + sfcpoqty + "' where poid = '" + poid + "' and item = '" + poitem + "'";
                                int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf9);

                            }
                        }
                    }
                }
            }
        }


        //20170605
        public void GetSFCdnConfirmdn(string dbtype, string dbconn, string sfcread, string evntype, string dbname, string tmpdate, string DN)
        {
            string dnid = "";
            int dnqty = 0;
            int count = 0;

            string checkgetdnsql = "select * from " + dbname + ".[dbo].[Delivery_Notification_MT] where flag1 = '1' ";
            if (DN != "")
            {
                checkgetdnsql += " and dnid = '" + DN + "'";
            }
            DataTable checkgetdnsqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkgetdnsql);

            if (checkgetdnsqldt.Rows.Count > 0)
            {
                for (int i = 0; i < checkgetdnsqldt.Rows.Count; i++)
                {
                    dnid = checkgetdnsqldt.Rows[i]["DNID"].ToString();
                    //string dnstatussql = "select *  from FLX.EDI_DN_INFO where dn_no= '" + dnid + "' and (status = '4' or status = '6') ";
                    string dnstatussql = "select *  from FLX.EDI_DN_INFO where dn_no= '" + dnid + "' and (status >= '4') "; //add 20170609
                    DataTable dnstatussqldt = PDataBaseOperation.PSelectSQLDT("Oracle", sfcread, dnstatussql);



                    if (dnstatussqldt.Rows.Count > 0)
                    {
                        string selectdnitemsql = "select PO,PO_ITEM ,SUM(TOTAL_QTY) TOTAL_QTY from FLX.EDI_DN_ITEM WHERE  dn_no = '" + dnid + "' GROUP BY PO,PO_ITEM";
                        DataTable selectdnitemdt = PDataBaseOperation.PSelectSQLDT("Oracle", sfcread, selectdnitemsql);

                        if (selectdnitemdt.Rows.Count > 0)
                        {
                            for (int j = 0; j < selectdnitemdt.Rows.Count; j++)
                            {
                                string poid = selectdnitemdt.Rows[j]["po"].ToString();
                                string poitem = selectdnitemdt.Rows[j]["po_item"].ToString();

                                poitem = int.Parse(poitem).ToString();
                                int sfcpoqty = Convert.ToInt32(selectdnitemdt.Rows[j]["total_qty"].ToString());

                                // update qty to [PO_CREATE_DT] "F16" AND F10 = '2'
                                string updatestatusuf9 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F10 = '2',F16 = '" + sfcpoqty + "' where poid = '" + poid + "' and itemid = '" + poitem + "'";
                                int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf9);


                                //update [IMSCMWS].[dbo].[Delivery_DNITEM] set uf1= '2' add 20170623
                                string updatestatusuf1 = "update " + dbname + ".[dbo].[Delivery_DNITEM] set UF1='2'where poid = '" + poid + "' and POitemid = '" + poitem + "'";
                                int updateF1count = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf1);
                               

                                //直接檢查.
                                string checkasnqtysql = "select * from " + dbname + ".[dbo].[Delivery_DNITEM] where dnid = '" + dnid + "' and poid = '" + poid + "' and poitemid = '" + poitem + "'";
                                DataTable checkasnqtysqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkasnqtysql);
                                for (int k = 0; k < checkasnqtysqldt.Rows.Count; k++)
                                {

                                    int asnqty = Convert.ToInt32(checkasnqtysqldt.Rows[k]["Total_QTY"].ToString());

                                    string checkqtysql = "select * from " + dbname + ".[dbo].[PO_CREATE_DT] where poid = '" + poid + "' and itemid  = '" + poitem + "'";
                                    DataTable checkqtysqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkqtysql);

                                    if (checkqtysqldt.Rows.Count > 0)
                                    {

                                        string baseqty = checkqtysqldt.Rows[0]["baseqty"].ToString();
                                        double poqty = 0.00;
                                        if (baseqty != "")
                                        {
                                            poqty = Convert.ToDouble(baseqty);
                                        }


                                        string dnqtytemp = checkqtysqldt.Rows[0]["F16"].ToString();
                                        if (dnqtytemp == "")
                                        {
                                            dnqty = 0;
                                        }
                                        else
                                        {
                                            dnqty = Convert.ToInt32(checkqtysqldt.Rows[0]["F16"].ToString());
                                        }

                                        if (poqty == dnqty && dnqty == asnqty)
                                        {
                                            string updatestatusuf11 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F11 = '2'  where poid = '" + poid + "' and itemid = '" + poitem + "'";
                                            int updatecountf11 = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf11);
                                            count++;
                                        }
                                    }
                                }


                            }
                            if (count == selectdnitemdt.Rows.Count)
                            {
                                string updateasnstatus = "update " + dbname + ".[dbo].[Delivery_Notification_MT] set flag1 = '2' where dnid = '" + dnid + "'";
                                int updatecountasn = PDataBaseOperation.PExecSQL(dbtype, dbconn, updateasnstatus);
                                count = 0;
                            }

                        }
                    }
                }
            }
        }


        //get sfc dn confirm data.(dn)
        public void GetSFCdnConfirmdnV1(string dbtype, string dbconn, string sfcread, string evntype, string dbname, string tmpdate,string DN)
        {
            string dnid = "";
            
            string checkgetdnsql = "select * from " + dbname + ".[dbo].[Delivery_Notification_MT] where flag1 = '1' ";
            if (DN != "")
            {
                checkgetdnsql += " and dnid = '" + DN + "'";
            }
            DataTable checkgetdnsqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkgetdnsql);

            if (checkgetdnsqldt.Rows.Count > 0) 
            {
                for(int i = 0 ; i < checkgetdnsqldt.Rows.Count ; i++)
                {
                   dnid = checkgetdnsqldt.Rows[i]["DNID"].ToString();
                   string dnstatussql = "select *  from FLX.EDI_DN_INFO where dn_no= '" + dnid + "' and (status = '4' or status = '6') ";
                   DataTable dnstatussqldt = PDataBaseOperation.PSelectSQLDT("Oracle", sfcread, dnstatussql);

                   if (dnstatussqldt.Rows.Count > 0) 
                   {
                       string selectdnitemsql = "select PO,PO_ITEM ,SUM(TOTAL_QTY) TOTAL_QTY from FLX.EDI_DN_ITEM WHERE  dn_no = '" + dnid + "' GROUP BY PO,PO_ITEM";
                       DataTable selectdnitemdt = PDataBaseOperation.PSelectSQLDT("Oracle", sfcread, selectdnitemsql);

                       if (selectdnitemdt.Rows.Count > 0) 
                       {
                           for (int j = 0; j < selectdnitemdt.Rows.Count; j++) 
                           {
                               string poid = selectdnitemdt.Rows[j]["po"].ToString();
                               string poitem = selectdnitemdt.Rows[j]["po_item"].ToString();
                               
                               poitem = int.Parse(poitem).ToString();
                               int sfcpoqty = Convert.ToInt32(selectdnitemdt.Rows[j]["total_qty"].ToString());

                               // update qty to [PO_CREATE_DT] "F16" AND F10 = '2'
                               string updatestatusuf9 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F10 = '2',F16 = '" + sfcpoqty + "' where poid = '" + poid + "' and itemid = '" + poitem + "'";
                               int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf9);
                           }
                       }
                   }
                }
            }
        }

        //check sfc po data qty  == receive po data qty (po)
        public void checkqtydn(string dbtype, string dbconn, string evntype, string dbname, string tmpdate)
        {
            string checkqtysql = "select * from " + dbname + ".[dbo].[PO_CREATE_DT] where  F10 = '2' and F11 = '1'";
            DataTable checkqtysqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkqtysql);

            if (checkqtysqldt.Rows.Count > 0)
            {
                for (int i = 0; i < checkqtysqldt.Rows.Count; i++)
                {
                    string poid = checkqtysqldt.Rows[i]["poid"].ToString();
                    string poitem = checkqtysqldt.Rows[i]["itemid"].ToString();
                    int poqty = Convert.ToInt32(checkqtysqldt.Rows[i]["baseqty"].ToString());
                    int dnqty = Convert.ToInt32(checkqtysqldt.Rows[i]["F16"].ToString());

                    string checkasnqtysql = "select * from " + dbname + ".[dbo].[Delivery_DNITEM] where POID = '" + poid + "' and poitemid = '" + poitem + "'";
                    DataTable checkasnqtysqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkasnqtysql);
                    int asnqty = Convert.ToInt32(checkasnqtysqldt.Rows[0]["Total_QTY"].ToString());

                    if (poqty == dnqty && dnqty == asnqty)
                    {
                        string updatestatusuf11 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F11 = '2'  where poid = '" + poid + "' and item = '" + poitem + "'";
                        int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf11);
                        //string updateasnstatus = "update " + dbname + ".[dbo].[Delivery_Notification_MT] where flag1 = '2' and dnid = '" + dnid + "'";
                        //int updatecountasn = PDataBaseOperation.PExecSQL(dbtype, dbconn, updateasnstatus);

                    }
                }
            }
        }

        //check sfc po data qty  == receive po data qty (dn)
        public void checkqty(string dbtype, string dbconn, string evntype, string dbname, string tmpdate,string dn)
        {
            string poid, poitem;
            //int asnqty, poqty, dnqty;
            int dnqty;


            string checkgetdnsql = "select * from " + dbname + ".[dbo].[Delivery_Notification_MT] where flag1 = '1' ";
            if (dn != "") 
            {
                checkgetdnsql += " and dnid = '" + dn + "'";
            }
            DataTable checkgetdnsqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkgetdnsql);

            if (checkgetdnsqldt.Rows.Count > 0)
            {
                for (int i = 0; i < checkgetdnsqldt.Rows.Count; i++)
                {
                    string strdn = checkgetdnsqldt.Rows[i]["DNID"].ToString();

                    string checkasnqtysql = "select * from " + dbname + ".[dbo].[Delivery_DNITEM] where dnid = '" + strdn + "'";
                    DataTable checkasnqtysqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkasnqtysql);
                    for (int j = 0; j < checkasnqtysqldt.Rows.Count; j++) 
                    {
                        poid = checkasnqtysqldt.Rows[j]["poid"].ToString();
                        poitem = checkasnqtysqldt.Rows[j]["poitemid"].ToString();
                        int asnqty = Convert.ToInt32(checkasnqtysqldt.Rows[j]["Total_QTY"].ToString());

                        string checkqtysql = "select * from " + dbname + ".[dbo].[PO_CREATE_DT] where poid = '" + poid + "' and itemid  = '" + poitem + "'";
                        DataTable checkqtysqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkqtysql);

                        if (checkqtysqldt.Rows.Count > 0) 
                        {

                            string baseqty = checkqtysqldt.Rows[0]["baseqty"].ToString();
                            double poqty = 0.00;
                            if (baseqty != "") 
                            {
                                poqty = Convert.ToDouble(baseqty);
                            }

                            
                            string dnqtytemp = checkqtysqldt.Rows[0]["F16"].ToString();
                            if (dnqtytemp == "")
                            {
                                dnqty = 0;
                            }
                            else
                            {
                                dnqty = Convert.ToInt32(checkqtysqldt.Rows[0]["F16"].ToString());
                            }

                            if (poqty == dnqty && dnqty == asnqty)
                            {
                                string updatestatusuf11 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F11 = '2'  where poid = '" + poid + "' and itemid = '" + poitem + "'";
                                //int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf11);
                                string updateasnstatus = "update " + dbname + ".[dbo].[Delivery_Notification_MT] set flag1 = '2' where dnid = '" + strdn + "'";
                                //int updatecountasn = PDataBaseOperation.PExecSQL(dbtype, dbconn, updateasnstatus);
                            }
                        }
                    }
                }
            }
        }

        //send 3b2 xml to hmd
        public void send3B2(string Dboracle,string db156,string dbtype, string dbconn, string evntype, string dbname, string tmpdate, string dn, string Runtype) 
        {
            string send3b2sql = "select * from " + dbname + ".[dbo].[Delivery_Notification_MT] where flag1 = '2' ";
            if (dn != "")
            {
                send3b2sql += " and dnid = '" + dn + "'";
            }
            DataTable send3b2sqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, send3b2sql);


            if (send3b2sqldt.Rows.Count > 0) 
            {
                for (int i = 0; i < send3b2sqldt.Rows.Count; i++) 
                {
                    string strdn = send3b2sqldt.Rows[i]["DNID"].ToString();

                    string status = lib6.gene3B2xml(dbtype, dbconn, dbname, strdn);
                    if (status == "Y") 
                    {
                        lib6.UploadXML(Dboracle, db156,dbtype, dbconn, dbname, strdn, Runtype);
                    }
                    
                    //lib6.UploadXML(strdn);
                }
            }
        }
        public void invoiceEU(string dbtype, string dbconn, string evntype, string dbname) 
        {
            string UF13NULL = "SELECT distinct POID FROM " + dbname + ".[dbo].[PO_CREATE_DT] where F13 IS NULL";
            DataTable send3b2sqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, UF13NULL);

            if (send3b2sqldt.Rows.Count > 0) 
            {
                for (int i = 0; i < send3b2sqldt.Rows.Count; i++) 
                {
                    string po = send3b2sqldt.Rows[i]["POID"].ToString();
                    string UP14NULL = "SELECT POID,[CountryCode] FROM [IMSCMWS].[dbo].[POShipToAddress] WHERE POID = '" + po + "' and CountryCode != ''";
                    DataTable UP14NULLdt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, UP14NULL);
                    if (UP14NULLdt.Rows.Count > 0)
                    {
                        for (int j = 0; j < UP14NULLdt.Rows.Count; j++)
                        {
                            string country = UP14NULLdt.Rows[j]["CountryCode"].ToString();

                            string findeucountry = "SELECT * FROM [IMSCMWS].[dbo].[FGISM1001] where F1 = 'NokFoxM2001' and  f7 = 'CountryName'  and F8 ='" + country + "'";
                            DataTable findeucountrydt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, findeucountry);
                            if (findeucountrydt.Rows.Count > 0)
                            {
                                string updateUF14 = "UPDATE " + dbname + ".[dbo].[PO_CREATE_DT] SET F13 = '1' , F14 = 'EU' WHERE POID = '" + po + "'";
                                int updateUF14SQL = PDataBaseOperation.PExecSQL(dbtype, dbconn, updateUF14);
                            }
                            else
                            {
                                string updateUF13 = "UPDATE " + dbname + ".[dbo].[PO_CREATE_DT] SET F13 = '1' WHERE POID = '" + po + "'";
                                int updateUF13SQL = PDataBaseOperation.PExecSQL(dbtype, dbconn, updateUF13);
                            }
                        }
                    }
                    else 
                    {
                        string updateUF13 = "UPDATE " + dbname + ".[dbo].[PO_CREATE_DT] SET F13 = '1' WHERE POID = '" + po + "'";
                        int updateUF13SQL = PDataBaseOperation.PExecSQL(dbtype, dbconn, updateUF13);
                    }

                }
            }

        }
        
    }
}