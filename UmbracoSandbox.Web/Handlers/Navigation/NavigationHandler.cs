namespace UmbracoSandbox.Web.Handlers.Navigation
{
    using System.Collections.Generic;
    using System.Linq;
    using RJP.MultiUrlPicker.Models;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Handlers.Base;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Models.Navigation;
    using Zone.UmbracoMapper;

    public class NavigationHandler : BaseHandler, INavigationHandler
    {
        #region Fields

        private IPublishedContent _currentPage;
        private IPublishedContent _root;

        #endregion

        #region Constructor

        public NavigationHandler(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action methods

        public NavigationModel GetMainNavigation(IPublishedContent currentPage)
        {
            _currentPage = currentPage;
            _root = currentPage.AncestorOrSelf(1);

            return new NavigationModel
            {
                Items = GetMenuItems(_root, 0, 3)
            };
        }

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
        /// Get the menu items by parent node
        /// </summary>
        /// <param name="parent">Parent node</param>
        /// <param name="currentLevel">Current level</param>
        /// <param name="maxLevel">Max level</param>
        /// <returns>List of menu items</returns>
        private IEnumerable<MenuItemModel> GetMenuItems(IPublishedContent parent, int currentLevel, int maxLevel)
        {
            return parent.Children
                .Where(x => !x.GetPropertyValue<bool>("umbracoNaviHide") && x.TemplateId != 0)
                .Select(x =>
                    {
                        var item = MapItem(x);
                        if (currentLevel < maxLevel && !x.GetPropertyValue<bool>("hideSubNavigation"))
                        {
                            item.Items = GetMenuItems(x, currentLevel + 1, maxLevel);
                        }

                        return item;
                    });
        }

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
        /// Map navigation item
        /// </summary>
        /// <param name="page">Page to map from</param>
        /// <returns>Menu item</returns>
        private MenuItemModel MapItem(IPublishedContent page)
        {
            var item = new MenuItemModel
            {
                IsCurrentPage = _currentPage.Id.Equals(page.Id),
                IsCurrentPageOrAncestor = _currentPage.Id.Equals(page.Id)
                    || _currentPage.Path.Split(',').Where(i => !i.Equals(_root.Id.ToString())).Contains(page.Id.ToString())
            };
            Mapper.Map(page, item);

            return item;
        }

        #endregion
    }
}
