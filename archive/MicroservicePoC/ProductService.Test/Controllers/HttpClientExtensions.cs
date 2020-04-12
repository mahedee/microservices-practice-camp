using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Test.Controllers
{
    static class HttpClientExtensions
    {
        public static async Task<T> DoGetAsync<T>(this HttpClient client, string uri)
        where T : class
        {
            var response = await client.GetAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<T>(responseContent);

            return result;
        }
    }
}
