﻿namespace UmbracoSandbox.Web.Controllers
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Mvc;
    using log4net;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using UmbracoSandbox.Web.Handlers;
    using UmbracoSandbox.Web.Infrastructure.Mapping;
    using UmbracoSandbox.Web.Models;
    using UmbracoSandbox.Web.Models.Modules;
    using Zone.GoogleMaps;
    using Zone.UmbracoMapper;

    public abstract class BaseController : SurfaceController, IRenderMvcController
    {
        #region Constructor

        protected BaseController(IUmbracoMapper mapper, IPageHandler handler)
        {
            Mapper = mapper;
            Handler = handler;
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            AddCustomMappings();
        }

        #endregion

        #region Properties

        protected IUmbracoMapper Mapper { get; private set; }

        protected IPageHandler Handler { get; private set; }

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

        #endregion
    }
}
