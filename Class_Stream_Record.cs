using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Text;

namespace XstreaMonNET8
{
    public class Class_Stream_Record
    {
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
        internal object Pro_Maschine { get; set; }
        internal bool Pro_Favorite { get; set; }
        internal int Pro_Website_ID { get; set; }
        internal Class_Decoder_Item Pro_Decoder_item { get; set; }
        internal string Pro_Stream_Extension { get; set; }

        public Class_Stream_Record()
        {
            ProzessID = 0;
        }

        public Class_Stream_Record(Class_Model Model_Class)
        {
            ProzessID = 0;

            try
            {
                string str1 = File_Name_Generate(Model_Class);
                Pro_Decoder_item = Decoder.Decoder_Find(Value_Back.get_CInteger(Model_Class.Pro_Decoder));
                if (Pro_Decoder_item == null || Model_Class.Pro_Model_M3U8 == null)
                    return;

                string m3u8 = VParse.GetPOST(Model_Class.Pro_Model_M3U8, "True").Result;
                if (string.IsNullOrEmpty(m3u8))
                    m3u8 = VParse.Chrome_Load(Model_Class.Pro_Model_M3U8, true).Result;

                string[] lines = m3u8?.Split('\n') ?? Array.Empty<string>();
                string extension = Pro_Decoder_item.Decoder_Extension;

                foreach (string line in lines)
                {
                    if (line.StartsWith("#EXT-X-MAP:URI="))
                    {
                        string cleaned = line.Replace("\"", "");
                        extension = cleaned.Substring(cleaned.LastIndexOf('.'));
                        break;
                    }

                    if (line.StartsWith("http") || line.StartsWith("media") || line.Contains(".ts"))
                    {
                        string temp = line.Replace("\"", "");
                        int dotIndex = temp.LastIndexOf('.');
                        if (dotIndex > -1)
                        {
                            extension = temp.Substring(dotIndex);
                            int queryIndex = extension.IndexOf('?');
                            if (queryIndex > -1)
                                extension = extension.Substring(0, queryIndex);
                        }
                        break;
                    }
                }

                if (!extension.StartsWith(".ts") && !extension.StartsWith(".mp4"))
                    extension = ".mp4";

                string fullFileName = Modul_Ordner.DateiName(Model_Class.Pro_Model_Directory, str1 + Pro_Decoder_item.Decoder_Extension);
                if (fullFileName == null)
                    return;

                var driveInfo = new Class_Driveinfo(Model_Class.Pro_Model_Directory);
                if (!driveInfo.Letter.StartsWith("\\\\") && (driveInfo.Total_Size == 0.0 || driveInfo.Freespace / 1024.0 / 1024.0 < 1024.0))
                {
                    ProzessID = 0;
                }
                else
                {
                    bool needsReload = !Parameter.URL_Response(Model_Class.Pro_Model_M3U8).Result;
                    bool validAudio = string.IsNullOrEmpty(Model_Class.Pro_Model_Audio_Path) || Parameter.URL_Response(Model_Class.Pro_Model_Audio_Path).Result;
                    if (needsReload || !validAudio)
                        Model_Class.Model_Stream_Adressen_Load();

                    if (!string.IsNullOrEmpty(Model_Class.Pro_Model_M3U8) && Model_Class.get_Pro_Model_Online(true))
                    {
                        bool convert = (Model_Class.Pro_SaveFormat.Pro_ID > 0 ? Model_Class.Pro_SaveFormat.Pro_Speicherformat_File_Ext : Pro_Decoder_item.Decoder_Extension) != extension;
                        ProzessID = Stream_Record_Start(Model_Class, fullFileName, convert).Result;
                    }
                }

                if (ProzessID <= 0)
                    return;

                Record_GUID = Guid.NewGuid();
                string streamExt = Model_Class.Pro_SaveFormat.Pro_ID > 0 ? Model_Class.Pro_SaveFormat.Pro_Speicherformat_File_Ext : extension;

                using var connection = new OleDbConnection(Database_Connect.Aktiv_Datenbank());
                using var adapter = new OleDbDataAdapter("Select * From DT_Record where Record_GUID = '" + Record_GUID.ToString() + "'", connection.ConnectionString);
                using var builder = new OleDbCommandBuilder(adapter);
                using var ds = new DataSet();

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    adapter.Fill(ds, "DT_Records");
                    connection.Close();

                    var row = ds.Tables[0].NewRow();
                    row["Record_GUID"] = Record_GUID;
                    row["User_GUID"] = Model_Class.Pro_Model_GUID;
                    row["User_Name"] = Model_Class.Pro_Model_Name;
                    row["Record_Name"] = new FileInfo(fullFileName).Name;
                    row["Record_Beginn"] = DateTime.Now;
                    row["Record_PID"] = ProzessID;
                    row["Maschine"] = MyProject.Computer.Name;
                    row["Record_Convert_Ext"] = streamExt;
                    row["Record_Encoder_ID"] = Pro_Decoder_item.Decoder_ID;
                    row["Record_M3U8"] = Model_Class.Pro_Model_M3U8;

                    ds.Tables[0].Rows.Add(row);
                    adapter.Update(ds.Tables[0]);

                    Pro_Record_GUID = Record_GUID;
                    Pro_User_GUID = Model_Class.Pro_Model_GUID;
                    Pro_User_Name = Model_Class.Pro_Model_Name;
                    Pro_Recordname = Path.Combine(Model_Class.Pro_Model_Directory, fullFileName);
                    Pro_Website_ID = Model_Class.Pro_Website_ID;
                    Pro_Record_PID = ProzessID;
                    Pro_Record_Beginn = DateTime.Now;
                    Pro_Stream_Extension = streamExt;

                    Stream_Run?.Invoke(this);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.New");
            }
        }

