using System.Net;

namespace XstreaMonNET8
{
    internal static class Stripchat
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses modelStream)
        {
            try
            {
                await Task.CompletedTask;
                string json = VParse.Replace_Space(await VParse.HTML_Load("https://stripchat.com/api/front/v2/models/username/" + modelStream.Pro_Model_Name + "/cam"));
                if (string.IsNullOrEmpty(json))
                    return null;

                return await Read_Model_StreamAsync(Find_M3u8_Path(json), modelStream, json);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Stripchat.Stream_Adresses");
                return null;
            }
        }

        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses modelStream, string htmlText)
        {
            try
            {
                await Task.CompletedTask;
                return await Stream_Adresses(modelStream);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Stripchat.Stream_Adresses");
                return null;
            }
        }

        private static async Task<StreamAdresses> Read_Model_StreamAsync(string streamUrl, StreamAdresses modelStream, string txt)
        {
            try
            {
                if (streamUrl == null)
                    return null;

                string content = await VParse.HTML_Load(streamUrl, true);
                if (string.IsNullOrEmpty(content))
                    return null;

                string[] playlist = VParse.Replace_Space(content).Split('#');
                string chunk = Find_Chunk_File(playlist, modelStream.Pro_Qualität_ID)?.ToString();
                modelStream.Pro_Record_Resolution = Sites.Resolution_Find(playlist, chunk);
                modelStream.Pro_M3U8_Path = chunk?.Trim();
                modelStream.Pro_TS_Path = null;
                modelStream.Pro_Preview_Image = "https://img.strpst.com/thumbs/{0}/" + VParse.HTML_Value(txt, "streamName:", ",") + "_webp";

                return modelStream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Stripchat.Read_Model_Stream");
                return null;
            }
        }

        private static string Find_M3u8_Path(string html)
        {
            try
            {
                html = html.Replace("\"", "");
                if (html.Length == 0) return null;

                string streamName = VParse.HTML_Value(html, "streamName:", ",").Replace("\\/", "/");
                if (string.IsNullOrEmpty(streamName)) return null;

                return $"https://edge-hls.doppiocdn.com/hls/{streamName}/master/{streamName}_auto.m3u8?playlistType=lowLatency";
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Stripchat.Find_M3U8_Path");
                return null;
            }
        }

        private static object Find_Chunk_File(string[] m3u8File, int qualityId)
        {
            string chunkFile = "";
            int maxBandwidth = 0;

            try
            {
                foreach (string line in m3u8File)
                {
                    if (line.Contains(".m3u8"))
                    {
                        var streamInfo = new Stream_Info(line);

                        if (qualityId == 0 && streamInfo.Bandwith > maxBandwidth)
                        {
                            chunkFile = streamInfo.M3U8String;
                            maxBandwidth = streamInfo.Bandwith;
                        }
                        else if (qualityId == 1 && line.Contains("284x160")) return line[line.IndexOf("https://")..];
                        else if (qualityId == 2 && line.Contains("426x240")) return line[line.IndexOf("https://")..];
                        else if (qualityId == 3 && line.Contains("854x480")) return line[line.IndexOf("https://")..];
                        else if (qualityId == 4 && line.Contains("1280x720")) return line[line.IndexOf("https://")..];
                    }
                }

                if (string.IsNullOrEmpty(chunkFile) && m3u8File.Length > 1)
                {
                    foreach (string line in m3u8File)
                    {
                        if (line.Contains(".m3u8"))
                        {
                            var streamInfo = new Stream_Info(line);
                            if (streamInfo.Bandwith > maxBandwidth)
                            {
                                chunkFile = streamInfo.M3U8String;
                                maxBandwidth = streamInfo.Bandwith;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(chunkFile))
                    {
                        var last = m3u8File.LastOrDefault(l => l.Contains("https://"));
                        chunkFile = last[last.IndexOf("https://")..];
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Stripchat.Find_Chunk_File");
            }

            return chunkFile;
        }

        internal static async Task<int> Online(string modelName)
        {
            try
            {
                await Task.CompletedTask;

                var model = await Class_Model_List.Class_Model_Find(0, modelName);
                if (model != null && await Parameter.URL_Response(model.Pro_Model_M3U8)) return 1;

                string result = await VParse.HTML_Load($"https://stripchat.com/api/front/v2/models/username/{modelName}/cam");
                string text = VParse.Replace_Space(result).Replace("\"", "");

                if (text.Contains("isCamAvailable:true")) return 1;
                if (!text.Contains("show:null"))
                {
                    if (text.Contains("mode:groupShow")) return 5;
                    if (text.Contains("mode:virtualPrivate")) return 2;
                }

                return 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Stripchat.Online({modelName})");
                return 0;
            }
        }

        internal static async Task<Image> Image_FromWeb(Class_Model model)
        {
            await Task.CompletedTask;

            try
            {
                if (string.IsNullOrEmpty(model.Pro_Model_Preview_Path))
                    return null;

                string tempPath = Path.Combine(Parameter.CommonPath, "Temp");
                Directory.CreateDirectory(tempPath);

                long seconds = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                string path = Path.Combine(tempPath, $"{DateTime.Now.Ticks}.webp");

                using WebClient client = new();
                string downloadUrl = string.Format(model.Pro_Model_Preview_Path, seconds);
                client.DownloadFile(downloadUrl, path);

                if (File.Exists(path))
                {
                    WebPWrapper.WebP decoder = new();
                    Image img = decoder.Load(path);
                    File.Delete(path);
                    return img;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Stripchat.Image_FromWeb({model.Pro_Model_Name})");
            }

            return null;
        }

        internal static async Task<bool> IsGalerie(string url, string html, Class_Model model)
        {
            await Task.CompletedTask;
            return url.Contains("stripchat.com/collection") || url.Contains("/videos/") && html.Contains("video-player__video src=") && model != null;
        }

        internal static async void Galerie_Movie_Download(string url, string html, Class_Model model = null)
        {
            await Task.CompletedTask;

            try
            {
                string downloadUrl = "";
                string videoName = "";

                if (url.Contains("/videos/") && html.Contains(".mp4"))
                {
                    downloadUrl = VParse.HTML_Value(html, "video-player__video src=", "></video>").Replace("amp;", "");
                    if (await Parameter.URL_Response(downloadUrl))
                        videoName = VParse.HTML_Value(html, "div class=media-gallery__info-title>", "</div>");
                }

                string savePath = model?.Pro_Model_Directory ?? Modul_Ordner.Ordner_Pfad();
                if (string.IsNullOrEmpty(downloadUrl) || string.IsNullOrEmpty(savePath)) return;

                Dialog_Save dlg = new()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    TopMost = true,
                    Pro_Download_Path = downloadUrl,
                    Pro_Target_Path = savePath,
                    Pro_Class_Model = model,
                    Pro_Target_Name = videoName
                };

                dlg.Show();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Stripchat.Galerie_Movie_Download");
            }
        }

        internal static async Task<Channel_Info> Profil(string modelName)
        {
            try
            {
                await Task.CompletedTask;

                var channelInfo = new Channel_Info
                {
                    Pro_Exist = false,
                    Pro_Website_ID = 2,
                    Pro_Name = modelName
                };

                using var client = new HttpClient();
                var response = await client.GetStringAsync($"https://stripchat.com/api/front/v2/models/username/{modelName.ToLower()}/cam");

                string html = VParse.HTML_Value(response, "user:{user:{id:", "");
                if (html.Length == 0)
                    return channelInfo;

                string gender = VParse.HTML_Value(html, "broadcastGender:", ",");
                channelInfo.Pro_Gender = gender switch
                {
                    "female" => 1,
                    "male" => 2,
                    "group" => 3,
                    "tranny" => 4,
                    _ => 0
                };

                channelInfo.Pro_Exist = !string.IsNullOrEmpty(gender);

                if (channelInfo.Pro_Exist)
                {
                    string birthDate = VParse.HTML_Value(html, "birthDate:", ",");
                    if (birthDate != "null")
                        channelInfo.Pro_Birthday = birthDate;

                    string country = VParse.HTML_Value(html, "country:", ",");
                    string region = VParse.HTML_Value(html, "region:", ",");
                    string city = VParse.HTML_Value(html, "city:", ",");

                    if (!string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(region))
                        country += ", " + region;

                    if (!string.IsNullOrEmpty(city))
                        country += ", " + city;

                    channelInfo.Pro_Country = country;

                    string languages = VParse.HTML_Value(html, "languages:[", "]");
                    if (languages != "null")
                        channelInfo.Pro_Languages = languages;

                    string lastOnline = VParse.HTML_Value(html, "statusChangedAt:", "T");
                    if (!lastOnline.StartsWith("null"))
                        channelInfo.Pro_Last_Online = lastOnline;

                    channelInfo.Pro_Profil_Beschreibung = country;

                    string avatar = VParse.HTML_Value(html, "avatarUrlThumb:", ",").Trim();
                    if (await Parameter.URL_Response(avatar))
                    {
                        using var webClient = new WebClient();
                        using var stream = webClient.OpenRead(avatar);
                        if (stream != null)
                            channelInfo.Pro_Profil_Image = Image.FromStream(stream);
                    }

                    channelInfo.Pro_Online = (await Stripchat.Online(channelInfo.Pro_Name)) != 0;
                }

                return channelInfo;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Stripchat.Profil(Model_Name) = {modelName}");
                return null!;
            }
        }

    }
}
