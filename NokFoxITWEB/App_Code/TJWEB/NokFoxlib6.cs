using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Configuration;
using System.Collections;
using Economy.BLL;
using Economy.Publibrary;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using SFC.TJWEB;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for FGISlib6
/// </summary>
namespace SFC.TJWEB
{
    public class NokFoxlib6
    {
        private string _dbType;
        private string _dbConn;
        private string _evnType;
        private string _dbName;

        public void send3B2(string dbtype, string dbconn, string evntype, string dbname, string tmpdate)
        {
            _dbType = dbtype;
            _dbConn = dbconn;
            _evnType = evntype;
            _dbName = dbname;
            try
            {
                string send3b2sql = "select * from " + _dbName + ".[dbo].[PO_CREATE_DT] where  POID='343' ";//F11 = '2' and F12 = '1'
                DataTable send3b2sqldt = PDataBaseOperation.PSelectSQLDT(_dbType, _dbConn, send3b2sql);
                if (send3b2sqldt.Rows.Count > 0)
                {
                    for (int i = 0; i < send3b2sqldt.Rows.Count; i++)
                    {
                        string poid = send3b2sqldt.Rows[i]["poid"].ToString();
                        string poitem = send3b2sqldt.Rows[i]["itemid"].ToString();

                        //call send 3b2 program.
                        string status = geneXML( poid, poitem);

                        if (status == "Y")
                        {
                            string updatestatusuf12 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F12 = '2'  where poid = '" + poid + "' and itemid = '" + poitem + "'";
                            int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf12);
                        }
                    }
                }
            }
            catch(Exception e)
            {

            }

        }
        private string geneXML(string po, string poItem)
        {
            try
            {
                //[IMSCMWS].[dbo].[PO_CREATE_DT]
                string selectpodtsql = "select * from  " + _dbName + ".[dbo].[PO_CREATE_DT] where poid ='" + po + "' and itemid = '" + poItem + "'";
                DataTable selectpodtsqldt = PDataBaseOperation.PSelectSQLDT(_dbType, _dbConn, selectpodtsql);

                string vendorPartyPartyID = "?";
                if (selectpodtsqldt.Rows.Count > 0)
                {
                    for (int i = 0; i < selectpodtsqldt.Rows.Count; i++)
                    {
                        vendorPartyPartyID = selectpodtsqldt.Rows[i]["PO_Create_DT_UF2"].ToString();
                    }
                }

                //[IMSCMWS].[dbo].[POSHIPTOADDRESS]
                string selectpomtsql = "select * from " + _dbName + ".[dbo].[PO_CREATE_MT] A left join " + _dbName + ".[dbo].[POSHIPTOADDRESS] B on  A.ID = B.ID where A.poid ='" + po + "'";
                DataTable selectpomtsqldt = PDataBaseOperation.PSelectSQLDT(_dbType, _dbConn, selectpomtsql);

                string classificationCode = "?";
                string transferLocationName = "?";
                string partyKeyPartyID = "?";

                if (selectpomtsqldt.Rows.Count > 0)
                {
                    for (int i = 0; i < selectpomtsqldt.Rows.Count; i++)
                    {
                        classificationCode = selectpomtsqldt.Rows[i]["PO_Create_MT_UF5"].ToString();
                        transferLocationName = selectpomtsqldt.Rows[i]["PO_Create_MT_UF6"].ToString();
                        partyKeyPartyID = selectpomtsqldt.Rows[i]["NumberDefaultIndicator"].ToString();
                    }
                }
                //[IMSCMWS].[dbo].[Delivery_Notification_MT]
                string selectdnmtsql = "select CreationDT from  " + _dbName + ".[dbo].[Delivery_Notification_MT] where poid = '" + po + "'";
                DataTable selectdnmtsqldt = PDataBaseOperation.PSelectSQLDT(_dbType, _dbConn, selectdnmtsql);

                string creationdt = "?";

                if(selectdnmtsqldt.Rows.Count>0)
                {
                    creationdt=selectdnmtsqldt.Rows[0]["CreationDT"].ToString();
                }

                //[IMSCMWS].[dbo].[DELIVERY_DNITEM]
                string selectdnitemsql = "select *  from  " + _dbName + ".[dbo].[DELIVERY_DNITEM] where poid = '" + po + "' and convert(INT, poitemid) = '" + poItem + "'";
                DataTable selectdnitemsqldt = PDataBaseOperation.PSelectSQLDT(_dbType, _dbConn, selectdnitemsql);

                string poitemID = "?";
                string ID = "?";
                string produtID = "?";
                string quantity = "?";
                string outboundDeliveryID = "?";
                string dnitem = "?";

                XmlDocument asnXML = new XmlDocument();

                asnXML.Load(@"E:\ASN_REQUEST.xml");

                asnXML.SelectSingleNode("//MessageHeader//CreationDateTime").InnerText = creationdt;
                asnXML.SelectSingleNode("//DeliveryDespatchAdvice//ID").InnerText = outboundDeliveryID;
                asnXML.SelectSingleNode("//DeliveryDespatchAdvice//VendorParty//InternalID").InnerText = "1000010";//GY
                asnXML.SelectSingleNode("//DeliveryDespatchAdvice//ProductRecipientParty//InternalID").InnerText = "10";//partyKeyPartyID;

                if (selectdnitemsqldt.Rows.Count > 0)
                {
                    for (int i = 0; i < selectdnitemsqldt.Rows.Count; i++)
                    {
                        poitemID = selectdnitemsqldt.Rows[i]["POItemID"].ToString();
                        ID = selectdnitemsqldt.Rows[i]["POID"].ToString();
                        produtID = selectdnitemsqldt.Rows[i]["ProductRecipientID"].ToString();
                        quantity = selectdnitemsqldt.Rows[i]["Total_QTY"].ToString();
                        outboundDeliveryID = selectdnitemsqldt.Rows[i]["DNID"].ToString();
                        dnitem = selectdnitemsqldt.Rows[i]["ItemID"].ToString();

                        //查找<DeliveryDespatchAdvice>
                        XmlNode root = asnXML.SelectSingleNode("DeliveryDespatchAdvice");

                        //创建一个<Item>节点
                        XmlElement xe1 = asnXML.CreateElement("Item");

                        //创建一个<CancellationDocumentIndicator>，并添加到<Item>节点
                        XmlElement xesub1 = asnXML.CreateElement("CancellationDocumentIndicator");
                        xesub1.InnerText = "false";//设置文本节点 
                        xe1.AppendChild(xesub1);//添加到<Item>节点中 

                        //创建一个<ID>，并添加到<Item>节点
                        XmlElement xesub2 = asnXML.CreateElement("ID");
                        xesub2.InnerText = ID;//设置文本节点 
                        xe1.AppendChild(xesub2);//添加到<Item>节点中 

                        //创建一个<TypeCode>，并添加到<Item>节点
                        XmlElement xesub3 = asnXML.CreateElement("TypeCode");
                        xesub3.InnerText = "14";//设置文本节点 
                        xe1.AppendChild(xesub3);//添加到<Item>节点中 

                        //创建一个<DeliveryQuantity>，并添加到<Item>节点
                        XmlElement xesub4 = asnXML.CreateElement("DeliveryQuantity");
                        xesub4.SetAttribute("unitCode", "EA");//设置该节点unitCode属性
                        xesub4.InnerText = quantity;//设置文本节点 
                        xe1.AppendChild(xesub4);//添加到<Item>节点中

                        //创建一个<DeliveryQuantityTypeCode>，并添加到<Item>节点
                        XmlElement xesub5 = asnXML.CreateElement("DeliveryQuantityTypeCode");
                        xesub5.InnerText = "EA";//设置文本节点 
                        xe1.AppendChild(xesub5);//添加到<Item>节点中

                        //创建一个<Product>，并添加到<Item>节点
                        XmlElement xesub6 = asnXML.CreateElement("Product");

                        //创建一个<BuyerID>，并添加到<Product>节点
                        XmlElement xesub61 = asnXML.CreateElement("BuyerID");
                        xesub61.InnerText = produtID;//设置文本节点 
                        xesub6.AppendChild(xesub61);//添加到<Product>节点中

                        //创建一个<TypeCode>，并添加到<Product>节点
                        XmlElement xesub62 = asnXML.CreateElement("TypeCode");
                        xesub62.InnerText = "1";//设置文本节点 
                        xesub6.AppendChild(xesub62);//添加到<Product>节点中

                        xe1.AppendChild(xesub6);//添加到<Item>节点中

                        //创建一个<OriginPurchaseOrderReference>，并添加到<Item>节点
                        XmlElement xesub7 = asnXML.CreateElement("OriginPurchaseOrderReference");

                        //创建一个<ID>，并添加到<OriginPurchaseOrderReference>节点
                        XmlElement xesub71 = asnXML.CreateElement("ID");
                        xesub71.InnerText = ID;//设置文本节点 
                        xesub7.AppendChild(xesub71);//添加到<OriginPurchaseOrderReference>节点中

                        //创建一个<TypeCode>，并添加到<OriginPurchaseOrderReference>节点
                        XmlElement xesub72 = asnXML.CreateElement("TypeCode");
                        xesub72.InnerText = "001";//设置文本节点 
                        xesub7.AppendChild(xesub72);//添加到<OriginPurchaseOrderReference>节点中

                        //创建一个<ItemID>，并添加到<OriginPurchaseOrderReference>节点
                        XmlElement xesub73 = asnXML.CreateElement("ItemID");
                        xesub73.InnerText = poitemID;//设置文本节点 
                        xesub7.AppendChild(xesub73);//添加到<OriginPurchaseOrderReference>节点中

                        xe1.AppendChild(xesub7);//添加到<Item>节点中

                        root.AppendChild(xe1);//添加到<DeliveryDespatchAdvice>节点中 
                    }
                }

                
                asnXML.SelectSingleNode("//Item//ID").InnerText = dnitem;
                asnXML.SelectSingleNode("//Item//DeliveryQuantity").InnerText = quantity;
                asnXML.SelectSingleNode("//Item//DeliveryQuantityTypeCode").InnerText = "EA";
                asnXML.SelectSingleNode("//Item//Product//BuyerID").InnerText = produtID;
                asnXML.SelectSingleNode("//Item//OriginPurchaseOrderReference//ID").InnerText = ID;
                asnXML.SelectSingleNode("//Item//OriginPurchaseOrderReference//ItemID").InnerText = poitemID;
                //asnXML.SelectSingleNode("//VendorParty//PartyKey//PartyID").InnerText = vendorPartyPartyID;
                //asnXML.SelectSingleNode("//ClassificationCode").InnerText = classificationCode;
                //asnXML.SelectSingleNode("//TransferLocationName").InnerText = transferLocationName;
                //asnXML.SelectSingleNode("//ProductRecipientParty//PartyKey//PartyID").InnerText = partyKeyPartyID;
                //asnXML.SelectSingleNode("//BusinessTransactionDocumentReferencePurchaseOrder//BusinessTransactionDocumentReference//ID").InnerText = ID;
                //asnXML.SelectSingleNode("//BusinessTransactionDocumentReferencePurchaseOrder//BusinessTransactionDocumentReference//ItemID").InnerText = itemID;
                //asnXML.SelectSingleNode("//Item//Product//ProductKey//ProductID").InnerText = produtID;
                //asnXML.SelectSingleNode("//Item//DeliveryQuantity//Quantity").InnerText = quantity;
                //asnXML.SelectSingleNode("//BusinessTransactionDocumentReferenceOutboundDelivery//BusinessTransactionDocumentReference//ID").InnerText = outboundDeliveryID;

                asnXML.Save(@"E:\newAsn.xml");

                return "Y";
            }
            catch (Exception e)
            {
                return "N";
            }

        }

