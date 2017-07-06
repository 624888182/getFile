using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
/// <summary>
/// Summary description for ReceiveData
/// </summary>
public class ReceiveData
{
    private static string FERROR = "";
    private static OracleConnection OraConn = null;
    public ReceiveData()
    {
    }
    
    private static string GetConnection()
    {
        string conn = ConfigurationManager.AppSettings["bjl6testConnectionString"];
        return conn;
    }

    private static void ConnectDB()
    {
        if (OraConn == null)
        {
            string conn = GetConnection();
            OraConn = new OracleConnection(conn);
        }
    }
    private static int CheckConnect()
    {
        ConnectDB();
        int iRet;
        try
        {
            if (OraConn.State == ConnectionState.Closed)
                OraConn.Open();
            iRet = 0;
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        return iRet;
    }

    private static int ExecSQLWithParam(string sSql, OracleParameter[] oraParam,ref DataTable ds)
    {
        OracleCommand cmd = null;
        OracleDataAdapter oda = null;
        
        int iRet = CheckConnect();
        if (iRet != 0) return iRet;
        try
        {
            cmd = new OracleCommand(sSql, OraConn);
            int i;
            for (i = 0; i < oraParam.GetLength(0); i++)
                cmd.Parameters.Add(oraParam[i]);
            oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(ds);
            iRet = 0;
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
            if (oda != null) oda.Dispose();
        }
        return iRet;
    }

    /* Return > zero effect count
     *  < Zero Error, call GetError get information
     */
    private static int ExecSQLNoQueryWithParam(string sSql, OracleParameter[] oraParam)
    {
        OracleCommand cmd = null;
        int iRet = CheckConnect();
        if (iRet != 0) return iRet;
        try
        {
            cmd = new OracleCommand(sSql, OraConn);
            int i;
            for (i = 0; i < oraParam.GetLength(0); i++)
                cmd.Parameters.Add(oraParam[i]);

            iRet = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;    
    }

    

    public static int GetLastDocumentID(int iCount,ref string[] RET)
    {
        string strSql = "Select DocumentID From  (select DocumentID,RowNum from  "+
                        "(Select  distinct DocumentID  from check_data Where RECVCNT is null  "+
                        "and DocumentID Not In "+
                        "(Select distinct DocumentID from check_data  where  RECSTATUS ='D') "+
                        "Order by 1 desc) "+
                        ") Where RowNum < :V_Count";
        OracleParameter[] oraParam = new OracleParameter[1];
        oraParam[0] = new OracleParameter("V_Count", iCount);
        DataTable dt = new DataTable();
        int iRet;
        try
        {
            iRet = ExecSQLWithParam(strSql, oraParam, ref dt);
            if (iRet == 0)
            {
                RET = new string[0];
                if (dt != null)
                {
                    RET = new string[dt.Rows.Count];
                    int i;
                    for (i = 0; i < dt.Rows.Count; i++)
                        RET[i] = dt.Rows[i][0].ToString();
                }
            }
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        finally
        {
            if (dt != null) dt.Dispose();
        }
        return iRet;
    }

    /* Return 0: PSN Count 0 Not Found, 1: Found
     *        1: F0;
     *        2: F1
     *        3: F2
     *        ...
     *        7: F6
     *        8:Total Count
     *        9: Scan Count
     *        10: STATUS Empty is OK, 'E' be locked.
     *        11: ERROR MESSAGE;
     */
    public static int CheckPSN(string PSN, string DocID,ref string[] RET)
    {
        string strSql = "select F0,F1,F2,F3,F4,F5,F6,RECSTATUS,RECVCNT from  sfis1.Check_Data Where DOCUMENTID = :V_DOC And PSN = :V_PSN ";
        OracleParameter[] oraParam = new OracleParameter[2];
        oraParam[0] = new OracleParameter("V_DOC", DocID);
        oraParam[1] = new OracleParameter("V_PSN", PSN);
        DataTable dt = new DataTable();
        int iRet;
        try
        {
            iRet = ExecSQLWithParam(strSql, oraParam, ref dt);
            if (iRet == 0)
            {
                RET = new string[12];
                if (dt.Rows.Count > 0)
                {

                    int i;
                    for (i = 0; i < 7; i++)
                        RET[i + 1] = dt.Rows[0][i].ToString();
                    RET[10] = dt.Rows[0][7].ToString();
                    if (dt.Rows[0][8].ToString().Trim() != "")
                    {
                        RET[0] = "3";
                        RET[11] = "PSN has been scaned";
                    }
                    else
                    {
                        if (RET[10] == "")
                        {
                            iRet = UpdatePSN(PSN, DocID);
                            RET[0] = "1";
                            RET[11] = "OK,Scan Pass";
                        }
                        else
                        {
                            RET[0] = "2";
                            RET[11] = "ERROR:PSN STATUS IS " + RET[10];
                        }
                    }
                }
                else
                {
                    RET[0] = "0";
                    RET[11] = "ERROR:Not Found PSN IN DOC";
                }
                string[] DocCount = new string[0];
                iRet = GetDocumentCount(DocID, ref DocCount);
                if (iRet == 0)
                {
                    RET[8] = DocCount[0];
                    RET[9] = DocCount[1];
                }
            }
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        finally
        {
            if (dt != null) dt.Dispose();
        }
        return iRet;
    }

    private static int StrToIntDef(string str, int def)
    {
        int iRet;
        try
        {
            iRet = Convert.ToInt32(str);
        }
        catch
        {
            iRet = def;
        }
        return iRet;
    
    }

    private static int CheckCartonSet(DataTable dt, ref string[] RET)
    {
        int i;
        RET = new string[12];
        RET[11]= "";
        int iScaned = 0;
        int iLock = 0;
        int iRet = 0;

        for (i = 0; i < dt.Rows.Count; i++)
        {
            RET[0] = (StrToIntDef(RET[0], 0) + StrToIntDef(dt.Rows[i][0].ToString(), 0)).ToString();
            RET[1] = (StrToIntDef(RET[1], 0) + StrToIntDef(dt.Rows[i][1].ToString(), 0)).ToString();
            RET[2] = (StrToIntDef(RET[2], 0) + StrToIntDef(dt.Rows[i][2].ToString(), 0)).ToString();
            RET[3] = (StrToIntDef(RET[3], 0) + StrToIntDef(dt.Rows[i][3].ToString(), 0)).ToString();
            RET[4] = (StrToIntDef(RET[4], 0) + StrToIntDef(dt.Rows[i][4].ToString(), 0)).ToString();
            RET[5] = (StrToIntDef(RET[5], 0) + StrToIntDef(dt.Rows[i][5].ToString(), 0)).ToString();
            RET[6] = (StrToIntDef(RET[6], 0) + StrToIntDef(dt.Rows[i][6].ToString(), 0)).ToString();
            if (dt.Rows[i][7].ToString().Trim() != "")
            {
                iLock++;
            }
            else
            {
                if (dt.Rows[i][8].ToString().Trim() != "")
                    iScaned++;
                else
                    iRet++;
            }
        }
        if (iScaned > 0)
            RET[11] = iScaned.ToString() + " PSN be scaned ";
        if (iLock > 0)
            RET[11] += iLock.ToString() + "PSN be locked or status is E ";
        return iRet;
    
    }

    /* Return 0: Carton Count 0 Not Found, 1: Found
     *        1: F0;
     *        2: F1
     *        3: F2
     *        ...
     *        7: F6
     *        8:Total Count
     *        9: Scan Count
     *        10: STATUS Empty is OK, 'E' be locked.
     *        11: ERROR MESSAGE;
     */
    public static int CheckCarton(string CARTONID, string DocID, ref string[] RET)
    {
        string strSql = "select F0,F1,F2,F3,F4,F5,F6,RECSTATUS,RECVCNT from  sfis1.Check_Data Where DOCUMENTID = :V_DOC And CARTON = :V_CARTON ";
        OracleParameter[] oraParam = new OracleParameter[2];
        oraParam[0] = new OracleParameter("V_DOC", DocID);
        oraParam[1] = new OracleParameter("V_CARTON", CARTONID);
        DataTable dt = new DataTable();
        int iRet;
        try
        {
            iRet = ExecSQLWithParam(strSql, oraParam, ref dt);
            if (iRet == 0)
            {
                RET = new string[0];
                
                if (dt.Rows.Count > 0)
                {
                    int iUpdateCount = CheckCartonSet(dt, ref RET);
                    if (iUpdateCount > 0)
                    {
                        iRet = UpdateCartcon(CARTONID, DocID);
                        RET[11] = iRet.ToString() + " PSN Passed " + RET[11];
                        if (iRet > 0)
                        {
                            RET[0] = "1";
                            iRet = 0;
                        }
                        else
                        {
                            RET[0] = "3";
                        }
                    }
                    else
                    {
                        RET[0] = "2";
                    }
                }
                else
                {
                    RET[0] = "0";
                    RET[11] = "ERROR:Not Found CARTON IN DOC";
                }
                string[] DocCount = new string[0];
                iRet = GetDocumentCount(DocID, ref DocCount);
                if (iRet == 0)
                {
                    RET[8] = DocCount[0];
                    RET[9] = DocCount[1];
                }
            }
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        finally
        {
            if (dt != null) dt.Dispose();
        }
        return iRet;
    }
    

    private static int UpdatePSN(string PSN, string DocID)
    {
        string strSql = "Update  sfis1.Check_Data Set RECVCNT = '1' Where DOCUMENTID = :V_DOC And PSN = :V_PSN";
        OracleParameter[] oraParam = new OracleParameter[2];
        oraParam[0] = new OracleParameter("V_DOC",DocID);
        oraParam[1] = new OracleParameter("V_PSN",PSN);
        int iRet = ExecSQLNoQueryWithParam(strSql, oraParam);
        return iRet;
    }

    private static int UpdateCartcon(string Carton,string Doc)
    {
        string strSql = "Update sfis1.Check_Data Set RECVCNT = '1' Where RECVCNT is null and RECSTATUS is null and DOCUMENTID = :V_DOC And CARTON = :V_CARTON ";
        OracleParameter[] oraParam = new OracleParameter[2];
        oraParam[0] = new OracleParameter("V_DOC", Doc);
        oraParam[1] = new OracleParameter("V_CARTON", Carton);
        int iRet = ExecSQLNoQueryWithParam(strSql, oraParam);
        return iRet;
    }

    public static int GetDocumentCount(string DocID,ref string[] RET)
    {
        string strSql = "select Count(1) TotalCount, Sum(Case When RECVCNT='1' then 1 else 0 end) from sfis1.Check_Data Where DocumentID=:V_DOC";
        OracleParameter[] oraParam = new OracleParameter[1];
        oraParam[0] = new OracleParameter("V_DOC", DocID);
        DataTable dt = null;
        RET = new string[2];
        int iRet;
        try
        {
            dt = new DataTable();
            iRet = ExecSQLWithParam(strSql, oraParam, ref dt);
            RET[0] = dt.Rows[0][0].ToString();
            RET[1] = dt.Rows[0][1].ToString();
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        finally
        {
            if (dt != null) dt.Dispose();
        }
        return iRet;
    }

    public static string GetErrorMessage()
    {
        return FERROR;
    }
    

}