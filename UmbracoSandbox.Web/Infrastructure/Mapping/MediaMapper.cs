namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class MediaMapper
    {
        public static object GetFile(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var mediaModel = contentToMapFrom.GetPropertyValue<IPublishedContent>(propertyAlias, recursive);
            if (mediaModel == null)
            {
                return null;
            }

            var mediaFile = new FileModel();
            MapFileProperties(mapper, mediaFile, mediaModel);

            return mediaFile;
        }

        public static object GetFiles(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var mediaModels = contentToMapFrom.GetPropertyValue<IEnumerable<IPublishedContent>>(propertyAlias, recursive);
            if (mediaModels == null)
            {
                return null;
            }

            var mediaFiles = new List<FileModel>();
            foreach (var mediaModel in mediaModels)
            {
                var mediaFile = new FileModel();
                MapFileProperties(mapper, mediaFile, mediaModel);
                mediaFiles.Add(mediaFile);
            }

            return mediaFiles;
        }

        public static object GetImage(IUmbracoMapper mapper, IPublishedContent mediaModel)
        {
            if (mediaModel == null)
            {
                return null;
            }

            var image = new ImageModel();
            MapFileProperties(mapper, image, mediaModel);
            MapImageProperties(image, mediaModel);

            return image;
        }

        public static object GetImage(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var mediaModel = contentToMapFrom.GetPropertyValue<IPublishedContent>(propertyAlias, recursive);
            if (mediaModel == null)
            {
                return null;
            }

            var image = new ImageModel();
            MapFileProperties(mapper, image, mediaModel);
            MapImageProperties(image, mediaModel);

            return image;
        }

        private static void MapFileProperties(IUmbracoMapper mapper, FileModel mediaFile, IPublishedContent mediaModel)
        {
            mediaFile.Id = mediaModel.Id;
            mediaFile.Name = mediaModel.Name;
            if (mediaModel.HasProperty("umbracoFile"))
            {
                mediaFile.Url = mediaModel.Url;
                mediaFile.DocumentTypeAlias = mediaModel.DocumentTypeAlias;
                mediaFile.DomainWithUrl = mapper.AssetsRootUrl + mediaModel.Url;
                mediaFile.Size = mediaModel.GetPropertyValue<int>("umbracoBytes");
                mediaFile.FileExtension = mediaModel.GetPropertyValue<string>("umbracoExtension");
            }
        }

        private static void MapImageProperties(ImageModel image, IPublishedContent mediaModel)
        {
            image.Width = mediaModel.GetPropertyValue<int>("umbracoWidth");
            image.Height = mediaModel.GetPropertyValue<int>("umbracoHeight");
            image.AltText = mediaModel.GetPropertyValue<string>("altText") ?? mediaModel.Name;
            MapImageCrops(image, mediaModel);
        }

        private static void MapImageCrops(ImageModel image, IPublishedContent mediaModel)
        {
            try
            {
                var imageCrops = JsonConvert.DeserializeObject<ImageCropperModel>(mediaModel.GetPropertyValue<string>("umbracoFile"));
                image.Crops = imageCrops.Crops.ToDictionary(x => x.Alias, x => mediaModel.GetCropUrl(x.Alias));
            }
            catch
            {
                image.Crops = null;
            }
        }
    }
}
