namespace UmbracoSandbox.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using log4net;
    using Newtonsoft.Json;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using UmbracoSandbox.Web.Infrastructure.Mapping;
    using UmbracoSandbox.Web.Models;
    using Zone.GoogleMaps;
    using Zone.UmbracoMapper;

    public abstract class BaseController : SurfaceController, IRenderMvcController
    {
        private static ILog _log;

        #region Constructor

        protected BaseController(IUmbracoMapper mapper)
        {
            Mapper = mapper;
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            AddCustomMappings();
        }

        #endregion

        #region Properties

        protected IUmbracoMapper Mapper { get; private set; }

        public static ILog Log
        {
            get
            {
                return _log;
            }

            set
            {
                _log = value;
            }
        }

        #endregion

        #region Render MVC

        /// <summary>
        /// Locates the template for the given route
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="model">Instance of model</param>
        /// <returns>Template for given route</returns>
        protected ActionResult CurrentTemplate<T>(T model)
        {
            return View(ControllerContext.RouteData.Values["action"].ToString(), model);
        }

        /// <summary>
        /// Applies the default index action
        /// </summary>
        /// <param name="model">Instance of model</param>
        /// <returns>Default ActionResult</returns>
        public virtual ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }

        #endregion

        #region Mapping helpers

        /// <summary>
        /// Sets up the custom mappings for the Umbraco Mapper used in the project
        /// </summary>
        private void AddCustomMappings()
        {
            Mapper.AddCustomMapping(typeof(MediaFileModel).FullName, MediaMapper.GetMediaFile)
                .AddCustomMapping(typeof(ImageModel).FullName, MediaMapper.GetImage)
                .AddCustomMapping(typeof(GoogleMap).FullName, GoogleMapMapper.GetGoogleMap);
        }

        /// <summary>
        /// Gets a list of properties that should be treated recursively when mapping (i.e. the site-wide settings
        /// stored on the home page node)
        /// </summary>
        /// <returns>Array of properties</returns>
        protected virtual string[] GetRecursiveProperties()
        {
            return new string[] { "oGSiteName", "linkedInText", "twitterText", "linkedInUrl", "twitterUrl" };
        }

        /// <summary>
        /// Helper to get related (picked) content for a given content item and property alias
        /// </summary>
        /// <param name="content">Instance of IPublishedContent</param>
        /// <param name="propertyAlias">Property alias to get the picked node Ids from</param>
        /// <returns>List of IPublishedContent</returns>
        /// <remarks>Utilises the 'Umbraco Core Property Editor Converters' package</remarks>
        protected IEnumerable<IPublishedContent> GetRelatedContent(IPublishedContent content, string propertyAlias)
        {
            return content.GetPropertyValue<IEnumerable<IPublishedContent>>(propertyAlias);
        }

        /// <summary>
        /// Get method for string property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <returns>Property value as string</returns>
        protected string Get(IPublishedContent content, string get)
        {
            return content.GetProperty(get) != null && content.GetProperty(get).Value != null ? content.GetProperty(get).Value.ToString() : string.Empty;
        }

        /// <summary>
        /// Recursive Get method for string property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property name</param>
        /// <param name="recursive">Boolean stating whether to look for the property recursively</param>
        /// <returns>Property value as string</returns>
        protected string Get(IPublishedContent content, string get, bool recursive)
        {
            return content.GetPropertyValue<string>(get, recursive, string.Empty);
        }

        /// <summary>
        /// Get method for integer property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <returns>Property value as integer or zero</returns>
        protected int GetIdValueOrZero(IPublishedContent content, string get)
        {
            return content.GetProperty(get) == null || content.GetProperty(get).Value.ToString() == string.Empty
                        ? 0
                        : int.Parse(content.GetProperty(get).Value.ToString());
        }

        /// <summary>
        /// Recursive Get method for integer property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <param name="recursive">Boolean stating whether to look for the property recursively</param>
        /// <returns>Property value as integer or zero</returns>
        protected int GetIdValueOrZero(IPublishedContent content, string get, bool recursive)
        {
            return content.GetPropertyValue<int>(get, recursive, 0);
        }

        /// <summary>
        /// Get method for boolean property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <returns>Property value as boolean</returns>
        protected bool GetBool(IPublishedContent content, string get)
        {
            return content.GetProperty(get) != null ? content.GetPropertyValue<bool>(get) ? true : false : false;
        }

        /// <summary>
        /// Get method for boolean property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <param name="recursive">Boolean stating whether to look for the property recursively</param>
        /// <returns>Property value as boolean</returns>
        protected bool GetBool(IPublishedContent content, string get, bool recursive)
        {
            return content.GetProperty(get) != null ? content.GetPropertyValue<bool>(get, recursive, false) : false;
        }

        /// <summary>
        /// Get method for related links
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <param name="recursive">Boolean stating whether to look for the property recursively</param>
        /// <returns>IEnumerable of related link objects</returns>
        protected IEnumerable<RelatedLink> GetRelatedLinks(IPublishedContent content, string get, bool recursive = false)
        {
            var json = Get(content, get, recursive);
            IEnumerable<RelatedLink> items;
            if (!string.IsNullOrEmpty(json))
            {
                items = JsonConvert.DeserializeObject<List<RelatedLink>>(json);

                return items;
            }

            return null;
        }

        /// <summary>
        /// Gets a list of IPublishedContent from multi-node tree picker property
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property name</param>
        /// <returns>IEnumerable of IPublishedContent objects</returns>
        protected IEnumerable<IPublishedContent> GetMntpItemsByCsv(IPublishedContent content, string get)
        {
            var csv = Get(content, get);
            IEnumerable<IPublishedContent> items;
            if (!string.IsNullOrEmpty(csv))
            {
                IEnumerable<int> mntpCsv = csv
                   .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(x => int.Parse(x));

                items = new UmbracoHelper(UmbracoContext.Current)
                    .TypedContent(mntpCsv)
                    .Where(x => x != null);
                return items;
            }

            return Enumerable.Empty<IPublishedContent>();
        }

        /// <summary>
        /// Gets content stored in data types in XML format on the current page as XML
        /// </summary>
        /// <param name="propertyAlias">Property alias to look up </param>
        /// <returns>XML value</returns>
        protected XElement GetXmlContent(string propertyAlias)
        {
            // Get the raw value.  As we have 'Umbraco Core Property Editor Converters' installed by default we'll get the
            // converted version.  But in this case we want the XML.
            XElement result = null;
            if (CurrentPage.GetProperty(propertyAlias) != null)
            {
                var rawValue = CurrentPage.GetProperty(propertyAlias).Value.ToString();

                // Convert to XML
                var sr = new StringReader(rawValue);
                result = XElement.Load(sr);
            }

            return result;
        }

        /// <summary>
        /// Gets the model and sets a value for the CanonicalUrl property
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Model</returns>
        protected T GetModel<T>()
            where T : BasePageViewModel, new()
        {
            var model = new T
            {
                CanonicalUrl = Request.Url == null || Request.Url.AbsolutePath == CurrentPage.Url ? null : CurrentPage.Url,
            };
            return model;
        }

        #endregion

        #region Querying helpers

        /// <summary>
        /// Gets the Umbraco root node (home page)
        /// </summary>
        /// <returns>Instance of IPublishedContent</returns>
        protected IPublishedContent GetRootNode()
        {
            return Umbraco.TypedContentAtRoot().First();
        }

        /// <summary>
        /// Gets the Umbraco root data node (data page)
        /// </summary>
        /// <returns>Instance of IPublishedContent</returns>
        protected IPublishedContent GetRootDataNode()
        {
            return Umbraco.TypedContentAtRoot().First(x => x.DocumentTypeAlias == "DataFolder");
        }

        public class RelatedLink
        {
            public string Caption { get; set; }

            public string Link { get; set; }

            public bool NewWindow { get; set; }

            public bool Edit { get; set; }

            public bool IsInternal { get; set; }

            public int Internal { get; set; }

            public string InternalName { get; set; }

            public string Type { get; set; }

            public string Title { get; set; }
        }

        #endregion
    }
}
