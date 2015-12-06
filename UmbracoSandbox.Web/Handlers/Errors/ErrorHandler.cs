namespace UmbracoSandbox.Web.Handlers.Errors
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Handlers.Base;
    using UmbracoSandbox.Web.Handlers.Modules;
    using UmbracoSandbox.Web.Models.Errors;
    using Zone.UmbracoMapper;

    public class ErrorHandler : BaseHandler, IErrorHandler
    {
        #region Fields

        private readonly IMetadataHandler _metadataHandler;

        #endregion Fields

        #region Constructor

        public ErrorHandler(IUmbracoMapper mapper, IMetadataHandler metadataHandler)
            : base(mapper)
        {
            _metadataHandler = metadataHandler;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the model for the page
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <returns>The page model</returns>
        public virtual ServerErrorViewModel GetError500Model(IPublishedContent currentPage)
        {
            var model = new ServerErrorViewModel
            {
                Metadata = _metadataHandler.GetMetadata(currentPage)
            };
            Mapper.Map(currentPage, model);

            return model;
        }

        #endregion Methods
    }
}
