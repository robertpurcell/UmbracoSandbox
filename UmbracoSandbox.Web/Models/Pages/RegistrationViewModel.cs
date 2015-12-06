namespace UmbracoSandbox.Web.Models.Pages
{
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Forms;

    public class RegistrationViewModel : BasePageViewModel
    {
        #region Constructor

        public RegistrationViewModel()
        {
            Form = new RegistrationFormViewModel();
        }

        #endregion Constructor

        #region Properties

        public RegistrationFormViewModel Form { get; set; }

        #endregion Properties
    }
}
