using System.Configuration;

namespace MD.ProfileManagement.Helper
{
    public static class Settings
    {
        public static bool AuthenticateRequest
        {
            get
            {
                bool authenticate = true;
                bool.TryParse(ConfigurationManager.AppSettings["authenticateRequest"], out authenticate);
                return authenticate;
            }
        }

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