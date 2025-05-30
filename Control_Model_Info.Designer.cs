using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Control_Model_Info
    {
        private IContainer components;
        internal Label lblInfo;
        internal PictureBox picPreview;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();

            // 
            // picPreview
            // 
            this.picPreview = new PictureBox
            {
                Dock = DockStyle.Left,
                Size = new Size(230, 138),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // 
            // lblInfo
            // 
            this.lblInfo = new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft
            };

            // 
            // Control_Model_Info
            // 
            this.Controls.Add(lblInfo);
            this.Controls.Add(picPreview);
            this.Size = new Size(452, 138);
        }
    }
}
