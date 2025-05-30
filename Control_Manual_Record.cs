using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XstreaMonNET8
{
    public partial class Control_Manual_Record : UserControl
    {
        private Class_Manual_Record Pri_Man_Record;
        private string Record_Time_String;
        private string ToolTipText;
        private System.Timers.Timer Record_Time;

        internal Class_Stream_Record Pro_Record_Stream { get; set; }
        internal string Pro_Channel_Name { get; set; }
        internal DateTime Pro_Record_Start { get; set; }

        internal delegate void Evt_Record_CloseEventHandler(Control_Manual_Record Control_Record);
        internal event Evt_Record_CloseEventHandler Evt_Record_Close;

        public Control_Manual_Record(Class_Manual_Record Man_Record)
        {
            InitializeComponent();

            Pro_Channel_Name = Man_Record.Pro_Channel_Name;
            Pro_Record_Start = Man_Record.Pro_Channel_Stream.Pro_Record_Beginn == DateTime.MinValue
                ? DateTime.Now
                : Man_Record.Pro_Channel_Stream.Pro_Record_Beginn;
            Pro_Record_Stream = Man_Record.Pro_Channel_Stream;
            Pri_Man_Record = Man_Record;
            Record_Time_String = $"{(int)(DateTime.Now - Pro_Record_Start).TotalMinutes} Minuten";

            Record_Time = new System.Timers.Timer(10000);
            Record_Time.Elapsed += Record_Time_Elapsed;
            Record_Time.Start();
        }

        private void Set_ToolTipText()
        {
            try
            {
                var path = Pri_Man_Record.Pro_Channel_Stream.Pro_Recordname;
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    long fileSize = new FileInfo(path).Length;
                    ToolTipText = $"{Pro_Channel_Name}\r\n" +
                                  $"Start: {Pro_Record_Start.ToLocalTime()}\r\n" +
                                  $"{Record_Time_String}\r\n" +
                                  $"Größe: {ValueBack.Get_Numeric2Bytes(fileSize)}";
                }
                else
                {
                    ToolTipText = "";
                }
                ToolTip1.SetToolTip(this, ToolTipText);
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Manual_Record.Set_ToolTipText");
            }
        }

        private void Control_Manual_Record_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(50, Color.Red)),
                    new Rectangle(2, 2, Width - 4, Height - 4));

                e.Graphics.DrawString(
                    Pro_Channel_Name,
                    new Font(Font, FontStyle.Bold),
                    new SolidBrush(ForeColor),
                    new PointF(26, 2));

                e.Graphics.DrawString(
                    Record_Time_String,
                    Font,
                    new SolidBrush(ForeColor),
                    new PointF(26, 16));
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Manual_Record.Paint");
            }
        }

        private void Record_Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Record_Time.Stop();
                Record_Time_String = $"{(int)(DateTime.Now - Pro_Record_Start).TotalMinutes} Min";

                if (Pro_Record_Stream == null ||
                    !Parameter.Task_Runs(Pro_Record_Stream.Pro_Record_PID))
                {
                    Record_Stop();
                }
                else
                {
                    Invalidate();
                    Record_Time.Start();
                }

                Set_ToolTipText();
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Manual_Record.Record_Time_Elapsed");
            }
        }

        private void Control_Manual_Record_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (ParentForm?.Name == "CamBrowser")
                    ((CamBrowser)ParentForm).WebView_Source = Pri_Man_Record.Pro_Channel_URL;
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Manual_Record.DoubleClick");
            }
        }

        private void CMI_Go_Site_Click(object sender, EventArgs e)
            => Control_Manual_Record_MouseDoubleClick(sender, null);

        private void CMI_Record_Stop_Click(object sender, EventArgs e)
            => Record_Stop();

        private void BTN_Record_Stop_Click(object sender, EventArgs e)
            => Record_Stop();

        private void Record_Stop()
        {
            try
            {
                var record = Class_Record_Manual.Find_Record(
                    Pri_Man_Record.Pro_Channel_Name,
                    Pri_Man_Record.Pro_Channel_Stream.Pro_Website_ID);

                if (record?.Pro_Channel_Stream != null)
                {
                    Class_Record_Manual.Stop_Record(
                        record.Pro_Channel_Name,
                        record.Pro_Channel_Stream.Pro_Website_ID);

                    Evt_Record_Close?.Invoke(this);
                }
            }
            catch (Exception ex)
            {
                Parameter.Error_Message(ex, "Control_Manual_Record.Record_Stop");
            }
        }

        private void Control_Manual_Record_Load(object sender, EventArgs e)
        {
            CMI_Go_Site.Text = TXT.TXT_Description("Webseite öffnen");
            CMI_Record_Stop.Text = TXT.TXT_Description("Aufnahme beenden");
            Set_ToolTipText();
        }
    }
}
