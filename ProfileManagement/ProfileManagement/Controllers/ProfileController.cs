using ProfileManagement.DataContract;
using ProfileManagement.Manager;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProfileManagement.Controllers
{
    public class ProfileController : BaseController
    {
        private IProfileManager profileManager;
        public ProfileController(IProfileManager profileManager)
        {
            this.profileManager = profileManager;
        }

        // GET: api/Profile
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await profileManager.GetAllProfilesAsync(GetUserFromRequest());
            if (result != null && result.Any())
            {
                return Ok(result);
            }

            return NotFound();
        }

        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetAsyncById(string profileId)
        {
            var result = await profileManager.GetProfileAsync(profileId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();

        }

        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> UpsertAsyn(Profile profile)
        {
            await profileManager.UpsertProfileAsync(GetUserFromRequest(), profile);
            return Ok();
        }


        // DELETE: api/Profile/5
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteAsync(string profileId)
        {
            await profileManager.DeleteProfileAsync(profileId);
            return Ok();
        }
    }
}
