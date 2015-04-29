namespace UmbracoSandbox.Web.Infrastructure.Markup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class LabelExtensions
    {
        public static MvcHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return RequiredLabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var sb = new StringBuilder();
            sb.Append(labelText);
            if (metadata.IsRequired)
            {
                sb.Append(" *");
            }

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(sb.ToString());

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}
