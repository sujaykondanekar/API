using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.DataModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MD.ProfileManagement.DataSource.Helper
{
    public static class ModelMapper
    {
        internal static Profile ConvertToDomain(this MDMemberProfile obj)
        {
            return new Profile()
            {
                DOB = obj.DOB,
                FirstName = obj.FirstName,
                Gender = obj.Gender,
                Height = obj.Height,
                LastName = obj.LastName,
                ProfileName = obj.ProfileName,
                Id = obj.ProfileID,
                UserId = obj.UserID.ToString(),
                ConsolidatedReport = obj.ConsolidatedReport               
            };
        }

        internal static MDMemberProfile ConvertToDbEntity(this Profile obj)
        {
            return new MDMemberProfile()
            {
                DOB = obj.DOB,
                FirstName = obj.FirstName,
                Gender = obj.Gender,
                Height = obj.Height,
                LastName = obj.LastName,
                ProfileName = obj.ProfileName,
                ProfileID = obj.Id ?? 0,
                UserID = obj.UserId,
                ConsolidatedReport = obj.ConsolidatedReport                
            };
        }


        public static LabTestType ConvertToDomain(this MDAttribute att)
        {
            return new LabTestType()
            {
                TestName = att.AttributeName,
                SearchKeyWords = new List<string>() { att.AttributeName },
                Unit = att.Unit,
                MaxValue = double.Parse(att.RefMaxValue),
                MinValue = double.Parse(att.RefMinValue),
                TestTypeId = att.AttributeID,
                TestCategory = att.AttributeGroup.AttributeGroupName
            };
        }

        public static MDLabReport ConvertToDbEntity(this LabReport report)
        {
            return new MDLabReport()
            {
                ProfileID = report.ProfileId,
                LabReportId = report.ReportId ?? 0,                
                Report = JsonConvert.SerializeObject(report.LabTests),
                ReportDate = report.ReportDate               
            };
        }

        public static MDLabTest ConvertToDbEntity(this LabTest report)
        {
            return new MDLabTest()
            {
                AttributeID = report.TestId,
                AttributeValue = report.TestValue.ToString(),
                LabReportID = report.ReportId,
                ProfileID = 15
            };
        }

        public static LabReport ConvertToDomain(this MDLabReport report)
        {
            return new LabReport()
            {
                ProfileId = report.ProfileID,
                ReportId = report.LabReportId,
                ReportName = string.Empty,
                ReportDate = report.ReportDate,
                LabTests = JsonConvert.DeserializeObject<List<LabTest>>(report.Report)

            };
        }
    }
}
