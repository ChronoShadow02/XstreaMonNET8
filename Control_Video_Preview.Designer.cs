using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace XstreaMonNET8
{
    public partial class Control_Video_Preview : UserControl
    {
        private IContainer components;
        private Panel panelPreview;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuOpen;
        private ToolStripMenuItem menuDelete;
        private ToolStripSeparator sep1;
        private ToolStripMenuItem menuFolder;
        private ToolStripMenuItem menuConvertMp4;
        private ToolStripSeparator sep2;
        private ToolStripMenuItem menuProperties;
        private Panel panelInfo;
        private Button btnPlay;
        private Button btnGallery;
        private Button btnDelete;
        private CheckBox chkFavorite;
        private ToolTip toolTip;
        private System.Windows.Forms.Timer previewTimer;
        private System.Windows.Forms.Timer convertTimer;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            panelPreview = new Panel();
            contextMenu = new ContextMenuStrip(components);
            menuOpen = new ToolStripMenuItem();
            menuDelete = new ToolStripMenuItem();
            sep1 = new ToolStripSeparator();
            menuFolder = new ToolStripMenuItem();
            menuConvertMp4 = new ToolStripMenuItem();
            sep2 = new ToolStripSeparator();
            menuProperties = new ToolStripMenuItem();
            panelInfo = new Panel();
            btnPlay = new Button();
            btnGallery = new Button();
            btnDelete = new Button();
            chkFavorite = new CheckBox();
            toolTip = new ToolTip(components);
            previewTimer = new System.Windows.Forms.Timer(components);
            convertTimer = new System.Windows.Forms.Timer(components);

            // panelPreview
            panelPreview.Dock = DockStyle.Bottom;
            panelPreview.Height = 28;
            panelPreview.ContextMenuStrip = contextMenu;
            panelPreview.Paint += PanelPreview_Paint;
            panelPreview.DoubleClick += PanelPreview_DoubleClick;

            // contextMenu
            contextMenu.Items.AddRange(new ToolStripItem[]{
                menuOpen, menuDelete, sep1, menuFolder, menuConvertMp4, sep2, menuProperties
            });

            menuOpen.Text = "Öffnen";
            menuOpen.Click += MenuOpen_Click;
            menuDelete.Text = "Löschen";
            menuDelete.Click += MenuDelete_Click;
            menuFolder.Text = "Speicherort öffnen";
            menuFolder.Click += MenuFolder_Click;
            menuConvertMp4.Text = "Konvertieren zur MP4 Datei";
            menuConvertMp4.Click += MenuConvertMp4_Click;
            menuProperties.Text = "Eigenschaften";
            menuProperties.Click += MenuProperties_Click;

            // panelInfo
            panelInfo.Dock = DockStyle.Right;
            panelInfo.Width = 56;
            panelInfo.Controls.Add(btnDelete);
            panelInfo.Controls.Add(btnGallery);
            panelInfo.Controls.Add(btnPlay);
            panelInfo.Controls.Add(chkFavorite);

            // btnPlay
            btnPlay.Dock = DockStyle.Top;
            btnPlay.Text = "▶";
            btnPlay.Click += BtnPlay_Click;
            // btnGallery
            btnGallery.Dock = DockStyle.Top;
            btnGallery.Text = "🖼";
            btnGallery.Click += BtnGallery_Click;
            // btnDelete
            btnDelete.Dock = DockStyle.Bottom;
            btnDelete.Text = "🗑";
            btnDelete.Click += MenuDelete_Click;
            // chkFavorite
            chkFavorite.Dock = DockStyle.Top;
            chkFavorite.Text = "★";
            chkFavorite.CheckedChanged += ChkFavorite_CheckedChanged;

            // previewTimer
            previewTimer.Interval = 250;
            previewTimer.Tick += PreviewTimer_Tick;

            // convertTimer
            convertTimer.Interval = 500;
            convertTimer.Tick += ConvertTimer_Tick;

            // Control
            this.Controls.Add(panelPreview);
            this.Controls.Add(panelInfo);
            this.DoubleBuffered = true;
            this.Size = new Size(404, 250);
        }
    }
}
