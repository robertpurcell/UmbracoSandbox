namespace UmbracoSandbox.Web.Models
{
    using Zone.GoogleMaps;

    public class HomePageViewModel : BasePageViewModel
    {
        public ImageModel HeroImage { get; set; }

        public GoogleMap GoogleMap { get; set; }
    }
}
