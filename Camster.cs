namespace XstreaMonNET8
{
    internal sealed class Camster
    {
        private static readonly HttpClient httpClient = new();

        internal static async Task<StreamAdresses?> Stream_Adresses(StreamAdresses modelStream)
        {
            try
            {
                string statusUrl = $"https://www.camster.com/ws/rooms/check-model-status.php?model_name={modelStream.Pro_Model_Name}";
                string statusResult = await VParse.HTML_Load(statusUrl, true);

                if (string.IsNullOrEmpty(statusResult) || !statusResult.Contains("online,model_id:"))
                    return null;

                string modelId = VParse.HTML_Value(statusResult, "online,model_id:", ",DATA", false);
                string m3u8Url = $"https://hls.vscdns.com/manifest.m3u8?key=nil&provider=cdn5&is_ll=true&model_id={modelId}";
                string m3u8Content = await VParse.HTML_Load(m3u8Url, true);

                string[] lines = m3u8Content.Split('#');
                string chunkPath = string.Empty;
                string audioPath = string.Empty;

                foreach (var line in lines)
                {
                    if (line.StartsWith("EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID"))
                    {
                        int uriIndex = line.IndexOf("URI=") + 4;
                        audioPath = line.Substring(uriIndex);
                        if (audioPath.Contains(','))
                            audioPath = audioPath.Substring(0, audioPath.LastIndexOf(','));
                        break;
                    }
                }

                foreach (var line in lines)
                {
                    bool found = modelStream.Pro_Qualität_ID switch
                    {
                        0 => lines.Last().Contains(".m3u8"),
                        1 => line.Contains("480x270"),
                        2 => line.Contains("640x360"),
                        3 => line.Contains("1280x720"),
                        4 => line.Contains("1920x1080"),
                        _ => false
                    };

                    if (found)
                    {
                        string source = modelStream.Pro_Qualität_ID == 0 ? lines.Last() : line;
                        int chunkIndex = source.IndexOf("chunklist");
                        if (chunkIndex > -1)
                        {
                            chunkPath = source.Substring(chunkIndex);
                            break;
                        }
                    }
                }

                modelStream.Pro_M3U8_Path = $"https://hls.vscdns.com/{chunkPath.Trim()}";
                modelStream.Pro_Audio_Path = $"https://hls.vscdns.com/{audioPath.Trim()}";
                modelStream.Pro_FFMPEG_Path = m3u8Url;
                modelStream.Pro_Record_Resolution = Sites.Resolution_Find(lines, chunkPath);
                modelStream.Pro_TS_Path = "";

                return modelStream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Camster.Stream_Adresses");
                return null;
            }
        }

        internal static async Task<int> Online(string modelName)
        {
            try
            {
                string statusUrl = $"https://www.camster.com/ws/rooms/check-model-status.php?model_name={modelName}";
                string htmlPage = await VParse.HTML_Load(statusUrl, true);

                htmlPage = htmlPage.Replace("\"", "");
                if (string.IsNullOrWhiteSpace(htmlPage) || !htmlPage.ToLower().Contains("status:online"))
                    return 0;

                string modelId = VParse.HTML_Value(htmlPage, "model_id:", ",DATA");

                string manifestUrl = $"https://hls.vscdns.com/manifest.m3u8?key=nil&provider=cdn5&is_ll=true&model_id={modelId}";
                bool isReachable = await Parameter.URL_Response(manifestUrl);

                return isReachable ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }

        internal static async Task<Image?> Image_FromWeb(Class_Model model)
        {
            try
            {
                string statusUrl = $"https://www.camster.com/ws/rooms/check-model-status.php?model_name={model.Pro_Model_Name}";
                string result = await VParse.HTML_Load(statusUrl, true);

                if (!string.IsNullOrWhiteSpace(result) && result.Contains("online,model_id:"))
                {
                    string modelId = VParse.HTML_Value(result, "online,model_id:", ",DATA", false);
                    string imageUrl = $"https://live-screencaps.vscdns.com/{modelId}-desktop.jpg";

                    byte[] imageData = await httpClient.GetByteArrayAsync(imageUrl);
                    using MemoryStream ms = new(imageData);
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Camster.Image_FromWeb");
            }

            return null;
        }

        internal static async Task<Channel_Info> Profil(string modelName)
        {
            Channel_Info info = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 10,
                Pro_Name = modelName
            };

            try
            {
                string url = $"https://www.camster.com/models/bios/{modelName.ToLower()}/about.php";
                string result = await VParse.HTML_Load(url, true);

                if (string.IsNullOrWhiteSpace(result))
                    return info;

                string genderUrl = VParse.HTML_Value(result, "var listsUrl", ";");

                info.Pro_Gender = genderUrl.Contains("/girls/") ? 1 :
                                  genderUrl.Contains("/guys/") ? 2 :
                                  genderUrl.Contains("/trans/") ? 4 : 0;

                info.Pro_Exist = genderUrl.Length > 0;

                if (!info.Pro_Exist)
                    return info;

                info.Pro_Country = VParse.HTML_Value(result, "<div>Location:</div><div>", "</div>");
                info.Pro_Profil_Beschreibung = info.Pro_Country;

                string birthdayString = VParse.HTML_Value(result, "<div>Birthday:</div><div>", "</div>");
                if (!string.IsNullOrWhiteSpace(birthdayString))
                {
                    int month = ValueBack.Month_From_String(birthdayString);
                    int day = ValueBack.Get_Int_From_String(birthdayString);
                    int age = ValueBack.Get_Int_From_String(VParse.HTML_Value(result, "<div>Age:</div><div>", "</div>"));
                    int year = DateTime.Today.Year - age;
                    if (DateTime.Now < new DateTime(DateTime.Now.Year, month, day))
                        year--;

                    info.Pro_Birthday = new DateTime(year, month, day);
                }

                string languages = VParse.HTML_Value(result, "<div>Languages:</div><div>", "</div>");

                while (languages.Contains("<a href="))
                {
                    int start = languages.IndexOf("<");
                    int end = languages.IndexOf(">", start);
                    if (start >= 0 && end > start)
                        languages = languages.Remove(start, end - start + 1);

                    int close = languages.IndexOf("</a>");
                    if (close >= 0)
                        languages = languages.Remove(close, 4);
                }

                languages = languages.TrimStart();
                if (!languages.Equals("null", StringComparison.OrdinalIgnoreCase))
                    info.Pro_Languages = languages;

                string ageText = VParse.HTML_Value(result, "<div>Age:</div><div>", "</div>");
                if (!string.IsNullOrWhiteSpace(ageText))
                {
                    if (!string.IsNullOrWhiteSpace(info.Pro_Profil_Beschreibung))
                        info.Pro_Profil_Beschreibung += " ";
                    info.Pro_Profil_Beschreibung += TXT.TXT_Description("Alter") + ": " + ageText;
                }

                info.Pro_Last_Online = VParse.HTML_Value(result, "<div>Last Online:</div><div>", "</div>").Trim();

                string imageUrl = VParse.HTML_Value(result, "<img class=model-photo src=", "/>").Trim();

                if (await Parameter.URL_Response(imageUrl))
                {
                    using Stream stream = await httpClient.GetStreamAsync(imageUrl);
                    info.Pro_Profil_Image = Image.FromStream(stream);
                }

                info.Pro_Online = await Camster.Online(info.Pro_Name) == 1;

                return info;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Camster.Profil");
                return info;
            }
        }
    }
}
