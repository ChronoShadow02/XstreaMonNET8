using System.Diagnostics;

namespace XstreaMonNET8
{
    public partial class Dialog_Videothek : Form
    {
        private readonly List<Video_File> _File_list;

        public Dialog_Videothek(List<Video_File> File_list)
        {
            this.Load += new EventHandler(this.Dialog_Videothek_Load!);
            this.FormClosed += new FormClosedEventHandler(this.Dialog_Videothek_Closed!);
            this._File_list = new List<Video_File>();
            InitializeComponent();
            try
            {
                this._File_list = File_list;
                this.CMI_Channel.Text = TXT.TXT_Description("Gruppieren nach Name");
                this.CMI_Channel.Checked = ValueBack.Get_CBoolean(
                    IniFile.Read(Parameter.INI_Common, "Galerie", "Channel", "False"));
                this.CMI_Tiles.Text = TXT.TXT_Description("Kachelansicht");
                this.CMI_Tiles.Checked = ValueBack.Get_CBoolean(
                    IniFile.Read(Parameter.INI_Common, "Galerie", "Tiles", "False"));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.New");
            }
        }

        private void Files_Load(List<Video_File> File_List)
        {
            try
            {
                Modul_StatusScreen.Status_Show(TXT.TXT_Description("Galerie wird geladen"));
                this.PAN_Galerie.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                this.PAN_Galerie.Controls.Clear();

                IEnumerable<Video_File> ordered;
                bool groupByName = this.CMI_Channel.Checked;

                if (groupByName)
                {
                    ordered = File_List
                        .OrderBy(v => v.Pro_Model_Name)
                        .Reverse();
                }
                else
                {
                    ordered = File_List
                        .OrderBy(v => v.Pro_Start)
                        .Reverse();
                }

                bool showTiles = this.CMI_Tiles.Checked;
                int index = 1;

                foreach (Video_File videoFile in ordered)
                {
                    try
                    {
                        Modul_StatusScreen.Status_Show(
                            string.Format(
                                TXT.TXT_Description("Datei {0} von {1} wird geladen"),
                                index, File_List.Count));
                        index++;

                        if (File.Exists(videoFile.Pro_Pfad))
                        {
                            FileInfo fileInfo = new FileInfo(videoFile.Pro_Pfad);
                            string ext = fileInfo.Extension.ToLower();
                            if (ext == ".mp4" || ext == ".ts")
                            {
                                CustomFlowLayoutPanel panelContainer = Panel_Find(
                                    groupByName ? videoFile.Pro_Model_Name! : string.Empty);

                                Control_Video_Preview controlVideoPreview = new()
                                {
                                    Dock = DockStyle.Top,
                                    Pro_IsInDB = videoFile.Pro_IsInDB,
                                    Pro_Tiles_Show = showTiles,
                                    Pro_Video_Start = videoFile.Pro_Start,
                                    Pro_Video_Ende = videoFile.Pro_Ende,
                                    Bezeichnung = videoFile.Pro_Bezeichnung,
                                    Model_Name = videoFile.Pro_Model_Name,
                                    Model_GUID = videoFile.Pro_Model_GUID,
                                    Pro_Record_Run = videoFile.Pro_Run_Record,
                                    Pro_FrameRate = videoFile.Pro_FrameRate.ToString(),
                                    Pro_Resoultion = videoFile.Pro_Resolution,
                                    Pro_Video_Länge = videoFile.Pro_Video_Länge,
                                    Vorschau_Visible = true,
                                    Pro_Video_GUID = ValueBack.Get_CUnique(videoFile.Pro_Video_GUID),
                                    Pro_Favorite = ValueBack.Get_CBoolean(videoFile.Pro_Favorite),
                                    Pro_Stream_File = videoFile.Pro_Pfad,
                                    Pro_Video_list = File_List
                                };

                                panelContainer.Controls.Add(controlVideoPreview);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Parameter.Error_Message(ex, "Dialog_Videothek.Files_Load");
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.Files_Load");
            }
            finally
            {
                Modul_StatusScreen.Status_Show(string.Empty);
                this.PAN_Galerie.Visible = true;
                this.PAN_Galerie.AutoScroll = true;
                this.Cursor = Cursors.Default;
                Parameter.FlushMemory();
            }
        }

        private void Dialog_Videothek_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = TXT.TXT_Description("Galerie");
                this.Files_Load(this._File_list);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.Dialog_Videothek_Load");
            }
        }

        private void CMI_Channel_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(
                    Parameter.INI_Common,
                    "Galerie",
                    "Channel",
                    this.CMI_Channel.Checked.ToString());
                this.Files_Load(this._File_list);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.CMI_Channel_Click");
            }
        }

