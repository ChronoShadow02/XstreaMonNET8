namespace XstreaMonNET8
{
    public class StreamAdresses
    {
        internal string Pro_Model_Name { get; set; }
        internal int Pro_Model_Website { get; set; }
        internal string? Pro_M3U8_Path { get; set; }
        internal string? Pro_TS_Path { get; set; }
        internal string? Pro_Audio_Path { get; set; }
        internal string? Pro_AU_Path { get; set; }
        internal string? Pro_FFMPEG_Path { get; set; }
        internal string? Pro_Preview_Image { get; set; }
        internal int Pro_Qualität_ID { get; set; }
        internal int Pro_Record_Resolution { get; set; }
        internal bool Pro_Access_Denied { get; set; }
        internal string? Pro_Model_Online_ID { get; set; }

        internal StreamAdresses(string modelName, int modelWebsite, int qualitaetId)
        {
            Pro_Model_Name = modelName;
            Pro_Model_Website = modelWebsite;
            Pro_Qualität_ID = qualitaetId;
            Pro_Access_Denied = false;

            try
            {
                Sites.Stream_Adressen_Load(this);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "StreamAdresses.New");
            }
        }

        internal StreamAdresses(string modelName, int modelWebsite, int qualitaetId, string htmlText)
        {
            Pro_Model_Name = modelName;
            Pro_Model_Website = modelWebsite;
            Pro_Qualität_ID = qualitaetId;
            Pro_Access_Denied = false;

            try
            {
                Sites.Stream_Adressen_Load(this, htmlText);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "StreamAdresses.New HTML");
            }
        }
    }

}
