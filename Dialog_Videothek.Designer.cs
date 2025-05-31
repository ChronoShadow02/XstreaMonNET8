// Archivo: Dialog_Videothek.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Dialog_Videothek
    {
        private IContainer components = null;

        internal ContextMenuStrip CMI_Galerie;
        internal ToolStripMenuItem CMI_Channel;
        internal ToolStripMenuItem CMI_Tiles;
        internal Panel PAN_Galerie;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Dialog_Videothek));

            // ======================================
            // CMI_Galerie (ContextMenuStrip)
            // ======================================
            this.CMI_Galerie = new ContextMenuStrip(this.components);
            this.CMI_Channel = new ToolStripMenuItem();
            this.CMI_Tiles = new ToolStripMenuItem();

            this.CMI_Galerie.Items.AddRange(new ToolStripItem[]
            {
                this.CMI_Channel,
                this.CMI_Tiles
            });

            this.CMI_Channel.CheckOnClick = true;
            this.CMI_Channel.Name = "CMI_Channel";
            this.CMI_Channel.Size = new Size(176, 22);
            this.CMI_Channel.Text = "Name";
            this.CMI_Channel.Click += new EventHandler(this.CMI_Channel_Click);

            this.CMI_Tiles.CheckOnClick = true;
            this.CMI_Tiles.Name = "CMI_Tiles";
            this.CMI_Tiles.Size = new Size(176, 22);
            this.CMI_Tiles.Text = "Kachelansicht";
            this.CMI_Tiles.Click += new EventHandler(this.CMI_Tiles_Click);

            // ======================================
            // PAN_Galerie (Panel)
            // ======================================
            this.PAN_Galerie = new Panel();
            this.PAN_Galerie.AutoScroll = true;
            this.PAN_Galerie.ContextMenuStrip = this.CMI_Galerie;
            this.PAN_Galerie.Dock = DockStyle.Fill;
            this.PAN_Galerie.Location = new Point(0, 0);
            this.PAN_Galerie.Name = "PAN_Galerie";
            this.PAN_Galerie.Size = new Size(686, 390);
            this.PAN_Galerie.TabIndex = 0;

            // ======================================
            // Dialog_Videothek (Form)
            // ======================================
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoValidate = AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new Size(686, 390);
            this.Controls.Add(this.PAN_Galerie);
            this.ContextMenuStrip = this.CMI_Galerie;
            this.DoubleBuffered = false;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Name = "Dialog_Videothek";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Galerie";
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
