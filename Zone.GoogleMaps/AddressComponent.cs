namespace Zone.GoogleMaps
{
    using Newtonsoft.Json;

    public class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        public string[] Types { get; set; }
    }
}
