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

public partial class MainBBRYPrg_FPOCO1 : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string param;
    private static string tablename;
    private static string pocnt;
    private static int count;
    private static DataTable dt = new DataTable();
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
            param = "2";

            if (param == "1")
            {
                tablename = "" + BBSCMDIR + ".[dbo].[PO_CREATE_DT]";
                pocnt = "'1'";
            }
            else
            {
                tablename = "" + BBSCMDIR + ".[dbo].[PO_CHANGE_DT]";
                pocnt = ">'1'";
            }

            dbo = new PDataBaseOperation(conntype, Conn);
            txtBeginDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            txtendDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            GridViewBind();
            GridView2Bind();
            Label8.Visible = false;
        }
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
        string confirmflag = DropDownList1.Text;
        string sqlselect = "select ID,POID,ITEMID,POCNT,DELIVERYSTARTDT,SCHEDULELINEID,SCHEDULEQUANTITY," +
                           " CONFIRMFLAG,UserConfirmFlag FROM " + tablename + " " +
                           " where 1=1";
        if (potxt != "") { sqlselect = sqlselect + " and POID like '" + potxt + "%'"; }
        if (begintime != "") { sqlselect = sqlselect + " and DELIVERYSTARTDT>='" + begintime + "'"; }
        if (endtime != "") { sqlselect = sqlselect + " and DELIVERYSTARTDT<='" + endtime + "'"; }
        if (confirmflag == "Yes") { sqlselect = sqlselect + " and UserCONFIRMFLAG='Y'"; }
        if (confirmflag == "No") { sqlselect = sqlselect + " and (UserCONFIRMFLAG='' or UserCONFIRMFLAG='N' or UserCONFIRMFLAG is null)"; }
        sqlselect = sqlselect + " order by DELIVERYSTARTDT desc,SCHEDULELINEID";
        DataTable dtdetail = dbo.PSelectSQLDT(sqlselect);
        count = dtdetail.Rows.Count;
        if (dtdetail.Rows.Count > 0)
        {
            GridView1.Visible = true;
            Session["SQL104"] = dtdetail;
            GridView1.DataSource = Session["SQL104"];
            GridView1.DataBind();

            GridView2Bind();
        }
        else
        {
            Label8.Visible = true;
            Label8.Text = "請重新選擇查詢條件！";
            GridView1.Visible = false;
        }
    }
    protected void GridView2Bind()
    {
        int pageindex = GridView1.PageIndex;
        int beginrow = pageindex * 10;
        int endrow = (pageindex + 1) * 10 - 1;

        for (int i = beginrow; i < endrow + 1 && i < count; i++)
        {
            int ii = i % 10;
            string id = ((Label)(GridView1.Rows[ii].FindControl("Label2"))).Text.ToString().Trim();
            string po = ((Label)(GridView1.Rows[ii].FindControl("Label3"))).Text.ToString().Trim();
            string itemid = ((Label)(GridView1.Rows[ii].FindControl("Label4"))).Text.ToString().Trim();
            string pocnt = ((Label)(GridView1.Rows[ii].FindControl("Label11"))).Text.ToString().Trim();
            string sqlitem = "select QTY,DELIVERYSTARTDT from " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] where ID='" + id + "' and POID='" + po + "' and ITEMID='" + itemid + "' and  POCNT ='" + pocnt + "' order by SCHEDULELINEID";
            DataTable dtitem = dbo.PSelectSQLDT(sqlitem);
            if (dtitem.Rows.Count > 0)
            {
                int pp = GridView1.Rows.Count;
                ((GridView)GridView1.Rows[ii].FindControl("GridView2")).DataSource = dtitem;
                ((GridView)GridView1.Rows[ii].FindControl("GridView2")).DataBind();
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label8.Visible = false;
        GridViewBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        Label8.Visible = false;
        GridView1.Visible = false;
        DropDownList1.Text = "ALL";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label2"))).Text;
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label4"))).Text;
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label3"))).Text;
        string slineid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label6"))).Text;
        string squantity = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label5"))).Text;
        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label11"))).Text;

        //檢查PO是否已經Confirm
        string str = "select * from " + tablename + " where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and CONFIRMFLAG='Y'";
        DataTable dtstr = dbo.PSelectSQLDT(str);
        if (dtstr.Rows.Count > 0)
        {
            Label8.Visible = true;
            Label8.Text = "該PO已經Confirm，不可重複Confirm，請重新選擇！";
            return;
        }

        string strsum = "select SUM(CONVERT(float,QTY)) QTY from " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] where ID='" + id + "' and POID='" + poid + "' and  ITEMID='" + itemid + "' and  POCNT ='" + pocnt + "'";
        DataTable dtsum = dbo.PSelectSQLDT(strsum);
        if (dtsum.Rows.Count > 0)
        {
            double sum = 0;
            if (dtsum.Rows[0][0].ToString() != "") sum = double.Parse(dtsum.Rows[0][0].ToString());
            if ((double.Parse(squantity)) == sum)
            {
                string strflag1 = "update " + tablename + " set UserCONFIRMFLAG='Y' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT ='" + pocnt + "'";
                int idet1 = dbo.PExecSQL(strflag1);
                string strflag2 = "update " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] set UserCONFIRMFLAG='Y',CONFIRMFLAG='Y' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT ='" + pocnt + "'";
                int idet2 = dbo.PExecSQL(strflag2);
                string strflag3 = "update " + tablename + " set CONFIRMFLAG='Y' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT ='" + pocnt + "'";
                int idet3 = dbo.PExecSQL(strflag3);
                Label8.Visible = true;
                if ((idet1 > 0) && (idet2 > 0))
                {
                    Label8.Text = "Confirm成功！";
                    GridViewBind();
                }
                else
                {
                    Label8.Text = "Confirm失敗，請核對數量！";
                    ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label7"))).Text = "N";
                    ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text = "N";
                }
            }
            else
            {
                Label8.Visible = true;
                Label8.Text = "數量不等，請核對！";
                ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label7"))).Text = "N";
                ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text = "N";
            }
        }
        else
        {
            Label8.Visible = true;
            Label8.Text = "Confirm失敗，該PO沒有Confirmation數量！";
            ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label7"))).Text = "N";
            ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text = "N";
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Label8.Visible = true;
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label2"))).Text;
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label3"))).Text;
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label4"))).Text;
        string slineid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label6"))).Text;
        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label11"))).Text;

        //檢查PO是否已經Confirm
        string str = "select * from " + tablename + " where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and CONFIRMFLAG='Y'";
        DataTable dtstr = dbo.PSelectSQLDT(str);
        if (dtstr.Rows.Count > 0)
        {
            Label8.Visible = true;
            Label8.Text = "該PO已經Confirm，不能Clear！";
            return;
        }

        string strdelete = "delete from " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT ='" + pocnt + "'";
        int idetdelete = dbo.PExecSQL(strdelete);
        string strflag = "update " + tablename + " set UserCONFIRMFLAG='',CONFIRMFLAG='',CONFIRM_ADD_FLAG='' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "'";
        int idetflag = dbo.PExecSQL(strflag);
        if (idetflag > 0)
        {
            Label8.Text = "此PO已經Clear!";
        }
        else
        {
            Label8.Text = "此筆資料Clear失敗!";
        }
        GridViewBind();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Label8.Visible = true;
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label2"))).Text;
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label3"))).Text;
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label4"))).Text;
        string slineid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label6"))).Text;
        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label11"))).Text;

        string clearMT = "update " + BBSCMDIR + ".[dbo].[PO_CHANGE_MT] set CONFIRMFLAG='' where ID='" + id + "' and POID='" + poid + "'";
        int idetMT = dbo.PExecSQL(clearMT);
        string clearDT = "update " + tablename + " set UserCONFIRMFLAG='',CONFIRMFLAG='',CONFIRM_ADD_FLAG='' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "'";
        int idetDT = dbo.PExecSQL(clearDT);
        string clearCON = "update " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] set UserCONFIRMFLAG='',CONFIRMFLAG='',SENDFLAG='' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and POCNT ='" + pocnt + "'";
        int idetCON = dbo.PExecSQL(clearDT);
        if (idetMT > 0 && idetDT > 0 && idetCON > 0)
        {
            Label8.Text = "此PO已經Clear!";
        }
        else
        {
            Label8.Text = "此筆資料Clear失敗!";
        }
        GridViewBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();

        GridView2Bind();
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
