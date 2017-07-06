using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DBAccess.EAI;

/// <summary>
/// ClsDBCpk 的摘要描述
/// </summary>
public class ClsDBCpk
{
	public ClsDBCpk()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    //public void CreateView(string sql, string viewname, String Model)
    //{
    //    OracleConnection conn = new OracleConnection(this.NewOraConn(Model));
    //    OracleCommand cmd = new OracleCommand();

    //    try
    //    {
    //        cmd.Connection = conn;
    //        cmd.Connection.Open();
    //        cmd.CommandText = sql;
    //        cmd.ExecuteReader();
    //        conn.Close();
    //        conn.Dispose();
    //        cmd.Dispose();
    //    }
    //    catch (OracleException ex)
    //    {
    //        conn.Close();
    //        conn.Dispose();
    //        cmd.Dispose();
    //    }
    //}

    ///// <summary>
    ///// Create View
    ///// </summary>
    ///// <param name="sql"></param>
    ///// <param name="Model"></param>
    //public void CreateView(string sql, string Model)
    //{
    //    OracleConnection conn = new OracleConnection(this.NewOraConn(Model));
    //    OracleCommand cmd = new OracleCommand();
    //    try
    //    {
    //        cmd.Connection = conn;
    //        cmd.Connection.Open();
    //        cmd.CommandText = sql;
    //        cmd.ExecuteReader();
    //        conn.Close();
    //        conn.Dispose();
    //        cmd.Dispose();
    //    }
    //    catch (OracleException)
    //    {
    //        conn.Close();
    //        conn.Dispose();
    //        cmd.Dispose();
    //    }
    //}

    //public void CreateViewNew(string sql, string viewname, string project)
    //{
    //    try
    //    {
    //        string str_conn = this.NewOraConn(project);
    //        if (this.Session["DataBaseType"].ToString() == "Oracle")
    //        {
    //            OracleConnection conn = new OracleConnection(str_conn);
    //            OracleCommand cmd = new OracleCommand();
    //            conn.Open();
    //            cmd.Connection = conn;
    //            cmd.CommandText = sql;
    //            cmd.ExecuteReader();
    //            conn.Close();
    //            conn.Dispose();
    //            cmd.Dispose();
    //        }
    //        else if (this.Session["DataBaseType"].ToString() != "Sql")
    //        {
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        string str_err = err.Message;
    //    }
    //}

    //public void DeleteView(string viewname, string project)
    //{
    //    string str_conn = this.NewOraConn(project);
    //    if (this.Session["DataBaseType"].ToString() == "Oracle")
    //    {
    //        OracleConnection conn = new OracleConnection(str_conn);
    //        OracleCommand cmd = new OracleCommand();
    //        string strsql = "drop view " + viewname;
    //        cmd.CommandText = strsql;
    //        cmd.Connection = conn;
    //        try
    //        {
    //            cmd.Connection.Open();
    //            cmd.ExecuteReader();
    //            conn.Close();
    //            conn.Dispose();
    //            cmd.Dispose();
    //        }
    //        catch (Exception)
    //        {
    //            conn.Close();
    //            conn.Dispose();
    //            cmd.Dispose();
    //        }
    //    }
    //    else if (this.Session["DataBaseType"].ToString() == "Sql")
    //    {
    //    }
    //}

    //public void GetComputerFromStation(ArrayList icustList, ref int icount, string sql, string project)
    //{
    //    try
    //    {
    //        OracleConnection conn = new OracleConnection(this.NewOraConn(project));
    //        conn.Open();
    //        OracleDataReader oqlr = null;
    //        OracleCommand oralcommand = new OracleCommand(sql, conn);
    //        oqlr = oralcommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["computer_name"].ToString());
    //        }
    //        icount = icustList.Count;
    //        conn.Close();
    //        conn.Dispose();
    //        oralcommand.Dispose();
    //    }
    //    catch (OracleException)
    //    {
    //    }
    //}

    //public int GetCountRecord(string sql, string project)
    //{
    //    try
    //    {
    //        OracleConnection conn = new OracleConnection(this.NewOraConn(project));
    //        OracleCommand cmd = new OracleCommand();
    //        OracleDataReader sqlr = null;
    //        cmd.CommandText = sql;
    //        cmd.Connection = conn;
    //        cmd.Connection.Open();
    //        sqlr = cmd.ExecuteReader();
    //        sqlr.Read();
    //        string strcount = sqlr["total"].ToString();
    //        conn.Close();
    //        conn.Dispose();
    //        cmd.Dispose();
    //        return int.Parse(strcount);
    //    }
    //    catch (OracleException oraEx)
    //    {
    //        string ex = oraEx.Message;
    //        return 0;
    //    }
    //}

    //public double GetCountRecordDouble(string sql, string project)
    //{
    //    try
    //    {
    //        OracleConnection conn = new OracleConnection(this.NewOraConn(project));
    //        OracleCommand cmd = new OracleCommand();
    //        OracleDataReader sqlr = null;
    //        cmd.CommandText = sql;
    //        cmd.Connection = conn;
    //        cmd.Connection.Open();
    //        sqlr = cmd.ExecuteReader();
    //        sqlr.Read();
    //        string strcount = sqlr["total"].ToString();
    //        conn.Close();
    //        conn.Dispose();
    //        cmd.Dispose();
    //        return double.Parse(strcount);
    //    }
    //    catch (OracleException)
    //    {
    //        return 0.0;
    //    }
    //}

