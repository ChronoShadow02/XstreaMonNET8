using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace XstreaMonNET8
{
    public partial class Control_Online_Time : UserControl
    {
        private readonly DateTime Pro_Online_Day;
        private readonly List<Online_Minuten> Online_List = new List<Online_Minuten>();
        private readonly List<Online_Minuten> Record_List = new List<Online_Minuten>();

        public Control_Online_Time(DateTime Online_Day, Guid Model_GUID)
        {
            InitializeComponent();

            // enganchar eventos
            this.Load += Control_Online_Time_Load;
            this.Paint += Control_Online_Time_Paint;

            Pro_Online_Day = Online_Day;

            // cargar datos de online
            using var conn = new OleDbConnection(Database_Connect.Aktiv_Datenbank());
            conn.Open();
            if (conn.State != ConnectionState.Open) return;

            string fechaIni = ValueBack.Get_SQL_Date(Pro_Online_Day);
            string fechaFin = ValueBack.Get_SQL_Date(Pro_Online_Day.AddDays(1));

            // Online
            using (var da = new OleDbDataAdapter(
                $"SELECT * FROM DT_Online WHERE Model_GUID='{Model_GUID}' " +
                $"AND Online_Beginn<={fechaFin} AND Online_Ende>{fechaIni}", conn))
            {
                var ds = new DataSet();
                da.Fill(ds, "DT_Online");
                foreach (DataRow r in ds.Tables["DT_Online"].Rows)
                {
                    var b = Convert.ToDateTime(r["Online_Beginn"]);
                    var e = Convert.ToDateTime(r["Online_Ende"]);
                    Online_List.Add(new Online_Minuten
                    {
                        Beginn = b.Hour * 60 + b.Minute,
                        Ende = e.Hour * 60 + e.Minute
                    });
                }
            }

            // Record
            using (var da = new OleDbDataAdapter(
                $"SELECT * FROM DT_Record WHERE User_GUID='{Model_GUID}' " +
                $"AND Record_Beginn<={fechaFin} AND Record_Ende>{fechaIni}", conn))
            {
                var ds = new DataSet();
                da.Fill(ds, "DT_Record");
                foreach (DataRow r in ds.Tables["DT_Record"].Rows)
                {
                    var b = Convert.ToDateTime(r["Record_Beginn"]);
                    var e = Convert.ToDateTime(r["Record_Ende"]);
                    Record_List.Add(new Online_Minuten
                    {
                        Beginn = b.Hour * 60 + b.Minute,
                        Ende = e.Hour * 60 + e.Minute
                    });
                }
            }
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                    components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void Control_Online_Time_Load(object sender, EventArgs e)
        {
            // ajustar altura si hace falta
            this.Height = 40;
        }

        private void Control_Online_Time_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            string txt = $"{Pro_Online_Day:dd.MM} {TXT.TXT_Description(Pro_Online_Day.DayOfWeek.ToString())}";
            using var brush = new SolidBrush(this.BackColor == Color.Black ? Color.DimGray : Color.DarkGray);
            g.DrawString(txt, this.Font, brush, 4f, 4f);

            // marcas horarias
            for (int i = 0; i <= 24; i++)
            {
                int x = (int)((this.Width - 4) / 24.0 * i) + 2;
                int y1 = (i % 3 == 0) ? 30 : 34;
                g.DrawLine(Pens.Gray, x, y1, x, 38);
            }

            // franjas online
            foreach (var seg in Online_List)
            {
                int x = (int)(seg.Beginn / 1440.0 * (this.Width - 4)) + 2;
                int w = (int)((seg.Ende - seg.Beginn) / 1440.0 * (this.Width - 4));
                using var f = new SolidBrush(Color.FromArgb(150, Color.Green));
                g.FillRectangle(f, x, 26, w, 10);
            }

            // franjas grabación
            foreach (var seg in Record_List)
            {
                int x = (int)(seg.Beginn / 1440.0 * (this.Width - 4)) + 2;
                int w = (int)((seg.Ende - seg.Beginn) / 1440.0 * (this.Width - 4));
                using var f = new SolidBrush(Color.FromArgb(200, Color.Red));
                g.FillRectangle(f, x, 16, w, 10);
            }

            g.DrawLine(Pens.DarkGray, 2, this.Height - 2, this.Width - 2, this.Height - 2);
        }
    }
}
