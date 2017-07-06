using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.OracleClient;

/// <summary>
/// Summary description for Monitor
/// </summary>
public class Monitor
{
    public static string FERROR = "";
	public Monitor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string getError()
    {
        return FERROR;
    }

    public static int GetDBList(string FileName, string sDBNameList, ref string[,] RET)
    {
        int iRet = 0;
        if (File.Exists(FileName) == false)
        {
            iRet = 1;
            FERROR = "File not exists";
        }
        while (iRet == 0)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(FileName);
                XmlNode xn = xmldoc.SelectSingleNode("DataBase");
                XmlNode xnc = xn.SelectSingleNode(sDBNameList);
                XmlNodeList xnl = xnc.ChildNodes;
                RET = new string[xnl.Count, 2];
                int i;
                for (i = 0; i < xnl.Count; i++)
                {
                    XmlElement xe = (XmlElement)xnl[i];
                    RET[i, 0] = xe.GetAttribute("Name");
                    RET[i, 1] = xe.GetAttribute("DBConn");
                }

            }
            catch (Exception e)
            {
                FERROR = e.Message;
                iRet = -1;
            }
            break;
        }
        return iRet;
    }


    // This function will get dbname & conntionstr from DBXML.xml file
    // return 0: success, non-zero error
    public static int GetDBList(string FileName,ref string[,] RET)
    {
        int iRet = 0;
        if (File.Exists(FileName) == false)
        {
            iRet = 1;
            FERROR = "File Not Exist";
        }
        while (iRet == 0)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(FileName);
                XmlNode xn = xmldoc.SelectSingleNode("DataBase");
                XmlNodeList xnl = xn.ChildNodes;
                RET = new string[xnl.Count, 2];
                int i;
                for (i = 0; i < xnl.Count; i++)
                {
                    XmlElement xe = (XmlElement)xnl[i];
                    RET[i, 0] = xe.GetAttribute("Name");
                    RET[i, 1] = xe.GetAttribute("DBConn");
                }
            }
            catch (Exception e)
            {
                FERROR = e.Message;
                iRet = -1;
            }
            break;
        }

        return iRet;
    }

}

public class Monitor_UsedOrder
{
    private static string FERROR = "";

    public static string GetError()
    {
        return FERROR;
    }

    public static int GetAbnData(string DBConnection, ref DataTable RET)
    {
        int iRet = 0;
        string sSql = "select T.SERIAL_NUMBER,T.LINE_NAME,T.STATUS_CODE,T.MODEL_NAME,T.USED_ORDER U1,R.USED_ORDER U2 " +
                      "from  SFISM5.RF_BOARD_ONLINE_T  T Left Join  SFISM5.RF_BOARD_RECORD_T  R " +
                      "ON T.Serial_NUmber=R. Serial_NUmber And T. STATUS_CODE=R.STATUS_CODE " +
                      "Where T.USED_ORDER <> R.USED_ORDER ";
        iRet = DB_ORACLE.EXECSQL(DBConnection, sSql, ref RET);
        if (iRet != 0) FERROR = DB_ORACLE.GetError();
        return iRet;
    }

    public static int GetAllData(string DBConnection, ref DataTable RET)
    {
        int iRet = 0;
        string sSql = "select T.SERIAL_NUMBER,T.LINE_NAME,T.STATUS_CODE,T.MODEL_NAME,T.USED_ORDER U1,R.USED_ORDER U2 " +
                      "from  SFISM5.RF_BOARD_ONLINE_T  T Left Join  SFISM5.RF_BOARD_RECORD_T  R " +
                      "ON T.Serial_NUmber=R. Serial_NUmber And T. STATUS_CODE=R.STATUS_CODE ";
        iRet = DB_ORACLE.EXECSQL(DBConnection, sSql, ref RET);
        if (iRet != 0) FERROR = DB_ORACLE.GetError();
        return iRet;
    }
    //
    // Return Effect count
    public static int FixOrder(string DBConnection, string Serial_Number, string Line_Number, string New_Order,string PSW)
    {
        int iRet =0;
        if (PSW.Trim() != "Foxconn88")
        {
            iRet = -2;
            FERROR = "Password is wrong";
        }
        if (iRet == 0)
        {
            string sSql = "Update SFISM5.RF_BOARD_ONLINE_T Set USED_ORDER= :V_ORDER Where Serial_NUmber = :V_SN And LINE_NAME = :V_LN ";
            OracleParameter[] oraParam = new OracleParameter[3];
            oraParam[0] = new OracleParameter("V_ORDER", Convert.ToInt32(New_Order));
            oraParam[1] = new OracleParameter("V_SN", Serial_Number);
            oraParam[2] = new OracleParameter("V_LN", Line_Number);
            iRet = DB_ORACLE.EXECNOQuerywithParam(DBConnection, sSql, oraParam);
            if (iRet == 0)
            {
                FERROR = "No Data be updated";
            }
            else if (iRet < 0)
            {
                FERROR = DB_ORACLE.GetError();
            }
        }
        return iRet;
    
    }

}

public class PIDIMEITrace
{
    private static string FERROR="";
    public PIDIMEITrace()
    { 
    
    }
    public static string GetError()
    {
        return FERROR;
    }

