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
using SAP.Middleware.Connector;

/// <summary>
/// Summary description for N_CreateSO
/// </summary>
public class N_CreateSO
{
	public N_CreateSO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region CreateSO

    /// <summary>
    /// 送DNN資料到SAP
    /// </summary>
    /// <param name="connType">数据库类型</param>
    /// <param name="connStr">数据库连接</param>
    /// <param name="SapFlag">SAP正式還是測試</param>
    /// <param name="POID">POID</param>
    /// <param name="DBName">DBName</param>
    public string WebCreateSO(string connType, string connStr, string SapFlag, string POID, string DBName)
    {
        string ermes = "N";
        //ASHOST=10.134.92.27 SYSNR=00 CLIENT=802 USER=FIHBJKFC PASSWD=FOXCONN8 LANG=en
        //ASHOST=10.134.28.98 SYSNR=00 CLIENT=802 USER=RFCSHARE02 PASSWD=it0215 LANG=en
        #region SAPDataReader
        //打開連接
        string connStr1 = "";
        if (SapFlag.ToUpper() == "TEST")
        {
            connStr1 = "ASHOST=10.134.92.27 SYSNR=00 CLIENT=802 USER=FIHBJKFC PASSWD=FOXCONN8 LANG=en";
        }
        else
        {
            connStr1 = "ASHOST=10.134.28.98 SYSNR=00 CLIENT=802 USER=RFCSHARE02 PASSWD=it0215 LANG=en";
        }
        string[] arrConn = connStr1.Split(' ');
        RfcConfigParameters rfcPar = new RfcConfigParameters();
        rfcPar.Clear();
        rfcPar.Add(RfcConfigParameters.Name, "ZRFC_SD_NOKIA_0001");
        rfcPar.Add(RfcConfigParameters.AppServerHost, arrConn[0].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.SystemNumber, arrConn[1].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.Client, arrConn[2].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.User, arrConn[3].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.Password, arrConn[4].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.Language, arrConn[5].Split('=')[1]);
        RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfcPar);
        RfcRepository rfcRep = rfcDest.Repository;
        IRfcFunction rfcFun = rfcRep.CreateFunction("ZRFC_SD_NOKIA_0001");

        //輸入參數1：Location_code
        //輸入Structure1:ORDER_HEADER_IN
        IRfcStructure poheader = rfcFun.GetStructure("ORDER_HEADER_IN");
        //IRfcTable poheader = rfcFun.GetTable("ORDER_HEADER_IN");
        //輸入Structure2:LOGIC_SWITCH
        IRfcStructure pologic = rfcFun.GetStructure("LOGIC_SWITCH");
        //IRfcTable pologic = rfcFun.GetTable("LOGIC_SWITCH");
        //輸入Table3:ORDER_ITEMS_IN
        IRfcTable poitem = rfcFun.GetTable("ORDER_ITEMS_IN");
        //輸入Table4:ORDER_PARTNERS
        IRfcTable popartner = rfcFun.GetTable("ORDER_PARTNERS");
        //輸入Table5:ORDER_CONDITIONS_IN
        IRfcTable poconditon = rfcFun.GetTable("ORDER_CONDITIONS_IN");
        //輸入Table6:ORDER_SCHEDULES_IN
        IRfcTable poschedule = rfcFun.GetTable("ORDER_SCHEDULES_IN");
        //構造輸入Table1:ORDER_HEADER_IN
        string strhd = "  select distinct POID,ITEMID,PO_CREATE_DT_UF2 Location_Code,"+
                       " PrePaymentBlock+CreditBlock+LaunchBlock Block_Flg,INTERNALID,BASEQTY,substring(DELIVERYSTARTDT,1,10) DELIVERYSTARTDT, " +
                       " case when Unit='Piece' then 'ST' else Unit end Unit" +
                       " FROM " + DBName + ".[dbo].[PO_CREATE_DT] where POID='" + POID + "'" +
                       " order by POID,ITEMID";
        DataTable dthd = PDataBaseOperation.PSelectSQLDT(connType, connStr, strhd);
        if (dthd.Rows.Count > 0)
        {
            
            rfcFun.SetValue("Location_code", dthd.Rows[0]["Location_Code"].ToString());
            poheader.SetValue("PURCH_NO_C", POID);

            //構造輸入Table2:LOGIC_SWITCH
            pologic.SetValue("PRICING", "B");
            //構造輸入Table3:ORDER_ITEMS_IN
            for (int i = 0; i < dthd.Rows.Count; i++)
            {
                //構造輸入Table3:ORDER_ITEMS_IN
                poitem.Insert();
                poitem.CurrentRow.SetValue("PO_ITM_NO", dthd.Rows[i]["ITEMID"].ToString());
                poitem.CurrentRow.SetValue("MATERIAL", dthd.Rows[i]["INTERNALID"].ToString());
                poitem.CurrentRow.SetValue("CUST_MAT35", dthd.Rows[i]["INTERNALID"].ToString());
                poitem.CurrentRow.SetValue("TARGET_QTY", dthd.Rows[i]["BASEQTY"].ToString());
                //poitem.CurrentRow.SetValue("TARGET_QU", dthd.Rows[i]["Unit"].ToString());
                if (dthd.Rows[0]["Block_Flg"].ToString().Trim().ToLower().Contains("true"))
                {
                    poitem.CurrentRow.SetValue("DLV_BLOCK", "01");
                }
                //構造輸入Table6:ORDER_SCHEDULES_IN
                poschedule.Insert();
                poschedule.CurrentRow.SetValue("ITM_NUMBER", dthd.Rows[i]["ITEMID"].ToString());
                poschedule.CurrentRow.SetValue("SCHED_LINE", "0001");
                poschedule.CurrentRow.SetValue("REQ_DATE", dthd.Rows[i]["DELIVERYSTARTDT"].ToString());
                poschedule.CurrentRow.SetValue("REQ_QTY", dthd.Rows[i]["BASEQTY"].ToString());
                if (dthd.Rows[0]["Block_Flg"].ToString().Trim().ToLower().Contains("true"))
                {
                    poschedule.CurrentRow.SetValue("REQ_DLV_BL", "01");
                }
            }
        }
        rfcFun.Invoke(rfcDest);
        string SO = rfcFun.GetValue("SALESDOCUMENT").ToString();
        IRfcTable RETURN1 = rfcFun.GetTable("NOKI_ITEM_OUT");
        DataTable SAPOUT_MSGV1 = GetDataTableFromRFCTable(RETURN1);
        IRfcTable RETURN2 = rfcFun.GetTable("RETURN");
        DataTable SAPOUT_MSGV2 = GetDataTableFromRFCTable(RETURN2);

