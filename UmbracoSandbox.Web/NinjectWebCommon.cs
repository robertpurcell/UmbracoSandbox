[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UmbracoSandbox.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UmbracoSandbox.Web.NinjectWebCommon), "Stop")]

namespace UmbracoSandbox.Web
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using UmbracoSandbox.Service.Email;
    using UmbracoSandbox.Service.Publishing;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Helpers;
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
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
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
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IPageHandler>().To<PageHandler>();
            kernel.Bind<IUmbracoMapper>().To<UmbracoMapper>();
            kernel.Bind<INavigationHandler>().To<NavigationHandler>();
            kernel.Bind<IListingPageHandler>().To<ListingPageHandler>();
            kernel.Bind<IRegistrationPageHandler>().To<RegistrationPageHandler>();
            kernel.Bind<IEmailService>().To<EmailService>()
                .WithConstructorArgument("emailAddress", ConfigHelper.GetSettingAsString("app.emailAddress"))
                .WithConstructorArgument("displayName", ConfigHelper.GetSettingAsString("app.displayName"));
            kernel.Bind<IPublishingService>().To<PublishingService>()
                .WithConstructorArgument("pageNotFoundFileName", ConfigHelper.GetSettingAsString("app.pageNotFoundFileName"))
                .WithConstructorArgument("serverErrorFileName", ConfigHelper.GetSettingAsString("app.serverErrorFileName"))
                .WithConstructorArgument("errorPagePublishingHostName", ConfigHelper.GetSettingAsString("app.errorPagePublishingHostName"));
        }        
    }
}
