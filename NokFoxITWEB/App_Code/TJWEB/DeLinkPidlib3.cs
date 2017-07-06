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
using SFC.TJWEB;
using EconomyUser;
using System.Linq;


/// <summary>
///test 的摘要说明
/// </summary>
public class DeLinkPidlib3
{
    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd");
    static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    // FSplitlib FSplitlibPointer = new FSplitlib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    string DBType = "oracle";


    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    public DeLinkPidlib3()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public string GetModelData(string P1, string DBType, string DBWriString, string PID)
    {
        if ((P1 != "P1") || (DBType != "oracle") || (DBWriString == "") || (PID == "")) return "";

        int v1 = PID.Length;
        if (v1 < 5) return (""); 
        
        string pidHead = PID.Substring(0, 3);
        string model = "";
        //從CustomerModel中取出機種及相應PIDHEAD
        string strmodel = "select model,pidhead from PUBLIB.CUSTOMERMODEL where pidHead='" + pidHead + "'";
        DataSet dsstmode = DataBaseOperation.SelectSQLDS(DBType, DBWriString, strmodel);
        if (dsstmode.Tables[0].Rows.Count > 0)
        {
            model = dsstmode.Tables[0].Rows[0]["MODEL"].ToString();
        }
        else
        {
            model = pidHead;
        }
        return model;
    }//GetModelData

    public string GetRoutingNO(string P1, string DBType, string DBReadString, string DBWriString, string DBReadString1, string PID, ref string PART, ref string model)
    {
        if ((P1 != "P1") || (DBType != "oracle") || (DBReadString == "") || (PID == "")) return "";

        if (DBWriString == "") DBWriString = DBReadString;
        if (DBReadString1 == "") DBReadString1 = DBReadString;

        string wono = "", RoutingNO = "";

        if (PART.Trim() == "")
        {
            string linename = "", lstation_id = "", status = "", level = "", tablename = "", linetime = "", customer = "";
            wono = GetWO_NO("P1", DBType, DBReadString, DBReadString1, PID, ref PART, ref linename, ref lstation_id, ref  status, ref  level, ref  tablename, ref  linetime, ref  customer, ref  model);
        }
        if (model.Trim() == "")
        {
            model = GetModelData("P1", DBType, DBWriString, PID);
        }

        //先根據料號找路由

        string dbstr2 = "select distinct ROUTING_SEQUENCE_ID from  SFC.SFC_ROUTING_HEADERS" +
                        " where model_name='" + model + "'" +
                        " and (PART_NUMBER='' or PART_NUMBER is null)";
        DataSet dsst4 = DataBaseOperation.SelectSQLDS(DBType, DBReadString, dbstr2);
        if (dsst4.Tables[0].Rows.Count > 0)
        {
            RoutingNO = dsst4.Tables[0].Rows[0]["ROUTING_SEQUENCE_ID"].ToString();
        }
        else
        {
            string dbstr1 = "select distinct ROUTING_SEQUENCE_ID from  SFC.SFC_ROUTING_HEADERS" +
                            " where PART_NUMBER='" + PART + "'" +
                            " and model_name='" + model + "'";
            DataSet dsst3 = DataBaseOperation.SelectSQLDS(DBType, DBReadString, dbstr1);
            if (dsst3.Tables[0].Rows.Count > 0)
            {
                RoutingNO = dsst3.Tables[0].Rows[0]["ROUTING_SEQUENCE_ID"].ToString();
            }
        }
        return RoutingNO;
    }//GetRoutingNO

