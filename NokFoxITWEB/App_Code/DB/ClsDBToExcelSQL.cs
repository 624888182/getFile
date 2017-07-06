/*************************************************************************
 * 
 *  Unit description: Export to excel by different method
 *  Developer: Shu Jian Bo             Date: 2007/09/08
 *  Modifier : Shu Jian Bo             Date: 2007/09/08
 * 
 * ***********************************************************************/
using System;
using System.Reflection;
using System.Configuration;
using Excel2 = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
/// <summary>
/// ClsDBToExcelSQL 的摘要描述
/// </summary>
/// 
namespace DB.EAI
{
    public class ClsDBToExcelSQL
    {
        public ClsDBToExcelSQL()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public static void ExportToExcel(Excel2.Worksheet objSheet, string strSql)
        {
            Missing missing = Missing.Value;

            string conn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
 
            //string strConn = ConfigurationSettings.AppSettings["DataSource"].ToString().ToUpper();
            //string strConn = ConfigurationManager.AppSettings["DataSource"].ToString().ToUpper();
            //string conn = "ODBC;DRIVER={Oracle in OraHome92};SERVER=" + strConn + ";UID=SFC;PWD=SFC;DBQ=" + strConn + ";";
            Excel2.QueryTable tb = objSheet.QueryTables.Add(conn, objSheet.get_Range("A1", missing), strSql); 
            //tb.Name = "來自 FCNP 的查詢";
            tb.FieldNames = true;
            tb.RowNumbers = false;
            tb.FillAdjacentFormulas = false;
            tb.PreserveFormatting = true;
            tb.RefreshOnFileOpen = false;
            tb.BackgroundQuery = true;
            tb.RefreshStyle = Excel2.XlCellInsertionMode.xlInsertDeleteCells;
            tb.SavePassword = false;
            tb.SaveData = true;
            tb.AdjustColumnWidth = true;
            tb.RefreshPeriod = 0;
            tb.PreserveColumnInfo = true;
            tb.BackgroundQuery = false;
            tb.Refresh(tb.BackgroundQuery);

            try
            {
                //objSheet.PageSetup.LeftMargin = 20;
                //objSheet.PageSetup.RightMargin = 20;
                //objSheet.PageSetup.TopMargin = 35;
                //objSheet.PageSetup.BottomMargin = 15;
                //objSheet.PageSetup.HeaderMargin = 7;
                //objSheet.PageSetup.FooterMargin = 10;
                //objSheet.PageSetup.CenterHorizontally = true;
                //objSheet.PageSetup.CenterVertically = false;
                //objSheet.PageSetup.Orientation = Excel2.XlPageOrientation.xlPortrait;
                //objSheet.PageSetup.PaperSize = Excel2.XlPaperSize.xlPaperA4;
                //objSheet.PageSetup.Zoom = false;
                //objSheet.PageSetup.FitToPagesWide = 1;
                //objSheet.PageSetup.FitToPagesTall = false;

            }
            catch
            {
            }
        }


        public static string GetCellName(int x, int y)
        {
            int A = 65;

            if (x <= 0 || x > 255)
            {
                throw new ArgumentException("Column index error!");
            }
            if (x <= 26)
            {
                return Convert.ToChar(A + x - 1).ToString() + y.ToString();
            }
            else
            {
                int unitsDigit = x % 26;//個位
                int tensDigit = x / 26;//十位
                string columnName = Convert.ToChar(A + tensDigit - 1).ToString();
                if (unitsDigit > 0)
                {
                    columnName += Convert.ToChar(A + unitsDigit - 1).ToString();
                }
                return columnName + y.ToString();
            }
        }

        public static void SetRangeValue(Excel2.Worksheet sheet, string cell1, string cell2, string valueText,
            bool boldFont, string fontName, int fontSize, System.Drawing.Color backColor,
            string horizontalAlign, string verticalAlign, double columnWidth, double columnHeight, bool ItalicFont, bool Line)
        {
            Excel2.Range range = sheet.get_Range(cell1, cell2);
            range.Select();
            range.MergeCells = true;
            range.Value2 = valueText;
            if (Line)
            {
                range.Borders.LineStyle = 1;
            }
            range.WrapText = false;
            //			range.Width = width;
            //			range.Height = height;
            range.Font.Bold = boldFont;
            range.Font.Italic = ItalicFont;
            //======================================
            if (fontName.Trim() != "")
            {
                range.Font.Name = fontName;
            }
            if (backColor != System.Drawing.Color.Empty)
            {
                range.Interior.Color = backColor.ToArgb();
            }
            if (fontSize != -1)
            {
                range.Font.Size = fontSize;
            }

            //=======================================
            range.VerticalAlignment = Excel2.XlVAlign.xlVAlignCenter;
            range.HorizontalAlignment = Excel2.XlHAlign.xlHAlignCenter;

            if (horizontalAlign.ToUpper() == "LEFT")
            {
                range.HorizontalAlignment = Excel2.XlHAlign.xlHAlignLeft;
            }
            else if (horizontalAlign.ToUpper() == "RIGHT")
            {
                range.HorizontalAlignment = Excel2.XlHAlign.xlHAlignRight;
            }
            else if (horizontalAlign.ToUpper() == "CENTER")
            {
                range.HorizontalAlignment = Excel2.XlHAlign.xlHAlignCenter;
            }
            //======================================
            if (verticalAlign.ToUpper() == "TOP")
            {
                range.VerticalAlignment = Excel2.XlVAlign.xlVAlignTop;
            }
            else if (horizontalAlign.ToUpper() == "BOTTON")
            {
                range.VerticalAlignment = Excel2.XlVAlign.xlVAlignBottom;
            }
            else if (horizontalAlign.ToUpper() == "CENTER")
            {
                range.VerticalAlignment = Excel2.XlVAlign.xlVAlignCenter;
            }

            //======================================
            if (columnWidth != 0)
            {
                range.ColumnWidth = columnWidth;
            }
            if (columnHeight != 0)
            {
                range.RowHeight = columnHeight;
            }


            System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
            range = null;
        }	

    }
}
