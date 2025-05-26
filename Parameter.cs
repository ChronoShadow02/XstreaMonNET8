using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net;
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

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

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
            using FileStream input = new(path, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new(input);
            return new Bitmap(new MemoryStream(reader.ReadBytes((int)input.Length)));
        }

        internal static int Record_Max_Lenght()
        {
            if (_Record_Max_Lenght == -1)
            {
                int num = Value_Back.get_CInteger(INI_File.Read(INI_Common, "Record", "RecordTime", "-1"));
                if (num == -1)
                {
                    INI_File.Write(INI_Common, "Directory", "Records", "30");
                }
                _Record_Max_Lenght = num;
            }
            return _Record_Max_Lenght;
        }

        internal static void Error_Message(Exception ex, string sender)
        {
            if (!Debug_Modus) return;

            try
            {
                using StreamWriter writer = File.AppendText(Path.Combine(CommonPath, "log.txt"));
                writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                writer.WriteLine($"Sender: {sender}");
                if (ex != null)
                {
                    writer.WriteLine($"Exception: {ex.Message}");
                    writer.WriteLine($"Source: {ex.Source}");
                    writer.WriteLine($"TargetSite: {ex.TargetSite}");
                    writer.WriteLine("-------------------------------");
                }
            }
            catch { /* Logging silently fails */ }
        }

        internal static void Wait(int interval)
        {
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < interval)
                Application.DoEvents();
            sw.Stop();
        }

        internal static bool Task_Runs(int processId)
        {
            if (processId < 1)
                return false;

            try
            {
                using Process proc = Process.GetProcessById(processId);
                return proc.ProcessName.StartsWith("CRStreamRec") || proc.ProcessName.StartsWith("RecordStream");
            }
            catch
            {
                return false;
            }
        }

        internal static async Task<bool> Task_Quit(int processId)
        {
            await Task.CompletedTask;

            if (Value_Back.get_CInteger(processId) <= 0)
                return false;

            if (!Task_Runs(processId))
                return true;

            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.Id == processId && (proc.ProcessName.StartsWith("CRStreamRec") || proc.ProcessName.StartsWith("RecordStream")))
                {
                    try
                    {
                        if (proc.ProcessName.StartsWith("CRStreamRec"))
                        {
                            proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            for (int i = 0; i < 10 && Task_Runs(processId); i++)
                            {
                                proc.Kill();
                                Wait(500);
                            }
                        }
                        else if (proc.ProcessName.StartsWith("RecordStream"))
                        {
                            for (int i = 0; i < 60 && Task_Runs(processId); i++)
                            {
                                Interaction.AppActivate(processId);
                                SendKeys.Send("q");
                                Wait(1000);
                            }

                            if (Task_Runs(processId))
                            {
                                Interaction.AppActivate(processId);
                                SendKeys.Send("^c");
                                SendKeys.Send("^c");
                                proc.WaitForExit(10000);
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Error_Message(ex, "Parameter.Task_Quit");
                        if (Task_Runs(processId))
                        {
                            try
                            {
                                proc.WaitForExit(60000);
                            }
                            catch (Exception ex2)
                            {
                                Error_Message(ex2, "Parameter.Task_Quit");
                            }
                        }
                    }
                    break;
                }
            }

            return !Task_Runs(processId);
        }

        internal static Task<bool> URL_Response(string siteUrl)
        {
            return Task.Run(() => URL_Response_Thread(siteUrl));
        }

        internal static bool URL_Response_Thread(string siteUrl)
        {
            if (string.IsNullOrWhiteSpace(siteUrl))
                return false;

            string requestUrl = siteUrl.Replace("\\/", "/");
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Timeout = 2000;
                request.ReadWriteTimeout = 1000;
                request.Method = "GET";

                using var response = request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Design_Change(int designId)
        {
            switch (designId)
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
