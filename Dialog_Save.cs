using System.ComponentModel;

namespace XstreaMonNET8
{
    public class Dialog_Save : Form
    {
        private IContainer components;
        private bool running;

        internal Button BTN_Speichern;
        internal TextBox TXB_Dateiname;
        internal ComboBox DDL_Datei_Typ;
        internal TextBox TXB_Verzeichnis;
        internal Button BTN_Verzeichnis;
        internal GroupBox RadGroupBox1;
        internal GroupBox RadGroupBox2;
        internal ProgressBar PB_Download;

        private WebFileDownloader _Downloader;

        internal string Pro_Download_Path { get; set; }
        internal string Pro_Target_Path { get; set; }
        internal string Pro_Target_Name { get; set; }
        internal Class_Model Pro_Class_Model { get; set; }

        public Dialog_Save()
        {
            Load += new EventHandler(Dialog_Save_Load);
            running = false;
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                    components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void InitializeComponent()
        {
            PB_Download = new ProgressBar();
            BTN_Speichern = new Button();
            TXB_Dateiname = new TextBox();
            DDL_Datei_Typ = new ComboBox();
            TXB_Verzeichnis = new TextBox();
            BTN_Verzeichnis = new Button();
            RadGroupBox1 = new GroupBox();
            RadGroupBox2 = new GroupBox();

            // PB_Download
            PB_Download.Location = new Point(6, 134);
            PB_Download.Name = "PB_Download";
            PB_Download.Size = new Size(374, 24);
            PB_Download.Visible = false;

            // BTN_Speichern
            BTN_Speichern.Location = new Point(270, 134);
            BTN_Speichern.Name = "BTN_Speichern";
            BTN_Speichern.Size = new Size(110, 24);
            BTN_Speichern.Text = "Speichern";
            BTN_Speichern.Click += new EventHandler(BTN_Speichern_Click);

            // TXB_Dateiname
            TXB_Dateiname.Location = new Point(6, 21);
            TXB_Dateiname.Name = "TXB_Dateiname";
            TXB_Dateiname.Size = new Size(279, 20);

            // DDL_Datei_Typ
            DDL_Datei_Typ.DropDownStyle = ComboBoxStyle.DropDownList;
            DDL_Datei_Typ.Items.Add("*.mp4");
            DDL_Datei_Typ.Location = new Point(291, 21);
            DDL_Datei_Typ.Name = "DDL_Datei_Typ";
            DDL_Datei_Typ.Size = new Size(78, 20);

            // TXB_Verzeichnis
            TXB_Verzeichnis.Location = new Point(5, 21);
            TXB_Verzeichnis.Name = "TXB_Verzeichnis";
            TXB_Verzeichnis.Size = new Size(328, 23);

            // BTN_Verzeichnis
            BTN_Verzeichnis.Location = new Point(336, 21);
            BTN_Verzeichnis.Name = "BTN_Verzeichnis";
            BTN_Verzeichnis.Size = new Size(32, 23);
            BTN_Verzeichnis.Text = "...";
            BTN_Verzeichnis.Click += new EventHandler(BTN_Verzeichnis_Click);

            // RadGroupBox1
            RadGroupBox1.Text = "Verzeichnis";
            RadGroupBox1.Location = new Point(6, 3);
            RadGroupBox1.Size = new Size(374, 53);
            RadGroupBox1.Controls.Add(TXB_Verzeichnis);
            RadGroupBox1.Controls.Add(BTN_Verzeichnis);

            // RadGroupBox2
            RadGroupBox2.Text = "Dateiname";
            RadGroupBox2.Location = new Point(6, 67);
            RadGroupBox2.Size = new Size(374, 50);
            RadGroupBox2.Controls.Add(TXB_Dateiname);
            RadGroupBox2.Controls.Add(DDL_Datei_Typ);

            // Form settings
            ClientSize = new Size(387, 166);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Datei speichern";
            Controls.Add(PB_Download);
            Controls.Add(BTN_Speichern);
            Controls.Add(RadGroupBox1);
            Controls.Add(RadGroupBox2);
        }

        private void Dialog_Save_Load(object sender, EventArgs e)
        {
            try
            {
                TXB_Verzeichnis.Text = Pro_Target_Path;
                TXB_Dateiname.Text = Modul_Ordner.DateiName(Pro_Target_Path, Pro_Target_Name + ".mp4").Replace(".mp4", "");
                DDL_Datei_Typ.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Save.Dialog_Save_Load");
            }
        }

        private async void BTN_Speichern_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;

            if (string.IsNullOrEmpty(Pro_Download_Path) || string.IsNullOrEmpty(TXB_Verzeichnis.Text) || string.IsNullOrEmpty(TXB_Dateiname.Text))
                return;

            try
            {
                if (!Directory.Exists(TXB_Verzeichnis.Text))
                    Directory.CreateDirectory(TXB_Verzeichnis.Text);

                BTN_Speichern.Enabled = false;
                PB_Download.Visible = true;

                _Downloader = new WebFileDownloader();
                _Downloader.FileDownloadSizeObtained += _Downloader_FileDownloadSizeObtained;
                _Downloader.FileDownloadComplete += _Downloader_FileDownloadComplete;
                _Downloader.FileDownloadFailed += _Downloader_FileDownloadFailed;
                _Downloader.AmountDownloadedChanged += _Downloader_AmountDownloadedChanged;

                _Downloader.DownloadFileWithProgress(Pro_Download_Path, Path.Combine(TXB_Verzeichnis.Text, TXB_Dateiname.Text + ".mp4"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void _Downloader_FileDownloadSizeObtained(long iFileSize)
        {
            try
            {
                PB_Download.Value = 0;
                PB_Download.Maximum = Convert.ToInt32(iFileSize);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Save._Downloader_FileDownloadSizeObtained");
            }
        }

        private void _Downloader_FileDownloadComplete()
        {
            try
            {
                PB_Download.Value = PB_Download.Maximum;
                Dispose();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Save._Downloader_FileDownloadComplete");
            }
        }

        private void _Downloader_FileDownloadFailed(Exception ex)
        {
            MessageBox.Show("An error has occured during download: " + ex.Message);
        }

        private void _Downloader_AmountDownloadedChanged(long iNewProgress)
        {
            try
            {
                PB_Download.Value = Convert.ToInt32(iNewProgress);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Save._Downloader_AmountDownloadedChanged");
            }
        }

        private void BTN_Verzeichnis_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.SelectedPath = TXB_Verzeichnis.Text;
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        TXB_Verzeichnis.Text = fbd.SelectedPath;
                        TXB_Dateiname.Text = Modul_Ordner.DateiName(TXB_Verzeichnis.Text, TXB_Dateiname.Text + ".mp4").Replace(".mp4", "");
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Save.BTN_Verzeichnis_Click");
            }
        }
    }
}
