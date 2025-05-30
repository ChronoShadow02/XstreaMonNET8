using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    public partial class Control_Image_Preview : UserControl
    {
        private List<Video_File> Pri_Video_list;
        internal string Model_Name;
        internal Guid Model_GUID;
        private Bitmap Video_Thumb;
        private Bitmap Pri_Vorschau_Image;
        private string Pri_Bezeichnung;
        private string Stream_File;

        /// <summary>GUID de vídeo actual.</summary>
        internal Guid Pro_Video_GUID { get; set; }

        /// <summary>Lista ordenada de vídeos.</summary>
        internal List<Video_File> Pro_Video_list
        {
            get => Pri_Video_list;
            set => Pri_Video_list = value.OrderBy(x => x.Pro_Start).ToList();
        }

        private Bitmap Vorschau_Image
        {
            get => Pri_Vorschau_Image;
            set
            {
                if (value == null) return;
                Pri_Vorschau_Image = value;
                Refresh();
            }
        }

        internal string Bezeichnung
        {
            get => Pri_Bezeichnung;
            set => Pri_Bezeichnung = value;
        }

        internal DateTime Pro_CreateDate => File.Exists(Stream_File)
            ? new FileInfo(Stream_File).CreationTime
            : DateTime.MinValue;

        internal string Pro_Stream_File
        {
            get => Stream_File;
            set
            {
                try
                {
                    Stream_File = value;
                    LAB_Größe.Text = ValueBack.Get_Numeric2Bytes(new FileInfo(Stream_File).Length);
                    if (!File.Exists(Stream_File)) return;
                    using var original = new Bitmap(Stream_File);
                    Vorschau_Image = new Bitmap(
                        original,
                        new Size(
                            (int)((double)Height / original.Height * original.Width - 4),
                            Height - 4
                        )
                    );
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Control_Image_Preview.Pro_Stream_File");
                }
            }
        }

        internal bool Pro_BtnGalerie_Show
        {
            get => BTN_Galerie.Visible;
            set => BTN_Galerie.Visible = value;
        }

        public Control_Image_Preview()
        {
            InitializeComponent();

            Disposed += Control_Video_Preview_Disposed;
            Load += Control_Video_Preview_Load;
            Paint += Control_Video_Preview_Paint;
            DoubleClick += Control_Video_Preview_DoubleClick;

            Pri_Video_list = new List<Video_File>();
        }

        private void Control_Video_Preview_Disposed(object sender, EventArgs e)
        {
            Video_Thumb = null;
            Pri_Vorschau_Image = null;
            Control_Clear_Dispose.Clear(Controls);
            Dispose(true);
        }

        private void Control_Video_Preview_Load(object sender, EventArgs e) { /* opcional */ }

        private void Control_Video_Preview_DoubleClick(object sender, EventArgs e)
            => Video_Play(0);

        private void Video_Play(int TimeStamp)
        {
            try
            {
                if (!File.Exists(Pro_Stream_File))
                {
                    MessageBox.Show(TXT.TXT_Description("Die Datei kann nicht geladen werden"));
                    return;
                }

                bool useInternal = IniFile.Read(Parameter.INI_Common, "Player", "Intern", "true") == "true";
                if (useInternal)
                {
                    string args = $" -File:\"{Pro_Stream_File}\" -Time:{TimeStamp} -List:";
                    var playlist = Pro_Video_list.Select(v => $"\"{v.Pro_Pfad}\"|");
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(Application.StartupPath, "CRPlayer.exe"),
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = args + string.Concat(playlist)
                    });
                }
                else
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Pro_Stream_File,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Image_Preview.Video_Play");
            }
        }

        private void CMI_Öffnen_Click(object sender, EventArgs e)
            => Video_Play(0);

        private void CMI_Löschen_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Pro_Stream_File))
                {
                    Vorschau_Image?.Dispose();
                    File.Delete(Pro_Stream_File);
                }

                if (Parent?.Controls.Count == 1)
                    Parent?.Parent?.Dispose();

                Dispose();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Image_Preview.CMI_Löschen_Click");
            }
        }

        private void BTN_Galerie_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var result = Class_Model_List.Class_Model_Find(Model_GUID).Result;
                result?.Dialog_Model_View_Show();
            });
        }

        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            var fi = new FileInfo(Pro_Stream_File);
            if (MessageBox.Show(
                    $"Möchten sie die Datei {fi.Name} löschen?",
                    "Datei löschen",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CMI_Löschen_Click(null, null);
            }
        }

        private void BTN_Play_Click(object sender, EventArgs e)
            => CMI_Öffnen_Click(null, null);

        private void CMI_Directory_Open_Click(object sender, EventArgs e)
        {
            var result = Class_Model_List.Class_Model_Find(Model_GUID).Result;
            if (Directory.Exists(result?.Pro_Model_Directory))
                Process.Start(result.Pro_Model_Directory);
        }

        private void CMI_Property_Click(object sender, EventArgs e)
        {
            var dlg = new Dialog_Video_Propertys(Stream_File)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            dlg.FormClosing += DLG_Property_Form_Closing;
            dlg.Show();
        }

        private void DLG_Property_Form_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Pro_Stream_File = Dialog_Video_Propertys.Pri_File_Name;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Image_Preview.CMI_Property_Click");
            }
        }

        private void PAN_Vorschau_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (Pri_Vorschau_Image != null)
                    e.Graphics.DrawImage(Pri_Vorschau_Image, 0, 0);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Image_Preview.PAN_Vorschau_Paint");
            }
        }

        private void Control_Video_Preview_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Pri_Bezeichnung))
                {
                    using var boldFont = new Font(Font, FontStyle.Bold);
                    using var brush = new SolidBrush(ForeColor);
                    e.Graphics.DrawString(Pri_Bezeichnung, boldFont, brush, 2f, 2f);
                }

                if (Pri_Vorschau_Image == null) return;

                int x = (int)Math.Round(169.0 - Pri_Vorschau_Image.Width / 2.0);
                if (x < 1) x = 1;
                e.Graphics.DrawImage(Pri_Vorschau_Image, x, 20);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Image_Preview.Control_Video_Preview_Paint");
            }
        }
    }
}
