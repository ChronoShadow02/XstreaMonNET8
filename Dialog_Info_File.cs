#nullable disable
namespace XstreaMonNET8
{
    public partial class Dialog_Info_File : Form
    {
        public Dialog_Info_File()
        {
            Load += new EventHandler(Dialog_Info_File_Load);
            InitializeComponent();
        }

        private void Dialog_Info_File_Load(object sender, EventArgs e)
        {
            // Aquí asignamos el texto real de la ventana y de cada label
            Text = TXT.TXT_Description("Dateiname Platzhalter");

            RadLabel1.Text = TXT.TXT_Description("{WK} Kürzel der Webseite");
            RadLabel2.Text = TXT.TXT_Description("{Year} Jahr der Aufnahme");
            RadLabel3.Text = TXT.TXT_Description("{Month} Monat der Aufnahme");
            RadLabel4.Text = TXT.TXT_Description("{MO} Monat der Aufnahme zweistellig");
            RadLabel5.Text = TXT.TXT_Description("{Day} Tag der Aufnahme");
            RadLabel6.Text = TXT.TXT_Description("{DY} Tag der Aufnahme zweistellig");
            RadLabel7.Text = TXT.TXT_Description("{Hour} Stunde der Aufnahme");
            RadLabel8.Text = TXT.TXT_Description("{HO} Stunde der Aufnahme zweistellig");
            RadLabel9.Text = TXT.TXT_Description("{Minute} Minute der Aufnahme");
            RadLabel10.Text = TXT.TXT_Description("{MI} Minute der Aufnahme zweistellig");
            RadLabel11.Text = TXT.TXT_Description("{Seconds} Sekunde der Aufnahme");
            RadLabel12.Text = TXT.TXT_Description("{SE} Sekunde der Aufnahme zweistellig");
            RadLabel13.Text = TXT.TXT_Description("{Name} Kanalbezeichnung oder Kanalname (wenn vorhanden)");
        }
    }
}
