using System.Timers;

namespace XstreaMonNET8
{
    public partial class Control_Model_Info : UserControl
    {
        private System.Timers.Timer VisibleTimer;
        private System.Timers.Timer UnVisible_Timer;
        private Guid Model_GUID;

        public event Action Evt_Timer_Elappsed;

        public Control_Model_Info()
        {
            InitializeComponent();

            // timers de visibilidad
            VisibleTimer = new System.Timers.Timer(1500);
            UnVisible_Timer = new System.Timers.Timer(30000);

            VisibleTimer.Elapsed += VisibleTimer_Elapsed;
            UnVisible_Timer.Elapsed += Unvisible_Timer_Elapsed;

            this.VisibleChanged += Control_Model_Info_VisibleChanged;
            this.Load += Control_Model_Info_Load;
        }

        internal Guid Pro_Model_GUID
        {
            get => Model_GUID;
            set => Model_GUID = value;
        }

        internal Image Pro_Model_Preview
        {
            get => picPreview.Image;
            set
            {
                picPreview.Visible = value != null;
                picPreview.Image = value;
            }
        }

        internal string Pro_Model_Info
        {
            get => lblInfo.Text;
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        lblInfo.Text = value;
                        Width = 400;
                    }
                    else
                    {
                        lblInfo.Text = string.Empty;
                        Width = 230;
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Control_Model_Info.Pro_Model_Info");
                }
            }
        }

        internal Class_Model Pro_Model
        {
            set
            {
                try
                {
                    if (value == null)
                    {
                        Visible = false;
                        VisibleTimer.Stop();
                        return;
                    }

                    // carga miniatura si existe
                    string thumbPath = Path.Combine(value.Pro_Model_Directory, "Thumbnail.jpg");
                    if (File.Exists(thumbPath) && new FileInfo(thumbPath).Length > 0)
                    {
                        try { Pro_Model_Preview = new Bitmap(thumbPath); }
                        catch { Pro_Model_Preview = null; }
                    }
                    else Pro_Model_Preview = null;

                    Pro_Model_GUID = ValueBack.Get_CUnique(value.Pro_Model_GUID);

                    // construye el texto informativo
                    var info = "";
                    if (!string.IsNullOrEmpty(value.Pro_Model_Country))
                        info += TXT.TXT_Description("Land") + ": " + value.Pro_Model_Country + "\r\n";
                    if (!string.IsNullOrEmpty(value.Pro_Model_Language))
                        info += TXT.TXT_Description("Sprachen") + ": " + value.Pro_Model_Language + "\r\n";
                    if (value.Pro_Model_Token > 0)
                        info += TXT.TXT_Description("Token") + ": " + value.Pro_Model_Token + "\r\n";
                    info += value.Pro_Model_Info + "\r\n";

                    int count = 0;
                    long size = 0;
                    foreach (var f in new DirectoryInfo(value.Pro_Model_Directory).GetFiles())
                    {
                        try
                        {
                            if (f.Extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase) ||
                                f.Extension.Equals(".ts", StringComparison.OrdinalIgnoreCase) ||
                                f.Extension.Equals(".mov", StringComparison.OrdinalIgnoreCase))
                            {
                                count++;
                                size += f.Length;
                            }
                        }
                        catch { }
                    }
                    if (count > 0)
                    {
                        info += TXT.TXT_Description("Aufnahmen gespeichert") + " " + count + "\r\n";
                        info += TXT.TXT_Description("Größe aller Aufnahmen") + " " + ValueBack.Get_Numeric2Bytes(size) + "\r\n";
                    }
                    if (value.Pro_Last_Online > DateTime.MinValue)
                        info += TXT.TXT_Description("zuletzt online") + ": " +
                                ValueBack.Get_CDatum(value.Pro_Last_Online) + "\r\n";

                    Pro_Model_Info = info;
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Control_Model_Info.Pro_Model");
                }
            }
        }

        internal object Control_Visible
        {
            set
            {
                try
                {
                    bool show = Convert.ToBoolean(value);
                    if (show)
                    {
                        VisibleTimer.Start();
                        UnVisible_Timer.Start();
                    }
                    else
                    {
                        VisibleTimer.Stop();
                        Visible = false;
                        UnVisible_Timer.Stop();
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Control_Model_Info.Control_Visible");
                }
            }
        }

        private void Control_Model_Info_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible && picPreview.Image == null && string.IsNullOrWhiteSpace(lblInfo.Text))
            {
                VisibleTimer.Stop();
                Visible = false;
            }
        }

        private void VisibleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        VisibleTimer.Stop();
                        Visible = true;
                        Evt_Timer_Elappsed?.Invoke();
                    }));
                }
                else
                {
                    VisibleTimer.Stop();
                    Visible = true;
                    Evt_Timer_Elappsed?.Invoke();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Model_Info.VisibleTimer_Elapsed");
            }
        }

        private void Unvisible_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        UnVisible_Timer.Stop();
                        Visible = false;
                    }));
                }
                else
                {
                    UnVisible_Timer.Stop();
                    Visible = false;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Model_Info.Unvisible_Timer_Elapsed");
            }
        }

        private void Control_Model_Info_Load(object sender, EventArgs e)
        {
            // lee tiempo de tooltip en segundos +10ms
            int secs = ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Optionen", "Tooltip", "3"));
            VisibleTimer.Interval = secs * 1000 + 10;
        }
    }
}
