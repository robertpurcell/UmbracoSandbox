namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Collections.Generic;

    using RJP.MultiUrlPicker.Models;

    using Umbraco.Core.Models.Membership;
    using UmbracoSandbox.Web.Models.Media;
    using UmbracoSandbox.Web.Models.Modules;

    using Zone.GoogleMaps;
    using Zone.Grid;
    using Zone.UmbracoMapper;

    public class CustomMapper : UmbracoMapper
    {
        #region Constructor

        public CustomMapper()
        {
            AddCustomMappings();
        }

        #endregion Constructor

        #region Helpers

        /// <summary>
        /// Sets up the custom mappings for the Umbraco Mapper used in the project
        /// </summary>
        private void AddCustomMappings()
        {
            AddCustomMapping(typeof(Link).FullName, LinkMapper.GetLink)
                .AddCustomMapping(typeof(Link).FullName, LinkMapper.GetLinkFromValue)
                .AddCustomMapping(typeof(FileViewModel).FullName, MediaMapper.GetFile)
                .AddCustomMapping(typeof(ImageViewModel).FullName, MediaMapper.GetImage)
                .AddCustomMapping(typeof(ImageViewModel).FullName, MediaMapper.GetImageFromValue)
                .AddCustomMapping(typeof(Grid).FullName, GridMapper.GetGrid)
                .AddCustomMapping(typeof(GoogleMap).FullName, GoogleMapMapper.GetGoogleMap)
                .AddCustomMapping(typeof(IUser).FullName, UserMapper.GetUser)
                .AddCustomMapping(typeof(IEnumerable<Link>).FullName, LinkMapper.GetLinks)
                .AddCustomMapping(typeof(IEnumerable<Link>).FullName, LinkMapper.GetLinksFromValue)
                .AddCustomMapping(typeof(IEnumerable<TimelineEntryViewModel>).FullName, ModuleMapper.GetCollection<TimelineEntryViewModel>)
                .AddCustomMapping(typeof(IEnumerable<BlogPostModuleViewModel>).FullName, ModuleMapper.GetCollection<BlogPostModuleViewModel>);
        }

        #endregion Helpers
    }
}
