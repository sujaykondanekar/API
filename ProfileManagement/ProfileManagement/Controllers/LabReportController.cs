using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.Manager;
using System.Threading.Tasks;
using System.Web.Http;

namespace MD.ProfileManagement.Controllers
{

    public class LabReportController : ApiController
    {
        private LabReportManager manager;
        public LabReportController(LabReportManager manager)
        {
            this.manager = manager;
        }
      
        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(int profileId)
        {
            var result = await manager.GetAllReportAsync(profileId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        //[HttpGet]
        //public async Task<LabReport> GetAsync(int profileId)
        //{
    
        //}

        [HttpGet]
        public async Task<IHttpActionResult> GetAsyncById(int reportId)
        {
            var result = await manager.GetReportAsync(reportId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

       [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(int reportId)
        {
            await manager.DeleteReportAsync(reportId);
            return Ok();
        } 

        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> UpsertAsyn(SlimLabReport report)
        {
            await manager.UpsertReportAsync(report);
            return Ok();
        }
    }
}
