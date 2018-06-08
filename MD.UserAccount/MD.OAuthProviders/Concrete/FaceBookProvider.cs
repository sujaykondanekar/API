using MD.OAuthProviders.Abstract;
using MD.OAuthProviders.Models;
using System;

namespace MD.OAuthProviders.Concrete
{
    /// <summary>
    /// Facebook OAuth provider
    /// </summary>
    /// <seealso cref="MD.OAuthProviders.Abstract.IOauthProvider" />
    public class FaceBookProvider : IOauthProvider
    {
        /// <summary>
        /// Authorizes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userName">Name of the user.</param>
        public void Authorize(ProviderAndAccessToken model, out string id, out string userName)
        {
            try
            {
                var fbclient = new Facebook.FacebookClient(model.Token);
                dynamic fb = fbclient.Get("/me?locale=en_US&fields=name,email");
                id = fb.id;
                userName = fb.email;
                if (null == userName) { userName = fb.name; }
                if (null == userName) { userName = fb.id; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}