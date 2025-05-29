using System.ComponentModel;
using System.Data;
using System.Data.OleDb;

namespace XstreaMonNET8
{
    public class Control_Online_Time : UserControl
    {
        private IContainer components;
        private readonly DateTime Pro_Online_Day;
        private readonly List<Online_Minuten> Online_List;
        private readonly List<Online_Minuten> Record_List;

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

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            Name = nameof(Control_Online_Time);
            Size = new Size(240, 35);
            ResumeLayout(false);
        }

        internal Control_Online_Time(DateTime Online_Day, Guid Model_GUID)
        {
            Load += Control_Online_Time_Load;
            Paint += Control_Online_Time_Paint;
            Online_List = new List<Online_Minuten>();
            Record_List = new List<Online_Minuten>();
            InitializeComponent();
            Pro_Online_Day = Online_Day;

            using OleDbConnection connection = new(Database_Connect.Aktiv_Datenbank());
            connection.Open();
            if (connection.State != ConnectionState.Open) return;

            string fechaIni = ValueBack.Get_SQL_Date(Pro_Online_Day);
            string fechaFin = ValueBack.Get_SQL_Date(Pro_Online_Day.AddDays(1));

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                $"Select * from DT_Online where Model_GUID = '{Model_GUID}' AND Online_Beginn <= {fechaFin} AND Online_Ende > {fechaIni}",
                connection))
            {
                using (DataSet ds = new DataSet())
                {
                    adapter.Fill(ds, "DT_Online");
                    foreach (DataRow row in ds.Tables["DT_Online"]!.Rows)
                    {
                        DateTime beginn = Convert.ToDateTime(row["Online_Beginn"]);
                        DateTime ende = Convert.ToDateTime(row["Online_Ende"]);
                        Online_List.Add(new Online_Minuten
                        {
                            Beginn = beginn.Hour * 60 + beginn.Minute,
                            Ende = ende.Hour * 60 + ende.Minute
                        });
                    }
                }
            }

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(
                $"Select * from DT_Record where User_GUID = '{Model_GUID}' AND Record_Beginn <= {fechaFin} AND Record_Ende > {fechaIni}",
                connection))
            {
                using DataSet ds = new();
                adapter.Fill(ds, "DT_Record");
                foreach (DataRow row in ds.Tables["DT_Record"].Rows)
                {
                    DateTime beginn = Convert.ToDateTime(row["Record_Beginn"]);
                    DateTime ende = Convert.ToDateTime(row["Record_Ende"]);
                    Record_List.Add(new Online_Minuten
                    {
                        Beginn = beginn.Hour * 60 + beginn.Minute,
                        Ende = ende.Hour * 60 + ende.Minute
                    });
                }
            }
        }

        private void Control_Online_Time_Load(object sender, EventArgs e)
        {
            Height = 40;
        }

        private void Control_Online_Time_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            string diaTexto = $"{Pro_Online_Day:dd.MM} {TXT.TXT_Description(Pro_Online_Day.DayOfWeek.ToString())}";
            Font fuente = Font;
            Brush brocha = new SolidBrush(BackColor == Color.Black ? Color.DimGray : Color.DarkGray);
            g.DrawString(diaTexto, fuente, brocha, 4f, 4f);

            for (int i = 0; i <= 24; i++)
            {
                int x = (int)((Width - 4) / 24.0 * i) + 2;
                int y1 = i % 3 == 0 ? 30 : 34;
                g.DrawLine(new Pen(Color.Gray), x, y1, x, 38);
            }

            foreach (var online in Online_List)
            {
                int x = (int)(online.Beginn / 1440.0 * (Width - 4)) + 2;
                int ancho = (int)((online.Ende - online.Beginn) / 1440.0 * (Width - 4));
                g.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Green)), x, 26, ancho, 10);
            }

            foreach (var record in Record_List)
            {
                int x = (int)(record.Beginn / 1440.0 * (Width - 4)) + 2;
                int ancho = (int)((record.Ende - record.Beginn) / 1440.0 * (Width - 4));
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Red)), x, 16, ancho, 10);
            }

            g.DrawLine(new Pen(Color.DarkGray), 2, Height - 2, Width - 2, Height - 2);
        }
    }

    public class Online_Minuten
    {
        public int Beginn { get; set; }
        public int Ende { get; set; }
    }
}
