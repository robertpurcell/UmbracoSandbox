namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using System.Linq;

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
            var publishedContents = contentList as IPublishedContent[] ?? contentList.ToArray();
            if (!publishedContents.IsAndAny())
            {
                return null;
            }

            var model = new List<T>();
            mapper.MapCollection(publishedContents, model);

            return model;
        }
    }
}
