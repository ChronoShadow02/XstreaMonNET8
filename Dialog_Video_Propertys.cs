// Archivo: Dialog_Video_Propertys.cs
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;

namespace XstreaMonNET8
{
    public partial class Dialog_Video_Propertys : Form
    {
        internal static string Pri_File_Name;
        private int Pri_File_Lenght;

        public Dialog_Video_Propertys(string File_Name)
        {
            this.Load += new EventHandler(this.Dialog_Video_Propertys_Load);
            this.Pri_File_Lenght = 0;
            InitializeComponent();

            try
            {
                Dialog_Video_Propertys.Pri_File_Name = File_Name;

                // Inicializa ComboBox equivalente a RadDropDownList
                this.DDL_Provider.Items.Clear();
                this.DDL_Provider.Items.Add(new RadListDataItem
                {
                    Text = TXT.TXT_Description("Keine Angabe"),
                    Value = (object)(-1)
                });

                foreach (Class_Website website in Sites.Website_List)
                {
                    this.DDL_Provider.Items.Add(new RadListDataItem
                    {
                        Text = website.Pro_Name,
                        Value = (object)website.Pro_ID
                    });
                }

                this.DDL_Provider.DisplayMember = "Text";
                this.DDL_Provider.ValueMember = "Value";
                this.DDL_Provider.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.DDL_Provider.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.DDL_Provider.Sorted = true;
                this.DDL_Provider.SelectedItem = null;

                this.VDB_File_Load();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.New - " + File_Name);
            }
        }

        private void BTN_Übernehmen_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Dialog_Video_Propertys.Pri_File_Name);
                string currentBaseName = fileInfo.Name.Replace(fileInfo.Extension, "");
                if (!string.Equals(currentBaseName, this.TXB_File_Name.Text, StringComparison.OrdinalIgnoreCase))
                {
                    DialogResult result = MessageBox.Show(
                        string.Format(TXT.TXT_Description("Möchten Sie die Datei in {0} umbenennen?"), this.TXB_File_Name.Text),
                        TXT.TXT_Description("Datei umbenennen"),
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Cancel)
                        return;
                    if (result == DialogResult.Yes)
                    {
                        string destFileName = Path.Combine(
                            fileInfo.Directory.FullName,
                            Modul_Ordner.DateiName(fileInfo.Directory.FullName, this.TXB_File_Name.Text + fileInfo.Extension));

                        File.Move(Dialog_Video_Propertys.Pri_File_Name, destFileName);
                        File.Move(Dialog_Video_Propertys.Pri_File_Name + ".vdb", destFileName + ".vdb");
                        Dialog_Video_Propertys.Pri_File_Name = destFileName;
                    }
                }

