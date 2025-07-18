using System.Diagnostics;
using System.Runtime.InteropServices;

namespace XstreaMonNET8
{
    internal static class Parameter
    {
        internal static string CommonPath;
        internal static string INI_Common;
        internal static bool Debug_Modus = false;
        internal static Lizenz Programlizenz;
        internal static Color Fore_Color_Dark;
        internal static Color Fore_Color_Hell;
        internal static bool Recording_Stop = false;
        internal static int Pro_Convert_Count = 0;
        private static int _Record_Max_Lenght = -1;

        private static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public static async void FlushMemory()
        {
            await Task.CompletedTask;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        internal static Bitmap LoadBitmap(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    byte[] data = reader.ReadBytes((int)fs.Length);
                    return new Bitmap(new MemoryStream(data));
                }
            }
        }

        internal static int Record_Max_Lenght()
        {
            if (_Record_Max_Lenght == -1)
            {
                int num = ValueBack.Get_CInteger(IniFile.Read(INI_Common, "Record", "RecordTime", "-1"));
                if (num == -1)
                {
                    IniFile.Write(INI_Common, "Directory", "Records", "30");
                }
                _Record_Max_Lenght = num;
            }
            return _Record_Max_Lenght;
        }

        internal static void Error_Message(Exception ex, string sender)
        {
            if (Debug_Modus)
            {
                try
                {
                    string logPath = Path.Combine(CommonPath, "log.txt");
                    using (StreamWriter writer = File.AppendText(logPath))
                    {
                        writer.WriteLine($"{DateTime.Now:HH:mm:ss dd/MM/yyyy}");
                        writer.WriteLine($"Sender: {sender}");
                        if (ex != null)
                        {
                            writer.WriteLine($"Exception: {ex.Message}");
                            writer.WriteLine($"Source: {ex.Source}");
                            writer.WriteLine($"TargetSite: {ex.TargetSite}");
                            writer.WriteLine("-------------------------------");
                        }
                    }
                }
                catch
                {
                    // Ignore logging errors
                }
            }
        }

        internal static void Wait(int interval)
        {
            Stopwatch sw = new();
            sw.Start();
            while (sw.ElapsedMilliseconds < interval)
            {
                Application.DoEvents();
            }
            sw.Stop();
        }

        internal static bool Task_Runs(int Prozess_ID)
        {
            if (Prozess_ID < 1)
                return false;

            try
            {
                Process proc = Process.GetProcessById(Prozess_ID);
                bool result = proc.ProcessName.StartsWith("CRStreamRec") || proc.ProcessName.StartsWith("RecordStream");
                proc.Dispose();
                return result;
            }
            catch
            {
                return false;
            }
        }

        internal static async Task<bool> Task_Quit(int Prozess_ID)
        {
            await Task.CompletedTask;

            if (ValueBack.Get_CInteger(Prozess_ID) <= 0)
                return false;

            if (!Task_Runs(Prozess_ID))
                return true;

            Process[] processes = Process.GetProcesses();
            foreach (Process proc in processes)
            {
                if (proc.Id == Prozess_ID &&
                   (proc.ProcessName.StartsWith("CRStreamRec") || proc.ProcessName.StartsWith("RecordStream")))
                {
                    try
                    {
                        if (proc.ProcessName.StartsWith("CRStreamRec"))
                        {
                            int tries = 0;
                            while (Task_Runs(Prozess_ID))
                            {
                                if (tries < 10)
                                {
                                    proc.Kill();
                                    Wait(500);
                                    tries++;
                                }
                                else
                                {
                                    proc.Kill();
                                    break;
                                }
                            }
                            return true;
                        }
                        else if (proc.ProcessName.StartsWith("RecordStream"))
                        {
                            int tries = 0;
                            while (Task_Runs(Prozess_ID))
                            {
                                if (tries < 60)
                                {
                                    // No Interaction.AppActivate, so fallback to Kill
                                    proc.Kill();
                                    Wait(1000);
                                    tries++;
                                }
                                else
                                {
                                    proc.Kill();
                                    break;
                                }
                            }
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Error_Message(ex, "Parameter.Task_Quit");
                        proc.WaitForExit(10000);
                    }
                }
            }
            return !Task_Runs(Prozess_ID);
        }

        internal static async Task<bool> URL_Response(string Site_URL)
        {
            if (string.IsNullOrWhiteSpace(Site_URL))
                return false;

            Site_URL = Site_URL.Replace("\\/", "/");

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(Site_URL).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static void Design_Change(int Design_ID)
        {
            switch (Design_ID)
            {
                case 0:
                case 1:
                    Fore_Color_Dark = Color.Black;
                    Fore_Color_Hell = Color.Gray;
                    break;
                case 2:
                    Fore_Color_Dark = Color.White;
                    Fore_Color_Hell = Color.Gray;
                    break;
            }
        }
    }
}
