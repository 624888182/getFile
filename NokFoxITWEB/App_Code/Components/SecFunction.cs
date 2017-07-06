using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;

using SQLHelperA;
using System.Globalization;
using System.Drawing;
using FIH.Security.db;

/// <summary>
/// SecFunction 的摘要说明
/// </summary>
public class SecFunction
{
    protected static FIH.Security.db.OperRoleDetail myEntity = new OperRoleDetail();
    protected static FIH.Security.db.OperRoleDetailInfo myEntityInfo = new OperRoleDetailInfo();

    

	public SecFunction()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}



    //-----------------------------------------
    //-----設置對應權限 Tree-------
    #region 設置對應權限頁面的 Tree
    public  DataTable GetOperateTable()
    {
        SecAccessing newSecAccessing = new SecAccessing();
        DataTable dt = newSecAccessing.ExecuteStoreTable("usp_usyOperateGetOperate");
        return dt;
    }

    public  DataTable GetRoleCode(string OperRoleGpCode, string ModuleName)
    {
        SecAccessing newSecAccessing = new SecAccessing();
        DataTable dt = newSecAccessing.GetRoleCode(OperRoleGpCode, ModuleName);
        return dt;
    }

    public  DataTable GetTreeTable(System.String ModuleName)
    {
        SecAccessing newSecAccessing = new SecAccessing();
        DataTable dt = newSecAccessing.GetTreeTable(ModuleName);
        return dt;
    }
    #endregion

    //--------------獲取權限------------------------
    #region 獲取權限
    public static bool HasOperPermissionBool(System.String ModuleCode, System.String OperCode)
    {
        bool flag = false;
        SecAccessing newSecAccessing = new SecAccessing();
        string OperRoleGpCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
        flag = newSecAccessing.HasOperPermissionBool(OperRoleGpCode, ModuleCode, OperCode);
        return flag;
    }

    public static bool HasOperPermissionBool(System.String OperRoleGpCode, System.String ModuleCode, System.String OperCode)
    {
        bool flag = false;
        SecAccessing newSecAccessing = new SecAccessing();
        flag = newSecAccessing.HasOperPermissionBool(OperRoleGpCode, ModuleCode, OperCode);
        return flag;
    }

    public static string HasOperPermissionString(System.String OperRoleGpCode, System.String ModuleCode)
    {
        string strValue = "";
        SecAccessing newSecAccessing = new SecAccessing();
        
        strValue = newSecAccessing.HasOperPermissionString(OperRoleGpCode, ModuleCode, "");
        return strValue;
    }

    public static string HasAllOperPermissionString( System.String ModuleCode)
    {
        string strValue = "";
        SecAccessing newSecAccessing = new SecAccessing();
        //string OperRoleGpCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();

        strValue = newSecAccessing.HasOperPermissionString("SuperManager", ModuleCode, "");
        return strValue;
    }

    public static string HasOperPermissionString(System.String OperRoleGpCode, System.String ModuleCode, System.String OperCode)
    {
        string strValue = "";
        SecAccessing newSecAccessing = new SecAccessing();
        strValue = newSecAccessing.HasOperPermissionString(OperRoleGpCode, ModuleCode, OperCode);
        return strValue;
    }



    public static bool IsPurview(string Purviews, string OperCode)
    {
        bool reValue = false;
        if (Purviews.ToLower().Contains(OperCode.ToLower()))
            reValue = true;
        return reValue;
    }
    public static string HasDeptPermissionString(string DeptRoleGpCode, string DivNo)//
    {
        string strValue = "";
        SecAccessing newSecAccessing = new SecAccessing();
        strValue = newSecAccessing.HasDeptPermissionString( DeptRoleGpCode,  DivNo);
        return strValue;
    }
    public static string HasDeptPermissionString(string DeptRoleGpCode )//
    {
        string strValue = "";
        string DivNo = "";
        SecAccessing newSecAccessing = new SecAccessing();
        strValue = newSecAccessing.HasDeptPermissionString(DeptRoleGpCode, DivNo);
        return strValue;
    }

    public static void BindOperPermissionList(System.Web.UI.Page page, string ModuleCode)
    {
        //應用List頁面//btnModify//btnAdd//btnDelete
        #region //操作權限管控開始------------
        string isPurview = "";
        string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
        isPurview = SecFunction.HasOperPermissionString(RoleCode, ModuleCode);
        string isAllPurview = SecFunction.HasAllOperPermissionString(ModuleCode);
        Button btnBrowser = new Button();
        //修改
        if (SecFunction.IsPurview(isAllPurview, "modify"))
        {
            try
            {
                btnBrowser = (Button)page.FindControl("btnModify");
                if (btnBrowser.Visible == true && btnBrowser.Enabled == true)
                    btnBrowser.Enabled = SecFunction.IsPurview(isPurview, "modify");
            }
            catch { }
        }
        else btnBrowser.Enabled = false;
        //刪除
        if (SecFunction.IsPurview(isAllPurview, "delete"))
        {
            try
            {
                btnBrowser = (Button)page.FindControl("btnDelete");
                if (btnBrowser.Visible == true && btnBrowser.Enabled == true)
                    btnBrowser.Enabled = SecFunction.IsPurview(isPurview, "delete");
            }
            catch { }
        }
        else btnBrowser.Enabled = false;

        #endregion　//操作權限管控結束------------
    }

    public static void BindOperPermissionExcelOut(System.Web.UI.Page page, string ModuleCode)
    {
        //用于TOExcel頁面//btnFind//btnToExcel
        #region //操作權限管控開始------------
        string isPurview = "";
        string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
        isPurview = SecFunction.HasOperPermissionString(RoleCode, ModuleCode);
        string isAllPurview = SecFunction.HasAllOperPermissionString(ModuleCode);
        Button btnBrowser = new Button();
        //查詢
        if (SecFunction.IsPurview(isAllPurview, "browse"))
        {
            try
            {
                //btnBrowser = (Button)page.FindControl("btnReset");
                //btnBrowser.Visible = SecFunction.IsPurview(isPurview, "browse");
                btnBrowser = (Button)page.FindControl("btnFind");
                if (btnBrowser.Visible == true)
                    btnBrowser.Enabled = SecFunction.IsPurview(isPurview, "browse");
            }
            catch { }
        }
        else
        {
            btnBrowser = (Button)page.FindControl("btnFind");
            btnBrowser.Enabled = false;
        }
        //新增
        if (SecFunction.IsPurview(isAllPurview, "excelOut"))
        {
            try
            {
                btnBrowser = (Button)page.FindControl("btnToExcel");
                if (btnBrowser.Visible == true)
                    btnBrowser.Enabled = SecFunction.IsPurview(isPurview, "excelOut");
            }
            catch { }
        }
        else
        {
            btnBrowser = (Button)page.FindControl("btnToExcel");
            btnBrowser.Enabled = false;
        }
        #endregion　//操作權限管控結束------------
    }

    public static void BindOperPermissionMain(System.Web.UI.Page page, string ModuleCode)
    {
        //應用于Main頁面//btnFind/btnAdd
        #region //操作權限管控開始------------
        string isPurview = "";
        string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
        isPurview = SecFunction.HasOperPermissionString(RoleCode, ModuleCode);
        string isAllPurview = SecFunction.HasAllOperPermissionString(ModuleCode);
        Button btnBrowser = new Button();
        //查詢
        if (SecFunction.IsPurview(isAllPurview, "browse"))
        {
            try
            {
                btnBrowser = (Button)page.FindControl("btnReset");
                if (btnBrowser.Visible == true)
                    btnBrowser.Enabled = SecFunction.IsPurview(isPurview, "browse");
                btnBrowser = (Button)page.FindControl("btnFind");
                if (btnBrowser.Visible == true)
                    btnBrowser.Enabled = SecFunction.IsPurview(isPurview, "browse");
            }
            catch { }
        }
        else
        {
            btnBrowser = (Button)page.FindControl("btnReset");
            btnBrowser.Enabled = false;
            btnBrowser = (Button)page.FindControl("btnFind");
            btnBrowser.Enabled = false;
        }
        //新增
        if (SecFunction.IsPurview(isAllPurview, "add"))
        {
            try
            {
                btnBrowser = (Button)page.FindControl("btnAdd");
                if (btnBrowser.Visible == true)
                    btnBrowser.Enabled = SecFunction.IsPurview(isPurview, "add");
            }
            catch { }
        }
        else
        {
            btnBrowser = (Button)page.FindControl("btnAdd");
            btnBrowser.Enabled = false;
        }
        #endregion　//操作權限管控結束------------
    }

    public static void BindOperPermission(System.Web.UI.Page page, string ModuleCode)
    {　//僅用于操作頁面的操作權限
        #region //操作權限管控開始------------
        try
        {
            string isPurview = "";
            string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
            isPurview = SecFunction.HasOperPermissionString(RoleCode, ModuleCode);
            string isAllPurview = SecFunction.HasAllOperPermissionString( ModuleCode); 
            Button btnBrowser = new Button();

            //查詢
            if (SecFunction.IsPurview(isAllPurview, "browse"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnFind");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "browse");
                }
                catch { }
            }
            else btnBrowser.Visible = false;
            //新增
            if (SecFunction.IsPurview(isAllPurview, "add"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnAdd");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "add");
                }
                catch { }
            }
            else btnBrowser.Visible = false;
            //修改
            if (SecFunction.IsPurview(isAllPurview, "modify"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnModify");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "modify");
                }
                catch 
                {
                    btnBrowser = (Button)page.FindControl("btnCommit");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "modify");
                }
            }
            else btnBrowser.Visible = false;
            //刪除
            if (SecFunction.IsPurview(isAllPurview, "delete"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnDelete");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "delete");
                }
                catch { }
            }
            else btnBrowser.Visible = false;
            //打印
            if (SecFunction.IsPurview(isAllPurview, "print"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnPrint");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "print");
                }
                catch { }
            }
            else btnBrowser.Visible = false;
            //确認
            if (SecFunction.IsPurview(isAllPurview, "confrim"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnConfrim");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "confrim");
                }
                catch { }
            }
            else btnBrowser.Visible = false;
            //反确認
            if (SecFunction.IsPurview(isAllPurview, "unConfrim"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnConfrim");
                    if (btnBrowser.Visible == true)
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "unConfrim");
                }
                catch { }
            }
            else btnBrowser.Visible = false;
        }
        catch { }

        #endregion　//操作權限管控結束------------
    }

    public static void BindOperPermission(System.Web.UI.Page page, string ModuleCode,Button btnBrowser, string OperCode)
    {　//僅用于操作頁面的單個Button操作權限
        #region //操作權限管控開始------------
        try
        {
            string isPurview = "";
            string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
            isPurview = SecFunction.HasOperPermissionString(RoleCode, ModuleCode);
            string isAllPurview = OperCode;
            //查詢

            if (SecFunction.IsPurview(isAllPurview, "browse"))
            {
                try
                {
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "browse");
                }
                catch { }
            }
            //新增
            if (SecFunction.IsPurview(isAllPurview, "add"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnAdd");
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "add");
                }
                catch { }
            }
            //修改
            if (SecFunction.IsPurview(isAllPurview, "modify"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnModify");
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "modify");
                }
                catch
                {
                    btnBrowser = (Button)page.FindControl("btnCommit");
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "modify");
                }
            }
            //刪除
            if (SecFunction.IsPurview(isAllPurview, "delete"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnDelete");
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "delete");
                }
                catch { }
            }
            //打印
            if (SecFunction.IsPurview(isAllPurview, "print"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnPrint");
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "print");
                }
                catch { }
            }
            //确認
            if (SecFunction.IsPurview(isAllPurview, "confrim"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnConfrim");
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "confrim");
                }
                catch { }
            }
            //反确認
            if (SecFunction.IsPurview(isAllPurview, "unConfrim"))
            {
                try
                {
                    btnBrowser = (Button)page.FindControl("btnConfrim");
                    btnBrowser.Visible = SecFunction.IsPurview(isPurview, "unConfrim");
                }
                catch { }
            }
        }
        catch { }

        #endregion　//操作權限管控結束------------
    }

    public static bool CheckLogin(System.Web.UI.Page page)
    {
        #region 登陸超時代碼
       // page.Session.Timeout = 40;
        //登陸超時代碼
        if ((page.Session["loginUser"] == null) || (page.Session["PageSize"] == null))
        {
        //    if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
        //    {
        //        page.Response.Write("<script>alert('timeout，pls login again！');top.location.replace('../Default.aspx');</script>");
        //    }
        //    else
        //    {
                page.Response.Write("<script>alert('登陸超時，請重新登陸！');top.location.replace('../Default.aspx');</script>");
            //}
            return false;
        }
        else
            return true;
        #endregion
    }

    public static bool CheckLoginAdd(System.Web.UI.Page page)
    {
        #region 登陸超時代碼
        //page.Session.Timeout = 20;
        //登陸超時代碼
        if ((page.Session["loginUser"] == null) || (page.Session["PageSize"] == null))
        {
            page.Response.Write("<SCRIPT   LANGUAGE='JavaScript'> window.open('Default.aspx?action=add&kerid=null','mainPages','width=max,height=max,status=no,resizable=yes,scrollbars=yes,top=0,left=0')</SCRIPT> ");
            page.Response.Write("<SCRIPT   LANGUAGE='JavaScript'>window.opener=null;window.close();</SCRIPT> ");
            
            return false;
        }
        else
            return true;
        #endregion
    }
    #endregion

    //-------------得主頁面的到Left Tree---------------
    #region 得主頁面的到Left Tree
    public  DataTable GetMainLeftTree(string parentmodulecode)//
    {
        string OperRoleGpCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
        DataTable dtValue = new DataTable();
        SecAccessing newSecAccessing = new SecAccessing();
        dtValue = newSecAccessing.GetMainLeftTree(OperRoleGpCode, parentmodulecode);
        return dtValue;
    }

    public  DataTable GetMainLeftTree(string OperRoleGpCode, string parentmodulecode)//
    {
        DataTable dtValue =new DataTable();
        SecAccessing newSecAccessing = new SecAccessing();
        dtValue = newSecAccessing.GetMainLeftTree(OperRoleGpCode, parentmodulecode);
        return dtValue;
    }
    #endregion

    #region
    //public static bool HasOperPermissionBool(string ModuleCode, string OperCode)
    //{
    //    bool reValue = false;

    //    if (System.Web.HttpContext.Current.Session["loginUser"].ToString() == "admins")
    //    {
    //        reValue = true;
    //    }
    //    else
    //    {
    //        try
    //        {
    //            myEntityInfo.ModuleCode = ModuleCode;
    //            myEntityInfo.OperCode = OperCode;
    //            myEntityInfo.OperRoleGpCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
    //            reValue = myEntity.HasOperPermissionBool(myEntityInfo);
    //        }
    //        catch { }
    //    }
    //    return reValue;
    //}

    //public static bool HasOperPermissionBool(OperRoleDetailInfo myEntityInfo)
    //{
    //    bool reValue = false;

    //    if (myEntityInfo.OperRoleGpCode.ToLower() == "supermanager")
    //    {
    //        reValue = true;
    //    }
    //    else
    //    {
    //        try
    //        {
    //            reValue = myEntity.HasOperPermissionBool(myEntityInfo);
    //        }
    //        catch { }
    //    }
    //    return reValue;
    //}

    //public static string HasOperPermissionString(string RoleCode, string ModuleCode)
    //{
    //    string reValue = "";
    //    try
    //    {
    //        myEntityInfo.OperRoleGpCode = RoleCode;
    //        myEntityInfo.ModuleCode = ModuleCode;
    //        reValue = myEntity.HasOperPermissionString(myEntityInfo);
    //    }
    //    catch (Exception ex) //(System.Data.SqlClient.SqlException ex)
    //    { throw new Exception(ex.Message); }
    //    return reValue;
    //}

    //public static string HasOperPermissionString(OperRoleDetailInfo myEntityInfo)
    //{
    //    string reValue = "";
    //    try
    //    {
    //        reValue = myEntity.HasOperPermissionString(myEntityInfo);
    //    }
    //    catch { }
    //    return reValue;
    //}
    #endregion

    public static void CheckUser(string sql, string username)
    {
    }

}
