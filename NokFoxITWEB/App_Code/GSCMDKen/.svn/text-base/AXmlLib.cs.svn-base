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
using SCM.GSCMDKen;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using System.Xml;
using System.Drawing;
using System.Drawing.Text;



namespace SCM.GSCMDKen
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class AXmlLib
{
    protected DateTime Pubtmptoday = DateTime.Today;
    protected string PubCurrtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string PubCuurDate = DateTime.Today.ToString("yyyyMMdd"); 
    public static string PubDefaultConnString = "Data Source=10.83.16.96;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public string PubRunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    FSplitlib FSplitlibPointer = new FSplitlib();
    // FPubLib FPubLibPointer = new FPubLib();
    FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();    
                           
 
   //////////////////////////////////////////////
   // MessLog  for all Record 
   //  t1 = FSplitlibPointer.Wri_MessLog("OrgDVQueryOK", v1.ToString(), QueryArr[3, 77], "", "", "", "", "", "", "");  // 10 Code
   public string WriXml_MessLog( string F2, string F3, string F4, string F5, string F6, string F7, string F8, string F9, string F10, string DPath)
   {
       string ErrMsg="", LF0="", LF1="", LF2="", LF3="", LF4="", LF5="", LF6="", LF7="", LF8="", LF9="", LF10="";
 
       string RPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
       if ((DPath != "") && (DPath != null)) RPath = DPath;
    
       // protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
           
       if ((F2 == "") || (F2 == null)) LF2 = "Normal";
       else LF2 = F2;

       if ((F3 == "") || (F3 == null)) LF3 = "";
       else LF3 = F3;

       if ((F4 == "") || (F4 == null)) LF4 = "";
       else LF4 = F4;

       if ((F5 == "") || (F5 == null)) LF5 = "";
       else LF5 = F5;

       if ((F6 == "") || (F6 == null)) LF6 = "";
       else LF6 = F6;

       if ((F7 == "") || (F7 == null)) LF7 = "";
       else LF7 = F7;

       if ((F8 == "") || (F8 == null)) LF8 = "";
       else LF8 = F8;

       if ((F9 == "") || (F9 == null)) LF9 = "";
       else LF9 = F9;

       if ((F10 == "") || (F10 == null)) LF10 = "";
       else LF10 = F10;

       LF0 = LibSCM1Pointer.CNGet_TicketPath(DateTime.Today.ToString("yyyyMMdd"), LF2, "1", RPath, 1); // get Ticket
       LF1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");

       string sql1 = "Insert into MessLog ( F0, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10 ) "
       + " Values ( '" + LF0 + "', '" + LF1 + "', '" + LF2 + "', '" + LF3 + "', '" + LF4 + "', '" + LF5 + "', '" + LF6 + "' , "
       + " '" + LF7 + "', '" + LF8 + "', '" + LF9 + "', '" + LF10 + "' ) ";

       if (!LibSCM1Pointer.DBExcuteByDataPath( sql1, RPath))
            ErrMsg = "ExeceptionLog 新增失敗，請稍后重試！";

       return ("1");
   }  // end MessLog

   //  sv1 = AXmlLibPointer.GetXML( "Asus", "ZASUSPO870", ordersp_00238215.xml, DateTime.Now.ToString("yyyyMMdd");
   public string GetXMLToTmp(string tCustNo, string tXmlType, string XmlFilePath1, string XmlFilePath2, string tDate, string DPath)
   {
       int v1 = 0, v2 = 0, v3 = 0, XRay = 100000, XmlLong = 0, SLoc = 16;
       string sv1 = "", sv2 = "", sv3 = "", sw1 = "N", tTicket = "", Sql1 = "", tMessg = "";
       string tPONUMBER = "", tPOITEM = "", tDocumentID = "", tDocumentTime = "", RunDBPath="";
       string tItem="";

       if (DPath == "") RunDBPath = PubRunDBPath;
       else             RunDBPath = DPath;

       string[,] tmpTot     = new string[2, 60 + 1];
        for (v1 = 0; v1 < 2; v1++)
           for (v2 = 0; v2 < 60 + 1; v2++)
               tmpTot[v1, v2] = "";

       string[,] tmpAsusXml = new string[XRay + 1, 5 + 1];
       for (v1 = 0; v1 < XRay; v1++)
           for (v2 = 0; v2 < 5 + 1; v2++)
               tmpAsusXml[v1, v2] = "";
                   
       v1 = 0;
       // XmlTextReader objXMLReader = new XmlTextReader(Server.MapPath("ordersp_00238215.xml"));
       XmlTextReader objXMLReader = new XmlTextReader( XmlFilePath1 );
       string strNodeResult = "";
       XmlNodeType objNodeType;
       while (objXMLReader.Read())
       {
           objNodeType = objXMLReader.NodeType;
           switch (objNodeType)
           {
               case XmlNodeType.XmlDeclaration:

                   strNodeResult += "XML Declaration:<b>" + objXMLReader.Name + "" + objXMLReader.Value + "</b><br/>";
                   sv1 = objXMLReader.Name;
                   break;
               case XmlNodeType.Element:
                   strNodeResult += "Element:<b>" + objXMLReader.Name + "</b><br/>";
                   sv2 = objXMLReader.Name;
                   break;
               case XmlNodeType.Text:
                   strNodeResult += "&nbsp;-Value:<b>" + objXMLReader.Value + "</b><br/>";
                   sv3 = objXMLReader.Value;
                   sw1 = "Y";
                   break;
           }

           // if (sv1 == "IDOC") v1++;

           if ((sw1 == "Y") || (sv2 == "ZASUSPO870"))
           {
               v1++;
               tmpAsusXml[v1, 1] = (100 + v1).ToString();
               tmpAsusXml[v1, 2] = sv1.ToString();
               tmpAsusXml[v1, 3] = sv2.ToString();
               tmpAsusXml[v1, 4] = sv3.ToString();
               sw1 = "N";
               sv1 = "";
               sv2 = "";
               sv3 = "";
           }

           if (objXMLReader.AttributeCount > 0)
           {

               while (objXMLReader.MoveToNextAttribute())
               {
                   strNodeResult += "&nbsp;-Attribute:<b>" + objXMLReader.Name + "</b>&nbsp;value:<b>" + objXMLReader.Value + "</b><br/>";
               }
           }
       }
       // Response.Write(strNodeResult);

       v2 = 1;
       while ((tmpAsusXml[v2, 1] != "") && (tmpAsusXml[v2, 1] != null))
       {
              tItem = tmpAsusXml[v2, 3].ToString();
              switch (tItem)
                   {
                       case "PONUMBER":  // 16
                            tmpTot[1, SLoc + 1] = tmpAsusXml[v2, 3].ToString();
                            tmpTot[1, SLoc + 2] = tmpAsusXml[v2, 4].ToString();
                            tPONUMBER = tmpAsusXml[v2, 4].ToString();
                            break;
                       case "POITEM":    
                            tmpTot[1, SLoc + 3] = tmpAsusXml[v2, 3].ToString();
                            tmpTot[1, SLoc + 4] = tmpAsusXml[v2, 4].ToString();
                            tPOITEM = tmpAsusXml[v2, 4];
                            break;
                       case "WOSTATS":    
                            tmpTot[ 1, SLoc + 5] = tmpAsusXml[v2, 3].ToString();
                            tmpTot[ 1, SLoc + 6] = tmpAsusXml[v2, 4].ToString();
                            break;
                       case "DATETIME":
                            tmpTot[ 1, SLoc + 7] = tmpAsusXml[v2, 3].ToString();
                            tmpTot[ 1, SLoc + 8] = tmpAsusXml[v2, 4].ToString();
                            break;
                       case "REFDOCNUM":
                            tmpTot[ 1, SLoc + 9] = tmpAsusXml[v2, 3].ToString();
                            tmpTot[ 1, SLoc + 10] = tmpAsusXml[v2, 4].ToString();
                            break;
                       case "ORDERQTY":
                            tmpTot[ 1, SLoc + 11] = tmpAsusXml[v2, 3].ToString();
                            tmpTot[ 1, SLoc + 12] = tmpAsusXml[v2, 4].ToString();
                            break;
                       case "QTYUNIT":
                            tmpTot[ 1, SLoc + 13] = tmpAsusXml[v2, 3].ToString();
                            tmpTot[ 1, SLoc + 14] = tmpAsusXml[v2, 4].ToString();
                            break;                       
                   }  // end case

              v2++;
       }

       tTicket = LibSCM1Pointer.CNGet_TicketPath(DateTime.Today.ToString("yyyyMMdd"), "AsusXml", "1", RunDBPath, 1); // get Ticket

       // Find Document_NO, PO_NO
       //v2 = 1;
       //while ((tmpAsusXml[v2, 1] != "") && (tmpAsusXml[v2, 1] != null))
       //{
       //    if (tmpAsusXml[v2, 3] == "PONUMBER") tPONUMBER = tmpAsusXml[v2, 4];
       //    if (tmpAsusXml[v2, 3] == "POITEM") tPOITEM = tmpAsusXml[v2, 4];
       //    v2++;
       //}

       XmlLong = v2 - 1;

       if (tPONUMBER == "") tDocumentID = tTicket;
       else tDocumentID = tPONUMBER + DateTime.Now.ToString("yyyyMMddHHmmss");

       tDocumentTime = DateTime.Now.ToString("yyyyMMddHHmmssss");

       v2 = 1;
       v3 = 0;

       while ((tmpAsusXml[v2, 1] != "") && (tmpAsusXml[v2, 1] != null))
       {
           Sql1 = "Insert into  APXMLT ( DocumentID, Ticket, CStatus, SeqNo, Item1, Item2, Item3 ) Values "
           + " ( '" + tDocumentID + "', '" + tTicket + "', '', '" + tmpAsusXml[v2, 1] + "', '" + tmpAsusXml[v2, 2] + "',  "
           + " '" + tmpAsusXml[v2, 3] + "', '" + tmpAsusXml[v2, 4] + "' ) ";
           if (!LibSCM1Pointer.DBExcuteByDataPath(Sql1, RunDBPath))
               tMessg = WriXml_MessLog("ErrZASUSPO870", tmpAsusXml[v2, 1], tTicket, tmpAsusXml[v2, 2], tmpAsusXml[v2, 3], tmpAsusXml[v2, 4], "", "", "", RunDBPath);
           else
               v3++; // Succ Count

           v2++; // Next Record

       }

       if (v3 <= 0)  // Fail 
       {
           tMessg = WriXml_MessLog( "ErrZASUSPO870", "", tTicket, "", "", "", "", "", "", RunDBPath);
           return ( "-1");
       }

       Sql1 = "Insert into  APXMLM ( CustNo, XmlType, DocumentID, DocumentTime, Ticket, DataCnt, Item1, Item2, Item3, CStatus, Semaphone1, "
       + " F1, F2, F3, F4, F5, F6, F7, F8, F9, F10,F11,F12,F13, F14 ) Values "
       + " ( 'Asus', 'ASUSPO870', '" + tDocumentID + "', '" + tDocumentTime + "', '" + tTicket + "', '" + XmlLong.ToString() + "', "
       + " '" + tPONUMBER + "', '" + tPOITEM + "', '', 'R', '',  '" + tmpTot[1, SLoc + 1] + "', '" + tmpTot[1, SLoc + 2] + "',   "
       + " '" + tmpTot[1, SLoc + 3] + "', '" + tmpTot[1, SLoc + 4] + "', '" + tmpTot[1, SLoc + 5] + "', '" + tmpTot[1, SLoc + 6] + "', "
       + " '" + tmpTot[1, SLoc + 7] + "', '" + tmpTot[1, SLoc + 8] + "', '" + tmpTot[1, SLoc + 9] + "', '" + tmpTot[1, SLoc + 10] + "', "
       + " '" + tmpTot[1, SLoc + 11] + "', '" + tmpTot[1, SLoc + 12] + "', '" + tmpTot[1, SLoc + 13] + "', '" + tmpTot[1, SLoc + 14] + "') ";
       if (!LibSCM1Pointer.DBExcuteByDataPath(Sql1, RunDBPath))
       {
           tMessg = WriXml_MessLog("ErrZASUSPO870", XmlLong.ToString(), tTicket, tmpAsusXml[v2, 2], tmpAsusXml[v2, 3], tmpAsusXml[v2, 4], "", "", "", RunDBPath);
           return ( "-1");
       }  
     

       objXMLReader.Close();
       
       // string fileRoot = Server.MapPath("") + "\\Upload\\" + dep + "\\" + fileAliasName;

       if (!File.Exists(XmlFilePath2)) File.Move(XmlFilePath1, XmlFilePath2);
       else File.Delete(XmlFilePath1); 
       
       //if ( (File.Exists(XmlFilePath1)) && ( !File.Exists(XmlFilePath2)) )
       //      File.Copy(XmlFilePath1, XmlFilePath2);
       //if (File.Exists(XmlFilePath1))
       //    File.Delete(XmlFilePath1);
             
       return ( XmlLong.ToString());
   }

   public string XMLTmpToDBF(string tCustNo, string tXmlType, string XmlFilePath1, string tDate, string DPath)
   {
       int v1 = 0, v2 = 0, v3 = 0, v4=0, v5=0, XRay = 100000, XmlLong = 0;
       int SLoc = 16; 
       string sv1 = "", sv2 = "", sv3 = "", sw1 = "N", tTicket = "", Sql1 = "", tMessg = "", tItem2="";
       string tPONUMBER = "", tPOITEM = "", tDocumentID = "", tDocumentTime = "", RunDBPath = "";
       string tDocument_NO = "", tSemaphone1 = "";

       if (DPath == "") RunDBPath = PubRunDBPath;
       else RunDBPath = DPath;

      
       //string[,] tmpAsusXml = new string[XRay + 1, 60 + 1];
       //for (v1 = 0; v1 < XRay; v1++)
       //    for (v2 = 0; v2 < 5 + 1; v2++)
       //        tmpAsusXml[v1, v2] = "";

       string sql01 = "  SELECT * from  APXMLM where CSTATUS = 'R' and Semaphone1 = '' ";
       DataSet dsXmlM = LibSCM1Pointer.GetDataByDataPath(sql01, RunDBPath);
       if (dsXmlM.Tables.Count <= 0) v1 = 0; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
       else v1 = dsXmlM.Tables[0].Rows.Count;

       if (v1 == 0) return ( v1.ToString());

       int dsXmlMLong = v1; //  dsXmlM.Tables[0].Rows.Count;
       string[,] tmpTot = new string[dsXmlMLong + 1, 60 + 1];
       for (v1 = 0; v1 < dsXmlMLong + 1; v1++)
           for (v2 = 0; v2 < 60 + 1; v2++)
                tmpTot[v1, v2] = "";

       v4 = 0;
       for (v3 = 0; v3 < dsXmlMLong; v3++)
       {
           v4 = 0;
           // arrayBOMM1[v1 + 1, 2] = GridView1.Rows[v2].Cells[0].Text;
           tmpTot[v3 + 1, 1] = v3.ToString();
           tmpTot[v3 + 1, 2] = dsXmlM.Tables[0].Rows[v3]["CustNo"].ToString();
           tmpTot[v3 + 1, 3] = dsXmlM.Tables[0].Rows[v3]["XmlType"].ToString();
           tmpTot[v3 + 1, 4] = dsXmlM.Tables[0].Rows[v3]["Ticket"].ToString();
           tmpTot[v3 + 1, 5] = dsXmlM.Tables[0].Rows[v3]["DocumentID"].ToString();
           tmpTot[v3 + 1, 6] = dsXmlM.Tables[0].Rows[v3]["DataCnt"].ToString();
           tmpTot[v3 + 1, 7] = dsXmlM.Tables[0].Rows[v3]["CStatus"].ToString();
           tmpTot[v3 + 1, 8] = dsXmlM.Tables[0].Rows[v3]["FStatus"].ToString();
           tmpTot[v3 + 1, 9] = dsXmlM.Tables[0].Rows[v3]["Semaphone1"].ToString();
           tmpTot[v3 + 1, SLoc + 1] = dsXmlM.Tables[0].Rows[v3]["F1"].ToString();      // PO_NO,   PONUMBER
           tmpTot[v3 + 1, SLoc + 2] = dsXmlM.Tables[0].Rows[v3]["F2"].ToString();      // LINE_ITEM, POITEM
           tmpTot[v3 + 1, SLoc + 3] = dsXmlM.Tables[0].Rows[v3]["F3"].ToString();
           tmpTot[v3 + 1, SLoc + 4] = dsXmlM.Tables[0].Rows[v3]["F4"].ToString();
           tmpTot[v3 + 1, SLoc + 5] = dsXmlM.Tables[0].Rows[v3]["F5"].ToString();
           tmpTot[v3 + 1, SLoc + 6] = dsXmlM.Tables[0].Rows[v3]["F6"].ToString();
           tmpTot[v3 + 1, SLoc + 7] = dsXmlM.Tables[0].Rows[v3]["F7"].ToString();
           tmpTot[v3 + 1, SLoc + 8] = dsXmlM.Tables[0].Rows[v3]["F8"].ToString();
           tmpTot[v3 + 1, SLoc + 9] = dsXmlM.Tables[0].Rows[v3]["F9"].ToString();
           tmpTot[v3 + 1, SLoc + 10] = dsXmlM.Tables[0].Rows[v3]["F10"].ToString();
           tmpTot[v3 + 1, SLoc + 11] = dsXmlM.Tables[0].Rows[v3]["F11"].ToString();
           tmpTot[v3 + 1, SLoc + 12] = dsXmlM.Tables[0].Rows[v3]["F12"].ToString();
           tmpTot[v3 + 1, SLoc + 13] = dsXmlM.Tables[0].Rows[v3]["F13"].ToString();
           tmpTot[v3 + 1, SLoc + 14] = dsXmlM.Tables[0].Rows[v3]["F14"].ToString();


           tTicket = dsXmlM.Tables[0].Rows[v3]["Ticket"].ToString();
           tDocument_NO = dsXmlM.Tables[0].Rows[v3]["DocumentID"].ToString();

           Sql1 = "Insert into  ASUS_EDI_PO870_HEADING ( DOCUMENT_NO, REPORT_STATUS, GROUP_CODE, REPORT_DATE, MANUFACTURER, PLANT_CODE ) Values "
               + " ( '" + tDocument_NO + "', '4', 'PA', '" + tmpTot[1, SLoc + 8] + "', 'QA050', 'V05CNLH'  ) ";
           if (!LibSCM1Pointer.DBExcuteByDataPath(Sql1, RunDBPath))
               v4++;  // Failss
           else       // 
               v2++;  // Succ Count
           
           Sql1 = "Insert into ASUS_EDI_PO870_DETAIL_ORDER ( DOCUMENT_NO, PO_NO, PO_DATE, Vendor_Order, MERCHANDISE_TYPE ) Values "
           + " ( '" + tDocument_NO + "', '" + tmpTot[v3 + 1, SLoc + 2] + "', '" + tmpTot[v3 + 1, SLoc + 8] + "', 'SO', 'HH'  ) ";
           if (!LibSCM1Pointer.DBExcuteByDataPath(Sql1, RunDBPath))
               v4++;  // Failss
           else       // 
               v2++;  // Succ Count

           Sql1 = "Insert into ASUS_EDI_PO870_DETAIL_ITEM ( DOCUMENT_NO, PO_NO, LINE_ITEM, Quantity, UNIT, Unit_Price   ) Values "
           + " ( '" + tDocument_NO + "', '" + tmpTot[v3 + 1, SLoc + 2] + "',  '" + tmpTot[v3 + 1, SLoc + 4] + "', '" + tmpTot[v3 + 1, SLoc + 8] + "', "
           + " '" + tmpTot[v3 + 1, SLoc + 14] + "', '" + tmpTot[v3 + 1, SLoc + 12] + "' ) ";
           if (!LibSCM1Pointer.DBExcuteByDataPath(Sql1, RunDBPath))
               v4++;  // Failss
           else       // 
               v2++;  // Succ Count

           if (v4 > 0) // Insert 3 file Fail OK Semaphone = v
               tSemaphone1 = v2.ToString();
           else
               tSemaphone1 = "";

           Sql1 = "update  APXMLM set Semaphone1 = '" + tSemaphone1 + "', CSTATUS = 'W' where DocumentID =  '" + tDocument_NO + "' ";
           if (!LibSCM1Pointer.DBExcuteByDataPath(Sql1, RunDBPath))
               v4++;  // Failss

       }

       return (XmlLong.ToString());

   }  // XMLTmpToDBF end


}  // end public class AXml1lib
}  // end namespace Economy.GCMDKen


