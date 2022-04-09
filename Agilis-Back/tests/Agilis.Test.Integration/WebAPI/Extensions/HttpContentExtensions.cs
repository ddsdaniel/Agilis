using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Agilis.Test.Integration.WebAPI.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<TViewModel> ReadAsViewModelAsync<TViewModel>(this HttpContent source)
        {
            var jsonString = await source.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TViewModel>(jsonString);
        }
    }
}
