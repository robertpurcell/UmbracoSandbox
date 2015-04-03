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
        #region Methods

        /// <summary>
        /// Maps a file model
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="contentToMapFrom">Content to map from</param>
        /// <param name="propertyAlias">Property alias</param>
        /// <param name="recursive">Recursive</param>
        /// <returns>File model</returns>
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

        /// <summary>
        /// Maps a list of file models
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="contentToMapFrom">Content to map from</param>
        /// <param name="propertyAlias">Property alias</param>
        /// <param name="recursive">Recursive</param>
        /// <returns>List of file models</returns>
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

        /// <summary>
        /// Maps the image model from the media node
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="mediaModel">Media model</param>
        /// <returns>Image model</returns>
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

        /// <summary>
        /// Maps the image model by property alias
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="contentToMapFrom">Content to map from</param>
        /// <param name="propertyAlias">Property alias</param>
        /// <param name="recursive">Recursive</param>
        /// <returns>Image model</returns>
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

        #endregion

        #region Helpers

        /// <summary>
        /// Maps the file properties
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="mediaFile">Media file</param>
        /// <param name="mediaNode">Media node</param>
        private static void MapFileProperties(IUmbracoMapper mapper, FileModel mediaFile, IPublishedContent mediaNode)
        {
            mediaFile.Id = mediaNode.Id;
            mediaFile.Name = mediaNode.Name;
            if (mediaNode.HasProperty("umbracoFile"))
            {
                mediaFile.Url = mediaNode.Url;
                mediaFile.DocumentTypeAlias = mediaNode.DocumentTypeAlias;
                mediaFile.DomainWithUrl = mapper.AssetsRootUrl + mediaNode.Url;
                mediaFile.Size = mediaNode.GetPropertyValue<int>("umbracoBytes");
                mediaFile.FileExtension = mediaNode.GetPropertyValue<string>("umbracoExtension");
            }
        }

        /// <summary>
        /// Maps the file properties
        /// </summary>
        /// <param name="imageModel">Image model</param>
        /// <param name="mediaNode">Media node</param>
        private static void MapImageProperties(ImageModel imageModel, IPublishedContent mediaNode)
        {
            imageModel.Width = mediaNode.GetPropertyValue<int>("umbracoWidth");
            imageModel.Height = mediaNode.GetPropertyValue<int>("umbracoHeight");
            imageModel.AltText = mediaNode.GetPropertyValue<string>("altText") ?? mediaNode.Name;
            MapImageCrops(imageModel, mediaNode);
        }

        /// <summary>
        /// Map image crops model
        /// </summary>
        /// <param name="imageModel">Image model</param>
        /// <param name="mediaNode">Media node</param>
        private static void MapImageCrops(ImageModel imageModel, IPublishedContent mediaNode)
        {
            try
            {
                var imageCrops = JsonConvert.DeserializeObject<ImageCropperModel>(mediaNode.GetPropertyValue<string>("umbracoFile"));
                imageModel.Crops = imageCrops.Crops.ToDictionary(x => x.Alias, x => mediaNode.GetCropUrl(x.Alias));
            }
            catch
            {
                imageModel.Crops = null;
            }
        }

        #endregion
    }
}
