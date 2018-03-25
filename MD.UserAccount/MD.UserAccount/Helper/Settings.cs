using System.Configuration;

namespace MD.UserAccount.Helper
{
    public static class Settings
    {        

        public static bool LogRequest
        {
            get
            {
                bool log = true;
                bool.TryParse(ConfigurationManager.AppSettings["logRequest"], out log);
                return log;
            }
        }
    }
}