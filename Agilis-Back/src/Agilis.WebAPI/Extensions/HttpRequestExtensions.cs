using Microsoft.AspNetCore.Http;

namespace Agilis.WebAPI.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetHostUrl(this HttpRequest httpRequest)
        {
            var hostUrl = $"{httpRequest.Scheme}://{httpRequest.Host.ToUriComponent()}";
            return hostUrl;
        }

        public static string GetFrontUrl(this HttpRequest httpRequest)
        {
            var referer = httpRequest.Headers["Referer"].ToString();
            return referer;
        }
    }
}
