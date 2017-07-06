using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using System.Text;
/// <summary>
/// Summary description for FileOperation
/// </summary>
public class FileOperation
{
    public FileOperation()
    { 
    }
    
        private static string FERROR="";
        public static void WriteLog(string sFileName, string sContent)
        {
            StreamWriter sw = null;
            try
            {
                if (File.Exists(sFileName))
                {
                    sw = File.AppendText(sFileName);
                }
                else
                {
                    sw = File.CreateText(sFileName);
                }
                sw.WriteLine(sContent);
            }
            finally
            {
                if (sw != null) sw.Dispose();
            }
        }

        public static string GetError()
        {
            return FERROR;
        }

        public static int ReWriteFile(string sFileName, string TextBuffer)
        {
            int iRet;
            try
            {
                File.WriteAllText(sFileName, TextBuffer);
                iRet = 0;
            }
            catch (Exception e)
            {
                FERROR = e.Message;
                iRet = -1;
            }
            return iRet;
        }

        public static bool ReadFile(string sFileName)
        {
            bool iRet=false;
            while (true)
            {
                try
                {
                    if (File.Exists(sFileName) == false)
                    {
                        iRet = false;
                        FERROR = "File Not EXISTS";
                        break;
                    }
                    iRet = true;
                }
                catch (Exception e)
                {
                     
                    FERROR = e.Message;
                }
                break;
            }
            return iRet;
        }

        public static bool CreateFile(StringBuilder sb, string FilePathName)
        {
            if (File.Exists(FilePathName))
            {
                File.Delete(FilePathName);
            }
            bool bRet = false;
            if (sb != null && sb.Length > 0)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(FilePathName))
                    {
                        sw.Write(sb.ToString());
                        sw.Close();
                        bRet = true;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return bRet;
        }

    
}


