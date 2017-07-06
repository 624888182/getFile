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
/// GlobalCs 的摘要说明

/// </summary>
public class LoginInfo
{
    private System.String pageSize;
    private System.String loginUser;
    private System.String loginUserName;
    //private System.String RoleCode;
    //private System.String OrgNo;

    public LoginInfo()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //public LoginInfo(
    //            System.String pageSize;
    //            System.String loginUser;
    //            System.String loginUserName;
    //            System.String RoleCode;
    //            System.String OrgNo;
    //        ){
    //        this.pageSize  = pageSize;
    //        this.loginUser  = loginUser;
    //        this.loginUserName  = loginUserName;
    //        this.RoleCode  = RoleCode;
    //        this.OrgNo  = OrgNo;
    //    }


    public System.String PageSize
    {
        get { return pageSize; }
        set { this.pageSize = value; }
    }


    public System.String LoginUser
    {
        get { return loginUser; }
        set { this.loginUser = value; }
    }


    public System.String LoginUserName
    {
        get { return loginUserName; }
        set { this.loginUserName = value; }
    }


		
   

}
