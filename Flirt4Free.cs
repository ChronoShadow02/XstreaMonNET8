using System.Diagnostics;
using System.Net;

namespace XstreaMonNET8
{
    internal static class Flirt4Free
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;

                string result = await VParse.HTML_Load("https://ws.vs3.com/rooms/check-model-status.php?model_name=" + Model_Stream.Pro_Model_Name, true);
                if (string.IsNullOrEmpty(result) || !result.Contains("online,model_id:"))
                    return null;

                string str1 = "https://hls.vscdns.com/manifest.m3u8?key=nil&provider=cdn5&model_id=" + VParse.HTML_Value(result, "online,model_id:", ",DATA", false);
                if (!await Parameter.URL_Response(str1))
                    str1 = "https://hls.vscdns.com/manifest.m3u8?key=nil&provider=cdn5&is_ll=true&model_id=" + VParse.HTML_Value(result, "online,model_id:", ",DATA", false);

                string[] strArray1 = (await VParse.HTML_Load(str1, true)).Split('#');
                string ChunkString = "";
                string str2 = "";

                foreach (var str3 in strArray1)
                {
                    if (str3.StartsWith("EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID"))
                    {
                        str2 = str3[(str3.IndexOf("URI=") + 4)..];
                        if (str2.Contains(","))
                        {
                            str2 = "https://hls.vs3.com/" + str2[..str2.LastIndexOf(",")];
                            break;
                        }
                    }
                }

                foreach (var str4 in strArray1)
                {
                    switch (Model_Stream.Pro_Qualität_ID)
                    {
                        case 0:
                            if (strArray1.Last().Contains(".m3u8"))
                            {
                                ChunkString = strArray1.Last()[strArray1.Last().IndexOf("chunklist")..];
                                goto assign;
                            }
                            break;
                        case 1:
                            if (str4.Contains("480x270"))
                            {
                                ChunkString = str4[str4.IndexOf("chunklist")..];
                                goto assign;
                            }
                            break;
                        case 2:
                            if (str4.Contains("640x360"))
                            {
                                ChunkString = str4[str4.IndexOf("chunklist")..];
                                goto assign;
                            }
                            break;
                        case 3:
                            if (str4.Contains("1280x720"))
                            {
                                ChunkString = str4[str4.IndexOf("chunklist")..];
                                goto assign;
                            }
                            break;
                        case 4:
                            if (str4.Contains("1920x1080"))
                            {
                                ChunkString = str4[str4.IndexOf("chunklist")..];
                                goto assign;
                            }
                            break;
                    }
                }

