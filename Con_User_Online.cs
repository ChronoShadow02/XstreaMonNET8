using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    public class Con_User_Online : UserControl
    {
        private bool Pri_Is_Current;
        private Class_Model _Pri_Model_Class;

        public event Action<Con_User_Online>? Con_User_Online_DoubleClick;

        public string Pro_Model_URL { get; private set; }

        public bool Pro_Is_Current
        {
            set
            {
                Pri_Is_Current = value;
                this.Refresh();
            }
        }

        public Class_Model Pro_Class_Model
        {
            get => _Pri_Model_Class;
            set
            {
                if (_Pri_Model_Class != null)
                {
                    _Pri_Model_Class.Model_Online_Change -= Model_Online_Change;
                    _Pri_Model_Class.Model_Record_Change -= Model_Record_Change;
                }

                _Pri_Model_Class = value;

                if (_Pri_Model_Class != null)
                {
                    _Pri_Model_Class.Model_Online_Change += Model_Online_Change;
                    _Pri_Model_Class.Model_Record_Change += Model_Record_Change;

                    var site = Sites.Website_Find(_Pri_Model_Class.Pro_Website_ID);
                    if (site != null)
                    {
                        Pro_Model_URL = string.Format(
                            _Pri_Model_Class.Pro_Model_Online ? site.Pro_Model_URL : site.Pro_Model_Offline_URL,
                            _Pri_Model_Class.Pro_Model_Name);
                    }

                    this.Visible = _Pri_Model_Class.Pro_Model_Online;
                }
            }
        }

        public Con_User_Online()
        {
            this.DoubleClick += Con_DoubleClick;
            this.Load += Con_User_Online_Load;
            this.Size = new Size(220, 24);
            this.Cursor = Cursors.Hand;
        }

        private void Con_User_Online_Load(object? sender, EventArgs e)
        {
            this.ContextMenuStrip = new ContextMenuStrip();
            var abrirItem = new ToolStripMenuItem("Webseite anzeigen");
            abrirItem.Click += (s, ev) => Open_Page();
            this.ContextMenuStrip.Items.Add(abrirItem);
        }

        private void Con_DoubleClick(object? sender, EventArgs e) => Open_Page();

        private void Open_Page()
        {
            try
            {
                if (this.ParentForm is CamBrowser browser)
                {
                    var site = Sites.Website_Find(Pro_Class_Model.Pro_Website_ID);
                    if (site != null)
                    {
                        string url = string.Format(site.Pro_Model_URL, Pro_Class_Model.Pro_Model_Name);
                        browser.WebView_Source = url;
                    }
                }
                Con_User_Online_DoubleClick?.Invoke(this);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Con_User_Online.Open_Page");
            }
        }

        private void Model_Online_Change(object? sender)
        {
            if (_Pri_Model_Class != null)
            {
                this.Visible = _Pri_Model_Class.Pro_Model_Online;
                this.BringToFront();
            }
        }

        private void Model_Record_Change(bool recordRun) => this.Refresh();

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_Pri_Model_Class == null) return;

            var fontToUse = _Pri_Model_Class.Pro_Model_Favorite
                ? new Font(this.Font, FontStyle.Bold)
                : this.Font;

            using var textBrush = new SolidBrush(Parameter.Fore_Color_Dark);
            e.Graphics.DrawString(_Pri_Model_Class.Pro_Model_Name, fontToUse, textBrush, 20f, 4f);

            if (_Pri_Model_Class.Pro_Model_Stream_Record != null)
                e.Graphics.DrawImage(Resources.RecordAutomatic16, 2, 5);
            else if (_Pri_Model_Class.Pro_Model_Record)
                e.Graphics.DrawImage(Resources.RecordWait16, 2, 5);
            else if (_Pri_Model_Class.Pro_Model_Online)
            {
                if (_Pri_Model_Class.Timer_Online_Change?.BGW_Result == 1)
                    e.Graphics.DrawImage(Resources.Model_Online16, 2, 5);
                else
                    e.Graphics.DrawImage(Resources.Model_Online_Key16, 2, 5);
            }
            else
            {
                e.Graphics.DrawImage(Resources.Model_Offline16, 2, 5);
            }

            if (Pri_Is_Current)
            {
                using var pen = new Pen(Color.OrangeRed, 2f);
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width, this.Height);
            }

            using var linePen = new Pen(Parameter.Fore_Color_Hell);
            e.Graphics.DrawLine(linePen, 1, 1, this.Width - 1, 1);
        }
    }
}
