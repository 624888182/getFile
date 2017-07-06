using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for NLVClassChen
/// </summary>
public class NLVClassChen
{
    private static string FERROR = "";
	
    public NLVClassChen()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string GetDBConn()
    {
        string sRet = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.186.19.20)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=tjsfc)));uid=sfis1;pwd=sfis1";
        return sRet;
    }

    public static string GetSAPConn()
    {
        string sRet = "ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=sidcfi;PASSWD=fi61625;LANG=EN";
        return sRet;
    
    }

    public static string GetError()
    {
        return FERROR;
    }

    public static int StrToInt(string str)
    {
        int iRet = 0;
        try
        {
            Decimal fRet = Convert.ToDecimal(str);
            iRet = Convert.ToInt32(fRet);

        }
        catch
        {
            iRet = 0;
        }
        return iRet;
    }

    private static decimal StrToDec(string str)
    {
        decimal dRet = 0;
        try
        {
            dRet = Convert.ToDecimal(str);
        }
        catch
        {
            dRet = 0;
        }
        return dRet;
    }

    public static int ClearRequest(string sDN, string DBconn)
    {
        int iRet = 0;
        try
        {
            string sSql = "Update SFIS1.N_WORKORDER_DETAIL Set PRODUCT_REQUEST_QTY = 0,PRODUCT_DRAWN_QTY = 0 Where ORDER_NUMBER = :V_NO ";
            DataBaseOperation.ExecSQL("oracle", DBconn, sSql, new string[] { "V_NO" }, new object[] { sDN });
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        return iRet;
    }

    public static bool IsNumber(string sStr)
    {
        bool Ret;
        try
        {
            Convert.ToDecimal(sStr);
            Ret = true;
        }
        catch
        {
            Ret = false;
        }
        return Ret;
    
    }

    public static int UpdateRequest(string sDN, string[] sPartNO, string[] sQTY, string[] sTime, string dbconn,string sPeople)
    {
        int iRet = 0;
        string sSql = "Update SFIS1.N_WORKORDER_DETAIL Set PRODUCT_REQUEST_QTY = :V_QTY,PRODUCT_DRAWN_QTY = 0,PRODUCT_REQUEST_TIME = :V_TIME,PRODUCT_REQUEST_PEOP = :V_PEOPLE Where MATERIAL_NUMBER = :V_MAT And ORDER_NUMBER = :V_ON ";
        DataBaseOperation dbo = new DataBaseOperation("oracle", dbconn);
        int i;
        for (i = 0; i < sPartNO.GetLength(0); i++)
        {
            dbo.ExecSQL(sSql, new string[] { "V_QTY", "V_TIME", "V_MAT", "V_ON","V_PEOPLE" }, new object[] { sQTY[i], sTime[i], sPartNO[i], sDN ,sPeople});
        }
        return iRet;
    
    }

    public static string UpdateMaterielMuil(string DN, string sapconn, string DBconn)
    {
        string sRet = "";
        string[] DNS = DN.Replace("/r","").Replace("/n","").Split(',');
        int i;
        for (i = 0; i < DNS.GetLength(0); i++)
        {
            int iRet = UpdateMateriel(DNS[i], sapconn, DBconn);
            if (iRet != 0)
                sRet = sRet + "WO: " + DNS[i] + " ERROR: " + GetError();
        }
        return sRet;
    }

    public static DataTable SelectWO(string DN, string DBconn)
    {
        DataTable dt = null;
        string[] DNS = DN.Replace("/r", "").Replace("/n", "").Split(',');
        string sSql = "Select Order_Number,Order_Type,PLANT,MATERIAL_NUMBER,Total_ORDER_QTY,GSTRP,GLTRP,GSTRS,GLTRS From SFIS1.N_WORKORDER ";
        int i;
        for (i = 0; i < DNS.GetLength(0); i++)
        {
            if (i == 0)
                sSql += " Where Order_Number Like '%" + DNS[i] + "%' ";
            else
                sSql += "OR Order_Number Like '%" + DNS[i] + "%' ";
        }
        dt = DataBaseOperation.SelectSQLDT("oracle", DBconn, sSql);
        return dt;
    }

    public static DataTable SelectWOItem(string DN, string DBConn)
    {
        DataTable dt = null;
        string sSql = "Select MATERIAL_NUMBER,REQUIREMENTS_QUANTITY,QUANTITY_WITHDRAWN,REQUIREMENTS_QUANTITY-QUANTITY_WITHDRAWN POSSIBLE_QUANTITY,PRODUCT_REQUEST_QTY,PRODUCT_REQUEST_TIME,PRODUCT_DRAWN_QTY,PRODUCT_REQUEST_PEOP From  SFIS1.N_WORKORDER_DETAIL Where ORDER_NUMBER = :V_WO ";
        dt = DataBaseOperation.SelectSQLDT("oracle", DBConn, sSql, new string[] { "V_WO" }, new object[] { DN });
        return dt;
    }

    public static DataTable SelectWOAllField(string WO, string DBConn)
    {
        DataTable dt = null;
        string sSql = @"Select PLANT,PLANT_TYPE,ORDER_NUMBER,ORDER_TYPE,SYSTEM_STATUS_LINE,EXTERNAL_MATERIAL_GROUP,
                        MATERIAL_NUMBER,MATERIAL_DESCRIPTION,BASE_UNIT,TOTAL_ORDER_QTY,DELIVERED_QTY,IGMNG,
                        IASMG,GSTRP,GLTRP,FTRMI,GSTRS,GLTRS,RSNUM
                        From SFIS1.N_WORKORDER Where ORDER_NUMBER = :V_ON ";

        dt = DataBaseOperation.SelectSQLDT("oracle", DBConn, sSql, new string[] { "V_ON" }, new object[] { WO });
        return dt;
    }

    public static int UpdateMateriel(string DN, string Sapconn, string DBconn)
    {
        string[] DNArray = { DN };
        int iRet = 0;
        string sDoc = "";

        #region CallSapClass
        try 
        {
            TestCallSap tcs = new TestCallSap();
            sDoc = tcs.Call_ZPRO_COMPONENT(DNArray, Sapconn, DBconn);
            if (sDoc == "-1")
            {
                FERROR = tcs.GetError();
                return -1;
            }
        }
        catch
        {
            sDoc = "";
            FERROR = "Call SAP Error";
            return -1;
        }
        #endregion

        #region UpdateMateriel&GetWO
        try
        {
            #region SelectTemptable
            string sSql = @"select AUFNR,HEADER_MATNR,MATNR,MAKTX,BDMNG,ENMNG,MEINS,WERKS,LGORT,LABST,INSME,SAP_Flag,BOM_FLAG,  
                            PLANT, PLANT_TYPE, ORDER_TYPE,SYSTEM_STATUS_LINE, EXTERNA_M_G, MATERIAL_DESC,
                            BASE_UNIT,TOTAL_QTY,DELIVERED_QTY,IGMNG,IASMG,GSTRP,GLTRP,FTRMI,GSTRS,GLTRS,RSNUM
                            from  publib.SAP_ZPRO_COMPONENT Where DocumentID= '" + sDoc + "' ";
            DataBaseOperation dbo = new DataBaseOperation("oracle", DBconn);
            DataTable dtMat = dbo.SelectSQLDT(sSql);
            #endregion

            #region UpdateMasterDetail
            int i;
            if (dtMat.Rows.Count > 0)
            {
                #region SetMasterValue
                string sON = dtMat.Rows[0]["AUFNR"].ToString();
                string sPlant = dtMat.Rows[0]["PLANT"].ToString();
                string sPlant_Type = dtMat.Rows[0]["PLANT_TYPE"].ToString();
                string sORDER_TYPE = dtMat.Rows[0]["ORDER_TYPE"].ToString();
                string sSTATUS_LINE = dtMat.Rows[0]["SYSTEM_STATUS_LINE"].ToString();
                string sMATERIAL_GROUP = dtMat.Rows[0]["EXTERNA_M_G"].ToString();
                string sMATERIAL_NUMBER= dtMat.Rows[0]["HEADER_MATNR"].ToString();
                string sMATERIAL_DESC = dtMat.Rows[0]["MATERIAL_DESC"].ToString();
                string sBASE_UNIT = dtMat.Rows[0]["BASE_UNIT"].ToString();
                decimal TOTAL_QTY = StrToDec(dtMat.Rows[0]["TOTAL_QTY"].ToString());
                decimal DELIVERED_QTY = StrToDec(dtMat.Rows[0]["DELIVERED_QTY"].ToString());
                decimal IGMNG = StrToDec(dtMat.Rows[0]["IGMNG"].ToString());
                decimal IASMG = StrToDec(dtMat.Rows[0]["IASMG"].ToString());
                string sGSTRP = dtMat.Rows[0]["GSTRP"].ToString();
                string sGLTRP = dtMat.Rows[0]["GLTRP"].ToString();
                string sFTRMI = dtMat.Rows[0]["FTRMI"].ToString();
                string sGSTRS = dtMat.Rows[0]["GSTRS"].ToString();
                string sGLTRS = dtMat.Rows[0]["GLTRS"].ToString();
                decimal RSNUM = StrToDec(dtMat.Rows[0]["RSNUM"].ToString());
                #endregion

                #region SelectMaster
                string sqlSelect = "Select ORDER_NUMBER From SFIS1.N_WORKORDER Where ORDER_NUMBER = :V_ON ";
                DataTable dtM = dbo.SelectSQLDT(sqlSelect,new string[] {"V_ON"},new object[] {sON});
                string sqlUpdate = "";
                #endregion

                #region MasterSQL
                if (dtM.Rows.Count > 0)
                {
                    sqlUpdate = @"Update SFIS1.N_WORKORDER Set 
                                PLANT = :V_PLANT,
                                PLANT_TYPE= :V_PLANT_TYPE,
                                ORDER_TYPE = :V_ORDER_TYPE, 
                                SYSTEM_STATUS_LINE =:V_SYSTEM_STATUS_LINE,
                                EXTERNAL_MATERIAL_GROUP =:V_EXTERNAL_MATERIAL_GROUP, 
                                MATERIAL_NUMBER = :V_MATERIAL_NUMBER,
                                MATERIAL_DESCRIPTION=:V_MATERIAL_DESCRIPTION,
                                BASE_UNIT = :V_BASE_UNIT,
                                TOTAL_ORDER_QTY = :V_TOTAL_ORDER_QTY,
                                DELIVERED_QTY  = :V_DELIVERED_QTY,
                                IGMNG  = :V_IGMNG,
                                IASMG  = :V_IASMG,
                                GSTRP = :V_GSTRP,
                                GLTRP = :V_GLTRP,
                                FTRMI = :V_FTRMI,
                                GSTRS= :V_GSTRS,
                                GLTRS = :V_GLTRS,
                                RSNUM = :V_RSNUM,
                                STATUS=0,
                                UPDATE_DATE=Sysdate
                                Where ORDER_NUMBER = :V_ORDER_NUMBER ";
                }
                else
                {
                    sqlUpdate = @"Insert Into SFIS1.N_WORKORDER 
                                (PLANT,PLANT_TYPE,ORDER_TYPE,SYSTEM_STATUS_LINE,EXTERNAL_MATERIAL_GROUP, 
                                MATERIAL_NUMBER, MATERIAL_DESCRIPTION,BASE_UNIT,TOTAL_ORDER_QTY,DELIVERED_QTY,
                                IGMNG,IASMG,GSTRP,GLTRP,FTRMI,GSTRS,GLTRS,RSNUM,ORDER_NUMBER,STATUS,UPDATE_DATE)
                                Values(:V_PLANT,:V_PLANT_TYPE,:V_ORDER_TYPE,:V_SYSTEM_STATUS_LINE,
                                :V_EXTERNAL_MATERIAL_GROUP,:V_MATERIAL_NUMBER,:V_MATERIAL_DESCRIPTION,
                                :V_BASE_UNIT,:V_TOTAL_ORDER_QTY,:V_DELIVERED_QTY,
                                :V_IGMNG,:V_IASMG,:V_GSTRP,:V_GLTRP,:V_FTRMI,:V_GSTRS,:V_GLTRS,:V_RSNUM,:V_ORDER_NUMBER,
                                0,sysdate) ";
                }
                #endregion
                
                #region ExecMasterSQL
                string[] paramName = {"V_PLANT","V_PLANT_TYPE","V_ORDER_TYPE","V_SYSTEM_STATUS_LINE","V_EXTERNAL_MATERIAL_GROUP","V_MATERIAL_NUMBER",
                                         "V_MATERIAL_DESCRIPTION","V_BASE_UNIT","V_TOTAL_ORDER_QTY","V_DELIVERED_QTY","V_IGMNG","V_IASMG","V_GSTRP",
                                         "V_GLTRP","V_FTRMI","V_GSTRS","V_GLTRS","V_RSNUM","V_ORDER_NUMBER"};
                object[] paramValue = {sPlant,sPlant_Type,sORDER_TYPE,sSTATUS_LINE,sMATERIAL_GROUP,sMATERIAL_NUMBER,
                                      sMATERIAL_DESC,sBASE_UNIT,TOTAL_QTY,DELIVERED_QTY,IGMNG,IASMG,sGSTRP,
                                      sGLTRP,sFTRMI,sGSTRS,sGLTRS,RSNUM,sON};
                dbo.ExecSQL(sqlUpdate,paramName,paramValue);
                #endregion

                #region DetailOperation
                for (i=0;i< dtMat.Rows.Count;i++)
                {
                    #region SetDetailValue
                    string sOrderNumber= dtMat.Rows[i]["AUFNR"].ToString();
                    string sMaterialNumber = dtMat.Rows[i]["MATNR"].ToString();
                    string sMaterialDesc = dtMat.Rows[i]["MAKTX"].ToString();
                    decimal REQUIRE_QTY = StrToDec(dtMat.Rows[i]["BDMNG"].ToString());
                    decimal DRAWN_QTY = StrToDec(dtMat.Rows[i]["ENMNG"].ToString());
                    string sUNIT = dtMat.Rows[i]["MEINS"].ToString();
                    string sPLANT = dtMat.Rows[i]["WERKS"].ToString();
                    string sLOCATION = dtMat.Rows[i]["LGORT"].ToString();
                    decimal LABST = StrToDec(dtMat.Rows[i]["LABST"].ToString());
                    decimal INSME = StrToDec(dtMat.Rows[i]["INSME"].ToString());
                    #endregion

                    #region SelectDetail
                    sqlSelect = "Select ORDER_NUMBER From SFIS1.N_WORKORDER_DETAIL Where MATERIAL_NUMBER= :V_MN And ORDER_NUMBER = :V_ON ";
                    DataTable dtS = dbo.SelectSQLDT(sqlSelect,new string[] {"V_MN","V_ON"},new object[] {sMaterialNumber,sOrderNumber});
                    #endregion

                    #region SetDetailUpdateSQL
                    if (dtS.Rows.Count > 0)
                    {
                        sqlUpdate = @"Update SFIS1.N_WORKORDER_DETAIL Set 
                                    MATERIAL_DESCRIPTIN  = :V_MATERIAL_DESC,
                                    REQUIREMENTS_QUANTITY = :V_REQUIRE_QTY,
                                    QUANTITY_WITHDRAWN   = :V_DRAWN_QTY,
                                    BASE_UNIT = :V_BASE_UNIT,
                                    PLANT = :V_PLANT,
                                    STORAGE_LOCATION =:V_STORAGE_LOCATION,
                                    LABST = :V_LABST,
                                    INSME = :V_INSME
                                    Where MATERIAL_NUMBER = :V_MATERIAL_NUMBER And ORDER_NUMBER = :V_ORDER_NUMBER ";
                    }
                    else
                    {
                        sqlUpdate = @"Insert Into SFIS1.N_WORKORDER_DETAIL 
                                    (MATERIAL_DESCRIPTIN,REQUIREMENTS_QUANTITY,QUANTITY_WITHDRAWN,BASE_UNIT,
                                    PLANT,STORAGE_LOCATION,LABST,INSME,
                                    MATERIAL_NUMBER,ORDER_NUMBER,
                                    PRODUCT_REQUEST_QTY,PRODUCT_DRAWN_QTY,PRODUCT_TOTAL_DRAWN_QTY,PRODUCT_REQUEST_TIME)
                                    Values(:V_MATERIAL_DESC,:V_REQUIRE_QTY,:V_DRAWN_QTY,:V_BASE_UNIT,
                                    :V_PLANT,:V_STORAGE_LOCATION,:V_LABST,:V_INSME,
                                    :V_MATERIAL_NUMBER,:V_ORDER_NUMBER,
                                    0,0,0,'') ";
                    }
                    #endregion

                    #region ExecDetailSQL
                    string[] p1 = {"V_MATERIAL_DESC","V_REQUIRE_QTY","V_DRAWN_QTY","V_BASE_UNIT","V_PLANT","V_STORAGE_LOCATION",
                                      "V_LABST","V_INSME","V_MATERIAL_NUMBER","V_ORDER_NUMBER"};
                    object[] p2 = {sMaterialDesc,REQUIRE_QTY,DRAWN_QTY,sUNIT,sPLANT,sLOCATION,LABST,INSME,sMaterialNumber,sOrderNumber};
                    dbo.ExecSQL(sqlUpdate,p1,p2);
                    #endregion

                }
                #endregion
            }
            #endregion

            #region OldVerison
            /*
            for (i = 0; i < dtMat.Rows.Count; i++)
            {
                string s
                string sSqlSelect = "Select Count(*) From SFIS1.N_WORKORDER_DETAIL Where ORDER_NUMBER = :V_ON And MATERIAL_NUMBER = :V_MN ";
                DataTable dtS = dbo.SelectSQLDT(sSqlSelect, new string[] { "V_ON", "V_MN" }, new object[] { });
            
            
            }

            for (i = 0; i < dtMat.Rows.Count; i++)
            {
                string sBOM_NO = dtMat.Rows[i][0].ToString() + "T";
                string sLine= dtMat.Rows[i][0].ToString();
                string sMateria = dtMat.Rows[i][1].ToString();
                string sqlSelect = "Select Count(*) From  SFIS1.C_SMT_BOM_T Where BOM_NO = :V_NO And KEY_PART_NO = :V_PNO ";
                DataTable dtS = dbo.SelectSQLDT(sqlSelect, new string[] { "V_NO", "V_PNO" }, new object[] { sBOM_NO, dtMat.Rows[i][2].ToString() });
                string sqlUpdate = "";
                if (dtS.Rows[0][0].ToString() == "0")
                    sqlUpdate = "Insert into SFIS1.C_SMT_BOM_T (BOM_NO,LINE_NAME,MACHINE_CODE,TRACK_NO,KEY_PART_NO,KP_RELATION,EMP_NO,CREATE_TIME,UPDATE_TIME,STD_QTY,SCRAP_QTY) " +
                        "Values (:V_VT,:V_V1,:V_V2,:V_V3,:V_V5,'','SFC',sysdate,sysdate,:V_V6,0) ";
                else
                    sqlUpdate = "Update SFIS1.C_SMT_BOM_T set LINE_NAME = :V_V1,MACHINE_CODE = :V_V2,TRACK_NO = :V_V3,EMP_NO = 'SFC',UPDATE_TIME = sysdate,STD_QTY=:V_V6 " +
                        " Where BOM_NO = :V_VT And KEY_PART_NO = :V_V5 ";

                dbo.ExecSQL(sqlUpdate, new string[] { "V_VT","V_V1", "V_V2", "V_V3", "V_V5", "V_V6" }, new object[] { sBOM_NO,dtMat.Rows[i][0].ToString(), dtMat.Rows[i][1].ToString(), i.ToString(), dtMat.Rows[i][2].ToString(), StrToInt(dtMat.Rows[i][4].ToString()) } );

                sqlSelect = "Select Count(*) From SFISM6.R_SMT_MACHINE_INV_T Where LINE_NAME = :V_V1 And TRACK_NO = :V_V3 ";
                DataTable dt_INV = dbo.SelectSQLDT(sqlSelect, new string[] { "V_V1", "V_V3" }, new object[] { sLine, i.ToString() });
                if (dt_INV.Rows[0][0].ToString() == "0")
                {
                    sqlUpdate = "Insert Into SFISM6.R_SMT_MACHINE_INV_T (LINE_NAME,MACHINE_CODE,TRACK_NO) VALUES (:V_V1,:V_V2,:V_V3) ";
                }
                else
                {
                    sqlUpdate = "Update SFISM6.R_SMT_MACHINE_INV_T Set MACHINE_CODE= :V_V2 Where LINE_NAME= :V_V1 And TRACK_NO = :V_V3";
                }
                dbo.ExecSQL(sqlUpdate, new string[] { "V_V1", "V_V3","V_V2" }, new object[] { dtMat.Rows[i][0].ToString(), i.ToString(), sMateria });
            }

            if (dtMat.Rows.Count > 0)
            {
                string sWO = dtMat.Rows[0][0].ToString();
                string sBOM_NO = dtMat.Rows[0][0].ToString() + "T";
                string sMat = dtMat.Rows[0][1].ToString();
                sSql = "Select Count(*) From SFIS1.C_LINE_DESC_T Where LINE_NAME = :V_LINE ";
                DataTable dt_Line = dbo.SelectSQLDT(sSql, new string[] { "V_LINE" }, new object[] { sWO });
                if (dt_Line.Rows[0][0].ToString() == "0")
                {
                    sSql = "Insert into SFIS1.C_LINE_DESC_T (LINE_NAME,LINE_TYPE,LINE_DESC,LINE_DESC2,LINE_MODULE,SMO_TYPE,LINESTATION) " +
                           " VALUES (:V_V1,'99',:V_V1,'3F','WO','ON','BF') ";
                    dbo.ExecSQL(sSql, new string[] { "V_V1" }, new object[] { sWO });
                }

                sSql = "Select Count(*) FROM SFISM6.R_SMT_MACHINE_MODEL_T Where  LINE_NAME = :V_LINE ";
                DataTable dt_Machine = dbo.SelectSQLDT(sSql, new string[] { "V_LINE" }, new object[] { sWO });
                if (dt_Machine.Rows[0][0].ToString() == "0")
                {
                    sSql = "Insert Into SFISM6.R_SMT_MACHINE_MODEL_T " +
                           "(LINE_NAME,MACHINE_CODE,MO_NUMBER,BOM_NO,MODEL_NAME,UPDATE_Time,CUR_QTY,SINGLE_DOWN,SINGLE_UP,PREP) " +
                           "VALUES (:V_V1,:V_V2,:V_V1,:V_VT,:V_V2,sysdate,0,'Y','Y','1') ";
                }
                else
                {
                    sSql = "Update SFISM6.R_SMT_MACHINE_MODEL_T  SET MACHINE_CODE= :V_V2,MO_NUMBER=:V_V1,BOM_NO=:V_VT,MODEL_NAME=:V_V2,UPDATE_Time= sysdate Where LINE_NAME=:V_V1 ";
                }
                dbo.ExecSQL(sSql, new string[] { "V_V1", "V_V2","V_VT" }, new object[] { sWO, sMat,sBOM_NO});
                sSql = "Select Count(*)  from SFISM4.R_SMT_PROD_BOM_T Where  LINE_NAME = :V_LINE ";
                DataTable dt_Prod = dbo.SelectSQLDT(sSql, new string[] { "V_LINE" }, new object[] { sWO });
                if (dt_Prod.Rows[0][0].ToString() == "0")
                {
                    sSql = "Insert Into  SFISM4.R_SMT_PROD_BOM_T (PRODUCT_NO,VER,LINE_NAME,BOM_NO,EMP_NO,LAST_LOG_TIME,STATUS) " +
                           " Values (:V_V2,'N/A',:V_V1,:V_VT,'SFC',sysdate,'Y') ";
                }
                else
                {
                    sSql = "UPDATE SFISM4.R_SMT_PROD_BOM_T Set BOM_NO= :V_VT,STATUS='Y',LAST_LOG_Time= sysdate,PRODUCT_NO=:V_V2 Where LINE_NAME=:V_V1 ";
                }
                dbo.ExecSQL(sSql, new string[] { "V_V1", "V_V2","V_VT" }, new object[] { sWO, sMat,sBOM_NO});


                sSql = "Select TRACK_NO ID,BOM_NO Order_Number,MACHINE_CODE Order_Material_Number,KEY_PART_NO Material_Number From sfis1.C_SMT_BOM_T Where LINE_NAME = :V_NO ";
                dt = dbo.SelectSQLDT(sSql, new string[] { "V_NO" }, new object[] { dtMat.Rows[0][0].ToString() });
            }
            */
            #endregion
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        #endregion

        return iRet;
    }
}
