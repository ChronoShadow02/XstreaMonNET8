using Microsoft.VisualBasic;
using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace XstreaMonNET8
{
    public partial class Control_Stream : UserControl
    {
        // Internal fields (variables)
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

        // Timers
        private Timer _RecordCheck;
        private Timer _RefreshTimer;
        private Timer _Header_Timer;

        // BackgroundWorker for image loading
        private BackgroundWorker _Preview_Loader_Worker;

        // Properties
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
                        Parameter.Error_Message(ex, "Control_Stream.Record_Run - " + value.ToString());
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

        // Constructor
        public Control_Stream()
        {
            InitializeComponent();

            // Initialize timers
            _RecordCheck = new Timer();
            _RefreshTimer = new Timer();
            _Header_Timer = new Timer();

            // Initialize other fields
            Refresh_Intervall = 3000;
            Refresh_Intervall_min = 30;
            Refresh_Intervall_max = 3000;
            Streamrun = false;
            Pri_StreamOfflineCount = 0;
            Pri_StreamOnline = false;
            Priv_Show_Header = false;
            Preview_image = null;

            // Initialize BackgroundWorker
            _Preview_Loader_Worker = new BackgroundWorker();
            _Preview_Loader_Worker.DoWork += (sender, e) => Preview_Image_Load();
            _Preview_Loader_Worker.RunWorkerCompleted += (sender, e) => Preview_Image_Show();

            // Event subscriptions
            this.Disposed += Control_Stream_Disposed;
            this.Load += Control_Stream_Load;
            this.ControlAdded += Control_Stream_ControlAdded;
            this.GotFocus += Control_Stream_GotFocus;

            // ToolTip settings
            ToolTip1.SetToolTip(BTN_Close, TXT.TXT_Description("Vorschau schliessen"));
            ToolTip1.SetToolTip(BTN_Modelview, TXT.TXT_Description("Galerie öffnen"));
            ToolTip1.SetToolTip(BTN_Record, TXT.TXT_Description("Aufnahme starten"));
            ToolTip1.SetToolTip(LAB_IP_Block, TXT.TXT_Description("Zugriff verweigert. Dieser Raum ist für deine Region nicht zugänglich."));

            _Header_Timer.Interval = 500;

            // Timer event subscriptions
            _RecordCheck.Tick += Record_Check;
            _RefreshTimer.Tick += RefreshTimerTick;
            _Header_Timer.Tick += Header_Timer_Tick;

            // Control event subscriptions
            PAN_Header.VisibleChanged += PAN_Header_VisibleChanged!;
            BTN_Record.Click += BTN_Record_Click!;
            TBT_Play.Click += TBT_Play_Click; // Changed from ToggleStateChanged to Click for native ToggleButton behavior
            BTN_Modelview.Click += BTN_Modelview_Click;
            BTN_Close.Click += BTN_Close_Click;
            TBT_Favorite.Click += TBT_Favorite_Click; // Changed from ToggleStateChanged to Click for native ToggleButton behavior
            PAN_Control.BackColorChanged += PAN_Control_BackColorChanged;
            PAN_Control.DoubleClick += PAN_Control_DoubleClick;
            PAN_Control.MouseEnter += PAN_Control_MouseEnter;
            PAN_Control.Click += PAN_Control_Click;

            // Context Menu Item event subscriptions
            COM_Preview.Opening += COM_Preview_DropDownOpening; // Changed from DropDownOpening to Opening for native ContextMenuStrip
            CMI_Aufnahme.Click += CMI_Aufnahme_Click;
            CMI_Favoriten_Model.Click += CMI_Favoriten_Click;
            CMI_Info.Click += CMI_Info_Click;
            CMI_Webseite.Click += CMI_Webseite_Click;
            CMI_Galerie.Click += CMI_Galerie_Click;
            CMI_Optionen.Click += CMI_Optionen_Click;
            CMI_Webseite_Kopieren.Click += CMI_Webseite_Kopieren_Click;
            CMI_Favoriten_Record.Click += CMI_Favoriten_Record_Click;
            CMI_Stop_Off.Click += CMI_Stop_Off_Click;
            CMI_CamBrowser.Click += CMI_CamBrowser_Click;
            CMI_StreamRefresh.Click += CMI_StreamRefresh_Click;
            CMI_Fade_In.Click += CMI_Fade_In_Click;
        }

        // Event Handlers
        private async void Model_Load()
        {
            try
            {
                await Task.CompletedTask; // Simulate async operation

                string caption = $"{TXT.TXT_Description("Geschlecht")}: {TXT.TXT_Description(Pri_Model_Class.Pro_Model_Gender_Text.ToString())}\r\n";
                if (!string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Country))
                    caption += $"{TXT.TXT_Description("Ort")}: {Pri_Model_Class.Pro_Model_Country}\r\n";
                if (!string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Language))
                    caption += $"{TXT.TXT_Description("Sprachen")}: {Pri_Model_Class.Pro_Model_Language}\r\n";
                if (!string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Birthday))
                    caption += $"{TXT.TXT_Description("Geburtstag")}: {ValueBack.Get_CDatum(Pri_Model_Class.Pro_Model_Birthday)}\r\n";
                if (!string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Info))
                    caption += Pri_Model_Class.Pro_Model_Info;

                ToolTip1.SetToolTip(LAB_Bezeichnung, caption);
                LAB_Gender.Image = Pri_Model_Class.Pro_Model_Gender_Image;

                Class_Website classWebsite = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
                if (classWebsite != null)
                {
                    Refresh_Intervall_max = classWebsite.Pro_Intervall_Max;
                    Refresh_Intervall_min = classWebsite.Pro_Intervall_Min;
                    LAB_Website.Image = new Bitmap(classWebsite.Pro_Image, 16, 16);
                    TBT_Play.Visible = Refresh_Intervall_max != Refresh_Intervall_min;
                    ToolTip1.SetToolTip(LAB_Website, classWebsite.Pro_Name);
                }

                LAB_Bezeichnung.Text = string.IsNullOrEmpty(Pri_Model_Class.Pro_Model_Description) ? Pri_Model_Class.Pro_Model_Name : Pri_Model_Class.Pro_Model_Description;

                if (Pri_Model_Class.Get_Pro_Model_Online())
                    PAN_Control.BackgroundImage = await Sites.ImageFromWeb(Pri_Model_Class);
                else if (File.Exists(Pri_Model_Class.Pro_Model_Directory + "\\Thumbnail.jpg"))
                    PAN_Control.BackgroundImage = (Image)new Bitmap(Pri_Model_Class.Pro_Model_Directory + "\\Thumbnail.jpg").Clone();

                Streamshow();
                _RefreshTimer.Interval = Refresh_Intervall_max;
                RefreshTimerTick(null, EventArgs.Empty);
                _RefreshTimer.Start();
                _RecordCheck.Interval = 10000;
                _RecordCheck.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.Model_Load");
            }
        }

        private async void RefreshTimerTick(object Sender, EventArgs e)
        {
            try
            {
                _RefreshTimer.Stop();
                if (!Pri_Model_Class.Get_Pro_Model_Online())
                {
                    Dispose();
                }
                else
                {
                    if (ParentForm != null && ParentForm.WindowState != FormWindowState.Minimized)
                        Streamshow();
                    if (_RefreshTimer != null)
                        _RefreshTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.RefreshTimerTick");
            }
        }

        private void Control_Stream_Disposed(object sender, EventArgs e)
        {
            try
            {
                _Header_Timer?.Stop();
                _Header_Timer?.Dispose();
                _Header_Timer = null;

                _RefreshTimer?.Stop();
                _RefreshTimer?.Dispose();
                _RefreshTimer = null;

                _RecordCheck?.Stop();
                _RecordCheck?.Dispose();
                _RecordCheck = null;

                _Preview_Loader_Worker?.Dispose();
                _Preview_Loader_Worker = null;

                Pri_Model_Class = null;
                PAN_Control.BackgroundImage?.Dispose();
                PAN_Control.BackgroundImage = null;

                // Assuming Control_Clear_Dispose.Clear is a utility method
                // If not, you might need to manually dispose child controls if they are not automatically handled by the UserControl's Dispose
                // Control_Clear_Dispose.Clear(this.Controls); 

                Dispose(true); // Call base Dispose
                Parameter.FlushMemory();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.Control_Stream_Disposed");
            }
        }

        internal async void Record_Check(object sender, EventArgs e)
        {
            try
            {
                _RecordCheck.Stop();
                if (Pri_Model_Class == null)
                    return;

                bool flag = false;
                if (Record_Run && Pri_Model_Class?.Pro_Model_Stream_Record != null && Parameter.Task_Runs(Pri_Model_Class.Pro_Model_Stream_Record.ProzessID/*.Value*/))
                    flag = true;

                BTN_Record.Visible = !string.IsNullOrEmpty(Pri_Model_Class?.Pro_Model_M3U8) || flag;

                if (!string.IsNullOrEmpty(Pri_Model_Class?.Pro_Model_M3U8))
                {
                    int? recordResolution = Pri_Model_Class?.Pro_Model_Record_Resolution;
                    if (recordResolution.HasValue)
                    {
                        switch (recordResolution.Value)
                        {
                            case 1:
                            case 2:
                                LAB_Resolution.Image = Resources.ResSD;
                                break;
                            case 3:
                                LAB_Resolution.Image = Resources.ResHD;
                                break;
                            case 4:
                                LAB_Resolution.Image = Resources.ResFHD;
                                break;
                            case 5:
                                LAB_Resolution.Image = Resources.Res4K;
                                break;
                            default:
                                LAB_Resolution.Image = null;
                                break;
                        }
                    }
                    else
                    {
                        LAB_Resolution.Image = null;
                    }
                }

                TBT_Favorite.Visible = flag;
                // For native CheckBox/ToggleButton, use Checked property
                TBT_Favorite.Checked = ValueBack.Get_CBoolean(Pri_Model_Class?.Pro_Model_Stream_Record?.Pro_Favorite);

                PAN_Header.BackColor = flag ? Color.FromArgb(100, Color.IndianRed) : Color.FromArgb(100, BackColor);
                ToolTip1.SetToolTip(BTN_Record, TXT.TXT_Description(flag ? "Aufnahme beenden" : "Aufnahme starten"));
                BTN_Record.Image = flag ? Resources.RecordAutomatikStop16 : Resources.RecordAutomatic16;
                LAB_Aufnahmezeit.Text = flag ? $"{DateAndTime.DateDiff(DateInterval.Minute, Pri_Model_Class.Pro_Model_Stream_Record.Pro_Record_Beginn, DateAndTime.Now)} min" : "";
                Streamshow();
                LAB_IP_Block.Visible = Pri_Model_Class?.Pro_Model_Access_Denied/*.Value*/ ?? false;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.RecordCheck.Tick");
            }
            finally
            {
                _RecordCheck?.Start();
            }
        }

        internal async void Streamshow()
        {
            try
            {
                _RefreshTimer.Stop();
                if (ParentForm == null || ParentForm.WindowState == FormWindowState.Minimized || _Preview_Loader_Worker.IsBusy)
                    return;
                _Preview_Loader_Worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                StreamOnline = false;
                StreamOfflineCount++;
                Parameter.Error_Message(ex, "Control_Stream.Streamshow");
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
                    Preview_Image_Load(); // Try loading again if it failed

                if (Preview_image != null)
                {
                    PAN_Control.BackgroundImage?.Dispose(); // Dispose previous image
                    PAN_Control.BackgroundImage = Preview_image;
                    StreamOnline = true;
                    StreamOfflineCount = 0;
                }
                else
                {
                    StreamOnline = false;
                    StreamOfflineCount++;
                }

                if (_RefreshTimer != null)
                    _RefreshTimer.Start();
            }
            catch (Exception ex)
            {
                StreamOnline = false;
                StreamOfflineCount++;
                Parameter.Error_Message(ex, "Control_Stream.Preview_Image_Show");
                if (_RefreshTimer != null)
                    _RefreshTimer.Start();
            }
        }

        private void PAN_Control_BackColorChanged(object sender, EventArgs e)
        {
            PAN_Control.BackColor = Color.Transparent;
        }

        private void Control_Stream_Load(object sender, EventArgs e)
        {
            Streamshow();
        }

        private void PAN_Control_DoubleClick(object sender, EventArgs e)
        {
            Sites.WebSiteOpen(Pri_Model_Class.Pro_Website_ID, Pri_Model_Class.Pro_Model_Name.ToString());
        }

        private void BTN_Record_Click(object sender, EventArgs e)
        {
            Aufnahme();
        }

        private async void Aufnahme()
        {
            try
            {
                Pri_Model_Class.Pro_Model_Record = !Record_Run;
                Record_Run = Pri_Model_Class.Pro_Model_Stream_Record != null;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.Aufnahme");
            }
        }

        private void TBT_Play_Click(object sender, EventArgs e) // Changed from ToggleStateChanged
        {
            try
            {
                // For native ToggleButton, CheckState is not directly available, use Checked property
                if (TBT_Play.Checked)
                {
                    Refresh_Intervall = Refresh_Intervall_min;
                    _RefreshTimer.Stop();
                    _RefreshTimer.Interval = Refresh_Intervall;
                    _RefreshTimer.Start();
                    TBT_Play.Image = Resources.control_stop_right_icon;
                }
                else
                {
                    Refresh_Intervall = Refresh_Intervall_max;
                    _RefreshTimer.Stop();
                    _RefreshTimer.Interval = Refresh_Intervall;
                    _RefreshTimer.Start();
                    TBT_Play.Image = Resources.control_right_icon;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.TBT_Play_ToggleStateChanged");
            }
        }

        private void BTN_Modelview_Click(object sender, EventArgs e)
        {
            Galerie_Open();
        }

        private async void Galerie_Open()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                await Task.Run(() => Pri_Model_Class.Dialog_Model_View_Show());
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.Galerie_Open");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            Visible = false;
            Vorschau_Close?.Invoke(this);
        }

        private void TBT_Favorite_Click(object sender, EventArgs e) // Changed from ToggleStateChanged
        {
            try
            {
                // For native ToggleButton, use Checked property
                if (TBT_Favorite.Checked)
                    TBT_Favorite.Image = Resources.Favorite16;
                else
                    TBT_Favorite.Image = Resources.FavoriteDeaktiv16;

                Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite = TBT_Favorite.Checked; // Update model based on new state
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.TBT_Favorite_Click");
            }
        }

        private void Favoriten_Record()
        {
            try
            {
                Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite = !TBT_Favorite.Checked; // Toggle the favorite status
                TBT_Favorite.Checked = Pri_Model_Class.Pro_Model_Stream_Record.Pro_Favorite; // Update UI
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.Favoriten_Record");
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
                Parameter.Error_Message(ex, "Control_Stream.Favoriten_Model");
            }
        }

        private void Header_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (ClientRectangle.Contains(PointToClient(Control.MousePosition)))
                    return;

                if (!Show_Header)
                    PAN_Header.Visible = false;

                if (!TBT_Play.Checked) // Check if not checked (i.e., playing at max interval)
                {
                    Refresh_Intervall = Refresh_Intervall_max;
                    _RefreshTimer.Stop();
                    _RefreshTimer.Interval = Refresh_Intervall;
                    _RefreshTimer.Start();
                }
                _Header_Timer.Stop();
            }
            catch (Exception ex)
            {
                // Log or handle exception
            }
        }

        private void PAN_Control_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (!Show_Header)
                    PAN_Header.Visible = true;
                _Header_Timer.Start();

                if (!TBT_Play.Checked) // If not playing (at max interval), switch to min interval
                {
                    Refresh_Intervall = Refresh_Intervall_min;
                    _RefreshTimer.Stop();
                    _RefreshTimer.Interval = Refresh_Intervall;
                    _RefreshTimer.Start();
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
            }
        }

        private void PAN_Header_VisibleChanged(object sender, EventArgs e)
        {
            if (Record_Run)
                PAN_Header.BackColor = Color.FromArgb(100, Color.IndianRed);
            else
                PAN_Header.BackColor = Color.FromArgb(100, BackColor);

            // Set background color for child controls to be transparent relative to PAN_Header
            LAB_Bezeichnung.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            LAB_Gender.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            BTN_Record.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            BTN_Modelview.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            BTN_Close.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            LAB_Resolution.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            LAB_Website.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            LAB_Offline.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            TBT_Favorite.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            TBT_Play.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
            LAB_Aufnahmezeit.BackColor = Color.FromArgb(0, PAN_Header.BackColor);
        }

        private void COM_Preview_DropDownOpening(object sender, CancelEventArgs e) // Changed from DropDownOpening to Opening
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

                CMI_CamBrowser.Visible = ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Browser", "Product", "0")) != -1;

                // Use .Result for async calls if you are sure it won't deadlock in a UI thread, or use await
                CMI_StreamRefresh.Visible = !Parameter.URL_Response(Pri_Model_Class.Pro_Model_M3U8).Result;

                CMI_Fade_In.Visible = !Pri_Model_Class.Pro_Model_Visible;

                if (!Pri_Model_Class.Pro_Model_Record)
                {
                    CMI_Aufnahme.Image = Resources.RecordAutomatic16;
                    CMI_Aufnahme.Text = TXT.TXT_Description("Automatische Aufnahme starten");
                    CMI_Aufnahme.Enabled = Parameter.URL_Response(Pri_Model_Class?.Pro_Model_M3U8).Result;
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
                    CMI_Favoriten_Record.Text = TBT_Favorite.Checked ? TXT.TXT_Description("Aufnahme aus Favoriten löschen") : TXT.TXT_Description("Aufnahme zu Favoriten hinzufügen");
                }

                if (Pri_Model_Class.Pro_Model_Favorite)
                {
                    CMI_Favoriten_Model.Image = Resources.Favorite16;
                    CMI_Favoriten_Model.Text = TXT.TXT_Description("Model aus Favoriten löschen");
                }
                else
                {
                    CMI_Favoriten_Model.Image = Resources.FavoriteDeaktiv16;
                    CMI_Favoriten_Model.Text = TXT.TXT_Description("Model zu Favoriten hinzufügen");
                }

                Class_Website classWebsite = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
                if (classWebsite != null)
                {
                    CMI_Webseite.Text = $"{classWebsite.Pro_Name} {TXT.TXT_Description("Webseite öffnen")}";
                    CMI_Webseite.Image = new Bitmap(classWebsite.Pro_Image, 16, 16);
                    CMI_Webseite.Tag = classWebsite.Pro_ID;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.COM_Preview_DropDown");
            }
        }

        private void CMI_Fade_In_Click(object sender, EventArgs e)
        {
            try
            {
                Pri_Model_Class.Pro_Model_Visible = true;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Fade_In_Click");
            }
        }

        private void CMI_Optionen_Click(object sender, EventArgs e)
        {
            try
            {
                using (Dialog_Model_Einstellungen modelEinstellungen = new Dialog_Model_Einstellungen(Pri_Model_Class.Pro_Model_GUID))
                {
                    modelEinstellungen.StartPosition = FormStartPosition.CenterParent;
                    modelEinstellungen.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Optionen_Click");
            }
        }

        private void CMI_Webseite_Click(object sender, EventArgs e)
        {
            Sites.WebSiteOpen(Pri_Model_Class.Pro_Website_ID, Pri_Model_Class.Pro_Model_Name);
        }

        private void CMI_CamBrowser_Click(object sender, EventArgs e)
        {
            Class_Website classWebsite = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
            if (classWebsite != null)
            {
                Sites.CamBrowser_Open(classWebsite.Pro_URL + Pri_Model_Class.Pro_Model_Name.ToString());
            }
        }

        private void CMI_Galerie_Click(object sender, EventArgs e)
        {
            Galerie_Open();
        }

        private void CMI_Aufnahme_Click(object sender, EventArgs e)
        {
            Aufnahme();
        }

        private void CMI_Favoriten_Click(object sender, EventArgs e)
        {
            Favoriten_Model();
        }

        private void CMI_Info_Click(object sender, EventArgs e)
        {
            try
            {
                Dialog_Model_Info dialogModelInfo = new Dialog_Model_Info();
                dialogModelInfo.TXB_Memo.Text = Pri_Model_Class.Pro_Model_Info.ToString();
                dialogModelInfo.StartPosition = FormStartPosition.CenterParent;
                if (dialogModelInfo.ShowDialog() == DialogResult.OK)
                {
                    Pri_Model_Class.Pro_Model_Info = dialogModelInfo.TXB_Memo.Text.ToString();
                    Pri_Model_Class.Model_Online_Changed();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Info_Click");
            }
        }

        private void CMI_Webseite_Kopieren_Click(object sender, EventArgs e)
        {
            try
            {
                string text = "";
                Class_Website classWebsite = Sites.Website_Find(Pri_Model_Class.Pro_Website_ID);
                if (classWebsite != null)
                    text = string.Format(classWebsite.Pro_Model_URL, Pri_Model_Class.Pro_Model_Name.ToString());

                if (text.Length > 0)
                {
                    Clipboard.SetText(text);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Webseite_Kopieren");
            }
        }

        private void CMI_Favoriten_Record_Click(object sender, EventArgs e)
        {
            Favoriten_Record();
        }

        private void CMI_Stop_Off_Click(object sender, EventArgs e)
        {
            try
            {
                Pri_Model_Class.Pro_Aufnahme_Stop_Off = !Pri_Model_Class.Pro_Aufnahme_Stop_Off;
                CMI_Stop_Off.Checked = Pri_Model_Class.Pro_Aufnahme_Stop_Off;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_Stop_Off");
            }
        }

        private async void CMI_StreamRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string result = await Pri_Model_Class.Model_Stream_Adressen_Load_Back();
                if (result != null)
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Stream.CMI_StreamRefresh_Click");
            }
        }

        private void Control_Stream_ControlAdded(object sender, ControlEventArgs e)
        {
            // No specific logic needed here based on the original VB.NET code
        }

        private void Control_Stream_GotFocus(object sender, EventArgs e)
        {
            // No specific logic needed here based on the original VB.NET code
        }

        private void PAN_Control_Click(object sender, EventArgs e)
        {
            Vorschau_Focused?.Invoke(this);
        }

        // Delegates for events
        internal delegate void StreamRecord_StartEventHandler(Control_Stream Control);
        internal delegate void StreamRecord_StopEventHandler(Control_Stream Control);
        internal delegate void Vorschau_CloseEventHandler(Control_Stream sender);
        internal delegate void Vorschau_FocusedEventHandler(Control_Stream sender);

        // Events
        internal event StreamRecord_StartEventHandler StreamRecord_Start;
        internal event StreamRecord_StopEventHandler StreamRecord_Stop;
        internal event Vorschau_CloseEventHandler Vorschau_Close;
        internal event Vorschau_FocusedEventHandler Vorschau_Focused;
    }
}