    /*
     * iType 
     * 0: PID
     * 1: IMEI 
     */
    public static int TracePID_IMEI(string connectStr,string str, int iType,ref DataTable dt_WO,ref DataTable dt_Process,ref DataTable dt_Relation)
    {
        int iRet = 0;
        string sIMEI, sPID,sModel;
        string sSql;
        OracleParameter[] param = new OracleParameter[0];
       

        while (iRet == 0)
        {
            DataTable dtt = new DataTable();
            OracleOperate op = new OracleOperate(connectStr);

            #region FoundPID
            if (iType == 1)
            {
                sIMEI = str;
                string strSql = "Select product_id,ppart from SHP.CMCS_SFC_IMEINUM where imeinum =:V_IMEI ";
                param = new OracleParameter[1];
                param[0] = new OracleParameter("V_IMEI", str);

                iRet = op.ExecSqlWithParam(strSql, param, ref dtt);
                if (iRet != 0)
                {
                    FERROR = op.GetError();
                    break;
                }
                if (dtt.Rows.Count > 0)
                {
                    sPID = dtt.Rows[0][0].ToString().Trim();
                    sModel = dtt.Rows[0][1].ToString().Trim().Substring(2, 3);
                    if (sPID == "")
                    {
                        sSql = "SELECT PRODUCTID FROM CMCS_SFC_SHIPPING_DATA Where IMEI = :V_IMEI";
                        param = new OracleParameter[1];
                        param[0] = new OracleParameter("V_IMEI", str);
                        dtt.Clear();
                        dtt.Columns.Clear();
                        iRet = op.ExecSqlWithParam(sSql, param, ref dtt);
                        if (iRet != 0)
                        {
                            FERROR = op.GetError();
                            break;
                        }
                        if (dtt.Rows.Count > 0)
                        {
                            sPID = dtt.Rows[0][0].ToString();
                        }
                        else
                        {
                            FERROR = "Not found PID ";
                            iRet = 1;
                            break;
                        }
                    }
                }
                else
                {
                    iRet = 1;
                    FERROR = "Not found IMEI";
                    break;
                }
            }
            else
            {
                sPID = str;
            }
            #endregion

            #region GetWo
            sSql = " SELECT 'PCBA' MFG_TYPE,SORDER WO_NO, DB_USER MODEL, A.SPART PN,PID_QTY WO_QTY " +
                    "FROM CMCS_SFC_SORDER A, SFC.CMCS_SFC_PCBA_BARCODE_CTL B " +
                    "WHERE A.SPART = B.SPART " +
                    "AND SORDER IN (SELECT WO_NO FROM MES_PCBA_HISTORY WHERE PRODUCT_ID = :V_PID) " +
                    "UNION " +
                    "SELECT 'ASSY' MFG_TYPE,AORDER WO_NO,DB_USER MODEL,APART PN,APID_QTY WO_QTY " +
                    "FROM CMCS_SFC_AORDER A, SFC.CMCS_SFC_PCBA_BARCODE_CTL B " +
                    "WHERE A.SPART = B.SPART " +
                    "AND AORDER IN (SELECT WO_NO  FROM MES_ASSY_HISTORY  WHERE PRODUCT_ID = :V_PID) " +
                    "UNION " +
                    "SELECT 'PACK' MFG_TYPE, PORDER WO_NO, DB_USER MODEL, PPART PN, PPID_QTY WO_QTY " +
                    "FROM CMCS_SFC_PORDER A, SFC.CMCS_SFC_PCBA_BARCODE_CTL B " +
                    "WHERE A.SPART = B.SPART " +
                    "AND PORDER IN (SELECT WO_NO  FROM MES_PACK_HISTORY  WHERE PRODUCT_ID = :V_PID) ";

            param = new OracleParameter[1];
            param[0] = new OracleParameter("V_PID", sPID);
            iRet = op.ExecSqlWithParam(sSql, param, ref dt_WO);
            if (iRet != 0)
            {
                FERROR = op.GetError();
                break;
            }
            
            #endregion

            #region GETPROCESSINFO
            sSql = "    SELECT MODEL, SPART FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL T  WHERE SPART =  "+
                   " (SELECT SPART FROM SFC.PRODUCT_PANEL_SORDER WHERE PRODUCT_ID = :V_PID)";
            dtt.Clear();
            dtt.Columns.Clear();
            iRet = op.ExecSqlWithParam(sSql,new string[] {"V_PID"},new object[] {sPID},ref dtt);
            if (iRet != 0)
            {
                FERROR = op.GetError();
                break;
            }
            if (dtt.Rows.Count > 0)
            {
                sModel = dtt.Rows[0][0].ToString();
            }
            else
            {
                sSql = "Select Substr(MODEL,1,3) MODEL from SHP.CMCS_SFC_SHIPPING_DATA where PRODUCTID = :V_PID ";
                dtt.Clear();
                dtt.Columns.Clear();
                iRet = op.ExecSqlWithParam(sSql, new string[] { "V_PID" }, new object[] { sPID }, ref dtt);
                if (iRet != 0)
                {
                    FERROR = op.GetError();
                    break;
                }
                if (dtt.Rows.Count > 0)
                {
                    sModel = dtt.Rows[0][0].ToString();
                }
                else
                {
                    FERROR = "Not Found PID in CMCS_SFC_PCBA_BARCODE_CTL/SFC.PRODUCT_PANEL_SORDER or CMCS_SFC_SHIPPING_DATA ";
                    iRet = 1;
                    break;
                }

            }

            sSql =  " SELECT TO_CHAR(PDDATE,'YYYY/MM/DD HH24:MI:SS') FCDATE, "+
                    "case(STATION_CODE) when 'D2' THEN 'Re/DL' else station_code end FSTATIONID,STATUS FSTATEID,LINE_CODE FLINE,EMPLOYEE EMP_ID,ERROR_MSG ERROR_CODE "+
                    "FROM "+sModel+".PRODUCT_HISTORY_V T WHERE PRODUCT_ID = :V_PID "+
                    "UNION "+
                    "SELECT TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE SUBSTR(S.STATION_ID, 1, 1) || '_' || SUBSTR(S.STATION_ID, 3, 2) "+
                    "WHEN 'A_BI' THEN 'ASSEMBLY INPUT' WHEN 'A_KT' THEN 'ABASEBAND1' WHEN 'A_CT' THEN 'ABASEBAND2' WHEN 'A_HT' THEN 'ABASEBAND3' "+
                    "WHEN 'A_IT' THEN 'ABASEBAND4' WHEN 'A_DT' THEN 'WIRELESS' WHEN 'A_LT' THEN 'REDOWNLOAD' WHEN 'A_FO' THEN 'FQC' WHEN 'A_MO' THEN 'REWORK' WHEN 'P_CK' THEN 'PACKING ' "+
                    "ELSE S.STATION_ID END FSTATIONID,S.STATE_ID FSTATEID,case length(S.line_name) when 4 then 'LINE'||SUBSTR(S.LINE_NAME, 3, 2) else S.line_name end FLINE,S.EMP_ID, "+
                    "(SELECT JL.DEFECT_CODE||' / '||JL.DESCRIPTION  FROM SFC.MES_REPAIR_DEFECTCODE JL WHERE JL.DEFECT_CODE=(SELECT T.DEFECT_CODE FROM MES_PRODUCT_FAIL_HISTORY T WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_ID = S.STATION_ID AND (S.STATE_ID='F' or (s.state_id='P' and (S.STATION_ID='QR' OR S.STATION_ID='DCQR'))) AND ROWNUM=1)) ERROR_CODE  "+
                    "FROM MES_ASSY_HISTORY S "+
                    "WHERE PRODUCT_ID= :V_PID  "+
                    "UNION "+
                    "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE SUBSTR(STATION_ID, 1, 1) || '_' || SUBSTR(STATION_ID, 3, 2) WHEN 'S_AI' THEN 'Smt INPUT' WHEN 'S_BO' THEN 'X-RAY' "+
                    "WHEN 'S_CO' THEN 'TOUTH UP' WHEN 'T_AI' THEN 'ROUTER' WHEN 'T_BT' THEN 'DOWNLOAD' WHEN 'T_ET' THEN 'CALIBRATION' WHEN 'T_JT' THEN 'PRETEST' "+
                    "WHEN 'T_CT' THEN 'TBASEBAND1' WHEN 'T_NT' THEN 'TBASEBAND2' WHEN 'T_MT' THEN 'BTWIRELESS' WHEN 'T_DT' THEN 'BLUETOOCH' WHEN 'T_FO' THEN 'GLUE' "+
                    "ELSE STATION_ID END FSTATIONID,STATE_ID FSTATEID,'LINE'||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1))-64) FLINE,EMP_ID,''ERROR_CODE FROM MES_PCBA_HISTORY WHERE PRODUCT_ID=:V_PID " +
                    "UNION "+
                    "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,'CFC-LINK' FSTATIONID,'P' FSTATEID,'CFC-LINE' FLINE,'CFC_TEST' EMP_ID,''ERROR_CODE  "+
                    "FROM SHP.CMCS_SFC_IMEINUM "+
                    "WHERE LENGTH(IMEINUM)<14 and LENGTH(IMEINUM)<>8 "+
                    "AND PRODUCT_ID=:V_PID "+
                    "UNION "+
                    "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE SUBSTR(STATION_ID, 1, 1) || '_' || SUBSTR(STATION_ID, 3, 2) WHEN 'P_AT' THEN 'REDOWNLOAD' WHEN 'P_BT' THEN 'E2PCONFIG' "+
                    "WHEN 'P_GO' THEN 'OQC' WHEN 'P_EO' THEN 'OOB' ELSE STATION_ID END FSTATIONID,STATE_ID FSTATEID,'LINE'||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1))-64) FLINE,EMP_ID, "+
                    "(SELECT DEFECT_CODE FROM MES_PRODUCT_FAIL_HISTORY T WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_ID = S.STATION_ID AND S.STATE_ID='F' AND ROWNUM=1)ERROR_CODE "+
                    "FROM MES_PACK_HISTORY S WHERE PRODUCT_ID =:V_PID "+
                    "union   "+
                    "select TO_CHAR(mb.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS')  FCDATE,'Carton 【'||mb.caseid||'】' FSTATIONID,'P' FSTATEID,'LINE'||mb.PACKLINE FLINE,'' EMP_ID,'' ERROR_CODE "+
                    "from shp.CMCS_SFC_SHIPPING_DATA ma,SHP.CMCS_SFC_CARTON mb "+
                    "where ma.CARTON_NO=mb.CARTON_NO and ma.carton_no is not null and ma.PRODUCTID=:V_PID "+
                    "union   "+
                    "select TO_CHAR(nb.SHIP_DATE,'YYYY/MM/DD HH24:MI:SS')  FCDATE,'3S4S 【'||nb.INVOICE_NUMBER||' 】' FSTATIONID,'P' FSTATEID,'' FLINE,'' EMP_ID,''ERROR_CODE "+
                    "from shp.CMCS_SFC_SHIPPING_DATA na,sap.CMCS_SFC_PACKING_LINES_ALL nb "+
                    "where  na.CARTON_NO=nb.INTERNAL_CARTON and na.PRODUCTID= :V_PID and nb.INVOICE_NUMBER is not null ";
            iRet = op.ExecSqlWithParam(sSql,new string[] {"V_PID"},new object[] {sPID},ref dt_Process);
            if (iRet != 0)
            {
                FERROR = op.GetError();
                break;
            }
            #endregion

            #region GetRelation
            sSql = "     SELECT PRODUCTID, IMEI,  T.SERIAL_NUM PICASSO, PPID_NUM,  CARTON_NO CARTONID, T.CUSTOMER_NUM MSN#, TO_char(DDATE, 'YYYY/MM/DD HH24:MI:SS') INDATE, S.STATUS " +
                   " FROM CMCS_SFC_SHIPPING_DATA S, SHP.CMCS_SFC_IMEINUM T "+
                   " WHERE S.WORK_ORDER = T.PORDER "+
                   " AND (S.IMEI = T.IMEINUM OR S.SERIAL_NO = T.SERIAL_NUM) "+
                   " AND S.PRODUCTID = :V_PID ";
            iRet = op.ExecSqlWithParam(sSql,new string[] {"V_PID"},new object[] {sPID},ref dt_Relation);
            if (iRet != 0)
            {
                FERROR = op.GetError();
                break;
            
            }


            #endregion

            break;
        }
        return iRet;
    
    }

}

public class SMTDataTransfer
{
    static string  FERROR = "";
    public SMTDataTransfer()
    { 
    }

    public static string GetError()
    {
        return FERROR;
    }

