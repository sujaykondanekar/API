using log4net;
using MD.Authorization;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Handlers
{
    public class TokenAuthorizationHandler : DelegatingHandler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token;
            try
            {
                token = request.Headers.GetValues("Authorization").FirstOrDefault();
                if (token == null)
                {
                    //Code for authenticate user. We got authentication ticket
                    return await Task.Factory.StartNew(() =>
                    {
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent("You are not authorized to access the resource")
                        };
                    });
                }
                else
                {
                    ITokenAuthorizationService tokenAuthorizationService = new APITokenAuthorizationService();
                    string userName = await tokenAuthorizationService.GetUserNameAsync(token);
                    if (string.IsNullOrEmpty(userName))
                    {
                        return new HttpResponseMessage(HttpStatusCode.Forbidden)
                        {
                            Content = new StringContent("Either session is expired or you are not authorized to access the resource")
                        };
                    }
                    request.Headers.Add("UserName", userName);
                    return await base.SendAsync(request, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    log.Error("Error occured while authenticating the request.", ex);
                }
                catch
                {
                    //do nothing  
                }
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Unable to authenticate currently. Please try after some time.")
                };

            }
        }
    }
}
