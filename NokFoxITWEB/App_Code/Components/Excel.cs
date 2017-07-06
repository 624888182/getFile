using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Microsoft.Office.Interop.Excel;
using System.Reflection;
using Excel;

/// <summary>
/// Excel 的摘要说明

/// </summary>
public class ExcelClass
{
    private _Application oXL = null;
    private _Workbook oWB = null;
    private _Worksheet oSheet;
    private Range oRng;
    Excel.Worksheet workSheet;
    Excel.Workbook workBook;
    Excel.Range range;



    /// <summary>
    /// WorkSheet数量
    /// </summary>

    public ExcelClass(string fileName)
    {
        try
        {
            //Start Excel and get Application object.
            oXL = new Excel.Application();//new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = false;

            //Get a new workbook.
            //Open a excel file ,which contain bom data, and do not update links ,and readonly.
            oWB = oXL.Workbooks.Open(fileName, 0, true,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing);

            oSheet = (_Worksheet)oWB.Sheets[1];
        }
        catch (Exception e)
        {
            throw e;
        }
    }



    public ExcelClass(string fileName,string tofilename)
    {
        try
        {
            //Start Excel and get Application object.
            oXL = new Excel.Application();  //Microsoft.Office.Interop.
            oXL.Visible = false; oXL.ScreenUpdating = false;

            //Get a new workbook.
            //Open a excel file ,which contain bom data, and do not update links ,and readonly.
            oWB = oXL.Workbooks.Open(fileName, 0, false,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing);
            oXL.ScreenUpdating = true; 
            oWB.SaveCopyAs(tofilename);
            oXL.ScreenUpdating = false;

            oWB = oXL.Workbooks.Open(tofilename, 0, false,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing);
            oSheet = (_Worksheet)oWB.Sheets[1];
        }
        catch (Exception e)
        {
            throw e;
        }
    }



    public void ActiveSheet(int index)
    {
        oSheet = (_Worksheet)oWB.Sheets[index];
    }
    

    public void AddRow(int rowIndex)
    {
        Excel.Range range = (Excel.Range)oSheet.Rows[rowIndex, Type.Missing];
        range.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
    }

    public void CopyRow(int rowIndex)
    {
        Excel.Range range1 = (Excel.Range)oSheet.Rows[rowIndex, Type.Missing];
        Excel.Range range2 = (Excel.Range)oSheet.Rows[rowIndex + 1, Type.Missing];
        range1.Copy(range2);
    }

    public string getCell(int rowIndex, int colIndex)
    {
        oRng = (Range)oSheet.Cells[rowIndex, colIndex];
        if (oRng.Value2 == null)
            return null;
        return oRng.Value2.ToString().Trim();
    }

    
    /// <summary>
    /// 向 excel 一个区域输入相同信息

    /// </summary>
    public void setCell(string Area1, string Area2, string tmpvalue)
    {

        oRng = (Range)oSheet.get_Range(Area1, Area2);      // Cells[rowIndex, colIndex];
        oRng.Value2 = tmpvalue;
    }


    
    /// <summary>
    /// 向 excel 一个方格输入信息

    /// </summary>
    public void setCell(string Area1, string tmpvalue)
    {

        oRng = (Range)oSheet.get_Range(Area1,Missing.Value);      // Cells[rowIndex, colIndex];
        oRng.Value2 = tmpvalue;
    }
    public void WorkSheetHide(string sheetName)
    {
        try
        {
            for (int i = 1; i <= this.oWB.Worksheets.Count; i++)
            {
                oSheet = (Excel.Worksheet)oWB.Sheets.get_Item(i);
                if (oSheet.Name == sheetName)
                {
                    oSheet.Visible = Excel.XlSheetVisibility.xlSheetHidden;
                }
            }

        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public void setHigh(int rowIndex)
    {
        Excel.Range range1 = (Excel.Range)oSheet.Rows[rowIndex, Type.Missing];
        range1.RowHeight = 20;

    }
    
    public DateTime getCellDateTime(int rowIndex, int colIndex)
    {
        oRng = (Range)oSheet.Cells[rowIndex, colIndex];
        if (oRng.Value2 == null)
          return DateTime.Now;
        return Convert.ToDateTime(oRng.Text);
    }

    public object getCellText(int rowIndex, int colIndex)
    {
        oRng = (Range)oSheet.Cells[rowIndex, colIndex];
        if (oRng.Value2 == null)
            return null;
        return oRng.Text;
    }
    /// <summary>
    /// 合并单元格，并赋值，对指定WorkSheet操作
    /// </summary>
    /// <param name="sheetIndex">WorkSheet索引</param>
    /// <param name="beginRowIndex">开始行索引</param>
    /// <param name="beginColumnIndex">开始列索引</param>
    /// <param name="endRowIndex">结束行索引</param>
    /// <param name="endColumnIndex">结束列索引</param>
    /// <param name="text">合并后Range的值</param>
    public void MergeCells( int beginRowIndex, int beginColumnIndex, int endRowIndex, int endColumnIndex, string text)
    {
        if (oSheet == null)
            return;

        range = oSheet.get_Range(oSheet.Cells[beginRowIndex, beginColumnIndex], oSheet.Cells[endRowIndex, endColumnIndex]);

        range.ClearContents();		//先把Range内容清除，合并才不会出错
        range.MergeCells = true;
        range.Value2 = text;
        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
    }


    public void close()
    {
        try
        {
            if (oRng != null)
            {
                oRng = null;
            }
            if (oSheet != null)
            {
                oSheet = null;
            }
            if (oWB != null)
            {
                oXL.ScreenUpdating = true;
                if (!oWB.Saved) oWB.Save(); 
                oWB.Close(Type.Missing, Type.Missing, Type.Missing);
            }
            if (oXL != null)
            {
                oXL.Quit();
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }


}
