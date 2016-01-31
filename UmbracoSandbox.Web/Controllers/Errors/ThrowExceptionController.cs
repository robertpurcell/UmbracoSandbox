namespace UmbracoSandbox.Web.Controllers.Errors
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
