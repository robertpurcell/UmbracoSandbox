namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Archetype.Models;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Infrastructure.Config;
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Modules;
    using Zone.UmbracoMapper;

    public class ArchetypeMapper
    {
        #region Archetype mappers

        /// <summary>
        /// Gets model of given type when mapping from dictionary and any selected content
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="contentToMapFrom">Content to map from</param>
        /// <param name="propertyAlias">Property alias</param>
        /// <param name="recursive">Recursive</param>
        /// <returns>The model</returns>
        public static T GetModel<T>(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
            where T : BaseModuleModel, new()
        {
            var archetypeModel = contentToMapFrom.GetPropertyValue<ArchetypeModel>(propertyAlias, recursive);
            if (archetypeModel == null)
            {
                return null;
            }

            var dictionary = GetDictionary(mapper, archetypeModel);
            var result = new T();
            if (dictionary.IsAndAny())
            {
                result = GetModel<T>(mapper, dictionary.SingleOrDefault());
            }

            return result;
        }

        /// <summary>
        /// Gets collection of given type when mapping from content
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="contentToMapFrom">Content to map from</param>
        /// <param name="propertyAlias">Property alias</param>
        /// <param name="recursive">Recursive</param>
        /// <returns>Collection of models</returns>
        public static IEnumerable<T> GetCollection<T>(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
            where T : BaseModuleModel, new()
        {
            var archetypeModel = contentToMapFrom.GetPropertyValue<ArchetypeModel>(propertyAlias, recursive);
            var collection = GetArchetypeCollection(mapper, archetypeModel);
            var result = TryConvert<T>(collection);

            return result;
        }

        /// <summary>
        /// Gets collection of given type when mapping from a dictionary
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="value">Object value</param>
        /// <returns>Collection of models</returns>
        public static IEnumerable<T> GetCollectionFromValue<T>(IUmbracoMapper mapper, object value)
            where T : BaseModuleModel, new()
        {
            if (value == null)
            {
                return null;
            }

            var collection = (IEnumerable<BaseModuleModel>)value;
            return TryConvert<T>(collection);
        }

        /// <summary>
        /// Gets collection of Archetype models when mapping from content
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="contentToMapFrom">Content to map from</param>
        /// <param name="propertyAlias">Property alias</param>
        /// <param name="recursive">Recursive</param>
        /// <returns>Collection of Archetype models</returns>
        public static IEnumerable<BaseModuleModel> GetArchetypeCollection(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var archetypeModel = contentToMapFrom.GetPropertyValue<ArchetypeModel>(propertyAlias, recursive);

            return GetArchetypeCollection(mapper, archetypeModel);
        }

        /// <summary>
        /// Gets collection of custom Archetype models when mapping from a dictionary
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="value">Object value</param>
        /// <returns>Collection of Archetype models</returns>
        public static IEnumerable<BaseModuleModel> GetArchetypeCollectionFromValue(IUmbracoMapper mapper, object value)
        {
            var result = new List<BaseModuleModel>();
            if (value != null)
            {
                result = (List<BaseModuleModel>)value;
            }

            return result;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the dictionary for the Archetype model
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="archetypeModel">Archetype model</param>
        /// <returns>Collection of dictionaries</returns>
        private static IEnumerable<Dictionary<string, object>> GetDictionary(IUmbracoMapper mapper, ArchetypeModel archetypeModel)
        {
            var propertyDictionary = archetypeModel
                .Select(item => item.Properties.ToDictionary(m => m.Alias, m => GetTypedValue(mapper, m, item.Alias)));
            var dictionary = propertyDictionary
                .Zip(archetypeModel
                    .Select(item => new Dictionary<string, object> { { "alias", item.Alias } }),
                    (a, b) => a.Concat(b).ToDictionary(k => k.Key, k => k.Value,
                    StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            return dictionary;
        }

        /// <summary>
        /// Gets the typed value of the Archetype property
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="archetypeProperty">Archetype property</param>
        /// <param name="modelAlias">Model alias</param>
        /// <returns>Object of the required type</returns>
        private static object GetTypedValue(IUmbracoMapper mapper, ArchetypePropertyModel archetypeProperty, string modelAlias)
        {
            switch (archetypeProperty.PropertyEditorAlias)
            {
                case "Umbraco.ContentPickerAlias":
                    return archetypeProperty.GetValue<IPublishedContent>();
                case "Umbraco.MultiNodeTreePicker":
                    return archetypeProperty.GetValue<IEnumerable<IPublishedContent>>();
                case "Umbraco.TinyMCEv3":
                    return archetypeProperty.GetValue<IHtmlString>();
                case "Imulus.Archetype":
                    return GetArchetypeCollection(mapper, archetypeProperty.GetValue<ArchetypeModel>());
                default:
                    return archetypeProperty.Value;
            }
        }

        /// <summary>
        /// Gets custom Archetype model collection from given Archetype model
        /// </summary>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="archetypeModel">Archetype model</param>
        /// <returns>Collection of Archetype models</returns>
        private static IEnumerable<BaseModuleModel> GetArchetypeCollection(IUmbracoMapper mapper, ArchetypeModel archetypeModel)
        {
            if (archetypeModel == null)
            {
                return null;
            }

            var result = new List<BaseModuleModel>();
            var dictionary = GetDictionary(mapper, archetypeModel);
            foreach (var item in dictionary)
            {
                BaseModuleModel model;
                var alias = item.ContainsKey("alias")
                    ? item["alias"] as string
                    : string.Empty;
                switch (alias)
                {
                    case ModelAliases.Module:
                        model = GetModel<ModuleModel>(mapper, item);
                        break;
                    default:
                        model = null;
                        break;
                }

                if (model != null)
                {
                    result.Add(model);
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to convert base Archetype model collection to more specific collection 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="collection">The base model collection</param>
        /// <returns>The more specific model collection</returns>
        private static IEnumerable<T> TryConvert<T>(IEnumerable<BaseModuleModel> collection)
            where T : BaseModuleModel, new()
        {
            if (collection == null)
            {
                yield break;
            }

            var type = typeof(T).FullName;
            foreach (var item in collection.ToList())
            {
                if (item.GetType().FullName == type)
                {
                    yield return (T)item;
                }
            }
        }

        /// <summary>
        /// Gets model of given type when mapping from dictionary and selected content
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="mapper">Umbraco mapper</param>
        /// <param name="dictionary">Dictionary</param>
        /// <param name="contentAlias">The alias of the property containing the content to map from</param>
        /// <returns>The model</returns>
        private static T GetModel<T>(IUmbracoMapper mapper, Dictionary<string, object> dictionary, string contentAlias = "content")
            where T : BaseModuleModel, new()
        {
            try
            {
                var result = new T();
                var contentToMapFrom = dictionary.ContainsKey(contentAlias)
                    ? dictionary[contentAlias] as IEnumerable<IPublishedContent>
                    : null;
                mapper.Map(dictionary, result);
                if (contentToMapFrom == null)
                {
                    return result;
                }

                var contentResult = new T();
                mapper.Map(contentToMapFrom.SingleOrDefault(), contentResult);
                result = CopyValues<T>(result, contentResult);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error<ArchetypeMapper>("Error getting data for model: " + ex.InnerException, ex);
            }

            return null;
        }

        /// <summary>
        /// Copies values from one typed object to another. Used for property value overrides.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="target">The target object</param>
        /// <param name="source">The source object</param>
        /// <returns>The combined object</returns>
        private static T CopyValues<T>(T target, T source)
            where T : BaseModuleModel, new()
        {
            var t = typeof(T);
            var result = new T();
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                var targetValue = prop.GetValue(target, null);
                if (value != null && (targetValue == null || string.IsNullOrEmpty(targetValue.ToString()) ||
                    targetValue.ToString() == "0" || targetValue.ToString() == DateTime.MinValue.ToString()))
                {
                    prop.SetValue(result, value, null);
                }
                else
                {
                    prop.SetValue(result, targetValue, null);
                }
            }

            return result;
        }

        #endregion
    }
}
