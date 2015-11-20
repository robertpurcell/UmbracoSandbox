namespace UmbracoSandbox.Web.Models.Base
{
    public abstract class BaseModuleModel
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
                    ? GetType().Name
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

        #endregion Properties
    }
}
