using System.Web.Http;

namespace ProfileManagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(APIConfig.Register);           
        }
    }
}
