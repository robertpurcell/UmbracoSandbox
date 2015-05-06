namespace UmbracoSandbox.Web.Models.Media
{
    using System.Collections.Generic;

    public class ImageCropperModel
    {
        public FocalPointCoordinates FocalPoint { get; set; }

        public string Src { get; set; }

        public IEnumerable<Crop> Crops { get; set; }

        public class FocalPointCoordinates
        {
            public double Left { get; set; }

            public double Top { get; set; }
        }

        public class Coordinates
        {
            public double X1 { get; set; }

            public double Y1 { get; set; }

            public double X2 { get; set; }

            public double Y2 { get; set; }
        }

        public class Crop
        {
            public string Alias { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            public Coordinates Coordinates { get; set; }
        }
    }
}
