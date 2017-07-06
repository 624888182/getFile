/*************************************************************************
 * 
 *  Unit description: Search the test station data for Pass/Fail
 *  Developer: Shu Jian Bo             Date: 2007/11/28
 *  Modifier : Shu Jian Bo             Date: 2007/11/28
 * 
 * ***********************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;
using System.Web.SessionState;

/// <summary>
/// Localzed 的摘要描述
/// 此類用於實現多國語言，對於*.ASPX.CS文件隻需繼承此類即可。
/// </summary>
public class Localized : System.Web.UI.Page
{
    protected override void InitializeCulture()
    {
        String s = (String)Session["Language"];
        if (String.IsNullOrEmpty(s))
        {
            s = Request.Cookies["Lan"].Value;//
            if (String.IsNullOrEmpty(s))
                s = System.Globalization.CultureInfo.CurrentCulture.Name;

        }

        Thread.CurrentThread.CurrentUICulture = new CultureInfo(s);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(s);

    }
    public Localized()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }
}
