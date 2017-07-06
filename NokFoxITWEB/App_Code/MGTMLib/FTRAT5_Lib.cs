using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Data.OracleClient;
using System.Data;
using Microsoft.Adapter.SAP;
using System.Configuration;

/// <summary>
/// Summary description for FTRAT5_Lib
/// </summary>
public class EDI_FTP
{
    private string URL;
    private string User;
    private string PSW;
    public EDI_FTP(string sURL, string sUser, string sPassword)
    {
        URL = sURL;
        User = sUser;
        PSW = sPassword;
    }

    public int GetFTPAllFiles(ref string[] Files, ref string sMsg)
    {
        StreamReader reader = null;
        FtpWebResponse response = null;
        int iRet;
        try
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(URL);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(User, PSW);
            response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            reader = new StreamReader(responseStream);
            Array.Resize(ref Files, 0);
            while (reader.EndOfStream == false)
            {
                Array.Resize(ref Files, Files.GetLength(0) + 1);
                string s = reader.ReadLine();
                Files[Files.GetLength(0) - 1] = s;
            }
            sMsg = "";
            iRet = 0;
        }
        catch (Exception e)
        {
            sMsg = e.Message.ToString();
            iRet = -1;
        }
        finally
        {
            if (reader != null) reader.Close();
            if (response != null) response.Close();
        }
        return iRet;
    }

    public int DownloadFTPFile(string FileName, string ObjectFile, ref string sMsg)
    {
        Stream responseStream = null;
        FileStream fileStream = null;
        FtpWebResponse response = null;
        int iRet;

        if (URL.Substring(URL.Length).ToUpper() != "/") URL = URL + "/";
        string sFileName = URL + FileName;
        try
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sFileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(User, PSW);
            response = (FtpWebResponse)request.GetResponse();

            responseStream = response.GetResponseStream();
            fileStream = File.Create(ObjectFile);
            byte[] buffer = new byte[1024];
            int bytesRead;
            while (true)
            {
                bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    break;
                fileStream.Write(buffer, 0, bytesRead);
            }
            iRet = 0;
        }
        catch (Exception e)
        {
            sMsg = e.Message.ToString();
            iRet = -1;
        }
        finally
        {
            if (response != null) response.Close();
            if (responseStream != null) responseStream.Close();
            if (fileStream != null) fileStream.Close();
        }
        return iRet;
    }

    public int DeleteFTPFile(string FileName, ref string sMsg)
    {
        Stream responseStream = null;
        FtpWebResponse response = null;
        int iRet;

        if (URL.Substring(URL.Length).ToUpper() != "/") URL = URL + "/";
        string sFileName = URL + FileName;
        try
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(sFileName);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(User, PSW);
            response = (FtpWebResponse)request.GetResponse();
            //responseStream = response.GetResponseStream();
            iRet = 0;
        }
        catch (Exception e)
        {
            sMsg = e.Message.ToString();
            iRet = -1;
        }
        finally
        {
            if (response != null) response.Close();
        }
        return iRet;
    }

}

public class EDI_DN
{
    private static string TrimLeft(string sStr,char sOjb)
    {
        string sRet = sStr;
        while (sRet.Length >0)
        {
            if (sRet[0] == sOjb)
                sRet = sRet.Substring(1);
            else
                break;
        }
        return sRet;
    }
    public static string[,] FileToDN(ref string[] Files)
    {
        string[,] FileDN = new string[Files.GetLength(0), 3];
        int i;
        for (i = 0; i < Files.GetLength(0); i++)
        {
            string sFileName = Files[i];
            int iPos = sFileName.IndexOf("_");
            int iPosEnd = sFileName.IndexOf(".");
            if (iPos <= 0) iPos = sFileName.Length;
            if (iPosEnd <= 0) iPosEnd = sFileName.Length;
            
            string sDN = "";
            string sTime = "";
            try 
            {
                sDN = sFileName.Substring(0, iPos);
                sDN = TrimLeft(sDN,'0');
            }
            catch
            {
                sDN  = sFileName;
            }
            try 
            {
                sTime = sFileName.Substring(iPos+1,iPosEnd-iPos-1);
            }
            catch
            {
                sTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            FileDN[i, 0] = sDN;
            FileDN[i, 1] = sTime;
            FileDN[i, 2] = sFileName;
        }
        return FileDN;
    }

}


public class UpdateFTRAT  
{
    private OracleConnection OrcConn;
    private string FErrorMsg;

