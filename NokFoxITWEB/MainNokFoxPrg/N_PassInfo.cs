using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAP.Middleware.Connector;

/// <summary>
/// Summary description for N_PassInfo
/// </summary>
public class N_PassInfo
{
	public N_PassInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region PassInfo

    /// <summary>
    /// 送PO資料到SAP
    /// </summary>
    /// <param name="connType">数据库类型</param>
    /// <param name="connStr">数据库连接</param>
    /// <param name="SapFlag">SAP正式還是測試</param>
    /// <param name="DnnID">POID</param>
    /// <param name="DBName">DBName</param>
    public string WebPassPOInfo(string connType, string connStr, string SapFlag, string TempPOID,string DBName)
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
        rfcPar.Add(RfcConfigParameters.Name, "ZRFC_HMD_DNN_INST_0001");
        rfcPar.Add(RfcConfigParameters.AppServerHost, arrConn[0].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.SystemNumber, arrConn[1].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.Client, arrConn[2].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.User, arrConn[3].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.Password, arrConn[4].Split('=')[1]);
        rfcPar.Add(RfcConfigParameters.Language, arrConn[5].Split('=')[1]);
        RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfcPar);
        RfcRepository rfcRep = rfcDest.Repository;
        IRfcFunction rfcFun = rfcRep.CreateFunction("ZRFC_HMD_DNN_INST_0001");
        //輸入Table1:DNN_HEAD_IN
        IRfcTable dthead = rfcFun.GetTable("DNN_HEAD_IN");
        //輸入Table2:DNN_ITEM_IN
        IRfcTable dtitem = rfcFun.GetTable("DNN_ITEM_IN");
        //構造輸入Table1:DNN_HEAD_IN
        string strhd = "  select distinct so.Address_UF1 POID,so.NumberDefaultIndicator Sold_Code,so.Address_UF2 AG_NAME,"+
                       " so.GivenName AG_STREET1,so.StreetName AG_STREET2,so.CareOfName AG_STREET3,"+
                         " so.CityName AG_CITY,so.CountryCode AG_LANDX,sp.NumberDefaultIndicator Ship_Code,sp.CountryCode WE_LAND1,"+
                         " pomt.PO_Create_MT_UF7 WE_NAME,sp.GivenName WE_STREET1,sp.StreetName WE_STREET2,sp.CareOfName WE_STREET3,"+
                         " sp.CityName WE_CITY,sp.CountryCode WE_LANDX,podt.ShipmentMode SHIP_Mode,podt.OriginalSOID TNS_SO,podt.FWCODE HMD_LSP,"+
                         " substring(podt.PO_Create_DT_UF1,1,10) Delivery_Date,substring(podt.DeliveryStartDT,1,10) Ex_factory_date,podt.Orincoterms INCO1,podt.Orincoterms2 INCO2" +
                         " FROM " + DBName + ".[dbo].[POSoldToAddress] so," + DBName + ".[dbo].[POShipToAddress] sp," +
                         " " + DBName + ".[dbo].[PO_CREATE_MT] pomt," + DBName + ".[dbo].[PO_CREATE_DT] podt" +
                         " where so.ID=sp.ID and so.ID=pomt.ID and so.ID=podt.ID and sp.ID=pomt.ID " +
                         " and sp.ID=podt.ID and pomt.ID=podt.ID and podt.POID='" + TempPOID + "'";
        DataTable dthd = PDataBaseOperation.PSelectSQLDT(connType, connStr, strhd);
        if (dthd.Rows.Count > 0)
        {
            for (int i = 0; i < dthd.Rows.Count; i++)
            {
                string POID = dthd.Rows[i]["POID"].ToString().Trim();
                string Sold_Code = dthd.Rows[i]["Sold_Code"].ToString().Trim();
                string AG_NAME = dthd.Rows[i]["AG_NAME"].ToString().Trim();
                string AG_STREET = dthd.Rows[i]["AG_STREET1"] + " " + dthd.Rows[i]["AG_STREET2"] + " " + dthd.Rows[i]["AG_STREET3"];
                string AG_CITY = dthd.Rows[i]["AG_CITY"].ToString().Trim();
                string AG_LANDX = dthd.Rows[i]["AG_LANDX"].ToString().Trim();
                string Ship_Code = dthd.Rows[i]["Ship_Code"].ToString().Trim();
                string WE_LAND1 = dthd.Rows[i]["WE_LAND1"].ToString().Trim();
                string WE_NAME = dthd.Rows[i]["WE_NAME"].ToString().Trim();
                string WE_STREET = dthd.Rows[i]["WE_STREET1"] + " " + dthd.Rows[i]["WE_STREET2"] + " " + dthd.Rows[i]["WE_STREET3"];
                string WE_CITY = dthd.Rows[i]["WE_CITY"].ToString().Trim();
                string WE_LANDX = dthd.Rows[i]["WE_LANDX"].ToString().Trim();
                string SHIP_Mode = dthd.Rows[i]["SHIP_Mode"].ToString().Trim();
                string TNS_SO = dthd.Rows[i]["TNS_SO"].ToString().Trim();
                string HMD_LSP = dthd.Rows[i]["HMD_LSP"].ToString().Trim();
                string Delivery_Date = dthd.Rows[i]["Delivery_Date"].ToString().Trim();
                string Ex_factory_date = dthd.Rows[i]["Ex_factory_date"].ToString().Trim();
                string INCO1 = dthd.Rows[i]["INCO1"].ToString().Trim();
                string INCO2 = dthd.Rows[i]["INCO2"].ToString().Trim();
                dthead.Insert();
                dthead.CurrentRow.SetValue("POID", POID);
                dthead.CurrentRow.SetValue("Sold_Code", Sold_Code);
                dthead.CurrentRow.SetValue("AG_NAME", AG_NAME);
                dthead.CurrentRow.SetValue("AG_STREET", AG_STREET);
                dthead.CurrentRow.SetValue("AG_CITY", AG_CITY);
                dthead.CurrentRow.SetValue("AG_LANDX", AG_LANDX);
                dthead.CurrentRow.SetValue("Ship_Code", Ship_Code);
                dthead.CurrentRow.SetValue("WE_LAND1", WE_LAND1);
                dthead.CurrentRow.SetValue("WE_NAME", WE_NAME);
                dthead.CurrentRow.SetValue("WE_STREET", WE_STREET);
                dthead.CurrentRow.SetValue("WE_CITY", WE_CITY);
                dthead.CurrentRow.SetValue("WE_LANDX", WE_LANDX);
                dthead.CurrentRow.SetValue("SHIP_Mode", SHIP_Mode);
                dthead.CurrentRow.SetValue("TNS_SO", TNS_SO);
                dthead.CurrentRow.SetValue("HMD_LSP", HMD_LSP);
                dthead.CurrentRow.SetValue("Delivery_Date", Delivery_Date);
                dthead.CurrentRow.SetValue("Ex_factory_date", Ex_factory_date);
                dthead.CurrentRow.SetValue("INCO1", INCO1);
                dthead.CurrentRow.SetValue("INCO2", INCO2);
            }
            //構造輸入Table2:DNN_ITEM_IN
            string strdt = " select POID,ItemID POITEM,InternalID KDMAT,Description ARKTX,BaseQty ORD_QTY,"+
                           " (select ContextText FROM " + DBName + ".[dbo].[POText] where POID=dt.POID and TypeCode='10014') UF1" +
                           " FROM " + DBName + ".[dbo].[PO_CREATE_DT] dt" +
                           " where POID='" + TempPOID + "'";
            DataTable dtdt = PDataBaseOperation.PSelectSQLDT(connType, connStr, strdt);
            if (dtdt.Rows.Count > 0)
            {
                for (int ii = 0; ii < dtdt.Rows.Count; ii++)
                {
                    string POID = dtdt.Rows[ii]["POID"].ToString().Trim();
                    string POITEM = dtdt.Rows[ii]["POITEM"].ToString().Trim();
                    string KDMAT = dtdt.Rows[ii]["KDMAT"].ToString().Trim();
                    string ARKTX = dtdt.Rows[ii]["ARKTX"].ToString().Trim();
                    string ORD_QTY = dtdt.Rows[ii]["ORD_QTY"].ToString().Trim();
                    string UF1 = dtdt.Rows[ii]["UF1"].ToString().Trim();
                    dtitem.Insert();
                    dtitem.CurrentRow.SetValue("POID", POID);
                    dtitem.CurrentRow.SetValue("POITEM", POITEM);
                    dtitem.CurrentRow.SetValue("KDMAT", KDMAT);
                    dtitem.CurrentRow.SetValue("ARKTX", ARKTX);
                    dtitem.CurrentRow.SetValue("ORD_QTY", ORD_QTY);
                    dtitem.CurrentRow.SetValue("Delivy_QTY", ORD_QTY);
                    dtitem.CurrentRow.SetValue("UF1", UF1);
                }
            }
        }
        rfcFun.Invoke(rfcDest);
        IRfcTable RETURN = rfcFun.GetTable("RETURN");
        DataTable SAPOUT_MSGV = GetDataTableFromRFCTable(RETURN);
        if(SAPOUT_MSGV.Rows.Count>0)
        {
            for (int ii = 0; ii < SAPOUT_MSGV.Rows.Count; ii++)
            {
                if (SAPOUT_MSGV.Rows[ii]["TYPE"].ToString().ToUpper() == "E")
                {
                    ermes = "N";
                    break;
                }
                else
                {
                    ermes = "Y";
                }
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