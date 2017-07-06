using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class SysConfig_SysLogView2 : System.Web.UI.Page
{
    DbAccessing myDbAccessing = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;


        string str_keyid = Server.UrlDecode(Request.QueryString["kerid"].ToString().Replace("'", ""));
        string sql_edit = " select * from View_Log where 1=1" + " and LogId ='" + str_keyid + "'";
        DataTable dt = myDbAccessing.ExecuteSqlTable(sql_edit);
        foreach (DataRow dr in dt.Rows)
        {
            this.tbLogId.Text = dr["LogId"].ToString();
            this.tbModuleName.Text = dr["ModuleName"].ToString();
            this.tbOperSQL.Text = dr["OperSQL"].ToString();
            this.tbOperTime.Text = dr["OperTime"].ToString();
            this.tbOperType.Text = dr["OperType"].ToString();
            this.tbUserID1.Text = dr["UserID"].ToString();
            this.tbUserName.Text = dr["UserName"].ToString();
        }
    }
    protected void btnExist_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }
}
