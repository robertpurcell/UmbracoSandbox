namespace UmbracoSandbox.Service.JustGiving
{
    using System.Collections.Generic;

    public interface IJustGivingService
    {
        bool ValidateCredentials(string email, string password);

        string GetPageName();

        bool IsPageNameRegistered(string pageName);

        string CreatePage(PageRequestDto requestDto);

        EventDetailsDto GetEventDetails(int id);

        IList<EventDetailsDto> GetEvents();
    }
}
