namespace UmbracoSandbox.Web.Infrastructure.ActionFilters
{
    using System.Web;
    using System.Web.Mvc;

    using UmbracoSandbox.Web.Infrastructure.Config;

    /// <summary>
    /// Action filter that copies ModelState from Session. Required for forms used within rich text editors, where
    /// ModelState is lost when returning the form after validation errors
    /// </summary>
    public class ImportModelStateFromSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var modelState = HttpContext.Current.Session[SessionVariables.ModelState] as ModelStateDictionary;
            var tempData = HttpContext.Current.Session[SessionVariables.TempData] as TempDataDictionary;
            HttpContext.Current.Session.Remove(SessionVariables.ModelState);
            HttpContext.Current.Session.Remove(SessionVariables.TempData);
            if (modelState != null)
            {
                filterContext.Controller.ViewData.ModelState.Merge(modelState);
            }

            if (tempData != null)
            {
                foreach (var item in tempData)
                {
                    filterContext.Controller.TempData[item.Key] = item.Value;
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
