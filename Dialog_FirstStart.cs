#nullable disable
namespace XstreaMonNET8
{
    public partial class Dialog_FirstStart : Form
    {
        private class ListDataItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public object Tag { get; set; }
            public override string ToString() => Text;
        }

        public Dialog_FirstStart()
        {
            Load += new EventHandler(Dialog_FirstStart_Load);
            InitializeComponent();
        }

        internal string Pro_Language_File => string.Empty;

        internal string Pro_Video_Folder => string.Empty;

        internal bool Pro_New_Channel => TSW_New_Channel.Checked;

        private void Dialog_FirstStart_Load(object sender, EventArgs e)
        {
            string langFolder = Path.Combine(Application.StartupPath, "Language");
            if (Directory.Exists(langFolder))
            {
                FileInfo[] files = new DirectoryInfo(langFolder).GetFiles();
                if (files.Length > 1)
                {
                    foreach (FileInfo fileInfo in files)
                    {
                        string code = Path.GetFileNameWithoutExtension(fileInfo.Name);
                        string str = Languages.Language_Find(code)?.Pro_Description ?? fileInfo.Name;
                        var item = new ListDataItem
                        {
                            Text = str,
                            Value = fileInfo.FullName,
                            Tag = code
                        };
                        DDL_Languages.Items.Add(item);
                    }
                    DDL_Languages.DisplayMember = "Text";
                    string currentIso = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
                    for (int i = 0; i < DDL_Languages.Items.Count; i++)
                    {
                        var itm = DDL_Languages.Items[i] as ListDataItem;
                        if (itm != null && Convert.ToString(itm.Tag).ToLower() == currentIso)
                        {
                            DDL_Languages.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }

            foreach (Class_Website website in Sites.Website_List)
            {
                var webItem = new ListDataItem
                {
                    Text = website.Pro_Name,
                    Value = website.Pro_ID,
                    Tag = null
                };
                DDL_Webseite.Items.Add(webItem);
            }
            var noneItem = new ListDataItem
            {
                Text = TXT.TXT_Description("Start without a website"),
                Value = -1,
                Tag = null
            };
            DDL_Webseite.Items.Add(noneItem);
            DDL_Webseite.DisplayMember = "Text";
            for (int i = 0; i < DDL_Webseite.Items.Count; i++)
            {
                var itm = DDL_Webseite.Items[i] as ListDataItem;
                if (itm != null && Convert.ToInt32(itm.Value) == -1)
                {
                    DDL_Webseite.SelectedIndex = i;
                    break;
                }
            }

            TXB_Folder.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }

        private void DDL_Languages_SelectedValueChanged(object sender, EventArgs e)
        {
            var langItem = DDL_Languages.SelectedItem as ListDataItem;
            if (langItem == null) return;

            TXT txt = new(Convert.ToString(langItem.Value));
            LAB_1.Text = "Welcome to XstreaMon!";
            LAB_2.Text = "Before we start XstreaMon needs some information from you.";
            LAB_3.Text = "Select the language you want XstreaMon to run in.";
            LAB_4.Text = "Select the directory where the videos and data can be saved.";
            LAB_5.Text = "Do you want to create a new channel?";
            BTN_Accept.Text = "Let's go";
            BTN_Abort.Text = "No better not";
            LAB_Webseite.Text = "Start with website";

            foreach (object obj in DDL_Webseite.Items)
            {
                var webItem = obj as ListDataItem;
                if (webItem != null && Convert.ToInt32(webItem.Value) == -1)
                {
                    webItem.Text = TXT.TXT_Description("Start without a website");
                    break;
                }
            }

            TXT.Control_Languages(this);
        }

        private void BTN_Folder_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog folderBrowserDialog = new();
            folderBrowserDialog.SelectedPath = TXB_Folder.Text;
            folderBrowserDialog.Description = TXT.TXT_Description("Geben Sie das Datenverzeichnis für die Aufnahmen an");
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                TXB_Folder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void TSW_New_Channel_ValueChanged(object sender, EventArgs e)
        {
            TSW_New_Channel.Text = TSW_New_Channel.Checked ? "Yes" : "No";
            DDL_Webseite.Visible = !TSW_New_Channel.Checked;
            LAB_Webseite.Visible = !TSW_New_Channel.Checked;
        }

        private void BTN_Accept_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(TXB_Folder.Text))
            {
                MessageBox.Show(TXT.TXT_Description("Geben sie den Ordner für die Aufnahmen an."));
                TXB_Folder.Focus();
            }
            else
            {
                IniFile.Write(Parameter.INI_Common, "Directory", "Records", TXB_Folder.Text);
                var langItem = DDL_Languages.SelectedItem as ListDataItem;
                IniFile.Write(Parameter.INI_Common, "Language", "Files", Convert.ToString(langItem?.Value));
                IniFile.Write(Parameter.INI_Common, "Record", "RecordTime", Convert.ToString(30));
                IniFile.Write(Parameter.INI_Common, "Browser", "Product", Convert.ToString(-1));
                TXT txt = new TXT(Convert.ToString(langItem?.Value));
                DialogResult = DialogResult.OK;
            }
        }
    }
}
