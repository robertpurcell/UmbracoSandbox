﻿namespace UmbracoSandbox.Web.Models
{
    public abstract class BaseModuleModel
    {
        #region Fields

        private string _alias;

        #endregion

        #region Properties

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

        #endregion
    }
}