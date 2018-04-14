using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MD.ProfileManagement.DataSource.DataModel
{
    [Table("AttributeGroup")]
    public class MDAttributeGroup
    {
        [Key]
        public int AttributeGroupID { get; set; }

        public string AttributeGroupName { get; set; }

        public int ? ParentGroupID { get; set; }

    }
}
