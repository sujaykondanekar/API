using System.Collections.Generic;

namespace MD.ProfileManagement.DataContract
{
    public class LabTest
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public decimal TestValue { get; set; }
        public string Unit { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public string Category { get; set; }
    }
}