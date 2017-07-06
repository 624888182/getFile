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
public class DeLinkPidlib2
{
    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd"); 
    static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();   
    // FSplitlib FSplitlibPointer = new FSplitlib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    string DBType = "oracle";


    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");

    public string DeLinkTrans(string P1, string DBType, string ReadDB, string WriDB1, string WriDB2, string begpid)
    {
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "";
        int limno = 200000, Datacnt = 0;
        DataSet tmpds = null;
        // string tCurrtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tmpsql = "select SERIAL_NUMBER, IN_LINE_TIME from SFC. R_WIP_TRACKING_T_PID ";
        // string tmpsql = @"Select  t.SERIAL_NUMBER, t.IN_LINE_TIME, ROWNUM  from SFC. R_WIP_TRACKING_T_PID t  where  ROWNUM <= " + limno + "   "
        //        + " and  t.SERIAL_NUMBER >  '" + begpid + "'   order by t.SERIAL_NUMBER asc ";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(DBType, ReadDB, tmpsql);
        if (ds1.Tables.Count > 0) Datacnt = ds1.Tables[0].Rows.Count;
        if (Datacnt == 0) return ("");

        string lastpid = "";
        string[,] arrtmp = new string[Datacnt + 2, 10];
        for (v2 = 0; v2 <= Datacnt + 1; v2++)
            for (v5 = 0; v5 < 9; v5++)
                arrtmp[v2, v5] = "";

        for (v2 = 0; v2 < Datacnt; v2++)
        {
            arrtmp[v2 + 1, 0] = (v2 + 1).ToString();
            arrtmp[v2 + 1, 1] = ds1.Tables[0].Rows[v2]["SERIAL_NUMBER"].ToString();
            arrtmp[v2 + 1, 2] = ds1.Tables[0].Rows[v2]["IN_LINE_TIME"].ToString();

            if ((arrtmp[v2 + 1, 2] != "") && (arrtmp[v2 + 1, 2] != null))
            {
                tmp1 = ((DateTime)(ds1.Tables[0].Rows[v2]["IN_LINE_TIME"])).ToString("yyyyMMddHHmmssmm");
                arrtmp[v2 + 1, 2] = tmp1.ToString();
            }
            else
            {
                tmp1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                arrtmp[v2 + 1, 2] = tmp1.ToString();
            }

            lastpid = arrtmp[v2 + 1, 1];
        }


        // find in MAINPIDTRACE
        v3 = 0; v4 = 0;
        string tmpsqlW1 = "", tmpsqlW2 = "", SP = "";
        for (v2 = 0; v2 < Datacnt; v2++)
        {
            v4++;
            tmp1 = arrtmp[v2 + 1, 1]; //= ds1.Tables[0].Rows[v2]["SERIAL_NUMBER"].ToString();
            tmp2 = arrtmp[v2 + 1, 2]; //= ds1.Tables[0].Rows[v2]["IN_LINE_TIME"].ToString();

            if ((tmp1 != "") && (tmp2 != ""))
            {
                tmpsqlW1 = "select PID from PUBLIB.MAINPIDTRACE where PID = '" + tmp1 + "' ";
                // tmpsqlW2 = "Insert into PUBLIB.MAINPIDBAK   ( PID , CDATE, CSTATUS ) Values ( '" + tmp1 + "', '" + tmp2 + "', '" + SP + "' ) ";
                tmpds = DataBaseOperation.SelectSQLDS(DBType, WriDB1, tmpsqlW1);
                if (tmpds.Tables.Count > 0) v1 = tmpds.Tables[0].Rows.Count;
                if (v1 <= 0)
                {
                    arrtmp[v2 + 1, 3] = "Y";
                    v3++;
                }
                else v4++;
            }
            else
            {
                tmp4 = tmp1;
            }

            //if (WriDB2 != "")
            //{
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW1);
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW2);
            //}
        }


        // Write Data
        v4 = 0;
        for (v2 = 0; v2 < Datacnt; v2++)
        {
            v4++;
            tmp1 = arrtmp[v2 + 1, 1]; //= ds1.Tables[0].Rows[v2]["SERIAL_NUMBER"].ToString();
            tmp2 = arrtmp[v2 + 1, 2]; //= ds1.Tables[0].Rows[v2]["IN_LINE_TIME"].ToString();

            if ((tmp1 != "") && (tmp2 != "") && (arrtmp[v2 + 1, 3] == "Y"))
            {
                tmpsqlW1 = "Insert into PUBLIB.MAINPIDTRACE ( PID , CDATE, CSTATUS ) Values ( '" + tmp1 + "', '" + tmp2 + "', '" + SP + "' ) ";
                tmpsqlW2 = "Insert into PUBLIB.MAINPIDBAK   ( PID , CDATE, CSTATUS ) Values ( '" + tmp1 + "', '" + tmp2 + "', '" + SP + "' ) ";
                v3 = DataBaseOperation.ExecSQL(DBType, WriDB1, tmpsqlW1);
                v3 = DataBaseOperation.ExecSQL(DBType, WriDB1, tmpsqlW2);
                tmp3 = tmp1;  // Last PID
            }
            else
            {
                tmp4 = tmp1;
            }

            //if (WriDB2 != "")
            //{
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW1);
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW2);
            //}
        }

        return (tmp3);


    }  // End trans data


    public string DeLinkTransUpdate(string P1, string DBType, string ReadDB, string WriDB1, string WriDB2, string begpid)
    {
        int v1 = 0, v2 = 0, v3 = 0;
        string tmp1 = "", tmp2 = "", tmp3 = "", tmp4 = "";
        int limno = 20000;
        string tmpsql = "select a.SERIAL_NUMBER, a.IN_LINE_TIME from SFC. R_WIP_TRACKING_T_PID  a, PUBLIB.MAINPIDTRACE b "
            + " where b.PID = b.CDATE  and a.SERIAL_NUMBER = b.PID ";
        // string tCurrtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        // string tmpsql = "select SERIAL_NUMBER, IN_LINE_TIME from SFC. R_WIP_TRACKING_T_PID ";
        // string tmpsql = @"Select  t.SERIAL_NUMBER, t.IN_LINE_TIME, ROWNUM  from SFC. R_WIP_TRACKING_T_PID t  where     "
        //        + "   t.SERIAL_NUMBER <=  '" + begpid + "'   order by t.SERIAL_NUMBER asc ";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(DBType, ReadDB, tmpsql);
        if (ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 == 0) return ("");


        string[,] arrtmp = new string[v1 + 2, 10];
        for (v2 = 0; v2 < v1; v2++)
        {
            arrtmp[v2 + 1, 0] = (v2 + 1).ToString();
            arrtmp[v2 + 1, 1] = ds1.Tables[0].Rows[v2]["SERIAL_NUMBER"].ToString();
            arrtmp[v2 + 1, 2] = ds1.Tables[0].Rows[v2]["IN_LINE_TIME"].ToString();

            if ((arrtmp[v2 + 1, 2] != "") && (arrtmp[v2 + 1, 2] != null))
            {
                tmp1 = ((DateTime)(ds1.Tables[0].Rows[v2]["IN_LINE_TIME"])).ToString("yyyyMMddHHmmssmm");
                arrtmp[v2 + 1, 2] = tmp1.ToString();
            }
            else
            {
                tmp1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                arrtmp[v2 + 1, 2] = tmp1.ToString();
            }
        }

        string tmpsqlW1 = "", tmpsqlW2 = "", SP = "";
        for (v2 = 0; v2 < v1; v2++)
        {
            tmp1 = arrtmp[v2 + 1, 1]; //= ds1.Tables[0].Rows[v2]["SERIAL_NUMBER"].ToString();
            tmp2 = arrtmp[v2 + 1, 2]; //= ds1.Tables[0].Rows[v2]["IN_LINE_TIME"].ToString();

            if ((tmp1 != "") && (tmp2 != ""))
            {
                tmpsqlW1 = "UPDATE PUBLIB.MAINPIDTRACE SET CDATE = '" + tmp2 + "'  where  PID = '" + tmp1 + "'  ";
                tmpsqlW2 = "UPDATE PUBLIB.MAINPIDBAK   SET CDATE = '" + tmp2 + "'  where  PID = '" + tmp1 + "'  ";
                v3 = DataBaseOperation.ExecSQL(DBType, WriDB1, tmpsqlW1);
                v3 = DataBaseOperation.ExecSQL(DBType, WriDB1, tmpsqlW2);
                tmp3 = tmp1;  // Last PID
            }
            else
            {
                tmp4 = tmp1;
            }

            //if (WriDB2 != "")
            //{
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW1);
            //    v3 = DataBaseOperation.ExecSQL(DBType, WriDB2, tmpsqlW2);
            //}
        }

        return (tmp3);

    }


}  // end public class DeLinkPIdlib2 
}  // end namespace SFC.TJWEB


