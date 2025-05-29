using System.Diagnostics;

namespace XstreaMonNET8
{
    internal sealed class Sites
    {
        internal static List<Class_Website> Website_List;

        internal static Class_Website Website_Find(int Platform_ID)
        {
            Class_Website classWebsite = null;
            foreach (Class_Website website in Website_List)
            {
                if (website.Pro_ID == Platform_ID)
                {
                    classWebsite = website;
                    break;
                }
            }
            return classWebsite;
        }

        internal static void WebSiteOpen(int Website_ID, string User_Name)
        {
            try
            {
                Class_Website classWebsite = Website_Find(Website_ID);
                if (classWebsite == null)
                    return;
                WebOpen(string.Format(classWebsite.Pro_Model_URL, User_Name));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Info_Click");
            }
        }

        internal static void WebOpen(string WebAdresse)
        {
            int product = ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Browser", "Product", "-1"));
            switch (product)
            {
                case -1:
                    CamBrowser_Open(WebAdresse);
                    break;
                case 0:
                    Process.Start(new ProcessStartInfo(WebAdresse) { UseShellExecute = true });
                    break;
                case 1:
                    Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", $"-inprivate -nomerge {WebAdresse}");
                    break;
                case 2:
                    Process.Start("C:\\Program Files\\Mozilla Firefox\\firefox.exe", $"-private {WebAdresse}");
                    break;
                case 3:
                    Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", $"--incognito {WebAdresse}");
                    break;
                case 4:
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lovense\\Browser\\chrome.exe");
                    Process.Start(path, WebAdresse);
                    break;
            }
        }

        internal static async void CamBrowser_Open(string WebAdresse)
        {
            await Task.CompletedTask;
            CamBrowser camBrowser = new()
            {
                WebView_Source = WebAdresse,
                WindowState = FormWindowState.Maximized,
                StartPosition = FormStartPosition.CenterParent
            };
            camBrowser.Show();
        }

        public static StreamAdresses Stream_Adressen_Load(StreamAdresses Model_Stream)
        {
            try
            {
                return Model_Stream.Pro_Model_Website switch
                {
                    0 => Chaturbate.StreamAdresses(Model_Stream).Result,
                    1 => Camsoda.StreamAdresses(Model_Stream).Result,
                    2 => Stripchat.Stream_Adresses(Model_Stream).Result,
                    3 => Bongacams.StreamAdresses(Model_Stream).Result,
                    4 => Cam4.StreamAdresses(Model_Stream).Result,
                    5 => Streamate.Stream_Adresses(Model_Stream).Result,
                    6 => Flirt4Free.Stream_Adresses(Model_Stream).Result,
                    7 => MyFreeCams.Stream_Adresses(Model_Stream).Result,
                    8 => Jerkmate.Stream_Adresses(Model_Stream).Result,
                    9 => CamsCom.StreamAdresses(Model_Stream).Result,
                    10 => Camster.Stream_Adresses(Model_Stream).Result,
                    11 => Freeoneslive.Stream_Adresses(Model_Stream).Result,
                    12 => EPlay.Stream_Adresses(Model_Stream).Result,
                    _ => null
                };
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Stream_Adressen_Load");
                return null;
            }
        }

        public static StreamAdresses Stream_Adressen_Load(StreamAdresses Model_Stream, string HTML_Txt)
        {
            try
            {
                return Model_Stream.Pro_Model_Website switch
                {
                    0 => Chaturbate.StreamAdresses(Model_Stream, HTML_Txt).Result,
                    1 => Camsoda.StreamAdresses(Model_Stream).Result,
                    2 => Stripchat.Stream_Adresses(Model_Stream, HTML_Txt).Result,
                    3 => Bongacams.StreamAdresses(Model_Stream, HTML_Txt).Result,
                    4 => Cam4.StreamAdresses(Model_Stream).Result,
                    5 => Streamate.Stream_Adresses(Model_Stream).Result,
                    6 => Flirt4Free.Stream_Adresses(Model_Stream).Result,
                    7 => MyFreeCams.Stream_Adresses(Model_Stream).Result,
                    8 => Jerkmate.Stream_Adresses(Model_Stream).Result,
                    9 => CamsCom.StreamAdresses(Model_Stream).Result,
                    10 => Camster.Stream_Adresses(Model_Stream).Result,
                    11 => Freeoneslive.Stream_Adresses(Model_Stream).Result,
                    12 => EPlay.Stream_Adresses(Model_Stream).Result,
                    _ => null
                };
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Parameter.Stream_Adressen_Load");
                return null;
            }
        }

