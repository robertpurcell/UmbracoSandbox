namespace UmbracoSandbox.Web.Handlers
{
    using System.Collections.Generic;
    using System.Reflection;
    using log4net;
    using UmbracoSandbox.Web.Infrastructure.Mapping;
    using UmbracoSandbox.Web.Models;
    using UmbracoSandbox.Web.Models.Modules;
    using Zone.GoogleMaps;
    using Zone.UmbracoMapper;

    public abstract class BaseHandler
    {
        #region Constructor

        public BaseHandler(IUmbracoMapper mapper)
        {
            Mapper = mapper;
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            AddCustomMappings();
        }

        #endregion

        #region Properties

        protected IUmbracoMapper Mapper { get; private set; }

        public static ILog Log { get; private set; }

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
