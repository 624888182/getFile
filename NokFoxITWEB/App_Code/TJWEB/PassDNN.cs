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
/// Summary description for PassDNN
/// </summary>
public class PassDNN
{
	public PassDNN()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region PassDNN

    /// <summary>
    /// 送DNN資料到SAP
    /// </summary>
    /// <param name="connType">数据库类型</param>
    /// <param name="connStr">数据库连接</param>
    /// <param name="SapFlag">SAP正式還是測試</param>
    /// <param name="DnnID">DNNID</param>
    public void PassDNN1(string connType, string connStr, string SapFlag,string DnnID)
    {
        //ASHOST=10.134.92.27 SYSNR=00 CLIENT=802 USER=FIHBJKFC PASSWD=FOXCONN8 LANG=en
        //ASHOST=10.134.28.98 SYSNR=00 CLIENT=802 USER=RFCSHARE02 PASSWD=it0215 LANG=en
        #region SAPDataReader
        //打開連接
        string connStr1 = "";
        if(SapFlag.ToUpper ()=="TEST")
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
        string strhd = "  select distinct dt.DNID DNNID,dt.POID,sp.company COMPANY_NAME1,'' COMPANY_NAME2,sp.GivenName ADDREES1,"+
                       "sp.StreetName ADDREES2,sp.CareOfName ADDREES3,sp.PostalCode POST_CODE1,sp.CityName CITY1,"+
                       "sp.CountryCode Country,sp.RegionCode Region,sp.FAXnumber Fax,'' E_mail,mt.ShipmentModeCode ship_Mode,"+
                       "dt.POID HMDPO,dt.CUDNN HMDDN,dt.CUORDER HMDSO,mt.FWCODE Forward_NAME1,'' Forward_NAME2,"+
                       "'' Forward_NAME3,mt.ArrivalDT Delivery_Date,mt.IssueDT Ex_factory_date,mt.IncotermsCode INCO1,"+
                       "'' INCO2,'' UF1,'' UF2,'' UF3,'' UF4"+
                       " from [IMSCM].[dbo].[DNN_MT] mt,[IMSCM].[dbo].[DNNITEM] dt,[IMSCM].[dbo].[DNNShipToAddress] sp"+
                       " where mt.DNID=dt.DNID and mt.DNID=sp.DNID and dt.DNID=sp.DNID and mt.DNID='" + DnnID + "'" +
                       " order by DNNID,POID";
        DataTable dthd = PDataBaseOperation.PSelectSQLDT(connType, connStr, strhd);
        if(dthd.Rows.Count >0)
        {
            for (int i = 0; i < dthd.Rows.Count; i++)
            {
                string DNNID = dthd.Rows[i]["DNNID"].ToString().Trim();
                string POID = dthd.Rows[i]["POID"].ToString().Trim();
                string COMPANY_NAME1 = dthd.Rows[i]["COMPANY_NAME1"].ToString().Trim();
                string ADDREES1 = dthd.Rows[i]["ADDREES1"].ToString().Trim();
                string ADDREES2 = dthd.Rows[i]["ADDREES2"].ToString().Trim();
                string ADDREES3 = dthd.Rows[i]["ADDREES3"].ToString().Trim();
                string POST_CODE1 = dthd.Rows[i]["POST_CODE1"].ToString().Trim();
                string CITY1 = dthd.Rows[i]["CITY1"].ToString().Trim();
                string Country = dthd.Rows[i]["Country"].ToString().Trim();
                string Region = dthd.Rows[i]["Region"].ToString().Trim();
                string Fax = dthd.Rows[i]["Fax"].ToString().Trim();
                string ship_Mode = dthd.Rows[i]["ship_Mode"].ToString().Trim();
                string HMDPO = dthd.Rows[i]["HMDPO"].ToString().Trim();
                string HMDDN = dthd.Rows[i]["HMDDN"].ToString().Trim();
                string HMDSO = dthd.Rows[i]["HMDSO"].ToString().Trim();
                string Forward_NAME1 = dthd.Rows[i]["Forward_NAME1"].ToString().Trim();
                string Delivery_Date = dthd.Rows[i]["Delivery_Date"].ToString().Trim();
                string Ex_factory_date = dthd.Rows[i]["Ex_factory_date"].ToString().Trim();
                string INCO1 = dthd.Rows[i]["INCO1"].ToString().Trim();
                dthead.Insert();
                dthead.CurrentRow.SetValue("DNNID", DNNID);
                dthead.CurrentRow.SetValue("POID", POID);
                dthead.CurrentRow.SetValue("COMPANY_NAME1", COMPANY_NAME1);
                dthead.CurrentRow.SetValue("ADDREES1", ADDREES1);
                dthead.CurrentRow.SetValue("ADDREES2", ADDREES2);
                dthead.CurrentRow.SetValue("ADDREES3", ADDREES3);
                dthead.CurrentRow.SetValue("POST_CODE1", POST_CODE1);
                dthead.CurrentRow.SetValue("CITY1", CITY1);
                dthead.CurrentRow.SetValue("Country", Country);
                dthead.CurrentRow.SetValue("Region", Region);
                dthead.CurrentRow.SetValue("Fax", Fax);
                dthead.CurrentRow.SetValue("ship_Mode", ship_Mode);
                dthead.CurrentRow.SetValue("HMDPO", HMDPO);
                dthead.CurrentRow.SetValue("HMDDN", HMDDN);
                dthead.CurrentRow.SetValue("HMDSO", HMDSO);
                dthead.CurrentRow.SetValue("Forward_NAME1", Forward_NAME1);
                dthead.CurrentRow.SetValue("Delivery_Date", Delivery_Date);
                dthead.CurrentRow.SetValue("Ex_factory_date", Ex_factory_date);
                dthead.CurrentRow.SetValue("INCO1", INCO1);
            }
            //構造輸入Table2:DNN_ITEM_IN
            string strdt = " select DNID DNNID,ItemID DNN_ITEM,POID,POItemID POITEM,ProductRecipientID KDMAT,Total_QTY ORD_QTY" +
                           " FROM [IMSCM].[dbo].[DNNITEM] where DNID='" + DnnID + "'" +
                           " order by DNNID,POID";
            DataTable dtdt = PDataBaseOperation.PSelectSQLDT(connType, connStr, strdt);
            if (dtdt.Rows.Count > 0)
            {
                for(int ii=0;ii<dtdt.Rows.Count;ii++)
                {
                    string DNNID = dtdt.Rows[ii]["DNNID"].ToString().Trim();
                    string DNN_ITEM = dtdt.Rows[ii]["DNN_ITEM"].ToString().Trim();
                    string POID = dtdt.Rows[ii]["POID"].ToString().Trim();
                    string POITEM = dtdt.Rows[ii]["POITEM"].ToString().Trim();
                    string KDMAT = dtdt.Rows[ii]["KDMAT"].ToString().Trim();
                    string ORD_QTY = dtdt.Rows[ii]["ORD_QTY"].ToString().Trim();
                    dtitem.Insert();
                    dtitem.CurrentRow.SetValue("DNNID", DNNID);
                    dtitem.CurrentRow.SetValue("DNN_ITEM", DNN_ITEM);
                    dtitem.CurrentRow.SetValue("POID", POID);
                    dtitem.CurrentRow.SetValue("POITEM", POITEM);
                    dtitem.CurrentRow.SetValue("KDMAT", KDMAT);
                    dtitem.CurrentRow.SetValue("ORD_QTY", ORD_QTY);
                }
            }
        }        
        rfcFun.Invoke(rfcDest);
        IRfcTable RETURN = rfcFun.GetTable("RETURN");
        DataTable SAPOUT_MSGV = GetDataTableFromRFCTable(RETURN);

        #endregion

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