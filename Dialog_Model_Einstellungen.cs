using System.Data;
using System.Data.OleDb;

#nullable disable
namespace XstreaMonNET8
{
    public partial class Dialog_Model_Einstellungen : Form
    {
        private Guid Pri_Model_GUID;
        private bool Pri_Model_New;
        private OleDbDataAdapter DA_Model;
        private OleDbCommandBuilder CB_Model;
        private DataSet DS_Model;
        private string Model_Name;
        private int Model_WebSite_ID;
        private List<Channel_Info> Profil_List;
        private bool AutoSearch;
        private bool Model_Exist;
        private bool Search_Run;
        private Channel_Info Profil_Info;

        public Dialog_Model_Einstellungen(Guid Model_GUID)
        {
            this.Load += new EventHandler(this.Dialog_Model_Einstellungen_Load);
            this.Pri_Model_New = false;
            this.Model_Name = "";
            this.Model_WebSite_ID = -1;
            this.Profil_List = new List<Channel_Info>();
            this.AutoSearch = false;
            this.Model_Exist = false;
            this.Search_Run = false;
            this.Profil_Info = null;

            InitializeComponent();

            try
            {
                this.Form_Initalize();
                this.Pri_Model_GUID = Model_GUID;
                this.Database_Set(this.Pri_Model_GUID);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.New");
            }
        }

        public Dialog_Model_Einstellungen(string Model_URL, Guid Model_GUID)
        {
            this.Load += new EventHandler(this.Dialog_Model_Einstellungen_Load);
            this.Pri_Model_New = false;
            this.Model_Name = "";
            this.Model_WebSite_ID = -1;
            this.Profil_List = new List<Channel_Info>();
            this.AutoSearch = false;
            this.Model_Exist = false;
            this.Search_Run = false;
            this.Profil_Info = null;

            InitializeComponent();
            this.Form_Initalize();
            this.Pri_Model_GUID = Model_GUID;
            this.Database_Set(this.Pri_Model_GUID);

            this.TXB_Sender_Name.Text = Model_URL;
            this.TXB_Sender_Name_TextChanged(this.AutoSearch);
        }

        /// <summary>
        /// Inicializa los menús contextuales y carga datos en ComboBoxes.
        /// </summary>
        private void Form_Initalize()
        {
            try
            {
                // ContextMenu para TXB_Sender_Name (Copiar / Pegar)
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(
                    TXT.TXT_Description("Kopieren"),
                    Resources.Kopieren16
                );
                toolStripMenuItem1.Click += new EventHandler(this.Copy_Click);
                contextMenuStrip.Items.Add(toolStripMenuItem1);

                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(
                    TXT.TXT_Description("Einfügen"),
                    Resources.Einfügen16
                );
                toolStripMenuItem2.Click += new EventHandler(this.Paste_Click);
                contextMenuStrip.Items.Add(toolStripMenuItem2);

                this.TXB_Sender_Name.ContextMenuStrip = contextMenuStrip;

                // Configurar DDL_Webseite (ComboBox) con primera opción "-1" (nada elegido)
                this.DDL_Webseite.Items.Clear();
                this.DDL_Webseite.Items.Add(new ComboboxItem
                {
                    Text = TXT.TXT_Description("Webseite wählen"),
                    Value = -1
                });

                foreach (Class_Website website in Sites.Website_List)
                {
                    var item = new ComboboxItem
                    {
                        Text = website.Pro_Name,
                        Value = website.Pro_ID
                    };
                    this.DDL_Webseite.Items.Add(item);
                }
                this.DDL_Webseite.SelectedIndex = -1;
                this.DDL_Webseite.DropDownStyle = ComboBoxStyle.DropDownList;

                // Cargar DDL_Video_Encoder
                this.DDL_Video_Encoder.Items.Clear();
                foreach (Class_Decoder_Item decoderItem in Decoder.Decoder_Item_List)
                {
                    var item = new ComboboxItem
                    {
                        Text = decoderItem.Decoder_Name,
                        Value = decoderItem.Decoder_ID
                    };
                    this.DDL_Video_Encoder.Items.Add(item);
                }
                this.DDL_Video_Encoder.DropDownStyle = ComboBoxStyle.DropDownList;

                // Cargar DDL_Speicherformat
                this.DDL_Speicherformat.Items.Clear();
                foreach (Class_Speicherformate speicherformateItem in Speicherformate.Speicherformate_Item_List)
                {
                    var item = new ComboboxItem
                    {
                        Text = speicherformateItem.Pro_Speicherformat_Name,
                        Value = speicherformateItem.Pro_ID
                    };
                    this.DDL_Speicherformat.Items.Add(item);
                }
                this.DDL_Speicherformat.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Load");
            }
        }

