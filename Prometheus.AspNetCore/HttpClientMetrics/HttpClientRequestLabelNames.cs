using System;

namespace Prometheus.HttpClientMetrics
{
    /// <summary>
    /// Label names reserved for the use by the HttpClient metrics.
    /// </summary>
    public static class HttpClientRequestLabelNames
    {
        public const string Method = "method";
        public const string Host = "host";
        public const string Code = "code";

        public static readonly string[] All =
        {
            Code,
            Method,
            Host
        };

        internal static readonly string[] AvailableBeforeRequest =
        {
            Method,
            Host
        };
    }
}