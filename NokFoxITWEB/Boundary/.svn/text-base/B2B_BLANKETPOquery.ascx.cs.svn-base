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
using System.Data.SqlClient;

public partial class Boundary_B2B_BLANKETPOquery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            MultiLanguage();
            bind();   
        }
    }

    private void MultiLanguage()
    {
        btnSearch.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    } 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bind();  
    }
    private void bind()
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetData();
        if (strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //SqlDataAdapter sda1 = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "blanketpo");
        this.gvBlanketPO.DataSource = this.GetIDTable(ds.Tables[0]).DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvBlanketPO.DataSource = ds;
            gvBlanketPO.DataBind();
            int columnCount = gvBlanketPO.Rows[0].Cells.Count;
            gvBlanketPO.Rows[0].Cells.Clear();
            gvBlanketPO.Rows[0].Cells.Add(new TableCell());
            gvBlanketPO.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvBlanketPO.Rows[0].Cells[0].Text = "No Records Found.";
            gvBlanketPO.Rows[0].Visible = true;

        }
        else
        {
            this.gvBlanketPO.DataBind();
        }
        conn.Close();
    }

    private string Split(string strPoNo)
    {
        string strInput = strPoNo;
        string strOutput = "";
        try
        {
            string[] strInput1 = strInput.Split(new Char[] { ',' });
            for (int i = 0; i < strInput1.Length; i++)
                strOutput = strOutput + "'" + strInput1[i].Trim().ToString() + "',";
            strOutput = strOutput.Substring(0, strOutput.Length - 1);

            return strOutput;
        }
        catch
        {
            return "split error";
        }
    }

    private string GetData()
    {
        string strBlanketPO = txtBlanketPO.Text.Trim();

        string strsql = "select * from xmlblanketpoconfig where 1=1 ";
        if (!strBlanketPO.Equals(""))
        {
            strBlanketPO = Split(strBlanketPO);
            if (strBlanketPO.Equals("split error"))
                return "input error";
            else
                strsql += " and blanketpo in (" + strBlanketPO + ")";
        }

        return strsql;
    }
    private DataTable GetIDTable(DataTable dt)
    {
        DataColumn col = new DataColumn("新增", Type.GetType("System.Int32"));
        dt.Columns.Add(col);
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (0 == i)
                dt.Rows[0][col] = 1;
            else
                dt.Rows[i][col] = Convert.ToInt32(dt.Rows[i - 1][col]) + 1;
        }
        return dt;
    }
    protected void gvBlanketPO_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

    }
    protected void gvBlanketPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblID");
            olabel.Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }
}
