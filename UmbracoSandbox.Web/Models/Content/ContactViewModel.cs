namespace UmbracoSandbox.Web.Models.Content
{
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Forms;

    public class ContactViewModel : BasePageViewModel
    {
        #region Constructor

        public ContactViewModel()
        {
            Form = new ContactForm();
        }

        #endregion

        #region Properties

        public ContactForm Form { get; set; }

        #endregion
    }
}
