using log4net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MD.ProfileManagement.Handlers
{
    public class RequestLoggingHandler : DelegatingHandler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await LogRequest(request);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task LogRequest(HttpRequestMessage request)
        {
            var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);
            var requestMessage = Encoding.UTF8.GetString(await request.Content.ReadAsByteArrayAsync());

            await Task.Run(() =>
            {
                try
                {
                    log.InfoFormat("Request: {0}\r\n {1}", requestInfo, requestMessage);
                }
                catch
                {
                    // do nothing
                }
            });
        }
    }
}