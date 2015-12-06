namespace UmbracoSandbox.Web.UI
{
    using System;
    using System.Web.UI;

    public partial class ServerError : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 500;
        }
    }
}