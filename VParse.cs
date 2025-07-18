using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace XstreaMonNET8
{
    public static class VParse
    {
        private static readonly HttpClient httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(20)
        };

        private static string BackString_Check(ref string Check_String)
        {
            try
            {
                if (string.IsNullOrEmpty(Check_String) ||
                    (Check_String.Length < 30 && Check_String != "429") ||
                    Check_String.StartsWith('\u001F') ||
                    Check_String.StartsWith('\u0003'))
                {
                    return null!;
                }
                return Check_String;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VParse.BackString_Check " + Check_String);
                return null!;
            }
        }

        internal static string URL_Check(string URL_String) => URL_String.Replace("\\/", "/");

        internal static async Task<string> HTML_Load(string URLString, bool ReplaceSpace)
        {
            if (string.IsNullOrWhiteSpace(URLString) || URLString == "https:")
                return "";

            URLString = URL_Check(URLString);
            string Check_String = "";

            try
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64)");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Check_String = await httpClient.GetStringAsync(URLString).ConfigureAwait(false);

                if (ReplaceSpace && !string.IsNullOrEmpty(Check_String))
                    Check_String = Replace_Space(Check_String);
            }
            catch (HttpRequestException ex)
            {
                Parameter.Error_Message(ex, "VParse.HTML_Load (HttpRequestException): " + URLString);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VParse.HTML_Load: " + URLString);
            }

            return BackString_Check(ref Check_String);
        }

        internal static async Task<string> HTML_Load(string URL_String)
        {
            if (string.IsNullOrWhiteSpace(URL_String))
                return "";

            URL_String = URL_Check(URL_String);
            string Check_String = "";

            try
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64)");
                Check_String = await httpClient.GetStringAsync(URL_String).ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                Parameter.Error_Message(ex, "VParse.HTML_Load (HttpRequestException): " + URL_String);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VParse.HTML_Load: " + URL_String);
            }

            return BackString_Check(ref Check_String);
        }

        internal static async Task<string> GetPOSTPHP(string Site_URL, SecurityProtocolType SSL_Type = SecurityProtocolType.SystemDefault)
        {
            if (string.IsNullOrWhiteSpace(Site_URL))
                return "";

            Site_URL = URL_Check(Site_URL);
            string Check_String = "";

            try
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                Check_String = await httpClient.GetStringAsync(Site_URL).ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                return EX_Webexception(ex, Site_URL);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VParse.GetPOSTPHP: " + Site_URL);
            }

            return BackString_Check(ref Check_String);
        }

        internal static async Task<string> Chrome_Load(string URLString, bool ReplaceSpace)
        {
            if (string.IsNullOrWhiteSpace(URLString) || URLString == "https:")
                return "";

            URLString = URL_Check(URLString);
            string Check_String = "";

            try
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) Chrome/115.0 Safari/537.36");
                Check_String = await httpClient.GetStringAsync(URLString).ConfigureAwait(false);

                if (ReplaceSpace && !string.IsNullOrEmpty(Check_String))
                    Check_String = Replace_Space(Check_String);
            }
            catch (HttpRequestException ex)
            {
                Parameter.Error_Message(ex, "VParse.Chrome_Load (HttpRequestException): " + URLString);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VParse.Chrome_Load: " + URLString);
            }

            return BackString_Check(ref Check_String);
        }

        internal static async Task<string> GetPOST(string url, string data, SecurityProtocolType SSL_Type = SecurityProtocolType.SystemDefault)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "";

            url = URL_Check(url);
            string result = "";

            try
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

                HttpResponseMessage response;
                if (string.IsNullOrWhiteSpace(data))
                {
                    response = await httpClient.GetAsync(url).ConfigureAwait(false);
                }
                else
                {
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                }

                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                return EX_Webexception(ex, url);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "VParse.GetPOST: " + url);
            }

            return BackString_Check(ref result);
        }

        internal static string EX_Webexception(HttpRequestException ex, string url)
        {
            Parameter.Error_Message(ex, "HTTP Exception: " + url);
            return null!;
        }

        internal static string Replace_Space(string Orginal_String)
        {
            return Orginal_String?.Replace("\n", "").Replace(" ", "").Replace("\"", "")!;
        }

        internal static string HTML_Value(string HTML_Page, string Start_String, string End_String, bool Bereinigen = true)
        {
            if (HTML_Page != null)
            {
                if (Bereinigen)
                {
                    HTML_Page = HTML_Page.Replace("\"", "")
                                         .Replace("\n", "")
                                         .Replace("\r", "")
                                         .Replace("\r\n", "")
                                         .Replace("\t", "")
                                         .Replace("  ", " ")
                                         .Replace("  ", "")
                                         .Replace("\\u00f1", "ñ")
                                         .Replace("\\u2665", "♥")
                                         .Replace("\\u2728", "✨")
                                         .Replace("\\u00ed", "í")
                                         .Replace("\\ud835", "\uD835\uDCDB")
                                         .Replace("\\udd84", "\uD83E\uDD84")
                                         .Replace("\\udd7a", "\uD83E\uDD7A")
                                         .Replace("\\udd80", "\uD83E\uDD80")
                                         .Replace("\\udd73", "\uD83E\uDD73")
                                         .Replace("\\udd70", "\uD83E\uDD70");
                }

                if (HTML_Page.Length > 0)
                {
                    int startIndex = HTML_Page.IndexOf(Start_String, StringComparison.OrdinalIgnoreCase);
                    if (startIndex >= 0)
                    {
                        startIndex += Start_String.Length;
                        int endIndex = HTML_Page.Length;

                        if (!string.IsNullOrEmpty(End_String))
                        {
                            endIndex = HTML_Page.IndexOf(End_String, startIndex, StringComparison.OrdinalIgnoreCase);
                            if (endIndex == -1)
                                endIndex = HTML_Page.Length;
                        }

                        if (startIndex < endIndex && endIndex <= HTML_Page.Length)
                        {
                            HTML_Page = HTML_Page[startIndex..endIndex];
                        }
                        else
                        {
                            HTML_Page = "";
                        }
                    }
                    else
                    {
                        HTML_Page = "";
                    }
                }
            }
            return HTML_Page!;
        }
    }
}
