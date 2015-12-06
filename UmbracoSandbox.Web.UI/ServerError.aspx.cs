namespace UmbracoSandbox.Web.UI
{
    using System;
    using System.Web.UI;

    /// <summary>
    /// Server error page code behind
    /// </summary>
    public partial class ServerError : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 500;
        }
    }
}