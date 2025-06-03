using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XstreaMonNET8
{
    public partial class Con_User_Online : UserControl
    {
        private bool Pri_Is_Current;
        private Class_Model _Pri_Model_Class;

        /// <summary>Se dispara al hacer doble-click externo.</summary>
        internal event Con_User_Online_DoubleClickEventHandler Con_User_Online_DoubleClick;

        /// <summary>URL construida (online/offline).</summary>
        internal string Pro_Model_URL { get; set; }

        public Con_User_Online()
        {
            InitializeComponent();
            this.Paint += Con_User_Online_Paint!;
            this.DoubleClick += Con_DoubleClick!;
            this.Load += Con_User_Online_Load!;
            this.Pri_Is_Current = false;
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>El modelo asociado a este control.</summary>
        internal Class_Model Pro_Class_Model
        {
            get => Pri_Model_Class;
            set
            {
                Pri_Model_Class = value;
                var site = Sites.Website_Find(value.Pro_Website_ID);
                if (site != null)
                {
                    string fmt = value.Get_Pro_Model_Online()
                        ? site.Pro_Model_URL
                        : site.Pro_Model_Offline_URL;
                    Pro_Model_URL = string.Format(fmt, value.Pro_Model_Name);
                }
                this.Visible = value.Get_Pro_Model_Online();
            }
        }

        private Class_Model Pri_Model_Class
        {
            get => _Pri_Model_Class;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                var onOnline = new Class_Model.Model_Online_ChangeEventHandler(Model_Online_Change);
                var onRecord = new Class_Model.Model_Record_ChangeEventHandler(Model_Record_Change);

                if (_Pri_Model_Class != null)
                {
                    _Pri_Model_Class.Model_Online_Change -= onOnline;
                    _Pri_Model_Class.Model_Record_Change -= onRecord;
                }

                _Pri_Model_Class = value;

                if (_Pri_Model_Class != null)
                {
                    _Pri_Model_Class.Model_Online_Change += onOnline;
                    _Pri_Model_Class.Model_Record_Change += onRecord;
                }
            }
        }

        /// <summary>Resalta este control si es el actual.</summary>
        internal bool Pro_Is_Current
        {
            set
            {
                Pri_Is_Current = value;
                this.Refresh();
            }
        }

        private void Model_Online_Change(object sender)
        {
            this.Visible = _Pri_Model_Class.Get_Pro_Model_Online();
            this.BringToFront();
        }

        private void Model_Record_Change(bool running)
        {
            this.Refresh();
        }

        private void Con_User_Online_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var font = _Pri_Model_Class.Pro_Model_Favorite
                         ? new Font(this.Font, FontStyle.Bold)
                         : this.Font;

            g.DrawString(
                _Pri_Model_Class.Pro_Model_Name,
                font,
                new SolidBrush(Parameter.Fore_Color_Dark),
                20f, 4f);

            if (_Pri_Model_Class.Pro_Model_Stream_Record != null)
                g.DrawImage(Resources.RecordAutomatic16, 2, 5);
            else if (_Pri_Model_Class.Pro_Model_Record)
                g.DrawImage(Resources.RecordWait16, 2, 5);
            else if (_Pri_Model_Class.Get_Pro_Model_Online())
                g.DrawImage(
                  _Pri_Model_Class.Timer_Online_Change!.BGW_Result == 1
                    ? Resources.Model_Online16
                    : Resources.Model_Online_Key16,
                  2, 5);
            else
                g.DrawImage(Resources.Model_Offline16, 2, 5);

            if (Pri_Is_Current)
                g.DrawRectangle(new Pen(Color.OrangeRed, 2f),
                                new Rectangle(0, 0, this.Width, this.Height));

            g.DrawLine(new Pen(Parameter.Fore_Color_Hell, 1f),
                       1, 1, this.Width - 1, 1);
        }

        private void Con_DoubleClick(object sender, EventArgs e)
        {
            Open_Page();
        }

        private void Open_Page()
        {
            try
            {
                if (this.ParentForm is CamBrowser cb)
                    cb.WebView_Source = Pro_Model_URL;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Con_User_Online.Open_Page");
            }

            Con_User_Online_DoubleClick?.Invoke(this);
        }

        private void Con_User_Online_Load(object sender, EventArgs e)
        {
            CMI_Open_Site.Text = TXT.TXT_Description("Webseite öffnen");
        }

        private void CMI_Open_Site_Click(object sender, EventArgs e)
        {
            Open_Page();
        }

        internal delegate void Con_User_Online_DoubleClickEventHandler(Con_User_Online Sender);
    }
}
