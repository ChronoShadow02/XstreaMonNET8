#nullable disable
namespace XstreaMonNET8.WebPWrapper
{
    internal enum WEBP_CSP_MODE
    {
        MODE_RGB = 0,
        MODE_RGBA = 1,
        MODE_BGR = 2,
        MODE_BGRA = 3,
        MODE_ARGB = 4,
        MODE_RGBA_4444 = 5,
        MODE_RGB_565 = 6,
        MODE_YUV = 11, // 0x0000000B
        MODE_YUVA = 12, // 0x0000000C
        MODE_LAST = 13, // 0x0000000D
    }
}