        public static int Model_Online(string Model_Name, int Website_ID)
        {
            var result = Class_Model_List.Class_Model_Find(Website_ID, Model_Name.ToLower()).Result;
            int num;
            if (result != null)
            {
                if (DateTime.Compare(result.Timer_Online_Change.BGW_Last_Check, DateTime.Now.AddSeconds(result.Timer_Online_Change.Pro_Timer_Intervall)) < 0)
                {
                    if (!Parameter.Recording_Stop)
                    {
                        switch (Website_ID)
                        {
                            case 0: return Chaturbate.Online(Model_Name).Result;
                            case 1: return Camsoda.Online(Model_Name).Result;
                            case 2: return Stripchat.Online(Model_Name).Result;
                            case 3: return Bongacams.Online(Model_Name).Result;
                            case 4: return Cam4.Online(Model_Name).Result;
                            case 5: return Streamate.Online(Model_Name).Result;
                            case 6: return Flirt4Free.Online(Model_Name).Result;
                            case 7: return MyFreeCams.Online(Model_Name).Result;
                            case 8: return Jerkmate.Online(Model_Name).Result;
                            case 9: return CamsCom.Online(Model_Name).Result;
                            case 10: return Camster.Online(Model_Name).Result;
                            case 11: return Freeoneslive.Online(Model_Name).Result;
                            case 12: return EPlay.Online(Model_Name).Result;
                        }
                    }
                    num = 0;
                }
                else
                {
                    num = result.Timer_Online_Change.BGW_Result;
                }
            }
            else
            {
                num = 0;
            }
            return num;
        }

        public static int Model_Online(string Model_Name, int Website_ID, string HTML_String)
        {
            if (Parameter.Recording_Stop)
                return 0;

            return Website_ID switch
            {
                0 => Chaturbate.Online(Model_Name).Result,
                1 => Camsoda.Online(Model_Name).Result,
                2 => Stripchat.Online(Model_Name).Result,
                3 => Bongacams.Online(Model_Name, HTML_String).Result,
                4 => Cam4.Online(Model_Name).Result,
                5 => Streamate.Online(Model_Name).Result,
                6 => Flirt4Free.Online(Model_Name).Result,
                7 => MyFreeCams.Online(Model_Name).Result,
                8 => Jerkmate.Online(Model_Name).Result,
                9 => CamsCom.Online(Model_Name).Result,
                10 => Camster.Online(Model_Name).Result,
                11 => Freeoneslive.Online(Model_Name).Result,
                12 => EPlay.Online(Model_Name).Result,
                _ => 0
            };
        }

        internal static async Task<Image> ImageFromWeb(Class_Model Model_Class)
        {
            try
            {
                Bitmap bitmap = null;
                if (Model_Class != null && !Parameter.Recording_Stop)
                {
                    switch (Model_Class.Pro_Website_ID)
                    {
                        case 0:
                            bitmap = (Bitmap)Chaturbate.Image_FromWeb(Model_Class.Pro_Model_Name.ToLower());
                            break;
                        case 1:
                            bitmap = (Bitmap)await Camsoda.Image_FromWeb(Model_Class);
                            break;
                        case 2:
                            bitmap = (Bitmap)await Stripchat.Image_FromWeb(Model_Class);
                            break;
                        case 3:
                            bitmap = (Bitmap)await Bongacams.Image_FromWeb(Model_Class);
                            break;
                        case 4:
                            bitmap = (Bitmap)await Cam4.Image_FromWeb(Model_Class.Pro_Model_Name);
                            break;
                        case 5:
                            bitmap = (Bitmap)await Streamate.Image_FromWeb(Model_Class);
                            break;
                        case 6:
                            bitmap = (Bitmap)await Flirt4Free.Image_FromWeb(Model_Class);
                            break;
                        case 7:
                            bitmap = (Bitmap)await MyFreeCams.Image_FromWeb(Model_Class);
                            break;
                        case 8:
                            bitmap = (Bitmap)await Jerkmate.Image_FromWeb(Model_Class);
                            break;
                        case 9:
                            bitmap = (Bitmap)await CamsCom.Image_FromWeb(Model_Class);
                            break;
                        case 10:
                            bitmap = (Bitmap)await Camster.Image_FromWeb(Model_Class);
                            break;
                        case 11:
                            bitmap = (Bitmap)await Freeoneslive.Image_FromWeb(Model_Class);
                            break;
                        case 12:
                            bitmap = (Bitmap)await EPlay.Image_FromWeb(Model_Class);
                            break;
                    }

                    if (bitmap == null && !string.IsNullOrEmpty(Model_Class.Pro_Model_M3U8))
                    {
                        bitmap = (Bitmap)await Sites.Image_FromStream(Model_Class);
                    }
                }
                return bitmap;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Parameter.ImageFromWeb(Model_GUID) = {Model_Class?.Pro_Model_GUID}");
                return null;
            }
        }

