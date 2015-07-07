namespace UmbracoSandbox.Web.Controllers.Base
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using UmbracoSandbox.Service.Logging;

    public abstract class BaseController : SurfaceController, IRenderMvcController
    {
        #region Constructor

        protected BaseController(ILoggingService logger)
        {
            Logger = logger;
            IsLoggedIn = Members.IsLoggedIn();
        }

        #endregion

        #region Properties

        protected ILoggingService Logger { get; private set; }

        protected bool IsLoggedIn { get; private set; }

        #endregion

        #region Render MVC

        /// <summary>
        /// Locates the template for the given route
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="model">Instance of model</param>
        /// <returns>Template for given route</returns>
        protected ActionResult CurrentTemplate<T>(T model)
        {
            return View(ControllerContext.RouteData.Values["action"].ToString(), model);
        }

        /// <summary>
        /// Applies the default index action
        /// </summary>
        /// <param name="model">Instance of model</param>
        /// <returns>Default ActionResult</returns>
        public virtual ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }

        #endregion
    }
}
