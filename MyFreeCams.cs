using System.Globalization;
using System.Net;

namespace XstreaMonNET8
{
    internal static class MyFreeCams
    {
        internal static async Task<StreamAdresses> Stream_Adresses(StreamAdresses Model_Stream)
        {
            try
            {
                await Task.CompletedTask;

                string configJs = await VParse.GetPOST("https://assets.mfcimg.com/_js/serverconfig.js", "");
                configJs = configJs.Replace("\"", "");

                string chatServer = VParse.HTML_Value(configJs.ToLower(), "chat_servers:[", ",").ToLower();
                string jsonData = await VParse.GetPOSTPHP($"https://www.myfreecams.com/php/FcwExtResp.php?host={chatServer}&type=14&opts=256&serv=1066&arg2=21&owner=0", SecurityProtocolType.Ssl3);

                string modelInfo = VParse.HTML_Value(jsonData.ToLower(), $"[{Model_Stream.Pro_Model_Name.ToLower()}", "]");
                if (modelInfo.Length <= 0) return null!;

                string modelId = modelInfo.Split(',')[2];
                string serverId = modelInfo.Split(',')[1];
                string prefix = modelInfo.Split(',')[6];
                string preview = "";
                string serverHost = "";

                foreach (string server in configJs.Replace("{", "").Replace("}", "").Split(','))
                {
                    if (server.StartsWith(prefix + ":"))
                    {
                        string[] parts = server.Split(':');
                        serverHost = parts[1];
                        preview = ValueBack.Get_Zahl_Extract_From_String(parts[1]);
                        break;
                    }
                }

                string streamPrefix = modelInfo.Split(',')[7] == "a" ? "a_1" : "1";
                string streamUrl = $"https://{serverHost}.myfreecams.com/NxServer/ngrp:mfc_{streamPrefix}{int.Parse(modelId):00000000}.f4v_cmaf/playlist_sfm4s.m3u8";

                string playlistRaw = await VParse.GetPOSTPHP(streamUrl, SecurityProtocolType.Ssl3);
                string[] playlist = playlistRaw.Split('#');
                if (playlist.Length < 2) return null!;

                string chunk = "";
                foreach (string line in playlist)
                {
                    switch (Model_Stream.Pro_Qualität_ID)
                    {
                        case 0 when line.Contains("chunk"): chunk = line[(line.IndexOf("chunk"))..]; goto Found;
                        case 1 when line.Contains("320x180"): chunk = line[(line.IndexOf("chunk"))..]; goto Found;
                        case 2 when line.Contains("640x360"): chunk = line[(line.IndexOf("chunk"))..]; goto Found;
                        case 3 when line.Contains("1280x720"): chunk = line[(line.IndexOf("chunk"))..]; goto Found;
                        case 4 when line.Contains("1920x1080"): chunk = line[(line.IndexOf("chunk"))..]; goto Found;
                    }
                }

                if (string.IsNullOrEmpty(chunk) && playlist[3].Contains("chunk"))
                {
                    chunk = playlist[3][(playlist[3].IndexOf("chunk"))..];
                }

            Found:
                Model_Stream.Pro_M3U8_Path = streamUrl.Replace("playlist_sfm4s.m3u8", chunk).Trim();
                Model_Stream.Pro_TS_Path = streamUrl.Replace("playlist_sfm4s.m3u8", "").Trim();
                Model_Stream.Pro_Preview_Image = $"https://snap.mfcimg.com/snapimg/{preview}/853x480/mfc_{modelId}";
                Model_Stream.Pro_Record_Resolution = Sites.Resolution_Find(playlist, chunk);

                return Model_Stream;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "MyFreeCams.Stream_Adresses" + Model_Stream.Pro_Model_Name);
                return null!;
            }
        }

        internal static async Task<int> Online(string Model_Name)
        {
            try
            {
                await Task.CompletedTask;
                var model = await Class_Model_List.Class_Model_Find(0, Model_Name);
                if (await Parameter.URL_Response(model?.Pro_Model_M3U8)) return 1;

                string result = await VParse.HTML_Load($"https://profiles.myfreecams.com/{Model_Name}", true);
                if (result.Contains("string:Online}")) return 1;
                if (result.Contains("string:InGroup}")) return 5;
                if (result.Contains("string:InPrivate}")) return 2;

                return 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "MyFreeCams.Online " + Model_Name);
                return 0;
            }
        }

        internal static async Task<Image?> Image_FromWeb(Class_Model Model_Class)
        {
            await Task.CompletedTask;
            try
            {
                if (Model_Class.Get_Pro_Model_Online() && Model_Class.Pro_Model_Preview_Path == null)
                    Model_Class.Model_Stream_Adressen_Load();

                if (Model_Class.Pro_Model_Preview_Path != null)
                {
                    using var client = new WebClient();
                    using var stream = client.OpenRead(Model_Class.Pro_Model_Preview_Path);
                    return Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "MyFreeCams.ImageFromWeb");
            }

            return null;
        }

        internal static async Task<Channel_Info> Profil(string Model_Name)
        {
            await Task.CompletedTask;
            var info = new Channel_Info
            {
                Pro_Exist = false,
                Pro_Website_ID = 7,
                Pro_Name = Model_Name,
                Pro_Profil_Beschreibung = ""
            };

            try
            {
                string result = await VParse.HTML_Load($"https://profiles.myfreecams.com/{Model_Name}", false);
                if (result.Length > 0)
                {
                    string gender = VParse.HTML_Value(result, "id=gender_value>", "</span>").Trim();
                    info.Pro_Gender = gender switch
                    {
                        "Female" => 1,
                        "Male" => 2,
                        "Couple" => 3,
                        "Trans" => 4,
                        _ => 0
                    };
                    info.Pro_Exist = gender.Length > 0;

                    if (info.Pro_Exist)
                    {
                        info.Pro_Country = VParse.HTML_Value(result, "<span class=value id=country_value>", "</span>").Replace("  ", "").Trim();
                        string ethnicity = VParse.HTML_Value(result, "<span class=value id=ethnicity_value>", "</span>").Trim();
                        string city = VParse.HTML_Value(result, "<span class=value id=city_value>", "</span>").Trim();

                        info.Pro_Country = $"{info.Pro_Country} {(info.Pro_Country.Length > 0 ? " - " : "")}{ethnicity}".Trim();
                        info.Pro_Country += (info.Pro_Country.Length > 0 ? " " : "") + city;

                        string age = VParse.HTML_Value(result, "id=age_value>", "</span>").Trim();
                        if (age.Length > 0)
                            info.Pro_Profil_Beschreibung = $"{TXT.TXT_Description("Alter")}: {age}";

                        info.Pro_Profil_Beschreibung += (info.Pro_Profil_Beschreibung.Length > 0 ? " " : "") + info.Pro_Country;

                        string lastOnline = VParse.HTML_Value(result, "data-mfc-unix-time=", " data-mfc-time-format=ddd");
                        var unixSeconds = double.TryParse(lastOnline, NumberStyles.Any, CultureInfo.InvariantCulture, out var seconds) ? seconds : 0;
                        info.Pro_Last_Online = new DateTime(1970, 1, 1).AddSeconds(unixSeconds).ToString();

                        string imgPath = VParse.HTML_Value(result, "<img id=profile_avatar class=img_radius_shadow src=", "onError=this").Trim();
                        if (await Parameter.URL_Response(imgPath))
                        {
                            using var stream = new WebClient().OpenRead(imgPath);
                            info.Pro_Profil_Image = Image.FromStream(stream);
                        }
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "MyFreeCams.Profil");
                return info;
            }
        }
    }
}
