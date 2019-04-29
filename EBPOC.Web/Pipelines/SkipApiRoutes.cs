namespace EBPOC.Web.Pipelines
{
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using System.Web.Routing;

    // TODO: \App_Config\include\SkipApiRoutes.config created automatically when creating SkipApiRoutes class.

    public class SkipApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}