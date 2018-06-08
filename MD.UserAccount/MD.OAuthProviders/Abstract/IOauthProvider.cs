using MD.OAuthProviders.Models;

namespace MD.OAuthProviders.Abstract
{
    /// <summary>
    /// OAuth provider
    /// </summary>
    public interface IOauthProvider
    {
        /// <summary>
        /// Authorizes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userName">Name of the user.</param>
        void Authorize(ProviderAndAccessToken model, out string id, out string userName);
    }
}