using System.Collections.Generic;
using System.Net.Http;

namespace WotStat
{
    static class Request
    {
        static async void PostRequest(string url, Dictionary<string, string> values) 
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
            }
        }

        static void GetRequest(string url)
        {
            using (var client = new HttpClient())
            {
                var responseString = client.GetStringAsync(url);
            }
        }
    }
}
