namespace UmbracoSandbox.Web.Models
{
    using UmbracoSandbox.Web.Models.Interfaces;

    public class MainNavigationModel : NavigationModel, IMainNavigation
    {
        public IMenuItem Home { get; set; }
    }
}
