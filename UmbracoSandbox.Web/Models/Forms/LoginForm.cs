namespace UmbracoSandbox.Web.Models.Forms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class LoginForm
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        [Display(Name = "Email")]
        [StringLength(254, ErrorMessage = "Your email must be fewer than 254 characters in length.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [Display(Name = "Password")]
        [StringLength(254, ErrorMessage = "Your password must be fewer than 254 characters in length.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
