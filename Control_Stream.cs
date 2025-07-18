using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace XstreaMonNET8
{
    public partial class Control_Stream : UserControl
    {
        internal string Record_Run_Maschine;
        internal bool Record_Allways;
        private bool _Record_Run;
        private int Refresh_Intervall;
        private int Refresh_Intervall_min;
        private int Refresh_Intervall_max;
        internal bool Streamrun;
        private int Pri_StreamOfflineCount;
        private bool Pri_StreamOnline;
        private Class_Model Pri_Model_Class;
        internal bool Priv_Show_Header;
        private Image Preview_image;

        // Delegates para los eventos
        internal delegate void StreamRecord_StartEventHandler(Control_Stream Control);
        internal delegate void StreamRecord_StopEventHandler(Control_Stream Control);
        internal delegate void Vorschau_CloseEventHandler(Control_Stream sender);
        internal delegate void Vorschau_FocusedEventHandler(Control_Stream sender);

        // Eventos
        internal event StreamRecord_StartEventHandler StreamRecord_Start;
        internal event StreamRecord_StopEventHandler StreamRecord_Stop;
        internal event Vorschau_CloseEventHandler Vorschau_Close;
        internal event Vorschau_FocusedEventHandler Vorschau_Focused;

        private Timer _RecordCheck;
        internal Timer RecordCheck
        {
            get => _RecordCheck;
            set
            {
                if (_RecordCheck != null)
                    _RecordCheck.Tick -= Record_Check;
                _RecordCheck = value;
                if (_RecordCheck != null)
                    _RecordCheck.Tick += Record_Check;
            }
        }

        private Timer _RefreshTimer;
        private Timer RefreshTimer
        {
            get => _RefreshTimer;
            set
            {
                if (_RefreshTimer != null)
                    _RefreshTimer.Tick -= RefreshTimerTick;
                _RefreshTimer = value;
                if (_RefreshTimer != null)
                    _RefreshTimer.Tick += RefreshTimerTick;
            }
        }

        private Timer _Header_Timer;
        private Timer Header_Timer
        {
            get => _Header_Timer;
            set
            {
                if (_Header_Timer != null)
                    _Header_Timer.Tick -= Header_Timer_Tick;
                _Header_Timer = value;
                if (_Header_Timer != null)
                    _Header_Timer.Tick += Header_Timer_Tick;
            }
        }

        private BackgroundWorker _Preview_Loader_Worker;
        private BackgroundWorker Preview_Loader_Worker
        {
            get => _Preview_Loader_Worker;
            set
            {
                if (_Preview_Loader_Worker != null)
                {
                    _Preview_Loader_Worker.DoWork -= Preview_Image_Load_DoWork;
                    _Preview_Loader_Worker.RunWorkerCompleted -= Preview_Image_Show_Completed;
                }
                _Preview_Loader_Worker = value;
                if (_Preview_Loader_Worker != null)
                {
                    _Preview_Loader_Worker.DoWork += Preview_Image_Load_DoWork;
                    _Preview_Loader_Worker.RunWorkerCompleted += Preview_Image_Show_Completed;
                }
            }
        }

        public Control_Stream()
        {
            InitializeComponent();

            RecordCheck = new Timer();
            RefreshTimer = new Timer();
            Refresh_Intervall = 3000;
            Refresh_Intervall_min = 30;
            Refresh_Intervall_max = 3000;
            Streamrun = false;
            Header_Timer = new Timer();
            Header_Timer.Interval = 500;
            Pri_StreamOfflineCount = 0;
            Pri_StreamOnline = false;
            Priv_Show_Header = false;
            Preview_Loader_Worker = new BackgroundWorker();

            ToolTip1.SetToolTip(BTN_Close, TXT.TXT_Description("Vorschau schliessen"));
            ToolTip1.SetToolTip(BTN_Modelview, TXT.TXT_Description("Galerie öffnen"));
            ToolTip1.SetToolTip(BTN_Record, TXT.TXT_Description("Aufnahme starten"));
            ToolTip1.SetToolTip(LAB_IP_Block, TXT.TXT_Description("Zugriff verweigert. Dieser Raum ist für deine Region nicht zugänglich."));

            Disposed += Control_Stream_Disposed;
            Load += Control_Stream_Load;
            ControlAdded += Control_Stream_ControlAdded;
            GotFocus += Control_Stream_GotFocus;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Header_Timer != null)
                    Header_Timer.Dispose();
                if (RefreshTimer != null)
                    RefreshTimer.Dispose();
                if (RecordCheck != null)
                    RecordCheck.Dispose();
                Preview_Loader_Worker?.Dispose();
                Pri_Model_Class = null;
                Preview_image?.Dispose();
                PAN_Control.BackgroundImage?.Dispose();

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        internal bool Record_Run
        {
            get => _Record_Run;
            set
            {
                _Record_Run = value;
                if (value)
                {
                    try
                    {
                        Record_Check(null, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in Record_Run setter: {ex.Message}");
                    }
                }
            }
        }

        internal Class_Model Pro_Model_Class
        {
            get => Pri_Model_Class;
            set
            {
                Pri_Model_Class = value;
                Model_Load();
            }
        }

        internal bool StreamOnline
        {
            get => Pri_StreamOnline;
            set
            {
                Pri_StreamOnline = value;
                LAB_Offline.Visible = StreamOfflineCount > 5;
                BTN_Record.Enabled = StreamOfflineCount < 5;
                TBT_Play.Enabled = StreamOfflineCount < 5;
            }
        }

        internal int StreamOfflineCount
        {
            get => Pri_StreamOfflineCount;
            set => Pri_StreamOfflineCount = value;
        }

        internal bool Show_Header
        {
            get => Priv_Show_Header;
            set
            {
                Priv_Show_Header = value;
                PAN_Header.Visible = value;
            }
        }

        private async void Model_Load()
        {
            try
            {
                string caption = $"{TXT.TXT_Description("Geschlecht")}: {TXT.TXT_Description(Pri_Model_Class.Pro_Model_Gender_Text.ToString())}\r\n";
                if (!string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Country))
                    caption += $"{TXT.TXT_Description("Ort")}: {Pri_Model_Class.Pro_Model_Country}\r\n";
                if (!string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Language))
                    caption += $"{TXT.TXT_Description("Sprachen")}: {Pri_Model_Class.Pro_Model_Language}\r\n";

                ToolTip1.SetToolTip(LAB_Bezeichnung, caption);
                LAB_Gender.Image = Pri_Model_Class.Pro_Model_Gender_Image;

                Class_Website website = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
                if (website != null)
                {
                    Refresh_Intervall_max = website.Pro_Intervall_Max;
                    Refresh_Intervall_min = website.Pro_Intervall_Min;
                    LAB_Website.Image = new Bitmap(website.Pro_Image, 16, 16);
                    TBT_Play.Visible = Refresh_Intervall_max != Refresh_Intervall_min;
                    ToolTip1.SetToolTip(LAB_Website, website.Pro_Name);
                }

                LAB_Bezeichnung.Text = string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Description) ?
                    Pri_Model_Class.Pro_Model_Name.ToString() :
                    Pri_Model_Class.Pro_Model_Description.ToString();

                if (Pri_Model_Class.Get_Pro_Model_Online())
                    PAN_Control.BackgroundImage = await Sites.ImageFromWeb(Pri_Model_Class);
                else if (File.Exists(Pri_Model_Class.Pro_Model_Directory + "\\Thumbnail.jpg"))
                    PAN_Control.BackgroundImage = new Bitmap(Pri_Model_Class.Pro_Model_Directory + "\\Thumbnail.jpg");

                Streamshow();
                RefreshTimer.Interval = Refresh_Intervall_max;
                RefreshTimerTick(null, EventArgs.Empty);
                RefreshTimer.Start();
                RecordCheck.Interval = 10000;
                RecordCheck.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Model_Load: {ex.Message}");
            }
        }

        private async void RefreshTimerTick(object sender, EventArgs e)
        {
            try
            {
                RefreshTimer.Stop();
                if (!Pri_Model_Class.Get_Pro_Model_Online())
                {
                    Dispose();
                }
                else
                {
                    if (ParentForm != null && ParentForm.WindowState != FormWindowState.Minimized)
                        Streamshow();
                    RefreshTimer?.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RefreshTimerTick: {ex.Message}");
            }
        }

        private void Control_Stream_Disposed(object sender, EventArgs e)
        {
            try
            {
                Header_Timer?.Stop();
                RefreshTimer?.Stop();
                RecordCheck?.Stop();
                Preview_Loader_Worker?.Dispose();
                Pri_Model_Class = null;
                PAN_Control.BackgroundImage = null;
                Parameter.FlushMemory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Control_Stream_Disposed: {ex.Message}");
            }
        }

        private async void Record_Check(object sender, EventArgs e)
        {
            try
            {
                RecordCheck.Stop();
                if (Pri_Model_Class == null) return;

                bool isRecording = Record_Run &&
                    Pri_Model_Class.Pro_Model_Stream_Record != null &&
                    Parameter.Task_Runs(Pri_Model_Class.Pro_Model_Stream_Record.ProzessID/*.Value*/);

                BTN_Record.Visible = !string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_M3U8) || isRecording;

                if (!string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_M3U8))
                {
                    LAB_Resolution.Image = Pri_Model_Class.Pro_Model_Record_Resolution switch
                    {
                        1 or 2 => Resources.ResSD,
                        3 => Resources.ResHD,
                        4 => Resources.ResFHD,
                        5 => Resources.Res4K,
                        _ => null
                    };
                }

                TBT_Favorite.Visible = isRecording;
                TBT_Favorite.Image = Value_Back.get_CBoolean(Pri_Model_Class.Pro_Model_Stream_Record?.Pro_Favorite) ?
                    Resources.Favorite16 :
                    Resources.FavoriteDeaktiv16;

                PAN_Header.BackColor = isRecording ?
                    Color.FromArgb(100, Color.IndianRed) :
                    Color.FromArgb(100, BackColor);

                ToolTip1.SetToolTip(BTN_Record, TXT.TXT_Description(isRecording ? "Aufnahme beenden" : "Aufnahme starten"));
                BTN_Record.Image = isRecording ?
                    Resources.RecordAutomatikStop16 :
                    Resources.RecordAutomatic16;

                LAB_Aufnahmezeit.Text = isRecording ?
                    $"{DateAndTime.DateDiff(DateInterval.Minute, Pri_Model_Class.Pro_Model_Stream_Record.Pro_Record_Beginn, DateTime.Now)} min" :
                    "";

                Streamshow();
                LAB_IP_Block.Visible = Pri_Model_Class.Pro_Model_Access_Denied/*.Value*/;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Record_Check: {ex.Message}");
            }
            finally
            {
                RecordCheck?.Start();
            }
        }

        private void Preview_Image_Load_DoWork(object sender, DoWorkEventArgs e)
        {
            Preview_Image_Load();
        }

        private void Preview_Image_Show_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Preview_Image_Show();
        }

        internal async void Streamshow()
        {
            try
            {
                RefreshTimer.Stop();
                if (ParentForm == null || ParentForm.WindowState == FormWindowState.Minimized || Preview_Loader_Worker.IsBusy)
                    return;
                await Task.Run(() => Preview_Loader_Worker.RunWorkerAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Streamshow: {ex.Message}");
                StreamOnline = false;
                StreamOfflineCount++;
            }
        }

        private void Preview_Image_Load()
        {
            Preview_image = Class_Model_Preview_Image_Load.Preview_Image(Pri_Model_Class, PAN_Control.Size).Result;
        }

        private async void Preview_Image_Show()
        {
            try
            {
                if (Preview_image == null)
                    Preview_Image_Load();

                if (Preview_image != null)
                {
                    PAN_Control.BackgroundImage = null;
                    PAN_Control.BackgroundImage = Preview_image;
                    StreamOnline = true;
                    StreamOfflineCount = 0;
                }
                else
                {
                    StreamOnline = false;
                    StreamOfflineCount++;
                }

                RefreshTimer?.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Preview_Image_Show: {ex.Message}");
            }
        }

        private void PAN_Control_BackColorChanged(object sender, EventArgs e)
        {
            PAN_Control.BackColor = Color.Transparent;
        }

        private void PAN_Control_DoubleClick(object sender, EventArgs e)
        {
            Sites.WebSiteOpen(Pri_Model_Class.Pro_Website_ID, Pri_Model_Class.Pro_Model_Name);
        }

        private void BTN_Record_Click(object sender, EventArgs e) => Aufnahme();

        private async void Aufnahme()
        {
            try
            {
                Pri_Model_Class.Pro_Model_Record = !Record_Run;
                Record_Run = Pri_Model_Class.Pro_Model_Stream_Record != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Aufnahme: {ex.Message}");
            }
        }

        private bool _TBT_Play_IsChecked = false;
        private void TBT_Play_Click(object sender, EventArgs e)
        {
            try
            {
                _TBT_Play_IsChecked = !_TBT_Play_IsChecked;

                if (_TBT_Play_IsChecked)
                {
                    Refresh_Intervall = Refresh_Intervall_min;
                    RefreshTimer.Stop();
                    RefreshTimer.Interval = Refresh_Intervall;
                    TBT_Play.Image = Resources.control_stop_right_icon;
                }
                else
                {
                    Refresh_Intervall = Refresh_Intervall_max;
                    RefreshTimer.Stop();
                    RefreshTimer.Interval = Refresh_Intervall;
                    TBT_Play.Image = Resources.control_right_icon;
                }
                RefreshTimer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TBT_Play_Click: {ex.Message}");
            }
        }

        private void BTN_Modelview_Click(object sender, EventArgs e) => Galerie_Open();

        private async void Galerie_Open()
        {
            try
            {
                await Task.Run(() => Pri_Model_Class.Dialog_Model_View_Show());
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Galerie_Open: {ex.Message}");
            }
        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            Visible = false;
            Vorschau_Close?.Invoke(this);
        }

        private void TBT_Favorite_Click(object sender, EventArgs e)
        {
            try
            {
                Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite =
                    !Value_Back.get_CBoolean(Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite);

                TBT_Favorite.Image = Value_Back.get_CBoolean(Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite) ?
                    Resources.Favorite16 :
                    Resources.FavoriteDeaktiv16;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TBT_Favorite_Click: {ex.Message}");
            }
        }

        private void Favoriten_Record()
        {
            try
            {
                Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite =
                    !Value_Back.get_CBoolean(Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite);

                TBT_Favorite.Image = Value_Back.get_CBoolean(Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite) ?
                    Resources.Favorite16 :
                    Resources.FavoriteDeaktiv16;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Favoriten_Record: {ex.Message}");
            }
        }

        private void Favoriten_Model()
        {
            try
            {
                Pri_Model_Class.Pro_Model_Favorite = !Pri_Model_Class.Pro_Model_Favorite;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Favoriten_Model: {ex.Message}");
            }
        }

        private void Header_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (ClientRectangle.Contains(PointToClient(Cursor.Position)))
                    return;

                if (!Show_Header)
                    PAN_Header.Visible = false;

                if (!_TBT_Play_IsChecked)
                {
                    Refresh_Intervall = Refresh_Intervall_max;
                    RefreshTimer.Stop();
                    RefreshTimer.Interval = Refresh_Intervall;
                    RefreshTimer.Start();
                }

                Header_Timer.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Header_Timer_Tick: {ex.Message}");
            }
        }

        private void PAN_Control_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (!Show_Header)
                    PAN_Header.Visible = true;

                Header_Timer.Start();

                if (!_TBT_Play_IsChecked)
                {
                    Refresh_Intervall = Refresh_Intervall_min;
                    RefreshTimer.Stop();
                    RefreshTimer.Interval = Refresh_Intervall;
                    RefreshTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PAN_Control_MouseEnter: {ex.Message}");
            }
        }

        private void PAN_Header_VisibleChanged(object sender, EventArgs e)
        {
            PAN_Header.BackColor = Record_Run ?
                Color.FromArgb(100, Color.IndianRed) :
                Color.FromArgb(100, BackColor);
        }

        private void COM_Preview_DropDownOpening(object sender, CancelEventArgs e)
        {
            try
            {
                CMI_Model_Promo.Text = TXT.TXT_Description("in die Modelliste aufnehmen");
                CMI_Galerie.Text = TXT.TXT_Description("Galerie");
                CMI_Optionen.Text = TXT.TXT_Description("Optionen");
                CMI_Info.Text = TXT.TXT_Description("Info bearbeiten");
                CMI_Webseite_Kopieren.Text = TXT.TXT_Description("URL kopieren");
                CMI_Stop_Off.Text = TXT.TXT_Description("Automatisches Aufnahmestop ausschalten");
                CMI_CamBrowser.Text = TXT.TXT_Description("Webseite öffnen mit CamBrowser");
                CMI_StreamRefresh.Text = TXT.TXT_Description("Streamadressen aktualisieren");
                CMI_Fade_In.Text = TXT.TXT_Description("Einblenden");

                CMI_Model_Promo.Visible = Pri_Model_Class.Pro_Model_Promo;
                CMI_Galerie.Enabled = !Pri_Model_Class.Pro_Model_Promo;
                CMI_Fade_In.Enabled = !Pri_Model_Class.Pro_Model_Promo;
                CMI_Optionen.Enabled = !Pri_Model_Class.Pro_Model_Promo;
                CMI_Info.Enabled = !Pri_Model_Class.Pro_Model_Promo;
                CMI_Stop_Off.Enabled = !Pri_Model_Class.Pro_Model_Promo;

                if (Value_Back.get_CInteger(INI_File.Read(Parameter.INI_Common, "Browser", "Product", "0")) == -1)
                    CMI_CamBrowser.Visible = false;
                else
                    CMI_CamBrowser.Visible = true;

                if (Parameter.URL_Response(Pri_Model_Class.Pro_Model_M3U8).Result)
                    CMI_StreamRefresh.Visible = false;
                else
                    CMI_StreamRefresh.Visible = true;

                CMI_Fade_In.Visible = !Pri_Model_Class.Pro_Model_Visible;

                if (!Pri_Model_Class.Pro_Model_Record)
                {
                    CMI_Aufnahme.Image = Resources.RecordAutomatic16;
                    CMI_Aufnahme.Text = TXT.TXT_Description("Automatische Aufnahme starten");
                    CMI_Aufnahme.Enabled = Parameter.URL_Response(Pri_Model_Class.Pro_Model_M3U8).Result;
                    CMI_Favoriten_Record.Visible = false;
                    CMI_Stop_Off.Visible = false;
                    CMI_Stop_Off.Checked = false;
                }
                else
                {
                    CMI_Aufnahme.Image = Resources.RecordAutomatikStop16;
                    CMI_Aufnahme.Text = TXT.TXT_Description("Keine Aufnahme");
                    CMI_Stop_Off.Visible = Pri_Model_Class.Pro_Aufnahme_Stop_Auswahl > 0;
                    CMI_Stop_Off.Checked = Pri_Model_Class.Pro_Aufnahme_Stop_Off;
                    CMI_Favoriten_Record.Text = Value_Back.get_CBoolean(Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite) ?
                        TXT.TXT_Description("Aufnahme aus Favoriten löschen") :
                        TXT.TXT_Description("Aufnahme zu Favoriten hinzufügen");
                }

                CMI_Favoriten_Model.Image = Pri_Model_Class.Pro_Model_Favorite ?
                    Resources.Favorite16 :
                    Resources.FavoriteDeaktiv16;
                CMI_Favoriten_Model.Text = Pri_Model_Class.Pro_Model_Favorite ?
                    TXT.TXT_Description("Model aus Favoriten löschen") :
                    TXT.TXT_Description("Model zu Favoriten hinzufügen");

                Class_Website website = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
                if (website != null)
                {
                    CMI_Webseite.Text = $"{website.Pro_Name} {TXT.TXT_Description("Webseite öffnen")}";
                    CMI_Webseite.Image = new Bitmap(website.Pro_Image, 16, 16);
                    CMI_Webseite.Tag = website.Pro_ID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in COM_Preview_DropDownOpening: {ex.Message}");
            }
        }

        private void CMI_Fade_In_Click(object sender, EventArgs e)
        {
            Pri_Model_Class.Pro_Model_Visible = true;
        }

        private void CMI_Optionen_Click(object sender, EventArgs e)
        {
            using Dialog_Model_Einstellungen dialog = new Dialog_Model_Einstellungen(Pri_Model_Class.Pro_Model_GUID);
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.ShowDialog();
        }

        private void CMI_Webseite_Click(object sender, EventArgs e)
        {
            Sites.WebSiteOpen(Pri_Model_Class.Pro_Website_ID, Pri_Model_Class.Pro_Model_Name);
        }

        private void CMI_CamBrowser_Click(object sender, EventArgs e)
        {
            Class_Website website = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
            if (website != null)
                Sites.CamBrowser_Open(website.Pro_URL + Pri_Model_Class.Pro_Model_Name);
        }

        private void CMI_Galerie_Click(object sender, EventArgs e) => Galerie_Open();

        private void CMI_Aufnahme_Click(object sender, EventArgs e) => Aufnahme();

        private void CMI_Favoriten_Click(object sender, EventArgs e) => Favoriten_Model();

        private void CMI_Info_Click(object sender, EventArgs e)
        {
            using (Dialog_Model_Info dialog = new Dialog_Model_Info())
            {
                dialog.TXB_Memo.Text = Pri_Model_Class.Pro_Model_Info.ToString();
                dialog.StartPosition = FormStartPosition.CenterParent;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Pri_Model_Class.Pro_Model_Info = dialog.TXB_Memo.Text;
                    Pri_Model_Class.Model_Online_Changed();
                }
            }
        }

        private void CMI_Webseite_Kopieren_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                Class_Website website = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
                if (website != null)
                    url = string.Format(website.Pro_Model_URL, Pri_Model_Class.Pro_Model_Name);

                if (url.Length > 0)
                    Clipboard.SetText(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CMI_Webseite_Kopieren_Click: {ex.Message}");
            }
        }

        private void CMI_Favoriten_Record_Click(object sender, EventArgs e) => Favoriten_Record();

        private void CMI_Stop_Off_Click(object sender, EventArgs e)
        {
            Pri_Model_Class.Pro_Aufnahme_Stop_Off = !Pri_Model_Class.Pro_Aufnahme_Stop_Off;
            CMI_Stop_Off.Checked = Pri_Model_Class.Pro_Aufnahme_Stop_Off;
        }

        private async void CMI_StreamRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string result = await Pri_Model_Class.Model_Stream_Adressen_Load_Back();
                if (result != null)
                    MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CMI_StreamRefresh_Click: {ex.Message}");
            }
        }

        private void Control_Stream_Load(object sender, EventArgs e) => Streamshow();

        private void Control_Stream_ControlAdded(object sender, ControlEventArgs e) { }

        private void Control_Stream_GotFocus(object sender, EventArgs e) { }

        private void PAN_Control_Click(object sender, EventArgs e)
        {
            Vorschau_Focused?.Invoke(this);
        }

        // Clases internas auxiliares (simuladas)
        internal static class TXT
        {
            internal static string TXT_Description(string key) => key;
        }

        internal static class Value_Back
        {
            internal static bool get_CBoolean(object value) => Convert.ToBoolean(value);
            internal static string get_CDatum(object value) => Convert.ToString(value);
            internal static int get_CInteger(object value) => Convert.ToInt32(value);
        }

        internal static class Sites
        {
            internal static Class_Website Website_Find(object id) => new Class_Website();
            internal static void WebSiteOpen(object id, object name) { }
            internal static void CamBrowser_Open(string url) { }
            internal static Task<Image> ImageFromWeb(Class_Model model) => Task.FromResult<Image>(null);
        }

        internal class Class_Website
        {
            internal object Pro_ID { get; set; }
            internal string Pro_Name { get; set; }
            internal Image Pro_Image { get; set; }
            internal string Pro_URL { get; set; }
            internal string Pro_Model_URL { get; set; }
            internal int Pro_Intervall_Max { get; set; }
            internal int Pro_Intervall_Min { get; set; }
        }

        internal static class Parameter
        {
            internal static string INI_Common { get; } = "Common.ini";
            internal static Task<bool> URL_Response(string url) => Task.FromResult(true);
            internal static bool Task_Runs(int processId) => false;
            internal static void FlushMemory() { }
        }

        internal static class INI_File
        {
            internal static string Read(string path, string section, string key, string defaultValue) => defaultValue;
        }

        internal static class DateAndTime
        {
            internal static long DateDiff(DateInterval interval, DateTime date1, DateTime date2)
            {
                return (long)(date2 - date1).TotalMinutes;
            }
        }

        internal enum DateInterval { Minute }

        internal class Dialog_Model_Einstellungen : Form
        {
            public Dialog_Model_Einstellungen(object guid) { }
        }

        internal class Dialog_Model_Info : Form
        {
            public TextBox TXB_Memo { get; set; } = new TextBox();
        }
    }
}
