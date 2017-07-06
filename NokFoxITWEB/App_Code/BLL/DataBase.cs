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

/// <summary>
/// Summary description for DataBase
/// </summary>

public class DataBase
{
    protected SqlConnection connection;
    protected string connectinstring=ConfigurationManager.AppSettings["ConnectionString"];
    #region 打開數據庫連接
    public void Open()
    {
        if (connection == null)
        {
            connection = new SqlConnection(connectinstring); 
        }
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
        }
    }
    #endregion
    #region 關閉數據庫連接
    public void Close()
    {
        if (connection != null)
        {
            connection.Close();
        }
    }
    #endregion
    #region 釋放數據庫連接資源
    public void Dispose()
    {
        //確認連接已經關閉
        if (connection != null)
        {
            connection.Dispose();
            connection = null;
        }
    }
    #endregion
    #region 傳入參數并且轉換成SqlParamter類型
    /// <summary>
    /// 轉換參數
    /// </summary>
    /// <param name="ParamName">命令文本</param>
    /// <param name="DbType">參數類型</param>
    /// <param name="Size">參數大小</param>
    /// <param name="Value">參數值</param>
    ///<returns>新的Paramter值</returns>
    public SqlParameter MakeInParam(string ParamName, SqlDbType dbType,int Size, object Value)
    {
        return MakeParam(ParamName,dbType,Size,ParameterDirection.Input,Value);
    }
    public SqlParameter MakeParam(string ParamName,SqlDbType DbType,Int32 Size,ParameterDirection Direction,object Value)
    {
        SqlParameter param;

        if (Size > 0)
            param = new SqlParameter(ParamName, DbType, Size);
        else
            param = new SqlParameter(ParamName ,DbType);
        param.Direction = Direction;
        if (!(Direction == ParameterDirection.Output && Value == null))
        {
            param.Value = Value;
        }
        return param;

    }
    #endregion
    #region   將命令文本添加到SqlCommand
    /// <summary>
    /// 創建一個SqlCommand對象依次來執行命令文本
    /// </summary>
    /// <param name="procName">命令文本</param>
    /// <param name="prams">所需參數</param>
    /// <returns>返回SqlCommand對象</returns>
    private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
    {
        // 確認打開連接
        this.Open();
        SqlCommand cmd = new SqlCommand(procName,connection);
        cmd.CommandType = CommandType.Text; //執行類型

        // 依次把參數傳入命令文本
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
                cmd.Parameters.Add(parameter);
        }
        // 加入返回參數
        cmd.Parameters.Add(
            new SqlParameter("ReturnValue", SqlDbType.Int, 4,
            ParameterDirection.ReturnValue, false, 0, 0,
            string.Empty, DataRowVersion.Default, null));

        return cmd;
    }
    #endregion
    #region 將命令文本添加到SqlDataAdapter
    /// <summary>
    /// 創建一個SqlDataAdapter對象來執行命令文本
    /// </summary>
    /// <param name="procName">命令文本</param>
    /// <param name="prams">參數對象</param>
    /// <returns></returns>
    private SqlDataAdapter CreateDataAdaper(string procName, SqlParameter[] prams)
    {
        this.Open();
        SqlDataAdapter dap = new SqlDataAdapter(procName, connection);
        dap.SelectCommand.CommandType = CommandType.Text;  //執行類型：命令文本
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
                dap.SelectCommand.Parameters.Add(parameter);
        }
        //加入返回參數
        dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
            ParameterDirection.ReturnValue, false, 0, 0,
            string.Empty, DataRowVersion.Default, null));

        return dap;
    }
     #endregion
    #region 執行參數命令文本(無數據返回)
    /// <summary>
    /// 直接執行SQL語句
    /// </summary>
    /// <param name="strsql"></param>
    public int ExcuteSql(string strsql)
    {
        this.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = strsql;
        cmd.ExecuteNonQuery();
        this.Close();
        return 1;

    }
    /// <summary>
    /// 執行命令文本
    /// </summary>
    /// <param name="procName">命令文本</param>
    /// <param name="prams">參數對象</param>
    /// <returns></returns>
    public int RunProc(string procName, SqlParameter[] prams)
    {
        SqlCommand cmd = CreateCommand(procName, prams);
        cmd.ExecuteNonQuery();
        this.Close();
        //得到執行成功返回值
        return (int)cmd.Parameters["ReturnValue"].Value;
    }
    public int RunProc(string procName)
    {
        this.Open();
        SqlCommand cmd = new SqlCommand(procName, connection);
        cmd.ExecuteNonQuery();
        this.Close();
        return 1;
    }
    #endregion
    #region 執行參數命令文本(有數據返回)
    /// <summary>
    /// 執行查詢命令文本，并且返回DataSet數據集
    /// </summary>
    /// <param name="procName">命令文本</param>
    /// <param name="prams">參數對象</param>
    /// <param name="tbName">數據表名稱</param>
    /// <returns></returns>
    public DataSet RunProcReturn(string procName, SqlParameter[] prams, string tbName)
    {
        SqlDataAdapter dap = CreateDataAdaper(procName, prams);
        DataSet ds = new DataSet();
        dap.Fill(ds, tbName);
        this.Close();
        //得到執行成功返回值
        return ds;
    }
    public DataSet RunProcReturn(string procName,string tbName)
    {
        SqlDataAdapter dap = CreateDataAdaper(procName,null);
        DataSet ds = new DataSet();
        dap.Fill(ds,tbName);
        this.Close();
        //得到執行成功返回值
        return ds;
    }
    public DataSet GetDataSet(string strsql)
    {
        SqlDataAdapter dap = new SqlDataAdapter(strsql,connection);
        DataSet ds = new DataSet();
        dap.Fill(ds);
        return ds;
    }

    public DataSet GetdataSet(string sqlstr, string tbName)
    {
        SqlDataAdapter dap = CreateDataAdaper(sqlstr, null);
        DataSet ds = new DataSet();
        dap.Fill(ds, tbName);
        this.Close();
        //得到執行成功返回值
        return ds;
    }
    #endregion
    public DataBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
