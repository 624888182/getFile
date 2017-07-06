using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using EconomyUser;
using System.IO;


public partial class MainMSPrg_IMSCMPODetailNew : System.Web.UI.Page
{
    #region   
    public string connstr = "";
    public static string MSSCMDIR;     
    public string POID = "";
    public string Idnum = "";
    public string Oreder_QTY = "";
    public string ShipToAdress = "";
    public string PO_Price = "";
    public string ProCode = "";
    public string DBType = "SQL";
    public string itemid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        POID = Request.QueryString["poid"].ToString();
        itemid = Request.QueryString["itemid"].ToString();
        Idnum = Request.QueryString["ID"].ToString();
        Oreder_QTY = Request.QueryString["Oreder_QTY"].ToString();
        PO_Price = Request.QueryString["PO_Price"].ToString();
        ShipToAdress = Request.QueryString["ShipToAdress"].ToString();
        connstr = Session["Param3"].ToString();
        MSSCMDIR = Session["Param7"].ToString();
        selecttable(POID, Idnum, itemid);
    }
    public void selecttable(string poid,string id,string itemid)
    {
        string strSQL = "select mt.ID,mt.SENDERID as  MSFT_DUNS,dt.POID as MSFT_PO,dt.ItemID AS MSFT_PO_ITEM,dt.InternalID AS MSFT_PN,MT.CREATIONDT,dt.Description," +
            "dt.DeliveryStartDT as Require_Delivery_Date," + "mt.PO_Create_MT_UF5 as Incoterm," +          
            //"DT.IncoTermsCode AS Payment_Term,"+            
            "mt.PO_Create_MT_UF4,mt.PO_Create_MT_UF6,mt.PO_Create_MT_UF7,dt.PO_Create_DT_UF1," +
            "mt.PO_Create_MT_UF2,DT.POCNT,mt.PO_Create_MT_UF3,mt.id,dt.PO_Create_DT_UF2" +
            " from " + MSSCMDIR + ".[dbo].[PO_CREATE_DT] dt," + MSSCMDIR + ".[dbo].[PO_CREATE_MT] mt" +
            " where mt.id=dt.id and mt.id='" + id + "' and mt.poid ='" + poid + "' and DT.itemid='" + itemid + "'";        
        DataTable dt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, strSQL);
        if (dt.Rows.Count > 0)
        {
            Label2.Text = dt.Rows[0]["ID"].ToString();
            Label4.Text = dt.Rows[0]["MSFT_PO_ITEM"].ToString();
            Label6.Text = dt.Rows[0]["MSFT_DUNS"].ToString();
            Label8.Text = dt.Rows[0]["MSFT_PO"].ToString();
            Label10.Text = dt.Rows[0]["MSFT_PN"].ToString();
            Label12.Text = dt.Rows[0]["CREATIONDT"].ToString();
            Label14.Text = dt.Rows[0]["Description"].ToString();
            Label16.Text = dt.Rows[0]["POCNT"].ToString();
            Label18.Text = PO_Price;
            Label20.Text = Oreder_QTY;
            Label22.Text = dt.Rows[0]["Require_Delivery_Date"].ToString();
            Label24.Text = ShipToAdress;
            //Label26.Text = dt.Rows[0]["Payment_Term"].ToString();
            Label28.Text = dt.Rows[0]["Incoterm"].ToString();
            Label30.Text = dt.Rows[0]["PO_Create_MT_UF4"].ToString();
            Label32.Text = dt.Rows[0]["PO_Create_MT_UF6"].ToString();
            Label34.Text = dt.Rows[0]["PO_Create_MT_UF7"].ToString();
            Label36.Text = dt.Rows[0]["PO_Create_DT_UF1"].ToString();
            Label38.Text = dt.Rows[0]["PO_Create_MT_UF2"].ToString();
            Label40.Text = dt.Rows[0]["PO_Create_DT_UF2"].ToString();
            Label26.Text = dt.Rows[0]["PO_Create_MT_UF3"].ToString();         
        }
    }
    #endregion
}