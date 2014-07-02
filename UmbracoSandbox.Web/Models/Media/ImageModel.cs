namespace UmbracoSandbox.Web.Models
{
    public class ImageModel : MediaFileModel
    {
        public string AltText { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
