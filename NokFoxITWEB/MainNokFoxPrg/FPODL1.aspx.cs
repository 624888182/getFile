using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class MainBBRYPrg_FPODL1 : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static DataTable dt = new DataTable();
    string rowcount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                conntype = Session["Param2"].ToString();
                Conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
                //param = Session["Param8"].ToString();
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;

            }

            //conntype = "sql";
            //Conn = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93
            //dbWrite = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93

            dbo = new PDataBaseOperation(conntype, Conn);
            Label0.Visible = false;
            rowcount = " Top 5 ";
            GridViewBind();
        }
        rowcount = "";
    }
    protected string TimeFormat(string datetime)
    {
        string dateform = "";
        if (datetime != "")
        {
            dateform = datetime.Replace("/", "-").Trim();
            dateform = dateform + "T00:00:00Z";
        }
        return dateform;
    }
    protected void GridViewBind()
    {
        string potxt = TextBox1.Text.Trim();
        string begintime = TimeFormat(txtBeginDate.Text);
        string endtime = TimeFormat(txtendDate.Text);
        string prcid = TextBox2.Text.Trim();
        string cpnc = TextBox3.Text.Trim();
        string incoterm = TextBox4.Text.Trim();
        string sqlselect = "";
        string createselect = "SELECT " + rowcount + " mt.ID,CREATIONDT,PRODUCTCATEGORYID,mt.POID,dt.ITEMID,dt.OriginID,dt.INTERNALID,dt.DESCRIPTION,dt.AMOUNT,dt.Currency," +
                              "dt.CostAmount,dt.CostCurrencyCode,dt.SCHEDULEQUANTITY,substring(dt.DELIVERYSTARTDT,0,11) DELIVERYSTARTDT,'CN' Country_Origin," +
                              "dt.IncoTermsCode+' '+dt.IncoTermsName Incoterm,dt.PO_CREATE_DT_UF3 Ship_to_Code,dt.PRODUCTRECIPIENTPARTYID Carrier_ID,mt.CONFIRMFLAG,mt.POCNT, " +
                              "(select Top 1 ContextText FROM " + BBSCMDIR + ".[dbo].[POText] where POID=mt.POID and TypeCode='SP') CCI_Sold_To," +
                              "(select Top 1 ContextText FROM " + BBSCMDIR + ".[dbo].[POText] where POID=mt.POID and TypeCode='HD') CCI_Header " +
                              "FROM " + BBSCMDIR + ".[dbo].[PO_CREATE_MT] mt," + BBSCMDIR + ".[dbo].[PO_CREATE_DT] dt " +
                              "where mt.POID=dt.POID and mt.POCNT=dt.POCNT";
        string changeselect = " union "+
                              "SELECT " + rowcount + " mt.ID,CREATIONDT,PRODUCTCATEGORYID,mt.POID,dt.ITEMID,dt.OriginID,dt.INTERNALID,dt.DESCRIPTION,dt.AMOUNT,dt.Currency," +
                              "dt.CostAmount,dt.CostCurrencyCode,dt.SCHEDULEQUANTITY,substring(dt.DELIVERYSTARTDT,0,11) DELIVERYSTARTDT,'CN' Country_Origin," +
                              "dt.IncoTermsCode+' '+dt.IncoTermsName Incoterm,dt.PO_CHANGE_DT_UF3 Ship_to_Code,dt.PRODUCTRECIPIENTPARTYID Carrier_ID,mt.CONFIRMFLAG,mt.POCNT, " +
                              "(select Top 1 ContextText FROM " + BBSCMDIR + ".[dbo].[POText] where POID=mt.POID and TypeCode='SP') CCI_Sold_To," +
                              "(select Top 1 ContextText FROM " + BBSCMDIR + ".[dbo].[POText] where POID=mt.POID and TypeCode='HD') CCI_Header " +
                              "FROM " + BBSCMDIR + ".[dbo].[PO_CHANGE_MT] mt," + BBSCMDIR + ".[dbo].[PO_CHANGE_DT] dt " +
                              "where mt.POID=dt.POID and mt.POCNT=dt.POCNT";
        if (potxt != "") 
        { 
            createselect = createselect + " and mt.POID like '" + potxt + "%'";
            changeselect = changeselect + " and mt.POID like '" + potxt + "%'"; 
        }
        if (begintime != "") 
        {
            createselect = createselect + " and mt.CREATIONDT>='" + begintime + "'";
            changeselect = changeselect + " and mt.CREATIONDT>='" + begintime + "'"; 
        }
        if (endtime != "") 
        {
            createselect = createselect + " and mt.CREATIONDT<='" + endtime + "'";
            changeselect = changeselect + " and mt.CREATIONDT<='" + endtime + "'"; 
        }
        if (prcid!= "") 
        {
            createselect = createselect + " and PRODUCTCATEGORYID like '" + prcid + "%'";
            changeselect = changeselect + " and PRODUCTCATEGORYID like '" + prcid + "%'"; 
        }
        if (cpnc != "")
        {
            createselect = createselect + " and dt.INTERNALID like '" + cpnc + "%'";
            changeselect = changeselect + " and dt.INTERNALID like '" + cpnc + "%'";
        }
        if (incoterm != "")
        {
            createselect = createselect + " and dt.IncoTermsCode like '" + incoterm + "%'";
            changeselect = changeselect + " and dt.IncoTermsCode like '" + incoterm + "%'";
        }
        sqlselect = createselect + changeselect + " order by mt.POID desc,POCNT,CREATIONDT";
        DataTable dtdetail = new DataTable();
        dtdetail = dbo.PSelectSQLDT(sqlselect);
        int count = dtdetail.Rows.Count;
        if (dtdetail.Rows.Count > 0)
        {
            GridView1.Visible = true;

            //補充FIH_P/N_Code--data from [BBSCM].[dbo].[Delivery_DNITEM]
            dtdetail.Columns.Add("FIH_P/N_Code");
            for (int i = 0; i < count; i++)
            {
                string poid = dtdetail.Rows[i]["POID"].ToString().Trim();
                string itemid = dtdetail.Rows[i]["ITEMID"].ToString().Trim();
                string internalid = dtdetail.Rows[i]["INTERNALID"].ToString().Trim();
                string sqldnitem = "select * FROM " + BBSCMDIR + ".[dbo].[Delivery_DNITEM] where POID='" + poid + "' and POItemID='0000" + itemid + "' and ProductRecipientID='" + internalid + "'";
                DataTable dtdnitem = new DataTable();
                dtdnitem = dbo.PSelectSQLDT(sqldnitem);
                if (dtdnitem.Rows.Count > 0)
                {
                    dtdetail.Rows[i]["FIH_P/N_Code"] = dtdnitem.Rows[0]["InternalID"].ToString().Trim();
                }
            }

            //Bind ShiptoAddress
            //20141122 Bind POSoldToAddress.GivenName to CarrierName
            dtdetail.Columns.Add("CountryCode");
            dtdetail.Columns.Add("Ship_to_Address");
            dtdetail.Columns.Add("CarrierName");
            for (int i = 0; i < count; i++)
            {
                string address = "";
                string id = dtdetail.Rows[i]["ID"].ToString().Trim();
                string poid = dtdetail.Rows[i]["POID"].ToString().Trim();
                string itemid = dtdetail.Rows[i]["ITEMID"].ToString().Trim();
                string pocnt = dtdetail.Rows[i]["POCNT"].ToString().Trim();

                //20150316 add
                string sqlshipto = "", sqlsoldto = "";
                if (pocnt == "1")
                {
                    sqlshipto = "select * FROM " + BBSCMDIR + ".[dbo].[POShipToAddress] where ID='" + id + "' and ItemID='" + itemid + "'";
                    sqlsoldto = "select * FROM " + BBSCMDIR + ".[dbo].[POSoldToAddress] where ID='" + id + "' and ItemID='" + itemid + "'";
                }
                else
                {
                    sqlshipto = "select * FROM " + BBSCMDIR + ".[dbo].[POChangeShipToAddress] where ID='" + id + "' and ItemID='" + itemid + "'";
                    sqlsoldto = "select * FROM " + BBSCMDIR + ".[dbo].[POChangeSoldToAddress] where ID='" + id + "' and ItemID='" + itemid + "'";
                }
                DataTable dtshipto = new DataTable();
                dtshipto = dbo.PSelectSQLDT(sqlshipto);
                DataTable dtsoldto = new DataTable();
                dtsoldto = dbo.PSelectSQLDT(sqlsoldto);

                ////20141122 Bind POSoldToAddress.GivenName to CarrierName
                //string sqlsoldto = "select * FROM [BBSCM].[dbo].[POSoldToAddress] where ID='" + id + "' and ItemID='" + itemid + "'";
                //DataTable dtsoldto = new DataTable();
                //dtsoldto = dbo.PSelectSQLDT(sqlsoldto);
                //if (dtsoldto.Rows.Count == 0)
                //{
                //    string sqlcreate = "select * from [BBSCM].[dbo].[PO_CREATE_DT] where POID='" + poid + "' and ITEMID='" + itemid + "'";
                //    DataTable dtcreate = new DataTable();
                //    dtcreate = dbo.PSelectSQLDT(sqlcreate);
                //    if (dtcreate.Rows.Count > 0)
                //    {
                //        id = dtcreate.Rows[0]["ID"].ToString().Trim();
                //    }
                //    sqlsoldto = "select * FROM [BBSCM].[dbo].[POSoldToAddress] where ID='" + id + "' and ItemID='" + itemid + "'";
                //    dtsoldto = dbo.PSelectSQLDT(sqlsoldto);
                //}

                if (dtshipto.Rows.Count > 0)
                {
                    if (dtshipto.Rows[0]["CountryCode"].ToString().Trim() != "") dtdetail.Rows[i]["CountryCode"] = dtshipto.Rows[0]["CountryCode"].ToString().Trim();
                    if (dtshipto.Rows[0]["GivenName"].ToString().Trim() != "") address = address + dtshipto.Rows[0]["GivenName"].ToString().Trim() + ",";
                    if (dtshipto.Rows[0]["StreetName"].ToString().Trim() != "") address = address + dtshipto.Rows[0]["StreetName"].ToString().Trim() + ",";
                    if (dtshipto.Rows[0]["CityName"].ToString().Trim() != "") address = address + dtshipto.Rows[0]["CityName"].ToString().Trim() + ",";
                    if (dtshipto.Rows[0]["RegionCode"].ToString().Trim() != "") address = address + dtshipto.Rows[0]["RegionCode"].ToString().Trim() + ",";
                    if (dtshipto.Rows[0]["PostalCode"].ToString().Trim() != "") address = address + dtshipto.Rows[0]["PostalCode"].ToString().Trim() + ",";
                    if (dtshipto.Rows[0]["CountryCode"].ToString().Trim() != "") address = address + dtshipto.Rows[0]["CountryCode"].ToString().Trim() + " ";
                    
                    dtdetail.Rows[i]["Ship_to_Address"] = address;
                }
                //20141122 Bind POSoldToAddress.GivenName to CarrierName
                if (dtsoldto.Rows.Count > 0)
                {
                    if (dtsoldto.Rows[0]["GivenName"].ToString().Trim() != "") dtdetail.Rows[i]["CarrierName"] = dtsoldto.Rows[0]["GivenName"].ToString().Trim();
                }
            }
            dt = dtdetail;
            Session["SQL93"] = dtdetail;
            GridView1.DataSource = Session["SQL93"];
            GridView1.DataBind();
            GridView1.Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        }
        else
        {
            Label0.Visible = true;
            Label0.Text = "請重新選擇查詢條件！";
            GridView1.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label0.Visible = false;
        GridViewBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        Label0.Visible = false;
        GridView1.Visible = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string dtime = DateTime.Now.ToString("yyyyMMddhhmmss");
        this.ExportDataTable("application/ms-excel", "" + dtime + "-POIntergration.xls");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    public void ExportDataTable(string FileType, string FileName)
    {
        GridView gv = new GridView();
        gv.DataSource = dt;
        gv.DataBind();
        Response.Charset = "UNICODE";
        //Response.ContentEncoding = System.Text.Encoding.UTF7;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
        Response.ContentType = FileType;
        this.EnableViewState = false;
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = Session["SQL93"];
        GridViewBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
    }
}
