using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace MD.Authorization
{
    public class APITokenAuthorizationService : ITokenAuthorizationService
    {
        private string authenticationServiceUrl;

        public APITokenAuthorizationService()
        {
            ConstructAuthenticationServiceEndPoint();
        }

        public async Task<string> GetUserNameAsync(string token)
        {
            return await GetUserNameFromAuthenticationService(token);
        }        

        public bool TryGetUserNameAsync(string token, out string userName)
        {
            userName = GetUserNameFromAuthenticationService(token).Result;
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            return true;            
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            var userName = await GetUserNameFromAuthenticationService(token);
            if (string.IsNullOrEmpty(userName)){
                return false;
            }

            return true;
        }

        private async Task<string> GetUserNameFromAuthenticationService(string token)
        {
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                httpClient.DefaultRequestHeaders.Add("Authorization", token);
                return await httpClient.GetStringAsync(new Uri(authenticationServiceUrl));
            }
        }

        private void ConstructAuthenticationServiceEndPoint()
        {
            string authenticationServiceBaseUrl = ConfigurationManager.AppSettings["AuthenticationServiceBaseUrl"];
            string authenticationServiceEndPoint = ConfigurationManager.AppSettings["AuthenticationServiceEndPoint"];
            if (string.IsNullOrEmpty(authenticationServiceBaseUrl) || string.IsNullOrEmpty(authenticationServiceEndPoint))
            {
                throw new ConfigurationErrorsException("Authentication service is not configured");
            }

            authenticationServiceUrl = $"{authenticationServiceBaseUrl}/{authenticationServiceEndPoint}";
        }
    }
}
