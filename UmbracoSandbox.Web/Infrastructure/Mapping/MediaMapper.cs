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
            var media = contentToMapFrom.GetPropertyValue<IPublishedContent>(propertyAlias, recursive);
            if (media == null)
            {
                return null;
            }

            var file = new FileModel();
            mapper.Map(media, file);

            return file;
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
            var mediaList = contentToMapFrom.GetPropertyValue<IEnumerable<IPublishedContent>>(propertyAlias, recursive);
            if (mediaList == null)
            {
                return null;
            }

            var files = new List<FileModel>();
            foreach (var media in mediaList)
            {
                var file = new FileModel();
                mapper.Map(media, file);
                files.Add(file);
            }

            return files;
        }

        /// <summary>
        /// Maps the image model from the media node
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="mediaModel">Media model</param>
        /// <returns>Image model</returns>
        public static object GetImage(IUmbracoMapper mapper, IPublishedContent media)
        {
            if (media == null)
            {
                return null;
            }

            var image = new ImageModel();
            mapper.Map(media, image);
            MapImageCrops(media, image);

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

            return GetImage(mapper, mediaModel);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Map image crops model
        /// </summary>
        /// <param name="imageModel">Image model</param>
        /// <param name="mediaNode">Media node</param>
        private static void MapImageCrops(IPublishedContent media, ImageModel image)
        {
            try
            {
                var imageCrops = JsonConvert.DeserializeObject<ImageCropperModel>(media.GetPropertyValue<string>("umbracoFile"));
                image.Crops = imageCrops.Crops.ToDictionary(x => x.Alias, x => media.GetCropUrl(x.Alias));
            }
            catch
            {
                image.Crops = null;
            }
        }

        #endregion
    }
}
