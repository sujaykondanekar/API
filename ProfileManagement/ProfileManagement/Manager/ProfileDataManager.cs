using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProfileManagement.DataContract;

namespace ProfileManagement.Manager
{
    public class ProfileDataManager : IProfileDataManager
    {
        public Task DeleteProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Profile>> GetAllProfilesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Profile> GetProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task UpsertProfileAsync(string userId, Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}