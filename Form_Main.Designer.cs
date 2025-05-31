namespace XstreaMonNET8
{
    partial class Form_Main
    {
        private System.ComponentModel.IContainer components = null;

        // Controles principales
        internal System.Windows.Forms.DataGridView GRV_Model_Kanal;
        internal System.Windows.Forms.Panel RadPanel1;
        internal System.Windows.Forms.SplitContainer RadSplitContainer1;
        internal System.Windows.Forms.Panel PAN_Navigation;
        internal System.Windows.Forms.Panel PAN_Streams;
        internal System.Windows.Forms.FlowLayoutPanel PAN_Show;
        internal System.Windows.Forms.FlowLayoutPanel PAN_Record;
        internal System.Windows.Forms.FlowLayoutPanel PAN_Favoriten;

        // Menús contextuales
        internal System.Windows.Forms.ContextMenuStrip CME_Model_Kanal;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Promo_Add;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Aufnahme;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Stream_Refresh;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Anzeigen;
        internal System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem2;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Favorite;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Info;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Online_Check;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Deaktivieren;
        internal System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem5;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Gesehen;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Delete;
        internal System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem1;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Webseite;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Galerie;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Optionen;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Folder_Open;
        internal System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem6;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Ansicht;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Grouping;
        internal System.Windows.Forms.ToolStripMenuItem CMI_LastOnline;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Provider;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Geschlecht;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Filter;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Online;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Offline;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Record;
        internal System.Windows.Forms.ToolStripMenuItem CMI_New_Records;
        internal System.Windows.Forms.ToolStripMenuItem CMI_AutoRecord;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Female;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Male;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Couple;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Trans;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Unknow;
        internal System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem3;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Alle_Anzeigen;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Alle_Ausblenden;

        // Menú contextual para preview
        internal System.Windows.Forms.ContextMenuStrip CME_Preview;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Favoriten;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Records;

        // Barra de menú superior
        internal System.Windows.Forms.MenuStrip CBB_Model_Kanal;
        internal System.Windows.Forms.ToolStripMenuItem CBB_Hinzufügen;
        internal System.Windows.Forms.ToolStripMenuItem CBB_Löschen;
        internal System.Windows.Forms.ToolStripTextBox CBT_Suche;
        internal System.Windows.Forms.ToolStripMenuItem CBT_ShowAll;
        internal System.Windows.Forms.ToolStripMenuItem CBD_Liste_Sender;
        internal System.Windows.Forms.ToolStripMenuItem DDI_Alle_Anzeigen;
        internal System.Windows.Forms.ToolStripMenuItem CBB_Aufnahmen_Heute;
        internal System.Windows.Forms.ToolStripMenuItem CBB_Favoriten;
        internal System.Windows.Forms.ToolStripMenuItem CBB_Einstellungen;
        internal System.Windows.Forms.ToolStripMenuItem CBB_CamsRecorder;

        // Otros controles
        internal System.Windows.Forms.ProgressBar PGB_Disk;
        internal System.Windows.Forms.Label LAB_Drive;
        internal System.Windows.Forms.Label LAB_Warnung;
        internal System.Windows.Forms.Label RadLabel1;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.NotifyIcon Cam_Benachrichtigung;
        internal System.Windows.Forms.ContextMenuStrip CMS_Benachrichtigung;
        internal System.Windows.Forms.ToolStripMenuItem TMI_Benachrichtung_Anzeigen;

        // Si tienes un UserControl llamado Control_Model_Info, decláralo aquí:
        // internal Control_Model_Info Control_Model_Info1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // DataGridView
            this.GRV_Model_Kanal = new System.Windows.Forms.DataGridView();
            this.GRV_Model_Kanal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GRV_Model_Kanal.ReadOnly = true;
            this.GRV_Model_Kanal.Name = "GRV_Model_Kanal";
            this.GRV_Model_Kanal.TabIndex = 3;

            // Panel para el grid
            this.RadPanel1 = new System.Windows.Forms.Panel();
            this.RadPanel1.Controls.Add(this.GRV_Model_Kanal);
            this.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadPanel1.Name = "RadPanel1";
            this.RadPanel1.TabIndex = 1;

            // SplitContainer principal
            this.RadSplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadSplitContainer1.Name = "RadSplitContainer1";
            // Panel izquierdo
            this.PAN_Navigation = new System.Windows.Forms.Panel();
            this.PAN_Navigation.Controls.Add(this.RadPanel1);
            this.PAN_Navigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Navigation.Name = "PAN_Navigation";
            this.PAN_Navigation.TabIndex = 0;
            this.RadSplitContainer1.Panel1.Controls.Add(this.PAN_Navigation);
            // Panel derecho
            this.PAN_Streams = new System.Windows.Forms.Panel();
            this.PAN_Streams.AutoScroll = true;
            this.PAN_Streams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Streams.Name = "PAN_Streams";
            this.PAN_Streams.TabIndex = 1;
            // FlowLayouts
            this.PAN_Show = new System.Windows.Forms.FlowLayoutPanel();
            this.PAN_Show.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Show.Name = "PAN_Show";
            this.PAN_Record = new System.Windows.Forms.FlowLayoutPanel();
            this.PAN_Record.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Record.Name = "PAN_Record";
            this.PAN_Favoriten = new System.Windows.Forms.FlowLayoutPanel();
            this.PAN_Favoriten.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Favoriten.Name = "PAN_Favoriten";
            this.PAN_Streams.Controls.Add(this.PAN_Favoriten);
            this.PAN_Streams.Controls.Add(this.PAN_Record);
            this.PAN_Streams.Controls.Add(this.PAN_Show);
            this.RadSplitContainer1.Panel2.Controls.Add(this.PAN_Streams);

            // ContextMenuStrip para el grid
            this.CME_Model_Kanal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMI_Promo_Add = new System.Windows.Forms.ToolStripMenuItem("in die Modelliste aufnehmen");
            this.CMI_Aufnahme = new System.Windows.Forms.ToolStripMenuItem("Automatische Aufnahme");
            this.CMI_Stream_Refresh = new System.Windows.Forms.ToolStripMenuItem("Streamadressen aktualisieren");
            this.CMI_Anzeigen = new System.Windows.Forms.ToolStripMenuItem("anzeigen");
            this.RadMenuSeparatorItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Favorite = new System.Windows.Forms.ToolStripMenuItem("Favorit");
            this.CMI_Info = new System.Windows.Forms.ToolStripMenuItem("Info bearbeiten");
            this.CMI_Online_Check = new System.Windows.Forms.ToolStripMenuItem("Online-Prüfung");
            this.CMI_Deaktivieren = new System.Windows.Forms.ToolStripMenuItem("Deaktivieren");
            this.RadMenuSeparatorItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Gesehen = new System.Windows.Forms.ToolStripMenuItem("Gesehen");
            this.CMI_Delete = new System.Windows.Forms.ToolStripMenuItem("Löschen");
            this.RadMenuSeparatorItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Webseite = new System.Windows.Forms.ToolStripMenuItem("Webseite");
            this.CMI_Galerie = new System.Windows.Forms.ToolStripMenuItem("Galerie");
            this.CMI_Optionen = new System.Windows.Forms.ToolStripMenuItem("Optionen");
            this.CMI_Folder_Open = new System.Windows.Forms.ToolStripMenuItem("Ordner öffnen");
            this.RadMenuSeparatorItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Ansicht = new System.Windows.Forms.ToolStripMenuItem("Ansicht");
            this.CMI_Grouping = new System.Windows.Forms.ToolStripMenuItem("Gruppierung");
            this.CMI_LastOnline = new System.Windows.Forms.ToolStripMenuItem("Zuletzt online");
            this.CMI_Provider = new System.Windows.Forms.ToolStripMenuItem("Provider");
            this.CMI_Geschlecht = new System.Windows.Forms.ToolStripMenuItem("Geschlecht");
            this.CMI_Filter = new System.Windows.Forms.ToolStripMenuItem("Filter");
            this.CMI_Online = new System.Windows.Forms.ToolStripMenuItem("Online");
            this.CMI_Offline = new System.Windows.Forms.ToolStripMenuItem("Offline");
            this.CMI_Record = new System.Windows.Forms.ToolStripMenuItem("Record");
            this.CMI_New_Records = new System.Windows.Forms.ToolStripMenuItem("Neue Aufnahmen");
            this.CMI_AutoRecord = new System.Windows.Forms.ToolStripMenuItem("AutoRecord");
            this.CMI_Female = new System.Windows.Forms.ToolStripMenuItem("Female");
            this.CMI_Male = new System.Windows.Forms.ToolStripMenuItem("Male");
            this.CMI_Couple = new System.Windows.Forms.ToolStripMenuItem("Couple");
            this.CMI_Trans = new System.Windows.Forms.ToolStripMenuItem("Trans");
            this.CMI_Unknow = new System.Windows.Forms.ToolStripMenuItem("Unbekannt");
            this.RadMenuSeparatorItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Alle_Anzeigen = new System.Windows.Forms.ToolStripMenuItem("Alle anzeigen");
            this.CMI_Alle_Ausblenden = new System.Windows.Forms.ToolStripMenuItem("Alle ausblenden");

            this.CME_Model_Kanal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CMI_Promo_Add,
                this.CMI_Aufnahme,
                this.CMI_Stream_Refresh,
                this.CMI_Anzeigen,
                this.RadMenuSeparatorItem2,
                this.CMI_Favorite,
                this.CMI_Info,
                this.CMI_Online_Check,
                this.CMI_Deaktivieren,
                this.RadMenuSeparatorItem5,
                this.CMI_Gesehen,
                this.CMI_Delete,
                this.RadMenuSeparatorItem1,
                this.CMI_Webseite,
                this.CMI_Galerie,
                this.CMI_Optionen,
                this.CMI_Folder_Open,
                this.RadMenuSeparatorItem6,
                this.CMI_Ansicht,
                this.CMI_Grouping,
                this.CMI_LastOnline,
                this.CMI_Provider,
                this.CMI_Geschlecht,
                this.CMI_Filter,
                this.CMI_Online,
                this.CMI_Offline,
                this.CMI_Record,
                this.CMI_New_Records,
                this.CMI_AutoRecord,
                this.CMI_Female,
                this.CMI_Male,
                this.CMI_Couple,
                this.CMI_Trans,
                this.CMI_Unknow,
                this.RadMenuSeparatorItem3,
                this.CMI_Alle_Anzeigen,
                this.CMI_Alle_Ausblenden
            });
            this.GRV_Model_Kanal.ContextMenuStrip = this.CME_Model_Kanal;

            // ContextMenuStrip para Preview
            this.CME_Preview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMI_Favoriten = new System.Windows.Forms.ToolStripMenuItem("Favoriten");
            this.CMI_Records = new System.Windows.Forms.ToolStripMenuItem("Records");
            this.CME_Preview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CMI_Favoriten,
                this.CMI_Records
            });

            // ToolTip
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);

            // ProgressBar
            this.PGB_Disk = new System.Windows.Forms.ProgressBar();
            this.PGB_Disk.Name = "PGB_Disk";

            // Labels
            this.LAB_Drive = new System.Windows.Forms.Label();
            this.LAB_Drive.Name = "LAB_Drive";
            this.LAB_Warnung = new System.Windows.Forms.Label();
            this.LAB_Warnung.Name = "LAB_Warnung";
            this.RadLabel1 = new System.Windows.Forms.Label();
            this.RadLabel1.Name = "RadLabel1";

            // MenuStrip (barra superior)
            this.CBB_Model_Kanal = new System.Windows.Forms.MenuStrip();
            this.CBB_Hinzufügen = new System.Windows.Forms.ToolStripMenuItem("Hinzufügen");
            this.CBB_Löschen = new System.Windows.Forms.ToolStripMenuItem("Löschen");
            this.CBT_Suche = new System.Windows.Forms.ToolStripTextBox();
            this.CBT_ShowAll = new System.Windows.Forms.ToolStripMenuItem("Alle anzeigen");
            this.CBD_Liste_Sender = new System.Windows.Forms.ToolStripMenuItem("Senderliste");
            this.DDI_Alle_Anzeigen = new System.Windows.Forms.ToolStripMenuItem("Alle anzeigen");
            this.CBB_Aufnahmen_Heute = new System.Windows.Forms.ToolStripMenuItem("Aufnahmen heute");
            this.CBB_Favoriten = new System.Windows.Forms.ToolStripMenuItem("Favoriten");
            this.CBB_Einstellungen = new System.Windows.Forms.ToolStripMenuItem("Einstellungen");
            this.CBB_CamsRecorder = new System.Windows.Forms.ToolStripMenuItem("CamsRecorder");
            this.CBB_Model_Kanal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CBB_Hinzufügen,
                this.CBB_Löschen,
                this.CBT_Suche,
                this.CBT_ShowAll,
                this.CBD_Liste_Sender,
                this.DDI_Alle_Anzeigen,
                this.CBB_Aufnahmen_Heute,
                this.CBB_Favoriten,
                this.CBB_Einstellungen,
                this.CBB_CamsRecorder
            });

            // NotifyIcon
            this.Cam_Benachrichtigung = new System.Windows.Forms.NotifyIcon(this.components);

            // ContextMenuStrip para NotifyIcon
            this.CMS_Benachrichtigung = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TMI_Benachrichtung_Anzeigen = new System.Windows.Forms.ToolStripMenuItem("Anzeigen");
            this.CMS_Benachrichtigung.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.TMI_Benachrichtung_Anzeigen
            });

            // Form
            this.ClientSize = new System.Drawing.Size(1086, 708);
            this.Controls.Add(this.RadSplitContainer1);
            this.Controls.Add(this.CBB_Model_Kanal);
            this.MainMenuStrip = this.CBB_Model_Kanal;
            this.Name = "Form_Main";
            this.Text = "Form_Main";
        }
    }
}
