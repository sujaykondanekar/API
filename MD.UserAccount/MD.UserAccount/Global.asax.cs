using System.Web.Http;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "logger.config", Watch = true)]
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
