using MD.ProfileManagement.Context;
using MD.ProfileManagement.Filters;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MD.ProfileManagement.Controllers
{
    [Authorize]
    [UserContextFilter]
    public class BaseController : ApiController
    {
        public UserContext UserContext;      
            
    }
}
