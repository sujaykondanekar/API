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

        public static int TokenExpirationDurationInDays
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["tokenExpirationDurationInDays"]);
            }
        }
    }
}