using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Control_Image_Preview
    {
        private IContainer components;
        internal Panel PAN_Info;
        internal Label LAB_Größe;
        internal Button BTN_Delete;
        internal Button BTN_Galerie;
        internal Button BTN_Play;
        internal ToolTip TTP_Control;
        internal ContextMenuStrip CMI_Image;
        internal ToolStripMenuItem CMI_Öffnen;
        internal ToolStripMenuItem CMI_Löschen;
        internal ToolStripSeparator RadMenuSeparatorItem1;
        internal ToolStripMenuItem CMI_Directory_Open;
        internal ToolStripSeparator RadMenuSeparatorItem2;
        internal ToolStripMenuItem CMI_Property;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
            this.CMI_Image = new ContextMenuStrip(this.components);
            this.CMI_Öffnen = new ToolStripMenuItem("Öffnen");
            this.CMI_Löschen = new ToolStripMenuItem("Löschen");
            this.RadMenuSeparatorItem1 = new ToolStripSeparator();
            this.CMI_Directory_Open = new ToolStripMenuItem("Speicherort öffnen");
            this.RadMenuSeparatorItem2 = new ToolStripSeparator();
            this.CMI_Property = new ToolStripMenuItem("Eigenschaften");

            // 
            // CMI_Image
            // 
            this.CMI_Image.Items.AddRange(new ToolStripItem[] {
                this.CMI_Öffnen,
                this.CMI_Löschen,
                this.RadMenuSeparatorItem1,
                this.CMI_Directory_Open,
                this.RadMenuSeparatorItem2,
                this.CMI_Property
            });

            // 
            // PAN_Info
            // 
            this.PAN_Info = new Panel
            {
                Dock = DockStyle.Right,
                Size = new Size(49, 250),
                BackColor = Color.FromArgb(100, 255, 255, 255),
                ContextMenuStrip = this.CMI_Image
            };

            // 
            // BTN_Play
            // 
            this.BTN_Play = new Button
            {
                Dock = DockStyle.Top,
                Text = "▶",
                Height = 34
            };
            this.BTN_Play.Click += new EventHandler(this.BTN_Play_Click);

            // 
            // BTN_Galerie
            // 
            this.BTN_Galerie = new Button
            {
                Dock = DockStyle.Top,
                Text = "🖼",
                Height = 34
            };
            this.BTN_Galerie.Click += new EventHandler(this.BTN_Galerie_Click);

            // 
            // LAB_Größe
            // 
            this.LAB_Größe = new Label
            {
                Dock = DockStyle.Top,
                Height = 14,
                TextAlign = ContentAlignment.MiddleRight
            };

            // 
            // BTN_Delete
            // 
            this.BTN_Delete = new Button
            {
                Dock = DockStyle.Bottom,
                Text = "🗑",
                Height = 34
            };
            this.BTN_Delete.Click += new EventHandler(this.BTN_Delete_Click);

            // 
            // TTP_Control
            // 
            this.TTP_Control = new ToolTip(this.components);
            this.TTP_Control.SetToolTip(this.BTN_Delete, "Video löschen");
            this.TTP_Control.SetToolTip(this.BTN_Play, "Video öffnen");
            this.TTP_Control.SetToolTip(this.BTN_Galerie, "Galerie öffnen");

            // 
            // PAN_Info.Controls
            // 
            this.PAN_Info.Controls.Add(this.BTN_Delete);
            this.PAN_Info.Controls.Add(this.LAB_Größe);
            this.PAN_Info.Controls.Add(this.BTN_Galerie);
            this.PAN_Info.Controls.Add(this.BTN_Play);

            // 
            // Control_Image_Preview
            // 
            this.Controls.Add(this.PAN_Info);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            this.Size = new Size(404, 250);
        }
    }
}
