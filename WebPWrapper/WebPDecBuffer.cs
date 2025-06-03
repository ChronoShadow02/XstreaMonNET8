#nullable disable
namespace XstreaMonNET8.WebPWrapper
{
    internal struct WebPDecBuffer
    {
        public WEBP_CSP_MODE colorspace;  // Espacio de color del destino
        public int width;                 // Ancho final
        public int height;                // Alto final
        public int is_external_memory;    // 1 si ‘u’ apunta a un buffer externo

        // Unión de buffers RGBA / YUVA —debe declararse por separado con
        // los mismos nombres de campo que en el código original.
        public RGBA_YUVA_Buffer u;

        // Campos de relleno para mantener el layout nativo
        private readonly uint pad1;
        private readonly uint pad2;
        private readonly uint pad3;
        private readonly uint pad4;

        // Memoria privada gestionada por la librería nativa (si aplica)
        public IntPtr private_memory;
    }
}
