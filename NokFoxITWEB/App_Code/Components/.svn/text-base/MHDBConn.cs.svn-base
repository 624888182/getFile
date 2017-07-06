using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
///MHDBConn 的摘要说明
/// </summary>
public class MHDBConn
{
    private SqlConnection con;
    private SqlDataAdapter da;
    private SqlCommand sc;
    private DataTable dt;
    private string constr =  System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"].ToString();

 
	public MHDBConn()
	{
        con = new SqlConnection(constr);
	}
    public DataTable SysLogin(string empno,string pass)
    {
        string sqlstr = "select userid,username,(select purview from PurView where id=purno) as purview from usyusers where userid=  '" + empno + "'";
        dt = new DataTable();
        da = new SqlDataAdapter(sqlstr, con);
        da.Fill(dt);
        if (dt.Rows.Count != 0)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }
    public DataTable getDT(string sqlstr)
    {
        da = new SqlDataAdapter(sqlstr, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public int exeq(string sqlstr)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        sc = new SqlCommand(sqlstr, con);
        int query = sc.ExecuteNonQuery();
        if (con.State == ConnectionState.Open)
            con.Close();
        return query;
    }
    public Object getvalue(string sqlstr)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        sc = new SqlCommand(sqlstr, con);
        Object obj = sc.ExecuteScalar();
        if (con.State == ConnectionState.Open)
            con.Close();
        return obj;
    }
    public Boolean ifemponly(string empno)
    {
        string sqlstr = "select USERID from dbo.usyUsers where USERID='" + empno + "'";
        DataTable dt = this.getDT(sqlstr);
        if (dt.Rows.Count > 0)
            return false;
        else
            return true;
    }
    public Boolean ifonly(string empno, string wk)
    {
        string sqlstr = "select * from proper where empid='"+empno+"' and weekno='"+wk+"' ";
        DataTable dt = this.getDT(sqlstr);
        if (dt.Rows.Count > 0)
            return false;
        else
            return true;
    }

}
