namespace XstreaMonNET8
{
    public class Class_Website(
        int id,
        string name,
        string url,
        Image image,
        int refreshMin,
        int refreshMax,
        bool showAll,
        string domain,
        string siteUrl,
        string homeUrl,
        string sequence,
        string offlineUrl,
        bool mustConvert)
    {
        internal int Pro_ID { get; set; } = id;
        internal string Pro_Name { get; set; } = name;
        internal string Pro_URL { get; set; } = url;
        internal string Pro_Model_URL { get; set; } = siteUrl;
        internal Image Pro_Image { get; set; } = image;
        internal int Pro_Intervall_Max { get; set; } = refreshMax;
        internal int Pro_Intervall_Min { get; set; } = refreshMin;
        internal bool Pro_ShowAll { get; set; } = showAll;
        internal string Pro_Domain { get; set; } = domain;
        internal string Pro_Home_URL { get; set; } = homeUrl;
        internal string Pro_Sequence { get; set; } = sequence;
        internal string Pro_Model_Offline_URL { get; set; } = offlineUrl;
        internal bool Pro_Must_Convert { get; set; } = mustConvert;

        internal async Task<Channel_Info> Profil(string modelName)
        {
            Dictionary<int, Func<string, Task<Channel_Info>>> profilActions = new Dictionary<int, Func<string, Task<Channel_Info>>>
            {
                { 0, Chaturbate.Profil },
                { 1, Camsoda.Profil },
                { 2, Stripchat.Profil },
                { 3, Bongacams.Profil },
                { 4, Cam4.Profil },
                { 5, Streamate.Profil },
                { 6, Flirt4Free.Profil },
                { 7, MyFreeCams.Profil },
                { 8, Jerkmate.Profil },
                { 9, CamsCom.Profil },
                { 10, Camster.Profil },
                { 11, Freeoneslive.Profil },
                { 12, EPlay.Profil }
            };

            if (profilActions.TryGetValue(Pro_ID, out Func<string, Task<Channel_Info>>? action))
            {
                return await action(modelName);
            }

            return null!;
        }
    }
}