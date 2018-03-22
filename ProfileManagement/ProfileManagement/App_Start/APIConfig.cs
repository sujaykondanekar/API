using MD.ProfileManagement.Handlers;
using MD.ProfileManagement.Helper;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

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
            if (Settings.AuthenticateRequest)
            {
                config.MessageHandlers.Add(new TokenAuthorizationHandler());
            }
            config.Services.Replace(typeof(IExceptionHandler), new APIExceptionHandler());
        }
    }
}
