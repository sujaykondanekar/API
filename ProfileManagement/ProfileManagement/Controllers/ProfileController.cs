using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.Manager;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace MD.ProfileManagement.Controllers
{
    public class ProfileController : BaseController
    {
        private IProfileManager profileManager;
        public ProfileController(IProfileManager profileManager)
        {
            this.profileManager = profileManager;
        }

        [Route("profile")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await profileManager.GetAllProfilesAsync(UserContext.UserId);
            return Ok(result);
        }

        [Route("profile/{profileId}")]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetAsyncById(int profileId)
        {
            var result = await profileManager.GetProfileAsync(profileId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();

        }

        [Route("profile")]
        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> UpsertAsyn(Profile profile)
        {
            int profileId = profile.Id ?? 0;
            int updatedProfileId = await profileManager.UpsertProfileAsync(UserContext.UserId, profile);
            if (profileId != updatedProfileId)
            {
                return Created(new Uri($"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/profile/{updatedProfileId}"), updatedProfileId);
            }
            return Ok(profile.Id);
        }

        [Route("profile/{profileId}")]
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteAsync(int profileId)
        {
            await profileManager.DeleteProfileAsync(profileId);
            return Ok("Deleted");
        }
    }
}
