using System.Net;
using System.Text.RegularExpressions;

namespace XstreaMonNET8
{
    internal static class EPlay
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;
                string result = await VParse.HTML_Load("https://search-cf.eplay.com/channels?size=1&exactMatch=true&username=" + Model_Stream.Pro_Model_Name, true);
                string Stream_URL = null!;

                string manifestUrl = VParse.HTML_Value(result, "manifest:", ",");
                if (!string.IsNullOrEmpty(manifestUrl))
                {
                    string manifestResult = await VParse.HTML_Load(manifestUrl, true);
                    Stream_URL = Find_M3u8_Path(manifestResult);
                }

                return await Read_Model_Stream(Stream_URL, Model_Stream);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "EPlay.Stream_Adresses");
                return null!;
            }
        }

        private static async Task<StreamAdresses> Read_Model_Stream(string Stream_URL, StreamAdresses Model_Stream)
        {
            try
            {
                if (!string.IsNullOrEmpty(Stream_URL))
                {
                    string[] strArray = (await VParse.HTML_Load(Stream_URL, true)).Split('#');
                    string ChunkString = Find_Chunk_File(strArray, Model_Stream.Pro_Qualität_ID);

                    if (!string.IsNullOrEmpty(ChunkString))
                    {
                        Model_Stream.Pro_M3U8_Path = ChunkString.Trim();
                        Model_Stream.Pro_TS_Path = ChunkString[..ChunkString.IndexOf("index.m3u8")].Trim();
                        Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray, ChunkString);
                    }
                    else
                    {
                        Model_Stream.Pro_M3U8_Path = null;
                        Model_Stream.Pro_TS_Path = null;
                        Model_Stream.Pro_Record_Resolution = 0;
                    }
                }
                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "EPlay.Read_Model_Stream");
                return null!;
            }
        }

        private static string Find_M3u8_Path(string HTML_String)
        {
            try
            {
                if (HTML_String.Contains("mp4-hls:{manifest:"))
                {
                    return VParse.HTML_Value(HTML_String, "mp4-hls:{manifest:", ",");
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Find_M3U8_Path");
            }

            return "";
        }

        private static string Find_Chunk_File(string[] M3U8_File, int Qualität_ID)
        {
            try
            {
                string chunkFile = "";
                int maxBandwidth = 0;

                foreach (var HTML_Page in M3U8_File)
                {
                    if (HTML_Page.StartsWith("EXT-X-STREAM-INF"))
                    {
                        switch (Qualität_ID)
                        {
                            case 0:
                                int bandwidth = ValueBack.Get_CInteger(VParse.HTML_Value(HTML_Page, "BANDWIDTH=", ","));
                                if (bandwidth > maxBandwidth)
                                {
                                    maxBandwidth = bandwidth;
                                    chunkFile = HTML_Page[HTML_Page.IndexOf("https://")..];
                                }
                                break;
                            case 1 when HTML_Page.Contains("426x240"):
                            case 2 when HTML_Page.Contains("960x540"):
                            case 3 when HTML_Page.Contains("1280x720"):
                            case 4 when HTML_Page.Contains("1920x1080"):
                                return HTML_Page[HTML_Page.IndexOf("https://")..];
                        }
                    }
                }

                if (string.IsNullOrEmpty(chunkFile))
                {
                    string lastLine = M3U8_File.LastOrDefault(line => line.Contains("chunk"))!;
                    if (!string.IsNullOrEmpty(lastLine))
                        chunkFile = lastLine[lastLine.IndexOf("https://")..];
                }

                return chunkFile;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "EPlay.Find_Chunk_File");
                return "";
            }
        }

        internal static async Task<int> Online(string Model_Name)
        {
            await Task.CompletedTask;
            try
            {
                var model = await Class_Model_List.Class_Model_Find(0, Model_Name);
                if (await Parameter.URL_Response(model?.Pro_Model_M3U8))
                    return 1;

                string? htmlRaw = await VParse.HTML_Load("https://search-cf.eplay.com/channels?size=1&exactMatch=true&username=" + Model_Name);

                if (string.IsNullOrEmpty(htmlRaw))
                    return 0;

                string HTML_Page = Regex.Unescape(htmlRaw).Replace("\"", "");

                if (HTML_Page.Contains("live:true"))
                {
                    string manifestUrl = VParse.HTML_Value(HTML_Page, "manifest:", ",");
                    return string.IsNullOrEmpty(await VParse.HTML_Load(manifestUrl, true)) ? 2 : 1;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Eplay.Online");
            }

            return 0;
        }

        internal static async Task<Image> Image_FromWeb(Class_Model Model_Class)
        {
            try
            {
                string result = await VParse.HTML_Load("https://search-cf.eplay.com/channels?size=1&exactMatch=true&username=" + Model_Class.Pro_Model_Name, true);
                string manifestUrl = VParse.HTML_Value(result, "manifest:", ",");

                if (!string.IsNullOrEmpty(manifestUrl))
                {
                    if (Model_Class.Pro_Model_Preview_Path == null)
                    {
                        string manifestResult = await VParse.HTML_Load(manifestUrl, true);
                        Model_Class.Pro_Model_Preview_Path = VParse.HTML_Value(manifestResult, "videoHeight:540,location:", "},");
                    }

                    if (!string.IsNullOrEmpty(Model_Class.Pro_Model_Preview_Path))
                    {
                        using HttpClient httpClient = new();
                        using Stream stream = await httpClient.GetStreamAsync(Model_Class.Pro_Model_Preview_Path);
                        return Image.FromStream(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "EPlay.Image_FromWeb");
            }

            return null!;
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;

            Channel_Info channelInfo = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 12,
                Pro_Name = Model_Name
            };

            try
            {
                if (string.IsNullOrEmpty(Model_Name)) return channelInfo;

                string str1 = Regex.Unescape(await VParse.HTML_Load("https://search-cf.eplay.com/channels?size=1&exactMatch=true&username=" + Model_Name) ?? "").Replace("\"", "");
                if (string.IsNullOrEmpty(str1)) return channelInfo;

                if (VParse.HTML_Value(str1, "id:", ",").Length > 0)
                {
                    channelInfo.Pro_Exist = true;

                    string pronoun = VParse.HTML_Value(str1, "pronoun:", ",").Trim();
                    channelInfo.Pro_Gender = pronoun switch
                    {
                        "she" => 1,
                        "a man" => 2,
                        "a couple" => 3,
                        "trans" => 4,
                        _ => 0
                    };

                    channelInfo.Pro_Country = VParse.HTML_Value(str1, "ethnicityTags:[", "]").Trim();
                    channelInfo.Pro_Languages = VParse.HTML_Value(str1, "languageTags:[", "]").Trim();
                    channelInfo.Pro_Last_Online = VParse.HTML_Value(str1, "personsLastUpdated:", ",").Trim();
                    channelInfo.Pro_Birthday = VParse.HTML_Value(str1, "dob:", ",");

                    if (!DateTime.TryParse(channelInfo.Pro_Birthday?.ToString(), out _))
                        channelInfo.Pro_Birthday = null!;

                    channelInfo.Pro_Profil_Beschreibung = VParse.HTML_Value(str1, "intro:", ",qualityTags:").Trim();

                    string avatar = VParse.HTML_Value(str1, "avatar:", ",").Trim();
                    if (await Parameter.URL_Response(avatar))
                    {
                        using HttpClient httpClient = new();
                        using Stream stream = await httpClient.GetStreamAsync(avatar);
                        channelInfo.Pro_Profil_Image = Image.FromStream(stream);
                    }

                    channelInfo.Pro_Online = str1.Contains("isOnline:true");
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Eplay.Profil");
            }

            return channelInfo;
        }
    }
}
