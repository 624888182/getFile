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
using EconomyUser;

namespace Economy.Publibrary
{
    /// <summary>
    /// Summary description for DataConn
    /// </summary>    
    
    public class DataConnlib
    {
        public static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];

        private SqlConnection sconn=null;
        public DataConnlib()
        {
            //
            // TODO: Add constructor logic here
            //
            CreatSQL();
        }
        //數據庫連接

        public void CreatSQL()
        {
            try
            {
               string connString =ConfigurationManager.AppSettings["DefaultConnectionString"];
               sconn = new SqlConnection(connString);
                sconn.Open();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConn()
        {
            try
            {
                sconn.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //寫入/更新數據
        public bool GetDataBySQL(string sql)
        {
            try
            {
                SqlCommand scmm = new SqlCommand();
                scmm.Connection = sconn;
                scmm.CommandType = CommandType.Text;
                scmm.CommandText = sql;
                //scmm.CommandTimeout = 300;
                scmm.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //調用存出過程
        public bool GetDataTableByCmd(string storedProcName, IDataParameter[] parameters)
        {
            try
            {
                SqlCommand scmd = sconn.CreateCommand();
                //scmd.Connection =sconn;
                scmd.CommandType = CommandType.StoredProcedure;
                scmd.CommandText = storedProcName;
                //scmd.CommandTimeout = 300;
                scmd.ExecuteNonQuery();
               // scmd.ExecuteReader();
                //scmd.ExecuteScalar();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SqlCommand CreateCmd()
        {
            try
            {
                return sconn.CreateCommand();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //判斷用戶是否存在
        public static bool Exist(string user)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                scnn.Open();
                string sql = "select count(1) from Users where UserName='" + user + "'";
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }

        public static DataSet GET_User(string id)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);
            DataSet ds = new DataSet();
            try
            {
                scnn.Open();
                string sql = "select * from Users where userid='" + id + "'";
                SqlCommand scmm = new SqlCommand(sql, scnn);
                SqlDataAdapter da = new SqlDataAdapter(scmm);
                da.Fill(ds);
                return ds;            
                
            }
            catch
            {
                return ds;
            }
            finally
            {
                scnn.Close();
            }
        }

        public static bool Excute(string sql)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                scnn.Open();
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }

        //獲取用戶權限信息
        public static DataSet Get_user(string sql)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);
            
            DataSet ds = new DataSet();
            try
            {
                scnn.Open();
                SqlCommand scmm = new SqlCommand(sql, scnn);

                SqlDataAdapter sdapter = new SqlDataAdapter(scmm);
                
                sdapter.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                scnn.Close();
            }

 
        }
        //登錄事件
        public static User Login(string user, string pass)
        {
           
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            User users = new User();
            
            try
            {
                scnn.Open();
                string sql = "select * from Users where UserName='" + user + "' and Password='" + pass + "'";
                SqlCommand scmm = new SqlCommand(sql, scnn);

                SqlDataAdapter sdapter = new SqlDataAdapter(scmm);
                DataSet ds = new DataSet();
                sdapter.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {                    
                    users.UserId = ds.Tables[0].Rows[0]["UserID"].ToString();
                    users.UserBU = ds.Tables[0].Rows[0]["UserBU"].ToString();
                    users.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                    users.Password = ds.Tables[0].Rows[0]["password"].ToString();
                    users.CardID = ds.Tables[0].Rows[0]["CardID"].ToString();
                    users.LastEditBy = ds.Tables[0].Rows[0]["LastEditBy"].ToString();
                    users.UserGroup = ds.Tables[0].Rows[0]["UserGroup"].ToString();
                    return users;
                }
                else
                {
                    return users; 
                }
            }
            catch
            {
                return users;
            }
            finally
            {
                scnn.Close();
            }
        }


        public static bool AccExist(string account_Id, string butype)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (butype == "NLV")
                {
                    sql = "select count(1) from MPE_Account_Item where Account_Id='" + account_Id + "'";
                }
                else if (butype == "RIM")
                {
                    sql = "select count(1) from Around_Account_Item where Account_Id='" + account_Id + "'";

                }
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }
        public static bool AcctOriginalExist(string currentMonth, string butype)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (butype == "NLV")
                {
                    sql = "select count(1) from MPE_Account_Original where CurrentMonth='" + currentMonth + "'";

                }
                else if (butype == "RIM")
                {
                    sql = "select count(1) from Around_Account_Original where CurrentMonth='" + currentMonth + "'";

                }
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }
        public static bool AccCollectExist(int accyear, string accmonth, string butype)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (butype == "NLV")
                {
                    sql = "select count(1) from MPE_Account where Year='" + accyear + "' and Month='" + accmonth + "'";
                }
                else if (butype == "FKD")
                {
                    sql = "select count(1) from FKD_Account where Year='" + accyear + "' and Month='" + accmonth + "'";
                }
                else if (butype == "RIM")
                {
                    sql = "select count(1) from Around_Account where Year='" + accyear + "' and Month='" + accmonth + "'";
                }
                else if (butype == "MPB")
                {
                    sql = "select count(1) from MPB_Account_Original where Year='" + accyear + "' and Month='" + accmonth + "'";
                }
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }
        public static bool IncExist(string itemCode, string incYear, string incMonth, string butype)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (butype == "NLV")
                {
                    sql = "select count(1) from MPE_Incr_Decr where ItemCode='" + itemCode + "' and Year='" + incYear + "' and Month='" + incMonth + "'";
                }
                else if (butype == "FKD")
                {
                    sql = "select count(1) from FKD_Incr_Decr where ItemCode='" + itemCode + "' and Year='" + incYear + "' and Month='" + incMonth + "'";
                }
                else if (butype == "MPB")
                {
                    sql = "select count(1) from MPB_Incr_Decr where ItemCode='" + itemCode + "' and Year='" + incYear + "' and Month='" + incMonth + "'";
                }
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }
        public static bool IncYearExist(string BU, string inMonth, int inYear, string incType)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (incType == "NLV")
                {
                    sql = "select count(1) from MPE_Incr_DecrACT where BUName='" + BU + "' and Year='" + inYear + "' and ACTofForecast='" + inMonth + "'";
                }
                else if (incType == "FKD")
                {
                    sql = "select count(1) from FKD_Incr_Decr_Forecast where BUName='" + BU + "' and  Year='" + inYear + "'";
                }
                else if (incType == "MPB")
                {
                    sql = "select count(1) from MPE_Incr_DecrACT where BUName='" + BU + "' and Year='" + inYear + "' and ACTofForecast='" + inMonth + "'";
                }

                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }
        public static bool StocksExist(int yearDate, string butype)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (butype == "NLV")
                {
                    sql = "select count(1) from MPE_Stocks_Original where YearDate='" + yearDate + "'";
                }
                else if (butype == "MPB")
                {
                    sql = "select count(1) from MPB_Stocks_Original where YearDate='" + yearDate + "'";
                }
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }
        public static bool StocksEndExist(string stocksWeek, int StockYear, string butype)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (butype == "NLV")
                {
                    sql = "select count(1) from MPE_Stocks where [Week]='" + stocksWeek + "' and Year='" + StockYear + "'";
                }
                else if (butype == "FKD")
                {
                    sql = "select count(1) from FKD_Stocks where [Week]='" + stocksWeek + "' and Year='" + StockYear + "'";

                }
                else if (butype == "MPB")
                {
                    sql = "select count(1) from MPB_Stocks where [Week]='" + stocksWeek + "' and Year='" + StockYear + "'";

                }
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }
        public static bool StocksDVExist(string stocksWeek, string butype)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);

            try
            {
                string sql = null;
                scnn.Open();
                if (butype == "NLV")
                {
                    sql = "select count(1) from MPE_DV where [Week]='" + stocksWeek + "'";
                }
                else if (butype == "MPB")
                {
                    sql = "select count(1) from MPB_DV where [Week]='" + stocksWeek + "'";
                }
                SqlCommand scmm = new SqlCommand(sql, scnn);

                int count = Convert.ToInt32(scmm.ExecuteScalar());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                scnn.Close();
            }
        }

        //獲取資料依送來參數送來 Sql  20100120  Ken Beijing 
        public static DataSet Get_InfoByPara(string sql)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            SqlConnection scnn = new SqlConnection(ConnString);
            
            DataSet ds = new DataSet();
            try
            {
                scnn.Open();
                SqlCommand scmm = new SqlCommand(sql, scnn);

                SqlDataAdapter sdapter = new SqlDataAdapter(scmm);
                sdapter.SelectCommand.CommandTimeout = 300;
                sdapter.Fill(ds);
                return ds;
            }
            // catch (Exception ex)
            // {
            //    throw ex;
            // }
            catch
            {
                return ds;
            }
            finally
            {
                scnn.Close();
            }
        }  // Get_InfoByPara end

              
        //// ////////////////////////////////////
        /// Bubble Sort 
        ////
        //public void BubbleSort(String sortcount, Sortarray )  
        //{
        //    
        //}
        // public static DataSet PGetDataByPara(string sql)
        public static DataSet GetDataByParaPath(string sql, string DataPath)
        {
            string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
            if (DataPath != "") ConnString = DataPath.ToString();

            SqlConnection scnn = new SqlConnection(ConnString);
            DataSet ds = new DataSet();
            try
            {
                scnn.Open();
                SqlCommand scmm = new SqlCommand(sql, scnn);

                SqlDataAdapter sdapter = new SqlDataAdapter(scmm);
                sdapter.SelectCommand.CommandTimeout = 300;
                sdapter.Fill(ds);
                return ds;
            }
            // catch (Exception ex)
            // {
            //    throw ex;
            // }
            catch
            {
                return ds;
            }
            finally
            {
                scnn.Close();
            }
        }  // PGetDataByPara end
    }
}