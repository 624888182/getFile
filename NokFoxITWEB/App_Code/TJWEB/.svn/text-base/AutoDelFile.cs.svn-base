using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.OracleClient;
using Microsoft.Adapter.SAP;
using System.IO;
using System.Net;


/// <summary>
/// Summary description for AutoDelFile
/// </summary>
public class AutoDelFile
{
	public AutoDelFile()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string FileHandle(string Sqlstr, string sPath, int timemm)
    {
        string sRet = "";
        string Sqlconn = "";
        DateTime dtime = DateTime.Now;
        string time2 = dtime.ToString("yyyyMMddhhmmssmm");
        string time1 = dtime.ToString("yyyyMMddHHmmss");
        int s = 1;
        try
        {

            if (Directory.Exists(sPath) == false)
            {
                Sqlconn = "INSERT INTO [ERPDBF].[dbo].[MessLog]  (F0,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10) VALUES('" + time1 + "','" + time2 + "','AutoDisScan','Error','','','','','','','Unable to connect to server')";// 
                DataBaseOperation.ExecSQL("sql", Sqlstr, Sqlconn);
                return "Please log " + sPath;
            }
            DirectoryInfo DirF = new DirectoryInfo(sPath);


            DirectoryInfo[] DirFS = DirF.GetDirectories();  // Get sub directors
            FileInfo[] Files = DirF.GetFiles();   // get all files in the path
            int i;
            for (i = 0; i < Files.GetLength(0); i++)
            {
                if (Files[i].CreationTime < System.DateTime.Now.AddMinutes(-timemm))  //判斷文件的創建時間是否大於timemm這個變量的時間（分鐘），如果大於這個分鐘，則刪除該文件
                {
                    string sFileName = Files[i].FullName;//文件的路徑與文件名
                    string sFileType = Files[i].Extension;//文件的類型
                    string sFileSize = Files[i].Length.ToString();//文件的字節大小
                    string sFileTime = Files[i].CreationTime.ToString();//文件創建時間
                    #region writeLog
                    //  INSERT 語句中，1表示刪除的是文件的字節大於300KB的文件，2表示刪除的是文件創建時間大於30分鐘（變量）的文件
                    Sqlconn = "INSERT INTO [ERPDBF].[dbo].[MessLog]  (F0,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10) VALUES('" + (time1 + s.ToString()) + "','" + time2 + "','AutoDisScan','" + sFileName + "','" + sFileType + "','" + sFileSize + "','DeleteFile','FileTime','" + sFileTime + "','2','success')";// 
                    s++;
                    DataBaseOperation.ExecSQL("sql", Sqlstr, Sqlconn);  //Sqlstr表示數據庫的IP地址，Sqlconn表示SQL語句

                    #endregion

                    File.Delete(Files[i].FullName);  // delete the file
                }
                else if (Files[i].Length > 1024 * 300) // if file the size more then 300KB
                {
                    string sFileName = Files[i].FullName;//文件的路徑與文件名
                    string sFileType = Files[i].Extension;//文件的類型
                    string sFileSize = Files[i].Length.ToString();//文件的字節大小
                    string sFileTime = Files[i].CreationTime.ToString();//文件創建時間
                    #region writeLog
                    //  INSERT 語句中，1表示刪除的是文件的字節大於300KB的文件，2表示刪除的是文件創建時間大於30分鐘（變量）的文件
                    Sqlconn = "INSERT INTO [ERPDBF].[dbo].[MessLog]  (F0,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10) VALUES('" + (time1 + s.ToString()) + "','" + time2 + "','AutoDisScan','" + sFileName + "','" + sFileType + "','" + sFileSize + "','DeleteFile','FileSize','" + sFileTime + "','1','success')";// 
                    s++;
                    DataBaseOperation.ExecSQL("sql", Sqlstr, Sqlconn);  //Sqlstr表示數據庫的IP地址，Sqlconn表示SQL語句

                    #endregion

                    File.Delete(Files[i].FullName);  // delete the file
                }
            }

            for (i = 0; i < DirFS.GetLength(0); i++)
                FileHandle(Sqlstr, DirFS[i].FullName, timemm);  // call itself handle sub director
            sRet = "Implementation Success";

        }
        catch (Exception e)
        {
            sRet = e.Message;
        }

        return sRet;


    }
}
