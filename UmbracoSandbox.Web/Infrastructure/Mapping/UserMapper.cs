namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    using Zone.UmbracoMapper;

    public class UserMapper
    {
        public static object GetUser(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var userId = contentToMapFrom.GetPropertyValue<int>(propertyAlias, recursive);
            var userService = ApplicationContext.Current.Services.UserService;
            return userService.GetUserById(userId);
        }
    }
}
