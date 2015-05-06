namespace UmbracoSandbox.Web.Models.Interfaces
{
    using UmbracoSandbox.Web.Models.Media;

    public interface IHeroTitle : ITitle
    {
        ImageModel HeroImage { get; }
    }
}
