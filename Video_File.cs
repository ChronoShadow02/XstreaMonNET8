namespace XstreaMonNET8
{
    public class Video_File
    {
        internal string? Pro_Pfad { get; set; }

        internal DateTime Pro_Start { get; set; }

        internal DateTime Pro_Ende { get; set; }

        internal string? Pro_Bezeichnung { get; set; }

        internal Guid Pro_Video_GUID { get; set; }

        internal bool Pro_Favorite { get; set; }

        internal Guid Pro_Model_GUID { get; set; }

        internal string? Pro_Model_Name { get; set; }

        internal bool Pro_Run_Record { get; set; }

        internal int Pro_Video_Länge { get; set; }

        internal string? Pro_Resolution { get; set; }

        internal int Pro_FrameRate { get; set; }

        internal bool Pro_IsInDB { get; set; }

        internal int Pro_Website_ID { get; set; }

        internal bool Pro_Is_Image { get; set; }
    }
}
