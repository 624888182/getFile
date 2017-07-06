using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class LabelMapUpLoad_FPOQ01 : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    protected void Page_Load(object sender, EventArgs e)
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
        ////  Conn = "Server=10.186.19.108 ;User id=sa;Pwd=Sa123456;Database=BBSCM";//108
        //Conn = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//108
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["poid"]))
            {
                this.txtKey.Text = Request.QueryString["poid"].ToString();
            }
            else
            {
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["pocnt"]))
            {
                this.txtPOCNT.Text = Request.QueryString["pocnt"].ToString();
            }
            GetPoDetail();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GetPoDetail();
    }
    private void GetPoDetail()
    {
        if (!string.IsNullOrEmpty(txtKey.Text))
        {
            DataTable baseDt = new DataTable();
            string POID = txtKey.Text.Trim().Replace("'", "");
            string BaseSqlStr = "  SELECT A.ID, A.POID, CONVERT(NVARCHAR(10),CAST(CREATIONDT AS DATETIME),111)  AS CREATIONDT, SELLERPARTYID , A.POCNT,  " +
                                "  C.GivenName,C.StreetName,C.CareOfName,(C.CityName+C.PostalCode)AS cityPost,C.CityName,B.CostCurrencyCode, " +
                                "  D.GivenName AS GivenName_sold,D.StreetName AS StreetName_sold,(D.CityName+D.PostalCode) AS  cityPost_sold , D.AreaID AS AreaID_sold" +
                                "  FROM dbo.PO_CREATE_MT  A" +
                                "  JOIN (SELECT TOP 1 * FROM [POShipToAddress] WHERE ID=(SELECT ID FROM PO_CREATE_MT WHERE POID='" + POID + "') ORDER BY ItemID )C ON A.ID=C.ID" +
                                "  JOIN (SELECT TOP 1 * FROM [POSoldToAddress] WHERE ID=(SELECT ID FROM PO_CREATE_MT WHERE POID='" + POID + "') ORDER BY ItemID) D ON A.ID=D.ID" +
                                "  JOIN (SELECT TOP 1 CostCurrencyCode,POID FROM dbo.PO_CREATE_DT WHERE POID='" + POID + "') B ON A.POID=B.POID" +
                                "  where A.POID='" + POID + "'";


          
            baseDt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, BaseSqlStr);

            if (baseDt.Rows.Count == 0)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('Not found any data,Please check the POID!'); window.close();</script>");
                return;
            }
            string sqlstr = "";
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(txtPOCNT.Text))
            {

                if (Convert.ToInt32(txtPOCNT.Text) > 1)
                {
                    sqlstr = "  SELECT ITEMID,CostCurrencyCode, INTERNALID,[DESCRIPTION],Unit,CONVERT(NVARCHAR(10),CAST(DELIVERYSTARTDT AS DATETIME),111) AS DELIVERYSTARTDT,SCHEDULEQUANTITY,AMOUNT " +
                             "  FROM  PO_CHANGE_DT  where POID='" + baseDt.Rows[0]["POID"].ToString() + "'  and POCNT ='" + txtPOCNT.Text + "'";
                    Response.Write(sqlstr);
                }
                else
                {
                    sqlstr = "  SELECT ITEMID,CostCurrencyCode, INTERNALID,[DESCRIPTION],Unit,CONVERT(NVARCHAR(10),CAST(DELIVERYSTARTDT AS DATETIME),111) AS DELIVERYSTARTDT,SCHEDULEQUANTITY,AMOUNT " +
                             "  FROM  PO_Create_DT  where ID='" + baseDt.Rows[0]["ID"].ToString() + "'";
                }
            }
            else
            {
                sqlstr = "  SELECT ITEMID,CostCurrencyCode, INTERNALID,[DESCRIPTION],Unit,CONVERT(NVARCHAR(10),CAST(DELIVERYSTARTDT AS DATETIME),111) AS DELIVERYSTARTDT,SCHEDULEQUANTITY,AMOUNT " +
                         "  FROM  PO_Create_DT  where ID='" + baseDt.Rows[0]["ID"].ToString() + "'";
            }
            dt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, sqlstr);

            try
            {
                PageDateBind(baseDt, dt);
            }
            catch (Exception)
            {
                //
            }
        }
    }
    private void PageDateBind(DataTable baseDt, DataTable dt)
    {
        if (baseDt.Rows.Count > 0)
        {
            DataRow dr = baseDt.Rows[0];
            //block2 MT/DT
            lblPoNum.Text = dr["POID"].ToString();
            lblPoDate.Text = dr["CREATIONDT"].ToString();

            lblChange.Text = dr["POCNT"].ToString().PadLeft(3, '0');
            lblRevisedDate.Text = dr["CREATIONDT"].ToString();
            lblTerms.Text = "NET60";

            lblSuppliter.Text = dr["SELLERPARTYID"].ToString();
            lblCurrency.Text = dr["CostCurrencyCode"].ToString();

            //block3 ship to 
            lblGivenName.Text = dr["GivenName"].ToString();
            lblCareOfName.Text = dr["CareOfName"].ToString();
            lblStreetName.Text = dr["StreetName"].ToString();
            lblCityName.Text = dr["CityName"].ToString();
            lblCityPost.Text = dr["cityPost"].ToString();
            //block4 sold to 
            lblGivenName_sold.Text = dr["GivenName_sold"].ToString();
            lblCityPost_sold.Text = dr["cityPost_sold"].ToString();
            lblAeriId.Text = dr["AreaID_sold"].ToString();
            lblStreetName_sold.Text = dr["StreetName_sold"].ToString();

            //    tbData.Rows.Clear();
            decimal total = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TableRow tr = new TableRow();
                for (int j = 0; j < 8; j++)
                {
                    TableCell tc = new TableCell();
                    tr.Cells.Add(tc);
                }
                tr.Cells[0].Text = dt.Rows[i]["ItemID"].ToString().PadLeft(4, '0');
                tr.Cells[0].CssClass = "tableCell_center";
                tr.Cells[1].Text = dt.Rows[i]["INTERNALID"].ToString();
                tr.Cells[1].CssClass = "tableCell_left";
                tr.Cells[2].Text = dt.Rows[i]["DESCRIPTION"].ToString();
                tr.Cells[2].CssClass = "tableCell_left";
                tr.Cells[3].Text = dt.Rows[i]["DELIVERYSTARTDT"].ToString();
                tr.Cells[3].CssClass = "tableCell_left";
                tr.Cells[4].Text = dt.Rows[i]["SCHEDULEQUANTITY"].ToString();
                tr.Cells[4].CssClass = "tableCell_right";
                tr.Cells[5].Text = dt.Rows[i]["Unit"].ToString();
                tr.Cells[5].CssClass = "tableCell_center";
                tr.Cells[6].Text = dt.Rows[i]["AMOUNT"].ToString();
                tr.Cells[6].CssClass = "tableCell_right";
                if (!string.IsNullOrEmpty(dt.Rows[i]["SCHEDULEQUANTITY"].ToString()))
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["AMOUNT"].ToString()))
                    {
                        Decimal UM = Convert.ToDecimal(dt.Rows[i]["SCHEDULEQUANTITY"].ToString());
                        Decimal Amount = Convert.ToDecimal(dt.Rows[i]["AMOUNT"].ToString());

                        total += UM * Amount;
                        tr.Cells[7].Text = (UM * Amount).ToString("N2");
                        tr.Cells[7].CssClass = "tableCell_right";
                    }
                    else
                    {
                        tr.Cells[7].Text = "";
                    }
                }
                else
                {
                    tr.Cells[7].Text = "";
                }
                tr.Font.Size = 11;
                tbData.Rows.Add(tr);
            }
            //add total
            TableRow tr_total = new TableRow();
            tr_total.Height = 60;
            tr_total.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Bottom;
            TableCell tc_total = new TableCell();
            tc_total.ColumnSpan = 8;
            tc_total.Text = "<table><tr><td align='left' width='100'>Net Value</td><td style=' border-top:1px dashed #000;width:100px;'>" + total + "</td></tr><tr><td align='left' width='100'>Total Amount</td><td style='font-weight:bold; border-top:1px dashed #000;width:100px;'>" + total + "</td></tr></table>";
            tc_total.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            tc_total.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Bottom;
            tr_total.Cells.Add(tc_total);
            tbData.Rows.Add(tr_total);
        }
    }
}