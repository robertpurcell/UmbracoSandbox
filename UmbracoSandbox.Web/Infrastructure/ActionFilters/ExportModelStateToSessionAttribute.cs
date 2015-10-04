namespace UmbracoSandbox.Web.Infrastructure.ActionFilters
{
    using System.Web;
    using System.Web.Mvc;

    using UmbracoSandbox.Web.Infrastructure.Config;

    /// <summary>
    /// Action filter that copies ModelState to Session. Required for forms used within rich text editors, where
    /// ModelState is lost when returning the form after validation errors
    /// </summary>
    public class ExportModelStateToSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                HttpContext.Current.Session[SessionVariables.ModelState] = filterContext.Controller.ViewData.ModelState;
            }

            HttpContext.Current.Session[SessionVariables.TempData] = filterContext.Controller.TempData;
            base.OnActionExecuted(filterContext);
        }
    }
}
