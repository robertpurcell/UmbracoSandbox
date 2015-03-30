namespace UmbracoSandbox.Web.Models
{
    using System.Web;

    public class ContentPageViewModel : BasePageViewModel
    {
        public string Title { get; set; }

        public IHtmlString BodyText { get; set; }
    }
}
