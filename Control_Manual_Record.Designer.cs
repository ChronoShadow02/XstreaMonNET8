using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Control_Manual_Record
    {
        private IContainer components;
        internal Button BTN_Record_Stop;
        internal ContextMenuStrip ContextMenu;
        internal ToolStripMenuItem CMI_Record_Stop;
        internal ToolStripMenuItem CMI_Go_Site;
        internal ToolTip ToolTip1;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
            this.BTN_Record_Stop = new Button();
            this.ContextMenu = new ContextMenuStrip(this.components);
            this.CMI_Record_Stop = new ToolStripMenuItem();
            this.CMI_Go_Site = new ToolStripMenuItem();
            this.ToolTip1 = new ToolTip(this.components);

            // 
            // BTN_Record_Stop
            // 
            this.BTN_Record_Stop.Dock = DockStyle.Left;
            this.BTN_Record_Stop.Size = new Size(24, 36);
            this.BTN_Record_Stop.Image = Resources.control_stop_icon;
            this.BTN_Record_Stop.Click += new EventHandler(this.BTN_Record_Stop_Click);

            // 
            // CMI_Record_Stop
            // 
            this.CMI_Record_Stop.Text = "Aufnahme beenden";
            this.CMI_Record_Stop.Click += new EventHandler(this.CMI_Record_Stop_Click);

            // 
            // CMI_Go_Site
            // 
            this.CMI_Go_Site.Text = "Webseite anzeigen";
            this.CMI_Go_Site.Click += new EventHandler(this.CMI_Go_Site_Click);

            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new ToolStripItem[] {
                this.CMI_Record_Stop,
                this.CMI_Go_Site
            });
            this.ContextMenuStrip = this.ContextMenu;

            // 
            // Control_Manual_Record
            // 
            this.Controls.Add(this.BTN_Record_Stop);
            this.Size = new Size(166, 36);

            this.Load += new EventHandler(this.Control_Manual_Record_Load);
            this.Paint += new PaintEventHandler(this.Control_Manual_Record_Paint);
            this.MouseDoubleClick += new MouseEventHandler(this.Control_Manual_Record_MouseDoubleClick);
        }
    }
}
