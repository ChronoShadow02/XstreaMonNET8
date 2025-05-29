using System.ComponentModel;
using System.Diagnostics;

namespace XstreaMonNET8
{
    public class Control_Image_Preview : UserControl
    {
        private IContainer components;
        private List<Video_File> Pri_Video_list;
        internal string Model_Name;
        internal Guid Model_GUID;
        private Bitmap Video_Thumb;
        private Bitmap Pri_Vorschau_Image;
        private string Pri_Bezeichnung;
        private string Stream_File;

        internal Panel PAN_Info { get; set; }
        internal Label LAB_Größe { get; set; }
        internal Button BTN_Delete { get; set; }
        internal Button BTN_Galerie { get; set; }
        internal Button BTN_Play { get; set; }
        internal ToolTip TTP_Control { get; set; }

        internal ContextMenuStrip CMI_Image { get; set; }
        internal ToolStripMenuItem CMI_Öffnen { get; set; }
        internal ToolStripMenuItem CMI_Löschen { get; set; }
        internal ToolStripSeparator RadMenuSeparatorItem1 { get; set; }
        internal ToolStripMenuItem CMI_Directory_Open { get; set; }
        internal ToolStripSeparator RadMenuSeparatorItem2 { get; set; }
        internal ToolStripMenuItem CMI_Property { get; set; }

        internal Guid Pro_Video_GUID { get; set; }

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
            set => Pri_Bezeichnung = value.ToString();
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
                    Bitmap original = new Bitmap(Stream_File);
                    Vorschau_Image = new Bitmap(original,
                        new Size((int)((double)Height / original.Height * original.Width - 4),
                                 Height - 4));
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Control_Video_Preview.Pro_Stream_File");
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
            Disposed += Control_Video_Preview_Disposed;
            Load += Control_Video_Preview_Load;
            Paint += Control_Video_Preview_Paint;
            DoubleClick += Control_Video_Preview_DoubleClick;

            Pri_Video_list = new List<Video_File>();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            components = new Container();
            CMI_Image = new ContextMenuStrip();
            CMI_Öffnen = new ToolStripMenuItem("Öffnen", null, CMI_Öffnen_Click);
            CMI_Löschen = new ToolStripMenuItem("Löschen", null, CMI_Löschen_Click);
            RadMenuSeparatorItem1 = new ToolStripSeparator();
            CMI_Directory_Open = new ToolStripMenuItem("Speicherort öffnen", null, CMI_Directory_Open_Click);
            RadMenuSeparatorItem2 = new ToolStripSeparator();
            CMI_Property = new ToolStripMenuItem("Eigenschaften", null, CMI_Property_Click);
            CMI_Image.Items.AddRange(new ToolStripItem[]
            {
                CMI_Öffnen,
                CMI_Löschen,
                RadMenuSeparatorItem1,
                CMI_Directory_Open,
                RadMenuSeparatorItem2,
                CMI_Property
            });

            PAN_Info = new Panel
            {
                Dock = DockStyle.Right,
                Size = new Size(49, 250),
                BackColor = Color.FromArgb(100, 255, 255, 255),
                ContextMenuStrip = CMI_Image
            };

            BTN_Play = new Button
            {
                Dock = DockStyle.Top,
                Text = "▶",
                Height = 34
            };
            BTN_Play.Click += BTN_Play_Click;

            BTN_Galerie = new Button
            {
                Dock = DockStyle.Top,
                Text = "🖼",
                Height = 34
            };
            BTN_Galerie.Click += BTN_Galerie_Click;

            LAB_Größe = new Label
            {
                Dock = DockStyle.Top,
                Height = 14,
                TextAlign = ContentAlignment.MiddleRight
            };

            BTN_Delete = new Button
            {
                Dock = DockStyle.Bottom,
                Text = "🗑",
                Height = 34
            };
            BTN_Delete.Click += BTN_Delete_Click;

            TTP_Control = new ToolTip(components);
            TTP_Control.SetToolTip(BTN_Delete, "Video löschen");
            TTP_Control.SetToolTip(BTN_Play, "Video öffnen");
            TTP_Control.SetToolTip(BTN_Galerie, "Galerie öffnen");

            PAN_Info.Controls.Add(BTN_Delete);
            PAN_Info.Controls.Add(LAB_Größe);
            PAN_Info.Controls.Add(BTN_Galerie);
            PAN_Info.Controls.Add(BTN_Play);

            Controls.Add(PAN_Info);
            BorderStyle = BorderStyle.FixedSingle;
            DoubleBuffered = true;
            Size = new Size(404, 250);
        }

        private void Control_Video_Preview_Disposed(object sender, EventArgs e)
        {
            Video_Thumb = null;
            Vorschau_Image = null;
            Control_Clear_Dispose.Clear(Controls);
            Dispose(true);
        }

        private void Control_Video_Preview_Load(object sender, EventArgs e) { }


        private void Control_Video_Preview_DoubleClick(object sender, EventArgs e)
        {
            Video_Play(0);
        }

        private void Video_Play(int TimeStamp)
        {
            try
            {
                if (!File.Exists(Pro_Stream_File))
                {
                    MessageBox.Show(TXT.TXT_Description("Die Datei kann nicht geladen werden"));
                    return;
                }

                if (IniFile.Read(Parameter.INI_Common, "Player", "Intern", "true") == "true")
                {
                    string args = $" -File:\"{Pro_Stream_File}\" -Time:{TimeStamp} -List:";

                    var playlist = Pro_Video_list.Select(v => $"\"{v.Pro_Pfad}\"|").ToList();
                    string argList = string.Concat(playlist);
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(Application.StartupPath, "CRPlayer.exe"),
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = args + argList
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
                Parameter.Error_Message(ex, "Control_Video_Preview.PAN_Image_DoubleClick");
            }
        }

        private void CMI_Öffnen_Click(object sender, EventArgs e) => Video_Play(0);

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
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Löschen_Click");
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
            if (MessageBox.Show($"Möchten sie die Datei {fi.Name} löschen?", "Datei löschen", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CMI_Löschen_Click(null, null);
            }
        }

        private void BTN_Play_Click(object sender, EventArgs e) => CMI_Öffnen_Click(null, null);

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
            dlg.FormClosing += (s, args) => Pro_Stream_File = Dialog_Video_Propertys.Pri_File_Name;
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
                Parameter.Error_Message(ex, "Class_Stream_Record.CMI_Property_Click");
            }
        }

        private void PAN_Vorschau_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (Vorschau_Image == null)
                    return;

                e.Graphics.DrawImage(Vorschau_Image, 0, 0);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Video_DropDownOpening");
            }
        }

        private void Control_Video_Preview_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (Bezeichnung != null)
                {
                    using (Font boldFont = new Font(Font, FontStyle.Bold))
                    using (Brush brush = new SolidBrush(ForeColor))
                    {
                        e.Graphics.DrawString(Bezeichnung, boldFont, brush, 2f, 2f);
                    }
                }

                if (Vorschau_Image == null)
                    return;

                int x = (int)Math.Round(169.0 - Vorschau_Image.Width / 2.0);
                if (x < 1)
                    x = 1;

                e.Graphics.DrawImage(Vorschau_Image, x, 20);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Video_DropDownOpening");
            }
        }

    }
}
