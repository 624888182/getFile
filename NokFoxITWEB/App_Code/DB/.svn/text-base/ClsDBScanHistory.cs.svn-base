/*************************************************************************
 * 
 *  Unit description: Scan history query
 *  Developer: Shu Jian Bo             Date: 2006/12/30
 *  Modifier : Shu Jian Bo             Date: 2007/12/30
 * 
 * ***********************************************************************/
using System;
using DBAccess.EAI;
using System.Data;

namespace DB.EAI
{
    /// <summary>
    /// ClsDBScanHistory 的摘要描述。
    /// </summary>
    public class ClsDBScanHistory
    {
        private string strProductID;
        private string strIMEI;
        private string strPicasso;
        private string strModel;
        private string strSerialNO;
        private string strPanelID;
        private int strLabelType;

        #region  Property
        public string FProductID
        {
            get
            {
                return strProductID;
            }
            set
            {
                strProductID = value;
            }
        }

        public string FIMEI
        {
            get
            {
                return strIMEI;
            }
            set
            {
                strIMEI = value;
            }
        }

        public string FPicasso
        {
            get
            {
                return strPicasso;
            }
            set
            {
                strPicasso = value;
            }
        }

        public string FModel
        {
            get
            {
                return strModel;
            }
            set
            {
                strModel = value;
            }
        }
        public string FSerialNO
        {
            get
            {
                return strSerialNO;
            }
            set
            {
                strSerialNO = value;
            }
        }
        public string FPanelID
        {
            get
            {
                return strPanelID;
            }
            set
            {
                strPanelID = value;
            }
        }

