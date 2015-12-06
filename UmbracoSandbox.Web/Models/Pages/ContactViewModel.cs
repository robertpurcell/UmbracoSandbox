namespace UmbracoSandbox.Web.Models.Pages
{
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Forms;

    public class ContactViewModel : BasePageViewModel
    {
        #region Constructor

        public ContactViewModel()
        {
            Form = new ContactFormViewModel();
        }

        #endregion Constructor

        #region Properties

        public ContactFormViewModel Form { get; set; }

        #endregion Properties
    }
}
