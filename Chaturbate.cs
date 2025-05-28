using System.Net;
using System.Text.RegularExpressions;

namespace XstreaMonNET8
{
    internal static class Chaturbate
    {
        internal static DateTime Last_Many_Request;

        internal static string Site_URL(bool Affilante = true)
        {
            return !Affilante ? "https://chaturbate.com/" : "https://chaturbate.com/in/?tour=YrCt&campaign=NbZCW&track=default&room=";
        }

        internal static async Task<StreamAdresses> StreamAdresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;
                if (Last_Many_Request != DateTime.MinValue && DateTime.Compare(Last_Many_Request.AddSeconds(30.0), DateTime.Now) > 0 || await Online(Model_Stream.Pro_Model_Name) != 1)
                {
                    return null;
                }

                string result = await VParse.GetPOSTPHP("https://m.chaturbate.com/api/chatvideocontext/" + Model_Stream.Pro_Model_Name.ToLower());

                if (string.Compare(result, "429", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Last_Many_Request = DateTime.Now;
                    return null;
                }

                if (result == null)
                {
                    return null;
                }

                string m3U8Path = Find_M3U8_Path(result);
                return string.Compare(m3U8Path, "", StringComparison.OrdinalIgnoreCase) != 0 ? Read_Model_Stream(m3U8Path, Model_Stream, await VParse.GetPOSTPHP(m3U8Path)) : null;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Stream_Adresses(Model_Name) =" + Model_Stream.Pro_Model_Name.ToString());

                if (ex is WebException webEx && ((HttpWebResponse)webEx.Response)?.StatusCode == (HttpStatusCode)429)
                {
                    Last_Many_Request = DateTime.Now;
                    return null;
                }
                return null;
            }
        }

        internal static async Task<StreamAdresses> StreamAdresses(StreamAdresses Model_Stream, string HTML_Text)
        {
            try
            {
                await Task.CompletedTask;
                string m3U8Path = Find_M3U8_Path(HTML_Text);
                return Read_Model_Stream(m3U8Path, Model_Stream, await VParse.GetPOSTPHP(m3U8Path));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.M3u8_URL(Model_Name) =" + Model_Stream.Pro_Model_Name.ToString().ToLower());
                return null;
            }
        }

