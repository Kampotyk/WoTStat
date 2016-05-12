using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace WotStat
{
    static class Request
    {
        public static string PostRequest(string url, NameValueCollection values) 
        {
            using (var client = new WebClient())
            {
                var response = client.UploadValues(url, values);
                return Encoding.Default.GetString(response);
            }
        }
    }
}
