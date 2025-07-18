using System.Net;

namespace XstreaMonNET8
{
    internal static class Bongacams
    {
        internal static DateTime BongaCams_Last_Many_Request;

        internal static async Task<StreamAdresses> StreamAdresses(StreamAdresses modelStream)
        {
            try
            {
                await Task.CompletedTask;

                if (BongaCams_Last_Many_Request != DateTime.MinValue &&
                    BongaCams_Last_Many_Request.AddSeconds(30) > DateTime.Now)
                {
                    return null;
                }

                string result = VParse.Chrome_Load("https://en.bongacams.com/" + modelStream.Pro_Model_Name, true).Result!;

                return Read_Model_Stream(Find_M3U8_Path(result, modelStream), modelStream, result);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Bongacams.StreamAdresses(Model_Name) = {modelStream.Pro_Model_Name}");
                BongaCams_Last_Many_Request = DateTime.Now;
                return null;
            }
        }

        internal static async Task<StreamAdresses> StreamAdresses(StreamAdresses modelStream, string htmlText)
        {
            try
            {
                await Task.CompletedTask;

                string cleanedHtml = VParse.Replace_Space(htmlText);
                return Read_Model_Stream(Find_M3U8_Path(cleanedHtml, modelStream), modelStream, cleanedHtml);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Bongacams.StreamAdresses (Model_Name) = {modelStream.Pro_Model_Name}");
                return null;
            }
        }

        private static StreamAdresses Read_Model_Stream(string streamUrl, StreamAdresses modelStream, string txt)
        {
            try
            {
                if (!string.IsNullOrEmpty(streamUrl))
                {
                    string[] streamParts = VParse.Chrome_Load(streamUrl, true).Result.Split('#');

                    if (streamParts.Length > 1)
                    {
                        string chunkFile = Find_Chunk_File(streamParts, modelStream.Pro_Qualität_ID);
                        string basePath = streamUrl[..streamUrl.IndexOf("playlist.m3u8")];

                        modelStream.Pro_M3U8_Path = (basePath + chunkFile).Trim();
                        modelStream.Pro_TS_Path = (basePath + chunkFile).Replace("chunks.m3u8", "").Trim();
                        modelStream.Pro_Record_Resolution = Sites.Resolution_Find(streamParts, chunkFile);

                        txt = VParse.Replace_Space(txt);

                        if (txt.Contains("frame:"))
                        {
                            int startIndex = txt.IndexOf("frame:") + "frame:".Length;
                            int endIndex = txt.IndexOf("?t=", startIndex);
                            modelStream.Pro_Preview_Image = "https:" + txt[startIndex..endIndex].Replace("\\", "");
                        }
                        else
                        {
                            modelStream.Pro_Preview_Image = null;
                        }
                    }
                    else
                    {
                        Parameter.Error_Message(null, "Bongacams.Read_Model_Stream Result:" + streamParts[0]);
                        BongaCams_Last_Many_Request = DateTime.Now;
                    }
                }

                return modelStream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Bongacams.Read_Model_Stream (Model_Name) = {modelStream.Pro_Model_Name}");
                return null;
            }
        }

