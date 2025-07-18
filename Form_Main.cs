using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace XstreaMonNET8
{
    public partial class Form_Main : Form
    {
        internal static Class_Driveinfo Drive_Info;
        private bool Pri_Show_All;
        private bool Pri_Show_Visible;
        private bool Pri_Data_Load;
        private Font Pri_Font_Favorite_Deaktiv;
        private Font Pri_Font_Favorite_Aktiv;
        private Font Pri_Font_Aktiv;
        private Font Pri_Font_Deaktiv;
        private Guid Show_Model_Info_GUID;

        public Form_Main()
        {
            Load += new EventHandler(Form_Load);
            Closing += new CancelEventHandler(Form_Closing);
            Resize += new EventHandler(Mybase_Resize);
            SizeChanged += new EventHandler(Form_Main_SizeChanged);
            Drive_Info_Refresh_Timer = new System.Windows.Forms.Timer()
            {
                Interval = 300000
            };
            Pri_Show_All = false;
            Pri_Show_Visible = true;
            Pri_Data_Load = false;
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        internal virtual CustomFlowLayoutPanel PAN_Show { get; set; }
        internal virtual DataGridView GRV_Model_Kanal
        {
            get => _GRV_Model_Kanal;
            set
            {
                if (_GRV_Model_Kanal != null)
                {
                    _GRV_Model_Kanal.DoubleClick -= GRV_Model_Kanal_DoubleClick;
                    _GRV_Model_Kanal.CellPainting -= GRV_Model_Kanal_CellPaint;
                    // _GRV_Model_Kanal.GroupSummaryEvaluate -= GRV_Model_Kanal_GroupSummaryEvaluate; // No direct equivalent in DataGridView
                    _GRV_Model_Kanal.MouseDown -= GRV_Model_Kanal_MouseDown;
                    _GRV_Model_Kanal.CellContextMenuStripNeeded -= GRV_Model_Kanal_ContextMenuOpening;
                    _GRV_Model_Kanal.MouseMove -= GRV_Model_Kanal_MouseMove;
                    _GRV_Model_Kanal.MouseLeave -= GRV_Model_Kanal_MouseLeave;
                }
                _GRV_Model_Kanal = value;
                if (_GRV_Model_Kanal != null)
                {
                    _GRV_Model_Kanal.DoubleClick += GRV_Model_Kanal_DoubleClick;
                    _GRV_Model_Kanal.CellPainting += GRV_Model_Kanal_CellPaint;
                    // _GRV_Model_Kanal.GroupSummaryEvaluate += GRV_Model_Kanal_GroupSummaryEvaluate;
                    _GRV_Model_Kanal.MouseDown += GRV_Model_Kanal_MouseDown;
                    _GRV_Model_Kanal.CellContextMenuStripNeeded += GRV_Model_Kanal_ContextMenuOpening;
                    _GRV_Model_Kanal.MouseMove += GRV_Model_Kanal_MouseMove;
                    _GRV_Model_Kanal.MouseLeave += GRV_Model_Kanal_MouseLeave;
                }
            }
        }
        private DataGridView _GRV_Model_Kanal;

        internal virtual Panel RadPanel1 { get; set; }
        internal virtual SplitContainer RadSplitContainer1 { get; set; }
        internal virtual SplitterPanel PAN_Navigation { get; set; }
        internal virtual SplitterPanel PAN_Streams { get; set; }
        // RadContextMenuManager1 has no direct equivalent, context menus are handled by ContextMenuStrip
        internal virtual ContextMenuStrip CME_Model_Kanal
        {
            get => _CME_Model_Kanal;
            set
            {
                if (_CME_Model_Kanal != null)
                {
                    _CME_Model_Kanal.Opening -= CME_Model_Kanal_DropDownOpening;
                }
                _CME_Model_Kanal = value;
                if (_CME_Model_Kanal != null)
                {
                    _CME_Model_Kanal.Opening += CME_Model_Kanal_DropDownOpening;
                }
            }
        }
        private ContextMenuStrip _CME_Model_Kanal;

        internal virtual ToolStripMenuItem CMI_Aufnahme
        {
            get => _CMI_Aufnahme;
            set
            {
                if (_CMI_Aufnahme != null)
                {
                    _CMI_Aufnahme.Click -= CMI_Aufnahme_Click;
                }
                _CMI_Aufnahme = value;
                if (_CMI_Aufnahme != null)
                {
                    _CMI_Aufnahme.Click += CMI_Aufnahme_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Aufnahme;

        internal virtual ToolStripSeparator RadMenuSeparatorItem1 { get; set; }

        internal virtual ToolStripMenuItem CMI_Webseite
        {
            get => _CMI_Webseite;
            set
            {
                if (_CMI_Webseite != null)
                {
                    _CMI_Webseite.Click -= CMI_Webseite_Click;
                }
                _CMI_Webseite = value;
                if (_CMI_Webseite != null)
                {
                    _CMI_Webseite.Click += CMI_Webseite_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Webseite;

        internal virtual ToolStripMenuItem CMI_Galerie
        {
            get => _CMI_Galerie;
            set
            {
                if (_CMI_Galerie != null)
                {
                    _CMI_Galerie.Click -= CMI_Galerie_Click;
                }
                _CMI_Galerie = value;
                if (_CMI_Galerie != null)
                {
                    _CMI_Galerie.Click += CMI_Galerie_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Galerie;

        internal virtual CustomFlowLayoutPanel PAN_Record { get; set; }

        internal virtual ToolStripMenuItem CMI_Anzeigen
        {
            get => _CMI_Anzeigen;
            set
            {
                if (_CMI_Anzeigen != null)
                {
                    _CMI_Anzeigen.Click -= CMI_Anzeigen_Click;
                }
                _CMI_Anzeigen = value;
                if (_CMI_Anzeigen != null)
                {
                    _CMI_Anzeigen.Click += CMI_Anzeigen_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Anzeigen;

        internal virtual ToolStripMenuItem CMI_Optionen
        {
            get => _CMI_Optionen;
            set
            {
                if (_CMI_Optionen != null)
                {
                    _CMI_Optionen.Click -= CMI_Optionen_Click;
                }
                _CMI_Optionen = value;
                if (_CMI_Optionen != null)
                {
                    _CMI_Optionen.Click += CMI_Optionen_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Optionen;

        internal virtual ToolStripSeparator RadMenuSeparatorItem2 { get; set; }

        internal virtual ToolStripMenuItem CMI_Favorite
        {
            get => _CMI_Favorite;
            set
            {
                if (_CMI_Favorite != null)
                {
                    _CMI_Favorite.Click -= CMI_Favorite_Click;
                }
                _CMI_Favorite = value;
                if (_CMI_Favorite != null)
                {
                    _CMI_Favorite.Click += CMI_Favorite_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Favorite;

        internal virtual ToolStripMenuItem CMI_Info
        {
            get => _CMI_Info;
            set
            {
                if (_CMI_Info != null)
                {
                    _CMI_Info.Click -= CMI_Info_Click;
                }
                _CMI_Info = value;
                if (_CMI_Info != null)
                {
                    _CMI_Info.Click += CMI_Info_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Info;

        internal virtual Control_Model_Info Control_Model_Info1 { get; set; }

        internal virtual ToolStripMenuItem CMI_Filter { get; set; }
        internal virtual ToolStripLabel CMH_Status { get; set; } // Changed from RadMenuHeaderItem to ToolStripLabel for native equivalent

        internal virtual ToolStripMenuItem CMI_Online
        {
            get => _CMI_Online;
            set
            {
                if (_CMI_Online != null)
                {
                    _CMI_Online.Click -= CMI_Online_Click;
                }
                _CMI_Online = value;
                if (_CMI_Online != null)
                {
                    _CMI_Online.Click += CMI_Online_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Online;

        internal virtual ToolStripMenuItem CMI_Offline
        {
            get => _CMI_Offline;
            set
            {
                if (_CMI_Offline != null)
                {
                    _CMI_Offline.Click -= CMI_Offline_Click;
                }
                _CMI_Offline = value;
                if (_CMI_Offline != null)
                {
                    _CMI_Offline.Click += CMI_Offline_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Offline;

        internal virtual ToolStripMenuItem CMI_Record
        {
            get => _CMI_Record;
            set
            {
                if (_CMI_Record != null)
                {
                    _CMI_Record.Click -= CMI_Record_Click;
                }
                _CMI_Record = value;
                if (_CMI_Record != null)
                {
                    _CMI_Record.Click += CMI_Record_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Record;

        internal virtual ToolStripLabel CMH_Gender { get; set; } // Changed from RadMenuHeaderItem to ToolStripLabel for native equivalent

        internal virtual ToolStripMenuItem CMI_Female
        {
            get => _CMI_Female;
            set
            {
                if (_CMI_Female != null)
                {
                    _CMI_Female.Click -= CMI_Female_Click;
                }
                _CMI_Female = value;
                if (_CMI_Female != null)
                {
                    _CMI_Female.Click += CMI_Female_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Female;

        internal virtual ToolStripMenuItem CMI_Male
        {
            get => _CMI_Male;
            set
            {
                if (_CMI_Male != null)
                {
                    _CMI_Male.Click -= CMI_Male_Click;
                }
                _CMI_Male = value;
                if (_CMI_Male != null)
                {
                    _CMI_Male.Click += CMI_Male_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Male;

        internal virtual ToolStripMenuItem CMI_Couple
        {
            get => _CMI_Couple;
            set
            {
                if (_CMI_Couple != null)
                {
                    _CMI_Couple.Click -= CMI_Couple_Click;
                }
                _CMI_Couple = value;
                if (_CMI_Couple != null)
                {
                    _CMI_Couple.Click += CMI_Couple_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Couple;

        internal virtual ToolStripMenuItem CMI_Trans
        {
            get => _CMI_Trans;
            set
            {
                if (_CMI_Trans != null)
                {
                    _CMI_Trans.Click -= CMI_Trans_Click;
                }
                _CMI_Trans = value;
                if (_CMI_Trans != null)
                {
                    _CMI_Trans.Click += CMI_Trans_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Trans;

        internal virtual ToolStripMenuItem CMI_Unknow
        {
            get => _CMI_Unknow;
            set
            {
                if (_CMI_Unknow != null)
                {
                    _CMI_Unknow.Click -= CMI_Unknow_Click;
                }
                _CMI_Unknow = value;
                if (_CMI_Unknow != null)
                {
                    _CMI_Unknow.Click += CMI_Unknow_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Unknow;

        internal virtual ToolStripMenuItem CMI_Online_Check
        {
            get => _CMI_Online_Check;
            set
            {
                if (_CMI_Online_Check != null)
                {
                    _CMI_Online_Check.Click -= CMI_Online_Check_Click;
                }
                _CMI_Online_Check = value;
                if (_CMI_Online_Check != null)
                {
                    _CMI_Online_Check.Click += CMI_Online_Check_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Online_Check;

        internal virtual ToolTip ToolTip1 { get; set; }
        // FluentDarkTheme1 has no direct native equivalent, styling would be custom

        internal virtual ToolStripMenuItem CMI_AutoRecord
        {
            get => _CMI_AutoRecord;
            set
            {
                if (_CMI_AutoRecord != null)
                {
                    _CMI_AutoRecord.Click -= CMI_AutoRecord_Click;
                }
                _CMI_AutoRecord = value;
                if (_CMI_AutoRecord != null)
                {
                    _CMI_AutoRecord.Click += CMI_AutoRecord_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_AutoRecord;

        internal virtual ToolStripSeparator RadMenuSeparatorItem3 { get; set; }

        internal virtual ToolStripMenuItem CMI_Alle_Anzeigen
        {
            get => _CMI_Alle_Anzeigen;
            set
            {
                if (_CMI_Alle_Anzeigen != null)
                {
                    _CMI_Alle_Anzeigen.Click -= CMI_Alle_Anzeigen_Click;
                }
                _CMI_Alle_Anzeigen = value;
                if (_CMI_Alle_Anzeigen != null)
                {
                    _CMI_Alle_Anzeigen.Click += CMI_Alle_Anzeigen_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Alle_Anzeigen;

        internal virtual ToolStripMenuItem CMI_Alle_Ausblenden
        {
            get => _CMI_Alle_Ausblenden;
            set
            {
                if (_CMI_Alle_Ausblenden != null)
                {
                    _CMI_Alle_Ausblenden.Click -= CMI_Alle_Anzeigen_Click;
                }
                _CMI_Alle_Ausblenden = value;
                if (_CMI_Alle_Ausblenden != null)
                {
                    _CMI_Alle_Ausblenden.Click += CMI_Alle_Anzeigen_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Alle_Ausblenden;

        internal virtual ToolStripMenuItem CMI_Deaktivieren
        {
            get => _CMI_Deaktivieren;
            set
            {
                if (_CMI_Deaktivieren != null)
                {
                    _CMI_Deaktivieren.Click -= CMI_Deaktivieren_Click;
                }
                _CMI_Deaktivieren = value;
                if (_CMI_Deaktivieren != null)
                {
                    _CMI_Deaktivieren.Click += CMI_Deaktivieren_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Deaktivieren;

        // CommandBarRowElement and CommandBarStripElement are Telerik specific, replaced with ToolStrip
        internal virtual ToolStrip CBB_Commands { get; set; }

        internal virtual ToolStripButton CBB_Hinzufügen
        {
            get => _CBB_Hinzufügen;
            set
            {
                if (_CBB_Hinzufügen != null)
                {
                    _CBB_Hinzufügen.Click -= CBB_Hinzufügen_Click;
                }
                _CBB_Hinzufügen = value;
                if (_CBB_Hinzufügen != null)
                {
                    _CBB_Hinzufügen.Click += CBB_Hinzufügen_Click;
                }
            }
        }
        private ToolStripButton _CBB_Hinzufügen;

        internal virtual ToolStripButton CBB_Löschen
        {
            get => _CBB_Löschen;
            set
            {
                if (_CBB_Löschen != null)
                {
                    _CBB_Löschen.Click -= CBB_Löschen_Click;
                }
                _CBB_Löschen = value;
                if (_CBB_Löschen != null)
                {
                    _CBB_Löschen.Click += CBB_Löschen_Click;
                }
            }
        }
        private ToolStripButton _CBB_Löschen;

        internal virtual ToolStripSeparator CommandBarSeparator1 { get; set; }

        internal virtual ToolStripTextBox CBT_Suche
        {
            get => _CBT_Suche;
            set
            {
                if (_CBT_Suche != null)
                {
                    _CBT_Suche.KeyUp -= CBT_Suche_KeyUp;
                    _CBT_Suche.TextChanged -= CBT_Suche_TextChanged;
                }
                _CBT_Suche = value;
                if (_CBT_Suche != null)
                {
                    _CBT_Suche.KeyUp += CBT_Suche_KeyUp;
                    _CBT_Suche.TextChanged += CBT_Suche_TextChanged;
                }
            }
        }
        private ToolStripTextBox _CBT_Suche;

        internal virtual ToolStripSeparator CommandBarSeparator3 { get; set; }

        internal virtual ToolStripButton CBT_ShowAll // Changed from ToggleButton to Button, logic handled in click
        {
            get => _CBT_ShowAll;
            set
            {
                if (_CBT_ShowAll != null)
                {
                    _CBT_ShowAll.Click -= CBT_ShowAll_CheckStateChanged; // Changed event
                }
                _CBT_ShowAll = value;
                if (_CBT_ShowAll != null)
                {
                    _CBT_ShowAll.Click += CBT_ShowAll_CheckStateChanged; // Changed event
                }
            }
        }
        private ToolStripButton _CBT_ShowAll;

        internal virtual ToolStripDropDownButton CBD_Liste_Sender { get; set; }

        internal virtual ToolStripMenuItem DDI_Alle_Anzeigen
        {
            get => _DDI_Alle_Anzeigen;
            set
            {
                if (_DDI_Alle_Anzeigen != null)
                {
                    _DDI_Alle_Anzeigen.Click -= DDI_Alle_Anzeigen_Click;
                }
                _DDI_Alle_Anzeigen = value;
                if (_DDI_Alle_Anzeigen != null)
                {
                    _DDI_Alle_Anzeigen.Click += DDI_Alle_Anzeigen_Click;
                }
            }
        }
        private ToolStripMenuItem _DDI_Alle_Anzeigen;

        internal virtual ToolStripSeparator RadMenuSeparatorItem4 { get; set; }
        internal virtual ToolStripSeparator CommandBarSeparator5 { get; set; }

        internal virtual ToolStripButton CBB_Aufnahmen_Heute
        {
            get => _CBB_Aufnahmen_Heute;
            set
            {
                if (_CBB_Aufnahmen_Heute != null)
                {
                    _CBB_Aufnahmen_Heute.Click -= CBB_Aufnahmen_Heute_Click;
                }
                _CBB_Aufnahmen_Heute = value;
                if (_CBB_Aufnahmen_Heute != null)
                {
                    _CBB_Aufnahmen_Heute.Click += CBB_Aufnahmen_Heute_Click;
                }
            }
        }
        private ToolStripButton _CBB_Aufnahmen_Heute;

        internal virtual ToolStripButton CBB_Favoriten
        {
            get => _CBB_Favoriten;
            set
            {
                if (_CBB_Favoriten != null)
                {
                    _CBB_Favoriten.Click -= CBB_Favoriten_Click;
                }
                _CBB_Favoriten = value;
                if (_CBB_Favoriten != null)
                {
                    _CBB_Favoriten.Click += CBB_Favoriten_Click;
                }
            }
        }
        private ToolStripButton _CBB_Favoriten;

        internal virtual ToolStripSeparator CommandBarSeparator2 { get; set; }

        internal virtual ToolStripButton CBB_Einstellungen
        {
            get => _CBB_Einstellungen;
            set
            {
                if (_CBB_Einstellungen != null)
                {
                    _CBB_Einstellungen.Click -= CBB_Einstellungen_Click;
                }
                _CBB_Einstellungen = value;
                if (_CBB_Einstellungen != null)
                {
                    _CBB_Einstellungen.Click += CBB_Einstellungen_Click;
                }
            }
        }
        private ToolStripButton _CBB_Einstellungen;

        internal virtual ToolStripSeparator CommandBarSeparator4 { get; set; }

        internal virtual ToolStripButton CBB_CamsRecorder
        {
            get => _CBB_CamsRecorder;
            set
            {
                if (_CBB_CamsRecorder != null)
                {
                    _CBB_CamsRecorder.Click -= CBB_CamsRecorder_Click;
                }
                _CBB_CamsRecorder = value;
                if (_CBB_CamsRecorder != null)
                {
                    _CBB_CamsRecorder.Click += CBB_CamsRecorder_Click;
                }
            }
        }
        private ToolStripButton _CBB_CamsRecorder;

        internal virtual ProgressBar PGB_Disk { get; set; }
        internal virtual Label LAB_Drive { get; set; }
        internal virtual Label LAB_Warnung { get; set; }
        internal virtual ToolStrip CBB_Model_Kanal { get; set; } // Changed from RadCommandBar to ToolStrip
        internal virtual ToolStripSeparator CommandBarSeparator6 { get; set; }
        internal virtual ToolStripSeparator RadMenuSeparatorItem5 { get; set; }

        internal virtual ToolStripMenuItem CMI_Delete
        {
            get => _CMI_Delete;
            set
            {
                if (_CMI_Delete != null)
                {
                    _CMI_Delete.Click -= CMI_Delete_Click;
                }
                _CMI_Delete = value;
                if (_CMI_Delete != null)
                {
                    _CMI_Delete.Click += CMI_Delete_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Delete;

        internal virtual ToolStripMenuItem CMI_Stream_Refresh
        {
            get => _CMI_Stream_Refresh;
            set
            {
                if (_CMI_Stream_Refresh != null)
                {
                    _CMI_Stream_Refresh.Click -= CMI_Stream_Refresh_Click;
                }
                _CMI_Stream_Refresh = value;
                if (_CMI_Stream_Refresh != null)
                {
                    _CMI_Stream_Refresh.Click += CMI_Stream_Refresh_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Stream_Refresh;

        internal virtual ToolStripMenuItem CMI_Folder_Open
        {
            get => _CMI_Folder_Open;
            set
            {
                if (_CMI_Folder_Open != null)
                {
                    _CMI_Folder_Open.Click -= CMI_Folder_Open_Click;
                }
                _CMI_Folder_Open = value;
                if (_CMI_Folder_Open != null)
                {
                    _CMI_Folder_Open.Click += CMI_Folder_Open_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Folder_Open;

        internal virtual ToolStripSeparator RadMenuSeparatorItem6 { get; set; }

        internal virtual ToolStripMenuItem CMI_New_Records
        {
            get => _CMI_New_Records;
            set
            {
                if (_CMI_New_Records != null)
                {
                    _CMI_New_Records.Click -= CMI_Record_New_Click;
                }
                _CMI_New_Records = value;
                if (_CMI_New_Records != null)
                {
                    _CMI_New_Records.Click += CMI_Record_New_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_New_Records;

        internal virtual ToolStripMenuItem CMI_Gesehen
        {
            get => _CMI_Gesehen;
            set
            {
                if (_CMI_Gesehen != null)
                {
                    _CMI_Gesehen.Click -= CMI_Gesehen_Click;
                }
                _CMI_Gesehen = value;
                if (_CMI_Gesehen != null)
                {
                    _CMI_Gesehen.Click += CMI_Gesehen_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Gesehen;

        internal virtual Label RadLabel1 { get; set; } // Changed from RadLabel to Label
        internal virtual ToolStripMenuItem CMI_Ansicht { get; set; }
        internal virtual ToolStripMenuItem CMI_Grouping { get; set; }

        internal virtual ToolStripMenuItem CMI_LastOnline
        {
            get => _CMI_LastOnline;
            set
            {
                if (_CMI_LastOnline != null)
                {
                    _CMI_LastOnline.Click -= GRV_GroupItem_Click;
                }
                _CMI_LastOnline = value;
                if (_CMI_LastOnline != null)
                {
                    _CMI_LastOnline.Click += GRV_GroupItem_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_LastOnline;

        internal virtual ToolStripMenuItem CMI_Provider
        {
            get => _CMI_Provider;
            set
            {
                if (_CMI_Provider != null)
                {
                    _CMI_Provider.Click -= GRV_GroupItem_Click;
                }
                _CMI_Provider = value;
                if (_CMI_Provider != null)
                {
                    _CMI_Provider.Click += GRV_GroupItem_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Provider;

        internal virtual ToolStripMenuItem CMI_Geschlecht
        {
            get => _CMI_Geschlecht;
            set
            {
                if (_CMI_Geschlecht != null)
                {
                    _CMI_Geschlecht.Click -= GRV_GroupItem_Click;
                }
                _CMI_Geschlecht = value;
                if (_CMI_Geschlecht != null)
                {
                    _CMI_Geschlecht.Click += GRV_GroupItem_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Geschlecht;

        internal virtual ToolStripMenuItem CMI_Promo_Add
        {
            get => _CMI_Promo_Add;
            set
            {
                if (_CMI_Promo_Add != null)
                {
                    _CMI_Promo_Add.Click -= CMI_Promo_Add_Click;
                }
                _CMI_Promo_Add = value;
                if (_CMI_Promo_Add != null)
                {
                    _CMI_Promo_Add.Click += CMI_Promo_Add_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Promo_Add;

        internal virtual CustomFlowLayoutPanel PAN_Favoriten { get; set; }

        internal virtual ContextMenuStrip CME_Preview
        {
            get => _CME_Preview;
            set
            {
                if (_CME_Preview != null)
                {
                    _CME_Preview.Opening -= CME_Preview_DropDownOpening;
                }
                _CME_Preview = value;
                if (_CME_Preview != null)
                {
                    _CME_Preview.Opening += CME_Preview_DropDownOpening;
                }
            }
        }
        private ContextMenuStrip _CME_Preview;

        internal virtual ToolStripMenuItem CMI_Favoriten
        {
            get => _CMI_Favoriten;
            set
            {
                if (_CMI_Favoriten != null)
                {
                    _CMI_Favoriten.Click -= CMI_Favoriten_Click;
                }
                _CMI_Favoriten = value;
                if (_CMI_Favoriten != null)
                {
                    _CMI_Favoriten.Click += CMI_Favoriten_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Favoriten;

        internal virtual ToolStripMenuItem CMI_Records
        {
            get => _CMI_Records;
            set
            {
                if (_CMI_Records != null)
                {
                    _CMI_Records.Click -= CMI_Records_Click;
                }
                _CMI_Records = value;
                if (_CMI_Records != null)
                {
                    _CMI_Records.Click += CMI_Records_Click;
                }
            }
        }
        private ToolStripMenuItem _CMI_Records;

        internal virtual NotifyIcon Cam_Benachrichtigung
        {
            get => _Cam_Benachrichtigung;
            set
            {
                if (_Cam_Benachrichtigung != null)
                {
                    _Cam_Benachrichtigung.MouseUp -= Cam_Benachrichtigung_Click;
                    _Cam_Benachrichtigung.MouseMove -= Cam_Benachrichtigung_MouseMove;
                }
                _Cam_Benachrichtigung = value;
                if (_Cam_Benachrichtigung != null)
                {
                    _Cam_Benachrichtigung.MouseUp += Cam_Benachrichtigung_Click;
                    _Cam_Benachrichtigung.MouseMove += Cam_Benachrichtigung_MouseMove;
                }
            }
        }
        private NotifyIcon _Cam_Benachrichtigung;

        internal virtual ContextMenuStrip CMS_Benachrichtigung
        {
            get => _CMS_Benachrichtigung;
            set
            {
                if (_CMS_Benachrichtigung != null)
                {
                    _CMS_Benachrichtigung.Opening -= CMS_Benachrichtigung_Opening;
                }
                _CMS_Benachrichtigung = value;
                if (_CMS_Benachrichtigung != null)
                {
                    _CMS_Benachrichtigung.Opening += CMS_Benachrichtigung_Opening;
                }
            }
        }
        private ContextMenuStrip _CMS_Benachrichtigung;

        internal virtual ToolStripMenuItem TMI_Benachrichtung_Anzeigen
        {
            get => _TMI_Benachrichtung_Anzeigen;
            set
            {
                if (_TMI_Benachrichtung_Anzeigen != null)
                {
                    _TMI_Benachrichtung_Anzeigen.Click -= TMI_Benachrichtung_Anzeigen_Click;
                }
                _TMI_Benachrichtung_Anzeigen = value;
                if (_TMI_Benachrichtung_Anzeigen != null)
                {
                    _TMI_Benachrichtung_Anzeigen.Click += TMI_Benachrichtung_Anzeigen_Click;
                }
            }
        }
        private ToolStripMenuItem _TMI_Benachrichtung_Anzeigen;

        // RadDesktopAlert has no direct native equivalent, would need custom form/notification
        internal virtual Form DTA_Benachrichtigung { get; set; } // Placeholder for custom notification form

        internal virtual System.Windows.Forms.Timer Drive_Info_Refresh_Timer
        {
            get => _Drive_Info_Refresh_Timer;
            set
            {
                if (_Drive_Info_Refresh_Timer != null)
                {
                    _Drive_Info_Refresh_Timer.Tick -= (sender, e) => DiskSpace();
                }
                _Drive_Info_Refresh_Timer = value;
                if (_Drive_Info_Refresh_Timer != null)
                {
                    _Drive_Info_Refresh_Timer.Tick += (sender, e) => DiskSpace();
                }
            }
        }
        private System.Windows.Forms.Timer _Drive_Info_Refresh_Timer;

        internal bool Show_All
        {
            get => Pri_Show_All;
            set
            {
                Pri_Show_All = value;
                Show_All_Run();
            }
        }

        private async void Show_All_Run()
        {
            await Task.CompletedTask;
            try
            {
                if (Pri_Show_All)
                {
                    CBT_ShowAll.Checked = true;
                    foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                    {
                        // Assuming row.Cells["Pro_Model_GUID"].Value is a Guid or convertible to Guid
                        if (row.Cells["Pro_Model_GUID"].Value != null)
                        {
                            Class_Model result = Class_Model_List.Class_Model_Find((Guid)row.Cells["Pro_Model_GUID"].Value).Result;
                            if (result != null && Sites.Website_Find(result.Pro_Website_ID).Pro_ShowAll)
                            {
                                Vorschau_New(result);
                            }
                        }
                    }
                }
                else
                {
                    CBT_ShowAll.Checked = false;
                    foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                    {
                        if (row.Cells["Pro_Model_GUID"].Value != null)
                        {
                            Class_Model result = Class_Model_List.Class_Model_Find((Guid)row.Cells["Pro_Model_GUID"].Value).Result;
                            if (result != null)
                            {
                                foreach (Control_Stream control in PAN_Show.Controls.OfType<Control_Stream>().ToList())
                                {
                                    if (control.Pro_Model_Class == result && !result.Pro_Model_Visible)
                                    {
                                        control.Visible = false;
                                        control.Dispose();
                                    }
                                }
                                foreach (Control_Stream control in PAN_Record.Controls.OfType<Control_Stream>().ToList())
                                {
                                    if (control.Pro_Model_Class == result && !result.Pro_Model_Visible)
                                    {
                                        control.Visible = false;
                                        control.Dispose();
                                    }
                                }
                            }
                        }
                    }
                    Parameter.FlushMemory();
                }
                GRV_Model_Kanal.Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Show_All_Run");
            }
        }

        private async void Show_Visible_Run()
        {
            await Task.CompletedTask;
            try
            {
                if (Pri_Show_Visible)
                {
                    foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                    {
                        if (row.Cells["Pro_Model_GUID"].Value != null)
                        {
                            Class_Model result = Class_Model_List.Class_Model_Find((Guid)row.Cells["Pro_Model_GUID"].Value).Result;
                            if (result != null && result.Pro_Model_Visible)
                            {
                                Vorschau_New(result);
                            }
                        }
                    }
                }
                else
                {
                    foreach (Control_Stream control in PAN_Show.Controls.OfType<Control_Stream>().ToList())
                    {
                        control.Visible = false;
                        control.Dispose();
                    }
                    foreach (Control_Stream control in PAN_Record.Controls.OfType<Control_Stream>().ToList())
                    {
                        control.Visible = false;
                        control.Dispose();
                    }
                }
                Parameter.FlushMemory();
                GRV_Model_Kanal.Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Show_Visible_Run");
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false); // Equivalent to UseCompatibleTextRendering = false
                Visible = false;
                Pri_Data_Load = true;
                FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
                string path2 = fileInfo.Name.Replace(fileInfo.Extension, "");
                Parameter.CommonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), path2);
                if (!Directory.Exists(Parameter.CommonPath))
                    Directory.CreateDirectory(Parameter.CommonPath);
                Parameter.INI_Common = Path.Combine(Parameter.CommonPath, Application.ProductName + ".ini");

                try
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CamRecorder\\CamRecorder.ini") && !File.Exists(Parameter.INI_Common))
                    {
                        File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CamRecorder\\CamRecorder.ini", Parameter.INI_Common);
                        IniFile.Write(Parameter.INI_Common, "Language", "Files", "");
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle exception
                }

                Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", Parameter.CommonPath);
                Sites.Website_Load();
                bool flag = true;
                int Platform_ID = 0;
                if (!File.Exists(Parameter.INI_Common))
                {
                    Modul_StatusScreen.Status_Show(TXT.TXT_Description(""));
                    Dialog_FirstStart dialogFirstStart = new Dialog_FirstStart();
                    dialogFirstStart.StartPosition = FormStartPosition.CenterScreen;
                    using (dialogFirstStart)
                    {
                        if (dialogFirstStart.ShowDialog() == DialogResult.OK)
                        {
                            if (dialogFirstStart.Pro_New_Channel)
                            {
                                flag = dialogFirstStart.Pro_New_Channel;
                                Platform_ID = -1;
                            }
                            else
                            {
                                flag = false;
                                // Assuming DDL_Webseite.SelectedValue is an int
                                Platform_ID = (int)dialogFirstStart.DDL_Webseite.SelectedValue!;
                            }
                        }
                        else
                        {
                            Application.Exit();
                            return;
                        }
                    }
                }
                Parameter.Design_Change(int.Parse(IniFile.Read(Parameter.INI_Common, "Design", "Style", "0")));
                Languages.Languages_Set();
                if (Modul_Update.Update_Check())
                {
                    Application.Exit();
                }
                else
                {
                    Modul_StatusScreen.Status_Show(TXT.TXT_Description("XstreaMon wird geladen"));
                    Parameter.Debug_Modus = bool.Parse(IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "False"));
                    Parameter.Programlizenz = new Lizenz(true);
                    Text = "XstreaMon" + Parameter.Programlizenz.Lizenz_Programmbezeichnung;
                    if (Modul_Ordner.Ordner_Pfad().Length == 0)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        if (!Database.DB_File_Check())
                        {
                            MessageBox.Show(TXT.TXT_Description("Ohne Datenbank geht leider nichts."), TXT.TXT_Description("Datenbank fehlt"));
                            Application.Exit();
                        }
                        Tray_Create();
                        Modul_StatusScreen.Status_Show(TXT.TXT_Description("Webseiten werden geladen"));
                        foreach (Class_Website website in Sites.Website_List)
                        {
                            if (website.Pro_ID > -1 && IniFile.Read(Parameter.INI_Common, "Sites", website.Pro_ID.ToString(), "True") == "True")
                            {
                                ToolStripButton commandBarButton = new ToolStripButton();
                                commandBarButton.Tag = website.Pro_ID;
                                commandBarButton.Image = new Bitmap(website.Pro_Image, 16, 16);
                                commandBarButton.Name = website.Pro_Name;
                                commandBarButton.ToolTipText = website.Pro_Name;
                                commandBarButton.Click += CBB_Site_Click;
                                CBB_Commands.Items.Add(commandBarButton); // Assuming CBB_Commands is a ToolStrip
                                ToolStripMenuItem radMenuItem = new ToolStripMenuItem();
                                radMenuItem.Name = website.Pro_ID.ToString();
                                radMenuItem.Text = website.Pro_Name;
                                radMenuItem.CheckOnClick = true;
                                radMenuItem.Checked = website.Pro_ShowAll;
                                radMenuItem.Click += (s, args) => DDI_Site_Click((ToolStripMenuItem)s, args);
                                CBD_Liste_Sender.DropDownItems.Add(radMenuItem);
                            }
                        }
                        Class_Streamsize.Size_Load();
                        Modul_StatusScreen.Status_Show(TXT.TXT_Description("Sprache wird verarbeitet"));
                        TXT.Control_Text_Change(this);
                        CBB_Aufnahmen_Heute.ToolTipText = TXT.TXT_Description("Aufnahmen letzten 24 Stunden");
                        CBB_Einstellungen.ToolTipText = TXT.TXT_Description("Programm Optionen");
                        CBB_Favoriten.ToolTipText = TXT.TXT_Description("Aufnahme Favoriten");
                        CBB_Hinzufügen.ToolTipText = TXT.TXT_Description("Kanal hinzufügen");
                        CBB_Löschen.ToolTipText = TXT.TXT_Description("Kanal löschen");
                        CBT_ShowAll.ToolTipText = TXT.TXT_Description("Alle Anzeigen");
                        CBD_Liste_Sender.ToolTipText = TXT.TXT_Description("Webseiten auswählen");
                        DDI_Alle_Anzeigen.Text = TXT.TXT_Description("Alle Anzeigen");
                        CBT_Suche.TextBox.PlaceholderText = TXT.TXT_Description("Suche..."); // PlaceholderText for ToolStripTextBox

                        Modul_StatusScreen.Status_Show(TXT.TXT_Description("Kanäle werden geladen"));
                        DataTable DT_User_Data = new DataTable();
                        using (OleDbConnection oleDbConnection = new OleDbConnection())
                        {
                            oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                            using (DataSet dataSet = new DataSet())
                            {
                                oleDbConnection.Open();
                                if (oleDbConnection.State == ConnectionState.Open)
                                {
                                    try
                                    {
                                        using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("Select User_GUID from DT_User ORDER BY User_Deaktiv DESC , User_Favorite, User_Record, User_Visible;", oleDbConnection.ConnectionString))
                                            oleDbDataAdapter.Fill(dataSet, "DT_User");
                                    }
                                    catch (Exception ex)
                                    {
                                        oleDbConnection.Close();
                                        Database.Database_Defekt(ex);
                                    }
                                    oleDbConnection.Close();
                                    DT_User_Data = dataSet.Tables["DT_User"];
                                }
                            }
                        }
                        GRV_Model_Load();
                        if (DT_User_Data.Rows.Count == 0)
                        {
                            if (flag)
                                Chanel_Add();
                            else if (Platform_ID > -1)
                                Sites.WebOpen(Sites.Website_Find(Platform_ID)?.Pro_Home_URL);
                        }
                        else
                        {
                            Model_load(DT_User_Data);
                            if (!Lizenz.Lizenz_vorhanden || bool.Parse(IniFile.Read(Parameter.INI_Common, "Lizenz", "Advice", "True")))
                                Model_Promo_load();
                        }
                        Modul_StatusScreen.Status_Show(TXT.TXT_Description("Speicherplatz überprüft"));
                        Drive_Info = new Class_Driveinfo(Modul_Ordner.Ordner_Pfad().Substring(0, 3));
                        DiskSpace();
                        Drive_Info_Refresh_Timer.Start();
                        GRV_Model_Kanal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        Parameter.Programlizenz.Laufzeit_Benachrichtigung();

                        // Setting shortcuts for ToolStripMenuItems
                        CMI_Gesehen.ShortcutKeys = Keys.Control | Keys.D;
                        CMI_Anzeigen.ShortcutKeys = Keys.Control | Keys.S;
                        CMI_Aufnahme.ShortcutKeys = Keys.Control | Keys.R;
                        CMI_Folder_Open.ShortcutKeys = Keys.Control | Keys.E;
                        CMI_Galerie.ShortcutKeys = Keys.Control | Keys.G;
                        CMI_Webseite.ShortcutKeys = Keys.Control | Keys.W;
                        CMI_Optionen.ShortcutKeys = Keys.Control | Keys.O;
                        CMI_Delete.ShortcutKeys = Keys.Control | Keys.Delete;

                        Pri_Data_Load = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Form_Load");
            }
            finally
            {
                Modul_StatusScreen.Status_Show(TXT.TXT_Description(""));
                Visible = true;
            }
        }

        // Reemplaza cada llamada a oleDbConnection.Open(); por await oleDbConnection.OpenAsync(); 
        // y asegúrate de que el método que contiene la llamada sea async y se espere correctamente.

        internal async void Model_load(DataTable DT_User_Data)
        {
            await Task.CompletedTask;
            try
            {
                int num = 1;
                foreach (DataRow row in DT_User_Data.Rows)
                {
                    try
                    {
                        Modul_StatusScreen.Status_Show(string.Format(TXT.TXT_Description("{0} von {1} Kanäle werden geladen"), num, DT_User_Data.Rows.Count));
                        num++;
                        object obj = row["User_GUID"];
                        Guid userGuid = obj != DBNull.Value ? (Guid)obj : Guid.Empty;
                        Class_Model classModel = new Class_Model(userGuid);
                        classModel.Model_Online_Change += Model_Online_Change;
                        classModel.Model_Show_Notification += Cam_Benachrichtigung_Notification;
                        Class_Model_List.Model_Add(classModel);
                        if (classModel.Get_Pro_Model_Online())
                            Model_Online_Change(classModel);

                        DataGridViewRow GRV_Row = new DataGridViewRow();
                        GRV_Row.CreateCells(GRV_Model_Kanal);
                        GRV_Row_Fill(GRV_Row, classModel);
                        GRV_Model_Kanal.Rows.Add(GRV_Row);
                    }
                    catch (Exception ex)
                    {
                        Parameter.Error_Message(ex, "Form_Class_Record.Model_load");
                    }
                }
                GRV_Model_Kanal.FirstDisplayedScrollingRowIndex = 0; // Equivalent to VScrollBar.Value = 0
                // GroupDescriptors are Telerik specific, would need custom grouping logic for DataGridView
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Model_load");
            }
        }

        internal async void Model_Promo_load()
        {
            await Task.CompletedTask;
            try
            {
                string str1 = "";
                try
                {
                    using HttpClient httpClient = new();
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("XstreaMon Promo" + Application.ProductVersion);
                    str1 = await httpClient.GetStringAsync("https://xstreamon.com/model_promo");
                }
                catch (Exception ex)
                {
                    // Log or handle exception
                }

                string[] strArray = str1.Split('\r');
                int index = 0;
                while (index < strArray.Length)
                {
                    string str2 = strArray[index];
                    if (str2.Trim().Length > 0)
                    {
                        string[] parts = str2.Split('|');
                        if (parts.Length >= 2)
                        {
                            string modelName = parts[0].Trim();
                            int websiteId = int.Parse(parts[1].Trim());

                            if (await Class_Model_List.Class_Model_Find(websiteId, modelName) == null)
                            {
                                try
                                {
                                    Class_Model classModel = new Class_Model(modelName, websiteId)
                                    {
                                        Pro_Model_Promo = true
                                    };
                                    classModel.Model_Online_Change += Model_Online_Change;
                                    classModel.Model_Show_Notification += Cam_Benachrichtigung_Notification;
                                    Class_Model_List.Model_Add(classModel);
                                    if (classModel.Get_Pro_Model_Online())
                                        Model_Online_Change(classModel);

                                    DataGridViewRow GRV_Row = new DataGridViewRow();
                                    GRV_Row.CreateCells(GRV_Model_Kanal);
                                    GRV_Row_Fill(GRV_Row, classModel);
                                    GRV_Model_Kanal.Rows.Add(GRV_Row);
                                }
                                catch (Exception ex)
                                {
                                    Parameter.Error_Message(ex, "Form_Class_Record.Model_load");
                                }
                            }
                        }
                    }
                    index++;
                }
                GRV_Model_Kanal.FirstDisplayedScrollingRowIndex = 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Model_load");
            }
        }

        internal async void DiskSpace()
        {
            Drive_Info_Refresh_Timer.Stop();
            await Task.CompletedTask;
            try
            {
                if (Drive_Info == null)
                    return;
                Drive_Info.Refresh();
                if (!Drive_Info.Letter.StartsWith("\\\\"))
                {
                    PGB_Disk.Visible = true;
                    PGB_Disk.Maximum = (int)Math.Round(Drive_Info.Total_Size / 1024.0 / 1024.0);
                    PGB_Disk.Value = (int)Math.Round((Drive_Info.UsedSpace - Drive_Info.Record_Space) / 1024.0 / 1024.0); // Value1
                    // PGB_Disk.Value2 = (int)Math.Round(Form_Main.Drive_Info.UsedSpace / 1024.0 / 1024.0); // No Value2 in native ProgressBar
                    PGB_Disk.Text = ""; // Text property is not directly settable for ProgressBar, usually handled by a label
                    ToolTip1.SetToolTip(PGB_Disk, TXT.TXT_Description("Belegt") + ": " + ValueBack.Get_Numeric2Bytes(Drive_Info.UsedSpace) + "\r\n" + TXT.TXT_Description("Verfügbar") + ": " + ValueBack.Get_Numeric2Bytes(Drive_Info.Total_Size - Drive_Info.UsedSpace) + "\r\n" + TXT.TXT_Description("Aufnahmen") + ": " + ValueBack.Get_Numeric2Bytes(Drive_Info.Record_Space) + "\r\n" + TXT.TXT_Description("ca.") + " " + Math.Round(Drive_Info.Freespace / 1024.0 / 1024.0 / 2000.0, 1) + " " + TXT.TXT_Description("Stunden in HD möglich"));

                    if ((double)Drive_Info.Freespace / Drive_Info.Total_Size * 100.0 < 10.0)
                    {
                        LAB_Warnung.Text = string.Format("{0} % freier Speicherplatz verfügbar", (int)Math.Round((double)Drive_Info.Freespace / Drive_Info.Total_Size * 100.0));
                        LAB_Warnung.Visible = true;
                    }
                    else
                    {
                        LAB_Warnung.Visible = false;
                        LAB_Warnung.Text = "";
                    }
                }
                else
                {
                    PGB_Disk.Visible = false;
                }
                Meldung_Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Diskspace");
            }
            Drive_Info_Refresh_Timer.Start();
        }

        internal async void Meldung_Refresh()
        {
            await Task.CompletedTask;
            try
            {
                string str = "";
                int proOnline = Class_Model_List.Pro_Online;
                int num = Class_Model_List.Pro_Records + Class_Record_Manual.Manual_Record_List.Count;
                str = str + string.Format(TXT.TXT_Description("{0} von {1} online"), proOnline, Class_Model_List.Pro_Count) + "\r\n";
                str += string.Format(TXT.TXT_Description("{0} Aufnahmen"), num);
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)(() => LAB_Drive.Text = str));
                }
                else
                {
                    LAB_Drive.Text = str;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Meldung_Refresh");
            }
        }

        internal async void Model_Online_Change(Class_Model Model_Class)
        {
            await Task.CompletedTask;
            try
            {
                if (Model_Class.Get_Pro_Model_Online(true) && Model_Class.Pro_Model_Visible && Pri_Show_Visible)
                    Vorschau_New(Model_Class);

                foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                {
                    if (row.Cells["Pro_Model_GUID"].Value != null && (Guid)row.Cells["Pro_Model_GUID"].Value == Model_Class.Pro_Model_GUID)
                    {
                        GRV_Row_Fill(row, Model_Class);
                        break;
                    }
                }

                if (!Model_Class.Get_Pro_Model_Online() && (Model_Class.Pro_Model_M3U8 == null || Model_Class.Pro_Model_M3U8 == "") || !Model_Class.Pro_Model_Visible)
                {
                    Panel[] panelArray = new Panel[] { PAN_Record, PAN_Show, PAN_Favoriten };
                    foreach (Panel panel in panelArray)
                    {
                        foreach (Control_Stream controlStream in panel.Controls.OfType<Control_Stream>().ToList())
                        {
                            if (controlStream.Pro_Model_Class == Model_Class)
                            {
                                if (InvokeRequired)
                                {
                                    Invoke((MethodInvoker)(() =>
                                    {
                                        controlStream.Visible = false;
                                        panel.Controls.Remove(controlStream);
                                        controlStream.Dispose();
                                    }));
                                }
                                else
                                {
                                    controlStream.Visible = false;
                                    panel.Controls.Remove(controlStream);
                                    controlStream.Dispose();
                                }
                                break;
                            }
                        }
                    }
                }
                Meldung_Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Model_online_Change");
            }
        }

        private void Vorschau_New(Class_Model Model_Class)
        {
            try
            {
                Vorschau_Verarbeiten(Model_Class);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Vorschau_New");
            }
        }

        private void Vorschau_Verarbeiten(Class_Model Model_Class)
        {
            try
            {
                if (!Model_Class.Get_Pro_Model_Online())
                    return;

                CustomFlowLayoutPanel[] panels = new CustomFlowLayoutPanel[] { PAN_Show, PAN_Record, PAN_Favoriten };
                Control_Stream con1 = panels.SelectMany(p => p.Controls.OfType<Control_Stream>()).FirstOrDefault(con => con.Pro_Model_Class == Model_Class);

                PAN_Streams.SuspendLayout();
                if (con1 == null)
                {
                    Control_Stream con = new Control_Stream()
                    {
                        Show_Header = bool.Parse(IniFile.Read(Parameter.INI_Common, "Design", "Header", "True")),
                        Streamrun = true,
                        Pro_Model_Class = Model_Class
                    };
                    con.StreamRecord_Start += Stream_Record_Start;
                    con.StreamRecord_Stop += Stream_Record_Stop;
                    con.Vorschau_Close += Vorschau_Close;
                    con.Vorschau_Focused += Vorschau_Focus;
                    con.Record_Run = Model_Class.Pro_Model_Stream_Record != null;
                    Find_Preview_panel(con).Controls.Add(con);
                }
                else if (con1 != null)
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)(() =>
                        {
                            con1.Visible = false;
                            con1.Record_Run = Model_Class.Pro_Model_Stream_Record != null;
                            con1.Parent = Find_Preview_panel(con1);
                            con1.Visible = true;
                        }));
                    }
                    else
                    {
                        con1.Visible = false;
                        con1.Record_Run = Model_Class.Pro_Model_Stream_Record != null;
                        con1.Parent = Find_Preview_panel(con1);
                        con1.Visible = true;
                    }
                }
                PAN_Streams.ResumeLayout();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Vorschau_New");
            }
        }

        private CustomFlowLayoutPanel Find_Preview_panel(Control_Stream con)
        {
            CustomFlowLayoutPanel previewPanel;
            try
            {
                bool isFavorite = con.Pro_Model_Class?.Pro_Model_Favorite ?? false;
                if (bool.Parse(IniFile.Read(Parameter.INI_Common, "Preview", "Favoriten", "True")) && isFavorite)
                {
                    con.Size = Class_Streamsize.Pro_Stream_Size_View;
                    previewPanel = PAN_Favoriten;
                }
                else if (con.Pro_Model_Class?.Pro_Model_Stream_Record != null && bool.Parse(IniFile.Read(Parameter.INI_Common, "Preview", "Records", "True")))
                {
                    con.Size = Class_Streamsize.Pro_Stream_Size_Recorder;
                    previewPanel = PAN_Record;
                }
                else
                {
                    con.Size = Class_Streamsize.Pro_Stream_Size_View;
                    previewPanel = PAN_Show;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Favoriten_Click");
                previewPanel = PAN_Show;
            }
            return previewPanel;
        }

        private async void Vorschau_Close(Control_Stream sender)
        {
            await Task.CompletedTask;
            try
            {
                Class_Model result = Class_Model_List.Class_Model_Find(sender.Pro_Model_Class?.Pro_Model_GUID ?? Guid.Empty).Result;
                if (result != null)
                {
                    result.Pro_Model_Visible = false;
                    result.Model_Online_Changed();
                }
                sender.Dispose();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Vorschau_Close");
            }
        }

        private void Vorschau_Focus(Control_Stream Sender)
        {
            try
            {
                foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                {
                    if (row.Cells["Pro_Model_GUID"].Value != null && (Guid)row.Cells["Pro_Model_GUID"].Value == Sender.Pro_Model_Class?.Pro_Model_GUID)
                    {
                        GRV_Model_Kanal.CurrentCell = row.Cells[0]; // Set current cell to make row current
                        row.Selected = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Vorschau_Focus");
            }
        }

        private async void Stream_Record_Start(Control_Stream Sender)
        {
            try
            {
                await Task.CompletedTask;
                Class_Model_List.Class_Model_Find(Sender.Pro_Model_Class?.Pro_Model_GUID ?? Guid.Empty).Result?.Stream_Record_Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Stream_Record_Start");
            }
        }

        internal async void Stream_Record_Stop(Control_Stream Sender)
        {
            try
            {
                await Task.CompletedTask;
                Class_Model_List.Class_Model_Find(Sender.Pro_Model_Class?.Pro_Model_GUID ?? Guid.Empty).Result?.Stream_Record_Stop();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Stream_Record_Stop");
            }
        }

        private void Form_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                bool flag1 = false;
                bool flag2 = false;
                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    if (model.Pro_Model_Stream_Record != null)
                    {
                        DialogResult dialogResult = MessageBox.Show(TXT.TXT_Description("Sollen die Aufnahmen beendet werden?"), TXT.TXT_Description("Aufnahmen beenden"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        switch (dialogResult)
                        {
                            case DialogResult.Cancel:
                                e.Cancel = true;
                                Visible = true;
                                flag2 = false;
                                return;
                            case DialogResult.Yes:
                                Parameter.Recording_Stop = true;
                                flag1 = true;
                                flag2 = true;
                                goto Label_9;
                            default:
                                Parameter.Recording_Stop = true;
                                flag2 = true;
                                goto Label_9;
                        }
                    }
                }
            Label_9:
                if (!flag2 && MessageBox.Show(TXT.TXT_Description("Möchten sie XstreaMon beenden?"), TXT.TXT_Description("XStreaMon beenden"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    Visible = true;
                }
                else
                {
                    Modul_StatusScreen.Status_Show(TXT.TXT_Description("wird beendet"));
                    Visible = false;
                    if (flag1)
                    {
                        int num = 0;
                        foreach (Class_Model model in Class_Model_List.Model_List)
                        {
                            Modul_StatusScreen.Status_Show(string.Format(TXT.TXT_Description("{0} von {1} werden geschlossen"), num, Class_Model_List.Pro_Count));
                            num++;
                            if (model.Pro_Model_Stream_Record != null)
                            {
                                Modul_StatusScreen.Status_Show(string.Format(TXT.TXT_Description("{0} Aufnahme wird beendet"), model.Pro_Model_Name));
                                model.Pro_Model_Stream_Record.Stream_Record_Stop();
                                foreach (Control_Stream control in PAN_Record.Controls.OfType<Control_Stream>().ToList())
                                {
                                    if (control.Pro_Model_Class == model)
                                        control.Dispose();
                                }
                                model.Dispose();
                            }
                        }
                    }
                    foreach (Control control in PAN_Show.Controls.OfType<Control>().ToList()) // Changed to Control to match original
                        control.Dispose();

                    for (int i = Class_Record_Manual.Manual_Record_List.Count - 1; i >= 0; i--)
                    {
                        Class_Record_Manual.Stop_Record(Class_Record_Manual.Manual_Record_List[i]);
                    }
                    Cam_Benachrichtigung.Dispose(); // Dispose NotifyIcon
                    Modul_StatusScreen.Status_Show(TXT.TXT_Description("Datenbackup wird erstellt"));
                    Database.Backup();
                    if (Directory.Exists(Parameter.CommonPath + "\\Temp"))
                        Directory.Delete(Parameter.CommonPath + "\\Temp", true);
                    Modul_StatusScreen.Status_Show(TXT.TXT_Description("Danke"));
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Form_Closing");
            }
        }

        internal bool Chanel_Add(string Site_URL = "")
        {
            bool flag1 = false;
            try
            {
                if (Class_Model_List.Pro_Count > 4 && !Lizenz.Lizenz_vorhanden)
                {
                    if (MessageBox.Show(TXT.TXT_Description("Mehr Kanäle können nur in der freigeschalteten Version aufgenommen werden. Möchten Sie Ihre Version freischalten?"), TXT.TXT_Description("Trial Version"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Dialog_Einstellungen dialogEinstellungen = new Dialog_Einstellungen();
                        dialogEinstellungen.StartPosition = FormStartPosition.CenterParent;
                        dialogEinstellungen.Show();
                        Process.Start("https://duehring-edv.com/?cat=40");
                    }
                    return flag1;
                }
                else
                {
                    Guid Model_GUID = Guid.NewGuid();
                    Dialog_Model_Einstellungen modelEinstellungen;
                    if (Site_URL.Length > 0)
                    {
                        modelEinstellungen = new Dialog_Model_Einstellungen(Site_URL, Model_GUID);
                        modelEinstellungen.StartPosition = FormStartPosition.CenterParent;
                    }
                    else
                    {
                        modelEinstellungen = new Dialog_Model_Einstellungen(Model_GUID);
                        modelEinstellungen.StartPosition = FormStartPosition.CenterParent;
                    }
                    using (modelEinstellungen)
                    {
                        if (modelEinstellungen.ShowDialog() == DialogResult.OK)
                        {
                            Class_Model result = Class_Model_List.Class_Model_Find(Model_GUID).Result;
                            if (result != null)
                            {
                                result.Model_Online_Change += Model_Online_Change;
                                result.Model_Show_Notification += Cam_Benachrichtigung_Notification;
                                DataGridViewRow GRV_Row = new DataGridViewRow();
                                GRV_Row.CreateCells(GRV_Model_Kanal);
                                GRV_Row_Fill(GRV_Row, result);
                                GRV_Model_Kanal.Rows.Add(GRV_Row);

                                // GroupDescriptors are Telerik specific, would need custom grouping logic for DataGridView
                                // foreach (GroupDescriptor groupDescriptor in (Collection<GroupDescriptor>)this.GRV_Model_Kanal.GroupDescriptors)
                                //    groupDescriptor.GroupNames[0].PropertyName = groupDescriptor.GroupNames[0].PropertyName;

                                if (result.Get_Pro_Model_Online())
                                    Model_Online_Change(result);
                                flag1 = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CBB_Hinzufügen_Click");
            }
            return flag1;
        }

        private async void Galerie_Öffnen()
        {
            try
            {
                // Original code had a 'num' variable that was not initialized or used consistently.
                // Assuming the intent is to check if a single row is selected.
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;

                try
                {
                    Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                    Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                    if (result == null)
                        return;
                    await Task.Run(() => result.Dialog_Model_View_Show());
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    Parameter.Error_Message(ex, "Form_Class_Record.Galerie_Öffnen");
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Galerie_Öffnen");
            }
        }

        private void Model_Delete()
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count == 0 || MessageBox.Show(string.Format(TXT.TXT_Description("Möchten Sie den Kanal {0} löschen?"), GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_Name"].Value.ToString()), TXT.TXT_Description("Kanal löschen"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null)
                    return;

                Class_Model_List.Model_Del(result);
                foreach (Control_Stream control in PAN_Show.Controls.OfType<Control_Stream>().ToList())
                {
                    if (control.Pro_Model_Class == result)
                    {
                        result.Pro_Model_Visible = false;
                        control.Dispose();
                        break;
                    }
                }
                result.Model_Delete();
                GRV_Model_Kanal.Rows.Remove(GRV_Model_Kanal.SelectedRows[0]);
                result.Dispose();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Model_Delete");
            }
        }

        private async void Mybase_Resize(object sender, EventArgs e)
        {
            await Task.CompletedTask;
            try
            {
                if (WindowState == FormWindowState.Minimized && bool.Parse(IniFile.Read(Parameter.INI_Common, "Tray", "Minimize", "False")))
                    Hide();
                Parameter.FlushMemory();
                if (!Visible || WindowState == FormWindowState.Minimized)
                    return;

                PAN_Streams.SuspendLayout();
                foreach (Control_Stream control in PAN_Favoriten.Controls.OfType<Control_Stream>().ToList())
                    control.Streamshow();
                foreach (Control_Stream control in PAN_Record.Controls.OfType<Control_Stream>().ToList())
                    control.Streamshow();
                foreach (Control_Stream control in PAN_Show.Controls.OfType<Control_Stream>().ToList())
                    control.Streamshow();

                PAN_Streams.BackColor = Color.White;
                PAN_Streams.ResumeLayout();
                PAN_Streams.Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.mybase_resize");
            }
        }

        private void Tray_Create()
        {
            // NotifyIcon is initialized in InitializeComponent
        }

        private void Cam_Benachrichtigung_Click(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left)
                    return;
                WindowState = FormWindowState.Maximized;
                Show();
                BringToFront();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Cam_Benachrichtigung_Click");
            }
        }

        private void Cam_Benachrichtigung_Notification(string Notification_Event, string Notification_Text, Image TooltipImage)
        {
            try
            {
                if (Pri_Data_Load)
                    return;

                // DTA_Benachrichtigung is a custom form/notification in native WinForms
                // This is a simplified example, you would need to create a custom notification form
                // For now, using a simple MessageBox or a custom Form for notification
                if (DTA_Benachrichtigung == null || DTA_Benachrichtigung.IsDisposed)
                {
                    DTA_Benachrichtigung = new Form(); // Replace with your custom notification form
                    DTA_Benachrichtigung.Text = Notification_Event;
                    // Add controls to DTA_Benachrichtigung to display Notification_Text and TooltipImage
                    // Example:
                    Label lblCaption = new Label() { Text = Notification_Event, Font = new Font("Segoe UI", 12f, FontStyle.Bold), AutoSize = true, Location = new Point(10, 10) };
                    Label lblContent = new Label() { Text = Notification_Text, AutoSize = true, Location = new Point(10, 40) };
                    DTA_Benachrichtigung.Controls.Add(lblCaption);
                    DTA_Benachrichtigung.Controls.Add(lblContent);

                    if (TooltipImage != null)
                    {
                        PictureBox pbImage = new PictureBox() { Image = new Bitmap(TooltipImage, new Size(180, 100)), SizeMode = PictureBoxSizeMode.Zoom, Location = new Point(10, 70) };
                        DTA_Benachrichtigung.Controls.Add(pbImage);
                        DTA_Benachrichtigung.Size = new Size(350, 160);
                    }
                    else
                    {
                        DTA_Benachrichtigung.Size = new Size(350, 100);
                    }
                    DTA_Benachrichtigung.Show();
                }
                else
                {
                    DTA_Benachrichtigung.Text = Notification_Event;
                    // Update content of existing notification form
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Cam_Benachrichtigung_Click");
            }
        }

        private void Cam_Benachrichtigung_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                string str = "";
                int num1 = 0;
                int num2 = 0;
                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    if (model.Pro_Model_Stream_Record != null)
                        num2++;
                    if (model.Get_Pro_Model_Online())
                        num1++;
                }
                if (num1 > 0)
                    str = str + num1 + " " + TXT.TXT_Description("von") + " " + Class_Model_List.Pro_Count + " " + TXT.TXT_Description("Kanäle Online") + "\r\n";
                if (num2 > 0)
                    str = str + num2 + " " + TXT.TXT_Description("Kanäle werden aufgenommen");
                Cam_Benachrichtigung.Text = str; // TooltipText for NotifyIcon
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.Cam_Benachrichtigung_MouseMove");
            }
        }

        private void TMI_Benachrichtung_Anzeigen_Click(object sender, EventArgs e)
        {
            if (IniFile.Read(Parameter.INI_Common, "Notification", "Enabled", "True") == "True")
                IniFile.Write(Parameter.INI_Common, "Notification", "Enabled", "False");
            else
                IniFile.Write(Parameter.INI_Common, "Notification", "Enabled", "True");
        }

        private void CMS_Benachrichtigung_Opening(object sender, CancelEventArgs e)
        {
            TMI_Benachrichtung_Anzeigen.Text = TXT.TXT_Description("Benachrichtigung");
            if (IniFile.Read(Parameter.INI_Common, "Notification", "Enabled", "True") == "True")
                TMI_Benachrichtung_Anzeigen.Checked = true;
            else
                TMI_Benachrichtung_Anzeigen.Checked = false;
        }

        private void CMI_Aufnahme_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result != null)
                    result.Pro_Model_Record = !result.Pro_Model_Record;
                result.Model_Online_Changed();
                GRV_Model_Kanal.Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Aufname_Click");
            }
        }

        private void CMI_Galerie_Click(object sender, EventArgs e) => Galerie_Öffnen();

        private void CMI_Promo_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Class_Website classWebsite = Sites.Website_Find((int)CMI_Webseite.Tag);
                if (classWebsite == null)
                    return;

                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null)
                    return;

                if (Chanel_Add(string.Format(classWebsite.Pro_Model_URL, result.Pro_Model_Name)))
                {
                    Class_Model_List.Model_Del(result);
                    foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                    {
                        if (row.Cells["Pro_Model_GUID"].Value != null && (Guid)row.Cells["Pro_Model_GUID"].Value == result.Pro_Model_GUID)
                        {
                            GRV_Model_Kanal.Rows.Remove(row);
                            break;
                        }
                    }
                    foreach (Control_Stream control in PAN_Show.Controls.OfType<Control_Stream>().ToList())
                    {
                        if (control.Pro_Model_Class == result)
                        {
                            result.Pro_Model_Visible = false;
                            control.Dispose();
                            break;
                        }
                    }
                    result.Model_Delete(true);
                    result.Dispose();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Promo_Add_Click");
            }
        }

        private void CMI_Folder_Open_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null || !Directory.Exists(result.Pro_Model_Directory))
                    return;
                Process.Start(result.Pro_Model_Directory);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Stream_Refresh_Click");
            }
        }

        private void CMI_Stream_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result1 = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result1 == null)
                    return;
                string result2 = result1.Model_Stream_Adressen_Load_Back().Result;
                if (result2 == null)
                    return;
                MessageBox.Show(result2);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Stream_Refresh_Click");
            }
        }

        private void CMI_Webseite_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Class_Website classWebsite = Sites.Website_Find((int)CMI_Webseite.Tag);
                if (classWebsite == null)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null)
                    return;

                string url = result.Get_Pro_Model_Online() ? classWebsite.Pro_Model_URL : classWebsite.Pro_Model_Offline_URL;
                Sites.WebOpen(string.Format(url, result.Pro_Model_Name));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Webseite_Click");
            }
        }

        private void CMI_Online_Check_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model_List.Class_Model_Find(modelGuid).Result?.Timer_Online_Change.Check_Run();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Online_Check_Click");
            }
        }

        private void CMI_Optionen_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                using (Dialog_Model_Einstellungen modelEinstellungen = new Dialog_Model_Einstellungen(modelGuid))
                {
                    modelEinstellungen.StartPosition = FormStartPosition.CenterParent;
                    modelEinstellungen.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Optionen_Click");
            }
        }

        private void CMI_Deaktivieren_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null)
                    return;
                result.Pro_Model_Deaktiv = !result.Pro_Model_Deaktiv;
                GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_Deaktiv"].Value = result.Pro_Model_Deaktiv;
                result.Model_Online_Changed();
                GRV_Model_Kanal.Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Favoriten_Click");
            }
        }

        private void CMI_Delete_Click(object senader, EventArgs e)
        {
            try
            {
                Model_Delete();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Delete_Click");
            }
        }

        private void CMI_Gesehen_Click(object sender, EventArgs e)
        {
            if (GRV_Model_Kanal.SelectedRows.Count != 1)
                return;
            Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
            Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
            if (result == null)
                return;
            result.Pro_Model_Galery_Last_Visit = DateTime.Now;
            result.Model_Online_Changed();
            GRV_Model_Kanal.Refresh();
        }

        private void CME_Model_Kanal_DropDownOpening(object sender, CancelEventArgs e)
        {
            try
            {
                Control_Model_Info1.Pro_Model_Preview = null;
                Control_Model_Info1.Pro_Model_GUID = Guid.Empty;
                Control_Model_Info1.Pro_Model_Info = "";
                Control_Model_Info1.Visible = false;

                if (GRV_Model_Kanal.SelectedRows.Count == 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    CMI_Promo_Add.Text = TXT.TXT_Description("in die Modelliste aufnehmen");
                    CMI_Stream_Refresh.Text = TXT.TXT_Description("Streamadressen aktualisieren");
                    CMI_Galerie.Text = TXT.TXT_Description("Galerie");
                    CMI_Folder_Open.Text = TXT.TXT_Description("Aufnahmeordner öffnen");
                    CMI_Optionen.Text = TXT.TXT_Description("Optionen");
                    CMI_Info.Text = TXT.TXT_Description("Info bearbeiten");
                    CMI_Online_Check.Text = TXT.TXT_Description("Online-Prüfung");
                    CMI_Deaktivieren.Text = TXT.TXT_Description("Kanal aktiv");
                    CMI_Delete.Text = TXT.TXT_Description("Löschen");
                    CMI_Gesehen.Text = TXT.TXT_Description("Als gesehen markieren");
                    CMI_Ansicht.Text = TXT.TXT_Description("Ansicht");
                    CMI_Grouping.Text = TXT.TXT_Description("Gruppierung");
                    CMI_LastOnline.Text = TXT.TXT_Description("Letztes mal Online");
                    CMI_Provider.Text = TXT.TXT_Description("Webseite");
                    CMI_Geschlecht.Text = TXT.TXT_Description("Geschlecht");

                    if (GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_Records"].Value != null && (int)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_Records"].Value == 2)
                        CMI_Gesehen.Visible = true;
                    else
                        CMI_Gesehen.Visible = false;

                    CMI_Filter.Text = TXT.TXT_Description("Filter");
                    CMH_Status.Text = TXT.TXT_Description("Onlinestatus");
                    // CMH_Status.FillPrimitive.BackColor = Parameter.Fore_Color_Hell; // No direct equivalent
                    CMH_Gender.Text = TXT.TXT_Description("Geschlecht");
                    // CMH_Gender.FillPrimitive.BackColor = Parameter.Fore_Color_Hell; // No direct equivalent

                    CMI_Couple.Text = TXT.TXT_Description("Paar");
                    CMI_Couple.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Couple", "True"));
                    CMI_Couple.ForeColor = CMI_Couple.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Female.Text = TXT.TXT_Description("Weiblich");
                    CMI_Female.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Female", "True"));
                    CMI_Female.ForeColor = CMI_Female.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Male.Text = TXT.TXT_Description("Männlich");
                    CMI_Male.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Male", "True"));
                    CMI_Male.ForeColor = CMI_Male.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Trans.Text = TXT.TXT_Description("Trans");
                    CMI_Trans.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Trans", "True"));
                    CMI_Trans.ForeColor = CMI_Trans.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Unknow.Text = TXT.TXT_Description("Diverse");
                    CMI_Unknow.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Unknow", "True"));
                    CMI_Unknow.ForeColor = CMI_Unknow.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Offline.Text = TXT.TXT_Description("Offline");
                    CMI_Offline.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Offline", "True"));
                    CMI_Offline.ForeColor = CMI_Offline.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Online.Text = TXT.TXT_Description("Online");
                    CMI_Online.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Online", "True"));
                    CMI_Online.ForeColor = CMI_Online.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Record.Text = TXT.TXT_Description("Aufnahme");
                    CMI_Record.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Record", "True"));
                    CMI_Record.ForeColor = CMI_Record.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_New_Records.Text = TXT.TXT_Description("Neue Aufnahme");
                    CMI_New_Records.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "RecordNew", "True"));
                    CMI_New_Records.ForeColor = CMI_New_Records.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_AutoRecord.Text = TXT.TXT_Description("Automatische Aufnahme");
                    CMI_AutoRecord.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "AutoRecord", "True"));
                    CMI_AutoRecord.ForeColor = CMI_AutoRecord.Checked ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;

                    CMI_Alle_Anzeigen.Text = TXT.TXT_Description("Alle Vorschauen anzeigen");
                    CMI_Alle_Ausblenden.Text = TXT.TXT_Description("Alle Vorschauen ausblenden");

                    Guid selectedModelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                    Class_Model result = Class_Model_List.Class_Model_Find(selectedModelGuid).Result;
                    if (result == null)
                        return;

                    if (result.Pro_Model_Gender == -1)
                    {
                        CMI_Promo_Add.Visible = true;
                        CMI_Anzeigen.Enabled = false;
                        CMI_Aufnahme.Enabled = false;
                        CMI_Favorite.Enabled = false;
                        CMI_Deaktivieren.Enabled = false;
                        CMI_Info.Enabled = false;
                        CMI_Delete.Enabled = false;
                        CMI_Galerie.Enabled = false;
                        CMI_Optionen.Enabled = false;
                        CMI_Folder_Open.Enabled = false;
                    }
                    else
                    {
                        CMI_Promo_Add.Visible = false;
                        CMI_Anzeigen.Enabled = true;
                        CMI_Aufnahme.Enabled = true;
                        CMI_Favorite.Enabled = true;
                        CMI_Deaktivieren.Enabled = true;
                        CMI_Info.Enabled = true;
                        CMI_Delete.Enabled = true;
                        CMI_Galerie.Enabled = true;
                        CMI_Optionen.Enabled = true;
                        CMI_Folder_Open.Enabled = true;
                    }

                    if (result.Get_Pro_Model_Online() && !Parameter.URL_Response(result.Pro_Model_M3U8).Result)
                        CMI_Stream_Refresh.Visible = true;
                    else
                        CMI_Stream_Refresh.Visible = false;

                    if (!result.Get_Pro_Model_Online())
                        CMI_Online_Check.Visible = true;
                    else
                        CMI_Online_Check.Visible = false;

                    if (result.Pro_Model_Visible)
                        CMI_Anzeigen.Text = TXT.TXT_Description("Verbergen");
                    else
                        CMI_Anzeigen.Text = TXT.TXT_Description("Einblenden");

                    if (!result.Pro_Model_Record)
                    {
                        CMI_Aufnahme.Image = Resources.RecordAutomatic16;
                        CMI_Aufnahme.Text = TXT.TXT_Description("Automatische Aufnahme starten");
                    }
                    else
                    {
                        CMI_Aufnahme.Image = Resources.control_stop_icon;
                        CMI_Aufnahme.Text = TXT.TXT_Description("Keine Aufnahme");
                    }

                    if (result.Pro_Model_Favorite)
                    {
                        CMI_Favorite.Image = Resources.Favorite16;
                        CMI_Favorite.Text = TXT.TXT_Description("aus Favoriten löschen");
                    }
                    else
                    {
                        CMI_Favorite.Image = Resources.FavoriteDeaktiv16;
                        CMI_Favorite.Text = TXT.TXT_Description("zu Favoriten hinzufügen");
                    }
                    CMI_Deaktivieren.Checked = !result.Pro_Model_Deaktiv;

                    Class_Website classWebsite = Sites.Website_Find(result.Pro_Website_ID);
                    if (classWebsite != null)
                    {
                        CMI_Webseite.Text = classWebsite.Pro_Name + " " + TXT.TXT_Description("Webseite öffnen");
                        CMI_Webseite.Image = new Bitmap(classWebsite.Pro_Image, 16, 16);
                        CMI_Webseite.Tag = classWebsite.Pro_ID;
                    }

                    if (result.Pro_Website_ID > -1)
                    {
                        CMI_Webseite.Visible = true;
                        CMI_Aufnahme.Visible = true;
                        CMI_Online_Check.Visible = true;
                    }
                    else
                    {
                        CMI_Webseite.Visible = false;
                        CMI_Aufnahme.Visible = false;
                        CMI_Online_Check.Visible = false;
                    }

                    if (Show_All)
                    {
                        CMI_Alle_Ausblenden.Visible = true;
                        CMI_Alle_Anzeigen.Visible = false;
                    }
                    else
                    {
                        CMI_Alle_Anzeigen.Visible = true;
                        CMI_Alle_Ausblenden.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Model_Kanal_DropDown_Opening");
            }
        }

        private void CMI_Favorite_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null)
                    return;
                result.Pro_Model_Favorite = !result.Pro_Model_Favorite;
                GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_Favorite"].Value = result.Pro_Model_Favorite;
                GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_Online_Day"].Value = result.Pro_Model_Online_Day;
                result.Model_Online_Changed();
                GRV_Model_Kanal.Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Favoriten_Click");
            }
        }

        private void CMI_Info_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null)
                    return;
                Dialog_Model_Info dialogModelInfo = new Dialog_Model_Info();
                dialogModelInfo.TXB_Memo.Text = result.Pro_Model_Info.ToString();
                dialogModelInfo.StartPosition = FormStartPosition.CenterParent;
                if (dialogModelInfo.ShowDialog() != DialogResult.OK)
                    return;
                result.Pro_Model_Info = dialogModelInfo.TXB_Memo.Text.ToString();
                result.Model_Online_Changed();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Info_Click");
            }
        }

        private void CMI_Alle_Anzeigen_Click(object sender, EventArgs e)
        {
            try
            {
                Show_All = !Show_All;
            }
            catch (Exception ex)
            {
                // Log or handle exception
            }
        }

        private void CMI_Anzeigen_Click(object sender, EventArgs e)
        {
            try
            {
                if (GRV_Model_Kanal.SelectedRows.Count != 1)
                    return;
                Guid modelGuid = (Guid)GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_GUID"].Value;
                Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                if (result == null)
                    return;
                result.Pro_Model_Visible = !result.Pro_Model_Visible;
                GRV_Model_Kanal.SelectedRows[0].Cells["Pro_Model_Visible"].Value = result.Pro_Model_Visible;
                result.Model_Online_Changed();
                GRV_Model_Kanal.Refresh();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Anzeigen_Click");
            }
        }

        private void GRV_Model_Load()
        {
            try
            {
                Pri_Font_Favorite_Deaktiv = new Font(GRV_Model_Kanal.Font, FontStyle.Bold | FontStyle.Strikeout);
                Pri_Font_Favorite_Aktiv = new Font(GRV_Model_Kanal.Font, FontStyle.Bold);
                Pri_Font_Aktiv = new Font(GRV_Model_Kanal.Font, FontStyle.Regular);
                Pri_Font_Deaktiv = new Font(GRV_Model_Kanal.Font, FontStyle.Strikeout);

                GRV_Model_Kanal.AutoGenerateColumns = false;
                GRV_Model_Kanal.AllowUserToOrderColumns = true; // Equivalent to EnableSorting
                GRV_Model_Kanal.Columns.Clear(); // Clear existing columns

                // Add columns
                GRV_Model_Kanal.Columns.Add(new DataGridViewImageColumn() { Name = "COL_Image_Gender", Width = 20, Resizable = DataGridViewTriState.False });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_GUID", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { Width = 20, Resizable = DataGridViewTriState.False, Visible = false }); // Placeholder for original column
                GRV_Model_Kanal.Columns.Add(new DataGridViewImageColumn() { Name = "COL_Image_Site", Width = 20, Resizable = DataGridViewTriState.False });
                GRV_Model_Kanal.Columns.Add(new DataGridViewImageColumn() { Name = "COL_Image_Record", Width = 20, Resizable = DataGridViewTriState.False });
                GRV_Model_Kanal.Columns.Add(new DataGridViewImageColumn() { Name = "COL_Image_Files", Width = 20, Resizable = DataGridViewTriState.False });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Name", HeaderText = TXT.TXT_Description("Model"), Resizable = DataGridViewTriState.True });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Last_Online", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Record", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Visible", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Online", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Online_Day", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewCheckBoxColumn() { DataPropertyName = "Pro_Model_Favorite", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewCheckBoxColumn() { DataPropertyName = "Pro_Model_Deaktiv", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Info", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Birthday", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Gender", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Pro_Model_Records", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Group_LastOnline", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Group_Provider", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Group_Gender", Visible = false });
                GRV_Model_Kanal.Columns.Add(new DataGridViewCheckBoxColumn() { DataPropertyName = "COL_Model_Promo", Visible = false });

                // Set default sort order (no direct equivalent for SortDescriptors in DataGridView, need to sort manually or bind to a sorted data source)
                // For initial sort, you might sort the underlying data source or apply a custom sort.
                // For simplicity, we'll assume the data is loaded in the desired order or sorted later.

                GRV_Model_Kanal.RowHeadersVisible = false; // Equivalent to ShowRowHeaderColumn = false
                GRV_Model_Kanal.AllowUserToAddRows = false; // Equivalent to AllowAddNewRow = false
                GRV_Model_Kanal.AllowUserToDeleteRows = false; // Equivalent to AllowDeleteRow = false
                GRV_Model_Kanal.EditMode = DataGridViewEditMode.EditProgrammatically; // Equivalent to AllowEditRow = false
                GRV_Model_Kanal.ReadOnly = true;

                GRV_Model_Grouping();
                Filter_Set();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Load");
            }
        }

        private void GRV_Model_Kanal_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!(e is MouseEventArgs mouseEventArgs))
                    return;
                if (mouseEventArgs.Button != MouseButtons.Left)
                    return;

                DataGridView.HitTestInfo hitTest = GRV_Model_Kanal.HitTest(mouseEventArgs.X, mouseEventArgs.Y);
                if (hitTest.Type == DataGridViewHitTestType.Cell)
                {
                    GRV_Model_Kanal.Rows[hitTest.RowIndex].Selected = true;
                    GRV_Model_Kanal.CurrentCell = GRV_Model_Kanal.Rows[hitTest.RowIndex].Cells[hitTest.ColumnIndex];

                    if (GRV_Model_Kanal.SelectedRows.Count != 1)
                        return;
                    Galerie_Öffnen();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_DoubleClick");
            }
        }

        private async void GRV_Model_Kanal_CellPaint(object sender, DataGridViewCellPaintingEventArgs e)
        {
            await Task.CompletedTask;
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewCell cell = GRV_Model_Kanal.Rows[e.RowIndex].Cells["Pro_Model_Name"];
                    if (cell != null)
                    {
                        bool isVisible = (bool)GRV_Model_Kanal.Rows[e.RowIndex].Cells["Pro_Model_Visible"].Value;
                        cell.Style.ForeColor = isVisible ? Parameter.Fore_Color_Dark : Parameter.Fore_Color_Hell;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_CellPaint");
            }
        }

        private void Set_Cell_Value(DataGridViewCell Cell, object New_Value)
        {
            try
            {
                if (Cell.Value == null || !Cell.Value.Equals(New_Value))
                {
                    Cell.Value = New_Value;
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
            }
        }

        private async void GRV_Row_Fill(DataGridViewRow GRV_Row, Class_Model Model_Class)
        {
            await Task.Run(() =>
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)(() => Aktualisiere_Zellen(GRV_Row, Model_Class)));
                }
                else
                {
                    Aktualisiere_Zellen(GRV_Row, Model_Class);
                }
            });
        }

        private void Aktualisiere_Zellen(DataGridViewRow GRV_Row, Class_Model Model_Class)
        {
            try
            {
                if (Model_Class.Pro_Model_GUID == Guid.Empty)
                    return;

                Set_Cell_Value(GRV_Row.Cells["Pro_Model_GUID"], Model_Class.Pro_Model_GUID);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Name"], Model_Class.Pro_Model_Description.Length == 0 ? Model_Class.Pro_Model_Name : Model_Class.Pro_Model_Description);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Online"], Model_Class.Get_Pro_Model_Online());
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Record"], Model_Class.Pro_Model_Record);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Visible"], Model_Class.Pro_Model_Visible);
                Set_Cell_Value(GRV_Row.Cells["Last_Online"], Model_Class.Pro_Last_Online);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Online_Day"], Model_Class.Pro_Model_Online_Day);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Favorite"], Model_Class.Pro_Model_Favorite);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Info"], Model_Class.Pro_Model_Info?.ToString());
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Birthday"], Model_Class.Pro_Model_Birthday);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Gender"], Model_Class.Pro_Model_Gender);
                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Deaktiv"], Model_Class.Pro_Model_Deaktiv);
                Set_Cell_Value(GRV_Row.Cells["Group_LastOnline"], Model_Class.Pro_Model_Online_Day);
                Set_Cell_Value(GRV_Row.Cells["Group_Provider"], Sites.Website_Find(Model_Class.Pro_Website_ID)?.Pro_Name);
                Set_Cell_Value(GRV_Row.Cells["Group_Gender"], Model_Class.Pro_Model_Gender_Text.ToString());
                Set_Cell_Value(GRV_Row.Cells["COL_Image_Gender"], Model_Class.Pro_Model_Gender_Image);
                Set_Cell_Value(GRV_Row.Cells["COL_Image_Site"], Model_Class.Pro_Website_Image);
                Set_Cell_Value(GRV_Row.Cells["COL_Model_Promo"], Model_Class.Pro_Model_Promo);

                if (Model_Class.Pro_Model_Stream_Record != null)
                {
                    if (Model_Class.Pro_Model_Visible)
                        Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.RecordView16);
                    else
                        Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.RecordAutomatic16);
                }
                else if (Model_Class.Pro_Model_Record)
                    Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.RecordWait16);
                else if (Model_Class.Pro_Model_Visible && Model_Class.Get_Pro_Model_Online())
                {
                    Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.View16);
                    if (Model_Class.Timer_Online_Change.BGW_Result > 1)
                        Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.View_Key16);
                }
                else if (Model_Class.Get_Pro_Model_Online())
                {
                    Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.Model_Online16);
                    if (Model_Class.Timer_Online_Change.BGW_Result > 1)
                        Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.Model_Online_Key16);
                }
                else
                    Set_Cell_Value(GRV_Row.Cells["COL_Image_Record"], Resources.Model_Offline16);

                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Model_Class.Pro_Model_Directory);
                    if (directoryInfo.Exists)
                    {
                        int fileCount = directoryInfo.GetFiles("*.mp4").Length + directoryInfo.GetFiles("*.ts").Length;
                        if (fileCount > 0)
                        {
                            Set_Cell_Value(GRV_Row.Cells["Pro_Model_Records"], 1);
                            Set_Cell_Value(GRV_Row.Cells["COL_Image_Files"], Resources.Play16);
                            List<FileInfo> fileInfoList = List_Files(directoryInfo.FullName);
                            if (fileInfoList.Count > 0 && fileInfoList[0].CreationTime > Model_Class.Pro_Model_Galery_Last_Visit)
                            {
                                Set_Cell_Value(GRV_Row.Cells["Pro_Model_Records"], 2);
                                Set_Cell_Value(GRV_Row.Cells["COL_Image_Files"], Resources.Play16New);
                            }
                        }
                        else
                        {
                            Set_Cell_Value(GRV_Row.Cells["Pro_Model_Records"], 0);
                            Set_Cell_Value(GRV_Row.Cells["COL_Image_Files"], null!);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Form_Main.GRV_Row_Fill");
                }

                bool isFavorite = (bool)GRV_Row.Cells["Pro_Model_Favorite"].Value;
                bool isDeaktiv = (bool)GRV_Row.Cells["Pro_Model_Deaktiv"].Value;

                if (isFavorite)
                {
                    GRV_Row.Cells["Pro_Model_Name"].Style.Font = isDeaktiv ? Pri_Font_Favorite_Deaktiv : Pri_Font_Favorite_Aktiv;
                }
                else
                {
                    GRV_Row.Cells["Pro_Model_Name"].Style.Font = isDeaktiv ? Pri_Font_Deaktiv : Pri_Font_Aktiv;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Row_Fill");
            }
        }

        public List<FileInfo> List_Files(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            return files.Where(x => x.Extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase) ||
                                    x.Extension.Equals(".ts", StringComparison.OrdinalIgnoreCase) ||
                                    x.Extension.Equals(".mov", StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(x => x.CreationTime)
                        .ToList();
        }

        private void GRV_Model_Grouping()
        {
            try
            {
                int groupingOption = int.Parse(IniFile.Read(Parameter.INI_Common, "Main", "Grouping", "0"));
                string sortColumn = "";

                // DataGridView does not have direct grouping like Telerik's RadGridView.
                // Grouping would typically involve sorting and then adding group header rows manually,
                // or using a BindingSource with custom grouping logic.
                // For this migration, we'll just set the sort order if grouping is implied by sorting.

                switch (groupingOption)
                {
                    case 0:
                        sortColumn = "Group_LastOnline";
                        break;
                    case 1:
                        sortColumn = "Group_Provider";
                        break;
                    case 2:
                        sortColumn = "Group_Gender";
                        break;
                }

                // Clear existing sort and apply new one if a column is specified
                if (!string.IsNullOrEmpty(sortColumn))
                {
                    // Clear previous sort
                    foreach (DataGridViewColumn column in GRV_Model_Kanal.Columns)
                    {
                        column.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }

                    // Apply new sort
                    DataGridViewColumn targetColumn = GRV_Model_Kanal.Columns[sortColumn];
                    if (targetColumn != null)
                    {
                        GRV_Model_Kanal.Sort(targetColumn, ListSortDirection.Ascending);
                        targetColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                }

                CMI_Geschlecht.Checked = groupingOption == 2;
                CMI_LastOnline.Checked = groupingOption == 0;
                CMI_Provider.Checked = groupingOption == 1;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_GroupSummaryEvaluate");
            }
        }

        // GRV_Model_Kanal_GroupSummaryEvaluate is Telerik specific, no direct native equivalent.
        // Grouping in DataGridView would require custom drawing or adding rows.
        // private void GRV_Model_Kanal_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e) { }

        private void GRV_GroupItem_Click(object Sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = Sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Tag != null)
            {
                IniFile.Write(Parameter.INI_Common, "Main", "Grouping", menuItem.Tag.ToString());
                GRV_Model_Grouping();
            }
        }

        private void GRV_Model_Kanal_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView.HitTestInfo hitTest = GRV_Model_Kanal.HitTest(e.X, e.Y);
                    if (hitTest.Type == DataGridViewHitTestType.Cell)
                    {
                        GRV_Model_Kanal.Rows[hitTest.RowIndex].Selected = true;
                        GRV_Model_Kanal.CurrentCell = GRV_Model_Kanal.Rows[hitTest.RowIndex].Cells[hitTest.ColumnIndex];
                    }
                }
                else
                {
                    OnMouseDown(e);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_MouseDown");
            }
        }

        private void GRV_Model_Kanal_ContextMenuOpening(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    GRV_Model_Kanal.Rows[e.RowIndex].Selected = true;
                    GRV_Model_Kanal.CurrentCell = GRV_Model_Kanal.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    e.ContextMenuStrip = CME_Model_Kanal;
                }
                else
                {
                    e.ContextMenuStrip = null; // No context menu for header or empty space
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_ContextMenuOpening");
            }
        }

        private void GRV_Model_Kanal_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (CME_Model_Kanal.Visible) // Check if context menu is open
                    return;

                DataGridView.HitTestInfo hitTest = GRV_Model_Kanal.HitTest(e.X, e.Y);
                if (hitTest.Type == DataGridViewHitTestType.Cell && hitTest.RowIndex >= 0)
                {
                    Guid modelGuid = (Guid)GRV_Model_Kanal.Rows[hitTest.RowIndex].Cells["Pro_Model_GUID"].Value;
                    Class_Model result = Class_Model_List.Class_Model_Find(modelGuid).Result;
                    if (result != null)
                    {
                        if (Show_Model_Info_GUID != result.Pro_Model_GUID)
                        {
                            Show_Model_Info_GUID = result.Pro_Model_GUID;
                            Control_Model_Info1.Control_Visible = false;
                            Control_Model_Info1.Pro_Model = result;

                            Point location = e.Location;
                            int x = location.X + 20;
                            int y = location.Y + CBB_Model_Kanal.Height + 20; // Assuming CBB_Model_Kanal is a ToolStrip at the top
                            if (y + Control_Model_Info1.Height > Height)
                            {
                                y = Height - CBB_Model_Kanal.Height - Control_Model_Info1.Height;
                            }
                            Control_Model_Info1.Location = new Point(x, y);
                            Control_Model_Info1.Control_Visible = true;
                        }
                    }
                    else
                    {
                        Control_Model_Info1.Pro_Model_Preview = null;
                        Control_Model_Info1.Pro_Model_GUID = Guid.Empty;
                        Control_Model_Info1.Pro_Model_Info = "";
                        Control_Model_Info1.Control_Visible = false;
                    }
                }
                else
                {
                    Control_Model_Info1.Pro_Model_Preview = null;
                    Control_Model_Info1.Pro_Model_GUID = Guid.Empty;
                    Control_Model_Info1.Pro_Model_Info = "";
                    Control_Model_Info1.Control_Visible = false;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_MouseMove");
            }
        }

        private void GRV_Model_Kanal_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Control_Model_Info1.Pro_Model_Preview = null;
                Control_Model_Info1.Pro_Model_GUID = Guid.Empty;
                Control_Model_Info1.Pro_Model_Info = "";
                Control_Model_Info1.Visible = false;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_MouseLeave");
            }
        }

        private string GroupSummary_LastOnline(int Lastonline_Value)
        {
            string str;
            try
            {
                switch (Lastonline_Value)
                {
                    case -4:
                        str = TXT.TXT_Description("Empfehlung");
                        break;
                    case -3:
                        str = TXT.TXT_Description("Favoriten");
                        break;
                    case -2:
                        str = TXT.TXT_Description("Record");
                        break;
                    case -1:
                        str = TXT.TXT_Description("Online");
                        break;
                    case 0:
                        str = TXT.TXT_Description("Heute");
                        break;
                    case 1:
                        str = TXT.TXT_Description("vor 1 Tag");
                        break;
                    case 2:
                        str = TXT.TXT_Description("vor 2 Tagen");
                        break;
                    case 3:
                        str = TXT.TXT_Description("vor 3 Tagen");
                        break;
                    case 4:
                        str = TXT.TXT_Description("vor 4 Tagen");
                        break;
                    case 5:
                        str = TXT.TXT_Description("vor 5 Tagen");
                        break;
                    case 6:
                        str = TXT.TXT_Description("vor 6 Tagen");
                        break;
                    default:
                        if (Lastonline_Value > 6)
                        {
                            str = TXT.TXT_Description("älter als 1 Woche");
                        }
                        else
                        {
                            str = ""; // Default for unhandled cases
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.GRV_Model_Kanal_GroupSummaryEvaluate");
                str = "";
            }
            return str;
        }

        private void DDI_Site_Click(ToolStripMenuItem sender, EventArgs e)
        {
            if (sender != null && sender.Name != null)
            {
                IniFile.Write(Parameter.INI_Common, "Showall", sender.Name, sender.Checked.ToString());
                Class_Website website = Sites.Website_Find(int.Parse(sender.Name));
                if (website != null)
                {
                    website.Pro_ShowAll = sender.Checked;
                }
            }
        }

        private void CBT_ShowAll_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                // For a ToolStripButton, Checked property directly reflects its toggle state
                // Assuming this button acts as a toggle
                Show_All = !Show_All; // Toggle the state
                ((ToolStripButton)sender).Checked = Show_All; // Update button's checked state
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CBT_ShowAll_CheckStateChanged");
            }
        }

        private async void DDI_Alle_Anzeigen_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;
            try
            {
                foreach (ToolStripItem item in CBD_Liste_Sender.DropDownItems)
                {
                    if (item is ToolStripMenuItem radMenuItem && int.TryParse(radMenuItem.Name, out int websiteId))
                    {
                        Class_Website classWebsite = Sites.Website_Find(websiteId);
                        if (classWebsite != null)
                        {
                            classWebsite.Pro_ShowAll = true;
                            IniFile.Write(Parameter.INI_Common, "Showall", radMenuItem.Name, "True");
                            radMenuItem.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.DDI_Alle_Anzeigen_Click");
            }
        }

        private void CBB_Hinzufügen_Click(object sender, EventArgs e) => Chanel_Add();

        private async void CBB_Aufnahmen_Heute_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;
            try
            {
                List<Video_File> FI_List = new List<Video_File>();
                using (OleDbConnection oleDbConnection = new OleDbConnection())
                {
                    oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                    await oleDbConnection.OpenAsync();
                    if (oleDbConnection.State == ConnectionState.Open)
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter("Select Top 200 * from DT_Record where Record_Beginn > " + ValueBack.Get_SQL_Date(DateTime.Now.AddDays(-1.0)) + " ORDER BY Record_Beginn DESC ", oleDbConnection.ConnectionString))
                        {
                            using (DataSet dataSet = new DataSet())
                            {
                                adapter.Fill(dataSet, "DT_Record");
                                await oleDbConnection.OpenAsync();
                                using (DataView dataView = new DataView(dataSet.Tables["DT_Record"], null, "Record_Beginn DESC", DataViewRowState.CurrentRows))
                                {
                                    foreach (DataRowView dataRowView in dataView)
                                    {
                                        Guid userGuid = (Guid)dataRowView["User_GUID"];
                                        Class_Model result = Class_Model_List.Class_Model_Find(userGuid).Result;
                                        if (result != null)
                                        {
                                            Video_File videoFile = new Video_File();
                                            videoFile.Pro_Pfad = result.Pro_Model_Directory + "\\" + dataRowView["Record_Name"].ToString();
                                            videoFile.Pro_Start = (DateTime)dataRowView["Record_Beginn"];
                                            videoFile.Pro_Video_GUID = (Guid)dataRowView["Record_GUID"];
                                            videoFile.Pro_Bezeichnung = dataRowView["Record_Name"].ToString();
                                            videoFile.Pro_Favorite = (bool)dataRowView["Record_Favorit"];
                                            videoFile.Pro_Model_GUID = result.Pro_Model_GUID;
                                            videoFile.Pro_Model_Name = result.Pro_Model_Name;
                                            videoFile.Pro_FrameRate = (int)dataRowView["Record_FrameRate"];
                                            videoFile.Pro_Resolution = dataRowView["Record_Resolution"].ToString();
                                            videoFile.Pro_Video_Länge = (int)dataRowView["Record_Länge_Minuten"];
                                            videoFile.Pro_Website_ID = result.Pro_Website_ID;
                                            videoFile.Pro_IsInDB = true;
                                            if (dataRowView["Record_Ende"] != DBNull.Value)
                                                videoFile.Pro_Ende = (DateTime)dataRowView["Record_Ende"];
                                            videoFile.Pro_Run_Record = result.Pro_Model_Stream_Record != null && result.Pro_Model_Stream_Record.Pro_Recordname == videoFile.Pro_Pfad;
                                            FI_List.Add(videoFile);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (FI_List.Count <= 0)
                    return;
                Dialog_Videothek_Open(FI_List);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CBB_Aufnahmen_Heute_Click");
            }
        }

        private static void Dialog_Videothek_Open(List<Video_File> FI_List)
        {
            new Thread(() =>
            {
                using (Dialog_Videothek dialog = new Dialog_Videothek(FI_List))
                {
                    dialog.ShowDialog();
                }
            }).Start();
        }

        private static void CBB_CamsRecorder_Click(object sender, EventArgs e)
        {
            Process.Start("https://xstreamon.com/");
        }

        private async void CBB_Favoriten_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;
            try
            {
                List<Video_File> Fi_List = new List<Video_File>();
                using (OleDbConnection oleDbConnection = new OleDbConnection())
                {
                    oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                    await oleDbConnection.OpenAsync();
                    if (oleDbConnection.State == ConnectionState.Open)
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from DT_Record where Record_Favorit = True", oleDbConnection.ConnectionString))
                        {
                            using (DataSet dataSet = new DataSet())
                            {
                                adapter.Fill(dataSet, "DT_Record");
                                await oleDbConnection.OpenAsync();
                                using (DataView dataView = new DataView(dataSet.Tables["DT_Record"], null, "Record_Beginn DESC", DataViewRowState.CurrentRows))
                                {
                                    foreach (DataRowView dataRowView in dataView)
                                    {
                                        Guid userGuid = (Guid)dataRowView["User_GUID"];
                                        Class_Model result = Class_Model_List.Class_Model_Find(userGuid).Result;
                                        if (result != null)
                                        {
                                            Video_File videoFile = new Video_File()
                                            {
                                                Pro_Video_GUID = (Guid)dataRowView["Record_GUID"],
                                                Pro_Bezeichnung = dataRowView["Record_Name"].ToString(),
                                                Pro_Favorite = (bool)dataRowView["Record_Favorit"],
                                                Pro_Model_Name = result.Pro_Model_Name,
                                                Pro_Model_GUID = result.Pro_Model_GUID,
                                                Pro_FrameRate = (int)dataRowView["Record_FrameRate"],
                                                Pro_Resolution = dataRowView["Record_Resolution"].ToString(),
                                                Pro_Video_Länge = (int)dataRowView["Record_Länge_Minuten"],
                                                Pro_Website_ID = result.Pro_Website_ID,
                                                Pro_IsInDB = true
                                            };
                                            if (dataRowView["Record_Beginn"] != DBNull.Value)
                                                videoFile.Pro_Start = (DateTime)dataRowView["Record_Beginn"];
                                            if (dataRowView["Record_Ende"] != DBNull.Value)
                                                videoFile.Pro_Ende = (DateTime)dataRowView["Record_Ende"];

                                            string favoritePath = Modul_Ordner.Favoriten_Pfad() + "\\" + dataRowView["Record_Name"].ToString();
                                            videoFile.Pro_Pfad = File.Exists(favoritePath) ? favoritePath : result.Pro_Model_Directory + "\\" + dataRowView["Record_Name"].ToString();
                                            Fi_List.Add(videoFile);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Directory.Exists(Modul_Ordner.Favoriten_Pfad()))
                {
                    string[] files = Directory.GetFiles(Modul_Ordner.Favoriten_Pfad());
                    foreach (string str in files)
                    {
                        FileInfo fileInfo = new FileInfo(str);
                        if (fileInfo.Extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase) || fileInfo.Extension.Equals(".ts", StringComparison.OrdinalIgnoreCase))
                        {
                            bool flag = false;
                            foreach (Video_File vbLocalFi in Fi_List)
                            {
                                if (vbLocalFi.Pro_Pfad.Equals(str, StringComparison.OrdinalIgnoreCase))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                Video_File videoFile = new Video_File()
                                {
                                    Pro_Pfad = str
                                };
                                if (File.Exists(str + ".vdb"))
                                {
                                    using (DataSet dataSet = new DataSet())
                                    {
                                        dataSet.ReadXml(str + ".vdb");
                                        if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow row = dataSet.Tables[0].Rows[0];
                                            videoFile.Pro_Model_Name = row["Channel_Name"].ToString();
                                            videoFile.Pro_Bezeichnung = row["Record_Name"].ToString();
                                            videoFile.Pro_Start = (DateTime)row["Record_Beginn"];
                                            videoFile.Pro_Ende = row["Record_Ende"] != DBNull.Value ? (DateTime)row["Record_Ende"] : default(DateTime);
                                            videoFile.Pro_FrameRate = (int)row["Record_FrameRate"];
                                            videoFile.Pro_Resolution = row["Record_Resolution"].ToString();
                                            videoFile.Pro_Video_Länge = (int)row["Record_Länge_Minuten"];
                                        }
                                    }
                                }
                                else
                                {
                                    videoFile.Pro_Start = fileInfo.CreationTime;
                                    videoFile.Pro_Bezeichnung = fileInfo.Name;
                                }
                                Fi_List.Add(videoFile);
                            }
                        }
                    }
                }
                if (Fi_List.Count <= 0)
                    return;
                Dialog_Videothek_Open(Fi_List);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CBB_Favoriten_Click");
            }
        }

        private async void CBB_Einstellungen_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;
            try
            {
                Dialog_Einstellungen dialogEinstellungen = new Dialog_Einstellungen();
                dialogEinstellungen.StartPosition = FormStartPosition.CenterParent;
                using (dialogEinstellungen)
                {
                    dialogEinstellungen.ShowDialog();
                    Text = "XstreaMon " + Parameter.Programlizenz.Lizenz_Programmbezeichnung;
                    GRV_Model_Kanal.Refresh();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CBB_Einstellungen_Click");
            }
        }

        private void CBB_Löschen_Click(object sender, EventArgs e)
        {
            try
            {
                Model_Delete();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CBB_Löschen_Click");
            }
        }

        private void CBT_Suche_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // DataGridView filtering is typically done via a BindingSource or custom logic.
                // This is a simplified example using RowFilter if the DataSource is a DataTable/DataView.
                // If not, you'd iterate rows and set visibility or re-bind data.
                if (GRV_Model_Kanal.DataSource is DataTable dt)
                {
                    DataView dv = dt.DefaultView;
                    if (CBT_Suche.TextBox.Text.Length > 0)
                    {
                        dv.RowFilter = $"Pro_Model_Name LIKE '%{CBT_Suche.TextBox.Text}%'";
                    }
                    else
                    {
                        dv.RowFilter = string.Empty;
                        Filter_Set();
                    }
                }
                else
                {
                    // Fallback for other data sources: iterate and hide/show rows
                    foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                    {
                        if (row.Cells["Pro_Model_Name"].Value != null)
                        {
                            string modelName = row.Cells["Pro_Model_Name"].Value.ToString();
                            row.Visible = modelName.Contains(CBT_Suche.TextBox.Text, StringComparison.OrdinalIgnoreCase);
                        }
                    }
                    if (CBT_Suche.TextBox.Text.Length == 0)
                    {
                        Filter_Set(); // Reapply other filters
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CBT_Suche_KeyUp");
            }
        }

        private void Filter_Set()
        {
            try
            {
                if (CBT_Suche.TextBox.Text.Length != 0)
                    return;

                bool filterCouple = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Couple", "True"));
                bool filterMale = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Male", "True"));
                bool filterFemale = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Female", "True"));
                bool filterTrans = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Trans", "True"));
                bool filterUnknow = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Unknow", "True"));
                bool filterOffline = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Offline", "True"));
                bool filterOnline = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Online", "True"));
                bool filterRecord = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "Record", "True"));
                bool filterAutoRecord = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "AutoRecord", "True"));
                bool filterRecordNew = bool.Parse(IniFile.Read(Parameter.INI_Common, "Filter", "RecordNew", "True"));

                // DataGridView filtering is typically done via a BindingSource or custom logic.
                // This is a simplified example using RowFilter if the DataSource is a DataTable/DataView.
                // For complex filters, you might need to iterate rows or use a more advanced data binding approach.

                if (GRV_Model_Kanal.DataSource is DataTable dt)
                {
                    DataView dv = dt.DefaultView;
                    List<string> genderFilters = new List<string>();
                    if (filterCouple) genderFilters.Add("Pro_Model_Gender = 2");
                    if (filterFemale) genderFilters.Add("Pro_Model_Gender = 0");
                    if (filterMale) genderFilters.Add("Pro_Model_Gender = 1");
                    if (filterTrans) genderFilters.Add("Pro_Model_Gender = 3");
                    if (filterUnknow) genderFilters.Add("Pro_Model_Gender = 4");

                    List<string> statusFilters = new List<string>();
                    if (filterAutoRecord) statusFilters.Add("Pro_Model_Record = True");
                    if (filterRecord) statusFilters.Add("Pro_Model_Records > 0");
                    if (filterRecordNew) statusFilters.Add("Pro_Model_Records = 2");
                    if (filterOnline) statusFilters.Add("Pro_Model_Online = True");
                    if (filterOffline) statusFilters.Add("Pro_Model_Online = False");

                    string genderFilter = string.Join(" OR ", genderFilters);
                    string statusFilter = string.Join(" OR ", statusFilters);

                    if (genderFilters.Count == 0 || statusFilters.Count == 0)
                    {
                        if (genderFilters.Count > 0) dv.RowFilter = genderFilter;
                        else if (statusFilters.Count > 0) dv.RowFilter = statusFilter;
                        else dv.RowFilter = string.Empty; // No filters applied
                    }
                    else
                    {
                        // Combine gender and status filters with AND
                        dv.RowFilter = $"({genderFilter}) AND ({statusFilter})";

                        // Add promo filter if applicable (original code had this as a separate filter descriptor)
                        // This part is tricky with simple RowFilter, might need more complex logic or separate filtering steps.
                        // For now, assuming promo models are always shown if online, regardless of other filters.
                        // This would require a more advanced filtering mechanism than simple RowFilter.
                        // For simplicity, we'll just apply the combined gender/status filter.
                    }
                }
                else
                {
                    // Fallback for other data sources: iterate and hide/show rows
                    foreach (DataGridViewRow row in GRV_Model_Kanal.Rows)
                    {
                        bool showRow = true;

                        // Gender filter
                        int gender = (int)row.Cells["Pro_Model_Gender"].Value;
                        bool genderMatch = false;
                        if (filterCouple && gender == 2) genderMatch = true;
                        if (filterFemale && gender == 0) genderMatch = true;
                        if (filterMale && gender == 1) genderMatch = true;
                        if (filterTrans && gender == 3) genderMatch = true;
                        if (filterUnknow && gender == 4) genderMatch = true;
                        if (!genderMatch && (filterCouple || filterFemale || filterMale || filterTrans || filterUnknow)) showRow = false;

                        // Status filter
                        bool isAutoRecord = (bool)row.Cells["Pro_Model_Record"].Value;
                        int records = (int)row.Cells["Pro_Model_Records"].Value;
                        bool isOnline = (bool)row.Cells["Pro_Model_Online"].Value;

                        bool statusMatch = false;
                        if (filterAutoRecord && isAutoRecord) statusMatch = true;
                        if (filterRecord && records > 0) statusMatch = true;
                        if (filterRecordNew && records == 2) statusMatch = true;
                        if (filterOnline && isOnline) statusMatch = true;
                        if (filterOffline && !isOnline) statusMatch = true;
                        if (!statusMatch && (filterAutoRecord || filterRecord || filterRecordNew || filterOnline || filterOffline)) showRow = false;

                        // Promo filter (simplified)
                        bool isPromo = (bool)row.Cells["COL_Model_Promo"].Value;
                        if (isPromo && isOnline) showRow = true; // Always show online promo models

                        row.Visible = showRow;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.FilterSet");
            }
        }

        private void CMI_Couple_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Couple", CMI_Couple.Checked.ToString());
            Filter_Set();
        }

        private void CMI_Female_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Female", CMI_Female.Checked.ToString());
            Filter_Set();
        }

        private void CMI_Male_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Male", CMI_Male.Checked.ToString());
            Filter_Set();
        }

        private void CMI_Offline_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Offline", CMI_Offline.Checked.ToString());
            Filter_Set();
        }

        private void CMI_Online_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Online", CMI_Online.Checked.ToString());
            Filter_Set();
        }

        private void CMI_AutoRecord_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "AutoRecord", CMI_AutoRecord.Checked.ToString());
            Filter_Set();
        }

        private void CMI_Record_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Record", CMI_Record.Checked.ToString());
            Filter_Set();
        }

        private void CMI_Record_New_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "RecordNew", CMI_New_Records.Checked.ToString()); // Note: original used CMI_Record.IsChecked here
            Filter_Set();
        }

        private void CMI_Trans_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Trans", CMI_Trans.Checked.ToString());
            Filter_Set();
        }

        private void CMI_Unknow_Click(object sender, EventArgs e)
        {
            IniFile.Write(Parameter.INI_Common, "Filter", "Unknow", CMI_Unknow.Checked.ToString());
            Filter_Set();
        }

        private async void CBB_Site_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;
            ToolStripButton button = sender as ToolStripButton;
            if (button != null && button.Tag is int siteId)
            {
                Sites.WebOpen(Sites.Website_Find(siteId)?.Pro_Home_URL);
            }
        }

        private void CBT_Suche_TextChanged(object sender, EventArgs e)
        {
            if (CBT_Suche.TextBox.Text.Length == 0)
            {
                Filter_Set();
            }
        }

        private void Form_Main_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    if (Pri_Show_Visible)
                    {
                        Pri_Show_Visible = false;
                        Show_Visible_Run();
                    }
                }
                else
                {
                    if (!Pri_Show_Visible)
                    {
                        Pri_Show_Visible = true;
                        Show_Visible_Run();
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.FilterSet");
            }
        }

        private void CME_Preview_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                CMI_Favoriten.Text = TXT.TXT_Description("Favoriten gruppieren");
                CMI_Favoriten.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Preview", "Favoriten", "True"));
                CMI_Records.Text = TXT.TXT_Description("Aufnahmen gruppieren");
                CMI_Records.Checked = bool.Parse(IniFile.Read(Parameter.INI_Common, "Preview", "Records", "True"));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CME_Preview_DropDownOpening");
            }
        }

        private void CMI_Records_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Preview", "Records", CMI_Records.Checked.ToString());
                Stream_Panel_Set();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Records_Click");
            }
        }

        private void CMI_Favoriten_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Preview", "Favoriten", CMI_Favoriten.Checked.ToString());
                Stream_Panel_Set();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Favoriten_Click");
            }
        }

        private void Stream_Panel_Set()
        {
            try
            {
                List<Control_Stream> allStreams = new List<Control_Stream>();
                allStreams.AddRange(PAN_Record.Controls.OfType<Control_Stream>());
                allStreams.AddRange(PAN_Show.Controls.OfType<Control_Stream>());
                allStreams.AddRange(PAN_Favoriten.Controls.OfType<Control_Stream>());

                foreach (Control_Stream con in allStreams)
                {
                    con.Parent = Find_Preview_panel(con);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Form_Class_Record.CMI_Favoriten_Click");
            }
        }
    }
}
