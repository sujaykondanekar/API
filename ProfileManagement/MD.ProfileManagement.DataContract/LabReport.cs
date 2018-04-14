using System;
using System.Collections.Generic;

namespace MD.ProfileManagement.DataContract
{
    public class LabReport
    {
        public int ProfileId { get; set; }
        public long? ReportId { get; set; }
        public string ReportName { get; set; }
        public DateTime ReportDate { get; set; }
        public IEnumerable<LabTest> LabTests { get; set; }
    }
}
