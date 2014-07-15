namespace UmbracoSandbox.Web.Controllers.BackOffice
{
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using UmbracoSandbox.Web.Models.BackOffice;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Umbraco.Web.WebApi.Filters;

    [PluginController("Data")]
    public class DataApiController : UmbracoAuthorizedJsonController
    {
        public string[] Names = { "Bobby Charlton", "Sylvester Stallone", "Maggie Thatcher", "Trevor McDonald",
                                    "Cameron Diaz", "Samuel L Jackson", "Andy Peters", "Tom Jones", "Andrew Lloyd Webber",
                                    "William Shatner", "Phil Collins", "Kanye West", "Sting & the Police", "Boris Becker",
                                    "Tina Turner", "John Fashanu", "Madonna", "Chris Eubank"
                                };

        public IEnumerable<OnlineDonation> GetAll()
        {
            var allData = new List<OnlineDonation>();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var donation = new OnlineDonation
                {
                    Id = i,
                    Name = Names[i % Names.Count()],
                    Date = DateTime.Now.AddSeconds(-r.Next(0, 31536000)),
                    Amount = "£" + r.Next(0, 500)
                };

                allData.Add(donation);
            }

            return allData.OrderByDescending(x => x.Date);
        }
    }
}