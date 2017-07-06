using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for StandToMax
/// </summary>
public class StandToMax
{
    private OracleConnection connStandBy;/*10.186.171.211*/
    private OracleConnection connMaxStore;/*10.186.166.108*/
    private OracleConnection connNormal;/*10.186.171.76*/
    private OracleConnection connNormalBakup;/*10.186.171.215*/
    private string FERROR;

    #region Share Method
    public StandToMax()
	{
        string strConnStand = ConfigurationManager.AppSettings["L8StandByConnectionString"];
        string strConnMax = ConfigurationManager.AppSettings["tjmaxstoreConnectionString"];
        string strConnNormal = ConfigurationManager.AppSettings["NormalDbConnectionString"];
        string strConnNormalBakup=ConfigurationManager.AppSettings["NormalBakupConnectionString"];
        connStandBy = new OracleConnection(strConnStand);
        connMaxStore = new OracleConnection(strConnMax);
        connNormal = new OracleConnection(strConnNormal);
        connNormalBakup = new OracleConnection(strConnNormalBakup);
        FERROR = "";
	}

    private int SelectSql(string SQL, OracleConnection conn,ref DataTable dt)
    {
        int iRet;
        CheckConnect(conn);
        OracleDataAdapter oda = null;
        OracleCommand cmd = null;
        try
        {
            cmd = new OracleCommand(SQL, conn);
            oda = new OracleDataAdapter();
            oda.SelectCommand = cmd;
            oda.Fill(dt);
            iRet = 0;
        }
        catch (Exception e)
        {
            iRet = -1;
            FERROR = e.Message;
        }
        finally
        {
            if (oda != null) oda.Dispose();
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }
    public string GetError()
    {
        return FERROR;
    }
    private int CheckConnect(OracleConnection conn)
    {
        int iRet;
        try
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            FERROR = "";
            iRet = 0;
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        return iRet;
        
    
    }
    private int CheckTalbe(string TableName,string IP,string[] Unique_Columns,ref string[] Columns)
    {
        int iRet,i;
        string SQL = "Select * from " + TableName + " Where RowNum < 0";
        DataTable dtStandBy = new DataTable();
        DataTable dtMax = new DataTable();
        
        while (true)
        {
            iRet = SelectSql(SQL, connStandBy, ref dtStandBy);
            if (iRet != 0) break;
            if(IP=="108")
            iRet = SelectSql(SQL, connMaxStore, ref dtMax);
            if (IP == "215")
                iRet = SelectSql(SQL, connNormalBakup, ref dtMax);
            if (iRet != 0) break;

            if (dtStandBy.Columns.Count != dtMax.Columns.Count)
            {
                iRet = 1;
                FERROR = "Source  columns count is different with destination ";
                break;
            }

            #region CheckColumns
            for (i = 0; i < dtStandBy.Columns.Count; i++)
            {
                string ColumnName = dtStandBy.Columns[i].ColumnName;
                try
                {
                    if (dtMax.Columns[ColumnName].DataType.ToString() != dtStandBy.Columns[i].DataType.ToString())
                    {
                        iRet = 2;
                        FERROR = "";
                        break;
                    }
                }
                catch
                {
                    iRet = 3;
                    FERROR = "Not found Column: " + ColumnName + " in destination";
                    break;
                }
            }
            if (iRet == 3) break;

            #endregion

            #region CheckUniqueColumns
            for (i = 0; i < Unique_Columns.GetLength(0); i++)
            {
                try
                {
                    string Tmp = dtStandBy.Columns[Unique_Columns[i]].ColumnName;
                }
                catch
                {
                    iRet = 4;
                    FERROR = "Unique Column " + Unique_Columns[i] + " not in table ";
                    break;
                }
            }

            if (iRet == 4) break;
            #endregion

            Columns = new string[dtStandBy.Columns.Count];
            for (i = 0; i < dtStandBy.Columns.Count; i++)
                Columns[i] = dtStandBy.Columns[i].ColumnName;
            iRet = 0;
            break;
        }
        dtStandBy.Dispose();
        dtMax.Dispose();
        return iRet;
    }
    private int GetDataFromTable(string TableName, string Date_Column, string BeginDate, string EndDate,string[] Columns,ref DataTable dt)
    {
        int iRet,i;
        string SQL = "Select ";
        for (i = 0; i < Columns.GetLength(0); i++)
        {
            SQL = SQL + Columns[i];
            if (i != Columns.GetLength(0) - 1) SQL += ",";
        }
        SQL += " From " + TableName;
        SQL += " Where " + Date_Column + ">= To_Date('" + BeginDate + "','YYYY/MM/DD') And " + Date_Column + " < To_Date('" + EndDate + "', 'YYYY/MM/DD') ";
        //SQL += " And RowNum <1000 ";
        iRet = SelectSql(SQL, connStandBy, ref dt);
        return iRet;
    }
 
    #endregion

    #region StandBy--->MaxStore

    private int InsertDateToTableWithUpdate(string TableName, string IP, string[] Unique_Columns, string[] Unique_Keys ,DataTable dt, ref int SuccessCount, ref int FailCount, ref int           DuplicateCount,ref int UpdateCount)
    {
        int iRet, i;
        for (i=0; i< Unique_Keys.GetLength(0);i++)
            Unique_Keys[i]= Unique_Keys[i].ToUpper();

        #region InsertSQL
        string SQL = "Insert Into " + TableName + "(";
        string SQLValue = "Values (";
        for (i = 0; i < dt.Columns.Count; i++)
        {
            SQL += dt.Columns[i].ColumnName;
            SQLValue += ":V" + i.ToString();
            if (i != dt.Columns.Count - 1)
            {
                SQL += ",";
                SQLValue += ",";
            }
            else
            {
                SQL += ") ";
                SQLValue += ") ";
            }
        }
        SQL += SQLValue;
        #endregion

        #region UpDateSQL
        string UpdateSql = "Update " + TableName + " SET ";
        string UpdateValue = "";
        for (i = 0; i < dt.Columns.Count; i++)
        {
            if (Unique_Keys.Contains(dt.Columns[i].ColumnName.ToUpper())==true)  // include index columns
            {
                continue;
            }
            else
            {
                if (UpdateValue != "") UpdateValue += ",";
                UpdateValue +=  dt.Columns[i].ColumnName + " = :VU" + i.ToString();
            
            }
        }
        UpdateSql += UpdateValue;
        for (i = 0; i < Unique_Keys.GetLength(0); i++)
        {
            if (i == 0)
                UpdateSql += " Where ";
            else
                UpdateSql += " And ";
            UpdateSql += Unique_Keys[i] + " = :VW" + i.ToString();
        }
        #endregion
        #region UniqueSQL

        string SelectSQL = "Select ";
        for (i=0; i< Unique_Columns.GetLength(0);i++)
        {
            if (i==0)
                SelectSQL += Unique_Columns[i];
            else
                SelectSQL += ", "+Unique_Columns[i];
        
        }
        SelectSQL += " From " + TableName + " Where ";
        for (i = 0; i < Unique_Keys.GetLength(0); i++)
            SelectSQL += Unique_Keys[i] + " = :V" + i.ToString() + " And ";
        SelectSQL += " RowNum <= 1";

        #endregion

        SuccessCount = 0;
        FailCount = 0;
        DuplicateCount = 0;
        UpdateCount = 0;
        OracleCommand cmd = null;
        OracleCommand cmdSel = null;
        OracleCommand cmdUpdate = null;
        if (IP == "108")
        {
            cmd = new OracleCommand(SQL, connMaxStore);
            cmdSel = new OracleCommand(SelectSQL, connMaxStore);
            cmdUpdate = new OracleCommand(UpdateSql, connMaxStore);
            iRet = CheckConnect(connMaxStore);
        }
        if (IP == "215")
        {
            cmd = new OracleCommand(SQL, connNormalBakup);
            cmdSel = new OracleCommand(SelectSQL, connNormalBakup);
            cmdUpdate = new OracleCommand(UpdateSql, connNormalBakup);
            iRet = CheckConnect(connNormalBakup);
        }
        OracleDataAdapter oda = new OracleDataAdapter();
        iRet = CheckConnect(connMaxStore);
        for (i = 0; i < dt.Rows.Count; i++)
        {
            int iCol;
            DataTable dtSel = new DataTable();
            try
            {
                cmdSel.Parameters.Clear();
                for (iCol = 0; iCol < Unique_Keys.GetLength(0); iCol++)
                {
                    cmdSel.Parameters.AddWithValue("V" + iCol.ToString(), dt.Rows[i][Unique_Keys[iCol]]);
                }
                oda.SelectCommand = cmdSel;
                oda.Fill(dtSel);
                if (dtSel.Rows.Count > 0)
                {
                    int iSel;
                    bool ValueSame = true;
                    for (iSel = 0; iSel < Unique_Columns.GetLength(0); iSel++)
                    {
                        if (dtSel.Rows[0][iSel].ToString() != dt.Rows[i][Unique_Columns[iSel]].ToString())
                        {
                            ValueSame = false;
                            break;
                        }
                    
                    }

                    if (ValueSame == true)
                        DuplicateCount++;
                    else
                    {
                        cmdUpdate.Parameters.Clear();
                        for (iCol = 0; iCol < dt.Columns.Count; iCol++)
                        {
                            if (Unique_Keys.Contains(dt.Columns[iCol].ColumnName.ToUpper()) == true)  // include index columns
                                continue;
                            cmdUpdate.Parameters.AddWithValue("VU"+iCol.ToString(),dt.Rows[i][iCol]);
                        }
                        for (iCol = 0; iCol < Unique_Keys.GetLength(0); iCol++)
                        {
                            cmdUpdate.Parameters.Add("VW" + iCol.ToString(), dt.Rows[i][Unique_Keys[iCol]]);
                        }
                        cmdUpdate.ExecuteNonQuery();
                        UpdateCount++;
                    }

                }
                else
                {
                    cmd.Parameters.Clear();
                    for (iCol = 0; iCol < dt.Columns.Count; iCol++)
                    {
                        cmd.Parameters.AddWithValue("V" + iCol.ToString(), dt.Rows[i][iCol]);
                    }
                    cmd.ExecuteNonQuery();
                    SuccessCount++;
                }
            }
            catch
            {
                FailCount++;
            }
            finally
            {
                if (dtSel != null) dtSel.Dispose();

            }
        }
        if (cmd != null) cmd.Dispose();
        if (oda != null) oda.Dispose();
        if (cmdSel != null) cmdSel.Dispose();
        if (cmdUpdate != null) cmdUpdate.Dispose();
        return iRet;
    }

    private int InsertDateToTable(string TableName,string IP,string[] Unique_Columns, DataTable dt,ref int SuccessCount,ref int FailCount,ref int DuplicateCount)
    {
        int iRet,i;
        #region InsertSQL
        string SQL = "Insert Into " + TableName + "(";
        string SQLValue = "Values (";
        for (i = 0; i < dt.Columns.Count; i++)
        {
            SQL += dt.Columns[i].ColumnName;
            SQLValue += ":V" + i.ToString();
            if (i != dt.Columns.Count - 1)
            {
                SQL += ",";
                SQLValue += ",";
            }
            else
            {
                SQL += ") ";
                SQLValue += ") ";
            }
        }
        SQL += SQLValue;
        #endregion

        string UpdateSql = "Update " + TableName + " SET ";
        string WhereSql = "Where ";
        for (i = 0; i < dt.Columns.Count; i++)
        {
            if (i != dt.Columns.Count - 1)
            {
                if (dt.Columns[i].ColumnName == Unique_Columns[0].ToString())
                {
                    WhereSql += dt.Columns[i].ColumnName + " =:V" + i.ToString();
                }
                else
                {
                    UpdateSql += dt.Columns[i].ColumnName + " =:V" + i.ToString() + ", ";
                }
            }


        }
        UpdateSql+=WhereSql;
        #region UniqueSQL

        string SelectSQL = "Select 1 From " + TableName + " Where ";
        for (i = 0; i < Unique_Columns.GetLength(0); i++)
            SelectSQL += Unique_Columns[i] + " = :V" + i.ToString() +" And ";
        SelectSQL += " RowNum <= 1";

        #endregion

        SuccessCount = 0;
        FailCount = 0;
        DuplicateCount = 0;
        OracleCommand cmd = null;
        OracleCommand cmdSel = null;
        if (IP == "108")
        {
            cmd = new OracleCommand(SQL, connMaxStore);
            cmdSel = new OracleCommand(SelectSQL, connMaxStore);
            iRet = CheckConnect(connMaxStore);
        }
        if (IP == "215")
        {
            cmd = new OracleCommand(SQL, connNormalBakup);
            cmdSel = new OracleCommand(SelectSQL, connNormalBakup);
            iRet = CheckConnect(connNormalBakup);
        } 
        OracleDataAdapter oda = new OracleDataAdapter();
        iRet = CheckConnect(connMaxStore);
        for (i = 0; i < dt.Rows.Count; i++)
        {
            int iCol;
            DataTable dtSel = new DataTable();
            try
            {
                cmdSel.Parameters.Clear();
                for (iCol = 0; iCol < Unique_Columns.GetLength(0); iCol++)
                {
                    cmdSel.Parameters.AddWithValue("V" + iCol.ToString(), dt.Rows[i][Unique_Columns[iCol]]);
                }
                oda.SelectCommand = cmdSel;
                oda.Fill(dtSel);
                if (dtSel.Rows.Count > 0)
                {
                    DuplicateCount++;

                }
                else
                {
                    cmd.Parameters.Clear();
                    for (iCol = 0; iCol < dt.Columns.Count; iCol++)
                    {
                        cmd.Parameters.AddWithValue("V" + iCol.ToString(), dt.Rows[i][iCol]);
                    }
                    cmd.ExecuteNonQuery();
                    SuccessCount++;
                }
            }
            catch
            {
                FailCount++;
            }
            finally
            {
                if (dtSel != null) dtSel.Dispose();
            
            }
        }
        if (cmd != null) cmd.Dispose();
        if (oda != null) oda.Dispose();
        if (cmdSel != null) cmdSel.Dispose();

        return iRet;
    }
    public int Insert_wcdma_tse_R_FUNCTION_DETAIL_T(string IP,string BeginDate, string EndDate, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "wcdma_tse.R_FUNCTION_DETAIL_T";
        string Date_Column = "TEST_TIME";
        string Order_Column = " ";
        string[] Unique_Columns = new string[5];
        Unique_Columns[0] = "SERIAL_NUMBER";
        Unique_Columns[1] = "TEST_TIME";
        Unique_Columns[2] = "STATUS";
        Unique_Columns[3] = "ITEM_KEY";
        Unique_Columns[4] = "SEQ";


        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckTalbe(TableName,IP,Unique_Columns, ref Columns);
            if (iRet != 0) break;
            int iBatch = 0;
            while (true)
            {
                dt.Clear();
                iRet = GetDataFromTableBatch(TableName, Date_Column,Order_Column, BeginDate, EndDate, Columns, ref dt, iBatch, 100000);
                iBatch++;
                if (iRet != 0) break;
                if (dt.Rows.Count <= 0) break;
                int TmpSucc = 0;
                int TmpFail = 0;
                int TmpDup = 0;
                iRet = InsertDateToTable(TableName,IP,Unique_Columns, dt, ref TmpSucc, ref TmpFail, ref TmpDup);
                SuccessCount += TmpSucc;
                FailCount += TmpFail;
                DuplicateCount += TmpDup;
                if (iRet != 0) break;
            }
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName +" "+ FERROR;
        if (dt != null) dt.Dispose();
        return iRet;
    }
    public int Insert_wcdma_tse_R_FUNCTION_HEAD_T(string IP,string Begindate, string EndDate, ref int SuccessCount, ref int FailCount, ref int DuplicateCount,ref int UpdateCount)
    {
        string TableName = "WCDMA_TSE.R_FUNCTION_HEAD_T";
        string Date_Column = "TEST_TIME";
        string [] Unique_Columns=new string[2];
        Unique_Columns[0] = "SERIAL_NUMBER";
        Unique_Columns[1] = "TEST_TIME";

        string[] Unique_Keys = new string[1];
        Unique_Keys[0] = "SERIAL_NUMBER";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt =new DataTable();
        while(true)
        {
            iRet=CheckTalbe(TableName,IP,Unique_Columns,ref Columns);
            if(iRet!=0) break;
            iRet = GetDataFromTable(TableName, Date_Column, Begindate, EndDate, Columns, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToTableWithUpdate(TableName, IP, Unique_Columns, Unique_Keys, dt, ref SuccessCount, ref FailCount, ref DuplicateCount, ref UpdateCount);
                //InsertDateToTable(TableName,IP,Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet;
 


        
    }
    public int Insert_wcdma_tse_R_FUNCTION_LOG_T(string IP,string Begindate, string EndDate, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "WCDMA_TSE.R_FUNCTION_LOG_T";
        string Date_Column = "TEST_TIME";
        string[] Unique_Columns = new string[3];
        Unique_Columns[0] = "SERIAL_NUMBER";
        Unique_Columns[1] = "GROUP_NAME";
        Unique_Columns[2] = "TEST_TIME";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckTalbe(TableName,IP, Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataFromTable(TableName, Date_Column, Begindate, EndDate, Columns, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToTable(TableName,IP, Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet;
        
    }
    public int Insert_testinfo_testinfo_DETAIL(string IP,string Begindate, string EndDate, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "TESTINFO.TESTINFO_DETAIL";
        string Date_Column = "CREATE_DATE";
        string[] Unique_Columns = new string[5];
        Unique_Columns[0] = "TRACK_ID";
        Unique_Columns[1] = "ID";
        Unique_Columns[2] = "CREATE_DATE";
        Unique_Columns[3] = "STATION_NAME";
        Unique_Columns[4] = "STATUS";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckTalbe(TableName,IP,Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataFromTable(TableName, Date_Column, Begindate, EndDate, Columns, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToTable(TableName,IP,Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet;

    }
    public int Insert_testinfo_HEAD(string IP,string Begindate, string EndDate, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "TESTINFO.TESTINFO_HEAD";
        string Date_Column = "CREATE_DATE";
        string[] Unique_Columns = new string[5];
        Unique_Columns[0] = "ID";
        Unique_Columns[1] = "CREATE_DATE";
        Unique_Columns[2] = "TRACK_ID";
        Unique_Columns[3] = "STATION_NAME";
        Unique_Columns[4] = "STATUS";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckTalbe(TableName,IP,Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataFromTable(TableName, Date_Column, Begindate, EndDate, Columns, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToTable(TableName,IP,Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet;
 

    }
    private int GetDataFromTableBatch(string TableName, string Date_Column, string OrderColumns, string BeginDate, string EndDate, string[] Columns, ref DataTable dt, int iBatch, int iBathcQty)
    {
        int iRet, i;
        string SQL = "(Select ";
        string StrField = "";
        for (i = 0; i < Columns.GetLength(0); i++)
        {
            StrField += Columns[i];
            SQL = SQL + Columns[i] + ",";
            if (i != Columns.GetLength(0) - 1) StrField += ",";
        }
        SQL += " RowNum RRR From " + TableName;
        SQL += " Where " + Date_Column + ">= To_Date('" + BeginDate + "','YYYY/MM/DD') And " + Date_Column + " < To_Date('" + EndDate + "', 'YYYY/MM/DD') )";
        SQL = "Select " + StrField + " From " + SQL + "Where rrr > " + (iBatch * iBathcQty).ToString() + " And rrr <= " + ((iBatch + 1) * iBathcQty).ToString();

        iRet = SelectSql(SQL, connStandBy, ref dt);
        return iRet;
    }
    #endregion

    #region MaxStore--->GetNormal
    private int CheckNormalTable(string TableName,string IP,string[] Unique_Columns, ref string[] Columns)
    {
        int iRet, i;
        string SQL = "Select * from " + TableName + " Where RowNum < 0";
        DataTable dtMax = new DataTable();
        DataTable dtNormal = new DataTable();

        while (true)
        {
            iRet = SelectSql(SQL, connMaxStore, ref dtMax);
            if (iRet != 0) break;
            if(IP=="76")
            iRet = SelectSql(SQL, connNormal, ref dtNormal);
            if (IP == "215")
            iRet = SelectSql(SQL, connNormalBakup, ref dtNormal);
            if (iRet != 0) break;

            if (dtMax.Columns.Count != dtNormal.Columns.Count)
            {
                iRet = 1;
                FERROR = "Source  columns count is different with destination ";
                break;
            }

            #region CheckColumns
            for (i = 0; i < dtMax.Columns.Count; i++)
            {
                string ColumnName = dtMax.Columns[i].ColumnName;
                try
                {
                    if (dtNormal.Columns[ColumnName].DataType.ToString() != dtMax.Columns[i].DataType.ToString())
                    {
                        iRet = 2;
                        FERROR = "";
                        break;
                    }
                }
                catch
                {
                    iRet = 3;
                    FERROR = "Not found Column: " + ColumnName + " in destination";
                    break;
                }
            }
            if (iRet == 3) break;

            #endregion

            #region CheckUniqueColumns
            for (i = 0; i < Unique_Columns.GetLength(0); i++)
            {
                try
                {
                    string Tmp = dtMax.Columns[Unique_Columns[i]].ColumnName;
                }
                catch
                {
                    iRet = 4;
                    FERROR = "Unique Column " + Unique_Columns[i] + " not in table ";
                    break;
                }
            }

            if (iRet == 4) break;
            #endregion

            Columns = new string[dtMax.Columns.Count];
            for (i = 0; i < dtMax.Columns.Count; i++)
                Columns[i] = dtMax.Columns[i].ColumnName;
            iRet = 0;
            break;
        }
        dtNormal.Dispose();
        dtMax.Dispose();
        return iRet;
    }
    private int GetDataNormalFromTable(string TableName, string Date_Column, string[] Columns, string SN, ref DataTable dt)
    {
        int iRet, i;
        string struser = TableName.Substring(0, 1);
        string SQL = "Select ";
        for (i = 0; i < Columns.GetLength(0); i++)
        {
            SQL = SQL + Columns[i];
            if (i != Columns.GetLength(0) - 1) SQL += ",";
        }
        SQL += " From " + TableName;
        SQL += " Where " + Date_Column + "='" + SN + "'";
        iRet = SelectSql(SQL, connMaxStore, ref dt);
        return iRet;

    }
    private int InsertDateToNormalTable(string TableName, string IP,string[] Unique_Columns, DataTable dt, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        int iRet, i;
        #region InsertSQL
        string SQL = "Insert Into " + TableName + "(";
        string SQLValue = "Values (";
        for (i = 0; i < dt.Columns.Count; i++)
        {
            SQL += dt.Columns[i].ColumnName;
            SQLValue += ":V" + i.ToString();
            if (i != dt.Columns.Count - 1)
            {
                SQL += ",";
                SQLValue += ",";
            }
            else
            {
                SQL += ") ";
                SQLValue += ") ";
            }
        }
        SQL += SQLValue;
        #endregion

        #region UniqueSQL

        string SelectSQL = "Select 1 From " + TableName + " Where ";
        for (i = 0; i < Unique_Columns.GetLength(0); i++)
            SelectSQL += Unique_Columns[i] + " = :V" + i.ToString() + " And ";
        SelectSQL += " RowNum <= 1";

        #endregion

        SuccessCount = 0;
        FailCount = 0;
        DuplicateCount = 0;
        OracleCommand cmd = null;
        OracleCommand cmdSel = null;
        if (IP == "76")
        {
            cmd = new OracleCommand(SQL, connNormal);
            cmdSel = new OracleCommand(SelectSQL, connNormal);
        }
        if (IP == "215")
        {
            cmd = new OracleCommand(SQL, connNormalBakup);
            cmdSel = new OracleCommand(SelectSQL, connNormalBakup);
        }
        
        OracleDataAdapter oda = new OracleDataAdapter();
        iRet = CheckConnect(connMaxStore);
        for (i = 0; i < dt.Rows.Count; i++)
        {
            int iCol;
            DataTable dtSel = new DataTable();
            try
            {
                cmdSel.Parameters.Clear();
                for (iCol = 0; iCol < Unique_Columns.GetLength(0); iCol++)
                {
                    cmdSel.Parameters.AddWithValue("V" + iCol.ToString(), dt.Rows[i][Unique_Columns[iCol]]);
                }
                oda.SelectCommand = cmdSel;
                oda.Fill(dtSel);
                if (dtSel.Rows.Count > 0)
                {
                    DuplicateCount++;

                }
                else
                {
                    cmd.Parameters.Clear();
                    for (iCol = 0; iCol < dt.Columns.Count; iCol++)
                    {
                        cmd.Parameters.AddWithValue("V" + iCol.ToString(), dt.Rows[i][iCol]);
                    }
                    cmd.ExecuteNonQuery();
                    SuccessCount++;
                }
            }
            catch
            {
                FailCount++;
            }
            finally
            {
                if (dtSel != null) dtSel.Dispose();

            }
        }
        if (cmd != null) cmd.Dispose();
        if (oda != null) oda.Dispose();
        if (cmdSel != null) cmdSel.Dispose();

        return iRet;
    }
    public int InsertNormal_testinfo_HEAD(string DN,string IP,ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "TESTINFO.TESTINFO_HEAD";
        string Date_Column = "TRACK_ID";
        string[] Unique_Columns = new string[5];
        Unique_Columns[0] = "ID";
        Unique_Columns[1] = "CREATE_DATE";
        Unique_Columns[2] = "TRACK_ID";
        Unique_Columns[3] = "STATION_NAME";
        Unique_Columns[4] = "STATUS";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckNormalTable(TableName,IP,Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataNormalFromTable(TableName, Date_Column, Columns, DN, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToNormalTable(TableName, IP,Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet;
    }
    public int InsertNormal_wcdma_tse_R_FUNCTION_HEAD_T(string DN, string IP, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "WCDMA_TSE.R_FUNCTION_HEAD_T";
        string Date_Column = "SERIAL_NUMBER";
        string[] Unique_Columns = new string[3];
        Unique_Columns[0] = "SERIAL_NUMBER";
        Unique_Columns[1] = "GROUP_NAME";
        Unique_Columns[2] = "TEST_TIME";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckNormalTable(TableName, IP, Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataNormalFromTable(TableName, Date_Column, Columns, DN, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToNormalTable(TableName, IP, Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet;     
    }
    public int InsertNormal_wcdma_tse_R_FUNCTION_DETAIL_T(string DN, string IP, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "WCDMA_TSE.R_FUNCTION_DETAIL_T";
        string Date_Column = "SERIAL_NUMBER";
        string[] Unique_Columns = new string[5];
        Unique_Columns[0] = "SERIAL_NUMBER";
        Unique_Columns[1] = "TEST_TIME";
        Unique_Columns[2] = "STATUS";
        Unique_Columns[3] = "ITEM_KEY";
        Unique_Columns[4] = "SEQ";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckNormalTable(TableName, IP, Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataNormalFromTable(TableName, Date_Column, Columns, DN, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToNormalTable(TableName, IP, Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet; 

    }
    public int InsertNormal_testinfo_DETAIL(string DN, string IP, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "TESTINFO.TESTINFO_DETAIL";
        string Date_Column = "TRACK_ID";
        string[] Unique_Columns = new string[5];
        Unique_Columns[0] = "TRACK_ID";
        Unique_Columns[1] = "ID";
        Unique_Columns[2] = "CREATE_DATE";
        Unique_Columns[3] = "STATION_NAME";
        Unique_Columns[4] = "STATUS";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckNormalTable(TableName, IP, Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataNormalFromTable(TableName, Date_Column, Columns, DN, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToNormalTable(TableName, IP, Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet; 
    }
    public int InsertNormal_wcdma_tse_R_FUNCTION_LOG_T(string DN, string IP, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)


    {
        string TableName = "WCDMA_TSE.R_FUNCTION_LOG_T";
        string Date_Column = "SERIAL_NUMBER";
        string[] Unique_Columns = new string[4];
        Unique_Columns[0] = "SERIAL_NUMBER";
        Unique_Columns[1] = "LINE_NAME";
        Unique_Columns[2] = "GROUP_NAME";
        Unique_Columns[3] = "TEST_TIME";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckNormalTable(TableName, IP, Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataNormalFromTable(TableName, Date_Column, Columns, DN, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToNormalTable(TableName, IP, Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet; 

    }
    public int InsertNormal_sfc_R_WIP_TRACKING_T_PID(string DN, string IP, ref int SuccessCount, ref int FailCount, ref int DuplicateCount)
    {
        string TableName = "SFC.R_WIP_TRACKING_T_PID";
        string Date_Column = "SERIAL_NUMBER";
        string[] Unique_Columns = new string[4];
        Unique_Columns[0] = "SERIAL_NUMBER";
        Unique_Columns[1] = "SECTION_NAME";
        Unique_Columns[2] = "GROUP_NAME";
        Unique_Columns[3] = "IN_STATION_TIME";

        int iRet;
        string[] Columns = new string[0];
        DataTable dt = new DataTable();
        while (true)
        {
            iRet = CheckNormalTable(TableName, IP, Unique_Columns, ref Columns);
            if (iRet != 0) break;
            iRet = GetDataNormalFromTable(TableName, Date_Column, Columns, DN, ref dt);
            if (iRet != 0) break;
            iRet = InsertDateToNormalTable(TableName, IP, Unique_Columns, dt, ref SuccessCount, ref FailCount, ref DuplicateCount);
            break;
        }
        if (iRet != 0) FERROR = "Table: " + TableName + " " + FERROR;
        if (dt != null) dt.Dispose();
        return iRet;
    }
    #endregion
}
