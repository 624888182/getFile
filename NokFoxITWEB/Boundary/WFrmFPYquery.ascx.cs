/*************************************************************************
 * 
 *  Unit description: FPY Query
 *  Developer: Zhang Ji Jing            Date: 2008/06/25
 *  Modifier : Zhang Ji Jing            Date: 2008/--/--
 * 
 * ***********************************************************************/
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
using DB.EAI;
using System.Threading;
using ChartDirector;
using System.Data.OracleClient;

public partial class Boundary_WFrmFPYquery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSql = "DELETE FPY_FIRST_STATUS";
        ClsGlobal.objDataConnect.DataExecute(strSql);

        // 在這裡放置使用者程式碼以初始化網頁
        if (!IsPostBack)
        {
            MultiLanaguage();
        }
        btnQuery.Attributes["onclick"] = "go();";
    }

    private void MultiLanaguage()
    {
        lblWO.Text = (String)GetGlobalResourceObject("SFCQuery", "WO");
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        
        txtModel.ReadOnly = true; 
        string strSql;
        strSql = " SELECT T.MODEL FROM Sfc.MES_PCBA_PANEL_DETAIL_LK  S,SHP.CMCS_SFC_SORDER  T WHERE S.WO_NO=T.SORDER AND S.WO_NO =" + ClsCommon.GetSqlString(txtWO.Text.Trim().ToUpper());
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            txtModel.ReadOnly = false;
            txtModel.Text = dt.Rows[0]["MODEL"].ToString();
            dt = null;
            dt = this.GetData();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "InputError", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "InputError") + "');</script>");
            return;
        }         
    }

    private DataTable GetStation(string strModel)
    {
        string strsql = "";
        strsql = strsql + "SELECT CURRENT_STATION STATION,FPY_SEQUENCE FROM CMCS_ROUTE_CONTROL_T WHERE ROUTE_CODE=(SELECT DISTINCT ROUTE_CODE  FROM CMCS_ROUTE_NAME_T WHERE  MODEL LIKE '%" + strModel + "%') AND  FPY_SEQUENCE>0  ORDER BY FPY_SEQUENCE  ASC";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        return dt;
    }

    private DataTable GetData()
    {
        string strWO = txtWO.Text.Trim().ToUpper();
        string strModel = txtModel.Text.Trim().ToUpper();
        DataTable dt = getDataTable(strWO, strModel);
        dgFPY.DataSource = dt.DefaultView;
        dgFPY.DataBind();
        return dt;
    }

    private DataTable CreateRateTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Station_Code", typeof(String));
        dt.Columns.Add("Input", typeof(String));
        dt.Columns.Add("FirstPass", typeof(String));
        dt.Columns.Add("Date", typeof(DateTime));
        dt.Columns.Add("FPYRate", typeof(String));

        return dt;
    }

    private DataTable getDataTable(string strWO, string strModel)
    {
        double multi = 1;
        DataTable dtStation = GetStation(strModel);
        int countStation;
        countStation = dtStation.Rows.Count;
        DataRow[] drStation = dtStation.Select("", "FPY_SEQUENCE");
        DataTable dtPID = getPID(strWO);
        DataRow[] drPID = dtPID.Select();
        int countPID;
        countPID = dtPID.Rows.Count;
        GC.Collect();
        DataTable dtRate = CreateRateTable();
        DataRow dtRW = null;       

        for (int i = 0; i < countStation; i++)
        {
            int Input = 0;
            int FirstPass = 0;
            for (int j = 0; j < countPID; j++)
            {
                try
                {
                    string strProcedureName = "GETFPY";
                    OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("PID",OracleType.VarChar),
																 new OracleParameter("MODEL",OracleType.VarChar),
																 new OracleParameter("STATION",OracleType.VarChar),
																 new OracleParameter("RES1",OracleType.VarChar,20),  //fen zi  FirstPass
																 new OracleParameter("RES2",OracleType.VarChar,20) };//fen mu  Input
                    orapara[0].Value = drPID[j]["PRODUCTID"];
                    orapara[1].Value = strModel;
                    orapara[2].Value = drStation[i]["STATION"];
                    orapara[3].Direction = ParameterDirection.Output;
                    orapara[4].Direction = ParameterDirection.Output;

                    ClsGlobal.objDataConnect.DataExecute(strProcedureName, orapara);

                    if (orapara[3].Value.ToString() == "FirstPass")
                    FirstPass++;
                    if (orapara[4].Value.ToString() == "Input")
                    Input++;
                }
                catch
                { }     
            }

            dtRW = dtRate.NewRow(); 
            dtRW["Station_Code"] = drStation[i]["STATION"];
            dtRW["Input"] = Input;
            dtRW["FirstPass"] = FirstPass; 
            dtRate.Rows.Add(dtRW);
        }
        for (int k= 0; k< dtRate.Rows.Count; k++)
        {
            double Rate;
            Rate = Math.Round(Convert.ToDouble(dtRate.Rows[k]["FirstPass"]) / Convert.ToDouble(dtRate.Rows[0]["Input"]), 4);
            Rate = Math.Round(Rate, 4); 
            dtRate.Rows[k]["FPYRate"] = Convert.ToString(Rate * 100) + "%";
        }


            if (dtRate.Rows.Count > 0)
            {
                int count = dtRate.Rows.Count;
                string FirstPassrate0;
                dtRW = dtRate.NewRow();
                dtRW["Station_Code"] = "Final";
                dtRW["Input"] = dtRate.Rows[0]["Input"];
                dtRW["FirstPass"] = dtRate.Rows[dtRate.Rows.Count - 1]["FirstPass"];
                multi = Math.Round(Convert.ToDouble(dtRate.Rows[dtRate.Rows.Count - 1]["FirstPass"]) / Convert.ToDouble(dtRate.Rows[0]["Input"]), 4);
                multi = Math.Round(multi, 4);
                FirstPassrate0 = Convert.ToString(multi * 100) + "%";
                dtRW["FPYRate"] = FirstPassrate0;
                dtRate.Rows.Add(dtRW);
            }
        return dtRate;
    }

    public DataTable getPID(string strWO)
    {
        string strsql = "SELECT PRODUCT_ID PRODUCTID FROM MES_PCBA_PANEL_LINK WHERE  PANEL_ID IN (SELECT PANEL_ID FROM MES_PCBA_PANEL_DETAIL_LK WHERE WO_NO='" + strWO + "')";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        return dt;
    }

}





