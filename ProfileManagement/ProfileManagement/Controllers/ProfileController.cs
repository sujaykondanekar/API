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
            var result = await profileManager.GetAllProfilesAsync(ExtractUserNameFromHeader());
            if (result != null && result.Any())
            {
                return Ok(result);
            }

            return NotFound();
        }

        // GET: api/Profile/5
        public async Task<IHttpActionResult> GetAsync(int profileId)
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
            await profileManager.UpsertProfileAsync(ExtractUserNameFromHeader(), profile);
            return Ok();
        }
        

        // DELETE: api/Profile/5
        public async Task<IHttpActionResult> DeleteAsync(int profileId)
        {
            await profileManager.DeleteProfileAsync(profileId);
            return Ok();
        }
    }
}
