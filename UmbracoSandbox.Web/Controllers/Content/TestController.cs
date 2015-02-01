namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Models;

    public class TestController : Controller
    {
        public ActionResult Index()
        {
            var vm = new HomePageViewModel();
            vm.BodyText = new MvcHtmlString("<h1>Heading</h1>");

            return PartialView("Home", vm);
        }
    }
}
