using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Timers;
using Timer = System.Timers.Timer;

namespace XstreaMonNET8
{
    public class Class_Model
    {
        private bool disposedValue;
        private int Timer_Online_Change_Timer;
        private readonly int Model_Offline_Count;
        private Timer Timer_Model_Thumbnail;
        private object Priv_Model_GUID;
        private string Priv_Model_Name;
        private string Priv_Model_Description;
        private string Priv_Model_Directory;
        private string Priv_Model_Info;
        private int Priv_Model_Gender;
        private string Priv_Model_Country;
        private string Priv_Model_Birthday;
        private string Priv_Model_Language;
        private bool Priv_Model_Record;
        private bool Priv_Model_Favorite;
        private bool Priv_Model_Online;
        private bool Priv_Model_Deaktiv;
        private DateTime Priv_Model_Galery_Last_Visit;
        private bool Priv_Model_Access_Denied;
        private int Priv_Model_Token;
        private string Priv_Model_M3U8;
        private string Priv_Model_Audio_Path;
        private string Priv_Model_AU_Path;
        private string Priv_Model_TS_Path;
        private string Priv_Model_FFMPEG_Path;
        private int Priv_Model_Record_Resolution;
        private string Priv_Model_Preview_Path;
        private bool Priv_Model_Visible;
        private bool Priv_Model_Promo;
        private DateTime Priv_Model_Last_Online;
        private Guid Priv_Model_Online_GUID;
        private int Priv_Aufnahme_Stop_Auswahl;
        private int Priv_Aufnahme_Stop_Minuten;
        private bool Priv_Aufnahme_Stop_Off;
        private int Priv_Aufnahme_Stop_Größe;
        private int Priv_Videoqualität;
        private int Priv_Decoder;
        internal int Priv_SaveFormat;
        private bool Priv_Benachrichtigung;
        private int Priv_Website_ID;
        private Bitmap Priv_Website_Image;
        private readonly System.Windows.Forms.Timer Record_Check_Timer;
        private Timer? _Timer_Model_Online;
        private Class_Model_Online_Check? _Timer_Online_Change;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                Timer_Model_Thumbnail1?.Stop();
                Timer_Model_Online?.Stop();
                Timer_Online_Change.Pro_Timer_Intervall = 0;
                Model_Online_Changed();
            }

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal event Class_Model.Model_Online_ChangeEventHandler Model_Online_Change;
        internal event Class_Model.Model_Record_ChangeEventHandler Model_Record_Change;
        internal event Class_Model.Model_Show_NotificationEventHandler Model_Show_Notification;
        internal event Class_Model.Model_Token_ChangeEventHandler Model_Token_Change;

        internal Class_Model_Online_Check? Timer_Online_Change
        {
            get => _Timer_Online_Change;
            set
            {
                var changeHandler = new Class_Model_Online_Check.Online_Status_ChangeEventHandler(Online_Change_Timer_Online_Status_Change);
                var changedHandler = new Class_Model_Online_Check.Online_ChangedEventHandler(Online_Change_Timer_Tick);

                if (_Timer_Online_Change != null)
                {
                    _Timer_Online_Change.Online_Status_Change -= changeHandler;
                    _Timer_Online_Change.Online_Changed -= changedHandler;
                }

                _Timer_Online_Change = value;

                if (_Timer_Online_Change != null)
                {
                    _Timer_Online_Change.Online_Status_Change += changeHandler;
                    _Timer_Online_Change.Online_Changed += changedHandler;
                }
            }
        }

        private Timer? Timer_Model_Online
        {
            get => _Timer_Model_Online;
            set
            {
                ElapsedEventHandler handler = (sender, e) => Model_Online_Save();

                if (_Timer_Model_Online != null)
                    _Timer_Model_Online.Elapsed -= handler;

                _Timer_Model_Online = value;

                if (_Timer_Model_Online != null)
                    _Timer_Model_Online.Elapsed += handler;
            }
        }

        internal Guid Pro_Model_GUID
        {
            get
            {
                object privModelGuid = Priv_Model_GUID;
                return privModelGuid == null ? Guid.Empty : (Guid)privModelGuid;
            }
            set => Priv_Model_GUID = value;
        }

        internal string Pro_Model_Name
        {
            get => Priv_Model_Name;
            set => Priv_Model_Name = value;
        }

        internal string Pro_Model_Description
        {
            get => Priv_Model_Description;
            set => Priv_Model_Description = value;
        }

        internal string Pro_Model_Directory
        {
            get
            {
                if (string.IsNullOrEmpty(Priv_Model_Directory))
                    Priv_Model_Directory = Path.Combine(Modul_Ordner.Ordner_Pfad(), Pro_Model_Name);

                try
                {
                    if (!Directory.Exists(Priv_Model_Directory))
                        Directory.CreateDirectory(Priv_Model_Directory);
                }
                catch (Exception)
                {
                    Priv_Model_Directory = Path.Combine(Modul_Ordner.Ordner_Pfad(), Pro_Model_Name);
                }

                return Priv_Model_Directory;
            }

            set
            {
                Priv_Model_Directory = value;

                if (string.IsNullOrEmpty(Priv_Model_Directory))
                    Priv_Model_Directory = Path.Combine(Modul_Ordner.Ordner_Pfad(), Pro_Model_Name);

                try
                {
                    if (!Directory.Exists(Priv_Model_Directory))
                        Directory.CreateDirectory(Priv_Model_Directory);
                }
                catch (Exception)
                {
                    Priv_Model_Directory = Path.Combine(Modul_Ordner.Ordner_Pfad(), Pro_Model_Name);
                }
            }
        }

        internal string Pro_Model_Info
        {
            get => Priv_Model_Info;
            set => Priv_Model_Info = value;
        }

        internal int Pro_Model_Gender
        {
            get => Priv_Model_Gender;
            set => Priv_Model_Gender = value;
        }

        internal string Pro_Model_Gender_Text
        {
            get
            {
                return Priv_Model_Gender switch
                {
                    -1 => TXT.TXT_Description("Empfehlung"),
                    0 => TXT.TXT_Description("Weiblich"),
                    1 => TXT.TXT_Description("Männlich"),
                    2 => TXT.TXT_Description("Paar"),
                    3 => TXT.TXT_Description("Trans"),
                    _ => TXT.TXT_Description("Sonstiges")
                };
            }
        }

