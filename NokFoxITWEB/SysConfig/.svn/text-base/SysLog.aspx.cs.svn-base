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

public partial class SysConfig_SysLog2 : System.Web.UI.Page
{
    DbAccessing db = new DbAccessing();
    private string SessionName = "log";
    private void BindGridView(string sqlBind)
    {
        DataTable tb = db.ExecuteSqlTable(sqlBind);
        GridViewList.DataSource = tb;
        GridViewList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
        GridViewList.DataBind();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        if (!IsPostBack)
        {
            // Bind the ddl control
            string sqlBind = "select distinct OperType,OperType from R_SystemLog";
            PubFunction.dlstBound(ddlOperType, sqlBind);

            sqlBind = "select distinct ModuleName,ModuleName from R_SystemLog  ";
            PubFunction.dlstBound(ddlModule, sqlBind);

            Session[SessionName] = "";

            //權限管控開始------------
            GridViewList.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
            this.btnFind.Visible = PubFunction.HasOperPermission("Z03", "F0");
            this.btnReset.Visible = PubFunction.HasOperPermission("Z03", "F0");
            this.btnToExcel.Visible = PubFunction.HasOperPermission("Z03", "E0");
            //權限管控結束------------
        }
        else
        {
            if ((Session[SessionName] != null) && (Session[SessionName].ToString() != ""))
            {
                BindGridView(Session[SessionName].ToString());
            }
        }
    }


    protected void btnFind_Click(object sender, EventArgs e)
    {
        string findSql = " select * from View_Log where 1=1";
        if (this.tbUserID.Text.Trim() != "")
        {
            findSql += " and UserID like '%" + tbUserID.Text.Trim().ToString() + "%'";
        }
        if (this.ddlOperType.Text.Trim() != "")
        {
            findSql += " and OperType like '%" + ddlOperType.SelectedValue.Trim().ToString() + "%'";
        }
        if (this.ddlModule.SelectedValue != "")
        {
            findSql += " and ModuleName ='" + ddlModule.SelectedValue.Trim().ToString() + "'";
        }
        if ((this.tbOperTimeStart.Text.Trim() != "") && (this.tbOperTimeEnd.Text.Trim() != ""))
        {
            findSql += " and OperTime BETWEEN '" + tbOperTimeStart.Text.Trim().ToString() + "' and '" + tbOperTimeEnd.Text.Trim().ToString() + "'";
        }
        Session[SessionName] = findSql;
        BindGridView(Session[SessionName].ToString());
    }


    protected void GridViewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewList.PageIndex = e.NewPageIndex;
        BindGridView(Session[SessionName].ToString());
    }
    protected void btnReset_ServerClick(object sender, EventArgs e)
    {
        this.tbOperTimeEnd.Text = this.tbOperTimeStart.Text = this.ddlModule.SelectedValue = this.ddlOperType.SelectedValue = this.tbUserID.Text = "";
    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }
    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label MyIndex = (Label)e.Row.FindControl("lbLogId");
            string StrID = Server.UrlEncode(MyIndex.Text.Trim().ToString());
            e.Row.Attributes.Add("ondblclick", "window.open('SysLogView.aspx?action=edit&kerid=" + StrID + "','one','width=630,height=400,status=no,resizable=no,scrollbars=yes,top=200,left=200');");
            Label myLabel = (Label)e.Row.FindControl("lbItem");
            myLabel.Text = System.Convert.ToString((e.Row.RowIndex + 1 + GridViewList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));
        }
    }
    protected void GridViewList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreated(sender, e);
    }
}
