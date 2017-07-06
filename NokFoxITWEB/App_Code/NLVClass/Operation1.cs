using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Operation
/// </summary>
public class Operation1
{
    static string mm;
    public Operation1()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Operation1(string conn)
    {
        mm = conn;
    }
    public static SqlConnection createCon()
    {

        string ConnStr = "";
        if(!mm.Equals(""))
           ConnStr = mm;
        else
           ConnStr = "Data Source=10.186.19.207;Initial Catalog=FIH_NOKIA_SYNCRO_DV;User ID=sa;Password=Sa123456";
        SqlConnection con = new SqlConnection(ConnStr);
        return con;
    }
    public static DataTable getDataTable(string sql, string table)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection con = createCon();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            
            sda.Fill(dt);
            return dt;
        }

        catch (Exception ex)
        {
            return dt;
        }

    }
    public static string GetParamByListBox(ListBox lb, string mark)
    {
        string parama = string.Empty;
        string splitChar = "/";
        int paramcount = 0;

        if (lb.Items[0].Selected)
        {
            parama = "ALL/";
        }
        else
        {
            
            
                for (int i = 1; i < lb.Items.Count; i++)
                {
                    if (lb.Items[i].Selected)
                    {

                        parama += lb.Items[i].Text + splitChar;

                        paramcount++;


                    }
                }

                parama = paramcount + splitChar + parama;
            }
        
        parama = splitChar + mark + splitChar + parama;
        
        return parama;
 
    }
}