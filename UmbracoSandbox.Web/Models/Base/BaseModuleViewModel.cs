namespace UmbracoSandbox.Web.Models.Base
{
    public abstract class BaseModuleViewModel
    {
        #region Fields

        private string _alias;

        #endregion Fields

        #region Properties

        public string Alias
        {
            get
            {
                return string.IsNullOrEmpty(_alias)
                    ? string.Format("{0}Module", DocumentTypeAlias)
                    : _alias;
            }

            set
            {
                _alias = value;
            }
        }

        public string DocumentTypeAlias { get; set; }

        public string PartialName
        {
            get
            {
                return string.Format("_{0}", Alias);
            }
        }

        #endregion Properties
    }
}
