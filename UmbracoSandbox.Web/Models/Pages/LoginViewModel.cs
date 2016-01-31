namespace UmbracoSandbox.Web.Models.Pages
{
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
