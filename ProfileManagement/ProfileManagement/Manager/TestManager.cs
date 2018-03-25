using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.DataManager;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Manager
{
    public class TestManager : ITestManager
    {
        private ITestDataManager dataManager;
        public TestManager(ITestDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public Task DeleteTestAsync(int testId)
        {
            return dataManager.DeleteTestAsync(testId);
        }

        public Task<IEnumerable<Test>> GetAllTestsAsync()
        {
            return dataManager.GetAllTestsAsync();
        }

        public Task<Test> GetTestAsync(int testId)
        {
            return dataManager.GetTestAsync(testId);
        }

        public Task<int> UpsertTestAsync(Test test)
        {
            return dataManager.UpsertTestAsync(test);
        }
    }
}