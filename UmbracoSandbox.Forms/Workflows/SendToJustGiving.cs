namespace UmbracoSandbox.Forms.Workflows
{
    using System;
    using System.Collections.Generic;
    using Ninject;
    using Umbraco.Forms.Core;
    using Umbraco.Forms.Core.Attributes;
    using Umbraco.Forms.Core.Enums;

    using UmbracoSandbox.Common.Helpers;
    using UmbracoSandbox.Forms.Config;
    using UmbracoSandbox.Forms.Models;
    using UmbracoSandbox.Service.JustGiving;
    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web;
    using UmbracoSandbox.Web.Helpers;

    public class SendToJustGiving : WorkflowType
    {
        #region Fields

        private readonly ILoggingService _loggingService;

        #endregion Fields

        #region Constructor

        public SendToJustGiving()
        {
            Id = new Guid("b63f089f-6a6c-4f0b-9a7a-cd3249175595");
            Name = "Send to Just Giving";
            Description = "This workflow will post the user form data to Just Giving.";
            _loggingService = NinjectWebCommon.Kernel.GetService<ILoggingService>();
        }

        #endregion Constructor

        #region Properties

        [Setting("Just Giving Field Mappings",
            description = "Enter the Just Giving field mappings to be used",
            view = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/prevaluefieldmapper.html")]
        public string Mappings { get; set; }

        [Inject]
        public IJustGivingService JustGivingService { get; set; }

        #endregion Properties

        #region Methods

        public override List<Exception> ValidateSettings()
        {   
            var l = new List<Exception>();

            return l;
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            var values = JsonHelper.Deserialize<MappingDto>(Mappings);
            foreach (var mapping in values.Mappings)
            {
                switch (mapping.Alias)
                {
                    case MappingAliases.JustGiving.EventId:
                        break;
                }
            }

            const string username = "rpurcell@thisiszone.com";
            const string password = "Zonepa55";
            var page = new PageRequestDto();
            if (!JustGivingService.ValidateCredentials(username, password))
            {
                return WorkflowExecutionStatus.Failed;
            }

            var pageUrl = CreatePage(username, password, page);
            return string.IsNullOrEmpty(pageUrl) ? WorkflowExecutionStatus.Failed : WorkflowExecutionStatus.Completed;
        }

        #endregion Methods

        #region Helpers

        private string CreatePage(string username, string password, PageRequestDto page)
        {
            try
            {
                if (JustGivingService.ValidateCredentials(username, password))
                {
                    return JustGivingService.CreatePage(page);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Failure creating JustGiving fundraising page: {0}", ex.Message), LogLevel.Error);
            }

            return string.Empty;
        }

        #endregion Helpers
    }
}
