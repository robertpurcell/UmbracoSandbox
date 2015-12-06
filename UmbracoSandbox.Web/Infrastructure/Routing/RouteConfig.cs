namespace UmbracoSandbox.Web.Infrastructure.Routing
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Throw Exception",
                "ThrowException",
                new { controller = "ThrowException", action = "Index" });
        }
    }
}
