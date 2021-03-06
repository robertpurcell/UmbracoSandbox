﻿namespace UmbracoSandbox.Web.Models.Interfaces
{
    public interface ILoginStatus
    {
        IMenuItem Login { get; }

        bool IsLoggedIn { get; }

        string Name { get; }

        string ImageUrl { get; }
    }
}
