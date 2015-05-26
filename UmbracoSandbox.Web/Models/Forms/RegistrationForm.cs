namespace UmbracoSandbox.Web.Models.Forms
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [Serializable]
    public class RegistrationForm
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Name")]
        [StringLength(254, ErrorMessage = "Your name must be fewer than 254 characters in length.")]
        public string Name { get; set; }

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

        [Display(Name = "Check box")]
        public bool CheckBox { get; set; }

        public SelectListItem[] Days { get; set; }

        [Display(Name = "Drop down list")]
        public int SelectedDay { get; set; }

        public SelectListItem[] Months { get; set; }

        [Display(Name = "Radio button list")]
        public int SelectedMonth { get; set; }

        [Display(Name = "Check box list")]
        public SelectListItem[] Years { get; set; }
    }
}
