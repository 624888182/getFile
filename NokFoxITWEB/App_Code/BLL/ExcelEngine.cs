using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Web.UI;
using System.Data.OleDb;

namespace Economy.BLL
{
    /// <summary>
    /// Summary description for ExcelEngine
    /// </summary>
    public class ExcelEngine
    {
        public ExcelEngine()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static System.Data.DataSet ReadDataExcel(string FileName,string sheetName,string excelClomnName)
        {
            DataSet dataSet = new DataSet();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;"
                + "Data Source=" + FileName + ";"
                + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";

            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();


                string str="SELECT " + excelClomnName + " FROM [" +sheetName+ "$]";
               
                OleDbCommand command = new OleDbCommand(str, connection);
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataSet, "ds");
               
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

        public static System.Data.DataSet ReadDataExcel(string FileName)
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
                    string str="SELECT * FROM [" + sheetName + "$]";
                    OleDbCommand command = new OleDbCommand(str, connection);
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

    }
}
