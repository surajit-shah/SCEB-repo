<%@Application Language='C#' Inherits="Sitecore.Web.Application" %>

<%-- WARNING: Every custom application must derive from the Sitecore.Web.Application class. Otherwise some Sitecore features will not be available or will not work correctly. --%> 
<script RunAt="server">
    public void Application_Start()
    {
        //RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
    public void Application_End()
    {

    }
    public void Application_Error(object sender,EventArgs args)
    {

    }
</script>