using Microsoft.Web.WebView2.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XstreaMonNET8
{
    partial class CamBrowser
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
        private ToolStrip RadCommandBar1;
        private ToolStripSeparator CommandBarSeparator1;
        private ToolStripSeparator CommandBarSeparator5;
        private ToolStripDropDownButton CBB_Navigation;
        private ToolStripSeparator RadMenuSeparatorItem1;
        private ToolStripSeparator CommandBarSeparator2;
        private ToolStripSeparator CommandBarSeparator3;
        private ToolStripSeparator CBS_Model_Navigation;
        private Panel PAN_Token;
        private Label LAB_Token;
        private Panel PAN_Token_Text;
        private Panel RadPanel3;
        private System.Windows.Forms.ToolTip TOT_Browser;
        private Panel PAN_Right;
        private Panel PAN_Online;
        private Panel PAN_Manual_Records;
        private Control_Model_Info Control_Model_Info1;
        private Class_Model _Current_Model_Class;
        private System.Windows.Forms.Timer _Site_Check;
        private System.Windows.Forms.TextBox _TXB_Intervall_Token;
        private System.Windows.Forms.ToolStripButton _CBB_Save;
        private System.Windows.Forms.ToolStripLabel _LAB_Info;
        private System.Windows.Forms.ToolStripButton _CBB_Model_Down;
        private System.Windows.Forms.ToolStripButton _CBB_Model_Up;
        private System.Windows.Forms.CheckBox _BTN_Token_Intervall;
        private System.Windows.Forms.NumericUpDown _SPE_Intervall_Sekunden;
        private System.Windows.Forms.Timer _Token_Timer;
        private System.Windows.Forms.Button _BTN_Token_25;
        private System.Windows.Forms.Button _BTN_Token_15;
        private System.Windows.Forms.Button _BTN_Token_10;
        private System.Windows.Forms.Button _BTN_Token_5;
        private System.Windows.Forms.Button _BTN_Token_2;
        private System.Windows.Forms.Button _BTN_Token_1;
        private System.Windows.Forms.Button _BTN_Token_50;
        private System.Windows.Forms.ToolStripButton _CBB_Add;
        private System.Windows.Forms.ToolStripButton _CBB_Record_Automatik;
        private System.Windows.Forms.ToolStripButton _CBB_Record;
        private System.Windows.Forms.ToolStripButton _CBB_Refresh;
        private System.Windows.Forms.ToolStripButton _CBB_Next;
        private System.Windows.Forms.ToolStripButton _CBB_Back;
        private System.Windows.Forms.ToolStripButton _CBB_Galerie;
        private Microsoft.Web.WebView2.WinForms.WebView2 _WV_Site;
        private System.Windows.Forms.ToolStripMenuItem _CMI_Off;
        private System.Windows.Forms.ToolStripMenuItem _CMI_Token;
        private System.Windows.Forms.ToolStripMenuItem _CMI_Records;
        private System.Windows.Forms.ToolStripMenuItem _CMI_Online;
        private System.Windows.Forms.Timer _TIM_Cache_Clear;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamBrowser));

            // RadCommandBar1 (reemplazado por ToolStrip)
            this.RadCommandBar1 = new System.Windows.Forms.ToolStrip();
            this.LAB_Info = new System.Windows.Forms.ToolStripLabel();
            this.CBB_Back = new System.Windows.Forms.ToolStripButton();
            this.CBB_Next = new System.Windows.Forms.ToolStripButton();
            this.CommandBarSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_Refresh = new System.Windows.Forms.ToolStripButton();
            this.CommandBarSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_Navigation = new System.Windows.Forms.ToolStripDropDownButton();
            this.CMI_Records = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Token = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Online = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Off = new System.Windows.Forms.ToolStripMenuItem();
            this.CommandBarSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_Add = new System.Windows.Forms.ToolStripButton();
            this.CBB_Galerie = new System.Windows.Forms.ToolStripButton();
            this.CBB_Save = new System.Windows.Forms.ToolStripButton();
            this.CommandBarSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_Record_Automatik = new System.Windows.Forms.ToolStripButton();
            this.CBB_Record = new System.Windows.Forms.ToolStripButton();
            this.CBS_Model_Navigation = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_Model_Up = new System.Windows.Forms.ToolStripButton();
            this.CBB_Model_Down = new System.Windows.Forms.ToolStripButton();

            // PAN_Token y controles relacionados
            this.PAN_Token = new System.Windows.Forms.Panel();
            this.LAB_Token = new System.Windows.Forms.Label();
            this.PAN_Token_Text = new System.Windows.Forms.Panel();
            this.RadPanel3 = new System.Windows.Forms.Panel();
            this.BTN_Token_Intervall = new System.Windows.Forms.CheckBox();
            this.SPE_Intervall_Sekunden = new System.Windows.Forms.NumericUpDown();
            this.TXB_Intervall_Token = new System.Windows.Forms.TextBox();
            this.BTN_Token_1 = new System.Windows.Forms.Button();
            this.BTN_Token_2 = new System.Windows.Forms.Button();
            this.BTN_Token_5 = new System.Windows.Forms.Button();
            this.BTN_Token_10 = new System.Windows.Forms.Button();
            this.BTN_Token_15 = new System.Windows.Forms.Button();
            this.BTN_Token_25 = new System.Windows.Forms.Button();
            this.BTN_Token_50 = new System.Windows.Forms.Button();

            this.TOT_Browser = new System.Windows.Forms.ToolTip(this.components);
            this.PAN_Right = new System.Windows.Forms.Panel();
            this.PAN_Online = new System.Windows.Forms.Panel();
            this.PAN_Manual_Records = new System.Windows.Forms.Panel();
            this.TIM_Cache_Clear = new System.Windows.Forms.Timer(this.components);
            this.Control_Model_Info1 = new Control_Model_Info();
            this.WV_Site = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.Site_Check = new System.Windows.Forms.Timer(this.components);
            this.Token_Timer = new System.Windows.Forms.Timer(this.components);

            this.RadCommandBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CBB_Back,
                this.CBB_Next,
                this.CommandBarSeparator1,
                this.CBB_Refresh,
                this.CommandBarSeparator5,
                this.CBB_Navigation,
                this.CommandBarSeparator2,
                this.CBB_Add,
                this.CBB_Galerie,
                this.CBB_Save,
                this.CommandBarSeparator3,
                this.CBB_Record_Automatik,
                this.CBB_Record,
                this.CBS_Model_Navigation,
                this.CBB_Model_Up,
                this.CBB_Model_Down,
                new ToolStripSeparator(),
                this.LAB_Info
            });
            this.RadCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.RadCommandBar1.Name = "RadCommandBar1";
            this.RadCommandBar1.Size = new System.Drawing.Size(1358, 40);
            this.RadCommandBar1.TabIndex = 0;
            this.RadCommandBar1.Text = "RadCommandBar1";

            // Configuración de botones y controles
            this.LAB_Info.AutoSize = false;
            this.LAB_Info.BackColor = System.Drawing.Color.Transparent;
            this.LAB_Info.Image = ((System.Drawing.Image)(resources.GetObject("LAB_Info.Image")));
            this.LAB_Info.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LAB_Info.Name = "LAB_Info";
            this.LAB_Info.Size = new System.Drawing.Size(39, 40);
            this.LAB_Info.Visible = false;

            // Configurar otros controles de manera similar...
            // (Aquí iría la configuración detallada de cada control)

            // PAN_Right
            this.PAN_Right.Controls.Add(this.PAN_Online);
            this.PAN_Right.Controls.Add(this.PAN_Token);
            this.PAN_Right.Controls.Add(this.PAN_Manual_Records);
            this.PAN_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.PAN_Right.Location = new System.Drawing.Point(1266, 40);
            this.PAN_Right.Name = "PAN_Right";
            this.PAN_Right.Size = new System.Drawing.Size(92, 732);
            this.PAN_Right.TabIndex = 3;

            // WV_Site
            this.WV_Site.AllowExternalDrop = true;
            this.WV_Site.CreationProperties = null;
            this.WV_Site.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WV_Site.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WV_Site.Location = new System.Drawing.Point(0, 40);
            this.WV_Site.Name = "WV_Site";
            this.WV_Site.Size = new System.Drawing.Size(1266, 732);
            this.WV_Site.TabIndex = 5;
            this.WV_Site.ZoomFactor = 1D;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 772);
            this.Controls.Add(this.WV_Site);
            this.Controls.Add(this.Control_Model_Info1);
            this.Controls.Add(this.PAN_Right);
            this.Controls.Add(this.RadCommandBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CamBrowser";
            this.Text = "CamBrowser";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        internal virtual System.Windows.Forms.Timer TIM_Cache_Clear
        {
            get => _TIM_Cache_Clear;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TIM_Cache_Clear != null)
                    _TIM_Cache_Clear.Tick -= TIM_Cache_Clear_Tick;
                _TIM_Cache_Clear = value;
                if (_TIM_Cache_Clear != null)
                    _TIM_Cache_Clear.Tick += TIM_Cache_Clear_Tick;
            }
        }

        internal virtual System.Windows.Forms.ToolStripMenuItem CMI_Online
        {
            get => _CMI_Online;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CMI_Online != null)
                    _CMI_Online.Click -= CMI_Online_CheckChanged;
                _CMI_Online = value;
                if (_CMI_Online != null)
                    _CMI_Online.Click += CMI_Online_CheckChanged;
            }
        }

        internal virtual System.Windows.Forms.ToolStripMenuItem CMI_Records
        {
            get => _CMI_Records;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CMI_Records != null)
                    _CMI_Records.Click -= CMI_Records_CheckChanged;
                _CMI_Records = value;
                if (_CMI_Records != null)
                    _CMI_Records.Click += CMI_Records_CheckChanged;
            }
        }

        internal virtual System.Windows.Forms.ToolStripMenuItem CMI_Token
        {
            get => _CMI_Token;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CMI_Token != null)
                    _CMI_Token.Click -= CMI_Token_CheckChanged;
                _CMI_Token = value;
                if (_CMI_Token != null)
                    _CMI_Token.Click += CMI_Token_CheckChanged;
            }
        }

        internal virtual System.Windows.Forms.ToolStripMenuItem CMI_Off
        {
            get => _CMI_Off;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CMI_Off != null)
                    _CMI_Off.Click -= CMI_Off_CheckChanged;
                _CMI_Off = value;
                if (_CMI_Off != null)
                    _CMI_Off.Click += CMI_Off_CheckChanged;
            }
        }

        internal virtual Microsoft.Web.WebView2.WinForms.WebView2 WV_Site
        {
            get => _WV_Site;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler<CoreWebView2NavigationCompletedEventArgs> navCompletedHandler = WV_Site_NavigationCompleted;
                EventHandler<CoreWebView2NavigationStartingEventArgs> navStartingHandler = WV_Site_NavigationStarting;
                EventHandler<CoreWebView2SourceChangedEventArgs> sourceChangedHandler = WV_Site_SourceChanged;
                EventHandler<CoreWebView2InitializationCompletedEventArgs> initCompletedHandler = WV_Site_CoreWebView2InitializationCompleted;

                if (_WV_Site != null)
                {
                    _WV_Site.NavigationCompleted -= navCompletedHandler;
                    _WV_Site.NavigationStarting -= navStartingHandler;
                    _WV_Site.SourceChanged -= sourceChangedHandler;
                    _WV_Site.CoreWebView2InitializationCompleted -= initCompletedHandler;
                }

                _WV_Site = value;
                if (_WV_Site != null)
                {
                    _WV_Site.NavigationCompleted += navCompletedHandler;
                    _WV_Site.NavigationStarting += navStartingHandler;
                    _WV_Site.SourceChanged += sourceChangedHandler;
                    _WV_Site.CoreWebView2InitializationCompleted += initCompletedHandler;
                }
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Galerie
        {
            get => _CBB_Galerie;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Galerie != null)
                    _CBB_Galerie.Click -= CBB_Galerie_Click;
                _CBB_Galerie = value;
                if (_CBB_Galerie != null)
                    _CBB_Galerie.Click += CBB_Galerie_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Back
        {
            get => _CBB_Back;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Back != null)
                    _CBB_Back.Click -= CBB_Back_Click;
                _CBB_Back = value;
                if (_CBB_Back != null)
                    _CBB_Back.Click += CBB_Back_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Next
        {
            get => _CBB_Next;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Next != null)
                    _CBB_Next.Click -= CBB_Next_Click;
                _CBB_Next = value;
                if (_CBB_Next != null)
                    _CBB_Next.Click += CBB_Next_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Refresh
        {
            get => _CBB_Refresh;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Refresh != null)
                    _CBB_Refresh.Click -= CBB_Refresh_Click;
                _CBB_Refresh = value;
                if (_CBB_Refresh != null)
                    _CBB_Refresh.Click += CBB_Refresh_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Record
        {
            get => _CBB_Record;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Record != null)
                    _CBB_Record.Click -= CBB_Record_Click;
                _CBB_Record = value;
                if (_CBB_Record != null)
                    _CBB_Record.Click += CBB_Record_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Record_Automatik
        {
            get => _CBB_Record_Automatik;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Record_Automatik != null)
                    _CBB_Record_Automatik.Click -= CBB_Record_Automatik_Click;
                _CBB_Record_Automatik = value;
                if (_CBB_Record_Automatik != null)
                    _CBB_Record_Automatik.Click += CBB_Record_Automatik_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Add
        {
            get => _CBB_Add;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Add != null)
                    _CBB_Add.Click -= CBB_Add_Click;
                _CBB_Add = value;
                if (_CBB_Add != null)
                    _CBB_Add.Click += CBB_Add_Click;
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_50
        {
            get => _BTN_Token_50;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_50 != null)
                    _BTN_Token_50.Click -= Token_Send_Click;
                _BTN_Token_50 = value;
                if (_BTN_Token_50 != null)
                    _BTN_Token_50.Click += Token_Send_Click;
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_1
        {
            get => _BTN_Token_1;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_1 != null)
                    _BTN_Token_1.Click -= Token_Send_Click;
                _BTN_Token_1 = value;
                if (_BTN_Token_1 != null)
                    _BTN_Token_1.Click += Token_Send_Click;
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_2
        {
            get => _BTN_Token_2;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_2 != null)
                    _BTN_Token_2.Click -= Token_Send_Click;
                _BTN_Token_2 = value;
                if (_BTN_Token_2 != null)
                    _BTN_Token_2.Click += Token_Send_Click;
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_5
        {
            get => _BTN_Token_5;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_5 != null)
                    _BTN_Token_5.Click -= Token_Send_Click;
                _BTN_Token_5 = value;
                if (_BTN_Token_5 != null)
                    _BTN_Token_5.Click += Token_Send_Click;
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_10
        {
            get => _BTN_Token_10;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_10 != null)
                    _BTN_Token_10.Click -= Token_Send_Click;
                _BTN_Token_10 = value;
                if (_BTN_Token_10 != null)
                    _BTN_Token_10.Click += Token_Send_Click;
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_15
        {
            get => _BTN_Token_15;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_15 != null)
                    _BTN_Token_15.Click -= Token_Send_Click;
                _BTN_Token_15 = value;
                if (_BTN_Token_15 != null)
                    _BTN_Token_15.Click += Token_Send_Click;
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_25
        {
            get => _BTN_Token_25;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_25 != null)
                    _BTN_Token_25.Click -= Token_Send_Click;
                _BTN_Token_25 = value;
                if (_BTN_Token_25 != null)
                    _BTN_Token_25.Click += Token_Send_Click;
            }
        }

        private System.Windows.Forms.Timer Token_Timer
        {
            get => _Token_Timer;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Token_Timer != null)
                    _Token_Timer.Tick -= Token_Timer_Elapsed;
                _Token_Timer = value;
                if (_Token_Timer != null)
                    _Token_Timer.Tick += Token_Timer_Elapsed;
            }
        }

        internal virtual System.Windows.Forms.NumericUpDown SPE_Intervall_Sekunden
        {
            get => _SPE_Intervall_Sekunden;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SPE_Intervall_Sekunden != null)
                    _SPE_Intervall_Sekunden.ValueChanged -= RadSpinEditor1_ValueChanged;
                _SPE_Intervall_Sekunden = value;
                if (_SPE_Intervall_Sekunden != null)
                    _SPE_Intervall_Sekunden.ValueChanged += RadSpinEditor1_ValueChanged;
            }
        }

        internal virtual System.Windows.Forms.CheckBox BTN_Token_Intervall
        {
            get => _BTN_Token_Intervall;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_Intervall != null)
                    _BTN_Token_Intervall.CheckedChanged -= BTN_Token_Intervall_CheckedChanged;
                _BTN_Token_Intervall = value;
                if (_BTN_Token_Intervall != null)
                    _BTN_Token_Intervall.CheckedChanged += BTN_Token_Intervall_CheckedChanged;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Model_Up
        {
            get => _CBB_Model_Up;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Model_Up != null)
                    _CBB_Model_Up.Click -= CBB_Model_Up_Click;
                _CBB_Model_Up = value;
                if (_CBB_Model_Up != null)
                    _CBB_Model_Up.Click += CBB_Model_Up_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Model_Down
        {
            get => _CBB_Model_Down;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Model_Down != null)
                    _CBB_Model_Down.Click -= CBB_Model_Down_Click;
                _CBB_Model_Down = value;
                if (_CBB_Model_Down != null)
                    _CBB_Model_Down.Click += CBB_Model_Down_Click;
            }
        }

        internal virtual System.Windows.Forms.ToolStripLabel LAB_Info
        {
            get => _LAB_Info;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_LAB_Info != null)
                    _LAB_Info.DoubleClick -= LAB_Info_DoubleClick;
                _LAB_Info = value;
                if (_LAB_Info != null)
                    _LAB_Info.DoubleClick += LAB_Info_DoubleClick;
            }
        }

        internal virtual System.Windows.Forms.TextBox TXB_Intervall_Token
        {
            get => _TXB_Intervall_Token;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TXB_Intervall_Token != null)
                    _TXB_Intervall_Token.TextChanged -= TXB_Intervall_Token_TextChanged;
                _TXB_Intervall_Token = value;
                if (_TXB_Intervall_Token != null)
                    _TXB_Intervall_Token.TextChanged += TXB_Intervall_Token_TextChanged;
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Save
        {
            get => _CBB_Save;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Save != null)
                    _CBB_Save.Click -= CBB_Save_Click;
                _CBB_Save = value;
                if (_CBB_Save != null)
                    _CBB_Save.Click += CBB_Save_Click;
            }
        }

        private System.Windows.Forms.Timer Site_Check
        {
            get => _Site_Check;
            set
            {
                if (_Site_Check != null)
                    _Site_Check.Tick -= Site_Check_Elapsed;
                _Site_Check = value;
                if (_Site_Check != null)
                    _Site_Check.Tick += Site_Check_Elapsed;
            }
        }
    }
}