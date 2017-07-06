/**************************************************************
 * 
 *  Unit description: Desktop Default Page
 *  Developer:              Date: 
 *  Modifier :                Date: 
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
//using Entity.EAI;
using WebControler.APS;

namespace Boundary.Web.APS
{
	/// <summary>
	/// Summary description for DesktopDefault.
	/// </summary>
	public partial class DesktopDefault : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			if (Request["__SCROLLPOS"] != null &&
				Request["__SCROLLPOS"] != String.Empty) 
			{ 
				int pos = Convert.ToInt32 (Request["__SCROLLPOS"]); 
				MainBody.Attributes["onscroll"]="javascript:document.all['__SCROLLPOS'].value = document.all['"
					+MainBody.ClientID+"'].scrollTop;"; 
				MainBody.Attributes["onload"] ="javascript:document.all['"+MainBody.ClientID+"'].scrollTop=" + pos; 
			}
			else 
			{
				MainBody.Attributes["onscroll"]="javascript:document.all['__SCROLLPOS'].value = document.all['"
					+MainBody.ClientID+"'].scrollTop;";
			}
			HtmlTableCell contentPane = (HtmlTableCell)Page.FindControl("ContentPane");
			if ((Request.QueryString["TabID"] == null) || (Request.QueryString["TabID"].ToString() == ""))
			{			    
				return;
			}

			string strUrl   = Request.QueryString["Url"].ToString();
            //ClsAuthority authority = new ClsAuthority();
            //if(strUrl != "WFrmChangePassword.ascx")// &&
            ////    !authority.CurrentUserAuthorizedTo(strUrl))
            //{
            //    return;
            //}
			string strTabID = Request.QueryString["TabID"].ToString();
			if (strTabID.Substring(0,4).Equals("M105"))  //直接轉向某個頁面，非Web使用者控制項
			{
				Response.Redirect(strUrl,false);
			}
			else
			{
				Session["TabID"] = strTabID;

				System.Web.UI.Control c = Page.LoadControl(strUrl);
				contentPane.Controls.Add(c);
				contentPane.Visible = true;
			}

			this.ClientScript.RegisterClientScriptBlock(this.GetType(),"SetLanguage","<script language=\"javascript\"> var LANGUAGE = '" 
				+ Session["Language"].ToString() +  "'; </script>");			
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

        //override VerifyRenderingInServerForm 

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
 

		#endregion
	}
}
