using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.DataModel;
using MD.ProfileManagement.DataSource.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.Contract
{
    public class LabReportDataManager : ILabReportDataManager
    {
        private ProfileManagementDbContext dbContext;

        public LabReportDataManager(ProfileManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteReportAsync(int reportId)
        {
            var result = await dbContext.LabReports.Where(report => report.LabReportId == reportId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.isDeleted = true;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteReportsAsync(int profileId)
        {
            var result = dbContext.LabReports.Where(report => report.ProfileID == profileId).ToList();
            if (result != null)
            {
                result.ForEach(labReport => labReport.isDeleted = true);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LabReport>> GetReportsAsync(int profileId)
        {
            var result = await dbContext.LabReports.Where(report => report.ProfileID == profileId).ToListAsync();
            return result.Select(lr => lr.ConvertToDomain()).ToList();
        }

        public async Task<LabReport> GetReportAsync(int reportId)
        {
            var result = await dbContext.LabReports.Where(report => report.LabReportId == reportId && !report.isDeleted).FirstOrDefaultAsync();
            if (result != null)
            {
                return result.ConvertToDomain();
            }
            return null;
        }

        public async Task<long> UpsertReportAsync(LabReport report)
        {

            MDLabReport dbLabReport = await dbContext.LabReports.Where(rpt => rpt.LabReportId == report.ReportId).FirstOrDefaultAsync();
            if (dbLabReport == null)
            {
                dbLabReport = GetDBLabReport(report);
                dbContext.MemberProfiles.Where(mp => mp.ProfileID == dbLabReport.ProfileID).FirstOrDefault().ConsolidatedReport = dbLabReport.Report;
                dbContext.LabReports.Add(dbLabReport);
            }
            else
            {
                dbLabReport.Report = JsonConvert.SerializeObject(report.LabTests);
               dbLabReport.MemberProfile.ConsolidatedReport = dbLabReport.Report;

            }

            await dbContext.SaveChangesAsync();
            return dbLabReport.LabReportId;
        }

        public async Task<IEnumerable<LabTestType>> GetLabTestTypesAsync()
        {
            var result = await dbContext.Attributes.Where(att => att.isDeleted == false).ToListAsync();
            return result.Select(lt => lt.ConvertToDomain()).ToList(); ;
        }

        internal MDLabReport GetDBLabReport(LabReport report)
        {
            MDLabReport mdReport = report.ConvertToDbEntity();
            mdReport.InsertedDate = DateTime.Now;
            mdReport.UpdatedDate = DateTime.Now;
            mdReport.isDeleted = false;
            return mdReport;
        }
    }
}