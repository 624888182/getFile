/*************************************************************************
 * 
 *  Unit description: Retest Detail
 *  Developer: Shu Jian Bo             Date: 2008/01/18
 *  Modifier : Shu Jian Bo             Date: 2008/01/18
 * 
 * ***********************************************************************/
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

public partial class Boundary_WFrmRetestDetail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strLine = Request.QueryString["Line"].ToString();
        string strTopN = Request.QueryString["TopN"].ToString();
        string strMOdel = Request.QueryString["Model"].ToString();
        string strRepair = bool.Parse(Request.QueryString["Repair"].ToString())?"1":"0";
        int intWOType = int.Parse(Request.QueryString["WOType"].ToString());
        string strStationID = Request.QueryString["Station"].ToString();

        string strProcedureName = "SFCQUERY.GETRETESTDETAIL";
        OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("STARTDATE",OracleType.VarChar,20),
                                                            new OracleParameter("ENDDATE",OracleType.VarChar,20),
                                                            new OracleParameter("LINE",OracleType.VarChar,10),
                                                            new OracleParameter("MODEL",OracleType.VarChar,3),
                                                            new OracleParameter("WOTYPE",OracleType.Number),
                                                            new OracleParameter("REPAIR",OracleType.VarChar,1),
                                                            new OracleParameter("STATION",OracleType.VarChar,10),
                                                            new OracleParameter("DATA",OracleType.Cursor)};
        orapara[0].Value = strStartDate;
        orapara[1].Value = strEndDate;
        orapara[2].Value = strLine;
        orapara[3].Value = strMOdel;
        orapara[4].Value = intWOType;
        orapara[5].Value = strRepair;
        orapara[6].Value = strStationID;
        orapara[7].Direction = ParameterDirection.Output;
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
        dgRetestDtail.DataSource = dt.DefaultView;
        dgRetestDtail.DataBind();
    }
}
