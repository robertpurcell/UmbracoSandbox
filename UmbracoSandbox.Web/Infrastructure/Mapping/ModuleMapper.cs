namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Models.Base;
    using Zone.UmbracoMapper;

    public class ModuleMapper
    {
        public static IEnumerable<T> GetCollection<T>(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
            where T : BaseModuleModel, new()
        {
            var contentList = contentToMapFrom.GetPropertyValue<IEnumerable<IPublishedContent>>(propertyAlias, recursive);
            if (contentList.IsAndAny())
            {
                var model = new List<T>();
                mapper.MapCollection(contentList, model);

                return model;
            }

            return null;
        }
    }
}
