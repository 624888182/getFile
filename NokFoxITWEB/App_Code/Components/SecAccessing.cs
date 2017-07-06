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
using FIH.Security.db;

/// <summary>
/// SecAccessing 的摘要说明

/// </summary>
public class SecAccessing
{
    public SqlConnection dbConn;

	public SecAccessing()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        setConn();
	}

    protected virtual void setConn()
    {
        dbConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"]);
    }

    public DataReader ExecuteStoreReader(string strStroe)//
    {
        #region 存儲過程無參數-獲取DataReader
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
        #endregion
    }

    public DataTable ExecuteStoreTable(string strStroe)//
    {
        #region 存儲過程無參數-獲取DataTable
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
        #endregion
    }

    //----------------獲取權限設置-----------------

    public DataTable GetTreeTable(string ModuleName)//
    {
        #region 獲取權限設置Tree
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = "usp_usySysModuleGetTrees";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@ModuleName", ModuleName);
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
        #endregion
    }
    
    public DataTable GetRoleCode(string OperRoleGpCode,string ModuleName)//
    {
        #region 根據節點ID和當前選擇的權限組代碼 得到所有操作權限Detail的集合

        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = "usp_usyOperRoleDetailGetRoleCode";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@OperRoleGpCode", OperRoleGpCode);
            sc.Parameters.AddWithValue("@ModuleCode", ModuleName);
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
        #endregion
    }
    //--------------獲取權限----------------

    public bool HasOperPermissionBool(string OperRoleGpCode, string ModuleCode, string OperCode)//
    {
        #region 獲取權限Bool
        bool flag = false;
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = "usp_usyOperRoleDetailHasOperPermissionBool";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@OperRoleGpCode", OperRoleGpCode);
            sc.Parameters.AddWithValue("@ModuleCode", ModuleCode);
            sc.Parameters.AddWithValue("@OperCode", OperCode);
            IDataReader dr = sc.ExecuteReader();
            if (dr.Read())
            {
                flag = true;
            }
            return flag;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
            
        }
        #endregion
    }

    public string HasOperPermissionString(string OperRoleGpCode, string ModuleCode, string OperCode)//
    {
        #region 獲取權限String
        string strValue = "";
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = "usp_usyOperRoleDetailHasOperPermissionString";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@OperRoleGpCode", OperRoleGpCode);
            sc.Parameters.AddWithValue("@ModuleCode", ModuleCode);
            sc.Parameters.AddWithValue("@OperCode", OperCode);
            IDataReader dr = sc.ExecuteReader();
            if (dr.Read())
            {
                if (!dr.IsDBNull(2)) strValue = dr.GetString(2); ;
            }
            return strValue;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
        #endregion
    }

    public string HasDeptPermissionString(string DeptRoleGpCode, string DivNo)//
    {
        #region 獲取權限String
        string strValue = "";
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            if (DeptRoleGpCode != "SuperManager")
            {
                sc.CommandText = "usp_usyDeptRoleGpHasDeptPermissionString";
                sc.Parameters.AddWithValue("@DeptRoleGpCode", DeptRoleGpCode);
               // sc.Parameters.AddWithValue("@DivNo", DivNo);
            }
            else
            {
                sc.CommandText = "usp_usyDeptRoleGpHasDeptPermissionAll";
                sc.Parameters.AddWithValue("@DeptRoleGpCode", DeptRoleGpCode);
            }
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;

            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                strValue += dr[1].ToString();
            }
            return strValue;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
        #endregion
    }



    //-------------得主頁面的到Left Tree---------------

    public DataTable GetMainLeftTree(string OperRoleGpCode, string parentmodulecode)//
    {
        #region 得主頁面的到Left Tree
        try
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();

            SqlCommand sc = new SqlCommand();
            sc.CommandText = "usp_usySysModuleGetMainLeftTree";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = dbConn;
            sc.Parameters.AddWithValue("@OperRoleGpCode", OperRoleGpCode);
            sc.Parameters.AddWithValue("@parentmodulecode", parentmodulecode);
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
        #endregion
    }
}
