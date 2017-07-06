using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  System.Xml;
using System.Data;
using System.Data.OracleClient;
using System.Security.Cryptography;
using System.IO;


namespace DB.EAI
{
    public class OracleDB : IDisposable
    {
        protected OracleConnection oraConn;

        public void Dispose()
        {
            if (oraConn != null)
            {
                if (oraConn.State != ConnectionState.Closed)
                {
                    oraConn.Close();
                }
                oraConn.Dispose();
            }
        }

        public OracleDB()
        {
            try
            {
                oraConn = new OracleConnection(GetConnectionString());
                oraConn.Open();
            }
            catch (OracleException ex)
            {
                Write("[ERROR] " + ex.Message + ":COracleDB()");
            }
        }

        public OracleDB(string strType)
        {
            try
            {
                oraConn = new OracleConnection(GetConnectionStringMail());
                oraConn.Open();
            }
            catch (OracleException ex)
            {
                Write("[ERROR] " + ex.Message + ":COracleDB() Mail");
            }
        }

        protected string GetConnectionString()
        {
            string strConn = "";
            strConn = System.Web.Configuration.WebConfigurationManager.AppSettings["MGT"];
            return strConn;
        }

        protected string GetConnectionStringMail()
        {
            string strConn = "";
            strConn = System.Web.Configuration.WebConfigurationManager.AppSettings["MGT"];
            return strConn;
        }

        public OracleDataReader GetDataReader(string strBySql)
        {
            OracleDataReader odr = null;
            OracleCommand oCmd = null;
            try
            {
                oCmd = new OracleCommand(strBySql, oraConn);
                odr = oCmd.ExecuteReader();
            }
            catch (OracleException ex)
            {
                Write("[ERROR] " + ex.Message + ":GetDataReader()");
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Dispose();
                }
            }

            return odr;
        }

        public bool ExecuteNonQuery(string strBySql)
        {
            bool bRet = false;
            OracleCommand oCmd = null;
            try
            {
                oCmd = new OracleCommand(strBySql, oraConn);
                int i = Convert.ToInt32(oCmd.ExecuteNonQuery());
                if (i > 0)
                {
                    bRet = true;
                }
            }
            catch (OracleException ex)
            {
                Write("[ERROR] " + ex.Message + ":ExecuteNonQuery - " + strBySql);
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Dispose();
                }
            }

            return bRet;
        }

        public DataSet GetDataSet(string strBySql)
        {
            DataSet odr = new DataSet();
            OracleCommand oCmd = null;
            try
            {
                oCmd = new OracleCommand(strBySql, oraConn);
                OracleDataAdapter dd = new OracleDataAdapter();
                dd.SelectCommand = oCmd;
                dd.Fill(odr);
            }
            catch (OracleException ex)
            {
                Write("[ERROR] " + ex.Message + ":GetDataReader()");
            }
            finally
            {
                if (oCmd != null)
                {
                    oCmd.Dispose();
                }
            }

            return odr;
        }

        /// <summary>
        /// Use e.x
        /// LH_CHECK_LOTCODE(DATA IN VARCHAR2,LANG IN VARCHAR2, RES OUT VARCHAR2)
        /// string[] strArr = COracleDB.ExecuteStoredProcedure("LVBASIC.LH_CHECK_LOTCODE", new string[] { "DATA","LANG", "RES" }, new string[] { "1TF4263733","TW"});
        /// </summary>
        /// <param name="strStoredProcedureName"></param>
        /// <param name="strsParams"></param>
        /// <param name="strsValue"></param>
        /// <returns></returns>
        public bool ExecuteStoredProcedure(string strStoredProcedureName, string[] strParams, string[] strValue, out string[] strOutput)
        {
            OracleCommand ocmd = null;
            bool bStatus = false;
            strOutput = new string[strParams.Length - strValue.Length];
            try
            {
                ocmd = new OracleCommand();
                ocmd.Connection = oraConn;
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.CommandText = strStoredProcedureName;
                for (int i = 0; i < strParams.Length; i++)
                {
                    if (i > strValue.Length - 1)
                    {
                        ocmd.Parameters.Add(strParams[i], OracleType.NVarChar, 200);
                        ocmd.Parameters[strParams[i]].Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        ocmd.Parameters.AddWithValue(strParams[i], strValue[i]);
                    }
                }
                ocmd.ExecuteNonQuery();
                int iOutputIndx = 0;
                for (int i = strValue.Length; i < strParams.Length; i++)
                {
                    strOutput[iOutputIndx] = ocmd.Parameters[strParams[i]].Value.ToString();
                    iOutputIndx++;
                }
                bStatus = true;
            }
            catch (OracleException ex)
            {
                bStatus = false;
                Write("[ERROR] " + ex.Message + " :ExecuteStoredProcedure() :" + strStoredProcedureName);
            }
            finally
            {
                if (ocmd != null)
                {
                    ocmd.Dispose();
                }

            }
            return bStatus;
        }
        #region XML
        public static string GetConfigPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
        }

        public static string XmlGetValue(string psMain, string psSub, string psThree)
        {
            string sReturn = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(GetConfigPath());
                XmlNode node1 = doc.SelectSingleNode("/" + psMain + "/" + psSub + "/" + psThree);
                sReturn = node1.InnerText;
            }
            catch { }
            return sReturn;
        }

        public static bool XmlSetValuestring(string psMain, string psSub, string psThree, string psKeyValue)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(GetConfigPath());
                XmlNode node1 = doc.SelectSingleNode("/" + psMain + "/" + psSub + "/" + psThree);
                node1.InnerText = psKeyValue;
                doc.Save(GetConfigPath());
                return true;
            }
            catch { return false; }
        }

        public static bool NodeIsExist(string psMain, string psSub)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(GetConfigPath());
                XmlNodeList xnList = doc.SelectNodes("/" + psMain + "/" + psSub);
                if (xnList.Count == 0)
                {
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        #endregion
        #region Utility
        public static string EncryptText(String strText)
        {
            return Encrypt(strText, "&%#@?,:*");
        }

        public static String DecryptText(String strText)
        {
            return Decrypt(strText, "&%#@?,:*");
        }

        private static String Encrypt(String strText, String strEncrKey)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static String Decrypt(String strText, String sDecrKey)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            Byte[] inputByteArray = new byte[strText.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region Log
        public static void Write(string strlog)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Log\\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
            }
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\CDMA_UPD" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            System.IO.FileStream oFileStream;
            if (!System.IO.File.Exists(filename))
            {
                oFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
            }
            else
            {
                oFileStream = new System.IO.FileStream(filename, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
            }
            System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(oFileStream, System.Text.Encoding.GetEncoding("big5"));
            oStreamWriter.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " || " + strlog);
            oStreamWriter.Close();
            oFileStream.Close();
        }

        public static void Write(string strfilename, int iRow, string strErrorType, string strErrors)
        {
            Write("Error [" + strfilename + "] [" + iRow.ToString() + "] [" + strErrorType + "] [" + strErrors + "]");
        }
        #endregion

    }

}
