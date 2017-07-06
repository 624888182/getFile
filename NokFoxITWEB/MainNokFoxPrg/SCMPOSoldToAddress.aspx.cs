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

public partial class MainBBRYPrg_SCMPOSoldToAddress : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string BBSCMDIR;
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
                BBSCMDIR = Session["Param7"].ToString();
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
            txtBeginDate.Text = DateTime.Now.AddMonths(-3).ToString("yyyy/MM/dd");
            txtendDate.Text = DateTime.Now.AddMonths(+1).ToString("yyyy/MM/dd");
            GridViewBind();
            Label0.Visible = false;
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
        string sqlselect1 = "SELECT b.[POID],b.POCNT,b.[CREATIONDT],[GivenName],[CountryCode],[RegionCode],[PostalCode],[CityName],[StreetName]" +
                     ",[CareOfName],[LanguageCode],[AreaID],[NumberDefaultIndicator],[NumberUsageDenialIndicator]" +
                     ",[URI],[URIDefaultIndicator],[URIUsageDenialIndicator]" +
                     " FROM " + BBSCMDIR + ".[dbo].[POSoldToAddress] a," + BBSCMDIR + ".[dbo].[PO_CREATE_MT] b" +
                     " where a.ID=b.ID ";
        string sqlselect2 = " union" +
                     " SELECT b.[POID],b.POCNT,b.[CREATIONDT],[GivenName],[CountryCode],[RegionCode],[PostalCode],[CityName],[StreetName]" +
                     ",[CareOfName],[LanguageCode],[AreaID],[NumberDefaultIndicator],[NumberUsageDenialIndicator]" +
                     ",[URI],[URIDefaultIndicator],[URIUsageDenialIndicator]" +
                     " from " + BBSCMDIR + ".[dbo].[POChangeSoldToAddress] a," + BBSCMDIR + ".[dbo].[PO_CHANGE_MT] b" +
                     " where a.ID=b.ID ";
        string sqlselect = "";
        if (potxt != "")
        {
            sqlselect1 = sqlselect1 + " and POID like '" + potxt + "%'";
            sqlselect2 = sqlselect2 + " and POID like '" + potxt + "%'";
        }
        if (begintime != "")
        {
            sqlselect1 = sqlselect1 + " and CREATIONDT>='" + begintime + "'";
            sqlselect2 = sqlselect2 + " and CREATIONDT>='" + begintime + "'";
        }
        if (endtime != "")
        {
            sqlselect1 = sqlselect1 + " and CREATIONDT<='" + endtime + "'";
            sqlselect2 = sqlselect2 + " and CREATIONDT<='" + endtime + "'";
        }
        sqlselect = sqlselect1 + sqlselect2 + " order by POID,POCNT";
        DataTable dtdetail = dbo.PSelectSQLDT(sqlselect);
        if (dtdetail.Rows.Count > 0)
        {
            GridView1.Visible = true;
            Session["SQL104"] = dtdetail;
            GridView1.DataSource = Session["SQL104"];
            GridView1.DataBind();
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
        Label0.Visible = false;
        GridView1.Visible = false;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();
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
