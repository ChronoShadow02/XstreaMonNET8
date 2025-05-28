using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace XstreaMonNET8
{
    internal class WebClientProcessor
    {
        private readonly NetworkCredential _credentials;
        public int WebClientTimeout { get; set; } = 100000;

        public WebClientProcessor(string username, string password)
        {
            _credentials = new NetworkCredential(username, password);
        }

        public WebClientResponse HttpAction(string url, HttpMethod method, string? json = null)
        {
            var response = new WebClientResponse();

            try
            {
                using var handler = new HttpClientHandler
                {
                    Credentials = _credentials
                };

                using var client = new HttpClient(handler)
                {
                    Timeout = TimeSpan.FromMilliseconds(WebClientTimeout)
                };

                client.DefaultRequestHeaders.UserAgent.ParseAdd("XstreaMon");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new(method, url);

                if (!string.IsNullOrEmpty(json))
                {
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage result = client.Send(request);
                result.EnsureSuccessStatusCode();

                response.ReturnedString = result.Content.ReadAsStringAsync().Result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Exception = ex;
                response.ReturnedString = ex is WebException webEx && webEx.Response != null
                    ? new StreamReader(webEx.Response.GetResponseStream()!).ReadToEnd()
                    : ex.Message;
            }

            return response;
        }
    }
}
