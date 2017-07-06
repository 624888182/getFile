using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Data.OracleClient;
using Excel11 = Microsoft.Office.Interop.Excel;

public partial class Boundary_wfrmwipstatusquery : System.Web.UI.UserControl
{
    protected System.Web.UI.HtmlControls.HtmlTable TABLE1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModel();
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text; 
        }
    }

    private void MultiLanguage()
    {
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo"); 
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model"); 
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
        ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        ViewState["WONotExist"] = (String)GetGlobalResourceObject("SFCQuery", "WONotExist");
    }

    private void BindModel()
    {
        string strProcedureName = "SFCQUERY.GETMODELNAME";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
        orapara[0].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind(); 
        ddlModel.Items.Insert(0, "ALL");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            Label28.Text = ViewState["ErrorDate"].ToString();
            Label28.Visible = true;
            Label29.Visible = false;
            return;
        }
        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            Label29.Text = ViewState["ErrorDate"].ToString();
            Label29.Visible = true;
            Label28.Visible = false;
            return;
        }

        Label28.Visible = false;
        Label29.Visible = false;
        string StrSql = "";
        if(!ddlModel.SelectedValue.Equals("ALL"))
            StrSql = QueryByModel(ddlModel.SelectedValue, tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
        else
            StrSql = QueryByTime(tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim()); 
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        BuilderHtmlTableTitleClicking(dt);
        FullTableData(dt);
    }

    private string QueryByModel(string Model, string DateStart, string DateEnd)
    {
        string StrSql = "";
        StrSql = " SELECT MODEL,PART,WO,WOQTY,"
            + " SUM(DECODE(STATION_ID,'S_IA',DECODE(STATE_ID, 'P',QTY,0),'A_IN',DECODE(STATE_ID, 'P',QTY,0),0)) ASIN_I,"
            + " SUM(DECODE(STATION_ID,'S_IB',DECODE(STATE_ID, 'P',QTY,0),'B_IN',DECODE(STATE_ID, 'P',QTY,0),0)) ASIN_O,"
            + " SUM(DECODE(STATION_ID,'S_IB',DECODE(STATE_ID, 'P',QTY,0),'B_IN',DECODE(STATE_ID, 'P',QTY,0),0)) BSIN_I,"
            + " SUM(DECODE(STATION_ID,'S_LK',DECODE(STATE_ID, 'P',QTY,0),'S_NK',DECODE(STATE_ID, 'P',QTY,0),0)) BSIN_O,"
            + " SUM(DECODE(STATION_ID,'S_LK',DECODE(STATE_ID, 'P',QTY,0),'S_NK',DECODE(STATE_ID, 'P',QTY,0),0)) TEST_I,"
            + " SUM(DECODE(STATION_ID,'A_BI',DECODE(STATE_ID, 'P',QTY,0),'A_NP',DECODE(STATE_ID, 'P',QTY,0),0)) TEST_O,"
            + " SUM(DECODE(STATION_ID,'A_BI',DECODE(STATE_ID, 'P',QTY,0),'A_NP',DECODE(STATE_ID, 'P',QTY,0),0)) ASSY_I,"
            + " SUM(DECODE(STATION_ID,'A_FO',DECODE(STATE_ID, 'P',QTY,0),'F_C',DECODE(STATE_ID, 'P',QTY,0),0)) ASSY_O,"
            + " SUM(DECODE(STATION_ID,'A_FO',DECODE(STATE_ID, 'P',QTY,0),'F_C',DECODE(STATE_ID, 'P',QTY,0),0)) PACK_I,"
            + " SUM(DECODE(STATION_ID,'OQC',DECODE(STATE_ID, 'P',QTY,0),0)) PACK_O,"
            + " SUM(DECODE(STATION_ID,'OQC',DECODE(STATE_ID, 'P',QTY,0),0)) WH_I,"
            + " SUM(DECODE(STATION_ID,'3S',DECODE(STATE_ID, 'P',QTY,0),0)) WH_O,"
            + " SUM(DECODE(STATION_ID,'ARE',DECODE(STATE_ID, 'I',QTY,0),0)) REPAIR_I,"
            + " SUM(DECODE(STATION_ID,'ARE',DECODE(STATE_ID, 'O',QTY,0),0)) REPAIR_O "
            + " FROM ("
            + " SELECT MODEL,PART,WO,WOQTY,STATION_ID,STATE_ID,COUNT(*) QTY"
            + " FROM ("
            + " SELECT d.model,D.SPART PART,WO_NO WO,PID_QTY WOQTY,C.PRODUCT_ID PID,SUBSTR(A.STATION_ID,1,1)||'_'||SUBSTR(A.STATION_ID,3,2) STATION_ID,A.STATE_ID"
            + " FROM Sfc.MES_PCBA_PANEL_HISTORY  A ,SFC.MES_PCBA_PANEL_DETAIL B, Sfc.MES_PCBA_PANEL_LINK  C ,SHP.CMCS_SFC_SORDER D"
            + " WHERE  A.CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi') "
            + " AND SUBSTR(A.PANEL_ID,2,20)= C.PANEL_ID AND d.model =" + ClsCommon.GetSqlString(Model)
            + " AND B.PANEL_ID=C.PANEL_ID AND  B.WO_NO=D.SORDER "
            + " UNION"
            + " SELECT  SUBSTR(D.APART,3,3) MODEL, D.APART PART,AORDER WO,APID_QTY WOQTY,A.PRODUCT_ID PID,SUBSTR(A.STATION_ID,1,1)||'_'||SUBSTR(A.STATION_ID,3,2) STATION_ID,A.STATE_ID"
            + " FROM SFC.MES_ASSY_HISTORY a,SHP.CMCS_SFC_AORDER D"
            + " where A.CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " AND SUBSTR(D.APART,3,3)=" + ClsCommon.GetSqlString(Model)
            + " and a.wo_no=d.AORDER"
            + " UNION"
            + " SELECT SUBSTR(C.PPART,3,3) MODEL, C.PPART PART,C.PORDER WO,PPID_QTY WOQTY, A.PRODUCT_ID PID,'OQC' STATION_ID,DECODE(A.DEFECT_CODE,'','P','F') STATE_ID"
            + " FROM SFC.MES_PACK_OQC A,SHP.CMCS_SFC_CARTON B,SHP.CMCS_SFC_PORDER C"
            + " WHERE A.CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " AND A.CARTON_NO=B.CARTON_NO AND B.PORDER=C.PORDER"
            + " AND SUBSTR(C.PPART,3,3)=" + ClsCommon.GetSqlString(Model)
            + " UNION"
            + " SELECT substr(product_id,1,3) MODEL,' ' PART,' ' WO,null WOQTY,PRODUCT_ID PID,'ARE' STATION_ID,DECODE(station_id,'ARE_IN','I','ARE_OUT','O',STATE_ID) STATE_ID"
            + " FROM SFC.MES_PCBA_HISTORY "
            + " Where  CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " and substr(product_id,1,3)=" + ClsCommon.GetSqlString(Model)
            + " AND (STATION_ID ='ARE' OR STATION_ID ='ARE_IN' OR STATION_ID ='ARE_OUT')"
            + " UNION"
            + " SELECT SUBSTR(MODEL,1,3) MODEL,C.PPART PART,A.WORK_ORDER WO,C.PPID_QTY WOQTY,A.PRODUCTID PID,'3S' STATION_ID,'P' STATE_ID"
            + " FROM SHP.CMCS_SFC_SHIPPING_DATA A,SHP.CMCS_SFC_SHIP_CARTON_MAP B,SHP.CMCS_SFC_PORDER C"
            + " WHERE DDATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " AND SUBSTR(MODEL,1,3)=" + ClsCommon.GetSqlString(Model)
            + " AND A.CARTON_NO=B.ORDER_CARTON_NO AND A.WORK_ORDER=C.PORDER"
            + " )GROUP BY MODEL,PART,WO,WOQTY,STATION_ID,STATE_ID"
            + " )GROUP BY MODEL,PART,WO,WOQTY";
        return StrSql;
    }

    private string QueryByTime( string DateStart, string DateEnd)
    {
        string StrSql = "";
        StrSql = " SELECT MODEL,PART,WO,WOQTY,"
            + " SUM(DECODE(STATION_ID,'S_IA',DECODE(STATE_ID, 'P',QTY,0),'A_IN',DECODE(STATE_ID, 'P',QTY,0),0)) ASIN_I,"
            + " SUM(DECODE(STATION_ID,'S_IB',DECODE(STATE_ID, 'P',QTY,0),'B_IN',DECODE(STATE_ID, 'P',QTY,0),0)) ASIN_O,"
            + " SUM(DECODE(STATION_ID,'S_IB',DECODE(STATE_ID, 'P',QTY,0),'B_IN',DECODE(STATE_ID, 'P',QTY,0),0)) BSIN_I,"
            + " SUM(DECODE(STATION_ID,'S_LK',DECODE(STATE_ID, 'P',QTY,0),'S_NK',DECODE(STATE_ID, 'P',QTY,0),0)) BSIN_O,"
            + " SUM(DECODE(STATION_ID,'S_LK',DECODE(STATE_ID, 'P',QTY,0),'S_NK',DECODE(STATE_ID, 'P',QTY,0),0)) TEST_I,"
            + " SUM(DECODE(STATION_ID,'A_BI',DECODE(STATE_ID, 'P',QTY,0),'A_NP',DECODE(STATE_ID, 'P',QTY,0),0)) TEST_O,"
            + " SUM(DECODE(STATION_ID,'A_BI',DECODE(STATE_ID, 'P',QTY,0),'A_NP',DECODE(STATE_ID, 'P',QTY,0),0)) ASSY_I,"
            + " SUM(DECODE(STATION_ID,'A_FO',DECODE(STATE_ID, 'P',QTY,0),'F_C',DECODE(STATE_ID, 'P',QTY,0),0)) ASSY_O,"
            + " SUM(DECODE(STATION_ID,'A_FO',DECODE(STATE_ID, 'P',QTY,0),'F_C',DECODE(STATE_ID, 'P',QTY,0),0)) PACK_I,"
            + " SUM(DECODE(STATION_ID,'OQC',DECODE(STATE_ID, 'P',QTY,0),0)) PACK_O,"
            + " SUM(DECODE(STATION_ID,'OQC',DECODE(STATE_ID, 'P',QTY,0),0)) WH_I,"
            + " SUM(DECODE(STATION_ID,'3S',DECODE(STATE_ID, 'P',QTY,0),0)) WH_O,"
            + " SUM(DECODE(STATION_ID,'ARE',DECODE(STATE_ID, 'I',QTY,0),0)) REPAIR_I,"
            + " SUM(DECODE(STATION_ID,'ARE',DECODE(STATE_ID, 'O',QTY,0),0)) REPAIR_O "
            + " FROM ("
            + " SELECT MODEL,PART,WO,WOQTY,STATION_ID,STATE_ID,COUNT(*) QTY"
            + " FROM ("
            + " SELECT d.model,D.SPART PART,WO_NO WO,PID_QTY WOQTY,C.PRODUCT_ID PID,SUBSTR(A.STATION_ID,1,1)||'_'||SUBSTR(A.STATION_ID,3,2) STATION_ID,A.STATE_ID"
            + " FROM Sfc.MES_PCBA_PANEL_HISTORY  A ,SFC.MES_PCBA_PANEL_DETAIL B, Sfc.MES_PCBA_PANEL_LINK  C ,SHP.CMCS_SFC_SORDER D"
            + " WHERE  A.CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi') "
            + " AND SUBSTR(A.PANEL_ID,2,20)= C.PANEL_ID "
            + " AND B.PANEL_ID=C.PANEL_ID AND  B.WO_NO=D.SORDER "
            + " UNION"
            + " SELECT  SUBSTR(D.APART,3,3) MODEL, D.APART PART,AORDER WO,APID_QTY WOQTY,A.PRODUCT_ID PID,SUBSTR(A.STATION_ID,1,1)||'_'||SUBSTR(A.STATION_ID,3,2) STATION_ID,A.STATE_ID"
            + " FROM SFC.MES_ASSY_HISTORY a,SHP.CMCS_SFC_AORDER D"
            + " where A.CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " and a.wo_no=d.AORDER"
            + " UNION"
            + " SELECT SUBSTR(C.PPART,3,3) MODEL, C.PPART PART,C.PORDER WO,PPID_QTY WOQTY, A.PRODUCT_ID PID,'OQC' STATION_ID,DECODE(A.DEFECT_CODE,'','P','F') STATE_ID"
            + " FROM SFC.MES_PACK_OQC A,SHP.CMCS_SFC_CARTON B,SHP.CMCS_SFC_PORDER C"
            + " WHERE A.CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " AND A.CARTON_NO=B.CARTON_NO AND B.PORDER=C.PORDER" 
            + " UNION"
            + " SELECT substr(product_id,1,3) MODEL,'Repair' PART,'Repair' WO,0 WOQTY,PRODUCT_ID PID,'ARE' STATION_ID,DECODE(station_id,'ARE_IN','I','ARE_OUT','O',STATE_ID) STATE_ID"
            + " FROM SFC.MES_PCBA_HISTORY "
            + " Where  CREATION_DATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " AND (STATION_ID ='ARE' OR STATION_ID ='ARE_IN' OR STATION_ID ='ARE_OUT')"
            + " UNION"
            + " SELECT SUBSTR(MODEL,1,3) MODEL,C.PPART PART,A.WORK_ORDER WO,C.PPID_QTY WOQTY,A.PRODUCTID PID,'3S' STATION_ID,'P' STATE_ID"
            + " FROM SHP.CMCS_SFC_SHIPPING_DATA A,SHP.CMCS_SFC_SHIP_CARTON_MAP B,SHP.CMCS_SFC_PORDER C"
            + " WHERE DDATE  BETWEEN TO_DATE(" + ClsCommon.GetSqlString(DateStart) + ",'yyyy/mm/dd hh24:mi') AND TO_DATE(" + ClsCommon.GetSqlString(DateEnd) + ",'yyyy/mm/dd hh24:mi')"
            + " AND A.CARTON_NO=B.ORDER_CARTON_NO AND A.WORK_ORDER=C.PORDER"
            + " )GROUP BY MODEL,PART,WO,WOQTY,STATION_ID,STATE_ID"
            + " )GROUP BY MODEL,PART,WO,WOQTY"
            +" order by model";
        return StrSql;
    }

    private void BuilderHtmlTableTitleClicking(DataTable dtSet)
    {
        HtmlTableRow htr = new HtmlTableRow();
        HtmlTableRow htr1 = new HtmlTableRow();

        //model
        HtmlTableCell htc1 = new HtmlTableCell();
        htc1.InnerHtml = "<font color='White'><STRONG>Model</STRONG></font>";
        htc1.RowSpan = 2;
        htc1.BgColor = "#006699";
        htc1.Align = "Center";
        htr.Cells.Add(htc1);

        //part
        HtmlTableCell htc2 = new HtmlTableCell();
        htc2.InnerHtml = "<font color='White'><STRONG>Part</STRONG></font>";
        htc2.RowSpan = 2;
        htc2.BgColor = "#006699";
        htc2.Align = "Center";
        htr.Cells.Add(htc2);

        //wo
        HtmlTableCell htc3 = new HtmlTableCell();
        htc3.InnerHtml = "<font color='White'><STRONG>WO</STRONG></font>";
        htc3.RowSpan = 2;
        htc3.BgColor = "#006699";
        htc3.Align = "Center";
        htr.Cells.Add(htc3);

        //woqty
        HtmlTableCell htc4 = new HtmlTableCell();
        htc4.InnerHtml = "<font color='White'><STRONG>WO Qty</STRONG></font>";
        htc4.RowSpan = 2;
        htc4.BgColor = "#006699";
        htc4.Align = "Center";
        htr.Cells.Add(htc4);

        for (int i = 4; i <= dtSet.Columns.Count - 1; i++)
        {
            string strColumnName = dtSet.Columns[i].ColumnName;
            AddStationCell(htr, htr1, strColumnName, strColumnName.Substring(strColumnName.Length - 1, 1).ToUpper());
        }
        tbwip.Rows.Add(htr);
        tbwip.Rows.Add(htr1);
        htr.Dispose();
        htr1.Dispose();
    }

    private void FullTableData(DataTable dt)
    {
        HtmlTableRow htr = null;
        HtmlTableCell htc = null; 
        foreach (DataRow rw in dt.Rows)
        {
            htr = new HtmlTableRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                htc = new HtmlTableCell(); 
                if (i == 2)
                {
                    if (rw["WO"].ToString() == "Repair")
                        htc.InnerHtml = rw[i].ToString(); 
                    else
                        htc.InnerHtml = "<a href=\"/sfcquery/Boundary/wfrmwodetail.aspx?WO=" + rw["WO"].ToString() + "&qty=" + rw["WOQTY"].ToString() + "\" target=\"_blank\">" + rw[i].ToString() + "</a>";
                }
                else
                    htc.InnerHtml = rw[i].ToString();
                htc.Align = "Center";
                htr.Cells.Add(htc);
                htc.Dispose();
            }
            tbwip.Rows.Add(htr);
            htr.Dispose();
        }
    }

    private void AddStationCell(HtmlTableRow htr, HtmlTableRow htr1, string StationName, string strStatus)
    {
        string strstation = StationName.Substring(0, StationName.Length - 2).ToUpper();
        switch (StationName.Substring(0, StationName.Length - 2).ToUpper())
        {
            case "ASIN":
                strstation ="SMT A Input";
                break;
            case "BSIN":
                strstation ="SMT B Input";
                break;
            case "TEST":
                strstation ="Board Test";
                break;
            case "ASSY":
                strstation ="Assmbly";
                break;
            case "PACK":
                strstation ="Pack";
                break;
            case "WH":
                strstation ="Warehouse";
                break;
            case "REPAIR":
                strstation ="Repair";
                break; 
        } 

        HtmlTableCell htc = new HtmlTableCell();
        htc.InnerHtml = "<font color='White'><STRONG>" + strstation + "</STRONG></font>";
        htc.BgColor = "#006699";
        htc.Align = "Center";
        //htc.ColSpan = 2;
        htr.Cells.Add(htc);
        htc.Dispose();

        if (strStatus.Equals("I"))
        {
            htc = new HtmlTableCell();
            htc.InnerHtml = "<font color='White'><STRONG>Input</STRONG></font>";
            htc.BgColor = "#006699";
            htc.Align = "Center";
            htr1.Cells.Add(htc);
            htc.Dispose();
        }
        else
        {
            htc = new HtmlTableCell();
            htc.InnerHtml = "<font color='White'><STRONG>Output</STRONG></font>";
            htc.BgColor = "#006699";
            htc.Align = "Center";
            htr1.Cells.Add(htc);
            htc.Dispose();
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            Label28.Text = ViewState["ErrorDate"].ToString();
            Label28.Visible = true;
            Label29.Visible = false;
            return;
        }
        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            Label29.Text = ViewState["ErrorDate"].ToString();
            Label29.Visible = true;
            Label28.Visible = false;
            return;
        }

        Label28.Visible = false;
        Label29.Visible = false;

        GetDownLoadData();
    }

    private void GetDownLoadData()
    {
        //string test = DateTime.Now.Year.ToString()+"/"+ddlMonth.SelectedValue+"/"+DateTime.Now.Day.ToString();
        string strFileName = Request.PhysicalApplicationPath + @"Templet\InputOutput.xlt";
        string ExportPath = Request.PhysicalApplicationPath + @"Temp\";

        Missing MissingValue = Missing.Value;

        Excel11.ApplicationClass objExcel = null;
        Excel11.Workbook objBook = null;
        Excel11.Worksheet objSheet = null;
        try
        {
            objExcel = new Excel11.ApplicationClass();
            objExcel.Visible = true;

            objBook = objExcel.Workbooks.Open(strFileName.ToString(), MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue);
            objSheet = (Excel11.Worksheet)objBook.Worksheets[1];

            string strSql = "";
            if (!ddlModel.SelectedValue.Equals("ALL"))
                strSql = QueryByModel(ddlModel.SelectedValue, tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
            else
                strSql = QueryByTime(tbStartDate.DateTextBox.Text.Trim(), tbEndDate.DateTextBox.Text.Trim());
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

            WriteToExcel(objSheet, dt);

            strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            objBook.SaveAs(ExportPath + strFileName, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, Excel11.XlSaveAsAccessMode.xlExclusive, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue);
            objBook.Close(false, MissingValue, MissingValue);
            objExcel.Quit();
        }
        finally
        {
            if (!objSheet.Equals(null))
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
            if (objBook != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
            if (objExcel != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
            GC.Collect();
        }

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

    private void WriteToExcel(Excel11.Worksheet objSheet, DataTable dt)
    {
        //objSheet.Cells[3, 1] = "日期:" + Date;
        //objSheet.Cells[3, 3] = "生產線別:" + Line;
        //objSheet.Cells[3, 5] = "幾種名稱:" + Model;

        //objSheet.Cells[4, 2] = Lotqty;
        //objSheet.Cells[4, 4] = Totalqty;
        //objSheet.Cells[4, 7] = Faillotqty;

        //objSheet.Cells[5, 2] = Sampleqty;
        //objSheet.Cells[5, 4] = Failqty;
        //objSheet.Cells[5, 7] = Failrate;

        int i = 1;
        foreach (DataRow rw in dt.Rows)
        {
            objSheet.Cells[4 + i, 1] = rw["model"].ToString();
            objSheet.Cells[4 + i, 2] = rw["part"].ToString();
            objSheet.Cells[4 + i, 3] = rw["wo"].ToString();
            objSheet.Cells[4+ i, 4] = rw["woqty"].ToString();
            objSheet.Cells[4 + i, 5] = rw["asin_i"].ToString();
            objSheet.Cells[4+ i, 6] = rw["asin_o"].ToString();

            objSheet.Cells[4 + i, 7] = rw["bsin_i"].ToString();
            objSheet.Cells[4 + i, 8] = rw["bsin_o"].ToString();
            objSheet.Cells[4 + i, 9] = rw["test_i"].ToString();
            objSheet.Cells[4 + i, 10] = rw["test_o"].ToString();
            objSheet.Cells[4 + i, 11] = rw["assy_i"].ToString();
            objSheet.Cells[4 + i, 12] = rw["assy_o"].ToString();
            objSheet.Cells[4 + i, 13] = rw["pack_i"].ToString();
            objSheet.Cells[4 + i, 14] = rw["pack_o"].ToString();
            objSheet.Cells[4 + i, 15] = rw["wh_i"].ToString();
            objSheet.Cells[4 + i, 16] = rw["wh_o"].ToString();
            objSheet.Cells[4 + i, 17] = rw["repair_i"].ToString();
            objSheet.Cells[4 + i, 18] = rw["repair_o"].ToString();
            i++;
        }
    }

}
