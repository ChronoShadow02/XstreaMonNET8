using System.Diagnostics;

namespace XstreaMonNET8
{
    public class CreateThumbFromVideo
    {
        public static Bitmap GenerateThumb(string file, int picWidth, int picHeight)
        {
            try
            {
                if (!File.Exists(file)) return null!;

                string path = Path.Combine(Parameter.CommonPath, "Temp", Guid.NewGuid().ToString());
                if (File.Exists(path))
                    File.Delete(path);

                var psi = new ProcessStartInfo
                {
                    FileName = Path.Combine(AppContext.BaseDirectory, "RecordStream.exe"),
                    Arguments = $" -loglevel warning -stats -ss 2 -i \"{file}\" -f image2 -vframes 1 -vf scale=-1:{picHeight} -y \"{path}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (var process = new Process { StartInfo = psi })
                {
                    process.Start();
                    process.WaitForExit();
                }

                if (!File.Exists(path)) return null!;

                using var fs = new FileStream(path, FileMode.Open);
                var bmp = new Bitmap(fs);
                File.Delete(path);
                return bmp;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CreateThumbFromVideo.GenerateThumb");
                return null!;
            }
        }

        public static async Task<Bitmap> GenerateVTN(string file, int picWidth, int picHeight)
        {
            await Task.CompletedTask;

            try
            {
                if (!File.Exists(file)) return null!;

                string path = Path.Combine(Parameter.CommonPath, "Temp", Guid.NewGuid().ToString());
                var psi = new ProcessStartInfo
                {
                    FileName = Path.Combine(AppContext.BaseDirectory, "RecordStream.exe"),
                    Arguments = $" -loglevel warning -stats -ss 2 -i \"{file}\" -f image2 -vframes 1 -vf scale=-1:{picHeight} -y \"{path}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (var process = new Process { StartInfo = psi })
                {
                    process.Start();
                    var waitTask = new WaitTask(process.Id, 100);
                    while (!waitTask.Pro_Task_Ready)
                        Application.DoEvents();
                }

                if (!File.Exists(path)) return null!;

                using var fs = new FileStream(path, FileMode.Open);
                var bmp = new Bitmap(fs);
                File.Delete(path);
                return bmp;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CreateThumbFromVideo.GenerateVTN");
                return null!;
            }
        }

        public static async Task<Bitmap> GenerateThumb(string file, int second, int width, int height)
        {
            await Task.CompletedTask;

            try
            {
                string time = ValueBack.MinutesToTime((float)second);
                string path = Path.Combine(Parameter.CommonPath, "Temp", Guid.NewGuid().ToString());

                if (!File.Exists(path))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = Path.Combine(AppContext.BaseDirectory, "RecordStream.exe"),
                        Arguments = $" -ss {time} -i \"{file}\" -f image2 -vframes 1 -s {width}x{height} -y \"{path}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    using (var process = new Process { StartInfo = psi })
                    {
                        process.Start();
                        var waitTask = new WaitTask(process.Id, 10);
                        while (!waitTask.Pro_Task_Ready)
                            Application.DoEvents();
                    }

                    if (!File.Exists(path)) return null!;

                    using var fs = new FileStream(path, FileMode.Open);
                    var bmp = new Bitmap(fs);
                    File.Delete(path);
                    return bmp;
                }

                using (var fs = new FileStream(path, FileMode.Open))
                {
                    return new Bitmap(fs);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CreateThumbFromVideo.GenerateThumb2");
                return null!;
            }
        }
    }
}
