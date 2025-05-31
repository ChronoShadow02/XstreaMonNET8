namespace XstreaMonNET8
{
    partial class Control_Video_Preview
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Declaración de controles (sin cambiar nombres)
        private System.Windows.Forms.Panel PAN_Vorschau;
        private System.Windows.Forms.ContextMenuStrip CMI_Video;
        private System.Windows.Forms.ToolStripMenuItem CMI_Öffnen;
        private System.Windows.Forms.ToolStripMenuItem CMI_Löschen;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem1;
        private System.Windows.Forms.ToolStripMenuItem CMI_Directory_Open;
        private System.Windows.Forms.ToolStripMenuItem CMI_Convert_MP4;
        private System.Windows.Forms.ToolStripSeparator RadMenuSeparatorItem2;
        private System.Windows.Forms.ToolStripMenuItem CMI_Property;

        private System.Windows.Forms.Panel PAN_Info;
        private System.Windows.Forms.Button BTN_Delete;
        private System.Windows.Forms.Label LAB_Video_Propertys;
        public System.Windows.Forms.Button BTN_Galerie;
        private System.Windows.Forms.Button BTN_Play;
        private System.Windows.Forms.CheckBox TBT_Favorite;
        private System.Windows.Forms.ToolTip TTP_Control;

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ContextMenuStrip y ToolStripMenuItems
            this.CMI_Video = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMI_Öffnen = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Löschen = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Directory_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Convert_MP4 = new System.Windows.Forms.ToolStripMenuItem();
            this.RadMenuSeparatorItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Property = new System.Windows.Forms.ToolStripMenuItem();

            // Panel PAN_Vorschau
            this.PAN_Vorschau = new System.Windows.Forms.Panel();

            // Panel PAN_Info
            this.PAN_Info = new System.Windows.Forms.Panel();

            // Botones y controles dentro de PAN_Info
            this.BTN_Delete = new System.Windows.Forms.Button();
            this.LAB_Video_Propertys = new System.Windows.Forms.Label();
            this.BTN_Galerie = new System.Windows.Forms.Button();
            this.BTN_Play = new System.Windows.Forms.Button();
            this.TBT_Favorite = new System.Windows.Forms.CheckBox();

            // ToolTip
            this.TTP_Control = new System.Windows.Forms.ToolTip(this.components);

            // 
            // CMI_Video
            // 
            this.CMI_Video.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CMI_Öffnen,
                this.CMI_Löschen,
                this.RadMenuSeparatorItem1,
                this.CMI_Directory_Open,
                this.CMI_Convert_MP4,
                this.RadMenuSeparatorItem2,
                this.CMI_Property
            });
            this.CMI_Video.Name = "CMI_Video";
            this.CMI_Video.Size = new System.Drawing.Size(214, 126);

            // 
            // CMI_Öffnen
            // 
            this.CMI_Öffnen.Name = "CMI_Öffnen";
            this.CMI_Öffnen.Size = new System.Drawing.Size(213, 22);
            this.CMI_Öffnen.Text = "Öffnen";

            // 
            // CMI_Löschen
            // 
            this.CMI_Löschen.Name = "CMI_Löschen";
            this.CMI_Löschen.Size = new System.Drawing.Size(213, 22);
            this.CMI_Löschen.Text = "Löschen";

            // 
            // RadMenuSeparatorItem1
            // 
            this.RadMenuSeparatorItem1.Name = "RadMenuSeparatorItem1";
            this.RadMenuSeparatorItem1.Size = new System.Drawing.Size(210, 6);

            // 
            // CMI_Directory_Open
            // 
            this.CMI_Directory_Open.Name = "CMI_Directory_Open";
            this.CMI_Directory_Open.Size = new System.Drawing.Size(213, 22);
            this.CMI_Directory_Open.Text = "Speicherort öffnen";

            // 
            // CMI_Convert_MP4
            // 
            this.CMI_Convert_MP4.Name = "CMI_Convert_MP4";
            this.CMI_Convert_MP4.Size = new System.Drawing.Size(213, 22);
            this.CMI_Convert_MP4.Text = "Convert to mp4";

            // 
            // RadMenuSeparatorItem2
            // 
            this.RadMenuSeparatorItem2.Name = "RadMenuSeparatorItem2";
            this.RadMenuSeparatorItem2.Size = new System.Drawing.Size(210, 6);

            // 
            // CMI_Property
            // 
            this.CMI_Property.Name = "CMI_Property";
            this.CMI_Property.Size = new System.Drawing.Size(213, 22);
            this.CMI_Property.Text = "Eigenschaften";

            // 
            // PAN_Vorschau
            // 
            this.PAN_Vorschau.BackColor = System.Drawing.Color.Transparent;
            this.PAN_Vorschau.ContextMenuStrip = this.CMI_Video;
            this.PAN_Vorschau.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PAN_Vorschau.Location = new System.Drawing.Point(0, 222);
            this.PAN_Vorschau.Name = "PAN_Vorschau";
            this.PAN_Vorschau.Size = new System.Drawing.Size(404, 28);
            this.PAN_Vorschau.TabIndex = 0;

            // 
            // PAN_Info
            // 
            this.PAN_Info.BackColor = System.Drawing.Color.FromArgb(100, 255, 255, 255);
            this.PAN_Info.ContextMenuStrip = this.CMI_Video;
            this.PAN_Info.Dock = System.Windows.Forms.DockStyle.Right;
            this.PAN_Info.Location = new System.Drawing.Point(348, 0);
            this.PAN_Info.Margin = new System.Windows.Forms.Padding(0);
            this.PAN_Info.Name = "PAN_Info";
            this.PAN_Info.Size = new System.Drawing.Size(56, 222);
            this.PAN_Info.TabIndex = 1;

            // 
            // TBT_Favorite (CheckBox utilizado como ToggleButton)
            // 
            this.TBT_Favorite.Appearance = System.Windows.Forms.Appearance.Button;
            this.TBT_Favorite.BackColor = System.Drawing.Color.Transparent;
            this.TBT_Favorite.ContextMenuStrip = this.CMI_Video;
            this.TBT_Favorite.Dock = System.Windows.Forms.DockStyle.Top;
            this.TBT_Favorite.Location = new System.Drawing.Point(0, 0);
            this.TBT_Favorite.Name = "TBT_Favorite";
            this.TBT_Favorite.Size = new System.Drawing.Size(56, 34);
            this.TBT_Favorite.TabIndex = 0;
            this.TBT_Favorite.Text = "";
            this.TBT_Favorite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TTP_Control.SetToolTip(this.TBT_Favorite, "Favorite");

            // 
            // BTN_Play
            // 
            this.BTN_Play.BackColor = System.Drawing.Color.Transparent;
            this.BTN_Play.ContextMenuStrip = this.CMI_Video;
            this.BTN_Play.Dock = System.Windows.Forms.DockStyle.Top;
            this.BTN_Play.Location = new System.Drawing.Point(0, 34);
            this.BTN_Play.Name = "BTN_Play";
            this.BTN_Play.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.BTN_Play.Size = new System.Drawing.Size(56, 34);
            this.BTN_Play.TabIndex = 1;
            this.BTN_Play.Text = "";
            this.TTP_Control.SetToolTip(this.BTN_Play, "Video öffnen");

            // 
            // BTN_Galerie
            // 
            this.BTN_Galerie.BackColor = System.Drawing.Color.Transparent;
            this.BTN_Galerie.ContextMenuStrip = this.CMI_Video;
            this.BTN_Galerie.Dock = System.Windows.Forms.DockStyle.Top;
            this.BTN_Galerie.Location = new System.Drawing.Point(0, 68);
            this.BTN_Galerie.Name = "BTN_Galerie";
            this.BTN_Galerie.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.BTN_Galerie.Size = new System.Drawing.Size(56, 34);
            this.BTN_Galerie.TabIndex = 2;
            this.BTN_Galerie.Text = "";
            this.TTP_Control.SetToolTip(this.BTN_Galerie, "Galerie öffnen");

            // 
            // LAB_Video_Propertys
            // 
            this.LAB_Video_Propertys.AutoSize = false;
            this.LAB_Video_Propertys.BackColor = System.Drawing.Color.Transparent;
            this.LAB_Video_Propertys.ContextMenuStrip = this.CMI_Video;
            this.LAB_Video_Propertys.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAB_Video_Propertys.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.LAB_Video_Propertys.Location = new System.Drawing.Point(0, 102);
            this.LAB_Video_Propertys.Margin = new System.Windows.Forms.Padding(0);
            this.LAB_Video_Propertys.Name = "LAB_Video_Propertys";
            this.LAB_Video_Propertys.Size = new System.Drawing.Size(56, 80);
            this.LAB_Video_Propertys.TabIndex = 3;
            this.LAB_Video_Propertys.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // 
            // BTN_Delete
            // 
            this.BTN_Delete.BackColor = System.Drawing.Color.Transparent;
            this.BTN_Delete.ContextMenuStrip = this.CMI_Video;
            this.BTN_Delete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BTN_Delete.Location = new System.Drawing.Point(0, 188);
            this.BTN_Delete.Name = "BTN_Delete";
            this.BTN_Delete.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.BTN_Delete.Size = new System.Drawing.Size(56, 34);
            this.BTN_Delete.TabIndex = 4;
            this.BTN_Delete.Image = (Image)Resources.Thrash32;
            this.TTP_Control.SetToolTip(this.BTN_Delete, "Video löschen");

            // Agregar controles a PAN_Info (de arriba a abajo, salvo BTN_Delete al fondo)
            this.PAN_Info.Controls.Add(this.BTN_Delete);
            this.PAN_Info.Controls.Add(this.LAB_Video_Propertys);
            this.PAN_Info.Controls.Add(this.BTN_Galerie);
            this.PAN_Info.Controls.Add(this.BTN_Play);
            this.PAN_Info.Controls.Add(this.TBT_Favorite);

            // 
            // Control_Video_Preview (UserControl)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.CMI_Video;
            this.Controls.Add(this.PAN_Info);
            this.Controls.Add(this.PAN_Vorschau);
            this.DoubleBuffered = true;
            this.Name = "Control_Video_Preview";
            this.Size = new System.Drawing.Size(404, 250);
        }

        #region Código generado por el Diseñador de Windows Forms

        #endregion
    }
}
