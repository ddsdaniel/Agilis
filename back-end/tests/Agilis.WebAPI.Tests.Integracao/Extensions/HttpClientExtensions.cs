using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Agilis.WebAPI.Tests.Integracao.Extensions
{
    public static class HttpClientExtensions
    {
        private static HttpContent CreateHttpContent<T>(T contentObject)
        {
            var jsonString = JsonConvert.SerializeObject(contentObject);
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            httpContent.Headers.Add(@"Content-Length", jsonString.Length.ToString());
            return httpContent;
        }

        /// <summary>
        /// Envia um POST para a URL especificada
        /// </summary>
        /// <typeparam name="TBody">Tipo de objeto passado para o body</typeparam>
        /// <param name="requestUri">URL em que o POST será executado</param>
        /// <param name="bodyObject">Corpo da requisição</param>
        public static Task<HttpResponseMessage> PostAsync<TBody>(this HttpClient httpClient, string requestUri, TBody bodyObject)
        {
            HttpContent content = CreateHttpContent(bodyObject);
            return httpClient.PostAsync(requestUri, content);
        }

        /// <summary>
        /// Envia um PUT para a URL especificada
        /// </summary>
        /// <typeparam name="TBody">Tipo de objeto passado para o body</typeparam>
        /// <param name="requestUri">URL em que o PUT será executado</param>
        /// <param name="bodyObject">Corpo da requisição</param>
        public static Task<HttpResponseMessage> PutAsync<TBody>(this HttpClient httpClient, string requestUri, TBody bodyObject)
        {
            HttpContent content = CreateHttpContent(bodyObject);
            return httpClient.PutAsync(requestUri, content);
        }

        /// <summary>
        /// Envia um PATCH para a URL especificada
        /// </summary>
        /// <typeparam name="TBody">Tipo de objeto passado para o body</typeparam>
        /// <param name="requestUri">URL em que o PATCH será executado</param>
        /// <param name="bodyObject">Corpo da requisição</param>
        public static Task<HttpResponseMessage> PatchAsync<TBody>(this HttpClient httpClient, string requestUri, TBody bodyObject)
        {
            HttpContent content = CreateHttpContent(bodyObject);
            return httpClient.PatchAsync(requestUri, content);
        }
    }
}
