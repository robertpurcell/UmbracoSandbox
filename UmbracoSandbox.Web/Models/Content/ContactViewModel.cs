namespace UmbracoSandbox.Web.Models
{
    using UmbracoSandbox.Web.Models.Forms;

    public class ContactViewModel : BasePageViewModel
    {
        #region Constructor

        public ContactViewModel()
        {
            Form = new ContactForm();
        }

        #endregion

        public ContactForm Form { get; set; }
    }
}
