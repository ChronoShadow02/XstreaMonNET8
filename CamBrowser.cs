using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XstreaMonNET8
{
    public class CamBrowser : Form
    {
        private IContainer components;
        private int Pri_Website_ID;
        private bool Pri_User_loggedOn;
        private System.Windows.Forms.Timer Site_Check;
        private string Site_Hash;
        private bool Pri_Navigation_Online;
        private bool Pri_Navigation_Records;
        private bool Pri_Navigation_Token;
        private string Pri_Last_Check_URL;
        private List<int> Token_List;
        private System.Windows.Forms.Timer Token_Timer;
        private List<string> Model_List_Online;

        /// <summary>
        /// 
        /// </summary>
        private ToolStripButton _CBB_Back;
        private ToolStripButton _CBB_Next;
        private ToolStripSeparator _CommandBarSeparator1;
        private ToolStripButton _CBB_Refresh;
        private ToolStripSeparator _CommandBarSeparator2;
        private ToolStripButton _CBB_Add;
        private ToolStripButton _CBB_Record;
        private ToolStripButton _CBB_Galerie;
        public CamBrowser()
        {
            Load += new EventHandler(this.CamBrowser_Load);
            Closed += new EventHandler(this.CamBrowser_Closed);
            Closing += new CancelEventHandler(this.CamBrowser_Closing);
            Pri_Website_ID = -1;
            Pri_User_loggedOn = false;
            Site_Check = new System.Windows.Forms.Timer();
            Site_Hash = "";
            Pri_Navigation_Online = true;
            Pri_Navigation_Records = true;
            Pri_Navigation_Token = true;
            Pri_Last_Check_URL = "";
            Token_List = new List<int>();
            Token_Timer = new System.Windows.Forms.Timer();
            Model_List_Online = new List<string>();
            this.InitializeComponent();
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!disposing || this.components == null)
                    return;
                this.components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
            var componentResourceManager = new ComponentResourceManager(typeof(CamBrowser));

            // === Command bar (ToolStrip) ===
            this.RadCommandBar1 = new ToolStrip();
            this.CBB_Back = new ToolStripButton();
            this.CBB_Next = new ToolStripButton();
            this.CommandBarSeparator1 = new ToolStripSeparator();
            this.CBB_Refresh = new ToolStripButton();
            this.CommandBarSeparator5 = new ToolStripSeparator();
            this.CBB_Navigation = new ToolStripDropDownButton();
            this.CommandBarSeparator2 = new ToolStripSeparator();
            this.CBB_Add = new ToolStripButton();
            this.CBB_Galerie = new ToolStripButton();
            this.CBB_Save = new ToolStripButton();
            this.CommandBarSeparator3 = new ToolStripSeparator();
            this.CBB_Record_Automatik = new ToolStripButton();
            this.CBB_Record = new ToolStripButton();
            this.CBB_Model_Up = new ToolStripButton();
            this.CBB_Model_Down = new ToolStripButton();

            // dropdown items
            this.CMI_Records = new ToolStripMenuItem();
            this.CMI_Token = new ToolStripMenuItem();
            this.CMI_Online = new ToolStripMenuItem();
            this.RadMenuSeparatorItem1 = new ToolStripSeparator();
            this.CMI_Off = new ToolStripMenuItem();

            // configure toolstrip
            this.RadCommandBar1.Items.AddRange(new ToolStripItem[] {
        CBB_Back, CBB_Next, CommandBarSeparator1,
        CBB_Refresh, CommandBarSeparator5,
        CBB_Navigation, CommandBarSeparator2,
        CBB_Add, CBB_Galerie, CBB_Save,
        CommandBarSeparator3, CBB_Record_Automatik,
        CBB_Record, CBB_Model_Up, CBB_Model_Down
    });
            this.RadCommandBar1.Dock = DockStyle.Top;
            this.RadCommandBar1.Location = new Point(0, 0);
            this.RadCommandBar1.Name = "RadCommandBar1";
            this.RadCommandBar1.Size = new Size(1358, 40);
            this.RadCommandBar1.TabIndex = 0;

            // LAB_Info (Label) hosted on the strip
            this.LAB_Info = new Label();
            this.LAB_Info.AutoSize = false;
            this.LAB_Info.BackColor = Color.Transparent;
            this.LAB_Info.Location = new Point(1319, 0);
            this.LAB_Info.Name = "LAB_Info";
            this.LAB_Info.Size = new Size(39, 40);
            this.LAB_Info.Visible = false;
            this.LAB_Info.Image = (Image)componentResourceManager.GetObject("info");
            this.LAB_Info.Text = "";
            this.LAB_Info.DoubleClick += LAB_Info_DoubleClick;
            // host label in ToolStrip
            var hostInfo = new ToolStripControlHost(this.LAB_Info) { Name = "LAB_Info_Host" };
            this.RadCommandBar1.Items.Add(hostInfo);

            // configure each button
            this.CBB_Back.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.CBB_Back.Image = (Image)componentResourceManager.GetObject("Back32");
            this.CBB_Back.Name = "CBB_Back";
            this.CBB_Back.Click += CBB_Back_Click;

            this.CBB_Next.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.CBB_Next.Image = (Image)componentResourceManager.GetObject("Next32");
            this.CBB_Next.Name = "CBB_Next";
            this.CBB_Next.Click += CBB_Next_Click;

            this.CBB_Refresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.CBB_Refresh.Image = (Image)componentResourceManager.GetObject("refresh_32");
            this.CBB_Refresh.Name = "CBB_Refresh";
            this.CBB_Refresh.Click += CBB_Refresh_Click;

            this.CBB_Navigation.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.CBB_Navigation.Image = (Image)componentResourceManager.GetObject("NavigationOnline32");
            this.CBB_Navigation.Name = "CBB_Navigation";
            this.CBB_Navigation.Text = "Navigation";
            this.CBB_Navigation.DropDownItems.AddRange(new ToolStripItem[] {
        CMI_Records, CMI_Token, CMI_Online, RadMenuSeparatorItem1, CMI_Off
    });

            this.CMI_Records.CheckOnClick = true;
            this.CMI_Records.Name = "CMI_Records";
            this.CMI_Records.Text = "Aufnahmen";
            this.CMI_Records.Click += CMI_Records_CheckChanged;

            this.CMI_Token.CheckOnClick = true;
            this.CMI_Token.Name = "CMI_Token";
            this.CMI_Token.Text = "Token";
            this.CMI_Token.Click += CMI_Token_CheckChanged;

            this.CMI_Online.CheckOnClick = true;
            this.CMI_Online.Name = "CMI_Online";
            this.CMI_Online.Text = "Online";
            this.CMI_Online.Click += CMI_Online_CheckChanged;

            this.CMI_Off.CheckOnClick = true;
            this.CMI_Off.Name = "CMI_Off";
            this.CMI_Off.Text = "Aus";
            this.CMI_Off.Click += CMI_Off_CheckChanged;

            // other toolbar buttons (collapsed by default)
            Action<ToolStripButton, string, string> setupBtn = (btn, resKey, text) =>
            {
                btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                btn.Image = (Image)componentResourceManager.GetObject(resKey);
                btn.Name = btn.Name; // keep field name
                btn.Text = text;
                btn.Visible = false;
            };
            setupBtn(CBB_Add, "Plus32", "Add");
            setupBtn(CBB_Galerie, "Galerie32", "Galerie");
            setupBtn(CBB_Save, "Speichern", "Speichern");
            setupBtn(CBB_Record_Automatik, "RecordAutomatic32", "AutoRecord");
            setupBtn(CBB_Record, "control_record_icon32", "Manuel Record");
            setupBtn(CBB_Model_Up, "Up", "Up");
            setupBtn(CBB_Model_Down, "Down", "Down");

            // === PAN_Token ===
            this.PAN_Token = new Panel { Name = "PAN_Token", Dock = DockStyle.Top, Size = new Size(92, 348), Visible = false, AutoSize = true };
            this.LAB_Token = new Label { Name = "LAB_Token", Dock = DockStyle.Top, Size = new Size(92, 34) };
            this.PAN_Token_Text = new Panel { Name = "PAN_Token_Text", Dock = DockStyle.Top, Size = new Size(92, 66), AutoSize = true };
            this.RadPanel3 = new Panel { Name = "RadPanel3", Dock = DockStyle.Fill };
            this.BTN_Token_Intervall = new CheckBox { Name = "BTN_Token_Intervall", Appearance = Appearance.Button, Dock = DockStyle.Fill, Text = "" };
            this.SPE_Intervall_Sekunden = new NumericUpDown { Name = "SPE_Intervall_Sekunden", Dock = DockStyle.Right, Minimum = 1, Maximum = 120, Visible = false };
            this.TXB_Intervall_Token = new TextBox { Name = "TXB_Intervall_Token", Dock = DockStyle.Top, Size = new Size(92, 34), Text = "" };

            // token buttons
            Button makeTokenBtn(string name, string text)
            {
                return new Button
                {
                    Name = name,
                    Text = text,
                    Dock = DockStyle.Top,
                    Font = new Font("Segoe UI", 8.25f, FontStyle.Bold)
                };
            }
            this.BTN_Token_1 = makeTokenBtn("BTN_Token_1", "1 Token");
            this.BTN_Token_2 = makeTokenBtn("BTN_Token_2", "2 Token");
            this.BTN_Token_5 = makeTokenBtn("BTN_Token_5", "5 Token");
            this.BTN_Token_10 = makeTokenBtn("BTN_Token_10", "10 Token");
            this.BTN_Token_15 = makeTokenBtn("BTN_Token_15", "15 Token");
            this.BTN_Token_25 = makeTokenBtn("BTN_Token_25", "25 Token");
            this.BTN_Token_50 = makeTokenBtn("BTN_Token_50", "50 Token");

            // assemble PAN_Token
            this.RadPanel3.Controls.AddRange(new Control[] { BTN_Token_Intervall, SPE_Intervall_Sekunden });
            this.PAN_Token_Text.Controls.AddRange(new Control[] { RadPanel3, TXB_Intervall_Token });
            this.PAN_Token.Controls.AddRange(new Control[] {
        BTN_Token_50, BTN_Token_25, BTN_Token_15,
        BTN_Token_10, BTN_Token_5, BTN_Token_2,
        BTN_Token_1, PAN_Token_Text, LAB_Token
    });

            // === PAN_Right, PAN_Online, PAN_Manual_Records ===
            this.PAN_Online = new Panel { Name = "PAN_Online", Dock = DockStyle.Fill, AutoScroll = true };
            this.PAN_Manual_Records = new Panel { Name = "PAN_Manual_Records", Dock = DockStyle.Top, AutoScroll = true, AutoSize = true };
            this.PAN_Right = new Panel { Name = "PAN_Right", Dock = DockStyle.Right, Size = new Size(92, 732) };
            this.PAN_Right.Controls.AddRange(new Control[] { PAN_Online, PAN_Token, PAN_Manual_Records });

            // === ToolTip, Timers, WebView2, Control_Model_Info ===
            this.TOT_Browser = new ToolTip(this.components);
            this.TIM_Cache_Clear = new Timer(this.components) { Interval = 600000 };
            this.TIM_Cache_Clear.Tick += TIM_Cache_Clear_Tick;

            this.Site_Check = new Timer { Interval = 2500 };
            this.Site_Check.Tick += (s, e) => Site_Check_Elapsed();

            this.Control_Model_Info1 = new Control_Model_Info { Name = "Control_Model_Info1", Location = new Point(-18, 93) };
            this.WV_Site = new WebView2 { Name = "WV_Site", Dock = DockStyle.Fill, ZoomFactor = 1.0 };

            this.WV_Site.CoreWebView2InitializationCompleted += WV_Site_CoreWebView2InitializationCompleted;
            this.WV_Site.NavigationStarting += WV_Site_NavigationStarting;
            this.WV_Site.NavigationCompleted += WV_Site_NavigationCompleted;
            this.WV_Site.SourceChanged += WV_Site_SourceChanged;

            // === Form settings & Controls ===
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1358, 772);
            this.Controls.AddRange(new Control[] {
                WV_Site,
                Control_Model_Info1,
                PAN_Right,
                RadCommandBar1
            });
            this.Name = nameof(CamBrowser);
            this.Text = nameof(CamBrowser);
            this.WindowState = FormWindowState.Maximized;
        }

        internal virtual ToolStripButton CBB_Back
        {
            get => this._CBB_Back;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new(this.CBB_Back_Click);
                var old = this._CBB_Back;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Back = value;
                var cur = this._CBB_Back;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        internal virtual ToolStripButton CBB_Next
        {
            get => this._CBB_Next;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Next_Click);
                var old = this._CBB_Next;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Next = value;
                var cur = this._CBB_Next;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        [field: AccessedThroughProperty("CommandBarSeparator1")]
        internal virtual ToolStripSeparator CommandBarSeparator1
        {
            get => this._CommandBarSeparator1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._CommandBarSeparator1 = value;
        }

        internal virtual ToolStripButton CBB_Refresh
        {
            get => this._CBB_Refresh;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Refresh_Click);
                var old = this._CBB_Refresh;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Refresh = value;
                var cur = this._CBB_Refresh;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        [field: AccessedThroughProperty("CommandBarSeparator2")]
        internal virtual ToolStripSeparator CommandBarSeparator2
        {
            get => this._CommandBarSeparator2;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._CommandBarSeparator2 = value;
        }

        internal virtual ToolStripButton CBB_Add
        {
            get => this._CBB_Add;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Add_Click);
                var old = this._CBB_Add;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Add = value;
                var cur = this._CBB_Add;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        internal virtual ToolStripButton CBB_Record
        {
            get => this._CBB_Record;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Record_Click);
                var old = this._CBB_Record;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Record = value;
                var cur = this._CBB_Record;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        internal virtual ToolStripButton CBB_Galerie
        {
            get => this._CBB_Galerie;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Galerie_Click);
                ToolStripButton cbbGalerie1 = this._CBB_Galerie;
                if (cbbGalerie1 != null)
                    cbbGalerie1.Click -= eventHandler;
                this._CBB_Galerie = value;
                ToolStripButton cbbGalerie2 = this._CBB_Galerie;
                if (cbbGalerie2 == null)
                    return;
                cbbGalerie2.Click += eventHandler;
            }
        }
    }
}
