using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfileManagement.DataContract
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
    }
}