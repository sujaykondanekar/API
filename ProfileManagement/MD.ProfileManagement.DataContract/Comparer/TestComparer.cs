using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataContract.Comparer
{
    public class TestComparer : IEqualityComparer<SlimLabTest>
    {
        public bool Equals(SlimLabTest testA, SlimLabTest testB)
        {
            return testA.Id == testB.Id;
        }

        public int GetHashCode(SlimLabTest obj)
        {
            return obj.GetHashCode();
        }
    }
}
