using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.DataModel;
using MD.ProfileManagement.DataSource.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.DataManager
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
            var profile = await dbContext.Profiles.Where(p => p.ProfileID == profileId && p.IsDeleted == false).FirstOrDefaultAsync();
            if (profile != null)
            {
                profile.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync(string userId)
        {
            var result = await dbContext.Profiles.Where(p => p.UserID.ToString().Equals(userId, StringComparison.OrdinalIgnoreCase) && p.IsDeleted == false).ToListAsync();
            return result.Select(mp => mp.ConvertToDomain()).ToList();
        }

        public async Task<Profile> GetProfileAsync(int profileId)
        {
            var result = await dbContext.Profiles.Where(p => p.ProfileID == profileId && p.IsDeleted == false).FirstOrDefaultAsync();
            if (result != null)
            {
                return result.ConvertToDomain();
            }

            return null;
        }

        public async Task<int> UpsertProfileAsync(string userId, Profile profile)
        {
            profile.UserId = userId;
            MDMemberProfile dbProfile = profile.ConvertToDbEntity();
            dbProfile.UpdatedDate = DateTime.Now;

            if (profile.Id == null)
            {
                dbProfile.InsertedDate = DateTime.Now;
                dbProfile.UpdatedDate = DateTime.Now;
                dbProfile.IsDeleted = false;
                dbContext.Profiles.Add(dbProfile);
            }
            else
            {
                var result = await dbContext.Profiles.Where(p => p.ProfileID == profile.Id).FirstOrDefaultAsync();
                if (result == null)
                {
                    dbProfile.InsertedDate = DateTime.Now;
                    dbProfile.IsDeleted = false;
                    dbContext.Profiles.Add(dbProfile);
                }
                else
                {
                    UpdateProfileValues(result, profile);
                }

            }

            await dbContext.SaveChangesAsync();
            profile.Id = dbProfile.ProfileID;
            return profile.Id.Value;
        }

        private void UpdateProfileValues(MDMemberProfile profileInDB, Profile profile)
        {
            profileInDB.FirstName = profile.FirstName;
            profileInDB.LastName = profile.LastName;
            profileInDB.DOB = profile.DOB;
            profileInDB.Gender = profile.Gender;
            profileInDB.Height = profile.Height;
            profile.ProfileName = profile.ProfileName;
            profile.Weight = profile.Weight;
        }
    }
}