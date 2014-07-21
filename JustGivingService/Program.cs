namespace JustGivingService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JustGiving.Api.Sdk;
    using JustGiving.Api.Sdk.Model;
    using JustGiving.Api.Sdk.Model.Page;

    public class Program
    {
        private static void Main(string[] args)
        {
            var result = CreatePage();
        }

        private static PageRegistrationConfirmation CreatePage()
        {
            var clientConfig = new ClientConfiguration("https://api-sandbox.justgiving.com/", "fd9c70cb", 1);
            clientConfig.Username = "rpurcell@thisiszone.com";
            clientConfig.Password = "Zonepa55";
            var client = new JustGivingClient(clientConfig);
            var isValid = client.Account.AreCredentialsValid(clientConfig.Username, clientConfig.Password);
            var images = new List<ImageInfo>();
            images.Add(new ImageInfo
                {
                    Caption = "Prostate Cancer UK Logo",
                    IsDefault = true,
                    Url = "http://prostatecanceruk.org/images/logo.png"
                });
            var temp = new JustGiving.Api.Sdk.Model.Page.RegisterPageRequest();
            var tempNames = temp.GetType().GetProperties().Select(x => x.Name);

            var pageReq = new RegisterPageRequest
            {
                CharityId = 2050,
                EventName = "My Event",
                CharityOptIn = false,
                IsCharityFunded = false,
                PageShortName = "my-event2",
                PageTitle = "My Event",
                ActivityType = ActivityType.InMemory,
                PageStory = "Page Story Field",
                PageSummaryWhat = "Page Summary What Field",
                PageSummaryWhy = "Page Summary Why Field",
                Images = images,
                TargetAmount = 500
            };

            try
            {
                //var result = client.Page.Create(pageReq);
                //return result;
            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }
    }
}
