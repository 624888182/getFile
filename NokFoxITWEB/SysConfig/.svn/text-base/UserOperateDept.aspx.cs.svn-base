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

public partial class SysConfig_UserOperateDept : System.Web.UI.Page
{
    protected string UserID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = Request.QueryString["UserID"];
        if (!IsPostBack)
        {
            
        }
    }

    private void DataBindFind()
    {
        DbAccessing CDAL = new DbAccessing();
        try
        {
            string sql = "select DepartmentID , DepartmentCode , DepartmentName,PriceCode  from usyDepartment where  DepartmentID not in (select DeptID  from usyUserOperateDept where UserID = '" + UserID + "')";
            if (txtDeptCode.Text != "")
            {
                sql += " and DepartmentCode like '%" + txtDeptCode.Text + "%'";
            }
            if (txtDeptName.Text != "")
            {
                sql += " and DepartmentName like '%" + txtDeptName.Text + "%'";
            }
            if (txtPrice.Text != "")
            {
                sql += " and PriceCode like '%" + txtPrice.Text + "%'";
            }
            sql += " order by DepartmentCode";
            DataTable dtDept = CDAL.ExecuteSqlTable(sql);
            gvDept.PageSize = Convert.ToInt32(Session["PageSize"]);
            gvDept.DataSource = dtDept;
            gvDept.DataBind();
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        DataBindFind();
    }
    protected void gvDept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDept.PageIndex = e.NewPageIndex;
        DataBindFind();
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session["ItemID"] = null;
        Response.Write("<script>window.close()</script>");
    }
    protected void gvDept_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string deptID = gvDept.DataKeys[Convert.ToInt32(e.RowIndex)].Value.ToString();
        DbAccessing CDAL = new DbAccessing();
        string sql = "insert into usyUserOperateDept(UserID , DeptID) values('" + UserID + "','" + deptID + "')";
        try
        {
            CDAL.ExecuteSql(sql);
            Session["ItemID"] = deptID;
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
        }
        DataBindFind();
    }
}
