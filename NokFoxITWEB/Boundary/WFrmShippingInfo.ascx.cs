namespace SFCQuery.Boundary
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Data.OracleClient;
    using DBAccess.EAI;
    using DB.EAI;
    using System.Reflection;
    using Excel = Microsoft.Office.Interop.Excel;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using System.Text;
    using System.IO;
    using System.Xml;
    using UsingClass;
    using System.Web.UI.WebControls;
    using System.Web.UI; 

	/// <summary>
	///		WFrmShippingInfo 的摘要描述。
	/// </summary>
	public partial class WFrmShippingInfo : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				MultiLanguage();
				btnDateFrom.Attributes["onclick"] = "return showCalendar('"+tbStartDate.ClientID+"','%Y/%m/%d');";
				btnDateTo.Attributes["onclick"] = "return showCalendar('"+tbEndDate.ClientID+"','%Y/%m/%d');";
				tbStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
				tbEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
			}
		}

		#region Web Form 設計工具產生的程式碼
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
			// 
			base.OnInit(e);
		}
		
		/// <summary>
		///		此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
		///		這個方法的內容。
		/// </summary> 
		#endregion

		private void MultiLanguage()
		{
            string strErrorDateTime = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
            Label28.Text = strErrorDateTime;
            Label29.Text = strErrorDateTime;
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery","Query");
            btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
//			lblStartDate.Text = rm.GetString("ShipDateFrom");
//			lblEndDate.Text = rm.GetString("ShipDateTo");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			dgProduct.PageIndex= 0;
			if (!ClsCommon.CheckIsDateTime(tbStartDate.Text.Trim()))
			{
				Label28.Visible = true;
				Label29.Visible = false;
				return;
			}

			if (!ClsCommon.CheckIsDateTime(tbEndDate.Text.Trim()))
			{
				Label28.Visible = false;
				Label29.Visible = true;
				return;
			}

			Label28.Visible = false;
			Label29.Visible = false;

			if(tbInvoiceNO.Text.Trim().Equals("") && tbCartonID.Text.Trim().Equals("") && tbIMEI.Text.Trim().Equals("") )
			{
				Page.ClientScript.RegisterStartupScript(this.GetType(),"Error","<script language='javascript'>alert('必須輸入一個查詢條件');</script>");
				return;
			}
            try
            {
                string strProcedureName = "SFCQUERY.GETSHIPPINGINFO";
                OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("INVOICENO",OracleType.VarChar,10),
															  new OracleParameter("STARTDATE",OracleType.VarChar,10),
															  new OracleParameter("ENDDATE",OracleType.VarChar,10),
															  new OracleParameter("CARTONID",OracleType.VarChar,20),
															  new OracleParameter("IMEI",OracleType.VarChar,20),
															  new OracleParameter("DATA",OracleType.Cursor)};
                orapara[0].Value = tbInvoiceNO.Text.Trim();
                orapara[1].Value = tbStartDate.Text.Trim();
                orapara[2].Value = tbEndDate.Text.Trim();
                orapara[3].Value = tbCartonID.Text.Trim();
                orapara[4].Value = tbIMEI.Text.Trim();
                orapara[5].Direction = ParameterDirection.Output;
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dgProduct.DataSource = dt.DefaultView;
                    dgProduct.DataBind();
                    Label1.Text = "Current Page:" + (dgProduct.PageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
                }
                else
                {
                    string strsql = "SELECT C.INVOICE_NUMBER Invoice,c.so_no so_number,c.item_number pn,'' imei,'' sn,c.internal_carton carton_id,c.customer_po po_no,'' sw_version,c.ship_date delivery_date,'' sim_lock,'' unlock_code FROM SAP.CMCS_SFC_PACKING_LINES_ALL C WHERE INVOICE_NUMBER='" + tbInvoiceNO.Text.Trim() + "'";
                    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        dgProduct.DataSource = dt1.DefaultView;
                        dgProduct.DataBind();
                        Label1.Text = "Current Page:" + (dgProduct.PageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該批手機尚未打包出貨!!!');</script>");
                        return;
                    }
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請確認你的查詢條件是否正確!');</script>");
                return;
            }
        }

        private void dgProduct_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgProduct.PageIndex = e.NewPageIndex;
            string strProcedureName = "SFCQUERY.GETSHIPPINGINFO";
            OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("INVOICENO",OracleType.VarChar,10),
                                                                 new OracleParameter("STARTDATE",OracleType.VarChar,10),
                                                                 new OracleParameter("ENDDATE",OracleType.VarChar,10),
                                                                 new OracleParameter("CARTONID",OracleType.VarChar,15),
                                                                 new OracleParameter("IMEI",OracleType.VarChar,20),
                                                                 new OracleParameter("DATA",OracleType.Cursor)};
            orapara[0].Value = tbInvoiceNO.Text.Trim();
            orapara[1].Value = tbStartDate.Text.Trim();
            orapara[2].Value = tbEndDate.Text.Trim();
            orapara[3].Value = tbCartonID.Text.Trim();
            orapara[4].Value = tbIMEI.Text.Trim();
            orapara[5].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            if (dt.Rows.Count > 0)
            { 
                dgProduct.DataSource = dt.DefaultView;
                dgProduct.DataBind();
                Label1.Text = "Current Page:" + (dgProduct.PageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            }
            else
            {
                string strsql = "SELECT C.INVOICE_NUMBER Invoice_Number,C.CUSTOMER_ITEM Customer_Item,C.CUSTOMER_PO Customer_PO,C.QUANTITY Quantity, C.INTERNAL_CARTON Carton_NO,SHIP_TO_CUSTOMERNAME Customer_Name,TO_CHAR(C.CREATION_DATE,'YYYY/MM/DD') Creation_Date,TO_CHAR(C.LAST_UPDATE_DATE,'YYYY/MM/DD') Last_Update_Date,C.SHIP_DATE Ship_Date,C.SHIP_TO_CITY Ship_to_City, C.SHIP_TO_COUNTRY Ship_to_Country FROM SAP.CMCS_SFC_PACKING_LINES_ALL C WHERE INVOICE_NUMBER='" + tbInvoiceNO.Text.Trim() + "'";
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                if (dt1.Rows.Count > 0)
                { 
                    dgProduct.DataSource = dt1.DefaultView;
                    dgProduct.DataBind();
                    Label1.Text = "Current Page:" + (dgProduct.PageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();
                }
            }
            //dgProduct.DataSource = dt.DefaultView;
            //dgProduct.DataBind();
            //Label1.Text = "Current Page:" + (dgProduct.CurrentPageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
        }

        protected void btnExportExcel_Click(object sender, System.EventArgs e)
        {
            Response.Clear();
            try
            {
                string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
                string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                Response.ContentType = "application/ms-excel";
                Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
                Response.AppendHeader("Content-Disposition", "attachment;filename=ship_info.xls");
                this.EnableViewState = false;
                StringWriter tw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                string strProcedureName = "SFCQUERY.GETSHIPPINGINFO";
                OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("INVOICENO",OracleType.VarChar,10),
															  new OracleParameter("STARTDATE",OracleType.VarChar,10),
															  new OracleParameter("ENDDATE",OracleType.VarChar,10),
															  new OracleParameter("CARTONID",OracleType.VarChar,20),
															  new OracleParameter("IMEI",OracleType.VarChar,20),
															  new OracleParameter("DATA",OracleType.Cursor)};
                orapara[0].Value = tbInvoiceNO.Text.Trim();
                orapara[1].Value = tbStartDate.Text.Trim();
                orapara[2].Value = tbEndDate.Text.Trim();
                orapara[3].Value = tbCartonID.Text.Trim();
                orapara[4].Value = tbIMEI.Text.Trim();
                orapara[5].Direction = ParameterDirection.Output;
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];

                this.dgProduct.DataSource = dt.DefaultView;

                this.dgProduct.AllowPaging = false;
                this.dgProduct.DataBind();

                dgProduct.RenderControl(hw);

                Response.Write(tw.ToString());
                Response.End();

                // turn the paging on again 
                //gvPO.AllowPaging = true;
                //bind(); 
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
                return;
            }     
        }       

        protected void dgProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[2].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[4].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[6].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[7].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[8].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[9].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[10].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    e.Row.Cells[11].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                }
            }
            catch
            { 
            
            }
        }

        protected void dgProduct_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            dgProduct.PageIndex = e.NewSelectedIndex;

            string strProcedureName = "SFCQUERY.GETSHIPPINGINFO";
            OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("INVOICENO",OracleType.VarChar,10),
                                                                 new OracleParameter("STARTDATE",OracleType.VarChar,10),
                                                                 new OracleParameter("ENDDATE",OracleType.VarChar,10),
                                                                 new OracleParameter("CARTONID",OracleType.VarChar,20),
                                                                 new OracleParameter("IMEI",OracleType.VarChar,20),
                                                                 new OracleParameter("DATA",OracleType.Cursor)};
            orapara[0].Value = tbInvoiceNO.Text.Trim();
            orapara[1].Value = tbStartDate.Text.Trim();
            orapara[2].Value = tbEndDate.Text.Trim();
            orapara[3].Value = tbCartonID.Text.Trim();
            orapara[4].Value = tbIMEI.Text.Trim();
            orapara[5].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dgProduct.DataSource = dt.DefaultView;
                dgProduct.DataBind();
                Label1.Text = "Current Page:" + (dgProduct.PageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            }
            else
            {
                string strsql = "SELECT distinct C.INVOICE_NUMBER Invoice_Number,C.CUSTOMER_ITEM Customer_Item,C.CUSTOMER_PO Customer_PO,C.QUANTITY Quantity, C.INTERNAL_CARTON Carton_NO,SHIP_TO_CUSTOMERNAME Customer_Name,TO_CHAR(C.CREATION_DATE,'YYYY/MM/DD') Creation_Date,TO_CHAR(C.LAST_UPDATE_DATE,'YYYY/MM/DD') Last_Update_Date,C.SHIP_DATE Ship_Date,C.SHIP_TO_CITY Ship_to_City, C.SHIP_TO_COUNTRY Ship_to_Country FROM SAP.CMCS_SFC_PACKING_LINES_ALL C WHERE INVOICE_NUMBER='" + tbInvoiceNO.Text.Trim() + "'";
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    dgProduct.DataSource = dt1.DefaultView;
                    dgProduct.DataBind();
                    Label1.Text = "Current Page:" + (dgProduct.PageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();
                }
            }

        } 
} 
}
