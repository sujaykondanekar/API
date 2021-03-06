﻿using MD.ProfileManagement.DataContract;
using MD.ProfileManagement.Manager;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace MD.ProfileManagement.Controllers
{
    public class LabReportController : BaseController
    {
        private LabReportManager manager;
        public LabReportController(LabReportManager manager)
        {
            this.manager = manager;
        }

        [Route("profile/{profileId}/labreports")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(int profileId)
        {
            var result = await manager.GetReportsAsync(profileId);
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
        [Route("labreport/{reportId}")]
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

        [Route("labreport/{reportId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(int reportId)
        {
            await manager.DeleteReportAsync(reportId);
            return Ok();
        }
        [Route("labreport")]
        [AcceptVerbs("POST", "PUT")]
        public async Task<IHttpActionResult> UpsertAsyn(LabReport report)
        {
            long updatedReportId = await manager.UpsertReportAsync(report);

            if (report.ReportId != updatedReportId)
            {
                return Created(new Uri($"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/labreport/{updatedReportId}"), updatedReportId);
            }
            return Ok(report.ReportId);
        }

        [Route("labreport/alltypes")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLabTestTypes()
        {
            var result = await manager.GetLabTestTypesAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
