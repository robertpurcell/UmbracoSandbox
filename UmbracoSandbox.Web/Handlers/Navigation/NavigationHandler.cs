namespace UmbracoSandbox.Web.Handlers.Navigation
{
    using System.Collections.Generic;
    using System.Linq;
    using RJP.MultiUrlPicker.Models;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Handlers.Base;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Infrastructure.Config;
    using UmbracoSandbox.Web.Models.Navigation;
    using Zone.UmbracoMapper;

    public class NavigationHandler : BaseHandler, INavigationHandler
    {
        #region Fields

        private IPublishedContent _root;

        #endregion

        #region Constructor

        public NavigationHandler(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get the model for the main navigation
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="currentMember">The current member</param>
        /// <returns>Navigation model</returns>
        public MainNavigationModel GetMainNavigation(IPublishedContent currentPage, IPublishedContent currentMember)
        {
            _root = currentPage.AncestorOrSelf(1);
            var login = _root.Descendant(PageTypes.Login);

            return new MainNavigationModel
            {
                Items = GetMenuItems(currentPage, _root, 0, 3),
                Login = MapItem(currentPage, login),
                IsLoggedIn = currentMember != null,
                Name = currentMember != null ? currentMember.Name : string.Empty
            };
        }

        /// <summary>
        /// Method to get the model for the footer navigation
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <returns>Navigation model</returns>
        public NavigationModel GetFooterNavigation(IPublishedContent currentPage)
        {
            return new NavigationModel
            {
                Items = GetMenuItems(currentPage, "footerNavigation")
            };
        }

        #endregion

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
                .Where(x => !x.GetPropertyValue<bool>("umbracoNaviHide") && x.TemplateId != 0)
                .Select(x =>
                    {
                        var item = MapItem(currentPage, x);
                        if (currentLevel < maxLevel && !x.GetPropertyValue<bool>("hideSubNavigation"))
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
                    || currentPage.Path.Split(',').Where(i => !i.Equals(_root.Id.ToString())).Contains(page.Id.ToString())
            };
            Mapper.Map(page, item);

            return item;
        }

        #endregion
    }
}
