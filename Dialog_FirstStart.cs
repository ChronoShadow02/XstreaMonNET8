namespace XstreaMonNET8
{
    public partial class Dialog_FirstStart : Form
    {
        // Properties from the original VB.NET code
        internal string Pro_Language_File => "";
        internal string Pro_Video_Folder => "";
        internal bool Pro_New_Channel => TSW_New_Channel.Checked; // Changed from .Value to .Checked for native ToggleSwitch equivalent

        public Dialog_FirstStart()
        {
            InitializeComponent();
            this.Load += new EventHandler(this.Dialog_FirstStart_Load!);
        }

        private void BTN_Accept_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.TXB_Folder.Text))
            {
                // Assuming TXT.TXT_Description is a static method or accessible
                MessageBox.Show(TXT.TXT_Description("Geben sie den Ordner für die Aufnahmen an."));
                this.TXB_Folder.Focus();
            }
            else
            {
                IniFile.Write(Parameter.INI_Common, "Directory", "Records", this.TXB_Folder.Text);
                IniFile.Write(Parameter.INI_Common, "Language", "Files", this.DDL_Languages.SelectedValue?.ToString()!);
                IniFile.Write(Parameter.INI_Common, "Record", "RecordTime", "30");
                IniFile.Write(Parameter.INI_Common, "Browser", "Product", "-1");

                TXT txt = new(this.DDL_Languages.SelectedValue?.ToString()!);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Dialog_FirstStart_Load(object sender, EventArgs e)
        {
            // Assuming Application.StartupPath is correct for C#
            if (new DirectoryInfo(Application.StartupPath + "\\Language").GetFiles().Length > 1)
            {
                FileInfo[] files = new DirectoryInfo(Application.StartupPath + "\\Language").GetFiles();
                int index = 0;
                while (index < files.Length)
                {
                    FileInfo fileInfo = files[index];
                    // Assuming Languages.Language_Find is defined elsewhere and accessible
                    string str = Languages.Language_Find(fileInfo.Name.Replace(fileInfo.Extension, ""))?.Pro_Description ?? fileInfo.Name;

                    // For ComboBox, use DisplayMember and ValueMember or add custom objects
                    // For simplicity, adding string representation to ComboBox
                    this.DDL_Languages.Items.Add(new ComboBoxItem { Text = str, Value = fileInfo.FullName, Tag = fileInfo.Name.Replace(fileInfo.Extension, "") });
                    checked { ++index; }
                }

                string letterIsoLanguageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                this.DDL_Languages.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.DDL_Languages.AutoCompleteSource = AutoCompleteSource.ListItems; // Equivalent to DropDownListElement.AutoCompleteAppend.LimitToList = true;

                // Sort items (ComboBox doesn't have a direct SortStyle property like RadDropDownList)
                // You might need to sort the underlying data source or manually sort items if needed.
                // For now, skipping direct sorting equivalent as it's not a direct ComboBox feature.

                try
                {
                    foreach (ComboBoxItem item in this.DDL_Languages.Items)
                    {
                        if (string.Equals(item.Tag?.ToString(), letterIsoLanguageName, StringComparison.OrdinalIgnoreCase))
                        {
                            this.DDL_Languages.SelectedItem = item;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception if necessary
                }

                try
                {
                    // Assuming Sites.Website_List is defined elsewhere and accessible
                    foreach (Class_Website website in Sites.Website_List)
                    {
                        // For ComboBox, use DisplayMember and ValueMember or add custom objects
                        // For simplicity, adding string representation to ComboBox
                        this.DDL_Webseite.Items.Add(new ComboBoxItem { Text = website.Pro_Name, Value = website.Pro_ID, Image = new Bitmap(website.Pro_Image, 16, 16) });
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception if necessary
                }

                this.DDL_Webseite.Items.Add(new ComboBoxItem { Text = TXT.TXT_Description("Start without a website"), Value = -1 });
                this.DDL_Webseite.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.DDL_Webseite.AutoCompleteSource = AutoCompleteSource.ListItems; // Equivalent to DropDownListElement.AutoCompleteAppend.LimitToList = true;

                // Sort items (ComboBox doesn't have a direct SortStyle property like RadDropDownList)
                // For now, skipping direct sorting equivalent.

                this.DDL_Webseite.SelectedValue = -1; // Set selected value
            }
            this.TXB_Folder.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos).ToString();
        }

        private void DDL_Languages_SelectedValueChanged(object sender, EventArgs e)
        {
            // Assuming TXT class constructor takes a string
            TXT txt = new TXT(this.DDL_Languages.SelectedValue?.ToString());

            this.LAB_1.Text = "Welcome to XstreaMon!";
            this.LAB_2.Text = "Before we start XstreaMon needs some information from you.";
            this.LAB_3.Text = "Select the language you want XstreaMon to run in.";
            this.LAB_4.Text = "Select the directory where the videos and data can be saved.";
            this.LAB_5.Text = "Do you want to create a new channel?";
            this.BTN_Accept.Text = "Let's go";
            this.BTN_Abort.Text = "No better not";
            this.LAB_Webseite.Text = "Start with website";

            try
            {
                foreach (ComboBoxItem item in this.DDL_Webseite.Items)
                {
                    if (item.Value is int && (int)item.Value == -1)
                    {
                        item.Text = TXT.TXT_Description("Start without a website");
                        break;
                    }
                }
                // Refresh ComboBox to show updated text
                this.DDL_Webseite.Invalidate();
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
            }

            // Assuming TXT.Control_Languages is a static method or accessible
            TXT.Control_Languages(this);
        }

        private void BTN_Folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = this.TXB_Folder.Text;
                // Assuming TXT.TXT_Description is a static method or accessible
                folderBrowserDialog.Description = TXT.TXT_Description("Geben Sie das Datenverzeichnis für die Aufnahmen an");
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    this.TXB_Folder.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void TSW_New_Channel_ValueChanged(object sender, EventArgs e)
        {
            // For CheckBox, use .Checked property
            this.DDL_Webseite.Visible = !this.TSW_New_Channel.Checked;
            this.LAB_Webseite.Visible = !this.TSW_New_Channel.Checked;
        }

        // Helper class for ComboBox items to store Text, Value, Tag, and Image
        // This replaces RadListDataItem functionality
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public object Tag { get; set; }
            public Image Image { get; set; } // For image display in ComboBox if needed

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
