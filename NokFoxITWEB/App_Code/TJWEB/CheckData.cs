using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

using System.IO;
using System.Text;

/// <summary>
/// Summary description for CheckData
/// </summary>
public class CheckData
{

    private OracleConnection connNormal;
    private string FERROR;

	public CheckData()
	{
        string strConnNormal = ConfigurationManager.AppSettings["bjl6testConnectionString"];
        connNormal = new OracleConnection(strConnNormal);
        FERROR = "";
        
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetFerror()
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

    private int SelectSql(string SQL, OracleConnection conn,ref DataTable dt)
    {
        int iRet;
        CheckConnect(conn);
        OracleDataAdapter oda = null;
        OracleCommand cmd = null;
        try
        {
            cmd = new OracleCommand(SQL, connNormal);
            oda = new OracleDataAdapter();
            iRet = CheckConnect(connNormal);
            oda.SelectCommand = cmd;
            oda.Fill(dt);         
            iRet = 0;
        }
        catch (Exception ex)
        {
            iRet = -1;
            FERROR = ex.Message;
        }
        finally
        {
            if (oda != null) oda.Dispose();
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    public int InsertData(string TableName,string DocumentID,DataTable dt,ref int InsertsueCont,ref int InsertErrCount,ref int UpdateSueCount,ref int UpdateErrCount )
    {
        int iRet = 0;
        int rowCount = dt.Rows.Count;
        int colCount = dt.Columns.Count;
        string PID = string.Empty;
        string Carton_ID = string.Empty;
        string strSql = string.Empty;
        for (int i = 0; i < rowCount; i++)
        {

            if (TableName == "SFC.R_WIP_TRACKING_T" || TableName == "SFC.R_WIP_TRACKING_T_PID")
            {
                PID = dt.Rows[i][0].ToString().Trim();
                if (PID.Length == 0 || PID == "")
                {
                    continue;
                }
            }
            else if (TableName == "TESTINFO.TESTINFO_HEAD")
            {
                PID = dt.Rows[i][3].ToString().Trim();
                if (PID.Length == 0 || PID == "")
                {
                    continue;
                }
            }
            else if (TableName == "SFC.MES_ASSY_HISTORY")
            {
                PID = dt.Rows[i][1].ToString().Trim();
                if (PID.Length == 0 || PID == "")
                {
                    continue;
                }
            }
            else if (TableName == "CARTON_PSN")
            {
                PID = dt.Rows[i][0].ToString().Trim();
                Carton_ID = dt.Rows[i][1].ToString().Trim();

            }
            else
            {
                PID = dt.Rows[i][1].ToString().Trim();
                if (PID.Length == 0 || PID == "")
                {
                    continue;
                }
            }
            try
            {
                DataTable dtSel = new DataTable();
                strSql = @"SELECT * FROM SFIS1.CHECK_DATA WHERE DOCUMENTID='"+DocumentID+"' AND PSN='"+PID+"'";
                iRet = SelectSql(strSql, connNormal,ref dtSel);
                if (iRet == 0)
                {
                    if (dtSel.Rows.Count <= 0)
                    {
                        Insertprimary(TableName, DocumentID, PID,Carton_ID);
                        InsertsueCont++;

                    }
                    else
                    {
                        UpdatePrimary(TableName, DocumentID, PID);
                        UpdateSueCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                InsertErrCount++;
                UpdateErrCount++;
                FERROR = ex.Message;
                iRet = -1;

            }
        }
        return iRet;

    }

    public int InsertCartonData(string TableName, string DocumentID, DataTable dt, ref int UpdateCountSucessTemp, ref int UpdateCountFailErrorTemp, ref int UpdateCountErrorTemp)
    {
        int iRet = 0;
        int rowCount = dt.Rows.Count;
        int colCount = dt.Columns.Count;
        string PID = string.Empty;
        string Carton_ID = string.Empty;
        string strSql = string.Empty;
        for (int i = 0; i < rowCount; i++)
        {
             PID = dt.Rows[i][0].ToString().Trim();
             Carton_ID = dt.Rows[i][1].ToString().Trim();
            try
            {
                DataTable dtSel = new DataTable();
                strSql = @"SELECT * FROM SFIS1.CHECK_DATA WHERE DOCUMENTID='" + DocumentID + "' AND PSN='" + PID + "'";
                iRet = SelectSql(strSql, connNormal, ref dtSel);
                if (iRet == 0)
                {
                    if (dtSel.Rows.Count >= 0)
                    {
                        UdateCarPrimary(TableName, DocumentID, PID, Carton_ID);
                        UpdateCountSucessTemp++;

                    }
                    else
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                UpdateCountFailErrorTemp++;
                FERROR = ex.Message;
                iRet = -1;

            }
        }
        return iRet;
    }

    private int Insertprimary(string TableName, string DocumentID, string PSN, string Carton_ID)
    {
        int iRet = 0;
        string strSql = string.Empty;
         OracleParameter[] Pram = new OracleParameter[3];
        
        if (TableName == "SFC.R_WIP_TRACKING_T")
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F0) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");

        }
        else if (TableName == "SFC.R_WIP_TRACKING_T_PID")
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F1) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");
        }
        else if (TableName == "SFC.MES_ASSY_HISTORY")
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F2) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");

        }
        else if (TableName == "SFC.MES_ASSY_PID_JOIN")
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F3) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");
        }
        else if (TableName == "SFC.MES_ASSY_WIP")
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F4) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");
        }
        else if (TableName == "SFC.MES_PCBA_HISTORY")
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F5) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");
        }
        else if (TableName == "TESTINFO.TESTINFO_HEAD")
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F6) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");
        }
        else
        {
            strSql = @"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,F7) VALUES(:V_MENTID,:V_PSN,:V_F)";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
            Pram[2] = new OracleParameter("V_F", "1");
        }

        OracleCommand cmd = null;
        try
        {
            iRet = CheckConnect(connNormal);
            cmd = new OracleCommand(strSql, connNormal);
            cmd.Parameters.Clear();
            foreach (OracleParameter par in Pram)
            {
                cmd.Parameters.Add(par);
            }
            int ii = cmd.ExecuteNonQuery();
            iRet = 0;

        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        if (iRet != 0) FERROR = "TableName:"+TableName +" "+ FERROR;
        return iRet;
    }

    private int UpdatePrimary(string TableName, string DocumentID, string PSN)
    {
        int iRet = 0;
        string strSql = string.Empty;
        OracleParameter[] Pram = new OracleParameter[2];

        if (TableName == "SFC.R_WIP_TRACKING_T")
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F0=TO_CHAR(cast(DECODE(F0,'',0,F0) as VARCHAR(5))+1) WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }
        else if (TableName == "SFC.R_WIP_TRACKING_T_PID")
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F1=TO_CHAR(cast(DECODE(F1,'',0,F1) as VARCHAR(5))+1) WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }
        else if (TableName == "SFC.MES_ASSY_HISTORY")
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F2=TO_CHAR(cast(DECODE(F2,'',0,F2) as VARCHAR(5))+1) WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }
        else if (TableName == "SFC.MES_ASSY_PID_JOIN")
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F3=TO_CHAR(cast(DECODE(F3,'',0,F3) as VARCHAR(5))+1) WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }
        else if (TableName == "SFC.MES_ASSY_WIP")
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F4=TO_CHAR(cast(DECODE(F4,'',0,F4) as VARCHAR(5))+1) WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }
        else if (TableName == "SFC.MES_PCBA_HISTORY")
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F5=TO_CHAR(cast(DECODE(F5,'',0,F5) as VARCHAR(5))+1) WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }
        else if (TableName == "TESTINFO.TESTINFO_HEAD")
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F6=TO_CHAR(cast(DECODE(F6,'',0,F6) as VARCHAR(5))+1) WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }
        else
        {
            strSql = @"UPDATE SFIS1.CHECK_DATA SET F7=TO_CHAR(cast(DECODE(F7,'',0,F7) as VARCHAR(5))+1) WHERE PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_MENTID", DocumentID);
            Pram[1] = new OracleParameter("V_PSN", PSN);
        }

        OracleCommand cmd = null;
        try
        {
            iRet = CheckConnect(connNormal);
            cmd = new OracleCommand(strSql, connNormal);
            cmd.Parameters.Clear();
            foreach (OracleParameter par in Pram)
            {
                cmd.Parameters.Add(par);
            }
            int ii = cmd.ExecuteNonQuery();
            iRet = 0;

        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    private int UdateCarPrimary(string TableName, string DocumentID, string PSN, string Carton_ID)
    {
        int iRet = 0;
        string strSql = string.Empty;
        OracleParameter[] Pram = new OracleParameter[2];
        strSql = @"UPDATE SFIS1.CHECK_DATA SET CARTON='" + Carton_ID + "' WHERE DOCUMENTID=:V_MENTID AND PSN=:V_PSN";
        Pram[0] = new OracleParameter("V_MENTID",DocumentID);
        Pram[1] = new OracleParameter("V_PSN", PSN);
        OracleCommand cmd = null;
        try
        {
            iRet = CheckConnect(connNormal);
            cmd = new OracleCommand(strSql, connNormal);
            cmd.Parameters.Clear();
            foreach (OracleParameter par in Pram)
            {
                cmd.Parameters.Add(par);
            }
            int ii = cmd.ExecuteNonQuery();
            iRet = 0;

        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;

    }

    private int InsertCarPrimary(string TableName, string DocumentID, string PSN, string Carton_ID)
    {
        int iRet = 0;
        string strSql = string.Empty;
        strSql=@"INSERT INTO SFIS1.CHECK_DATA(DOCUMENTID,PSN,CARTON,RECSTATUS) VALUES(:V_MENTID,:V_PSN,'"+Carton_ID+"','E')";
        OracleParameter[] Pram = new OracleParameter[2];
        Pram[0] = new OracleParameter("V_MENTID", DocumentID);
        Pram[1] = new OracleParameter("V_PSN", PSN);
        OracleCommand cmd = null;
        try
        {
            iRet = CheckConnect(connNormal);
            cmd = new OracleCommand(strSql, connNormal);
            cmd.Parameters.Clear();
            foreach (OracleParameter par in Pram)
            {
                cmd.Parameters.Add(par);
            }
            int ii = cmd.ExecuteNonQuery();
            iRet = 0;

        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;  
    }

    public int CollectData(string DocumentID,ref int AllCount, ref int RightCount, ref DataTable dt,string strSwicthValue)
    {
        int iRet = 0;
        string strSql = @"SELECT DOCUMENTID,PSN,F0,F1,F2,F3,F4,F5,F6 FROM sfis1.check_data WHERE DOCUMENTID='" + DocumentID + "'";
        DataTable dtAll = new DataTable();
        try
        {
            iRet = SelectSql(strSql, connNormal, ref dtAll);
            if (dtAll.Rows.Count > 0)
            {
                AllCount = dtAll.Rows.Count;
                for (int i = 0; i < dtAll.Rows.Count; i++)
                {
                    string RowF0 = dtAll.Rows[i][2].ToString();
                    string RowF1 = dtAll.Rows[i][3].ToString();
                    string RowF2 = dtAll.Rows[i][4].ToString();
                    string RowF3 = dtAll.Rows[i][5].ToString();
                    string RowF4 = dtAll.Rows[i][6].ToString();
                    string RowF5 = dtAll.Rows[i][7].ToString();
                    string RowF6 = dtAll.Rows[i][8].ToString();
                    if (strSwicthValue == "A")
                    {
                        if (RowF1.Length != 0 && RowF2.Length != 0 && RowF3.Length != 0 && RowF4.Length != 0 && RowF5.Length != 0 && RowF6.Length != 0)
                        {
                            RightCount++;
                        }
                    }
                    else
                    {
                        if (RowF1.Length != 0 &&  RowF3.Length != 0 && RowF4.Length != 0 && RowF5.Length != 0 && RowF6.Length != 0)
                        {
                            RightCount++;
                        }
                    }
       
                }
            
            }
            dt = dtAll;
              
        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }


        return iRet;
    }

    public int   PageGet(string SQL, ref DataTable dt)
    {
        int iRet = 0;
        DataTable dtAll = new DataTable();
        try
        {
            iRet = SelectSql(SQL, connNormal, ref dtAll);
            dt = dtAll;

        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }


        return iRet;
    }

    public bool CheckField(string TableName, DataTable dt, ref int errortempCount, ref string ErrorMessage)
    {
        bool iRet = true;
        int RowCount = dt.Rows.Count;
        int ColCount = dt.Columns.Count;
        string strField = string.Empty;
        string ErrorMes = string.Empty;
        string  ReadPath = AppDomain.CurrentDomain.BaseDirectory + "\\CheckLog\\";
        ErrorMessage = string.Empty;
        StringBuilder sbs = new StringBuilder();
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                 strField = dt.Rows[i][j].ToString();
                 bool CheckiRet = CheckFiedlValue(TableName, strField, i, j, ref ErrorMes);
                 if (!CheckiRet)
                 {
                     sbs.Append(ErrorMes + "\r\n");
                     errortempCount++;
                     iRet = false;
                 }
            }
        }
        if (!iRet)
        {
            if (!Directory.Exists(ReadPath))
            {
                Directory.CreateDirectory(ReadPath);

            }
            string CreateFileName=ReadPath + TableName+".txt";
            bool ReadRet = FileOperation.CreateFile(sbs, CreateFileName);
            sbs = null;
        }
            
        return iRet;
    }

    private bool CheckFiedlValue(string TableName, string FiledValue, int Rows, int Cols, ref string ErrorMes)
    {
        bool iRet = false;
        int FieldIndex=Cols;
        ErrorMes = "";
        if (TableName == "SFC.R_WIP_TRACKING_T")
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }
        else if (TableName == "SFC.R_WIP_TRACKING_T_PID")
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }
        else if (TableName == "SFC.MES_ASSY_HISTORY")
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }
        else if (TableName == "SFC.MES_ASSY_PID_JOIN")
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }
        else if (TableName == "SFC.MES_ASSY_WIP")
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }
        else if (TableName == "SFC.MES_PCBA_HISTORY")
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }
        else if (TableName == "TESTINFO.TESTINFO_HEAD")
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }
        else
        {
            switch (FieldIndex)
            {
                case 1:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 2:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 3:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 4:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 5:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 6:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;
                case 7:
                    if (FiledValue == "")
                    {
                        ErrorMes = "TableName:" + TableName + " 行:" + Convert.ToString(Rows) + " 列:" + Convert.ToString(Cols) + " 為空值。";
                    }
                    else
                    {
                        iRet = true;
                    }
                    break;

                default:
                    iRet = true;
                    break;

            }
        }

        return iRet;
    }

    public int DbOperton(string strDrowpList, string strTextInput, string strRecStatus)
    {
        int iRet = 0;
        string strSql = string.Empty;
        OracleParameter[] Pram = new OracleParameter[2];
        if (strDrowpList == "DOCUMENTID")
        {
            strSql = "UPDATE SFIS1.CHECK_DATA SET RECSTATUS=:V_REC WHERE DOCUMENTID=:V_MENDID";
            Pram[0] = new OracleParameter("V_REC", strRecStatus);
            Pram[1] = new OracleParameter("V_MENDID", strTextInput);
        }
        else if (strDrowpList == "PSN")
        {
            strSql = "UPDATE SFIS1.CHECK_DATA SET RECSTATUS=:V_REC WHERE PSN=:V_PSN";
            Pram[0] = new OracleParameter("V_REC", strRecStatus);
            Pram[1] = new OracleParameter("V_PSN", strTextInput);
        }
        else
        {
            strSql = "UPDATE SFIS1.CHECK_DATA SET RECSTATUS=:V_REC WHERE CARTON=:V_CA";
            Pram[0] = new OracleParameter("V_REC", strRecStatus);
            Pram[1] = new OracleParameter("V_CA", strTextInput);
        }
        OracleCommand cmd = null;
        try
        {
            iRet = CheckConnect(connNormal);
            cmd = new OracleCommand(strSql, connNormal);
            cmd.Parameters.Clear();
            foreach (OracleParameter par in Pram)
            {
                cmd.Parameters.Add(par);
            }
            int ii = cmd.ExecuteNonQuery();
            iRet = 0;

        }
        catch (Exception ex)
        {
            FERROR = ex.Message;
            iRet = -1;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }



}
