using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MD.ProfileManagement.DataSource.DataModel
{
    [Table("Attribute")]
    public class MDAttribute
    {
        [Key]
        public int AttributeID { get; set; }

        [ForeignKey("AttributeGroup")]
        public int AttributeGroupID { get; set; }

        public string AttributeName { get; set; }

        public string Unit { get; set; }

        public string RefMinValue { get; set; }

        public string RefMaxValue { get; set; }

        public bool isDeleted { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
      
        public virtual  MDAttributeGroup AttributeGroup { get; set; }

    }
}
