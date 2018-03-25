using MD.ProfileManagement.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.DataManager
{
    public interface ITestDataManager
    {
        Task DeleteTestAsync(int testId);        

        Task<IEnumerable<Test>> GetAllTestsAsync();        

        Task<Test> GetTestAsync(int testId);        

        Task<int> UpsertTestAsync(Test test);
        

    }
}