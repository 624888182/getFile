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
using System.IO;
using System.Text;

public partial class Boundary_WFrmTestValueData : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strPID = txtPID.Text.ToString();
        if (strPID == "" || strPID == null||(strPID.Length!=14&&strPID.Length!=15))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('PID不能為空或長度不對！！');</script>");
            return;
        }
        string strSql = string.Empty;
        switch (rblQueryType.SelectedIndex)
        {
            case 0:  //Total Data
                strSql = "select SERIAL_NUMBER PID,STATUS Status,ITEM_NAME ItemName,LO_LIMIT MinSpec,UP_LIMIT MaxSpec,READING TestValue,UNIT_NAME Unit,TEST_TIME TestTime"
                            +" from WCDMA_TSE.R_FUNCTION_DETAIL_T WHERE SERIAL_NUMBER = '"
                            + strPID + "' order by TEST_TIME desc";
                break;
            case 1://fail Data
                strSql = "select SERIAL_NUMBER PID,STATUS Status,ITEM_NAME ItemName,LO_LIMIT MinSpec,UP_LIMIT MaxSpec,READING TestValue,UNIT_NAME Unit,TEST_TIME TestTime"
                           + " from WCDMA_TSE.R_FUNCTION_DETAIL_T WHERE STATUS = 1 AND SERIAL_NUMBER = '"
                           + strPID + "' order by TEST_TIME desc";
                break;
        }

        DataSet ds = ClsGlobal.objDataConnect.DataQuery(strSql);
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            dgData.DataSource = dt.DefaultView;
            dgData.DataBind();
            LabelTotal.Text = "Total:" + dt.Rows.Count.ToString();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('抱歉,暫時還沒有數據,可能還沒有上傳到數據庫中！！');</script>");
            return;
        }
        
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);

        dgData.RenderControl(hw);
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment; filename=TestValueQuery.xls");
        response.Charset = "gb2312";
        response.Write(tw.ToString());
        response.End();
    }
    protected void Data_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#bec5e7'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f5f5'");
        }



    }
}
