/*************************************************************************
 * 
 *  Unit description: Search the test station data for Pass/Fail
 *  Developer: Shu Jian Bo             Date: 2007/01/25
 *  Modifier : Shu Jian Bo             Date: 2007/04/10
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
	using DB.EAI;
	using System.Reflection;
	using System.Globalization;
	using System.Resources;
	using DBAccess.EAI;
	using System.Data.OracleClient;

	/// <summary>
	///		WFrmScanHistory 的摘要描述。
	/// </summary>
	public partial class WFrmScanHistory : System.Web.UI.UserControl
	{
        //protected System.Data.OracleClient.OracleConnection oracleConnection1;
        //private static ResourceManager rm = new ResourceManager("SFCQuery.MultiLanguage.SFCQuery", Assembly.GetExecutingAssembly());

		protected void Page_Load(object sender, System.EventArgs e)
		{

			// 在這裡放置使用者程式碼以初始化網頁
			if (!IsPostBack)
			{
				MultiLanguage();
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
			//this.oracleConnection1 = new System.Data.OracleClient.OracleConnection();
			this.dgPanelInfo.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgPanelInfo_PageIndexChanged);

		}
		#endregion

		private void MultiLanguage()
		{
            //modified by shujianbo at 2007/11/28  upgrade to vs 2005
            Label1.Text = (String)GetGlobalResourceObject("SFCQuery", "WoInfo");//rm.GetString("WoInfo");
            Label2.Text = (String)GetGlobalResourceObject("SFCQuery", "ProductInfo");//rm.GetString("ProductInfo");
            Label3.Text = (String)GetGlobalResourceObject("SFCQuery", "IMEIRelationShip");//rm.GetString("IMEIRelationShip");
            dgWO.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");//rm.GetString("WO");
            dgWO.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Model");//rm.GetString("Model");
            dgWO.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Item");//rm.GetString("Item");
            dgWO.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WOQty");//rm.GetString("WOQty");

            dgProduct.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "CDate");//rm.GetString("CDate");
            dgProduct.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "StationID");//rm.GetString("StationID");
            dgProduct.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "StateID");//rm.GetString("StateID");
            dgProduct.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Line");//rm.GetString("Line");
            dgProduct.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "EmpID");//rm.GetString("EmpID");

            dgE2P.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "ComputerName");//rm.GetString("ComputerName");
            dgE2P.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "WO");//rm.GetString("WO");
            dgE2P.Columns[6].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "E2PDate");//rm.GetString("E2PDate");
            dgE2P.Columns[7].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "StateID");//rm.GetString("StateID");
            dgE2P.Columns[8].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "ErrorMsg");//rm.GetString("ErrorMsg");
            dgE2P.Columns[9].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "EmpID");//rm.GetString("EmpID");

            dgShip.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "INVOICENUMBER");
            dgShip.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "ITEMNUMBER");
            dgShip.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "QUANTITY");
            dgShip.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "INTERNALCARTON");
            dgShip.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "SHIPDATE"); 
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
            if (tbProductID.Text.Trim().Equals(""))
            {
                Label4.Visible = true;
                Label4.Text = "請輸入IMEI或PID！";
                return;
            }
			Label1.Visible = true;
			Label2.Visible = true;
			Label3.Visible = true;
			Label5.Visible = true;
			lblPanelInfo.Visible = true;

            DatagridLink.DataSource = null;
            DatagridLink.DataBind();

			ClsDBScanHistory objScanHistory = new ClsDBScanHistory();
			string ReturnMsg = "";
            int InputLength = tbProductID.Text.Trim().Length;
            string strModel = "";
            string strModel1 = "";
            //by litao add 2011/4/7 start
            string strProductid = tbProductID.Text.Trim().ToUpper();
            if (InputLength == 10)
            {
                string strSqlTemp = "select PRODUCT_ID FROM  CMCS_SFC_IMEINUM where SERIAL_NUM='" + tbProductID.Text.Trim().ToUpper() +"'";
                DataTable dtTemp = ClsGlobal.objDataConnect.DataQuery(strSqlTemp).Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    strProductid = dtTemp.Rows[0]["PRODUCT_ID"].ToString();
                }
            }
            string strSql = "SELECT DB_USER,SPART FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL " +
                   "WHERE SPART = (SELECT SPART FROM SFC.PRODUCT_PANEL_SORDER WHERE PRODUCT_ID = '"
                   + strProductid + "')";
            //by litao add 2011/4/7 end

            //string strSql = "SELECT DB_USER,SPART FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL "+
               //    "WHERE SPART = (SELECT SPART FROM SFC.PRODUCT_PANEL_SORDER WHERE PRODUCT_ID = "
               //    +ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper())+")";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];            

            //string strSql = "SELECT * FROM CDMA_SERIAL_NO T WHERE T.SERIAL_NUM =" + ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper());
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];


            if (tbProductID.Text.Trim().Substring(0, 1).ToUpper().Equals("A"))
                InputLength = 15;

            if (dt.Rows.Count > 0)
            {
                 
                InputLength = 14;
                strModel = dt.Rows[0]["DB_USER"].ToString();
                strModel1 = dt.Rows[0]["SPART"].ToString().Substring(2,3);
                if (strModel.Equals("HER") && strModel==strModel1)
                    strModel = "HAIER";
                else
                    if(strModel.Equals("010"))
                        strModel = "RUY";
            }

            switch (InputLength)
			{
                case 14:  //Product ID
					//ReturnMsg = objScanHistory.setProductID(tbProductID.Text.Trim().ToUpper(),strModel);
                    ReturnMsg = objScanHistory.setProductID(strProductid, strModel);//by litao add 2011/4/7
					
                    objScanHistory.FLabelType = 0 ;
                    break;
                case 8:
				case 15:  //IMEI
                    if (strModel.Equals("HAIER"))
                    {
                        ReturnMsg = objScanHistory.setProductID(tbProductID.Text.Trim().ToUpper(), strModel);
                        objScanHistory.FLabelType = 0;
                    }
                    else
                    { 
                        ReturnMsg = objScanHistory.setIMEI(tbProductID.Text.Trim().ToUpper());
					    objScanHistory.FLabelType = 1 ;
                        objScanHistory.FIMEI = tbProductID.Text.ToUpper().Trim();
                    }
					
					break;

                case 16:  //PPID
                    ReturnMsg = objScanHistory.setIMEI(tbProductID.Text.Trim().ToUpper());
                    objScanHistory.FLabelType = 1;
                    objScanHistory.FIMEI = tbProductID.Text.ToUpper().Trim();
                    break;
                case 20:  //PPID
                    ReturnMsg = objScanHistory.setIMEI(tbProductID.Text.Trim().ToUpper());
                    objScanHistory.FLabelType = 1;
                    objScanHistory.FIMEI = tbProductID.Text.ToUpper().Trim();
                    break;
                case 7:
				case 10:  //Picasso
					ReturnMsg = objScanHistory.setPICASSO(tbProductID.Text.Trim().ToUpper());
					objScanHistory.FLabelType = 2 ;
                    objScanHistory.FPicasso = tbProductID.Text.ToUpper().Trim();
                    //if (!ReturnMsg.Equals(""))  //CDMA's PID
                    //{
                    //    string strSql = "SELECT * FROM CDMA_SERIAL_NO T WHERE T.SERIAL_NUM =" + ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper());
                    //    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        objScanHistory.FProductID = tbProductID.Text.ToUpper().Trim();
                    //    }
                    //}
					break;
				default :
					ReturnMsg = "Input Error!";
					break;
			}

			if (!ReturnMsg.Equals(""))
			{
				Label4.Text = ReturnMsg;
				Label4.Visible = true;
				Label1.Visible = false;
				Label2.Visible = false;
				Label3.Visible = false;
				Label5.Visible = false;
				lblPanelInfo.Visible = false;
				dgWO.Visible = false;
				dgRelationShip.Visible = false;
				dgProduct.Visible = false;
				dgE2P.Visible = false;
				dgPanelInfo.Visible = false;
                dgShip.Visible = false;
				return;
			}
			Label4.Visible = false;
			Label1.Visible = true;
			Label2.Visible = true;
			Label3.Visible = true;
			Label5.Visible = true;
            Label6.Visible = true;
			lblPanelInfo.Visible = true;
			dgWO.Visible = true;
			dgRelationShip.Visible = true;
			dgProduct.Visible = true;
			dgE2P.Visible = true;
			dgPanelInfo.Visible = true;
            dgShip.Visible = true;
			//DataTable dt = objScanHistory.GetWoInfo(objScanHistory.FProductID);
			string strProcedureName = "SFCQUERY.GETWOINFO";
			OracleParameter[] oraPara = new OracleParameter[]{new OracleParameter("PRODUCTID",OracleType.VarChar,20),
																 new OracleParameter("DATA",OracleType.Cursor)};
			oraPara[0].Value = objScanHistory.FProductID;
			oraPara[1].Direction = ParameterDirection.Output;
			dt =ClsGlobal.objDataConnect.DataQuery(strProcedureName,oraPara).Tables[0];
            //DataSet ds = new DataSet();
            //ds = ClsGlobal.objDataConnect.DataQuery(strProcedureName, oraPara); 
			dgWO.DataSource = dt.DefaultView;
			dgWO.DataBind();
			
			//判斷是不是PCR1手機
			foreach(DataRow rw in dt.Rows)
			{
				if (rw["WO_NO"].ToString().ToUpper().Equals("A1760504") || rw["WO_NO"].ToString().ToUpper().Equals("P1760504") || rw["WO_NO"].ToString().ToUpper().Equals("RP176151"))
				{
					Label4.Visible = true;
					Label4.Text = "此ID是PCR手機！";
					break;
				}
			}

			//dt = objScanHistory.GetProcessInfo(objScanHistory.FProductID,objScanHistory.FPanelID);
			strProcedureName = "SFCQUERY.GETPROCESSINFO";
			oraPara = new OracleParameter[]{new OracleParameter("PRODUCTID",OracleType.VarChar,20),
											new OracleParameter("PANELID",OracleType.VarChar,9),
											new OracleParameter("DATA",OracleType.Cursor)};
			oraPara[0].Value = objScanHistory.FProductID;
			oraPara[1].Value = objScanHistory.FPanelID;
			oraPara[2].Direction = ParameterDirection.Output;
			dt =ClsGlobal.objDataConnect.DataQuery(strProcedureName,oraPara).Tables[0];
			dgProduct.DataSource = dt.DefaultView; 
			dgProduct.DataBind();

			//dt = objScanHistory.GetRelationShipInfo(objScanHistory.FProductID);
			strProcedureName = "SFCQUERY.GETRELATIONSHIPINFO";
			oraPara = new OracleParameter[]{new OracleParameter("PRODUCTID1",OracleType.VarChar,14),
											new OracleParameter("DATA",OracleType.Cursor)};
			oraPara[0].Value = objScanHistory.FProductID;
			oraPara[1].Direction = ParameterDirection.Output;
			dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName,oraPara).Tables[0];
			dgRelationShip.DataSource = dt.DefaultView;
			dgRelationShip.DataBind();

            if (!(strModel.Equals("HAIER")))
            { 
                strProcedureName = "SFCQUERY.GETE2PINFO";
			    oraPara = new OracleParameter[]{new OracleParameter("PRODUCTID",OracleType.VarChar,14),
											       new OracleParameter("INPUTDATA",OracleType.VarChar,20),
											       new OracleParameter("DATA",OracleType.Cursor)};
			    oraPara[0].Value = objScanHistory.FProductID;
			    switch(objScanHistory.FLabelType)
			    {
				    case 0:
					    oraPara[1].Value = objScanHistory.FProductID;
					    break;
				    case 1:
					    oraPara[1].Value = objScanHistory.FIMEI;
					    break;
				    case 2:
					    oraPara[1].Value = objScanHistory.FPicasso;
					    break;
			    }
			    oraPara[2].Direction = ParameterDirection.Output;
                try
                {
                    dgE2P.Visible = true;
                    dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, oraPara).Tables[0];
                    dgE2P.DataSource = dt.DefaultView;
                    dgE2P.DataBind();
                }
                catch
                {
                    dgE2P.Visible = false;
                }
            }

            string strsql = "select c.invoice_number,c.item_number,c.quantity,c.internal_carton,c.ship_date from SHP.CMCS_SFC_SHIPPING_DATA a,SAP.CMCS_SFC_PACKING_LINES_ALL c where a.carton_no=c.internal_carton and a.productid='" + objScanHistory.FProductID + "'";
            dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            dgShip.DataSource = dt.DefaultView;
            dgShip.DataBind();
//			switch(objScanHistory.FLabelType)
//			{
//				case 0:
//					dt = objScanHistory.GetE2PInfo(objScanHistory.FProductID,objScanHistory.FProductID,objScanHistory.FLabelType);
//					break;
//				case 1:
//					dt = objScanHistory.GetE2PInfo(objScanHistory.FProductID,objScanHistory.FIMEI,objScanHistory.FLabelType);
//					break;
//				case 2:
//					dt = objScanHistory.GetE2PInfo(objScanHistory.FProductID,objScanHistory.FPicasso,objScanHistory.FLabelType);
//					break;
//			}
			//dt = objScanHistory.GetE2PInfo(objScanHistory.FProductID,objScanHistory.FLabelType);
            
        
			strProcedureName = "SFCQUERY.GETPANELINFO";
			oraPara = new OracleParameter[]{new OracleParameter("PANELID",OracleType.VarChar,20),
											   new OracleParameter("DATA",OracleType.Cursor)};
			oraPara[0].Value = objScanHistory.FPanelID;
			oraPara[1].Direction = ParameterDirection.Output;
			dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName,oraPara).Tables[0];
			//dt = objScanHistory.GetPanelInfo(objScanHistory.FPanelID);
			dgPanelInfo.DataSource = dt.DefaultView;
			dgPanelInfo.DataBind();
			//string test = objScanHistory.FProductID;
			Session["PanelID"] = objScanHistory.FPanelID;

            Label7.Visible = true;
            DatagridLink.DataSource = objScanHistory.GetLinkInfo(objScanHistory.FProductID, tbProductID.Text.Trim().ToUpper(), objScanHistory.FLabelType);
            DatagridLink.DataBind();
            
			dt.Dispose();
			GC.Collect();
		}

//		public DataTable FilterByCondition(string moduleID,string moduleName)
//		{
//			string sProcedureName = "SystemManagement_Modules.SelectModule";			
//			OracleParameter[] parms =  new OracleParameter[]{new OracleParameter("Pcondition",OracleType.VarChar),
//																new OracleParameter("PdataTable",OracleType.Cursor)};
//			parms[0].Value = "";
//			parms[1].Direction = ParameterDirection.Output;
//			ClsGlobal.objDataConnect.DataExecute(sProcedureName,parms);
//			return (DataTable)parms[1].Value;
//		} 

		private void dgPanelInfo_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgPanelInfo.CurrentPageIndex = e.NewPageIndex;
			//ClsDBScanHistory obj = new ClsDBScanHistory();
			//obj.FPanelID = Session["PanelID"].ToString();
			string FPanelID = Session["PanelID"].ToString();
			string strProcedureName = "SFCQUERY.GETPANELINFO";
			OracleParameter[] oraPara = new OracleParameter[]{new OracleParameter("PANELID",OracleType.VarChar,20),
											   new OracleParameter("DATA",OracleType.Cursor)};
			oraPara[0].Value = FPanelID;
			oraPara[1].Direction = ParameterDirection.Output;
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName,oraPara).Tables[0];

			//DataTable dt = obj.GetPanelInfo(obj.FPanelID);
			dgPanelInfo.DataSource = dt.DefaultView;
			dgPanelInfo.DataBind();

			dt.Dispose();
			GC.Collect();
		}
        protected void dgPanelInfo_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "PANELID")
            {
                string strPanelID = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text;

                string strScript = "<script language='jscript'>var res = window.open('./WFrmModalDialog.aspx?Url=./WFrmPanelInfoQuery.ascx&PanelID=" + strPanelID + "','_blank', '');</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);	
            }
        }
}
}
