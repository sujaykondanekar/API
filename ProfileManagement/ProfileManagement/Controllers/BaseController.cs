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
        protected string GetUserFromRequest()
        {           
            IEnumerable<string> headerValues = null;
            if(Request.Headers.TryGetValues("UserName", out headerValues))
            {
                return headerValues.First();
            }
            var message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("User name missing in header")
            };
            throw new HttpResponseException(message);
        }       
    }
}
