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
            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlModel.DataTextField = "MODEL";
            ddlModel.DataValueField = "MODEL";
            ddlModel.DataSource = dt.DefaultView;
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, "");
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
            string strsql=null ;
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

			if(ddlModel.Text.Trim().Equals("") )
			{
				Page.ClientScript.RegisterStartupScript(this.GetType(),"Error","<script language='javascript'>alert('必須選擇幾種！');</script>");
				return;
			}
            else
            {
                if(tbPID.Text.Trim().Equals("") && tbIMEI.Text.Trim().Equals("") )
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),"Error","<script language='javascript'>alert('必須輸入查詢條件！');</script>");
				return;
                }
            }




            switch (ddlModel.Text.Trim())
            {
                case "BZ3":
                    strsql = "SELECT IMEI,PRODUCTID PID,NKEY UNLOCKCODE,NSKEY SIMLOCKCODE,E2PDATE TESTDATE  FROM BZ3.KEY_LIST WHERE IMEI='" + tbIMEI.Text.Trim() + "' OR PRODUCTID= '" + tbPID.Text.Trim() + "'";
                    break;
                case "FA6":

                    if (!tbIMEI.Text.Trim().Equals(""))
                    {
                        if (FA6FA1.Checked == true)
                        {                            
                            strsql = "SELECT  A.VALUE IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA6.SIMLKTESTSPECINFO A,FA6.SIMLKTESTSPECINFO B,FA6.SIMLKTESTSPECINFO C,FA6.SIMLKSTATIONINFO D WHERE A.UNIQUEID=B.UNIQUEID AND A.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND A.VALUE ='" + tbIMEI.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                        else
                        {
                            strsql = "SELECT  E.IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA6.RDLTESTSPECINFO B,FA6.RDLTESTSPECINFO C,FA6.RDLSTATIONINFO D,FA6.E2PCONFIG E WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND D.PRODUCTID =E.PRODUCTID AND E.IMEI='" + tbIMEI.Text.Trim() + "' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                            //FA6 e2p 在 rdl前面  
                            //strsql = "SELECT  A.VALUE IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA6.RDLTESTSPECINFO A,FA6.RDLTESTSPECINFO B,FA6.RDLTESTSPECINFO C,FA6.RDLSTATIONINFO D WHERE A.UNIQUEID=B.UNIQUEID AND A.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND A.VALUE ='" + tbIMEI.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                    }
                    else
                    {
                        if (FA6FA1.Checked == true)
                        {
                            strsql = "SELECT  A.VALUE IMEI,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.PRODUCTID PID,D.TESTDATE FROM FA6.SIMLKTESTSPECINFO A,FA6.SIMLKTESTSPECINFO B,FA6.SIMLKTESTSPECINFO C,FA6.SIMLKSTATIONINFO D WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND D.PRODUCTID ='" + tbPID.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                        else
                        {
                            strsql = "SELECT  E.IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA6.RDLTESTSPECINFO B,FA6.RDLTESTSPECINFO C,FA6.RDLSTATIONINFO D,FA6.E2PCONFIG E WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND D.PRODUCTID =E.PRODUCTID AND E.PRODUCTID='" + tbPID.Text.Trim() + "' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                            //strsql = "SELECT  A.VALUE IMEI,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.PRODUCTID PID,D.TESTDATE FROM FA6.RDLTESTSPECINFO A,FA6.RDLTESTSPECINFO B,FA6.RDLTESTSPECINFO C,FA6.RDLSTATIONINFO D WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND D.PRODUCTID ='" + tbPID.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                    }
                        break;
                case "FA1":
                    if (!tbIMEI.Text.Trim().Equals(""))
                    {
                        if (FA6FA1.Checked == true)
                        {
                            strsql = "SELECT  A.VALUE IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA1.SIMLKTESTSPECINFO A,FA1.SIMLKTESTSPECINFO B,FA1.SIMLKTESTSPECINFO C,FA1.SIMLKSTATIONINFO D WHERE A.UNIQUEID=B.UNIQUEID AND A.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND A.VALUE ='" + tbIMEI.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                        else
                        {
                            strsql = "SELECT  E.IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA1.RDLTESTSPECINFO B,FA1.RDLTESTSPECINFO C,FA1.RDLSTATIONINFO D,FA1.E2PCONFIG E WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND D.PRODUCTID =E.PRODUCTID AND E.IMEI='" + tbIMEI.Text.Trim() + "' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                            //strsql = "SELECT  A.VALUE IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA1.RDLTESTSPECINFO A,FA1.RDLTESTSPECINFO B,FA1.RDLTESTSPECINFO C,FA1.RDLSTATIONINFO D WHERE A.UNIQUEID=B.UNIQUEID AND A.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND A.VALUE ='" + tbIMEI.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                    }
                    else
                    {
                        if (FA6FA1.Checked == true)
                        {
                            strsql = "SELECT  A.VALUE IMEI,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.PRODUCTID PID,D.TESTDATE FROM FA1.SIMLKTESTSPECINFO A,FA1.SIMLKTESTSPECINFO B,FA1.SIMLKTESTSPECINFO C,FA1.SIMLKSTATIONINFO D WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND D.PRODUCTID ='" + tbPID.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                        else
                        {
                            strsql = "SELECT  E.IMEI,D.PRODUCTID PID,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.TESTDATE FROM FA1.RDLTESTSPECINFO B,FA1.RDLTESTSPECINFO C,FA1.RDLSTATIONINFO D,FA1.E2PCONFIG E WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND D.PRODUCTID =E.PRODUCTID AND E.PRODUCTID='" + tbPID.Text.Trim() + "' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                            //strsql = "SELECT  A.VALUE IMEI,B.VALUE UNLOCKCODE,C.VALUE SIMLOCKCODE,D.PRODUCTID PID,D.TESTDATE FROM FA1.RDLTESTSPECINFO A,FA1.RDLTESTSPECINFO B,FA1.RDLTESTSPECINFO C,FA1.RDLSTATIONINFO D WHERE D.UNIQUEID=B.UNIQUEID AND D.UNIQUEID=C.UNIQUEID AND A.UNIQUEID=D.UNIQUEID  AND D.PRODUCTID ='" + tbPID.Text.Trim() + "' AND A.SPECNAME='IMEI' AND B.SPECNAME='UnlockCode' AND C.SPECNAME='SIMLockInfo'";
                        }
                    }
                    break;                
                case "BLA":
                    strsql = "SELECT IMEI,PRODUCTID PID,NKEY UNLOCKCODE,NSKEY SIMLOCKCODE,E2PDATE TESTDATE FROM BLA.KEY_LIST WHERE IMEI='" + tbIMEI.Text.Trim() + "' OR PRODUCTID= '" + tbPID.Text.Trim() + "'";
                    break;
                default:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不能查詢該機種，請聯繫資訊!');</script>");
                    return;
            }
            try
            {
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dgProduct.DataSource = dt.DefaultView;
                    dgProduct.DataBind();
                    Label1.Text = "Current Page:" + (dgProduct.PageIndex + 1).ToString() + "/" + dgProduct.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據不存在!');</script>");
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請確認你的查詢條件是否正確!');</script>");
                return;
            }
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
                orapara[0].Value = ddlModel.Text.Trim();
                orapara[1].Value = tbStartDate.Text.Trim();
                orapara[2].Value = tbEndDate.Text.Trim();
                orapara[3].Value = tbPID.Text.Trim();
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
                }
            }
            catch
            { 
            
            }
        }
} 
}
