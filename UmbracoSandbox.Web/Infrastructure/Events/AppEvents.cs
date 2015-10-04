namespace UmbracoSandbox.Web.Infrastructure.Events
{
    using System.Web.Http;
    using System.Web.Routing;

    using Umbraco.Core;
    using Umbraco.Core.Services;

    using UmbracoSandbox.Web.Infrastructure.Routing;

    public class AppEvents : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            GlobalConfiguration.Configuration.MessageHandlers.Add(new NotificationHandler());
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ContentService.Published += UmbracoEvents.Published;
        }
    }
}
