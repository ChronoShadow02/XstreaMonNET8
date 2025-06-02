namespace XstreaMonNET8
{
    partial class Form_Main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));

            // Main Controls
            this.GRV_Model_Kanal = new System.Windows.Forms.DataGridView();
            this.RadPanel1 = new System.Windows.Forms.Panel();
            this.RadSplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PAN_Navigation = new System.Windows.Forms.Panel();
            this.PAN_Streams = new System.Windows.Forms.Panel();
            this.PAN_Show = new System.Windows.Forms.FlowLayoutPanel();
            this.PAN_Record = new System.Windows.Forms.FlowLayoutPanel();
            this.PAN_Favoriten = new System.Windows.Forms.FlowLayoutPanel();

            // Context Menus
            this.CME_Model_Kanal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMI_Promo_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Aufnahme = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Stream_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Anzeigen = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Favorite = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Online_Check = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Deaktivieren = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Gesehen = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Webseite = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Galerie = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Optionen = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Folder_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Ansicht = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Grouping = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_LastOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Provider = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Geschlecht = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Filter = new System.Windows.Forms.ToolStripMenuItem();
            this.CMH_Status = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Online = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Offline = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Record = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_New_Records = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_AutoRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.CMH_Gender = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Female = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Male = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Couple = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Trans = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Unknow = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Alle_Anzeigen = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Alle_Ausblenden = new System.Windows.Forms.ToolStripMenuItem();

            // Toolbar Controls
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CBB_Commands = new System.Windows.Forms.ToolStrip();
            this.CBB_Hinzufügen = new System.Windows.Forms.ToolStripButton();
            this.CBB_Löschen = new System.Windows.Forms.ToolStripButton();
            this.CommandBarSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CBT_Suche = new System.Windows.Forms.ToolStripTextBox();
            this.CommandBarSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CBT_ShowAll = new System.Windows.Forms.ToolStripButton();
            this.CBD_Liste_Sender = new System.Windows.Forms.ToolStripDropDownButton();
            this.DDI_Alle_Anzeigen = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_Aufnahmen_Heute = new System.Windows.Forms.ToolStripButton();
            this.CBB_Favoriten = new System.Windows.Forms.ToolStripButton();
            this.CommandBarSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_Einstellungen = new System.Windows.Forms.ToolStripButton();
            this.CommandBarSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.CBB_CamsRecorder = new System.Windows.Forms.ToolStripButton();

            // Status Controls
            this.PGB_Disk = new System.Windows.Forms.ProgressBar();
            this.LAB_Drive = new System.Windows.Forms.Label();
            this.LAB_Warnung = new System.Windows.Forms.Label();
            this.CBB_Model_Kanal = new System.Windows.Forms.StatusStrip();
            this.RadLabel1 = new System.Windows.Forms.ToolStripStatusLabel();

            // Notification Controls
            this.Cam_Benachrichtigung = new System.Windows.Forms.NotifyIcon(this.components);
            this.CMS_Benachrichtigung = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TMI_Benachrichtung_Anzeigen = new System.Windows.Forms.ToolStripMenuItem();

            // Other Controls
            this.Control_Model_Info1 = new Control_Model_Info();
            this.Drive_Info_Refresh_Timer = new System.Windows.Forms.Timer(this.components);

            // Setup DataGridView
            ((System.ComponentModel.ISupportInitialize)(this.GRV_Model_Kanal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadSplitContainer1)).BeginInit();
            this.RadSplitContainer1.Panel1.SuspendLayout();
            this.RadSplitContainer1.Panel2.SuspendLayout();
            this.RadSplitContainer1.SuspendLayout();
            this.CME_Model_Kanal.SuspendLayout();
            this.CBB_Commands.SuspendLayout();
            this.CBB_Model_Kanal.SuspendLayout();
            this.CMS_Benachrichtigung.SuspendLayout();
            this.SuspendLayout();

            // GRV_Model_Kanal
            this.GRV_Model_Kanal.AllowUserToAddRows = false;
            this.GRV_Model_Kanal.AllowUserToDeleteRows = false;
            this.GRV_Model_Kanal.AllowUserToResizeRows = false;
            this.GRV_Model_Kanal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GRV_Model_Kanal.ContextMenuStrip = this.CME_Model_Kanal;
            this.GRV_Model_Kanal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GRV_Model_Kanal.Location = new System.Drawing.Point(0, 0);
            this.GRV_Model_Kanal.MultiSelect = false;
            this.GRV_Model_Kanal.Name = "GRV_Model_Kanal";
            this.GRV_Model_Kanal.ReadOnly = true;
            this.GRV_Model_Kanal.RowHeadersVisible = false;
            this.GRV_Model_Kanal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GRV_Model_Kanal.Size = new System.Drawing.Size(204, 678);
            this.GRV_Model_Kanal.TabIndex = 0;

            // RadPanel1
            this.RadPanel1.Controls.Add(this.GRV_Model_Kanal);
            this.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadPanel1.Location = new System.Drawing.Point(0, 0);
            this.RadPanel1.Name = "RadPanel1";
            this.RadPanel1.Size = new System.Drawing.Size(204, 678);
            this.RadPanel1.TabIndex = 0;

            // RadSplitContainer1
            this.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadSplitContainer1.Location = new System.Drawing.Point(0, 30);
            this.RadSplitContainer1.Name = "RadSplitContainer1";

            // Panel1 (Navigation)
            this.RadSplitContainer1.Panel1.Controls.Add(this.PAN_Navigation);
            this.RadSplitContainer1.Panel1MinSize = 100;

            // Panel2 (Streams)
            this.RadSplitContainer1.Panel2.Controls.Add(this.PAN_Streams);
            this.RadSplitContainer1.Size = new System.Drawing.Size(1086, 678);
            this.RadSplitContainer1.SplitterDistance = 204;
            this.RadSplitContainer1.TabIndex = 0;

            // PAN_Navigation
            this.PAN_Navigation.Controls.Add(this.RadPanel1);
            this.PAN_Navigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Navigation.Location = new System.Drawing.Point(0, 0);
            this.PAN_Navigation.Name = "PAN_Navigation";
            this.PAN_Navigation.Size = new System.Drawing.Size(204, 678);
            this.PAN_Navigation.TabIndex = 0;

            // PAN_Streams
            this.PAN_Streams.AutoScroll = true;
            this.PAN_Streams.Controls.Add(this.PAN_Show);
            this.PAN_Streams.Controls.Add(this.PAN_Record);
            this.PAN_Streams.Controls.Add(this.PAN_Favoriten);
            this.PAN_Streams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Streams.Location = new System.Drawing.Point(0, 0);
            this.PAN_Streams.Name = "PAN_Streams";
            this.PAN_Streams.Size = new System.Drawing.Size(878, 678);
            this.PAN_Streams.TabIndex = 0;

            // PAN_Show
            this.PAN_Show.AutoSize = true;
            this.PAN_Show.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Show.Location = new System.Drawing.Point(0, 0);
            this.PAN_Show.Name = "PAN_Show";
            this.PAN_Show.Size = new System.Drawing.Size(878, 0);
            this.PAN_Show.TabIndex = 0;

            // PAN_Record
            this.PAN_Record.AutoSize = true;
            this.PAN_Record.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Record.Location = new System.Drawing.Point(0, 0);
            this.PAN_Record.Name = "PAN_Record";
            this.PAN_Record.Size = new System.Drawing.Size(878, 0);
            this.PAN_Record.TabIndex = 1;

            // PAN_Favoriten
            this.PAN_Favoriten.AutoSize = true;
            this.PAN_Favoriten.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Favoriten.Location = new System.Drawing.Point(0, 0);
            this.PAN_Favoriten.Name = "PAN_Favoriten";
            this.PAN_Favoriten.Size = new System.Drawing.Size(878, 0);
            this.PAN_Favoriten.TabIndex = 2;

            // CME_Model_Kanal
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
                this.RadMenuSeparatorItem3,
                this.CMI_Alle_Anzeigen,
                this.CMI_Alle_Ausblenden});
            this.CME_Model_Kanal.Name = "CME_Model_Kanal";
            this.CME_Model_Kanal.Size = new System.Drawing.Size(221, 408);

            // Menu Items (partial setup)
            this.CMI_Promo_Add.Name = "CMI_Promo_Add";
            this.CMI_Promo_Add.Size = new System.Drawing.Size(220, 22);
            this.CMI_Promo_Add.Text = "in die Modelliste aufnehmen";

            this.CMI_Aufnahme.Name = "CMI_Aufnahme";
            this.CMI_Aufnahme.Size = new System.Drawing.Size(220, 22);
            this.CMI_Aufnahme.Text = "Automatische Aufnahme";

            // ... (setup other menu items similarly)

            // Toolbar (CBB_Commands)
            this.CBB_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CBB_Hinzufügen,
                this.CBB_Löschen,
                this.CommandBarSeparator1,
                this.CBT_Suche,
                this.CommandBarSeparator3,
                this.CBT_ShowAll,
                this.CBD_Liste_Sender,
                this.CBB_Aufnahmen_Heute,
                this.CBB_Favoriten,
                this.CommandBarSeparator2,
                this.CBB_Einstellungen,
                this.CommandBarSeparator4,
                this.CBB_CamsRecorder});
            this.CBB_Commands.Location = new System.Drawing.Point(0, 0);
            this.CBB_Commands.Name = "CBB_Commands";
            this.CBB_Commands.Size = new System.Drawing.Size(1086, 25);
            this.CBB_Commands.TabIndex = 0;

            // Toolbar buttons
            this.CBB_Hinzufügen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBB_Hinzufügen.Image = ((System.Drawing.Image)(resources.GetObject("CBB_Hinzufügen.Image")));
            this.CBB_Hinzufügen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBB_Hinzufügen.Name = "CBB_Hinzufügen";
            this.CBB_Hinzufügen.Size = new System.Drawing.Size(23, 22);
            this.CBB_Hinzufügen.Text = "Hinzufügen";
            this.CBB_Hinzufügen.ToolTipText = "Kanal hinzufügen";

            // ... (setup other toolbar items similarly)

            // Status bar
            this.CBB_Model_Kanal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.RadLabel1});
            this.CBB_Model_Kanal.Location = new System.Drawing.Point(0, 708);
            this.CBB_Model_Kanal.Name = "CBB_Model_Kanal";
            this.CBB_Model_Kanal.Size = new System.Drawing.Size(1086, 22);
            this.CBB_Model_Kanal.TabIndex = 1;

            // Notification icon
            this.Cam_Benachrichtigung.Icon = ((System.Drawing.Icon)(resources.GetObject("Cam_Benachrichtigung.Icon")));
            this.Cam_Benachrichtigung.Text = "XstreaMon";
            this.Cam_Benachrichtigung.Visible = true;
            this.Cam_Benachrichtigung.ContextMenuStrip = this.CMS_Benachrichtigung;

            // Main Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 730);
            this.Controls.Add(this.RadSplitContainer1);
            this.Controls.Add(this.CBB_Commands);
            this.Controls.Add(this.CBB_Model_Kanal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "XstreaMon";

            // Finalize setup
            this.RadSplitContainer1.Panel1.ResumeLayout(false);
            this.RadSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RadSplitContainer1)).EndInit();
            this.RadSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GRV_Model_Kanal)).EndInit();
            this.CME_Model_Kanal.ResumeLayout(false);
            this.CBB_Commands.ResumeLayout(false);
            this.CBB_Commands.PerformLayout();
            this.CBB_Model_Kanal.ResumeLayout(false);
            this.CBB_Model_Kanal.PerformLayout();
            this.CMS_Benachrichtigung.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Declare all controls
        private System.Windows.Forms.DataGridView GRV_Model_Kanal;
        private System.Windows.Forms.Panel RadPanel1;
        private System.Windows.Forms.SplitContainer RadSplitContainer1;
        private System.Windows.Forms.Panel PAN_Navigation;
        private System.Windows.Forms.Panel PAN_Streams;
        private System.Windows.Forms.FlowLayoutPanel PAN_Show;
        private System.Windows.Forms.FlowLayoutPanel PAN_Record;
        private System.Windows.Forms.FlowLayoutPanel PAN_Favoriten;
        private System.Windows.Forms.ContextMenuStrip CME_Model_Kanal;
        private System.Windows.Forms.ToolStripMenuItem CMI_Promo_Add;
        private System.Windows.Forms.ToolStripMenuItem CMI_Aufnahme;
        private System.Windows.Forms.ToolStripMenuItem CMI_Stream_Refresh;
        private System.Windows.Forms.ToolStripMenuItem CMI_Anzeigen;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem2;
        private System.Windows.Forms.ToolStripMenuItem CMI_Favorite;
        private System.Windows.Forms.ToolStripMenuItem CMI_Info;
        private System.Windows.Forms.ToolStripMenuItem CMI_Online_Check;
        private System.Windows.Forms.ToolStripMenuItem CMI_Deaktivieren;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem5;
        private System.Windows.Forms.ToolStripMenuItem CMI_Gesehen;
        private System.Windows.Forms.ToolStripMenuItem CMI_Delete;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem1;
        private System.Windows.Forms.ToolStripMenuItem CMI_Webseite;
        private System.Windows.Forms.ToolStripMenuItem CMI_Galerie;
        private System.Windows.Forms.ToolStripMenuItem CMI_Optionen;
        private System.Windows.Forms.ToolStripMenuItem CMI_Folder_Open;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem6;
        private System.Windows.Forms.ToolStripMenuItem CMI_Ansicht;
        private System.Windows.Forms.ToolStripMenuItem CMI_Grouping;
        private System.Windows.Forms.ToolStripMenuItem CMI_LastOnline;
        private System.Windows.Forms.ToolStripMenuItem CMI_Provider;
        private System.Windows.Forms.ToolStripMenuItem CMI_Geschlecht;
        private System.Windows.Forms.ToolStripMenuItem CMI_Filter;
        private System.Windows.Forms.ToolStripMenuItem CMH_Status;
        private System.Windows.Forms.ToolStripMenuItem CMI_Online;
        private System.Windows.Forms.ToolStripMenuItem CMI_Offline;
        private System.Windows.Forms.ToolStripMenuItem CMI_Record;
        private System.Windows.Forms.ToolStripMenuItem CMI_New_Records;
        private System.Windows.Forms.ToolStripMenuItem CMI_AutoRecord;
        private System.Windows.Forms.ToolStripMenuItem CMH_Gender;
        private System.Windows.Forms.ToolStripMenuItem CMI_Female;
        private System.Windows.Forms.ToolStripMenuItem CMI_Male;
        private System.Windows.Forms.ToolStripMenuItem CMI_Couple;
        private System.Windows.Forms.ToolStripMenuItem CMI_Trans;
        private System.Windows.Forms.ToolStripMenuItem CMI_Unknow;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem3;
        private System.Windows.Forms.ToolStripMenuItem CMI_Alle_Anzeigen;
        private System.Windows.Forms.ToolStripMenuItem CMI_Alle_Ausblenden;
        private System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.ToolStrip CBB_Commands;
        private System.Windows.Forms.ToolStripButton CBB_Hinzufügen;
        private System.Windows.Forms.ToolStripButton CBB_Löschen;
        private System.Windows.Forms.ToolStripSeparator CommandBarSeparator1;
        private System.Windows.Forms.ToolStripTextBox CBT_Suche;
        private System.Windows.Forms.ToolStripSeparator CommandBarSeparator3;
        private System.Windows.Forms.ToolStripButton CBT_ShowAll;
        private System.Windows.Forms.ToolStripDropDownButton CBD_Liste_Sender;
        private System.Windows.Forms.ToolStripMenuItem DDI_Alle_Anzeigen;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem4;
        private System.Windows.Forms.ToolStripButton CBB_Aufnahmen_Heute;
        private System.Windows.Forms.ToolStripButton CBB_Favoriten;
        private System.Windows.Forms.ToolStripSeparator CommandBarSeparator2;
        private System.Windows.Forms.ToolStripButton CBB_Einstellungen;
        private System.Windows.Forms.ToolStripSeparator CommandBarSeparator4;
        private System.Windows.Forms.ToolStripButton CBB_CamsRecorder;
        private System.Windows.Forms.ProgressBar PGB_Disk;
        private System.Windows.Forms.Label LAB_Drive;
        private System.Windows.Forms.Label LAB_Warnung;
        private System.Windows.Forms.StatusStrip CBB_Model_Kanal;
        private System.Windows.Forms.ToolStripStatusLabel RadLabel1;
        private System.Windows.Forms.NotifyIcon Cam_Benachrichtigung;
        private System.Windows.Forms.ContextMenuStrip CMS_Benachrichtigung;
        private System.Windows.Forms.ToolStripMenuItem TMI_Benachrichtung_Anzeigen;
        private Control_Model_Info Control_Model_Info1;
        private System.Windows.Forms.Timer Drive_Info_Refresh_Timer;

        // Class variables
        private bool Pri_Show_All;
        private bool Pri_Show_Visible;
        private bool Pri_Data_Load;
        private System.Drawing.Font Pri_Font_Favorite_Deaktiv;
        private System.Drawing.Font Pri_Font_Favorite_Aktiv;
        private System.Drawing.Font Pri_Font_Aktiv;
        private System.Drawing.Font Pri_Font_Deaktiv;
        private System.Guid Show_Model_Info_GUID;
        public static Class_Driveinfo Drive_Info;
    }
}