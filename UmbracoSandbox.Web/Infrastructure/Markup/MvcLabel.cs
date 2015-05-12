namespace UmbracoSandbox.Web.Infrastructure.Markup
{
    using System;
    using System.Web.Mvc;

    public class MvcLabel : IDisposable
    {
        private readonly ViewContext _viewContext;
        private readonly string _labelText;
        private bool _disposed;

        public MvcLabel(ViewContext viewContext, string labelText)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;
            _labelText = labelText;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            HtmlLabelExtensions.EndLabel(_viewContext, _labelText);
        }

        public void EndLabel()
        {
            Dispose(true);
        }
    }
}