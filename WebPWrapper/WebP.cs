using Microsoft.VisualBasic.CompilerServices;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

#nullable disable
namespace XstreaMonNET8.WebPWrapper
{
    public sealed class WebP : IDisposable
    {
        private const int WEBP_MAX_DIMENSION = 16383;

        // ---------------------------------------------------------------------
        //  CARGA DESDE DISCO
        // ---------------------------------------------------------------------
        public Bitmap Load(string pathFileName)
        {
            try
            {
                return Decode(File.ReadAllBytes(pathFileName));
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.Load");
            }
        }

        // ---------------------------------------------------------------------
        //  DECODE (simple → buffer completo)
        // ---------------------------------------------------------------------
        public Bitmap Decode(byte[] rawWebP)
        {
            Bitmap bitmap = null;
            BitmapData bitmapdata = null;
            GCHandle gcHandle = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);

            try
            {
                int width, height;
                bool has_alpha, has_animation;
                string format;

                GetInfo(rawWebP, out width, out height, out has_alpha, out has_animation, out format);

                bitmap = has_alpha
                    ? new Bitmap(width, height, PixelFormat.Format32bppArgb)
                    : new Bitmap(width, height, PixelFormat.Format24bppRgb);

                bitmapdata = bitmap.LockBits(new Rectangle(0, 0, width, height),
                                             ImageLockMode.WriteOnly,
                                             bitmap.PixelFormat);

                int outputSize = checked(bitmapdata.Stride * height);
                IntPtr src = gcHandle.AddrOfPinnedObject();

                if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                    UnsafeNativeMethods.WebPDecodeBGRInto(src, rawWebP.Length,
                                                          bitmapdata.Scan0, outputSize, bitmapdata.Stride);
                else
                    UnsafeNativeMethods.WebPDecodeBGRAInto(src, rawWebP.Length,
                                                           bitmapdata.Scan0, outputSize, bitmapdata.Stride);

                return bitmap;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw;
            }
            finally
            {
                if (bitmapdata != null) bitmap.UnlockBits(bitmapdata);
                if (gcHandle.IsAllocated) gcHandle.Free();
            }
        }

        // ---------------------------------------------------------------------
        //  DECODE con opciones avanzadas
        // ---------------------------------------------------------------------
        public Bitmap Decode(byte[] rawWebP, WebPDecoderOptions options)
        {
            GCHandle gcHandle = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
            Bitmap bitmap = null;
            BitmapData bitmapdata = null;

            try
            {
                WebPDecoderConfig config = new WebPDecoderConfig();
                if (UnsafeNativeMethods.WebPInitDecoderConfig(ref config) == 0)
                    throw new Exception("WebPInitDecoderConfig failed. Wrong version?");

                IntPtr src = gcHandle.AddrOfPinnedObject();

                // Cuando NO se escala, hay que validar crop vs. tamaño real
                if (options.use_scaling == 0)
                {
                    VP8StatusCode st = UnsafeNativeMethods.WebPGetFeatures(src, rawWebP.Length, ref config.input);
                    if (st != VP8StatusCode.VP8_STATUS_OK)
                        throw new Exception("Failed WebPGetFeatures with error " + Conversions.ToString((int)st));

                    if (options.use_cropping == 1 &&
                        (options.crop_left + options.crop_width > config.input.Width ||
                         options.crop_top + options.crop_height > config.input.Height))
                        throw new Exception("Crop options exceeded WebP image dimensions");
                }

                // Copiar struct de opciones
                config.options = options;

                if (config.input.Has_alpha == 1)
                {
                    config.output.colorspace = WEBP_CSP_MODE.MODE_BGRA;
                    bitmap = new Bitmap(config.input.Width, config.input.Height, PixelFormat.Format32bppArgb);
                }
                else
                {
                    config.output.colorspace = WEBP_CSP_MODE.MODE_BGR;
                    bitmap = new Bitmap(config.input.Width, config.input.Height, PixelFormat.Format24bppRgb);
                }

                bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                             ImageLockMode.WriteOnly,
                                             bitmap.PixelFormat);

                config.output.u.RGBA.rgba = bitmapdata.Scan0;
                config.output.u.RGBA.stride = bitmapdata.Stride;
                config.output.u.RGBA.size = (UIntPtr)checked((ulong)(bitmap.Height * bitmapdata.Stride));
                config.output.height = bitmap.Height;
                config.output.width = bitmap.Width;
                config.output.is_external_memory = 1;

                VP8StatusCode decSt = UnsafeNativeMethods.WebPDecode(src, rawWebP.Length, ref config);
                if (decSt != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPDecode with error " + Conversions.ToString((int)decSt));

                UnsafeNativeMethods.WebPFreeDecBuffer(ref config.output);
                return bitmap;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.Decode");
            }
            finally
            {
                if (bitmapdata != null) bitmap.UnlockBits(bitmapdata);
                if (gcHandle.IsAllocated) gcHandle.Free();
            }
        }

