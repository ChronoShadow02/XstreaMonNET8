using System.Runtime.InteropServices;

#nullable disable
namespace XstreaMonNET8.WebPWrapper
{
    internal struct WebPBitstreamFeatures
    {
        public int Width;
        public int Height;
        public int Has_alpha;
        public int Has_animation;
        public int Format;

        // Padding to match the original unmanaged layout
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
        private readonly uint[] pad;
    }
}
