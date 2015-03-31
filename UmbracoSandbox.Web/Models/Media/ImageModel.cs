namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;

    public class ImageModel : FileModel
    {
        public string AltText { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public IDictionary<string, string> Crops { get; set; }
    }
}
