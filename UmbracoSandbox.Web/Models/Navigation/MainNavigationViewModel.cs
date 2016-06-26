﻿namespace UmbracoSandbox.Web.Models.Navigation
{
    using UmbracoSandbox.Web.Models.Interfaces;

    public class MainNavigationViewModel : NavigationViewModel, ILoginStatus
    {
        public IMenuItem Login { get; set; }

        public bool ShowLoginStatus => Login != null;

        public bool IsLoggedIn { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool ShowImage => !string.IsNullOrEmpty(ImageUrl);
    }
}