            assign:
                Model_Stream.Pro_M3U8_Path = "https://hls.vscdns.com/" + ChunkString.Trim();
                Model_Stream.Pro_Audio_Path = str2.Trim();
                Model_Stream.Pro_FFMPEG_Path = str1;
                Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(strArray1, ChunkString);
                Model_Stream.Pro_TS_Path = "";
                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Flirt4Free.Stream_Adresses");
                return null;
            }
        }

        private static string Model_ID(string Model_Name)
        {
            try
            {
                string result = VParse.HTML_Load("https://ws.vs3.com/rooms/check-model-status.php?model_name=" + Model_Name, true).Result;
                if (string.IsNullOrEmpty(result) || !result.Contains("online,model_id:"))
                    return "";
                return VParse.HTML_Value(result, "online,model_id:", ",DATA", false);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Flirt4Free.Model_ID");
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

                string result = await VParse.HTML_Load("https://ws.vs3.com/rooms/check-model-status.php?model_name=" + Model_Name, true);
                if (!string.IsNullOrEmpty(result) && result.Contains("status:online"))
                {
                    string id = VParse.HTML_Value(result, "online,model_id:", ",DATA", false);
                    string urlPart = VParse.HTML_Value(
                        (await VParse.HTML_Load("https://www.flirt4free.com/ws/chat/get-stream-urls.php?model_id=" + id, true))
                        .Replace("\"", "").Replace("\\", ""), "//hls.", "}");
                    if (!string.IsNullOrEmpty(urlPart))
                        return await Parameter.URL_Response("https://hls." + urlPart) ? 1 : 2;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        internal static async Task<Image> Image_FromWeb(Class_Model Model_Class)
        {
            await Task.CompletedTask;
            string path = Path.Combine(Parameter.CommonPath, $"Temp\\IFW_{Model_Class.Pro_Model_Name}.jpg");
            try
            {
                if (File.Exists(path))
                {
                    using FileStream fs = new(path, FileMode.Open);
                    var bmp = new Bitmap(fs);
                    File.Delete(path);
                    return bmp;
                }

                if (!string.IsNullOrWhiteSpace(Model_Class.Pro_Model_FFMPEG_Path))
                {
                    ProcessStartInfo psi = new()
                    {
                        FileName = Path.Combine(AppContext.BaseDirectory, "RecordStream.exe"),
                        Arguments = $"-i \"{Model_Class.Pro_Model_FFMPEG_Path}\" -f image2 -vf fps=1 \"{path}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    using Process proc = new() { StartInfo = psi };
                    proc.Start();
                    proc.WaitForExit(5000);
                    if (!proc.HasExited && Parameter.Task_Runs(proc.Id))
                        proc.Kill();

                    if (File.Exists(path))
                    {
                        byte[] buffer = await File.ReadAllBytesAsync(path);
                        File.Delete(path);
                        return new Bitmap(new MemoryStream(buffer));
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Flirt4Free.Image");
            }

            return null;
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;
            Channel_Info info = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 6,
                Pro_Name = Model_Name
            };

            try
            {
                string result = await VParse.HTML_Load("https://www.flirt4free.com/models/bios/" + Model_Name.ToLower() + "/about.php");
                if (string.IsNullOrWhiteSpace(result))
                    return info;

                string listsUrl = VParse.HTML_Value(result, "var listsUrl", ";");
                info.Pro_Gender = listsUrl.Contains("/girls/") ? 1 :
                                  listsUrl.Contains("/guys/") ? 2 :
                                  listsUrl.Contains("/trans/") ? 4 : 0;
                info.Pro_Exist = listsUrl.Length > 0;

                if (!info.Pro_Exist)
                    return info;

                info.Pro_Country = VParse.HTML_Value(result, "<li>Location:", "</li>");
                info.Pro_Profil_Beschreibung = info.Pro_Country;

                string birthday = VParse.HTML_Value(result, "<li>Birthday:", "</li>");
                if (!string.IsNullOrEmpty(birthday))
                {
                    int month = Array.FindIndex(new[]
                    {
                        "January", "February", "March", "April", "May", "June",
                        "July", "August", "September", "October", "November", "December"
                    }, m => birthday.Contains(m)) + 1;

                    int day = int.Parse(ValueBack.Get_Zahl_Extract_From_String(birthday));
                    int age = int.Parse(ValueBack.Get_Zahl_Extract_From_String(VParse.HTML_Value(result, "<li>Age:", "</li>")));
                    int year = DateTime.Today.Year - age;
                    if (new DateTime(DateTime.Now.Year, month, day) > DateTime.Now)
                        year--;

                    info.Pro_Birthday = new DateTime(year, month, day);
                }

                string languages = VParse.HTML_Value(result, "<li>Languages:", "</li>");
                while (languages.Contains("<a href="))
                {
                    int start = languages.IndexOf("<");
                    int end = languages.IndexOf(">", start);
                    if (start >= 0 && end > start)
                        languages = languages.Remove(start, end - start + 1);

                    int anchorEnd = languages.IndexOf("</a>");
                    if (anchorEnd >= 0)
                        languages = languages.Remove(anchorEnd, 4);
                }
                info.Pro_Languages = languages.Trim() == "null" ? null : languages.Trim();

                string ageValue = VParse.HTML_Value(result, "<li>Age: ", "</li>");
                if (!string.IsNullOrEmpty(ageValue))
                    info.Pro_Profil_Beschreibung += $" {TXT.TXT_Description("Alter")}: {ageValue}";

                info.Pro_Last_Online = VParse.HTML_Value(result, "<li>Last Online: ", "</li>").Trim();

                string imageUrl = VParse.HTML_Value(result, "<img class=model-photo src=", " loading=lazy>").Trim();
                if (await Parameter.URL_Response(imageUrl))
                {
                    using WebClient wc = new();
                    using Stream stream = wc.OpenRead(imageUrl);
                    info.Pro_Profil_Image = Image.FromStream(stream);
                }

                info.Pro_Online = await Online(info.Pro_Name) != 0;
                return info;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Flirt4Free.Profil");
                return info;
            }
        }
    }
}
