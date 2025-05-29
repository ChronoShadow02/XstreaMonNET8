using System.Diagnostics;

namespace XstreaMonNET8
{
    internal class Sites
    {
        internal static List<Class_Website> Website_List;

        internal static Class_Website Website_Find(int platformId)
        {
            foreach (var website in Website_List)
            {
                if (website.Pro_ID == platformId)
                {
                    return website;
                }
            }

            return null!;
        }

        internal static void WebSiteOpen(int websiteId, string userName)
        {
            try
            {
                var classWebsite = Website_Find(websiteId);
                if (classWebsite == null)
                    return;

                string url = string.Format(classWebsite.Pro_Model_URL, userName);
                WebOpen(url);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Info_Click");
            }
        }

        internal static void WebOpen(string webAddress)
        {
            int browser = ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Browser", "Product", "-1"));

            switch (browser)
            {
                case -1:
                    CamBrowser_Open(webAddress);
                    break;
                case 0:
                    Process.Start(new ProcessStartInfo(webAddress) { UseShellExecute = true });
                    break;
                case 1:
                    Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", $"-inprivate -nomerge {webAddress}");
                    break;
                case 2:
                    Process.Start("C:\\Program Files\\Mozilla Firefox\\firefox.exe", $"-private {webAddress}");
                    break;
                case 3:
                    Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", $"--incognito {webAddress}");
                    break;
                case 4:
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lovense\\Browser\\chrome.exe");
                    Process.Start(path, webAddress);
                    break;
            }
        }

        internal static void CamBrowser_Open(string webAddress)
        {
            var camBrowser = new CamBrowser
            {
                WebView_Source = webAddress,
                WindowState = FormWindowState.Maximized,
                StartPosition = FormStartPosition.CenterParent
            };

            camBrowser.Show();
        }

        public static StreamAdresses Stream_Adressen_Load(StreamAdresses modelStream)
        {
            try
            {
                return modelStream.Pro_Model_Website switch
                {
                    0 => Chaturbate.StreamAdresses(modelStream).Result,
                    1 => Camsoda.StreamAdresses(modelStream).Result,
                    2 => Stripchat.StreamAdresses(modelStream).Result,
                    3 => Bongacams.StreamAdresses(modelStream).Result,
                    4 => Cam4.StreamAdresses(modelStream).Result,
                    5 => Streamate.StreamAdresses(modelStream).Result,
                    6 => Flirt4Free.StreamAdresses(modelStream).Result,
                    7 => MyFreeCams.StreamAdresses(modelStream).Result,
                    8 => Jerkmate.StreamAdresses(modelStream).Result,
                    9 => CamsCom.StreamAdresses(modelStream).Result,
                    10 => Camster.StreamAdresses(modelStream).Result,
                    11 => Freeoneslive.StreamAdresses(modelStream).Result,
                    12 => EPlay.StreamAdresses(modelStream).Result,
                    _ => null
                };
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Stream_Adressen_Load");
                return null;
            }
        }

        public static StreamAdresses Stream_Adressen_Load(StreamAdresses modelStream, string htmlText)
        {
            try
            {
                return modelStream.Pro_Model_Website switch
                {
                    0 => Chaturbate.StreamAdresses(modelStream, htmlText).Result,
                    1 => Camsoda.StreamAdresses(modelStream).Result,
                    2 => Stripchat.StreamAdresses(modelStream, htmlText).Result,
                    3 => Bongacams.StreamAdresses(modelStream, htmlText).Result,
                    4 => Cam4.StreamAdresses(modelStream).Result,
                    5 => Streamate.Stream_Adresses(modelStream).Result,
                    6 => Flirt4Free.Stream_Adresses(modelStream).Result,
                    7 => MyFreeCams.StreamAdresses(modelStream).Result,
                    8 => Jerkmate.Stream_Adresses(modelStream).Result,
                    9 => CamsCom.StreamAdresses(modelStream).Result,
                    10 => Camster.Stream_Adresses(modelStream).Result,
                    11 => Freeoneslive.StreamAdresses(modelStream).Result,
                    12 => EPlay.StreamAdresses(modelStream).Result,
                    _ => null
                };
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Stream_Adressen_Load");
                return null!;
            }
        }

