namespace UmbracoSandbox.Web.Handlers.Navigation
{
    using System.Collections.Generic;
    using System.Linq;

    using GravatarHelper;

    using RJP.MultiUrlPicker.Models;

    using Umbraco.Core.Models;
    using Umbraco.Web;

    using UmbracoSandbox.Utilities;
    using UmbracoSandbox.Web.Handlers.Base;
    using UmbracoSandbox.Web.Infrastructure.Config;
    using UmbracoSandbox.Web.Infrastructure.ContentLocators;
    using UmbracoSandbox.Web.Models.Navigation;

    using Zone.UmbracoMapper;

    public class NavigationHandler : BaseHandler, INavigationHandler
    {
        #region Fields

        private readonly IRootContentLocator _rootContentLocator;
        private IPublishedContent _root;

        #endregion Fields

        #region Constructor

        public NavigationHandler(IUmbracoMapper mapper, IRootContentLocator rootContentLocator)
            : base(mapper)
        {
            _rootContentLocator = rootContentLocator;
        }

        #endregion Constructor

        #region Properties

        protected IPublishedContent Root
        {
            get
            {
                return _root ?? _rootContentLocator.Find();
            }

            set
            {
                _root = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Method to get the model for the main navigation
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="currentMember">The current member</param>
        /// <returns>Navigation model</returns>
        public MainNavigationViewModel GetMainNavigation(IPublishedContent currentPage, IMember currentMember)
        {
            var login = Root.Descendant(ContentTypeAliases.Login);
            var model = new MainNavigationViewModel
            {
                Items = GetMenuItems(currentPage, Root, 0, 3),
                Login = login != null ? MapItem(currentPage, login) : null
            };

            if (currentMember == null)
            {
                return model;
            }

            model.IsLoggedIn = true;
            model.Name = currentMember.Name;
            model.ImageUrl = GravatarHelper.CreateGravatarUrl(currentMember.Email, 30, string.Empty, null, null, null);
            return model;
        }

        /// <summary>
        /// Method to get the model for the footer navigation
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <returns>Navigation model</returns>
        public NavigationViewModel GetFooterNavigation(IPublishedContent currentPage)
        {
            return new NavigationViewModel
            {
                Items = GetMenuItems(currentPage, PropertyAliases.FooterNavigation)
            };
        }

        #endregion Methods

        #region Helpers

        /// <summary>
        /// Get the menu items for a link picker property
        /// </summary>
        /// <param name="page">Page containing the property</param>
        /// <param name="alias">Property alias</param>
        /// <returns>Navigation model</returns>
        private static IEnumerable<MenuItemModel> GetMenuItems(IPublishedContent page, string alias)
        {
            var links = page.GetPropertyValue<MultiUrls>(alias, true);
            if (!links.IsAndAny())
            {
                yield break;
            }

            foreach (var link in links)
            {
                yield return new MenuItemModel
                {
                    Name = link.Name,
                    Url = link.Url,
                    Target = link.Target
                };
            }
        }

        /// <summary>
        /// Get the menu items by parent node
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="parent">Parent node</param>
        /// <param name="currentLevel">Current level</param>
        /// <param name="maxLevel">Max level</param>
        /// <returns>List of menu items</returns>
        private IEnumerable<MenuItemModel> GetMenuItems(IPublishedContent currentPage, IPublishedContent parent, int currentLevel, int maxLevel)
        {
            return parent.Children
                .Where(x => !x.GetPropertyValue<bool>(PropertyAliases.UmbracoNaviHide) && x.TemplateId != 0)
                .Select(x =>
                    {
                        var item = MapItem(currentPage, x);
                        if (currentLevel < maxLevel && !x.GetPropertyValue<bool>(PropertyAliases.HideSubNavigation))
                        {
                            item.Items = GetMenuItems(currentPage, x, currentLevel + 1, maxLevel);
                        }

                        return item;
                    });
        }

        /// <summary>
        /// Map navigation item
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="page">Page to map from</param>
        /// <returns>Menu item</returns>
        private MenuItemModel MapItem(IPublishedContent currentPage, IPublishedContent page)
        {
            var item = new MenuItemModel
            {
                IsCurrentPage = currentPage.Id.Equals(page.Id),
                IsCurrentPageOrAncestor = currentPage.Id.Equals(page.Id)
                    || currentPage.Path.Split(',').Where(i => !i.Equals(Root.Id.ToString())).Contains(page.Id.ToString())
            };
            Mapper.Map(page, item);

            return item;
        }

        #endregion Helpers
    }
}