        internal static string Find_Chunk_File(string[] m3u8Lines, int calidadId)
        {
            try
            {
                string result = "";

                // Selección basada en calidad
                foreach (string line in m3u8Lines)
                {
                    switch (calidadId)
                    {
                        case 0:
                            if (m3u8Lines.LastOrDefault()?.Contains("chunks.m3u8") == true)
                            {
                                result = m3u8Lines.Last().Substring(m3u8Lines.Last().IndexOf("public-aac"));
                                goto Return;
                            }
                            break;

                        case 1:
                            if (line.Contains("426x240"))
                            {
                                result = line.Substring(line.IndexOf("public-aac"));
                                goto Return;
                            }
                            break;

                        case 2:
                            if (line.Contains("854x480"))
                            {
                                result = line.Substring(line.IndexOf("public-aac"));
                                goto Return;
                            }
                            break;

                        case 3:
                            if (line.Contains("1280x720"))
                            {
                                result = line.Substring(line.IndexOf("public-aac"));
                                goto Return;
                            }
                            break;

                        case 4:
                            if (line.Contains("1920x1080"))
                            {
                                result = line.Substring(line.IndexOf("public-aac"));
                                goto Return;
                            }
                            break;
                    }
                }

                // Si no se encontró nada específico, se elige el stream con mayor ancho de banda
                if (string.IsNullOrEmpty(result) && m3u8Lines.Length > 1)
                {
                    int maxBandwith = 0;
                    foreach (var line in m3u8Lines)
                    {
                        if (line.Contains(".m3u8"))
                        {
                            var info = new Stream_Info(line);
                            if (info.Bandwith > maxBandwith)
                            {
                                result = line.Substring(line.IndexOf("public-aac"));
                                maxBandwith = info.Bandwith;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        var last = m3u8Lines.Last();
                        result = last.Substring(last.IndexOf("public-aac"));
                    }
                }

            Return:
                return result;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Bongacams.Find_Chunk_File");
                return null;
            }
        }


        internal static string Find_M3U8_Path(string html, StreamAdresses modelStream)
        {
            try
            {
                html = html.Replace("\"", "");

                if (!string.IsNullOrEmpty(html) && html.Contains("mobile-edge"))
                {
                    string segment = VParse.HTML_Value(html, "mobile-edge", ".bcvcdn.com", false);
                    string username = VParse.HTML_Value(html, "{username:", ",", false);

                    if (!string.IsNullOrEmpty(segment))
                    {
                        return $"https://live-edge{segment}.bcvcdn.com/hls/stream_{username}/playlist.m3u8";
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Bongacams.Find_M3U8_Path (Model_Name) = {modelStream.Pro_Model_Name}");
                return "";
            }
        }


        internal static async Task<int> Online(string modelName)
        {
            try
            {
                await Task.CompletedTask;

                var model = await Class_Model_List.Class_Model_Find(0, modelName);
                var url = model?.Pro_Model_M3U8;

                bool isOnline = await Parameter.URL_Response(url);
                if (isOnline)
                    return 1;

                string html = await VParse.Chrome_Load($"https://en.bongacams.com/{modelName}", true);
                return Bongacams.Online_HTML(html);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Bongacams.Online (Model_Name) = {modelName}");
                return 0;
            }
        }


        internal static async Task<int> Online(string modelName, string html)
        {
            await Task.CompletedTask;
            return Bongacams.Online_HTML(html);
        }

        private static int Online_HTML(string html)
        {
            try
            {
                if (!string.IsNullOrEmpty(html) && html.Contains("mobile-edge"))
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Bongacams.Online_HTML");
                return 0;
            }
        }

        internal static async Task<Image?> Image_FromWeb(Class_Model model)
        {
            await Task.CompletedTask;
            try
            {
                if (string.IsNullOrEmpty(model.Pro_Model_Preview_Path))
                    return null;

                using (HttpClient httpClient = new HttpClient())
                {
                    byte[] data = await httpClient.GetByteArrayAsync(model.Pro_Model_Preview_Path);
                    using MemoryStream stream = new(data);
                    return Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Bongacams.Image_FromWeb (Model_Name) = {model.Pro_Model_Preview_Path}");
                return null;
            }
        }

        internal static async Task<Channel_Info> Profil(string modelName)
        {
            Channel_Info info = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 3
            };

            try
            {
                string result = await VParse.Chrome_Load($"https://en.bongacams.mobi/profile/{modelName.ToLower()}", true);

                if (result.Length <= 4)
                    return info;

                string gender = VParse.HTML_Value(result, "gender:", ",").ToLower();

                info.Pro_Gender = gender switch
                {
                    "female" => 1,
                    "male" => 2,
                    var g when g.StartsWith("couple") => 3,
                    "trans" => 4,
                    _ => 0
                };

                info.Pro_Exist = gender.Length > 0 && gender.Length < 20;

                if (!info.Pro_Exist)
                    return info;

                info.Pro_Name = VParse.HTML_Value(result, ",username:", ",");
                info.Pro_Profil_Beschreibung = VParse.HTML_Value(result, $"descriptioncontent=Model{info.Pro_Name}:", "/>");
                info.Pro_Last_Online = VParse.HTML_Value(result, "class=la_date>", "</span>").Trim();
                info.Pro_Country = VParse.HTML_Value(result, "Hometown:,value:[", "}");

                string speaks = VParse.HTML_Value(result, "speaks:", ";");
                if (!string.Equals(speaks, "null", StringComparison.OrdinalIgnoreCase))
                    info.Pro_Languages = speaks;

                string imageUrl = VParse.HTML_Value(result, "imagecontent=", "/>").Trim();

                if (await Parameter.URL_Response(imageUrl))
                {
                    using (HttpClient httpClient = new HttpClient())
                    using (Stream stream = await httpClient.GetStreamAsync(imageUrl))
                    {
                        info.Pro_Profil_Image = Image.FromStream(stream);
                    }
                }

                info.Pro_Online = await Bongacams.Online(info.Pro_Name) != 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Bongacams.Profil (Model_Name) = {modelName}");
            }

            return info;
        }
    }

}
