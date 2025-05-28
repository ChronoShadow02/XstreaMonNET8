namespace XstreaMonNET8
{
    public class Stream_Info
    {
        internal int Bandwith { get; set; }
        internal string Resolution { get; set; } = string.Empty;
        internal string Name { get; set; } = string.Empty;
        internal string M3U8String { get; set; } = string.Empty;

        internal Stream_Info(string Stream_String)
        {
            if (string.IsNullOrWhiteSpace(Stream_String))
                return;

            string lowered = Stream_String.ToLowerInvariant();

            Bandwith = ValueBack.Get_CInteger(VParse.HTML_Value(lowered, "bandwidth=", ","));

            if (Bandwith == 0)
            {
                string resolutionBandwidth = VParse.HTML_Value(lowered, "resolution=", "x");
                if (int.TryParse(resolutionBandwidth, out int parsedBandwidth))
                    Bandwith = parsedBandwidth;
            }

            Resolution = VParse.HTML_Value(lowered, "resolution=", ",");
            Name = VParse.HTML_Value(lowered, "name=", "https");
            M3U8String = "https:" + VParse.HTML_Value(lowered, "https:", "");
        }
    }
}
