using System.Runtime.InteropServices;

namespace XstreaMonNET8.WebPWrapper
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct RGBA_YUVA_Buffer
    {
        [FieldOffset(0)]
        public WebPRGBABuffer RGBA;
        [FieldOffset(0)]
        public WebPYUVABuffer YUVA;
    }
}
