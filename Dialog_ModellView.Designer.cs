// Archivo: Dialog_ModellView.Designer.cs
namespace XstreaMonNET8
{
    partial class Dialog_ModellView : Form
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Declaración de controles (sin cambiar nombres)
        private System.Windows.Forms.SplitContainer RadSplitContainer1;
        private System.Windows.Forms.Panel SPA_Info;
        private System.Windows.Forms.Panel SPA_Gallerie;

        private System.Windows.Forms.Panel RadPanel1;
        private System.Windows.Forms.Panel RadPanel2;
        private System.Windows.Forms.Panel RadPanel3;

        private System.Windows.Forms.Label LAB_Verzeichnis;
        private System.Windows.Forms.Button BTN_Explorer;
        private System.Windows.Forms.Button BTN_Einstellungen;

        private System.Windows.Forms.Label LAB_Dateien;
        private System.Windows.Forms.Label LAB_Größe;
        private System.Windows.Forms.PictureBox RadPictureBox1;
        private System.Windows.Forms.Label LAB_Geschlecht;
        private System.Windows.Forms.Label LAB_Country;
        private System.Windows.Forms.Label LAB_Languages;
        private System.Windows.Forms.Label LAB_Info;

        private System.Windows.Forms.Panel PAN_Work;
        private System.Windows.Forms.ProgressBar PGB_Fortschritt;
        private System.Windows.Forms.Label LAB_TimeRun;
        private System.Windows.Forms.Panel PAN_Container;

        private System.Windows.Forms.ContextMenuStrip CME_Ansicht;
        private System.Windows.Forms.ToolStripMenuItem CMI_Datum;
        private System.Windows.Forms.ToolStripMenuItem CMI_Tiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CMI_Sort;
        private System.Windows.Forms.ToolStripMenuItem CMI_Sort_Datum;
        private System.Windows.Forms.ToolStripMenuItem CMI_Sort_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CMI_Sort_ASC;
        private System.Windows.Forms.ToolStripMenuItem CMI_Sort_DESC;
        private System.Windows.Forms.ToolStripMenuItem CMI_Image;
        private System.Windows.Forms.ToolStripMenuItem CMI_Videos;


        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ContextMenuStrip y ToolStripMenuItems
            this.CME_Ansicht = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMI_Datum = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Tiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Sort = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Sort_Datum = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Sort_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CMI_Sort_ASC = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Sort_DESC = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Image = new System.Windows.Forms.ToolStripMenuItem();
            this.CMI_Videos = new System.Windows.Forms.ToolStripMenuItem();

            // SplitContainer principal (reemplaza RadSplitContainer1)
            this.RadSplitContainer1 = new System.Windows.Forms.SplitContainer();

            // Panel izquierdo (equivalente a SPA_Info)
            this.SPA_Info = new System.Windows.Forms.Panel();

            // Panel derecho (equivalente a SPA_Gallerie)
            this.SPA_Gallerie = new System.Windows.Forms.Panel();

            // Paneles internos del lado izquierdo
            this.RadPanel2 = new System.Windows.Forms.Panel();
            this.RadPanel3 = new System.Windows.Forms.Panel();
            this.RadPanel1 = new System.Windows.Forms.Panel();

            // Controles dentro de RadPanel3
            this.LAB_Verzeichnis = new System.Windows.Forms.Label();
            this.BTN_Explorer = new System.Windows.Forms.Button();
            this.BTN_Einstellungen = new System.Windows.Forms.Button();

            // Controles de etiquetas e imagen en RadPanel2
            this.LAB_Dateien = new System.Windows.Forms.Label();
            this.LAB_Größe = new System.Windows.Forms.Label();
            this.RadPictureBox1 = new System.Windows.Forms.PictureBox();
            this.LAB_Geschlecht = new System.Windows.Forms.Label();
            this.LAB_Country = new System.Windows.Forms.Label();
            this.LAB_Languages = new System.Windows.Forms.Label();
            this.LAB_Info = new System.Windows.Forms.Label();

            // Paneles internos del lado derecho
            this.PAN_Work = new System.Windows.Forms.Panel();
            this.PGB_Fortschritt = new System.Windows.Forms.ProgressBar();
            this.LAB_TimeRun = new System.Windows.Forms.Label();
            this.PAN_Container = new System.Windows.Forms.Panel();

