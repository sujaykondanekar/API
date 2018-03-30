using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataContract.Comparer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var result = await dbContext.LabReports.Where(report => report.Id == reportId).FirstOrDefaultAsync();
            if (result != null)
            {
                dbContext.LabReports.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAllReportAsync(int profileId)
        {
            var result = await dbContext.LabReports.Where(report => report.ProfileId == profileId).ToListAsync();
            if (result != null)
            {
                dbContext.LabReports.RemoveRange(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SlimLabReport>> GetAllReportAsync(int profileId)
        {
            return await dbContext.LabReports.Where(report=> report.ProfileId == profileId).ToListAsync();
        }

        public async Task<SlimLabReport> GetReportAsync(int reportId)
        {
            return await dbContext.LabReports.Where(report => report.Id == reportId).FirstOrDefaultAsync();
        }     

        public async Task<int> UpsertReportAsync(SlimLabReport report)
        {
            if (report.Id == null)
            {
                dbContext.LabReports.Add(report);
            }
            else
            {
                SlimLabReport reportInDb = await dbContext.LabReports.Where(rpt => rpt.Id == report.Id).FirstOrDefaultAsync();
                if(reportInDb == null)
                {
                    dbContext.LabReports.Add(report);
                }
                else
                {
                    IEqualityComparer<SlimLabTest> comparer = new TestComparer();
                    reportInDb.Tests.Intersect(report.Tests, comparer).ToList().ForEach(testInDb =>
                    {
                        var test = report.Tests.Where(tst => tst.Id == testInDb.Id).FirstOrDefault();
                        testInDb.Value = test.Value;

                    });

                    reportInDb.Tests.Except(report.Tests, comparer).ToList().ForEach(test=>
                    {
                    reportInDb.Tests.ToList().Remove(test);
                });
                   
                }
            }

            await dbContext.SaveChangesAsync();
            return report.Id.Value;
        }
    }
}