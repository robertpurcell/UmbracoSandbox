namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class MediaMapper
    {
        private static string[] crops = { "Hero", "Teaser", "Page" };

        public static object GetMediaFile(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var mediaModel = contentToMapFrom.GetMedia(propertyName, recursive);
            if (mediaModel == null)
            {
                return null;
            }
            
            var mediaFile = new MediaFileModel();
            MapMediaFileProperties(mediaFile, mediaModel, mapper.AssetsRootUrl);

            return mediaFile;
        }

        public static object GetImage(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var mediaModel = contentToMapFrom.GetMedia(propertyName, recursive);
            if (mediaModel == null)
            {
                return null;
            }

            var image = new ImageModel();
            MapMediaFileProperties(image, mediaModel, mapper.AssetsRootUrl);
            MapImageProperties(image, mediaModel);

            return image;
        }

        private static void MapMediaFileProperties(MediaFileModel mediaFile, IPublishedContent mediaModel, string rootUrl)
        {
            mediaFile.Id = mediaModel.Id;
            mediaFile.Name = mediaModel.Name;
            mediaFile.Url = mediaModel.Url;
            mediaFile.DocumentTypeAlias = mediaModel.DocumentTypeAlias;
            mediaFile.DomainWithUrl = rootUrl + mediaModel.Url;

            mediaFile.Size = mediaModel.GetPropertyValue<int>("umbracoBytes");
            mediaFile.FileExtension = mediaModel.GetPropertyValue<string>("umbracoExtension");
        }

        private static void MapImageProperties(ImageModel image, IPublishedContent mediaModel)
        {
            image.Width = mediaModel.GetPropertyValue<int>("umbracoWidth");
            image.Height = mediaModel.GetPropertyValue<int>("umbracoHeight");
            image.AltText = mediaModel.GetPropertyValue<string>("altText") ?? mediaModel.Name;
            var cropUrls = new Dictionary<string, string>();
            for (var i = 0; i < crops.Length; i++)
            {
                cropUrls.Add(crops[i], mediaModel.GetCropUrl(crops[i]));
            }

            image.CropUrls = cropUrls;
        }
    }
}
