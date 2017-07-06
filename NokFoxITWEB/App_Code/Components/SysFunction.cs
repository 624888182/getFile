using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using System.Data.SqlClient;
/// <summary>
/// SysFunction 的摘要说明
/// </summary>
public class SysFunction : System.Web.UI.Page
{
    //DbAccessing dbAccess = new DbAccessing();
	public SysFunction()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    //string dbConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"]).ToString();

    //得到流水ＩＤ號//Main+0001
    public static string GetIdOfData(string Main,string MaxID)
    {
        try
        {
            MaxID = System.Convert.ToString(System.Convert.ToInt32(MaxID)+1);
            switch (MaxID.Length)
            {
                case 0: MaxID = "0001"; break;
                case 1: MaxID = "000" + MaxID.ToString(); break;
                case 2: MaxID = "00" + MaxID.ToString(); break;
                case 3: MaxID = "0" + MaxID.ToString(); break;
                case 4: MaxID = MaxID.ToString(); break;
                default: MaxID = "0001"; break;
            }
        }
        catch
        {
            return Main+"0001";
        }

        return Main+MaxID;
    }
    //得到流水ＩＤ號//Main+yyMMdd+0001
    public static string GetIdOfDataSql(string Main, string GetMaxIDSql)
    {
        DbAccessing dbAccess = new DbAccessing();
        //GetMaxIDSql = "select max(ID) as MaxID from table";
        string MaxID = "0000";
        DataTable dt = dbAccess.ExecuteSqlTable(GetMaxIDSql);
        try
        {
            string row = "0000";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["MaxID"].ToString().Length == 0)
                    row = "0001";
                else
                {
                    string rowstr = dr["MaxID"].ToString();
                    rowstr = rowstr.Substring(rowstr.Length - 4, 4);
                    rowstr = System.Convert.ToString(System.Convert.ToUInt32(rowstr) + 1);

                    switch (rowstr.Length)
                    {
                        case 1: row = "000" + rowstr.ToString(); break;
                        case 2: row = "00" + rowstr.ToString(); break;
                        case 3: row = "0" + rowstr.ToString(); break;
                        case 4: row = rowstr.ToString(); break;
                        default: row = "0001"; break;
                    }
                }
            }
            MaxID = row;
        }
        catch
        {
            Main += System.DateTime.Now.ToString("yyMMdd");
            return Main + "0001";
        }
        Main += System.DateTime.Now.ToString("yyMMdd");
        return Main + MaxID;
    }
    //得到流水ＩＤ號//yyMMdd+0001
    public static string GetIdOfDataMainDate( string table, string DefaultID)
    {
        string Main = "";
        DbAccessing dbAccess = new DbAccessing();
        //GetMaxIDSql = "select max(ID) as MaxID from table";
        string GetMaxIDSql = "select max(" + DefaultID + ") as MaxID from " + table + " where " + DefaultID + " like '" 
                + System.DateTime.Now.ToString("yyMMdd") + "%'";
        string MaxID = "0000";
        DataTable dt = dbAccess.ExecuteSqlTable(GetMaxIDSql);
        try
        {
            string row = "0000";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["MaxID"].ToString().Length == 0)
                    row = "0001";
                else
                {
                    string rowstr = dr["MaxID"].ToString();
                    rowstr = rowstr.Substring(rowstr.Length - 4, 4);
                    rowstr = System.Convert.ToString(System.Convert.ToUInt32(rowstr) + 1);

                    switch (rowstr.Length)
                    {
                        case 1: row = "000" + rowstr.ToString(); break;
                        case 2: row = "00" + rowstr.ToString(); break;
                        case 3: row = "0" + rowstr.ToString(); break;
                        case 4: row = rowstr.ToString(); break;
                        default: row = "0001"; break;
                    }
                }
            }
            MaxID = row;
        }
        catch
        {
            Main += System.DateTime.Now.ToString("yyMMdd");
            return Main + "0001";
        }
        Main += System.DateTime.Now.ToString("yyMMdd");
        return Main + MaxID;
    }
    //得到流水ＩＤ號//Main+yyMMdd+0001
    public static string GetIdOfDataMainDate(string Main, string table,string DefaultID)
    {
        DbAccessing dbAccess = new DbAccessing();
        //GetMaxIDSql = "select max(ID) as MaxID from table";
        string GetMaxIDSql = "select max(" + DefaultID + ") as MaxID from " + table + " where " + DefaultID + " like '" + Main
                + System.DateTime.Now.ToString("yyMMdd") + "%'";
        string MaxID = "0000";
        DataTable dt = dbAccess.ExecuteSqlTable(GetMaxIDSql);
        try
        {
            string row = "0000";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["MaxID"].ToString().Length == 0)
                    row = "0001";
                else
                {
                    string rowstr = dr["MaxID"].ToString();
                    rowstr = rowstr.Substring(rowstr.Length - 4, 4);
                    rowstr = System.Convert.ToString(System.Convert.ToUInt32(rowstr) + 1);

                    switch (rowstr.Length)
                    {
                        case 1: row = "000" + rowstr.ToString(); break;
                        case 2: row = "00" + rowstr.ToString(); break;
                        case 3: row = "0" + rowstr.ToString(); break;
                        case 4: row = rowstr.ToString(); break;
                        default: row = "0001"; break;
                    }
                }
            }
            MaxID = row;
        }
        catch
        {
            Main += System.DateTime.Now.ToString("yyMMdd");
            return Main + "0001";
        }
        Main += System.DateTime.Now.ToString("yyMMdd");
        return Main + MaxID;
    }

    public static string JustifyDDLNotNullAndTime(System.Web.UI.WebControls.DropDownList myDropDownList, string ErrorName)
    {
        string return_value = "";
        if (myDropDownList.Text.Trim() == "")
        {
            return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
        }

        
        if (myDropDownList.SelectedItem.Text != "")
        {
            try
            {
                DateTime dtDate;
                dtDate = DateTime.Parse(myDropDownList.Text.Trim());
            }
            catch
            {
                return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為時間類型！";
            }
        }

        return return_value;
    }



  
}