        internal Image Pro_Model_Gender_Image
        {
            get
            {
                Image modelGenderImage;

                if (!string.IsNullOrEmpty(this.Pro_Model_Birthday))
                {
                    DateTime dateTime = ValueBack.Get_CDatum((object)this.Pro_Model_Birthday);
                    int day1 = dateTime.Day;
                    dateTime = DateTime.Today;
                    int day2 = dateTime.Day;

                    if (day1 == day2)
                    {
                        dateTime = ValueBack.Get_CDatum((object)this.Pro_Model_Birthday);
                        int month1 = dateTime.Month;
                        dateTime = DateTime.Today;
                        int month2 = dateTime.Month;

                        if (month1 == month2)
                        {
                            modelGenderImage = new Bitmap(Resources.Birthday, 16, 16);
                            return modelGenderImage;
                        }
                    }
                }

                switch (ValueBack.Get_CInteger((object)this.Priv_Model_Gender))
                {
                    case -1:
                        modelGenderImage = new Bitmap(Resources.Star16, 16, 16);
                        break;
                    case 0:
                        modelGenderImage = new Bitmap(Resources.Gender_Female, 16, 16);
                        break;
                    case 1:
                        modelGenderImage = new Bitmap(Resources.Gender_Male, 16, 16);
                        break;
                    case 2:
                        modelGenderImage = new Bitmap(Resources.Gender_Couple, 16, 16);
                        break;
                    case 3:
                        modelGenderImage = new Bitmap(Resources.Gender_Trans, 16, 16);
                        break;
                    default:
                        modelGenderImage = new Bitmap(Resources.Gender_Unknow, 16, 16);
                        break;
                }

                return modelGenderImage;
            }
        }

        internal string Pro_Model_Country
        {
            get => this.Priv_Model_Country;
            set => this.Priv_Model_Country = value;
        }

        internal string Pro_Model_Birthday
        {
            get => this.Priv_Model_Birthday;
            set => this.Priv_Model_Birthday = value;
        }

        internal string Pro_Model_Language
        {
            get => this.Priv_Model_Language;
            set => this.Priv_Model_Language = value;
        }

        internal bool Pro_Model_Record
        {
            get => this.Priv_Model_Record;
            set
            {
                this.Priv_Model_Record = value;

                if (this.Priv_Model_Record)
                {
                    if (this.Pro_Model_Stream_Record != null)
                        return;

                    this.Stream_Record_Start();

                    Class_Model.Model_Record_ChangeEventHandler recordChangeEvent = this.Model_Record_ChangeEvent;
                    if (recordChangeEvent == null)
                        return;

                    recordChangeEvent(value);
                }
                else
                {
                    if (this.Pro_Model_Stream_Record == null)
                        return;

                    this.Stream_Record_Stop();

                    Class_Model.Model_Record_ChangeEventHandler recordChangeEvent = this.Model_Record_ChangeEvent;
                    if (recordChangeEvent == null)
                        return;

                    recordChangeEvent(value);
                }
            }
        }

        internal bool Pro_Model_Favorite
        {
            get => this.Priv_Model_Favorite;
            set => this.Priv_Model_Favorite = value;
        }

        internal bool Get_Pro_Model_Online(bool Check = false)
        {
            if (this.Timer_Online_Change != null && Check)
                this.Priv_Model_Online = this.Timer_Online_Change.Get_Pro_Model_Online(Check);

            return this.Priv_Model_Online;
        }

        internal void Set_Pro_Model_Online(bool check, bool value)
        {
            this.Priv_Model_Online = value;
        }

        internal bool Pro_Model_Deaktiv
        {
            get => this.Priv_Model_Deaktiv;
            set => this.Priv_Model_Deaktiv = value;
        }

        internal DateTime Pro_Model_Galery_Last_Visit
        {
            get => this.Priv_Model_Galery_Last_Visit;
            set => this.Priv_Model_Galery_Last_Visit = value;
        }

        internal bool Pro_Model_Access_Denied
        {
            get => this.Priv_Model_Access_Denied;
            set
            {
                if (ValueBack.Get_CBoolean(value) == this.Priv_Model_Access_Denied)
                    return;

                this.Priv_Model_Access_Denied = ValueBack.Get_CBoolean(value);
                this.Set_Online_Check_Time();
            }
        }

        internal int Pro_Model_Token
        {
            get => this.Priv_Model_Token;
            set
            {
                this.Priv_Model_Token = value;

                Class_Model.Model_Token_ChangeEventHandler tokenChangeEvent = this.Model_Token_ChangeEvent;
                if (tokenChangeEvent != null)
                    tokenChangeEvent(this.Priv_Model_Token);

                if (this.Priv_Model_Token <= 0)
                    return;

                this.Model_Online_Changed();
            }
        }

        internal string Pro_Model_M3U8
        {
            get
            {
                if (!this.Priv_Model_Access_Denied &&
                    string.IsNullOrEmpty(this.Priv_Model_M3U8) &&
                    this.Get_Pro_Model_Online())
                {
                    this.Model_Stream_Adressen_Load();
                }

                return this.Priv_Model_M3U8;
            }

            set => this.Priv_Model_M3U8 = value;
        }

        internal string Pro_Model_Audio_Path
        {
            get => this.Priv_Model_Audio_Path;
            set => this.Priv_Model_Audio_Path = value;
        }

        internal string Pro_Model_AU_Path
        {
            get => this.Priv_Model_AU_Path;
            set => this.Priv_Model_AU_Path = value;
        }

        internal string Pro_Model_TS_Path
        {
            get => this.Priv_Model_TS_Path;
            set => this.Priv_Model_TS_Path = value;
        }

        internal string Pro_Model_FFMPEG_Path
        {
            get
            {
                if (!this.Priv_Model_Access_Denied &&
                    string.IsNullOrEmpty(this.Priv_Model_FFMPEG_Path?.ToString()) &&
                    this.Get_Pro_Model_Online())
                {
                    this.Model_Stream_Adressen_Load();
                }

                return this.Priv_Model_FFMPEG_Path!;
            }

            set => this.Priv_Model_FFMPEG_Path = value;
        }

