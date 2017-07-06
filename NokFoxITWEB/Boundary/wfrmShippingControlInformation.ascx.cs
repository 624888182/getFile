/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/09/11
 *  Modifier : Shu Jian Bo             Date: 2007/09/11
 * 
 * ***********************************************************************/
namespace SFCQuery.Boundary
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Reflection;
    using System.Resources;
    using DBAccess.EAI;
    using DB.EAI;
    using ChartDirector;

    /// <summary>
    ///		WFrmMachineRate 的摘要描述。
    /// </summary>
    public partial class wfrmShippingControlInformation : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiLanaguage();
            }
        }

        private void MultiLanaguage()
        {
            lblWorkOrder.Text = (String)GetGlobalResourceObject("SFCQuery", "WO");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
            lblCartonID.Text = (String)GetGlobalResourceObject("SFCQuery","CartonID");
            dgShippingControlInfor.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");
            dgShippingControlInfor.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","WO");
            dgShippingControlInfor.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "ScanDate");
            dgShippingControlInfor.Columns[5].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "TransferOuteDate");
            dgShippingControlInfor.Columns[6].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Status");
            dgShippingControlInfor.Columns[8].HeaderText = (String)GetGlobalResourceObject("SFCQuery","GustPN");
            dgShippingControlInfor.Columns[9].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "CartonID");
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            dgShippingControlInfor.CurrentPageIndex = 0;
            if (tbCartonID.Text.Trim().Equals("") && tbIMEI.Text.Trim().Equals("") && tbWorkOrder.Text.Trim().Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('必須輸入一個查詢條件');</script>");
                return;
            }
            string StrSql = ReturnSql();
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無信息返回！');</script>");
                dgShippingControlInfor.Visible = false;
                return;
            }
            dgShippingControlInfor.DataSource = dt.DefaultView ;
            dgShippingControlInfor.DataBind();
            Label1.Text = Label1.Text = "Current Page:" + (dgShippingControlInfor.CurrentPageIndex + 1).ToString() + "/" + dgShippingControlInfor.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            dgShippingControlInfor.Visible = true;

        }

        private string ReturnSql()
        {
            string StrSql = "select a.model,a.work_order,a.serial_no,a.imei,to_char(a.ddate,'" + "YYYY/MM/DD HH24:MI" + "') ddate,to_char(a.in_date,'" + "YYYY/MM/DD HH24:MI" + "') in_date,a.status,a.productid,a.cust_pno,a.carton_no from shp.cmcs_sfc_shipping_data  a  where  1=1  ";

            if (!tbWorkOrder.Text.Trim().Equals(""))
            {
                string WoSql = " and a.work_order =" + ClsCommon.GetSqlString(tbWorkOrder.Text.Trim().ToUpper().ToString());
                StrSql = StrSql + WoSql;
            }

            if (!tbIMEI.Text.Trim().Equals(""))
            {
                string ImeiSql = " and  a.imei =" + ClsCommon.GetSqlString(tbIMEI.Text.Trim().ToUpper().ToString()) + " or a.serial_no=" + ClsCommon.GetSqlString(tbIMEI.Text.Trim().ToUpper().ToString()) + " or a.productid=" + ClsCommon.GetSqlString(tbIMEI.Text.Trim().ToUpper().ToString());
                StrSql = StrSql + ImeiSql;
            }

            if (!tbCartonID.Text.Trim().Equals(""))
            {
                string CartonSql = " and a.Carton_no=" + ClsCommon.GetSqlString(tbCartonID.Text.Trim().ToUpper().ToString());
                StrSql = StrSql + CartonSql;
            }

            return StrSql;
        }

        protected void dgShippingControlInfor_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            string StrSql = ReturnSql();
            dgShippingControlInfor.CurrentPageIndex = e.NewPageIndex;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            dgShippingControlInfor.DataSource = dt.DefaultView;
            dgShippingControlInfor.DataBind();
            Label1.Text = "Current Page:" + (dgShippingControlInfor.CurrentPageIndex + 1).ToString() + "/" + dgShippingControlInfor.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            dgShippingControlInfor.Visible = true;
        }
}
}

