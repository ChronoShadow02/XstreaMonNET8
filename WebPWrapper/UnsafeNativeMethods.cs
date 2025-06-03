using System.Runtime.InteropServices;
using System.Security;

#nullable disable
namespace XstreaMonNET8.WebPWrapper
{
    [SuppressUnmanagedCodeSecurity]
    internal sealed class UnsafeNativeMethods
    {
        private static readonly int WEBP_DECODER_ABI_VERSION = 520;
        internal static WebPMemoryWrite OnCallback;

        // --- utilidades -------------------------------------------------------
        [DllImport("kernel32.dll")]
        internal static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        // --- inicialización de configuración ----------------------------------
        internal static int WebPConfigInit(ref WebPConfig config, WebPPreset preset, float quality)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPConfigInitInternal_x86(ref config, preset, quality, WEBP_DECODER_ABI_VERSION);
                case 8:
                    return WebPConfigInitInternal_x64(ref config, preset, quality, WEBP_DECODER_ABI_VERSION);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPConfigInitInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPConfigInitInternal_x86(
            ref WebPConfig config,
            WebPPreset preset,
            float quality,
            int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPConfigInitInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPConfigInitInternal_x64(
            ref WebPConfig config,
            WebPPreset preset,
            float quality,
            int WEBP_DECODER_ABI_VERSION);

        // --- obtención de características -------------------------------------
        internal static VP8StatusCode WebPGetFeatures(
            IntPtr rawWebP,
            int data_size,
            ref WebPBitstreamFeatures features)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPGetFeaturesInternal_x86(rawWebP, (UIntPtr)checked((ulong)data_size), ref features, WEBP_DECODER_ABI_VERSION);
                case 8:
                    return WebPGetFeaturesInternal_x64(rawWebP, (UIntPtr)checked((ulong)data_size), ref features, WEBP_DECODER_ABI_VERSION);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPGetFeaturesInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern VP8StatusCode WebPGetFeaturesInternal_x86(
            IntPtr rawWebP,
            UIntPtr data_size,
            ref WebPBitstreamFeatures features,
            int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPGetFeaturesInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern VP8StatusCode WebPGetFeaturesInternal_x64(
            IntPtr rawWebP,
            UIntPtr data_size,
            ref WebPBitstreamFeatures features,
            int WEBP_DECODER_ABI_VERSION);

        // --- presets, validación ----------------------------------------------
        internal static int WebPConfigLosslessPreset(ref WebPConfig config, int level)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPConfigLosslessPreset_x86(ref config, level);
                case 8:
                    return WebPConfigLosslessPreset_x64(ref config, level);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPConfigLosslessPreset", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPConfigLosslessPreset_x86(ref WebPConfig config, int level);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPConfigLosslessPreset", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPConfigLosslessPreset_x64(ref WebPConfig config, int level);

        internal static int WebPValidateConfig(ref WebPConfig config)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPValidateConfig_x86(ref config);
                case 8:
                    return WebPValidateConfig_x64(ref config);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPValidateConfig", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPValidateConfig_x86(ref WebPConfig config);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPValidateConfig", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPValidateConfig_x64(ref WebPConfig config);

        // --- inicialización / importación de imágenes -------------------------
        internal static int WebPPictureInitInternal(ref WebPPicture wpic)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureInitInternal_x86(ref wpic, WEBP_DECODER_ABI_VERSION);
                case 8:
                    return WebPPictureInitInternal_x64(ref wpic, WEBP_DECODER_ABI_VERSION);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPPictureInitInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureInitInternal_x86(ref WebPPicture wpic, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPPictureInitInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureInitInternal_x64(ref WebPPicture wpic, int WEBP_DECODER_ABI_VERSION);

        internal static int WebPPictureImportBGR(ref WebPPicture wpic, IntPtr bgr, int stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureImportBGR_x86(ref wpic, bgr, stride);
                case 8:
                    return WebPPictureImportBGR_x64(ref wpic, bgr, stride);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPPictureImportBGR", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureImportBGR_x86(ref WebPPicture wpic, IntPtr bgr, int stride);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPPictureImportBGR", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureImportBGR_x64(ref WebPPicture wpic, IntPtr bgr, int stride);

        internal static int WebPPictureImportBGRA(ref WebPPicture wpic, IntPtr bgra, int stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureImportBGRA_x86(ref wpic, bgra, stride);
                case 8:
                    return WebPPictureImportBGRA_x64(ref wpic, bgra, stride);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPPictureImportBGRA", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureImportBGRA_x86(ref WebPPicture wpic, IntPtr bgra, int stride);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPPictureImportBGRA", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureImportBGRA_x64(ref WebPPicture wpic, IntPtr bgra, int stride);

        internal static int WebPPictureImportBGRX(ref WebPPicture wpic, IntPtr bgr, int stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureImportBGRX_x86(ref wpic, bgr, stride);
                case 8:
                    return WebPPictureImportBGRX_x64(ref wpic, bgr, stride);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPPictureImportBGRX", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureImportBGRX_x86(ref WebPPicture wpic, IntPtr bgr, int stride);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPPictureImportBGRX", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureImportBGRX_x64(ref WebPPicture wpic, IntPtr bgr, int stride);

        // --- codificación / liberación ----------------------------------------
        internal static int WebPEncode(ref WebPConfig config, ref WebPPicture picture)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncode_x86(ref config, ref picture);
                case 8:
                    return WebPEncode_x64(ref config, ref picture);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPEncode", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncode_x86(ref WebPConfig config, ref WebPPicture picture);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPEncode", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncode_x64(ref WebPConfig config, ref WebPPicture picture);

        internal static void WebPPictureFree(ref WebPPicture picture)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    WebPPictureFree_x86(ref picture);
                    break;
                case 8:
                    WebPPictureFree_x64(ref picture);
                    break;
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPPictureFree", CallingConvention = CallingConvention.Cdecl)]
        private static extern void WebPPictureFree_x86(ref WebPPicture wpic);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPPictureFree", CallingConvention = CallingConvention.Cdecl)]
        private static extern void WebPPictureFree_x64(ref WebPPicture wpic);

        // --- decodificación rápida a búfer ------------------------------------
        internal static int WebPGetInfo(IntPtr data, int data_size, out int width, out int height)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPGetInfo_x86(data, (UIntPtr)checked((ulong)data_size), out width, out height);
                case 8:
                    return WebPGetInfo_x64(data, (UIntPtr)checked((ulong)data_size), out width, out height);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPGetInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPGetInfo_x86(
            IntPtr data,
            UIntPtr data_size,
            out int width,
            out int height);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPGetInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPGetInfo_x64(
            IntPtr data,
            UIntPtr data_size,
            out int width,
            out int height);

        internal static void WebPDecodeBGRInto(
            IntPtr data,
            int data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    if (WebPDecodeBGRInto_x86(data, (UIntPtr)checked((ulong)data_size), output_buffer, output_buffer_size, output_stride) == IntPtr.Zero)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;
                case 8:
                    if (WebPDecodeBGRInto_x64(data, (UIntPtr)checked((ulong)data_size), output_buffer, output_buffer_size, output_stride) == IntPtr.Zero)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPDecodeBGRInto", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr WebPDecodeBGRInto_x86(
            IntPtr data,
            UIntPtr data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPDecodeBGRInto", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr WebPDecodeBGRInto_x64(
            IntPtr data,
            UIntPtr data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride);

        // --- (BGRA, ARGB) ------------------------------------------------------
        internal static void WebPDecodeBGRAInto(
            IntPtr data,
            int data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    if (WebPDecodeBGRAInto_x86(data, (UIntPtr)checked((ulong)data_size), output_buffer, output_buffer_size, output_stride) == IntPtr.Zero)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;
                case 8:
                    if (WebPDecodeBGRAInto_x64(data, (UIntPtr)checked((ulong)data_size), output_buffer, output_buffer_size, output_stride) == IntPtr.Zero)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPDecodeBGRAInto", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr WebPDecodeBGRAInto_x86(
            IntPtr data,
            UIntPtr data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPDecodeBGRAInto", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr WebPDecodeBGRAInto_x64(
            IntPtr data,
            UIntPtr data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride);

        internal static void WebPDecodeARGBInto(
            IntPtr data,
            int data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    if (WebPDecodeARGBInto_x86(data, (UIntPtr)checked((ulong)data_size), output_buffer, output_buffer_size, output_stride) == IntPtr.Zero)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;
                case 8:
                    if (WebPDecodeARGBInto_x64(data, (UIntPtr)checked((ulong)data_size), output_buffer, output_buffer_size, output_stride) == IntPtr.Zero)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPDecodeARGBInto", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr WebPDecodeARGBInto_x86(
            IntPtr data,
            UIntPtr data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPDecodeARGBInto", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr WebPDecodeARGBInto_x64(
            IntPtr data,
            UIntPtr data_size,
            IntPtr output_buffer,
            int output_buffer_size,
            int output_stride);

        // --- configuración de decodificador -----------------------------------
        internal static int WebPInitDecoderConfig(ref WebPDecoderConfig webPDecoderConfig)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPInitDecoderConfigInternal_x86(ref webPDecoderConfig, WEBP_DECODER_ABI_VERSION);
                case 8:
                    return WebPInitDecoderConfigInternal_x64(ref webPDecoderConfig, WEBP_DECODER_ABI_VERSION);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPInitDecoderConfigInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPInitDecoderConfigInternal_x86(ref WebPDecoderConfig webPDecoderConfig, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPInitDecoderConfigInternal", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPInitDecoderConfigInternal_x64(ref WebPDecoderConfig webPDecoderConfig, int WEBP_DECODER_ABI_VERSION);

        internal static VP8StatusCode WebPDecode(
            IntPtr data,
            int data_size,
            ref WebPDecoderConfig webPDecoderConfig)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPDecode_x86(data, (UIntPtr)checked((ulong)data_size), ref webPDecoderConfig);
                case 8:
                    return WebPDecode_x64(data, (UIntPtr)checked((ulong)data_size), ref webPDecoderConfig);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPDecode", CallingConvention = CallingConvention.Cdecl)]
        private static extern VP8StatusCode WebPDecode_x86(IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPDecode", CallingConvention = CallingConvention.Cdecl)]
        private static extern VP8StatusCode WebPDecode_x64(IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

        internal static void WebPFreeDecBuffer(ref WebPDecBuffer buffer)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    WebPFreeDecBuffer_x86(ref buffer);
                    break;
                case 8:
                    WebPFreeDecBuffer_x64(ref buffer);
                    break;
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPFreeDecBuffer", CallingConvention = CallingConvention.Cdecl)]
        private static extern void WebPFreeDecBuffer_x86(ref WebPDecBuffer buffer);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPFreeDecBuffer", CallingConvention = CallingConvention.Cdecl)]
        private static extern void WebPFreeDecBuffer_x64(ref WebPDecBuffer buffer);

        // --- codificadores directos -------------------------------------------
        internal static int WebPEncodeBGR(
            IntPtr bgr,
            int width,
            int height,
            int stride,
            float quality_factor,
            out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeBGR_x86(bgr, width, height, stride, quality_factor, out output);
                case 8:
                    return WebPEncodeBGR_x64(bgr, width, height, stride, quality_factor, out output);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPEncodeBGR", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeBGR_x86(
            IntPtr bgr,
            int width,
            int height,
            int stride,
            float quality_factor,
            out IntPtr output);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPEncodeBGR", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeBGR_x64(
            IntPtr bgr,
            int width,
            int height,
            int stride,
            float quality_factor,
            out IntPtr output);

        internal static int WebPEncodeBGRA(
            IntPtr bgra,
            int width,
            int height,
            int stride,
            float quality_factor,
            out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeBGRA_x86(bgra, width, height, stride, quality_factor, out output);
                case 8:
                    return WebPEncodeBGRA_x64(bgra, width, height, stride, quality_factor, out output);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPEncodeBGRA", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeBGRA_x86(
            IntPtr bgra,
            int width,
            int height,
            int stride,
            float quality_factor,
            out IntPtr output);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPEncodeBGRA", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeBGRA_x64(
            IntPtr bgra,
            int width,
            int height,
            int stride,
            float quality_factor,
            out IntPtr output);

        internal static int WebPEncodeLosslessBGR(
            IntPtr bgr,
            int width,
            int height,
            int stride,
            out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeLosslessBGR_x86(bgr, width, height, stride, out output);
                case 8:
                    return WebPEncodeLosslessBGR_x64(bgr, width, height, stride, out output);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPEncodeLosslessBGR", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeLosslessBGR_x86(
            IntPtr bgr,
            int width,
            int height,
            int stride,
            out IntPtr output);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPEncodeLosslessBGR", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeLosslessBGR_x64(
            IntPtr bgr,
            int width,
            int height,
            int stride,
            out IntPtr output);

        internal static int WebPEncodeLosslessBGRA(
            IntPtr bgra,
            int width,
            int height,
            int stride,
            out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeLosslessBGRA_x86(bgra, width, height, stride, out output);
                case 8:
                    return WebPEncodeLosslessBGRA_x64(bgra, width, height, stride, out output);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPEncodeLosslessBGRA", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeLosslessBGRA_x86(
            IntPtr bgra,
            int width,
            int height,
            int stride,
            out IntPtr output);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPEncodeLosslessBGRA", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPEncodeLosslessBGRA_x64(
            IntPtr bgra,
            int width,
            int height,
            int stride,
            out IntPtr output);

        // --- liberación de memoria --------------------------------------------
        internal static void WebPFree(IntPtr p)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    WebPFree_x86(p);
                    break;
                case 8:
                    WebPFree_x64(p);
                    break;
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPFree", CallingConvention = CallingConvention.Cdecl)]
        private static extern void WebPFree_x86(IntPtr p);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPFree", CallingConvention = CallingConvention.Cdecl)]
        private static extern void WebPFree_x64(IntPtr p);

        // --- versión del decodificador ----------------------------------------
        internal static int WebPGetDecoderVersion()
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPGetDecoderVersion_x86();
                case 8:
                    return WebPGetDecoderVersion_x64();
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPGetDecoderVersion", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPGetDecoderVersion_x86();

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPGetDecoderVersion", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPGetDecoderVersion_x64();

        // --- distorsión entre imágenes ----------------------------------------
        internal static int WebPPictureDistortion(
            ref WebPPicture srcPicture,
            ref WebPPicture refPicture,
            int metric_type,
            IntPtr pResult)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureDistortion_x86(ref srcPicture, ref refPicture, metric_type, pResult);
                case 8:
                    return WebPPictureDistortion_x64(ref srcPicture, ref refPicture, metric_type, pResult);
                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", EntryPoint = "WebPPictureDistortion", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureDistortion_x86(
            ref WebPPicture srcPicture,
            ref WebPPicture refPicture,
            int metric_type,
            IntPtr pResult);

        [DllImport("libwebp_x64.dll", EntryPoint = "WebPPictureDistortion", CallingConvention = CallingConvention.Cdecl)]
        private static extern int WebPPictureDistortion_x64(
            ref WebPPicture srcPicture,
            ref WebPPicture refPicture,
            int metric_type,
            IntPtr pResult);

        // --- callback ----------------------------------------------------------
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WebPMemoryWrite(IntPtr data, UIntPtr data_size, ref WebPPicture wpic);
    }
}
