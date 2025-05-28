namespace XstreaMonNET8
{
    internal static class Cam4
    {
        private static readonly HttpClient httpClient = new()
        {
            DefaultRequestHeaders = { { "User-Agent", "Mozilla/5.0 (compatible; Cam4Bot/1.0)" } }
        };

        internal static async Task<StreamAdresses> StreamAdresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;
                string str1 = "";

                string result1 = VParse.GetPOST($"https://webchat.cam4.com/requestAccess?roomname={Model_Stream.Pro_Model_Name.ToLower()}&chat_history_limit=0", null).Result;
                if (result1.Length > 0)
                {
                    string str2 = result1.Replace("\"", "");
                    string str3 = "protocol:HLS";
                    if (!str2.Contains(str3))
                        str3 = "protocol:LLHS";
                    if (str2.Contains(str3) && str2.IndexOf(",uri:", str2.IndexOf(str3)) > 0)
                    {
                        int startIndex = str2.IndexOf(",uri:", str2.IndexOf(str3)) + 5;
                        int num = str2.IndexOf(".m3u8", startIndex);
                        str1 = str2.Substring(startIndex, num - startIndex) + ".m3u8";
                    }
                }

                if (string.IsNullOrEmpty(str1) || !Parameter.URL_Response(str1).Result)
                {
                    string result2 = VParse.GetPOST($"https://www.cam4.com/rest/v1.0/profile/{Model_Stream.Pro_Model_Name}/streamInfo?forceHlsUrl=true", null).Result;
                    if (result2.Length > 0)
                    {
                        string str4 = result2.Replace("\"", "");
                        string str5 = "cdnURL:";
                        if (str4.Contains(str5))
                        {
                            int startIndex = str4.IndexOf(str5) + str5.Length;
                            int num = str4.IndexOf("}", startIndex);
                            str1 = str4.Substring(startIndex, num - startIndex);
                        }
                    }
                }

                string[] m3U8_File = VParse.HTML_Load(str1, false).Result.Split('#');
                string str6 = "";

                foreach (string html in m3U8_File)
                {
                    if (html.StartsWith("EXT-X-MEDIA:TYPE=AUDIO,NAME=") && html.Contains("URI="))
                    {
                        str6 = str1.Substring(0, str1.LastIndexOf("/")) + "/" + VParse.HTML_Value(html, "URI=", ",");
                        break;
                    }
                }

                string chunkFile = Find_Chunk_File(m3U8_File, Model_Stream.Pro_Qualität_ID);
                if (!string.IsNullOrEmpty(chunkFile) && str1.Contains(".m3u8"))
                {
                    Model_Stream.Pro_M3U8_Path = str1.Substring(0, str1.LastIndexOf("/")) + "/" + chunkFile.Trim();
                    Model_Stream.Pro_TS_Path = Model_Stream.Pro_M3U8_Path.Substring(0, Model_Stream.Pro_M3U8_Path.LastIndexOf("/")).Trim() + "/";
                    Model_Stream.Pro_Audio_Path = str6.Trim();
                    Model_Stream.Pro_FFMPEG_Path = str1;
                }

                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Cam4.Stream_Adresses");
                return null;
            }
        }

        internal static string Find_Chunk_File(string[] m3U8_File, int Qualität_ID)
        {
            string result = "";
            try
            {
                foreach (string line in m3U8_File)
                {
                    if (line.StartsWith("EXT-X-STREAM-INF"))
                    {
                        int startIndex = line.IndexOf("\r\n");
                        if (startIndex == -1)
                            startIndex = line.IndexOf("\n");

                        bool found = Qualität_ID switch
                        {
                            0 => line.Contains(".m3u8"),
                            1 => line.Contains("640x360") || line.Contains("426x240") || line.Contains("320x180"),
                            2 => line.Contains("960x540"),
                            3 => line.Contains("1280x720"),
                            4 => line.Contains("1920x1080"),
                            _ => false
                        };

                        if (found)
                        {
                            result = line.Substring(startIndex);
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(result))
                {
                    string last = m3U8_File.LastOrDefault() ?? "";
                    if (last.Contains("chunk"))
                        result = last.Substring(last.IndexOf("chunk"));
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Cam4.Find_Chunk_File");
            }

            return result.Replace("\r\n", "").Replace("\n", "");
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                await Task.CompletedTask;
                var model = await Class_Model_List.Class_Model_Find(0, Model_Name);
                if (await Parameter.URL_Response(model?.Pro_Model_M3U8))
                    return 1;

                string result = VParse.HTML_Load($"https://www.cam4.de.com/rest/v1.0/profile/{Model_Name}/streamInfo", true).Result;
                return result.Contains("canUseCDN:true") ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }

        internal static async Task<Image> Image_FromWeb(string Model_Name)
        {
            try
            {
                string url = $"https://snapshots.xcdnpro.com/thumbnails/{Model_Name}";
                using Stream stream = await httpClient.GetStreamAsync(url);
                return Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Parameter.C4_ImageFromWeb(Model_Name) = {Model_Name}");
                return null;
            }
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;

            Channel_Info info = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 4,
                Pro_Name = Model_Name
            };

            try
            {
                string result = VParse.HTML_Load($"https://www.cam4.de.com/rest/v1.0/profile/{Model_Name}/info", true).Result;

                if (result.Length > 11)
                {
                    info.Pro_Name = VParse.HTML_Value(result, "username:", ",");
                    info.Pro_Exist = info.Pro_Name.Length > 0;

                    string gender = VParse.HTML_Value(result, "gender:", ",");
                    info.Pro_Gender = gender switch
                    {
                        "female" => 1,
                        "male" => 2,
                        "Couple" => 3,
                        "transgender" => 4,
                        _ => 0
                    };

                    string country = VParse.HTML_Value(result, "countryId:", ",");
                    string city = VParse.HTML_Value(result, "city:", ",");
                    info.Pro_Country = $"{country}{(string.IsNullOrEmpty(country) ? "" : " ")}{city}";

                    string description = info.Pro_Profil_Beschreibung + info.Pro_Country;
                    description += $"{(description.Length > 0 ? " " : "")}{TXT.TXT_Description("Alter")}: {ValueBack.Alter(Convert.ToDateTime(info.Pro_Birthday))}";
                    info.Pro_Profil_Beschreibung = description;

                    info.Pro_Last_Online = ValueBack.TimeStampToDateTime(VParse.HTML_Value(result, "lastBroadcast:", ",")).ToString();

                    string language = VParse.HTML_Value(result, "mainLanguage:", ",");
                    if (!string.Equals(language, "null", StringComparison.OrdinalIgnoreCase))
                        info.Pro_Languages = language;

                    string imageUrl = VParse.HTML_Value(result, "profileImageUrl:", ",").Trim();
                    if (await Parameter.URL_Response(imageUrl))
                    {
                        using Stream imgStream = await httpClient.GetStreamAsync(imageUrl);
                        info.Pro_Profil_Image = Image.FromStream(imgStream);
                    }

                    info.Pro_Birthday = Convert.ToDateTime(VParse.HTML_Value(result, "birthdate:", ","));
                    info.Pro_Online = await Online(info.Pro_Name) == 1;
                }

                return info;
            }
            catch
            {
                return info;
            }
        }
    }
}
