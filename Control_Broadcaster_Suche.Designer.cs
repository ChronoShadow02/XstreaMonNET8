using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    public partial class Control_Broadcaster_Suche : UserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MIT_Uebernehmen = new System.Windows.Forms.ToolStripMenuItem();
            this.MIT_Webseite = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // MIT_Uebernehmen
            // 
            this.MIT_Uebernehmen.Name = "MIT_Uebernehmen";
            this.MIT_Uebernehmen.Size = new System.Drawing.Size(180, 22);
            this.MIT_Uebernehmen.Text = "übernehmen";
            this.MIT_Uebernehmen.Click += new System.EventHandler(this.MIT_Uebernehmen_Click);
            // 
            // MIT_Webseite
            // 
            this.MIT_Webseite.Name = "MIT_Webseite";
            this.MIT_Webseite.Size = new System.Drawing.Size(180, 22);
            this.MIT_Webseite.Text = "Webseite öffnen";
            this.MIT_Webseite.Click += new System.EventHandler(this.MIT_Webseite_Click);
            // 
            // Control_Broadcaster_Suche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "Control_Broadcaster_Suche";
            this.Size = new System.Drawing.Size(584, 48);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStripMenuItem MIT_Uebernehmen;
        internal System.Windows.Forms.ToolStripMenuItem MIT_Webseite;
    }
}
