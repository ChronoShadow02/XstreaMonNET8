using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Timer = System.Windows.Forms.Timer;

namespace XstreaMonNET8
{
    public class CamBrowser : Form
    {
        private IContainer components;
        private string Pri_WebView_Source;
        private string Current_URL;
        private string Current_Model_Name;
        private int Pri_Website_ID;
        private bool Pri_User_loggedOn;
        private string Site_Hash;
        private Guid Token_Model_GUID;
        private bool Pri_Navigation_Online;
        private bool Pri_Navigation_Records;
        private bool Pri_Navigation_Token;
        private string Pri_Last_Check_URL;
        private int Token_Send;
        private int Token_Count;
        private List<int> Token_List;
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
        private ToolStripSeparator _CommandBarSeparator3;
        private Button _BTN_Token_25;
        private Button _BTN_Token_15;
        private Button _BTN_Token_10;
        private Button _BTN_Token_5;
        private Button _BTN_Token_2;
        private Button _BTN_Token_1;
        private TextBox _TXB_Intervall_Token;
        private Panel _PAN_Token;
        private Label _LAB_Token;
        private NumericUpDown _SPE_Intervall_Sekunden;
        private CheckBox _BTN_Token_Intervall;
        private Panel _PAN_Token_Text;
        private ToolStripButton _CBB_Record_Automatik;
        private Button _BTN_Token_50;
        private Panel _RadPanel3;
        private ToolTip _TOT_Browser;
        private Panel _PAN_Right;
        private Panel _PAN_Manual_Records;
        private Timer _TIM_Cache_Clear;
        private ToolStripDropDownButton _CBB_Navigation;
        private ToolStripMenuItem _CMI_Records;
        private ToolStripMenuItem _CMI_Token;
        private ToolStripMenuItem _CMI_Online;
        private ToolStripSeparator _RadMenuSeparatorItem1;
        private ToolStripMenuItem _CMI_Off;
        private Panel _PAN_Online;
        private ToolStripSeparator _CommandBarStripElement2;
        private ToolStripSeparator _CommandBarSeparator5;
        private Control_Model_Info _Control_Model_Info1;
        private ToolStripSeparator _CBS_Model_Navigation;
        private ToolStripButton _CBB_Model_Up;
        private ToolStripButton _CBB_Model_Down;
        private Label _LAB_Info;
        private ToolStripButton _CBB_Save;
        private Microsoft.Web.WebView2.WinForms.WebView2 _WV_Site;
        private Class_Model _Current_Model_Class;
        private Timer _Site_Check;
        private Timer _Token_Timer;
        private ToolStrip _RadCommandBar1;
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
            this.LAB_Info.Text = string.Empty;
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

        [field: AccessedThroughProperty("RadCommandBar1")]
        internal virtual ToolStrip RadCommandBar1
        {
            get => this._RadCommandBar1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._RadCommandBar1 = value;
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

        [field: AccessedThroughProperty("CommandBarSeparator3")]
        internal virtual ToolStripSeparator CommandBarSeparator3
        {
            get => this._CommandBarSeparator3;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._CommandBarSeparator3 = value;
        }

        internal virtual Button BTN_Token_25
        {
            get => this._BTN_Token_25;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Send_Click);
                var oldBtn = this._BTN_Token_25;
                if (oldBtn != null)
                    oldBtn.Click -= eventHandler;
                this._BTN_Token_25 = value;
                var newBtn = this._BTN_Token_25;
                if (newBtn != null)
                    newBtn.Click += eventHandler;
            }
        }

