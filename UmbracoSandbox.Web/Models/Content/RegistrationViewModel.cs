namespace UmbracoSandbox.Web.Models.Content
{
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Forms;

    public class RegistrationViewModel : BasePageViewModel
    {
        #region Constructor

        public RegistrationViewModel()
        {
            Form = new RegistrationForm();
        }

        #endregion Constructor

        #region Properties

        public RegistrationForm Form { get; set; }

        #endregion Properties
    }
}