        private static StreamAdresses Read_Model_Stream(string Stream_URL, StreamAdresses Model_Stream, string TXT)
        {
            if (Stream_URL.Length > 0)
            {
                string audioPathFragment = "";

                if (string.Compare(TXT, "429", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Last_Many_Request = DateTime.Now;
                    return null;
                }
                else
                {
                    string[] strArray = TXT?.Split('#');

                    if (strArray != null)
                    {
                        foreach (string str in strArray.Reverse())
                        {
                            if (str.IndexOf("EXT-X-MEDIA:TYPE=AUDIO", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                audioPathFragment = str.Substring(str.IndexOf("URI=", StringComparison.OrdinalIgnoreCase) + 4).Replace("\"", "");
                                break;
                            }
                        }
                    }

                    string chunkFile = Find_Chunk_File(strArray, Model_Stream.Pro_Qualität_ID);

                    if (chunkFile.Length > 0)
                    {
                        if (Stream_URL.IndexOf("live-hls", StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            Model_Stream.Pro_M3U8_Path = (Stream_URL.Remove(Stream_URL.IndexOf("playlist_sfm4s.m3u8", StringComparison.OrdinalIgnoreCase)) + chunkFile).Trim();
                            Model_Stream.Pro_TS_Path = Stream_URL.Remove(Stream_URL.IndexOf("playlist_sfm4s.m3u8", StringComparison.OrdinalIgnoreCase)).Trim();
                            Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray, chunkFile);
                            Model_Stream.Pro_Audio_Path = Stream_URL.Remove(Stream_URL.IndexOf("playlist_sfm4s.m3u8", StringComparison.OrdinalIgnoreCase)).Trim() + audioPathFragment.Trim();
                            Model_Stream.Pro_AU_Path = Stream_URL.Remove(Stream_URL.IndexOf("playlist_sfm4s.m3u8", StringComparison.OrdinalIgnoreCase)).Trim();
                            Model_Stream.Pro_Preview_Image = "https://jpeg.live.mmcdn.com/stream?room=" + Model_Stream.Pro_Model_Name.ToLower();
                        }
                        else
                        {
                            Model_Stream.Pro_M3U8_Path = (Stream_URL.Remove(Stream_URL.IndexOf("playlist.m3u8", StringComparison.OrdinalIgnoreCase)) + chunkFile).Trim();
                            Model_Stream.Pro_TS_Path = Stream_URL.Remove(Stream_URL.IndexOf("playlist.m3u8", StringComparison.OrdinalIgnoreCase)).Trim();
                            Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray, chunkFile);
                            Model_Stream.Pro_Audio_Path = null;
                            Model_Stream.Pro_AU_Path = null;
                            Model_Stream.Pro_Preview_Image = "https://jpeg.live.mmcdn.com/stream?room=" + Model_Stream.Pro_Model_Name.ToLower();
                        }
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
            }
            else if (TXT.IndexOf("Accessdenied", StringComparison.OrdinalIgnoreCase) > -1)
            {
                Model_Stream.Pro_M3U8_Path = null;
                Model_Stream.Pro_TS_Path = null;
                Model_Stream.Pro_Access_Denied = true;
                return Model_Stream;
            }
            else
            {
                if (string.Compare(TXT, "429", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Last_Many_Request = DateTime.Now;
                }
                return null!;
            }
        }

        internal static string Find_M3U8_Path(string HTML_String)
        {
            try
            {
                if (HTML_String != null && HTML_String.IndexOf("https://edge", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    HTML_String = HTML_String.Substring(HTML_String.IndexOf("https://edge", StringComparison.OrdinalIgnoreCase));
                    HTML_String = HTML_String.Substring(0, HTML_String.IndexOf(".m3u8", StringComparison.OrdinalIgnoreCase) + 5);

                    if (HTML_String.Length < 20)
                    {
                        HTML_String = "";
                        MessageBox.Show("Sorry, the model is offline or does not exist!");
                    }

                    HTML_String = HTML_String.Replace("\"", "");
                    HTML_String = HTML_String.Replace("/live\\u002Dhls/", "/live-hls/");
                    HTML_String = HTML_String.Replace("\\u002Dhls/", "");
                    HTML_String = HTML_String.Replace("\\u002D", "-");
                    HTML_String = HTML_String.Replace("\\u003D", "=");
                    HTML_String = HTML_String.Replace("\\u0022", "");
                    HTML_String = HTML_String.Replace("\\u0026", "&");

                    if (!Parameter.URL_Response(HTML_String).Result)
                    {
                        HTML_String = HTML_String.Replace("live-hls", "live-c-fhls");
                        HTML_String = HTML_String.Replace("playlist.m3u8", "playlist_sfm4s.m3u8");
                    }
                    return HTML_String;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Find_M3U8_Path");
                return "";
            }
        }

        internal static string Find_Chunk_File(string[] m3U8_File, int Qualität_ID)
        {
            string chunkFile = "";
            try
            {
                int num = 0;

                foreach (string HTML_Page in m3U8_File)
                {
                    switch (Qualität_ID)
                    {
                        case 0:
                            int currentBandwidth = ValueBack.Get_CInteger(VParse.HTML_Value(HTML_Page, "BANDWIDTH=", ","));
                            if (currentBandwidth > num)
                            {
                                chunkFile = HTML_Page.Substring(HTML_Page.IndexOf("chunk", StringComparison.OrdinalIgnoreCase));
                                num = currentBandwidth;
                            }
                            break;
                        case 1:
                            if (HTML_Page.IndexOf("426x240", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                chunkFile = HTML_Page.Substring(HTML_Page.IndexOf("chunk", StringComparison.OrdinalIgnoreCase));
                                goto Label_ExitLoop;
                            }
                            break;
                        case 2:
                            if (HTML_Page.IndexOf("960x540", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                chunkFile = HTML_Page.Substring(HTML_Page.IndexOf("chunk", StringComparison.OrdinalIgnoreCase));
                                goto Label_ExitLoop;
                            }
                            break;
                        case 3:
                            if (HTML_Page.IndexOf("1280x720", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                chunkFile = HTML_Page.Substring(HTML_Page.IndexOf("chunk", StringComparison.OrdinalIgnoreCase));
                                goto Label_ExitLoop;
                            }
                            break;
                        case 4:
                            if (HTML_Page.IndexOf("1920x1080", StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                chunkFile = HTML_Page.Substring(HTML_Page.IndexOf("chunk", StringComparison.OrdinalIgnoreCase));
                                goto Label_ExitLoop;
                            }
                            break;
                    }
                }

            Label_ExitLoop:

                if (chunkFile.Length == 0)
                {
                    if (m3U8_File.LastOrDefault()?.IndexOf("chunk", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        chunkFile = m3U8_File.Last().Substring(m3U8_File.Last().IndexOf("chunk", StringComparison.OrdinalIgnoreCase));
                    }
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
                Parameter.Error_Message(ex, "Chaturbate.Online - " + Model_Name);
                return 0;
            }
        }

        internal static async Task<bool> IsLogin(string HTML)
        {
            await Task.CompletedTask;
            return HTML.IndexOf("Menu data-testid=username", StringComparison.OrdinalIgnoreCase) > -1;
        }

        internal static async Task<int> Tokens()
        {
            await Task.CompletedTask;
            string result = await VParse.GetPOSTPHP("https://chaturbate.com/tipping/current_tokens/");
            string[] parts = result.Split(':');
            if (parts.Length <= 1)
            {
                return 0;
            }
            return ValueBack.Get_CInteger(parts[1].Replace("}", ""));
        }

        internal static Image Image_FromWeb(string Model_Name)
        {
            Bitmap bitmap = null;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        byte[] buffer = webClient.DownloadData("https://jpeg.live.mmcdn.com/stream?room=" + Model_Name.ToLower());
                        if (buffer.Length > 0)
                        {
                            using (MemoryStream memoryStream = new MemoryStream(buffer))
                            {
                                bitmap = (Bitmap)Image.FromStream(memoryStream);
                            }
                        }
                        if (bitmap != null)
                        {
                            if (bitmap.Size == new Size(360, 202))
                            {
                                bitmap = null;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                bitmap = null;
            }
            return bitmap;
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;
            Channel_Info channelInfo = new Channel_Info()
            {
                Pro_Exist = false,
                Pro_Website_ID = 0,
                Pro_Name = Model_Name
            };

            try
            {
                if (Model_Name.Length > 0)
                {
                    string str = Regex.Unescape(await VParse.GetPOSTPHP("https://chaturbate.com/api/biocontext/" + Model_Name.ToLower() + "/?"));

                    if (string.Compare(str, "429", StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        string gender = VParse.HTML_Value(str, "sex:", ",").Trim();
                        channelInfo.Pro_Gender = string.Compare(gender, "a woman", StringComparison.OrdinalIgnoreCase) == 0 ? 1 :
                                                 string.Compare(gender, "a man", StringComparison.OrdinalIgnoreCase) == 0 ? 2 :
                                                 string.Compare(gender, "a couple", StringComparison.OrdinalIgnoreCase) == 0 ? 3 :
                                                 string.Compare(gender, "trans", StringComparison.OrdinalIgnoreCase) == 0 ? 4 : 0;
                        channelInfo.Pro_Exist = gender.Length > 0;

                        if (channelInfo.Pro_Exist)
                        {
                            channelInfo.Pro_Country = VParse.HTML_Value(str, "location:", ",").Trim();
                            channelInfo.Pro_Languages = VParse.HTML_Value(str, "languages:", ", p").Trim();
                            channelInfo.Pro_Last_Online = VParse.HTML_Value(str, "last_broadcast:", "T").Trim();
                            string birthday = VParse.HTML_Value(str, "display_birthday\": \"", "\"", false);
                            birthday = birthday.Replace("Jan.", "Januar");
                            birthday = birthday.Replace("Febr.", "Februar");
                            birthday = birthday.Replace("Sept.", "September");
                            birthday = birthday.Replace("Okt.", "Oktober");
                            birthday = birthday.Replace("Nov.", "November");
                            birthday = birthday.Replace("Dec.", "December");
                            channelInfo.Pro_Birthday = birthday;

                            channelInfo.Pro_Profil_Beschreibung = channelInfo.Pro_Country;

                            if (!DateTime.TryParse(channelInfo.Pro_Birthday.ToString(), out _))
                            {
                                channelInfo.Pro_Birthday = null;
                            }
                        }
                    }
                    else
                    {
                        channelInfo.Pro_Exist = true;
                    }
                    channelInfo.Pro_Online = await Online(channelInfo.Pro_Name) != 0;
                }
                return channelInfo;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Profil");
                return channelInfo;
            }
        }

        internal static async Task<bool> IsGalerie(string URL_String, string Html_String, Class_Model Model_Class)
        {
            await Task.CompletedTask;
            return Html_String.IndexOf("https://s3v.highwebmedia.com", StringComparison.OrdinalIgnoreCase) > -1 && Model_Class != null || URL_String.IndexOf("chaturbate.com/my_collection/detail", StringComparison.OrdinalIgnoreCase) > -1;
        }

        internal static async void Galerie_Movie_Download(string URL_String, string HTML_String, Class_Model Model_Class = null)
        {
            await Task.CompletedTask;
            URL_String = VParse.URL_Check(URL_String);
            try
            {
                string downloadUrl = "";
                string targetPath = "";
                string targetName = "";

                if (URL_String.IndexOf("chaturbate.com/my_collection/detail", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    if (HTML_String.IndexOf(".mp4", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        string siteUrl = "https://pvr.highwebmedia.com" + VParse.HTML_Value(HTML_String, "src=https://pvr.highwebmedia.com", "></video>").Replace("amp;", "");
                        if (await Parameter.URL_Response(siteUrl))
                        {
                            targetPath = Modul_Ordner.Ordner_Pfad();
                            downloadUrl = siteUrl;
                        }
                    }
                }
                else if (HTML_String.IndexOf("https://s3v.highwebmedia.com", StringComparison.OrdinalIgnoreCase) > -1 && Model_Class != null)
                {
                    string s3vUrl = "https://s3v" + VParse.HTML_Value(HTML_String, "src=https://s3v", "></video>").Replace("amp;", "");
                    if (await Parameter.URL_Response(s3vUrl))
                    {
                        downloadUrl = s3vUrl;
                        targetPath = Model_Class.Pro_Model_Directory;
                        targetName = VParse.HTML_Value(s3vUrl, "transcoded/", ".mp4");
                    }
                }

                if (targetPath.Length > 0 && downloadUrl.Length > 0)
                {
                    Dialog_Save dialogSave = new Dialog_Save
                    {
                        StartPosition = FormStartPosition.CenterParent,
                        TopMost = true,
                        Pro_Download_Path = downloadUrl,
                        Pro_Target_Path = targetPath,
                        Pro_Class_Model = Model_Class,
                        Pro_Target_Name = targetName
                    };
                    dialogSave.Show();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Chaturbate.Galerie_Movie_Download");
            }
        }
    }
}
