using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace XstreaMonNET8
{
    public class Class_Stream_Record : IDisposable
    {
        private bool disposedValue;
        internal int ProzessID;
        internal Guid Record_GUID;

        internal Guid Pro_Record_GUID { get; set; }
        internal Guid Pro_User_GUID { get; set; }
        internal string Pro_User_Name { get; set; }
        internal string Pro_Recordname { get; set; }
        internal DateTime Pro_Record_Beginn { get; set; }
        internal DateTime Pro_Record_Ende { get; set; }
        internal int Pro_Record_PID { get; set; }
        internal int Pro_Auflösung { get; set; }
        private object _Pro_Maschine;
        internal object Pro_Maschine
        {
            get => _Pro_Maschine;
            set => _Pro_Maschine = value;
        }

        internal bool Pro_Favorite { get; set; }
        internal int Pro_Website_ID { get; set; }
        internal Class_Decoder_Item Pro_Decoder_item { get; set; }
        internal string Pro_Stream_Extension { get; set; }

        internal event Stream_RunEventHandler Stream_Run;
        internal event Stream_StopEventHandler Stream_Stop;

        public Class_Stream_Record()
        {
            ProzessID = 0;
        }

        internal Class_Stream_Record(Class_Model Model_Class)
        {
            this.ProzessID = 0;
            try
            {
                string str1 = this.File_Name_Generate(Model_Class);
                this.Pro_Decoder_item = Decoder.Decoder_Find(ValueBack.Get_CInteger((object)Model_Class.Pro_Decoder));
                if (this.Pro_Decoder_item == null || Model_Class.Pro_Model_M3U8 == null)
                    return;

                string str2 = null;
                if (string.IsNullOrEmpty(str2))
                    str2 = VParse.GetPOST(Model_Class.Pro_Model_M3U8, true.ToString()).Result;
                if (string.IsNullOrEmpty(str2))
                    str2 = VParse.Chrome_Load(Model_Class.Pro_Model_M3U8, true).Result;

                string[] strArray1 = str2?.Split('\n');
                string str3 = this.Pro_Decoder_item.Decoder_Extension;

                if (strArray1 != null)
                {
                    foreach (string str4 in strArray1)
                    {
                        if (str4.StartsWith("#EXT-X-MAP:URI="))
                        {
                            str3 = str4.Replace("\"", "").Substring(str4.Replace("\"", "").LastIndexOf('.'));
                            break;
                        }
                        if (str4.StartsWith("http") || str4.StartsWith("media") || str4.Contains(".ts"))
                        {
                            string str5 = str4.Replace("\"", "");
                            str5 = str5.Substring(str5.LastIndexOf('.'));
                            if (str5.Contains("?"))
                                str5 = str5.Substring(0, str5.IndexOf('?'));
                            str3 = str5;
                            break;
                        }
                    }
                }

                if (!str3.StartsWith(".ts") && !str3.StartsWith(".mp4"))
                    str3 = ".mp4";

                string str6 = Modul_Ordner.DateiName(Model_Class.Pro_Model_Directory, str1 + this.Pro_Decoder_item.Decoder_Extension);
                if (str6 == null) return;

                Class_Driveinfo classDriveinfo = new(Model_Class.Pro_Model_Directory);
                if (!classDriveinfo.Letter.StartsWith("\\\\") &&
                    (classDriveinfo.Total_Size == 0.0 || classDriveinfo.Freespace / 1024.0 / 1024.0 < 1024.0))
                {
                    this.ProzessID = 0;
                }
                else
                {
                    bool noStream = !Parameter.URL_Response(Model_Class.Pro_Model_M3U8).Result;
                    bool noAudio = string.IsNullOrEmpty(Model_Class.Pro_Model_Audio_Path) ||
                                   !Parameter.URL_Response(Model_Class.Pro_Model_Audio_Path).Result;

                    if (noStream || noAudio)
                        Model_Class.Model_Stream_Adressen_Load();

                    if (!string.IsNullOrEmpty(Model_Class.Pro_Model_M3U8) && Model_Class.Get_Pro_Model_Online(true))
                    {
                        string ext = Model_Class.Pro_SaveFormat.Pro_ID > 0
                            ? Model_Class.Pro_SaveFormat.Pro_Speicherformat_File_Ext
                            : this.Pro_Decoder_item.Decoder_Extension;

                        this.ProzessID = this.Stream_Record_Start(Model_Class, str6, ext != str3).Result;
                    }
                }

                if (this.ProzessID <= 0)
                    return;

                this.Record_GUID = Guid.NewGuid();
                string str7 = Model_Class.Pro_SaveFormat.Pro_ID > 0
                    ? Model_Class.Pro_SaveFormat.Pro_Speicherformat_File_Ext
                    : str3;

                using OleDbConnection oleDbConnection = new OleDbConnection
                {
                    ConnectionString = Database_Connect.Aktiv_Datenbank()
                };
                using OleDbDataAdapter adapter = new OleDbDataAdapter("Select * From DT_Record where Record_GUID = '" + this.Record_GUID + "'", oleDbConnection.ConnectionString);
                using OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                using DataSet dataSet = new DataSet();

                oleDbConnection.Open();
                if (oleDbConnection.State == ConnectionState.Open)
                {
                    adapter.Fill(dataSet, "DT_Records");
                    oleDbConnection.Close();

                    DataRow row = dataSet.Tables[0].NewRow();
                    row["Record_GUID"] = this.Record_GUID;
                    row["User_GUID"] = Model_Class.Pro_Model_GUID;
                    row["User_Name"] = Model_Class.Pro_Model_Name;
                    row["Record_Name"] = new FileInfo(str6).Name;
                    row["Record_Beginn"] = DateTime.Now;
                    row["Record_PID"] = this.ProzessID;
                    row["Maschine"] = Environment.MachineName;
                    row["Record_Convert_Ext"] = str7;
                    row["Record_Encoder_ID"] = this.Pro_Decoder_item.Decoder_ID;
                    row["Record_M3U8"] = Model_Class.Pro_Model_M3U8;
                    dataSet.Tables[0].Rows.Add(row);
                    adapter.Update(dataSet.Tables[0]);

                    this.Pro_Record_GUID = this.Record_GUID;
                    this.Pro_User_GUID = Model_Class.Pro_Model_GUID;
                    this.Pro_User_Name = Model_Class.Pro_Model_Name;
                    this.Pro_Recordname = Path.Combine(Model_Class.Pro_Model_Directory, str6);
                    this.Pro_Website_ID = Model_Class.Pro_Website_ID;
                    this.Pro_Record_PID = this.ProzessID;
                    this.Pro_Record_Beginn = DateTime.Now;
                    this.Pro_Stream_Extension = str7;

                    this.Stream_Run?.Invoke(this);//Stream_RunEvent
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.New");
            }
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                Pro_Record_GUID = Guid.Empty;
                Pro_User_GUID = Guid.Empty;
                Pro_Record_PID = 0;
                Pro_Decoder_item = null;
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
            Parameter.FlushMemory();
        }

        internal string File_Name_Generate(Class_Model Model_Class)
        {
            string template = IniFile.Read(Parameter.INI_Common, "Record", "Name", "{WK}-{Year}-{Month}-{Day}-{Hour}-{Minute}");
            DateTime now = DateTime.Now;
            string newValue = Model_Class.Pro_Website_ID switch
            {
                0 => "CU",
                1 => "CS",
                2 => "SC",
                3 => "BC",
                4 => "C4",
                5 => "SM",
                6 => "F4",
                7 => "MF",
                8 => "JM",
                9 => "CC",
                10 => "CA",
                11 => "FO",
                12 => "EP",
                _ => ""
            };

            string description = string.IsNullOrWhiteSpace(Model_Class.Pro_Model_Description)
                ? Model_Class.Pro_Model_Name
                : Model_Class.Pro_Model_Description;

            return template
                .Replace("{WK}", newValue)
                .Replace("{Year}", now.Year.ToString())
                .Replace("{Month}", now.Month.ToString())
                .Replace("{MO}", now.Month.ToString("D2"))
                .Replace("{Day}", now.Day.ToString())
                .Replace("{DY}", now.Day.ToString("D2"))
                .Replace("{Hour}", now.Hour.ToString())
                .Replace("{HO}", now.Hour.ToString("D2"))
                .Replace("{Minute}", now.Minute.ToString())
                .Replace("{MI}", now.Minute.ToString("D2"))
                .Replace("{Seconds}", now.Second.ToString())
                .Replace("{SE}", now.Second.ToString("D2"))
                .Replace("{Name}", description);
        }

        internal async Task<int> Stream_Record_Start(Class_Model Model_Class, string File_Name, bool Convert)
        {
            await Task.CompletedTask;
            try
            {
                string filePath = Path.Combine(Model_Class.Pro_Model_Directory, File_Name);
                if (!Directory.Exists(Model_Class.Pro_Model_Directory))
                    Directory.CreateDirectory(Model_Class.Pro_Model_Directory);

                int pid = 0;
                Pro_Decoder_item = Decoder.Decoder_Find(ValueBack.Get_CInteger(Model_Class.Pro_Decoder));
                if (!string.IsNullOrWhiteSpace(Model_Class.Pro_Model_M3U8))
                {
                    int decoder = ValueBack.Get_CInteger(Model_Class.Pro_Decoder);
                    if (decoder == 0)
                        pid = TS_Record(Model_Class, File_Name, Convert);
                    else if (decoder == 1 || decoder == 2)
                        pid = FF_Record(Model_Class, filePath, Pro_Decoder_item);
                }

                return pid;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Class_Stream_Record.Stream_Record_Start - {Model_Class.Pro_Model_Name}");
                return 0;
            }
        }

        internal static int TS_Record(Class_Model Model_Class, string File_name, bool ConvertToMp4)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Model_Class.Pro_Model_M3U8))
                    return 0;

                var psi = new ProcessStartInfo
                {
                    FileName = $"\"{AppDomain.CurrentDomain.BaseDirectory}CRStreamRec.exe\"",
                    WindowStyle = IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False") == "True" ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden,
                    CreateNoWindow = IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False") != "True"
                };

                var site = Sites.Website_Find(Model_Class.Pro_Website_ID);
                var args = $" |Debug:{(psi.WindowStyle == ProcessWindowStyle.Normal ? "True" : "False")}" +
                           $" |Site:\"{site.Pro_Name}\"" +
                           $" |Name:\"{Model_Class.Pro_Model_Name}\"" +
                           $" |File:\"{File_name}\"" +
                           $" |Data:\"{Model_Class.Pro_Model_Directory}\"" +
                           $" |Covert:{ConvertToMp4}" +
                           $" |Res:{Model_Class.Pro_Videoqualität}" +
                           $" |M3U8:{Model_Class.Pro_Model_M3U8}" +
                           $" |TS:{Model_Class.Pro_Model_TS_Path}" +
                           $" |Audio:{Model_Class.Pro_Model_Audio_Path}" +
                           $" |AU:{Model_Class.Pro_Model_AU_Path}" +
                           $" |Squence:\"{site.Pro_Sequence}\"";

                psi.Arguments = args;
                return Process.Start(psi)?.Id ?? 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.TS_Record");
                return 0;
            }
        }

        internal int FF_Record(Class_Model Model_Class, string File_name, Class_Decoder_Item Decoder_Item)
        {
            try
            {
                Pro_Decoder_item = Decoder_Item;
                if (string.IsNullOrWhiteSpace(Model_Class.Pro_Model_M3U8))
                    return 0;

                string args = IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False") == "True"
                    ? " -loglevel verbose -stats"
                    : " -loglevel warning -stats";

                string input = string.IsNullOrWhiteSpace(Model_Class.Pro_Model_FFMPEG_Path)
                    ? Model_Class.Pro_Model_M3U8.Trim()
                    : Model_Class.Pro_Model_FFMPEG_Path;

                var psi = new ProcessStartInfo
                {
                    FileName = $"\"{AppDomain.CurrentDomain.BaseDirectory}RecordStream.exe\"",
                    Arguments = $"{args} -i \"{input}\" {Decoder_Item.Decoder_Parameter}\"{File_name}\"",
                    WindowStyle = args.Contains("verbose") ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden,
                    CreateNoWindow = !args.Contains("verbose")
                };

                return Process.Start(psi)?.Id ?? 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.FF_Record");
                return 0;
            }
        }

        internal bool Stream_Record_Stop()
        {
            if (Parameter.Task_Quit(ProzessID).Result || !Parameter.Task_Runs(ProzessID))
            {
                if (!Parameter.Recording_Stop)
                    Record_Nachbereitung();

                return true;
            }

            return false;
        }

        private async void Record_Nachbereitung()
        {
            await Task.CompletedTask;
            try
            {
                if (Pro_Recordname == null) return;

                bool? canConvert = Pro_Decoder_item?.Decoder_CanConvert;
                bool extDiffers = !string.IsNullOrWhiteSpace(Pro_Stream_Extension) &&
                                  new FileInfo(Pro_Recordname).Extension != Pro_Stream_Extension;

                if (canConvert == true && extDiffers)
                    Record_Nachbereitung_Convert();
                else
                    Record_Nachbereitung_DBSave();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.Record_Nachbereitung");
            }
        }

        private void Record_Nachbereitung_Convert()
        {
            try
            {
                var converter = new Video_Convert(Pro_Recordname, Pro_Stream_Extension);
                converter.Video_Convert_Ready += Record_Nachbereitung_DBSave;
                Pro_Recordname = converter.Pri_Ziel_File;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.Record_Nachbereitung_Convert");
            }
        }

        private async void Record_Nachbereitung_DBSave()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (File.Exists(Pro_Recordname) &&
                        new FileInfo(Pro_Recordname).Length < ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Record", "MinSize", "0")) * 1048576)
                    {
                        File.Delete(Pro_Recordname);
                        return;
                    }

                    using var conn = new OleDbConnection(Database_Connect.Aktiv_Datenbank());
                    using var adapter = new OleDbDataAdapter($"Select * From DT_Record where Record_GUID = '{Record_GUID}'", conn);
                    using var builder = new OleDbCommandBuilder(adapter);
                    using var ds = new DataSet();

                    conn.Open();
                    adapter.Fill(ds, "DT_Records");
                    conn.Close();

                    var table = ds.Tables["DT_Records"];
                    if (table.Rows.Count == 1 && File.Exists(Pro_Recordname))
                    {
                        var row = table.Rows[0];
                        using var mediaInfo = new Class_MediaInfo(Pro_Recordname, Pro_User_Name, Pro_Website_ID, Pro_Record_Beginn);
                        row["Record_Favorit"] = Pro_Favorite;
                        row["Record_Länge_Minuten"] = mediaInfo.Pro_Record_Länge;
                        row["Record_Ende"] = mediaInfo.Pro_Record_Ende;
                        row["Record_Resolution"] = mediaInfo.Pro_Record_Resolution;
                        row["Record_FrameRate"] = mediaInfo.Pro_Record_FrameRate;
                        row["Record_Name"] = Path.GetFileName(Pro_Recordname);
                        adapter.Update(table);

                        if (!Modul_Ordner.DateiInBenutzung(Pro_Recordname))
                        {
                            Preview_Create();
                            if (ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Favorite", "Copy", "False")) && Pro_Favorite)
                                Favoriten_Copy();
                        }
                    }
                    else if (table.Rows.Count == 1 && !File.Exists(Pro_Recordname))
                    {
                        table.Rows[0].Delete();
                        adapter.Update(table);
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Class_Stream_Record.Record_Nachbereitung_DBSave");
                }
            });
        }

        private async void Preview_Create()
        {
            await Task.Run(() =>
            {
                try
                {
                    Preview_Files_Create(Pro_Recordname, Pro_User_Name, Pro_Website_ID, Pro_Record_Beginn);
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Class_Stream_Record.Preview_Create");
                }
            });
        }

        private async void Favoriten_Copy()
        {
            await Task.Run(() =>
            {
                try
                {
                    string destFolder = Modul_Ordner.Favoriten_Pfad();
                    if (!Directory.Exists(destFolder))
                        Directory.CreateDirectory(destFolder);

                    string fileName = Path.GetFileName(Pro_Recordname);
                    File.Copy(Pro_Recordname, Path.Combine(destFolder, fileName));
                    File.Copy(Pro_Recordname + ".vdb", Path.Combine(destFolder, fileName + ".vdb"));
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Class_Stream_Record.Record_Nachbereitung.FavoritenCopy");
                }
            });
        }

        public static async void Preview_Files_Create(string Record_File, string Record_user_Name, int Record_Website_ID, DateTime Record_Beginn)
        {
            await Task.CompletedTask;
            try
            {
                if (!File.Exists(Record_File))
                    return;

                using var ds = new DataSet("RecordFile");
                var dt = new DataTable("DT_RecordFile");

                dt.Columns.Add("Channel_GUID");
                dt.Columns.Add("Channel_Name");
                dt.Columns.Add("Record_Name");
                dt.Columns.Add(nameof(Record_Beginn));
                dt.Columns.Add("Record_Ende");
                dt.Columns.Add("Record_Länge_Minuten");
                dt.Columns.Add("Record_Resolution");
                dt.Columns.Add("Record_FrameRate");
                dt.Columns.Add("Record_Site");
                dt.Columns.Add("Record_M3U8");
                dt.Columns.Add("Record_Maschine");
                dt.Columns.Add("Video_Preview", typeof(byte[]));
                dt.Columns.Add("Video_Timeline", typeof(byte[]));
                dt.Columns.Add("Video_Tiles", typeof(byte[]));

                using var mediaInfo = new Class_MediaInfo(Record_File, Record_user_Name, Record_Website_ID, Record_Beginn);
                var row = dt.NewRow();
                row["Channel_Name"] = Record_user_Name;
                row["Record_Beginn"] = Record_Beginn;
                row["Record_Name"] = Path.GetFileName(Record_File);
                row["Record_Site"] = Record_Website_ID;
                row["Record_Ende"] = mediaInfo.Pro_Record_Ende;
                row["Record_Länge_Minuten"] = mediaInfo.Pro_Record_Länge;
                row["Record_Resolution"] = mediaInfo.Pro_Record_Resolution;
                row["Record_FrameRate"] = mediaInfo.Pro_Record_FrameRate;
                row["Video_Timeline"] = mediaInfo.Pro_TimeLine_Byte;
                row["Video_Tiles"] = mediaInfo.Pro_Tiles_Byte;
                row["Video_Preview"] = mediaInfo.Pro_Preview_Byte;

                dt.Rows.Add(row);
                ds.Tables.Add(dt);
                ds.WriteXml(Record_File + ".vdb", XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.Preview_Create - " + Record_File);
            }
        }

        internal delegate void Stream_RunEventHandler(Class_Stream_Record Record_Class);
        internal delegate void Stream_StopEventHandler(Class_Stream_Record Record_Class);
    }
}