        internal static async Task<Image> Image_FromStream(Class_Model Model_Class)
        {
            await Task.CompletedTask;

            string path = Path.Combine(Parameter.CommonPath, "Temp", $"IFW_{Model_Class.Pro_Model_Name}.jpg");

            try
            {
                if (File.Exists(path))
                {
                    using FileStream fileStream = new(path, FileMode.Open);
                    Bitmap bitmap = new(fileStream);
                    fileStream.Close();
                    File.Delete(path);
                    return bitmap;
                }

                string ffmpegPath = Model_Class.Pro_Model_FFMPEG_Path;

                if (!string.IsNullOrEmpty(ffmpegPath))
                {
                    ProcessStartInfo psi = new()
                    {
                        FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecordStream.exe"),
                        Arguments = $"-i \"{ffmpegPath}\" -f image2 -vf fps=1 \"{path}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    using Process proc = new() { StartInfo = psi };
                    proc.Start();
                    proc.WaitForExit(5000);

                    if (!proc.HasExited && Parameter.Task_Runs(proc.Id))
                    {
                        proc.Kill();
                    }

                    if (File.Exists(path))
                    {
                        using FileStream fileStream = new(path, FileMode.Open);
                        byte[] buffer = new byte[fileStream.Length];
                        fileStream.Read(buffer, 0, buffer.Length);
                        fileStream.Close();

                        Bitmap bitmap = new(new MemoryStream(buffer));
                        File.Delete(path);
                        return bitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, nameof(Image_FromStream));
            }

            return null;
        }

        internal static int Resolution_Find(string[] Playlist, string ChunkString)
        {
            int num = 0;
            foreach (string str1 in Playlist)
            {
                if (str1.ToLower().Contains(ChunkString.Trim().ToLower()))
                {
                    string str2 = str1.Replace(" ", "");

                    if (str2.Contains("256x144") || str2.Contains("284x160") || str2.Contains("320x180") ||
                        str2.Contains("426x240") || str2.Contains("480x270"))
                        num = 1;
                    else if (str2.Contains("640x360") || str2.Contains("768x432") || str2.Contains("854x480") ||
                             str2.Contains("960x540"))
                        num = 2;
                    else if (str2.Contains("1280x720"))
                        num = 3;
                    else if (str2.Contains("1920x1080"))
                        num = 4;
                    else if (str2.Contains("3840x2160"))
                        num = 5;
                }
            }
            return num;
        }

        public static bool WebSite_IsLogin(int WebSite_ID, string HTML_String)
        {
            return WebSite_ID switch
            {
                0 => Chaturbate.IsLogin(HTML_String).Result,
                1 => Camsoda.IsLogin(HTML_String).Result,
                _ => false
            };
        }

        internal static (string, int) Site_Model_URL_Read(string URL_String)
        {
            string str1 = "";
            int num1 = -1;
            string url = URL_String.ToLower();

            if (url.Contains("chaturbate."))
            {
                string key = URL_String.Contains("&room=") ? "&room=" : "chaturbate.com/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOfAny(new[] { '/', '&' }, start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 0;
            }
            else if (url.Contains("camsoda."))
            {
                string key = "camsoda.com/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 1;
            }
            else if (url.Contains("stripchat."))
            {
                string key = "stripchat.com/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 2;
            }
            else if (url.Contains("bongacams."))
            {
                string key = url.Contains("bongacams.net/") ? "bongacams.net/" : "bongacams.com/";
                if (url.Contains("profile/")) key = "profile/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 3;
            }
            else if (url.Contains("cam4."))
            {
                string key = ".com/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 4;
            }
            else if (url.Contains("streamate."))
            {
                string key = "/cam/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 5;
            }
            else if (url.Contains("flirt4free."))
            {
                string key = "/?model=";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 6;
            }
            else if (url.Contains("myfreecams.com"))
            {
                string key = ".com/#";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 7;
            }
            else if (url.Contains("jerkmate."))
            {
                string key = "/cam/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 8;
            }
            else if (url.Contains("cams.com"))
            {
                string key = "com/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("#", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 9;
            }
            else if (url.Contains("camster."))
            {
                string key = "/?model=";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 10;
            }
            else if (url.Contains("freeoneslive."))
            {
                string key = "/?model=";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 11;
            }
            else if (url.Contains("eplay."))
            {
                string key = "com/";
                int start = URL_String.IndexOf(key) + key.Length;
                int end = URL_String.IndexOf("/", start);
                str1 = end >= 0 ? URL_String[start..end] : URL_String[start..];
                num1 = 12;
            }

            return (str1, num1);
        }

        internal static bool Site_Is_Galerie(string URL_String, string HTML_String, Class_Model Model_Class)
        {
            if (URL_String.Contains("chaturbate"))
                return Chaturbate.IsGalerie(URL_String, HTML_String, Model_Class).Result;
            else if (URL_String.Contains("stripchat"))
                return Stripchat.IsGalerie(URL_String, HTML_String, Model_Class).Result;
            else if (URL_String.Contains("camsoda"))
                return Camsoda.IsGalerie(URL_String, HTML_String, Model_Class).Result;

            return false;
        }

        internal static void Download_Galerie_Movie(string URL_String, string HTML_String, Class_Model Model_Class)
        {
            if (URL_String.Contains("chaturbate"))
                Chaturbate.Galerie_Movie_Download(URL_String, HTML_String, Model_Class);
            else if (URL_String.Contains("stripchat"))
                Stripchat.Galerie_Movie_Download(URL_String, HTML_String, Model_Class);
            else if (URL_String.Contains("camsoda"))
                Camsoda.Galerie_Movie_Download(URL_String, HTML_String, Model_Class);
        }

        internal static void Website_Load()
        {
            Website_List = new List<Class_Website>
            {
                new Class_Website(-1, TXT.TXT_Description("Modelordner"), "",
                    Resources.Folder16, 0, 0,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "0", "true")),
                    "", "", "", "", "", false),

                new Class_Website(0, "Chaturbate", Chaturbate.Site_URL(false),
                    Resources.Chaturbate, 250, 5000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "0", "true")),
                    "chaturbate.", Chaturbate.Site_URL() + "{0}",
                    "https://chaturbate.com/in/?tour=g4pe&campaign=NbZCW&track=default",
                    "", "https://chaturbate.com/{0}", false),