        internal int Pro_Model_Record_Resolution
        {
            get => this.Priv_Model_Record_Resolution;
            set => this.Priv_Model_Record_Resolution = value;
        }

        internal string Pro_Model_Preview_Path
        {
            get => this.Priv_Model_Preview_Path;
            set => this.Priv_Model_Preview_Path = value;
        }

        internal int Pro_Model_Online_Day
        {
            get
            {
                if (this.Pro_Model_Gender == -1)
                    return -4;

                if (this.Pro_Model_Favorite && this.Pro_Model_Stream_Record == null)
                    return -3;

                if (this.Get_Pro_Model_Online())
                {
                    if (this.Pro_Model_Stream_Record != null)
                        return -2;
                    return -1;
                }

                var daysOffline = (DateTime.Today - this.Pro_Last_Online).Days;
                return daysOffline > 6 ? 7 : daysOffline;
            }
        }

        internal bool Pro_Model_Visible
        {
            get => this.Priv_Model_Visible;
            set => this.Priv_Model_Visible = value;
        }

        internal bool Pro_Model_Promo
        {
            get => this.Priv_Model_Promo;
            set => this.Priv_Model_Promo = value;
        }

        internal DateTime Pro_Last_Online
        {
            get => this.Priv_Model_Last_Online;
            set => this.Priv_Model_Last_Online = value;
        }

        internal Guid Pro_Model_Online_GUID
        {
            get => this.Priv_Model_Online_GUID;
            set => this.Priv_Model_Online_GUID = value;
        }

        internal int Pro_Aufnahme_Stop_Auswahl
        {
            get => ValueBack.Get_CInteger(Priv_Aufnahme_Stop_Auswahl);
            set => this.Priv_Aufnahme_Stop_Auswahl = value;
        }

        internal int Pro_Aufnahme_Stop_Minuten
        {
            get => ValueBack.Get_CInteger(Priv_Aufnahme_Stop_Minuten);
            set => this.Priv_Aufnahme_Stop_Minuten = value;
        }

        internal bool Pro_Aufnahme_Stop_Off
        {
            get => this.Priv_Aufnahme_Stop_Off;
            set => this.Priv_Aufnahme_Stop_Off = value;
        }

        internal int Pro_Aufnahme_Stop_Größe
        {
            get => ValueBack.Get_CInteger(Priv_Aufnahme_Stop_Größe);
            set => this.Priv_Aufnahme_Stop_Größe = value;
        }

        internal int Pro_Videoqualität
        {
            get => ValueBack.Get_CInteger(Priv_Videoqualität);
            set
            {
                if (value != this.Priv_Videoqualität)
                    this.Model_Stream_Adressen_Load();

                this.Priv_Videoqualität = value;
            }
        }

        internal int Pro_Decoder
        {
            get => ValueBack.Get_CInteger(Priv_Decoder);
            set => this.Priv_Decoder = value;
        }

        internal Class_Speicherformate Pro_SaveFormat
        {
            get => Speicherformate.Speicherformate_Find(ValueBack.Get_CInteger(Priv_SaveFormat));
        }

        internal bool Pro_Benachrichtigung
        {
            get => ValueBack.Get_CBoolean(Priv_Benachrichtigung);
            set => this.Priv_Benachrichtigung = value;
        }

        internal int Pro_Website_ID
        {
            get => ValueBack.Get_CInteger(Priv_Website_ID);
            set => this.Priv_Website_ID = value;
        }

        internal Bitmap Pro_Website_Image
        {
            get
            {
                if (Priv_Website_Image == null)
                {
                    Class_Website classWebsite = Sites.Website_Find(Pro_Website_ID);
                    if (classWebsite != null)
                        Priv_Website_Image = new Bitmap(classWebsite.Pro_Image, 16, 16);
                }

                return Priv_Website_Image!;
            }
        }

        internal Class_Stream_Record Pro_Model_Stream_Record { get; set; }
        public Timer Timer_Model_Thumbnail1 { get => Timer_Model_Thumbnail; set => Timer_Model_Thumbnail = value; }

        internal Class_Model(Guid Model_GUID)
        {
            this.Timer_Online_Change_Timer = 30000;
            this.Model_Offline_Count = 0;
            this.Timer_Model_Thumbnail1 = new Timer();
            this.Timer_Model_Online = new Timer();
            this.Priv_Model_Description = "";
            this.Priv_Model_Directory = "";
            this.Priv_Model_Deaktiv = false;
            this.Priv_Model_Access_Denied = false;
            this.Priv_Model_Token = 0;
            this.Priv_Model_M3U8 = "";
            this.Priv_Model_FFMPEG_Path = "";
            this.Priv_Aufnahme_Stop_Off = false;
            this.Priv_Videoqualität = 0;
            this.Record_Check_Timer = new System.Windows.Forms.Timer();
            this.LoadModel(Model_GUID);
        }

