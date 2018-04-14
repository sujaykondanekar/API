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

        public Task<IEnumerable<LabReport>> GetAllReportAsync(int profileId)
        {
            return dataManager.GetAllReportAsync(profileId);
        }

        public Task<LabReport> GetReportAsync(int reportId)
        {
            return dataManager.GetReportAsync(reportId);
        }

        public Task<long> UpsertReportAsync(LabReport report)
        {
            return dataManager.UpsertReportAsync(report);
        }

        public Task<IEnumerable<LabTestType>> GetLabTestTypesAsync()
        {
            return dataManager.GetLabTestTypesAsync();
        }
    }
}