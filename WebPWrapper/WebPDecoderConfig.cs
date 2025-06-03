#nullable disable
using XstreaMonNET8.WebPWrapper;

namespace XstreaMonNET8.WebPWrapper
{
    internal struct WebPDecoderConfig
    {
        public WebPBitstreamFeatures input;  // Información del bitstream
        public WebPDecBuffer output;  // Buffer de salida (RGBA/YUVA)
        public WebPDecoderOptions options; // Opciones de decodificación
    }
}
