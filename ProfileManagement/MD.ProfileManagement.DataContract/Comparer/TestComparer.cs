using System.Collections.Generic;

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
