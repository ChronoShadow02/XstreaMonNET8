// Archivo: Dialog_Video_Propertys.Designer.cs
using System.ComponentModel;

namespace XstreaMonNET8
{
    partial class Dialog_Video_Propertys
    {
        private System.ComponentModel.IContainer components = null;

        internal GroupBox RadGroupBox1;
        internal Button BTN_Refresh;
        internal Label LAB_File_Typ;
        internal Label RadLabel11;
        internal Label LAB_Länge;
        internal Label LAB_Ende;
        internal Label LAB_Framerate;
        internal Label LAB_Resolution;
        internal Label LAB_Change;
        internal Label RadLabel15;
        internal Label LAB_Create;
        internal Label RadLabel12;
        internal Label LAB_File_Size;
        internal Label RadLabel10;
        internal Label RadLabel8;
        internal Label RadLabel7;
        internal Label RadLabel6;
        internal Label RadLabel5;
        internal Label RadLabel4;
        internal Label RadLabel3;
        internal ComboBox DDL_Provider;
        internal DateTimePicker DTP_Start;
        internal TextBox TXB_Channel_Name;
        internal Label RadLabel2;
        internal TextBox TXB_File_Name;
        internal Label RadLabel1;

        internal GroupBox RadGroupBox2;
        internal SplitContainer RadSplitContainer1;
        internal Panel SplitPanel1;
        internal SplitContainer RadSplitContainer2;
        internal Panel SplitPanel3;
        internal Panel PAN_Preview_Image;
        internal Label LAB_Preview_Image;
        internal TrackBar TRB_VideoPosition;
        internal Panel SplitPanel4;
        internal Panel PAN_Tiles_Image;
        internal Button BTN_Tiles_Refresh;
        internal Label LAB_Tiles_Image;
        internal Panel SplitPanel2;
        internal Panel PAN_Timeline_Image;
        internal Label LAB_Timeline_Image;
        internal Button BTN_Timeline_Refresh;
        internal Panel RadPanel4;
        internal Button BTN_Übernehmen;
        internal Button BTN_Abbrechen;

