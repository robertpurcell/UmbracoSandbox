namespace UmbracoSandbox.Service.Email
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Web;

    public class EmailService : IEmailService
    {
        #region Fields

        private const string UrlPattern = @"(?<name>src|href)=""(?<value>/[^""]*)""";
        private readonly string _emailAddress;
        private readonly string _displayName;

        #endregion

        #region Constructor

        public EmailService(string emailAddress, string displayName)
        {
            _emailAddress = emailAddress;
            _displayName = displayName;
        }

        #endregion

        #region Interface methods

        /// <summary>
        /// Sends an email for the given details
        /// </summary>
        /// <param name="emailDetail">Email details</param>
        public void Send(EmailDetail emailDetail)
        {
            var context = HttpContext.Current;
            var from = string.IsNullOrEmpty(emailDetail.From) ? _emailAddress : emailDetail.From;
            var displayName = string.IsNullOrEmpty(emailDetail.From) ? _displayName : emailDetail.DisplayName;
            var url = context != null ? string.Format("{0}://{1}", context.Request.Url.Scheme, context.Request.ServerVariables["HTTP_HOST"]) : string.Empty;
            var mail = new MailMessage
            {
                From = new MailAddress(from, displayName),
                Subject = emailDetail.Subject,
                Body = string.IsNullOrEmpty(url) ? emailDetail.Body : RelativeToAbsoluteUrls(emailDetail.Body, url),
                IsBodyHtml = emailDetail.IsBodyHtml
            };
            if (emailDetail.To != null && emailDetail.To.Any())
            {
                foreach (var to in emailDetail.To)
                {
                    mail.To.Add(new MailAddress(to));
                }
            }
            else
            {
                mail.To.Add(new MailAddress(_emailAddress));
            }

            if (emailDetail.Bcc != null && emailDetail.Bcc.Any())
            {
                foreach (var to in emailDetail.Bcc)
                {
                    mail.Bcc.Add(new MailAddress(to));
                }
            }

            if (emailDetail.Attachments != null && emailDetail.Attachments.Any())
            {
                foreach (var file in emailDetail.Attachments.Where(x => !string.IsNullOrEmpty(x)))
                {
                    mail.Attachments.Add(new Attachment(string.Concat(AppDomain.CurrentDomain.BaseDirectory, file)));
                }
            }
            
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                switch (ex.StatusCode)
                {
                    case SmtpStatusCode.GeneralFailure:
                    case SmtpStatusCode.ServiceNotAvailable:
                    case SmtpStatusCode.SyntaxError:
                    case SmtpStatusCode.CommandUnrecognized:
                    case SmtpStatusCode.TransactionFailed:
                    case SmtpStatusCode.BadCommandSequence:
                        throw new Exception(string.Format("Error sending mail with code: {0}: {1}", ex.StatusCode, ex.Message), ex);
                }
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
            catch
            {
                return false;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Method to replace relative URLs with absolute URLs
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="absoluteUri">Base absolute URI</param>
        /// <returns>Amended string</returns>
        private static string RelativeToAbsoluteUrls(string text = "", string absoluteUri = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var baseUri = new Uri(absoluteUri);
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

        #endregion
    }
}
