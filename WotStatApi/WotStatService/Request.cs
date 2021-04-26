﻿using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace WotStat
{
    internal static class Request
    {
        public static string PostRequest(string url, NameValueCollection values) 
        {
            using (var client = new WebClient())
            {
                var response = client.UploadValues(url, values);
                return Encoding.Default.GetString(response);
            }
        }

        public static string GetRequest(string url)
        {
            using (var client = new WebClient())
            {
                var response = client.DownloadData(url);
                return Encoding.Default.GetString(response);
            }
        }
    }
}
