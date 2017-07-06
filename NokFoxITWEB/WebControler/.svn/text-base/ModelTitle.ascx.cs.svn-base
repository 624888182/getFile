
namespace WebControler.APS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using System.Resources;
	using System.Reflection;
	using System.Threading;
	using System.Web.Security;
    using WebControler.Security;
	using DBAccess.EAI;


	/// <summary>
	///		Summary description for ModelTitle.
	/// </summary>
	public partial  class ModelTitle : System.Web.UI.UserControl
	{
        //protected CultureInfo ci = new CultureInfo(CultureInfo.CurrentCulture.Name);
        //private ResourceManager rmMenu      = new ResourceManager("SFCQuery.MultiLanguage.Menu", Assembly.GetExecutingAssembly());

		protected void Page_Load(object sender, System.EventArgs e)
		{			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
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
            //this.ci = new CultureInfo(Session["Language"].ToString());
            //Thread.CurrentThread.CurrentUICulture = this.ci;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["Language"].ToString());
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Language"].ToString());

			/* // Ensure that the visiting user has access to the current page
			if (Session["MenuTable"] == null)
			{
				Session["MenuTable"] = GetMenuRoles.SetMenuRoles();
			}
			// Ensure the Role Authority
			if (Session["AuthorityOfRole"] == null)
			{
				Session["AuthorityOfRole"] = GetMenuRoles.GetRoleAuthority(Context.User.Identity.Name);
			}
			// Ensure the url */
			string strTabID = "";
			if (Session["TabID"] == null)
			{
				//Response.Redirect(Request.ApplicationPath);
			}
			else
			{
				strTabID = Session["TabID"].ToString();
				Session.Remove("TabID");
			}/*
			DataRow[] drMenu = ((DataTable)Session["MenuTable"]).Select("TabID="+strTabID);
			string strRoles = "";
			if (drMenu.Length == 0)
			{
				Response.Redirect(Request.ApplicationPath);
			}
			else
			{
				strRoles = drMenu[0]["Roles"].ToString();
			}*/ string strRoles = "";
			if (PortalSecurity.IsInRoles(strRoles) == false) 
			{
				// Log User Off from Cookie Authentication System
				FormsAuthentication.SignOut();
      
				// Invalidate roles token
				Response.Cookies["SFCQuery_PortalRoles"].Value = null;
				Response.Cookies["SFCQuery_PortalRoles"].Expires = DateTime.Now.AddMilliseconds(1);
				Response.Cookies["SFCQuery_PortalRoles"].Path = "/";	

				Session.RemoveAll();

				Response.Redirect(Request.ApplicationPath);
			}

            lblModelTitle.Text = (String)GetGlobalResourceObject("menu", strTabID); //rmMenu.GetString(strTabID);

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
	}
}
