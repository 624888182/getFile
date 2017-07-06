using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;

/// <summary>
/// Summary description for DB_ORACLE
/// </summary>
public class DB_ORACLE
{
    private static string FERROR = "";
	public DB_ORACLE()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string GetError()
    {
        return FERROR;
    }
    private static int CrateConnection(string conn,ref OracleConnection oraConn)
    {
        int iRet;
        try
        {
            oraConn = new OracleConnection(conn);
            oraConn.Open();
            iRet = 0;
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        return iRet;
    
    }

    public static int EXECNOQuerywithParam(string conn, string SQL, OracleParameter[] oraParam)
    {
        int iRet = 0;
        OracleConnection oraConn = null;
        OracleCommand cmd = null;
        try
        {
            iRet = CrateConnection(conn, ref oraConn);
            if (iRet == 0)
            {
                cmd = new OracleCommand(SQL, oraConn);
                int i;
                for (i = 0; i < oraParam.GetLength(0); i++)
                    cmd.Parameters.Add(oraParam[i]);
                iRet = cmd.ExecuteNonQuery();
            }
            oraConn.Close();
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
            if (oraConn != null) oraConn.Dispose();
        }
        return iRet;
    
    }

    public static int EXECSQL(string conn, string SQL, ref DataTable ds)
    {
        int iRet = 0;
        OracleConnection oraConn = null;
        OracleDataAdapter oda = null;
        OracleCommand cmd = null;
        if (ds == null) ds = new DataTable();
        try
        {
            iRet = CrateConnection(conn, ref oraConn);
            if (iRet == 0)
            {
                cmd = new OracleCommand(SQL, oraConn);
                oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                oda.Fill(ds);
                oraConn.Close();
                iRet = 0;
            }
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        finally
        { 
            if (oda !=null) oda.Dispose();
            if (cmd !=null) cmd.Dispose();
            if (oraConn != null) oraConn.Dispose();
        }


        return iRet;
    }
}

public class OracleOperate
{
    private OracleConnection conn = null;
    private string FERROR = "";

    public string GetError()
    {
        return FERROR;
    }

    private int connect()
    {
        int iRet = 0;
        if (conn != null)
        { 
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    FERROR = e.Message;
                    iRet = -1;
                }
            }
        
        }
        return iRet;
    
    }

    public OracleOperate(string connstr)
    {
        conn = new OracleConnection(connstr);
        connect();
    }

    public int ExecSqlWithParam(string sSql,OracleParameter[] Param,ref DataTable dt)
    {
        int iRet = 0;
        iRet = connect();
        if (iRet == 0)
        {
            OracleCommand cmd = null;
            OracleDataAdapter oda = null;
            try
            {
                cmd = new OracleCommand(sSql, conn);
                int i;
                for (i = 0; i < Param.GetLength(0); i++)
                {
                    cmd.Parameters.Add(Param[i]);
                }
                oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                if (dt == null)
                    dt = new DataTable();
                oda.Fill(dt);
                iRet = 0;
            }
            catch (Exception e)
            {
                FERROR = e.Message;
                iRet = -1;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (oda != null) oda.Dispose();
            }
        }
        return iRet;
    }

    public int ExecSqlWithParam(string sSql, string[] ParamName,object[] oParam , ref DataTable dt)
    {
        
        int iRet = 0;
        iRet = connect();
        if (iRet == 0)
        {
            OracleCommand cmd = null;
            OracleDataAdapter oda = null;
            try
            {
                cmd = new OracleCommand(sSql, conn);
                int i;
                for (i = 0; i < oParam.GetLength(0); i++)
                {
                    OracleParameter op = new OracleParameter(ParamName[i], oParam[i]);
                    cmd.Parameters.Add(op);
                }
                oda = new OracleDataAdapter();
                oda.SelectCommand = cmd;
                if (dt == null)
                    dt = new DataTable();
                oda.Fill(dt);
                iRet = 0;
            }
            catch (Exception e)
            {
                FERROR = e.Message;
                iRet = -1;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (oda != null) oda.Dispose();
            }
        }
        return iRet;
    }

    public int ExecSqlWithParam(string sSql, OracleParameter[] Param)
    {
        int iRet = 0;
        iRet = connect();
        if (iRet == 0)
        {
            OracleCommand cmd = null;
            try
            {
                cmd = new OracleCommand(sSql, conn);
                int i;
                for (i = 0; i < Param.GetLength(0); i++)
                {
                    cmd.Parameters.Add(Param[i]);
                }
                cmd.ExecuteNonQuery();
                iRet = 0;
            }
            catch (Exception e)
            {
                FERROR = e.Message;
                iRet = -1;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }
        return iRet;
    }

    



}



