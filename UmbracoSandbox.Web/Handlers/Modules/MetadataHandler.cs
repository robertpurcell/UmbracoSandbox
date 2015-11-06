namespace UmbracoSandbox.Web.Handlers.Modules
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    using UmbracoSandbox.Web.Handlers.Base;
    using UmbracoSandbox.Web.Models.Modules;

    using Zone.UmbracoMapper;

    public class MetadataHandler : BaseHandler, IMetadataHandler
    {
        #region Constructor

        public MetadataHandler(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Method to get the metadata view model
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <returns>Metadata view model</returns>
        public MetadataViewModel GetMetadata(IPublishedContent currentPage)
        {
            var model = new MetadataViewModel
            {
                AbsoluteUrl = currentPage.UrlAbsolute()
            };
            Mapper.Map(currentPage, model);

            return model;
        }

        #endregion Methods
    }
}
