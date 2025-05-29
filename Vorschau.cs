using System.Drawing.Drawing2D;

namespace XstreaMonNET8
{
    internal static class Vorschau
    {
        internal static async Task<Bitmap?> Vorschau_Image_Create(
            string videoFile,
            int imageWidth,
            int imageHeight,
            int videoLengthSeconds,
            bool imageRotation)
        {
            if (!File.Exists(videoFile))
                return null;

            try
            {
                int width = (int)Math.Round(imageRotation ? imageHeight * 1.7676767 : imageHeight / 1.3);
                int height = imageHeight;

                int num1 = (int)Math.Round(videoLengthSeconds - (videoLengthSeconds / 10.0 / (imageWidth / (double)width)));
                Bitmap bitmap = new(imageWidth, imageHeight);
                using Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.Transparent);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                int totalFrames = (int)Math.Round(imageWidth / (double)width) + 1;

                for (int i = 0; i <= totalFrames; i++)
                {
                    try
                    {
                        using Image? result = await CreateThumbFromVideo.GenerateThumb(videoFile, num1 * i + 1, width, height);
                        if (result != null)
                            graphics.DrawImage(result, width * i, 0);
                    }
                    catch (Exception ex)
                    {
                        Parameter.Error_Message(ex, "Vorschau_Image_Create.ForMaxPic");
                    }
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Vorschau_Image_Create.FileExist");
                return null;
            }
        }

        internal static async Task<Bitmap?> Vorschau_Tiles_Create(
            string videoFile,
            int imageWidth,
            int imageHeight,
            int videoLengthSeconds)
        {
            if (!File.Exists(videoFile))
                return null;

            try
            {
                int width = (int)Math.Round(imageWidth / 4.0);
                int height = (int)Math.Round(imageHeight / 4.0);
                int secondsPerTile = (int)Math.Round((videoLengthSeconds - 1) / 16.0);

                Bitmap bitmap = new(imageWidth, imageHeight);
                using Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.Transparent);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                for (int i = 0; i <= 16; i++)
                {
                    try
                    {
                        using Image? result = await CreateThumbFromVideo.GenerateThumb(videoFile, secondsPerTile * i + 1, width, height);
                        if (result != null)
                        {
                            int x = width * (i % 4);
                            int y = height * (i / 4);
                            graphics.DrawImage(result, x, y);
                        }
                    }
                    catch (Exception ex)
                    {
                        Parameter.Error_Message(ex, "Vorschau_Tiles_Create.ForMaxPic");
                    }
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Vorschau_Tiles_Create.FileExist");
                return null;
            }
        }
    }
}
