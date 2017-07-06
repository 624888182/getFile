namespace WebControler.APS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Threading;
	using System.Globalization;
	using System.Reflection;
	using System.Resources;
	using System.Xml;
	using System.Configuration;
    using WebControler.Security;
	using DBAccess.EAI;

	/// <summary>
	///		Summary description for HeadBar.
	/// </summary>
	public partial  class HeadBar : System.Web.UI.UserControl
	{
		protected string LogoffLink = "";
        protected string ChangePasswordLink = "";
		public int  tabIndex;

		protected System.Web.UI.WebControls.ImageButton ibPortal2;
		protected System.Web.UI.WebControls.ImageButton ibPortal1;
		protected System.Web.UI.WebControls.ImageButton ibPortal0;


		public bool ShowTabs = true;
		
		//用於記錄Session超時的情況
		private bool blIsSessionTimeout = false;
		

		protected void Page_Load(object sender, System.EventArgs e)
		{
			blIsSessionTimeout = false;

			if (!IsPostBack)
			{
				if (Session["Language"] == null)
				{
					if (Request.Cookies["Lan"] != null)
					{
						Session["Language"] = Request.Cookies["Lan"].Value;
					}
					else
					{
						Session["Language"] = "en-us";
					}
				}

				if (Session["Language"].ToString() == "en-us")
				{
					lbtnLanguage.Text = "中文";
				}
				else
				{
					lbtnLanguage.Text = "English";
				}

                //this.ci = new CultureInfo(Session["Language"].ToString());
                //Thread.CurrentThread.CurrentUICulture = this.ci;

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["Language"].ToString());
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Language"].ToString());

				string strSql = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME ="+ClsCommon.GetSqlString(Context.User.Identity.Name);
				DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
				ViewState["UserName"] = dt.Rows[0]["USERNAME"].ToString();
				if (Request.IsAuthenticated)
				{
                    hlWelCome.Text = (String)GetGlobalResourceObject("CommonRes", "Welcome") + "  " + ViewState["UserName"].ToString() + "!";//rmCommonRes.GetString("Welcome") + "  " + ViewState["UserName"].ToString()+"!"; //Context.User.Identity.Name + " !";
                    hlHome.Text = (String)GetGlobalResourceObject("CommonRes", "Home"); //rmCommonRes.GetString("Home");
                    hlChangePass.Text = (String)GetGlobalResourceObject("CommonRes", "ChangePass"); //rmCommonRes.GetString("ChangePass");
                    hlDefaultPage.Text = (String)GetGlobalResourceObject("CommonRes", "DefaultPage"); //rmCommonRes.GetString("DefaultPage");
                    hlLogOut.Text = (String)GetGlobalResourceObject("CommonRes", "Logout"); //rmCommonRes.GetString("Logout");
                    lblSiteName.Text = "<font size=8 color=red>" + ConfigurationManager.AppSettings["Factory"].ToString() + "</font>  " + (String)GetGlobalResourceObject("signin", "Portal1");//rmSignIn.GetString("Portal1");
					hlHome.NavigateUrl = Request.ApplicationPath;
					hlChangePass.NavigateUrl = Request.ApplicationPath + "/Boundary/DesktopDefault.aspx?TabID=999&Url=WFrmChangePassword.ascx";
					//hlLogOut.NavigateUrl = Request.ApplicationPath + "/Boundary/WFrmLogout.aspx";
					hlLogOut.NavigateUrl = "../Boundary/WFrmLogout.aspx";

					lbHomePage.Attributes["onclick"] = "this.style.behavior='url(#default#homepage)';this.setHomePage('http://10.76.32.61/sfcquery');";
                    lbHomePage.Text = (String)GetGlobalResourceObject("signin", "HomePage"); //rmSignIn.GetString("HomePage");

					lbFavorite.Attributes["onclick"] = "window.external.addFavorite('http://10.76.32.61/sfcquery','SFC Query')";
                    lbFavorite.Text = (String)GetGlobalResourceObject("signin", "Favorite"); //rmSignIn.GetString("Favorite");
				}

				//建立保存多國語言的ViewState變量
				this.InitializeMultilanguage();
				
				//hlWelCome.Attributes["onDblClick"] = "alert('"+rmSignIn.GetString("Portal1")+"     "+hlWelCome.Text+"')";
                hlWelCome.Attributes["onDblClick"] = "alert('" + (String)GetGlobalResourceObject("signin", "Portal1") + "     " + hlWelCome.Text + "')";


				
			}

			this.ShowTheMenu();

			//Session 超時了, 需要記錄下來, 然後導向首頁
			if (blIsSessionTimeout)
			{
				Response.Redirect(Request.ApplicationPath);					
			}
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void ShowTheMenu()
		{
			DataTable menuTable;

			#region Portal Function. Modified by Jack at 2006/04/21 10:34
			/*
			if (Session["Menu"] == null)
			{
				menuTable = GetAuthorizedMenuTable();
				Session["Menu"] = menuTable;
			}
			else
			{
				menuTable = (DataTable)Session["Menu"];
			}
			*/

			menuTable = GetAuthorizedMenuTable();

			#endregion

			XmlNode node;
			string strID       = "";
			string strTitle    = "";
			if (menuTable.Rows.Count == 0)
			{
				try
				{
					//SolpartMenu.AddMenuItem("0", rmMenu.GetString("NoMenu"), "");
                    SolpartMenu.AddMenuItem("0", (String)GetGlobalResourceObject("Menu", "NoMenu"), "");
				}
				catch
				{
					//No Action
				}
			}


			if (SolpartMenu != null)
			{
				foreach (DataRow row in menuTable.Rows)
				{
					if ( row["FParentID"].ToString().Trim() == "0" )
					{
						strID       = row["FTabID"].ToString().Trim();
						SolpartMenu.RemoveMenuItem(strID);			
					}
				}
			}
			
			foreach (DataRow row in menuTable.Rows)
			{
				if ( row["FParentID"].ToString().Trim() == "0" )
				{
					strID       = row["FTabID"].ToString().Trim();
					strTitle    = row["FDisplayText"].ToString().Trim();
					node = SolpartMenu.AddMenuItem(strID, " "+strTitle, "");
					SubMenuItem(strID, menuTable, node);					
				}
			}
		}


		private void SubMenuItem(string ParentID, DataTable menuTable, XmlNode ParentNode)
		{
			XmlNode node;
			string strID      = "";
			string strLink    = "";
			string strTitle   = "";
			foreach (DataRow row in menuTable.Rows)
			{
				if ( row["FParentID"].ToString().Trim() == ParentID )
				{
					strID    = row["FTabID"].ToString().Trim();
					strLink  = row["FDesktopSrc"].ToString().Trim();
					strTitle = row["FDisplayText"].ToString().Trim();
					if ((bool)row["FParentMenu"])  //have subMenu
					{
						node = SolpartMenu.AddMenuItem(ParentNode, strID, strTitle, "", "", false, "", "");
					}
					else
					{
						if(strID=="11001")
						{						
							node = SolpartMenu.AddMenuItem(ParentNode, strID, "<div onclick=\"javascript:window.open('" + Request.ApplicationPath + "/Help/Josh_-APS_System_User_Guide(050119).htm','_help','')\">"+strTitle+"</div>", "", "", false, "", "");
						}
						else
						{						
							node = SolpartMenu.AddMenuItem(ParentNode, strID, strTitle,
								"DesktopDefault.aspx?TabID=" + strID + "&Url=" + strLink, "", false, "", "");
						}
					}
					SubMenuItem(strID, menuTable, node);
				}
			}
		}


		private DataTable GetAuthorizedMenuTable()
		 {
			DataTable dtAllMenuTable = new DataTable();
			DataTable dtParentTable  = new DataTable();
			DataTable dtReturnTable  = null;		

			#region 支持多個Portal, 且每個模組可以屬於多個Portal	
//			string strSQL = 
//				  " Select tblTabs.FTabID, tblTabs.FParentID, tblTabs.FAuthorizedRoles, tblTabsDefinitions.FDesktopSrc "
//				+ " from tblTabs JOIN tblTabsDefinitions ON tblTabsDefinitions.FTabID = tblTabs.FTabID "
//				+ " where substr(tblTabs.FPortalID3, length(tblTabs.FPortalID3) - {0}, 1) = '1' "
//				+ " and tblTabs.FPortalID=0 "
//				+ " and tblTabs.FShowTab=1 "
//				+ " Order by tbltabs.ftaborder, tbltabs.ftabid";			
            //string strSQL ="SELECT A.FTABID,A.FPARENTID,A.FAUTHORIZEDROLES,B.FDESKTOPSRC FROM TBLTABS A,TBLTABSDEFINITIONS B,TBLMENUPRIVILEGES C "
            //    +" WHERE A.FTABID = B.FTABID AND B.FTABID = C.MENUID AND SUBSTR(A.FPORTALID3, LENGTH(A.FPORTALID3) - 0, 1) = '1' AND C.USERID = '"+Context.User.Identity.Name + "'"
            //    +" AND A.FPORTALID = 0  AND A.FSHOWTAB = 1  ORDER BY A.FTABORDER, A.FTABID";

            string strSQL = "SELECT B.FTABID, B.FPARENTID, B.FAUTHORIZEDROLES, B.FDESKTOPSRC FROM TBLTABSDEFINITIONS B, TBLMENUPRIVILEGES C "
                +" WHERE B.FTABID = C.MENUID AND SUBSTR(B.FPORTALID, LENGTH(B.FPORTALID) - 0, 1) = '1' AND C.USERID = '"+Context.User.Identity.Name + "'"
                +" AND B.FSHOWTAB = 1 ORDER BY B.FTABORDER, B.FTABID";
			#endregion

			strSQL = string.Format(strSQL, 0);


			dtAllMenuTable = ClsGlobal.objDataConnect.DataQuery(strSQL).Tables[0];

			dtAllMenuTable.Columns.Add("FParentMenu", typeof(bool));
			dtAllMenuTable.Columns.Add("FDisplayText", typeof(String));
			foreach (DataRow dataRow in dtAllMenuTable.Rows)
			{
				dataRow["FParentMenu"]  = false;
				
				/*
				 * 2006/12/10 13:16 Modified  用於修正當在User端Session超時時, 中文環境下的菜單會全部變為英文
				 * 
				 * 原始語句:
				 * 
				 * dataRow["FDisplayText"] = rmMenu.GetString(dataRow["FTabID"].ToString());
				 * 
				 * 將上條語句改為以下:
				*/
				dataRow["FDisplayText"] = ViewState[dataRow["FTabID"].ToString()];
			}
			dtReturnTable = dtAllMenuTable.Clone();

			// Add every authorized menu item to the table
			int intColumns = dtAllMenuTable.Columns.Count;
			foreach (DataRow row in dtAllMenuTable.Rows)
			{
				if ( PortalSecurity.IsInRoles(row["FAuthorizedRoles"].ToString()) )
				{
					object[] aArray = new object[intColumns];
					row.ItemArray.CopyTo(aArray, 0);
					dtReturnTable.Rows.Add(aArray);
				}
			}
			
			dtAllMenuTable.Dispose();
			dtAllMenuTable = null;

			strSQL = "Select distinct FParentID from tblTabs "
				+ "where FPortalID=0 and FParentID<>0 Order by FParentID DESC";  //找出沒有SubMenu的Menu，並刪除之
			dtParentTable = ClsGlobal.objDataConnect.DataQuery(strSQL).Tables[0];
			DataRow[] myRow;
			bool blnHasSub;
			foreach (DataRow row in dtParentTable.Rows)
			{
				blnHasSub = true;
				if ( dtReturnTable.Select("FParentID=" + row["FParentID"].ToString()).Length == 0 )
				{
					blnHasSub = false;
				}

				myRow = dtReturnTable.Select("FTabID=" + row["FParentID"].ToString());

				if (myRow.Length != 0) // Jack 修改
				{					
					if (blnHasSub)
					{
						myRow[0]["FParentMenu"] = true;
					}
					else
					{
						myRow[0].Delete();					
					}
				}
			}
			dtReturnTable.AcceptChanges();

			return dtReturnTable;
		}


		// 用於修正當在User端Session超時時, 中文環境下的菜單會全部變為英文
		private void InitializeMultilanguage() ////初始化多國語言的 ViewState 變量, 及給界面控件設置多國語言
		{
			String strSQL
				= "SELECT ftabid FROM tbltabs ";

			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSQL).Tables[0];

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach(DataRow row in dt.Rows)
				{
					try
					{
                        ViewState[row["ftabid"].ToString()] = (String)GetGlobalResourceObject("Menu", row["ftabid"].ToString());// rmMenu.GetString(row["ftabid"].ToString());
					}
					catch
					{
						//出錯時忽略
					}
				}
			}
		}


		protected void lbtnLanguage_Click(object sender, System.EventArgs e)
		{
			if (Session["Language"].ToString() == "en-us")
			{
				lbtnLanguage.Text = "English";
				Session["Language"] = "zh-tw";
			}
			else
			{
				lbtnLanguage.Text = "中文";
				Session["Language"] = "en-us";
			}
			Response.Cookies["Lan"].Value = Session["Language"].ToString();


            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["Language"].ToString());
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Language"].ToString());

			if (Request.IsAuthenticated)
			{
                //hlWelCome.Text    = rmCommonRes.GetString("Welcome") + "  " +  ViewState["UserName"].ToString()+"!";//Context.User.Identity.Name + " !";
                //hlHome.Text       = rmCommonRes.GetString("Home");
                //hlChangePass.Text = rmCommonRes.GetString("ChangePass");
                //hlLogOut.Text     = rmCommonRes.GetString("Logout");
                hlWelCome.Text = (String)GetGlobalResourceObject("CommonRes", "Welcome");//rmCommonRes.GetString("Welcome") + "  " + ViewState["UserName"].ToString()+"!"; //Context.User.Identity.Name + " !";
                hlHome.Text = (String)GetGlobalResourceObject("CommonRes", "Home"); //rmCommonRes.GetString("Home");
                hlChangePass.Text = (String)GetGlobalResourceObject("CommonRes", "ChangePass"); //rmCommonRes.GetString("ChangePass");
                hlDefaultPage.Text = (String)GetGlobalResourceObject("CommonRes", "DefaultPage"); //rmCommonRes.GetString("DefaultPage");
                hlLogOut.Text = (String)GetGlobalResourceObject("CommonRes", "Logout"); //rmCommonRes.GetString("Logout");

				hlHome.NavigateUrl = Request.ApplicationPath;
				hlChangePass.NavigateUrl = Request.ApplicationPath + "/Boundary/DesktopDefault.aspx?TabID=999&Url=WFrmChangePassword.ascx";
				hlLogOut.NavigateUrl =  "../Boundary/WFrmLogout.aspx";
			}


            hlWelCome.Attributes["onDblClick"] = "alert('" + (String)GetGlobalResourceObject("signin", "Portal1") + "     " + hlWelCome.Text + "')";
			Session["Menu"] = null;
			
			//刷新保存多國語言的ViewState變量
			this.InitializeMultilanguage();

			ShowTheMenu();
			Response.Redirect(Request.Url.ToString());	
		}



		protected void hlDefaultPage_Click(object sender, System.EventArgs e)
		{			
			string strScript = "<script language='javascript'>"
				+ " window.showModalDialog('WFrmDesktopSrc.aspx?UserID="
				+ Server.UrlEncode(Context.User.Identity.Name) 
				+ "&MyDate=' + Date(), null, 'dialogWidth:400px;dialogHeight:250px;center:1;scroll:0;help:0;status:0;resizable:yes');"
				+ "</script>";

			Page.ClientScript.RegisterStartupScript(this.GetType(),"ShowDialog", strScript);
		}
	}
	
}
