<%@ Application Language="C#"%>
<%@ Import Namespace="System.Globalization" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Session.Timeout = 120;   
       
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Session_OnEnd(object sender, EventArgs e)
    {
       
    }

    void Application_AcquireRequestState( Object sender, EventArgs e ) {
        if( System.Web.HttpContext.Current.Session != null ) {
            CultureInfo ci = (CultureInfo)System.Web.HttpContext.Current.Session["culture"];
            if( ci != null ) {
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture( ci.Name );
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            }
        }
    }
       
</script>
