using System.ComponentModel;
using System.Diagnostics;

namespace XstreaMonNET8
{
    partial class Con_User_Online
    {
        private IContainer components;
        internal ContextMenuStrip CME_User_Online;
        internal ToolStripMenuItem CMI_Open_Site;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            CME_User_Online = new ContextMenuStrip(components);
            CMI_Open_Site = new ToolStripMenuItem();
            CME_User_Online.SuspendLayout();
            SuspendLayout();
            // 
            // CME_User_Online
            // 
            CME_User_Online.ImageScalingSize = new Size(20, 20);
            CME_User_Online.Items.AddRange(new ToolStripItem[] { CMI_Open_Site });
            CME_User_Online.Name = "CME_User_Online";
            CME_User_Online.Size = new Size(204, 28);
            // 
            // CMI_Open_Site
            // 
            CMI_Open_Site.Name = "CMI_Open_Site";
            CMI_Open_Site.Size = new Size(203, 24);
            CMI_Open_Site.Text = "Webseite anzeigen";
            CMI_Open_Site.Click += CMI_Open_Site_Click;
            // 
            // Con_User_Online
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ContextMenuStrip = CME_User_Online;
            Margin = new Padding(4, 5, 4, 5);
            Name = "Con_User_Online";
            Size = new Size(293, 37);
            CME_User_Online.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
