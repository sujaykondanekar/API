using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProfileManagement.DataContract;
using ProfileManagement.DataSource;

namespace ProfileManagement.Manager
{
    public class ProfileDataManager : IProfileDataManager
    {
        private ProfileManagementDbContext dbContext;

        public ProfileDataManager(ProfileManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteProfileAsync(int profileId)
        {
            var profile = await dbContext.Profiles.Where(p => p.Id == profileId).FirstOrDefaultAsync();
            if (profile != null)
            {
                dbContext.Profiles.Remove(profile);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync(string userId)
        {
            return await dbContext.Profiles.Where(p => p.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<Profile> GetProfileAsync(int profileId)
        {
            return await dbContext.Profiles.Where(p => p.Id == profileId).FirstOrDefaultAsync();
        }

        public async Task<int> UpsertProfileAsync(string userId, Profile profile)
        {
            if (profile.Id == null)
            {
                profile.UserId = userId;
                dbContext.Profiles.Add(profile);
                await dbContext.SaveChangesAsync();
                return profile.Id.Value;

            }
            else
            {
                return profile.Id.Value;
            }
        }
    }
}