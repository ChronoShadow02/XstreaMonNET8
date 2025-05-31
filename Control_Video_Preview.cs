#nullable disable
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace XstreaMonNET8
{
    public partial class Control_Video_Preview : UserControl
    {
        // Campos privados (sin cambiar nombres)
        public bool Vorschau_Visible;
        private List<Video_File> Pri_Video_list;
        internal string Model_Name;
        internal Guid Model_GUID;
        private bool Pri_Record_Run;
        private Bitmap Video_Thumb;
        private System.Windows.Forms.Timer Vorschau_timer;
        private Bitmap Pri_Vorschau_Image;
        private Bitmap Pri_Video_Thumb_Time;
        private DateTime Pri_Video_Start;
        private DateTime Pri_Video_Ende;
        private string Pri_Resolution;
        private int Pri_FrameRate;
        private string Pri_Bezeichnung;
        private bool Pri_Favoriten;
        private int Pri_Video_Länge;
        private string Pri_Video_Größe;
        private int Pri_WebSite_ID;
        private bool Pri_Tiles_Show;
        private string Stream_File;
        private Bitmap Stream_File_Load_Vorschau;
        private Bitmap Stream_File_Load_Zeitleiste;
        private int Vorschau_position;
        private Bitmap Thumb_BMP;
        private int Video_position;
        private bool MouseIsDown;
        private int Convert_ID;
        private System.Windows.Forms.Timer Convert_Timer;

        // Eventos públicos
        internal delegate void Videos_MergeEventHandler(string FirstFile, string SecondFile, string Ausgabe_File);
        internal delegate void Video_ConvertEventHandler();

        internal event Videos_MergeEventHandler Videos_Merge;
        internal event Video_ConvertEventHandler Video_Convert;

        // Propiedades públicas (sin cambiar nombres)
        internal Guid Pro_Video_GUID { get; set; }

        internal List<Video_File> Pro_Video_list
        {
            get => this.Pri_Video_list;
            set
            {
                this.Pri_Video_list = value
                    .OrderBy(x => x.Pro_Start)
                    .ToList();
            }
        }

        internal bool Can_DragDrop
        {
            get => this.AllowDrop;
            set => this.AllowDrop = value;
        }

        internal bool Pro_Record_Run
        {
            get => this.Pri_Record_Run;
            set => this.Pri_Record_Run = value;
        }

        private Bitmap Vorschau_Image
        {
            get => this.Pri_Vorschau_Image;
            set
            {
                if (value == null) return;
                this.Pri_Vorschau_Image = value;
                this.Refresh();
            }
        }

        private Bitmap Video_Thumb_Time
        {
            get => this.Pri_Video_Thumb_Time;
            set
            {
                if (value == null) return;
                this.Pri_Video_Thumb_Time = value;
                this.Refresh();
            }
        }

        internal DateTime Pro_Video_Start
        {
            get => this.Pri_Video_Start;
            set => this.Pri_Video_Start = value;
        }

        internal DateTime Pro_Video_Ende
        {
            get => this.Pri_Video_Ende;
            set => this.Pri_Video_Ende = value;
        }

        internal string Pro_Resoultion
        {
            get => this.Pri_Resolution;
            set => this.Pri_Resolution = value;
        }

        internal string Pro_FrameRate
        {
            get => this.Pri_FrameRate.ToString();
            set
            {
                if (int.TryParse(value, out int v))
                    this.Pri_FrameRate = v;
                else
                    this.Pri_FrameRate = 0;
            }
        }

        internal bool Pro_IsInDB { get; set; }

        internal string Bezeichnung
        {
            get => this.Pri_Bezeichnung;
            set => this.Pri_Bezeichnung = value ?? "";
        }

        internal bool Pro_Favorite
        {
            get => this.TBT_Favorite.Checked;
            set
            {
                this.Pri_Favoriten = value;
                this.TBT_Favorite.Checked = value;
            }
        }

        internal int Pro_Video_Länge
        {
            get => this.Pri_Video_Länge;
            set => this.Pri_Video_Länge = value;
        }

        internal string Pro_Video_Größe
        {
            get => this.Pri_Video_Größe;
            set => this.Pri_Video_Größe = value;
        }

        internal string Pro_Website_ID
        {
            get => Sites.Website_Find(this.Pri_WebSite_ID).Pro_Name;
            set => this.Pri_WebSite_ID = int.Parse(value);
        }

        internal bool Pro_Tiles_Show
        {
            get => this.Pri_Tiles_Show;
            set => this.Pri_Tiles_Show = value;
        }

        internal string Pro_Stream_File
        {
            get => this.Stream_File;
            set
            {
                try
                {
                    this.Stream_File = value;
                    this.Pro_Video_Größe = ValueBack.Get_Numeric2Bytes((double)new FileInfo(this.Stream_File).Length);
                    if (!File.Exists(this.Stream_File + ".vdb") && !this.Pro_Record_Run)
                    {
                        this.Video_Thumb_Time = Resources.Wait; // placeholder resource
                        BackgroundWorker BgW = new BackgroundWorker();
                        BgW.DoWork += (s, e) => this.CreateLoad_Worker_Work();
                        BgW.RunWorkerCompleted += (s, e) => this.File_Worker_Load();
                        VDB_File_Creator.WorkAdd(BgW);
                    }
                    else if (this.Pro_Record_Run)
                    {
                        this.Stream_File_Load_Vorschau = new Bitmap(Resources.RecordRun,
                            new Size(
                                (int)Math.Round(Resources.RecordRun.Width * 202.0 / Resources.RecordRun.Height),
                                202));
                        this.FileLoad_Worker_Completed();
                    }
                    else
                    {
                        this.File_Worker_Load();
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Control_Video_Preview.Pro_Stream_File");
                }
            }
        }

        // Constructor
        public Control_Video_Preview()
        {
            this.Disposed += Control_Video_Preview_Disposed;
            this.MouseLeave += Control_Video_Preview_MouseLeave;
            this.Load += Control_Video_Preview_Load;
            this.MouseEnter += Control_Video_Preview_MouseEnter;
            this.DragEnter += Control_DragEnter;
            this.DragDrop += Control_DragDrop;
            this.MouseMove += Control_MouseDown;
            this.MouseUp += Control_MouseUp;
            this.MouseMove += Control_MouseMove;
            this.Paint += Control_Video_Preview_Paint;
            this.DoubleClick += Control_Video_Preview_DoubleClick;

            this.Vorschau_Visible = true;
            this.Pri_Video_list = new List<Video_File>();
            this.Pri_Record_Run = false;
            this.Vorschau_timer = new System.Windows.Forms.Timer();
            this.Pro_IsInDB = false;
            this.Pri_Video_Länge = 1;
            this.Pri_WebSite_ID = -1;
            this.Pri_Tiles_Show = false;
            this.Stream_File_Load_Vorschau = null;
            this.Stream_File_Load_Zeitleiste = null;
            this.Vorschau_position = -4;
            this.Thumb_BMP = null;
            this.MouseIsDown = false;
            this.Convert_Timer = new System.Windows.Forms.Timer();
            this.Convert_ID = 0;

            InitializeComponent(); // Llama al código generado en Designer
            try
            {
                this.CMI_Öffnen.Text = TXT.TXT_Description("Öffnen");
                this.CMI_Löschen.Text = TXT.TXT_Description("Löschen");
                this.CMI_Directory_Open.Text = TXT.TXT_Description("Dateispeicherort öffnen");
                this.CMI_Convert_MP4.Text = TXT.TXT_Description("Konvertieren zur MP4 Datei");
                this.CMI_Property.Text = TXT.TXT_Description("Eigenschaften");
                this.TTP_Control.SetToolTip(this.BTN_Delete, TXT.TXT_Description("Video löschen"));
                this.TTP_Control.SetToolTip(this.BTN_Play, TXT.TXT_Description("Video öffnen"));
                this.TTP_Control.SetToolTip(this.BTN_Galerie, TXT.TXT_Description("Galerie öffnen"));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.New");
            }
        }

        // Limpia recursos al desechar
        private void Control_Video_Preview_Disposed(object sender, EventArgs e)
        {
            try
            {
                this.Video_Thumb = null;
                this.Video_Thumb_Time = null;
                this.Vorschau_timer?.Stop();
                this.Vorschau_timer = null;
                this.Pri_Vorschau_Image = null;
                this.Controls.Clear();
                this.Dispose(true);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_Video_Preview_Disposed");
            }
        }

        // Crea archivos de vista previa en segundo plano
        private void CreateLoad_Worker_Work()
        {
            Class_Stream_Record.Preview_Files_Create(
                this.Stream_File, this.Model_Name, this.Pri_WebSite_ID, this.Pro_Video_Start);
            this.Refresh();
        }

        // Inicia carga de datos del .vdb
        private void File_Worker_Load()
        {
            try
            {
                if (this.Disposing || !File.Exists(this.Stream_File + ".vdb"))
                    return;
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (s, e) => this.FileLoad_Worker_Work();
                backgroundWorker.RunWorkerCompleted += (s, e) => this.FileLoad_Worker_Completed();
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.File_Worker_Load");
            }
        }

        // Lee datos de .vdb en segundo plano
        private void FileLoad_Worker_Work()
        {
            try
            {
                if (!File.Exists(this.Stream_File + ".vdb"))
                    return;
                using (DataSet dataSet = new DataSet())
                {
                    int _ = (int)dataSet.ReadXml(this.Stream_File + ".vdb");
                    if (dataSet.Tables.Count == 1)
                    {
                        DataRow row = dataSet.Tables[0].Rows[0];
                        this.Model_Name = row["Channel_Name"].ToString();
                        this.Pro_Video_Start = Convert.ToDateTime(row["Record_Beginn"]);
                        var endeVal = row["Record_Ende"];
                        this.Pro_Video_Ende = (endeVal == DBNull.Value)
                            ? this.Pro_Video_Start
                            : Convert.ToDateTime(endeVal);
                        this.Pro_FrameRate = row["Record_FrameRate"].ToString();
                        this.Pro_Resoultion = row["Record_Resolution"].ToString();
                        this.Pro_Video_Länge = Convert.ToInt32(row["Record_Länge_Minuten"]);
                        this.Bezeichnung = row["Record_Name"].ToString();

                        if (this.Pro_Tiles_Show)
                        {
                            if (dataSet.Tables[0].Columns.Contains("Video_Tiles") &&
                                row["Video_Tiles"] != DBNull.Value)
                            {
                                byte[] bytes = (byte[])row["Video_Tiles"];
                                using (var ms = new MemoryStream(bytes))
                                {
                                    this.Stream_File_Load_Vorschau = new Bitmap(ms);
                                }
                            }
                        }
                        else if (dataSet.Tables[0].Columns.Contains("Video_Preview") &&
                                 row["Video_Preview"] != DBNull.Value)
                        {
                            byte[] bytes = (byte[])row["Video_Preview"];
                            using (var ms = new MemoryStream(bytes))
                            {
                                this.Stream_File_Load_Vorschau = new Bitmap(ms);
                            }
                        }
                    }

                    if (dataSet.Tables[0].Columns.Contains("Video_Timeline") &&
                        dataSet.Tables[0].Rows[0]["Video_Timeline"] != DBNull.Value)
                    {
                        byte[] bytes = (byte[])dataSet.Tables[0].Rows[0]["Video_Timeline"];
                        using (var ms = new MemoryStream(bytes))
                        {
                            this.Stream_File_Load_Zeitleiste = new Bitmap(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.FileLoad_Worker_Work");
            }
        }

        // Acción al completar la carga del .vdb
        private void FileLoad_Worker_Completed()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => this.FileLoad_Worker_Completed_Thread()));
                }
                else
                {
                    this.FileLoad_Worker_Completed_Thread();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.FileLoad_Worker_Completed");
            }
        }

        // Ajusta UI tras cargar datos
        private void FileLoad_Worker_Completed_Thread()
        {
            try
            {
                if (this.Disposing) return;

                this.BTN_Delete.Enabled = !this.Pro_Record_Run;
                this.BTN_Play.Enabled = !this.Pro_Record_Run;
                this.CMI_Löschen.Enabled = !this.Pro_Record_Run;
                this.CMI_Öffnen.Enabled = !this.Pro_Record_Run;

                if (this.Stream_File_Load_Vorschau != null)
                {
                    this.Video_Thumb = this.Stream_File_Load_Vorschau;
                    this.Video_Thumb_Time = this.Stream_File_Load_Vorschau;
                }

                if (!this.Pro_Tiles_Show && this.Stream_File_Load_Zeitleiste != null)
                {
                    this.Vorschau_Image = this.Stream_File_Load_Zeitleiste;
                }

                this.PAN_Vorschau.Visible = !this.Pro_Tiles_Show;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.FileLoad_Worker_Completed_Thread");
            }
        }

        // Acción final tras cargarse el archivo de vista previa
        private void Stream_File_Load_Finaly()
        {
            try
            {
                this.BTN_Delete.Enabled = !this.Pro_Record_Run;
                this.BTN_Play.Enabled = !this.Pro_Record_Run;
                this.CMI_Löschen.Enabled = !this.Pro_Record_Run;
                this.CMI_Öffnen.Enabled = !this.Pro_Record_Run;
                if (this.Stream_File_Load_Vorschau != null)
                {
                    this.Video_Thumb = this.Stream_File_Load_Vorschau;
                    this.Video_Thumb_Time = this.Stream_File_Load_Vorschau;
                }
                if (this.Stream_File_Load_Zeitleiste != null)
                {
                    this.Vorschau_Image = this.Stream_File_Load_Zeitleiste;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Stream_File_Load_Finaly");
            }
        }

        // Reproduce video en el tiempo indicado
        private void Video_Play(int TimeStamp)
        {
            try
            {
                if (File.Exists(this.Pro_Stream_File))
                {
                    // Si se configura player interno en INI, lo llamamos
                    bool useInternal = Convert.ToBoolean(IniFile.Read(Parameter.INI_Common, "Player", "Intern", "True"));
                    if (useInternal)
                    {
                        // Arma argumentos para CRPlayer
                        string argList = "-File:\"" + this.Pro_Stream_File + "\" -Time:" + TimeStamp + " -List:";
                        int indexInList = 0;
                        foreach (var proVideo in this.Pro_Video_list)
                        {
                            if (!proVideo.Pro_Pfad.Equals(this.Pro_Stream_File, StringComparison.OrdinalIgnoreCase))
                                indexInList++;
                            else
                                break;
                        }

                        int halfList = 0;
                        int charCount = argList.Length;
                        foreach (var proVideo in this.Pro_Video_list)
                        {
                            string candidate = "\"" + proVideo.Pro_Pfad + "\"|";
                            if (charCount + candidate.Length <= 32000)
                            {
                                charCount += candidate.Length;
                                halfList++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        int endIdx = Math.Min(this.Pro_Video_list.Count, indexInList + halfList / 2);
                        int startIdx = Math.Max(0, indexInList - halfList / 2);
                        if (endIdx > this.Pro_Video_list.Count)
                        {
                            int diff = endIdx - this.Pro_Video_list.Count;
                            endIdx = this.Pro_Video_list.Count;
                            startIdx = Math.Max(0, startIdx - diff);
                        }
                        if (startIdx < 0)
                        {
                            int diff = -startIdx;
                            startIdx = 0;
                            endIdx = Math.Min(this.Pro_Video_list.Count, endIdx + diff);
                        }

                        string listArgs = "";
                        for (int i = startIdx; i < endIdx; i++)
                        {
                            string path = this.Pro_Video_list[i].Pro_Pfad;
                            if (argList.Length + listArgs.Length + path.Length + 3 <= 32000)
                            {
                                listArgs += "\"" + path + "\"|";
                            }
                            else
                            {
                                break;
                            }
                        }

                        Process.Start(new ProcessStartInfo
                        {
                            FileName = Path.Combine(Application.StartupPath, "CRPlayer.exe"),
                            Arguments = argList + listArgs,
                            WindowStyle = ProcessWindowStyle.Normal,
                            UseShellExecute = false
                        });
                    }
                    else
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = this.Pro_Stream_File,
                            UseShellExecute = true
                        });
                    }
                }
                else
                {
                    MessageBox.Show(TXT.TXT_Description("Die Datei kann nicht geladen werden"));
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Video_Play");
            }
        }

        // Al salir del control con el mouse
        private void Control_Video_Preview_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.Vorschau_timer?.Stop();
                this.Video_Thumb_Time = this.Video_Thumb;
                this.Vorschau_position = -4;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_Video_Preview_MouseLeave");
            }
        }

        // Carga inicial del control
        private void Control_Video_Preview_Load(object sender, EventArgs e)
        {
            try
            {
                this.Vorschau_timer.Interval = 250;
                this.Vorschau_timer.Tick += (s, ev) => this.Vorschau_Timer_Tick();

                if (this.Vorschau_Visible)
                {
                    this.Height = 250;
                    this.PAN_Vorschau.Visible = true;
                }
                else
                {
                    this.Height = 224;
                    this.PAN_Vorschau.Visible = false;
                }

                this.TBT_Favorite.Enabled = this.Pro_Video_GUID != Guid.Empty;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_Video_Preview_Load");
            }
        }

        // Temporizador de vista previa (avanza thumbnail)
        private void Vorschau_Timer_Tick()
        {
            try
            {
                this.Vorschau_timer?.Stop();
                if (this.Vorschau_position > 0)
                {
                    if (this.Video_Thumb == null)
                        return;

                    if (this.Vorschau_position >= this.Pro_Video_Länge)
                        this.Vorschau_position = 0;

                    BackgroundWorker bg = new BackgroundWorker();
                    bg.DoWork += (s, e) => this.Vorschau_Timer_Tick_Image_Create();
                    bg.RunWorkerCompleted += (s, e) => this.Vorschau_Timer_Tick_Finaly();
                    bg.RunWorkerAsync();
                }
                else
                {
                    this.Vorschau_position++;
                    this.Vorschau_timer?.Start();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Vorschau_Timer_Tick");
            }
        }

        // Crea la imagen de vista previa en un hilo de fondo
        private void Vorschau_Timer_Tick_Image_Create()
        {
            try
            {
                this.Thumb_BMP = CreateThumbFromVideo.GenerateThumb(
                    this.Pro_Stream_File,
                    this.Vorschau_position * 60,
                    this.Video_Thumb.Width,
                    this.Video_Thumb.Height
                ).Result;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Vorschau_Timer_Tick_Image_Create");
            }
        }

        // Actualiza la vista una vez creada la imagen
        private void Vorschau_Timer_Tick_Finaly()
        {
            try
            {
                if (this.Thumb_BMP != null)
                    this.Video_Thumb_Time = this.Thumb_BMP;

                if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    this.Vorschau_position++;
                    this.Vorschau_timer?.Start();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Vorschau_Timer_Tick_Finaly");
            }
        }

        // Al entrar con el mouse en el control, inicia temporizador
        private void Control_Video_Preview_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.Vorschau_timer?.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_Video_Preview_MouseEnter");
            }
        }

        // Calcula thumbnail al mover el mouse sobre PAN_Vorschau
        private void PAN_Vorschau_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.Pro_Video_Länge <= 0 || this.Video_Thumb == null)
                    return;

                this.PAN_Vorschau.MouseMove -= PAN_Vorschau_MouseMove;
                this.Video_position = Convert.ToInt32((this.Pro_Video_Länge * 60.0) * (e.X / (double)this.PAN_Vorschau.Width));

                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += (s, ev) => this.PAN_Vorschau_Image_Create();
                bg.RunWorkerCompleted += (s, ev) => this.PAN_Vorschau_Finaly();
                bg.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.PAN_Vorschau_MouseMove");
            }
        }

        // Crea imagen de thumbnail para posición específica
        private void PAN_Vorschau_Image_Create()
        {
            try
            {
                this.Thumb_BMP = CreateThumbFromVideo.GenerateThumb(
                    this.Pro_Stream_File,
                    this.Video_position,
                    this.Video_Thumb.Width,
                    this.Video_Thumb.Height
                ).Result;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.PAN_Vorschau_Image_Create");
            }
        }

        // Finaliza la actualización de thumbnail
        private void PAN_Vorschau_Finaly()
        {
            try
            {
                if (this.Thumb_BMP != null)
                    this.Video_Thumb_Time = this.Thumb_BMP;

                this.PAN_Vorschau.MouseMove += PAN_Vorschau_MouseMove;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.PAN_Vorschau_Finaly");
            }
        }

        // Al mover mouse sobre control, inicia arrastre si se mantiene shift+click izquierdo
        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && Control.ModifierKeys.HasFlag(Keys.Shift))
                {
                    this.MouseIsDown = true;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_MouseDown");
            }
        }

        // Al soltar mouse, termina arrastre
        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                this.MouseIsDown = false;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_MouseUp");
            }
        }

        // Manejo de arrastre de archivos
        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.Pro_Stream_File != null && this.Can_DragDrop && this.MouseIsDown &&
                    e.Button == MouseButtons.Left && Control.ModifierKeys.HasFlag(Keys.Shift))
                {
                    var data = new DataObject();
                    data.SetText(this.Pro_Stream_File);
                    this.DoDragDrop(data, DragDropEffects.Move);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_MouseMove");
            }
        }

        // Manejo del evento DragEnter
        private void Control_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Text))
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_DragEnter");
            }
        }

        // Manejo del evento DragDrop (fusiona videos si es necesario)
        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string dragged = e.Data.GetData(DataFormats.Text).ToString();
                if (dragged.Equals(this.Pro_Stream_File, StringComparison.OrdinalIgnoreCase))
                    return;

                if (MessageBox.Show(
                        TXT.TXT_Description("Möchten sie beide Dateien zusammenfügen?"),
                        TXT.TXT_Description("Dateien zusammenfügen"),
                        MessageBoxButtons.YesNo
                    ) != DialogResult.Yes)
                {
                    return;
                }

                string outputName = Path.Combine(
                    Path.GetDirectoryName(this.Pro_Stream_File),
                    Path.GetFileNameWithoutExtension(this.Pro_Stream_File) + "_Temp" + Path.GetExtension(this.Pro_Stream_File)
                );
                string path1 = Path.Combine(Path.GetDirectoryName(this.Pro_Stream_File), "TMPFile1.ts");
                string path2 = Path.Combine(Path.GetDirectoryName(this.Pro_Stream_File), "TMPFile2.ts");

                this.Videos_Merge?.Invoke(this.Pro_Stream_File, dragged, outputName);

                if (File.Exists(path1)) File.Delete(path1);
                if (File.Exists(path2)) File.Delete(path2);
                if (File.Exists(outputName)) File.Delete(outputName);

                string converter = Path.Combine(Application.StartupPath, "RecordStream.exe");
                int id1 = Process.Start(new ProcessStartInfo
                {
                    FileName = converter,
                    Arguments = $"-i \"{this.Pro_Stream_File}\" -c copy -c:v libx264 -f mpegts \"{path1}\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false
                }).Id;
                while (Parameter.Task_Runs(id1))
                {
                    Thread.Sleep(500);
                    Application.DoEvents();
                }

                if (!File.Exists(path1))
                {
                    MessageBox.Show(TXT.TXT_Description("Die Temp Datei von dem Video") + " " + this.Pro_Stream_File + " " + TXT.TXT_Description("ist nicht erstellt worden."));
                    this.Videos_Merge?.Invoke(null, null, null);
                    return;
                }

                int id2 = Process.Start(new ProcessStartInfo
                {
                    FileName = converter,
                    Arguments = $"-i \"{dragged}\" -c copy -c:v libx264 -f mpegts \"{path2}\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false
                }).Id;
                while (Parameter.Task_Runs(id2))
                {
                    Thread.Sleep(500);
                    Application.DoEvents();
                }

                if (!File.Exists(path2))
                {
                    MessageBox.Show(TXT.TXT_Description("Die Temp Datei von dem Video") + " " + dragged + " " + TXT.TXT_Description("ist nicht erstellt worden."));
                    this.Videos_Merge?.Invoke(null, null, null);
                    return;
                }

                int id3 = Process.Start(new ProcessStartInfo
                {
                    FileName = converter,
                    Arguments = $"-f mpegts -i \"concat:{path1}|{path2}\" -c copy -c:v libx264 \"{outputName}\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false
                }).Id;
                while (Parameter.Task_Runs(id3))
                {
                    Thread.Sleep(500);
                    Application.DoEvents();
                }

                if (!File.Exists(outputName))
                {
                    MessageBox.Show(TXT.TXT_Description("Die Videos wurden nicht zusammengefügt."));
                    this.Videos_Merge?.Invoke(null, null, null);
                    return;
                }

                File.Delete(path1);
                File.Delete(path2);

                string backup = Path.Combine(
                    Path.GetDirectoryName(this.Pro_Stream_File),
                    "Copy " + Path.GetFileName(this.Pro_Stream_File)
                );
                File.Move(this.Pro_Stream_File, backup);
                File.Move(outputName, this.Pro_Stream_File);

                this.Videos_Merge?.Invoke(null, null, null);
                MessageBox.Show(TXT.TXT_Description("Die Videos sind zusammen gefügt worden."));
                this.Videos_Merge?.Invoke(null, null, null);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_DragDrop");
            }
        }

        // Botón Galería
        private async void BTN_Galerie_Click(object sender, EventArgs e)
        {
            try
            {
                var resultModel = await Class_Model_List.Class_Model_Find(this.Model_GUID);
                if (resultModel == null) return;
                await Task.Run(() => resultModel.Dialog_Model_View_Show());
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.BTN_Galerie_Click");
            }
        }

        // Botón Eliminar
        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(this.Pro_Stream_File);
                if (MessageBox.Show(
                        string.Format(TXT.TXT_Description("Möchten sie die Datei {0} löschen?"), fileInfo.Name),
                        TXT.TXT_Description("Datei löschen"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) != DialogResult.Yes)
                {
                    return;
                }
                CMI_Löschen_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.BTN_Delete_Click");
            }
        }

        // Botón Play
        private void BTN_Play_Click(object sender, EventArgs e)
        {
            try
            {
                CMI_Öffnen_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.BTN_Play_Click");
            }
        }

        // Abrir directorio con archivos relacionados
        private void CMI_Directory_Open_Click(object sender, EventArgs e)
        {
            try
            {
                var model = Class_Model_List.Class_Model_Find(this.Model_GUID).Result;
                if (model == null) return;
                string dir = model.Pro_Model_Directory;
                if (Directory.Exists(dir))
                    Process.Start(dir);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Directory_Open_Click");
            }
        }

        // Botón Convertir MP4
        private void CMI_Convert_MP4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Convert_Timer.Interval = 500;
                string output = Modul_Ordner.DateiPfadName(this.Pro_Stream_File.Replace(".ts", ".mp4"));
                string converter = $"\"{Path.Combine(Application.StartupPath, "RecordStream.exe")}\"";
                this.Convert_ID = Process.Start(new ProcessStartInfo
                {
                    FileName = converter,
                    Arguments = $"-loglevel warning -stats -i \"{this.Pro_Stream_File}\" -f mp4 -codec copy \"{output}\"",
                    WindowStyle = ProcessWindowStyle.Minimized,
                    UseShellExecute = false
                }).Id;
                this.Convert_Timer.Start();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Convert_MP4_Click");
            }
        }

        // Verifica fin de conversión
        private void Convert_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Parameter.Task_Runs(this.Convert_ID))
                    return;
                this.Convert_Timer.Stop();
                this.Convert_ID = 0;
                MessageBox.Show("Konvertierung ist abgeschlossen");
                this.Video_Convert?.Invoke();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Convert_Timer_Tick");
            }
        }

        // Al abrir menú contextual, muestra u oculta Convert_MP4 según extensión
        private void CMI_Video_Opened(object sender, EventArgs e)
        {
            try
            {
                if (this.Pro_Stream_File.EndsWith(".ts", StringComparison.OrdinalIgnoreCase))
                    this.CMI_Convert_MP4.Visible = true;
                else
                    this.CMI_Convert_MP4.Visible = false;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Video_Opened");
            }
        }

        // Antes de abrir menú contextual, comprueba si existe archivo
        private void CMI_Video_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.Pro_Stream_File))
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Video_Opening");
            }
        }

        // Muestra diálogo de propiedades al hacer clic en propiedad
        private void CMI_Property_Click(object sender, EventArgs e)
        {
            try
            {
                var dlg = new Dialog_Video_Propertys(this.Stream_File);
                dlg.StartPosition = FormStartPosition.CenterScreen;
                dlg.FormClosing += (s, ev) =>
                {
                    this.Pro_Stream_File = Dialog_Video_Propertys.Pri_File_Name;
                };
                dlg.Show();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Property_Click");
            }
        }

        // Doble clic en PAN_Vorschau avanza reproducción
        private void PAN_Vorschau_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MouseEventArgs me = e as MouseEventArgs;
                if (me == null) return;

                int timestamp = (int)Math.Round((double)(this.Pro_Video_Länge * 60) * (me.X / (double)this.PAN_Vorschau.Width));
                this.Video_Play(timestamp);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.PAN_Vorschau_DoubleClick");
            }
        }

        // Pinta thumbnails y texto de Bezeichnung
        private void Control_Video_Preview_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Bezeichnung))
                {
                    using (Font bold = new Font(this.Font, FontStyle.Bold))
                    using (Brush brush = new SolidBrush(this.ForeColor))
                    {
                        e.Graphics.DrawString(this.Bezeichnung, bold, brush, 2f, 2f);
                    }
                }

                if (this.Video_Thumb_Time == null)
                    return;

                this.Video_Propertys();

                int x = (int)Math.Round(172.0 - (double)this.Video_Thumb_Time.Width / 2.0);
                if (x < 1) x = 1;
                e.Graphics.DrawImage(this.Video_Thumb_Time, x, 20);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Control_Video_Preview_Paint");
            }
        }

        // Genera texto descriptivo con propiedades de video en LAB_Video_Propertys
        private void Video_Propertys()
        {
            try
            {
                DateTime dt = this.Pro_Video_Start.Date;
                string day = dt.Day.ToString("D2");
                string month = dt.Month.ToString("D2");
                string yearSuffix = (dt.Year - 2000).ToString();
                string datePart = $"{day}.{month}.{yearSuffix}";

                TimeSpan ts = this.Pro_Video_Start.TimeOfDay;
                string hour = ts.Hours.ToString("D2");
                string minute = ts.Minutes.ToString("D2");
                string timePart = $"{hour}:{minute}";

                string text = $"{datePart}\r\n{timePart}\r\n{this.Pro_Video_Länge} {TXT.TXT_Description("Minuten")}";

                if (!string.IsNullOrEmpty(this.Pro_Video_Größe))
                    text += $"\r\n{this.Pro_Video_Größe}";

                if (this.Pri_FrameRate > 0)
                    text += $"\r\n{(this.Pri_FrameRate / 1000.0):F2} fps";

                if (!string.IsNullOrEmpty(this.Pro_Resoultion))
                    text += $"\r\n{this.Pro_Resoultion}";

                this.LAB_Video_Propertys.Text = text + "\r\n";
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.Video_Propertys");
            }
        }

        // Doble clic en el control para reproducir desde inicio
        private void Control_Video_Preview_DoubleClick(object sender, EventArgs e)
        {
            this.Video_Play(0);
        }

        // Menú contextual: Abrir
        private void CMI_Öffnen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Video_Play(0);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Öffnen_Click");
            }
        }

        // Menú contextual: Eliminar
        private void CMI_Löschen_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(this.Pro_Stream_File + ".vdb"))
                {
                    try
                    {
                        this.Vorschau_Image?.Dispose();
                        File.Delete(this.Pro_Stream_File + ".vdb");
                    }
                    catch
                    {
                        // Ignorar errores al eliminar .vdb
                    }
                }
                try
                {
                    File.Delete(this.Pro_Stream_File);
                }
                catch
                {
                    // Ignorar errores al eliminar video
                }

                using (System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection
                {
                    ConnectionString = Database_Connect.Aktiv_Datenbank()
                })
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(
                            $"SELECT * FROM DT_Record WHERE Record_GUID = '{this.Pro_Video_GUID}'",
                            conn.ConnectionString))
                        {
                            using (System.Data.OleDb.OleDbCommandBuilder builder = new System.Data.OleDb.OleDbCommandBuilder(adapter))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    adapter.Fill(ds, "DT_Record");
                                    conn.Close();
                                    if (ds.Tables["DT_Record"].Rows.Count == 1)
                                    {
                                        ds.Tables["DT_Record"].Rows[0].Delete();
                                    }
                                    adapter.Update(ds.Tables["DT_Record"]);
                                }
                            }
                        }
                    }
                }

                if (this.Parent != null && this.Parent.Controls.Count == 1)
                    this.Parent.Parent.Dispose();

                this.Dispose();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.CMI_Löschen_Click");
            }
        }
    }
}