        // ---------------------------------------------------------------------
        //  THUMBNAIL rápido (calidad baja)
        // ---------------------------------------------------------------------
        public Bitmap GetThumbnailFast(byte[] rawWebP, int width, int height)
        {
            GCHandle gcHandle = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
            Bitmap thumb = null;
            BitmapData bitmapdata = null;

            try
            {
                WebPDecoderConfig cfg = new WebPDecoderConfig();
                if (UnsafeNativeMethods.WebPInitDecoderConfig(ref cfg) == 0)
                    throw new Exception("WebPInitDecoderConfig failed. Wrong version?");

                // Opciones mínimas
                cfg.options.bypass_filtering = 1;
                cfg.options.no_fancy_upsampling = 1;
                cfg.options.use_threads = 1;
                cfg.options.use_scaling = 1;
                cfg.options.scaled_width = width;
                cfg.options.scaled_height = height;

                thumb = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bitmapdata = thumb.LockBits(new Rectangle(0, 0, width, height),
                                            ImageLockMode.WriteOnly,
                                            thumb.PixelFormat);

                cfg.output.colorspace = WEBP_CSP_MODE.MODE_BGR;
                cfg.output.u.RGBA.rgba = bitmapdata.Scan0;
                cfg.output.u.RGBA.stride = bitmapdata.Stride;
                cfg.output.u.RGBA.size = (UIntPtr)checked((ulong)(height * bitmapdata.Stride));
                cfg.output.width = width;
                cfg.output.height = height;
                cfg.output.is_external_memory = 1;

                VP8StatusCode st = UnsafeNativeMethods.WebPDecode(gcHandle.AddrOfPinnedObject(),
                                                                  rawWebP.Length,
                                                                  ref cfg);
                if (st != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPDecode with error " + Conversions.ToString((int)st));

                UnsafeNativeMethods.WebPFreeDecBuffer(ref cfg.output);
                return thumb;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.Thumbnail");
            }
            finally
            {
                if (bitmapdata != null) thumb.UnlockBits(bitmapdata);
                if (gcHandle.IsAllocated) gcHandle.Free();
            }
        }

        // ---------------------------------------------------------------------
        //  THUMBNAIL de alta calidad
        // ---------------------------------------------------------------------
        public Bitmap GetThumbnailQuality(byte[] rawWebP, int width, int height)
        {
            GCHandle gcHandle = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
            Bitmap thumb = null;
            BitmapData bitmapdata = null;

            try
            {
                WebPDecoderConfig cfg = new WebPDecoderConfig();
                if (UnsafeNativeMethods.WebPInitDecoderConfig(ref cfg) == 0)
                    throw new Exception("WebPInitDecoderConfig failed. Wrong version?");

                IntPtr src = gcHandle.AddrOfPinnedObject();
                VP8StatusCode stF = UnsafeNativeMethods.WebPGetFeatures(src, rawWebP.Length, ref cfg.input);
                if (stF != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPGetFeatures with error " + Conversions.ToString((int)stF));

                cfg.options.bypass_filtering = 0;
                cfg.options.no_fancy_upsampling = 0;
                cfg.options.use_threads = 1;
                cfg.options.use_scaling = 1;
                cfg.options.scaled_width = width;
                cfg.options.scaled_height = height;

                if (cfg.input.Has_alpha == 1)
                {
                    cfg.output.colorspace = WEBP_CSP_MODE.MODE_BGRA;
                    thumb = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                }
                else
                {
                    cfg.output.colorspace = WEBP_CSP_MODE.MODE_BGR;
                    thumb = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                }

                bitmapdata = thumb.LockBits(new Rectangle(0, 0, width, height),
                                            ImageLockMode.WriteOnly,
                                            thumb.PixelFormat);

                cfg.output.u.RGBA.rgba = bitmapdata.Scan0;
                cfg.output.u.RGBA.stride = bitmapdata.Stride;
                cfg.output.u.RGBA.size = (UIntPtr)checked((ulong)(height * bitmapdata.Stride));
                cfg.output.width = width;
                cfg.output.height = height;
                cfg.output.is_external_memory = 1;

                VP8StatusCode st = UnsafeNativeMethods.WebPDecode(src, rawWebP.Length, ref cfg);
                if (st != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPDecode with error " + Conversions.ToString((int)st));

                UnsafeNativeMethods.WebPFreeDecBuffer(ref cfg.output);
                return thumb;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.Thumbnail");
            }
            finally
            {
                if (bitmapdata != null) thumb.UnlockBits(bitmapdata);
                if (gcHandle.IsAllocated) gcHandle.Free();
            }
        }

        // ---------------------------------------------------------------------
        //  SAVE a disco (EncodeLossy y File.WriteAllBytes)
        // ---------------------------------------------------------------------
        public void Save(Bitmap bmp, string pathFileName, int quality = 75)
        {
            try
            {
                byte[] bytes = EncodeLossy(bmp, quality);
                File.WriteAllBytes(pathFileName, bytes);
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.Save");
            }
        }

        // ---------------------------------------------------------------------
        //  ENCODE Lossy (simple)
        // ---------------------------------------------------------------------
        public byte[] EncodeLossy(Bitmap bmp, int quality = 75)
        {
            if (bmp.Width == 0 || bmp.Height == 0)
                throw new ArgumentException("Bitmap contains no data.", nameof(bmp));
            if (bmp.Width > WEBP_MAX_DIMENSION || bmp.Height > WEBP_MAX_DIMENSION)
                throw new NotSupportedException($"Bitmap's dimension is too large. Max is {WEBP_MAX_DIMENSION}x{WEBP_MAX_DIMENSION} pixels.");
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb &&
                bmp.PixelFormat != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Only support Format24bppRgb and Format32bppArgb pixelFormat.");

            BitmapData bitmapdata = null;
            IntPtr output = IntPtr.Zero;

            try
            {
                bitmapdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                          ImageLockMode.ReadOnly,
                                          bmp.PixelFormat);

                int length = (bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    ? UnsafeNativeMethods.WebPEncodeBGR(bitmapdata.Scan0, bmp.Width, bmp.Height, bitmapdata.Stride,
                                                        (float)quality, out output)
                    : UnsafeNativeMethods.WebPEncodeBGRA(bitmapdata.Scan0, bmp.Width, bmp.Height, bitmapdata.Stride,
                                                         (float)quality, out output);

                if (length == 0)
                    throw new Exception("Can´t encode WebP");

                byte[] dst = new byte[length];
                Marshal.Copy(output, dst, 0, length);
                return dst;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.EncodeLossly");
            }
            finally
            {
                if (bitmapdata != null) bmp.UnlockBits(bitmapdata);
                if (output != IntPtr.Zero) UnsafeNativeMethods.WebPFree(output);
            }
        }

        // ---------------------------------------------------------------------
        //  ENCODE Lossless / Near-Lossless – lógica avanzada
        // ---------------------------------------------------------------------
        public byte[] EncodeLossless(Bitmap bmp)
        {
            if (bmp.Width == 0 || bmp.Height == 0)
                throw new ArgumentException("Bitmap contains no data.", nameof(bmp));
            if (bmp.Width > WEBP_MAX_DIMENSION || bmp.Height > WEBP_MAX_DIMENSION)
                throw new NotSupportedException($"Bitmap's dimension is too large. Max is {WEBP_MAX_DIMENSION}x{WEBP_MAX_DIMENSION} pixels.");
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb &&
                bmp.PixelFormat != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Only support Format24bppRgb and Format32bppArgb pixelFormat.");

            BitmapData bitmapdata = null;
            IntPtr output = IntPtr.Zero;

            try
            {
                bitmapdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                          ImageLockMode.ReadOnly,
                                          bmp.PixelFormat);

                int length = (bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    ? UnsafeNativeMethods.WebPEncodeLosslessBGR(bitmapdata.Scan0, bmp.Width, bmp.Height, bitmapdata.Stride, out output)
                    : UnsafeNativeMethods.WebPEncodeLosslessBGRA(bitmapdata.Scan0, bmp.Width, bmp.Height, bitmapdata.Stride, out output);

                byte[] dst = new byte[length];
                Marshal.Copy(output, dst, 0, length);
                return dst;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.EncodeLossless (Simple)");
            }
            finally
            {
                if (bitmapdata != null) bmp.UnlockBits(bitmapdata);
                if (output != IntPtr.Zero) UnsafeNativeMethods.WebPFree(output);
            }
        }

        // ---------------------------------------------------------------------
        //  ENCODE Lossy / Lossless Avanzado  (config → AdvancedEncode)
        // ---------------------------------------------------------------------
        public byte[] EncodeLossy(Bitmap bmp, int quality, int speed, bool info = false)
        {
            WebPConfig cfg = new WebPConfig();
            if (UnsafeNativeMethods.WebPConfigInit(ref cfg, WebPPreset.WEBP_PRESET_DEFAULT, 75f) == 0)
                throw new Exception("Can´t configure preset");

            cfg.method = Math.Min(speed, 6);
            cfg.quality = quality;
            cfg.autofilter = 1;
            cfg.pass = speed + 1;
            cfg.segments = 4;
            cfg.partitions = 3;
            cfg.thread_level = 1;
            cfg.alpha_quality = quality;
            cfg.alpha_filtering = 2;
            cfg.use_sharp_yuv = 1;

            if (UnsafeNativeMethods.WebPGetDecoderVersion() > 1082)
            {
                cfg.preprocessing = 4;
                cfg.use_sharp_yuv = 1;
            }
            else
            {
                cfg.preprocessing = 3;
            }

            return AdvancedEncode(bmp, cfg, info);
        }

        public byte[] EncodeLossless(Bitmap bmp, int speed)
        {
            WebPConfig cfg = new WebPConfig();
            if (UnsafeNativeMethods.WebPConfigInit(ref cfg, WebPPreset.WEBP_PRESET_DEFAULT, speed + 10) == 0)
                throw new Exception("Can´t config preset");

            if (UnsafeNativeMethods.WebPGetDecoderVersion() > 1082)
            {
                if (UnsafeNativeMethods.WebPConfigLosslessPreset(ref cfg, speed) == 0)
                    throw new Exception("Can´t configure lossless preset");
            }
            else
            {
                cfg.lossless = 1;
                cfg.method = Math.Min(speed, 6);
                cfg.quality = speed + 10;
            }

            cfg.pass = speed + 1;
            cfg.thread_level = 1;
            cfg.alpha_filtering = 2;
            cfg.use_sharp_yuv = 1;
            cfg.exact = 0;

            return AdvancedEncode(bmp, cfg, false);
        }

        public byte[] EncodeNearLossless(Bitmap bmp, int quality, int speed = 9)
        {
            if (UnsafeNativeMethods.WebPGetDecoderVersion() <= 1082)
                throw new Exception("This DLL version not support EncodeNearLossless");

            WebPConfig cfg = new WebPConfig();
            if (UnsafeNativeMethods.WebPConfigInit(ref cfg, WebPPreset.WEBP_PRESET_DEFAULT, speed + 10) == 0)
                throw new Exception("Can´t configure preset");

            if (UnsafeNativeMethods.WebPConfigLosslessPreset(ref cfg, speed) == 0)
                throw new Exception("Can´t configure lossless preset");

            cfg.pass = speed + 1;
            cfg.near_lossless = quality;
            cfg.thread_level = 1;
            cfg.alpha_filtering = 2;
            cfg.use_sharp_yuv = 1;
            cfg.exact = 0;

            return AdvancedEncode(bmp, cfg, false);
        }

        // ---------------------------------------------------------------------
        //  VERSION de DLL
        // ---------------------------------------------------------------------
        public string GetVersion()
        {
            try
            {
                return Conversions.ToString(UnsafeNativeMethods.WebPGetDecoderVersion() & 0xFF);
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.GetVersion");
            }
        }

        // ---------------------------------------------------------------------
        //  INFO de bitstream
        // ---------------------------------------------------------------------
        public void GetInfo(byte[] rawWebP,
                            out int width,
                            out int height,
                            out bool has_alpha,
                            out bool has_animation,
                            out string format)
        {
            GCHandle gcHandle = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
            try
            {
                IntPtr src = gcHandle.AddrOfPinnedObject();
                WebPBitstreamFeatures feat = new WebPBitstreamFeatures();

                VP8StatusCode st = UnsafeNativeMethods.WebPGetFeatures(src, rawWebP.Length, ref feat);
                if (st != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception(st.ToString());

                width = feat.Width;
                height = feat.Height;
                has_alpha = feat.Has_alpha == 1;
                has_animation = feat.Has_animation == 1;

                format = feat.Format switch
                {
                    1 => "lossy",
                    2 => "lossless",
                    _ => "undefined"
                };
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.GetInfo");
            }
            finally
            {
                if (gcHandle.IsAllocated) gcHandle.Free();
            }
        }

        // ---------------------------------------------------------------------
        //  DISTORSIÓN entre dos bitmaps
        // ---------------------------------------------------------------------
        public float[] GetPictureDistortion(Bitmap source, Bitmap reference, int metric_type)
        {
            WebPPicture picSrc = new WebPPicture();
            WebPPicture picRef = new WebPPicture();
            BitmapData bmpSrc = null;
            BitmapData bmpRef = null;

            float[] resultArr = new float[5];
            GCHandle hRes = GCHandle.Alloc(resultArr, GCHandleType.Pinned);

            try
            {
                if (source == null) throw new Exception("Source picture is void");
                if (reference == null) throw new Exception("Reference picture is void");
                if (metric_type > 2) throw new Exception("Bad metric_type. Use 0 = PSNR, 1 = SSIM, 2 = LSIM");
                if (source.Width != reference.Width || source.Height != reference.Height)
                    throw new Exception("Source and Reference pictures have different dimensions");

                // Cargar source
                bmpSrc = source.LockBits(new Rectangle(0, 0, source.Width, source.Height),
                                         ImageLockMode.ReadOnly,
                                         source.PixelFormat);

                if (UnsafeNativeMethods.WebPPictureInitInternal(ref picSrc) != 1)
                    throw new Exception("Can´t initialize WebPPictureInit");

                picSrc.width = source.Width;
                picSrc.height = source.Height;

                if (bmpSrc.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    picSrc.use_argb = 1;
                    if (UnsafeNativeMethods.WebPPictureImportBGRA(ref picSrc, bmpSrc.Scan0, bmpSrc.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGRA");
                }
                else
                {
                    picSrc.use_argb = 0;
                    if (UnsafeNativeMethods.WebPPictureImportBGR(ref picSrc, bmpSrc.Scan0, bmpSrc.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                }

                // Cargar reference
                bmpRef = reference.LockBits(new Rectangle(0, 0, reference.Width, reference.Height),
                                            ImageLockMode.ReadOnly,
                                            reference.PixelFormat);

                if (UnsafeNativeMethods.WebPPictureInitInternal(ref picRef) != 1)
                    throw new Exception("Can´t initialize WebPPictureInit");

                picRef.width = reference.Width;
                picRef.height = reference.Height;
                picRef.use_argb = 1;

                if (bmpSrc.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    if (UnsafeNativeMethods.WebPPictureImportBGRA(ref picRef, bmpRef.Scan0, bmpRef.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGRA");
                }
                else
                {
                    picRef.use_argb = 0;
                    if (UnsafeNativeMethods.WebPPictureImportBGR(ref picRef, bmpRef.Scan0, bmpRef.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                }

                IntPtr pRes = hRes.AddrOfPinnedObject();
                if (UnsafeNativeMethods.WebPPictureDistortion(ref picSrc, ref picRef, metric_type, pRes) != 1)
                    throw new Exception("Can´t measure.");

                return resultArr;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.GetPictureDistortion");
            }
            finally
            {
                if (bmpSrc != null) source.UnlockBits(bmpSrc);
                if (bmpRef != null) reference.UnlockBits(bmpRef);

                if (picSrc.argb != IntPtr.Zero) UnsafeNativeMethods.WebPPictureFree(ref picSrc);
                if (picRef.argb != IntPtr.Zero) UnsafeNativeMethods.WebPPictureFree(ref picRef);

                if (hRes.IsAllocated) hRes.Free();
            }
        }

        // ---------------------------------------------------------------------
        //  ENCODE avanzado  (config 100 % libre)
        // ---------------------------------------------------------------------
        private byte[] AdvancedEncode(Bitmap bmp, WebPConfig config, bool info)
        {
            byte[] managedBuf = null;
            WebPPicture pic = new WebPPicture();
            BitmapData bmpData = null;
            IntPtr hAuxStats = IntPtr.Zero;
            GCHandle hManaged = default;

            try
            {
                if (UnsafeNativeMethods.WebPValidateConfig(ref config) != 1)
                    throw new Exception("Bad configuration parameters");

                if (bmp.Width == 0 || bmp.Height == 0)
                    throw new ArgumentException("Bitmap contains no data.", nameof(bmp));
                if (bmp.Width > WEBP_MAX_DIMENSION || bmp.Height > WEBP_MAX_DIMENSION)
                    throw new NotSupportedException($"Bitmap's dimension is too large. Max is {WEBP_MAX_DIMENSION}x{WEBP_MAX_DIMENSION} pixels.");
                if (bmp.PixelFormat != PixelFormat.Format24bppRgb &&
                    bmp.PixelFormat != PixelFormat.Format32bppArgb)
                    throw new NotSupportedException("Only support Format24bppRgb and Format32bppArgb pixelFormat.");

                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                       ImageLockMode.ReadOnly,
                                       bmp.PixelFormat);

                if (UnsafeNativeMethods.WebPPictureInitInternal(ref pic) != 1)
                    throw new Exception("Can´t initialize WebPPictureInit");

                pic.width = bmp.Width;
                pic.height = bmp.Height;
                pic.use_argb = 1;

                int rawSize;
                if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    if (UnsafeNativeMethods.WebPPictureImportBGRA(ref pic, bmpData.Scan0, bmpData.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGRA");
                    pic.colorspace = 3u;
                    rawSize = checked(bmp.Width * bmp.Height * 32);
                    managedBuf = new byte[rawSize];
                }
                else
                {
                    if (UnsafeNativeMethods.WebPPictureImportBGR(ref pic, bmpData.Scan0, bmpData.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                    rawSize = checked(bmp.Width * bmp.Height * 24);
                }

                // Stats opcionales
                if (info)
                {
                    WebPAuxStats tmp = new WebPAuxStats();
                    hAuxStats = Marshal.AllocHGlobal(Marshal.SizeOf(tmp));
                    Marshal.StructureToPtr(tmp, hAuxStats, false);
                    pic.stats = hAuxStats;
                }

                // Preparar buffer administrado donde libwebp irá escribiendo
                byte[] buffer = new byte[checked(bmp.Width * bmp.Height * 32)];
                hManaged = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                IntPtr pManaged = hManaged.AddrOfPinnedObject();
                pic.custom_ptr = pManaged;

                // Callback de escritura
                UnsafeNativeMethods.OnCallback = MyWriter;
                pic.writer = Marshal.GetFunctionPointerForDelegate(UnsafeNativeMethods.OnCallback);

                // --- Codificar ---------------------------------------------------
                if (UnsafeNativeMethods.WebPEncode(ref config, ref pic) != 1)
                    throw new Exception("Encoding error: " + ((WebPEncodingError)pic.error_code).ToString());

                UnsafeNativeMethods.OnCallback = null; // liberar

                bmp.UnlockBits(bmpData);
                bmpData = null;

                int encodedLen = (int)(pic.custom_ptr.ToInt64() - pManaged.ToInt64());
                byte[] dst = new byte[encodedLen];
                Array.Copy(buffer, dst, encodedLen);

                // Stats emergentes opcionales
                if (info)
                {
                    WebPAuxStats stat = Marshal.PtrToStructure<WebPAuxStats>(hAuxStats);
                    MessageBox.Show(
                        $"Dimension: {pic.width} x {pic.height} pixels\n" +
                        $"Output:    {stat.coded_size} bytes\n" +
                        $"PSNR Y:    {stat.PSNRY} db\n" +
                        $"PSNR U:    {stat.PSNRU} db\n" +
                        $"PSNR V:    {stat.PSNRV} db\n" +
                        $"PSNR ALL:  {stat.PSNRALL} db\n",
                        "Compression statistics");
                }

                return dst;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                throw new Exception(ex.Message + "\r\nIn WebP.AdvancedEncode");
            }
            finally
            {
                if (hManaged.IsAllocated) hManaged.Free();
                if (hAuxStats != IntPtr.Zero) Marshal.FreeHGlobal(hAuxStats);
                if (bmpData != null) bmp.UnlockBits(bmpData);
                if (pic.argb != IntPtr.Zero) UnsafeNativeMethods.WebPPictureFree(ref pic);
            }
        }

        // ---------------------------------------------------------------------
        //  CALLBACK de escritura (memcpy sobre buffer administrado)
        // ---------------------------------------------------------------------
        private int MyWriter(IntPtr data, UIntPtr data_size, ref WebPPicture picture)
        {
            UnsafeNativeMethods.CopyMemory(picture.custom_ptr, data, (uint)data_size);
            picture.custom_ptr = new IntPtr(picture.custom_ptr.ToInt64() + (long)(uint)data_size);
            return 1;
        }

        // ---------------------------------------------------------------------
        //  IDisposable
        // ---------------------------------------------------------------------
        public void Dispose() => GC.SuppressFinalize(this);

        void IDisposable.Dispose() => Dispose();

        // (solo referencia; no se usa directamente desde C#)
        private delegate int MyWriterDelegate(IntPtr data, UIntPtr data_size, ref WebPPicture picture);
    }
}