        public static int Model_Online(string modelName, int websiteId)
        {
            var result = Class_Model_List.Class_Model_Find(websiteId, modelName.ToLower()).Result;

            if (result != null)
            {
                var lastCheck = result.Timer_Online_Change.BGW_Last_Check;
                var intervalo = result.Timer_Online_Change.Pro_Timer_Intervall;
                var nextCheckTime = lastCheck.AddSeconds(intervalo);

                if (DateTime.Now > nextCheckTime)
                {
                    if (!Parameter.Recording_Stop)
                    {
                        return websiteId switch
                        {
                            0 => Chaturbate.Online(modelName).Result,
                            1 => Camsoda.Online(modelName).Result,
                            2 => Stripchat.Online(modelName).Result,
                            3 => Bongacams.Online(modelName).Result,
                            4 => Cam4.Online(modelName).Result,
                            5 => Streamate.Online(modelName).Result,
                            6 => Flirt4Free.Online(modelName).Result,
                            7 => MyFreeCams.Online(modelName).Result,
                            8 => Jerkmate.Online(modelName).Result,
                            9 => CamsCom.Online(modelName).Result,
                            10 => Camster.Online(modelName).Result,
                            11 => Freeoneslive.Online(modelName).Result,
                            12 => EPlay.Online(modelName).Result,
                            _ => 0
                        };
                    }
                    return 0;
                }
                else
                {
                    return result.Timer_Online_Change.BGW_Result;
                }
            }

            return 0;
        }

        public static int Model_Online(string modelName, int websiteId, string htmlString)
        {
            if (Parameter.Recording_Stop)
                return 0;

            return websiteId switch
            {
                0 => Chaturbate.Online(modelName).Result,
                1 => Camsoda.Online(modelName).Result,
                2 => Stripchat.Online(modelName).Result,
                3 => Bongacams.Online(modelName, htmlString).Result,
                4 => Cam4.Online(modelName).Result,
                5 => Streamate.Online(modelName).Result,
                6 => Flirt4Free.Online(modelName).Result,
                7 => MyFreeCams.Online(modelName).Result,
                8 => Jerkmate.Online(modelName).Result,
                9 => CamsCom.Online(modelName).Result,
                10 => Camster.Online(modelName).Result,
                11 => Freeoneslive.Online(modelName).Result,
                12 => EPlay.Online(modelName).Result,
                _ => 0
            };
        }

