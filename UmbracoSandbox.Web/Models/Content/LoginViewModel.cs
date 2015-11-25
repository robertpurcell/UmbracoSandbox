namespace UmbracoSandbox.Web.Models.Content
{
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Forms;

    public class LoginViewModel : BasePageViewModel
    {
        #region Constructor

        public LoginViewModel()
        {
            Form = new LoginFormViewModel();
        }

        #endregion Constructor

        #region Properties

        public LoginFormViewModel Form { get; set; }

        #endregion Properties
    }
}
