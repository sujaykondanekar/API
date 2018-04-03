using MD.ProfileManagement.Controllers;
using Microsoft.AspNet.Identity;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MD.ProfileManagement.Filters
{
    public class UserContextFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            BaseController controller = (BaseController)actionContext.ControllerContext.Controller;
            controller.UserContext = new Context.UserContext()
            {
                Email = controller.User.Identity.GetUserName(),
                UserId = controller.User.Identity.GetUserId(),
                UserName = controller.User.Identity.GetUserName()
            };
        }

    }
}