    public string GetWO_NO(string P1, string DBType, string DBReadString, string DBReadString1, string PID, ref string PART, ref string linename, ref string lstation_id, ref string status, ref string level, ref string tablename, ref string linetime, ref string customer, ref string model)
    {

        if ((P1 != "P1") || (DBType != "oracle") || (DBReadString == "") || (PID == "")) return "";
        if (DBReadString1 == "") DBReadString1 = DBReadString;
        string workno = "", ptable = "", fpart = "", fwono = "", ptab = "";
        ArrayList creation_date = new ArrayList();
        ArrayList wo_no = new ArrayList();
        ArrayList tabname = new ArrayList();
        ArrayList lstation = new ArrayList();
        ArrayList Line_Name = new ArrayList();
        ArrayList parttable = new ArrayList();
        string strPCBA = "SELECT CREATION_DATE, WO_NO, STATION_ID,LINE_NAME from  SFC.MES_PCBA_WIP  where PRODUCT_ID = '" + PID + "'";
        DataSet PCBA = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strPCBA);
        if (PCBA.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(PCBA.Tables[0].Rows[0][0].ToString());
            wo_no.Add(PCBA.Tables[0].Rows[0][1].ToString());
            tabname.Add("SFC.MES_PCBA_WIP");
            lstation.Add(PCBA.Tables[0].Rows[0][2].ToString());
            Line_Name.Add(PCBA.Tables[0].Rows[0][3].ToString());
            parttable.Add("SHP.CMCS_SFC_SORDER");
        }
        else//可能備份到215上
        {
            strPCBA = "select CREATION_DATE,WO_NO, STATION_ID,LINE_NAME from SFC.MES_PCBA_WIP" +
                                    " where product_id='" + PID + "'";
            PCBA = DataBaseOperation.SelectSQLDS(DBType, DBReadString1, strPCBA);
            if (PCBA.Tables[0].Rows.Count > 0)
            {
                creation_date.Add(PCBA.Tables[0].Rows[0][0].ToString());
                wo_no.Add(PCBA.Tables[0].Rows[0][1].ToString());
                tabname.Add("SFC.MES_PCBA_WIP");
                lstation.Add(PCBA.Tables[0].Rows[0][2].ToString());
                Line_Name.Add(PCBA.Tables[0].Rows[0][3].ToString());
                parttable.Add("SHP.CMCS_SFC_SORDER");
            }
        }

