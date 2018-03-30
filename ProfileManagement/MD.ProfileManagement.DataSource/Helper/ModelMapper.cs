using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.Helper
{
    public static class ModelMapper
    {
        internal static Profile ConvertToDomain(this MemberProfile obj)
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
                UserId = obj.UserID.ToString()
            };
        }

        internal static MemberProfile ConvertToData(this Profile obj)
        {
            return new MemberProfile()
            {
                DOB = obj.DOB,
                FirstName = obj.FirstName,
                Gender = obj.Gender,
                Height = obj.Height,
                LastName = obj.LastName,
                ProfileName = obj.ProfileName,
                ProfileID = obj.Id ?? 0,
                UserID = Convert.ToInt32(obj.UserId)
            };
        }
    }
}
