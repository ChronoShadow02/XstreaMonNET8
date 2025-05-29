using System.ComponentModel;

namespace XstreaMonNET8
{
    public class Control_Broadcaster_Suche : UserControl
    {
        private ContextMenuStrip menuContextual;
        private ToolStripMenuItem itemAceptar;
        private ToolStripMenuItem itemWeb;

        private int Pri_Website_ID;
        private Image Pri_Website_Logo;
        private readonly string Pri_Beschreibung;
        private readonly Image Pri_Profil_Image;
        private readonly string Pri_Profil_Online;
        private readonly string Pri_Broadcaster_Name;
        private readonly bool Pri_Online;
        private readonly Channel_Info Pri_Channel_Info;

        public Control_Broadcaster_Suche(Channel_Info broadcastInfo)
        {
            Pri_Channel_Info = broadcastInfo;
            Pri_Beschreibung = broadcastInfo.Pro_Profil_Beschreibung;
            Pri_Profil_Image = broadcastInfo.Pro_Profil_Image;
            Pri_Profil_Online = broadcastInfo.Pro_Last_Online;
            Pri_Broadcaster_Name = broadcastInfo.Pro_Name;
            Pri_Online = ValueBack.Get_CBoolean(broadcastInfo.Pro_Online);
            Pro_Website_ID = broadcastInfo.Pro_Website_ID;

            Size = new Size(584, 48);
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;

            Paint += Control_Broadcaster_Suche_Paint;
            DoubleClick += MIT_Uebernehmen_Click;

            InitializeContextMenu();
            ContextMenuStrip = menuContextual;
        }

        private void InitializeContextMenu()
        {
            menuContextual = new ContextMenuStrip();

            itemAceptar = new ToolStripMenuItem(TXT.TXT_Description("Übernehmen"));
            itemWeb = new ToolStripMenuItem(TXT.TXT_Description("Webseite öffnen"));

            itemAceptar.Click += MIT_Uebernehmen_Click;
            itemWeb.Click += MIT_Webseite_Click;

            menuContextual.Items.AddRange(new ToolStripItem[] { itemAceptar, itemWeb });
        }

        public int Pro_Website_ID
        {
            get => Pri_Website_ID;
            set
            {
                Pri_Website_ID = value;
                var website = Sites.Website_Find(value);
                if (website != null)
                {
                    Pri_Website_Logo = new Bitmap(website.Pro_Image, 32, 32);
                }
            }
        }

        private void Control_Broadcaster_Suche_Paint(object sender, PaintEventArgs e)
        {
            using Pen pen = new Pen(Color.DarkGray);
            e.Graphics.DrawLine(pen, 1, 1, Width - 1, 1);

            int x = 4;
            int y = 4;

            if (Pri_Website_Logo != null)
            {
                e.Graphics.DrawImage(Pri_Website_Logo, x, y, 24, 24);
                x += 28;
            }

            if (!string.IsNullOrEmpty(Pri_Beschreibung))
            {
                e.Graphics.DrawString(Pri_Beschreibung, Font, Brushes.Black, x, y);
                y += 16;
            }

            if (!string.IsNullOrEmpty(Pri_Profil_Online))
            {
                e.Graphics.DrawString($"{TXT.TXT_Description("Letzte Sendung")}: {Pri_Profil_Online}", Font, Brushes.Black, x, y);
            }

            if (Pri_Profil_Image != null)
            {
                e.Graphics.DrawImage(Pri_Profil_Image, Width - 44, 4, 40, 40);
            }

            Brush statusBrush = Pri_Online ? Brushes.Green : Brushes.Gray;
            e.Graphics.FillEllipse(statusBrush, Width - 16, 6, 10, 10);
        }

        private void MIT_Uebernehmen_Click(object sender, EventArgs e)
        {
            Such_Item_Accept?.Invoke(Pri_Channel_Info);
        }

        private void MIT_Webseite_Click(object sender, EventArgs e)
        {
            Sites.WebSiteOpen(Pro_Website_ID, Pri_Broadcaster_Name);
        }

        public delegate void Such_Item_AcceptEventHandler(Channel_Info infoSeleccionado);
        public event Such_Item_AcceptEventHandler Such_Item_Accept;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                menuContextual?.Dispose();
            }
            base.Dispose(disposing);
        }

        private IContainer components = null;
    }
}
