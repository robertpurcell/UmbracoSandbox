namespace UmbracoSandbox.Web.Models.Interfaces
{
    using System.Collections.Generic;

    public interface IMenuItem
    {
        int Id { get; }

        string Url { get; }

        string Name { get; }

        bool IsCurrentPage { get; }

        bool IsCurrentPageOrAncestor { get; }

        string Target { get; }

        int Level { get; }

        string CssClass { get; }

        IEnumerable<IMenuItem> Items { get; }
    }
}
