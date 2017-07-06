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
using Microsoft.Adapter.SAP;

/// <summary>
/// Summary description for NokFoxlib1
/// </summary>
public class NokFoxlib1
{
    public static string A1 = "中港出口物流費用分攤系統";
    public static string A2 = "新增數據成功！";

    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd");
    protected string CuurNumber = (DateTime.Now.Hour * 60 + DateTime.Now.Minute).ToString();//霜阨瘍
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    ClassLibraryPDBA1.Class1 LibPDBA1Pointer = new ClassLibraryPDBA1.Class1();
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    string DBType = "oracle";
    static int PredayQty = 10;
    static int Gdaycnt = 2;
    static string tmpType = "";
    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static int SEQID;

    public static string GetSapDN(string DN, string dboReadS, string dboWriteS, string bdate, string edate,string DBName,string org,string plant)
    {
        try
        {
            if (dboReadS.ToUpper() == "REAL")
            {
                dboReadS = "ASHOST=10.134.28.98 SYSNR=00 CLIENT=802 USER=RFCSHARE02 PASSWD=it0215 LANG=en";
            }
            else
            {          
                dboReadS = "ASHOST=10.134.92.27 SYSNR=00 CLIENT=802 USER=FIHBJKFC PASSWD=FOXCONN8 LANG=en";
            }

            // SAPConnection con = new SAPConnection("ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh");
            // SAPConnection con = new SAPConnection("ASHOST=10.134.28.98;  CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh");
            //  SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh");
            //   SAPConnection con = new SAPConnection("ASHOST=110.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh");
            SAPConnection con = new SAPConnection(dboReadS);


            con.Open();

            SAPCommand cmd = new SAPCommand(con);
            //cmd.CommandText = "EXEC ZRFC_SD_BBRY_0001   @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV , @SOLD_TO= @SOLD_TOV, @OUTPUT_HEADER=@OUTPUT_HEADERV output, @OUTPUT_ITEM=@OUTPUT_ITEMV output ";
            cmd.CommandText = "EXEC ZRFC_SD_MS_0001   @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV , @VBELN=@VBELNV , @VKORG=@VKORGV ,@WERKS=@WERKSV ,  @OUTPUT_HEADER=@OUTPUT_HEADERV output, @OUTPUT_ITEM=@OUTPUT_ITEMV output, @OUTPUT_PACKING=@OUTPUT_PACKINGV output ";
            //  cmd.CommandText = "EXEC ZRFC_INTEL_B2B_3B2  @T_INPUT=@TINPUT output, @T_OUTPUT=@TOUTPUT output,@T_MSG=@TMSG output";
            //@VBELN=@VBELNV ,
            SAPParameter BEGIN_DATEV = new SAPParameter("@BEGIN_DATEV", ParameterDirection.Input);
            BEGIN_DATEV.Value = bdate;
            cmd.Parameters.Add(BEGIN_DATEV);

            SAPParameter END_DATEV = new SAPParameter("@END_DATEV", ParameterDirection.Input);
            END_DATEV.Value = edate;
            cmd.Parameters.Add(END_DATEV);

            SAPParameter VBELNV = new SAPParameter("@VBELNV", ParameterDirection.Input);
            VBELNV.Value = DN;
            cmd.Parameters.Add(VBELNV);


            SAPParameter VKORGV = new SAPParameter("@VKORGV", ParameterDirection.Input);
            VKORGV.Value = org;
            cmd.Parameters.Add(VKORGV);


            SAPParameter WERKSV = new SAPParameter("@WERKSV", ParameterDirection.Input);
            WERKSV.Value = plant;
            cmd.Parameters.Add(WERKSV);





            SAPParameter OUTPUT_HEADERV = new SAPParameter("@OUTPUT_HEADERV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUT_HEADERV);
            SAPParameter OUTPUT_ITEMV = new SAPParameter("@OUTPUT_ITEMV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUT_ITEMV);
            SAPParameter OUTPUT_PACKINGV = new SAPParameter("@OUTPUT_PACKINGV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUT_PACKINGV);
            SAPDataReader dr = cmd.ExecuteReader();
            DataTable SAPdt1 = (DataTable)cmd.Parameters["@OUTPUT_HEADERV"].Value;
            DataTable SAPdt2 = (DataTable)cmd.Parameters["@OUTPUT_ITEMV"].Value;
            //DataTable SAPdt3 = (DataTable)cmd.Parameters["@OUTPUT_PACKINGV"].Value;
            //string SSS = "";
            //for (int I = 0; I < SAPdt3.Columns.Count; I++)
            //{
            //    SSS = SSS + SAPdt3.Columns[I].ColumnName + ",";



            //}


            if (SAPdt1.Rows.Count.Equals(0) || SAPdt2.Rows.Count.Equals(0))
            {
                return "False";
            }
            string Error = "";
            string sqlstr = "DNID,CreationDT,IssueDT,GrossWeight,NetWeight,Country";
            //  sqlstr = sqlstr + ",BuyerPartyInternalID,VendorPartyVendorID ";
            Int64 v1 = 0, v2 = 0, v3 = 0;
            int v4 = 0, v5 = 0;
            string tmp6 = "", tmp7 = "";
            DataSet DNdt = null;

            int sapv1 = SAPdt1.Rows.Count;
            int sapv2 = SAPdt2.Rows.Count;

            if ((sapv1 <= 0) && (sapv2 <= 0)) return (""); // No data 

            string[,] arrDN_MT = new string[sapv1 + 1, 5 + 1];
            for (v2 = 0; v2 <= sapv1; v2++)
                for (v3 = 0; v3 <= 5; v3++)
                    arrDN_MT[v2, v3] = "";



            int I5 = 0, I6 = 0;
            string sysdatetime = System.DateTime.Now.ToString("yyMMddHHmmssmm"); // 
            string ID = "F" + SAPdt1.Rows[0]["VBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;
            string tmpDN = "";
            for (int i = 0; i < SAPdt1.Rows.Count; i++)
            {

                sysdatetime = System.DateTime.Now.ToString("yyMMddHHmmssmm");
                v1 = Convert.ToInt64(sysdatetime) + i;
                sysdatetime = v1.ToString();   //  time+ count var 

                ID = "F" + SAPdt1.Rows[0]["VBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;
                tmpDN = SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8);

                sqlstr = "select count(*) ct from " + DBName + ".[dbo].Delivery_Notification_MT where DNID='" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);

                sqlstr = "select * from " + DBName + ".[dbo].PO_CREATE_MT where POID='" + SAPdt1.Rows[i]["POID"].ToString() + "'";
                DataTable dt1 = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);

                if (dt.Rows[0]["ct"].Equals(0)&&dt1.Rows.Count>0)
                {
                    string vendercode = dt1.Rows[0]["SELLERPARTYID"].ToString();
                    arrDN_MT[i + 1, 1] = tmpDN;
                    arrDN_MT[i + 1, 2] = ID;
                    //     ,HUQTY-----pallet_QTY
                    //     ,BoxQTY-----Carton_QTY
                    //     ,SerialIDQTY----QTY
                    //,ItemQTY -----""
                    // netunitcode----unit(SAP)

                    //sqlstr = "GrossUnitCode,ID,DNID,CreationDT,IssueDT,GrossWeight,NetWeight,Country,BuyerPartyInternalID, VendorPartyVendorID ,HUQTY ,BoxQTY ,SerialIDQTY  ,ItemQTY ,netunitcode ,VolumeUnitCode,Volume";
                    //sqlstr = " update Delivery_Notification_MT set Volume = '" + SAPdt3.Rows[i]["TAVOL"] + "'  where DNID= '" + tmpDN + "'";
                    //20140912 去掉IssueDT
                    sqlstr = "GrossUnitCode,ID,DNID,CreationDT,GrossWeight,NetWeight,Country,BuyerPartyInternalID, VendorPartyVendorID ,HUQTY ,BoxQTY ,SerialIDQTY  ,ItemQTY ,netunitcode ,VolumeUnitCode,Volume,WayBillID,DocumentID,Flag1,Flag2,Flag3,Flag4,Flag5,POID";
                    sqlstr = "insert into  " + DBName + ".[dbo].Delivery_Notification_MT (" + sqlstr + " ) values ('";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["UNIT"].ToString().Replace("KG", "KGM") + "','" + ID + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    //2013-12-02T12:12:12.1234567Z
                    sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["BLDAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";
                    //sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["WADAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["BRGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["NTGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["COUNTRY_KEY"] + "',";
                    sqlstr = sqlstr + "'" + "" + "',";
                    sqlstr = sqlstr + "'" + vendercode + "'  , ";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["pallet_QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["Carton_QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["QTY"] + ",";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["ITEM_QTY"] + ",";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["UNIT"].ToString().Replace("KG", "KGM") + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["VOLEH"].ToString().Replace("M3", "LTR") + "',";



                    //  Thread.Sleep(100);  // 1000 = 1sec  sleep
                    // 20140508
                    //sqlstr = sqlstr + "'" + (System.Convert.ToInt32(SAPdt1.Rows[i]["TAVOL"]) * 1000).ToString() + "')";
                    //int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    //sqlstr = "  update Delivery_Notification_MT set ";
                    //sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
                    //I5 = PDataBaseOperation.PExecSQL("SQL", d boWriteS, sqlstr);
                    //sqlstr = " update  a ";
                    //sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
                    //sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
                    //sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.ID= '" + ID + "'";
                    //I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["TAVOL"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["HMD_DNN"] + "',";
                    sqlstr = sqlstr + "'" + sysdatetime + "'" + ",";
                    sqlstr = sqlstr + "'1','1','1','1','1'" + ",";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["POID"] + "')";
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    sqlstr = string.Format(@"select DeliveryStartDT,PO_Create_DT_UF1 from " + DBName + ".[dbo].PO_Create_DT where POID='" + SAPdt1.Rows[i]["POID"] + "' order by POID,ItemID");
                    DataTable dtpo = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                    if(dtpo.Rows.Count>0)
                    {
                        string date1 = dtpo.Rows[0]["DeliveryStartDT"].ToString();
                        string date2 = dtpo.Rows[0]["PO_Create_DT_UF1"].ToString();
                        sqlstr = "  update " + DBName + ".[dbo].Delivery_Notification_MT set ";
                        sqlstr = sqlstr + "IssueDT ='" + date1 + "',ArrivalDT ='" + date2 + "'   where DNID= '" + tmpDN + "'";
                        I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    }
                }
            }


            for (int i = 0; i < SAPdt2.Rows.Count; i++)
            {
                sqlstr = "select ID,WayBillID,DOCUMENTID from " + DBName + ".[dbo].Delivery_Notification_MT where DNID='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' ";
                DNdt = PDataBaseOperation.PSelectSQLDS("SQL", dboWriteS, sqlstr);
                v4 = DNdt.Tables[0].Rows.Count;
                string DNNID = "";
                string documentid = "";
                if (v4 > 0)
                {
                    tmp6 = DNdt.Tables[0].Rows[0]["ID"].ToString().Trim();
                    DNNID = DNdt.Tables[0].Rows[0]["WayBillID"].ToString().Trim();
                    documentid = DNdt.Tables[0].Rows[0]["DocumentID"].ToString().Trim();
                }
                else
                {
                    continue;
                }



                sqlstr = "select count(*) ct from " + DBName + ".[dbo].Delivery_DNITEM where DNID='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' and ItemID ='" + SAPdt2.Rows[i]["POSNR"] + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows[0]["ct"].Equals(0))
                {


                    sqlstr = "insert into  " + DBName + ".[dbo].Delivery_DNITEM ( ID,DNID,ItemID,POID,POItemID,InternalID,ProductRecipientID , Total_QTY,UNIT,Carton_QTY,Pallet_QTY,DocumentID) values ( ";
                    // 20140507 sqlstr = sqlstr + "'" + ID + "',";
                    sqlstr = sqlstr + "'" + tmp6 + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["POSNR"] + "',";
                    //20151223給PO補0到長度為十位
                    string poid = "";
                    if (SAPdt2.Rows[i]["VGBEL"].ToString().Trim().Length == 8) { poid = "00" + SAPdt2.Rows[i]["VGBEL"].ToString().Trim(); }
                    else if (SAPdt2.Rows[i]["VGBEL"].ToString().Trim().Length == 9) { poid = "0" + SAPdt2.Rows[i]["VGBEL"].ToString().Trim(); }
                    else { poid = SAPdt2.Rows[i]["VGBEL"].ToString().Trim(); }
                    sqlstr = sqlstr + "'" + poid + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VGPOS"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["MATNR"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["KDMAT"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["Total_QTY"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["UNIT"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["Carton_QTY"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["Pallets_QTY"] + "',";
                    sqlstr = sqlstr + "'" + documentid + "')";
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    //if (v > 0) // Succ update POID into DN
                    //{
                    //    tmpDN = SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8); // DNID
                    //    tmp7 = SAPdt2.Rows[i]["VGBEL"].ToString().Trim(); // POID
                    //    sqlstr = " update " + DBName + ".[dbo].Delivery_Notification_MT set POID = '" + poid + "'  where DNID= '" + tmpDN + "'";
                    //    I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    //}
                    ////20170309
                    //string strdnnitem = "select * from " + DBName + ".[dbo].[DNNITEM] where DNID='" + DNNID + "'" +
                    //    " and POID='" + poid + "' and POItemID='" + SAPdt2.Rows[i]["VGPOS"].ToString().Substring(1, 5) + "'" +
                    //    " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                    //DataTable dtdnn = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, strdnnitem);
                    //string updDnn = "";
                    //if (dtdnn.Rows.Count > 0)
                    //{
                    //    updDnn = "update " + DBName + ".[dbo].[DNNITEM] set FOXDN='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',FOXDNITEM='" + SAPdt2.Rows[i]["POSNR"] + "'" +
                    //        " where DNID='" + DNNID + "'" +
                    //        " and POID='" + poid + "' and POItemID='" + SAPdt2.Rows[i]["VGPOS"].ToString().Substring(1, 5) + "'" +
                    //        " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                    //}
                    //else
                    //{
                    //    string strdnnitem1 = "select * from " + DBName + ".[dbo].[DNNITEM] where DNID='" + DNNID + "'" +
                    //                           " and POID='" + poid + "'" +
                    //                           " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                    //    DataTable dtdnn1 = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, strdnnitem1);
                    //    if (dtdnn1.Rows.Count > 0)
                    //    {
                    //        updDnn = "update " + DBName + ".[dbo].[DNNITEM] set UF1='HMD PO & PO item is wrong'" +
                    //        " where DNID='" + DNNID + "'" +
                    //        " and POID='" + poid + "'" +
                    //        " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                    //    }
                    //    else
                    //    {
                    //        updDnn = "update " + DBName + ".[dbo].[DNN_MT] set UF1='HMD PO & PO item is wrong'" +
                    //        " where DNID='" + DNNID + "'";
                    //    }
                    //}
                    //int i6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, updDnn);
                }
            }


            ////=========================================================================================
            //for (int i = 0; i < SAPdt3.Rows.Count; i++)
            //{
            //    sqlstr = "select ID,DOCUMENTID from " + DBName + ".[dbo].Delivery_Notification_MT where DNID='" + SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' ";
            //    DNdt = PDataBaseOperation.PSelectSQLDS("SQL", dboWriteS, sqlstr);
            //    v4 = DNdt.Tables[0].Rows.Count;
            //    string documentid = "";
            //    if (v4 > 0)
            //    {
            //        tmp6 = DNdt.Tables[0].Rows[0]["ID"].ToString().Trim();
            //        documentid = DNdt.Tables[0].Rows[0]["DOCUMENTID"].ToString().Trim();
            //    }
            //    else
            //    {
            //        continue;
            //    }

            //    sqlstr = "select count(*) ct from " + DBName + ".[dbo].Delivery_Notification_HU where DNID='" + SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' and HUID ='" + SAPdt3.Rows[i]["VENUM"] + "' and itemid ='" + SAPdt3.Rows[i]["POSNR"] + "'";
            //    DataTable dgt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
            //    if (dgt.Rows[0]["ct"].Equals(0))
            //    {



            //        sqlstr = "insert into  " + DBName + ".[dbo].Delivery_Notification_HU (  ID,DNID,HUID,ItemID,LoadInternalID,Delivery_Notification_HU_UF1,Delivery_Notification_HU_UF2,Delivery_Notification_HU_UF3,Delivery_Notification_HU_UF4,LoadQA,Delivery_Notification_HU_UF5,DocumentID) values ( ";
            //        sqlstr = sqlstr + "'" + ID + "',";

            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VENUM"] + "',";

            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["POSNR"] + "',";
            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["TVTYT"] + "',";
            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["NTGEW"] + "',";
            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["BRGEW"] + "',";
            //        sqlstr = sqlstr + "'KG',";
            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["TAVOL"] + "',";
            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VEMNG"] + "',";
            //        sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VOLEH"] + "',";
            //        sqlstr = sqlstr + "'" + documentid + "')";

            //        int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

            //        // Delivery_Notification_MT- Volume資料寫SAP Output_header-TAVOL欄位元值。



            //        //if (v > 0) // Succ update POID into DN
            //        //{
            //        //    tmpDN = SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8); // DNID
            //        //   // tmp7 = SAPdt3.Rows[i]["VGBEL"].ToString().Trim(); // POID
            //        //    sqlstr = " update Delivery_Notification_MT set Volume = '" + SAPdt3.Rows[i]["TAVOL"] + "'  where DNID= '" + tmpDN + "'";
            //        //    I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
            //        //}
            //    }
            //}




            //  20140504
            //  sqlstr = "  update Delivery_Notification_MT set ";
            //  sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
            //  int I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
            //  sqlstr = " update  a ";
            //  sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
            //  sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
            //  sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.ID= '" + ID + "'";
            //  int I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

            //// 20140508
            //for (int i = 0; i < SAPdt1.Rows.Count; i++)
            //{
            //    tmpDN = arrDN_MT[i + 1, 1];
            //    ID = arrDN_MT[i + 1, 2];
            //    if ((ID != "") && (tmpDN != ""))
            //    {
            //        sqlstr = "  update " + DBName + ".[dbo].Delivery_Notification_MT set ";
            //        sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from " + DBName + ".[dbo].DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
            //        I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
            //        sqlstr = " update  a ";
            //        sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
            //        sqlstr = sqlstr + " from " + DBName + ".[dbo].Delivery_Notification_MT a ," + DBName + ".[dbo].PO_Create_DT b , " + DBName + ".[dbo].PO_Create_MT  c ";
            //        sqlstr = sqlstr + " where  a.POID =c.POID and a.POID =b.POID and a.DNID = '" + tmpDN + "'";
            //        I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
            //    }
            //}


            return "True";
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            return "False";
        }

    }  
}