        internal static async Task<System.Drawing.Image?> Image_FromStream(Class_Model model)
        {
            await Task.CompletedTask;
            string path = Path.Combine(Parameter.CommonPath, "Temp", $"IFW_{model.Pro_Model_Name}.jpg");

            try
            {
                if (File.Exists(path))
                {
                    using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    var image = new Bitmap(fileStream);
                    File.Delete(path);
                    return image;
                }

                string ffmpegPath = model.Pro_Model_FFMPEG_Path;
                if (!string.IsNullOrWhiteSpace(ffmpegPath))
                {
                    var processStartInfo = new ProcessStartInfo
                    {
                        FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecordStream.exe"),
                        Arguments = $"-i \"{ffmpegPath}\" -f image2 -vf fps=1 \"{path}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    using var process = new Process { StartInfo = processStartInfo };
                    process.Start();
                    process.WaitForExit(5000);

                    if (!process.HasExited && Parameter.Task_Runs(process.Id))
                    {
                        process.Kill();
                    }

                    if (File.Exists(path))
                    {
                        byte[] imageBytes = await File.ReadAllBytesAsync(path);
                        File.Delete(path);
                        return new Bitmap(new MemoryStream(imageBytes));
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, nameof(Image_FromStream));
            }

            return null;
        }

        internal static int Resolution_Find(string[] playlist, string chunkString)
        {
            int resolutionLevel = 0;
            string chunkKey = chunkString.Trim().ToLowerInvariant();

            foreach (string line in playlist)
            {
                if (!line.ToLowerInvariant().Contains(chunkKey))
                    continue;

                string cleanLine = line.Replace(" ", "");

                if (cleanLine.Contains("256x144") || cleanLine.Contains("284x160") ||
                    cleanLine.Contains("320x180") || cleanLine.Contains("426x240") ||
                    cleanLine.Contains("480x270"))
                {
                    resolutionLevel = 1;
                }
                else if (cleanLine.Contains("640x360") || cleanLine.Contains("768x432") ||
                         cleanLine.Contains("854x480") || cleanLine.Contains("960x540"))
                {
                    resolutionLevel = 2;
                }
                else if (cleanLine.Contains("1280x720"))
                {
                    resolutionLevel = 3;
                }
                else if (cleanLine.Contains("1920x1080"))
                {
                    resolutionLevel = 4;
                }
                else if (cleanLine.Contains("3840x2160"))
                {
                    resolutionLevel = 5;
                }
            }

            return resolutionLevel;
        }

        public static bool WebSite_IsLogin(int WebSite_ID, string HTML_String)
        {
            bool flag;
            switch (WebSite_ID)
            {
                case 0:
                    flag = Chaturbate.IsLogin(HTML_String).Result;
                    break;
                case 1:
                    flag = Camsoda.IsLogin(HTML_String).Result;
                    break;
                case 2:
                    flag = false;
                    break;
                case 3:
                    flag = false;
                    break;
                case 4:
                    flag = false;
                    break;
                case 5:
                    flag = false;
                    break;
                case 6:
                    flag = false;
                    break;
                case 7:
                    flag = false;
                    break;
                case 8:
                    flag = false;
                    break;
                case 9:
                    flag = false;
                    break;
                case 10:
                    flag = false;
                    break;
                case 11:
                    flag = false;
                    break;
                default:
                    flag = false;
                    break;
            }
            return flag;
        }

        internal static (string, int) Site_Model_URL_Read(string URL_String)
        {
            string modelName = "";
            int siteId = -1;
            string urlLower = URL_String.ToLower();

            if (urlLower.Contains("chaturbate."))
            {
                string marker = "chaturbate.com/";
                if (URL_String.Contains(marker))
                {
                    if (URL_String.Contains("&room=")) marker = "&room=";
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    if (end == -1) end = URL_String.IndexOf("&", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 0;
            }
            else if (urlLower.Contains("camsoda."))
            {
                string marker = "camsoda.com/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 1;
            }
            else if (urlLower.Contains("stripchat."))
            {
                string marker = "stripchat.com/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 2;
            }
            else if (urlLower.Contains("bongacams."))
            {
                string marker = "bongacams.com/";
                if (urlLower.Contains("bongacams.net/")) marker = "bongacams.net/";
                if (urlLower.Contains("profile/")) marker = "profile/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 3;
            }
            else if (urlLower.Contains("cam4."))
            {
                string marker = ".com/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 4;
            }
            else if (urlLower.Contains("streamate."))
            {
                string marker = "/cam/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 5;
            }
            else if (urlLower.Contains("flirt4free."))
            {
                string marker = "/?model=";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 6;
            }
            else if (urlLower.Contains("myfreecams.com"))
            {
                string marker = ".com/#";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 7;
            }
            else if (urlLower.Contains("jerkmate."))
            {
                string marker = "/cam/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 8;
            }
            else if (urlLower.Contains("cams.com"))
            {
                string marker = "com/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("#", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 9;
            }
            else if (urlLower.Contains("camster."))
            {
                string marker = "/?model=";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 10;
            }
            else if (urlLower.Contains("freeoneslive."))
            {
                string marker = "/?model=";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 11;
            }
            else if (urlLower.Contains("eplay."))
            {
                string marker = "com/";
                if (URL_String.Contains(marker))
                {
                    int start = URL_String.IndexOf(marker) + marker.Length;
                    int end = URL_String.IndexOf("/", start);
                    modelName = end != -1 ? URL_String.Substring(start, end - start) : URL_String[start..];
                }
                siteId = 12;
            }

            return (modelName, siteId);
        }

        internal static bool Site_Is_Galerie(string URL_String, string HTML_String, Class_Model Model_Class)
        {
            if (URL_String.Contains("chaturbate"))
            {
                return Chaturbate.IsGalerie(URL_String, HTML_String, Model_Class).Result;
            }
            else if (URL_String.Contains("stripchat"))
            {
                return Stripchat.IsGalerie(URL_String, HTML_String, Model_Class).Result;
            }
            else if (URL_String.Contains("camsoda"))
            {
                return Camsoda.IsGalerie(URL_String, HTML_String, Model_Class).Result;
            }

            return false;
        }

        internal static void Download_Galerie_Movie(string URL_String, string HTML_String, Class_Model Model_Class)
        {
            if (URL_String.Contains("chaturbate"))
            {
                Chaturbate.Galerie_Movie_Download(URL_String, HTML_String, Model_Class);
            }
            else if (URL_String.Contains("stripchat"))
            {
                Stripchat.Galerie_Movie_Download(URL_String, HTML_String, Model_Class);
            }
            else if (URL_String.Contains("camsoda"))
            {
                Camsoda.Galerie_Movie_Download(URL_String, HTML_String, Model_Class);
            }
        }

        internal static void Website_Load()
        {
            Sites.Website_List =
            [
                new(-1, TXT.TXT_Description("Modelordner"), "", Resources.Folder16, 0, 0,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "0", true.ToString())),
                    "", "", "", "", "", false),

                new(0, "Chaturbate", Chaturbate.Site_URL(false), Resources.Chaturbate, 250, 5000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "0", true.ToString())),
                    "chaturbate.", $"{Chaturbate.Site_URL()}{{0}}", "https://chaturbate.com/in/?tour=g4pe&campaign=NbZCW&track=default",
                    "", "https://chaturbate.com/{0}", false),

                new(1, "Camsoda", "https://camsoda.com/", Resources.Camsoda, 30000, 30000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "1", true.ToString())),
                    "camsoda.", "https://camsoda.com/{0}", "https://camsoda.com", "", "https://camsoda.com/{0}", true),

