using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MD.ProfileManagement.DataSource.DataModel
{
    [Table("LabReport_Attribute_Value")]
    public class MDLabTest
    {
        [Key]
        public int LabReportAttributeValueID { get; set; }
       
        public long ? LabReportID { get; set; }

        public int ProfileID { get; set; }

        public int AttributeID { get; set; }

        public string AttributeValue { get; set; }     

        public virtual  MDLabReport LabReport { get; set; }

    }
}
