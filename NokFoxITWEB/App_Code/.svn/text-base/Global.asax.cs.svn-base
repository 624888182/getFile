using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Security;
using System.Security.Principal;
using System.Web.Security;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using Control.EAI;

//using WebControler.APS;

namespace SFCQuery 
{
	public class Global : System.Web.HttpApplication 
	{		
		protected string userAccount;
		protected string bOrSer;
		protected string buyerId;
		protected string bEmployeeId;
		protected string sellerId;
		protected string sEmployeeId;
		protected string authorizedPurcharser;
		protected string authorizedFact;

		//*********************************************************************
		//
		// Application_BeginRequest Event
		//
		// The Application_BeginRequest method is an ASP.NET event that executes 
		// on each web request into the portal application.  The below method
		// obtains the current tabIndex and TabId from the querystring of the 
		// request -- and then obtains the configuration necessary to process
		// and render the request.
		//
		// This portal configuration is stored within the application's "Context"
		// object -- which is available to all pages, controls and components
		// during the processing of a single request.
		// 
		//*********************************************************************
        
		protected void Application_Start(object sender,EventArgs e)	
		{
		}

		protected void Application_End(object sender, EventArgs e)
		{
//			ClsUserLogControl clsUserLogControl = new ClsUserLogControl();
//			clsUserLogControl.RequestToEdit( Session.SessionID ); 
		}

		protected void Session_Start(object sender,EventArgs e)
		{
			Session.Timeout = 90;				
		}

		protected void Application_BeginRequest(object sender, EventArgs e) 
		{
		}
                          
		//*********************************************************************
		//
		// Application_AuthenticateRequest Event
		//
		// If the client is authenticated with the application, then determine
		// which security roles he/she belongs to and replace the "User" intrinsic
		// with a custom IPrincipal security object that permits "User.IsInRole"
		// role checks within the application
		//
		// Roles are cached in the browser in an in-memory encrypted cookie.  If the
		// cookie doesn't exist yet for this session, create it.
		//
		//*********************************************************************

		protected void Application_AuthenticateRequest(Object sender, EventArgs e) 
		{
			/*if (Request.IsAuthenticated == true) 
			{
				string[] roles;

				// Create the roles cookie if it doesn't exist yet for this session.
				if ((Request.Cookies["SFCQuery_PortalRoles"] == null) || (Request.Cookies["SFCQuery_PortalRoles"].Value == "")) 
				{
					// Get roles from UserRoles table, and add to cookie
					UsersDB user = new UsersDB();
					roles = user.GetRoles(User.Identity.Name);
                
					// Create a string to persist the roles
					string roleStr = "";
					foreach (string role in roles) 
					{
						roleStr += role;
						roleStr += ";";
					}

					// Create a cookie authentication ticket.
					FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
						1,                              // version
						Context.User.Identity.Name,     // user name
						DateTime.Now,                   // issue time
						DateTime.Now.AddSeconds(2),     // expires every hour
						false,                          // don't persist cookie
						roleStr                         // roles
						);

					// Encrypt the ticket
					string cookieStr = FormsAuthentication.Encrypt(ticket);

					// Send the cookie to the client
					Response.Cookies["SFCQuery_PortalRoles"].Value = cookieStr;
					Response.Cookies["SFCQuery_PortalRoles"].Path = "/";
					Response.Cookies["SFCQuery_PortalRoles"].Expires = DateTime.Now.AddMinutes(1);
				}
				else 
				{
					// Get roles from roles cookie
					FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Context.Request.Cookies["SFCQuery_PortalRoles"].Value);

					//convert the string representation of the role data into a string array
					ArrayList userRoles = new ArrayList();

					foreach (string role in ticket.UserData.Split( new char[] {';'} )) 
					{
						if ( !role.Equals("") )
							userRoles.Add(role);
					}

					roles = (string[]) userRoles.ToArray(typeof(String));
				}                                				

				// Add our own custom principal to the request containing the roles in the auth ticket
				Context.User = new GenericPrincipal(Context.User.Identity, roles);
			}*/
		}	
			
	}
}