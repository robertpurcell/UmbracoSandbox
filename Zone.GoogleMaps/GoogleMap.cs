namespace Zone.GoogleMaps
{
    using System.Collections.Generic;

    public class GoogleMap
    {
        public decimal Lat { get; set; }

        public decimal Lng { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public IEnumerable<AddressComponent> AddressComponents { get; set; }

        public int Zoom { get; set; }
    }
}
