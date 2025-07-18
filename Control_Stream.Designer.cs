using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Control_Stream
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Control_Stream));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PAN_Control = new System.Windows.Forms.Panel();
            this.LAB_Resolution = new System.Windows.Forms.Label();
            this.PAN_Header = new System.Windows.Forms.Panel();
            this.LAB_Bezeichnung = new System.Windows.Forms.Label();
            this.TBT_Favorite = new System.Windows.Forms.CheckBox(); // Changed from RadToggleButton to CheckBox
            this.LAB_Aufnahmezeit = new System.Windows.Forms.Label();
            this.TBT_Play = new System.Windows.Forms.CheckBox(); // Changed from RadToggleButton to CheckBox
            this.BTN_Modelview = new System.Windows.Forms.Button(); // Changed from RadButton to Button
            this.BTN_Record = new System.Windows.Forms.Button(); // Changed from RadButton to Button
            this.BTN_Close = new System.Windows.Forms.Button(); // Changed from RadButton to Button
            this.LAB_Offline = new System.Windows.Forms.Label();
            this.LAB_IP_Block = new System.Windows.Forms.Label();
            this.LAB_Gender = new System.Windows.Forms.Label();
            this.LAB_Website = new System.Windows.Forms.Label();
            this.COM_Preview = new System.Windows.Forms.ContextMenuStrip(this.components); // Changed from RadContextMenu to ContextMenuStrip
            this.CMI_Model_Promo = new System.Windows.Forms.ToolStripMenuItem(); // Changed from RadMenuItem to ToolStripMenuItem
            this.CMI_Fade_In = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Aufnahme = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_StreamRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Stop_Off = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Favoriten_Record = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem2 = new System.Windows.Forms.ToolStripSeparator(); // Changed from RadMenuSeparatorItem to ToolStripSeparator
            this.CMI_Favoriten_Model = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Galerie = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Optionen = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Webseite = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_CamBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Webseite_Kopieren = new System.Windows.Forms.ToolStripMenuItem();
            this.PAN_Control.SuspendLayout();
            this.PAN_Header.SuspendLayout();
            this.COM_Preview.SuspendLayout();
            this.SuspendLayout();
            // 
            // PAN_Control
            // 
            this.PAN_Control.BackColor = System.Drawing.Color.Transparent;
            this.PAN_Control.BackgroundImage = Resources.Status_Wait;
            this.PAN_Control.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PAN_Control.Controls.Add(this.LAB_Resolution);
            this.PAN_Control.Controls.Add(this.PAN_Header);
            this.PAN_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Control.Location = new System.Drawing.Point(1, 1);
            this.PAN_Control.Margin = new System.Windows.Forms.Padding(0);
            this.PAN_Control.Name = "PAN_Control";
            this.PAN_Control.Size = new System.Drawing.Size(398, 226);
            this.PAN_Control.TabIndex = 0;
            this.PAN_Control.ContextMenuStrip = this.COM_Preview; // Set ContextMenuStrip
            // 
            // LAB_Resolution
            // 
            this.LAB_Resolution.AutoSize = false;
            this.LAB_Resolution.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LAB_Resolution.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LAB_Resolution.Location = new System.Drawing.Point(0, 206);
            this.LAB_Resolution.Name = "LAB_Resolution";
            this.LAB_Resolution.Size = new System.Drawing.Size(398, 20);
            this.LAB_Resolution.TabIndex = 15;
            // 
            // PAN_Header
            // 
            this.PAN_Header.Controls.Add(this.LAB_Bezeichnung);
            this.PAN_Header.Controls.Add(this.TBT_Favorite);
            this.PAN_Header.Controls.Add(this.LAB_Aufnahmezeit);
            this.PAN_Header.Controls.Add(this.TBT_Play);
            this.PAN_Header.Controls.Add(this.BTN_Modelview);
            this.PAN_Header.Controls.Add(this.BTN_Record);
            this.PAN_Header.Controls.Add(this.BTN_Close);
            this.PAN_Header.Controls.Add(this.LAB_Offline);
            this.PAN_Header.Controls.Add(this.LAB_IP_Block);
            this.PAN_Header.Controls.Add(this.LAB_Gender);
            this.PAN_Header.Controls.Add(this.LAB_Website);
            this.PAN_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Header.Location = new System.Drawing.Point(0, 0);
            this.PAN_Header.Name = "PAN_Header";
            this.PAN_Header.Padding = new System.Windows.Forms.Padding(2);
            this.PAN_Header.Size = new System.Drawing.Size(398, 24);
            this.PAN_Header.TabIndex = 4;
            this.PAN_Header.Visible = false;
            // 
            // LAB_Bezeichnung
            // 
            this.LAB_Bezeichnung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LAB_Bezeichnung.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.LAB_Bezeichnung.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LAB_Bezeichnung.Location = new System.Drawing.Point(86, 2);
            this.LAB_Bezeichnung.Name = "LAB_Bezeichnung";
            this.LAB_Bezeichnung.Size = new System.Drawing.Size(214, 20);
            this.LAB_Bezeichnung.TabIndex = 2;
            this.LAB_Bezeichnung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TBT_Favorite
            // 
            this.TBT_Favorite.Appearance = System.Windows.Forms.Appearance.Button; // Make it look like a button
            this.TBT_Favorite.BackColor = System.Drawing.Color.Transparent;
            this.TBT_Favorite.Dock = System.Windows.Forms.DockStyle.Right;
            this.TBT_Favorite.Image = ((System.Drawing.Image)(resources.GetObject("TBT_Favorite.Image")));
            this.TBT_Favorite.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TBT_Favorite.Location = new System.Drawing.Point(300, 2);
            this.TBT_Favorite.Name = "TBT_Favorite";
            this.TBT_Favorite.Size = new System.Drawing.Size(22, 20);
            this.TBT_Favorite.TabIndex = 11;
            this.TBT_Favorite.UseVisualStyleBackColor = false;
            this.TBT_Favorite.Visible = false;
            // 
            // LAB_Aufnahmezeit
            // 
            this.LAB_Aufnahmezeit.Dock = System.Windows.Forms.DockStyle.Right;
            this.LAB_Aufnahmezeit.Location = new System.Drawing.Point(322, 2);
            this.LAB_Aufnahmezeit.Name = "LAB_Aufnahmezeit";
            this.LAB_Aufnahmezeit.Size = new System.Drawing.Size(2, 20);
            this.LAB_Aufnahmezeit.TabIndex = 5;
            // 
            // TBT_Play
            // 
            this.TBT_Play.Appearance = System.Windows.Forms.Appearance.Button;
            this.TBT_Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TBT_Play.Dock = System.Windows.Forms.DockStyle.Right;
            this.TBT_Play.Image = ((System.Drawing.Image)(resources.GetObject("TBT_Play.Image")));
            this.TBT_Play.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TBT_Play.Location = new System.Drawing.Point(324, 2);
            this.TBT_Play.Name = "TBT_Play";
            this.TBT_Play.Size = new System.Drawing.Size(18, 20);
            this.TBT_Play.TabIndex = 4;
            this.TBT_Play.UseVisualStyleBackColor = true;
            // 
            // BTN_Modelview
            // 
            this.BTN_Modelview.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_Modelview.Image = Resources.Galerie161;
            this.BTN_Modelview.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BTN_Modelview.Location = new System.Drawing.Point(342, 2);
            this.BTN_Modelview.Name = "BTN_Modelview";
            this.BTN_Modelview.Size = new System.Drawing.Size(18, 20);
            this.BTN_Modelview.TabIndex = 6;
            this.BTN_Modelview.UseVisualStyleBackColor = true;
            // 
            // BTN_Record
            // 
            this.BTN_Record.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_Record.Image = Resources.RecordAutomatic16;
            this.BTN_Record.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BTN_Record.Location = new System.Drawing.Point(360, 2);
            this.BTN_Record.Name = "BTN_Record";
            this.BTN_Record.Size = new System.Drawing.Size(18, 20);
            this.BTN_Record.TabIndex = 3;
            this.BTN_Record.UseVisualStyleBackColor = true;
            // 
            // BTN_Close
            // 
            this.BTN_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_Close.Image = Resources.close;
            this.BTN_Close.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BTN_Close.Location = new System.Drawing.Point(378, 2);
            this.BTN_Close.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Size = new System.Drawing.Size(18, 20);
            this.BTN_Close.TabIndex = 7;
            this.BTN_Close.UseVisualStyleBackColor = true;
            // 
            // LAB_Offline
            // 
            this.LAB_Offline.AutoSize = false;
            this.LAB_Offline.Dock = System.Windows.Forms.DockStyle.Left;
            this.LAB_Offline.Image = Resources.Offline16;
            this.LAB_Offline.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LAB_Offline.Location = new System.Drawing.Point(64, 2);
            this.LAB_Offline.Name = "LAB_Offline";
            this.LAB_Offline.Size = new System.Drawing.Size(22, 20);
            this.LAB_Offline.TabIndex = 9;
            this.LAB_Offline.Visible = false;
            // 
            // LAB_IP_Block
            // 
            this.LAB_IP_Block.AutoSize = false;
            this.LAB_IP_Block.Dock = System.Windows.Forms.DockStyle.Left;
            this.LAB_IP_Block.Image = Resources.IP_Block16;
            this.LAB_IP_Block.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LAB_IP_Block.Location = new System.Drawing.Point(42, 2);
            this.LAB_IP_Block.Name = "LAB_IP_Block";
            this.LAB_IP_Block.Size = new System.Drawing.Size(22, 20);
            this.LAB_IP_Block.TabIndex = 14;
            this.LAB_IP_Block.Visible = false;
            // 
            // LAB_Gender
            // 
            this.LAB_Gender.AutoSize = false;
            this.LAB_Gender.Dock = System.Windows.Forms.DockStyle.Left;
            this.LAB_Gender.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LAB_Gender.Location = new System.Drawing.Point(20, 2);
            this.LAB_Gender.Name = "LAB_Gender";
            this.LAB_Gender.Size = new System.Drawing.Size(22, 20);
            this.LAB_Gender.TabIndex = 12;
            // 
            // LAB_Website
            // 
            this.LAB_Website.AutoSize = false;
            this.LAB_Website.Dock = System.Windows.Forms.DockStyle.Left;
            this.LAB_Website.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LAB_Website.Location = new System.Drawing.Point(2, 2);
            this.LAB_Website.Name = "LAB_Website";
            this.LAB_Website.Size = new System.Drawing.Size(18, 20);
            this.LAB_Website.TabIndex = 13;
            // 
            // COM_Preview
            // 
            this.COM_Preview.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.COM_Preview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMI_Model_Promo,
            this.CMI_Fade_In,
            this.CMI_Aufnahme,
            this.CMI_StreamRefresh,
            this.CMI_Stop_Off,
            this.CMI_Favoriten_Record,
            this.RadMenuSeparatorItem2,
            this.CMI_Favoriten_Model,
            this.CMI_Galerie,
            this.CMI_Info,
            this.CMI_Optionen,
            this.RadMenuSeparatorItem1,
            this.CMI_Webseite,
            this.CMI_CamBrowser,
            this.CMI_Webseite_Kopieren});
            this.COM_Preview.Name = "COM_Preview";
            this.COM_Preview.Size = new System.Drawing.Size(290, 320); // Adjusted size for native context menu
            // 
            // CMI_Model_Promo
            // 
            this.CMI_Model_Promo.Name = "CMI_Model_Promo";
            this.CMI_Model_Promo.Size = new System.Drawing.Size(289, 24);
            this.CMI_Model_Promo.Text = "in die Modelliste aufnehmen";
            // 
            // CMI_Fade_In
            // 
            this.CMI_Fade_In.Image = Resources.View16;
            this.CMI_Fade_In.Name = "CMI_Fade_In";
            this.CMI_Fade_In.Size = new System.Drawing.Size(289, 24);
            this.CMI_Fade_In.Text = "Einblenden";
            // 
            // CMI_Aufnahme
            // 
            this.CMI_Aufnahme.Name = "CMI_Aufnahme";
            this.CMI_Aufnahme.Size = new System.Drawing.Size(289, 24);
            this.CMI_Aufnahme.Text = "Automatische Aufnahme";
            // 
            // CMI_StreamRefresh
            // 
            this.CMI_StreamRefresh.Image = Resources.refresh16;
            this.CMI_StreamRefresh.Name = "CMI_StreamRefresh";
            this.CMI_StreamRefresh.Size = new System.Drawing.Size(289, 24);
            this.CMI_StreamRefresh.Text = "Streamadressen aktualisieren";
            // 
            // CMI_Stop_Off
            // 
            this.CMI_Stop_Off.Name = "CMI_Stop_Off";
            this.CMI_Stop_Off.Size = new System.Drawing.Size(289, 24);
            this.CMI_Stop_Off.Text = "Automatisches Aufnahmestop ausschalten";
            // 
            // CMI_Favoriten_Record
            // 
            this.CMI_Favoriten_Record.Name = "CMI_Favoriten_Record";
            this.CMI_Favoriten_Record.Size = new System.Drawing.Size(289, 24);
            this.CMI_Favoriten_Record.Text = "Aufnahme zu Favoriten hinzufügen";
            // 
            // RadMenuSeparatorItem2
            // 
            this.RadMenuSeparatorItem2.Name = "RadMenuSeparatorItem2";
            this.RadMenuSeparatorItem2.Size = new System.Drawing.Size(286, 6);
            // 
            // CMI_Favoriten_Model
            // 
            this.CMI_Favoriten_Model.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Favoriten_Model.Image")));
            this.CMI_Favoriten_Model.Name = "CMI_Favoriten_Model";
            this.CMI_Favoriten_Model.Size = new System.Drawing.Size(289, 24);
            this.CMI_Favoriten_Model.Text = "";
            // 
            // CMI_Galerie
            // 
            this.CMI_Galerie.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Galerie.Image")));
            this.CMI_Galerie.Name = "CMI_Galerie";
            this.CMI_Galerie.Size = new System.Drawing.Size(289, 24);
            this.CMI_Galerie.Text = "Galerie";
            // 
            // CMI_Info
            // 
            this.CMI_Info.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Info.Image")));
            this.CMI_Info.Name = "CMI_Info";
            this.CMI_Info.Size = new System.Drawing.Size(289, 24);
            this.CMI_Info.Text = "Info bearbeiten";
            // 
            // CMI_Optionen
            // 
            this.CMI_Optionen.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Optionen.Image")));
            this.CMI_Optionen.Name = "CMI_Optionen";
            this.CMI_Optionen.Size = new System.Drawing.Size(289, 24);
            this.CMI_Optionen.Text = "Einstellungen";
            // 
            // RadMenuSeparatorItem1
            // 
            this.RadMenuSeparatorItem1.Name = "RadMenuSeparatorItem1";
            this.RadMenuSeparatorItem1.Size = new System.Drawing.Size(286, 6);
            // 
            // CMI_Webseite
            // 
            this.CMI_Webseite.Image = ((System.Drawing.Image)(resources.GetObject("CMI_Webseite.Image")));
            this.CMI_Webseite.Name = "CMI_Webseite";
            this.CMI_Webseite.Size = new System.Drawing.Size(289, 24);
            this.CMI_Webseite.Text = "Webseite öffnen";
            // 
            // CMI_CamBrowser
            // 
            this.CMI_CamBrowser.Image = Resources.CamBrowser16;
            this.CMI_CamBrowser.Name = "CMI_CamBrowser";
            this.CMI_CamBrowser.Size = new System.Drawing.Size(289, 24);
            this.CMI_CamBrowser.Text = "Webseite öffnen mit CamBrowser";
            // 
            // CMI_Webseite_Kopieren
            // 
            this.CMI_Webseite_Kopieren.Image = Resources.Kopieren16;
            this.CMI_Webseite_Kopieren.Name = "CMI_Webseite_Kopieren";
            this.CMI_Webseite_Kopieren.Size = new System.Drawing.Size(289, 24);
            this.CMI_Webseite_Kopieren.Text = "URL Kopieren";
            // 
            // Control_Stream
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.PAN_Control);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Control_Stream";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(400, 228);
            this.PAN_Control.ResumeLayout(false);
            this.PAN_Header.ResumeLayout(false);
            this.COM_Preview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Declare controls as internal virtual for accessibility from the main class
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.Panel PAN_Control;
        internal System.Windows.Forms.Label LAB_Resolution;
        internal System.Windows.Forms.Panel PAN_Header;
        internal System.Windows.Forms.Label LAB_Bezeichnung;
        internal System.Windows.Forms.CheckBox TBT_Favorite; // Changed to CheckBox
        internal System.Windows.Forms.Label LAB_Aufnahmezeit;
        internal System.Windows.Forms.CheckBox TBT_Play; // Changed to CheckBox
        internal System.Windows.Forms.Button BTN_Modelview; // Changed to Button
        internal System.Windows.Forms.Button BTN_Record; // Changed to Button
        internal System.Windows.Forms.Button BTN_Close; // Changed to Button
        internal System.Windows.Forms.Label LAB_Offline;
        internal System.Windows.Forms.Label LAB_IP_Block;
        internal System.Windows.Forms.Label LAB_Gender;
        internal System.Windows.Forms.Label LAB_Website;
        internal System.Windows.Forms.ContextMenuStrip COM_Preview; // Changed to ContextMenuStrip
        internal System.Windows.Forms.ToolStripMenuItem CMI_Model_Promo; // Changed to ToolStripMenuItem
        internal System.Windows.Forms.ToolStripMenuItem CMI_Fade_In;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Aufnahme;
        internal System.Windows.Forms.ToolStripMenuItem CMI_StreamRefresh;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Stop_Off;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Favoriten_Record;
        internal System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem2; // Changed to ToolStripSeparator
        internal System.Windows.Forms.ToolStripMenuItem CMI_Favoriten_Model;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Galerie;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Info;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Optionen;
        internal System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem1;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Webseite;
        internal System.Windows.Forms.ToolStripMenuItem CMI_CamBrowser;
        internal System.Windows.Forms.ToolStripMenuItem CMI_Webseite_Kopieren;
    }
}
