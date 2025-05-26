namespace XstreaMonNET8
{
    public class CustomHttpClient
    {
        private readonly HttpClient _httpClient;

        public CustomHttpClient(int timeoutMilliseconds = 100000)
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds)
            };
        }

        public async Task<string> GetStringAsync(string url, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetStringAsync(url, cancellationToken);
        }

        public async Task<byte[]> GetByteArrayAsync(string url, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetByteArrayAsync(url, cancellationToken);
        }

        public async Task<Stream> GetStreamAsync(string url, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetStreamAsync(url, cancellationToken);
        }

        public HttpClient Instance => _httpClient;
    }
}
