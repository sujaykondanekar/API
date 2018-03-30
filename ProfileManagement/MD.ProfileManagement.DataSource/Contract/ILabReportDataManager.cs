using MD.ProfileManagement.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.DataManager
{
    public interface ILabReportDataManager
    {
        Task DeleteReportAsync(int reportId);

        Task DeleteAllReportAsync(int profileId);

        Task<IEnumerable<SlimLabReport>> GetAllReportAsync(int profileId);

        Task<SlimLabReport> GetReportAsync(int reportId);

        Task<int> UpsertReportAsync(SlimLabReport report);

    }
}