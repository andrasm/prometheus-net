using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
namespace Prometheus.HttpClientMetrics
{
    internal sealed class HttpClientRequestCountHandler : HttpClientDelegatingHandlerBase<ICollector<ICounter>, ICounter>
    {
        public HttpClientRequestCountHandler(HttpClientRequestCountOptions? options)
            : base(options, options?.Counter)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage? response;
            try
            {
                response = await base.SendAsync(request, cancellationToken);
            }
            catch
            {
                CreateChild(request).Inc();
                throw;
            }
            CreateChild(request, response).Inc();
            return response;
        }

        protected override ICollector<ICounter> CreateMetricInstance(string[] labelNames) => MetricFactory.CreateCounter(
            "httpclient_requests_received_total",
            "Count of HTTP requests that have been started by an HttpClient.",
            new CounterConfiguration
            {
                LabelNames = labelNames
            });
    }
}