    //public void GetCountRecordIntoArray(string[,] storedata1, ref int icount, string sql, int i, object itestitemname, string viewname, string project)
    //{
    //    try
    //    {
    //        string str_conn = this.NewOraConn(project);
    //        if (this.Session["DataBaseType"].ToString() == "Oracle")
    //        {
    //            OracleConnection conn = new OracleConnection(str_conn);
    //            conn.Open();
    //            OracleDataReader oqlr = null;
    //            oqlr = new OracleCommand(sql, conn).ExecuteReader();
    //            while (oqlr.Read())
    //            {
    //                storedata1[i, 0] = oqlr["total"].ToString();
    //                storedata1[i, 1] = oqlr["low"].ToString();
    //                storedata1[i, 2] = oqlr["high"].ToString();
    //                storedata1[i, 3] = oqlr["avg"].ToString();
    //                storedata1[i, 4] = oqlr["min"].ToString();
    //                storedata1[i, 5] = oqlr["max"].ToString();
    //            }
    //            NumberFormatInfo mynfi = new NumberFormatInfo();
    //            mynfi.NumberDecimalDigits = 2;
    //            int iCount1 = int.Parse(storedata1[i, 0].ToString());
    //            float valuelow = float.Parse(storedata1[i, 1].ToString());
    //            float valuehigh = float.Parse(storedata1[i, 2].ToString());
    //            float iTotalAve = float.Parse(storedata1[i, 3].ToString());
    //            valuelow = float.Parse(valuelow.ToString("F3"));
    //            valuehigh = float.Parse(valuehigh.ToString("F3"));
    //            iTotalAve = float.Parse(iTotalAve.ToString("F3"));
    //            storedata1[i, 1] = valuelow.ToString();
    //            storedata1[i, 2] = valuehigh.ToString();
    //            storedata1[i, 3] = iTotalAve.ToString();
    //            float fSigma = 0f;
    //            float iHigh = float.Parse(storedata1[i, 5].ToString());
    //            float iLow = float.Parse(storedata1[i, 4].ToString());
    //            iHigh = float.Parse(iHigh.ToString("F3"));
    //            iLow = float.Parse(iLow.ToString("F3"));
    //            storedata1[i, 4] = iLow.ToString();
    //            storedata1[i, 5] = iHigh.ToString();
    //            string sqlgetvalue = "select round(value,3) as value from " + viewname + " where testitem='" + itestitemname.ToString() + "'";
    //            OracleDataReader oqlr1 = null;
    //            oqlr1 = new OracleCommand(sqlgetvalue, conn).ExecuteReader();
    //            for (int iii = 0; oqlr1.Read(); iii++)
    //            {
    //                float fff = float.Parse(oqlr1["value"].ToString()) - iTotalAve;
    //                double ddd = Math.Pow((double)fff, 2.0);
    //                fSigma += float.Parse(ddd.ToString(), mynfi);
    //            }
    //            if (iCount1 != 1)
    //            {
    //                fSigma /= (float)(iCount1 - 1);
    //            }
    //            else
    //            {
    //                fSigma = 0f;
    //            }
    //            fSigma = float.Parse(Math.Sqrt((double)fSigma).ToString("F5"));
    //            storedata1[i, 6] = fSigma.ToString();
    //            float fCPK = 0f;
    //            if (fSigma == 0f)
    //            {
    //                fCPK = 0f;
    //            }
    //            else
    //            {
    //                fCPK = Math.Min((float)((iHigh - iTotalAve) / (3f * fSigma)), (float)((iTotalAve - iLow) / (3f * fSigma)));
    //            }
    //            storedata1[i, 7] = float.Parse(fCPK.ToString("F3")).ToString();
    //            conn.Close();
    //        }
    //        else if (this.Session["DataBaseType"].ToString() != "Sql")
    //        {
    //        }
    //    }
    //    catch (OracleException)
    //    {
    //    }
    //}

    //public void GetCountRecordIntoArray1(string[] storedata1, string sql, string itestitemname, string viewname, string model)
    //{
    //    try
    //    {
    //        string str_conn = this.NewOraConn(model);
    //        if (this.Session["DataBaseType"].ToString() == "Oracle")
    //        {
    //            OracleConnection conn = new OracleConnection(str_conn);
    //            conn.Open();
    //            OracleDataReader oqlr = null;
    //            OracleCommand oralcommand = new OracleCommand(sql, conn);
    //            oqlr = oralcommand.ExecuteReader();
    //            while (oqlr.Read())
    //            {
    //                storedata1[0] = oqlr["total"].ToString();
    //                storedata1[1] = oqlr["low"].ToString();
    //                storedata1[2] = oqlr["high"].ToString();
    //                storedata1[3] = oqlr["avg"].ToString();
    //                storedata1[4] = oqlr["min"].ToString();
    //                storedata1[5] = oqlr["max"].ToString();
    //            }
    //            int iCount1 = int.Parse(storedata1[0].ToString());
    //            float valuelow = float.Parse(storedata1[1].ToString());
    //            float valuehigh = float.Parse(storedata1[2].ToString());
    //            float iTotalAve = float.Parse(storedata1[3].ToString());
    //            valuelow = float.Parse(valuelow.ToString("F3"));
    //            valuehigh = float.Parse(valuehigh.ToString("F3"));
    //            iTotalAve = float.Parse(iTotalAve.ToString("F3"));
    //            storedata1[1] = valuelow.ToString();
    //            storedata1[2] = valuehigh.ToString();
    //            storedata1[3] = iTotalAve.ToString();
    //            float fSigma = 0f;
    //            float iHigh = float.Parse(storedata1[5].ToString());
    //            float iLow = float.Parse(storedata1[4].ToString());
    //            iHigh = float.Parse(iHigh.ToString("F3"));
    //            iLow = float.Parse(iLow.ToString("F3"));

    //            //T & C
    //            float varT = iHigh - iLow;
    //            float varC = (iHigh + iLow) / 2;

    //            storedata1[4] = iLow.ToString();
    //            storedata1[5] = iHigh.ToString();

