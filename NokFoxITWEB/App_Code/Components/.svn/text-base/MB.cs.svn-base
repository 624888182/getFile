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
/// MessageBox 的摘要说明
/// </summary>
public class MBClass
{
	public MBClass()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string Show(string strMsg)
    {

        //这里0就指strMsg这东西,1就是指\这东西.
        return String.Format("<script language={1}javascript{1}>alert({1}{0}{1});</script>", strMsg, "\"");
    }  


}
