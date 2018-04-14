using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.DataSource.DataModel;
using System.Collections.Generic;

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

        internal static MemberProfile ConvertToDomain(this Profile obj)
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
                UserID = obj.UserId               
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
       
    }
}
