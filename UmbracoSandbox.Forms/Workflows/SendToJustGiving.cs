namespace UmbracoSandbox.Forms.Workflows
{
    using System;
    using System.Collections.Generic;

    using Umbraco.Forms.Core;
    using Umbraco.Forms.Core.Attributes;
    using Umbraco.Forms.Core.Enums;

    using UmbracoSandbox.Forms.Models;
    using UmbracoSandbox.Service.JustGiving;
    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Utilities;
    using UmbracoSandbox.Web;
    using UmbracoSandbox.Web.Helpers;

    public class SendToJustGiving : WorkflowType
    {
        #region Fields

        private readonly IJustGivingService _justGivingService;
        private readonly ILoggingService _loggingService;

        #endregion Fields

        #region Constructor

        public SendToJustGiving()
        {
            Id = new Guid("b63f089f-6a6c-4f0b-9a7a-cd3249175595");
            Name = "Send to Just Giving";
            Description = "This workflow will post the user form data to Just Giving.";
            _justGivingService = NinjectWebCommon.Kernel.GetService<IJustGivingService>();
            _loggingService = NinjectWebCommon.Kernel.GetService<ILoggingService>();
        }

        #endregion Constructor

        #region Properties

        [Setting("Just Giving Field Mappings",
            description = "Enter the Just Giving field mappings to be used",
            view = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/prevaluefieldmapper.html")]
        public string Mappings { get; set; }

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

            var test = _justGivingService.ValidateCredentials("rpurcell@thisiszone.com", "Zonepa55");
      
            return WorkflowExecutionStatus.Completed;
        }

        #endregion Methods
    }
}