    private static int CheckTimeFormat(string sTime)
    {
        int iRet = 0;
        try
        {
            if (sTime.Trim() == "")
            {
                iRet = 1;
            }
            else if (sTime.Trim().Length == 8)
            {
                DateTime.ParseExact(sTime, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                iRet = 0;
            }
            else if (sTime.Trim().Length == 10)
            {
                DateTime.ParseExact(sTime, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                iRet = 0;
            }
            else if (sTime.Trim().Length > 14)
            {
                DateTime.ParseExact(sTime.Substring(0, 14), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                iRet = 0;
            }
            else
            {
                DateTime.ParseExact(sTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                iRet = 0;
            }
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        return iRet;
    }

    private static bool FoundInArray(ref string[,] ArrayItem, string Value,int iLoc)
    {
        bool Ret = false;
        int i;
        for (i = 0; i < ArrayItem.GetLength(0); i++)
        {
            if (ArrayItem[i,iLoc].ToString() == Value.ToString())
            {
                Ret = true;
                break;
            }
        }
        return Ret;
    }

    private static void ArrayAddOne(ref string[,] ArrayItem)
    {
        string[,] NewArray = new string [ArrayItem.GetLength(0)+1,ArrayItem.GetLength(1)];
        for (int iRow = 0; iRow < ArrayItem.GetLength(0); iRow++)
        {
            for (int iCol = 0; iCol < ArrayItem.GetLength(1); iCol++)
                NewArray[iRow,iCol] = ArrayItem[iRow,iCol];
        }
        ArrayItem = NewArray;
    
    }


    public static int SMTL6DataTransfer(string DBRead, string DBWrite, string sStart, string sEnd, ref DataTable dt)
    {
        int iRet = 0;
        int[,] aRet = new int[6, 4];
        aRet[0, 0] = 0;  // Source Total Count
        aRet[0, 1] = 0;  // Update Count
        aRet[0, 2] = 0;  // Inset Count;   
        aRet[0, 3] = 0;  // Duplicate Count  
        string[] sTableName = {"T_R_WIP_LOG_T","T_R_WIP_TRACKING_T","R_MO_BASE_T","T_R_WIP_TRACKING_T_PID","T_MES_PCBA_WIP_PAUSE","T_MES_PCBA_HISTORY"};
        if ((CheckTimeFormat(sStart) != 0) || (CheckTimeFormat(sEnd) != 0))
        {
            iRet = 1;
            FERROR = "Input Time Invalid ";
            return iRet;
        }
        DataBaseOperation dboSource = new DataBaseOperation("oracle", DBRead);
        DataBaseOperation dboDest = new DataBaseOperation("oracle", DBWrite);
        string sSql, sSqlDest,sSqlDest2,sSqlDest3;
        int i;
        
        #region T_R_WIP_LOG_T
        
        sSql = "Select SERIAL_NUMBER,GROUP_NAME,To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,MO_NUMBER from SFISM6.R_WIP_LOG_T Where IN_STATION_TIME >= To_Date(:V_Start,'YYYY/MM/DD') And IN_STATION_TIME < To_Date(:V_End,'YYYY/MM/DD') Order By SERIAL_NUMBER";
        sSqlDest = "Select SERIAL_NUMBER,GROUP_NAME,To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,MO_NUMBER from PUBLIB.T_R_WIP_LOG_T Where IN_STATION_TIME >= To_Date(:V_Start,'YYYY/MM/DD') And IN_STATION_TIME < To_Date(:V_End,'YYYY/MM/DD') Order By SERIAL_NUMBER";
        DataTable dtS1 = dboSource.SelectSQLDT(sSql, new string[] { "V_Start", "V_End" }, new object[] { sStart, sEnd });
        aRet[0, 0] = dtS1.Rows.Count;
        DataTable dtD1 = dboDest.SelectSQLDT(sSqlDest, new string[] { "V_Start", "V_End" }, new object[] { sStart, sEnd });
        
        string OLDSN = "";
        DataRow[] dr = new DataRow[0];
        for (i = 0; i < dtS1.Rows.Count; i++)
        {
            string SN = dtS1.Rows[i][0].ToString();
            string F1 = dtS1.Rows[i][1].ToString();
            string F2 = dtS1.Rows[i][2].ToString();
            string F3 = dtS1.Rows[i][3].ToString();
            if (OLDSN != SN)
            {
                dr = dtD1.Select("SERIAL_NUMBER='" + SN + "' ");
                OLDSN = SN;
            }
            bool bFound = false;
            int i2;
            for (i2 = 0; i2 < dr.GetLength(0); i2++)
            {
                if ((SN == dr[i2][0].ToString()) && (F1 == dr[i2][1].ToString()) && (F2 == dr[i2][2].ToString()) && (F3 == dr[i2][3].ToString()))
                {
                    bFound = true;
                    break;
                }
            }
            if (bFound == false)
            {
                sSql = "Insert Into PUBLIB.T_R_WIP_LOG_T (SERIAL_NUMBER,GROUP_NAME,IN_STATION_TIME,MO_NUMBER) Values (:V_SN,:V_GN,To_Date(:V_IS,'YYYY/MM/DD HH24:Mi:SS'),:V_MO) ";
                dboDest.ExecSQL(sSql, new string[] { "V_SN", "V_GN", "V_IS", "V_MO" }, new object[] { SN, F1, F2, F3 });
                aRet[0, 2]++;
            }
            else
            {
                aRet[0, 3]++;
            }
        }
        dtS1.Dispose();
        dtD1.Dispose();
        
        #endregion
        
        #region SFISM6.R_WIP_TRACKING_T 
        
        sSql = @"select 
                SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                from SFISM6.R_WIP_TRACKING_T
                Where IN_STATION_TIME >= To_Date(:V_S,'YYYY/MM/DD') And IN_STATION_TIME < To_Date(:V_E,'YYYY/MM/DD') ";
        sSqlDest = @"select 
                SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                from PUBLIB.T_R_WIP_TRACKING_T
                Where SERIAL_NUMBER = :V_SN ";
        DataTable dtS2 =  dboSource.SelectSQLDT(sSql, new string[] { "V_S", "V_E" }, new object[] { sStart, sEnd });
        aRet[1, 0] = dtS2.Rows.Count;
        aRet[1, 1] = 0;
        aRet[1, 2] = 0;
        aRet[1, 3] = 0;
        for (i = 0; i < dtS2.Rows.Count; i++)
        {
            string SN = dtS2.Rows[i][0].ToString();
            string[] FA = new string[dtS2.Columns.Count];
            int i2;
            for (i2 = 0; i2 < dtS2.Columns.Count; i2++)
            {
                    FA[i2] = dtS2.Rows[i][i2].ToString();
            }
            DataTable DTD2 = dboDest.SelectSQLDT(sSqlDest, new string[] { "V_SN" }, new object[] { SN });
            if (DTD2.Rows.Count > 0)
            {
                bool bSame = true;
                for (i2 = 1; i2 < FA.GetLength(0); i2++)
                {
                    if (FA[i2] != DTD2.Rows[0][i2].ToString())
                    {
                        bSame = false;
                        break;
                    }
                }
                if (bSame == false)
                {
                    sSql = @"Update Publib.T_R_WIP_TRACKING_T Set 
                            SECTION_FLAG =:V_1,MO_NUMBER = :V_2,MODEL_NAME = :V_3 ,TYPE = :V_4,VERSION_CODE = :V_5,LINE_NAME=:V_6,
                            SECTION_NAME = :V_7,GROUP_NAME = :V_8,STATION_NAME=:V_9,LOCATION=:V_10,STATION_SEQ=:V_11,ERROR_FLAG=:V_12,
                            IN_STATION_TIME=To_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),IN_LINE_TIME=To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),
                            OUT_LINE_TIME = To_Date(:V_15 ,'YYYY/MM/DD HH24:MI:SS'),SHIPPING_SN = :V_16,WORK_FLAG = :V_17,FINISH_FLAG=:V_18,
                            ENC_CNT=:V_19,SPECIAL_ROUTE = :V_20,PALLET_NO = :V_21,CONTAINER_NO=:V_22,QA_NO = :V_23,QA_RESULT=:V_24,
                            SCRAP_FLAG=:V_25,NEXT_STATION = :V_26,CUSTOMER_NO=:V_27,WORK_DATE=:V_28,WORK_SECTION=:V_29,PASS_QTY=:V_30,
                            FAIL_QTY = :V_31,REPASS_QTY=:V_32,REFAIL_QTY=:V_33,ECN_PASS_QTY=:V_34,ECN_FAIL_QTY=:V_35,KEY_PART_NO=:V_36,
                            CARTON_NO=:V_37,WARRANTY_DATE=To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),BOM_NO=:V_39,PO_NO=:V_40,
                            EMP_NO=:V_41,REWORK_NO=:V_42,PALLET_FULL_FLAG=:V_43,BIG_SN=:V_44,BASEQTY=:V_45,HASQTY=:V_46,BSAP=:V_47,
                            F1_FLAG = NULL Where SERIAL_NUMBER = :V_SN ";
                    string[] ParamName = new string[48];
                    ParamName[0] = "V_SN";
                    for (i2 = 1; i2 < 48; i2++)
                        ParamName[i2] = "V_" + i2.ToString();
                    dboDest.ExecSQL(sSql, ParamName, FA);
                    aRet[1, 1]++;
                }
                else
                {
                    aRet[1, 3]++;
                }
            }
            else
            {
                sSql = @"Insert Into PUBLIB.T_R_WIP_TRACKING_T 
                        (SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                        STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG, IN_STATION_TIME,IN_LINE_TIME,OUT_LINE_TIME,
                        SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                        NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                        ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,WARRANTY_DATE,BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP)
                        VALUES
                        (:V_SN,:V_1,:V_2,:V_3,:V_4,:V_5,:V_6,:V_7,:V_8,:V_9,:V_10,:V_11,:V_12,TO_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_15,'YYYY/MM/DD HH24:Mi:SS'),:V_16,:V_17,:V_18,:V_19,:V_20,:V_21,:V_22,:V_23,:V_24,
                        :V_25,:V_26,:V_27,:V_28,:V_29,:V_30,:V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),:V_39,:V_40,:V_41,:V_42,:V_43,:V_44,:V_45,:V_46,:V_47) ";
                string[] ParamName = new string[48];
                ParamName[0] = "V_SN";
                for (i2 = 1; i2 < 48; i2++)
                    ParamName[i2] = "V_" + i2.ToString();
                dboDest.ExecSQL(sSql, ParamName, FA);
                aRet[1, 2]++;
            }
        }
         
        #endregion

        #region SFISM6.R_MO_BASE_T
        sSql = @"select 
                MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,TOP,
                BOTTOM,LINE_TYPE,DESCRIPT
                from sfism6.R_MO_BASE_T ";
        sSqlDest = @"select 
                MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,TOP,
                BOTTOM,LINE_TYPE,DESCRIPT
                from publib.T_R_MO_BASE_T 
                Where MO_NUMBER = :V_SN ";
        DataTable dtS3 = dboSource.SelectSQLDT(sSql);
        aRet[2, 0] = dtS3.Rows.Count;
        aRet[2, 1] = 0;
        aRet[2, 2] = 0;
        aRet[2, 3] = 0;
        for (i = 0; i < dtS3.Rows.Count; i++)
        {
            string SN = dtS3.Rows[i][0].ToString();
            string[] FA = new string[dtS3.Columns.Count];
            int i2;
            for (i2 = 0; i2 < dtS3.Columns.Count; i2++)
            {
                FA[i2] = dtS3.Rows[i][i2].ToString();
            }
            DataTable DTD3 = dboDest.SelectSQLDT(sSqlDest, new string[] { "V_SN" }, new object[] { SN });
            if (DTD3.Rows.Count > 0)
            {
                bool bSame = true;
                for (i2 = 1; i2 < FA.GetLength(0); i2++)
                {
                    if (FA[i2] != DTD3.Rows[0][i2].ToString())
                    {
                        bSame = false;
                        break;
                    }
                }
                if (bSame == false)
                {
                    sSql = @"Update Publib.T_R_MO_BASE_T Set
                            MO_TYPE=:V_1,MODEL_NAME=:V_2,VERSION_CODE=:V_3,TARGET_QTY=:V_4,
                            MO_CREATE_DATE=To_Date(:V_5,'YYYY/MM/DD HH24:Mi:SS'),
                            MO_SCHEDULE_DATE=To_Date(:V_6,'YYYY/MM/DD HH24:Mi:SS'),
                            MO_DUE_DATE=To_Date(:V_7,'YYYY/MM/DD HH24:Mi:SS'),
                            MO_START_DATE=To_Date(:V_8,'YYYY/MM/DD HH24:Mi:SS'),
                            MO_TARGET_DATE=To_Date(:V_9,'YYYY/MM/DD HH24:Mi:SS'),
                            MO_CLOSE_DATE=To_Date(:V_10,'YYYY/MM/DD HH24:Mi:SS'),
                            ROUTE_CODE=:V_11,INPUT_QTY=:V_12,OUTPUT_QTY=:V_13,TURN_OUT_QTY=:V_14,
                            TOTAL_SCRAP_QTY=:V_15,START_SN=:V_16,END_SN=:V_17,SHIPPING_START_SN=:V_18,
                            SHIPPING_QTY=:V_19,WORK_FLAG=:V_20,CLOSE_FLAG=:V_21,DEFAULT_LINE=:V_22,
                            DEFAULT_GROUP=:V_23,CUST_CODE=:V_24,ORDER_NO=:V_25,BOM_NO=:V_26,
                            MASTER_FLAG=:V_27,MASTER_MO=:V_28,END_GROUP=:V_29,PO_NO=:V_30,
                            HW_BOM=:V_31,SW_BOM=:V_32,UPC_CO=:V_33,OPTION_DESC=:V_34,
                            KEY_PART_NO=:V_35,SN_RULE=:V_36,REWORK_QTY=:V_37,MO_OPTION=:V_38,
                            SUPPLIER_CODE=:V_39,MO_TAG=:V_40,BASEQTY=:V_41,BOARD_FLAG=:V_42,
                            TOP=:V_43,BOTTOM=:V_44,LINE_TYPE=:V_45,DESCRIPT=:V_46,
                            F1_FLAG = NULL Where MO_NUMBER = :V_SN ";
                    string[] ParamName = new string[47];
                    ParamName[0] = "V_SN";
                    for (i2 = 1; i2 < 47; i2++)
                        ParamName[i2] = "V_" + i2.ToString();
                    dboDest.ExecSQL(sSql, ParamName, FA);
                    aRet[2, 1]++;
                }
                else
                {
                    aRet[2, 3]++;
                }
            }
            else
            {
                sSql = @"Insert Into Publib.T_R_MO_BASE_T
                        (MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                        MO_CREATE_DATE,MO_SCHEDULE_DATE,
                        MO_DUE_DATE,MO_START_DATE,
                        MO_TARGET_DATE,MO_CLOSE_DATE,
                        ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                        CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,
                        HW_BOM,SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,MO_OPTION,SUPPLIER_CODE,MO_TAG,
                        BASEQTY,BOARD_FLAG,TOP,BOTTOM,LINE_TYPE,DESCRIPT)
                        Values
                        (:V_SN,:V_1,:V_2,:V_3,:V_4,To_Date(:V_5,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_6,'YYYY/MM/DD HH24:Mi:SS'),
                        To_Date(:V_7,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_8,'YYYY/MM/DD HH24:Mi:SS'),
                        To_Date(:V_9,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_10,'YYYY/MM/DD HH24:Mi:SS'),
                        :V_11,:V_12,:V_13,:V_14,:V_15,:V_16,:V_17,:V_18,:V_19,:V_20,
                        :V_21,:V_22,:V_23,:V_24,:V_25,:V_26,:V_27,:V_28,:V_29,:V_30,
                        :V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,:V_38,:V_39,:V_40,
                        :V_41,:V_42,:V_43,:V_44,:V_45,:V_46) ";
                string[] ParamName = new string[47];
                ParamName[0] = "V_SN";
                for (i2 = 1; i2 < 47; i2++)
                    ParamName[i2] = "V_" + i2.ToString();
                dboDest.ExecSQL(sSql, ParamName, FA);
                aRet[2, 2]++;
            }


        }





        #endregion

        #region SFISM4.R_WIP_TRACKING_T

        sSql = @"select 
                SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                from SFISM4.R_WIP_TRACKING_T
                Where IN_STATION_TIME >= To_Date(:V_S,'YYYY/MM/DD') And IN_STATION_TIME < To_Date(:V_E,'YYYY/MM/DD') ";
        sSqlDest = @"select 
                SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                from PUBLIB.T_R_WIP_TRACKING_T_PID
                Where SERIAL_NUMBER = :V_SN ";
        sSqlDest2 = @"select WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME 
                     From Publib.T_MES_PCBA_WIP_PAUSE
                     Where PRODUCT_ID =:V_SN ";
        sSqlDest3 = @"SELECT WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,SEQUENCE_ID,EMP_ID,ON_LINE,LINE_NAME 
                      From Publib.T_MES_PCBA_HISTORY Where PRODUCT_ID= :V_SN ";


        DataTable dtS4 =  dboSource.SelectSQLDT(sSql, new string[] { "V_S", "V_E" }, new object[] { sStart, sEnd });
        aRet[3, 0] = dtS4.Rows.Count;
        aRet[3, 1] = 0;
        aRet[3, 2] = 0;
        aRet[3, 3] = 0;
        aRet[4, 0] = dtS4.Rows.Count;
        aRet[4, 1] = 0;
        aRet[4, 2] = 0;
        aRet[4, 3] = 0;
        aRet[5, 0] = dtS4.Rows.Count;
        aRet[5, 0] = 0;
        aRet[5, 0] = 0;
        aRet[5, 0] = 0;

        for (i = 0; i < dtS4.Rows.Count; i++)
        {
            string SN = dtS4.Rows[i][0].ToString();
            string WO = dtS4.Rows[i][2].ToString();
            string sTime = dtS4.Rows[i]["IN_STATION_TIME"].ToString();
            string sLine = dtS4.Rows[i]["LINE_NAME"].ToString();
            string sEMP = dtS4.Rows[i]["EMP_NO"].ToString();
            string[] FA = new string[dtS4.Columns.Count];
            int i2;
            for (i2 = 0; i2 < dtS4.Columns.Count; i2++)
            {
                    FA[i2] = dtS4.Rows[i][i2].ToString();
            }
            #region T_R_WIP_TRACKING_T_PID
            DataTable DTD4 = dboDest.SelectSQLDT(sSqlDest, new string[] { "V_SN" }, new object[] { SN });
            if (DTD4.Rows.Count > 0)
            {
                aRet[3, 3]++;
                #region T_R_WIP_TRACKING_T_PID_Compair
                /*
                bool bSame = true;
                for (i2 = 1; i2 < FA.GetLength(0); i2++)
                {
                    if (FA[i2] != DTD2.Rows[0][i2].ToString())
                    {
                        bSame = false;
                        break;
                    }
                }
                if (bSame == false)
                {
                    sSql = @"Update Publib.T_R_WIP_TRACKING_T Set 
                            SECTION_FLAG =:V_1,MO_NUMBER = :V_2,MODEL_NAME = :V_3 ,TYPE = :V_4,VERSION_CODE = :V_5,LINE_NAME=:V_6,
                            SECTION_NAME = :V_7,GROUP_NAME = :V_8,STATION_NAME=:V_9,LOCATION=:V_10,STATION_SEQ=:V_11,ERROR_FLAG=:V_12,
                            IN_STATION_TIME=To_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),IN_LINE_TIME=To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),
                            OUT_LINE_TIME = To_Date(:V_15 ,'YYYY/MM/DD HH24:MI:SS'),SHIPPING_SN = :V_16,WORK_FLAG = :V_17,FINISH_FLAG=:V_18,
                            ENC_CNT=:V_19,SPECIAL_ROUTE = :V_20,PALLET_NO = :V_21,CONTAINER_NO=:V_22,QA_NO = :V_23,QA_RESULT=:V_24,
                            SCRAP_FLAG=:V_25,NEXT_STATION = :V_26,CUSTOMER_NO=:V_27,WORK_DATE=:V_28,WORK_SECTION=:V_29,PASS_QTY=:V_30,
                            FAIL_QTY = :V_31,REPASS_QTY=:V_32,REFAIL_QTY=:V_33,ECN_PASS_QTY=:V_34,ECN_FAIL_QTY=:V_35,KEY_PART_NO=:V_36,
                            CARTON_NO=:V_37,WARRANTY_DATE=To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),BOM_NO=:V_39,PO_NO=:V_40,
                            EMP_NO=:V_41,REWORK_NO=:V_42,PALLET_FULL_FLAG=:V_43,BIG_SN=:V_44,BASEQTY=:V_45,HASQTY=:V_46,BSAP=:V_47,
                            F1_FLAG = NULL Where SERIAL_NUMBER = :V_SN ";
                    string[] ParamName = new string[48];
                    ParamName[0] = "V_SN";
                    for (i2 = 1; i2 < 48; i2++)
                        ParamName[i2] = "V_" + i2.ToString();
                    dboDest.ExecSQL(sSql, ParamName, FA);
                    aRet[1, 1]++;
                }
                else
                {
                    aRet[1, 3]++;
                }
                 */
                #endregion
            }
            else
            {
                sSql = @"Insert Into PUBLIB.T_R_WIP_TRACKING_T_PID 
                        (SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                        STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG, IN_STATION_TIME,IN_LINE_TIME,OUT_LINE_TIME,
                        SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                        NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                        ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,WARRANTY_DATE,BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP)
                        VALUES
                        (:V_SN,:V_1,:V_2,:V_3,:V_4,:V_5,:V_6,:V_7,:V_8,:V_9,:V_10,:V_11,:V_12,TO_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_15,'YYYY/MM/DD HH24:Mi:SS'),:V_16,:V_17,:V_18,:V_19,:V_20,:V_21,:V_22,:V_23,:V_24,
                        :V_25,:V_26,:V_27,:V_28,:V_29,:V_30,:V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),:V_39,:V_40,:V_41,:V_42,:V_43,:V_44,:V_45,:V_46,:V_47) ";
                string[] ParamName = new string[48];
                ParamName[0] = "V_SN";
                for (i2 = 1; i2 < 48; i2++)
                    ParamName[i2] = "V_" + i2.ToString();
                dboDest.ExecSQL(sSql, ParamName, FA);
                aRet[3, 2]++;
            }
            #endregion

