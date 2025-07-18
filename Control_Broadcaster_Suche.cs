#nullable disable
namespace XstreaMonNET8
{
    public partial class Control_Broadcaster_Suche : UserControl
    {
        private int Pri_Website_ID;
        private Image Pri_Website_Logo;
        private readonly string Pri_Beschreibung;
        private readonly Image Pri_Profil_Image;
        private readonly string Pri_Profil_Online;
        private readonly string Pri_Broadcaster_Name;
        private readonly bool Pri_Online;
        private readonly Channel_Info Pri_Channel_Info;

        public Control_Broadcaster_Suche(Channel_Info Broadcast_Info)
        {
            this.Paint += new PaintEventHandler(this.Control_Broadcaster_Suche_Paint);
            this.DoubleClick += new EventHandler(this.MIT_Uebernehmen_Click);
            this.Pri_Website_ID = 0;
            this.InitializeComponent();
            this.Pri_Channel_Info = Broadcast_Info;
            this.Pro_Website_ID = Broadcast_Info.Pro_Website_ID;
            this.Pri_Beschreibung = Broadcast_Info.Pro_Profil_Beschreibung;
            this.Pri_Profil_Image = Broadcast_Info.Pro_Profil_Image;
            this.Pri_Profil_Online = Broadcast_Info.Pro_Last_Online;
            this.Pri_Broadcaster_Name = Broadcast_Info.Pro_Name;
            this.Pri_Online = ValueBack.Get_CBoolean((object)Broadcast_Info.Pro_Online);
            this.MIT_Uebernehmen.Text = TXT.TXT_Description("Übernehmen");
            this.MIT_Webseite.Text = TXT.TXT_Description("Webseite öffnen");

            this.ContextMenuStrip = new ContextMenuStrip();
            this.ContextMenuStrip.Items.Add(this.MIT_Uebernehmen);
            this.ContextMenuStrip.Items.Add(this.MIT_Webseite);
        }

        internal int Pro_Website_ID
        {
            get => this.Pri_Website_ID;
            set
            {
                this.Pri_Website_ID = value;
                Class_Website classWebsite = Sites.Website_Find(this.Pri_Website_ID);
                if (classWebsite == null)
                    return;
                this.Pri_Website_Logo = (Image)new Bitmap(classWebsite.Pro_Image, 32, 32);
            }
        }

        private void Control_Broadcaster_Suche_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen((Brush)new SolidBrush(Color.DarkGray)), 1, 1, checked(this.Width - 1), 1);
            int x = 4;
            int y = 4;
            if (this.Pri_Website_Logo != null)
            {
                e.Graphics.DrawImage(this.Pri_Website_Logo, x, y, 24, 24);
                checked { x += 28; }
            }
            if (this.Pri_Beschreibung != null && this.Pri_Beschreibung.ToString() != "")
            {
                e.Graphics.DrawString(this.Pri_Beschreibung, this.Font, (Brush)new SolidBrush(Color.Black), (float)x, (float)y);
                checked { y += 16; }
            }
            if (this.Pri_Profil_Online != null)
                e.Graphics.DrawString(TXT.TXT_Description("Letzte Sendung") + ": " + this.Pri_Profil_Online, this.Font, (Brush)new SolidBrush(Color.Black), (float)x, (float)y);
            if (this.Pri_Profil_Image != null)
                e.Graphics.DrawImage(this.Pri_Profil_Image, checked(this.Width - 44), 4, 40, 40);
            if (this.Pri_Online)
                e.Graphics.FillEllipse((Brush)new SolidBrush(Color.Green), checked(this.Width - 16), 6, 10, 10);
            else
                e.Graphics.FillEllipse((Brush)new SolidBrush(Color.Gray), checked(this.Width - 16), 6, 10, 10);
        }

        private void MIT_Uebernehmen_Click(object sender, EventArgs e)
        {
            Such_Item_AcceptEventHandler suchItemAcceptEvent = this.Such_Item_Accept;
            if (suchItemAcceptEvent == null)
                return;
            suchItemAcceptEvent(this.Pri_Channel_Info);
        }

        private void MIT_Webseite_Click(object sender, EventArgs e)
        {
            Sites.WebSiteOpen(this.Pro_Website_ID, this.Pri_Broadcaster_Name);
        }

        internal event Control_Broadcaster_Suche.Such_Item_AcceptEventHandler Such_Item_Accept;

        internal delegate void Such_Item_AcceptEventHandler(Channel_Info Info_Select);
    }
}
