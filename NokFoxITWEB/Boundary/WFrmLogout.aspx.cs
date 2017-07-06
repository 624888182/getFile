/**************************************************************
 * 
 *  Unit description: Logout Page
 *  Developer: Nek.Chan             Date: 2003/11/11
 *  Modifier : Nek.Chan             Date: 2003/11/26 
 * 
 **************************************************************/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace Boundary.Web.APS
{
	/// <summary>
	/// Summary description for WFrmLogout.
	/// </summary>
	public partial class WFrmLogout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Log User Off from Cookie Authentication System
			FormsAuthentication.SignOut();
      
			// Invalidate roles token
			Response.Cookies["SFCQuery_PortalRoles"].Value = null;
			Response.Cookies["SFCQuery_PortalRoles"].Expires = DateTime.Now.AddMilliseconds(1);
			Response.Cookies["SFCQuery_PortalRoles"].Path = "/";

			Session.RemoveAll();
        
			// Redirect user back to the Portal Home Page
			Response.Redirect(Request.ApplicationPath);
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
