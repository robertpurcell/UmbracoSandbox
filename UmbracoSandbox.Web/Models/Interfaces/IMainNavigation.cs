namespace UmbracoSandbox.Web.Models.Interfaces
{
    public interface IMainNavigation : INavigation
    {
        IMenuItem Login { get; }

        bool IsLoggedIn { get; }
    }
}
