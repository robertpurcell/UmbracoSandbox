namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Linq;

    using Umbraco.Core.Models;
    using Umbraco.Web;

    using UmbracoSandbox.Common.Helpers;
    using UmbracoSandbox.Web.Infrastructure.Config;
    using UmbracoSandbox.Web.Models.Media;

    using Zone.UmbracoMapper;

    public class MediaMapper : BaseMapper
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

            return GetModel<FileViewModel>(mapper, media);
        }

        /// <summary>
        /// Maps the image model from the media node
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="media">Media node</param>
        /// <returns>Image model</returns>
        public static object GetImage(IUmbracoMapper mapper, IPublishedContent media)
        {
            var image = GetModel<ImageViewModel>(mapper, media);
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
            var media = contentToMapFrom.GetPropertyValue<IPublishedContent>(propertyAlias, recursive);

            return GetImage(mapper, media);
        }

        public static object GetImageFromValue(IUmbracoMapper mapper, object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }

            var media = new UmbracoHelper(UmbracoContext.Current).TypedMedia(value.ToString());
            var image = GetModel<ImageViewModel>(mapper, media);
            MapImageCrops(media, image);

            return image;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Map image crops model
        /// </summary>
        /// <param name="media">Media node</param>
        /// <param name="image">Image model</param>
        private static void MapImageCrops(IPublishedContent media, ImageViewModel image)
        {
            if (media == null)
            {
                return;
            }

            var imageString = media.GetPropertyValue<string>(PropertyAliases.UmbracoFile);
            var imageCrops = JsonHelper.Deserialize<ImageCropperModel>(imageString);
            image.Crops = imageCrops.Crops.ToDictionary(x => x.Alias, x => media.GetCropUrl(x.Alias));
        }

        #endregion
    }
}
