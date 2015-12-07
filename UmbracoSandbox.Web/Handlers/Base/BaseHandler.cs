﻿namespace UmbracoSandbox.Web.Handlers.Base
{
    using System.Collections.Generic;

    using RJP.MultiUrlPicker.Models;

    using Umbraco.Core.Models.Membership;
    using UmbracoSandbox.Web.Infrastructure.Mapping;
    using UmbracoSandbox.Web.Models.Media;
    using UmbracoSandbox.Web.Models.Modules;

    using Zone.GoogleMaps;
    using Zone.Grid;
    using Zone.UmbracoMapper;

    public abstract class BaseHandler
    {
        #region Constructor

        protected BaseHandler(IUmbracoMapper mapper)
        {
            Mapper = mapper;
            AddCustomMappings();
        }

        #endregion Constructor

        #region Properties

        protected IUmbracoMapper Mapper { get; private set; }

        #endregion Properties

        #region Mapping helpers

        /// <summary>
        /// Sets up the custom mappings for the Umbraco Mapper used in the project
        /// </summary>
        private void AddCustomMappings()
        {
            Mapper
                .AddCustomMapping(typeof(Link).FullName, LinkMapper.GetLink)
                .AddCustomMapping(typeof(Link).FullName, LinkMapper.GetLinkFromValue)
                .AddCustomMapping(typeof(FileViewModel).FullName, MediaMapper.GetFile)
                .AddCustomMapping(typeof(ImageViewModel).FullName, MediaMapper.GetImage)
                .AddCustomMapping(typeof(ImageViewModel).FullName, MediaMapper.GetImageFromValue)
                .AddCustomMapping(typeof(Grid).FullName, GridMapper.GetGrid)
                .AddCustomMapping(typeof(GoogleMap).FullName, GoogleMapMapper.GetGoogleMap)
                .AddCustomMapping(typeof(IUser).FullName, UserMapper.GetUser)
                .AddCustomMapping(typeof(IEnumerable<Link>).FullName, LinkMapper.GetLinks)
                .AddCustomMapping(typeof(IEnumerable<Link>).FullName, LinkMapper.GetLinksFromValue)
                .AddCustomMapping(typeof(IEnumerable<ModuleViewModel>).FullName, ArchetypeMapper.GetCollection<ModuleViewModel>)
                .AddCustomMapping(typeof(IEnumerable<BlogPostModuleViewModel>).FullName, ModuleMapper.GetCollection<BlogPostModuleViewModel>);
        }

        #endregion Mapping helpers
    }
}
