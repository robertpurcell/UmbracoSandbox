namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Linq;
    using System.Web.Mvc;

    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Templates;

    using UmbracoSandbox.Common.Constants;

    using Zone.Grid;
    using Zone.UmbracoMapper;

    public class GridMapper
    {
        public static object GetGrid(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var grid = contentToMapFrom.GetPropertyValue<Grid>(propertyAlias, recursive);
            foreach (var control in grid.Sections.SelectMany(s => s.Rows.SelectMany(r => r.Areas.SelectMany(a => a.Controls))).Where(control => control.Value != null))
            {
                control.TypedValue = GetTypedVaue(control.Editor.Alias, control.Value);
            }

            return grid;
        }

        private static object GetTypedVaue(string alias, object value)
        {
            switch (alias)
            {
                case GridAliases.Headline:
                case GridAliases.Quote:
                    return value.ToString();
                case GridAliases.Rte:
                    return new MvcHtmlString(TemplateUtilities.ParseInternalLinks(value.ToString()));
                default:
                    return string.Empty;
            }
        }
    }
}
