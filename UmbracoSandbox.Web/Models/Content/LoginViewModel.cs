namespace UmbracoSandbox.Web.Models.Content
{
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Forms;

    public class LoginViewModel : BasePageViewModel
    {
        #region Constructor

        public LoginViewModel()
        {
            Form = new LoginForm();
        }

        #endregion

        #region Properties

        public LoginForm Form { get; set; }

        #endregion
    }
}
