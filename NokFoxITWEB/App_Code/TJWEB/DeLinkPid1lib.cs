using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for DeLinkPid1lib
/// </summary>
public class DeLinkPid1lib
{
	public DeLinkPid1lib()
	{
		//
		// TODO: Add constructor logic here
		//
       
    }
    public string DeLinkPidInsertSQL(string P1, string DBType, string ReadDB, string WriDB, string tableName, string pidColName, string sqlProg, string action, string wo_no, string pid, string linename, ref string ReMess, ref bool bRet)
    {
        string strsql = "";
        switch (action)
        {
            case "105":
                strsql = sqlProg + "(WO_NO, PRODUCT_ID,STATION_ID,STATE_ID, LINE_NAME) values('" + wo_no + "','" + pid + "','UBDC','P','" + linename + "')";
                break;
            default:

                ReMess = "Not Definition Rout: " + action;
                bRet = false;
                break;
        }
        return strsql;
    }
}
