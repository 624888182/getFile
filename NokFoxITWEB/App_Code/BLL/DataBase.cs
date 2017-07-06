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
    #region ���}�ƾڮw�s��
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
    #region �����ƾڮw�s��
    public void Close()
    {
        if (connection != null)
        {
            connection.Close();
        }
    }
    #endregion
    #region ����ƾڮw�s���귽
    public void Dispose()
    {
        //�T�{�s���w�g����
        if (connection != null)
        {
            connection.Dispose();
            connection = null;
        }
    }
    #endregion
    #region �ǤJ�ѼƦ}�B�ഫ��SqlParamter����
    /// <summary>
    /// �ഫ�Ѽ�
    /// </summary>
    /// <param name="ParamName">�R�O�奻</param>
    /// <param name="DbType">�Ѽ�����</param>
    /// <param name="Size">�ѼƤj�p</param>
    /// <param name="Value">�Ѽƭ�</param>
    ///<returns>�s��Paramter��</returns>
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
    #region   �N�R�O�奻�K�[��SqlCommand
    /// <summary>
    /// �Ыؤ@��SqlCommand��H�̦��Ӱ���R�O�奻
    /// </summary>
    /// <param name="procName">�R�O�奻</param>
    /// <param name="prams">�һݰѼ�</param>
    /// <returns>��^SqlCommand��H</returns>
    private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
    {
        // �T�{���}�s��
        this.Open();
        SqlCommand cmd = new SqlCommand(procName,connection);
        cmd.CommandType = CommandType.Text; //��������

        // �̦���ѼƶǤJ�R�O�奻
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
                cmd.Parameters.Add(parameter);
        }
        // �[�J��^�Ѽ�
        cmd.Parameters.Add(
            new SqlParameter("ReturnValue", SqlDbType.Int, 4,
            ParameterDirection.ReturnValue, false, 0, 0,
            string.Empty, DataRowVersion.Default, null));

        return cmd;
    }
    #endregion
    #region �N�R�O�奻�K�[��SqlDataAdapter
    /// <summary>
    /// �Ыؤ@��SqlDataAdapter��H�Ӱ���R�O�奻
    /// </summary>
    /// <param name="procName">�R�O�奻</param>
    /// <param name="prams">�Ѽƹ�H</param>
    /// <returns></returns>
    private SqlDataAdapter CreateDataAdaper(string procName, SqlParameter[] prams)
    {
        this.Open();
        SqlDataAdapter dap = new SqlDataAdapter(procName, connection);
        dap.SelectCommand.CommandType = CommandType.Text;  //���������G�R�O�奻
        if (prams != null)
        {
            foreach (SqlParameter parameter in prams)
                dap.SelectCommand.Parameters.Add(parameter);
        }
        //�[�J��^�Ѽ�
        dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
            ParameterDirection.ReturnValue, false, 0, 0,
            string.Empty, DataRowVersion.Default, null));

        return dap;
    }
     #endregion
    #region ����ѼƩR�O�奻(�L�ƾڪ�^)
    /// <summary>
    /// ��������SQL�y�y
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
    /// ����R�O�奻
    /// </summary>
    /// <param name="procName">�R�O�奻</param>
    /// <param name="prams">�Ѽƹ�H</param>
    /// <returns></returns>
    public int RunProc(string procName, SqlParameter[] prams)
    {
        SqlCommand cmd = CreateCommand(procName, prams);
        cmd.ExecuteNonQuery();
        this.Close();
        //�o����榨�\��^��
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
    #region ����ѼƩR�O�奻(���ƾڪ�^)
    /// <summary>
    /// ����d�ߩR�O�奻�A�}�B��^DataSet�ƾڶ�
    /// </summary>
    /// <param name="procName">�R�O�奻</param>
    /// <param name="prams">�Ѽƹ�H</param>
    /// <param name="tbName">�ƾڪ�W��</param>
    /// <returns></returns>
    public DataSet RunProcReturn(string procName, SqlParameter[] prams, string tbName)
    {
        SqlDataAdapter dap = CreateDataAdaper(procName, prams);
        DataSet ds = new DataSet();
        dap.Fill(ds, tbName);
        this.Close();
        //�o����榨�\��^��
        return ds;
    }
    public DataSet RunProcReturn(string procName,string tbName)
    {
        SqlDataAdapter dap = CreateDataAdaper(procName,null);
        DataSet ds = new DataSet();
        dap.Fill(ds,tbName);
        this.Close();
        //�o����榨�\��^��
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
        //�o����榨�\��^��
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
