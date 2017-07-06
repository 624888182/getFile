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

public partial class Boundary_wfrmRouterManage : System.Web.UI.UserControl
{
    public static int Row = 1;
    public static int ReRow = 1;
    public static int index = 0;
    public static int flag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(WFrmYieldRate));
        // 在這裡放置使用者程式碼以初始化網頁
        if (!IsPostBack)
        {
            MultiLanguage();
            BindModel();
            BindStation();
            SessionRemove();
            ButtonSave.Attributes.Add("onclick", "return confirm('你确定要保存所要修改的路由吗？');");
            ButtonStartCopy.Attributes.Add("onclick", "return confirm('你確認要COPY該路由吗？');");

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).Items.Add("");
                ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).Items.FindByText("").Selected = true;
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add("");
                ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.FindByText("").Selected = true;
            }

            Row = 1;
            ReRow = 1;
        }
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
        ddlModel.Items.Add("");
        ddlModel.Items.FindByText("").Selected = true;
    }

    private void BindPpart(string Model)
    {
        string sql = "SELECT distinct PART FROM SHP.CMCS_SFC_WO_V WHERE MODEL LIKE '" + Model + "%' order by PART ASC";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DropDownListSPart.DataTextField = "PART";
            DropDownListSPart.DataValueField = "PART";
            DropDownListSPart.DataSource = dt.DefaultView;
            DropDownListSPart.DataBind();
        }
        DropDownListSPart.Items.Add("");
        DropDownListSPart.Items.FindByText("").Selected = true;

    }

    private void MultiLanguage()
    {
        ViewState["ErrorDate"] = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        ViewState["WONotExist"] = (String)GetGlobalResourceObject("SFCQuery", "WONotExist");//rm.GetString("WONotExist");
    }

    private void BindStation()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Station_Name");

        for (int i = 0; i < 10; i++)
        {
            DataRow r = dt.NewRow();

            dt.Rows.Add(r);
        }

        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Station_Name");
        dt1.Columns.Add("Flow_Type");

        for (int i = 0; i < 10; i++)
        {
            DataRow r = dt1.NewRow();
            dt1.Rows.Add(r);
        }

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();

            GridView2.DataSource = dt1;
            GridView2.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前還沒有創建該路由！！');</script>");
            return;
        }
    }

    private void SessionRemove()
    {
        string sql = "SELECT STATION_DESC STATION_NAME FROM SFC.SFC_PRODUCTION_STATIONS";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Session.Remove(Session.SessionID + dt.Rows[i][0].ToString());
            }
        }
        Session[Session.SessionID + "RowID"] = null;
    }

    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPpart(ddlModel.SelectedItem.Text);
        SessionRemove();
    }

    protected void ButtonFind_Click(object sender, EventArgs e)
    {
        string PPart = DropDownListSPart.SelectedItem.Text.Trim();
        string sql1 = "";
        if (PPart == "")
        {
            sql1 = "SELECT * FROM SFC.SFC_ROUTING_HEADERS WHERE MODEL_NAME ='" + ddlModel.SelectedItem.Text + "' AND PART_NUMBER IS NULL";
            if (TextBoxOrder.Text.Trim() != "")
            {
                sql1 += " AND WORK_ORDER_NUMBER='" + TextBoxOrder.Text.Trim() + "'";
            }
            else
            {
                sql1 += " AND WORK_ORDER_NUMBER IS NULL";
            }
            if (ddlLine.SelectedItem.Text.Trim() != "")
            {
                sql1 += " AND LINE_NUMBER='" + ddlLine.SelectedItem.Text.Trim() + "'";
            }
            else
            {
                sql1 += " AND LINE_NUMBER IS NULL";
            }
        }
        else
        {
            sql1 = "SELECT * FROM SFC.SFC_ROUTING_HEADERS WHERE MODEL_NAME like '" + ddlModel.SelectedItem.Text + "%' AND PART_NUMBER='" + PPart + "'";

            if (TextBoxOrder.Text.Trim() != "")
            {
                sql1 += " AND WORK_ORDER_NUMBER='" + TextBoxOrder.Text.Trim() + "'";
            }
            else
            {
                sql1 += " AND WORK_ORDER_NUMBER IS NULL";
            }
            if (ddlLine.SelectedItem.Text.Trim() != "")
            {
                sql1 += " AND LINE_NUMBER='" + ddlLine.SelectedItem.Text.Trim() + "'";
            }
            else
            {
                sql1 += " AND LINE_NUMBER IS NULL";
            }
        }

        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sql1).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            DropDownListSPart.SelectedItem.Text = dt1.Rows[0][2].ToString();
            TextBoxOrder.Text = dt1.Rows[0][3].ToString();
            ddlLine.SelectedItem.Text = dt1.Rows[0][4].ToString();
            TextBoxDescription.Text = dt1.Rows[0][5].ToString();
            TextBoxRouteID.Text = dt1.Rows[0][0].ToString();
            tbInvalidDate.DateTextBox.Text = dt1.Rows[0][7].ToString();
            if (TextBoxRouteID.Text.Trim() != "")
            {
                string sql = "SELECT STATION_NAME,DISABLE_FLAG FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text + " order by STATION_SEQUENCE_ID";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ((Label)(GridView1.Rows[i].FindControl("LabelID"))).Text = dt.Rows[i][0].ToString();
                        string StationName = GetStationName(dt.Rows[i][0].ToString());
                        ((DropDownList)(GridView1.Rows[i].FindControl("DropDownListName"))).Items.FindByText(StationName).Selected = true; ;
                        string temp2 = dt.Rows[i][1].ToString();
                        if (temp2 == "Y")
                        {
                            ((CheckBox)(GridView1.Rows[i].FindControl("CheckBoxDisable"))).Checked = true;
                        }
                        ((DropDownList)(GridView1.Rows[i].FindControl("DropDownListName"))).Enabled = false;

                        string StationFlowID = GetStationFlowID(TextBoxRouteID.Text, GetStationValue(StationName));
                        sql = "SELECT STATION_NAME,FLOW_TYPE,DISABLE_FLAG FROM SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + StationFlowID;
                        DataTable flows = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                        string temp = "";
                        for (int k = 0; k < flows.Rows.Count; k++)
                        {
                            string Flow = GetStationName(flows.Rows[k][0].ToString());
                            string Type = flows.Rows[k][1].ToString();
                            string Disable = flows.Rows[k][2].ToString();
                            if (Disable == "Y")
                                Disable = "True";
                            else
                                Disable = "False";
                            temp += Flow + "," + Type + "," + Disable + ",";
                        }
                        Session[Session.SessionID + StationName] = temp;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前還沒有創建該路由！！');</script>");
                    return;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前還沒有創建該路由！！');</script>");
                return;
            }
        }
        else
        {
            // TextBoxOrder.Text = "";
            TextBoxDescription.Text = "";
            TextBoxRouteID.Text = "";
            tbInvalidDate.DateTextBox.Text = "";
            BindStation();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).Items.Add("");
                ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).Items.FindByText("").Selected = true;
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add("");
                ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.FindByText("").Selected = true;
            }
            Row = 1;
            ReRow = 1;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前還沒有創建該路由！！');</script>");
            return;
        }
    }

    public string GetStationName(string value)
    {
        string sql = "Select STATION_DESC STATION_NAME from SFC.SFC_PRODUCTION_STATIONS where STATION_CODE='" + value.Trim() + "'";
        string id = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0].ToString();
        return id;
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

    //protected void ButtonSave_Click(object sender, EventArgs e)
    //{
    //    if (Session[Session.SessionID + "RowID"] != null)
    //    {
    //        string temp = "";
    //        int rowid = Convert.ToInt32(Session[Session.SessionID + "RowID"]);
    //        string priorstation = ((DropDownList)GridView1.Rows[rowid].FindControl("DropDownListName")).SelectedItem.Text;
    //        for (int i = 0; i < GridView2.Rows.Count; i++)
    //        {
    //            if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
    //            {

    //                string Flow = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text;

    //                string Type = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).SelectedItem.Text;

    //                string Disable = ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked.ToString();

    //                temp += Flow + "," + Type + "," + Disable + ",";
    //            }
    //        }
    //        Session[Session.SessionID + priorstation] = temp;

    //        Session[Session.SessionID + "RowID"] = null;
    //    }
    //    if (TextBoxRouteID.Text != "")
    //    {
    //        try
    //        {
    //            string cmd = "SELECT ROUTING_SEQUENCE_ID,STATION_SEQUENCE_ID,STATION_NAME,DISABLE_FLAG,CREATION_DATE,LAST_UPDATED_BY FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text + " order by STATION_SEQUENCE_ID";

    //            DataTable dt = ClsGlobal.objDataConnect.DataQuery(cmd).Tables[0];
    //            if (dt.Rows.Count > 0)
    //            {
    //                for (int i = 0; i < dt.Rows.Count; i++)
    //                {
    //                    string time = Convert.ToDateTime(dt.Rows[i][4].ToString()).Year + "/" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Month + "/" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Day + " " + Convert.ToDateTime(dt.Rows[i][4].ToString()).Hour + ":" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Minute + ":" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Second;
    //                    cmd = "Insert into SFC.SFC_ROUTING_STATION_HISTORY(ROUTING_SEQUENCE_ID,STATION_SEQUENCE_ID,STATION_NAME,DISABLE_FLAG,CREATION_DATE,CREATED_BY) VALUES (" + dt.Rows[i][0].ToString() + "," + dt.Rows[i][1].ToString() + ",'" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "',to_date('" + time + "','YYYY/MM/DD HH24:MI:SS'),'" + dt.Rows[i][5].ToString() + "')";
    //                    ClsGlobal.objDataConnect.DataExecute(cmd);
    //                    cmd = "SELECT '" + TextBoxRouteID.Text + "' ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID,STATION_NAME,FLOW_TYPE,DISABLE_FLAG,CREATION_DATE,LAST_UPDATED_BY From SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + dt.Rows[i][1].ToString();
    //                    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(cmd).Tables[0];
    //                    for (int k = 0; k < dt1.Rows.Count; k++)
    //                    {
    //                        time = Convert.ToDateTime(dt1.Rows[k][5].ToString()).Year + "/" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Month + "/" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Day + " " + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Hour + ":" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Minute + ":" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Second;
    //                        cmd = "Insert into SFC.SFC_ROUTING_FLOW_HISTORY(ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY) VALUES (" + dt1.Rows[k][0].ToString() + "," + dt1.Rows[k][1].ToString() + ",'" + dt1.Rows[k][2].ToString() + "','" + dt1.Rows[k][3].ToString() + "','" + dt1.Rows[k][4].ToString() + "',to_date('" + time + "','YYYY/MM/DD HH24:MI:SS'),'" + dt1.Rows[k][6].ToString() + "')";
    //                        ClsGlobal.objDataConnect.DataExecute(cmd);

    //                    }
    //                    cmd = "Delete From SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + dt.Rows[i][1].ToString();
    //                    ClsGlobal.objDataConnect.DataExecute(cmd);

    //                }
    //                cmd = "Delete FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text;
    //                ClsGlobal.objDataConnect.DataExecute(cmd);
    //                cmd = "Delete FROM SFC.SFC_ROUTING_HEADERS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text;
    //                ClsGlobal.objDataConnect.DataExecute(cmd);
    //            }

    //        }
    //        catch
    //        {

    //        }
    //    }
    //    try
    //    {
    //        string sql = "Select SFC.SFC_ROUTING_HEADERS_S.nextval from dual";
    //        int MaxHeaderIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

    //        sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
    //        int MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;
    //        string time = "";
    //        if (tbInvalidDate.DateTextBox.Text.Trim() != "")
    //            time = Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Year + "/" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Month + "/" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Day + " " + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Hour + ":" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Minute + ":" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Second;
    //        else
    //            time = tbInvalidDate.DateTextBox.Text + ":00";

    //        if (this.tbInvalidDate.DateTextBox.Text.Trim() == "")
    //        {
    //            sql = "Insert into SFC.SFC_ROUTING_HEADERS"
    //                           + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                           + " Values"
    //                           + " (" + MaxHeaderIndex + ", '" + ddlModel.SelectedItem.Text + "', '" + DropDownListSPart.SelectedItem.Text + "', '" + TextBoxOrder.Text + "', '" + ddlLine.SelectedItem.Text + "', '" + TextBoxDescription.Text + "'," + MaxStationIndex + ", sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
    //        }
    //        else
    //        {

    //            sql = "Insert into SFC.SFC_ROUTING_HEADERS"
    //                             + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, DISABLE_DATE, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                             + " Values"
    //                             + " (" + MaxHeaderIndex + ", '" + ddlModel.SelectedItem.Text + "', '" + DropDownListSPart.SelectedItem.Text + "', '" + TextBoxOrder.Text + "', '" + ddlLine.SelectedItem.Text + "', '" + TextBoxDescription.Text + "'," + MaxStationIndex + ", TO_DATE('" + time + "', 'YYYY/MM/DD HH24:MI:SS'), sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
    //        }
    //        ClsGlobal.objDataConnect.DataExecute(sql);

    //        for (int i = 0; i < GridView1.Rows.Count; i++)
    //        {
    //            if (((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text.Trim() != "")
    //            {
    //                sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
    //                MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

    //                string station = ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text;
    //                string stationvalue = GetStationValue(station);

    //                string Disable = ((CheckBox)GridView1.Rows[i].FindControl("CheckBoxDisable")).Checked.ToString();
    //                if (Disable == "True")
    //                    Disable = "Y";
    //                else
    //                    Disable = "N";
    //                if (Session[Session.SessionID + station] != null)
    //                {
    //                    sql = "Insert into SFC.SFC_ROUTING_STATIONS"
    //                                    + " (ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                                    + " Values"
    //                                    + " (" + MaxHeaderIndex + ", " + MaxStationIndex + ", '" + stationvalue + "', '" + Disable + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
    //                    ClsGlobal.objDataConnect.DataExecute(sql);
    //                    if (Session[Session.SessionID + station] != null)
    //                    {
    //                        string temp = Session[Session.SessionID + station].ToString();
    //                        if (temp.Trim() != "")
    //                        {
    //                            temp = temp.Substring(0, temp.Length - 1);
    //                            string[] value = temp.Split(',');
    //                            for (int j = 0; j < value.Length; j = j + 3)
    //                            {
    //                                if (value[j + 2] == "True")
    //                                    value[j + 2] = "Y";
    //                                else
    //                                    value[j + 2] = "N";

    //                                value[j] = GetStationValue(value[j]);

    //                                sql = "Insert into SFC.SFC_ROUTING_STATION_FLOWS"
    //                                           + " (STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                                           + " Values"
    //                                           + " (" + MaxStationIndex + ", '" + value[j].Trim() + "', '" + value[j + 1].Trim() + "', '" + value[j + 2].Trim() + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
    //                                ClsGlobal.objDataConnect.DataExecute(sql);

    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Information", "<script language='javascript'>alert('恭喜，保存路由成功!!!');</script>");
    //        ButtonFind_Click(this, null);
    //    }
    //    catch
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
    //        return;
    //    }
    //}

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (Session[Session.SessionID + "RowID"] != null)
        {
            string temp = "";
            int rowid = Convert.ToInt32(Session[Session.SessionID + "RowID"]);
            string priorstation = ((DropDownList)GridView1.Rows[rowid].FindControl("DropDownListName")).SelectedItem.Text;
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
                {

                    string Flow = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text;

                    string Type = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).SelectedItem.Text;

                    string Disable = ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked.ToString();

                    temp += Flow + "," + Type + "," + Disable + ",";
                }
            }
            Session[Session.SessionID + priorstation] = temp;

            Session[Session.SessionID + "RowID"] = null;
        }
        if (TextBoxRouteID.Text != "")
        {
            string cmd1 = "SELECT ROUTING_SEQUENCE_ID,STATION_SEQUENCE_ID,STATION_NAME,DISABLE_FLAG,CREATION_DATE,LAST_UPDATED_BY FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text + " order by STATION_SEQUENCE_ID";

            DataTable dt = ClsGlobal.objDataConnect.DataQuery(cmd1).Tables[0];

            if (dt.Rows.Count > 0)
            {
                //OracleConnection conn = new OracleConnection("Data Source=TYSFC;Persist Security Info=True;User ID=sfc;Password=sfc;Unicode=True");
                string strcon = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
                OracleConnection conn = new OracleConnection(strcon);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                OracleTransaction mytrans1;
                mytrans1 = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = mytrans1;
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string time = Convert.ToDateTime(dt.Rows[i][4].ToString()).Year + "/" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Month + "/" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Day + " " + Convert.ToDateTime(dt.Rows[i][4].ToString()).Hour + ":" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Minute + ":" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Second;
                        string strsql1 = "Insert into SFC.SFC_ROUTING_STATION_HISTORY(ROUTING_SEQUENCE_ID,STATION_SEQUENCE_ID,STATION_NAME,DISABLE_FLAG,CREATION_DATE,CREATED_BY) VALUES (" + dt.Rows[i][0].ToString() + "," + dt.Rows[i][1].ToString() + ",'" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "',to_date('" + time + "','YYYY/MM/DD HH24:MI:SS'),'" + dt.Rows[i][5].ToString() + "')";
                        cmd.CommandText = strsql1;
                        cmd.ExecuteNonQuery();

                        string strsql0 = "SELECT '" + TextBoxRouteID.Text + "' ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID,STATION_NAME,FLOW_TYPE,DISABLE_FLAG,CREATION_DATE,LAST_UPDATED_BY From SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + dt.Rows[i][1].ToString();
                        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
                        for (int k = 0; k < dt1.Rows.Count; k++)
                        {
                            time = Convert.ToDateTime(dt1.Rows[k][5].ToString()).Year + "/" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Month + "/" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Day + " " + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Hour + ":" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Minute + ":" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Second;
                            string strsql2 = "Insert into SFC.SFC_ROUTING_FLOW_HISTORY(ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY) VALUES (" + dt1.Rows[k][0].ToString() + "," + dt1.Rows[k][1].ToString() + ",'" + dt1.Rows[k][2].ToString() + "','" + dt1.Rows[k][3].ToString() + "','" + dt1.Rows[k][4].ToString() + "',to_date('" + time + "','YYYY/MM/DD HH24:MI:SS'),'" + dt1.Rows[k][6].ToString() + "')";
                            cmd.CommandText = strsql2;
                            cmd.ExecuteNonQuery();

                        }
                        string strsql3 = "Delete From SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + dt.Rows[i][1].ToString();
                        cmd.CommandText = strsql3;
                        cmd.ExecuteNonQuery();
                    }

                    string strsql4 = "Delete FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text;
                    cmd.CommandText = strsql4;
                    cmd.ExecuteNonQuery();
                    string strsql5 = "Delete FROM SFC.SFC_ROUTING_HEADERS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text;
                    cmd.CommandText = strsql5;
                    cmd.ExecuteNonQuery();

                    try
                    {
                        string sql = "Select SFC.SFC_ROUTING_HEADERS_S.nextval from dual";
                        int MaxHeaderIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

                        sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
                        int MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;
                        string time = "";
                        if (tbInvalidDate.DateTextBox.Text.Trim() != "")
                            time = Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Year + "/" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Month + "/" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Day + " " + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Hour + ":" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Minute + ":" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Second;
                        else
                            time = tbInvalidDate.DateTextBox.Text + ":00";

                        if (this.tbInvalidDate.DateTextBox.Text.Trim() == "")
                        {
                            sql = "Insert into SFC.SFC_ROUTING_HEADERS"
                                           + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                           + " Values"
                                           + " (" + MaxHeaderIndex + ", '" + ddlModel.SelectedItem.Text + "', '" + DropDownListSPart.SelectedItem.Text + "', '" + TextBoxOrder.Text + "', '" + ddlLine.SelectedItem.Text + "', '" + TextBoxDescription.Text + "'," + MaxStationIndex + ", sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
                        }
                        else
                        {
                            sql = "Insert into SFC.SFC_ROUTING_HEADERS"
                                             + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, DISABLE_DATE, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                             + " Values"
                                             + " (" + MaxHeaderIndex + ", '" + ddlModel.SelectedItem.Text + "', '" + DropDownListSPart.SelectedItem.Text + "', '" + TextBoxOrder.Text + "', '" + ddlLine.SelectedItem.Text + "', '" + TextBoxDescription.Text + "'," + MaxStationIndex + ", TO_DATE('" + time + "', 'YYYY/MM/DD HH24:MI:SS'), sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
                        }
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            if (((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text.Trim() != "")
                            {
                                sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
                                MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

                                string station = ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text;
                                string stationvalue = GetStationValue(station);

                                string Disable = ((CheckBox)GridView1.Rows[i].FindControl("CheckBoxDisable")).Checked.ToString();
                                if (Disable == "True")
                                    Disable = "Y";
                                else
                                    Disable = "N";
                                if (Session[Session.SessionID + station] != null)
                                {
                                    sql = "Insert into SFC.SFC_ROUTING_STATIONS"
                                                    + " (ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                                    + " Values"
                                                    + " (" + MaxHeaderIndex + ", " + MaxStationIndex + ", '" + stationvalue + "', '" + Disable + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                    if (Session[Session.SessionID + station] != null)
                                    {
                                        string temp = Session[Session.SessionID + station].ToString();
                                        if (temp.Trim() != "")
                                        {
                                            temp = temp.Substring(0, temp.Length - 1);
                                            string[] value = temp.Split(',');
                                            for (int j = 0; j < value.Length; j = j + 3)
                                            {
                                                if (value[j + 2] == "True")
                                                    value[j + 2] = "Y";
                                                else
                                                    value[j + 2] = "N";

                                                value[j] = GetStationValue(value[j]);

                                                sql = "Insert into SFC.SFC_ROUTING_STATION_FLOWS"
                                                           + " (STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                                           + " Values"
                                                           + " (" + MaxStationIndex + ", '" + value[j].Trim() + "', '" + value[j + 1].Trim() + "', '" + value[j + 2].Trim() + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
                                                cmd.CommandText = sql;
                                                cmd.ExecuteNonQuery();

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        mytrans1.Commit();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Information", "<script language='javascript'>alert('恭喜，保存路由成功!!!');</script>");
                        ButtonFind_Click(this, null);
                    }
                    catch
                    {
                        try
                        {
                            mytrans1.Rollback();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
                            ButtonFind_Click(this, null);
                            return;
                        }
                        catch
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
                            return;
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
                    return;
                }
            }
        }
        else
        {

            try
            {
                string sql = "Select SFC.SFC_ROUTING_HEADERS_S.nextval from dual";
                int MaxHeaderIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

                sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
                int MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;
                string time = "";
                if (tbInvalidDate.DateTextBox.Text.Trim() != "")
                    time = Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Year + "/" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Month + "/" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Day + " " + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Hour + ":" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Minute + ":" + Convert.ToDateTime(tbInvalidDate.DateTextBox.Text).Second;
                else
                    time = tbInvalidDate.DateTextBox.Text + ":00";

                //OracleConnection conn1 = new OracleConnection("Data Source=TYSFC;Persist Security Info=True;User ID=sfc;Password=sfc;Unicode=True");
                string strcon1 = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
                OracleConnection conn1 = new OracleConnection(strcon1);
                conn1.Open();
                OracleCommand cmd1 = new OracleCommand();
                OracleTransaction mytrans2;
                mytrans2 = conn1.BeginTransaction();
                cmd1.Connection = conn1;
                cmd1.Transaction = mytrans2;
                try
                {

                    if (this.tbInvalidDate.DateTextBox.Text.Trim() == "")
                    {
                        sql = "Insert into SFC.SFC_ROUTING_HEADERS"
                                       + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                       + " Values"
                                       + " (" + MaxHeaderIndex + ", '" + ddlModel.SelectedItem.Text + "', '" + DropDownListSPart.SelectedItem.Text + "', '" + TextBoxOrder.Text + "', '" + ddlLine.SelectedItem.Text + "', '" + TextBoxDescription.Text + "'," + MaxStationIndex + ", sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
                    }
                    else
                    {
                        sql = "Insert into SFC.SFC_ROUTING_HEADERS"
                                         + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, DISABLE_DATE, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                         + " Values"
                                         + " (" + MaxHeaderIndex + ", '" + ddlModel.SelectedItem.Text + "', '" + DropDownListSPart.SelectedItem.Text + "', '" + TextBoxOrder.Text + "', '" + ddlLine.SelectedItem.Text + "', '" + TextBoxDescription.Text + "'," + MaxStationIndex + ", TO_DATE('" + time + "', 'YYYY/MM/DD HH24:MI:SS'), sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
                    }
                    cmd1.CommandText = sql;
                    cmd1.ExecuteNonQuery();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        if (((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text.Trim() != "")
                        {
                            sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
                            MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

                            string station = ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text;
                            string stationvalue = GetStationValue(station);

                            string Disable = ((CheckBox)GridView1.Rows[i].FindControl("CheckBoxDisable")).Checked.ToString();
                            if (Disable == "True")
                                Disable = "Y";
                            else
                                Disable = "N";
                            if (Session[Session.SessionID + station] != null)
                            {
                                sql = "Insert into SFC.SFC_ROUTING_STATIONS"
                                                + " (ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                                + " Values"
                                                + " (" + MaxHeaderIndex + ", " + MaxStationIndex + ", '" + stationvalue + "', '" + Disable + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
                                cmd1.CommandText = sql;
                                cmd1.ExecuteNonQuery();
                                if (Session[Session.SessionID + station] != null)
                                {
                                    string temp = Session[Session.SessionID + station].ToString();
                                    if (temp.Trim() != "")
                                    {
                                        temp = temp.Substring(0, temp.Length - 1);
                                        string[] value = temp.Split(',');
                                        for (int j = 0; j < value.Length; j = j + 3)
                                        {
                                            if (value[j + 2] == "True")
                                                value[j + 2] = "Y";
                                            else
                                                value[j + 2] = "N";

                                            value[j] = GetStationValue(value[j]);

                                            sql = "Insert into SFC.SFC_ROUTING_STATION_FLOWS"
                                                       + " (STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                                       + " Values"
                                                       + " (" + MaxStationIndex + ", '" + value[j].Trim() + "', '" + value[j + 1].Trim() + "', '" + value[j + 2].Trim() + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
                                            cmd1.CommandText = sql;
                                            cmd1.ExecuteNonQuery();

                                        }
                                    }
                                }
                            }
                        }
                    }
                    mytrans2.Commit();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Information", "<script language='javascript'>alert('恭喜，保存路由成功!!!');</script>");
                    ButtonFind_Click(this, null);
                }
                catch
                {
                    try
                    {
                        mytrans2.Rollback();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
                        ButtonFind_Click(this, null);
                        return;

                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
                        return;
                    }
                }
                finally
                {
                    conn1.Close();
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
                return;
            }
        }
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton btnHiddenPostButton = (LinkButton)e.Row.FindControl("LinkButton1");
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标经过时，行背景色变 
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
            //鼠标移出时，行背景色变 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            //如果是绑定数据行 
        }
    }

    public string GetStationFlowID(string routeid, string station)
    {
        string sql = "Select STATION_SEQUENCE_ID from SFC.SFC_ROUTING_STATIONS where STATION_NAME='" + station + "' and ROUTING_SEQUENCE_ID=" + routeid;
        string id = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0].ToString();
        return id;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Enabled = true;
            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Enabled = true;
        }
        if (Session[Session.SessionID + "RowID"] != null)
        {
            string temp = "";
            int rowid = Convert.ToInt32(Session[Session.SessionID + "RowID"]);
            string priorstation = ((DropDownList)GridView1.Rows[rowid].FindControl("DropDownListName")).SelectedItem.Text;
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
                {

                    string Flow = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text;

                    string Type = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).SelectedItem.Text;

                    string Disable = ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked.ToString();

                    temp += Flow + "," + Type + "," + Disable + ",";
                }
            }
            Session[Session.SessionID + priorstation] = temp;

            Session[Session.SessionID + "RowID"] = null;
        }
        int index = 0;
        if (e.CommandName == "ButtonFile")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            // 获取到行索引 RowIndex
            index = gvrow.RowIndex;//取的行索引
        }
        Session[Session.SessionID + "RowID"] = index.ToString();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((Image)GridView1.Rows[i].FindControl("passShow")).Visible = false;
        }
        ((Image)GridView1.Rows[index].FindControl("passShow")).Visible = true;

        string station = ((DropDownList)GridView1.Rows[index].FindControl("DropDownListName")).SelectedItem.Text;

        if (Session[Session.SessionID + station] == null)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
                {

                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Clear();
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add("");

                    string sql = "SELECT STATION_CODE STATION_ID, STATION_DESC STATION_NAME FROM SFC.SFC_PRODUCTION_STATIONS ORDER BY STATION_NAME";
                    DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        ListItem item = new ListItem(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString());
                        ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add(item);
                    }

                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Clear();
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("");
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("FROM");
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("TO");
                    ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
                {

                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Clear();
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add("");

                    string sql = "SELECT STATION_CODE STATION_ID, STATION_DESC STATION_NAME FROM SFC.SFC_PRODUCTION_STATIONS ORDER BY STATION_NAME";
                    DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        ListItem item = new ListItem(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString());
                        ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add(item);
                    }

                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Clear();
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("");
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("FROM");
                    ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("TO");

                    ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked = false;
                }
            }

            try
            {
                string temp = Session[Session.SessionID + station].ToString();
                temp = temp.Substring(0, temp.Length - 1);

                string[] value = temp.Split(',');
                for (int i = 0; i < value.Length; i = i + 3)
                {
                    ((DropDownList)GridView2.Rows[i / 3].FindControl("DropDownListReName")).SelectedItem.Text = value[i];

                    ((DropDownList)GridView2.Rows[i / 3].FindControl("DropDownListReType")).SelectedItem.Text = value[i + 1];

                    if (value[i + 2] == "True")
                        ((CheckBox)GridView2.Rows[i / 3].FindControl("CheckBoxReDisable")).Checked = true;
                    else
                        ((CheckBox)GridView2.Rows[i / 3].FindControl("CheckBoxReDisable")).Checked = false;

                    if (TextBoxRouteID.Text != "")
                    {
                        ((DropDownList)GridView2.Rows[i / 3].FindControl("DropDownListReName")).Enabled = false;

                        ((DropDownList)GridView2.Rows[i / 3].FindControl("DropDownListReType")).Enabled = false;
                    }
                }
            }
            catch
            {
            }
        }
    }

    protected void DropDownListName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drp = sender as DropDownList; // 触发事件的 DropDownList
        GridViewRow row = drp.NamingContainer as GridViewRow; // GridView 当前行  即时在dropdownlist所在容器里 就是行的信息         
        row.Style.Add(HtmlTextWriterStyle.BackgroundColor, drp.SelectedValue);

        // Response.Write(row.RowIndex + 1);//获取dropdownlist中选定行的行号.
        DropDownList ddlClass = (DropDownList)sender;

        string station = ddlClass.SelectedItem.Text.ToString();//获取Dropdownlist中选定值
        string stationvalue = GetStationValue(station);//获取Dropdownlist中选定值

        ((Label)GridView1.Rows[row.RowIndex].FindControl("LabelID")).Text = stationvalue;
        Session[Session.SessionID + "RowID"] = row.RowIndex.ToString();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((Image)GridView1.Rows[i].FindControl("passShow")).Visible = false;
        }
        ((Image)GridView1.Rows[row.RowIndex].FindControl("passShow")).Visible = true;


        string PriorStation = "";
        if (row.RowIndex >= 1)
            PriorStation = ((DropDownList)GridView1.Rows[row.RowIndex - 1].FindControl("DropDownListName")).SelectedItem.Text;


        string temp = "";
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
            {

                string Flow = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text;

                string Type = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).SelectedItem.Text;

                string Disable = ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked.ToString();

                temp += Flow + Type + Disable;
            }
        }
        if (row.RowIndex >= 1)
        {
            if (temp.Trim() != "")
            {
                temp = "";
                if (Session[Session.SessionID + station] == null)
                {
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
                        {

                            string Flow = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text;

                            string Type = ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).SelectedItem.Text;

                            string Disable = ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked.ToString();

                            temp += Flow + "," + Type + "," + Disable + ",";
                        }
                    }
                    Session[Session.SessionID + PriorStation] = temp;
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        if (((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).SelectedItem.Text != "")
                        {
                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Clear();
                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add("");

                            string sql = "SELECT STATION_CODE STATION_ID, STATION_DESC STATION_NAME FROM SFC.SFC_PRODUCTION_STATIONS ORDER BY STATION_NAME";
                            DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                ListItem item = new ListItem(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString());
                                ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add(item);
                            }

                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Clear();
                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("");
                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("FROM");
                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Items.Add("TO");

                            ((CheckBox)GridView2.Rows[i].FindControl("CheckBoxReDisable")).Checked = false;
                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Enabled = true;
                            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReType")).Enabled = true;

                        }
                    }
                }
                else
                {
                    temp = Session[Session.SessionID + station].ToString();
                    temp = temp.Substring(0, temp.Length - 1);
                    string[] value = temp.Split(',');
                    for (int i = 0; i < value.Length; i = i + 3)
                    {
                        ((DropDownList)GridView2.Rows[i / 3].FindControl("DropDownListReName")).SelectedItem.Text = value[i];

                        ((DropDownList)GridView2.Rows[i / 3].FindControl("DropDownListReType")).SelectedItem.Text = value[i + 1];

                        if (value[i + 2] == "True")
                            ((CheckBox)GridView2.Rows[i / 3].FindControl("CheckBoxReDisable")).Checked = true;
                        else
                            ((CheckBox)GridView2.Rows[i / 3].FindControl("CheckBoxReDisable")).Checked = false;

                    }
                }
            }
            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((Image)GridView1.Rows[i].FindControl("passShow")).Visible = false;
                }
                ((Image)GridView1.Rows[row.RowIndex - 1].FindControl("passShow")).Visible = true;

                ((DropDownList)GridView1.Rows[row.RowIndex].FindControl("DropDownListName")).Items.Clear();
                ((DropDownList)GridView1.Rows[row.RowIndex].FindControl("DropDownListName")).Items.Add("");

                string sql = "SELECT STATION_CODE STATION_ID, STATION_DESC STATION_NAME FROM SFC_PRODUCTION_STATIONS ORDER BY STATION_NAME";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem item = new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
                    ((DropDownList)GridView1.Rows[row.RowIndex].FindControl("DropDownListName")).Items.Add(item);
                }
                ((Label)GridView1.Rows[row.RowIndex].FindControl("LabelID")).Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請設置依賴項!!!');</script>");
                return;
            }

        }
    }

    protected DataTable GetDataFromGrid(GridView g)
    {
        DataTable dt1 = new DataTable("");
        dt1.Columns.Add("index");
        dt1.Columns.Add("Station_Name");
        dt1.Columns.Add("Disable");

        for (int i = 0; i < g.Rows.Count; i++)
        {
            DataRow newRow = dt1.NewRow();
            if (((DropDownList)(g.Rows[i].FindControl("DropDownListName"))).SelectedItem.Text.Trim() != "")
            {
                newRow[0] = ((Label)(g.Rows[i].FindControl("LabelID"))).Text;

                newRow[1] = ((DropDownList)(g.Rows[i].FindControl("DropDownListName"))).SelectedItem.Text;
                newRow[2] = ((CheckBox)g.Rows[i].FindControl("CheckBoxDisable")).Checked.ToString();
            }

            dt1.Rows.Add(newRow);
        }
        DataRow newRow1 = dt1.NewRow();
        dt1.Rows.Add(newRow1);

        dt1.AcceptChanges();
        return dt1;
    }

    protected DataTable GetDataFromGrid2(GridView g)
    {
        DataTable dt1 = new DataTable("");
        dt1.Columns.Add("Station_Name");
        dt1.Columns.Add("Type");
        dt1.Columns.Add("Disable");

        for (int i = 0; i < g.Rows.Count; i++)
        {
            DataRow newRow = dt1.NewRow();
            if (((DropDownList)(g.Rows[i].FindControl("DropDownListReName"))).SelectedItem.Text.Trim() != "")
            {
                newRow[0] = ((DropDownList)(g.Rows[i].FindControl("DropDownListReName"))).SelectedItem.Text;
                newRow[1] = ((DropDownList)(g.Rows[i].FindControl("DropDownListReType"))).SelectedItem.Text;

                newRow[2] = ((CheckBox)g.Rows[i].FindControl("CheckBoxReDisable")).Checked.ToString();
            }

            dt1.Rows.Add(newRow);
        }
        DataRow newRow1 = dt1.NewRow();
        dt1.Rows.Add(newRow1);

        dt1.AcceptChanges();
        return dt1;
    }

    protected void ButtonAddRow_Click(object sender, EventArgs e)
    {

        DataTable dt = GetDataFromGrid(GridView1);

        GridView1.DataSource = dt;
        GridView1.DataBind();
        for (int i = 0; i < dt.Rows.Count - 1; i++)
        {
            ((Label)(GridView1.Rows[i].FindControl("LabelID"))).Text = dt.Rows[i][0].ToString();
            ((DropDownList)(GridView1.Rows[i].FindControl("DropDownListName"))).SelectedItem.Text = dt.Rows[i][1].ToString();
            string temp2 = dt.Rows[i][2].ToString();
            if (temp2 == "True")
            {
                ((CheckBox)(GridView1.Rows[i].FindControl("CheckBoxDisable"))).Checked = true;
            }
            if (TextBoxRouteID.Text != "")
            {
                if (((DropDownList)(GridView1.Rows[i].FindControl("DropDownListName"))).SelectedItem.Text != "")
                    ((DropDownList)(GridView1.Rows[i].FindControl("DropDownListName"))).Enabled = false;
            }

        }

        for (int i = dt.Rows.Count - 1; i < GridView1.Rows.Count; i++)
        {
            ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).Items.Add("");
            ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).Items.FindByText("").Selected = true;
        }


    }
    protected void ButtonReRowAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = GetDataFromGrid2(GridView2);
        GridView2.DataSource = dt;
        GridView2.DataBind();
        for (int i = 0; i < dt.Rows.Count - 1; i++)
        {
            ((DropDownList)(GridView2.Rows[i].FindControl("DropDownListReName"))).SelectedItem.Text = dt.Rows[i][0].ToString();
            ((DropDownList)(GridView2.Rows[i].FindControl("DropDownListReType"))).SelectedItem.Text = dt.Rows[i][1].ToString();

            string temp = dt.Rows[i][2].ToString();
            if (temp == "True")
            {
                ((CheckBox)(GridView1.Rows[i].FindControl("CheckBoxReDisable"))).Checked = true;
            }
            if (TextBoxRouteID.Text != "")
            {

                if (((DropDownList)(GridView2.Rows[i].FindControl("DropDownListReName"))).SelectedItem.Text != "")
                {
                    ((DropDownList)(GridView2.Rows[i].FindControl("DropDownListReName"))).Enabled = false;
                }
                if (((DropDownList)(GridView2.Rows[i].FindControl("DropDownListReType"))).SelectedItem.Text != "")
                {
                    ((DropDownList)(GridView2.Rows[i].FindControl("DropDownListReType"))).Enabled = false;
                }
            }
        }

        for (int i = dt.Rows.Count - 1; i < GridView2.Rows.Count; i++)
        {
            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.Add("");
            ((DropDownList)GridView2.Rows[i].FindControl("DropDownListReName")).Items.FindByText("").Selected = true;
        }
    }

    protected void ButtonCopy_Click(object sender, EventArgs e)
    {
        SessionRemove();
        this.PanelCopy.Visible = true;
        string sql = "select distinct Model_Name from SFC.SFC_ROUTING_HEADERS";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        this.DropDownListModel.DataTextField = "Model_Name";
        this.DropDownListModel.DataValueField = "Model_Name";
        this.DropDownListModel.DataSource = dt.DefaultView;
        this.DropDownListModel.DataBind();

        this.DropDownListModel.Items.Add("");
        this.DropDownListModel.Items.FindByText("");

    }

    protected void ButtonCancelCopy_Click(object sender, EventArgs e)
    {
        this.PanelCopy.Visible = false;
    }
    
    //protected void ButtonStartCopy_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string sql = "Select SFC.SFC_ROUTING_HEADERS_S.nextval from dual";
    //        int MaxHeaderIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

    //        sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
    //        int MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

    //        if (this.tbInvalidDate.DateTextBox.Text.Trim() == "")
    //        {
    //            sql = "Insert into SFC.SFC_ROUTING_HEADERS"
    //                             + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                             + " Values"
    //                             + " (" + MaxHeaderIndex + ", '" + this.ddlModel.SelectedItem.Text + "', '" + this.DropDownListSPart.Text + "', '" + this.TextBoxOrder.Text + "', '" + this.ddlLine.SelectedItem.Text + "', '" + this.TextBoxDescription.Text + "'," + MaxStationIndex + ", sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";

    //        }
    //        else
    //        {
    //            sql = "Insert into SFC.SFC_ROUTING_HEADERS"
    //                             + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, DISABLE_DATE, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                             + " Values"
    //                             + " (" + MaxHeaderIndex + ", '" + this.ddlModel.SelectedItem.Text + "', '" + this.DropDownListSPart.Text + "', '" + this.TextBoxOrder.Text + "', '" + this.ddlLine.SelectedItem.Text + "', '" + this.TextBoxDescription.Text + "'," + MaxStationIndex + ", TO_DATE('" + this.tbInvalidDate.DateTextBox.Text + ":00', 'YYYY/MM/DD HH24:MI:SS'), sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
    //        }
    //        ClsGlobal.objDataConnect.DataExecute(sql);

    //        DataTable dt = null;
    //        if (Label5.Text.Trim() == "")
    //        {
    //            string Model = this.DropDownListModel.SelectedItem.Text.Trim();

    //            sql = "SELECT ROUTING_SEQUENCE_ID FROM SFC.SFC_ROUTING_HEADERS WHERE MODEL_NAME ='" + Model + "'";

    //            if (TextBoxCopyOrder.Text.Trim() != "")
    //            {
    //                sql += " AND WORK_ORDER_NUMBER='" + TextBoxCopyOrder.SelectedItem.Text.Trim() + "'";
    //            }
    //            else
    //            {
    //                sql += " AND WORK_ORDER_NUMBER IS NULL";
    //            }
    //            if (DropdownlistLine.Text.Trim() != "")
    //            {
    //                sql += " AND LINE_NUMBER='" + DropdownlistLine.SelectedItem.Text.Trim() + "'";
    //            }
    //            else
    //            {
    //                sql += " AND LINE_NUMBER IS NULL";
    //            }


    //            dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
    //            Label5.Text = dt.Rows[0][0].ToString();
    //        }
    //        sql = "SELECT * FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + Label5.Text.Trim() + " order by STATION_SEQUENCE_ID";

    //        dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
    //            MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

    //            string stationvalue = dt.Rows[i][2].ToString();
    //            string Disable = dt.Rows[i][3].ToString();
    //            sql = "Insert into SFC.SFC_ROUTING_STATIONS"
    //                                    + " (ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                                    + " Values"
    //                                    + " (" + MaxHeaderIndex + ", " + MaxStationIndex + ", '" + stationvalue + "', '" + Disable + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
    //            ClsGlobal.objDataConnect.DataExecute(sql);

    //            string StationFlowID = GetStationFlowID(Label5.Text, stationvalue);
    //            sql = "SELECT STATION_NAME,FLOW_TYPE,DISABLE_FLAG FROM SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + StationFlowID;
    //            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];

    //            for (int k = 0; k < dt1.Rows.Count; k++)
    //            {
    //                sql = "Insert into SFC.SFC_ROUTING_STATION_FLOWS"
    //                                   + " (STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //                                   + " Values"
    //                                   + " (" + MaxStationIndex + ", '" + dt1.Rows[k][0].ToString() + "', '" + dt1.Rows[k][1].ToString() + "', '" + dt1.Rows[k][2].ToString() + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
    //                ClsGlobal.objDataConnect.DataExecute(sql);
    //            }
    //        }
    //        //for (int i = 0; i < GridView1.Rows.Count; i++)
    //        //{
    //        //    if (((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text.Trim() != "")
    //        //    {
    //        //        sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
    //        //        MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

    //        //        string station = ((DropDownList)GridView1.Rows[i].FindControl("DropDownListName")).SelectedItem.Text;
    //        //        string stationvalue = GetStationValue(station);

    //        //        string Disable = ((CheckBox)GridView1.Rows[i].FindControl("CheckBoxDisable")).Checked.ToString();
    //        //        if (Disable == "True")
    //        //            Disable = "Y";
    //        //        else
    //        //            Disable = "N";
    //        //        if (Session[Session.SessionID + station] != null)
    //        //        {
    //        //            sql = "Insert into SFC.SFC_ROUTING_STATIONS"
    //        //                            + " (ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //        //                            + " Values"
    //        //                            + " (" + MaxHeaderIndex + ", " + MaxStationIndex + ", '" + stationvalue + "', '" + Disable + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
    //        //            ClsGlobal.objDataConnect.DataExecute(sql);

    //        //            string temp = Session[Session.SessionID + station].ToString();
    //        //            temp = temp.Substring(0, temp.Length - 1);
    //        //            string[] value = temp.Split(',');
    //        //            for (int j = 0; j < value.Length; j = j + 3)
    //        //            {
    //        //                if (value[j + 2] == "True")
    //        //                    value[j + 2] = "Y";
    //        //                else
    //        //                    value[j + 2] = "N";

    //        //                value[j] = GetStationValue(value[j]);

    //        //                sql = "Insert into SFC.SFC_ROUTING_STATION_FLOWS"
    //        //                           + " (STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
    //        //                           + " Values"
    //        //                           + " (" + MaxStationIndex + ", '" + value[j].Trim() + "', '" + value[j + 1].Trim() + "', '" + value[j + 2].Trim() + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
    //        //                ClsGlobal.objDataConnect.DataExecute(sql);

    //        //            }


    //        //        }
    //        //    }
    //        //}
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('恭喜，復制路由成功!!!');</script>");
    //        this.PanelCopy.Visible = false;
    //        ButtonFind_Click(this, null);

    //    }
    //    catch
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('保存路由失敗!!!');</script>");
    //        return;
    //    }
    //}


    protected void ButtonStartCopy_Click(object sender, EventArgs e)
    {
        //OracleConnection conn = new OracleConnection("Data Source=TYSFC;Persist Security Info=True;User ID=sfc;Password=sfc;Unicode=True");
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        OracleConnection conn = new OracleConnection(strcon);
        conn.Open();
        OracleCommand cmd = new OracleCommand();
        OracleTransaction mytrans1;
        mytrans1 = conn.BeginTransaction();
        cmd.Connection = conn;
        cmd.Transaction = mytrans1;
        try
        {  
            string sql = "Select SFC.SFC_ROUTING_HEADERS_S.nextval from dual";
            int MaxHeaderIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

            sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
            int MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

            if (this.tbInvalidDate.DateTextBox.Text.Trim() == "")
            {
                sql = "Insert into SFC.SFC_ROUTING_HEADERS"
                                 + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                 + " Values"
                                 + " (" + MaxHeaderIndex + ", '" + this.ddlModel.SelectedItem.Text + "', '" + this.DropDownListSPart.Text + "', '" + this.TextBoxOrder.Text + "', '" + this.ddlLine.SelectedItem.Text + "', '" + this.TextBoxDescription.Text + "'," + MaxStationIndex + ", sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
            }
            else
            {
                sql = "Insert into SFC.SFC_ROUTING_HEADERS"
                                 + " (ROUTING_SEQUENCE_ID, MODEL_NAME, PART_NUMBER, WORK_ORDER_NUMBER, LINE_NUMBER, DESCRIPTION, COMMON_ROUTING_SEQ_ID, DISABLE_DATE, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                 + " Values"
                                 + " (" + MaxHeaderIndex + ", '" + this.ddlModel.SelectedItem.Text + "', '" + this.DropDownListSPart.Text + "', '" + this.TextBoxOrder.Text + "', '" + this.ddlLine.SelectedItem.Text + "', '" + this.TextBoxDescription.Text + "'," + MaxStationIndex + ", TO_DATE('" + this.tbInvalidDate.DateTextBox.Text + ":00', 'YYYY/MM/DD HH24:MI:SS'), sysdate, '" + Context.User.Identity.Name + "', sysdate, '" + Context.User.Identity.Name + "')";
            }
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            DataTable dt = null;
            if (Label5.Text.Trim() == "")
            {
                string Model = this.DropDownListModel.SelectedItem.Text.Trim();

                sql = "SELECT ROUTING_SEQUENCE_ID FROM SFC.SFC_ROUTING_HEADERS WHERE MODEL_NAME ='" + Model + "'";

                if (TextBoxCopyOrder.Text.Trim() != "")
                {
                    sql += " AND WORK_ORDER_NUMBER='" + TextBoxCopyOrder.SelectedItem.Text.Trim() + "'";
                }
                else
                {
                    sql += " AND WORK_ORDER_NUMBER IS NULL";
                }
                if (DropdownlistLine.Text.Trim() != "")
                {
                    sql += " AND LINE_NUMBER='" + DropdownlistLine.SelectedItem.Text.Trim() + "'";
                }
                else
                {
                    sql += " AND LINE_NUMBER IS NULL";
                }


                dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
                Label5.Text = dt.Rows[0][0].ToString();
            }
            sql = "SELECT * FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + Label5.Text.Trim() + " order by STATION_SEQUENCE_ID";

            dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = "Select SFC_ROUTING_STATIONS_S.nextval from dual";
                MaxStationIndex = Convert.ToInt32(ClsGlobal.objDataConnect.DataQuery(sql).Tables[0].Rows[0][0]) + 1;

                string stationvalue = dt.Rows[i][2].ToString();
                string Disable = dt.Rows[i][3].ToString();
                sql = "Insert into SFC.SFC_ROUTING_STATIONS"
                                        + " (ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                        + " Values"
                                        + " (" + MaxHeaderIndex + ", " + MaxStationIndex + ", '" + stationvalue + "', '" + Disable + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                string StationFlowID = GetStationFlowID(Label5.Text, stationvalue);
                sql = "SELECT STATION_NAME,FLOW_TYPE,DISABLE_FLAG FROM SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + StationFlowID;
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];

                for (int k = 0; k < dt1.Rows.Count; k++)
                {
                    sql = "Insert into SFC.SFC_ROUTING_STATION_FLOWS"
                                       + " (STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY, LAST_UPDATE_DATE, LAST_UPDATED_BY)"
                                       + " Values"
                                       + " (" + MaxStationIndex + ", '" + dt1.Rows[k][0].ToString() + "', '" + dt1.Rows[k][1].ToString() + "', '" + dt1.Rows[k][2].ToString() + "', sysdate, '" + Context.User.Identity.Name + "',sysdate,'" + Context.User.Identity.Name + "')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            mytrans1.Commit();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('恭喜，復制路由成功!!!');</script>");
            this.PanelCopy.Visible = false;
            ButtonFind_Click(this, null);

        }
        catch
        {
            try
            {
                mytrans1.Rollback();
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('復制路由失敗!!!');</script>");
                return;
            }
        }
        finally
        {
            conn.Close();
        }       
    }

    protected void DropDownListModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        SessionRemove();
        this.DropDownListPPart.Items.Clear();

        this.TextBoxCopyOrder.Items.Clear();

        this.DropdownlistLine.Items.Clear();

        this.Label5.Text = "";

        string Model = this.DropDownListModel.SelectedItem.Text;
        string sql = "SELECT distinct PART_NUMBER,WORK_ORDER_NUMBER,LINE_NUMBER,ROUTING_SEQUENCE_ID FROM SFC.SFC_ROUTING_HEADERS WHERE MODEL_NAME = '" + Model + "' order by work_order_number asc ";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 1)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DropDownListPPart.Items.Add(dt.Rows[i][0].ToString());

                this.TextBoxCopyOrder.Items.Add(dt.Rows[i][1].ToString());
                this.DropdownlistLine.Items.Add(dt.Rows[i][2].ToString());
                this.Label5.Text = dt.Rows[i][3].ToString();
            }
        }

    }

    protected void DropDownListPPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Model = this.DropDownListModel.SelectedItem.Text.Trim();
        string PPart = this.DropDownListPPart.SelectedItem.Text.Trim();
        string sql = "SELECT WORK_ORDER_NUMBER,LINE_NUMBER,ROUTING_SEQUENCE_ID FROM SFC.SFC_ROUTING_HEADERS WHERE MODEL_NAME LIKE '" + Model + "%'";

        if (PPart != "")
        {
            sql += " and PART_NUMBER ='" + PPart + "'";
        }
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.TextBoxCopyOrder.Items.Add(dt.Rows[i][0].ToString());
                this.DropdownlistLine.Text = dt.Rows[i][1].ToString();
                this.Label5.Text = dt.Rows[i][2].ToString();
            }
        }
        else
        {
            this.TextBoxCopyOrder.Text = "";
            this.DropdownlistLine.Text = "";
            this.Label5.Text = "";
        }

    }

    protected void ButtonDelete_Click1(object sender, EventArgs e)
    {
        if (TextBoxRouteID.Text != "")
        {
            try
            {
                string strcmd = "SELECT ROUTING_SEQUENCE_ID,STATION_SEQUENCE_ID,STATION_NAME,DISABLE_FLAG,CREATION_DATE,LAST_UPDATED_BY FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text + " order by STATION_SEQUENCE_ID";

                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strcmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //OracleConnection conn = new OracleConnection("Data Source=TYSFC;Persist Security Info=True;User ID=sfc;Password=sfc;Unicode=True");
                    string strcon = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
                    OracleConnection conn = new OracleConnection(strcon);
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    OracleTransaction mytrans1;
                    mytrans1 = conn.BeginTransaction();
                    cmd.Connection = conn;
                    cmd.Transaction = mytrans1;

                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string time = Convert.ToDateTime(dt.Rows[i][4].ToString()).Year + "/" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Month + "/" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Day + " " + Convert.ToDateTime(dt.Rows[i][4].ToString()).Hour + ":" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Minute + ":" + Convert.ToDateTime(dt.Rows[i][4].ToString()).Second;
                            strcmd = "Insert into SFC.SFC_ROUTING_STATION_HISTORY(ROUTING_SEQUENCE_ID,STATION_SEQUENCE_ID,STATION_NAME,DISABLE_FLAG,CREATION_DATE,CREATED_BY) VALUES (" + dt.Rows[i][0].ToString() + "," + dt.Rows[i][1].ToString() + ",'" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "',to_date('" + time + "','YYYY/MM/DD HH24:MI:SS'),'" + dt.Rows[i][5].ToString() + "')";
                            cmd.CommandText = strcmd;
                            cmd.ExecuteNonQuery();
                            strcmd = "SELECT '" + TextBoxRouteID.Text + "' ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID,STATION_NAME,FLOW_TYPE,DISABLE_FLAG,CREATION_DATE,LAST_UPDATED_BY From SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + dt.Rows[i][1].ToString();
                            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strcmd).Tables[0];
                            for (int k = 0; k < dt1.Rows.Count; k++)
                            {
                                time = Convert.ToDateTime(dt1.Rows[k][5].ToString()).Year + "/" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Month + "/" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Day + " " + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Hour + ":" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Minute + ":" + Convert.ToDateTime(dt1.Rows[k][5].ToString()).Second;
                                strcmd = "Insert into SFC.SFC_ROUTING_FLOW_HISTORY(ROUTING_SEQUENCE_ID, STATION_SEQUENCE_ID, STATION_NAME, FLOW_TYPE, DISABLE_FLAG, CREATION_DATE, CREATED_BY) VALUES (" + dt1.Rows[k][0].ToString() + "," + dt1.Rows[k][1].ToString() + ",'" + dt1.Rows[k][2].ToString() + "','" + dt1.Rows[k][3].ToString() + "','" + dt1.Rows[k][4].ToString() + "',to_date('" + time + "','YYYY/MM/DD HH24:MI:SS'),'" + dt1.Rows[k][6].ToString() + "')";
                                cmd.CommandText = strcmd;
                                cmd.ExecuteNonQuery();

                            }
                            strcmd = "Delete From SFC.SFC_ROUTING_STATION_FLOWS WHERE STATION_SEQUENCE_ID=" + dt.Rows[i][1].ToString();
                            cmd.CommandText = strcmd;
                            cmd.ExecuteNonQuery();

                        }
                        strcmd = "Delete FROM SFC.SFC_ROUTING_STATIONS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text;
                        cmd.CommandText = strcmd;
                        cmd.ExecuteNonQuery();
                        strcmd = "Delete FROM SFC.SFC_ROUTING_HEADERS WHERE ROUTING_SEQUENCE_ID=" + TextBoxRouteID.Text;
                        cmd.CommandText = strcmd;
                        cmd.ExecuteNonQuery();
                        mytrans1.Commit();
                    }
                    catch
                    {
                        try
                        {
                            mytrans1.Rollback();
                        }
                        catch
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('刪除路由失敗!!!');</script>");
                            return;
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                } 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('刪除路由成功!!!');</script>");
                ButtonFind_Click(this, null);
                return;

            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('刪除路由失敗!!!');</script>");
                return;
            }
        }
    }
}
