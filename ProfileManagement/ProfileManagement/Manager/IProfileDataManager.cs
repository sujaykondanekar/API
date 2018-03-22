using MD.ProfileManagement.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProfileDataManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Profile>> GetAllProfilesAsync(string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        Task<Profile> GetProfileAsync(int  profileId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileId"></param>
        Task<int> UpsertProfileAsync(string userId, Profile profile);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileId"></param>
        Task DeleteProfileAsync(int  profileId);
    }
}
