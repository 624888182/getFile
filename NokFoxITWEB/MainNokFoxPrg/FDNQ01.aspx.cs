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
using System.Text.RegularExpressions;

public partial class MainBBRYPrg_FDNQ01 : System.Web.UI.Page
{
    static string dbRead;
    static string dbWrite;
    static string dbType;
    static string sqlSel = string.Empty;
    static string BBSCMDIR;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dbRead = Session["Param3"].ToString();
            dbWrite = Session["Param4"].ToString();
            dbType = Session["Param2"].ToString();
            BBSCMDIR = Session["Param7"].ToString();
            sqlSel = @"SELECT TOP 10 * FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] ORDER BY CreationDT DESC";
            DataTable dt = new DataTable();
            dt = PDataBaseOperation.PSelectSQLDT(dbType, dbRead, sqlSel);
            gridBind(sqlSel, GridView1, ref dt);
            Session["dtMain"] = dt;
            gv1.Visible = true;
            gv2.Visible = false;
        }
    }
    protected void ButtonOk_Click(object sender, ImageClickEventArgs e)
    {
       
        if (dnNum.Text.Length == 0)
        {
            string bTime = BegTime.Text.ToString().Replace("/", "");
            string eTime = EndTime.Text.ToString().Replace("/", "");
            sqlSel = @"SELECT * FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] WHERE REPLACE(SUBSTRING(CreationDT,0,11),'-','') >= '" + bTime + "' AND REPLACE(SUBSTRING(CreationDT,0,11),'-','') <= '" + eTime + "'";

        }
        else
        {
            string dn = dnNum.Text.ToString();
            sqlSel = @"SELECT * FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] WHERE DNID = '" + dn + "'";
        }
        DataTable dt = new DataTable();
        gridBind(sqlSel, GridView1,ref dt);
        Session["dtMain"] = dt;
        gv1.Visible = true;
        
        gv2.Visible = false;
    }
    protected void ButtonDet_Click(object sender, EventArgs e)
    {
        int rowsNum = Convert.ToInt32(((Button)sender).CommandArgument);
        string dn = GridView1.Rows[rowsNum].Cells[0].Text.ToString();
        string sql = @"SELECT * from " + BBSCMDIR + ".[dbo].[Delivery_Map_DN] where DNID = '" + dn + "'";
        DataTable dt = new DataTable();
        gridBind(sql, GridView2,ref dt);
        if (dt.Rows.Count > 0) Label2.Text = dt.Rows.Count.ToString();
        else Label2.Text = "0";
        gv2.Visible = true;

        string sql1 = @"SELECT * from " + BBSCMDIR + ".[dbo].[Delivery_Notification_DT] where DNID = '" + dn + "'";
        DataTable dt1 = new DataTable();
        gridBind(sql1, GridView3, ref dt1);
        if (dt1.Rows.Count > 0) Label4.Text = dt1.Rows.Count.ToString();
        else Label4.Text = "0";

        string sql2 = @"SELECT * from " + BBSCMDIR + ".[dbo].[Delivery_Notification_HU] where DNID = '" + dn + "'";
        DataTable dt2 = new DataTable();
        gridBind(sql2, GridView4, ref dt2);
        if (dt2.Rows.Count > 0) Label6.Text = dt2.Rows.Count.ToString();
        else Label6.Text = "0";
       
        gv1.Visible = false;
        Session["dtMap"] = dt;
        Session["dtDT"] = dt1;
        Session["dtHU"] = dt2;
    }

    private void gridBind(string sql, GridView gv, ref DataTable dt)
    {
        dt = PDataBaseOperation.PSelectSQLDT(dbType, dbRead, sql);
        gv.DataSource = dt;
        gv.DataBind();

    }
   
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataSource = Session["dtMap"];
        GridView2.DataBind();
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        GridView3.DataSource = Session["dtDT"];
        GridView3.DataBind();
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        GridView4.DataSource = Session["dtHU"];
        GridView4.DataBind();
    }

    protected void ButtonConf_Click(object sender, EventArgs e)
    {
        int rowsNum = Convert.ToInt32(((Button)sender).CommandArgument);
        string dn = GridView1.Rows[rowsNum].Cells[0].Text.ToString();
        //string sql = @"SELECT T1.[DNID],T1.[HUID],T1.[BoxID],T2.[ItemID],T2.[QA],T2.[SerialID],T2.[POID],T2.[POTypeCode],T2.[POItemID],T2.[POItemTypeCode],T2.[InternalID],T2.[ProductRecipientID],T3.[IssueDT],T3.[ArrivalDT],T3.[ProductRecipientID],T3.[BuyerPartyInternalID],T3.[VendorPartyVendorID] FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_HU] T1," + BBSCMDIR + ".[dbo].[Delivery_Notification_DT] T2," + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] T3 WHERE T1.DNID = T2.DNID AND T2.DNID = T3.DNID AND  T1.DNID = '" + dn + "' and T1.BoxID =T2.BoxID ORDER BY T1.HUID,T1.BoxID,T1.DNID,T2.ItemID";
        string sql = @"SELECT T3.[IssueDT],T3.[ArrivalDT],T3.[ProductRecipientID],T3.[VendorPartyVendorID] FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] T3 WHERE T3.DNID = '" + dn + "' ";
        DataTable dt = new DataTable();
        dt = PDataBaseOperation.PSelectSQLDT(dbType, dbRead, sql);
        if (dt.Rows.Count > 0)
        {
            string items = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (items.Length != 0)
                        break;
                    if (dt.Rows[i][j].ToString().Trim().Length == 0)
                    {
                        items = items + "第" + (i + 1) + "行" + dt.Rows[i].Table.Columns[j].ColumnName.ToString() + "為空！";
                        break;
                    }
                }
            if (items.Length == 0)
            {

                Response.Write("<script>alert('Confirm ok!');</script>");
                string sqlUpDate = @"UPDATE " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] SET CONFIRMFLAG = 'Y' WHERE DNID = '" + dn + "'";
                int ret = PDataBaseOperation.PExecSQL(dbType, dbWrite, sqlUpDate);
            }
            else
                Response.Write("<script>alert('" + items + "');</script>");       
        }     
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        gv2.Visible = false;
        gv1.Visible = true;
    }

    private int allQty(DataTable dt, string name)
    {
        int ret = 0;
        string itemName = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == 0)
            {
                itemName = dt.Rows[i][name].ToString();
                ret = 1;
            }
            else
            {
                if (itemName != dt.Rows[i][name].ToString())
                {
                    itemName = dt.Rows[i][name].ToString();
                    ret++;
                }
            }
        }
        return ret;
    }
   
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = PDataBaseOperation.PSelectSQLDT(dbType, dbRead, sqlSel);
        int row = 0;
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            Button bt = (Button)gvr.FindControl("ButtonConf");
            Button bt1 = (Button)gvr.FindControl("ButtonClear");
            Button bt2 = (Button)gvr.FindControl("ButtonUp");
            TextBox tbIssDt = (TextBox)gvr.FindControl("issueDt");
            TextBox tbArrDt = (TextBox)gvr.FindControl("arrivalDt");
            TextBox tbBillId = (TextBox)gvr.FindControl("wayBillId");
            TextBox tbPro = (TextBox)gvr.FindControl("productrecipientId");
            tbIssDt.Text = dt.Rows[row][4].ToString();
            tbArrDt.Text = dt.Rows[row][5].ToString();
            tbBillId.Text = dt.Rows[row][10].ToString();
            tbPro.Text = dt.Rows[row][14].ToString();
            if (dt.Rows[row][33].ToString().Trim() == "Y")
            {
                bt.Enabled = false;
                bt2.Enabled = false;
            }
            if (dt.Rows[row][15].ToString().Trim() == "Y")
                bt1.Enabled = false;
            row++;
        }
    }
    protected void ButtonClear_Click(object sender, EventArgs e)
    {
        if (Hidden1.Value.ToString() == "1")
        {
            int rowsNum = Convert.ToInt32(((Button)sender).CommandArgument);
            string dn = GridView1.Rows[rowsNum].Cells[1].Text.ToString();
            string sqlD1 = @"DELETE FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_DT]  WHERE DNID = '" + dn + "'";
            string sqlD2 = @"DELETE FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_HU]  WHERE DNID = '" + dn + "'";
            //int ret1 = PDataBaseOperation.PExecSQL(dbType, dbWrite, sqlD1);
            //int ret2 = PDataBaseOperation.PExecSQL(dbType, dbWrite, sqlD2);
            Hidden1.Value = "0";
        }
    }
    protected void ButtonUp_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        string issDay = ((TextBox)GridView1.Rows[row].FindControl("issueDt")).Text.ToString();
        string arrDay = ((TextBox)GridView1.Rows[row].FindControl("arrivalDt")).Text.ToString();
        string dn = GridView1.Rows[row].Cells[0].Text.ToString();
        string wayBill = ((TextBox)GridView1.Rows[row].FindControl("wayBillId")).Text.ToString();
        string ProductRecipientID = ((TextBox)GridView1.Rows[row].FindControl("productrecipientId")).Text.ToString();
        if (issDay.Length != 24)
        {
            Response.Write("<script>alert('IssueDT日期格式不正確');</script>");
            return;
        }
        if (arrDay.Length != 24)
        {
            Response.Write("<script>alert('ArrivalDT日期格式不正確');</script>");
            return;
        }
        //arrDay = arrDay.Substring(0, 4) + "-" + arrDay.Substring(4, 2) + "-" + arrDay.Substring(6, 2) + "T" + arrDay.Substring(8, 2) + "." + arrDay.Substring(10, 2) + "." + arrDay.Substring(12, 3) + "Z"; 
        string sqlUp = @"UPDATE " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] SET [IssueDT] = '" + issDay + "',[ArrivalDT] = '" + arrDay + "',[WayBillID] = '" + wayBill + "',ProductRecipientID = '" + ProductRecipientID + "' WHERE DNID = '" + dn + "'";
        int iRet = PDataBaseOperation.PExecSQL(dbType, dbRead, sqlUp);
        DataTable dt = PDataBaseOperation.PSelectSQLDT(dbType, dbRead, sqlSel);
        GridView1.DataSource = dt;
        Session["dtMain"] = dt;
        GridView1.DataBind();
    }
    protected void arrivalDt_TextChanged(object sender, EventArgs e)
    {
        string id = ((TextBox)sender).Parent.Parent.ClientID;
        int x = id.IndexOf('l');
        string i = id.Substring(x + 1);
        int rowid = int.Parse(i) - 2;
        DateTime dt = System.DateTime.Now;
        string dtT = dt.ToString("yyyyMMddHHmmssfff");
        string[] day = ((TextBox)GridView1.Rows[rowid].FindControl("arrivalDt")).Text.ToString().Split('/');
        string dayTime = day[0] + "-" + day[1] + "-" + day[2] + "T" + dtT.Substring(8, 2) + ":" + dtT.Substring(10, 2) + ":" + dtT.Substring(12, 2) + "." + dtT.Substring(14, 3) + "Z";
        ((TextBox)GridView1.Rows[rowid].FindControl("arrivalDt")).Text = dayTime.ToString();

    }
    protected void issueDt_TextChanged(object sender, EventArgs e)
    {
        string id = ((TextBox)sender).Parent.Parent.ClientID;
        int x = id.IndexOf('l');
        string i = id.Substring(x + 1);
        int rowid = int.Parse(i) - 2;
        DateTime dt = System.DateTime.Now;
        string dtT = dt.ToString("yyyyMMddHHmmssfff");
        string[] day = ((TextBox)GridView1.Rows[rowid].FindControl("issueDt")).Text.ToString().Split('/');
        string dayTime = day[0] + "-" + day[1] + "-" + day[2] + "T" + dtT.Substring(8, 2) + ":" + dtT.Substring(10, 2) + ":" + dtT.Substring(12, 2) + "." + dtT.Substring(14, 3) + "Z";
        ((TextBox)GridView1.Rows[rowid].FindControl("issueDt")).Text = dayTime.ToString();

    }
}
