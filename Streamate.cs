using System.Net;

namespace XstreaMonNET8
{
    internal static class Streamate
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;

                string url = $"https://manifest-server.naiadsystems.com/live/s:{Model_Stream.Pro_Model_Name}.json?last=load&format=webrtc";
                string HTML_Page = (await VParse.HTML_Load(url, false)).Replace("\"", "");

                if (HTML_Page.Length <= 0 || !HTML_Page.Contains("mp4-hls:{manifest:"))
                    return null;

                string manifestUrl = VParse.HTML_Value(HTML_Page, "mp4-hls:{manifest:", ",videoCodec", false);
                string[] strArray1 = (await VParse.HTML_Load(manifestUrl, true)).Split('#');

                string ChunkString = string.Empty;
                foreach (var str in strArray1)
                {
                    switch (Model_Stream.Pro_Qualität_ID)
                    {
                        case 0:
                            if (strArray1.Last().Contains(".m3u8"))
                            {
                                ChunkString = strArray1.Last()[strArray1.Last().IndexOf("https:")..];
                                goto end_loop;
                            }
                            break;
                        case 1:
                            if (str.Contains("256x144"))
                            {
                                ChunkString = str[str.IndexOf("https:")..];
                                goto end_loop;
                            }
                            break;
                        case 2:
                            if (str.Contains("768x432"))
                            {
                                ChunkString = str[str.IndexOf("https:")..];
                                goto end_loop;
                            }
                            break;
                        case 3:
                            if (str.Contains("1280x720"))
                            {
                                ChunkString = str[str.IndexOf("https:")..];
                                goto end_loop;
                            }
                            break;
                        case 4:
                            if (str.Contains("1920x1080"))
                            {
                                ChunkString = str[str.IndexOf("https:")..];
                                goto end_loop;
                            }
                            break;
                    }
                }

            end_loop:
                Model_Stream.Pro_M3U8_Path = ChunkString.Trim();
                Model_Stream.Pro_TS_Path = ChunkString[..ChunkString.IndexOf("index.m3u8")].Trim();

                string previewRaw = VParse.HTML_Value(HTML_Page, "jpeg:{encodings:[", "]").Trim();
                Model_Stream.Pro_Preview_Image = previewRaw[(previewRaw.LastIndexOf("https://") >= 0 ? previewRaw.LastIndexOf("https://") : 0)..].Replace("}", "");

                Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray1, ChunkString);

                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Streamate.Stream_Adresses");
                return null;
            }
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                await Task.CompletedTask;

                var model = await Class_Model_List.Class_Model_Find(0, Model_Name);
                if (await Parameter.URL_Response(model?.Pro_Model_M3U8))
                    return 1;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://manifest-server.naiadsystems.com/live/s:{Model_Name}.json?last=load&format=webrtc");
                request.Timeout = 20000;
                request.ReadWriteTimeout = 10000;

                using var response = request.GetResponse();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        internal static async Task<Image> Image_FromWeb(Class_Model Model_Class)
        {
            await Task.CompletedTask;

            try
            {
                if (Model_Class.Pro_Model_Preview_Path == null)
                    return null;

                using var webClient = new WebClient();
                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                using var stream = webClient.OpenRead(Model_Class.Pro_Model_Preview_Path);
                return Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Streamate.Image_FromWeb({Model_Class.Pro_Model_Name})");
                return null;
            }
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;

            var channelInfo = new Channel_Info
            {
                Pro_Exist = false,
                Pro_Website_ID = 5,
                Pro_Name = Model_Name.ToLower()
            };

            try
            {
                string html = await VParse.Chrome_Load($"https://streamate.com//cam/{Model_Name.ToLower()}", true);
                if (html.Length > 0)
                {
                    string genderCode = VParse.HTML_Value(html, "characteristic:{gender:", "}");

                    channelInfo.Pro_Gender = genderCode switch
                    {
                        "f" => 1,
                        "m" => 2,
                        "c" => 3,
                        "t" => 4,
                        _ => 0
                    };

                    channelInfo.Pro_Exist = genderCode.Length > 0;

                    if (channelInfo.Pro_Exist)
                    {
                        channelInfo.Pro_Country = VParse.HTML_Value(html, "country:", ",");
                        channelInfo.Pro_Profil_Beschreibung = VParse.HTML_Value(html, "Headline:", ",Attr");
                        channelInfo.Pro_Languages = VParse.HTML_Value(html, "Languages:[", "],");
                        channelInfo.Pro_Online = await Online(channelInfo.Pro_Name) == 1;
                    }
                }

                return channelInfo;
            }
            catch
            {
                return channelInfo;
            }
        }
    }
}
