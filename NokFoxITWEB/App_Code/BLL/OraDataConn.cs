﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Collections;


    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) By Anda
    /// 2009-12-21
    /// </summary>
public abstract class OraDataConn  //namespace Economy.BLL  //
{
    //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		

    public static string connectionString = ConfigurationManager.AppSettings["DefaultConnectionString"];
    public static string connectionString1 = ConfigurationManager.AppSettings["TybakConnectionString"];
    public static string connectionString2 = ConfigurationManager.AppSettings["NormalConnectionString"];
    public static string StandBy211 = ConfigurationManager.AppSettings["L8StandByConnectionString"]; //ConnectionString"]; //LocDBPath;

    // public OraDataConn()
    // {
    // }

    /// <summary>
    /// 數據庫是否連接成功
    /// </summary>
    /// <returns></returns>
    public static bool OraIsConnectSuccess()
    {
        if (connectionString == string.Empty || connectionString == "")
        {
            return false;
        }

        bool b = false;
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                b = true;
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                b = false;
            }

        }

        return b;
    }


    #region 公用方法

    /// <summary>
    /// 獲取字段的的總和,根據條碼
    /// </summary>
    /// <param name="FieldName">字段</param>
    /// <param name="TableName">表名</param>
    /// <returns>該字段的最大值</returns>


    public static object OraGetObject(string strSql)
    {
        return GetSingle(strSql);
    }

   

    


    /// <summary>
    /// 判斷記錄是否有存在,查詢結果是否有記錄
    /// </summary>
    /// <param name="strSql">SQL語句</param>
    /// <returns></returns>
    public static bool OraExists(string strSql)
    {
        object obj = GetSingle(strSql);
        int cmdresult;
        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
        {
            cmdresult = 0;
        }
        else
        {
            cmdresult = int.Parse(obj.ToString());
        }
        if (cmdresult == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 判斷記錄是否有存在,查詢結果是否有記錄
    /// </summary>
    /// <param name="strSql">SQL語句</param>
    /// <param name="cmdParms">存儲過程參數</param>
    /// <returns></returns>
    public static bool OraExists(string strSql, params OracleParameter[] cmdParms)
    {
        object obj = GetSingle(strSql, cmdParms);
        int cmdresult;
        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
        {
            cmdresult = 0;
        }
        else
        {
            cmdresult = int.Parse(obj.ToString());
        }
        if (cmdresult == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    #endregion

    #region  执行简单SQL语句

    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public static int OraExecuteSql(string SQLString)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            using (OracleCommand cmd = new OracleCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.OracleClient.OracleException E)
                {
                    connection.Close();
                    throw new Exception(E.Message);

                }
            }
        }
    }

    public DataSet GetOraDataByPara(string Fstr1, string RunCDBPath)
    {
        string SQLString = Fstr1;
        string RunStr = RunCDBPath;
        using (OracleConnection connection = new OracleConnection(RunStr))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
            }
            catch (System.Data.OracleClient.OracleException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }

        // string strsql = @"select * from TESTINFO.TESTINFO_HEAD where create_date > sysdate-1 and rownum <= 10 order by create_date desc"; //
        //string strsql = Fstr1;
        //return OracleQuery(strsql, RunCDBPath );

    }
    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public static int OraExcuteSqlNew(string SQLString)
    {
        using (OracleConnection connection = new OracleConnection(connectionString1))
        {
            using (OracleCommand cmd = new OracleCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    connection.Close();
                    throw new Exception(ex.Message);
                }
            }
        }
    }


    /// <summary>
    /// 执行多条SQL语句，实现数据库事务。
    /// </summary>
    /// <param name="SQLStringList">多条SQL语句</param>		
    public static void OraExecuteSqlTran(ArrayList SQLStringList)
    {
        using (OracleConnection conn = new OracleConnection(connectionString))
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleTransaction tx = conn.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
        }
    }

    /// <summary>
    /// 执行带一个存储过程参数的的SQL语句。
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
    /// <returns>影响的记录数</returns>
    public static int OraExecuteSql(string SQLString, string content)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            OracleCommand cmd = new OracleCommand(SQLString, connection);
            System.Data.OracleClient.OracleParameter myParameter = new System.Data.OracleClient.OracleParameter("@content", OracleType.NVarChar);
            myParameter.Value = content;
            cmd.Parameters.Add(myParameter);
            try
            {
                connection.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }


    /// <summary>
    /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
    /// </summary>
    /// <param name="strSQL">SQL语句</param>
    /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
    /// <returns>影响的记录数</returns>
    public static int OraExecuteSqlInsertImg(string strSQL, byte[] fs)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            OracleCommand cmd = new OracleCommand(strSQL, connection);
            System.Data.OracleClient.OracleParameter myParameter = new System.Data.OracleClient.OracleParameter("@fs", OracleType.LongRaw);
            myParameter.Value = fs;
            cmd.Parameters.Add(myParameter);
            try
            {
                connection.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }

    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    private static object GetSingle(string SQLString)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            using (OracleCommand cmd = new OracleCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.OracleClient.OracleException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
            }
        }
    }
    /// <summary>
    /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>OracleDataReader</returns>
    public static OracleDataReader OraExecuteReader(string strSQL)
    {
        OracleConnection connection = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(strSQL, connection);
        try
        {
            connection.Open();
            OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return myReader;
        }
        catch (System.Data.OracleClient.OracleException e)
        {
            throw new Exception(e.Message);

        }

    }
    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataSet</returns>
    public static DataSet OraQuery(string SQLString)
    {
        // using (OracleConnection connection = new OracleConnection( connectionString)) //
        using (OracleConnection connection = new OracleConnection(StandBy211 ))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
            }
            catch (System.Data.OracleClient.OracleException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }
    }

    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataSet</returns>
    public static DataSet OraQueryNew(string SQLString)
    {
        using (OracleConnection connection = new OracleConnection(connectionString1))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
            }
            catch (System.Data.OracleClient.OracleException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }
    }



    #endregion

    #region 执行带参数的SQL语句

    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public static int OraExecuteSql(string SQLString, params OracleParameter[] cmdParms)
    {
        using (OracleConnection connection = new OracleConnection(connectionString1))
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch (System.Data.OracleClient.OracleException E)
                {
                    throw new Exception(E.Message);
                }
            }
        }
    }

    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public static int OraExecuteSqlTONORMAL(string SQLString, params OracleParameter[] cmdParms)
    {
        using (OracleConnection connection = new OracleConnection(connectionString2))
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch (System.Data.OracleClient.OracleException E)
                {
                    throw new Exception(E.Message);
                }
            }
        }
    }

    /// <summary>
    /// 执行多条SQL语句，实现数据库事务。
    /// </summary>
    /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OracleParameter[]）</param>
    public static void OraExecuteSqlTran(Hashtable SQLStringList)
    {
        using (OracleConnection conn = new OracleConnection(connectionString))
        {
            conn.Open();
            using (OracleTransaction trans = conn.BeginTransaction())
            {
                OracleCommand cmd = new OracleCommand();
                try
                {
                    //循环
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        string cmdText = myDE.Key.ToString();
                        OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
                        PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        trans.Commit();
                    }
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }


    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    private static object GetSingle(string SQLString, params OracleParameter[] cmdParms)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.OracleClient.OracleException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }

    /// <summary>
    /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>OracleDataReader</returns>
    public static OracleDataReader OraExecuteReader(string SQLString, params OracleParameter[] cmdParms)
    {
        OracleConnection connection = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand();
        try
        {
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return myReader;
        }
        catch (System.Data.OracleClient.OracleException e)
        {
            throw new Exception(e.Message);
        }

    }

    /// <summary>
    /// 不同數據庫連接
    /// </summary>
    /// <param name="SQLString"></param>
    /// <param name="cmdParms"></param>
    /// <returns></returns>
    public static OracleDataReader OraExecuteReaderNew(string SQLString, params OracleParameter[] cmdParms)
    {
        OracleConnection connection = new OracleConnection(connectionString1);
        OracleCommand cmd = new OracleCommand();
        try
        {
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return myReader;
        }
        catch (System.Data.OracleClient.OracleException e)
        {
            throw new Exception(e.Message);
        }

    }

  
    #endregion

    #region 存储过程操作

    /// <summary>
    /// 执行存储过程 返回SqlDataReader ( 注意：调用该方法后，一定要对DataReader进行Close )
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>OracleDataReader</returns>
    public static OracleDataReader OraRunProcedure(string storedProcName, IDataParameter[] parameters)
    {
        OracleConnection connection = new OracleConnection(connectionString);
        OracleDataReader returnReader;
        connection.Open();
        OracleCommand command = OraBuildQueryCommand(connection, storedProcName, parameters);
        command.CommandType = CommandType.StoredProcedure;
        returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
        return returnReader;
    }


    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="tableName">DataSet结果中的表名</param>
    /// <returns>DataSet</returns>
    public static DataSet OraRunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            DataSet dataSet = new DataSet();
            connection.Open();
            OracleDataAdapter sqlDA = new OracleDataAdapter();
            sqlDA.SelectCommand = OraBuildQueryCommand(connection, storedProcName, parameters);
            sqlDA.Fill(dataSet, tableName);
            connection.Close();
            return dataSet;
        }
    }


    /// <summary>
    /// 构建 OracleCommand 对象(用来返回一个结果集，而不是一个整数值)
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <returns>OracleCommand</returns>
    private static OracleCommand OraBuildQueryCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)
    {
        OracleCommand command = new OracleCommand(storedProcName, connection);
        command.CommandType = CommandType.StoredProcedure;
        foreach (OracleParameter parameter in parameters)
        {
            command.Parameters.Add(parameter);
        }
        return command;
    }

    /// <summary>
    /// 执行存储过程，返回影响的行数		
    /// </summary>
    /// <param name="storedProcName">存储过程名</param>
    /// <param name="parameters">存储过程参数</param>
    /// <param name="rowsAffected">影响的行数</param>
      
    #endregion

    private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        if (trans != null)
            cmd.Transaction = trans;
        cmd.CommandType = CommandType.Text;//cmdType;
        if (cmdParms != null)
        {
            foreach (OracleParameter parm in cmdParms)
                cmd.Parameters.Add(parm);
        }
    }

    public static DataView OraQuery()
    {
        throw new NotImplementedException();
    }

    public static void OraExcuteSqlNew(string sqlinstert, OracleParameter[] op)
    {
        throw new NotImplementedException();
    }

    public static DataSet OraExeQueryByPara(string SQLString, string RunStr)
    {
        // using (OracleConnection connection = new OracleConnection(connectionString)) //
        using (OracleConnection connection = new OracleConnection(RunStr))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
            }
            catch (System.Data.OracleClient.OracleException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }
    }
       
}