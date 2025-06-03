using System.Runtime.InteropServices;

#nullable disable
namespace XstreaMonNET8.WebPWrapper
{
    internal struct WebPPicture
    {
        // --- parámetros de entrada/imagen -----------------------------------
        public int use_argb;
        public uint colorspace;
        public int width;
        public int height;

        // Planos YUV
        public IntPtr y;
        public IntPtr u;
        public IntPtr v;
        public int y_stride;
        public int uv_stride;

        // Plano alfa
        public IntPtr a;
        public int a_stride;

        // Relleno para mantener el layout nativo
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
        private readonly uint[] pad1;

        // Buffer ARGB (si se usa_argb == 1)
        public IntPtr argb;
        public int argb_stride;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        private readonly uint[] pad2;

        // Callback de escritura y datos extra
        public IntPtr writer;      // WebPMemoryWrite* o función de usuario
        public IntPtr custom_ptr;  // parámetro para el callback

        // Extra-info y estadísticas
        public int extra_info_type;
        public IntPtr extra_info;  // apunta a buffer provisto por el usuario
        public IntPtr stats;       // WebPAuxStats*

        // Gestión de errores y progreso
        public uint error_code;
        public IntPtr progress_hook;  // WebPProgressHook*
        public IntPtr user_data;      // parámetro para el progreso

        // Más relleno para respetar el layout
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13, ArraySubType = UnmanagedType.U4)]
        private readonly uint[] pad3;

        // Memoria interna usada por libwebp
        private readonly IntPtr memory_;
        private readonly IntPtr memory_argb_;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
        private readonly uint[] pad4;
    }
}
