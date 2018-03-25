using MD.ProfileManagement.DataContract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.DataManager
{
    public class TestDataManager : ITestDataManager
    {
        private ProfileManagementDbContext dbContext;

        public TestDataManager(ProfileManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteTestAsync(int testId)
        {
            var Test = await dbContext.Tests.Where(t => t.Id == testId).FirstOrDefaultAsync();
            if (Test != null)
            {
                dbContext.Tests.Remove(Test);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Test>> GetAllTestsAsync()
        {
            return await dbContext.Tests.ToListAsync();
        }

        public async Task<Test> GetTestAsync(int testId)
        {
            return await dbContext.Tests.Where(p => p.Id == testId).FirstOrDefaultAsync();
        }

        public async Task<int> UpsertTestAsync(Test test)
        {
            if (test.Id == null)
            {
                dbContext.Tests.Add(test);
            }
            else
            {
                Test TestInDB = await dbContext.Tests.Where(p => p.Id == test.Id).FirstOrDefaultAsync();
                if(TestInDB == null)
                {
                    dbContext.Tests.Add(test);
                }
                else
                {
                    UpdateTestValues(TestInDB, test);
                }
                
            }

            await dbContext.SaveChangesAsync();
            return test.Id.Value;
        }

        private void UpdateTestValues(Test TestInDB, Test test)
        {
            TestInDB.Name = test.Name;
            TestInDB.References = test.References;
            TestInDB.MaximumValueForMale = test.MaximumValueForMale;
            TestInDB.MinimumValueForMale = test.MinimumValueForMale;
            TestInDB.MaximumValueForFemale = test.MaximumValueForFemale;
            TestInDB.MinimumValueForFemale = test.MinimumValueForFemale;
            TestInDB.Category = test.Category;
            TestInDB.SubCategory = test.SubCategory;
        }
    }
}