        internal string File_Name_Generate(Class_Model Model_Class)
        {
            string plantilla = IniFile.Read(Parameter.INI_Common, "Record", "Name", "{WK}-{Year}-{Month}-{Day}-{Hour}-{Minute}");
            DateTime ahora = DateTime.Now;

            string wk = Model_Class.Pro_Website_ID switch
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

            string year = ahora.Year.ToString();
            string month = ahora.Month.ToString();
            string mo = ahora.Month < 10 ? $"0{ahora.Month}" : ahora.Month.ToString();
            string day = ahora.Day.ToString();
            string dy = ahora.Day < 10 ? $"0{ahora.Day}" : ahora.Day.ToString();
            string hour = ahora.Hour.ToString();
            string ho = ahora.Hour < 10 ? $"0{ahora.Hour}" : ahora.Hour.ToString();
            string minute = ahora.Minute.ToString();
            string mi = ahora.Minute < 10 ? $"0{ahora.Minute}" : ahora.Minute.ToString();
            string second = ahora.Second.ToString();
            string se = ahora.Second < 10 ? $"0{ahora.Second}" : ahora.Second.ToString();
            string name = string.IsNullOrEmpty(Model_Class.Pro_Model_Description) ? Model_Class.Pro_Model_Name : Model_Class.Pro_Model_Description;

            return plantilla
                .Replace("{WK}", wk)
                .Replace("{Year}", year)
                .Replace("{Month}", month)
                .Replace("{MO}", mo)
                .Replace("{Day}", day)
                .Replace("{DY}", dy)
                .Replace("{Hour}", hour)
                .Replace("{HO}", ho)
                .Replace("{Minute}", minute)
                .Replace("{MI}", mi)
                .Replace("{Seconds}", second)
                .Replace("{SE}", se)
                .Replace("{Name}", name);
        }

        internal async Task<int> Stream_Record_Start(Class_Model Model_Class, string File_Name, bool Convert)
        {
            await Task.CompletedTask;

            try
            {
                string filePath = Path.Combine(Model_Class.Pro_Model_Directory, File_Name);
                if (!Directory.Exists(Model_Class.Pro_Model_Directory))
                    Directory.CreateDirectory(Model_Class.Pro_Model_Directory);

                int num = 0;

                this.Pro_Decoder_item = Decoder.Decoder_Find(Value_Back.get_CInteger(Model_Class.Pro_Decoder));

                if (!string.IsNullOrEmpty(Model_Class.Pro_Model_M3U8))
                {
                    int decoderId = ValueBack.Get_CInteger(Model_Class.Pro_Decoder);

                    if (decoderId == 0)
                    {
                        num = Class_Stream_Record.TS_Record(Model_Class, File_Name, Convert);
                    }
                    else if (decoderId == 1 || decoderId == 2)
                    {
                        num = this.FF_Record(Model_Class, filePath, this.Pro_Decoder_item);
                    }
                }

                return num;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.Stream_Record_Start" + Model_Class.Pro_Model_Name);
                return 0;
            }
        }

