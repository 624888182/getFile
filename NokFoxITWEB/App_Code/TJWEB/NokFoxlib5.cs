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
/// <summary>
/// Summary description for FGISlib5
/// </summary>
namespace SFC.TJWEB
{
    public class NokFoxlib5
    {
        public string GetPoHeader(string DbReadType,string DbRead,string DbWriteType, string Dbwrite, string DBName, string starttime, string endtime, string PO,string Envirment,string FactoryCode)
        {
            string documentid = DateTime.Now.ToString("yyyyMMddHHmmssff");
            String endpoint = "", UserName = "",Password="" ;
            string sql = "select * from FGIS.DATABOM1001 where upper(ITEM)='POHEADER' and upper(flag8)=upper('"+Envirment+"')";
            DataTable dt = PDataBaseOperation.PSelectSQLDT(DbReadType, DbRead, sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    endpoint=dt.Rows[0]["flag13"].ToString();
                    UserName = dt.Rows[0]["flag2"].ToString();
                    Password = dt.Rows[0]["flag5"].ToString();
                }
            }
            #region
            //if (Envirment.ToUpper() == "UAT")
            //{
            //    //endpoint = "https://my339666.sapbydesign.com/sap/bc/srt/scs/sap/querypurchaseorderqueryin?sap-vhost=my339666.sapbydesign.com";
            //    //UserName = "_FIH";
            //    //Password = "Welcome1";
            //    endpoint = "https://my340202.sapbydesign.com/sap/bc/srt/scs/sap/querypurchaseorderqueryin?sap-vhost=my340202.sapbydesign.com";
            //    UserName = "_FIHPROD";
            //    Password = "Welcome1";
            //}
            //else if (Envirment.ToUpper() == "TEST")//測試環境
            //{
            //    endpoint = "https://my338886.sapbydesign.com/sap/bc/srt/scs/sap/querypurchaseorderqueryin";
            //    UserName = "_FIH";
            //    Password = "Welcome1";
            //}
            //else if (Envirment.ToUpper() == "REAL")//正式環境
            //{
            //    endpoint = "https://my339319.sapbydesign.com/sap/bc/srt/scs/sap/querypurchaseorderqueryin";
            //    UserName = "_FIHPROD";
            //    Password = "GYxZkga2x6Yv";
            //}
            #endregion
            if (PO == null || PO == "")
            {
                #region
                //取消按照日期抓取PO 20170608
                //2017-01-01T08:46:55Z
                //2017-01-24T08:46:55Z  u can contorl this .
                //string starttime = "2017-01-01T08:46:55Z";
                //string endtime = "2017-01-15T08:46:55Z";
                //if (starttime == "") starttime = DateTime.Now.ToString("yyyyMMddHHmmss");
                //if (endtime == "") endtime = DateTime.Now.ToString("yyyyMMddHHmmss");
                //starttime = starttime.Substring(0, 4) + "-" + starttime.Substring(4, 2) + "-" + starttime.Substring(6, 2) + "T" + starttime.Substring(8, 2) + ":" + starttime.Substring(10, 2) + ":" + starttime.Substring(12, 2) + "Z";
                //endtime = endtime.Substring(0, 4) + "-" + endtime.Substring(4, 2) + "-" + endtime.Substring(6, 2) + "T" + endtime.Substring(8, 2) + ":" + endtime.Substring(10, 2) + ":" + endtime.Substring(12, 2) + "Z";                
                #endregion

                // 讀取 request_message
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + "GetPoList.xml");
                XmlNode sLower = doc.CreateNode(XmlNodeType.Element, "LowerBoundarySellerPartyID", null);
                sLower.InnerText = FactoryCode;
                doc.SelectSingleNode("//SelectionByPartySellerPartyKeyPartyID").AppendChild(sLower);
                XmlNode sUpper = doc.CreateNode(XmlNodeType.Element, "UpperBoundarySellerPartyID", null);
                sUpper.InnerText = FactoryCode;
                doc.SelectSingleNode("//SelectionByPartySellerPartyKeyPartyID").AppendChild(sUpper);

                #region
                //取消按照日期抓取PO 20170608
                ////starttime and endtime put the value into the xml file
                //XmlNodeList elemlist = doc.GetElementsByTagName("LowerBoundarySystemAdministrativeDataCreationDateTime");
                //elemlist[0].InnerXml = starttime;
                ////string aaa = elemlist[0].Value;
                //XmlNodeList elemlistupper = doc.GetElementsByTagName("UpperBoundarySystemAdministrativeDataCreationDateTime");
                //elemlistupper[0].InnerXml = endtime;
                #endregion

                //doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + "GetUpdataPoList.xml");
                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = doc.OuterXml;
                byte[] data = encoding.GetBytes(postData);
                 
                // HTTP POST 請求物件
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "text/xml;charset=\"utf-8\"";
                httpWReq.ContentLength = data.Length;
                httpWReq.Headers.Add("SOAPAction", "http://tempuri.org/HelloWorld");

                System.Net.WebProxy webProxy = new System.Net.WebProxy("10.191.131.45", 3128);
                //webProxy.Credentials = new NetworkCredential("502112", "Mr123321..");
                httpWReq.Proxy = webProxy;
                httpWReq.Credentials = new System.Net.NetworkCredential(UserName, Password);
                //SOAPAction =  "http://tempuri.org/HelloWorld";

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                XmlDocument doct = new XmlDocument();
                doct.LoadXml(responseString);


                doct.Save(AppDomain.CurrentDomain.BaseDirectory + @"POXmlRecord\" + documentid + "POHeader.xml");

                XmlReader xr = XmlReader.Create(new System.IO.StringReader(doct.OuterXml));
                DataSet dss = new DataSet();
                dss.ReadXml(xr);

                //put the purchaseorder in array
                if (dss.Tables.Contains("purchaseorder"))
                {
                    string[] aaa = new string[dss.Tables["purchaseorder"].Rows.Count];
                    //string[] aaa = new string[1];
                    if (dss == null) return "";
                    if (dss.Tables["purchaseorder"] == null) return "";
                    for (int i = 0; i < dss.Tables["purchaseorder"].Rows.Count; i++)
                    {
                        aaa[i] = dss.Tables["purchaseorder"].Rows[i]["purchaseorderid"].ToString();
                        // aaa[0] = "487";
                    }
                    GetPoDetail(documentid, DbReadType, DbRead, DbWriteType, Dbwrite, DBName, aaa, Envirment);
                }
            }
            else
            {
                string[] aaa = new string[1];
                aaa[0] = PO;
                GetPoDetail(documentid, DbReadType, DbRead, DbWriteType, Dbwrite, DBName, aaa, Envirment);
            }
            return "";
        }

