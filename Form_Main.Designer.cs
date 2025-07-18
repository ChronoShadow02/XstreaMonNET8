namespace XstreaMonNET8
{
    partial class Form_Main
    {
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GRV_Model_Kanal = new System.Windows.Forms.DataGridView();
            this.RadPanel1 = new System.Windows.Forms.Panel();
            this.RadSplitContainer1 = new System.Windows.Forms.SplitContainer();
            //this.PAN_Navigation = new System.Windows.Forms.Panel();
            //this.PAN_Streams = new System.Windows.Forms.Panel();
            this.PAN_Show = new XstreaMonNET8.CustomFlowLayoutPanel();
            this.PAN_Record = new XstreaMonNET8.CustomFlowLayoutPanel();
            this.PAN_Favoriten = new XstreaMonNET8.CustomFlowLayoutPanel();
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
            this.CMH_Status = new System.Windows.Forms.ToolStripLabel();
            this.CMI_Online = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Offline = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Record = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_New_Records = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_AutoRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.CMH_Gender = new System.Windows.Forms.ToolStripLabel();
            this.CMI_Female = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Male = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Couple = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Trans = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Unknow = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Alle_Anzeigen = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Alle_Ausblenden = new System.Windows.Forms.ToolStripMenuItem();
            this.CME_Preview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMI_Favoriten = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Records = new System.Windows.Forms.ToolStripMenuItem();
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
            this.CommandBarSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.CommandBarSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.PGB_Disk = new System.Windows.Forms.ProgressBar();
            this.LAB_Drive = new System.Windows.Forms.Label();
            this.LAB_Warnung = new System.Windows.Forms.Label();
            this.CBB_Model_Kanal = new System.Windows.Forms.ToolStrip();
            this.RadLabel1 = new System.Windows.Forms.Label();
            this.Cam_Benachrichtigung = new System.Windows.Forms.NotifyIcon(this.components);
            this.CMS_Benachrichtigung = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TMI_Benachrichtung_Anzeigen = new System.Windows.Forms.ToolStripMenuItem();
            this.DTA_Benachrichtigung = new System.Windows.Forms.Form(); // Placeholder for custom notification form
            this.Control_Model_Info1 = new XstreaMonNET8.Control_Model_Info();
            ((System.ComponentModel.ISupportInitialize)(this.GRV_Model_Kanal)).BeginInit();
            this.RadPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RadSplitContainer1)).BeginInit();
            this.RadSplitContainer1.Panel1.SuspendLayout();
            this.RadSplitContainer1.Panel2.SuspendLayout();
            this.RadSplitContainer1.SuspendLayout();
            this.PAN_Navigation.SuspendLayout();
            this.PAN_Streams.SuspendLayout();
            this.CME_Model_Kanal.SuspendLayout();
            this.CME_Preview.SuspendLayout();
            this.CBB_Commands.SuspendLayout();
            this.CBB_Model_Kanal.SuspendLayout();
            this.CMS_Benachrichtigung.SuspendLayout();
            this.SuspendLayout();
            //
            // GRV_Model_Kanal
            //
            this.GRV_Model_Kanal.AllowUserToAddRows = false;
            this.GRV_Model_Kanal.AllowUserToDeleteRows = false;
            this.GRV_Model_Kanal.AllowUserToResizeRows = false;
            this.GRV_Model_Kanal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GRV_Model_Kanal.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.GRV_Model_Kanal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GRV_Model_Kanal.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GRV_Model_Kanal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.GRV_Model_Kanal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GRV_Model_Kanal.ColumnHeadersVisible = false;
            this.GRV_Model_Kanal.ContextMenuStrip = this.CME_Model_Kanal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GRV_Model_Kanal.DefaultCellStyle = dataGridViewCellStyle1;
            this.GRV_Model_Kanal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GRV_Model_Kanal.EnableHeadersVisualStyles = false;
            this.GRV_Model_Kanal.Location = new System.Drawing.Point(0, 0);
            this.GRV_Model_Kanal.MultiSelect = false;
            this.GRV_Model_Kanal.Name = "GRV_Model_Kanal";
            this.GRV_Model_Kanal.ReadOnly = true;
            this.GRV_Model_Kanal.RowHeadersVisible = false;
            this.GRV_Model_Kanal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GRV_Model_Kanal.ShowCellErrors = false;
            this.GRV_Model_Kanal.ShowCellToolTips = false;
            this.GRV_Model_Kanal.ShowEditingIcon = false;
            this.GRV_Model_Kanal.ShowRowErrors = false;
            this.GRV_Model_Kanal.Size = new System.Drawing.Size(204, 678);
            this.GRV_Model_Kanal.TabIndex = 3;
            //
            // RadPanel1
            //
            this.RadPanel1.Controls.Add(this.GRV_Model_Kanal);
            this.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadPanel1.Location = new System.Drawing.Point(0, 0);
            this.RadPanel1.Name = "RadPanel1";
            this.RadPanel1.Size = new System.Drawing.Size(204, 678);
            this.RadPanel1.TabIndex = 1;
            //
            // RadSplitContainer1
            //
            this.RadSplitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadSplitContainer1.Location = new System.Drawing.Point(0, 30);
            this.RadSplitContainer1.Name = "RadSplitContainer1";
            //
            // RadSplitContainer1.Panel1
            //
            this.RadSplitContainer1.Panel1.Controls.Add(this.PAN_Navigation);
            //
            // RadSplitContainer1.Panel2
            //
            this.RadSplitContainer1.Panel2.Controls.Add(this.PAN_Streams);
            this.RadSplitContainer1.Size = new System.Drawing.Size(1086, 678);
            this.RadSplitContainer1.SplitterDistance = 204;
            this.RadSplitContainer1.TabIndex = 0;
            //
            // PAN_Navigation
            //
            this.PAN_Navigation.Controls.Add(this.RadPanel1);
            this.PAN_Navigation.Cursor = System.Windows.Forms.Cursors.Default;
            this.PAN_Navigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Navigation.Location = new System.Drawing.Point(0, 0);
            this.PAN_Navigation.Name = "PAN_Navigation";
            this.PAN_Navigation.Size = new System.Drawing.Size(204, 678);
            this.PAN_Navigation.TabIndex = 0;
            //
            // PAN_Streams
            //
            this.PAN_Streams.AutoScroll = true;
            this.PAN_Streams.BackColor = System.Drawing.SystemColors.Control;
            this.PAN_Streams.ContextMenuStrip = this.CME_Preview;
            this.PAN_Streams.Controls.Add(this.PAN_Show);
            this.PAN_Streams.Controls.Add(this.PAN_Record);
            this.PAN_Streams.Controls.Add(this.PAN_Favoriten);
            this.PAN_Streams.Cursor = System.Windows.Forms.Cursors.Default;
            this.PAN_Streams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Streams.Location = new System.Drawing.Point(0, 0);
            this.PAN_Streams.Margin = new System.Windows.Forms.Padding(3);
            this.PAN_Streams.Name = "PAN_Streams";
            this.PAN_Streams.Padding = new System.Windows.Forms.Padding(3);
            this.PAN_Streams.Size = new System.Drawing.Size(878, 678);
            this.PAN_Streams.TabIndex = 1;
            //
            // PAN_Show
            //
            this.PAN_Show.AutoSize = true;
            this.PAN_Show.BackColor = System.Drawing.Color.Transparent;
            this.PAN_Show.ContextMenuStrip = this.CME_Preview;
            this.PAN_Show.Cursor = System.Windows.Forms.Cursors.Default;
            this.PAN_Show.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Show.Location = new System.Drawing.Point(3, 3);
            this.PAN_Show.Margin = new System.Windows.Forms.Padding(0);
            this.PAN_Show.Name = "PAN_Show";
            this.PAN_Show.Size = new System.Drawing.Size(872, 0);
            this.PAN_Show.TabIndex = 2;
            //
            // PAN_Record
            //
            this.PAN_Record.AutoSize = true;
            this.PAN_Record.BackColor = System.Drawing.Color.Transparent;
            this.PAN_Record.ContextMenuStrip = this.CME_Preview;
            this.PAN_Record.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Record.Location = new System.Drawing.Point(3, 3);
            this.PAN_Record.Margin = new System.Windows.Forms.Padding(0);
            this.PAN_Record.Name = "PAN_Record";
            this.PAN_Record.Size = new System.Drawing.Size(872, 0);
            this.PAN_Record.TabIndex = 1;
            //
            // PAN_Favoriten
            //
            this.PAN_Favoriten.AutoSize = true;
            this.PAN_Favoriten.BackColor = System.Drawing.Color.Transparent;
            this.PAN_Favoriten.ContextMenuStrip = this.CME_Preview;
            this.PAN_Favoriten.Cursor = System.Windows.Forms.Cursors.Default;
            this.PAN_Favoriten.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Favoriten.Location = new System.Drawing.Point(3, 3);
            this.PAN_Favoriten.Margin = new System.Windows.Forms.Padding(0);
            this.PAN_Favoriten.Name = "PAN_Favoriten";
            this.PAN_Favoriten.Size = new System.Drawing.Size(872, 0);
            this.PAN_Favoriten.TabIndex = 3;
            //
            // CME_Model_Kanal
            //
            this.CME_Model_Kanal.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.CME_Model_Kanal.Size = new System.Drawing.Size(280, 436);
            //
            // CMI_Promo_Add
            //
            this.CMI_Promo_Add.Name = "CMI_Promo_Add";
            this.CMI_Promo_Add.Size = new System.Drawing.Size(279, 24);
            this.CMI_Promo_Add.Text = "in die Modelliste aufnehmen";
            //
            // CMI_Aufnahme
            //
            this.CMI_Aufnahme.Name = "CMI_Aufnahme";
            this.CMI_Aufnahme.Size = new System.Drawing.Size(279, 24);
            this.CMI_Aufnahme.Text = "Automatische Aufnahme";
            //
            // CMI_Stream_Refresh
            //
            this.CMI_Stream_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Stream_Refresh.Image")));
            this.CMI_Stream_Refresh.Name = "CMI_Stream_Refresh";
            this.CMI_Stream_Refresh.Size = new System.Drawing.Size(279, 24);
            this.CMI_Stream_Refresh.Text = "Streamadressen aktualisieren";
            //
            // CMI_Anzeigen
            //
            this.CMI_Anzeigen.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Anzeigen.Image")));
            this.CMI_Anzeigen.Name = "CMI_Anzeigen";
            this.CMI_Anzeigen.Size = new System.Drawing.Size(279, 24);
            this.CMI_Anzeigen.Text = "anzeigen";
            //
            // RadMenuSeparatorItem2
            //
            this.RadMenuSeparatorItem2.Name = "RadMenuSeparatorItem2";
            this.RadMenuSeparatorItem2.Size = new System.Drawing.Size(276, 6);
            //
            // CMI_Favorite
            //
            this.CMI_Favorite.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Favorite.Image")));
            this.CMI_Favorite.Name = "CMI_Favorite";
            this.CMI_Favorite.Size = new System.Drawing.Size(279, 24);
            this.CMI_Favorite.Text = "";
            //
            // CMI_Info
            //
            this.CMI_Info.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Info.Image")));
            this.CMI_Info.Name = "CMI_Info";
            this.CMI_Info.Size = new System.Drawing.Size(279, 24);
            this.CMI_Info.Text = "Info bearbeiten";
            //
            // CMI_Online_Check
            //
            this.CMI_Online_Check.Name = "CMI_Online_Check";
            this.CMI_Online_Check.Size = new System.Drawing.Size(279, 24);
            this.CMI_Online_Check.Text = "Online-Prüfung";
            //
            // CMI_Deaktivieren
            //
            this.CMI_Deaktivieren.CheckOnClick = true;
            this.CMI_Deaktivieren.Name = "CMI_Deaktivieren";
            this.CMI_Deaktivieren.Size = new System.Drawing.Size(279, 24);
            this.CMI_Deaktivieren.Text = "deaktivieren";
            //
            // RadMenuSeparatorItem5
            //
            this.RadMenuSeparatorItem5.Name = "RadMenuSeparatorItem5";
            this.RadMenuSeparatorItem5.Size = new System.Drawing.Size(276, 6);
            //
            // CMI_Gesehen
            //
            this.CMI_Gesehen.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Gesehen.Image")));
            this.CMI_Gesehen.Name = "CMI_Gesehen";
            this.CMI_Gesehen.Size = new System.Drawing.Size(279, 24);
            this.CMI_Gesehen.Text = "Als gesehen markieren";
            //
            // CMI_Delete
            //
            this.CMI_Delete.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Delete.Image")));
            this.CMI_Delete.Name = "CMI_Delete";
            this.CMI_Delete.Size = new System.Drawing.Size(279, 24);
            this.CMI_Delete.Text = "Löschen";
            //
            // RadMenuSeparatorItem1
            //
            this.RadMenuSeparatorItem1.Name = "RadMenuSeparatorItem1";
            this.RadMenuSeparatorItem1.Size = new System.Drawing.Size(276, 6);
            //
            // CMI_Webseite
            //
            this.CMI_Webseite.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Webseite.Image")));
            this.CMI_Webseite.Name = "CMI_Webseite";
            this.CMI_Webseite.Size = new System.Drawing.Size(279, 24);
            this.CMI_Webseite.Text = "Webseite öffnen";
            //
            // CMI_Galerie
            //
            this.CMI_Galerie.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Galerie.Image")));
            this.CMI_Galerie.Name = "CMI_Galerie";
            this.CMI_Galerie.Size = new System.Drawing.Size(279, 24);
            this.CMI_Galerie.Text = "Galerie";
            //
            // CMI_Optionen
            //
            this.CMI_Optionen.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Optionen.Image")));
            this.CMI_Optionen.Name = "CMI_Optionen";
            this.CMI_Optionen.Size = new System.Drawing.Size(279, 24);
            this.CMI_Optionen.Text = "Einstellungen";
            //
            // CMI_Folder_Open
            //
            this.CMI_Folder_Open.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Folder_Open.Image")));
            this.CMI_Folder_Open.Name = "CMI_Folder_Open";
            this.CMI_Folder_Open.Size = new System.Drawing.Size(279, 24);
            this.CMI_Folder_Open.Text = "Aufnahmeordner öffnen";
            //
            // RadMenuSeparatorItem6
            //
            this.RadMenuSeparatorItem6.Name = "RadMenuSeparatorItem6";
            this.RadMenuSeparatorItem6.Size = new System.Drawing.Size(276, 6);
            //
            // CMI_Ansicht
            //
            this.CMI_Ansicht.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMI_Grouping,
            this.CMI_Filter});
            this.CMI_Ansicht.Name = "CMI_Ansicht";
            this.CMI_Ansicht.Size = new System.Drawing.Size(279, 24);
            this.CMI_Ansicht.Text = "Ansicht";
            //
            // CMI_Grouping
            //
            this.CMI_Grouping.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMI_LastOnline,
            this.CMI_Provider,
            this.CMI_Geschlecht});
            this.CMI_Grouping.Name = "CMI_Grouping";
            this.CMI_Grouping.Size = new System.Drawing.Size(169, 24);
            this.CMI_Grouping.Text = "Gruppierung";
            //
            // CMI_LastOnline
            //
            this.CMI_LastOnline.CheckOnClick = true;
            this.CMI_LastOnline.Name = "CMI_LastOnline";
            this.CMI_LastOnline.Size = new System.Drawing.Size(209, 24);
            this.CMI_LastOnline.Tag = "0";
            this.CMI_LastOnline.Text = "Letztes mal online";
            //
            // CMI_Provider
            //
            this.CMI_Provider.CheckOnClick = true;
            this.CMI_Provider.Name = "CMI_Provider";
            this.CMI_Provider.Size = new System.Drawing.Size(209, 24);
            this.CMI_Provider.Tag = "1";
            this.CMI_Provider.Text = "Webseite";
            //
            // CMI_Geschlecht
            //
            this.CMI_Geschlecht.CheckOnClick = true;
            this.CMI_Geschlecht.Name = "CMI_Geschlecht";
            this.CMI_Geschlecht.Size = new System.Drawing.Size(209, 24);
            this.CMI_Geschlecht.Tag = "2";
            this.CMI_Geschlecht.Text = "Geschlecht";
            //
            // CMI_Filter
            //
            this.CMI_Filter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMH_Status,
            this.CMI_Online,
            this.CMI_Offline,
            this.CMI_Record,
            this.CMI_New_Records,
            this.CMI_AutoRecord,
            this.CMH_Gender,
            this.CMI_Female,
            this.CMI_Male,
            this.CMI_Couple,
            this.CMI_Trans,
            this.CMI_Unknow});
            this.CMI_Filter.Name = "CMI_Filter";
            this.CMI_Filter.Size = new System.Drawing.Size(169, 24);
            this.CMI_Filter.Text = "Filter";
            //
            // CMH_Status
            //
            this.CMH_Status.Name = "CMH_Status";
            this.CMH_Status.Size = new System.Drawing.Size(209, 20);
            this.CMH_Status.Text = "Onlinestatus";
            //
            // CMI_Online
            //
            this.CMI_Online.CheckOnClick = true;
            this.CMI_Online.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Online.Image")));
            this.CMI_Online.Name = "CMI_Online";
            this.CMI_Online.Size = new System.Drawing.Size(209, 24);
            this.CMI_Online.Text = "Online";
            //
            // CMI_Offline
            //
            this.CMI_Offline.CheckOnClick = true;
            this.CMI_Offline.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Offline.Image")));
            this.CMI_Offline.Name = "CMI_Offline";
            this.CMI_Offline.Size = new System.Drawing.Size(209, 24);
            this.CMI_Offline.Text = "Offline";
            //
            // CMI_Record
            //
            this.CMI_Record.CheckOnClick = true;
            this.CMI_Record.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Record.Image")));
            this.CMI_Record.Name = "CMI_Record";
            this.CMI_Record.Size = new System.Drawing.Size(209, 24);
            this.CMI_Record.Text = "Aufnahmen";
            //
            // CMI_New_Records
            //
            this.CMI_New_Records.Image = ((System.Drawing.Image)(resources.GetObject("CMI_New_Records.Image")));
            this.CMI_New_Records.Name = "CMI_New_Records";
            this.CMI_New_Records.Size = new System.Drawing.Size(209, 24);
            this.CMI_New_Records.Text = "Neue Aufnahmen";
            //
            // CMI_AutoRecord
            //
            this.CMI_AutoRecord.CheckOnClick = true;
            this.CMI_AutoRecord.Image = ((System.Drawing.Image)(resources.GetObject("CMI_AutoRecord.Image")));
            this.CMI_AutoRecord.Name = "CMI_AutoRecord";
            this.CMI_AutoRecord.Size = new System.Drawing.Size(209, 24);
            this.CMI_AutoRecord.Text = "Autorecord";
            //
            // CMH_Gender
            //
            this.CMH_Gender.Name = "CMH_Gender";
            this.CMH_Gender.Size = new System.Drawing.Size(209, 20);
            this.CMH_Gender.Text = "Geschlecht";
            //
            // CMI_Female
            //
            this.CMI_Female.CheckOnClick = true;
            this.CMI_Female.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Female.Image")));
            this.CMI_Female.Name = "CMI_Female";
            this.CMI_Female.Size = new System.Drawing.Size(209, 24);
            this.CMI_Female.Text = "Weiblich";
            //
            // CMI_Male
            //
            this.CMI_Male.CheckOnClick = true;
            this.CMI_Male.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Male.Image")));
            this.CMI_Male.Name = "CMI_Male";
            this.CMI_Male.Size = new System.Drawing.Size(209, 24);
            this.CMI_Male.Text = "Männlich";
            //
            // CMI_Couple
            //
            this.CMI_Couple.CheckOnClick = true;
            this.CMI_Couple.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Couple.Image")));
            this.CMI_Couple.Name = "CMI_Couple";
            this.CMI_Couple.Size = new System.Drawing.Size(209, 24);
            this.CMI_Couple.Text = "Paar";
            //
            // CMI_Trans
            //
            this.CMI_Trans.CheckOnClick = true;
            this.CMI_Trans.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Trans.Image")));
            this.CMI_Trans.Name = "CMI_Trans";
            this.CMI_Trans.Size = new System.Drawing.Size(209, 24);
            this.CMI_Trans.Text = "Trans";
            //
            // CMI_Unknow
            //
            this.CMI_Unknow.CheckOnClick = true;
            this.CMI_Unknow.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Unknow.Image")));
            this.CMI_Unknow.Name = "CMI_Unknow";
            this.CMI_Unknow.Size = new System.Drawing.Size(209, 24);
            this.CMI_Unknow.Text = "Unbekannt";
            //
            // RadMenuSeparatorItem3
            //
            this.RadMenuSeparatorItem3.Name = "RadMenuSeparatorItem3";
            this.RadMenuSeparatorItem3.Size = new System.Drawing.Size(276, 6);
            //
            // CMI_Alle_Anzeigen
            //
            this.CMI_Alle_Anzeigen.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Alle_Anzeigen.Image")));
            this.CMI_Alle_Anzeigen.Name = "CMI_Alle_Anzeigen";
            this.CMI_Alle_Anzeigen.Size = new System.Drawing.Size(279, 24);
            this.CMI_Alle_Anzeigen.Text = "Alle Anzeigen";
            //
            // CMI_Alle_Ausblenden
            //
            this.CMI_Alle_Ausblenden.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Alle_Ausblenden.Image")));
            this.CMI_Alle_Ausblenden.Name = "CMI_Alle_Ausblenden";
            this.CMI_Alle_Ausblenden.Size = new System.Drawing.Size(279, 24);
            this.CMI_Alle_Ausblenden.Text = "Alle Ausblenden";
            //
            // CME_Preview
            //
            this.CME_Preview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CME_Preview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMI_Favoriten,
            this.CMI_Records});
            this.CME_Preview.Name = "CME_Preview";
            this.CME_Preview.Size = new System.Drawing.Size(229, 52);
            //
            // CMI_Favoriten
            //
            this.CMI_Favoriten.CheckOnClick = true;
            this.CMI_Favoriten.Name = "CMI_Favoriten";
            this.CMI_Favoriten.Size = new System.Drawing.Size(228, 24);
            this.CMI_Favoriten.Text = "Favoriten gruppieren";
            //
            // CMI_Records
            //
            this.CMI_Records.CheckOnClick = true;
            this.CMI_Records.Name = "CMI_Records";
            this.CMI_Records.Size = new System.Drawing.Size(228, 24);
            this.CMI_Records.Text = "Aufnahmen gruppieren";
            //
            // CBB_Commands
            //
            this.CBB_Commands.Dock = System.Windows.Forms.DockStyle.None;
            this.CBB_Commands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.CBB_Commands.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.CBB_Commands.Size = new System.Drawing.Size(478, 27);
            this.CBB_Commands.TabIndex = 0;
            this.CBB_Commands.Text = "toolStrip1";
            //
            // CBB_Hinzufügen
            //
            this.CBB_Hinzufügen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBB_Hinzufügen.Image = ((System.Drawing.Image)(resources.GetObject("CBB_Hinzufügen.Image")));
            this.CBB_Hinzufügen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBB_Hinzufügen.Name = "CBB_Hinzufügen";
            this.CBB_Hinzufügen.Size = new System.Drawing.Size(24, 24);
            this.CBB_Hinzufügen.Text = "Hinzufügen";
            //
            // CBB_Löschen
            //
            this.CBB_Löschen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBB_Löschen.Image = ((System.Drawing.Image)(resources.GetObject("CBB_Löschen.Image")));
            this.CBB_Löschen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBB_Löschen.Name = "CBB_Löschen";
            this.CBB_Löschen.Size = new System.Drawing.Size(24, 24);
            this.CBB_Löschen.Text = "Löschen";
            //
            // CommandBarSeparator1
            //
            this.CommandBarSeparator1.Name = "CommandBarSeparator1";
            this.CommandBarSeparator1.Size = new System.Drawing.Size(6, 27);
            //
            // CBT_Suche
            //
            this.CBT_Suche.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBT_Suche.Name = "CBT_Suche";
            this.CBT_Suche.Size = new System.Drawing.Size(100, 27);
            this.CBT_Suche.Text = "Suche...";
            //
            // CommandBarSeparator3
            //
            this.CommandBarSeparator3.Name = "CommandBarSeparator3";
            this.CommandBarSeparator3.Size = new System.Drawing.Size(6, 27);
            //
            // CBT_ShowAll
            //
            this.CBT_ShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBT_ShowAll.Image = ((System.Drawing.Image)(resources.GetObject("CBT_ShowAll.Image")));
            this.CBT_ShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBT_ShowAll.Name = "CBT_ShowAll";
            this.CBT_ShowAll.Size = new System.Drawing.Size(24, 24);
            this.CBT_ShowAll.Text = "Show all";
            //
            // CBD_Liste_Sender
            //
            this.CBD_Liste_Sender.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBD_Liste_Sender.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DDI_Alle_Anzeigen,
            this.RadMenuSeparatorItem4});
            this.CBD_Liste_Sender.Image = ((System.Drawing.Image)(resources.GetObject("CBD_Liste_Sender.Image")));
            this.CBD_Liste_Sender.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBD_Liste_Sender.Name = "CBD_Liste_Sender";
            this.CBD_Liste_Sender.Size = new System.Drawing.Size(34, 24);
            this.CBD_Liste_Sender.Text = "CommandBarDropDownButton1";
            //
            // DDI_Alle_Anzeigen
            //
            this.DDI_Alle_Anzeigen.Name = "DDI_Alle_Anzeigen";
            this.DDI_Alle_Anzeigen.Size = new System.Drawing.Size(176, 24);
            this.DDI_Alle_Anzeigen.Text = "Alle anzeigen";
            //
            // RadMenuSeparatorItem4
            //
            this.RadMenuSeparatorItem4.Name = "RadMenuSeparatorItem4";
            this.RadMenuSeparatorItem4.Size = new System.Drawing.Size(173, 6);
            //
            // CBB_Aufnahmen_Heute
            //
            this.CBB_Aufnahmen_Heute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBB_Aufnahmen_Heute.Image = ((System.Drawing.Image)(resources.GetObject("CBB_Aufnahmen_Heute.Image")));
            this.CBB_Aufnahmen_Heute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBB_Aufnahmen_Heute.Name = "CBB_Aufnahmen_Heute";
            this.CBB_Aufnahmen_Heute.Size = new System.Drawing.Size(24, 24);
            this.CBB_Aufnahmen_Heute.Text = "Galerie";
            //
            // CBB_Favoriten
            //
            this.CBB_Favoriten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBB_Favoriten.Image = ((System.Drawing.Image)(resources.GetObject("CBB_Favoriten.Image")));
            this.CBB_Favoriten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBB_Favoriten.Name = "CBB_Favoriten";
            this.CBB_Favoriten.Size = new System.Drawing.Size(24, 24);
            this.CBB_Favoriten.Text = "Favoriten";
            //
            // CommandBarSeparator2
            //
            this.CommandBarSeparator2.Name = "CommandBarSeparator2";
            this.CommandBarSeparator2.Size = new System.Drawing.Size(6, 27);
            //
            // CBB_Einstellungen
            //
            this.CBB_Einstellungen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBB_Einstellungen.Image = ((System.Drawing.Image)(resources.GetObject("CBB_Einstellungen.Image")));
            this.CBB_Einstellungen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBB_Einstellungen.Name = "CBB_Einstellungen";
            this.CBB_Einstellungen.Size = new System.Drawing.Size(24, 24);
            this.CBB_Einstellungen.Text = "CommandBarButton1";
            //
            // CommandBarSeparator4
            //
            this.CommandBarSeparator4.Name = "CommandBarSeparator4";
            this.CommandBarSeparator4.Size = new System.Drawing.Size(6, 27);
            //
            // CBB_CamsRecorder
            //
            this.CBB_CamsRecorder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CBB_CamsRecorder.Image = ((System.Drawing.Image)(resources.GetObject("CBB_CamsRecorder.Image")));
            this.CBB_CamsRecorder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CBB_CamsRecorder.Name = "CBB_CamsRecorder";
            this.CBB_CamsRecorder.Size = new System.Drawing.Size(24, 24);
            this.CBB_CamsRecorder.Text = "";
            this.CBB_CamsRecorder.ToolTipText = "xstreamon.com";
            //
            // CommandBarSeparator5
            //
            this.CommandBarSeparator5.Name = "CommandBarSeparator5";
            this.CommandBarSeparator5.Size = new System.Drawing.Size(6, 27);
            //
            // CommandBarSeparator6
            //
            this.CommandBarSeparator6.Name = "CommandBarSeparator6";
            this.CommandBarSeparator6.Size = new System.Drawing.Size(6, 27);
            //
            // PGB_Disk
            //
            this.PGB_Disk.Dock = System.Windows.Forms.DockStyle.Right;
            this.PGB_Disk.Location = new System.Drawing.Point(991, 0);
            this.PGB_Disk.Name = "PGB_Disk";
            this.PGB_Disk.Size = new System.Drawing.Size(95, 30);
            this.PGB_Disk.TabIndex = 3;
            //
            // LAB_Drive
            //
            this.LAB_Drive.AutoSize = true;
            this.LAB_Drive.Dock = System.Windows.Forms.DockStyle.Right;
            this.LAB_Drive.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.LAB_Drive.Location = new System.Drawing.Point(887, 0);
            this.LAB_Drive.Name = "LAB_Drive";
            this.LAB_Drive.Size = new System.Drawing.Size(104, 30);
            this.LAB_Drive.TabIndex = 4;
            this.LAB_Drive.Text = "LAB_Drive";
            this.LAB_Drive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // LAB_Warnung
            //
            this.LAB_Warnung.AutoSize = true;
            this.LAB_Warnung.Dock = System.Windows.Forms.DockStyle.Right;
            this.LAB_Warnung.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LAB_Warnung.ForeColor = System.Drawing.Color.Firebrick;
            this.LAB_Warnung.Location = new System.Drawing.Point(481, 0);
            this.LAB_Warnung.Name = "LAB_Warnung";
            this.LAB_Warnung.Size = new System.Drawing.Size(275, 30);
            this.LAB_Warnung.TabIndex = 6;
            this.LAB_Warnung.Text = "LAB_Warnung";
            this.LAB_Warnung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LAB_Warnung.Visible = false;
            //
            // CBB_Model_Kanal
            //
            this.CBB_Model_Kanal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CBB_Commands.Items[0], // CBB_Hinzufügen
            this.CBB_Commands.Items[1], // CBB_Löschen
            this.CBB_Commands.Items[2], // CommandBarSeparator1
            this.CBB_Commands.Items[3], // CBT_Suche
            this.CBB_Commands.Items[4], // CommandBarSeparator3
            this.CBB_Commands.Items[5], // CBT_ShowAll
            this.CBB_Commands.Items[6], // CBD_Liste_Sender
            this.CBB_Commands.Items[7], // CBB_Aufnahmen_Heute
            this.CBB_Commands.Items[8], // CBB_Favoriten
            this.CBB_Commands.Items[9], // CommandBarSeparator2
            this.CBB_Commands.Items[10], // CBB_Einstellungen
            this.CBB_Commands.Items[11], // CommandBarSeparator4
            this.CBB_Commands.Items[12]});// CBB_CamsRecorder
            this.CBB_Model_Kanal.Dock = System.Windows.Forms.DockStyle.Top;
            this.CBB_Model_Kanal.Location = new System.Drawing.Point(0, 0);
            this.CBB_Model_Kanal.Name = "CBB_Model_Kanal";
            this.CBB_Model_Kanal.Size = new System.Drawing.Size(1086, 30);
            this.CBB_Model_Kanal.TabIndex = 1;
            this.CBB_Model_Kanal.Text = "toolStrip2";
            this.CBB_Model_Kanal.Controls.Add(this.LAB_Warnung);
            this.CBB_Model_Kanal.Controls.Add(this.RadLabel1);
            this.CBB_Model_Kanal.Controls.Add(this.LAB_Drive);
            this.CBB_Model_Kanal.Controls.Add(this.PGB_Disk);
            //
            // RadLabel1
            //
            this.RadLabel1.AutoSize = true;
            this.RadLabel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RadLabel1.BackgroundImage")));
            this.RadLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RadLabel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.RadLabel1.Location = new System.Drawing.Point(756, 0);
            this.RadLabel1.Name = "RadLabel1";
            this.RadLabel1.Size = new System.Drawing.Size(131, 30);
            this.RadLabel1.TabIndex = 8;
            //
            // Cam_Benachrichtigung
            //
            this.Cam_Benachrichtigung.Icon = ((System.Drawing.Icon)(resources.GetObject("Cam_Benachrichtigung.Icon")));
            this.Cam_Benachrichtigung.Text = "notifyIcon1";
            this.Cam_Benachrichtigung.Visible = true;
            this.Cam_Benachrichtigung.ContextMenuStrip = this.CMS_Benachrichtigung;
            //
            // CMS_Benachrichtigung
            //
            this.CMS_Benachrichtigung.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CMS_Benachrichtigung.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMI_Benachrichtung_Anzeigen});
            this.CMS_Benachrichtigung.Name = "CMS_Benachrichtigung";
            this.CMS_Benachrichtigung.Size = new System.Drawing.Size(194, 28);
            //
            // TMI_Benachrichtung_Anzeigen
            //
            this.TMI_Benachrichtung_Anzeigen.CheckOnClick = true;
            this.TMI_Benachrichtung_Anzeigen.Name = "TMI_Benachrichtung_Anzeigen";
            this.TMI_Benachrichtung_Anzeigen.Size = new System.Drawing.Size(193, 24);
            this.TMI_Benachrichtung_Anzeigen.Text = "Benachrichtigungen";
            //
            // DTA_Benachrichtigung
            //
            this.DTA_Benachrichtigung.Location = new System.Drawing.Point(0, 0);
            this.DTA_Benachrichtigung.Name = "DTA_Benachrichtigung";
            this.DTA_Benachrichtigung.Size = new System.Drawing.Size(200, 100);
            this.DTA_Benachrichtigung.TabIndex = 0;
            this.DTA_Benachrichtigung.Text = "Form1";
            this.DTA_Benachrichtigung.Visible = false;
            //
            // Control_Model_Info1
            //
            this.Control_Model_Info1.Location = new System.Drawing.Point(75, 70);
            this.Control_Model_Info1.Name = "Control_Model_Info1";
            this.Control_Model_Info1.Size = new System.Drawing.Size(400, 138);
            this.Control_Model_Info1.TabIndex = 0;
            this.Control_Model_Info1.Visible = false;
            //
            // Form_Main
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 708);
            this.Controls.Add(this.Control_Model_Info1);
            this.Controls.Add(this.RadSplitContainer1);
            this.Controls.Add(this.CBB_Model_Kanal);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "XstreaMon";
            ((System.ComponentModel.ISupportInitialize)(this.GRV_Model_Kanal)).EndInit();
            this.RadPanel1.ResumeLayout(false);
            this.RadSplitContainer1.Panel1.ResumeLayout(false);
            this.RadSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RadSplitContainer1)).EndInit();
            this.RadSplitContainer1.ResumeLayout(false);
            this.PAN_Navigation.ResumeLayout(false);
            this.PAN_Streams.ResumeLayout(false);
            this.PAN_Streams.PerformLayout();
            this.CME_Model_Kanal.ResumeLayout(false);
            this.CME_Preview.ResumeLayout(false);
            this.CBB_Commands.ResumeLayout(false);
            this.CBB_Commands.PerformLayout();
            this.CBB_Model_Kanal.ResumeLayout(false);
            this.CBB_Model_Kanal.PerformLayout();
            this.CMS_Benachrichtigung.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}