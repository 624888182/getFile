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
using SFC.TJWEB;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using System.Linq;
using System.Web.UI;


namespace SFC.TJWEB
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class ChiMeilib
{
    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd"); 
    static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    DeLinkPidlib DeLinkPidlibPointer = new DeLinkPidlib();
    DeLinkPidlib3 DeLinkPidlib3Pointer = new DeLinkPidlib3();
    // FSplitlib FSplitlibPointer = new FSplitlib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    string DBType = "oracle";
    protected string Backdbstring = ConfigurationManager.AppSettings["NormalBakupConnectionString"];


    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");

    public string CMDelUpdate(string ptype, string dbtype, string DBRead, string DBWri, string daycnts)
    {
        string Ret1 = "", sql1 = "", tmpsqlW = "", sp = "", sp0 = "0", tpo="", tdn="", tmpselwri = "";
        DataSet dt1 = null, dt2 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5=0, v6=0, v7=0, daycnt = Convert.ToInt32(daycnts);
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "";
        Decimal d1=0, d2=0, d3=0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(daycnt).ToString("yyyyMMdd"); 

        // Get PO
        sql1 = " SELECT  PO, CDATE, DDATE  from PUBLIB.CPOM1 "
             + " where substr(CDATE, 1, 8 )  >= '" + tmp1Date + "' "
             + " and   substr(CDATE, 1, 8 )  <= '" + tmpDate + "'  "
             + " order by PO desc ";
        // sap.cmcs_sfc_packing_lines_all ";
        //     // + "  AND to_char((last_update_date),'yyyyMMdd') >= '20101231' "
        //     // + "      to_char((last_update_date),'yyyyMMdd') <= '20110431'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        // if (v3 <= 0) return (Ret1);

        int arrPOlong = v3, arrPOwidth = 20;
        string[,] arrayPO = new string[arrPOwidth + 1, arrPOlong + 1];

        for (v1 = 0; v1 < arrPOlong; v1++)
            for (v2 = 0; v2 < arrPOwidth; v2++)
                arrayPO[v2, v1] = "";

        for (v1 = 0; v1 < arrPOlong; v1++)
        {
            arrayPO[15, v1 + 1] = "Y";  // write flag
            arrayPO[1,  v1 + 1] = dt1.Tables[0].Rows[v1]["PO"].ToString();  // PO
            arrayPO[16, v1 + 1] = dt1.Tables[0].Rows[v1]["CDATE"].ToString();  // CPOM1- first-time
            arrayPO[17, v1 + 1] = dt1.Tables[0].Rows[v1]["DDATE"].ToString();  // CPOM1
        }
      
        // Check need update
        // for (v1 = arrPOlong; v1 < arrPOlong; v1++)  // Run from head
        for (v1 = 0; v1 < arrPOlong; v1++)
        {
            if (arrayPO[16, v1 + 1] != "")  // not-first-time
            {
                tpo = arrayPO[1, v1 + 1]; //  = dt1.Tables[0].Rows[v1]["PO"].ToString();  // PO
                tmpselwri = "select to_char( max(ship_date), 'yyyyMMddHHmmssmm') ship_date from SAP.CMCS_SFC_PACKING_LINES_ALL where customer_po = '" + tpo + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 > 0)
                {
                    arrayPO[18, v1 + 1] = dt2.Tables[0].Rows[0]["ship_date"].ToString();
                    tmp1 = arrayPO[17, v1 + 1];
                    tmp2 = arrayPO[18, v1 + 1];
                    if ((arrayPO[18, v1 + 1] == "") || (arrayPO[17, v1 + 1] == arrayPO[18, v1 + 1]))
                        arrayPO[15, v1 + 1] = "N";
                }
            } // end not-first-time
        }

        v7 = 0;

        // for (v1 = arrPOlong; v1 < arrPOlong; v1++)  // from head
        for (v1 = 0; v1 < arrPOlong; v1++)
        {

            if ((  arrayPO[15, v1 + 1] == "Y" ) || ( arrayPO[16, v1 + 1] == "" ) ) // need update or first-time
            {
            d1 = 0;
            t01 = arrayPO[1, v1 + 1];

            sql1 = " SELECT a.QUANTITY  QUANTITY, to_char(a.creation_date, 'yyyyMMdd') cdate,  to_char(a.ship_date, 'yyyyMMddHHmmssmm') ddate, "
            + " a.ship_to_country  ship_to_country, a.project  project , a.plant  plant, a.ship_to_customername ship_to_customername ,  "
            + "  a.customer_po  customer_po,    a.item_number item_number,  substr(a.item_number,3,3) model  "
            + "  from sap.cmcs_sfc_packing_lines_all a  where a.customer_po = '" + t01 + "' order by a.ship_date desc ";
            
            dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
            v3 = dt2.Tables[0].Rows.Count;
            if (v3 > 0)
            {
                d2 = 0;
                tmp1 = "";
                for (v4 = 0; v4 < v3; v4++)
                {
                    if (v4 == 0) // first time
                    {
                        // t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                        t03 = dt2.Tables[0].Rows[v4]["cdate"].ToString();
                        t04 = dt2.Tables[0].Rows[v4]["ddate"].ToString();
                        t05 = dt2.Tables[0].Rows[v4]["ship_to_country"].ToString();
                        t06 = dt2.Tables[0].Rows[v4]["plant"].ToString();
                        t07 = dt2.Tables[0].Rows[v4]["project"].ToString();
                        t08 = dt2.Tables[0].Rows[v4]["ship_to_customername"].ToString();
                        t09 = dt2.Tables[0].Rows[v4]["customer_po"].ToString();
                        t10 = dt2.Tables[0].Rows[v4]["item_number"].ToString();
                        t11 = dt2.Tables[0].Rows[v4]["model"].ToString();
                        // t12 = dt2.Tables[0].Rows[v4]["color"].ToString();                      
                    }

                    t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                    if (t02 != "") d2 = Convert.ToDecimal(t02);
                    d1 = d1 + d2;

                }

                t21 = d1.ToString();

                sql1 = " SELECT  b.color color, b.SW_VER SW_VER "
                + "  from sap.cmcs_sfc_packing_lines_all a,  shp.ROS_TCH_PN  b  where a.customer_po = '" + t01 + "' "
                + "  and  a.item_number = b.ppart   ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t12  = dt2.Tables[0].Rows[0]["color"].ToString();
                    t121 = dt2.Tables[0].Rows[0]["SW_VER"].ToString();
                }

                sql1 = " SELECT  customer_name brand "
                + "  from sfc.cmcs_sfc_model where substr(model,1,3)  = '" + t11 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t13 = dt2.Tables[0].Rows[0]["brand"].ToString();
                }

                tmpsqlW = "Update PUBLIB.CPOM1 set DELQUANTITY = '" + t21 + "', DDATE = '" + t04 + "', SITE = '" + t06 + "', BU = '" + t06 + "',  "
                + " CUSTOMER =  '" + t08 + "', SWVERSION =  '" + t121 + "', "
                + " CDATE = '" + t03 + "', COUNTRY = '" + t05 + "',  BRAND =  '" + t13 + "',"
                + " PO = '" + t09 + "', FLAG3 = '" + sp0 + "',  PROJ =  '" + t11 + "', COLOR =  '" + t12 + "', BOMPARTNO =  '" + t10 + "' "
                + " where PO  = '" + t01 + "'   ";
                v5 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                if (v5 <= 0) v5++;
                v7++;

                t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = ""; t12 = ""; t13 = "";

            } // end if ( arrayPO[15, v1 + 1] == "Y" )  // need update
            }  // end for 

        }


  //      return (Ret1);

        // Get DN
        sql1 = " SELECT  DN from PUBLIB.CDNM1 "
             + " where substr(CDATE, 1, 8 )  >= '" + tmp1Date + "' "
             + " and   substr(CDATE, 1, 8 )  <= '" + tmpDate + "'  "           
             + " order by DN desc ";
        // sap.cmcs_sfc_packing_lines_all ";
        //     // + "  AND to_char((last_update_date),'yyyyMMdd') >= '20101231' "
        //     // + "      to_char((last_update_date),'yyyyMMdd') <= '20110431'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);

        int arrDNlong = v3, arrDNwidth = 20;
        string[,] arrayDN = new string[arrDNwidth + 1, arrDNlong + 1];

        for (v1 = 0; v1 < arrDNlong; v1++)
            for (v2 = 0; v2 < arrDNwidth; v2++)
                arrayDN[v2, v1] = "";

        for (v1 = 0; v1 < arrDNlong; v1++)
        {
            arrayDN[1, v1 + 1] = dt1.Tables[0].Rows[v1]["DN"].ToString();  // PO
        }

        v7=0;
        for (v1 = 0; v1 < arrDNlong; v1++)
        {
            d1 = 0;
            t01 = arrayDN[1, v1 + 1];

             sql1 = " SELECT a.QUANTITY  QUANTITY, to_char(a.creation_date, 'yyyyMMdd') cdate,  to_char(a.ship_date, 'yyyyMMddHHmmssmm') ddate, "
             + " a.ship_to_country  ship_to_country, a.project  project , a.plant  plant, a.ship_to_customername ship_to_customername ,  "
             + "  a.customer_po  customer_po,    a.item_number item_number,  substr(a.item_number,3,3) model  "
             + "  from sap.cmcs_sfc_packing_lines_all a  where a.invoice_number = '" + t01 + "' ";
            

            // sql1 = " SELECT a.QUANTITY  QUANTITY, to_char(a.creation_date, 'yyyyMMdd') cdate,  to_char(a.ship_date, 'yyyyMMddHHmmssmm') ddate, "
            // + " a.ship_to_country  ship_to_country, a.project  project , a.plant  plant, a.ship_to_customername ship_to_customername ,  "
            // + "  a.customer_po  customer_po,    a.item_number item_number,  substr(a.item_number,3,3) model, b.color color "
            // + "  from sap.cmcs_sfc_packing_lines_all a,  shp.ROS_TCH_PN  b  where a.invoice_number = '" + t01 + "' "
            // + "  and  a.item_number = b.ppart   ";
            dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
            v3 = dt2.Tables[0].Rows.Count;
            if (v3 > 0)
            {
                d2 = 0;
                for (v4 = 0; v4 < v3; v4++)
                {
                    if (v4 == 0) // first time
                    {
                        // t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                        t03 = dt2.Tables[0].Rows[v4]["cdate"].ToString();
                        t04 = dt2.Tables[0].Rows[v4]["ddate"].ToString();
                        t05 = dt2.Tables[0].Rows[v4]["ship_to_country"].ToString();
                        t06 = dt2.Tables[0].Rows[v4]["plant"].ToString();
                        t07 = dt2.Tables[0].Rows[v4]["project"].ToString();
                        t08 = dt2.Tables[0].Rows[v4]["ship_to_customername"].ToString();
                        t09 = dt2.Tables[0].Rows[v4]["customer_po"].ToString();
                        t10 = dt2.Tables[0].Rows[v4]["item_number"].ToString();
                        t11 = dt2.Tables[0].Rows[v4]["model"].ToString();
                        // t12 = dt2.Tables[0].Rows[v4]["color"].ToString();
                    }

                    t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                    if (t02 != "") d2 = Convert.ToDecimal(t02);
                    d1 = d1 + d2;                  

                }

                t21 = d1.ToString();

                sql1 = " SELECT  b.color color, b.SW_VER SW_VER "
                + "  from sap.cmcs_sfc_packing_lines_all a,  shp.ROS_TCH_PN  b  where a.invoice_number = '" + t01 + "' "
                + "  and  a.item_number = b.ppart   ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t12 = dt2.Tables[0].Rows[0]["color"].ToString();
                    t121 = dt2.Tables[0].Rows[0]["SW_VER"].ToString();
                }

                sql1 = " SELECT  customer_name brand "
                + "  from sfc.cmcs_sfc_model where substr(model,1,3)  = '" + t11 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t13 = dt2.Tables[0].Rows[0]["brand"].ToString();
                }

                tmpsqlW = "Update PUBLIB.CDNM1 set DELQUANTITY = '" + t21 + "', DDATE = '" + t04 + "', SITE = '" + t06 + "', BU = '" + t06 + "',  "
                + " CUSTOMER =  '" + t08 + "', SWVERSION =  '" + t121 + "', "
                + " CDATE = '" + t03 + "', COUNTRY = '" + t05 + "',  BRAND =  '" + t13 + "',"
                + " PO = '" + t09 + "', FLAG3 = '" + sp0 + "',  PROJ =  '" + t11 + "', COLOR =  '" + t12 + "', BOMPARTNO =  '" + t10 + "' "
                + " where DN  = '" + t01 + "'   ";
                v5 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                if (v5 <= 0) v5++;
                v7++;

                t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = ""; t12 = ""; t13="";
            }  // end for 

        }


        return (Ret1);        
       
    }    


    /////////////////
    // select  distinct(customer_po), ship_date   from SAP.CMCS_SFC_PACKING_LINES_ALL  where SHIP_DATE in ( SELECT max( SHIP_DATE )
    // FROM SAP.CMCS_SFC_PACKING_LINES_ALL  group by customer_po ) order  by customer_po desc 
    ////////////////
    public string CMDelPOData(string ptype, string dbtype, string DBRead, string DBWri, string daycnts)  
    {
        string Ret1 = "", sql1 = "", tmpsqlW="", sp="", sp0="0", tmpselwri = "";
        DataSet dt1 = null, dt2 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5=0, v6=0, v7=0, preday = Convert.ToInt32(daycnts);
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string tmpDate  = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays( preday ) .ToString("yyyyMMdd"); 
        // GET DN
        sql1 = " SELECT  distinct( INVOICE_NUMBER)  from sap.cmcs_sfc_packing_lines_all   "
              + " where to_char((creation_date),'yyyyMMdd') >= '" + tmp1Date + "' "
              + " and   to_char((creation_date),'yyyyMMdd') <= '" + tmpDate + "'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        for (v1 = 0; v1 < v3; v1++)
        {
            //t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString("yyyyMMddHHmmssmm");   
            //  t01 = Convert.ToDateTime(dt1.Tables[0].Rows[v1]["emeidate"].ToString()).ToString("yyyyMMddHHmmssmm");
            t01 = dt1.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString();  // PO

            if ((t01 != "") && (t01 != ""))
            {
                tmpselwri = "select * from  PUBLIB.CDNM1 where DN =  '" + t01 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 <= 0) // not int 221
                {
                    tmpsqlW = "Insert into PUBLIB.CDNM1 ( DN, FLAG1 , FLAG2, FLAG3, FLAG4) Values "
                       + " ( '" + t01 + "', '" + sp + "', '" + sp + "', '" + sp + "', '" + sp + "' ) ";
                    v4 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                    if (v4 <= 0) v5++;
                }

            }
            t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = "";
            t12 = ""; t13 = ""; t14 = ""; t15 = "";
        }

        // return (Ret1);

         // GET CUSTOMER_PO
        sql1 = " SELECT  distinct(CUSTOMER_PO) from sap.cmcs_sfc_packing_lines_all "
             + " where to_char((creation_date),'yyyyMMdd') >= '" + tmp1Date + "' "
             + " and to_char((creation_date),'yyyyMMdd') <= '" + tmpDate + "'  ";
        //     // + "  AND to_char((last_update_date),'yyyyMMdd') >= '20101231' "
        //     // + "      to_char((last_update_date),'yyyyMMdd') <= '20110431'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        // if (v3 <= 0)  return (Ret1);
        v5 = 0;
        for (v1 = 0; v1 < v3; v1++)
        {
            //t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString("yyyyMMddHHmmssmm");   
            //  t01 = Convert.ToDateTime(dt1.Tables[0].Rows[v1]["emeidate"].ToString()).ToString("yyyyMMddHHmmssmm");
            t01 = dt1.Tables[0].Rows[v1]["CUSTOMER_PO"].ToString();  // PO
            
            if ((t01 != "") && (t01 != ""))
            {
                tmpselwri = "select * from  PUBLIB.CPOM1 where PO =  '" + t01 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 <= 0) // not int 221
                {
                    tmpsqlW = "Insert into PUBLIB.CPOM1 ( PO, FLAG1 , FLAG2, FLAG3, FLAG4) Values "
                       + " ( '" + t01 + "', '" + sp + "', '" + sp + "', '" + sp + "', '" + sp + "' ) ";
                    v4 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                    if (v4 <= 0) v5++;
                }

            }
            t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = "";
            t12 = ""; t13 = ""; t14 = ""; t15 = "";
        }

        // return (Ret1);
       

        // Get EMEI

