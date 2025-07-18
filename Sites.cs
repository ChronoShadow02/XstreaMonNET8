using System.Diagnostics;
using static XstreaMonNET8.Control_Stream;

namespace XstreaMonNET8
{
    internal sealed class Sites
    {
        internal static List<Class_Website> Website_List  =
            [
            new Class_Website(-1, TXT.TXT_Description("Modelordner"), "", Resources.Folder16, 0, 0, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "0", "True")), "", "", "", "", "", false),
            new Class_Website(0, "Chaturbate", Chaturbate.Site_URL(false), Resources.Chaturbate, 250, 5000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "0", "True")), "chaturbate.", Chaturbate.Site_URL() + "{0}", "https://chaturbate.com/in/?tour=g4pe&campaign=NbZCW&track=default", "", "https://chaturbate.com/{0}", false),
            new Class_Website(1, "Camsoda", "https://camsoda.com/", Resources.Camsoda, 30000, 30000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "1", "True")), "camsoda.", "https://camsoda.com/{0}", "https://camsoda.com", "", "https://camsoda.com/{0}", true),
            new Class_Website(2, "Stripchat", "https://stripchat.com/", Resources.Stripchat, 30000, 30000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "2", "True")), "stripchat.", "https://stripchat.com/{0}", "https://stripchat.com", "&_HLS_msn={0}", "https://stripchat.com/{0}", true),
            new Class_Website(3, "BongaCams", "https://bongacams.com/", Resources.Bongacams, 250, 5000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "3", "True")), "bongacams.", "https://bongacams.com/{0}", "https://bongacams.com", "", "https://bongacams.com/{0}", true),
            new Class_Website(4, "Cam4", "https://cam4.com/", Resources.Cam4, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "4", "True")), "cam4.", "https://cam4.com/{0}", "https://cam4.com/", "", "https://cam4.com/{0}", false),
            new Class_Website(5, "Streamate", "https://streamate.com/", Resources.Streammate, 20000, 20000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "5", "True")), "streamate.", "https://streamate.com/cam/{0}", "https://streamate.com", "", "https://streamate.com/cam/{0}", false),
            new Class_Website(6, "Flirt4Free", "https://flirt4free.com/", Resources.Flirt4Free, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "6", "True")), "flirt4free.", "https://flirt4free.com/?model={0}", "https://flirt4free.com", "", "https://flirt4free.com/?model={0}", false),
            new Class_Website(7, "MyFreeCams", "https://myfreecams.com/", Resources.mfc, 500, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "7", "True")), "myfreecams.", "https://myfreecams.com/#{0}", "https://myfreecams.com", "", "https://myfreecams.com/#{0}", false),
            new Class_Website(8, "Jerkmate", "https://jerkmate.com/", Resources.Jerkmate, 20000, 20000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "8", "True")), "jerkmate.", "https://jerkmate.com/cam/{0}", "https://jerkmate.com", "", "https://jerkmate.com/cam/{0}", false),
            new Class_Website(9, "Cams.com", "https://cams.com/", Resources.CamsCom, 30000, 30000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "9", "True")), "cams.", "https://cams.com/{0}", "https://cams.com", "", "https://cams.com/{0}", false),
            new Class_Website(10, "Camster.com", "https://camster.com/", Resources.Camster16, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "10", "True")), "camster.", "https://camster.com/?model={0}", "https://camster.com", "", "https://camster.com/?model={0}", false),
            new Class_Website(11, "FreeOnesLIVE.com", "https://freeoneslive.com/", Resources.fol16, 5000, 5000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "11", "True")), "freeoneslive.", "https://freeoneslive.com/?model={0}", "https://freeoneslive.com", "", "https://freeoneslive.com/?model={0}", false),
            new Class_Website(12, "eplay.com", "https://eplay.com/", Resources.eplay16, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "12", "True")), "eplay.", "https://eplay.com/{0}/live", "https://eplay.com", "", "https://eplay.com/{0}/live", false)
            ];

        internal static Class_Website Website_Find(int Platform_ID)
        {
            Class_Website classWebsite = null;
            try
            {
                foreach (Class_Website website in Website_List)
                {
                    if (website.Pro_ID == Platform_ID)
                    {
                        classWebsite = website;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Website_Find: {ex.Message}");
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
                Console.WriteLine($"Error in WebSiteOpen: {ex.Message}");
            }
        }

        internal static void WebOpen(string WebAdresse)
        {
            try
            {
                switch (ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Browser", "Product", "-1")))
                {
                    case -1:
                        CamBrowser_Open(WebAdresse);
                        break;
                    case 0:
                        Process.Start(WebAdresse);
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
                        Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Lovense\\Browser\\chrome.exe", WebAdresse);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in WebOpen: {ex.Message}");
            }
        }

        internal static async void CamBrowser_Open(string WebAdresse)
        {
            await Task.CompletedTask;
            CamBrowser camBrowser = new CamBrowser
            {
                WebView_Source = WebAdresse,
                WindowState = FormWindowState.Maximized,
                StartPosition = FormStartPosition.CenterParent
            };
            camBrowser.Show();
        }

        public static StreamAdresses Stream_Adressen_Load(StreamAdresses Model_Stream)
        {
            StreamAdresses streamAdresses = null!;
            try
            {
                switch (Model_Stream.Pro_Model_Website)
                {
                    case 0:
                        streamAdresses = Chaturbate.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 1:
                        streamAdresses = Camsoda.StreamAdresses(Model_Stream).Result;
                        break;
                    case 2:
                        streamAdresses = Stripchat.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 3:
                        streamAdresses = Bongacams.StreamAdresses(Model_Stream).Result;
                        break;
                    case 4:
                        streamAdresses = Cam4.StreamAdresses(Model_Stream).Result;
                        break;
                    case 5:
                        streamAdresses = Streamate.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 6:
                        streamAdresses = Flirt4Free.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 7:
                        streamAdresses = MyFreeCams.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 8:
                        streamAdresses = Jerkmate.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 9:
                        streamAdresses = CamsCom.StreamAdresses(Model_Stream).Result;
                        break;
                    case 10:
                        streamAdresses = Camster.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 11:
                        streamAdresses = Freeoneslive.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 12:
                        streamAdresses = EPlay.Stream_Adresses(Model_Stream).Result;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Stream_Adressen_Load: {ex.Message}");
            }
            return streamAdresses;
        }

        public static StreamAdresses Stream_Adressen_Load(StreamAdresses Model_Stream, string HTML_Txt)
        {
            StreamAdresses streamAdresses = null;
            try
            {
                switch (Model_Stream.Pro_Model_Website)
                {
                    case 0:
                        streamAdresses = Chaturbate.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 1:
                        streamAdresses = Camsoda.StreamAdresses(Model_Stream).Result;
                        break;
                    case 2:
                        streamAdresses = Stripchat.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 3:
                        streamAdresses = Bongacams.StreamAdresses(Model_Stream, HTML_Txt).Result;
                        break;
                    case 4:
                        streamAdresses = Cam4.StreamAdresses(Model_Stream).Result;
                        break;
                    case 5:
                        streamAdresses = Streamate.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 6:
                        streamAdresses = Flirt4Free.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 7:
                        streamAdresses = MyFreeCams.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 8:
                        streamAdresses = Jerkmate.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 9:
                        streamAdresses = CamsCom.StreamAdresses(Model_Stream).Result;
                        break;
                    case 10:
                        streamAdresses = Camster.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 11:
                        streamAdresses = Freeoneslive.Stream_Adresses(Model_Stream).Result;
                        break;
                    case 12:
                        streamAdresses = EPlay.Stream_Adresses(Model_Stream).Result;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Stream_Adressen_Load: {ex.Message}");
            }
            return streamAdresses;
        }

        public static int Model_Online(string modelName, int websiteId)
        {
            var result = Class_Model_List.Class_Model_Find(websiteId, modelName.ToLower()).Result;
            int num;

            if (result != null)
            {
                if (DateTime.Compare(
                        result.Timer_Online_Change!.BGW_Last_Check,
                        DateTime.Now.AddSeconds(result.Timer_Online_Change.Pro_Timer_Intervall)) < 0)
                {
                    if (!Parameter.Recording_Stop)
                    {
                        switch (websiteId)
                        {
                            case 0:
                                num = Chaturbate.Online(modelName).Result;
                                break;
                            case 1:
                                num = Camsoda.Online(modelName).Result;
                                break;
                            case 2:
                                num = Stripchat.Online(modelName).Result;
                                break;
                            case 3:
                                num = Bongacams.Online(modelName).Result;
                                break;
                            case 4:
                                num = Cam4.Online(modelName).Result;
                                break;
                            case 5:
                                num = Streamate.Online(modelName).Result;
                                break;
                            case 6:
                                num = Flirt4Free.Online(modelName).Result;
                                break;
                            case 7:
                                num = MyFreeCams.Online(modelName).Result;
                                break;
                            case 8:
                                num = Jerkmate.Online(modelName).Result;
                                break;
                            case 9:
                                num = CamsCom.Online(modelName).Result;
                                break;
                            case 10:
                                num = Camster.Online(modelName).Result;
                                break;
                            case 11:
                                num = Freeoneslive.Online(modelName).Result;
                                break;
                            case 12:
                                num = EPlay.Online(modelName).Result;
                                break;
                            default:
                                num = 0;
                                break;
                        }
                    }
                    else
                    {
                        num = 0;
                    }
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
            int num = 0;
            if (!Parameter.Recording_Stop)
            {
                switch (Website_ID)
                {
                    case 0:
                        num = Chaturbate.Online(Model_Name).Result;
                        break;
                    case 1:
                        num = Camsoda.Online(Model_Name).Result;
                        break;
                    case 2:
                        num = Stripchat.Online(Model_Name).Result;
                        break;
                    case 3:
                        num = Bongacams.Online(Model_Name, HTML_String).Result;
                        break;
                    case 4:
                        num = Cam4.Online(Model_Name).Result;
                        break;
                    case 5:
                        num = Streamate.Online(Model_Name).Result;
                        break;
                    case 6:
                        num = Flirt4Free.Online(Model_Name).Result;
                        break;
                    case 7:
                        num = MyFreeCams.Online(Model_Name).Result;
                        break;
                    case 8:
                        num = Jerkmate.Online(Model_Name).Result;
                        break;
                    case 9:
                        num = CamsCom.Online(Model_Name).Result;
                        break;
                    case 10:
                        num = Camster.Online(Model_Name).Result;
                        break;
                    case 11:
                        num = Freeoneslive.Online(Model_Name).Result;
                        break;
                    case 12:
                        num = EPlay.Online(Model_Name).Result;
                        break;
                }
            }
            return num;
        }

        internal static async Task<Image> ImageFromWeb(Class_Model Model_Class)
        {
            Bitmap bitmap = null;
            try
            {
                if (Model_Class != null && !Parameter.Recording_Stop)
                {
                    switch (Model_Class.Pro_Website_ID)
                    {
                        case 0:
                            bitmap = (Bitmap)/*await*/ Chaturbate.Image_FromWeb(Model_Class.Pro_Model_Name.ToLower());
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
                        bitmap = (Bitmap)await Image_FromStream(Model_Class);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ImageFromWeb: {ex.Message}");
            }
            return bitmap;
        }

        internal static async Task<Image> Image_FromStream(Class_Model Model_Class)
        {
            await Task.CompletedTask;
            string path = Parameter.CommonPath + "\\Temp\\IFW_" + Model_Class.Pro_Model_Name + ".jpg";
            try
            {
                if (File.Exists(path))
                {
                    using (FileStream fileStream = new FileStream(path, FileMode.Open))
                    {
                        Bitmap bitmap = new Bitmap(fileStream);
                        File.Delete(path);
                        return bitmap;
                    }
                }

                string proModelFfmpegPath = Model_Class.Pro_Model_FFMPEG_Path;
                if (proModelFfmpegPath.Length > 0)
                {
                    ProcessStartInfo processStartInfo = new()
                    {
                        FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecordStream.exe"),
                        Arguments = $"-i \"{proModelFfmpegPath}\" -f image2 -vf fps=1 \"{path}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    using (Process process = new Process())
                    {
                        process.StartInfo = processStartInfo;
                        process.Start();
                        process.WaitForExit(5000);
                        if (!process.HasExited && Parameter.Task_Runs(process.Id))
                        {
                            process.Kill();
                        }
                    }

                    if (File.Exists(path))
                    {
                        using (FileStream fileStream = new FileStream(path, FileMode.Open))
                        {
                            byte[] numArray = new byte[fileStream.Length];
                            fileStream.Read(numArray, 0, numArray.Length);
                            return new Bitmap(new MemoryStream(numArray));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Image_FromStream: {ex.Message}");
            }
            return null;
        }

        internal static int Resolution_Find(string[] Playlist, string ChunkString)
        {
            int num = 0;
            foreach (string str in Playlist)
            {
                if (str.ToLower().Contains(ChunkString.Trim().ToLower()))
                {
                    string str2 = str.Replace(" ", "");
                    if (str2.Contains("256x144") || str2.Contains("284x160") || str2.Contains("320x180") || str2.Contains("426x240") || str2.Contains("480x270"))
                        num = 1;
                    else if (str2.Contains("640x360") || str2.Contains("768x432") || str2.Contains("854x480") || str2.Contains("960x540"))
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
            bool flag = false;
            switch (WebSite_ID)
            {
                case 0:
                    flag = Chaturbate.IsLogin(HTML_String).Result;
                    break;
                case 1:
                    flag = Camsoda.IsLogin(HTML_String).Result;
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    flag = false;
                    break;
            }
            return flag;
        }

        internal static (string, int) Site_Model_URL_Read(string URL_String)
        {
            string modelName = "";
            int websiteId = -1;

            if (URL_String.ToLower().Contains("chaturbate."))
            {
                string str = "chaturbate.com/";
                if (URL_String.Contains(str))
                {
                    if (URL_String.Contains("&room="))
                        str = "&room=";
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 0;
            }
            else if (URL_String.ToLower().Contains("camsoda."))
            {
                string str = "camsoda.com/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 1;
            }
            else if (URL_String.ToLower().Contains("stripchat."))
            {
                string str = "stripchat.com/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 2;
            }
            else if (URL_String.ToLower().Contains("bongacams."))
            {
                string str = "bongacams.com/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 3;
            }
            else if (URL_String.ToLower().Contains("cam4."))
            {
                string str = ".com/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 4;
            }
            else if (URL_String.ToLower().Contains("streamate."))
            {
                string str = "/cam/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 5;
            }
            else if (URL_String.ToLower().Contains("flirt4free."))
            {
                string str = "/?model=";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 6;
            }
            else if (URL_String.ToLower().Contains("myfreecams.com"))
            {
                string str = ".com/#";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 7;
            }
            else if (URL_String.ToLower().Contains("jerkmate."))
            {
                string str = "/cam/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 8;
            }
            else if (URL_String.ToLower().Contains("cams.com"))
            {
                string str = "com/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("#", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 9;
            }
            else if (URL_String.ToLower().Contains("camster."))
            {
                string str = "/?model=";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 10;
            }
            else if (URL_String.ToLower().Contains("freeoneslive."))
            {
                string str = "/?model=";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 11;
            }
            else if (URL_String.ToLower().Contains("eplay."))
            {
                string str = "com/";
                if (URL_String.Contains(str))
                {
                    int startIndex = URL_String.IndexOf(str) + str.Length;
                    int endIndex = URL_String.IndexOf("/", startIndex);
                    modelName = endIndex != -1 ? URL_String.Substring(startIndex, endIndex - startIndex) : URL_String.Substring(startIndex);
                }
                websiteId = 12;
            }

            return (modelName, websiteId);
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
            Website_List =
            [
            new Class_Website(-1, TXT.TXT_Description("Modelordner"), "", Resources.Folder16, 0, 0, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "0", "True")), "", "", "", "", "", false),
            new Class_Website(0, "Chaturbate", Chaturbate.Site_URL(false), Resources.Chaturbate, 250, 5000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "0", "True")), "chaturbate.", Chaturbate.Site_URL() + "{0}", "https://chaturbate.com/in/?tour=g4pe&campaign=NbZCW&track=default", "", "https://chaturbate.com/{0}", false),
            new Class_Website(1, "Camsoda", "https://camsoda.com/", Resources.Camsoda, 30000, 30000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "1", "True")), "camsoda.", "https://camsoda.com/{0}", "https://camsoda.com", "", "https://camsoda.com/{0}", true),
            new Class_Website(2, "Stripchat", "https://stripchat.com/", Resources.Stripchat, 30000, 30000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "2", "True")), "stripchat.", "https://stripchat.com/{0}", "https://stripchat.com", "&_HLS_msn={0}", "https://stripchat.com/{0}", true),
            new Class_Website(3, "BongaCams", "https://bongacams.com/", Resources.Bongacams, 250, 5000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "3", "True")), "bongacams.", "https://bongacams.com/{0}", "https://bongacams.com", "", "https://bongacams.com/{0}", true),
            new Class_Website(4, "Cam4", "https://cam4.com/", Resources.Cam4, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "4", "True")), "cam4.", "https://cam4.com/{0}", "https://cam4.com/", "", "https://cam4.com/{0}", false),
            new Class_Website(5, "Streamate", "https://streamate.com/", Resources.Streammate, 20000, 20000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "5", "True")), "streamate.", "https://streamate.com/cam/{0}", "https://streamate.com", "", "https://streamate.com/cam/{0}", false),
            new Class_Website(6, "Flirt4Free", "https://flirt4free.com/", Resources.Flirt4Free, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "6", "True")), "flirt4free.", "https://flirt4free.com/?model={0}", "https://flirt4free.com", "", "https://flirt4free.com/?model={0}", false),
            new Class_Website(7, "MyFreeCams", "https://myfreecams.com/", Resources.mfc, 500, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "7", "True")), "myfreecams.", "https://myfreecams.com/#{0}", "https://myfreecams.com", "", "https://myfreecams.com/#{0}", false),
            new Class_Website(8, "Jerkmate", "https://jerkmate.com/", Resources.Jerkmate, 20000, 20000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "8", "True")), "jerkmate.", "https://jerkmate.com/cam/{0}", "https://jerkmate.com", "", "https://jerkmate.com/cam/{0}", false),
            new Class_Website(9, "Cams.com", "https://cams.com/", Resources.CamsCom, 30000, 30000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "9", "True")), "cams.", "https://cams.com/{0}", "https://cams.com", "", "https://cams.com/{0}", false),
            new Class_Website(10, "Camster.com", "https://camster.com/", Resources.Camster16, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "10", "True")), "camster.", "https://camster.com/?model={0}", "https://camster.com", "", "https://camster.com/?model={0}", false),
            new Class_Website(11, "FreeOnesLIVE.com", "https://freeoneslive.com/", Resources.fol16, 5000, 5000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "11", "True")), "freeoneslive.", "https://freeoneslive.com/?model={0}", "https://freeoneslive.com", "", "https://freeoneslive.com/?model={0}", false),
            new Class_Website(12, "eplay.com", "https://eplay.com/", Resources.eplay16, 10000, 10000, Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Showall", "12", "True")), "eplay.", "https://eplay.com/{0}/live", "https://eplay.com", "", "https://eplay.com/{0}/live", false)
            ];
        }
    }
}
