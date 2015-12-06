namespace UmbracoSandbox.Forms.Workflows
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Umbraco.Forms.Core;
    using Umbraco.Forms.Core.Attributes;
    using Umbraco.Forms.Core.Enums;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web;
    using UmbracoSandbox.Web.Helpers;

    public class SendCustomEmail : WorkflowType
    {
        #region Constructor

        public SendCustomEmail()
        {
            Id = new Guid("27131715-f390-46c6-acd4-a1397a64da8a");
            Name = "Send custom email";
            Description = "This workflow will send a custom email to the given email address.";
        }

        #endregion Constructor

        #region Properties

        [Setting("Email Template",
            description = "Select the custom email template",
            view = "~/App_Plugins/UmbracoForms/Backoffice/Common/SettingTypes/pickers.content.html",
            prevalues = "Email Templates")]
        public string EmailTemplate { get; set; }

        public ILoggingService LoggingService { get; set; }

        #endregion Properties

        #region Methods

        public override List<Exception> ValidateSettings()
        {
            var ex = new List<Exception>();
            if (string.IsNullOrEmpty(EmailTemplate))
            {
                ex.Add(new Exception("Please select and email template."));
            }

            return ex;
        }

        public override Dictionary<string, Setting> Settings()
        {
            var settings = base.Settings();
            var storage = new Umbraco.Forms.Data.Storage.PrevalueSourceStorage();
            var prevalueSource = storage.GetPrevalueSource(new Guid("85caeb6a-7dc8-419e-a810-c4faec6c10e6"));

            // done
            return settings;
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            return WorkflowExecutionStatus.Completed;
        }

        #endregion Methods
    }
}
