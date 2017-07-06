using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.OracleClient;
using System.Data;
using System.Configuration;
using Microsoft.Adapter.SAP;
using System.IO;
using System.Net;
/// <summary>
/// Summary description for TestCallSap
/// </summary>
public class TestCallSap
{
    private string FERROR;
    private string sapconnect;
    private string databaseconn;

	public TestCallSap()
	{
        FERROR = "";
		//
		// TODO: Add constructor logic here
		//
	}
    private SAPConnection ConnectSAP()
    {
        SAPConnection sapconn = new SAPConnection(sapconnect);
        return sapconn;
    }


    public string GetError()
    {
        string sRet = FERROR;
        return sRet;
    }

    private string FillString(string sStr, string Def, int Len)
    {
        string Ret = sStr;
        while (Ret.Length < Len)
        {
            Ret += Def;
        }
        return Ret;
    }

    private DataTable InAufnr(string[] MOCode)
    {
        DataTable _dtRes = new DataTable("INAUFNR");
        //
        _dtRes.Columns.Add("SIGN");
        _dtRes.Columns.Add("OPTIONS");
        _dtRes.Columns.Add("LOW");
        _dtRes.Columns.Add("HIGH");
        for (int i = 0; i < MOCode.GetLength(0); i++)
        {
            _dtRes.Rows.Add("I", "EQ", MOCode[i], "");
        }//
        return _dtRes;
    }

    public string Call_ZPRO_COMPONENT(string[] DN, string sapcon, string oraclecon)
    {
        int iRet = 0;
        DateTime CH = DateTime.Now;
        string DocumentID = CH.ToString("yyyyMMddhhmmssmm");
        string orcalConnect = "";

        //chenhongbo chb=new chenhongbo(sapcon,oraclecon);
        //DataTable dt = new DataTable();
        sapconnect = sapcon;
        databaseconn = oraclecon;

        string cmdText = "EXEC ZRFC_PP_DPRS_ORDERINFO @HEADER=@DN_H OUTPUT,@COMPONENT=@DN_I OUTPUT,@IN_AUFNR=@IN_TABLAUFNR OUTPUT";

        try
        {
            DataBaseOracleConnection dboc = new DataBaseOracleConnection(databaseconn);
            SAPConnection sap = ConnectSAP();
            sap.Open();
            SAPCommand cmd = new SAPCommand(sap);
            cmd.CommandText = cmdText;

            SAPParameter DN_H = new SAPParameter("@DN_H", ParameterDirection.InputOutput);
            cmd.Parameters.Add(DN_H);

            SAPParameter DN_I = new SAPParameter("@DN_I", ParameterDirection.InputOutput);
            cmd.Parameters.Add(DN_I);

            SAPParameter IN_TABLEAUFNR = new SAPParameter("@IN_TABLAUFNR", ParameterDirection.InputOutput);
            IN_TABLEAUFNR.Value = InAufnr(DN);
            cmd.Parameters.Add(IN_TABLEAUFNR);

            SAPDataReader drSAP = cmd.ExecuteReader();
            DataTable dtHEADER = (DataTable)cmd.Parameters["@DN_H"].Value;
            DataTable dt = (DataTable)cmd.Parameters["@DN_I"].Value;

            string header_m = dtHEADER.Rows[0][6].ToString();
            string sPlant = dtHEADER.Rows[0]["WERKS"].ToString().Replace("'","''");
            string sPlantType = dtHEADER.Rows[0]["TYPE"].ToString().Replace("'", "''");
            string sOrderType = dtHEADER.Rows[0]["AUART"].ToString().Replace("'", "''");
            string sSystemStatus = dtHEADER.Rows[0]["STATUS"].ToString().Replace("'", "''");
            string sMG = dtHEADER.Rows[0]["EXTWG"].ToString().Replace("'", "''");
            string sMD = dtHEADER.Rows[0]["MAKTX"].ToString().Replace("'", "''");
            string sUnit = dtHEADER.Rows[0]["MEINS"].ToString().Replace("'", "''");
            string sTotalQty = dtHEADER.Rows[0]["GAMNG"].ToString().Replace("'", "''");
            string sDelQty = dtHEADER.Rows[0]["WEMNG"].ToString().Replace("'", "''");
            string sIGMNG = dtHEADER.Rows[0]["IGMNG"].ToString().Replace("'", "''");
            string sIASMG = dtHEADER.Rows[0]["IASMG"].ToString().Replace("'", "''");
            string sGSTRP = ((DateTime)(dtHEADER.Rows[0]["GSTRP"])).ToString("yyyy/MM/dd");
            string sGLTRP = ((DateTime)(dtHEADER.Rows[0]["GLTRP"])).ToString("yyyy/MM/dd");
            string sFTRMI = ((DateTime)(dtHEADER.Rows[0]["FTRMI"])).ToString("yyyy/MM/dd");
            string sGSTRS = ((DateTime)(dtHEADER.Rows[0]["GSTRS"])).ToString("yyyy/MM/dd");
            string sGLTRS = ((DateTime)(dtHEADER.Rows[0]["GLTRS"])).ToString("yyyy/MM/dd");
            string sRSNUM = dtHEADER.Rows[0]["RSNUM"].ToString().Replace("'","''");


            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    orcalConnect = @"insert into PUBLIB.SAP_ZPRO_COMPONENT 
                                    (DOCUMENTID,AUFNR,HEADER_MATNR,MATNR,MAKTX,BDMNG,ENMNG,MEINS,WERKS,LGORT,LABST,INSME,SAP_FLAG,BOM_FLAG,
                                    PLANT, PLANT_TYPE,ORDER_TYPE,SYSTEM_STATUS_LINE,EXTERNA_M_G,MATERIAL_DESC,BASE_UNIT,TOTAL_QTY,DELIVERED_QTY,
                                    IGMNG,IASMG ,GSTRP,GLTRP,FTRMI,GSTRS,GLTRS,RSNUM)
                                     Values('" + DocumentID + "','" + dt.Rows[i][0].ToString() + "','" + header_m + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','','','"+
                                    sPlant +"','"+sPlantType+"','"+sOrderType+"','"+sSystemStatus +"','"+sMG+"','"+sMD+"','"+
                                    sUnit +"','"+sTotalQty+"','"+sDelQty +"','"+sIGMNG+"','"+sIASMG+"','"+sGSTRP+"','"+
                                    sGLTRP+"','"+sFTRMI +"','"+sGSTRS+"','"+sGLTRS+"','"+sRSNUM+"')";
                    dboc.ExecSQL(orcalConnect);

                }
                //orcalConnect = "select * FROM SAP_ZPRO_COMPONENT where DOCUMENTID='" + DocumentID + "'";
                //dt = dboc.SelectSQLDT(orcalConnect);
                //ddd = dt;

                //orcalConnect = "Select count(*) from SAP_ZPRO_COMPONENT where AUFNR='0" + DN + "'";
                //DataTable dt2 = dboc.SelectSQLDT(orcalConnect);
                //dt2.Rows[0][0].ToString();

            }




        }
        catch (Exception ex)
        {

            FERROR = ex.Message;
            DocumentID = "-1";
        }


        return DocumentID;
    }
}

