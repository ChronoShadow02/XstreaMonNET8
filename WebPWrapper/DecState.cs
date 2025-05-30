namespace XstreaMonNET8.WebPWrapper
{
    internal enum DecState
    {
        STATE_WEBP_HEADER,
        STATE_VP8_HEADER,
        STATE_VP8_PARTS0,
        STATE_VP8_DATA,
        STATE_VP8L_HEADER,
        STATE_VP8L_DATA,
        STATE_DONE,
        STATE_ERROR,
    }
}
