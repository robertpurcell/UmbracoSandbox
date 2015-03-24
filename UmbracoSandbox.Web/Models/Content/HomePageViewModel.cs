namespace UmbracoSandbox.Web.Models
{
    using System.Web;
    using Zone.GoogleMaps;

    public class HomePageViewModel : BasePageViewModel
    {
        public string Title { get; set; }

        public IHtmlString BodyText { get; set; }

        public ImageModel HeroImage { get; set; }

        public GoogleMap GoogleMap { get; set; }

        public string ImageUrl { get; set; }
    }
}