                new(2, "Stripchat", "https://stripchat.com/", Resources.Stripchat, 30000, 30000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "2", true.ToString())),
                    "stripchat.", "https://stripchat.com/{0}", "https://stripchat.com", "&_HLS_msn={0}", "https://stripchat.com/{0}", true),

                new(3, "BongaCams", "https://bongacams.com/", Resources.Bongacams, 250, 5000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "3", true.ToString())),
                    "bongacams.", "https://bongacams.com/{0}", "https://bongacams.com", "", "https://bongacams.com/{0}", true),

                new(4, "Cam4", "https://cam4.com/", Resources.Cam4, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "4", true.ToString())),
                    "cam4.", "https://cam4.com/{0}", "https://cam4.com/", "", "https://cam4.com/{0}", false),

                new(5, "Streamate", "https://streamate.com/", Resources.Streammate, 20000, 20000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "5", true.ToString())),
                    "streamate.", "https://streamate.com/cam/{0}", "https://streamate.com", "", "https://streamate.com/cam/{0}", false),

                new(6, "Flirt4Free", "https://flirt4free.com/", Resources.Flirt4Free, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "6", true.ToString())),
                    "flirt4free.", "https://flirt4free.com/?model={0}", "https://flirt4free.com", "", "https://flirt4free.com/?model={0}", false),

                new(7, "MyFreeCams", "https://myfreecams.com/", Resources.mfc, 500, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "7", true.ToString())),
                    "myfreecams.", "https://myfreecams.com/#{0}", "https://myfreecams.com", "", "https://myfreecams.com/#{0}", false),

                new Class_Website(8, "Jerkmate", "https://jerkmate.com/", Resources.Jerkmate, 20000, 20000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "8", true.ToString())),
                    "jerkmate.", "https://jerkmate.com/cam/{0}", "https://jerkmate.com", "", "https://jerkmate.com/cam/{0}", false),

                new(9, "Cams.com", "https://cams.com/", Resources.CamsCom, 30000, 30000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "9", true.ToString())),
                    "cams.", "https://cams.com/{0}", "https://cams.com", "", "https://cams.com/{0}", false),

                new(10, "Camster.com", "https://camster.com/", Resources.Camster16, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "10", true.ToString())),
                    "camster.", "https://camster.com/?model={0}", "https://camster.com", "", "https://camster.com/?model={0}", false),

                new(11, "FreeOnesLIVE.com", "https://freeoneslive.com/", Resources.fol16, 5000, 5000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "11", true.ToString())),
                    "freeoneslive.", "https://freeoneslive.com/?model={0}", "https://freeoneslive.com", "", "https://freeoneslive.com/?model={0}", false),

                new(12, "eplay.com", "https://eplay.com/", Resources.eplay16, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "12", true.ToString())),
                    "eplay.", "https://eplay.com/{0}/live", "https://eplay.com", "", "https://eplay.com/{0}/live", false)
            ];
        }
    }
}
