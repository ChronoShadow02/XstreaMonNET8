namespace XstreaMonNET8
{
    internal static class CamsCom
    {
        private static readonly HttpClient httpClient = new()
        {
            DefaultRequestHeaders = { { "User-Agent", "Mozilla/5.0 (CamsComBot/1.0)" } }
        };

        internal static async Task<StreamAdresses> StreamAdresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;
                string modelName = Model_Stream.Pro_Model_Name.ToLower().Trim();
                string url = $"https://camshls.cams.com/cdn-{modelName}.m3u8";

                if (await Parameter.URL_Response(url))
                {
                    Model_Stream.Pro_M3U8_Path = url;
                    Model_Stream.Pro_TS_Path = "https://camshls.cams.com/";
                    Model_Stream.Pro_Preview_Image = $"https://images3.streamray.com/images/streamray/won/jpg/{modelName.First()}/{modelName.Last()}/{modelName}_640.jpg";
                    Model_Stream.Pro_FFMPEG_Path = url;
                }
                else
                {
                    Model_Stream.Pro_M3U8_Path = "";
                    Model_Stream.Pro_TS_Path = "";
                    Model_Stream.Pro_Preview_Image = "";
                    Model_Stream.Pro_FFMPEG_Path = "";
                }

                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamsCom.Stream_Adresses");
                return null;
            }
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                await Task.CompletedTask;
                var model = await Class_Model_List.Class_Model_Find(0, Model_Name);
                if (await Parameter.URL_Response(model?.Pro_Model_M3U8))
                    return 1;

                string result = VParse.HTML_Load($"https://beta-api.cams.com/models/stream/{Model_Name}", true).Result;
                if (result.Length <= 0)
                    return 0;

                return VParse.HTML_Value(result, "online:", ",").Trim() == "1" ? 1 : 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamsCom.Online");
                return 0;
            }
        }

        internal static async Task<Image> Image_FromWeb(Class_Model Model_Class)
        {
            try
            {
                await Task.CompletedTask;
                if (string.IsNullOrEmpty(Model_Class.Pro_Model_Preview_Path))
                    return null;

                string url = $"{Model_Class.Pro_Model_Preview_Path}?{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
                using Stream stream = await httpClient.GetStreamAsync(url);
                return Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"CamsCom.Image_FromWeb({Model_Class.Pro_Model_Name})");
                return null;
            }
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;

            Channel_Info info = new()
            {
                Pro_Exist = false,
                Pro_Website_ID = 9,
                Pro_Name = Model_Name
            };

            try
            {
                string result = VParse.HTML_Load($"https://cams.com/{Model_Name}#modelsPage", true).Result;
                if (result.Length > 0)
                {
                    info.Pro_Name = VParse.HTML_Value(result, "screen_name:", ",");
                    info.Pro_Exist = !string.IsNullOrEmpty(info.Pro_Name);

                    string gender = VParse.HTML_Value(result, "gender:", ",").ToLower();
                    info.Pro_Gender = gender switch
                    {
                        "female" => 1,
                        "male" => 2,
                        "couple" => 3,
                        "transgender" => 4,
                        _ => 0
                    };

                    info.Pro_Country = VParse.HTML_Value(result, "country:", ",");
                    info.Pro_Profil_Beschreibung = $"{info.Pro_Profil_Beschreibung}{info.Pro_Country}";

                    string lastOnlineRaw = VParse.HTML_Value(result, "online_at:", ",");
                    info.Pro_Last_Online = DateTime.Parse(lastOnlineRaw).ToString();

                    string[] langs = VParse.HTML_Value(result, "languages:[", "],").Split('}');
                    foreach (string lang in langs)
                    {
                        if (!string.IsNullOrWhiteSpace(lang))
                        {
                            string name = VParse.HTML_Value(lang, "name:", "}");
                            if (!string.IsNullOrEmpty(name))
                            {
                                if (!string.IsNullOrEmpty(info.Pro_Languages))
                                    info.Pro_Languages += ", ";
                                info.Pro_Languages += name;
                            }
                        }
                    }

                    info.Pro_Birthday = ValueBack.Get_CDatum(VParse.HTML_Value(result, "public_birthdate:", ","));

                    string imageUrl = VParse.HTML_Value(result, "profile_image:", ",").Trim();
                    if (await Parameter.URL_Response(imageUrl))
                    {
                        using Stream imgStream = await httpClient.GetStreamAsync(imageUrl);
                        info.Pro_Profil_Image = Image.FromStream(imgStream);
                    }

                    info.Pro_Online = await Online(info.Pro_Name) == 1;
                }

                return info;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"CamsCom.Profil({Model_Name})");
                return info;
            }
        }
    }
}
