using MD.OAuthProviders.Abstract;
using MD.OAuthProviders.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MD.OAuthProviders.Concrete
{
    /// <summary>
    /// Facebook OAuth provider
    /// </summary>
    /// <seealso cref="MD.OAuthProviders.Abstract.IOauthProvider" />
    public class GoogleProvider : IOauthProvider
    {
        /// <summary>
        /// Authorizes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userName">Name of the user.</param>
        public void Authorize(ProviderAndAccessToken model, out string id, out string userName)
        {
            id = userName = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //api to get the data from google.
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                       model.Token);
                    var response = client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo?fields=email%2Cid%2Cname").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic userInfo = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        id = userInfo.id;
                        userName = userInfo.email;
                        if (null == userName)
                        { userName = userInfo.name; }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}