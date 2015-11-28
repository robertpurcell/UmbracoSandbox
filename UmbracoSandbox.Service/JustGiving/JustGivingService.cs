namespace UmbracoSandbox.Service.JustGiving
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using UmbracoSandbox.Service.Helpers;
    using UmbracoSandbox.Service.HttpClient;
    using UmbracoSandbox.Service.Logging;

    public class JustGivingService : IJustGivingService
    {
        #region Fields

        private readonly IHttpClient _httpClient;
        private readonly ILoggingService _loggingService;
        private readonly int _charityId;
        private readonly string _endPoint;

        #endregion Fields

        #region Constructor

        public JustGivingService(IHttpClient httpClient, ILoggingService loggingService, string apiKey, string endPoint, int charityId)
        {
            _httpClient = httpClient;
            _loggingService = loggingService;
            _endPoint = string.Format("{0}{1}/v1/", endPoint, apiKey);
            _charityId = charityId;
        }

        #endregion Constructor

        #region Methods

        public bool ValidateCredentials(string email, string password)
        {
            var result = Task.Factory.StartNew(() => ValidateCredentialsTask(email, password).Result, TaskCreationOptions.LongRunning).Result;
            if (result)
            {
                _httpClient.SetAuthenticationHeader(email, password);
            }

            return result;
        }

        public string GetPageName()
        {
            var account = Task.Factory.StartNew(() => RetrieveAccountTask().Result, TaskCreationOptions.LongRunning).Result;
            if (account == null)
            {
                return string.Empty;
            }

            var preferredName = string.Format("{0}-{1}", account.FirstName, account.LastName);
            var result = Task.Factory.StartNew(() => SuggestPageNameTask(preferredName).Result, TaskCreationOptions.LongRunning).Result;
            return result;
        }

        public bool IsPageNameRegistered(string pageName)
        {
            return Task.Factory.StartNew(() => IsPageNameRegisteredTask(pageName).Result, TaskCreationOptions.LongRunning).Result;
        }

        public string CreatePage(PageRequestDto dto)
        {
            dto.CharityId = _charityId;
            if (dto.EventId != null)
            {
                return Task.Factory.StartNew(() => CreatePageTask(dto).Result, TaskCreationOptions.LongRunning).Result;
            }

            var activityType = (ActivityType)Enum.Parse(typeof(ActivityType), dto.ActivityType, true);
            switch (activityType)
            {
                case ActivityType.Birthday:
                case ActivityType.InMemory:
                case ActivityType.OtherCelebration:
                case ActivityType.Wedding:
                    break;
                default:
                    dto.EventId = CreateEvent(dto);
                    break;
            }

            return Task.Factory.StartNew(() => CreatePageTask(dto).Result, TaskCreationOptions.LongRunning).Result;
        }

        private int CreateEvent(PageRequestDto dto)
        {
            var eventRequest = new EventRequestDto
            {
                Name = dto.EventName,
                Description = dto.PageSummaryWhat,
                StartDate = dto.EventDate,
                CompletionDate = dto.EventDate,
                ExpiryDate = DateTime.Now.AddYears(1),
                EventType = dto.ActivityType
            };
            return Task.Factory.StartNew(() => CreateEventTask(eventRequest).Result, TaskCreationOptions.LongRunning).Result;
        }

        public EventDetailsDto GetEventDetails(int id)
        {
            return Task.Factory.StartNew(() => GetEventDetailsTask(id).Result, TaskCreationOptions.LongRunning).Result;
        }

        public IList<EventDetailsDto> GetEvents()
        {
            return Task.Factory.StartNew(() => GetEventsTask().Result, TaskCreationOptions.LongRunning).Result;
        }

        private async Task<bool> ValidateCredentialsTask(string username, string password)
        {
            try
            {
                var validationRequest = new ValidationRequestDto
                {
                    Email = username,
                    Password = password
                };
                var url = string.Format("{0}account/validate", _endPoint);
                var response = _httpClient.PostAsJsonAsync(url, validationRequest);
                response.Result.EnsureSuccessStatusCode();
                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonHelper.Deserialize<ValidationResponseDto>(content);

                return result.IsValid;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error validating JustGiving account: {0}", ex.Message),
                    LogLevel.Error);
            }

            return false;
        }

        private async Task<AccountDetailsDto> RetrieveAccountTask()
        {
            try
            {
                var url = string.Format("{0}account", _endPoint);
                var response = _httpClient.GetAsync(url);
                response.Result.EnsureSuccessStatusCode();
                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonHelper.Deserialize<AccountDetailsDto>(content);

                return result;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error retrieving JustGiving account information: {0}", ex.Message),
                    LogLevel.Error);
            }

            return null;
        }

        private async Task<string> SuggestPageNameTask(string preferredName)
        {
            try
            {
                var url = string.Format("{0}fundraising/pages/suggest?preferredname={1}", _endPoint, preferredName);
                var response = _httpClient.GetAsync(url);
                response.Result.EnsureSuccessStatusCode();
                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonHelper.Deserialize<SuggestPageNameDto>(content);

                return result.Names.First();
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error retrieving JustGiving suggested page names: {0}", ex.Message),
                    LogLevel.Error);
            }

            return string.Empty;
        }

        private async Task<bool> IsPageNameRegisteredTask(string pageName)
        {
            try
            {
                var url = string.Format("{0}fundraising/pages/{1}", _endPoint, pageName);
                var response = _httpClient.GetAsync(url);
                await response.Result.Content.ReadAsStringAsync();

                return response.Result.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error checking JustGiving page name: {0}", ex.Message),
                    LogLevel.Error);
            }

            return false;
        }

        private async Task<string> CreatePageTask(PageRequestDto dto)
        {
            try
            {
                var url = string.Format("{0}fundraising/pages", _endPoint);
                var response = _httpClient.PutAsJsonAsync(url, dto);
                response.Result.EnsureSuccessStatusCode();
                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonHelper.Deserialize<PageResponseDto>(content);

                return result.Next.Uri;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error creating JustGiving page: {0}", ex.Message),
                    LogLevel.Error);
            }

            return string.Empty;
        }

        private async Task<int> CreateEventTask(EventRequestDto dto)
        {
            try
            {
                var url = string.Format("{0}event", _endPoint);
                var response = _httpClient.PostAsJsonAsync(url, dto);
                response.Result.EnsureSuccessStatusCode();
                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonHelper.Deserialize<EventResponseDto>(content);

                return result.Id;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error creating JustGiving event: {0}", ex.Message),
                    LogLevel.Error);
            }

            return 0;
        }
        
        private async Task<EventDetailsDto> GetEventDetailsTask(int id)
        {
            try
            {
                var url = string.Format("{0}event/{1}", _endPoint, id);
                var response = _httpClient.GetAsJsonAsync(url);
                response.Result.EnsureSuccessStatusCode();
                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonHelper.Deserialize<EventDetailsDto>(content);

                return result;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error retrieving JustGiving event details: {0}", ex.Message),
                    LogLevel.Error);
            }

            return null;
        }

        private async Task<IList<EventDetailsDto>> GetEventsTask()
        {
            try
            {
                var url = string.Format("{0}charity/{1}/events", _endPoint, _charityId);
                var response = _httpClient.GetAsJsonAsync(url);
                response.Result.EnsureSuccessStatusCode();
                var content = await response.Result.Content.ReadAsStringAsync();
                var result = JsonHelper.Deserialize<EventsDto>(content);

                return result.Events;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error retrieving JustGiving events: {0}", ex.Message),
                    LogLevel.Error);
            }

            return null;
        }

        #endregion Methods
    }
}
