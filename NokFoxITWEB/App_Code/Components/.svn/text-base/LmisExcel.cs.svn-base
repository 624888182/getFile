using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;

//using FIH.LmisExcel;

/// <summary>
/// Class1 的摘要说明

/// </summary>
public class LmisExcel
{

    private string Bill_No;
    private IList<string> str_Message;

    //FIH.lmis.db.OutboundBody myEntity = new OutboundBody();
    //FIH.lmis.db.OutboundBodyInfo Info = new OutboundBodyInfo();

    public LmisExcel()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public string BillNo
    {
        get { return Bill_No; }
        set { Bill_No = value; }
    }


    public IList<string> Message
    {
        get { return str_Message; }
    }


    #region 判斷標題是否正確
    public void CheckCell(string strgetCell, int rowIndex, int colIndex, string strColName)
    {
        if (strgetCell != strColName)
        {
            str_Message.Add("對不起，第[" + rowIndex.ToString() + "," + colIndex.ToString() + "]格應該為'" + strColName + "'欄的標題");
        }
    }
    #endregion

    #region 判斷cell[i，j]是否為空值

    public void CheckCellValueNull(string strgetCell, int rowIndex, string strColName)
    {
        if (string.IsNullOrEmpty(strgetCell))
        {
            str_Message.Add("對不起，序號為[" + Convert.ToString(rowIndex - 2) + "]行的'" + strColName + "'欄不能為空值");
        }
    }
    #endregion

    #region 判斷cell[i，j]是否為整数

    public void CheckCellValueInt(string strgetCell, int rowIndex, string strColName)
    {
        try
        {
            Convert.ToInt32(strgetCell);
        }
        catch
        {
            str_Message.Add("對不起，序號為[" + Convert.ToString(rowIndex - 2) + "]行的'" + strColName + "'欄必須為整数");
        }
    }
    #endregion

    #region 判斷cell[i，j]是否為日期型
    public void CheckCellValueDateTime(string strgetCell, int rowIndex, string strColName)
    {
        try
        {
            Convert.ToDateTime(strgetCell);
        }
        catch
        {
            str_Message.Add("對不起，序號為[" + Convert.ToString(rowIndex - 2) + "]行的'" + strColName + "'欄必須為日期");
        }
    }
    #endregion

    #region 判斷cell[i，j]是否為小数

    public void CheckCellValueDecimal(string strgetCell, int rowIndex, string strColName)
    {
        try
        {
            Convert.ToDecimal(strgetCell);
        }
        catch
        {
            str_Message.Add("對不起，序號為[" + Convert.ToString(rowIndex - 2) + "]行的'" + strColName + "'欄必須為小数");
        }
    }
    #endregion

    #region 判斷cell[i，j]在不為空的情況下是否為小数

    public void CheckCellValueDecimalCanNull(string strgetCell, int rowIndex, string strColName)
    {
        try
        {
            if (strgetCell.Trim() != "" && strgetCell.Length > 0)
            {
                Convert.ToDecimal(strgetCell);
            }
        }
        catch
        {
            str_Message.Add("對不起，序號為[" + Convert.ToString(rowIndex - 2) + "]行的'" + strColName + "'欄必須為小数");
        }
    }
    #endregion

    #region 用于刪除所上傳的文件

