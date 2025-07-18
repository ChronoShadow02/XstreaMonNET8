using System.Text.RegularExpressions;

namespace XstreaMonNET8
{
    internal static class Chaturbate
    {
        internal static DateTime Last_Many_Request;

        private static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        internal static string Site_URL(bool Affilante = true)
        {
            return !Affilante ? "https://chaturbate.com/" : "https://chaturbate.com/in/?tour=YrCt&campaign=NbZCW&track=default&room=";
        }

        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;

                if ((Last_Many_Request != DateTime.MinValue && Last_Many_Request.AddSeconds(30) > DateTime.Now) ||
                    await Online(Model_Stream.Pro_Model_Name).ConfigureAwait(false) != 1)
                    return null;

                string result = await VParse.GetPOSTPHP($"https://m.chaturbate.com/api/chatvideocontext/{Model_Stream.Pro_Model_Name.ToLower()}").ConfigureAwait(false);
                if (result == "429")
                {
                    Last_Many_Request = DateTime.Now;
                    return null;
                }

                if (result == null)
                    return null;

                string m3U8Path = Find_M3U8_Path(result);
                return !string.IsNullOrEmpty(m3U8Path)
                    ? Read_Model_Stream(m3U8Path, Model_Stream, await VParse.GetPOSTPHP(m3U8Path).ConfigureAwait(false))
                    : null;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Chaturbate.Stream_Adresses(Model_Name) = {Model_Stream.Pro_Model_Name}");
                return null;
            }
        }

        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream, string HTML_Text)
        {
            try
            {
                await Task.CompletedTask;
                string m3U8Path = Find_M3U8_Path(HTML_Text);
                return Read_Model_Stream(m3U8Path, Model_Stream, await VParse.GetPOSTPHP(m3U8Path).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Chaturbate.M3u8_URL(Model_Name) = {Model_Stream.Pro_Model_Name.ToLower()}");
                return null;
            }
        }

        private static StreamAdresses Read_Model_Stream(string Stream_URL, StreamAdresses Model_Stream, string TXT)
        {
            if (string.IsNullOrEmpty(Stream_URL))
            {
                if (TXT.Contains("Accessdenied"))
                {
                    Model_Stream.Pro_M3U8_Path = null;
                    Model_Stream.Pro_TS_Path = null;
                    Model_Stream.Pro_Access_Denied = true;
                    return Model_Stream;
                }
                if (TXT == "429")
                {
                    Last_Many_Request = DateTime.Now;
                }
                return null;
            }

            if (TXT == "429")
            {
                Last_Many_Request = DateTime.Now;
                return null;
            }

            string[] playlist = TXT?.Split('#');
            string audioPath = playlist?.Reverse().FirstOrDefault(line => line.Contains("EXT-X-MEDIA:TYPE=AUDIO"))?
                .Split(new[] { "URI=" }, StringSplitOptions.None)[1].Replace("\"", "");

            string chunkFile = Find_Chunk_File(playlist, Model_Stream.Pro_Qualität_ID);

            if (!string.IsNullOrEmpty(chunkFile))
            {
                string baseUrl;
                if (!Stream_URL.Contains("live-hls"))
                {
                    baseUrl = Stream_URL.Substring(0, Stream_URL.IndexOf("playlist_sfm4s.m3u8"));
                }
                else
                {
                    baseUrl = Stream_URL.Substring(0, Stream_URL.IndexOf("playlist.m3u8"));
                }

                Model_Stream.Pro_M3U8_Path = $"{baseUrl}{chunkFile}".Trim();
                Model_Stream.Pro_TS_Path = baseUrl.Trim();
                Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(playlist, chunkFile);
                Model_Stream.Pro_Audio_Path = !string.IsNullOrEmpty(audioPath) ? $"{baseUrl}{audioPath.Trim()}" : null;
                Model_Stream.Pro_AU_Path = baseUrl.Trim();
                Model_Stream.Pro_Preview_Image = $"https://jpeg.live.mmcdn.com/stream?room={Model_Stream.Pro_Model_Name.ToLower()}";
            }
            else
            {
                Model_Stream.Pro_M3U8_Path = null;
                Model_Stream.Pro_TS_Path = null;
                Model_Stream.Pro_Record_Resolution = 0;
                Model_Stream.Pro_Audio_Path = null;
                Model_Stream.Pro_AU_Path = null;
                Model_Stream.Pro_Preview_Image = null;
            }

            return Model_Stream;
        }

        internal static string Find_M3U8_Path(string HTML_String)
        {
            try
            {
                if (!string.IsNullOrEmpty(HTML_String) && HTML_String.Contains("https://edge"))
                {
                    HTML_String = HTML_String.Substring(HTML_String.IndexOf("https://edge"));
                    HTML_String = HTML_String.Substring(0, HTML_String.IndexOf(".m3u8") + 5);
                    if (HTML_String.Length < 20)
                    {
                        MessageBox.Show("Sorry, the model is offline or does not exist!");
                        return "";
                    }

                    HTML_String = HTML_String.Replace("\"", "")
                                             .Replace("/live\\u002Dhls/", "/live-hls/")
                                             .Replace("\\u002Dhls/", "")
                                             .Replace("\\u002D", "-")
                                             .Replace("\\u003D", "=")
                                             .Replace("\\u0022", "")
                                             .Replace("\\u0026", "&");

                    if (!Parameter.URL_Response(HTML_String).Result)
                    {
                        HTML_String = HTML_String.Replace("live-hls", "live-c-fhls")
                                                 .Replace("playlist.m3u8", "playlist_sfm4s.m3u8");
                    }

                    return HTML_String;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Find_M3U8_Path");
            }
            return "";
        }

        internal static string Find_Chunk_File(string[] m3U8_File, int Qualität_ID)
        {
            string chunkFile = "";
            int maxBandwidth = 0;

            try
            {
                foreach (string line in m3U8_File)
                {
                    if (Qualität_ID == 0)
                    {
                        int bandwidth = ValueBack.Get_CInteger(VParse.HTML_Value(line, "BANDWIDTH=", ","));
                        if (bandwidth > maxBandwidth)
                        {
                            chunkFile = line.Substring(line.IndexOf("chunk"));
                            maxBandwidth = bandwidth;
                        }
                    }
                    else if ((Qualität_ID == 1 && line.Contains("426x240")) ||
                             (Qualität_ID == 2 && line.Contains("960x540")) ||
                             (Qualität_ID == 3 && line.Contains("1280x720")) ||
                             (Qualität_ID == 4 && line.Contains("1920x1080")))
                    {
                        chunkFile = line.Substring(line.IndexOf("chunk"));
                        break;
                    }
                }

                if (string.IsNullOrEmpty(chunkFile))
                {
                    string lastLine = m3U8_File.LastOrDefault(l => l.Contains("chunk"));
                    if (lastLine != null)
                        chunkFile = lastLine.Substring(lastLine.IndexOf("chunk"));
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Find_Chunk_File");
            }
            return chunkFile;
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                await Task.CompletedTask;
                return Image_FromWeb(Model_Name?.ToLower()) == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Chaturbate.Online - {Model_Name}");
                return 0;
            }
        }

        internal static async Task<bool> IsLogin(string HTML)
        {
            await Task.CompletedTask;
            return HTML.Contains("Menu data-testid=username");
        }

        internal static async Task<int> Tokens()
        {
            await Task.CompletedTask;
            string result = await VParse.GetPOSTPHP("https://chaturbate.com/tipping/current_tokens/").ConfigureAwait(false);
            var parts = result?.Split(':');
            if (parts == null || parts.Length <= 1)
                return 0;
            return ValueBack.Get_CInteger(parts[1].Replace("}", ""));
        }

        internal static Image Image_FromWeb(string Model_Name)
        {
            try
            {
                byte[] buffer = httpClient.GetByteArrayAsync($"https://jpeg.live.mmcdn.com/stream?room={Model_Name.ToLower()}").Result;
                using var ms = new MemoryStream(buffer);
                Image img = Image.FromStream(ms);
                return img.Size == new Size(360, 202) ? null : img;
            }
            catch
            {
                return null;
            }
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;
            Channel_Info channelInfo1 = new Channel_Info
            {
                Pro_Exist = false,
                Pro_Website_ID = 0,
                Pro_Name = Model_Name
            };

            try
            {
                if (!string.IsNullOrEmpty(Model_Name))
                {
                    string str = Regex.Unescape(await VParse.GetPOSTPHP($"https://chaturbate.com/api/biocontext/{Model_Name.ToLower()}/?").ConfigureAwait(false));
                    if (str != "429")
                    {
                        string gender = VParse.HTML_Value(str, "sex:", ",").Trim();
                        channelInfo1.Pro_Gender = gender switch
                        {
                            "a woman" => 1,
                            "a man" => 2,
                            "a couple" => 3,
                            "trans" => 4,
                            _ => 0
                        };
                        channelInfo1.Pro_Exist = !string.IsNullOrEmpty(gender);

                        if (channelInfo1.Pro_Exist)
                        {
                            channelInfo1.Pro_Country = VParse.HTML_Value(str, "location:", ",").Trim();
                            channelInfo1.Pro_Languages = VParse.HTML_Value(str, "languages:", ", p").Trim();
                            channelInfo1.Pro_Last_Online = VParse.HTML_Value(str, "last_broadcast:", "T").Trim();
                            channelInfo1.Pro_Birthday = VParse.HTML_Value(str, "display_birthday\": \"", "\"", false);
                            channelInfo1.Pro_Profil_Beschreibung = channelInfo1.Pro_Country;
                        }
                    }
                    else
                    {
                        channelInfo1.Pro_Exist = true;
                    }
                    channelInfo1.Pro_Online = await Online(channelInfo1.Pro_Name).ConfigureAwait(false) != 0;
                }
                return channelInfo1;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Profil");
                return channelInfo1;
            }
        }

        internal static async Task<bool> IsGalerie(string URL_String, string Html_String, Class_Model Model_Class)
        {
            await Task.CompletedTask;
            return Html_String.Contains("https://s3v.highwebmedia.com") && Model_Class != null ||
                   URL_String.Contains("chaturbate.com/my_collection/detail");
        }

        internal static async void Galerie_Movie_Download(string URL_String, string HTML_String, Class_Model Model_Class = null)
        {
            await Task.CompletedTask;
            URL_String = VParse.URL_Check(URL_String);
            try
            {
                string downloadUrl = "";
                string savePath = "";
                string fileName = "";

                if (URL_String.Contains("chaturbate.com/my_collection/detail"))
                {
                    if (HTML_String.Contains(".mp4"))
                    {
                        string siteUrl = "https://pvr.highwebmedia.com" + VParse.HTML_Value(HTML_String, "src=https://pvr.highwebmedia.com", "></video>").Replace("amp;", "");
                        if (await Parameter.URL_Response(siteUrl).ConfigureAwait(false))
                        {
                            savePath = Modul_Ordner.Ordner_Pfad();
                            downloadUrl = siteUrl;
                        }
                    }
                }
                else if (HTML_String.Contains("https://s3v.highwebmedia.com") && Model_Class != null)
                {
                    string str4 = "https://s3v" + VParse.HTML_Value(HTML_String, "src=https://s3v", "></video>").Replace("amp;", "");
                    if (await Parameter.URL_Response(str4).ConfigureAwait(false))
                    {
                        downloadUrl = str4;
                        savePath = Model_Class.Pro_Model_Directory;
                        fileName = VParse.HTML_Value(str4, "transcoded/", ".mp4");
                    }
                }

                if (string.IsNullOrEmpty(downloadUrl) || string.IsNullOrEmpty(savePath))
                    return;

                Dialog_Save dialogSave = new Dialog_Save
                {
                    StartPosition = FormStartPosition.CenterParent,
                    TopMost = true,
                    Pro_Download_Path = downloadUrl,
                    Pro_Target_Path = savePath,
                    Pro_Class_Model = Model_Class,
                    Pro_Target_Name = fileName
                };
                dialogSave.Show();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Galerie_Movie_Download");
            }
        }
    }
}
