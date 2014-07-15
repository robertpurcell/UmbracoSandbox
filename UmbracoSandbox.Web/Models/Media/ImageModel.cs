namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;

    public class ImageModel : MediaFileModel
    {
        public string AltText { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Dictionary<string, string> CropUrls { get; set; }
    }
}
