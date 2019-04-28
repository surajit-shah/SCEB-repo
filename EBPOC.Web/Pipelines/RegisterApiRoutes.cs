namespace EBPOC.Web.Pipelines
{
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using System.Web.Routing;

    // TODO: \App_Config\include\RegisterApiRoutes.config created automatically when creating RegisterApiRoutes class.

    //public class RegisterApiRoutes : Sitecore.Mvc.Pipelines.Loader.InitializeRoutes
    //{
    //    public override void Process(PipelineArgs args)
    //    {
    //        //RouteConfig.RegisterRoutes(RouteTable.Routes);
    //    }
    //}
    public class RegisterApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            //Log.Info("Sitecore is starting", this);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}