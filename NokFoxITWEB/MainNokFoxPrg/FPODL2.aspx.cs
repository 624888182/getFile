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
using System.Drawing;

public partial class MainBBRYPrg_FPODL2 : System.Web.UI.Page
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
    string rowcount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 數據庫連接
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                tparam = Session["tparam"].ToString();
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
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            #endregion

            dbo = new PDataBaseOperation(conntype, Conn);
            Label0.Visible = false;
            BegTime = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            EndTime = DateTime.Now.ToString("yyyy/MM/dd");
            txtBeginDate.Text = BegTime;
            txtendDate.Text = EndTime;
            GridViewBind(" top 20 ");
        }
        rowcount = "";
    }

    protected void GridViewBind(string top)
    {//mt.PO_Create_MT_UF1 AS 'Request Delivery Date',
        string StrSql = "";
        StrSql = @"select mt.SENDERID as  'MSFT DUNS',dt.POID as 'MSFT PO',dt.ItemID AS 'MSFT PO ITEM',dt.InternalID AS 'MSFT P/N',MT.CREATIONDT,dt.Description," +
            " '' 'PO Price Base Unit',dt.CostAmount,dt.CostCurrencyCode,'' 'Oreder QTY',dt.BaseQty,dt.Unit,"+
            "'' 'MRP_Price',dt.PO_Create_DT_UF3,dt.PO_Create_DT_UF4,'' 'MRP_PricingUnit',dt.PO_Create_DT_UF5,dt.PO_Create_DT_UF6," +
            "dt.DeliveryStartDT as 'Pick Up Date'," +
            " '' ShipToAdress,ship.givenname,ship.streetname,ship.cityname,ship.regioncode,ship.postalcode,ship.countrycode," +
            //"DT.IncoTermsCode AS 'Payment Term'," +
            "mt.PO_Create_MT_UF5 as Incoterm,mt.PO_Create_MT_UF4 AS Payment,mt.PO_Create_MT_UF6 AS 'ShipTo DUNS Code',mt.PO_Create_MT_UF7 AS ShipToCode," +
            "dt.PO_Create_DT_UF1 AS 'Request Delivery Date',dt.PO_Create_DT_UF2 AS 'Storage Location',mt.PO_Create_MT_UF2 AS PurchaseGroup,DT.POCNT," +
            "mt.PO_Create_MT_UF3 AS DateTimeStamp,mt.id" +
            ",dt.CONFIRMFLAG" +
            //"mt.PO_Create_MT_UF8 AS 'Storage Location'" +
            " from " + BBSCMDIR + ".[dbo].[PO_CREATE_DT] dt," + BBSCMDIR + ".[dbo].[PO_CREATE_MT] mt," + BBSCMDIR + ".[dbo].[POSHIPTOADDRESS] ship where mt.id=dt.id and mt.id = ship.id";
        StrSql = "select distinct "+top+" * from (" + StrSql + ") M where 1=1 ";
        #region
        if (TextBox1.Text != "")
        {
            StrSql = StrSql + " and \"MSFT PO\"= '" + TextBox1.Text + "'";
        }
        if (txtBeginDate.Text != "")
        {
            string begintime = txtBeginDate.Text.Replace("/", "-").Trim();
            begintime = begintime + " 00:00:00";
            StrSql = StrSql + " and CREATIONDT> '" + begintime + "'";
        }
        if (txtendDate.Text != "")
        {
            string endtime = txtendDate.Text.Replace("/", "-").Trim();
            endtime = endtime + " 23:59:59";
            StrSql = StrSql + " and CREATIONDT< '" + endtime + "'";
        }

        StrSql = StrSql + "  order by CREATIONDT desc,M.POCNT";
        DataTable dt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, StrSql);
        #endregion
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string address = "";

                string DT_UF3 = dt.Rows[i]["PO_Create_DT_UF3"].ToString().Trim();
                string DT_UF4 = dt.Rows[i]["PO_Create_DT_UF4"].ToString().Trim();
                string DT_UF5 = dt.Rows[i]["PO_Create_DT_UF5"].ToString().Trim();
                string DT_UF6 = dt.Rows[i]["PO_Create_DT_UF6"].ToString().Trim();
                if (DT_UF3 != "" && DT_UF6 != "")
                {
                    dt.Rows[i]["MRP_Price"] = DT_UF3 + DT_UF6;
                }
                if (DT_UF4 != "" && DT_UF5 != "")
                {
                    dt.Rows[i]["MRP_PricingUnit"] = DT_UF4 + DT_UF5;
                }

                //PO Price Base Unit
                string CostAmount = dt.Rows[i]["CostAmount"].ToString().Trim();
                string CostCurrencyCode = dt.Rows[i]["CostCurrencyCode"].ToString().Trim();
                if (CostAmount != "" && CostCurrencyCode != "")
                {
                    dt.Rows[i]["PO Price Base Unit"] = CostAmount + CostCurrencyCode;
                }
                //Oreder QTY
                string BaseQty = dt.Rows[i]["BaseQty"].ToString().Trim();
                string Unit = dt.Rows[i]["Unit"].ToString().Trim();
                if (BaseQty != "" && Unit != "")
                {
                    dt.Rows[i]["Oreder QTY"] = BaseQty + Unit;
                }
                //ShipToAdress    
                string givenname = dt.Rows[i]["givenname"].ToString().Trim();
                string streetname = dt.Rows[i]["streetname"].ToString().Trim();
                string cityname = dt.Rows[i]["cityname"].ToString().Trim();
                string regioncode = dt.Rows[i]["regioncode"].ToString().Trim();
                string postalcode = dt.Rows[i]["postalcode"].ToString().Trim();
                string countrycode = dt.Rows[i]["countrycode"].ToString().Trim();
                if (givenname != "") address = address + givenname + ",";
                if (streetname != "") address = address + streetname + ",";
                if (cityname != "") address = address + cityname + ",";
                if (regioncode != "") address = address + regioncode + ",";
                if (postalcode != "") address = address + postalcode + ",";
                if (countrycode != "") address = address + countrycode + " ";
                dt.Rows[i]["ShipToAdress"] = address;
              
            }
       
            dt.Columns.Remove("CostAmount");
            dt.Columns.Remove("CostCurrencyCode");
            dt.Columns.Remove("BaseQty");
            dt.Columns.Remove("Unit");
            dt.Columns.Remove("givenname");
            dt.Columns.Remove("streetname");
            dt.Columns.Remove("cityname");
            dt.Columns.Remove("regioncode");
            dt.Columns.Remove("postalcode");
            dt.Columns.Remove("countrycode");
            dt.Columns.Remove("PO_Create_DT_UF3");
            dt.Columns.Remove("PO_Create_DT_UF4");
            dt.Columns.Remove("PO_Create_DT_UF5");
            dt.Columns.Remove("PO_Create_DT_UF6");
            Session["SQL104"] = dt;
            //lblTotal.Text = "";
            //lblTotal.Text = "Total:&nbsp;<span style='font-weight:bold;'>" + dt.Rows.Count.ToString() + "</span>&nbsp;Rows";           
            Label19.Visible = false;
            GridView11.Visible = true;
            GridView1.Visible = true;
            GridView1.DataSource = Session["SQL104"];
            GridView1.DataBind();
        }
        else
        {
            Label19.Text = "NO Data found!";
           
            Label19.Visible = true;
            GridView11.Visible = false;
        }
    }

    #region button
    //Select
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label0.Visible = false;
        string top = "";
        GridViewBind(top);
    }

    //Reset
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        Label0.Visible = false;
        GridView1.Visible = false;
    }
    

    //Download
    protected void Button3_Click(object sender, EventArgs e)
    {
        string dtime = DateTime.Now.ToString("yyyyMMddhhmmss");
        this.ExportDataTable("application/ms-excel", "" + dtime + "-POIntergration.xls");
    }

    #region ExportDataTable
    public void ExportDataTable(string FileType, string FileName)
    {
        GridView gv = new GridView();
        gv.DataSource = Session["SQL104"];
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
    #endregion
    #endregion

    #region GridView
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
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string confirmFlag = DataBinder.Eval(e.Row.DataItem, "CONFIRMFLAG").ToString().Trim();
            if (confirmFlag == "Y" || confirmFlag == "y")
            {
                e.Row.BackColor = Color.LightGreen;           
            }
            else if (confirmFlag == "D" || confirmFlag == "d")
            {
                e.Row.BackColor = Color.Red;
            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i == 12 || i == 11)
                {
                    e.Row.Cells[i].BackColor = Color.Yellow;
                }
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
       
    }
    protected void Gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int row = e.RowIndex;
        row = row % 20;
        string poid = ((Label)GridView1.Rows[row].FindControl("Label3")).Text.ToString();
        string itemid = ((Label)GridView1.Rows[row].FindControl("Label4")).Text.ToString();
        string ID = ((Label)GridView1.Rows[row].FindControl("Labelid")).Text.ToString();
        string Oreder_QTY = ((Label)GridView1.Rows[row].FindControl("Label8")).Text.ToString();
        string PO_Price = ((Label)GridView1.Rows[row].FindControl("Label7")).Text.ToString();
        string ShipToAdress = ((Label)GridView1.Rows[row].FindControl("Label10")).Text.ToString();
        Response.Write("<script>window.open('IMSCMPODetailNew.aspx?poid=" + poid + "&itemid=" + itemid + "&ID=" + ID +"&Oreder_QTY=" + Oreder_QTY + "&PO_Price=" + PO_Price + "&ShipToAdress=" + ShipToAdress + "','newwindow','width=\"100%\",height=\"100%\",status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=no,top=0,left=0');</script>");
        return;
    }

    protected void shippingdate_TextChanged(object sender, EventArgs e)
    {
        string id = ((TextBox)sender).Parent.Parent.ClientID;
        int x = id.IndexOf('l');
        string i = id.Substring(x + 1);
        int rowid = int.Parse(i) - 2;
        DateTime dt = System.DateTime.Now;
        string dtT = dt.ToString("yyyyMMddHHmmssfff");
        //string[] day = ((TextBox)GridView1.Rows[rowid].FindControl("issueDt")).Text.ToString().Split('/');
        //string dayTime = day[0] + "-" + day[1] + "-" + day[2] + "T" + dtT.Substring(8, 2) + ":" + dtT.Substring(10, 2) + ":" + dtT.Substring(12, 2) + "." + dtT.Substring(14, 3) + "Z";
        //((TextBox)GridView1.Rows[rowid].FindControl("issueDt")).Text = dayTime.ToString();
    }
    
    protected void shippingdate1_TextChanged(object sender, EventArgs e)
    {
        string id = ((TextBox)sender).Parent.Parent.ClientID;
        int x = id.IndexOf('l');
        string i = id.Substring(x + 1);
        int rowid = int.Parse(i) - 2;
        DateTime dt = System.DateTime.Now;
        string dtT = dt.ToString("yyyyMMddHHmmssfff");
        //string[] day = ((TextBox)GridView1.Rows[rowid].FindControl("issueDt")).Text.ToString().Split('/');
        //string dayTime = day[0] + "-" + day[1] + "-" + day[2] + "T" + dtT.Substring(8, 2) + ":" + dtT.Substring(10, 2) + ":" + dtT.Substring(12, 2) + "." + dtT.Substring(14, 3) + "Z";
        //((TextBox)GridView1.Rows[rowid].FindControl("issueDt")).Text = dayTime.ToString();

    }
    #endregion
}
