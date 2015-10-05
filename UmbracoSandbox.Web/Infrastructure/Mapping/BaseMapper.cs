namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using Umbraco.Core.Models;

    using Zone.UmbracoMapper;

    public abstract class BaseMapper
    {
        /// <summary>
        /// Gets the model of the given type
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="contentToMapFrom">Content to map from</param>
        /// <returns>The model</returns>
        protected static T GetModel<T>(IUmbracoMapper mapper, IPublishedContent contentToMapFrom) where T : BaseNodeViewModel, new()
        {
            if (contentToMapFrom == null)
            {
                return null;
            }

            var model = new T();
            mapper.Map(contentToMapFrom, model);

            return model;
        }
    }
}