//        sql1 = " SELECT  to_char(a.last_update_date, 'yyyyMMddHHmmssmm') emeidate, to_char(b.last_update_date, 'yyyyMMddHHmmssmm') packdate, "
//       + " to_char(e.DDATE, 'yyyyMMddHHmmssmm') DDATE, 'FIHMLXTJ' site,  "
//       + " a.imeinum imei, b.customer_po po, b.invoice_number dn, "
//       + " to_char(b.last_update_date, 'yyyyMMdd') datetime, c.customer_name brand, "
//       + " b.ship_to_customername customer, c.model project, "
//       + " b.ship_to_country country, '' sw, '1' quantity, d.color color, "
//       + " '' charger, '' bompartno, a.product_id pid, '' flag1, '' flag2, '' flag3   "
//       + " FROM shp.cmcs_sfc_imeinum a,  "
//       + " sap.cmcs_sfc_packing_lines_all b, "
//       + " sfc.cmcs_sfc_model c, "
//       + " shp.ros_tch_pn d, "
//       + " shp.cmcs_sfc_shipping_data e "
//       + " WHERE a.imeinum = e.imei "
//       + "    AND e.carton_no = b.internal_carton "
//       + " AND e.model = c.model "
//       + "  AND a.ppart = d.ppart "
//       + "  AND e.DDATE >= to_date('20101215', 'yyyyMMdd') "
//       + "  AND e.DDATE <= to_date('20110415', 'yyyyMMdd') ";
//       //+ "  AND to_char((e.DDATE),'yyyyMMdd') >= '20101231' "
//       //+ "  AND to_char((e.DDATE),'yyyyMMdd') <= '20110431'  ";
       
        sql1 = " SELECT  to_char(a.last_update_date, 'yyyyMMddHHmmssmm') emeidate, to_char(b.last_update_date, 'yyyyMMddHHmmssmm') packdate, "
     + " to_char(e.DDATE, 'yyyyMMddHHmmssmm') DDATE, 'FIHMLXTJ' site,  "
     + " a.imeinum imei, b.customer_po po, b.invoice_number dn, "
     + " to_char(b.last_update_date, 'yyyyMMdd') datetime, c.customer_name brand, "
     + " b.ship_to_customername customer, c.model project, "
     + " b.ship_to_country country,  d.SW_VER swversion, '1' quantity, d.color color, "
     + " '' charger, '' bompartno, a.product_id pid, '' flag1, '' flag2, '' flag3   "
     + " FROM shp.cmcs_sfc_imeinum a,  "
     + " sap.cmcs_sfc_packing_lines_all b, "
     + " sfc.cmcs_sfc_model c, "
     + " shp.ros_tch_pn d, "
     + " shp.cmcs_sfc_shipping_data e "
     + " WHERE a.imeinum = e.imei "
     + " and e.carton_no = b.internal_carton "
     + " and e.model = c.model "
     + " and a.ppart = d.ppart "
     + " and to_char((b.CREATION_DATE),'yyyyMMdd') >= '" + tmp1Date + "' "
     + " and to_char((b.CREATION_DATE),'yyyyMMdd') <= '" + tmpDate + "'  ";
     //+ "  AND e.DDATE >= to_date('20130115', 'yyyyMMdd') "
     //+ "  AND e.DDATE <= to_date('20130425', 'yyyyMMdd') ";
        //+ "  AND to_char((e.DDATE),'yyyyMMdd') >= '20101231' "
        //+ "  AND to_char((e.DDATE),'yyyyMMdd') <= '20110431'  ";

        // -- AND a.imeinum in ('356384041593821')
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        if (v3 <= 0) return (Ret1);
        t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = ""; t12 = ""; t13 = ""; t14 = ""; t15 = "";
        v5 = 0;
        for (v1 = 0; v1 < v3; v1++)
        {
            //t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString("yyyyMMddHHmmssmm");   
            //  t01 = Convert.ToDateTime(dt1.Tables[0].Rows[v1]["emeidate"].ToString()).ToString("yyyyMMddHHmmssmm");
            t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString();
            t02 = dt1.Tables[0].Rows[v1]["packdate"].ToString();
            t03 = dt1.Tables[0].Rows[v1]["DDATE"].ToString();
            t04 = dt1.Tables[0].Rows[v1]["site"].ToString();
            t05 = dt1.Tables[0].Rows[v1]["imei"].ToString();
            t06 = dt1.Tables[0].Rows[v1]["po"].ToString();
            t07 = dt1.Tables[0].Rows[v1]["dn"].ToString();
            t08 = dt1.Tables[0].Rows[v1]["datetime"].ToString();
            t09 = dt1.Tables[0].Rows[v1]["brand"].ToString();
            t10 = dt1.Tables[0].Rows[v1]["customer"].ToString();
            t11 = dt1.Tables[0].Rows[v1]["project"].ToString();
            t12 = dt1.Tables[0].Rows[v1]["country"].ToString();
            t13 = dt1.Tables[0].Rows[v1]["color"].ToString();
            t14 = dt1.Tables[0].Rows[v1]["pid"].ToString();
            t15 = dt1.Tables[0].Rows[v1]["swversion"].ToString();




            if ((t05 != "") && (t06 != ""))
            {
                tmpselwri = "select * from  PUBLIB.CEMEI1 where IMEI =  '" + t05 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 <= 0) // not int 221
                {
                    tmpsqlW = "Insert into PUBLIB.CEMEI1 ( RDATE, DDATE, SITE, BU, IMEI, PO, DN, CDATE, PID, FLAG1 , FLAG2, FLAG3, FLAG4, COUNTRY, SWVERSION ) "
                    + " Values ( '" + sp + "', '" + t03 + "', '" + t04 + "', '" + t04 + "', '" + t05 + "',  "
                    + "  '" + t06 + "', '" + t07 + "', '" + t08 + "', '" + t14 + "', '" + sp + "',  "
                    + "  '" + sp + "', '" + sp0 + "', '" + sp + "' , '" + t12 + "' , '" + t15 + "' ) ";
                    v4 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                    if (v4 <= 0) v5++;
                }

            }


            t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = "";
            t12 = ""; t13 = ""; t14 = ""; t15 = "";

            // if (v1 >= limno) v1 = v3; // break
        }
        

        return (Ret1);
    } 


}  // end public class ChiMeilib 
}  // end namespace SFC.TJWEB


