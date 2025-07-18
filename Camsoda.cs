namespace XstreaMonNET8
{
    internal static class Camsoda
    {
        internal static async Task<StreamAdresses> StreamAdresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;
                string ChunkString = "";
                string str1 = "";
                Task<int> task = Online(Model_Stream.Pro_Model_Name.Trim().ToLower());
                if ((task != null ? task.Result != 1 ? 1 : 0 : 0) != 0)
                    return null;

                string result1 = VParse.GetPOSTPHP("https://m.camsoda.com/api/v1/video/vtoken/" + Model_Stream.Pro_Model_Name.Trim().ToLower()).Result;
                string HTML_Page1 = (string.IsNullOrEmpty(result1) ? "" : result1).Replace("\"", "");

                if (HTML_Page1.Length > 0 && HTML_Page1.IndexOf("edge_servers:[]") > -1)
                    return null;

                string str2 = VParse.HTML_Value(HTML_Page1, "edge_servers:[", "]", false);
                string str3 = VParse.HTML_Value(HTML_Page1, "stream_name:", ",", false).Replace("\\/", "/");
                string str4 = VParse.HTML_Value(HTML_Page1, "token:", ",", false);

                if (str2.Split(',').Length <= 1)
                    return null;

                string str5 = str2.Split(',')[1].Replace("\\/", "/");
                string str6 = "https://" + str5 + "/" + str3 + "_v1/";
                string result2 = VParse.GetPOSTPHP("https://" + str5 + "/" + str3 + "_v1/index.m3u8?token=" + str4).Result;

                if (result2 == null)
                    return null;

                string[] strArray1 = VParse.Replace_Space(result2).Split('#');
                foreach (string str7 in strArray1)
                {
                    if (str7.Contains("EXT-X-MEDIA:TYPE=AUDIO"))
                    {
                        str1 = str7.Substring(str7.IndexOf("DEFAULT=YES,URI=") + 16);
                        break;
                    }
                }

                foreach (string str8 in strArray1)
                {
                    switch (Model_Stream.Pro_Qualität_ID)
                    {
                        case 0:
                            if (strArray1.Last().Contains(".m3u8"))
                            {
                                ChunkString = strArray1.Last().Substring(strArray1.Last().IndexOf("tracks"));
                                goto label_29;
                            }
                            break;
                        case 1:
                            if (str8.Contains("426x240"))
                            {
                                ChunkString = str8.Substring(str8.IndexOf("tracks"));
                                goto label_29;
                            }
                            break;
                        case 2:
                            if (str8.Contains("854x480"))
                            {
                                ChunkString = str8.Substring(str8.IndexOf("tracks"));
                                goto label_29;
                            }
                            break;
                        case 3:
                        case 4:
                            if (str8.Contains("1280x720"))
                            {
                                ChunkString = str8.Substring(str8.IndexOf("tracks"));
                                goto label_29;
                            }
                            break;
                    }
                }

            label_29:
                Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray1, ChunkString);
                Model_Stream.Pro_FFMPEG_Path = "https://" + str5 + "/" + str3 + "_v1/index.m3u8?token=" + str4;
                Model_Stream.Pro_M3U8_Path = ChunkString.Length <= 0 ? null : str6.Trim() + ChunkString.Trim();

                if (ChunkString.Contains("mono.m3u8"))
                {
                    Model_Stream.Pro_TS_Path = str6.Trim() + ChunkString.Substring(0, ChunkString.IndexOf("mono.m3u8")).Trim();
                }
                else if (ChunkString.Contains("fmp4.m3u8"))
                {
                    Model_Stream.Pro_TS_Path = str6.Trim() + ChunkString.Substring(0, ChunkString.IndexOf("index.fmp4.m3u8")).Trim();
                    Model_Stream.Pro_Audio_Path = str6.Trim() + str1.Trim();
                    Model_Stream.Pro_AU_Path = str6.Trim() + str1.Substring(0, str1.IndexOf("index.fmp4.m3u8")).Trim();
                }

                string result3 = VParse.GetPOSTPHP($"https://m.camsoda.com/api/v1/chat/react/{Model_Stream.Pro_Model_Name.Trim().ToLower()}?username=guest_{new Random().Next(100000)}").Result;
                string HTML_Page2 = string.IsNullOrEmpty(result3) ? "" : result3;
                Model_Stream.Pro_Preview_Image = HTML_Page2.Length <= 0 ? null : "https://media.livemediahost.com/stills" + VParse.HTML_Value(HTML_Page2, "/stills", "?").Trim().Replace("\\/", "/");

                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Camsoda.m3u8_URL(Model_Name) = {Model_Stream.Pro_Model_Name}");
                return null;
            }
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                await Task.CompletedTask;
                Class_Model result = Class_Model_List.Class_Model_Find(1, Model_Name).Result;
                if (result == null)
                    return 0;

                string str = VParse.Replace_Space(VParse.GetPOSTPHP($"https://m.camsoda.com/api/v1/chat/react/{Model_Name.Trim().ToLower()}").Result);
                if (str != null)
                {
                    if (str.Contains("error:Nousernamefound"))
                    {
                        result.Pro_Model_Deaktiv = true;
                        return 0;
                    }

                    if (str.Contains("status") && !str.Contains("status:offline"))
                    {
                        if (str.Contains("status:online")) return 1;
                        if (str.Contains("status:private")) return 2;
                        if (str.Contains("status:limited")) return 5;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Camsoda.Online(Model_Name) = {Model_Name}");
                return 0;
            }
        }

        internal static async Task<bool> IsLogin(string HTML)
        {
            await Task.CompletedTask;
            return false;
        }

        internal static async Task<Image?> Image_FromWeb(Class_Model Model_Class)
        {
            await Task.CompletedTask;
            try
            {
                if (!string.IsNullOrEmpty(Model_Class.Pro_Model_Preview_Path))
                {
                    string url = Model_Class.Pro_Model_Preview_Path + "?cb=" + DateTime.UtcNow.Ticks;

                    using (HttpClient httpClient = new HttpClient())
                    {
                        byte[] data = await httpClient.GetByteArrayAsync(url);
                        using (MemoryStream memoryStream = new MemoryStream(data))
                        {
                            return Image.FromStream(memoryStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Camsoda.ImageFromWeb(Model_Name) = {Model_Class.Pro_Model_Name}");
            }
            return null;
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;
            Channel_Info channelInfo = new Channel_Info
            {
                Pro_Exist = false,
                Pro_Website_ID = 1,
                Pro_Name = Model_Name
            };

            try
            {
                string result = await VParse.GetPOSTPHP($"https://m.camsoda.com/api/v1/chat/react/{Model_Name.ToLower()}?username=guest_{new Random().Next(100000)}");
                if (result.Length > 0)
                {
                    string gender = VParse.HTML_Value(result, "gender:", ",");
                    channelInfo.Pro_Gender = gender switch
                    {
                        "f" => 1,
                        "m" => 2,
                        "c" => 3,
                        "t" => 4,
                        _ => 0
                    };

                    channelInfo.Pro_Exist = gender.Length > 0;

                    if (channelInfo.Pro_Exist)
                    {
                        channelInfo.Pro_Birthday = VParse.HTML_Value(result, "Birth Date:", ",");
                        channelInfo.Pro_Country = VParse.HTML_Value(result, "location:", ",");
                        channelInfo.Pro_Profil_Beschreibung = TXT.TXT_Description("Name") + ": " + VParse.HTML_Value(result, "displayName:", ",");

                        string languages = VParse.HTML_Value(result, "languageList:", ",location");
                        if (languages != "null")
                            channelInfo.Pro_Languages = languages;

                        string lastOnline = VParse.HTML_Value(result, "lastOnlineAt:", ",");
                        if (!lastOnline.StartsWith("null"))
                            channelInfo.Pro_Last_Online = lastOnline;

                        string imageUrl = VParse.HTML_Value(result, "avatarUrl:", ",").Trim().Replace("\\/", "/");
                        if (await Parameter.URL_Response(imageUrl))
                        {
                            using (HttpClient httpClient = new HttpClient())
                            using (Stream stream = await httpClient.GetStreamAsync(imageUrl))
                            {
                                channelInfo.Pro_Profil_Image = Image.FromStream(stream);
                            }
                        }
                    }
                }

                return channelInfo;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Camsoda.Profil(Model_Name) = {Model_Name}");
                return channelInfo;
            }
        }

        internal static async Task<bool> IsGalerie(string URL_String, string Html_String, Class_Model Model_Class)
        {
            await Task.CompletedTask;
            return Html_String.Contains("https://md-secure.camsoda.com/recordings/") && Model_Class != null || URL_String.Contains("/media/");
        }

        internal static async void Galerie_Movie_Download(string URL_String, string HTML_String, Class_Model Model_Class = null)
        {
            await Task.CompletedTask;
            try
            {
                string downloadUrl = "";
                string fileName = "";

                if (!URL_String.StartsWith("/media/"))
                {
                    if (HTML_String.Contains("https://md-secure.camsoda.com/user/videos"))
                    {
                        string siteUrl = "https://md-secure.camsoda.com/user/videos" + VParse.HTML_Value(HTML_String, "https://md-secure.camsoda.com/user/videos", "></video>").Replace("amp;", "");
                        if (Parameter.URL_Response(siteUrl).Result)
                        {
                            downloadUrl = siteUrl;
                            fileName = URL_String.Split('/')[5];
                        }
                    }
                    else if (HTML_String.Contains("https://md-secure.camsoda.com/recordings/"))
                    {
                        string siteUrl = "https://md-secure.camsoda.com/recordings" + VParse.HTML_Value(HTML_String, "src=https://md-secure.camsoda.com/recordings", "></video>").Replace("amp;", "");
                        if (Parameter.URL_Response(siteUrl).Result)
                        {
                            downloadUrl = siteUrl;
                            fileName = VParse.HTML_Value(HTML_String, "ListItem><span itemprop=name>", "</");
                        }
                    }
                }

                string targetPath = Model_Class == null ? Modul_Ordner.Ordner_Pfad() : Model_Class.Pro_Model_Directory;

                if (targetPath.Length <= 0 || downloadUrl.Length <= 0)
                    return;

                Dialog_Save dialogSave = new Dialog_Save
                {
                    StartPosition = FormStartPosition.CenterParent,
                    TopMost = true,
                    Pro_Download_Path = downloadUrl,
                    Pro_Target_Path = targetPath,
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
