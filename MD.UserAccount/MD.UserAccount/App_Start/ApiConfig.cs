using MD.Common.Handlers;
using MD.UserAccount.Helper;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace MD.UserAccount
{
    public static class ApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.         
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            RouteConfig.RegisterRoutes(config);
          
            if (Settings.LogRequest)
            {
                config.MessageHandlers.Add(new RequestLoggingHandler());
            }
            
            config.Services.Replace(typeof(IExceptionHandler), new APIExceptionHandler());
        }
    }
}
