using MD.ProfileManagement.Context;
using MD.ProfileManagement.Filters;
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
