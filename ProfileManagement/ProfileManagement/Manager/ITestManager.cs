using MD.ProfileManagement.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITestManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Test>> GetAllTestsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        Task<Test> GetTestAsync(int testId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        Task<int> UpsertTestAsync(Test test);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="testId"></param>
       /// <returns></returns>
        Task DeleteTestAsync(int testId);
    }
}
