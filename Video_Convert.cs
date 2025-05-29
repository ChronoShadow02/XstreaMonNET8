using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace XstreaMonNET8
{
    public class Video_Convert
    {
        internal string Pri_Ziel_File = string.Empty;
        private readonly string Pri_Video_File = string.Empty;
        private WaitTask? Wait_Convert;
        private Timer _Convert_Count_Timer = new(30000);

        public delegate void Video_Convert_ReadyEventHandler();
        internal event Video_Convert_ReadyEventHandler? Video_Convert_Ready;

        private Timer Convert_Count_Timer
        {
            get => _Convert_Count_Timer;
            set
            {
                if (_Convert_Count_Timer != null)
                    _Convert_Count_Timer.Elapsed -= Convert_Check;

                _Convert_Count_Timer = value;

                if (_Convert_Count_Timer != null)
                    _Convert_Count_Timer.Elapsed += Convert_Check;
            }
        }

        internal Video_Convert(string videoFile, string videoExtension)
        {
            Convert_Count_Timer = new Timer(30000);
            try
            {
                if (!File.Exists(videoFile))
                    return;

                Pri_Video_File = videoFile;
                Pri_Ziel_File = videoFile.Replace(Path.GetExtension(videoFile), videoExtension);
                Convert_Count_Timer.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Video_Convert.New");
            }
        }

        private void Convert_Check(object? sender, ElapsedEventArgs e)
        {
            Convert_Count_Timer.Stop();
            int maxConversions = int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "MaxConversion", "5"), out int max) ? max : 5;

            if (Parameter.Pro_Convert_Count < maxConversions)
            {
                Parameter.Pro_Convert_Count++;
                _ = Convert_Start();
            }
            else
            {
                Convert_Count_Timer.Start();
            }
        }

        private async Task Convert_Start()
        {
            try
            {
                if (File.Exists(Pri_Ziel_File))
                    File.Delete(Pri_Ziel_File);

                string audioArgs = "";
                string audioInput = "";

                if (File.Exists(Pri_Video_File + "a"))
                {
                    int offset = await Media_Info.Start_Video_Timer(Pri_Video_File, Pri_Video_File + "a");
                    string offsetStr = Math.Abs(offset / 1000.0).ToString("0.###", System.Globalization.CultureInfo.InvariantCulture);

                    if (offset < 0)
                        audioArgs = $"-itsoffset {offsetStr} ";
                    else if (offset > 0)
                        audioInput = $"-itsoffset {offsetStr} ";

                    audioInput += $"-i \"{Pri_Video_File}a\"";
                }

                string exePath = Path.Combine(AppContext.BaseDirectory, "RecordStream.exe");
                string arguments = $"-loglevel warning -stats {audioArgs}-i \"{Pri_Video_File}\" {audioInput} -shortest -f mp4 -codec copy \"{Pri_Ziel_File}\"";

                ProcessStartInfo startInfo = new()
                {
                    FileName = exePath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False") != "True",
                    WindowStyle = IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False") == "True"
                        ? ProcessWindowStyle.Normal
                        : ProcessWindowStyle.Hidden
                };

                var process = Process.Start(startInfo);
                if (process != null)
                {
                    Wait_Convert = new WaitTask(process.Id, 2000);
                    Wait_Convert.Evt_Task_Ready += Convert_Ready;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Video_Convert.Convert_Start");
            }
        }

        private void Convert_Ready(int taskId)
        {
            try
            {
                if (File.Exists(Pri_Ziel_File) && File.Exists(Pri_Video_File))
                {
                    long originalSize = new FileInfo(Pri_Video_File).Length;

                    if (File.Exists(Pri_Video_File + "a"))
                        originalSize += new FileInfo(Pri_Video_File + "a").Length;

                    long resultSize = new FileInfo(Pri_Ziel_File).Length;

                    if (ValueBack.Get_Numeric_Similar(resultSize, originalSize, 5))
                    {
                        File.Delete(Pri_Video_File);
                        if (File.Exists(Pri_Video_File + "a"))
                            File.Delete(Pri_Video_File + "a");
                        if (Pri_Video_File != Pri_Ziel_File)
                            File.Delete(Pri_Video_File);
                        if (File.Exists(Pri_Video_File + ".vdb"))
                            File.Delete(Pri_Video_File + ".vdb");

                        Video_Convert_Ready?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Video_Convert.Convert_Ready");
            }
            finally
            {
                Parameter.Pro_Convert_Count--;
            }
        }
    }
}
