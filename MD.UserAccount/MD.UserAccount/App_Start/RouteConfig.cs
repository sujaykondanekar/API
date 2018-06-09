using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web.Http;

namespace MD.UserAccount
{
    public class RouteConfig
    {
        /// <summary>
        /// Name of the controller
        /// </summary>
        private const string Account = "Account";

        /// <summary>
        /// Name of the route used to access the <see cref="UserInfo"/> resources.
        /// </summary>
        private const string UserInfo = "UserInfo";

        /// <summary>
        /// Name of the route used to access the <see cref="Logout"/> resources.
        /// </summary>
        private const string Logout = "Logout";

        /// <summary>
        /// Name of the route used to access the <see cref="ManageInfo"/> resources.
        /// </summary>
        private const string ManageInfo = "ManageInfo";

        /// <summary>
        /// Name of the route used to access the <see cref="ChangePassword"/> resources.
        /// </summary>
        private const string ChangePassword = "ChangePassword";

        /// <summary>
        /// Name of the route used to access the <see cref="SetPassword"/> resources.
        /// </summary>
        private const string SetPassword = "SetPassword";

        /// <summary>
        /// Name of the route used to access the <see cref="RemoveLogin"/> resources.
        /// </summary>
        private const string RemoveLogin = "RemoveLogin";

        /// <summary>
        /// Name of the route used to access the <see cref="ExternalLogin"/> resources.
        /// </summary>
        private const string ExternalLogin = "ExternalLogin";

        /// <summary>
        /// Name of the route used to access the <see cref="AddExternalLogin"/> resources.
        /// </summary>
        private const string AddExternalLogin = "AddExternalLogin";

        /// <summary>
        /// Name of the route used to access the <see cref="Register"/> resources.
        /// </summary>
        private const string Register = "Register";

        /// <summary>
        /// Name of the route used to access the <see cref="RegisterExternal"/> resources.
        /// </summary>
        private const string RegisterExternal = "RegisterExternal";

        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //AddRoutesToConfiguration(config.Routes);
            //     config.Routes.MapHttpRoute(
            //name: "DefaultApi",
            //routeTemplate: "api/{controller}/{action}"
            //);
        }

        private static void AddRoutesToConfiguration(HttpRouteCollection routeCollection)
        {
            routeCollection.MapHttpRoute(UserInfo, RouteMappings[UserInfo], new { controller = Account }, null);
            routeCollection.MapHttpRoute(Logout, RouteMappings[Logout], new { controller = Account });
            routeCollection.MapHttpRoute(ManageInfo, RouteMappings[ManageInfo], new { controller = Account });
            routeCollection.MapHttpRoute(ChangePassword, RouteMappings[ChangePassword], new { controller = Account });
            routeCollection.MapHttpRoute(SetPassword, RouteMappings[SetPassword], new { controller = Account });
            routeCollection.MapHttpRoute(RemoveLogin, RouteMappings[RemoveLogin], new { controller = Account });
            routeCollection.MapHttpRoute(ExternalLogin, RouteMappings[ExternalLogin], new { controller = Account });
            routeCollection.MapHttpRoute(AddExternalLogin, RouteMappings[AddExternalLogin], new { controller = Account });
            routeCollection.MapHttpRoute(Register, RouteMappings[Register], new { controller = Account });
            routeCollection.MapHttpRoute(RegisterExternal, RouteMappings[RegisterExternal], new { controller = Account });
        }

        /// <summary>
        /// The routes for this application, defined as a mapping from the controller name to the route path.
        /// </summary>
        private static readonly IDictionary<string, string> RouteMappings = new ConcurrentDictionary<string, string>(
            new Dictionary<string, string>
            {
                { UserInfo, "account/userInfo" },
                { Logout, "account/logout" },
                { ManageInfo, "account/manageInfo" },
                { ChangePassword, "account/changePassword" },
                { SetPassword, "account/setPassword" },
                { RemoveLogin, "account/removeLogin" },
                { ExternalLogin, "account/externalLogin" },
                { AddExternalLogin, "account/sddExternalLogin" },
                { Register, "account/register" },
                { RegisterExternal, "account/registerExternal" },
            });
    }
}