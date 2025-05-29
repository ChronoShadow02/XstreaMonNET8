using System.Drawing.Imaging;
using Xabe.FFmpeg;

namespace XstreaMonNET8
{
    public class Class_MediaInfo : IDisposable
    {
        private bool _disposed;
        internal string Pro_Record_File { get; set; } = string.Empty;
        internal DateTime Pro_Record_Start { get; set; }
        internal DateTime Pro_Record_Ende { get; set; }
        internal int Pro_Record_Länge { get; set; }
        internal string Pro_Record_Resolution { get; set; } = string.Empty;
        internal string Pro_Record_FrameRate { get; set; } = string.Empty;
        internal string Pro_Record_Size { get; set; } = string.Empty;
        internal string Pro_Record_UserName { get; set; } = string.Empty;
        internal int Pro_Website_ID { get; set; }
        internal int Pro_Duration { get; set; }
        internal int Pro_Heigth { get; set; }
        internal int Pro_Widht { get; set; }

        internal byte[] Pro_TimeLine_Byte => ValueBack.Get_CImageToByte(Pro_TimeLine_Image, ImageFormat.Jpeg);
        internal Image Pro_TimeLine_Image => Vorschau.Vorschau_Image_Create(Pro_Record_File, 400, 26, Pro_Duration, Pro_Widht > Pro_Heigth).Result is Image img ? new Bitmap(img) : new Bitmap(1, 1);

        internal byte[] Pro_Tiles_Byte => ValueBack.Get_CImageToByte(Pro_Tiles_Image, ImageFormat.Jpeg);
        internal Image Pro_Tiles_Image =>
            Vorschau.Vorschau_Tiles_Create(Pro_Record_File, 340, 224, Pro_Duration).Result is Image img
            ? new Bitmap(img)
            : new Bitmap(1, 1);

        internal byte[] Pro_Preview_Byte => ValueBack.Get_CImageToByte(Pro_Preview_Image, ImageFormat.Jpeg);
        internal Image Pro_Preview_Image => new Bitmap(
            CreateThumbFromVideo.GenerateVTN(Pro_Record_File, 340, 194).Result);

        internal Class_MediaInfo(string recordFile, string recordUserName, int recordWebsiteId, DateTime recordBeginn)
        {
            _disposed = false;

            try
            {
                Pro_Record_File = recordFile;
                Pro_Record_UserName = recordUserName;
                Pro_Website_ID = recordWebsiteId;
                Pro_Record_Start = recordBeginn;

                var task = Task.Run(async () =>
                {
                    IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(recordFile);
                    var videoStream = mediaInfo.VideoStreams.FirstOrDefault();

                    if (videoStream != null)
                    {
                        Pro_Duration = (int)mediaInfo.Duration.TotalSeconds;
                        Pro_Heigth = videoStream.Height;
                        Pro_Widht = videoStream.Width;
                        Pro_Record_Ende = recordBeginn.AddSeconds(Pro_Duration);
                        Pro_Record_Länge = (int)Math.Round(Pro_Duration / 60.0);
                        Pro_Record_Resolution = $"{Pro_Widht}x{Pro_Heigth}";
                        Pro_Record_FrameRate = videoStream.Framerate.ToString("0.##");
                    }
                    else
                    {
                        Parameter.Error_Message(null!, $"Class_MediaInfo.NoVideo {recordFile}");
                    }
                });
                task.Wait();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_MediaInfo.New");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
        }

        ~Class_MediaInfo()
        {
            Dispose(false);
        }
    }
}
