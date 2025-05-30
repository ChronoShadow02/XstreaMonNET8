using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XstreaMonNET8
{
    public class Dialog_Model_Info : Form
    {
        private IContainer components;

        internal virtual Panel RadPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }
        internal virtual Button BTN_Übernehmen { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }
        internal virtual Button BTN_Abbrechen { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }
        internal virtual TextBox TXB_Memo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

        public Dialog_Model_Info()
        {
            Load += new EventHandler(Dialog_Model_Info_Load);
            InitializeComponent();
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

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            RadPanel1 = new Panel();
            BTN_Übernehmen = new Button();
            BTN_Abbrechen = new Button();
            TXB_Memo = new TextBox();

            // 
            // RadPanel1
            // 
            RadPanel1.Controls.Add(BTN_Übernehmen);
            RadPanel1.Controls.Add(BTN_Abbrechen);
            RadPanel1.Dock = DockStyle.Bottom;
            RadPanel1.Location = new Point(0, 312);
            RadPanel1.Name = "RadPanel1";
            RadPanel1.Size = new Size(478, 44);
            RadPanel1.TabIndex = 4;
            // 
            // BTN_Übernehmen
            // 
            BTN_Übernehmen.DialogResult = DialogResult.OK;
            BTN_Übernehmen.Location = new Point(243, 10);
            BTN_Übernehmen.Name = "BTN_Übernehmen";
            BTN_Übernehmen.Size = new Size(110, 24);
            BTN_Übernehmen.TabIndex = 0;
            BTN_Übernehmen.Text = "Übernehmen";
            // 
            // BTN_Abbrechen
            // 
            BTN_Abbrechen.DialogResult = DialogResult.Cancel;
            BTN_Abbrechen.Location = new Point(359, 10);
            BTN_Abbrechen.Name = "BTN_Abbrechen";
            BTN_Abbrechen.Size = new Size(110, 24);
            BTN_Abbrechen.TabIndex = 1;
            BTN_Abbrechen.Text = "Abbrechen";
            // 
            // TXB_Memo
            // 
            TXB_Memo.AcceptsReturn = true;
            TXB_Memo.AcceptsTab = true;
            TXB_Memo.Dock = DockStyle.Fill;
            TXB_Memo.Location = new Point(0, 0);
            TXB_Memo.Multiline = true;
            TXB_Memo.Name = "TXB_Memo";
            TXB_Memo.ScrollBars = ScrollBars.Both;
            TXB_Memo.Size = new Size(478, 312);
            TXB_Memo.TabIndex = 5;
            // 
            // Dialog_Model_Info (Form)
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 356);
            ControlBox = false;
            Controls.Add(TXB_Memo);
            Controls.Add(RadPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = nameof(Dialog_Model_Info);
            Text = "Info";
        }

        private void Dialog_Model_Info_Load(object sender, EventArgs e)
        {
            BTN_Übernehmen.Text = TXT.TXT_Description("Übernehmen");
            BTN_Abbrechen.Text = TXT.TXT_Description("Abbrechen");
        }
    }
}
