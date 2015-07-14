namespace UmbracoSandbox.Web.Models.Navigation
{
    using UmbracoSandbox.Web.Models.Interfaces;

    public class MainNavigationModel : NavigationModel, IMainNavigation
    {
        public IMenuItem Login { get; set; }

        public bool IsLoggedIn { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
