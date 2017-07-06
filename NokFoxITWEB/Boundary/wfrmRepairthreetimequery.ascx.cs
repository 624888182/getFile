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
using DBAccess.EAI;

public partial class Boundary_wfrmRepairthreetimequery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDDL();
            MultiLanaguage();
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
        }
    }

    #region Web Form 設計工具產生的程式碼
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
    ///		這個方法的內容。
    /// </summary>
    private void InitializeComponent()
    {
        this.dgRepairHistory.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgRepairWip_PageIndexChanged);

    }
    #endregion

    private void InitialDDL()
    {
        string strSql;
        strSql = "SELECT distinct DB_USER MODEL FROM CMCS_SFC_MODEL ORDER BY MODEL";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();

        ddlModel.Items.Insert(0, "");
    }

    private void MultiLanaguage()
    { 
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo"); 
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
        Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgRepairHistory.CurrentPageIndex = 0;
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            Label28.Visible = true;
            Label29.Visible = false;
            return;
        }

        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            Label28.Visible = false;
            Label29.Visible = true;
            return;
        }


        Label28.Visible = false;
        Label29.Visible = false;

        GetData();

    }

    private void GetData()
    {
        string strSql = "SELECT * FROM (SELECT PRODUCT_ID,MODEL_ID,count(*) qty from ( select product_id,model_id,complete_date FROM MES_REPAIR_HISTORY T ";
        string strWhere = "";
        if (!tbProductID.Text.Trim().Equals(""))
        {
            strWhere = strWhere + " AND PRODUCT_ID LIKE " + ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper() + "%");
        }

        //			if (!ddlStationID.SelectedIndex.Equals(0))
        //				strWhere = strWhere + " AND STATION_CODE = "+ ClsCommon.GetSqlString(ddlStationID.SelectedValue);

        
        if (!ddlModel.SelectedIndex.Equals(0))
            strWhere = strWhere + " AND MODEL_ID = " + ClsCommon.GetSqlString(ddlModel.SelectedValue);

        if (!tbStartDate.DateTextBox.Text.Trim().Equals(""))
        {
            strWhere = strWhere + " AND COMPLETE_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(tbStartDate.DateTextBox.Text.Trim()) + ",'YYYY/MM/DD HH24:MI') AND TO_DATE("
                + ClsCommon.GetSqlString(tbEndDate.DateTextBox.Text.Trim()) + ",'YYYY/MM/DD HH24:MI') ";
        }

        strSql = strSql + " WHERE " + strWhere.Substring(4) + " ) group by product_id,model_id ) where qty>2 order by qty";

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        dgRepairHistory.DataSource = dt.DefaultView;
        dgRepairHistory.DataBind();
        Label2.Visible = true;
        dgDtail.Visible = false;
        Label3.Visible = false;
        Label4.Text = "Current Page:" + (dgRepairHistory.CurrentPageIndex + 1).ToString() + "/" + dgRepairHistory.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
    }

    private void dgRepairWip_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgRepairHistory.CurrentPageIndex = e.NewPageIndex;
        GetData();
    }

    protected void dgRepairHistory_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "PRODUCTID")
        {
            dgDtail.Visible = true;
            Label3.Visible = true;

            string strproductid = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text;
            string strmodel = ((Label)(e.Item.Cells[1].Controls[1])).Text;

            string strSql = "SELECT PRODUCT_ID,MODEL_ID model,station,defect_code,fail_message,component,repair_man,complete_date FROM MES_REPAIR_HISTORY  ";
            string strWhere = " AND PRODUCT_ID LIKE " + ClsCommon.GetSqlString(strproductid.ToUpper() + "%");
            strWhere = strWhere + " AND MODEL_ID = " + ClsCommon.GetSqlString(strmodel);

            if (!tbStartDate.DateTextBox.Text.Trim().Equals(""))
            {
                strWhere = strWhere + " AND COMPLETE_DATE BETWEEN TO_DATE(" + ClsCommon.GetSqlString(tbStartDate.DateTextBox.Text.Trim()) + ",'YYYY/MM/DD HH24:MI') AND TO_DATE("
                    + ClsCommon.GetSqlString(tbEndDate.DateTextBox.Text.Trim()) + ",'YYYY/MM/DD HH24:MI') ";
            }

            strSql = strSql + " WHERE " + strWhere.Substring(4) + " order by complete_date asc";

           DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            

            dgDtail.DataSource = dt.DefaultView;
            dgDtail.DataBind();
        }
    }
}
