namespace UmbracoSandbox.Web.Models.Interfaces
{
    using System.Collections.Generic;

    public interface INavigation
    {
        IEnumerable<IMenuItem> Items { get; }
    }
}
