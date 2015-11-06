namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Handlers.Modules;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Models.Content;

    using Zone.UmbracoMapper;

    public class ContactPageHandler : PageHandler, IContactPageHandler
    {
        #region Constructor

        public ContactPageHandler(IUmbracoMapper mapper, IMetadataHandler metadataHandler, INavigationHandler navigationHandler)
            : base(mapper, metadataHandler, navigationHandler)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the model for the contact page
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="currentMember">The current member</param>
        /// <returns>The page model</returns>
        public ContactViewModel GetContactPageModel(IPublishedContent currentPage, IMember currentMember)
        {
            var model = GetPageModel<ContactViewModel>(currentPage, currentMember);
            Mapper.Map(currentPage, model.Form);

            return model;
        }

        #endregion Methods
    }
}
