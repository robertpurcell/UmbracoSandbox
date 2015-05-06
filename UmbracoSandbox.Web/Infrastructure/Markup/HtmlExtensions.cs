namespace UmbracoSandbox.Web.Infrastructure.Markup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using HtmlAgilityPack;

    public static class HtmlExtensions
    {
        #region Html extensions

        /// <summary>
        /// Validates Html input
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="input">Html input</param>
        /// <returns>Validated output</returns>
        public static IHtmlString ValidateHtml(this HtmlHelper helper, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(input);

            return !htmlDoc.ParseErrors.Any() ? new MvcHtmlString(input) : null;
        }

        /// <summary>
        /// Outputs Html attribute
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Html attribute</returns>
        public static IHtmlString Attribute(this HtmlHelper helper, string name, string value)
        {
            return new MvcHtmlString(string.IsNullOrEmpty(value) ? null : string.Format(@" {0}=""{1}""", name, value));
        }

        /// <summary>
        /// Required label for
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="helper">Html helper</param>
        /// <param name="expression">An expression that identifies the property to display</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns>An HTML label element with added asterisk if the field is required</returns>
        public static IHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return RequiredLabelFor(helper, expression, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Required label for
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="helper">Html helper</param>
        /// <param name="expression">An expression that identifies the property to display</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns>An HTML label element with added asterisk if the field is required</returns>
        public static IHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            labelText += metadata.IsRequired ? " *" : string.Empty;
            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        #endregion
    }
}
