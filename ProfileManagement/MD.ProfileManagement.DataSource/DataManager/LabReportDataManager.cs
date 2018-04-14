using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataContract.Comparer;
using MD.ProfileManagement.DataSource.DataModel;
using Newtonsoft.Json;
using MD.ProfileManagement.DataSource.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.DataManager
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
                dbContext.LabReports.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAllReportAsync(int profileId)
        {
            var result = await dbContext.LabReports.Where(report => report.ProfileID == profileId).ToListAsync();
            if (result != null)
            {
                dbContext.LabReports.RemoveRange(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LabReport>> GetAllReportAsync(int profileId)
        {
            var result = await dbContext.LabReports.Where(report => report.ProfileID == profileId).ToListAsync();
            return result.Select(lr => lr.ConvertToDomain()).ToList();
        }

        public async Task<LabReport> GetReportAsync(int reportId)
        {
            var result =  await dbContext.LabReports.Where(report => report.LabReportId == reportId).FirstOrDefaultAsync();
            if (result != null)
            {
                return result.ConvertToDomain();
            }
            return null;
        }

        public async Task<long> UpsertReportAsync(LabReport report)
        {
            MDLabReport dbLabReport;
            if (report.ReportId == null || report.ReportId==0)
            {
                dbLabReport = GetDBLabReport(report);
                dbContext.LabReports.Add(dbLabReport);
            }
            else
            {
                dbLabReport = await dbContext.LabReports.Where(rpt => rpt.LabReportId == report.ReportId).FirstOrDefaultAsync();
                if (dbLabReport == null)
                {
                    dbLabReport = GetDBLabReport(report);
                    dbContext.LabReports.Add(dbLabReport);
                }
                else
                {
                    //IEqualityComparer<SlimLabTest> comparer = new TestComparer();
                    //reportInDb.Tests.Intersect(report.Tests, comparer).ToList().ForEach(testInDb =>
                    //{
                    //    var test = report.Tests.Where(tst => tst.Id == testInDb.Id).FirstOrDefault();
                    //    testInDb.Value = test.Value;

                    //});

                    //reportInDb.Tests.Except(report.Tests, comparer).ToList().ForEach(test =>
                    //{
                    //    reportInDb.Tests.ToList().Remove(test);
                    //});

                }
            }

            await dbContext.SaveChangesAsync();
            return dbLabReport.LabReportId;
        }

        public async Task<IEnumerable<LabTestType>> GetLabTestTypesAsync()
        {
            var result = await dbContext.Attributes.Where(att => att.isDeleted == false).ToListAsync();
            return result.Select(lt => lt.ConvertToDomain()).ToList(); ;
        }

        private MDLabReport GetDBLabReport(LabReport report)
        {
            MDLabReport mdReport = report.ConvertToDbEntity();
            mdReport.InsertedDate = DateTime.Now;
            mdReport.UpdatedDate = DateTime.Now;
            mdReport.isDeleted = false;
            dbContext.LabReports.Add(mdReport);

            return mdReport;
        }
    }
}