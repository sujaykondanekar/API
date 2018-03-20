using ProfileManagement.Manager;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ProfileManagement
{
    public static class APIContainerConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IProfileDataManager, ProfileDataManager>();
            container.RegisterType<IProfileManager, ProfileManager>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}