    public void DeleteFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }
    #endregion

    #region 判斷excel的格式是否正確，不正確返回false，並將錯誤信息保存在strMessage
    public bool CheckExcelData(string fileName)
    {
        ExcelClass excel = null;
        try
        {
            excel = new ExcelClass(fileName);
            str_Message = new List<string>();// Store string message.
            CheckCell(excel.getCell(1, 1).ToString(), 1, 1, "鴻海料號");
            CheckCell(excel.getCell(1, 2).ToString(), 1, 2, "客戶料號");
            CheckCell(excel.getCell(1, 3).ToString(), 1, 3, "貨物名稱");
            CheckCell(excel.getCell(1, 4).ToString(), 1, 4, "數量");
            CheckCell(excel.getCell(1, 5).ToString(), 1, 5, "單價(USD)");
            CheckCell(excel.getCell(1, 6).ToString(), 1, 6, "包裝方式");
            CheckCell(excel.getCell(1, 7).ToString(), 1, 7, "長(cm)");
            CheckCell(excel.getCell(1, 8).ToString(), 1, 8, "寬(cm)");
            CheckCell(excel.getCell(1, 9).ToString(), 1, 9, "高(cm)");
            CheckCell(excel.getCell(1, 10).ToString(), 1, 10, "材積重(kg)");
            CheckCell(excel.getCell(1, 11).ToString(), 1, 11, "毛重(kg)");
            CheckCell(excel.getCell(1, 12).ToString(), 1, 12, "淨重(kg)");
            CheckCell(excel.getCell(1, 13).ToString(), 1, 13, "箱數");
            CheckCell(excel.getCell(1, 14).ToString(), 1, 14, "小計(USD)");


            //假如前面沒有錯誤則繼續執行下面代碼

            for (int i = 2; excel.getCell(i, 1) != null; i++)
            {
                
                    //判断是否为空值

                   CheckCellValueNull(excel.getCell(i, 1), i, "鴻海料號");
                    //判斷是否日期

                    //CheckCellValueDateTime(excel.getCellText(i, 3).ToString(), i, "運輸日期");

                    //CheckCellValueNull(excel.getCellText(i, 4).ToString(), i, "大陸車牌號碼");
                    ////判断类别代码是否存在
                    //CheckCellValueNull(excel.getCellText(i, 5).ToString(), i, "卡車類別代碼");

                   // CheckCellValueNull(excel.getCellText(i, 7).ToString(), i, "派車單號");

                    //判断縂箱數是否为整数
                    CheckCellValueInt(excel.getCell(i, 4).ToString(), i, "數量");
                    CheckCellValueInt(excel.getCell(i, 13).ToString(), i, "箱數");
                    //判斷縂重量是否为小数
                   //CheckCellValueDecimal(excel.getCell(i, 9).ToString(), i, "縂重量");
                    //若中港運輸費.等不為空則判斷縂重量是否为小数

                    CheckCellValueDecimalCanNull(excel.getCell(i, 5).ToString(), i, "單價(USD)");
                    CheckCellValueDecimalCanNull(excel.getCell(i, 7).ToString(), i, "長(cm)");
                    CheckCellValueDecimalCanNull(excel.getCell(i, 8), i, "寬(cm)");
                    CheckCellValueDecimalCanNull(excel.getCell(i, 9), i, "高(cm)");
                    CheckCellValueDecimalCanNull(excel.getCell(i, 10), i, "材積重(kg)");
                    CheckCellValueDecimalCanNull(excel.getCell(i, 11), i, "毛重(kg)");
                    CheckCellValueDecimalCanNull(excel.getCell(i, 12), i, "淨重(kg)");

                
                if (str_Message.Count > 0)
                    return false;   //当strMessage中有錯誤信息，則不再執行
            }
        }
        catch
        {
            if (str_Message == null)
            {
                str_Message = new List<string>();
            }
            str_Message.Add("Excel表中存在錯誤！");
        }
        finally
        {
            if (excel != null)
            {
                excel.close();
            }
        }

        //if (File.Exists(fileName))
        //{
        //    File.Delete(fileName);
        //}

        //返回值

        if (str_Message.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion



    //#region 判斷excel的格式是否正確，不正確返回false，並將錯誤信息保存在strMessage
    ////string sqlss = "sqlss";
    //public bool SaveExcelDataToDataBase(string fileName, string OutBoundID)
    //{
    //    ExcelClass excel = null;
    //    //事務處理
    //    string myConn = System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"].ToString();
    //    SqlConnection dbConn = new SqlConnection(myConn);
    //    dbConn.Open();
    //    SqlTransaction Trans = dbConn.BeginTransaction();
    //    SqlCommand cmdTrans = new SqlCommand();
    //    try
    //    {
    //        cmdTrans.Connection = dbConn;
    //        cmdTrans.Transaction = Trans;

    //        //-----------excel操作 開始            
    //        excel = new ExcelClass(fileName);
    //        DbAccessingLmisExcel db = new DbAccessingLmisExcel();
    //        //string s_BillListNo = "";  // 單據明細號

    //        string sql = "";
    //        for (int i = 2; excel.getCell(i, 1) != null; i++)
    //        {

    //                Info.OutBoundHeadId = OutBoundID;

    //                sql = "select max(ItemNo) from tbout_OutboundBody where OutBoundHeadId = '" + OutBoundID + "'";
    //                Info.ItemNo =Convert.ToInt32(PubFunction.GetMaxItem(sql));


                   

                
    //                Info.HsMaterialNo = excel.getCell(i, 1);
    //                Info.CustomMaterialNo = excel.getCell(i, 2);
    //                Info.Descript = excel.getCell(i, 3);

    //                string Volume = excel.getCell(i, 7) + "*" + excel.getCell(i, 8) + "*" + excel.getCell(i, 9);
    //                Info.PalletNo = Volume;
    //                //
    //                Info.Number = Convert.ToInt32((excel.getCell(i, 4) == "") ? "0" : excel.getCell(i, 4));
    //                Info.Volumuweight = Decimal.Parse((excel.getCell(i, 10) == "") ? "0" : excel.getCell(i, 10));
    //                Info.GrossWeight = Decimal.Parse((excel.getCell(i, 11) == "") ? "0" : excel.getCell(i, 11));
    //                Info.NetWeight = Decimal.Parse((excel.getCell(i, 12) == "") ? "0" : excel.getCell(i, 12));
    //                Info.BoxNumber = Convert.ToInt32((excel.getCell(i, 13) == "") ? "0" : excel.getCell(i, 13));


    //                //載入縂費用

                   

    //                //載入創立信息
    //                Info.CreaterID = System.Web.HttpContext.Current.Session["loginUser"].ToString();
    //                Info.CreaterDate = DateTime.Parse(DateTime.Now.ToShortDateString());
    //                Info.ModiID = System.Web.HttpContext.Current.Session["loginUser"].ToString();
    //                Info.ModiDate = DateTime.Parse(DateTime.Now.ToShortDateString());


    //                //把載體中的數據保存到數據庫


    //                myEntity.Insert(Info);
           
    //            }
            

    //        //-----------excel操作 結束 

    //        Trans.Commit();
    //        return true;
    //        //return sqls;
    //    }
    //    catch
    //    {
    //        Trans.Rollback();
    //        return false;

    //        //return sqlss;//"可能SQL問題";
    //    }
    //    finally
    //    {
    //        if (excel != null)
    //        {
    //            excel.close();
    //        }
    //        cmdTrans.Dispose();
    //        dbConn.Close();
    //    }

    //}

    //#endregion




}
