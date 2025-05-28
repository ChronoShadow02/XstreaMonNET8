namespace XstreaMonNET8
{
    public class WebFileDownloader
    {
        private string mCurrentFile = string.Empty;

        public string CurrentFile => mCurrentFile;

        public event AmountDownloadedChangedEventHandler? AmountDownloadedChanged;
        public event FileDownloadSizeObtainedEventHandler? FileDownloadSizeObtained;
        public event FileDownloadCompleteEventHandler? FileDownloadComplete;
        public event FileDownloadFailedEventHandler? FileDownloadFailed;

        public delegate void AmountDownloadedChangedEventHandler(long iNewProgress);
        public delegate void FileDownloadSizeObtainedEventHandler(long iFileSize);
        public delegate void FileDownloadCompleteEventHandler();
        public delegate void FileDownloadFailedEventHandler(Exception ex);

        private readonly HttpClient httpClient;

        public WebFileDownloader()
        {
            httpClient = new HttpClient();
        }

        private static string GetFileName(string URL)
        {
            try
            {
                return Path.GetFileName(new Uri(URL).AbsolutePath);
            }
            catch
            {
                return URL;
            }
        }

        public async Task<bool> DownloadFileWithProgress(string URL, string Location)
        {
            FileStream? fileStream = null;
            try
            {
                mCurrentFile = GetFileName(URL);

                using var response = await httpClient.GetAsync(URL, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                long? contentLength = response.Content.Headers.ContentLength;
                if (contentLength.HasValue)
                    FileDownloadSizeObtained?.Invoke(contentLength.Value);

                using Stream responseStream = await response.Content.ReadAsStreamAsync();
                fileStream = new FileStream(Location, FileMode.Create, FileAccess.Write, FileShare.None);

                byte[] buffer = new byte[8192];
                int bytesRead;
                long totalRead = 0;

                while ((bytesRead = await responseStream.ReadAsync(buffer.AsMemory(0, buffer.Length))) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                    totalRead += bytesRead;

                    if (contentLength.HasValue && totalRead > contentLength.Value)
                        AmountDownloadedChanged?.Invoke(contentLength.Value);
                    else
                        AmountDownloadedChanged?.Invoke(totalRead);
                }

                fileStream.Close();
                FileDownloadComplete?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                fileStream?.Close();
                FileDownloadFailed?.Invoke(ex);
                return false;
            }
        }

        public static string FormatFileSize(long size)
        {
            try
            {
                const int KB = 1024;
                const int MB = KB * KB;
                const int GB = MB * KB;

                return size switch
                {
                    < KB => $"{size} bytes",
                    < MB => $"{(double)size / KB:N} KB",
                    < GB => $"{(double)size / MB:N} MB",
                    _ => $"{(double)size / GB:N} GB"
                };
            }
            catch
            {
                return size.ToString();
            }
        }
    }
}
