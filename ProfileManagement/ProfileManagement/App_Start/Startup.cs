using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(MD.ProfileManagement.Startup))]

namespace MD.ProfileManagement
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(CorsOptions.AllowAll);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
            });
        }
    }
}
