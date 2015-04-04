namespace UmbracoSandbox.Service.EmailService
{
    public interface IEmailService
    {
        void Send(EmailDetail email);

        bool EmailAddressIsValid(string emailAddress);
    }
}