        public int FLabelType
        {
            get
            {
                return strLabelType;
            }
            set
            {
                strLabelType = value;
            }
        }
        #endregion
        public ClsDBScanHistory()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public string setPICASSO(string v_picasso)
        {
            /*string StrSql = "SELECT PRODUCT_ID FROM MES_PACK_HISTORY a "
                + " WHERE PICASSO="+ClsCommon.GetSqlString(v_picasso)
                + " AND STATE_ID='P' "
                + " AND CREATION_DATE=(SELECT MAX(CREATION_DATE) FROM MES_PACK_HISTORY "
                + " WHERE PRODUCT_ID=a.PRODUCT_ID AND picasso is not null)";*/
            string Result = "";
            string strSql;
            strSql = "SELECT SUBSTR(PPART,3,3)MODEL FROM CMCS_SFC_IMEINUM T WHERE SERIAL_NUM =" + ClsCommon.GetSqlString(v_picasso) + " OR IMEINUM =" + ClsCommon.GetSqlString(v_picasso);
            try
            {
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                string strModel = dt1.Rows[0][0].ToString();
            
                //for gng gongyebao------------
                string StrSql; 
                //StrSql= "SELECT PRODUCTID FROM CMCS_SFC_SHIPPING_DATA a "
                //    + " WHERE SERIAL_NO=" + ClsCommon.GetSqlString(v_picasso);
                //DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];

                if (strModel=="GNG")
                     StrSql = "SELECT PRODUCT_ID FROM SHP.CMCS_SFC_IMEINUM Where SERIAL_NUM=" 
                         + ClsCommon.GetSqlString(v_picasso); 
                else
                     StrSql= "SELECT PRODUCTID FROM CMCS_SFC_SHIPPING_DATA a " 
                         + " WHERE SERIAL_NO=" + ClsCommon.GetSqlString(v_picasso);
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strPicasso = v_picasso;
                    Result = setProductID(dt.Rows[0][0].ToString(), "");
                }
                else //如果在出入庫中找不到，則在E2P里找.
                {
                    //StrSql = "SELECT SUBSTR(PPART,3,3)MODEL FROM CMCS_SFC_IMEINUM T WHERE SERIAL_NUM =" + ClsCommon.GetSqlString(v_picasso) + " OR IMEINUM =" + ClsCommon.GetSqlString(v_picasso);
                    //dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                    //string strModel = dt.Rows[0][0].ToString();
                    if (!(strModel.Equals("RCX")) || !(strModel.Equals("DVR")) || !(strModel.Equals("GNG")) || !(strModel.Equals("SLG")))
                    {
                        string strsql = "SELECT PRODUCTID FROM " + strModel + ".E2PCONFIG T WHERE PICASSO=" + ClsCommon.GetSqlString(v_picasso) + " AND E2PDATE=(SELECT MAX(E2PDATE) FROM " + strModel + ".E2PCONFIG WHERE T.PICASSO=PICASSO) ";
                        DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                        if (dt2.Rows.Count > 0)
                        {
                            setProductID(dt2.Rows[0][0].ToString(), ""); 
                        }  
                        else
                            Result = "Picasso is not in use !";
                    }
                    else
                    {
                        string strpid = v_picasso;
                        setProductID(v_picasso, strModel);
                    }                    
                }
            }
            catch
            {
                string strsql1="select linkid from SHP.CMCS_SFC_CKD_SERIALNO where serialno="+ ClsCommon.GetSqlString(v_picasso);
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    strPicasso = v_picasso;
                    Result = setProductID(dt1.Rows[0][0].ToString(), "");
                }
                else
                {
                    Result = "Picasso is not exist!";
                }
            }
            return Result;
        }

        public string setIMEI(string v_imei)
        {
            /*string StrSql = "SELECT PRODUCT_ID FROM MES_PACK_HISTORY a "
                + " WHERE IMEI=" + ClsCommon.GetSqlString(v_imei)
                + " AND STATE_ID='P' "
                + " AND CREATION_DATE=(SELECT MAX(CREATION_DATE) FROM MES_PACK_HISTORY "
                + " WHERE PRODUCT_ID=a.PRODUCT_ID AND STATE_ID='P')";
            */
            string Result = "";
            string strmodel = "";
         
            try
            {
                string StrSql;
                if (v_imei.Substring(0, 1).ToUpper().Equals("A"))
                    StrSql = "SELECT PRODUCTID,model,imei FROM CMCS_SFC_SHIPPING_DATA a "
                    + " WHERE IMEI LIKE  '" + v_imei + "%'";
                else
                    StrSql = "select product_id,ppart,imeinum from SHP.CMCS_SFC_IMEINUM where imeinum like  '" + v_imei + "%' or ppid_num like '" + v_imei + "%' or CUSTOMER_NUM like '" + v_imei + "%'";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strIMEI = dt.Rows[0][2].ToString();
                    if (dt.Rows[0][0].ToString().Trim() == "" && !strIMEI.Equals(""))
                    {
                        strmodel = dt.Rows[0][1].ToString().Substring(2, 3);
                        string strsql0 = "SELECT PRODUCTID FROM CMCS_SFC_SHIPPING_DATA a " + " WHERE IMEI LIKE  '" + strIMEI + "%'";
                        DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
                        if (dt0.Rows.Count > 0)
                        {
                            strIMEI = dt.Rows[0][2].ToString();
                            if(!strmodel.Equals(""))
                                Result = setProductID(dt0.Rows[0][0].ToString(), strmodel);
                            else
                                Result = setProductID(dt0.Rows[0][0].ToString(), "");
                        }
                        else
                        {
                            string strsql1 = "SELECT  PRODUCTID FROM " + strmodel + ".E2PCONFIG T WHERE PICASSO=" + ClsCommon.GetSqlString(v_imei) + " OR IMEI LIKE " + ClsCommon.GetSqlString( strIMEI + "%");
                            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                            if (dt1.Rows.Count > 0)
                            {
                                if (!strmodel.Equals(""))
                                    Result = setProductID(dt1.Rows[0][0].ToString(), strmodel);
                                else
                                    Result = setProductID(dt1.Rows[0][0].ToString(), "");
 
                            }
                            else
                            {
                                Result = "Imei is not in use!";
                            }
                        }

                    }
                    else
                    {
                        Result = setProductID(dt.Rows[0][0].ToString(), "");
                    }
                }
                else //如果在出入庫中找不到，則在E2P里找. 
                {
                    StrSql = "SELECT SUBSTR(type_code,1,3) MODEL FROM CMCS_SFC_IMEINUM T,SHP.ROS_TCH_PN s WHERE t.ppart=s.ppart and (SERIAL_NUM =" + ClsCommon.GetSqlString(v_imei) + " OR IMEINUM LIKE " + ClsCommon.GetSqlString(v_imei + "%")+")";
                    dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                    if (dt.Rows.Count > 0)
                        strModel = dt.Rows[0][0].ToString();
                    if (strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "RUY" || strModel == "TWN" || strModel == "MRO" || strModel == "CAS")
                        StrSql = "SELECT SERIAL_NUM FROM CMCS_SFC_IMEINUM T WHERE SERIAL_NUM =" + ClsCommon.GetSqlString(v_imei) + " OR IMEINUM LIKE " + ClsCommon.GetSqlString(v_imei + "%");
                    else
                        StrSql = "SELECT  PRODUCTID FROM " + dt.Rows[0][0].ToString() + ".E2PCONFIG T WHERE PICASSO=" + ClsCommon.GetSqlString(v_imei) + " OR IMEI LIKE " + ClsCommon.GetSqlString(v_imei + "%");//+" AND E2PDATE=(SELECT MAX(E2PDATE) FROM "+dt.Rows[0][0].ToString()+".E2PCONFIG WHERE T.PICASSO=PICASSO) ";
                    dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                    if (!dt.Rows[0][0].ToString().Equals(""))
                        setProductID(dt.Rows[0][0].ToString(), "");
                    else
                        Result = "Imei is not in use!";
                }
            }
            catch
            {
                Result = "Imei is not exist!";
            }
            return Result;
        }

        public string setProductID(string ProductID, string strModel1)
        {
            string Result = "";
            try
            {
                strProductID = ProductID;
                string StrSql = "";
                DataTable dt = null;
                string strsql="";
                if (strModel1.Equals(""))
                {
                    strsql = "SELECT DB_USER,SPART FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL " +
                     "WHERE SPART = (SELECT SPART FROM SFC.PRODUCT_PANEL_SORDER WHERE PRODUCT_ID = "
                     + ClsCommon.GetSqlString(strProductID) + ")";

                    //string strsql = "SELECT MODEL FROM Sfc.PRODUCT_PANEL_SORDER WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(strProductID);
                    dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                    strModel1 = dt.Rows[0]["DB_USER"].ToString();
                }              

                if (strModel1.Equals(""))
                {  
                    dt = null;
                    StrSql = "SELECT SUBSTR(IMEI,1,15)IMEI, PICASSO FROM " + strModel + ".E2PCONFIG a "
                        + " WHERE PRODUCTID=" + ClsCommon.GetSqlString(strProductID)
                        + " AND STATUS LIKE 'P%' "
                        + " AND E2PDATE=(SELECT MAX(E2PDATE) FROM " + strModel + ".E2PCONFIG "
                        + " WHERE PRODUCTID=a.PRODUCTID "
                        + " AND STATUS LIKE 'P%')";
                    dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        strIMEI = dt.Rows[0]["IMEI"].ToString();
                        strPicasso = dt.Rows[0]["PICASSO"].ToString();
                        if (strPicasso != "")
                        {
                            strSerialNO = strPicasso;
                        }
                        else
                        {
                            StrSql = "SELECT SERIAL_NO FROM CMCS_SFC_SHIPPING_DATA "
                                + "WHERE IMEI=" + ClsCommon.GetSqlString(strIMEI);
                            dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                strSerialNO = dt.Rows[0]["SERIAL_NO"].ToString();
                            }
                        }
                    }
                }
                StrSql = "SELECT T.PANEL_ID FROM MES_PCBA_PANEL_LINK T WHERE T.PRODUCT_ID = " + ClsCommon.GetSqlString(strProductID);
                dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strPanelID = dt.Rows[0][0].ToString();
                }
                else
                {
                    strPanelID = "";
                }
            }
            catch
            {
                Result = "Product ID is not exist!";
            }

            return Result;
        }
        public DataTable GetWoInfo(string ProductID)
        {
            string StrSql = "SELECT 'PCBA'MFG_TYPE,SORDER WO_NO,SUBSTR(SPART,3,3)MODEL,SPART PN,PID_QTY WO_QTY FROM CMCS_SFC_SORDER "
                + " WHERE SORDER IN (SELECT  WO_NO FROM MES_PCBA_HISTORY "
                + " WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(ProductID) + ") "
                + " UNION "
                + " SELECT 'ASSY'MFG_TYPE,AORDER WO_NO,SUBSTR(APART,3,3)MODEL,APART PN,APID_QTY WO_QTY FROM CMCS_SFC_AORDER "
                + " WHERE AORDER IN (SELECT  WO_NO FROM MES_ASSY_HISTORY "
                + " WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(ProductID) + ") "
                + " UNION "
                + " SELECT 'PACK'MFG_TYPE,PORDER WO_NO,SUBSTR(PPART,3,3)MODEL,PPART PN,PPID_QTY WO_QTY FROM CMCS_SFC_PORDER "
                + " WHERE PORDER IN (SELECT  WO_NO FROM MES_PACK_HISTORY "
                + " WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(ProductID) + ") ";
            return ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        }

        public DataTable GetProcessInfo(string ProductID, string strPanel)
        {
            string StrSql = "SELECT OLDPID FROM SHP.CMCS_SFC_SHIPPING_RESCAN WHERE NEWPID = " + ClsCommon.GetSqlString(ProductID);
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            string strID = "";
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0][0].ToString();
            }
            StrSql = "SELECT TO_CHAR(PDDATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE STATION_CODE WHEN 'DL' THEN 'DOWNLOAD' WHEN 'CA' THEN 'CALIBRATION' WHEN 'PT' THEN 'PRETEST' WHEN 'BT' THEN 'BLUETOOCH' "
                + " WHEN 'BTWL' THEN 'BTWIRELESS' WHEN 'B1' THEN 'TBASEBAND1' "
                + " WHEN 'B2' THEN 'TBASEBAND2' WHEN 'A1' THEN 'ABASEBAND1' WHEN 'A2' THEN 'ABASEBAND2' WHEN 'A3' THEN 'ABASEBAND3' WHEN 'A4' THEN 'ABASEBAND4' WHEN 'WL' THEN 'WIRELESS' "
                + " WHEN 'D2' THEN 'REDOWNLOAD' WHEN 'E2' THEN 'E2PCONFIG' ELSE STATION_CODE END FSTATIONID,STATUS FSTATEID,LINE_CODE FLINE,EMPLOYEE EMP_ID,ERROR_MSG ERROR_CODE  "
                + " FROM " + ProductID.Substring(0, 3) + ".PRODUCT_HISTORY_V T WHERE PRODUCT_ID = " + ClsCommon.GetSqlString(ProductID)
                + " UNION "
                + " SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE SUBSTR(STATION_ID, 1, 1) || '_' || SUBSTR(STATION_ID, 3, 2) "
                + " WHEN 'A_BI' THEN 'ASSEMBLY INPUT' WHEN 'A_KT' THEN 'ABASEBAND1' WHEN 'A_CT' THEN 'ABASEBAND2' WHEN 'A_HT' THEN 'ABASEBAND3' "
                + " WHEN 'A_IT' THEN 'ABASEBAND4' WHEN 'A_DT' THEN 'WIRELESS' WHEN 'A_LT' THEN 'REDOWNLOAD' WHEN 'A_FO' THEN 'FQC' WHEN 'A_MO' THEN 'REWORK' "
                + " ELSE STATION_ID END FSTATIONID,STATE_ID FSTATEID,'Line'||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1))-64) FLINE,EMP_ID,''ERROR_CODE FROM MES_ASSY_HISTORY WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(ProductID)
                + " UNION  "
                + " SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE SUBSTR(STATION_ID, 1, 1) || '_' || SUBSTR(STATION_ID, 3, 2) WHEN 'S_AI' THEN 'SMT INPUT' WHEN 'S_BO' THEN 'X-RAY' "
                + " WHEN 'S_CO' THEN 'TOUTH UP' WHEN 'T_AI' THEN 'ROUTER' WHEN 'T_BT' THEN 'DOWNLOAD' WHEN 'T_ET' THEN 'CALIBRATION' WHEN 'T_JT' THEN 'PRETEST' "
                + " WHEN 'T_CT' THEN 'TBASEBAND1' WHEN 'T_NT' THEN 'TBASEBAND2' WHEN 'T_MT' THEN 'BTWIRELESS' WHEN 'T_DT' THEN 'BLUETOOCH' WHEN 'T_FO' THEN 'GLUE' "
                + " ELSE STATION_ID END FSTATIONID,STATE_ID FSTATEID,'Line'||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1))-64) FLINE,EMP_ID,''ERROR_CODE FROM MES_PCBA_HISTORY WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(ProductID)
                + " UNION "
                + " SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE SUBSTR(STATION_ID, 1, 1) || '_' || SUBSTR(STATION_ID, 3, 2) WHEN 'P_AT' THEN 'REDOWNLOAD' WHEN 'P_BT' THEN 'E2PCONFIG' "
                + " WHEN 'P_GO' THEN 'OQC' WHEN 'P_EO' THEN 'OOB' ELSE STATION_ID END FSTATIONID,STATE_ID FSTATEID,'Line'||TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1))-64) FLINE,EMP_ID,"
                + " (SELECT DEFECT_CODE FROM MES_PRODUCT_FAIL_HISTORY T WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_ID = S.STATION_ID AND S.STATE_ID='F')ERROR_CODE "
                + " FROM MES_PACK_HISTORY S WHERE PRODUCT_ID IN (" + ClsCommon.GetSqlString(ProductID) + "," + ClsCommon.GetSqlString(strID) + ")"
                + " UNION "
                + " SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS')  FCDATE,'ARE-IN' FSTATION,'P'FSTATEID,FACTORY_AREA FLINE,''EMP_ID,DEFECT_CODE ERROR_CODE FROM MES_REPAIR_WIP "
                + " WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(ProductID)
                + " UNION "
                + " SELECT TO_CHAR(COMPLETE_DATE,'YYYY/MM/DD HH24:MI:SS')  FCDATE,'ARE-OUT' FSTATION,'P'FSTATEID,FACTORY_AREA FLINE,REPAIR_MAN EMP_ID,DEFECT_CODE ERROR_CODE FROM MES_REPAIR_HISTORY "
                + " WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(ProductID);
            if (!strPanel.Equals(""))
            {
                StrSql = StrSql + " UNION "
                    + " SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') FCDATE,CASE SUBSTR(STATION_ID, 1, 1) || '_' || SUBSTR(STATION_ID, 3, 2) "
                    + " WHEN 'S_IA' THEN 'SMT A SITE INPUT' WHEN 'S_IB' THEN 'SMT B SITE INPUT' WHEN 'S_OA' THEN 'A SITE AOI' WHEN 'S_OB' THEN 'B SITE AOI' WHEN 'S_VA' THEN 'A SITE VI' WHEN 'S_VB' THEN "
                    + " 'B SITE VI'  ELSE STATION_ID END FSTATIONID, STATE_ID FSTATEID,'Line' || TO_CHAR(ASCII(SUBSTR(STATION_ID, 2, 1)) - 64) FLINE,EMP_NO EMP_ID,''ERROR_CODE FROM MES_PCBA_PANEL_HISTORY "
                    + " WHERE PANEL_ID LIKE " + ClsCommon.GetSqlString("%" + strPanel);
            }

            return ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        }

        public DataTable GetRelationShipInfo(string ProductID)
        {
            string StrSql = "SELECT PRODUCTID,IMEI,SERIAL_NO PICASSO,WORK_ORDER||'_'||ALLBOXID CARTONID,T.CUSTOMER_NUM MSN# FROM CMCS_SFC_SHIPPING_DATA S, SHP.CMCS_SFC_IMEINUM T "
                + " WHERE S.WORK_ORDER = T.PORDER AND (S.IMEI = T.IMEINUM OR S.SERIAL_NO = T.SERIAL_NUM) AND S.PRODUCTID=" + ClsCommon.GetSqlString(ProductID);
            return ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        }

        public DataTable GetE2PInfo(string ProductID, string InputData, int LabelType)
        {
            string StrSql = "SELECT  COMPUTERNAME,WORKORDER,PRODUCTID,DECODE(UPPER(SUBSTR(IMEI,1,4)),'SKIP','',SUBSTR(IMEI,1,15))IMEI,PICASSO,TO_CHAR(E2PDATE,'YYYY/MM/DD HH24:MI:SS')E2PDATE,STATUS,ERRORMSG,EMPLOYEE "
                + " FROM " + ProductID.Substring(0, 3) + ".E2PCONFIG WHERE  ";
            switch (LabelType)
            {
                case 0:
                    StrSql = StrSql + " PRODUCTID = " + ClsCommon.GetSqlString(InputData);
                    break;
                case 2:
                    StrSql = StrSql + " PICASSO = " + ClsCommon.GetSqlString(InputData);
                    break;
                case 1:
                    StrSql = StrSql + " IMEI LIKE " + ClsCommon.GetSqlString(InputData + "%");
                    break;
            }

            //PRODUCTID="+ClsCommon.GetSqlString(ProductID);
            return ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        }
        public DataTable GetLinkInfo(string ProductID, string InputData, int LabelType)
        {
            string StrSql = "SELECT PORDER WORKORDER,PPART,IMEINUM,SERIAL_NUM,CUSTOMER_NUM MSN,PRODUCT_ID,TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') CREATION_DATE "
                + " FROM SHP.CMCS_SFC_IMEINUM WHERE  ";
            switch (LabelType)
            {
                case 0:
                    StrSql = StrSql + " PRODUCT_ID = " + ClsCommon.GetSqlString(ProductID) +" or SERIAL_NUM = " + ClsCommon.GetSqlString(InputData);
                    break;
                case 2:
                    StrSql = StrSql + " SERIAL_NUM = " + ClsCommon.GetSqlString(InputData);
                    break;
                case 1:
                    StrSql = StrSql + " IMEINUM like '" + InputData +"%'";
                    break;
            }
            return ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        }

        public DataTable GetPanelInfo(string strPanel)
        {
            string strSql = "SELECT * FROM MES_PCBA_PANEL_DETAIL T WHERE PANEL_ID = " + ClsCommon.GetSqlString(strPanel);
            return ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        }
    }
}
