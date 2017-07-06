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
using Excel8 = Microsoft.Office.Interop.Excel;

public partial class Boundary_wfrmwipbymodel : System.Web.UI.UserControl
{
    protected System.Web.UI.HtmlControls.HtmlTable TABLE1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModel(); 
        }
    }

    private void MultiLanguage()
    {
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
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
       // ddlModel.Items.Insert(0, "ALL");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string StrSql = "";
        if (!ddlModel.SelectedValue.Equals(" "))
            StrSql = QueryByModel(ddlModel.SelectedValue);
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "<script language=javascript>alert('請選擇幾種!');</script>");
            return;
        }
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        BuilderHtmlTableTitleClicking(dt);
        FullTableData(dt);

    }

    private string QueryByModel(string Model)
    {
        string StrSql = "";
        StrSql = " SELECT MODEL,PART,WO,WOQTY,"
            + " SUM(DECODE(STATION,'SMTA',QTY,0)) SMTA,"
            + " SUM(DECODE(STATION,'SMTB',QTY,0)) SMTB,"
            + " SUM(DECODE(STATION,'TEST',QTY,0)) TEST,"
            + " SUM(DECODE(STATION,'ASSY',QTY,0)) ASSY,"
            + " SUM(DECODE(STATION,'PACK',QTY,0)) PACK," 
            + " SUM(DECODE(STATION,'WH',QTY,0)) WH,"
            + " SUM(DECODE(STATION,'ARE',QTY,0)) ARE "
            + " FROM ("
            + " SELECT MODEL,PART,WO,WOQTY,STATION,COUNT(*) QTY"
            + " FROM ("
            + " select C.MODEL,C.SPART part,B.WO_NO wo,C.PID_QTY woqty,B.PANEL_ID pid,'SMTA' STATION"
            + " from sfc.mes_pcba_panel_wip a ,SFC.MES_PCBA_PANEL_DETAIL B,SHP.CMCS_SFC_SORDER C"
            + " WHERE  a.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND substr(a.panel_id,2,20)=b.panel_id  AND B.WO_NO=C.SORDER"
            + " AND (A.STATION_ID='ASIN' OR A.STATION_ID='AAOI' OR A.STATION_ID='ASVI'"
            + " OR A.STATION_ID LIKE 'S_IA' OR A.STATION_ID LIKE 'S_OA' OR A.STATION_ID LIKE 'S_VA')"
            + " AND C.MODEL =" + ClsCommon.GetSqlString(Model)
            + " UNION"
            + " select C.MODEL,C.SPART part,B.WO_NO wo,C.PID_QTY woqty,B.PANEL_ID pid,'SMTB' STATION"
            + " from sfc.mes_pcba_panel_wip a ,SFC.MES_PCBA_PANEL_DETAIL B,SHP.CMCS_SFC_SORDER C"
            + " where a.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND substr(a.panel_id,2,20)=b.panel_id AND B.WO_NO=C.SORDER"
            + " AND (A.STATION_ID='BSIN' OR A.STATION_ID='BAOI' OR A.STATION_ID='BSVI'"
            + " OR A.STATION_ID LIKE 'S_IB' OR A.STATION_ID LIKE 'S_OB' OR A.STATION_ID LIKE 'S_VB')"
            + " AND C.MODEL=" + ClsCommon.GetSqlString(Model) 
            + " UNION"
            + " select B.MODEL,B.SPART PART,A.WO_NO WO,B.PID_QTY WOQTY,A.PRODUCT_ID PID,'TEST' STATION"
            + " from sfc.MES_PCBA_WIP A,SHP.CMCS_SFC_SORDER B"
            + " where A.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND A.WO_NO=B.SORDER AND SEQUENCE_ID='1'"
            + " AND  B.MODEL=" + ClsCommon.GetSqlString(Model)
            + " UNION"
            + " select substr(b.apart,3,3) MODEL,b.apart PART,b.aorder WO,b.apid_qty WOQTY, a.product_id PID,'ASSY' STATION"
            + " from sfc.mes_assy_wip a,shp.cmcs_sfc_aorder b "
            + " where A.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND A.WO_NO=B.AORDER AND (station_id='AINP' or station_id like 'A_BI')"
            + " AND  substr(b.apart,3,3)=" + ClsCommon.GetSqlString(Model) 
            + " UNION"
            + " select substr(b.apart,3,3) MODEL,b.apart PART,b.aorder WO,b.apid_qty WOQTY, a.product_id PID,'PACK' STATION"
            + " from sfc.mes_assy_wip a,shp.cmcs_sfc_aorder b"
            + " where A.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND A.WO_NO=B.AORDER AND (a.station_id='FQC' or a.station_id like 'A_FO')"
            + " AND  substr(b.apart,3,3)=" + ClsCommon.GetSqlString(Model)
            + " UNION"
            + " select substr(b.ppart,3,3) MODEL,b.ppart PART,c.work_order WO,b.ppid_qty WOQTY, a.product_id PID,'PACK' STATION"
            + " from sfc.mes_assy_wip a,shp.cmcs_sfc_porder b,shp.cmcs_sfc_shipping_data c"
            + " where A.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND C.WORK_ORDER=B.PORDER AND a.station_id='PACK' AND A.PRODUCT_ID=C.PRODUCTID AND C.STATUS<>'已出庫'"
            + " AND  substr(b.apart,3,3)=" + ClsCommon.GetSqlString(Model) 
            + " UNION"
            + " select SUBSTR(C.PPART,3,3) MODEL,c.ppart part,b.work_order WO,c.ppid_qty WOQTY,b.productid PID,'WH' STATION"
            + " from SFC.MES_PACK_OQC a,SHP.CMCS_SFC_SHIPPING_DATA b,SHP.CMCS_SFC_PORDER C"
            + " where A.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND B.Ddate>to_date('2009/09/15','YYYY/MM/DD') and c.porder =b.work_order"
            + " AND A.product_id=B.productid and b.status='未進倉'"
            + " AND SUBSTR(C.PPART,3,3)=" + ClsCommon.GetSqlString(Model)
            + " UNION"
            + " select A.MODEL_ID MODEL,'Repair' part,'Repair' WO,0 WOQTY,a.product_id PID,'ARE' STATION"
            + " from SFC.MES_REPAIR_WIP a"
            + " where A.creation_date>to_date('2009/09/15','YYYY/MM/DD')"
            + " AND A.MODEL_ID=" + ClsCommon.GetSqlString(Model)
            + " )GROUP BY MODEL,PART,WO,WOQTY,STATION"
            + " )GROUP BY MODEL,PART,WO,WOQTY";

        StrSql = StrSql + " union"
            + " select '總計' MODEL,'-' PART,'-' WO,0 WOQTY,SUM(SMTA),SUM(SMTB),SUM(TEST),SUM(ASSY),SUM(PACK),SUM(WH),SUM(ARE)"
            + " FROM ("
            + StrSql
            + ")";

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
            AddStationCell(htr, htr1, strColumnName);
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
                    if (rw["WO"].ToString() == "Repair" || rw["WO"].ToString() == "-")
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

    private void AddStationCell(HtmlTableRow htr, HtmlTableRow htr1, string StationName)
    {
        string strstation = StationName.ToUpper();
        switch (strstation)
        {
            case "SMTA":
                strstation = "SMT A Input";
                break;
            case "SMTB":
                strstation = "SMT B Input";
                break;
            case "TEST":
                strstation = "Board Test";
                break;
            case "ASSY":
                strstation = "Assmbly";
                break;
            case "PACK":
                strstation = "Pack";
                break;
            case "WH":
                strstation = "Warehouse";
                break;
            case "ARE":
                strstation = "Repair";
                break;
        }

        HtmlTableCell htc = new HtmlTableCell();
        htc.InnerHtml = "<font color='White'><STRONG>" + strstation + "</STRONG></font>";
        htc.BgColor = "#006699";
        htc.Align = "Center";
        //htc.ColSpan = 2;
        htr.Cells.Add(htc);
        htc.Dispose();       
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (!ddlModel.SelectedValue.Equals(" "))
            GetDownLoadData();
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "<script language=javascript>alert('請選擇幾種!');</script>");
            return;
        }
    }

    private void GetDownLoadData()
    {
        //string test = DateTime.Now.Year.ToString()+"/"+ddlMonth.SelectedValue+"/"+DateTime.Now.Day.ToString();
        string strFileName = Request.PhysicalApplicationPath + @"Templet\wip status.xlt";
        string ExportPath = Request.PhysicalApplicationPath + @"Temp\";

        Missing MissingValue = Missing.Value;

        Excel8.ApplicationClass objExcel = null;
        Excel8.Workbook objBook = null;
        Excel8.Worksheet objSheet = null;
        try
        {
            objExcel = new Excel8.ApplicationClass();
            objExcel.Visible = true;

            objBook = objExcel.Workbooks.Open(strFileName.ToString(), MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue, MissingValue, MissingValue,

                MissingValue);
            objSheet = (Excel8.Worksheet)objBook.Worksheets[1];

            string strSql = "";
            if (!ddlModel.SelectedValue.Equals(" "))
                strSql = QueryByModel(ddlModel.SelectedValue);
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "<script language=javascript>alert('請選擇幾種!');</script>");
                return;
            }
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

            WriteToExcel(objSheet, dt);

            strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            objBook.SaveAs(ExportPath + strFileName, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, Excel8.XlSaveAsAccessMode.xlExclusive, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue);
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

    private void WriteToExcel(Excel8.Worksheet objSheet, DataTable dt)
    {
        
        int i = 0;
        foreach (DataRow rw in dt.Rows)
        {
            objSheet.Cells[4 + i, 1] = rw["model"].ToString();
            objSheet.Cells[4 + i, 2] = rw["part"].ToString();
            objSheet.Cells[4 + i, 3] = rw["wo"].ToString();
            objSheet.Cells[4 + i, 4] = rw["woqty"].ToString();
            objSheet.Cells[4 + i, 5] = rw["smta"].ToString();
            objSheet.Cells[4 + i, 6] = rw["smtb"].ToString();

            objSheet.Cells[4 + i, 7] = rw["test"].ToString();
            objSheet.Cells[4 + i, 8] = rw["assy"].ToString();
            objSheet.Cells[4 + i, 9] = rw["pack"].ToString();
            objSheet.Cells[4 + i, 10] = rw["wh"].ToString();
            objSheet.Cells[4 + i, 11] = rw["are"].ToString(); 
            i++;
        }
    }
}
