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

namespace SCM.GSCMDKen
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class FMerslib
{
    protected DateTime tmptoday = DateTime.Today;
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd"); 
    public static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();

    public string Syncro_mergelinkEV(string iRunDate, int iDayCnt, string iDBFPath)
    {
        int v1 = 0;
        string s1 = "", DownDVDay = "", ErrMsg="";
        if (iRunDate == "") iRunDate = CuurDate;
        if (iDayCnt <= 0) iDayCnt = 1;
        if (iDBFPath == "") iDBFPath = RunDBPath;

        DownDVDay = iRunDate; 
        // DownDVDay = textBox1.Text.Substring(0, 4) + textBox1.Text.Substring(5, 2) + textBox1.Text.Substring(8, 2);
        int Sglf1Long = 0;
        string Sqlf1 = "select distinct a.CustomerSite, a.FoxconnSite, a.CustomerPN, b.DocumentTime, "
        + " b.Document_ID, a.DocumentID "
        + " from PreETA a, Syncro_4A3_Detail_PNOneSet b, Syncro_4A5_Detail_Plant c "
        + " where a.CustomerSite = b.CustomerSite "
        + " and   a.CustomerPN   = b.Forecast_CustomerPN "
        + " and   b.Document_ID = c.Document_ID "
        + " and   b.Forecast_CustomerPN = c.Forecast_CustomerPN "
        + " and   a.ReleaseDate like '" + DownDVDay + "'+'%' "
        + " and   ( a.DVcode = '' or a.Nokia1Document_ID = '' ) "
        + " and   a.DVtype = 'EV' "
        + " order by a.CustomerSite, a.FoxconnSite, a.CustomerPN, b.DocumentTime, b.Document_ID, a.DocumentID ";

        // DataSet dsf1 = DataConnlib.GetDataByParaPath(Sqlf1, RunDBPath);  // All Data Not only EV
        DataSet dsf1 = LibSCM1Pointer.GetDataByDataPath(Sqlf1, RunDBPath);  // All Data Not only EV
        if (dsf1.Tables.Count <= 0) Sglf1Long = 0; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        else
            Sglf1Long = dsf1.Tables[0].Rows.Count;

        if (Sglf1Long <= 0)
        {
            // Response.Write("<script>alert('No Data been selected ! 稍后重試！ ')</script>");
            return("");
        }

        string[,] arrayf1 = new string[Sglf1Long + 1, 10 + 1];
        int v2 = 0, v3 = 0;

        for (v1 = 0; v1 < Sglf1Long; v1++)  // load in memory
        {
            arrayf1[v1 + 1, 1] = dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
            arrayf1[v1 + 1, 2] = dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
            arrayf1[v1 + 1, 3] = dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
            arrayf1[v1 + 1, 4] = dsf1.Tables[0].Rows[v1]["DocumentTime"].ToString();
            arrayf1[v1 + 1, 5] = dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();
            arrayf1[v1 + 1, 6] = dsf1.Tables[0].Rows[v1]["DocumentID"].ToString();
            arrayf1[v1 + 1, 7] = ""; 
            arrayf1[v1 + 1, 8] = "";
            arrayf1[v1 + 1, 9] = "";
            arrayf1[v1 + 1, 10] = ""; 
            
            
            // s1 = arrayNokiaPVDocm[localvar, 2];
            // s2 = arrayNokiaPVDocm[localvar, 3];
            // s3 = arrayNokiaPVDocm[localvar, 4];
            // if ((tmpCustomerSite == arrayNokiaPVDocm[localvar, 2]) && (tmpFoxconnSite == arrayNokiaPVDocm[localvar, 3]) && (tmpCustomerPN == arrayNokiaPVDocm[localvar, 4]))
            // {
            //     tmpCustomerDum = arrayNokiaPVDocm[localvar, 5]; //  ds6.Tables[0].Rows[localvar]["Forecast_PartnerGBI"].ToString();  // CustomerDun       
            //    tmpNokiaPVDocm = arrayNokiaPVDocm[localvar, 1]; //  ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
            //    localvar = arrayNokiaPVDocmLong + 1; // break
            // }
        }


        string s2 = "", s3 = "", s4 = "", s5 = "", s6 = "";

        for (v1 = Sglf1Long - 1; v1 > 0; v1--)  // load in memory
        {
            // s1 = arrayf1[v1, 1]; // dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
            // s2 = arrayf1[v1, 2]; // dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
            // s3 = arrayf1[v1, 3]; // dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
            // s5 = arrayf1[v1, 5]; // dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();

            if ((s1 == arrayf1[v1, 1].ToString()) && (s2 == arrayf1[v1, 2].ToString()) && (s3 == arrayf1[v1, 3].ToString()))
            {
                arrayf1[v1, 7] = "N"; 
            }
            else
            {
                arrayf1[v1, 7] = "Y"; 

            }

            s1 = arrayf1[v1, 1].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
            s2 = arrayf1[v1, 2].ToString(); // dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
            s3 = arrayf1[v1, 3].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
            s5 = arrayf1[v1, 5].ToString(); // dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();



        }

        string Sqlf2="", tDVCode="1", tCheck_Count="1";
        // Rewrite back PreETA Nokia1Document_ID, DVCode
        for (v1 = 0; v1 < Sglf1Long; v1++)  // load in memory
        {
            if (arrayf1[v1, 7] == "Y")   // UpDate PreETA
            {
                s1 = arrayf1[v1, 1].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
                s2 = arrayf1[v1, 2].ToString(); // dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
                s3 = arrayf1[v1, 3].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
                s4 = arrayf1[v1, 4].ToString(); // dsf1.Tables[0].Rows[v1]["DocumentTime"].ToString();
                s5 = arrayf1[v1, 5].ToString(); // dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();
                s6 = arrayf1[v1, 6].ToString(); // dsf1.Tables[0].Rows[v1]["DocumentID"].ToString();

                Sqlf2 = "Update PreETA set Nokia1Document_ID = '" + s5 + "', DVCode = '" + tDVCode + "' "
                + " where CustomerSite = '" + s1 + "' and FoxconnSite =  '" + s2 + "' and CustomerPN =  '" + s3 + "' "
                + " and DocumentID =  '" + s6 + "'  ";

                // if (!DataConnlib.Excute(Sqlf2)) 
                if ( !LibSCM1Pointer.DBExcuteByDataPath(Sqlf2, RunDBPath) )
                {           
                    // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                    ErrMsg = "Update PreETA 新增失敗，請稍后重試！";
                }

                Sqlf2 = "Update Syncro_4A3_Detail_PNOneSet set Check_Count = '" + tCheck_Count + "', ReplyNokia_DateTime = '" + s6 + "' "
                + " where CustomerSite = '" + s1 + "' and Forecast_CustomerPN =  '" + s3 + "' "
                + " and Document_ID =  '" + s5 + "'  ";

                // if (!DataConnlib.Excute(Sqlf2))
                if ( !LibSCM1Pointer.DBExcuteByDataPath(Sqlf2, RunDBPath) )
                {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                    // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                    ErrMsg = "Update Syncro_4A3_Detail_PNOneSet 新增失敗，請稍后重試！";
                }

            } // end if 

        }

        return ("");

    }  // end of Syncro_mergelink  

    public string Syncro_mergelinkPV(string iRunDate, int iDayCnt, string iDBFPath)
    {
        int v1 = 0;
        string s1 = "", DownDVDay = "", ErrMsg = "";
        if (iRunDate == "") iRunDate = CuurDate;
        if (iDayCnt <= 0) iDayCnt = 1;
        if (iDBFPath == "") iDBFPath = RunDBPath;

        DownDVDay = iRunDate;
        // DownDVDay = textBox1.Text.Substring(0, 4) + textBox1.Text.Substring(5, 2) + textBox1.Text.Substring(8, 2);
        int Sglf1Long = 0;
        string Sqlf1 = "select distinct a.CustomerSite, a.FoxconnSite, a.CustomerPN, b.DocumentTime, "
        + " b.Document_ID, a.DocumentID "
        + " from PreETA a, Syncro_4A1_Detail_PNOneSet b, Syncro_4A1_Detail_Plant c "
        + " where a.CustomerSite = b.CustomerSite "
        + " and   a.CustomerPN   = b.Forecast_CustomerPN "
        + " and   b.Document_ID = c.Document_ID "
        + " and   b.Forecast_CustomerPN = c.Forecast_CustomerPN "
        + " and   a.ReleaseDate like '" + DownDVDay + "'+'%' "
        + " and   ( a.DVcode = '' or a.Nokia1Document_ID = '' ) "
        + " and   a.DVtype = 'PV' "
        + " order by a.CustomerSite, a.FoxconnSite, a.CustomerPN, b.DocumentTime, b.Document_ID, a.DocumentID ";

        // DataSet dsf1 = DataConnlib.GetDataByParaPath(Sqlf1, RunDBPath);  // All Data Not only EV
        DataSet dsf1 = LibSCM1Pointer.GetDataByDataPath(Sqlf1, RunDBPath);  // All Data Not only EV
        if (dsf1.Tables.Count <= 0) Sglf1Long = 0; // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
        else
            Sglf1Long = dsf1.Tables[0].Rows.Count;

        if (Sglf1Long <= 0)
        {
            // Response.Write("<script>alert('No Data been selected ! 稍后重試！ ')</script>");
            return ("");
        }

        string[,] arrayf1 = new string[Sglf1Long + 1, 10 + 1];
        int v2 = 0, v3 = 0;

        for (v1 = 0; v1 < Sglf1Long; v1++)  // load in memory
        {
            arrayf1[v1 + 1, 1] = dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
            arrayf1[v1 + 1, 2] = dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
            arrayf1[v1 + 1, 3] = dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
            arrayf1[v1 + 1, 4] = dsf1.Tables[0].Rows[v1]["DocumentTime"].ToString();
            arrayf1[v1 + 1, 5] = dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();
            arrayf1[v1 + 1, 6] = dsf1.Tables[0].Rows[v1]["DocumentID"].ToString();
            arrayf1[v1 + 1, 7] = "";
            arrayf1[v1 + 1, 8] = "";
            arrayf1[v1 + 1, 9] = "";
            arrayf1[v1 + 1, 10] = "";


            // s1 = arrayNokiaPVDocm[localvar, 2];
            // s2 = arrayNokiaPVDocm[localvar, 3];
            // s3 = arrayNokiaPVDocm[localvar, 4];
            // if ((tmpCustomerSite == arrayNokiaPVDocm[localvar, 2]) && (tmpFoxconnSite == arrayNokiaPVDocm[localvar, 3]) && (tmpCustomerPN == arrayNokiaPVDocm[localvar, 4]))
            // {
            //     tmpCustomerDum = arrayNokiaPVDocm[localvar, 5]; //  ds6.Tables[0].Rows[localvar]["Forecast_PartnerGBI"].ToString();  // CustomerDun       
            //    tmpNokiaPVDocm = arrayNokiaPVDocm[localvar, 1]; //  ds6.Tables[0].Rows[localvar]["Document_ID"].ToString();
            //    localvar = arrayNokiaPVDocmLong + 1; // break
            // }
        }


        string s2 = "", s3 = "", s4 = "", s5 = "", s6 = "";

        for (v1 = Sglf1Long - 1; v1 > 0; v1--)  // load in memory
        {
            // s1 = arrayf1[v1, 1]; // dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
            // s2 = arrayf1[v1, 2]; // dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
            // s3 = arrayf1[v1, 3]; // dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
            // s5 = arrayf1[v1, 5]; // dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();

            if ((s1 == arrayf1[v1, 1].ToString()) && (s2 == arrayf1[v1, 2].ToString()) && (s3 == arrayf1[v1, 3].ToString()))
            {
                arrayf1[v1, 7] = "N";
            }
            else
            {
                arrayf1[v1, 7] = "Y";

            }

            s1 = arrayf1[v1, 1].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
            s2 = arrayf1[v1, 2].ToString(); // dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
            s3 = arrayf1[v1, 3].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
            s5 = arrayf1[v1, 5].ToString(); // dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();



        }

        string Sqlf2 = "", tDVCode = "1", tCheck_Count = "1";
        // Rewrite back PreETA Nokia1Document_ID, DVCode
        for (v1 = 0; v1 < Sglf1Long; v1++)  // load in memory
        {
            if (arrayf1[v1, 7] == "Y")   // UpDate PreETA
            {
                s1 = arrayf1[v1, 1].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerSite"].ToString();
                s2 = arrayf1[v1, 2].ToString(); // dsf1.Tables[0].Rows[v1]["FoxconnSite"].ToString();
                s3 = arrayf1[v1, 3].ToString(); // dsf1.Tables[0].Rows[v1]["CustomerPN"].ToString();
                s4 = arrayf1[v1, 4].ToString(); // dsf1.Tables[0].Rows[v1]["DocumentTime"].ToString();
                s5 = arrayf1[v1, 5].ToString(); // dsf1.Tables[0].Rows[v1]["Document_ID"].ToString();
                s6 = arrayf1[v1, 6].ToString(); // dsf1.Tables[0].Rows[v1]["DocumentID"].ToString();

                Sqlf2 = "Update PreETA set Nokia1Document_ID = '" + s5 + "', DVCode = '" + tDVCode + "' "
                + " where CustomerSite = '" + s1 + "' and FoxconnSite =  '" + s2 + "' and CustomerPN =  '" + s3 + "' "
                + " and DocumentID =  '" + s6 + "'  ";

                // if (!DataConnlib.Excute(Sqlf2)) 
                if (!LibSCM1Pointer.DBExcuteByDataPath(Sqlf2, RunDBPath))
                {
                    // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                    ErrMsg = "Update PreETA 新增失敗，請稍后重試！";
                }

                Sqlf2 = "Update Syncro_4A1_Detail_PNOneSet set Check_Count = '" + tCheck_Count + "', ReplyNokia_DateTime = '" + s6 + "' "
                + " where CustomerSite = '" + s1 + "' and Forecast_CustomerPN =  '" + s3 + "' "
                + " and Document_ID =  '" + s5 + "'  ";

                // if (!DataConnlib.Excute(Sqlf2))
                if (!LibSCM1Pointer.DBExcuteByDataPath(Sqlf2, RunDBPath))
                {           //      Response.Write("<script>alert('MemoryETDToETA 新增失敗，請稍后重試！ '" + tmpCustomerSite + "' ')</script>");
                    // Response.Write("<script>alert('ExeceptionLog 新增失敗，請稍后重試！ ')</script>");
                    ErrMsg = "Update Syncro_4A1_Detail_PNOneSet 新增失敗，請稍后重試！";
                }

            } // end if 

        }

        return ("");

    }  // end of Syncro_mergelink  
        
}  // end public class FMers1lib
}  // end namespace SCM.GCMDKen


