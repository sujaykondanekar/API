using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Manager
{
    public class ProfileManager : IProfileManager
    {
        private IProfileDataManager dataManager;
        public ProfileManager(IProfileDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public Task DeleteProfileAsync(int profileId)
        {
            return dataManager.DeleteProfileAsync(profileId);
        }

        public Task<IEnumerable<Profile>> GetAllProfilesAsync(string userId)
        {
            return dataManager.GetAllProfilesAsync(userId);
        }

        public Task<Profile> GetProfileAsync(int profileId)
        {
            return dataManager.GetProfileAsync(profileId);
        }

        public Task<int> UpsertProfileAsync(string userId, Profile profile)
        {
            return dataManager.UpsertProfileAsync(userId, profile);
        }
    }
}