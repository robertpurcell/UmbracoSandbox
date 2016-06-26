namespace UmbracoSandbox.Web.Models.Navigation
{
    using System.Collections.Generic;

    using UmbracoSandbox.Common.Helpers;
    using UmbracoSandbox.Web.Models.Interfaces;

    public class NavigationViewModel
    {
        public IEnumerable<IMenuItem> Items { get; set; }

        public bool ShowNavigation => Items.IsAndAny();
    }
}