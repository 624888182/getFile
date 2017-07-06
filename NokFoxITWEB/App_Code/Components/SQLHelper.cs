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

//Design by james 2006/8/14
/// <summary>
/// SQLHelper 的摘要说明
/// </summary>

public class SystemError
{
    public static void SystemLog(string message)
    {
    }
}

public class SQLHelper
{

    // 连接数据源
    private SqlConnection myConnection;
    private readonly string RETURNVALUE = "RETURNVALUE";

    /// <summary>
    /// 打开数据库连接.
    /// </summary>
    private void Open()
    {
        // 打开数据库连接
        if (myConnection == null)
        {
            myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"]);
        }
        if (myConnection.State == ConnectionState.Closed)
        {
            try
            {
                ///打开数据库连接
                myConnection.Open();
            }
            catch (Exception ex)
            {
                SystemError.SystemLog(ex.Message);
            }
            finally
            {
                ///关闭已经打开的数据库连接				
            }
        }
    }


    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void Close()
    {
        ///判断连接是否已经创建
        if (myConnection != null)
        {
            ///判断连接的状态是否打开
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
    }


    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        // 确认连接是否已经关闭
        if (myConnection != null)
        {
            myConnection.Dispose();
            myConnection = null;
        }
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="procName">存储过程的名称</param>
    /// <returns>返回存储过程返回值</returns>
    public int RunProc(string procName)
    {
        SqlCommand cmd = CreateCommand(procName, null);
        try
        {
            ///执行存储过程
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ///记录错误日志
            SystemError.SystemLog(ex.Message);
        }
        ///关闭数据库的连接
        Close();

        ///返回存储过程的参数值
        return (int)cmd.Parameters[RETURNVALUE].Value;
    }


    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="procName">存储过程名称</param>
    /// <param name="prams">存储过程所需参数</param>
    /// <returns>返回存储过程返回值</returns>
    public int RunProc(string procName, SqlParameter[] prams)
    {
        SqlCommand cmd = CreateCommand(procName, prams);
        try
        {
            ///执行存储过程
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ///记录错误日志
            SystemError.SystemLog(ex.Message);
        }
        ///关闭数据库的连接
        Close();

        ///返回存储过程的参数值
        return (int)cmd.Parameters[RETURNVALUE].Value;
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="procName">存储过程的名称</param>
    /// <param name="dataReader">返回存储过程返回值</param>
    public void RunProc(string procName, out SqlDataReader dataReader)
    {
        ///创建Command
        SqlCommand cmd = CreateCommand(procName, null);

        ///读取数据
        dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="procName">存储过程的名称</param>
    /// <param name="prams">存储过程所需参数</param>
    /// <param name="dataReader">存储过程所需参数</param>
    public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
    {
        ///创建Command
        SqlCommand cmd = CreateCommand(procName, prams);

        ///读取数据
        dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// 创建一个SqlCommand对象以此来执行存储过程
    /// </summary>
    /// <param name="procName">存储过程的名称</param>
    /// <param name="prams">存储过程所需参数</param>
    /// <returns>返回SqlCommand对象</returns>
    private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
    {
        ///打开数据库连接
        Open();

        ///设置Command
        SqlCommand cmd = new SqlCommand(procName, myConnection);
        cmd.CommandType = CommandType.StoredProcedure;

        ///添加把存储过程的参数
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
            {
                cmd.Parameters.Add(parameter);
            }
        }

        ///添加返回参数ReturnValue
        cmd.Parameters.Add(
            new SqlParameter(RETURNVALUE, SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            false, 0, 0, string.Empty, DataRowVersion.Default, null));

        ///返回创建的SqlCommand对象
        return cmd;
    }

    /// <summary>
    /// 生成存储过程参数
    /// </summary>
    /// <param name="ParamName">存储过程名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <param name="Direction">参数方向</param>
    /// <param name="Value">参数值</param>
    /// <returns>新的 parameter 对象</returns>
    public SqlParameter CreateParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value, bool isNullable)
    {
        SqlParameter param;

        ///当参数大小为0时，不使用该参数大小值
        if (Size > 0)
        {
            param = new SqlParameter(ParamName, DbType, Size);
        }
        else
        {
            ///当参数大小为0时，不使用该参数大小值
            param = new SqlParameter(ParamName, DbType);
        }

        ///创建输出类型的参数
        param.Direction = Direction;
        if (!(Direction == ParameterDirection.Output && Value == null))
        {
            param.Value = Value;
        }

        param.IsNullable = isNullable;

        ///返回创建的参数
        return param;
    }

    /// <summary>
    /// 传入输入参数
    /// </summary>
    /// <param name="ParamName">存储过程名称</param>
    /// <param name="DbType">参数类型</param></param>
    /// <param name="Size">参数大小</param>
    /// <param name="Value">参数值</param>
    /// <returns>新的parameter 对象</returns>
    public SqlParameter CreateInParam(string ParamName, SqlDbType DbType, int Size, object Value)
    {
        ///创建输入类型的参数
        return CreateParam(ParamName, DbType, Size, ParameterDirection.Input, Value, false);
    }

    /// <summary>
    /// 传入输入参数
    /// </summary>
    /// <param name="ParamName">存储过程名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <param name="Value">参数值</param>
    /// <param name="isNullable">参数值是否可以为NULL</param>
    /// <returns>新的parameter对象</returns>
    public SqlParameter CreateInParam(string ParamName, SqlDbType DbType, int Size, object Value, bool isNullable)
    {
        ///创建输入类型的参数
        return CreateParam(ParamName, DbType, Size, ParameterDirection.Input, Value, isNullable);
    }

    /// <summary>
    /// 传入返回值参数
    /// </summary>
    /// <param name="ParamName">存储过程名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <returns>新的 parameter 对象</returns>
    public SqlParameter CreateOutParam(string ParamName, SqlDbType DbType, int Size)
    {
        ///创建输出类型的参数
        return CreateParam(ParamName, DbType, Size, ParameterDirection.Output, null, true);
    }

    /// <summary>
    /// 传入返回值参数
    /// </summary>
    /// <param name="ParamName">存储过程名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <returns>新的 parameter 对象</returns>
    public SqlParameter CreateReturnParam(string ParamName, SqlDbType DbType, int Size)
    {
        ///创建返回类型的参数
        return CreateParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null, false);
    }
}

