using MediaInfoNET;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
//TODO: ver el MediaInfoNET o su alternativa
namespace XstreaMonNET8
{
    public class Class_MediaInfo : IDisposable
    {
        private bool _disposed;

        internal string Pro_Record_File { get; set; }
        internal DateTime Pro_Record_Start { get; set; }
        internal DateTime Pro_Record_Ende { get; set; }
        internal int Pro_Record_Länge { get; set; }
        internal string Pro_Record_Resolution { get; set; }
        internal string Pro_Record_FrameRate { get; set; }
        internal string Pro_Record_Size { get; set; }
        internal string Pro_Record_UserName { get; set; }
        internal int Pro_Website_ID { get; set; }
        internal int Pro_Duration { get; set; }
        internal int Pro_Heigth { get; set; }
        internal int Pro_Widht { get; set; }

        internal byte[] Pro_TimeLine_Byte => ValueBack.Get_CImageToByte(Pro_TimeLine_Image, ImageFormat.Jpeg);

        internal Image Pro_TimeLine_Image => new Bitmap(
            Vorschau.Vorschau_Image_Create(Pro_Record_File, 400, 26, Pro_Duration, Pro_Widht > Pro_Heigth).Result);

        internal byte[] Pro_Tiles_Byte => ValueBack.Get_CImageToByte(Pro_Tiles_Image, ImageFormat.Jpeg);

        internal Image Pro_Tiles_Image => new Bitmap(
            Vorschau.Vorschau_Tiles_Create(Pro_Record_File, 340, 224, Pro_Duration).Result);

        internal byte[] Pro_Preview_Byte => ValueBack.Get_CImageToByte(Pro_Preview_Image, ImageFormat.Jpeg);

        internal Image Pro_Preview_Image => new Bitmap(
            CreateThumbFromVideo.GenerateVTN(Pro_Record_File, 340, 194).Result);

        internal Class_MediaInfo(string Record_File, string Record_user_Name, int Record_Website_ID, DateTime Record_Beginn)
        {
            _disposed = false;

            try
            {
                Pro_Record_File = Record_File;
                Pro_Record_UserName = Record_user_Name;
                Pro_Website_ID = Record_Website_ID;
                Pro_Record_Start = Record_Beginn;

                MediaFile mediaFile = new MediaFile(Record_File);
                if (mediaFile.Video.Count == 1)
                {
                    Pro_Duration = ValueBack.Get_Duration_To_Seconds(mediaFile.Video[0].Properties["Duration"]);
                    Pro_Heigth = ValueBack.Get_CInteger(mediaFile.Video[0].Height);
                    Pro_Widht = ValueBack.Get_CInteger(mediaFile.Video[0].Width);
                    Pro_Record_Ende = Record_Beginn.AddSeconds(Pro_Duration);
                    Pro_Record_Länge = (int)Math.Round((double)Pro_Duration / 60);
                    Pro_Record_Resolution = mediaFile.Video[0].FrameSize;
                    Pro_Record_FrameRate = ValueBack.Get_CInteger(mediaFile.Video[0].FrameRate).ToString();
                }
                else
                {
                    Parameter.Error_Message(null, $"Class_MediaInfo.NoVideo {Record_File} - {mediaFile.MediaInfo_Text}");
                }
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
