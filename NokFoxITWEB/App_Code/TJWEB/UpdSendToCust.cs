using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
/// <summary>
/// Summary description for SendFile
/// </summary>
public class  UpdSendToCust
{

    //private static readonly string UPD_UPLOAD_PROGRAM_PATH = @"C:\MCMS\PROGRAM\";
    private static string ErrorMessage;


    public UpdSendToCust()
	{
		//
		// TODO: Add constructor logic here
		//
        ErrorMessage = "";
	}
    public int AutoSendFile(string DBReadString,string DBWriString, string TYPE)
    {
        int iTea = 0;
        try
        {
            int flag1 = 0;
            string Filepath1 = "";
            string Filepath2 = "";
            string name1 = "";
            string name2 = "";
            string str = "select INVOICE,PO,UPDFILENAME,IMEIFILENAME,GFSFILENAME,MODELTYPE,SENDFLAG from PUBLIB.UPD_PODN_LIST_T where UserConfirm='Y' and SendFlag is null";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = DataBaseOperation.SelectSQLDT(TYPE, DBReadString, str);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iTea = 0;
                    if (dt.Rows[i][2].ToString().Length != 8)
                    {
                        Filepath1 = "D:\\MCMS\\DataBack\\UPD\\";
                        Filepath2 = "C:\\MCMS\\upd_service\\to_be_processed\\";
                        name1 = dt.Rows[i][2].ToString();
                        if (GetFileInformation(Filepath1, name1) == "OK")
                        {
                            Filepath1 = Filepath1 + name1;
                            Filepath2 = Filepath2 + name1;
                            File.Copy(Filepath1, Filepath2);
                            flag1++;
                            iTea = 1;
                        }
                    }
                    //if(dt.Rows[i][3].ToString().Length!=8)
                    //{
                    //    Filepath1 = "D:\\abc\\";
                    //    Filepath2 = "D:\\abc\\";
                    //    name1 = dt.Rows[i][3].ToString();
                    //    if (GetFileInformation(Filepath1, name1) == "OK")
                    //    {
                    //        Filepath1 = Filepath1 + name1;
                    //        Filepath2 = Filepath2 + name1;
                    //        File.Copy(Filepath1, Filepath2);
                    //        flag1++;
                    //        iTea = 1;

                    //    }
                    //}
                    //if (dt.Rows[i][4].ToString().Length != 8)
                    //{
                    //    Filepath1 = "D:\\abc\\";
                    //    Filepath2 = "D:\\abc\\";
                    //    name1 = dt.Rows[i][4].ToString();
                    //    if (GetFileInformation(Filepath1, name1) == "OK")
                    //    {
                    //        Filepath1 = Filepath1 + name1;
                    //        Filepath2 = Filepath2 + name1;
                    //        File.Copy(Filepath1, Filepath2);
                    //        flag1++;
                    //        iTea = 1;

                    //    }
                    //}
                    if (flag1 == 1)
                    {
                        str = "update PUBLIB.UPD_PODN_LIST_T set SENDFLAG='Y' where INVOICE='" + dt.Rows[i][0].ToString() + "'";
                        DataBaseOperation.ExecSQL(TYPE, DBReadString, str);
                        flag1 = 0;
                    }
                }
            }
            string FailPath = "C:\\MCMS\\upd_service\\failed";
            if (SeleteFileFail(FailPath, DBReadString, TYPE) == "OK")//判断是否有发送不成功的文件，如果有，则DEL，重新发送
            {
                iTea = 1;
            }
            else
            {
                iTea = 0;
            }


        }
        catch (Exception e)
        {
            iTea =0;
        }

        return iTea;
    }

    public string GetFileInformation(string path, string getFileName)    
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] allFiles = dir.GetFiles();
        int k = 0;
        foreach (FileInfo fi in allFiles)
        {
            string fileName = fi.Name;
            if (fileName == getFileName)
            {
                k = 1;
                break;
            }

        }
        if (k == 1)
        {
            return "OK";
        }
        else
        {
            return "Eorro";
        }
    }

    public string SeleteFileFail(string path,string DBstr,string TYPE)
    {
        string k = "";
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] allFiles = dir.GetFiles();

            if (allFiles.GetLength(0) > 0)
            {
                foreach (FileInfo fi in allFiles)
                {
                    string fileName = fi.Name;

                    string str = "UPDATE PUBLIB.UPD_PODN_LIST_T set SENDFLAG='N' WHERE UPDFILENAME='" + fileName + "'";
                    DataBaseOperation.ExecSQL(TYPE, DBstr, path);
                    fi.Delete();

                }
            }
            k = "OK";
        }
        catch (Exception e)
        {
            k=e.Message;
        }
        return k;
        
    }


}
