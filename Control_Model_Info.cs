using System.Timers;

namespace XstreaMonNET8
{
    public class Control_Model_Info : UserControl
    {
        private Label lblInfo;
        private PictureBox picPreview;
        private Guid Model_GUID;
        private System.Timers.Timer VisibleTimer;
        private System.Timers.Timer UnVisible_Timer;

        public event Action Evt_Timer_Elappsed;

        public Control_Model_Info()
        {
            VisibleTimer = new System.Timers.Timer(1500);
            UnVisible_Timer = new System.Timers.Timer(30000);

            VisibleTimer.Elapsed += VisibleTimer_Elapsed;
            UnVisible_Timer.Elapsed += Unvisible_Timer_Elapsed;

            VisibleChanged += Control_Model_Info_VisibleChanged;
            Load += Control_Model_Info_Load;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            lblInfo = new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft
            };

            picPreview = new PictureBox
            {
                Dock = DockStyle.Left,
                Size = new Size(230, 138),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Controls.Add(lblInfo);
            Controls.Add(picPreview);
            Size = new Size(452, 138);
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
                    if (value != null)
                    {
                        lblInfo.Text = value;
                        Width = string.IsNullOrEmpty(value) ? 230 : 400;
                    }
                    else
                    {
                        lblInfo.Text = "";
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
            get => null;
            set
            {
                try
                {
                    if (value != null)
                    {
                        string thumbPath = Path.Combine(value.Pro_Model_Directory, "Thumbnail.jpg");

                        if (File.Exists(thumbPath) && new FileInfo(thumbPath).Length > 0)
                        {
                            try
                            {
                                Pro_Model_Preview = new Bitmap(thumbPath);
                            }
                            catch
                            {
                                Pro_Model_Preview = null;
                            }
                        }
                        else
                        {
                            Pro_Model_Preview = null;
                        }

                        Pro_Model_GUID = ValueBack.Get_CUnique(value.Pro_Model_GUID);
                        string info = "";

                        if (!string.IsNullOrEmpty(value.Pro_Model_Country))
                            info += TXT.TXT_Description("Land") + ": " + value.Pro_Model_Country + "\r\n";
                        if (!string.IsNullOrEmpty(value.Pro_Model_Language))
                            info += TXT.TXT_Description("Sprachen") + ": " + value.Pro_Model_Language + "\r\n";
                        if (value.Pro_Model_Token > 0)
                            info += TXT.TXT_Description("Token") + ": " + value.Pro_Model_Token + "\r\n";
                        info += value.Pro_Model_Info + "\r\n";

                        int count = 0;
                        double size = 0;
                        foreach (var file in new DirectoryInfo(value.Pro_Model_Directory).GetFiles())
                        {
                            try
                            {
                                if (file.Extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase) ||
                                    file.Extension.Equals(".ts", StringComparison.OrdinalIgnoreCase) ||
                                    file.Extension.Equals(".mov", StringComparison.OrdinalIgnoreCase))
                                {
                                    count++;
                                    size += file.Length;
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
                        {
                            info += TXT.TXT_Description("zuletzt online") + ": " +
                                    ValueBack.Get_CDatum(value.Pro_Last_Online) + "\r\n";
                        }

                        Pro_Model_Info = info;
                    }
                    else
                    {
                        Visible = false;
                        VisibleTimer.Stop();
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Control_Model_Info.Pro_Model");
                }
            }
        }

        internal object Control_Visible
        {
            get => Visible;
            set
            {
                try
                {
                    if (Convert.ToBoolean(value))
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
            if (Visible && Pro_Model_Preview == null && string.IsNullOrWhiteSpace(Pro_Model_Info))
            {
                Visible = false;
                VisibleTimer.Stop();
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
                    }));
                }
                else
                {
                    VisibleTimer.Stop();
                    Visible = true;
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
                Parameter.Error_Message(ex, "Control_Model_Info.UnVisible_Timer_Elapsed");
            }
        }

        private void Control_Model_Info_Load(object sender, EventArgs e)
        {
            VisibleTimer.Interval = ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Optionen", "Tooltip", "3")) * 1000 + 10;
        }

        public delegate void Evt_Timer_ElappsedEventHandler();
    }
}
