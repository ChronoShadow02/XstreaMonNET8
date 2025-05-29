using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace XstreaMonNET8
{
    internal static class Freeoneslive
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                Model_Stream.Pro_Model_Online_ID ??= Model_ID(Model_Stream.Pro_Model_Name);

                if (!string.IsNullOrEmpty(Model_Stream.Pro_Model_Online_ID))
                {
                    string html = await VParse.HTML_Load($"https://www.freeoneslive.com/ws/chat/get-stream-urls.php?model_id={Model_Stream.Pro_Model_Online_ID}", true);
                    string Site_URL = StreamAdressResolution(html, Model_Stream.Pro_Record_Resolution);

                    if (await Parameter.URL_Response(Site_URL))
                    {
                        Model_Stream.Pro_M3U8_Path = Site_URL;
                        Model_Stream.Pro_FFMPEG_Path = Site_URL;
                        Model_Stream.Pro_TS_Path = string.Empty;
                        return Model_Stream;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Freeoneslive.Stream_Adresses");
            }

            return null!;
        }

        private static string StreamAdressResolution(string M3U8_File, int Qualität_ID)
        {
            string content = VParse.HTML_Load("https:" + VParse.HTML_Value(M3U8_File.Replace("\\", ""), "name:cdn5,url:", "}"), false).Result;
            string[] parts = content.Split('#');
            string result = string.Empty;
            int maxBandwidth = 0;

            foreach (string HTML_Page in parts)
            {
                if (!HTML_Page.StartsWith("EXT-X-STREAM-INF:"))
                    continue;

                switch (Qualität_ID)
                {
                    case 0:
                        int bandwidth = ValueBack.Get_CInteger(VParse.HTML_Value(HTML_Page, "BANDWIDTH=", ","));
                        if (bandwidth > maxBandwidth)
                        {
                            maxBandwidth = bandwidth;
                            result = "chunklist" + VParse.HTML_Value(HTML_Page, "chunklist", ",");
                        }
                        break;
                    case 1 when HTML_Page.Contains("RESOLUTION=480"):
                    case 2 when HTML_Page.Contains("RESOLUTION=640"):
                    case 3 when HTML_Page.Contains("RESOLUTION=1280"):
                    case 4 when HTML_Page.Contains("RESOLUTION=1920"):
                        result = "chunklist" + VParse.HTML_Value(HTML_Page, "chunklist", ",");
                        break;
                }
            }

            return result.Length > 0 ? "https://hls.vscdns.com/" + result.Trim() : string.Empty;
        }

        private static string Model_ID(string Model_Name)
        {
            try
            {
                string result = VParse.HTML_Load($"https://www.freeoneslive.com/ws/rooms/chat-room-interface.php?a=login_room&model_name={Model_Name}", true).Result;
                if (!string.IsNullOrEmpty(result) && result.Contains("{id:"))
                    return VParse.HTML_Value(result, "{id:", ",", false);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Freeoneslive.Model_ID");
            }

            return string.Empty;
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                var model = await Class_Model_List.Class_Model_Find(0, Model_Name);
                if (await Parameter.URL_Response(model?.Pro_Model_M3U8))
                    return 1;

                string result = await VParse.HTML_Load($"https://www.freeoneslive.com/ws/rooms/chat-room-interface.php?a=login_room&model_name={Model_Name}", true);
                if (string.IsNullOrEmpty(result))
                    return 0;

                return result.Contains(",status:O") ? 1 :
                       result.Contains(",status:A") ? 4 :
                       result.Contains(",status:F") ? 5 : 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Freeoneslive.Online");
                return 0;
            }
        }

        internal static async Task<Image> Image_FromWeb(Class_Model Model_Class)
        {
            await Task.CompletedTask;
            string path = Path.Combine(Parameter.CommonPath, "Temp", $"IFW_{Model_Class.Pro_Model_Name}.jpg");

            try
            {
                if (File.Exists(path))
                {
                    using FileStream fs = new(path, FileMode.Open);
                    Bitmap bmp = new(fs);
                    File.Delete(path);
                    return bmp;
                }

                if (!string.IsNullOrEmpty(Model_Class.Pro_Model_FFMPEG_Path))
                {
                    string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecordStream.exe");
                    string args = $"-i \"{Model_Class.Pro_Model_FFMPEG_Path}\" -f image2 -vf fps=1 \"{path}\"";

                    using Process proc = new()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = exePath,
                            Arguments = args,
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    proc.Start();
                    proc.WaitForExit(5000);

                    if (!proc.HasExited && Parameter.Task_Runs(proc.Id))
                        proc.Kill();

                    if (File.Exists(path))
                    {
                        byte[] bytes = await File.ReadAllBytesAsync(path);
                        File.Delete(path);
                        return Image.FromStream(new MemoryStream(bytes));
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Freeoneslive.Image_FromWeb");
            }

            return null!;
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;
            Channel_Info channelInfo = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 11,
                Pro_Name = Model_Name
            };

            try
            {
                string html = (await VParse.HTML_Load($"https://www.freeoneslive.com/models/bios/{Model_Name.ToLower()}/about.php")).Replace("\n", "").Replace(" ", "").Replace("\"", "");
                if (string.IsNullOrEmpty(html))
                    return channelInfo;

                string urlSegment = VParse.HTML_Value(html, "varlistsUrl", ";");
                channelInfo.Pro_Gender = urlSegment.Contains("/girls/") ? 1 :
                                         urlSegment.Contains("/guys/") ? 2 :
                                         urlSegment.Contains("/trans/") ? 4 : 0;

                channelInfo.Pro_Exist = urlSegment.Length > 0;

                if (!channelInfo.Pro_Exist)
                    return channelInfo;

                channelInfo.Pro_Country = VParse.HTML_Value(html, "Location:</div><div>", "</div>");
                channelInfo.Pro_Profil_Beschreibung = channelInfo.Pro_Country;

                string birthdayString = VParse.HTML_Value(html, "Birthday:</div><div>", "</div>");
                if (!string.IsNullOrEmpty(birthdayString))
                {
                    int day = ValueBack.Get_Int_From_String(birthdayString);
                    int month = DateTime.ParseExact(
                        Regex.Match(birthdayString, @"[a-zA-Z]+").Value,
                        "MMMM",
                        CultureInfo.InvariantCulture).Month;

                    int age = ValueBack.Get_Int_From_String(VParse.HTML_Value(html, "Age:</div><div>", "</div>"));
                    int year = DateTime.Today.Year - age;
                    if (new DateTime(DateTime.Today.Year, month, day) > DateTime.Today)
                        year--;

                    channelInfo.Pro_Birthday = new DateTime(year, month, day);
                }

                string languages = VParse.HTML_Value(html, "Languages:</div><div>", "</div");
                if (!languages.Trim().Equals("n/a", StringComparison.OrdinalIgnoreCase))
                    channelInfo.Pro_Languages = languages.Trim();

                string ageDescription = VParse.HTML_Value(html, "Age:</div><div>", "</div>");
                string description = $"{channelInfo.Pro_Profil_Beschreibung} {(channelInfo.Pro_Profil_Beschreibung.Length > 0 ? " " : "")}{TXT.TXT_Description("Alter")}: {ageDescription}";
                channelInfo.Pro_Profil_Beschreibung = description;

                channelInfo.Pro_Last_Online = VParse.HTML_Value(html, "LastOnline:</div><div>", "</div>").Trim();
                channelInfo.Pro_Online = await Online(channelInfo.Pro_Name) != 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Freeoneslive.Profil");
            }

            return channelInfo;
        }
    }
}