        private async void LoadModel(Guid Model_GUID)
        {
            try
            {
                using var oleDbConnection = new OleDbConnection
                {
                    ConnectionString = Database_Connect.Aktiv_Datenbank()
                };

                string query = $@"
            SELECT 
                DT_User.User_GUID, DT_User.User_Name, DT_User.User_Record, DT_User.User_Visible, 
                Max(DT_Online.Online_Ende) AS MaxvonOnline_Ende, DT_User.Aufnahmestop_Auswahl, 
                DT_User.Aufnahmestop_Minuten, DT_User.Aufnahmestop_Größe, DT_User.Videoqualität, 
                DT_User.Website_ID, DT_User.Benachrichtigung, User_Favorite, User_Birthday, 
                User_Language, User_Gender, User_Country, User_Memo, Recorder_ID, 
                User_Description, User_Directory, User_Deaktiv, SaveFormat, 
                User_Token, User_LastVisit
            FROM DT_User 
            LEFT JOIN DT_Online ON DT_User.User_GUID = DT_Online.Model_GUID 
            GROUP BY 
                DT_User.User_GUID, DT_User.User_Name, DT_User.User_Record, DT_User.User_Visible, 
                DT_User.Aufnahmestop_Auswahl, DT_User.Aufnahmestop_Minuten, DT_User.Aufnahmestop_Größe, 
                DT_User.Videoqualität, Website_ID, DT_User.Benachrichtigung, User_Favorite, 
                User_Birthday, User_Language, User_Gender, User_Country, User_Memo, Recorder_ID, 
                User_Description, User_Directory, User_Deaktiv, SaveFormat, User_Token, User_LastVisit 
            HAVING DT_User.User_GUID = '{Model_GUID}'";

                using var oleDbDataAdapter = new OleDbDataAdapter(query, oleDbConnection.ConnectionString);
                var dataSet = new DataSet();

                await oleDbConnection.OpenAsync().ConfigureAwait(false);
                if (oleDbConnection.State != ConnectionState.Open) return;

                oleDbDataAdapter.Fill(dataSet, "DT_Model");
                if (dataSet.Tables[0].Rows.Count != 1) return;

                DataRow row = dataSet.Tables[0].Rows[0];
                this.Pro_Model_GUID = ValueBack.Get_CUnique(row["User_GUID"]);
                this.Pro_Model_Name = row["User_Name"].ToString();
                this.Pro_Model_Visible = ValueBack.Get_CBoolean(row["User_Visible"]);
                this.Pro_Aufnahme_Stop_Auswahl = ValueBack.Get_CInteger(row["Aufnahmestop_Auswahl"]);
                this.Pro_Aufnahme_Stop_Minuten = ValueBack.Get_CInteger(row["Aufnahmestop_Minuten"]);
                this.Pro_Aufnahme_Stop_Größe = ValueBack.Get_CInteger(row["Aufnahmestop_Größe"]);
                this.Pro_Videoqualität = ValueBack.Get_CInteger(row["Videoqualität"]);
                this.Pro_Last_Online = ValueBack.Get_CDatum(row["MaxvonOnline_Ende"]);
                this.Pro_Benachrichtigung = ValueBack.Get_CBoolean(row["Benachrichtigung"]);
                this.Pro_Model_Favorite = ValueBack.Get_CBoolean(row["User_Favorite"]);
                this.Pro_Model_Info = row["User_Memo"].ToString();
                this.Pro_Model_Language = row["User_Language"].ToString();
                this.Pro_Model_Country = row["User_Country"].ToString();
                this.Pro_Model_Birthday = row["User_Birthday"].ToString();
                this.Pro_Model_Gender = string.IsNullOrEmpty(row["User_Gender"]?.ToString())
                    ? 4
                    : ValueBack.Get_CInteger(row["User_Gender"].ToString());
                this.Pro_Model_Token = ValueBack.Get_CInteger(row["User_Token"]);
                this.Pro_Website_ID = ValueBack.Get_CInteger(row["Website_ID"]);
                this.Pro_Decoder = ValueBack.Get_CInteger(row["Recorder_ID"]);
                this.Pro_Model_Description = row["User_Description"].ToString();
                this.Pro_Model_Directory = row["User_Directory"].ToString();
                this.Pro_Model_Deaktiv = ValueBack.Get_CBoolean(row["User_Deaktiv"]);

                var sitio = Sites.Website_Find(this.Pro_Website_ID);
                this.Priv_SaveFormat = sitio != null && sitio.Pro_Must_Convert
                    ? 1
                    : ValueBack.Get_CInteger(row["SaveFormat"]);

                object lastVisit = row["User_LastVisit"];
                this.Pro_Model_Galery_Last_Visit = lastVisit == DBNull.Value
                    ? DateTime.Now
                    : ValueBack.Get_CDatum(lastVisit);

                this.Record_load();
                this.Record_Files_Check();
                this.Pro_Model_Record = ValueBack.Get_CBoolean(row["User_Record"]);

                if (this.Priv_Website_Image == null && sitio != null)
                {
                    this.Priv_Website_Image = new Bitmap(sitio.Pro_Image, 16, 16);
                }

                this.Set_Online_Check_Time();
                this.Timer_Online_Change = new Class_Model_Online_Check(
                    this.Pro_Model_GUID,
                    this.Pro_Website_ID,
                    this.Pro_Model_Name,
                    this.Timer_Online_Change_Timer,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Online", "Check", "True"))
                );

                this.Timer_Model_Thumbnail1.Elapsed += (s, e) => this.Model_Thumbnail_save();

                this.Record_Check_Timer.Interval = 15000;
                this.Record_Check_Timer.Tick += (s, e) => this.Record_Check();
                this.Record_Check();

                if (string.IsNullOrEmpty(this.Priv_Model_M3U8) && this.Priv_Model_Online)
                {
                    this.Model_Stream_Adressen_Load();
                }

                if (this.Pro_Model_Record && this.Get_Pro_Model_Online() && !this.Record_Check_Timer.Enabled)
                {
                    this.Record_Check_Timer.Start();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Class_Model.New({Model_GUID})");
            }
        }

        internal Class_Model(string Model_Name, int Site_ID)
        {
            this.Timer_Online_Change_Timer = 30000;
            this.Model_Offline_Count = 0;
            this.Timer_Model_Thumbnail = new System.Timers.Timer();
            this.Timer_Model_Online = new System.Timers.Timer();
            this.Priv_Model_Description = "";
            this.Priv_Model_Directory = "";
            this.Priv_Model_Deaktiv = false;
            this.Priv_Model_Access_Denied = false;
            this.Priv_Model_Token = 0;
            this.Priv_Model_M3U8 = "";
            this.Priv_Model_FFMPEG_Path = "";
            this.Priv_Aufnahme_Stop_Off = false;
            this.Priv_Videoqualität = 0;
            this.Record_Check_Timer = new System.Windows.Forms.Timer();
            this.LoadModel(Model_Name, Site_ID);
        }

        private async void LoadModel(string Model_Name, int Site_ID)
        {
            await Task.CompletedTask;
            try
            {
                this.Pro_Model_GUID = Guid.NewGuid();
                this.Pro_Model_Name = Model_Name;
                this.Pro_Model_Visible = true;
                this.Pro_Website_ID = Site_ID;
                this.Pro_Last_Online = DateTime.Today;
                this.Pro_Benachrichtigung = true;
                this.Pro_Model_Gender = -1;
                this.Pro_Decoder = 0;
                this.Pro_Model_Description = string.Empty;
                this.Pro_Model_Deaktiv = false;
                this.Set_Pro_Model_Online(false, false);
                this.Pro_Model_Record = false;

                if (this.Priv_Website_Image == null)
                {
                    Class_Website classWebsite = Sites.Website_Find(this.Pro_Website_ID);
                    if (classWebsite != null)
                        this.Priv_Website_Image = new Bitmap(classWebsite.Pro_Image, 16, 16);
                }

                this.Set_Online_Check_Time();
                this.Timer_Online_Change = new Class_Model_Online_Check(
                    this.Pro_Model_GUID,
                    this.Pro_Website_ID,
                    this.Pro_Model_Name,
                    this.Timer_Online_Change_Timer,
                    ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Online", "Check", "True"))
                );

                if (string.IsNullOrEmpty(this.Priv_Model_M3U8?.ToString()) && this.Priv_Model_Online)
                    this.Model_Stream_Adressen_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Class_Model.New({Model_Name}) - Promo");
            }
        }

        internal async Task<string> Model_Stream_Adressen_Load_Back()
        {
            await Task.CompletedTask;
            try
            {
                StreamAdresses streamAdresses = new(this.Pro_Model_Name, this.Pro_Website_ID, this.Pro_Videoqualität);
                if (streamAdresses != null)
                {
                    this.Priv_Model_M3U8 = streamAdresses.Pro_M3U8_Path;
                    this.Priv_Model_TS_Path = streamAdresses.Pro_TS_Path;
                    this.Priv_Model_Audio_Path = streamAdresses.Pro_Audio_Path;
                    this.Priv_Model_FFMPEG_Path = streamAdresses.Pro_FFMPEG_Path;
                    this.Priv_Model_Preview_Path = streamAdresses.Pro_Preview_Image;
                    this.Priv_Model_AU_Path = streamAdresses.Pro_AU_Path;
                    this.Priv_Model_Access_Denied = ValueBack.Get_CBoolean((object)streamAdresses.Pro_Access_Denied);
                    this.Priv_Model_Record_Resolution = streamAdresses.Pro_Record_Resolution;
                }

                return await Parameter.URL_Response(streamAdresses.Pro_M3U8_Path)
                    ? null
                    : TXT.TXT_Description("Die Streamadressen konnten nicht abgefragt werden. Versuchen sie es später noch einmal.");
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Model_Stream_Adressen_Load");
                return null;
            }
        }

        internal async void Model_Stream_Adressen_Load()
        {
            await Task.CompletedTask;
            try
            {
                StreamAdresses streamAdresses = new(this.Pro_Model_Name, this.Pro_Website_ID, this.Pro_Videoqualität);
                if (streamAdresses != null)
                {
                    this.Priv_Model_M3U8 = streamAdresses.Pro_M3U8_Path;
                    this.Priv_Model_TS_Path = streamAdresses.Pro_TS_Path;
                    this.Priv_Model_Audio_Path = streamAdresses.Pro_Audio_Path;
                    this.Priv_Model_FFMPEG_Path = streamAdresses.Pro_FFMPEG_Path;
                    this.Priv_Model_Preview_Path = streamAdresses.Pro_Preview_Image;
                    this.Priv_Model_AU_Path = streamAdresses.Pro_AU_Path;
                    this.Priv_Model_Access_Denied = ValueBack.Get_CBoolean(streamAdresses.Pro_Access_Denied);
                    this.Priv_Model_Record_Resolution = streamAdresses.Pro_Record_Resolution;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Model_Stream_Adressen_Load");
            }
        }

        private async void Record_load()
        {
            await Task.CompletedTask;
            try
            {
                string selectCommandText = "SELECT * FROM DT_Record WHERE User_GUID = '" + this.Pro_Model_GUID.ToString() + "' AND (Record_Ende IS NULL)";
                using DataSet dataSet = new();
                using OleDbConnection oleDbConnection = new(Database_Connect.Aktiv_Datenbank());
                await oleDbConnection.OpenAsync();
                if (oleDbConnection.State != ConnectionState.Open)
                    return;

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommandText, oleDbConnection))
                using (OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter))
                {
                    adapter.Fill(dataSet, "DT_Record");
                    if (dataSet.Tables["DT_Record"].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataSet.Tables["DT_Record"].Rows)
                        {
                            bool recordEndeNull = row.IsNull("Record_Ende");
                            bool convertExtEmpty = row.IsNull("Record_Convert_Ext") || !row["Record_Name"].ToString().EndsWith(row["Record_Convert_Ext"]?.ToString() ?? "");

                            if (recordEndeNull || convertExtEmpty)
                            {
                                try
                                {
                                    var classStreamRecord = new Class_Stream_Record()
                                    {
                                        Pro_Record_GUID = ValueBack.Get_CUnique(row["Record_GUID"]),
                                        Record_GUID = ValueBack.Get_CUnique(row["Record_GUID"]),
                                        Pro_User_GUID = ValueBack.Get_CUnique(row["User_GUID"]),
                                        Pro_User_Name = this.Pro_Model_Name,
                                        Pro_Recordname = Path.Combine(this.Pro_Model_Directory, row["Record_Name"].ToString()),
                                        Pro_Record_Beginn = Convert.ToDateTime(row["Record_Beginn"]),
                                        Pro_Record_PID = ValueBack.Get_CInteger(row["Record_PID"]),
                                        ProzessID = ValueBack.Get_CInteger(row["Record_PID"]),
                                        Pro_Maschine = row["Maschine"].ToString(),
                                        Pro_Favorite = ValueBack.Get_CBoolean(row["Record_Favorit"]),
                                        Pro_Stream_Extension = row["Record_Convert_Ext"]?.ToString() ?? "",
                                        Pro_Decoder_item = Decoder.Decoder_Find(ValueBack.Get_CInteger(row["Record_Encoder_ID"])),
                                        Pro_Auflösung = this.Pro_Model_Record_Resolution
                                    };

                                    if (!Parameter.Task_Runs(Convert.ToInt32(row["Record_PID"])))
                                    {
                                        classStreamRecord.Stream_Record_Stop();
                                        classStreamRecord.Dispose();
                                    }
                                    else
                                    {
                                        this.Pro_Model_Stream_Record = classStreamRecord;
                                        if (this.Pro_Model_Record && this.Get_Pro_Model_Online() && !this.Record_Check_Timer.Enabled)
                                        {
                                            this.Record_Check_Timer.Start();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Parameter.Error_Message(ex, "Error processing active record");
                                }
                            }
                            else
                            {
                                try
                                {
                                    string path = Path.Combine(this.Pro_Model_Directory, row["Record_Name"].ToString());
                                    if (!File.Exists(path))
                                        row.Delete();
                                }
                                catch (Exception ex)
                                {
                                    Parameter.Error_Message(ex, "Error validating file existence");
                                }
                            }
                        }

                        try
                        {
                            adapter.Update(dataSet.Tables["DT_Record"]);
                        }
                        catch (Exception ex)
                        {
                            Parameter.Error_Message(ex, "Error updating DT_Record");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Record_load");
            }

            if (this.Pro_Model_Stream_Record != null)
                this.Model_Online_Changed();
        }

        private async void Record_Files_Check()
        {
            await Task.Run(() =>
            {
                try
                {
                    string selectCommandText = $"SELECT * FROM DT_Record WHERE User_GUID = '{this.Pro_Model_GUID}'";
                    using DataSet dataSet = new();
                    using OleDbConnection connection = new(Database_Connect.Aktiv_Datenbank());
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        return;

                    using (var adapter = new OleDbDataAdapter(selectCommandText, connection))
                    using (new OleDbCommandBuilder(adapter))
                    {
                        adapter.Fill(dataSet, "DT_Record");
                        connection.Close();

                        if (dataSet.Tables["DT_Record"].Rows.Count <= 0)
                            return;

                        foreach (DataRow row in dataSet.Tables["DT_Record"].Rows)
                        {
                            string recordName = row["Record_Name"].ToString();
                            string filePath = Path.Combine(this.Pro_Model_Directory, recordName);
                            if (!File.Exists(filePath))
                            {
                                try
                                {
                                    string vdbFile = filePath + ".vdb";
                                    if (File.Exists(vdbFile))
                                        File.Delete(vdbFile);

                                    row.Delete();
                                }
                                catch (Exception ex)
                                {
                                    Parameter.Error_Message(ex, "Class_Model.Record_File_Check.VDB Delete");
                                }
                            }
                        }

                        try
                        {
                            adapter.Update(dataSet.Tables["DT_Record"]);
                        }
                        catch (Exception ex)
                        {
                            Parameter.Error_Message(ex, "Class_Model.Record_File_Check.Update");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Class_Model.Record_File_Check");
                }
            });
        }

        internal async void Model_Online_Changed()
        {
            await Task.CompletedTask;
            try
            {
                using (var connection = new OleDbConnection(Database_Connect.Aktiv_Datenbank()))
                using (var adapter = new OleDbDataAdapter(
                    $"SELECT * FROM DT_User WHERE User_GUID = '{this.Pro_Model_GUID}'", connection))
                using (new OleDbCommandBuilder(adapter))
                using (var dataSet = new DataSet())
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        adapter.Fill(dataSet, "DT_Model");
                        connection.Close();

                        if (dataSet.Tables[0].Rows.Count == 1)
                        {
                            try
                            {
                                DataRow row = dataSet.Tables[0].Rows[0];
                                row["User_Name"] = this.Pro_Model_Name;
                                row["User_Record"] = this.Pro_Model_Record;
                                row["User_Visible"] = this.Pro_Model_Visible;
                                row["User_Favorite"] = this.Pro_Model_Favorite;
                                row["User_Memo"] = this.Pro_Model_Info;
                                row["User_Deaktiv"] = this.Pro_Model_Deaktiv;
                                row["User_Token"] = this.Pro_Model_Token;
                                row["User_LastVisit"] = this.Pro_Model_Galery_Last_Visit;

                                adapter.Update(dataSet.Tables[0]);
                            }
                            catch (Exception ex)
                            {
                                Parameter.Error_Message(ex, "Class_Model.Model_Online_Change.Update");
                            }
                        }
                    }
                }

                if (this.Pro_Model_Record && this.Get_Pro_Model_Online())
                {
                    if (!this.Record_Check_Timer.Enabled)
                        this.Record_Check_Timer.Start();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Model_Online_Change");
            }

            this.Model_Online_ChangeEvent?.Invoke(this);
        }

        private void Online_Change_Timer_Online_Status_Change()
        {
            try
            {
                if (this.Timer_Online_Change.Get_Pro_Model_Online())
                {
                    this.Online_Change_Timer_Tick(this.Timer_Online_Change.Get_Pro_Model_Online());
                }

                if (this.Timer_Online_Change.BGW_Result > 0 &&
                    !Parameter.URL_Response(this.Priv_Model_M3U8).Result)
                {
                    this.Model_Stream_Adressen_Load();
                }

                this.Set_Pro_Model_Online(false, this.Timer_Online_Change.Get_Pro_Model_Online());
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Online_Change_Timer_Online_Status_Change");
            }
        }

        internal async void Online_Change_Timer_Tick(bool newValue)
        {
            await Task.CompletedTask;

            try
            {
                if (newValue && (string.IsNullOrEmpty(this.Pro_Model_M3U8)))
                {
                    this.Model_Stream_Adressen_Load();
                }

                if (this.Timer_Online_Change.Get_Pro_Model_Online() &&
                    !Parameter.URL_Response(this.Pro_Model_M3U8).Result)
                {
                    this.Model_Stream_Adressen_Load();
                }

                if (newValue && this.Pro_Model_Record)
                {
                    this.Record_Check();
                }

                if (newValue && !this.Priv_Model_Online)
                {
                    this.Model_Go_Online();
                }
                else if (!newValue && this.Priv_Model_Online)
                {
                    this.Model_Go_Offline();
                }

                this.Set_Online_Check_Time();

                if (newValue && this.Get_Pro_Model_Online() &&
                    this.Pro_Model_Record &&
                    !Parameter.Task_Runs(ValueBack.Get_CInteger(this.Pro_Model_Stream_Record?.ProzessID)))
                {
                    this.Stream_Record_Start();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Online_Change_Timer_Tick");
            }
        }

        private void Set_Online_Check_Time()
        {
            try
            {
                Timer_Online_Change_Timer = 60000;

                if (!Pro_Model_Deaktiv)
                {
                    if (!Priv_Model_Access_Denied)
                    {
                        if (!Pro_Model_Record)
                        {
                            if (!Pro_Model_Visible)
                            {
                                int dia = Math.Clamp(Pro_Model_Online_Day, 0, 5);
                                Timer_Online_Change_Timer = (dia + 1) * 300000; // 5 minutos por unidad
                            }
                            else
                            {
                                Timer_Online_Change_Timer = !Get_Pro_Model_Online() ? 120000 : 60000;
                            }
                        }
                        else
                        {
                            Timer_Online_Change_Timer = !Get_Pro_Model_Online() ? 30000 : 60000;
                        }
                    }
                    else
                    {
                        Timer_Online_Change_Timer = 1800000; // 30 minutos
                    }
                }
                else
                {
                    Timer_Online_Change_Timer = 3600000; // 1 hora
                }

                if (Timer_Online_Change != null)
                {
                    Timer_Online_Change.Pro_Timer_Intervall = Timer_Online_Change_Timer;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Set_Online_Check_Time");
            }
        }

        private async void Model_Go_Online()
        {
            await Task.CompletedTask;
            try
            {
                // Establece el intervalo del temporizador de miniaturas
                this.Timer_Model_Thumbnail.Interval = this.Pro_Model_Deaktiv ? 14400000 : 600000; // 4 horas o 10 minutos
                this.Timer_Model_Thumbnail.Start();

                // Establece el temporizador para el estado online
                this.Timer_Model_Online.Interval = 60000; // 1 minuto
                this.Model_Online_Save();
                this.Timer_Model_Online.Start();

                // Cambia el estado online del modelo
                this.Set_Pro_Model_Online(false, true);

                if (this.Pro_Model_Online_GUID == Guid.Empty)
                    this.Pro_Model_Online_GUID = Guid.NewGuid();

                Class_Online.New_Row(this.Pro_Model_Online_GUID, this.Pro_Model_GUID);

                if (this.Pro_Benachrichtigung && string.Equals(IniFile.Read(Parameter.INI_Common, "Notification", "Enabled", "True"), "True", StringComparison.OrdinalIgnoreCase))
                {
                    var notificationEvent = this.Model_Show_NotificationEvent;
                    if (notificationEvent != null)
                    {
                        string titulo = TXT.TXT_Description("Online") + " " + this.Pro_Model_Name;
                        string mensaje = this.Pro_Model_Name + " " + TXT.TXT_Description("ist online") + "\r\n" + TXT.TXT_Description("Unterstütze dein Model!");
                        notificationEvent(titulo, mensaje, this.Thumbnail_Ico());
                    }
                }

                if (this.Pro_Model_Record && this.Pro_Model_Stream_Record == null)
                {
                    this.Stream_Record_Start();
                    if (!this.Record_Check_Timer.Enabled)
                        this.Record_Check_Timer.Start();
                }

                this.Model_Online_Changed();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Model_Go_Online");
            }
        }

        private async void Model_Go_Offline()
        {
            await Task.CompletedTask;
            try
            {
                this.Pro_Model_Online_GUID = Guid.Empty;
                this.Set_Pro_Model_Online(false, false);
                this.Pro_Model_M3U8 = null;
                this.Pro_Model_TS_Path = null;
                this.Pro_Model_Preview_Path = null;
                this.Pro_Model_AU_Path = null;

                this.Timer_Model_Thumbnail.Stop();
                this.Timer_Model_Online.Stop();

                this.Model_Online_Changed();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Model_Go_Offline");
            }
        }

        private void Model_Online_Save()
        {
            if (!this.Get_Pro_Model_Online())
                return;

            this.Pro_Last_Online = DateTime.Now;

            if (this.Pro_Model_Online_GUID == Guid.Empty)
                return;

            Class_Online.End_Write(this.Pro_Model_Online_GUID);
        }
        private async void Record_Check()
        {
            await Task.CompletedTask;
            try
            {
                if (this.Pro_Model_Stream_Record != null)
                {
                    if (Parameter.Task_Runs(this.Pro_Model_Stream_Record.ProzessID))
                    {
                        if (!this.Priv_Aufnahme_Stop_Off && this.Pro_Aufnahme_Stop_Auswahl > 0)
                        {
                            if (this.Pro_Aufnahme_Stop_Auswahl == 1 && this.Pro_Aufnahme_Stop_Minuten > 0)
                            {
                                double minutosGrabados = (DateTime.Now - this.Pro_Model_Stream_Record.Pro_Record_Beginn).TotalMinutes;
                                if (minutosGrabados < this.Pro_Aufnahme_Stop_Minuten ||
                                    !this.Stream_Record_Stop() ||
                                    this.Pro_Model_Stream_Record != null)
                                    return;

                                this.Stream_Record_Start();
                            }
                            else if (this.Pro_Aufnahme_Stop_Auswahl == 2 &&
                                     this.Pro_Aufnahme_Stop_Größe > 0 &&
                                     File.Exists(this.Pro_Model_Stream_Record.Pro_Recordname))
                            {
                                double tamañoMB = new FileInfo(this.Pro_Model_Stream_Record.Pro_Recordname).Length / 1048576.0;
                                if (tamañoMB <= this.Pro_Aufnahme_Stop_Größe ||
                                    !this.Stream_Record_Stop() ||
                                    this.Pro_Model_Stream_Record != null)
                                    return;

                                this.Stream_Record_Start();
                            }
                        }
                    }
                    else
                    {
                        this.Stream_Record_Stop();
                    }
                }
                else
                {
                    if (!this.Pro_Model_Record || !this.Get_Pro_Model_Online())
                        return;

                    this.Stream_Record_Start();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Timer_Record_Check_Tick");
            }
        }

        internal bool Stream_Record_Start()
        {
            try
            {
                bool isRunning = Parameter.Task_Runs(ValueBack.Get_CInteger(this.Pro_Model_Stream_Record?.ProzessID));
                bool hasM3u8 = !string.IsNullOrEmpty(this.Pro_Model_M3U8);
                if (!isRunning && hasM3u8 && !Parameter.Recording_Stop)
                {
                    this.Model_Stream_Adressen_Load();
                    this.Pro_Model_Stream_Record?.Dispose();
                    this.Pro_Model_Stream_Record = new Class_Stream_Record(this);

                    if (this.Pro_Model_Stream_Record.ProzessID > 0)
                    {
                        this.Pro_Aufnahme_Stop_Off = false;

                        if (Pro_Benachrichtigung &&string.Equals(IniFile.Read(Parameter.INI_Common, "Notification", "Enabled", "True"), "True", StringComparison.OrdinalIgnoreCase))
                        {
                            this.Model_Show_NotificationEvent?.Invoke(
                                $"{TXT.TXT_Description("Aufnahme")} {this.Pro_Model_Name}",
                                $"{TXT.TXT_Description("Aufnahme für")} {this.Pro_Model_Name} {TXT.TXT_Description("ist gestartet")}",
                                this.Thumbnail_Ico()
                            );
                        }

                        this.Model_Online_Changed();
                        return true;
                    }
                    else
                    {
                        this.Pro_Model_Stream_Record.Dispose();
                        this.Pro_Model_Stream_Record = null;
                        this.Model_Online_Changed();
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Stream_Record_Start");
                return false;
            }
        }

        private Image Thumbnail_Ico()
        {
            Image image = Resources.XstreaMon;
            string thumbnailPath = Path.Combine(this.Pro_Model_Directory, "Thumbnail.jpg");

            if (File.Exists(thumbnailPath))
            {
                try
                {
                    image = new Bitmap(thumbnailPath);
                }
                catch (Exception)
                {
                    // Silencia el error como en el original
                }
            }

            return image;
        }


        internal bool Stream_Record_Stop()
        {
            try
            {
                if (this.Pro_Model_Stream_Record != null)
                {
                    if (this.Pro_Model_Stream_Record.Stream_Record_Stop())
                    {
                        this.Pro_Model_Stream_Record.Dispose();
                        this.Pro_Model_Stream_Record = null!;
                        this.Model_Online_Changed();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Stream_Record_Stop");
                return false;
            }
        }

        internal async void Model_Delete(bool Dir_Delete = false)
        {
            await Task.CompletedTask;
            try
            {
                if (this.Pro_Model_Stream_Record != null)
                    this.Stream_Record_Stop();

                this.Timer_Model_Online.Stop();
                this.Timer_Model_Online = null;

                this.Timer_Model_Thumbnail.Stop();
                this.Timer_Model_Thumbnail = null;

                this.Timer_Online_Change = null;

                using (OleDbConnection oleDbConnection = new OleDbConnection())
                {
                    oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                    oleDbConnection.Open();

                    Class_Online.Online_Delete(ValueBack.Get_CUnique(this.Pro_Model_GUID));

                    if (oleDbConnection.State == ConnectionState.Open)
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM DT_Record where User_GUID = '" + this.Pro_Model_GUID.ToString() + "' AND Record_Ende is Null", oleDbConnection.ConnectionString))
                        using (new OleDbCommandBuilder(adapter))
                        using (DataSet dataSet = new DataSet())
                        {
                            adapter.Fill(dataSet, "DT_Record");
                            foreach (DataRow row in dataSet.Tables["DT_Record"].Rows)
                                row.Delete();
                            adapter.Update(dataSet.Tables["DT_Record"]);
                        }

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM DT_User where User_GUID = '" + this.Pro_Model_GUID.ToString() + "'", oleDbConnection.ConnectionString))
                        using (new OleDbCommandBuilder(adapter))
                        using (DataSet dataSet = new DataSet())
                        {
                            adapter.Fill(dataSet, "DT_User");
                            foreach (DataRow row in dataSet.Tables["DT_User"].Rows)
                                row.Delete();
                            adapter.Update(dataSet.Tables["DT_User"]);
                        }
                    }

                    oleDbConnection.Close();

                    if (string.Compare(this.Pro_Model_Directory, Modul_Ordner.Ordner_Pfad(), StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        if (Directory.Exists(this.Pro_Model_Directory))
                        {
                            try
                            {
                                if (!Dir_Delete)
                                {
                                    Dir_Delete = MessageBox.Show(
                                        string.Format(TXT.TXT_Description("Möchten sie den Ordner {0} löschen?"), this.Pro_Model_Directory),
                                        TXT.TXT_Description("Ordner löschen"),
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes;
                                }

                                if (Dir_Delete)
                                    Directory.Delete(this.Pro_Model_Directory, true);
                            }
                            catch (Exception) { }
                        }
                    }

                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Model_Delete");
            }
        }

        private async void Model_Thumbnail_save()
        {
            await Task.CompletedTask;
            try
            {
                this.Timer_Model_Thumbnail.Stop();

                if (!Directory.Exists(this.Pro_Model_Directory))
                    Directory.CreateDirectory(this.Pro_Model_Directory);

                Image result = await Sites.ImageFromWeb(this);
                if (result != null)
                {
                    using (Bitmap bitmap = new Bitmap(result))
                    {
                        try
                        {
                            string ruta = this.Pro_Model_Directory + "\\Thumbnail.jpg";
                            if (!Modul_Ordner.DateiInBenutzung(ruta))
                            {
                                bitmap.Save(ruta, ImageFormat.Jpeg);
                            }
                        }
                        catch (Exception ex)
                        {
                            Parameter.Error_Message(ex, "Class_Model.Model_Thumbnail_Save.User_Image_Save");
                        }
                    }
                }

                this.Timer_Model_Thumbnail.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Model_Thumbnail_Save");
                this.Timer_Model_Thumbnail.Start();
            }
        }

        internal void Dialog_Model_View_Show()
        {
            try
            {
                Dialog_ModellView dialogModellView = new Dialog_ModellView();
                dialogModellView.Text = this.Pro_Model_Name;
                dialogModellView.Pro_Model_Class = this;
                Dialog_ModellView mainForm = dialogModellView;

                Thread thread = new(() =>
                {
                    Application.Run(new ApplicationContext(mainForm));
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Class_Model.Dialog_Model_View_Show");
            }
        }

        internal delegate void Model_Online_ChangeEventHandler(Class_Model Model_Class);

        internal delegate void Model_Record_ChangeEventHandler(bool Record_Run);

        internal delegate void Model_Show_NotificationEventHandler(
            string Notification_Event,
            string Notification_Text,
            Image TooltipImage);

        internal delegate void Model_Token_ChangeEventHandler(int Token_Value);
    }
}
