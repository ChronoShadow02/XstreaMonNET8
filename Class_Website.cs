namespace XstreaMonNET8
{
    public class Class_Website
    {
        internal int Pro_ID { get; set; }
        internal string Pro_Name { get; set; }
        internal string Pro_URL { get; set; }
        internal string Pro_Model_URL { get; set; }
        internal Image Pro_Image { get; set; }
        internal int Pro_Intervall_Max { get; set; }
        internal int Pro_Intervall_Min { get; set; }
        internal bool Pro_ShowAll { get; set; }
        internal string Pro_Domain { get; set; }
        internal string Pro_Home_URL { get; set; }
        internal string Pro_Sequence { get; set; }
        internal string Pro_Model_Offline_URL { get; set; }
        internal bool Pro_Must_Convert { get; set; }

        internal async Task<Channel_Info> Profil(string modelName)
        {
            await Task.CompletedTask;
            return Pro_ID switch
            {
                0 => await Chaturbate.Profil(modelName),
                1 => await Camsoda.Profil(modelName),
                2 => await Stripchat.Profil(modelName),
                3 => await Bongacams.Profil(modelName),
                4 => await Cam4.Profil(modelName),
                5 => await Streamate.Profil(modelName),
                6 => await Flirt4Free.Profil(modelName),
                7 => await MyFreeCams.Profil(modelName),
                8 => await Jerkmate.Profil(modelName),
                9 => await CamsCom.Profil(modelName),
                10 => await Camster.Profil(modelName),
                11 => await Freeoneslive.Profil(modelName),
                12 => await EPlay.Profil(modelName),
                _ => null
            };
        }

        public Class_Website(int id, string name, string url, Image image, int refreshMin, int refreshMax, bool showAll, string domain, string siteUrl, string homeUrl, string sequence, string offlineUrl, bool mustConvert)
        {
            Pro_ID = id;
            Pro_Name = name;
            Pro_URL = url;
            Pro_Image = image;
            Pro_Intervall_Min = refreshMin;
            Pro_Intervall_Max = refreshMax;
            Pro_ShowAll = showAll;
            Pro_Domain = domain;
            Pro_Model_URL = siteUrl;
            Pro_Home_URL = homeUrl;
            Pro_Sequence = sequence;
            Pro_Model_Offline_URL = offlineUrl;
            Pro_Must_Convert = mustConvert;
        }
    }
}
