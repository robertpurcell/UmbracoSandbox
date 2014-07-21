namespace UmbracoSandbox.Service
{
    using System;
    using JustGiving.Api.Sdk;
    using JustGiving.Api.Sdk.Model.Page;

    public class JustGivingService
    {
        public PageRegistrationConfirmation CreatePage()
        {
            var clientconfig = new ClientConfiguration("https://api-sandbox.justgiving.com", "myapikey", 1);
            var client = new JustGivingClient(clientconfig);
            var pageReq = new JustGiving.Api.Sdk.Model.Page.RegisterPageRequest
            {
                CharityId = 1234,
                EventName = "My Event",
                CharityOptIn = false,
                IsCharityFunded = false,
                PageShortName = "my-event",
            };

            try
            {
                var result = client.Page.Create(pageReq);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
