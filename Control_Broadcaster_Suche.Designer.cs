using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Control_Broadcaster_Suche
    {
        private IContainer components;
        internal ContextMenuStrip menuContextual;
        internal ToolStripMenuItem itemAceptar;
        internal ToolStripMenuItem itemWeb;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
            this.menuContextual = new ContextMenuStrip(this.components);
            this.itemAceptar = new ToolStripMenuItem();
            this.itemWeb = new ToolStripMenuItem();

            // 
            // itemAceptar
            // 
            this.itemAceptar.Name = "itemAceptar";
            this.itemAceptar.Text = TXT.TXT_Description("Übernehmen");
            this.itemAceptar.Click += new EventHandler(this.MIT_Uebernehmen_Click);
            // 
            // itemWeb
            // 
            this.itemWeb.Name = "itemWeb";
            this.itemWeb.Text = TXT.TXT_Description("Webseite öffnen");
            this.itemWeb.Click += new EventHandler(this.MIT_Webseite_Click);
            // 
            // menuContextual
            // 
            this.menuContextual.Items.AddRange(new ToolStripItem[] {
                this.itemAceptar,
                this.itemWeb
            });
            this.menuContextual.Name = "menuContextual";
            // 
            // Control_Broadcaster_Suche
            // 
            this.ContextMenuStrip = this.menuContextual;
        }
    }
}
