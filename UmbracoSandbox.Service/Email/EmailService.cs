namespace UmbracoSandbox.Service.EmailService
{
    using System;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Web;

    public class EmailService : IEmailService
    {
        #region Fields

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

        #region Methods

        /// <summary>
        /// Sends an email for the given details
        /// </summary>
        /// <param name="emailDetail">Email details</param>
        public void Send(EmailDetail emailDetail)
        {
            var context = HttpContext.Current;
            var from = string.IsNullOrEmpty(emailDetail.From) ? _emailAddress : emailDetail.From;
            var displayName = string.IsNullOrEmpty(emailDetail.From) ? _displayName : emailDetail.DisplayName;
            var body = context != null
                ? RelativeToAbsoluteUrls(emailDetail.Body, "http://" + context.Request.ServerVariables["HTTP_HOST"])
                : emailDetail.Body;
            var mail = new MailMessage
            {
                From = new MailAddress(from, displayName),
                Subject = emailDetail.Subject,
                Body = body,
                IsBodyHtml = emailDetail.IsBodyHtml
            };
            if (emailDetail.To != null && emailDetail.To.Count != 0)
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

            if (emailDetail.Bcc != null && emailDetail.Bcc.Count != 0)
            {
                foreach (var to in emailDetail.Bcc)
                {
                    mail.Bcc.Add(new MailAddress(to));
                }
            }

            if (emailDetail.Attachments != null && emailDetail.Attachments.Count != 0)
            {
                foreach (var file in emailDetail.Attachments)
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        mail.Attachments.Add(new Attachment(AppDomain.CurrentDomain.BaseDirectory + file));
                    }
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
                    case SmtpStatusCode.GeneralFailure | SmtpStatusCode.ServiceNotAvailable | SmtpStatusCode.SyntaxError |
                        SmtpStatusCode.CommandUnrecognized | SmtpStatusCode.TransactionFailed | SmtpStatusCode.BadCommandSequence:
                        throw new Exception("ERROR sending mail with code:" + ex.StatusCode + ": " + ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Validates an email address using the class that will eventually send to it
        /// </summary>
        /// <param name="emailAddress">Email address</param>
        /// <returns>Whether it's valid or not</returns>
        public bool EmailAddressIsValid(string emailAddress)
        {
            try
            {
                var addr = new MailAddress(emailAddress);

                return true;
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
        /// <param name="absoluteUrl">Base absolute URLs</param>
        /// <returns>Amended string</returns>
        private string RelativeToAbsoluteUrls(string text = "", string absoluteUrl = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var baseUri = new Uri(absoluteUrl);
            var pattern = @"(?<name>src|href)=""(?<value>/[^""]*)""";
            var matchEvaluator = new MatchEvaluator(
                    match =>
                    {
                        var value = match.Groups["value"].Value;
                        Uri uri;
                        if (Uri.TryCreate(baseUri, value, out uri))
                        {
                            var name = match.Groups["name"].Value;
                            return string.Format("{0}=\"{1}\"", name, uri.AbsoluteUri);
                        }

                        return null;
                    });
            var adjustedHtml = Regex.Replace(text, pattern, matchEvaluator);

            return adjustedHtml;
        }

        #endregion
    }
}
