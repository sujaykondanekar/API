using System.Collections.Generic;

namespace MD.ProfileManagement.DataContract
{
    public class LabTestType
    {
        public int TestTypeId { get; set; }
        public string TestName { get; set; }
        public string TestCategory { get; set; }
        public IEnumerable<string> SearchKeyWords { get; set; }
        public string Unit { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
