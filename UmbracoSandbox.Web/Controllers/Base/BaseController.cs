namespace UmbracoSandbox.Web.Controllers
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using log4net;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using UmbracoSandbox.Web.Models;
    using UmbracoSandbox.Web.Infrastructure.Mapping;
    using Zone.GoogleMaps;
    using Zone.UmbracoMapper;
    using System.Collections.Generic;

    public abstract class BaseController : SurfaceController, IRenderMvcController
    {
        #region Fields

        private static ILog _log;
        private IPublishedContent _root;

        #endregion

        #region Constructor

        protected BaseController(IUmbracoMapper mapper)
        {
            Mapper = mapper;
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            AddCustomMappings();
        }

        #endregion

        #region Properties

        protected IUmbracoMapper Mapper { get; private set; }

        protected IPublishedContent Root
        {
            get
            {
                return _root ?? Umbraco.TypedContentAtRoot().FirstOrDefault();
            }

            set
            {
                _root = value;
            }
        }

        public static ILog Log
        {
            get
            {
                return _log;
            }

            set
            {
                _log = value;
            }
        }

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

        #region Mapping helpers

        /// <summary>
        /// Sets up the custom mappings for the Umbraco Mapper used in the project
        /// </summary>
        private void AddCustomMappings()
        {
            Mapper.AddCustomMapping(typeof(FileModel).FullName, MediaMapper.GetFile)
                .AddCustomMapping(typeof(ImageModel).FullName, MediaMapper.GetImage)
                .AddCustomMapping(typeof(GoogleMap).FullName, GoogleMapMapper.GetGoogleMap)
                .AddCustomMapping(typeof(IEnumerable<ModuleModel>).FullName, ArchetypeMapper.GetCollection<ModuleModel>);
        }

        /// <summary>
        /// Gets the model for the page
        /// </summary>
        /// <typeparam name="T">The page type</typeparam>
        /// <returns>The page model</returns>
        protected virtual T GetPageModel<T>() where T : BasePageViewModel, new()
        {
            var model = new T
            {
                CanonicalUrl = Request.Url == null || Request.Url.AbsolutePath == CurrentPage.Url ? null : CurrentPage.UrlAbsolute(),
                AbsoluteUrl = CurrentPage.UrlAbsolute()
            };

            Mapper.Map(CurrentPage, model);

            return model;
        }

        /// <summary>
        /// Gets the model of the given type
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <returns>The model</returns>
        protected virtual T GetModel<T>() where T : BaseNodeViewModel, new()
        {
            var model = new T();
            Mapper.Map(CurrentPage, model);

            return model;
        }

        #endregion
    }
}
