using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Dialog_FirstStart
    {
        private IContainer components = null;
        private Label RadLabel1;
        private Label LAB_1;
        private Label LAB_2;
        private Label LAB_3;
        private Label LAB_4;
        private ComboBox DDL_Languages;
        private Button BTN_Accept;
        private Button BTN_Abort;
        private Button BTN_Folder;
        private TextBox TXB_Folder;
        private Label LAB_5;
        private CheckBox TSW_New_Channel;
        private ComboBox DDL_Webseite;
        private Label LAB_Webseite;

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
            this.RadLabel1 = new Label();
            this.LAB_1 = new Label();
            this.LAB_2 = new Label();
            this.LAB_3 = new Label();
            this.LAB_4 = new Label();
            this.DDL_Languages = new ComboBox();
            this.BTN_Accept = new Button();
            this.BTN_Abort = new Button();
            this.BTN_Folder = new Button();
            this.TXB_Folder = new TextBox();
            this.LAB_5 = new Label();
            this.TSW_New_Channel = new CheckBox();
            this.DDL_Webseite = new ComboBox();
            this.LAB_Webseite = new Label();
            this.SuspendLayout();
            // 
            // RadLabel1
            // 
            this.RadLabel1.AutoSize = false;
            this.RadLabel1.BackgroundImageLayout = ImageLayout.Zoom;
            this.RadLabel1.Dock = DockStyle.Top;
            this.RadLabel1.Location = new Point(0, 0);
            this.RadLabel1.Name = "RadLabel1";
            this.RadLabel1.Size = new Size(481, 124);
            this.RadLabel1.TabIndex = 7;
            // 
            // LAB_1
            // 
            this.LAB_1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold | FontStyle.Italic);
            this.LAB_1.Location = new Point(12, 130);
            this.LAB_1.Name = "LAB_1";
            this.LAB_1.Size = new Size(215, 29);
            this.LAB_1.TabIndex = 8;
            this.LAB_1.Text = "Welcome to XstreaMon!";
            // 
            // LAB_2
            // 
            this.LAB_2.AutoSize = false;
            this.LAB_2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.LAB_2.Location = new Point(12, 161);
            this.LAB_2.Name = "LAB_2";
            this.LAB_2.Size = new Size(457, 42);
            this.LAB_2.TabIndex = 9;
            this.LAB_2.Text = "Before we start XstreaMon needs some information from you.";
            // 
            // LAB_3
            // 
            this.LAB_3.AutoSize = false;
            this.LAB_3.Font = new Font("Segoe UI", 9.75F);
            this.LAB_3.Location = new Point(12, 209);
            this.LAB_3.Name = "LAB_3";
            this.LAB_3.Size = new Size(457, 27);
            this.LAB_3.TabIndex = 10;
            this.LAB_3.Text = "Select the language you want XstreaMon to run in.";
            // 
            // DDL_Languages
            // 
            this.DDL_Languages.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DDL_Languages.Font = new Font("Segoe UI", 9.75F);
            this.DDL_Languages.FormattingEnabled = true;
            this.DDL_Languages.Location = new Point(12, 239);
            this.DDL_Languages.Name = "DDL_Languages";
            this.DDL_Languages.Size = new Size(457, 23);
            this.DDL_Languages.TabIndex = 0;
            this.DDL_Languages.SelectedIndexChanged += new EventHandler(this.DDL_Languages_SelectedValueChanged);
            // 
            // LAB_4
            // 
            this.LAB_4.AutoSize = false;
            this.LAB_4.Font = new Font("Segoe UI", 9.75F);
            this.LAB_4.Location = new Point(12, 287);
            this.LAB_4.Name = "LAB_4";
            this.LAB_4.Size = new Size(457, 25);
            this.LAB_4.TabIndex = 11;
            this.LAB_4.Text = "Select the directory where the videos and data can be saved.";
            // 
            // TXB_Folder
            // 
            this.TXB_Folder.Font = new Font("Segoe UI", 9.75F);
            this.TXB_Folder.Location = new Point(12, 315);
            this.TXB_Folder.Name = "TXB_Folder";
            this.TXB_Folder.Size = new Size(425, 23);
            this.TXB_Folder.TabIndex = 1;
            // 
            // BTN_Folder
            // 
            this.BTN_Folder.Location = new Point(443, 314);
            this.BTN_Folder.Name = "BTN_Folder";
            this.BTN_Folder.Size = new Size(26, 24);
            this.BTN_Folder.TabIndex = 2;
            this.BTN_Folder.Text = "...";
            this.BTN_Folder.UseVisualStyleBackColor = true;
            this.BTN_Folder.Click += new EventHandler(this.BTN_Folder_Click);
            // 
            // LAB_5
            // 
            this.LAB_5.AutoSize = false;
            this.LAB_5.Font = new Font("Segoe UI", 9.75F);
            this.LAB_5.Location = new Point(12, 349);
            this.LAB_5.Name = "LAB_5";
            this.LAB_5.Size = new Size(391, 30);
            this.LAB_5.TabIndex = 12;
            this.LAB_5.Text = "Do you want to create a new channel?";
            // 
            // TSW_New_Channel
            // 
            this.TSW_New_Channel.Appearance = Appearance.Button;
            this.TSW_New_Channel.Font = new Font("Segoe UI", 9.75F);
            this.TSW_New_Channel.Location = new Point(409, 349);
            this.TSW_New_Channel.Name = "TSW_New_Channel";
            this.TSW_New_Channel.Size = new Size(60, 30);
            this.TSW_New_Channel.TabIndex = 3;
            this.TSW_New_Channel.Text = "No";
            this.TSW_New_Channel.TextAlign = ContentAlignment.MiddleCenter;
            this.TSW_New_Channel.CheckedChanged += new EventHandler(this.TSW_New_Channel_ValueChanged);
            // 
            // DDL_Webseite
            // 
            this.DDL_Webseite.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DDL_Webseite.Font = new Font("Segoe UI", 9.75F);
            this.DDL_Webseite.FormattingEnabled = true;
            this.DDL_Webseite.Location = new Point(241, 385);
            this.DDL_Webseite.Name = "DDL_Webseite";
            this.DDL_Webseite.Size = new Size(228, 23);
            this.DDL_Webseite.TabIndex = 4;
            // 
            // LAB_Webseite
            // 
            this.LAB_Webseite.AutoSize = false;
            this.LAB_Webseite.Font = new Font("Segoe UI", 9.75F);
            this.LAB_Webseite.Location = new Point(12, 385);
            this.LAB_Webseite.Name = "LAB_Webseite";
            this.LAB_Webseite.Size = new Size(223, 23);
            this.LAB_Webseite.TabIndex = 13;
            this.LAB_Webseite.Text = "Start with website";
            // 
            // BTN_Accept
            // 
            this.BTN_Accept.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.BTN_Accept.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.BTN_Accept.Location = new Point(213, 433);
            this.BTN_Accept.Name = "BTN_Accept";
            this.BTN_Accept.Size = new Size(125, 35);
            this.BTN_Accept.TabIndex = 5;
            this.BTN_Accept.Text = "Let's go";
            this.BTN_Accept.UseVisualStyleBackColor = true;
            this.BTN_Accept.Click += new EventHandler(this.BTN_Accept_Click);
            // 
            // BTN_Abort
            // 
            this.BTN_Abort.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.BTN_Abort.DialogResult = DialogResult.Cancel;
            this.BTN_Abort.Font = new Font("Segoe UI Semibold", 9.5F);
            this.BTN_Abort.Location = new Point(344, 433);
            this.BTN_Abort.Name = "BTN_Abort";
            this.BTN_Abort.Size = new Size(125, 35);
            this.BTN_Abort.TabIndex = 6;
            this.BTN_Abort.Text = "No better not";
            this.BTN_Abort.UseVisualStyleBackColor = true;
            // 
            // Dialog_FirstStart
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(481, 480);
            this.Controls.Add(this.LAB_Webseite);
            this.Controls.Add(this.DDL_Webseite);
            this.Controls.Add(this.TSW_New_Channel);
            this.Controls.Add(this.LAB_5);
            this.Controls.Add(this.BTN_Folder);
            this.Controls.Add(this.TXB_Folder);
            this.Controls.Add(this.BTN_Abort);
            this.Controls.Add(this.BTN_Accept);
            this.Controls.Add(this.DDL_Languages);
            this.Controls.Add(this.LAB_4);
            this.Controls.Add(this.LAB_3);
            this.Controls.Add(this.LAB_2);
            this.Controls.Add(this.LAB_1);
            this.Controls.Add(this.RadLabel1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Dialog_FirstStart";
            this.Text = "Dialog_FirstStart";
            this.ResumeLayout(false);
        }
    }
}
