using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Dialog_Model_Einstellungen
    {
        private IContainer components = null;

        private Panel RadPanel1;
        private Button BTN_Übernehmen;
        private Button BTN_Abbrechen;
        private TextBox TXB_Sender_Name;
        private Button RadButtonElement1;      // Botón de búsqueda junto al TextBox
        private NumericUpDown SPE_Recordtime;
        private GroupBox RadGroupBox1;
        private Label RadLabel3;
        private RadioButton RBT_Aufnahmestop_Size;
        private NumericUpDown SPE_FileSize;
        private Label RadLabel2;
        private RadioButton RBT_Aufnahmestop_Time;
        private RadioButton RBT_Aufnahmestop_NoStop;

        private GroupBox RadGroupBox2;
        private ComboBox DDL_Speicherformat;
        private Label RadLabel9;
        private Button BTN_Folder;
        private ComboBox DDL_Video_Encoder;
        private TextBox TXB_Folder;
        private Label RadLabel7;
        private Label RadLabel1;

        private RadioButton RBT_Video_FullHD;
        private RadioButton RBT_Video_HD;
        private RadioButton RBT_Video_SD;
        private RadioButton RBT_Video_Minimal;
        private RadioButton RBT_Video_Send;
        private CheckBox CBX_Benachrichtigung;

        private TabControl PVP_Einstellungen;
        private TabPage PVP_Info;
        private TabPage PVP_Aufnahme;

        private GroupBox RadGroupBox6;
        private Label RadLabel8;
        private TextBox TXB_Land;
        private Label RadLabel4;
        private TextBox TXB_Name;
        private TextBox TXB_Sprachen;
        private PictureBox LAB_Image;  // Se usa PictureBox para mostrar la imagen del modelo
        private Label RadLabel5;
        private Label RadLabel6;
        private DateTimePicker DTP_Geburtstag;

        private GroupBox RadGroupBox5;
        private CheckBox CBX_Favoriten;
        private CheckBox CBX_Record;
        private CheckBox CBX_Visible;

        private GroupBox RadGroupBox3;
        private RadioButton RBT_Geschlecht_Trans;
        private RadioButton RBT_Geschlecht_Sonstiges;
        private RadioButton RBT_Geschlecht_Paar;
        private RadioButton RBT_Geschlecht_Männlich;
        private RadioButton RBT_Geschlecht_Weiblich;

        private Label LAB_Kanal_Gefunden;
        private ComboBox DDL_Webseite;
        private ProgressBar RadProgressBar1;
        private System.Windows.Forms.GroupBox RadGroupBox4;
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

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Dialog_Model_Einstellungen));

            // ----------------------------------
            // RadPanel1 (Panel inferior con botones)
            // ----------------------------------
            this.RadPanel1 = new Panel();
            this.BTN_Übernehmen = new Button();
            this.BTN_Abbrechen = new Button();
            this.RadPanel1.SuspendLayout();

            // 
            // RadPanel1
            // 
            this.RadPanel1.Controls.Add(this.BTN_Übernehmen);
            this.RadPanel1.Controls.Add(this.BTN_Abbrechen);
            this.RadPanel1.Dock = DockStyle.Bottom;
            this.RadPanel1.Location = new Point(0, 442);
            this.RadPanel1.Name = "RadPanel1";
            this.RadPanel1.Size = new Size(529, 44);
            this.RadPanel1.TabIndex = 3;
            // 
            // BTN_Übernehmen
            // 
            this.BTN_Übernehmen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.BTN_Übernehmen.Location = new Point(291, 8);
            this.BTN_Übernehmen.Name = "BTN_Übernehmen";
            this.BTN_Übernehmen.Size = new Size(110, 24);
            this.BTN_Übernehmen.TabIndex = 0;
            this.BTN_Übernehmen.Text = "Übernehmen";
            this.BTN_Übernehmen.UseVisualStyleBackColor = true;
            this.BTN_Übernehmen.Click += new EventHandler(this.BTN_Übernehmen_Click);
            // 
            // BTN_Abbrechen
            // 
            this.BTN_Abbrechen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.BTN_Abbrechen.DialogResult = DialogResult.Cancel;
            this.BTN_Abbrechen.Location = new Point(407, 8);
            this.BTN_Abbrechen.Name = "BTN_Abbrechen";
            this.BTN_Abbrechen.Size = new Size(110, 24);
            this.BTN_Abbrechen.TabIndex = 1;
            this.BTN_Abbrechen.Text = "Abbrechen";
            this.BTN_Abbrechen.UseVisualStyleBackColor = true;

            this.RadPanel1.ResumeLayout(false);

            // ----------------------------------
            // TXB_Sender_Name + RadButtonElement1 (Texto y botón de búsqueda)
            // ----------------------------------
            this.TXB_Sender_Name = new TextBox();
            this.RadButtonElement1 = new Button();

            // 
            // TXB_Sender_Name
            // 
            this.TXB_Sender_Name.Location = new Point(7, 9);
            this.TXB_Sender_Name.Name = "TXB_Sender_Name";
            this.TXB_Sender_Name.Size = new Size(300, 20);
            this.TXB_Sender_Name.TabIndex = 0;
            this.TXB_Sender_Name.Text = "";
            this.TXB_Sender_Name.KeyUp += new KeyEventHandler(this.TXB_Sender_Name_KeyUp);
            // 
            // RadButtonElement1
            // 
            this.RadButtonElement1.Location = new Point(313, 8);
            this.RadButtonElement1.Name = "RadButtonElement1";
            this.RadButtonElement1.Size = new Size(24, 23);
            this.RadButtonElement1.TabIndex = 1;
            this.RadButtonElement1.Text = "..."; // icono de lupa, si lo desea puede asignar una imagen aquí
            this.RadButtonElement1.UseVisualStyleBackColor = true;
            this.RadButtonElement1.Click += new EventHandler(this.RadButtonElement1_Click);

            // ----------------------------------
            // SPE_Recordtime (NumericUpDown para minutos)
            // ----------------------------------
            this.SPE_Recordtime = new NumericUpDown();
            // 
            // SPE_Recordtime
            // 
            this.SPE_Recordtime.Location = new Point(311, 43);
            this.SPE_Recordtime.Maximum = 99999999;
            this.SPE_Recordtime.Name = "SPE_Recordtime";
            this.SPE_Recordtime.Size = new Size(55, 20);
            this.SPE_Recordtime.TabIndex = 2;

            // ----------------------------------
            // RadGroupBox1 (Automatischer Aufnahmestop)
            // ----------------------------------
            this.RadGroupBox1 = new GroupBox();
            this.RadLabel3 = new Label();
            this.RBT_Aufnahmestop_Size = new RadioButton();
            this.SPE_FileSize = new NumericUpDown();
            this.RadLabel2 = new Label();
            this.RBT_Aufnahmestop_Time = new RadioButton();
            this.RBT_Aufnahmestop_NoStop = new RadioButton();

            // 
            // RadGroupBox1
            // 
            this.RadGroupBox1.Controls.Add(this.RadLabel3);
            this.RadGroupBox1.Controls.Add(this.RBT_Aufnahmestop_Size);
            this.RadGroupBox1.Controls.Add(this.SPE_FileSize);
            this.RadGroupBox1.Controls.Add(this.RadLabel2);
            this.RadGroupBox1.Controls.Add(this.RBT_Aufnahmestop_Time);
            this.RadGroupBox1.Controls.Add(this.RBT_Aufnahmestop_NoStop);
            this.RadGroupBox1.Location = new Point(3, 9);
            this.RadGroupBox1.Name = "RadGroupBox1";
            this.RadGroupBox1.Size = new Size(486, 96);
            this.RadGroupBox1.TabIndex = 0;
            this.RadGroupBox1.TabStop = false;
            this.RadGroupBox1.Text = "Automatischer Aufnahmestop";
            // 
            // RadLabel3
            // 
            this.RadLabel3.Location = new Point(372, 69);
            this.RadLabel3.Name = "RadLabel3";
            this.RadLabel3.Size = new Size(23, 18);
            this.RadLabel3.TabIndex = 6;
            this.RadLabel3.Text = "MB";
            // 
            // RBT_Aufnahmestop_Size
            // 
            this.RBT_Aufnahmestop_Size.Location = new Point(5, 69);
            this.RBT_Aufnahmestop_Size.Name = "RBT_Aufnahmestop_Size";
            this.RBT_Aufnahmestop_Size.Size = new Size(138, 18);
            this.RBT_Aufnahmestop_Size.TabIndex = 4;
            this.RBT_Aufnahmestop_Size.Text = "stoppen bei Dateigröße";
            this.RBT_Aufnahmestop_Size.UseVisualStyleBackColor = true;
            // 
            // SPE_FileSize
            // 
            this.SPE_FileSize.Location = new Point(311, 67);
            this.SPE_FileSize.Maximum = new Decimal(new int[] { 999999999, 0, 0, 0 });
            this.SPE_FileSize.Name = "SPE_FileSize";
            this.SPE_FileSize.Size = new Size(55, 20);
            this.SPE_FileSize.TabIndex = 5;
            // 
            // RadLabel2
            // 
            this.RadLabel2.Location = new Point(372, 45);
            this.RadLabel2.Name = "RadLabel2";
            this.RadLabel2.Size = new Size(48, 18);
            this.RadLabel2.TabIndex = 3;
            this.RadLabel2.Text = "Minuten";
            // 
            // RBT_Aufnahmestop_Time
            // 
            this.RBT_Aufnahmestop_Time.Location = new Point(5, 45);
            this.RBT_Aufnahmestop_Time.Name = "RBT_Aufnahmestop_Time";
            this.RBT_Aufnahmestop_Time.Size = new Size(152, 18);
            this.RBT_Aufnahmestop_Time.TabIndex = 1;
            this.RBT_Aufnahmestop_Time.Text = "stoppen bei Aufnahmezeit";
            this.RBT_Aufnahmestop_Time.UseVisualStyleBackColor = true;
            // 
            // RBT_Aufnahmestop_NoStop
            // 
            this.RBT_Aufnahmestop_NoStop.Location = new Point(5, 21);
            this.RBT_Aufnahmestop_NoStop.Name = "RBT_Aufnahmestop_NoStop";
            this.RBT_Aufnahmestop_NoStop.Size = new Size(371, 18);
            this.RBT_Aufnahmestop_NoStop.TabIndex = 0;
            this.RBT_Aufnahmestop_NoStop.Text = "nicht stoppen (wird beendet wenn kein Stream mehr empfangen wird)";
            this.RBT_Aufnahmestop_NoStop.UseVisualStyleBackColor = true;

            // ----------------------------------
            // RadGroupBox2 (Videoaufnahme)
            // ----------------------------------
            this.RadGroupBox2 = new GroupBox();
            this.DDL_Speicherformat = new ComboBox();
            this.RadLabel9 = new Label();
            this.BTN_Folder = new Button();
            this.DDL_Video_Encoder = new ComboBox();
            this.TXB_Folder = new TextBox();
            this.RadLabel7 = new Label();
            this.RadLabel1 = new Label();

            // 
            // RadGroupBox2
            // 
            this.RadGroupBox2.Controls.Add(this.DDL_Speicherformat);
            this.RadGroupBox2.Controls.Add(this.RadLabel9);
            this.RadGroupBox2.Controls.Add(this.BTN_Folder);
            this.RadGroupBox2.Controls.Add(this.DDL_Video_Encoder);
            this.RadGroupBox2.Controls.Add(this.TXB_Folder);
            this.RadGroupBox2.Controls.Add(this.RadLabel7);
            this.RadGroupBox2.Controls.Add(this.RadLabel1);
            this.RadGroupBox2.Location = new Point(3, 118);
            this.RadGroupBox2.Name = "RadGroupBox2";
            this.RadGroupBox2.Size = new Size(486, 105);
            this.RadGroupBox2.TabIndex = 1;
            this.RadGroupBox2.TabStop = false;
            this.RadGroupBox2.Text = "Videoaufnahme";
            // 
            // DDL_Speicherformat
            // 
            this.DDL_Speicherformat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DDL_Speicherformat.Location = new Point(145, 74);
            this.DDL_Speicherformat.Name = "DDL_Speicherformat";
            this.DDL_Speicherformat.Size = new Size(333, 21);
            this.DDL_Speicherformat.TabIndex = 3;
            // 
            // RadLabel9
            // 
            this.RadLabel9.Location = new Point(5, 76);
            this.RadLabel9.Name = "RadLabel9";
            this.RadLabel9.Size = new Size(83, 18);
            this.RadLabel9.TabIndex = 18;
            this.RadLabel9.Text = "Speicherformat";
            // 
            // BTN_Folder
            // 
            this.BTN_Folder.Location = new Point(453, 46);
            this.BTN_Folder.Name = "BTN_Folder";
            this.BTN_Folder.Size = new Size(26, 24);
            this.BTN_Folder.TabIndex = 2;
            this.BTN_Folder.Text = "...";
            this.BTN_Folder.UseVisualStyleBackColor = true;
            this.BTN_Folder.Click += new EventHandler(this.BTN_Folder_Click);
            // 
            // DDL_Video_Encoder
            // 
            this.DDL_Video_Encoder.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DDL_Video_Encoder.Location = new Point(145, 19);
            this.DDL_Video_Encoder.Name = "DDL_Video_Encoder";
            this.DDL_Video_Encoder.Size = new Size(333, 21);
            this.DDL_Video_Encoder.TabIndex = 0;
            // 
            // TXB_Folder
            // 
            this.TXB_Folder.Location = new Point(145, 48);
            this.TXB_Folder.Name = "TXB_Folder";
            this.TXB_Folder.Size = new Size(304, 20);
            this.TXB_Folder.TabIndex = 1;
            // 
            // RadLabel7
            // 
            this.RadLabel7.Location = new Point(5, 49);
            this.RadLabel7.Name = "RadLabel7";
            this.RadLabel7.Size = new Size(42, 18);
            this.RadLabel7.TabIndex = 16;
            this.RadLabel7.Text = "Ordner";
            // 
            // RadLabel1
            // 
            this.RadLabel1.Location = new Point(5, 21);
            this.RadLabel1.Name = "RadLabel1";
            this.RadLabel1.Size = new Size(79, 18);
            this.RadLabel1.TabIndex = 13;
            this.RadLabel1.Text = "Video Encoder";

            // ----------------------------------
            // RBT_Video_* (Opciones de resolución)
            // ----------------------------------
            this.RBT_Video_FullHD = new RadioButton();
            this.RBT_Video_HD = new RadioButton();
            this.RBT_Video_SD = new RadioButton();
            this.RBT_Video_Minimal = new RadioButton();
            this.RBT_Video_Send = new RadioButton();

            // 
            // RBT_Video_FullHD
            // 
            this.RBT_Video_FullHD.Location = new Point(253, 69);
            this.RBT_Video_FullHD.Name = "RBT_Video_FullHD";
            this.RBT_Video_FullHD.Size = new Size(114, 18);
            this.RBT_Video_FullHD.TabIndex = 4;
            this.RBT_Video_FullHD.Text = "Full HD 1920x1080";
            this.RBT_Video_FullHD.UseVisualStyleBackColor = true;
            // 
            // RBT_Video_HD
            // 
            this.RBT_Video_HD.Location = new Point(253, 45);
            this.RBT_Video_HD.Name = "RBT_Video_HD";
            this.RBT_Video_HD.Size = new Size(87, 18);
            this.RBT_Video_HD.TabIndex = 2;
            this.RBT_Video_HD.Text = "HD 1280x720";
            this.RBT_Video_HD.UseVisualStyleBackColor = true;
            // 
            // RBT_Video_SD
            // 
            this.RBT_Video_SD.Location = new Point(7, 69);
            this.RBT_Video_SD.Name = "RBT_Video_SD";
            this.RBT_Video_SD.Size = new Size(79, 18);
            this.RBT_Video_SD.TabIndex = 3;
            this.RBT_Video_SD.Text = "SD 960x540";
            this.RBT_Video_SD.UseVisualStyleBackColor = true;
            // 
            // RBT_Video_Minimal
            // 
            this.RBT_Video_Minimal.Location = new Point(7, 45);
            this.RBT_Video_Minimal.Name = "RBT_Video_Minimal";
            this.RBT_Video_Minimal.Size = new Size(105, 18);
            this.RBT_Video_Minimal.TabIndex = 1;
            this.RBT_Video_Minimal.Text = "Minimal 480x270";
            this.RBT_Video_Minimal.UseVisualStyleBackColor = true;
            // 
            // RBT_Video_Send
            // 
            this.RBT_Video_Send.Location = new Point(7, 21);
            this.RBT_Video_Send.Name = "RBT_Video_Send";
            this.RBT_Video_Send.Size = new Size(150, 18);
            this.RBT_Video_Send.TabIndex = 0;
            this.RBT_Video_Send.Text = "wie gesendet (empfohlen)";
            this.RBT_Video_Send.UseVisualStyleBackColor = true;

            // ----------------------------------
            // CBX_Benachrichtigung
            // ----------------------------------
            this.CBX_Benachrichtigung = new CheckBox();
            // 
            // CBX_Benachrichtigung
            // 
            this.CBX_Benachrichtigung.Location = new Point(10, 21);
            this.CBX_Benachrichtigung.Name = "CBX_Benachrichtigung";
            this.CBX_Benachrichtigung.Size = new Size(166, 18);
            this.CBX_Benachrichtigung.TabIndex = 0;
            this.CBX_Benachrichtigung.Text = "Benachrichtigen wenn Online";
            this.CBX_Benachrichtigung.UseVisualStyleBackColor = true;

            // ----------------------------------
            // PVP_Einstellungen (TabControl con dos pestañas)
            // ----------------------------------
            this.PVP_Einstellungen = new TabControl();
            this.PVP_Info = new TabPage();
            this.PVP_Aufnahme = new TabPage();
            this.PVP_Einstellungen.SuspendLayout();
            this.PVP_Info.SuspendLayout();
            this.PVP_Aufnahme.SuspendLayout();

            // 
            // PVP_Einstellungen
            // 
            this.PVP_Einstellungen.Controls.Add(this.PVP_Info);
            this.PVP_Einstellungen.Controls.Add(this.PVP_Aufnahme);
            this.PVP_Einstellungen.Location = new Point(7, 43);
            this.PVP_Einstellungen.Name = "PVP_Einstellungen";
            this.PVP_Einstellungen.SelectedIndex = 0;
            this.PVP_Einstellungen.Size = new Size(513, 393);
            this.PVP_Einstellungen.TabIndex = 2;
            // 
            // PVP_Info
            // 
            this.PVP_Info.Controls.Add(this.RadGroupBox6);
            this.PVP_Info.Controls.Add(this.RadGroupBox5);
            this.PVP_Info.Controls.Add(this.RadGroupBox3);
            this.PVP_Info.Location = new Point(4, 22);
            this.PVP_Info.Name = "PVP_Info";
            this.PVP_Info.Padding = new Padding(3);
            this.PVP_Info.Size = new Size(505, 367);
            this.PVP_Info.TabIndex = 0;
            this.PVP_Info.Text = "Kanal Info";
            this.PVP_Info.UseVisualStyleBackColor = true;
            // 
            // PVP_Aufnahme
            // 
            this.PVP_Aufnahme.Controls.Add(this.RadGroupBox4);
            this.PVP_Aufnahme.Controls.Add(this.RadGroupBox1);
            this.PVP_Aufnahme.Controls.Add(this.RadGroupBox2);
            this.PVP_Aufnahme.Location = new Point(4, 22);
            this.PVP_Aufnahme.Name = "PVP_Aufnahme";
            this.PVP_Aufnahme.Padding = new Padding(3);
            this.PVP_Aufnahme.Size = new Size(505, 367);
            this.PVP_Aufnahme.TabIndex = 1;
            this.PVP_Aufnahme.Text = "Aufnahme";
            this.PVP_Aufnahme.UseVisualStyleBackColor = true;

            // ----------------------------------
            // RadGroupBox6 (Info básica de canal)
            // ----------------------------------
            this.RadGroupBox6 = new GroupBox();
            this.RadLabel8 = new Label();
            this.TXB_Land = new TextBox();
            this.RadLabel4 = new Label();
            this.TXB_Name = new TextBox();
            this.TXB_Sprachen = new TextBox();
            this.LAB_Image = new PictureBox();
            this.RadLabel5 = new Label();
            this.RadLabel6 = new Label();
            this.DTP_Geburtstag = new DateTimePicker();

            // 
            // RadGroupBox6
            // 
            this.RadGroupBox6.Controls.Add(this.RadLabel8);
            this.RadGroupBox6.Controls.Add(this.TXB_Land);
            this.RadGroupBox6.Controls.Add(this.RadLabel4);
            this.RadGroupBox6.Controls.Add(this.TXB_Name);
            this.RadGroupBox6.Controls.Add(this.TXB_Sprachen);
            this.RadGroupBox6.Controls.Add(this.LAB_Image);
            this.RadGroupBox6.Controls.Add(this.RadLabel5);
            this.RadGroupBox6.Controls.Add(this.RadLabel6);
            this.RadGroupBox6.Controls.Add(this.DTP_Geburtstag);
            this.RadGroupBox6.Location = new Point(3, 2);
            this.RadGroupBox6.Name = "RadGroupBox6";
            this.RadGroupBox6.Size = new Size(486, 142);
            this.RadGroupBox6.TabIndex = 0;
            this.RadGroupBox6.TabStop = false;
            this.RadGroupBox6.Text = "Info";
            // 
            // RadLabel8
            // 
            this.RadLabel8.Location = new Point(5, 21);
            this.RadLabel8.Name = "RadLabel8";
            this.RadLabel8.Size = new Size(36, 18);
            this.RadLabel8.TabIndex = 16;
            this.RadLabel8.Text = "Name";
            // 
            // TXB_Land
            // 
            this.TXB_Land.Location = new Point(94, 47);
            this.TXB_Land.Name = "TXB_Land";
            this.TXB_Land.Size = new Size(216, 20);
            this.TXB_Land.TabIndex = 2;
            // 
            // RadLabel4
            // 
            this.RadLabel4.Location = new Point(5, 49);
            this.RadLabel4.Name = "RadLabel4";
            this.RadLabel4.Size = new Size(30, 18);
            this.RadLabel4.TabIndex = 8;
            this.RadLabel4.Text = "Land";
            // 
            // TXB_Name
            // 
            this.TXB_Name.Location = new Point(94, 19);
            this.TXB_Name.Name = "TXB_Name";
            this.TXB_Name.Size = new Size(216, 20);
            this.TXB_Name.TabIndex = 1;
            // 
            // TXB_Sprachen
            // 
            this.TXB_Sprachen.Location = new Point(94, 75);
            this.TXB_Sprachen.Name = "TXB_Sprachen";
            this.TXB_Sprachen.Size = new Size(216, 20);
            this.TXB_Sprachen.TabIndex = 3;
            // 
            // LAB_Image
            // 
            this.LAB_Image.Location = new Point(316, 16);
            this.LAB_Image.Name = "LAB_Image";
            this.LAB_Image.Size = new Size(163, 120);
            this.LAB_Image.TabIndex = 0;
            this.LAB_Image.TabStop = false;
            this.LAB_Image.SizeMode = PictureBoxSizeMode.Zoom;
            this.LAB_Image.BorderStyle = BorderStyle.FixedSingle;
            // 
            // RadLabel5
            // 
            this.RadLabel5.Location = new Point(5, 77);
            this.RadLabel5.Name = "RadLabel5";
            this.RadLabel5.Size = new Size(53, 18);
            this.RadLabel5.TabIndex = 10;
            this.RadLabel5.Text = "Sprachen";
            // 
            // RadLabel6
            // 
            this.RadLabel6.Location = new Point(5, 103);
            this.RadLabel6.Name = "RadLabel6";
            this.RadLabel6.Size = new Size(62, 18);
            this.RadLabel6.TabIndex = 12;
            this.RadLabel6.Text = "Geburtstag";
            // 
            // DTP_Geburtstag
            // 
            this.DTP_Geburtstag.Location = new Point(94, 103);
            this.DTP_Geburtstag.Name = "DTP_Geburtstag";
            this.DTP_Geburtstag.Size = new Size(216, 20);
            this.DTP_Geburtstag.TabIndex = 4;
            this.DTP_Geburtstag.Value = DateTime.Now;

            // ----------------------------------
            // RadGroupBox5 (Opciones)
            // ----------------------------------
            this.RadGroupBox5 = new GroupBox();
            this.CBX_Favoriten = new CheckBox();
            this.CBX_Record = new CheckBox();
            this.CBX_Visible = new CheckBox();

            // 
            // RadGroupBox5
            // 
            this.RadGroupBox5.Controls.Add(this.CBX_Favoriten);
            this.RadGroupBox5.Controls.Add(this.CBX_Record);
            this.RadGroupBox5.Controls.Add(this.CBX_Visible);
            this.RadGroupBox5.Controls.Add(this.CBX_Benachrichtigung);
            this.RadGroupBox5.Location = new Point(3, 250);
            this.RadGroupBox5.Name = "RadGroupBox5";
            this.RadGroupBox5.Size = new Size(486, 75);
            this.RadGroupBox5.TabIndex = 2;
            this.RadGroupBox5.TabStop = false;
            this.RadGroupBox5.Text = "Optionen";
            // 
            // CBX_Favoriten
            // 
            this.CBX_Favoriten.Location = new Point(271, 45);
            this.CBX_Favoriten.Name = "CBX_Favoriten";
            this.CBX_Favoriten.Size = new Size(66, 18);
            this.CBX_Favoriten.TabIndex = 3;
            this.CBX_Favoriten.Text = "Favoriten";
            this.CBX_Favoriten.UseVisualStyleBackColor = true;
            // 
            // CBX_Record
            // 
            this.CBX_Record.Location = new Point(271, 21);
            this.CBX_Record.Name = "CBX_Record";
            this.CBX_Record.Size = new Size(143, 18);
            this.CBX_Record.TabIndex = 1;
            this.CBX_Record.Text = "Automatische Aufnahme";
            this.CBX_Record.UseVisualStyleBackColor = true;
            // 
            // CBX_Visible
            // 
            this.CBX_Visible.Location = new Point(10, 45);
            this.CBX_Visible.Name = "CBX_Visible";
            this.CBX_Visible.Size = new Size(103, 18);
            this.CBX_Visible.TabIndex = 2;
            this.CBX_Visible.Text = "Stream anzeigen";
            this.CBX_Visible.UseVisualStyleBackColor = true;

            // ----------------------------------
            // RadGroupBox3 (Sexo)
            // ----------------------------------
            this.RadGroupBox3 = new GroupBox();
            this.RBT_Geschlecht_Trans = new RadioButton();
            this.RBT_Geschlecht_Sonstiges = new RadioButton();
            this.RBT_Geschlecht_Paar = new RadioButton();
            this.RBT_Geschlecht_Männlich = new RadioButton();
            this.RBT_Geschlecht_Weiblich = new RadioButton();

            // 
            // RadGroupBox3
            // 
            this.RadGroupBox3.Controls.Add(this.RBT_Geschlecht_Trans);
            this.RadGroupBox3.Controls.Add(this.RBT_Geschlecht_Sonstiges);
            this.RadGroupBox3.Controls.Add(this.RBT_Geschlecht_Paar);
            this.RadGroupBox3.Controls.Add(this.RBT_Geschlecht_Männlich);
            this.RadGroupBox3.Controls.Add(this.RBT_Geschlecht_Weiblich);
            this.RadGroupBox3.Location = new Point(3, 153);
            this.RadGroupBox3.Name = "RadGroupBox3";
            this.RadGroupBox3.Size = new Size(486, 86);
            this.RadGroupBox3.TabIndex = 1;
            this.RadGroupBox3.TabStop = false;
            this.RadGroupBox3.Text = "Geschlecht";
            // 
            // RBT_Geschlecht_Trans
            // 
            this.RBT_Geschlecht_Trans.Location = new Point(10, 55);
            this.RBT_Geschlecht_Trans.Name = "RBT_Geschlecht_Trans";
            this.RBT_Geschlecht_Trans.Size = new Size(63, 18);
            this.RBT_Geschlecht_Trans.TabIndex = 3;
            this.RBT_Geschlecht_Trans.Tag = "3";
            this.RBT_Geschlecht_Trans.Text = "Trans";
            this.RBT_Geschlecht_Trans.UseVisualStyleBackColor = true;
            // 
            // RBT_Geschlecht_Sonstiges
            // 
            this.RBT_Geschlecht_Sonstiges.Location = new Point(174, 55);
            this.RBT_Geschlecht_Sonstiges.Name = "RBT_Geschlecht_Sonstiges";
            this.RBT_Geschlecht_Sonstiges.Size = new Size(84, 18);
            this.RBT_Geschlecht_Sonstiges.TabIndex = 4;
            this.RBT_Geschlecht_Sonstiges.Tag = "4";
            this.RBT_Geschlecht_Sonstiges.Text = "Sonstiges";
            this.RBT_Geschlecht_Sonstiges.UseVisualStyleBackColor = true;
            // 
            // RBT_Geschlecht_Paar
            // 
            this.RBT_Geschlecht_Paar.Location = new Point(317, 27);
            this.RBT_Geschlecht_Paar.Name = "RBT_Geschlecht_Paar";
            this.RBT_Geschlecht_Paar.Size = new Size(58, 18);
            this.RBT_Geschlecht_Paar.TabIndex = 2;
            this.RBT_Geschlecht_Paar.Tag = "2";
            this.RBT_Geschlecht_Paar.Text = "Paar";
            this.RBT_Geschlecht_Paar.UseVisualStyleBackColor = true;
            // 
            // RBT_Geschlecht_Männlich
            // 
            this.RBT_Geschlecht_Männlich.Location = new Point(174, 24);
            this.RBT_Geschlecht_Männlich.Name = "RBT_Geschlecht_Männlich";
            this.RBT_Geschlecht_Männlich.Size = new Size(82, 18);
            this.RBT_Geschlecht_Männlich.TabIndex = 1;
            this.RBT_Geschlecht_Männlich.Tag = "1";
            this.RBT_Geschlecht_Männlich.Text = "Männlich";
            this.RBT_Geschlecht_Männlich.UseVisualStyleBackColor = true;
            // 
            // RBT_Geschlecht_Weiblich
            // 
            this.RBT_Geschlecht_Weiblich.Location = new Point(10, 24);
            this.RBT_Geschlecht_Weiblich.Name = "RBT_Geschlecht_Weiblich";
            this.RBT_Geschlecht_Weiblich.Size = new Size(79, 18);
            this.RBT_Geschlecht_Weiblich.TabIndex = 0;
            this.RBT_Geschlecht_Weiblich.Tag = "0";
            this.RBT_Geschlecht_Weiblich.Text = "Weiblich";
            this.RBT_Geschlecht_Weiblich.UseVisualStyleBackColor = true;

            // ----------------------------------
            // RadGroupBox4 (Opciones de resolución, en la pestaña Aufnahme)
            // ----------------------------------
            this.RadGroupBox4 = new GroupBox();
            // (Se reutilizan los mismos RadioButtons RBT_Video_Send, RBT_Video_Minimal, etc.)

            // 
            // RadGroupBox4
            // 
            this.RadGroupBox4.Controls.Add(this.RBT_Video_Send);
            this.RadGroupBox4.Controls.Add(this.RBT_Video_FullHD);
            this.RadGroupBox4.Controls.Add(this.RBT_Video_SD);
            this.RadGroupBox4.Controls.Add(this.RBT_Video_Minimal);
            this.RadGroupBox4.Controls.Add(this.RBT_Video_HD);
            this.RadGroupBox4.Location = new Point(3, 239);
            this.RadGroupBox4.Name = "RadGroupBox4";
            this.RadGroupBox4.Size = new Size(486, 94);
            this.RadGroupBox4.TabIndex = 2;
            this.RadGroupBox4.TabStop = false;
            this.RadGroupBox4.Text = "Auflösung";

            // ----------------------------------
            // LAB_Kanal_Gefunden (ícono de check para canal encontrado)
            // ----------------------------------
            this.LAB_Kanal_Gefunden = new Label();
            // 
            // LAB_Kanal_Gefunden
            // 
            this.LAB_Kanal_Gefunden.AutoSize = false;
            this.LAB_Kanal_Gefunden.Location = new Point(497, 9);
            this.LAB_Kanal_Gefunden.Name = "LAB_Kanal_Gefunden";
            this.LAB_Kanal_Gefunden.Size = new Size(24, 24);
            this.LAB_Kanal_Gefunden.TabIndex = 2;
            this.LAB_Kanal_Gefunden.Visible = false;
            // Si desea mostrar un ícono, use:
            // this.LAB_Kanal_Gefunden.Image = XstreaMon.My.Resources.Resources.accept;
            // this.LAB_Kanal_Gefunden.ImageAlign = ContentAlignment.MiddleCenter;

            // ----------------------------------
            // DDL_Webseite (ComboBox para elegir sitio web)
            // ----------------------------------
            this.DDL_Webseite = new ComboBox();
            // 
            // DDL_Webseite
            // 
            this.DDL_Webseite.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DDL_Webseite.Location = new Point(336, 9);
            this.DDL_Webseite.Name = "DDL_Webseite";
            this.DDL_Webseite.Size = new Size(159, 21);
            this.DDL_Webseite.TabIndex = 1;

            // ----------------------------------
            // RadProgressBar1 (ProgressBar para búsqueda)
            // ----------------------------------
            this.RadProgressBar1 = new ProgressBar();
            // 
            // RadProgressBar1
            // 
            this.RadProgressBar1.Location = new Point(8, 34);
            this.RadProgressBar1.Maximum = 14; // Se actualiza dinámicamente en código
            this.RadProgressBar1.Name = "RadProgressBar1";
            this.RadProgressBar1.Size = new Size(509, 6);
            this.RadProgressBar1.Step = 1;
            this.RadProgressBar1.TabIndex = 14;
            this.RadProgressBar1.Visible = false;

            // ----------------------------------
            // Propiedades del Form
            // ----------------------------------
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(529, 486);
            this.Controls.Add(this.RadProgressBar1);
            this.Controls.Add(this.TXB_Sender_Name);
            this.Controls.Add(this.RadButtonElement1);
            this.Controls.Add(this.LAB_Kanal_Gefunden);
            this.Controls.Add(this.DDL_Webseite);
            this.Controls.Add(this.PVP_Einstellungen);
            this.Controls.Add(this.RadPanel1);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_Model_Einstellungen";
            this.Text = "Kanal Einstellungen";  // Texto fijo; se actualizará en Load si corresponde
            this.StartPosition = FormStartPosition.CenterScreen;

            this.PVP_Einstellungen.ResumeLayout(false);
            this.PVP_Info.ResumeLayout(false);
            this.PVP_Aufnahme.ResumeLayout(false);

            this.RadPanel1.ResumeLayout(false);
        }

        #endregion
    }
}
