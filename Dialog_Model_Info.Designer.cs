using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    [DesignerCategory("Form")]
    partial class Dialog_Model_Info
    {
        // Para que Dispose() encuentre esto
        private IContainer components = null;

        // Campos (antes RadPanel1, BTN_Übernehmen, etc.)
        internal Panel RadPanel1;
        internal Button BTN_Übernehmen;
        internal Button BTN_Abbrechen;
        internal TextBox TXB_Memo;

        /// <summary>
        /// Método requerido para el diseñador — no modificar manualmente aquí,
        /// sino sólo a través del editor de Windows Forms.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.RadPanel1 = new Panel();
            this.BTN_Übernehmen = new Button();
            this.BTN_Abbrechen = new Button();
            this.TXB_Memo = new TextBox();

            // 
            // RadPanel1
            // 
            this.RadPanel1.Controls.Add(this.BTN_Übernehmen);
            this.RadPanel1.Controls.Add(this.BTN_Abbrechen);
            this.RadPanel1.Dock = DockStyle.Bottom;
            this.RadPanel1.Location = new Point(0, 312);
            this.RadPanel1.Name = "RadPanel1";
            this.RadPanel1.Size = new Size(478, 44);
            this.RadPanel1.TabIndex = 4;
            // 
            // BTN_Übernehmen
            // 
            this.BTN_Übernehmen.DialogResult = DialogResult.OK;
            this.BTN_Übernehmen.Location = new Point(243, 10);
            this.BTN_Übernehmen.Name = "BTN_Übernehmen";
            this.BTN_Übernehmen.Size = new Size(110, 24);
            this.BTN_Übernehmen.TabIndex = 0;
            this.BTN_Übernehmen.Text = "Übernehmen";
            // 
            // BTN_Abbrechen
            // 
            this.BTN_Abbrechen.DialogResult = DialogResult.Cancel;
            this.BTN_Abbrechen.Location = new Point(359, 10);
            this.BTN_Abbrechen.Name = "BTN_Abbrechen";
            this.BTN_Abbrechen.Size = new Size(110, 24);
            this.BTN_Abbrechen.TabIndex = 1;
            this.BTN_Abbrechen.Text = "Abbrechen";
            // 
            // TXB_Memo
            // 
            this.TXB_Memo.AcceptsReturn = true;
            this.TXB_Memo.AcceptsTab = true;
            this.TXB_Memo.Dock = DockStyle.Fill;
            this.TXB_Memo.Location = new Point(0, 0);
            this.TXB_Memo.Multiline = true;
            this.TXB_Memo.Name = "TXB_Memo";
            this.TXB_Memo.ScrollBars = ScrollBars.Both;
            this.TXB_Memo.Size = new Size(478, 312);
            this.TXB_Memo.TabIndex = 5;
            // 
            // Dialog_Model_Info (Form)
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(478, 356);
            this.ControlBox = false;
            this.Controls.Add(this.TXB_Memo);
            this.Controls.Add(this.RadPanel1);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "Dialog_Model_Info";
            this.Text = "Info";
        }
    }
}
