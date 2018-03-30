using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataContract
{
    public class LabReport
    {
        public int? ProfileId { get; set; }
        public int? Id { get; set; }
       public IEnumerable<LabTest> Tests { get; set; }
    }
}
