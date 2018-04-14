using MD.ProfileManagement.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILabReportManager
    {
        Task DeleteReportAsync(int reportId);

        Task DeleteAllReportAsync(int profileId);

        Task<IEnumerable<LabReport>> GetAllReportAsync(int profileId);

        Task<LabReport> GetReportAsync(int reportId);

        Task<long> UpsertReportAsync(LabReport report);

        Task<IEnumerable<LabTestType>> GetLabTestTypesAsync();
    }
}
