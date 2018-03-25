using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web.Http;

namespace MD.ProfileManagement
{
    public class APIRouteConfig
    {

        /// <summary>
        /// Name of the route used to access the <see cref="Profile"/> resources of logged in user.
        /// </summary>
        private const string Profile = "Profile";

        /// <summary>
        /// Name of the route used to access the <see cref="Profile"/> resource by identifier.
        /// </summary>
        /// <remarks>
        /// The {profileId} parameter is used to specify identifier of the <see cref="Profile"/> resource.
        /// </remarks>
        private const string ProfileById = "ProfileById";

        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();            
            AddRoutesToConfiguration(config.Routes);
        }

        private static void AddRoutesToConfiguration(HttpRouteCollection routeCollection)
        {
            routeCollection.MapHttpRoute(ProfileById, RouteMappings[ProfileById], new { controller = "Profile", }, null);
            routeCollection.MapHttpRoute(Profile, RouteMappings[Profile], new { controller = "Profile" });
        }

        /// <summary>
        /// The routes for this application, defined as a mapping from the controller name to the route path.
        /// </summary>
        private static readonly IDictionary<string, string> RouteMappings = new ConcurrentDictionary<string, string>(
            new Dictionary<string, string>
                {
                 { ProfileById, "profile/{profileId}" },
                    { Profile, "profile" }
            });
    }
}
