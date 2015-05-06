﻿namespace UmbracoSandbox.Web.Models.Interfaces
{
    using UmbracoSandbox.Web.Models.Media;

    public interface IMetadata
    {
        string MetaTitle { get; }

        string MetaTitleSuffix { get; }

        string MetaDescription { get; }

        string CanonicalUrl { get; }

        string SocialUrl { get; }

        string SocialSiteName { get; }

        string SocialTitle { get; }

        string SocialDescription { get; }

        ImageModel SocialImage { get; }

        string SocialImageUrl { get; }
    }
}