                new Class_Website(1, "Camsoda", "https://camsoda.com/",
                    Resources.Camsoda, 30000, 30000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "1", "true")),
                    "camsoda.", "https://camsoda.com/{0}", "https://camsoda.com", "",
                    "https://camsoda.com/{0}", true),

                new Class_Website(2, "Stripchat", "https://stripchat.com/",
                    Resources.Stripchat, 30000, 30000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "2", "true")),
                    "stripchat.", "https://stripchat.com/{0}", "https://stripchat.com", "&_HLS_msn={0}",
                    "https://stripchat.com/{0}", true),

                new Class_Website(3, "BongaCams", "https://bongacams.com/",
                    Resources.Bongacams, 250, 5000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "3", "true")),
                    "bongacams.", "https://bongacams.com/{0}", "https://bongacams.com", "",
                    "https://bongacams.com/{0}", true),

                new Class_Website(4, "Cam4", "https://cam4.com/",
                    Resources.Cam4, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "4", "true")),
                    "cam4.", "https://cam4.com/{0}", "https://cam4.com/", "",
                    "https://cam4.com/{0}", false),

                new Class_Website(5, "Streamate", "https://streamate.com/",
                    Resources.Streammate, 20000, 20000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "5", "true")),
                    "streamate.", "https://streamate.com/cam/{0}", "https://streamate.com", "",
                    "https://streamate.com/cam/{0}", false),

                new Class_Website(6, "Flirt4Free", "https://flirt4free.com/",
                    Resources.Flirt4Free, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "6", "true")),
                    "flirt4free.", "https://flirt4free.com/?model={0}", "https://flirt4free.com", "",
                    "https://flirt4free.com/?model={0}", false),

                new Class_Website(7, "MyFreeCams", "https://myfreecams.com/",
                    Resources.mfc, 500, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "7", "true")),
                    "myfreecams.", "https://myfreecams.com/#{0}", "https://myfreecams.com", "",
                    "https://myfreecams.com/#{0}", false),

                new Class_Website(8, "Jerkmate", "https://jerkmate.com/",
                    Resources.Jerkmate, 20000, 20000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "8", "true")),
                    "jerkmate.", "https://jerkmate.com/cam/{0}", "https://jerkmate.com", "",
                    "https://jerkmate.com/cam/{0}", false),

                new Class_Website(9, "Cams.com", "https://cams.com/",
                    Resources.CamsCom, 30000, 30000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "9", "true")),
                    "cams.", "https://cams.com/{0}", "https://cams.com", "",
                    "https://cams.com/{0}", false),

                new Class_Website(10, "Camster.com", "https://camster.com/",
                    Resources.Camster16, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "10", "true")),
                    "camster.", "https://camster.com/?model={0}", "https://camster.com", "",
                    "https://camster.com/?model={0}", false),

                new Class_Website(11, "FreeOnesLIVE.com", "https://freeoneslive.com/",
                    Resources.fol16, 5000, 5000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "11", "true")),
                    "freeoneslive.", "https://freeoneslive.com/?model={0}", "https://freeoneslive.com", "",
                    "https://freeoneslive.com/?model={0}", false),

                new Class_Website(12, "eplay.com", "https://eplay.com/",
                    Resources.eplay16, 10000, 10000,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Showall", "12", "true")),
                    "eplay.", "https://eplay.com/{0}/live", "https://eplay.com", "",
                    "https://eplay.com/{0}/live", false)
            };
        }
    }
}
