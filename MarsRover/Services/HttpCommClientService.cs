using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MarsRover.Interfaces;
using MarsRover.Models;
using Newtonsoft.Json;
using NLog;

namespace MarsRover.Services
{
   public class HttpCommClientService : IHttpCommClientService
    {
        public HttpCommClientService()
        {

        }

        /// <summary>
        /// Gets the web response back
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> GetResponseAsync(Uri uri)
        {
            var url = uri?.AbsoluteUri;

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            handler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;

            var client = new HttpClient(handler);
            var urlDataString = Uri.UnescapeDataString(uri.ToString());

            Debug.WriteLine($"Get the image for (uri={urlDataString}");
            var response = await client.GetAsync(urlDataString) ??
                           throw new ArgumentNullException($"GetJsonResponseAsync({url})");

            // A non-OK response throws a web exception
            if (response.StatusCode != HttpStatusCode.OK) throw new WebException($"{response.StatusCode}");

            var requestResponse = string.Empty;
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                if (responseStream != null)
                    using (var reader = new StreamReader(responseStream))
                    {
                        requestResponse = reader.ReadToEnd();
                    }
            }

            return requestResponse;
        }
    }
}
