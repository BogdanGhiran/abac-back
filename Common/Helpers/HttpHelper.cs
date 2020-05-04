using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Helpers
{
    public class HttpHelper
    {
        public static async Task PostAsync(Uri uri, object payload)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var json = JsonConvert.SerializeObject(payload);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None)
                                             .ConfigureAwait(false);
            
            response.EnsureSuccessStatusCode();
        }
    }
}
