namespace UmbracoSandbox.Forms.Models
{
    using System.Collections.Generic;

    public class MappingDto
    {
        public string PrevalueSourceId { get; set; }

        public List<Mapping> Mappings { get; set; }

        public class Mapping
        {
            public string Id { get; set; }

            public string Alias { get; set; }
        }
    }
}