        /// <summary>
        /// Conecta a la base de datos y llena o crea la fila del modelo.
        /// </summary>
        private void Database_Set(Guid Model_GUID)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection())
            {
                oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                this.DA_Model = new OleDbDataAdapter(
                    "SELECT * FROM DT_User WHERE User_GUID = '" + Model_GUID.ToString() + "'",
                    oleDbConnection.ConnectionString
                );
                this.CB_Model = new OleDbCommandBuilder(this.DA_Model);
                this.DS_Model = new DataSet();

                oleDbConnection.Open();
                if (oleDbConnection.State != ConnectionState.Open) return;

                this.DA_Model.Fill(this.DS_Model, "DT_Model");
                oleDbConnection.Close();

                if (this.DS_Model.Tables[0].Rows.Count == 0)
                {
                    // Nuevo canal
                    this.Text = TXT.TXT_Description("Neuen Kanal hinzufügen");

                    DataRow row = this.DS_Model.Tables[0].NewRow();
                    row["User_GUID"] = Model_GUID;
                    row["Benachrichtigung"] = Convert.ToBoolean(IniFile.Read(Parameter.INI_Common, "Optionen", "Notification", "True"));
                    row["Aufnahmestop_Auswahl"] = Convert.ToInt32(IniFile.Read(Parameter.INI_Common, "Record", "RecordStop", "1"));
                    row["Aufnahmestop_Größe"] = Convert.ToInt32(IniFile.Read(Parameter.INI_Common, "Record", "RecordSize", "0"));
                    row["Aufnahmestop_Minuten"] = Convert.ToInt32(IniFile.Read(Parameter.INI_Common, "Record", "RecordTime", "30"));
                    row["Videoqualität"] = Convert.ToInt32(IniFile.Read(Parameter.INI_Common, "Record", "Resolution", "0"));
                    row["User_Visible"] = Convert.ToBoolean(IniFile.Read(Parameter.INI_Common, "Optionen", "Visible", "True"));
                    row["User_Record"] = Convert.ToBoolean(IniFile.Read(Parameter.INI_Common, "Optionen", "Record", "False"));
                    row["Last_Online"] = DateTime.Now;
                    row["Website_ID"] = -1;
                    row["User_Gender"] = 0;
                    row["User_Favorite"] = 0;
                    row["Recorder_ID"] = Convert.ToInt32(IniFile.Read(Parameter.INI_Common, "Record", "Encoder", "0"));
                    row["SaveFormat"] = Convert.ToInt32(IniFile.Read(Parameter.INI_Common, "Record", "SaveFormat", "0"));
                    row["User_Create"] = DateTime.Now;
                    this.DS_Model.Tables[0].Rows.Add(row);

                    this.Pri_Model_New = true;
                }
                else
                {
                    // Canal existente
                    this.Model_Exist = true;
                    this.Text = TXT.TXT_Description("Kanal bearbeiten");
                    this.TXB_Sender_Name.ReadOnly = true;
                }

                if (this.DS_Model.Tables[0].Rows.Count != 1) return;

                DataRow row1 = this.DS_Model.Tables[0].Rows[0];
                this.TXB_Sender_Name.Text = row1["User_Name"].ToString();
                this.Model_Name = row1["User_Name"].ToString();

                this.CBX_Benachrichtigung.Checked = Convert.ToBoolean(row1["Benachrichtigung"]);
                this.CBX_Favoriten.Checked = Convert.ToBoolean(row1["User_Favorite"]);
                this.CBX_Record.Checked = Convert.ToBoolean(row1["User_Record"]);
                this.CBX_Visible.Checked = Convert.ToBoolean(row1["User_Visible"]);

                // Seleccionar DDL_Webseite
                int websiteId = Convert.ToInt32(row1["Website_ID"]);
                for (int i = 0; i < this.DDL_Webseite.Items.Count; i++)
                {
                    var item = this.DDL_Webseite.Items[i] as ComboboxItem;
                    if (item != null && (int)item.Value == websiteId)
                    {
                        this.DDL_Webseite.SelectedIndex = i;
                        break;
                    }
                }

                this.TXB_Land.Text = row1["User_Country"].ToString();
                this.TXB_Sprachen.Text = row1["User_Language"].ToString();
                if (row1["User_Birthday"] != DBNull.Value &&
                    DateTime.TryParse(row1["User_Birthday"].ToString(), out DateTime dt))
                {
                    this.DTP_Geburtstag.Value = dt;
                }

                // Sexo
                int gender = Convert.ToInt32(row1["User_Gender"]);
                switch (gender)
                {
                    case 0: this.RBT_Geschlecht_Weiblich.Checked = true; break;
                    case 1: this.RBT_Geschlecht_Männlich.Checked = true; break;
                    case 2: this.RBT_Geschlecht_Paar.Checked = true; break;
                    case 3: this.RBT_Geschlecht_Trans.Checked = true; break;
                    case 4: this.RBT_Geschlecht_Sonstiges.Checked = true; break;
                }

                // Stop por tiempo/tamaño
                int stopChoice = Convert.ToInt32(row1["Aufnahmestop_Auswahl"]);
                switch (stopChoice)
                {
                    case 0: this.RBT_Aufnahmestop_NoStop.Checked = true; break;
                    case 1: this.RBT_Aufnahmestop_Time.Checked = true; break;
                    case 2: this.RBT_Aufnahmestop_Size.Checked = true; break;
                }

                this.SPE_FileSize.Value = Convert.ToDecimal(row1["Aufnahmestop_Größe"]);
                this.SPE_Recordtime.Value = Convert.ToDecimal(row1["Aufnahmestop_Minuten"]);

                // Calidad de video
                int videoQual = Convert.ToInt32(row1["Videoqualität"]);
                switch (videoQual)
                {
                    case 0: this.RBT_Video_Send.Checked = true; break;
                    case 1: this.RBT_Video_Minimal.Checked = true; break;
                    case 2: this.RBT_Video_SD.Checked = true; break;
                    case 3: this.RBT_Video_HD.Checked = true; break;
                    case 4: this.RBT_Video_FullHD.Checked = true; break;
                }

                // Seleccionar encoder
                int encoderId = Convert.ToInt32(row1["Recorder_ID"]);
                for (int i = 0; i < this.DDL_Video_Encoder.Items.Count; i++)
                {
                    var item = this.DDL_Video_Encoder.Items[i] as ComboboxItem;
                    if (item != null && (int)item.Value == encoderId)
                    {
                        this.DDL_Video_Encoder.SelectedIndex = i;
                        break;
                    }
                }

                // Seleccionar formato de almacenamiento
                int saveFormatId = Convert.ToInt32(row1["SaveFormat"]);
                for (int i = 0; i < this.DDL_Speicherformat.Items.Count; i++)
                {
                    var item = this.DDL_Speicherformat.Items[i] as ComboboxItem;
                    if (item != null && (int)item.Value == saveFormatId)
                    {
                        this.DDL_Speicherformat.SelectedIndex = i;
                        break;
                    }
                }

                this.TXB_Name.Text = row1["User_Description"].ToString();

                // Calcular carpeta:
                string defaultDir = Modul_Ordner.Ordner_Pfad() + "\\";
                string savedDir = row1["User_Directory"].ToString().Trim();
                if (string.IsNullOrEmpty(savedDir))
                {
                    this.TXB_Folder.Text = defaultDir + this.TXB_Sender_Name.Text.Trim();
                }
                else
                {
                    this.TXB_Folder.Text = savedDir;
                }
            }
        }

        private void Dialog_Model_Einstellungen_Load(object sender, EventArgs e)
        {
            try
            {
                TXT.Control_Languages(this);
                this.TXB_Sender_Name.PlaceholderText = TXT.TXT_Description("Modelname oder URL");
                this.DDL_Webseite.Text = TXT.TXT_Description("Webseite wählen");

                this.AutoSearch = Convert.ToBoolean(IniFile.Read(Parameter.INI_Common, "Optionen", "Search", "False"));

                int selectedSite = -1;
                if (this.DDL_Webseite.SelectedIndex >= 0)
                {
                    var item = this.DDL_Webseite.Items[this.DDL_Webseite.SelectedIndex] as ComboboxItem;
                    if (item != null)
                        selectedSite = (int)item.Value;
                }

                if (Sites.Website_Find(selectedSite) != null)
                {
                    this.LAB_Kanal_Gefunden.Visible = this.Profil_Info?.Pro_Exist ?? false;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Load");
            }
        }

        private void BTN_Übernehmen_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TXB_Sender_Name.Text.Trim().Length == 0)
                {
                    MessageBox.Show(TXT.TXT_Description("Modelname eingeben"));
                    this.TXB_Sender_Name.Focus();
                }
                else if (this.DDL_Webseite.SelectedIndex < 0)
                {
                    MessageBox.Show(TXT.TXT_Description("Webseite wählen"));
                    this.DDL_Webseite.Focus();
                }
                else
                {
                    if (this.Profil_Check())
                    {
                        var result = MessageBox.Show(
                            TXT.TXT_Description("Der Kanal ist bereits vorhanden. Möchten Sie den Kanal noch hinzufügen?"),
                            TXT.TXT_Description("Kanal erfassen"),
                            MessageBoxButtons.YesNo
                        );
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }

                    var websiteItem = this.DDL_Webseite.Items[this.DDL_Webseite.SelectedIndex] as ComboboxItem;
                    int websiteValue = (websiteItem != null) ? (int)websiteItem.Value : -1;

                    // Si no encontró canal y el usuario aún elige guardar, pedir confirmación:
                    if (!this.LAB_Kanal_Gefunden.Visible && !this.Model_Exist && websiteValue > -1)
                    {
                        var dialogRes = MessageBox.Show(
                            TXT.TXT_Description("Der Sender wurde nicht gefunden. Möchten sie die Einstellungen übernehmen?"),
                            TXT.TXT_Description("Sender nicht gefunden"),
                            MessageBoxButtons.YesNoCancel
                        );
                        if (dialogRes == DialogResult.Cancel) return;
                        if (dialogRes == DialogResult.No)
                        {
                            this.DialogResult = DialogResult.Cancel;
                            return;
                        }
                    }

                    // Si no hay carpeta seleccionada:
                    string defaultDir = Modul_Ordner.Ordner_Pfad() + "\\";
                    if (websiteValue < 0 && this.TXB_Folder.Text.Trim() == defaultDir)
                    {
                        this.TXB_Folder.Text = defaultDir + this.TXB_Sender_Name.Text.Trim();
                    }

                    if (this.TXB_Folder.Text.Trim() == defaultDir)
                    {
                        MessageBox.Show(TXT.TXT_Description("Geben sie den Ordner für die Aufnahmen an."));
                        this.PVP_Einstellungen.SelectedTab = this.PVP_Aufnahme;
                        this.TXB_Folder.Focus();
                    }
                    else
                    {
                        // Actualizar fila de DataSet
                        if (this.DS_Model.Tables[0].Rows.Count == 1)
                        {
                            DataRow row = this.DS_Model.Tables[0].Rows[0];
                            row["User_Name"] = this.Model_Name;
                            row["Website_ID"] = websiteValue;
                            row["Benachrichtigung"] = this.CBX_Benachrichtigung.Checked;
                            row["User_Country"] = this.TXB_Land.Text;
                            row["User_Language"] = this.TXB_Sprachen.Text;
                            row["User_Record"] = this.CBX_Record.Checked;
                            row["User_Visible"] = this.CBX_Visible.Checked;
                            row["User_Favorite"] = this.CBX_Favoriten.Checked;

                            if (this.DTP_Geburtstag.Value != DateTime.MinValue)
                                row["User_Birthday"] = this.DTP_Geburtstag.Value;

                            if (this.RBT_Geschlecht_Weiblich.Checked)
                                row["User_Gender"] = 0;
                            else if (this.RBT_Geschlecht_Männlich.Checked)
                                row["User_Gender"] = 1;
                            else if (this.RBT_Geschlecht_Paar.Checked)
                                row["User_Gender"] = 2;
                            else if (this.RBT_Geschlecht_Trans.Checked)
                                row["User_Gender"] = 3;
                            else if (this.RBT_Geschlecht_Sonstiges.Checked)
                                row["User_Gender"] = 4;

                            if (this.RBT_Aufnahmestop_NoStop.Checked)
                                row["Aufnahmestop_Auswahl"] = 0;
                            else if (this.RBT_Aufnahmestop_Time.Checked)
                                row["Aufnahmestop_Auswahl"] = 1;
                            else if (this.RBT_Aufnahmestop_Size.Checked)
                                row["Aufnahmestop_Auswahl"] = 2;

                            row["Aufnahmestop_Größe"] = this.SPE_FileSize.Value;
                            row["Aufnahmestop_Minuten"] = this.SPE_Recordtime.Value;

                            if (this.RBT_Video_Send.Checked)
                                row["Videoqualität"] = 0;
                            else if (this.RBT_Video_Minimal.Checked)
                                row["Videoqualität"] = 1;
                            else if (this.RBT_Video_SD.Checked)
                                row["Videoqualität"] = 2;
                            else if (this.RBT_Video_HD.Checked)
                                row["Videoqualität"] = 3;
                            else if (this.RBT_Video_FullHD.Checked)
                                row["Videoqualität"] = 4;

                            // Encoder seleccionado
                            var encItem = this.DDL_Video_Encoder.Items[this.DDL_Video_Encoder.SelectedIndex] as ComboboxItem;
                            if (encItem != null)
                                row["Recorder_ID"] = encItem.Value;

                            // Formato de guardado
                            var fmtItem = this.DDL_Speicherformat.Items[this.DDL_Speicherformat.SelectedIndex] as ComboboxItem;
                            if (fmtItem != null)
                                row["SaveFormat"] = fmtItem.Value;

                            row["User_Description"] = this.TXB_Name.Text;
                            row["User_Directory"] = this.TXB_Folder.Text;

                            this.DA_Model.Update(this.DS_Model.Tables[0]);

                            // Actualizar objeto Class_Model en memoria
                            Class_Model Model_Class = Class_Model_List.Class_Model_Find(this.Pri_Model_GUID).Result;
                            if (Model_Class == null)
                            {
                                Class_Model New_Model = new Class_Model(this.Pri_Model_GUID);
                                Class_Model_List.Model_Add(New_Model);
                                Model_Class = New_Model;
                                Model_Class.Model_Online_Change += new Class_Model.Model_Online_ChangeEventHandler(Form_Main.Model_Online_Change);
                                Model_Class.Pro_Last_Online = DateTime.Now;
                                Model_Class.Model_Stream_Adressen_Load();
                                Model_Class.Timer_Online_Change.Pro_Timer_Intervall = 15000;
                                Model_Class.Timer_Online_Change?.Check_Run();
                            }
                            else
                            {
                                Model_Class.Pro_Model_Name = row["User_Name"].ToString();
                                Model_Class.Pro_Website_ID = Convert.ToInt32(row["Website_ID"]);
                                Model_Class.Pro_Model_Visible = Convert.ToBoolean(row["User_Visible"]);
                                Model_Class.Pro_Model_Favorite = Convert.ToBoolean(row["User_Favorite"]);
                                Model_Class.Pro_Model_Record = Convert.ToBoolean(row["User_Record"]);
                                Model_Class.Pro_Aufnahme_Stop_Auswahl = Convert.ToInt32(row["Aufnahmestop_Auswahl"]);
                                Model_Class.Pro_Aufnahme_Stop_Minuten = Convert.ToInt32(row["Aufnahmestop_Minuten"]);
                                Model_Class.Pro_Aufnahme_Stop_Größe = Convert.ToInt32(row["Aufnahmestop_Größe"]);
                                Model_Class.Pro_Videoqualität = Convert.ToInt32(row["Videoqualität"]);
                                Model_Class.Pro_Benachrichtigung = Convert.ToBoolean(row["Benachrichtigung"]);
                                Model_Class.Pro_Decoder = Convert.ToInt32(row["Recorder_ID"]);
                                Model_Class.Pro_Model_Description = row["User_Description"].ToString();
                                Model_Class.Pro_Model_Directory = row["User_Directory"].ToString();
                                Model_Class.Pro_Model_Gender = Convert.ToInt32(row["User_Gender"]);
                                Model_Class.Priv_SaveFormat = Convert.ToInt32(row["SaveFormat"]);
                                Model_Class.Model_Stream_Adressen_Load();
                                Model_Class.Timer_Online_Change?.Check_Run();
                                Model_Class.Model_Online_Changed();
                            }

                            if (Model_Class.Get_Pro_Model_Online(true))
                            {
                                Form_Main.Model_Online_Change(Model_Class);
                            }
                        }

                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.BTN_Übernehmen_Click");
                this.DialogResult = DialogResult.Abort;
            }
        }

        private void TXB_Sender_Name_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // Si presiona Enter o Ctrl+V => lanzar búsqueda automática
                if (e.KeyCode == Keys.Enter || (e.Modifiers == Keys.Control && e.KeyCode == Keys.V))
                {
                    this.TXB_Sender_Name.KeyUp -= new KeyEventHandler(this.TXB_Sender_Name_KeyUp);
                    this.TXB_Sender_Name_TextChanged(this.AutoSearch);
                    this.TXB_Sender_Name.KeyUp += new KeyEventHandler(this.TXB_Sender_Name_KeyUp);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.TXB_Sender_Name_KeyUp");
            }
        }

        private void BTN_Folder_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = TXT.TXT_Description("Geben Sie das Kanalverzeichnis für die Aufnahmen an");
                    folderBrowserDialog.SelectedPath = Modul_Ordner.Ordner_Pfad();

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        this.TXB_Folder.Text = folderBrowserDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.BTN_Folder_Click");
            }
        }

        private void RadButtonElement1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TXB_Sender_Name.Text.Length <= 0) return;
                this.RadButtonElement1.Click -= new EventHandler(this.RadButtonElement1_Click);
                this.TXB_Sender_Name_TextChanged(true);
                this.RadButtonElement1.Click += new EventHandler(this.RadButtonElement1_Click);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.RadButtonElement1_Click");
            }
        }

        /// <summary>
        /// Inicia la búsqueda de perfil(es) de modelo según la URL o nombre introducido.
        /// </summary>
        private void TXB_Sender_Name_TextChanged(bool AutoSearchRun)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Profil_List.Clear();
            this.TXB_Land.Text = "";
            this.TXB_Sprachen.Text = "";
            this.DTP_Geburtstag.Value = DateTime.MinValue;
            this.LAB_Image.Image = null;
            this.LAB_Kanal_Gefunden.Visible = false;

            // Si hay pestaña de búsqueda previa, la removemos:
            foreach (TabPage tab in this.PVP_Einstellungen.TabPages)
            {
                if (tab.Name == "PVP_Suche")
                {
                    this.PVP_Einstellungen.TabPages.Remove(tab);
                    break;
                }
            }

            try
            {
                if (this.TXB_Sender_Name.Text.Length > 0)
                {
                    if (!this.TXB_Sender_Name.Text.StartsWith("https://"))
                    {
                        this.Model_Name = this.TXB_Sender_Name.Text;
                        var _ = this.Profil_Search().Result; // Ejecutar búsqueda asíncrona
                    }
                    else
                    {
                        (this.Model_Name, this.Model_WebSite_ID) = Sites.Site_Model_URL_Read(this.TXB_Sender_Name.Text);
                        if (this.Model_WebSite_ID == -1)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(TXT.TXT_Description("Die Webseite wird nicht unterstützt."));
                            return;
                        }
                        if (this.Model_WebSite_ID > -1)
                        {
                            if (!string.IsNullOrEmpty(this.Model_Name))
                            {
                                this.DDL_Webseite.SelectedIndexChanged -= new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
                                for (int i = 0; i < this.DDL_Webseite.Items.Count; i++)
                                {
                                    var itm = this.DDL_Webseite.Items[i] as ComboboxItem;
                                    if (itm != null && (int)itm.Value == this.Model_WebSite_ID)
                                    {
                                        this.DDL_Webseite.SelectedIndex = i;
                                        break;
                                    }
                                }
                                this.DDL_Webseite.SelectedIndexChanged += new EventHandler(this.DDL_Webseite_SelectedIndexChanged);

                                var _ = this.Profil_Search().Result;
                            }
                        }
                    }
                }
                else
                {
                    // Si campo vacío, limpiar datos
                    this.TXB_Land.Text = "";
                    this.TXB_Sprachen.Text = "";
                    this.DTP_Geburtstag.Value = DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.TXB_Sender_Name_TextChanged");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DDL_Webseite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DDL_Webseite.SelectedIndexChanged -= new EventHandler(this.DDL_Webseite_SelectedIndexChanged);

                var item = this.DDL_Webseite.Items[this.DDL_Webseite.SelectedIndex] as ComboboxItem;
                this.Model_WebSite_ID = (item != null) ? (int)item.Value : -1;

                if (this.TXB_Sender_Name.Text.Length > 0 && string.IsNullOrEmpty(this.Model_Name))
                {
                    this.Model_Name = this.TXB_Sender_Name.Text;
                }

                if (!string.IsNullOrEmpty(this.Model_Name) && (this.Profil_Info == null || this.Profil_Info.Pro_Website_ID != this.Model_WebSite_ID))
                {
                    var _ = this.Profil_Search().Result;
                }

                this.DDL_Webseite.SelectedIndexChanged += new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.DDL_Webseite_SelectedIndexChanged");
            }
        }

        /// <summary>
        /// Verifica en la base si ya existe un canal con ese nombre (distinto GUID).
        /// </summary>
        private bool Profil_Check()
        {
            bool flag = false;
            try
            {
                if (this.Pri_Model_New)
                {
                    using (OleDbConnection oleDbConnection = new OleDbConnection())
                    {
                        oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                        int websiteValue = (this.DDL_Webseite.SelectedIndex >= 0)
                            ? (int)((ComboboxItem)this.DDL_Webseite.Items[this.DDL_Webseite.SelectedIndex]).Value
                            : -1;

                        string query =
                            "SELECT * FROM DT_User WHERE User_Name = '" +
                            this.Model_Name + "' AND User_GUID <> '" +
                            this.Pri_Model_GUID.ToString() + "' AND Website_ID = " +
                            websiteValue;

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection.ConnectionString))
                        {
                            using (new OleDbCommandBuilder(adapter))
                            {
                                using (DataSet dataSet = new DataSet())
                                {
                                    oleDbConnection.Open();
                                    if (oleDbConnection.State == ConnectionState.Open)
                                    {
                                        adapter.Fill(dataSet, "DT_Model");
                                        oleDbConnection.Close();
                                        if (dataSet.Tables["DT_Model"].Rows.Count > 0)
                                        {
                                            flag = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Profil_Check");
            }
            return flag;
        }

        /// <summary>
        /// Realiza la búsqueda del canal en todos los sitios configurados (asíncrono).
        /// </summary>
        private async Task<Channel_Info> Profil_Search()
        {
            try
            {
                this.Search_Run = true;

                // Limpiar resultados anteriores
                if (this.PVP_Einstellungen.TabPages.Count > 2)
                {
                    this.PVP_Einstellungen.TabPages.RemoveAt(2);
                }

                this.Profil_List = new List<Channel_Info>();
                this.RadProgressBar1.Maximum = Sites.Website_List.Count + 1;
                this.RadProgressBar1.Visible = true;
                this.RadProgressBar1.Value = 0;
                Application.DoEvents();

                // Iterar sobre cada sitio configurado
                for (int i = 0; i < Sites.Website_List.Count; i++)
                {
                    Channel_Info result = await Sites.Website_List[i].Profil(this.Model_Name);
                    if (result.Pro_Exist)
                    {
                        this.Profil_List.Add(result);
                    }
                    this.RadProgressBar1.Value = i + 1;
                    Application.DoEvents();
                }

                this.RadProgressBar1.Visible = false;
                this.RadProgressBar1.Value = 0;
                this.Search_Run = false;

                if (this.Profil_List.Count > 1)
                {
                    // Mostrar pestaña de selección
                    TabPage tabBuscar = new TabPage
                    {
                        Name = "PVP_Suche",
                        Text = TXT.TXT_Description("Suche") + " (" + this.Profil_List.Count + ")",
                        AutoScroll = true
                    };
                    Panel radPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Padding = new Padding(4),
                        AutoScroll = true
                    };

                    foreach (Channel_Info profil in this.Profil_List)
                    {
                        Control_Broadcaster_Suche broadcasterSuche = new Control_Broadcaster_Suche(profil)
                        {
                            Dock = DockStyle.Top
                        };
                        broadcasterSuche.Such_Item_Accept += new Control_Broadcaster_Suche.Such_Item_AcceptEventHandler(this.Suche_Accept);
                        radPanel.Controls.Add(broadcasterSuche);
                    }

                    tabBuscar.Controls.Add(radPanel);
                    this.PVP_Einstellungen.TabPages.Add(tabBuscar);
                    this.PVP_Einstellungen.SelectedTab = tabBuscar;
                    return null;
                }
                else if (this.Profil_List.Count == 1)
                {
                    this.Search_Run = false;
                    return this.Profil_List[0];
                }
                else
                {
                    // Ningún resultado
                    MessageBox.Show(TXT.TXT_Description("Der Sender wurde nicht gefunden"));
                    this.Search_Run = false;
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.RadProgressBar1.Visible = false;
                this.RadProgressBar1.Value = 0;
                this.Search_Run = false;
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Profil_Search");
                return null;
            }
        }

        /// <summary>
        /// Se ejecuta cuando el usuario selecciona un canal de la lista de búsqueda.
        /// </summary>
        private void Suche_Accept(Channel_Info Info_Selected)
        {
            try
            {
                this.Profil_Info = Info_Selected;
                this.Profil_Füllen(Info_Selected);
                this.PVP_Einstellungen.SelectedTab = this.PVP_Info;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Suche_Accept");
            }
        }

        /// <summary>
        /// Llena los campos con la información del canal encontrado.
        /// </summary>
        private void Profil_Füllen(Channel_Info Profil_Info)
        {
            try
            {
                this.TXB_Land.Text = "";
                this.DTP_Geburtstag.Value = DateTime.MinValue;
                this.TXB_Sprachen.Text = "";
                this.LAB_Kanal_Gefunden.Visible = Profil_Info?.Pro_Exist ?? false;
                this.LAB_Image.Image = null;

                if (Profil_Info.Pro_Exist)
                {
                    switch (Profil_Info.Pro_Gender)
                    {
                        case 1: this.RBT_Geschlecht_Weiblich.Checked = true; break;
                        case 2: this.RBT_Geschlecht_Männlich.Checked = true; break;
                        case 3: this.RBT_Geschlecht_Paar.Checked = true; break;
                        case 4: this.RBT_Geschlecht_Trans.Checked = true; break;
                        default: this.RBT_Geschlecht_Sonstiges.Checked = true; break;
                    }

                    this.TXB_Land.Text = Profil_Info.Pro_Country;
                    this.TXB_Sprachen.Text = Profil_Info.Pro_Languages;
                    this.TXB_Sender_Name.Text = Profil_Info.Pro_Name;
                    this.Model_Name = Profil_Info.Pro_Name;
                    this.TXB_Folder.Text = Modul_Ordner.Ordner_Pfad() + "\\" + Profil_Info.Pro_Name.Trim();

                    if (Profil_Info.Pro_Birthday != null &&
                        DateTime.TryParse(Profil_Info.Pro_Birthday.ToString(), out DateTime bd))
                    {
                        this.DTP_Geburtstag.Value = bd;
                    }

                    // Seleccionar en DDL_Webseite
                    for (int i = 0; i < this.DDL_Webseite.Items.Count; i++)
                    {
                        var itm = this.DDL_Webseite.Items[i] as ComboboxItem;
                        if (itm != null && (int)itm.Value == Profil_Info.Pro_Website_ID)
                        {
                            this.DDL_Webseite.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (Profil_Info.Pro_Profil_Image != null)
                {
                    this.LAB_Image.Image = Profil_Info.Pro_Profil_Image;
                }
                else
                {
                    this.LAB_Image.Image = null;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Profil_Füllen");
            }
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Clipboard.ContainsText(TextDataFormat.Text)) return;
                this.TXB_Sender_Name.Text = Clipboard.GetText(TextDataFormat.Text);
                if (this.TXB_Sender_Name.Text.Length > 0)
                {
                    this.TXB_Sender_Name_TextChanged(this.AutoSearch);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Paste_Click");
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TXB_Sender_Name.Text)) return;
                if (this.TXB_Sender_Name.SelectionLength > 0)
                {
                    this.TXB_Sender_Name.Copy();
                }
                else
                {
                    this.TXB_Sender_Name.SelectAll();
                    this.TXB_Sender_Name.Copy();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Copy_Click");
            }
        }
    }

    /// <summary>
    /// Clase auxiliar para representar items en ComboBox con Text/Value.
    /// </summary>
    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public override string ToString() => Text;
    }
}
