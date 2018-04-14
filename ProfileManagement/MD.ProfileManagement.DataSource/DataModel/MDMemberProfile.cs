using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.ProfileManagement.DataSource.DataModel
{
    [Table("MemberProfile")]
    public class MDMemberProfile
    {
        [Key]
        public  int ProfileID { get; set; }
        public string ProfileName { get; set; }
        public string UserID { get; set; }
        public bool SendMedicineReminder { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime? DOB { get; set; }
    }
}