        internal static int TS_Record(Class_Model Model_Class, string File_name, bool ConvertToMp4)
        {
            try
            {
                int pid = 0;

                if (!string.IsNullOrEmpty(Model_Class.Pro_Model_M3U8))
                {
                    string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CRStreamRec.exe");
                    ProcessStartInfo startInfo = new()
                    {
                        FileName = $"\"{exePath}\""
                    };

                    string args = "";
                    string debugValue = IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False");

                    if (debugValue.Equals("True", StringComparison.OrdinalIgnoreCase))
                    {
                        args += " |Debug:True";
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;
                        startInfo.CreateNoWindow = false;
                    }
                    else
                    {
                        args += " |Debug:False";
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.CreateNoWindow = true;
                    }

                    var site = Sites.Website_Find(Model_Class.Pro_Website_ID);

                    args += $" |Site:\"{site.Pro_Name}\"";
                    args += $" |Name:\"{Model_Class.Pro_Model_Name}\"";
                    args += $" |File:\"{File_name}\"";
                    args += $" |Data:\"{Model_Class.Pro_Model_Directory}\"";
                    args += $" |Covert:{ConvertToMp4}";
                    args += $" |Res:{Model_Class.Pro_Videoqualität}";
                    args += $" |M3U8:{Model_Class.Pro_Model_M3U8}";
                    args += $" |TS:{Model_Class.Pro_Model_TS_Path}";
                    args += $" |Audio:{Model_Class.Pro_Model_Audio_Path}";
                    args += $" |AU:{Model_Class.Pro_Model_AU_Path}";
                    args += $" |Squence:\"{site.Pro_Sequence}\"";

                    startInfo.Arguments = args;

                    pid = Process.Start(startInfo)!.Id;
                }

                return pid;
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
                int num2 = 0;
                this.Pro_Decoder_item = Decoder_Item;

                if (!string.IsNullOrEmpty(Model_Class.Pro_Model_M3U8))
                {
                    string str1 = " -loglevel warning -stats";

                    string debugFlag = IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False");
                    if (debugFlag.Equals("True", StringComparison.OrdinalIgnoreCase))
                        str1 = " -loglevel verbose -stats";

                    string str2 = Model_Class.Pro_Model_M3U8.Trim();
                    if (!string.IsNullOrEmpty(Model_Class.Pro_Model_FFMPEG_Path))
                        str2 = Model_Class.Pro_Model_FFMPEG_Path;

                    string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecordStream.exe");
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = $"\"{exePath}\"",
                        Arguments = $"{str1} -i \"{str2}\"{Decoder_Item.Decoder_Parameter}\"{File_name.Trim()}\""
                    };

                    if (debugFlag.Equals("True", StringComparison.OrdinalIgnoreCase))
                    {
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;
                        startInfo.CreateNoWindow = false;
                    }
                    else
                    {
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.CreateNoWindow = true;
                    }

                    num2 = Process.Start(startInfo)!.Id;
                }

                return num2;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.FF_Record");
                return 0;
            }
        }

        internal bool Stream_Record_Stop()
        {
            if (Parameter.Task_Quit(this.ProzessID).Result || !Parameter.Task_Runs(this.ProzessID))
            {
                if (!Parameter.Recording_Stop)
                    this.Record_Nachbereitung();
                return true;
            }
            return false;
        }

        private async void Record_Nachbereitung()
        {
            await Task.CompletedTask;
            try
            {
                if (this.Pro_Recordname == null)
                    return;

                bool? decoderCanConvert = this.Pro_Decoder_item?.Decoder_CanConvert;

                bool? debeConvertir = decoderCanConvert.HasValue && decoderCanConvert.Value &&
                                      !string.IsNullOrEmpty(this.Pro_Stream_Extension);

                if (debeConvertir == true &&
                    string.Compare(Path.GetExtension(this.Pro_Recordname), this.Pro_Stream_Extension, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    this.Record_Nachbereitung_Convert();
                }
                else
                {
                    this.Record_Nachbereitung_DBSave();
                }
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
                var videoConvert = new Video_Convert(this.Pro_Recordname, this.Pro_Stream_Extension);
                videoConvert.Video_Convert_Ready += this.Record_Nachbereitung_DBSave;
                this.Pro_Recordname = videoConvert.Pri_Ziel_File;
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
                    if (File.Exists(this.Pro_Recordname))
                    {
                        string minSizeStr = IniFile.Read(Parameter.INI_Common, "Record", "MinSize", "0");
                        if (int.TryParse(minSizeStr, out int minSizeMB))
                        {
                            long minSizeBytes = minSizeMB * 1048576L;
                            if (new FileInfo(this.Pro_Recordname).Length < minSizeBytes)
                            {
                                File.Delete(this.Pro_Recordname);
                            }
                        }
                    }

                    using (OleDbConnection connection = new OleDbConnection(Database_Connect.Aktiv_Datenbank()))
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter($"Select * From DT_Record where Record_GUID = '{this.Record_GUID}'", connection.ConnectionString))
                    using (OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter))
                    using (DataSet dataSet = new DataSet())
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            adapter.Fill(dataSet, "DT_Records");
                            connection.Close();

                            var recordsTable = dataSet.Tables["DT_Records"];
                            if (recordsTable!.Rows.Count == 1)
                            {
                                if (File.Exists(this.Pro_Recordname))
                                {
                                    using (var mediaInfo = new Class_MediaInfo(this.Pro_Recordname, this.Pro_User_Name, this.Pro_Website_ID, this.Pro_Record_Beginn))
                                    {
                                        var row = recordsTable.Rows[0];
                                        row["Record_Favorit"] = this.Pro_Favorite;
                                        row["Record_Länge_Minuten"] = mediaInfo.Pro_Record_Länge;
                                        row["Record_Ende"] = mediaInfo.Pro_Record_Ende;
                                        row["Record_Resolution"] = mediaInfo.Pro_Record_Resolution;
                                        row["Record_FrameRate"] = mediaInfo.Pro_Record_FrameRate;
                                        row["Record_Name"] = Path.GetFileName(this.Pro_Recordname);
                                    }

                                    adapter.Update(recordsTable);

                                    if (!Modul_Ordner.DateiInBenutzung(this.Pro_Recordname))
                                    {
                                        this.Preview_Create();

                                        string copySetting = IniFile.Read(Parameter.INI_Common, "Favorite", "Copy", "False");
                                        if (bool.TryParse(copySetting, out bool copyEnabled) && copyEnabled && this.Pro_Favorite)
                                        {
                                            this.Favoriten_Copy();
                                        }
                                    }
                                }
                                else
                                {
                                    // Si el archivo no existe pero la fila existe, se elimina la fila
                                    recordsTable.Rows[0].Delete();
                                    adapter.Update(recordsTable);
                                }
                            }
                        }
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
                    Class_Stream_Record.Preview_Files_Create(
                        this.Pro_Recordname,
                        this.Pro_User_Name,
                        this.Pro_Website_ID,
                        this.Pro_Record_Beginn
                    );
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
                    string destinoDirectorio = Modul_Ordner.Favoriten_Pfad();
                    if (!Directory.Exists(destinoDirectorio))
                        Directory.CreateDirectory(destinoDirectorio);

