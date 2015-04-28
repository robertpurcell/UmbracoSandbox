namespace UmbracoSandbox.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using log4net;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using UmbracoSandbox.Service.EmailService;
    using UmbracoSandbox.Web.Infrastructure.Mapping;
    using UmbracoSandbox.Web.Models;
    using UmbracoSandbox.Web.Models.Modules;
    using Zone.GoogleMaps;
    using Zone.UmbracoMapper;

    public abstract class BaseController : SurfaceController, IRenderMvcController
    {
        #region Fields

        private static ILog _log;
        private IPublishedContent _root;

        #endregion

        #region Constructor

        protected BaseController(IUmbracoMapper mapper, IEmailService mailer)
        {
            Mapper = mapper;
            Mailer = mailer;
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            AddCustomMappings();
        }

        #endregion

        #region Properties

        protected IUmbracoMapper Mapper { get; private set; }

        protected IEmailService Mailer { get; private set; }

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

        /// <summary>
        /// Applies the Home action
        /// </summary>
        /// <returns>Redirect to Home page</returns>
        public virtual ActionResult Home()
        {
            var url = Root == null ? "/" : Root.Url;

            return Redirect(url);
        }

        #endregion

        #region Mapping helpers

        /// <summary>
        /// Sets up the custom mappings for the Umbraco Mapper used in the project
        /// </summary>
        private void AddCustomMappings()
        {
            Mapper
                .AddCustomMapping(typeof(FileModel).FullName, MediaMapper.GetFile)
                .AddCustomMapping(typeof(ImageModel).FullName, MediaMapper.GetImage)
                .AddCustomMapping(typeof(GoogleMap).FullName, GoogleMapMapper.GetGoogleMap)
                .AddCustomMapping(typeof(IEnumerable<ModuleModel>).FullName, ArchetypeMapper.GetCollection<ModuleModel>)
                .AddCustomMapping(typeof(IEnumerable<BlogPostModuleModel>).FullName, ModuleMapper.GetCollection<BlogPostModuleModel>);
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
                CanonicalUrl = Request.Url == null || string.Equals(Request.Url.AbsolutePath, CurrentPage.Url) ? null : CurrentPage.UrlAbsolute(),
                AbsoluteUrl = CurrentPage.UrlAbsolute()
            };

            Mapper.Map(CurrentPage, model);

            return model;
        }

        #endregion
    }
}