        /// <summary>
        /// Limpia los recursos que se estén usando.
        /// </summary>
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Dialog_Video_Propertys));
            this.components = new Container();

            // ============================
            // RadGroupBox1
            // ============================
            this.RadGroupBox1 = new GroupBox();
            this.RadGroupBox1.AccessibleRole = AccessibleRole.Grouping;
            this.RadGroupBox1.Dock = DockStyle.Top;
            this.RadGroupBox1.Location = new Point(0, 0);
            this.RadGroupBox1.Name = "RadGroupBox1";
            this.RadGroupBox1.Size = new Size(761, 180);
            this.RadGroupBox1.TabIndex = 0;
            this.RadGroupBox1.Text = "Datei";

            // BTN_Refresh
            this.BTN_Refresh = new Button();
            this.BTN_Refresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.BTN_Refresh.Image = Resources.refresh_32;
            this.BTN_Refresh.Location = new Point(709, 12);
            this.BTN_Refresh.Name = "BTN_Refresh";
            this.BTN_Refresh.Size = new Size(47, 46);
            this.BTN_Refresh.TabIndex = 29;
            this.BTN_Refresh.UseVisualStyleBackColor = true;
            this.BTN_Refresh.Click += new EventHandler(this.BTN_Refresh_Click);

            // LAB_File_Typ
            this.LAB_File_Typ = new Label();
            this.LAB_File_Typ.Location = new Point(493, 22);
            this.LAB_File_Typ.Name = "LAB_File_Typ";
            this.LAB_File_Typ.Size = new Size(2, 2);
            this.LAB_File_Typ.TabIndex = 28;

            // RadLabel11
            this.RadLabel11 = new Label();
            this.RadLabel11.Location = new Point(393, 22);
            this.RadLabel11.Name = "RadLabel11";
            this.RadLabel11.Size = new Size(48, 18);
            this.RadLabel11.TabIndex = 27;
            this.RadLabel11.Text = "Dateityp";

            // LAB_Länge
            this.LAB_Länge = new Label();
            this.LAB_Länge.Location = new Point(116, 153);
            this.LAB_Länge.Name = "LAB_Länge";
            this.LAB_Länge.Size = new Size(2, 2);
            this.LAB_Länge.TabIndex = 26;

            // LAB_Ende
            this.LAB_Ende = new Label();
            this.LAB_Ende.Location = new Point(116, 125);
            this.LAB_Ende.Name = "LAB_Ende";
            this.LAB_Ende.Size = new Size(2, 2);
            this.LAB_Ende.TabIndex = 26;

            // LAB_Framerate
            this.LAB_Framerate = new Label();
            this.LAB_Framerate.Location = new Point(493, 152);
            this.LAB_Framerate.Name = "LAB_Framerate";
            this.LAB_Framerate.Size = new Size(2, 2);
            this.LAB_Framerate.TabIndex = 26;

            // LAB_Resolution
            this.LAB_Resolution = new Label();
            this.LAB_Resolution.Location = new Point(493, 126);
            this.LAB_Resolution.Name = "LAB_Resolution";
            this.LAB_Resolution.Size = new Size(2, 2);
            this.LAB_Resolution.TabIndex = 25;

            // LAB_Change
            this.LAB_Change = new Label();
            this.LAB_Change.Location = new Point(493, 99);
            this.LAB_Change.Name = "LAB_Change";
            this.LAB_Change.Size = new Size(2, 2);
            this.LAB_Change.TabIndex = 24;

            // RadLabel15
            this.RadLabel15 = new Label();
            this.RadLabel15.Location = new Point(393, 99);
            this.RadLabel15.Name = "RadLabel15";
            this.RadLabel15.Size = new Size(52, 18);
            this.RadLabel15.TabIndex = 23;
            this.RadLabel15.Text = "Geändert";

            // LAB_Create
            this.LAB_Create = new Label();
            this.LAB_Create.Location = new Point(493, 73);
            this.LAB_Create.Name = "LAB_Create";
            this.LAB_Create.Size = new Size(2, 2);
            this.LAB_Create.TabIndex = 22;

            // RadLabel12
            this.RadLabel12 = new Label();
            this.RadLabel12.Location = new Point(393, 73);
            this.RadLabel12.Name = "RadLabel12";
            this.RadLabel12.Size = new Size(40, 18);
            this.RadLabel12.TabIndex = 21;
            this.RadLabel12.Text = "Erstellt";

            // LAB_File_Size
            this.LAB_File_Size = new Label();
            this.LAB_File_Size.Location = new Point(493, 47);
            this.LAB_File_Size.Name = "LAB_File_Size";
            this.LAB_File_Size.Size = new Size(2, 2);
            this.LAB_File_Size.TabIndex = 20;

            // RadLabel10
            this.RadLabel10 = new Label();
            this.RadLabel10.Location = new Point(393, 47);
            this.RadLabel10.Name = "RadLabel10";
            this.RadLabel10.Size = new Size(37, 18);
            this.RadLabel10.TabIndex = 19;
            this.RadLabel10.Text = "Größe";

            // RadLabel8
            this.RadLabel8 = new Label();
            this.RadLabel8.Location = new Point(7, 151);
            this.RadLabel8.Name = "RadLabel8";
            this.RadLabel8.Size = new Size(36, 18);
            this.RadLabel8.TabIndex = 16;
            this.RadLabel8.Text = "Länge";

            // RadLabel7
            this.RadLabel7 = new Label();
            this.RadLabel7.Location = new Point(7, 125);
            this.RadLabel7.Name = "RadLabel7";
            this.RadLabel7.Size = new Size(31, 18);
            this.RadLabel7.TabIndex = 15;
            this.RadLabel7.Text = "Ende";

            // RadLabel6
            this.RadLabel6 = new Label();
            this.RadLabel6.Location = new Point(7, 99);
            this.RadLabel6.Name = "RadLabel6";
            this.RadLabel6.Size = new Size(30, 18);
            this.RadLabel6.TabIndex = 14;
            this.RadLabel6.Text = "Start";

            // RadLabel5
            this.RadLabel5 = new Label();
            this.RadLabel5.Location = new Point(7, 47);
            this.RadLabel5.Name = "RadLabel5";
            this.RadLabel5.Size = new Size(53, 18);
            this.RadLabel5.TabIndex = 13;
            this.RadLabel5.Text = "Webseite";

            // RadLabel4
            this.RadLabel4 = new Label();
            this.RadLabel4.Location = new Point(393, 152);
            this.RadLabel4.Name = "RadLabel4";
            this.RadLabel4.Size = new Size(57, 18);
            this.RadLabel4.TabIndex = 10;
            this.RadLabel4.Text = "Framerate";

            // RadLabel3
            this.RadLabel3 = new Label();
            this.RadLabel3.Location = new Point(393, 126);
            this.RadLabel3.Name = "RadLabel3";
            this.RadLabel3.Size = new Size(57, 18);
            this.RadLabel3.TabIndex = 8;
            this.RadLabel3.Text = "Auflösung";

            // DDL_Provider
            this.DDL_Provider = new ComboBox();
            this.DDL_Provider.Location = new Point(116, 47);
            this.DDL_Provider.Name = "DDL_Provider";
            this.DDL_Provider.Size = new Size(264, 21);
            this.DDL_Provider.TabIndex = 1;

            // DTP_Start
            this.DTP_Start = new DateTimePicker();
            this.DTP_Start.Format = DateTimePickerFormat.Custom;
            this.DTP_Start.Location = new Point(116, 99);
            this.DTP_Start.Name = "DTP_Start";
            this.DTP_Start.Size = new Size(264, 20);
            this.DTP_Start.TabIndex = 3;
            this.DTP_Start.Value = DateTime.Now;
            this.DTP_Start.ValueChanged += new EventHandler(this.DTP_Start_ValueChanged);

            // TXB_Channel_Name
            this.TXB_Channel_Name = new TextBox();
            this.TXB_Channel_Name.Location = new Point(116, 73);
            this.TXB_Channel_Name.Name = "TXB_Channel_Name";
            this.TXB_Channel_Name.Size = new Size(264, 20);
            this.TXB_Channel_Name.TabIndex = 2;

            // RadLabel2
            this.RadLabel2 = new Label();
            this.RadLabel2.Location = new Point(7, 73);
            this.RadLabel2.Name = "RadLabel2";
            this.RadLabel2.Size = new Size(61, 18);
            this.RadLabel2.TabIndex = 2;
            this.RadLabel2.Text = "Kanalname";

            // TXB_File_Name
            this.TXB_File_Name = new TextBox();
            this.TXB_File_Name.Location = new Point(116, 21);
            this.TXB_File_Name.Name = "TXB_File_Name";
            this.TXB_File_Name.Size = new Size(264, 20);
            this.TXB_File_Name.TabIndex = 0;

            // RadLabel1
            this.RadLabel1 = new Label();
            this.RadLabel1.Location = new Point(7, 21);
            this.RadLabel1.Name = "RadLabel1";
            this.RadLabel1.Size = new Size(60, 18);
            this.RadLabel1.TabIndex = 0;
            this.RadLabel1.Text = "Dateiname";

            this.RadGroupBox1.Controls.Add(this.BTN_Refresh);
            this.RadGroupBox1.Controls.Add(this.LAB_File_Typ);
            this.RadGroupBox1.Controls.Add(this.RadLabel11);
            this.RadGroupBox1.Controls.Add(this.LAB_Länge);
            this.RadGroupBox1.Controls.Add(this.LAB_Ende);
            this.RadGroupBox1.Controls.Add(this.LAB_Framerate);
            this.RadGroupBox1.Controls.Add(this.LAB_Resolution);
            this.RadGroupBox1.Controls.Add(this.LAB_Change);
            this.RadGroupBox1.Controls.Add(this.RadLabel15);
            this.RadGroupBox1.Controls.Add(this.LAB_Create);
            this.RadGroupBox1.Controls.Add(this.RadLabel12);
            this.RadGroupBox1.Controls.Add(this.LAB_File_Size);
            this.RadGroupBox1.Controls.Add(this.RadLabel10);
            this.RadGroupBox1.Controls.Add(this.RadLabel8);
            this.RadGroupBox1.Controls.Add(this.RadLabel7);
            this.RadGroupBox1.Controls.Add(this.RadLabel6);
            this.RadGroupBox1.Controls.Add(this.RadLabel5);
            this.RadGroupBox1.Controls.Add(this.RadLabel4);
            this.RadGroupBox1.Controls.Add(this.RadLabel3);
            this.RadGroupBox1.Controls.Add(this.DDL_Provider);
            this.RadGroupBox1.Controls.Add(this.DTP_Start);
            this.RadGroupBox1.Controls.Add(this.TXB_Channel_Name);
            this.RadGroupBox1.Controls.Add(this.RadLabel2);
            this.RadGroupBox1.Controls.Add(this.TXB_File_Name);
            this.RadGroupBox1.Controls.Add(this.RadLabel1);

            // ============================
            // RadGroupBox2
            // ============================
            this.RadGroupBox2 = new GroupBox();
            this.RadGroupBox2.AccessibleRole = AccessibleRole.Grouping;
            this.RadGroupBox2.Dock = DockStyle.Fill;
            this.RadGroupBox2.Location = new Point(0, 180);
            this.RadGroupBox2.Name = "RadGroupBox2";
            this.RadGroupBox2.Size = new Size(761, 363);
            this.RadGroupBox2.TabIndex = 1;
            this.RadGroupBox2.Text = "Vorschau";

            // RadSplitContainer1
            this.RadSplitContainer1 = new SplitContainer();
            this.RadSplitContainer1.Dock = DockStyle.Fill;
            this.RadSplitContainer1.Location = new Point(3, 16);
            this.RadSplitContainer1.Name = "RadSplitContainer1";
            this.RadSplitContainer1.Orientation = Orientation.Horizontal;
            this.RadSplitContainer1.SplitterWidth = 0;
            this.RadSplitContainer1.TabIndex = 0;
            this.RadSplitContainer1.TabStop = false;
            this.RadSplitContainer1.Size = new Size(755, 344);

            // SplitPanel1
            this.SplitPanel1 = new Panel();
            this.SplitPanel1.Location = new Point(0, 0);
            this.SplitPanel1.Name = "SplitPanel1";
            this.SplitPanel1.Size = new Size(755, 295);
            this.SplitPanel1.TabIndex = 0;
            this.SplitPanel1.BorderStyle = BorderStyle.None;

            // SplitPanel2
            this.SplitPanel2 = new Panel();
            this.SplitPanel2.Location = new Point(0, 295);
            this.SplitPanel2.Name = "SplitPanel2";
            this.SplitPanel2.Size = new Size(755, 48);
            this.SplitPanel2.TabIndex = 1;
            this.SplitPanel2.BorderStyle = BorderStyle.None;

            this.RadSplitContainer1.Panel1.Controls.Add(this.SplitPanel1);
            this.RadSplitContainer1.Panel2.Controls.Add(this.SplitPanel2);

            this.RadGroupBox2.Controls.Add(this.RadSplitContainer1);

            // RadSplitContainer2
            this.RadSplitContainer2 = new SplitContainer();
            this.RadSplitContainer2.Dock = DockStyle.Fill;
            this.RadSplitContainer2.Location = new Point(0, 0);
            this.RadSplitContainer2.Name = "RadSplitContainer2";
            this.RadSplitContainer2.Orientation = Orientation.Vertical;
            this.RadSplitContainer2.SplitterWidth = 0;
            this.RadSplitContainer2.TabIndex = 0;
            this.RadSplitContainer2.TabStop = false;
            this.RadSplitContainer2.Size = new Size(755, 295);

            // SplitPanel3
            this.SplitPanel3 = new Panel();
            this.SplitPanel3.Location = new Point(0, 0);
            this.SplitPanel3.Name = "SplitPanel3";
            this.SplitPanel3.Size = new Size(378, 295);
            this.SplitPanel3.TabIndex = 0;
            this.SplitPanel3.BorderStyle = BorderStyle.None;

            // SplitPanel4
            this.SplitPanel4 = new Panel();
            this.SplitPanel4.Location = new Point(378, 0);
            this.SplitPanel4.Name = "SplitPanel4";
            this.SplitPanel4.Size = new Size(377, 295);
            this.SplitPanel4.TabIndex = 1;
            this.SplitPanel4.BorderStyle = BorderStyle.None;

            this.RadSplitContainer2.Panel1.Controls.Add(this.SplitPanel3);
            this.RadSplitContainer2.Panel2.Controls.Add(this.SplitPanel4);

            this.SplitPanel1.Controls.Add(this.RadSplitContainer2);

            // PAN_Preview_Image
            this.PAN_Preview_Image = new Panel();
            this.PAN_Preview_Image.BackColor = Color.Transparent;
            this.PAN_Preview_Image.BackgroundImageLayout = ImageLayout.Zoom;
            this.PAN_Preview_Image.Dock = DockStyle.Fill;
            this.PAN_Preview_Image.Location = new Point(0, 0);
            this.PAN_Preview_Image.Name = "PAN_Preview_Image";
            this.PAN_Preview_Image.Padding = new Padding(3);
            this.PAN_Preview_Image.Size = new Size(378, 295);
            this.PAN_Preview_Image.TabIndex = 1;

            // LAB_Preview_Image
            this.LAB_Preview_Image = new Label();
            this.LAB_Preview_Image.AutoSize = false;
            this.LAB_Preview_Image.Dock = DockStyle.Fill;
            this.LAB_Preview_Image.BackgroundImageLayout = ImageLayout.Zoom;
            this.LAB_Preview_Image.Location = new Point(3, 3);
            this.LAB_Preview_Image.Name = "LAB_Preview_Image";
            this.LAB_Preview_Image.Size = new Size(372, 264);
            this.LAB_Preview_Image.TabIndex = 3;

            // TRB_VideoPosition
            this.TRB_VideoPosition = new TrackBar();
            this.TRB_VideoPosition.Dock = DockStyle.Bottom;
            this.TRB_VideoPosition.Location = new Point(3, 267);
            this.TRB_VideoPosition.Name = "TRB_VideoPosition";
            this.TRB_VideoPosition.Size = new Size(372, 25);
            this.TRB_VideoPosition.TabIndex = 0;
            this.TRB_VideoPosition.TickStyle = TickStyle.BottomRight;
            this.TRB_VideoPosition.ValueChanged += new EventHandler(this.TRB_VideoPosition_ValueChanged);

            this.PAN_Preview_Image.Controls.Add(this.LAB_Preview_Image);
            this.PAN_Preview_Image.Controls.Add(this.TRB_VideoPosition);

            this.SplitPanel3.Controls.Add(this.PAN_Preview_Image);

            // PAN_Tiles_Image
            this.PAN_Tiles_Image = new Panel();
            this.PAN_Tiles_Image.BackColor = Color.Transparent;
            this.PAN_Tiles_Image.BackgroundImageLayout = ImageLayout.Zoom;
            this.PAN_Tiles_Image.Dock = DockStyle.Fill;
            this.PAN_Tiles_Image.Location = new Point(0, 0);
            this.PAN_Tiles_Image.Name = "PAN_Tiles_Image";
            this.PAN_Tiles_Image.Padding = new Padding(3, 3, 3, 28);
            this.PAN_Tiles_Image.Size = new Size(377, 295);
            this.PAN_Tiles_Image.TabIndex = 2;

            // BTN_Tiles_Refresh
            this.BTN_Tiles_Refresh = new Button();
            this.BTN_Tiles_Refresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.BTN_Tiles_Refresh.Image = Resources.refresh_32;
            this.BTN_Tiles_Refresh.Location = new Point(329, 3);
            this.BTN_Tiles_Refresh.Name = "BTN_Tiles_Refresh";
            this.BTN_Tiles_Refresh.Size = new Size(47, 46);
            this.BTN_Tiles_Refresh.TabIndex = 1;
            this.BTN_Tiles_Refresh.UseVisualStyleBackColor = true;
            this.BTN_Tiles_Refresh.Click += new EventHandler(this.BTN_Tiles_Refresh_Click);

            // LAB_Tiles_Image
            this.LAB_Tiles_Image = new Label();
            this.LAB_Tiles_Image.AutoSize = false;
            this.LAB_Tiles_Image.Dock = DockStyle.Fill;
            this.LAB_Tiles_Image.BackgroundImageLayout = ImageLayout.Zoom;
            this.LAB_Tiles_Image.Location = new Point(3, 3);
            this.LAB_Tiles_Image.Name = "LAB_Tiles_Image";
            this.LAB_Tiles_Image.Size = new Size(371, 264);
            this.LAB_Tiles_Image.TabIndex = 2;

            this.PAN_Tiles_Image.Controls.Add(this.BTN_Tiles_Refresh);
            this.PAN_Tiles_Image.Controls.Add(this.LAB_Tiles_Image);

            this.SplitPanel4.Controls.Add(this.PAN_Tiles_Image);

            // PAN_Timeline_Image
            this.PAN_Timeline_Image = new Panel();
            this.PAN_Timeline_Image.BackColor = Color.Transparent;
            this.PAN_Timeline_Image.BackgroundImageLayout = ImageLayout.Center;
            this.PAN_Timeline_Image.Dock = DockStyle.Fill;
            this.PAN_Timeline_Image.Location = new Point(0, 0);
            this.PAN_Timeline_Image.Name = "PAN_Timeline_Image";
            this.PAN_Timeline_Image.Padding = new Padding(3);
            this.PAN_Timeline_Image.Size = new Size(755, 48);
            this.PAN_Timeline_Image.TabIndex = 2;

            // LAB_Timeline_Image
            this.LAB_Timeline_Image = new Label();
            this.LAB_Timeline_Image.AutoSize = false;
            this.LAB_Timeline_Image.Dock = DockStyle.Fill;
            this.LAB_Timeline_Image.BackgroundImageLayout = ImageLayout.Center;
            this.LAB_Timeline_Image.Location = new Point(3, 3);
            this.LAB_Timeline_Image.Name = "LAB_Timeline_Image";
            this.LAB_Timeline_Image.Size = new Size(701, 42);
            this.LAB_Timeline_Image.TabIndex = 3;

            // BTN_Timeline_Refresh
            this.BTN_Timeline_Refresh = new Button();
            this.BTN_Timeline_Refresh.Dock = DockStyle.Right;
            this.BTN_Timeline_Refresh.Image = Resources.refresh_32;
            this.BTN_Timeline_Refresh.Location = new Point(704, 3);
            this.BTN_Timeline_Refresh.Name = "BTN_Timeline_Refresh";
            this.BTN_Timeline_Refresh.Size = new Size(48, 42);
            this.BTN_Timeline_Refresh.TabIndex = 1;
            this.BTN_Timeline_Refresh.UseVisualStyleBackColor = true;
            this.BTN_Timeline_Refresh.Click += new EventHandler(this.BTN_Timeline_Refresh_Click);

            this.PAN_Timeline_Image.Controls.Add(this.LAB_Timeline_Image);
            this.PAN_Timeline_Image.Controls.Add(this.BTN_Timeline_Refresh);

            this.SplitPanel2.Controls.Add(this.PAN_Timeline_Image);

            // ============================
            // RadPanel4
            // ============================
            this.RadPanel4 = new Panel();
            this.RadPanel4.Dock = DockStyle.Bottom;
            this.RadPanel4.Location = new Point(0, 543);
            this.RadPanel4.Name = "RadPanel4";
            this.RadPanel4.Size = new Size(761, 44);
            this.RadPanel4.TabIndex = 2;

            // BTN_Übernehmen
            this.BTN_Übernehmen = new Button();
            this.BTN_Übernehmen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.BTN_Übernehmen.Location = new Point(523, 8);
            this.BTN_Übernehmen.Name = "BTN_Übernehmen";
            this.BTN_Übernehmen.Size = new Size(110, 24);
            this.BTN_Übernehmen.TabIndex = 0;
            this.BTN_Übernehmen.Text = "Übernehmen";
            this.BTN_Übernehmen.UseVisualStyleBackColor = true;
            this.BTN_Übernehmen.Click += new EventHandler(this.BTN_Übernehmen_Click);

            // BTN_Abbrechen
            this.BTN_Abbrechen = new Button();
            this.BTN_Abbrechen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.BTN_Abbrechen.DialogResult = DialogResult.Cancel;
            this.BTN_Abbrechen.Location = new Point(639, 8);
            this.BTN_Abbrechen.Name = "BTN_Abbrechen";
            this.BTN_Abbrechen.Size = new Size(110, 24);
            this.BTN_Abbrechen.TabIndex = 1;
            this.BTN_Abbrechen.Text = "Abbrechen";
            this.BTN_Abbrechen.UseVisualStyleBackColor = true;
            this.BTN_Abbrechen.Click += new EventHandler(this.BTN_Abbrechen_Click);

            this.RadPanel4.Controls.Add(this.BTN_Übernehmen);
            this.RadPanel4.Controls.Add(this.BTN_Abbrechen);

            // ============================
            // Configuración final del Form
            // ============================
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(761, 587);
            this.Controls.Add(this.RadGroupBox2);
            this.Controls.Add(this.RadPanel4);
            this.Controls.Add(this.RadGroupBox1);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Video Eigenschaften";
            this.Text = "Video Eigenschaften";
        }
    }
}