    public UpdateFTRAT()
    {
        string strConn = System.Web.Configuration.WebConfigurationManager.AppSettings["MGT"];
        OrcConn = new OracleConnection();
        try
        {
            OrcConn.ConnectionString = strConn;
            OrcConn.Open();
            FErrorMsg = "";
        }
        catch (Exception e)
        {
            FErrorMsg = e.Message;
        }
    }

    private void CheckConnect()
    {
        try
        {
            if (OrcConn.State == System.Data.ConnectionState.Closed) OrcConn.Open();
            FErrorMsg = "";
        }
        catch (Exception e)
        {
            FErrorMsg = e.Message;
        }
    }

    private int UpdateFTRAT_A(string DN, string Type, string TTime, string FileName)
    {
        #region Default
        /* Update SFC.UPD_DATALOAD_DETAIL_T  UPD/ASN/IMEI/EDI
         * T_Time Format need be YYYYMMDDHHMMSS , if format is wrong , system will use now time.
         * sMsg return message when error happen
         * Success return 0, less zero is error, >0 informate or parameter error.
        */
        #endregion
        int iRet;
        string sStr = "Update SFC.UPD_DATALOAD_DETAIL_T Set ";
        switch (Type.ToUpper().Trim())
        {
            case "UPD":
                sStr = sStr + "FileName =:FileName , Create_Date = To_Date(:Create_Date ,'yyyymmddHH24Miss') ";
                break;
            case "IMEI":
                sStr = sStr + "EMEI_File =:FileName , IMEI_File_Time = To_Date(:Create_Date ,'yyyymmddHH24Miss') ";
                break;
            case "ASN":
                sStr = sStr + "ASN_FILE =:FileName , ASN_FTP_Time = To_Date(:Create_Date ,'yyyymmddHH24Miss') ";
                break;
            case "EDI":
                sStr = sStr + "EDI_FILE =:FileName , EDI = To_Date(:Create_Date ,'yyyymmddHH24Miss') ";
                break;
            default :
                sStr = "";
                break;
        }
        if (sStr != "")
        {
            OracleCommand cmd = null;
            try
            {
                sStr = sStr + " Where INVOICE = :DN";
                if (TTime.Trim() == "")
                {
                    sStr = sStr.Replace(":Create_Date", "null");
                }
                string sTime = CheckDateTimeFormate(TTime);
                CheckConnect();
                cmd = new OracleCommand(sStr, OrcConn);
                cmd.Parameters.AddWithValue(":FileName", FileName);
                cmd.Parameters.AddWithValue(":DN", DN);
                if (TTime.Trim() != "")
                {
                    cmd.Parameters.AddWithValue(":Create_Date", sTime);                    
                }

                int iRec = cmd.ExecuteNonQuery();
                if (iRec <= 0)
                {
                    iRet = 1;
                    FErrorMsg  = "No Record Update";
                }
                else
                {
                    FErrorMsg = "";
                    iRet = 0;
                }
            }
            catch (Exception e)
            {
                iRet = -1;
                FErrorMsg = e.Message;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }
        else
        {
            iRet = 2;
            FErrorMsg = "Parameter Type is wrong";
        }
        return iRet;
    }

    private int InsertFTRAT_A(string DN, string Ship_Qty, string Model, string Ship_To, string Customer, string Ship_Date,string Action, string Type, string FileName, string Date)
    {
        #region Default
        /* Insert New Values SFC.UPD_DATALOAD_DETAIL_T  UPD/ASN/IMEI/EDI
         * T_Time Format need be YYYYMMDDHHMMSS , if format is wrong , system will use now time.
         * sMsg return message when error happen
        */
        #endregion

        #region InitSql
        int iRet;
        string sStr = "Insert INTO SFC.UPD_DATALOAD_DETAIL_T (TYPE,INVOICE,SHIP_QTY,MODEL,SHIP_TO,CUSTOMER,SHIP_Date,ACTION,";
        switch (Type.ToUpper().Trim())
        {
            case "UPD":
                sStr = sStr + "FileName , Create_Date) ";
                break;
            case "IMEI":
                sStr = sStr + "EMEI_File , IMEI_File_Time,FILENAME) ";
                break;
            case "ASN":
                sStr = sStr + "ASN_FILE, ASN_FTP_Time,FILENAME) ";
                break;
            case "EDI":
                sStr = sStr + "EDI_FILE , EDI,FILENAME) ";
                break;
            default :
                sStr = "";
                break;
        }
        #endregion

        #region SetValueExec
        if (sStr != "")
        {
            OracleCommand cmd = null;
            try
            {
                sStr = sStr + " Values ('UPD',:DN,:SHIP_QTY,:MODELName,:SHIP_TO,:CUSTOMER,To_Date(:SHIP_Date, 'YYYYMMDDHH24MISS'),:ACTION,:FILEName,To_Date(:Create_Date ,'YYYYMMDDHH24MISS') ";
                if (Type.ToUpper().Trim() != "UPD")
                {
                    sStr = sStr + ", ' '";
                }
                sStr = sStr + ")";
                if (Date.Trim() == "")
                {
                    sStr = sStr.Replace(":Create_Date", "null");
                }

                if (Ship_Date.Trim() == "")
                {
                    Ship_Date = DateTime.Now.ToString("yyyyMMdd");
                }

                CheckConnect();
                cmd = new OracleCommand(sStr, OrcConn);
                cmd.Parameters.AddWithValue(":DN", DN);
                cmd.Parameters.AddWithValue(":SHIP_QTY", Ship_Qty);
                cmd.Parameters.AddWithValue(":MODELName", Model);
                cmd.Parameters.AddWithValue(":SHIP_TO", Ship_To);
                cmd.Parameters.AddWithValue(":CUSTOMER", Customer);
                cmd.Parameters.AddWithValue(":SHIP_Date", CheckDateTimeFormate(Ship_Date));
                cmd.Parameters.AddWithValue(":ACTION", Action);
                cmd.Parameters.AddWithValue(":FILEName", FileName);
                cmd.Parameters.AddWithValue(":SHIP_Date", CheckDateTimeFormate(Ship_Date));
                if (Date.Trim() != "")
                {
                    cmd.Parameters.AddWithValue(":Create_Date", CheckDateTimeFormate(Date));
                }

                cmd.ExecuteNonQuery();
                iRet = 0;
                FErrorMsg = "";
            }
            catch (Exception e)
            {
                iRet = -1;
                FErrorMsg = e.Message;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }
        else
        {
            iRet = 2;
            FErrorMsg = "Input Parameter TYPE is wrong";

        }
        #endregion

        return iRet;
    }

    private int UpdateFTRAT_B(string DN, string Type, string TTime, string FileName)
    {
        int iRet;
        string sStr = "Update SFC.FTRAT1 Set FILENAME = :FileName,CREATE_DATE = To_Date(:Create_Date,'yyyyMMDDHH24MISS') Where INVOICE = :DN And Type=:Type";
        if (FileName == "") FileName = " ";
        if (TTime.Trim() == "")
        {
            sStr = sStr.Replace(":Create_Date", "null");
        }
        OracleCommand cmd = null;
        try
        {
            CheckConnect();
            cmd = new OracleCommand(sStr, OrcConn);
            cmd.Parameters.AddWithValue(":FileName", FileName);
            cmd.Parameters.AddWithValue(":DN", DN);
            cmd.Parameters.AddWithValue(":Type", Type.ToUpper().Trim());
            if (TTime.Trim() != "")
            {
                cmd.Parameters.AddWithValue(":Create_Date", CheckDateTimeFormate(TTime));
            }
            int iRec = cmd.ExecuteNonQuery();
            if (iRec <= 0)
            {
                iRet = 1;
                FErrorMsg = "No Record Update";
            }
            else
            {
                iRet = 0;
                FErrorMsg = "";
            }
        }
        catch (Exception e)
        {
            iRet = -1;
            FErrorMsg = e.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }

    private int InsertFTRAT_B(string DN, string Ship_Qty, string Model, string Ship_To, string Customer, string Ship_Date, string Action, string Type, string FileName, string Date)
    {
        #region Default
        /* Insert New Values SFC.FTRAT1  UPD/ASN/IMEI/EDI
         * T_Time Format need be YYYYMMDDHHMMSS , if format is wrong , system will use now time.
         * sMsg return message when error happen
        */
        #endregion

        #region InitSql
        int iRet;
        string sStr = "Insert Into SFC.FTRAT1(TYPE,INVOICE,SHIP_QTY,MODEL,SHIP_TO,CUSTOMER,SHIP_Date,Create_Date,ACTION,FileName)";
        sStr = sStr + " Values (:File_Type,:DN,:SHIP_QTY,:MODELName,:SHIP_TO,:CUSTOMER,To_Date(:SHIP_Date,'YYYYMMDDHH24MISS'),To_Date(:Create_Date,'YYYYMMDDHH24MISS'),:ACTION,:FILENAME) ";
        if (Ship_Date.Trim() == "") Ship_Date = DateTime.Now.ToString("yyyyMMddHHmmss");
        if (FileName.Trim() == "") FileName = " ";
        if (Date.Trim() == "") sStr = sStr.Replace(":Create_Date", "null");
            
        #endregion

        #region SetValueExec
        OracleCommand cmd = null;
        try
        {
            if (Ship_Qty == "") Ship_Qty ="0";
            if (Model == "") Model = " ";
            if (Ship_To == "") Ship_To = " ";
            if (Customer == "") Customer = " ";
            if (Action == "") Action = " ";
            CheckConnect();
            cmd = new OracleCommand(sStr, OrcConn);
            cmd.Parameters.AddWithValue(":File_Type", Type.ToUpper().Trim());
            cmd.Parameters.AddWithValue(":DN", DN);
            cmd.Parameters.AddWithValue(":SHIP_QTY", Ship_Qty);
            cmd.Parameters.AddWithValue(":MODELName", Model);
            cmd.Parameters.AddWithValue(":SHIP_TO", Ship_To);
            cmd.Parameters.AddWithValue(":CUSTOMER", Customer);
            cmd.Parameters.AddWithValue(":SHIP_Date", CheckDateTimeFormate(Ship_Date));
            cmd.Parameters.AddWithValue(":ACTION", Action);
            cmd.Parameters.AddWithValue(":FILENAME", FileName);
            if (Date.Trim() != "") cmd.Parameters.AddWithValue(":Create_Date", CheckDateTimeFormate(Date));
            cmd.ExecuteNonQuery();
            iRet = 0;
            FErrorMsg = "";
        }
        catch (Exception e)
        {
            iRet = -1;
            FErrorMsg = e.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }

        #endregion

        return iRet;
    }

    // if Date is not yyyyMMdd hhmmss return now
    private string CheckDateTimeFormate(string Date)
    {
        string sRet;
        try
        {
            if (Date.Trim() == "")
            {
                sRet = null;
            
            }
            else if (Date.Trim().Length == 8)
            {
                DateTime.ParseExact(Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                sRet = Date;
            }
            else if (Date.Trim().Length > 14)
            {
                DateTime.ParseExact(Date.Substring(0,14), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                sRet = Date.Substring(0,14);
            }
            else
            {
                DateTime.ParseExact(Date, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                sRet = Date;
            }
        }
        catch (Exception e)
        {
            sRet = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        return sRet;


    }

    private int GetDNInformation(string DN,ref string Ship_Qty,ref string Moduel,ref string ship_To,ref string Customer,ref string Ship_Date)
    {
        int iRet = -1;
        OracleCommand cmd = null;
        OracleDataAdapter oda = null;
        DataTable dt = null;

        OracleCommand cmdSO = null;
        OracleDataAdapter odaSO = null;
        DataTable dtSO = null;


        Ship_Qty = " ";
        Moduel = " ";
        ship_To = " ";
        Customer = " ";
        Ship_Date = "";
        string SO = "";


        string sStr = "select CUSTOMER_NAME,SHIPPED_QTY,ITEM_MODULE,TO_Char(Last_Shipment_Date,'yyyyMMddHH24MiSS')  from sfc.mes_mit_invoice Where RowNum = 1 And INVOICE = :DN "; 
        string sStr2 = "select SHIP_TO_Country from shp.cmcs_sfc_packing_lines_all  Where Where RowNum = 1 aND INVOICE_NUmber = :DN";
        string sStr3 = "";
        CheckConnect();
        while (true)
        {
            try
            {
                cmd = new OracleCommand(sStr,OrcConn);
                cmd.Parameters.AddWithValue(":DN",DN);
                oda = new OracleDataAdapter();
                dt = new DataTable();
                oda.SelectCommand = cmd;
                oda.Fill(dt);
                if (dt.Rows.Count <=0) break;
                Customer = dt.Rows[0][0].ToString();
                Ship_Qty = dt.Rows[0][1].ToString();
                Moduel = dt.Rows[0][2].ToString();
                Ship_Date = dt.Rows[0][3].ToString();

                cmdSO = new OracleCommand(sStr2,OrcConn);
                cmdSO.Parameters.AddWithValue(":DN",DN);
                odaSO = new OracleDataAdapter();
                dtSO = new DataTable();
                odaSO.SelectCommand = cmdSO;
                odaSO.Fill(dtSO);
                if (dtSO.Rows.Count <=0) break;
                ship_To = dt.Rows[0][0].ToString();
                iRet = 0;
                break;
            }
            catch  (Exception e)
            {
                iRet = -1;
                FErrorMsg = e.Message;
                break;
            }
        }
        if (cmd != null) cmd.Dispose();
        if (oda != null) oda.Dispose();
        if (dt != null) dt.Dispose();
        if (cmdSO != null) cmdSO.Dispose();
        if (odaSO != null) odaSO.Dispose();
        if (dtSO != null) dtSO.Dispose();
        return iRet;
    }

    private int InsertNotesSend(string Sub,string Content, string Send_To, string Send_CC, string From, string Prog)
    {
        string sStr = "Insert into SFC.C_Notes_Send (Note_Subject,Note_Content,Send_To,Send_CC,Send_From,Send_Prog) " +
                      "Values(:Sub,:ConTent,:Send_To,:Send_CC,:Send_From,:Send_Prog) ";
        OracleCommand cmd = null;
        int iRet;
        try
        {
            CheckConnect();
            cmd = new OracleCommand(sStr, OrcConn);
            cmd.Parameters.AddWithValue(":Sub", Sub);
            cmd.Parameters.AddWithValue(":ConTent", Content);
            cmd.Parameters.AddWithValue(":Send_To", Send_To);
            cmd.Parameters.AddWithValue(":Send_CC", Send_CC);
            cmd.Parameters.AddWithValue(":Send_From", From);
            cmd.Parameters.AddWithValue(":Send_Prog", Prog);
            cmd.ExecuteNonQuery();
            iRet = 0;
            FErrorMsg = "";
        }
        catch (Exception e)
        {
            iRet = -1;
            FErrorMsg = e.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
        }
        return iRet;
    }



    /* Update SFC.UPD_DATALOAD_DETAIL_T  UPD/ASN/IMEI/EDI 
     * if no record be updated, then will insert the new one.(Parameter Type = "I")
     * Parameters
     * DN :         Delivery Number (must)
     * Ship_Qty:    Ship quality (option,Update_TYPE=I must)
     * Model :      Model (option,Update_TYPE=I must)
     * Ship_To:     Ship To (option,Update_TYPE=I must)
     * Customer:    Customer Name (option,Update_TYPE=I must)
     * Ship_Date:   Ship Date Time ((option,Update_TYPE=I must) // Formate should be 'YYYYMMDDHHMMSS','YYYYMMDD',''(not set value)
     * Action:      Action (option,Update_TYPE=I must) 
     * Type:        UPD/ASN/IMEI/EDI (must) 
     * FileName:    File Name (must)
     * Date:        File Create Date (must) // Formate should be 'YYYYMMDDHHMMSS','YYYYMMDD',''(null will be writen)
     * Update_Type  U/I (must) U: Only update,I: first update , if no date be found, then insert new one.
     * Return:      zero success, non-zero error, use function GetErrorMessage return error information.
     */
    public int ModifyFTRAT_A(string DN, string Ship_Qty, string Model, string Ship_To, string Customer, string Ship_Date, string Action,string Type, string FileName, string Date,string Update_TYPE)
    {
        int iRet = UpdateFTRAT_A(DN, Type, Date, FileName);
        if ((iRet == 1) && (Update_TYPE.ToUpper().Trim() =="I")) // NO Record be update will insert new one.
        {
            iRet = InsertFTRAT_A(DN, Ship_Qty, Model, Ship_To, Customer, Ship_Date, Action, Type, FileName, Date);
        }
        return iRet;
    }

    public int ModifyFTRAT_B(string DN, string Ship_Qty, string Model, string Ship_To, string Customer, string Ship_Date, string Action, string Type, string FileName, string Date)
    {
        int iRet = UpdateFTRAT_B(DN, Type, Date, FileName);
        if (iRet == 1)  // NO Record be update will insert new one.
        {
            InsertFTRAT_B(DN, Ship_Qty, Model, Ship_To, Customer, Ship_Date, Action, Type, FileName, Date);
        }
        return iRet;
    }

    public int Check_EDI_Receive(string FTPHost, string FTPUser, string FTPPSW)
    {
        if ((FTPHost == null) || (FTPHost == "")) FTPHost = "FTP://10.186.171.77";
        if ((FTPUser == null) || (FTPUser == "")) FTPUser = "lhy";
        if ((FTPPSW == null) ||  (FTPPSW == ""))  FTPPSW = "456123";

        string[] EDI = new string[0];
        string[,] EDI_Files;
        string sMsg = "";
        int iRet = -1;
        int i;
        while (true)
        {

            #region CallFTPGetFiles
            try
            {
                EDI_FTP myFTP = new EDI_FTP(FTPHost, FTPUser, FTPPSW);
                if (myFTP.GetFTPAllFiles(ref EDI, ref sMsg) != 0)
                {
                    FErrorMsg = "Get FTP Error: " + sMsg; ;
                    break;
                }
            }
            catch (Exception e)
            {
                FErrorMsg = "Connect FTP Error: " + e.Message;
                break;
            }
            #endregion

            #region UpdateDatabase
            EDI_Files = EDI_DN.FileToDN(ref EDI);
            for (i = 0; i < EDI_Files.GetLength(0); i++)
            {
                string DN = EDI_Files[i, 0];
                string sDate = EDI_Files[i, 1];
                string FileName = EDI_Files[i, 2];
                string Ship_Qty= "";
                string Module = "";
                string Ship_to="";
                string Customer="";
                string Ship_Date= DateTime.Now.ToString("yyyyMMdd");
                //GetDNInformation(DN, ref Ship_Qty, ref Module, ref Ship_to, ref Customer, ref Ship_Date);
                ModifyFTRAT_A(DN, Ship_Qty, Module, Ship_to, Customer, Ship_Date, " ", "EDI", FileName, sDate, "U");
                ModifyFTRAT_B(DN, Ship_Qty, Module, Ship_to, Customer, Ship_Date, " ", "EDI", FileName, sDate);
            }
            iRet = 0;
            break;
            #endregion
        }

        return iRet;
    
    }

    public int Check_EDI_Send()
    {
        int iRet = -1;
        string sStr = "SELECT DISTINCT a.plant, a.invoice, a.customer_name, a.item_module, "+
                      "          b.ship_to_country,  "+
                      "          a.SHIPPED_QTY shipped_qty, "+
                      "          a.last_shipment_date, c.customer_type "+
                      "     FROM sfc.mes_mit_invoice a, "+
                      "          sap.cmcs_sfc_packing_lines_all b, "+
                      "          sfc.cmcs_sfc_model c "+
                      "    WHERE a.invoice = b.invoice_number "+
                      "      AND a.item_module = c.model "+
                      "      And  a.last_shipment_date > sysdate -10 "+
                      "      AND a.SHIPPED_QTY=(SELECT SUM (quantity) FROM SAP.cmcs_sfc_packing_lines_all b WHERE b.invoice_number = a.invoice) "+
                      "      AND a.invoice NOT IN (SELECT invoice FROM SFC.FTRAT1 Where Type = 'EDI' )  "+
                      "      ORDER BY a.last_shipment_date DESC ";
        OracleCommand cmd = null;
        OracleDataAdapter  oda = null;
        DataTable dt = null;
        try
        {
            CheckConnect();
            cmd = new OracleCommand(sStr, OrcConn);
            oda = new OracleDataAdapter();
            dt = new DataTable();
            oda.SelectCommand = cmd;
            oda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string sContent = "";
                int i;
                int j;
                for (i = 0; i < dt.Columns.Count; i++)
                    sContent +=  dt.Columns[i].Caption + System.Convert.ToChar(9);
                sContent += System.Convert.ToChar(13);
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    for (j = 0; j < dt.Columns.Count; j++)
                    {
                        string sTmp = dt.Rows[i][j].ToString();
                        if (sTmp == "") sTmp = " ";
                        sContent += sTmp + System.Convert.ToChar(9);
                    }
                    sContent += System.Convert.ToChar(13);
                }
                InsertNotesSend("DN Not Received EDI List - " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), sContent, "Qiang.q.chen@Foxconn.com", "", "BJ-IT-Develop/FIH/Foxconn@Foxconn.com", "EDI");
            }
            iRet = 0;
        }
        catch (Exception e)
        {
            iRet = -1;
            FErrorMsg = e.Message;
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
            if (oda != null) oda.Dispose();
            if (dt != null) dt.Dispose();
        }
        return iRet;
    
    
    }



    public string GetErrorMessage()
    {
        return FErrorMsg;
    }

}

public class SAPOperate
{
    private string FERROR;
    private string sapconnect;

    public SAPOperate()
    {
        FERROR = "";
        sapconnect = ConfigurationManager.AppSettings["SAPConnect"];
    }

    private SAPConnection ConnectSAP()
    {
        SAPConnection sapconn = new SAPConnection(sapconnect);
        return sapconn;
    }

    private string FillStringLeft(string sStr, string Def, int Len)
    {
        string Ret = sStr;
        while (Ret.Length < Len)
        {
            Ret = Def+Ret;
        }
        return Ret;
    }
    private string FillString(string sStr, string Def,int Len)
    {
        
        string Ret = sStr;
        while (Ret.Length < Len)
        {
            Ret += Def;
        }
        return Ret;
    }

    public string GetError()
    {
        string sRet = FERROR;
        return sRet;
    }

    /* This Function be used to call rfc ZRFC_SD_TJ_0001 
     * Sucessful return zero, else non-zero, use GetError get error message.
     * WBSTK: goods movement status
     *        C: Complete
     *        B: Part
     *        A/empty no
     * EDIFLAG : Y Need send EDI 
     *           N No
     * DNITEM  will return DN Detail
     * 
     */
    public int Call_ZRFC_SD_TJ_0001(string DN, ref string WBSTK, ref string EDIFLAG, ref string[,] DNITEM)
    {
        string cmdText = "EXEC ZRFC_SD_TJ_0001 @DNHEADER=@DN_H OUTPUT, @DNITEM =@DN_I OUTPUT";
        string sDN = DN;
        if (sDN.Length > 10) sDN = sDN.Substring(1, 10);
        if (sDN.Length != 10) sDN = FillStringLeft(sDN, "0", 10);
        DataTable DTHead = new DataTable();
        DTHead.Columns.Add("VBELN");
        DTHead.Columns.Add("WBSTK");
        DTHead.Columns.Add("EDIFLAG");
        DataRow dr = DTHead.Rows.Add();
        dr[0] = sDN;
        dr[1] = "";
        dr[2] = "";
        int iRet;


        try
        {
            SAPConnection sap = ConnectSAP();
            sap.Open();
            SAPCommand cmd = new SAPCommand(sap);
            cmd.CommandText = cmdText;

            SAPParameter DN_H = new SAPParameter("@DN_H", ParameterDirection.InputOutput);
            DN_H.Value = DTHead;
            cmd.Parameters.Add(DN_H);

            SAPParameter DN_I = new SAPParameter("@DN_I", ParameterDirection.InputOutput);
            cmd.Parameters.Add(DN_I);

            SAPDataReader drSAP = cmd.ExecuteReader();
            DataTable dt = (DataTable)cmd.Parameters["@DN_H"].Value;
            DataTable dtOutItem = (DataTable)cmd.Parameters["@DN_I"].Value;
            if (dt.Rows.Count > 0)
            {
                WBSTK = dt.Rows[0][1].ToString();
                EDIFLAG = dt.Rows[0][2].ToString();
            }
            DNITEM = new string[dtOutItem.Rows.Count,dtOutItem.Columns.Count];
            int iCol, iRow;
            for (iRow = 0; iRow < dtOutItem.Rows.Count; iRow++)
                for (iCol = 0; iCol < dtOutItem.Columns.Count; iCol++)
                    DNITEM[iRow, iCol] = dtOutItem.Rows[iRow][iCol].ToString();
            
            iRet = 0;
        }
        catch (Exception e)
        {
            FERROR = e.Message;
            iRet = -1;
        }
        return iRet;
    
    }

    /*
     * This Function be used to connect SAP & Check DN goods movement status
     * call SAP RFC: ZRFC_SD_TJ_0001
     * return 
     * < 0 System or Function Error; call GetError get error information
     * 0: Complete
     * 1: Part 
     * 2: no
     */
    public int CheckDNMovementStatus(string DN)
    {
        string WBSTK = "";
        string EDIFlag= "";
        string[,] DNITEM = new string[0,0];
        int iRet = Call_ZRFC_SD_TJ_0001(DN, ref WBSTK, ref EDIFlag, ref DNITEM);
        if (iRet == 0)
        {
            switch (WBSTK.ToUpper())
            {
                case "C": 
                    iRet = 0;
                    break;
                case "B":
                    iRet = 1;
                    break;
                default:
                    iRet = 2;
                    break;
            }
        }
        return iRet;
    }


    /* This function be used to check whether send EDT
     *  return 
     *  0: need send 
     *  1: not need.
     *  < 0 System error, use GetError return error message
     */
    public int CheckDN_EDISTATUS(string DN)
    {
        string WBSTK = "";
        string EDIFlag = "";
        string[,] DNITEM = new string[0, 0];
        int iRet = Call_ZRFC_SD_TJ_0001(DN, ref WBSTK, ref EDIFlag, ref DNITEM);
        if (iRet == 0)
        {
            if (EDIFlag.ToUpper() == "Y")
            {
                iRet = 0;
            }
            else
            {
                iRet = 1;
            }
        }
        return iRet;    
    }
   


}

