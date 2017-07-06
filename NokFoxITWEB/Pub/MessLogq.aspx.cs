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

public partial class MainNokPrg_MessLogq : System.Web.UI.Page
{
    static string tmpDocumentID = "", Ptype = "1", tmp1 = "";
    static string connRead;//207
    static string connWrite;//207
    static string dbType;
    static string Autoprg;
    static DataBaseOperation dbo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["Param1"].ToString() == "1")
            {
                dbType = Session["Param2"].ToString();
                connRead = Session["Param3"].ToString();
                connWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
            }
            else
            {
                Response.Write("<script>alert('您所傳遞的字符串地址不正確，請重新傳遞!')</script>");
                return;
            }


            //connRead = "Server=10.186.19.207;User id=sa;Pwd=Sa123456;Database=ERPDBF";//207
            //connWrite = System.Web.Configuration.WebConfigurationManager.AppSettings["connWrite"];
            //dbType = "sql";


            dbo = new DataBaseOperation(dbType, connRead);
        }
        this.DataBind();
    }
    protected void DataBind()
    {
        string sqlStr = "SELECT Top 200 [F0],[F1],[F2],[F3],[F4],[F5],[F6],[F7],[F8],[F9],[F10]"+
                        " FROM [ERPDBF].[dbo].[MessLog]"+
                        " order by F0 desc";
        DataTable dt = dbo.SelectSQLDT(sqlStr);
        if (dt.Rows.Count > 0)
        {
            label2.Text = dt.Rows.Count.ToString();
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
        GridView1.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
    }
}
