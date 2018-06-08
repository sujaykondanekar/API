using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(MD.UserAccount.Startup))]

namespace MD.UserAccount
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app, HttpConfiguration config)
        {
            ConfigureAuth(app, config);
        }
    }
}