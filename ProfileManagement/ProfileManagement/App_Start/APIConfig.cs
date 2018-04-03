using MD.ProfileManagement.Helper;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using MD.Common.Handlers;
using Microsoft.Owin.Security.OAuth;

namespace MD.ProfileManagement
{
    public static class APIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //register routes
            APIRouteConfig.RegisterRoutes(config);

            // register dependency servive
            APIContainerConfig.RegisterComponents();
            if (Settings.LogRequest)
            {
                config.MessageHandlers.Add(new RequestLoggingHandler());
            }
            config.Services.Replace(typeof(IExceptionHandler), new APIExceptionHandler());

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

        }
    }
}
