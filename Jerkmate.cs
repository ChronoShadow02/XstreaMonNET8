using System.Net;

namespace XstreaMonNET8
{
    internal static class Jerkmate
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;

                string htmlPage = (await VParse.HTML_Load($"https://manifest-server.naiadsystems.com/live/s:{Model_Stream.Pro_Model_Name}.json?last=load&format=webrtc", true)).Replace("\"", "");

                if (string.IsNullOrEmpty(htmlPage) || !htmlPage.Contains("mp4-hls:{manifest:"))
                    return null;

                string manifestUrl = VParse.HTML_Value(htmlPage, "mp4-hls:{manifest:", ",videoCodec", false);
                string[] strArray = (await VParse.HTML_Load(manifestUrl, true)).Split('#');

                string chunkString = "";

                foreach (var line in strArray)
                {
                    switch (Model_Stream.Pro_Qualität_ID)
                    {
                        case 0:
                            if (strArray.Last().Contains(".m3u8"))
                            {
                                chunkString = strArray.Last()[strArray.Last().IndexOf("https:")..];
                                goto assign;
                            }
                            break;
                        case 1:
                            if (line.Contains("256x144"))
                            {
                                chunkString = line[line.IndexOf("https:")..];
                                goto assign;
                            }
                            break;
                        case 2:
                            if (line.Contains("768x432"))
                            {
                                chunkString = line[line.IndexOf("https:")..];
                                goto assign;
                            }
                            break;
                        case 3:
                            if (line.Contains("1280x720"))
                            {
                                chunkString = line[line.IndexOf("https:")..];
                                goto assign;
                            }
                            break;
                        case 4:
                            if (line.Contains("1920x1080"))
                            {
                                chunkString = line[line.IndexOf("https:")..];
                                goto assign;
                            }
                            break;
                    }
                }

            assign:
                Model_Stream.Pro_M3U8_Path = chunkString.Trim();
                Model_Stream.Pro_TS_Path = chunkString[..chunkString.IndexOf("index.m3u8")].Trim();
                Model_Stream.Pro_Preview_Image = VParse.HTML_Value(htmlPage, "videoHeight:432,location:", "}").Trim();
                Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray, chunkString);

                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Jerkmate.Stream_Adresses");
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

                using (WebResponse response = request.GetResponse())
                {
                    response.Close();
                    return 1;
                }
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
                if (string.IsNullOrEmpty(Model_Class.Pro_Model_Preview_Path))
                    return null;

                WebClient client = new();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                using Stream stream = client.OpenRead(Model_Class.Pro_Model_Preview_Path);
                return new Bitmap(Image.FromStream(stream));
            }
            catch
            {
                return null;
            }
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;

            Channel_Info info = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 8,
                Pro_Name = Model_Name
            };

            try
            {
                string result = await VParse.HTML_Load("https://de.jerkmate.com/cam/" + Model_Name.ToLower(), true);

                if (!string.IsNullOrEmpty(result))
                {
                    string genderCode = VParse.HTML_Value(result, "characteristic:{gender:", "}");

                    info.Pro_Gender = genderCode switch
                    {
                        "f" => 1,
                        "m" => 2,
                        "c" => 3,
                        "t" => 4,
                        _ => 0
                    };

                    info.Pro_Exist = genderCode.Length > 0;

                    if (info.Pro_Exist)
                    {
                        info.Pro_Country = VParse.HTML_Value(result, "Country:", ",");
                        info.Pro_Profil_Beschreibung = VParse.HTML_Value(result, "Headline:", ",Attr");
                        info.Pro_Languages = VParse.HTML_Value(result, "Languages:[", "],");

                        info.Pro_Online = await Online(info.Pro_Name) != 0;
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Jerkmate.Profil({Model_Name})");
                return info;
            }
        }
    }
}
