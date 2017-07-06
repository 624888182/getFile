using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class MainNokPrg_IHub_Header : System.Web.UI.UserControl
{
    string strUserName = "";
    string Foxconn_Location = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //strUserName = Session["UserName"].ToString();
        //Foxconn_Location = Session["Region"].ToString();
        //Label1.Text = "[ " + Foxconn_Location + ":" + strUserName + " ]";
        //foreach(MenuItem item in Menu1.Items)
        //{
        //    if (Path.GetFileName(item.NavigateUrl).Equals(Path.GetFileName(Request.PhysicalPath), StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        item.Selected = true;
        //    }          
        //}
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        //Response.Write("<SCRIPT   LANGUAGE='JavaScript'>window.close();</SCRIPT> ");
        Server.Transfer("I_HubLogin.aspx");
    }
}