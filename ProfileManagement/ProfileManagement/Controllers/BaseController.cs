using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProfileManagement.Controllers
{
    public class BaseController : ApiController
    {
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