    //            //Caculate Sigma
    //            string sqlgetvalue = "select round(value,3) as value from " + viewname;
    //            OracleDataReader oqlr1 = null;
    //            OracleCommand oralcommand1 = new OracleCommand(sqlgetvalue, conn);
    //            oqlr1 = oralcommand1.ExecuteReader();
    //            for (int iii = 0; oqlr1.Read(); iii++)
    //            {
    //                float fff = float.Parse(oqlr1["value"].ToString()) - iTotalAve;
    //                double ddd = Math.Pow((double)fff, 2.0);
    //                fSigma += float.Parse(ddd.ToString("F5"));
    //            }
    //            if (iCount1 != 1)
    //            {
    //                fSigma /= (float)(iCount1 - 1);
    //            }
    //            else
    //            {
    //                fSigma = 0f;
    //            }
    //            fSigma = float.Parse(Math.Sqrt((double)fSigma).ToString("F5"));
    //            storedata1[6] = fSigma.ToString();

    //            //Caculate CPK
    //            float fCPK = 0f;
    //            if (fSigma == 0f)
    //            {
    //                fCPK = 0f;
    //            }
    //            else
    //            {
    //                //fCPK = Math.Min((float) ((iHigh - iTotalAve) / (3f * fSigma)), (float) ((iTotalAve - iLow) / (3f * fSigma)));
    //                fCPK = (varT - (Math.Abs(iTotalAve - varC) * 2)) / (6 * fSigma);
    //            }
    //            storedata1[7] = float.Parse(fCPK.ToString("F3")).ToString();
    //            conn.Close();
    //            conn.Dispose();
    //            oralcommand.Dispose();
    //            oralcommand1.Dispose();
    //        }
    //        else if (this.Session["DataBaseType"].ToString() != "Sql")
    //        {
    //        }
    //    }
    //    catch (OracleException)
    //    {
    //    }
    //}

    //public void GetCountRecordIntoArrayNew(string[,] storedata1, ref int icount, string sql, int i, object itestitemname, string viewname, string project, OracleConnection conn, OracleCommand cmd)
    //{
    //    try
    //    {
    //        OracleDataReader oqlr = null;
    //        cmd.CommandText = sql;
    //        oqlr = cmd.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            storedata1[i, 0] = oqlr["total"].ToString();
    //            storedata1[i, 1] = oqlr["low"].ToString();
    //            storedata1[i, 2] = oqlr["high"].ToString();
    //            storedata1[i, 3] = oqlr["avg"].ToString();
    //            storedata1[i, 4] = oqlr["min"].ToString();
    //            storedata1[i, 5] = oqlr["max"].ToString();
    //        }
    //        NumberFormatInfo mynfi = new NumberFormatInfo();
    //        mynfi.NumberDecimalDigits = 2;
    //        int iCount1 = int.Parse(storedata1[i, 0].ToString());
    //        float valuelow = float.Parse(storedata1[i, 1].ToString());
    //        float valuehigh = float.Parse(storedata1[i, 2].ToString());

    //        //AVG value
    //        float iTotalAve = float.Parse(storedata1[i, 3].ToString());

    //        valuelow = float.Parse(valuelow.ToString("F3"));
    //        valuehigh = float.Parse(valuehigh.ToString("F3"));
    //        iTotalAve = float.Parse(iTotalAve.ToString("F3"));
    //        storedata1[i, 1] = valuelow.ToString();
    //        storedata1[i, 2] = valuehigh.ToString();
    //        storedata1[i, 3] = iTotalAve.ToString();
    //        float fSigma = 0f;
    //        float iHigh = float.Parse(storedata1[i, 5].ToString());
    //        float iLow = float.Parse(storedata1[i, 4].ToString());
    //        iHigh = float.Parse(iHigh.ToString("F3"));
    //        iLow = float.Parse(iLow.ToString("F3"));

    //        //LSL
    //        storedata1[i, 4] = iLow.ToString();
    //        //USL
    //        storedata1[i, 5] = iHigh.ToString();

    //        //T & C
    //        float varT = iHigh - iLow;
    //        float varC = (iHigh + iLow) / 2;

    //        string sqlgetvalue = "select round(value,3) as value from " + viewname + " where descriptions='" + itestitemname.ToString() + "'";
    //        OracleDataReader oqlr1 = null;
    //        cmd.CommandText = sqlgetvalue;
    //        oqlr1 = cmd.ExecuteReader();

    //        //Xi-avg
    //        //pow( (Xi-avg), 2 )
    //        //Add ( pow( (Xi-avg), 2 ) ) 
    //        //( Add ( pow( (Xi-avg), 2 ) ) ) /(n-1)
    //        //Sigma= sqrt( ( Add ( pow( (Xi-avg), 2 ) ) ) /(n-1) )      (i=1....n)
    //        for (int iii = 0; oqlr1.Read(); iii++)
    //        {
    //            float fff = float.Parse(oqlr1["value"].ToString()) - iTotalAve;
    //            double ddd = Math.Pow((double)fff, 2.0);
    //            fSigma += float.Parse(ddd.ToString(), mynfi);
    //        }
    //        if (iCount1 > 1)
    //        {
    //            fSigma /= (float)(iCount1 - 1);
    //        }
    //        else
    //        {
    //            fSigma = 0f;
    //        }
    //        fSigma = float.Parse(Math.Sqrt((double)fSigma).ToString("F5"));

    //        //Sigma
    //        storedata1[i, 6] = fSigma.ToString();
    //        float fCPK = 0f;

