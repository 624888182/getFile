using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// StoreFunction 的摘要说明
/// </summary>
public class StoreFunction
{
	public StoreFunction()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//

	}

    public static bool dlstBound(DropDownList list, string store)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable dt = newDbAccessing.ExecuteStoreTable(store);//GetNewGoodsDetailsBodyItem
            list.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                list.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
            return true;

        }
        catch
        {
            return false;
        }
    }

    public static bool dlstBound(DropDownList list, string store, bool IsFirstRowNull)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable dt = newDbAccessing.ExecuteStoreTable(store);//GetNewGoodsDetailsBodyItem
            list.Items.Clear();
            if (IsFirstRowNull == true)
            {
                list.Items.Add(new ListItem("--All--", ""));
            }
            foreach (DataRow dr in dt.Rows)
            {
                list.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
            return true;

        }
        catch
        {
            return false;
        }
    }

}