        private void CMI_Tiles_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(
                    Parameter.INI_Common,
                    "Galerie",
                    "Tiles",
                    this.CMI_Tiles.Checked.ToString());
                this.Files_Load(this._File_list);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.CMI_Tiles_Click");
            }
        }

        private void CommandBarButton1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> paths = new List<string>();
                foreach (Video_File videoFile in this._File_list
                             .OrderBy(v => v.Pro_Start)
                             .Reverse())
                {
                    paths.Add(videoFile.Pro_Pfad);
                }

                bool useInternal = ValueBack.Get_CBoolean(
                    IniFile.Read(Parameter.INI_Common, "Player", "Intern", "True"));

                if (!useInternal)
                {
                    if (paths.Count > 1)
                    {
                        using (var sw = new StreamWriter(Path.Combine(Parameter.CommonPath, "Playlist.m3u"), append: true))
                        {
                            foreach (string p in paths)
                            {
                                sw.WriteLine(p);
                            }
                        }

                        Process.Start(new ProcessStartInfo
                        {
                            FileName = Path.Combine(Parameter.CommonPath, "Playlist.m3u"),
                            UseShellExecute = true
                        });

                    }
                    else if (paths.Count == 1)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = paths[0],
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.CommandBarButton1_Click");
            }
        }

        private void Dialog_Videothek_Closed(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                this.Dispose(true);
                Parameter.FlushMemory();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.Dialog_Videothek_Closed");
            }
        }

        private CustomFlowLayoutPanel Panel_Find(string Panel_Name)
        {
            try
            {
                Control foundContainer = null;
                foreach (Control ctrl in this.PAN_Galerie.Controls)
                {
                    if (ctrl.Tag is string tag && tag == Panel_Name)
                    {
                        foundContainer = ctrl.Controls[0];
                        break;
                    }
                }

                if (foundContainer == null)
                {
                    Panel groupPanel = new Panel
                    {
                        Dock = DockStyle.Top,
                        AutoSize = true,
                        Tag = Panel_Name,
                        AutoScroll = false
                    };

                    Label headerLabel = new Label
                    {
                        Dock = DockStyle.Top,
                        Text = Panel_Name,
                        BackColor = Color.DarkGray,
                        AutoSize = false,
                        Visible = !string.IsNullOrEmpty(Panel_Name),
                        Font = new Font(Font, FontStyle.Bold)
                    };

                    CustomFlowLayoutPanel flowPanel = new CustomFlowLayoutPanel
                    {
                        Dock = DockStyle.Top,
                        AutoSizeMode = AutoSizeMode.GrowOnly,
                        AutoSize = true,
                        Tag = Panel_Name
                    };
                    flowPanel.ContextMenuStrip = this.CMI_Galerie;

                    groupPanel.Controls.Add(flowPanel);
                    groupPanel.Controls.Add(headerLabel);

                    this.PAN_Galerie.Controls.Add(groupPanel);
                    foundContainer = flowPanel;
                }

                return (CustomFlowLayoutPanel)foundContainer;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Videothek.Panel_Find");
                return null;
            }
        }
    }
}
