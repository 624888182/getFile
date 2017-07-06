using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;


using System.Collections;
/// <summary>
/// DbAccess2 的摘要说明
/// </summary>
public class DbAccessingLmisExcel
{
    public SqlConnection dbConn;
    DbAccessing db = new DbAccessing();

    public DbAccessingLmisExcel()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        setConn();
	}

    protected virtual void setConn()
    {
        string strConn = "Data Source=10.162.130.114;Initial Catalog=lmis;Persist Security Info=True;User ID=lmis;Password=123456";
        //strConn = System.Configuration.ConfigurationManager.AppSettings["ConnectionTruckSqlServer"].ToString();
        //dbConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionTruckSqlServer"]);
        dbConn = new SqlConnection(strConn);
    }

    public DataTable ExecuteSqlTable(string strSQL)
    {
        try
        {
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
    public bool CheckTwoTextboxTime(string tbStartDateTime, string tbEndDateTime)
    {
        try
        {
            if (System.Convert.ToDateTime(tbStartDateTime) <= System.Convert.ToDateTime(tbEndDateTime))
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
        
    }
    public bool CheckTwoTextboxTimeUtter(string tbStartDateTime, string tbEndDateTime)
    {
        try
        {
            if (System.Convert.ToDateTime(tbStartDateTime) < System.Convert.ToDateTime(tbEndDateTime))
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }

    }
    public bool CheckTextboxTime(string tbDateTime)
    {
        try
        {
            System.Convert.ToDateTime(tbDateTime);
            return true;
        }
        catch
        {
            return false;
        }
        
    }
    public  bool dlstBound(DropDownList list, string sql)
    {
        try
        {
           // DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = db.ExecuteSqlTable(sql);
            list.Items.Clear();
            list.Items.Add(new ListItem("", ""));
            foreach (DataRow dr in tb.Rows)
            {
                list.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    //實現從號碼進行累加
    public string GetDetailNumber(string number, int start, int lenght,string sql)
    {
        //sql = "select max(BillListNo) as rowNo from D_TrunkCostList where BillNo='" + number + "'";
        DataTable dt = db.ExecuteSqlTable(sql);
        string row = "";
        string[] zero = new string[lenght];
        string z = "0";
        for (int i = 0; i < lenght; i++)
        {
            if (i == 0)
                zero[i] = "";
            else
                zero[i] = zero[i - 1] + z;
        }
        foreach (DataRow dr in dt.Rows)
        {
            if (dr[0].ToString().Length == 0)
                row = zero[lenght - 1] + "1";
            else
            {

                string rowstr = dr["rowNo"].ToString();
                rowstr = rowstr.Substring(start, lenght);
                string rowNo = System.Convert.ToString(System.Convert.ToUInt32(rowstr) + 1);

                row = zero[lenght - rowNo.Length] + rowNo.ToString();
                //if (rowNo.Length < 2)
                //    row = "000" + rowNo.ToString();
                //else if (rowNo.Length < 3)
                //    row = "00" + rowNo.ToString();
                //else if (rowNo.Length < 4)
                //    row = "0" + rowNo.ToString();
                //else
                //    row = rowNo.ToString();
            }
            row = number.Substring(0,start) + row;
        }
        return row;
    }

    //實現從號碼進行累加
    public string GetDetailNumber(string number, int start, int lenght)
    {     
        string row = "";
        string[] zero = new string[lenght];
        string z = "0";
        for (int i = 0; i < lenght; i++)
        {
            if (i == 0)
                zero[i] = "";
            else
                zero[i] = zero[i - 1] + z;
        }
        try
        {
            row = number.Substring(start, lenght);
        }
        catch
        {}
        if (row.Length == 0)
                row = zero[lenght - 1] + "1";
            else
            {

                string rowstr = number.ToString();
                rowstr = rowstr.Substring(start, lenght);
                string rowNo = System.Convert.ToString(System.Convert.ToUInt32(rowstr) + 1);

                row = zero[lenght - rowNo.Length] + rowNo.ToString();

            }
            row = number.Substring(0, start) + row;
        
        return row;
    }
    //從卡車系統導入數據操作(表的結構及類型必須相同)
    public bool  ExecuteTabToTab(string dataTable, string sqlGetData,string sqlDelAll)
    {
        string strSql = "";
        try
        {
            dbConn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlGetData, dbConn);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            int i = 0;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                i++;
            }
            string strS = "";
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                strSql = " insert into " + dataTable + " values(";// +dr[0].ToString() + "," + dr[1].ToString() + ")";
                for (int j = 0; j < i; j++)
                {
                    strSql += "'"+dr[j].ToString();
                    if (j != (i - 1))
                    {
                        strSql += "',";
                    }
                    else
                        strSql += "')";
                }
                //return strSql;
                strS += strSql;
            }
            string strOperType = "導入";
            string strModuleName = "從卡車系統導入數據操作";
            strSql = sqlDelAll;
            strSql += strS;
            db.ExecuteSql(strSql, strOperType, strModuleName);
            
            return true;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            dbConn.Close();
        }
        //return strSql;
    }
    //從卡車系統導入數據操作(表的結構及類型必須相同)
    public bool ExecuteTabToTabArray(string dataTable, string sqlGetData, string sqlDelAll)
    {
        string strSql = "",strSqls = "";
        try
        {
            
            dbConn.Open();
            
            
            SqlDataAdapter sda = new SqlDataAdapter(sqlGetData, dbConn);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            int i = 0;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                i++;
            }
            ArrayList arrS = new ArrayList();
            int k =0,l=0;

            string strOperType = "導入";
            string strModuleName = "從卡車系統導入數據操作";
            sqlDelAll = GetReportSql(sqlDelAll, strOperType, strModuleName);
            arrS.Add(sqlDelAll);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strSql = " insert into " + dataTable + " values(";// +dr[0].ToString() + "," + dr[1].ToString() + ")";
                for (int j = 0; j < i; j++)
                {
                    strSql += "'" + dr[j].ToString();
                    if (j != (i - 1))
                    {
                        strSql += "',";
                    }
                    else
                        strSql += "')";
                }
                k++;
                if (k % 50 == 0)
                {
                    l++;
                    strSqls += strSql;
                }
                else
                {
                    strSqls += strSql;
                    arrS.Add(strSqls);
                    strSqls = "";
                }
                
                
            }
            try
            {
                dbConn.Close();
            }
            catch
            { }




            string strConn = "";// "Data Source=10.162.130.114;Initial Catalog=COSTAPP;Persist Security Info=True;User ID=costapp;Password=costapp";
            strConn = System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"].ToString();
            SqlConnection dbConnCost = new SqlConnection(strConn);
            dbConnCost.Open();
            SqlTransaction Trans = dbConnCost.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = dbConnCost;
                cmd.Transaction = Trans;
                
                for (i = 0; i < arrS.Count; i++)
                {
                    cmd.CommandText = arrS[i].ToString();
                    cmd.ExecuteNonQuery();
                    //db.ExecuteSql(strS[i].ToString(), strOperType, strModuleName);         
                }
                Trans.Commit();
            }
            catch
            {
                
                Trans.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
                dbConnCost.Close();
            }
            return true;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        
        //return strSql;
    }
    //從卡車系統導入數據操作(表的結構及類型必須相同)
    public bool ExecuteTabToTabArrayNew(string dataTable, string sqlGetData, string sqlDelAll)
    {
        string strSql = "", strSqls = "";
        try
        {

            dbConn.Open();


            SqlDataAdapter sda = new SqlDataAdapter(sqlGetData, dbConn);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            int i = 0;
            //foreach (DataColumn dc in ds.Tables[0].Columns)
            //{
            //    i++;
            //}
            i = ds.Tables[0].Columns.Count;
            ArrayList arrS = new ArrayList();
            int k = 0, l = 0;

            string strOperType = "導入";
            string strModuleName = "從卡車系統導入數據操作";
            sqlDelAll = GetReportSql(sqlDelAll, strOperType, strModuleName);
            arrS.Add(sqlDelAll);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strSql = " insert into " + dataTable + " values(";// +dr[0].ToString() + "," + dr[1].ToString() + ")";
                for (int j = 0; j < i; j++)
                {
                    strSql += "'" + dr[j].ToString();
                    if (j != (i - 1))
                    {
                        strSql += "',";
                    }
                    else
                        strSql += "')";
                }
                k++;
                if (strSqls.Length < 600)//SQL長8000bit,ArrayList長小于8000
                //if (k % 50 == 0)
                {
                    l++;
                    strSqls += strSql;
                }
                else
                {
                    strSqls += strSql;
                    arrS.Add(strSqls);
                    strSqls = "";
                }


            }
            try
            {
                dbConn.Close();
            }
            catch
            { }




            string strConn = "Data Source=10.162.130.114;Initial Catalog=COSTAPP;Persist Security Info=True;User ID=costapp;Password=costapp";
            strConn = System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"].ToString();
            SqlConnection dbConnCost = new SqlConnection(strConn);
            dbConnCost.Open();
            SqlTransaction Trans = dbConnCost.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = dbConnCost;
                cmd.Transaction = Trans;

                for (i = 0; i < arrS.Count; i++)
                {
                    cmd.CommandText = arrS[i].ToString();
                    cmd.ExecuteNonQuery();
                    //db.ExecuteSql(strS[i].ToString(), strOperType, strModuleName);         
                }
                Trans.Commit();
            }
            catch
            {

                Trans.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
                dbConnCost.Close();
            }
            return true;
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }

        //return strSql;
    }

    public string GetReportSql(string strSQL, string strOperType, string strModuleName)
    {
        try
        {
            //save the operate log
            string sql = "Insert into R_SystemLog(UserID,OperTime,OperType,ModuleName,OperSQL) Values('"
                + System.Web.HttpContext.Current.Session["loginUser"].ToString() + "',GetDate()" + ",'" + strOperType
                + "','" + strModuleName + "','" + strSQL.ToString().Replace("'", "''") + "')";

            strSQL += sql;
        }
        catch
        {}
        return strSQL;
    }

    public bool CheckTextbox(string strTB)
    {
        bool flag = false;
        if (strTB.Contains(" "))
            flag = true;
        else if(strTB.Contains("'"))
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