                using (DataSet dataSet = new DataSet())
                {
                    dataSet.DataSetName = "RecordFile";
                    DataTable table = new DataTable
                    {
                        TableName = "DT_RecordFile"
                    };
                    table.Columns.Add("Channel_Name");
                    table.Columns.Add("Record_Beginn");
                    table.Columns.Add("Record_Ende");
                    table.Columns.Add("Record_Länge_Minuten");
                    table.Columns.Add("Record_Resolution");
                    table.Columns.Add("Record_FrameRate");
                    table.Columns.Add("Record_Name");
                    table.Columns.Add("Record_Site");
                    table.Columns.Add("Video_Preview", typeof(byte[]));
                    table.Columns.Add("Video_Timeline", typeof(byte[]));
                    table.Columns.Add("Video_Tiles", typeof(byte[]));

                    DataRow row = table.NewRow();
                    row["Channel_Name"] = this.TXB_Channel_Name.Text;
                    row["Record_Beginn"] = this.DTP_Start.Value;
                    row["Record_Name"] = this.TXB_File_Name.Text;
                    row["Record_Site"] = (this.DDL_Provider.SelectedItem is RadListDataItem selItem) ? selItem.Value : (object)(-1);
                    row["Record_Ende"] = this.LAB_Ende.Text;
                    row["Record_Länge_Minuten"] = ValueBack.Get_Zahl_Extract_From_String(this.LAB_Länge.Text);
                    row["Record_Resolution"] = ValueBack.Get_Zahl_Extract_From_String(this.LAB_Resolution.Text);
                    row["Record_FrameRate"] = ValueBack.Get_Zahl_Extract_From_String(this.LAB_Framerate.Text);
                    row["Video_Timeline"] = ValueBack.Get_CImageToByte(this.LAB_Timeline_Image.BackgroundImage, ImageFormat.Jpeg);
                    row["Video_Tiles"] = ValueBack.Get_CImageToByte(this.LAB_Tiles_Image.BackgroundImage, ImageFormat.Jpeg);
                    row["Video_Preview"] = ValueBack.Get_CImageToByte(this.LAB_Preview_Image.BackgroundImage, ImageFormat.Jpeg);
                    table.Rows.Add(row);

                    dataSet.Tables.Add(table);
                    dataSet.WriteXml(Dialog_Video_Propertys.Pri_File_Name + ".vdb", XmlWriteMode.WriteSchema);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.BTN_Übernehmen_Click");
            }
        }

        private void VDB_File_Load()
        {
            try
            {
                if (File.Exists(Dialog_Video_Propertys.Pri_File_Name))
                {
                    FileInfo fileInfo = new FileInfo(Dialog_Video_Propertys.Pri_File_Name);
                    this.TXB_File_Name.Text = fileInfo.Name.Replace(fileInfo.Extension, "");
                    this.LAB_Create.Text = fileInfo.CreationTime.ToString();
                    this.LAB_Change.Text = fileInfo.LastWriteTime.ToString();
                    this.LAB_File_Size.Text = ValueBack.Get_Numeric2Bytes((double)fileInfo.Length);
                    this.LAB_File_Typ.Text = fileInfo.Extension;
                }

                string vdbPath = Dialog_Video_Propertys.Pri_File_Name + ".vdb";
                if (!File.Exists(vdbPath))
                    return;

                using (DataSet dataSet = new DataSet())
                {
                    dataSet.ReadXml(vdbPath);
                    if (dataSet.Tables.Count != 1)
                        return;

                    DataTable table = dataSet.Tables[0];
                    DataRow row = table.Rows[0];

                    this.TXB_Channel_Name.Text = row["Channel_Name"].ToString();
                    this.DTP_Start.Value = Convert.ToDateTime(row["Record_Beginn"]);

                    object rawEnde = row["Record_Ende"];
                    this.LAB_Ende.Text = (rawEnde == DBNull.Value)
                        ? Convert.ToString(row["Record_Beginn"])
                        : rawEnde.ToString();

                    double frameRateValue = ValueBack.Get_CDouble(row["Record_FrameRate"]) / 1000.0;
                    this.LAB_Framerate.Text = frameRateValue.ToString() + " " + TXT.TXT_Description("Bilder/Sekunde");

                    this.LAB_Resolution.Text = row["Record_Resolution"].ToString();
                    
                    int lengthMinutes = ValueBack.Get_CInteger(row["Record_Länge_Minuten"]);
                    this.Pri_File_Lenght = lengthMinutes * 60;
                    this.LAB_Länge.Text = lengthMinutes.ToString() + " " + TXT.TXT_Description("Minuten");

                    object siteObj = row["Record_Site"];
                    int siteValue = (siteObj == DBNull.Value) ? -1 : ValueBack.Get_CInteger(siteObj);
                    foreach (RadListDataItem item in this.DDL_Provider.Items)
                    {
                        if ((int)item.Value == siteValue)
                        {
                            this.DDL_Provider.SelectedItem = item;
                            break;
                        }
                    }

                    double maxTick = lengthMinutes * 6.0;
                    this.TRB_VideoPosition.Maximum = (int)Math.Round(maxTick);
                    this.TRB_VideoPosition.LargeChange = (int)Math.Round(this.TRB_VideoPosition.Maximum / 5.0);
                    this.TRB_VideoPosition.SmallChange = (int)Math.Round(this.TRB_VideoPosition.Maximum / 50.0);

                    if (table.Columns.Contains("Video_Tiles") && row["Video_Tiles"] != DBNull.Value)
                    {
                        byte[] tilesBytes = (byte[])row["Video_Tiles"];
                        this.LAB_Tiles_Image.BackgroundImage = ValueBack.Get_CBytesToImage(tilesBytes);
                    }

                    if (table.Columns.Contains("Video_Preview") && row["Video_Preview"] != DBNull.Value)
                    {
                        byte[] previewBytes = (byte[])row["Video_Preview"];
                        this.LAB_Preview_Image.BackgroundImage = ValueBack.Get_CBytesToImage(previewBytes);
                    }

                    if (table.Columns.Contains("Video_Timeline") && row["Video_Timeline"] != DBNull.Value)
                    {
                        byte[] timelineBytes = (byte[])row["Video_Timeline"];
                        this.LAB_Timeline_Image.BackgroundImage = ValueBack.Get_CBytesToImage(timelineBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Video_Preview.VDB_File_Load");
            }
        }

        private void BTN_Tiles_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Task<Bitmap> task = Vorschau.Vorschau_Tiles_Create(Dialog_Video_Propertys.Pri_File_Name, 340, 224, this.Pri_File_Lenght);
                Bitmap bmp = task.Result;
                this.LAB_Tiles_Image.BackgroundImage = bmp;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.BTN_Tiles_Refresh_Click");
            }
        }

        private void BTN_Timeline_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string priFileName = Dialog_Video_Propertys.Pri_File_Name;
                int videoLengthSeconds = this.Pri_File_Lenght;
                Size previewSize = this.LAB_Preview_Image.BackgroundImage?.Size ?? new Size(0, 0);
                bool isLandscape = previewSize.Width > previewSize.Height;

                Task<Bitmap> task = Vorschau.Vorschau_Image_Create(priFileName, 400, 26, videoLengthSeconds, isLandscape);
                Bitmap bmp = task.Result;
                this.LAB_Timeline_Image.BackgroundImage = bmp;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.BTN_Timeline_Refresh_Click");
            }
        }

        private void TRB_VideoPosition_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.TRB_VideoPosition.ValueChanged -= new EventHandler(this.TRB_VideoPosition_ValueChanged);
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (DoWorkEventHandler)((a0, a1) => this.PAN_Vorschau_Image_Create());
                backgroundWorker.RunWorkerCompleted += (RunWorkerCompletedEventHandler)((a0, a1) => this.PAN_Vorschau_Finaly());
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.TRB_VideoPosition_ValueChanged");
            }
        }

        private void PAN_Vorschau_Image_Create()
        {
            try
            {
                int framePosition = (int)Math.Round(this.TRB_VideoPosition.Value * 10.0);
                Task<Bitmap> task = CreateThumbFromVideo.GenerateThumb(
                    Dialog_Video_Propertys.Pri_File_Name, framePosition, 340, 194);
                Bitmap bmp = task.Result;
                this.LAB_Preview_Image.BackgroundImage = bmp;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.PAN_Vorschau_Image_Create");
            }
        }

        private void PAN_Vorschau_Finaly()
        {
            try
            {
                this.TRB_VideoPosition.ValueChanged += new EventHandler(this.TRB_VideoPosition_ValueChanged);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.PAN_Vorschau_Finaly");
            }
        }

        private void DTP_Start_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.LAB_Ende.Text = DateTime.Now.AddMinutes(this.Pri_File_Lenght).ToString();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.DTP_Start_ValueChanged");
            }
        }

        private void BTN_Abbrechen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.BTN_Abbrechen_Click");
            }
        }

        private void Dialog_Video_Propertys_Load(object sender, EventArgs e)
        {
            TXT.Control_Languages(this);
            this.Text = TXT.TXT_Description("Video Eigenschaften");
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(Dialog_Video_Propertys.Pri_File_Name))
                    return;

                try
                {
                    using (Class_MediaInfo classMediaInfo = new Class_MediaInfo(
                        Dialog_Video_Propertys.Pri_File_Name,
                        this.TXB_Channel_Name.Text,
                        (this.DDL_Provider.SelectedItem is RadListDataItem selItem) ? Convert.ToInt32(selItem.Value) : -1,
                        this.DTP_Start.Value))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.LAB_Ende.Text = classMediaInfo.Pro_Record_Ende.ToString();
                        this.Pri_File_Lenght = classMediaInfo.Pro_Record_Länge;
                        this.LAB_Länge.Text = classMediaInfo.Pro_Record_Länge.ToString() + " " + TXT.TXT_Description("Minuten");
                        this.LAB_Resolution.Text = classMediaInfo.Pro_Record_Resolution;
                        this.LAB_Framerate.Text = classMediaInfo.Pro_Record_FrameRate + " " + TXT.TXT_Description("Bilder/Sekunde");
                        this.LAB_Timeline_Image.BackgroundImage = classMediaInfo.Pro_TimeLine_Image;
                        this.LAB_Tiles_Image.BackgroundImage = classMediaInfo.Pro_Tiles_Image;
                        this.LAB_Preview_Image.BackgroundImage = classMediaInfo.Pro_Preview_Image;

                        double maxTick = this.Pri_File_Lenght * 6.0;
                        this.TRB_VideoPosition.Maximum = (int)Math.Round(maxTick);
                        this.TRB_VideoPosition.LargeChange = (int)Math.Round(this.TRB_VideoPosition.Maximum / 5.0);
                        this.TRB_VideoPosition.SmallChange = (int)Math.Round(this.TRB_VideoPosition.Maximum / 50.0);
                    }
                }
                catch (Exception ex)
                {
                    Parameter.Error_Message(ex, "Class_Stream_Record.Video_File_Info - " + Dialog_Video_Propertys.Pri_File_Name);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Dialog_Video_Propertys.BTN_Refresh_Click");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        internal class RadListDataItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
