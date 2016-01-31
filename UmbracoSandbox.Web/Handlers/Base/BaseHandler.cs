namespace UmbracoSandbox.Web.Handlers.Base
{
    using Zone.UmbracoMapper;

    public abstract class BaseHandler
    {
        #region Constructor

        protected BaseHandler(IUmbracoMapper mapper)
        {
            Mapper = mapper;
        }

        #endregion Constructor

        #region Properties

        protected IUmbracoMapper Mapper { get; private set; }

        #endregion Properties
    }
}
