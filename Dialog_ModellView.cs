using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace XstreaMonNET8
{
    public partial class Dialog_ModellView : Form
    {
        private string Folder_Path;
        private readonly string Pro_Model_Name;
        private DataTable DT_Record;
        private Guid Priv_Model_GUID;
        private Class_Model Priv_Model_Class;
        private readonly System.Windows.Forms.Timer Work_Timer;
        private string Pri_FirstFile;
        private string Pri_SecondFile;
        private string Pri_AusgabeFile;
        private double Pri_Input_Lenght;
        private string Pri_Temp_First;
        private string Pri_Temp_Last;
        private DateTime Pri_Merge_Start;

        public Dialog_ModellView()
        {
            // Este constructor se usa cuando el Diseñador crea el formulario.
            InitializeComponent();

            this.Work_Timer = new System.Windows.Forms.Timer();
            this.Pri_Input_Lenght = 0.0;
            this.Load += new EventHandler(this.Dialog_ModellView_Load);
            this.FormClosed += new FormClosedEventHandler(this.Dialog_ModellView_Closed);
            this.Shown += new EventHandler(this.Dialog_ModellView_Shown);

            // Asociar eventos del ContextMenuStrip
            this.CMI_Datum.Click += new EventHandler(this.CMI_Channel_Click);
            this.CMI_Tiles.Click += new EventHandler(this.CMI_Kachel_Click);
            this.CMI_Sort_Datum.Click += new EventHandler(this.CMI_Sort_Datum_Click);
            this.CMI_Sort_Name.Click += new EventHandler(this.CMI_Sort_Name_Click);
            this.CMI_Sort_ASC.Click += new EventHandler(this.CMI_Sort_ASC_Click);
            this.CMI_Sort_DESC.Click += new EventHandler(this.CMI_Sort_DESC_Click);
            this.CMI_Image.Click += new EventHandler(this.CMI_Image_Click);
            this.CMI_Videos.Click += new EventHandler(this.CMI_Videos_Click);

            // Asociar botones
            this.BTN_Explorer.Click += new EventHandler(this.BTN_Explorer_Click);
            this.BTN_Einstellungen.Click += new EventHandler(this.BTN_Einstellungen_Click);
        }

        /// <summary>
        /// Permite que otra parte de la aplicación asigne el objeto Class_Model antes de mostrar el form.
        /// </summary>
        internal Class_Model Pro_Model_Class
        {
            set => this.Priv_Model_Class = value;
        }

        private void Dialog_ModellView_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Priv_Model_Class != null)
                {
                    Modul_StatusScreen.Status_Show(TXT.TXT_Description("Modelgalerie wird geladen"));
                    this.Priv_Model_GUID = this.Priv_Model_Class.Pro_Model_GUID;
                    this.Priv_Model_Class.Pro_Model_Galery_Last_Visit = DateTime.Now;

                    // Construir historial de los últimos 14 días
                    this.RadPanel1.SuspendLayout();
                    int daysBack = 14;
                    do
                    {
                        Control_Online_Time controlOnlineTime = new Control_Online_Time(
                            DateTime.Today.AddDays(-daysBack),
                            this.Priv_Model_Class.Pro_Model_GUID
                        );
                        controlOnlineTime.Dock = DockStyle.Top;
                        this.RadPanel1.Controls.Add(controlOnlineTime);
                        daysBack--;
                    } while (daysBack >= 0);
                    this.RadPanel1.ResumeLayout(true);

                    // Cargar registros desde la base de datos
                    using (OleDbConnection oleDbConnection = new OleDbConnection())
                    {
                        oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                        oleDbConnection.Open();
                        if (oleDbConnection.State == ConnectionState.Open)
                        {
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                                "SELECT * FROM DT_Record WHERE User_GUID = '" + this.Priv_Model_Class.Pro_Model_GUID.ToString() + "'",
                                oleDbConnection.ConnectionString))
                            {
                                using (OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter))
                                {
                                    DataSet ds = new DataSet();
                                    adapter.Fill(ds, "DT_Record");
                                    this.DT_Record = ds.Tables["DT_Record"]!;
                                }
                            }
                        }
                    }

                    // Mostrar información en las etiquetas
                    this.LAB_Geschlecht.Text = this.Priv_Model_Class.Pro_Model_Gender_Text;
                    this.LAB_Geschlecht.Image = this.Priv_Model_Class.Pro_Model_Gender_Image;
                    this.LAB_Geschlecht.Visible = this.LAB_Geschlecht.Text.Length > 0;

                    this.LAB_Country.Text = this.Priv_Model_Class.Pro_Model_Country;
                    this.LAB_Country.Visible = this.LAB_Country.Text.Length > 0;

                    this.LAB_Languages.Text = this.Priv_Model_Class.Pro_Model_Language;
                    this.LAB_Languages.Visible = this.LAB_Languages.Text.Length > 0;

                    this.LAB_Info.Text = this.Priv_Model_Class.Pro_Model_Info;
                    this.LAB_Info.Visible = this.LAB_Info.Text.Length > 0;

                    this.Folder_Path = this.Priv_Model_Class.Pro_Model_Directory;
                    Application.DoEvents();
                    this.Files_Load();
                }

                this.PAN_Work.Visible = false;
                this.Work_Timer.Tick += new EventHandler((a0, a1) => this.Work_Fortschritt());

                // Inicializar estado de los ítems del menú según INI
                this.CMI_Datum.Text = TXT.TXT_Description("Gruppieren nach Datum");
                this.CMI_Datum.Checked = ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Modelview", "Date", "False"));
                this.CMI_Tiles.Text = TXT.TXT_Description("Kachelansicht");
                this.CMI_Tiles.Checked = ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Modelview", "Tiles", "False"));

                this.CMI_Sort.Text = TXT.TXT_Description("Sortieren nach");
                this.CMI_Sort_ASC.Text = TXT.TXT_Description("Aufsteigend");
                this.CMI_Sort_ASC.Checked = (ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Modelview", "SortDirection", "0")) == 0);
                this.CMI_Sort_DESC.Text = TXT.TXT_Description("Absteigend");
                this.CMI_Sort_DESC.Checked = (ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Modelview", "SortDirection", "0")) == 1);

                this.CMI_Sort_Name.Text = TXT.TXT_Description("Dateiname");
                this.CMI_Sort_Name.Checked = (IniFile.Read(Parameter.INI_Common, "Modelview", "SortBy", "Date") == "Name");
                this.CMI_Sort_Datum.Text = TXT.TXT_Description("Erstelldatum");
                this.CMI_Sort_Datum.Checked = (IniFile.Read(Parameter.INI_Common, "Modelview", "SortBy", "Date") == "Date");

                Modul_StatusScreen.Status_Show("");
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.Dialog_ModellView_Load");
                Modul_StatusScreen.Status_Show("");
            }
        }

        private void Dialog_ModellView_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Hide();
                this.Priv_Model_Class.Model_Online_Changed();
                Control_Clear_Dispose.Clear(this.Controls);
                Parameter.FlushMemory();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.Dialog_ModellView_Closed");
            }
        }

        private void Dialog_ModellView_Shown(object sender, EventArgs e)
        {
            try
            {
                this.PAN_Container.Visible = true;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.Dialog_ModellView_Shown");
            }
        }

        private void Files_Load()
        {
            try
            {
                this.PAN_Container.Visible = false;
                this.PAN_Container.Controls.Clear();
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                if (!string.IsNullOrEmpty(this.Folder_Path) && Directory.Exists(this.Folder_Path))
                {
                    // Mostrar miniatura si existe
                    this.LAB_Verzeichnis.Text = this.Folder_Path;
                    string thumbPath = Path.Combine(this.Folder_Path, "Thumbnail.jpg");
                    if (File.Exists(thumbPath))
                    {
                        try
                        {
                            this.RadPictureBox1.Image = new Bitmap(thumbPath);
                            this.RadPictureBox1.Visible = true;
                        }
                        catch
                        {
                            this.RadPictureBox1.Visible = false;
                        }
                    }
                    else
                    {
                        this.RadPictureBox1.Visible = false;
                    }

                    int totalFiles = 0;
                    double totalBytes = 0.0;
                    List<Video_File> videoFileList = [];
                    int counter = 1;

                    string[] allFiles = Directory.GetFiles(this.Folder_Path, "*.*", SearchOption.TopDirectoryOnly);
                    static bool predicate(string s) =>
                        s.EndsWith(".ts", StringComparison.OrdinalIgnoreCase) ||
                        s.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) ||
                        s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        s.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        s.EndsWith(".mov", StringComparison.OrdinalIgnoreCase);

                    IEnumerable<string> filtered = allFiles.Where(predicate);

                    foreach (string fileName in filtered)
                    {
                        Modul_StatusScreen.Status_Show(
                            string.Format(
                                TXT.TXT_Description("Datei {0} von {1} wird geladen"),
                                counter,
                                filtered.Count()
                            )
                        );
                        counter++;
                        Application.DoEvents();

                        try
                        {
                            FileInfo fileInfo = new FileInfo(fileName);
                            string ext = fileInfo.Extension.ToLower();

                            bool includeVideos = (IniFile.Read(Parameter.INI_Common, "Modelview", "Videos", "True") == "True");
                            bool includeImages = (IniFile.Read(Parameter.INI_Common, "Modelview", "Images", "True") == "True");

                            if (includeVideos && (ext == ".mp4" || ext == ".ts" || ext == ".mov"))
                            {
                                DataView dv = new DataView(this.DT_Record,
                                    "Record_Name = '" + fileInfo.Name + "'",
                                    null,
                                    DataViewRowState.CurrentRows);

                                if (dv.Count == 1)
                                {
                                    Video_File video = new Video_File
                                    {
                                        Pro_Video_GUID = dv[0]["Record_GUID"] is Guid g ? g : Guid.Empty,
                                        Pro_Bezeichnung = fileInfo.Name,
                                        Pro_Model_Name = this.Pro_Model_Name,
                                        Pro_Model_GUID = this.Priv_Model_GUID,
                                        Pro_Pfad = fileInfo.FullName,
                                        Pro_Favorite = ValueBack.Get_CBoolean(dv[0]["Record_Favorit"]),
                                        Pro_Resolution = dv[0]["Record_Resolution"].ToString(),
                                        Pro_FrameRate = ValueBack.Get_CInteger(dv[0]["Record_FrameRate"]),
                                        Pro_Video_Länge = ValueBack.Get_CInteger(dv[0]["Record_Länge_Minuten"]),
                                        Pro_Start = Convert.ToDateTime(dv[0]["Record_Beginn"]),
                                        Pro_Website_ID = this.Priv_Model_Class.Pro_Website_ID,
                                        Pro_IsInDB = true
                                    };

                                    if (!Convert.IsDBNull(dv[0]["Record_Ende"]))
                                    {
                                        video.Pro_Ende = Convert.ToDateTime(dv[0]["Record_Ende"]);
                                        video.Pro_Run_Record = false;
                                    }
                                    else if (dv[0]["Maschine"].ToString() == Environment.MachineName &&
                                             Parameter.Task_Runs(ValueBack.Get_CInteger(dv[0]["Record_PID"])))
                                    {
                                        video.Pro_Run_Record = true;
                                    }

                                    videoFileList.Add(video);
                                }
                                else if (File.Exists(fileInfo.FullName + ".vdb"))
                                {
                                    DataSet ds = new DataSet();
                                    ds.ReadXml(fileInfo.FullName + ".vdb");
                                    if (ds.Tables.Count == 1)
                                    {
                                        DataRow row = ds.Tables[0].Rows[0];
                                        videoFileList.Add(new Video_File
                                        {
                                            Pro_Video_GUID = ValueBack.Get_CUnique(null),
                                            Pro_Bezeichnung = fileInfo.Name,
                                            Pro_Model_Name = row["Channel_Name"].ToString(),
                                            Pro_Pfad = fileInfo.FullName,
                                            Pro_Resolution = row["Record_Resolution"].ToString(),
                                            Pro_FrameRate = ValueBack.Get_CInteger(row["Record_FrameRate"].ToString()),
                                            Pro_Video_Länge = ValueBack.Get_CInteger(row["Record_Länge_Minuten"]),
                                            Pro_Start = Convert.ToDateTime(row["Record_Beginn"]),
                                            Pro_IsInDB = false
                                        });
                                    }
                                }
                                else
                                {
                                    videoFileList.Add(new Video_File
                                    {
                                        Pro_Video_GUID = ValueBack.Get_CUnique(null),
                                        Pro_Bezeichnung = fileInfo.Name,
                                        Pro_Model_Name = this.Pro_Model_Name,
                                        Pro_Model_GUID = this.Priv_Model_GUID,
                                        Pro_Pfad = fileInfo.FullName,
                                        Pro_Start = fileInfo.CreationTime,
                                        Pro_IsInDB = false
                                    });
                                }
                            }
                            else if (includeImages && !fileInfo.Name.StartsWith("Thumbnail", StringComparison.OrdinalIgnoreCase) &&
                                     (ext == ".jpg" || ext == ".jpeg"))
                            {
                                videoFileList.Add(new Video_File
                                {
                                    Pro_Pfad = fileInfo.FullName,
                                    Pro_Is_Image = true,
                                    Pro_Start = fileInfo.CreationTime,
                                    Pro_Bezeichnung = fileInfo.Name
                                });
                            }

                            totalFiles++;
                            totalBytes += fileInfo.Length;
                        }
                        catch
                        {
                            // Ignorar cualquier excepción individual
                        }
                    }

                    Modul_StatusScreen.Status_Show(TXT.TXT_Description("Vorschau wird vorbereitet"));

                    // Ordenar lista según configuración
                    List<Video_File> sortedList;
                    bool sortByDate = (IniFile.Read(Parameter.INI_Common, "Modelview", "SortBy", "Date") == "Date");
                    bool asc = (ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Modelview", "SortDirection", "0")) == 0);

                    if (sortByDate)
                    {
                        sortedList = asc
                            ? videoFileList.OrderBy(x => x.Pro_Start).ToList()
                            : videoFileList.OrderByDescending(x => x.Pro_Start).ToList();
                    }
                    else
                    {
                        sortedList = asc
                            ? videoFileList.OrderBy(x => x.Pro_Bezeichnung).ToList()
                            : videoFileList.OrderByDescending(x => x.Pro_Bezeichnung).ToList();
                    }

                    this.Videos_Show(sortedList);

                    // Actualizar etiquetas de conteo y tamaño
                    if (totalFiles > 0)
                    {
                        this.LAB_Dateien.Text = totalFiles + " " + TXT.TXT_Description("Aufnahmen gespeichert");
                    }
                    else
                    {
                        this.LAB_Dateien.Visible = false;
                    }

                    if (totalBytes > 0.0)
                    {
                        this.LAB_Größe.Text = TXT.TXT_Description("Größe aller Aufnahmen") + " " + ValueBack.Get_Numeric2Bytes(totalBytes);
                    }
                    else
                    {
                        this.LAB_Größe.Visible = false;
                    }
                }

                // Volver a mostrar contenido
                this.PAN_Container.Visible = true;
                this.Cursor = Cursors.Default;
                Parameter.FlushMemory();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.Files_Load");
                Modul_StatusScreen.Status_Show("");
            }
        }

        private async void Videos_Show(List<Video_File> Video_List)
        {
            await Task.CompletedTask;

            bool showTiles = ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Modelview", "Tiles", "False"));
            bool groupByDate = ValueBack.Get_CBoolean(IniFile.Read(Parameter.INI_Common, "Modelview", "Date", "False"));

            // Invertir orden para que los más recientes queden arriba
            Video_List.Reverse();

            try
            {
                foreach (Video_File video in Video_List)
                {
                    Application.DoEvents();

                    if (video.Pro_Is_Image)
                    {
                        Control_Image_Preview ctrlImg = new Control_Image_Preview
                        {
                            Dock = DockStyle.Top,
                            Model_Name = this.Pro_Model_Name,
                            Bezeichnung = video.Pro_Bezeichnung,
                            Model_GUID = this.Priv_Model_GUID,
                            Pro_Stream_File = video.Pro_Pfad,
                            Pro_Video_list = Video_List
                        };
                        ctrlImg.BTN_Galerie.Visible = false;

                        string panelTag = groupByDate
                            ? video.Pro_Start.ToShortDateString()
                            : string.Empty;

                        this.Panel_Find(panelTag).Controls.Add(ctrlImg);
                    }
                    else
                    {
                        Control_Video_Preview ctrlVid = new Control_Video_Preview
                        {
                            Dock = DockStyle.Top,
                            Pro_IsInDB = video.Pro_IsInDB,
                            Pro_Tiles_Show = showTiles,
                            Pro_Video_Start = video.Pro_Start,
                            Pro_Video_Ende = video.Pro_Ende,
                            Bezeichnung = video.Pro_Bezeichnung,
                            Can_DragDrop = true,
                            Model_Name = this.Pro_Model_Name,
                            Model_GUID = this.Priv_Model_GUID,
                            Pro_Favorite = video.Pro_Favorite,
                            Pro_Website_ID = video.Pro_Website_ID.ToString(),
                            Pro_FrameRate = video.Pro_FrameRate.ToString(),
                            Pro_Resoultion = video.Pro_Resolution,
                            Pro_Video_GUID = video.Pro_Video_GUID,
                            Pro_Video_Länge = video.Pro_Video_Länge,
                            Pro_Stream_File = video.Pro_Pfad,
                            Pro_Video_list = Video_List
                        };
                        ctrlVid.BTN_Galerie.Visible = false;
                        ctrlVid.Videos_Merge += new Control_Video_Preview.Videos_MergeEventHandler(this.Videos_Merge);
                        ctrlVid.Video_Convert += new Control_Video_Preview.Video_ConvertEventHandler(this.Files_Load);

                        string panelTag = groupByDate
                            ? ctrlVid.Pro_Video_Start.ToShortDateString()
                            : string.Empty;

                        this.Panel_Find(panelTag).Controls.Add(ctrlVid);
                        ctrlVid.BringToFront();
                    }
                }
            }
            catch { }
        }

        private void CME_Ansicht_DropDownOpening(object sender, EventArgs e)
        {
            // Actualizar checks en el menú antes de mostrarlo
            this.CMI_Sort_ASC.Checked = (ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Modelview", "SortDirection", "0")) == 0);
            this.CMI_Sort_DESC.Checked = (ValueBack.Get_CInteger(IniFile.Read(Parameter.INI_Common, "Modelview", "SortDirection", "0")) == 1);
            this.CMI_Sort_Datum.Checked = (IniFile.Read(Parameter.INI_Common, "Modelview", "SortBy", "Date") == "Date");
            this.CMI_Sort_Name.Checked = (IniFile.Read(Parameter.INI_Common, "Modelview", "SortBy", "Date") == "Name");
            this.CMI_Image.Checked = (IniFile.Read(Parameter.INI_Common, "Modelview", "Images", "True") == "True");
            this.CMI_Videos.Checked = (IniFile.Read(Parameter.INI_Common, "Modelview", "Videos", "True") == "True");
        }

        private void CMI_Sort_ASC_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "SortDirection", "0");
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Sort_ASC_Click");
            }
        }

        private void CMI_Sort_DESC_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "SortDirection", "1");
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Sort_DESC_Click");
            }
        }

        private void CMI_Sort_Name_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "SortBy", "Name");
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Sort_Name_Click");
            }
        }

        private void CMI_Sort_Datum_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "SortBy", "Date");
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Sort_Datum_Click");
            }
        }

        private void CMI_Channel_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "Date", this.CMI_Datum.Checked.ToString());
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Channel_Click");
            }
        }

        private void CMI_Kachel_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "Tiles", this.CMI_Tiles.Checked.ToString());
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Kachel_Click");
            }
        }

        private void CMI_Image_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "Images", this.CMI_Image.Checked.ToString());
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Image_Click");
            }
        }

        private void CMI_Videos_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile.Write(Parameter.INI_Common, "Modelview", "Videos", this.CMI_Videos.Checked.ToString());
                this.Files_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.CMI_Videos_Click");
            }
        }

        private void BTN_Explorer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(this.Folder_Path))
                    Directory.CreateDirectory(this.Folder_Path);
                Process.Start(this.Folder_Path);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.BTN_Explorer_Click");
            }
        }

        private void BTN_Einstellungen_Click(object sender, EventArgs e)
        {
            try
            {
                using (Dialog_Model_Einstellungen modelEinstellungen = new Dialog_Model_Einstellungen(this.Priv_Model_GUID))
                {
                    modelEinstellungen.StartPosition = FormStartPosition.CenterParent;
                    modelEinstellungen.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.BTN_Einstellungen_Click");
            }
        }

        private CustomFlowLayoutPanel Panel_Find(string Panel_Name)
        {
            try
            {
                CustomFlowLayoutPanel found = null;

                // Buscar un panel con la misma etiqueta (Tag) en PAN_Container
                foreach (Control ctrl in this.PAN_Container.Controls)
                {
                    if (ctrl is Panel pnl && pnl.Tag != null && pnl.Tag.ToString() == Panel_Name)
                    {
                        found = pnl.Controls.OfType<CustomFlowLayoutPanel>().FirstOrDefault();
                        if (found != null)
                            return found;
                    }
                }

                // Si no existe, crear uno nuevo
                Panel newPanel = new Panel
                {
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    Tag = Panel_Name,
                    AutoScroll = true
                };

                Label header = new Label
                {
                    Dock = DockStyle.Top,
                    Text = Panel_Name,
                    BackColor = Color.DarkGray,
                    AutoSize = false,
                    Height = twentyFivePixels(),
                    Font = new Font(Font, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                CustomFlowLayoutPanel flow = new CustomFlowLayoutPanel
                {
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    Tag = Panel_Name
                };

                newPanel.Controls.Add(flow);
                newPanel.Controls.Add(header);
                this.PAN_Container.Controls.Add(newPanel);

                return flow;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.Panel_Find");
                return null;
            }

            // Método auxiliar para dar altura consistente a los encabezados:
            int twentyFivePixels() => 25;
        }

        private void Videos_Merge(string FirstFile, string SecondFile, string Ausgabe_File)
        {
            try
            {
                if (FirstFile == null || SecondFile == null || Ausgabe_File == null)
                {
                    this.Work_Timer.Stop();
                    this.PAN_Work.Visible = false;
                    return;
                }

                this.Pri_FirstFile = FirstFile;
                this.Pri_SecondFile = SecondFile;
                this.Pri_AusgabeFile = Ausgabe_File;

                string dir1 = Path.GetDirectoryName(this.Pri_FirstFile);
                string dir2 = Path.GetDirectoryName(this.Pri_SecondFile);
                this.Pri_Temp_First = Path.Combine(dir1, "TMPFile1.ts");
                this.Pri_Temp_Last = Path.Combine(dir2, "TMPFile2.ts");

                // Calcular longitud total en KB
                this.Pri_Input_Lenght = new FileInfo(FirstFile).Length
                                     + new FileInfo(SecondFile).Length;
                this.Pri_Input_Lenght /= 1024.0;

                this.PGB_Fortschritt.Maximum = (int)Math.Round(this.Pri_Input_Lenght);
                this.PAN_Work.Visible = true;
                this.Pri_Merge_Start = DateTime.Now;
                this.Work_Timer.Interval = 1000;
                this.Work_Timer.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.Videos_Merge");
            }
        }

        private void Work_Fortschritt()
        {
            try
            {
                double accumulated = 0.0;

                if (File.Exists(this.Pri_Temp_First))
                    accumulated += new FileInfo(this.Pri_Temp_First).Length;
                if (File.Exists(this.Pri_Temp_Last))
                    accumulated += new FileInfo(this.Pri_Temp_Last).Length;
                if (File.Exists(this.Pri_AusgabeFile))
                    accumulated += new FileInfo(this.Pri_AusgabeFile).Length;

                string currentFile = "";
                if (File.Exists(this.Pri_Temp_First) && !File.Exists(this.Pri_Temp_Last) && !File.Exists(this.Pri_AusgabeFile))
                {
                    currentFile = this.Pri_FirstFile;
                    this.PGB_Fortschritt.Value = 0;
                    this.PGB_Fortschritt.Text = TXT.TXT_Description("Datei") + " " + currentFile + " " + TXT.TXT_Description("wird konvertiert");
                }
                else if (File.Exists(this.Pri_Temp_First) && File.Exists(this.Pri_Temp_Last) && !File.Exists(this.Pri_AusgabeFile))
                {
                    currentFile = this.Pri_SecondFile;
                    this.PGB_Fortschritt.Text = TXT.TXT_Description("Datei") + " " + currentFile + " " + TXT.TXT_Description("wird konvertiert");
                }
                else if (File.Exists(this.Pri_Temp_First) && File.Exists(this.Pri_Temp_Last) && File.Exists(this.Pri_AusgabeFile))
                {
                    currentFile = this.Pri_AusgabeFile;
                    this.PGB_Fortschritt.Text = TXT.TXT_Description("Datei") + " " + currentFile + " " + TXT.TXT_Description("wird erstellt");
                }
                else
                {
                    this.PGB_Fortschritt.Text = "";
                }

                double progressKB = accumulated / 1024.0;
                int intProgress = (int)Math.Min(progressKB, this.PGB_Fortschritt.Maximum);
                this.PGB_Fortschritt.Value = intProgress;

                // Calcular tiempo restante aproximado
                double ratio = this.Pri_Input_Lenght / progressKB;
                int elapsedSeconds = (int)(DateTime.Now - this.Pri_Merge_Start).TotalSeconds;
                int estimatedSecs = (int)(ratio * elapsedSeconds) - elapsedSeconds;

                if (estimatedSecs < 0)
                {
                    this.LAB_TimeRun.Text = TXT.TXT_Description("Gleich fertig");
                }
                else if (estimatedSecs / 60.0 > 2.0)
                {
                    this.LAB_TimeRun.Text = (estimatedSecs / 60).ToString() + " min";
                }
                else
                {
                    this.LAB_TimeRun.Text = estimatedSecs.ToString() + " sec";
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_ModellView.Work_Fortschritt");
            }
        }
    }
}
