 


using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Data;
  using System.Data.Odbc;
 
 
      public class Odbc : System.Web.UI.Page
    {

 private string _ConnectionString = "";
  
          private string _CommandText = "";
  
          private OdbcConnection Conn = null;
  
          private DataSet _DSAFDataSet = null;
 
          public string ConnectionString
          { 
              get { return this._ConnectionString; }
              set { this._ConnectionString = value; }
          }
 
          public string CommandText 
          {   get { return this._CommandText; }
              set { this._CommandText = value; } 
          }
  
          public Odbc()
          {
              this._DSAFDataSet = new DataSet();
          }
 
          public DataSet Execute()
          {
              if (string.IsNullOrEmpty(_ConnectionString)) { throw (new Exception("无传入数据连接字串")); }
  
              if (string.IsNullOrEmpty(_CommandText)) { throw (new Exception("无传入SQL语句")); }
  
              if (!DSAFConnectionTest()) { throw (new Exception("尝试数据库连接失败")); }
  
              try
              {
                  Conn = new OdbcConnection(); 
                  Conn.ConnectionString = _ConnectionString;
                // Conn.Open();
                  OdbcDataAdapter odbcDa = new OdbcDataAdapter();
  
                odbcDa.SelectCommand.CommandText = _CommandText;
                odbcDa.SelectCommand.Connection = Conn;
                //OracleCommand cmd = new OracleCommand(sql, conn);
                


                  odbcDa.Fill(_DSAFDataSet); odbcDa.Dispose();
              }
             //  catch (OdbcException odbcEx) { throw odbcEx; }
             catch (Exception ex) { throw ex; }
  
              return _DSAFDataSet;
          }


          public DataTable execsql( )
          {
              //_CommandText string sql = "select LOGIN_NAME 登錄名稱,LOGIN_PASS 登錄密碼,USER_NAME 姓名,USER_DEPT 部門,USER_POPEDOM 用戶權限,UPDATE_TIME 更新時間,UPDATE_USER 更新管理員 from tj_sfc_users";
   
 
              Conn = new OdbcConnection();
              Conn.ConnectionString = _ConnectionString;
              OdbcCommand cmd = new OdbcCommand(_CommandText, Conn);


              OdbcDataAdapter oda = new OdbcDataAdapter();
              try
              {
                  Conn.Open();
                  oda.SelectCommand = cmd;
                  DataTable dt = new DataTable();
                  oda.Fill(dt);
                  return dt;
              }

              catch (Exception ex)
              {
                  throw ex; 
                 // ex.Message;
                  return null;
              }
              finally
              {
                  Conn.Close();
              }
          }


          public int ExecuteNonQuery()
          {
              if (string.IsNullOrEmpty(_ConnectionString)) { throw (new Exception("无传入数据连接字串")); }
  
              if (string.IsNullOrEmpty(_CommandText)) { throw (new Exception("无传入SQL语句")); }
  
              if (!DSAFConnectionTest()) { throw (new Exception("尝试数据库连接失败")); }
  
              int iQuery = 0; OdbcTransaction Transaction = null;
  
              try
              {
                  OdbcCommand Comm = new OdbcCommand(); 
                        Conn = new OdbcConnection();
  
                  Conn.ConnectionString = _ConnectionString; 
                        Comm.Connection = Conn; 
                        Comm.CommandText = _CommandText;
  
                  Transaction = Conn.BeginTransaction(); 
                        Comm.Transaction = Transaction;
  
                  iQuery = Comm.ExecuteNonQuery(); 
                        Transaction.Commit();
                         return iQuery;
              }
              catch (OdbcException odbcEx) { Transaction.Rollback(); throw odbcEx; }
              catch (Exception ex) { throw ex; }
              finally { Transaction.Dispose(); Conn.Close(); Conn.Dispose(); }
          }
  
          public object ExecuteScalar()
          {
              if (string.IsNullOrEmpty(_ConnectionString)) { throw (new Exception("无传入数据连接字串")); }
  
              if (string.IsNullOrEmpty(_CommandText)) { throw (new Exception("无传入SQL语句")); }
  
              if (!DSAFConnectionTest()) { throw (new Exception("尝试数据库连接失败")); }
  
              object iQuery = null; 
                  OdbcTransaction Transaction = null;
  
             try
             {
                 OdbcCommand Comm = new OdbcCommand();
                         Conn = new OdbcConnection();
 
                 Conn.ConnectionString = _ConnectionString; 
                        Comm.Connection = Conn; 
                        Comm.CommandText = _CommandText;
 
                 Transaction = Conn.BeginTransaction(); 
                        Comm.Transaction = Transaction;
 
                 iQuery = Comm.ExecuteScalar(); 
                        Transaction.Commit(); 
                        return iQuery;
             }
             catch (OdbcException odbcEx) { Transaction.Rollback(); throw odbcEx; }
             catch (Exception ex) { throw ex; }
             finally { Transaction.Dispose(); Conn.Close(); Conn.Dispose(); }
         }
 
         private bool DSAFConnectionTest()
         {
             try
             {
                 Conn = new OdbcConnection(); 
                        Conn.ConnectionString = _ConnectionString;
 
                 Conn.Open(); 
                        Conn.Close();
             }
             catch { return (false); }
 
             return (true);
         }
 

         public void Dispose()
         {
             if (Conn != null) { this.Conn.Dispose(); }
 
             if (_DSAFDataSet != null) { this._DSAFDataSet = null; }
         }
    }
 




 