    //        //CPK = ( T - 2| AVG - C | ) / 6Sigma
    //        if (fSigma == 0f)
    //        {
    //            fCPK = 0f;
    //        }
    //        else
    //        {
    //            //fCPK = Math.Min((float) ((iHigh - iTotalAve) / (3f * fSigma)), (float) ((iTotalAve - iLow) / (3f * fSigma)));
    //            fCPK = (varT - (Math.Abs(iTotalAve - varC) * 2)) / (6 * fSigma);
    //        }
    //        storedata1[i, 7] = float.Parse(fCPK.ToString("F3")).ToString();
    //    }
    //    catch (OracleException ex)
    //    {
    //    }
    //}

    //public int GetCountRecordNew(string sql, string project, OracleConnection conn, OracleCommand cmd)
    //{
    //    try
    //    {
    //        OracleDataReader sqlr = null;
    //        cmd.CommandText = sql;
    //        sqlr = cmd.ExecuteReader();
    //        //return val;
    //        sqlr.Read();
    //        return int.Parse(sqlr["total"].ToString());
    //    }
    //    catch (OracleException emsg)
    //    {
    //        string errStr = emsg.Message;
    //        return 0;
    //    }
    //}

    ///// <summary>
    ///// GET Count New
    ///// </summary>
    ///// <param name="sql"></param>
    ///// <param name="conn"></param>
    ///// <param name="cmd"></param>
    ///// <returns></returns>
    //public string GetCountRecordNew(string sql, ref int computerQTY, ref int snQTY, OracleConnection conn, OracleCommand cmd)
    //{
    //    try
    //    {
    //        OracleDataReader sqlr = null;
    //        cmd.Connection = conn;
    //        cmd.CommandText = sql;

    //        //羲蟀諉
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();

    //        sqlr = cmd.ExecuteReader();
    //        //return val;
    //        sqlr.Read();
    //        computerQTY = int.Parse(sqlr["computertotal"].ToString());
    //        snQTY = int.Parse(sqlr["sntotal"].ToString());
    //        return null;
    //    }
    //    catch (OracleException emsg)
    //    {
    //        conn.Close();
    //        string errStr = emsg.Message;
    //        return errStr;
    //    }
    //}

    ///// <summary>
    ///// Overload GetCountRecordNew
    ///// </summary>
    ///// <param name="sql"></param>
    ///// <param name="conn"></param>
    ///// <param name="cmd"></param>
    ///// <returns></returns>
    //public int GetCountRecordNew(string sql, OracleConnection conn, OracleCommand cmd)
    //{
    //    try
    //    {
    //        OracleDataReader sqlr = null;
    //        cmd.Connection = conn;
    //        cmd.CommandText = sql;

    //        //羲蟀諉
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();

    //        sqlr = cmd.ExecuteReader();
    //        //return val;
    //        sqlr.Read();
    //        return int.Parse(sqlr["total"].ToString());
    //    }
    //    catch (OracleException emsg)
    //    {
    //        conn.Close();
    //        string errStr = emsg.Message;
    //        return 0;
    //    }
    //}

