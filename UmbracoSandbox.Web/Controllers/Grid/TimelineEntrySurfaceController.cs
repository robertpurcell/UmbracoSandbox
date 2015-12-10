namespace UmbracoSandbox.Web.Controllers.Grid
{
    using System.Web.Mvc;
    using Our.Umbraco.DocTypeGridEditor.Web.Controllers;

    public class TimelineEntrySurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult TimelineEntry()
        {
            return CurrentPartialView();
        }
    }
}
