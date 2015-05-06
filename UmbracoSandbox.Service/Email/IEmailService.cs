namespace UmbracoSandbox.Service.Email
{
    public interface IEmailService
    {
        void Send(EmailDetail email);

        bool EmailAddressIsValid(string emailAddress);
    }
}
