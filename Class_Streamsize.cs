namespace XstreaMonNET8
{
    public class Class_Streamsize
    {
        public static Size Pro_Stream_Size_View { get; set; }

        public static Size Pro_Stream_Size_Recorder { get; set; }

        public static void Size_Load()
        {
            string recordSize = IniFile.Read(Parameter.INI_Common, "Size", "Record", "0");
            switch (recordSize)
            {
                case "0":
                    Pro_Stream_Size_Recorder = new Size(295, 168);
                    break;
                case "1":
                    Pro_Stream_Size_Recorder = new Size(400, 228);
                    break;
                case "2":
                    Pro_Stream_Size_Recorder = new Size(540, 308);
                    break;
            }

            string viewSize = IniFile.Read(Parameter.INI_Common, "Size", "View", "1");
            switch (viewSize)
            {
                case "0":
                    Pro_Stream_Size_View = new Size(295, 168);
                    break;
                case "1":
                    Pro_Stream_Size_View = new Size(400, 228);
                    break;
                case "2":
                    Pro_Stream_Size_View = new Size(540, 308);
                    break;
            }
        }
    }
}
