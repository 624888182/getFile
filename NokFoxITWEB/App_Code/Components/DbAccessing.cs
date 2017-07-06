using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataReader = System.Data.SqlClient.SqlDataReader;
using System.Data.SqlClient;
using System.Collections;


//Create by james 2006/08/09


/// <summary>
/// DbAccessing 的摘要说明

/// </summary>
public class DbAccessing: System.Web.UI.Page
{
    public SqlConnection dbConn;  
	public DbAccessing()
	{
        setConn();
	}

    protected virtual void setConn()
    {
        dbConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DefaultConnectionString"]);
    }
   
    public bool ExecuteSql(string strSQL)
    {
        SqlCommand cmd = new SqlCommand(strSQL,dbConn);
        bool returnValue = false;
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            cmd.ExecuteNonQuery();
            returnValue =  true;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();           
        }
        return returnValue;
    }

    public bool ExecuteSql(string strSQL, string strOperType, string strModuleName)
    {
        bool reValue = false;
        if(dbConn.State  != ConnectionState.Open)
            dbConn.Open();
        SqlCommand cmd = new SqlCommand();
        System.Data.SqlClient.SqlTransaction dbTrans;
        dbTrans = dbConn.BeginTransaction();
        cmd.Connection = dbConn;
        cmd.Transaction = dbTrans;
        try
        {
            cmd.CommandText = strSQL;
            cmd.ExecuteNonQuery();
            dbTrans.Commit();
            reValue = true;
        }
        catch
        {
            dbTrans.Rollback();
            reValue = false;
        }
        finally
        {
            dbConn.Close();
        }
        return reValue;
    }



    public bool ExecuteSql(ArrayList list)
    {
        if (dbConn.State != ConnectionState.Open)
            dbConn.Open();
        SqlTransaction Trans = dbConn.BeginTransaction();
        SqlCommand cmd = new SqlCommand();
        bool returnValue = false;
        try
        {
            cmd.Connection = dbConn;
            cmd.Transaction = Trans;
            for (int i = 0; i < list.Count; i++)
            {
                cmd.CommandText = list[i].ToString();
                cmd.ExecuteNonQuery();
            }
            Trans.Commit();
            returnValue = true;
        }
        catch (Exception ex)
        {
            Trans.Rollback();
            throw new Exception(ex.ToString());
        }
        finally
        {
            cmd.Dispose();
            dbConn.Close();
        }
        return returnValue;
    }

    public DataSet ExecuteSqlDS(string strSQL)
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, dbConn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
    }


    public DataTable ExecuteSqlTable(string strSQL)
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, dbConn);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }

    }

    public DataTable ExecuteStoreTable(string strStroe)//存儲過程無參數-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            
            SqlCommand sc = new SqlCommand();
            sc.CommandText = strStroe;
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }

    }

    public DataReader ExecuteSqlReader(string strSql)//-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = strSql;
            sc.CommandType = CommandType.Text;
            sc.Connection = dbConn;

            return sc.ExecuteReader();
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }


    }
    public DataReader ExecuteStoreReader(string strStroe)//存儲過程無參數-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = strStroe;
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;

            return sc.ExecuteReader();
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }

    }

    public DataReader ExecuteStoreReader(string strStroe,string likeID)//存儲過程無參數-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = strStroe;
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@ID", likeID);
            return sc.ExecuteReader();
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
    }
    public DataTable  ExecuteStoregetdata(string strStroe, string wk,string dept ,string userid,string empid)//存儲過程無參數-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = strStroe;
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@WEEKNO", wk);
            sc.Parameters.AddWithValue("@dept", dept);
            sc.Parameters.AddWithValue("@userid", userid);
            sc.Parameters.AddWithValue("@empid", empid);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];         
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
    }
    public DataTable ExecuteStoreSearchData(string commandText, string documentID, string customerSite, string foxconnStie, string customerPN)//存儲過程無參數-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = commandText;
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@DocumentID", documentID);
            sc.Parameters.AddWithValue("@CustomerSite", customerSite);
            sc.Parameters.AddWithValue("@FoxconnSite", foxconnStie);
            sc.Parameters.AddWithValue("@CustomerPN", customerPN);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
    }
    public DataTable ExecuteStoregetMONTH(string strStroe, string wk, string dept, string userid, string empid)//存儲過程無參數-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = strStroe;
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@MONTHNO", wk);
            sc.Parameters.AddWithValue("@dept", dept);
            sc.Parameters.AddWithValue("@userid", userid);
            sc.Parameters.AddWithValue("@empid", empid);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];


        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
    }
    public DataTable ExecuteGetFlowID(string TableName, string KeyID)//Execute The Store Get Flow ID
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            string MaxDate = System.DateTime.Now.ToString("yyyyMMdd");
            SqlCommand sc = new SqlCommand();
            sc.CommandText = "usy_UserSelfGetFlowID";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@ID", KeyID);
            sc.Parameters.AddWithValue("@TableName", TableName);
            sc.Parameters.AddWithValue("@MaxDate", MaxDate);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }

    }
    public DataTable ExecuteGetItemNO(string TableName, string KeyID,string KeyIDName)//Execute The Store Get ItemNO
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            SqlCommand sc = new SqlCommand();
            sc.CommandText = "usy_UserSelfGetItemNO";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@ID", KeyID);
            sc.Parameters.AddWithValue("@TableName", TableName);
            sc.Parameters.AddWithValue("@IDName", KeyIDName);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
    }
    public DataTable ExecuteStoreTable(string strStroe, string likeID)//存儲過程無參數-獲取
    {
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = strStroe;
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@ID", likeID);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }

    }
    
    public DataSet ExcelToDS(string Path, String TableName)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            string strExcel = "";
            SqlDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [Sheet1$]";
            myCommand = new SqlDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, TableName);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }


    public bool CheckTextbox(string strTB)
    {
        bool flag = false;
        if (strTB.Contains(" "))
            flag = true;
        else if (strTB.Contains("'"))
            flag = true;
        else if (strTB.Contains("\\"))
            flag = true;
        else if (strTB.Contains("\""))
            flag = true;
        else if (strTB.Contains("$"))
            flag = true;
        else if (strTB.Contains(","))
            flag = true;
        else if (strTB.Contains(":"))
            flag = true;
        else if (strTB.Contains("."))
            flag = true;
        else if (strTB.Contains("!"))
            flag = true;
        else if (strTB.Contains("^"))
            flag = true;
        //new string str[10] = {"'",";",","," ","@","/","\"","\\","(",")","!"};
        //strTB.Contains(
        flag = !flag;
        return flag;
    }

}
