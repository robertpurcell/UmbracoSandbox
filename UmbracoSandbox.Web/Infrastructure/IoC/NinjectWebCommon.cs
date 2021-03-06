[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UmbracoSandbox.Web.Infrastructure.IoC.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UmbracoSandbox.Web.Infrastructure.IoC.NinjectWebCommon), "Stop")]

namespace UmbracoSandbox.Web.Infrastructure.IoC
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Syntax;
    using Ninject.Web.Common;
    using Ninject.WebApi.DependencyResolver;

    using UmbracoSandbox.Common.Helpers;
    using UmbracoSandbox.Service.Email;
    using UmbracoSandbox.Service.HttpClient;
    using UmbracoSandbox.Service.JustGiving;
    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Service.Publishing;
    using UmbracoSandbox.Web.Handlers.Errors;
    using UmbracoSandbox.Web.Handlers.Modules;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Handlers.Pages;
    using UmbracoSandbox.Web.Infrastructure.ContentLocators;
    using UmbracoSandbox.Web.Infrastructure.Mapping;

    using Zone.UmbracoMapper;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static IKernel Kernel
        {
            get { return Bootstrapper.Kernel; }
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IBindingRoot kernel)
        {
            kernel.Bind<IHttpClient>().To<HttpClientWrapper>()
                .WithConstructorArgument("timeoutInSeconds", 30);
            kernel.Bind<IUmbracoMapper>().To<CustomMapper>().InSingletonScope();
            kernel.Bind<IPageHandler>().To<PageHandler>();
            kernel.Bind<ILoggingService>().To<LoggingService>().InSingletonScope();
            kernel.Bind<IMetadataHandler>().To<MetadataHandler>();
            kernel.Bind<INavigationHandler>().To<NavigationHandler>();
            kernel.Bind<IContactPageHandler>().To<ContactPageHandler>();
            kernel.Bind<IBlogPostPageHandler>().To<BlogPostPageHandler>();
            kernel.Bind<IListingPageHandler>().To<ListingPageHandler>();
            kernel.Bind<IErrorHandler>().To<ErrorHandler>();
            kernel.Bind<IRegistrationPageHandler>().To<RegistrationPageHandler>();
            kernel.Bind<IPublishingService>().To<PublishingService>();
            kernel.Bind<IEmailService>().To<EmailService>()
                .WithConstructorArgument("emailAddress", ConfigHelper.GetSettingAsString("app.emailAddress"))
                .WithConstructorArgument("displayName", ConfigHelper.GetSettingAsString("app.displayName"));
            kernel.Bind<IJustGivingService>().To<JustGivingService>()
                .WithConstructorArgument("apiKey", ConfigHelper.GetSettingAsString("app.justGivingAPIKey"))
                .WithConstructorArgument("charityId", ConfigHelper.GetSettingAsInteger("app.justGivingCharityId"))
                .WithConstructorArgument("endPoint", ConfigHelper.GetSettingAsBoolean("app.justGivingTestMode")
                    ? ConfigHelper.GetSettingAsString("app.justGivingAPISandboxEndpoint")
                    : ConfigHelper.GetSettingAsString("app.justGivingAPIEndpoint"));

            // Register in request scope so that the UmbracoHelper created in constructor is only created once per request context
            kernel.Bind<IRootContentLocator>().To<RootContentLocator>().WhenInjectedExactlyInto<RootContentLocatorCachingProxy>().InRequestScope();
            kernel.Bind<IRootContentLocator>().To<RootContentLocatorCachingProxy>();

            // Register in request scope so that the UmbracoHelper created in constructor is only created once per request context
            kernel.Bind<IContentLocator>().To<ContentLocator>().WhenInjectedExactlyInto<ContentLocatorCachingProxy>().InRequestScope();
            kernel.Bind<IContentLocator>().To<ContentLocatorCachingProxy>();
        }        
    }
}
