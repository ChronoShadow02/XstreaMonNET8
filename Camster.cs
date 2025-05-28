using System.Net;

namespace XstreaMonNET8
{
    internal sealed class Camster
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;
                string result = VParse.HTML_Load("https://www.camster.com/ws/rooms/check-model-status.php?model_name=" + Model_Stream.Pro_Model_Name, true).Result;
                if (result.Length <= 0 || result.IndexOf("online,model_id:") <= -1)
                    return null!;

                string URLString = "https://hls.vscdns.com/manifest.m3u8?key=nil&provider=cdn5&is_ll=true&model_id=" + VParse.HTML_Value(result, "online,model_id:", ",DATA", false);
                string[] strArray1 = VParse.HTML_Load(URLString, true).Result.Split('#');
                string ChunkString = "";
                string str1 = "";

                foreach (string str2 in strArray1)
                {
                    if (str2.StartsWith("EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID"))
                    {
                        str1 = str2.Substring(str2.IndexOf("URI=") + 4);
                        if (str1.IndexOf(',') > -1)
                        {
                            str1 = str1.Substring(0, str1.LastIndexOf(","));
                            break;
                        }
                        break;
                    }
                }

                foreach (string str3 in strArray1)
                {
                    switch (Model_Stream.Pro_Qualität_ID)
                    {
                        case 0:
                            if (strArray1.Last().IndexOf(".m3u8") > -1)
                            {
                                ChunkString = strArray1.Last().Substring(strArray1.Last().IndexOf("chunklist"));
                                goto label_23;
                            }
                            break;
                        case 1:
                            if (str3.IndexOf("480x270") > -1)
                            {
                                ChunkString = str3.Substring(str3.IndexOf("chunklist"));
                                goto label_23;
                            }
                            break;
                        case 2:
                            if (str3.IndexOf("640x360") > -1)
                            {
                                ChunkString = str3.Substring(str3.IndexOf("chunklist"));
                                goto label_23;
                            }
                            break;
                        case 3:
                            if (str3.IndexOf("1280x720") > -1)
                            {
                                ChunkString = str3.Substring(str3.IndexOf("chunklist"));
                                goto label_23;
                            }
                            break;
                        case 4:
                            if (str3.IndexOf("1920x1080") > -1)
                            {
                                ChunkString = str3.Substring(str3.IndexOf("chunklist"));
                                goto label_23;
                            }
                            break;
                    }
                }

            label_23:
                Model_Stream.Pro_M3U8_Path = "https://hls.vscdns.com/" + ChunkString.Trim();
                Model_Stream.Pro_Audio_Path = "https://hls.vscdns.com/" + str1.Trim();
                Model_Stream.Pro_FFMPEG_Path = URLString;
                Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray1, ChunkString);
                Model_Stream.Pro_TS_Path = "";
                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Camster.Stream_Adresses");
                return null!;
            }
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                await Task.CompletedTask;
                string HTML_Page = VParse.HTML_Load("https://www.camster.com/ws/rooms/check-model-status.php?model_name=" + Model_Name, true).Result.Replace("\"", "");
                if (HTML_Page.Length <= 0)
                    return 0;

                if (HTML_Page.ToLower().IndexOf("status:online") <= -1)
                    return 0;

                string model_id = VParse.HTML_Value(HTML_Page, "model_id:", ",DATA");
                return Parameter.URL_Response("https://hls.vscdns.com/manifest.m3u8?key=nil&provider=cdn5&is_ll=true&model_id=" + model_id).Result ? 1 : 0;
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
                string result = VParse.HTML_Load("https://www.camster.com/ws/rooms/check-model-status.php?model_name=" + Model_Class.Pro_Model_Name, true).Result;
                if (result.Length > 0 && result.IndexOf("online,model_id:") > -1)
                {
                    string address = "https://live-screencaps.vscdns.com/" + VParse.HTML_Value(result, "online,model_id:", ",DATA", false) + "-desktop.jpg";
                    using WebClient webClient = new();
                    using MemoryStream ms = new(webClient.DownloadData(address));
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Camster.Image_FromWeb");
            }

            return null!;
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;
            Channel_Info channelInfo1 = new();
            try
            {
                channelInfo1.Pro_Exist = false;
                channelInfo1.Pro_Website_ID = 10;
                channelInfo1.Pro_Name = Model_Name;

                string result = VParse.HTML_Load("https://www.camster.com/models/bios/" + Model_Name.ToLower() + "/about.php").Result;

                if (result.Length > 0)
                {
                    string str1 = VParse.HTML_Value(result, "var listsUrl", ";");
                    channelInfo1.Pro_Gender = str1.Contains("/girls/") ? 1 :
                                               str1.Contains("/guys/") ? 2 :
                                               str1.Contains("/trans/") ? 4 : 0;
                    channelInfo1.Pro_Exist = str1.Length > 0;

                    if (channelInfo1.Pro_Exist)
                    {
                        channelInfo1.Pro_Country = VParse.HTML_Value(result, "<div>Location:</div><div>", "</div>");
                        channelInfo1.Pro_Profil_Beschreibung = channelInfo1.Pro_Country;

                        string Item_String = VParse.HTML_Value(result, "<div>Birthday:</div><div>", "</div>");
                        if (Item_String.Length > 0)
                        {
                            int month = ValueBack.Month_From_String(Item_String);
                            int day = ValueBack.Get_Int_From_String(Item_String);
                            int age = ValueBack.Get_Int_From_String(VParse.HTML_Value(result, "<div>Age:</div><div>", "</div>"));
                            int year = DateTime.Today.Year - age;
                            if (DateTime.Compare(new DateTime(DateTime.Now.Year, month, day), DateTime.Now) > 0)
                                year--;
                            channelInfo1.Pro_Birthday = new DateTime(year, month, day);
                        }

                        string Left = VParse.HTML_Value(result, "<div>Languages:</div><div>", "</div>");
                        while (Left.IndexOf("<a href=") > 0)
                        {
                            int start = Left.IndexOf("<");
                            int end = Left.IndexOf(">", start);
                            Left = Left.Remove(start, end - start + 1);
                            if (Left.IndexOf("</a>") > 0)
                                Left = Left.Remove(Left.IndexOf("</a>"), 4);
                        }

                        if (Left.StartsWith(" "))
                            Left = Left.Substring(1);

                        if (!Left.Equals("null", StringComparison.OrdinalIgnoreCase))
                            channelInfo1.Pro_Languages = Left;

                        channelInfo1.Pro_Profil_Beschreibung += (channelInfo1.Pro_Profil_Beschreibung.Length > 0 ? " " : "") +
                                                                TXT.TXT_Description("Alter") + ": " +
                                                                VParse.HTML_Value(result, "<div>Age:</div><div>", "</div>");

                        channelInfo1.Pro_Last_Online = VParse.HTML_Value(result, "<div>Last Online:</div><div>", "</div>").Trim();

                        string str3 = VParse.HTML_Value(result, "<img class=model-photo src=", "/>").Trim();

                        if (await Parameter.URL_Response(str3))
                        {
                            using HttpClient httpClient = new();
                            using Stream stream = await httpClient.GetStreamAsync(str3);
                            channelInfo1.Pro_Profil_Image = Image.FromStream(stream);
                        }
                        channelInfo1.Pro_Online = Camster.Online(channelInfo1.Pro_Name).Result == 1;
                    }
                }

                return channelInfo1;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Camster.Profil");
                return channelInfo1;
            }
        }
    }
}