            #region T_MES_PCBA_WIP_PAUSE
            DataTable DTD5 = dboDest.SelectSQLDT(sSqlDest2, new string[] { "V_SN" }, new object[] { SN });
            if (DTD5.Rows.Count > 0)
            {
                aRet[4, 3]++;
                #region T_MES_PCBA_WIP_PAUSE_Compair
                /*
                bool bSame = true;
                for (i2 = 1; i2 < FA.GetLength(0); i2++)
                {
                    if (FA[i2] != DTD2.Rows[0][i2].ToString())
                    {
                        bSame = false;
                        break;
                    }
                }
                if (bSame == false)
                {
                    sSql = @"Update Publib.T_R_WIP_TRACKING_T Set 
                            SECTION_FLAG =:V_1,MO_NUMBER = :V_2,MODEL_NAME = :V_3 ,TYPE = :V_4,VERSION_CODE = :V_5,LINE_NAME=:V_6,
                            SECTION_NAME = :V_7,GROUP_NAME = :V_8,STATION_NAME=:V_9,LOCATION=:V_10,STATION_SEQ=:V_11,ERROR_FLAG=:V_12,
                            IN_STATION_TIME=To_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),IN_LINE_TIME=To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),
                            OUT_LINE_TIME = To_Date(:V_15 ,'YYYY/MM/DD HH24:MI:SS'),SHIPPING_SN = :V_16,WORK_FLAG = :V_17,FINISH_FLAG=:V_18,
                            ENC_CNT=:V_19,SPECIAL_ROUTE = :V_20,PALLET_NO = :V_21,CONTAINER_NO=:V_22,QA_NO = :V_23,QA_RESULT=:V_24,
                            SCRAP_FLAG=:V_25,NEXT_STATION = :V_26,CUSTOMER_NO=:V_27,WORK_DATE=:V_28,WORK_SECTION=:V_29,PASS_QTY=:V_30,
                            FAIL_QTY = :V_31,REPASS_QTY=:V_32,REFAIL_QTY=:V_33,ECN_PASS_QTY=:V_34,ECN_FAIL_QTY=:V_35,KEY_PART_NO=:V_36,
                            CARTON_NO=:V_37,WARRANTY_DATE=To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),BOM_NO=:V_39,PO_NO=:V_40,
                            EMP_NO=:V_41,REWORK_NO=:V_42,PALLET_FULL_FLAG=:V_43,BIG_SN=:V_44,BASEQTY=:V_45,HASQTY=:V_46,BSAP=:V_47,
                            F1_FLAG = NULL Where SERIAL_NUMBER = :V_SN ";
                    string[] ParamName = new string[48];
                    ParamName[0] = "V_SN";
                    for (i2 = 1; i2 < 48; i2++)
                        ParamName[i2] = "V_" + i2.ToString();
                    dboDest.ExecSQL(sSql, ParamName, FA);
                    aRet[1, 1]++;
                }
                else
                {
                    aRet[1, 3]++;
                }
                 */
                #endregion
            }
            else
            {
                sSql = @"Insert Into PUBLIB.T_MES_PCBA_WIP_PAUSE 
                        (WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME )
                        VALUES
                        (:V_MO,:V_SN,'SAAI','P',To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS'),'Y',:V_LINE) ";
                
                dboDest.ExecSQL(sSql, new string[] {"V_MO","V_SN","V_DATE","V_LINE"}, new object[] {WO,SN,sTime,sLine});
                aRet[4, 2]++;
            }
            #endregion

            #region T_MES_PCBA_HISTORY
            DataTable DTD6 = dboDest.SelectSQLDT(sSqlDest3, new string[] { "V_SN" }, new object[] { SN });
            if (DTD6.Rows.Count > 0)
            {
                aRet[5, 3]++;
                #region T_MES_PCBA_HISTORY_Compair
                /*
                bool bSame = true;
                for (i2 = 1; i2 < FA.GetLength(0); i2++)
                {
                    if (FA[i2] != DTD2.Rows[0][i2].ToString())
                    {
                        bSame = false;
                        break;
                    }
                }
                if (bSame == false)
                {
                    sSql = @"Update Publib.T_R_WIP_TRACKING_T Set 
                            SECTION_FLAG =:V_1,MO_NUMBER = :V_2,MODEL_NAME = :V_3 ,TYPE = :V_4,VERSION_CODE = :V_5,LINE_NAME=:V_6,
                            SECTION_NAME = :V_7,GROUP_NAME = :V_8,STATION_NAME=:V_9,LOCATION=:V_10,STATION_SEQ=:V_11,ERROR_FLAG=:V_12,
                            IN_STATION_TIME=To_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),IN_LINE_TIME=To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),
                            OUT_LINE_TIME = To_Date(:V_15 ,'YYYY/MM/DD HH24:MI:SS'),SHIPPING_SN = :V_16,WORK_FLAG = :V_17,FINISH_FLAG=:V_18,
                            ENC_CNT=:V_19,SPECIAL_ROUTE = :V_20,PALLET_NO = :V_21,CONTAINER_NO=:V_22,QA_NO = :V_23,QA_RESULT=:V_24,
                            SCRAP_FLAG=:V_25,NEXT_STATION = :V_26,CUSTOMER_NO=:V_27,WORK_DATE=:V_28,WORK_SECTION=:V_29,PASS_QTY=:V_30,
                            FAIL_QTY = :V_31,REPASS_QTY=:V_32,REFAIL_QTY=:V_33,ECN_PASS_QTY=:V_34,ECN_FAIL_QTY=:V_35,KEY_PART_NO=:V_36,
                            CARTON_NO=:V_37,WARRANTY_DATE=To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),BOM_NO=:V_39,PO_NO=:V_40,
                            EMP_NO=:V_41,REWORK_NO=:V_42,PALLET_FULL_FLAG=:V_43,BIG_SN=:V_44,BASEQTY=:V_45,HASQTY=:V_46,BSAP=:V_47,
                            F1_FLAG = NULL Where SERIAL_NUMBER = :V_SN ";
                    string[] ParamName = new string[48];
                    ParamName[0] = "V_SN";
                    for (i2 = 1; i2 < 48; i2++)
                        ParamName[i2] = "V_" + i2.ToString();
                    dboDest.ExecSQL(sSql, ParamName, FA);
                    aRet[1, 1]++;
                }
                else
                {
                    aRet[1, 3]++;
                }
                 */
                #endregion
            }
            else
            {
                sSql = @"Insert Into PUBLIB.T_MES_PCBA_HISTORY 
                        (WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,SEQUENCE_ID,EMP_ID,ON_LINE,LINE_NAME)
                        VALUES
                        (:V_MO,:V_SN,'SAAI','P',To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS'),1,:V_EMP,'Y',:V_LINE) ";
                dboDest.ExecSQL(sSql, new string[] { "V_MO", "V_SN", "V_DATE","V_EMP", "V_LINE" }, new object[] { WO, SN, sTime,sEMP,sLine });
                aRet[5, 2]++;
            }
            #endregion
        }

        #endregion

        dt = new DataTable();
        dt.Columns.Add("Tabble");
        dt.Columns.Add("SourceCount");
        dt.Columns.Add("UpdateCount");
        dt.Columns.Add("InsertCount");
        dt.Columns.Add("IgnoreCount");
        for (i = 0; i < aRet.GetLength(0); i++)
        {
            DataRow newdr = dt.NewRow();
            newdr[0] = sTableName[i];
            newdr[1] = aRet[i, 0].ToString();
            newdr[2] = aRet[i, 1].ToString();
            newdr[3] = aRet[i, 2].ToString();
            newdr[4] = aRet[i, 3].ToString();
        }
        return iRet;   
    }