            // Asignar propiedades de ContextMenuStrip
            this.CME_Ansicht.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CMI_Datum,
                this.CMI_Tiles,
                this.toolStripSeparator1,
                this.CMI_Sort,
                this.CMI_Image,
                this.CMI_Videos
            });
            this.CME_Ansicht.Name = "CME_Ansicht";
            this.CME_Ansicht.Size = new System.Drawing.Size(181, 148);

            // CMI_Datum
            this.CMI_Datum.CheckOnClick = true;
            this.CMI_Datum.Name = "CMI_Datum";
            this.CMI_Datum.Size = new System.Drawing.Size(180, 22);
            this.CMI_Datum.Text = "Gruppieren nach Datum";

            // CMI_Tiles
            this.CMI_Tiles.CheckOnClick = true;
            this.CMI_Tiles.Name = "CMI_Tiles";
            this.CMI_Tiles.Size = new System.Drawing.Size(180, 22);
            this.CMI_Tiles.Text = "Kachelansicht";

            // Separador
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);

            // CMI_Sort (elemento principal)
            this.CMI_Sort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.CMI_Sort_Datum,
                this.CMI_Sort_Name,
                this.toolStripSeparator2,
                this.CMI_Sort_ASC,
                this.CMI_Sort_DESC
            });
            this.CMI_Sort.Name = "CMI_Sort";
            this.CMI_Sort.Size = new System.Drawing.Size(180, 22);
            this.CMI_Sort.Text = "Sortieren nach";

            // CMI_Sort_Datum
            this.CMI_Sort_Datum.CheckOnClick = true;
            this.CMI_Sort_Datum.Name = "CMI_Sort_Datum";
            this.CMI_Sort_Datum.Size = new System.Drawing.Size(180, 22);
            this.CMI_Sort_Datum.Text = "Erstelldatum";

            // CMI_Sort_Name
            this.CMI_Sort_Name.CheckOnClick = true;
            this.CMI_Sort_Name.Name = "CMI_Sort_Name";
            this.CMI_Sort_Name.Size = new System.Drawing.Size(180, 22);
            this.CMI_Sort_Name.Text = "Dateiname";

            // Separador dentro de CMI_Sort
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);

            // CMI_Sort_ASC
            this.CMI_Sort_ASC.CheckOnClick = true;
            this.CMI_Sort_ASC.Name = "CMI_Sort_ASC";
            this.CMI_Sort_ASC.Size = new System.Drawing.Size(180, 22);
            this.CMI_Sort_ASC.Text = "Aufsteigend";

            // CMI_Sort_DESC
            this.CMI_Sort_DESC.CheckOnClick = true;
            this.CMI_Sort_DESC.Name = "CMI_Sort_DESC";
            this.CMI_Sort_DESC.Size = new System.Drawing.Size(180, 22);
            this.CMI_Sort_DESC.Text = "Absteigend";

            // CMI_Image
            this.CMI_Image.CheckOnClick = true;
            this.CMI_Image.Name = "CMI_Image";
            this.CMI_Image.Size = new System.Drawing.Size(180, 22);
            this.CMI_Image.Text = "Bilder";

            // CMI_Videos
            this.CMI_Videos.CheckOnClick = true;
            this.CMI_Videos.Name = "CMI_Videos";
            this.CMI_Videos.Size = new System.Drawing.Size(180, 22);
            this.CMI_Videos.Text = "Videos";

            // Configuración de RadSplitContainer1 (SplitContainer estándar)
            this.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.RadSplitContainer1.Name = "RadSplitContainer1";
            // Orientación vertical (Panel1 = izquierda, Panel2 = derecha)
            this.RadSplitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical;
            // Tamaño total del formulario
            this.RadSplitContainer1.Size = new System.Drawing.Size(1186, 755);
            // Ancho del panel izquierdo = 250
            this.RadSplitContainer1.SplitterDistance = 250;
            this.RadSplitContainer1.SplitterWidth = 1;
            this.RadSplitContainer1.TabIndex = 0;

            // Panel1 del SplitContainer → SPA_Info
            this.SPA_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SPA_Info.Location = new System.Drawing.Point(0, 0);
            this.SPA_Info.Name = "SPA_Info";
            this.SPA_Info.Size = new System.Drawing.Size(250, 755);
            this.SPA_Info.TabIndex = 0;

            // Panel2 del SplitContainer → SPA_Gallerie
            this.SPA_Gallerie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SPA_Gallerie.Location = new System.Drawing.Point(0, 0);
            this.SPA_Gallerie.Name = "SPA_Gallerie";
            this.SPA_Gallerie.Size = new System.Drawing.Size(935, 755);
            this.SPA_Gallerie.TabIndex = 1;

            // ===== Construcción del panel izquierdo (SPA_Info) =====

            // RadPanel2: contenedor superior dentro de SPA_Info
            this.RadPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.RadPanel2.Location = new System.Drawing.Point(0, 0);
            this.RadPanel2.Name = "RadPanel2";
            this.RadPanel2.Size = new System.Drawing.Size(250, 287);
            this.RadPanel2.TabIndex = 0;

            // RadPanel3: fila superior dentro de RadPanel2 para carpeta y botones
            this.RadPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.RadPanel3.Location = new System.Drawing.Point(0, 0);
            this.RadPanel3.Name = "RadPanel3";
            this.RadPanel3.Size = new System.Drawing.Size(250, 30);
            this.RadPanel3.TabIndex = 0;

            // LAB_Verzeichnis
            this.LAB_Verzeichnis.AutoSize = false;
            this.LAB_Verzeichnis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LAB_Verzeichnis.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.LAB_Verzeichnis.Location = new System.Drawing.Point(0, 0);
            this.LAB_Verzeichnis.Name = "LAB_Verzeichnis";
            this.LAB_Verzeichnis.Size = new System.Drawing.Size(194, 30);
            this.LAB_Verzeichnis.TabIndex = 0;
            this.LAB_Verzeichnis.Text = "";

            // BTN_Explorer
            this.BTN_Explorer.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_Explorer.Location = new System.Drawing.Point(194, 0);
            this.BTN_Explorer.Name = "BTN_Explorer";
            this.BTN_Explorer.Size = new System.Drawing.Size(28, 30);
            this.BTN_Explorer.TabIndex = 1;
            this.BTN_Explorer.Text = "…";

            // BTN_Einstellungen
            this.BTN_Einstellungen.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_Einstellungen.Location = new System.Drawing.Point(222, 0);
            this.BTN_Einstellungen.Name = "BTN_Einstellungen";
            this.BTN_Einstellungen.Size = new System.Drawing.Size(28, 30);
            this.BTN_Einstellungen.TabIndex = 2;
            this.BTN_Einstellungen.Text = "⚙";

            // Agregar controles a RadPanel3
            this.RadPanel3.Controls.Add(this.LAB_Verzeichnis);
            this.RadPanel3.Controls.Add(this.BTN_Explorer);
            this.RadPanel3.Controls.Add(this.BTN_Einstellungen);

            // LAB_Dateien
            this.LAB_Dateien.AutoSize = false;
            this.LAB_Dateien.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAB_Dateien.Location = new System.Drawing.Point(0, 30);
            this.LAB_Dateien.Name = "LAB_Dateien";
            this.LAB_Dateien.Size = new System.Drawing.Size(250, 20);
            this.LAB_Dateien.TabIndex = 1;
            this.LAB_Dateien.Text = "";

            // LAB_Größe
            this.LAB_Größe.AutoSize = false;
            this.LAB_Größe.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAB_Größe.Location = new System.Drawing.Point(0, 50);
            this.LAB_Größe.Name = "LAB_Größe";
            this.LAB_Größe.Size = new System.Drawing.Size(250, 20);
            this.LAB_Größe.TabIndex = 2;
            this.LAB_Größe.Text = "";

            // RadPictureBox1 (convertido a PictureBox estándar)
            this.RadPictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.RadPictureBox1.Location = new System.Drawing.Point(0, 70);
            this.RadPictureBox1.Name = "RadPictureBox1";
            this.RadPictureBox1.Size = new System.Drawing.Size(250, 160);
            this.RadPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RadPictureBox1.TabIndex = 3;
            this.RadPictureBox1.TabStop = false;

            // LAB_Geschlecht
            this.LAB_Geschlecht.AutoSize = false;
            this.LAB_Geschlecht.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAB_Geschlecht.Location = new System.Drawing.Point(0, 230);
            this.LAB_Geschlecht.Name = "LAB_Geschlecht";
            this.LAB_Geschlecht.Size = new System.Drawing.Size(250, 20);
            this.LAB_Geschlecht.TabIndex = 4;
            this.LAB_Geschlecht.Text = "";

            // LAB_Country
            this.LAB_Country.AutoSize = false;
            this.LAB_Country.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAB_Country.Location = new System.Drawing.Point(0, 250);
            this.LAB_Country.Name = "LAB_Country";
            this.LAB_Country.Size = new System.Drawing.Size(250, 20);
            this.LAB_Country.TabIndex = 5;
            this.LAB_Country.Text = "";

            // LAB_Languages
            this.LAB_Languages.AutoSize = false;
            this.LAB_Languages.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAB_Languages.Location = new System.Drawing.Point(0, 270);
            this.LAB_Languages.Name = "LAB_Languages";
            this.LAB_Languages.Size = new System.Drawing.Size(250, 20);
            this.LAB_Languages.TabIndex = 6;
            this.LAB_Languages.Text = "";

            // LAB_Info
            this.LAB_Info.AutoSize = false;
            this.LAB_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAB_Info.Location = new System.Drawing.Point(0, 290);
            this.LAB_Info.Name = "LAB_Info";
            this.LAB_Info.Size = new System.Drawing.Size(250, 20);
            this.LAB_Info.TabIndex = 7;
            this.LAB_Info.Text = "";

            // Agregar controles a RadPanel2 (de arriba a abajo)
            this.RadPanel2.Controls.Add(this.LAB_Info);
            this.RadPanel2.Controls.Add(this.LAB_Languages);
            this.RadPanel2.Controls.Add(this.LAB_Country);
            this.RadPanel2.Controls.Add(this.LAB_Geschlecht);
            this.RadPanel2.Controls.Add(this.RadPictureBox1);
            this.RadPanel2.Controls.Add(this.LAB_Größe);
            this.RadPanel2.Controls.Add(this.LAB_Dateien);
            this.RadPanel2.Controls.Add(this.RadPanel3);

            // RadPanel1 (contenedor de historial, área con AutoScroll)
            this.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadPanel1.Location = new System.Drawing.Point(0, 287);
            this.RadPanel1.Name = "RadPanel1";
            this.RadPanel1.AutoScroll = true;
            this.RadPanel1.Size = new System.Drawing.Size(250, 468);
            this.RadPanel1.TabIndex = 1;

            // Agregar RadPanel2 y RadPanel1 a SPA_Info
            this.SPA_Info.Controls.Add(this.RadPanel1);
            this.SPA_Info.Controls.Add(this.RadPanel2);

            // ===== Construcción del panel derecho (SPA_Gallerie) =====

            // PAN_Work: barra de progreso superior
            this.PAN_Work.Dock = System.Windows.Forms.DockStyle.Top;
            this.PAN_Work.Location = new System.Drawing.Point(0, 0);
            this.PAN_Work.Name = "PAN_Work";
            this.PAN_Work.Size = new System.Drawing.Size(935, 30);
            this.PAN_Work.TabIndex = 0;
            this.PAN_Work.Visible = false;

            // PGB_Fortschritt
            this.PGB_Fortschritt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PGB_Fortschritt.Location = new System.Drawing.Point(0, 0);
            this.PGB_Fortschritt.Name = "PGB_Fortschritt";
            this.PGB_Fortschritt.Size = new System.Drawing.Size(822, 30);
            this.PGB_Fortschritt.TabIndex = 0;

            // LAB_TimeRun
            this.LAB_TimeRun.AutoSize = false;
            this.LAB_TimeRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.LAB_TimeRun.Location = new System.Drawing.Point(822, 0);
            this.LAB_TimeRun.Name = "LAB_TimeRun";
            this.LAB_TimeRun.Size = new System.Drawing.Size(113, 30);
            this.LAB_TimeRun.TabIndex = 1;
            this.LAB_TimeRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LAB_TimeRun.Text = "";

            // Agregar PGB_Fortschritt y LAB_TimeRun a PAN_Work
            this.PAN_Work.Controls.Add(this.PGB_Fortschritt);
            this.PAN_Work.Controls.Add(this.LAB_TimeRun);

            // PAN_Container: contenedor principal de vistas de imágenes/videos
            this.PAN_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PAN_Container.Location = new System.Drawing.Point(0, 30);
            this.PAN_Container.Name = "PAN_Container";
            this.PAN_Container.AutoScroll = true;
            this.PAN_Container.Size = new System.Drawing.Size(935, 725);
            this.PAN_Container.TabIndex = 1;

            // Asignar ContextMenuStrip a PAN_Container
            this.PAN_Container.ContextMenuStrip = this.CME_Ansicht;

            // Agregar PAN_Work y PAN_Container a SPA_Gallerie
            this.SPA_Gallerie.Controls.Add(this.PAN_Container);
            this.SPA_Gallerie.Controls.Add(this.PAN_Work);

            // ===== Agregar paneles al SplitContainer =====
            this.RadSplitContainer1.Panel1.Controls.Add(this.SPA_Info);
            this.RadSplitContainer1.Panel2.Controls.Add(this.SPA_Gallerie);

            // ===== Configuración final del formulario =====
            this.ClientSize = new System.Drawing.Size(1186, 755);
            this.Controls.Add(this.RadSplitContainer1);
            this.Name = "Dialog_ModellView";
            this.Text = "Model View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        #endregion
    }
}
