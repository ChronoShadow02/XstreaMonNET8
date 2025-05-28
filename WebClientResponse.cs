namespace XstreaMonNET8
{
    internal class WebClientResponse
    {
        public bool Success { get; set; } = false;

        public Exception? Exception { get; set; }

        public string? ReturnedString { get; set; }
    }
}
