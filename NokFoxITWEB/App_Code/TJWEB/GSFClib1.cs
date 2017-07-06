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

 
namespace SFC.TJWEB
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public   class GSFClib1
{

    public static string A1 = "い翠瑈禣ノだ舥╰参";
    public static string A2 = "穝糤计沮Θ";

    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd");
    protected string CuurNumber = (DateTime.Now.Hour * 60 + DateTime.Now.Minute).ToString ();//流水号
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
    static int    SEQID ;

    //public string CutTraData(string Dbread, string Dbwrite, string Documnent, string BeginTime)
    //{
    //    string mes = "";
    //    string sql = "insert into   bak.PRODSFC1 (";
    //    sql = sql + "documentid,ddate ,line,runname,projectno ,process ,materialno,standardupph,standardcapacity,dlactualman ,";
    //    sql=sql+"dlstandardman ,productarrange,arrangetime ,actualtime,actualupph,actualoutput,actualoutputyield ,yield ,";
    //    sql=sql+"productoutputstandardtime ,dltotaltime ,timedifference,agreementrate ,performancerate ,timerate,upphrate,";
    //    sql=sql+"creatdate ,datasource,ydproduct ,yduph ,site,project ,standardtime,sectionname ,departmentname,divname ,";
    //    sql=sql+"flag1 ,flag2 ,flag3 ,flag4 ,flag5 ,flag6 ,flag7 ,flag8 ,flag9 ,updatefusetime,fusereturn ,time ,seqid ,";
    //    sql=sql+"rdate ,prodqty ,docseq ,documentid1 ,docseq1 )values (";

    //    string sql_value = ":documentid,:ddate ,:line,:runname,:projectno,:process,:materialno,:standardupph,:standardcapacity,:dlactualman,:dlstandardman,";
    //    sql_value = sql_value +  ":productarrange,:arrangetime,:actualtime,:actualupph,:actualoutput,:actualoutputyield,:yield,:productoutputstandardtime,";
    //    sql_value = sql_value +   ":dltotaltime,:timedifference,:agreementrate,:performancerate,:timerate,:upphrate,:creatdate,:datasource,:ydproduct,:yduph,";
    //    sql_value = sql_value + ":site,:project,:standardtime,:sectionname,:departmentname,:divname,:flag1,:flag2,:flag3,:flag4,:flag5,:flag6,:flag7,:flag8";
    //    sql_value = sql_value + ",:flag9,:updatefusetime,:fusereturn,:time,:seqid,:rdate,:prodqty,:docseq,:documentid1,:docseq1) ";
    //    sql = sql + sql_value; 

    //    string strselect ="select * from TMP.PRODSFC1 where Rdate = '"+BeginTime +"' ";                       
    //    DataTable dtselect = PDataBaseOperation.PSelectSQLDT("oracle", Dbread, strselect);
    //    if (dtselect.Rows.Count > 0)
    //    {
    //        string documentid = DateTime.Now.ToString("yyyyMMddHHmmssff");
    //        string[] paramName = new string[53];
    //        paramName[0] = ":documentid";
    //        paramName[1] = ":ddate";
    //        paramName[2] = ":line";
    //        paramName[3] = ":runname";
    //        paramName[4] = ":projectno";
    //        paramName[5] = ":process";
    //        paramName[6] = ":materialno";
    //        paramName[7] = ":standardupph";
    //        paramName[8] = ":standardcapacity";
    //        paramName[9] = ":dlactualman";
    //        paramName[10] = ":dlstandardman";
    //        paramName[11] = ":productarrange";
    //        paramName[12] = ":arrangetime";
    //        paramName[13] = ":actualtime";
    //        paramName[14] = ":actualupph";
    //        paramName[15] = ":actualoutput";
    //        paramName[16] = ":actualoutputyield";
    //        paramName[17] = ":yield";
    //        paramName[18] = ":productoutputstandardtime";
    //        paramName[19] = ":dltotaltime";
    //        paramName[20] = ":timedifference";
    //        paramName[21] = ":agreementrate";
    //        paramName[22] = ":performancerate";
    //        paramName[23] = ":timerate";
    //        paramName[24] = ":upphrate";
    //        paramName[25] = ":creatdate";
    //        paramName[26] = ":datasource";
    //        paramName[27] = ":ydproduct";
    //        paramName[28] = ":yduph";
    //        paramName[29] = ":site";
    //        paramName[30] = ":project";
    //        paramName[31] = ":standardtime";
    //        paramName[32] = ":sectionname";
    //        paramName[33] = ":departmentname";
    //        paramName[34] = ":divname";
    //        paramName[35] = ":flag1";
    //        paramName[36] = ":flag2";
    //        paramName[37] = ":flag3";
    //        paramName[38] = ":flag4";
    //        paramName[39] = ":flag5";
    //        paramName[40] = ":flag6";
    //        paramName[41] = ":flag7";
    //        paramName[42] = ":flag8";
    //        paramName[43] = ":flag9";
    //        paramName[44] = ":updatefusetime";
    //        paramName[45] = ":fusereturn";
    //        paramName[46] = ":time";
    //        paramName[47] = ":seqid";
    //        paramName[48] = ":rdate";
    //        paramName[49] = ":prodqty";
    //        paramName[50] = ":docseq";
    //        paramName[51] = ":documentid1";
    //        paramName[52] = ":docseq1";
    //        for (int rc = 0; rc < dtselect.Rows.Count; rc++)
    //        {               
    //            object [] paramValue = new string[53];
    //            paramValue[0]  = dtselect.Rows[rc]["documentid"].ToString();
    //            paramValue[1]  = dtselect.Rows[rc]["ddate"].ToString();
    //            paramValue[2]  = dtselect.Rows[rc]["line"].ToString();
    //            paramValue[3]  = dtselect.Rows[rc]["runname"].ToString();
    //            paramValue[4]  = dtselect.Rows[rc]["projectno"].ToString();
    //            paramValue[5]  = dtselect.Rows[rc]["process"].ToString();
    //            paramValue[6]  = dtselect.Rows[rc]["materialno"].ToString();
    //            paramValue[7]  = dtselect.Rows[rc]["standardupph"].ToString();
    //            paramValue[8]  = dtselect.Rows[rc]["standardcapacity"].ToString();
    //            paramValue[9]  = dtselect.Rows[rc]["dlactualman"].ToString();
    //            paramValue[10] = dtselect.Rows[rc]["dlstandardman"].ToString();
    //            paramValue[11] = dtselect.Rows[rc]["productarrange"].ToString();
    //            paramValue[12] = dtselect.Rows[rc]["arrangetime"].ToString();
    //            paramValue[13] = dtselect.Rows[rc]["actualtime"].ToString();
    //            paramValue[14] = dtselect.Rows[rc]["actualupph"].ToString();
    //            paramValue[15] = dtselect.Rows[rc]["actualoutput"].ToString();
    //            paramValue[16] = dtselect.Rows[rc]["actualoutputyield"].ToString();
    //            paramValue[17] = dtselect.Rows[rc]["yield"].ToString();
    //            paramValue[18] = dtselect.Rows[rc]["productoutputstandardtime"].ToString();
    //            paramValue[19] = dtselect.Rows[rc]["dltotaltime"].ToString();
    //            paramValue[20] = dtselect.Rows[rc]["timedifference"].ToString();
    //            paramValue[21] = dtselect.Rows[rc]["agreementrate"].ToString();
    //            paramValue[22] = dtselect.Rows[rc]["performancerate"].ToString();
    //            paramValue[23] = dtselect.Rows[rc]["timerate"].ToString();
    //            paramValue[24] = dtselect.Rows[rc]["upphrate"].ToString();
    //            paramValue[25] = dtselect.Rows[rc]["creatdate"].ToString();
    //            paramValue[26] = dtselect.Rows[rc]["datasource"].ToString();
    //            paramValue[27] = dtselect.Rows[rc]["ydproduct"].ToString();
    //            paramValue[28] = dtselect.Rows[rc]["yduph"].ToString();
    //            paramValue[29] = dtselect.Rows[rc]["site"].ToString();
    //            paramValue[30] = dtselect.Rows[rc]["project"].ToString();
    //            paramValue[31] = dtselect.Rows[rc]["standardtime"].ToString();
    //            paramValue[32] = dtselect.Rows[rc]["sectionname"].ToString();
    //            paramValue[33] = dtselect.Rows[rc]["departmentname"].ToString();
    //            paramValue[34] = dtselect.Rows[rc]["divname"].ToString();
    //            paramValue[35] = dtselect.Rows[rc]["flag1"].ToString();
    //            paramValue[36] = dtselect.Rows[rc]["flag2"].ToString();
    //            paramValue[37] = dtselect.Rows[rc]["flag3"].ToString();
    //            paramValue[38] = dtselect.Rows[rc]["flag4"].ToString();
    //            paramValue[39] = dtselect.Rows[rc]["flag5"].ToString();
    //            paramValue[40] = dtselect.Rows[rc]["flag6"].ToString();
    //            paramValue[41] = dtselect.Rows[rc]["flag7"].ToString();
    //            paramValue[42] = dtselect.Rows[rc]["flag8"].ToString();
    //            paramValue[43] = dtselect.Rows[rc]["flag9"].ToString();
    //            paramValue[44] = dtselect.Rows[rc]["updatefusetime"].ToString();
    //            paramValue[45] = dtselect.Rows[rc]["fusereturn"].ToString();
    //            paramValue[46] = dtselect.Rows[rc]["time"].ToString();
    //            paramValue[47] = dtselect.Rows[rc]["seqid"].ToString();
    //            paramValue[48] = dtselect.Rows[rc]["rdate"].ToString();
    //            paramValue[49] = dtselect.Rows[rc]["prodqty"].ToString();
    //            paramValue[50] = dtselect.Rows[rc]["docseq"].ToString();
    //            paramValue[51] = dtselect.Rows[rc]["documentid1"].ToString();
    //            paramValue[52] = dtselect.Rows[rc]["docseq1"].ToString();       
    //            int idetinsert = PDataBaseOperation.PExecSQL("oracle", Dbwrite, sql, paramName, paramValue);
    //            }
    //         string strdelete = "delete from TMP.PRODSFC1 where Rdate = '" + BeginTime + "' ";
    //         int iii = PDataBaseOperation.PExecSQL ( "oracle",  Dbread, strselect);
           
    //    }
    //    return "";
    //}


    //public string Mfr_Summary_call( string constr_read,string constr_write, string time_start, string time_end, string level_id, string line_id, string cellline_id, string family_id, string station_id, string model_code )
    //  {

    //      try 
    //      { 
    //          string documentid = System.DateTime.Now.ToString("yyyyMMddHHmmssff");       
    //           DataTable dt = PDataBaseOperation.Mfr_Summary(constr_read,time_start, time_end, level_id, line_id, cellline_id, family_id, station_id, model_code);
    //          string sqlstr;
    //          if (dt.Rows.Count >0)
    //            {
    //                for (int i=0; i < dt.Rows.Count;i++ )
    //                { 
    //                    sqlstr = "insert into  REPORT.PUBPRODUCTDAILYREPORT ";
    //                    sqlstr = sqlstr + "(  documentid ,ddate ,line,projectno ,process  ,actualoutputyield,yield,site, project,  updatefusetime) ";
    //                    sqlstr = sqlstr + " values ('" + documentid + "','" + time_start + "','" + line_id + "','" + model_code + "','" + level_id + "','" + dt.Rows[i]["FT"].ToString() + "','" + dt.Rows[i]["FTY"].ToString() + "','" + "LF" + "','"+  model_code+ "','" +System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' )";
    //                    int ii = DataBaseOperation.ExecSQL("ORACLE", constr_write, sqlstr);
    //                 }
    //             }
    //                return "true";
    //    }
    //    catch(Exception ee )
    //    {
    //       return  ee.Message;
        
    //    }
    //}

    public static string GetSapDN(string DN, string dboReadS, string dboWriteS, string bdate, string edate)
    {
        try
        {
            if (dboReadS.ToUpper() == "TEST")
            {
                dboReadS = "ASHOST=10.134.92.27 SYSNR=00 CLIENT=802 USER=FIHBJKFC PASSWD=FOXCONN8 LANG=en";
            }
            else
            {
                dboReadS = "ASHOST=10.134.28.98 SYSNR=00 CLIENT=802 USER=RFCSHARE02 PASSWD=it0215 LANG=en";
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
            VKORGV.Value = "WIX1";
            cmd.Parameters.Add(VKORGV);


            SAPParameter WERKSV = new SAPParameter("@WERKSV", ParameterDirection.Input);
            WERKSV.Value = "WXI6";
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
            DataTable SAPdt3 = (DataTable)cmd.Parameters["@OUTPUT_PACKINGV"].Value;
            string SSS="";
            for (int I=0;I< SAPdt3.Columns .Count ;I++)
            {
                SSS = SSS + SAPdt3.Columns[I].ColumnName +",";
            
            
            
            }
      

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

                sqlstr = "select count(*) ct from Delivery_Notification_MT where DNID='" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows[0]["ct"].Equals(0))
                {

                    arrDN_MT[i + 1, 1] = tmpDN;
                    arrDN_MT[i + 1, 2] = ID;
                    //     ,HUQTY-----pallet_QTY
                    //     ,BoxQTY-----Carton_QTY
                    //     ,SerialIDQTY----QTY
                    //,ItemQTY -----""
                    // netunitcode----unit(SAP)

                    //sqlstr = "GrossUnitCode,ID,DNID,CreationDT,IssueDT,GrossWeight,NetWeight,Country,BuyerPartyInternalID, VendorPartyVendorID ,HUQTY ,BoxQTY ,SerialIDQTY  ,ItemQTY ,netunitcode ,VolumeUnitCode,Volume";
                    //sqlstr = " update Delivery_Notification_MT set Volume = '" + SAPdt3.Rows[i]["TAVOL"] + "'  where DNID= '" + tmpDN + "'";
                    //20140912 奔IssueDT
                    sqlstr = "GrossUnitCode,ID,DNID,CreationDT,GrossWeight,NetWeight,Country,BuyerPartyInternalID, VendorPartyVendorID ,HUQTY ,BoxQTY ,SerialIDQTY  ,ItemQTY ,netunitcode ,VolumeUnitCode,Volume,WayBillID,DocumentID";
                    sqlstr = "insert into  Delivery_Notification_MT (" + sqlstr + " ) values ('";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["UNIT"].ToString().Replace("KG", "KGM") + "','" + ID + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    //2013-12-02T12:12:12.1234567Z
                    sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["BLDAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";
                    //sqlstr = sqlstr + "'" + System.Convert.ToDateTime(SAPdt1.Rows[i]["WADAT"]).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["BRGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["NTGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["COUNTRY_KEY"] + "',";
                    sqlstr = sqlstr + "'" + "" + "',";
                    sqlstr = sqlstr + "'" + "" + "'  , ";
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
                    //I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    //sqlstr = " update  a ";
                    //sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
                    //sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
                    //sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.ID= '" + ID + "'";
                    //I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

                    sqlstr = sqlstr + "'" +  SAPdt1.Rows[i]["TAVOL"]  + "',";
                    sqlstr = sqlstr + SAPdt1.Rows[i]["HMD_DNN"] + ",";
                    sqlstr = sqlstr + "'" + sysdatetime + "'" + ")";
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    sqlstr = "  update Delivery_Notification_MT set ";
                    sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where DNID= '" + tmpDN + "'";
                    I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    sqlstr = " update  a ";
                    sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
                    sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
                    sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.DNID= '" + tmpDN + "'";
                    I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                }
            }


            for (int i = 0; i < SAPdt2.Rows.Count; i++)
            {
                sqlstr = "select ID,WayBillID from Delivery_Notification_MT where DNID='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' ";
                DNdt = PDataBaseOperation.PSelectSQLDS("SQL", dboWriteS, sqlstr);             
                v4 = DNdt.Tables[0].Rows.Count;
                string DNNID="";
                string documentid = "";
                if (v4 > 0)
                {
                    tmp6 = DNdt.Tables[0].Rows[0]["ID"].ToString().Trim();
                    DNNID = DNdt.Tables[0].Rows[0]["WayBillID"].ToString().Trim();
                    documentid = DNdt.Tables[0].Rows[0]["DocumentID"].ToString().Trim();
                }
                    


                sqlstr = "select count(*) ct from Delivery_DNITEM where DNID='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' and ItemID ='" + SAPdt2.Rows[i]["POSNR"] + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows[0]["ct"].Equals(0))
                {


                    sqlstr = "insert into  Delivery_DNITEM ( ID,DNID,ItemID,POID,POItemID,InternalID,ProductRecipientID , Total_QTY,UNIT,Carton_QTY,Pallet_QTY,DocumentID) values ( ";
                    // 20140507 sqlstr = sqlstr + "'" + ID + "',";
                    sqlstr = sqlstr + "'" + tmp6 + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',";
                    sqlstr = sqlstr + "'" + SAPdt2.Rows[i]["POSNR"] + "',";
                    //20151223倒PO干0
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
                    if (v > 0) // Succ update POID into DN
                    {
                        tmpDN = SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8); // DNID
                        tmp7 = SAPdt2.Rows[i]["VGBEL"].ToString().Trim(); // POID
                        sqlstr = " update Delivery_Notification_MT set POID = '" + poid + "'  where DNID= '" + tmpDN + "'";
                        I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    }
                    //20170309
                    string strdnnitem = "select * from [IMSCM].[dbo].[DNNITEM] where DNID='" + DNNID + "'"+
                        " and POID='" + poid + "' and POItemID='" + SAPdt2.Rows[i]["VGPOS"].ToString().Substring(1,5) + "'"+
                        " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                    DataTable dtdnn = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, strdnnitem);
                    string updDnn = "";
                    if (dtdnn.Rows.Count > 0)
                    {
                        updDnn = "update [IMSCM].[dbo].[DNNITEM] set FOXDN='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "',FOXDNITEM='" + SAPdt2.Rows[i]["POSNR"] + "'" +
                            " where DNID='" + DNNID + "'" +
                            " and POID='" + poid + "' and POItemID='" + SAPdt2.Rows[i]["VGPOS"].ToString().Substring(1, 5) + "'" +
                            " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                    }
                    else
                    {
                        string strdnnitem1 = "select * from [IMSCM].[dbo].[DNNITEM] where DNID='" + DNNID + "'" +
                                               " and POID='" + poid + "'" +
                                               " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                        DataTable dtdnn1 = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, strdnnitem1);
                        if(dtdnn1.Rows.Count>0)
                        {
                            updDnn = "update [IMSCM].[dbo].[DNNITEM] set UF1='HMD PO & PO item is wrong'" +
                            " where DNID='" + DNNID + "'" +
                            " and POID='" + poid + "'" +
                            " and ProductRecipientID='" + SAPdt2.Rows[i]["KDMAT"].ToString().Trim() + "'";
                        }
                        else
                        {
                            updDnn = "update [IMSCM].[dbo].[DNN_MT] set UF1='HMD PO & PO item is wrong'" +
                            " where DNID='" + DNNID + "'";
                        }
                    }
                    int i6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, updDnn);
                }
            }


            //=========================================================================================
            for (int i = 0; i < SAPdt3.Rows.Count; i++)
            {
                sqlstr = "select ID from Delivery_Notification_MT where DNID='" + SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' ";
                DNdt = PDataBaseOperation.PSelectSQLDS("SQL", dboWriteS, sqlstr);
                v4 = DNdt.Tables[0].Rows.Count;
                string documentid = "";
                if (v4 > 0)
                {
                    tmp6 = DNdt.Tables[0].Rows[0]["ID"].ToString().Trim();
                    documentid = DNdt.Tables[0].Rows[0]["DocumentID"].ToString().Trim();
                }
                    

                sqlstr = "select count(*) ct from Delivery_Notification_HU where DNID='" + SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' and HUID ='" + SAPdt3.Rows[i]["VENUM"] + "' and itemid ='" + SAPdt3.Rows[i]["POSNR"] + "'";
                DataTable dgt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dgt.Rows[0]["ct"].Equals(0))
                {



                    sqlstr = "insert into  Delivery_Notification_HU (  ID,DNID,HUID,ItemID,LoadInternalID,Delivery_Notification_HU_UF1,Delivery_Notification_HU_UF2,Delivery_Notification_HU_UF3,Delivery_Notification_HU_UF4,LoadQA,Delivery_Notification_HU_UF5,DocumentID) values ( ";
                    sqlstr = sqlstr + "'" + ID + "',";
                   
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VBELN"].ToString ().Substring(2, 8)  + "',";
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VENUM"] + "',";

                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["POSNR"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["TVTYT"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["NTGEW"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["BRGEW"] + "',";
                    sqlstr = sqlstr + "'KG',";
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["TAVOL"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VEMNG"] + "',";
                    sqlstr = sqlstr + "'" + SAPdt3.Rows[i]["VOLEH"] +  "',";
                    sqlstr = sqlstr + "'" + documentid + "')";

                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

                   // Delivery_Notification_MT- Volume戈糶SAP Output_header-TAVOL逆じ



                    //if (v > 0) // Succ update POID into DN
                    //{
                    //    tmpDN = SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8); // DNID
                    //   // tmp7 = SAPdt3.Rows[i]["VGBEL"].ToString().Trim(); // POID
                    //    sqlstr = " update Delivery_Notification_MT set Volume = '" + SAPdt3.Rows[i]["TAVOL"] + "'  where DNID= '" + tmpDN + "'";
                    //    I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    //}
                }
            }




            //  20140504
            //  sqlstr = "  update Delivery_Notification_MT set ";
            //  sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
            //  int I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
            //  sqlstr = " update  a ";
            //  sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
            //  sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c, Delivery_DNITEM d";
            //  sqlstr = sqlstr + " where  a.DNID=d.DNID and d.POID =c.POID and d.POID =b.POID and a.ID= '" + ID + "'";
            //  int I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);

            // 20140508
            for (int i = 0; i < SAPdt1.Rows.Count; i++)
            {
                tmpDN = arrDN_MT[i + 1, 1];
                ID = arrDN_MT[i + 1, 2];
                if ((ID != "") && (tmpDN != ""))
                {
                    sqlstr = "  update Delivery_Notification_MT set ";
                    sqlstr = sqlstr + "ReturnsIndicator =( select ValueStr  from DN_Master where    NameStr=  'ReturnsIndicator')   where ID= '" + ID + "'";
                    I5 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    sqlstr = " update  a ";
                    sqlstr = sqlstr + " set a.BuyerPartyInternalID  =b.ProductBuyerID,a.VendorPartyVendorID=c.SELLERPARTYID";
                    sqlstr = sqlstr + " from Delivery_Notification_MT a ,PO_Create_DT b , PO_Create_MT  c ";
                    sqlstr = sqlstr + " where  a.POID =c.POID and a.POID =b.POID and a.DNID = '" + tmpDN + "'";
                    I6 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                }
            }


            return "True";
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            return "False";
        }

    }  //

    public static string GetSap940(string DN, string dboReadS, string dboWriteS, string bdate, string edate)
    {
         

            //VBELN_TAB  -->   VBELN
            //  ORDER_PARTNERS
            // ORDER_HEADER
           
            //ORDER_ITEM
            //   MESSAGE


        SAPConnection con = new SAPConnection(dboReadS);
       // SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh");
            con.Open();
            SAPCommand cmd = new SAPCommand(con);
            cmd.CommandText = "EXEC ZRFC_B2B_MICROSOFT      @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV , @VBELN=@VBELNV , @VKORG=@VKORGV ,@WERKS=@WERKSV ,  @ORDER_PARTNERS=@ORDER_PARTNERSV output, @ORDER_HEADER=@ORDER_HEADERV output , @ORDER_ITEM=@ORDER_ITEMV output, @MESSAGE=@MESSAGEV output ";
          //  cmd.CommandText = "EXEC ZRFC_SD_MS_0001   @BEGIN_DATE=@BEGIN_DATEV , @END_DATE=@END_DATEV , @VBELN=@VBELNV , @VKORG=@VKORGV ,@WERKS=@WERKSV ,  @OUTPUT_HEADER=@OUTPUT_HEADERV output, @OUTPUT_ITEM=@OUTPUT_ITEMV output ";
         
        
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
            VKORGV.Value = "WIX1";
            cmd.Parameters.Add(VKORGV);


            SAPParameter WERKSV = new SAPParameter("@WERKSV", ParameterDirection.Input);
            WERKSV.Value = "WXI6";
            cmd.Parameters.Add(WERKSV);
            ///-------------------------------------------------------------------------------

            SAPParameter ORDER_PARTNERSV = new SAPParameter("@ORDER_PARTNERSV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(ORDER_PARTNERSV);

            SAPParameter ORDER_HEADERV = new SAPParameter("@ORDER_HEADERV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(ORDER_HEADERV);


            SAPParameter ORDER_ITEMV = new SAPParameter("@ORDER_ITEMV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(ORDER_ITEMV);

            SAPParameter MESSAGEV = new SAPParameter("@MESSAGEV", ParameterDirection.InputOutput);
            cmd.Parameters.Add(MESSAGEV);



            SAPDataReader dr = cmd.ExecuteReader();
            DataTable SAPdt1 = (DataTable)cmd.Parameters["@ORDER_PARTNERSV"].Value;
            DataTable SAPdt2 = (DataTable)cmd.Parameters["@ORDER_HEADERV"].Value;

             DataTable SAPdt3 = (DataTable)cmd.Parameters["@ORDER_ITEMV"].Value;
              DataTable SAPdt4= (DataTable)cmd.Parameters["@MESSAGEV"].Value;
              if (SAPdt1.Rows.Count.Equals ( 0))
              {
                  return "";
              }
             
            string sysdatetime = System.DateTime.Now.ToString("yyMMddHHmmssmm"); // 
            string ID="";
            string tmpDN = "";
            ID = "F" + SAPdt1.Rows[0]["ZVBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;
                


                sysdatetime = System.DateTime.Now.ToString("yyMMddHHmmssmm");
                ID = "F" + SAPdt1.Rows[0]["ZVBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;
                tmpDN = SAPdt1.Rows[0]["ZVBELN"].ToString().Substring(2, 8);
            
       
        for (int i = 0; i < SAPdt1.Rows.Count; i++)
            {
                string sqlstr = "select count(*) ct from Address where w0502   =  '" + SAPdt1.Rows[i]["ZVBELN"].ToString().Substring(2, 8) + "' and addresstype ='" + SAPdt1.Rows[i]["PARVW"] + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ct"].Equals(0))
                    {
                        sqlstr = "w0502,addresstype,addresstocode,addresstoname,addressinfodesc,cityname,statecode,postcode,countrycode,ID,CreationDT";
                        sqlstr = "insert into  Address (" + sqlstr + " ) values (";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["ZVBELN"].ToString().Substring(2, 8) + "',";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["PARVW"] + "',";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["KUNNR"] + "',";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["NAME1"] + "',";
                        sqlstr = sqlstr + "substring( '" + SAPdt1.Rows[i]["STRAS"] + " " + SAPdt1.Rows[i]["STR_SUPPL1"] + " " + SAPdt1.Rows[i]["STR_SUPPL2"] + " " + SAPdt1.Rows[i]["STR_SUPPL3"] + " " + SAPdt1.Rows[i]["LOCATION"] + "',1,55),";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["ORT01"] + "',";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["REGIO"] + "',";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["PSTLZ"] + "',";
                        sqlstr = sqlstr + "'" + SAPdt1.Rows[i]["LAND1"] + "',";
                        sqlstr = sqlstr + "'" + ID + "',";
                        sqlstr = sqlstr + "'" + sysdatetime + "')";

                        int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr);
                    }
                }
            }




        
        //select deliverRefer ,t.* from  DN_Header t where w0502='0082740106'
        string strname="";
        string strmail = "";
        string id = "";
        string strdnid ="";
        string DNID = "";
       string get_update_ser =  " SELECT  TESTDATAINDICATOR,TRANSFERLOCATIONNAME ,id FROM  PO_CREATE_MT  where poid ='"+SAPdt2.Rows[0]["BSTNK"] +"'";

       DataTable dtt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, get_update_ser);

       if (dtt.Rows.Count != 0)

       {
           strname = dtt.Rows[0]["TESTDATAINDICATOR"].ToString ();
           strmail = dtt.Rows[0]["TRANSFERLOCATIONNAME"].ToString();
           ID = dtt.Rows[0]["ID"].ToString();
       }

        string sqlstrr2 =  "w0502,w0501,shipfrom,salesorg,deliverRefer,venderterm,customerSO,contractname,contractEmail,contractPhone,requestdate,shippingdate,routing,FOBpoint,industrycode,orderedqty,carton_number_qty,pallete_mumber_qty,ID,CreationDT,uf1,uf10,uf8";
    
        for (int i = 0; i < SAPdt2.Rows.Count; i++)
            {
               string POID = "";

               DataTable POID_table = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, "SELECT  ID FROM  PO_CREATE_MT  where  POID ='" + SAPdt2.Rows[i]["BSTNK"].ToString() + "'");

              if (POID_table.Rows.Count > 0)
              {
                  POID = POID_table.Rows[0]["ID"].ToString();
              }



              DataTable DNID_table = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, "select ID  from  Delivery_Notification_MT   where DNID='" + SAPdt2.Rows[i]["vbeln"].ToString().Substring(2, 8) + "'");

              if (DNID_table.Rows.Count > 0)
              {
                  DNID = DNID_table.Rows[0]["ID"].ToString();
              }



              ID = "F" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "544790470" + sysdatetime;

               string  sqlstr2 = "select count(*) ct from DN_Header where w0502 ='" + SAPdt2.Rows[i]["VBELN"].ToString().Substring(2, 8) + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr2);
                if (dt.Rows[0]["ct"].Equals(0))
                {

                    string sqlstr1 = "insert into  DN_Header (" + sqlstrr2 + " ) values (";
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["vbeln"].ToString().Substring(2, 8) + "',";
                     sqlstr1 = sqlstr1 + "'N',";

                     if (SAPdt2.Rows[i]["WERKS"].ToString().Substring(0, 2) == "WX")
                     {
                         sqlstr1 = sqlstr1 + "'544886',";
                     }
                     else
                     {
                         sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["WERKS"] + "',";
                     }
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["VKORG"] + "',";
                     //20151223倒PO干0
                     string poid = "";
                     if (SAPdt2.Rows[i]["BSTNK"].ToString().Trim().Length == 8) { poid = "00" + SAPdt2.Rows[i]["BSTNK"].ToString().Trim(); }
                     else if (SAPdt2.Rows[i]["BSTNK"].ToString().Trim().Length == 9) { poid = "0" + SAPdt2.Rows[i]["BSTNK"].ToString().Trim(); }
                     else { poid = SAPdt2.Rows[i]["BSTNK"].ToString().Trim(); }
                     sqlstr1 = sqlstr1 + "'" + poid + "',";
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["ZTERM"] + "',";
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["ZVBELN"] + " ',";
                     //sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["NAME1"] + "',";
                     //sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["EMAIL"] + "',";
                     sqlstr1 = sqlstr1 + "'" + strname + "',";
                     sqlstr1 = sqlstr1 + "'" + strmail + "',";

                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["TELF1"] + "',";

                     //   
                     sqlstr2 = " SELECT  PO_CREATE_DT_UF1 FROM PO_CREATE_DT  where  POID ='" + poid + "'";

                     DataTable dt11 = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr2);

                     if (dt11.Rows.Count > 0) { strdnid = dt11.Rows[0]["PO_CREATE_DT_UF1"].ToString(); } else { strdnid = ""; }

                    //sqlstr1 = sqlstr1 + "'" + string.Format("{0:yyyyMMdd}", SAPdt2.Rows[i]["WADAT"]) + "',";
                     sqlstr1 = sqlstr1 + "'" + strdnid.Replace ("Z","") + "',";

                     sqlstr1 = sqlstr1 + "'" + string.Format("{0:yyyyMMdd}", SAPdt2.Rows[i]["WADAT"]) + "',";
                     
                    
              

                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["ROUTE"] + "',";
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["VSTEL"] + "',";
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["INCO1"] + "',";
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["Z_LFIMG"] + "',";
                     //sqlstr1 = sqlstr1 + "'" + SAPdt3.Rows[i]["Z_CARTONS"] + "',";
                     //sqlstr1 = sqlstr1 + "'" + SAPdt3.Rows[i]["z_PALLETS"] + "',";       
                     sqlstr1 = sqlstr1 + "'" + Convert.ToInt16(SAPdt2.Rows[i]["T_CARTONS"].ToString().Trim()).ToString() + "',";
                     sqlstr1 = sqlstr1 + "'" + SAPdt2.Rows[i]["T_PALLETS"].ToString().Trim() + "',";   
                     sqlstr1 = sqlstr1 + "'" + ID+"',";
                     sqlstr1 = sqlstr1 + "'" + sysdatetime + "','" + POID + "','" + POID + "','" + DNID +   "')";
                   
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr1);

                  //  sqlstr1 = "update PO_CREATE_MT   set  PO_CREATE_MT_UF8='" + DNID + "', PO_CREATE_MT_UF6 ='"+ ID+ "' where POID ='" + SAPdt2.Rows[i]["BSTNK"].ToString() +"'"; 

                  //  int v1 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr1);

                    string sqlstrr1 = "update   Delivery_Notification_MT set    Delivery_Notification_MT_uf10 = '" + POID + "' ,Delivery_Notification_MT_uf6='"+ID+"'   where   DNID  ='" + SAPdt2.Rows[i]["vbeln"].ToString().Substring (2,8)+"'";

                    int v2 = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstrr1); 





                }
            }











        string sqlstr5 = "LX01,w0502,qty,MGpartnum,buyeritemno,Selleritemno,plantno,lineitemweight,origiancountry,cartonqty,palletqty,ID,CreationDT";
        int i_count = 1;
        for (int i = 0; i < SAPdt3.Rows.Count; i++)
            {
                string sqlstr4 = "select count(*) ct from DN_detail where w0502  = '" + SAPdt3.Rows[i]["VBELN"].ToString().Substring(2, 8) + "' and Selleritemno ='" + SAPdt3.Rows[i]["POSNR"].ToString() + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT("SQL", dboWriteS, sqlstr4);
                if (dt.Rows[0]["ct"].Equals(0))
                {

                    string sqlstr3 = "insert into  DN_detail (" + sqlstr5 + " ) values (";
                    sqlstr3 = sqlstr3 + "'" + (i+1).ToString() + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["vbeln"].ToString().Substring(2, 8) + "',";                  
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["lfimg"] + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["KDMAT"] + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["VGPOS"] + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["POSNR"] + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["WERKS"] + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["BRGEW"] + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["LAND1"] + "',";
                    sqlstr3 = sqlstr3 + "'" + Convert.ToInt16 ( SAPdt3.Rows[i]["Z_CARTONS"].ToString ().Trim()).ToString () + "',";
                    sqlstr3 = sqlstr3 + "'" + SAPdt3.Rows[i]["z_PALLETS"].ToString().Trim() + "',";            
                    sqlstr3 = sqlstr3 + "'" + ID+"',";
                    sqlstr3 = sqlstr3 + "'" + sysdatetime+"')";
                   
                    int v = PDataBaseOperation.PExecSQL("SQL", dboWriteS, sqlstr3); 
                }
            }






            return "True";
        //}
        //catch (Exception ex)
        //{
        //    string tmp = ex.ToString();
        //    return "False";
        //}

    }  //


    public static string GetSapWO(string Readtbl, string Writbl, string PUBLIB, string PLANT, string SCHEDULED_DATE, string COUNT, string ORDER_NUMBER)
    { 
    
    GetSapWO1( Readtbl, Writbl,  PUBLIB, "WBF2,WXF2,WXF3,WXF4,WXF6" , SCHEDULED_DATE, COUNT, ORDER_NUMBER);
    GetSapWO1( Readtbl, Writbl,  PUBLIB, "WBFB,WXFB,WXFC,WXF5,WXF7" , SCHEDULED_DATE, COUNT, ORDER_NUMBER);
    return "";
    }
    public static string GetSapWO1(string Readtbl,string Writbl, string PUBLIB, string PLANT ,string SCHEDULED_DATE,string COUNT,string ORDER_NUMBER)
    {
        string sss1;
        sss1 = "";
        try
        {
           // DataBaseOperation dboRead  = new DataBaseOperation("ORACLE", dboReadS);
           // DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", dboWriteS);
             
           // SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh");
                SAPConnection con = new SAPConnection("ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh");
 
                con.Open();
                SAPCommand cmd = new SAPCommand(con);
                cmd.CommandText = "EXEC  ZRFC_WLBG_KIT_PO2   @PLANT=@PLANTV,@SCHEDULED_DATE=@SCHEDULED_DATEV,@COUNT=@COUNTV ,@ORDER_NUMBER=@ORDER_NUMBERV, @WO_HEADER=@WOHEADER output, @WO_ITEM=@WOITEM output";


                SAPParameter PLANTV = new SAPParameter("@PLANTV", ParameterDirection.Input);
                PLANTV.Value =PLANT;//"WBBB"
                cmd.Parameters.Add(PLANTV);


                SAPParameter SCHEDULED_DATEV = new SAPParameter("@SCHEDULED_DATEV", ParameterDirection.Input);
                SCHEDULED_DATEV.Value = SCHEDULED_DATE;// "20150615";
                cmd.Parameters.Add(SCHEDULED_DATEV);


                SAPParameter COUNTV = new SAPParameter("@COUNTV", ParameterDirection.Input);
                COUNTV.Value = COUNT;// "14";
                cmd.Parameters.Add(COUNTV);


                SAPParameter ORDER_NUMBERV = new SAPParameter("@ORDER_NUMBERV", ParameterDirection.Input);
                ORDER_NUMBERV.Value =ORDER_NUMBER;
                cmd.Parameters.Add(ORDER_NUMBERV);


                SAPParameter WOHEADER = new SAPParameter("@WOHEADER", ParameterDirection.InputOutput);
                cmd.Parameters.Add(WOHEADER);
                SAPParameter WOITEM = new SAPParameter("@WOITEM", ParameterDirection.InputOutput);
                cmd.Parameters.Add(WOITEM);
                SAPDataReader dr = cmd.ExecuteReader();
                DataTable SAPdt1 = (DataTable)cmd.Parameters["@WOHEADER"].Value;
                DataTable SAPdt2 = (DataTable)cmd.Parameters["@WOITEM"].Value;


                string sql_insert_str;
                if (SAPdt1.Rows.Count > 0)
                {
                    SEQID = 100001;
                    for (int x = 0; x < SAPdt1.Rows.Count; x++)
                    {
                        string strselect1 = "select * from  tmp2.WOHEADER where AUFNR = '" + SAPdt1.Rows[x]["AUFNR"].ToString() + "' ";

                         DataTable dt = PDataBaseOperation.PSelectSQLDT("oracle", Writbl, strselect1);

                         if (dt.Rows.Count.Equals(0))
                         {

                             sql_insert_str = "insert into tmp2.WOHEADER (AUFNR,WERKS,AUART,MATNR,REVLV,KDAUF,GSTRS,GAMNG,KDMAT,AEDAT,AENAM,MATKL,MAKTX,ERDAT,GSUPS,ERFZEIT,GLTRS,GLUPS,LGORT,PFlag,SEQID,Documentid) values (";
                             sql_insert_str = sql_insert_str + "'" + SAPdt1.Rows[x]["AUFNR"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["WERKS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["AUART"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["MATNR"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["REVLV"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["KDAUF"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["GSTRS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["GAMNG"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["KDMAT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["AEDAT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["AENAM"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["MATKL"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["MAKTX"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["ERDAT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["GSUPS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["ERFZEIT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["GLTRS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["GLUPS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["LGORT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt1.Rows[x]["PFlag"].ToString() + "','";

                             sql_insert_str = sql_insert_str + SEQID.ToString() + "','";

                             sql_insert_str = sql_insert_str + tDocumentID + "')";
                             SEQID = SEQID + 1;

                             int iii = PDataBaseOperation.PExecSQL("oracle", Writbl, sql_insert_str);

                         }
                         else 
                         {


                             sql_insert_str = "update tmp2.WOHEADER  set ";
                             sql_insert_str = sql_insert_str + "AUFNR='" + SAPdt1.Rows[x]["AUFNR"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "WERKS='" + SAPdt1.Rows[x]["WERKS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "AUART='" + SAPdt1.Rows[x]["AUART"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "MATNR='" + SAPdt1.Rows[x]["MATNR"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "REVLV='" + SAPdt1.Rows[x]["REVLV"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "KDAUF='" + SAPdt1.Rows[x]["KDAUF"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "GSTRS='" + SAPdt1.Rows[x]["GSTRS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "GAMNG='" + SAPdt1.Rows[x]["GAMNG"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "KDMAT='" + SAPdt1.Rows[x]["KDMAT"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "AEDAT='" + SAPdt1.Rows[x]["AEDAT"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "AENAM='" + SAPdt1.Rows[x]["AENAM"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "MATKL='" + SAPdt1.Rows[x]["MATKL"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "MAKTX='" + SAPdt1.Rows[x]["MAKTX"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "ERDAT='" + SAPdt1.Rows[x]["ERDAT"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "GSUPS='" + SAPdt1.Rows[x]["GSUPS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "ERFZEIT='" + SAPdt1.Rows[x]["ERFZEIT"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "GLTRS='" + SAPdt1.Rows[x]["GLTRS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "GLUPS='" + SAPdt1.Rows[x]["GLUPS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "LGORT='" + SAPdt1.Rows[x]["LGORT"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "PFlag='" + SAPdt1.Rows[x]["PFlag"].ToString() + "'";

                             sql_insert_str = sql_insert_str + " where AUFNR = '" + SAPdt1.Rows[x]["AUFNR"].ToString() + "' ";

                             
                              

                             int iii = PDataBaseOperation.PExecSQL("oracle", Writbl, sql_insert_str);


                         
                         }
                    }
                
                }

                if (SAPdt2.Rows.Count > 0)
                {
                    SEQID = 100001;
                    for (int y = 0; y < SAPdt2.Rows.Count; y++)
                    {

                        string strselect1 = "select * from  tmp2.WOITEM where AUFNR = '" + SAPdt2.Rows[y]["AUFNR"].ToString() + "' ";

                         DataTable dt = PDataBaseOperation.PSelectSQLDT("oracle", Writbl, strselect1);

                         if (dt.Rows.Count.Equals(0))
                         {

                             sql_insert_str = "insert into tmp2.WOITEM ( AUFNR,RSNUM,POSNR,MATNR,PARTS,KDMAT,BDMNG,MEINS,REVLV,BAUGR,REPNO,REPPARTNO,AUART,AENAM,AEDAT,MAKTX,MATKL,WGBEZ,ALPOS,POTX1,POTX2,LTXSP,TEXT1,LGORT,SEQID,Documentid) values (";









                             sql_insert_str = sql_insert_str + "'" + SAPdt2.Rows[y]["AUFNR"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["RSNUM"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["POSNR"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["MATNR"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["PARTS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["KDMAT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["BDMNG"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["MEINS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["REVLV"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["BAUGR"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["REPNO"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["REPPARTNO"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["AUART"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["AENAM"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["AEDAT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["MAKTX"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["MATKL"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["WGBEZ"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["ALPOS"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["POTX1"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["POTX2"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["LTXSP"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["TEXT1"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["LGORT"].ToString() + "','";
                             sql_insert_str = sql_insert_str + SEQID.ToString() + "','";
                             sql_insert_str = sql_insert_str + tDocumentID + "')";

                             int iii = PDataBaseOperation.PExecSQL("oracle", Writbl, sql_insert_str);
                             SEQID = SEQID + 1;

                         }
                         else {
                             sql_insert_str = "update tmp2.WOITEM  set ";
                             sql_insert_str = sql_insert_str + "AUFNR='" + SAPdt2.Rows[y]["AUFNR"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "RSNUM='" + SAPdt2.Rows[y]["RSNUM"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "POSNR='" + SAPdt2.Rows[y]["POSNR"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "MATNR='" + SAPdt2.Rows[y]["MATNR"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "PARTS='" + SAPdt2.Rows[y]["PARTS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "KDMAT='" + SAPdt2.Rows[y]["KDMAT"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "BDMNG='" + SAPdt2.Rows[y]["BDMNG"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "MEINS='" + SAPdt2.Rows[y]["MEINS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "REVLV='" + SAPdt2.Rows[y]["REVLV"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "BAUGR='" + SAPdt2.Rows[y]["BAUGR"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "REPNO='" + SAPdt2.Rows[y]["REPNO"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "REPPARTNO='" + SAPdt2.Rows[y]["REPPARTNO"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "AUART='" + SAPdt2.Rows[y]["AUART"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "AENAM='" + SAPdt2.Rows[y]["AENAM"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "AEDAT='" + SAPdt2.Rows[y]["AEDAT"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "MAKTX='" + SAPdt2.Rows[y]["MAKTX"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "MATKL='" + SAPdt2.Rows[y]["MATKL"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "WGBEZ='" + SAPdt2.Rows[y]["WGBEZ"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "ALPOS='" + SAPdt2.Rows[y]["ALPOS"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "POTX1='" + SAPdt2.Rows[y]["POTX1"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "POTX2='" + SAPdt2.Rows[y]["POTX2"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "LTXSP='" + SAPdt2.Rows[y]["LTXSP"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "TEXT1='" + SAPdt2.Rows[y]["TEXT1"].ToString() + "',";
                             sql_insert_str = sql_insert_str + "LGORT='" + SAPdt2.Rows[y]["LGORT"].ToString() + "'";
                             sql_insert_str = sql_insert_str + " where AUFNR = '" + SAPdt2.Rows[y]["AUFNR"].ToString() + "' ";
                             int iiii = PDataBaseOperation.PExecSQL("oracle", Writbl, sql_insert_str);
                         }

                    }
                

                }

             
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            return "False";
        }

        return "true";
    }



   // public static DataSet GetWO(string woNumber, string plant, ConnInfo conn)
    //public static DataSet GetWO(ConnInfo conn)
    //{
    //    string woNumber; 
    //    string plant; 
          
    //    woNumber = "";
    //    plant = "WBBB";
        
    //    RfcExecutor sap = new RfcExecutor();
    //    Inputs ins = new Inputs();
    //    ins.RfcName = "ZRFC_SFC_MSE_0008";
    //    ins.ImportParams.Add(new SapParam("PLANT", plant));
    //    ins.ImportParams.Add(new SapParam("SCHEDULED_DATE", "2016-06-16"));
    //    ins.ImportParams.Add(new SapParam("COUNT", "14"));
    //    woNumber = woNumber.PadLeft(12, '0');
    //    ins.ImportParams.Add(new SapParam("ORDER_NUMBER", woNumber));
    //    Outputs ous = null;
    //    try
    //    {         
    //      ous = sap.Exec(ins, new Outputs(), conn);
    //    }
    //    catch (Exception ex)
    //    {
    //        string s = ex.Message;
    //    }
    //    return ous.Tables;
    //}


    //public static DataSet GetWONew(string woNumber, string plant, ConnInfo conn)
    //{
    //    RfcExecutor sap = new RfcExecutor();
    //    Inputs ins = new Inputs();
    //    ins.RfcName = "ZRFC_SFC_ENSK_0001";
    //    ins.ImportParams.Add(new SapParam("PLANT", plant));
    //    ins.ImportParams.Add(new SapParam("AUART", ""));
    //    ins.ImportParams.Add(new SapParam("I_DATUV", ""));
    //    ins.ImportParams.Add(new SapParam("I_DATUB", ""));
    //    ins.ImportParams.Add(new SapParam("AUFNR", woNumber));
    //    Outputs ous = null;
    //    try
    //    {
    //        ous = sap.Exec(ins, new Outputs(), conn);
    //    }
    //    catch (Exception ex)
    //    {
    //        string s = ex.Message;
    //    }
    //    return ous.Tables;
    //}




    //public static string ExecuteGetSMAWOData(string Readtbl, string Writbl, string Date1, string Date2)
    //{
    //    string sqlstr;
    //    string strconn = "" ; 
    //   GetWorkOrder.ProductionService getworkorder = new GetWorkOrder.ProductionService();

    //   string str = getworkorder.ExecuteGetSMAWOData(Date1, Date2).ToString();
 
    //    if (str.Substring(0, 5) == "FALSE") { return "FALSE"; }
    //    str = str.Replace("[", "");
    //    str = str.Replace("]", "");
    //    str = str.Replace(",{", "{");
    //    str = str.Replace("{", "");
    //    str = str.Replace(@"""", "");
    //    str = str.Replace(@"\", "");
    //    str = str.Replace(@"""", "");
    //    str = str.Replace(@":", "");
    //    str = str.Replace("CREATOR", "");
    //    str = str.Replace("CREATE_TIME", "");
    //    str = str.Replace("WO_TYPE", "");
    //    str = str.Replace("ORDER_NUMBER", "");
    //    str = str.Replace("MODEL_CODE", "");
    //    str = str.Replace("QUANTITY", "");
    //    str = str.Replace("START_QTY", "");
    //    str = str.Replace("INPUT_QTY", "");
    //    str = str.Replace("READY_QTY", "");
    //    str = str.Replace("LINE", "");
       
    //    str = str.Replace("WO_FLAG", "");
    //    str = str.Replace("UNIT", "");
    //    str = str.Replace("WO_DESC", "");
    //    str = str.Replace("WERKS", "");
    //    str = str.Replace("LGORT", "");
    //    str = str.Replace("PNL_QTY", "");
    //    str = str.Replace("GSTRS", "");
    //    str = str.Replace("GLTRS", "");
    //    str = str.Replace("CONFIRMOR", "");
    //    str = str.Replace("CONFIRM_TIME", "");
    //    str = str.Replace("FLAG", "");
 
    //    string sysdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss_fffZ");
    //    string[] sArray = str.Split('}');


    //    try
    //    {
    //        SEQID = 100001;

    //        foreach (string i in sArray)
    //        {
    //            if (i.Length > 8)
    //            {
    //                string[] SA = i.Split(','); 
    //                sqlstr = " insert into publib.WORK_ORDER_L6 ( CREATOR,CREATE_TIME,WO_TYPE,ORDER_NUMBER,MODEL_CODE,QUANTITY,START_QTY,INPUT_QTY,READY_QTY,LINE,FLAG,WO_FLAG,UNIT,WO_DESC,WERKS,LGORT,PNL_QTY,GSTRS,GLTRS,CONFIRMOR,CONFIRM_TIME,DocumentID ,SeqID) values ('";
    //                        sqlstr = sqlstr + SA[0] + "','";
    //                        sqlstr = sqlstr + SA[1] + "','";
    //                        sqlstr = sqlstr + SA[2] + "','";
    //                        sqlstr = sqlstr + SA[3] + "','";
    //                        sqlstr = sqlstr + SA[4] + "','";
    //                        sqlstr = sqlstr + SA[5] + "','";
    //                        sqlstr = sqlstr + SA[6] + "','";
    //                        sqlstr = sqlstr + SA[7] + "','";
    //                        sqlstr = sqlstr + SA[8] + "','";
    //                        sqlstr = sqlstr + SA[9] + "','";
    //                        sqlstr = sqlstr + SA[10] + "','";
    //                        sqlstr = sqlstr + SA[11] + "','";
    //                        sqlstr = sqlstr + SA[12] + "','";
    //                        sqlstr = sqlstr + SA[13] + "','";
    //                        sqlstr = sqlstr + SA[14] + "','";
    //                        sqlstr = sqlstr + SA[15] + "','";
    //                        sqlstr = sqlstr + SA[16] + "','";
    //                        sqlstr = sqlstr + SA[17] + "','";
    //                        sqlstr = sqlstr + SA[18] + "','";
    //                        sqlstr = sqlstr + SA[19] + "','";
    //                        sqlstr = sqlstr + SA[20] + "','";
    //                        sqlstr = sqlstr + tDocumentID + "','";
    //                        sqlstr = sqlstr + SEQID + "' )";                            
                             
    //                        SEQID = SEQID + 1;




    //                        int I = PDataBaseOperation.PExecSQL("ORACLE", Writbl, sqlstr);
                       
    //                }
    //            }
    //        }

             

        

    //    catch (Exception ex)
    //    {

    //        string err_str = ex.Message;

    //    }
    //    return ("");
    //}




    public static string ExecuteGet_WORK_ORDER(string Readtbl, string Writbl, string Date1, string Date2)
    {
        string sqlstr;
        string str_columns;
        str_columns = "ORDER_NUMBER,FAMILY_ID,MODEL_CODE,PCF_VERSION,QUANTITY,CREATE_TIME,AUTHOR,DESCRIPTION,ORDER_TYPE,ORDER_PRIORITY,START_QTY,READY_QTY,";
        str_columns = str_columns + "INPUT_QTY,ORDER_STATE,LINE_ID,EXCEPTION_FLAG,EXCEPTION_COUNT,COMPLETE_TIME,PALLET_MASTERPACK_COUNT,MASTERPACK_UNIT_COUNT,LOSS_QTY,UNBIND_QTY,CURRENT_MASTERPACK_NUMBER,";
        str_columns = str_columns + "CURRENT_PALLET_NUMBER,PO,PO_QTY,DNN,PO_TO_ADDRESS,BTS_FLAG,PID,SHIPPING_FLAG,AVG_WEIGHT,SUB_MP_FLAG,WO_ID,PO_CONFIRM_RESULT_ID,COUNTRY_ISO_CODE,INDUSTRIAL_FLAG,MPK_AVG_WEIGHT,IS_CELL_ORDER";

        string sysdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss_fffZ");
        sqlstr = "select *  from master_user.work_order where ";
        sqlstr = sqlstr + "  CREATE_TIME  BETWEEN TO_DATE('" + Date1 + "','YYYY/MM/DD HH24:MI:SS') AND  TO_DATE('" + Date2 + "','YYYY/MM/DD HH24:MI:SS')";

        DataTable dt1 = PDataBaseOperation.PSelectSQLDT("ORACLE", Readtbl, sqlstr);
        try
        {
            SEQID = 100001;

            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                sqlstr = " select * from PUBLIB.WORK_ORDER   where  ORDER_NUMBER ='" + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "'";




                DataTable dtt = PDataBaseOperation.PSelectSQLDT("ORACLE", Writbl, sqlstr);

                if (dtt.Rows.Count > 0)
                {

                    sqlstr = " update PUBLIB.WORK_ORDER  set  ";


                    sqlstr = sqlstr + "ORDER_NUMBER='" + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "',";
                    sqlstr = sqlstr + "FAMILY_ID='" + dt1.Rows[i]["FAMILY_ID"].ToString() + "',";
                    sqlstr = sqlstr + "MODEL_CODE='" + dt1.Rows[i]["MODEL_CODE"].ToString() + "',";
                    sqlstr = sqlstr + "PCF_VERSION='" + dt1.Rows[i]["PCF_VERSION"].ToString() + "',";
                    sqlstr = sqlstr + "QUANTITY='" + dt1.Rows[i]["QUANTITY"].ToString() + "',";
                    sqlstr = sqlstr + "CREATE_TIME='" + dt1.Rows[i]["CREATE_TIME"].ToString() + "',";
                    sqlstr = sqlstr + "AUTHOR='" + dt1.Rows[i]["AUTHOR"].ToString() + "',";
                    sqlstr = sqlstr + "DESCRIPTION='" + dt1.Rows[i]["DESCRIPTION"].ToString() + "',";
                    sqlstr = sqlstr + "ORDER_TYPE='" + dt1.Rows[i]["ORDER_TYPE"].ToString() + "',";
                    sqlstr = sqlstr + "ORDER_PRIORITY='" + dt1.Rows[i]["ORDER_PRIORITY"].ToString() + "',";
                    sqlstr = sqlstr + "START_QTY='" + dt1.Rows[i]["START_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "READY_QTY='" + dt1.Rows[i]["READY_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "INPUT_QTY='" + dt1.Rows[i]["INPUT_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "ORDER_STATE='" + dt1.Rows[i]["ORDER_STATE"].ToString() + "',";
                    sqlstr = sqlstr + "LINE_ID='" + dt1.Rows[i]["LINE_ID"].ToString() + "',";
                    sqlstr = sqlstr + "EXCEPTION_FLAG='" + dt1.Rows[i]["EXCEPTION_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "EXCEPTION_COUNT='" + dt1.Rows[i]["EXCEPTION_COUNT"].ToString() + "',";
                    sqlstr = sqlstr + "COMPLETE_TIME='" + dt1.Rows[i]["COMPLETE_TIME"].ToString() + "',";
                    sqlstr = sqlstr + "PALLET_MASTERPACK_COUNT='" + dt1.Rows[i]["PALLET_MASTERPACK_COUNT"].ToString() + "',";
                    sqlstr = sqlstr + "MASTERPACK_UNIT_COUNT='" + dt1.Rows[i]["MASTERPACK_UNIT_COUNT"].ToString() + "',";
                    sqlstr = sqlstr + "LOSS_QTY='" + dt1.Rows[i]["LOSS_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "UNBIND_QTY='" + dt1.Rows[i]["UNBIND_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "CURRENT_MASTERPACK_NUMBER='" + dt1.Rows[i]["CURRENT_MASTERPACK_NUMBER"].ToString() + "',";
                    sqlstr = sqlstr + "CURRENT_PALLET_NUMBER='" + dt1.Rows[i]["CURRENT_PALLET_NUMBER"].ToString() + "',";
                    sqlstr = sqlstr + "PO='" + dt1.Rows[i]["PO"].ToString() + "',";
                    sqlstr = sqlstr + "PO_QTY='" + dt1.Rows[i]["PO_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "DNN='" + dt1.Rows[i]["DNN"].ToString() + "',";
                    sqlstr = sqlstr + "PO_TO_ADDRESS='" + dt1.Rows[i]["PO_TO_ADDRESS"].ToString() + "',";
                    sqlstr = sqlstr + "BTS_FLAG='" + dt1.Rows[i]["BTS_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "PID='" + dt1.Rows[i]["PID"].ToString() + "',";
                    sqlstr = sqlstr + "SHIPPING_FLAG='" + dt1.Rows[i]["SHIPPING_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "AVG_WEIGHT='" + dt1.Rows[i]["AVG_WEIGHT"].ToString() + "',";
                    sqlstr = sqlstr + "SUB_MP_FLAG='" + dt1.Rows[i]["SUB_MP_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "WO_ID='" + dt1.Rows[i]["WO_ID"].ToString() + "',";
                    sqlstr = sqlstr + "PO_CONFIRM_RESULT_ID='" + dt1.Rows[i]["PO_CONFIRM_RESULT_ID"].ToString() + "',";
                    sqlstr = sqlstr + "COUNTRY_ISO_CODE='" + dt1.Rows[i]["COUNTRY_ISO_CODE"].ToString() + "',";
                    sqlstr = sqlstr + "INDUSTRIAL_FLAG='" + dt1.Rows[i]["INDUSTRIAL_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "MPK_AVG_WEIGHT='" + dt1.Rows[i]["MPK_AVG_WEIGHT"].ToString() + "',";
                    sqlstr = sqlstr + "IS_CELL_ORDER='" + dt1.Rows[i]["IS_CELL_ORDER"].ToString() + "'";
                    sqlstr = sqlstr + " where ORDER_NUMBER='" + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "'";



                }
                else
                {


                    sqlstr = " insert into      PUBLIB.WORK_ORDER  ( " + str_columns;
                    sqlstr = sqlstr + " ,DocumentID ,SeqID) values ('";

                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["FAMILY_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["MODEL_CODE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PCF_VERSION"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["QUANTITY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["CREATE_TIME"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["AUTHOR"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["DESCRIPTION"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_TYPE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_PRIORITY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["START_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["READY_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["INPUT_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_STATE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["LINE_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["EXCEPTION_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["EXCEPTION_COUNT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["COMPLETE_TIME"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PALLET_MASTERPACK_COUNT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["MASTERPACK_UNIT_COUNT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["LOSS_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["UNBIND_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["CURRENT_MASTERPACK_NUMBER"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["CURRENT_PALLET_NUMBER"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["DNN"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO_TO_ADDRESS"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["BTS_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["SHIPPING_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["AVG_WEIGHT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["SUB_MP_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["WO_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO_CONFIRM_RESULT_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["COUNTRY_ISO_CODE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["INDUSTRIAL_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["MPK_AVG_WEIGHT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["IS_CELL_ORDER"].ToString() + "','";
                    sqlstr = sqlstr + tDocumentID + "','";
                    sqlstr = sqlstr + SEQID + "' )";





                }
                SEQID = SEQID + 1;




                int I = PDataBaseOperation.PExecSQL("ORACLE", Writbl, sqlstr);


            }
        }





        catch (Exception ex)
        {

            string err_str = ex.Message;

        }
        return ("");
    }






    public static string ExecuteGet_WO_Data1(string Readtbl, string Writbl, string Date1, string Date2)
    {
        string sqlstr; 
        string str_columns ;
        str_columns="ORDER_NUMBER,FAMILY_ID,MODEL_CODE,PCF_VERSION,QUANTITY,CREATE_TIME,AUTHOR,DESCRIPTION,ORDER_TYPE,ORDER_PRIORITY,START_QTY,READY_QTY,";
        str_columns=str_columns+"INPUT_QTY,ORDER_STATE,LINE_ID,EXCEPTION_FLAG,EXCEPTION_COUNT,COMPLETE_TIME,PALLET_MASTERPACK_COUNT,MASTERPACK_UNIT_COUNT,LOSS_QTY,UNBIND_QTY,CURRENT_MASTERPACK_NUMBER,";
        str_columns=str_columns+"CURRENT_PALLET_NUMBER,PO,PO_QTY,DNN,PO_TO_ADDRESS,BTS_FLAG,PID,SHIPPING_FLAG,AVG_WEIGHT,SUB_MP_FLAG,WO_ID,PO_CONFIRM_RESULT_ID,COUNTRY_ISO_CODE,INDUSTRIAL_FLAG,MPK_AVG_WEIGHT,IS_CELL_ORDER";

        string sysdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss_fffZ");
        sqlstr = "select *  from master_user.work_order where ";
        sqlstr = sqlstr + "  CREATE_TIME  BETWEEN TO_DATE('" + Date1 + "','YYYY/MM/DD HH24:MI:SS') AND  TO_DATE('" + Date2 + "','YYYY/MM/DD HH24:MI:SS')";

        DataTable dt1 = PDataBaseOperation.PSelectSQLDT("ORACLE", Readtbl, sqlstr);
        try
        {
            SEQID = 100001;          
           
            for (Int32 i=0;i<dt1.Rows.Count ;i++ )
            {
                sqlstr = " select * from PUBLIB.WORK_ORDER_L10   where  ORDER_NUMBER ='" + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "'";




                DataTable dtt = PDataBaseOperation.PSelectSQLDT("ORACLE", Writbl, sqlstr);

                if (dtt.Rows.Count > 0) {

                    sqlstr = " update      PUBLIB.WORK_ORDER_L10  set  ";


                    sqlstr = sqlstr + "ORDER_NUMBER='" + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "',";
                    sqlstr = sqlstr + "FAMILY_ID='" + dt1.Rows[i]["FAMILY_ID"].ToString() + "',";
                    sqlstr = sqlstr + "MODEL_CODE='" + dt1.Rows[i]["MODEL_CODE"].ToString() + "',";
                    sqlstr = sqlstr + "PCF_VERSION='" + dt1.Rows[i]["PCF_VERSION"].ToString() + "',";
                    sqlstr = sqlstr + "QUANTITY='" + dt1.Rows[i]["QUANTITY"].ToString() + "',";
                    sqlstr = sqlstr + "CREATE_TIME='" + dt1.Rows[i]["CREATE_TIME"].ToString() + "',";
                    sqlstr = sqlstr + "AUTHOR='" + dt1.Rows[i]["AUTHOR"].ToString() + "',";
                    sqlstr = sqlstr + "DESCRIPTION='" + dt1.Rows[i]["DESCRIPTION"].ToString() + "',";
                    sqlstr = sqlstr + "ORDER_TYPE='" + dt1.Rows[i]["ORDER_TYPE"].ToString() + "',";
                    sqlstr = sqlstr + "ORDER_PRIORITY='" + dt1.Rows[i]["ORDER_PRIORITY"].ToString() + "',";
                    sqlstr = sqlstr + "START_QTY='" + dt1.Rows[i]["START_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "READY_QTY='" + dt1.Rows[i]["READY_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "INPUT_QTY='" + dt1.Rows[i]["INPUT_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "ORDER_STATE='" + dt1.Rows[i]["ORDER_STATE"].ToString() + "',";
                    sqlstr = sqlstr + "LINE_ID='" + dt1.Rows[i]["LINE_ID"].ToString() + "',";
                    sqlstr = sqlstr + "EXCEPTION_FLAG='" + dt1.Rows[i]["EXCEPTION_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "EXCEPTION_COUNT='" + dt1.Rows[i]["EXCEPTION_COUNT"].ToString() + "',";
                    sqlstr = sqlstr + "COMPLETE_TIME='" + dt1.Rows[i]["COMPLETE_TIME"].ToString() + "',";
                    sqlstr = sqlstr + "PALLET_MASTERPACK_COUNT='" + dt1.Rows[i]["PALLET_MASTERPACK_COUNT"].ToString() + "',";
                    sqlstr = sqlstr + "MASTERPACK_UNIT_COUNT='" + dt1.Rows[i]["MASTERPACK_UNIT_COUNT"].ToString() + "',";
                    sqlstr = sqlstr + "LOSS_QTY='" + dt1.Rows[i]["LOSS_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "UNBIND_QTY='" + dt1.Rows[i]["UNBIND_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "CURRENT_MASTERPACK_NUMBER='" + dt1.Rows[i]["CURRENT_MASTERPACK_NUMBER"].ToString() + "',";
                    sqlstr = sqlstr + "CURRENT_PALLET_NUMBER='" + dt1.Rows[i]["CURRENT_PALLET_NUMBER"].ToString() + "',";
                    sqlstr = sqlstr + "PO='" + dt1.Rows[i]["PO"].ToString() + "',";
                    sqlstr = sqlstr + "PO_QTY='" + dt1.Rows[i]["PO_QTY"].ToString() + "',";
                    sqlstr = sqlstr + "DNN='" + dt1.Rows[i]["DNN"].ToString() + "',";
                    sqlstr = sqlstr + "PO_TO_ADDRESS='" + dt1.Rows[i]["PO_TO_ADDRESS"].ToString() + "',";
                    sqlstr = sqlstr + "BTS_FLAG='" + dt1.Rows[i]["BTS_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "PID='" + dt1.Rows[i]["PID"].ToString() + "',";
                    sqlstr = sqlstr + "SHIPPING_FLAG='" + dt1.Rows[i]["SHIPPING_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "AVG_WEIGHT='" + dt1.Rows[i]["AVG_WEIGHT"].ToString() + "',";
                    sqlstr = sqlstr + "SUB_MP_FLAG='" + dt1.Rows[i]["SUB_MP_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "WO_ID='" + dt1.Rows[i]["WO_ID"].ToString() + "',";
                    sqlstr = sqlstr + "PO_CONFIRM_RESULT_ID='" + dt1.Rows[i]["PO_CONFIRM_RESULT_ID"].ToString() + "',";
                    sqlstr = sqlstr + "COUNTRY_ISO_CODE='" + dt1.Rows[i]["COUNTRY_ISO_CODE"].ToString() + "',";
                    sqlstr = sqlstr + "INDUSTRIAL_FLAG='" + dt1.Rows[i]["INDUSTRIAL_FLAG"].ToString() + "',";
                    sqlstr = sqlstr + "MPK_AVG_WEIGHT='" + dt1.Rows[i]["MPK_AVG_WEIGHT"].ToString() + "',";
                    sqlstr = sqlstr + "IS_CELL_ORDER='" + dt1.Rows[i]["IS_CELL_ORDER"].ToString() + "'";
                    
                    sqlstr = sqlstr +" where ORDER_NUMBER='" + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "'";

                
                
                }
                else
                {


                    sqlstr = " insert into      PUBLIB.WORK_ORDER_L10  ( " + str_columns;
                    sqlstr = sqlstr + " ,DocumentID ,SeqID) values ('";

                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_NUMBER"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["FAMILY_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["MODEL_CODE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PCF_VERSION"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["QUANTITY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["CREATE_TIME"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["AUTHOR"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["DESCRIPTION"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_TYPE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_PRIORITY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["START_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["READY_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["INPUT_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["ORDER_STATE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["LINE_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["EXCEPTION_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["EXCEPTION_COUNT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["COMPLETE_TIME"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PALLET_MASTERPACK_COUNT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["MASTERPACK_UNIT_COUNT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["LOSS_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["UNBIND_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["CURRENT_MASTERPACK_NUMBER"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["CURRENT_PALLET_NUMBER"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO_QTY"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["DNN"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO_TO_ADDRESS"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["BTS_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["SHIPPING_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["AVG_WEIGHT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["SUB_MP_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["WO_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["PO_CONFIRM_RESULT_ID"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["COUNTRY_ISO_CODE"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["INDUSTRIAL_FLAG"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["MPK_AVG_WEIGHT"].ToString() + "','";
                    sqlstr = sqlstr + dt1.Rows[i]["IS_CELL_ORDER"].ToString() + "','";
                    sqlstr = sqlstr + tDocumentID + "','";
                    sqlstr = sqlstr + SEQID + "' )";





                }
                    SEQID = SEQID + 1;




                    int I = PDataBaseOperation.PExecSQL("ORACLE", Writbl, sqlstr);

                
            }
        }





        catch (Exception ex)
        {

            string err_str = ex.Message;

        }
        return ("");
    }



    ////public static string ExecuteGetFUSELineInfo(string Readtbl, string Writbl)

    ////     {
    ////    string sqlstr;
    ////    string strconn = "" ; 
    ////   GetWorkOrder.ProductionService getworkorder = new GetWorkOrder.ProductionService();

    ////   string str = getworkorder.ExecuteGetFUSELineInfo().ToString();
 
    ////    if (str.Substring(0, 5) == "FALSE") { return "FALSE"; }
    ////    str = str.Replace("[", "");
    ////    str = str.Replace("]", "");
    ////    str = str.Replace(",{", "{");
    ////    str = str.Replace("{", "");
    ////    str = str.Replace(@"""", "");
    ////    str = str.Replace(@"\", "");
    ////    str = str.Replace(@"""", "");
    ////    str = str.Replace(@":", "");



    ////    str = str.Replace("CREATE_TIME", "");
    ////    str=str.Replace("LINE_ID","");
    ////    str=str.Replace("CELL_ID","");
    ////    str = str.Replace("AUTHOR", "");
    ////    str=str.Replace("ATMOS","");
       
 
    ////    string sysdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss_fffZ");

    ////    string StartDateTime = System.DateTime.Now.ToString("yyyy/MM/dd");
    ////    string EndDateTime = System.DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd");


    ////    string[] sArray = str.Split('}');


    ////    try
    ////    {
    ////        SEQID = 100001;
           
    ////        int IDE = PDataBaseOperation.PExecSQL("ORACLE", Writbl,  " DELETE FROM PUBLIB.SFCLINEMAP ");
    ////        foreach (string i in sArray)
    ////        {



    ////            if (i.Length > 8)
    ////            {
    ////                string[] SA = i.Split(',');


    ////                DataTable  Idata_get = PDataBaseOperation.PSelectSQLDT("ORACLE", Writbl, " select count(*) ct FROM PUBLIB.SFCLINEMAP where LINE_ID ='" + SA[0] + "' and CELL_ID='" + SA[1] + "'  ");


    ////                if (Idata_get.Rows[0]["ct"].ToString().Equals("0"))
    ////                {


    ////                    sqlstr = " insert into PUBLIB.SFCLINEMAP ( LINE_ID,CELL_ID ,CREATE_TIME,AUTHOR,ATMOS ) values ('";

    ////                    sqlstr = sqlstr + SA[0] + "','";
    ////                    sqlstr = sqlstr + SA[1] + "','";
    ////                    sqlstr = sqlstr + SA[2] + "','";
    ////                    sqlstr = sqlstr + SA[3] + "','";
    ////                    sqlstr = sqlstr + SA[4] + "' )";


    ////                    int I = PDataBaseOperation.PExecSQL("ORACLE", Writbl, sqlstr);
    ////                }
                     
    ////                }
    ////            }
    ////        }

             

        

    ////    catch (Exception ex)
    ////    {

    ////        string err_str = ex.Message;

    ////    }
    ////    return ("");
    ////}

    public static string SFCTraData(string Dbread, string Dbwrite, string BeginTime, string ENDTime)
    {

        string c1_list;
        string mes = "";
        string strselect;
        string sql;
        string strdelete;
        //   string strdelete;
        int idetinsert;
        int iii;
        //*  ****************************************************************************************************************************************************
        // 1  translation table PACK_HISTORY from SFC to 136

        strdelete = "delete from TMP2.PACK_HISTORY  where create_time >=  '" + BeginTime + "'   and  create_time <=   '" + ENDTime + "'  ";
        iii = PDataBaseOperation.PExecSQL("oracle", Dbwrite, strdelete);



        c1_list = "PSN,LINE_ID,ORDER_NUMBER,PACK_TYPE,WEIGHT,PALLET_NUMBER,MASTERPACK_NUMBER,PACK_TIME,ACTIVE,CREATE_TIME,RESULT_ID,documentid,seqid ";
        //Dc1_list = "PSN,RESULT_ID,MODEL_CODE,PCF_VERSION,STATUS,STATION_ID,TIME_START,TIME_END,LINE_ID,STATION_GROUP,TIMES_FLAG,CREATE_TIME,AUTHOR,ROUTING_FLAG,YIELD_TYPE,GROUP_FAIL_NUMBER,ORDER_NUMBER,EXTEND_FLAG,TIME_TOTAL,FIXTURE,FAMILY_ID,CELL_ID";
        strselect = "select * from sa1.PACK_HISTORY where create_time >=  to_date ( '" + BeginTime + "' ,'yyyy/mm/dd HH24:MI:SS') and  create_time <= to_date ( '" + ENDTime + "','yyyy/mm/dd HH24:MI:SS')  and   pack_type='ATO' and active ='1'";

        System.Data.DataTable dtselect = PDataBaseOperation.PSelectSQLDT("oracle", Dbread, strselect);
        if (dtselect.Rows.Count > 0)
        {
            SEQID = 100001;
            for (Int32 rc = 0; rc < dtselect.Rows.Count; rc++)
            {
                sql = "insert into   tmp2.PACK_HISTORY (" + c1_list + ") values ('";
                sql = sql + dtselect.Rows[rc]["PSN"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["LINE_ID"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["ORDER_NUMBER"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["PACK_TYPE"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["WEIGHT"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["PALLET_NUMBER"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["MASTERPACK_NUMBER"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["PACK_TIME"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["ACTIVE"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["CREATE_TIME"].ToString() + "','";
                sql = sql + dtselect.Rows[rc]["RESULT_ID"].ToString() + "','";
                sql = sql + tDocumentID + "','";
                sql = sql + SEQID + "')";
                idetinsert = PDataBaseOperation.PExecSQL("oracle", Dbwrite, sql);
                SEQID = SEQID + 1;
            }

        }

        //*****************************************************************************************************************************************************
        // translation table PACK_HISTORY from SFC to 136
        strdelete = "delete from TMP2.PRODUCT_LINE_CONFIG ";// where create_time >= '" + BeginTime + "' and  create_time <= '" + EndTime + "' ";
        iii = PDataBaseOperation.PExecSQL("oracle", Dbwrite, strdelete);
        c1_list = "FAMILY_ID,STATION_GROUP,FRCS_IP,FRCS_PORT,DB_IP,FTRS_FOLDER,APPLICATION,QUANTITY,GROUP_LEVEL,ORDER_ID,TEST_FLAG,TABLE_SPACE,WORK_TYPE,CONFIG_GUID,FRCS_IP1,FRCS_PORT1,FTRS_FOLDER1,FUNCTION_GUID,SN_FORMAT,documentid,seqid";
        strselect = "select * from master_user.PRODUCT_LINE_CONFIG ";//  where create_time >=  to_date ( '" + BeginTime + "' ,'yyyy/mm/dd HH24:MI:SS') and  create_time <= to_date ( '" + ENDTime + "','yyyy/mm/dd HH24:MI:SS')  and   pack_type='ATO' and active ='1'";

        System.Data.DataTable dtselect1 = PDataBaseOperation.PSelectSQLDT("oracle", Dbread, strselect);
        if (dtselect1.Rows.Count > 0)
        {
            SEQID = 100001;
            for (Int32 rc = 0; rc < dtselect1.Rows.Count; rc++)
            {
                sql = "insert into   tmp2.PRODUCT_LINE_CONFIG (" + c1_list + ") values ('";

                sql = sql + dtselect1.Rows[rc]["FAMILY_ID"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["STATION_GROUP"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["FRCS_IP"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["FRCS_PORT"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["DB_IP"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["FTRS_FOLDER"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["APPLICATION"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["QUANTITY"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["GROUP_LEVEL"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["ORDER_ID"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["TEST_FLAG"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["TABLE_SPACE"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["WORK_TYPE"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["CONFIG_GUID"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["FRCS_IP1"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["FRCS_PORT1"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["FTRS_FOLDER1"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["FUNCTION_GUID"].ToString() + "','";
                sql = sql + dtselect1.Rows[rc]["SN_FORMAT"].ToString() + "','";


                sql = sql + tDocumentID + "','";
                sql = sql + SEQID + "')";
                idetinsert = PDataBaseOperation.PExecSQL("oracle", Dbwrite, sql);
                SEQID = SEQID + 1;
            }
        }

        //*****************************************************************************************************************************************************
        // 3 translation table PRODUCT_MAINTAIN from SFC to 136

        strdelete = "delete from TMP2.PRODUCT_MAINTAIN where create_time >= '" + BeginTime + "'    and  create_time <=   '" + ENDTime + "'  ";
        iii = PDataBaseOperation.PExecSQL("oracle", Dbwrite, strdelete);
        c1_list = "FAMILY_ID,PRODUCT_TYPE,MODEL_CODE,PCF_VERSION,CREATE_TIME,AUTHOR,PID,ACTIVE,CHILD_PID,documentid,seqid";
        strselect = "select * from master_user.PRODUCT_MAINTAIN ";

        System.Data.DataTable dtselect2 = PDataBaseOperation.PSelectSQLDT("oracle", Dbread, strselect);
        if (dtselect2.Rows.Count > 0)
        {
            SEQID = 100001;
            for (Int32 rc = 0; rc < dtselect2.Rows.Count; rc++)
            {
                sql = "insert into   tmp2.PRODUCT_MAINTAIN (" + c1_list + ") values ('";
                sql = sql + dtselect2.Rows[rc]["FAMILY_ID"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["PRODUCT_TYPE"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["MODEL_CODE"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["PCF_VERSION"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["CREATE_TIME"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["AUTHOR"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["PID"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["ACTIVE"].ToString() + "','";
                sql = sql + dtselect2.Rows[rc]["CHILD_PID"].ToString() + "','";
                sql = sql + tDocumentID + "','";
                sql = sql + SEQID + "')";
                idetinsert = PDataBaseOperation.PExecSQL("oracle", Dbwrite, sql);
                SEQID = SEQID + 1;
            }
        }


        //*****************************************************************************************************************************************************
        // 4 translation table routing_history from SFC to 136
        strdelete = "delete from TMP2.routing_history   where create_time >=   '" + BeginTime + "'   and  create_time <=   '" + ENDTime + "'   ";
        iii = PDataBaseOperation.PExecSQL("oracle", Dbwrite, strdelete);
        c1_list = "PSN,RESULT_ID,MODEL_CODE,PCF_VERSION,STATUS,STATION_ID,TIME_START,TIME_END,LINE_ID,STATION_GROUP,TIMES_FLAG,CREATE_TIME,AUTHOR,ROUTING_FLAG,YIELD_TYPE,GROUP_FAIL_NUMBER,ORDER_NUMBER,EXTEND_FLAG,TIME_TOTAL,FIXTURE,FAMILY_ID,CELL_ID,documentid,seqid";
        strselect = "select * from sa1.routing_history    where create_time >=  to_date ( '" + BeginTime + "' ,'yyyy/mm/dd HH24:MI:SS') and  create_time <= to_date ( '" + ENDTime + "','yyyy/mm/dd HH24:MI:SS')  ";
        System.Data.DataTable dtselect3 = PDataBaseOperation.PSelectSQLDT("oracle", Dbread, strselect);
        if (dtselect3.Rows.Count > 0)
        {
            SEQID = 100001;
            for (Int32 rc = 0; rc < dtselect3.Rows.Count; rc++)
            {
                sql = "insert into   tmp2.routing_history (" + c1_list + ") values ('";
                sql = sql + dtselect3.Rows[rc]["PSN"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["RESULT_ID"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["MODEL_CODE"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["PCF_VERSION"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["STATUS"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["STATION_ID"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["TIME_END"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["TIME_START"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["LINE_ID"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["STATION_GROUP"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["TIMES_FLAG"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["CREATE_TIME"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["AUTHOR"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["ROUTING_FLAG"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["YIELD_TYPE"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["GROUP_FAIL_NUMBER"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["ORDER_NUMBER"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["EXTEND_FLAG"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["TIME_TOTAL"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["FIXTURE"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["FAMILY_ID"].ToString() + "','";
                sql = sql + dtselect3.Rows[rc]["CELL_ID"].ToString() + "','";


                sql = sql + tDocumentID + "','";
                sql = sql + SEQID + "')";
                idetinsert = PDataBaseOperation.PExecSQL("oracle", Dbwrite, sql);
                SEQID = SEQID + 1;
            }
        }
        //*****************************************************************************************************************************************************
        // 5 translation table UNIT_NI_LIST_B from SFC to 136

        strdelete = "delete from TMP2.UNIT_NI_LIST_B  where create_time >=  '" + BeginTime + "'  and  create_time <=   '" + ENDTime + "'   ";
        iii = PDataBaseOperation.PExecSQL("oracle", Dbwrite, strdelete);

        c1_list = "  RECORD_ID,PSN,GROUP_LEVEL,STATION_GROUP,STATUS,NI_REASON,FI_REASON,CREATE_TIME,MODI_TIME,AUTHOR,NOTE,NI_TYPE,TO_STATION_GROUP,QTY,TYPE,CLASS,LINE,NG_QRY,NAME,ROW_GUID,INPUT_TIME,documentid,seqid";
        strselect = "select * from  sa1.UNIT_NI_LIST_B   where create_time >=  to_date ( '" + BeginTime + "' ,'yyyy/mm/dd HH24:MI:SS') and  create_time <= to_date ( '" + ENDTime + "','yyyy/mm/dd HH24:MI:SS')  and group_level ='ATO' ";

        System.Data.DataTable dtselect4 = PDataBaseOperation.PSelectSQLDT("oracle", Dbread, strselect);
        if (dtselect4.Rows.Count > 0)
        {
            SEQID = 100001;
            for (Int32 rc = 0; rc < dtselect4.Rows.Count; rc++)
            {

                sql = "insert into   tmp2.UNIT_NI_LIST_B (" + c1_list + ") values ('";
                sql = sql + dtselect4.Rows[rc]["RECORD_ID"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["PSN"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["GROUP_LEVEL"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["STATION_GROUP"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["STATUS"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["NI_REASON"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["FI_REASON"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["CREATE_TIME"].ToString() + "','";

                sql = sql + dtselect4.Rows[rc]["MODI_TIME"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["AUTHOR"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["NOTE"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["NI_TYPE"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["TO_STATION_GROUP"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["QTY"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["TYPE"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["CLASS"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["LINE"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["NG_QRY"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["NAME"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["ROW_GUID"].ToString() + "','";
                sql = sql + dtselect4.Rows[rc]["INPUT_TIME"].ToString() + "','";
                sql = sql + tDocumentID + "','";
                sql = sql + SEQID + "')";
                idetinsert = PDataBaseOperation.PExecSQL("oracle", Dbwrite, sql);
                SEQID = SEQID + 1;
            }




        }









        return "";
    }


    public static string ExecuteGetFUSEWOData(string Readtbl, string Writbl, string Date1, string Date2)
    {
       // string sqlstr;
       // string strconn = "" ; 
       //GetWorkOrder.ProductionService getworkorder = new GetWorkOrder.ProductionService();

       //string str = getworkorder.ExecuteGetFUSEWOTOTALData(Date1, Date2).ToString();
 
       // if (str.Substring(0, 5) == "FALSE") { return "FALSE"; }
       // str = str.Replace("[", "");
       // str = str.Replace("]", "");
       // str = str.Replace(",{", "{");
       // str = str.Replace("{", "");
       // str = str.Replace(@"""", "");
       // str = str.Replace(@"\", "");
       // str = str.Replace(@"""", "");
       // str = str.Replace(@":", "");
       // str=str.Replace("ORDER_NUMBER","");
       // str=str.Replace("FAMILY_ID","");
       // str=str.Replace("MODEL_CODE","");
       // str=str.Replace("PCF_VERSION","");
       // str=str.Replace("QUANTITY","");
       // str=str.Replace("CREATE_TIME","");
       // str=str.Replace("AUTHOR","");
       // str=str.Replace("DESCRIPTION","");
       // str=str.Replace("ORDER_TYPE","");
       // str=str.Replace("ORDER_PRIORITY","");
       // str=str.Replace("START_QTY","");
       // str=str.Replace("READY_QTY","");
       // str=str.Replace("INPUT_QTY","");
       // str=str.Replace("ORDER_STATE","");
       // str=str.Replace("LINE_ID","");
       // str=str.Replace("EXCEPTION_FLAG","");
       // str=str.Replace("EXCEPTION_COUNT","");
       // str=str.Replace("COMPLETE_TIME","");
       // str=str.Replace("PALLET_MASTERPACK_COUNT","");
       // str=str.Replace("MASTERPACK_UNIT_COUNT","");
       // str=str.Replace("LOSS_QTY","");
       // str=str.Replace("UNBIND_QTY","");
       // str=str.Replace("CURRENT_MASTERPACK_NUMBER","");
       // str=str.Replace("CURRENT_PALLET_NUMBER","");
       // str=str.Replace("PO_QTYNUMBER","");
       // str=str.Replace("DNN","");
       // str=str.Replace("PO_TO_ADDRESS","");
       // str=str.Replace("BTS_FLAG","");
       // str=str.Replace("PID","");
       // str=str.Replace("SHIPPING_FLAG","");
       
       // str=str.Replace("SUB_MP_FLAG","");
       // str=str.Replace("WO_ID","");
       // str=str.Replace("PO_CONFIRM_RESULT_ID","");
       // str=str.Replace("COUNTRY_ISO_CODE","");
       // str=str.Replace("INDUSTRIAL_FLAG","");
       // str=str.Replace("MPK_AVG_WEIGHT","");
       // str = str.Replace("AVG_WEIGHT", "");
       // str=str.Replace("IS_CELL_ORDER","");
       // str = str.Replace("PO_QTY", "");
       // str = str.Replace("PO", "");
 
       // string sysdatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss_fffZ");
       // string[] sArray = str.Split('}');


       // try
       // {
       //     SEQID = 100001;

       //     foreach (string i in sArray)
       //     {



       //         if (i.Length > 8)
       //         {
       //             string[] SA = i.Split(',');


       //             DataTable T1 = PDataBaseOperation.PSelectSQLDT("ORACLE", Writbl, "select count( *) ct  from publib.WORK_ORDER_L10  where ORDER_NUMBER='" + SA[0].ToString () + "'");
       //             if (T1.Rows[0]["ct"].ToString().Equals("0"))
       //             {
       //                 sqlstr = " insert into publib.WORK_ORDER_L10 ( ORDER_NUMBER,FAMILY_ID,MODEL_CODE,PCF_VERSION,QUANTITY,CREATE_TIME,AUTHOR,DESCRIPTION,ORDER_TYPE,";
       //                 sqlstr = sqlstr + " ORDER_PRIORITY,START_QTY,READY_QTY,INPUT_QTY,ORDER_STATE,LINE_ID,EXCEPTION_FLAG,EXCEPTION_COUNT,COMPLETE_TIME,PALLET_MASTERPACK_COUNT,";
       //                 sqlstr = sqlstr + " MASTERPACK_UNIT_COUNT,LOSS_QTY,UNBIND_QTY,CURRENT_MASTERPACK_NUMBER,CURRENT_PALLET_NUMBER,PO,PO_QTY,DNN,PO_TO_ADDRESS,BTS_FLAG,PID,";
       //                 sqlstr = sqlstr + " SHIPPING_FLAG,AVG_WEIGHT,SUB_MP_FLAG,WO_ID,PO_CONFIRM_RESULT_ID,COUNTRY_ISO_CODE,INDUSTRIAL_FLAG,MPK_AVG_WEIGHT,IS_CELL_ORDER,DocumentID ,SeqID) values ('";
                       
       //                 sqlstr = sqlstr + SA[0] + "','";
       //                 sqlstr = sqlstr + SA[1] + "','";
       //                 sqlstr = sqlstr + SA[2] + "','";
       //                 sqlstr = sqlstr + SA[3] + "','";
       //                 sqlstr = sqlstr + SA[4] + "','";
       //                 sqlstr = sqlstr + SA[5] + "','";
       //                 sqlstr = sqlstr + SA[6] + "','";
       //                 sqlstr = sqlstr + SA[7] + "','";
       //                 sqlstr = sqlstr + SA[8] + "','";
       //                 sqlstr = sqlstr + SA[9] + "','";
       //                 sqlstr = sqlstr + SA[10] + "','";
       //                 sqlstr = sqlstr + SA[11] + "','";
       //                 sqlstr = sqlstr + SA[12] + "','";
       //                 sqlstr = sqlstr + SA[13] + "','";
       //                 sqlstr = sqlstr + SA[14] + "','";
       //                 sqlstr = sqlstr + SA[15] + "','";
       //                 sqlstr = sqlstr + SA[16] + "','";
       //                 sqlstr = sqlstr + SA[17] + "','";
       //                 sqlstr = sqlstr + SA[18] + "','";
       //                 sqlstr = sqlstr + SA[19] + "','";
       //                 sqlstr = sqlstr + SA[20] + "','";

       //                 sqlstr = sqlstr + SA[21] + "','";
       //                 sqlstr = sqlstr + SA[22] + "','";
       //                 sqlstr = sqlstr + SA[23] + "','";
       //                 sqlstr = sqlstr + SA[24] + "','";
       //                 sqlstr = sqlstr + SA[25] + "','";
       //                 sqlstr = sqlstr + SA[26] + "','";
       //                 sqlstr = sqlstr + SA[27] + "','";
       //                 sqlstr = sqlstr + SA[28] + "','";
       //                 sqlstr = sqlstr + SA[29] + "','";
       //                 sqlstr = sqlstr + SA[30] + "','";
       //                 sqlstr = sqlstr + SA[31] + "','";
       //                 sqlstr = sqlstr + SA[32] + "','";
       //                 sqlstr = sqlstr + SA[33] + "','";
       //                 sqlstr = sqlstr + SA[34] + "','";
       //                 sqlstr = sqlstr + SA[35] + "','";
       //                 sqlstr = sqlstr + SA[36] + "','";
       //                 sqlstr = sqlstr + SA[37] + "','";
       //                 sqlstr = sqlstr + SA[38] + "','";

       //                 sqlstr = sqlstr + tDocumentID + "','";
       //                 sqlstr = sqlstr + SEQID + "' )";

       //                 SEQID = SEQID + 1;




       //                 int I = PDataBaseOperation.PExecSQL("ORACLE", Writbl, sqlstr);
       //             }
       //             else
       //             {

       //                 //select order_state   from PUBLIB.WORK_ORDER_L10 where  order_state='5'
       //                 int II = PDataBaseOperation.PExecSQL("ORACLE", Writbl, "update  publib.WORK_ORDER_L10  set READY_QTY ='"
       //                     + SA[11] + "'  , order_state ='" + SA[13] + "'  where ORDER_NUMBER='" + SA[0].ToString() + "'");
                    
                    
       //             }
       //             }
       //         }
       //     }

             

        

       // catch (Exception ex)
       // {

       //     string err_str = ex.Message;

       // }
        return ("");
    }





    public static string GetSCRAP(string Readtbl, string Writbl, string CUSTOMER, string WERKS, string START_DATE, string END_DATE)
    {//ZRFC_WO_SCRAP_0001
        string sss1;
        sss1 = "";
        try
        {


            //             ?入:import ：CUSTOMER 客戶
            //          WERKS：工廠
            //          START_DATE(必?)   開始時間
            //          END_DATE(必?)         結束時間
            //?出：tables  OUTPUT

            // DataBaseOperation dboRead  = new DataBaseOperation("ORACLE", dboReadS);
            // DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", dboWriteS);

            SAPConnection con = new SAPConnection("ASHOST=10.134.28.98;  CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh");

            //SAPConnection con = new SAPConnection("ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh");

            con.Open();
            SAPCommand cmd = new SAPCommand(con);
            cmd.CommandText = "EXEC  ZRFC_WO_SCRAP_0001  @CUSTOMER=@CUSTOMERV,  @WERKS=@WERKSV,@START_DATE=@START_DATEV,@END_DATE=@END_DATEV , @OUTPUT=@OUTPUTT output ";


            SAPParameter CUSTOMERV = new SAPParameter("@CUSTOMERV", ParameterDirection.Input);
            CUSTOMERV.Value = CUSTOMER;//"WBBB"
            cmd.Parameters.Add(CUSTOMERV);


            SAPParameter WERKSV = new SAPParameter("@WERKSV", ParameterDirection.Input);
            WERKSV.Value = WERKS;// "20150615";
            cmd.Parameters.Add(WERKSV);


            SAPParameter START_DATEV = new SAPParameter("@START_DATEV", ParameterDirection.Input);
            START_DATEV.Value = START_DATE;// "14";
            cmd.Parameters.Add(START_DATEV);


            SAPParameter END_DATEV = new SAPParameter("@END_DATEV", ParameterDirection.Input);
            END_DATEV.Value = END_DATE;
            cmd.Parameters.Add(END_DATEV);



            SAPParameter OUTPUTT = new SAPParameter("@OUTPUTT", ParameterDirection.InputOutput);
            cmd.Parameters.Add(OUTPUTT);

            SAPDataReader dr = cmd.ExecuteReader();

            System.Data.DataTable SAPdt2 = (System.Data.DataTable)cmd.Parameters["@OUTPUTT"].Value;



            string sql_insert_str;


            if (SAPdt2.Rows.Count > 0)
            {
                SEQID = 100001;
                for (int y = 0; y < SAPdt2.Rows.Count; y++)
                {
                    sql_insert_str = "insert into SAP.S_WO_SCRAP_0001 (ddate,customer,process,project,maabc,scarpmoney,discardmoney,totalmoney,exceedmoney,inputmoney,scarprate,scarpstandard,difference,SEQID,Documentid) values (";








                    sql_insert_str = sql_insert_str + "'" + START_DATE + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["customer"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["process"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["project"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["maabc"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["scarpmoney"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["discardmoney"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["totalmoney"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["exceedmoney"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["inputmoney"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["scarprate"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["scarpstandard"].ToString() + "','";
                    sql_insert_str = sql_insert_str + SAPdt2.Rows[y]["difference"].ToString() + "','";

                    sql_insert_str = sql_insert_str + SEQID.ToString() + "','";
                    sql_insert_str = sql_insert_str + tDocumentID + "')";

                    int iii = PDataBaseOperation.PExecSQL("oracle", Writbl, sql_insert_str);
                    SEQID = SEQID + 1;

                }


            }


        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            return "False";
        }

        return "true";
    }



}  
}  


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       