        string strASSY = "SELECT CREATION_DATE, WO_NO , STATION_ID,LINE_NAME from  SFC.MES_ASSY_WIP  where PRODUCT_ID = '" + PID + "'";
        DataSet ASSY = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strASSY);
        if (ASSY.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(ASSY.Tables[0].Rows[0][0].ToString());
            wo_no.Add(ASSY.Tables[0].Rows[0][1].ToString());
            tabname.Add("SFC.MES_ASSY_WIP");
            lstation.Add(ASSY.Tables[0].Rows[0][2].ToString());
            Line_Name.Add(ASSY.Tables[0].Rows[0]["LINE_NAME"].ToString());
            parttable.Add("SHP.CMCS_SFC_AORDER");
        }

        string strPACK = "SELECT CREATION_DATE, WO_NO , STATION_ID,LINE_NAME from  SFC.MES_PACK_WIP  where PRODUCT_ID = '" + PID + "'";
        DataSet PACK = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strPACK);
        if (PACK.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(PACK.Tables[0].Rows[0][0].ToString());
            wo_no.Add(PACK.Tables[0].Rows[0][1].ToString());
            tabname.Add("SFC.MES_PACK_WIP");
            lstation.Add(PACK.Tables[0].Rows[0][2].ToString());
            Line_Name.Add(PACK.Tables[0].Rows[0][3].ToString());
            parttable.Add("SHP.CMCS_SFC_PORDER");
        }



        string strHISTORY = "SELECT CREATION_DATE, WO_NO , STATION_ID,LINE_NAME from  SFC.MES_PCBA_HISTORY  where PRODUCT_ID = '" + PID + "'" +
                            " order by CREATION_DATE desc";
        DataSet HISTORY = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strHISTORY);
        if (HISTORY.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(HISTORY.Tables[0].Rows[0][0].ToString());
            wo_no.Add(HISTORY.Tables[0].Rows[0][1].ToString());
            tabname.Add("SFC.MES_PCBA_HISTORY");
            lstation.Add(HISTORY.Tables[0].Rows[0][2].ToString());
            Line_Name.Add(HISTORY.Tables[0].Rows[0][3].ToString());
            parttable.Add("SHP.CMCS_SFC_SORDER");
        }
        if (creation_date.Count > 1)
        {
            for (int i = 1; i < creation_date.Count; i++)
            {
                DateTime date1 = DateTime.Parse(creation_date[i - 1].ToString());
                DateTime date2 = DateTime.Parse(creation_date[i].ToString());
                if (date1 > date2)
                {
                    workno = wo_no[i - 1].ToString();
                    tablename = tabname[i - 1].ToString();
                    lstation_id = lstation[i - 1].ToString();
                    linename = Line_Name[i - 1].ToString();
                    linetime = date1.ToString("yyyyMMddHHmmssmm");
                    ptab = parttable[i - 1].ToString();
                }
                else
                {
                    workno = wo_no[i].ToString();
                    tablename = tabname[i].ToString();
                    lstation_id = lstation[i].ToString();
                    linename = Line_Name[i].ToString();
                    linetime = date2.ToString("yyyyMMddHHmmssmm");
                    ptab = parttable[i].ToString();
                }
            }
        }
        else
        {
            if (wo_no.Count > 0)
            {
                DateTime date = DateTime.Today;  // 20130213 DateTime.Parse(creation_date[0].ToString());
                workno = wo_no[0].ToString();
                tablename = tabname[0].ToString();
                lstation_id = lstation[0].ToString();
                linename = Line_Name[0].ToString();
                linetime = date.ToString("yyyyMMddHHmmssmm");
                linetime = "2011010101010101";
                ptab = parttable[0].ToString();
            }
        }

        switch (ptab)
        {
            case "SHP.CMCS_SFC_SORDER": fpart = "SPART"; fwono = "SORDER"; break;
            case "SHP.CMCS_SFC_AORDER": fpart = "APART"; fwono = "AORDER"; break;
            case "SHP.CMCS_SFC_PORDER": fpart = "PPART"; fwono = "PORDER"; break;
        }
        //根據Wo_No找Part
        if (workno.Trim() != "")
        {
            string dbstr = "SELECT DISTINCT " + fpart + "" +
                           " FROM " + ptab + "" +
                           " WHERE " + fwono + " = '" + workno + "'";
            DataSet dsst2 = DataBaseOperation.SelectSQLDS(DBType, DBReadString, dbstr);
            if (dsst2.Tables[0].Rows.Count > 0)
            {
                PART = dsst2.Tables[0].Rows[0][0].ToString();
            }
        }


        //根據Customer和Station_ID去表SFC.SFC_LINE_WS找Level


        //先找customer
        if (model.Trim() == "")
        {
            model = GetModelData("P1", DBType, DBReadString, PID);
        }

        string lcstr = " SELECT DISTINCT SUBSTR(MODEL,1,3) AA, CUSTOMER_NAME BB FROM SFC.CMCS_SFC_MODEL WHERE SUBSTR(MODEL,1,3) = '" + model + "' ";
        DataSet dsstcus = DataBaseOperation.SelectSQLDS(DBType, DBReadString, lcstr);
        if (dsstcus.Tables[0].Rows.Count > 0)
        {
            customer = dsstcus.Tables[0].Rows[0]["BB"].ToString();
        }

        //再找Level
        string levelstr = " SELECT SFC_LINE, SFC_WS  FROM  SFC.SFC_LINE_WS   WHERE CUSTOMER_NO = '" + customer + "' and SFC_WS = '" + lstation_id + "' ";
        DataSet dsstlevel = DataBaseOperation.SelectSQLDS(DBType, DBReadString, levelstr);
        if (dsstlevel.Tables[0].Rows.Count > 0)
        {
            level = dsstlevel.Tables[0].Rows[0][0].ToString();
        }


        //找status
        string statusstr = " SELECT DDATE, STATUS  from  SHP.CMCS_SFC_SHIPPING_DATA where PRODUCTID = '" + PID + "' ";
        DataSet dsststatus = DataBaseOperation.SelectSQLDS(DBType, DBReadString, statusstr);
        if (dsststatus.Tables[0].Rows.Count > 0)
        {
            status = dsststatus.Tables[0].Rows[0]["STATUS"].ToString();
        }

        string TestTime = "", TestWS="", TestStatus="";
        string TestStr = " SELECT *  from  WCDMA_TSE.R_FUNCTION_HEAD_T  where SERIAL_NUMBER = '" + PID + "' ";
        DataSet TESTDS = DataBaseOperation.SelectSQLDS(DBType, DBReadString, TestStr);
        if ( TESTDS.Tables[0].Rows.Count > 0)
        {
            TestWS     = TESTDS.Tables[0].Rows[0]["STATION_NAME"].ToString();
            TestTime   = TESTDS.Tables[0].Rows[0]["TEST_TIME"].ToString();
            // TestTime   = TESTDS.Tables[0].Rows[0]["TEST_TIME"].ToString("yyyyMMddHHmmssmm");
            // ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");=
            if ( TestTime != "" )
                 TestTime = ((DateTime)(Convert.ToDateTime(TESTDS.Tables[0].Rows[0]["TEST_TIME"].ToString()))).ToString("yyyyMMddHHmmssmm");
            TestStatus = TESTDS.Tables[0].Rows[0]["STATUS"].ToString();
        }

        if ( ( TestTime != "" ) && (  linetime != "" ) )  // Check CDATE
        {
            if (Convert.ToInt64(TestTime) > Convert.ToInt64(linetime))  // Update
            {
               lstation_id = TestWS;
               lstation_id = TestWS;
               status = TestStatus;
            }
        } //

        return workno;
    }//GetWO_NO


    
    //GetPIDData加參數Rtype
    //DBString:211 DBReadString1:215  DBWriString:221
    //對於221上MAINPIDTRACE:
    //1.從221獲得PID
    //2.大多數數據從211上讀
    //3.但在PCBA段，有部份數據放到了215上，所以WO_NO有時需要去215上讀
    public string GetPIDData(string Rtype, string dbtype, string DBString, string DBReadString1, string DBWriString, string PID, ref string WO_NO, ref string LINE_NAME, ref string tSTATION_ID, ref string IMEI, ref string STATUS, ref string SFC_Level, ref string STATION_IDTable, ref string LTIME, ref string Model, ref string RoutingNo, ref string PART, ref string Customer)
    {
        // Test Only 20121220
        // string TestDBPath = ConfigurationManager.AppSettings["L8StandByConnectionString"];
        // DBString = TestDBPath;
        // End Test

        if ((Rtype != "1") || (dbtype != "oracle") || (DBString == "") || (PID == "")) return "";
        if (DBReadString1 == "") DBReadString1 = DBString;
        if (DBWriString == "") DBWriString = DBString;

        string tmp1 = "1";

        IMEI = GetPIDIMEI("1", dbtype, DBString, DBString, "1", PID);  // return IMEI   LINE_NAME, ref string tSTATION_ID, ref string IMEI, ref string STATUS, ref string SFC_Level, ref string STATION_IDTable, ref string LTIME, ref string Model, ref string RoutingNo, ref string PART, ref string Customer
        Model = GetModelData("P1", DBType, DBString, PID);
        WO_NO = GetWO_NO("P1", DBType, DBString, DBString, PID, ref PART, ref LINE_NAME, ref tSTATION_ID, ref  STATUS, ref  SFC_Level, ref  STATION_IDTable, ref  LTIME, ref  Customer, ref  Model);
        RoutingNo = GetRoutingNO("P1", DBType, DBString, DBWriString, DBReadString1, PID, ref PART, ref Model);

        if ((Model == "") || (WO_NO == "") || (RoutingNo == "")) tmp1 = "";

        return (tmp1);

    }//GetPIDData

    public string GetPIDIMEI(string Type1, string dbtype, string dbstring, string dbWristring, string InTYpe, string IData)  // return IMEI
    {
        string Ret1 = "", sql1 = "";
        if (InTYpe == "1") // Input PID Ret IMEINUM
            sql1 = " select * from SHP.CMCS_SFC_IMEINUM where PRODUCT_ID = '" + IData + "' ";
        else
            sql1 = " select * from SHP.CMCS_SFC_IMEINUM where IMEINUM = '" + IData + "' ";

        DataSet dt1 = DataBaseOperation.SelectSQLDS(dbtype, dbstring, sql1);
        int v3 = dt1.Tables[0].Rows.Count;
        if (v3 > 0)
        {
            if (InTYpe == "1") // Input PID Ret IMEINUM
                Ret1 = dt1.Tables[0].Rows[0]["IMEINUM"].ToString();
            else
                Ret1 = dt1.Tables[0].Rows[0]["PRODUCT_ID"].ToString();
        }

        return (Ret1);
    }

}