    public static int SMTL6DataToL8Auto(string DBFlag, string DBRead, string[,] DBWrite, string MO, int Count)
    {
        int iRet;
        DataTable dt = GetPIDSNList(DBRead, Count);
        if (dt != null)
        {
            iRet = SMTL6DataToL8WithDT(DBFlag,DBRead, DBWrite, MO, dt);
            DeletePIDSNList(dt, DBRead);
        }
        else
        {
            iRet = -1;
        }
        return iRet;
    
    
    }

    private static int SMTL6DataToL8WithDT(string DBFlag,string DBRead, string[,] DBWrite,string MO, DataTable dtPS)
    {
        int iRet = 0;
        int i,iDB;
        #region InitPSN&SN
        string[,] SN = new string[0,2];
        string[,] PSN = new string[0,2];
        for (i = 0; i < dtPS.Rows.Count; i++ )
        {
            string sPSN = dtPS.Rows[i][0].ToString();
            string sSN = dtPS.Rows[i][1].ToString();

            if (FoundInArray(ref SN, sSN,0) == false)
            {
                ArrayAddOne(ref SN);
                SN[SN.GetLength(0) - 1,0] = sSN;
                SN[SN.GetLength(0) - 1, 1] = sPSN.Substring(0, 3);

            }

            if (FoundInArray(ref PSN, sPSN,0) == false)
            {
                ArrayAddOne(ref PSN);
                PSN[PSN.GetLength(0) - 1,0] = sPSN;
                PSN[PSN.GetLength(0) - 1, 1] = sPSN.Substring(0, 3);
            }
        }
        #endregion
        DataBaseOperation dboSource = new DataBaseOperation("oracle", DBRead);
        // More Desc DataBase
        DataBaseOperation[] dboDest = new DataBaseOperation[DBWrite.GetLength(0)];
        for (iDB = 0; iDB < DBWrite.GetLength(0); iDB++)
        {
            dboDest[iDB] = new DataBaseOperation("oracle", DBWrite[iDB,0]);
        }

        for (i = 0; i < SN.GetLength(0); i++)
        {
            string sSN = SN[i,0];
            string sSNType = SN[i, 1];
            #region R_WIP_LOG_T

            string sSql = "Select SERIAL_NUMBER,GROUP_NAME,To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,MO_NUMBER from SFISM6.R_WIP_LOG_T Where  SERIAL_NUMBER = :V_SN ";
            string sSqlDest2 = "Select SERIAL_NUMBER,GROUP_NAME,To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,MO_NUMBER from SFC.R_WIP_LOG_T Where  SERIAL_NUMBER = :V_SN ";
            string sSqlDest = "Insert Into SFC.R_WIP_LOG_T (SERIAL_NUMBER,GROUP_NAME,IN_STATION_TIME,MO_NUMBER) Values (:V_SN,:V_GN,To_Date(:V_IS,'YYYY/MM/DD HH24:Mi:SS'),:V_MO) ";

            DataTable dtS = dboSource.SelectSQLDT(sSql, new string[] { "V_SN" }, new object[] { sSN });
            for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
            {
                if (DBWrite[iDB, 1].ToUpper().Trim() == "")
                {
                    DataTable dtDest2 = dboDest[iDB].SelectSQLDT(sSqlDest2, new string[] { "V_SN" }, new object[] { sSN });
                    int j;
                    if (dtDest2.Rows.Count == 0)
                    {
                        for (j = 0; j < dtS.Rows.Count; j++)
                        {
                            string[] LogValue = new string[4];
                            LogValue[0] = dtS.Rows[j][0].ToString();
                            LogValue[1] = dtS.Rows[j][1].ToString();
                            LogValue[2] = dtS.Rows[j][2].ToString();
                            LogValue[3] = dtS.Rows[j][3].ToString();
                            dboDest[iDB].ExecSQL(sSqlDest, new string[] { "V_SN", "V_GN", "V_IS", "V_MO" }, LogValue);
                        }
                    }
                }
            }

            #endregion

            #region R_WIP_TRACKING_T
            string sSql2 = "";

            if (DBFlag.ToLower() == "othersite")
            {
              sSql2 = @"select 
                        SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                        STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                        To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                        To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                        To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                        SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                        NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                        ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                        To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                        BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                        from SFISM6.R_WIP_TRACKING_T
                        Where SERIAL_NUMBER = :V_SN ";
            }
            else
            if (DBFlag.ToLower() == "lf")
            {
                sSql2 = @"select 
                        SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                        STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                        To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                        To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                        To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                        SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                        NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                        ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                        To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                        BOM_NO,'',EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,'0'
                        from SFISM6.R_WIP_TRACKING_T
                        Where SERIAL_NUMBER = :V_SN ";
            }
            else
            {
                sSql2 = @"select 
                        SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                        STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                        To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                        To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                        To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                        SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                        NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                        ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                        To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                        BOM_NO,'',EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,'0'
                        from SFISM6.R_WIP_TRACKING_T
                        Where SERIAL_NUMBER = :V_SN ";

            }

            string sSqlDestInsert = @"Insert Into sfc.R_WIP_TRACKING_T 
                                (SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                                STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG, IN_STATION_TIME,IN_LINE_TIME,OUT_LINE_TIME,
                                SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                                NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                                ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,WARRANTY_DATE,BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP)
                                VALUES
                                (:V_SN,:V_1,:V_2,:V_3,:V_4,:V_5,:V_6,:V_7,:V_8,:V_9,:V_10,:V_11,:V_12,TO_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_15,'YYYY/MM/DD HH24:Mi:SS'),:V_16,:V_17,:V_18,:V_19,:V_20,:V_21,:V_22,:V_23,:V_24,
                                :V_25,:V_26,:V_27,:V_28,:V_29,:V_30,:V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),:V_39,:V_40,:V_41,:V_42,:V_43,:V_44,:V_45,:V_46,:V_47) ";

