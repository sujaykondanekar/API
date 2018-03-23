using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MD.ProfileManagement.DataContract;

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