using System.Threading.Tasks;

namespace MD.Authorization
{
    public interface ITokenAuthorizationService
    {
        Task<string> GetUserNameAsync(string token);

        bool TryGetUserNameAsync(string token, out string userName);

        Task<bool> ValidateTokenAsync(string token);

    }
}
