namespace UmbracoSandbox.Web.Models
{
    using UmbracoSandbox.Web.Helpers;

    public abstract class BaseItemModel
    {
        #region Fields

        private string _alias;

        private string _title;

        #endregion

        public int Id { get; set; }

        public string DocumentTypeAlias { get; set; }

        public string Name { get; set; }

        public string Title
        {
            get
            {
                return string.IsNullOrEmpty(_title)
                    ? Name
                    : _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Alias
        {
            get
            {
                return string.IsNullOrEmpty(_alias)
                    ? this.GetType().Name
                    : _alias;
            }

            set
            {
                _alias = value;
            }
        }

        public string PartialName
        {
            get
            {
                return string.Format("_{0}", Alias);
            }
        }
    }
}
