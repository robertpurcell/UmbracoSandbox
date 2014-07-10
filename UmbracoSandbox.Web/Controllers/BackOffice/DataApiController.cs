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
        public IEnumerable<OnlineDonation> GetAll()
        {
            var allData = new List<OnlineDonation>();

            var donation = new OnlineDonation
            {
                Id = 1,
                Name = "Rob Purcell",
                Date = DateTime.Now,
                Amount = "£100"
            };

            var donation2 = new OnlineDonation
            {
                Id = 16,
                Name = "Maggie Thatcher",
                Date = DateTime.Now,
                Amount = "£6"
            };

            for (var i = 0; i < 40; i++)
            {
                donation.Id = i + 1;
                allData.Add(donation);
            }

            for (var i = 0; i < 60; i++)
            {
                donation2.Id = i + 16;
                allData.Add(donation2);
            }

            return allData;
        }
    }
}