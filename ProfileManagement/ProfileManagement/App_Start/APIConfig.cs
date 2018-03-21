using Authorization;
using System.Web.Http;

namespace ProfileManagement
{
    public static class APIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //register routes
            APIRouteConfig.RegisterRoutes(config);

            // register dependency servive
            APIContainerConfig.RegisterComponents();

            config.MessageHandlers.Add(new TokenAuthorizationHandler());
        }
    }
}
