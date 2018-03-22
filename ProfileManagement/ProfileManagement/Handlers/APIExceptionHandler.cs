using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace MD.ProfileManagement.Handlers
{
    public class APIExceptionHandler: ExceptionHandler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void Handle(ExceptionHandlerContext context)
        {
            try
            {
                log.Error("Exception.", context.Exception);
            }
            catch
            {
                //do nothing
            }
            context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError,
            "Unable to process the request currently. Please try again later."));
         }
    }
}