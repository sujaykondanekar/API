using MD.OAuthProviders.Abstract;
using MD.OAuthProviders.Concrete;
using MD.UserAccount.Helper;
using Ninject;

namespace MD.UserAccount.Infrastructure
{
    /// <summary>
    ///Ninject dependency resolver
    /// </summary>
    public static class DependencyResolver
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private static IKernel kernel;

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <returns></returns>
        public static IKernel GetKernel()
        {
            if (null == kernel) //singleton pattern
            {
                kernel = new StandardKernel();
                AddBindings();
            }
            return kernel;
        }

        /// <summary>
        /// Adds the bindings here.
        /// </summary>
        private static void AddBindings()
        {
            kernel.Bind<IOauthProvider>().To<FaceBookProvider>().Named(ExternalProvider.facebook.ToString());
            kernel.Bind<IOauthProvider>().To<GoogleProvider>().Named(ExternalProvider.google.ToString());
        }
    }
}