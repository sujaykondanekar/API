using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.Contract;
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

        public Task DeleteReportsAsync(int profileId)
        {
            return dataManager.DeleteReportsAsync(profileId);
        }

        public Task<IEnumerable<LabReport>> GetReportsAsync(int profileId)
        {
            return dataManager.GetReportsAsync(profileId);
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