        internal virtual Button BTN_Token_15
        {
            get => this._BTN_Token_15;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Send_Click);
                var oldBtn = this._BTN_Token_15;
                if (oldBtn != null)
                    oldBtn.Click -= eventHandler;
                this._BTN_Token_15 = value;
                var newBtn = this._BTN_Token_15;
                if (newBtn != null)
                    newBtn.Click += eventHandler;
            }
        }

        internal virtual Button BTN_Token_10
        {
            get => this._BTN_Token_10;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Send_Click);
                Button btnToken10_1 = this._BTN_Token_10;
                if (btnToken10_1 != null)
                    btnToken10_1.Click -= eventHandler;
                this._BTN_Token_10 = value;
                Button btnToken10_2 = this._BTN_Token_10;
                if (btnToken10_2 != null)
                    btnToken10_2.Click += eventHandler;
            }
        }

        internal virtual Button BTN_Token_5
        {
            get => this._BTN_Token_5;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Send_Click);
                Button btnToken5_1 = this._BTN_Token_5;
                if (btnToken5_1 != null)
                    btnToken5_1.Click -= eventHandler;
                this._BTN_Token_5 = value;
                Button btnToken5_2 = this._BTN_Token_5;
                if (btnToken5_2 != null)
                    btnToken5_2.Click += eventHandler;
            }
        }

        internal virtual Button BTN_Token_2
        {
            get => this._BTN_Token_2;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Send_Click);
                Button btnToken2_1 = this._BTN_Token_2;
                if (btnToken2_1 != null)
                    btnToken2_1.Click -= eventHandler;
                this._BTN_Token_2 = value;
                Button btnToken2_2 = this._BTN_Token_2;
                if (btnToken2_2 != null)
                    btnToken2_2.Click += eventHandler;
            }
        }

        internal virtual Button BTN_Token_1
        {
            get => this._BTN_Token_1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Send_Click);
                Button btnToken1_1 = this._BTN_Token_1;
                if (btnToken1_1 != null)
                    btnToken1_1.Click -= eventHandler;
                this._BTN_Token_1 = value;
                Button btnToken1_2 = this._BTN_Token_1;
                if (btnToken1_2 != null)
                    btnToken1_2.Click += eventHandler;
            }
        }

        internal virtual TextBox TXB_Intervall_Token
        {
            get => this._TXB_Intervall_Token;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler changingEventHandler = new EventHandler(this.TXB_Intervall_Token_TextChanging);
                var old = this._TXB_Intervall_Token;
                if (old != null)
                    old.TextChanged -= changingEventHandler;
                this._TXB_Intervall_Token = value;
                var cur = this._TXB_Intervall_Token;
                if (cur != null)
                    cur.TextChanged += changingEventHandler;
            }
        }

        [field: AccessedThroughProperty("PAN_Token")]
        internal virtual Panel PAN_Token
        {
            get => this._PAN_Token;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._PAN_Token = value;
        }

        [field: AccessedThroughProperty("LAB_Token")]
        internal virtual Label LAB_Token
        {
            get => this._LAB_Token;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._LAB_Token = value;
        }

        internal virtual NumericUpDown SPE_Intervall_Sekunden
        {
            get => this._SPE_Intervall_Sekunden;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.RadSpinEditor1_ValueChanged);
                var oldCtrl = this._SPE_Intervall_Sekunden;
                if (oldCtrl != null)
                    oldCtrl.ValueChanged -= eventHandler;
                this._SPE_Intervall_Sekunden = value;
                var newCtrl = this._SPE_Intervall_Sekunden;
                if (newCtrl != null)
                    newCtrl.ValueChanged += eventHandler;
            }
        }

        internal virtual CheckBox BTN_Token_Intervall
        {
            get => this._BTN_Token_Intervall;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.BTN_Token_Intervall_ToggleStateChanged);
                var oldCtrl = this._BTN_Token_Intervall;
                if (oldCtrl != null)
                    oldCtrl.CheckedChanged -= eventHandler;
                this._BTN_Token_Intervall = value;
                var newCtrl = this._BTN_Token_Intervall;
                if (newCtrl != null)
                    newCtrl.CheckedChanged += eventHandler;
            }
        }

        [field: AccessedThroughProperty("PAN_Token_Text")]
        internal virtual Panel PAN_Token_Text
        {
            get => this._PAN_Token_Text;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._PAN_Token_Text = value;
        }

        internal virtual ToolStripButton CBB_Record_Automatik
        {
            get => this._CBB_Record_Automatik;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Record_Automatik_Click);
                var old = this._CBB_Record_Automatik;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Record_Automatik = value;
                var cur = this._CBB_Record_Automatik;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        internal virtual Button BTN_Token_50
        {
            get => this._BTN_Token_50;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Send_Click);
                var old = this._BTN_Token_50;
                if (old != null)
                    old.Click -= eventHandler;
                this._BTN_Token_50 = value;
                var cur = this._BTN_Token_50;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        [field: AccessedThroughProperty("RadPanel3")]
        internal virtual Panel RadPanel3
        {
            get => this._RadPanel3;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._RadPanel3 = value;
        }

        [field: AccessedThroughProperty("TOT_Browser")]
        internal virtual ToolTip TOT_Browser
        {
            get => this._TOT_Browser;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._TOT_Browser = value;
        }

        [field: AccessedThroughProperty("PAN_Right")]
        internal virtual Panel PAN_Right
        {
            get => this._PAN_Right;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._PAN_Right = value;
        }

        [field: AccessedThroughProperty("PAN_Manual_Records")]
        internal virtual Panel PAN_Manual_Records
        {
            get => this._PAN_Manual_Records;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._PAN_Manual_Records = value;
        }

        internal virtual Timer TIM_Cache_Clear
        {
            get => this._TIM_Cache_Clear;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.TIM_Cache_Clear_Tick);
                Timer oldTimer = this._TIM_Cache_Clear;
                if (oldTimer != null)
                    oldTimer.Tick -= eventHandler;
                this._TIM_Cache_Clear = value;
                Timer newTimer = this._TIM_Cache_Clear;
                if (newTimer != null)
                    newTimer.Tick += eventHandler;
            }
        }

        // CBB_Navigation
        [field: AccessedThroughProperty("CBB_Navigation")]
        internal virtual ToolStripDropDownButton CBB_Navigation
        {
            get => this._CBB_Navigation;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._CBB_Navigation = value;
        }

        // CMI_Records
        internal virtual ToolStripMenuItem CMI_Records
        {
            get => this._CMI_Records;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CMI_Records_CheckChanged);
                var oldItem = this._CMI_Records;
                if (oldItem != null)
                    oldItem.Click -= eventHandler;
                this._CMI_Records = value;
                var newItem = this._CMI_Records;
                if (newItem != null)
                    newItem.Click += eventHandler;
            }
        }

        // CMI_Token
        internal virtual ToolStripMenuItem CMI_Token
        {
            get => this._CMI_Token;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CMI_Token_CheckChanged);
                var oldItem = this._CMI_Token;
                if (oldItem != null)
                    oldItem.Click -= eventHandler;
                this._CMI_Token = value;
                var newItem = this._CMI_Token;
                if (newItem != null)
                    newItem.Click += eventHandler;
            }
        }

        // CMI_Online
        internal virtual ToolStripMenuItem CMI_Online
        {
            get => this._CMI_Online;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CMI_Online_CheckChanged);
                var oldItem = this._CMI_Online;
                if (oldItem != null)
                    oldItem.Click -= eventHandler;
                this._CMI_Online = value;
                var newItem = this._CMI_Online;
                if (newItem != null)
                    newItem.Click += eventHandler;
            }
        }

        // RadMenuSeparatorItem1
        [field: AccessedThroughProperty("RadMenuSeparatorItem1")]
        internal virtual ToolStripSeparator RadMenuSeparatorItem1
        {
            get => this._RadMenuSeparatorItem1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._RadMenuSeparatorItem1 = value;
        }

        // CMI_Off
        internal virtual ToolStripMenuItem CMI_Off
        {
            get => this._CMI_Off;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CMI_Off_CheckChanged);
                var oldItem = this._CMI_Off;
                if (oldItem != null)
                    oldItem.Click -= eventHandler;
                this._CMI_Off = value;
                var newItem = this._CMI_Off;
                if (newItem != null)
                    newItem.Click += eventHandler;
            }
        }

        [field: AccessedThroughProperty("PAN_Online")]
        internal virtual Panel PAN_Online
        {
            get => this._PAN_Online;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._PAN_Online = value;
        }

        [field: AccessedThroughProperty("CommandBarStripElement2")]
        internal virtual ToolStripSeparator CommandBarStripElement2
        {
            get => this._CommandBarStripElement2;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._CommandBarStripElement2 = value;
        }

        [field: AccessedThroughProperty("CommandBarSeparator5")]
        internal virtual ToolStripSeparator CommandBarSeparator5
        {
            get => this._CommandBarSeparator5;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._CommandBarSeparator5 = value;
        }

        [field: AccessedThroughProperty("Control_Model_Info1")]
        internal virtual Control_Model_Info Control_Model_Info1
        {
            get => this._Control_Model_Info1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._Control_Model_Info1 = value;
        }

        [field: AccessedThroughProperty("CBS_Model_Navigation")]
        internal virtual ToolStripSeparator CBS_Model_Navigation
        {
            get => this._CBS_Model_Navigation;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => this._CBS_Model_Navigation = value;
        }

        internal virtual ToolStripButton CBB_Model_Up
        {
            get => this._CBB_Model_Up;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Model_Up_Click);
                var old = this._CBB_Model_Up;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Model_Up = value;
                var cur = this._CBB_Model_Up;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        internal virtual ToolStripButton CBB_Model_Down
        {
            get => this._CBB_Model_Down;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Model_Down_Click);
                var old = this._CBB_Model_Down;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Model_Down = value;
                var cur = this._CBB_Model_Down;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        internal virtual Label LAB_Info
        {
            get => this._LAB_Info;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.LAB_Info_DoubleClick);
                var old = this._LAB_Info;
                if (old != null)
                    old.DoubleClick -= eventHandler;
                this._LAB_Info = value;
                var cur = this._LAB_Info;
                if (cur != null)
                    cur.DoubleClick += eventHandler;
            }
        }


        internal virtual ToolStripButton CBB_Save
        {
            get => this._CBB_Save;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.CBB_Save_Click);
                var old = this._CBB_Save;
                if (old != null)
                    old.Click -= eventHandler;
                this._CBB_Save = value;
                var cur = this._CBB_Save;
                if (cur != null)
                    cur.Click += eventHandler;
            }
        }

        internal virtual Microsoft.Web.WebView2.WinForms.WebView2 WV_Site
        {
            get => this._WV_Site;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler<CoreWebView2NavigationCompletedEventArgs> eventHandler1 =
                    new EventHandler<CoreWebView2NavigationCompletedEventArgs>(this.WV_Site_NavigationCompleted);
                EventHandler<CoreWebView2NavigationStartingEventArgs> eventHandler2 =
                    new EventHandler<CoreWebView2NavigationStartingEventArgs>(this.WV_Site_NavigationStarting);
                EventHandler<CoreWebView2SourceChangedEventArgs> eventHandler3 =
                    new EventHandler<CoreWebView2SourceChangedEventArgs>(this.WV_Site_SourceChanged);
                EventHandler<CoreWebView2InitializationCompletedEventArgs> eventHandler4 =
                    new EventHandler<CoreWebView2InitializationCompletedEventArgs>(this.WV_Site_CoreWebView2InitializationCompleted);

                var old = this._WV_Site;
                if (old != null)
                {
                    old.NavigationCompleted -= eventHandler1;
                    old.NavigationStarting -= eventHandler2;
                    old.SourceChanged -= eventHandler3;
                    old.CoreWebView2InitializationCompleted -= eventHandler4;
                }

                this._WV_Site = value;

                var cur = this._WV_Site;
                if (cur != null)
                {
                    cur.NavigationCompleted += eventHandler1;
                    cur.NavigationStarting += eventHandler2;
                    cur.SourceChanged += eventHandler3;
                    cur.CoreWebView2InitializationCompleted += eventHandler4;
                }
            }
        }

        internal string WebView_Source
        {
            get => this.Pri_WebView_Source;
            set
            {
                if (value == null)
                    return;
                this.WV_Site.Source = new Uri(value);
                this.Pri_WebView_Source = value;
            }
        }

        private int Current_Website_ID
        {
            get => this.Pri_Website_ID;
            set
            {
                if (this.Pri_Website_ID != value & value > -1)
                {
                    this.Pri_Website_ID = value;
                    this.Online_User_Load();
                }
                else
                {
                    this.Pri_Website_ID = value;
                }
            }
        }

        private bool User_loggedOn
        {
            get => this.Pri_User_loggedOn;
            set
            {
                this.Pri_User_loggedOn = value;
                this.Navigation_Show();
            }
        }

        internal virtual Class_Model Current_Model_Class
        {
            get => this._Current_Model_Class;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                Class_Model.Model_Record_ChangeEventHandler changeEventHandler = new(this.Record_Change_Run);
                var oldModel = this._Current_Model_Class;
                if (oldModel != null)
                    oldModel.Model_Record_Change -= changeEventHandler;
                this._Current_Model_Class = value;
                var newModel = this._Current_Model_Class;
                if (newModel != null)
                    newModel.Model_Record_Change += changeEventHandler;
            }
        }

        internal virtual Timer Site_Check
        {
            get => this._Site_Check;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler((s, e) => this.Site_Check_Elapsed());
                var oldTimer = this._Site_Check;
                if (oldTimer != null)
                    oldTimer.Tick -= eventHandler;
                this._Site_Check = value;
                var newTimer = this._Site_Check;
                if (newTimer != null)
                    newTimer.Tick += eventHandler;
            }
        }

        private void CamBrowser_Load(object sender, EventArgs e)
        {
            try
            {
                this.Navigation_Load();
                this.Manual_Records_Change();
                Class_Record_Manual.Evt_List_Changed += new Class_Record_Manual.Evt_List_ChangedEventHandler(this.Manual_Records_Change);
                this.WV_Site.Source = new Uri(this.WebView_Source);
                this.Token_Timer.Interval = 1000;

                this.CBB_Add.ToolTipText = TXT.TXT_Description("Kanal hinzufügen");
                this.CBB_Back.ToolTipText = TXT.TXT_Description("Klicken zum Zurückkehren");
                this.CBB_Galerie.ToolTipText = TXT.TXT_Description("Galerie öffnen");
                this.CBB_Next.ToolTipText = TXT.TXT_Description("Klicken zum Fortführen");
                this.CBB_Record.ToolTipText = TXT.TXT_Description("Aufnahme");
                this.CBB_Record_Automatik.ToolTipText = TXT.TXT_Description("Automatische Aufnahme");
                this.CBB_Refresh.ToolTipText = TXT.TXT_Description("Aktualisieren");
                this.CBB_Model_Down.ToolTipText = TXT.TXT_Description("Nächstes Model");
                this.CBB_Model_Up.ToolTipText = TXT.TXT_Description("Vorheriges Model");

                this.CMI_Off.Text = TXT.TXT_Description("Alle aus");
                this.CMI_Off.ToolTipText = TXT.TXT_Description("Schaltet alle Listen aus");
                this.CMI_Online.Text = TXT.TXT_Description("Online");
                this.CMI_Online.ToolTipText = TXT.TXT_Description("Blendet alle Online Kanäle ein");
                this.CMI_Records.Text = TXT.TXT_Description("Aufnahmen");
                this.CMI_Records.ToolTipText = TXT.TXT_Description("Alle manuellen Aufnahmen anzeigen");
                this.CMI_Token.Text = TXT.TXT_Description("Token");
                this.CMI_Token.ToolTipText = TXT.TXT_Description("Zeigt die Tokenleiste an");

                this.TOT_Browser.SetToolTip(this.SPE_Intervall_Sekunden, TXT.TXT_Description("Interval in Sekunden"));
                this.TOT_Browser.SetToolTip(this.BTN_Token_Intervall, TXT.TXT_Description("Sendet die Token"));

                this.Site_Check.Interval = 2500;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Load");
            }
        }

        private void CamBrowser_Closed(object sender, EventArgs e)
        {
            try
            {
                this.CamBrowser_Cache_Clear();
                Parameter.FlushMemory();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Closed");
            }
        }

        private void CamBrowser_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (Class_Record_Manual.Manual_Record_List.Count > 0)
                {
                    int count = Class_Record_Manual.Manual_Record_List.Count;
                    var prompt = string.Format(
                        TXT.TXT_Description("Es laufen noch {0} Aufnahmen. Sollen die Aufnahmen beendet werden?"),
                        count);
                    var result = MessageBox.Show(
                        prompt,
                        TXT.TXT_Description("Aufnahmen beenden"),
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            Class_Record_Manual.Stop_Record(Class_Record_Manual.Manual_Record_List[0]);
                        }
                    }
                }

                this.Site_Check.Stop();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Closing");
            }
        }

        private void TIM_Cache_Clear_Tick(object sender, EventArgs e)
        {
            this.CamBrowser_Cache_Clear();
        }

        private async void CamBrowser_Cache_Clear()
        {
            try
            {
                await this.WV_Site.CoreWebView2.Profile?.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.DiskCache);
                Parameter.FlushMemory();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Cache_Clear");
            }
        }

        private void Navigation_Show()
        {
            try
            {
                // Mostrar/ocultar los paneles según las flags y el estado actual
                this.PAN_Manual_Records.Visible = Class_Record_Manual.Manual_Record_List.Count > 0 & this.Pri_Navigation_Records;
                this.PAN_Token.Visible = this.Pri_Navigation_Token & this.User_loggedOn;

                // Comprobar si hay al menos un control online activo
                bool flag = false;
                foreach (Control ctrl in this.PAN_Online.Controls)
                {
                    if (ctrl is Con_User_Online control && control.Pro_Class_Model.Get_Pro_Model_Online())
                    {
                        flag = true;
                        break;
                    }
                }

                this.PAN_Online.Visible = this.Pri_Navigation_Online & flag;
                this.PAN_Right.Visible = (Class_Record_Manual.Manual_Record_List.Count > 0 & this.Pri_Navigation_Records)
                                         | (this.Pri_Navigation_Token & this.User_loggedOn)
                                         | (this.Pri_Navigation_Online & flag);

                this.Model_List_Online_Create();

                // Mostrar u ocultar la navegación de modelos
                bool multipleModels = this.Model_List_Online.Count > 1;
                this.CBS_Model_Navigation.Visible = multipleModels;
                this.CBB_Model_Down.Visible = multipleModels;
                this.CBB_Model_Up.Visible = multipleModels;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Navigation_Show");
            }
        }

        private void Navigation_Load()
        {
            try
            {
                // Leer configuración y actualizar los Check de los ToolStripMenuItem
                this.CMI_Online.Checked = ValueBack.Get_CBoolean((object)IniFile.Read(Parameter.INI_Common, "Browser", "Online", "True"));
                this.CMI_Records.Checked = ValueBack.Get_CBoolean((object)IniFile.Read(Parameter.INI_Common, "Browser", "Records", "True"));
                this.CMI_Token.Checked = ValueBack.Get_CBoolean((object)IniFile.Read(Parameter.INI_Common, "Browser", "Token", "True"));

                // Sincronizar flags internas
                this.Pri_Navigation_Online = this.CMI_Online.Checked;
                this.Pri_Navigation_Records = this.CMI_Records.Checked;
                this.Pri_Navigation_Token = this.CMI_Token.Checked;

                // Desactivar “Off” si todo está desmarcado
                this.CMI_Off.Checked =
                    !this.CMI_Online.Checked
                  & !this.CMI_Records.Checked
                  & !this.CMI_Token.Checked;

                // Refrescar la vista
                this.Navigation_Show();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Navigation_Load");
            }
        }

        private void CMI_Online_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Browser", "Online", this.CMI_Online.Checked.ToString());
            this.Pri_Navigation_Online = this.CMI_Online.Checked;
            this.Online_User_Load();
            this.Navigation_Load();
        }

        private void CMI_Records_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Browser", "Records", this.CMI_Records.Checked.ToString());
            this.Navigation_Load();
        }

        private void CMI_Token_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Browser", "Token", this.CMI_Token.Checked.ToString());
            this.Navigation_Load();
        }

        private void CMI_Off_CheckChanged(object sender, EventArgs e)
        {
            bool enabled = !this.CMI_Off.Checked;
            IniFile.Write(Parameter.INI_Common, "Browser", "Online", enabled.ToString());
            IniFile.Write(Parameter.INI_Common, "Browser", "Records", enabled.ToString());
            IniFile.Write(Parameter.INI_Common, "Browser", "Token", enabled.ToString());
            this.Navigation_Load();
        }

        private async void Site_Check_Elapsed()
        {
            try
            {
                string URL_String = this.WV_Site.Source.AbsoluteUri;
                string html = await this.WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;");
                html = Regex.Unescape(html).Replace("\"", "");

                if (Sites.Site_Is_Galerie(URL_String, html, this.Current_Model_Class))
                {
                    this.CBB_Save.Visible = true;
                }
                else
                {
                    this.CBB_Save.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Site_Check_Elapsed");
                if (ex.HResult != 429)
                    this.Site_Check.Start();
            }
        }

        private void WV_Site_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                this.WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.WV_Site_NavigationCompleted");
            }
        }

        private async void WV_Site_Completed()
        {
            try
            {
                this.Current_URL = this.WV_Site.Source.ToString().Replace("www.", "");
                int IntegerValue;
                (this.Current_Model_Name, IntegerValue) = Sites.Site_Model_URL_Read(this.Current_URL);

                if (IntegerValue == -1)
                {
                    this.Text = nameof(CamBrowser);
                }
                else
                {
                    var site = Sites.Website_Find(ValueBack.Get_CInteger(IntegerValue));
                    this.Text = "CamBrowser - " + site.Pro_Name +
                                (this.Current_Model_Name.Length > 0 ? " - " + this.Current_Model_Name : "");
                }

                if (this.Current_Website_ID != IntegerValue)
                    this.Current_Website_ID = IntegerValue;

                string HTML_String = Regex.Unescape(
                    (await this.WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;"))
                    .Replace("\"", "")
                );

                this.Current_Model_Class = Class_Model_List.Class_Model_Find(
                    ValueBack.Get_CInteger(this.Current_Website_ID),
                    this.Current_Model_Name
                ).Result;

                if (this.Current_Model_Class != null)
                {
                    if (this.Token_Model_GUID != this.Current_Model_Class.Pro_Model_GUID)
                    {
                        this.Token_Count = 0;
                        this.Token_Send = 0;
                    }

                    this.CBB_Record.Enabled = true;
                    this.CBB_Record_Automatik.Enabled = true;
                    this.CBB_Galerie.Enabled = true;
                    this.LAB_Info.Visible = this.Current_Model_Class.Pro_Model_Info.Length > 0;
                    this.TOT_Browser.SetToolTip(this.LAB_Info, this.Current_Model_Class.Pro_Model_Info);

                    this.Current_Model_Class.Set_Pro_Model_Online(
                        false,
                        Sites.Model_Online(this.Current_Model_Name, this.Current_Website_ID, HTML_String) != 0
                    );

                    this.LAB_Token.Text =
                        TXT.TXT_Description("Token") + ": " + this.Token_Send + "\r\n" +
                        TXT.TXT_Description("Anzahl") + ": " + this.Token_Count + "\r\n" +
                        TXT.TXT_Description("Gesamt") + ": " + this.Current_Model_Class.Pro_Model_Token;

                    this.CBB_Add.Visible = false;
                    this.CBB_Record.Visible = true;
                    this.CBB_Record_Automatik.Visible = true;
                    this.CBB_Galerie.Visible = true;
                    this.CBS_Model_Navigation.Visible = true;
                    this.CommandBarSeparator3.Visible = true;
                }
                else
                {
                    this.CBB_Add.Visible = true;
                    this.CBB_Record.Visible = false;
                    this.CBB_Record_Automatik.Visible = false;
                    this.CBB_Galerie.Visible = false;
                    this.CBS_Model_Navigation.Visible = false;
                    this.CommandBarSeparator3.Visible = false;
                    this.Token_Count = 0;
                    this.Token_Send = 0;
                }

                this.LAB_Token.Visible = this.Current_Model_Class != null;
                this.CBB_Back.Enabled = this.WV_Site.CanGoBack;
                this.CBB_Next.Enabled = this.WV_Site.CanGoForward;

                this.Record_Button_Change();
                this.User_loggedOn = Sites.WebSite_IsLogin(this.Current_Website_ID, HTML_String)
                                    && this.Current_Model_Class != null;

                this.Navigation_Show();
                this.TIM_Cache_Clear.Start();
                this.Site_Check.Start();
                this.Online_User_SetCurrent();

                this.CBB_Save.Visible = Sites.Site_Is_Galerie(
                    this.WV_Site.Source.AbsoluteUri,
                    HTML_String,
                    this.Current_Model_Class
                );

                this.WV_Site.PerformLayout();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.WV_Site_Completed");
            }
        }

        private void WV_Site_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            try
            {
                this.Site_Check.Stop();
                this.Current_Model_Name = "";
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.WV_Site_NavigationStarting");
            }
        }

        private void WV_Site_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            try
            {
                this.WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.WV_Site_SourceChanged");
            }
        }

        private void WV_Site_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            try
            {
                if (this.WV_Site.CoreWebView2 == null)
                    return;
                this.WV_Site.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.WV_Site_CoreWebView2InitializationCompleted");
            }
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            try
            {
                Sites.CamBrowser_Open(e.Uri);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CoreWebView2_NewWindowRequested");
            }
        }

        private void Manual_Records_Change()
        {
            try
            {
                // 1) Añadir controles nuevos para cada manualRecord que no tenga ya un control
                foreach (var manualRecord in Class_Record_Manual.Manual_Record_List)
                {
                    bool exists = PAN_Manual_Records.Controls
                        .OfType<Control_Manual_Record>()
                        .Any(ctrl => ctrl.Pro_Record_Stream == manualRecord.Pro_Channel_Stream);

                    if (!exists)
                    {
                        var control = new Control_Manual_Record(manualRecord)
                        {
                            Dock = DockStyle.Top
                        };
                        control.Evt_Record_Close += Manual_Records_Close;
                        PAN_Manual_Records.Controls.Add(control);
                    }
                }

                // 2) Eliminar controles cuyas grabaciones ya no existan
                var toRemove = PAN_Manual_Records.Controls
                    .OfType<Control_Manual_Record>()
                    .Where(ctrl =>
                        Class_Record_Manual.Find_Record(
                            ctrl.Pro_Channel_Name,
                            ctrl.Pro_Record_Stream.Pro_Website_ID) == null)
                    .ToList();

                foreach (var ctrl in toRemove)
                {
                    if (PAN_Manual_Records.InvokeRequired)
                    {
                        PAN_Manual_Records.Invoke((Action)(() =>
                        {
                            PAN_Manual_Records.Controls.Remove(ctrl);
                            ctrl.Dispose();
                        }));
                    }
                    else
                    {
                        PAN_Manual_Records.Controls.Remove(ctrl);
                        ctrl.Dispose();
                    }
                }

                // 3) Actualizar el estado de los botones en el hilo de UI
                if (InvokeRequired)
                    Invoke((Action)Record_Button_Change);
                else
                    Record_Button_Change();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Manual_Records_Change");
            }
        }

        private void Record_Button_Change()
        {
            try
            {
                if (this.Current_Model_Class != null)
                {
                    // Actualiza el icono del botón automático según el estado de grabación
                    if (this.Current_Model_Class.Pro_Model_Stream_Record != null)
                        this.CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                    else
                        this.CBB_Record_Automatik.Image = Resources.RecordAutomatic32;

                    // Actualiza el icono del botón manual según si hay una grabación activa
                    var rec = Class_Record_Manual.Find_Record(
                        this.Current_Model_Class.Pro_Model_Name,
                        this.Current_Model_Class.Pro_Website_ID);

                    if (rec?.Pro_Channel_Stream != null)
                        this.CBB_Record.Image = Resources.control_stop_icon32;
                    else
                        this.CBB_Record.Image = Resources.control_record_icon32;
                }

                this.Navigation_Show();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Record_Button_Change");
            }
        }

        private void Record_Change_Run(bool Record_Run)
        {
            try
            {
                // Cambia el icono del botón automático cuando cambia el estado de grabación
                if (Record_Run)
                    this.CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                else
                    this.CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Record_Change_Run");
            }
        }

        private void Manual_Records_Close(Control_Manual_Record Con)
        {
            // Detiene la grabación asociada al control que se cierra
            Class_Record_Manual.Stop_Record(
                Con.Pro_Channel_Name,
                Con.Pro_Record_Stream.Pro_Website_ID);
        }

        private async void CBB_Galerie_Click(object sender, EventArgs e)
        {
            try
            {
                // Sólo si hay un modelo actual
                if (this.Current_Model_Class == null)
                    return;

                try
                {
                    await Task.Run(() => this.Current_Model_Class.Dialog_Model_View_Show());
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    Parameter.Error_Message(ex, "Control_Stream.Galerie_Open");
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Galerie_Click");
            }
        }

        private void CBB_Back_Click(object sender, EventArgs e)
            => this.WV_Site?.GoBack();

        private void CBB_Next_Click(object sender, EventArgs e)
            => this.WV_Site?.GoForward();

        private void CBB_Refresh_Click(object sender, EventArgs e)
        {
            this.CamBrowser_Cache_Clear();
            this.WV_Site?.Reload();
        }

        private void CBB_Record_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Current_Model_Class != null)
                {
                    var rec = Class_Record_Manual.Find_Record(
                        this.Current_Model_Class.Pro_Model_Name,
                        this.Current_Model_Class.Pro_Website_ID);

                    if (rec?.Pro_Channel_Stream == null)
                    {
                        this.WV_Site.ResetText();
                        Class_Record_Manual.New_Record(
                            this.Current_Model_Class.Pro_Model_Name,
                            this.Current_Model_Class.Pro_Website_ID,
                            new Class_Stream_Record(this.Current_Model_Class));
                    }
                    else
                    {
                        Class_Record_Manual.Stop_Record(
                            this.Current_Model_Class.Pro_Model_Name,
                            this.Current_Model_Class.Pro_Website_ID);
                    }
                }
                this.WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Record_Click");
            }
        }

        private void CBB_Record_Automatik_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Current_Model_Class == null)
                    return;

                this.Current_Model_Class.Pro_Model_Record = !this.Current_Model_Class.Pro_Model_Record;

                if (this.Current_Model_Class.Pro_Model_Stream_Record != null)
                    this.CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                else
                    this.CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Record_Automatik_Click");
            }
        }

        private void CBB_Add_Click(object sender, EventArgs e)
        {
            try
            {
                Form_Main.Chanel_Add(this.WV_Site.Source.ToString());
                this.WV_Site_Completed();
                this.Online_User_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Add_Click");
            }
        }

        private virtual Timer Token_Timer
        {
            get => this._Token_Timer;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler eventHandler = new EventHandler(this.Token_Timer_Elapsed);
                var oldTimer = this._Token_Timer;
                if (oldTimer != null)
                    oldTimer.Tick -= eventHandler;
                this._Token_Timer = value;
                var newTimer = this._Token_Timer;
                if (newTimer != null)
                    newTimer.Tick += eventHandler;
            }
        }

        private void Token_Send_Click(object sender, EventArgs e)
        {
            this.Send_Token(Convert.ToInt32(((Control)sender).Tag));
        }

        private void BTN_Token_Text_Click(object sender, EventArgs e)
        {
            try
            {
                var parts = this.TXB_Intervall_Token.Text.Split('-');
                foreach (var part in parts)
                {
                    int value = ValueBack.Get_CInteger(ValueBack.Get_Zahl_Extract_From_String(part));
                    if (value > 0)
                        this.Token_List_Add(value);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.BTN_Token_Text");
            }
        }

        private async void Send_Token(int Token_Value)
        {
            await Task.CompletedTask;
            try
            {
                if (Token_Value <= 0)
                    return;

                this.WV_Site.Focus();
                SendKeys.SendWait("^(s)" + Token_Value + "{ENTER}");

                this.Token_Send += Token_Value;
                this.Token_Count++;

                if (this.Current_Model_Class != null)
                    this.Current_Model_Class.Pro_Model_Token += Token_Value;

                this.LAB_Token.Text =
                    TXT.TXT_Description("Token") + ": " + this.Token_Send + "\r\n" +
                    TXT.TXT_Description("Anzahl") + ": " + this.Token_Count + "\r\n" +
                    TXT.TXT_Description("Gesamt") + ": " + this.Current_Model_Class.Pro_Model_Token;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Send_Token");
            }
        }

        private void Token_List_Add(int Token_Value)
        {
            try
            {
                this.Token_List.Add(Token_Value);
                this.BTN_Token_Intervall.Text = this.Token_List.Count.ToString();
                this.BTN_Token_Intervall.Visible = this.Token_List.Count > 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Token_List_Add");
            }
        }

        private void Token_Timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                if (this.Token_List.Count > 0)
                {
                    this.Send_Token(this.Token_List[0]);
                    this.Token_List.RemoveAt(0);
                }

                this.BTN_Token_Intervall.Text = this.Token_List.Count.ToString();
                this.BTN_Token_Intervall.Visible = this.Token_List.Count > 0
                                                 || this.TXB_Intervall_Token.Text.Length > 0;

                if (this.Token_List.Count == 0)
                {
                    this.Token_Timer.Stop();
                    this.BTN_Token_Intervall.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Token_Timer_Elapsed");
            }
        }

        private void RadSpinEditor1_ValueChanged(object sender, EventArgs e)
        {
            // Actualiza el intervalo del timer según el valor del NumericUpDown
            this.Token_Timer.Interval = Convert.ToInt32(this.SPE_Intervall_Sekunden.Value * 1000M);
        }

        private void BTN_Token_Intervall_ToggleStateChanged(object sender, EventArgs e)
        {
            try
            {
                // Si se activa el CheckBox y no hay elementos en la lista, llenamos la cola
                if (this.BTN_Token_Intervall.Checked && this.Token_List.Count == 0)
                {
                    var partes = this.TXB_Intervall_Token.Text.Split('-');
                    foreach (var item in partes)
                    {
                        int valor = ValueBack.Get_CInteger(ValueBack.Get_Zahl_Extract_From_String(item));
                        if (valor > 0)
                            this.Token_List_Add(valor);
                    }
                }

                // Si quedó solo un elemento, envíalo inmediatamente y desactiva
                if (this.Token_List.Count == 1)
                {
                    this.Token_Timer_Elapsed(null, EventArgs.Empty);
                    this.BTN_Token_Intervall.Checked = false;
                }
                // Si el CheckBox se desmarca, detenemos el timer y cambiamos el icono a “Start”
                else if (!this.BTN_Token_Intervall.Checked)
                {
                    this.Token_Timer.Stop();
                    this.BTN_Token_Intervall.Image = Resources.Start24;
                }
                // Si el CheckBox está marcado y hay más de un token, enviamos uno y arrancamos el timer
                else
                {
                    this.Token_Timer_Elapsed(null, EventArgs.Empty);
                    if (this.Token_List.Count > 1)
                        this.Token_Timer.Start();
                    this.BTN_Token_Intervall.Image =Resources.Pause24;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.BTN_Token_Intervall_ToggleStateChanged");
            }
        }

        private void TXB_Intervall_Token_TextChanging(object sender, EventArgs e)
        {
            try
            {
                this.Token_List.Clear();
                var parts = this.TXB_Intervall_Token.Text.Split('-');
                int TruePart = 0;
                int sum = 0;
                foreach (var part in parts)
                {
                    if (int.TryParse(part, out int val) && val > 0)
                    {
                        TruePart++;
                        sum += val;
                    }
                }
                this.SPE_Intervall_Sekunden.Visible = TruePart > 1;
                this.BTN_Token_Intervall.Visible = TruePart > 0 || this.Token_List.Count > 0;
                this.BTN_Token_Intervall.Text = TruePart > 1 ? TruePart.ToString() : string.Empty;
                this.TOT_Browser.SetToolTip(
                    this.BTN_Token_Intervall,
                    $"{TXT.TXT_Description("Sendet die Token")} {TXT.TXT_Description("Gesamt")}: {sum}"
                );
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.TXB_Intervall_Token_TextChanging");
            }
        }

        private void Online_User_SetCurrent()
        {
            try
            {
                var url = (this.Current_URL ?? string.Empty).ToLower();
                foreach (Con_User_Online control in this.PAN_Online.Controls.OfType<Con_User_Online>())
                {
                    control.Pro_Is_Current = url.Contains(control.Pro_Class_Model.Pro_Model_Name.ToLower());
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Online_User_SetCurrent");
            }
        }

        private void Online_User_Load()
        {
            try
            {
                if (!Pri_Navigation_Online)
                    return;

                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    if (Current_Website_ID != model.Pro_Website_ID)
                        continue;

                    bool exists = false;
                    foreach (Control ctrl in PAN_Online.Controls)
                    {
                        if (ctrl is Con_User_Online cuo && cuo.Pro_Class_Model == model)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        var conUserOnline = new Con_User_Online
                        {
                            Pro_Class_Model = model,
                            Dock = DockStyle.Top,
                            Visible = model.Get_Pro_Model_Online()
                        };
                        conUserOnline.MouseMove += Online_User_MouseMove;
                        conUserOnline.MouseLeave += Online_User_Mouse_Leave;
                        PAN_Online.Controls.Add(conUserOnline);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Online_User_Load");
            }
        }

        private void Online_User_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var conUserOnline = (Con_User_Online)sender;
                Control_Model_Info1.Pro_Model = conUserOnline.Pro_Class_Model;

                // calculamos X justo a la izquierda del panel derecho
                int x = PAN_Right.Location.X - Control_Model_Info1.Width;

                // convertimos la posición del control a coordenadas cliente del formulario
                var screenPt = conUserOnline.PointToScreen(Point.Empty);
                var clientPt = PointToClient(screenPt);

                // posición preferida: debajo del control, si cabe; si no, arriba
                int yBelow = clientPt.Y + conUserOnline.Height + 20;
                int yAbove = clientPt.Y - Control_Model_Info1.Height - 20;
                int y = (yBelow + Control_Model_Info1.Height > ClientSize.Height)
                        ? Math.Max(0, yAbove)
                        : yBelow;

                Control_Model_Info1.Location = new Point(x, y);
                Control_Model_Info1.Control_Visible = true;
                Control_Model_Info1.BringToFront();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Online_User_MouseMove");
            }
        }

        private void Online_User_Mouse_Leave(object sender, EventArgs e)
        {
            // Oculta el panel de información al dejar de pasar el ratón
            this.Control_Model_Info1.Pro_Model_Preview = null;
            this.Control_Model_Info1.Pro_Model_GUID = Guid.Empty;
            this.Control_Model_Info1.Pro_Model_Info = "";
            this.Control_Model_Info1.Control_Visible = false;
        }

        private void CBB_Model_Up_Click(object sender, EventArgs e)
        {
            try
            {
                this.Model_List_Online_Create();
                if (this.Model_List_Online.Count == 0)
                    return;

                // Encuentra la posición actual en la lista
                int index = this.Model_List_Online.IndexOf(this.Current_Model_Class.Pro_Model_GUID.ToString());
                if (index == -1)
                    index = 0;

                // Calcula el siguiente índice (o vuelve al inicio)
                int nextIndex = (index < this.Model_List_Online.Count - 1) ? index + 1 : 0;
                string nextGuid = this.Model_List_Online[nextIndex];

                // Obtiene el modelo correspondiente y carga su URL
                var result = Class_Model_List
                    .Class_Model_Find(ValueBack.Get_CUnique((object)nextGuid))
                    .Result;
                var website = Sites.Website_Find(result.Pro_Website_ID);
                if (website == null)
                    return;

                this.WebView_Source = string.Format(website.Pro_Model_URL, result.Pro_Model_Name);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Model_Up_Click");
            }
        }

        private void CBB_Model_Down_Click(object sender, EventArgs e)
        {
            try
            {
                this.Model_List_Online_Create();
                if (this.Model_List_Online.Count == 0)
                    return;

                int index = this.Model_List_Online.IndexOf(this.Current_Model_Class?.Pro_Model_GUID.ToString());
                if (index == -1)
                    index = 0;

                int prevIndex = (index > 0)
                    ? index - 1
                    : this.Model_List_Online.Count - 1;

                string guid = this.Model_List_Online[prevIndex];
                var result = Class_Model_List
                    .Class_Model_Find(ValueBack.Get_CUnique((object)guid))
                    .Result;
                var website = Sites.Website_Find(result.Pro_Website_ID);
                if (website == null)
                    return;

                this.WebView_Source = string.Format(website.Pro_Model_URL, result.Pro_Model_Name);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Model_Down_Click");
            }
        }

        private void Model_List_Online_Create()
        {
            try
            {
                this.Model_List_Online.Clear();
                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    if (this.Current_Website_ID == model.Pro_Website_ID
                        && model.Get_Pro_Model_Online()
                        && model.Timer_Online_Change.BGW_Result == 1)
                    {
                        this.Model_List_Online.Add(model.Pro_Model_GUID.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Model_List_Online_Create");
            }
        }

        private void LAB_Info_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.Current_Model_Class == null)
                    return;

                using (var dialogModelInfo = new Dialog_Model_Info())
                {
                    dialogModelInfo.TXB_Memo.Text = this.Current_Model_Class.Pro_Model_Info;
                    dialogModelInfo.StartPosition = FormStartPosition.CenterParent;
                    if (dialogModelInfo.ShowDialog() == DialogResult.OK)
                    {
                        this.Current_Model_Class.Pro_Model_Info = dialogModelInfo.TXB_Memo.Text;
                        this.Current_Model_Class.Model_Online_Changed();
                        this.TOT_Browser.SetToolTip(this.LAB_Info, this.Current_Model_Class.Pro_Model_Info);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.LAB_Info_DoubleClick");
            }
        }

        private async void CBB_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string html = await this.WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;");
                html = Regex.Unescape(html).Replace("\"", "");
                Sites.Download_Galerie_Movie(
                    this.WV_Site.Source.AbsoluteUri,
                    html,
                    this.Current_Model_Class
                );
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CBB_Save_Click");
            }
        }
    }
}