                    string nombreArchivo = new FileInfo(this.Pro_Recordname).Name;
                    string rutaDestino = Path.Combine(destinoDirectorio, nombreArchivo);

                    File.Copy(this.Pro_Recordname, rutaDestino, overwrite: true);
                    File.Copy(this.Pro_Recordname + ".vdb", rutaDestino + ".vdb", overwrite: true);
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
                using DataSet dataSet = new() { DataSetName = "RecordFile" };
                DataTable table = new()
                {
                    TableName = "DT_RecordFile"
                };

                table.Columns.Add("Channel_GUID");
                table.Columns.Add("Channel_Name");
                table.Columns.Add("Record_Name");
                table.Columns.Add(nameof(Record_Beginn));
                table.Columns.Add("Record_Ende");
                table.Columns.Add("Record_Länge_Minuten");
                table.Columns.Add("Record_Resolution");
                table.Columns.Add("Record_FrameRate");
                table.Columns.Add("Record_Site");
                table.Columns.Add("Record_M3U8");
                table.Columns.Add("Record_Maschine");
                table.Columns.Add("Video_Preview", typeof(byte[]));
                table.Columns.Add("Video_Timeline", typeof(byte[]));
                table.Columns.Add("Video_Tiles", typeof(byte[]));

                if (File.Exists(Record_File))
                {
                    try
                    {
                        DataRow row = table.NewRow();
                        using Class_MediaInfo classMediaInfo = new(Record_File, Record_user_Name, Record_Website_ID, Record_Beginn);

                        row["Channel_Name"] = Record_user_Name;
                        row[nameof(Record_Beginn)] = Record_Beginn;
                        row["Record_Name"] = Path.GetFileName(Record_File);
                        row["Record_Site"] = Record_Website_ID;
                        row["Record_Ende"] = classMediaInfo.Pro_Record_Ende;
                        row["Record_Länge_Minuten"] = classMediaInfo.Pro_Record_Länge;
                        row["Record_Resolution"] = classMediaInfo.Pro_Record_Resolution;
                        row["Record_FrameRate"] = classMediaInfo.Pro_Record_FrameRate;
                        row["Video_Timeline"] = classMediaInfo.Pro_TimeLine_Byte;
                        row["Video_Tiles"] = classMediaInfo.Pro_Tiles_Byte;
                        row["Video_Preview"] = classMediaInfo.Pro_Preview_Byte;

                        table.Rows.Add(row);
                        dataSet.Tables.Add(table);
                        dataSet.WriteXml(Record_File + ".vdb", XmlWriteMode.WriteSchema);
                    }
                    catch (Exception ex)
                    {
                        Parameter.Error_Message(ex, "Class_Stream_Record.Video_File_Info - " + Record_File);
                    }
                }

                table.Dispose();
                dataSet.Dispose();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Stream_Record.Preview_Create - " + Record_File);
            }
        }

        // Eventos
        internal event Stream_RunEventHandler Stream_Run;
        internal event Stream_StopEventHandler Stream_Stop;

        internal delegate void Stream_RunEventHandler(Class_Stream_Record record);
        internal delegate void Stream_StopEventHandler(Class_Stream_Record record);
    }
}
