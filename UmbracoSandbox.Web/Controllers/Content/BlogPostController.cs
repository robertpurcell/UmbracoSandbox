namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using GravatarHelper;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class BlogPostController : BaseController
    {
        #region Constructor

        public BlogPostController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult BlogPost()
        {
            var vm = GetPageModel<BlogPostViewModel>();
            vm.ImageUrl = GravatarHelper.CreateGravatarUrl("rpurcell@thisiszone.com", 200, string.Empty, null, null, null);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
