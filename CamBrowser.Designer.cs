using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using Timer = System.Windows.Forms.Timer;

namespace XstreaMonNET8
{
    public partial class CamBrowser : Form
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

        internal virtual System.Windows.Forms.Timer TIM_Cache_Clear
        {
            get => _TIM_Cache_Clear;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TIM_Cache_Clear != null)
                    _TIM_Cache_Clear.Tick -= TIM_Cache_Clear_Tick;  // desengancha del timer anterior

                _TIM_Cache_Clear = value;

                if (_TIM_Cache_Clear != null)
                    _TIM_Cache_Clear.Tick += TIM_Cache_Clear_Tick;  // engancha al nuevo timer
            }
        }

        internal virtual System.Windows.Forms.ToolStripMenuItem CMI_Online
        {
            get => _CMI_Online;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CMI_Online != null)
                    _CMI_Online.Click -= CMI_Online_CheckChanged;   // desengancha manejador anterior

                _CMI_Online = value;

                if (_CMI_Online != null)
                    _CMI_Online.Click += CMI_Online_CheckChanged;   // engancha manejador al nuevo ítem
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
                    _CMI_Off.Click -= CMI_Off_CheckChanged;   // desengancha manejador anterior

                _CMI_Off = value;

                if (_CMI_Off != null)
                    _CMI_Off.Click += CMI_Off_CheckChanged;   // engancha manejador al nuevo ítem
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
                    _CBB_Galerie.Click -= CBB_Galerie_Click;  // desengancha del botón anterior

                _CBB_Galerie = value;

                if (_CBB_Galerie != null)
                    _CBB_Galerie.Click += CBB_Galerie_Click;  // engancha al nuevo botón
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Back
        {
            get => _CBB_Back;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Back != null)
                    _CBB_Back.Click -= CBB_Back_Click;   // desengancha manejador anterior

                _CBB_Back = value;

                if (_CBB_Back != null)
                    _CBB_Back.Click += CBB_Back_Click;   // engancha manejador al nuevo botón
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Next
        {
            get => _CBB_Next;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Next != null)
                    _CBB_Next.Click -= CBB_Next_Click;   // desengancha del botón anterior

                _CBB_Next = value;

                if (_CBB_Next != null)
                    _CBB_Next.Click += CBB_Next_Click;   // engancha al nuevo botón
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Refresh
        {
            get => _CBB_Refresh;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Refresh != null)
                    _CBB_Refresh.Click -= CBB_Refresh_Click;   // desengancha del botón anterior

                _CBB_Refresh = value;

                if (_CBB_Refresh != null)
                    _CBB_Refresh.Click += CBB_Refresh_Click;   // engancha al nuevo botón
            }
        }


        internal virtual System.Windows.Forms.ToolStripButton CBB_Record
        {
            get => _CBB_Record;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Record != null)
                    _CBB_Record.Click -= CBB_Record_Click;   // desengancha del botón anterior

                _CBB_Record = value;

                if (_CBB_Record != null)
                    _CBB_Record.Click += CBB_Record_Click;   // engancha al nuevo botón
            }
        }

        internal virtual System.Windows.Forms.ToolStripButton CBB_Record_Automatik
        {
            get => _CBB_Record_Automatik;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Record_Automatik != null)
                    _CBB_Record_Automatik.Click -= CBB_Record_Automatik_Click;  // desengancha

                _CBB_Record_Automatik = value;

                if (_CBB_Record_Automatik != null)
                    _CBB_Record_Automatik.Click += CBB_Record_Automatik_Click;  // engancha
            }
        }


        internal virtual System.Windows.Forms.ToolStripButton CBB_Add
        {
            get => _CBB_Add;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Add != null)
                    _CBB_Add.Click -= CBB_Add_Click;   // desengancha del botón anterior

                _CBB_Add = value;

                if (_CBB_Add != null)
                    _CBB_Add.Click += CBB_Add_Click;   // engancha al nuevo botón
            }
        }


        internal virtual System.Windows.Forms.Button BTN_Token_50
        {
            get => _BTN_Token_50;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                // Desengancha el manejador del botón anterior, si existe
                if (_BTN_Token_50 != null)
                    _BTN_Token_50.Click -= Token_Send_Click;

                _BTN_Token_50 = value;

                // Engancha el manejador al nuevo botón, si no es null
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
                    _BTN_Token_1.Click -= Token_Send_Click;   // desengancha manejador anterior

                _BTN_Token_1 = value;

                if (_BTN_Token_1 != null)
                    _BTN_Token_1.Click += Token_Send_Click;   // engancha manejador al nuevo botón
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_2
        {
            get => _BTN_Token_2;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_2 != null)
                    _BTN_Token_2.Click -= Token_Send_Click;   // quita manejador anterior

                _BTN_Token_2 = value;

                if (_BTN_Token_2 != null)
                    _BTN_Token_2.Click += Token_Send_Click;   // añade manejador al nuevo botón
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_5
        {
            get => _BTN_Token_5;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_5 != null)
                    _BTN_Token_5.Click -= Token_Send_Click;   // desengancha manejador anterior

                _BTN_Token_5 = value;

                if (_BTN_Token_5 != null)
                    _BTN_Token_5.Click += Token_Send_Click;   // engancha manejador al nuevo botón
            }
        }

        internal virtual System.Windows.Forms.Button BTN_Token_10
        {
            get => _BTN_Token_10;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_10 != null)
                    _BTN_Token_10.Click -= Token_Send_Click;   // desengancha el anterior

                _BTN_Token_10 = value;

                if (_BTN_Token_10 != null)
                    _BTN_Token_10.Click += Token_Send_Click;   // engancha el nuevo
            }
        }


        internal virtual System.Windows.Forms.Button BTN_Token_15
        {
            get => _BTN_Token_15;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_15 != null)
                    _BTN_Token_15.Click -= Token_Send_Click;   // quita manejador

                _BTN_Token_15 = value;

                if (_BTN_Token_15 != null)
                    _BTN_Token_15.Click += Token_Send_Click;   // pone manejador
            }
        }


        internal virtual System.Windows.Forms.Button BTN_Token_25
        {
            get => _BTN_Token_25;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BTN_Token_25 != null)
                    _BTN_Token_25.Click -= Token_Send_Click;   // desengancha

                _BTN_Token_25 = value;

                if (_BTN_Token_25 != null)
                    _BTN_Token_25.Click += Token_Send_Click;   // engancha
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
                    _SPE_Intervall_Sekunden.ValueChanged -= RadSpinEditor1_ValueChanged; // quita

                _SPE_Intervall_Sekunden = value;

                if (_SPE_Intervall_Sekunden != null)
                    _SPE_Intervall_Sekunden.ValueChanged += RadSpinEditor1_ValueChanged; // añade
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
                    _CBB_Model_Up.Click -= CBB_Model_Up_Click;   // desconecta evento

                _CBB_Model_Up = value;

                if (_CBB_Model_Up != null)
                    _CBB_Model_Up.Click += CBB_Model_Up_Click;   // conecta evento
            }
        }


        internal virtual System.Windows.Forms.ToolStripButton CBB_Model_Down
        {
            get => _CBB_Model_Down;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CBB_Model_Down != null)
                    _CBB_Model_Down.Click -= CBB_Model_Down_Click;   // desengancha

                _CBB_Model_Down = value;

                if (_CBB_Model_Down != null)
                    _CBB_Model_Down.Click += CBB_Model_Down_Click;   // engancha
            }
        }

        internal virtual System.Windows.Forms.ToolStripLabel LAB_Info
        {
            get => _LAB_Info;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_LAB_Info != null)
                    _LAB_Info.DoubleClick -= LAB_Info_DoubleClick;   // desconecta

                _LAB_Info = value;

                if (_LAB_Info != null)
                    _LAB_Info.DoubleClick += LAB_Info_DoubleClick;   // conecta
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

        public CamBrowser()
        {
            InitializeComponent();

            Pri_Website_ID = -1;
            Pri_User_loggedOn = false;
            Site_Check = new Timer();
            Site_Hash = "";
            Pri_Navigation_Online = true;
            Pri_Navigation_Records = true;
            Pri_Navigation_Token = true;
            Pri_Last_Check_URL = "";
            Token_List = new List<int>();
            Token_Timer = new Timer();
            Model_List_Online = new List<string>();

            this.Load += CamBrowser_Load;
            this.FormClosed += CamBrowser_Closed;
            this.FormClosing += CamBrowser_Closing;
        }

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

        // Propiedades
        internal string WebView_Source
        {
            get { return Pri_WebView_Source; }
            set
            {
                if (value != null)
                {
                    WV_Site.Source = new Uri(value);
                    Pri_WebView_Source = value;
                }
            }
        }

        private int Current_Website_ID
        {
            get { return Pri_Website_ID; }
            set
            {
                if (Pri_Website_ID != value && value > -1)
                {
                    Pri_Website_ID = value;
                    Online_User_Load();
                }
                else
                {
                    Pri_Website_ID = value;
                }
            }
        }

        private bool User_loggedOn
        {
            get { return Pri_User_loggedOn; }
            set
            {
                Pri_User_loggedOn = value;
                Navigation_Show();
            }
        }

        private Class_Model Current_Model_Class
        {
            get { return _Current_Model_Class; }
            set
            {
                if (_Current_Model_Class != null)
                {
                    _Current_Model_Class.Model_Record_Change -= Record_Change_Run;
                }

                _Current_Model_Class = value;

                if (_Current_Model_Class != null)
                {
                    _Current_Model_Class.Model_Record_Change += Record_Change_Run;
                }
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

        // Métodos de eventos
        private void CamBrowser_Load(object sender, EventArgs e)
        {
            try
            {
                Navigation_Load();
                Manual_Records_Change();
                Class_Record_Manual.Evt_List_Changed += Manual_Records_Change;
                WV_Site.Source = new Uri(WebView_Source);
                Token_Timer.Interval = 1000;

                this.TOT_Browser = new System.Windows.Forms.ToolTip();

                ((ToolStrip)CBB_Add.Owner).ShowItemToolTips = true;

                CBB_Add.ToolTipText = "Kanal hinzufügen";
                CBB_Back.ToolTipText = "Klicken zum Zurückkehren";
                CBB_Galerie.ToolTipText = "Galerie öffnen";
                CBB_Next.ToolTipText = "Klicken zum Fortführen";
                CBB_Record.ToolTipText = "Aufnahme";
                CBB_Record_Automatik.ToolTipText = "Automatische Aufnahme";
                CBB_Refresh.ToolTipText = "Aktualisieren";
                CBB_Model_Down.ToolTipText = "Nächstes Model";
                CBB_Model_Up.ToolTipText = "Vorheriges Model";

                CMI_Off.Text = "Alle aus";
                CMI_Off.ToolTipText = "Schaltet alle Listen aus";
                CMI_Online.Text = "Online";
                CMI_Online.ToolTipText = "Blendet alle Online Kanäle ein";
                CMI_Records.Text = "Aufnahmen";
                CMI_Records.ToolTipText = "Alle manuellen Aufnahmen anzeigen";
                CMI_Token.Text = "Token";
                CMI_Token.ToolTipText = "Zeigt die Tokenleiste an";

                TOT_Browser.SetToolTip(SPE_Intervall_Sekunden, "Interval in Sekunden");
                TOT_Browser.SetToolTip(BTN_Token_Intervall, "Sendet die Token");

                Site_Check.Interval = 2500;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error en CamBrowser_Load: " + ex.Message);
            }
        }

        private void CamBrowser_Closed(object sender, EventArgs e)
        {
            try
            {
                CamBrowser_Cache_Clear();
                // Parameter.FlushMemory(); // Comentado ya que no está definido en C#
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CamBrowser_Closed: " + ex.Message);
            }
        }

        private void CamBrowser_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (Class_Record_Manual.Manual_Record_List.Count > 0)
                {
                    var result = MessageBox.Show(
                        $"Es laufen noch {Class_Record_Manual.Manual_Record_List.Count} Aufnahmen. Sollen die Aufnahmen beendet werden?",
                        "Aufnahmen beenden",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                        case DialogResult.Yes:
                            for (int i = 0; i < Class_Record_Manual.Manual_Record_List.Count; i++)
                            {
                                Class_Record_Manual.Stop_Record(Class_Record_Manual.Manual_Record_List[0]);
                            }
                            break;
                    }
                }
                Site_Check.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CamBrowser_Closing: " + ex.Message);
            }
        }

        private void TIM_Cache_Clear_Tick(object sender, EventArgs e)
        {
            CamBrowser_Cache_Clear();
        }

        private async void CamBrowser_Cache_Clear()
        {
            try
            {
                if (WV_Site.CoreWebView2?.Profile != null)
                {
                    await WV_Site.CoreWebView2.Profile.ClearBrowsingDataAsync();
                }
                // Parameter.FlushMemory(); // Comentado ya que no está definido en C#
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CamBrowser_Cache_Clear: " + ex.Message);
            }
        }

        private void Navigation_Show()
        {
            try
            {
                PAN_Manual_Records.Visible = Class_Record_Manual.Manual_Record_List.Count > 0 && Pri_Navigation_Records;
                PAN_Token.Visible = Pri_Navigation_Token && User_loggedOn;

                bool flag = false;
                foreach (Control control in PAN_Online.Controls)
                {
                    if (control is Con_User_Online userOnline && userOnline.Pro_Class_Model.Get_Pro_Model_Online())
                    {
                        flag = true;
                        break;
                    }
                }

                PAN_Online.Visible = Pri_Navigation_Online && flag;
                PAN_Right.Visible = (Class_Record_Manual.Manual_Record_List.Count > 0 && Pri_Navigation_Records) ||
                                    (Pri_Navigation_Token && User_loggedOn) ||
                                    (Pri_Navigation_Online && flag);

                Model_List_Online_Create();

                if (Model_List_Online.Count > 1)
                {
                    CBS_Model_Navigation.Visible = true;
                    CBB_Model_Down.Visible = true;
                    CBB_Model_Up.Visible = true;
                }
                else
                {
                    CBS_Model_Navigation.Visible = false;
                    CBB_Model_Down.Visible = false;
                    CBB_Model_Up.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Navigation_Show: " + ex.Message);
            }
        }

        private void Navigation_Load()
        {
            try
            {
                // Cargar configuraciones desde INI
                // Nota: Se asume que IniFile.Read está disponible
                CMI_Online.Checked = Convert.ToBoolean(IniFile.Read("Common", "Browser", "Online", "True"));
                CMI_Records.Checked = Convert.ToBoolean(IniFile.Read("Common", "Browser", "Records", "True"));
                CMI_Token.Checked = Convert.ToBoolean(IniFile.Read("Common", "Browser", "Token", "True"));

                Pri_Navigation_Online = CMI_Online.Checked;
                Pri_Navigation_Records = CMI_Records.Checked;
                Pri_Navigation_Token = CMI_Token.Checked;

                CMI_Off.Checked = !CMI_Online.Checked && !CMI_Records.Checked && !CMI_Token.Checked;
                Navigation_Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Navigation_Load: " + ex.Message);
            }
        }

        private void CMI_Online_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Online", CMI_Online.Checked.ToString());
            Pri_Navigation_Online = CMI_Online.Checked;
            Online_User_Load();
            Navigation_Load();
        }

        private void CMI_Records_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Records", CMI_Records.Checked.ToString());
            Navigation_Load();
        }

        private void CMI_Token_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Token", CMI_Token.Checked.ToString());
            Navigation_Load();
        }

        private void CMI_Off_CheckChanged(object sender, EventArgs e)
        {
            IniFile.Write("Common", "Browser", "Online", (!CMI_Off.Checked).ToString());
            IniFile.Write("Common", "Browser", "Records", (!CMI_Off.Checked).ToString());
            IniFile.Write("Common", "Browser", "Token", (!CMI_Off.Checked).ToString());
            Navigation_Load();
        }

        private async void Site_Check_Elapsed(object sender, EventArgs e)
        {
            try
            {
                string url = WV_Site.Source.AbsoluteUri;
                string html = await WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;");
                html = Regex.Unescape(html).Replace("\"", "");

                CBB_Save.Visible = Sites.Site_Is_Galerie(url, html, Current_Model_Class);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Site_Check_Elapsed: {ex.Message}");

                // si no es E_ACCESSDENIED, vuelve a arrancar el timer
                if (ex.HResult != -2147024891)
                    Site_Check.Start();
            }
        }

        private void WV_Site_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_NavigationCompleted: " + ex.Message);
            }
        }

        private async void WV_Site_Completed()
        {
            try
            {
                Current_URL = WV_Site.Source.ToString().Replace("www.", "");
                var (modelName, websiteId) = Sites.Site_Model_URL_Read(Current_URL);
                Current_Model_Name = modelName;

                if (websiteId == -1)
                {
                    Text = "CamBrowser";
                }
                else
                {
                    var website = Sites.Website_Find(websiteId);
                    Text = $"CamBrowser - {website.Pro_Name}" +
                          (Current_Model_Name.Length > 0 ? $" - {Current_Model_Name}" : "");
                }

                if (Current_Website_ID != websiteId)
                {
                    Current_Website_ID = websiteId;
                }

                string html = await WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;");
                html = Regex.Unescape(html).Replace("\"", "");

                Current_Model_Class = Class_Model_List.Class_Model_Find(Current_Website_ID, Current_Model_Name).Result;

                if (Current_Model_Class != null)
                {
                    if (Token_Model_GUID != Current_Model_Class.Pro_Model_GUID)
                    {
                        Token_Count = 0;
                        Token_Send = 0;
                    }

                    CBB_Record.Enabled = true;
                    CBB_Record_Automatik.Enabled = true;
                    CBB_Galerie.Enabled = true;

                    LAB_Info.Visible = Current_Model_Class.Pro_Model_Info.Length > 0;
                    ((ToolStrip)LAB_Info.Owner).ShowItemToolTips = true;
                    LAB_Info.ToolTipText = Current_Model_Class.Pro_Model_Info;

                    bool isOnline = Sites.Model_Online(Current_Model_Name, Current_Website_ID, html) != 0;
                    Current_Model_Class.Set_Pro_Model_Online(false, isOnline);

                    LAB_Token.Text = $"Token: {Token_Send}\r\nAnzahl: {Token_Count}\r\nGesamt: {Current_Model_Class.Pro_Model_Token}";

                    CBB_Add.Visible = false;
                    CBB_Record.Visible = true;
                    CBB_Record_Automatik.Visible = true;
                    CBB_Galerie.Visible = true;
                    CBS_Model_Navigation.Visible = true;
                    CommandBarSeparator3.Visible = true;
                }
                else
                {
                    CBB_Add.Visible = true;
                    CBB_Record.Visible = false;
                    CBB_Record_Automatik.Visible = false;
                    CBB_Galerie.Visible = false;
                    CBS_Model_Navigation.Visible = false;
                    CommandBarSeparator3.Visible = false;

                    Token_Count = 0;
                    Token_Send = 0;
                }

                LAB_Token.Visible = Current_Model_Class != null;
                CBB_Back.Enabled = WV_Site.CanGoBack;
                CBB_Next.Enabled = WV_Site.CanGoForward;

                Record_Button_Change();
                User_loggedOn = Sites.WebSite_IsLogin(Current_Website_ID, html) && Current_Model_Class != null;
                Navigation_Show();

                TIM_Cache_Clear.Start();
                Site_Check.Start();
                Online_User_SetCurrent();

                if (Sites.Site_Is_Galerie(WV_Site.Source.AbsoluteUri, html, Current_Model_Class))
                {
                    CBB_Save.Visible = true;
                }
                else
                {
                    CBB_Save.Visible = false;
                }

                WV_Site.PerformLayout();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_Completed: " + ex.Message);
            }
        }

        private void WV_Site_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            try
            {
                Site_Check.Stop();
                Current_Model_Name = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_NavigationStarting: " + ex.Message);
            }
        }

        private void WV_Site_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            try
            {
                WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_SourceChanged: " + ex.Message);
            }
        }

        private void WV_Site_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            try
            {
                if (WV_Site.CoreWebView2 != null)
                {
                    WV_Site.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en WV_Site_CoreWebView2InitializationCompleted: " + ex.Message);
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
                Console.WriteLine("Error en CoreWebView2_NewWindowRequested: " + ex.Message);
            }
        }

        private void Manual_Records_Change()
        {
            try
            {
                // Agregar nuevos registros manuales
                foreach (var manualRecord in Class_Record_Manual.Manual_Record_List)
                {
                    bool exists = false;
                    foreach (Control control in PAN_Manual_Records.Controls)
                    {
                        if (control is Control_Manual_Record recordControl &&
                            recordControl.Pro_Record_Stream == manualRecord.Pro_Channel_Stream)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        var newRecordControl = new Control_Manual_Record(manualRecord)
                        {
                            Dock = DockStyle.Top
                        };

                        newRecordControl.Evt_Record_Close += Manual_Records_Close;
                        PAN_Manual_Records.Controls.Add(newRecordControl);
                    }
                }

                // Eliminar controles de registros que ya no existen
                for (int i = PAN_Manual_Records.Controls.Count - 1; i >= 0; i--)
                {
                    if (PAN_Manual_Records.Controls[i] is Control_Manual_Record control)
                    {
                        if (Class_Record_Manual.Find_Record(control.Pro_Channel_Name, control.Pro_Record_Stream.Pro_Website_ID) == null)
                        {
                            if (PAN_Manual_Records.InvokeRequired)
                            {
                                PAN_Manual_Records.Invoke((MethodInvoker)delegate
                                {
                                    PAN_Manual_Records.Controls.Remove(control);
                                    control.Dispose();
                                });
                            }
                            else
                            {
                                PAN_Manual_Records.Controls.Remove(control);
                                control.Dispose();
                            }
                        }
                    }
                }

                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { Record_Button_Change(); });
                }
                else
                {
                    Record_Button_Change();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Manual_Records_Change: " + ex.Message);
            }
        }

        private void Record_Button_Change()
        {
            try
            {
                if (Current_Model_Class != null)
                {
                    if (Current_Model_Class.Pro_Model_Stream_Record != null)
                    {
                        CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                    }
                    else
                    {
                        CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
                    }

                    var record = Class_Record_Manual.Find_Record(Current_Model_Class.Pro_Model_Name, Current_Model_Class.Pro_Website_ID);
                    if (record?.Pro_Channel_Stream != null)
                    {
                        CBB_Record.Image = Resources.control_stop_icon32;
                    }
                    else
                    {
                        CBB_Record.Image = Resources.control_record_icon32;
                    }
                }

                Navigation_Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Record_Button_Change: " + ex.Message);
            }
        }

        private void Record_Change_Run(bool Record_Run)
        {
            try
            {
                if (Record_Run)
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                }
                else
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Record_Change_Run: " + ex.Message);
            }
        }

        private void Manual_Records_Close(Control_Manual_Record Con)
        {
            Class_Record_Manual.Stop_Record(Con.Pro_Channel_Name, Con.Pro_Record_Stream.Pro_Website_ID);
        }

        private async void CBB_Galerie_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current_Model_Class == null)
                    return;

                try
                {
                    await Task.Run(() => Current_Model_Class.Dialog_Model_View_Show());
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    Console.WriteLine("Error en CBB_Galerie_Click: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Galerie_Click: " + ex.Message);
            }
        }

        private void CBB_Back_Click(object sender, EventArgs e)
        {
            WV_Site?.GoBack();
        }

        private void CBB_Next_Click(object sender, EventArgs e)
        {
            WV_Site?.GoForward();
        }

        private void CBB_Refresh_Click(object sender, EventArgs e)
        {
            CamBrowser_Cache_Clear();
            WV_Site?.Reload();
        }

        private void CBB_Record_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current_Model_Class != null)
                {
                    var record = Class_Record_Manual.Find_Record(Current_Model_Class.Pro_Model_Name, Current_Model_Class.Pro_Website_ID);

                    if (record?.Pro_Channel_Stream == null)
                    {
                        WV_Site.ResetText();
                        Class_Record_Manual.New_Record(
                            Current_Model_Class.Pro_Model_Name,
                            Current_Model_Class.Pro_Website_ID,
                            new Class_Stream_Record(Current_Model_Class));
                    }
                    else
                    {
                        Class_Record_Manual.Stop_Record(Current_Model_Class.Pro_Model_Name, Current_Model_Class.Pro_Website_ID);
                    }
                }

                WV_Site_Completed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Record_Click: " + ex.Message);
            }
        }

        private void CBB_Record_Automatik_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current_Model_Class == null)
                    return;

                Current_Model_Class.Pro_Model_Record = !Current_Model_Class.Pro_Model_Record;

                if (Current_Model_Class.Pro_Model_Stream_Record != null)
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatikStop32;
                }
                else
                {
                    CBB_Record_Automatik.Image = Resources.RecordAutomatic32;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Record_Automatik_Click: " + ex.Message);
            }
        }

        private void CBB_Add_Click(object sender, EventArgs e)
        {
            try
            {
                Form_Main.Instance.Chanel_Add(WV_Site.Source.ToString());
                WV_Site_Completed();
                Online_User_Load();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CBB_Add_Click: " + ex.Message);
            }
        }

        private void Token_Send_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Button button && button.Tag != null)
            {
                int tokenValue = Convert.ToInt32(button.Tag);
                Send_Token(tokenValue);
            }
        }

        private void BTN_Token_Text_Click(object sender, EventArgs e)
        {
            try
            {
                string[] parts = TXB_Intervall_Token.Text.Split('-');
                foreach (string part in parts)
                {
                    int tokenValue;
                    if (int.TryParse(part, out tokenValue) && tokenValue > 0)
                    {
                        Token_List_Add(tokenValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BTN_Token_Text_Click: " + ex.Message);
            }
        }

        private async void Send_Token(int Token_Value)
        {
            await Task.CompletedTask;
            try
            {
                if (Token_Value <= 0)
                    return;

                WV_Site.Focus();
                SendKeys.Send("^(s)" + Token_Value + "{ENTER}");

                Token_Send += Token_Value;
                Token_Count++;

                if (Current_Model_Class != null)
                {
                    Current_Model_Class.Pro_Model_Token += Token_Value;
                }

                LAB_Token.Text = $"Token: {Token_Send}\r\nAnzahl: {Token_Count}\r\nGesamt: {Current_Model_Class?.Pro_Model_Token ?? 0}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Send_Token: " + ex.Message);
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

        private void Token_Timer_Elapsed(object Sender, EventArgs e)
        {
            try
            {
                if (this.Token_List.Count > 0)
                {
                    this.Send_Token(this.Token_List[0]);
                    this.Token_List.RemoveAt(0);
                }

                this.BTN_Token_Intervall.Text = this.Token_List.Count.ToString();
                this.BTN_Token_Intervall.Visible = this.Token_List.Count > 0 ||
                                                   this.TXB_Intervall_Token.Text.Length > 0;

                if (this.Token_List.Count == 0)
                {
                    this.Token_Timer.Stop();
                    this.BTN_Token_Intervall.CheckState = CheckState.Unchecked;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.Token_Timer_Elapsed");
            }
        }

        private void RadSpinEditor1_ValueChanged(object sender, EventArgs e)
        {
            this.Token_Timer.Interval =
                Convert.ToInt32(this.SPE_Intervall_Sekunden.Value * 1000M);
        }

        private void BTN_Token_Intervall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isOn = BTN_Token_Intervall.Checked;   // estado actual

                // ---- Al marcar, llena la lista de token si está vacía ----
                if (isOn && this.Token_List.Count == 0)
                {
                    foreach (var item in this.TXB_Intervall_Token.Text.Split('-'))
                    {
                        int val = ValueBack.Get_CInteger(
                                      ValueBack.Get_Zahl_Extract_From_String(item));
                        if (val > 0) this.Token_List_Add(val);
                    }
                }

                // ---- Lógica de envío / pausa ----
                if (this.Token_List.Count == 1)
                {
                    this.Token_Timer_Elapsed(null, null);
                    BTN_Token_Intervall.Checked = false;           // se desactiva solo
                }
                else if (!isOn)                                    // botón desmarcado
                {
                    this.Token_Timer.Stop();
                    this.BTN_Token_Intervall.Image = Resources.Start24;
                }
                else                                               // botón marcado
                {
                    this.Token_Timer_Elapsed(null, null);
                    if (this.Token_List.Count > 1) this.Token_Timer.Start();
                    this.BTN_Token_Intervall.Image = Resources.Pause24;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex,
                    "CamBrowser.BTN_Token_Intervall_CheckedChanged");
            }
        }


        private void TXB_Intervall_Token_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int TruePart = 0;
                this.Token_List.Clear();
                int num = 0;

                string newValue = TXB_Intervall_Token.Text;

                foreach (var valStr in newValue.Split('-'))
                {
                    int val = ValueBack.Get_CInteger(valStr);
                    if (val > 0)
                    {
                        TruePart++;
                        num += val;
                    }
                }

                this.SPE_Intervall_Sekunden.Visible = TruePart > 1;
                this.BTN_Token_Intervall.Visible = TruePart > 0 || this.Token_List.Count > 0;
                this.BTN_Token_Intervall.Text = TruePart > 1 ? TruePart.ToString() : string.Empty;

                this.TOT_Browser.SetToolTip(
                    this.BTN_Token_Intervall,
                    $"{TXT.TXT_Description("Sendet die Token")} {TXT.TXT_Description("Gesamt")}: {num}");
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.TXB_Intervall_Token_TextChanged");
            }
        }

        private void Online_User_SetCurrent()
        {
            try
            {
                foreach (Con_User_Online control in this.PAN_Online.Controls)
                {
                    bool isCurrent = (this.Current_URL ?? string.Empty)
                                     .ToLower()
                                     .Contains(control.Pro_Class_Model.Pro_Model_Name.ToLower());
                    control.Pro_Is_Current = isCurrent;
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
                if (!this.Pri_Navigation_Online) return;

                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    if (this.Current_Website_ID != model.Pro_Website_ID) continue;

                    bool exists = false;
                    foreach (Con_User_Online c in this.PAN_Online.Controls)
                    {
                        if (c.Pro_Class_Model == model) { exists = true; break; }
                    }

                    if (!exists)
                    {
                        var ctrl = new Con_User_Online
                        {
                            Pro_Class_Model = model,
                            Dock = DockStyle.Top,
                            Visible = model.Get_Pro_Model_Online()
                        };
                        ctrl.MouseMove += this.Online_User_MouseMove;
                        ctrl.MouseLeave += this.Online_User_Mouse_Leave;
                        this.PAN_Online.Controls.Add(ctrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Cache_Clear");
            }
        }

        private void Online_User_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var u = (Con_User_Online)sender;
                this.Control_Model_Info1.Pro_Model = u.Pro_Class_Model;

                int x = this.PAN_Right.Left - this.Control_Model_Info1.Width;
                int y = this.PointToScreen(u.Location).Y + u.Parent.Location.Y + 20;
                if (y + this.Control_Model_Info1.Height > this.Height)
                    y = this.Height - this.Control_Model_Info1.Height;

                this.Control_Model_Info1.Location = new Point(x, y);
                this.Control_Model_Info1.Control_Visible = true;
                this.Control_Model_Info1.BringToFront();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "CamBrowser.CamBrowser_Cache_Clear");
            }
        }

        private void Online_User_Mouse_Leave(object sender, EventArgs e)
        {
            this.Control_Model_Info1.Pro_Model_Preview = null;
            this.Control_Model_Info1.Pro_Model_GUID = Guid.Empty;
            this.Control_Model_Info1.Pro_Model_Info = string.Empty;
            this.Control_Model_Info1.Control_Visible = false;
        }

        private void CBB_Model_Up_Click(object sender, EventArgs e)
        {
            try
            {
                this.Model_List_Online_Create();
                if (this.Model_List_Online.Count == 0) return;

                int idx = this.Model_List_Online
                              .IndexOf(this.Current_Model_Class.Pro_Model_GUID.ToString());
                if (idx == -1) idx = 0;

                var guid = this.Model_List_Online[idx != this.Model_List_Online.Count - 1 ? idx + 1 : 0];
                var model = Class_Model_List.Class_Model_Find(ValueBack.Get_CUnique(guid)).Result;
                var site = Sites.Website_Find(model.Pro_Website_ID);
                if (site == null) return;

                this.WebView_Source = string.Format(site.Pro_Model_URL, model.Pro_Model_Name);
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
                if (this.Model_List_Online.Count == 0) return;

                int idx = this.Model_List_Online
                              .IndexOf(this.Current_Model_Class?.Pro_Model_GUID.ToString());
                if (idx == -1) idx = 0;

                var guid = this.Model_List_Online[idx != 0 ? idx - 1 : this.Model_List_Online.Count - 1];
                var model = Class_Model_List.Class_Model_Find(ValueBack.Get_CUnique(guid)).Result;
                var site = Sites.Website_Find(model.Pro_Website_ID);
                if (site == null) return;

                this.WebView_Source = string.Format(site.Pro_Model_URL, model.Pro_Model_Name);
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

                foreach (Class_Model m in Class_Model_List.Model_List)
                {
                    if (this.Current_Website_ID == m.Pro_Website_ID &&
                        m.Get_Pro_Model_Online() &&
                        m.Timer_Online_Change.BGW_Result == 1)
                    {
                        this.Model_List_Online.Add(m.Pro_Model_GUID.ToString());
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
                if (this.Current_Model_Class == null) return;

                Dialog_Model_Info dlg = new Dialog_Model_Info
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                dlg.TXB_Memo.Text = this.Current_Model_Class.Pro_Model_Info;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Current_Model_Class.Pro_Model_Info = dlg.TXB_Memo.Text;
                    this.Current_Model_Class.Model_Online_Changed();

                    ((ToolStrip)LAB_Info.Owner).ShowItemToolTips = true;
                    LAB_Info.ToolTipText = this.Current_Model_Class.Pro_Model_Info;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Info_Click");
            }
        }

        private async void CBB_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string html = Regex.Unescape(
                    await this.WV_Site.ExecuteScriptAsync("document.documentElement.outerHTML;"))
                    .Replace("\"", "");

                Sites.Download_Galerie_Movie(
                    this.WV_Site.Source.AbsoluteUri, html, this.Current_Model_Class);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Info_Click");
            }
        }
    }

}