        public string GetPoDetail(string Documentid, string DbReadType, string DbRead, string DbWriteType, string DbWrite, string DBName, string[] poList, string Envirment)
        {
            String endpoint = "", UserName = "", Password = "";
            string sql = "select * from FGIS.DATABOM1001 where upper(ITEM)='PODETAIL' and upper(flag8)=upper('" + Envirment + "')";
            DataTable dt = PDataBaseOperation.PSelectSQLDT(DbReadType, DbRead, sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    endpoint = dt.Rows[0]["flag13"].ToString();
                    UserName = dt.Rows[0]["flag2"].ToString();
                    Password = dt.Rows[0]["flag5"].ToString();
                }
            }
            #region
            //if (Envirment.ToUpper() == "UAT")
            //{
            //    //endpoint = "https://my339666.sapbydesign.com/sap/bc/srt/scs/sap/managepurchaseorderin?sap-vhost=my339666.sapbydesign.com";
            //    //UserName = "_FIH";
            //    //Password = "Welcome1";
            //    endpoint = "https://my340202.sapbydesign.com/sap/bc/srt/scs/sap/managepurchaseorderin?sap-vhost=my340202.sapbydesign.com";
            //    UserName = "_FIHPROD";
            //    Password = "Welcome1";
            //}
            //else if (Envirment.ToUpper() == "TEST")//測試環境
            //{
            //    endpoint = "https://my338886.sapbydesign.com/sap/bc/srt/scs/sap/managepurchaseorderin";
            //    UserName = "_FIH";
            //    Password = "Welcome1";
            //}
            //else if (Envirment.ToUpper() == "REAL")//正式環境
            //{
            //    endpoint = "https://my339319.sapbydesign.com/sap/bc/srt/scs/sap/managepurchaseorderin";
            //    UserName = "_FIHPROD";
            //    Password = "GYxZkga2x6Yv";
            //}
            #endregion
            // 讀取 request_message
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + "GetPurchaseOrderDetail.xml");

            //add id number
            for (int i = 0; i < poList.Length; i++)
            {
                XmlNode s = doc.CreateNode(XmlNodeType.Element, "ID", null);
                s.InnerText = poList[i];
                doc.SelectSingleNode("//PurchaseOrder").AppendChild(s);
            }

            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = doc.OuterXml;
            byte[] data = encoding.GetBytes(postData);

            // HTTP POST 請求物件
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "text/xml;charset=\"utf-8\"";
            httpWReq.ContentLength = data.Length;
            httpWReq.Headers.Add("SOAPAction", "http://tempuri.org/HelloWorld");

            System.Net.WebProxy webProxy = new System.Net.WebProxy("10.191.131.45", 3128);
            //webProxy.Credentials = new NetworkCredential("502112", "Mr123321..");
            httpWReq.Proxy = webProxy;
            httpWReq.Credentials = new System.Net.NetworkCredential(UserName, Password);
            //SOAPAction =  "http://tempuri.org/HelloWorld";

            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            XmlDocument doct = new XmlDocument();
            doct.LoadXml(responseString);
            doct.Save(AppDomain.CurrentDomain.BaseDirectory + @"POXmlRecord\" + Documentid + "PODetail.xml");

            DataSet dss = new DataSet();
            dss = ConvertTextTag(responseString);

            SaveData(DbWriteType, DbWrite, DBName, dss, responseString);
            return responseString;
        }

        public string ReadXml(string DbType,string Dbwrite)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + "724UAT.xml");

            //string rep="xmlns:n1=\"http://0050018995-one-off.sap.com/YTGGWAEPY_\"";
            XmlReader xr = XmlReader.Create(new System.IO.StringReader(doc.OuterXml));
            DataSet dss = new DataSet();
            //dss.ReadXml(xr);
            dss = ConvertTextTag(doc.InnerXml);
            SaveData(DbType, Dbwrite, "", dss, doc.InnerXml);
            return "";
        }

        public string SaveData(string DbType, string Dbwrite,string DBName, DataSet dss, string responseString)
        {
            //查找對應欄位
            #region
            string poID = "", OrderdDateTime = "", ThirdPartyDealIndicator = "", PartyID = "", UUID = "", CurrencyCode = "",
                QuantityTypeCode = "", ItemID = "", Quantity = "", ProductID = "", Description = "", NetUnitPrice = "", BaseQuantity = "",
                NetAmount = "", ProcessingTypeCode = "", EndDateTime = "", ProductPartyid = "", PO_Create_MT_UF5 = "", PO_Create_MT_UF6 = "", CreationDateTime = "",
                ShipToAccountNameOriginalSO = "", ShipToAccountStreetOriginalSO = "", ShipToAccountHouseNumberOriginalSO = "", ShipToAccountFloorOriginalSO = "",
                ShipToAccountCityOriginalSO = "", ShipToAccountCountryOriginalSO = "", ShipToAccountPostalCodeOriginalSO = "", CarrierDetail = "", PrePaymentBlock = "",
                CreditBlock = "", LaunchBlock = "", IncotermsOriginalSO = "", IncotermsCodeOriginalSO = "",IncotermsLocationOriginalSO = "", OriginalSOID = "", ExternalReferenceOriginalSO = "",
                PGIDateOriginalSO = "", HMDGlobalStreetOriginalSO = "", BillToAccountHouseNumberOriginalSO = "", BillToAccountRoomOriginalSO = "", HMDGlobalCityOriginalSO = "",
                HMDGlobalCountryOriginalSO = "", HMDGlobalPostalCodeOriginalSO = "", HMDGlobalNameOriginalSO = "", HMDGlobalIDOriginalSO = "", TypeCode = "",
                ContextText = "", TransferLocationName = "", ModeOfTransportOriginalSO = "", FullPaymentDueDaysValue = "", PurchaserCountryOriginalSO="",
                PurchaserIDOriginalSO = "", PurchaserNameOriginalSO = "", PurchaserFloorOriginalSO = "", PurchaserHouseNumberOriginalSO = "", PurchaserStreetOriginalSO="",
                PurchaserCityOriginalSO = "", PurchaserPostalCodeOriginalSO = "", ShiptoAccountAddressLine1 = "", ShiptoAccountAddressLine2 = "", ShiptoAccountAddressLine4="",
                ShiptoAccountAddressLine5 = "", PurchaserAddressLine1 = "", PurchaserAddressLine2 = "", PurchaserAddressLine4 = "", PurchaserAddressLine5="",
                ShiptoAccountCountryCodeOriginalSO="";
            string PurchaseOrder_Id="", Item_ID="";
            DataTable dtPurchaseOrder = dss.Tables["PurchaseOrder"];
            if (dtPurchaseOrder == null) return "";

            for (int i = 0; i < dtPurchaseOrder.Rows.Count; i++)
            {
                string documentid = DateTime.Now.ToString("yyyyMMddHHmmssff");
                //查找序列號
                string SelID = "select max(id) id from " + DBName + ".[dbo].[PO_CREATE_MT]";
                DataTable dtID = PDataBaseOperation.PSelectSQLDT(DbType, Dbwrite, SelID);
                if (dtID == null) return "";
                if (dtID.Rows.Count == 0) return "";
                string idStr = dtID.Rows[0]["id"].ToString();
                int id = int.Parse(idStr) + 1;
                //poID,PurchaseOrder_Id,UUID,CurrencyCode
                poID = dtPurchaseOrder.Rows[i]["ID"].ToString();
                //檢查PO是否存在，若存在，則不寫入
                string selPo = "select * from " + DBName + ".[dbo].[PO_CREATE_MT] where poid='" + poID + "'";
                DataTable dtPO = PDataBaseOperation.PSelectSQLDT(DbType, Dbwrite, selPo);
                if (dtPO != null)
                {
                    if (dtPO.Rows.Count > 0)
                    {
                        continue;
                    }
                }
                if (dtPurchaseOrder.Columns.Contains("PurchaseOrder_Id"))
                {
                    PurchaseOrder_Id = dtPurchaseOrder.Rows[i]["PurchaseOrder_Id"].ToString();
                }
                if (dtPurchaseOrder.Columns.Contains("UUID"))
                {
                    UUID = dtPurchaseOrder.Rows[i]["UUID"].ToString();
                }
                if (dtPurchaseOrder.Columns.Contains("CurrencyCode"))
                {
                    CurrencyCode = dtPurchaseOrder.Rows[i]["CurrencyCode"].ToString();
                }
                //string PurchaseOrderItem_ID = dtPurchaseOrder.Rows[i]["Item_ID"].ToString();
                if (dtPurchaseOrder.Columns.Contains("ProcessingTypeCode"))
                {
                    ProcessingTypeCode = dtPurchaseOrder.Rows[i]["ProcessingTypeCode"].ToString();
                }
                if (dtPurchaseOrder.Columns.Contains("HMDGlobalNameOriginalSO"))
                {
                    HMDGlobalNameOriginalSO = dtPurchaseOrder.Rows[i]["HMDGlobalNameOriginalSO"].ToString();
                }
                if (dtPurchaseOrder.Columns.Contains("HMDGlobalIDOriginalSO"))
                {
                    HMDGlobalIDOriginalSO = dtPurchaseOrder.Rows[i]["HMDGlobalIDOriginalSO"].ToString();
                }
                if(dtPurchaseOrder.Columns.Contains("HMDGlobalStreetOriginalSO"))
                {
                    HMDGlobalStreetOriginalSO = dtPurchaseOrder.Rows[i]["HMDGlobalStreetOriginalSO"].ToString();
                }
                if (dtPurchaseOrder.Columns.Contains("HMDGlobalCityOriginalSO"))
                {
                    HMDGlobalCityOriginalSO = dtPurchaseOrder.Rows[i]["HMDGlobalCityOriginalSO"].ToString();
                }
                if (dtPurchaseOrder.Columns.Contains("HMDGlobalCountryOriginalSO"))
                {
                    HMDGlobalCountryOriginalSO = dtPurchaseOrder.Rows[i]["HMDGlobalCountryOriginalSO"].ToString();
                }
                if (dtPurchaseOrder.Columns.Contains("HMDGlobalPostalCodeOriginalSO"))
                {
                    HMDGlobalPostalCodeOriginalSO = dtPurchaseOrder.Rows[i]["HMDGlobalPostalCodeOriginalSO"].ToString();
                }
                ///從CashDiscountTerms中查找FullPaymentDueDaysValue
                #region
                DataTable dtCashDiscountTerms = dss.Tables["CashDiscountTerms"];
                if (dtCashDiscountTerms != null)
                {
                    string CashDiscountTermsPO = "";
                    for (int p = 0; p < dtCashDiscountTerms.Rows.Count; p++)
                    {
                        if (dtCashDiscountTerms.Columns.Contains("PurchaseOrder_Id"))
                        {
                            CashDiscountTermsPO = dtCashDiscountTerms.Rows[p]["PurchaseOrder_Id"].ToString();
                        }
                        if (PurchaseOrder_Id == CashDiscountTermsPO)
                        {
                            if (dtCashDiscountTerms.Columns.Contains("FullPaymentDueDaysValue"))
                            {
                                FullPaymentDueDaysValue = dtCashDiscountTerms.Rows[p]["FullPaymentDueDaysValue"].ToString();
                            }
                        }
                    }
                }
                #endregion
                ///從OrderdDateTime中查找OrderdDateTime
                #region
                DataTable dtOrderdDateTime = dss.Tables["OrderedDateTime"];
                if (dtOrderdDateTime != null)
                {
                    for (int j = 0; j < dtOrderdDateTime.Rows.Count; j++)
                    {
                        string DetailpoID = dtOrderdDateTime.Rows[j]["PurchaseOrder_Id"].ToString();
                        if (PurchaseOrder_Id == DetailpoID)
                        {
                            //OrderdDateTime
                            OrderdDateTime = dtOrderdDateTime.Rows[j]["OrderedDateTime_Text"].ToString();
                        }
                    }
                }
                #endregion
                ///從SellerParty中查找PartyID
                #region
                DataTable dtSellerParty = dss.Tables["SellerParty"];
                if (dtSellerParty != null)
                {
                    for (int k = 0; k < dtSellerParty.Rows.Count; k++)
                    {
                        string SellerPartypoID = dtSellerParty.Rows[k]["PurchaseOrder_Id"].ToString();
                        if (PurchaseOrder_Id == SellerPartypoID)
                        {
                            string SellerParty_Id = "";
                            if (dtSellerParty.Columns.Contains("SellerParty_Id"))
                            {
                                SellerParty_Id = dtSellerParty.Rows[k]["SellerParty_Id"].ToString();
                            }
                            DataTable dtPartyKey = dss.Tables["PartyKey"];
                            if (dtPartyKey != null)
                            {
                                for (int s = 0; s < dtPartyKey.Rows.Count; s++)
                                {
                                    string DetailSellerParty_Id = dtPartyKey.Rows[s]["SellerParty_Id"].ToString();
                                    if (SellerParty_Id == DetailSellerParty_Id)
                                    {
                                        //PartyID
                                        PartyID = dtPartyKey.Rows[s]["PartyID"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                ///從DeliveryTerms中查找PO_Create_MT_UF5,PO_Create_MT_UF6
                #region
                DataTable dtDeliveryTerms = dss.Tables["DeliveryTerms"];
                if (dtDeliveryTerms != null)
                {
                    for (int j = 0; j < dtDeliveryTerms.Rows.Count; j++)
                    {
                        string DetailpoID = dtDeliveryTerms.Rows[j]["PurchaseOrder_Id"].ToString();
                        if (PurchaseOrder_Id == DetailpoID)
                        {
                            string DeliveryTerms_Id = "";
                            if (dtDeliveryTerms.Columns.Contains("DeliveryTerms_Id"))
                            {
                                DeliveryTerms_Id = dtDeliveryTerms.Rows[j]["DeliveryTerms_Id"].ToString();
                            }

                            DataTable dtIncoterms = dss.Tables["Incoterms"];
                            if (dtDeliveryTerms != null)
                            {
                                for (int k = 0; k < dtIncoterms.Rows.Count; k++)
                                {
                                    string IncotermsDeliveryTerms_Id = "";
                                    if (dtIncoterms.Columns.Contains("DeliveryTerms_Id"))
                                    {
                                        IncotermsDeliveryTerms_Id = dtIncoterms.Rows[k]["DeliveryTerms_Id"].ToString();
                                    }
                                    if (DeliveryTerms_Id == IncotermsDeliveryTerms_Id)
                                    {
                                        string ClassificationCode = "";
                                        if (dtIncoterms.Columns.Contains("ClassificationCode"))
                                        {
                                            ClassificationCode = dtIncoterms.Rows[k]["ClassificationCode"].ToString();
                                        }
                                        if (dtIncoterms.Columns.Contains("TransferLocationName"))
                                        {
                                            TransferLocationName = dtIncoterms.Rows[k]["TransferLocationName"].ToString();
                                        }
                                        //PO_Create_MT_UF5,PO_Create_MT_UF6
                                        PO_Create_MT_UF5 = ClassificationCode;
                                        PO_Create_MT_UF6 = TransferLocationName;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                ///從SystemAdministrativeData中查找CreationDateTime
                #region
                DataTable dtSystemAdministrativeData = dss.Tables["SystemAdministrativeData"];
                if (dtSystemAdministrativeData != null)
                {
                    for (int j = 0; j < dtSystemAdministrativeData.Rows.Count; j++)
                    {
                        string SystemAdministrativeDataPurchaseOrder_Id = dtSystemAdministrativeData.Rows[j]["PurchaseOrder_Id"].ToString();
                        if (PurchaseOrder_Id == SystemAdministrativeDataPurchaseOrder_Id)
                        {
                            //CreationDateTime
                            if (dtSystemAdministrativeData.Columns.Contains("CreationDateTime"))
                            {
                                CreationDateTime = dtSystemAdministrativeData.Rows[j]["CreationDateTime"].ToString();
                            }
                        }
                    }
                }
                #endregion
                /////從TextCollection中查找Text(TypeCode，ContextText)
                #region
                DataTable dtTextCollection = dss.Tables["TextCollection"];
                if (dtTextCollection != null)
                {
                    for (int k = 0; k < dtTextCollection.Rows.Count; k++)
                    {
                        string TextCollectionPurchaseOrder_Id = "", TextCollection_ID="";
                        if (dtTextCollection.Columns.Contains("PurchaseOrder_Id"))
                        {
                             TextCollectionPurchaseOrder_Id = dtTextCollection.Rows[k]["PurchaseOrder_Id"].ToString();
                        }
                        else
                        {
                            break;
                        }
                        //if (responseString.Contains("TextCollection"))
                        if (dtTextCollection.Columns.Contains("TextCollection_ID"))
                        {
                            TextCollection_ID = dtTextCollection.Rows[k]["TextCollection_ID"].ToString();
                        }
                        else
                        {
                            break;
                        }
                        if (PurchaseOrder_Id == TextCollectionPurchaseOrder_Id)
                        {
                            DataTable dtText1 = dss.Tables["Text1"];
                            if (dtText1 != null)
                            {
                                for (int s = 0; s < dtText1.Rows.Count; s++)
                                {
                                    string Text1TextCollection_ID = "", Text1_ID="";
                                    if (dtText1.Columns.Contains("TextCollection_ID"))
                                    {
                                        Text1TextCollection_ID = dtText1.Rows[s]["TextCollection_ID"].ToString();
                                    }
                                    if (dtText1.Columns.Contains("Text1_ID"))
                                    { 
                                        Text1_ID = dtText1.Rows[s]["Text1_ID"].ToString(); 
                                    }
                                    if (TextCollection_ID == Text1TextCollection_ID)
                                    {
                                        ///TypeCode
                                        if (dtText1.Columns.Contains("TypeCode"))
                                        {
                                            TypeCode = dtText1.Rows[s]["TypeCode"].ToString();
                                        }
                                        DataTable dtTextContent = dss.Tables["TextContent"];
                                        if (dtTextContent != null)
                                        {
                                            for (int t = 0; t < dtTextContent.Rows.Count; t++)
                                            {
                                                string TextContentText1_ID = "", TextContentContent_ID="";
                                                if (dtTextContent.Columns.Contains("Text1_ID"))
                                                {
                                                    TextContentText1_ID = dtTextContent.Rows[t]["Text1_ID"].ToString();
                                                }
                                                if (dtTextContent.Columns.Contains("TextContent_ID"))
                                                {
                                                    TextContentContent_ID = dtTextContent.Rows[t]["TextContent_ID"].ToString();
                                                }
                                                if (Text1_ID == TextContentText1_ID)
                                                {
                                                    DataTable dtText = dss.Tables["Text"];
                                                    if (dtText != null)
                                                    {
                                                        for (int u = 0; u < dtText.Rows.Count; u++)
                                                        {
                                                            if (dtText.Columns.Contains("TextContent_ID"))
                                                            {
                                                                string TextContent_ID = dtText.Rows[u]["TextContent_ID"].ToString();

                                                                if (TextContentContent_ID == TextContent_ID)
                                                                {
                                                                    ///ContextText
                                                                    if (dtText.Columns.Contains("Text_Text"))
                                                                    {
                                                                        ContextText = dtText.Rows[u]["Text_Text"].ToString();
                                                                    }
                                                                    //Insert into POText
                                                                    #region
                                                                    string insPOTextStr = "Insert into " + DBName + ".[dbo].[POText](id,poid,ItemID,typecode,ContextText,documentid) " +
                                                                         " VALUES('" + id + "','" + poID + "','H','" + TypeCode + "','" + ContextText.Replace("'", "''") + "','" + documentid + "')";
                                                                    int g = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insPOTextStr);
                                                                    #endregion
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                ///從Item中查找ThirdPartyDealIndicator,QuantityTypeCode
                #region
                DataTable dtItem = dss.Tables["Item"];
                int docSeq = 10001;
                if (dtItem != null)
                {
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        string ItempoID = "", Item_id="";
                        if (dtItem.Columns.Contains("PurchaseOrder_Id"))
                        {
                            ItempoID = dtItem.Rows[j]["PurchaseOrder_Id"].ToString();
                        }
                        else
                        {
                            break;
                        }

                        if (PurchaseOrder_Id == ItempoID)
                        {
                            if (dtItem.Columns.Contains("Item_id"))
                            {
                                Item_id = dtItem.Rows[j]["Item_id"].ToString();
                            }
                            else
                            {
                                break;
                            }
                            //////添加item判斷 20170324 Sally begin
                            //if(==Item_id)
                            //{
                            //取得出貨信息
                            #region
                            if (dtItem.Columns.Contains("ModeOfTransportOriginalSO"))
                            {
                                ModeOfTransportOriginalSO = dtItem.Rows[j]["ModeOfTransportOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShipToAccountNameOriginalSO"))
                            {
                                ShipToAccountNameOriginalSO = dtItem.Rows[j]["ShipToAccountNameOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShipToAccountStreetOriginalSO"))
                            {
                                ShipToAccountStreetOriginalSO = dtItem.Rows[j]["ShipToAccountStreetOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShipToAccountHouseNumberOriginalSO"))
                            {
                                ShipToAccountHouseNumberOriginalSO = dtItem.Rows[j]["ShipToAccountHouseNumberOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShipToAccountFloorOriginalSO"))
                            {
                                ShipToAccountFloorOriginalSO = dtItem.Rows[j]["ShipToAccountFloorOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShipToAccountCityOriginalSO"))
                            {
                                ShipToAccountCityOriginalSO = dtItem.Rows[j]["ShipToAccountCityOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShipToAccountCountryOriginalSO"))
                            {
                                ShipToAccountCountryOriginalSO = dtItem.Rows[j]["ShipToAccountCountryOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShipToAccountPostalCodeOriginalSO"))
                            {
                                ShipToAccountPostalCodeOriginalSO = dtItem.Rows[j]["ShipToAccountPostalCodeOriginalSO"].ToString();
                            }
                            //Sally 20170412  PO_Create_DT-FWCODE 改成PurchaseOrder/item/n1:n1:LogisticsServiceProviderName
                            //if (dtItem.Columns.Contains("CarrierDetail"))
                            //{
                            //    CarrierDetail = dtItem.Rows[j]["CarrierDetail"].ToString();
                            //}
                            if (dtItem.Columns.Contains("LogisticsServiceProviderName"))
                            {
                                CarrierDetail = dtItem.Rows[j]["LogisticsServiceProviderName"].ToString();
                            }
                            //Sally 20170425 新增POShipTo(Address_UF3\4\5\6)、POSoldTo(Address_UF3\4\5\6)欄位
                            if (dtItem.Columns.Contains("ShiptoAccountAddressLine1"))
                            {
                                ShiptoAccountAddressLine1 = dtItem.Rows[j]["ShiptoAccountAddressLine1"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShiptoAccountAddressLine2"))
                            {
                                ShiptoAccountAddressLine2 = dtItem.Rows[j]["ShiptoAccountAddressLine2"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShiptoAccountAddressLine4"))
                            {
                                ShiptoAccountAddressLine4 = dtItem.Rows[j]["ShiptoAccountAddressLine4"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShiptoAccountAddressLine5"))
                            {
                                ShiptoAccountAddressLine5 = dtItem.Rows[j]["ShiptoAccountAddressLine5"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserAddressLine1"))
                            {
                                PurchaserAddressLine1 = dtItem.Rows[j]["PurchaserAddressLine1"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserAddressLine2"))
                            {
                                PurchaserAddressLine2 = dtItem.Rows[j]["PurchaserAddressLine2"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserAddressLine4"))
                            {
                                PurchaserAddressLine4 = dtItem.Rows[j]["PurchaserAddressLine4"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserAddressLine5"))
                            {
                                PurchaserAddressLine5 = dtItem.Rows[j]["PurchaserAddressLine5"].ToString();
                            }
                            //Sally 20170327 v2.5 Spec沒有了Block信息
                            //if (dtItem.Columns.Contains("PrePaymentBlock"))
                            //{
                            //    PrePaymentBlock = dtItem.Rows[j]["PrePaymentBlock"].ToString();
                            //}
                            //if (dtItem.Columns.Contains("CreditBlock"))
                            //{
                            //    CreditBlock = dtItem.Rows[j]["CreditBlock"].ToString();
                            //}
                            //20170616
                            //if (dtItem.Columns.Contains("IncotermsOriginalSO"))
                            //{
                            //    IncotermsOriginalSO = dtItem.Rows[j]["IncotermsOriginalSO"].ToString();
                            //}

                            if (dtItem.Columns.Contains("IncotermsCodeOriginalSO"))
                            {
                                IncotermsCodeOriginalSO = dtItem.Rows[j]["IncotermsCodeOriginalSO"].ToString();
                            }
                            //if (dtItem.Columns.Contains("LaunchBlock"))
                            //{
                            //    LaunchBlock = dtItem.Rows[j]["LaunchBlock"].ToString();
                            //}
                            if (dtItem.Columns.Contains("IncotermsLocationOriginalSO"))
                            {
                                IncotermsLocationOriginalSO = dtItem.Rows[j]["IncotermsLocationOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("OriginalSOID"))
                            {
                                OriginalSOID = dtItem.Rows[j]["OriginalSOID"].ToString();
                            }
                            if (dtItem.Columns.Contains("ExternalReferenceOriginalSO"))
                            {
                                ExternalReferenceOriginalSO = dtItem.Rows[j]["ExternalReferenceOriginalSO"].ToString();
                            }
                            //if (dtItem.Columns.Contains("PGIDateOriginalSO"))
                            //{
                            //    PGIDateOriginalSO = dtItem.Rows[j]["PGIDateOriginalSO"].ToString();
                            //}
                            if (dtItem.Columns.Contains("PGIDate"))
                            {
                                PGIDateOriginalSO = dtItem.Rows[j]["PGIDate"].ToString();
                            }
                            if (dtItem.Columns.Contains("ShiptoAccountCountryCodeOriginalSO"))
                            {
                                ShiptoAccountCountryCodeOriginalSO = dtItem.Rows[j]["ShiptoAccountCountryCodeOriginalSO"].ToString();
                            }
                            //if (dtItem.Columns.Contains("BillToAccountStreetOriginalSO"))
                            //{
                            //    BillToAccountStreetOriginalSO = dtItem.Rows[j]["BillToAccountStreetOriginalSO"].ToString();
                            //}
                            //if (dtItem.Columns.Contains("BillToAccountHouseNumberOriginalSO"))
                            //{
                            //    BillToAccountHouseNumberOriginalSO = dtItem.Rows[j]["BillToAccountHouseNumberOriginalSO"].ToString();
                            //}
                            //if (dtItem.Columns.Contains("BillToAccountRoomOriginalSO"))
                            //{
                            //    BillToAccountRoomOriginalSO = dtItem.Rows[j]["BillToAccountRoomOriginalSO"].ToString();
                            //}
                            //if (dtItem.Columns.Contains("BillToAccountCityOriginalSO"))
                            //{
                            //    BillToAccountCityOriginalSO = dtItem.Rows[j]["BillToAccountCityOriginalSO"].ToString();
                            //}
                            //if (dtItem.Columns.Contains("BillToAccountCountryOriginalSO"))
                            //{
                            //    BillToAccountCountryOriginalSO = dtItem.Rows[j]["BillToAccountCountryOriginalSO"].ToString();
                            //}
                            //if (dtItem.Columns.Contains("BillToAccountPostalCodeOriginalSO"))
                            //{
                            //    BillToAccountPostalCodeOriginalSO = dtItem.Rows[j]["BillToAccountPostalCodeOriginalSO"].ToString();
                            //}
                            ///從Item中取得SoldToAddress
                            #region
                            if (dtItem.Columns.Contains("PurchaserCountryOriginalSO"))
                            {
                                PurchaserCountryOriginalSO = dtItem.Rows[j]["PurchaserCountryOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserIDOriginalSO"))
                            {
                                PurchaserIDOriginalSO = dtItem.Rows[j]["PurchaserIDOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserNameOriginalSO"))
                            {
                                PurchaserNameOriginalSO = dtItem.Rows[j]["PurchaserNameOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserFloorOriginalSO"))
                            {
                                PurchaserFloorOriginalSO = dtItem.Rows[j]["PurchaserFloorOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserHouseNumberOriginalSO"))
                            {
                                PurchaserHouseNumberOriginalSO = dtItem.Rows[j]["PurchaserHouseNumberOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserStreetOriginalSO"))
                            {
                                PurchaserStreetOriginalSO = dtItem.Rows[j]["PurchaserStreetOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserCityOriginalSO"))
                            {
                                PurchaserCityOriginalSO = dtItem.Rows[j]["PurchaserCityOriginalSO"].ToString();
                            }
                            if (dtItem.Columns.Contains("PurchaserPostalCodeOriginalSO"))
                            {
                                PurchaserPostalCodeOriginalSO = dtItem.Rows[j]["PurchaserPostalCodeOriginalSO"].ToString();
                            }
                            #endregion
                            ///從Item中查找ThirdPartyDealIndicator,QuantityTypeCode，ItemID
                            #region
                            if (dtItem.Columns.Contains("ThirdPartyDealIndicator"))
                            {
                                ThirdPartyDealIndicator = dtItem.Rows[j]["ThirdPartyDealIndicator"].ToString();
                            }
                            if (dtItem.Columns.Contains("QuantityTypeCode"))
                            {
                                QuantityTypeCode = dtItem.Rows[j]["QuantityTypeCode"].ToString();
                            }
                            if (dtItem.Columns.Contains("ItemID"))
                            {
                                ItemID = dtItem.Rows[j]["ItemID"].ToString();
                            }
                            if (dtItem.Columns.Contains("Item_id"))
                            {
                                Item_id = dtItem.Rows[j]["Item_id"].ToString();
                            }
                            #endregion
                            #endregion
                            ///從Quantity中查找Quantity_text（Quantity）
                            #region
                            DataTable dtQuantity = dss.Tables["Quantity"];
                            if (dtQuantity != null)
                            {
                                for (int k = 0; k < dtQuantity.Rows.Count; k++)
                                {
                                    string QuantityItem_id = dtQuantity.Rows[k]["Item_id"].ToString();
                                    if (Item_id == QuantityItem_id)
                                    {
                                        //Quantity
                                        if (dtQuantity.Columns.Contains("Quantity_text"))
                                        {
                                            Quantity = dtQuantity.Rows[k]["Quantity_text"].ToString();
                                        }
                                    }
                                }
                            }
                            #endregion
                            ///從Item的ItemProduct的ProductKey中查找ProductID
                            #region
                            DataTable dtItemProduct = dss.Tables["ItemProduct"];
                            if (dtItemProduct != null)
                            {
                                for (int k = 0; k < dtItemProduct.Rows.Count; k++)
                                {
                                    string ItemProductItem_ID = "", ItemProduct_id = "";
                                    if (dtItemProduct.Columns.Contains("Item_ID"))
                                    {
                                        ItemProductItem_ID = dtItemProduct.Rows[k]["Item_ID"].ToString();
                                    }
                                    if (dtItemProduct.Columns.Contains("ItemProduct_id"))
                                    {
                                        ItemProduct_id = dtItemProduct.Rows[k]["ItemProduct_id"].ToString();
                                    }
                                    if (Item_id == ItemProductItem_ID)
                                    {
                                        //string DItemProduct_id = dtItemProduct.Rows[k]["ItemProduct_id"].ToString();
                                        DataTable dtProductKey = dss.Tables["ProductKey"];
                                        if (dtProductKey != null)
                                        {
                                            for (int s = 0; s < dtProductKey.Rows.Count; s++)
                                            {
                                                string ProductKeyProduct_id = "";
                                                if (dtProductKey.Columns.Contains("ItemProduct_id"))
                                                {
                                                    ProductKeyProduct_id = dtProductKey.Rows[s]["ItemProduct_id"].ToString();
                                                }
                                                if (ItemProduct_id == ProductKeyProduct_id)
                                                {
                                                    if (dtProductKey.Columns.Contains("ProductID"))
                                                    {
                                                        ProductID = dtProductKey.Rows[s]["ProductID"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            ///從Item的Description中查找Description
                            #region
                            DataTable dtDescription = dss.Tables["Description"];
                            if (dtDescription != null)
                            {
                                for (int k = 0; k < dtDescription.Rows.Count; k++)
                                {
                                    string DescriptionItem_id = dtDescription.Rows[k]["Item_id"].ToString();
                                    if (Item_id == DescriptionItem_id)
                                    {
                                        //Description
                                        if (dtDescription.Columns.Contains("Description_Text"))
                                        {
                                            Description = dtDescription.Rows[k]["Description_Text"].ToString();
                                        }
                                    }
                                }
                            }
                            #endregion
                            ///從NetUnitPrice中查找NetUnitPrice(Amount_Text)
                            #region
                            DataTable dtNetUnitPrice = dss.Tables["NetUnitPrice"];
                            if (dtNetUnitPrice != null)
                            {
                                for (int k = 0; k < dtNetUnitPrice.Rows.Count; k++)
                                {
                                    string NetUnitItem_id = dtNetUnitPrice.Rows[k]["Item_id"].ToString();
                                    if (Item_id == NetUnitItem_id)
                                    {
                                        string NetUnitPrice_id = "";
                                        if (dtNetUnitPrice.Columns.Contains("NetUnitPrice_id"))
                                        {
                                            NetUnitPrice_id = dtNetUnitPrice.Rows[k]["NetUnitPrice_id"].ToString();
                                        }
                                        ///從Amount中查找NetUnitPrice
                                        #region
                                        DataTable dtAmount = dss.Tables["Amount"];
                                        if (dtAmount != null)
                                        {
                                            for (int s = 0; s < dtAmount.Rows.Count; s++)
                                            {
                                                string AmountNetUnitPrice_id = "";
                                                if (dtAmount.Columns.Contains("NetUnitPrice_id"))
                                                {
                                                    AmountNetUnitPrice_id = dtAmount.Rows[s]["NetUnitPrice_id"].ToString();
                                                }
                                                if (NetUnitPrice_id == AmountNetUnitPrice_id)
                                                {
                                                    //NetUnitPrice
                                                    if (dtAmount.Columns.Contains("Amount_Text"))
                                                    {
                                                        NetUnitPrice = dtAmount.Rows[s]["Amount_Text"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        //Sally 20170327 v2.5 Spec沒有了Block信息
                                        ///從BaseQuantity中查找BaseQuantity(BaseQuantity_Text)
                                        #region
                                        //DataTable dtBaseQuantity = dss.Tables["BaseQuantity"];
                                        //if (dtBaseQuantity != null)
                                        //{
                                        //    for (int s = 0; s < dtBaseQuantity.Rows.Count; s++)
                                        //    {
                                        //        string BaseQuantityNetUnitPrice_id = "";
                                        //        if (dtBaseQuantity.Columns.Contains("NetUnitPrice_id"))
                                        //        {
                                        //            BaseQuantityNetUnitPrice_id = dtBaseQuantity.Rows[s]["NetUnitPrice_id"].ToString();
                                        //        }
                                        //        if (NetUnitPrice_id == BaseQuantityNetUnitPrice_id)
                                        //        {
                                        //            //BaseQuantity
                                        //            if (dtBaseQuantity.Columns.Contains("BaseQuantity_Text"))
                                        //            {
                                        //                BaseQuantity = dtBaseQuantity.Rows[s]["BaseQuantity_Text"].ToString();
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                            ///從NetAmount中查找NetAmount（NetAmount_Text）
                            #region
                            DataTable dtNetAmount = dss.Tables["NetAmount"];
                            if (dtNetAmount != null)
                            {
                                for (int k = 0; k < dtNetAmount.Rows.Count; k++)
                                {
                                    string NetAmountItem_Id = dtNetAmount.Rows[k]["Item_Id"].ToString();
                                    if (Item_id == NetAmountItem_Id)
                                    {
                                        //NetAmount
                                        if (dtNetAmount.Columns.Contains("NetAmount_Text"))
                                        {
                                            NetAmount = dtNetAmount.Rows[k]["NetAmount_Text"].ToString();
                                        }
                                    }
                                }
                            }
                            #endregion
                            ///從DeliveryPeriod中查找EndDateTime（EndDateTime_Text）
                            #region
                            DataTable dtDeliveryPeriod = dss.Tables["DeliveryPeriod"];
                            if (dtDeliveryPeriod != null)
                            {
                                for (int k = 0; k < dtDeliveryPeriod.Rows.Count; k++)
                                {
                                    string DeliveryPeriodItem_ID = dtDeliveryPeriod.Rows[k]["Item_ID"].ToString();
                                    if (Item_id == DeliveryPeriodItem_ID)
                                    {
                                        string DeliveryPeriod_id = "";
                                        if (dtDeliveryPeriod.Columns.Contains("DeliveryPeriod_id"))
                                        {
                                            DeliveryPeriod_id = dtDeliveryPeriod.Rows[k]["DeliveryPeriod_id"].ToString();
                                        }
                                        DataTable dtEndDateTime = dss.Tables["EndDateTime"];
                                        if (dtEndDateTime != null)
                                        {
                                            for (int s = 0; s < dtEndDateTime.Rows.Count; s++)
                                            {
                                                string EndDateTimePeriodItem_ID = dtEndDateTime.Rows[k]["DeliveryPeriod_id"].ToString();
                                                if (DeliveryPeriod_id == EndDateTimePeriodItem_ID)
                                                {
                                                    if (dtEndDateTime.Columns.Contains("EndDateTime_Text"))
                                                    {
                                                        EndDateTime = dtEndDateTime.Rows[k]["EndDateTime_Text"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            ///從ProductRecipientParty中查找ProductPartyid
                            #region
                            DataTable dtProductRecipientParty = dss.Tables["ProductRecipientParty"];
                            if (dtProductRecipientParty != null)
                            {
                                for (int k = 0; k < dtProductRecipientParty.Rows.Count; k++)
                                {
                                    string ProductRecipientPartyItem_id = dtProductRecipientParty.Rows[k]["Item_id"].ToString();
                                    if (Item_id == ProductRecipientPartyItem_id)
                                    {
                                        string ProductRecipientParty_id = dtProductRecipientParty.Rows[k]["ProductRecipientParty_ID"].ToString();
                                        DataTable dtPartyKey = dss.Tables["PARTYKEY"];
                                        if (dtPartyKey != null)
                                        {
                                            for (int s = 0; s < dtPartyKey.Rows.Count; s++)
                                            {
                                                string PartyKeyProductRecipientParty_ID = "";
                                                if (dtPartyKey.Columns.Contains("ProductRecipientParty_ID"))
                                                {
                                                    PartyKeyProductRecipientParty_ID = dtPartyKey.Rows[s]["ProductRecipientParty_ID"].ToString();
                                                }
                                                if (ProductRecipientParty_id == PartyKeyProductRecipientParty_ID)
                                                {
                                                    //ProductPartyid
                                                    if (dtPartyKey.Columns.Contains("Partyid"))
                                                    {
                                                        ProductPartyid = dtPartyKey.Rows[s]["Partyid"].ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            ///從ItemTextCollection中查找Text(TypeCode，ContextText)
                            #region
                            DataTable dtItemTextCollection = dss.Tables["ItemTextCollection"];
                            if (dtItemTextCollection != null)
                            {
                                for (int k = 0; k < dtItemTextCollection.Rows.Count; k++)
                                {
                                    string ItemTextCollectionItem_ID = "", ItemTextCollection_ID = "";
                                    if (dtItemTextCollection.Columns.Contains("Item_ID"))
                                    {
                                        ItemTextCollectionItem_ID = dtItemTextCollection.Rows[k]["Item_ID"].ToString();
                                    }
                                    if (dtItemTextCollection.Columns.Contains("ItemTextCollection_ID"))
                                    {
                                        ItemTextCollection_ID = dtItemTextCollection.Rows[k]["ItemTextCollection_ID"].ToString();
                                    }
                                    if (Item_id == ItemTextCollectionItem_ID)
                                    {
                                        DataTable dtText1 = dss.Tables["Text1"];
                                        if (dtText1 != null)
                                        {
                                            for (int s = 0; s < dtText1.Rows.Count; s++)
                                            {
                                                string Text1ItemTextCollection_ID = dtText1.Rows[s]["ItemTextCollection_ID"].ToString();
                                                string Text1_ID = dtText1.Rows[s]["Text1_ID"].ToString();
                                                if (ItemTextCollection_ID == Text1ItemTextCollection_ID)
                                                {
                                                    ///TypeCode
                                                    TypeCode = dtText1.Rows[s]["TypeCode"].ToString();
                                                    DataTable dtTextContent = dss.Tables["TextContent"];
                                                    if (dtTextContent != null)
                                                    {
                                                        for (int t = 0; t < dtTextContent.Rows.Count; t++)
                                                        {
                                                            string TextContentText1_ID = "", TextContentContent_ID = "";
                                                            if (dtTextContent.Columns.Contains("Text1_ID"))
                                                            {
                                                                TextContentText1_ID = dtTextContent.Rows[t]["Text1_ID"].ToString();
                                                            }
                                                            if (dtTextContent.Columns.Contains("TextContent_ID"))
                                                            {
                                                                TextContentContent_ID = dtTextContent.Rows[t]["TextContent_ID"].ToString();
                                                            }
                                                            if (Text1_ID == TextContentText1_ID)
                                                            {
                                                                DataTable dtText = dss.Tables["Text"];
                                                                if (dtText != null)
                                                                {
                                                                    for (int u = 0; u < dtText.Rows.Count; u++)
                                                                    {
                                                                        if (dtText.Columns.Contains("TextContent_ID"))
                                                                        {
                                                                            string TextContent_ID = dtText.Rows[u]["TextContent_ID"].ToString();

                                                                            if (TextContentContent_ID == TextContent_ID)
                                                                            {
                                                                                ///ContextText
                                                                                if (dtText.Columns.Contains("Text_Text"))
                                                                                {
                                                                                    ContextText = dtText.Rows[u]["Text_Text"].ToString();
                                                                                }
                                                                                //Insert into POText
                                                                                #region
                                                                                string insPOTextStr = "Insert into " + DBName + ".[dbo].[POText](id,poid,ItemID,typecode,ContextText,documentid) " +
                                                                                     " VALUES('" + id + "','" + poID + "','" + ItemID + "','" + TypeCode + "','" + ContextText.Replace("'","''") + "','" + documentid + "')";
                                                                                int g = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insPOTextStr);
                                                                                #endregion
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            //Save Data to DB（PO_CREATE_DT，POShipToAddress，POSoldToAddress）
                            //Insert into PO_CREATE_DT
                            #region
                //            string insStr = "INSERT INTO " + DBName + ".[dbo].[PO_CREATE_DT](id,PO_Create_DT_UF2,Unit,ItemID,BaseQty,InternalID,Description, " +
                //" CostCurrencyCode,CostAmount,PRICEQTY,Amount,PO_Create_DT_UF1,poid,INVOICEVERIFYINDICATOR,FWCODE,PrePaymentBlock,CreditBlock," +
                //" LaunchBlock,Orincoterms,Orincoterms2,OriginalSOID,DeliveryStartDT,f4,documentid,OriginID,ShipmentMode) " +
                //" VALUES('" + id + "','" + PartyID + "','" + QuantityTypeCode + "','" + ItemID + "','" + Quantity + "','" + ProductID +
                //            "','" + Description + "','" + CurrencyCode + "','" + NetUnitPrice + "','" + BaseQuantity + "','" + NetAmount + "','" + EndDateTime +
                //            "','" + poID + "','','" + CarrierDetail + "','" + PrePaymentBlock + "','" + CreditBlock + "','" + LaunchBlock + "','" +
                //            IncotermsOriginalSO + "','" + IncotermsLocationOriginalSO + "','" + OriginalSOID + "','" + PGIDateOriginalSO + "','" +
                //            documentid + "','" + documentid + "','" + ExternalReferenceOriginalSO + "','" + ModeOfTransportOriginalSO + "')";
                            //Sally Xue 20170505 解決特殊字符問題
                //            string insStr = "INSERT INTO " + DBName + ".[dbo].[PO_CREATE_DT](id,PO_Create_DT_UF2,Unit,ItemID,BaseQty,InternalID,Description, " +
                //" CostCurrencyCode,CostAmount,PRICEQTY,Amount,PO_Create_DT_UF1,poid,INVOICEVERIFYINDICATOR,FWCODE,PrePaymentBlock,CreditBlock," +
                //" LaunchBlock,Orincoterms,Orincoterms2,OriginalSOID,DeliveryStartDT,f4,documentid,OriginID,ShipmentMode) " +
                //" VALUES('" + id + "','" + PartyID + "','" + QuantityTypeCode + "','" + ItemID + "','" + Quantity + "','" + ProductID +
                //            "','" + Description+ "','" + CurrencyCode + "','" + NetUnitPrice + "','" + BaseQuantity + "','" + NetAmount + "','" + EndDateTime +
                //            "','" + poID + "','','" + CarrierDetail + "','" + PrePaymentBlock + "','" + CreditBlock + "','" + LaunchBlock + "','" +
                //            IncotermsOriginalSO + "','" + IncotermsLocationOriginalSO + "','" + OriginalSOID + "','" + PGIDateOriginalSO.Replace("'", "''") + "','" +
                //            documentid + "','" + documentid + "','" + ExternalReferenceOriginalSO.Replace("'", "''") + "','" + ModeOfTransportOriginalSO.Replace("'", "''") + "')";

               //             string insStr = "INSERT INTO " + DBName + ".[dbo].[PO_CREATE_DT](id,PO_Create_DT_UF2,Unit,ItemID,BaseQty,InternalID,Description, " +
               //" CostCurrencyCode,CostAmount,PRICEQTY,Amount,PO_Create_DT_UF1,poid,INVOICEVERIFYINDICATOR,FWCODE,PrePaymentBlock,CreditBlock," +
               //" LaunchBlock,Orincoterms,Orincoterms2,OriginalSOID,DeliveryStartDT,f4,documentid,OriginID,ShipmentMode) " +
               //" VALUES('" + id + "','" + PartyID + "','" + QuantityTypeCode.Replace("'", "''") + "','" + ItemID + "','" + Quantity.Replace("'", "''") + "','" + ProductID.Replace("'", "''") +
               //            "','" + Description.Replace("'", "''") + "','" + CurrencyCode.Replace("'", "''") + "','" + NetUnitPrice.Replace("'", "''") + "','" + BaseQuantity.Replace("'", "''") + "','" + NetAmount.Replace("'", "''") + "','" + EndDateTime +
               //            "','" + poID + "','','" + CarrierDetail.Replace("'", "''") + "','" + PrePaymentBlock.Replace("'", "''") + "','" + CreditBlock.Replace("'", "''") + "','" + LaunchBlock.Replace("'", "''") + "','" +
               //            IncotermsOriginalSO.Replace("'", "''") + "','" + IncotermsLocationOriginalSO.Replace("'", "''") + "','" + OriginalSOID.Replace("'", "''") + "','" + PGIDateOriginalSO.Replace("'", "''") + "','" +
               //            documentid + "','" + documentid + "','" + ExternalReferenceOriginalSO.Replace("'", "''") + "','" + ModeOfTransportOriginalSO.Replace("'", "''") + "')";

                            string insStr = "INSERT INTO " + DBName + ".[dbo].[PO_CREATE_DT](id,PO_Create_DT_UF2,Unit,ItemID,BaseQty,InternalID,Description, " +
               " CostCurrencyCode,CostAmount,PRICEQTY,Amount,PO_Create_DT_UF1,poid,INVOICEVERIFYINDICATOR,FWCODE,PrePaymentBlock,CreditBlock," +
               " LaunchBlock,Orincoterms,Orincoterms2,OriginalSOID,DeliveryStartDT,f4,documentid,OriginID,ShipmentMode) " +
               " VALUES('" + id + "','" + PartyID + "','" + QuantityTypeCode.Replace("'", "''") + "','" + ItemID + "','" + Quantity.Replace("'", "''") + "','" + ProductID.Replace("'", "''") +
                           "','" + Description.Replace("'", "''") + "','" + CurrencyCode.Replace("'", "''") + "','" + NetUnitPrice.Replace("'", "''") + "','" + BaseQuantity.Replace("'", "''") + "','" + NetAmount.Replace("'", "''") + "','" + EndDateTime +
                           "','" + poID + "','','" + CarrierDetail.Replace("'", "''") + "','" + PrePaymentBlock.Replace("'", "''") + "','" + CreditBlock.Replace("'", "''") + "','" + LaunchBlock.Replace("'", "''") + "','" +
                           IncotermsCodeOriginalSO.Replace("'", "''") + "','" + IncotermsLocationOriginalSO.Replace("'", "''") + "','" + OriginalSOID.Replace("'", "''") + "','" + PGIDateOriginalSO.Replace("'", "''") + "','" +
                           documentid + "','" + documentid + "','" + ExternalReferenceOriginalSO.Replace("'", "''") + "','" + ModeOfTransportOriginalSO.Replace("'", "''") + "')";
                            
                            int m = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insStr);

                            //Insert into POParseHead
                            string insPOParseHead = "Insert into " + DBName + ".[dbo].[POParseHead]([DocumentID],[DocSeq],[POID],[ItemID],[Flag1],[Flag2],[Flag3],[Flag4]," +
                            " flag5,flag6,flag7,flag8,flag9,flag10) VALUES('" + documentid + "','" + docSeq + "','" + poID + "','" + ItemID + "','1','1','1','1'," +
                            " '1','1','1','1','1','1') ";
                            int poParse = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insPOParseHead);
                            docSeq = docSeq + 1;
                            #endregion
                            //Insert into POShipToAddress
                            #region
                            //Sally Xue 20170512 shipto新增欄位Address_UF7、RegionCode
                            #region
                            string f13 = "";
                            string selCou = "select f13 from dbo.FGISM1001 where F1='NokFoxM2001' and F10 ='" + ShiptoAccountCountryCodeOriginalSO + "'";
                            DataTable dtCou = PDataBaseOperation.PSelectSQLDT(DbType, Dbwrite, selCou);
                            if (dtCou != null)
                            {
                                if (dtCou.Rows.Count > 0)
                                {
                                    f13 = dtCou.Rows[0]["f13"].ToString();
                                }
                            }
                            string insShipStr = "INSERT INTO " + DBName + ".[dbo].[POShipToAddress](id,itemid,GivenName,StreetName,CareOfName,CityName," +
                            " CountryCode,PostalCode,NumberDefaultIndicator,documentid,poid,Address_UF2,Address_UF3,Address_UF4,Address_UF5,Address_UF6,"+
                            " Address_UF7,RegionCode) VALUES('" + id + "','" + ItemID + "','" +
                            ShipToAccountStreetOriginalSO.Replace("'", "''") + "','" + ShipToAccountHouseNumberOriginalSO.Replace("'", "''") + "','" + ShipToAccountFloorOriginalSO.Replace("'", "''") +
                            "','" + ShipToAccountCityOriginalSO.Replace("'", "''") + "','" + ShipToAccountCountryOriginalSO.Replace("'", "''") + "','" + ShipToAccountPostalCodeOriginalSO.Replace("'", "''") +
                            "','" + ProductPartyid.Replace("'", "''") + "','" + documentid + "','" + poID + "','" + ShipToAccountNameOriginalSO.Replace("'", "''") + "','" + ShiptoAccountAddressLine1.Replace("'", "''") +
                            "','" + ShiptoAccountAddressLine2.Replace("'", "''") + "','" + ShiptoAccountAddressLine4.Replace("'", "''") + "','" + ShiptoAccountAddressLine5.Replace("'", "''") +
                            "','" + ShiptoAccountCountryCodeOriginalSO.Replace("'", "''") + "','" + f13.Replace("'", "''") + "')";

                            #endregion
                            //Sally Xue 20170505 解決特殊字符問題
                            //string insShipStr = "INSERT INTO " + DBName + ".[dbo].[POShipToAddress](id,itemid,GivenName,StreetName,CareOfName,CityName," +
                            //" CountryCode,PostalCode,NumberDefaultIndicator,documentid,poid,Address_UF2,Address_UF3,Address_UF4,Address_UF5,Address_UF6)" +
                            //" VALUES('" + id + "','" + ItemID + "','" +
                            //ShipToAccountStreetOriginalSO + "','" + ShipToAccountHouseNumberOriginalSO + "','" + ShipToAccountFloorOriginalSO +
                            //"','" + ShipToAccountCityOriginalSO + "','" + ShipToAccountCountryOriginalSO + "','" + ShipToAccountPostalCodeOriginalSO +
                            //"','" + ProductPartyid + "','" + documentid + "','" + poID + "','" + ShipToAccountNameOriginalSO.Replace("'", "''") + "','" + ShiptoAccountAddressLine1.Replace("'", "''") +
                            //"','" + ShiptoAccountAddressLine2.Replace("'", "''") + "','" + ShiptoAccountAddressLine4.Replace("'", "''") + "','" + ShiptoAccountAddressLine5.Replace("'", "''") + "')";
                            //string insShipStr = "INSERT INTO " + DBName + ".[dbo].[POShipToAddress](id,itemid,GivenName,StreetName,CareOfName,CityName," +
                            //" CountryCode,PostalCode,NumberDefaultIndicator,documentid,poid,Address_UF2,Address_UF3,Address_UF4,Address_UF5,Address_UF6)" +
                            //" VALUES('" + id + "','" + ItemID + "','" +
                            //ShipToAccountStreetOriginalSO.Replace("'", "''") + "','" + ShipToAccountHouseNumberOriginalSO.Replace("'", "''") + "','" + ShipToAccountFloorOriginalSO.Replace("'", "''") +
                            //"','" + ShipToAccountCityOriginalSO.Replace("'", "''") + "','" + ShipToAccountCountryOriginalSO.Replace("'", "''") + "','" + ShipToAccountPostalCodeOriginalSO.Replace("'", "''") +
                            //"','" + ProductPartyid.Replace("'", "''") + "','" + documentid + "','" + poID + "','" + ShipToAccountNameOriginalSO.Replace("'", "''") + "','" + ShiptoAccountAddressLine1.Replace("'", "''") +
                            //"','" + ShiptoAccountAddressLine2.Replace("'", "''") + "','" + ShiptoAccountAddressLine4.Replace("'", "''") + "','" + ShiptoAccountAddressLine5.Replace("'", "''") + "')";
                            int h = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insShipStr);
                            #endregion
                            //Insert into POSoldToAddress
                            #region
                            //Sally Xue 20170505 解決特殊字符問題
                    //        string insSoldStr = "INSERT INTO " + DBName + ".[dbo].[POSoldToAddress](id,itemid,GivenName,CountryCode,PostalCode,CityName," +
                    //" NumberDefaultIndicator,Address_UF2,documentID,poid,CareOfName,StreetName,Address_UF3,Address_UF4,Address_UF5,Address_UF6)VALUES('" +
                    //id + "','" + ItemID + "','" + PurchaserStreetOriginalSO +
                    //"','" + PurchaserCountryOriginalSO + "','" + PurchaserPostalCodeOriginalSO + "','" + PurchaserCityOriginalSO + "','" +
                    //PurchaserIDOriginalSO + "','" + PurchaserNameOriginalSO + "','" + documentid + "','" + poID + "','" + PurchaserFloorOriginalSO +
                    //"','" + PurchaserHouseNumberOriginalSO + "','" + PurchaserAddressLine1 + "','" + PurchaserAddressLine2 + "','" + PurchaserAddressLine4 +
                    //"','" + PurchaserAddressLine5 + "')";
                            string insSoldStr = "INSERT INTO " + DBName + ".[dbo].[POSoldToAddress](id,itemid,GivenName,CountryCode,PostalCode,CityName," +
                    " NumberDefaultIndicator,Address_UF2,documentID,poid,CareOfName,StreetName,Address_UF3,Address_UF4,Address_UF5,Address_UF6)VALUES('" +
                    id + "','" + ItemID + "','" + PurchaserStreetOriginalSO.Replace("'", "''") +
                    "','" + PurchaserCountryOriginalSO.Replace("'", "''") + "','" + PurchaserPostalCodeOriginalSO.Replace("'", "''") + "','" + PurchaserCityOriginalSO.Replace("'", "''") + "','" +
                    PurchaserIDOriginalSO.Replace("'", "''") + "','" + PurchaserNameOriginalSO.Replace("'", "''") + "','" + documentid + "','" + poID + "','" + PurchaserFloorOriginalSO.Replace("'", "''") +
                    "','" + PurchaserHouseNumberOriginalSO.Replace("'", "''") + "','" + PurchaserAddressLine1.Replace("'", "''") + "','" + PurchaserAddressLine2.Replace("'", "''") + "','" + PurchaserAddressLine4.Replace("'", "''") +
                    "','" + PurchaserAddressLine5.Replace("'", "''") + "')";
                            int ItemSoldTo = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insSoldStr);
                            #endregion
                            //}
                        }
                    }
                }
                #endregion

                //Save Data to DB（PO_CREATE_MT）
                #region
                //Sally Xue 20170505 解決特殊字符問題
//                string insMtStr = "INSERT INTO " + DBName + ".[dbo].[PO_CREATE_MT](id,PO_Create_MT_UF3,TransmissionIndicator,PO_Create_MT_UF5,PO_Create_MT_UF6,BuyerPostDT," +
//" poid,pocnt,PO_Create_MT_UF7,documentid,SellerPartyID,PO_Create_MT_UF4) VALUES('" + id + "','" + OrderdDateTime + "','" +
//ThirdPartyDealIndicator + "','" + PO_Create_MT_UF5 + "','" + PO_Create_MT_UF6 + "','" + CreationDateTime + "','" + poID + "','','" +
//ShipToAccountNameOriginalSO + "','" + documentid + "','" + PartyID + "','" + FullPaymentDueDaysValue + "')";
                string insMtStr = "INSERT INTO " + DBName + ".[dbo].[PO_CREATE_MT](id,PO_Create_MT_UF3,TransmissionIndicator,PO_Create_MT_UF5,PO_Create_MT_UF6,BuyerPostDT," +
" poid,pocnt,PO_Create_MT_UF7,documentid,SellerPartyID,PO_Create_MT_UF4) VALUES('" + id + "','" + OrderdDateTime + "','" +
ThirdPartyDealIndicator.Replace("'", "''") + "','" + PO_Create_MT_UF5.Replace("'", "''") + "','" + PO_Create_MT_UF6.Replace("'", "''") + "','" + CreationDateTime + "','" + poID + "','','" +
ShipToAccountNameOriginalSO.Replace("'", "''") + "','" + documentid + "','" + PartyID.Replace("'", "''") + "','" + FullPaymentDueDaysValue.Replace("'", "''") + "')";
                int q = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insMtStr);
                #endregion

                //Sally 20170408 屏蔽此段代碼，SoldToAddress數據源變更為Item中信息
                ////Insert into POSoldToAddress
                #region
                ////20170325 ExternalReferenceOriginalSO欄位由原來的保存在poSoldtoAddress(Address_UF1)中變更為保存在po_Create_DT中
                ////string insSoldStr = "INSERT INTO " + DBName + ".[dbo].[POSoldToAddress](id,itemid,GivenName,CountryCode,PostalCode,CityName,StreetName," +
                ////    " CareOfName,NumberDefaultIndicator,Address_UF1,Address_UF2,documentID,poid)VALUES('" + id + "','" + ItemID + "','" + BillToAccountStreetOriginalSO +
                ////    "','" + BillToAccountCountryOriginalSO + "','" + BillToAccountPostalCodeOriginalSO + "','" + BillToAccountCityOriginalSO + "','" +
                ////    BillToAccountHouseNumberOriginalSO + "','" + BillToAccountRoomOriginalSO + "','" + HMDGlobalIDOriginalSO + "','" +
                ////    ExternalReferenceOriginalSO + "','" + HMDGlobalNameOriginalSO + "','" + documentid + "','" + poID + "')";
                ////int p = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insSoldStr);
                //string insSoldStr = "INSERT INTO " + DBName + ".[dbo].[POSoldToAddress](id,itemid,GivenName,CountryCode,PostalCode,CityName," +
                //    " NumberDefaultIndicator,Address_UF2,documentID,poid)VALUES('" + id + "','H','" + HMDGlobalStreetOriginalSO +
                //    "','" + HMDGlobalCountryOriginalSO + "','" + HMDGlobalPostalCodeOriginalSO + "','" + HMDGlobalCityOriginalSO + "','" +
                //    HMDGlobalIDOriginalSO + "','" + HMDGlobalNameOriginalSO + "','" + documentid + "','" + poID + "')";
                //int H = PDataBaseOperation.PExecSQL(DbType, Dbwrite, insSoldStr);
                #endregion
            }
            #endregion
            return "";
        }

        private DataSet ConvertTextTag(string xmlstring) 
        {
            //string firstPart = xmlstring.Substring(0, xmlstring.IndexOf("<ItemTextCollection>"));
            //string lastPart = xmlstring.Substring(xmlstring.LastIndexOf("<ItemTaxCalculation>"));
            ////string firstPart = xmlstring.Substring(0, xmlstring.IndexOf("<TextCollection>"));
            ////string lastPart = xmlstring.Substring(xmlstring.LastIndexOf("<SellerParty>"));


            //string modifyPart = xmlstring.Substring(xmlstring.IndexOf("<ItemTextCollection>"),
            //xmlstring.LastIndexOf("<ItemTaxCalculation>") - xmlstring.IndexOf("<ItemTextCollection>"));
            ////string modifyPart = xmlstring.Substring(xmlstring.IndexOf("<TextCollection>"),
            ////    xmlstring.LastIndexOf("<SellerParty>") - xmlstring.IndexOf("<TextCollection>"));

            //string[] text = Regex.Split(modifyPart, "<Text>");

            //StringBuilder sb = new StringBuilder();
            //sb.Append(text[0]);

            //for (int i = 1; i < text.Length; i++)
            //{
            //    string[] r = Regex.Split(text[i], "</Text>");
            //    sb.Append("<Text1>");
            //    sb.Append(r[0]);
            //    sb.Append("</Text>");
            //    sb.Append(r[1]);
            //    sb.Append("</Text1>");
            //    sb.Append(r[2]);
            //}

            //StringBuilder fullPart = new StringBuilder();
            //fullPart.Append(firstPart);
            //fullPart.Append(sb);
            //fullPart.Append(lastPart);

            //StringReader xmlSR = new StringReader(fullPart.ToString());

            //DataSet ds = new DataSet();
            //ds.ReadXml(xmlSR);

            //return ds;

            string[] total = Regex.Split(xmlstring, "<Text>");

            StringBuilder xmlSB = new StringBuilder();
            xmlSB.Append(total[0]);
            for (int i = 1; i < total.Length; i++)
            {
                string[] r = Regex.Split(total[i], "</Text>");
                xmlSB.Append("<Text1>");
                xmlSB.Append(r[0]);
                xmlSB.Append("</Text>");
                xmlSB.Append(r[1]);
                xmlSB.Append("</Text1>");
                xmlSB.Append(r[2]);
            }

            StringReader xmlSR = new StringReader(xmlSB.ToString());

            DataSet ds = new DataSet();
            ds.ReadXml(xmlSR);

            return ds;
        }

        public string FeedBackACK(string DbReadType, string DbRead, string DbWriteType, string DbWrite, string DBName, string starttime, string endtime, string PO, string Envirment, string FactoryCode)
        {
            string selPo = "";
            DataTable dtPO = null;
            if (PO == null || PO == "")
            {
                selPo = "select distinct POID from " + DBName + ".dbo.PO_CREATE_DT where f6 in('1','3') and PO_CREATE_DT_UF2='" + FactoryCode + "' ";
            }
            else
            {
                selPo = "select distinct POID from " + DBName + ".dbo.PO_CREATE_DT where f6 in('1','3') and poid='" + PO + "' and PO_CREATE_DT_UF2='" + FactoryCode + "' ";
            }
            dtPO = PDataBaseOperation.PSelectSQLDT(DbWriteType, DbWrite, selPo);
            if (dtPO == null) return "";

            String endpoint = "", UserName = "", Password = "";
            string sql = "select * from FGIS.DATABOM1001 where upper(ITEM)='POACK' and upper(flag8)=upper('" + Envirment + "')";
            DataTable dt = PDataBaseOperation.PSelectSQLDT(DbReadType, DbRead, sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    endpoint = dt.Rows[0]["flag13"].ToString();
                    UserName = dt.Rows[0]["flag2"].ToString();
                    Password = dt.Rows[0]["flag5"].ToString();
                }
            }
            #region
            //if (Envirment.ToUpper() == "UAT")
            //{
            //    //endpoint = "https://my339666.sapbydesign.com/sap/bc/srt/scs/sap/yy3gd6gvsy_po_ack?sap-vhost=my339666.sapbydesign.com";
            //    //UserName = "_100055";
            //    //Password = "Jute7hER";
            //    endpoint = "https://my340202.sapbydesign.com/sap/bc/srt/scs/sap/yy3gd6gvsy_po_ack?sap-vhost=my340202.sapbydesign.com";
            //    UserName = "_545645";
            //    Password = "Welcome1";
            //}
            //else if (Envirment.ToUpper() == "TEST")//測試環境
            //{
            //    endpoint = "https://my338886.sapbydesign.com/sap/bc/srt/scs/sap/yy3gd6gvsy_po_ack?sap-vhost=my338886.sapbydesign.com";
            //    UserName = "_1000163";
            //    Password = "Jute7hER";
            //}
            //else if (Envirment.ToUpper() == "REAL")//正式環境
            //{
            //    endpoint = "https://my339319.sapbydesign.com/sap/bc/srt/scs/sap/yy3gd6gvsy_po_ack";
            //    UserName = "_545645";
            //    Password = "Jute7hER";
            //}
            #endregion

            XmlDocument doc = new XmlDocument();            
            for (int i = 0; i < dtPO.Rows.Count; i++)
            {
                string poID = dtPO.Rows[i]["POID"].ToString();

                if (!PoCheck(DbWriteType, DbWrite, DBName, poID))
                {
                    //數據校驗失敗，則更新PO_CREATE_DT中F6狀態為4
                    string updPOFail = "update  " + DBName + ".dbo.PO_CREATE_DT set f6='4' where poid='" + poID + "' and PO_CREATE_DT_UF2='" + FactoryCode + "'";
                    int k = PDataBaseOperation.PExecSQL(DbWriteType, DbWrite, updPOFail);

                    //數據校驗失敗，則更新poparsehead中F6狀態為4
                    string updpoParseFail = "update  " + DBName + ".dbo.poparsehead set flag1='4' where poid='" + poID + "'";
                    int d = PDataBaseOperation.PExecSQL(DbWriteType, DbWrite, updpoParseFail);
                    break;
                }
                //add id number
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + "PO_Ack_WebServiceReq.xml");
                XmlNode s = doc.CreateNode(XmlNodeType.Element, "ID", null);
                s.InnerText = poID;
                doc.SelectSingleNode("//PurchaseOrder").AppendChild(s);

                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = doc.OuterXml;
                byte[] data = encoding.GetBytes(postData);

                // HTTP POST 請求物件
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "text/xml;charset=\"utf-8\""; 
                //httpWReq.ContentType = "text/xml;charset=\"en_US.UTF-8\"";
                httpWReq.ContentLength = data.Length;
                httpWReq.Headers.Add("SOAPAction", "http://tempuri.org/HelloWorld");

                System.Net.WebProxy webProxy = new System.Net.WebProxy("10.191.131.45", 3128);
                //webProxy.Credentials = new NetworkCredential("F6100538", "zxy20110715");
                httpWReq.Proxy = webProxy;
                ////httpWReq.Credentials = new System.Net.NetworkCredential("_1000163", "Welcome1");
                httpWReq.Credentials = new System.Net.NetworkCredential(UserName, Password);
                //SOAPAction =  "http://tempuri.org/HelloWorld";

                httpWReq.Timeout = 1800000;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                try
                {
                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    XmlDocument doct = new XmlDocument();
                    doct.LoadXml(responseString);
                    doct.Save(AppDomain.CurrentDomain.BaseDirectory + @"POXmlRecord\" + poID + "POACK.xml");

                    //回覆ACK正常，則更新DT表F6狀態為2
                    string updPO = "update  " + DBName + ".dbo.PO_CREATE_DT set f6='2' where poid='" + poID + "' and PO_CREATE_DT_UF2='" + FactoryCode + "'";
                    int j = PDataBaseOperation.PExecSQL(DbWriteType, DbWrite, updPO);
                    //回覆ACK正常，則更新poparsehead表Flag狀態為2
                    string updpoParse = "update " + DBName + ".dbo.poparsehead set flag1='2' where poid='" + poID + "'";
                    int n = PDataBaseOperation.PExecSQL(DbWriteType, DbWrite, updpoParse);
                }
                catch (Exception ex)
                {
                    //回覆ACK異常，則更新F6狀態為3
                    string updPO = "update  " + DBName + ".dbo.PO_CREATE_DT set f6='3' where poid='" + poID + "' and PO_CREATE_DT_UF2='" + FactoryCode + "'";
                    int j = PDataBaseOperation.PExecSQL(DbWriteType, DbWrite, updPO);

                    //回覆ACK異常，則更新F6狀態為3
                    string updpoParse = "update  " + DBName + ".dbo.poparsehead set flag1='3' where poid='" + poID + "'";
                    int n = PDataBaseOperation.PExecSQL(DbWriteType, DbWrite, updpoParse);
                }
            }
            return "";
        }

        public bool PoCheck(string DbType, string DbRead, string DBName, string PO)
        {
            bool ret = true;
            string sel = "";
            if (PO == null || PO == "")
            {
                sel = "select * from " + DBName + ".dbo.poparsehead where flag1='1' ";
            }
            else
            {
                sel = "select * from " + DBName + ".dbo.poparsehead where POID='" + PO + "' ";
            }
            DataTable dt = PDataBaseOperation.PSelectSQLDT(DbType, DbRead, sel);
            if (dt == null) return false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string PoID = dt.Rows[i]["poid"].ToString();
                string ItemID = dt.Rows[i]["ItemID"].ToString();
                //檢查在PO_Create_MT表中是否存在
                string selMt = "select * from " + DBName + ".dbo.PO_CREATE_MT where poid='" + PoID + "'";
                DataTable dtMt = PDataBaseOperation.PSelectSQLDT(DbType, DbRead, selMt);
                if (dtMt == null) return ret = false;
                if (dtMt.Rows.Count > 0)
                {
                    ret = true;
                }
                else
                {
                    return ret = false;
                }
                //檢查PO_Create_DT表中是否存在
                string selDt = "select * from " + DBName + ".dbo.PO_CREATE_DT where poid='" + PoID + "' AND ITEMID='" + ItemID + "'";
                DataTable dtDt = PDataBaseOperation.PSelectSQLDT(DbType, DbRead, selDt);
                if (dtDt == null) return ret = false;
                if (dtDt.Rows.Count > 0)
                {
                    ret = true;
                }
                else
                {
                    return ret = false;
                }
                //檢查POShipToAddress表中是否存在
                string selPOShipToAddress = "select * from " + DBName + ".dbo.POShipToAddress where poid='" + PoID + "' AND ITEMID='" + ItemID + "'";
                DataTable dtPOShipToAddress = PDataBaseOperation.PSelectSQLDT(DbType, DbRead, selPOShipToAddress);
                if (dtPOShipToAddress == null) return ret = false;
                if (dtPOShipToAddress.Rows.Count > 0)
                {
                    ret = true;
                }
                else
                {
                    return ret = false;
                }
                //檢查POSoldToAddress表中是否存在
                string selPOSoldToAddress = "select * from " + DBName + ".dbo.POSoldToAddress where poid='" + PoID + "' AND ITEMID='" + ItemID + "'";
                DataTable dtPOSoldToAddress = PDataBaseOperation.PSelectSQLDT(DbType, DbRead, selPOSoldToAddress);
                if (dtPOSoldToAddress == null) return ret = false;
                if (dtPOSoldToAddress.Rows.Count > 0)
                {
                    ret = true;
                }
                else
                {
                    return ret = false;
                }
            }
            return ret;
         }

        public static DataSet GetDataSetByXmlpath(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                //读取XML到DataSet 
                StreamReader sr = new StreamReader(strXmlPath, Encoding.Default);
                ds.ReadXml(sr);
                sr.Close();
                if (ds.Tables.Count > 0)
                    return ds;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
        
        private DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        //測試傳ASN使用
        public string UploadXML(string Envirment)
        {
            //string endpoint = "https://my339666.sapbydesign.com/sap/bc/srt/scs/sap/inbounddeliveryprocessingdeliv?sap-vhost=my339666.sapbydesign.com";

            String endpoint = "", UserName = "", Password = "";
            if (Envirment == "UAT")
            {
                endpoint = "https://my339666.sapbydesign.com/sap/bc/srt/scs/sap/inbounddeliveryprocessingdeliv?sap-vhost=my339666.sapbydesign.com";
                UserName = "_100055";
                Password = "Jute7hER";
            }
            else//測試環境
            {
                endpoint = "https://my338886.sapbydesign.com/sap/bc/srt/scs/sap/yy3gd6gvsy_po_ack?sap-vhost=my338886.sapbydesign.com";
                UserName = "_1000163";
                Password = "Welcome1";
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"xmlRecord\" + "555.xml");

            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = doc.OuterXml;
            byte[] data = encoding.GetBytes(postData);

            // HTTP POST 請求物件
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);
            httpWReq.Method = "POST";
            //httpWReq.ContentType = "text/xml;charset=\"utf-8\"";
            //httpWReq.ContentType = "text/xml;charset=\"en_US.UTF-8\"";
            httpWReq.ContentLength = data.Length;
            httpWReq.Headers.Add("SOAPAction", "http://tempuri.org/HelloWorld");
            httpWReq.ContentType = "application/soap+xml;charset=\"utf-8\""; 


            System.Net.WebProxy webProxy = new System.Net.WebProxy("10.191.131.45", 3128);
            //webProxy.Credentials = new NetworkCredential("F6100538", "zxy20110715");
            httpWReq.Proxy = webProxy;
            httpWReq.Credentials = new System.Net.NetworkCredential(UserName, Password);
            //SOAPAction =  "http://tempuri.org/HelloWorld";

            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        public string GenXML(string DbType, string Dbwrite)
        {
            string sql = "select * from fgis.uploadtmp where f1='ECN' and flag1='2'";
            DataSet ds = PDataBaseOperation.PSelectSQLDS(DbType, Dbwrite, sql);
            if (ds == null) return "";
            string ret = ConvertDataTableToXML(ds);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ret);
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + @"xmlRecord\" + "uploadTmp.xml");

            return ret;
        }


        public string GenXML2(string DbType, string DbRead)
        {
            string sql = "select * from PO_CREATE_MT where poid='522'";
            DataSet ds = PDataBaseOperation.PSelectSQLDS(DbType, DbRead, sql);
            if (ds == null) return "";
            string ret = ConvertDataTableToXML(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string item = "";
                string sql1 = "select * from PO_CREATE_DT where poid='522'";
                DataTable dt1 = PDataBaseOperation.PSelectSQLDT(DbType, DbRead, sql1);
                if (dt1 == null) return "";
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    item = dt1.Rows[j]["ItemID"].ToString();

                }
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ret);
            doc.Save(AppDomain.CurrentDomain.BaseDirectory + @"xmlRecord\" + "uploadTmp.xml");

            return ret;
        }

        private string ConvertDataTableToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim();
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public string SendData(string ReadDBType, string ReadDB, string ReadTable, string WriteDBType, string WriteDB, string WriteTable, string RDate)
        {
            string ret = "";
            string sel = "select * from "+ReadTable+" where flag1='2' and rdate='"+RDate+"' AND FLAG2='1'";
            string UpdDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssff");
            DataTable dt = PDataBaseOperation.PSelectSQLDT(ReadDBType, ReadDB, sel);
            if (dt == null) return "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string seqID = dt.Rows[i]["SEQID"].ToString();
                string documentID = dt.Rows[i]["documentID"].ToString();
                string DDATE = dt.Rows[i]["DDATE"].ToString();
                string DOCSEQ = dt.Rows[i]["DOCSEQ"].ToString();
                string F1 = dt.Rows[i]["F1"].ToString();
                string F2 = dt.Rows[i]["F2"].ToString();
                string F3 = dt.Rows[i]["F3"].ToString();
                string F4 = dt.Rows[i]["F4"].ToString();
                string F5 = dt.Rows[i]["F5"].ToString();
                string F6 = dt.Rows[i]["F6"].ToString();
                string F7 = dt.Rows[i]["F7"].ToString();
                string F8 = dt.Rows[i]["F8"].ToString();
                string F9 = dt.Rows[i]["F9"].ToString();
                string F10 = dt.Rows[i]["F10"].ToString();
                string F11 = dt.Rows[i]["F11"].ToString();
                string F12 = dt.Rows[i]["F12"].ToString();
                string F13 = dt.Rows[i]["F13"].ToString();
                string F14 = dt.Rows[i]["F14"].ToString();
                string F15 = dt.Rows[i]["F15"].ToString();
                string F16 = dt.Rows[i]["F16"].ToString();
                string F17 = dt.Rows[i]["F17"].ToString();
                string F18 = dt.Rows[i]["F18"].ToString();
                string F19 = dt.Rows[i]["F19"].ToString();
                string F20 = dt.Rows[i]["F20"].ToString();
                string F21 = dt.Rows[i]["F21"].ToString();
                string F22 = dt.Rows[i]["F22"].ToString();
                string F23 = dt.Rows[i]["F23"].ToString();
                string F24 = dt.Rows[i]["F24"].ToString();
                string F25 = dt.Rows[i]["F25"].ToString();
                string F26 = dt.Rows[i]["F26"].ToString();
                string F27 = dt.Rows[i]["F27"].ToString();
                string F28 = dt.Rows[i]["F28"].ToString();
                string F29 = dt.Rows[i]["F29"].ToString();
                string F30 = dt.Rows[i]["F30"].ToString();
                string F31 = dt.Rows[i]["F31"].ToString();
                string F32 = dt.Rows[i]["F32"].ToString();
                string F33 = dt.Rows[i]["F33"].ToString();
                string F34 = dt.Rows[i]["F34"].ToString();
                string F35 = dt.Rows[i]["F35"].ToString();
                string F36 = dt.Rows[i]["F36"].ToString();
                string F37 = dt.Rows[i]["F37"].ToString();
                string F38 = dt.Rows[i]["F38"].ToString();
                string F39 = dt.Rows[i]["F39"].ToString();
                string F40 = dt.Rows[i]["F40"].ToString();
                string F41 = dt.Rows[i]["F41"].ToString();
                string F42 = dt.Rows[i]["F42"].ToString();
                string F43 = dt.Rows[i]["F43"].ToString();
                string F44 = dt.Rows[i]["F44"].ToString();
                string F45 = dt.Rows[i]["F45"].ToString();
                string F46 = dt.Rows[i]["F46"].ToString();
                string F47 = dt.Rows[i]["F47"].ToString();
                string F48 = dt.Rows[i]["F48"].ToString();
                string F49 = dt.Rows[i]["F49"].ToString();
                string F50 = dt.Rows[i]["F50"].ToString();
                string FLAG1 = dt.Rows[i]["FLAG1"].ToString();
                string FLAG2 = dt.Rows[i]["FLAG2"].ToString();
                string FLAG3 = dt.Rows[i]["FLAG3"].ToString();
                string FLAG4 = dt.Rows[i]["FLAG4"].ToString();
                string FLAG5 = dt.Rows[i]["FLAG5"].ToString();
                string FLAG6 = dt.Rows[i]["FLAG6"].ToString();
                string FLAG7 = dt.Rows[i]["FLAG7"].ToString();
                string FLAG8 = dt.Rows[i]["FLAG8"].ToString();
                string FLAG9 = dt.Rows[i]["FLAG9"].ToString();
                string USERS = dt.Rows[i]["USERS"].ToString();
                string UPDATETIME = dt.Rows[i]["UPDATETIME"].ToString();
                string ins = "insert into "+WriteTable+" (DOCUMENTID,DDATE,DOCSEQ,F1,F2,F3,F4,F5,F6,F7,F8,F9, "+
" F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29," +
" F30,F31,F32,F33,F34,F35,F36,F37,F38,F39,F40,F41,F42,F43,F44,F45,F46,F47,F48,F49," +
" F50,FLAG1,FLAG2,FLAG3,FLAG4,FLAG5,FLAG6,FLAG7,FLAG8,FLAG9,RDATE,USERS,UPDATETIME,SEQID) "+
" VALUES('"+documentID+"','"+DDATE+"','"+DOCSEQ+"','"+F1+"','"+F2+"','"+F3+"','"+F4+"','"+F5+"','"+F6+"','"+F7+
"','"+F8+"','"+F9+"','"+F10+"','"+F11+"','"+F12+"','"+F13+"','"+F14+"','"+F15+"','"+F16+"','"+F17+"','"+F18+
"','"+F19+"','"+F20+"','"+F21+"','"+F22+"','"+F23+"','"+F24+"','"+F25+"','"+F26+"','"+F27+"','"+F28+"','"+F29+
"','"+F30+"','"+F31+"','"+F32+"','"+F33+"','"+F34+"','"+F35+"','"+F36+"','"+F37+"','"+F38+"','"+F39+"','"+F40+
"','" + F41 + "','" + F42 + "','" + F43 + "','" + F44 + "','" + F45 + "','" + F46 + "','" + F47 + "','" + F48 +
"','" + F49 + "','" + F50 + "','" + FLAG1 + "','" + UpdDocumentID + "','" + FLAG3 + "','" + FLAG4 + "','" + FLAG5 +
"','" + FLAG6 + "','" + FLAG7 +"','"+FLAG8+"','"+FLAG9+"','"+USERS+"','"+UPDATETIME+"','"+UPDATETIME+"','"+seqID+"')";
                int j = PDataBaseOperation.PExecSQL(WriteDBType, WriteDB, ins);
                if (j > 0)
                {
                    string upd = "update " + ReadTable + " set flag2='" + UpdDocumentID + "' where seqid='" + seqID + "'";
                    int s = PDataBaseOperation.PExecSQL(ReadDBType, ReadDB, upd);
                }
            }
            return ret;
        }

        public void CreateXmlFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点
            XmlNode root = xmlDoc.CreateElement("Users");
            xmlDoc.AppendChild(root);

            XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "User", null);
            CreateNode(xmlDoc, node1, "name", "xuwei");
            CreateNode(xmlDoc, node1, "sex", "male");
            CreateNode(xmlDoc, node1, "age", "25");
            root.AppendChild(node1);

            XmlNode node2 = xmlDoc.CreateNode(XmlNodeType.Element, "User", null);
            CreateNode(xmlDoc, node2, "name", "xiaolai");
            CreateNode(xmlDoc, node2, "sex", "female");
            CreateNode(xmlDoc, node2, "age", "23");
            root.AppendChild(node2);

            try
            {
                xmlDoc.Save("c://data5.xml");
            }
            catch (Exception e)
            {
                //显示错误信息
                Console.WriteLine(e.Message);
            }
            //Console.ReadLine();
        }

        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
    
    }
}