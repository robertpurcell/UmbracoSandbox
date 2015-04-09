namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class ModuleMapper
    {
        public static IEnumerable<T> GetCollection<T>(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
            where T : BaseModuleModel, new()
        {
            var content = contentToMapFrom.GetPropertyValue<IEnumerable<IPublishedContent>>(propertyAlias, recursive);
            if (content.IsAndAny())
            {
                var model = new List<T>();
                mapper.MapCollection((IEnumerable<IPublishedContent>)content, model);

                return model;
            }

            return null;
        }
    }
}
