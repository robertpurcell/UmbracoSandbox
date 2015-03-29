namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using UmbracoSandbox.Web.Models.Interfaces;
    using Zone.UmbracoMapper;

    public class MenuItemModel : BaseNodeViewModel, IMenuItem
    {
        public bool IsCurrentPage { get; set; }

        public bool IsCurrentPageOrAncestor { get; set; }

        public string Target { get; set; }

        public string CssClass { get; set; }

        public IEnumerable<IMenuItem> Items { get; set; }
    }
}
