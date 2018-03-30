using System.Web.Http;

namespace MD.UserAccount
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {    
            GlobalConfiguration.Configure(ApiConfig.Register);           
        }
    }
}
