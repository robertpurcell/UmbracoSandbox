namespace UmbracoSandbox.Web.Models.Interfaces
{
    using UmbracoSandbox.Web.Models.Media;

    public interface IHeroTitle : ITitle
    {
        ImageViewModel HeroImage { get; }
    }
}
