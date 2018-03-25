using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MD.UserAccount.Startup))]

namespace MD.UserAccount
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