        /// <summary>
        /// ASN發送程式
        /// </summary>
        /// <param name="dbtype">sql</param>
        /// <param name="dbconn">數據庫連接字串</param>
        /// <param name="dbname">數據庫名稱</param>
        /// <param name="dnid">DNID</param>
        /// <param name="runmode">REAL或者TEST</param>
        /// <param name="urldbtype">oracle</param>
        /// <param name="urldbconn">url數據庫連接字串</param>
        /// <returns></returns>

        public string UploadXML(string urldbtype,string urldbconn,string dbtype, string dbconn, string dbname, string dnid,string runmode)
        {
            String endpoint = "", UserName = "", Password = "";
            string sqlstr = "select * from FGIS.DATABOM1001 where SITE='HMD' and ITEM='ASN' and FLAG8='" + runmode.ToUpper() + "'";
            DataTable dtsqlstr = PDataBaseOperation.PSelectSQLDT(urldbtype, urldbconn, sqlstr);
            if(dtsqlstr.Rows.Count>0)
            {
                endpoint = dtsqlstr.Rows[0]["FLAG13"].ToString();
                UserName = dtsqlstr.Rows[0]["FLAG2"].ToString();
                Password = dtsqlstr.Rows[0]["FLAG5"].ToString();
            }
            else
            {
                return "0";
            }
            string mes = "";
            
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"xmlRecord\" + dnid+".xml");
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = doc.OuterXml;
            byte[] data = encoding.GetBytes(postData);
            // HTTP POST 請求物件
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/soap+xml;charset=\"utf-8\"";
            httpWReq.ContentLength = data.Length;
            httpWReq.Headers.Add("SOAPAction", "http://tempuri.org/HelloWorld");
            System.Net.WebProxy webProxy = new System.Net.WebProxy("10.191.131.45", 3128);
            //webProxy.Credentials = new NetworkCredential("F6100538", "zxy20110715");
            httpWReq.Proxy = webProxy;
            httpWReq.Credentials = new System.Net.NetworkCredential(UserName, Password);//Prod

            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                string sendtime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //更新DN主表
                string upddnmt = "update " + dbname + ".[dbo].[Delivery_Notification_MT] set SendFlag='Y',SendTime='" + sendtime + "',Flag1='3' where DNID='" + dnid + "'";
                int upd2 = PDataBaseOperation.PExecSQL(dbtype, dbconn, upddnmt);
                //更新DN明顯表
                string upddndt = "update " + dbname + ".[dbo].[Delivery_DNITEM] set UF1='3' where DNID='" + dnid + "' and UF1='2'";
                int upd4 = PDataBaseOperation.PExecSQL(dbtype, dbconn, upddndt);
                //更新PO主表和明顯表
                string checkpo = "select * from [IMSCMWS].[dbo].[Delivery_DNITEM] where DNID='" + dnid + "' and UF1='3'";
                DataTable dtcheckpo = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, checkpo);
                if(dtcheckpo.Rows.Count>0)
                {
                    for(int i=0;i<dtcheckpo.Rows.Count;i++)
                    {
                        string poid = dtcheckpo.Rows[i]["POID"].ToString();
                        string itemid = dtcheckpo.Rows[i]["POItemID"].ToString();
                        string dnitem = dtcheckpo.Rows[i]["ITEMID"].ToString();
                        string updpomt = "update " + dbname + ".[dbo].[PO_CREATE_MT] set PO_Create_MT_UF9='Y',PO_Create_MT_UF10='" + sendtime + "',DNID='" + dnid + "'where POID='" + poid + "'";
                        int upd1 = PDataBaseOperation.PExecSQL(dbtype, dbconn, updpomt);
                        string updpodt = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F12='2',DNID='" + dnid + "',DNITEM='" + dnitem + "' where POID='" + poid + "' and ItemID='" + itemid + "'";
                        int upd3 = PDataBaseOperation.PExecSQL(dbtype, dbconn, updpodt);
                    }
                }                
            }
            catch (Exception ex)
            {
                mes = ex.ToString();
            }
            return mes;
        }
        public string gene3B2xml(string dbtype, string dbconn, string dbname, string dnid)
        {
            try
            {
                //DN主表資料

                //[IMSCMWS].[dbo].[Delivery_Notification_MT]
                string selectdnmtsql = "select distinct mt.DNID,mt.CreationDT,sp.NumberDefaultIndicator partyKeyPartyID,pomt.SELLERPARTYID venderid" +
                                       " from  " + dbname + ".[dbo].[Delivery_Notification_MT] mt," + dbname + ".[dbo].[POSHIPTOADDRESS] sp," + dbname + ".[dbo].[PO_CREATE_MT] pomt" +
                                       " where  mt.POID=sp.POID and mt.POID=pomt.POID and sp.POID=pomt.POID and mt.DNID = '" + dnid + "'";
                DataTable selectdnmtsqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, selectdnmtsql);

                string creationdt = "?";
                string partyKeyPartyID = "?";
                string venderid = "?";

                if (selectdnmtsqldt.Rows.Count > 0)
                {
                    creationdt = selectdnmtsqldt.Rows[0]["CreationDT"].ToString().Replace("000Z","0000000Z");
                    partyKeyPartyID = selectdnmtsqldt.Rows[0]["partyKeyPartyID"].ToString();
                    venderid = selectdnmtsqldt.Rows[0]["venderid"].ToString();
                }

                XmlDocument asnXML = new XmlDocument();

                asnXML.Load(AppDomain.CurrentDomain.BaseDirectory + @"xmlRecord\ASN_REQUEST.xml");

                asnXML.SelectSingleNode("//MessageHeader//CreationDateTime").InnerText = creationdt;
                asnXML.SelectSingleNode("//DeliveryDespatchAdvice//ID").InnerText = dnid;
                asnXML.SelectSingleNode("//DeliveryDespatchAdvice//VendorParty//InternalID").InnerText = venderid;//GY
                asnXML.SelectSingleNode("//DeliveryDespatchAdvice//ProductRecipientParty//InternalID").InnerText = partyKeyPartyID;//partyKeyPartyID;

                //DN_Item資料


                string selectpodtsql = "select dndt.POID POID,dndt.POItemID POItemID,dndt.ItemID DNItemID,dndt.ProductRecipientID produtID," +
                                       " dndt.Total_QTY quantity,dndt.UNIT UnitCode,sp.NumberDefaultIndicator partyKeyPartyID" +
                                       " from [IMSCMWS].[dbo].[POSHIPTOADDRESS] sp,[IMSCMWS].[dbo].[Delivery_DNITEM] dndt" +
                                       " where dndt.POID=sp.POID and dndt.POItemID=sp.ItemID and dndt.DNID='" + dnid + "' and dndt.UF1='2'";
                DataTable selectpodtsqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, selectpodtsql);

                string POID = "?";
                string POItemID = "?";
                string produtID = "?";
                string quantity = "?";
                string DNItemID = "?";
                string UnitCode = "?";
                //string vendorPartyPartyID = "?";
                //string classificationCode = "?";
                //string transferLocationName = "?";

                if (selectpodtsqldt.Rows.Count > 0)
                {
                    for (int i = 0; i < selectpodtsqldt.Rows.Count; i++)
                    {
                        POID = selectpodtsqldt.Rows[i]["POID"].ToString();
                        POItemID = selectpodtsqldt.Rows[i]["POItemID"].ToString();
                        produtID = selectpodtsqldt.Rows[i]["produtID"].ToString();
                        quantity = selectpodtsqldt.Rows[i]["quantity"].ToString();
                        DNItemID = selectpodtsqldt.Rows[i]["DNItemID"].ToString();
                        UnitCode = selectpodtsqldt.Rows[i]["UnitCode"].ToString();
                        string subdnitem = "";
                        for(int ii=0;ii<DNItemID.Length;ii++)
                        {
                            if(DNItemID.Substring(ii,1)!="0")
                            {
                                subdnitem = DNItemID.Substring(ii, DNItemID.Length - ii);
                                break;
                            }
                        }
                        //vendorPartyPartyID = selectpodtsqldt.Rows[i]["vendorPartyPartyID"].ToString();
                        //classificationCode = selectpodtsqldt.Rows[i]["classificationCode"].ToString();
                        //transferLocationName = selectpodtsqldt.Rows[i]["transferLocationName"].ToString();
                        
                        //查找<DeliveryDespatchAdvice>

                        //XmlNode root = asnXML.SelectSingleNode("soap:Body/glob:DeliveryDespatchAdviceNotification/DeliveryDespatchAdvice");
                        XmlNodeList root = asnXML.GetElementsByTagName("DeliveryDespatchAdvice");
                        XmlNode root1=root.Item(0);

                        //创建一个<Item>节点
                        XmlElement xe1 = asnXML.CreateElement("Item");

                        //创建一个<CancellationDocumentIndicator>，并添加到<Item>节点
                        XmlElement xesub1 = asnXML.CreateElement("CancellationDocumentIndicator");
                        xesub1.InnerText = "false";//设置文本节点 
                        xe1.AppendChild(xesub1);//添加到<Item>节点中 

                        //创建一个<ID>，并添加到<Item>节点
                        XmlElement xesub2 = asnXML.CreateElement("ID");
                        xesub2.InnerText = subdnitem;//设置文本节点 
                        xe1.AppendChild(xesub2);//添加到<Item>节点中 

                        //创建一个<TypeCode>，并添加到<Item>节点
                        XmlElement xesub3 = asnXML.CreateElement("TypeCode");
                        xesub3.InnerText = "14";//设置文本节点 
                        xe1.AppendChild(xesub3);//添加到<Item>节点中 

                        //创建一个<DeliveryQuantity>，并添加到<Item>节点
                        XmlElement xesub4 = asnXML.CreateElement("DeliveryQuantity");
                        xesub4.SetAttribute("unitCode", "EA");//设置该节点unitCode属性
                        xesub4.InnerText = quantity;//设置文本节点 
                        xe1.AppendChild(xesub4);//添加到<Item>节点中

                        //创建一个<DeliveryQuantityTypeCode>，并添加到<Item>节点
                        XmlElement xesub5 = asnXML.CreateElement("DeliveryQuantityTypeCode");
                        xesub5.InnerText = UnitCode;//设置文本节点 
                        xe1.AppendChild(xesub5);//添加到<Item>节点中

                        //创建一个<Product>，并添加到<Item>节点
                        XmlElement xesub6 = asnXML.CreateElement("Product");

                        //创建一个<BuyerID>，并添加到<Product>节点
                        XmlElement xesub61 = asnXML.CreateElement("BuyerID");
                        xesub61.InnerText = produtID;//设置文本节点 
                        xesub6.AppendChild(xesub61);//添加到<Product>节点中

                        //创建一个<TypeCode>，并添加到<Product>节点
                        XmlElement xesub62 = asnXML.CreateElement("TypeCode");
                        xesub62.InnerText = "1";//设置文本节点 
                        xesub6.AppendChild(xesub62);//添加到<Product>节点中

                        xe1.AppendChild(xesub6);//添加到<Item>节点中

                        //创建一个<OriginPurchaseOrderReference>，并添加到<Item>节点
                        XmlElement xesub7 = asnXML.CreateElement("OriginPurchaseOrderReference");

                        //创建一个<ID>，并添加到<OriginPurchaseOrderReference>节点
                        XmlElement xesub71 = asnXML.CreateElement("ID");
                        xesub71.InnerText = POID;//设置文本节点 
                        xesub7.AppendChild(xesub71);//添加到<OriginPurchaseOrderReference>节点中

                        //创建一个<TypeCode>，并添加到<OriginPurchaseOrderReference>节点
                        XmlElement xesub72 = asnXML.CreateElement("TypeCode");
                        xesub72.InnerText = "001";//设置文本节点 
                        xesub7.AppendChild(xesub72);//添加到<OriginPurchaseOrderReference>节点中

                        //创建一个<ItemID>，并添加到<OriginPurchaseOrderReference>节点
                        XmlElement xesub73 = asnXML.CreateElement("ItemID");
                        xesub73.InnerText = POItemID;//设置文本节点 
                        xesub7.AppendChild(xesub73);//添加到<OriginPurchaseOrderReference>节点中

                        xe1.AppendChild(xesub7);//添加到<Item>节点中

                        root1.AppendChild(xe1);//添加到<DeliveryDespatchAdvice>节点中                     
                    }
                }
                
                asnXML.Save(AppDomain.CurrentDomain.BaseDirectory + @"xmlRecord\" + dnid + ".xml");

                return "Y";
            }
            catch (Exception e)
            {
                return "N";
            }
        }
    }
}