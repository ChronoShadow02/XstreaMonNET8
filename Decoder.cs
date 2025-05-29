namespace XstreaMonNET8
{
    internal static class Decoder
    {
        internal static List<Class_Decoder_Item> Decoder_Item_List
        {
            get
            {
                return
                [
                    new(0, "CRStreamRec", "", ".ts", true, true),
                    new(1, "FFMPEG COPY", " -c:v copy -c:a copy -movflags faststart -y -f mp4 ", ".mp4", true, false)
                ];
            }
        }

        internal static Class_Decoder_Item? Decoder_Find(int Decoder_ID)
        {
            foreach (var decoderItem in Decoder_Item_List)
            {
                if (decoderItem.Decoder_ID == Decoder_ID)
                    return decoderItem;
            }
            return null;
        }
    }
}
