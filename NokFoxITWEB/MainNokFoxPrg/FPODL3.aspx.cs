using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMSPrg_FPODL3 : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string BegTime;
    private static string EndTime;
    private static string tparam;
    private static DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 數據庫連接
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                //tparam = Session["tparam"].ToString();
                conntype = Session["Param2"].ToString();
                Conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
            }
            else if (tmp1 == "")
            {
                conntype = "sql";
                Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                Conn = "Server=10.83.18.93 ;User id=IMSCM;Pwd=Foxconn88;Database=IMSCM";//205
                dbWrite = "Server=10.83.18.93 ;User id=IMSCM;Pwd=Foxconn88;Database=IMSCM";//205
                BBSCMDIR = "IMSCM";
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            #endregion

            dbo = new PDataBaseOperation(conntype, Conn);
            BegTime = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            EndTime = DateTime.Now.ToString("yyyy/MM/dd");
            txtBeginDate.Text = BegTime;
            txtendDate.Text = EndTime;
            GridViewBind("", "", BegTime.Replace("/", ""), EndTime.Replace("/", ""));
            DataSet dts = (DataSet)Session["ds"];
            Tmp_Download(dts);
        }
        //GridViewBind1();
        //GridViewBind();
        //divGridView1.Visible = false;       
    }

    /// <summary>
    /// GridView數據來源
    /// </summary>
    /// <param name="strPOID">開始POID</param>
    /// <param name="strPOID1">結束POID</param>
    /// <param name="strBegDate">開始時間</param>
    /// <param name="strEndDate">結束時間</param>
    protected void GridViewBind(string strPOID, string strPOID1, string strBegDate, string strEndDate)
    {

        string t1, t2, t3, t4 = "";
        #region select
        //3A4
        t1 = " select distinct " +
            "dt.POID as 'MSFT PO',dt.ItemID AS 'MSFT PO ITEM',dt.BaseQty as 'Balance Qty',dt.InternalID AS 'MSFT P/N',MT.CREATIONDT,dt.Description," +
            "'' 'PO Price Base Unit',dt.CostAmount,dt.CostCurrencyCode,'' 'Oreder QTY',dt.BaseQty,dt.Unit," +
            "dt.DeliveryStartDT as 'Pick Up Date'," +
            "'' ShipToAdress,ship.givenname,ship.streetname,ship.cityname,ship.regioncode,ship.postalcode,ship.countrycode," +
            "dt.PO_Create_DT_UF1 AS 'Request Delivery Date'," +
            "dt.PO_Create_DT_UF2 AS 'Storage Location'" +
            ",'' as W0502,'' as B204 " +

            " from " + BBSCMDIR + ".[dbo].[PO_CREATE_DT] dt," + BBSCMDIR + ".[dbo].[PO_CREATE_MT] mt," + BBSCMDIR +
            ".[dbo].[POSHIPTOADDRESS] ship" +
            " where mt.id=dt.id and mt.id = ship.id" +
            //" and mt.poid='0022404938' " +
            " and ( mt.CONFIRMFLAG !='D' or mt.confirmflag is null or mt.confirmflag = '')";


        //940--dh.orderedqty
        t2 = " select distinct  dd.W0502 AS 'dnid',dh.deliverRefer as'poid',dd.Selleritemno as 'itemid',dd.buyeritemno as 'poitemid',dh.state as 'Booking request Status'," +
           " dh.ackflag as 'Booking request ACK',dd.qty as 'Delivery Note Qty'," +
           " dh.shippingdate as 'Delivery date'" +
           " from IMSCM.[dbo].[DN_Header] dh,IMSCM.[dbo].[DN_Detail] dd" +
           " where 1=1   and dh.w0502=dd.W0502 and (dh.Create_flag !='D' or dh.Create_flag is null)";

        //
        //204
        //t3 = " select DISTINCT EI.OID01,EI.OID02,EH.B204,EI.OID01,EH.[L1101] AS LoadID,EH.B202 AS CarrierID,EH.MS304 AS 'Transportation Method'" +
        //    "  FROM " + BBSCMDIR + ".[dbo].[EDI204HEADER] EH," + BBSCMDIR + ".[dbo].[EDI204OIDLAD] EI," + BBSCMDIR + ".[dbo].[EDI204L11] EL" +
        //    "  WHERE 1=1 " +
        //    //"  AND EI.OID02='0022404938'" +
        //    "  and EH.B204=EI.B204 AND EH.B204=EL.B204 ";

        //t3 = "  select distinct EI.B204, EI.OID01,EI.OID02,eh1.* from  " + BBSCMDIR + ".[dbo].[EDI204OIDLAD] EI," +
        //    "  ( select EH.B204,EH.[L1101] AS LoadID,EH.B202 AS CarrierID," +
        //    "   EH.MS304 AS 'Transportation Method'  from  " + BBSCMDIR + ".[dbo].[EDI204HEADER] eh" +
        //    "   where (eh.B204 + eh.insertdate in (   select B204 + Max(insertdate) " +
        //    "   from  " + BBSCMDIR + ".[dbo].[EDI204HEADER] group by B204) or ConfirmFlag='Y')) eh1" +
        //    "   where ei.B204=eh1.b204";
        t3 = " select distinct EI.B204, EI.OID01,EI.OID02,eh1.* from  IMSCM.[dbo].[EDI204OIDLAD] EI,  (" +
           " select * from (" +
           " select B204,[L1101] AS LoadID,B202 AS CarrierID,MS304 AS 'Transportation Method' ,confirmflag,rcount," +
           " (case when confirmflag='Y' then '2200-01-01 00:00:01' else insertdate end) as insertdate" +
           " from  IMSCM.[dbo].[EDI204HEADER]  " +
           " ) eh where 	eh.B204 + eh.insertdate in ( " +
           " select b204 + max (case when confirmflag='Y' then '2200-01-01 00:00:01' " +
           " else insertdate end) as insertdate" +
           " from IMSCM.[dbo].[EDI204HEADER]" +
           " a group by a.b204) ) eh1" +
           " where ei.B204=eh1.b204 and ei.RCOUNT=eh1.rcount " +
           " and (confirmflag !='D' or confirmflag is null)";


        //3B2
        t4 = " select " +
            " distinct mt.DNID,dnitem.POID,mt.WayBillID,mt.SendFlag as 'ASN Send','' as 'ASN Send ACK',mt.SendTime as 'Send3B2Time'" +
            " from " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] mt," + BBSCMDIR + ".[dbo].[Delivery_DNITEM] dnitem" +
            " where 1=1 " +
            //" and dnitem.poid='0022404938'" +
            " and mt.dnid=dnitem.dnid ";


        #endregion
        #region where
        if (strPOID != "" && strPOID1 != "")
        {
            t1 = t1 + " and mt.poid>='" + strPOID + "' and mt.poid<='" + strPOID1 + "'";
            t2 = t2 + " and dh.deliverRefer>='" + strPOID + "' and dh.deliverRefer<='" + strPOID1 + "'";
            t3 = t3 + " and EI.OID02>='" + strPOID + "' and EI.OID02<='" + strPOID1 + "'";
            t4 = t4 + " and mt.poid>='" + strPOID + "' and mt.poid<='" + strPOID1 + "'";
        }
        else if (strPOID != "" && strPOID1 == "")
        {
            t1 = t1 + " and mt.poid>='" + strPOID + "'";
            t2 = t2 + " and dh.deliverRefer>='" + strPOID + "'";
            t3 = t3 + " and EI.OID02>='" + strPOID + "'";
            t4 = t4 + " and mt.poid>='" + strPOID + "'";
        }
        else if (strPOID == "" && strPOID1 != "")
        {
            t1 = t1 + " and mt.poid<='" + strPOID + "'";
            t2 = t2 + " and dh.deliverRefer<='" + strPOID + "'";
            t3 = t3 + " and EI.OID02<='" + strPOID + "'";
            t4 = t4 + " and mt.poid<='" + strPOID + "'";
        }
        if (strBegDate != "")
        {
            t1 = t1 + " and creationDT >='" + strBegDate + " 00:00:00'";
        }
        if (strEndDate != "")
        {
            t1 = t1 + " and creationDT <='" + strEndDate + " 23:59:59'";
        }
        t1 = t1 + " order by creationdt desc,'MSFT PO' ASC ,'MSFT PO ITEM' ASC ";
        t2 = t2 + " order by DNID asc";
        t3 = t3 + " order by EI.OID01";
        t4 = t4 + " order by DNID asc";
        #endregion
        string str = t1 + t2 + t3 + t4;
        DataSet dts = dbo.PSelectSQLDS(str);
        #region 整合address and  PO Price Base Unit Oreder QTY
        try
        {
            if (dts.Tables.Count > 0)
            {
                if (dts.Tables[0].Rows.Count > 0)
                {
                    #region Transportation Method
                    if (dts.Tables.Count > 3 && dts.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dts.Tables[2].Rows.Count; i++)
                        {
                            #region Transportation Method
                            string ms304 = dts.Tables[2].Rows[i]["Transportation Method"].ToString().Trim();
                            switch (ms304)
                            {
                                case "A":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "By Air";
                                    break;
                                case "H":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Customer Pickup";
                                    break;
                                case "M":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "By Road";
                                    break;
                                case "R":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "By Rail";
                                    break;
                                case "S":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "By Sea";
                                    break;
                                case "U":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Private Parcel Service";
                                    break;
                                case "X":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Intermodal (Piggyback)";
                                    break;
                                case "LT":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Less Than Trailer Load (LTL)";
                                    break;
                                case "RC":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Rail, Less than Carload";
                                    break;
                                case "RR":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Roadrailer";
                                    break;
                                case "SE":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Sea/Air";
                                    break;
                                case "TA":
                                    dts.Tables[2].Rows[i]["Transportation Method"] = "Towaway Service";
                                    break;
                                default:
                                    dts.Tables[2].Rows[i]["Transportation Method"] = dts.Tables[2].Rows[i]["Transportation Method"].ToString().Trim();
                                    break;
                            }
                            #endregion
                        }
                    }
                    #endregion
                    #region address and  PO Price Base Unit Oreder QTY
                    for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                    {
                        string str1 = dts.Tables[0].Rows[i]["Pick Up Date"].ToString().Trim().Substring(0, 8);
                        string str2 = dts.Tables[0].Rows[i]["Request Delivery Date"].ToString().Trim().Substring(0, 8);
                        //string str3 = dts.Tables[0].Rows[i]["Delivery date"].ToString().Trim().Substring(0, 8);
                        string str4 = dts.Tables[0].Rows[i]["CREATIONDT"].ToString().Trim().Substring(0, 10);
                        dts.Tables[0].Rows[i]["Pick Up Date"] = str1;
                        dts.Tables[0].Rows[i]["Request Delivery Date"] = str2;
                        //dts.Tables[0].Rows[i]["Delivery date"] = str3;
                        dts.Tables[0].Rows[i]["CREATIONDT"] = str4;
                        string address = "";
                        //PO Price Base Unit
                        string CostAmount = dts.Tables[0].Rows[i]["CostAmount"].ToString().Trim();
                        string CostCurrencyCode = dts.Tables[0].Rows[i]["CostCurrencyCode"].ToString().Trim();
                        if (CostAmount != "" && CostCurrencyCode != "")
                        {
                            dts.Tables[0].Rows[i]["PO Price Base Unit"] = CostAmount + CostCurrencyCode;
                        }
                        //Oreder QTY
                        string BaseQty = dts.Tables[0].Rows[i]["BaseQty"].ToString().Trim();
                        string Unit = dts.Tables[0].Rows[i]["Unit"].ToString().Trim();
                        if (BaseQty != "" && Unit != "")
                        {
                            dts.Tables[0].Rows[i]["Oreder QTY"] = BaseQty + Unit;
                        }
                        //ShipToAdress    
                        string givenname = dts.Tables[0].Rows[i]["givenname"].ToString().Trim();
                        string streetname = dts.Tables[0].Rows[i]["streetname"].ToString().Trim();
                        string cityname = dts.Tables[0].Rows[i]["cityname"].ToString().Trim();
                        string regioncode = dts.Tables[0].Rows[i]["regioncode"].ToString().Trim();
                        string postalcode = dts.Tables[0].Rows[i]["postalcode"].ToString().Trim();
                        string countrycode = dts.Tables[0].Rows[i]["countrycode"].ToString().Trim();
                        if (givenname != "") address = address + givenname + ",";
                        if (streetname != "") address = address + streetname + ",";
                        if (cityname != "") address = address + cityname + ",";
                        if (regioncode != "") address = address + regioncode + ",";
                        if (postalcode != "") address = address + postalcode + ",";
                        if (countrycode != "") address = address + countrycode + " ";
                        dts.Tables[0].Rows[i]["ShipToAdress"] = address;
                    }

                    //dts.Tables[0].Columns.Remove("CostAmount");
                    //dts.Tables[0].Columns.Remove("CostCurrencyCode");
                    //dts.Tables[0].Columns.Remove("BaseQty");
                    //dts.Tables[0].Columns.Remove("Unit");
                    //dts.Tables[0].Columns.Remove("givenname");
                    //dts.Tables[0].Columns.Remove("streetname");
                    //dts.Tables[0].Columns.Remove("cityname");
                    //dts.Tables[0].Columns.Remove("regioncode");
                    //dts.Tables[0].Columns.Remove("postalcode");
                    //dts.Tables[0].Columns.Remove("countrycode");
                    #endregion
                }

            }
        }
        catch { }
        #endregion
        divGridView1.Visible = true;
        Session["ds"] = dts;
        GridView1.DataSource = dts;
        GridView1.DataBind();


    }


    protected void Tmp_Download(DataSet dts)
    {
        GridView2.DataSource = dts;
        GridView2.DataBind();
    }

    #region
    /// <summary>
    /// select
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strPOID = txtPOID.Text.ToString().Trim();
        string strPOID1 = txtPOID1.Text.ToString().Trim();
        string strBegDate = txtBeginDate.Text.ToString().Trim().Replace("/", "");
        string strEndDate = txtendDate.Text.ToString().Trim().Replace("/", "");
        GridViewBind(strPOID, strPOID1, strBegDate, strEndDate);
        DataSet dts = (DataSet)Session["ds"];
        Tmp_Download(dts);
    }
    /// <summary>
    /// reset
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        divGridView1.Visible = false;
        txtendDate.Text = "";
        txtBeginDate.Text = "";
        txtPOID.Text = "";
        txtPOID1.Text = "";
    }
    /// <summary>
    /// download Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        lblHeader.Visible = true;
        lblHeader.Text = "loading..";
        //ToExcel(GridView1, "aaa");
        ResponseStream();
        //lblHeader.Visible = false;
        //lblHeader.Text = "";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //得到text控件 
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引 
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮 
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出 
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= GridView1.PageCount ? GridView1.PageCount - 1 : newPageIndex;

        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["ds"];
        GridView1.DataBind();

    }

    protected double ToInt(string str)
    {
        try
        {
            return Convert.ToDouble(str);
        }
        catch
        {

            return 0;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region gridview數據行格式設置
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            #region 根據POID 對應940 204 3B2的DNID
            try
            {
                int row = e.Row.RowIndex;
                //得到POID
                string strPOID = DataBinder.Eval(e.Row.DataItem, "MSFT PO").ToString().Trim();
                string strPOItem = "0" + DataBinder.Eval(e.Row.DataItem, "MSFT PO ITEM").ToString().Trim();
                string strOrderQty = DataBinder.Eval(e.Row.DataItem, "BaseQty").ToString().Trim();
                DataSet dts = (DataSet)Session["ds"];
                #region 940
                DataRow[] dr1 = dts.Tables[1].Select("poid='" + strPOID + "'");
                if (dr1.Length > 0)
                {
                    for (int i = 0; i < dr1.Length; i++)
                    {
                        string aaaa = dr1[i]["poitemid"].ToString();
                        if (aaaa == strPOItem)
                        {
                            e.Row.Cells[13].Text += dr1[i]["DNID"].ToString() + "</br>";
                            e.Row.Cells[14].Text += dr1[i]["Delivery date"].ToString() + "</br>";
                            e.Row.Cells[15].Text += dr1[i]["Delivery Note Qty"].ToString() + "</br>";
                            e.Row.Cells[16].Text += dr1[i]["Booking request Status"].ToString() + "</br>";
                            e.Row.Cells[17].Text += dr1[i]["Booking request ACK"].ToString() + "</br>";

                            //------------------BalanceQty--------------------------//
                            if (dr1[i]["Booking request Status"].ToString() == "y" || dr1[i]["Booking request Status"].ToString() == "Y")
                            {
                                string a = strOrderQty;
                                string b = dr1[i]["Delivery Note Qty"].ToString();
                                e.Row.Cells[12].Text = (ToInt(strOrderQty) - ToInt(dr1[i]["Delivery Note Qty"].ToString())).ToString("0.000");
                                dts.Tables[0].Rows[row]["Balance Qty"] = (ToInt(strOrderQty) - ToInt(dr1[i]["Delivery Note Qty"].ToString())).ToString();
                                strOrderQty = (ToInt(strOrderQty) - ToInt(dr1[i]["Delivery Note Qty"].ToString())).ToString();
                            }
                        }
                    }
                }
                int iW0502, iOID01 = 0;
                //a = a.Replace("</br>", "-");
                string[] strW0502 = e.Row.Cells[13].Text.ToString().Trim().Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries);
                #endregion
                #region 204

                DataRow[] dr2 = dts.Tables[2].Select("OID02='" + strPOID + "'");
                if (dr2.Length > 0)
                {
                    int j = 0;
                    for (int i = 0; i < strW0502.Length; i++)
                    {
                        iW0502 = Convert.ToInt32(strW0502[i]);
                        bool b = true;
                        for (int k = 0; k < dr2.Length; k++)
                        {
                            try
                            {
                                iOID01 = Convert.ToInt32(dr2[k]["OID01"].ToString().Trim());
                            }
                            catch
                            {
                                iOID01 = 0;
                            }
                            if (iW0502 == iOID01)
                            {
                                e.Row.Cells[18].Text += dr2[k]["OID01"].ToString() + "</br>";
                                e.Row.Cells[19].Text += dr2[k]["LoadID"].ToString() + "</br>";
                                e.Row.Cells[20].Text += dr2[k]["CarrierID"].ToString() + "</br>";
                                e.Row.Cells[21].Text += dr2[k]["Transportation Method"].ToString() + "</br>";
                                b = false;
                            }
                            if (b && k == dr2.Length - 1)
                            {
                                e.Row.Cells[18].Text += "</br>";
                                e.Row.Cells[19].Text += "</br>";
                                e.Row.Cells[20].Text += "</br>";
                                e.Row.Cells[21].Text += "</br>";
                            }
                        }
                    }
                }
                #endregion
                #region 3B2
                DataRow[] dr3 = dts.Tables[3].Select("POID='" + strPOID + "'");
                if (dr3.Length > 0)
                {
                    int j = 0;
                    for (int i = 0; i < strW0502.Length; i++)
                    {
                        iW0502 = Convert.ToInt32(strW0502[i]);
                        bool b = false;
                        for (int k = 0; k < dr3.Length; k++)
                        {
                            try
                            {
                                iOID01 = Convert.ToInt32(dr3[k]["DNID"].ToString().Trim());
                            }
                            catch
                            {
                                iOID01 = 0;
                            }
                            if (iW0502 == iOID01)
                            {
                                e.Row.Cells[22].Text += dr3[k]["DNID"].ToString() + "</br>";
                                e.Row.Cells[23].Text += dr3[k]["ASN Send"].ToString() + "</br>";
                                //暫定與e.Row.Cells[19]一致
                                e.Row.Cells[24].Text += dr3[k]["ASN Send"].ToString() + "</br>";
                                //e.Row.Cells[20].Text += dr4[i]["ASN Send ACK"].ToString() + "</br>";                        
                                e.Row.Cells[25].Text += dr3[k]["SEND3B2Time"].ToString() + "</br>";
                                b = true;
                            }
                            if (b == false && k == dr3.Length - 1)
                            {
                                e.Row.Cells[22].Text += "</br>";
                                e.Row.Cells[23].Text += "</br>";
                                e.Row.Cells[24].Text += "</br>";
                                e.Row.Cells[25].Text += "</br>";
                            }
                        }
                    }
                }
                #endregion
            }
            catch { }
            #endregion

            #region gridview 格式設置
            //不自動換行
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            //選中行變色
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            #endregion
        }
        #endregion

        #region gridview表體格式設置
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
        #endregion
    }


    //20151204 BalanceQty
    protected void GridView11_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region gridview數據行格式設置
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            #region 根據POID 對應940 204 3B2的DNID
            try
            {
                int row = e.Row.RowIndex;
                //得到POID
                string strPOID = DataBinder.Eval(e.Row.DataItem, "MSFT PO").ToString().Trim();
                string strPOItem = "0" + DataBinder.Eval(e.Row.DataItem, "MSFT PO ITEM").ToString().Trim();
                string strOrderQty = DataBinder.Eval(e.Row.DataItem, "BaseQty").ToString().Trim();
                DataSet dts = (DataSet)Session["ds"];
                #region 940
                DataRow[] dr1 = dts.Tables[1].Select("poid='" + strPOID + "'");
                if (dr1.Length > 0)
                {
                    for (int i = 0; i < dr1.Length; i++)
                    {
                        string aaaa = dr1[i]["poitemid"].ToString();
                        if (aaaa == strPOItem)
                        {
                            e.Row.Cells[12].Text += dr1[i]["DNID"].ToString() + "</br>";
                            e.Row.Cells[13].Text += dr1[i]["Delivery date"].ToString() + "</br>";
                            e.Row.Cells[14].Text += dr1[i]["Delivery Note Qty"].ToString() + "</br>";
                            e.Row.Cells[15].Text += dr1[i]["Booking request Status"].ToString() + "</br>";
                            e.Row.Cells[16].Text += dr1[i]["Booking request ACK"].ToString() + "</br>";

                            //------------------BalanceQty--------------------------//
                            if (dr1[i]["Booking request Status"].ToString() == "y" || dr1[i]["Booking request Status"].ToString() == "Y")
                            {
                                string strBalanceQty = (ToInt(strOrderQty) - ToInt(dr1[i]["Delivery Note Qty"].ToString())).ToString();
                            }
                        }
                    }
                }
                int iW0502, iOID01 = 0;
                //a = a.Replace("</br>", "-");
                string[] strW0502 = e.Row.Cells[12].Text.ToString().Trim().Split(new string[] { "</br>" }, StringSplitOptions.RemoveEmptyEntries);
                #endregion
                #region 204

                DataRow[] dr2 = dts.Tables[2].Select("OID02='" + strPOID + "'");
                if (dr2.Length > 0)
                {
                    int j = 0;
                    for (int i = 0; i < strW0502.Length; i++)
                    {
                        iW0502 = Convert.ToInt32(strW0502[i]);
                        bool b = true;
                        for (int k = 0; k < dr2.Length; k++)
                        {
                            try
                            {
                                iOID01 = Convert.ToInt32(dr2[k]["OID01"].ToString().Trim());
                            }
                            catch
                            {
                                iOID01 = 0;
                            }
                            if (iW0502 == iOID01)
                            {
                                e.Row.Cells[17].Text += dr2[k]["OID01"].ToString() + "</br>";
                                e.Row.Cells[18].Text += dr2[k]["LoadID"].ToString() + "</br>";
                                e.Row.Cells[19].Text += dr2[k]["CarrierID"].ToString() + "</br>";
                                e.Row.Cells[20].Text += dr2[k]["Transportation Method"].ToString() + "</br>";
                                b = false;
                            }
                            if (b && k == dr2.Length - 1)
                            {
                                e.Row.Cells[17].Text += "</br>";
                                e.Row.Cells[18].Text += "</br>";
                                e.Row.Cells[19].Text += "</br>";
                                e.Row.Cells[20].Text += "</br>";
                            }
                        }
                    }
                }
                #endregion
                #region 3B2
                DataRow[] dr3 = dts.Tables[3].Select("POID='" + strPOID + "'");
                if (dr3.Length > 0)
                {
                    int j = 0;
                    for (int i = 0; i < strW0502.Length; i++)
                    {
                        iW0502 = Convert.ToInt32(strW0502[i]);
                        bool b = false;
                        for (int k = 0; k < dr3.Length; k++)
                        {
                            try
                            {
                                iOID01 = Convert.ToInt32(dr3[k]["DNID"].ToString().Trim());
                            }
                            catch
                            {
                                iOID01 = 0;
                            }
                            if (iW0502 == iOID01)
                            {
                                e.Row.Cells[21].Text += dr3[k]["DNID"].ToString() + "</br>";
                                e.Row.Cells[22].Text += dr3[k]["ASN Send"].ToString() + "</br>";
                                //暫定與e.Row.Cells[19]一致
                                e.Row.Cells[23].Text += dr3[k]["ASN Send"].ToString() + "</br>";
                                //e.Row.Cells[20].Text += dr4[i]["ASN Send ACK"].ToString() + "</br>";                        
                                e.Row.Cells[24].Text += dr3[k]["SEND3B2Time"].ToString() + "</br>";
                                b = true;
                            }
                            if (b == false && k == dr3.Length - 1)
                            {
                                e.Row.Cells[17].Text += "</br>";
                                e.Row.Cells[18].Text += "</br>";
                                e.Row.Cells[19].Text += "</br>";
                                e.Row.Cells[20].Text += "</br>";
                            }
                        }
                    }
                }
                #endregion
            }
            catch { }
            #endregion

            #region gridview 格式設置
            //不自動換行
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            //選中行變色
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            #endregion
        }
        #endregion

        #region gridview表體格式設置
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
        #endregion
    }

    #endregion
    /// <summary>
    /// download 
    /// </summary>
    public void ResponseStream()
    {
        DataSet dts = (DataSet)Session["ds"];
        string strPOitem = "";
        string res = "";
        if (dts.Tables.Count > 0)
        {
            if (dts.Tables[0].Rows.Count > 0)
            {
                #region thead
                res += " <table cellpadding=\"0\" style='font-size:12.0pt;' cellspacing=\"0\" id=\"tReport\" width=\"100%\"> " +
                    " <thead> " +
                    " <tr><td colspan='22' style='text-align:center;font-weight:bold; height:50px; font-size:16px;'>PO Trace Report</td></tr> " +
                    " <tr style='height:50px; background-color:#00B0F0; color:#fff;font-weight:700;'> " +
                    " <th > MSFT PO </th>" +
                    " <th > MSFT PO ITEM </th> " +
                    " <th > MSFT P/N</th>" +
                    " <th > CREATIONDT</th>" +
                    " <th > Description </th>" +
                    " <th > PO Price Base Unit</th>" +
                    " <th > Purchase Oreder QTY (Piece)</th>" +
                    " <th > Pick Up Date </th>" +
                    " <th > ShipToAdress </th>" +
                    " <th > Request Delivery Date</th>" +
                    " <th > Storage Location  </th>" +
                    " <th > Balance Qty </th>" +
                    " <th > Delivery Note No </th>" +
                    " <th > Delivery date </th> " +
                    " <th > Delivery Note Qty </th>" +
                    " <th > Booking request Status </th>" +
                    " <th > Booking request ACK </th> " +
                    " <th > Load ID </th>" +
                    " <th > Carrier ID</th>" +
                    " <th > Transportation Method </th>" +
                    " <th > ASN Send </th>" +
                    //" <th > ASN Send ACK </th> " +
                    " <th > Send3B2Time </th>" +
                    " </tr>  " +
                    " </thead>" +
                    " <tbody >";
                #endregion

                #region tbody
                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    string str940h = "";
                    #region 3A4
                    res += "<tr>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["MSFT PO"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["MSFT PO ITEM"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["MSFT P/N"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["CREATIONDT"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["Description"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["PO Price Base Unit"].ToString() + "</td>";
                    //res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["Oreder QTY"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["BaseQty"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["Pick Up Date"].ToString().Trim().Substring(0, 8) + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["ShipToAdress"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["Request Delivery Date"].ToString().Trim().Substring(0, 8) + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["Storage Location"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["Balance Qty"].ToString() + "</td>";
                    #endregion

                    #region 940
                    DataRow[] dr;
                    dr = dts.Tables[1].Select("poid='" + dts.Tables[0].Rows[i]["MSFT PO"].ToString() + "'");
                    strPOitem = "0" + dts.Tables[0].Rows[i]["MSFT PO ITEM"].ToString();
                    if (dr.Length > 0)
                    {
                        #region W0502
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td1\">";
                        res += "<table>";
                        for (int j = 0; j < dr.Length; j++)
                        {
                            string aaaa = dr[j]["poitemid"].ToString();
                            if (aaaa == strPOitem)
                            {
                                res += "<tr>";
                                res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr[j]["dnid"].ToString();
                                res += "</td>";
                                res += "</tr>";
                                str940h = str940h + "_" + j;
                            }
                        }
                        res += "</table>";
                        res += "</td>";
                        #endregion

                        #region Delivery date
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td1\">";
                        res += "<table>";
                        for (int j = 0; j < dr.Length; j++)
                        {
                            if (dr[j]["poitemid"].ToString() == strPOitem)
                            {
                                res += "<tr>";
                                res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr[j]["Delivery date"].ToString().Trim().Substring(0, 8);
                                res += "</td>";
                                res += "</tr>";
                            }
                        }
                        res += "</table>";
                        res += "</td>";
                        #endregion

                        #region Delivery Note Qty
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td1\">";
                        res += "<table>";
                        for (int j = 0; j < dr.Length; j++)
                        {
                            if (dr[j]["poitemid"].ToString() == strPOitem)
                            {
                                res += "<tr>";
                                res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr[j]["Delivery Note Qty"].ToString();
                                res += "</td>";
                                res += "</tr>";
                            }
                        }
                        res += "</table>";
                        res += "</td>";
                        #endregion

                        #region Booking request Status
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td1\">";
                        res += "<table>";
                        for (int j = 0; j < dr.Length; j++)
                        {
                            if (dr[j]["poitemid"].ToString() == strPOitem)
                            {
                                res += "<tr>";
                                res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr[j]["Booking request Status"].ToString();
                                res += "</td>";
                                res += "</tr>";
                            }
                        }
                        res += "</table>";
                        res += "</td>";
                        #endregion

                        #region Booking request ACK
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td1\">";
                        res += "<table>";
                        for (int j = 0; j < dr.Length; j++)
                        {
                            if (dr[j]["poitemid"].ToString() == strPOitem)
                            {
                                res += "<tr>";
                                res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr[j]["Booking request ACK"].ToString();
                                res += "</td>";
                                res += "</tr>";
                            }
                        }
                        res += "</table>";
                        res += "</td>";
                        #endregion

                    }
                    else
                    {
                        #region
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td1\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td1\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td1\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td1\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td1\"></td>";
                        #endregion
                    }
                    #endregion

                    #region 204

                    DataRow[] dr1;
                    dr1 = dts.Tables[2].Select("OID02='" + dts.Tables[0].Rows[i]["MSFT PO"].ToString() + "'");
                    string[] arr940h = str940h.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if (dr1.Length > 0)
                    {
                        int iW0502, iOID01 = 0;
                        string strLi = "";
                        #region LoadID
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        res += "<table>";
                        for (int j = 0; j < arr940h.Length; j++)
                        {
                            bool b = true;
                            string strDNID = dr[Convert.ToInt32(arr940h[j])]["DNID"].ToString();
                            iW0502 = Convert.ToInt32(strDNID);
                            for (int l = 0; l < dr1.Length; l++)
                            {
                                try
                                { iOID01 = Convert.ToInt32(dr1[l]["OID01"].ToString().Trim()); }
                                catch
                                { iOID01 = 0; }
                                if (iW0502 == iOID01)
                                {
                                    res += "<tr>";
                                    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr1[l]["LoadID"].ToString();
                                    res += "</td>";
                                    res += "</tr>";
                                    strLi = strLi + l + "_";
                                    b = false;
                                    break;
                                }
                                if (b && l == dr1.Length - 1)
                                {
                                    res += "<tr>";
                                    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>";
                                    res += "</td>";
                                    res += "</tr>";
                                }
                            }

                        }

                        res += "</table>";
                        res += "</td>";
                        #endregion
                        DownLoadType(str940h, dr, dr1, ref res, "CarrierID", "N", "204");
                        DownLoadType(str940h, dr, dr1, ref res, "Transportation Method", "T", "204");
                        #region  CarrierID
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        //res += "<table>";
                        //for (int k = 0; k < dr1.Length; k++)
                        //{
                        //    res += "<tr>";
                        //    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr1[k]["CarrierID"].ToString();
                        //    res += "</td>";
                        //    res += "</tr>";
                        //}
                        //res += "</table>";
                        //res += "</td>";
                        #endregion
                        #region Transportation Method
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        //res += "<table>";
                        //for (int k = 0; k < dr1.Length; k++)
                        //{
                        //    res += "<tr>";
                        //    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr1[k]["Transportation Method"].ToString();
                        //    res += "</td>";
                        //    res += "</tr>";
                        //}
                        //res += "</table>";
                        //res += "</td>";
                        #endregion
                        #region IssueDT
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        //res += "<table>";
                        //for (int k = 0; k < dr1.Length; k++)
                        //{
                        //    res += "<tr>";
                        //    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + Convert.ToDateTime(dr1[k]["IssueDT"]).ToString("yyyy-MM-dd");
                        //    res += "</td>";
                        //    res += "</tr>";
                        //}
                        //res += "</table>";
                        //res += "</td>";
                        #endregion
                    }
                    else
                    {
                        #region
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        #endregion
                    }
                    #endregion

                    #region 3B2
                    DataRow[] dr2;
                    dr2 = dts.Tables[3].Select("POID='" + dts.Tables[0].Rows[i]["MSFT PO"].ToString() + "'");
                    if (dr2.Length > 0)
                    {
                        #region ASN Send
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        //res += "<table>";
                        //for (int k = 0; k < dr.Length; k++)
                        //{
                        //    bool b = true;
                        //    string strDNID = dr[k]["DNID"].ToString();
                        //    iW0502 = Convert.ToInt32(strDNID);
                        //    try
                        //    { iOID01 = Convert.ToInt32(dr2[l1]["DNID"].ToString().Trim()); }
                        //    catch
                        //    { iOID01 = 0; }
                        //    if (iW0502 == iOID01)
                        //    {
                        //        res += "<tr>";
                        //        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr2[l1]["ASN Send"].ToString();
                        //        res += "</td>";
                        //        res += "</tr>";
                        //        strLi = strLi + k + "-";
                        //        b = false;
                        //        break;
                        //    }
                        //    if (b && k == dr2.Length - 1)
                        //    {
                        //        res += "<tr>";
                        //        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>";
                        //        res += "</td>";
                        //        res += "</tr>";
                        //    }
                        //}
                        //res += "</table>";
                        //res += "</td>";
                        #endregion
                        DownLoadType(str940h, dr, dr2, ref res, "ASN Send", "N", "3B2");
                        DownLoadType(str940h, dr, dr2, ref res, "Send3B2Time", "Y", "3B2");
                        #region  ASN Send ACK
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        //res += "<table>";
                        //for (int k = 0; k < dr2.Length; k++)
                        //{
                        //    res += "<tr>";
                        //    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr2[k]["ASN Send"].ToString();
                        //    res += "</td>";
                        //    res += "</tr>";
                        //}
                        //res += "</table>";
                        //res += "</td>";
                        #endregion

                        #region Send3B2Time
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        //res += "<table>";
                        //for (int k = 0; k < dr2.Length; k++)
                        //{
                        //    res += "<tr>";
                        //    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr2[k]["Send3B2Time"].ToString();
                        //    res += "</td>";
                        //    res += "</tr>";
                        //}
                        //res += "</table>";
                        //res += "</td>";
                        #endregion

                        #region IssueDT
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        //res += "<table>";
                        //for (int k = 0; k < dr1.Length; k++)
                        //{
                        //    res += "<tr>";
                        //    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + Convert.ToDateTime(dr1[k]["IssueDT"]).ToString("yyyy-MM-dd");
                        //    res += "</td>";
                        //    res += "</tr>";
                        //}
                        //res += "</table>";
                        //res += "</td>";
                        #endregion
                    }
                    else
                    {
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        //res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                    }
                    res += "</tr>";
                    #endregion

                }
                res += "<tr><td colspan='22'></td></tr>";
                res += " </tbody> </table>";
                #endregion
            }
        }
        #region Response
        this.Response.Clear();

        this.Response.Charset = "GB2312";

        this.Response.ContentEncoding = System.Text.Encoding.UTF8;

        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
        this.Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("POReport" + DateTime.Now.ToString("yyyyMMdd") + ".xls"));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
        this.Response.AddHeader("Content-Length", res.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载 
        this.Response.ContentType = "application/ms-excel";
        //// 把文件流发送到客户端 

        this.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
        this.Response.Write(res);
        #endregion
    }
    /// <summary>
    /// 數據轉換
    /// </summary>
    /// <param name="strLi">DNID相同的下標集合</param>
    /// <param name="dr">940的行數</param>
    /// <param name="dr1">3B2或204的數據行數</param>
    /// <param name="res">導出的Excel樣式代碼</param>
    /// <param name="str">要轉化的列名</param>
    /// <param name="str1">要轉換的格式代碼</param>
    protected void DownLoadType(string strLi, DataRow[] dr, DataRow[] dr1, ref string res, string str, string str1, string flag)
    {
        int iW0502, iOID01 = 0;
        string abc = "";
        string[] arr940h = strLi.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
        res += "<table>";
        for (int j = 0; j < arr940h.Length; j++)
        {
            bool b = true;
            string strDNID = dr[Convert.ToInt32(arr940h[j])]["DNID"].ToString();
            iW0502 = Convert.ToInt32(strDNID);
            for (int l = 0; l < dr1.Length; l++)
            {
                #region
                if (flag == "204")
                {
                    try
                    { iOID01 = Convert.ToInt32(dr1[l]["OID01"].ToString().Trim()); }
                    catch
                    { iOID01 = 0; }
                }
                else if (flag == "3B2")
                {
                    try
                    { iOID01 = Convert.ToInt32(dr1[l]["DNID"].ToString().Trim()); }
                    catch
                    { iOID01 = 0; }
                }

                if (iW0502 == iOID01)
                {

                    if (str1 == "T")
                    {
                        #region Transportation Method
                        string ms304 = dr1[l][str].ToString().Trim();
                        switch (ms304)
                        {
                            case "A":
                                abc = "By Air";
                                break;
                            case "H":
                                abc = "Customer Pickup";
                                break;
                            case "M":
                                abc = "By Road";
                                break;
                            case "R":
                                abc = "By Rail";
                                break;
                            case "S":
                                abc = "By Sea";
                                break;
                            case "U":
                                abc = "Private Parcel Service";
                                break;
                            case "X":
                                abc = "Intermodal (Piggyback)";
                                break;
                            case "LT":
                                abc = "Less Than Trailer Load (LTL)";
                                break;
                            case "RC":
                                abc = "Rail, Less than Carload";
                                break;
                            case "RR":
                                abc = "Roadrailer";
                                break;
                            case "SE":
                                abc = "Sea/Air";
                                break;
                            case "TA":
                                abc = "Towaway Service";
                                break;
                            default:
                                abc = dr1[l][str].ToString().Trim();
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        abc = dr1[l][str].ToString();
                    }
                    res += "<tr>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + abc;
                    res += "</td>";
                    res += "</tr>";
                    b = false;
                    break;
                }
                #endregion
                if (b && l == dr1.Length - 1)
                {
                    res += "<tr>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>";
                    res += "</td>";
                    res += "</tr>";
                }
            }
        }
        res += "</table>";
        res += "</td>";
    }

    public static void ToExcel(System.Web.UI.Control ctl, string FileName)
    {
        //HttpContext.Current.Response.Charset = "UTF-8";
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        //HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName + ".xls");
        //ctl.Page.EnableViewState = false;
        //System.IO.StringWriter tw = new System.IO.StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(tw);
        //ctl.RenderControl(hw);
        //HttpContext.Current.Response.Write(tw.ToString());
        //HttpContext.Current.Response.End();
    }

    //下面这几行代码一定不要忘记，否则导入不成功！
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
}