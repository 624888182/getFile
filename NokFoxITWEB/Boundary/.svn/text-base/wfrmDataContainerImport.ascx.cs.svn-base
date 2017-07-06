using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Excel9 = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Data.OracleClient;
using System.Reflection;

public partial class Boundary_wfrmDataContainerImport : System.Web.UI.UserControl
{
    private Excel9.ApplicationClass ObjApp;
    private Excel9.Workbooks ObjBooks;
    private Excel9.Workbook ObjBook;
    private Excel9.Worksheet ObjSheet;
    private Missing miss = Missing.Value;
    private string[] FiledName;
    private string[] FiledValue;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Button1.Enabled = false;
        BeforImport();
        Import();
        Quit();
        Response.Write("<script>alter('導入成功！')</script>");
        this.Button1.Enabled = true;
    }
    private void Import()
    {
        ObjApp = new Excel9.ApplicationClass();
        ObjBooks = (Excel9.Workbooks)ObjApp.Workbooks;
        ObjBook = ObjBooks.Add(tbPath.FileName);
        ObjSheet = (Excel9.Worksheet)ObjBook.Worksheets[1];
        Excel9.Range Rng1;
        Excel9.Range Rng2;
        Excel9.Range Rng3;
        Excel9.Range Rng4;
        Excel9.Range Rng5;
        string Item;
        string Group;
        string Name;
        string Value;
        string Version;
        string temp;
        string FiledName = "";
        string FiledValue = "";
        int j = 0;
        bool flag = true;

        int ii = 0;
        int kk = 4;   //  控制 : 讀第幾列 


        int c = ObjSheet.Rows.Count;

        for (int i = 1; i <= ObjSheet.Rows.Count; i++)
        {
            Item = "";
            Group = "";
            Name = "";
            Value = "";
            Version = "";
            temp = "";

            Rng1 = (Excel9.Range)ObjSheet.Cells[i, kk];
            Rng2 = (Excel9.Range)ObjSheet.Cells[i, kk + 1];
            Rng3 = (Excel9.Range)ObjSheet.Cells[i, kk + 2];

            Rng4 = (Excel9.Range)ObjSheet.Cells[i, kk + 4];
            Rng5 = (Excel9.Range)ObjSheet.Cells[i, kk + 3];

            if (Rng1.Value2 != null)
            {
                Item = Rng1.Value2.ToString();
            }
            if (Rng2.Value2 != null)
            {
                Group = Rng2.Value2.ToString();
                if (Group != "")
                {
                    if (Group.Length == 1)
                    {
                        j = j + 1;  //用於控制Download_Set

                    }
                    else if (Group.Substring(0, 1).ToUpper() == "U")
                    {
                        break;   //  Exit
                    }
                    else
                    {
                        j = 0;
                    }

                }


            }



            if (Rng3.Value2 != null)
            {
                Name = Rng3.Value2.ToString();   // Filed

                #region Download_Set 動態(VERSION1,COMMENT1,VERSION2,COMMENT2,VERSION3,COMMENT4)
                if (j > 0)
                {
                    if (j == 7 && Name.ToUpper() == "COMMENT")
                    {
                        flag = false;
                    }

                    Name = Name + j.ToString();

                }
                #endregion

            }
            if (Rng4.Value2 != null)
            {
                Value = Rng4.Value2.ToString();  // Value


                if (Value.ToUpper() == "NONE" || Value.ToUpper() == "NULL" || Value.Trim() == "")
                {
                    Value = "";
                }

            }
            if (Rng5.Value2 != null)
            {
                Version = Rng5.Value2.ToString();
                if (Version.Substring(0, 1).ToUpper() == "R")
                {
                    FiledName = FiledName + "SALES_MODEL_REVISION" + ",";
                    FiledValue = FiledValue + "'" + Value + "'" + ",";
                }
            }

            #region Control Exit ---> When the Application Exit
            if (Name == "")
            {
                ii = ii + 1;
            }
            else
            {
                ii = 0;
            }
            if (ii >= 10)
            {
                break;
            }

            #endregion

            if (Check(Name.Trim().ToUpper()))
            {
                #region  If Order_Number is Null  Then Create a Foxconn internal Order_Number
                if (Name.Trim().ToUpper() == "ORDER_NUMBER" && Value == "")
                {
                    Value = ReturnOrderNum(DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
                #endregion
                FiledName = FiledName + Name + ",";
                FiledValue = FiledValue + "'" + Value + "'" + ",";
                if (!flag)
                {
                    break;
                }
            }

        }

        FiledName = FiledName.Substring(0, FiledName.Length - 1);
        FiledValue = FiledValue.Substring(0, FiledValue.Length - 1);

        ImportData(FiledName, FiledValue);

    }
    private bool Check(string str)
    {
        DataSet ds = null;
        OracleConnection Oraconn = null;
        string tablenm;
        string StrSql;
        try
        {
            Oraconn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");
            Oraconn.Open();
            tablenm = "CDMA_MOTO_ORDERNO";
            StrSql = "select COLUMN_NAME from USER_TAB_COLUMNS a where A.TABLE_NAME=" + "'" + tablenm + "'" + "  AND A.COLUMN_NAME=" + "'" + str + "'";

            OracleCommand OraCmd = new OracleCommand();
            OracleDataAdapter OraAP = new OracleDataAdapter();
            ds = new DataSet();

            OraCmd.Connection = Oraconn;
            OraCmd.CommandText = StrSql;
            OraAP.SelectCommand = OraCmd;
            OraCmd.ExecuteNonQuery();
            OraAP.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write("<script>alter('" + ex.Message + "')</script>");
        }
        finally
        {
            Oraconn.Close();
        }

        if (ds.Tables[0].Rows.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }


    }

    private void Quit()
    {
        if (ObjBooks.Count > 0)
        {
            foreach (Excel9.Workbook wb in ObjBooks)
            {
                if (wb == ObjBook)
                {
                    wb.Close(miss, tbPath.FileName, miss);
                }
            }
        }

        if (ObjBooks.Count == 0)
        {
            ObjApp.Quit();
        }

    }



    private void ImportData(string Str1, string Str2)
    {
        OracleConnection Oraconn = null;
        try
        {
            Oraconn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");
            Oraconn.Open();
            string tablenm;
            string StrSql;
            string StrHaiPn = Str1;
            string StrLoaction = Str2;
            tablenm = "CDMA_MOTO_ORDERNO";

            StrSql = "insert into " + tablenm + "(" + Str1 + ") values (" + Str2 + ")";
            OracleCommand myCommand2 = new OracleCommand(StrSql, Oraconn);
            myCommand2.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alter('" + ex.Message + "')</script>");
        }
        finally
        {
            Oraconn.Close();
        }

    }

    private void BeforImport()
    {
        //OracleConnection Oraconn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=mcm");
        //Oraconn.Open();
        //string tablenm;
        //string StrSql;
        //tablenm = "mcm_smt_programbom";
        //StrSql = "delete from " + tablenm + " where documentno =" + "'" + tbPn.Text.ToUpper().Trim() + "'";
        //OracleCommand myCommand1 = new OracleCommand(StrSql, Oraconn);
        //myCommand1.ExecuteNonQuery();
    }

    private string ReturnOrderNum(string in_Num)
    {
        //string in_Num = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ss = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        long nLen = ss.Length;
        string out_Num = "";
        long tempNum = Convert.ToInt64(in_Num);
        do
        {
            out_Num = ss.Substring(Convert.ToInt16(tempNum % nLen), 1) + out_Num;
            tempNum /= nLen;
        } while (tempNum >= nLen);
        out_Num = tempNum.ToString() + out_Num;
        return out_Num;
    }
}
