namespace XstreaMonNET8.WebPWrapper
{
    public struct WebPDecoderOptions
    {
        public int bypass_filtering;
        public int no_fancy_upsampling;
        public int use_cropping;
        public int crop_left;
        public int crop_top;
        public int crop_width;
        public int crop_height;
        public int use_scaling;
        public int scaled_width;
        public int scaled_height;
        public int use_threads;
        public int dithering_strength;
        public int flip;
        public int alpha_dithering_strength;

        // Relleno para conservar el layout original esperado por libwebp
        private readonly uint pad1;
        private readonly uint pad2;
        private readonly uint pad3;
        private readonly uint pad4;
        private readonly uint pad5;
    }
}
