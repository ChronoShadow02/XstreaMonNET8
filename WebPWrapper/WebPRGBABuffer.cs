#nullable disable
namespace XstreaMonNET8.WebPWrapper
{
    internal struct WebPRGBABuffer
    {
        public IntPtr rgba;   // puntero al inicio del buffer RGBA
        public int stride; // bytes por fila
        public UIntPtr size;   // tamaño total del buffer en bytes
    }
}
