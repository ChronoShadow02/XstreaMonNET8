using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

#nullable disable
namespace XstreaMonNET8
{
    public partial class Dialog_Einstellungen : Form
    {

        public Dialog_Einstellungen()
        {
            this.Load += new EventHandler(this.Dialog_Einstellungen_Load);
        }

        private void BTN_Übernehmen_Click(object sender, EventArgs e)
        {
            try
            {
                // Update data path if it has changed
                string oldRecordsPath = IniFile.Read(Parameter.INI_Common, "Directory", "Records");
                string newRecordsPath = TXB_Aufnahmen.Text;
                if (!string.Equals(oldRecordsPath, newRecordsPath, StringComparison.Ordinal))
                {
                    DB_Datapath_Update(oldRecordsPath, newRecordsPath);
                }

                // Write basic settings
                IniFile.Write(Parameter.INI_Common, "Language", "Files", DDL_Languages.SelectedValue?.ToString());
                IniFile.Write(Parameter.INI_Common, "Directory", "Records", newRecordsPath);
                IniFile.Write(Parameter.INI_Common, "Directory", "Database", TXB_Datenbank.Text);
                IniFile.Write(Parameter.INI_Common, "Directory", "Favorites", TXB_Favoriten.Text);
                IniFile.Write(Parameter.INI_Common, "Lizenz", "LizenzValue", TXB_LizenzValue.Text);
                IniFile.Write(Parameter.INI_Common, "Browser", "Product", DDL_Browser.SelectedValue?.ToString() ?? "0");
                IniFile.Write(Parameter.INI_Common, "Tray", "Minimize", CBX_Minimize.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Favorite", "Copy", CBX_FavoritenRecords.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Notification", "Enabled", CBX_Notification.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Player", "Intern", CBX_Player.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Design", "Style", DDL_Design.SelectedValue?.ToString() ?? "0");
                IniFile.Write(Parameter.INI_Common, "Design", "Header", CBX_StreamHeader.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Debug", "Debug", CBX_Debug.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Online", "Check", CBX_Start_Check.Checked.ToString());

                // Recording settings
                IniFile.Write(Parameter.INI_Common, "Record", "MaxConversion", ((int)SPE_Conversion.Value).ToString());
                IniFile.Write(Parameter.INI_Common, "Record", "Encoder", DDL_Video_Encoder.SelectedValue?.ToString() ?? "0");
                IniFile.Write(Parameter.INI_Common, "Record", "Name", TXB_Dateiname.Text);
                IniFile.Write(Parameter.INI_Common, "Record", "RecordTime", ((int)SPE_Recordtime.Value).ToString());
                IniFile.Write(Parameter.INI_Common, "Record", "RecordSize", ((int)SPE_FileSize.Value).ToString());
                IniFile.Write(Parameter.INI_Common, "Record", "MinSize", ((int)SPE_Min_Size.Value).ToString());
                IniFile.Write(Parameter.INI_Common, "Record", "SaveFormat", DDL_Speicherformat.SelectedValue?.ToString() ?? "0");

                // Preview/record size settings
                IniFile.Write(Parameter.INI_Common, "Size", "Record", DDL_Size_Record.SelectedValue?.ToString() ?? "0");
                IniFile.Write(Parameter.INI_Common, "Size", "View", DDL_Size_Preview.SelectedValue?.ToString() ?? "0");
                IniFile.Write(Parameter.INI_Common, "Lizenz", "Advice", CBX_Promo.Checked.ToString());

                // Record stop options
                if (RBT_Aufnahmestop_NoStop.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "RecordStop", "0");
                else if (RBT_Aufnahmestop_Time.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "RecordStop", "1");
                else if (RBT_Aufnahmestop_Size.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "RecordStop", "2");

                // Resolution options
                if (RBT_Video_Send.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "Resolution", "0");
                else if (RBT_Video_Minimal.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "Resolution", "1");
                else if (RBT_Video_SD.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "Resolution", "2");
                else if (RBT_Video_HD.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "Resolution", "3");
                else if (RBT_Video_FullHD.Checked)
                    IniFile.Write(Parameter.INI_Common, "Record", "Resolution", "4");

                // Additional options
                IniFile.Write(Parameter.INI_Common, "Optionen", "Notification", CBX_Benachrichtigung.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Optionen", "Visible", CBX_Visible.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Optionen", "Record", CBX_Record.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Optionen", "Search", CBX_Suche.Checked.ToString());
                IniFile.Write(Parameter.INI_Common, "Optionen", "Tooltip", ((int)SPE_Tooltip.Value).ToString());

                // Save website-specific settings
                foreach (ListViewItem listViewDataItem in LIV_Websites.Items)
                {
                    IniFile.Write(
                        Parameter.INI_Common,
                        "Sites",
                        listViewDataItem.Tag.ToString(),
                        listViewDataItem.Checked.ToString()
                    );
                }

                // Refresh license and design
                Parameter.Programlizenz = new Lizenz(true);
                Parameter.Design_Change(int.Parse(IniFile.Read(Parameter.INI_Common, "Design", "Style", "0")));

                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.Übernehmen");
            }
        }


        private void Dialog_Einstellungen_Load(object sender, EventArgs e)
        {
            try
            {
                // Set copyright
                LAB_CopyRight.Text = $"© {DateTime.Now.Year} Dühring EDV-Service\r\n{TXT.TXT_Description("Alle Rechte vorbehalten.")}";
                TXT.Control_Languages(this);

                TTP_Einstellungen.SetToolTip(BTN_Licence_Check, TXT.TXT_Description("Lizenz prüfen"));

                // Form title
                Text = TXT.TXT_Description("Einstellungen");

                // Populate design ComboBox
                DDL_Design.Items.Clear();
                DDL_Design.Items.Add(TXT.TXT_Description("Default"));
                DDL_Design.Items.Add(TXT.TXT_Description("Hell"));
                DDL_Design.Items.Add(TXT.TXT_Description("Dunkel"));
                DDL_Design.SelectedIndex = 0;

                // Populate languages ComboBox if multiple language files exist
                string languageFolder = Path.Combine(Application.StartupPath, "Language");
                if (Directory.Exists(languageFolder) && Directory.GetFiles(languageFolder).Length > 1)
                {
                    DDL_Languages.Items.Clear();
                    foreach (var filePath in Directory.GetFiles(languageFolder))
                    {
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                        string description = Languages.Language_Find(fileNameWithoutExt)?.Pro_Description ?? Path.GetFileName(filePath);
                        DDL_Languages.Items.Add(description);
                    }

                    string savedLang = IniFile.Read(Parameter.INI_Common, "Language", "Files");

                    DDL_Languages.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    DDL_Languages.AutoCompleteSource = AutoCompleteSource.ListItems;
                    DDL_Languages.Sorted = true;

                    // Try to select the saved language (case-insensitive)
                    int langIndex = -1;
                    if (!string.IsNullOrEmpty(savedLang))
                    {
                        for (int i = 0; i < DDL_Languages.Items.Count; i++)
                        {
                            if (string.Equals(DDL_Languages.Items[i].ToString(), savedLang, StringComparison.OrdinalIgnoreCase))
                            {
                                langIndex = i;
                                break;
                            }
                        }
                    }
                    if (langIndex >= 0)
                    {
                        DDL_Languages.SelectedIndex = langIndex;
                    }
                    else
                    {
                        // Fallback: select any item ending with "en.ini"
                        for (int i = 0; i < DDL_Languages.Items.Count; i++)
                        {
                            if (DDL_Languages.Items[i].ToString().EndsWith("en.ini", StringComparison.OrdinalIgnoreCase))
                            {
                                DDL_Languages.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                // Load saved directory paths
                TXB_Aufnahmen.Text = IniFile.Read(Parameter.INI_Common, "Directory", "Records");
                TXB_Datenbank.Text = IniFile.Read(Parameter.INI_Common, "Directory", "Database");
                TXB_Favoriten.Text = IniFile.Read(Parameter.INI_Common, "Directory", "Favorites");

                // Load numeric values (Tooltip, RecordTime, MinSize)
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Optionen", "Tooltip", "3"), out int tooltipSeconds))
                    tooltipSeconds = 3;
                SPE_Tooltip.Value = tooltipSeconds;

                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "RecordTime", "30"), out int recordTime))
                    recordTime = 30;
                SPE_Recordtime.Value = recordTime;

                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "MinSize", "0"), out int minSize))
                    minSize = 0;
                SPE_Min_Size.Value = minSize;

                // Load filename template and license value
                TXB_Dateiname.Text = IniFile.Read(Parameter.INI_Common, "Record", "Name", "{WK}-{Year}-{Month}-{Day}-{Hour}-{Minute}");
                TXB_LizenzValue.Text = IniFile.Read(Parameter.INI_Common, "Lizenz", "LizenzValue");

                // Populate preview-size ComboBox
                DDL_Size_Preview.Items.Clear();
                DDL_Size_Preview.Items.Add(TXT.TXT_Description("Klein"));
                DDL_Size_Preview.Items.Add(TXT.TXT_Description("Mittel"));
                DDL_Size_Preview.Items.Add(TXT.TXT_Description("Groß"));
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Size", "View", "1"), out int previewIndex))
                    previewIndex = 1;
                DDL_Size_Preview.SelectedIndex = Math.Clamp(previewIndex, 0, DDL_Size_Preview.Items.Count - 1);

                // Populate record-size ComboBox
                DDL_Size_Record.Items.Clear();
                DDL_Size_Record.Items.Add(TXT.TXT_Description("Klein"));
                DDL_Size_Record.Items.Add(TXT.TXT_Description("Mittel"));
                DDL_Size_Record.Items.Add(TXT.TXT_Description("Groß"));
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Size", "Record", "0"), out int recordSizeIndex))
                    recordSizeIndex = 0;
                DDL_Size_Record.SelectedIndex = Math.Clamp(recordSizeIndex, 0, DDL_Size_Record.Items.Count - 1);

                // Load checkbox states
                CBX_Minimize.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Tray", "Minimize", "true"), out bool trayMinimize) && trayMinimize;
                CBX_FavoritenRecords.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Favorite", "Copy", "true"), out bool favCopy) && favCopy;
                CBX_Notification.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Notification", "Enabled", "true"), out bool notifEnabled) && notifEnabled;
                CBX_Player.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Player", "Intern", "true"), out bool playerIntern) && playerIntern;
                CBX_Debug.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Debug", "Debug", "false"), out bool debugMode) && debugMode;

                // Update license-version label
                LAB_LizenzVersion.Text = $"{Application.ProductVersion} {Parameter.Programlizenz.Lizenz_Bezeichnung}";

                // Load design-header and online-check flags
                CBX_StreamHeader.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Design", "Header", "true"), out bool headerVisible) && headerVisible;
                CBX_Start_Check.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Online", "Check", "true"), out bool onlineCheck) && onlineCheck;

                // Load maximum concurrent conversions
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "MaxConversion", "5"), out int maxConv))
                    maxConv = 5;
                SPE_Conversion.Value = maxConv;

                // Load option flags (Notification, Visible, Record, Search)
                CBX_Benachrichtigung.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Optionen", "Notification", "true"), out bool optNotif) && optNotif;
                CBX_Visible.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Optionen", "Visible", "true"), out bool optVisible) && optVisible;
                CBX_Record.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Optionen", "Record", "false"), out bool optRecord) && optRecord;
                CBX_Suche.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Optionen", "Search", "false"), out bool optSearch) && optSearch;

                // Load promo flag and enable checkbox only if a license exists
                CBX_Promo.Checked = bool.TryParse(IniFile.Read(Parameter.INI_Common, "Lizenz", "Advice", "true"), out bool promoAdvice) && promoAdvice;
                CBX_Promo.Enabled = Lizenz.Lizenz_vorhanden;

                // Load record-stop radio buttons
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "RecordStop", "1"), out int stopMode))
                    stopMode = 1;
                RBT_Aufnahmestop_NoStop.Checked = (stopMode == 0);
                RBT_Aufnahmestop_Time.Checked = (stopMode == 1);
                RBT_Aufnahmestop_Size.Checked = (stopMode == 2);

                // Load file-size and record-time values again (if needed)
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "RecordSize", "0"), out int fileSizeVal))
                    fileSizeVal = 0;
                SPE_FileSize.Value = fileSizeVal;

                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "RecordTime", "30"), out int fileRecTime))
                    fileRecTime = 30;
                SPE_Recordtime.Value = fileRecTime;

                // Load video resolution radio buttons
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "Resolution", "0"), out int resolution))
                    resolution = 0;
                RBT_Video_Send.Checked = (resolution == 0);
                RBT_Video_Minimal.Checked = (resolution == 1);
                RBT_Video_SD.Checked = (resolution == 2);
                RBT_Video_HD.Checked = (resolution == 3);
                RBT_Video_FullHD.Checked = (resolution == 4);

                // Populate video-encoder ComboBox
                DDL_Video_Encoder.Items.Clear();
                foreach (var decoderItem in Decoder.Decoder_Item_List)
                {
                    if (decoderItem.Decorder_Default_Select)
                        DDL_Video_Encoder.Items.Add(decoderItem.Decoder_Name);
                }
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "Encoder", "0"), out int encoderIdx))
                    encoderIdx = 0;
                DDL_Video_Encoder.SelectedIndex = Math.Clamp(encoderIdx, 0, DDL_Video_Encoder.Items.Count - 1);

                // Populate storage-format ComboBox
                DDL_Speicherformat.Items.Clear();
                foreach (var formatItem in Speicherformate.Speicherformate_Item_List)
                {
                    DDL_Speicherformat.Items.Add(formatItem.Pro_Speicherformat_Name);
                }
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Record", "SaveFormat", "0"), out int formatIdx))
                    formatIdx = 0;
                DDL_Speicherformat.SelectedIndex = Math.Clamp(formatIdx, 0, DDL_Speicherformat.Items.Count - 1);

                // Populate browser ComboBox
                DDL_Browser.Items.Clear();
                DDL_Browser.Items.Add(TXT.TXT_Description("CamBrowser"));
                DDL_Browser.Items.Add(TXT.TXT_Description("Standard"));
                if (File.Exists(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"))
                    DDL_Browser.Items.Add("Microsoft Edge (Private)");
                if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
                    DDL_Browser.Items.Add("Firefox (Private)");
                if (File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe"))
                    DDL_Browser.Items.Add("Chrome (Inkognito)");
                if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lovense", "Browser", "Lovense Starter.exe")))
                    DDL_Browser.Items.Add("Lovense Browser");

                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Browser", "Product", "0"), out int browserIdx))
                    browserIdx = 0;
                DDL_Browser.SelectedIndex = Math.Clamp(browserIdx, 0, DDL_Browser.Items.Count - 1);

                // Select design from saved value
                if (!int.TryParse(IniFile.Read(Parameter.INI_Common, "Design", "Style", "0"), out int designIdx))
                    designIdx = 0;
                DDL_Design.SelectedIndex = Math.Clamp(designIdx, 0, DDL_Design.Items.Count - 1);

                // Load website list and tooltips
                Load_Websites();
                Set_ToolTip();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.Load");
            }
        }


        private void Set_ToolTip()
        {
            ToolTip ttpEinstellungen = this.TTP_Einstellungen;
            ttpEinstellungen.SetToolTip(DDL_Languages, TXT.TXT_Description("Wähle die Sprache für XstreaMon"));
            ttpEinstellungen.SetToolTip(DDL_Browser, TXT.TXT_Description("Wähle den Standardbrowser mit dem Streams geöffnet werden sollen"));
            ttpEinstellungen.SetToolTip(DDL_Design, TXT.TXT_Description("Wähle ein Design um das Aussehen von XstreaMon zu verändern"));
            ttpEinstellungen.SetToolTip(CBX_Minimize, TXT.TXT_Description("XstreaMon wird als Symbolleistensymbol abgelegt."));
            ttpEinstellungen.SetToolTip(CBX_Notification, TXT.TXT_Description("Es werden alle Benachrichtigungen von den Kanälen angezeigt bei denen die Benachrichtiugung aktiviert sind. Deaktiviert werden keine Benachrichtigungen angezeigt."));
            ttpEinstellungen.SetToolTip(CBX_Player, TXT.TXT_Description("Verwendet beim öffnen aus den Galerien den internen Player. Deaktiviert wird die Datei mit dem Standard Programm geöffnet"));
            ttpEinstellungen.SetToolTip(CBX_StreamHeader, TXT.TXT_Description("Bei der Streamvorschau werden die Streaminformationen dauerhaft angezeigt. Deaktiviert werden die Informationen nur angezeigt wenn sich der Mauszeiger auf dem Streamelement befindet."));
            ttpEinstellungen.SetToolTip(CBX_Debug, TXT.TXT_Description("Schalten den Debugmodus an. Ausgeblendete Fenster wie die Aufnahmefenster und Konvertierungen werden eingeblendet. Es werden Fehler protokolliert."));
            ttpEinstellungen.SetToolTip(SPE_Conversion, TXT.TXT_Description("Die maximale Anzahl der ausstehenden Videokonverierungen die gleichzeitig ausgeführt werden können. Die Aufnahmen von einigen Webseiten müssen in das MP4 Format konvertiert werden."));
            ttpEinstellungen.SetToolTip(DDL_Size_Preview, TXT.TXT_Description("Die Größe des Streamelements"));
            ttpEinstellungen.SetToolTip(DDL_Size_Record, TXT.TXT_Description("Die Größe des Streamelements wenn eine Aufnahme erfolgt"));
            ttpEinstellungen.SetToolTip(CBX_Start_Check, TXT.TXT_Description("Alle Kanäle werden beim Start geprüft. Deaktiviert werden die Kanäle nach dem Ablauf des Prüfintervalls geprüft. (Empfohlen bei mehr als 50 erfassten Kanälen bei einer Webseite)"));
            ttpEinstellungen.SetToolTip(CBX_Suche, TXT.TXT_Description("Beim Anlegen eines neuen Kanals werden alle Plattformen nach dem Kanalname durchsucht."));
            ttpEinstellungen.SetToolTip(TXB_Aufnahmen, TXT.TXT_Description("Das Verzeichnis in denen die Aufnahmen der Kanäle gespeichert werden"));
            ttpEinstellungen.SetToolTip(TXB_Datenbank, TXT.TXT_Description("Das Verzeichnis in dem sich die XstreaMon.mdb Datei befindet"));
            ttpEinstellungen.SetToolTip(TXB_Favoriten, TXT.TXT_Description("Das Verzeichnis in den die Aufnahmen gespeichert werden die als Favoriten gekennzeichet sind."));
            ttpEinstellungen.SetToolTip(CBX_FavoritenRecords, TXT.TXT_Description("Kopiert die als Favoriten gekennzeichneten Aufnahmen zusätzlich in den Favoritenordner."));
            ttpEinstellungen.SetToolTip(TXB_Dateiname, TXT.TXT_Description("Die Bezeichnung der Aufnahmen im Dateiverzeichnis. Mit dem Fragzeichen könne alle Optionen angzeigt werden."));
            ttpEinstellungen.SetToolTip(BTN_Anfordern, TXT.TXT_Description("Öffnet den Webshop um eine Lizenz für XstreaMon zu erwerben."));
            ttpEinstellungen.SetToolTip(TXB_LizenzValue, TXT.TXT_Description("Dein Lizenzschlüssel für XstreaMon. Mit der Lupe kann die Lizenz überprüft werden."));
            ttpEinstellungen.SetToolTip(PVP_Vorgaben, TXT.TXT_Description("Vorgaben für neue Kanäle"));
            ttpEinstellungen.SetToolTip(CBX_Benachrichtigung, TXT.TXT_Description("Benachrichtigung wird eingeblendet wenn der Kanal online geht."));
            ttpEinstellungen.SetToolTip(CBX_Visible, TXT.TXT_Description("Der Stream wird angezeigt wenn der Kanal online ist"));
            ttpEinstellungen.SetToolTip(CBX_Record, TXT.TXT_Description("Die Aufnahme wird automatisch gestartet wenn der Kanal online ist."));
            ttpEinstellungen.SetToolTip(DDL_Video_Encoder, TXT.TXT_Description("Legt das Programm fest welches für die Aufnahmen genutzt wird. (CRStreamRec empfohlen)"));
            ttpEinstellungen.SetToolTip(RBT_Video_Send, TXT.TXT_Description("Aufnahmen werden in der besten verfügbaren Auflösung gespeichert"));
            ttpEinstellungen.SetToolTip(DDL_Speicherformat, TXT.TXT_Description("Das Videoformat in dem die Aufnahme gespeichert werden soll. Eine nötige Konvertierung wird nach der Aufnahme ausgeführt"));
            ttpEinstellungen.SetToolTip(RBT_Aufnahmestop_NoStop, TXT.TXT_Description("Die Aufnahme endet erst wenn der Sender nicht mehr sendet. Die Dateigröße kann sehr groß werden. (empfohlen ist der Aufnahmestop nach Zeit oder Dateigröße)"));
            ttpEinstellungen.SetToolTip(RBT_Aufnahmestop_Size, TXT.TXT_Description("Die Aufnahme wird beendet wenn die Dateigröße erreicht ist und gleich wieder neu gestartet."));
            ttpEinstellungen.SetToolTip(RBT_Aufnahmestop_Time, TXT.TXT_Description("Die Aufnahme wird beendet wenn die Aufnahmezeit erreicht ist und gleich wieder neu gestartet."));
            ttpEinstellungen.SetToolTip(BTN_Optionen_Update, TXT.TXT_Description("Die Optionen-Einstellungen werden auf alle Kanäle übertragen"));
            ttpEinstellungen.SetToolTip(BTN_Aufnahme_Update, TXT.TXT_Description("Die Aufnahme-Einstellungen werden auf alle Kanäle übertragen"));
            ttpEinstellungen.SetToolTip(BTN_Stop_Update, TXT.TXT_Description("Die Aufnahmestop-Einstellungen werden auf alle Kanäle übertragen"));
            ttpEinstellungen.SetToolTip(SPE_Tooltip, TXT.TXT_Description("Zeit in Sekunden bis der Tooltip eingeblendet wird."));
        }

        private void Load_Websites()
        {
            try
            {
                LIV_Websites.Items.Clear();
                foreach (Class_Website website in Sites.Website_List)
                {
                    bool isChecked = string.Equals(
                        IniFile.Read(Parameter.INI_Common, "Sites", website.Pro_ID.ToString(), "True"),
                        "True",
                        StringComparison.OrdinalIgnoreCase
                    );

                    var item = new ListViewItem
                    {
                        Text = website.Pro_Name,
                        Tag = website.Pro_ID,
                        Checked = isChecked
                    };

                    LIV_Websites.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.Load_Websites");
            }
        }

        private void BTN_Records_Click(object sender, EventArgs e)
        {
            try
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = TXT.TXT_Description("Geben Sie das Verzeichnis für die Aufnahmen an");
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        TXB_Aufnahmen.Text = folderDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Records_Click");
            }
        }

        private void BTN_Datenbank_Click(object sender, EventArgs e)
        {
            try
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = TXT.TXT_Description("Geben Sie das Verzeichnis für die Datenbank an");
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        TXB_Datenbank.Text = folderDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Datenbank_Click");
            }
        }


        private void BTN_Favoriten_Click(object sender, EventArgs e)
        {
            try
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = TXT.TXT_Description("Geben Sie das Verzeichnis für die Favoriten an");
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        TXB_Favoriten.Text = folderDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Favoriten_Click");
            }
        }

        private void BTN_Anfordern_Click(object sender, EventArgs e)
        {
            try
            {
                var psi = new ProcessStartInfo("https://duehring-edv.com/?cat=40")
                {
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Anfordern_Click");
            }
        }

        private void TXB_LizenzValue_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TXB_LizenzValue.Text))
                    return;

                if (e.KeyCode == Keys.Enter || (e.Control && e.KeyCode == Keys.V))
                {
                    LizenzValue_Check();
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.TXB_LizenzValue_KeyUp");
            }
        }

        private void LizenzValue_Check()
        {
            try
            {
                var lizenz = new Lizenz(TXB_LizenzValue.Text);
                LAB_LizenzVersion.Text = $"{Application.ProductVersion} {lizenz.Lizenz_Bezeichnung}";
            }
            catch (Exception ex)
            {
                // Directly log the error without VB ProjectData calls
                Parameter.Error_Message(ex, "Dialog_Einstellungen.LizenzValue_Check");
            }
        }


        private void BTN_Abbrechen_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Abbrechen_Click");
            }
        }


        private void BTN_Dateinname_Click(object sender, EventArgs e)
        {
            try
            {
                var dialogInfoFile = new Dialog_Info_File
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                dialogInfoFile.Show();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Dateinname_Click");
            }
        }


        private void BTN_Optionen_Update_Click(object sender, EventArgs e)
        {
            try
            {
                var prompt = TXT.TXT_Description("Möchten Sie die Einstellungen auf alle Kanäle anwenden?");
                var title = TXT.TXT_Description("Einstellungen übernehmen");
                if (MessageBox.Show(prompt, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    string connectionString = Database_Connect.Aktiv_Datenbank();
                    string query = $"SELECT * FROM DT_User WHERE User_GUID = '{model.Pro_Model_GUID}'";

                    using (var connection = new OleDbConnection(connectionString))
                    using (var adapter = new OleDbDataAdapter(query, connectionString))
                    using (var builder = new OleDbCommandBuilder(adapter))
                    using (var dataSet = new DataSet())
                    {
                        connection.Open();
                        adapter.Fill(dataSet, "DT_Model");
                        connection.Close();

                        if (dataSet.Tables["DT_Model"].Rows.Count == 1)
                        {
                            DataRow row = dataSet.Tables["DT_Model"].Rows[0];
                            row["User_Record"] = CBX_Record.Checked;
                            row["User_Visible"] = CBX_Visible.Checked;
                            row["Benachrichtigung"] = CBX_Benachrichtigung.Checked;
                            adapter.Update(dataSet.Tables["DT_Model"]);
                        }
                    }

                    model.Pro_Model_Visible = CBX_Visible.Checked;
                    model.Pro_Benachrichtigung = CBX_Benachrichtigung.Checked;
                    model.Pro_Model_Record = CBX_Record.Checked;
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Optionen_Update_Click");
            }
        }

        private void BTN_Aufnahme_Update_Click(object sender, EventArgs e)
        {
            try
            {
                var prompt = TXT.TXT_Description("Möchten Sie die Einstellungen auf alle Kanäle anwenden?");
                var title = TXT.TXT_Description("Einstellungen übernehmen");
                if (MessageBox.Show(prompt, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    string connectionString = Database_Connect.Aktiv_Datenbank();
                    string query = $"SELECT * FROM DT_User WHERE User_GUID = '{model.Pro_Model_GUID}'";

                    using (var connection = new OleDbConnection(connectionString))
                    using (var adapter = new OleDbDataAdapter(query, connectionString))
                    using (var builder = new OleDbCommandBuilder(adapter))
                    using (var dataSet = new DataSet())
                    {
                        connection.Open();
                        adapter.Fill(dataSet, "DT_Model");
                        connection.Close();

                        if (dataSet.Tables["DT_Model"].Rows.Count == 1)
                        {
                            DataRow row = dataSet.Tables["DT_Model"].Rows[0];
                            row["Recorder_ID"] = Convert.ToInt32(DDL_Video_Encoder.SelectedValue ?? 0);
                            row["SaveFormat"] = Convert.ToInt32(DDL_Speicherformat.SelectedValue ?? 0);

                            if (RBT_Video_Send.Checked)
                                row["Videoqualität"] = 0;
                            else if (RBT_Video_Minimal.Checked)
                                row["Videoqualität"] = 1;
                            else if (RBT_Video_SD.Checked)
                                row["Videoqualität"] = 2;
                            else if (RBT_Video_HD.Checked)
                                row["Videoqualität"] = 3;
                            else if (RBT_Video_FullHD.Checked)
                                row["Videoqualität"] = 4;

                            adapter.Update(dataSet.Tables["DT_Model"]);
                        }
                    }

                    model.Pro_Decoder = Convert.ToInt32(DDL_Video_Encoder.SelectedValue ?? 0);
                    if (RBT_Video_Send.Checked)
                        model.Pro_Videoqualität = 0;
                    else if (RBT_Video_Minimal.Checked)
                        model.Pro_Videoqualität = 1;
                    else if (RBT_Video_SD.Checked)
                        model.Pro_Videoqualität = 2;
                    else if (RBT_Video_HD.Checked)
                        model.Pro_Videoqualität = 3;
                    else if (RBT_Video_FullHD.Checked)
                        model.Pro_Videoqualität = 4;
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Aufnahme_Update_Click");
            }
        }

        private void BTN_Stop_Update_Click(object sender, EventArgs e)
        {
            try
            {
                var prompt = TXT.TXT_Description("Möchten Sie die Einstellungen auf alle Kanäle anwenden?");
                var title = TXT.TXT_Description("Einstellungen übernehmen");
                if (MessageBox.Show(prompt, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                foreach (Class_Model model in Class_Model_List.Model_List)
                {
                    string connectionString = Database_Connect.Aktiv_Datenbank();
                    string query = $"SELECT * FROM DT_User WHERE User_GUID = '{model.Pro_Model_GUID}'";

                    using (var connection = new OleDbConnection(connectionString))
                    using (var adapter = new OleDbDataAdapter(query, connectionString))
                    using (var builder = new OleDbCommandBuilder(adapter))
                    using (var dataSet = new DataSet())
                    {
                        connection.Open();
                        adapter.Fill(dataSet, "DT_Model");
                        connection.Close();

                        if (dataSet.Tables["DT_Model"].Rows.Count == 1)
                        {
                            DataRow row = dataSet.Tables["DT_Model"].Rows[0];
                            if (RBT_Aufnahmestop_NoStop.Checked)
                                row["Aufnahmestop_Auswahl"] = 0;
                            else if (RBT_Aufnahmestop_Time.Checked)
                                row["Aufnahmestop_Auswahl"] = 1;
                            else if (RBT_Aufnahmestop_Size.Checked)
                                row["Aufnahmestop_Auswahl"] = 2;

                            row["Aufnahmestop_Größe"] = (int)SPE_FileSize.Value;
                            row["Aufnahmestop_Minuten"] = (int)SPE_Recordtime.Value;

                            adapter.Update(dataSet.Tables["DT_Model"]);
                        }
                    }

                    model.Pro_Aufnahme_Stop_Größe = Convert.ToInt32(SPE_FileSize.Value);
                    model.Pro_Aufnahme_Stop_Minuten = Convert.ToInt32(SPE_Recordtime.Value);
                    if (RBT_Aufnahmestop_NoStop.Checked)
                        model.Pro_Aufnahme_Stop_Auswahl = 0;
                    else if (RBT_Aufnahmestop_Time.Checked)
                        model.Pro_Aufnahme_Stop_Auswahl = 1;
                    else if (RBT_Aufnahmestop_Size.Checked)
                        model.Pro_Aufnahme_Stop_Auswahl = 2;
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Einstellungen.BTN_Stop_Update_Click");
            }
        }

        private static void DB_Datapath_Update(string Datapath_Old, string Datapath_New)
        {
            try
            {
                string connectionString = Database_Connect.Aktiv_Datenbank();
                using var connection = new OleDbConnection(connectionString);
                using var adapter = new OleDbDataAdapter("SELECT * FROM DT_User", connectionString);
                using var builder = new OleDbCommandBuilder(adapter);
                using var dataSet = new DataSet();
                connection.Open();
                adapter.Fill(dataSet, "DT_Record");
                connection.Close();

                var table = dataSet.Tables["DT_Record"];
                foreach (DataRow row in table.Rows)
                {
                    string userDir = row["User_Directory"].ToString();
                    if (userDir.Contains(Datapath_Old))
                    {
                        string updatedPath = userDir.Replace(Datapath_Old, Datapath_New);
                        row["User_Directory"] = updatedPath;

                        Guid userGuid = Guid.Parse(row["User_GUID"].ToString());
                        Class_Model result = Class_Model_List.Class_Model_Find(userGuid).Result;
                        if (result != null)
                        {
                            result.Pro_Model_Directory = result.Pro_Model_Directory.Replace(Datapath_Old, Datapath_New);
                        }

                        // Persist change for this row
                        adapter.Update(table);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, $"Parameter.DB_Datapath_Update ({Datapath_Old}, {Datapath_New})");
            }
        }
    }
}
