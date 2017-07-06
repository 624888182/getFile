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
using System.Data.OracleClient;
using DBAccess.EAI;
using System.IO;
using System.Text;

public partial class Boundary_WFrmRetestReport : System.Web.UI.UserControl
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
    private void InitiaPage()
    {
        string strProcedureName = "SFCQUERY.GETMODELNAME";
        OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
        orapara[0].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();
        ddlModel.Items.Insert(0, "");
    }
    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");//rm.GetString("Model");
        lblLine.Text = (String)GetGlobalResourceObject("SFCQuery", "Line");//rm.GetString("Line");
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");//rm.GetString("DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");//rm.GetString("DateTo");
        btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");//rm.GetString("Query");
        btnExportDetailedExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "DetailExcel");

    }
    //protected void FailData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    protected void FailData_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#bec5e7'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f5f5'");
        }
        LinkButton btnHiddenPostButton = (LinkButton)e.Row.FindControl("lbtnTitle");
        if (btnHiddenPostButton != null)
        {
            e.Row.Cells[4].Attributes["onclick"] = String.Format("javascript:document.getElementById('{0}').click()", btnHiddenPostButton.ClientID);
            // 额外样式定义 
            e.Row.Cells[4].Attributes.Add("onmouseover", "this.style.backgroundColor='Red'");
            //鼠标移出时，行背景色变
            e.Row.Cells[4].Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            e.Row.Cells[4].Attributes.Add("style", "cursor:pointer");
            e.Row.Cells[4].Attributes.Add("title", "单击选择当前行");
        }
         
        
    }
    protected void FailData_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        int index = 0;
        if (e.CommandName == "RetestQty")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            // 获取到行索引 RowIndex
            index = gvrow.RowIndex;//取的行索引
            //string stationName = dgData.Rows[index].Cells[0].Text;
            //string error = dgData.Rows[index].Cells[1].Text;
            string[] strPID = dgData.Rows[index].Cells[5].Text.Split(',');
            listPID.Items.Clear();
            for (int i = 0; i < strPID.Length - 1; i++)
            {
                listPID.Items.Add(new ListItem(strPID[i].ToString()));
            }
        }

         
    }
    //查詢報表:1.查找幾種下的所有工站;2.篩選工站中的路由工站
    //3.按工站分類查找數據并按失敗原因統計數據
    //4.把各個工站的統計結果合并成幾種的結果顯示出來
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strModel = ddlModel.SelectedItem.Text.ToString();
        string strLine = ddlLine.SelectedItem.Text.ToString();
        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        if (strModel == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種！！');</script>");
            return;
        }

        if (strStartDate == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇起始日期！！');</script>");
            return;
        }
        if (strEndDate == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇截止日期！！');</script>");
            return;
        }

        string[] strAllstationNames = GetAllStations(strModel, strLine);
        dgData.DataSource = GetRetestReport(strModel, strAllstationNames, strLine,strStartDate,strEndDate);
        dgData.DataBind();

        dgData.Dispose();
      
    }

    protected string[] GetAllStations(string model, string line)
    {
        string[] strAllStations = null;
        string sql1 = "";

        sql1 = "SELECT * FROM SFC.SFC_ROUTING_HEADERS WHERE MODEL_NAME ='" + model 
                            + "' AND PART_NUMBER IS NULL  AND WORK_ORDER_NUMBER IS NULL";
       
        if (line != "")
        {
            sql1 += " AND LINE_NUMBER='" + line + "'";
        }
        else
        {
            sql1 += " AND LINE_NUMBER IS NULL";
        }

        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sql1).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            //DropDownListSPart.SelectedItem.Text = dt1.Rows[0][2].ToString();
            //TextBoxOrder.Text = dt1.Rows[0][3].ToString();
            //ddlLine.SelectedItem.Text = dt1.Rows[0][4].ToString();
            //TextBoxDescription.Text = dt1.Rows[0][5].ToString();
            //TextBoxRouteID.Text = dt1.Rows[0][0].ToString();
            //tbInvalidDate.DateTextBox.Text = dt1.Rows[0][7].ToString();
            string strRouteID = dt1.Rows[0][0].ToString();
            if (strRouteID != "")
            {
                string sql = "SELECT STATION_NAME,DISABLE_FLAG FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + strRouteID + " order by STATION_SEQUENCE_ID";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string temp = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // ((Label)(GridView1.Rows[i].FindControl("LabelID"))).Text = dt.Rows[i][0].ToString();
                        string stationName = GetStationName(dt.Rows[i][0].ToString());
                        //string StationFlowID = GetStationFlowID(strRouteID, GetStationValue(StationName));
                        //string stationName = dt.Rows[i][0].ToString();
                        if (temp == "")
                        {
                            temp = stationName;
                        }
                        else
                        {
                            temp += "," + stationName;
                        }
                        //sql = "SELECT STATION_NAME,FLOW_TYPE,DISABLE_FLAG FROM SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + StationFlowID;
                        //DataTable flows = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                        
                        //for (int k = 0; k < flows.Rows.Count; k++)
                        //{
                        //    string Flow = GetStationName(flows.Rows[k][0].ToString());
                        //    string Type = flows.Rows[k][1].ToString();
                        //    string Disable = flows.Rows[k][2].ToString();
                        //    if (Disable == "Y")
                        //        Disable = "True";
                        //    else
                        //        Disable = "False";
                        //    temp += Flow + "," + Type + "," + Disable + ",";
                        //}
                        
                    }
                    strAllStations = temp.Split(',');
                }
            }
        }

        return strAllStations;
    }

    protected string[] GetRouterStations(string model, string[] strAllStations)
    {
        return null;
    }

    protected DataTable GetRetestReport(string model, string[] strRouterStations,string line,string startDate,string endDate)
    {
        DataTable dtRetest = new DataTable();
        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "TestStation";
        dtRetest.Columns.Add(dc1);
        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "FailuresDescription";
        dtRetest.Columns.Add(dc2);
        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "InputQty";
        dtRetest.Columns.Add(dc3);
        DataColumn dc4 = new DataColumn();
        dc4.ColumnName = "RetestYieldloss(%)";
        dtRetest.Columns.Add(dc4);
        DataColumn dc5 = new DataColumn();
        dc5.ColumnName = "RetestQty";
        dtRetest.Columns.Add(dc5);
        DataColumn dc6 = new DataColumn();
        dc6.ColumnName = "PID";
        dtRetest.Columns.Add(dc6);

        for (int i = 0; i < strRouterStations.Length; i++)
        {
            string strAll = startDate + "," + endDate + "," + strRouterStations[i].ToString() + "," + model + "," + line + ",0";
            DataTable dtTemp = FindRetestByStation(strAll);//0开始时间/1结束时间/2选择站别/3幾種/4線別/5Without Repair
            if (dtTemp!=null&&dtTemp.Rows.Count > 0)
            {
                for (int j = 0; j < dtTemp.Rows.Count; j++)
                {
                    DataRow dr = dtRetest.NewRow();
                    dr[0] = strRouterStations[i].ToString();
                    for (int k = 0; k < 5; k++)
                    {
                        dr[k+1] = dtTemp.Rows[j][k].ToString();
                    }
                    dtRetest.Rows.Add(dr);
                }
            }
        }
         return dtRetest;
    }

    public string GetStationName(string value)
    {
        try
        {
            switch (value)//因為這幾個station_desc的值與*.PRODUCT_HISTORY_V視圖中不相符,故區別對待
            {
                case "DL":
                case "BT":
                case "BTWL":
                case "D2":
                case "DL1":
                case "DL2":
                    return value;
            }
            string sql = "Select STATION_DESC STATION_NAME from SFC.SFC_PRODUCTION_STATIONS where STATION_CODE='" + value.Trim() + "'";
            string id = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0].ToString();
            return id;
        }
        catch
        {
            return "";
        }
    }

    public string GetStationValue(string value)
    {
        try
        {
            string sql = "Select STATION_CODE STATION_ID from SFC.SFC_PRODUCTION_STATIONS where STATION_DESC='" + value.Trim() + "'";
            string id = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0].ToString();
            return id;
        }
        catch
        {
            return "";
        }
    }

    public string GetStationFlowID(string routeid, string station)
    {
        try
        {
            string sql = "Select STATION_SEQUENCE_ID from SFC.SFC_ROUTING_STATIONS where STATION_NAME='" + station + "' and ROUTING_SEQUENCE_ID=" + routeid;
            string id = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0].ToString();
            return id;
        }
        catch
        {
            return "";
        }
    }

    public DataTable FindRetestByStation(string strAll)
    {
        //0开始时间/1结束时间/2选择站别/3幾種/4線別/5Without Repair
        DataTable dtResult = null;
        string sql = strAll;
        //test 某個站
        string[] strTest = strAll.Split(',');
        if (strTest[2] == "BTWL")
        {
            strTest[2] = "";
        }
        //
        string[] strSql = GetData(sql);//得到SQL語句
        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql[0].ToString());//全部
        DataSet dsFinalPass = ClsGlobal.objDataConnect.DataQuery(strSql[1].ToString());//第二,三次Pass
        DataSet dsFinalFail = ClsGlobal.objDataConnect.DataQuery(strSql[3].ToString());//第三次fail
        DataSet dsFirstOrSecondFail = null;
        //ClsGlobal.objDataConnect.DataQuery(strSql[2].ToString());//第一次或第二次Fail
        DataTable dtFails = null;
        DataTable dtPass = null;
        string strError = string.Empty;
        string strErrorItem = string.Empty;
        int total = 0;

        //計算總量
        if (ds!=null&&ds.Tables.Count > 0)
        {
            //dt = ds.Tables[0];
            total = ds.Tables[0].Rows.Count;
        }

        if (rblQueryType.SelectedIndex == 0)
        {
            if (dsFinalPass != null && dsFinalPass.Tables.Count > 0)
            {
                //取得第二,三次Pass的數據
                dtPass = dsFinalPass.Tables[0];

                //取出Error_Msg的類型,把PID和ERROR_MSG存起來

                for (int i = 0; i < dtPass.Rows.Count; i++)
                {
                    string failItemSql = strSql[2] + "'" + dtPass.Rows[i][3].ToString() + "'";//根據pid取得查詢失敗信息的sql語句
                    dsFirstOrSecondFail = ClsGlobal.objDataConnect.DataQuery(failItemSql.ToString());
                    if (dsFirstOrSecondFail != null && dsFirstOrSecondFail.Tables.Count > 0)
                    {
                        dtFails = dsFirstOrSecondFail.Tables[0];
                        for (int j = 0; j < dtFails.Rows.Count; j++)
                        {
                            string strTempPid = dtFails.Rows[j][3].ToString() + ",";
                            string strTempError_msg = dtFails.Rows[j][9].ToString() + ";";
                            string strTempUnit = strTempPid + strTempError_msg;
                            if (strErrorItem.IndexOf(strTempUnit.ToString()) == -1)//過濾掉pid和失敗類型重複的記錄
                            {
                                strErrorItem += strTempUnit;
                            }
                            //strErrorItem += dtFails.Rows[j][3].ToString() + ",";//取得pid
                            //strErrorItem += dtFails.Rows[j][9].ToString() + ";";//取得error_msg

                            if (strError.IndexOf(dtFails.Rows[j][9].ToString()) == -1)
                            {
                                if (strError != "")
                                {
                                    strError += ",";
                                }
                                strError += dtFails.Rows[j][9].ToString();//取得error_msg的不同類型
                            }
                        }
                    }
                }
            }
        }
        else
        { 
            if (dsFinalFail != null && dsFinalFail.Tables.Count > 0)
            {
                //取得第三次Fail的數據
                dtFails = dsFinalFail.Tables[0];

                //取出Error_Msg的類型,把PID和ERROR_MSG存起來

                //for (int i = 0; i < dtPass.Rows.Count; i++)
                //{
                    //string failItemSql = strSql[2] + "'" + dtPass.Rows[i][3].ToString() + "'";//根據pid取得查詢失敗信息的sql語句
                    //dsFirstOrSecondFail = ClsGlobal.objDataConnect.DataQuery(failItemSql.ToString());
                    //if (dsFirstOrSecondFail != null && dsFirstOrSecondFail.Tables.Count > 0)
                    //{
                        //dtFails = dsFirstOrSecondFail.Tables[0];
                        for (int j = 0; j < dtFails.Rows.Count; j++)
                        {
                            string strTempPid = dtFails.Rows[j][3].ToString() + ",";
                            string strTempError_msg = dtFails.Rows[j][9].ToString() + ";";
                            string strTempUnit = strTempPid + strTempError_msg;
                            if (strErrorItem.IndexOf(strTempUnit.ToString()) == -1)//過濾掉pid和失敗類型重複的記錄
                            {
                                strErrorItem += strTempUnit;
                            }
                            //strErrorItem += dtFails.Rows[j][3].ToString() + ",";//取得pid
                            //strErrorItem += dtFails.Rows[j][9].ToString() + ";";//取得error_msg

                            if (strError.IndexOf(dtFails.Rows[j][9].ToString()) == -1)
                            {
                                if (strError != "")
                                {
                                    strError += ",";
                                }
                                strError += dtFails.Rows[j][9].ToString();//取得error_msg的不同類型
                            }
                        }
                    //}
                //}
            }
        }
        //if (dsFirstOrSecondFail != null && dsFirstOrSecondFail.Tables.Count > 0)
        //{
        //    dtFails = dsFirstOrSecondFail.Tables[0];
        //    //抓出不同的失敗類型
        //    for (int i = 0; i < dtFails.Rows.Count; i++)
        //    {
        //        if (strError.IndexOf(dtFails.Rows[i][9].ToString()) == -1)
        //        {
        //            if (strError != "")
        //            {
        //                strError += ",";
        //            }
        //            strError += dtFails.Rows[i][9].ToString();
        //        }
        //    }

            string[] strError_msg = strError.Split(',');//分離出錯誤類型
            string[] strFailItems = strErrorItem.Split(';');//分離出失敗項
            dtResult = new DataTable();
            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "FailuresDescription";
            dtResult.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "InputQty";
            dtResult.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "RetestYieldloss(%)";
            dtResult.Columns.Add(dc3);
            DataColumn dc4 = new DataColumn();
            dc4.ColumnName = "RetestQty";
            dtResult.Columns.Add(dc4);
            DataColumn dc5 = new DataColumn();
            dc5.ColumnName = "PID";
            dtResult.Columns.Add(dc5);

            for (int j = 0; j < strError_msg.Length; j++)
            {
                DataRow dr = dtResult.NewRow();
                dr[0] = strError_msg[j].ToString();
                int iTemp = 0;
                string strPID = string.Empty;
                //for (int k = 0; k < dtFails.Rows.Count; k++)
                //{
                //    if (dtFails.Rows[k][9].ToString() == dr[0].ToString() && isFinalPass(dtFails.Rows[k][3].ToString(), dtPass))
                //    {
                //        iTemp++;
                //        strPID += dtFails.Rows[k][3].ToString()+",";
                //    }
                //}
                for (int k = 0; k < strFailItems.Length - 1; k++)
                {
                    string[] strFailItem = strFailItems[k].Split(',');
                    if (strFailItem[1].ToString() == dr[0].ToString())
                    {
                        iTemp++;
                        strPID += strFailItem[0].ToString() + ",";
                    }
                }
                dr[1] = total.ToString();
                dr[3] = iTemp.ToString();
                dr[4] = strPID.ToString();
                if (total != 0)
                {
                    dr[2] = Convert.ToString(Convert.ToInt32(((double)iTemp / total) * 100)) + "%";
                }
                if (iTemp != 0)
                {
                    dtResult.Rows.Add(dr);
                }
            }
        
        return dtResult;
    }

    public bool isFinalPass(string productID, DataTable dtPass)
    {
        for (int i = 0; i < dtPass.Rows.Count; i++)
        {
            if (productID == dtPass.Rows[i][3].ToString())
            {
                return true;
            }
        }
        return false;
    }
    public string[] GetData(string sql)
    {
        if (sql == "")
        {
            return null;
        }
        string[] strDateAndFail = sql.Split(',');////0开始时间/1结束时间/2选择站别/3幾種/4線別/5Without Repair
        string strModel = strDateAndFail[3];
        string strStationID = strDateAndFail[2];
        string strTableName = "";
        string strColumnName = "";
        switch (strStationID)
        {


            case "RE/DL":
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;
            case "CA_EDGE":
            case "CA_SET 1":
                strTableName = "CALIBRATION";
                strColumnName = "CALDATE";
                break;
            case "PT_EDGE":
            case "PT_SET 1":
                strTableName = "PRETEST";
                strColumnName = "PTDATE";
                break;
            case "EDGEPT":
                strTableName = "EDGE_PRETEST";
                strColumnName = "PTDATE";
                break;

            case "B1":
            case "B2":
            case "B3":
            case "B4":
                strTableName = "BASEBANDTEST";
                strColumnName = "BBDATE";
                break;

            case "GPSWL":
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;

            case "A1":
            case "A2":
            case "A3":
            case "A4":
            case "A5":
                strTableName = "BASEBANDTEST";
                strColumnName = "BBDATE";
                break;
            case "OOB":
                if (strModel.Equals("GLM"))
                {
                    strTableName = "BASEBANDTEST";
                    strColumnName = "BBDATE";
                }
                else
                {
                    strTableName = "MES_ASSY_HISTORY";
                    strColumnName = "CREATION_DATE";
                }
                break;
            case "FQC":
                if (strModel.Equals("GLM"))
                {
                    strTableName = "BASEBANDTEST";
                    strColumnName = "BBDATE";
                }
                else
                {
                    strTableName = "MES_PACK_OOB";
                    strColumnName = "CREATION_DATE";
                }
                break;
            case "GP":

            case "GPS":
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;

            case "BlnkFlsh":
                if (strModel.Equals("GNG"))
                {
                    strTableName = "GNG_POWERON_PATS_TH";
                    strColumnName = "TEST_DATE";
                }

                break;

            case "SIMLOCK":
                strTableName = "E2PCONFIG";
                strColumnName = "E2PDATE";
                break;
            default:
                strTableName = "PRODUCT_HISTORY_V";
                strColumnName = "PDDATE";
                break;
        }
        string strSql = GetTestStationSql(strTableName, strColumnName, sql);
        string[] strAllSql = strSql.Split('$');
        if (strStationID.Equals("FQC") && !strModel.Equals("GLM"))
        {
            for (int i = 0; i < strAllSql.Length; i++)
            {
                strAllSql[i] = strAllSql[i].Replace(strModel + ".", " ");
                strAllSql[i] = strAllSql[i].Replace("PRODUCTID", "PRODUCT_ID");
                strAllSql[i] = strAllSql[i].Replace("STATUS", "STATE_ID");
            }
        }

        return strAllSql;
    }

    private string GetTestStationSql(string strTableName, string strColumnName, string sql)
    {
        if (sql == "")
        {
            return "";
        }
        string[] strDateAndFail = sql.Split(',');//0开始时间/1结束时间/2选择站别/3幾種/4線別/5Without Repair//0:ERROR_MSG;1:?始??;2:?束??;3:??;4：??站?;5:幾種;6:線別;7:Repair
        string strModel = strDateAndFail[3];

        string strStationID = strDateAndFail[2];
        string strStartDate = strDateAndFail[0];
        string strEndDate = strDateAndFail[1];
        string strLine = strDateAndFail[4];
        string strRepair = strDateAndFail[5];
        //string strRadioType = strDateAndFail[3];
        string strProductID = "PRODUCTID";
        string strWorkOrder = "WORKORDER";
        if (strTableName.Equals("PRODUCT_HISTORY_V"))
        {
            strProductID = "PRODUCT_ID";
            strWorkOrder = "WORK_ORDER";
        }
        string strSql = "";
        string strFASFassSql1 = string.Empty;//第2,3次Pass
        string strFailSql2 = string.Empty;//第一,二次失敗
        string strFinalFailSql = string.Empty;//第三次失敗
        string strWhere = "";
        string strStaioncode = "";
        string strStationName = "";

        //DataTime 范圍
        strWhere = strWhere + " AND " + strColumnName + " BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

        //LINE
        if (!strLine.ToString().Equals(""))
            strWhere = strWhere + " AND UPPER(ONLINENAME) = " + ClsCommon.GetSqlString(strLine.ToString().ToUpper());


        //whether Repair
        switch (strRepair)
        {
            case "0":
                if ((!strStationID.Equals("FQC") && !strStationID.Equals("CFC") && !strStationID.Equals("CI") && !strStationID.Equals("Pack") && !strStationID.Equals("CFC")
                 && !strStationID.Equals("Phasing") && !strStationID.Equals("PowerOn") && !strStationID.Equals("Proto") && !strStationID.Equals("CFC")
                 && !strStationID.Equals("Bluetooth") && !strStationID.Equals("Bluetest")))
                    strWhere = strWhere + " AND REPAIR=0  and '" + strWorkOrder + "' not like 'R%'";
                break;
            case "1":
                //if (strModel.Equals("DVR") || strModel.Equals("GNG") || strModel.Equals("MRE") || strModel.Equals("MRO") || strModel.Equals("RCD") || strModel.Equals("RCX") || strModel.Equals("RUY") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("WLO"))
                //strWhere = strWhere + " AND REPAIR=3 ";
                break;
        }

        switch (strStationID)
        {

            case "B1":
                strWhere = strWhere + " AND STATION='B1'  ";
                strStaioncode = " AND STATION='B1'";
                strStationName = "STATION";
                break;
            case "B2":
                strWhere = strWhere + " AND STATION='B2'  ";
                strStaioncode = " AND STATION='B2'";
                strStationName = "STATION";
                break;
            case "B3":
                strWhere = strWhere + " AND STATION='B3'  ";
                strStaioncode = " AND STATION='B3'";
                strStationName = "STATION";
                break;
            case "B4":
                strWhere = strWhere + " AND STATION='B4'  ";
                strStaioncode = " AND STATION='B4'";
                strStationName = "STATION";
                break;
            case "A1":
                strWhere = strWhere + " AND STATION='A1'  ";
                strStaioncode = " AND STATION='A1'";
                strStationName = "STATION";
                break;
            case "A2":
                strWhere = strWhere + " AND STATION='A2'  ";
                strStaioncode = " AND STATION='A2'";
                strStationName = "STATION";
                break;
            case "A3":
                strWhere = strWhere + " AND STATION='A3'  ";
                strStaioncode = " AND STATION='A3'";
                strStationName = "STATION";
                break;
            case "A4":
                strWhere = strWhere + " AND STATION='A4'  ";
                strStaioncode = " AND STATION='A4'";
                strStationName = "STATION";
                break;
            case "A5":
                strWhere = strWhere + " AND STATION='A5'  ";
                strStaioncode = " AND STATION='A5'";
                strStationName = "STATION";
                break;
            case "GP":
                strWhere = strWhere + " AND STATION='GP'  ";
                strStaioncode = " AND STATION='GP'";
                strStationName = "STATION";
                break;

            case "GPS":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPS'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;


            case "GPSWL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPSWL'  ";
                strStaioncode = " AND STATION_CODE = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "OOB":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION='OOB'  ";
                    strStaioncode = " AND STATION='OOB'";
                    strStationName = "STATION";
                }
                break;
            case "CA_EDGE":
            case "PT_EDGE":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'EDGE' ";
                strStationName = "STATION";
                break;
            case "CA_SET 1":
            case "PT_SET 1":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'SET 1' ";
                strStationName = "STATION";

                break;
            case "FQC":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION = 'FQC' ";
                    strStaioncode = " AND STATION='FQC'";
                    strStationName = "STATION";
                }
                else
                    strWhere = strSql + " AND STATION_ID LIKE 'A_FO' ";
                break;
            case "LV":

                strWhere = strWhere + " AND UPPER(STATION_CODE) LIKE '" + strStationID.Trim() + "%'";

                strStaioncode = " AND STATION_CODE='LV'";
                strStationName = "STATION_CODE";
                break;
            case "GSMWL":
            case "FQA":
            case "QA":
                strWhere = strWhere + " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "SIMLOCK":
                strStationName = "STATION_CODE";
                break;
            case "RE/DL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='D2'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            default:
                strWhere = strWhere + " AND UPPER(STATION_CODE) = " + ClsCommon.GetSqlString(strStationID);
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStationID.Trim() + "'";
                strStationName = "STATION_CODE";
                break;


        }

        if ((strModel.Equals("GLM") && strTableName.Equals("PRETEST")) || (strModel.Equals("GLM") && strTableName.Equals("FQC_PRETEST")))
            strWhere = strWhere + " AND (UPPER(TESTNAME) NOT LIKE 'PRTINITUUT%' or testname is null )";
        string strOrder = " ASC";
        //核心語句
        //switch (strRadioType)
        //{
        //    case "0":  //Total Data
                if (strModel.Equals("GLM") && strTableName.Equals("PRETEST"))
                    strSql = "SELECT distinct(product_id) FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4) + " AND UPPER(TESTNAME) NOT LIKE 'PRTINITUUT%'";// ORDER BY " + strColumnName;
                else
                {
                    if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                        strSql = "SELECT A.* FROM " + strModel + "." + strTableName + " A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD  WHERE " + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID ORDER BY A." + strColumnName;
                    else
                        strSql = "SELECT distinct(product_id) FROM " + strModel + "." + strTableName + " A WHERE " + strWhere.Substring(4);// +"  ORDER BY " + strColumnName;
                }
                 
             

                if ((strModel.Equals("DVR") || strModel.Equals("RCX") || strModel.Equals("GNG") || strModel.Equals("SLG") || strModel.Equals("TWN") || strModel.Equals("DVL") || strModel.Equals("MRO") || strModel.Equals("MRE") || strModel.Equals("RUY")) && strRepair == "0")
                {
                    //switch (strRadioType)
                    //{
                    //    case "5":
                    strFASFassSql1 = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE ASC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                           + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN IN (2,3) AND STATUS='P'";// AND ERROR_MSG='"
                    //+ strDateAndFail[0] + "')";

                    //    break;
                    //case "6":
                    //strFailSql2 = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE DESC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                    //       + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE rn<4 and rn>1 AND STATUS='P' order by error_msg";// AND ERROR_MSG='"

                    strFailSql2 = "SELECT * FROM " + strModel + ".PRODUCT_HISTORY_V WHERE "
                           + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE STATUS='F' AND PRODUCT_ID =";// AND ERROR_MSG='"
                    //+ strDateAndFail[0] + "')";
                    //break;
                    // }
                    strFinalFailSql = "SELECT * FROM (SELECT A.*,ROW_NUMBER() OVER (PARTITION BY A.PRODUCT_ID ORDER BY A.PDDATE ASC) RN FROM " + strModel + ".PRODUCT_HISTORY_V A,(SELECT * FROM  " + strModel + ".PRODUCT_HISTORY_VYIELD WHERE "
                           + strWhere.Substring(4) + " AND TESTTIME=1) B WHERE A.STATION_CODE=B.STATION_CODE AND A.PRODUCT_ID=B.PRODUCT_ID )WHERE RN = 3 AND STATUS='F'"; 
                    
                }
                else
                {
                    strFASFassSql1 = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                     + strWhere.Substring(4) + ") WHERE RN IN (2,3)  AND UPPER(SUBSTR(STATUS, 1, 1)) = 'P'";// AND ERROR_MSG='"

                    //strFailSql2 = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                    //    + strWhere.Substring(4) + ") WHERE rn<4 and rn>1  AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F'order by error_msg";
                    strFailSql2 = "SELECT distinct  * FROM " + strModel + "." + strTableName + " A WHERE "
                         + strWhere.Substring(4) + " AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F' AND PRODUCT_ID =";

                    strFinalFailSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY " + strColumnName + strOrder + " ) RN FROM " + strModel + "." + strTableName + " A WHERE "
                     + strWhere.Substring(4) + ") WHERE RN = 3  AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F'";// AND ERROR_MSG='"
 
                }
                string strAllSql = strSql + "$" + strFASFassSql1 + "$" + strFailSql2 + "$" + strFinalFailSql;
        return strAllSql;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);

        dgData.RenderControl(hw);
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment; filename=FailQuery.xls");
        response.Charset = "gb2312";
        response.Write(tw.ToString());
        response.End();
    }

  //============================詳細報表部分  strat===========================================================
    protected void btnExportDetailedExcel_Click(object sender, EventArgs e)
    {
        string strModel = ddlModel.SelectedItem.Text.ToString();         
        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        if (strModel == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇幾種！！');</script>");
            return;
        }

        if (strStartDate == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇起始日期！！');</script>");
            return;
        }
        if (strEndDate == "")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請選擇截止日期！！');</script>");
            return;
        }

        string[] strRouteStations = GetRouteStations(strModel);
        // GetRetestReport(strModel, strAllstationNames, strLine, strStartDate, strEndDate);
        DetailToExcel(strModel,strRouteStations); 
    }
    protected void DetailToExcel(string strModel,string[] strRouteStations)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "UTF-8";//GB2312
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + "RetestDetailReport"
            + DateTime.Now.AddDays(1).ToString("yyyyMMdd") + ".xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//GB2312
        Response.ContentType = "application/ms-excel";

        Response.Write(GetDetailInfo(strModel, strRouteStations));
       
        Response.End();
    
    }

    protected string GetDetailInfo(string strModel, string[] strRouteStations)
    {
        StringBuilder sb;
        sb = new StringBuilder();
        DataTable dt = null;
        //================================良率部分==================================================================
        sb = CreateFirstRow(sb);//創建表頭
        //return sb.ToString();
        sb.Append("<table width='100%' style='BORDER-STYLE: solid;' border='1px' bordercolor='#000000'>");


        //一站一站的寫
        for (int i = 0; i < strRouteStations.Length; i++)
        {
            dt = GetStationContent(strModel, strRouteStations[i]);

            //一線一線的寫
            if (dt.Rows.Count > 0)
            {
                sb.Append("<tr>");
                sb.Append("<td rowSpan='" + dt.Rows.Count.ToString() + "'>" + strRouteStations[i] + "</td>");
                for (int z = 0; z < dt.Columns.Count; z++)
                {
                    sb.Append("<td>" + dt.Rows[0][z].ToString() + "</td>");
                }
                sb.Append("</tr>");
                for (int k = 1; k < dt.Rows.Count; k++)
                {
                    sb.Append("<tr>");

                    for (int z = 0; z < dt.Columns.Count; z++)
                    {
                        if (dt.Rows[k][0].ToString() == "All Station")
                        {
                            sb.Append("<td style='background-color: #00ff00;'>" + dt.Rows[k][z].ToString() + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + dt.Rows[k][z].ToString() + "</td>");
                        }
                    }
                    sb.Append("</tr>");
                }
            }

        }
        sb.Append("<tr><td colspan='14' rowspan='3'></td></tr></table>");
        //return sb.ToString();
        //=====================================重測率部分===========================================================
        sb = CreateRetestFirstRow(sb);//創建表頭

        sb.Append("<table width='100%' style='collapse;BORDER-STYLE: solid;' border='1px' bordercolor='#000000'>");

        //一站一站的寫
        DataTable dtContent = null;
        for (int i = 0; i < strRouteStations.Length; i++)
        {
            dt = GetRetestSymptom(strModel, strRouteStations[i]);//得到Error_MSG
            string[] strSortErrorMsg = SortErrorMsg(strModel, strRouteStations[i], dt);//給報表排序
            if (null == dt||dt.Rows.Count<2)
            {
                continue;
            }
            //先畫第一行
            int colNumber = GetStationColNumber(strModel, strRouteStations[i], dt);
            colNumber += 1;
            sb.Append("<tr>");
            sb.Append("<td rowspan='" + colNumber.ToString() + "'>" + strRouteStations[i] + "</td>");
            //dtContent = GetRetestStationContent(strModel, strRouteStations[i], dt.Rows[0][0].ToString());
            dtContent = GetRetestStationContent(strModel, strRouteStations[i], strSortErrorMsg[0].ToString());
            if (dtContent.Rows.Count < 1)
            {
                continue;
            }
            //sb.Append("<td rowspan='" + dtContent.Rows.Count.ToString() + "' colspan='4'>" + dt.Rows[0][0].ToString() + "</td>");
            sb.Append("<td rowspan='" + dtContent.Rows.Count.ToString() + "' colspan='4'>" + strSortErrorMsg[0].ToString() + "</td>");

            int totalnumber = 0;
            for (int m = 0; m < dtContent.Rows.Count; m++)
            {
                totalnumber += Convert.ToInt32(dtContent.Rows[m][2].ToString());
            }
            int[] InputAndRetestTotalNumber = GetInputTotalNumber(strModel, strRouteStations[i]);
            double retestRate = (double)totalnumber*100 / InputAndRetestTotalNumber[0];
            sb.Append("<td rowspan='" + dtContent.Rows.Count.ToString() + "'>" + retestRate.ToString() + "%</td>");
            sb.Append("<td>" + dtContent.Rows[0][0].ToString() + "</td>");
            sb.Append("<td>" + dtContent.Rows[0][1].ToString() + "</td>");
            sb.Append("<td>" + dtContent.Rows[0][2].ToString() + "</td>");
            sb.Append("<td>" + dtContent.Rows[0][3].ToString() + "</td>");
            sb.Append("</tr>");
            //一Fail一Fail的寫
            if (dt.Rows.Count > 0)
            {

                for (int j = 0; j < dt.Rows.Count; j++)//Error_Msg
                {
                    //dtContent = GetRetestStationContent(strModel, strRouteStations[i], dt.Rows[j][0].ToString());
                    dtContent = GetRetestStationContent(strModel, strRouteStations[i], strSortErrorMsg[j].ToString());
                    if (null == dtContent || dtContent.Rows.Count < 1)
                    {
                        continue;
                    }
                    
                    //sb.Append("<td rowspan='" + dt.Rows.Count.ToString() + "'>" + strRouteStations[i] + "</td>");
                    if (j != 0)
                    {
                        sb.Append("<tr>");
                        //sb.Append("<td rowspan='" + dtContent.Rows.Count.ToString() + "' colspan='4'>" + dt.Rows[j][0].ToString() + "</td>");
                        sb.Append("<td rowspan='" + dtContent.Rows.Count.ToString() + "' colspan='4'>" + strSortErrorMsg[j].ToString() + "</td>");

                        totalnumber = 0;
                        for (int m = 0; m < dtContent.Rows.Count; m++)
                        {
                            totalnumber += Convert.ToInt32(dtContent.Rows[m][2].ToString());
                        }
                        retestRate = (double)totalnumber * 100 / InputAndRetestTotalNumber[0];
                        sb.Append("<td rowspan='" + dtContent.Rows.Count.ToString() + "'>" + retestRate.ToString() + "%</td>");
                        sb.Append("<td>" + dtContent.Rows[0][0].ToString() + "</td>");
                        sb.Append("<td>" + dtContent.Rows[0][1].ToString() + "</td>");
                        sb.Append("<td>" + dtContent.Rows[0][2].ToString() + "</td>");
                        sb.Append("<td>" + dtContent.Rows[0][3].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    for (int z = 1; z < dtContent.Rows.Count; z++)
                    {
                        //if (j == 0 && 0 == z)
                        //{
                        //    continue;//因為此行已經在上面畫了,故跳過
                        //}
                        sb.Append("<tr>");
                        sb.Append("<td>" + dtContent.Rows[z][0].ToString() + "</td>");
                        sb.Append("<td>" + dtContent.Rows[z][1].ToString() + "</td>");
                        sb.Append("<td>" + dtContent.Rows[z][2].ToString() + "</td>");
                        sb.Append("<td>" + dtContent.Rows[z][3].ToString() + "</td>");
                        sb.Append("</tr>");
                        
                    }
                    

                }
                sb.Append("<tr style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold; background-color: #00ff00;'>");
                sb.Append("<td colspan='4'>All Symptom Code</td>");
                sb.Append("<td> </td>");
                sb.Append("<td>All Station</td>");
                sb.Append("<td>" + InputAndRetestTotalNumber[0].ToString() + "</td>");
                sb.Append("<td>" + InputAndRetestTotalNumber[1].ToString() + "</td>");
                double tempRate = (double)InputAndRetestTotalNumber[1] *100/ InputAndRetestTotalNumber[0];
                sb.Append("<td>" + tempRate.ToString() + "%</td>");
                sb.Append("</tr>");
                 
            }

        }
        sb.Append("</table>");


        return sb.ToString();
    }
    protected StringBuilder CreateFirstRow(StringBuilder sb)
    {
        sb.Append("<table width='100%' style='BORDER-STYLE: solid;' border='1px' bordercolor='#000000'>");
        sb.Append("<tr><td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;");
        sb.Append("' align='center' rowspan='2' width='100'>");
        sb.Append("Station Group</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold; background-color: #33ff66;' align='center' rowspan='2' width='100'>Station LineID</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold; background-color: #33ff66;' align='center' colspan='3' width='120'>Frist Times(unit:pcs)</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold; background-color: #33ff66;' align='center' colspan='3' width='120'>Direct Retest</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold; background-color: #33ff66;' align='center' colspan='3' width='120'>2nd Retest</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold; background-color: #33ff66;' align='center' colspan='3' width='120'>Yield(unit:percent)</td>");
        sb.Append("</tr><tr>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='50' >Input</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='40'>Pass</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='40'>Fail</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='50'>Input</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='40'>Pass</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='40'>Fail</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='50'>Input</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='40'>Pass</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='40'>Fail</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66; ' align='center' width='40'>FristTime</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='50'>FinalYield</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 15px; font-weight: bold; background-color: #33ff66;' align='center' width='60'>RetestYield</td>");
        sb.Append("</tr></table>");
        return sb;
    }
    protected string[] GetRouteStations(string strModel)
    {
        string[] strRouteStations = null;
        string sql1 = "";

        sql1 = "SELECT * FROM " + strModel + ".SFC_STATION_NAME";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql1).Tables[0];
        string stationName = string.Empty;

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (stationName == "")
                {
                    stationName = dt.Rows[i][1].ToString();
                }
                else
                {
                    stationName += ","+ dt.Rows[i][1].ToString() ;
                }
                 
            }
            strRouteStations = stationName.Split(',');
                 
        }

        return strRouteStations;
    }
    protected DataTable GetStationContent(string strModel, string strStation)
    {
        DataTable dtResult = new DataTable();
        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "StationLineID";
        dtResult.Columns.Add(dc1);
        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "FirstInput";
        dtResult.Columns.Add(dc2);
        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "FirstPass";
        dtResult.Columns.Add(dc3);
        DataColumn dc4 = new DataColumn();
        dc4.ColumnName = "FirstFail";
        dtResult.Columns.Add(dc4);
        DataColumn dc5 = new DataColumn();
        dc5.ColumnName = "DirectRetestInput";
        dtResult.Columns.Add(dc5);
        DataColumn dc6 = new DataColumn();
        dc6.ColumnName = "DirectRetestPass";
        dtResult.Columns.Add(dc6);
        DataColumn dc7 = new DataColumn();
        dc7.ColumnName = "DirectRetestFail";
        dtResult.Columns.Add(dc7);
        DataColumn dc8 = new DataColumn();
        dc8.ColumnName = "2ndRetestInput";
        dtResult.Columns.Add(dc8);
        DataColumn dc9 = new DataColumn();
        dc9.ColumnName = "2ndRetestPass";
        dtResult.Columns.Add(dc9);
        DataColumn dc10 = new DataColumn();
        dc10.ColumnName = "2ndRetestFail";
        dtResult.Columns.Add(dc10);
        DataColumn dc11 = new DataColumn();
        dc11.ColumnName = "FristYield";
        dtResult.Columns.Add(dc11);
        DataColumn dc12 = new DataColumn();
        dc12.ColumnName = "FinalYield";
        dtResult.Columns.Add(dc12);
        DataColumn dc13 = new DataColumn();
        dc13.ColumnName = "RetestYield";
        dtResult.Columns.Add(dc13);

         
            //DataRow dr = dtResult.NewRow();
            //dr[0] = strError_msg[j].ToString();
             //   dtResult.Rows.Add(dr);
           
        

        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        string strWhere = "";
         
        //DataTime 范圍
        strWhere = strWhere + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

        string strSql = "SELECT distinct(line_code) FROM  " + strModel + ".PRODUCT_HISTORY_V WHERE STATION_CODE = '" + strStation + "' and " + strWhere.Substring(4) + "ORDER BY line_code ";
        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql.ToString());//查找線別

        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            double firstrate = 0.0;
            double finalrate = 0.0;
            for (int i = 0; i < ds.Tables[0].Rows.Count+1; i++)
            {
                string[] strcontent = null;
                DataRow dr = dtResult.NewRow();
                if (i == ds.Tables[0].Rows.Count)
                {
                    strcontent = GetLineCodeContent(strModel, strStation, "");//算總的all
                    dr[0] = "All Station";
                }
                else
                {
                    strcontent = GetLineCodeContent(strModel, strStation, ds.Tables[0].Rows[i][0].ToString());
                    dr[0] = ds.Tables[0].Rows[i][0].ToString();
                }
                  
                for (int j = 0; j < strcontent.Length; j++)
                {                    
                    dr[j+1] = strcontent[j].ToString();
                }
                if (dr[2].ToString() != "")
                {
                    firstrate = Convert.ToDouble(dr[2].ToString()) / Convert.ToDouble(dr[1].ToString());
                    dr[10] = Convert.ToString(Convert.ToDouble(dr[2].ToString()) / Convert.ToDouble(dr[1].ToString()) * 100) + "%";
                }
                else
                {
                    firstrate = 0.0;
                    dr[10] = "0%";
                }
                
                double finalpass = 0.0;
                if (dr[2].ToString() != "")
                {
                    finalpass = Convert.ToDouble(dr[2].ToString());
                }
                if (i != ds.Tables[0].Rows.Count)//最終通過率按本線上的失敗后的算
                {
                    finalpass += FindLineFinal(strModel, strStation, ds.Tables[0].Rows[i][0].ToString());
                }
                else
                {
                    if (dr[5].ToString() != "")
                    {
                        finalpass += Convert.ToDouble(dr[5].ToString());
                    }
                    if (dr[8].ToString() != "")
                    {
                        finalpass += Convert.ToDouble(dr[8].ToString());
                    }
                }
                double totalinput = 0.0;// Convert.ToDouble(dr[1].ToString()) + Convert.ToDouble(dr[4].ToString()) + Convert.ToDouble(dr[7].ToString());
                
                 
                if (dr[1].ToString() != "")
                {
                    totalinput = Convert.ToDouble(dr[1].ToString());
                }
                //if (dr[4].ToString() != "")
                //{
                //    totalinput += Convert.ToDouble(dr[4].ToString());
                //}
                //if (dr[7].ToString() != "")
                //{
                //    totalinput += Convert.ToDouble(dr[7].ToString());
                //}
                finalrate = finalpass / totalinput;
                dr[11] = Convert.ToString((finalpass/totalinput)*100)+"%";

                dr[12] = Convert.ToString(( finalrate-firstrate)*100)+"%";
                //if (dr[2].ToString() != "")
                //{
                    
                //    dr[12] = Convert.ToString(((finalpass - Convert.ToDouble(dr[2].ToString())) / totalinput) * 100) + "%";
                //}
                //else
                //{
                //    dr[12] = Convert.ToString((finalpass / totalinput) * 100) + "%";
                //}
                dtResult.Rows.Add(dr);
            }
        }
        return dtResult;
    }
    protected string[] GetLineCodeContent(string strModel, string strStation, string strLineCode)
    {
        DataSet ds = null;
        string[] strsql = GetSqlByLineCode(strModel, strStation, strLineCode);//得到9條查詢語句
        string strContent = string.Empty;

        for (int i = 0; i < strsql.Length; i++)
        {
            ds = ClsGlobal.objDataConnect.DataQuery(strsql[i].ToString());
            if (null != ds && ds.Tables[0].Rows.Count > 0)
            {
                if (strContent == "")
                {
                    strContent = ds.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    strContent += "," + ds.Tables[0].Rows.Count.ToString();
                }
            }
            else
            {
                if (strContent == "")
                {
                    strContent = "";
                }
                else
                {
                    strContent += ",";
                }
            }
        }

        string[] strResult = strContent.Split(',');
        return strResult;
    }

    protected string[] GetSqlByLineCode(string strModel, string strStation, string strLineCode)
    {
        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        string strProductID = "PRODUCT_ID";
        string strStaioncode = string.Empty;
        string strStationName = string.Empty;
        string strSql = "";
         
        string strWhere = "";
         
        //DataTime 范圍
        strWhere = strWhere + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

        //LINE
        //if (!strLineCode.ToString().Equals(""))
        //    strWhere = strWhere + " AND UPPER(line_code) = " + ClsCommon.GetSqlString(strLineCode.ToString().ToUpper());
        if (!strLineCode.ToString().Equals(""))
            strLineCode = " AND UPPER(line_code) = " + ClsCommon.GetSqlString(strLineCode.ToString().ToUpper());
        switch (strStation)
        {

            case "B1":
                strWhere = strWhere + " AND STATION='B1'  ";
                strStaioncode = " AND STATION='B1'";
                strStationName = "STATION";
                break;
            case "B2":
                strWhere = strWhere + " AND STATION='B2'  ";
                strStaioncode = " AND STATION='B2'";
                strStationName = "STATION";
                break;
            case "B3":
                strWhere = strWhere + " AND STATION='B3'  ";
                strStaioncode = " AND STATION='B3'";
                strStationName = "STATION";
                break;
            case "B4":
                strWhere = strWhere + " AND STATION='B4'  ";
                strStaioncode = " AND STATION='B4'";
                strStationName = "STATION";
                break;
            case "A1":
                strWhere = strWhere + " AND STATION='A1'  ";
                strStaioncode = " AND STATION='A1'";
                strStationName = "STATION";
                break;
            case "A2":
                strWhere = strWhere + " AND STATION='A2'  ";
                strStaioncode = " AND STATION='A2'";
                strStationName = "STATION";
                break;
            case "A3":
                strWhere = strWhere + " AND STATION='A3'  ";
                strStaioncode = " AND STATION='A3'";
                strStationName = "STATION";
                break;
            case "A4":
                strWhere = strWhere + " AND STATION='A4'  ";
                strStaioncode = " AND STATION='A4'";
                strStationName = "STATION";
                break;
            case "A5":
                strWhere = strWhere + " AND STATION='A5'  ";
                strStaioncode = " AND STATION='A5'";
                strStationName = "STATION";
                break;
            case "GP":
                strWhere = strWhere + " AND STATION='GP'  ";
                strStaioncode = " AND STATION='GP'";
                strStationName = "STATION";
                break;

            case "GPS":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPS'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;


            case "GPSWL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPSWL'  ";
                strStaioncode = " AND STATION_CODE = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "OOB":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION='OOB'  ";
                    strStaioncode = " AND STATION='OOB'";
                    strStationName = "STATION";
                }
                break;
            case "CA_EDGE":
            case "PT_EDGE":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'EDGE' ";
                strStationName = "STATION";
                break;
            case "CA_SET 1":
            case "PT_SET 1":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'SET 1' ";
                strStationName = "STATION";

                break;
            case "FQC":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION = 'FQC' ";
                    strStaioncode = " AND STATION='FQC'";
                    strStationName = "STATION";
                }
                else
                    strWhere = strSql + " AND STATION_ID LIKE 'A_FO' ";
                break;
            case "LV":

                strWhere = strWhere + " AND UPPER(STATION_CODE) LIKE '" + strStation.Trim() + "%'";

                strStaioncode = " AND STATION_CODE='LV'";
                strStationName = "STATION_CODE";
                break;
            case "GSMWL":
            case "FQA":
            case "QA":
                strWhere = strWhere + " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "SIMLOCK":
                strStationName = "STATION_CODE";
                break;
            case "RE/DL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='D2'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            default:
                strWhere = strWhere + " AND UPPER(STATION_CODE) = " + ClsCommon.GetSqlString(strStation);
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;


        }

         string strOrder = " ASC";
        //核心語句

         strSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
             + strWhere.Substring(4) + ") WHERE RN =1 " + strLineCode; 
         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
              + strWhere.Substring(4) + ") WHERE RN =1 AND UPPER(SUBSTR(STATUS, 1, 1)) = 'P'" + strLineCode; 
         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
               + strWhere.Substring(4) + ") WHERE RN =1 AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F'" + strLineCode; 

         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
            + strWhere.Substring(4) + ") WHERE RN =2 " + strLineCode;
         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
              + strWhere.Substring(4) + ") WHERE RN =2 AND UPPER(SUBSTR(STATUS, 1, 1)) = 'P'" + strLineCode; 
         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
               + strWhere.Substring(4) + ") WHERE RN =2 AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F'" + strLineCode; 

         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
            + strWhere.Substring(4) + ") WHERE RN =3 " + strLineCode;
         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
              + strWhere.Substring(4) + ") WHERE RN =3 AND UPPER(SUBSTR(STATUS, 1, 1)) = 'P'" + strLineCode; 
         strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
               + strWhere.Substring(4) + ") WHERE RN =3 AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F'" + strLineCode;

         string[] strAllSql = strSql.Split('$');
        return strAllSql;
    }

    protected StringBuilder CreateRetestFirstRow(StringBuilder sb)
    {
        sb.Append("<table width='100%' style='collapse;BORDER-STYLE: solid;' border='1px' bordercolor='#000000'>");
        sb.Append("<tr><td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;");
        sb.Append("' align='center' width='100'>");
        sb.Append("Station Group</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;' align='center' colSpan='4' width='160'>Retest Symptom</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;' align='center' width='40'>Retest Rate</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;' align='center'width='50' >Station LineID</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;' align='center' width='40'>Input</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;' align='center' width='40'>Retest Qty</td>");
        sb.Append("<td style='font-family:Arial,Helvetica,sans-serif;font-size: 17px; font-weight: bold;background-color: #33ff66;' align='center' width='40'>Retest Rate</td>");
        sb.Append("</tr></table>");
        
        return sb;
    }
    protected DataTable GetRetestStationContent(string strModel, string strStation,string strErrorMsg)
    {
        DataTable dtResult = new DataTable();
        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "StationLineID";
        dtResult.Columns.Add(dc1);
        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "Input";
        dtResult.Columns.Add(dc2);
        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "RetestQty";
        dtResult.Columns.Add(dc3);
        DataColumn dc4 = new DataColumn();
        dc4.ColumnName = "RetestRate";
        dtResult.Columns.Add(dc4);
             
        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        string strWhere = "";

        //DataTime 范圍
        strWhere = strWhere + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

        string strSql = "SELECT distinct(line_code) FROM  " + strModel + ".PRODUCT_HISTORY_V WHERE STATION_CODE = '" + strStation + "' and ERROR_MSG='" + strErrorMsg+"' and " + strWhere.Substring(4) + "ORDER BY line_code ";
        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql.ToString());//查找線別
        DataTable dtTemp = null;
        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)//要加條總的就加一
            {
                 
                //if (i == ds.Tables[0].Rows.Count)
                //{
                //    dtTemp = GetRetestLineCodeContent(strModel, strStation, "", strErrorMsg);//算總的all
                    
                //}
                //else
                //{
                    dtTemp = GetRetestLineCodeContent(strModel, strStation, ds.Tables[0].Rows[i][0].ToString(), strErrorMsg);
                     
                //}

                for (int j = 0; j < dtTemp.Rows.Count; j++)
                {
                    DataRow dr = dtResult.NewRow();
                    dr[0] = dtTemp.Rows[j][0].ToString();
                    dr[1] = dtTemp.Rows[j][1].ToString();
                    dr[2] = dtTemp.Rows[j][2].ToString();
                    dr[3] = dtTemp.Rows[j][3].ToString();
                    //dr[4] = dtTemp.Rows[j][4].ToString();
                    dtResult.Rows.Add(dr);
                }                
                 
               
            }
        }
        return dtResult;
    }
    protected DataTable GetRetestLineCodeContent(string strModel, string strStation, string strLineCode, string strErrorMsg)
    {
        DataSet ds = null;
        DataTable dtResult = new DataTable();
        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "StationLineID";
        dtResult.Columns.Add(dc1);         
        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "Input";
        dtResult.Columns.Add(dc2);
        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "RetestQty";
        dtResult.Columns.Add(dc3);
        DataColumn dc4 = new DataColumn();
        dc4.ColumnName = "RetestRate";
        dtResult.Columns.Add(dc4);

        string[] strsql = GetRetestSqlByLineCode(strModel, strStation, strLineCode, strErrorMsg);//得到2條查詢語句
        int total = 0;
        int failnumber = 0;
        ds = ClsGlobal.objDataConnect.DataQuery(strsql[0].ToString());
        if (null != ds && ds.Tables[0].Rows.Count > 0)
        { 
            total = ds.Tables[0].Rows.Count;            
        }
        else
        {
            return null;
        }
        ds = ClsGlobal.objDataConnect.DataQuery(strsql[1].ToString());
        string strError = string.Empty;
        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            //failnumber = ds.Tables[0].Rows.Count;
            //計算本線上失敗后是否Pass的,不管在本線還是其他線
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (isPIDPass(strModel, strStation, ds.Tables[0].Rows[i][3].ToString()))
                {
                    failnumber += 1;
                }
            }
        }
            //if (strLineCode == "")//全站情況
            //{
            //    DataRow dr = dtResult.NewRow();
            //    dr[0] = "All Station";
            //    dr[1] = "All Symptom Code";
            //    dr[2] = total.ToString();
            //    dr[3] = failnumber.ToString();
            //    dr[4] = Convert.ToString(Convert.ToInt32(((double)failnumber / total) * 100)) + "%";
            //    dtResult.Rows.Add(dr);
            //    return dtResult;
            //}
            //for (int j = 0; j < failnumber; j++)
            //{

            //    string strTempError_msg = ds.Tables[0].Rows[j][9].ToString();// +";";

            //    if (strError.IndexOf(strTempError_msg) == -1)
            //    {
            //        if (strError != "")
            //        {
            //            strError += ",";
            //        }
            //        strError += strTempError_msg;//取得error_msg的不同類型
            //    }
            //}
         
        //string[] strError_msg = strError.Split(',');
        //for (int j = 0; j < strError_msg.Length; j++)
        //{
            DataRow dr = dtResult.NewRow();
            dr[0] = strLineCode;
            //dr[1] = strError_msg[j].ToString();
            //int iTemp = 0;

            //for (int k = 0; k < failnumber; k++)
            //{
            //    string strTempError_msg = ds.Tables[0].Rows[j][9].ToString();

            //    if (strTempError_msg == dr[1].ToString())
            //    {
            //        iTemp++;                     
            //    }
            //}
            dr[1] = total.ToString();
            dr[2] = failnumber.ToString();
             
            if (total != 0)
            {
                dr[3] = Convert.ToString(Convert.ToInt32(((double)failnumber / total) * 100)) + "%";
            }
            //if (iTemp != 0)
            //{
                dtResult.Rows.Add(dr);
           // }
        //}
        //DataRow drr = dtResult.NewRow();
        //drr[0] = strLineCode;
        //drr[1] = "All Symptom Code";
        //drr[2] = total.ToString();
        //drr[3] = failnumber.ToString();
        //drr[4] = Convert.ToString(Convert.ToInt32(((double)failnumber / total) * 100)) + "%";
        //dtResult.Rows.Add(drr);
        return dtResult;
    }
    protected string[] GetRetestSqlByLineCode(string strModel, string strStation, string strLineCode, string strErrorMsg)
    {
        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        string strProductID = "PRODUCT_ID";
        string strStaioncode = string.Empty;
        string strStationName = string.Empty;
        string strSql = "";

        string strWhere = "";

        //DataTime 范圍
        strWhere = strWhere + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

        switch (strStation)
        {

            case "B1":
                strWhere = strWhere + " AND STATION='B1'  ";
                strStaioncode = " AND STATION='B1'";
                strStationName = "STATION";
                break;
            case "B2":
                strWhere = strWhere + " AND STATION='B2'  ";
                strStaioncode = " AND STATION='B2'";
                strStationName = "STATION";
                break;
            case "B3":
                strWhere = strWhere + " AND STATION='B3'  ";
                strStaioncode = " AND STATION='B3'";
                strStationName = "STATION";
                break;
            case "B4":
                strWhere = strWhere + " AND STATION='B4'  ";
                strStaioncode = " AND STATION='B4'";
                strStationName = "STATION";
                break;
            case "A1":
                strWhere = strWhere + " AND STATION='A1'  ";
                strStaioncode = " AND STATION='A1'";
                strStationName = "STATION";
                break;
            case "A2":
                strWhere = strWhere + " AND STATION='A2'  ";
                strStaioncode = " AND STATION='A2'";
                strStationName = "STATION";
                break;
            case "A3":
                strWhere = strWhere + " AND STATION='A3'  ";
                strStaioncode = " AND STATION='A3'";
                strStationName = "STATION";
                break;
            case "A4":
                strWhere = strWhere + " AND STATION='A4'  ";
                strStaioncode = " AND STATION='A4'";
                strStationName = "STATION";
                break;
            case "A5":
                strWhere = strWhere + " AND STATION='A5'  ";
                strStaioncode = " AND STATION='A5'";
                strStationName = "STATION";
                break;
            case "GP":
                strWhere = strWhere + " AND STATION='GP'  ";
                strStaioncode = " AND STATION='GP'";
                strStationName = "STATION";
                break;

            case "GPS":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPS'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;


            case "GPSWL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='GPSWL'  ";
                strStaioncode = " AND STATION_CODE = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "OOB":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION='OOB'  ";
                    strStaioncode = " AND STATION='OOB'";
                    strStationName = "STATION";
                }
                break;
            case "CA_EDGE":
            case "PT_EDGE":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'EDGE' ";
                strStationName = "STATION";
                break;
            case "CA_SET 1":
            case "PT_SET 1":
                strWhere = strWhere + " AND UPPER(EXTRAOPTION) = 'SET 1' ";
                strStationName = "STATION";

                break;
            case "FQC":
                if (strModel.Equals("GLM"))
                {
                    strWhere = strWhere + " AND STATION = 'FQC' ";
                    strStaioncode = " AND STATION='FQC'";
                    strStationName = "STATION";
                }
                else
                    strWhere = strSql + " AND STATION_ID LIKE 'A_FO' ";
                break;
            case "LV":

                strWhere = strWhere + " AND UPPER(STATION_CODE) LIKE '" + strStation.Trim() + "%'";

                strStaioncode = " AND STATION_CODE='LV'";
                strStationName = "STATION_CODE";
                break;
            case "GSMWL":
            case "FQA":
            case "QA":
                strWhere = strWhere + " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            case "SIMLOCK":
                strStationName = "STATION_CODE";
                break;
            case "RE/DL":
                strWhere = strWhere + " AND UPPER(STATION_CODE)='D2'  ";
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;
            default:
                strWhere = strWhere + " AND UPPER(STATION_CODE) = " + ClsCommon.GetSqlString(strStation);
                strStaioncode = " AND UPPER(STATION_CODE) = '" + strStation.Trim() + "'";
                strStationName = "STATION_CODE";
                break;


        }

        string strOrder = " ASC";
        //核心語句
        if (!strLineCode.ToString().Equals(""))
        {
            strLineCode = " UPPER(line_code) = " + ClsCommon.GetSqlString(strLineCode.ToString().ToUpper());// +" and ERROR_MSG='" + strErrorMsg + "'";

            strSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
                + strWhere.Substring(4) + ") WHERE " + strLineCode;
            strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
                 + strWhere.Substring(4) + ") WHERE " + strLineCode + " and ERROR_MSG='" + strErrorMsg+"'"+" AND UPPER(SUBSTR(STATUS, 1, 1)) = 'F'";
        }
        else
        {
            if (!strErrorMsg.ToString().Equals(""))
            {
                strSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
               + strWhere.Substring(4) +" and ERROR_MSG='" + strErrorMsg+ "')  where RN=1" ;
                strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
                 + strWhere.Substring(4) +") WHERE UPPER(SUBSTR(STATUS, 1, 1)) = 'F'";
            }
            else
            {
                strSql = "SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
               + strWhere.Substring(4)+ ") ";// +" and ERROR_MSG='" + strErrorMsg ;
                strSql += "$SELECT distinct  * FROM(SELECT A.*,ROW_NUMBER() OVER (PARTITION BY " + strProductID + "  ORDER BY PDDATE" + strOrder + " ) RN FROM " + strModel + ".PRODUCT_HISTORY_V  A WHERE "
                 + strWhere.Substring(4) +") WHERE UPPER(SUBSTR(STATUS, 1, 1)) = 'F'";
            }
            

        }
        string[] strAllSql = strSql.Split('$');
        return strAllSql;
    }
    protected DataTable GetRetestSymptom(string strModel, string strStation)
    {
        DataTable dtResult = new DataTable();
        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "RetestSymptom";
        dtResult.Columns.Add(dc1);
        

        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        string strWhere = "";

        //DataTime 范圍
        strWhere = strWhere + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";

        string strSql = "SELECT distinct(error_msg) FROM  " + strModel + ".PRODUCT_HISTORY_V WHERE STATION_CODE = '" + strStation + "' and " + strWhere.Substring(4) + "ORDER BY error_msg ";
        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql.ToString());//查找線別
        
        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)//之所以加一就是要加條總的
            {
                DataRow dr = dtResult.NewRow();
                dr[0] = ds.Tables[0].Rows[i][0].ToString();
                dtResult.Rows.Add(dr);
            }
        }
        return dtResult;
    }
    protected int GetStationColNumber(string strModel, string strStation, DataTable dt)
    {
        int total = 0;
        DataTable dtcontent = null;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dtcontent = GetRetestStationContent(strModel, strStation, dt.Rows[i][0].ToString());
            if (dtcontent != null && dtcontent.Rows.Count > 0)
            {
                total += dtcontent.Rows.Count;
            }
        }
        return total;
    }
    //得到站的總Input數量和失敗后重測通過的數量
    protected int[] GetInputTotalNumber(string strModel, string strStation)
    {
        int[] result = new int[2];
        string[] strSql = GetRetestSqlByLineCode(strModel, strStation, "", "");
        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql[0].ToString());//查找總的

        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows.Count;
        }
        ds = ClsGlobal.objDataConnect.DataQuery(strSql[1].ToString());//查找失敗的
        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            //查找失敗后重測Pass的數量
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (isPIDPass(strModel, strStation, ds.Tables[0].Rows[i][3].ToString()))
                {
                    result[1] += 1;
                }
            }
            //result[1] = ds.Tables[0].Rows.Count;
        }

        return result;
    }

    //查找本條線上失敗后最終的Pass情況
    protected int FindLineFinal(string strModel, string strStation, string strLine)
    {
        int number = 0;
        DataSet ds = null;
        string strPID = string.Empty;
        //先查線上第一次失敗情況,記錄下PID
        string[] strSql = GetSqlByLineCode(strModel, strStation, strLine);
        ds = ClsGlobal.objDataConnect.DataQuery(strSql[2].ToString());
        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    strPID = ds.Tables[0].Rows[i][3].ToString();
                }
                else
                {
                    strPID += "," + ds.Tables[0].Rows[i][3];
                }
            }
        }
        string[] strFailPID = strPID.Split(',');
        //再查失敗的PID最終有沒有通過
        for (int j = 0; j < strFailPID.Length; j++)
        {
            if (isPIDPass(strModel, strStation, strFailPID[j]))
            {
                number += 1;
            }
        }
        
        return number;
    }
    //判斷PID是否最終通過
    protected bool isPIDPass(string strModel, string strStation, string strPID)
    {
        string strStartDate = tbStartDate.DateTextBox.Text;
        string strEndDate = tbEndDate.DateTextBox.Text;
        string strWhere = "";

        //DataTime 范圍
        strWhere = strWhere + " AND PDDATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') "
            + " AND TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI') ";
        strWhere = strWhere + " AND PRODUCT_ID = " + ClsCommon.GetSqlString(strPID)+" AND STATUS='P'";
        string strSql = "SELECT status FROM  " + strModel + ".PRODUCT_HISTORY_V WHERE STATION_CODE = '" + strStation + "' " + strWhere.ToString();
        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql.ToString()); 

        if (null != ds && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        return false;
    }
    //排序
    protected string[] SortErrorMsg(string strModel, string strRouteStation, DataTable dt)
    {
        int[] count = new int[dt.Rows.Count];
        string[] strdt = new string[dt.Rows.Count];

        for(int m=0;m<count.Length;m++)
        {
            count[m] = 0;
        }
       
        for (int i = 0; i < dt.Rows.Count; i++)//Error_Msg
        {
            strdt[i] = dt.Rows[i][0].ToString();
            if (strdt[i] == "")
            {
                count[i] = 0;
                continue;
            }
            string[] strSql = GetRetestSqlByLineCode(strModel, strRouteStation, "", dt.Rows[i][0].ToString());
            DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql[0].ToString());
            if (null != ds && ds.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)//PID
                {
                    if (isPIDPass(strModel, strRouteStation, ds.Tables[0].Rows[j][3].ToString()))
                    {
                        count[i] += 1;
                    }
                }
            }
        }

        //排序開始,采用平行冒泡排序法
        for (int u = 0; u < count.Length - 1; u++)
        {
            for (int v = 0; v < count.Length - u - 1; v++)
            {
                 
                if (count[v] < count[v+1])
                {
                    int temp = count[v];
                    count[v] = count[v+1];
                    count[v + 1] = temp;

                    string strtemp = strdt[v].ToString();
                    strdt[v]  = strdt[v + 1].ToString();
                    strdt[v + 1]  = strtemp;
                }
            }
        }
        //if (strdt[0] == "")
        //{
        //    for (int w = 0; w < strdt.Length-1; w++)
        //    {
        //        strdt[w] = strdt[w + 1];
        //    }
        //    strdt[strdt.Length - 1] = "";
        //}
        return strdt;

    }
}
