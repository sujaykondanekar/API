﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MD.ProfileManagement.DataSource.DataModel
{
    [Table("LabReport")]
    public class MDLabReport
    {
        [Key]
        public long LabReportId { get; set; }
      
        public int ProfileID { get; set; }

        public DateTime ReportDate { get; set; }

        public string Report { get; set; }

        public bool isDeleted { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime UpdatedDate { get; set; }  

        public virtual  MDMemberProfile MemberProfile { get; set; }

    }
}
