using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.OleDb;

namespace Economy.Publibrary
{
    public class ExcelWRlib
    {
        public ExcelWRlib()
        {
        }

        public static System.Data.DataSet ReadDataExcellib(string FileName)
        {
            DataSet dataSet = new DataSet();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;"
                + "Data Source=" + FileName + ";"
                + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";

            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();

                string xlsSheetNames = GetXlsSheetNames(connection);

                foreach (string sheetName in xlsSheetNames.Split(','))
                {
                    OleDbCommand command = new OleDbCommand("SELECT * FROM [" + sheetName + "$]", connection);
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataSet, sheetName);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

            }
            finally
            {
                connection.Close();
            }
            return dataSet;
        }
        //讀EXCEL的sheet名

        private static string GetXlsSheetNames(OleDbConnection connection)
        {
            string sheetNames = "";
            DataTable schemaTable = connection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string tableName = schemaTable.TableName;
            int rowCount = schemaTable.Rows.Count;
            string sheetName = "";
            int itemIndex = 0;

            try
            {
                while (itemIndex < rowCount)
                {
                    DataRow row = schemaTable.Rows[itemIndex];
                    sheetName = row[2].ToString();
                    if (sheetName.Substring(sheetName.Length - 1, 1) == "$")
                    {
                        System.Math.Min(System.Threading.Interlocked.Increment(ref itemIndex), itemIndex - 1);
                        sheetNames += sheetName;
                    }
                    else
                    {
                        itemIndex = itemIndex + 1;
                    }
                }

                if (sheetNames == null)
                {
                    connection.Close();
                }

                sheetNames = sheetNames.Replace("$", ",");
                if (sheetNames.EndsWith(","))
                {
                    sheetNames = sheetNames.Substring(0, sheetNames.Length - 1);
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            return sheetNames;
        }

        //得到 sheet 的名字

        public static string GetXlsSheetNames(string FileName)
        {
             string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;"
                + "Data Source=" + FileName + ";"
                + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            string sheetNames = "";
            DataTable schemaTable = connection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string tableName = schemaTable.TableName;
            int rowCount = schemaTable.Rows.Count;
            string sheetName = "";
            int itemIndex = 0;

            try
            {
                while (itemIndex < rowCount)
                {
                    DataRow row = schemaTable.Rows[itemIndex];
                    sheetName = row[2].ToString();
                    if (sheetName.Substring(sheetName.Length - 1, 1) == "$")
                    {
                        System.Math.Min(System.Threading.Interlocked.Increment(ref itemIndex), itemIndex - 1);
                        sheetNames += sheetName;
                    }
                    else
                    {
                        itemIndex = itemIndex + 1;
                    }
                }

                if (sheetNames == null)
                {
                    connection.Close();
                }

                sheetNames = sheetNames.Replace("$", ",");
                if (sheetNames.EndsWith(","))
                {
                    sheetNames = sheetNames.Substring(0, sheetNames.Length - 1);
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }
            return sheetNames;
        }

        //
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)       //the blank
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
            }
            return new string(c);
        }

        public static string ToSBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)         //the blank
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                {
                    c[i] = (char)(c[i] + 65248);
                }
            }
            return new string(c);
        }
        
    }
 }