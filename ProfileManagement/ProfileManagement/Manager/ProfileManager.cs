using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProfileManagement.DataContract;

namespace ProfileManagement.Manager
{
    public class ProfileManager : IProfileManager
    {
        private IProfileDataManager dataManager;
        public ProfileManager(IProfileDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public Task DeleteProfileAsync(string profileId)
        {
            return dataManager.DeleteProfileAsync(profileId);
        }

        public Task<IEnumerable<Profile>> GetAllProfilesAsync(string userId)
        {
            return dataManager.GetAllProfilesAsync(userId);
        }

        public Task<Profile> GetProfileAsync(string profileId)
        {
            return dataManager.GetProfileAsync(profileId);
        }

        public Task UpsertProfileAsync(string userId, Profile profile)
        {
            return dataManager.UpsertProfileAsync(userId, profile);
        }
    }
}