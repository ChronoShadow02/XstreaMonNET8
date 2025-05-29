using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace XstreaMonNET8
{
    internal static class Media_Info
    {
        /// <summary>
        /// Calcula la diferencia en milisegundos entre el inicio de un video y un audio.
        /// </summary>
        /// <param name="videoPath">Ruta del archivo de video.</param>
        /// <param name="audioPath">Ruta del archivo de audio.</param>
        /// <returns>Diferencia en milisegundos como cadena.</returns>
        internal static async Task<int> Start_Video_Timer(string videoPath, string audioPath)
        {
            long videoStart = await File_Start_Time(videoPath);
            long audioStart = await File_Start_Time(audioPath);
            return (int)(videoStart - audioStart);
        }

        /// <summary>
        /// Obtiene la duración del archivo en milisegundos.
        /// </summary>
        /// <param name="filePath">Ruta del archivo multimedia.</param>
        /// <returns>Duración en milisegundos.</returns>
        private static async Task<long> File_Start_Time(string filePath)
        {
            try
            {
                // Asegura que FFmpeg esté disponible
                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
                FFmpeg.SetExecutablesPath("ffmpeg"); // Ajusta si tienes una ruta distinta

                IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filePath);

                // Nota: aquí se usa la duración como un aproximado al "start time"
                return (long)mediaInfo.Duration.TotalMilliseconds;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Media_Info.File_Start_Time");
                return 0L;
            }
        }
    }
}
