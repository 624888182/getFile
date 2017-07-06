using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Threading;


public partial class Boundary_WFrmCPKInfoQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        string strModel = Request.QueryString["Model"].ToString();
        string strStationName = Request.QueryString["StationName"].ToString();
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strItems = Request.QueryString["Items"].ToString();
        string strUpLimit = Request.QueryString["UpLimit"].ToString();
        string strLowLimit = Request.QueryString["LowLimit"].ToString();

        string strSql = "select TRACK_ID PRODUCTID,TEST_VALUE,TEST_TIME from " + strModel + "." + strModel + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE  LOWER_LIMIT='" + strLowLimit + "' and UPPER_LIMIT='" + strUpLimit + "'  AND TEST_DATE >= TO_DATE('" + strStartDate + "','YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE('" + strEndDate + "','YYYY/MM/DD HH24:MI')  AND TEST_CODE='" + strItems + "'";
        //string strSql = " select TRACK_ID PRODUCTID,TEST_VALUE,TEST_TIME  from (select TRACK_ID,TEST_VALUE,TEST_TIME,ROW_NUMBER() OVER(PARTITION BY TRACK_ID ORDER BY TEST_DATE DESC) RN from " + strModel + "." + strModel + "_" + strStationName + "_PATS_TEST@nextestsvr S WHERE  LOWER_LIMIT='" + strLowLimit + "' and UPPER_LIMIT='" + strUpLimit + "'  AND TEST_DATE >= TO_DATE('" + strStartDate + "','YYYY/MM/DD HH24:MI') AND TEST_DATE <=TO_DATE('" + strEndDate + "','YYYY/MM/DD HH24:MI')  AND TEST_CODE='" + strItems + "') where  RN=1";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        dgCPKDetail.DataSource = dt.DefaultView;
        dgCPKDetail.DataBind();
    }
}