        if (SO.Trim() != "")
        {
            string updhd = "update " + DBName + ".[dbo].[PO_CREATE_MT] set SAP_Log='" + SO + "' where POID='" + POID + "'";
            int idet = PDataBaseOperation.PExecSQL(connType, connStr, updhd);
            if(SAPOUT_MSGV1.Rows.Count>0)
            {
                for(int i=0;i<SAPOUT_MSGV1.Rows.Count;i++)
                {
                    string upddt = "update " + DBName + ".[dbo].[PO_CREATE_DT] set SOID='" + SAPOUT_MSGV1.Rows[i]["SO_NO"].ToString() + "'," +
                        "SOITEM='" + SAPOUT_MSGV1.Rows[i]["SO_ITM_NO"].ToString() + "'" +//,F7='2'
                        " where POID='" + SAPOUT_MSGV1.Rows[i]["PURCH_NO_C"].ToString() + "' and ITEMID='" + SAPOUT_MSGV1.Rows[i]["PO_ITM_NO"].ToString() + "'";
                    int idet1 = PDataBaseOperation.PExecSQL(connType, connStr, upddt);
                    ermes = "Y";
                }
            }
        }
        else
        {
            if (SAPOUT_MSGV2.Rows.Count > 0)
            {
                string mes = "";
                for (int ii = 0; ii < SAPOUT_MSGV2.Rows.Count; ii++)
                {
                    if (SAPOUT_MSGV2.Rows[ii]["TYPE"].ToString().ToUpper()=="E")
                    {
                        mes = mes + "{"+SAPOUT_MSGV2.Rows[ii]["MESSAGE"].ToString()+"}";
                    }
                    
                }
                string updhdw = "update " + DBName + ".[dbo].[PO_CREATE_MT] set SAP_Log='" + mes + "' where POID='" + POID + "'";
                int idetw = PDataBaseOperation.PExecSQL(connType, connStr, updhdw);
                ermes = "N";
            }
        }

        #endregion
        return ermes;
    }

    #region GetDataTableFromRFCTable
    public DataTable GetDataTableFromRFCTable(IRfcTable myrfcTable)
    {
        DataTable loTable = new DataTable();
        int liElement = 0;
        for (liElement = 0; liElement <= myrfcTable.ElementCount - 1; liElement++)
        {
            RfcElementMetadata metadata = myrfcTable.GetElementMetadata(liElement);
            loTable.Columns.Add(metadata.Name);
        }
        foreach (IRfcStructure Row in myrfcTable)
        {
            DataRow ldr = loTable.NewRow();
            for (liElement = 0; liElement <= myrfcTable.ElementCount - 1; liElement++)
            {
                RfcElementMetadata metadata = myrfcTable.GetElementMetadata(liElement);
                ldr[metadata.Name] = Row.GetString(metadata.Name);
            }
            loTable.Rows.Add(ldr);
        }
        return loTable;
    }
    #endregion
    #endregion
}