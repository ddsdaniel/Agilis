using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Agilis.Test.Integration.WebAPI.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostViewModelAsync<TViewModel>(this HttpClient source, string requestUri, TViewModel viewModel)
            where TViewModel : class
        {
            StringContent stringContent = new StringContent(
                    JsonConvert.SerializeObject(viewModel),
                    Encoding.UTF8,
                    "application/json"
                    );

            EnsureContentLength(stringContent);

            return source.PostAsync(requestUri, stringContent);
        }

        private static void EnsureContentLength(HttpContent httpContent)
        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            // required due to https://github.com/dotnet/aspnetcore/issues/18463
            var contentLenth = httpContent.Headers.ContentLength;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }
    }
}
