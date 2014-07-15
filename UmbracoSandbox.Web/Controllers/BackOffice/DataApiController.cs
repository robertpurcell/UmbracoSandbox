namespace UmbracoSandbox.Web.Controllers.BackOffice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using UmbracoSandbox.Web.Models.BackOffice;

    [PluginController("Data")]
    public class DataApiController : UmbracoAuthorizedJsonController
    {
        #region Fields

        private string[] names =
        {
            "Bobby Charlton", "Sylvester Stallone", "Maggie Thatcher", "Trevor McDonald",
            "Cameron Diaz", "Samuel L Jackson", "Andy Peters", "Tom Jones", "Andrew Lloyd Webber",
            "William Shatner", "Phil Collins", "Kanye West", "Sting & the Police", "Boris Becker",
            "Tina Turner", "John Fashanu", "Madonna", "Chris Eubank"
        };

        #endregion

        #region Methods

        public IEnumerable<OnlineDonation> GetAll()
        {
            var allData = new List<OnlineDonation>();
            var r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var donation = new OnlineDonation
                {
                    Id = i,
                    Name = names[i % names.Count()],
                    Date = DateTime.Now.AddSeconds(-r.Next(0, 31536000)),
                    Amount = r.Next(0, 500)
                };
                allData.Add(donation);
            }

            return allData.OrderByDescending(x => x.Date);
        }

        #endregion
    }
}