namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using UmbracoSandbox.Web.Models.Interfaces;

    public class NavigationModel : INavigation
    {
        public IEnumerable<IMenuItem> Items { get; set; }
    }
}
