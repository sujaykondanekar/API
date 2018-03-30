using log4net;
using MD.Authorization;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace MD.Common.Handlers
{
    public class TokenAuthorizationHandler : DelegatingHandler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token;
            string userId = string.Empty;

            token = request.Headers.GetValues("Authorization").FirstOrDefault();
            if (token == null)
            {
                return UnauthorizeResponse();
            }
            else
            {
                try
                {
                    ITokenAuthorizationService tokenAuthorizationService = new APITokenAuthorizationService();
                    string data = await tokenAuthorizationService.GetUserNameAsync(token);
                    if (string.IsNullOrEmpty(data))
                    {
                        return UnauthorizeResponse();
                    }
                    userId = GetUserIdFromResponse(data);
                }

                catch (HttpRequestException ex)
                {
                    if (ex.Message.Contains("401"))
                    {
                        return UnauthorizeResponse();
                    }
                    return InternalServerResponse(ex);
                }
                catch (Exception ex)
                {
                    return InternalServerResponse(ex);

                }
            }
            request.Headers.Add("UserName", userId);
            return await base.SendAsync(request, cancellationToken);

        }

        private string GetUserIdFromResponse(string data)
        {
            JObject jobject = JObject.Parse(data);
            return jobject.Value<string>("UserId");


        }

        private HttpResponseMessage InternalServerResponse(Exception ex)
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

        private HttpResponseMessage UnauthorizeResponse()
        {
            return new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent("You are not authorized to access the resource.")
            };
        }
    }
}
