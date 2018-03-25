using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.Manager;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace MD.ProfileManagement.Controllers
{
    public class TestController : BaseController
    {
        private ITestManager testManager;
        public TestController(ITestManager testManager)
        {
            this.testManager = testManager;
        }

        // GET: api/Profile
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await testManager.GetAllTestsAsync();
            if (result != null && result.Any())
            {
                return Ok(result);
            }

            return NotFound();
        }

        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> GetAsyncById(int testId)
        {
            var result = await testManager.GetTestAsync(testId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();

        }

        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> UpsertAsyn(Test test)
        {
            await testManager.UpsertTestAsync(test);
            return Ok();
        }

        // DELETE: api/Profile/5
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteAsync(int testId)
        {
            await testManager.DeleteTestAsync(testId);
            return Ok();
        }
    }
}
