namespace UmbracoSandbox.Service.Email
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Web;

    using UmbracoSandbox.Service.Logging;

    public class EmailService : IEmailService
    {
        #region Fields

        private readonly ILoggingService _loggingService;
        private const string UrlPattern = @"(?<name>src|href)=""(?<value>/[^""]*)""";
        private static string _emailAddress;
        private static string _displayName;

        #endregion Fields

        #region Constructor

        public EmailService(ILoggingService loggingService, string emailAddress, string displayName)
        {
            _loggingService = loggingService;
            _emailAddress = emailAddress;
            _displayName = displayName;
        }

        #endregion Constructor

        #region Interface methods

        /// <summary>
        /// Sends an email for the given details
        /// </summary>
        /// <param name="emailDetail">Email details</param>
        public void Send(EmailDetail emailDetail)
        {
            var mail = new MailMessage
            {
                Subject = emailDetail.Subject,
                Body = RelativeToAbsoluteUrls(emailDetail.Body),
                IsBodyHtml = emailDetail.IsBodyHtml
            };
            AddSender(mail, emailDetail);
            AddRecipients(mail, emailDetail);
            AddBlindCopyRecipients(mail, emailDetail);
            AddAttachments(mail, emailDetail);
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                _loggingService.Log(string.Format("Error sending mail with code: {0}, {1}", ex.StatusCode, ex.Message), LogLevel.Error);
            }
        }

        /// <summary>
        /// Validates an email address using the class that will eventually send to it
        /// </summary>
        /// <param name="address">Email address</param>
        /// <returns>Whether or not the email address is valid</returns>
        public bool EmailAddressIsValid(string address)
        {
            try
            {
                return string.Equals(new MailAddress(address).Address, address);
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Email address is invalid: {0}", ex.Message), LogLevel.Error);
                return false;
            }
        }

        #endregion Interface methods

        #region Helpers

        private static void AddSender(MailMessage mail, EmailDetail emailDetail)
        {
            mail.From = string.IsNullOrEmpty(emailDetail.From)
                ? new MailAddress(_emailAddress, _displayName)
                : new MailAddress(emailDetail.From, emailDetail.DisplayName);
        }

        private static void AddRecipients(MailMessage mail, EmailDetail emailDetail)
        {
            if (emailDetail.To == null || !emailDetail.To.Any())
            {
                mail.To.Add(new MailAddress(_emailAddress));
                return;
            }

            foreach (var to in emailDetail.To.Distinct())
            {
                mail.To.Add(new MailAddress(to));
            }
        }

        private static void AddBlindCopyRecipients(MailMessage mail, EmailDetail emailDetail)
        {
            if (emailDetail.Bcc == null || !emailDetail.Bcc.Any())
            {
                return;
            }

            foreach (var to in emailDetail.Bcc.Distinct())
            {
                mail.Bcc.Add(new MailAddress(to));
            }
        }

        private static void AddAttachments(MailMessage mail, EmailDetail emailDetail)
        {
            if (emailDetail.Attachments == null || !emailDetail.Attachments.Any())
            {
                return;
            }

            foreach (var file in emailDetail.Attachments.Where(x => !string.IsNullOrEmpty(x)))
            {
                mail.Attachments.Add(new Attachment(string.Concat(AppDomain.CurrentDomain.BaseDirectory, file)));
            }
        }

        /// <summary>
        /// Method to replace relative URLs with absolute URLs
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Amended string</returns>
        private static string RelativeToAbsoluteUrls(string text = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var context = HttpContext.Current;
            if (context == null)
            {
                return text;
            }

            var baseUri = new Uri(string.Format("{0}://{1}", context.Request.Url.Scheme, context.Request.ServerVariables["HTTP_HOST"]));
            var matchEvaluator = new MatchEvaluator(
                match =>
                {
                    var value = match.Groups["value"].Value;
                    Uri uri;
                    if (!Uri.TryCreate(baseUri, value, out uri))
                    {
                        return null;
                    }

                    var name = match.Groups["name"].Value;

                    return string.Format("{0}=\"{1}\"", name, uri.AbsoluteUri);
                });
            var adjustedHtml = Regex.Replace(text, UrlPattern, matchEvaluator);

            return adjustedHtml;
        }

        #endregion Helpers
    }
}
