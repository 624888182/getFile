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
using Excelf = Microsoft.Office.Interop.Excel; 
using System.Drawing;   
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Text;
using System.IO;
using System.Xml;
using UsingClass;
using System.Web.UI.WebControls; 

public partial class Boundary_WFrmCDMATestDetailQuery : System.Web.UI.UserControl
{
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
        //string StrSql = " SELECT distinct MODEL FROM CMCS_SFC_MODEL WHERE ID <> -1 AND CUSTOMER_TYPE in ('CDMA','NTGSM') ORDER BY MODEL ";
        string StrSql = "SELECT distinct db_user MODEL FROM CMCS_SFC_MODEL WHERE ID <> -1 ORDER BY MODEL";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();

        string strProcedureName = "SFCQUERY.GETSTATION";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
        orapara[0].Direction = ParameterDirection.Output;
        dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlStation.DataTextField = "DESCRIPTION";
        //ddlStation.DataTextField = "STATION_ID";
        ddlStation.DataValueField = "STATION_ID";
        ddlStation.DataSource = dt.DefaultView;
        ddlStation.DataBind();
    }

    /// <summary>
    /// Change the english and chinese
    /// </summary>
    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        lblStation.Text = (String)GetGlobalResourceObject("SFCQuery", "StationID");
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");
        Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
    }

    private string GetSql()
    {
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strSql = "SELECT * FROM " + ddlModel.SelectedValue + "." + ddlModel.SelectedValue + "_ALL_DETAIL@nextestsvr WHERE ";
        string strWhere = "";
        if (ddlStation.SelectedIndex >= 0)
            strWhere = strWhere + "AND STATION_CODE = " + ClsCommon.GetSqlString(ddlStation.SelectedValue.ToUpper());

        
        if (!txtPID.Text.Trim().Equals("") || !txtErrorCode.Text.Trim().Equals(""))
        {
            if (!txtPID.Text.Trim().Equals(""))
                strWhere = strWhere + " AND TRACK_ID = " + ClsCommon.GetSqlString(txtPID.Text.Trim().ToUpper());

            if (!txtErrorCode.Text.Trim().Equals(""))
                strWhere = strWhere + " AND upper(TEST_CODE)= " + ClsCommon.GetSqlString(txtErrorCode.Text.Trim().ToUpper());
     
            if (!tbStartDate.DateTextBox.Text.Equals("") && !tbEndDate.DateTextBox.Text.Equals(""))
            {
                if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
                {
                    Label28.Visible = true;
                    Label29.Visible = false;
                }

                if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
                {
                    Label28.Visible = false;
                    Label29.Visible = true;
                }
                strWhere = strWhere + " AND TEST_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";
            } 

            if (ddlPorF.SelectedIndex >= 0)
                strWhere = strWhere + "AND SUBSTR(TEST_RESULT,1,1) = " + ClsCommon.GetSqlString(ddlPorF.SelectedValue.ToUpper().Substring(0, 1));
        }
        else
        {
            if (!txtPID.Text.Trim().Equals("") || !txtErrorCode.Text.Trim().Equals(""))
            {
                if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
                {
                    Label28.Visible = true;
                    Label29.Visible = false;
                }

                if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
                {
                    Label28.Visible = false;
                    Label29.Visible = true;
                }
            }            
            strWhere = strWhere + " AND TEST_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";
            if (ddlPorF.SelectedIndex >= 0)
                strWhere = strWhere + "AND SUBSTR(TEST_RESULT,1,1) = " + ClsCommon.GetSqlString(ddlPorF.SelectedValue.ToUpper().Substring(0, 1));

        }
        
       strSql = strSql + strWhere.Substring(3);
        return strSql;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgTestStationData.CurrentPageIndex = 0;    

        Label28.Visible = false;
        Label29.Visible = false;

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetSql(), DBKey.CDMA).Tables[0];
        dgTestStationData.DataSource = dt.DefaultView;
        dgTestStationData.DataBind();

        Label4.Text = "Current Page:" + (dgTestStationData.CurrentPageIndex + 1).ToString() + "/" + dgTestStationData.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string strSql = GetSql();
        ExortToExcel1(strSql);
    }
    protected void dgTestStationData_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgTestStationData.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetSql(), DBKey.CDMA).Tables[0];
        dgTestStationData.DataSource = dt.DefaultView;
        dgTestStationData.DataBind();
        Label4.Text = "Current Page:" + (dgTestStationData.CurrentPageIndex + 1).ToString() + "/" + dgTestStationData.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excelf.ApplicationClass objExcel = null;
        Excelf.Workbooks objBooks = null;
        Excelf.Workbook objBook = null;
        Excelf.Worksheet objSheet = null;
        try
        {
            objExcel = new Excelf.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excelf.Workbooks)objExcel.Workbooks;
            objBook = (Excelf.Workbook)(objBooks.Add(missing));
            objSheet = (Excelf.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excelf.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
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
}
