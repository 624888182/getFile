using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBAccess.EAI;
using DB.EAI;
using System.Reflection;
using Excelc = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Text;
using System.IO;
using System.Xml;
using UsingClass;
using System.Web.UI.WebControls;

public partial class Boundary_NexTestCPKquery : System.Web.UI.UserControl
{
    System.Data.DataTable Table = null;
    UsingClass.ComUtility cm = new ComUtility();
    //OracleConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
            InitiaPage();
            MultiLanguage();
        }
    }

    /// <summary>
    /// Load data from database to DropDownList contain Model and Station
    /// </summary>
    private void InitiaPage()
    {
        string StrSql = "SELECT MODEL FROM CMCS_SFC_MODEL WHERE ID <> -1 AND (CUSTOMER_TYPE = 'CDMA' or CUSTOMER_TYPE = 'NTGSM') ORDER BY MODEL";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();

        string strsql = "SELECT T.STATION_ID,T.DESCRIPTION FROM CDMA_STATION_DESCRIPTION T WHERE TESTSTATION = 'Y' ORDER BY DESCRIPTION";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        ddlStation.DataTextField = "DESCRIPTION";
        ddlStation.DataValueField = "STATION_ID";
        ddlStation.DataSource = dt1.DefaultView;
        ddlStation.DataBind();
    }

    /// <summary>
    /// Change the english and chinese
    /// </summary>
    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        lblStation.Text = (String)GetGlobalResourceObject("SFCQuery", "StationID");
        lblItem.Text = (String)GetGlobalResourceObject("SFCQuery", "Items");
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");
        Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");

        dgTestStationData.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Items");
        dgTestStationData.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Lo_Limit");
        dgTestStationData.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Up_Limit");
        dgTestStationData.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Max");
        dgTestStationData.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Min");
        dgTestStationData.Columns[5].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Average");
        dgTestStationData.Columns[6].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Tetval_Sample");
        dgTestStationData.Columns[7].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Tetval_CP");
        dgTestStationData.Columns[8].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Tetval_CPK");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        dgTestStationData.CurrentPageIndex = 0;
        string strmodel = ddlModel.SelectedValue.ToString().ToUpper();
        Label28.Visible = false;
        Label29.Visible = false;

        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            Label28.Visible = true;
            Label29.Visible = false;
            return;
        }

        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            Label28.Visible = false;
            Label29.Visible = true;
            return;
        }

        System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
        if (intday.TotalDays > 1&&strmodel!="RUY")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於24小時！');</script>");
            return;
        }
        if (intday.TotalDays <= 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('結束時間必須大于開始時間！');</script>");
            return;
        }

        Panel1.Visible = true;
        Panel2.Visible = false;
        this.bindcpk();
    }

    private string GetStationName()
    {
        string strStationID = ddlStation.SelectedValue.ToString();
        string strTableName = "";
        switch (strStationID)
        {
            case "PW":
                strTableName = "POWERON";
                break;
            case "FL":
                strTableName = "FLASHING";
                break;
            case "BT":
                strTableName = "BLUETEST";
                break;
            case "BD":
                strTableName = "BOARD";
                break;
            case "PH":
                strTableName = "PHASING";
                break;
            case "PRT":
                strTableName = "PROTO";
                break;
            case "CI":
                strTableName = "CIT";
                break;
            case "BL":
                strTableName = "BLUETOOTH";
                break;
            case "HT":
                strTableName = "HOTTHERM";
                break;
            case "CT":
                strTableName = "CLDTHERM";
                break;
            case "CM":
                strTableName = "CAMERA";
                break;
            case "FC":
                strTableName = "FOCUS";
                break;
            case "AD":
                strTableName = "AUDIO";
                break;
            case "PK":
                strTableName = "PACK";
                break;
            case "RA":
                strTableName = "RADIATED";
                break;
            case "QA":
                strTableName = "FQA";
                break;
            case "WT":
                strTableName = "WTM";
                break;
            case "CF":
                strTableName = "CFC";
                break;
            case "WLA":
                strTableName = "WLAN";
                break;
            case "GP":
                strTableName = "GPS";
                break;
            case "EV":
                strTableName = "EV";
                break;
            default:
                strTableName = "";
                break;
        }
        return strTableName;
    }

    private DataTable CPKTable()
    { 
        string sqllimit = "";
        string strStationName = GetStationName();
        string strModel = ddlModel.SelectedValue.ToString();
        string strStationCode = tbItem.Text.Trim().ToUpper();
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        ArrayList values = null;
        float max = 0.0f, min = 0.0f, sigma = 0.0f, avg = 0.0f;
        double limitu = 0.00, limitl = 0.00;
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        //conn = new OracleConnection(connectionString);
        //conn.Open();

        Table = new DataTable();
        Table.Columns.Add("Items");
        Table.Columns.Add("Lo_Limit");
        Table.Columns.Add("Up_Limit");
        Table.Columns.Add("Max");
        Table.Columns.Add("Min");
        Table.Columns.Add("Average");
        Table.Columns.Add("Tetval_Sample");
        Table.Columns.Add("Tetval_CP");
        Table.Columns.Add("Tetval_CPK");
        try
        {
            //if (!tbItem.Text.ToUpper().Equals(""))                                                  
            //{
            //    //require += "AND TEST_CODE=" + ClsCommon.GetSqlString(tbItem.Text.ToUpper());
            //    //require += " AND TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT ";
            //    //sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE " + require.Substring(3);

            //    //-----------max(test_date)
            //    //require += " AND TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND TEST_DATE=(SELECT MAX(TEST_DATE) FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr T WHERE T.TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND T.TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND S.TRACK_ID=T.TRACK_ID  AND T.TEST_CODE=S.TEST_CODE)  GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT order by TEST_CODE";
            //    //sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE " + require.Substring(3);         

            //    //-------------rownum()
            //    //require += " AND TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')) WHERE RN=1 GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT order by TEST_CODE";
            //    //sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM (select TEST_CODE,UPPER_LIMIT,LOWER_LIMIT,ROW_NUMBER() OVER(PARTITION BY TRACK_ID,TEST_CODE ORDER BY TEST_DATE DESC) RN FROM " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE " + require.Substring(3);
            //}
            //else
            //{
            //    //require += "AND TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT ";
            //    //sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE " + require.Substring(3);

            //    //-----------max(test_date)
            //    //require += "AND TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND TEST_DATE=(SELECT MAX(TEST_DATE) FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr T WHERE T.TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND T.TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND S.TRACK_ID=T.TRACK_ID AND  T.TEST_CODE=S.TEST_CODE)  GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT order by TEST_CODE";
            //    //sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE " + require.Substring(3);

            //    //-------------rownum()
            //    //require += "AND TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')) WHERE RN=1 GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT order by TEST_CODE ";
            //    //sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM (select TEST_CODE,UPPER_LIMIT,LOWER_LIMIT,ROW_NUMBER() OVER(PARTITION BY TRACK_ID,TEST_CODE ORDER BY TEST_DATE DESC) RN FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE " + require.Substring(3);
            //}

            if (!tbItem.Text.ToUpper().Equals(""))
                sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr  WHERE TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND TEST_CODE=" + ClsCommon.GetSqlString(tbItem.Text.ToUpper()) + " GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT ";
            else
                sqllimit = "select TEST_CODE TESTCONTENT,UPPER_LIMIT LIMITU,LOWER_LIMIT LIMITL,COUNT(*) SAMPLE FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr  WHERE TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  GROUP BY TEST_CODE,UPPER_LIMIT,LOWER_LIMIT ";

            DataTable limittable = ClsGlobal.objDataConnect.DataQuery(sqllimit).Tables[0];

            //OracleParameter[] orapara = new OracleParameter[] {new OracleParameter("MODEL",OracleType.VarChar,5),
            //                                                    new OracleParameter("STARTDATE",OracleType.VarChar,20),
            //                                                    new OracleParameter("ENDDATE",OracleType.VarChar,20),
            //                                                    new OracleParameter("STATIONID",OracleType.VarChar,20),
            //                                                    new OracleParameter("TESTCODE",OracleType.VarChar,50), 
            //                                                    new OracleParameter("DATA",OracleType.Cursor)};
            //orapara[0].Value = strModel;
            //orapara[1].Value = strStartDate;
            //orapara[2].Value = strEndDate;
            //orapara[3].Value = strStationName;
            //orapara[4].Value = strStationCode;
            //orapara[5].Direction = ParameterDirection.Output;
            //DataTable limittable = ClsGlobal.objDataConnect.DataQuery("SFCQUERY.GETNEXTESTCPKITEMS", orapara).Tables[0];

            if (limittable == null)
                return null;
            else
            {
                DataRow row = null;
                for (int i = 0; i < limittable.Rows.Count; i++)
                {
                    //int intcount = Convert.ToInt32(limittable.Rows[i]["SAMPLE"].ToString());
                    int intcount = Convert.ToInt32(limittable.Rows[i]["SAMPLE"]);
                    if (intcount >= 100)
                    {
                        //string strTestContent = limittable.Rows[i]["TESTCONTENT"].ToString(); 
                        string sqlsum = "select TEST_VALUE FROM " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr  WHERE TEST_DATE >=TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND TEST_CODE='" + limittable.Rows[i]["TESTCONTENT"].ToString() + "'";

                        //-----------max(test_date)
                        //sqlsum = "select TEST_VALUE FROM " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE TEST_DATE >=TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND TEST_DATE=(SELECT MAX(TEST_DATE) FROM  " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr  T WHERE  S.TRACK_ID=T.TRACK_ID AND  T.TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND T.TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  AND T.TEST_CODE='" + limittable.Rows[i]["TESTCONTENT"].ToString() + "') AND S.TEST_CODE='" + limittable.Rows[i]["TESTCONTENT"].ToString() + "'";

                        //-------------rownum()
                        //sqlsum = "SELECT TEST_VALUE FROM (select TEST_VALUE,ROW_NUMBER() OVER (PARTITION BY TRACK_ID ORDER BY TEST_DATE desc ) RN  from " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE  TEST_DATE >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI:SS') AND TEST_DATE <= TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI:SS') AND S.TEST_CODE='" + limittable.Rows[i]["TESTCONTENT"].ToString() + "') WHERE RN=1";

                        //try
                        //{
                        //    OracleDataReader orr = null;
                        //    OracleCommand cmd = new OracleCommand(sqlsum, conn);
                        //    cmd.CommandType = CommandType.Text;
                        //    orr = cmd.ExecuteReader();

                        //    if (orr == null)
                        //        return null;
                        //    else
                        //    {
                        //        values = new ArrayList();
                        //        while (orr.Read())
                        //        {
                        //            float t = 0.00f;
                        //            t = float.Parse(orr[0].ToString()); 
                        //            values.Add(t);
                        //        }
                        //        orr.Close();

                        //        ComUtility com = new ComUtility(values);
                        //        max = com.CalMax();
                        //        min = com.CalMin();
                        //        sigma = com.CalSig();
                        //        avg = com.CalAvg();
                        //        row = Table.NewRow();
                        //        row["Items"] = limittable.Rows[i]["TESTCONTENT"];
                        //        row["Lo_Limit"] = limittable.Rows[i]["LIMITL"];
                        //        row["Up_Limit"] = limittable.Rows[i]["LIMITU"];
                        //        row["Min"] = min;
                        //        row["Average"] = avg;
                        //        row["Tetval_Sample"] = limittable.Rows[i]["SAMPLE"];
                        //        row["Max"] = max;
                        //        limitu = Convert.ToDouble(limittable.Rows[i]["LIMITU"]);
                        //        limitl = Convert.ToDouble(limittable.Rows[i]["LIMITL"]);

                        //        if (sigma <= 0)
                        //        {
                        //            row["Tetval_CP"] = 0;
                        //            row["Tetval_CPK"] = 0;
                        //        }
                        //        else
                        //        {
                        //            row["Tetval_CP"] = (limitu - limitl) / (6 * sigma);
                        //            row["Tetval_CPK"] = com.CalCPK(limitu, limitl, sigma, avg);
                        //        }
                        //        Table.Rows.Add(row);
                        //    }
                        //}
                       try
                       {
                           DataTable dt = ClsGlobal.objDataConnect.DataQuery(sqlsum).Tables[0];   

                           if(dt.Rows.Count<=0) 
                               return null;
                           else
                           {
                               try 
                               {                                
                                   values = new ArrayList();
                                   for (int  m= 0;  m< dt.Rows.Count; m++)
                                   {
                                       float t = 0.00f;
                                       //t = float.Parse(dt.Rows[i]["TEST_VALUE"].ToString());
                                       t = float.Parse(dt.Rows[m]["TEST_VALUE"].ToString());
                                       values.Add(t);
                                   } 

                                   ComUtility com = new ComUtility(values);
                                   max = com.CalMax();
                                   min = com.CalMin();
                                   sigma = com.CalSig();
                                   avg = com.CalAvg();
                                   row = Table.NewRow();
                                   row["Items"] = limittable.Rows[i]["TESTCONTENT"];
                                   row["Lo_Limit"] = limittable.Rows[i]["LIMITL"];
                                   row["Up_Limit"] = limittable.Rows[i]["LIMITU"];
                                   row["Min"] = min;
                                   row["Average"] = avg;
                                   row["Tetval_Sample"] = limittable.Rows[i]["SAMPLE"];
                                   row["Max"] = max;
                                   limitu = Convert.ToDouble(limittable.Rows[i]["LIMITU"]);
                                   limitl = Convert.ToDouble(limittable.Rows[i]["LIMITL"]);

                                   if (sigma <= 0)
                                   {
                                       row["Tetval_CP"] = 0;
                                       row["Tetval_CPK"] = 0;
                                   }
                                   else
                                   {
                                       row["Tetval_CP"] = (limitu - limitl) / (6 * sigma);
                                       row["Tetval_CPK"] = com.CalCPK(limitu, limitl, sigma, avg);
                                   }
                                   Table.Rows.Add(row);
                               }
                               catch (Exception e1)
                               {
                                   string k = e1.Message;
                                   return null;
                               }
                           }

                       }
                        catch (Exception e)
                        {
                            string k = e.Message;
                            return null;
                        }
                    }
                }
            }
        }
        catch (Exception err)
        {
            string k = err.Message;
            return null;
        }
        //finally
        //{
        //    conn.Close();
        //}
        return Table;
    }

    private void bindcpk()
    {
        DataTable Table = CPKTable();
        if (Table != null)
        {
            System.Data.DataView source = null;
            source = new DataView(Table);

            dgTestStationData.DataSource = source;
            dgTestStationData.DataBind();
            Label4.Text = " Total:" + Table.Rows.Count.ToString();
            //Table.Dispose();
            //GC.Collect();
        }
        else
        {
            string nodata = (String)GetGlobalResourceObject("SFCQuery", "NoData");
            Response.Write("<script language=javascript>alert('" + nodata + "')</script>");
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = CPKTable();
        ExortToExcel(dt);
    }

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excelc.ApplicationClass objExcel = null;
        Excelc.Workbooks objBooks = null;
        Excelc.Workbook objBook = null;
        Excelc.Worksheet objSheet = null;
        try
        {
            objExcel = new Excelc.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excelc.Workbooks)objExcel.Workbooks;
            objBook = (Excelc.Workbook)(objBooks.Add(missing));
            objSheet = (Excelc.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excelc.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
            objBook.Close(false, missing, missing);
            objBooks.Close();
            objExcel.Quit();
        }
        finally
        {
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
            if (!objSheet.Equals(null))
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
            if (objBook != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
            if (objBooks != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
            if (objExcel != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
            GC.Collect();
        }
        //保存或打開報表
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
        Response.Charset = "";
        this.EnableViewState = false;
        Response.WriteFile(ExportPath + strFileName);
        Response.End();
    }

    private void ExortToExcel(DataTable dt)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing miss = Missing.Value;

        Excelc.ApplicationClass objExcel = null;
        Excelc.Workbooks objBooks = null;
        Excelc.Workbook objBook = null;
        Excelc.Worksheet objSheet = null;

        try
        {
            objExcel = new Excelc.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excelc.Workbooks)objExcel.Workbooks;
            objBook = (Excelc.Workbook)(objBooks.Add(miss));
            objSheet = (Excelc.Worksheet)objBook.ActiveSheet;
            objSheet.Name = ddlModel.SelectedValue + "_" + ddlStation.SelectedValue;

            int intColumn = dt.Columns.Count;
            for (int i = 1; i <= intColumn; i++)
            {
                //SetRangeValue(objSheet,GetCellName(i,1),GetCellName(i,1),dt.Columns[i].ColumnName,true,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                objSheet.Cells[1, i] = dt.Columns[i - 1].ColumnName;
            }

            int RowID = 2;

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 1; i <= intColumn; i++)
                {
                    //SetRangeValue(objSheet,GetCellName(i,RowID),GetCellName(i,RowID),row[i].ToString(),false,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                    objSheet.Cells[RowID, i] = row[i - 1].ToString();
                }
                RowID++;
            }
            //

            objSheet.Columns.AutoFit();
            objSheet.Rows.AutoFit();

            //頁面設置
            try
            {
                objSheet.PageSetup.LeftMargin = 20;
                objSheet.PageSetup.RightMargin = 20;
                objSheet.PageSetup.TopMargin = 35;
                objSheet.PageSetup.BottomMargin = 15;
                objSheet.PageSetup.HeaderMargin = 7;
                objSheet.PageSetup.FooterMargin = 10;
                objSheet.PageSetup.CenterHorizontally = true;
                objSheet.PageSetup.CenterVertically = false;
                objSheet.PageSetup.Orientation = Excelc.XlPageOrientation.xlPortrait;
                objSheet.PageSetup.PaperSize = Excelc.XlPaperSize.xlPaperA4;
                objSheet.PageSetup.Zoom = false;
                objSheet.PageSetup.FitToPagesWide = 1;
                objSheet.PageSetup.FitToPagesTall = false;
            }
            catch
            {
                throw;
            }
            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, miss, miss, miss, miss, miss, Excelc.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
            objBook.Close(false, miss, miss);
            objBooks.Close();
            objExcel.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
        }
        catch
        {
            throw;
        }
        finally
        {
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
            if (!objSheet.Equals(null))
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
            if (objBook != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
            if (objBooks != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
            if (objExcel != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
            GC.Collect();
        }

        //保存或打開報表
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
        Response.Charset = "";
        this.EnableViewState = false;
        Response.WriteFile(ExportPath + strFileName);
        Response.End();
    }

    protected void dgTestStationData_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Sample")
        {
            string strModel = ddlModel.SelectedValue.ToUpper();
            string strStation = ddlStation.SelectedValue.ToUpper();
            string strStationName = GetStationName();
            string strStartDate = tbStartDate.DateTextBox.Text.Trim();
            string strEndDate = tbEndDate.DateTextBox.Text.Trim();
            string strItems = ((Label)(e.Item.Cells[0].Controls[1])).Text;
            string strLowLimit = ((Label)(e.Item.Cells[1].Controls[1])).Text;
            string strUpLimit = ((Label)(e.Item.Cells[2].Controls[1])).Text;

            string strScript = "<script language='jscript'>var res = window.open('./WFrmModalDialog.aspx?Url=./WFrmCPKInfoQuery.ascx&Model=" + strModel + "&StationName=" + strStationName + "&StartDate=" + strStartDate + "&EndDate=" + strEndDate + "&Items=" + strItems + "&UpLimit=" + strUpLimit + "&LowLimit=" + strLowLimit + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
}
