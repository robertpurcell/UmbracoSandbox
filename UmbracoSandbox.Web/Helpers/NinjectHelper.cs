namespace UmbracoSandbox.Web.Helpers
{
    using Ninject;

    public static class NinjectHelper
    {
        public static T GetService<T>(this IKernel kernel)
        {
            return (T)kernel.GetService(typeof(T));
        }
    }
}
