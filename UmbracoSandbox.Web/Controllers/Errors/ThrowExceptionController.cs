namespace UmbracoSandbox.Web.Controllers.Pages
{
    using System;
    using System.Web.Mvc;

    public class ThrowExceptionController : Controller
    {
        public ActionResult Index()
        {
            throw new Exception();
        }
    }
}