    //public void GetLineFromAccess(ArrayList icustList, ref int icount, string project, OleDbConnection myconn)
    //{
    //    OleDbCommand myCommand = new OleDbCommand("select * from Line where project='" + project + "' order by id", myconn);
    //    try
    //    {
    //        myconn.Open();
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["line"].ToString());
    //        }
    //        icount = icustList.Count;
    //    }
    //    catch (OracleException)
    //    {
    //        return;
    //    }
    //    myconn.Close();
    //}

    //public void GetLineFromAccessNew(ArrayList icustList, ref int icount, string project, string model, OleDbConnection myconn)
    //{
    //    OleDbCommand myCommand = new OleDbCommand("select * from Line where project='" + project + "' and model='" + model + "' order by id", myconn);
    //    try
    //    {
    //        myconn.Open();
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["line"].ToString());
    //        }
    //        icount = icustList.Count;
    //    }
    //    catch (OracleException)
    //    {
    //        return;
    //    }
    //    myconn.Close();
    //}

    //public void GetModelFromAccess(ArrayList icustList, ref int icount, string project, OleDbConnection myconn)
    //{
    //    OleDbCommand myCommand = new OleDbCommand("select * from ProjectTable where project='" + project + "' order by id", myconn);
    //    try
    //    {
    //        myconn.Open();
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["Model"].ToString());
    //        }
    //        icount = icustList.Count;
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //    catch (Exception)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //}

    ///// <summary>
    ///// Get Model from DB:G2TSEDB TABLE:Master_User
    ///// </summary>
    ///// <param name="icustList"></param>
    ///// <param name="icount"></param>
    ///// <param name="project"></param>
    ///// <param name="myconn"></param>
    //public void GetModelFromMainDB(ArrayList icustList, ref int icount, string project, OracleConnection myconn)
    //{
    //    OracleCommand myCommand = new OracleCommand("select * from master_user.family_info order by model_fullname", myconn);
    //    try
    //    {
    //        myconn.Open();
    //        OracleDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["Model_fullname"].ToString());
    //        }
    //        icount = icustList.Count;
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //    catch (OracleException)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //}

    //public void GetProjectAndModelFromAccess(string[,] temp, string sql, OleDbConnection myconn, ref int countmodel)
    //{
    //    OleDbCommand myCommand = new OleDbCommand(sql, myconn);
    //    try
    //    {
    //        myconn.Open();
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            temp[countmodel, 0] = oqlr["project"].ToString();
    //            temp[countmodel, 1] = oqlr["model"].ToString();
    //            countmodel++;
    //        }
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //    catch (Exception)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //}

    //public void GetStationFromAccess(ArrayList icustList, ref int icount, string project, OleDbConnection myconn)
    //{
    //    OleDbCommand myCommand = new OleDbCommand("select * from Station where Project='" + project + "'  order by id", myconn);
    //    try
    //    {
    //        myconn.Open();
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["station"].ToString());
    //        }
    //        icount = icustList.Count;
    //    }
    //    catch (OracleException)
    //    {
    //        return;
    //    }
    //    myconn.Close();
    //}

    ///// <summary>
    ///// GetDSFromAccess
    ///// </summary>
    ///// <param name="sql"></param>
    ///// <param name="conn"></param>
    ///// <returns></returns>
    //public DataSet GetDSFromAccess(string sql, OleDbConnection conn)
    //{
    //    using (OleDbDataAdapter odda = new OleDbDataAdapter(sql, conn))
    //    {
    //        try
    //        {
    //            if (conn.State == ConnectionState.Closed)
    //            {
    //                conn.Open();
    //            }
    //            DataSet ds = new DataSet();
    //            odda.Fill(ds);
    //            return ds;
    //        }
    //        catch (Exception emsg)
    //        {
    //            string err = emsg.Message;
    //            conn.Close();
    //            return null;
    //        }
    //    }
    //}

    ///// <summary>
    ///// GetDSFromAccess
    ///// </summary>
    ///// <param name="sql"></param>
    ///// <param name="conn"></param>
    ///// <returns></returns>
    //public void RunAccessSQL(string sql, OleDbConnection conn)
    //{
    //    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
    //    {
    //        try
    //        {
    //            if (conn.State == ConnectionState.Closed)
    //            {
    //                conn.Open();
    //            }
    //            cmd.ExecuteNonQuery();
    //            //conn.Close();
    //        }
    //        catch (OleDbException emsg)
    //        {
    //            conn.Close();
    //        }
    //    }
    //}

    //public void GetStationFromAccessNew(ArrayList icustList, ref int icount, string project, string model, OleDbConnection myconn)
    //{
    //    OleDbCommand myCommand = new OleDbCommand("select * from Station where Project='" + project + "' and Model='" + model + "' order by id", myconn);
    //    try
    //    {
    //        myconn.Open();
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["station"].ToString());
    //        }
    //        icount = icustList.Count;
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //    catch (Exception)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //}

    ///// <summary>
    ///// GET station_group from G2TSEDB
    ///// </summary>
    ///// <param name="icustList"></param>
    ///// <param name="icount"></param>
    ///// <param name="project"></param>
    ///// <param name="model"></param>
    ///// <param name="myconn"></param>
    //public void GetStationFromMainDB(ArrayList icustList, ref int icount, string project, string model, OracleConnection myconn)
    //{
    //    OracleCommand myCommand = new OracleCommand("select * from Routing_station order by station_group", myconn);
    //    try
    //    {
    //        if (myconn.State == ConnectionState.Closed)
    //            myconn.Open();
    //        OracleDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["station_group"].ToString());
    //        }
    //        icount = icustList.Count;
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //    catch (OracleException ex)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //}

    ///// <summary>
    ///// Bind DropDownList
    ///// </summary>
    ///// <param name="model"></param>
    ///// <param name="ddl"></param>
    ///// <param name="myconn"></param>
    //public void BindStationFromDB(string model, DropDownList ddl, OracleConnection myconn)
    //{
    //    OracleCommand myCommand = new OracleCommand("select * from " + model + ".Routing_station order by station_group", myconn);
    //    try
    //    {
    //        myconn.Open();
    //        using (OracleDataAdapter oqlr = new OracleDataAdapter(myCommand))
    //        {
    //            DataSet ds = new DataSet();
    //            oqlr.Fill(ds);
    //            ddl.DataSource = ds.Tables[0];
    //            ddl.DataTextField = "Station_group";
    //            ddl.DataValueField = "Station_group";
    //            ddl.DataBind();
    //            ddl.Items.Insert(0, "All");
    //        }
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //    catch (OracleException)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //    }
    //}

    ///// <summary>
    ///// Get DataSet
    ///// </summary>
    ///// <param name="sql"></param>
    ///// <param name="myconn"></param>
    ///// <param name="cmd"></param>
    //public DataSet GetDS(string sql, OracleConnection myconn, OracleCommand cmd)
    //{
    //    try
    //    {
    //        cmd.Connection = myconn;
    //        cmd.CommandText = sql;

    //        if (myconn.State == ConnectionState.Closed)
    //            myconn.Open();

    //        using (OracleDataAdapter oqlr = new OracleDataAdapter(cmd))
    //        {
    //            DataSet ds = new DataSet();
    //            oqlr.Fill(ds);
    //            return ds;
    //        }
    //    }
    //    catch (OracleException)
    //    {
    //        return null;
    //        myconn.Close();
    //    }
    //}

    //public void GetStationFromDropDownList(ArrayList icustList, ref int icount, DropDownList dropdownlist)
    //{
    //    int i = 0;
    //    string a = "";
    //    i = dropdownlist.Items.Count;
    //    for (int j = 1; j < i; j++)
    //    {
    //        icustList.Add(dropdownlist.Items[j]);
    //        a = dropdownlist.Items[j].ToString();
    //    }
    //    icount = icustList.Count;
    //}

    //public void GetStationFromProject(ArrayList icustList, ref int icount, string sql, string project)
    //{
    //    try
    //    {
    //        string str_conn = this.NewOraConn(project);
    //        if (this.Session["DataBaseType"].ToString() == "Oracle")
    //        {
    //            OracleConnection conn = new OracleConnection(str_conn);
    //            conn.Open();
    //            OracleDataReader oqlr = null;
    //            OracleCommand oralcommand = new OracleCommand(sql, conn);
    //            oqlr = oralcommand.ExecuteReader();
    //            while (oqlr.Read())
    //            {
    //                icustList.Add(oqlr["item"].ToString());
    //            }
    //            icount = icustList.Count;
    //            conn.Close();
    //            conn.Dispose();
    //            oralcommand.Dispose();
    //        }
    //        else if (this.Session["DataBaseType"].ToString() == "Sql")
    //        {
    //            OracleConnection conn = new OracleConnection(str_conn);
    //            conn.Open();
    //            OracleDataReader oqlr = null;
    //            OracleCommand oralcommand = new OracleCommand(sql, conn);
    //            oqlr = oralcommand.ExecuteReader();
    //            while (oqlr.Read())
    //            {
    //                icustList.Add(oqlr["item"].ToString());
    //            }
    //            icount = icustList.Count;
    //            conn.Close();
    //            conn.Dispose();
    //            oralcommand.Dispose();
    //        }
    //    }
    //    catch (OracleException emsg)
    //    {
    //        string errStr = emsg.Message;
    //    }
    //}

    ///// <summary>
    ///// Get Station from project
    ///// </summary>
    ///// <param name="icustList"></param>
    ///// <param name="icount"></param>
    ///// <param name="sql"></param>
    ///// <param name="project"></param>
    ///// <param name="conn"></param>
    ///// <param name="cmd"></param>
    //public void GetStationFromProjectNew(ArrayList icustList, ref int icount, string sql, string project, OracleConnection conn, OracleCommand cmd)
    //{
    //    try
    //    {
    //        OracleDataReader oqlr = null;
    //        cmd.CommandText = sql;
    //        oqlr = cmd.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["test_point"].ToString());
    //        }
    //        icount = icustList.Count;
    //    }
    //    catch (OracleException)
    //    {
    //    }
    //}

    ///// <summary>
    ///// Get Station From Project New
    ///// </summary>
    ///// <param name="icustList"></param>
    ///// <param name="icount"></param>
    ///// <param name="sql"></param>
    ///// <param name="conn"></param>
    ///// <param name="cmd"></param>
    //public void GetItemFromDB(ArrayList icustList, ref int icount, string sql, OracleConnection conn, OracleCommand cmd)
    //{
    //    try
    //    {
    //        OracleDataReader oqlr = null;
    //        cmd.Connection = conn;
    //        cmd.CommandText = sql;

    //        //羲蟀諉
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();

    //        oqlr = cmd.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["item"].ToString());
    //        }
    //        icount = icustList.Count;
    //    }
    //    catch (OracleException emsg)
    //    {
    //        conn.Close();
    //        string errStr = emsg.Message;
    //    }
    //}

    //public void GetTestItemFromAccessNew(ArrayList icustList, ref int icount, string project, string model, string station, OleDbConnection myconn)
    //{
    //    myconn.Open();
    //    OleDbCommand myCommand = new OleDbCommand("select * from Testitem where Project='" + project + "' and Model='" + model + "' and station='" + station + "' order by id", myconn);
    //    try
    //    {
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["testitem"].ToString());
    //        }
    //        icount = icustList.Count;
    //    }
    //    catch (OracleException)
    //    {
    //        return;
    //    }
    //    myconn.Close();
    //}

    ///// <summary>
    ///// GetTestItemFromMainDB
    ///// </summary>
    ///// <param name="icustList"></param>
    ///// <param name="icount"></param>
    ///// <param name="model"></param>
    ///// <param name="station"></param>
    ///// <param name="myconn"></param>
    //public void GetTestItemFromMainDB(ArrayList icustList, ref int icount, string model, string station, OracleConnection myconn)
    //{
    //    myconn.Open();

    //    //Usable for debug
    //    string sql = "select distinct(test_point) as item from Test_Measurement_" + station + " where test_point like'" + model + "_" + station + "%' order by test_point";
    //    OracleCommand myCommand = new OracleCommand(sql, myconn);
    //    try
    //    {
    //        OracleDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        while (oqlr.Read())
    //        {
    //            icustList.Add(oqlr["item"].ToString());
    //        }
    //        icount = icustList.Count;
    //    }
    //    catch (OracleException emsg)
    //    {
    //        string errStr = emsg.Message;
    //        return;
    //    }
    //    myconn.Close();
    //}

    //public void LoadData(string sql, GridView gridview, string project)
    //{
    //    //※DSet§+GridView腔ID釬Session腔ID
    //    string id = "DSet" + gridview.ID.ToString();
    //    try
    //    {
    //        string str_conn = this.NewOraConn(project);
    //        if (this.Session["DataBaseType"].ToString() == "Oracle")
    //        {
    //            OracleDataAdapter OracleDa = new OracleDataAdapter();
    //            OracleConnection conn = new OracleConnection(str_conn);
    //            OracleCommand commd = new OracleCommand(sql);
    //            commd.Connection = conn;
    //            OracleDa.SelectCommand = commd;
    //            DataSet ds = new DataSet();
    //            OracleDa.Fill(ds);
    //            gridview.DataSource = ds.Tables[0].DefaultView;
    //            try
    //            {
    //                gridview.DataBind();
    //                gridview.Visible = true;
    //            }
    //            catch (Exception ex)
    //            {
    //                string k = ex.Message;
    //                try
    //                {
    //                    //gridview.CurrentPageIndex = 0;
    //                    gridview.DataBind();
    //                    gridview.Visible = true;
    //                }
    //                catch
    //                {
    //                    conn.Close();
    //                    conn.Dispose();
    //                    commd.Dispose();
    //                }
    //            }
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                gridview.Visible = false;
    //            }
    //            Session[id] = ds;
    //            conn.Close();
    //            conn.Dispose();
    //            commd.Dispose();
    //        }
    //        else if (this.Session["DataBaseType"].ToString() != "Sql")
    //        {
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        gridview.Visible = false;
    //        string k = err.Message;
    //    }
    //}

    ///// <summary>
    /////  Mege table
    ///// </summary>
    ///// <param name="sql"></param>
    ///// <param name="gridview"></param>
    ///// <param name="project"></param>
    //public void LoadData(string sql, string sql1, GridView gridview, string project)
    //{
    //    //※DSet§+GridView腔ID釬Session腔ID
    //    string id = "DSet" + gridview.ID.ToString();
    //    try
    //    {
    //        string str_conn = this.NewOraConn(project);
    //        OracleDataAdapter OracleDa = new OracleDataAdapter();
    //        OracleConnection conn = new OracleConnection(str_conn);
    //        OracleCommand commd = new OracleCommand(sql);
    //        commd.Connection = conn;
    //        OracleDa.SelectCommand = commd;
    //        DataSet ds = new DataSet();
    //        OracleDa.Fill(ds, "test_header");
    //        OleDbConnection oleconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + ConfigurationManager.AppSettings["AccessParth3"]);
    //        using (OleDbDataAdapter odda = new OleDbDataAdapter(sql1, oleconn))
    //        {
    //            try
    //            {
    //                if (oleconn.State == ConnectionState.Closed)
    //                {
    //                    oleconn.Open();
    //                }
    //                odda.Fill(ds, "computerlist");
    //            }
    //            catch (Exception emsg)
    //            {
    //                string err = emsg.Message;
    //                oleconn.Close();
    //                oleconn.Dispose();
    //            }
    //        }
    //        DataColumn ParentCol = ds.Tables["test_header"].Columns["computer_name"];
    //        DataColumn ChildCol = ds.Tables["computerlist"].Columns["Computer_Name"];
    //        DataRelation PCRelation = new DataRelation("ComputerMachine", ParentCol, ChildCol, false);
    //        ds.Relations.Add(PCRelation);
    //        DataTable mydt = (DataTable)CombineDataTables(ds.Tables["test_header"], ds.Tables["computerlist"], PCRelation);
    //        gridview.DataSource = mydt;
    //        try
    //        {
    //            gridview.DataBind();
    //            gridview.Visible = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            string k = ex.Message;
    //            try
    //            {
    //                //gridview.CurrentPageIndex = 0;
    //                gridview.DataBind();
    //                gridview.Visible = true;
    //            }
    //            catch
    //            {
    //                conn.Close();
    //                conn.Dispose();
    //                commd.Dispose();
    //            }
    //        }
    //        if (ds.Tables[0].Rows.Count == 0)
    //        {
    //            gridview.Visible = false;
    //        }
    //        Session[id] = mydt;
    //        conn.Close();
    //        conn.Dispose();
    //        commd.Dispose();
    //    }
    //    catch (Exception err)
    //    {
    //        gridview.Visible = false;
    //        string k = err.Message;
    //    }
    //}

    ///// <summary>
    ///// Combine two datatable
    ///// </summary>
    ///// <param name="dtParent"></param>
    ///// <param name="dtChild"></param>
    ///// <param name="drRelation"></param>
    ///// <returns></returns>
    //public DataTable CombineDataTables(DataTable dtParent, DataTable dtChild, DataRelation drRelation)
    //{
    //    /*虜桶菴珨蹈腔蹈靡*/
    //    string ParentColumn = drRelation.ParentColumns[0].ToString();

    //    /* 斐膘猁殿隙腔賦彆桶(磁甜綴腔桶跡) */
    //    DataTable dtSupProd = new DataTable();

    //    /*斐膘賦彆桶腔蹈, 磁甜虜桶睿赽桶腔蹈*/
    //    //忑珂跦擂虜桶腔蹈斐膘蹈
    //    for (Int32 i = 0; i < dtParent.Columns.Count; i++)
    //    {
    //        dtSupProd.Columns.Add(new DataColumn(dtParent.Columns[i].ColumnName));
    //    }
    //    //跦擂赽桶腔蹈斐膘蹈
    //    for (Int32 i = 0; i < dtChild.Columns.Count; i++)
    //    {
    //        //秏壺謗跺桶眈肮腔蹈,硐氝樓祥眈肮腔蹈,珩憩翋俋瑩岆眈肮腔蹈,涴爵岆
    //        //跦擂蹈靡眈肮懂秏壺
    //        if (dtChild.Columns[i].ColumnName != ParentColumn)
    //        {
    //            dtSupProd.Columns.Add(new DataColumn(dtChild.Columns[i].ColumnName));
    //        }
    //    }

    //    /* 跦擂虜桶睿赽桶懂郪蚾datatable */
    //    DataRow dr;

    //    //跦擂虜桶腔暮翹懂赽桶腔杅擂
    //    for (Int32 iRow = 0; iRow < dtParent.Rows.Count; iRow++)
    //    {
    //        //蚚GetChildRows源楊,跦擂datarelation赽桶腔杅擂,溫善datarow杅
    //        //郪笢,彆岆珨勤嗣腔壽炵,褫夔憩衄嗣沭趼暮翹
    //        DataRow[] childrows = dtParent.Rows[iRow].GetChildRows(drRelation);
    //        //悜遠腔植datarow杅郪笢堤赽桶笢腔杅擂
    //        for (Int32 iRowProd = 0; iRowProd < childrows.Length; iRowProd++)
    //        {
    //            dr = dtSupProd.NewRow();

    //            /*參蹈硉沓善陔桶腔陔俴笢*/
    //            //忑珂沓虜桶腔俴腔菴珨蹈杅擂
    //            for (Int32 iColSup = 0; iColSup < dtParent.Columns.Count; iColSup++)
    //            {
    //                dr[dtParent.Columns[iColSup].ColumnName] = dtParent.Rows[iRow][iColSup].ToString();
    //            }

    //            /* 沓喃赽桶藩珨蹈腔杅擂 */
    //            for (Int32 iColProd = 0; iColProd < dtChild.Columns.Count; iColProd++)
    //            {
    //                if (dtChild.Columns[iColProd].ColumnName != ParentColumn)
    //                {
    //                    dr[dtChild.Columns[iColProd].ColumnName] = childrows[iRowProd][iColProd].ToString();
    //                }
    //            }
    //            dtSupProd.Rows.Add(dr);
    //        }

    //    }
    //    /*Return DataTable*/
    //    return dtSupProd;
    //}

    //public void LoadData(string sql, DataGrid datagrid, string project)
    //{
    //    try
    //    {
    //        string str_conn = this.NewOraConn(project);
    //        if (this.Session["DataBaseType"].ToString() == "Oracle")
    //        {
    //            OracleDataAdapter OracleDa = new OracleDataAdapter();
    //            OracleConnection conn = new OracleConnection(str_conn);
    //            OracleCommand commd = new OracleCommand(sql);
    //            commd.Connection = conn;
    //            OracleDa.SelectCommand = commd;
    //            DataSet ds = new DataSet();
    //            OracleDa.Fill(ds);
    //            datagrid.DataSource = ds.Tables[0];
    //            try
    //            {
    //                datagrid.DataBind();
    //                datagrid.Visible = true;
    //            }
    //            catch
    //            {
    //                try
    //                {
    //                    datagrid.CurrentPageIndex = 0;
    //                    datagrid.DataBind();
    //                    datagrid.Visible = true;
    //                }
    //                catch
    //                {
    //                    conn.Close();
    //                    conn.Dispose();
    //                    commd.Dispose();
    //                }
    //            }
    //            conn.Close();
    //            conn.Dispose();
    //            commd.Dispose();
    //        }
    //        else if (this.Session["DataBaseType"].ToString() != "Sql")
    //        {
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        string k = err.Message;
    //    }
    //}

    //public string neworacleconn(string project)
    //{
    //    string a = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=E:\HWDWEBSFC\StationTable.aspx";
    //    string strCon = "null";
    //    OleDbConnection myconn = new OleDbConnection(a);
    //    OleDbCommand myCommand = new OleDbCommand();
    //    try
    //    {
    //        myconn.Open();
    //        string sql1 = "select * from ProjectTable where Model='" + project + "' order by id";
    //        myCommand.CommandText = sql1;
    //        myCommand.Connection = myconn;
    //        OleDbDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader();
    //        string databasetype = "";
    //        if (oqlr.Read())
    //        {
    //            databasetype = oqlr["DataBaseType"].ToString();
    //            if (oqlr["DataBaseType"].ToString() == "Oracle")
    //            {
    //                strCon = "Password=" + oqlr["Pwd"].ToString() + ";user id=" + oqlr["Uid"].ToString() + ";Data Source=" + oqlr["Sid"].ToString() + ";Persist Security Info=True";
    //                this.Session["DataBaseType"] = "Oracle";
    //                oqlr.Close();
    //            }
    //            else if (oqlr["DataBaseType"].ToString() == "Sql")
    //            {
    //                strCon = "SERVER=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + oqlr["Ip"].ToString() + ")(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=" + oqlr["Sid"].ToString() + ")));uid=" + oqlr["Uid"].ToString() + ";pwd=" + oqlr["Pwd"].ToString();
    //                this.Session["DataBaseType"] = "Sql";
    //                oqlr.Close();
    //            }
    //        }
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //        return strCon;
    //    }
    //    catch (OracleException)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //        return strCon;
    //    }
    //}

    //public string NewOraConn(string Model)
    //{
    //    string a = @"Password=huaweipwd;user id=huawei;Data Source=G2TSEDB;Persist Security Info=True";
    //    string strCon = "null";
    //    OracleConnection myconn = new OracleConnection(a);
    //    OracleCommand myCommand = new OracleCommand();
    //    try
    //    {
    //        myconn.Open();
    //        string sql1 = "select * from Master_user.family_info where Model_fullname='" + Model + "' order by model_fullname";
    //        myCommand.CommandText = sql1;
    //        myCommand.Connection = myconn;
    //        OracleDataReader oqlr = null;
    //        oqlr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
    //        if (oqlr.Read())
    //        {
    //            strCon = "Password=" + oqlr["Password"].ToString() + ";user id=" + oqlr["User_name"].ToString() + ";Data Source=G2TSEDB;Persist Security Info=True";
    //            this.Session["DataBaseType"] = "Oracle";
    //            oqlr.Close();
    //        }
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //        return strCon;
    //    }
    //    catch (OracleException)
    //    {
    //        myconn.Close();
    //        myconn.Dispose();
    //        myCommand.Dispose();
    //        return strCon;
    //    }
    //}

    //public string NewOraConn()
    //{
    //    string a = @"Password=huaweipwd;user id=huawei;Data Source=G2TSEDB;Persist Security Info=True";
    //    return a;
    //}

    ///// <summary>
    ///// Return HW sn's modelname
    ///// </summary>
    ///// <param name="sn"></param>
    ///// <returns></returns>
    //public string GetHWModel(string sn)
    //{
    //    string connectionString = ConfigurationManager.ConnectionStrings["TMISConnectionString"].ConnectionString;
    //    string queryString = "SELECT B.modelfullname as modelfullname FROM r_serialcode A LEFT JOIN r_model B on A.model=B.model " +
    //        "WHERE A.sn='" + sn + "'";

    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        try
    //        {
    //            SqlDataReader sdr = null;
    //            SqlCommand cmd = new SqlCommand(queryString, conn);
    //            cmd.CommandType = CommandType.Text;
    //            conn.Open();
    //            sdr = cmd.ExecuteReader();

    //            if (sdr.Read())
    //            {
    //                return sdr["modelfullname"].ToString();
    //            }
    //            return null;
    //        }
    //        catch (Exception e)
    //        {
    //            return null;
    //        }
    //    }
    //}
}
