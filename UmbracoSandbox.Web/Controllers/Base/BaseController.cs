namespace UmbracoSandbox.Web.Controllers
{
    using System.Reflection;
    using System.Web.Mvc;
    using log4net;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    public abstract class BaseController : SurfaceController, IRenderMvcController
    {
        #region Constructor

        protected BaseController()
        {
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Properties

        public static ILog Log { get; private set; }

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
