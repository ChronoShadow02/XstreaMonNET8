using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    // Nota: partial para que la otra mitad (Designer) complete la clase
    public partial class Dialog_Model_Info : Form
    {
        public Dialog_Model_Info()
        {
            InitializeComponent();
            this.Load += Dialog_Model_Info_Load;
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void Dialog_Model_Info_Load(object sender, EventArgs e)
        {
            BTN_Übernehmen.Text = TXT.TXT_Description("Übernehmen");
            BTN_Abbrechen.Text = TXT.TXT_Description("Abbrechen");
        }
    }
}
