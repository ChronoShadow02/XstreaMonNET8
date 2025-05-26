using System.Net;
using System.Text;

namespace XstreaMonNET8
{
    public static class VParse
    {
        private static string BackString_Check(ref string checkString)
        {
            try
            {
                if (string.IsNullOrEmpty(checkString) ||
                    (checkString.Length < 30 && checkString != "429") ||
                    checkString.StartsWith('\u001F') ||
                    checkString.StartsWith('\u0003'))
                {
                    return string.Empty;
                }

                return checkString;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"VParse.BackString_Check {checkString}");
                return string.Empty;
            }
        }

        internal static string URL_Check(string urlString) => urlString.Replace("\\/", "/");

        internal static async Task<string> HTML_Load(string urlString, bool replaceSpace)
        {
            if (string.IsNullOrWhiteSpace(urlString) || urlString == "https:")
                return "";

            urlString = URL_Check(urlString);
            string checkString = "";

            try
            {
                var client = new CustomHttpClient(5000);
                using var httpClient = client.Instance;

                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64)");
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                checkString = await httpClient.GetStringAsync(urlString);
                if (replaceSpace && !string.IsNullOrEmpty(checkString))
                {
                    checkString = Replace_Space(checkString);
                }
            }
            catch (HttpRequestException ex)
            {
                Parameter.Error_Message(ex, $"HTML_Load (HttpRequestException): {urlString}");
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"HTML_Load (General Exception): {urlString}");
            }

            return BackString_Check(ref checkString);
        }


        internal static async Task<string?> HTML_Load(string url)
        {
            if (string.IsNullOrWhiteSpace(url) || url == "https:")
                return null;

            string? checkString = null;
            try
            {
                url = URL_Check(url);

                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64)");
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));

                checkString = await httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException ex)
            {
                Parameter.Error_Message(ex, $"HTML_Load (HttpRequestException): {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }

            return BackString_Check(ref checkString!);
        }

        internal static async Task<string?> GetPOSTPHP(string siteUrl, SecurityProtocolType sslType = SecurityProtocolType.SystemDefault)
        {
            if (string.IsNullOrWhiteSpace(siteUrl) || siteUrl == "https:")
                return null;

            string url = URL_Check(siteUrl);
            string? checkString = null;
            var originalProtocol = ServicePointManager.SecurityProtocol;

            try
            {
                ServicePointManager.SecurityProtocol = sslType;

                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(20)
                };

                httpClient.DefaultRequestHeaders.Accept.ParseAdd("*/*");
                httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64)");

                checkString = await httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException ex)
            {
                return EX_Webexception(ex, url);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            finally
            {
                ServicePointManager.SecurityProtocol = originalProtocol;
            }

            return BackString_Check(ref checkString!);
        }

        internal static async Task<string?> Chrome_Load(string url, bool replaceSpace)
        {
            if (string.IsNullOrWhiteSpace(url) || url == "https:")
                return null;

            url = URL_Check(url);
            string? checkString = null;

            try
            {
                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(20)
                };

                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome Safari");
                httpClient.DefaultRequestHeaders.Accept.ParseAdd("text/html");

                checkString = await httpClient.GetStringAsync(url);

                if (replaceSpace && checkString != null)
                    checkString = Replace_Space(checkString);
            }
            catch (HttpRequestException ex)
            {
                Parameter.Error_Message(ex, $"Chrome_Load (HttpRequestException): {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }

            return BackString_Check(ref checkString!);
        }


        internal static async Task<string?> GetPOST(string url, string data, SecurityProtocolType sslType = SecurityProtocolType.SystemDefault)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            url = URL_Check(url);
            string? result = null;

            // Establecer el protocolo SSL (aunque con HttpClient, esto no es estrictamente necesario en .NET 6+)
            var originalProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = sslType;

            try
            {
                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(20)
                };

                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

                HttpResponseMessage response;

                if (string.IsNullOrWhiteSpace(data))
                {
                    response = await httpClient.GetAsync(url);
                }
                else
                {
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync(url, content);
                }

                response.EnsureSuccessStatusCode(); // Lanza excepción si el status code no es 2xx
                result = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return EX_Webexception(ex, url); // Asegúrate de que EX_Webexception también soporte HttpRequestException
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            finally
            {
                ServicePointManager.SecurityProtocol = originalProtocol;
            }

            return BackString_Check(ref result!);
        }

        internal static string? EX_Webexception(Exception ex, string url)
        {
            Parameter.Error_Message(ex, $"Excepción en: {url}");
            return null;
        }

        internal static string Replace_Space(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input
                .Replace("\n", "")
                .Replace(" ", "")
                .Replace("\"", "");
        }

        internal static string HTML_Value(string html, string start, string end, bool clean = true)
        {
            if (string.IsNullOrEmpty(html)) return "";

            if (clean)
            {
                html = html
                    .Replace("\"", "")
                    .Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", "")
                    .Replace("\t", "")
                    .Replace("  ", "")
                    .Replace("\\u00f1", "ñ")
                    .Replace("\\u2665", "♥")
                    .Replace("\\u2728", "✨")
                    .Replace("\\u00ed", "í")
                    .Replace("\\ud835", "𝓛")
                    .Replace("\\udd84", "🦄")
                    .Replace("\\udd7a", "🤺")
                    .Replace("\\udd80", "🦀")
                    .Replace("\\udd73", "🧳")
                    .Replace("\\udd70", "🥰");
            }

            string htmlLower = html.ToLower();
            string startLower = start.ToLower();
            string endLower = end.ToLower();

            int startIndex = htmlLower.IndexOf(startLower);
            if (startIndex == -1) return "";

            startIndex += start.Length;
            int endIndex = htmlLower.IndexOf(endLower, startIndex);
            if (endIndex == -1) endIndex = html.Length;

            return html.Substring(startIndex, endIndex - startIndex);
        }
    }
}
