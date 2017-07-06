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
public class DeLinkPidlib
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
    DeLinkPidlib3 DeLinkPidlib3Pointer = new DeLinkPidlib3();
    string DBType = "oracle";
    protected string Backdbstring = ConfigurationManager.AppSettings["NormalBakupConnectionString"];


    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    /////////////////////////////////////////////////////////////////////////////////////////////
    // 20121110 Arthur: Ken.Lin
    // DeLinkPidProc ( P1, P2, P3, P4, P5, P6, P7, P8, P9, P10 )
    // INPUT :
    // Param P1 " "1" TJSFC 
    //       P2 : Database Type, Oracle, sql,
    //       P3 : Read DB
    //       P4 : Write DB
    //       P5 : DeLink Routing NO 秆竕絪腹 101:Cartin  102:穨锣眒舶, 103: 3S4S秆竕 
    //       P6 : Data Kind 1:DN, 2:PID, 3:EMEI, 4.Carton  5.Group DN, 6. Group PID, 7. Group IMEI, 8 Group Carton 
    //       P7 : InputData (  if P5 = "PID" )
    //       P8 : Refene=rence array (  if P5 != '1' )
    //       P9 : Optype "R", "W"
    //       P10: Response Message
    //        : Succ : Last DocumentID
    /////////////////////////////////////////////////////////////////////////////////////////////
    public string DeLinkPidProc(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] arrDeLinkPid, string tDeLinkLine, string tDeLinkLineWS, string Optype, ref string ReMess)
    {
        //if ( tDeLinkLineWS == "" ) tDeLinkLineWS = "PACK"; // Test Only 
        if ( ( DBType == "") || ( ReadDB=="") || ( WriDB=="") || ( tmpDeLinkRoutNo=="") || ( DataKind=="") || ( InputData=="") || ( arrDeLinkPid[1,1]=="") || ( tDeLinkLine=="") || 
             ( tDeLinkLineWS=="") || ( Optype=="")  )
        {
            ReMess = "Data Empty";
            return("");
        }
  
        // string[,] arrRout = null; 
        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        int arrv1 = 10; // arrDeLinkPid.GetEnumerator();
        arrv1 = arrDeLinkPid.GetLength(0);
        int v1=0, v2=0, RetInt=0;
        string tmpsqlW = "", sp = "", RetStr = "", RetSSstr = "", tmpPID = "", tmpIMEI = "", SWPID = DataKind, Ret1 = "";
        string tWO_NO = "", tLINE_NAME = "", tSTATION_ID = "", tIMEI = "", tSTATUS = "", SFC_LEVEL = "", STATION_IDTable="", LTIME="", Reqflag = "";
        string tWO = "", tLINE = "", tEMEI = "", tpart = "", tmodel = "", tcustomer = "", routno = "";
        string r1 = "", r2 = "", r3 = "", r4 = "", r5 = "";

        if (DataKind == "3") // Convert EMEI to PID
        {
            tmpIMEI = InputData;
            tmpPID = GetPIDIMEI(P1, DBType, ReadDB, WriDB, "2", InputData); // arrDeLinkPid[1, 1];
        }
        else
        if (DataKind == "2") // PID
        {
            tmpPID = InputData;
            tmpIMEI = GetPIDIMEI(P1, DBType, ReadDB, WriDB, "1", tmpPID);
        }

        string tmpsql = "select * from PUBLIB.DeLinkPIDRouting where DeLinkPIDRoutingNo = '" + tmpDeLinkRoutNo + "' order by  DeLinkPIDRoutingNo, Seqno";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(DBType, WriDB, tmpsql);
        if ( ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 == 0)
        {
            ReMess = tmpDeLinkRoutNo + "Not in Rout";
            return ("");
        } 

        string[,] arrRout = new string[ 10+1, v1+1];
        string[,] tmparrRout = new string[10 + 1, v1 + 1];
        for (v2 = 0; v2 < v1; v2++)
        {
               arrRout[v2 + 1, 0] = (v2 + 1).ToString();
               arrRout[v2 + 1, 1] = ds1.Tables[0].Rows[v2]["DeLinkPIDRoutingNo"].ToString();
               arrRout[v2 + 1, 2] = ds1.Tables[0].Rows[v2]["Desp"].ToString();
               arrRout[v2 + 1, 3] = ds1.Tables[0].Rows[v2]["ActionItem"].ToString();
               arrRout[v2 + 1, 4] = ds1.Tables[0].Rows[v2]["Seqno"].ToString();
            tmparrRout[v2 + 1, 0] = (v2 + 1).ToString(); 
            tmparrRout[v2 + 1, 1] = ds1.Tables[0].Rows[v2]["DeLinkPIDRoutingNo"].ToString();
            tmparrRout[v2 + 1, 2] = ds1.Tables[0].Rows[v2]["Desp"].ToString();
            tmparrRout[v2 + 1, 3] = ds1.Tables[0].Rows[v2]["ActionItem"].ToString();
            tmparrRout[v2 + 1, 4] = ds1.Tables[0].Rows[v2]["Seqno"].ToString();
        }

   
        tDocumentID = Currtime;
        if (((DataKind == "6") || (DataKind == "7")) && (InputData != "") && (InputData != null ) )
            tDocumentID = InputData;

        // Check Array
        v2 = 0;
        if ( (DataKind=="1" ) || (DataKind=="4" ) || (DataKind=="5" ) ||  (DataKind=="6" ) || (DataKind=="7" ) || (DataKind=="8" ) ) 
        {
             v1 = 1;
             while ((arrDeLinkPid[v1, 1] != "") && (arrDeLinkPid[v1, 1] != null) && (v1 <= arrv1))
             {
                 tmpPID = arrDeLinkPid[v1, 1]; // 材 PID 
                 tWO_NO = ""; tLINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = "";
                 tpart = ""; tmodel = ""; tcustomer = ""; routno = "";
                 // Ret1 = GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
                 Ret1 = DeLinkPidlib3Pointer.GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
                 if ((Ret1 != "0") && (Ret1 != "") && (tSTATUS != "畐"))
                 {
                     v2++;
                     arrDeLinkPid[ v1, 0] = tDocumentID; // REturn Back
                 }
                 else
                     arrDeLinkPid[v1, 0] = ""; // Not-Data

                 v1++;
             }
        }
        else
        if ( (DataKind=="2" ) || (DataKind=="3" ) )
        {
                 tWO_NO = ""; tLINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = "";
                 //Ret1 = GetPIDData(DBType, ReadDB, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME);
                 Ret1 = DeLinkPidlib3Pointer.GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
                 if ( ( Ret1 != "0" ) && ( Ret1 != "" )  && ( tSTATUS != "畐" ) )
                 {
                        v2 ++;
                        arrDeLinkPid[1, 0] = tDocumentID; // REturn Back
                 }
                 else                  
                        arrDeLinkPid[1, 0] = "";
                 
        }
        // End Chaeck Data

        if ( v2 == 0 ) return(""); // Not-Value Data

 

         tmpsqlW = "Insert into PUBLIB.DeLinkPidReq ( DocumentID , ReqType, DeLinkData, DeLinkPIDRoutingNo, DeLinkLine, DeLinkLineWS, CSTATUS  ) "
                 + " Values ( '" + tDocumentID + "', '" + DataKind + "', '" + InputData + "','" + tmpDeLinkRoutNo + "', '" + tDeLinkLine + "','" + tDeLinkLineWS + "','" + sp + "' ) ";
        RetInt = DataBaseOperation.ExecSQL(DBType, WriDB, tmpsqlW);
        if (RetInt < 0) RetStr = "";

        Thread.Sleep(100);
        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        tDocumentIDPid = Currtime;
        
           
        if ( (DataKind == "2") || (DataKind == "3")  ) // PID, IMEI
        {  
            //           if (DataKind == "2") tmpIMEI = GetPIDIMEI(P1, DBType, ReadDB, WriDB, "1", tmpPID);  // return IMEI
            //if (DataKind == "3") tmpIMEI = InputData; // Org inout is IMEI
            //   Ret1 = GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
            Ret1 = DeLinkPidlib3Pointer.GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
            if (  ( Ret1 == "" ) ||  ( Ret1 == "-1" ) || ( tSTATUS == "畐" )) return("-1");
            RetSSstr = DeLinkPidBackup(P1, DBType, ReadDB, WriDB, tmpDeLinkRoutNo, DataKind, tmpPID, ref  tmparrRout, Optype, ref ReMess, tDocumentID, tDocumentIDPid, tDeLinkLine, tDeLinkLineWS, tmpIMEI);
            if ((RetSSstr != "0") && (RetSSstr != "") && (RetSSstr != "-1"))
            {
                RetSSstr = DeLinkPidModify(P1, DBType, ReadDB, WriDB, tmpDeLinkRoutNo, DataKind, tmpPID, ref  tmparrRout, Optype, ref ReMess, tDocumentID, tDocumentIDPid, tDeLinkLine, tDeLinkLineWS, tWO_NO, tLINE_NAME, tSTATION_ID, tmpIMEI);
                if ((RetSSstr != "0") && (RetSSstr != "") && (RetSSstr != "-1"))
                {
                    r1 = InsertMES_ASSY_HISTORY(WriDB, tWO_NO, tmpPID, tDeLinkLineWS, "P", Currtime, "0", "ken", "N", tSTATION_ID, tDeLinkLine);
                    r2 = UpdateFileSTatus(P1, DBType, ReadDB, WriDB, "PUBLIB.DELINKPIDMAIN", "DOCUMENTIDPID", tDocumentIDPid, "Y", "CSTATUS");
                    Reqflag = "Y";
                    r3 = UpdateFileSTatus(P1, DBType, ReadDB, WriDB, "PUBLIB.DELINKPIDREQ", "DOCUMENTID", tDocumentID, "Y", "CSTATUS");
                    RetStr = tDocumentID;
                    arrDeLinkPid[1, 0] = tDocumentID;
                }   // end DeLinkPidModify
                else arrDeLinkPid[1, 0] = "";
            }     // end DeLinkPidBackup
        }
        else
        {
            v1 = 1;
            while ((arrDeLinkPid[v1, 1] != "") && (arrDeLinkPid[v1, 1] != null) && (v1 <= arrv1) )
            {
                tmpPID = arrDeLinkPid[v1, 1]; // 材 PID 
                arrDeLinkPid[v1, 0] = "";     // CLear
                tWO_NO = ""; tLINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = "";
                                   //Ret1 = GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
                Ret1 = DeLinkPidlib3Pointer.GetPIDData("1", DBType, ReadDB, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
                if ( ( Ret1 != "0" ) && ( Ret1 != "" )  && ( tSTATUS != "畐" ) ) 
                {
                     Thread.Sleep(100);
                     Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                     tDocumentIDPid = Currtime;
                     tmpIMEI = GetPIDIMEI(P1, DBType, ReadDB, WriDB, "1", tmpPID); // tmpIMEI = arrDeLinkPid[v1, 4];                    
                     RetSSstr = DeLinkPidBackup(P1, DBType, ReadDB, WriDB, tmpDeLinkRoutNo, DataKind, tmpPID, ref  tmparrRout, Optype, ref ReMess, tDocumentID, tDocumentIDPid, tDeLinkLine, tDeLinkLineWS, tmpIMEI);
                     if ((RetSSstr != "0") && (RetSSstr != "") && (RetSSstr != "-1"))
                     {
                         RetSSstr = DeLinkPidModify(P1, DBType, ReadDB, WriDB, tmpDeLinkRoutNo, DataKind, tmpPID, ref  tmparrRout, Optype, ref ReMess, tDocumentID, tDocumentIDPid, tDeLinkLine, tDeLinkLineWS, tWO_NO, tLINE_NAME, tSTATION_ID, tmpIMEI);
                         if ((RetSSstr != "0") && (RetSSstr != "") && (RetSSstr != "-1"))
                         {
                             r1 = InsertMES_ASSY_HISTORY(WriDB, tWO_NO, tmpPID, tDeLinkLineWS, "P", Currtime, "0", "ken", "N", tSTATION_ID, tDeLinkLine);
                             r2 = UpdateFileSTatus(P1, DBType, ReadDB, WriDB, "PUBLIB.DELINKPIDMAIN", "DOCUMENTIDPID", tDocumentIDPid, "Y", "CSTATUS");
                             Reqflag = "Y";
                             r3 = UpdateFileSTatus(P1, DBType, ReadDB, WriDB, "PUBLIB.DELINKPIDREQ", "DOCUMENTID", tDocumentID, "Y", "CSTATUS");
                             RetStr = tDocumentID;
                             arrDeLinkPid[v1, 0] = tDocumentID; 
                         }
                     }
                     else arrDeLinkPid[v1, 0] = "";
               }

                v1++;
            }

        }


        return ( RetStr );
        
    }

    public string InsertMES_ASSY_HISTORY(string tWriDB, string tWO_NO, string tmpPID, string tDeLinkLineWS, string tStatus, string tCurrtime, string tSeqid, string tUserid, string tOnline, string tLINE_NAME, string tDeLinkLine)
    {
        DateTime tmpDATE = DateTime.Now;
        long tseqno = Convert.ToInt64(tSeqid);

        string RetStr = "";
     //   string tmpsqlW = "Insert into SFC.MES_ASSY_HISTORY ( WO_NO , PRODUCT_ID, STATION_ID, STATE_ID, CREATION_DATE, SEQUENCE_ID, EMP_ID, ON_LINE, LINE_NAME  ) "
     //                  + " Values ( '" + tWO_NO + "', '" + tmpPID + "', '" + tDeLinkLineWS + "','" + tStatus + "', '" + tmpDATE + "', "
     //                  + " " + tseqno + ", '" + tUserid + "', '" + tOnline + "', '" + tLINE_NAME + "' ) ";
        string tmpsqlW = "Insert into SFC.MES_ASSY_HISTORY ( WO_NO , PRODUCT_ID, STATION_ID, STATE_ID, SEQUENCE_ID, EMP_ID, ON_LINE, LINE_NAME  ) "
            + " Values ( '" + tWO_NO + "', '" + tmpPID + "', '" + tDeLinkLineWS + "','" + tStatus + "', "
            + " " + tseqno + ", '" + tUserid + "', '" + tOnline + "', '" + tLINE_NAME + "' ) ";
        int RetInt = DataBaseOperation.ExecSQL(DBType, tWriDB, tmpsqlW);
        if (RetInt < 0) RetStr = "";

        string tmpSTATUS = "";
        if (tDeLinkLine != "") tmpSTATUS = tDeLinkLine.Substring(0, 1);

        tmpsqlW = "UPDATE  PUBLIB.MAINPIDTRACE SET  LEVELLINE  = '" + tDeLinkLine + "', LINEWS  = '" + tDeLinkLineWS + "',  CDATE = '" + tCurrtime + "',     "
                + " CSTATUS  = '" + tmpSTATUS + "' WHERE PID = '" + tmpPID + "' ";
        //string tmpsqlW = "Insert into SFC.MES_ASSY_HISTORY ( WO_NO , PRODUCT_ID, STATION_ID, STATE_ID, SEQUENCE_ID, EMP_ID, ON_LINE, LINE_NAME  ) "
        //    + " Values ( '" + tWO_NO + "', '" + tmpPID + "', '" + tDeLinkLineWS + "','" + tStatus + "', "
        //    + " " + tseqno + ", '" + tUserid + "', '" + tOnline + "', '" + tLINE_NAME + "' ) ";
        RetInt = DataBaseOperation.ExecSQL(DBType, tWriDB, tmpsqlW);
        if (RetInt < 0) RetStr = "";

        return("");

    }
    public string DeLinkPidCheck(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] arrDeLinkPid, string tDeLinkLine, string tDeLinkLineWS, string Optype, ref string ReMess)
    {
        return ("");

    }   
  

    //private string DeLinkPidModify(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] tmpRout, string Optype, ref string ReMess, string tDocumentID, string tDocumentIDPid, string tDeLinkLine, string tDeLinkLineWS, string tWO_NO, string tLINE_NAME, string tSTATION_ID)
    //{
    //    return ("");
    //}
 //   private string DeLinkPidBackup(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] tmpRout, string Optype, ref string ReMess, string tDocumentID, string tDocumentIDPid)
 //   {
 //       return ("");
 //   }
    private string DeLinkPidDelete(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] tmpRout, string Optype, ref string ReMess, string tDocumentID, string tDocumentIDPid)
    {
        bool bRet = true;

        DataBaseOperation dbo = new DataBaseOperation(DBType, WriDB);
        #region GetRoutSeq
        string[] RoutNO = new string[0];
        try
        {
            for (int i = 0; i < tmpRout.GetLength(0); i++)
            {
                if ((tmpRout[i, 3] != null) && (tmpRout[i, 3] != ""))
                {
                    Array.Resize(ref RoutNO, RoutNO.GetLength(0) + 1);
                    RoutNO[RoutNO.GetLength(0) - 1] = tmpRout[i, 3];
                }
            }
        }
        catch (Exception e)
        {
            bRet = false;
            ReMess = e.Message;
        }
        #endregion

        #region GetRoutSql
        /*
         * 0: RoutNo
         * 1: SqlProg
         * 2: PID Column Name
         * 3: Exec Sql
         */
        string[,] RoutExec = new string[RoutNO.GetLength(0), 4];
        if (bRet == true)
        {
            string sSql = "Select SQLPROG,PIDCOLName From PUBLIB.DELINKPIDITEM Where ACTIONITEM = :V_ITEM ";
            for (int i = 0; i < RoutNO.GetLength(0); i++)
            {
                RoutExec[i, 0] = RoutNO[i];
                DataTable dt = dbo.SelectSQLDT(sSql, new string[] { "V_ITEM" }, new object[] { RoutNO[i] });
                if (dt.Rows.Count <= 0)
                {
                    bRet = false;
                    ReMess = "Not Found Rout " + RoutNO[i];
                }
                else
                {
                    RoutExec[i, 1] = dt.Rows[0][0].ToString();
                    RoutExec[i, 2] = dt.Rows[0][1].ToString();
                    RoutExec[i, 3] = dt.Rows[0][0].ToString() + " Where " + dt.Rows[0][1].ToString() + "='" + InputData + "'";
                }
                dt.Dispose();
                if (bRet == false) break; // when error ,exit loop
            }
        }
        #endregion

        #region ExecSQL
        // Write mode : Delete Data;
        if ((bRet == true) && (Optype.ToUpper().Trim() == "W"))
        {
            dbo.BeginTran();
            for (int i = 0; i < RoutExec.GetLength(0); i++)
            {
                int iRet = dbo.ExecSQLTran(RoutExec[i, 3]);
                if (iRet < 0)
                {
                    ReMess = DataBaseOperation.GetError();
                    bRet = false;
                    break;
                }

            }
            if (bRet == true)
            {
                dbo.CommitTran();
            }
            else
            {
                dbo.Rollback();
            }
        }
        #endregion

        if (bRet == true)
        {
            return tDocumentIDPid;
        }
        else
        {
            return "-1";
        }


    }
    
    private string tmpDeLinkPidDelete(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] tmpRout, string Optype, ref string ReMess, string tDocumentID, string tDocumentIDPid)
    {
        return ("");
    }
        
    private string GetDeLinkData1( string ReadDB, string InputData, ref string tWO, ref string tLINE, ref string tEMEI)
    {
        int v1=0, v2=0, v3=0, v4=0;
        string p1 = "", p2 = "", p3 = "", p4 = "", p5 = "";
        string p11 = "", p22 = "", p33 = "", p44 = "";
        string p111 = "", p222 = "", p333 = "", p444 = "";
        string tmpsql = " select * from shp.cmcs_sfc_shipping_data where PRODUCTID = '" + InputData + "' ";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(DBType, ReadDB, tmpsql);
        if ( ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 > 0)
        {
            p1 = ds1.Tables[0].Rows[v2]["WORK_ORDER"].ToString();
            p2 = ds1.Tables[0].Rows[v2]["SERIAL_NO"].ToString();
            p3 = ds1.Tables[0].Rows[v2]["IMEI"].ToString();
            p4 = ds1.Tables[0].Rows[v2]["PRODUCTID"].ToString();
            p5 = ds1.Tables[0].Rows[v2]["STATUS"].ToString();
        }

        tmpsql = " select * from shp.cmcs_sfc_imeinum where PRODUCT_ID = '" + InputData + "' ";
        ds1 = DataBaseOperation.SelectSQLDS(DBType, ReadDB, tmpsql);
        if (ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 > 0)
        {
            p11 = ds1.Tables[0].Rows[v2]["PORDER"].ToString();
            p22 = ds1.Tables[0].Rows[v2]["PPART"].ToString();
            p33 = ds1.Tables[0].Rows[v2]["IMEINUM"].ToString();
            p44 = ds1.Tables[0].Rows[v2]["PRODUCT_ID"].ToString();
        }

        tmpsql = " select * from sfc.mes_assy_pid_join where MAIN_ID = '" + InputData + "' ";
        ds1 = DataBaseOperation.SelectSQLDS(DBType, ReadDB, tmpsql);
        if (ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 > 0)
        {
            p111 = ds1.Tables[0].Rows[v2]["WO_NO"].ToString();
            p222 = ds1.Tables[0].Rows[v2]["MAIN_ID"].ToString();
           
        }

        tWO = p1;
        tLINE = "";
        tEMEI = p3;
           
        return("");
   //      shp.cmcs_sfc_shipping_data,
   // shp.cmcs_sfc_imeinum ,
   // sfc.mes_assy_pid_join

    }

    public string GetDeLinkData(string dbtype, string DBString, string PID, ref string WO_NO, ref string LINE_NAME, ref string STATION_ID, ref string IMEI, ref string STATUS, ref string SFC_LEVEL)
    {

        string sqlStr = "SELECT  B.WO_NO,C.IMEI,C.STATUS,A.PRODUCT_ID,A.STATION_ID,A. CREATION_DATE, A.STATE_ID,A.LINE_NAME "
             + "FROM (SELECT  WO_NO, PRODUCT_ID,  STATION_ID, STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME, EMP_ID FROM SFC.MES_PCBA_WIP UNION ALL "
             + "SELECT WO_NO, PRODUCT_ID,  STATION_ID, STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME, EMP_ID FROM SFC.MES_ASSY_WIP UNION ALL "
             + "SELECT  WO_NO, PRODUCT_ID,  STATION_ID, STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME, EMP_ID FROM SFC.MES_PACK_WIP UNION ALL "
             + "SELECT WO_NO, PRODUCT_ID,  STATION_ID, STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME, EMP_ID FROM SFC.MES_PCBA_HISTORY WHERE STATION_ID IN ('ARE_IN', 'ARE_OUT', 'GLUE', 'EC', 'BR'))A, SHP.CMCS_SFC_SHIPPING_DATA C,SFC.MES_ASSY_PID_JOIN B "
             + "WHERE  A.PRODUCT_ID=C.PRODUCTID AND B. MAIN_ID=C.PRODUCTID AND C.PRODUCTID  = '" + PID + "' ORDER BY A.CREATION_DATE DESC";
        DataTable dt = DataBaseOperation.SelectSQLDT(dbtype, DBString, sqlStr);
        int v1 = dt.Rows.Count;
        if (v1 > 0)
        {

            WO_NO = dt.Rows[0][0].ToString();       //虫
            IMEI = dt.Rows[0][1].ToString();        //Imei
            STATUS = dt.Rows[0][2].ToString();      //篈
            STATION_ID = dt.Rows[0][4].ToString();  //程沧
            LINE_NAME = dt.Rows[0][7].ToString();   //ネ玻絬
            //莉赣┮妮絬 狾代 ASSY DC
            string sqlStr1 = "select * FROM SFC.SFC_LINE_WS WHERE SFC_WS='" + STATION_ID + "'";
            DataTable dt1 = DataBaseOperation.SelectSQLDT(dbtype, DBString, sqlStr1);
            if (dt1.Rows.Count > 0)
            {
                SFC_LEVEL = dt1.Rows[0][1].ToString(); //程沧┮妮絬 狾代 ASSY DC
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }

        return "1";
    }  // end GetDeLinkData    

    // Param
    // Rtype = "1" Select Data, Rtype = "2" Exec Data
    // dbtype database
    // DBString  db pointer
    // sql1   sql language
    // DataSet tdt  database table pointer
    // string[,] tmpBUFF   array
    // Arrcnt    array number
    // Fieldnum   how many fileds )
    
    public string GetTableData( string Rtype, string dbtype, string DBString, ref string sql1, ref DataSet tdt, ref  string[,] tmpBUFF, string Arrcnt, string Fieldnum )
    {
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, cnt = 0, fieldcnt = 0; ;
        string s1 = "", s2 = "", s3 = "", Rstr = "";
        string tSERIAL_NUMBER = "", tMODEL_NAME = "", tMODEL = "";

        if ((Arrcnt == "") || (Fieldnum == "")) return ("");
        else
        {
            cnt = Convert.ToInt32(Arrcnt);
            fieldcnt = Convert.ToInt32(Fieldnum);
        }
 
        if (Rtype == "1") // Rtype = "1" Select Data
        {
            tdt = DataBaseOperation.SelectSQLDS(dbtype, DBString, sql1);
            if (tdt.Tables.Count > 0) v1 = tdt.Tables[0].Rows.Count;
            if (v1 > 0)
            {
                for ( v4=0; v4 < fieldcnt; v4++)
                      tmpBUFF[cnt, v4+1] = tdt.Tables[0].Rows[0][v4].ToString();
                Rstr = v1.ToString();
            }
        }

        return( Rstr );

    }

    public string UpdateFileSTatus(string P1, string DBType, string ReadDB, string WriDB, string filename, string UFieldsindex, string indextDocumentID, string tstatus, string Ufields)
    {
      string str1 = "", st2 = "";
      int v1=0, v2=0, v3=0;

      // UPDATE  PUBLIB.DELINKPIDMAIN  SET CSTATUS = 'Y' WHERE DOCUMENTIDPID = '2012121513225622' 
      str1 = "UPDATE  " + filename + "  SET " + Ufields + " = '" + tstatus + "' WHERE " + UFieldsindex + " = '" + indextDocumentID + "' ";
      v1 = DataBaseOperation.ExecSQL(DBType, WriDB, str1);
      if ( v1 > 0)
          return ("1");
      else
          return ("0");

    } // end  UpdateFileSTatus

    // InType == "1" Input PID Return IMEI
    // TnType == "2" Input IMEI return PID
    public string GetPIDIMEI(string Type1, string dbtype, string dbstring, string dbWristring, string InTYpe, string IData)  // return IMEI
    {
        string Ret1 = "", sql1 = "";
        if ( InTYpe  == "1" ) // Input PID Ret IMEINUM
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

  


    public string DeLinkPidModify(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] tmpRout, string Optype, ref string ReMess, string tDocumentID, string tDocumentIDPid, string tDeLinkLine, string tDeLinkLineWS, string tWO_NO, string tLINE_NAME, string tSTATION_ID, string tmpIMEI)
    {
        bool bRet = true;

        DataBaseOperation dbo = new DataBaseOperation(DBType, WriDB);
        #region GetRoutSeq
        string[] RoutNO = new string[0];
        try
        {
            for (int i = 0; i < tmpRout.GetLength(0); i++)
            {
                if ((tmpRout[i, 3] != null) && (tmpRout[i, 3] != ""))
                {
                    Array.Resize(ref RoutNO, RoutNO.GetLength(0) + 1);
                    RoutNO[RoutNO.GetLength(0) - 1] = tmpRout[i, 3];
                }
            }
        }
        catch (Exception e)
        {
            bRet = false;
            ReMess = e.Message;
        }
        #endregion

        #region GetRoutSql
        /*
         * 0: RoutNo
         * 1: SqlProg
         * 2: PID Column Name
         * 3: Exec Sql
         */
        string[,] RoutExec = new string[RoutNO.GetLength(0), 4];
        if (bRet == true)
        {
            string sSql = "Select SQLPROG,PIDCOLName,ACTIONMODE,TABNAME From PUBLIB.DELINKPIDITEM Where ACTIONITEM = :V_ITEM ";
            DeLinkPid1lib delinklib1 = new DeLinkPid1lib();
            for (int i = 0; i < RoutNO.GetLength(0); i++)
            {
                RoutExec[i, 0] = RoutNO[i];
                DataTable dt = dbo.SelectSQLDT(sSql, new string[] { "V_ITEM" }, new object[] { RoutNO[i] });
                if (dt.Rows.Count <= 0)
                {
                    bRet = false;
                    ReMess = "Not Found Rout " + RoutNO[i];
                }
                else
                {
                    RoutExec[i, 1] = dt.Rows[0][0].ToString();
                    RoutExec[i, 2] = dt.Rows[0][1].ToString();
                    string actionMode = dt.Rows[0]["ACTIONMODE"].ToString();
                    if (actionMode.ToUpper() == "I")
                    {

                        RoutExec[i, 3] = delinklib1.DeLinkPidInsertSQL(P1, DBType, ReadDB, WriDB, dt.Rows[0]["TABNAME"].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][0].ToString(), RoutNO[i], tWO_NO.Trim(), InputData, tLINE_NAME.Trim(), ref ReMess, ref bRet);

                    }
                    else if (actionMode.ToUpper() == "E")
                    {
                        RoutExec[i, 3] = dt.Rows[0][0].ToString() + " Where " + dt.Rows[0][1].ToString() + "='" + tmpIMEI + "'";
                    }
                    else
                    {
                        RoutExec[i, 3] = dt.Rows[0][0].ToString() + " Where " + dt.Rows[0][1].ToString() + "='" + InputData + "'";
                    }
                }
                dt.Dispose();
                if (bRet == false) break; // when error ,exit loop
            }
        }
        #endregion

        #region ExecSQL
        // Write mode : Delete Data;
        if ((bRet == true) && (Optype.ToUpper().Trim() == "W"))
        {
            dbo.BeginTran();
            for (int i = 0; i < RoutExec.GetLength(0); i++)
            {
                int iRet = dbo.ExecSQLTran(RoutExec[i, 3]);
                if (iRet < 0)
                {
                    ReMess = DataBaseOperation.GetError();
                    bRet = false;
                    break;
                }

            }
            if (bRet == true)
            {
                dbo.CommitTran();
            }
            else
            {
                dbo.Rollback();
            }
        }
        #endregion

        if (bRet == true)
        {
            return tDocumentIDPid;
        }
        else
        {
            return "-1";
        }

    }

    public string GetMAINPIDTRACEData(string P1, String DBType, string DBReadString, string DBWriString)
    {
         int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5=0;
         string st11 = "select * from PUBLIB.MAINPIDTRACE where CDATE like '20120901%' "; // CSTATUS = '000' and CDATE like '20120901%' ";  //  SUBSTR( CSTATUS, 0,1) = 'S' 
         DataSet dsst1 = DataBaseOperation.SelectSQLDS(DBType, DBWriString, st11);
         if (dsst1.Tables.Count > 0) v4 = dsst1.Tables[0].Rows.Count;
         if (v4 <= 0) return("");
         string[,] arrPID = new string[v4 + 1, 10 + 1];
         string  tpart="", tmodel="", tcustomer="", routno="";

         string Ret1 = "", tmpsqlW = "", tmpPID = "", tWO_NO = "", tLINE_NAME = "", tSTATION_ID = "", tIMEI = "", tSTATUS = "", SFC_LEVEL = "", STATION_IDTable = "", LTIME="", tCSTATUS="000"; 
         for (v1 = 0; v1 < v4; v1++)
         {
             tmpPID = ""; tWO_NO = ""; tLINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = ""; SFC_LEVEL = ""; STATION_IDTable = ""; tCSTATUS=""; 
             tmpPID = dsst1.Tables[0].Rows[v1]["PID"].ToString();
             //    et1 = GetPIDData(DBType, DBReadString, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME);
             Ret1 = GetPIDData("1", DBType, DBReadString, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tpart, ref tmodel, ref tcustomer, ref routno);
             if ( ( Ret1 != "" ) && ( Ret1 != "0" ) && ( Ret1 != "-1" ) )
             {
               if ( tSTATUS == "畐" ) tCSTATUS = "S00";
               else                       tCSTATUS = "000";
         //      tmpsqlW = "UPDATE PUBLIB.MAINPIDTRACE SET CSTATUS = '" + tCSTATUS + "', IMEI =  '" + tIMEI + "', WO =  '" + tWO_NO + "',   LEVELLINE =  '" + SFC_LEVEL + "', LINEWS =  '" + tSTATION_ID + "' "
         //      + " where PID =  '" + tmpPID + "' ";
         //      v5 = DataBaseOperation.ExecSQL( DBType, DBWriString, tmpsqlW);
             }

             arrPID[v1 + 1, 0] = Ret1;
             arrPID[v1 + 1, 1] = tmpPID;
             arrPID[v1 + 1, 2] = tIMEI;
             arrPID[v1 + 1, 3] = tWO_NO;
             arrPID[v1 + 1, 4] = SFC_LEVEL;
             arrPID[v1 + 1, 5] = tSTATION_ID;
             arrPID[v1 + 1, 6] = tCSTATUS;
             arrPID[v1 + 1, 7] = STATION_IDTable;
             arrPID[v1 + 1, 8] = LTIME;
              
         }

         dsst1 = null;

         for (v1 = 0; v1 < v4; v1++)
         {
             tmpPID = ""; tWO_NO = ""; tLINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = ""; SFC_LEVEL = ""; STATION_IDTable = ""; tCSTATUS = "";
             Ret1 = arrPID[v1 + 1, 0].ToString();
             tmpPID = arrPID[v1 + 1, 1].ToString();
             tIMEI = arrPID[v1 + 1, 2].ToString();
             tWO_NO = arrPID[v1 + 1, 3].ToString();
             SFC_LEVEL = arrPID[v1 + 1, 4].ToString();
             tSTATION_ID = arrPID[v1 + 1, 5].ToString();
             tCSTATUS = arrPID[v1 + 1, 6].ToString();
             STATION_IDTable = arrPID[v1 + 1, 7].ToString();
             LTIME = arrPID[v1 + 1, 8].ToString();


             if ((Ret1 != "") && (Ret1 != "0") && (Ret1 != "-1"))
             {
                  tmpsqlW = "UPDATE PUBLIB.MAINPIDTRACE SET CSTATUS = '" + tCSTATUS + "', IMEI =  '" + tIMEI + "', WO =  '" + tWO_NO + "',   LEVELLINE =  '" + SFC_LEVEL + "', LINEWS =  '" + tSTATION_ID + "' "
                       + " where PID =  '" + tmpPID + "' ";
                  v5 = DataBaseOperation.ExecSQL( DBType, DBWriString, tmpsqlW);
             }

          

         }

         return ("");

    } // Get MAINPIDTRACEData end

    public string DeLinkPidBackup(string P1, string DBType, string ReadDB, string WriDB, string tmpDeLinkRoutNo, string DataKind, string InputData, ref string[,] tmpRout, string Optype, ref string ReMess, string tDocumentID, string tDocumentIDPid, string tDeLinkLine, string tDeLinkLineWS, string tmpIMEI)
    {
        string[] rout = new string[1];
        DataBaseOperation dbo = new DataBaseOperation(DBType, WriDB);
        try
        {
            int j = 0;
            int flag = 0;
            for (int i = 0; i < tmpRout.GetLength(0); i++)
            {
                if (tmpRout[i, 3] != null)
                {
                    j++;
                    Array.Resize(ref rout, j);//增長數組長度到j 原值不?；
                    rout[j - 1] = tmpRout[i, 3];//獲取需執行的actionItem
                }
            }
            string strsql = "select distinct tabname,pidcolname,ACTIONITEM,ACTIONMODE from  PUBLIB.DELINKPIDITEM where ACTIONITEM in (";
            for (int i = 0; i < j - 1; i++)
            {
                strsql = strsql + "'" + rout[i] + "',";
            }
            strsql = strsql + "'" + rout[j - 1] + "')";
            //strsql = strsql.Substring(0, strsql.Length - 1);
            //strsql = strsql + ")";
            DataTable dt = dbo.SelectSQLDT(strsql);//獲取需備份數據庫名稱
            if (dt.Rows.Count > 0)
            {
                dbo.BeginTran();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string table = dt.Rows[i]["tabname"].ToString();
                    string colunms = dt.Rows[i]["pidcolname"].ToString();
                    string actionItem = dt.Rows[i]["ACTIONITEM"].ToString();
                    string actionMode = dt.Rows[i]["ACTIONMODE"].ToString();
                    string expt = actionItem.Substring(0, 1);
                    //if (expt != "1")//判斷1開頭為insert 不進行備份
                    //{
                    if (actionMode.ToUpper() == "E")
                    {
                        strsql = "select distinct * from " + table + " where " + colunms + "= '" + tmpIMEI + "'";
                    }
                    else
                    {
                        strsql = "select distinct * from " + table + " where " + colunms + "= '" + InputData + "'";
                    }
                    DataTable dtcon = dbo.SelectSQLDTWithTran(strsql);//獲取備份數據表中數據
                    if (dtcon.Rows.Count > 0)//存在數據執行備份
                    {
                        int colno = 101;
                        for (int m = 0; m < dtcon.Columns.Count; m++)
                        {
                            string colName = dtcon.Columns[m].ToString();
                            string colVal = dtcon.Rows[0][m].ToString();
                            string colType = dtcon.Columns[m].DataType.Name.ToString();
                            strsql = "insert into PUBLIB.DELINKPIDDETAIL (DOCUMENTIDPID,ACTIONITEM,TABNAME,RDATAFIELDNO,RDATA,FILENAME,FIELDTYPE) values (:V_F1,:V_F2,:V_F3,:V_F4,:V_F5,:V_F6,:V_F7)";
                            string F1 = tDocumentIDPid;
                            string F2 = actionItem;
                            string F3 = table;
                            string F4 = colno.ToString();
                            string F5 = colVal;
                            string F6 = colName;
                            string F7 = colType;
                            flag = dbo.ExecSQLTran(strsql, new string[] { "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6", "V_F7" },
                                new object[] { F1, F2, F3, F4, F5, F6, F7 });
                            if (flag == 0)
                            {
                                ReMess = ReMess + "Insert table " + table + " Error ;";
                                dbo.Rollback();
                                return "-1";
                            }
                            colno++;
                        }

                    }
                    //else//2012 12 14 開會決定是否有數據都能執行
                    //{
                    //    if (actionMode.ToUpper().Trim() != "I" && actionMode.ToUpper().Trim() != "D")
                    //    {
                    //        ReMess = ReMess + "The table " + table + " is no rows ;";
                    //        dbo.Rollback();
                    //        return ("-1");
                    //    }

                    //}
                    //}
                }
                strsql = "insert into  PUBLIB.DELINKPIDMAIN (DOCUMENTID,PID,DELINKPIDROUTINGNO,DOCUMENTIDPID,DELINKLINE,DELINKLINEWS)values(:V_F1,:V_F2,:V_F3,:V_F4,:V_F5,:V_F6)";
                string M1 = tDocumentID;
                string M2 = InputData;
                string M3 = tmpDeLinkRoutNo;
                string M4 = tDocumentIDPid;
                string M5 = tDeLinkLine;
                string M6 = tDeLinkLineWS;
                flag = dbo.ExecSQLTran(strsql, new string[] { "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6" },
                                new object[] { M1, M2, M3, M4, M5, M6 });
                if (flag == 0)
                {
                    ReMess = "Insert DELINKPIDMAIN Error!";
                    dbo.Rollback();
                    return ("-1");
                }
                dbo.CommitTran();
            }
            else
            {
                ReMess = "無操作";
                return "-1";
            }

            return (tDocumentIDPid);
        }
        catch (Exception ex)
        {
            ReMess = ex.Message;
            return ("");
        }
        finally
        {
            dbo.Dispose();
        }


    } // end DeLinkPidBackup

    public string GetModelData(string P1, string DBType, string DBWriString, string PID)
    {

        string pidHead = PID.Substring(0, 3);
        string model = "";
        //眖CustomerModelい诀贺の莱PIDHEAD
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
        string wono = "", RoutingNO = "";

        if ((PART.Trim() == "") || (model.Trim() == ""))
        {
            wono = GetWO_NO("P1", DBType, DBReadString, DBReadString1, PID, ref PART);
            model = GetModelData("P1", DBType, DBWriString, PID);
        }

        //沮腹т隔パ
        else
        {
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
        }
        return RoutingNO;
    }//GetRoutingNO


    public string GetWO_NO(string P1, string DBType, string DBReadString, string DBReadString1, string PID, ref string PART)
    {
        string workno = "", tablename = "", fname = "", w_order = "";
        ArrayList creation_date = new ArrayList();
        ArrayList wo_no = new ArrayList();
        ArrayList tabname = new ArrayList();
        ArrayList fieldname = new ArrayList();
        ArrayList order = new ArrayList();
        string strPCBA = "SELECT CREATION_DATE, WO_NO  from  SFC.MES_PCBA_WIP  where PRODUCT_ID = '" + PID + "'";
        DataSet PCBA = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strPCBA);
        if (PCBA.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(PCBA.Tables[0].Rows[0][0].ToString());
            wo_no.Add(PCBA.Tables[0].Rows[0][1].ToString());
            tabname.Add("SHP.CMCS_SFC_SORDER");
            fieldname.Add("SPART");
            order.Add("SORDER");
        }
        else//称215
        {
            strPCBA = "select CREATION_DATE,WO_NO from SFC.MES_PCBA_WIP" +
                                    " where product_id='" + PID + "'";
            PCBA = DataBaseOperation.SelectSQLDS(DBType, DBReadString1, strPCBA);
            if (PCBA.Tables[0].Rows.Count > 0)
            {
                creation_date.Add(PCBA.Tables[0].Rows[0][0].ToString());
                wo_no.Add(PCBA.Tables[0].Rows[0][1].ToString());
                tabname.Add("SHP.CMCS_SFC_SORDER");
                fieldname.Add("SPART");
                order.Add("SORDER");
            }
        }

        string strPACK = "SELECT CREATION_DATE, WO_NO  from  SFC.MES_PACK_WIP  where PRODUCT_ID = '" + PID + "'";
        DataSet PACK = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strPACK);
        if (PACK.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(PACK.Tables[0].Rows[0][0].ToString());
            wo_no.Add(PACK.Tables[0].Rows[0][1].ToString());
            tabname.Add("SHP.CMCS_SFC_PORDER");
            fieldname.Add("PPART");
            order.Add("PORDER");
        }

        string strASSY = "SELECT CREATION_DATE, WO_NO  from  SFC.MES_ASSY_WIP  where PRODUCT_ID = '" + PID + "'";
        DataSet ASSY = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strASSY);
        if (ASSY.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(ASSY.Tables[0].Rows[0][0].ToString());
            wo_no.Add(ASSY.Tables[0].Rows[0][1].ToString());
            tabname.Add("SHP.CMCS_SFC_AORDER");
            fieldname.Add("APART");
            order.Add("AORDER");
        }

        string strHISTORY = "SELECT CREATION_DATE, WO_NO  from  SFC.MES_PCBA_HISTORY  where PRODUCT_ID = '" + PID + "'" +
                            " order by CREATION_DATE desc";
        DataSet HISTORY = DataBaseOperation.SelectSQLDS(DBType, DBReadString, strHISTORY);
        if (HISTORY.Tables[0].Rows.Count > 0)
        {
            creation_date.Add(HISTORY.Tables[0].Rows[0][0].ToString());
            wo_no.Add(HISTORY.Tables[0].Rows[0][1].ToString());
            tabname.Add("SHP.CMCS_SFC_SORDER");
            fieldname.Add("SPART");
            order.Add("SORDER");
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
                    fname = fieldname[i - 1].ToString();
                    w_order = order[i - 1].ToString();
                }
                else
                {
                    workno = wo_no[i].ToString();
                    tablename = tabname[i].ToString();
                    fname = fieldname[i].ToString();
                    w_order = order[i].ToString();
                }
            }
        }
        else
        {
            if (wo_no.Count > 0)
            {
                workno = wo_no[0].ToString();
                tablename = tabname[0].ToString();
                fname = fieldname[0].ToString();
                w_order = order[0].ToString();
            }
        }
        //沮Wo_NoтPart
        string dbstr = "SELECT DISTINCT " + fname + "" +
                       " FROM " + tablename + "" +
                       " WHERE " + w_order + " = '" + workno + "'";
        DataSet dsst2 = DataBaseOperation.SelectSQLDS(DBType, DBReadString, dbstr);
        if (dsst2.Tables[0].Rows.Count > 0)
        {
            PART = dsst2.Tables[0].Rows[0][0].ToString();
        }
        else if ((PART == "") && (workno != ""))
        {
            for (int i = 0; i < tabname.Count; i++)
            {
                dbstr = "SELECT DISTINCT " + fieldname[i] + "" +
                       " FROM " + tabname[i] + "" +
                       " WHERE " + order[i] + " = '" + workno + "'";
                dsst2 = DataBaseOperation.SelectSQLDS(DBType, DBReadString, dbstr);
                if (dsst2.Tables[0].Rows.Count > 0)
                {
                    PART = dsst2.Tables[0].Rows[0][0].ToString();
                    if (PART.Trim() != "") break;
                }
            }
        }

        return workno;
    }//GetWO_NO

    //DBReadString:221    DBWriString:211     DBReadString1:215
    public string GetMAINPIDTRACEData(string P1, string DBType, string DBReadString, string DBWriString, string DBReadString1)
    {
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
        string st11 = "select * from PUBLIB.MAINPIDTRACE where PID='NH1MA224304939' ";
        //string st11 = "select * from PUBLIB.MAINPIDTRACE where CDATE like '20120901%' "; // CSTATUS = '000' and CDATE like '20120901%' ";  //  SUBSTR( CSTATUS, 0,1) = 'S' 
        DataSet dsst1 = DataBaseOperation.SelectSQLDS(DBType, DBWriString, st11);
        if (dsst1.Tables.Count > 0) v4 = dsst1.Tables[0].Rows.Count;
        if (v4 <= 0) return ("");
        string[,] arrPID = new string[v4 + 1, 13 + 1];
        string Ret1 = "", tmpsqlW = "", tmpPID = "", tWO_NO = "", tLINE_NAME = "", tSTATION_ID = "", tIMEI = "", tSTATUS = "", SFC_LEVEL = "", STATION_IDTable = "", LTIME = "", tCSTATUS = "0", tmpPART = "", tRoutingNO = "", tModel = "", tmpCustomer = "";


        for (v1 = 0; v1 < v4; v1++)
        {

            tmpPID = ""; tWO_NO = ""; tLINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = ""; SFC_LEVEL = ""; STATION_IDTable = ""; tCSTATUS = "";
            tmpPID = dsst1.Tables[0].Rows[v1]["PID"].ToString();
            Ret1 = GetPIDData("1", DBType, DBReadString, Backdbstring, Backdbstring, tmpPID, ref tWO_NO, ref tLINE_NAME, ref tSTATION_ID, ref tIMEI, ref tSTATUS, ref SFC_LEVEL, ref STATION_IDTable, ref LTIME, ref tModel, ref tRoutingNO, ref tmpPART, ref tmpCustomer);
            if ((Ret1 != "") && (Ret1 != "0") && (Ret1 != "-1"))
            {
                if (tSTATUS == "畐") tCSTATUS = "S";
                else
                {
                    switch (STATION_IDTable)
                    {
                        case "SFC.MES_PCBA_WIP": tCSTATUS = "P"; break;
                        case "SFC.MES_PCBA_HISTORY": tCSTATUS = "P"; break;
                        case "SFC.MES_ASSY_WIP": tCSTATUS = "A"; break;
                        case "SFC.MES_PACK_WIP": tCSTATUS = "D"; break;
                    }
                }
                //      tmpsqlW = "UPDATE PUBLIB.MAINPIDTRACE SET CSTATUS = '" + tCSTATUS + "', IMEI =  '" + tIMEI + "', WO =  '" + tWO_NO + "',   LEVELLINE =  '" + SFC_LEVEL + "', LINEWS =  '" + tSTATION_ID + "' "
                //      + " where PID =  '" + tmpPID + "' ";
                //      v5 = DataBaseOperation.ExecSQL( DBType, DBWriString, tmpsqlW);
            }

            arrPID[v1 + 1, 0] = Ret1;
            arrPID[v1 + 1, 1] = tmpPID;
            arrPID[v1 + 1, 2] = tIMEI;
            arrPID[v1 + 1, 3] = tWO_NO;
            arrPID[v1 + 1, 4] = SFC_LEVEL;
            arrPID[v1 + 1, 5] = tSTATION_ID;
            arrPID[v1 + 1, 6] = tCSTATUS;
            arrPID[v1 + 1, 7] = STATION_IDTable;
            arrPID[v1 + 1, 8] = LTIME;
            arrPID[v1 + 1, 9] = tmpPART;
            arrPID[v1 + 1, 10] = tRoutingNO;
            arrPID[v1 + 1, 11] = tModel;
        }

        dsst1 = null;

        for (v1 = 0; v1 < v4; v1++)
        {
            tmpPID = ""; tWO_NO = ""; tLINE_NAME = ""; tSTATION_ID = ""; tIMEI = ""; tSTATUS = ""; SFC_LEVEL = ""; STATION_IDTable = ""; tCSTATUS = ""; tmpPART = ""; tRoutingNO = ""; tModel = "";
            Ret1 = arrPID[v1 + 1, 0].ToString();
            tmpPID = arrPID[v1 + 1, 1].ToString();
            tIMEI = arrPID[v1 + 1, 2].ToString();
            tWO_NO = arrPID[v1 + 1, 3].ToString();
            SFC_LEVEL = arrPID[v1 + 1, 4].ToString();
            tSTATION_ID = arrPID[v1 + 1, 5].ToString();
            tCSTATUS = arrPID[v1 + 1, 6].ToString();
            STATION_IDTable = arrPID[v1 + 1, 7].ToString();
            LTIME = arrPID[v1 + 1, 8].ToString();
            tmpPART = arrPID[v1 + 1, 9].ToString();
            tRoutingNO = arrPID[v1 + 1, 10].ToString();
            tModel = arrPID[v1 + 1, 11].ToString();

            if ((Ret1 != "") && (Ret1 != "0") && (Ret1 != "-1"))
            {
                tmpsqlW = "UPDATE PUBLIB.MAINPIDTRACE SET CSTATUS = '" + tCSTATUS + "', IMEI =  '" + tIMEI + "', WO =  '" + tWO_NO + "',   LEVELLINE =  '" + SFC_LEVEL + "', LINEWS =  '" + tSTATION_ID + "',PART =  '" + tmpPART + "',ROUTINGNO =  '" + tRoutingNO + "',MODEL =  '" + tModel + "' "
                     + " where PID =  '" + tmpPID + "' ";
                v5 = DataBaseOperation.ExecSQL(DBType, DBWriString, tmpsqlW);
            }
        }

        return ("");

    } // Get MAINPIDTRACEData end

    //GetPIDData把计Rtype
    // Param
    // Rtype = "1" Select Data, Rtype = "2" Exec Data
    // dbtype database
    // DBString  db pointer
    // sql1   sql language
    // DataSet tdt  database table pointer
    // string[,] tmpBUFF   array
    // Arrcnt    array number
    // Fieldnum   how many fileds )
    public string GetPIDData(string Rtype, string dbtype, string DBString, string DBReadString1, string DBWriString, string PID, ref string WO_NO, ref string LINE_NAME, ref string tSTATION_ID, ref string IMEI, ref string STATUS, ref string SFC_Level, ref string STATION_IDTable, ref string LTIME, ref string Model, ref string RoutingNo, ref string PART, ref string Customer)
    {
        // Test Only 20121220
        // string TestDBPath = ConfigurationManager.AppSettings["L8StandByConnectionString"];
        // DBString = TestDBPath;
        // End Test

        DateTime d1 = DateTime.Today;
        int v1 = 0, v2 = 20, v3 = 0, ArrPtr = 1, Datecnt = 0;
        string[,] tmpBUFF = new string[v2 + 1, v2 + 1];
        for (v1 = 0; v1 < v2 + 1; v1++)
            for (v3 = 0; v3 < v2 + 1; v3++)
                tmpBUFF[v1, v3] = "";

        v1 = 0; v2 = 0; v3 = 0;
        string tPID = PID, Rets1 = "", tmp1 = "", tmp2 = "";
        string tSERIAL_NUMBER = "", tMODEL_NAME = "", tMODEL = "";
        DataSet tdt = null;
        if (Rtype == "1")
        {
            string str1 = " SELECT A.SERIAL_NUMBER tmpSERIAL_NUMBER, A.MODEL_NAME tmpMODEL_NAME, B.MODEL tmpMODEL FROM sfc.r_wip_tracking_t_pid A,SFC.CMCS_SFC_PCBA_BARCODE_CTL B "
                 + " WHERE A.MODEL_NAME=B.SPART AND A.SERIAL_NUMBER = '" + tPID + "' "; // 52EE11105146347'
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "sfc.r_wip_tracking_t_pid";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "3");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                tSERIAL_NUMBER = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // PID
                tMODEL_NAME = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    // Part
                tMODEL = tmpBUFF[Convert.ToInt32(ArrPtr), 3];  // tdt.Tables[0].Rows[0]["tmpMODEL"].ToString();         // MODEL 
            }

            ArrPtr++;
            string ttMODEL = "", tCUSTOMER_NAME = "";
            str1 = " SELECT DISTINCT SUBSTR(MODEL,1,3) AA, CUSTOMER_NAME BB FROM SFC.CMCS_SFC_MODEL WHERE SUBSTR(MODEL,1,3) = '" + tMODEL + "' ";
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.CMCS_SFC_MODEL";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                ttMODEL = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
                tCUSTOMER_NAME = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //             
            }
            Customer = tCUSTOMER_NAME;

            ArrPtr++;
            v2 = 0;
            string tLevel = "", tLineWS = "";
            str1 = " SELECT SFC_LEVEL, SFC_WS  FROM  SFC.SFC_LINE_WS   WHERE CUSTOMER_NO = '" + tCUSTOMER_NAME + "' ";  // and SFC_WS = '" + tCUSTOMER_NAME + "'
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.SFC_LINE_WS";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                tLevel = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
                tLineWS = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //             
            }

            ArrPtr++;
            v2 = 0;
            string CMCS_SFC_SHIPPING_DATAtime = "", CMCS_SFC_SHIPPING_DATASTATUS = "";
            str1 = " SELECT DDATE, STATUS  from  SHP.CMCS_SFC_SHIPPING_DATA where PRODUCTID = '" + tPID + "' ";
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SHP.CMCS_SFC_SHIPPING_DATA";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                tmp1 = ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");
                tmpBUFF[Convert.ToInt32(ArrPtr), 1] = tmp1;
                CMCS_SFC_SHIPPING_DATAtime = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
                CMCS_SFC_SHIPPING_DATASTATUS = tmpBUFF[Convert.ToInt32(ArrPtr), 2];
                STATUS = CMCS_SFC_SHIPPING_DATASTATUS;  // Last SHIPPING STATUS
                // MES_PCBA_WIP_PAUSEtime = ((DateTime)(tdt.Tables[0].Rows[0]["CREATION_DATE"])).ToString("yyyyMMddHHmmssmm");          
            }

            ArrPtr++;
            Datecnt = ArrPtr; // Register Datetime
            v2 = 0;
            string MES_PACK_WIPtime = "";
            str1 = " SELECT CREATION_DATE, WO_NO, STATION_ID, LINE_NAME  from  SFC.MES_PACK_WIP where PRODUCT_ID = '" + tPID + "' ";
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.MES_PACK_WIP";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "4");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                tmp1 = ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");
                tmpBUFF[Convert.ToInt32(ArrPtr), 1] = tmp1;
                MES_PACK_WIPtime = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
                // MES_PCBA_WIP_PAUSEtime = ((DateTime)(tdt.Tables[0].Rows[0]["CREATION_DATE"])).ToString("yyyyMMddHHmmssmm");          
            }


            ArrPtr++;
            v2 = 0;
            string MES_ASSY_WIPtime = "";
            str1 = " SELECT CREATION_DATE, WO_NO, STATION_ID, LINE_NAME  from  SFC.MES_ASSY_WIP where PRODUCT_ID = '" + tPID + "' ";
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.MES_ASSY_WIP";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "4");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                tmp1 = ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");
                tmpBUFF[Convert.ToInt32(ArrPtr), 1] = tmp1;
                MES_ASSY_WIPtime = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
                // MES_PCBA_WIP_PAUSEtime = ((DateTime)(tdt.Tables[0].Rows[0]["CREATION_DATE"])).ToString("yyyyMMddHHmmssmm");          
            }

            ArrPtr++;
            v2 = 0;
            string MES_PCBA_WIP_PAUSEtime = "";
            str1 = " SELECT CREATION_DATE, WO_NO, STATION_ID, LINE_NAME from  SFC.MES_PCBA_WIP where PRODUCT_ID = '" + tPID + "' ";
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.MES_PCBA_WIP";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "4");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                // tmp1 = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
                // d1 = Convert.ToDateTime(tmp1);
                // tmp1 = ((DateTime)( d1 )).ToString("yyyyMMddHHmmssmm");  
                tmp1 = ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");
                tmpBUFF[Convert.ToInt32(ArrPtr), 1] = tmp1;
                MES_PCBA_WIP_PAUSEtime = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
                // MES_PCBA_WIP_PAUSEtime = ((DateTime)(tdt.Tables[0].Rows[0]["CREATION_DATE"])).ToString("yyyyMMddHHmmssmm");          
            }

            ArrPtr++;
            v2 = 0;
            string MES_PCBA_HISTORYtime = "";
            str1 = " SELECT CREATION_DATE, WO_NO, STATION_ID, LINE_NAME  from  SFC.MES_PCBA_HISTORY where PRODUCT_ID = '" + tPID + "' ";
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.MES_PCBA_HISTORY";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "4");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                tmp1 = ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");
                tmpBUFF[Convert.ToInt32(ArrPtr), 1] = tmp1;
                MES_PCBA_HISTORYtime = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
                // MES_PCBA_WIP_PAUSEtime = ((DateTime)(tdt.Tables[0].Rows[0]["CREATION_DATE"])).ToString("yyyyMMddHHmmssmm");          
            }


            // Get MaxTime
            tmp1 = "0"; v2 = 4;
            for (v1 = Datecnt; v1 <= ArrPtr; v1++)  // 眖Τ癘魁ら戳秨﹍
            {
                tmp2 = tmpBUFF[v1, 1].ToString();
                if ((tmp2 != "") && (Convert.ToInt64(tmp1) < Convert.ToInt64(tmp2)))
                {
                    v2 = v1;
                    tmp1 = tmpBUFF[v1, 1].ToString();
                    STATION_IDTable = tmpBUFF[v1, 0].ToString();
                    LTIME = tmpBUFF[v1, 1].ToString();
                    WO_NO = tmpBUFF[v1, 2].ToString();
                    tSTATION_ID = tmpBUFF[v1, 3].ToString();
                    LINE_NAME = tmpBUFF[v1, 4].ToString();
                }

            }

            ArrPtr++;
            v2 = 0;
            // string tLevel = "", tLineWS = "";
            str1 = " SELECT SFC_LINE, SFC_WS  FROM  SFC.SFC_LINE_WS   WHERE CUSTOMER_NO = '" + tCUSTOMER_NAME + "' and SFC_WS = '" + tSTATION_ID + "' ";
            tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.SFC_LINE_WS";
            Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
            if ((Rets1 != "") && (Rets1 != "0"))
            {
                tLevel = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
                tLineWS = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //      
                SFC_Level = tLevel;  // Get Data
            }

            IMEI = GetPIDIMEI("1", dbtype, DBString, DBString, "1", PID);  // return IMEI
            WO_NO = GetWO_NO("P1", DBType, DBString, DBReadString1, PID, ref PART);
            Model = GetModelData("P1", DBType, DBString, PID);
            RoutingNo = GetRoutingNO("P1", DBType, DBString, DBWriString, DBReadString1, PID, ref PART, ref Model);

            if (tmp1 == "0") tmp1 = "";
        }
        return (tmp1);
    }
       
    
    public string GetPIDDataL6(string Rtype, string dbtype, string DBString, string DBReadString1, string DBWriString, string PID, ref string WO_NO, ref string LINE_NAME, ref string tSTATION_ID, ref string IMEI, ref string STATUS, ref string SFC_Level, ref string STATION_IDTable, ref string LTIME, ref string Model, ref string RoutingNo, ref string PART, ref string Customer)
    {
        DateTime d1 = DateTime.Today;
        int v1 = 0, v2 = 20, v3 = 0, ArrPtr = 1, Datecnt = 0;
        string[,] tmpBUFF = new string[v2 + 1, v2 + 1];
        for (v1 = 0; v1 < v2 + 1; v1++)
            for (v3 = 0; v3 < v2 + 1; v3++)
                tmpBUFF[v1, v3] = "";

        v1 = 0; v2 = 0; v3 = 0;
        string tPID = PID, Rets1 = "", tmp1 = "", tmp2 = "", tWO = "";
        string tSERIAL_NUMBER = "", tpart = "", tMODEL = "", tSTATION_NAME = "";
        DataSet tdt = null;
        if (Rtype != "1") return ("");

        string str1 = " SELECT A.SERIAL_NUMBER tmpSERIAL_NUMBER, A.MODEL_NAME tmppart, A.STATION_NAME tSTATION_NAME, B.MODEL tmpMODEL, "
             + " A.MO_NUMBER tmpWO FROM sfc.r_wip_tracking_t_pid A,SFC.CMCS_SFC_PCBA_BARCODE_CTL B "
             + " WHERE A.MODEL_NAME=B.SPART AND A.SERIAL_NUMBER = '" + tPID + "' "; // 52EE11105146347'
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "sfc.r_wip_tracking_t_pid";
        Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "5");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            tSERIAL_NUMBER = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // PID
            tpart = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    // Part
            tSTATION_NAME = tmpBUFF[Convert.ToInt32(ArrPtr), 3];  // tdt.Tables[0].Rows[0]["tmpMODEL"].ToString();         // MODEL 
            tMODEL = tmpBUFF[Convert.ToInt32(ArrPtr), 4];  // MODEL
            tWO = tmpBUFF[Convert.ToInt32(ArrPtr), 5];  // WO 
        }

        ArrPtr++;
        string ttMODEL = "", tCUSTOMER_NAME = "";
        str1 = " SELECT DISTINCT SUBSTR(MODEL,1,3) AA, CUSTOMER_NAME BB FROM SFC.CMCS_SFC_MODEL WHERE SUBSTR(MODEL,1,3) = '" + tMODEL + "' ";
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.CMCS_SFC_MODEL";
        Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            ttMODEL = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
            tCUSTOMER_NAME = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //             
        }
        Customer = tCUSTOMER_NAME;

        ArrPtr++;
        v2 = 0;
        string tLevel = "", tLineWS = "";
        str1 = " SELECT SFC_LINE, SFC_WS  FROM  SFC.SFC_LINE_WS   WHERE CUSTOMER_NO = '" + tCUSTOMER_NAME + "' ";  // and SFC_WS = '" + tCUSTOMER_NAME + "'
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.SFC_LINE_WS";
        Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            tLevel = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
            tLineWS = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //             
        }

        string sqls2 = "select ROUTING_SEQUENCE_ID, PART_NUMBER from  SFC.SFC_ROUTING_HEADERS " +
                            " where model_name='" + tMODEL + "'  order by PART_NUMBER desc ";
                      //      " and (PART_NUMBER='' or PART_NUMBER is null)";

        int v7=0, v8=0, v9=0;
        string sp1 = "";
        DataSet dt2 = DataBaseOperation.SelectSQLDS( DBType, DBString, sqls2);
        if (dt2.Tables.Count <= 0) RoutingNo = "";
        else
        {
            v3 = dt2.Tables[0].Rows.Count;
            if (v3 <= 0) RoutingNo = "";
            else if (v3 == 1) RoutingNo = dt2.Tables[0].Rows[v7]["ROUTING_SEQUENCE_ID"].ToString();
            else
            {
                for (v7 = 0; v7 < v3; v7++)
                {
                    if (tpart == dt2.Tables[0].Rows[v7]["PART_NUMBER"].ToString())  v8 = v7 + 1;
                    else if (sp1 == dt2.Tables[0].Rows[v7]["PART_NUMBER"].ToString()) v9 = v7 + 1;                     
                }

                if (v8 > 0) RoutingNo = dt2.Tables[0].Rows[v8-1]["ROUTING_SEQUENCE_ID"].ToString();
                else RoutingNo = dt2.Tables[0].Rows[v9 - 1]["ROUTING_SEQUENCE_ID"].ToString();
            }
                            
            // RoutingNo = GetRoutingNO("P1", DBType, DBString, DBString, DBString, tSERIAL_NUMBER, ref tpart, ref tMODEL);
        }
        tSTATION_ID = tSTATION_NAME;
        IMEI = IMEI = GetPIDIMEI("1", dbtype, DBString, DBString, "1", PID);
        STATUS = "1";
        SFC_Level = tLevel;
        STATION_IDTable = "sfc.r_wip_tracking_t_pid";
        LTIME = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        Model = tMODEL;
        RoutingNo = RoutingNo;
        PART = tpart;
        Customer = tCUSTOMER_NAME;
        WO_NO = tWO;

        if ((Model == "") || (PART == "") || (WO_NO == "") || (RoutingNo == "")) STATUS = "0";

        return (PID);
        // Close this Proc

        ArrPtr++;
        v2 = 0;
        string MES_PCBA_WIP_PAUSEtime = "";
        str1 = " SELECT CREATION_DATE, WO_NO, STATION_ID, LINE_NAME from  SFC.MES_PCBA_WIP where PRODUCT_ID = '" + tPID + "' ";
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.MES_PCBA_WIP";
        Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "4");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            // tmp1 = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
            // d1 = Convert.ToDateTime(tmp1);
            // tmp1 = ((DateTime)( d1 )).ToString("yyyyMMddHHmmssmm");  
            tmp1 = ((DateTime)(Convert.ToDateTime(tmpBUFF[Convert.ToInt32(ArrPtr), 1]))).ToString("yyyyMMddHHmmssmm");
            tmpBUFF[Convert.ToInt32(ArrPtr), 1] = tmp1;
            MES_PCBA_WIP_PAUSEtime = tmpBUFF[Convert.ToInt32(ArrPtr), 1];
            // MES_PCBA_WIP_PAUSEtime = ((DateTime)(tdt.Tables[0].Rows[0]["CREATION_DATE"])).ToString("yyyyMMddHHmmssmm");          
        }

        // Get MaxTime
        tmp1 = "0"; v2 = 4;
        for (v1 = Datecnt; v1 <= ArrPtr; v1++)  // 眖Τ癘魁ら戳秨﹍
        {
            tmp2 = tmpBUFF[v1, 1].ToString();
            if ((tmp2 != "") && (Convert.ToInt64(tmp1) < Convert.ToInt64(tmp2)))
            {
                v2 = v1;
                tmp1 = tmpBUFF[v1, 1].ToString();
                STATION_IDTable = tmpBUFF[v1, 0].ToString();
                LTIME = tmpBUFF[v1, 1].ToString();
                WO_NO = tmpBUFF[v1, 2].ToString();
                tSTATION_ID = tmpBUFF[v1, 3].ToString();
                LINE_NAME = tmpBUFF[v1, 4].ToString();
            }

        }

        ArrPtr++;
        v2 = 0;
        // string tLevel = "", tLineWS = "";
        str1 = " SELECT SFC_LINE, SFC_WS  FROM  SFC.SFC_LINE_WS   WHERE CUSTOMER_NO = '" + tCUSTOMER_NAME + "' and SFC_WS = '" + tSTATION_ID + "' ";
        tmpBUFF[Convert.ToInt32(ArrPtr), 0] = "SFC.SFC_LINE_WS";
        Rets1 = GetTableData("1", dbtype, DBString, ref str1, ref tdt, ref tmpBUFF, ArrPtr.ToString(), "2");
        if ((Rets1 != "") && (Rets1 != "0"))
        {
            tLevel = tmpBUFF[Convert.ToInt32(ArrPtr), 1]; // tdt.Tables[0].Rows[0]["tmpSERIAL_NUMBER"].ToString(); // 
            tLineWS = tmpBUFF[Convert.ToInt32(ArrPtr), 2]; // tdt.Tables[0].Rows[0]["tmpMODEL_NAME"].ToString();    //      
            SFC_Level = tLevel;  // Get Data
        }

        //         IMEI = GetPIDIMEI("1", dbtype, DBString, DBString, "1", PID);  // return IMEI
        //         WO_NO = GetWO_NO("P1", DBType, DBString, DBReadString1, PID, ref PART);
        //         Model = GetModelData("P1", DBType, DBString, PID);
        //         RoutingNo = GetRoutingNO("P1", DBType, DBString, DBWriString, DBReadString1, PID, ref PART, ref Model);

        if (tmp1 == "0") tmp1 = "";

        return (tmp1);

    } // end GetPIDDataL6 


}  // end public class DeLinkPId 
}  // end namespace SFC.TJWEB


