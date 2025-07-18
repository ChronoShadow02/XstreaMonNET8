using System.Drawing;
using System.Windows.Forms;

namespace XstreaMonNET8
{
    partial class Dialog_FirstStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dialog_FirstStart));
            this.RadLabel1 = new System.Windows.Forms.Label(); // Replaced RadLabel with Label
            this.LAB_1 = new System.Windows.Forms.Label(); // Replaced RadLabel with Label
            this.LAB_2 = new System.Windows.Forms.Label(); // Replaced RadLabel with Label
            this.LAB_3 = new System.Windows.Forms.Label(); // Replaced RadLabel with Label
            this.LAB_4 = new System.Windows.Forms.Label(); // Replaced RadLabel with Label
            this.DDL_Languages = new System.Windows.Forms.ComboBox(); // Replaced RadDropDownList with ComboBox
            this.BTN_Accept = new System.Windows.Forms.Button(); // Replaced RadButton with Button
            this.BTN_Abort = new System.Windows.Forms.Button(); // Replaced RadButton with Button
            this.BTN_Folder = new System.Windows.Forms.Button(); // Replaced RadButton with Button
            this.TXB_Folder = new System.Windows.Forms.TextBox(); // Replaced RadTextBox with TextBox
            this.LAB_5 = new System.Windows.Forms.Label(); // Replaced RadLabel with Label
            this.TSW_New_Channel = new System.Windows.Forms.CheckBox(); // Replaced RadToggleSwitch with CheckBox
            this.DDL_Webseite = new System.Windows.Forms.ComboBox(); // Replaced RadDropDownList with ComboBox
            this.LAB_Webseite = new System.Windows.Forms.Label(); // Replaced RadLabel with Label
            this.SuspendLayout();
            //
            // RadLabel1
            //
            this.RadLabel1.AutoSize = false; // Set to false for custom sizing
            this.RadLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RadLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.RadLabel1.Location = new System.Drawing.Point(0, 0);
            this.RadLabel1.Name = "RadLabel1";
            this.RadLabel1.Size = new System.Drawing.Size(481, 124);
            this.RadLabel1.TabIndex = 7;
            // Assuming XstreaMon.My.Resources.Resources.Logo is accessible, otherwise replace with actual image path
            // For native controls, you'd typically set Image property directly or use a PictureBox
            // For now, leaving as is, assuming it's handled externally or will be replaced.
            // this.RadLabel1.Image = ((System.Drawing.Image)(resources.GetObject("RadLabel1.Image"))); // Example if it's an image resource
            //
            // LAB_1
            //
            this.LAB_1.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.LAB_1.Location = new System.Drawing.Point(12, 130);
            this.LAB_1.Name = "LAB_1";
            this.LAB_1.Size = new System.Drawing.Size(215, 29);
            this.LAB_1.TabIndex = 8;
            this.LAB_1.Text = "Welcome to XstreaMon!";
            //
            // LAB_2
            //
            this.LAB_2.AutoSize = false; // Set to false for custom sizing
            this.LAB_2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LAB_2.Location = new System.Drawing.Point(12, 161);
            this.LAB_2.Name = "LAB_2";
            this.LAB_2.Size = new System.Drawing.Size(457, 42);
            this.LAB_2.TabIndex = 9;
            this.LAB_2.Text = "Before we start XstreaMon needs some information from you.";
            //
            // LAB_3
            //
            this.LAB_3.AutoSize = false; // Set to false for custom sizing
            this.LAB_3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.LAB_3.Location = new System.Drawing.Point(12, 209);
            this.LAB_3.Name = "LAB_3";
            this.LAB_3.Size = new System.Drawing.Size(457, 27);
            this.LAB_3.TabIndex = 10;
            this.LAB_3.Text = "Select the language you want XstreaMon to run in.";
            //
            // LAB_4
            //
            this.LAB_4.AutoSize = false; // Set to false for custom sizing
            this.LAB_4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.LAB_4.Location = new System.Drawing.Point(12, 287);
            this.LAB_4.Name = "LAB_4";
            this.LAB_4.Size = new System.Drawing.Size(457, 25);
            this.LAB_4.TabIndex = 11;
            this.LAB_4.Text = "Select the directory where the videos and data can be saved.";
            //
            // DDL_Languages
            //
            this.DDL_Languages.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.DDL_Languages.Location = new System.Drawing.Point(12, 239);
            this.DDL_Languages.Name = "DDL_Languages";
            this.DDL_Languages.Size = new System.Drawing.Size(457, 25); // ComboBox height is usually 25-27
            this.DDL_Languages.TabIndex = 0;
            this.DDL_Languages.DropDownStyle = ComboBoxStyle.DropDownList; // Equivalent to DropDownAnimationEnabled and LimitToList
            this.DDL_Languages.SelectedValueChanged += new System.EventHandler(this.DDL_Languages_SelectedValueChanged);
            //
            // BTN_Accept
            //
            this.BTN_Accept.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.BTN_Accept.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.BTN_Accept.Location = new System.Drawing.Point(213, 433);
            this.BTN_Accept.Name = "BTN_Accept";
            this.BTN_Accept.Size = new System.Drawing.Size(125, 35);
            this.BTN_Accept.TabIndex = 5;
            this.BTN_Accept.Text = "Let's go";
            this.BTN_Accept.UseVisualStyleBackColor = true;
            this.BTN_Accept.Click += new System.EventHandler(this.BTN_Accept_Click);
            //
            // BTN_Abort
            //
            this.BTN_Abort.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.BTN_Abort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_Abort.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.BTN_Abort.Location = new System.Drawing.Point(344, 433);
            this.BTN_Abort.Name = "BTN_Abort";
            this.BTN_Abort.Size = new System.Drawing.Size(125, 35);
            this.BTN_Abort.TabIndex = 6;
            this.BTN_Abort.Text = "No better not";
            this.BTN_Abort.UseVisualStyleBackColor = true;
            //
            // BTN_Folder
            //
            // For native Button, Image property is used. Assuming XstreaMon.My.Resources.Resources.Folder16 is accessible.
            // Otherwise, replace with actual image path or resource.
            // this.BTN_Folder.Image = ((System.Drawing.Image)(resources.GetObject("BTN_Folder.Image")));
            this.BTN_Folder.Image = ((System.Drawing.Image)(Resources.Folder16)); // Example if Folder16 is in project resources
            this.BTN_Folder.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BTN_Folder.Location = new System.Drawing.Point(443, 314);
            this.BTN_Folder.Name = "BTN_Folder";
            this.BTN_Folder.Size = new System.Drawing.Size(26, 24);
            this.BTN_Folder.TabIndex = 2;
            this.BTN_Folder.UseVisualStyleBackColor = true;
            this.BTN_Folder.Click += new System.EventHandler(this.BTN_Folder_Click);
            //
            // TXB_Folder
            //
            this.TXB_Folder.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.TXB_Folder.Location = new System.Drawing.Point(12, 315);
            this.TXB_Folder.Name = "TXB_Folder";
            this.TXB_Folder.Size = new System.Drawing.Size(425, 25); // TextBox height is usually 23-25
            this.TXB_Folder.TabIndex = 1;
            //
            // LAB_5
            //
            this.LAB_5.AutoSize = false; // Set to false for custom sizing
            this.LAB_5.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.LAB_5.Location = new System.Drawing.Point(12, 349);
            this.LAB_5.Name = "LAB_5";
            this.LAB_5.Size = new System.Drawing.Size(391, 30);
            this.LAB_5.TabIndex = 12;
            this.LAB_5.Text = "Do you want to create a new channel?";
            //
            // TSW_New_Channel
            //
            this.TSW_New_Channel.Appearance = System.Windows.Forms.Appearance.Button; // To mimic toggle switch appearance
            this.TSW_New_Channel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.TSW_New_Channel.Location = new System.Drawing.Point(409, 349);
            this.TSW_New_Channel.Name = "TSW_New_Channel";
            this.TSW_New_Channel.Size = new System.Drawing.Size(60, 30);
            this.TSW_New_Channel.TabIndex = 3;
            this.TSW_New_Channel.Text = "No"; // Default text for unchecked state
            this.TSW_New_Channel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TSW_New_Channel.UseVisualStyleBackColor = true;
            this.TSW_New_Channel.CheckedChanged += new System.EventHandler(this.TSW_New_Channel_ValueChanged); // Use CheckedChanged for value change
            // Custom drawing might be needed for "OnText" and "OffText" visual
            // For simplicity, we'll update text in CheckedChanged event
            //
            // DDL_Webseite
            //
            this.DDL_Webseite.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.DDL_Webseite.Location = new System.Drawing.Point(241, 385);
            this.DDL_Webseite.Name = "DDL_Webseite";
            this.DDL_Webseite.Size = new System.Drawing.Size(228, 25); // ComboBox height
            this.DDL_Webseite.TabIndex = 4;
            this.DDL_Webseite.DropDownStyle = ComboBoxStyle.DropDownList; // Equivalent to DropDownAnimationEnabled and LimitToList
            //
            // LAB_Webseite
            //
            this.LAB_Webseite.AutoSize = false; // Set to false for custom sizing
            this.LAB_Webseite.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.LAB_Webseite.Location = new System.Drawing.Point(12, 385);
            this.LAB_Webseite.Name = "LAB_Webseite";
            this.LAB_Webseite.Size = new System.Drawing.Size(223, 23);
            this.LAB_Webseite.TabIndex = 13;
            this.LAB_Webseite.Text = "Start with website";
            //
            // Dialog_FirstStart
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 480);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Dialog_FirstStart";
            this.Text = "Dialog_FirstStart";
            // ShapedForm and RoundRectShape are Telerik specific. For native forms, this is usually not needed
            // unless you implement custom form painting. Removing for native conversion.
            // this.Shape = (ElementShape) this.RoundRectShapeForm;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Internal virtual properties for controls
        internal System.Windows.Forms.Label RadLabel1;
        internal System.Windows.Forms.Label LAB_1;
        internal System.Windows.Forms.Label LAB_2;
        internal System.Windows.Forms.Label LAB_3;
        internal System.Windows.Forms.Label LAB_4;
        internal System.Windows.Forms.ComboBox DDL_Languages;
        internal System.Windows.Forms.Button BTN_Accept;
        internal System.Windows.Forms.Button BTN_Abort;
        internal System.Windows.Forms.Button BTN_Folder;
        internal System.Windows.Forms.TextBox TXB_Folder;
        internal System.Windows.Forms.Label LAB_5;
        internal System.Windows.Forms.CheckBox TSW_New_Channel; // Changed to CheckBox
        internal System.Windows.Forms.ComboBox DDL_Webseite;
        internal System.Windows.Forms.Label LAB_Webseite;

        // Removed RoundRectShapeForm as it's Telerik specific
        // internal virtual RoundRectShape RoundRectShapeForm { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }
    }
}
