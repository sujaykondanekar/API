using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Manager
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
            }
            else
            {
                Profile profileInDB = await dbContext.Profiles.Where(p => p.Id == profile.Id).FirstOrDefaultAsync();
                if(profileInDB == null)
                {
                    dbContext.Profiles.Add(profile);
                }
                else
                {
                    UpdateProfileValues(profileInDB, profile);
                }
                
            }

            await dbContext.SaveChangesAsync();
            return profile.Id.Value;
        }

        private void UpdateProfileValues(Profile profileInDB, Profile profile)
        {
            profileInDB.FirstName = profile.FirstName;
            profileInDB.LastName = profile.LastName;
            profileInDB.Age = profile.Age;
            profileInDB.Gender = profile.Gender;
            profileInDB.Height = profile.Height;
            profileInDB.Report = UpdateJsonReport(profileInDB.Report, profile.Report);
        }

        private string UpdateJsonReport(string reportInDB, string newReport)
        {
            return newReport;
        }
    }
}