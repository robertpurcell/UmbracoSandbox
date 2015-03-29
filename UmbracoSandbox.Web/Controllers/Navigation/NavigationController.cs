namespace UmbracoSandbox.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using RJP.MultiUrlPicker.Models;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Zone.UmbracoMapper;

    public class NavigationController : BaseController
    {
        #region Constructor

        public NavigationController(IUmbracoMapper mapper) : base(mapper)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Render the main navigation
        /// </summary>
        /// <returns>Partial view result</returns>
        [ChildActionOnly]
        public PartialViewResult MainNavigation()
        {
            var items = GetMenuItems(Root, 0, 3).ToList();
            var root = MapItem(Root);
            items.Insert(0, root);
            var vm = new NavigationModel
            {
                Items = items
            };

            return PartialView("_MainNavigation", vm);
        }

        /// <summary>
        /// Render the footer navigation
        /// </summary>
        /// <returns>Partial view result</returns>
        [ChildActionOnly]
        public PartialViewResult FooterNavigation()
        {
            var vm = GetNavigationModel(Root, "footerNavigation");

            return PartialView("_FooterNavigation", vm);
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
                .Where(x => !x.GetPropertyValue<bool>("umbracoNaviHide") && (x.TemplateId != 0))
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
        private NavigationModel GetNavigationModel(IPublishedContent page, string alias)
        {
            var items = new List<MenuItemModel>();
            var links = page.GetPropertyValue<MultiUrls>(alias);
            if (links.Any())
            {
                foreach (var link in links)
                {
                    items.Add(new MenuItemModel
                    {
                        Name = link.Name,
                        Url = link.Url,
                        Target = link.Target
                    });
                }
            }

            return new NavigationModel
            {
                Items = items
            };
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
                IsCurrentPage = CurrentPage.Id.Equals(page.Id),
                IsCurrentPageOrAncestor = CurrentPage.Id.Equals(page.Id)
                    ? true
                    : CurrentPage.Path.Split(',').Where(i => !i.Equals(Root.Id.ToString())).Contains(page.Id.ToString())
            };
            Mapper.Map(page, item);

            return item;
        }

        #endregion
    }
}
