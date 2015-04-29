namespace UmbracoSandbox.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ContactViewModel : BasePageViewModel
    {
        public IHtmlString BodyText { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Name")]
        [StringLength(254, ErrorMessage = "Your name must be fewer than 254 characters in length.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [Display(Name = "Email")]
        [StringLength(254, ErrorMessage = "Your email must be fewer than 254 characters in length.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [StringLength(254, ErrorMessage = "Your subject must be fewer than 254 characters in length.")]
        public string Subject { get; set; }

        [Display(Name = "Enter your message here:")]
        [StringLength(1000, ErrorMessage = "Your message must be fewer than 1000 characters in length.")]
        public string Message { get; set; }
    }
}
