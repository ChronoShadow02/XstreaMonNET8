using System.Data;
using System.Data.OleDb;
using static XstreaMonNET8.Control_Stream;

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

        // Custom class to hold Text and Value for ComboBox items, similar to RadListDataItem
        // This class is defined here to keep it within the scope of the Dialog_Model_Einstellungen class
        // as it's specifically used for its ComboBoxes.
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public Image Image { get; set; } // Added for image support

            public override string ToString()
            {
                return Text;
            }
        }

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
            this.InitializeComponent();
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
            this.InitializeComponent();
            this.Form_Initalize();
            this.Pri_Model_GUID = Model_GUID;
            this.Database_Set(this.Pri_Model_GUID);
            this.TXB_Sender_Name.Text = Model_URL;
            this.TXB_Sender_Name_TextChanged(this.AutoSearch);
        }

        private void Form_Initalize()
        {
            try
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(TXT.TXT_Description("Kopieren"), Resources.Kopieren16);
                toolStripMenuItem1.Click += new EventHandler(this.Copy_Click);
                contextMenuStrip.Items.Add(toolStripMenuItem1);
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(TXT.TXT_Description("Einfügen"), Resources.Einfügen16);
                toolStripMenuItem2.Click += new EventHandler(this.Paste_Click);
                contextMenuStrip.Items.Add(toolStripMenuItem2);
                this.TXB_Sender_Name.ContextMenuStrip = contextMenuStrip;

                this.DDL_Webseite.Items.Clear();
                this.DDL_Webseite.Items.Add(new ComboBoxItem()
                {
                    Text = TXT.TXT_Description("Webseite wählen"),
                    Value = -1
                });

                // Set DisplayMember and ValueMember for the ComboBox
                this.DDL_Webseite.DisplayMember = "Text";
                this.DDL_Webseite.ValueMember = "Value";

                try
                {
                    foreach (Class_Website website in Sites.Website_List)
                    {
                        this.DDL_Webseite.Items.Add(new ComboBoxItem()
                        {
                            Text = website.Pro_Name,
                            Value = website.Pro_ID,
                            Image = new Bitmap(website.Pro_Image, 16, 16)
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Handle specific exceptions if necessary
                    Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Form_Initalize - DDL_Webseite population");
                }

                this.DDL_Webseite.SelectedItem = null;
                this.DDL_Webseite.AutoCompleteMode = AutoCompleteMode.None;
                this.DDL_Webseite.AutoCompleteSource = AutoCompleteSource.ListItems;

                this.DDL_Video_Encoder.Items.Clear();
                this.DDL_Video_Encoder.DisplayMember = "Text";
                this.DDL_Video_Encoder.ValueMember = "Value";
                try
                {
                    foreach (Class_Decoder_Item decoderItem in Decoder.Decoder_Item_List)
                    {
                        this.DDL_Video_Encoder.Items.Add(new ComboBoxItem()
                        {
                            Text = decoderItem.Decoder_Name,
                            Value = decoderItem.Decoder_ID
                        });
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Form_Initalize - DDL_Video_Encoder population");
                }

                this.DDL_Speicherformat.Items.Clear();
                this.DDL_Speicherformat.DisplayMember = "Text";
                this.DDL_Speicherformat.ValueMember = "Value";
                try
                {
                    foreach (Class_Speicherformate speicherformateItem in Speicherformate.Speicherformate_Item_List)
                    {
                        this.DDL_Speicherformat.Items.Add(new ComboBoxItem()
                        {
                            Text = speicherformateItem.Pro_Speicherformat_Name,
                            Value = speicherformateItem.Pro_ID
                        });
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Form_Initalize - DDL_Speicherformat population");
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Load");
            }
        }

        private void Database_Set(Guid Model_GUID)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection())
            {
                oleDbConnection.ConnectionString = Database_Connect.Aktiv_Datenbank();
                this.DA_Model = new OleDbDataAdapter(" SELECT * FROM DT_User Where User_GUID = '" + Model_GUID.ToString() + "'", oleDbConnection.ConnectionString);
                this.CB_Model = new OleDbCommandBuilder(this.DA_Model);
                this.DS_Model = new DataSet();
                oleDbConnection.Open();
                if (oleDbConnection.State != ConnectionState.Open)
                    return;
                this.DA_Model.Fill(this.DS_Model, "DT_Model");
                oleDbConnection.Close();
                if (this.DS_Model.Tables[0].Rows.Count == 0)
                {
                    this.Text = TXT.TXT_Description("Neuen Kanal hinzufügen");
                    DataRow row = this.DS_Model.Tables[0].NewRow();
                    row["User_GUID"] = Model_GUID;
                    row["Benachrichtigung"] = Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Optionen", "Notification", true.ToString()));
                    row["Aufnahmestop_Auswahl"] = Value_Back.get_CInteger(INI_File.Read(Parameter.INI_Common, "Record", "RecordStop", 1.ToString()));
                    row["Aufnahmestop_Größe"] = Value_Back.get_CInteger(INI_File.Read(Parameter.INI_Common, "Record", "RecordSize", 0.ToString()));
                    row["Aufnahmestop_Minuten"] = Value_Back.get_CInteger(INI_File.Read(Parameter.INI_Common, "Record", "RecordTime", 30.ToString()));
                    row["Videoqualität"] = Value_Back.get_CInteger(INI_File.Read(Parameter.INI_Common, "Record", "Resolution", 0.ToString()));
                    row["User_Visible"] = Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Optionen", "Visible", true.ToString()));
                    row["User_Record"] = Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Optionen", "Record", false.ToString()));
                    row["Last_Online"] = DateTime.Now;
                    row["Website_ID"] = -1;
                    row["User_Gender"] = 0;
                    row["User_Favorite"] = 0;
                    row["Recorder_ID"] = Value_Back.get_CInteger(INI_File.Read(Parameter.INI_Common, "Record", "Encoder", 0.ToString()));
                    row["SaveFormat"] = Value_Back.get_CInteger(INI_File.Read(Parameter.INI_Common, "Record", "SaveFormat", 0.ToString()));
                    row["User_Create"] = DateTime.Now;
                    this.DS_Model.Tables[0].Rows.Add(row);
                    this.Pri_Model_New = true;
                }
                else
                {
                    this.Model_Exist = true;
                    this.Text = TXT.TXT_Description("Kanal bearbeiten");
                    this.TXB_Sender_Name.ReadOnly = true;
                }
                if (this.DS_Model.Tables[0].Rows.Count != 1)
                    return;
                DataRow row1 = this.DS_Model.Tables[0].Rows[0];
                this.TXB_Sender_Name.Text = row1["User_Name"].ToString();
                this.Model_Name = row1["User_Name"].ToString();
                this.CBX_Benachrichtigung.Checked = Value_Back.get_CBoolean(row1["Benachrichtigung"]);
                this.CBX_Favoriten.Checked = Value_Back.get_CBoolean(row1["User_Favorite"]);
                this.CBX_Record.Checked = Value_Back.get_CBoolean(row1["User_Record"]);
                this.CBX_Visible.Checked = Value_Back.get_CBoolean(row1["User_Visible"]);

                this.DDL_Webseite.SelectedIndexChanged -= new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
                // Find the ComboBoxItem with the matching Value
                this.DDL_Webseite.SelectedItem = this.DDL_Webseite.Items.Cast<ComboBoxItem>().FirstOrDefault(item => (int)item.Value == Value_Back.get_CInteger(row1["Website_ID"]));
                this.DDL_Webseite.SelectedIndexChanged += new EventHandler(this.DDL_Webseite_SelectedIndexChanged);

                this.TXB_Land.Text = row1["User_Country"].ToString();
                this.TXB_Sprachen.Text = row1["User_Language"].ToString();
                if (row1["User_Birthday"].ToString().Length > 0)
                    this.DTP_Geburtstag.Value = Convert.ToDateTime(row1["User_Birthday"]);
                switch (Value_Back.get_CInteger(row1["User_Gender"]))
                {
                    case 0:
                        this.RBT_Geschlecht_Weiblich.Checked = true;
                        break;
                    case 1:
                        this.RBT_Geschlecht_Männlich.Checked = true;
                        break;
                    case 2:
                        this.RBT_Geschlecht_Paar.Checked = true;
                        break;
                    case 3:
                        this.RBT_Geschlecht_Trans.Checked = true;
                        break;
                    case 4:
                        this.RBT_Geschlecht_Sonstiges.Checked = true;
                        break;
                }
                switch (Value_Back.get_CInteger(row1["Aufnahmestop_Auswahl"]))
                {
                    case 0:
                        this.RBT_Aufnahmestop_NoStop.Checked = true;
                        break;
                    case 1:
                        this.RBT_Aufnahmestop_Time.Checked = true;
                        break;
                    case 2:
                        this.RBT_Aufnahmestop_Size.Checked = true;
                        break;
                }
                this.SPE_FileSize.Value = Convert.ToDecimal(row1["Aufnahmestop_Größe"]);
                this.SPE_Recordtime.Value = Convert.ToDecimal(row1["Aufnahmestop_Minuten"]);
                switch (Value_Back.get_CInteger(row1["Videoqualität"]))
                {
                    case 0:
                        this.RBT_Video_Send.Checked = true;
                        break;
                    case 1:
                        this.RBT_Video_Minimal.Checked = true;
                        break;
                    case 2:
                        this.RBT_Video_SD.Checked = true;
                        break;
                    case 3:
                        this.RBT_Video_HD.Checked = true;
                        break;
                    case 4:
                        this.RBT_Video_FullHD.Checked = true;
                        break;
                }
                this.DDL_Video_Encoder.SelectedItem = this.DDL_Video_Encoder.Items.Cast<ComboBoxItem>().FirstOrDefault(item => (int)item.Value == Value_Back.get_CInteger(row1["Recorder_ID"]));
                this.DDL_Speicherformat.SelectedItem = this.DDL_Speicherformat.Items.Cast<ComboBoxItem>().FirstOrDefault(item => (int)item.Value == Value_Back.get_CInteger(row1["SaveFormat"]));
                this.TXB_Name.Text = row1["User_Description"].ToString();

                this.TXB_Folder.Text = row1["User_Directory"].ToString().Length == 0 ?
                                        (Modul_Ordner.Ordner_Pfad() + "\\" + row1["User_Name"]).Trim() :
                                        row1["User_Directory"].ToString().Trim();
            }
        }

        private void Dialog_Model_Einstellungen_Load(object sender, EventArgs e)
        {
            try
            {
                TXT.Control_Languages(this);
                this.TXB_Sender_Name.Text = TXT.TXT_Description("Nombre del modelo o URL");
                this.DDL_Webseite.Text = TXT.TXT_Description("Seleccionar sitio web");

                this.AutoSearch = Value_Back.get_CBoolean(INI_File.Read(Parameter.INI_Common, "Optionen", "Search", false.ToString()));
                if (Sites.Website_Find(Value_Back.get_CInteger(this.DDL_Webseite.SelectedValue)) == null)
                    return;
                this.LAB_Kanal_Gefunden.Visible = this.Profil_Info?.Pro_Exist ?? false;
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
                if (this.TXB_Sender_Name.Text.Length == 0)
                {
                    MessageBox.Show(TXT.TXT_Description("Modelname eingeben"));
                    this.TXB_Sender_Name.Focus();
                }
                else if (this.DDL_Webseite.SelectedValue == null)
                {
                    MessageBox.Show(TXT.TXT_Description("Webseite wählen"));
                    this.DDL_Webseite.Focus();
                }
                else
                {
                    if (this.Profil_Check() && MessageBox.Show(TXT.TXT_Description("Der Kanal ist bereits vorhanden. Möchten Sie den Kanal noch hinzufügen?"), TXT.TXT_Description("Kanal erfassen"), MessageBoxButtons.YesNo) == DialogResult.No)
                        return;

                    // Get selected website ID safely
                    int selectedWebsiteId = -1;
                    if (this.DDL_Webseite.SelectedItem is ComboBoxItem selectedItem)
                    {
                        selectedWebsiteId = Value_Back.get_CInteger(selectedItem.Value);
                    }

                    if (!this.LAB_Kanal_Gefunden.Visible && !this.Model_Exist && selectedWebsiteId > -1)
                    {
                        DialogResult msgResult = MessageBox.Show(TXT.TXT_Description("Der Sender wurde nicht gefunden. Möchten sie die Einstellungen übernehmen?"), TXT.TXT_Description("Sender nicht gefunden"), MessageBoxButtons.YesNoCancel);
                        switch (msgResult)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.No:
                                this.DialogResult = DialogResult.Cancel;
                                return;
                        }
                    }

                    if (selectedWebsiteId == -1 && this.TXB_Folder.Text == Modul_Ordner.Ordner_Pfad() + "\\")
                        this.TXB_Folder.Text = Modul_Ordner.Ordner_Pfad() + "\\" + this.TXB_Sender_Name.Text;

                    if (this.TXB_Folder.Text == Modul_Ordner.Ordner_Pfad() + "\\")
                    {
                        MessageBox.Show(TXT.TXT_Description("Geben sie den Ordner für die Aufnahmen an."));
                        this.PVP_Einstellungen.SelectedTab = this.PVP_Aufnahme;
                        this.TXB_Folder.Focus();
                    }
                    else
                    {
                        if (this.DS_Model.Tables[0].Rows.Count == 1)
                        {
                            DataRow row = this.DS_Model.Tables[0].Rows[0];
                            row["User_Name"] = this.Model_Name;
                            row["Website_ID"] = selectedWebsiteId;
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

                            row["Recorder_ID"] = Value_Back.get_CInteger(((ComboBoxItem)this.DDL_Video_Encoder.SelectedItem)?.Value);
                            row["SaveFormat"] = Value_Back.get_CInteger(((ComboBoxItem)this.DDL_Speicherformat.SelectedItem)?.Value);
                            row["User_Description"] = this.TXB_Name.Text;
                            row["User_Directory"] = this.TXB_Folder.Text;

                            this.DA_Model.Update(this.DS_Model.Tables[0]);

                            Class_Model Model_Class = Class_Model_List.Class_Model_Find(this.Pri_Model_GUID).Result;
                            if (Model_Class == null)
                            {
                                Class_Model New_Model = new Class_Model(this.Pri_Model_GUID);
                                Class_Model_List.Model_Add(New_Model);
                                Model_Class = New_Model;
                                // Assuming Form_Main and its Model_Online_Change method are accessible
                                // and Model_Online_ChangeEventHandler is defined in Class_Model
                                // Model_Class.Model_Online_Change += Form_Main.Model_Online_Change; // Direct reference
                                Model_Class.Pro_Last_Online = DateTime.Now;
                                Model_Class.Model_Stream_Adressen_Load();
                                Model_Class.Timer_Online_Change.Pro_Timer_Intervall = 15000;
                                Model_Class.Timer_Online_Change?.Check_Run();
                            }
                            else
                            {
                                Model_Class.Pro_Model_Name = this.DS_Model.Tables[0].Rows[0]["User_Name"].ToString();
                                Model_Class.Pro_Website_ID = Convert.ToInt32(this.DS_Model.Tables[0].Rows[0]["Website_ID"]);
                                Model_Class.Pro_Model_Visible = Convert.ToBoolean(this.DS_Model.Tables[0].Rows[0]["User_Visible"]);
                                Model_Class.Pro_Model_Favorite = Convert.ToBoolean(this.DS_Model.Tables[0].Rows[0]["User_Favorite"]);
                                Model_Class.Pro_Model_Record = Convert.ToBoolean(this.DS_Model.Tables[0].Rows[0]["User_Record"]);
                                Model_Class.Pro_Aufnahme_Stop_Auswahl = Value_Back.get_CInteger(this.DS_Model.Tables[0].Rows[0]["Aufnahmestop_Auswahl"]);
                                Model_Class.Pro_Aufnahme_Stop_Minuten = Value_Back.get_CInteger(this.DS_Model.Tables[0].Rows[0]["Aufnahmestop_Minuten"]);
                                Model_Class.Pro_Aufnahme_Stop_Größe = Value_Back.get_CInteger(this.DS_Model.Tables[0].Rows[0]["Aufnahmestop_Größe"]);
                                Model_Class.Pro_Videoqualität = Value_Back.get_CInteger(this.DS_Model.Tables[0].Rows[0]["Videoqualität"]);
                                Model_Class.Pro_Benachrichtigung = Value_Back.get_CBoolean(this.DS_Model.Tables[0].Rows[0]["Benachrichtigung"]);
                                Model_Class.Pro_Decoder = Value_Back.get_CInteger(this.DS_Model.Tables[0].Rows[0]["Recorder_ID"]);
                                Model_Class.Pro_Model_Description = this.DS_Model.Tables[0].Rows[0]["User_Description"].ToString();
                                Model_Class.Pro_Model_Directory = this.DS_Model.Tables[0].Rows[0]["User_Directory"].ToString();
                                Model_Class.Pro_Model_Gender = Value_Back.get_CInteger(this.DS_Model.Tables[0].Rows[0]["User_Gender"]);
                                Model_Class.Priv_SaveFormat = Value_Back.get_CInteger(this.DS_Model.Tables[0].Rows[0]["SaveFormat"]);
                                Model_Class.Model_Stream_Adressen_Load();
                                Model_Class.Timer_Online_Change?.Check_Run();
                                Model_Class.Model_Online_Changed();
                            }
                            // Assuming Form_Main and its Model_Online_Change method are accessible
                            // if (Model_Class.get_Pro_Model_Online(true))
                            //     Form_Main.Model_Online_Change(Model_Class);
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
                // Check for Enter key or Ctrl+V
                if ((e.KeyCode == Keys.Enter) || (e.Control && e.KeyCode == Keys.V))
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
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
                {
                    Description = TXT.TXT_Description("Geben Sie das Kanalverzeichnis für die Aufnahmen an"),
                    SelectedPath = Modul_Ordner.Ordner_Pfad()
                };
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    return;
                this.TXB_Folder.Text = folderBrowserDialog.SelectedPath;
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
                if (this.TXB_Sender_Name.Text.Length <= 0)
                    return;
                this.TXB_Sender_Name_TextChanged(true);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.RadButtonElement1_Click");
            }
        }

        private void TXB_Sender_Name_TextChanged(bool AutosearchRun)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Profil_List.Clear();
            this.TXB_Land.Text = "";
            this.TXB_Sprachen.Text = "";
            this.DTP_Geburtstag.Value = this.DTP_Geburtstag.MinDate;
            this.LAB_Image.Image = null;
            this.LAB_Kanal_Gefunden.Visible = false;

            // Remove extra tab pages if they exist (e.g., search results page)
            if (this.PVP_Einstellungen.TabPages.Count > 2)
            {
                // Iterate backwards to avoid issues with collection modification
                for (int i = this.PVP_Einstellungen.TabPages.Count - 1; i >= 2; i--)
                {
                    this.PVP_Einstellungen.TabPages.RemoveAt(i);
                }
            }

            try
            {
                if (this.TXB_Sender_Name.Text.Length > 0)
                {
                    if (!this.TXB_Sender_Name.Text.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Model_Name = this.TXB_Sender_Name.Text;
                        this.Profil_Load(AutosearchRun);
                    }
                    else
                    {
                        (this.Model_Name, this.Model_WebSite_ID) = Sites.Site_Model_URL_Read(this.TXB_Sender_Name.Text);
                        if (this.Model_WebSite_ID == -1)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(TXT.TXT_Description("El sitio web no es compatible."));
                            return;
                        }
                        if (this.Model_WebSite_ID > -1)
                        {
                            if (this.Model_Name.Length > 0)
                            {
                                this.DDL_Webseite.SelectedIndexChanged -= new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
                                // Select the ComboBoxItem with the matching Value
                                this.DDL_Webseite.SelectedItem = this.DDL_Webseite.Items.Cast<ComboBoxItem>().FirstOrDefault(item => (int)item.Value == this.Model_WebSite_ID);
                                this.DDL_Webseite.SelectedIndexChanged += new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
                                this.Profil_Load(AutosearchRun);
                            }
                        }
                    }
                }
                else
                {
                    this.TXB_Land.Text = "";
                    this.TXB_Sprachen.Text = "";
                    this.DTP_Geburtstag.Value = this.DTP_Geburtstag.MinDate;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.TXB_Sender_Name_TextChanged");
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }

        private void DDL_Webseite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DDL_Webseite.SelectedIndexChanged -= new EventHandler(this.DDL_Webseite_SelectedIndexChanged);

                // Safely get the selected value from the ComboBox
                int currentSelectedWebsiteId = -1;
                if (this.DDL_Webseite.SelectedItem is ComboBoxItem selectedItem)
                {
                    currentSelectedWebsiteId = Value_Back.get_CInteger(selectedItem.Value);
                }
                this.Model_WebSite_ID = currentSelectedWebsiteId;

                if (this.TXB_Sender_Name.Text.Length > 0 && string.IsNullOrEmpty(this.Model_Name))
                    this.Model_Name = this.TXB_Sender_Name.Text;

                // Check if Model_Name is not empty and if the selected website has changed or Profil_Info is null
                if (!string.IsNullOrEmpty(this.Model_Name) &&
                    (this.Profil_Info == null || this.Profil_Info.Pro_Website_ID != currentSelectedWebsiteId))
                {
                    this.Profil_Load(false);
                }
                this.DDL_Webseite.SelectedIndexChanged += new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.DDL_Webseite_SelectedIndexChanged");
            }
        }

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
                        int selectedWebsiteId = -1;
                        if (this.DDL_Webseite.SelectedItem is ComboBoxItem selectedItem)
                        {
                            selectedWebsiteId = Value_Back.get_CInteger(selectedItem.Value);
                        }

                        string query = $" SELECT * FROM DT_User  Where User_Name = '{this.Model_Name}' AND User_GUID <> '{this.Pri_Model_GUID}'  AND Website_ID = {selectedWebsiteId}";
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection.ConnectionString))
                        {
                            using (OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter))
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

        private async Task<Channel_Info> Profil_Search()
        {
            try
            {
                this.Search_Run = true;
                await Task.CompletedTask; // This line is for async/await structure, actual work is synchronous below

                // Remove any existing search result tab pages
                if (this.PVP_Einstellungen.TabPages.Count > 2)
                {
                    for (int i = this.PVP_Einstellungen.TabPages.Count - 1; i >= 2; i--)
                    {
                        this.PVP_Einstellungen.TabPages.RemoveAt(i);
                    }
                }

                this.Profil_List = new List<Channel_Info>();
                this.RadProgressBar1.Maximum = Sites.Website_List.Count;
                this.RadProgressBar1.Visible = true;
                this.RadProgressBar1.Value = 0;
                Application.DoEvents(); // Process UI events

                try
                {

                    // Site 0: Chaturbate
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "0", "True") == "True")
                    {
                        Channel_Info result = await Chaturbate.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 1: Camsoda
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "1", "True") == "True")
                    {
                        Channel_Info result = await Camsoda.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 2: Stripchat
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "2", "True") == "True")
                    {
                        Channel_Info result = await Stripchat.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 3: Bongacams
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "3", "True") == "True")
                    {
                        Channel_Info result = await Bongacams.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 4: Cam4
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "4", "True") == "True")
                    {
                        Channel_Info result = await Cam4.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 5: Streamate
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "5", "True") == "True")
                    {
                        Channel_Info result = await Streamate.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 6: Flirt4Free
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "6", "True") == "True")
                    {
                        Channel_Info result = await Flirt4Free.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 7: MyFreeCams
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "7", "True") == "True")
                    {
                        Channel_Info result = await MyFreeCams.Profil(this.Model_Name);
                        if (result.Pro_Exist)
                        {
                            this.RadProgressBar1.Value++;
                            this.Profil_List.Add(result);
                        }
                    }

                    // Site 8: Jerkmate
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "8", "True") == "True")
                    {
                        Channel_Info result = await Jerkmate.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 9: CamsCom
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "9", "True") == "True")
                    {
                        Channel_Info result = await CamsCom.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 10: Camster
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "10", "True") == "True")
                    {
                        Channel_Info result = await Camster.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 11: Freeoneslive
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "11", "True") == "True")
                    {
                        Channel_Info result = await Freeoneslive.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    // Site 12: EPlay
                    Application.DoEvents();
                    this.RadProgressBar1.Value++;
                    if (INI_File.Read(Parameter.INI_Common, "Sites", "12", "True") == "True")
                    {
                        Channel_Info result = await EPlay.Profil(this.Model_Name);
                        if (result.Pro_Exist) this.Profil_List.Add(result);
                    }

                    this.RadProgressBar1.Visible = false;
                    this.RadProgressBar1.Value = 0;
                    this.Search_Run = false;
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Profil_Search - Site Loop");
                    this.RadProgressBar1.Visible = false;
                    this.RadProgressBar1.Value = 0;
                    this.Search_Run = false;
                }

                if (this.Profil_List.Count > 1)
                {
                    TabPage tabPage = new TabPage();
                    tabPage.Location = new Point(10, 37);
                    tabPage.Name = "PVP_Suche";
                    tabPage.Size = new Size(440, 206);

                    Panel panel = new Panel();
                    panel.AutoScroll = true;
                    panel.Cursor = Cursors.Default;
                    panel.Dock = DockStyle.Fill;
                    panel.Location = new Point(0, 0);
                    panel.Name = "PAN_Sender";
                    panel.Padding = new Padding(4);
                    panel.Size = new Size(440, 206);
                    panel.TabIndex = 8;

                    tabPage.Text = TXT.TXT_Description("Suche") + " (" + this.Profil_List.Count + ")";

                    try
                    {
                        foreach (Channel_Info profil in this.Profil_List)
                        {
                            Control_Broadcaster_Suche broadcasterSuche = new Control_Broadcaster_Suche(profil);
                            broadcasterSuche.Dock = DockStyle.Top;
                            broadcasterSuche.Such_Item_Accept += new Control_Broadcaster_Suche.Such_Item_AcceptEventHandler(this.Suche_Accept);
                            panel.Controls.Add(broadcasterSuche);
                        }
                    }
                    catch (Exception ex)
                    {
                        Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Profil_Search - Add Broadcaster Controls");
                    }

                    tabPage.Controls.Add(panel);
                    this.PVP_Einstellungen.TabPages.Add(tabPage);
                    this.PVP_Einstellungen.SelectedTab = tabPage;
                    this.Search_Run = false;
                }
                else if (this.Profil_List.Count == 1)
                {
                    this.Search_Run = false;
                    return this.Profil_List[0];
                }
                else // Profil_List.Count == 0
                {
                    this.TXB_Sender_Name.KeyUp -= new KeyEventHandler(this.TXB_Sender_Name_KeyUp);
                    this.LAB_Image.Visible = false;
                    MessageBox.Show(TXT.TXT_Description("Der Sender wurde nicht gefunden"));
                    this.TXB_Sender_Name.KeyUp += new KeyEventHandler(this.TXB_Sender_Name_KeyUp);
                    this.Search_Run = false;
                }

                this.Search_Run = false;
                return null; // No profile found or multiple profiles handled in UI
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Profil_Search");
                this.Search_Run = false;
                return null;
            }
        }

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

        private void Profil_Load(bool AutoSearchStart)
        {
            try
            {
                if (AutoSearchStart && !this.Search_Run)
                {
                    this.Profil_Info = this.Profil_Search().Result; // Blocking call for simplicity, consider await
                }

                Class_Website classWebsite = Sites.Website_Find(this.Model_WebSite_ID);
                if (classWebsite != null)
                {
                    this.Profil_Info = classWebsite.Profil(this.Model_Name).Result; // Blocking call for simplicity, consider await
                }

                if (this.Profil_Info != null && this.Profil_Info.Pro_Exist)
                {
                    this.Profil_Füllen(this.Profil_Info);
                }
                else
                {
                    this.LAB_Kanal_Gefunden.Visible = this.Profil_Info?.Pro_Exist ?? false;
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Modeleinstellungen.Profil_Load");
            }
        }

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
                        case 1:
                            this.RBT_Geschlecht_Weiblich.Checked = true;
                            break;
                        case 2:
                            this.RBT_Geschlecht_Männlich.Checked = true;
                            break;
                        case 3:
                            this.RBT_Geschlecht_Paar.Checked = true;
                            break;
                        case 4:
                            this.RBT_Geschlecht_Trans.Checked = true;
                            break;
                        default:
                            this.RBT_Geschlecht_Sonstiges.Checked = true;
                            break;
                    }
                    this.TXB_Land.Text = Profil_Info.Pro_Country;
                    this.TXB_Sprachen.Text = Profil_Info.Pro_Languages;
                    this.TXB_Sender_Name.Text = Profil_Info.Pro_Name;
                    this.Model_Name = Profil_Info.Pro_Name;
                    this.TXB_Folder.Text = Modul_Ordner.Ordner_Pfad() + "\\" + Profil_Info.Pro_Name.Trim();

                    if (Profil_Info.Pro_Birthday != null)
                    {
                        // Safely convert to DateTime, checking if it's a valid date string
                        if (DateTime.TryParse(Profil_Info.Pro_Birthday.ToString(), out DateTime birthdayDate))
                        {
                            this.DTP_Geburtstag.Value = birthdayDate;
                        }
                        else
                        {
                            this.DTP_Geburtstag.Value = DateTime.MinValue; // Or handle invalid date as needed
                        }
                    }

                    this.DDL_Webseite.SelectedIndexChanged -= new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
                    // Select the ComboBoxItem with the matching Value
                    this.DDL_Webseite.SelectedItem = this.DDL_Webseite.Items.Cast<ComboBoxItem>().FirstOrDefault(item => (int)item.Value == Profil_Info.Pro_Website_ID);
                    this.DDL_Webseite.SelectedIndexChanged += new EventHandler(this.DDL_Webseite_SelectedIndexChanged);
                }
                if (Profil_Info.Pro_Profil_Image != null)
                    this.LAB_Image.BackgroundImage = Profil_Info.Pro_Profil_Image;
                else
                    this.LAB_Image.BackgroundImage = null;
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
                if (!Clipboard.ContainsText(TextDataFormat.Text))
                    return;
                this.TXB_Sender_Name.Text = Clipboard.GetText(TextDataFormat.Text);
                if (this.TXB_Sender_Name.Text.Length <= 0)
                    return;
                this.TXB_Sender_Name_TextChanged(this.AutoSearch);
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
                if (string.IsNullOrEmpty(this.TXB_Sender_Name.Text))
                    return;
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
}
