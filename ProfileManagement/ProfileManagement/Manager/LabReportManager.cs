using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.DataManager;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Manager
{
    public class LabReportManager : ILabReportManager
    {
        private ILabReportDataManager dataManager;
        public LabReportManager(ILabReportDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public Task DeleteReportAsync(int reportId)
        {
            return dataManager.DeleteReportAsync(reportId);
        }

        public Task DeleteAllReportAsync(int profileId)
        {
            return dataManager.DeleteAllReportAsync(profileId);
        }

        public Task<IEnumerable<SlimLabReport>> GetAllReportAsync(int profileId)
        {
            return dataManager.GetAllReportAsync(profileId);
        }

        public Task<SlimLabReport> GetReportAsync(int reportId)
        {
            return dataManager.GetReportAsync(reportId);
        }

        public Task<int> UpsertReportAsync(SlimLabReport report)
        {
            return dataManager.UpsertReportAsync(report);
        }
    }
}