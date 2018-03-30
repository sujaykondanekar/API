using MD.ProfileManagement.DataSource.DataManager;
using MD.ProfileManagement.Manager;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace MD.ProfileManagement
{
    public static class APIContainerConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IProfileDataManager, ProfileDataManager>();
            container.RegisterType<IProfileManager, ProfileManager>();
            container.RegisterType<ILabReportDataManager, LabReportDataManager>();
            container.RegisterType<ILabReportManager, LabReportManager>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}