            string sSqlDestUpdate = @"Update sfc.R_WIP_TRACKING_T Set 
                                    SECTION_FLAG =:V_1,MO_NUMBER = :V_2,MODEL_NAME = :V_3 ,TYPE = :V_4,VERSION_CODE = :V_5,LINE_NAME=:V_6,
                                    SECTION_NAME = :V_7,GROUP_NAME = :V_8,STATION_NAME=:V_9,LOCATION=:V_10,STATION_SEQ=:V_11,ERROR_FLAG=:V_12,
                                    IN_STATION_TIME=To_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),IN_LINE_TIME=To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),
                                    OUT_LINE_TIME = To_Date(:V_15 ,'YYYY/MM/DD HH24:MI:SS'),SHIPPING_SN = :V_16,WORK_FLAG = :V_17,FINISH_FLAG=:V_18,
                                    ENC_CNT=:V_19,SPECIAL_ROUTE = :V_20,PALLET_NO = :V_21,CONTAINER_NO=:V_22,QA_NO = :V_23,QA_RESULT=:V_24,
                                    SCRAP_FLAG=:V_25,NEXT_STATION = :V_26,CUSTOMER_NO=:V_27,WORK_DATE=:V_28,WORK_SECTION=:V_29,PASS_QTY=:V_30,
                                    FAIL_QTY = :V_31,REPASS_QTY=:V_32,REFAIL_QTY=:V_33,ECN_PASS_QTY=:V_34,ECN_FAIL_QTY=:V_35,KEY_PART_NO=:V_36,
                                    CARTON_NO=:V_37,WARRANTY_DATE=To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),BOM_NO=:V_39,PO_NO=:V_40,
                                    EMP_NO=:V_41,REWORK_NO=:V_42,PALLET_FULL_FLAG=:V_43,BIG_SN=:V_44,BASEQTY=:V_45,HASQTY=:V_46,BSAP=:V_47
                                    Where SERIAL_NUMBER = :V_SN ";
   
          
            DataTable dtS2 = dboSource.SelectSQLDT(sSql2, new string[] { "V_SN" }, new object[] { sSN });
            if (dtS2.Rows.Count > 0)
            {
                string LineName = dtS2.Rows[0]["LINE_NAME"].ToString();
                string BIG_SN = dtS2.Rows[0]["SERIAL_NUMBER"].ToString();
                string MO_NUMBER = dtS2.Rows[0]["MO_NUMBER"].ToString();
                string MODEL_NAME = dtS2.Rows[0]["MODEL_NAME"].ToString();
                string sTime = dtS2.Rows[0]["IN_STATION_TIME"].ToString();
                
                string[] ParamValue = new string[48];
                string[] ParamName = new string[48];
                ParamName[0] = "V_SN";
                ParamValue[0] = dtS2.Rows[0][0].ToString();
                int j;
                for (j = 1; j < 48; j++)
                {
                    ParamName[j] = "V_" + j.ToString();
                    ParamValue[j] = dtS2.Rows[0][j].ToString();
                }
                // More DB 
                for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
                {
                    if (DBWrite[iDB, 1].Trim() == "")  // Normal module
                    {
                        int iExec = dboDest[iDB].ExecSQL(sSqlDestInsert, ParamName, ParamValue);
                        if (iExec == 0)
                        {
                            dboDest[iDB].ExecSQL(sSqlDestUpdate, ParamName, ParamValue);
                        }
                    }
                    else  // special module
                    {
                        if (DBWrite[iDB, 1].Trim().ToUpper() == "MODEL")
                        {
                            if (ModelIn(sSNType,DBWrite[iDB, 2])== true)
                            { 
                                string sSqlInsert = @"Insert into SFC.MES_PCBA_PANEL_DETAIL (PANEL_ID,SEQUENCE_ID,WO_NO,ITEM_NO,CREATE_DATE) 
                                                        Values (:V_ID,1,:V_WO,:V_ITEM,To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS') ) ";
                                int iTmp = dboDest[iDB].ExecSQL(sSqlInsert, new string[] { "V_ID", "V_WO", "V_ITEM", "V_DATE" }, new object[] { BIG_SN, MO_NUMBER, MODEL_NAME, sTime });
                            }
                        }
                    }
                }
            }
            

            #endregion
        }

        #region R_MO_BASE_T
        if (MO == "Y")
        {
            string sSqlMO = "";

            if (DBFlag.ToLower() == "othersite") // TJ Site
            {
                sSqlMO = @"select 
                MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,TOP,
                BOTTOM,LINE_TYPE,DESCRIPT
                from sfism6.R_MO_BASE_T ";
            }
            else
            if (DBFlag.ToLower() == "lf")
            {
                sSqlMO = @"select 
                    MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                    To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                    To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                    To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                    To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                    To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                    To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                    ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                    START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                    CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                    BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                    SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                    MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,'',
                    '','',''
                    from sfism6.R_MO_BASE_T ";
            }
            else
            {
               sSqlMO = @"select 
                    MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                    To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                    To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                    To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                    To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                    To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                    To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                    ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                    START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                    CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                    BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                    SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                    MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,'',
                    '','',''
                    from sfism6.R_MO_BASE_T ";
            }
         

            string sSqlMO2 = @"select 
                MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,TOP,
                BOTTOM,LINE_TYPE,DESCRIPT
                from sfc.R_MO_BASE_T ";

            string sSqlDestMO = @"Insert Into sfc.R_MO_BASE_T
                        (MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                        MO_CREATE_DATE,MO_SCHEDULE_DATE,
                        MO_DUE_DATE,MO_START_DATE,
                        MO_TARGET_DATE,MO_CLOSE_DATE,
                        ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                        CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,
                        HW_BOM,SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,MO_OPTION,SUPPLIER_CODE,MO_TAG,
                        BASEQTY,BOARD_FLAG,TOP,BOTTOM,LINE_TYPE,DESCRIPT)
                        Values
                        (:V_SN,:V_1,:V_2,:V_3,:V_4,To_Date(:V_5,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_6,'YYYY/MM/DD HH24:Mi:SS'),
                        To_Date(:V_7,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_8,'YYYY/MM/DD HH24:Mi:SS'),
                        To_Date(:V_9,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_10,'YYYY/MM/DD HH24:Mi:SS'),
                        :V_11,:V_12,:V_13,:V_14,:V_15,:V_16,:V_17,:V_18,:V_19,:V_20,
                        :V_21,:V_22,:V_23,:V_24,:V_25,:V_26,:V_27,:V_28,:V_29,:V_30,
                        :V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,:V_38,:V_39,:V_40,
                        :V_41,:V_42,:V_43,:V_44,:V_45,:V_46) ";
        
            DataTable dtMO1 = dboSource.SelectSQLDT(sSqlMO);
            // More DB 
            for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
            {
                if (DBWrite[iDB, 1].Trim() == "")
                {
                    DataTable dtMO2 = dboDest[iDB].SelectSQLDT(sSqlMO2);
                    for (i = 0; i < dtMO1.Rows.Count; i++)
                    {
                        string sMO = dtMO1.Rows[i][0].ToString();
                        DataRow[] dr = dtMO2.Select("MO_NUMBER = '" + sMO + "'");
                        if (dr.GetLength(0) == 0)
                        {
                            string[] ParamValue = new string[dtMO1.Columns.Count];
                            int i2;
                            for (i2 = 0; i2 < dtMO1.Columns.Count; i2++)
                            {
                                ParamValue[i2] = dtMO1.Rows[i][i2].ToString();
                            }
                            string[] ParamName = new string[47];
                            ParamName[0] = "V_SN";
                            for (i2 = 1; i2 < 47; i2++)
                                ParamName[i2] = "V_" + i2.ToString();
                            dboDest[iDB].ExecSQL(sSqlDestMO, ParamName, ParamValue);
                        }
                    }
                }
            }
        }
        #endregion 

        for (i = 0; i < PSN.GetLength(0); i++)
        {
            string sPSN = PSN[i,0];
            string sPSNType = PSN[i, 1];
            string sSql = @"select 
                          SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                          STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                          To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                          To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                            To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                            SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                            NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                            ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                            To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                            BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                            from SFISM4.R_WIP_TRACKING_T
                            Where SERIAL_NUMBER = :V_SN ";
            string sSqlDest = @"Insert Into sfc.R_WIP_TRACKING_T_PID 
                            (SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                            STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG, IN_STATION_TIME,IN_LINE_TIME,OUT_LINE_TIME,
                            SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                            NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                            ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,WARRANTY_DATE,BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP)
                            VALUES
                            (:V_SN,:V_1,:V_2,:V_3,:V_4,:V_5,:V_6,:V_7,:V_8,:V_9,:V_10,:V_11,:V_12,TO_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_15,'YYYY/MM/DD HH24:Mi:SS'),:V_16,:V_17,:V_18,:V_19,:V_20,:V_21,:V_22,:V_23,:V_24,
                            :V_25,:V_26,:V_27,:V_28,:V_29,:V_30,:V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),:V_39,:V_40,:V_41,:V_42,:V_43,:V_44,:V_45,:V_46,:V_47) ";
            string sSqlDest2 = @"Insert Into SFC.MES_PCBA_WIP_PAUSE 
                        (WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME )
                        VALUES
                        (:V_MO,:V_SN,'SAAI','P',To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS'),'Y',:V_LINE) ";
            string sSqlDest3 =  @"Insert Into SFC.MES_PCBA_HISTORY 
                        (WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,SEQUENCE_ID,EMP_ID,ON_LINE,LINE_NAME)
                        VALUES
                        (:V_MO,:V_SN,'SAAI','P',To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS'),1,:V_EMP,'Y',:V_LINE) ";
            string sSqlDest4 = "Select Count(*) From SFC.MES_PCBA_HISTORY Where PRODUCT_ID = :V_SN ";
            DataTable dtS1 = dboSource.SelectSQLDT(sSql, new string[] { "V_SN" }, new object[] { sPSN });
            if (dtS1.Rows.Count > 0)
            {
                string[] ParamName = new string[48];
                string[] ParamValue = new string[48];

                string WO = dtS1.Rows[0][2].ToString();
                string sTime = dtS1.Rows[0]["IN_STATION_TIME"].ToString();
                string sLine = dtS1.Rows[0]["LINE_NAME"].ToString();
                string sEMP = dtS1.Rows[0]["EMP_NO"].ToString();
                string PID = dtS1.Rows[0]["SERIAL_NUMBER"].ToString();
                string MODEL_NAME = dtS1.Rows[0]["MODEL_NAME"].ToString();
                string BIG_SN = dtS1.Rows[0]["BIG_SN"].ToString();
                
                ParamName[0] = "V_SN";
                ParamValue[0] = dtS1.Rows[0][0].ToString(); 
                int j;
                for (j = 1; j < 48; j++)
                {
                    ParamName[j] = "V_" + j.ToString();
                    ParamValue[j] = dtS1.Rows[0][j].ToString(); 
                }
                // More DB
                for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
                {
                    if (DBWrite[iDB, 1].Trim() == "")  // normal module
                    {
                        dboDest[iDB].ExecSQL(sSqlDest, ParamName, ParamValue);
                        dboDest[iDB].ExecSQL(sSqlDest2, new string[] { "V_MO", "V_SN", "V_DATE", "V_LINE" }, new object[] { WO, sPSN, sTime, sLine });
                        DataTable dt4 = dboDest[iDB].SelectSQLDT(sSqlDest4, new string[] { "V_SN" }, new object[] { sPSN });
                        if (dt4.Rows[0][0].ToString() == "0")
                            dboDest[iDB].ExecSQL(sSqlDest3, new string[] { "V_MO", "V_SN", "V_DATE", "V_EMP", "V_LINE" }, new object[] { WO, sPSN, sTime, sEMP, sLine });
                    }
                    else  // special module
                    {
                        if (DBWrite[iDB, 1].Trim().ToUpper() == "MODEL")
                        {
                            if (ModelIn(sPSNType,DBWrite[iDB, 2])== true)
                            {
                                string sSqlInsert = @"Insert into SFC.MES_PCBA_PANEL_DETAIL (PANEL_ID,SEQUENCE_ID,WO_NO,ITEM_NO,CREATE_DATE) 
                                                        Values (:V_ID,null,:V_WO,:V_ITEM,To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS') ) ";
                                int iTmp = dboDest[iDB].ExecSQL(sSqlInsert, new string[] { "V_ID", "V_WO", "V_ITEM", "V_DATE" }, new object[] { PID, WO, MODEL_NAME,sTime});

                                sSqlInsert = @"Insert into  SFC.MES_PCBA_PANEL_LINK (PANEL_ID,PRODUCT_ID,SEQUENCE_ID,CREATE_DATE) 
                                                 Values (:V_ID,:V_PID,0,To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS') ) ";
                                iTmp = dboDest[iDB].ExecSQL(sSqlInsert, new string[] { "V_ID", "V_PID", "V_DATE" }, new object[] { BIG_SN, PID, sTime });
                            }
                        }
                    }

                }
            }
        }
        iRet = dtPS.Rows.Count;
        return iRet;
    }

    // tmp 陳強 Org Prg 20130504
    private static int SMTL6DataToL8WithDT20130504(string DBFlag, string DBRead, string[,] DBWrite, string MO, DataTable dtPS)
    {
        int iRet = 0;
        int i, iDB;
        #region InitPSN&SN
        string[,] SN = new string[0, 2];
        string[,] PSN = new string[0, 2];
        for (i = 0; i < dtPS.Rows.Count; i++)
        {
            string sPSN = dtPS.Rows[i][0].ToString();
            string sSN = dtPS.Rows[i][1].ToString();

            if (FoundInArray(ref SN, sSN, 0) == false)
            {
                ArrayAddOne(ref SN);
                SN[SN.GetLength(0) - 1, 0] = sSN;
                SN[SN.GetLength(0) - 1, 1] = sPSN.Substring(0, 3);

            }

            if (FoundInArray(ref PSN, sPSN, 0) == false)
            {
                ArrayAddOne(ref PSN);
                PSN[PSN.GetLength(0) - 1, 0] = sPSN;
                PSN[PSN.GetLength(0) - 1, 1] = sPSN.Substring(0, 3);
            }
        }
        #endregion
        DataBaseOperation dboSource = new DataBaseOperation("oracle", DBRead);
        // More Desc DataBase
        DataBaseOperation[] dboDest = new DataBaseOperation[DBWrite.GetLength(0)];
        for (iDB = 0; iDB < DBWrite.GetLength(0); iDB++)
        {
            dboDest[iDB] = new DataBaseOperation("oracle", DBWrite[iDB, 0]);
        }

        for (i = 0; i < SN.GetLength(0); i++)
        {
            string sSN = SN[i, 0];
            string sSNType = SN[i, 1];
            #region R_WIP_LOG_T

            string sSql = "Select SERIAL_NUMBER,GROUP_NAME,To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,MO_NUMBER from SFISM6.R_WIP_LOG_T Where  SERIAL_NUMBER = :V_SN ";
            string sSqlDest2 = "Select SERIAL_NUMBER,GROUP_NAME,To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,MO_NUMBER from SFC.R_WIP_LOG_T Where  SERIAL_NUMBER = :V_SN ";
            string sSqlDest = "Insert Into SFC.R_WIP_LOG_T (SERIAL_NUMBER,GROUP_NAME,IN_STATION_TIME,MO_NUMBER) Values (:V_SN,:V_GN,To_Date(:V_IS,'YYYY/MM/DD HH24:Mi:SS'),:V_MO) ";

            DataTable dtS = dboSource.SelectSQLDT(sSql, new string[] { "V_SN" }, new object[] { sSN });
            for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
            {
                if (DBWrite[iDB, 1].ToUpper().Trim() == "")
                {
                    DataTable dtDest2 = dboDest[iDB].SelectSQLDT(sSqlDest2, new string[] { "V_SN" }, new object[] { sSN });
                    int j;
                    if (dtDest2.Rows.Count == 0)
                    {
                        for (j = 0; j < dtS.Rows.Count; j++)
                        {
                            string[] LogValue = new string[4];
                            LogValue[0] = dtS.Rows[j][0].ToString();
                            LogValue[1] = dtS.Rows[j][1].ToString();
                            LogValue[2] = dtS.Rows[j][2].ToString();
                            LogValue[3] = dtS.Rows[j][3].ToString();
                            dboDest[iDB].ExecSQL(sSqlDest, new string[] { "V_SN", "V_GN", "V_IS", "V_MO" }, LogValue);
                        }
                    }
                }
            }

            #endregion

            #region R_WIP_TRACKING_T
            string sSql2 = @"select 
                        SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                        STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                        To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                        To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                        To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                        SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                        NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                        ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                        To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                        BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                        from SFISM6.R_WIP_TRACKING_T
                        Where SERIAL_NUMBER = :V_SN ";
            string sSqlDestInsert = @"Insert Into sfc.R_WIP_TRACKING_T 
                                (SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                                STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG, IN_STATION_TIME,IN_LINE_TIME,OUT_LINE_TIME,
                                SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                                NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                                ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,WARRANTY_DATE,BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP)
                                VALUES
                                (:V_SN,:V_1,:V_2,:V_3,:V_4,:V_5,:V_6,:V_7,:V_8,:V_9,:V_10,:V_11,:V_12,TO_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_15,'YYYY/MM/DD HH24:Mi:SS'),:V_16,:V_17,:V_18,:V_19,:V_20,:V_21,:V_22,:V_23,:V_24,
                                :V_25,:V_26,:V_27,:V_28,:V_29,:V_30,:V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),:V_39,:V_40,:V_41,:V_42,:V_43,:V_44,:V_45,:V_46,:V_47) ";

            string sSqlDestUpdate = @"Update sfc.R_WIP_TRACKING_T Set 
                                    SECTION_FLAG =:V_1,MO_NUMBER = :V_2,MODEL_NAME = :V_3 ,TYPE = :V_4,VERSION_CODE = :V_5,LINE_NAME=:V_6,
                                    SECTION_NAME = :V_7,GROUP_NAME = :V_8,STATION_NAME=:V_9,LOCATION=:V_10,STATION_SEQ=:V_11,ERROR_FLAG=:V_12,
                                    IN_STATION_TIME=To_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),IN_LINE_TIME=To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),
                                    OUT_LINE_TIME = To_Date(:V_15 ,'YYYY/MM/DD HH24:MI:SS'),SHIPPING_SN = :V_16,WORK_FLAG = :V_17,FINISH_FLAG=:V_18,
                                    ENC_CNT=:V_19,SPECIAL_ROUTE = :V_20,PALLET_NO = :V_21,CONTAINER_NO=:V_22,QA_NO = :V_23,QA_RESULT=:V_24,
                                    SCRAP_FLAG=:V_25,NEXT_STATION = :V_26,CUSTOMER_NO=:V_27,WORK_DATE=:V_28,WORK_SECTION=:V_29,PASS_QTY=:V_30,
                                    FAIL_QTY = :V_31,REPASS_QTY=:V_32,REFAIL_QTY=:V_33,ECN_PASS_QTY=:V_34,ECN_FAIL_QTY=:V_35,KEY_PART_NO=:V_36,
                                    CARTON_NO=:V_37,WARRANTY_DATE=To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),BOM_NO=:V_39,PO_NO=:V_40,
                                    EMP_NO=:V_41,REWORK_NO=:V_42,PALLET_FULL_FLAG=:V_43,BIG_SN=:V_44,BASEQTY=:V_45,HASQTY=:V_46,BSAP=:V_47
                                    Where SERIAL_NUMBER = :V_SN ";
            if (DBFlag.ToLower() == "lf")
            {
                sSql2 = @"select 
                        SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                        STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                        To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                        To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                        To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                        SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                        NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                        ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                        To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                        BOM_NO,'',EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,'0'
                        from SFISM6.R_WIP_TRACKING_T
                        Where SERIAL_NUMBER = :V_SN ";
            }

            DataTable dtS2 = dboSource.SelectSQLDT(sSql2, new string[] { "V_SN" }, new object[] { sSN });
            if (dtS2.Rows.Count > 0)
            {
                string LineName = dtS2.Rows[0]["LINE_NAME"].ToString();
                string BIG_SN = dtS2.Rows[0]["SERIAL_NUMBER"].ToString();
                string MO_NUMBER = dtS2.Rows[0]["MO_NUMBER"].ToString();
                string MODEL_NAME = dtS2.Rows[0]["MODEL_NAME"].ToString();
                string sTime = dtS2.Rows[0]["IN_STATION_TIME"].ToString();

                string[] ParamValue = new string[48];
                string[] ParamName = new string[48];
                ParamName[0] = "V_SN";
                ParamValue[0] = dtS2.Rows[0][0].ToString();
                int j;
                for (j = 1; j < 48; j++)
                {
                    ParamName[j] = "V_" + j.ToString();
                    ParamValue[j] = dtS2.Rows[0][j].ToString();
                }
                // More DB 
                for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
                {
                    if (DBWrite[iDB, 1].Trim() == "")  // Normal module
                    {
                        int iExec = dboDest[iDB].ExecSQL(sSqlDestInsert, ParamName, ParamValue);
                        if (iExec == 0)
                        {
                            dboDest[iDB].ExecSQL(sSqlDestUpdate, ParamName, ParamValue);
                        }
                    }
                    else  // special module
                    {
                        if (DBWrite[iDB, 1].Trim().ToUpper() == "MODEL")
                        {
                            if (ModelIn(sSNType, DBWrite[iDB, 2]) == true)
                            {
                                string sSqlInsert = @"Insert into SFC.MES_PCBA_PANEL_DETAIL (PANEL_ID,SEQUENCE_ID,WO_NO,ITEM_NO,CREATE_DATE) 
                                                        Values (:V_ID,1,:V_WO,:V_ITEM,To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS') ) ";
                                int iTmp = dboDest[iDB].ExecSQL(sSqlInsert, new string[] { "V_ID", "V_WO", "V_ITEM", "V_DATE" }, new object[] { BIG_SN, MO_NUMBER, MODEL_NAME, sTime });
                            }
                        }
                    }
                }
            }


            #endregion
        }

        #region R_MO_BASE_T
        if (MO == "Y")
        {
            string sSqlMO = @"select 
                MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,TOP,
                BOTTOM,LINE_TYPE,DESCRIPT
                from sfism6.R_MO_BASE_T ";

            string sSqlMO2 = @"select 
                MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,TOP,
                BOTTOM,LINE_TYPE,DESCRIPT
                from sfc.R_MO_BASE_T ";

            string sSqlDestMO = @"Insert Into sfc.R_MO_BASE_T
                        (MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                        MO_CREATE_DATE,MO_SCHEDULE_DATE,
                        MO_DUE_DATE,MO_START_DATE,
                        MO_TARGET_DATE,MO_CLOSE_DATE,
                        ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                        CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,
                        HW_BOM,SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,MO_OPTION,SUPPLIER_CODE,MO_TAG,
                        BASEQTY,BOARD_FLAG,TOP,BOTTOM,LINE_TYPE,DESCRIPT)
                        Values
                        (:V_SN,:V_1,:V_2,:V_3,:V_4,To_Date(:V_5,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_6,'YYYY/MM/DD HH24:Mi:SS'),
                        To_Date(:V_7,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_8,'YYYY/MM/DD HH24:Mi:SS'),
                        To_Date(:V_9,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_10,'YYYY/MM/DD HH24:Mi:SS'),
                        :V_11,:V_12,:V_13,:V_14,:V_15,:V_16,:V_17,:V_18,:V_19,:V_20,
                        :V_21,:V_22,:V_23,:V_24,:V_25,:V_26,:V_27,:V_28,:V_29,:V_30,
                        :V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,:V_38,:V_39,:V_40,
                        :V_41,:V_42,:V_43,:V_44,:V_45,:V_46) ";
            if (DBFlag.ToLower() == "lf")
            {
                sSqlMO = @"select 
                    MO_NUMBER,MO_TYPE,MODEL_NAME,VERSION_CODE,TARGET_QTY,
                    To_Char(MO_CREATE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CREATE_DATE,
                    To_Char(MO_SCHEDULE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_SCHEDULE_DATE, 
                    To_Char(MO_DUE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_DUE_DATE,
                    To_Char(MO_START_DATE,'YYYY/MM/DD HH24:Mi:SS')     MO_START_DATE,
                    To_Char(MO_TARGET_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_TARGET_DATE,
                    To_Char(MO_CLOSE_DATE,'YYYY/MM/DD HH24:Mi:SS') MO_CLOSE_DATE,
                    ROUTE_CODE,INPUT_QTY,OUTPUT_QTY,TURN_OUT_QTY,TOTAL_SCRAP_QTY,
                    START_SN,END_SN,SHIPPING_START_SN,SHIPPING_QTY,WORK_FLAG,
                    CLOSE_FLAG,DEFAULT_LINE,DEFAULT_GROUP,CUST_CODE,ORDER_NO,
                    BOM_NO,MASTER_FLAG,MASTER_MO,END_GROUP,PO_NO,HW_BOM,
                    SW_BOM,UPC_CO,OPTION_DESC,KEY_PART_NO,SN_RULE,REWORK_QTY,
                    MO_OPTION,SUPPLIER_CODE,MO_TAG,BASEQTY,BOARD_FLAG,'',
                    '','',''
                    from sfism6.R_MO_BASE_T ";
            }

            DataTable dtMO1 = dboSource.SelectSQLDT(sSqlMO);
            // More DB 
            for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
            {
                if (DBWrite[iDB, 1].Trim() == "")
                {
                    DataTable dtMO2 = dboDest[iDB].SelectSQLDT(sSqlMO2);
                    for (i = 0; i < dtMO1.Rows.Count; i++)
                    {
                        string sMO = dtMO1.Rows[i][0].ToString();
                        DataRow[] dr = dtMO2.Select("MO_NUMBER = '" + sMO + "'");
                        if (dr.GetLength(0) == 0)
                        {
                            string[] ParamValue = new string[dtMO1.Columns.Count];
                            int i2;
                            for (i2 = 0; i2 < dtMO1.Columns.Count; i2++)
                            {
                                ParamValue[i2] = dtMO1.Rows[i][i2].ToString();
                            }
                            string[] ParamName = new string[47];
                            ParamName[0] = "V_SN";
                            for (i2 = 1; i2 < 47; i2++)
                                ParamName[i2] = "V_" + i2.ToString();
                            dboDest[iDB].ExecSQL(sSqlDestMO, ParamName, ParamValue);
                        }
                    }
                }
            }
        }
        #endregion

        for (i = 0; i < PSN.GetLength(0); i++)
        {
            string sPSN = PSN[i, 0];
            string sPSNType = PSN[i, 1];
            string sSql = @"select 
                          SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                          STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG,
                          To_Char(IN_STATION_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_STATION_TIME,
                          To_Char(IN_LINE_TIME,'YYYY/MM/DD HH24:Mi:SS') IN_LINE_TIME,
                            To_Char(OUT_LINE_TIME,'YYYY/MM/DD HH24:MI:SS') OUT_LINE_TIME,
                            SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                            NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                            ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,
                            To_Char(WARRANTY_DATE,'YYYY/MM/DD HH24:Mi:SS') WARRANTY_DATE,
                            BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP                
                            from SFISM4.R_WIP_TRACKING_T
                            Where SERIAL_NUMBER = :V_SN ";
            string sSqlDest = @"Insert Into sfc.R_WIP_TRACKING_T_PID 
                            (SERIAL_NUMBER,SECTION_FLAG,MO_NUMBER,MODEL_NAME,TYPE,VERSION_CODE,LINE_NAME,SECTION_NAME,GROUP_NAME,
                            STATION_NAME,LOCATION,STATION_SEQ,ERROR_FLAG, IN_STATION_TIME,IN_LINE_TIME,OUT_LINE_TIME,
                            SHIPPING_SN,WORK_FLAG,FINISH_FLAG,ENC_CNT,SPECIAL_ROUTE,PALLET_NO,CONTAINER_NO,QA_NO,QA_RESULT,SCRAP_FLAG,
                            NEXT_STATION,CUSTOMER_NO,WORK_DATE,WORK_SECTION,PASS_QTY,FAIL_QTY,REPASS_QTY,REFAIL_QTY,ECN_PASS_QTY,
                            ECN_FAIL_QTY,KEY_PART_NO,CARTON_NO,WARRANTY_DATE,BOM_NO,PO_NO,EMP_NO,REWORK_NO,PALLET_FULL_FLAG,BIG_SN,BASEQTY,HASQTY,BSAP)
                            VALUES
                            (:V_SN,:V_1,:V_2,:V_3,:V_4,:V_5,:V_6,:V_7,:V_8,:V_9,:V_10,:V_11,:V_12,TO_Date(:V_13,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_14,'YYYY/MM/DD HH24:Mi:SS'),To_Date(:V_15,'YYYY/MM/DD HH24:Mi:SS'),:V_16,:V_17,:V_18,:V_19,:V_20,:V_21,:V_22,:V_23,:V_24,
                            :V_25,:V_26,:V_27,:V_28,:V_29,:V_30,:V_31,:V_32,:V_33,:V_34,:V_35,:V_36,:V_37,To_Date(:V_38,'YYYY/MM/DD HH24:Mi:SS'),:V_39,:V_40,:V_41,:V_42,:V_43,:V_44,:V_45,:V_46,:V_47) ";
            string sSqlDest2 = @"Insert Into SFC.MES_PCBA_WIP_PAUSE 
                        (WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,ON_LINE,LINE_NAME )
                        VALUES
                        (:V_MO,:V_SN,'SAAI','P',To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS'),'Y',:V_LINE) ";
            string sSqlDest3 = @"Insert Into SFC.MES_PCBA_HISTORY 
                        (WO_NO,PRODUCT_ID,STATION_ID,STATE_ID,CREATION_DATE,SEQUENCE_ID,EMP_ID,ON_LINE,LINE_NAME)
                        VALUES
                        (:V_MO,:V_SN,'SAAI','P',To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS'),1,:V_EMP,'Y',:V_LINE) ";
            string sSqlDest4 = "Select Count(*) From SFC.MES_PCBA_HISTORY Where PRODUCT_ID = :V_SN ";
            DataTable dtS1 = dboSource.SelectSQLDT(sSql, new string[] { "V_SN" }, new object[] { sPSN });
            if (dtS1.Rows.Count > 0)
            {
                string[] ParamName = new string[48];
                string[] ParamValue = new string[48];

                string WO = dtS1.Rows[0][2].ToString();
                string sTime = dtS1.Rows[0]["IN_STATION_TIME"].ToString();
                string sLine = dtS1.Rows[0]["LINE_NAME"].ToString();
                string sEMP = dtS1.Rows[0]["EMP_NO"].ToString();
                string PID = dtS1.Rows[0]["SERIAL_NUMBER"].ToString();
                string MODEL_NAME = dtS1.Rows[0]["MODEL_NAME"].ToString();
                string BIG_SN = dtS1.Rows[0]["BIG_SN"].ToString();

                ParamName[0] = "V_SN";
                ParamValue[0] = dtS1.Rows[0][0].ToString();
                int j;
                for (j = 1; j < 48; j++)
                {
                    ParamName[j] = "V_" + j.ToString();
                    ParamValue[j] = dtS1.Rows[0][j].ToString();
                }
                // More DB
                for (iDB = 0; iDB < dboDest.GetLength(0); iDB++)
                {
                    if (DBWrite[iDB, 1].Trim() == "")  // normal module
                    {
                        dboDest[iDB].ExecSQL(sSqlDest, ParamName, ParamValue);
                        dboDest[iDB].ExecSQL(sSqlDest2, new string[] { "V_MO", "V_SN", "V_DATE", "V_LINE" }, new object[] { WO, sPSN, sTime, sLine });
                        DataTable dt4 = dboDest[iDB].SelectSQLDT(sSqlDest4, new string[] { "V_SN" }, new object[] { sPSN });
                        if (dt4.Rows[0][0].ToString() == "0")
                            dboDest[iDB].ExecSQL(sSqlDest3, new string[] { "V_MO", "V_SN", "V_DATE", "V_EMP", "V_LINE" }, new object[] { WO, sPSN, sTime, sEMP, sLine });
                    }
                    else  // special module
                    {
                        if (DBWrite[iDB, 1].Trim().ToUpper() == "MODEL")
                        {
                            if (ModelIn(sPSNType, DBWrite[iDB, 2]) == true)
                            {
                                string sSqlInsert = @"Insert into SFC.MES_PCBA_PANEL_DETAIL (PANEL_ID,SEQUENCE_ID,WO_NO,ITEM_NO,CREATE_DATE) 
                                                        Values (:V_ID,null,:V_WO,:V_ITEM,To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS') ) ";
                                int iTmp = dboDest[iDB].ExecSQL(sSqlInsert, new string[] { "V_ID", "V_WO", "V_ITEM", "V_DATE" }, new object[] { PID, WO, MODEL_NAME, sTime });

                                sSqlInsert = @"Insert into  SFC.MES_PCBA_PANEL_LINK (PANEL_ID,PRODUCT_ID,SEQUENCE_ID,CREATE_DATE) 
                                                 Values (:V_ID,:V_PID,0,To_Date(:V_DATE,'YYYY/MM/DD HH24:Mi:SS') ) ";
                                iTmp = dboDest[iDB].ExecSQL(sSqlInsert, new string[] { "V_ID", "V_PID", "V_DATE" }, new object[] { BIG_SN, PID, sTime });
                            }
                        }
                    }

                }
            }
        }
        iRet = dtPS.Rows.Count;
        return iRet;
    }  // end 30130504

    private static bool ModelIn(string sModel, string sModelSet)
    {
        bool bRet = false;
        string[] Tmp = sModelSet.Split(',');
        for (int i = 0; i < Tmp.GetLength(0); i++)
        {
            if (sModel.Trim().ToUpper() == Tmp[i].ToUpper().Trim())
            {
                bRet = true;
                break;
            }
        }
        return bRet;
    }

    private static DataTable GetPIDSNList(string DBRead, int Count)
    {
        string sql = "Select * from PUBLIB.L6TOL8T1 ";
        if (Count > 0) sql += " Where RowNum <= " + Count.ToString();
        DataTable dt = DataBaseOperation.SelectSQLDT("oracle", DBRead, sql);
        if (dt == null) FERROR = DataBaseOperation.GetError();
        return dt;
    }
    private static void DeletePIDSNList(DataTable dt,string DBRead)
    {
        int i;
        string sSql = "Delete From PUBLIB.L6TOL8T1 Where DOCUMENTID = :V_DOCID ";
        DataBaseOperation dbo = new DataBaseOperation("oracle", DBRead);
        for (i = 0; i < dt.Rows.Count; i++)
        { 
            string DocID = dt.Rows[i][2].ToString();
            dbo.ExecSQL(sSql,new string[] {"V_DOCID"},new object[] {DocID});
        }
    }

}