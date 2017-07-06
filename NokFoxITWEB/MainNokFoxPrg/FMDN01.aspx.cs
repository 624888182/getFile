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
using SFC.TJWEB;
using System.Drawing;

public partial class MainMSPrg_FMDN01 : System.Web.UI.Page
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
    private static string tparam;
    private static int count;
    private static string tprivate;
    private static DataTable dt = new DataTable();
    BBSCMlib BBSCMlibPointer = new BBSCMlib();
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
                tparam = Session["tparam"].ToString();  // PROLINE 
                tprivate = Session["C_3B2"].ToString();
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
            //conntype = "sql";
            ////Conn = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93
            ////dbWrite = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93

            //Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//108
            //dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//108

            dbo = new PDataBaseOperation(conntype, Conn);
            txtBeginDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            txtendDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            Label1.Visible = false;
            Label2.Visible = false;
            GridViewBind();

            //根據權限設定按鈕有效性
            if (tprivate == "DISPLAY" || tprivate == "")
            {
                Button3.Enabled = false;
            }
        }      
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        dnNum.Text = "";
        plant.Text = "";
        Label1.Visible = false;
        Label2.Visible = false;
        GridView1.Visible = false;
        GridView2.Visible = false;
    }
    
    protected void Button3_Click(object sender, EventArgs e)
    {
        string dnid = dnNum.Text.ToString().Trim();
        string begindate = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
        string enddate = DateTime.Now.ToString("yyyyMMdd");
        string dbRead="";
        if (dnid == "")
        {

            Response.Write("<script>alert('Please input DN number,it is mandatory condition !');</script>");
            return;
        }
        //20151117
        string check = "select * from " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] where DNID='" + dnid + "'";
        DataTable dtcheck = dbo.PSelectSQLDT(check);
        if (dtcheck.Rows.Count > 0)
        {
            GridViewBind();
            Label1.Text = "Have get the SAP Data!";
            Label1.Visible = true;
            return;
        }
        if (tparam == "TEST")
        {
            dbRead = "ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh";
        }
        else if (tparam == "PROD")
        {
            dbRead = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";
        }
        string mes = GSFClib1.GetSapDN(dnid, dbRead, dbWrite, begindate, enddate);
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        if (mes == "True")
        {
            GridViewBind();
            Label1.Text = "Get DN from SAP successfully!";
            Label1.Visible = true;
        }
        else
        {
            Label1.Text = "Get DN from SAP failed!";
            Label1.Visible = true;
        }
    }
    protected void lbtnDnID_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        row = row % 7;
        string dnid = ((LinkButton)GridView1.Rows[row].FindControl("lbtnDnID")).Text.ToString();
        string strselect = "select * from " + BBSCMDIR + ".[dbo].[Delivery_DNITEM] where DNID='" + dnid + "'";
        DataTable dtselect = dbo.PSelectSQLDT(strselect);
        if (dtselect.Rows.Count > 0)
        {
            GridView2.Visible = true;
            Session["SQLDNDT"] = dtselect;
            GridView2.DataSource = Session["SQLDNDT"];
            GridView2.DataBind();
            Label2.Visible = false;
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "NO Data Found!";
            GridView2.Visible = false;
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
        string dntxt = dnNum.Text.Trim();
        string begintime = TimeFormat(txtBeginDate.Text);
        string endtime = TimeFormat(txtendDate.Text);
        string plantid = plant.Text.Trim();
        string sqlselect = "select substring(CreationDT,1,10) tCreationDT,substring(IssueDT,1,10) tIssueDT,substring(ArrivalDT,1,10) tArrivalDT,*"+
                           " from " + BBSCMDIR + ".dbo.Delivery_Notification_MT where (CONFIRMFLAG!='D' or CONFIRMFLAG is null)";
        if (dntxt != "") { sqlselect = sqlselect + " and DNID like '" + dntxt + "%'"; }
        if (begintime != "") { sqlselect = sqlselect + " and CreationDT>='" + begintime + "'"; }
        if (endtime != "") { sqlselect = sqlselect + " and CreationDT<='" + endtime + "'"; }
        if (plantid != "") { sqlselect = sqlselect + ""; }//MT表中工廠欄位？？？
        sqlselect = sqlselect + " order by CreationDT desc";
        DataTable dtdetail = dbo.PSelectSQLDT(sqlselect);
        count = dtdetail.Rows.Count;
        if (dtdetail.Rows.Count > 0)
        {
            GridView1.Visible = true;
            Session["SQLDN"] = dtdetail;
            GridView1.DataSource = Session["SQLDN"];
            GridView1.DataBind();
            Label1.Visible = false;
            Label2.Visible = false;
            GridView2.Visible = false;
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "No Data found!You can input this DN in textbox,then click 'GetSAP' button to get the record!";
            GridView1.Visible = false;
            Label2.Visible = false;
            GridView2.Visible = false;
        }
    }
    #region GV1
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
        //得到新的值 
        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["SQLDN"];
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //after confirm,no saving
            string confirmFlag = DataBinder.Eval(e.Row.DataItem, "CONFIRMFLAG").ToString().Trim();
            string sendFlag = DataBinder.Eval(e.Row.DataItem, "SendFlag").ToString().Trim();
            if (sendFlag == "Y")
            {
                e.Row.BackColor = Color.LightGreen;
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
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
    #endregion

    #region GV2
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView2.BottomPagerRow;
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
        newPageIndex = newPageIndex >= GridView2.PageCount ? GridView2.PageCount - 1 : newPageIndex;
        //得到新的值 
        GridView2.PageIndex = newPageIndex;
        GridView2.DataSource = Session["SQLDNDT"];
        GridView2.DataBind();

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //after confirm,no saving
            string dnid = DataBinder.Eval(e.Row.DataItem, "DNID").ToString().Trim();
            string strselect = "select SendFlag FROM " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] where DNID='" + dnid + "'";
            DataTable dtselect = dbo.PSelectSQLDT(strselect);
            if (dtselect.Rows.Count > 0)
            {
                string confirmflag = dtselect.Rows[0]["SendFlag"].ToString().Trim();
                if (confirmflag != "N")
                {
                    e.Row.BackColor = Color.LightGreen;
                }
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
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
    #endregion
}