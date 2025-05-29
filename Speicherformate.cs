namespace XstreaMonNET8
{
    internal static class Speicherformate
    {
        internal static List<Class_Speicherformate> Speicherformate_Item_List =>
            [
                new Class_Speicherformate(0, TXT.TXT_Description("Wie gesendet"), "", ""),
                new Class_Speicherformate(1, "MP4 - MPEG-4 video file", ".mp4", "")
            ];

        internal static Class_Speicherformate Speicherformate_Find(int formatId)
        {
            foreach (var speicherformat in Speicherformate_Item_List)
            {
                if (speicherformat.Pro_ID == formatId)
                    return speicherformat;
            }

            return null!;
        }
    }
}
